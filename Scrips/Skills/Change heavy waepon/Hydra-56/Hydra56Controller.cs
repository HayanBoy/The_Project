using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Hydra56Controller : MonoBehaviour
{
    Animator animator;
    GunController gunController;  //GunController�� CharsingEnergy �� ������ ���� ����
    Movement movement;
    GameControlSystem gameControlSystem;
    VM5GrenadeController vM5GrenadeController;
    public Image CHWActive;

    private Shake shake;

    public GameObject GrenadeUI;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public GameObject SwapUI;
    public Image AnimationUICHWLeft;

    public int EnergyNeeds;
    public int GunType; //�⺻ �� Ÿ���� ���޹ޱ� ���� ����
    public int SubGunTypeFront = 0; //�� ������� ��������
    public int SubGunTypeBack = 0; //�� ������� ��������

    public GameObject ChangeSkillBtn;

    public GameObject MetalBullet;
    public Transform MetalBulletPos; //�Ѿ� ���� ��ǥ
    public int MetalBulletDamage; //�Ѿ˴� ������

    private float ActiveTime; //�� ���� ������ �� �ֵ��� ����
    private float SoundTime;
    private float EnergyNeedTime;
    private float CoolTimeRunning; //��Ÿ�� ���� �ð�
    public float CoolTime; //��Ÿ��
    private int Cnt; //��Ÿ���� �����ϵ��� ����

    private bool Active = false;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    public bool Seleted; //�� ���Ⱑ ���õǾ��� ������ �߻��� �� �ֵ��� ����
    public int HeavyWeaponUsing;

    private bool SecondOnline = false; //���� ���� Ȱ��ȭ���� �ʰ� �ٸ� ü���� ���� ���¿��� �ٲپ��� ����� ����ġ
    private bool FireCan = true; //�߻� �غ� ����ġ
    public bool WeaponOn;
    public bool isExplosion;
    public bool UsingTask;

    public float SwitchTypeStart; //ü���� ��ȭ�Ⱑ ��ü�� �� ���� ������ȣ
    public float SwitchTypeEnd; //ü���� ��ȭ�Ⱑ ��ü�� �� �� ������ȣ

    public AudioClip Hydra56On;
    public AudioClip Hydra56Off;
    public AudioClip Hydra56Fire;
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

        MetalBulletDamage = UpgradeDataSystem.instance.Hydra56Damage;
    }

    void Update()
    {
        MetalBulletSkill(); //ü���� Hydra-56 ö���� ��ų
        CHWCool();

        if (WeaponOn == true && SecondOnline == false)
        {
            animator.SetFloat("Gun fire", 0);
            animator.SetBool("SW-06_Effect1", false);
            animator.SetBool("SW-06_Effect2", false);
            animator.SetBool("SW-06_Effect3", false);
            animator.SetBool("SW-06_Effect4", false);

            if (ActiveTime == 0)
            {
                ActiveTime += Time.deltaTime;

                GrenadeUI.GetComponent<Animator>().SetBool("Cool time start, Grenade", true);
                GrenadeUI.GetComponent<Animator>().SetBool("Cool time running, Grenade", true);
                DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
                DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
                ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
                ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
                SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
                SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", true);

                StartCoroutine(ChangeActive());
            }

            UsingTask = true;
            GetComponent<GunController>().LaserGuiding(true); //��ų ��� ���� ����
            movement.UsingChangeWeapon = true;

            //ü���� ��ų ���� ���
            GetComponent<ArthesL775Controller>().XMoveStop(true); //ü���� ��ų ��� ����
            GetComponent<UGG98Controller>().XMoveStop(true); //ü���� ��ų ��� ����
            GetComponent<MEAGController>().XMoveStop(true); //ü���� ��ų ��� ����

            GetComponent<VM5GrenadeController>().XMoveStop(true); //��ų ��� ����
            GetComponent<M3078Controller>().TurnOff();
            GetComponent<M3078Controller>().CantSwap = true;
        }
    }

    //���� ��Ÿ��
    void CHWCool()
    {
        if (Cnt == 0)
        {
            FireCan = false;
            AnimationUICHWLeft.fillAmount = CoolTimeRunning / CoolTime;
            CoolTimeRunning += Time.deltaTime;
        }

        if (AnimationUICHWLeft.fillAmount == 1)
        {
            CoolTimeRunning = 0;
            Cnt = 1;
            FireCan = true;
            AnimationUICHWLeft.fillAmount = 0;
        }
    }

    //���� Ȱ��ȭ �ִϸ��̼�
    IEnumerator ChangeActive()
    {
        FireCan = false;
        gameControlSystem.UsingChangeWeapon = true;
        gameControlSystem.ChangeWeaponOnline++;
        GetComponent<Movement>().FireJoystickType = 0;
        GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����

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

        if (animator.GetBool("Hydra-56 off") == true)
            animator.SetBool("Hydra-56 off", false);
        animator.SetFloat("Hydra-56 on", 1);
        SoundManager.instance.SFXPlay2("DT-37 Fire Sound", Hydra56On);
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
        animator.SetFloat("Hydra-56 on", 0);
        FireCan = true;
        GetComponent<Movement>().FireJoystickType = 1000;
        gameControlSystem.UsingChangeWeapon = false;
    }

    //ü���� Hydra-56 ö���� ��ų
    void MetalBulletSkill()
    {
        if (Seleted == true && FireCan == true && isExplosion && gunController.ChangeSkillEnergy >= EnergyNeeds || FireCan == true && isExplosion && gunController.ChangeSkillEnergy > EnergyNeeds && Input.GetKeyDown(KeyCode.F))
        {
            if(UsingChangeWeapon == false && Reload == false)
            {
                FireCan = false;
                Active = true;
                StartCoroutine(Firing());
            }
        }

        if (Seleted == true && FireCan == true && isExplosion && gunController.ChangeSkillEnergy < EnergyNeeds)
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
        else if (!isExplosion)
            EnergyNeedTime = 0;
    }

    public IEnumerator Firing()
    {
        Cnt = 0;
        Shake.Instance.ShakeCamera(6, 0.25f);
        gunController.ChangeSkillEnergy -= EnergyNeeds;
        animator.SetBool("Hydra-56 Fire", true);
        SoundManager.instance.SFXPlay24("DT-37 Fire Sound", Hydra56Fire);
        GameObject MB = Instantiate(MetalBullet, MetalBulletPos.transform.position, MetalBulletPos.transform.rotation);
        MB.GetComponent<MetalSkillBullet>().SetDamage(MetalBulletDamage); //�Ѿ˿��� ������ ����
        Invoke("MetalSkillBullet_Shell", 2f); // ź�� 2�ʵڿ� �������ϴ� �ڵ� 
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hydra-56 Fire", false);
    }

    public void ChangeOn()
    {
        StartCoroutine(ChangeOnline());
    }

    IEnumerator ChangeOnline()
    {
        UsingTask = true;

        if (animator.GetBool("Hydra-56 off") == true)
            animator.SetBool("Hydra-56 off", false);
        if (SoundTime == 0)
        {
            SoundTime += Time.deltaTime;
            SoundManager.instance.SFXPlay2("DT-37 Fire Sound", Hydra56On);
        }
        GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����
        animator.SetFloat("Change weapon off", SwitchTypeStart);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon off", 0);
        animator.SetFloat("Change weapon on", SwitchTypeEnd);
        yield return new WaitForSeconds(0.35f);
        animator.SetFloat("Change weapon on", 0);

        animator.SetFloat("Hydra-56 on", 1);

        CHWActive.raycastTarget = true;
        SecondOnline = true;
        WeaponOn = true;
        UsingChangeWeapon = false;

        GameObject.Find("Game Control").GetComponent<GameControlSystem>().Hydra56WeaponOn = true;
        GetComponent<Movement>().FireJoystickType = 1000;
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

        animator.SetFloat("Hydra-56 on2", 1);
        animator.Play("Hydra-56 on2");
        SoundManager.instance.SFXPlay2("DT-37 Fire Sound", Hydra56Off);

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
        animator.SetBool("Hydra-56 off", true);
        animator.SetFloat("Hydra-56 on", 0);
        animator.SetFloat("Hydra-56 on2", 0);
        animator.SetBool("Hydra-56 Fire", false);

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
            GetComponent<Movement>().HeavyWeaponUsing(0); //��ȭ�� ��� ���� ����
        else if (HeavyWeaponUsing == 1)
            GetComponent<Movement>().HeavyWeaponUsing(50);
        else if (HeavyWeaponUsing == 2)
            GetComponent<Movement>().HeavyWeaponUsing(51);

        GetComponent<ArthesL775Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<UGG98Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<MEAGController>().XMoveStop(false); //ü���� ��ų ��� ���� ����

        GetComponent<VM5GrenadeController>().XMoveStop(false); //��ų ��� ���� ����
        GetComponent<M3078Controller>().TurnOn();
        GetComponent<M3078Controller>().CantSwap = false;
        GetComponent<GunController>().StopReload = false;

        SecondOnline = false;
        ActiveTime = 0;
        SoundTime = 0;
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
        GetComponent<GunController>().LaserGuiding(false); //��ų ��� ���� ����
        movement.UsingChangeWeapon = false;
        UsingChangeWeapon = false;
        UsingTask = false;

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
        animator.SetBool("Hydra-56 off", false);
    }

    public void ChangeOff()
    {
        WeaponOn = false;
        CHWActive.raycastTarget = false;
        GetComponent<Movement>().HeavyWeaponUsing(1); //��ȭ�� ��� ���� ����

        GetComponent<UGG98Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<MEAGController>().XMoveStop(false); //ü���� ��ų ��� ���� ����
        GetComponent<ArthesL775Controller>().XMoveStop(false); //ü���� ��ų ��� ���� ����

        animator.SetFloat("Hydra-56 on", 0);
        animator.SetFloat("Hydra-56 on2", 0);
        animator.SetBool("Hydra-56 Fire", false);

        UsingChangeWeapon = false;
        SecondOnline = false;
        ActiveTime = 0;
        SoundTime = 0;
        GetComponent<Movement>().FireJoystickType = 0;
        Invoke("TaskOff", 0.5f);
    }

    void TaskOff()
    {
        UsingTask = false;
    }
}
