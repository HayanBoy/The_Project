using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UGG98Controller : MonoBehaviour
{
    Animator animator;
    Movement movement;
    GunController gunController;  //GunController의 CharsingEnergy 값 연동을 위한 선언
    GameControlSystem gameControlSystem;
    VM5GrenadeController vM5GrenadeController;
    public Image CHWActive;

    private Shake shake;

    public GameObject GrenadeUI;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public GameObject SwapUI;
    public Image AnimationUICHWDown;

    public int GunType; //기본 총 타입을 전달받기 위한 변수
    public int SubGunTypeFront = 0; //앞 기관단총 장착여부
    public int SubGunTypeBack = 0; //뒤 기관단총 장착여부

    public GameObject ChangeWeapon; //무기 공격시, 기타 버튼 사용 무력화
    public GameObject Dash;
    public GameObject Grenade;

    public float Charging;

    public int UGG98Step1Damage;
    public int UGG98Step2Damage;

    public GameObject MagnetShotBullet;
    public Transform MagnetShotBulletPos;

    private float MagnetShotSkillCoolTime; // 자력포 산탄포 채워지는 쿨타임
    private float MagnetShotSkill_Lv2_CoolTime; // 강화 자력포 산탄포 채워지는 쿨타임

    public float MagnetCharging;
    private float SoundTime;
    private float SoundChargeTime;
    private float EnergyNeedTime;
    private float CoolTimeRunning; //쿨타임 도는 시간
    public float CoolTime; //쿨타임
    private int Cnt; //쿨타임이 가능하도록 조취

    private bool Active = false;
    private bool ChargingStart = true;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    private bool BallGeneration = false;
    private bool SoundOnePlay = false;
    public bool UsingTask;
    public int HeavyWeaponUsing;

    public bool WeaponOn;
    private bool SecondOnline = false; //제일 먼저 활성화하지 않고 다른 체인지 무기 상태에서 바꾸었을 경우의 스위치
    public float SwitchTypeStart; //체인지 중화기가 교체될 때 시작 고유번호
    public float SwitchTypeEnd; //체인지 중화기가 교체될 때 끝 고유번호

    public Slider ChargingBar;
    public Image ChargingBarColor;
    private float maxCharsing;
    public Image ChargingGauge;
    public Color Step1Color;
    public Color Step2Color;

    public AudioClip UGG98On;
    public AudioClip UGG98Off;
    public AudioClip UGG98Charging1;
    public AudioClip UGG98Charging2;
    public AudioClip UGG98Fire;
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
        maxCharsing = 1.0f;

        UGG98Step1Damage = UpgradeDataSystem.instance.UGG98Step1Damage;
        UGG98Step2Damage = UpgradeDataSystem.instance.UGG98Step2Damage;
    }

    void Update()
    {
        MagnetShotSkill(); //중력건 활성화
        CHWCool();

        if (BallGeneration == true)
            animator.SetBool("UGG98 ball generation", true);
        else
            animator.SetBool("UGG98 ball generation", false);

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

                    StartCoroutine(ChangeActiveGravity()); //중력건 활성화 애니메이션
                    SoundManager.instance.SFXPlay5("Sound", UGG98On);
                }

                UsingTask = true;
                GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달
                GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달
                movement.UsingChangeWeapon = true;

                //체인지 스킬 제한 목록
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<ArthesL775Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<MEAGController>().XMoveStop(true); //체인지 스킬 사용 전달

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
            AnimationUICHWDown.fillAmount = CoolTimeRunning / CoolTime;
            CoolTimeRunning += Time.deltaTime;
        }

        if (AnimationUICHWDown.fillAmount == 1)
        {
            CoolTimeRunning = 0;
            Cnt = 1;
            ChargingStart = true;
            AnimationUICHWDown.fillAmount = 0;
        }
    }

    //충전바 색깔 변경 
    void ColorChanger()
    {
        if (Charging < 1)
        {
            Color healthColor = Step1Color;
            ChargingBarColor.color = healthColor;
        }
        else if (Charging >= 1)
        {
            Color healthColor = Step2Color;
            ChargingBarColor.color = healthColor;
        }
    }

    //중력건 활성화
    void MagnetShotSkill()
    {
        if (WeaponOn && ChargingStart == true && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 || WeaponOn && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 && Input.GetKey(KeyCode.K))
        {
            if(UsingChangeWeapon == false && Reload == false && ChargingStart == true)
            {
                BallGeneration = true;
                GravityGunTransform(); //중력건 차징 변신 제어
                MagnetCharging += Time.deltaTime;
                Charging += Time.deltaTime;

                if (WeaponOn == true)
                {
                    ChargingBar.gameObject.SetActive(true); //충전바 켜기
                    ColorChanger();
                    ChargingBar.value = Mathf.MoveTowards(ChargingBar.value, maxCharsing, Time.deltaTime * 1 / maxCharsing);
                }
            }
        }
        else
        {
            StartCoroutine(MagnetCharge()); //발사
            MagnetCharging = 0;
            Charging = 0;

            if (WeaponOn == true)
            {
                ChargingBar.gameObject.SetActive(false);
                ChargingBar.value = 0;
            }
        }
        if (WeaponOn && ChargingStart == true && movement.FireJoystick && gunController.ChangeSkillEnergy < 30)
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

    //활성화 애니메이션
    public IEnumerator ChangeActiveGravity()
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

        Active = false;
        if (animator.GetBool("UGG98 off") == true)
            animator.SetBool("UGG98 off", false);
        animator.SetFloat("UGG98 on", 1);
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
        animator.SetFloat("UGG98 on", 0);
        ChargingStart = true;
        GetComponent<Movement>().FireJoystickType = 1000;
        gameControlSystem.UsingChangeWeapon = false;
    }

    //중력건 차징 변신 제어
    void GravityGunTransform()
    {
        if (Charging >= 0.01f && Charging < 1)
        {
            animator.SetBool("UGG98 charge 1", true);
        }
        else if (Charging >= 1 && Charging < 2)
        {
            animator.SetBool("UGG98 charge 2", true);
        }
        else if (Charging == 0)
        {
            animator.SetBool("UGG98 charge 1", false);
            animator.SetBool("UGG98 charge 2", false);
        }
    }

    //발사
    public IEnumerator MagnetCharge()
    {
        if (MagnetCharging >= 0.01f && MagnetCharging <= 1.0f)
        {
            Cnt = 0;
            Shake.Instance.ShakeCamera(3, 0.25f);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
            BallGeneration = false;
            gunController.ChangeSkillEnergy -= 15;

            if (gunController.ChangeSkillEnergy <= 0)
                gunController.ChangeSkillEnergy = 0;
            //Debug.Log("1초 미만으로 차징되었습니다");
            animator.SetBool("UGG98 fire", true);
            GameObject MGSS = Instantiate(MagnetShotBullet, MagnetShotBulletPos.transform.position, MagnetShotBulletPos.transform.rotation);
            MGSS.GetComponent<GravityBall1>().damage = UGG98Step1Damage;
            MGSS.GetComponent<GravityBall1>().ChargeBall(1); //중력볼 사라지는 시간 변수 전달
            SoundManager.instance.SFXPlay5("Sound", UGG98Fire);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("UGG98 charge 1", false);
            animator.SetBool("UGG98 fire", false);

            GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
            GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

            ChangeWeapon.gameObject.SetActive(true);
            Dash.gameObject.SetActive(true);
            Grenade.gameObject.SetActive(true);

            UsingTask = false;
            UsingChangeWeapon = false;
            SoundChargeTime = 0;

        }
        else if (MagnetCharging >= 1.01f)
        {
            Cnt = 0;
            Shake.Instance.ShakeCamera(5, 0.5f);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
            BallGeneration = false;
            gunController.ChangeSkillEnergy -= 30;

            if (gunController.ChangeSkillEnergy <= 0)
                gunController.ChangeSkillEnergy = 0;
            //Debug.Log("1초간 차징되었습니다");
            animator.SetBool("UGG98 fire2", true);
            GameObject MGSS = Instantiate(MagnetShotBullet, MagnetShotBulletPos.transform.position, MagnetShotBulletPos.transform.rotation);
            MGSS.GetComponent<GravityBall1>().damage = UGG98Step2Damage;
            SoundManager.instance.SFXPlay5("Sound", UGG98Fire);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("UGG98 charge 1", false);
            animator.SetBool("UGG98 charge 2", false);
            animator.SetBool("UGG98 fire2", false);

            GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
            GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

            ChangeWeapon.gameObject.SetActive(true);
            Dash.gameObject.SetActive(true);
            Grenade.gameObject.SetActive(true);

            UsingTask = false;
            UsingChangeWeapon = false;
            SoundChargeTime = 0;
        }
    }

    public void ChangeOn()
    {
        StartCoroutine(ChangeOnline());
    }

    IEnumerator ChangeOnline()
    {
        UsingTask = true;

        if (animator.GetBool("UGG98 off") == true)
            animator.SetBool("UGG98 off", false);
        if (SoundTime == 0)
        {
            SoundTime += Time.deltaTime;
            SoundManager.instance.SFXPlay5("Sound", UGG98On);
        }
        animator.SetFloat("Change weapon off", SwitchTypeStart);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon off", 0);
        animator.SetFloat("Change weapon on", SwitchTypeEnd);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon on", 0);

        animator.SetFloat("UGG98 on", 1);

        CHWActive.raycastTarget = true;
        SecondOnline = true;
        WeaponOn = true;
        UsingChangeWeapon = false;

        GameObject.Find("Game Control").GetComponent<GameControlSystem>().UGG98WeaponOn = true;
        GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달
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
        animator.SetBool("UGG98 idle", false);
        animator.SetFloat("UGG98 on2", 1);
        animator.Play("UGG98 on2");
        SoundManager.instance.SFXPlay4("Sound", UGG98Off);
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
        animator.SetBool("UGG98 off", true);
        animator.SetFloat("UGG98 on", 0);
        animator.SetFloat("UGG98 on2", 0);

        if (animator.GetBool("Reload Stop") == true)
            animator.SetBool("Reload Stop", false);

        if (HeavyWeaponUsing == 0)
            GetComponent<Movement>().HeavyWeaponUsing(0); //중화기 사용 상태 전달
        else if (HeavyWeaponUsing == 1)
            GetComponent<Movement>().HeavyWeaponUsing(50);
        else if (HeavyWeaponUsing == 2)
            GetComponent<Movement>().HeavyWeaponUsing(51);

        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().XMoveStop(false); //스킬 사용 해제 전달
        GetComponent<M3078Controller>().TurnOn();
        GetComponent<M3078Controller>().CantSwap = false;
        GetComponent<GunController>().StopReload = false;

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

        SoundOnePlay = false;
        SoundTime = 0;
        SoundChargeTime = 0;

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
        GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 해제 전달
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
        animator.SetBool("UGG98 off", false);
    }

    public void ChangeOff()
    {
        WeaponOn = false;
        CHWActive.raycastTarget = false;
        GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달

        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        animator.SetBool("UGG98 idle", false);
        animator.SetFloat("UGG98 on", 0);
        animator.SetFloat("UGG98 on2", 0);

        SecondOnline = false;
        UsingChangeWeapon = false;
        SoundOnePlay = false;
        SoundTime = 0;
        SoundChargeTime = 0;
        GetComponent<Movement>().FireJoystickType = 0;
        Invoke("TaskOff", 0.5f);
    }

    void TaskOff()
    {
        UsingTask = false;
    }
}
