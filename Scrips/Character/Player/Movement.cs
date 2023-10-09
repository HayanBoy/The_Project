using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour
{
    [Header("스크립트")]
    Player player;
    GunController gunController;  //GunController의 CharsingEnergy 값 연동을 위한 선언
    M3078Controller m3078Controller;
    Hydra56Controller hydra59Controller;
    VM5GrenadeController vM5GrenadeController;
    Animator animator;
    Rigidbody2D rb2D;
    public ObjectManager objectManager;
    public BombSettings BombSettings;
    public GameControlSystem GameControlSystem;

    [Header("플레이어 이동 컨트롤러")]
    public GameObject AnimationUIMove;
    public GameObject MoveController1;
    public GameObject MoveController2;
    public GameObject MoveController3;
    public GameObject ActiveMoveController1;
    public GameObject ActiveMoveController2;
    public Image SpeedBar;

    [Header("플레이어 공격 컨트롤러")]
    public GameObject AnimationUIAttack;
    public GameObject AttackController1;
    public GameObject AttackController2;
    public GameObject AttackController3;
    public GameObject ActiveAttackController1;
    public GameObject ActiveAttackController2;

    [Header("UI")]
    public GameObject AnimationUIDash;
    public GameObject AnimationUIAirStrike;
    public GameObject AnimationUIVehicleCall;

    //플레이어 기타 행동들
    private Vector2 target;
    private Vector2 movement = new Vector2(); //벡터2 생성 선언.

    [Header("이동")]
    public bool SubMachineGunOnline; //기관단총 활성화
    public float Speed; //플레이어 기본 이동 속도.
    private float XtargetSpeed; //플레이어의 기본 속도에 맞출 목표 속도.
    private float YtargetSpeed;
    private float SpeedView;

    [Header("상태 스위치")]
    private int MovingState;
    public int HeavyWeaponOnline; //중화기 무기 사용 여부

    public bool StartAction = false; //수류탄을 든 액션이 취해졌을 때, Blend tree의 댐퍼 현상용 애니메이션코드로 전환하기 위한 스위치
    private bool BeamFiring = false; //X축 반전 사용 여부
    public bool isDash;
    public bool isRaiserBomb;
    public bool UsingChangeWeapon;
    public bool VehicleActive = false; //차량에 탑승했을 때 이동 및 행동 금지용 스위치
    private bool ClickDash;
    private bool ClickAirStrike;
    private bool ClickVehicleCall;

    [Header("조이스틱")]
    public Joystick joyStick;
    public Joystick GunjoyStick;
    public Joystick joyStickCopy;
    public Joystick GunjoyStickCopy;
    public bool FireJoystick;
    public int FireJoystickType; //무기별 전달용 사격 컨트롤러
    float horizontalMove = 0f;

    [Header("대쉬")]
    public Text DashCountTxt;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashCooldown = 0.5f; //대쉬 쿨타임.
    public float DashCoolTime; //대쉬가 하나 채워지는 쿨타임
    public float DashTime;

    public int DashCount; //대쉬할 수 있는 횟수
    private int DashCountInGame;
    public int dashDamage = 1000;

    private bool Dashing = false; //대쉬를 한번만 할 수 있도록 조취
    private bool DashOn = false;
    public bool UsingTask;
    public bool TakingVehicle = false;

    [Header("대쉬 부스터 이펙트")]
    public GameObject mainBoostPrefab; //메인 부스트 프리팹
    public Transform mainBoostPos; //메인 부스트 좌표
    public GameObject subBoostPrefab1; //서브 부스트1 프리팹
    public Transform subBoostPos1; //서브 부스트1 좌표
    public GameObject subBoostPrefab2; //서브 부스트2 프리팹
    public Transform subBoostPos2; //서브 부스트2 좌표
    public GameObject boostCloudPrefab; //부스트 연기 프리팹

    [Header("폭격지원")]
    public int MissileAmountPlus; //미사일 갯수 강화용
    public int random; // 미사일 위치 랜덤좌표 생성
    public int AirStrikeCnt;
    public int PGM1036ScaletHawkDamage;

    public float MissileDropTimePlus; //미사일 드랍 시간 강화용
    public float AirStrikeCoolTime; //함선 폭격 쿨타임
    public float AirStrikeCoolTimeRunning; //함선 폭격 쿨타임 가동

    [Header("폭격지원 좌표 및 프리팹")]
    public GameObject ShipMissile;
    public Transform ShipMissilePos1;
    public Transform ShipMissilePos2;
    public Transform ShipMissilePos3;
    public Transform ShipMissilePos4;
    public GameObject Laiser;
    public Transform LaiserPos;
    public Transform LaiserVehiclePos;
    public GameObject BigMissile;
    public Transform MissilePos;

    [Header("폭격지원 스위치")]
    public bool LaserGuiding = false;
    public bool Reload;
    private bool missileWating = false;

    [Header("탑승 차량 호출")]
    public bool isRobotCall = false;
    public GameObject RobotLandingDotPrefab;
    private int VehicleItem;
    public float VehicleItemCool;
    private float VehicleItemTime;
    public bool MBCA79Entering = false;
    public bool CallBackComplete = false; //차량을 함선으로 귀환시킬 때 다시 호출할 수 있도록 조취
    public Transform EnterArea;

    [Header("미션 행동")]
    public bool MissionComplete = false;
    public bool MissionAction = false; //특정 비콘에 도달하면 해당 비콘 내로 자동으로 이동
    public int MissionNumber; //미션번호, 1 = 폭탄 설치
    public Vector3 EnteringMissionPos; //미션 비콘 좌표

    [Header("미션 폭탄")]
    public GameObject MissionBombPrefab;
    public GameObject MissionBombCountDownPrefab;
    public Transform MissionBombPos;

    [Header("미션 완료 후, 수송기 탑승")]
    public bool EnteringShuttle = false; //미션 완료 후, 수송기에 탑승하기 위해 이동하는 스위치
    public Transform EnteringShuttlePos; //셔틀 탑승 좌표

    [Header("사운드")]
    public AudioClip DashSound;
    public AudioClip Beep1;
    public AudioClip Beep2;
    public AudioClip Beep3;
    public AudioClip Beep4;

    void LeftOnRight() // 조이스틱 화면 좌우변동기능 로직 변경 
    {
        if (joyStick != null)
        {
            //이동 조이스틱
            if (joyStick.Horizontal < 0 && BeamFiring == false)
            {
                AnimationUIMove.GetComponent<Animator>().SetBool("Move, Move joystick", true);
                MoveController1.GetComponent<RotationUI>().isMove = true;
                MoveController2.GetComponent<RotationUI>().isMove = true;
                MoveController3.GetComponent<RotationUI>().isMove = true;
                ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = true;
                ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = true;
                if (GunjoyStick.Horizontal == 0)
                    transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (joyStick.Horizontal > 0 && BeamFiring == false)
            {
                AnimationUIMove.GetComponent<Animator>().SetBool("Move, Move joystick", true);
                MoveController1.GetComponent<RotationUI>().isMove = true;
                MoveController2.GetComponent<RotationUI>().isMove = true;
                MoveController3.GetComponent<RotationUI>().isMove = true;
                ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = true;
                ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = true;
                if (GunjoyStick.Horizontal == 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                AnimationUIMove.GetComponent<Animator>().SetBool("Move, Move joystick", false);
                MoveController1.GetComponent<RotationUI>().isMove = false;
                MoveController2.GetComponent<RotationUI>().isMove = false;
                MoveController3.GetComponent<RotationUI>().isMove = false;
                ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = false;
                ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = false;
            }
        }

        if (GunjoyStick != null)
        {
            //사격 조이스틱
            if (GunjoyStick.Horizontal <= -0.02f && BeamFiring == false)
            {
                AnimationUIAttack.GetComponent<Animator>().SetBool("Move, Attack joystick", true);
                AttackController1.GetComponent<RotationUI>().isMove = true;
                AttackController2.GetComponent<RotationUI>().isMove = true;
                AttackController3.GetComponent<RotationUI>().isMove = true;
                ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = true;
                ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (GunjoyStick.Horizontal >= 0.02f && BeamFiring == false)
            {
                AnimationUIAttack.GetComponent<Animator>().SetBool("Move, Attack joystick", true);
                AttackController1.GetComponent<RotationUI>().isMove = true;
                AttackController2.GetComponent<RotationUI>().isMove = true;
                AttackController3.GetComponent<RotationUI>().isMove = true;
                ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = true;
                ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                AnimationUIAttack.GetComponent<Animator>().SetBool("Move, Attack joystick", false);
                AttackController1.GetComponent<RotationUI>().isMove = false;
                AttackController2.GetComponent<RotationUI>().isMove = false;
                AttackController3.GetComponent<RotationUI>().isMove = false;
                ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = false;
                ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = false;
            }

            if (GunjoyStick.Horizontal <= -0.02f && GunjoyStick.Horizontal > -0.9f || GunjoyStick.Horizontal >= 0.02f && GunjoyStick.Horizontal < 0.9f) //자동으로 전방을 조준
            {
                if (FireJoystickType <= 1 && gunController.reloading == false)
                    GunHold();
            }
            else
            {
                if (gunController.SubGunTypeFront == 0)
                {
                    animator.SetFloat("GunHold", 0);
                }
                else if (gunController.SubGunTypeFront > 0)
                {
                    if (gunController.SubGunTypeFront == 1) //기관단총 앞
                        animator.SetFloat("SubGunHold front", 0); //CGD-27

                    if (gunController.SubGunTypeBack == 1) //기관단총 뒤
                        animator.SetFloat("SubGunHold back", 0); //CGD-27
                }
            }

            if (GunjoyStick.Horizontal <= -.9f && BeamFiring == false)
            {
                if (VehicleActive == false)
                    AnimationUIAttack.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 1);

                if (FireJoystickType == 1)
                {
                    gunController.isGun = true; //기본총
                    m3078Controller.isMiniGun = false;
                    hydra59Controller.isExplosion = false;
                }
                else if (FireJoystickType == 100)
                {
                    m3078Controller.isMiniGun = true; //강화 중화기(미니건)
                    gunController.isGun = false;
                    hydra59Controller.isExplosion = false;
                }
                else if (FireJoystickType == 1000)
                {
                    hydra59Controller.isExplosion = true; //체인지 중화기(산탄포)
                    gunController.isGun = false;
                    m3078Controller.isMiniGun = false;
                }
            }
            else if (GunjoyStick.Horizontal >= .9f && BeamFiring == false)
            {
                if (VehicleActive == false)
                    AnimationUIAttack.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 1);

                if (FireJoystickType == 1)
                {
                    gunController.isGun = true; //기본총
                    m3078Controller.isMiniGun = false;
                    hydra59Controller.isExplosion = false;
                }
                else if (FireJoystickType == 100)
                {
                    m3078Controller.isMiniGun = true; //강화 중화기(미니건)
                    gunController.isGun = false;
                    hydra59Controller.isExplosion = false;
                }
                else if (FireJoystickType == 1000)
                {
                    hydra59Controller.isExplosion = true; //체인지 중화기(산탄포)
                    gunController.isGun = false;
                    m3078Controller.isMiniGun = false;
                }
            }
            else
            {
                AnimationUIAttack.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 0);

                if (FireJoystickType == 1)
                    gunController.isGun = false;
                else if (FireJoystickType == 100)
                    m3078Controller.isMiniGun = false;
                else if (FireJoystickType == 1000)
                    hydra59Controller.isExplosion = false;
            }
        }
    }

    void GunHold()
    {
        if (gunController.SubGunTypeFront == 0)
        {
            if (gunController.GunType == 1)
                animator.SetFloat("GunHold", 1); //DT-37
            else if (gunController.GunType == 1000)
                animator.SetFloat("GunHold", 2); //DS-65
            else if (gunController.GunType == 2000)
                animator.SetFloat("GunHold", 3); //DP-9007
        }
        else if (gunController.SubGunTypeFront > 0)
        {
            if (gunController.SubGunTypeFront == 1) //기관단총 앞
                animator.SetFloat("SubGunHold front", 1); //CGD-27

            if (gunController.SubGunTypeBack == 1) //기관단총 뒤
                animator.SetFloat("SubGunHold back", 1); //CGD-27
        }
    }

    public void FireUp()
    {
        FireJoystick = false;
    }

    public void FireDown()
    {
        FireJoystick = true;
    }

    public void DashClick()
    {
        if (Dashing == false && DashCountInGame > 0 && UsingChangeWeapon == false)
            if (XtargetSpeed != 0 || YtargetSpeed != 0)
                if (HeavyWeaponOnline == 0 || HeavyWeaponOnline == 100)
                    isDash = true;
    }

    public void DashUp()
    {
        if (ClickDash == true)
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, Dash", false);
        ClickDash = false;
    }

    public void DashDown()
    {
        ClickDash = true;
        SoundManager.instance.SFXPlay2("Sound", Beep3);
        AnimationUIDash.GetComponent<Animator>().SetBool("Click, Dash", true);
    }

    public void DashEnter()
    {
        if (ClickDash == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep3);
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, Dash", true);
        }
    }

    public void DashExit()
    {
        if (ClickDash == true)
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, Dash", false);
    }

    public void RaiserBombClick()
    {
        if (missileWating == false && TakingVehicle == false)
            isRaiserBomb = true;
    }

    public void RaiserBombUp()
    {
        if (ClickAirStrike == true)
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Click, Air strike", false);
        ClickAirStrike = false;
    }

    public void RaiserBombDown()
    {
        ClickAirStrike = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        AnimationUIAirStrike.GetComponent<Animator>().SetBool("Click, Air strike", true);
    }

    public void RaiserBombEnter()
    {
        if (ClickAirStrike == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Click, Air strike", true);
        }
    }

    public void RaiserBombExit()
    {
        if (ClickAirStrike == true)
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Click, Air strike", false);
    }

    public void RobotCallUp()
    {
        if (ClickVehicleCall == true)
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Click, Vehicle call", false);
        ClickVehicleCall = false;
    }

    public void RobotCallDown()
    {
        ClickVehicleCall = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Click, Vehicle call", true);
    }

    public void RobotCallEnter()
    {
        if (ClickVehicleCall == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Click, Vehicle call", true);
        }
    }

    public void RobotCallExit()
    {
        if (ClickVehicleCall == true)
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Click, Vehicle call", false);
    }

    public void RobotCallClick()
    {
        if (CallBackComplete == false)
            if (VehicleItem > 0)
                isRobotCall = true;
    }

    //중화기 원격 제어
    public void HeavyWeaponUsing(int num)
    {
        HeavyWeaponOnline = num;
    }

    //움직임 원격 제어
    public void MovingStop(bool MoveStop)
    {
        if (MoveStop == true)
        {
            LaserGuiding = true;
        }
        else
        {
            LaserGuiding = false;
        }
    }

    //X축 반전 원격 제어
    public void XMoveStop(bool MoveXStop)
    {
        if (MoveXStop == true)
        {
            BeamFiring = true;
        }
        else
        {
            BeamFiring = false;
        }
    }

    public void ReloadTime(bool Reloading)
    {
        if (Reloading == true)
        {
            Reload = true;
        }
        else
        {
            Reload = false;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        activeMoveSpeed = Speed;
        objectManager = FindObjectOfType<ObjectManager>();
        gunController = FindObjectOfType<GunController>();
        m3078Controller = FindObjectOfType<M3078Controller>();
        hydra59Controller = FindObjectOfType<Hydra56Controller>();
        vM5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        objectManager.SupplyList.Add(gameObject);

        joyStickCopy = joyStick;
        GunjoyStickCopy = GunjoyStick;
        DashCountInGame = DashCount;

        PGM1036ScaletHawkDamage = UpgradeDataSystem.instance.PGM1036ScaletHawkDamage;
    }

    //실시간 업데이트
    public void Update()
    {
        if(player.hitPoints > 0)
        {
            if (VehicleActive == false && MissionComplete == false && EnteringShuttle == false && MissionAction == false)
            {
                SpeedBar.fillAmount = SpeedView / 70;

                LeftOnRight();
                MoveCharacter(); //상하좌우 키입력
                MoveSpeedUI(); //이동 컨트롤러의 스피드 계수
                StartCoroutine(UpdateState()); //상하좌우 동작 애니메이션
                Dash(); //대쉬
                UpdateDashText();
                RobotCallStart(); //탑승차량 호출
                RobotCallCool(); //탑승차량 쿨타임 함수
            }
            else if (MissionComplete == true)
            {
                StartCoroutine(UpdateState());
                SpeedBar.fillAmount = 0;
                movement.x = 0;
                movement.y = 0;

                //사격 중지
                gunController.isGun = false; //기본 총
                m3078Controller.isMiniGun = false; //강화 중화기
                FireJoystick = false; //체인지 중화기
            }
            else if (GameControlSystem.GetComponent<GameControlSystem>().MBCA79EnterOn == true)
            {
                StopMoveAction();
            }

            RaiserBomb(); //함선 폭격
            AirStrikeCool(); //함선 폭격 쿨타임

            //MBCA-79 탑승
            if (MBCA79Entering == true)
            {
                float distanceFromPlayer = Vector2.Distance(EnterArea.position, transform.position);

                movement.x = activeMoveSpeed;
                movement.y = activeMoveSpeed * 0.6f;
                LookAtVehicle();

                if (distanceFromPlayer > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, EnterArea.position, Speed * Time.deltaTime);
                    SpeedView = Speed;
                }
                else if (distanceFromPlayer <= 0.1f)
                {
                    MBCA79Entering = false;
                    SpeedBar.fillAmount = 0;
                    movement.x = 0;
                    movement.y = 0;
                    StartCoroutine(UpdateState());
                    GameObject.Find("Game Control").GetComponent<GameControlSystem>().MBCA79EnterOn = true;
                }
            }

            //임무 행동
            if (MissionAction == true || EnteringShuttle == true)
            {
                float distanceFromPlayer = 0;

                if (MissionAction == true) //임무 비콘으로 고정
                    distanceFromPlayer = Vector2.Distance(EnteringMissionPos, transform.position);
                else if (EnteringShuttle == true) //임무 완료 후, 수송기 탑승
                    distanceFromPlayer = Vector2.Distance(EnteringShuttlePos.position, transform.position);

                //행동 제어
                GameControlSystem GameControlSystem = GameObject.Find("Game Control").GetComponent<GameControlSystem>();
                animator.SetBool("Dash, Player", false);
                LookAtVehicle();
                StartCoroutine(UpdateState());
                SpeedBar.fillAmount = 0;

                //사격 중지
                gunController.isGun = false; //기본 총
                m3078Controller.isMiniGun = false; //강화 중화기
                FireJoystick = false; //체인지 중화기
                gunController.StopReload = true; //장전 캔슬
                joyStick = null;
                GunjoyStick = null;

                //중화기를 사용하지 않으면 탑승
                if (GameControlSystem.ChangeWeaponOnline <= 0 && GameControlSystem.inWeapon == false && GameControlSystem.isChangeWeapon == false)
                {
                    if (distanceFromPlayer > 0.1f)
                    {
                        if (MissionAction == true)
                            transform.position = Vector2.MoveTowards(transform.position, EnteringMissionPos, 5 * Time.deltaTime);
                        else if(EnteringShuttle == true)
                            transform.position = Vector2.MoveTowards(transform.position, EnteringShuttlePos.position, 5 * Time.deltaTime);
                        movement.x = 1;
                        movement.y = 1;
                    }
                    else if(distanceFromPlayer <= 0.1f)
                    {
                        if (MissionAction == true)
                        {
                            MissionAction = false;
                            if (MissionNumber == 1) //기함 폭탄 설치
                            {
                                MissionNumber = 0;
                                StartCoroutine(SettingMissionBomb());
                            }
                        }
                        else if (EnteringShuttle == true)
                        {
                            EnteringShuttle = false;
                            SpeedBar.fillAmount = 0;
                            movement.x = 0;
                            movement.y = 0;
                            StartCoroutine(UpdateState());
                            StartCoroutine(StartEnterShuttle());
                        }
                    }
                }
                else if (GameControlSystem.ChangeWeaponOnline > 0 || GameControlSystem.inWeapon == true)
                {
                    GameControlSystem.ChangeWeaponOnline = 0;
                    GameControlSystem.isChangeWeapon = false;
                    movement.x = 0;
                    movement.y = 0;
                }
            }
        }
    }

    //탑승차량 탑승시, 플레이어 액션 정지
    public void StopMoveAction()
    {
        SpeedBar.fillAmount = 0;
        movement.x = 0;
        movement.y = 0;
        StartCoroutine(UpdateState());
    }

    //탑승 차량 및 비콘을 향해 쳐다보기
    void LookAtVehicle()
    {
        if (MBCA79Entering == true)
        {
            if (transform.position.x < EnterArea.position.x)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else
                transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (MissionAction == true || EnteringShuttle == true)
        {
            if (MissionAction == true)
            {
                if (transform.position.x < EnteringMissionPos.x)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (EnteringShuttle == true)
            {
                if (transform.position.x < EnteringShuttlePos.position.x)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    //미션 폭탄 설치
    IEnumerator SettingMissionBomb()
    {
        animator.SetBool("Player setting mission bomb", true);
        yield return new WaitForSeconds(1.83f);
        GameObject MissionBomb = Instantiate(MissionBombPrefab, MissionBombPos.position, MissionBombPos.rotation);
        Instantiate(MissionBombCountDownPrefab, new Vector2(MissionBombPos.position.x, MissionBombPos.position.y + 1), Quaternion.identity);
        MissionBomb.GetComponent<Animator>().SetFloat("Step, Mission Bomb", 1);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("Player setting mission bomb", false);
        BombSettings.BombSetted(MissionBomb); //폭탄 설치 완료
        gunController.StopReload = false;
        joyStick = joyStickCopy;
        GunjoyStick = GunjoyStickCopy; //조이스틱 되돌리기
        Debug.Log("폭탄 설치 완료");
    }

    //셔틀 탑승
    IEnumerator StartEnterShuttle()
    {
        GameObject.Find("Game Control").GetComponent<GameControlSystem>().cinemachineVirtualCamera.Follow = null;
        transform.eulerAngles = new Vector3(0, 0, 0);
        animator.SetBool("Player entering shuttle", true);
        yield return new WaitForSeconds(0.75f);
        transform.position = new Vector2(2000, 2000);
        animator.SetBool("Player entering shuttle", false);
        GameObject.Find("Game play process/Vehicle/HA-767 Shoebill").GetComponent<Animator>().SetBool("Player Entering, HA-767", true);
        yield return new WaitForSeconds(0.416f);
        ReturnToFleet ReturnToFleet = GameObject.Find("Game play process/Vehicle/HA-767 Shoebill/Back to universe").GetComponent<ReturnToFleet>();
        StartCoroutine(ReturnToFleet.ChangeScene());
    }

    ////////////////////////////// 대쉬 //////////////////////////////
    private void UpdateDashText()
    {
        DashCountTxt.text = string.Format("{0}", DashCountInGame);
    }

    //대쉬
    void Dash()
    {
        if (isDash)
        {
            DashCountInGame--;
            isDash = false;
            Dashing = true;
            DashOn = true;
            UsingTask = true;
            SoundManager.instance.SFXPlay2("Sound", DashSound);
            StartCoroutine(dashAnimation()); //대쉬 애니메이션
            DashThruster(); //대쉬 부스터
            StartCoroutine(DAMAGED());
        }

        if (DashOn == true && activeMoveSpeed > 0)
        {
            activeMoveSpeed -= dashSpeed * 1.5f / dashSpeed;
        }

        //대쉬 쿨타임 회복을 위해 시간 가동
        if (DashCountInGame < DashCount)
        {
            AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle start, Dash", true);
            AnimationUIDash.GetComponent<Animator>().SetFloat("Cool time, Dash", 1 / DashCoolTime);
            DashTime += Time.deltaTime;

            if (DashCountInGame == 0)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
            }
        }

        //대쉬 시간이 찼을 경우, 대쉬를 하나 채운다.
        if (DashTime >= DashCoolTime)
        {
            DashTime = 0;
            DashCountInGame++;
            AnimationUIDash.GetComponent<Animator>().SetBool("View count complete, Dash", true);
            AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle count, Dash", true);
            Invoke("ViewCountComplete", 0.5f);

            if (DashCountInGame > 0)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
                Invoke("AfterEndCycle", 0.5f);
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
            }
            if (DashCountInGame == DashCount)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle complete, Dash", true);
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle start, Dash", false);
                AnimationUIDash.GetComponent<Animator>().SetFloat("Cool time, Dash", 0);
                Invoke("CycleComplete", 0.5f);
            }
        }
    }

    void AfterEndCycle()
    {
        AnimationUIDash.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
    }

    void CycleComplete()
    {
        AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle complete, Dash", false);
    }

    void ViewCountComplete()
    {
        AnimationUIDash.GetComponent<Animator>().SetBool("View count complete, Dash", false);
        AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle count, Dash", false);
    }

    //대쉬 부스터 출력
    void DashThruster()
    {
        GameObject MainBoost = Instantiate(mainBoostPrefab, mainBoostPos.position, mainBoostPos.rotation);
        GameObject SubBoost1 = Instantiate(subBoostPrefab1, subBoostPos1.position, subBoostPos1.rotation);
        GameObject SubBoost2 = Instantiate(subBoostPrefab2, subBoostPos2.position, subBoostPos2.rotation);
        GameObject BoostCloud = Instantiate(boostCloudPrefab, mainBoostPos.position, mainBoostPos.rotation);

        Destroy(MainBoost, 3);
        Destroy(SubBoost1, 3);
        Destroy(SubBoost2, 3);
        Destroy(BoostCloud, 3);
    }

    //대쉬 애니메이션 출력
    IEnumerator dashAnimation()
    {
        animator.SetBool("Dash, Player", true);
        AnimationUIMove.GetComponent<Animator>().SetBool("Dash, Move joystick", true);
        AnimationUIDash.GetComponent<Animator>().SetBool("Activated, Dash", true);
        activeMoveSpeed = dashSpeed;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Dash, Player", false);
        AnimationUIMove.GetComponent<Animator>().SetBool("Dash, Move joystick", false);
        AnimationUIDash.GetComponent<Animator>().SetBool("Activated, Dash", false);
        activeMoveSpeed = Speed;
        DashOn = false;
        UsingTask = false;
        yield return new WaitForSeconds(dashCooldown);
        Dashing = false;
    }

    //대쉬시, 플레이어 숄더어택 여부
    IEnumerator DAMAGED()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 6;
    }
    ////////////////////////////// 대쉬 //////////////////////////////

    ////////////////////////////// 미사일 폭격 //////////////////////////////
    //폭격 레이저
    void RaiserBomb()
    {
        if (isRaiserBomb || Input.GetKeyDown(KeyCode.Z))
        {
            if(AirStrikeCnt > 0 && missileWating == false)
            {
                AirStrikeCnt--;
                isRaiserBomb = false;
                missileWating = true;
                random = Random.Range(-4, 4); //미사일 위치 랜덤좌표 생성

                StartCoroutine(ShipMissileLaunch());
                if (VehicleActive == false)
                {
                    GameObject Dot = Instantiate(Laiser, LaiserPos.position, LaiserPos.rotation);
                    Dot.GetComponent<ContinuousBombardment>().MissileAmountPlus = MissileAmountPlus;
                    Dot.GetComponent<ContinuousBombardment>().MissileDropTimePlus = MissileDropTimePlus;
                    Dot.GetComponent<ContinuousBombardment>().damage = PGM1036ScaletHawkDamage;
                }
                else
                {
                    GameObject Dot = Instantiate(Laiser, LaiserVehiclePos.position, LaiserVehiclePos.rotation);
                    Dot.GetComponent<ContinuousBombardment>().MissileAmountPlus = MissileAmountPlus;
                    Dot.GetComponent<ContinuousBombardment>().MissileDropTimePlus = MissileDropTimePlus;
                    Dot.GetComponent<ContinuousBombardment>().damage = PGM1036ScaletHawkDamage;
                }
            }
        }
    }

    //함선 미사일 폭격 지원 연출
    IEnumerator ShipMissileLaunch()
    {
        for (int i = 0; i < MissileAmountPlus + 1; i++)
        {
            int MissileLaunchRandom = Random.Range(0, 4);

            if (MissileLaunchRandom == 0)
                Instantiate(ShipMissile, ShipMissilePos1.position, ShipMissilePos1.rotation);
            else if (MissileLaunchRandom == 1)
                Instantiate(ShipMissile, ShipMissilePos2.position, ShipMissilePos2.rotation);
            else if (MissileLaunchRandom == 2)
                Instantiate(ShipMissile, ShipMissilePos3.position, ShipMissilePos3.rotation);
            else
                Instantiate(ShipMissile, ShipMissilePos4.position, ShipMissilePos4.rotation);
            yield return new WaitForSeconds(MissileDropTimePlus);
        }
    }

    //함선 폭격 대기상태, 쿨타임 시작
    void AirStrikeCool()
    {
        if (AirStrikeCnt == 0)
        {
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time start, Air strike", true);
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time running, Air strike", true);
            AnimationUIAirStrike.GetComponent<Animator>().SetFloat("Cool time, Air strike", 1 / AirStrikeCoolTime);
            AirStrikeCoolTimeRunning += Time.deltaTime;
        }

        if (AirStrikeCoolTimeRunning > AirStrikeCoolTime)
        {
            AirStrikeCoolTimeRunning = 0;
            missileWating = false;
            AirStrikeCnt++;
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time end, Air strike", true);
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time cycle count, Air strike", true);
            Invoke("AfterEndCycleAir", 0.5f);
            Invoke("ViewCountCompleteAir", 0.5f);
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time start, Air strike", false);
            AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time running, Air strike", false);
            AnimationUIAirStrike.GetComponent<Animator>().SetFloat("Cool time, Air strike", 0);
        }
    }

    void AfterEndCycleAir()
    {
        AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time end, Air strike", false);
    }

    void ViewCountCompleteAir()
    {
        AnimationUIAirStrike.GetComponent<Animator>().SetBool("Cool time cycle count, Air strike", false);
    }

    void BigRaiserBomb()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (Reload == false && missileWating == false && HeavyWeaponOnline == 0)
            {
                //Debug.Log("레이저~~~");
                //체인지 스킬 제한 목록
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<UGG98Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<MEAGController>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<ArthesL775Controller>().XMoveStop(true); //체인지 스킬 사용 전달

                GetComponent<VM5GrenadeController>().XMoveStop(true); //VM-5 수류탄 스킬 사용 전달

                UsingTask = true;
                missileWating = true;
                LaserGuiding = true;
                StartCoroutine(BigLaserGuidAction());
                Instantiate(Laiser, LaiserPos.position, LaiserPos.rotation);

                StartCoroutine(BigDropMissle()); //레이저포인트 주위로 미사일 생성 후 폭격 
            }
        }
    }

    //레이저포인트 주위로 미사일 생성 후 폭격
    IEnumerator BigDropMissle()
    {
        //Debug.Log("-폭격-");
        yield return new WaitForSeconds(0.05f);
        Instantiate(BigMissile, MissilePos.position, MissilePos.rotation);
    }

    IEnumerator BigLaserGuidAction()
    {
        animator.SetBool("isWalking", false);
        GetComponent<GunController>().LaserGuiding(true); //레이져 지시 상태 전달
        animator.SetBool("Laser site, Player", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Laser site, Player", false);
        LaserGuiding = false;
        UsingTask = false;
        GetComponent<GunController>().LaserGuiding(false); //레이져 지시 상태 전달
        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().XMoveStop(false); //VM-5 수류탄 스킬 사용 해제 전달
    }

    void CannonRaiserBomb()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (Reload == false && missileWating == false && HeavyWeaponOnline == 0)
            {
                //Debug.Log("레이저~~~");
                //체인지 스킬 제한 목록
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<UGG98Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<MEAGController>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<ArthesL775Controller>().XMoveStop(true); //체인지 스킬 사용 전달

                GetComponent<VM5GrenadeController>().XMoveStop(true); //VM-5 수류탄 스킬 사용 전달

                UsingTask = true;
                missileWating = true;
                LaserGuiding = true;
                StartCoroutine(CannonLaserGuidAction());
                Instantiate(Laiser, LaiserPos.position, LaiserPos.rotation);

                StartCoroutine(CannonDropMissle()); //레이저포인트 주위로 미사일 생성 후 폭격 
            }
        }
    }

    //레이저포인트 주위로 미사일 생성 후 폭격
    IEnumerator CannonDropMissle()
    {
        //Debug.Log("-폭격-");
        yield return new WaitForSeconds(0.03f);
        GameObject Cannon = objectManager.CannonDropPool();
        Cannon.transform.position = MissilePos.position + Vector3.down * random;


        GameObject Cannon1 = objectManager.CannonDropPool();
        Cannon1.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;


        GameObject Cannon2 = objectManager.CannonDropPool();
        Cannon2.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        yield return new WaitForSeconds(0.06f);
        GameObject Cannon3 = objectManager.CannonDropPool();
        Cannon3.transform.position = MissilePos.position + Vector3.left / random + Vector3.up * random;


        GameObject Cannon4 = objectManager.CannonDropPool();
        Cannon4.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon5 = objectManager.CannonDropPool();
        Cannon5.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        yield return new WaitForSeconds(0.09f);
        GameObject Cannon6 = objectManager.CannonDropPool();
        Cannon6.transform.position = MissilePos.position + Vector3.left / random / 1.5f + Vector3.up * random;


        GameObject Cannon7 = objectManager.CannonDropPool();
        Cannon7.transform.position = MissilePos.position + Vector3.right * random / 1.5f + Vector3.up * random;


        GameObject Cannon8 = objectManager.CannonDropPool();
        Cannon8.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        yield return new WaitForSeconds(0.12f);
        GameObject Cannon9 = objectManager.CannonDropPool();
        Cannon9.transform.position = MissilePos.position + Vector3.left / random + Vector3.up * random;


        GameObject Cannon10 = objectManager.CannonDropPool();
        Cannon10.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon11 = objectManager.CannonDropPool();
        Cannon11.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        yield return new WaitForSeconds(0.15f);
        GameObject Cannon12 = objectManager.CannonDropPool();
        Cannon12.transform.position = MissilePos.position + Vector3.left * random * 1.5f + Vector3.up * random;


        GameObject Cannon13 = objectManager.CannonDropPool();
        Cannon13.transform.position = MissilePos.position + Vector3.left / random + Vector3.up * random;

        GameObject Cannon14 = objectManager.CannonDropPool();
        Cannon14.transform.position = MissilePos.position + Vector3.right * random * 1.5f + Vector3.up * random;

        yield return new WaitForSeconds(0.18f);
        GameObject Cannon15 = objectManager.CannonDropPool();
        Cannon15.transform.position = MissilePos.position + Vector3.left / random + Vector3.up * random;


        GameObject Cannon16 = objectManager.CannonDropPool();
        Cannon16.transform.position = MissilePos.position + Vector3.right * random * 1.5f + Vector3.up * random;


        GameObject Cannon17 = objectManager.CannonDropPool();
        Cannon17.transform.position = MissilePos.position + Vector3.left * random * 1.5f + Vector3.up * random;

        yield return new WaitForSeconds(0.21f);
        GameObject Cannon18 = objectManager.CannonDropPool();
        Cannon18.transform.position = MissilePos.position + Vector3.down * random;


        GameObject Cannon19 = objectManager.CannonDropPool();
        Cannon19.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon20 = objectManager.CannonDropPool();
        Cannon20.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        yield return new WaitForSeconds(0.24f);
        GameObject Cannon21 = objectManager.CannonDropPool();
        Cannon21.transform.position = MissilePos.position + Vector3.down * random;


        GameObject Cannon22 = objectManager.CannonDropPool();
        Cannon22.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;


        GameObject Cannon23 = objectManager.CannonDropPool();
        Cannon23.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        yield return new WaitForSeconds(0.27f);
        GameObject Cannon24 = objectManager.CannonDropPool();
        Cannon24.transform.position = MissilePos.position + Vector3.right * random * 1.5f + Vector3.up * random;


        GameObject Cannon25 = objectManager.CannonDropPool();
        Cannon25.transform.position = MissilePos.position + Vector3.left * random / 1.5f + Vector3.up * random;


        GameObject Cannon26 = objectManager.CannonDropPool();
        Cannon26.transform.position = MissilePos.position + Vector3.down * random * 1.5f;

        yield return new WaitForSeconds(0.3f);
        GameObject Cannon27 = objectManager.CannonDropPool();
        Cannon27.transform.position = MissilePos.position + Vector3.down * random / 1.5f;

        GameObject Cannon28 = objectManager.CannonDropPool();
        Cannon28.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon29 = objectManager.CannonDropPool();
        Cannon29.transform.position = MissilePos.position + Vector3.right * random * 1.5f + Vector3.up * random;


        GameObject Cannon30 = objectManager.CannonDropPool();
        Cannon30.transform.position = MissilePos.position + Vector3.left * random / 1.5f + Vector3.up * random;

        yield return new WaitForSeconds(0.33f);
        GameObject Cannon31 = objectManager.CannonDropPool();
        Cannon31.transform.position = MissilePos.position + Vector3.down * random;


        GameObject Cannon32 = objectManager.CannonDropPool();
        Cannon32.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon33 = objectManager.CannonDropPool();
        Cannon33.transform.position = MissilePos.position + Vector3.right * random * 1.5f + Vector3.up * random;


        GameObject Cannon34 = objectManager.CannonDropPool();
        Cannon34.transform.position = MissilePos.position + Vector3.left * random * 1.5f + Vector3.up * random;

        yield return new WaitForSeconds(0.36f);
        GameObject Cannon35 = objectManager.CannonDropPool();
        Cannon35.transform.position = MissilePos.position + Vector3.down * random;


        GameObject Cannon36 = objectManager.CannonDropPool();
        Cannon36.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;


        GameObject Cannon37 = objectManager.CannonDropPool();
        Cannon37.transform.position = MissilePos.position + Vector3.right * random / 1.5f + Vector3.up * random;


        GameObject Cannon38 = objectManager.CannonDropPool();
        Cannon38.transform.position = MissilePos.position + Vector3.left * random / 1.5f + Vector3.up * random;

        //yield return new WaitForSeconds(0.42f);
        //GameObject Cannon39 = objectManager.CannonDropPool();
        //Cannon39.transform.position = MissilePos.position;

        //yield return new WaitForSeconds(0.42f);
        //GameObject Cannon40 = objectManager.CannonDropPool();
        //Cannon40.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.45f);
        //GameObject Cannon41 = objectManager.CannonDropPool();
        //Cannon41.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.45f);
        //GameObject Cannon42 = objectManager.CannonDropPool();
        //Cannon42.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.45f);
        //GameObject Cannon43 = objectManager.CannonDropPool();
        //Cannon43.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.48f);
        //GameObject Cannon44 = objectManager.CannonDropPool();
        //Cannon44.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.48f);
        //GameObject Cannon45 = objectManager.CannonDropPool();
        //Cannon45.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.48f);
        //GameObject Cannon46 = objectManager.CannonDropPool();
        //Cannon46.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.51f);
        //GameObject Cannon47 = objectManager.CannonDropPool();
        //Cannon47.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.51f);
        //GameObject Cannon48 = objectManager.CannonDropPool();
        //Cannon48.transform.position = MissilePos.position + Vector3.right * random + Vector3.up * random;

        //yield return new WaitForSeconds(0.51f);
        //GameObject Cannon49 = objectManager.CannonDropPool();
        //Cannon49.transform.position = MissilePos.position + Vector3.left * random + Vector3.up * random;
    }

    IEnumerator CannonLaserGuidAction()
    {
        animator.SetBool("isWalking", false);
        GetComponent<GunController>().LaserGuiding(true); //레이져 지시 상태 전달
        animator.SetBool("Laser site, Player", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Laser site, Player", false);
        LaserGuiding = false;
        UsingTask = false;
        GetComponent<GunController>().LaserGuiding(false); //레이져 지시 상태 전달
        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().XMoveStop(false); //VM-5 수류탄 스킬 사용 해제 전달
    }
    ////////////////////////////// 미사일 폭격 //////////////////////////////

    //상하좌우 키입력
    public void MoveCharacter()
    {
        if (LaserGuiding == false && joyStick != null)
        {
            if (HeavyWeaponOnline == 0 || HeavyWeaponOnline == 100)
            {
                XtargetSpeed = joyStick.Horizontal * activeMoveSpeed;
                YtargetSpeed = joyStick.Vertical * activeMoveSpeed;

                movement.x = XtargetSpeed;
                movement.y = YtargetSpeed * 0.6f;

                transform.position += Vector3.right * XtargetSpeed * Time.deltaTime;
                transform.position += Vector3.up * YtargetSpeed * 0.6f * Time.deltaTime;
            }

            else if (HeavyWeaponOnline == 1 || HeavyWeaponOnline == 2 || HeavyWeaponOnline == 3 || HeavyWeaponOnline == 50 || HeavyWeaponOnline == 51)
            {
                XtargetSpeed = joyStick.Horizontal * activeMoveSpeed;
                YtargetSpeed = joyStick.Vertical * activeMoveSpeed;

                movement.x = XtargetSpeed * 0.7f;
                movement.y = YtargetSpeed * 0.4f;

                transform.position += Vector3.right * XtargetSpeed * 0.7f * Time.deltaTime;
                transform.position += Vector3.up * YtargetSpeed * 0.4f * Time.deltaTime;
            }
        }
    }

    void MoveSpeedUI()
    {
        if (joyStick != null)
        {
            if (joyStick.Horizontal > 0 && joyStick.Vertical > 0)
            {
                if (joyStick.Horizontal > joyStick.Vertical)
                    SpeedView = XtargetSpeed;
                else
                    SpeedView = YtargetSpeed;
            }
            else if (joyStick.Horizontal > 0 && joyStick.Vertical < 0)
            {
                if (joyStick.Horizontal > -joyStick.Vertical)
                    SpeedView = XtargetSpeed;
                else
                    SpeedView = YtargetSpeed * -1;
            }
            else if (joyStick.Horizontal < 0 && joyStick.Vertical < 0)
            {
                if (-joyStick.Horizontal > -joyStick.Vertical)
                    SpeedView = XtargetSpeed * -1;
                else
                    SpeedView = YtargetSpeed * -1;
            }
            else if (joyStick.Horizontal < 0 && joyStick.Vertical > 0)
            {
                if (-joyStick.Horizontal > joyStick.Vertical)
                    SpeedView = XtargetSpeed * -1;
                else
                    SpeedView = YtargetSpeed;
            }
            else
                SpeedView = 0;
        }
    }

    //상하좌우 동작 애니메이션, 중화기 사용시 자세 전환 포함
    IEnumerator UpdateState()
    {
        if (HeavyWeaponOnline == 0) //기본 상태
        {
            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                if (SubMachineGunOnline == false)
                {
                    animator.SetBool("isWalking", false);
                    animator.SetFloat("Move Type", 1f);
                }
                else
                {
                    animator.SetBool("subMachineGun idle", false);
                    animator.SetFloat("Move Type", 3000f);
                }
                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1.2f);
                else //전진
                    animator.SetFloat("Move speed", 1.2f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                if (SubMachineGunOnline == false)
                {
                    animator.SetBool("isWalking", false);
                    animator.SetFloat("Move Type", 1f);
                }
                else
                {
                    animator.SetBool("subMachineGun idle", false);
                    animator.SetFloat("Move Type", 3000f);
                }
                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1.2f);
                else //전진
                    animator.SetFloat("Move speed", -1.2f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0)) //움직임 벡터가 여전히 0에 있는지 확인한다. 즉, 플레이어 동작 입력이 존재하지 않는 경우이다.
            {
                if (SubMachineGunOnline == false)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetFloat("Move Type", 0);
                }
                else
                {
                    animator.SetBool("subMachineGun idle", true);
                    animator.SetFloat("Move Type", 0);
                }
            }
        }
        else if (HeavyWeaponOnline == 1) //일반 중화기
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 3f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1f);
                else //전진
                    animator.SetFloat("Move speed", 1f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 3f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1f);
                else //전진
                    animator.SetFloat("Move speed", -1f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                animator.SetFloat("Move Type", 2f);
        }

        else if (HeavyWeaponOnline == 2) //MEAG
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 5f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1f);
                else //전진
                    animator.SetFloat("Move speed", 1f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 5f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1f);
                else //전진
                    animator.SetFloat("Move speed", -1f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                animator.SetFloat("Move Type", 4f);
        }

        else if (HeavyWeaponOnline == 3) //Arthes L-775 레이져
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 7f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1f);
                else //전진
                    animator.SetFloat("Move speed", 1f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 7f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1f);
                else //전진
                    animator.SetFloat("Move speed", -1f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                animator.SetFloat("Move Type", 6f);
        }

        else if (HeavyWeaponOnline == 50) //M3078
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 101f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1f);
                else //전진
                    animator.SetFloat("Move speed", 1f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 101f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1f);
                else //전진
                    animator.SetFloat("Move speed", -1f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                animator.SetFloat("Move Type", 100f);
        }

        else if (HeavyWeaponOnline == 51) //ASC 365
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 103f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", -1f);
                else //전진
                    animator.SetFloat("Move speed", 1f);
            }
            else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
            {
                animator.SetFloat("Move Type", 103f);

                if (v1.x > transform.position.x) //후진
                    animator.SetFloat("Move speed", 1f);
                else //전진
                    animator.SetFloat("Move speed", -1f);
            }
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                animator.SetFloat("Move Type", 102f);
        }

        else if (HeavyWeaponOnline == 100) //수류탄
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("subMachineGun idle", false);

            if (StartAction == false)
            {
                if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0)
                    animator.SetFloat("Move Type", 1001f);
                else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0)
                    animator.SetFloat("Move Type", 1001f);
                if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                    animator.SetFloat("Move Type", 1000f);
            }

            else
            {
                Vector3 v1 = transform.position;
                yield return new WaitForSeconds(0.1f);

                if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
                {
                    animator.SetFloat("Move Type", 1001f, 0.1f, Time.deltaTime);

                    if (v1.x > transform.position.x) //후진
                        animator.SetFloat("Move speed", -1f);
                    else //전진
                        animator.SetFloat("Move speed", 1f);
                }
                else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
                {
                    animator.SetFloat("Move Type", 1001f, 0.1f, Time.deltaTime);

                    if (v1.x > transform.position.x) //후진
                        animator.SetFloat("Move speed", 1f);
                    else //전진
                        animator.SetFloat("Move speed", -1f);
                }
                if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
                    animator.SetFloat("Move Type", 1000f, 0.1f, Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DashOn == true)
        {
            //카오티-자이오스 4
            if (collision is CircleCollider2D && collision.gameObject.tag == "Kantakri, Kaoti-Jaios 4" && isDash == true || collision is CircleCollider2D && collision.gameObject.tag == "Kantakri, Kaoti-Jaios 4 Armor" && isDash == true)
            {
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                StartCoroutine(kaotiJaios4.DamageCharacter(dashDamage, 0.0f));
                kaotiJaios4.Hurt(collision.gameObject.transform.position);
            }
            if (collision is CircleCollider2D && collision.gameObject.tag == "Kantakri, Kaoti-Jaios 4 Spear" && isDash == true)
            {
                KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4Spear 스크립트 불러오기
                StartCoroutine(KaotiJaios4Spear.DamageCharacter(5, 0.0f));
                KaotiJaios4Spear.Hurt(collision.gameObject.transform.position);
            }

            //타이카-라이-쓰로트로 1
            if (collision is CircleCollider2D && collision.gameObject.tag == "Kantakri, Taika-Lai-Throtro 1" && isDash == true)
            {
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(dashDamage, 0.0f));
                TaikaLaiThrotro1.Hurt(collision.gameObject.transform.position);
            }
            //타이카-라이-쓰로트로 1 대전차형
            if (collision is CircleCollider2D && collision.gameObject.tag == "Kantakri, Taika-Lai-Throtro 1 Plasma" && isDash == true)
            {
                Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(dashDamage, 0.0f));
                Health2TaikaLaiThrotro1.Hurt(collision.gameObject.transform.position);
            }

            //아트로-크로스파 390
            if (collision.gameObject.tag == "Atro-Crossfa 390" && isDash == true)
            {
                HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                StartCoroutine(HealthAtroCrossfa.DamageCharacter(dashDamage, 0.0f));
                HealthAtroCrossfa.Hurt(collision.gameObject.transform.position);
            }

            //감염자
            if (collision is CapsuleCollider2D && collision.gameObject.tag == "Infector, Standard" && isDash == true)
            {
                if (gameObject.activeSelf == true)
                {
                    HealthInfector HealthInfector = collision.gameObject.GetComponent<HealthInfector>(); //HealthInfector 스크립트 불러오기
                    StartCoroutine(HealthInfector.DamageCharacter(dashDamage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    HealthInfector.Hurt(collision.gameObject.transform.position);
                }
            }

            //슬로리어스
            if (collision.gameObject.layer == 12 || collision.gameObject.layer == 27)
            {
                //애이소 시이오셰어(앨리트)
                //몸통
                if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                {
                    if (collision is CircleCollider2D) //몸통
                    {
                        if (gameObject.activeSelf == true && isDash == true)
                        {
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(dashDamage, 0.0f));
                            healthAsoShiioshare.Hurt(collision.gameObject.transform.position);
                        }
                    }
                }
                if (collision.CompareTag("Shield"))
                {
                    if (collision is CircleCollider2D) //방어막
                    {
                        if (gameObject.activeSelf == true && isDash == true)
                        {
                            ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //타격 부위의 부모 오브젝트의 ShieldAsoShiioshare 스크립트 불러오기
                            shieldAsoShiioshare.ShieldDamageExplosion(true);
                            StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(dashDamage, 0.0f));
                            shieldAsoShiioshare.Hurt(collision.gameObject.transform.position);
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            activeMoveSpeed = Speed;
        }
    }

    void RobotCallStart()
    {
        if (CallBackComplete == false)
        {
            if (VehicleItem > 0)
            {
                if (isRobotCall)
                {
                    VehicleItem--;
                    SoundManager.instance.SFXPlay2("Sound", Beep2);
                    SoundManager.instance.SFXPlay2("Sound", Beep4);
                    AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Activated, Vehicle call", true);
                    AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Dropping, Vehicle call", true);
                    isRobotCall = false;
                    CallBackComplete = true;
                    SpawnSite();
                }
            }
        }
    }

    //랜덤 좌표
    void SpawnSite()
    {
        Vector3 basePosition = transform.position;

        float posX = basePosition.x + Random.Range(-5, 5);
        float posY = basePosition.y + Random.Range(-5, 5);

        RobotLandingDotPrefab.transform.position = new Vector3(posX, posY, 0);
        RobotLandingDotPrefab.GetComponent<VehicleCall>().StratCall();
        RobotLandingDotPrefab.GetComponent<VehicleCall>().DropType = 0;
    }

    void RobotCallCool()
    {
        if (VehicleItem == 0 && CallBackComplete == false)
        {
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time start, Vehicle call", true);
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time running, Vehicle call", true);
            AnimationUIVehicleCall.GetComponent<Animator>().SetFloat("Cool time, Vehicle call", 1 / VehicleItemCool);
            VehicleItemTime += Time.deltaTime;
        }

        if (VehicleItemTime > VehicleItemCool)
        {
            VehicleItemTime = 0;
            VehicleItem++;
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time end, Vehicle call", true);
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time cycle count, Vehicle call", true);
            Invoke("AfterEndCycleVehicle", 0.5f);
            Invoke("ViewCountCompleteVehicle", 0.5f);
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time start, Vehicle call", false);
            AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time running, Vehicle call", false);
            AnimationUIVehicleCall.GetComponent<Animator>().SetFloat("Cool time, Vehicle call", 0);
        }
    }

    void AfterEndCycleVehicle()
    {
        AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time end, Vehicle call", false);
    }

    void ViewCountCompleteVehicle()
    {
        AnimationUIVehicleCall.GetComponent<Animator>().SetBool("Cool time cycle count, Vehicle call", false);
    }
}