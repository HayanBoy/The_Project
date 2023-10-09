using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("스크립트")]
    public ArthesL775Controller arthesL775Controller;
    public Hydra56Controller hydra56Controller;
    public MEAGController meagController;
    public UGG98Controller ugg98Controller;
    public VM5GrenadeController vm5GrenadeController;
    public M3078Controller m3078Controller;
    public ObjectManager objectManager;
    //public GameObject ObjectsManager;

    Animator animator;
    Rigidbody2D rb2D;

    [Header("주 무기 번호")]
    public int GunType = 0; //총 장착여부
    public int SubGunTypeFront = 0; //총 장착여부
    public int SubGunTypeBack = 0; //총 장착여부

    [Header("UI 정보")]
    public GameObject AnimationUIReload;
    public GameObject AnimationUIAmmoDrop;
    public GameObject WeaponNumberUI;
    public GameObject DT37UI;
    public GameObject DS65UI;
    public GameObject DP9007UI;

    private Shake shake;

    Coroutine shotgunReload;
    Coroutine shotgunReloadAfterGetAmmo;
    Coroutine MagazineReload1;
    Coroutine MagazineReload2;
    Coroutine MagazineReload3;
    Coroutine SubGunReload1;
    Coroutine SubGunReload2;
    Coroutine SubGunReload3;
    Coroutine dS65Fire;
    Coroutine reloadContinueTactical; //소총 이어서 장전
    Coroutine reloadContinueFull1;
    Coroutine reloadContinueFull2;
    Coroutine reloadContinueAddAmmo1;
    Coroutine reloadContinueAddAmmo2;
    Coroutine SubGunReloadContinueFull1; //기관단총 이어서 장전
    Coroutine SubGunReloadContinueFull2;
    Coroutine SubGunReloadContinueFull3;
    Coroutine SubGunReloadContinueTactical1;
    Coroutine SubGunReloadContinueTactical2;
    Coroutine SubGunReloadContinueAddAmmo1;
    Coroutine SubGunReloadContinueAddAmmo2;
    Coroutine SubGunReloadHalf1;
    Coroutine SubGunReloadHalf2;
    Coroutine SubGunHalfReload1;
    Coroutine SubGunHalfReload2;
    Coroutine SubGunReloadComplete;

    [Header("사이트")]
    public GameObject ShipBoxPrefab;
    public Transform ShipBoxPos;
    private BoxCollider2D area;
    public GameObject SitePrefab; //보급박스 랜덤 사이트 생성기
    GameObject Site;

    [Header("주 무기 상태")]
    private float timeStamp = 0.0f; //기관단총을 제외한 총 전용
    private float subFrontTimeStamp = 0.0f; //앞 기관단총 전용
    private float subBackTimeStamp = 0.0f; //뒤 기관단총 전용
    private float DelayShot; //기관단총 전용 뒤총이 앞총보다 조금 늦게 쏘도록 조취
    private float ShotgunFireTime = 0;
    private float AddAmmoTime; //탄약을 한번에 두 번 받게 되는 버그를 해결하기 위한 조취
    public bool reloading = false;
    private bool SniperMuzzleFlash = false; //한번만 머즐 플레쉬 효과가 발생하기 위한 조취
    private bool SnipergunAfterFire = false; //스나이퍼총을 다 쏜 후에 액션실시용
    private bool ShotgunAfterFire = false; //샷건을 다 쏜 후에 액션실시용
    private bool ShotgunReloadCompleteType = false; //샷건 장전시 중간 장전이면 꺼짐, 사격후 및 보급 후일 경우 켜짐.
    bool NeedAddAmmo = false; //탄약이 고갈되었을 때 신호
    bool AfterAddAmmo = false; //탄약이 고갈되었을 때 보급을 받았을 경우, 보급후 재장전을 구동하거나 두 번 받게되는 버그 수정을 해결하기 위한 조취
    public bool ReloadTime;
    public bool UsingTask; //플레이어가 작업 중임을 뜻하는 스위치
    public bool StopReload = false;
    public bool NotReloaded = false; //장전 도중에 다른 행동을 했을 경우, 장전이 중간에서 멈추었다는 신호
    public bool ReloadCompleteUI = false; //장전이 완료되었을 때 탄약 UI의 깜빡꺼리는 현상을 중지 및 다른 장전이 충돌되지 않도록 처리
    private bool StopFireUntillReloadingTactical = false; //전술 재장전을 하던 도중에 멈추고 나서 남은 탐환 1개를 발사해서 탄을 비웠을 때, 강제로 사격 중지하도록 처리
    private bool ReloadTacticalComplete = false; //전술 재장전 도중에 탄창을 넣은 상태에서 캔슬했을 때, 자동으로 나머지 0.35초 이후의 완료처리를 강제로 처리
    public bool VehicleActive;
    private bool ClickReload;
    private bool ClickAmmoDrop;

    [Header("텍스트 정보")]
    public Text ammoText;
    public Text MagazineText;
    public Text ammoText2;
    public Text MagazineText2;
    public Text EnergyText;
    public Text EnergyText2;

    [Header("탄약 정보")]
    public int ammoInMagagine = 0; //탄창만큼 총알을 사용했을 때의 표시용도
    int tempAmmo;
    int viewTempAmmo;
    int fireEffect;

    [Header("장전 상태 및 탄약 표시")]
    public int ReloadState = 0; //장전 상태 표시. 소총 및 앞 기관단총 포함. 0 = 장전 시작 안됨, 1 = 탄창을 제거한 상태, 2 = 탄창을 갈아끼운 상태(노리쇠 후퇴는 안함)
    public int ReloadState2 = 0; //뒤 기관단총 장전 상태 표시. 
    public int ReloadType = 0; //어떤 장전 상태인지 표시. 1 = 사격 후 재장전, 2 = 전술 재장전, 3 = 보급 후 재장전
    public int subGunReloadType1 = 0; //기관단총 장전 상태 표시. ReloadType과 동일함.
    public int subGunReloadType2 = 0;
    public int subGunMagazineType1 = 1; //기관단총 탄창이 빠졌는지 안 빠졌는지에 대한 여부. 0 = 빠짐, 1 = 안 빠짐
    public int subGunMagazineType2 = 1;
    public int subGunCanFire1 = 0; //기관단총에 탄알이 있는지 확인하는 여부. 0 : 있음, 1 = 없음.
    public int subGunCanFire2 = 0;
    public int ViewAmmo; //인게임 탄약 소지 표시기
    public int ViewSW06_Magazine; //인게임 탄창 내 탄약 표시기
    public int ViewSubMachineGunMagazineFront; //인게임 기관단총 앞총 탄창 내 탄약 표시기
    public int ViewSubMachineGunMagazineBack; //인게임 기관단총 뒷총 탄창 내 탄약 표시기
    private int ViewAllMagazine; //인게임용 탄창 내 탄약 표시
    private float ReloadUITime; //재장전할 때 탄약 숫자들 연출을 한번만 하도록 조취
    private float ZeroAmmoUITime;
    public float ZeroMagagineUITime;
    private float ZeroEnergyUITime;
    private float ReloadCancleTime;

    [Header("총알 및 탄피 프리팹")]
    public GameObject subGunMuzzleFlash1;
    public GameObject subGunMuzzleFlash2;
    GameObject DT37Shell; //DT-37 탄피
    GameObject DS65Shell; //DS-65 탄피
    GameObject DP9007Shell; //DP-9007 탄피
    GameObject CGD27Shell; //CGD-27 탄피
    GameObject DT37Fire; //DT-37 발사 탄알
    public Transform DT37ShellPos; //DT-37 탄피 배출 좌표
    public Transform DS65ShellPos; //DS-65 탄피 배출 좌표
    public Transform DP9007ShellPos; //DP-9007 탄피 배출 좌표
    public Transform DT37Firepos; //총알 생성 좌표
    public Transform CGD27FirePosFront; //기관단총 앞 CGD-27 총알 생성 좌표
    public Transform CGD27FirePosBack; //기관단총 뒤 CGD-27 총알 생성 좌표
    public Transform CGD27ShellPosFront; //기관단총 앞 CGD-27 탄피 생성 좌표
    public Transform CGD27ShellPosBack; //기관단총 뒤 CGD-27 탄피 생성 좌표

    [Header("주 무기 발사 정보")]
    public int Damage; //총알당 데미지
    public float FireRate; //초당 발사수
    public float subFrontFireRate; //앞 기관단총 초당 발사수
    public float subBackFireRate; //뒤 기관단총 초당 발사수
    public float FinalReloadTime; //재장전 시간
    public float TaticalReloadTime; //전술 재장전 시간

    [Header("주 무기 탄약량")]
    public int AmmoPerMagazine; //탄창당 탄약량
    private int SubMachineGunFrontAmmoPerMagazine; //기관단총 앞총 탄창 내 탄약량
    private int SubMachineGunBackAmmoPerMagazine; //기관단총 뒷총 탄창 내 탄약량
    public int AmmoAmount; //탄약량
    public int SubMachineGunFrontAmmoAmount;
    public int SubMachineGunBackAmmoAmount;
    public int MaxAmmoAmount; //최대 소지가능한 탄약량
    public int SubMachineGunFrontMaxAmmoAmount;
    public int SubMachineGunBackMaxAmmoAmount;
    public int GetAmmoAmount; //탄약 획득량
    public int SubMachineGunFrontGetAmmoAmount;
    public int SubMachineGunBackGetAmmoAmount;

    [Header("체인지 중화기 에너지량")]
    public int ChangeSkillEnergy; //체인지스킬 에너지값 표시
    public int ChangeWeaponEnergy; //체인지 중화기 에너지값
    public int GetChangeWeaponEnergy; //체인지 에너지 보금품 먹었을 때 에너지 획득량
    private int MaxChangeWeaponEnergy; //체인지 중화기 최대에너지값
    private float ReloadOneTime; //장전을 한번 만 할 수 있도록 조취
    private float FireStopTime = 1;
    private float subGunFireStopTime1 = 1; //기관단총 사격 종료 직후 연기 작동 스위치
    private float subGunFireStopTime2 = 1;

    private float subGunFrontOutputTime; //앞 기관단총 탄창 빼는 시간
    private float subGunBackOutputTime; //뒤 기관단총 탄창 빼는 시간
    private float subGunFrontInputTime; //앞 기관단총 탄창 넣는 시간
    private float subGunBackInputTime; //뒤 기관단총 탄창 넣는 시간
    private float subGunReloadTime; //기관단총 전체 장전 시간

    float vectorY; //샷건 총알 벡터
    float angle; //샷건 총알 각도

    [Header("주 무기 발포 후 연기 프리팹")]
    public GameObject smokePrefab; //연기 프리팹
    public Transform smokePos; //연기 좌표
    public GameObject ShotGunSmokePrefab; //DS-65 연기 프리팹
    public Transform ShotGunSmokePos; //DS-65 연기 좌표
    public GameObject DP9007SmokePrefab; //DP-9007 연기 프리팹
    public Transform DP9007SmokePos; //DP-9007 연기 좌표

    public GameObject CGD27FrontSmokePrefab; //앞 CGD-27 연기 프리팹
    public Transform CGD27FrontSmokePos; //앞 CGD-27 연기 좌표
    public GameObject CGD27BackSmokePrefab; //뒤 CGD-27 연기 프리팹
    public Transform CGD27BackSmokePos; //뒤 CGD-27 연기 좌표
    int GunSmokeOn = 0; //처음 연기 발생 방지, 이후 사격마다 연기 발생 유도

    [Header("폭격을 위한 레이저 지시 상태")]
    public bool guiding; //레이저 지시 상태
    public bool guidingStopOrder; //레이저 지시 유지 상태

    [Header("보급")]
    public int BulletItem; //보급 아이템
    public float BulletItemCool; //보급 아이템 쿨타임 (1번먹고 난 뒤 그 다음 물약 먹는 텀)
    float BulletItemTime;

    [Header("주 무기 버튼 입력용")]
    //버튼 입력용 bool값 
    public bool isGun;
    public bool isReload;
    public bool isItemDrop;
    public bool UsingChangeWeapon = false;

    [Header("주 무기 사운드")]
    //public AudioClip DT_37Fire;
    public AudioClip DT_37FireComplete;
    public AudioClip DT_37Reload2;
    public AudioClip CGD27FireComplete;
    public AudioClip AmmoGet;
    public AudioClip NoAmmo;
    public AudioClip Beep1;
    public AudioClip Beep2;

    public void GunDown()
    {
        isGun = true;
    }

    public void GunUp()
    {
        isGun = false;
    }

    public void ReloadUp()
    {
        if (ClickReload == true)
            AnimationUIReload.GetComponent<Animator>().SetBool("Click, Reload", false);
        ClickReload = false;
    }

    public void ReloadDown()
    {
        ClickReload = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        AnimationUIReload.GetComponent<Animator>().SetBool("Click, Reload", true);
    }

    public void ReloadEnter()
    {
        if (ClickReload == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            AnimationUIReload.GetComponent<Animator>().SetBool("Click, Reload", true);
        }
    }

    public void ReloadExit()
    {
        if (ClickReload == true)
            AnimationUIReload.GetComponent<Animator>().SetBool("Click, Reload", false);
    }

    public void ReloadClick()
    {
        if (GunType > 0) //일반 총
        {
            if (reloading == false && ViewSW06_Magazine < AmmoPerMagazine + 1 && arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false 
                && meagController.UsingTask == false && ugg98Controller.UsingTask == false && vm5GrenadeController.UsingTask == false && m3078Controller.UsingTask == false)
                isReload = true;
        }
        else //기관단총
        {
            if (reloading == false && ViewSW06_Magazine < AmmoPerMagazine + 2 && arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false 
                && meagController.UsingTask == false && ugg98Controller.UsingTask == false && vm5GrenadeController.UsingTask == false && m3078Controller.UsingTask == false)
                isReload = true;
        }
    }

    public void ItemDropClick()
    {
        if (BulletItem > 0)
            isItemDrop = true;
    }

    public void ItemDropUp()
    {
        if (ClickAmmoDrop == true)
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Click, Ammo drop", false);
        ClickAmmoDrop = false;
    }

    public void ItemDropDown()
    {
        ClickAmmoDrop = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Click, Ammo drop", true);
    }

    public void ItemDropEnter()
    {
        if (ClickAmmoDrop == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Click, Ammo drop", true);
        }
    }

    public void ItemDropExit()
    {
        if (ClickAmmoDrop == true)
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Click, Ammo drop", false);
    }

    public void LaserGuiding(bool guiding)
    {
        if (guiding == true)
        {
            guidingStopOrder = true;
        }
        else
        {
            guidingStopOrder = false;
        }
    }

    private void UpdateBulletText()
    {
        //기본 총, 전체 탄약량
        if (ViewAmmo >= 100)
        {
            ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
        }
        else if (ViewAmmo < 100 && ViewAmmo >= 10)
        {
            ammoText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
        }
        else if (ViewAmmo < 10 && ViewAmmo >= 1)
        {
            ammoText.text = string.Format("<color=#00665B>{0}</color><color=#00665B>{1}</color><color=#8DFFF3>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#2E3D3F>{1}</color><color=#50888C>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
        }
        else if (ViewAmmo <= 0)
        {
            if (ZeroAmmoUITime == 0)
            {
                ZeroAmmoUITime += Time.deltaTime;
                StartCoroutine(ZeroAmmoUI());
            }
        }

        //기본 총, 탄창 내 탄약량
        if (ReloadCompleteUI == false)
        {
            if (ViewSW06_Magazine >= ViewAllMagazine * 0.2f)
            {
                if (ViewSW06_Magazine < 100 && ViewSW06_Magazine >= 10)
                {
                    MagazineText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                    MagazineText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                }
                else if (ViewSW06_Magazine < 10 && ViewSW06_Magazine >= 1)
                {
                    MagazineText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                    MagazineText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                }
            }
            else if (ViewSW06_Magazine < ViewAllMagazine * 0.2f && ViewSW06_Magazine >= 1)
            {
                if (ViewSW06_Magazine >= 10 && ViewSW06_Magazine >= 1)
                {
                    MagazineText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                    MagazineText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                }
                else if (ViewSW06_Magazine < 10 && ViewSW06_Magazine >= 1)
                {
                    MagazineText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                    MagazineText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
                }
            }
            else if (ViewSW06_Magazine <= 0)
            {
                if (ZeroMagagineUITime == 0)
                {
                    ZeroMagagineUITime += Time.deltaTime;
                    StartCoroutine(ZeroMagagineUI());
                }
            }
        }
        else if (ReloadCompleteUI == true)
        {
            if (ReloadUITime == 0)
            {
                ReloadUITime += Time.deltaTime;
                StartCoroutine(ReloadUI());
            }
        }

        //체인지 중화기 나노입자량
        if (ChangeWeaponEnergy >= 1000)
        {
            if (ChangeSkillEnergy >= 1000)
            {
                EnergyText.text = string.Format("<color=#8DFFF3>{0:F0}</color><color=#8DFFF3>{1:F0}</color><color=#8DFFF3>{2:F0}</color><color=#8DFFF3>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#50888C>{0:F0}</color><color=#50888C>{1:F0}</color><color=#50888C>{2:F0}</color><color=#50888C>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < 1000 && ChangeSkillEnergy >= ChangeWeaponEnergy * 0.1f)
            {
                EnergyText.text = string.Format("<color=#00665B>{0:F0}</color><color=#8DFFF3>{1:F0}</color><color=#8DFFF3>{2:F0}</color><color=#8DFFF3>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#2E3D3F>{0:F0}</color><color=#50888C>{1:F0}</color><color=#50888C>{2:F0}</color><color=#50888C>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy >= ChangeWeaponEnergy * 0.1f && ChangeSkillEnergy >= 100)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#FF1A15>{1:F0}</color><color=#FF1A15>{2:F0}</color><color=#FF1A15>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#7E0D0B>{1:F0}</color><color=#7E0D0B>{2:F0}</color><color=#7E0D0B>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < ChangeWeaponEnergy * 0.1f && ChangeSkillEnergy < 100 && ChangeSkillEnergy >= 10)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#8C1411>{1:F0}</color><color=#FF1A15>{2:F0}</color><color=#FF1A15>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#380302>{1:F0}</color><color=#7E0D0B>{2:F0}</color><color=#7E0D0B>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < 10 && ChangeSkillEnergy >= 1)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#8C1411>{1:F0}</color><color=#8C1411>{2:F0}</color><color=#FF1A15>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#380302>{1:F0}</color><color=#380302>{2:F0}</color><color=#7E0D0B>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }

            else if (ChangeSkillEnergy <= 0)
            {
                if (ZeroEnergyUITime == 0)
                {
                    ZeroEnergyUITime += Time.deltaTime;
                    StartCoroutine(ZeroEnergyUIOver());
                }
            }
        }
        else if (ChangeWeaponEnergy < 1000)
        {
            if (ChangeSkillEnergy >= 100)
            {
                EnergyText.text = string.Format("<color=#8DFFF3>{0:F0}</color><color=#8DFFF3>{1:F0}</color><color=#8DFFF3>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#50888C>{0:F0}</color><color=#50888C>{1:F0}</color><color=#50888C>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < 100 && ChangeSkillEnergy >= ChangeWeaponEnergy * 0.1f)
            {
                EnergyText.text = string.Format("<color=#00665B>{0:F0}</color><color=#8DFFF3>{1:F0}</color><color=#8DFFF3>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#2E3D3F>{0:F0}</color><color=#50888C>{1:F0}</color><color=#50888C>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < ChangeWeaponEnergy * 0.1f && ChangeSkillEnergy >= 10)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#FF1A15>{1:F0}</color><color=#FF1A15>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#7E0D0B>{1:F0}</color><color=#7E0D0B>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }
            else if (ChangeSkillEnergy < 10 && ChangeSkillEnergy >= 1)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#8C1411>{1:F0}</color><color=#FF1A15>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#380302>{1:F0}</color><color=#7E0D0B>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            }

            else if (ChangeSkillEnergy <= 0)
            {
                if (ZeroEnergyUITime == 0)
                {
                    ZeroEnergyUITime += Time.deltaTime;
                    StartCoroutine(ZeroEnergyUI());
                }
            }
        }
    }

    //기본 총, 재장전 표시
    IEnumerator ReloadUI()
    {
        while(ReloadCompleteUI)
        {
            MagazineText.text = string.Format("<color=#FF8A26>{0}</color><color=#FF8A26>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            MagazineText2.text = string.Format("<color=#874A15>{0}</color><color=#874A15>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            yield return new WaitForSeconds(0.2f);
            MagazineText.text = string.Format("<color=#BA651D>{0}</color><color=#BA651D>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            MagazineText2.text = string.Format("<color=#5E330F>{0}</color><color=#5E330F>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            yield return new WaitForSeconds(0.2f);
        }
    }

    //기본 총, 전체 탄약량이 바닥났을 때
    IEnumerator ZeroAmmoUI()
    {
        while (ViewAmmo == 0)
        {
            ammoText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ViewAmmo != 0)
                break;
        }
    }

    //기본 총, 탄창 속 탄약량이 바닥났을 때
    IEnumerator ZeroMagagineUI()
    {
        while (reloading == false && ViewSW06_Magazine == 0)
        {
            MagazineText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            MagazineText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            if (reloading == true)
                break;
            yield return new WaitForSeconds(0.2f);
            MagazineText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            MagazineText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color>", ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            if (reloading == true)
                break;
            yield return new WaitForSeconds(0.2f);
            if (reloading == true)
                break;
        }
    }

    //체인지 중화기, 나노입자가 바닥났을 때
    IEnumerator ZeroEnergyUI()
    {
        while (ChangeSkillEnergy <= 0)
        {
            EnergyText.text = string.Format("<color=#FF1A15>{0:F0}</color><color=#FF1A15>{1:F0}</color><color=#FF1A15>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            EnergyText2.text = string.Format("<color=#7E0D0B>{0:F0}</color><color=#7E0D0B>{1:F0}</color><color=#7E0D0B>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            if (ChangeSkillEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#8C1411>{1:F0}</color><color=#8C1411>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#380302>{1:F0}</color><color=#380302>{2:F0}</color>", ChangeSkillEnergy / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            if (ChangeSkillEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ChangeSkillEnergy != 0)
                break;
        }
    }
    IEnumerator ZeroEnergyUIOver()
    {
        while (ChangeSkillEnergy <= 0)
        {
            EnergyText.text = string.Format("<color=#FF1A15>{0:F0}</color><color=#FF1A15>{1:F0}</color><color=#FF1A15>{2:F0}</color><color=#FF1A15>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            EnergyText2.text = string.Format("<color=#7E0D0B>{0:F0}</color><color=#7E0D0B>{1:F0}</color><color=#7E0D0B>{2:F0}</color><color=#7E0D0B>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            if (ChangeSkillEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            EnergyText.text = string.Format("<color=#8C1411>{0:F0}</color><color=#8C1411>{1:F0}</color><color=#8C1411>{2:F0}</color><color=#8C1411>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            EnergyText2.text = string.Format("<color=#380302>{0:F0}</color><color=#380302>{1:F0}</color><color=#380302>{2:F0}</color><color=#380302>{3:F0}</color>", ChangeSkillEnergy / 1000, ChangeSkillEnergy % 1000 / 100, ChangeSkillEnergy % 100 / 10, ChangeSkillEnergy % 10);
            if (ChangeSkillEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ChangeSkillEnergy != 0)
                break;
        }
    }

    void Start()
    {
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        area = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        ChangeSkillEnergy = ChangeWeaponEnergy;
        MaxChangeWeaponEnergy = ChangeWeaponEnergy;

        if (GunType != 0)
        {
            GetComponent<ArthesL775Controller>().GunType = GunType;
            GetComponent<UGG98Controller>().GunType = GunType;
            GetComponent<Hydra56Controller>().GunType = GunType;
            GetComponent<MEAGController>().GunType = GunType;
            GetComponent<VM5GrenadeController>().GunType = GunType;
            GetComponent<M3078Controller>().GunType = GunType;
        }
        else if (GunType == 0)
        {
            GetComponent<ArthesL775Controller>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<ArthesL775Controller>().SubGunTypeBack = SubGunTypeBack;
            GetComponent<UGG98Controller>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<UGG98Controller>().SubGunTypeBack = SubGunTypeBack;
            GetComponent<Hydra56Controller>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<Hydra56Controller>().SubGunTypeBack = SubGunTypeBack;
            GetComponent<MEAGController>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<MEAGController>().SubGunTypeBack = SubGunTypeBack;
            GetComponent<VM5GrenadeController>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<VM5GrenadeController>().SubGunTypeBack = SubGunTypeBack;
            GetComponent<M3078Controller>().SubGunTypeFront = SubGunTypeFront;
            GetComponent<M3078Controller>().SubGunTypeBack = SubGunTypeBack;
        }

        //탄피 및 탄창 발생 갯수 동일화
        //소총류
        if (GunType == 1) //DT-37
        {
            DT37UI.SetActive(true);
            animator.SetFloat("Gun active", 2);
            Damage = UpgradeDataSystem.instance.DT37Damage;
            FireRate = 0.083f;
            FinalReloadTime = 4.916f / 1.5f;
            TaticalReloadTime = 3.26f / 1.5f;
            AmmoPerMagazine = DeltaHrricaneData.instance.DT37AmmoPerMagazine;
            AmmoAmount = DeltaHrricaneData.instance.DT37AmmoAmount;
            MaxAmmoAmount = DeltaHrricaneData.instance.DT37MaxAmmoAmount;
            GetAmmoAmount = DeltaHrricaneData.instance.DT37GetAmmoAmount;

            ViewAmmo = AmmoAmount - AmmoPerMagazine;
            ViewSW06_Magazine = AmmoPerMagazine;
        }

        //샷건류
        else if(GunType == 1000) //DS-65
        {
            //DS65UI.SetActive(true);
            animator.SetFloat("Gun active", 1000);
            Damage = UpgradeDataSystem.instance.DS65Damage;
            FireRate = 0.73f;
            AmmoPerMagazine = DeltaHrricaneData.instance.DS65AmmoPerMagazine;
            AmmoAmount = DeltaHrricaneData.instance.DS65AmmoAmount;
            MaxAmmoAmount = DeltaHrricaneData.instance.DS65MaxAmmoAmount;
            GetAmmoAmount = DeltaHrricaneData.instance.DS65GetAmmoAmount;

            ViewAmmo = AmmoAmount - AmmoPerMagazine;
            ViewSW06_Magazine = AmmoPerMagazine;
        }

        //저격총류
        else if(GunType == 2000) //DP-9007
        {
            //DP9007UI.SetActive(true);
            animator.SetFloat("Gun active", 2000);
            Damage = UpgradeDataSystem.instance.DP9007Damage;
            FireRate = 1.16f;
            AmmoPerMagazine = DeltaHrricaneData.instance.DP9007AmmoPerMagazine;
            AmmoAmount = DeltaHrricaneData.instance.DP9007AmmoAmount;
            MaxAmmoAmount = DeltaHrricaneData.instance.DP9007MaxAmmoAmount;
            GetAmmoAmount = DeltaHrricaneData.instance.DP9007GetAmmoAmount;

            ViewAmmo = AmmoAmount - AmmoPerMagazine;
            ViewSW06_Magazine = AmmoPerMagazine;
        }

        if(SubGunTypeFront >= 1 || SubGunTypeBack >= 1)
            GetComponent<Movement>().SubMachineGunOnline = true;

        //기관단총류
        if (SubGunTypeFront == 1) //CGD-27 Pillishion front
        {
            animator.SetFloat("subGun active", 1);
            Damage = UpgradeDataSystem.instance.CGD27PillishionDamage;
            subFrontFireRate = 0.083f;
            TaticalReloadTime = 3.26f / 1.5f;
            SubMachineGunFrontAmmoPerMagazine = DeltaHrricaneData.instance.CGD27AmmoPerMagazine / 2;
            SubMachineGunFrontAmmoAmount = DeltaHrricaneData.instance.CGD27AmmoAmount / 2;
            SubMachineGunFrontMaxAmmoAmount = DeltaHrricaneData.instance.CGD27MaxAmmoAmount / 2;
            SubMachineGunFrontGetAmmoAmount = DeltaHrricaneData.instance.CGD27GetAmmoAmount / 2;

            subGunFrontOutputTime = 1.246f;
            subGunFrontInputTime = 1.246f;
        }

        if (SubGunTypeBack == 1) //CGD-27 Pillishion back
        {
            animator.SetFloat("subGun active2", 1);
            Damage = UpgradeDataSystem.instance.CGD27PillishionDamage;
            subBackFireRate = 0.083f;
            TaticalReloadTime = 3.26f / 1.5f;
            SubMachineGunBackAmmoPerMagazine = DeltaHrricaneData.instance.CGD27AmmoPerMagazine / 2;
            SubMachineGunBackAmmoAmount = DeltaHrricaneData.instance.CGD27AmmoAmount / 2;
            SubMachineGunBackMaxAmmoAmount = DeltaHrricaneData.instance.CGD27MaxAmmoAmount / 2;
            SubMachineGunBackGetAmmoAmount = DeltaHrricaneData.instance.CGD27GetAmmoAmount / 2;

            subGunBackOutputTime = 0.83f;
            subGunBackInputTime = 0.746f;

            AmmoPerMagazine = SubMachineGunFrontAmmoPerMagazine + SubMachineGunBackAmmoPerMagazine;
            AmmoAmount = SubMachineGunFrontAmmoAmount + SubMachineGunBackAmmoAmount;
            MaxAmmoAmount = SubMachineGunFrontMaxAmmoAmount + SubMachineGunBackMaxAmmoAmount;
            GetAmmoAmount = SubMachineGunFrontGetAmmoAmount + SubMachineGunBackGetAmmoAmount;
            ViewAmmo = AmmoAmount - AmmoPerMagazine;
            ViewSW06_Magazine = AmmoPerMagazine;
            ViewSubMachineGunMagazineFront = SubMachineGunFrontAmmoPerMagazine;
            ViewSubMachineGunMagazineBack = SubMachineGunBackAmmoPerMagazine;
        }

        ViewAllMagazine = ViewSW06_Magazine;
        timeStamp = FireRate - 0.1f;
    }

    ////////////////////////////////////보급/////////////////////////////////////////////

    //보급 아이템 사용
    void itemDrop()
    {
        if (isItemDrop || Input.GetKeyUp(KeyCode.B))
        {
            if (BulletItem > 0)
            {
                BulletItem--;
                isItemDrop = false;
                Instantiate(ShipBoxPrefab, ShipBoxPos.position, ShipBoxPos.rotation); //함선 보급 지원 연출
                SpawnSite();
                //StartCoroutine(UseDropAmmo());
            }
        }
    }

    //랜덤 좌표
    void SpawnSite()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        Site = Instantiate(SitePrefab, spawnPos, Quaternion.Euler(0f, 0f, 0f));
        if (GunType == 1)
            Site.GetComponent<RandomSitePlayer>().DropType = 1;
        if (GunType == 1000)
            Site.GetComponent<RandomSitePlayer>().DropType = 1000;
        if (GunType == 2000)
            Site.GetComponent<RandomSitePlayer>().DropType = 2000;
        if (SubGunTypeFront == 1)
            Site.GetComponent<RandomSitePlayer>().DropsubGunFrontType = 1;
        if (SubGunTypeBack == 1)
            Site.GetComponent<RandomSitePlayer>().DropsubGunBackType = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 29 && AddAmmoTime <= 0)
        {
            if (collision.gameObject.tag == "Ammo item") //일반 보급 아이템 먹기
            {
                if (AmmoAmount != MaxAmmoAmount)
                {
                    //Debug.Log("보급 아이템을 먹었습니다.");
                    Destroy(collision.gameObject);
                    SoundManager.instance.SFXPlay22(" Sound", AmmoGet);
                    GetAmmo();
                }
            }

            if (collision.gameObject.tag == "Ammo item2") //체인지 에너지 아이템 먹기
            {
                if (ChangeSkillEnergy != MaxChangeWeaponEnergy)
                {
                    //Debug.Log("체인지 아이템을 먹었습니다.");
                    Destroy(collision.gameObject);
                    SoundManager.instance.SFXPlay22(" Sound", AmmoGet);
                    GetChangeAmmo();
                }
            }
        }
    }

    void GetAmmo()
    {
        //탄약 보급(탄약이 고갈되지 않았을 경우)
        if (NeedAddAmmo == false && AmmoAmount > 0)
        {
            //보급후 최대 탄약량을 넘지 못하는 경우
            if (AmmoAmount + GetAmmoAmount < MaxAmmoAmount && AddAmmoTime <= 0)
            {
                AddAmmoTime = 0.05f;
                AmmoAmount += GetAmmoAmount;
                ViewAmmo += GetAmmoAmount;
                this.gameObject.AddComponent<GunAttackText>().AmmoGet2(); //Debug 탄약 넉넉 보급
            }
            //보급후 최대 탄약량을 넘을 경우
            else if (AmmoAmount + GetAmmoAmount > MaxAmmoAmount && AddAmmoTime <= 0)
            {
                AddAmmoTime = 0.05f;
                AmmoAmount = MaxAmmoAmount;
                ViewAmmo = MaxAmmoAmount - ViewSW06_Magazine;

                this.gameObject.AddComponent<GunAttackText>().AmmoOverGet2(); //Debug 탄약 넉넉 과보급
            }
            //보급후 최대 탄약량과 동일한 경우
            else if (AmmoAmount + GetAmmoAmount == MaxAmmoAmount && AddAmmoTime <= 0)
            {
                AddAmmoTime = 0.05f;
                AmmoAmount = MaxAmmoAmount;
                ViewAmmo = MaxAmmoAmount - ViewSW06_Magazine;

                this.gameObject.AddComponent<GunAttackText>().AmmoOverGet2(); //Debug 탄약 넉넉 과보급
            }
        }
        //탄약 보급(탄약이 고갈되었을 경우)
        else if (NeedAddAmmo == true && AddAmmoTime <= 0)
        {
            AddAmmoTime = 0.05f;
            NeedAddAmmo = false;
            AfterAddAmmo = true;
            AmmoAmount += GetAmmoAmount;
            ViewAmmo += GetAmmoAmount;
            this.gameObject.AddComponent<GunAttackText>().AmmoGet(); //Debug 탄약 보급
        }
    }

    //체인지 중화기 에너지 보급
    void GetChangeAmmo()
    {
        if (ChangeSkillEnergy < MaxChangeWeaponEnergy)
        {
            if (ChangeSkillEnergy + GetChangeWeaponEnergy < MaxChangeWeaponEnergy)
                ChangeSkillEnergy += GetChangeWeaponEnergy;
            else
                ChangeSkillEnergy = MaxChangeWeaponEnergy;

            ZeroEnergyUITime = 0;
        }
    }

    //보급 아이템 쿨타임 함수
    void BulletItem_Cool()
    {
        if (BulletItem == 0)
        {
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time start, Ammo drop", true);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time running, Ammo drop", true);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetFloat("Cool time, Ammo drop", 1 / BulletItemCool);
            BulletItemTime += Time.deltaTime;
        }

        if (BulletItemTime > BulletItemCool)
        {
            BulletItemTime = 0;
            BulletItem++;
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time end, Ammo drop", true);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time cycle count, Ammo drop", true);
            Invoke("AfterEndCycleAmmo", 0.5f);
            Invoke("ViewCountCompleteAmmo", 0.5f);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time start, Ammo drop", false);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time running, Ammo drop", false);
            AnimationUIAmmoDrop.GetComponent<Animator>().SetFloat("Cool time, Ammo drop", 0);
        }
    }

    void AfterEndCycleAmmo()
    {
        AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time end, Ammo drop", false);
    }

    void ViewCountCompleteAmmo()
    {
        AnimationUIAmmoDrop.GetComponent<Animator>().SetBool("Cool time cycle count, Ammo drop", false);
    }

    ////////////////////////////////////보급/////////////////////////////////////////////

    //탄약 고갈
    void AmmoZero()
    {
        if (AmmoAmount == 0)
        {
            if (NeedAddAmmo == false)
            {
                if (GunType == 1)
                {
                    GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation);
                    Destroy(Smoke, 1.5f);
                    SoundManager.instance.SFXPlay10("DT-37 Fire Sound", DT_37FireComplete);
                }
                else if (GunType == 2000)
                {
                    GameObject Smoke = Instantiate(DP9007SmokePrefab, DP9007SmokePos.position, DP9007SmokePos.rotation);
                    Destroy(Smoke, 4);
                }
                if (ViewSubMachineGunMagazineFront <= 0)
                {
                    animator.SetFloat("subGunFront fire", 0);
                    if (subGunFireStopTime1 == 0)
                    {
                        if (SubGunTypeFront == 1)
                        {
                            subGunFireStopTime1 = 1;
                            subGunCanFire1 = 1;
                            DelayShot = 0;
                            subGunMuzzleFlash1.SetActive(false);
                            animator.SetFloat("NoLED", 3000);
                            SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                            GameObject Smoke1 = Instantiate(CGD27FrontSmokePrefab, CGD27FrontSmokePos.position, CGD27FrontSmokePos.rotation);
                            Destroy(Smoke1, 1.5f);
                        }
                    }
                }
                if (ViewSubMachineGunMagazineBack <= 0)
                {
                    animator.SetFloat("subGunBack fire", 0);
                    if (subGunFireStopTime2 == 0)
                    {
                        if (SubGunTypeBack == 1)
                        {
                            subGunFireStopTime2 = 1;
                            subGunCanFire2 = 1;
                            DelayShot = 0;
                            subGunMuzzleFlash2.SetActive(false);
                            animator.SetFloat("NoLED2", 1);
                            SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                            GameObject Smoke2 = Instantiate(CGD27BackSmokePrefab, CGD27BackSmokePos.position, CGD27BackSmokePos.rotation);
                            Destroy(Smoke2, 1.5f);
                        }
                    }
                }
                FireStopTime = 1;
                this.gameObject.AddComponent<GunAttackText>().NeedAmmo(); //Debug 탄약 고갈
            }

            ammoInMagagine = 0;
            NeedAddAmmo = true;
            reloading = false;
            UsingTask = false;
            StartCoroutine(FireActionOff()); //탄약 고갈직후 사격 액션 멈춤

            if (isGun || Input.GetKey(KeyCode.C))
            {
                if (timeStamp >= 0.4f)
                {
                    timeStamp = 0;
                    SoundManager.instance.SFXPlay2("DT-37 Fire Sound", NoAmmo);
                    this.gameObject.AddComponent<GunAttackText>().AmmoGive();
                }
            }
        }
    }

    ////////////////////////////////////사격/////////////////////////////////////////////

    public void AfterReload()
    {
        UsingTask = true;

        GetComponent<Movement>().ReloadTime(true);
        GetComponent<HeavyWeaponSupport>().ReloadTime(true);

        //체인지 스킬 제한 목록
        GetComponent<ArthesL775Controller>().ReloadTime(true); //체인지 스킬 사용 전달
        GetComponent<UGG98Controller>().ReloadTime(true); //체인지 스킬 사용 전달
        GetComponent<Hydra56Controller>().ReloadTime(true); //체인지 스킬 사용 전달
        GetComponent<MEAGController>().XMoveStop(true); //체인지 스킬 사용 전달

        GetComponent<VM5GrenadeController>().ReloadTime(true); //스킬 사용 전달
    }

    public void AfterReloadOff()
    {
        UsingTask = false;

        GetComponent<Movement>().ReloadTime(false);
        GetComponent<HeavyWeaponSupport>().ReloadTime(false);

        //체인지 스킬 제한 목록
        GetComponent<ArthesL775Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
        GetComponent<Hydra56Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().ReloadTime(false); //스킬 사용 해제 전달
    }

    //완전 재장전 탄약 계산
    void RealoadFull()
    {
        if (AmmoAmount < AmmoPerMagazine)
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewAmmo = 0;
        }
        else
        {
            ViewAmmo = AmmoAmount - AmmoPerMagazine;
            ViewSW06_Magazine = AmmoPerMagazine;
        }
    }

    //보급후 재장전 탄약 계산
    void ReloadFullAfterSupport()
    {
        ViewAmmo = AmmoAmount - AmmoPerMagazine;
        ViewSW06_Magazine = AmmoPerMagazine;
    }

    //전술 재장전 탄약 계산
    void ReloadTactical()
    {
        if (AmmoAmount < AmmoPerMagazine)
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewAmmo = 0;
        }
        else if (AmmoAmount == AmmoPerMagazine)
        {
            ViewAmmo = 0;
            ViewSW06_Magazine = AmmoPerMagazine;
        }
        else if (AmmoAmount > AmmoPerMagazine)
        {
            ViewAmmo = AmmoAmount - AmmoPerMagazine - 1; //-1을 넣은 이유는 총 내부에 물려있는 1발에 해당하기 때문
            ViewSW06_Magazine = AmmoPerMagazine + 1;
        }
    }

    //기관단총 앞부분 완전 재장전 탄약 계산
    void ReloadsubFront()
    {
        if (AmmoAmount < SubMachineGunFrontAmmoPerMagazine)
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewSubMachineGunMagazineFront = ViewAmmo;
            ViewAmmo = 0;
        }
        else
        {
            ViewSubMachineGunMagazineFront = SubMachineGunFrontAmmoPerMagazine;
            ViewAmmo = AmmoAmount - ViewSubMachineGunMagazineFront;
            ViewSW06_Magazine = ViewSubMachineGunMagazineFront;
        }
    }

    //기관단총 뒷부분 완전 재장전 탄약 계산
    void ReloadsubBack()
    {
        if (AmmoAmount < SubMachineGunBackAmmoPerMagazine)
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewSubMachineGunMagazineBack = ViewAmmo;
            ViewAmmo = 0;
        }
        else
        {
            if (ViewAmmo > SubMachineGunBackAmmoPerMagazine)
            {
                ViewSubMachineGunMagazineBack = SubMachineGunBackAmmoPerMagazine;
                ViewAmmo = AmmoAmount - ViewSubMachineGunMagazineFront - ViewSubMachineGunMagazineBack;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
            }
            else
            {
                ViewSubMachineGunMagazineBack = ViewAmmo;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
                ViewAmmo = 0;
            }
        }
    }

    //기관단총 앞부분 전술 재장전 탄약 계산
    void ReloadTacticalsubFront()
    {
        if (AmmoAmount < SubMachineGunFrontAmmoPerMagazine)
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewSubMachineGunMagazineFront = ViewAmmo;
            ViewAmmo = 0;
        }
        else if (AmmoAmount == SubMachineGunFrontAmmoPerMagazine)
        {
            ViewAmmo = 0;
            ViewSW06_Magazine = SubMachineGunFrontAmmoPerMagazine;
            ViewSubMachineGunMagazineFront = SubMachineGunFrontAmmoPerMagazine;
        }
        else if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            ViewSubMachineGunMagazineFront = SubMachineGunFrontAmmoPerMagazine + 1;
            if (ViewSubMachineGunMagazineBack == SubMachineGunBackAmmoPerMagazine + 1) //뒷 총이 이미 전술 재장전까지 마친 경우
            {
                ViewAmmo = AmmoAmount - ViewSubMachineGunMagazineFront - ViewSubMachineGunMagazineBack;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
            }
            else if (ViewSubMachineGunMagazineBack < SubMachineGunBackAmmoPerMagazine + 1)
            {
                ViewAmmo = AmmoAmount - ViewSubMachineGunMagazineFront;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront;
            }
        }
    }

    //기관단총 뒷부분 전술 재장전 탄약 계산
    void ReloadTacticalsubBcak()
    {
        if (AmmoAmount > SubMachineGunBackAmmoPerMagazine)
        {
            if (ViewAmmo > SubMachineGunBackAmmoPerMagazine) //뒷 탄창까지 모두 채울 수 있을 경우
            {
                ViewSubMachineGunMagazineBack = SubMachineGunBackAmmoPerMagazine + 1;
                ViewAmmo = AmmoAmount - ViewSubMachineGunMagazineFront - ViewSubMachineGunMagazineBack;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
            }
            else //남은 총알이 뒷 탄창을 다 채우지 못할 경우
            {
                ViewSubMachineGunMagazineBack = ViewAmmo;
                ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
                ViewAmmo = 0;
            }
        }
        else
        {
            ViewSW06_Magazine += ViewAmmo;
            ViewSubMachineGunMagazineBack += ViewAmmo;
            ViewAmmo = 0;
        }
    }

    //기관단총 앞부분 보급후 재장전 탄약 계산
    void ReloadSupportsubFront()
    {
        ViewAmmo = AmmoAmount - SubMachineGunFrontAmmoPerMagazine;
        ViewSubMachineGunMagazineFront = SubMachineGunFrontAmmoPerMagazine;
        ViewSW06_Magazine = ViewSubMachineGunMagazineFront;
    }

    //기관단총 뒷부분 보급후 재장전 탄약 계산
    void ReloadSupportsubBack()
    {
        ViewAmmo = AmmoAmount - SubMachineGunFrontAmmoPerMagazine - SubMachineGunBackAmmoPerMagazine;
        ViewSubMachineGunMagazineBack = SubMachineGunBackAmmoPerMagazine;
        ViewSW06_Magazine = ViewSubMachineGunMagazineFront + ViewSubMachineGunMagazineBack;
    }

    public void Update()
    {
        if(timeStamp <= FireRate + 0.1f)
            timeStamp += Time.deltaTime;

        if (subFrontTimeStamp <= FireRate + 0.1f)
            subFrontTimeStamp += Time.deltaTime;
        if (subBackTimeStamp <= FireRate + 0.1f)
            subBackTimeStamp += Time.deltaTime;

        if (AddAmmoTime > 0)
            AddAmmoTime -= Time.deltaTime;

        itemDrop(); //보급 아이템 사용
        if (VehicleActive == false)
            BulletItem_Cool(); //보급 아이템 쿨타임 함수
        UpdateBulletText(); //탄약 UI
        AmmoZero(); //탄약 고갈

        //장전 도중에 다른 행동을 했을 때 단계별 장전 중단
        if (StopReload == true)
        {
            reloading = false;
            animator.SetFloat("ReloadType", 0);

            //장전 상태가 아닐 경우, 정상적인 장전이 가능하도록 처리
            if (ReloadState > 0 || ReloadState2 > 0)
                NotReloaded = true;

            //탄창을 뺀 상태에서 장전이 캔슬되었을 때 무기별로 탄창을 사라지게 하기
            if (ReloadState == 1)
            {
                if (GunType == 1)
                    animator.SetFloat("NoLED", 1);
                else if (GunType == 2000)
                    animator.SetFloat("NoLED", 2000);
            }

            //전술 재장전 도중에 장전을 캔슬했을 때, 남은 탄약들을 사용할 수 있도록 조취, 그리고 탄창을 뺀 상태(1발)에서 사격했을 때 사격이 강제 중지되도록 처리
            if (ReloadType == 2)
            {
                StopFireUntillReloadingTactical = true;
                ReloadCompleteUI = false;
            }

            if (ReloadTacticalComplete == true)
            {
                ReloadTacticalComplete = false;

                AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
                AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
                Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
                Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
                AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
                AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
                AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

                ammoInMagagine = -1;
                ReloadOneTime = 0;
                ReloadState = 0;
                ReloadType = 0;
                AfterReloadOff(); //장전 상태 해제 전달

                animator.SetFloat("NoLED", 0);
                animator.SetFloat("ReloadType", 0);
                reloading = false;
                NotReloaded = false;
                ReloadCompleteUI = false;
                this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete3(); //Debug 탄창 무기류 전술 재장전 완료
            }

            //장전으로 캔슬시킨 다른 작업들의 장전 신호를 되돌리기
            if (ReloadUITime <= 0.1f)
            {
                ReloadUITime += Time.deltaTime;
                AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
                AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);
                UsingTask = false;

                GetComponent<Movement>().ReloadTime(false);
                GetComponent<HeavyWeaponSupport>().ReloadTime(false);

                //체인지 스킬 제한 목록
                GetComponent<ArthesL775Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
                GetComponent<UGG98Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
                GetComponent<Hydra56Controller>().ReloadTime(false); //체인지 스킬 사용 해제 전달
                GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달

                GetComponent<VM5GrenadeController>().ReloadTime(false); //스킬 사용 해제 전달
            }

            if (GunType == 1)
            {
                if (MagazineReload1 != null)
                    StopCoroutine(MagazineReload1);
                if (MagazineReload2 != null)
                    StopCoroutine(MagazineReload2);
                if (MagazineReload3 != null)
                    StopCoroutine(MagazineReload3);
                if (reloadContinueTactical != null)
                    StopCoroutine(reloadContinueTactical);
                if (reloadContinueFull1 != null)
                    StopCoroutine(reloadContinueFull1);
                if (reloadContinueFull2 != null)
                    StopCoroutine(reloadContinueFull2);
                if (reloadContinueAddAmmo1 != null)
                    StopCoroutine(reloadContinueAddAmmo1);
                if (reloadContinueAddAmmo2 != null)
                    StopCoroutine(reloadContinueAddAmmo2);

                animator.SetBool("DT-37 Reload", false);
                animator.SetBool("DT-37 Reload R", false);
                ReloadOneTime = 0;
            }
            else if (GunType == 1000)
            {
                if (shotgunReload != null)
                    StopCoroutine(shotgunReload);
                if (shotgunReloadAfterGetAmmo != null)
                    StopCoroutine(shotgunReloadAfterGetAmmo);

                animator.SetBool("Reload Stop", true);
                animator.SetBool("DS-65 Reload(ready)", false);
                animator.SetBool("DS-65 Reload(input ammo)", false);
            }
            else if (GunType == 2000)
            {
                if (MagazineReload1 != null)
                    StopCoroutine(MagazineReload1);
                if (MagazineReload2 != null)
                    StopCoroutine(MagazineReload2);
                if (MagazineReload3 != null)
                    StopCoroutine(MagazineReload3);

                animator.SetFloat("DP-9007 Reload", 0);
                ReloadOneTime = 0;
            }
            if (SubGunTypeFront >= 1)
            {
                if (SubGunReload1 != null)
                    StopCoroutine(SubGunReload1);
                if (SubGunReload2 != null)
                    StopCoroutine(SubGunReload2);
                if (SubGunReload3 != null)
                    StopCoroutine(SubGunReload3);
                if (SubGunReloadContinueFull1 != null)
                    StopCoroutine(SubGunReloadContinueFull1);
                if (SubGunReloadContinueFull2 != null)
                    StopCoroutine(SubGunReloadContinueFull2);
                if (SubGunReloadContinueFull3 != null)
                    StopCoroutine(SubGunReloadContinueFull3);
                if (SubGunReloadContinueTactical1 != null)
                    StopCoroutine(SubGunReloadContinueTactical1);
                if (SubGunReloadContinueTactical2 != null)
                    StopCoroutine(SubGunReloadContinueTactical2);
                if (SubGunReloadContinueAddAmmo1 != null)
                    StopCoroutine(SubGunReloadContinueAddAmmo1);
                if (SubGunReloadContinueAddAmmo2 != null)
                    StopCoroutine(SubGunReloadContinueAddAmmo2);
                if (SubGunReloadHalf1 != null)
                    StopCoroutine(SubGunReloadHalf1);
                if (SubGunReloadHalf2 != null)
                    StopCoroutine(SubGunReloadHalf2);
                if (SubGunHalfReload1 != null)
                    StopCoroutine(SubGunHalfReload1);
                if (SubGunHalfReload2 != null)
                    StopCoroutine(SubGunHalfReload2);
                if (SubGunReloadComplete != null)
                    StopCoroutine(SubGunReloadComplete);

                if (ReloadState == 1) //기관단총 앞 탄창만 제거했을 경우
                {
                    animator.SetFloat("subGun active2", 1);
                    animator.SetFloat("subGunReload", 0);
                    animator.SetFloat("subGun Reload back", 0);
                }

                if (ReloadType == 1 && ReloadType == 3) //기관단총 전술 재장전을 제외한 나머지에서 장전하다 캔슬했을 경우
                {
                    animator.SetFloat("NoLED", 0);
                    animator.SetFloat("NoLED2", 0);
                }

                if (ReloadState2 == 1)
                    animator.SetFloat("subGun Magazine active", 0);

                animator.SetFloat("subGunReload", 0);
                animator.SetBool("subMachineGun idle", false);
                ReloadOneTime = 0;

                if (ReloadType == 1 && ReloadState == 2 && ReloadCancleTime == 0) //기관단총 앞 총까지 탄창을 넣은 직후에 캔슬했을 경우(완전 재장전)
                {
                    ReloadCancleTime += Time.deltaTime;
                    Debug.Log("완전 재장전 캔슬");
                    if (SubGunTypeBack == 1)
                        animator.SetFloat("subGun active2", 1);
                    animator.SetFloat("subGun Reload back", 0);
                    animator.SetFloat("subGunReload", 0);

                    if (SubGunTypeFront == 1)
                    {
                        if (ViewSubMachineGunMagazineFront > 0)
                            animator.SetFloat("NoLED", 0);
                        else
                            animator.SetFloat("NoLED", 3000);
                    }
                    if (SubGunTypeBack == 1)
                    {
                        if (ViewSubMachineGunMagazineBack > 0)
                            animator.SetFloat("NoLED2", 0);
                        else
                            animator.SetFloat("NoLED2", 1);
                    }
                    //0.25f 초

                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
                    Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
                    Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

                    if (ReloadState == 2 && ReloadState2 == 1) //기관단총 앞 총까지만 탄창을 넣은 경우, 절반 재장전(기관단총 앞 : 전술 재장전, 뒤 : 완전 재장전)으로 넘긴다.
                    {
                        Debug.Log("완전 재장전 앞까지 장전 캔슬");
                        subGunReloadType1 = 0;
                        subGunReloadType2 = 1;
                    }

                    StopFireUntillReloadingTactical = false;
                    ReloadCompleteUI = false;
                    reloading = false;
                    NotReloaded = false;
                    ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                    ReloadOneTime = 0;
                    subGunCanFire1 = 0;
                    subGunCanFire2 = 0;
                    ReloadType = 0;

                    if (subGunMagazineType1 == 0)
                        animator.SetFloat("Magazine off1", 1);
                    if (subGunMagazineType2 == 0)
                        animator.SetFloat("Magazine off2", 1);
                    AfterReloadOff(); //장전 상태 해제 전달
                }
                if (ReloadType == 2 && ReloadCancleTime == 0) //기관단총 전술 재장전 도중에 캔슬했을 경우
                {
                    ReloadCancleTime += Time.deltaTime;
                    Debug.Log("전술 재장전 캔슬");
                    if (SubGunTypeBack == 1)
                        animator.SetFloat("subGun active2", 1);
                    animator.SetFloat("subGun Reload back", 0);
                    animator.SetFloat("subGunReload", 0);
                    //0.25f 초

                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
                    Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
                    Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

                    if (ReloadState == 2 && ReloadState2 == 2) //기관단총 뒷 총까지 탄창을 넣은 경우, 장전을 모두 마친 것으로 간주
                    {
                        Debug.Log("뒤까지 재장전 캔슬");
                        subGunCanFire1 = 0;
                        subGunCanFire2 = 0;
                        ReloadState = 0;
                        ReloadState2 = 0;
                    }

                    StopFireUntillReloadingTactical = false;
                    ReloadCompleteUI = false;
                    reloading = false;
                    FireStopTime = 1;
                    subGunReloadType1 = 0;
                    subGunReloadType2 = 0;

                    if (SubGunTypeFront == 1)
                    {
                        if (ViewSubMachineGunMagazineFront > 0)
                            animator.SetFloat("NoLED", 0);
                        else
                            animator.SetFloat("NoLED", 3000);
                    }
                    if (SubGunTypeBack == 1)
                    {
                        if (ViewSubMachineGunMagazineBack > 0)
                            animator.SetFloat("NoLED2", 0);
                        else
                            animator.SetFloat("NoLED2", 1);
                    }

                    if (subGunMagazineType1 == 0)
                        animator.SetFloat("Magazine off1", 1);
                    if (subGunMagazineType2 == 0)
                        animator.SetFloat("Magazine off2", 1);
                    AfterReloadOff(); //장전 상태 해제 전달
                }
                if (ReloadType == 3 && ReloadCancleTime == 0) //기관단총 보급 후, 재장전 도중에 캔슬했을 경우
                {
                    ReloadCancleTime += Time.deltaTime;
                    Debug.Log("보급 후, 재장전 캔슬");
                    if (SubGunTypeBack == 1)
                        animator.SetFloat("subGun active2", 1);
                    animator.SetFloat("subGun Reload back", 0);
                    animator.SetFloat("subGunReload", 0);

                    if (SubGunTypeFront == 1)
                    {
                        if (ViewSubMachineGunMagazineFront > 0)
                            animator.SetFloat("NoLED", 0);
                        else
                            animator.SetFloat("NoLED", 3000);
                    }
                    if (SubGunTypeBack == 1)
                    {
                        if (ViewSubMachineGunMagazineBack > 0)
                            animator.SetFloat("NoLED2", 0);
                        else
                            animator.SetFloat("NoLED2", 1);
                    }
                    //0.25f 초

                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
                    Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
                    Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
                    AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

                    if (ReloadState == 2 && ReloadState2 == 1) //앞 총까지만 탄창을 넣고 캔슬
                    {
                        Debug.Log("보급 후, 재장전 앞까지 캔슬");
                        ReloadType = 0;
                        subGunMagazineType2 = 0;
                        subGunCanFire1 = 0;
                        subGunCanFire2 = 1;
                        ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                    }
                    else if (ReloadState == 2 && ReloadState2 == 2) //뒷 총까지 탄창을 넣고 캔슬
                    {
                        Debug.Log("보급 후, 재장전 뒤까지 캔슬");
                        ReloadState = 0;
                        ReloadState2 = 0;
                        ReloadType = 0;
                        subGunMagazineType2 = 0;
                        subGunCanFire1 = 0;
                        subGunCanFire2 = 0;
                        ammoInMagagine = 0;
                    }

                    StopFireUntillReloadingTactical = false;
                    ReloadCompleteUI = false;
                    reloading = false;
                    AfterAddAmmo = false;
                    ReloadOneTime = 0;

                    if (subGunMagazineType1 == 0)
                        animator.SetFloat("Magazine off1", 1);
                    if (subGunMagazineType2 == 0)
                        animator.SetFloat("Magazine off2", 1);
                    AfterReloadOff(); //장전 상태 해제 전달
                }
            }
        }
        else
        {
            if (animator.GetBool("Reload Stop") == true)
                animator.SetBool("Reload Stop", false);
        }
        
        //장전이 완료가 안된 상태
        if (NotReloaded == true && reloading == false)
        {
            if (SubGunTypeFront >= 1)
            {
                if (SubGunTypeFront == 1 && ReloadState == 1)
                    animator.SetFloat("Magazine off1", 1);
                if (SubGunTypeBack == 1 && ReloadState2 == 1)
                    animator.SetFloat("Magazine off2", 1);
            }
        }

        if (SniperMuzzleFlash == true)
        {
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
        }

        /////////////////////////////////////////////////////////////////////////////////

        if (AmmoAmount > 0 && guidingStopOrder == false)
        {
            //사격
            SW_Fire();

            //기관단총 사격 제어
            if (ViewSubMachineGunMagazineFront <= 0)
            {
                animator.SetFloat("subGunFront fire", 0);
                if (subGunFireStopTime1 == 0)
                {
                    if (SubGunTypeFront == 1)
                    {
                        subGunFireStopTime1 = 1;
                        subGunCanFire1 = 1;
                        DelayShot = 0;
                        subGunMuzzleFlash1.SetActive(false);
                        animator.SetFloat("NoLED", 3000);
                        SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                        GameObject Smoke1 = Instantiate(CGD27FrontSmokePrefab, CGD27FrontSmokePos.position, CGD27FrontSmokePos.rotation);
                        Destroy(Smoke1, 1.5f);
                    }
                }
            }
            if (ViewSubMachineGunMagazineBack <= 0)
            {
                animator.SetFloat("subGunBack fire", 0);
                if (subGunFireStopTime2 == 0)
                {
                    if (SubGunTypeBack == 1)
                    {
                        subGunFireStopTime2 = 1;
                        subGunCanFire2 = 1;
                        DelayShot = 0;
                        subGunMuzzleFlash2.SetActive(false);
                        animator.SetFloat("NoLED2", 1);
                        SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                        GameObject Smoke2 = Instantiate(CGD27BackSmokePrefab, CGD27BackSmokePos.position, CGD27BackSmokePos.rotation);
                        Destroy(Smoke2, 1.5f);
                    }
                }
            }

            if (NotReloaded == false)
            {
                //탄창 무기류 사격 후 재장전
                if (ammoInMagagine == AmmoPerMagazine && reloading == false && NeedAddAmmo == false && AfterAddAmmo == false && AmmoAmount > 0 && SnipergunAfterFire == false)
                {
                    if (GunType > 0)
                    {
                        if (GunType < 1000 || GunType >= 2000)
                        {
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ammoInMagagine = AmmoPerMagazine;
                                MagazineReload1 = StartCoroutine(SW_reload());
                            }
                        }
                    }
                }

                //탄창 무기류 전술 재장전
                if (isReload || Input.GetKeyDown(KeyCode.R) && SnipergunAfterFire == false)
                {
                    if (GunType > 0 && ReloadState == 0 && reloading == false && AfterAddAmmo == false && NeedAddAmmo == false && ViewAmmo > 0 && ViewSW06_Magazine != AmmoPerMagazine + 1)
                        if (GunType < 1000 || GunType >= 2000)
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                //ammoInMagagine = AmmoPerMagazine - ViewSW06_Magazine;
                                MagazineReload2 = StartCoroutine(SW_reload2());
                            }
                }

                //기관단총 탄창 무기류 사격 후 재장전
                if (ammoInMagagine == AmmoPerMagazine && reloading == false && NeedAddAmmo == false && AfterAddAmmo == false && AmmoAmount > 0)
                {
                    if (SubGunTypeFront > 0 || SubGunTypeBack > 0)
                    {
                        if (ReloadOneTime == 0 && ReloadState == 0 && ReloadState2 >= 0)
                        {
                            Debug.Log("완전 재장전");
                            ReloadOneTime += Time.deltaTime;
                            ReloadCancleTime = 0;
                            ReloadUITime = 0;
                            ZeroAmmoUITime = 0;
                            ZeroMagagineUITime = 0;
                            if (subGunCanFire1 == 1 && subGunCanFire2 == 1 && ReloadState2 == 0) //탄창이 둘 다 비워진 상태에서 재장전
                                SubGunReload1 = StartCoroutine(subGunReload());
                            if (subGunCanFire1 == 0 && subGunCanFire2 == 1 || ReloadState2 == 1) //사격 후 재장전(기관단총 앞 탄창 넣고 뒷 탄창이 비워진 상태에서 사격후 재장전 상태)
                                SubGunReloadContinueFull3 = StartCoroutine(subGunReloadContinueFull3());
                        }
                        else if (ReloadOneTime == 0 && ReloadState == 2 && ReloadState2 == 1) //사격 후 재장전(기관단총 앞 탄창 넣고 뒷 탄창이 비워진 상태에서 사격후 재장전 상태)(완전 및 보급 후 재장전)
                        {
                            Debug.Log("완전 부분 재장전1");
                            ReloadOneTime += Time.deltaTime;
                            ReloadCancleTime = 0;
                            ReloadUITime = 0;
                            ZeroAmmoUITime = 0;
                            ZeroMagagineUITime = 0;
                            SubGunReloadContinueFull3 = StartCoroutine(subGunReloadContinueFull3());
                        }
                        else if (ReloadOneTime == 0 && ReloadState == 1 && ReloadState2 == 0) //사격 후 재장전(기관단총 앞 탄창만 뺀 상태에서 사격후 재장전 상태)(전술 재장전)
                        {
                            Debug.Log("완전 부분 재장전2");
                            ReloadOneTime += Time.deltaTime;
                            ReloadCancleTime = 0;
                            ReloadUITime = 0;
                            ZeroAmmoUITime = 0;
                            ZeroMagagineUITime = 0;
                            SubGunReloadContinueFull1 = StartCoroutine(subGunReloadContinueFull1());
                        }
                    }
                }

                //기관단총 탄창 무기류 전술 재장전
                if (isReload || Input.GetKeyDown(KeyCode.R) && SnipergunAfterFire == false)
                {
                    if (reloading == false && AfterAddAmmo == false && NeedAddAmmo == false && ViewAmmo > 0 && ViewSW06_Magazine != AmmoPerMagazine + 2)
                        if (SubGunTypeFront > 0 || SubGunTypeBack > 0)
                        {
                            if (ReloadOneTime == 0)
                            {
                                if (subGunReloadType1 == 0 && subGunReloadType2 == 0 && subGunCanFire1 == 0 && subGunCanFire2 == 0)
                                {
                                    Debug.Log("전술 재장전");
                                    ReloadOneTime += Time.deltaTime;
                                    isReload = false;
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    if (ViewSubMachineGunMagazineFront < SubMachineGunFrontAmmoPerMagazine + 1 && ViewSubMachineGunMagazineBack < SubMachineGunBackAmmoPerMagazine + 1)
                                        SubGunReload2 = StartCoroutine(subGunReload2()); //둘 다 전술 재장전
                                    else if (ViewSubMachineGunMagazineFront < SubMachineGunFrontAmmoPerMagazine + 1 && ViewSubMachineGunMagazineBack == SubMachineGunBackAmmoPerMagazine + 1)
                                        SubGunHalfReload1 = StartCoroutine(subGunHalfReload1()); //앞부분 전술 재장전
                                    else if (ViewSubMachineGunMagazineFront == SubMachineGunFrontAmmoPerMagazine + 1 && ViewSubMachineGunMagazineBack < SubMachineGunBackAmmoPerMagazine + 1)
                                        SubGunHalfReload2 = StartCoroutine(subGunHalfReload2()); //뒷부분 전술 재장전
                                }
                                else if (subGunReloadType1 == 0 && subGunReloadType2 == 1 || subGunCanFire1 == 0 && subGunCanFire2 == 1) //절반 재장전(기관단총 앞 : 전술 재장전, 뒤 : 완전 재장전)
                                {
                                    Debug.Log("절반 재장전1(전술)");
                                    ReloadOneTime += Time.deltaTime;
                                    isReload = false;
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadHalf1 = StartCoroutine(subGunReloadHalf1());
                                }
                                else if (subGunReloadType1 == 1 && subGunReloadType2 == 0 || subGunCanFire1 == 1 && subGunCanFire2 == 0) //절반 재장전(기관단총 앞 : 완전 재장전, 뒤 : 전술 재장전)
                                {
                                    Debug.Log("절반 재장전2(전술)");
                                    ReloadOneTime += Time.deltaTime;
                                    isReload = false;
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadHalf2 = StartCoroutine(subGunReloadHalf2());
                                }
                                else if (subGunCanFire1 == 1 && subGunCanFire2 == 1) //사격 후 재장전(전술 재장전 도중 두 탄창을 모두 빼고나서 사격한 뒤에 완전 재장전)
                                {
                                    Debug.Log("완전 재장전2");
                                    ReloadOneTime += Time.deltaTime;
                                    isReload = false;
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadComplete = StartCoroutine(subGunReloadComplete());
                                }
                            }
                        }
                }

                //샷건 무기류 사격 후 재장전
                if (ammoInMagagine == AmmoPerMagazine && StopReload == false || ViewSW06_Magazine == 0 && StopReload == false)
                {
                    if (GunType > 0 && reloading == false && ShotgunAfterFire == false && AfterAddAmmo == false && NeedAddAmmo == false && AmmoAmount > 0)
                        if (GunType >= 1000 && GunType < 2000)
                        {
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ShotgunReloadCompleteType = true;
                                ammoInMagagine = AmmoPerMagazine;
                                shotgunReload = StartCoroutine(ShotgunReload());
                            }
                        }
                }

                //샷건 무기류 중간 재장전
                if (isReload && AmmoAmount > 0 && StopReload == false || Input.GetKeyDown(KeyCode.R))
                {
                    if (GunType > 0 && reloading == false && ShotgunAfterFire == false && AfterAddAmmo == false && NeedAddAmmo == false && AmmoAmount > 0 && ViewSW06_Magazine != AmmoPerMagazine && ViewAmmo != 0)
                        if (GunType >= 1000 && GunType < 2000)
                        {
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ShotgunReloadCompleteType = false;
                                ammoInMagagine = AmmoPerMagazine - ViewSW06_Magazine;
                                shotgunReload = StartCoroutine(ShotgunReload());
                            }
                        }
                }

                //탄창 무기류 보급후 재장전
                if (isReload && StopReload == false || Input.GetKeyDown(KeyCode.R))
                {
                    if (GunType > 0 && reloading == false && NeedAddAmmo == false && AfterAddAmmo == true)
                    {
                        if (GunType < 1000 || GunType >= 2000)
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ammoInMagagine = AmmoPerMagazine;
                                MagazineReload3 = StartCoroutine(SW_reload1());
                            }
                    }
                }

                //기관단총 탄창 무기류 보급후 재장전
                if (isReload && StopReload == false || Input.GetKeyDown(KeyCode.R))
                {
                    if (reloading == false && NeedAddAmmo == false && AfterAddAmmo == true)
                    {
                        if (SubGunTypeFront > 0 || SubGunTypeBack > 0)
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                ReloadCancleTime = 0;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ammoInMagagine = AmmoPerMagazine;
                                SubGunReload3 = StartCoroutine(subGunReload3());
                            }
                    }
                }

                //샷건 무기류 보급 후 재장전
                if (isReload || Input.GetKeyDown(KeyCode.R))
                {
                    if (GunType > 0 && reloading == false && NeedAddAmmo == false && AfterAddAmmo == true)
                    {
                        if (GunType >= 1000 && GunType < 2000)
                        {
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                ReloadUITime = 0;
                                ZeroAmmoUITime = 0;
                                ZeroMagagineUITime = 0;
                                ammoInMagagine = AmmoPerMagazine;
                                shotgunReloadAfterGetAmmo = StartCoroutine(ShotgunReloadAfterGetAmmo());
                            }
                        }
                    }
                }
            }
            else
            {
                //탄창 무기류 재장전 이어서 하기
                if (isReload)
                {
                    if (GunType > 0 && reloading == false)
                        if (GunType < 1000 || GunType >= 2000)
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                if (ReloadState == 1 && ReloadType == 1) //사격 후 재장전(탄창 뺀 상태)
                                {
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    reloadContinueFull1 = StartCoroutine(ReloadContinueFull1());
                                }
                                else if (ReloadState == 2 && ReloadType == 1) //사격 후 재장전(탄창 갈아끼운 상태)
                                {
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    reloadContinueFull2 = StartCoroutine(ReloadContinueFull2());
                                }
                                else if (ReloadState == 1 && ReloadType == 2) //전술 재장전(탄창 뺀 상태)
                                {
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    reloadContinueTactical = StartCoroutine(ReloadContinueTactical());
                                }
                                else if (ReloadState == 1 && ReloadType == 3) //보급후 재장전(탄창 뺀 상태)
                                {
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    reloadContinueAddAmmo1 = StartCoroutine(ReloadContinueAddAmmo1());
                                }
                                else if (ReloadState == 2 && ReloadType == 3) //보급후 재장전(탄창 갈아끼운 상태)
                                {
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    reloadContinueAddAmmo2 = StartCoroutine(ReloadContinueAddAmmo2());
                                }
                            }
                }

                //기관단총 탄창 무기류 재장전 이어서 하기
                if (isReload)
                {
                    if (reloading == false)
                        if (SubGunTypeFront > 0 || SubGunTypeBack > 0)
                            if (ReloadOneTime == 0)
                            {
                                ReloadOneTime += Time.deltaTime;
                                isReload = false;
                                if (ReloadState == 1 && ReloadState2 == 0 && ReloadType == 1) //사격 후 재장전(기관단총 앞 탄창 뺀 상태)
                                {
                                    Debug.Log("사격후 재장전1(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadContinueFull1 = StartCoroutine(subGunReloadContinueFull1());
                                }
                                else if (ReloadState == 1 && ReloadState2 == 1 && ReloadType == 1) //사격 후 재장전(기관단총 뒷 탄창 뺀 상태)
                                {
                                    Debug.Log("사격후 재장전2(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadContinueFull2 = StartCoroutine(subGunReloadContinueFull2());
                                }
                                else if (ReloadState == 1 && ReloadState2 == 0 && ReloadType == 2) //전술 재장전(기관단총 앞 탄창 뺀 상태)
                                {
                                    Debug.Log("전술 재장전1(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    if (ViewSubMachineGunMagazineFront > 0)
                                        SubGunReloadContinueTactical1 = StartCoroutine(subGunReloadContinueTactical1()); //사격을 안하고, 즉시 전술 재장전
                                    else
                                        SubGunReloadHalf2 = StartCoroutine(subGunReloadHalf2()); //앞총 탄창을 빼고 나서 사격한 뒤, 뒷총의 탄이 남은 상태에서 절반 재장전(기관단총 앞 : 완전 재장전, 뒤 : 전술 재장전)
                                }
                                else if (ReloadState == 1 && ReloadState2 == 1 && ReloadType == 2) //전술 재장전(기관단총 뒷 탄창 뺀 상태)
                                {
                                    Debug.Log("전술 재장전2(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadContinueTactical2 = StartCoroutine(subGunReloadContinueTactical2());
                                }
                                else if (ReloadState == 1 && ReloadState2 == 0 && ReloadType == 3) //보급후 재장전(기관단총 앞 탄창 뺀 상태)
                                {
                                    Debug.Log("보급 재장전1(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadContinueAddAmmo1 = StartCoroutine(subGunReloadContinueAddAmmo1());
                                }
                                else if (ReloadState == 1 && ReloadState2 == 1 && ReloadType == 3) //보급후 재장전(기관단총 뒷 탄창 뺀 상태)
                                {
                                    Debug.Log("보급 재장전2(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadContinueAddAmmo2 = StartCoroutine(subGunReloadContinueAddAmmo2());
                                }
                                else if (ReloadState == 2 && ReloadState2 == 1 && ReloadType == 0) //보급 후, 재장전 도중 앞 총 탄창까지만 장착하고 나서 절반 재장전
                                {
                                    Debug.Log("비 탄창 및 보급 재장전3(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    ammoInMagagine = 0;
                                    if (ViewSubMachineGunMagazineFront > 0 && ViewSubMachineGunMagazineBack == 0)
                                        SubGunReloadHalf1 = StartCoroutine(subGunReloadHalf1()); //기관단총 앞 : 전술 재장전, 뒤 : 완전 재장전
                                    else if (ViewSubMachineGunMagazineFront > SubMachineGunFrontAmmoPerMagazine && ViewSubMachineGunMagazineBack == 1)
                                        SubGunHalfReload2 = StartCoroutine(subGunHalfReload2()); //기관단총 전술 재장전 도중 앞총 탄창까지 끼우고 나서 뒷총 전술 재장전만 남은 상태
                                }
                                else if (ReloadState == 1 && ReloadState2 == 0 && ReloadType == 0) //앞총의 탄이 남아있고, 뒷총의 탄이 없을 때, 절반 재장전(기관단총 앞 : 전술 재장전, 뒤 : 완전 재장전)
                                {
                                    Debug.Log("절반 재장전1");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunReloadHalf1 = StartCoroutine(subGunReloadHalf1());
                                }
                                else if (ReloadState == 0 && ReloadState2 == 1 || ReloadState == 2 && ReloadState2 == 1 && ReloadType == 2) //뒷 전술 재장전
                                {
                                    Debug.Log("뒷 전술 재장전(캔슬)");
                                    ReloadCancleTime = 0;
                                    ReloadUITime = 0;
                                    ZeroAmmoUITime = 0;
                                    ZeroMagagineUITime = 0;
                                    SubGunHalfReload2 = StartCoroutine(subGunHalfReload2());
                                }
                            }
                }
            }
        }

        //보급후, 재장전까지 및 레이져 지시동안 사격 불가
        if (/*AmmoAmount > 0 && AfterAddAmmo == true && reloading == false || */guidingStopOrder == true)
        {
            if (GunType == 1)
            {
                animator.SetFloat("Gun fire", 0);
                animator.SetBool("SW-06_Effect1", false);
                animator.SetBool("SW-06_Effect2", false);
                animator.SetBool("SW-06_Effect3", false);
                animator.SetBool("SW-06_Effect4", false);

                if (MagazineReload1 != null)
                    StopCoroutine(MagazineReload1);
                if (MagazineReload2 != null)
                    StopCoroutine(MagazineReload2);
                if (MagazineReload3 != null)
                    StopCoroutine(MagazineReload3);

                animator.SetBool("DT-37 Reload", false);
                animator.SetBool("DT-37 Reload R", false);
                ReloadOneTime = 0;
            }
            else if (GunType == 1000)
            {
                if (dS65Fire != null)
                    StopCoroutine(dS65Fire);

                animator.SetFloat("Gun fire", 0);
                animator.SetBool("SW-06_Effect1", false);
                animator.SetBool("SW-06_Effect2", false);
                animator.SetBool("SW-06_Effect3", false);
                animator.SetBool("SW-06_Effect4", false);

                if (shotgunReload != null)
                    StopCoroutine(shotgunReload);
                if (shotgunReloadAfterGetAmmo != null)
                    StopCoroutine(shotgunReloadAfterGetAmmo);

                animator.SetBool("DS-65 Reload(ready)", false);
                animator.SetBool("DS-65 Reload(input ammo)", false);
                ReloadOneTime = 0;
            }
            else if (GunType == 2000)
            {
                animator.SetFloat("Gun fire", 0);
                animator.SetBool("SW-06_Effect1", false);
                animator.SetBool("SW-06_Effect2", false);
                animator.SetBool("SW-06_Effect3", false);
                animator.SetBool("SW-06_Effect4", false);

                if (MagazineReload1 != null)
                    StopCoroutine(MagazineReload1);
                if (MagazineReload2 != null)
                    StopCoroutine(MagazineReload2);
                if (MagazineReload3 != null)
                    StopCoroutine(MagazineReload3);

                animator.SetFloat("DP-9007 Reload", 0);
                ReloadOneTime = 0;
            }
        }

        if (ChangeSkillEnergy <= 0)
            ChangeSkillEnergy = 0;
    }

    //사격
    public void SW_Fire()
    {
        if (isGun || Input.GetKey(KeyCode.C))
        {
            if (reloading == false && NeedAddAmmo == false && AfterAddAmmo == false && ViewSW06_Magazine > 0 && ReloadCompleteUI == false)
            {
                if (GunType == 1)
                    animator.SetFloat("Gun fire", 2);
                else if (GunType == 1000)
                {
                    if (ShotgunFireTime == 0)
                    {
                        ShotgunFireTime += Time.deltaTime;
                        dS65Fire = StartCoroutine(DS65Fire());
                    }
                }
                else if (GunType == 2000)
                    animator.SetFloat("Gun fire", 2000);

                if (SubGunTypeFront == 1 && ViewSubMachineGunMagazineFront > 0)
                    animator.SetFloat("subGunFront fire", 1);
                if (SubGunTypeBack == 1 && ViewSubMachineGunMagazineBack > 0 && DelayShot >= 0.04f)
                    animator.SetFloat("subGunBack fire", 1);

                UsingTask = true;
                GunSmokeOn = 2;

                if (timeStamp >= FireRate)
                {
                    timeStamp = 0;

                    if (GunType == 1) //DT-37 사격
                    {
                        //SoundManager.instance.SFXPlay("DT-37 Fire Sound", DT_37Fire);
                        SW06_FIreEffect(); //발사 불꽃 출력 랜덤으로 출력
                        FireStopTime = 0;
                        Shake.Instance.ShakeCamera(2, 0.1f);

                        DT37Fire = objectManager.Loader("DT37Ammo");
                        DT37Fire.transform.position = DT37Firepos.transform.position;
                        DT37Fire.transform.rotation = DT37Firepos.transform.rotation;
                        DT37Fire.GetComponent<AmmoMovement>().SetDamage(Damage); //총알에다 데미지 전달

                        AmmoMovement ammoMovement = DT37Fire.GetComponent<AmmoMovement>(); //AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 

                        ammoMovement.isHit = false; // 피격방지
                        ammoMovement.objectManager = objectManager;

                        SW06_EjectShell(); //탄피 방출

                        if (StopFireUntillReloadingTactical == true)
                        {
                            StopFireUntillReloadingTactical = false;
                            FireStopTime = 1;
                            SoundManager.instance.SFXPlay10("DT-37 Fire Sound", DT_37FireComplete);
                            GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation);
                            Destroy(Smoke, 1.5f);
                            StartCoroutine(FireActionOff());
                        }
                    }
                    else if (GunType == 1000) //DS-65 사격
                    {
                        ShotGunFire();
                        //SoundManager.instance.SFXPlay("DT-37 Fire Sound", DT_37Fire);
                    }
                    else if (GunType == 2000) //DP-9007 사격
                    {
                        SniperMuzzleFlash = false;
                        SnipergunAfterFire = true;

                        if (SniperMuzzleFlash == false)
                            SW06_FIreEffect();
                        Invoke("MuzzleFlashComplete", 0.08f);

                        Invoke("SnipeFireComplete", FireRate / 2);
                        //SoundManager.instance.SFXPlay("DT-37 Fire Sound", DT_37Fire);
                        GameObject SniperBullet = objectManager.Loader("PlayerSniperAmmo");
                        SniperBullet.transform.position = DT37Firepos.position;
                        SniperBullet.transform.rotation = DT37Firepos.rotation; //발사 총알 생성
                        SniperBullet.GetComponent<RailgunlBullet>().SetDamage(Damage); //총알에다 데미지 전달

                        RailgunlBullet RailgunBullet = SniperBullet.GetComponent<RailgunlBullet>(); //AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 

                        RailgunBullet.isHit = false; // 피격방지
                        RailgunBullet.objectManager = objectManager;

                        SW06_EjectShell(); //탄피 방출

                        GameObject Smoke = Instantiate(DP9007SmokePrefab, DP9007SmokePos.position, DP9007SmokePos.rotation);
                        Destroy(Smoke, 4);

                        if (StopFireUntillReloadingTactical == true)
                        {
                            StopFireUntillReloadingTactical = false;
                            FireStopTime = 1;
                            StartCoroutine(FireActionOff());
                        }
                    }

                    if (GunType > 0)
                    {
                        ViewSW06_Magazine -= 1;
                        ammoInMagagine += 1;
                        AmmoAmount -= 1;
                    }

                    if (ViewSW06_Magazine == 0)
                        ReloadType = 1;
                }

                if (subFrontTimeStamp >= subFrontFireRate)
                {
                    subFrontTimeStamp = 0;

                    if (SubGunTypeFront == 1 && ViewSubMachineGunMagazineFront > 0) //앞 CGD-27 사격
                    {
                        FireStopTime = 0;
                        subGunFireStopTime1 = 0;
                        NotReloaded = false;
                        subGunMuzzleFlash1.SetActive(true);

                        GameObject SMGBullet = objectManager.Loader("PlayerSMGAmmo");
                        SMGBullet.transform.position = CGD27FirePosFront.position;
                        SMGBullet.transform.rotation = transform.rotation; //발사 총알 생성
                        SMGBullet.GetComponent<AmmoMovement>().SetDamage(Damage); //총알에다 데미지 전달

                        AmmoMovement ammoMovement = SMGBullet.GetComponent<AmmoMovement>(); //AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 
                        ammoMovement.isHit = false; // 피격방지 
                        ammoMovement.objectManager = objectManager;
                        EjectShellFront(); //탄피 방출

                        ViewSW06_Magazine--;
                        ammoInMagagine++;
                        AmmoAmount--;
                        ViewSubMachineGunMagazineFront--;

                        if (StopFireUntillReloadingTactical == true)
                        {
                            StopFireUntillReloadingTactical = false;
                            FireStopTime = 1;
                            SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                            GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation);
                            Destroy(Smoke, 1.5f);
                            StartCoroutine(FireActionOff());
                        }
                    }

                    if (ViewSW06_Magazine == 0)
                        ReloadType = 1;
                }
                if (subBackTimeStamp >= subBackFireRate)
                {
                    DelayShot += Time.deltaTime;

                    if (DelayShot >= 0.04f)
                    {
                        subBackTimeStamp = 0;

                        if (SubGunTypeBack == 1 && ViewSubMachineGunMagazineBack > 0) //뒤 CGD-27 사격
                        {
                            FireStopTime = 0;
                            subGunFireStopTime2 = 0;
                            NotReloaded = false;
                            subGunMuzzleFlash2.SetActive(true);

                            GameObject SMGBullet = objectManager.Loader("PlayerSMGAmmo");
                            SMGBullet.transform.position = CGD27FirePosBack.position;
                            SMGBullet.transform.rotation = transform.rotation; //발사 총알 생성
                            SMGBullet.GetComponent<AmmoMovement>().SetDamage(Damage); //총알에다 데미지 전달

                            AmmoMovement ammoMovement = SMGBullet.GetComponent<AmmoMovement>(); //AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 
                            ammoMovement.isHit = false; // 피격방지 
                            ammoMovement.objectManager = objectManager;
                            EjectShellBack(); //탄피 방출

                            ViewSW06_Magazine--;
                            ammoInMagagine++;
                            AmmoAmount--;
                            ViewSubMachineGunMagazineBack--;

                            if (StopFireUntillReloadingTactical == true)
                            {
                                StopFireUntillReloadingTactical = false;
                                FireStopTime = 1;
                                SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                                GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation);
                                Destroy(Smoke, 1.5f);
                                StartCoroutine(FireActionOff());
                            }
                        }
                    }

                    if (ViewSW06_Magazine == 0)
                        ReloadType = 1;
                }
            }
        }
        else //사격 버튼을 땠을 때 사격 중지
        {
            if (GunType > 0 && GunType < 1000)
            {
                animator.SetFloat("Gun fire", 0);
                animator.SetBool("SW-06_Effect1", false);
                animator.SetBool("SW-06_Effect2", false);
                animator.SetBool("SW-06_Effect3", false);
                animator.SetBool("SW-06_Effect4", false);
                if (FireStopTime == 0 && reloading == false && NeedAddAmmo == false)
                {
                    FireStopTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay10("DT-37 Fire Sound", DT_37FireComplete);
                }
            }
            else if (GunType >= 2000 && GunType < 3000 && SnipergunAfterFire == false)
            {
                animator.SetFloat("Gun fire", 0);
                animator.SetBool("SW-06_Effect1", false);
                animator.SetBool("SW-06_Effect2", false);
                animator.SetBool("SW-06_Effect3", false);
                animator.SetBool("SW-06_Effect4", false);
            }

            if (SubGunTypeFront >= 1 || SubGunTypeBack >= 1)
            {
                animator.SetFloat("subGunFront fire", 0);
                animator.SetFloat("subGunBack fire", 0);

                if (FireStopTime == 0)
                {
                    if (subGunFireStopTime1 == 0)
                    {
                        if (SubGunTypeFront == 1)
                        {
                            subGunFireStopTime1 = 1;
                            DelayShot = 0;
                            subGunMuzzleFlash1.SetActive(false);
                            SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                            GameObject Smoke1 = Instantiate(CGD27FrontSmokePrefab, CGD27FrontSmokePos.position, CGD27FrontSmokePos.rotation);
                            Destroy(Smoke1, 1.5f);
                        }
                    }
                    if (subGunFireStopTime2 == 0)
                    {
                        if (SubGunTypeBack == 1)
                        {
                            subGunFireStopTime2 = 1;
                            DelayShot = 0;
                            subGunMuzzleFlash2.SetActive(false);
                            SoundManager.instance.SFXPlay10("CGD27 Fire Complete", CGD27FireComplete);
                            GameObject Smoke2 = Instantiate(CGD27BackSmokePrefab, CGD27BackSmokePos.position, CGD27BackSmokePos.rotation);
                            Destroy(Smoke2, 1.5f);
                        }
                    }
                }                   
            }

            if (reloading == false)
                UsingTask = false;
        }

        //샷건의 경우 한 발 쏘았을 때 한 번씩 사격이 중지되도록 처리. 연속으로 사격버튼을 누르고 있으면 알아서 연속으로 발사한다.
        if (GunType >= 1000 && GunType < 2000 && ShotgunAfterFire == false)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
        }

        //사격직후 연기 발생(특정 총기에 해당)
        if(!isGun && GunSmokeOn == 2 || Input.GetKeyUp(KeyCode.C))
        {
            GunSmokeOn = 1;

            if (reloading == false && GunSmokeOn == 1)
            {
                if (GunType == 1) //DT-37
                {
                    GunSmokeOn = 0;
                    GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //총쏜 후 연기 (오브젝트 풀링 안해도 됨)
                    Destroy(Smoke, 1.5f);
                }
            }
        }
    }

    //스나이퍼총 사격할 때 머즐플래쉬를 한 번만 낼 수 있도록 조취
    void MuzzleFlashComplete()
    {
        SniperMuzzleFlash = true;
    }

    //스나이퍼총 사격 완료표시. 이 후에 장전을 할 수 있도록 조취
    void SnipeFireComplete()
    {
        SnipergunAfterFire = false;
    }

    //발사 불꽃 출력 랜덤으로 출력
    void SW06_FIreEffect()
    {
        fireEffect = Random.Range(1, 4);

        if (fireEffect == 1)
        {
            animator.SetBool("SW-06_Effect1", true);
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("SW-06_Effect2", true);
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("SW-06_Effect3", true);
        }
        else if (fireEffect == 4)
        {
            animator.SetBool("SW-06_Effect4", true);
        }
    }

    //탄창 무기류 사격 후 재장전
    public IEnumerator SW_reload()
    {
        if (FireStopTime == 0)
        {
            SoundManager.instance.SFXPlay10("DT-37 Fire Sound", DT_37FireComplete);
            GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation);
            Destroy(Smoke, 1.5f);
        }
        FireStopTime = 1;
        ReloadType = 1;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        if (GunType == 1)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetBool("DT-37 Reload", true);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 3.566f);

            yield return new WaitForSeconds(0.816f);
            ReloadState = 1;

            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            RealoadFull(); //완전 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.35f);
            animator.SetBool("DT-37 Reload", false);
        }
        else if (GunType == 2000)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetFloat("DP-9007 Reload", 4);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 4.15f);

            yield return new WaitForSeconds(0.916f);
            ReloadState = 1;
            yield return new WaitForSeconds(1.66f);
            RealoadFull(); //완전 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.57f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFire(); //Debug SW-06 재장전

        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete(); //Debug SW-06 재장전 완료
    }

    //탄창 무기류 보급후 재장전
    public IEnumerator SW_reload1()
    {
        FireStopTime = 1;
        ReloadType = 3;
        AfterReload(); //장전 상태 전달
        reloading = true;
        NeedAddAmmo = false;
        ReloadCompleteUI = true;

        if (GunType == 1)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetBool("DT-37 Reload", true);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 3.566f);

            yield return new WaitForSeconds(0.55f);
            ReloadState = 1;
            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            ReloadFullAfterSupport(); //보급후 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.35f);
            animator.SetBool("DT-37 Reload", false);
        }
        else if (GunType == 2000)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetFloat("DP-9007 Reload", 4);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 4.15f);

            yield return new WaitForSeconds(0.916f);
            ReloadState = 1;
            yield return new WaitForSeconds(1.66f);
            ReloadFullAfterSupport(); //보급후 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.57f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFire2(); //Debug SW-06 보급후 재장전

        ReloadType = 0;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        reloading = false;
        AfterAddAmmo = false;
        ReloadCompleteUI = false;
        AfterReloadOff(); //장전 상태 해제 전달
        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete2(); //Debug SW-06 보급후 재장전 완료
    }

    //탄창 무기류 전술 재장전
    public IEnumerator SW_reload2()
    {
        FireStopTime = 1;
        ReloadType = 2;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        if (isGun || Input.GetKey(KeyCode.C)) //사격 도중 전술 재장전
        {
            if(reloading == true)
            {
                if (GunType == 0 || GunType == 1)
                {
                    GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); // 총쏜 후 연기 (오브젝트 풀링 안해도 됨 )  
                    Destroy(Smoke, 1.5f);
                }
                else if (GunType == 2000)
                {
                    GameObject Smoke = Instantiate(DP9007SmokePrefab, DP9007SmokePos.position, DP9007SmokePos.rotation);
                    Destroy(Smoke, 4);
                }
            }
        }

        if (GunType == 1)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetBool("DT-37 Reload R", true);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 2.2f);

            yield return new WaitForSeconds(0.55f);
            ReloadState = 1;
            ViewAmmo += ViewSW06_Magazine - 1;
            ViewSW06_Magazine = 1;
            ammoInMagagine = AmmoPerMagazine - 1;
            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            ReloadTactical(); //전술 재장전 탄약 계산
            ReloadTacticalComplete = true;

            yield return new WaitForSeconds(0.35f);

            ammoInMagagine = -1;
            ReloadOneTime = 0;
            ReloadState = 0;
            ReloadType = 0;
            AfterReloadOff(); //장전 상태 해제 전달

            animator.SetBool("DT-37 Reload R", false);
            reloading = false;
            ReloadCompleteUI = false;
            ReloadTacticalComplete = false;
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete3(); //Debug 탄창 무기류 전술 재장전 완료
        }
        else if (GunType == 2000)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
            animator.SetFloat("DP-9007 Reload", 1);

            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 3.25f);

            yield return new WaitForSeconds(0.83f);
            ReloadState = 1;
            ViewAmmo += ViewSW06_Magazine - 1;
            ViewSW06_Magazine = 1;
            ammoInMagagine = AmmoPerMagazine - 1;
            yield return new WaitForSeconds(1.75f);
            ReloadTactical(); //전술 재장전 탄약 계산
            ReloadTacticalComplete = true;

            yield return new WaitForSeconds(0.67f);
            ammoInMagagine = -1;
            ReloadOneTime = 0;
            ReloadState = 0;
            ReloadType = 0;
            AfterReloadOff(); //장전 상태 해제 전달

            animator.SetFloat("DP-9007 Reload", 0);
            reloading = false;
            ReloadCompleteUI = false;
            ReloadTacticalComplete = false;
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete3(); //Debug 탄창 무기류 전술 재장전 완료
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);
    }

    //탄창을 뺀 상태에서 재장전 이어서하기(전술 재장전)
    public IEnumerator ReloadContinueTactical()
    {
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달
        animator.SetFloat("NoLED", 0);

        if (GunType == 1)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 1.65f);

            animator.SetFloat("ReloadType", 3);
            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            ReloadTactical(); //전술 재장전 탄약 계산
            ReloadTacticalComplete = true;

            yield return new WaitForSeconds(0.35f);

            ammoInMagagine = -1;
            ReloadOneTime = 0;
            ReloadState = 0;
            ReloadType = 0;
            AfterReloadOff(); //장전 상태 해제 전달

            animator.SetFloat("ReloadType", 0);
            reloading = false;
            NotReloaded = false;
            ReloadCompleteUI = false;
            ReloadTacticalComplete = false;
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete3(); //Debug 탄창 무기류 전술 재장전 완료
        }
        else if (GunType == 2000)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 2.25f);
            animator.SetFloat("DP-9007 Reload", 2);

            yield return new WaitForSeconds(1.58f);
            ReloadTactical(); //전술 재장전 탄약 계산
            ReloadTacticalComplete = true;

            yield return new WaitForSeconds(0.67f);

            ammoInMagagine = -1;
            ReloadOneTime = 0;
            ReloadState = 0;
            ReloadType = 0;
            AfterReloadOff(); //장전 상태 해제 전달

            animator.SetFloat("DP-9007 Reload", 0);
            reloading = false;
            NotReloaded = false;
            ReloadCompleteUI = false;
            ReloadTacticalComplete = false;
            this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete3(); //Debug 탄창 무기류 전술 재장전 완료
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);
    }

    //탄창을 뺀 상태에서 재장전 이어서하기(완전 재장전)
    public IEnumerator ReloadContinueFull1()
    {
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달
        animator.SetFloat("NoLED", 0);

        if (GunType == 1)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 2.7f);

            animator.SetFloat("ReloadType", 4);
            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            RealoadFull(); //완전 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.35f);
            animator.SetBool("DT-37 Reload", false);

            animator.SetFloat("ReloadType", 0);
        }
        else if (GunType == 2000)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 3.16f);
            animator.SetFloat("DP-9007 Reload", 3);

            yield return new WaitForSeconds(1.58f);
            RealoadFull(); //완전 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.58f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadType = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete(); //Debug SW-06 재장전 완료
    }

    //탄창을 갈아끼운 상태에서 노리쇠 후퇴하기(완전 재장전)
    public IEnumerator ReloadContinueFull2()
    {
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (GunType == 1)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 1.6f);

            animator.SetFloat("ReloadType", 5);
            yield return new WaitForSeconds(1.6f);
            animator.SetBool("DT-37 Reload", false);

            animator.SetFloat("ReloadType", 0);
        }
        else if (GunType == 2000)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 1.58f);
            animator.SetFloat("DP-9007 Reload", 5);

            yield return new WaitForSeconds(1.58f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFire(); //Debug SW-06 재장전

        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadType = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete(); //Debug SW-06 재장전 완료
    }

    //탄창을 뺀 상태에서 재장전 이어서하기(보급후 재장전)
    public IEnumerator ReloadContinueAddAmmo1()
    {
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        NeedAddAmmo = false;
        AfterReload(); //장전 상태 전달
        animator.SetFloat("NoLED", 0);

        if (GunType == 1)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 2.7f);

            animator.SetFloat("ReloadType", 4);
            yield return new WaitForSeconds(0.25f);
            SoundManager.instance.SFXPlay14("DT-37 Fire Sound", DT_37Reload2);
            yield return new WaitForSeconds(1.05f);
            ReloadFullAfterSupport(); //보급후 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.4f);
            animator.SetBool("DT-37 Reload", false);

            animator.SetFloat("ReloadType", 0);
        }
        else if (GunType == 2000)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 3.16f);
            animator.SetFloat("DP-9007 Reload", 3);

            yield return new WaitForSeconds(1.58f);
            ReloadFullAfterSupport(); //보급후 재장전 탄약 계산
            ReloadState = 2;
            yield return new WaitForSeconds(1.58f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        AfterAddAmmo = false;
        NotReloaded = false;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadType = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete(); //Debug SW-06 재장전 완료
    }

    //탄창을 갈아끼운 상태에서 노리쇠 후퇴하기(보급후 재장전)
    public IEnumerator ReloadContinueAddAmmo2()
    {
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        NeedAddAmmo = false;
        AfterReload(); //장전 상태 전달

        if (GunType == 1)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 1.6f);

            animator.SetFloat("ReloadType", 5);
            yield return new WaitForSeconds(1.6f);
            animator.SetBool("DT-37 Reload", false);

            animator.SetFloat("ReloadType", 0);
        }
        else if (GunType == 2000)
        {
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
            AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / 1.58f);
            animator.SetFloat("DP-9007 Reload", 5);

            yield return new WaitForSeconds(1.58f);
            animator.SetFloat("DP-9007 Reload", 0);
        }

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        AfterAddAmmo = false;
        NotReloaded = false;
        ammoInMagagine = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadType = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete(); //Debug SW-06 재장전 완료
    }

    //DS-65(샷건) 사격
    IEnumerator DS65Fire()
    {
        animator.SetFloat("Gun fire", 1000);

        ShotgunAfterFire = true;
        GunSmokeOn = 1;

        if (GunSmokeOn == 1)
        {
            GunSmokeOn = 0;
            GameObject ShotgunSmoke = Instantiate(ShotGunSmokePrefab, ShotGunSmokePos.position, ShotGunSmokePos.rotation); //총쏜 후 연기(오브젝트 풀링 안해도 됨)
            Destroy(ShotgunSmoke, 3f);
        }

        yield return new WaitForSeconds(0.4f);
        SW06_EjectShell(); //탄피 방출
        yield return new WaitForSeconds(0.3f);
        ShotgunFireTime = 0;
        ShotgunAfterFire = false;
    }

    //샷건 무기류 사격 후 및 중간 재장전
    IEnumerator ShotgunReload()
    {
        if (StopReload == false)
        {
            if (GunType == 1000) //DS-65
            {
                reloading = true;
                //AfterReload(); //장전 상태 전달
                if (ammoInMagagine > ViewAmmo)
                    ammoInMagagine = ViewAmmo;

                animator.SetBool("DS-65 Reload(ready)", true);
                yield return new WaitForSeconds(0.583f);
                animator.SetBool("DS-65 Reload(ready)", false);
                ammoInMagagine--;
                ViewAmmo--;
                ViewSW06_Magazine++;
                //Debug.Log(ammoInMagagine);

                if (ammoInMagagine == 0 || isGun == true)
                {
                    animator.SetBool("DS-65 Reload(input ammo)", false);
                    if (ShotgunReloadCompleteType == true)
                    {
                        animator.SetBool("DS-65 Reload(complete)", true);
                        yield return new WaitForSeconds(0.916f);
                        animator.SetBool("DS-65 Reload(complete)", false);
                    }
                    else
                    {
                        animator.SetBool("DS-65 Reload(complete2)", true);
                        yield return new WaitForSeconds(0.25f);
                        animator.SetBool("DS-65 Reload(complete2)", false);
                    }

                    ZeroMagagineUITime = 0;
                    ReloadOneTime = 0;
                    reloading = false;
                    //AfterReloadOff(); //장전 상태 해제 전달
                }
                else if (ammoInMagagine > 0)
                {
                    while (true)
                    {
                        animator.SetBool("DS-65 Reload(input ammo)", true);
                        yield return new WaitForSeconds(0.583f);
                        ammoInMagagine--;
                        ViewAmmo--;
                        ViewSW06_Magazine++;

                        if (ammoInMagagine == 0)
                        {
                            animator.SetBool("DS-65 Reload(input ammo)", false);
                            if (ShotgunReloadCompleteType == true)
                            {
                                animator.SetBool("DS-65 Reload(complete)", true);
                                yield return new WaitForSeconds(0.916f);
                                animator.SetBool("DS-65 Reload(complete)", false);
                            }
                            else
                            {
                                animator.SetBool("DS-65 Reload(complete2)", true);
                                yield return new WaitForSeconds(0.25f);
                                animator.SetBool("DS-65 Reload(complete2)", false);
                            }

                            ZeroMagagineUITime = 0;
                            ReloadOneTime = 0;
                            reloading = false;
                            //AfterReloadOff(); //장전 상태 해제 전달
                            break;
                        }
                        else if (isGun == true)
                        {
                            animator.SetBool("DS-65 Reload(input ammo)", false);
                            if (ShotgunReloadCompleteType == true)
                            {
                                animator.SetBool("DS-65 Reload(complete)", true);
                                yield return new WaitForSeconds(0.916f);
                                animator.SetBool("DS-65 Reload(complete)", false);
                            }
                            else
                            {
                                animator.SetBool("DS-65 Reload(complete2)", true);
                                yield return new WaitForSeconds(0.25f);
                                animator.SetBool("DS-65 Reload(complete2)", false);
                            }

                            ZeroMagagineUITime = 0;
                            ReloadOneTime = 0;
                            reloading = false;
                            //AfterReloadOff(); //장전 상태 해제 전달
                            break;
                        }
                        else
                            continue;
                    }
                }
            }
        }
    }

    //샷건 무기류 보급 후 재장전
    IEnumerator ShotgunReloadAfterGetAmmo()
    {
        if (StopReload == false)
        {
            if (GunType == 1000) //DS-65
            {
                reloading = true;
                //AfterReload(); //장전 상태 전달

                animator.SetBool("DS-65 Reload(ready)", true);
                yield return new WaitForSeconds(0.583f);
                animator.SetBool("DS-65 Reload(ready)", false);
                ammoInMagagine--;
                ViewAmmo--;
                ViewSW06_Magazine++;

                if (ammoInMagagine == 0 || isGun == true)
                {
                    animator.SetBool("DS-65 Reload(input ammo)", false);
                    animator.SetBool("DS-65 Reload(complete)", true);
                    yield return new WaitForSeconds(0.916f);
                    animator.SetBool("DS-65 Reload(complete)", false);
                    ZeroMagagineUITime = 0;
                    ReloadOneTime = 0;
                    reloading = false;
                    AfterAddAmmo = false;
                    //AfterReloadOff(); //장전 상태 해제 전달
                }
                else if (ammoInMagagine > 0)
                {
                    while (true)
                    {
                        animator.SetBool("DS-65 Reload(input ammo)", true);
                        yield return new WaitForSeconds(0.583f);
                        ammoInMagagine--;
                        ViewAmmo--;
                        ViewSW06_Magazine++;

                        if (ammoInMagagine == 0)
                        {
                            animator.SetBool("DS-65 Reload(input ammo)", false);
                            animator.SetBool("DS-65 Reload(complete)", true);
                            yield return new WaitForSeconds(0.916f);
                            animator.SetBool("DS-65 Reload(complete)", false);
                            ZeroMagagineUITime = 0;
                            ReloadOneTime = 0;
                            reloading = false;
                            AfterAddAmmo = false;
                            //AfterReloadOff(); //장전 상태 해제 전달
                            break;
                        }
                        else if (isGun == true)
                        {
                            animator.SetBool("DS-65 Reload(input ammo)", false);
                            animator.SetBool("DS-65 Reload(complete)", true);
                            yield return new WaitForSeconds(0.916f);
                            animator.SetBool("DS-65 Reload(complete)", false);
                            ZeroMagagineUITime = 0;
                            ReloadOneTime = 0;
                            reloading = false;
                            AfterAddAmmo = false;
                            //AfterReloadOff(); //장전 상태 해제 전달
                            break;
                        }
                        else
                            continue;
                    }
                }
            }
        }
    }

    //기관단총 사격후, 재장전
    IEnumerator subGunReload()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 1;
        DelayShot = 0;
        FireStopTime = 1;
        ReloadType = 1;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        subGunMuzzleFlash1.SetActive(false);
        subGunMuzzleFlash2.SetActive(false);
        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontOutputTime + subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.25f;
        else
            subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.22f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 1);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                animator.SetFloat("Magazine off1", 1);
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 제거");
                //1.246f 초
            }
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 3);
                yield return new WaitForSeconds(0.5f);
                animator.SetFloat("Magazine off2", 1);
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //0.83f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = 0;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 7);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 제거");
                //1.416f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
    }

    //기관단총 탄창 무기류 전술 재장전
    IEnumerator subGunReload2()
    {
        subGunReloadType1 = 2;
        subGunReloadType2 = 2;
        FireStopTime = 1;
        ReloadType = 2;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontOutputTime + subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.25f;
        else
            subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.22f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 2);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                ViewSubMachineGunMagazineFront = 1;
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 제거");
                //1.246f 초
            }
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 4);
                yield return new WaitForSeconds(0.5f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineBack - 1);
                ViewAmmo += (ViewSubMachineGunMagazineBack - 1);
                ammoInMagagine = 0;
                ViewSubMachineGunMagazineBack = 1;
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //0.83f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine - 1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                ViewSW06_Magazine++;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    ammoInMagagine = -2;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 8);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                ViewSubMachineGunMagazineFront = 1;
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.5f);
                //Debug.Log("앞 탄창 제거");
                //1.416f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                ammoInMagagine -= SubMachineGunFrontAmmoPerMagazine - 1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                ViewSW06_Magazine++;
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }
        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
    }

    //기관단총 탄창 무기류 보급 후, 재장전
    IEnumerator subGunReload3()
    {
        ammoInMagagine = 0;
        subGunReloadType1 = 3;
        subGunReloadType2 = 3;
        FireStopTime = 1;
        ReloadType = 3;
        AfterReload(); //장전 상태 전달
        reloading = true;
        NeedAddAmmo = false;
        ReloadCompleteUI = true;

        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);

        subGunReloadTime = subGunFrontOutputTime + subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.25f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (SubGunTypeFront == 1) //탄창 제거
        {
            animator.SetFloat("subGunReload", 1);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("subGun active2", 0);
            animator.SetFloat("subGun Reload back", 1);
            yield return new WaitForSeconds(0.666f);
            subGunMagazineType1 = 0;
            ReloadState = 1;
            yield return new WaitForSeconds(0.33f);
            //Debug.Log("앞 탄창 제거");
            //1.246f 초
        }
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("subGunReload", 3);
            yield return new WaitForSeconds(0.5f);
            subGunMagazineType2 = 0;
            ReloadState2 = 1;
            yield return new WaitForSeconds(0.33f);
            //Debug.Log("뒷 탄창 제거");
            //0.83f 초
        }
        if (SubGunTypeFront == 1) //탄창 주입
        {
            animator.SetFloat("subGun Magazine active", 1);
            animator.SetFloat("subGunReload", 5);
            yield return new WaitForSeconds(0.916f);
            StartCoroutine(BoltAction1());
            subGunMagazineType1 = 1;
            subGunReloadType1 = 0;
            ReloadState = 2;
            ReloadSupportsubFront(); //기관단총 앞부분 보급후 탄약 계산
            yield return new WaitForSeconds(0.33f);
            //Debug.Log("앞 탄창 주입");
            //1.246f 초
        }
        if (AmmoAmount != 0)
        {
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 6);
                yield return new WaitForSeconds(0.1f);
                animator.SetFloat("subGun Magazine active", 0);
                yield return new WaitForSeconds(0.316f);
                StartCoroutine(BoltAction2());
                subGunMagazineType2 = 1;
                subGunReloadType2 = 0;
                ReloadState2 = 2;
                ReloadSupportsubBack(); //기관단총 뒷부분 보급후 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 주입");
                //0.746f 초
            }
        }
        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFire2(); //Debug SW-06 보급후 재장전

        ReloadType = 0;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        reloading = false;
        AfterAddAmmo = false;
        ReloadCompleteUI = false;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        ammoInMagagine = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        this.gameObject.AddComponent<GunAttackText>().ReloadAfterFireComplete2(); //Debug SW-06 보급후 재장전 완료
    }

    //기관단총 앞 탄창까지 뺀 상태에서 재장전 이어서하기(완전 재장전)
    IEnumerator subGunReloadContinueFull1()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 1;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.553f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.553f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeBack == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 9);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.583f);
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //1.163f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                animator.SetFloat("Magazine off1", 0);
                StartCoroutine(BoltAction1());
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    ammoInMagagine = 0;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("완전 탄창 앞부터 재장전 완료(캔슬)");
    }

    //기관단총 뒷 탄창까지 뺀 상태에서 재장전 이어서하기(완전 재장전)
    IEnumerator subGunReloadContinueFull2()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 1;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);
        animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontInputTime + subGunBackInputTime + 0.5f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.47f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            animator.SetFloat("subGunReload", 13);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("subGun active2", 0);
            animator.SetFloat("subGun Reload back", 1);

            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = 0;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("완전 탄창 뒤부터 재장전 완료(캔슬)");
    }

    //기관단총 앞 탄창까지 넣고 뒷 탄창이 비워진 상태에서 사격 후, 재장전하기(완전 재장전)
    IEnumerator subGunReloadContinueFull3()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 1;
        FireStopTime = 1;
        ReloadType = 1;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        subGunMuzzleFlash1.SetActive(false);
        subGunMuzzleFlash2.SetActive(false);
        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);
        animator.SetFloat("Magazine off2", 1);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.414f;
        else
            subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.384f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 7);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.5f);
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.66f);
                //Debug.Log("앞 탄창 제거");
                //1.41f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = 0;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 7);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 제거");
                //1.416f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("완전 재장전 탄창 앞까지 넣고 이어서 완료(캔슬)");
    }

    //기관단총 앞 탄창까지 뺀 상태에서 재장전 이어서하기(전술 재장전)
    IEnumerator subGunReloadContinueTactical1()
    {
        subGunReloadType1 = 2;
        subGunReloadType2 = 2;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.553f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.553f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 10);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.583f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineBack - 1);
                ViewAmmo += (ViewSubMachineGunMagazineBack - 1);
                ViewSubMachineGunMagazineBack = 1;
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //1.163f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = -1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    ammoInMagagine -= 1;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                ammoInMagagine = -1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }
        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        animator.SetFloat("ReloadType", 0);
        reloading = false;
        NotReloaded = false;
        ReloadCompleteUI = false;
        ReloadTacticalComplete = false;
        StopFireUntillReloadingTactical = false;
        Debug.Log("전술 탄창 앞부터 재장전 완료(캔슬)");
    }

    //기관단총 뒷 탄창까지 뺀 상태에서 재장전 이어서하기(전술 재장전)
    IEnumerator subGunReloadContinueTactical2()
    {
        subGunReloadType1 = 2;
        subGunReloadType2 = 2;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);
        animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontInputTime + subGunBackInputTime + 0.5f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.47f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            animator.SetFloat("subGunReload", 13);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("subGun active2", 0);
            animator.SetFloat("subGun Reload back", 1);

            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = -1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine -= 1;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                ammoInMagagine = -1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달

        animator.SetFloat("ReloadType", 0);
        reloading = false;
        NotReloaded = false;
        ReloadCompleteUI = false;
        ReloadTacticalComplete = false;
        StopFireUntillReloadingTactical = false;
        Debug.Log("전술 탄창 뒤부터 재장전 완료(캔슬)");
    }

    //기관단총 앞 탄창까지 뺀 상태에서 재장전 이어서하기(보급 후, 재장전)
    IEnumerator subGunReloadContinueAddAmmo1()
    {
        subGunReloadType1 = 3;
        subGunReloadType2 = 3;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        NeedAddAmmo = false;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.553f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.553f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeBack == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 9);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.583f);
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //1.163f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadSupportsubFront(); //기관단총 앞부분 보급후 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadSupportsubBack(); //기관단총 뒷부분 보급후 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        AfterAddAmmo = false;
        NotReloaded = false;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("보급 후, 탄창 앞부터 재장전 완료(캔슬)");
    }

    //기관단총 뒷 탄창까지 뺀 상태에서 재장전 이어서하기(보급 후, 재장전)
    IEnumerator subGunReloadContinueAddAmmo2()
    {
        subGunReloadType1 = 3;
        subGunReloadType2 = 3;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        NeedAddAmmo = false;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);
        animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontInputTime + subGunBackInputTime + 0.5f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.47f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            animator.SetFloat("subGunReload", 13);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("subGun active2", 0);
            animator.SetFloat("subGun Reload back", 1);

            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadSupportsubFront(); //기관단총 앞부분 보급후 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadSupportsubBack(); //기관단총 뒷부분 보급후 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        AfterAddAmmo = false;
        NotReloaded = false;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("보급 후, 탄창 뒤부터 재장전 완료(캔슬)");
    }

    //기관단총 앞 : 전술 재장전, 뒤 : 완전 재장전(절반 재장전)
    IEnumerator subGunReloadHalf1()
    {
        subGunReloadType1 = 2;
        subGunReloadType2 = 1;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        if (subGunMagazineType1 == 0)
            animator.SetFloat("Magazine off1", 1);
        if (subGunMagazineType2 == 0)
            animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (subGunMagazineType2 == 1)
            {
                if (NotReloaded == false)
                    subGunReloadTime = subGunFrontOutputTime + subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.25f;
                else
                    subGunReloadTime = subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.583f;
            }
            else
                subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.42f;
        }
        else
            subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.28f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (subGunMagazineType2 == 1) //기관단총 뒷 총에 탄창이 있을 경우, 순차적으로 제거하는 애니메이션으로 이어진다.
            {
                if (NotReloaded == false) //캔슬된 상태일 경우
                {
                    if (SubGunTypeFront == 1) //탄창 제거
                    {
                        animator.SetFloat("subGunReload", 2);
                        yield return new WaitForSeconds(0.25f);
                        animator.SetFloat("subGun active2", 0);
                        animator.SetFloat("subGun Reload back", 1);
                        yield return new WaitForSeconds(0.666f);
                        ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                        ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                        ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                        ViewSubMachineGunMagazineFront = 1;
                        subGunMagazineType1 = 0;
                        ReloadState = 1;
                        yield return new WaitForSeconds(0.33f);
                        //Debug.Log("앞 탄창 제거");
                        //1.246f 초
                    }
                    if (SubGunTypeBack == 1)
                    {
                        animator.SetFloat("subGunReload", 3);
                        yield return new WaitForSeconds(0.5f);
                        ammoInMagagine = - 1;
                        subGunMagazineType2 = 0;
                        ReloadState2 = 1;
                        yield return new WaitForSeconds(0.33f);
                        //Debug.Log("뒷 탄창 제거");
                        //0.83f 초
                    }
                }
                else
                {
                    if (SubGunTypeBack == 1) //탄창 제거
                    {
                        animator.SetFloat("subGunReload", 9);
                        yield return new WaitForSeconds(0.25f);
                        animator.SetFloat("subGun active2", 0);
                        animator.SetFloat("subGun Reload back", 1);
                        yield return new WaitForSeconds(0.583f);
                        ammoInMagagine = - 1;
                        subGunMagazineType2 = 0;
                        ReloadState2 = 1;
                        yield return new WaitForSeconds(0.33f);
                        //Debug.Log("뒷 탄창 제거");
                        //1.163f 초
                    }
                }
            }
            else //기관단총 뒷 총에 탄창이 없을 경우, 앞 총의 탄창만 빼는 연출로 이어진다.
            {
                if (SubGunTypeFront == 1)
                {
                    animator.SetFloat("subGunReload", 8);
                    yield return new WaitForSeconds(0.25f);
                    animator.SetFloat("subGun active2", 0);
                    animator.SetFloat("subGun Reload back", 1);
                    yield return new WaitForSeconds(0.666f);
                    ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                    ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                    ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                    ViewSubMachineGunMagazineFront = 1;
                    subGunMagazineType1 = 0;
                    ReloadState = 1;
                    yield return new WaitForSeconds(0.5f);
                    //Debug.Log("앞 탄창 제거");
                    //1.416f 초
                }
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine -= SubMachineGunFrontAmmoPerMagazine - 1;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = -1;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 8);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                ViewSubMachineGunMagazineFront = 1;
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.5f);
                //Debug.Log("앞 탄창 제거");
                //1.416f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine -= SubMachineGunFrontAmmoPerMagazine - 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ammoInMagagine = - 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("절반 재장전1 완료(캔슬)");
    }

    //기관단총 앞 : 완전 재장전, 뒤 : 전술 재장전(절반 재장전)
    IEnumerator subGunReloadHalf2()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 2;
        FireStopTime = 1;
        ReloadCompleteUI = true;
        reloading = true;
        AfterReload(); //장전 상태 전달

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        if (subGunMagazineType1 == 0)
            animator.SetFloat("Magazine off1", 1);
        if (subGunMagazineType2 == 0)
            animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (subGunMagazineType1 == 1)
                subGunReloadTime = subGunFrontOutputTime + subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.25f;
            else
                subGunReloadTime = subGunBackOutputTime + subGunFrontInputTime + subGunBackInputTime + 0.673f;
        }
        else
        {
            if (subGunMagazineType1 == 1)
                subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.25f;
            else
                subGunReloadTime = subGunFrontInputTime + 0.25f;
        }
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (subGunMagazineType1 == 1) //기관단총 앞 총에 탄창이 있을 경우, 순차적으로 제거하는 애니메이션으로 이어진다.
            {
                if (SubGunTypeFront == 1) //탄창 제거
                {
                    animator.SetFloat("subGunReload", 1);
                    yield return new WaitForSeconds(0.25f);
                    animator.SetFloat("subGun active2", 0);
                    animator.SetFloat("subGun Reload back", 1);
                    yield return new WaitForSeconds(0.666f);
                    subGunMagazineType1 = 0;
                    ReloadState = 1;
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("앞 탄창 제거");
                    //1.246f 초
                }
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 4);
                    yield return new WaitForSeconds(0.5f);
                    ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineBack - 1);
                    ViewAmmo += (ViewSubMachineGunMagazineBack - 1);
                    ViewSubMachineGunMagazineBack = 1;
                    subGunMagazineType2 = 0;
                    ReloadState2 = 1;
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 제거");
                    //0.83f 초
                }
            }
            else //기관단총 앞 총에 탄창이 없을 경우, 뒷 총의 탄창만 빼는 연출로 이어진다.
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 10);
                    yield return new WaitForSeconds(0.25f);
                    animator.SetFloat("subGun active2", 0);
                    animator.SetFloat("subGun Reload back", 1);
                    yield return new WaitForSeconds(0.583f);
                    ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineBack - 1);
                    ViewAmmo += (ViewSubMachineGunMagazineBack - 1);
                    ViewSubMachineGunMagazineBack = 1;
                    subGunMagazineType2 = 0;
                    ReloadState2 = 1;
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 제거");
                    //1.163f 초
                }
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = -1;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (subGunMagazineType1 == 1) //기관단총 앞 총에 탄창이 있을 경우, 순차적으로 제거하는 애니메이션으로 이어진다.
            {
                if (SubGunTypeFront == 1) //탄창 제거
                {
                    animator.SetFloat("subGunReload", 1);
                    yield return new WaitForSeconds(0.25f);
                    animator.SetFloat("subGun active2", 0);
                    animator.SetFloat("subGun Reload back", 1);
                    yield return new WaitForSeconds(0.666f);
                    subGunMagazineType1 = 0;
                    ReloadState = 1;
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("앞 탄창 제거");
                    //1.246f 초
                }
                if (SubGunTypeFront == 1) //탄창 주입
                {
                    if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                        animator.SetFloat("subGun Magazine active", 1);
                    animator.SetFloat("subGunReload", 5);
                    yield return new WaitForSeconds(0.916f);
                    StartCoroutine(BoltAction1());
                    animator.SetFloat("Magazine off1", 0);
                    ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                    subGunMagazineType1 = 1;
                    subGunReloadType1 = 0;
                    ReloadState = 2;
                    ReloadsubFront(); //기관단총 앞부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("앞 탄창 주입");
                    //1.246f 초
                }
            }
            else
            {
                if (SubGunTypeFront == 1) //탄창 주입
                {
                    if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                        animator.SetFloat("subGun Magazine active", 1);
                    animator.SetFloat("subGunReload", 5);
                    yield return new WaitForSeconds(0.916f);
                    StartCoroutine(BoltAction1());
                    animator.SetFloat("Magazine off1", 0);
                    ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                    subGunMagazineType1 = 1;
                    subGunReloadType1 = 0;
                    ReloadState = 2;
                    ReloadsubFront(); //기관단총 앞부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("앞 탄창 주입");
                    //1.246f 초
                }
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        NotReloaded = false;
        ammoInMagagine = -1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        ReloadType = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("절반 재장전2 완료(캔슬)");
    }

    //기관단총 탄창 무기류 앞부분 전술 재장전
    IEnumerator subGunHalfReload1()
    {
        subGunReloadType1 = 2;
        FireStopTime = 1;
        ReloadType = 2;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);

        subGunReloadTime = subGunFrontOutputTime + subGunFrontInputTime + 0.42f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            if (SubGunTypeFront == 1) //탄창 제거
            {
                animator.SetFloat("subGunReload", 8);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.666f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineFront - 1);
                ViewAmmo += (ViewSubMachineGunMagazineFront - 1);
                ammoInMagagine += ViewSubMachineGunMagazineFront - 1;
                ViewSubMachineGunMagazineFront = 1;
                subGunMagazineType1 = 0;
                ReloadState = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 제거");
                //1.416f 초
            }
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                ammoInMagagine = -2;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadTacticalsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("앞 탄창 전술 장전 완료(캔슬)");
    }

    //기관단총 탄창 무기류 뒷부분 전술 재장전
    IEnumerator subGunHalfReload2()
    {
        subGunReloadType2 = 2;
        FireStopTime = 1;
        ReloadType = 2;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);

        if (subGunMagazineType2 == 1)
            subGunReloadTime = subGunBackOutputTime + subGunBackInputTime + 0.833f;
        else if (subGunMagazineType2 == 1)
            subGunReloadTime = subGunBackInputTime + 0.833f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (subGunMagazineType2 == 1) //뒷 탄창이 있을 경우
        {
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 10);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                yield return new WaitForSeconds(0.5f);
                ViewSW06_Magazine = ViewSW06_Magazine - (ViewSubMachineGunMagazineBack - 1);
                ViewAmmo += (ViewSubMachineGunMagazineBack - 1);
                ammoInMagagine += ViewSubMachineGunMagazineBack - 1;
                ViewSubMachineGunMagazineBack = 1;
                subGunMagazineType2 = 0;
                ReloadState2 = 1;
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 제거");
                //1.163f 초
            }
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 12);
                yield return new WaitForSeconds(0.1f);
                animator.SetFloat("subGun Magazine active", 0);
                yield return new WaitForSeconds(0.566f);
                animator.SetFloat("Magazine off2", 0);
                ammoInMagagine = -2;
                subGunMagazineType2 = 1;
                subGunReloadType2 = 0;
                ReloadState2 = 2;
                ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 주입");
                //0.996f 초
            }
        }
        else if (subGunMagazineType2 == 0)
        {
            if (SubGunTypeBack == 1)
            {
                animator.SetFloat("subGunReload", 10);
                yield return new WaitForSeconds(0.25f);
                animator.SetFloat("subGun active2", 0);
                animator.SetFloat("subGun Reload back", 1);
                animator.SetFloat("subGunReload", 12);
                yield return new WaitForSeconds(0.1f);
                animator.SetFloat("subGun Magazine active", 0);
                yield return new WaitForSeconds(0.566f);
                animator.SetFloat("Magazine off2", 0);
                ammoInMagagine = -2;
                subGunMagazineType2 = 1;
                subGunReloadType2 = 0;
                ReloadState2 = 2;
                ReloadTacticalsubBcak(); //기관단총 뒷부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("뒷 탄창 주입");
                //0.996f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        StopFireUntillReloadingTactical = false;
        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("뒷 탄창 전술 장전 완료(캔슬)");
    }

    //기관단총 탄창이 모두 없는 상태에서 완전 재장전
    IEnumerator subGunReloadComplete()
    {
        subGunReloadType1 = 1;
        subGunReloadType2 = 1;
        FireStopTime = 1;
        ReloadType = 1;
        reloading = true;
        ReloadCompleteUI = true;
        AfterReload(); //장전 상태 전달

        animator.SetFloat("subGunFront fire", 0);
        animator.SetFloat("subGunBack fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 3000);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 1);
        animator.SetFloat("Magazine off1", 1);
        animator.SetFloat("Magazine off2", 1);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
            subGunReloadTime = subGunFrontInputTime + subGunBackInputTime + 0.5f;
        else
            subGunReloadTime = subGunFrontInputTime + 0.5f;
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 1 / subGunReloadTime);

        if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
        {
            animator.SetFloat("subGunReload", 13);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("subGun active2", 0);
            animator.SetFloat("subGun Reload back", 1);

            if (SubGunTypeFront == 1) //탄창 주입
            {
                if (AmmoAmount > SubMachineGunFrontAmmoPerMagazine)
                    animator.SetFloat("subGun Magazine active", 1);
                animator.SetFloat("subGunReload", 5);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.33f);
                //Debug.Log("앞 탄창 주입");
                //1.246f 초
            }
            if (AmmoAmount != 0)
            {
                if (SubGunTypeBack == 1)
                {
                    animator.SetFloat("subGunReload", 6);
                    yield return new WaitForSeconds(0.1f);
                    animator.SetFloat("subGun Magazine active", 0);
                    yield return new WaitForSeconds(0.316f);
                    StartCoroutine(BoltAction2());
                    animator.SetFloat("Magazine off2", 0);
                    ammoInMagagine = 0;
                    subGunMagazineType2 = 1;
                    subGunReloadType2 = 0;
                    ReloadState2 = 2;
                    ReloadsubBack(); //기관단총 뒷부분 탄약 계산
                    yield return new WaitForSeconds(0.33f);
                    //Debug.Log("뒷 탄창 주입");
                    //0.746f 초
                }
            }
        }
        else
        {
            if (SubGunTypeFront == 1) //탄창 주입
            {
                animator.SetFloat("subGunReload", 11);
                yield return new WaitForSeconds(0.916f);
                StartCoroutine(BoltAction1());
                animator.SetFloat("Magazine off1", 0);
                ammoInMagagine = SubMachineGunBackAmmoPerMagazine;
                subGunMagazineType1 = 1;
                subGunReloadType1 = 0;
                ReloadState = 2;
                ReloadsubFront(); //기관단총 앞부분 탄약 계산
                yield return new WaitForSeconds(0.13f);
                //Debug.Log("앞 탄창 주입");
                //1.046f 초
            }
        }

        animator.SetFloat("subGunReload", 1000); //마무리
        yield return new WaitForSeconds(0.083f);
        if (SubGunTypeBack == 1)
            animator.SetFloat("subGun active2", 1);
        animator.SetFloat("subGun Reload back", 0);
        yield return new WaitForSeconds(0.167f);
        animator.SetFloat("subGunReload", 0);

        if (SubGunTypeFront == 1)
            animator.SetFloat("NoLED", 0);
        if (SubGunTypeBack == 1)
            animator.SetFloat("NoLED2", 0);
        //0.25f 초

        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", true);
        Invoke("AfterEndCycle", 0.5f); //장전 UI 싸이클 완료후
        Invoke("ViewCountComplete", 0.5f); //장전 UI 싸이클 완료처리
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
        AnimationUIReload.GetComponent<Animator>().SetFloat("Cool time, Reload", 0);

        ReloadCompleteUI = false;
        reloading = false;
        ReloadType = 0;
        FireStopTime = 1;
        ReloadOneTime = 0;
        ReloadState = 0;
        ReloadState2 = 0;
        subGunCanFire1 = 0;
        subGunCanFire2 = 0;
        AfterReloadOff(); //장전 상태 해제 전달
        Debug.Log("No 탄창 장전 완료(캔슬)");
    }

    //장전 애니메이션1
    IEnumerator BoltAction1()
    {
        if (SubGunTypeFront == 1)
        {
            animator.SetFloat("subGun Bolt action1", 1);
            yield return new WaitForSeconds(0.16f);
            animator.SetFloat("subGun Bolt action1", 0);
        }
    }

    //장전 애니메이션2
    IEnumerator BoltAction2()
    {
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("subGun Bolt action2", 1);
            yield return new WaitForSeconds(0.16f);
            animator.SetFloat("subGun Bolt action2", 0);
        }
    }

    //탄약 고갈직후 사격 액션 멈춤
    private IEnumerator FireActionOff()
    {
        if (GunType != 1000)
        {
            yield return new WaitForSeconds(0.01f);
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);
        }

        if (GunType >= 1000 && GunType < 2000)
        {
            yield return new WaitForSeconds(0.01f);
            animator.SetFloat("Gun fire", 0);
        }
    }

    //탄피 배출
    public void SW06_EjectShell()
    {
        if (GunType == 1)
        {
            DT37Shell = objectManager.Loader("DT37Shell");
            DT37Shell.transform.position = DT37ShellPos.transform.position;
            DT37Shell.transform.rotation = DT37ShellPos.transform.rotation;
            ShellCase_SW06 ShellCase_SW06 = DT37Shell.GetComponent<ShellCase_SW06>();
            ShellCase_SW06.Pos = DT37Shell.transform.position.y;

            float xVnot = Random.Range(5f, 10f);
            float yVnot = Random.Range(5f, 10f);

            DT37Shell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
            DT37Shell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
        }
        else if(GunType == 1000)
        {
            DS65Shell = objectManager.Loader("DS65Shell");
            DS65Shell.transform.position = DS65ShellPos.transform.position;
            DS65Shell.transform.rotation = DS65ShellPos.transform.rotation;
            ShellCase_SW06 ShellCase_SW06 = DS65Shell.GetComponent<ShellCase_SW06>();
            ShellCase_SW06.Pos = DS65Shell.transform.position.y;

            float xVnot = Random.Range(5f, 10f);
            float yVnot = Random.Range(5f, 10f);

            DS65Shell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
            DS65Shell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
        }
        else if (GunType == 2000)
        {
            DP9007Shell = objectManager.Loader("DP9007Shell");
            DP9007Shell.transform.position = DP9007ShellPos.transform.position;
            DP9007Shell.transform.rotation = DP9007ShellPos.transform.rotation;
            ShellCase_SW06 ShellCase_SW06 = DP9007Shell.GetComponent<ShellCase_SW06>();
            ShellCase_SW06.Pos = DP9007Shell.transform.position.y;

            float xVnot = Random.Range(5f, 10f);
            float yVnot = Random.Range(5f, 10f);

            DP9007Shell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
            DP9007Shell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
        }
    }

    void EjectShellFront()
    {
        if (SubGunTypeFront == 1)
        {
            CGD27Shell = objectManager.Loader("CGD27Shell");
            CGD27Shell.transform.position = CGD27ShellPosFront.transform.position;
            CGD27Shell.transform.rotation = transform.rotation;
            ShellCase_SW06 ShellCase_SW06 = CGD27Shell.GetComponent<ShellCase_SW06>();
            ShellCase_SW06.Pos = CGD27Shell.transform.position.y;

            float xVnot = Random.Range(5f, 10f);
            float yVnot = Random.Range(5f, 10f);

            CGD27Shell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
            CGD27Shell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
        }
    }

    void EjectShellBack()
    {
        if (SubGunTypeBack == 1)
        {
            CGD27Shell = objectManager.Loader("CGD27Shell");
            CGD27Shell.transform.position = CGD27ShellPosBack.transform.position;
            CGD27Shell.transform.rotation = transform.rotation;
            ShellCase_SW06 ShellCase_SW06 = CGD27Shell.GetComponent<ShellCase_SW06>();
            ShellCase_SW06.Pos = CGD27Shell.transform.position.y;
            ShellCase_SW06.Layer();

            float xVnot = Random.Range(5f, 10f);
            float yVnot = Random.Range(5f, 10f);

            CGD27Shell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
            CGD27Shell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
        }
    }

    void ShotGunFire()
    {
        for (int index = 0; index < 7; index++)
        {
            GameObject bullet = objectManager.Loader("PlayerShotGunAmmo");
            bullet.transform.position = DT37Firepos.transform.position;
            bullet.transform.rotation = DT37Firepos.transform.rotation;

            bullet.GetComponent<ShotgunAmmoMovement>().SetDamage(Damage); //총알에다 데미지 전달
            ShotgunAmmoMovement ShotgunammoMovement = bullet.GetComponent<ShotgunAmmoMovement>();// AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 
            ShotgunammoMovement.isHit = false; // 피격방지 
            ShotgunammoMovement.objectManager = objectManager;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            float radians;
            radians = angle * 360 * Mathf.PI / 2;
            float x = Mathf.Cos(radians);

            if (transform.rotation.y == 0)
            {
                Vector2 dirVec = new Vector2(x, vectorY);
                rigid.AddForce(dirVec.normalized * 15, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 dirVec = new Vector2(-x, vectorY);
                rigid.AddForce(dirVec.normalized * 15, ForceMode2D.Impulse);
            }

            vectorY += 0.4F;

            if (vectorY >= 1.0F)
                vectorY = -1;
        }
    }

    //장전 UI 싸이클 완료후
    void AfterEndCycle()
    {
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
    }

    //장전 UI 싸이클 완료처리
    void ViewCountComplete()
    {
        AnimationUIReload.GetComponent<Animator>().SetBool("Cool time cycle count, Reload", false);
    }
}