using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UGG98Controller : MonoBehaviour
{
    Animator animator;
    Movement movement;
    GunController gunController;  //GunController�� CharsingEnergy �� ������ ���� ����
    GameControlSystem gameControlSystem;
    VM5GrenadeController vM5GrenadeController;
    public Image CHWActive;

    private Shake shake;

    public GameObject GrenadeUI;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public GameObject SwapUI;
    public Image AnimationUICHWDown;

    public int GunType; //�⺻ �� Ÿ���� ���޹ޱ� ���� ����
    public int SubGunTypeFront = 0; //�� ������� ��������
    public int SubGunTypeBack = 0; //�� ������� ��������

    public GameObject ChangeWeapon; //���� ���ݽ�, ��Ÿ ��ư ��� ����ȭ
    public GameObject Dash;
    public GameObject Grenade;

    public float Charging;

    public int UGG98Step1Damage;
    public int UGG98Step2Damage;

    public GameObject MagnetShotBullet;
    public Transform MagnetShotBulletPos;

    private float MagnetShotSkillCoolTime; // �ڷ��� ��ź�� ä������ ��Ÿ��
    private float MagnetShotSkill_Lv2_CoolTime; // ��ȭ �ڷ��� ��ź�� ä������ ��Ÿ��

    public float MagnetCharging;
    private float SoundTime;
    private float SoundChargeTime;
    private float EnergyNeedTime;
    private float CoolTimeRunning; //��Ÿ�� ���� �ð�
    public float CoolTime; //��Ÿ��
    private int Cnt; //��Ÿ���� �����ϵ��� ����

    private bool Active = false;
    private bool ChargingStart = true;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    private bool BallGeneration = false;
    private bool SoundOnePlay = false;
    public bool UsingTask;
    public int HeavyWeaponUsing;

    public bool WeaponOn;
    private bool SecondOnline = false; //���� ���� Ȱ��ȭ���� �ʰ� �ٸ� ü���� ���� ���¿��� �ٲپ��� ����� ����ġ
    public float SwitchTypeStart; //ü���� ��ȭ�Ⱑ ��ü�� �� ���� ������ȣ
    public float SwitchTypeEnd; //ü���� ��ȭ�Ⱑ ��ü�� �� �� ������ȣ

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

    //X�� ���� ���� ����
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
        MagnetShotSkill(); //�߷°� Ȱ��ȭ
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

                    StartCoroutine(ChangeActiveGravity()); //�߷°� Ȱ��ȭ �ִϸ��̼�
                    SoundManager.instance.SFXPlay5("Sound", UGG98On);
                }

                UsingTask = true;
                GetComponent<GunController>().LaserGuiding(true); //��ų ��� ���� ����
                GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����
                movement.UsingChangeWeapon = true;

                //ü���� ��ų ���� ���
                GetComponent<Hydra56Controller>().XMoveStop(true); //ü���� ��ų ��� ����
                GetComponent<ArthesL775Controller>().XMoveStop(true); //ü���� ��ų ��� ����
                GetComponent<MEAGController>().XMoveStop(true); //ü���� ��ų ��� ����

                GetComponent<VM5GrenadeController>().XMoveStop(true); //��ų ��� ����
                GetComponent<M3078Controller>().TurnOff();
                GetComponent<M3078Controller>().CantSwap = true;
            }
        }
    }

    //���� ��Ÿ��
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

    //������ ���� ���� 
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

    //�߷°� Ȱ��ȭ
    void MagnetShotSkill()
    {
        if (WeaponOn && ChargingStart == true && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 || WeaponOn && movement.FireJoystick && gunController.ChangeSkillEnergy >= 30 && Input.GetKey(KeyCode.K))
        {
            if(UsingChangeWeapon == false && Reload == false && ChargingStart == true)
            {
                BallGeneration = true;
                GravityGunTransform(); //�߷°� ��¡ ���� ����
                MagnetCharging += Time.deltaTime;
                Charging += Time.deltaTime;

                if (WeaponOn == true)
                {
                    ChargingBar.gameObject.SetActive(true); //������ �ѱ�
                    ColorChanger();
                    ChargingBar.value = Mathf.MoveTowards(ChargingBar.value, maxCharsing, Time.deltaTime * 1 / maxCharsing);
                }
            }
        }
        else
        {
            StartCoroutine(MagnetCharge()); //�߻�
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
                //Debug.Log("�������� �����մϴ�.");
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

    //Ȱ��ȭ �ִϸ��̼�
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

    //�߷°� ��¡ ���� ����
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

    //�߻�
    public IEnumerator MagnetCharge()
    {
        if (MagnetCharging >= 0.01f && MagnetCharging <= 1.0f)
        {
            Cnt = 0;
            Shake.Instance.ShakeCamera(3, 0.25f);
            GetComponent<Movement>().XMoveStop(true); //X���� ���� ����
            GetComponent<Movement>().MovingStop(true); //���� ���� ����
            BallGeneration = false;
            gunController.ChangeSkillEnergy -= 15;

            if (gunController.ChangeSkillEnergy <= 0)
                gunController.ChangeSkillEnergy = 0;
            //Debug.Log("1�� �̸����� ��¡�Ǿ����ϴ�");
            animator.SetBool("UGG98 fire", true);
            GameObject MGSS = Instantiate(MagnetShotBullet, MagnetShotBulletPos.transform.position, MagnetShotBulletPos.transform.rotation);
            MGSS.GetComponent<GravityBall1>().damage = UGG98Step1Damage;
            MGSS.GetComponent<GravityBall1>().ChargeBall(1); //�߷º� ������� �ð� ���� ����
            SoundManager.instance.SFXPlay5("Sound", UGG98Fire);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("UGG98 charge 1", false);
            animator.SetBool("UGG98 fire", false);

            GetComponent<Movement>().XMoveStop(false); //X���� ���� ���� ����
            GetComponent<Movement>().MovingStop(false); //���� ���� ���� ����

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
            GetComponent<Movement>().XMoveStop(true); //X���� ���� ����
            GetComponent<Movement>().MovingStop(true); //���� ���� ����
            BallGeneration = false;
            gunController.ChangeSkillEnergy -= 30;

            if (gunController.ChangeSkillEnergy <= 0)
                gunController.ChangeSkillEnergy = 0;
            //Debug.Log("1�ʰ� ��¡�Ǿ����ϴ�");
            animator.SetBool("UGG98 fire2", true);
            GameObject MGSS = Instantiate(MagnetShotBullet, MagnetShotBulletPos.transform.position, MagnetShotBulletPos.transform.rotation);
            MGSS.GetComponent<GravityBall1>().damage = UGG98Step2Damage;
            SoundManager.instance.SFXPlay5("Sound", UGG98Fire);
            yield return new WaitForSeconds(0.583f);

            animator.SetBool("UGG98 charge 1", false);
            animator.SetBool("UGG98 charge 2", false);
            animator.SetBool("UGG98 fire2", false);

            GetComponent<Movement>().XMoveStop(false); //X���� ���� ���� ����
            GetComponent<Movement>().MovingStop(false); //���� ���� ���� ����

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
        GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����
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
        GetComponent<Movement>().HeavyWeaponUsing(0); //��ȭ�� ��� ���� ���� ����
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
            GetComponent<Movement>().HeavyWeaponUsing(0); //��ȭ�� ��� ���� ����
        else if (HeavyWeaponUsing == 1)
            GetComponent<Movement>().HeavyWeaponUsing(50);
        else if (HeavyWeaponUsing == 2)
            GetComponent<Movement>().HeavyWeaponUsing(51);

        GetComponent<Hydra56Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<ArthesL775Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����

        GetComponent<VM5GrenadeController>().XMoveStop(false); //��ų ��� ���� ����
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
        GetComponent<GunController>().LaserGuiding(false); //��ų ��� ���� ���� ����
        movement.UsingChangeWeapon = false;
        SecondOnline = false;
        UsingTask = false;
        UsingChangeWeapon = false;

        yield return new WaitForSeconds(0.5f);

        //���۱� ž�� ���� �̸� ü���� ��ȭ�� �� ��ȭ ��ȭ�� ����
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
        GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����

        GetComponent<Hydra56Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<MEAGController>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<ArthesL775Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����

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
