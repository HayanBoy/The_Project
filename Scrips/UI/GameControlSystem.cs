using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameControlSystem : MonoBehaviour
{
    [Header("스크립트")]
    //플레이어
    Animator animator;
    public ObjectManager objectManager;
    public Player player1;
    public GunController gunController;
    public Movement movement;
    public ArthesL775Controller arthesL775Controller;
    public Hydra56Controller hydra56Controller;
    public MEAGController meagController;
    public UGG98Controller ugg98Controller;
    public M3078Controller m3078Controller;
    public VM5GrenadeController vm5GrenadeController;
    public SwichHeavyWeapon swichHeavyWeapon;
    public TutorialSystem TutorialSystem;
    ClearLine ClearLine;

    //MBCA-79
    public VehicleLanding vehicleLanding;
    public RobotPlayer robotPlayer;
    public RobotMove robotmove;
    public HTACController hTACController;
    public APCController aPCController;
    public AutoTurretSystem autoTurretSystem;

    Coroutine vehicleOnlineButten;
    Coroutine playerOnlineButten;

    CountDown countdown;

    [Header("탑승 수단 프리팹 및 좌표")]
    [SerializeField]
    GameObject player, robot, EnterPlayer, VehicleTakeUI, robotAIbutton, camera; //플레이어, 로봇, 로봇탑승 버튼, 로봇ai버튼

    [SerializeField]
    Transform MBCA79Exiting; //MBCA-79 하차 좌표

    [SerializeField]
    Transform robotAI_range; // ai버튼 활성화 범위

    public float GameStartTime; //플레이어를 본격적으로 조종하기까지 걸리는 시간
    public float ShipArrivalTime; //함선이 도착하여 함선지원이 가능하기까지 걸리는 시간

    [Header("카메라")]
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Transform PlayerCameraPos;
    public Transform MBCA79Pos;

    [Header("UI 창")]
    public GameObject Menu;
    public GameObject MenuClick;
    public Image SubMenu; //서브메뉴창
    public Image TutorialMenu; //튜토리얼 메뉴창
    public Image RobotTutorialMenu; //로봇 튜토리얼 메뉴창

    [Header("시스템")]
    public Image GameOverUI; //게임오버 ui
    public Text returnToTitle; //메인화면으로 돌아간다
    public Image Fadein;
    public int ClearCnt;
    public int ClearStageCNT;
    public int NextClearStageCNT;
    private float FailOnceTime;

    [Header("사격 컨트롤러 아이콘")]
    public GameObject TriggerIcon;
    public GameObject ChargeIcon;

    [Header("보병전 조작 UI 버튼")]
    public GameObject MoveJoyStick; //이동 조이스틱
    public GameObject AttackJoyStick; //사격 조이스틱
    public GameObject PlayerMoveUI;
    public GameObject VehicleMoveUI;
    public GameObject PlayerAttackUI;
    public GameObject VehicleAttackUI;
    public GameObject SubWeaponUI;
    public GameObject GrenadeUI;
    public GameObject HpRestoreUI;
    public GameObject DashUI;
    public GameObject AmmoDropUI;
    public GameObject WeaponDropUI;
    public GameObject AirStrikeUI;
    public GameObject SwapUI;
    public GameObject ReloadUI;
    public GameObject CHW;
    public GameObject PlayerMagazine;
    public GameObject PlayerMinigunAmmo;
    public GameObject PlayerAmmoViewUI;
    public GameObject SupArmyBtn;
    public GameObject ShipSupport;
    public GameObject ShipSupportUI;
    public GameObject ShipStateUI;
    public GameObject HealthBar;
    public RectTransform ShipView;
    public Image SubWeaponActive;
    public Image GrenadeActive;
    public Image CHWActive;
    public Image DashActive;
    public Image ReloadActive;
    public Image HPStoreActive;
    public Image SwapActive;
    public Image AmmoDropActive;
    public Image WeaponDropActive;
    public Image AirStrikeActive;
    public Image VehicleCallActive;
    public Image VehicleRecallActive;
    public Image VehicleTakeActive;
    public Image MoveJoystickActive;
    public Image AttackJoystickActive;

    [Header("플레이어 무기 가동 상태")]
    public bool UsingChangeWeapon = false;
    public bool ArthesL775WeaponOn = false;
    public bool Hydra56WeaponOn = false;
    public bool MEAGWeaponOn = false;
    public bool UGG98WeaponOn = false;
    public bool inWeapon = false;
    public bool GetHeavyWeapon = false;
    public bool UsingTask;
    public bool HeavyWeaponOn;
    private bool inRobot; //로봇 탑승했나/안했나 구분 
    private bool RobotAI; //로봇 ai모드 켯나/안켯나 구분
    public bool MBCA79EnterOn = false;
    private float EnterTime = 0;
    public int VehicleType; //탑승차량 유형
    public int TurretType; //자동포탑 유형

    [Header("체인지 중화기 프리팹")]
    public GameObject ChangeSkillBtn; //체인지 중화기 무기 선택 컨트롤러
    public GameObject ChangeSkillEnergyBoard; //체인지 중화기 에너지보드
    public GameObject TopChangeWeapon;
    public GameObject DownChangeWeapon;
    public GameObject RightChangeWeapon;
    public GameObject LeftChangeWeapon;

    [Header("MBCA-79 UI")]
    public GameObject HTACUI;
    public GameObject APCUI;
    public GameObject AutoCannonUI;
    public GameObject VehicleDashUI;
    public GameObject Barrier;
    public GameObject VehicleSubWeaponUI;
    public GameObject VehicleCallUI;
    public GameObject VehicleRecallUI;
    public GameObject AiRangeCol;  // 로봇 ai range
    public GameObject AttackRangeCol;  // 로봇 attack range
    public GameObject AnimationUIVehicleHUD;
    public Image HTACActive;
    public Image APCActive;
    public Image AutoCannonActive;
    public Image VehicleSubWeaponActive;
    public Image VehicleDashActive;

    [Header("MBCA-79 무기 가동 및 탑승 상태")]
    private bool isHTACActive;
    private bool isAPCActive;
    public int ChangeWeaponOnline; //체인지 중화기 사용상태
    public bool isChangeWeapon;
    public static bool robotTheDoor;
    public static bool robotTheAIRange;
    public bool TakeAble; //탑승 가능 여부
    private bool ClickVehicleEnter;
    private bool ClickCHW;
    private bool ClickHTAC;
    private bool ClickAPC;

    [Header("카운트 다운")]
    private bool CountDownControl = true;
    public float setTime = 1200.0f;
    [SerializeField] Text countdownText;
    int min;
    float sec;

    [Header("사운드")]
    public AudioClip RobotEnter;
    public AudioClip RobotEnter2;
    public AudioClip RobotExit;
    public AudioClip Beep1;
    public AudioClip Beep2;
    public AudioClip Beep3;
    public AudioClip Beep4;
    public AudioClip Beep5;
    public AudioClip Beep6;
    public AudioClip Beep7;
    public AudioClip ControlBeep1;
    public AudioClip ControlBeep2;
    public AudioClip ControlBeep3;
    public AudioClip ControlBeep4;
    public AudioClip HealthBarBeep1;
    public AudioClip HealthBarBeep2;
    public AudioClip SwtichControlBeep1;
    public AudioClip SwtichControlBeep2;
    public AudioClip SwtichControlBeep3;
    public AudioClip SwtichControlBeep4;
    public AudioClip SwtichControlBeep5;
    public AudioClip SwtichControlBeep6;
    public AudioClip SwtichControlBeep7;
    public AudioClip SwtichControlBeep8;
    public AudioClip SwtichControlBeep9;
    public AudioClip SwtichControlBeep10;

    void Start()
    {
        Fadein.gameObject.SetActive(true);

        ClearCnt = 0;
        ClearStageCNT = 0;
        NextClearStageCNT = 1;

        // 해당 스크립트 변수값 가지고와서 사용하기 
        animator = GetComponent<Animator>();
        countdown = FindObjectOfType<CountDown>();

        // 여러 메뉴창들 비활성화 
        SubMenu.gameObject.SetActive(false);
        TutorialMenu.gameObject.SetActive(false);
        RobotTutorialMenu.gameObject.SetActive(false);

        //체인지 중화기 언락 정보
        if (WeaponUnlockManager.instance.ChangeWeaponTopSlotUnlock == true)
            TopChangeWeapon.SetActive(true);
        if (WeaponUnlockManager.instance.ChangeWeaponDownSlotUnlock == true)
            DownChangeWeapon.SetActive(true);
        if (WeaponUnlockManager.instance.ChangeWeaponRightSlotUnlock == true)
            RightChangeWeapon.SetActive(true);
        if (WeaponUnlockManager.instance.ChangeWeaponLeftSlotUnlock == true)
            LeftChangeWeapon.SetActive(true);
    }
    void Update()
    {
        //CountDown();
        GameOver();

        if (robotTheDoor || inRobot)
        {
            if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false) //미션 완료가 아닐 때에만 작동
            {
                //로봇 탑승 구에서 플레이어가 작업 중일 경우 로봇 탑승을 금지시킴
                if (arthesL775Controller.UsingTask == true || hydra56Controller.UsingTask == true || meagController.UsingTask == true || ugg98Controller.UsingTask == true
                    || movement.UsingTask == true || gunController.UsingTask == true || vm5GrenadeController.UsingTask == true || m3078Controller.UsingTask == true
                    || player1.UsingTask == true || vehicleLanding.StartOut == true)
                {
                    TakeAble = false;
                    VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", false);
                    VehicleTakeUI.GetComponent<Animator>().SetBool("Access denied, Vehicle take", true);
                }
                else if (arthesL775Controller.UsingTask == false || hydra56Controller.UsingTask == false || meagController.UsingTask == false || ugg98Controller.UsingTask == false
                    || movement.UsingTask == false || gunController.UsingTask == false || vm5GrenadeController.UsingTask == false || m3078Controller.UsingTask == false
                    || player1.UsingTask == false || vehicleLanding.StartOut == false)
                {
                    VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", false);
                    VehicleTakeUI.GetComponent<Animator>().SetBool("Access denied, Vehicle take", false);
                    TakeAble = true;
                }
            }
            else
            {
                VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", true);
            }
            //robotAIbutton.SetActive(true);
        }
        else if (robotTheAIRange)
        {
           // robotAIbutton.SetActive(true);
        }
        else //로봇 탑승 범위를 벗어났을 경우, 탑승 UI을 숨긴다.
        {
            VehicleTakeUI.GetComponent<Animator>().SetBool("Access denied, Vehicle take", false);
            TakeAble = true;
            if (VehicleTakeUI.GetComponent<Animator>().GetFloat("Taking, Vehicle take") == 0)
                VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", true);
            robotAIbutton.SetActive(false);
        }

        //MBCA-79 탑승
        if (MBCA79EnterOn)
        {
            if (EnterTime == 0)
            {
                EnterTime += Time.deltaTime;
                MBCA79EnterOn = false;
                StartCoroutine(MBCA79EnterVehicle());
                StartCoroutine(VehicleUITransform());
            }
        }
    }

    public void MenuClickDown()
    {
        Menu.gameObject.SetActive(false);
        MenuClick.gameObject.SetActive(true);
    }

    public void MenuClickUp()
    {
        Menu.gameObject.SetActive(true);
        MenuClick.gameObject.SetActive(false);
    }

    public void OpenSetting()
    {
        SubMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void OpenSettingClose()
    {
        SubMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TutorialOpen()
    {
        TutorialMenu.gameObject.SetActive(true);
        RobotTutorialMenu.gameObject.SetActive(false);

        Time.timeScale = 0;

    }

    public void TutorialClose()
    {
        TutorialMenu.gameObject.SetActive(false);
        RobotTutorialMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RobotTutorialOpen()
    {
        RobotTutorialMenu.gameObject.SetActive(true);
        TutorialMenu.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1;

        //AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        //operation.allowSceneActivation = true; 
        SceneManager.LoadScene("Main menu");     
    }

    public void RobotAImode()
    {
        if (!RobotAI)
        {
            robot.tag = "Player";
            robot.GetComponent<RobotAI>().enabled = true;

            VehicleTakeUI.GetComponent<Animator>().SetBool("Access denied, Vehicle take", true);
            TakeAble = false;
            VehicleTakeUI.SetActive(false);
            AiRangeCol.gameObject.SetActive(true);
            AttackRangeCol.gameObject.SetActive(true);
        }

        if (RobotAI)
        {
            robot.tag = "Untagged";
            robot.GetComponent<RobotAI>().enabled = false;
            robotTheAIRange = false;

            VehicleTakeUI.GetComponent<Animator>().SetBool("Access denied, Vehicle take", false);
            TakeAble = true;
            AiRangeCol.gameObject.SetActive(false);
            AttackRangeCol.gameObject.SetActive(false);

        }

        RobotAI = !RobotAI;
    }

    public void EnterExitRobotUp()
    {
        if (ClickVehicleEnter == true)
            VehicleTakeUI.GetComponent<Animator>().SetBool("Click, Vehicle take", false);
        ClickVehicleEnter = false;
    }

    public void EnterExitRobotDown()
    {
        ClickVehicleEnter = true;
        SoundManager.instance.SFXPlay2("Sound", Beep5);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Click, Vehicle take", true);
    }

    public void EnterExitRobotEnter()
    {
        if (ClickVehicleEnter == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep5);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            VehicleTakeUI.GetComponent<Animator>().SetBool("Click, Vehicle take", true);
        }
    }

    public void EnterExitRobotExit()
    {
        if (ClickVehicleEnter == true)
            VehicleTakeUI.GetComponent<Animator>().SetBool("Click, Vehicle take", false);
    }

    public void EnterExitRobotClick()
    {
        if (TakeAble == true)
        {
            if (!inRobot) //탑승
            {
                if (VehicleType == 1)
                {
                    TakeAble = false;
                    VehicleTakeUI.GetComponent<Animator>().SetBool("Activated, Vehicle take", true);
                    player.GetComponent<Movement>().MBCA79Entering = true;
                }
            }

            else if (inRobot) //하차
            {
                if (VehicleType == 1)
                {
                    StartCoroutine(MBCA79ExitVehicle());
                    StartCoroutine(PlayerUITransform());
                }
            }

            inRobot = !inRobot;
        }
    }

    //MBCA-79 탑승
    IEnumerator MBCA79EnterVehicle()
    {
        SoundManager.instance.SFXPlay12("Sound", RobotEnter);
        vehicleOnlineButten = StartCoroutine(VehicleOnlineButten());
        movement.TakingVehicle = true;
        AirStrikeUI.GetComponent<Animator>().SetBool("Taking color, Air strike", true);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Taking color, Vehicle take", true);
        cinemachineVirtualCamera.Follow = MBCA79Pos.transform;
        player.transform.position = new Vector2(2000, 2000);
        robot.tag = "Untagged";
        player.tag = "Untagged";
        robot.layer = 0;
        player.layer = 0;
        player1.HealthBarActive = false;
        player1.GetComponent<Movement>().StopMoveAction();
        vehicleLanding.ReadyForBattle = false;

        StartCoroutine(AmmoHUDDeactive());

        player.GetComponent<GunController>().StopReload = true;
        EnterPlayer.gameObject.SetActive(true);
        robot.GetComponent<RobotMove>().Entering(true);
        player.GetComponent<Movement>().VehicleActive = true;
        player.GetComponent<Movement>().MovingStop(true);
        objectManager.SupplyList.Remove(player);
        objectManager.SupplyList.Add(robot);

        if (CHW.GetComponent<Animator>().GetBool("Active(start), CHW") == true)
            CHW.GetComponent<Animator>().SetBool("Active(offline), CHW", true);
        ChangeSkillBtn.gameObject.SetActive(false);
        //GameObject.Find("Ship Spawner").GetComponent<XAxisFollow>().FollowVehicle();

        yield return new WaitForSeconds(0.916f);
        CHW.GetComponent<Animator>().SetBool("Active(start), CHW", false);
        CHW.GetComponent<Animator>().SetBool("Active(offline), CHW", false);
        EnterPlayer.GetComponent<SortingGroup>().sortingOrder = 14;
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.SFXPlay12("Sound", RobotEnter2);

        yield return new WaitForSeconds(0.6f);

        //SupArmyBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.244f);

        player1.GetComponent<Player>().HealthTransform(true);
        player1.HealthBarActive = true;
        player1.VehicleStart = true;

        isHTACActive = true;
        isAPCActive = true;
        hTACController.isGunActive = true;
        aPCController.isGunActive = true;
        aPCController.ChargingStart = true;
        autoTurretSystem.GetComponent<AutoTurretSystem>().NoAmmo = false;

        yield return new WaitForSeconds(0.83f);

        movement.TakingVehicle = false;
        AirStrikeUI.GetComponent<Animator>().SetBool("Taking color, Air strike", false);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Taking color, Vehicle take", false);
        robot.GetComponent<RobotMove>().Entering(false);
        robot.tag = "Vehicle";
        robot.layer = 18;
        player.GetComponent<Movement>().FireJoystickType = 0;
        robot.GetComponent<RobotMove>().FireJoystickType = 1;
        robot.GetComponent<RobotMove>().Stop(true);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Activated, Vehicle take", false);
        HTACActive.GetComponent<Animator>().SetBool("Active, HTAC", true);
        APCActive.GetComponent<Animator>().SetBool("Active, APC", true);

        if (BattleSave.Save1.Tutorial == 2) //MBCA-79 사용법 튜토리얼
        {
            StartCoroutine(TutorialSystem.ShowMBCA79Instruction());
        }
    }

    //MBCA-79 하차
    IEnumerator MBCA79ExitVehicle()
    {
        StartCoroutine(AmmoHUDActive()); //탄약창 활성화

        if (TurretType == 1)
        {
            if (autoTurretSystem.isAutoTurretOnline == false)
                autoTurretSystem.GetComponent<AutoTurretSystem>().AutoSystem();
        }

        SoundManager.instance.SFXPlay12("Sound", RobotExit);
        playerOnlineButten = StartCoroutine(PlayerOnlineButten());
        movement.TakingVehicle = true;
        AirStrikeUI.GetComponent<Animator>().SetBool("Taking color, Air strike", true);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Taking color, Vehicle take", true);
        player1.HealthBarActive = false;
        robot.tag = "Untagged";
        robot.layer = 0;
        robot.GetComponent<RobotMove>().Exiting(true);
        robot.GetComponent<RobotMove>().Stop(false);

        yield return new WaitForSeconds(0.416f);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == true) //하차 도중에 임무가 완료되었을 경우, UI를 다시 해제
        {
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerUI());
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerController());
        }
        isHTACActive = false;
        isAPCActive = false;
        hTACController.isGunActive = false;
        aPCController.isGunActive = false;
        aPCController.ChargingStart = false;

        yield return new WaitForSeconds(0.5f);

        EnterPlayer.GetComponent<SortingGroup>().sortingOrder = 35;
        //SupArmyBtn.gameObject.SetActive(true);

        player1.GetComponent<Player>().HealthTransform(false);
        player1.HealthBarActive = true;

        yield return new WaitForSeconds(1);

        objectManager.SupplyList.Remove(robot);
        objectManager.SupplyList.Insert(0, player);
        robot.tag = "Vehicle";
        player.tag = "Player";
        robot.layer = 18;
        player.layer = 6;
        EnterTime = 0;
        cinemachineVirtualCamera.Follow = PlayerCameraPos.transform;
        player.transform.position = new Vector2(MBCA79Exiting.position.x, MBCA79Exiting.position.y);
        player.GetComponent<Movement>().VehicleActive = false;
        EnterPlayer.gameObject.SetActive(false);
        robot.GetComponent<RobotMove>().Exiting(false);
        if (!inWeapon)
            player.GetComponent<Movement>().FireJoystickType = 1;
        if (inWeapon)
            player.GetComponent<Movement>().FireJoystickType = 100;
        robot.GetComponent<RobotMove>().FireJoystickType = 0;
        player.GetComponent<Movement>().MovingStop(false);
        TakeAble = true;
        player.GetComponent<GunController>().StopReload = false;
        movement.TakingVehicle = false;
        vehicleLanding.ReadyForBattle = true;
        AirStrikeUI.GetComponent<Animator>().SetBool("Taking color, Air strike", false);
        VehicleTakeUI.GetComponent<Animator>().SetBool("Taking color, Vehicle take", false);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == true)
        {
            StartCoroutine(AmmoHUDDeactive());
        }
    }

    //게임을 시작할 때 플레이어 버튼 활성화
    public IEnumerator StartButten()
    {
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 2);
        }
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 2);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 2);
        }
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 2);
        }
        StartCoroutine(HealthBarActive());
        if (GetHeavyWeapon == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SwapUI.GetComponent<Animator>().SetFloat("Transform, Swap", 2);
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(AmmoHUDActive());
        yield return new WaitForSeconds(0.8f);
        player.GetComponent<Player>().HealthBarActive = true;

        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
            CHW.GetComponent<Animator>().SetBool("Turn off, CHW", false);
        ReloadUI.GetComponent<Animator>().SetBool("Turn off, Reload", false);
        HpRestoreUI.GetComponent<Animator>().SetBool("Turn off, Hp store", false);
        DashUI.GetComponent<Animator>().SetBool("Turn off, Dash", false);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
            SubWeaponUI.GetComponent<Animator>().SetBool("Turn off, Sub weapon fire", false);
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
            GrenadeUI.GetComponent<Animator>().SetBool("Turn off, Grenade", false);

        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
            CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 0);
        ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 0);
        HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 0);
        DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 0);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
            SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 0);
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
            GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 0);

        if (ChangeWeaponOnline > 0)
            ChangeSkillBtn.SetActive(true);

        yield return new WaitForSeconds(GameStartTime);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
            SubWeaponActive.raycastTarget = true;
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
            GrenadeActive.raycastTarget = true;
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
            CHWActive.raycastTarget = true;
        DashActive.raycastTarget = true;
        ReloadActive.raycastTarget = true;
        HPStoreActive.raycastTarget = true;
        if (GetHeavyWeapon == true)
            SwapActive.raycastTarget = true;
    }

    //게임을 시작할 때 플레이어 컨트롤러 활성화
    public IEnumerator StartController()
    {
        SoundManager.instance.SFXPlay2("Sound", ControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep3);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep4);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Start, Move joystick", 1);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Start, Attack joystick", 1);
        yield return new WaitForSeconds(1.53f);
        MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", false);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", false);
        MoveJoystickActive.raycastTarget = true;
        AttackJoystickActive.raycastTarget = true;

        TriggerIcon.SetActive(true);
        ChargeIcon.SetActive(false);
    }

    //차량에 탑승할 때 버튼 전환
    IEnumerator VehicleOnlineButten()
    {
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
            SubWeaponActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
            GrenadeActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
            CHWActive.raycastTarget = false;
        DashActive.raycastTarget = false;
        ReloadActive.raycastTarget = false;
        HPStoreActive.raycastTarget = false;
        if (GetHeavyWeapon == true)
            SwapActive.raycastTarget = false;
        AmmoDropActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeActive.raycastTarget = false;
        VehicleRecallActive.raycastTarget = false;
        VehicleTakeActive.raycastTarget = false;

        player1.GetComponent<Player>().ButtenOutAtVehicle(true);
        VehicleTakeUI.GetComponent<Animator>().SetFloat("Taking, Vehicle take", 1);
        if (GetHeavyWeapon == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SwapUI.GetComponent<Animator>().SetFloat("Transform, Swap", 1);
        }
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 1);
        }
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 1);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 1);
        }
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 1);
        }
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AirStrikeUI.GetComponent<Animator>().SetFloat("Taking, Air strike", 1);
        }
        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 1);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 1);
        }
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep7);
        VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 1);
        yield return new WaitForSeconds(1.16f);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            AutoCannonUI.GetComponent<Animator>().SetBool("Turn off, FBWS", false);
            AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 2);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 1);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            APCUI.GetComponent<Animator>().SetBool("Turn off, APC", false);
            APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            HTACUI.GetComponent<Animator>().SetBool("Turn off, HTAC", false);
            HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            VehicleDashUI.GetComponent<Animator>().SetBool("Turn off, MBCA-79 dash", false);
            VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            VehicleSubWeaponUI.GetComponent<Animator>().SetBool("Turn off, OSEHS", false);
            VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 2);
        }
        yield return new WaitForSeconds(0.43f);

        player1.GetComponent<Player>().VehicleButtenOutAtVehicle(true);
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            HTACActive.raycastTarget = true;
            APCActive.raycastTarget = true;
            AutoCannonActive.raycastTarget = true;
            VehicleSubWeaponActive.raycastTarget = true;
            VehicleDashActive.raycastTarget = true;
            if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
                AirStrikeActive.raycastTarget = true;
            VehicleTakeActive.raycastTarget = true;
            HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 0);
            APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 0);
            AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 0);
            VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 0);
            VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 0);
        }
        else //임무가 완료되었을 경우, 다시 해제
        {
            StartCoroutine(DisappearVehicleUI());
        }
    }

    //차량에 탑승할 때 컨트롤러 전환
    IEnumerator VehicleUITransform()
    {
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep3);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 1);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 1);
        yield return new WaitForSeconds(1.55f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep4);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep5);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep6);
        PlayerMoveUI.SetActive(false);
        PlayerAttackUI.SetActive(false);

        TriggerIcon.SetActive(true);
        ChargeIcon.SetActive(true);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            VehicleMoveUI.SetActive(true);
            VehicleAttackUI.SetActive(true);
            MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 2);
            AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 2);
            yield return new WaitForSeconds(1.633f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            MoveJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Move joystick", true);
            AttackJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Attack joystick", true);
            MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 0);
            AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 0);
        }
        else //임무가 완료되었을 경우, 다시 해제
        {
            StartCoroutine(DisappearVehicleController());
        }
    }

    //차량에서 내릴 때 버튼 전환
    IEnumerator PlayerOnlineButten()
    {
        player1.GetComponent<Player>().VehicleButtenOutAtVehicle(false);
        HTACActive.raycastTarget = false;
        APCActive.raycastTarget = false;
        AutoCannonActive.raycastTarget = false;
        VehicleSubWeaponActive.raycastTarget = false;
        VehicleDashActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeActive.raycastTarget = false;
        VehicleTakeActive.raycastTarget = false;

        VehicleTakeUI.GetComponent<Animator>().SetFloat("Taking, Vehicle take", 2);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetFloat("Taking, Air strike", 2);
        AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 1);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 1);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            if (GetHeavyWeapon == true)
            {
                SoundManager.instance.SFXPlay2("Sound", Beep6);
                SwapUI.GetComponent<Animator>().SetFloat("Transform, Swap", 2);
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false && WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false && WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false && WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false && WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 2);
            yield return new WaitForSeconds(0.1f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 2);
            yield return new WaitForSeconds(0.4f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            if (GetHeavyWeapon == true)
                SwapUI.GetComponent<Animator>().SetFloat("Transform, Swap", 0);
            if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
                CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 0);
            ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 0);
            HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 0);
            DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 0);
            if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
                SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 0);
            if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
                GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 0);
            AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 0);
            if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
                WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 0);
            if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
                AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 0);
            VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 0);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 0);
            yield return new WaitForSeconds(0.116f);
        }
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetFloat("Taking, Air strike", 0);
        player1.GetComponent<Player>().ButtenOutAtVehicle(false);
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
                SubWeaponActive.raycastTarget = true;
            if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
                GrenadeActive.raycastTarget = true;
            if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
                CHWActive.raycastTarget = true;
            DashActive.raycastTarget = true;
            ReloadActive.raycastTarget = true;
            HPStoreActive.raycastTarget = true;
            if (GetHeavyWeapon == true)
                SwapActive.raycastTarget = true;
            AmmoDropActive.raycastTarget = true;
            if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
                WeaponDropActive.raycastTarget = true;
            if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
                AirStrikeActive.raycastTarget = true;
            VehicleCallActive.raycastTarget = true;
            VehicleRecallActive.raycastTarget = true;
            VehicleTakeActive.raycastTarget = true;
        }
        else //임무가 완료되었을 경우, 다시 해제
        {
            StartCoroutine(DisappearPlayerUI());
        }
        VehicleTakeUI.GetComponent<Animator>().SetFloat("Taking, Vehicle take", 0);
    }

    //차량에서 내릴 때 컨트롤러 전환
    IEnumerator PlayerUITransform()
    {
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep3);
        MoveJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Move joystick", false);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Attack joystick", false);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", -1);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", -1);
        yield return new WaitForSeconds(0.716f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep7);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep8);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep9);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay25("Sound", SwtichControlBeep10);
        VehicleMoveUI.SetActive(false);
        VehicleAttackUI.SetActive(false);

        TriggerIcon.SetActive(true);
        ChargeIcon.SetActive(false);

        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            PlayerMoveUI.SetActive(true);
            PlayerAttackUI.SetActive(true);
            MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", -2);
            AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", -2);
            MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", true);
            AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", true);
            yield return new WaitForSeconds(1.533f);
        }
        if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", false);
            AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", false);
            MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 0);
            AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 0);
        }
        else //임무가 완료되었을 경우, 다시 해제
        {
            StartCoroutine(DisappearPlayerController());
            MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", -3);
            AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", -3);
        }
    }

    //게임 시작시, 플레이어가 등장하기 전까지 버튼 비활성화
    public void StartUI()
    {
        SubWeaponActive.raycastTarget = false;
        GrenadeActive.raycastTarget = false;
        CHWActive.raycastTarget = false;
        DashActive.raycastTarget = false;
        ReloadActive.raycastTarget = false;
        HPStoreActive.raycastTarget = false;
        SwapActive.raycastTarget = false;
        AmmoDropActive.raycastTarget = false;
        WeaponDropActive.raycastTarget = false;
        AirStrikeActive.raycastTarget = false;
        VehicleCallActive.raycastTarget = false;
        VehicleRecallActive.raycastTarget = false;
        HTACActive.raycastTarget = false;
        APCActive.raycastTarget = false;
        AutoCannonActive.raycastTarget = false;
        VehicleSubWeaponActive.raycastTarget = false;
        VehicleDashActive.raycastTarget = false;
        MoveJoystickActive.raycastTarget = false;
        AttackJoystickActive.raycastTarget = false;
        MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", true);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", true);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Start, Move joystick", 0);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Start, Attack joystick", 0);
        SwapUI.GetComponent<Animator>().SetBool("Turn off, Swap", true);
        CHW.GetComponent<Animator>().SetBool("Turn off, CHW", true);
        ReloadUI.GetComponent<Animator>().SetBool("Turn off, Reload", true);
        HpRestoreUI.GetComponent<Animator>().SetBool("Turn off, Hp store", true);
        DashUI.GetComponent<Animator>().SetBool("Turn off, Dash", true);
        SubWeaponUI.GetComponent<Animator>().SetBool("Turn off, Sub weapon fire", true);
        GrenadeUI.GetComponent<Animator>().SetBool("Turn off, Grenade", true);
        AmmoDropActive.GetComponent<Animator>().SetBool("Turn off, Ammo drop", true);
        WeaponDropActive.GetComponent<Animator>().SetBool("Turn off, Weapon drop", true);
        AirStrikeActive.GetComponent<Animator>().SetBool("Turn off, Air strike", true);
        VehicleCallActive.GetComponent<Animator>().SetBool("Turn off, Vehicle call", true);
        VehicleRecallActive.GetComponent<Animator>().SetBool("Turn off, Vehicle recall", true);
        HTACUI.GetComponent<Animator>().SetBool("Turn off, HTAC", true);
        APCUI.GetComponent<Animator>().SetBool("Turn off, APC", true);
        AutoCannonUI.GetComponent<Animator>().SetBool("Turn off, FBWS", true);
        VehicleSubWeaponUI.GetComponent<Animator>().SetBool("Turn off, OSEHS", true);
        VehicleDashUI.GetComponent<Animator>().SetBool("Turn off, MBCA-79 dash", true);
    }

    //체력바 활성화
    public IEnumerator HealthBarActive()
    {
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", HealthBarBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", HealthBarBeep2);
    }

    //탄약 정보창 활성화
    public IEnumerator AmmoHUDActive()
    {
        PlayerAmmoViewUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(true);
    }

    //체력바 비활성화
    public IEnumerator HealthBarDeactive()
    {
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        HealthBar.SetActive(false);
    }

    //탄약 정보창 비활성화
    public IEnumerator AmmoHUDDeactive()
    {
        PlayerAmmoViewUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        PlayerAmmoViewUI.gameObject.SetActive(false);
    }

    //함선 워프 도착 연출
    public IEnumerator StartShipArrival()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        ShipSupportUI.SetActive(true);
        ShipSupportUI.GetComponent<Animator>().SetBool("Ship cool time, Ship", true);
        ShipSupportUI.GetComponent<Animator>().SetFloat("Cool time, Ship", 1 / ShipArrivalTime);
        ShipSupportUI.GetComponent<Animator>().SetFloat("Ship warp start, Ship", 1);
        yield return new WaitForSeconds(ShipArrivalTime -1);
        ShipSupport.SetActive(true);
        ShipSupport.GetComponent<Animator>().SetBool("Warp complete, Ship", true);
        yield return new WaitForSeconds(1);
        ShipSupportUI.GetComponent<Animator>().SetFloat("Ship warp start, Ship", 2);
        yield return new WaitForSeconds(0.5f);

        ShipStateUI.SetActive(true);

        yield return new WaitForSeconds(1);
        SoundManager.instance.SFXPlay2("Sound", Beep7);
        AmmoDropActive.raycastTarget = true;
        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 2);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            WeaponDropActive.raycastTarget = true;
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 2);
        }
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AirStrikeActive.raycastTarget = true;
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 2);
        }
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            VehicleCallActive.raycastTarget = true;
            VehicleCallUI.GetComponent<Animator>().SetFloat("Transform, Vehicle call", 2);
        }       
        yield return new WaitForSeconds(1);
        AmmoDropUI.GetComponent<Animator>().SetBool("Turn off, Ammo drop", false);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropUI.GetComponent<Animator>().SetBool("Turn off, Weapon drop", false);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetBool("Turn off, Air strike", false);
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            VehicleCallUI.GetComponent<Animator>().SetBool("Turn off, Vehicle call", false);

        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 0);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 0);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 0);
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            VehicleCallUI.GetComponent<Animator>().SetFloat("Transform, Vehicle call", 0);
    }

    //함선 지원 UI 활성화
    public IEnumerator StartShipUIActive()
    {
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            VehicleTakeActive.raycastTarget = true;
        SoundManager.instance.SFXPlay2("Sound", Beep7);
        AmmoDropActive.raycastTarget = true;
        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 2);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            WeaponDropActive.raycastTarget = true;
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 2);
        }
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AirStrikeActive.raycastTarget = true;
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 2);
        }

        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
        {
            if (GameObject.Find("Play Control/Player").GetComponent<Movement>().CallBackComplete == true) //차량을 부른 상태일 경우, 리콜 버튼을 활성화
            {
                yield return new WaitForSeconds(0.1f);
                SoundManager.instance.SFXPlay2("Sound", Beep7);
                VehicleRecallActive.raycastTarget = true;
                VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 2);
            }
            else //차량을 부르지 않은 상태일 경우, 호출 버튼을 활성화
            {
                yield return new WaitForSeconds(0.1f);
                SoundManager.instance.SFXPlay2("Sound", Beep7);
                VehicleCallActive.raycastTarget = true;
                VehicleCallUI.GetComponent<Animator>().SetFloat("Transform, Vehicle call", 2);
            }
        }
        ShipView.anchoredPosition = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        AmmoDropUI.GetComponent<Animator>().SetBool("Turn off, Ammo drop", false);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropUI.GetComponent<Animator>().SetBool("Turn off, Weapon drop", false);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetBool("Turn off, Air strike", false);

        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 0);
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 0);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 0);
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
        {
            VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 0);
            VehicleCallUI.GetComponent<Animator>().SetFloat("Transform, Vehicle call", 0);
        }
    }

    //플레이어 컨트롤러를 모두 활성화
    public IEnumerator ActivePlayerController()
    {
        PlayerMoveUI.SetActive(true);
        PlayerAttackUI.SetActive(true);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep3);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", ControlBeep4);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", -2);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", -2);
        MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", true);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", true);
        yield return new WaitForSeconds(1.55f);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 0);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 0);
        MoveJoyStick.GetComponent<Animator>().SetBool("Start player, Move joystick", false);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start player, Attack joystick", false);
        MoveJoystickActive.raycastTarget = true;
        AttackJoystickActive.raycastTarget = true;

        TriggerIcon.SetActive(true);
        ChargeIcon.SetActive(false);
    }

    //탑승차량 전투 UI를 모두 활성화
    public IEnumerator ActiveVehicleUI()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        AutoCannonUI.GetComponent<Animator>().SetBool("Turn off, FBWS", false);
        AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 2);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        APCUI.GetComponent<Animator>().SetBool("Turn off, APC", false);
        APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HTACUI.GetComponent<Animator>().SetBool("Turn off, HTAC", false);
        HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleDashUI.GetComponent<Animator>().SetBool("Turn off, MBCA-79 dash", false);
        VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleSubWeaponUI.GetComponent<Animator>().SetBool("Turn off, OSEHS", false);
        VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 2);
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 2);
        }
        ShipView.anchoredPosition = new Vector2(0, 0);
        yield return new WaitForSeconds(0.43f);

        VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", true);
        HTACActive.raycastTarget = true;
        APCActive.raycastTarget = true;
        AutoCannonActive.raycastTarget = true;
        VehicleSubWeaponActive.raycastTarget = true;
        VehicleDashActive.raycastTarget = true;
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeActive.raycastTarget = true;
        VehicleTakeActive.raycastTarget = true;
        HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 0);
        APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 0);
        AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 0);
        VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 0);
        VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 0);
    }

    //탑승차량 컨트롤러를 모두 활성화
    public IEnumerator ActiveVehicleController()
    {
        VehicleMoveUI.SetActive(true);
        VehicleAttackUI.SetActive(true);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep4);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep5);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep6);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 2);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 2);
        MoveJoystickActive.raycastTarget = true;
        AttackJoystickActive.raycastTarget = true;

        TriggerIcon.SetActive(true);
        ChargeIcon.SetActive(true);
    }

    //플레이어 전투 UI를 모두 비활성화
    public IEnumerator DisappearPlayerUI()
    {
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
            SubWeaponActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
            GrenadeActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
            CHWActive.raycastTarget = false;
        DashActive.raycastTarget = false;
        ReloadActive.raycastTarget = false;
        HPStoreActive.raycastTarget = false;
        SwapActive.raycastTarget = false;
        if (ChangeWeaponOnline > 0)
            ChangeSkillBtn.SetActive(false);

        if (GetHeavyWeapon == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SwapUI.GetComponent<Animator>().SetFloat("Transform, Swap", 1);
        }
        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            CHW.GetComponent<Animator>().SetFloat("Transform, CHW", 1);
        }
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        ReloadUI.GetComponent<Animator>().SetFloat("Transform, Reload", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HpRestoreUI.GetComponent<Animator>().SetFloat("Transform, Hp store", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        DashUI.GetComponent<Animator>().SetFloat("Transform, Dash", 1);
        if (WeaponUnlockManager.instance.SubWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            SubWeaponUI.GetComponent<Animator>().SetFloat("Transform, Sub weapon fire", 1);
        }
        if (WeaponUnlockManager.instance.GrenadeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep6);
            GrenadeUI.GetComponent<Animator>().SetFloat("Transform, Grenade", 1);
        }
    }

    //플레이어 컨트롤러를 모두 비활성화
    public IEnumerator DisappearPlayerController()
    {
        MoveJoystickActive.raycastTarget = false;
        AttackJoystickActive.raycastTarget = false;
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep3);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", 1);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", 1);

        TriggerIcon.SetActive(false);
        ChargeIcon.SetActive(false);
    }

    //탑승차량 전투 UI를 모두 비활성화
    public IEnumerator DisappearVehicleUI()
    {
        HTACActive.raycastTarget = false;
        APCActive.raycastTarget = false;
        AutoCannonActive.raycastTarget = false;
        VehicleSubWeaponActive.raycastTarget = false;
        VehicleDashActive.raycastTarget = false;

        AutoCannonUI.GetComponent<Animator>().SetFloat("Transform, FBWS", 1);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        APCUI.GetComponent<Animator>().SetFloat("Transform, APC", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        HTACUI.GetComponent<Animator>().SetFloat("Transform, HTAC", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleDashUI.GetComponent<Animator>().SetFloat("Transform, MBCA-79 dash", 1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep6);
        VehicleSubWeaponUI.GetComponent<Animator>().SetFloat("Transform, OSEHS", 1);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetFloat("Activated, Vehicle HUD", 0);
    }

    //탑승차량 컨트롤러를 모두 비활성화
    public IEnumerator DisappearVehicleController()
    {
        MoveJoystickActive.raycastTarget = false;
        AttackJoystickActive.raycastTarget = false;
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep1);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep2);
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", SwtichControlBeep3);
        MoveJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Move joystick", false);
        AttackJoyStick.GetComponent<Animator>().SetBool("Start vehicle, Attack joystick", false);
        MoveJoyStick.GetComponent<Animator>().SetFloat("Transform, Move joystick", -1);
        AttackJoyStick.GetComponent<Animator>().SetFloat("Transform, Attack joystick", -1);

        TriggerIcon.SetActive(false);
        ChargeIcon.SetActive(false);
    }

    //함선 지원 UI 비활성화
    public IEnumerator DisappearShipUI()
    {
        AmmoDropActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            WeaponDropActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
            AirStrikeActive.raycastTarget = false;
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
        {
            VehicleRecallActive.raycastTarget = false;
            VehicleTakeActive.raycastTarget = false;
            VehicleTakeUI.GetComponent<Animator>().SetBool("TurnOff, Vehicle take", false);
        }
        if (WeaponUnlockManager.instance.AirStrikeCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            AirStrikeUI.GetComponent<Animator>().SetFloat("Transform, Air strike", 1);
        }
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            WeaponDropUI.GetComponent<Animator>().SetFloat("Transform, Weapon drop", 1);
        }
        if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.SFXPlay2("Sound", Beep7);
            VehicleRecallActive.GetComponent<Animator>().SetFloat("Transform, Vehicle recall", 1);
            VehicleCallUI.GetComponent<Animator>().SetFloat("Transform, Vehicle call", 1);
        }
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay2("Sound", Beep7);
        AmmoDropUI.GetComponent<Animator>().SetFloat("Transform, Ammo drop", 1);
        ShipView.anchoredPosition = new Vector2(1000, 1000);
    }

    //체인지 중화기 무기 선택 버튼 4개
    public void ChangeWeaponSystemUp()
    {
        if (ClickCHW == true)
            CHW.GetComponent<Animator>().SetBool("Click, CHW", false);
        ClickCHW = false;
    }

    public void ChangeWeaponSystemDown()
    {
        ClickCHW = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        CHW.GetComponent<Animator>().SetBool("Click, CHW", true);
    }

    public void ChangeWeaponSystemEnter()
    {
        if (ClickCHW == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            CHW.GetComponent<Animator>().SetBool("Click, CHW", true);
        }
    }

    public void ChangeWeaponSystemExit()
    {
        if (ClickCHW == true)
            CHW.GetComponent<Animator>().SetBool("Click, CHW", false);
    }

    //체인지 중화기 활성화 버튼
    public void ChangeWeaponSystemClick()
    {
        if (UsingChangeWeapon == false)
        {
            if (!isChangeWeapon)
            {
                ChangeWeaponOnline++;
                CHW.GetComponent<Animator>().SetBool("Active(start), CHW", true);
                ChangeSkillBtn.gameObject.SetActive(true);
                if (vm5GrenadeController.isBomb == true)
                    vm5GrenadeController.BombOff();
            }

            if (isChangeWeapon)
            {
                ChangeWeaponOnline--;
                CHW.GetComponent<Animator>().SetBool("Active(offline), CHW", true);
                CHW.GetComponent<Animator>().SetFloat("Active(icon direction), CHW", -2);
                GetComponent<ChangeSkillSystem>().StopWeapons();
                Invoke("OfflineActive", 0.1f);
                ChangeSkillEnergyBoard.gameObject.SetActive(false);

                if (!inWeapon) // 기본 총일 때 
                {
                    TriggerIcon.SetActive(true);
                    ChargeIcon.SetActive(false);
                    PlayerMagazine.gameObject.SetActive(true);
                    PlayerMinigunAmmo.gameObject.SetActive(false);
                }

                if (inWeapon) // 미니건으로 스왑했을 때 
                {
                    TriggerIcon.SetActive(true);
                    ChargeIcon.SetActive(false);
                    PlayerMagazine.gameObject.SetActive(false);
                    PlayerMinigunAmmo.gameObject.SetActive(true);
                }
            }

            if (isChangeWeapon == true && ArthesL775WeaponOn == true)
            {
                GameObject.Find("Player").GetComponent<ArthesL775Controller>().TurnOff();
                ArthesL775WeaponOn = false;
                GetComponent<ChangeSkillSystem>().ArthesL775WeaponOn = false;
            }

            if (isChangeWeapon == true && Hydra56WeaponOn == true)
            {
                GameObject.Find("Player").GetComponent<Hydra56Controller>().TurnOff();
                Hydra56WeaponOn = false;
                GetComponent<ChangeSkillSystem>().Hydra56WeaponOn = false;
            }

            if (isChangeWeapon == true && MEAGWeaponOn == true)
            {
                GameObject.Find("Player").GetComponent<MEAGController>().TurnOff();
                MEAGWeaponOn = false;
                GetComponent<ChangeSkillSystem>().MEAGWeaponOn = false;
            }

            if (isChangeWeapon == true && UGG98WeaponOn == true)
            {
                GameObject.Find("Player").GetComponent<UGG98Controller>().TurnOff();
                UGG98WeaponOn = false;
                GetComponent<ChangeSkillSystem>().UGG98WeaponOn = false;
            }

            isChangeWeapon = !isChangeWeapon;
        }
    }

    public void ChangeWeaponOff()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        ChangeSkillBtn.SetActive(false);
        CHW.GetComponent<Animator>().SetBool("Active(offline), CHW", true);
        CHW.GetComponent<Animator>().SetFloat("Active(icon direction), CHW", -2);
        Invoke("OfflineActive", 0.1f);
    }

    void OfflineActive()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        CHW.GetComponent<Animator>().SetBool("Active(start), CHW", false);
        CHW.GetComponent<Animator>().SetBool("Active(offline), CHW", false);
        if (CHW.GetComponent<Animator>().GetBool("Active(run), CHW") == true)
            CHW.GetComponent<Animator>().SetBool("Active(run), CHW", false);
        if (CHW.GetComponent<Animator>().GetBool("Active(icon run), CHW") == true)
            CHW.GetComponent<Animator>().SetBool("Active(icon run), CHW", false);
    }

    //HTAC 사격 활성화
    public void HTACActivebutten()
    {
        if (!isHTACActive)
        {
            //Debug.Log("HTAC on");
            TriggerIcon.SetActive(true);
            hTACController.isGunActive = true;
            SoundManager.instance.SFXPlay2("Sound", Beep3);
            HTACActive.GetComponent<Animator>().SetBool("Active, HTAC", true);
        }
        if (isHTACActive)
        {
            //Debug.Log("HTAC off");
            TriggerIcon.SetActive(false);
            hTACController.isGunActive = false;
            SoundManager.instance.SFXPlay2("Sound", Beep4);
            HTACActive.GetComponent<Animator>().SetBool("Active, HTAC", false);
        }

        isHTACActive = !isHTACActive;
    }

    public void HTACActivebuttenUp()
    {
        if (ClickHTAC == true)
        {
            if (isHTACActive == false)
                HTACActive.GetComponent<Animator>().SetBool("Click, HTAC", false);
            else
                HTACActive.GetComponent<Animator>().SetBool("ClickOnline, HTAC", false);
        }
        ClickHTAC = false;
    }

    public void HTACActivebuttenDown()
    {
        ClickHTAC = true;
        if (isHTACActive == false)
            HTACActive.GetComponent<Animator>().SetBool("Click, HTAC", true);
        else
            HTACActive.GetComponent<Animator>().SetBool("ClickOnline, HTAC", true);
    }

    public void HTACActivebuttenEnter()
    {
        if (ClickHTAC == true)
        {
            if (isHTACActive == false)
                HTACActive.GetComponent<Animator>().SetBool("Click, HTAC", true);
            else
                HTACActive.GetComponent<Animator>().SetBool("ClickOnline, HTAC", true);
        }
    }

    public void HTACActivebuttenExit()
    {
        if (ClickHTAC == true)
        {
            if (isHTACActive == false)
                HTACActive.GetComponent<Animator>().SetBool("Click, HTAC", false);
            else
                HTACActive.GetComponent<Animator>().SetBool("ClickOnline, HTAC", false);
        }
    }

    //APC 사격 활성화
    public void APCActivebutten()
    {
        if (!isAPCActive)
        {
            //Debug.Log("APC on");
            ChargeIcon.SetActive(true);
            aPCController.isGunActive = true;
            SoundManager.instance.SFXPlay2("Sound", Beep3);
            APCActive.GetComponent<Animator>().SetBool("Active, APC", true);
        }
        if (isAPCActive)
        {
            //Debug.Log("APC off");
            ChargeIcon.SetActive(false);
            aPCController.isGunActive = false;
            SoundManager.instance.SFXPlay2("Sound", Beep4);
            APCActive.GetComponent<Animator>().SetBool("Active, APC", false);
        }

        isAPCActive = !isAPCActive;
    }

    public void APCActivebuttenUp()
    {
        if (ClickAPC == true)
        {
            if (isAPCActive == false)
                APCActive.GetComponent<Animator>().SetBool("Click, APC", false);
            else
                APCActive.GetComponent<Animator>().SetBool("ClickOnline, APC", false);
        }
        ClickAPC = false;
    }

    public void APCActivebuttenDown()
    {
        ClickAPC = true;
        if (isAPCActive == false)
            APCActive.GetComponent<Animator>().SetBool("Click, APC", true);
        else
            APCActive.GetComponent<Animator>().SetBool("ClickOnline, APC", true);
    }

    public void APCActivebuttenEnter()
    {
        if (ClickAPC == true)
        {
            if (isAPCActive == false)
                APCActive.GetComponent<Animator>().SetBool("Click, APC", true);
            else
                APCActive.GetComponent<Animator>().SetBool("ClickOnline, APC", true);
        }
    }

    public void APCActivebuttenExit()
    {
        if (ClickAPC == true)
        {
            if (isAPCActive == false)
                APCActive.GetComponent<Animator>().SetBool("Click, APC", false);
            else
                APCActive.GetComponent<Animator>().SetBool("ClickOnline, APC", false);
        }
    }

    public void GameOver() // 게임오버 조건 
    {
        if (robotPlayer != null && player1 != null)
        {
            if (inRobot == true && robotPlayer.hitPoints <= 0 || inRobot == false && player1.hitPoints <= 0)
            {
                if (FailOnceTime == 0)
                {
                    FailOnceTime += Time.deltaTime;
                    ClearLine = FindObjectOfType<ClearLine>();
                    ClearLine.MissionFail();
                }
            }
        }
    }
}