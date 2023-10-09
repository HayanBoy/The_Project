using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RTSControlSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public UIControlSystem UIControlSystem;
    public FlagshipAttackSkill FlagshipAttackSkill;
    public CameraZoom CameraZoom;
    public UniverseMapSystem UniverseMapSystem;
    public CameraFollow CameraFollow;
    public MultiFlagshipSystem MultiFlagshipSystem;
    MoveVelocity moveVelocity;

    [Header("��ǥ")]
    [SerializeField] private Transform SelectionAreaTransform;
    Coroutine selectionAreaOn;
    public Vector3 StartPosition;
    private Collider2D SelectCollider2D;
    public int FormationRange;

    [Header("���̾�")]
    public LayerMask EnemiesShipLayer;
    public LayerMask ShipFormationLayer;

    [Header("��� ����ġ")]
    public bool ShipSelected = false; //����忡�� �Լ��� ���õ� ���¿��� ī�޶� �������� �ʴ´�.

    private bool Tapped = false; //���� �ѹ� �̷��������, ���� �� ����� ����
    private bool TapMoved = false; //�Լ� ���� ��, �̵� �׼��� ������ ���� ����ġ
    private int TapAccount; //���� Ƚ��. ���� �ð����� ���������� ���� ������ ����
    private float TapTime; //�������� ���� ���ѽð�
    private float StayPushTime; //������ �ð��� ���� ���ѽð�
    private bool DontAction = false; //�۾� �߿����� �Լ� ���� �Ұ�

    [Header("ī�޶� �� ����")]
    public int ShipNumber; //���õ� �Լ� ��ȣ
    public GameObject SelectShipsInCameraTop;
    public GameObject SelectShipsInCameraDown;

    [Header("��� ����Ʈ")]
    public GameObject ShipMoveClickUI; //�̵������ ���� ���� Ŭ�� ����Ʈ
    public GameObject ShipActionUI; //����忡�� ���õ� �Լ��� �̵��� �ڸ��� ǥ�����ִ� UI. �Լ��� ��ġ�� ����, ��ġ�� ��ǥ�� �����ȴ�.
    public GameObject ShipAttackUI; //����忡�� ���õ� �Լ��� ������ ǥ�����ִ� UI. �Լ��� ��ġ�� ����, ��ġ�� ��ǥ�� �����ȴ�.

    //���� ����, ������ ���� �Լ� ������ ������ ���� �� ��, ���� ù��° ������ �ڵ� ����
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
        Debug.Log("���� ��� Ȱ��ȭ, ���õ� ���� : " + ShipManager.instance.FlagShipList[0]);
    }
    public void FlagShipSelectionModeWarp(GameObject Flagship) //���� �� �ڵ����� �Լ����� ���� ���� ��ȯ
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

    //��� ��� ���ý�, ���õ� ������ ���
    public void SelectShipMode()
    {
        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipAccount; i++)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().FormationOn = false;
        }
        ShipManager.instance.SelectedShipList.Clear();
        Debug.Log("�Լ� ���� ��� Ȱ��ȭ");
    }

    //��ư�� ���� ������ ��ġ�� ��ȯ
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
        //���� ���
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            {
                if (UIControlSystem.FlagShipBehaviorMode == true && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && ShipSelected == false) //���� �̵�
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended) //��ġ�� ���
                        {
                            //Debug.Log("���� �̵�");
                            Vector3 MoveToPosition = GetMouseWorldPosition();
                            GameObject MoveOrder = Instantiate(ShipMoveClickUI, MoveToPosition, Quaternion.identity);
                            Destroy(MoveOrder, 1.5f);
                            ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
                            shipRTS.MoveTo(MoveToPosition);
                        }
                    }
                    if (Input.GetMouseButtonDown(1) && UIControlSystem.FlagshipEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false) //���콺 Ŭ������ ���
                    {
                        Vector3 MoveToPosition = GetMouseWorldPosition();
                        ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
                        shipRTS.MoveTo(MoveToPosition);
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //ī�޶� �̵��߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //ī�޶� �� ����߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (UIControlSystem.FlagShipBehaviorMode == false && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && ShipSelected == false) //���� ����
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                        {
                            StartPosition = GetMouseWorldPosition(); //������ �������� ����
                            StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);

                            Collider2D collider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, EnemiesShipLayer); //�� ���� ��� ����
                            if (collider2D != null)
                            {
                                collider2D.gameObject.transform.parent.GetComponent<EnemyShipNavigator>().TargetMark();
                                GameObject gameObject = collider2D.gameObject; //���õ� ��� �ݶ��̴��� ������Ʈ�� ��ȯ
                                ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().TargetEngage(gameObject); //������ �Դ뿡�� ������ ���� ���

                                //Debug.Log("���� ��� ����");
                            }
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //ī�޶� �̵��߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //ī�޶� �� ����߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == true && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true) //�迭 ���
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false && DontAction == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began) //�迭 ����
                        {
                            StartPosition = GetMouseWorldPosition(); //������ �������� ����
                            StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);
                            SelectCollider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, ShipFormationLayer);

                            if (SelectCollider2D != null)
                            {
                                ShipSelected = true;
                            }
                        }
                        if (touch.phase == TouchPhase.Moved) //�迭 ���ġ
                        {
                            if (Vector2.Distance(ShipManager.instance.SelectedFlagShip[0].transform.position, SelectCollider2D.gameObject.transform.position) < FormationRange)
                                SelectCollider2D.gameObject.transform.position = Vector2.MoveTowards(SelectCollider2D.gameObject.transform.position, GetMouseWorldPosition(), 1000 * Time.deltaTime);
                        }
                        if (touch.phase == TouchPhase.Ended)
                        {
                            ShipSelected = false;
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //ī�޶� �̵��߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //ī�޶� �� ����߿��� �Լ� �̵��� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    //���� �Ÿ� �̻� ����� �ǵ��ƿ���
                    if (SelectCollider2D != null)
                    {
                        if (Vector2.Distance(ShipManager.instance.SelectedFlagShip[0].transform.position, SelectCollider2D.gameObject.transform.position) >= FormationRange)
                            SelectCollider2D.gameObject.transform.position = Vector2.MoveTowards(SelectCollider2D.gameObject.transform.position, ShipManager.instance.SelectedFlagShip[0].transform.position, 100 * Time.deltaTime);
                    }

                    UIControlSystem.FlagshipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                    DontAction = false;
                }
                if (UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true) //��Ƽ ���� ����
                {
                    if (Input.touchCount == 1 && UIControlSystem.FlagshipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraUsing == false && CameraZoom.CameraZoomOn == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Ended)
                        {
                            //�� ���� ����
                            TapAccount++; //�� ������ �� ���� �߰�
                            TapTime = 0;
                            StayPushTime = 0;
                            Tapped = true; //�� ������ �˸�
                            StartPosition = GetMouseWorldPosition(); //������ �������� ����
                            StartPosition = new Vector2(StartPosition.x - (0.25f / 2), StartPosition.y + (0.25f / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 0.25f, StartPosition.y - 0.25f);

                            //���� ���� �������� ���� 1�� ����
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition);
                            foreach (Collider2D collider2D in collider2DArray)
                            {
                                ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
                                if (shipRTS != null && shipRTS.GetComponent<MoveVelocity>().FlagShip == true)
                                    ShipManager.instance.SelectedShipList.Add(shipRTS); //���õ� �Լ� ����Ʈ�� �߰�
                            }

                            //���Կ��� �������� �������� ���, �ش� �������� ����
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
                                    //Debug.Log("���� ��ȯ");
                                }
                            }
                        }
                    }

                    if (Tapped == true && TapTime < 0.5f) //�� �ѹ� �̷������ ��, �������� ���� ���ѽð� ����
                        TapTime += Time.deltaTime;
                    if (Tapped == true && TapTime >= 0.5f) //���ѽð��� ������ ���, ������ ��� �ʱ�ȭ
                    {
                        Tapped = false;
                        TapTime = 0;
                        TapAccount = 0;
                        ShipManager.instance.SelectedShipList.Clear();
                    }
                }
            }
            //��� ���
            else
            {
                //���õ� ���� �̵�
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false && UniverseMapSystem.UniverseMapEnabled == false && CameraFollow.MoveEnabled == true && DontAction == false) //�Լ� �̵�
                {
                    if (Input.touchCount == 1 && UIControlSystem.FollowShipEnabled == true && UIControlSystem.BehaivorEnabled == true && CameraZoom.CameraZoomOn == false)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Began) //���� ���� Ȱ��ȭ
                        {
                            if (ShipManager.instance.SelectedShipList.Count > 0) //���õ� �Լ��� ������ ���, �ش� ���õ� ��� ���� �̼������� ó��
                            {
                                foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                {
                                    shipRTS.SetSelectedVisible(false);
                                }
                                ShipManager.instance.SelectedShipList.Clear();
                            }

                            //�� ���� ����
                            TapAccount++; //�� ������ �� ���� �߰�
                            TapTime = 0;
                            StayPushTime = 0;
                            Tapped = true; //�� ������ �˸�
                            StartPosition = GetMouseWorldPosition(); //������ �������� ����
                            StartPosition = new Vector2(StartPosition.x - (0.25f / 2), StartPosition.y + (0.25f / 2));
                            Vector3 currentMousePosition = new Vector2(StartPosition.x + 0.25f, StartPosition.y - 0.25f);

                            //�������� �������� ���, �ش� ���� ������ �Լ� �����ϵ��� ���� ���� ����
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
                                selectionAreaOn = StartCoroutine(SelectionAreaOn()); //���� �� ���� �ִϸ��̼� Ȱ��ȭ

                                StartPosition = GetMouseWorldPosition();
                                StartPosition = new Vector2(StartPosition.x - (10 / 2), StartPosition.y + (10 / 2));
                                currentMousePosition = new Vector2(StartPosition.x + 10, StartPosition.y - 10); //�簢�� ���ù����� ���� ����, ��ġ�� ���� �߾����� ��ġ
                            }

                            //���� ���� �������� �� �Լ� ���� ����
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition);
                            foreach (Collider2D collider2D in collider2DArray)
                            {
                                ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
                                SelecteShipRouteLine selecteShipRouteLine = collider2D.GetComponent<SelecteShipRouteLine>(); //���� �Լ��� �̵���Ʈ ����
                                if (shipRTS != null)
                                {
                                    ShipNumber = shipRTS.ShipNumber;
                                    shipRTS.SetSelectedVisible(true); //���� UI Ȱ��ȭ
                                    ShipActionUI.SetActive(true); //�̵� ��Ʈ ������ Ȱ��ȭ
                                    ShipActionUI.GetComponent<SpriteRenderer>().enabled = false; //�̵� ��Ʈ ��������Ʈ Ȱ��ȭ(���� �������� �ʴ� ���¿����� ������ �ʴ´�.)
                                    ShipActionUI.transform.position = GetMouseWorldPosition(); //�̵� ��Ʈ �������� ��ġ�� ��ǥ�� �̵�
                                    selecteShipRouteLine.EndPosition = ShipActionUI; //�̵� ��Ʈ ������ ���� ��ǥ�� �ڵ� �����ϱ� ���� ShipActionUI�� �ν�
                                    ShipManager.instance.SelectedShipList.Add(shipRTS); //���õ� �Լ� ����Ʈ�� �߰�
                                    ShipSelected = true;
                                }
                            }
                        }

                        if (touch.phase == TouchPhase.Moved) //������ ���ֿ��� �����̱� ���� ��� ����
                        {
                            if (Vector2.Distance(ShipActionUI.transform.position, StartPosition) > 0.5f) //���������� ����� ������ ������ ����(���� �̼��� �����ӵ� ��� ������ ������ �����ϱ� ����)
                                TapMoved = true;
                            ShipActionUI.transform.position = GetMouseWorldPosition();

                            Collider2D collider2D = Physics2D.OverlapArea(ShipActionUI.transform.position, ShipActionUI.transform.position, EnemiesShipLayer); //������ ������ ���� ǥ�÷� ��ȯ
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

                        if (touch.phase == TouchPhase.Ended) //���ֿ��� ���
                        {
                            Vector3 MoveToPosition = GetMouseWorldPosition();

                            if (ShipActionUI.GetComponent<SpriteRenderer>().enabled == true) //�̵� ���
                            {
                                List<Vector3> TargetPositionList = GetPositionListAround(MoveToPosition, new float[] { 2.5f, 5f, 7.5f }, new int[] { 5, 10, 20 }); //�̵� ��, �Դ� ����
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

                                if (ShipManager.instance.SelectedShipList.Count > 0) //���õ� �Լ��� ������ ���, �ش� ���õ� ��� ���� �̼������� ó��
                                {
                                    foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                    {
                                        shipRTS.SetSelectedVisible(false);
                                    }
                                    ShipManager.instance.SelectedShipList.Clear();
                                }
                                //Debug.Log("�Լ� �̵�");
                            }
                            else if (ShipAttackUI.GetComponent<SpriteRenderer>().enabled == true) //���� ���
                            {
                                StartPosition = GetMouseWorldPosition(); //������ �������� ����
                                StartPosition = new Vector2(StartPosition.x - (2 / 2), StartPosition.y + (2 / 2));
                                Vector3 currentMousePosition = new Vector2(StartPosition.x + 2, StartPosition.y - 2);
                                Collider2D collider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, EnemiesShipLayer); //�� ���� ��� ����
                                if (collider2D != null)
                                {
                                    collider2D.gameObject.transform.parent.GetComponent<EnemyShipNavigator>().TargetMark();
                                    GameObject gameObject = collider2D.gameObject; //���õ� ��� �ݶ��̴��� ������Ʈ�� ��ȯ

                                    for (int i = 0; i < ShipManager.instance.SelectedShipList.Count; i++)
                                    {
                                        ShipManager.instance.SelectedShipList[i].GetComponent<MoveVelocity>().TargetEngagePersonal(gameObject); //���� ����
                                    }

                                    foreach (ShipRTS shipRTS in ShipManager.instance.SelectedShipList)
                                    {
                                        shipRTS.SetSelectedVisible(false);
                                    }
                                    ShipManager.instance.SelectedShipList.Clear();

                                    //Debug.Log("�Լ� ��� ����");
                                }
                            }

                            //���� �� ��� ������ ��� �ʱ�ȭ
                            StayPushTime = 0;
                            ShipSelected = false;
                            TapMoved = false;
                            ShipAttackUI.GetComponent<SpriteRenderer>().enabled = false;
                            ShipActionUI.GetComponent<SpriteRenderer>().enabled = false;
                            ShipActionUI.SetActive(false);
                        }
                    }
                    if (Input.touchCount == 1 && CameraZoom.CameraUsing == true) //ī�޶� �̵��߿��� RTS ���� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);
                        InitializeSelect();

                        if (touch.phase == TouchPhase.Ended)
                            CameraZoom.CameraUsing = false;
                    }
                    if (Input.touchCount <= 2 && CameraZoom.CameraZoomOn == true) //ī�޶� �� ����߿��� RTS ���� �Ұ�
                    {
                        Touch touch = Input.GetTouch(0);
                        InitializeSelect();

                        if (touch.phase == TouchPhase.Ended && CameraZoom.CameraZoomUsing == 0)
                            CameraZoom.CameraZoomOn = false;
                    }

                    if (Tapped == true && TapTime < 0.3f) //�� �ѹ� �̷������ ��, �������� ���� ���ѽð� ����
                        TapTime += Time.deltaTime;
                    if (Tapped == true && TapTime >= 0.3f) //���ѽð��� ������ ���, ������ ��� �ʱ�ȭ
                    {
                        Tapped = false;
                        TapTime = 0;
                        TapAccount = 0;
                    }
                    if (Tapped == true && StayPushTime < 0.3f)
                        StayPushTime += Time.deltaTime;
                    if (StayPushTime >= 0.28f && TapMoved == false && ShipManager.instance.SelectedShipList.Count > 0) //���ѽð��� ������ ���, ������ ��� �ʱ�ȭ�� �ش� �Լ��� ���� �Լ����� ����
                    {
                        StayPushTime = 0;
                        SelectSameShips(); //���� �Լ� ����
                    }

                    UIControlSystem.FollowShipEnabled = true;
                    UIControlSystem.BehaivorEnabled = true;
                }
            }
        }
    }

    //���� �� ���� �ִϸ��̼�
    IEnumerator SelectionAreaOn()
    {
        SelectionAreaTransform.gameObject.SetActive(true);
        SelectionAreaTransform.position = StartPosition;
        yield return new WaitForSeconds(1);
        SelectionAreaTransform.gameObject.SetActive(false);
    }

    //���� �Լ� ����
    void SelectSameShips()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(SelectShipsInCameraTop.transform.position, SelectShipsInCameraDown.transform.position);
        ShipManager.instance.SelectedShipList.Clear();
        //Debug.Log("���� �Լ� ����");

        foreach (Collider2D collider2D in collider2DArray)
        {
            ShipRTS shipRTS = collider2D.GetComponent<ShipRTS>();
            SelecteShipRouteLine selecteShipRouteLine = collider2D.GetComponent<SelecteShipRouteLine>();

            if (shipRTS != null)
            {
                if (ShipNumber == shipRTS.ShipNumber) //���� �Լ� ��ȣ���� Ȯ���� ��, ���� ��ȣ �Լ��� ��� �����Ѵ�.
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

    //RTS ���� �ʱ�ȭ
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

    //��ġ ���� ���
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

    //GetpositionListAround ��ȯ
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    //�̵� ��, �Դ���� �������κ��� ���������� ������������ �̵�
    public void FormationTransfer(Vector3 transform)
    {
        ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<ShipRTS>();
        shipRTS.WarpLocationGet(transform, ShipManager.instance.SelectedFlagShip[0].transform.rotation, ShipManager.instance.SelectedFlagShip[0]);
    }

    //��� �۾� ���̰ų� ��ġ�� �ϸ� �ȵǴ� ������ ������ ���, �Լ��� �̵��ϰų� ���� ����� ������ ���ϵ��� ����
    public void DoNotClickWhenTaskDown()
    {
        DontAction = true;
    }
    public void DoNotClickWhenTaskUp()
    {
        DontAction = false;
    }
}