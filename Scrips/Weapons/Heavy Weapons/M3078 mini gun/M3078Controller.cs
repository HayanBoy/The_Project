using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class M3078Controller : MonoBehaviour
{
    public GameControlSystem GameControlSystem;
    Animator animator;
    Rigidbody2D rb2D;
    ArthesL775Controller arthesL775Controller;
    Hydra56Controller hydra56Controller;
    MEAGController meagController;
    UGG98Controller ugg98Controller;
    VM5GrenadeController vm5GrenadeController;
    Movement movement;
    GunController gunController;
    public GameObject GameControlScript;
    public ObjectManager objectManager;

    private Shake shake;

    //GameObject MiniGunWeapon;
    public GameObject WeaponSwapBtn;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public Image SwapActive;

    public int ViewSW06_Magazine; //인게임 탄창 내 탄약 표시기
    public int GunType; //기본 총 타입을 전달받기 위한 변수
    public int SubGunTypeFront = 0; //앞 기관단총 장착여부
    public int SubGunTypeBack = 0; //뒤 기관단총 장착여부
    public int AmmoAmount; //탄약량
    public int RealAmmoAmount; //실제 탄약 사용 변수
    private bool reloading = false; //중화기 재장전할 때의 스위치
    private bool Active = false; //중화기 획득시, 중화기를 활성화시키는 스위치

    //UI
    public GameObject PlayerMagazine;
    public GameObject PlayerMinigunAmmo;
    public Text ammoText;
    public Text ammoText2;

    //M3078
    private bool TurnRolling = false; //M3078 회전시키는 신호
    public GameObject M3078Prefab;
    public Transform M3078Pos;
    public GameObject M3078SmokePrefab;
    public Transform M3078SmokePos;
    public Transform ammoPos; //M3078 총알 생성 좌표
    private GameObject ejectedShell; //M3078 탄창 오브젝트 풀링, 딜레이 삭제하기 위해 선언 
    public Transform ejectPos; //M3078 탄피 배출 좌표
    public int MiniGunDamage; //M3078 총알당 데미지
    private int GunSmokeOn = 0; //M3078 처음 연기 발생 방지, 이후 사격마다 연기 발생 유도
    public float FireRate; //M3078 초당 발사수
    private float rollingTime = 0; //M3078 회전된 시간

    //ASC 365
    public GameObject ASC365Prefab;
    public GameObject ASC365FlamePrefab; //ASC 365 화염발사 프리팹
    public GameObject ASC365FlameStartPrefab; //ASC 365 스파크 화염 프리팹
    public GameObject ASC365FlameAmmoPrefab; //ASC 365 화염 방사시, 충돌이 아닌 일정시간 이후에만 사라지는 투명한 연사 발사체
    public GameObject ASC365FlameSmokePrefab; //ASC 365를 일정시간 이상 연속 발사시 연기 발생
    public Transform ASC365FlameAmmoPos;
    public int ASC365FlameDamage;
    public float ASC365FireRate; //ASC 365 초당 발사수
    private float ASC365FireTime; //일정시간 이상 연속 사격시 연기가 발생하기 위한 조취
    private float ASCFireRepeatTime; //화염발사반복을 항상 발사 후 3초 뒤에 반복되도록 처리

    public int HeavyWeaponType; //중화기 타입
    public int GetHeavyWeapon; //획득한 중화기 번호
    public float UsingTime = 0; //중화기를 획득했을 때, 중화기 획득 연출로 이어지기 위한 변수
    private float timeStamp = 0.0f; //중화기 초당 발사수에 사용되는 변수
    private float SwapTime = 0; //무기 교체용 변수
    private float SwapTime2 = 0; //무기 교체용 변수
    private float SwichTime = 0.5f; //무기를 교체하기 위한 시간적 허용 변수
    private float SoundTime; //소리를 딱 한 번만 출력시키기 위한 조취
    private float SoundChargeTime; //소리를 딱 한 번만 출력시키기 위한 조취
    private float TaskTime; //중화기 사용을 멈추었을 때를 표시하기 위한 변수
    private float AbandonTime; //중화기를 다 사용하자마자 코루틴을 딱 한번만 활성화하기 위한 조취

    public bool CantSwap = false; //체인지 중화기 들고 있을 때, 스왑을 못하게 처리
    public bool isMiniGun; // 버튼 입력용 bool값
    public bool isSwapM3078 = false;
    public bool UsingTask;
    private bool UsingChangeWeapon = false; //중화기 사용가능한 여부
    private bool Click;

    //M3078 사운드
    public AudioClip M3078Start;
    public AudioClip M3078End;
    public AudioClip M3078PickUp;
    public AudioClip M3078Abandone;
    public AudioClip Beep1;
    //ASC 365 사운드
    public GameObject ASC365SteamSound; //스팀 반복 소리
    public GameObject ASC365FireRepeat;
    public AudioClip ASC365FireEnd;

    //다른 무기 사용시 중화기의 효과 끄기
    public void TurnOn()
    {
        if (HeavyWeaponType == 2)
            ASC365FlameStartPrefab.SetActive(true);
    }
    public void TurnOff()
    {
        if (HeavyWeaponType == 2)
            ASC365FlameStartPrefab.SetActive(false);
    }

    public void MiniGunDown()
    {
        isMiniGun = true;
        CantSwap = true;
    }

    public void MiniGunUp()
    {
        isMiniGun = false;
        CantSwap = false;
    }

    public void M3078SwapUp()
    {
        if (Click == true)
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", false);
        Click = false;
    }

    public void M3078SwapDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", true);
    }

    public void M3078SwapEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", true);
        }
    }

    public void M3078SwapExit()
    {
        if (Click == true)
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", false);
    }

    public void M3078SwapClick()
    {
        if(arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false && 
            movement.UsingTask == false && vm5GrenadeController.UsingTask == false && CantSwap == false)
        {
            if (SwichTime < 0)
            {
                isSwapM3078 = !isSwapM3078;
            }
        }
    }

    private void UpdateBulletText()
    {
        if (ViewSW06_Magazine >= 100)
        {
            ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
        else if (ViewSW06_Magazine < 100 && ViewSW06_Magazine >= 10)
        {
            ammoText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
        else if (ViewSW06_Magazine < 10)
        {
            ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
    }

    void Start()
    {
        arthesL775Controller = FindObjectOfType<ArthesL775Controller>();
        hydra56Controller = FindObjectOfType<Hydra56Controller>();
        meagController = FindObjectOfType<MEAGController>();
        ugg98Controller = FindObjectOfType<UGG98Controller>();
        vm5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        movement = FindObjectOfType<Movement>();
        gunController = FindObjectOfType<GunController>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        ViewSW06_Magazine = AmmoAmount;
        RealAmmoAmount = -1;

        MiniGunDamage = UpgradeDataSystem.instance.M3078Damage;
        ASC365FlameDamage = UpgradeDataSystem.instance.ASC365Damage;
    }

    //사격 및 재장전
    public void Update()
    {
        timeStamp += Time.deltaTime;
        UpdateBulletText();

        if(UsingChangeWeapon == true && SwichTime > -0.5f)
            SwichTime -= Time.deltaTime;

        if (GetHeavyWeapon > 0)
            ChangeWeapon();

        //M3078
        if (HeavyWeaponType == 1)
        {
            if (GetHeavyWeapon == 1)
                M3078Fire();

            if (rollingTime <= 0 || GetHeavyWeapon == 0)
                rollingTime = 0;

            //M3078 회전소리 출력
            if (GetHeavyWeapon == 1 && UsingChangeWeapon == false)
            {
                if (isMiniGun && TurnRolling == true)
                {
                    if (SoundTime == 0)
                    {
                        SoundTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay18("Sound", M3078Start);
                    }
                }
            }
            //M3078 종료소리 출력
            if (GetHeavyWeapon == 1 && UsingChangeWeapon == false && GunSmokeOn == 0)
            {
                if (!isMiniGun && TurnRolling == false)
                {
                    if (SoundChargeTime == 0)
                    {
                        SoundChargeTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay18("Sound", M3078End);
                    }
                }
            }

            //미니건 공격 중단시 회전속도 감소 애니메이션
            if (TurnRolling == false && UsingChangeWeapon == false)
            {
                if (rollingTime >= 0)
                    rollingTime -= Time.deltaTime * 0.2f;

                if (rollingTime >= 0.3f && rollingTime < 0.5f)
                {
                    animator.SetBool("Attack rolling M3078, 2", false);
                    animator.SetBool("Attack rolling M3078, 1", true);
                    animator.SetFloat("Attack start M3078", 0.5f);
                }
                else if (rollingTime > 0 && rollingTime < 0.3f)
                {
                    animator.SetBool("Attack rolling M3078, 1", false);
                    animator.SetFloat("Attack start M3078", animator.GetFloat("Attack start M3078") - 0.01f);

                    if (animator.GetFloat("Attack start M3078") < 0)
                        animator.SetFloat("Attack start M3078", 0);
                }
                else if (rollingTime <= 0)
                {
                    animator.SetFloat("Attack start M3078", 0);

                    if (TaskTime == 0)
                    {
                        TaskTime += Time.deltaTime;
                        UsingTask = false;
                        Invoke("SwapAnimationOff", 0.5f);
                    }
                }
            }
        }

        //ASC 365
        else if (HeavyWeaponType == 2)
        {
            if (GetHeavyWeapon == 2 && UsingChangeWeapon == false)
            {
                if (SwichTime > -0.5f)
                    SwichTime -= Time.deltaTime;

                if (UsingTime > 0.5f)
                    StartCoroutine(ReadyHeavyWeapon());
                ASC365Fire();
            }

            if (!isMiniGun) //미사격시 가열 시간 감소
            {
                if (TaskTime == 0 && UsingTask == true) //미사격시 사격종료음 발생
                {
                    TaskTime += Time.deltaTime;
                    UsingTask = false;
                    ASCFireRepeatTime = 0;
                    ASC365FireRepeat.SetActive(false);
                    SoundManager.instance.SFXPlay("Sound", ASC365FireEnd);
                    animator.SetFloat("ASC 365 Attack", 0);
                }

                if (ASC365FireTime > 0)
                    ASC365FireTime -= Time.deltaTime;
            }

            if (ASC365FireTime < 5) //일정 시간 미사격시 연기 미발생
            {
                ASC365SteamSound.SetActive(false);
                animator.SetBool("ASC Smoke", false);
            }
        }

        //중화기 버리기
        if(RealAmmoAmount == 0)
        {
            GetHeavyWeapon = 0;

            if (AbandonTime == 0)
            {
                AbandonTime += Time.deltaTime;
                StartCoroutine(AbandonWeapon());
                //Debug.Log(Active);
            }
        }
    }

    //무기 교체
    void ChangeWeapon()
    {
        //주무기로 교체
        if (isSwapM3078 || Input.GetKeyDown(KeyCode.Y))
        {
            if (ViewSW06_Magazine > 0 && Active == true && UsingChangeWeapon == false && SwichTime < 0)
            {
                if (SwapTime2 == 0)
                {
                    SwapTime2 += Time.deltaTime;
                    StartCoroutine(OutputBasicWeapon());
                }
            }
        }
        //중화기로 교체
        if (!isSwapM3078 || Input.GetKeyDown(KeyCode.Y))
        {
            if (UsingChangeWeapon == true && SwichTime < 0)
            {
                if (SwapTime == 0)
                {
                    SwapTime += Time.deltaTime;
                    StartCoroutine(OuputHeavyWeapon());
                }
            }
        }
    }

    //M3078 미니건 사격
    public void M3078Fire()
    {
        if(GetHeavyWeapon == 1 && UsingChangeWeapon == false)
        {
            if (SwichTime > -0.5f)
                SwichTime -= Time.deltaTime;
            GunSmokeOn = 2;

            if (UsingTime > 0.5f)
                StartCoroutine(ReadyHeavyWeapon());

            if (isMiniGun || Input.GetKey(KeyCode.C))
            {
                if(ViewSW06_Magazine > 0 && Active == true)
                {
                    TurnRolling = true;
                    UsingTask = true;
                    TaskTime = 0;
                    AbandonTime = 0;

                    if (TurnRolling == true)
                    {
                        rollingTime += Time.deltaTime;

                        if (rollingTime <= 0)
                        {
                            animator.SetFloat("Attack start M3078", 0);
                        }

                        else if (rollingTime > 0 && rollingTime < 0.3f)
                        {
                            animator.SetFloat("Attack start M3078", animator.GetFloat("Attack start M3078") + 0.01f);
                        }

                        else if (rollingTime >= 0.3f && rollingTime < 0.5f)
                        {
                            animator.SetBool("Attack rolling M3078, 2", false);
                            animator.SetBool("Attack rolling M3078, 1", true);
                        }
                        else if (rollingTime >= 0.5f)
                        {
                            animator.SetBool("Attack rolling M3078, 1", false);
                            animator.SetBool("Attack rolling M3078, 2", true);

                            if (rollingTime > 0.5f)
                                rollingTime = 0.5f;

                            if (timeStamp >= FireRate)
                            {
                                timeStamp = 0;
                                Shake.Instance.ShakeCamera(2, 0.1f);

                                GameObject MiniGunBullet = objectManager.LoaderMiniGun();

                                MiniGunBullet.transform.position = ammoPos.position;
                                MiniGunBullet.transform.rotation = ammoPos.rotation; //발사 총알 생성
                                MiniGunBullet.GetComponent<MiniGunAmmoMovement>().SetDamage(MiniGunDamage); //총알에다 데미지 전달

                                MiniGunAmmoMovement MiniGunammoMovement = MiniGunBullet.GetComponent<MiniGunAmmoMovement>(); //MiniGunAmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 

                                MiniGunammoMovement.isHit = false; //피격방지
                                MiniGunammoMovement.objectManager = objectManager;

                                EjectShell(); //탄피 방출
                                ViewSW06_Magazine -= 1;
                                RealAmmoAmount -= 1;
                            }
                        }
                    }
                }
            }

            //사격직후 연기 발생
            else if (!isMiniGun && GunSmokeOn == 2 || Input.GetKeyUp(KeyCode.C))
            {
                TurnRolling = false;
                GunSmokeOn = 1;

                if (GunSmokeOn == 1 && rollingTime >= 0.5f)
                {
                    GunSmokeOn = 0;
                    GameObject M3078Smoke = Instantiate(M3078SmokePrefab, M3078SmokePos.position, M3078SmokePos.rotation);
                    M3078Smoke.SetActive(true);
                    Destroy(M3078Smoke, 3);
                    SoundTime = 0;
                    SoundChargeTime = 0;
                }
            }
        }
    }

    //ASC 365 화염방사기 사격
    void ASC365Fire()
    {
        if (isMiniGun || Input.GetKey(KeyCode.C))
        {
            UsingTask = true;
            TaskTime = 0;
            AbandonTime = 0;
            if (ASC365FireTime <= 7)
                ASC365FireTime += Time.deltaTime;
            if (ASCFireRepeatTime <= 3)
                ASCFireRepeatTime += Time.deltaTime;

            if (ASC365FireTime > 0 && ASC365FireTime < 1)
                animator.SetFloat("ASC 365 Attack", 1);
            else if (ASC365FireTime >= 1)
                animator.SetFloat("ASC 365 Attack", 1 + ASC365FireTime);

            if (timeStamp >= ASC365FireRate)
            {
                timeStamp = 0;

                GameObject FlameAmmo = objectManager.LoaderFlameGun();

                FlameAmmo.transform.position = ASC365FlameAmmoPos.position;
                FlameAmmo.transform.rotation = ASC365FlameAmmoPos.rotation; //발사 총알 생성
                FlameAmmo.GetComponent<FlameAmmoMovement>().SetDamage(ASC365FlameDamage); //총알에다 데미지 전달

                ViewSW06_Magazine -= 1;
                RealAmmoAmount -= 1;
            }

            if (ASCFireRepeatTime > 1) //발사 후 1초 뒤에 발사 반복소리 발생
                ASC365FireRepeat.SetActive(true);

            if (ASC365FireTime > 5) //5초 이상 발사시 연기 발생
            {
                ASC365SteamSound.SetActive(true);
                animator.SetBool("ASC Smoke", true);
            }
        }
    }

    //주무기로 교체
    IEnumerator OutputBasicWeapon()
    {
        UsingTask = true;
        CantSwap = true;
        animator.SetBool("Swap(M3078 input)", true);
        animator.SetBool("Reload Stop", false);

        GetComponent<GunController>().StopReload = false;
        GetComponent<Movement>().HeavyWeaponUsing(0); //중화기 사용 상태 전달

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 1 / 0.66f);

        yield return new WaitForSeconds(0.33f);

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(false);
            ASC365FlameStartPrefab.SetActive(false);
            ASC365FlameSmokePrefab.SetActive(false);
        }

        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        if (HeavyWeaponType == 1)
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(true);
        else if (HeavyWeaponType == 2)
            transform.Find("bone_1/ASC 365").gameObject.SetActive(true);
        if (GunType == 1)
        {
            animator.SetFloat("Gun active", 2);
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
        }
        if (GunType == 1000)
        {
            animator.SetFloat("Gun active", 1000);
            transform.Find("bone_1/DS-65").gameObject.SetActive(false);
        }
        if (GunType == 2000)
        {
            animator.SetFloat("Gun active", 2000);
            transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
        }
        if (SubGunTypeFront == 1)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("subGun active", 1);
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
        }
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("subGun active2", 1);
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
        }

        animator.SetBool("Swap(DT-37 output)", true);
        animator.SetBool("Swap(M3078 input)", false);

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = false;
        PlayerMagazine.gameObject.SetActive(true);
        PlayerMinigunAmmo.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.33f);

        animator.SetBool("Swap(DT-37 output)", false);

        GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 전달

        GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 0;
        GetComponent<UGG98Controller>().HeavyWeaponUsing = 0;
        GetComponent<MEAGController>().HeavyWeaponUsing = 0;
        GetComponent<Hydra56Controller>().HeavyWeaponUsing = 0;
        GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 0;

        SwichTime = 0.5f;
        SwapTime = 0;
        UsingChangeWeapon = true;
        CantSwap = false;
        UsingTask = false;
        GetComponent<Movement>().FireJoystickType = 1;

        animator.SetBool("Attack rolling M3078, 2", false);
        animator.SetBool("Attack rolling M3078, 1", false);
        animator.SetBool("M3078 end", true);
        animator.SetFloat("Attack start M3078", 0);
        rollingTime = 0;

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 0);

        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);

        yield return new WaitForSeconds(0.5f);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
    }

    //중화기로 교체
    IEnumerator OuputHeavyWeapon()
    {
        TaskTime = 1;
        UsingTask = true;
        CantSwap = true;
        if (GetComponent<GunController>().reloading == true)
            GetComponent<GunController>().StopReload = true;
        animator.SetFloat("Gun fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        animator.SetBool("Swap(DT-37 input)", true);
        animator.SetBool("M3078 end", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);

        GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 1 / 0.913f);

        yield return new WaitForSeconds(0.33f);

        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        if (GunType == 1)
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
        if (GunType == 1000)
            transform.Find("bone_1/DS-65").gameObject.SetActive(true);
        if (GunType == 2000)
            transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
        if (SubGunTypeFront == 1)
        {
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            animator.SetFloat("subGun active", 0);
        }
        if (SubGunTypeBack == 1)
        {
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            animator.SetFloat("subGun active2", 0);
        }

        if (HeavyWeaponType == 1)
        {
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
            animator.SetFloat("Gun active", 5000);
        }
        else if (HeavyWeaponType == 2)
        {
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);
            animator.SetFloat("Gun active", 5001);
        }

        animator.SetBool("Swap(M3078 output)", true);
        GetComponent<Movement>().HeavyWeaponUsing(50); //중화기 사용 상태 전달

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = true;
        PlayerMagazine.gameObject.SetActive(false);
        PlayerMinigunAmmo.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.583f);

        animator.SetBool("Swap(M3078 output)", false);
        animator.SetBool("Swap(DT-37 input)", false);

        if (HeavyWeaponType == 1 || HeavyWeaponType == 2)
        {
            GameControlSystem.TriggerIcon.SetActive(true);
            GameControlSystem.ChargeIcon.SetActive(false);
        }

        if (HeavyWeaponType == 1)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 1;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 1;
            GetComponent<MEAGController>().HeavyWeaponUsing = 1;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 1;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 1;
        }
        else if (HeavyWeaponType == 2)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 2;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 2;
            GetComponent<MEAGController>().HeavyWeaponUsing = 2;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 2;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 2;
        }

        CantSwap = false;
        UsingChangeWeapon = false;
        SwichTime = 0.5f;
        SwapTime2 = 0;
        GetComponent<Movement>().FireJoystickType = 100;

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 0);

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(true);
            ASC365FlameStartPrefab.SetActive(true);
            ASC365FlameSmokePrefab.SetActive(true);
        }
    }

    //중화기 획득
    public IEnumerator ReadyHeavyWeapon()
    {
        TaskTime = 1;
        UsingTask = true;
        CantSwap = true;
        SwapActive.raycastTarget = false;
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Turn off, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 2);
        GameControlScript.GetComponent<GameControlSystem>().inWeapon = true;
        GameControlScript.GetComponent<GameControlSystem>().GetHeavyWeapon = true;
        PlayerMagazine.gameObject.SetActive(false);
        PlayerMinigunAmmo.gameObject.SetActive(true);

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);

        if (HeavyWeaponType == 1)
            animator.SetFloat("Gun active", 5000);
        else if (HeavyWeaponType == 2)
            animator.SetFloat("Gun active", 5001);

        if (HeavyWeaponType == 1 || HeavyWeaponType == 2)
        {
            GameControlSystem.TriggerIcon.SetActive(true);
            GameControlSystem.ChargeIcon.SetActive(false);
        }

        if (GetComponent<GunController>().reloading == true)
            GetComponent<GunController>().StopReload = true;
        animator.SetFloat("Gun fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (GunType == 1)
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
        if (GunType == 1000)
            transform.Find("bone_1/DS-65").gameObject.SetActive(true);
        if (GunType == 2000)
            transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
        if (SubGunTypeFront == 1)
        {
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            animator.SetFloat("subGun active", 0);
        }
        if (SubGunTypeBack == 1)
        {
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            animator.SetFloat("subGun active2", 0);
        }


        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달
        GetComponent<Movement>().HeavyWeaponUsing(50); //중화기 사용 상태 전달

        //스킬 제한 목록
        if (HeavyWeaponType == 1)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 1;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 1;
            GetComponent<MEAGController>().HeavyWeaponUsing = 1;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 1;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 1;
        }
        else if (HeavyWeaponType == 2)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 2;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 2;
            GetComponent<MEAGController>().HeavyWeaponUsing = 2;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 2;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 2;
        }

        UsingTime = 0;
        animator.SetBool("Pick M3078", true);
        yield return new WaitForSeconds(0.75f);
        SwapActive.raycastTarget = true;
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 0);
        animator.SetBool("Pick M3078", false);
        Active = true;
        CantSwap = false;
        GetComponent<Movement>().FireJoystickType = 100;

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(true);
            ASC365FlameStartPrefab.SetActive(true);
            ASC365FlameSmokePrefab.SetActive(true);
        }
    }

    //중화기 버리기
    public IEnumerator AbandonWeapon()
    {
        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(false);
            ASC365FlameStartPrefab.SetActive(false);
            ASC365FlameSmokePrefab.SetActive(false);
        }

        UsingTask = true;
        GetComponent<GunController>().StopReload = false;
        animator.SetBool("Reload Stop", false);

        SwapActive.raycastTarget = false;
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 1);
        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);

        SoundManager.instance.SFXPlay14("Sound", M3078End);
        GetComponent<Movement>().HeavyWeaponUsing(51); //중화기 사용 상태 전달
        animator.SetBool("Abandon M3078", true);
        yield return new WaitForSeconds(0.35f);
        if (HeavyWeaponType == 1)
            Instantiate(M3078Prefab, M3078Pos.position, M3078Pos.rotation);
        else if (HeavyWeaponType == 2)
            Instantiate(ASC365Prefab, M3078Pos.position, M3078Pos.rotation);
        animator.SetFloat("Gun active", 0);
        animator.SetBool("Pick M3078", false);
        animator.SetBool("Attack rolling M3078, 1", false);
        animator.SetBool("Attack rolling M3078, 2", false);
        animator.SetFloat("Attack start M3078", 0);
        yield return new WaitForSeconds(0.483f);
        GetComponent<Movement>().HeavyWeaponUsing(0); //중화기 사용 상태 전달
        if (GunType == 1)
        {
            animator.SetFloat("Gun active", 2);
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
        }
        if (GunType == 1000)
        {
            animator.SetFloat("Gun active", 1000);
            transform.Find("bone_1/DS-65").gameObject.SetActive(false);
        }
        if (GunType == 2000)
        {
            animator.SetFloat("Gun active", 2000);
            transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
        }
        if (SubGunTypeFront == 1)
        {
            animator.SetFloat("subGun active", 1);
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
        }
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("subGun active2", 1);
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Abandon M3078", false);
        RealAmmoAmount = -1;
        this.GetComponent<HeavyWeaponSupport>().TimeStart = 1;

        GameControlSystem.TriggerIcon.SetActive(true);
        GameControlSystem.ChargeIcon.SetActive(false);

        GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 전달

        GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 0;
        GetComponent<UGG98Controller>().HeavyWeaponUsing = 0;
        GetComponent<MEAGController>().HeavyWeaponUsing = 0;
        GetComponent<Hydra56Controller>().HeavyWeaponUsing = 0;
        GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 0;

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = false;
        GameControlScript.GetComponent<GameControlSystem>().GetHeavyWeapon = false;
        PlayerMagazine.gameObject.SetActive(true);
        PlayerMinigunAmmo.gameObject.SetActive(false);

        GetComponent<Movement>().FireJoystickType = 1;
        UsingChangeWeapon = false;
        Active = false;
        if (TaskTime == 0)
        {
            TaskTime += Time.deltaTime;
            UsingTask = false;
        }

        if (HeavyWeaponType == 1)
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
        else if (HeavyWeaponType == 2)
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Turn off, Swap", true);

        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);

        yield return new WaitForSeconds(0.5f);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
    }

    //탄피 방출
    public void EjectShell()
    {
        ejectedShell = objectManager.Loader("MiniGunShell");
        ejectedShell.transform.position = ejectPos.transform.position;
        ejectedShell.transform.rotation = ejectPos.transform.rotation;
        ShellCase_SW06 ShellCase_SW06 = ejectedShell.GetComponent<ShellCase_SW06>();
        ShellCase_SW06.Pos = ejectedShell.transform.position.y;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        ejectedShell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
        ejectedShell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
    }

    //장전 UI 싸이클 완료후
    void AfterEndCycle()
    {
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
    }

    //장전 UI 싸이클 완료처리
    void ViewCountComplete()
    {
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", false);
    }
}