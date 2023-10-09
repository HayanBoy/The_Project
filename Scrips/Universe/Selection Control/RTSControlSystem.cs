using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RTSControlSystem : MonoBehaviour
{
    [Header("스크립트")]
    public UIControlSystem UIControlSystem;
    public FlagshipAttackSkill FlagshipAttackSkill;
    public CameraZoom CameraZoom;
    public UniverseMapSystem UniverseMapSystem;
    public CameraFollow CameraFollow;
    public MultiFlagshipSystem MultiFlagshipSystem;
    MoveVelocity moveVelocity;

    [Header("좌표")]
    [SerializeField] private Transform SelectionAreaTransform;
    Coroutine selectionAreaOn;
    public Vector3 StartPosition;
    private Collider2D SelectCollider2D;
    public int FormationRange;

    [Header("레이어")]
    public LayerMask EnemiesShipLayer;
    public LayerMask ShipFormationLayer;

    [Header("모드 스위치")]
    public bool ShipSelected = false; //편대모드에서 함선이 선택된 상태에서 카메라가 움직이지 않는다.

    private bool Tapped = false; //탭이 한번 이루어졌으며, 더블 탭 기능이 시작
    private bool TapMoved = false; //함선 선택 후, 이동 액션을 조취할 때의 스위치
    private int TapAccount; //탭한 횟수. 일정 시간내로 더블탭했을 때의 조건이 시작
    private float TapTime; //더블탭을 위한 제한시간
    private float StayPushTime; //누르는 시간을 위한 제한시간
    private bool DontAction = false; //작업 중에서는 함선 조작 불가

    [Header("카메라 내 선택")]
    public int ShipNumber; //선택된 함선 번호
    public GameObject SelectShipsInCameraTop;
    public GameObject SelectShipsInCameraDown;

    [Header("명령 이펙트")]
    public GameObject ShipMoveClickUI; //이동명령을 했을 때의 클릭 이펙트
    public GameObject ShipActionUI; //편대모드에서 선택된 함선이 이동할 자리를 표시해주는 UI. 함선을 터치한 직후, 터치한 좌표에 스폰된다.
    public GameObject ShipAttackUI; //편대모드에서 선택된 함선이 공격을 표시해주는 UI. 함선을 터치한 직후, 터치한 좌표에 스폰된다.

    //기함 모드시, 나머지 개별 함선 선택을 강제로 해제 한 뒤, 가장 첫번째 기함을 자동 선택
    public void FlagShipSelectionMode()
    {
        ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().MoveOrder = false;
        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipAccount; i++)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().MoveOrder = false;
            ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().FormationOn = true;
        }
        foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
        {
            shipRTS.SetSelectedVisible(false);
        }
        ShipManager.instance.SelectedShipList.Clear();
        Debug.Log("기함 모드 활성화, 선택된 기함 : " + ShipManager.instance.FlagShipList[0]);
    }
    public void FlagShipSelectionModeWarp(GameObject Flagship) //워프 시 자동으로 함선들이 기함 모드로 전환
    {
        Flagship.GetComponent<MoveVelocity>().MoveOrderStemp = 0;
        Flagship.GetComponent<MoveVelocity>().MoveOrder = false;
        for (int i = 0; i < Flagship.GetComponent<FollowShipManager>().ShipAccount; i++)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().MoveOrder = false;
            Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().FormationOn = true;
        }
        foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
        {
            shipRTS.SetSelectedVisible(false);
        }
        ShipManager.instance.SelectedShipList.Clear();
    }

    //편대 모드 선택시, 선택된 기함이 취소
    public void SelectShipMode()
    {
        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipAccount; i++)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().FormationOn = false;
        }
        ShipManager.instance.SelectedShipList.Clear();
        Debug.Log("함선 선택 모드 활성화");
    }

    //버튼을 누른 지역의 위치를 변환
    Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private void Awake()
    {
        SelectionAreaTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        //기함 모드
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            {
                if (UIControlSystem.FlagShipBehaviorMode == true && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && ShipSelected == false) //기함 이동
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended) //터치로 명령
                        {
                            //Debug.Log("기함 이동");
                            Vector3 MoveToPosition = GetMouseWorldPosition();
                            GameObject MoveOrder = Instantiate(ShipMoveClickUI, MoveToPosition, Quaternion.identity);
                            Destroy(MoveOrder, 1.5f);
                            ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
                            shipRTS.MoveTo(MoveToPosition);
                        }
                    }
                    if (Input.GetMouseButtonDown(1) && UIControlSystem.FlagshipEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false) //마우스 클릭으로 명령
                    {
                        Vector3 MoveToPosition = GetMouseWorldPosition();
                        ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
                        shipRTS.MoveTo(MoveToPosition);
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //카메라 이동중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //카메라 줌 사용중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (UIControlSystem.FlagShipBehaviorMode == false && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && ShipSelected == false) //기함 공격
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                        {
                            StartPosition = GetMouseWorldPosition(); //지정된 지역으로 선택
                            StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);

                            Collider2D collider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, EnemiesShipLayer); //적 단일 대상 선택
                            if (collider2D != null)
                            {
                                collider2D.gameObject.transform.parent.GetComponent<EnemyShipNavigator>().TargetMark();
                                GameObject gameObject = collider2D.gameObject; //선택된 대상 콜라이더를 오브젝트로 변환
                                ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().TargetEngage(gameObject); //기함이 함대에게 일점사 공격 명령

                                //Debug.Log("기함 대상 공격");
                            }
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //카메라 이동중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //카메라 줌 사용중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == true && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true) //배열 모드
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began) //배열 지정
                        {
                            StartPosition = GetMouseWorldPosition(); //지정된 지역으로 선택
                            StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);
                            SelectCollider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, ShipFormationLayer);

                            if (SelectCollider2D != null)
                            {
                                ShipSelected = true;
                            }
                        }
                        if (touch.phase == TouchPhase.Moved) //배열 재배치
                        {
                            if (Vector2.Distance(ShipManager.instance.SelectedFlagShip[0].transform.position, SelectCollider2D.gameObject.transform.position) < FormationRange)
                                SelectCollider2D.gameObject.transform.position = Vector2.MoveTowards(SelectCollider2D.gameObject.transform.position, GetMouseWorldPosition(), 1000 * Time.deltaTime);
                        }
                        if (touch.phase == TouchPhase.Ended)
                        {
                            ShipSelected = false;
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //카메라 이동중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //카메라 줌 사용중에는 함선 이동이 불가
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    //일정 거리 이상 벗어나면 되돌아오기
                    if (SelectCollider2D != null)
                    {
                        if (Vector2.Distance(ShipManager.instance.SelectedFlagShip[0].transform.position, SelectCollider2D.gameObject.transform.position) >= FormationRange)
                            SelectCollider2D.gameObject.transform.position = Vector2.MoveTowards(SelectCollider2D.gameObject.transform.position, ShipManager.instance.SelectedFlagShip[0].transform.position, 100 * Time.deltaTime);
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true) //멀티 기함 선택
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                        {
                            //탭 시작 영역
                            TapAccount++; //탭 시작할 때 마다 추가
                            TapTime = 0;
                            StayPushTime = 0;
                            Tapped = true; //탭 시작을 알림
                            StartPosition = GetMouseWorldPosition(); //지정된 지역으로 선택
                            StartPosition = new Vector2(StartPosition.x - (0.25f / 2), StartPosition.y + (0.25f / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 0.25f, StartPosition.y - 0.25f);

                            //선택 영역 내에서의 기함 1차 선택
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition);
                            foreach (Collider2D collider2D in collider2DArray)
                            {
                                ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
                                if (shipRTS != null && shipRTS.GetComponent<MoveVelocity>().FlagShip == true)
                                    ShipManager.instance.SelectedShipList.Add(shipRTS); //선택된 함선 리스트에 추가
                            }

                            //기함에서 더블탭을 진행했을 경우, 해당 기함으로 변경
                            if (Tapped == true && TapTime < 0.5f && TapAccount == 2)
                            {
                                Tapped = false;
                                TapAccount = 0;
                                TapTime = 0;
                                StayPushTime = 0;

                                if (ShipManager.instance.SelectedShipList[0].gameObject.GetComponent<MoveVelocity>().FlagshipNumber == ShipManager.instance.SelectedShipList[1].gameObject.GetComponent<MoveVelocity>().FlagshipNumber)
                                {
                                    if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == true)
                                        UIControlSystem.ShipSelectClick();

                                    ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.SelectedShipList[0].gameObject;
                                    UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
                                    ShipManager.instance.SelectedShipList.Clear();
                                    MultiFlagshipSystem.FlagshipControlMode();
                                    //Debug.Log("기함 전환");
                                }
                            }
                        }
                    }

                    if (Tapped == true && TapTime < 0.5f) //탭 한번 이루어졌을 때, 더블탭을 위한 제한시간 시작
                        TapTime += Time.deltaTime;
                    if (Tapped == true && TapTime >= 0.5f) //제한시간이 지났을 경우, 더블탭 기능 초기화
                    {
                        Tapped = false;
                        TapTime = 0;
                        TapAccount = 0;
                        ShipManager.instance.SelectedShipList.Clear();
                    }
                }
            }
            //편대 모드
            else
            {
                //선택된 유닛 이동
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && DontAction == false) //함선 이동
                {
                    if (Input.touchCount == 1 && UIControlSystem.FollowShipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraZoomOn == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began) //유닛 선택 활성화
                        {
                            if (ShipManager.instance.SelectedShipList.Count > 0) //선택된 함선이 존재할 경우, 해당 선택된 모든 유닛 미선택으로 처리
                            {
                                foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                {
                                    shipRTS.SetSelectedVisible(false);
                                }
                                ShipManager.instance.SelectedShipList.Clear();
                            }

                            //탭 시작 영역
                            TapAccount++; //탭 시작할 때 마다 추가
                            TapTime = 0;
                            StayPushTime = 0;
                            Tapped = true; //탭 시작을 알림
                            StartPosition = GetMouseWorldPosition(); //지정된 지역으로 선택
                            StartPosition = new Vector2(StartPosition.x - (0.25f / 2), StartPosition.y + (0.25f / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 0.25f, StartPosition.y - 0.25f);

                            //더블탭을 진행했을 경우, 해당 범위 내에서 함선 선택하도록 선택 영역 변경
                            if (Tapped == true && TapTime < 0.3f && TapAccount == 2)
                            {
                                Tapped = false;
                                TapAccount = 0;
                                TapTime = 0;
                                StayPushTime = 0;

                                if (selectionAreaOn != null)
                                {
                                    StopCoroutine(selectionAreaOn);
                                    SelectionAreaTransform.gameObject.SetActive(false);
                                }
                                selectionAreaOn = StartCoroutine(SelectionAreaOn()); //더블 탭 구간 애니메이션 활성화

                                StartPosition = GetMouseWorldPosition();
                                StartPosition = new Vector2(StartPosition.x - (10 / 2), StartPosition.y + (10 / 2));
                                currentMousePosition = new Vector2(StartPosition.x + 10, StartPosition.y - 10); //사각형 선택범위를 만든 다음, 터치한 곳의 중앙으로 배치
                            }

                            //선택 영역 내에서의 각 함선 유닛 선택
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition);
                            foreach (Collider2D collider2D in collider2DArray)
                            {
                                ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
                                SelecteShipRouteLine selecteShipRouteLine = collider2D.GetComponent<SelecteShipRouteLine>(); //개별 함선의 이동루트 구현
                                if (shipRTS != null)
                                {
                                    ShipNumber = shipRTS.ShipNumber;
                                    shipRTS.SetSelectedVisible(true); //선택 UI 활성화
                                    ShipActionUI.SetActive(true); //이동 루트 프리팹 활성화
                                    ShipActionUI.GetComponent<SpriteRenderer>().enabled = false; //이동 루트 스프라이트 활성화(아직 움직이지 않는 상태에서는 보이지 않는다.)
                                    ShipActionUI.transform.position = GetMouseWorldPosition(); //이동 루트 프리팹을 터치한 좌표로 이동
                                    selecteShipRouteLine.EndPosition = ShipActionUI; //이동 루트 라인의 도착 좌표를 자동 추적하기 위해 ShipActionUI로 인식
                                    ShipManager.instance.SelectedShipList.Add(shipRTS); //선택된 함선 리스트에 추가
                                    ShipSelected = true;
                                }
                            }
                        }

                        if (touch.phase == TouchPhase.Moved) //선택한 유닛에서 움직이기 위한 명령 시작
                        {
                            if (Vector2.Distance(ShipActionUI.transform.position, StartPosition) > 0.5f) //일정범위를 벗어나면 움직인 것으로 간주(아주 미세한 움직임도 계속 누르는 것으로 간주하기 위함)
                                TapMoved = true;
                            ShipActionUI.transform.position = GetMouseWorldPosition();

                            Collider2D collider2D = Physics2D.OverlapArea(ShipActionUI.transform.position, ShipActionUI.transform.position, EnemiesShipLayer); //적에게 닿으면 공격 표시로 전환
                            if (collider2D != null)
                            {
                                ShipActionUI.GetComponent<SpriteRenderer>().enabled = false;
                                ShipAttackUI.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                ShipActionUI.GetComponent<SpriteRenderer>().enabled = true;
                                ShipAttackUI.GetComponent<SpriteRenderer>().enabled = false;
                            }
                        }

                        if (touch.phase == TouchPhase.Ended) //유닛에게 명령
                        {
                            Vector3 MoveToPosition = GetMouseWorldPosition();

                            if (ShipActionUI.GetComponent<SpriteRenderer>().enabled == true) //이동 명령
                            {
                                List<Vector3> TargetPositionList = GetPositionListAround(MoveToPosition, new float[] { 2.5f, 5f, 7.5f }, new int[] { 5, 10, 20 }); //이동 시, 함대 간격
                                int TargetPositionListIndex = 0;

                                foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                {
                                    shipRTS.GetComponent<MoveVelocity>().MoveOrder = true;
                                    shipRTS.GetComponent<MoveVelocity>().ImMoving = true;
                                    shipRTS.MoveTo(TargetPositionList[TargetPositionListIndex]);
                                    TargetPositionListIndex = (TargetPositionListIndex + 1) % TargetPositionList.Count;
                                }

                                if (ShipManager.instance.SelectedShipList.Count > 0)
                                {
                                    GameObject MoveOrder = Instantiate(ShipMoveClickUI, MoveToPosition, Quaternion.identity);
                                    Destroy(MoveOrder, 1.5f);
                                }

                                if (ShipManager.instance.SelectedShipList.Count > 0) //선택된 함선이 존재할 경우, 해당 선택된 모든 유닛 미선택으로 처리
                                {
                                    foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                    {
                                        shipRTS.SetSelectedVisible(false);
                                    }
                                    ShipManager.instance.SelectedShipList.Clear();
                                }
                                //Debug.Log("함선 이동");
                            }
                            else if (ShipAttackUI.GetComponent<SpriteRenderer>().enabled == true) //공격 명령
                            {
                                StartPosition = GetMouseWorldPosition(); //지정된 지역으로 선택
                                StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                                Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);
                                Collider2D collider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, EnemiesShipLayer); //적 단일 대상 선택
                                if (collider2D != null)
                                {
                                    collider2D.gameObject.transform.parent.GetComponent<EnemyShipNavigator>().TargetMark();
                                    GameObject gameObject = collider2D.gameObject; //선택된 대상 콜라이더를 오브젝트로 변환

                                    for (int i = 0; i < ShipManager.instance.SelectedShipList.Count; i++)
                                    {
                                        ShipManager.instance.SelectedShipList[i].GetComponent<MoveVelocity>().TargetEngagePersonal(gameObject); //개별 공격
                                    }

                                    foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                    {
                                        shipRTS.SetSelectedVisible(false);
                                    }
                                    ShipManager.instance.SelectedShipList.Clear();

                                    //Debug.Log("함선 대상 공격");
                                }
                            }

                            //선택 및 명령 관련을 모두 초기화
                            StayPushTime = 0;
                            ShipSelected = false;
                            TapMoved = false;
                            ShipAttackUI.GetComponent<SpriteRenderer>().enabled = false;
                            ShipActionUI.GetComponent<SpriteRenderer>().enabled = false;
                            ShipActionUI.SetActive(false);
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //카메라 이동중에는 RTS 조작 불가
                    {
                        Touch touch = Input.GetTouch(0);
                        InitializeSelect();

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //카메라 줌 사용중에는 RTS 조작 불가
                    {
                        Touch touch = Input.GetTouch(0);
                        InitializeSelect();

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    if (Tapped == true && TapTime < 0.3f) //탭 한번 이루어졌을 때, 더블탭을 위한 제한시간 시작
                        TapTime += Time.deltaTime;
                    if (Tapped == true && TapTime >= 0.3f) //제한시간이 지났을 경우, 더블탭 기능 초기화
                    {
                        Tapped = false;
                        TapTime = 0;
                        TapAccount = 0;
                    }
                    if (Tapped == true && StayPushTime < 0.3f)
                        StayPushTime += Time.deltaTime;
                    if (StayPushTime >= 0.28f && TapMoved == false && ShipManager.instance.SelectedShipList.Count > 0) //제한시간이 지났을 경우, 누르기 기능 초기화후 해당 함선의 같은 함선들을 선택
                    {
                        StayPushTime = 0;
                        SelectSameShips(); //같은 함선 선택
                    }

                    UIControlSystem.FollowShipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                }
            }
        }
    }

    //더블 탭 구간 애니메이션
    IEnumerator SelectionAreaOn()
    {
        SelectionAreaTransform.gameObject.SetActive(true);
        SelectionAreaTransform.position = StartPosition;
        yield return new WaitForSeconds(1);
        SelectionAreaTransform.gameObject.SetActive(false);
    }

    //같은 함선 선택
    void SelectSameShips()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(SelectShipsInCameraTop.transform.position, SelectShipsInCameraDown.transform.position);
        ShipManager.instance.SelectedShipList.Clear();
        //Debug.Log("같은 함선 선택");

        foreach (Collider2D collider2D in collider2DArray)
        {
            ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
            SelecteShipRouteLine selecteShipRouteLine = collider2D.GetComponent<SelecteShipRouteLine>();

            if (shipRTS != null)
            {
                if (ShipNumber == shipRTS.ShipNumber) //같은 함선 번호인지 확인한 뒤, 같은 번호 함선만 골라서 선택한다.
                {
                    shipRTS.SetSelectedVisible(true);
                    ShipActionUI.SetActive(true);
                    ShipActionUI.GetComponent<SpriteRenderer>().enabled = false;
                    ShipActionUI.transform.position = GetMouseWorldPosition();
                    selecteShipRouteLine.EndPosition = ShipActionUI;
                    ShipManager.instance.SelectedShipList.Add(shipRTS);
                    ShipSelected = true;
                }
            }
        }
    }

    //RTS 선택 초기화
    void InitializeSelect()
    {
        ShipSelected = false;
        TapMoved = false;
        Tapped = false;
        TapAccount = 0;
        TapTime = 0;
        StayPushTime = 0;
        ShipActionUI.GetComponent<SpriteRenderer>().enabled = false;
        ShipActionUI.SetActive(false);

        foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
        {
            shipRTS.SetSelectedVisible(false);
        }
        ShipManager.instance.SelectedShipList.Clear();
    }

    //위치 정렬 계산
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] RingDistanceArray, int[] RingPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < RingDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, RingDistanceArray[i], RingPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float Distance, int PositionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < PositionCount; i++)
        {
            float angle = i * (360 / PositionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * Distance;
            positionList.Add(position);
        }
        return positionList;
    }

    //GetpositionListAround 반환
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    //이동 중, 함대들이 기함으로부터 독립적으로 도착지점으로 이동
    public void FormationTransfer(Vector3 transform)
    {
        ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
        shipRTS.WarpLocationGet(transform, ShipManager.instance.SelectedFlagShip[0].transform.rotation, ShipManager.instance.SelectedFlagShip[0]);
    }

    //어딘가 작업 중이거나 터치를 하면 안되는 영역을 눌렀을 경우, 함선이 이동하거나 공격 명령을 내리지 못하도록 조취
    public void DoNotClickWhenTaskDown()
    {
        DontAction = true;
    }
    public void DoNotClickWhenTaskUp()
    {
        DontAction = false;
    }
}