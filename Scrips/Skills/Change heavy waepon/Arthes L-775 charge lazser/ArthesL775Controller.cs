using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ArthesL775Controller : MonoBehaviour
{
    Animator animator;
    Movement movement;
    GunController gunController;  //GunController의 CharsingEnergy 값 연동을 위한 선언
    ChangeSkillSystem changeSkillSystem;
    GameControlSystem gameControlSystem;
    VM5GrenadeController vM5GrenadeController;
    public Image CHWActive;

    private Shake shake;

    public GameObject GrenadeUI;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public GameObject SwapUI;
    public Image AnimationUICHWTop;

    Coroutine FireLaserSyetem;

    public int GunType; //기본 총 타입을 전달받기 위한 변수
    public int SubGunTypeFront = 0; //앞 기관단총 장착여부
    public int SubGunTypeBack = 0; //뒤 기관단총 장착여부
    private int FireState; //발사시 몇 단계에서 실시했는지를 보여주는 변수

    public int EnergyNeeds1;
    public int EnergyNeeds2;
    public int EnergyNeeds3;
    public int EnergyNeeds4;
    public int EnergyNeeds5;
    public float FireTime; //발사하는 동안 에너지 사용량 제어용도
    private float EnergyNeedTime;
    private bool Firing = false;

    private float Charging;
    public GameObject ChangeSkillBtn;

    GameObject FireLaser;
    public GameObject ChargingRaserBullet0;
    public GameObject ChargingRaserBullet;
    public GameObject ChargingRaserBullet1;
    public GameObject ChargingRaserBullet2;
    public GameObject ChargingRaserBullet3;
    public Transform ChargingRaserBulletPos1; //레이저 총알 생성 좌표
    public Transform ChargingRaserBulletPos2;
    public Transform ChargingRaserBulletPos3;
    public Transform ChargingRaserBulletPos4;
    public int FirstStepRaserDamage; //1단계 레이저 총알당 데미지
    public int SecondStepRaserDamage; //2단계 레이저 총알당 데미지
    public int ThirdStepRaserDamage; //3단계 레이저 총알당 데미지
    public int ForthStepRaserDamage; //4단계 레이저 총알당 데미지
    public float ChargingRaserTime;
    public float Attacktime;
    private float SoundTime;
    private float SoundChargeTime;
    private float SoundChargeTimeTwo;
    private float SoundChargeTimeThree;
    private float HeavyTime; //HeavyWeaponUsing 전달을 한 번만 하도록 조취
    private float StopFireTime; //중간에 레이져 사격을 강제 종료를 한 번만 할 수 있도록 조취
    private float CoolTimeRunning; //쿨타임 도는 시간
    public float CoolTime; //쿨타임
    private int Cnt; //쿨타임이 가능하도록 조취

    private bool Active = false;
    private bool ChargingStart = true;
    private bool SoundOnePlay = false;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    private bool StopFiring = false; //사격이 중지되면 CR코루틴 내부가 실행되지 않도록 조취
    public int HeavyWeaponUsing;

    public bool WeaponOn;
    public bool UsingTask;
    private bool SecondOnline = false; //제일 먼저 활성화하지 않고 다른 체인지 무기 상태에서 바꾸었을 경우의 스위치

    public float SwitchTypeStart; //체인지 중화기가 교체될 때 시작 고유번호
    public float SwitchTypeEnd; //체인지 중화기가 교체될 때 끝 고유번호

    public Slider ChargingBar;
    public Image ChargingBarColor;
    private float maxCharsing;
    public Image ChargingGauge;
    public Color Step1Color;
    public Color Step2Color;
    public Color Step3Color;
    public Color Step4Color;

    public AudioClip ArthesL775On;
    public AudioClip ArthesL775Off;
    public AudioClip ArthesL775Charging1;
    public AudioClip ArthesL775Charging2;
    public AudioClip ArthesL775Charging3;
    public AudioClip ArthesL775Charging4;
    public AudioClip ArthesL775ChargingChange;
    public AudioClip ArthesL775Fire1;
    public AudioClip ArthesL775Fire2;
    public AudioClip ArthesL775Fire3;
    public AudioClip ArthesL775Fire4;
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
        changeSkillSystem = FindObjectOfType<ChangeSkillSystem>();
        gameControlSystem = FindObjectOfType<GameControlSystem>();
        vM5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        Cnt = 1;
        maxCharsing = 4.0f;

        FirstStepRaserDamage = UpgradeDataSystem.instance.ArthesL775Step1Damage;
        SecondStepRaserDamage = UpgradeDataSystem.instance.ArthesL775Step2Damage;
        ThirdStepRaserDamage = UpgradeDataSystem.instance.ArthesL775Step3Damage;
        ForthStepRaserDamage = UpgradeDataSystem.instance.ArthesL775Step4Damage;
    }

    void Update()
    {
        ChargingRaiser();
        CHWCool();

        if (Firing == true)
        {
            if (FireTime > 0)
                FireTime -= Time.deltaTime;
            if (FireTime <= 0)
            {
                FireTime = 0.1f;
                if (FireState == 1)
                    gunController.ChangeSkillEnergy -= EnergyNeeds1;
                else if (FireState == 2)
                    gunController.ChangeSkillEnergy -= EnergyNeeds2;
                else if (FireState == 3)
                    gunController.ChangeSkillEnergy -= EnergyNeeds3;
                else if (FireState == 4)
                    gunController.ChangeSkillEnergy -= EnergyNeeds4;
            }

            if(FireState == 1)
                Shake.Instance.ShakeCamera(1, 0.1f);
            else if (FireState == 2)
                Shake.Instance.ShakeCamera(2, 0.1f);
            else if (FireState == 3)
                Shake.Instance.ShakeCamera(3, 0.1f);
            else if (FireState == 4)
                Shake.Instance.ShakeCamera(5, 0.1f);
        }

        //에너지가 부족할 경우, 즉시 발사 중지
        if (WeaponOn == true && gunController.ChangeSkillEnergy <= 0)
        {
            if (FireLaserSyetem != null)
                StopCoroutine(FireLaserSyetem);

            if (StopFireTime == 0)
            {
                StopFireTime += Time.deltaTime;
                StopFiring = true;
                gunController.ChangeSkillEnergy = 0;
                StartCoroutine(StopFire());
            }
        }

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

                    StartCoroutine(ChangeActive());
                    GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달
                    SoundManager.instance.SFXPlay2("Sound", ArthesL775On);
                    HeavyTime = 0;
                }

                UsingTask = true;
                GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달
                movement.UsingChangeWeapon = true;

                //체인지 스킬 제한 목록
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<UGG98Controller>().XMoveStop(true); //체인지 스킬 사용 전달
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
            AnimationUICHWTop.fillAmount = CoolTimeRunning / CoolTime;
            CoolTimeRunning += Time.deltaTime;
        }

        if (AnimationUICHWTop.fillAmount == 1)
        {
            CoolTimeRunning = 0;
            Cnt = 1;
            ChargingStart = true;
            if (StopFiring == true)
                StopFiring = false;
            AnimationUICHWTop.fillAmount = 0;
        }
    }

    //충전바 색깔 변경 
    void ColorChanger()
    {
        if (Charging < 2)
        {
            Color healthColor = Step1Color;
            ChargingBarColor.color = healthColor;
        }
        else if (Charging >= 2 && Charging < 3)
        {
            Color healthColor = Step2Color;
            ChargingBarColor.color = healthColor;
        }
        else if (Charging >= 3 && Charging < 4)
        {
            Color healthColor = Step3Color;
            ChargingBarColor.color = healthColor;
        }
        else if (Charging >= 4)
        {
            Color healthColor = Step4Color;
            ChargingBarColor.color = healthColor;
        }
    }

    //차징 레이저 스킬
    void ChargingRaiser()
    {
        if (WeaponOn && movement.FireJoystick && ChargingStart == true && gunController.ChangeSkillEnergy > 1 || WeaponOn && movement.FireJoystick && gunController.ChangeSkillEnergy > 1 && Input.GetKey(KeyCode.H))
        {
            if(UsingChangeWeapon == false && Reload == false)
            {
                if (ChargingStart == true)
                {
                    LaserTransform();
                    ColorChanger();
                    ChargingBar.value = Mathf.MoveTowards(ChargingBar.value, maxCharsing, Time.deltaTime * 1 / maxCharsing);

                    if (Charging == 0)
                    {
                        SoundManager.instance.SFXPlay29("Sound", ArthesL775Charging1);
                    }
                    if (Charging > 2 && Charging < 2.1f)
                    {
                        if (SoundChargeTime == 0)
                        {
                            SoundChargeTime += Time.deltaTime;
                            SoundManager.instance.SFXPlay12("Sound", ArthesL775ChargingChange);
                            SoundManager.instance.SFXPlay24("Sound", ArthesL775Charging2);
                        }
                    }
                    if(Charging > 3 && Charging < 3.1f)
                    {
                        if (SoundChargeTimeTwo == 0)
                        {
                            SoundChargeTimeTwo += Time.deltaTime;
                            SoundManager.instance.SFXPlay12("Sound", ArthesL775ChargingChange);
                            SoundManager.instance.SFXPlay4("Sound", ArthesL775Charging3);
                        }
                    }
                    if(Charging > 4 && Charging < 4.1f)
                    {
                        if (SoundChargeTimeThree == 0)
                        {
                            SoundChargeTimeThree += Time.deltaTime;
                            SoundManager.instance.SFXPlay12("Sound", ArthesL775ChargingChange);
                            SoundManager.instance.SFXPlay9("Sound", ArthesL775Charging4);
                        }
                    }

                    if (WeaponOn == true)
                    {
                        Charging += Time.deltaTime;
                        animator.SetBool("Arthes L-775 charging", true);
                        ChargingBar.gameObject.SetActive(true); //충전바 켜기
                    }
                }
            }
        }
        else
        {
            FireLaserSyetem = StartCoroutine(CR());
            animator.SetBool("Arthes L-775 charging", false);
            Charging = 0;

            if (WeaponOn == true)
            {
                ChargingBar.gameObject.SetActive(false);
                ChargingBar.value = 0;
            }
        }

        if (WeaponOn && ChargingStart == true && movement.FireJoystick == true && gunController.ChangeSkillEnergy == 0)
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

    //레이져 스킬 가동 애니메이션
    public IEnumerator ChangeActive()
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
        if (animator.GetBool("Arthes L-775 off") == true)
            animator.SetBool("Arthes L-775 off", false);
        animator.SetFloat("Arthes L-775 on", 1);
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
        animator.SetFloat("Arthes L-775 on", 0);
        ChargingStart = true;
        GetComponent<Movement>().FireJoystickType = 1000;
        gameControlSystem.UsingChangeWeapon = false;
        if (HeavyTime == 0)
        {
            HeavyTime += Time.deltaTime;
            GetComponent<Movement>().HeavyWeaponUsing(3); //중화기 사용 상태 전달
        }
    }


    //레이져 차징 변신 제어
    void LaserTransform()
    {
        if (Charging >= 2 && Charging < 3)
        {
            animator.SetBool("Arthes L-775 charge 1", true);
        }
        else if (Charging >= 3 && Charging < 4)
        {
            animator.SetBool("Arthes L-775 charge 2", true);
        }
        else if (Charging >= 4)
        {
            animator.SetBool("Arthes L-775 charge 3", true);
        }
        else if (Charging == 0)
        {
            animator.SetBool("Arthes L-775 charge 1", false);
            animator.SetBool("Arthes L-775 charge 2", false);
            animator.SetBool("Arthes L-775 charge 3", false);
        }
    }

    //차징 레이저 발사
    public IEnumerator CR()
    {
        if (Charging >= 0.01f && Charging <= 2.0f) //1단계
        {
            Firing = true;
            FireState = 1;
            StopFireTime = 0;
            UsingChangeWeapon = true;

            gameControlSystem.UsingChangeWeapon = true;
            changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = true;

            //Debug.Log("1초 미만으로 차징되었습니다");
            animator.SetBool("Arthes L-775 attack", true);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            FireLaser = Instantiate(ChargingRaserBullet0, ChargingRaserBulletPos1.transform.position, ChargingRaserBulletPos1.transform.rotation);
            FireLaser.GetComponent<RaserSkillBullet>().SetDamageBeam(FirstStepRaserDamage); //총알에다 데미지 전달
            FireLaser.GetComponent<RaserSkillBullet>().SetBeam(1); //총알에다 빔 이펙트 값 전달

            yield return new WaitForSeconds(Attacktime);
            if (StopFiring == false)
            {
                Firing = false;
                Cnt = 0;
                FireTime = 0;
                Destroy(FireLaser);
                GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
                animator.SetBool("Arthes L-775 attack", false);
                animator.SetBool("Arthes L-775 attack end 1", true);
                SoundManager.instance.SFXPlay27("Sound", ArthesL775Fire1);
                yield return new WaitForSeconds(0.583f);

                animator.SetBool("Arthes L-775 attack end 1", false);

                gameControlSystem.UsingChangeWeapon = false;
                changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = false;
                GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
                GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

                UsingChangeWeapon = false;
                SoundChargeTime = 0;
                SoundChargeTimeTwo = 0;
                SoundChargeTimeThree = 0;
            }
        }        
        else if (Charging >= 2.01f && Charging <= 3.0f) //2단계
        {
            Firing = true;
            FireState = 2;
            StopFireTime = 0;
            UsingChangeWeapon = true;

            gameControlSystem.UsingChangeWeapon = true;
            changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = true;

            //Debug.Log("2초간 차징되었습니다");
            animator.SetBool("Arthes L-775 attack2", true);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            FireLaser = Instantiate(ChargingRaserBullet1, ChargingRaserBulletPos2.transform.position, ChargingRaserBulletPos2.transform.rotation);
            FireLaser.GetComponent<RaserSkillBullet>().SetDamageBeam(SecondStepRaserDamage); //총알에다 데미지 전달
            FireLaser.GetComponent<RaserSkillBullet>().SetBeam(2); //총알에다 빔 이펙트 값 전달

            yield return new WaitForSeconds(Attacktime);
            if (StopFiring == false)
            {
                Firing = false;
                Cnt = 0;
                FireTime = 0;
                Destroy(FireLaser);
                GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
                animator.SetBool("Arthes L-775 attack2", false);
                animator.SetBool("Arthes L-775 attack end 2", true);
                SoundManager.instance.SFXPlay23("Sound", ArthesL775Fire2);
                yield return new WaitForSeconds(0.583f);

                animator.SetBool("Arthes L-775 charge 1", false);
                animator.SetBool("Arthes L-775 attack end 2", false);

                gameControlSystem.UsingChangeWeapon = false;
                changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = false;
                GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
                GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

                UsingChangeWeapon = false;
                SoundChargeTime = 0;
                SoundChargeTimeTwo = 0;
                SoundChargeTimeThree = 0;
            }
        }
        else if (Charging >= 3.01f && Charging <= 4.0f) //3단계
        {
            Firing = true;
            FireState = 3;
            StopFireTime = 0;
            UsingChangeWeapon = true;

            gameControlSystem.UsingChangeWeapon = true;
            changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = true;

            //Debug.Log("3초간 차징되었습니다");
            animator.SetBool("Arthes L-775 attack3", true);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            FireLaser = Instantiate(ChargingRaserBullet2, ChargingRaserBulletPos3.transform.position, ChargingRaserBulletPos3.transform.rotation);
            FireLaser.GetComponent<RaserSkillBullet>().SetDamageBeam(ThirdStepRaserDamage); //총알에다 데미지 전달
            FireLaser.GetComponent<RaserSkillBullet>().SetBeam(3); //총알에다 빔 이펙트 값 전달

            yield return new WaitForSeconds(Attacktime);
            if (StopFiring == false)
            {
                Firing = false;
                Cnt = 0;
                FireTime = 0;
                Destroy(FireLaser);
                GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
                animator.SetBool("Arthes L-775 attack3", false);
                animator.SetBool("Arthes L-775 attack end 3", true);
                SoundManager.instance.SFXPlay5("Sound", ArthesL775Fire3);
                yield return new WaitForSeconds(0.83f);

                animator.SetBool("Arthes L-775 charge 1", false);
                animator.SetBool("Arthes L-775 charge 2", false);
                animator.SetBool("Arthes L-775 attack end 3", false);

                gameControlSystem.UsingChangeWeapon = false;
                changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = false;
                GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
                GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

                UsingChangeWeapon = false;
                SoundChargeTime = 0;
                SoundChargeTimeTwo = 0;
                SoundChargeTimeThree = 0;
            }
        }
        else if (Charging >= 4.01f) //4단계
        {
            Firing = true;
            FireState = 4;
            StopFireTime = 0;
            UsingChangeWeapon = true;

            gameControlSystem.UsingChangeWeapon = true;
            changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = true;

            //Debug.Log("4초간 차징되었습니다");
            animator.SetBool("Arthes L-775 attack4", true);
            GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
            FireLaser = Instantiate(ChargingRaserBullet3, ChargingRaserBulletPos4.transform.position, ChargingRaserBulletPos4.transform.rotation);
            FireLaser.GetComponent<RaserSkillBullet>().SetDamageBeam(ForthStepRaserDamage); //총알에다 데미지 전달
            FireLaser.GetComponent<RaserSkillBullet>().SetBeam(4); //총알에다 빔 이펙트 값 전달

            yield return new WaitForSeconds(Attacktime);
            if (StopFiring == false)
            {
                Firing = false;
                Cnt = 0;
                FireTime = 0;
                Destroy(FireLaser);
                GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달
                animator.SetBool("Arthes L-775 attack4", false);
                animator.SetBool("Arthes L-775 attack end 4", true);
                SoundManager.instance.SFXPlay8("Sound", ArthesL775Fire4);
                yield return new WaitForSeconds(1.16f);

                animator.SetBool("Arthes L-775 charge 1", false);
                animator.SetBool("Arthes L-775 charge 2", false);
                animator.SetBool("Arthes L-775 charge 3", false);
                animator.SetBool("Arthes L-775 attack end 4", false);

                gameControlSystem.UsingChangeWeapon = false;
                changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = false;
                GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
                GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

                UsingChangeWeapon = false;
                SoundChargeTime = 0;
                SoundChargeTimeTwo = 0;
                SoundChargeTimeThree = 0;

                if (movement.EnteringShuttle == true)
                {
                    gameControlSystem.ChangeWeaponOnline = 0;
                    gameControlSystem.ChangeWeaponSystemClick();
                }
            }
        }
    }

    //발사 중지
    public IEnumerator StopFire()
    {
        Firing = false;
        FireTime = 0;
        Cnt = 0;
        Destroy(FireLaser);
        GetComponent<Movement>().MovingStop(true); //멈춤 상태 전달

        if (FireState == 1)
        {
            animator.SetBool("Arthes L-775 attack", false);
            animator.SetBool("Arthes L-775 attack end 1", true);
            SoundManager.instance.SFXPlay27("Sound", ArthesL775Fire1);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("Arthes L-775 attack end 1", false);
        }
        else if (FireState == 2)
        {
            animator.SetBool("Arthes L-775 attack2", false);
            animator.SetBool("Arthes L-775 attack end 2", true);
            SoundManager.instance.SFXPlay23("Sound", ArthesL775Fire2);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("Arthes L-775 charge 1", false);
            animator.SetBool("Arthes L-775 attack end 2", false);
        }
        else if (FireState == 3)
        {
            animator.SetBool("Arthes L-775 attack3", false);
            animator.SetBool("Arthes L-775 attack end 3", true);
            SoundManager.instance.SFXPlay5("Sound", ArthesL775Fire3);
            yield return new WaitForSeconds(0.83f);

            animator.SetBool("Arthes L-775 charge 1", false);
            animator.SetBool("Arthes L-775 charge 2", false);
            animator.SetBool("Arthes L-775 attack end 3", false);
        }
        else if (FireState == 4)
        {
            animator.SetBool("Arthes L-775 attack4", false);
            animator.SetBool("Arthes L-775 attack end 4", true);
            SoundManager.instance.SFXPlay8("Sound", ArthesL775Fire4);
            yield return new WaitForSeconds(1.16f);

            animator.SetBool("Arthes L-775 charge 1", false);
            animator.SetBool("Arthes L-775 charge 2", false);
            animator.SetBool("Arthes L-775 charge 3", false);
            animator.SetBool("Arthes L-775 attack end 4", false);
        }

        gameControlSystem.UsingChangeWeapon = false;
        changeSkillSystem.GetComponent<ChangeSkillSystem>().UsingChangeWeapon = false;
        GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달
        GetComponent<Movement>().MovingStop(false); //멈춤 상태 해제 전달

        UsingChangeWeapon = false;
        SoundChargeTime = 0;
        SoundChargeTimeTwo = 0;
        SoundChargeTimeThree = 0;

        if (movement.EnteringShuttle == true)
        {
            gameControlSystem.ChangeWeaponOnline = 0;
            gameControlSystem.ChangeWeaponSystemClick();
        }
    }

    public void ChangeOn()
    {
        StartCoroutine(ChangeOnline());
    }

    IEnumerator ChangeOnline()
    {
        UsingTask = true;

        if (animator.GetBool("Arthes L-775 off") == true)
            animator.SetBool("Arthes L-775 off", false);
        if (SoundTime == 0)
        {
            SoundTime += Time.deltaTime;
            SoundManager.instance.SFXPlay2("Sound", ArthesL775On);
        }
        animator.SetFloat("Change weapon off", SwitchTypeStart);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon off", 0);
        animator.SetFloat("Change weapon on", SwitchTypeEnd);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon on", 0);

        animator.SetFloat("Arthes L-775 on", 1);

        CHWActive.raycastTarget = true;
        SecondOnline = true;
        WeaponOn = true;
        UsingChangeWeapon = false;

        GameObject.Find("Game Control").GetComponent<GameControlSystem>().ArthesL775WeaponOn = true;
        GetComponent<Movement>().HeavyWeaponUsing(3); //중화기 사용 상태 전달
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
        animator.SetFloat("Arthes L-775 on2", 1);
        SoundManager.instance.SFXPlay2("Sound", ArthesL775Off);
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
        animator.SetBool("Arthes L-775 off", true);
        animator.SetFloat("Arthes L-775 on", 0);
        animator.SetFloat("Arthes L-775 on2", 0);
        animator.SetBool("Arthes L-775 charge 1", false);
        animator.SetBool("Arthes L-775 charge 2", false);
        animator.SetBool("Arthes L-775 charge 3", false);

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

        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        GetComponent<VM5GrenadeController>().XMoveStop(false); //스킬 사용 해제 전달
        GetComponent<M3078Controller>().TurnOn();
        GetComponent<M3078Controller>().CantSwap = false;
        GetComponent<GunController>().StopReload = false;

        SoundOnePlay = false;
        SecondOnline = false;
        SoundTime = 0;
        SoundChargeTime = 0;
        SoundChargeTimeThree = 0;
        SoundChargeTimeTwo = 0;

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
        animator.SetBool("Arthes L-775 off", false);
    }

    public void ChangeOff()
    {
        WeaponOn = false;
        CHWActive.raycastTarget = false;
        GetComponent<Movement>().HeavyWeaponUsing(1); //중화기 사용 상태 전달

        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달

        animator.SetBool("Arthes L-775 idle", false);
        animator.SetFloat("Arthes L-775 on", 0);
        animator.SetFloat("Arthes L-775 on2", 0);
        animator.SetBool("Arthes L-775 charge 1", false);
        animator.SetBool("Arthes L-775 charge 2", false);
        animator.SetBool("Arthes L-775 charge 3", false);

        UsingChangeWeapon = false;
        SoundOnePlay = false;
        SecondOnline = false;
        SoundTime = 0;
        SoundChargeTime = 0;
        SoundChargeTimeThree = 0;
        SoundChargeTimeTwo = 0;
        GetComponent<Movement>().FireJoystickType = 0;
        Invoke("TaskOff", 0.5f);
    }

    void TaskOff()
    {
        UsingTask = false;
    }
}