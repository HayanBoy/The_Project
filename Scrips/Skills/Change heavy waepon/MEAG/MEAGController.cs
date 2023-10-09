using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MEAGController : MonoBehaviour
{
    Animator animator;
    Movement movement;
    GunController gunController; //GunController의 CharsingEnergy 값 연동을 위한 선언
    GameControlSystem gameControlSystem;
    VM5GrenadeController vM5GrenadeController;
    public Image CHWActive;

    private Shake shake;
    private float FireShake;

    public GameObject GrenadeUI;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public GameObject SwapUI;
    public Image AnimationUICHWRight;

    public int EnergyNeeds;
    public int FireEnergy;
    public int GunType; //기본 총 타입을 전달받기 위한 변수
    public int SubGunTypeFront = 0; //앞 기관단총 장착여부
    public int SubGunTypeBack = 0; //뒤 기관단총 장착여부

    public GameObject RailGunBullet;
    public Transform RailGunBulletPos; //총알 생성 좌표

    public ObjectManager objectManager;
    public GameObject ChangeWeapon; //무기 공격시, 기타 버튼 사용 무력화
    public GameObject Dash;
    public GameObject Grenade;
    public GameObject ChangeSkillBtn;

    public int RailGunBulletDamage; //차지당 레일건 데미지
    public int FireDamage;
    private float SoundTime;
    private float SoundChargeTime;
    private float SoundChargeTimeTwo;
    private float ChargeDamageSecond; //초당 데미지가 상승되도록 조취
    private float EnergyNeedTime;
    private float CoolTimeRunning; //쿨타임 도는 시간
    public float CoolTime; //쿨타임
    private int Cnt; //쿨타임이 가능하도록 조취

    private bool Active = false;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    private bool ChargingStart = true;
    private bool SoundOnePlay = false;
    public bool UsingTask;
    private bool ChargeEnd = false;
    public int HeavyWeaponUsing;

    private float Charging;
    public float RailCharging;  //누를 때 차징이 되기 위한 float 값 

    public bool isRailChargingReady; //레일건이 나타난 뒤 차징되게끔 하기위한 Bool값 

    public bool WeaponOn;
    private bool SecondOnline = false; //제일 먼저 활성화하지 않고 다른 체인지 무기 상태에서 바꾸었을 경우의 스위치
    public float SwitchTypeStart; //체인지 중화기가 교체될 때 시작 고유번호
    public float SwitchTypeEnd; //체인지 중화기가 교체될 때 끝 고유번호

    public AudioClip MEAGOn;
    public AudioClip MEAGOff;
    public AudioClip MEAGCharging;
    public AudioClip MEAGFire;
    public AudioClip MEAGChargingEnd;
    public AudioClip EnergyLow1;
    public AudioClip EnergyLow2;

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

    //X축 반전 원격 제어
    public void XMoveStop(bool ChangeWeaponUsing)
    {
        if (ChangeWeaponUsing == true)
        {
            UsingChangeWeapon = true;
        }
        else
        {
            UsingChangeWeapon = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        gunController = FindObjectOfType<GunController>();
        movement = FindObjectOfType<Movement>();
        gameControlSystem = FindObjectOfType<GameControlSystem>();
        vM5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        Cnt = 1;

        FireDamage = UpgradeDataSystem.instance.MEAGDamage;
        RailGunBulletDamage = UpgradeDataSystem.instance.MEAGAddDamage;
    }

    void Update()
    {
        RailgunSkill();
        CHWCool();

        if (WeaponOn == true && SecondOnline == false)
        {
            Active = true;
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);

            if (Active == true)
            {
                if (Active == true)
                {
                    SoundOnePlay = true;

                    if (SoundOnePlay == true && SoundTime == 0)
                    {
                        SoundTime += Time.deltaTime;

                        GrenadeUI.GetComponent<Animator>().SetBool("Cool time start, Grenade", true);
                        GrenadeUI.GetComponent<Animator>().SetBool("Cool time running, Grenade", true);
                        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
                        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
                        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
                        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
                        SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
                        SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", true);

                        StartCoroutine(RailgunReady()); //레일건 활성화
                        SoundManager.instance.SFXPlay6("Sound", MEAGOn);
                    }
                }

                UsingTask = true;
                GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달
                GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달
                movement.UsingChangeWeapon = true;

                //체인지 스킬 제한 목록
                GetComponent<ArthesL775Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<UGG98Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달

                GetComponent<VM5GrenadeController>().XMoveStop(true); //스킬 사용 전달
                GetComponent<M3078Controller>().TurnOff();
                GetComponent<M3078Controller>().CantSwap = true;
            }
        }
    }

    //무기 쿨타임
    void CHWCool()
    {
        if (Cnt == 0)
        {
            ChargingStart = false;
            AnimationUICHWRight.fillAmount = CoolTimeRunning / CoolTime;
            CoolTimeRunning += Time.deltaTime;
        }

        if (AnimationUICHWRight.fillAmount == 1)
        {
            transform.Find("Change heavy weapons/MEAG/Fire").gameObject.SetActive(false);
            CoolTimeRunning = 0;
            Cnt = 1;
            ChargingStart = true;
            AnimationUICHWRight.fillAmount = 0;
        }
    }

    void RailgunSkill()
    {
        if (WeaponOn && ChargingStart == true && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 || WeaponOn && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 && Input.GetKey(KeyCode.G))
        {
            if(UsingChangeWeapon == false && Reload == false)
            {
                if (ChargingStart == true)
                    Charging += Time.deltaTime;

                if (isRailChargingReady)
                    RailCharging += Time.deltaTime;

                ChargeDamageSecond += Time.deltaTime;

                if (ChargeDamageSecond > 0.1f && ChargeEnd == false)
                {
                    ChargeDamageSecond = 0;
                    FireShake += Time.deltaTime * 100f;
                    FireEnergy += EnergyNeeds;
                    FireDamage += RailGunBulletDamage;
                }

                if (SoundChargeTime == 0)
                {
                    SoundChargeTime += Time.deltaTime;
                    StartCoroutine(ChargingRailgun());
                }
            }
        }
        else
        {
            StartCoroutine(RailgunFire()); //레일건 발사
            Charging = 0;
            ChargeEnd = false;
            isRailChargingReady = false;
        }

        if(WeaponOn && ChargingStart == true && movement.FireJoystick && gunController.ChangeSkillEnergy < 30)
        {
            if (EnergyNeedTime == 0)
            {
                //Debug.Log("에너지가 부족합니다.");
                SoundManager.instance.SFXPlay("Sound", EnergyLow1);
                SoundManager.instance.SFXPlay("Sound", EnergyLow2);
            }

            EnergyNeedTime += Time.deltaTime;

            if (EnergyNeedTime > 0.5f)
                EnergyNeedTime = 0;
        }
        else if (!movement.FireJoystick)
            EnergyNeedTime = 0;
    }

    //차징 애니메이션
    IEnumerator ChargingRailgun()
    {
        animator.SetBool("MEAG charging", true);
        SoundManager.instance.SFXPlay5("Sound", MEAGCharging);

        yield return new WaitForSeconds(1.5f);

        if (SoundChargeTimeTwo == 0)
        {
            SoundChargeTimeTwo += Time.deltaTime;
            SoundManager.instance.SFXPlay8("Sound", MEAGChargingEnd);
        }

        ChargeEnd = true;

        FireEnergy += EnergyNeeds * 2;
        FireDamage += RailGunBulletDamage * 2;
    }

    //활성화 애니메이션
    public IEnumerator RailgunReady()
    {
        ChargingStart = false;
        gameControlSystem.UsingChangeWeapon = true;
        gameControlSystem.ChangeWeaponOnline++;
        GetComponent<Movement>().FireJoystickType = 0;

        if (HeavyWeaponUsing == 0)
            animator.SetFloat("Change active number", 1);
        else if (HeavyWeaponUsing == 1)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("Change active number", 2);
        }
        else if (HeavyWeaponUsing == 2)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("Change active number", 3);
        }

        if (animator.GetBool("MEAG off") == true)
            animator.SetBool("MEAG off", false);
        animator.SetFloat("MEAG on", 1);
        yield return new WaitForSeconds(0.583f);
        if (HeavyWeaponUsing == 0)
        {
            if (GunType == 1)
                transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
            if (GunType == 1000)
                transform.Find("bone_1/DS-65").gameObject.SetActive(true);
            if (GunType == 2000)
                transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
            if (SubGunTypeFront == 1)
                transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            if (SubGunTypeBack == 1)
                transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
        }
        else if (HeavyWeaponUsing == 1)
        {
            animator.SetFloat("Gun active", 0);
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(true);
        }
        else if (HeavyWeaponUsing == 2)
        {
            animator.SetFloat("Gun active", 0);
            transform.Find("bone_1/ASC 365").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.417f);
        animator.SetFloat("Change active number", 0);
        animator.SetFloat("MEAG on", 0);
        transform.Find("Change heavy weapons/MEAG/Module7").gameObject.SetActive(true);
        GetComponent<Movement>().HeavyWeaponUsing(2); //중화기 사용 상태 전달
        GetComponent<Movement>().FireJoystickType = 1000;
        isRailChargingReady = true;
        ChargingStart = true;
        gameControlSystem.UsingChangeWeapon = false;
    }

    //레일건 발사
    public IEnumerator RailgunFire()
    {
        if (Charging > 0.01f && RailCharging >= 0)
        {
            Shake.Instance.ShakeCamera(FireShake + 7.5f, 0.25f);
            FireShake = 0;
            Cnt = 0;
            gunController.ChangeSkillEnergy -= FireEnergy + 30;
            if (gunController.ChangeSkillEnergy <= 0)
                gunController.ChangeSkillEnergy = 0;

            animator.SetBool("MEAG charging", false);
            animator.SetBool("MEAG fire", true);
            transform.Find("Change heavy weapons/MEAG/Fire").gameObject.SetActive(true);
            GameObject RailgunFire = Instantiate(RailGunBullet, RailGunBulletPos.transform.position, RailGunBulletPos.transform.rotation);
            RailgunFire.GetComponent<RailgunlBullet>().SetDamage(FireDamage); //총알에다 데미지 전달
            RailgunlBullet RailgunBullet = RailgunFire.GetComponent<RailgunlBullet>();// AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함
            RailgunBullet.isHit = false; // 피격방지
            RailgunBullet.objectManager = objectManager;
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
            SoundManager.instance.SFXPlay15("Sound", MEAGFire);
            yield return new WaitForSeconds(0.583f);
            animator.SetBool("MEAG fire", false);

            GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
            GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

            ChangeWeapon.gameObject.SetActive(true);
            Dash.gameObject.SetActive(true);
            Grenade.gameObject.SetActive(true);

            UsingChangeWeapon = false;
            SoundChargeTime = 0;
            SoundChargeTimeTwo = 0;
            RailCharging = 0;
            Charging = 0;
            ChargeDamageSecond = 0;
            FireEnergy = 0;
            FireDamage = 0;
        }
    }

    public void ChangeOn()
    {
        StartCoroutine(ChangeOnline());
    }

    IEnumerator ChangeOnline()
    {
        UsingTask = true;

        if (animator.GetBool("MEAG off") == true)
            animator.SetBool("MEAG off", false);
        if (SoundTime == 0)
        {
            SoundTime += Time.deltaTime;
            SoundManager.instance.SFXPlay6("Sound", MEAGOn);
        }
        animator.SetFloat("Change weapon off", SwitchTypeStart);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon off", 0);
        animator.SetFloat("Change weapon on", SwitchTypeEnd);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon on", 0);

        animator.SetFloat("MEAG on", 1);

        CHWActive.raycastTarget = true;
        SecondOnline = true;
        WeaponOn = true;
        UsingChangeWeapon = false;
        Active = true;

        GameObject.Find("Game Control").GetComponent<GameControlSystem>().MEAGWeaponOn = true;
        GetComponent<Movement>().HeavyWeaponUsing(2); //중화기 사용 상태 전달
        GetComponent<Movement>().FireJoystickType = 1000;
        //ChangeSkillBtn.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        StartCoroutine(Offline());
        Invoke("AfterOff", 1.2f);
    }

    IEnumerator Offline()
    {
        GetComponent<Movement>().FireJoystickType = 0;
        if (HeavyWeaponUsing == 0)
        {
            animator.SetFloat("Change active number", 100);
            ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        }
        else if (HeavyWeaponUsing == 1)
        {
            animator.SetFloat("Change active number", 200);
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        }
        else if (HeavyWeaponUsing == 2)
        {
            animator.SetFloat("Change active number", 201);
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        }

        WeaponOn = false;
        GetComponent<Movement>().HeavyWeaponUsing(0); //중화기 사용 상태 해제 전달
        animator.SetFloat("MEAG on2", 1);
        animator.Play("MEAG on2");
        SoundManager.instance.SFXPlay6("Sound", MEAGOff);
        yield return new WaitForSeconds(0.417f);
        if (HeavyWeaponUsing == 0)
        {
            if (GunType == 1)
                transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
            if (GunType == 1000)
                transform.Find("bone_1/DS-65").gameObject.SetActive(false);
            if (GunType == 2000)
                transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
            if (SubGunTypeFront == 1)
                transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
            if (SubGunTypeBack == 1)
                transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
        }
        else if (HeavyWeaponUsing == 1)
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
        else if (HeavyWeaponUsing == 2)
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.583f);

        if (HeavyWeaponUsing == 0)
            animator.SetFloat("Change active number", 0);
        animator.SetBool("MEAG off", true);
        animator.SetFloat("MEAG on", 0);
        animator.SetFloat("MEAG on2", 0);
        animator.SetBool("MEAG fire", false);

        if (animator.GetBool("Reload Stop") == true)
            animator.SetBool("Reload Stop", false);

        if (HeavyWeaponUsing == 0)
        {
            if (vM5GrenadeController.BombCount > 0)
            {
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time start, Grenade", false);
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time running, Grenade", false);
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time end, Grenade", true);
            }
            DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
            DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
            ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", false);

            DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        }
        else if (HeavyWeaponUsing >= 1)
        {
            if (vM5GrenadeController.BombCount > 0)
            {
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time start, Grenade", false);
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time running, Grenade", false);
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time end, Grenade", true);
            }
            SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        }

        if (HeavyWeaponUsing == 0)
            GetComponent<Movement>().HeavyWeaponUsing(0); //중화기 사용 상태 전달
        else if (HeavyWeaponUsing == 1)
            GetComponent<Movement>().HeavyWeaponUsing(50);
        else if (HeavyWeaponUsing == 2)
            GetComponent<Movement>().HeavyWeaponUsing(51);

        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().XMoveStop(false); //스킬 사용 해제 전달
        GetComponent<M3078Controller>().TurnOn();
        GetComponent<M3078Controller>().CantSwap = false;
        GetComponent<GunController>().StopReload = false;

        SoundOnePlay = false;
        Active = false;
        SoundTime = 0;
        SoundChargeTime = 0;
        SoundChargeTimeTwo = 0;
        RailCharging = 0;
        Charging = 0;

        if (HeavyWeaponUsing == 0)
            GetComponent<Movement>().FireJoystickType = 1;
        else if (HeavyWeaponUsing >= 1)
            GetComponent<Movement>().FireJoystickType = 100;

        if (HeavyWeaponUsing == 1)
        {
            animator.SetFloat("Gun active", 5000);
            animator.SetBool("M3078 end", false);
            animator.SetFloat("Change active number", 0);
        }
        if (HeavyWeaponUsing == 2)
        {
            animator.SetFloat("Gun active", 5001);
            animator.SetFloat("Change active number", 0);
        }

        GetComponent<GunController>().StopReload = false;
        GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 전달
        movement.UsingChangeWeapon = false;
        SecondOnline = false;
        UsingTask = false;
        UsingChangeWeapon = false;

        yield return new WaitForSeconds(0.5f);

        //수송기 탑승 전에 미리 체인지 중화기 및 강화 중화기 종료
        gameControlSystem.ChangeWeaponOnline--;
        if (GameObject.Find("Game Control").GetComponent<GameControlSystem>().inWeapon == true)
            GetComponent<M3078Controller>().M3078SwapClick();

        if (HeavyWeaponUsing == 0)
        {
            if (vM5GrenadeController.BombCount > 0)
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time end, Grenade", false);
            DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
        }
        else if (HeavyWeaponUsing >= 1)
        {
            if (vM5GrenadeController.BombCount > 0)
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time end, Grenade", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
        }
    }
    void AfterOff()
    {
        animator.SetBool("MEAG off", false);
    }

    public void ChangeOff()
    {
        WeaponOn = false;
        CHWActive.raycastTarget = false;
        GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달

        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        animator.SetFloat("MEAG on", 0);
        animator.SetFloat("MEAG on2", 0);
        animator.SetBool("MEAG fire", false);

        SecondOnline = false;
        UsingChangeWeapon = false;
        SoundOnePlay = false;
        Active = false;
        SoundTime = 0;
        SoundChargeTime = 0;
        SoundChargeTimeTwo = 0;
        RailCharging = 0;
        Charging = 0;
        GetComponent<Movement>().FireJoystickType = 0;
        Invoke("TaskOff", 0.5f);
    }

    void TaskOff()
    {
        UsingTask = false;
    }
}