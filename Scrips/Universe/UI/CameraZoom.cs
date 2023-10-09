using Cinemachine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("스크립트")]
    public UIControlSystem UIControlSystem;
    public UniverseMapSystem UniverseMapSystem;
    public RTSControlSystem RTSControlSystem;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public FleetMenuSystem FleetMenuSystem;
    public MultiFlagshipSystem MultiFlagshipSystem;
    DataSaveManager DataSaveManager;

    [Header("카메라 동작 및 줌")]
    public int ZoomMaxSize;
    public int ZoomMinSize;
    public float ZoomModifierSpeed; //카메라 줌 속도
    public int CameraZoomUsing = 0; //카메라 줌 사용여부
    public float BattleZoom; //우주 전투 화면에서의 줌 값
    public float UniverseMapZoom; //우주 맵 화면에서의 줌 값

    [Header("카메라 오브젝트 및 위치")]
    public Camera MainCamera;
    public GameObject MainCameraPrefab;
    public CinemachineVirtualCamera CVCamera;
    public GameObject CameraUI;
    public GameObject CameraPrefab;
    public Image CameraImage;
    public Transform MainCameraMove; //카메라 조이스틱에서 CVCamera가 따라갈 투명물체
    private Transform MainCameraMove2; //함대 메뉴에서 함선 

        [Header("함대 메뉴 카메라 조작")]
    public float FleetMenuZoomMaxSize, FleetMenuZoomMinSize;

    [Header("우주맵 카메라 조작")]
    public Transform MapCameraMove;
    public SpriteRenderer MapSizeImage;
    public float MapZoomMaxSize, MapZoomMinSize;
    private float MaxMapX, MinMapX, MaxMapY, MinMapY;

    [Header("우주맵 카메라 눈동자")]
    public GameObject CameraPupilClose;
    public GameObject CameraEye;
    public GameObject CameraTracking;
    public RectTransform Highlight;
    public RectTransform Pupill;
    private float TouchStemp;
    private float ZoomStemp;

    [Header("카메라 스위치")]
    public bool CameraJoystickOn = false; //카메라 조이스틱 사용 여부
    public bool CameraUsing = false; //카메라 사용여부
    public bool CameraZoomOn = false;
    public bool ClickCameraMode = false;

    private Vector3 TouchStart; //터치 좌표 입력
    private float TouchesPrevPosDifference; //카메라 줌 터치 이전 거리
    private float TouchesCurrPosDifference; //카메라 줌 터치 현재 거리
    private float ZoomModifier; //카메라 줌 수정
    private Vector2 FirstTouchPrepos; //카메라 줌 첫 터치 좌표
    private Vector2 SecondTouchPrepos; //카메라 줌 두번째 터치 좌표
    private Vector3 StartTouchPos;

    [Header("사운드")]
    public AudioClip CameraModeAudio;
    public AudioClip BootingOnAudio;
    public AudioClip BootingOffAudio;

    //기함모드로 초기화(워프 상태의 기함으로 카메라가 옮겨졌을 때)
    public void CameraInitialize()
    {
        CameraJoystickOn = false;
        CameraImage.raycastTarget = false;
        CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 0.5f);
        CameraUI.GetComponent<Animator>().SetBool("Online, Camera", false);
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
        CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
    }

    //카메라 전환
    public void CameraChangeClick()
    {
        if (CameraJoystickOn) //기함 중심으로 전환
        {
            UIControlSystem.FlagshipEnabled = false;
            UIControlSystem.FollowShipEnabled = false;
            CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 0.5f);
            CameraUI.GetComponent<Animator>().SetBool("Online, Camera", false);
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
            CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        }
        if (!CameraJoystickOn) //카메라 조종으로 전환
        {
            UIControlSystem.FlagshipEnabled = false;
            UIControlSystem.FollowShipEnabled = false;
            CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 1);
            CameraUI.GetComponent<Animator>().SetBool("Online, Camera", true);
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
            CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
            MainCameraMove.position = ShipManager.instance.SelectedFlagShip[0].transform.position;
            CVCamera.Follow = MainCameraMove;
        }
        CameraJoystickOn = !CameraJoystickOn;
    }
    public void CameraChangeDown()
    {
        ClickCameraMode = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CameraModeAudio);
        CameraUI.GetComponent<Animator>().SetBool("Click, Camera", true);
    }
    public void CameraChangeUp()
    {
        if (ClickCameraMode == true)
            CameraUI.GetComponent<Animator>().SetBool("Click, Camera", false);
        ClickCameraMode = false;
    }
    public void CameraChangeEnter()
    {
        if (ClickCameraMode == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CameraModeAudio);
            CameraUI.GetComponent<Animator>().SetBool("Click, Camera", true);
        }
    }
    public void CameraChangeExit()
    {
        if (ClickCameraMode == true)
            CameraUI.GetComponent<Animator>().SetBool("Click, Camera", false);
    }

    //우주맵 카메라 작동
    IEnumerator TurnCameraMap()
    {
        Time.timeScale = 0;
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 1);
        yield return new WaitForSecondsRealtime(0.2f);
        if (MultiFlagshipSystem.FlagshipNumber == 1)
        {
            CVCamera.Follow = UniverseMapSystem.Player1.transform;
            MapCameraMove.position = UniverseMapSystem.Player1.position;
        }
        else if (MultiFlagshipSystem.FlagshipNumber == 2)
        {
            CVCamera.Follow = UniverseMapSystem.Player2.transform;
            MapCameraMove.position = UniverseMapSystem.Player2.position;
        }
        else if (MultiFlagshipSystem.FlagshipNumber == 3)
        {
            CVCamera.Follow = UniverseMapSystem.Player3.transform;
            MapCameraMove.position = UniverseMapSystem.Player3.position;
        }
        else if (MultiFlagshipSystem.FlagshipNumber == 4)
        {
            CVCamera.Follow = UniverseMapSystem.Player4.transform;
            MapCameraMove.position = UniverseMapSystem.Player4.position;
        }
        else if (MultiFlagshipSystem.FlagshipNumber == 5)
        {
            CVCamera.Follow = UniverseMapSystem.Player5.transform;
            MapCameraMove.position = UniverseMapSystem.Player5.position;
        }
        yield return new WaitForSecondsRealtime(0.05f);
        CVCamera.Follow = MapCameraMove.transform;
        BattleZoom = CVCamera.m_Lens.OrthographicSize;
        CVCamera.m_Lens.OrthographicSize = UniverseMapZoom;
    }

    //우주맵 카메라 종료
    void TurnOffCameraMap()
    {
        if (CameraJoystickOn == true)
            CVCamera.Follow = MainCameraMove;
        else
            CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;

        UniverseMapZoom = CVCamera.m_Lens.OrthographicSize;
        CVCamera.m_Lens.OrthographicSize = BattleZoom;
        Time.timeScale = 1;
    }

    //함대 메뉴 카메라 작동
    public IEnumerator TurnCameraFleetMenu()
    {
        CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        BattleZoom = CVCamera.m_Lens.OrthographicSize;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
        yield return new WaitForSecondsRealtime(0.2f);
        CVCamera.m_Lens.OrthographicSize = 15;
        CVCamera.Follow = ShipManager.instance.FleetShipList[0].transform;

        if (MainMenuButtonSystem.FleetMenuMode == true) //함대 장비 메뉴 부팅 출력
        {
            yield return new WaitForSecondsRealtime(0.35f);
            CameraPrefab.GetComponent<Animator>().SetBool("Fleet online, Camera", true);
            yield return new WaitForSecondsRealtime(0.67f);
            CameraPrefab.GetComponent<Animator>().SetBool("Fleet online, Camera", false);
            CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
            yield return new WaitForSecondsRealtime(0.55f);
        }
    }

    //함대 메뉴 카메라 종료
    public void TurnOffCameraFleetMenu()
    {
        if (CameraJoystickOn == true)
            CVCamera.Follow = MainCameraMove;
        else
            CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;

        MainCamera.rect = new Rect(0, 0, 1.0f, 1.0f);
        CVCamera.m_Lens.OrthographicSize = BattleZoom;
    }

    //메뉴 부팅 애니메이션
    public IEnumerator BootingAnimation()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", BootingOnAudio);
        if (MainMenuButtonSystem.FleetMenuMode == true) //함대 장비 메뉴 부팅 출력
        {
            UniverseMapSystem.WordPrintSystem.UCCISBootingPrint(2);
        }
        else if (MainMenuButtonSystem.FleetFormationMenuMode == true) //함대 배열 메뉴 부팅 출력
        {
            UniverseMapSystem.WordPrintSystem.UCCISBootingPrint(3);
        }
        else if (MainMenuButtonSystem.FlagshipMenuMode == true) //기함 관리 메뉴 부팅 출력
        {
            UniverseMapSystem.WordPrintSystem.UCCISBootingPrint(4);
        }
        else if (MainMenuButtonSystem.LabMenuMode == true) //연구 메뉴 부팅 출력
        {
            UniverseMapSystem.WordPrintSystem.UCCISBootingPrint(5);
        }

        UIControlSystem.MenuUIImage.raycastTarget = false;
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 1);

        yield return new WaitForSecondsRealtime(0.5f);
        //화면 전환이 시작된다.
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().Direction = 0;
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;
        MainMenuButtonSystem.MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 0);

        if (MainMenuButtonSystem.FleetMenuMode == true) //함대 장비 메뉴 재화 정렬
        {
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 3);
        }
        else if (MainMenuButtonSystem.FleetFormationMenuMode == true) //함대 배열 메뉴 재화 정렬
        {
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 2);
        }
        else if (MainMenuButtonSystem.FlagshipMenuMode == true) //기함 관리 메뉴 재화 정렬
        {
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 3);
        }
        else if (MainMenuButtonSystem.LabMenuMode == true) //연구 메뉴 재화 정렬
        {
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 2);
        }

        yield return new WaitForSecondsRealtime(0.05f);
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        UniverseMapSystem.WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 0);
        UIControlSystem.MenuUI.GetComponent<Animator>().SetBool("Menu booting, Main menu", true);
        yield return new WaitForSecondsRealtime(1.25f);
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
    }

    //메뉴 나가기 애니메이션
    public IEnumerator ExitingAnimation()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", BootingOffAudio);
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 2);
        UniverseMapSystem.WordPrintSystem.UCCISExitingPrint();
        yield return new WaitForSecondsRealtime(0.25f);
        UIControlSystem.MenuUIImage.raycastTarget = true;
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().Direction = 1;
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        UniverseMapSystem.WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 2);
        MainMenuButtonSystem.MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 1);
        MainMenuButtonSystem.MenuListBack();
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 2);
        yield return new WaitForSecondsRealtime(0.35f);
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 0);
        CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
        yield return new WaitForSecondsRealtime(1.25f);
        UniverseMapSystem.MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = false;
        UniverseMapSystem.WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 0);
    }

    //우주맵 종료시 카메라 댐핑 복원
    public void TurnCameraDamping()
    {
        if (CameraJoystickOn == false)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true || ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == true
                || ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == true)
            {
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
            }
            else
            {
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
                CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
            }
        }
    }

    //우주맵 카메라 조작
    public void UniverseMapOnline()
    {
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
        StartCoroutine(TurnCameraMap());
    }
    public void UniverseMapOffline()
    {
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
        TurnOffCameraMap();
    }

    void Awake()
    {
        DataSaveManager = FindObjectOfType<DataSaveManager>();
        CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 0.5f);

        MaxMapX = MapSizeImage.transform.position.x + MapSizeImage.bounds.size.x;
        MinMapX = MapSizeImage.transform.position.x - MapSizeImage.bounds.size.x;
        MaxMapY = MapSizeImage.transform.position.y + MapSizeImage.bounds.size.y;
        MinMapY = MapSizeImage.transform.position.y - MapSizeImage.bounds.size.y;
    }

    void Update()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (CameraJoystickOn == true && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false && RTSControlSystem.ShipSelected == false && MainMenuButtonSystem.MenuMode == false) //함대전 카메라 조작
            {
                if (UniverseMapSystem.UniverseMapMode == false)
                {
                    if (Input.touchCount == 1 && CameraZoomUsing == 0) //카메라 화면 이동
                    {
                        CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 1.5f);
                        Touch touch = Input.GetTouch(0);

                        if (Input.GetMouseButtonDown(0))
                        {
                            CameraUI.GetComponent<Animator>().SetBool("Camera Move, Camera", true);
                            StartTouchPos = MainCameraMove.position;
                            TouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            TouchStemp = 0;
                        }
                        if (touch.phase == TouchPhase.Moved)
                        {
                            if (TouchStemp == 0)
                            {
                                TouchStemp += Time.deltaTime;
                                Highlight.anchoredPosition = new Vector2(2.17f, 8.24f);
                                Pupill.anchoredPosition = new Vector2(0, 10);
                            }
                            Vector3 Direction = TouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Vector3 CurTouchPos = MainCameraMove.position;
                            Camera.main.transform.position += new Vector3(Direction.x, Direction.y, 0);
                            MainCameraMove.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

                            if (Vector3.Distance(StartTouchPos, CurTouchPos) > 0.001f) //첫 터치거리와 현재 거리가 서로 차이가 발생할 경우, 스와이프로 간주
                                CameraUsing = true;

                            //카메라 눈동자의 손가락 추적
                            Vector3 dir = (TouchStart - CameraTracking.transform.position).normalized;
                            CameraTracking.transform.up = Vector3.Lerp(CameraTracking.transform.up, dir, 1);
                            Quaternion rotation = CameraTracking.transform.rotation;
                            Vector3 euler = rotation.eulerAngles;
                            CameraEye.transform.eulerAngles = new Vector3(0, 0, euler.z);
                        }
                        if (Input.GetMouseButtonUp(0)) //버튼을 땠을 때, 카메라 눈동차 초기화
                        {
                            CameraTracking.transform.rotation = Quaternion.Euler(0, 0, 0);
                            CameraEye.transform.rotation = Quaternion.Euler(0, 0, 0);
                            Highlight.anchoredPosition = new Vector2(0, 0);
                            Pupill.anchoredPosition = new Vector2(0, 0);
                        }
                    }
                }
            }
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false && UniverseMapSystem.UniverseMapMode == false)
            {
                if (Input.touchCount == 2) //카메라 줌
                {
                    CameraUI.GetComponent<Animator>().SetFloat("Roll, Camera", 1.5f);
                    Touch FirstTouch = Input.GetTouch(0);
                    Touch SecondTouch = Input.GetTouch(1);

                    if (Input.GetMouseButton(0))
                    {
                        CameraZoomOn = true;
                        CameraZoomUsing = 2;
                        ZoomStemp = 0;
                    }

                    FirstTouchPrepos = FirstTouch.position - FirstTouch.deltaPosition;
                    SecondTouchPrepos = SecondTouch.position - SecondTouch.deltaPosition;

                    TouchesPrevPosDifference = (FirstTouchPrepos - SecondTouchPrepos).magnitude;
                    TouchesCurrPosDifference = (FirstTouch.position - SecondTouch.position).magnitude;

                    if (CVCamera.m_Lens.OrthographicSize < 20)
                        ZoomModifier = (FirstTouch.deltaPosition - SecondTouch.deltaPosition).magnitude * ZoomModifierSpeed;
                    else if (CVCamera.m_Lens.OrthographicSize >= 20 && CVCamera.m_Lens.OrthographicSize < 40)
                        ZoomModifier = (FirstTouch.deltaPosition - SecondTouch.deltaPosition).magnitude * ZoomModifierSpeed * 2;
                    else if (CVCamera.m_Lens.OrthographicSize >= 40 && CVCamera.m_Lens.OrthographicSize < 60)
                        ZoomModifier = (FirstTouch.deltaPosition - SecondTouch.deltaPosition).magnitude * ZoomModifierSpeed * 4;
                    else if (CVCamera.m_Lens.OrthographicSize >= 60 && CVCamera.m_Lens.OrthographicSize <= ZoomMaxSize)
                        ZoomModifier = (FirstTouch.deltaPosition - SecondTouch.deltaPosition).magnitude * ZoomModifierSpeed * 8;

                    if (TouchesPrevPosDifference > TouchesCurrPosDifference)
                    {
                        CVCamera.m_Lens.OrthographicSize += ZoomModifier;
                        CameraEye.transform.localScale = new Vector2(CameraEye.transform.localScale.x - 0.05f, CameraEye.transform.localScale.y - 0.05f);
                        CameraPupilClose.transform.localScale = new Vector2(CameraEye.transform.localScale.x - 0.05f, CameraEye.transform.localScale.y - 0.05f);
                    }
                    if (TouchesPrevPosDifference < TouchesCurrPosDifference)
                    {
                        CVCamera.m_Lens.OrthographicSize -= ZoomModifier;
                        CameraEye.transform.localScale = new Vector2(CameraEye.transform.localScale.x + 0.05f, CameraEye.transform.localScale.y + 0.05f);
                        CameraPupilClose.transform.localScale = new Vector2(CameraEye.transform.localScale.x + 0.05f, CameraEye.transform.localScale.y + 0.05f);
                    }

                    if (FirstTouch.phase == TouchPhase.Ended)
                        CameraZoomUsing--;
                    if (SecondTouch.phase == TouchPhase.Ended)
                        CameraZoomUsing--;
                }

                if (Input.touchCount == 1) //카메라 줌이 종료시, CameraZoomUsing = 0 으로 처리
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        CameraZoomUsing = 0;
                        CameraEye.transform.localScale = new Vector2(1, 1);
                        CameraPupilClose.transform.localScale = new Vector2(1, 1);
                        CameraUI.GetComponent<Animator>().SetBool("Camera Move, Camera", false);
                    }
                }

                CVCamera.m_Lens.OrthographicSize = Mathf.Clamp(CVCamera.m_Lens.OrthographicSize, ZoomMinSize, ZoomMaxSize); //카메라 줌 크기 제한

                Vector2 Scale = CameraEye.transform.localScale; //카메라 눈동자 크기 제한
                Scale.x = Mathf.Clamp(Scale.x, 0.5f, 1.5f);
                Scale.y = Mathf.Clamp(Scale.y, 0.5f, 1.5f);
                CameraEye.transform.localScale = Scale;
                CameraPupilClose.transform.localScale = Scale;
            }

            if (CameraZoomUsing == 0)
            {
                if (ZoomStemp == 0)
                {
                    ZoomStemp += Time.deltaTime;
                    CameraEye.transform.localScale = new Vector2(1, 1);
                    CameraPupilClose.transform.localScale = new Vector2(1, 1);

                    CameraTracking.transform.rotation = Quaternion.Euler(0, 0, 0);
                    CameraEye.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Highlight.anchoredPosition = new Vector2(0, 0);
                    Pupill.anchoredPosition = new Vector2(0, 0);
                }
            }

            //우주맵 카메라 조작
            if (UniverseMapSystem.UniverseMapMode == true || MainMenuButtonSystem.FleetMenuMode == true)
            {
                if (Input.touchCount == 1) //카메라 화면 이동
                {
                    Touch touch = Input.GetTouch(0);

                    if (UniverseMapSystem.UniverseMapMode == true)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartTouchPos = MapCameraMove.position;
                            TouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        }
                        if (touch.phase == TouchPhase.Moved)
                        {
                            Vector3 Direction = TouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Vector3 CurTouchPos = MapCameraMove.position;
                            Camera.main.transform.position += new Vector3(Direction.x, Direction.y, 0);
                            MapCameraMove.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

                            if (Vector3.Distance(StartTouchPos, CurTouchPos) > 0.001f) //첫 터치거리와 현재 거리가 서로 차이가 발생할 경우, 스와이프로 간주
                                CameraUsing = true;
                        }
                    }
                }

                if (Input.touchCount == 2) //카메라 줌
                {
                    Touch FirstTouch = Input.GetTouch(0);
                    Touch SecondTouch = Input.GetTouch(1);

                    FirstTouchPrepos = FirstTouch.position - FirstTouch.deltaPosition;
                    SecondTouchPrepos = SecondTouch.position - SecondTouch.deltaPosition;

                    TouchesPrevPosDifference = (FirstTouchPrepos - SecondTouchPrepos).magnitude;
                    TouchesCurrPosDifference = (FirstTouch.position - SecondTouch.position).magnitude;

                    if (CVCamera.m_Lens.OrthographicSize < 20)
                        ZoomModifier = (FirstTouch.deltaPosition - SecondTouch.deltaPosition).magnitude * ZoomModifierSpeed;

                    if (TouchesPrevPosDifference > TouchesCurrPosDifference)
                        CVCamera.m_Lens.OrthographicSize += ZoomModifier;
                    if (TouchesPrevPosDifference < TouchesCurrPosDifference)
                        CVCamera.m_Lens.OrthographicSize -= ZoomModifier;
                }

                if (UniverseMapSystem.UniverseMapMode == true)
                {
                    IconActive();
                    MapCameraMove.transform.position = ClampCamera(MapCameraMove.transform.position); //카메라 이동 범위 제한
                    CVCamera.m_Lens.OrthographicSize = Mathf.Clamp(CVCamera.m_Lens.OrthographicSize, MapZoomMinSize, MapZoomMaxSize); //카메라 줌 크기 제한
                }
                if (MainMenuButtonSystem.FleetMenuMode == true)
                {
                    CVCamera.m_Lens.OrthographicSize = Mathf.Clamp(CVCamera.m_Lens.OrthographicSize, FleetMenuZoomMinSize, FleetMenuZoomMaxSize);
                }
            }
        }
    }

    //카메라 이동 범위 제한
    private Vector3 ClampCamera(Vector3 Target)
    {
        float CamHeight = CVCamera.m_Lens.OrthographicSize;
        float CamWidth = CVCamera.m_Lens.OrthographicSize * CVCamera.m_Lens.Aspect;

        float MaxX = MaxMapX - CamWidth;
        float MinX = MinMapX + CamWidth;
        float MaxY = MaxMapY - CamHeight;
        float MinY = MinMapY + CamHeight;

        float NewX = Mathf.Clamp(Target.x, MinX, MaxX);
        float NewY = Mathf.Clamp(Target.y, MinY, MaxY);

        return new Vector3(NewX, NewY, Target.z);
    }

    //우주맵 아이콘
    void IconActive()
    {
        if (CVCamera.m_Lens.OrthographicSize < 7)
        {
            UniverseMapSystem.ToropioStarOrbitPrefab.SetActive(true);
            UniverseMapSystem.RoroSystemOrbitPrefab.SetActive(true);
            UniverseMapSystem.SarisiStarOrbitPrefab.SetActive(true);
            UniverseMapSystem.GarixStarOrbitPrefab.SetActive(true);
            UniverseMapSystem.OctoKrasisPatoroSystemOrbitPrefab.SetActive(true);
            UniverseMapSystem.DeltaD31_402054SystemOrbitPrefab.SetActive(true);
            UniverseMapSystem.DeltaD31_4AStarOrbitPrefab.SetActive(true);
            UniverseMapSystem.DeltaD31_4BStarOrbitPrefab.SetActive(true);
            UniverseMapSystem.JeratoO95_99024SystemOrbitPrefab.SetActive(true);
            UniverseMapSystem.JeratoO95_7OrbitPrefab.SetActive(true);
            UniverseMapSystem.JeratoO95_14OrbitPrefab.SetActive(true);

            UniverseMapSystem.SatariusGlessiaPrefab.SetActive(true);
            UniverseMapSystem.AposisPrefab.SetActive(true);
            UniverseMapSystem.ToronoPrefab.SetActive(true);
            UniverseMapSystem.Plopa2Prefab.SetActive(true);
            UniverseMapSystem.Vedes4Prefab.SetActive(true);
            UniverseMapSystem.AronPeriPrefab.SetActive(true);
            UniverseMapSystem.Papatus2Prefab.SetActive(true);
            UniverseMapSystem.Papatus3Prefab.SetActive(true);
            UniverseMapSystem.KyepotorosPrefab.SetActive(true);
            UniverseMapSystem.TratosPrefab.SetActive(true);
            UniverseMapSystem.OclasisPrefab.SetActive(true);
            UniverseMapSystem.DeriousHeriPrefab.SetActive(true);
            UniverseMapSystem.VeltrorexyPrefab.SetActive(true);
            UniverseMapSystem.ErixJeoqetaPrefab.SetActive(true);
            UniverseMapSystem.QeepoPrefab.SetActive(true);
            UniverseMapSystem.CrownYoserePrefab.SetActive(true);
            UniverseMapSystem.OrosPrefab.SetActive(true);
            UniverseMapSystem.JapetAgronePrefab.SetActive(true);
            UniverseMapSystem.Xacro042351Prefab.SetActive(true);
            UniverseMapSystem.DeltaD31_2208Prefab.SetActive(true);
            UniverseMapSystem.DeltaD31_9523Prefab.SetActive(true);
            UniverseMapSystem.DeltaD31_12721Prefab.SetActive(true);
            UniverseMapSystem.JeratoO95_1125Prefab.SetActive(true);
            UniverseMapSystem.JeratoO95_2252Prefab.SetActive(true);
            UniverseMapSystem.JeratoO95_8510Prefab.SetActive(true);
        }
        else
        {
            UniverseMapSystem.ToropioStarOrbitPrefab.SetActive(false);
            UniverseMapSystem.RoroSystemOrbitPrefab.SetActive(false);
            UniverseMapSystem.SarisiStarOrbitPrefab.SetActive(false);
            UniverseMapSystem.GarixStarOrbitPrefab.SetActive(false);
            UniverseMapSystem.OctoKrasisPatoroSystemOrbitPrefab.SetActive(false);
            UniverseMapSystem.DeltaD31_402054SystemOrbitPrefab.SetActive(false);
            UniverseMapSystem.DeltaD31_4AStarOrbitPrefab.SetActive(false);
            UniverseMapSystem.DeltaD31_4BStarOrbitPrefab.SetActive(false);
            UniverseMapSystem.JeratoO95_99024SystemOrbitPrefab.SetActive(false);
            UniverseMapSystem.JeratoO95_7OrbitPrefab.SetActive(false);
            UniverseMapSystem.JeratoO95_14OrbitPrefab.SetActive(false);

            if (UniverseMapSystem.AreaNumber != 1001)
                UniverseMapSystem.SatariusGlessiaPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1002)
                UniverseMapSystem.AposisPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1003)
                UniverseMapSystem.ToronoPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1004)
                UniverseMapSystem.Plopa2Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1005)
                UniverseMapSystem.Vedes4Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1006)
                UniverseMapSystem.AronPeriPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1007)
                UniverseMapSystem.Papatus2Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1008)
                UniverseMapSystem.Papatus3Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1009)
                UniverseMapSystem.KyepotorosPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1010)
                UniverseMapSystem.TratosPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1011)
                UniverseMapSystem.OclasisPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1012)
                UniverseMapSystem.DeriousHeriPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1013)
                UniverseMapSystem.VeltrorexyPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1014)
                UniverseMapSystem.ErixJeoqetaPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1015)
                UniverseMapSystem.QeepoPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1016)
                UniverseMapSystem.CrownYoserePrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1017)
                UniverseMapSystem.OrosPrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1018)
                UniverseMapSystem.JapetAgronePrefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1019)
                UniverseMapSystem.Xacro042351Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1020)
                UniverseMapSystem.DeltaD31_2208Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1021)
                UniverseMapSystem.DeltaD31_9523Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1022)
                UniverseMapSystem.DeltaD31_12721Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1023)
                UniverseMapSystem.JeratoO95_1125Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1024)
                UniverseMapSystem.JeratoO95_2252Prefab.SetActive(false);
            if (UniverseMapSystem.AreaNumber != 1025)
                UniverseMapSystem.JeratoO95_8510Prefab.SetActive(false);
        }
    }
}