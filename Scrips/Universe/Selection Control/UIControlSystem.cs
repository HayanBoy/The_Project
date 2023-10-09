using UnityEngine.UI;
using Cinemachine;
using UnityEngine;
using System.Collections;

public class UIControlSystem : MonoBehaviour
{
    [Header("스크립트")]
    public RTSControlSystem RTSControlSystem;
    public FlagshipAttackSkill FlagshipAttackSkill;

    [Header("함대 모드 UI")]
    public GameObject MenuUI;
    public GameObject UniverseFrame;
    public GameObject ShipModeUI;
    public GameObject BehaviorModeUI;
    public GameObject SelectButtenUI;
    public GameObject UniverseMapUI;
    private bool ClickShipMode;
    private bool ClickBehaviorMode;
    private bool ClickSelectMode;

    [Header("함대전 UI 버튼")]
    public Image MenuUIImage;
    public Image ShipModeUIImage;
    public Image BehaviorModeUIImage;
    public Image SelectButtenImage;

    [Header("기함 이동")]
    public bool WarpActive = false; //함대가 워프에 돌입했는지에 대한 여부

    [Header("카메라")]
    public CinemachineVirtualCamera CVCamera;

    [Header("함대 모드 스위치")]
    public bool FlagShipBehaviorMode = true; //기함 행동 모드. true : 이동모드, false : 공격 및 지원모드

    Coroutine behaviorTurnOff;

    [Header("함대 행동 스위치")]
    public bool FlagshipEnabled = false; //터치가 먼저 활성화 된 이후에 행동 개시 가능하도록 만든 스위치(기함 전용)
    public bool FollowShipEnabled = false; //터치가 먼저 활성화 된 이후에 행동 개시 가능하도록 만든 스위치(소속 함선 전용)
    public bool BehaivorEnabled = false; //행동 모드 UI버튼이 눌러져도 함선이 강제로 이동하지 않도록 만든 스위치
    public bool FlagshipTargetReady = false; //대상이 선택된 이후에 발사할 수 있도록 조취
    public bool RTSSelectEnabled = false; //RTS로 선택이 가능하도록 만든 스위치
    private Vector3 Formation; //함대모드 전환시, 기함의 도착지점

    [Header("사운드")]
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

    //함선유형 모드 UI
    public void ShipControlModeClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == false) //기함모드
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode = true;
                    ShipControlModeOn();
                }
                else //편대 함선모드
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode = false;
                    ShipControlModeOff();
                }
            }
            else //워프 중인 함대는 배열 모드를 사용할 수 없다.
            {
                Debug.Log("워프 중입니다. 함대 모드를 사용 불가능합니다.");
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
        SelectDeactive(); //선택 UI 애니메이션
    }
    public void ShipControlModeOff()
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", false);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 3);
        Formation = ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().DestinationArea; //이동중에 함대들이 기함과 독립적으로 도착지점에 따로 이동

        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == true)
            ShipSelectClick();
        RTSSelectEnabled = false;
        FollowShipEnabled = false;
        RTSControlSystem.SelectShipMode();

        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 10);
        SelectActive(); //선택 UI 애니메이션
    }
    public void ShipControlModeOffWarp(GameObject Flagship) //워프에 진입할 때 함대모드를 종료
    {
        ShipModeUI.GetComponent<Animator>().SetBool("Flagship mode, Ship Mode Butten", true);
        ShipModeUI.GetComponent<Animator>().SetFloat("Roll, Ship Mode Butten", 1);
        FlagshipEnabled = false;
        RTSControlSystem.FlagShipSelectionModeWarp(Flagship);

        SelectButtenUI.GetComponent<Animator>().SetFloat("Roll, Select Butten", 1);
        SelectDeactive(); //선택 UI 애니메이션
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

    //선택 UI 애니메이션
    public void SelectActive() //편대 모드
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
    public void SelectDeactive() //기함 모드
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
    IEnumerator BehaviorTurnOff() //기함 모드로 돌아갈 때 UI 초기화
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

    //행동 모드 UI
    public void FlagShipBehaviorClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            RTSSelectEnabled = false;

            //기함모드에서 전환
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == true)
            {
                if (FlagShipBehaviorMode) //true : 이동모드, false : 공격 및 지원모드
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

    //함선 기동 모드 전환 애니메이션
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
    //함선 공격 모드 전환 애니메이션
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

    //함대 배열 모드 UI
    public void ShipSelectClick()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false) //배열모드 활성화
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode = true;
                    ShipFormationSettingModeOn();
                }
                else //배열모드 비활성화(지정된 배열을 저장한다.)
                {
                    ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode = false;
                    ShipFormationSettingModeOff();
                }
            }
            else //워프 중인 함대는 배열 모드를 사용할 수 없다.
            {
                Debug.Log("워프 중입니다. 배열 모드를 사용 불가능합니다.");
            }
        }
    }

    //선택한 기함의 배열 모드를 불러오기
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
    public void ShipFormationSettingModeOffWarp(GameObject Flagship) //워프 시 자동으로 함대들이 배열 모드를 해제
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

    //워프 시작(기함이 워프를 시작한 직후)
    public void WarpStart()
    {

    }

    //워프 도착 직전
    public void WarpArrive()
    {
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
    }

    //워프 완료
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