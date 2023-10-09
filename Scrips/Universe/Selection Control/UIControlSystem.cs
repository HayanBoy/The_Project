using UnityEngine.UI;
using Cinemachine;
using UnityEngine;
using System.Collections;

public class UIControlSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public RTSControlSystem RTSControlSystem;
    public FlagshipAttackSkill FlagshipAttackSkill;

    [Header("�Դ� ��� UI")]
    public GameObject MenuUI;
    public GameObject UniverseFrame;
    public GameObject ShipModeUI;
    public GameObject BehaviorModeUI;
    public GameObject SelectButtenUI;
    public GameObject UniverseMapUI;
    private bool ClickShipMode;
    private bool ClickBehaviorMode;
    private bool ClickSelectMode;

    [Header("�Դ��� UI ��ư")]
    public Image MenuUIImage;
    public Image ShipModeUIImage;
    public Image BehaviorModeUIImage;
    public Image SelectButtenImage;

    [Header("���� �̵�")]
    public bool WarpActive = false; //�Դ밡 ������ �����ߴ����� ���� ����

    [Header("ī�޶�")]
    public CinemachineVirtualCamera CVCamera;

    [Header("�Դ� ��� ����ġ")]
    public bool FlagShipBehaviorMode = true; //���� �ൿ ���. true : �̵����, false : ���� �� �������

    Coroutine behaviorTurnOff;

    [Header("�Դ� �ൿ ����ġ")]
    public bool FlagshipEnabled = false; //��ġ�� ���� Ȱ��ȭ �� ���Ŀ� �ൿ ���� �����ϵ��� ���� ����ġ(���� ����)
    public bool FollowShipEnabled = false; //��ġ�� ���� Ȱ��ȭ �� ���Ŀ� �ൿ ���� �����ϵ��� ���� ����ġ(�Ҽ� �Լ� ����)
    public bool BehaivorEnabled = false; //�ൿ ��� UI��ư�� �������� �Լ��� ������ �̵����� �ʵ��� ���� ����ġ
    public bool FlagshipTargetReady = false; //����� ���õ� ���Ŀ� �߻��� �� �ֵ��� ����
    public bool RTSSelectEnabled = false; //RTS�� ������ �����ϵ��� ���� ����ġ
    private Vector3 Formation; //�Դ��� ��ȯ��, ������ ��������

    [Header("����")]
    public AudioClip ShipControlModeAudio;
    public AudioClip ShipBehaviorModeAudio;
    public AudioClip ShipAttackModeAudio;
    public AudioClip UIbuttonAudio;

    void Start()
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", true);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 1);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Move Roll, Behavior Butten", 1);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Attack Roll, Behavior Butten", 1);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);
        if (SelectButtenUI.GetComponent<Animator>().GetBool("Turn off, Select Butten") == true)
            SelectButtenUI.GetComponent<Animator>().SetBool("Turn off, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Active, Select Butten", 1);
    }

    public void TransformDestination()
    {
        Debug.Log(Formation);
        RTSControlSystem.FormationTransfer(Formation);
    }

    //�Լ����� ��� UI
    public void ShipControlModeClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == false) //���Ը��
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode = true;
                    ShipControlModeOn();
                }
                else //��� �Լ����
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode = false;
                    ShipControlModeOff();
                }
            }
            else //���� ���� �Դ�� �迭 ��带 ����� �� ����.
            {
                Debug.Log("���� ���Դϴ�. �Դ� ��带 ��� �Ұ����մϴ�.");
            }
        }
    }
    public void ShipControlModeOn()
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", true);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 1);
        ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().MoveOrderStemp = 0;
        FlagshipEnabled = false;
        RTSControlSystem.FlagShipSelectionMode();

        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);
        SelectDeactive(); //���� UI �ִϸ��̼�
    }
    public void ShipControlModeOff()
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", false);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 3);
        Formation = ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().DestinationArea; //�̵��߿� �Դ���� ���԰� ���������� ���������� ���� �̵�

        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == true)
            ShipSelectClick();
        RTSSelectEnabled = false;
        FollowShipEnabled = false;
        RTSControlSystem.SelectShipMode();

        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 10);
        SelectActive(); //���� UI �ִϸ��̼�
    }
    public void ShipControlModeOffWarp(GameObject Flagship) //������ ������ �� �Դ��带 ����
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", true);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 1);
        FlagshipEnabled = false;
        RTSControlSystem.FlagShipSelectionModeWarp(Flagship);

        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);
        SelectDeactive(); //���� UI �ִϸ��̼�
    }
    public void ShipControlModeDown()
    {
        FlagshipEnabled = false;
        FollowShipEnabled = false;
        ClickShipMode = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipControlModeAudio);
        ShipModeUI.GetComponent<Animator>().SetBool("Click, Ship Mode Butten", true);
    }
    public void ShipControlModeUp()
    {
        if (ClickShipMode == true)
            ShipModeUI.GetComponent<Animator>().SetBool("Click, Ship Mode Butten", false);
        ClickShipMode = false;
    }
    public void ShipControlModeEnter()
    {
        if (ClickShipMode == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipControlModeAudio);
            ShipModeUI.GetComponent<Animator>().SetBool("Click, Ship Mode Butten", true);
        }
    }
    public void ShipControlModeExit()
    {
        if (ClickShipMode == true)
            ShipModeUI.GetComponent<Animator>().SetBool("Click, Ship Mode Butten", false);
    }

    //���� UI �ִϸ��̼�
    public void SelectActive() //��� ���
    {
        if (behaviorTurnOff != null)
            StopCoroutine(behaviorTurnOff);
        SelectButtenImage.raycastTarget = false;
        if (FlagShipBehaviorMode == true)
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off, Behavior Butten", 1);
        else
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off2, Behavior Butten", 1);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 10);
        if (SelectButtenUI.GetComponent<Animator>().GetBool("Turn off, Select Butten") == true)
            SelectButtenUI.GetComponent<Animator>().SetBool("Turn off, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Active, Select Butten", 2);
    }
    public void SelectDeactive() //���� ���
    {
        SelectButtenImage.raycastTarget = true;
        if (FlagShipBehaviorMode == true)
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off, Behavior Butten", 2);
        else
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off2, Behavior Butten", 2);
        if (SelectButtenUI.GetComponent<Animator>().GetBool("Turn off, Select Butten") == true)
            SelectButtenUI.GetComponent<Animator>().SetBool("Turn off, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Active, Select Butten", 1);
        behaviorTurnOff = StartCoroutine(BehaviorTurnOff());
    }
    IEnumerator BehaviorTurnOff() //���� ���� ���ư� �� UI �ʱ�ȭ
    {
        yield return new WaitForSeconds(0.25f);
        if (FlagShipBehaviorMode == true)
        {
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off, Behavior Butten", 0);
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 0);
        }
        else
        {
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Turn off2, Behavior Butten", 0);
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 2);
        }
    }

    //�ൿ ��� UI
    public void FlagShipBehaviorClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            RTSSelectEnabled = false;

            //���Ը�忡�� ��ȯ
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            {
                if (FlagShipBehaviorMode) //true : �̵����, false : ���� �� �������
                {
                    BehaivorEnabled = false;
                    StartCoroutine(MoveModeActive());
                }
                else
                {
                    BehaivorEnabled = false;
                    StartCoroutine(AttackModeActive());
                }
                FlagShipBehaviorMode = !FlagShipBehaviorMode;
            }
        }
    }
    public void FlagShipBehaviorDown()
    {
        ClickBehaviorMode = true;
        if (FlagShipBehaviorMode == false)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipAttackModeAudio);
            BehaviorModeUI.GetComponent<Animator>().SetBool("Attack mode click, Behavior Butten", true);
        }
        else
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipBehaviorModeAudio);
            BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode click, Behavior Butten", true);
        }
    }
    public void FlagShipBehaviorUp()
    {
        if (ClickBehaviorMode == true)
        {
            if (FlagShipBehaviorMode == false)
                BehaviorModeUI.GetComponent<Animator>().SetBool("Attack mode click, Behavior Butten", false);
            else
                BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode click, Behavior Butten", false);
        }
        ClickBehaviorMode = false;
    }
    public void FlagShipBehaviorEnter()
    {
        if (ClickBehaviorMode == true)
        {
            if (FlagShipBehaviorMode == false)
            {
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipAttackModeAudio);
                BehaviorModeUI.GetComponent<Animator>().SetBool("Attack mode click, Behavior Butten", true);
            }
            else
            {
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ShipBehaviorModeAudio);
                BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode click, Behavior Butten", true);
            }
        }
    }
    public void FlagShipBehaviorExit()
    {
        if (ClickBehaviorMode == true)
        {
            if (FlagShipBehaviorMode == false)
                BehaviorModeUI.GetComponent<Animator>().SetBool("Attack mode click, Behavior Butten", false);
            else
                BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode click, Behavior Butten", false);
        }
    }

    //�Լ� �⵿ ��� ��ȯ �ִϸ��̼�
    IEnumerator MoveModeActive()
    {
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Move Roll, Behavior Butten", 0);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Attack Roll, Behavior Butten", 0);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 1);
        yield return new WaitForSeconds(0.5f);
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 2);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Move Roll, Behavior Butten", 1);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Attack Roll, Behavior Butten", 1);
    }
    //�Լ� ���� ��� ��ȯ �ִϸ��̼�
    IEnumerator AttackModeActive()
    {
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Move Roll, Behavior Butten", 0);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Attack Roll, Behavior Butten", 0);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 3);
        yield return new WaitForSeconds(0.5f);
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 0);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Move Roll, Behavior Butten", 1);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Attack Roll, Behavior Butten", 1);
    }

    //�Դ� �迭 ��� UI
    public void ShipSelectClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false) //�迭��� Ȱ��ȭ
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode = true;
                    ShipFormationSettingModeOn();
                }
                else //�迭��� ��Ȱ��ȭ(������ �迭�� �����Ѵ�.)
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode = false;
                    ShipFormationSettingModeOff();
                }
            }
            else //���� ���� �Դ�� �迭 ��带 ����� �� ����.
            {
                Debug.Log("���� ���Դϴ�. �迭 ��带 ��� �Ұ����մϴ�.");
            }
        }
    }

    //������ ������ �迭 ��带 �ҷ�����
    public void ShipFormationSettingModeOn()
    {
        BehaivorEnabled = false;
        SelectButtenUI.GetComponent<Animator>().SetBool("Turn off, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 3);
        SelectButtenUI.GetComponent<Animator>().SetBool("Using, Select Butten", true);
        if (FlagShipBehaviorMode == true)
            BehaviorModeUI.GetComponent<Animator>().SetBool("Turn off, Behavior Butten", true);
        else
            BehaviorModeUI.GetComponent<Animator>().SetBool("Turn off2, Behavior Butten", true);
        BehaviorModeUI.GetComponent<Animator>().SetFloat("Mode, Behavior Butten", 10);
        ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().SelectModeOnline();
    }
    public void ShipFormationSettingModeOff()
    {
        BehaivorEnabled = false;
        SelectButtenUI.GetComponent<Animator>().SetBool("Using, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);
        ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().SelectModeOffline();
        ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().WarpFormation.transform.position = ShipManager.instance.SelectedFlagShip[0].transform.position;
        ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().WarpFormation.transform.rotation = ShipManager.instance.SelectedFlagShip[0].transform.rotation;

        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().WarpFormation.GetComponent<WarpFormation>().GetFormationForNewShip(ShipManager.instance.SelectedFlagShip[0], i);
        }
    }
    public void ShipFormationSettingModeOffWarp(GameObject Flagship) //���� �� �ڵ����� �Դ���� �迭 ��带 ����
    {
        BehaivorEnabled = false;
        SelectButtenUI.GetComponent<Animator>().SetBool("Using, Select Butten", false);
        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);

        Flagship.GetComponent<FollowShipManager>().SelectModeOffline();
        Flagship.GetComponent<FollowShipManager>().WarpFormation.transform.position = Flagship.transform.position;
        Flagship.GetComponent<FollowShipManager>().WarpFormation.transform.rotation = Flagship.transform.rotation;

        for (int i = 0; i < Flagship.GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
            Flagship.GetComponent<FollowShipManager>().WarpFormation.GetComponent<WarpFormation>().GetFormationForNewShip(Flagship, i);
        }
    }
    public void ShipSelectDown()
    {
        FlagshipEnabled = false;
        ClickSelectMode = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UIbuttonAudio);
        SelectButtenUI.GetComponent<Animator>().SetBool("Click, Select Butten", true);
    }
    public void ShipSelectUp()
    {
        if (ClickSelectMode == true)
            SelectButtenUI.GetComponent<Animator>().SetBool("Click, Select Butten", false);
        ClickSelectMode = false;
    }
    public void ShipSelectEnter()
    {
        if (ClickSelectMode == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UIbuttonAudio);
            SelectButtenUI.GetComponent<Animator>().SetBool("Click, Select Butten", true);
        }
    }
    public void ShipSelectExit()
    {
        if (ClickSelectMode == true)
            SelectButtenUI.GetComponent<Animator>().SetBool("Click, Select Butten", false);
    }

    //���� ����(������ ������ ������ ����)
    public void WarpStart()
    {

    }

    //���� ���� ����
    public void WarpArrive()
    {
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
    }

    //���� �Ϸ�
    public void FlagShipWarpComplete()
    {
        //RTSControlSystem.WarpComplete();
        CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
    }

    void Update()
    {
        if (ShipManager.instance.SelectedFlagShip.Count != 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipMove == true)
            {
                BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode icon move, Behavior Butten", true);
                if (GetComponent<UniverseMapSystem>().UniverseMapMode == false)
                {
                    UniverseMapUI.GetComponent<Animator>().SetBool("Ship move, Universe Map Butten", true);
                    UniverseMapUI.GetComponent<Animator>().SetFloat("Star move, Universe Map Butten", 1);
                }
            }
            else
            {
                BehaviorModeUI.GetComponent<Animator>().SetBool("Move mode icon move, Behavior Butten", false);
                if (GetComponent<UniverseMapSystem>().UniverseMapMode == false)
                {
                    UniverseMapUI.GetComponent<Animator>().SetBool("Ship move, Universe Map Butten", false);
                    UniverseMapUI.GetComponent<Animator>().SetFloat("Star move, Universe Map Butten", 0);
                }
            }
        }
    }
}