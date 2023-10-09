using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    ScoreManager scoreManager;
    RobotPlayer robotPlayer;
    GameControlSystem GameControlSystem;
    public RegdollControllerPlayer RegdollControllerPlayer;

    public GameObject AnimationUIMove;
    public GameObject AnimationUIAttack;
    public GameObject AnimationUIHpStore;
    public GameObject AnimationUIHealthBar;
    public GameObject AnimationUISubWeapon;
    public GameObject BattleRobotUI;

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
    public Image VehicleSubWeaponActive;
    public Image VehicleDashActive;

    public float hitPoints;
    public float hitPoints2;
    float armor;
    public int Ricochet;
    private float HitAction;

    public Text Hp;

    public Image HealthBarColor1; //체력 게이지바 색깔 Fill
    public Image HealthBarColor2;
    public Image HealthBarColor3; //체력 프레임
    public Image HealthBarColor4; //체력 프레임2
    public Slider HealthBar1; //체력 게이지바
    public Slider HealthBar2;
    public Color HealthBarNormalVehicle;
    public Color HealthBarNormal;
    public Color HealthBarCaution;
    public Color HealthBarDanger;
    public Color HealthBarNormalVehicleFrame;
    public Color HealthBarNormalFrame;
    public Color HealthBarCautionFrame;
    public Color HealthBarDangerFrame;

    public Image HealthBarCircutColor1; //체력바 회로도
    public Image HealthBarCircutColor2;
    public Image HealthBarCircutColor3;
    public Image HealthBarCircutColor4;
    public Color CircutNormal1;
    public Color CircutNormal2;
    public Color CircutCaution1;
    public Color CircutCaution2;
    public Color CircutDanger1;
    public Color CircutDanger2;

    public Image HealthBarDamageColor1; //체력바 데미지 표시
    public Image HealthBarDamageColor2;
    private bool ActivateDamageColor = false;

    public Slider HealthBarOutline1; //체력바의 이동흔적을 먼저 표시해주는 체력바 그림자
    public Slider HealthBarOutline2;
    public Slider HealthBarOutline3;
    public Slider HealthBarOutline4;
    private bool HealthMoveDamage;
    private bool HealthMoveStore;
    private bool HealthStoreMove; //체력바가 데미지를 받으면 가장 먼저 움직이며, 회복했을 때에는 가장 늦게 움직이도록 조취
    public bool HealthBarStoreActive = false; //미션 시작할 때 체력바보다 먼저 차지 않도록 하기 위한 조취
    private float StoreTime;
    private float StartTime;

    public GameObject HPRestorePrefab;
    public Transform HPRestorePos;
    private float HealthBarSpeed;
    public bool HealthBarActive = true;
    private bool BarStore = false; //체력이 차는 연출이 다 끝나기도 전에 체력 회복을 시도했을 경우, 다른 중복된 연출을 불러오는 용도
    private bool ControllerStore = false; //BarStore와 같은 용도. 사용대상은 이동 및 공격 컨트롤러 애니메이션.
    public bool VehicleActive = false; //탑승차량 체력 가져오기. 차량에 탑승했을 때 해당 차량의 체력으로 체력바를 전환하기 위함
    public bool VehicleStart = false; //차량에 탑승했을 때 처음에 체력바가 빠르게 차오른 뒤, 이후에는 천천히 줄어들도록 설정
    public bool UsingTask = false;
    public bool isHp; // 버튼 입력용 bool값
    private bool Click;
    private bool Death = false;

    public int Medicine; //물약 개수
    public float MedicineCool;
    public int MedicineTimeAmount; //물약 쿨타임 (1번먹고 난 뒤 그 다음 물약 먹는 텀)
    public float HPFullPoint; //회복력
    private int MedicineCoolTimeCount = 1; //물약 쿨타임 채우기용

    //타격 빔 생성
    public GameObject ShockWaveTaken;
    public Transform BeamTakenPos;
    public int BeamDamageAction; //빔 효과 받기
    float TimeStemp;

    public AudioClip HP;
    public AudioClip Beep1;
    public AudioClip EnergyLow1;
    public AudioClip EnergyLow2;

    public void HPClick()
    {
        if (MedicineCoolTimeCount > 0)
        {
            if (hitPoints != maxHitPoints)
                isHp = true;
            if (hitPoints == maxHitPoints)
            {
                //Debug.Log("체력이 가득차 사용 불가합니다.");
            }
        }
    }

    public void HPUp()
    {
        if (Click == true)
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Click, Hp store", false);
        Click = false;
    }

    public void HPDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        AnimationUIHpStore.GetComponent<Animator>().SetBool("Click, Hp store", true);
    }

    public void HPEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Click, Hp store", true);
        }
    }

    public void HPExit()
    {
        if (Click == true)
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Click, Hp store", false);
    }

    //도탄 수치 받기
    public void RicochetNum(int num)
    {
        Ricochet = num;
    }

    //빔 데미지 받기
    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    //차량에 탑승했을 때 플레이어 버튼 활성화 관리
    public void ButtenOutAtVehicle(bool boolean)
    {
        if (boolean == true)
        {
            GetComponent<MissileHomming>().VehicleActive = true;
            SubWeaponActive.GetComponent<Animator>().SetFloat("Cool time, Sub weapon fire", 0);
            GetComponent<VM5GrenadeController>().VehicleActive = true;
            GrenadeActive.GetComponent<Animator>().SetFloat("Cool time, Grenade", 0);
            GetComponent<Movement>().VehicleActive = true;
            DashActive.GetComponent<Animator>().SetFloat("Cool time, Dash", 0);
            GetComponent<GunController>().VehicleActive = true;
            HPStoreActive.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);
            AmmoDropActive.GetComponent<Animator>().SetFloat("Cool time, Ammo drop", 0);
            GetComponent<HeavyWeaponSupport>().VehicleActive = true;
            WeaponDropActive.GetComponent<Animator>().SetFloat("Cool time, Weapon drop", 0);
        }
        else
        {
            GetComponent<MissileHomming>().VehicleActive = false;
            GetComponent<VM5GrenadeController>().VehicleActive = false;
            GetComponent<Movement>().VehicleActive = false;
            GetComponent<GunController>().VehicleActive = false;
            GetComponent<HeavyWeaponSupport>().VehicleActive = false;
        }
    }

    //차량에 탑승했을 때 차량 버튼 활성화 관리
    public void VehicleButtenOutAtVehicle(bool boolean)
    {
        if (boolean == true)
        {
            BattleRobotUI.GetComponent<OSEHSMiissileController>().VehicleActive = true;
            BattleRobotUI.GetComponent<RobotMove>().VehicleActive = true;
        }
        else
        {
            BattleRobotUI.GetComponent<OSEHSMiissileController>().VehicleActive = false;
            VehicleSubWeaponActive.GetComponent<Animator>().SetFloat("Cool time, OSEHS", 0);
            BattleRobotUI.GetComponent<RobotMove>().VehicleActive = false;
            VehicleDashActive.GetComponent<Animator>().SetFloat("Cool time, MBCA-79 dash", 0);
        }
    }

    //플레이어와 탑승차량 간의 체력바 전환
    public void HealthTransform(bool boolean)
    {
        if (boolean == true)
        {
            VehicleActive = true;
            HealthBar1.maxValue = robotPlayer.hitPoints2;
            HealthBar2.maxValue = robotPlayer.hitPoints2;
            HealthBarOutline1.maxValue = robotPlayer.hitPoints2;
            HealthBarOutline2.maxValue = robotPlayer.hitPoints2;
            HealthBarOutline3.maxValue = robotPlayer.hitPoints2;
            HealthBarOutline4.maxValue = robotPlayer.hitPoints2;
        }
        else
        {
            VehicleActive = false;
            HealthBar1.maxValue = hitPoints2;
            HealthBar2.maxValue = hitPoints2;
            HealthBarOutline1.maxValue = hitPoints2;
            HealthBarOutline2.maxValue = hitPoints2;
            HealthBarOutline3.maxValue = hitPoints2;
            HealthBarOutline4.maxValue = hitPoints2;
        }
    }

    //차량에 탑승된 플레이어가 데미지를 받았을 때 체력바가 데미지 작용을 하도록 조취
    public void VehicleHealthDamage()
    {
        Color color1 = HealthBarDamageColor1.color;
        Color color2 = HealthBarDamageColor2.color;
        color1.a = 1;
        color2.a = 1;
        HealthBarDamageColor1.color = color1;
        HealthBarDamageColor2.color = color2;
        ActivateDamageColor = true;

        StartCoroutine(HealthShadow());
        HealthStoreMove = false;
        HealthBarSpeed = startingHitPoints * 0.5f;
    }

    private void UpdateBulletText()
    {
        Hp.text = string.Format("{0}", Medicine);

    }

    public void Start()
    {
        //시작 시, 전투 데이터 정보를 가져오기
        startingHitPoints = UpgradeDataSystem.instance.HurricaneHitPoint;
        startingArmor = UpgradeDataSystem.instance.HurricaneHitArmor;

        HPFullPoint = startingHitPoints / 2.5f;
        maxHitPoints = startingHitPoints;
        HealthBarSpeed = startingHitPoints * 0.5f;
        hitPoints = startingHitPoints;
        armor = startingArmor;
        hitPoints2 = hitPoints;
        HealthBar1.maxValue = hitPoints2;
        HealthBar2.maxValue = hitPoints2;
        HealthBarOutline1.maxValue = hitPoints2;
        HealthBarOutline2.maxValue = hitPoints2;
        HealthBarOutline3.maxValue = hitPoints2;
        HealthBarOutline4.maxValue = hitPoints2;
        robotPlayer = FindObjectOfType<RobotPlayer>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void Update()
    {
        HP_Full(); //Hp 회복
        if (VehicleActive == false)
            HP_Cool(); //Hp회복 쿨타임
        UpdateBulletText();
        HealthBarCount(); //체력바가 자연스럽게 채워지는 연출
        ColorChanger(); //체력바 색깔 변경

        if (ActivateDamageColor == true)
        {
            Color color1 = HealthBarDamageColor1.color;
            Color color2 = HealthBarDamageColor2.color;

            color1.a -= 0.01f;
            color2.a -= 0.01f;
            HealthBarDamageColor1.color = color1;
            HealthBarDamageColor2.color = color2;

            if (color1.a == 0)
                ActivateDamageColor = false;
        }

        if (TimeStemp > 0)
            TimeStemp -= Time.deltaTime;

        if (TimeStemp < 0)
        {
            TimeStemp = 0;
            BeamDamageAction = 0; //레이져 무기에 타격받은 이후, 다른 무기 타격을 받았을 때 레이져 맞은 효과가 나타나지 않도록 하기 위한 조취
        }

        if (HitAction >= 0)
            HitAction -= Time.deltaTime;

        if (hitPoints < hitPoints2 * 0.2f)
        {
            AnimationUIMove.GetComponent<Animator>().SetBool("Rad heath, Move joystick", true);
            AnimationUIAttack.GetComponent<Animator>().SetBool("Rad heath, Attack joystick", true);
        }
        else
        {
            AnimationUIMove.GetComponent<Animator>().SetBool("Rad heath, Move joystick", false);
            AnimationUIAttack.GetComponent<Animator>().SetBool("Rad heath, Attack joystick", false);
        }
    }

    //체력바가 자연스럽게 채워지는 연출
    void HealthBarCount()
    {
        if (HealthBarActive == true)
        {
            //데미지를 입은 직후에만 체력바 이동
            if (HealthStoreMove == false)
            {
                if (VehicleActive == false)
                {
                    HealthBar1.value = Mathf.MoveTowards(HealthBar1.value, hitPoints, Time.deltaTime * HealthBarSpeed);
                    HealthBar2.value = Mathf.MoveTowards(HealthBar2.value, hitPoints, Time.deltaTime * HealthBarSpeed);
                }
                else
                {
                    if (VehicleStart == true)
                    {
                        HealthBar1.value = Mathf.MoveTowards(HealthBar1.value, robotPlayer.hitPoints, Time.deltaTime * robotPlayer.hitPoints);
                        HealthBar2.value = Mathf.MoveTowards(HealthBar2.value, robotPlayer.hitPoints, Time.deltaTime * robotPlayer.hitPoints);
                        if (HealthBar1.value == robotPlayer.hitPoints)
                            VehicleStart = false;
                        if (HealthBar2.value == robotPlayer.hitPoints)
                            VehicleStart = false;
                    }
                    else
                    {
                        HealthBar1.value = Mathf.MoveTowards(HealthBar1.value, robotPlayer.hitPoints, Time.deltaTime * HealthBarSpeed);
                        HealthBar2.value = Mathf.MoveTowards(HealthBar2.value, robotPlayer.hitPoints, Time.deltaTime * HealthBarSpeed);
                    }
                }

                if(HealthBarStoreActive == true) //미션 시작할 때 체력바보다 먼저 차지 않도록 하기 위한 조취
                {
                    if (VehicleActive == false)
                    {
                        HealthBarOutline1.value = hitPoints;
                        HealthBarOutline2.value = hitPoints;
                    }
                    else
                    {
                        HealthBarOutline1.value = robotPlayer.hitPoints;
                        HealthBarOutline2.value = robotPlayer.hitPoints;
                    }
                }
            }

            //미션 시작할 때 체력바가 다 채워지면 나머지 2개 체력바 그림자들이 맞춰진다.
            if (VehicleActive == false)
            {
                if (HealthBar1.value == hitPoints)
                {
                    if (StartTime == 0)
                    {
                        StartTime += Time.deltaTime;
                        HealthBarStoreActive = true;
                        HealthBarOutline1.value = HealthBar1.value;
                        HealthBarOutline2.value = HealthBar2.value;
                        HealthBarOutline3.value = HealthBar1.value;
                        HealthBarOutline4.value = HealthBar2.value;
                    }
                }
            }
            else
            {
                if (HealthBar1.value == robotPlayer.hitPoints)
                {
                    if (StartTime == 0)
                    {
                        StartTime += Time.deltaTime;
                        HealthBarStoreActive = true;
                        HealthBarOutline1.value = HealthBar1.value;
                        HealthBarOutline2.value = HealthBar2.value;
                        HealthBarOutline3.value = HealthBar1.value;
                        HealthBarOutline4.value = HealthBar2.value;
                    }
                }
            }

            //데미지를 입었을 경우
            if (HealthMoveDamage == true)
            {
                HealthBarOutline3.value = Mathf.MoveTowards(HealthBarOutline3.value, HealthBar1.value, Time.deltaTime * HealthBarSpeed);
                HealthBarOutline4.value = Mathf.MoveTowards(HealthBarOutline4.value, HealthBar2.value, Time.deltaTime * HealthBarSpeed);

                if (HealthBarOutline3.value == HealthBar1.value || HealthMoveStore == true)
                    HealthMoveDamage = false;
            }

            //체력 회복을 했을 경우
            if (HealthMoveStore == true)
            {
                //체력 회복 도중에 미리 2개 체력바 그림자를 미리 회복된 체력까지 올려둬서 회복 직후에 데미지를 입었을 때, 데미지 입은 영역까지 표시되도록 한다.
                if (HealthMoveDamage == false)
                {
                    if (StoreTime == 0)
                    {
                        StoreTime += Time.deltaTime;
                        if (VehicleActive == false)
                        {
                            HealthBarOutline1.value = hitPoints;
                            HealthBarOutline2.value = hitPoints;
                            HealthBarOutline3.value = hitPoints;
                            HealthBarOutline4.value = hitPoints;
                        }
                        else
                        {
                            HealthBarOutline1.value = robotPlayer.hitPoints;
                            HealthBarOutline2.value = robotPlayer.hitPoints;
                            HealthBarOutline3.value = robotPlayer.hitPoints;
                            HealthBarOutline4.value = robotPlayer.hitPoints;
                        }
                    }
                }

                //체력 회복 직후에 데미지를 입었을 경우, 회복 체력바 그림자를 체력 수치에 조정하면, 체력 회복보다 더 낮은 수치로 조정되기 때문에 데미지 체력바 그림자가 드러난다.
                if (HealthBarOutline1.value == HealthBar1.value || HealthMoveDamage == true)
                {
                    HealthMoveStore = false;
                    if (VehicleActive == false)
                    {
                        HealthBarOutline1.value = hitPoints;
                        HealthBarOutline2.value = hitPoints;
                    }
                    else
                    {
                        HealthBarOutline1.value = robotPlayer.hitPoints;
                        HealthBarOutline2.value = robotPlayer.hitPoints;
                    }
                }
            }
        }
        else //플레이어 체력바 비활성화(탑승차량에 탑승했을 때 필요함)
        {
            if (VehicleActive == false)
            {
                HealthBarStoreActive = false;
                StartTime = 0;
                HealthBar1.value = Mathf.MoveTowards(HealthBar1.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
                HealthBar2.value = Mathf.MoveTowards(HealthBar2.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
                HealthBarOutline1.value = Mathf.MoveTowards(HealthBarOutline1.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
                HealthBarOutline2.value = Mathf.MoveTowards(HealthBarOutline2.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
                HealthBarOutline3.value = Mathf.MoveTowards(HealthBarOutline3.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
                HealthBarOutline4.value = Mathf.MoveTowards(HealthBarOutline4.value, 0, Time.deltaTime * HealthBarSpeed * 1.1f);
            }
            else
            {
                HealthBarStoreActive = false;
                StartTime = 0;
                HealthBar1.value = Mathf.MoveTowards(HealthBar1.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
                HealthBar2.value = Mathf.MoveTowards(HealthBar2.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
                HealthBarOutline1.value = Mathf.MoveTowards(HealthBarOutline1.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
                HealthBarOutline2.value = Mathf.MoveTowards(HealthBarOutline2.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
                HealthBarOutline3.value = Mathf.MoveTowards(HealthBarOutline3.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
                HealthBarOutline4.value = Mathf.MoveTowards(HealthBarOutline4.value, 0, Time.deltaTime * (robotPlayer.hitPoints * 1.5f));
            }
        }
    }

    //체력바 색깔 변경
    void ColorChanger() 
    {
        if (VehicleActive == false)
        {
            if (hitPoints > hitPoints2 * 0.65f)
            {
                Color healthColor = HealthBarNormal;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarNormalFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutNormal1;
                Color CircutColor2 = CircutNormal2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 0);
            }
            else if (hitPoints < hitPoints2 * 0.65f && hitPoints > hitPoints2 * 0.2f)
            {
                Color healthColor = HealthBarCaution;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarCautionFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutCaution1;
                Color CircutColor2 = CircutCaution2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 1);
            }
            else if (hitPoints < hitPoints2 * 0.2f)
            {
                Color healthColor = HealthBarDanger;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarDangerFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutDanger1;
                Color CircutColor2 = CircutDanger2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 2);
            }
        }
        else
        {
            if (robotPlayer.hitPoints > robotPlayer.hitPoints2 * 0.65f)
            {
                Color healthColor = HealthBarNormalVehicle;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarNormalVehicleFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutNormal1;
                Color CircutColor2 = CircutNormal2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 0);
            }
            else if (robotPlayer.hitPoints < robotPlayer.hitPoints2 * 0.65f && robotPlayer.hitPoints > robotPlayer.hitPoints2 * 0.2f)
            {
                Color healthColor = HealthBarCaution;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarCautionFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutCaution1;
                Color CircutColor2 = CircutCaution2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 1);
            }
            else if (robotPlayer.hitPoints < robotPlayer.hitPoints2 * 0.2f)
            {
                Color healthColor = HealthBarDanger;
                HealthBarColor1.color = healthColor;
                HealthBarColor2.color = healthColor;
                Color healthColorFrame = HealthBarDangerFrame;
                HealthBarColor3.color = healthColorFrame;
                HealthBarColor4.color = healthColorFrame;
                Color CircutColor1 = CircutDanger1;
                Color CircutColor2 = CircutDanger2;
                HealthBarCircutColor1.color = CircutColor1;
                HealthBarCircutColor2.color = CircutColor2;
                HealthBarCircutColor3.color = CircutColor1;
                HealthBarCircutColor4.color = CircutColor2;
                AnimationUIHealthBar.GetComponent<Animator>().SetFloat("HP State, Health bar", 2);
            }
        }
    }

    IEnumerator HealthShadow()
    {
        yield return new WaitForSeconds(0.5f);
        if (HealthMoveStore == false)
            HealthMoveDamage = true;
    }

    IEnumerator HealthBarStoreMove()
    {
        HealthStoreMove = true;
        yield return new WaitForSeconds(0.5f);
        HealthStoreMove = false;
    }

    //Hp 회복
    void HP_Full()
    {
        if (isHp && MedicineCoolTimeCount > 0 && Medicine > 0)
        {
            isHp = false;

            if (Medicine <= 0)
            {
                //Debug.Log("물약이 다 떨어졌습니다");
                SoundManager.instance.SFXPlay("Sound", EnergyLow1);
                SoundManager.instance.SFXPlay("Sound", EnergyLow2);
                AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time start, Hp store", true);
                AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time running, Hp store", true);
            }
            else
            {
                if (MedicineCoolTimeCount > 0)
                {
                    SoundManager.instance.SFXPlay8("Sound", HP);
                    GameObject HPRestore = Instantiate(HPRestorePrefab, HPRestorePos.transform.position, HPRestorePos.transform.rotation);
                    Destroy(HPRestore, 10);
                    if (ControllerStore == false)
                        StartCoroutine(RestoreHP());
                    else
                        StartCoroutine(RestoreHP2());
                    if (BarStore == false)
                        StartCoroutine(RestoreHPBar());
                    else
                        StartCoroutine(RestoreHPBar2());
                    StartCoroutine(RestoreHPAnimation());
                    //Debug.Log("물약을 사용했습니다.");
                    MedicineCoolTimeCount--;
                    Medicine--;
                    hitPoints += HPFullPoint;
                    HealthMoveStore = true;
                    StoreTime = 0;
                    StartCoroutine(HealthBarStoreMove());

                    if (hitPoints >= maxHitPoints)
                    {
                        hitPoints = maxHitPoints;
                    }
                    MedicineCool = 0;
                }
                else if (MedicineCool < MedicineTimeAmount)
                {
                    //Debug.Log("물약 쿨이 차지 않았습니다.");
                }
            }
        }
    }

    IEnumerator RestoreHP()
    {
        ControllerStore = true;
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("Restore heath2, Move joystick") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("Restore heath2, Move joystick", false);
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("Restore heath2, Attack joystick") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("Restore heath2, Attack joystick", false);
        AnimationUIMove.GetComponent<Animator>().SetBool("Restore heath, Move joystick", true);
        AnimationUIAttack.GetComponent<Animator>().SetBool("Restore heath, Attack joystick", true);
        yield return new WaitForSeconds(1.5f);
        AnimationUIMove.GetComponent<Animator>().SetBool("Restore heath, Move joystick", false);
        AnimationUIAttack.GetComponent<Animator>().SetBool("Restore heath, Attack joystick", false);
        ControllerStore = false;
    }
    IEnumerator RestoreHP2()
    {
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("Restore heath, Move joystick") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("Restore heath, Move joystick", false);
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("Restore heath, Attack joystick") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("Restore heath, Attack joystick", false);
        AnimationUIMove.GetComponent<Animator>().SetBool("Restore heath2, Move joystick", true);
        AnimationUIAttack.GetComponent<Animator>().SetBool("Restore heath2, Attack joystick", true);
        yield return new WaitForSeconds(1.5f);
        AnimationUIMove.GetComponent<Animator>().SetBool("Restore heath2, Move joystick", false);
        AnimationUIAttack.GetComponent<Animator>().SetBool("Restore heath2, Attack joystick", false);
    }

    IEnumerator RestoreHPBar()
    {
        BarStore = true;
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("HP store2, Health bar") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store2, Health bar", false);
        AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store, Health bar", true);
        yield return new WaitForSeconds(1.83f);
        AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store, Health bar", false);
        BarStore = false;
    }
    IEnumerator RestoreHPBar2()
    {
        if (AnimationUIHealthBar.GetComponent<Animator>().GetBool("HP store, Health bar") == true)
            AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store, Health bar", false);
        AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store2, Health bar", true);
        yield return new WaitForSeconds(1.83f);
        AnimationUIHealthBar.GetComponent<Animator>().SetBool("HP store2, Health bar", false);
    }

    IEnumerator RestoreHPAnimation()
    {
        AnimationUIHpStore.GetComponent<Animator>().SetBool("Activated, Hp store", true);
        AnimationUIHpStore.GetComponent<Animator>().SetBool("View count complete, Hp store", true);
        yield return new WaitForSeconds(0.416f);
        AnimationUIHpStore.GetComponent<Animator>().SetBool("Activated, Hp store", false);
        yield return new WaitForSeconds(0.084f);
        AnimationUIHpStore.GetComponent<Animator>().SetBool("View count complete, Hp store", false);
    }

    //Hp회복 쿨타임
    void HP_Cool()
    {
        if (MedicineCoolTimeCount < 1)
        {
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time cycle start, Hp store", true);
            AnimationUIHpStore.GetComponent<Animator>().SetFloat("Cool time, Hp store", 1 / MedicineTimeAmount);
            MedicineCool += Time.deltaTime;
        }

        if (MedicineCool >= MedicineTimeAmount)
        {
            MedicineCool = 0;
            MedicineCoolTimeCount++;
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time cycle count, Hp store", true);
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time cycle start, Hp store", false);
            Invoke("ViewCountComplete", 0.5f);
        }

        if (Medicine <= 0)
        {
            AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time running, Hp store", true);
        }
    }

    void ViewCountComplete()
    {
        AnimationUIHpStore.GetComponent<Animator>().SetBool("Cool time cycle count, Hp store", false);
    }

    //플레이어가 원거리 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if (Death == false)
        {
            while (true)
            {
                if (Ricochet != 0)
                {
                    hitPoints = hitPoints - (damage - armor * 5);

                    if (HitAction <= 0)
                    {
                        HitAction = 0.4f;
                        StartCoroutine(DamageAction());
                    }

                    StartCoroutine(BeamAction());

                    Color color1 = HealthBarDamageColor1.color;
                    Color color2 = HealthBarDamageColor2.color;
                    color1.a = 1;
                    color2.a = 1;
                    HealthBarDamageColor1.color = color1;
                    HealthBarDamageColor2.color = color2;
                    ActivateDamageColor = true;

                    StartCoroutine(HealthShadow());
                    HealthStoreMove = false;

                    TimeStemp += 0.025f;

                    if (hitPoints <= float.Epsilon)
                    {
                        //KillCharacter();
                        Death = true;
                        UsingTask = true;
                        this.gameObject.layer = 0;
                        //GetComponent<Animator>().SetBool("Death, player", true);
                        gameObject.GetComponent<Movement>().enabled = false;
                        gameObject.GetComponent<GunController>().enabled = false;
                        gameObject.GetComponent<Hydra56Controller>().enabled = false;
                        gameObject.GetComponent<ArthesL775Controller>().enabled = false;
                        gameObject.GetComponent<UGG98Controller>().enabled = false;
                        gameObject.GetComponent<MEAGController>().enabled = false;
                        gameObject.GetComponent<MissileHomming>().enabled = false;
                        gameObject.GetComponent<VM5GrenadeController>().enabled = false;
                        gameObject.GetComponent<M3078Controller>().enabled = false;
                        gameObject.GetComponent<HeavyWeaponSupport>().enabled = false;
                        gameObject.GetComponent<SwichHeavyWeapon>().enabled = false;
                        DeathRagdoll();
                        break;
                    }

                    if (interval > float.Epsilon)
                    {
                        yield return new WaitForSeconds(interval);
                    }

                    else
                    {
                        break;
                    }
                }
                else if (Ricochet == 0 && Death == false)
                {
                    //Debug.Log("팅!");
                    break;
                }
            }
        }
    }

    //체력바 데미지 연출
    IEnumerator HealthBarDamageAction()
    {
        yield return new WaitForSeconds(0.02f);
        AnimationUIHealthBar.GetComponent<Animator>().SetBool("Damage Handle, Health bar", false);
    }

    //플레이어가 근접 타격을 받았을 때의 데미지 적용
    public IEnumerator NearDamageCharacter(int damage, float interval)
    {
        if (Death == false)
        {
            while (true)
            {
                hitPoints = hitPoints - (damage / armor);

                if (HitAction <= 0)
                {
                    HitAction = 0.4f;
                    StartCoroutine(DamageAction());
                }

                Color color1 = HealthBarDamageColor1.color;
                Color color2 = HealthBarDamageColor2.color;
                color1.a = 1;
                color2.a = 1;
                HealthBarDamageColor1.color = color1;
                HealthBarDamageColor2.color = color2;
                ActivateDamageColor = true;

                StartCoroutine(HealthShadow());
                HealthStoreMove = false;

                TimeStemp += 0.025f;

                if (hitPoints <= float.Epsilon)
                {
                    //KillCharacter();
                    Death = true;
                    UsingTask = true;
                    this.gameObject.layer = 0;
                    //GetComponent<Animator>().SetBool("Death, player", true);
                    gameObject.GetComponent<Movement>().enabled = false;
                    gameObject.GetComponent<GunController>().enabled = false;
                    gameObject.GetComponent<Hydra56Controller>().enabled = false;
                    gameObject.GetComponent<ArthesL775Controller>().enabled = false;
                    gameObject.GetComponent<UGG98Controller>().enabled = false;
                    gameObject.GetComponent<MEAGController>().enabled = false;
                    gameObject.GetComponent<MissileHomming>().enabled = false;
                    gameObject.GetComponent<VM5GrenadeController>().enabled = false;
                    gameObject.GetComponent<M3078Controller>().enabled = false;
                    gameObject.GetComponent<HeavyWeaponSupport>().enabled = false;
                    gameObject.GetComponent<SwichHeavyWeapon>().enabled = false;
                    DeathRagdoll();
                    break;
                }

                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }

                else
                {
                    break;
                }
            }
        }
    }

    //빔 공격 받았을 때의 시각효과 발생
    public IEnumerator BeamAction()
    {
        //Debug.Log(TimeStemp);
        if (BeamDamageAction == 1) //슬로리어스 쇼크 웨이브 타격 이펙트
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam1 = Instantiate(ShockWaveTaken, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public IEnumerator DamageAction()
    {
        int damageAction = Random.Range(1, 3); //1~2까지의 숫자를 출력

        if (damageAction == 1)
        {
            GetComponent<Animator>().SetBool("Taking damage1, player", true);
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().SetBool("Taking damage1, player", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Taking damage2, player", true);
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().SetBool("Taking damage2, player", false);
        }
    }

    void DeathRagdoll()
    {
        GameObject.Find("Play Control/Player/Guns").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Change heavy weapons").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Support heavy weapons").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Grenade").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Gun hand skin").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Gun hand skin").gameObject.SetActive(false);
        GameObject.Find("Play Control/Player/Delta Leader skin/Left Hand/Left Hand(stand)").gameObject.SetActive(true);

        RegdollControllerPlayer.ActiveRagdoll(); //RegdollControllerInfector스크립트의 ActiveRagdoll 메소드 호출
        GetComponent<DeathCallPlayer>().enabled = true; //레그돌 담당 신체 파트에다 죽음 신호 전달
        DeathRolling call = transform.Find("bone_1").GetComponent<DeathRolling>(); //죽을 때 신체 회전
        call.enabled = true;
        TraceX call1 = transform.Find("bone_1").GetComponent<TraceX>();
        call1.DeathTransformTime = true; //죽었을 때의 위치 좌표 계산 스위치
        call1.ShadowTime = 31536000;
        Invoke("Turn", 10);
    }

    void Turn()
    {
        GameControlSystem = FindObjectOfType<GameControlSystem>();
        GameControlSystem.cinemachineVirtualCamera.Follow = null;
        this.gameObject.SetActive(false);
    }
}