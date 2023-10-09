using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class APCController : MonoBehaviour
{
    VehicleObjectsManager VehicleObjectsManager;
    VehicleLanding vehicleLanding;
    RobotMove robotMove;
    Animator animator;

    private Shake shake;
    private float FireShake;

    public GameObject APCUI;
    public GameObject AnimationUIVehicleHUD;
    public Text EnergyText;
    public Text EnergyText2;
    public Image FillBar;

    GameObject APCAmmo;
    public GameObject PlazmaFire;
    public Transform APCAmmoPos; //�Ѿ� ���� ��ǥ
    public int FireDamage; //���� ������
    public int FirstDamage; //���� ������
    public int Damage; //������ ������ ��·�
    private int StartAmmo; //ù ������ �� �־����� ������. �� ������ ��꿡 ����.
    public int ViewEnergy; //�ΰ��� ������ ǥ�ñ�
    public int EnergyAmount; //��������
    public int FillEnergyAmount; //������ ȸ����
    public int EnergyNeed; //�ѹ� ������ �ʿ��� �ּ� ��������
    public int EnergyNeeds; //������ �ʿ��� ������ ��·�
    public int FireEnergy; //�߻�� ���� �Ҹ�Ǵ� ��������

    private float AmmoAmountBar;
    private float Charging;
    private float SoundChargeTime;
    private float SoundChargeTimeTwo;
    private float EnergyNeedTime;
    private float AmmoZeroTime;
    private float ChargeTime;

    public bool isGunActive;
    public bool ChargingStart = false;
    private bool SoundOnePlay = false;
    private bool ChargeEnd = false;
    public bool APCUse = false;

    public AudioClip APCCharging;
    public AudioClip APCFire;
    public AudioClip APCChargingEnd;
    public AudioClip EnergyLow;

    public void FillAmmo()
    {
        EnergyAmount = FillEnergyAmount;
        ViewEnergy = FillEnergyAmount;
    }

    public void ReloadVehicle()
    {
        AmmoZeroTime = 0;
        EnergyAmount = StartAmmo;
        ViewEnergy = StartAmmo;
        AmmoAmountBar = StartAmmo;
    }

    private void UpdateBulletText()
    {
        if (StartAmmo >= 1000)
        {
            if (ViewEnergy >= 1000)
            {
                EnergyText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color><color=#8DFFF3>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color><color=#50888C>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < 1000 && ViewEnergy >= StartAmmo * 0.1f)
            {
                EnergyText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color><color=#8DFFF3>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color><color=#50888C>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < StartAmmo * 0.1f && ViewEnergy >= 100)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < StartAmmo * 0.1f && ViewEnergy < 100 && ViewEnergy >= 10)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < 10 && ViewEnergy >= 1)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color><color=#FF1A15>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color><color=#7E0D0B>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }

            else if (ViewEnergy <= 0)
            {
                if (AmmoZeroTime == 0)
                {
                    AmmoZeroTime += Time.deltaTime;
                    StartCoroutine(ZeroAmmoOverUI());
                }
            }
        }
        else if (StartAmmo < 1000)
        {
            if (ViewEnergy >= 100)
            {
                EnergyText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < 100 && ViewEnergy >= StartAmmo * 0.1f)
            {
                EnergyText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < StartAmmo * 0.1f && ViewEnergy >= 10)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }
            else if (ViewEnergy < 10 && ViewEnergy >= 1)
            {
                EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
                EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            }

            else if (ViewEnergy <= 0)
            {
                if (AmmoZeroTime == 0)
                {
                    AmmoZeroTime += Time.deltaTime;
                    StartCoroutine(ZeroAmmoUI());
                }
            }
        }
    }

    IEnumerator ZeroAmmoOverUI()
    {
        while (ViewEnergy == 0)
        {
            EnergyText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            EnergyText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            if (ViewEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color><color=#8C1411>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color><color=#380302>{3}</color>", ViewEnergy / 1000, ViewEnergy % 1000 / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            if (ViewEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ViewEnergy != 0)
                break;
        }
    }

    IEnumerator ZeroAmmoUI()
    {
        while (ViewEnergy == 0)
        {
            EnergyText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            EnergyText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            if (ViewEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            EnergyText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            EnergyText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color>", ViewEnergy / 100, ViewEnergy % 100 / 10, ViewEnergy % 10);
            if (ViewEnergy != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ViewEnergy != 0)
                break;
        }
    }

    void Start()
    {
        if (APCUse == true)
            GetComponent<VehicleLanding>().APCOnline = true;
        animator = GetComponent<Animator>();
        robotMove = FindObjectOfType<RobotMove>();
        VehicleObjectsManager = FindObjectOfType<VehicleObjectsManager>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();

        FillEnergyAmount = EnergyAmount;
        StartAmmo = EnergyAmount;
        ViewEnergy = EnergyAmount;
        AmmoAmountBar = EnergyAmount;

        FirstDamage = UpgradeDataSystem.instance.MBCA79IronHurricaneAPCDamage;
    }

    void Update()
    {
        UpdateBulletText();
        ChargeAttack();
    }

    void ChargeAttack()
    {
        if (isGunActive && ChargingStart == true && robotMove.FireJoystick && EnergyAmount > 0)
        {
            if (ChargingStart == true)
                Charging += Time.deltaTime;

            if (ChargeEnd == false)
            {
                if (ChargeTime > 0)
                    ChargeTime -= Time.deltaTime;
                if (ChargeTime <= 0)
                {
                    ChargeTime = 0.1f;
                    FireShake += Time.deltaTime * 150f;
                    FireEnergy += EnergyNeeds;
                    FireDamage += Damage;
                }
            }

            if (SoundChargeTime == 0)
            {
                SoundChargeTime += Time.deltaTime;
                StartCoroutine(Charge());
            }
        }
        else
        {
            APCUI.GetComponent<Animator>().SetBool("Charge time complete, APC", false);
            StartCoroutine(Fire()); //�߻�
            Charging = 0;
            ChargeEnd = false;
        }

        if (isGunActive && ChargingStart == true && robotMove.FireJoystick && EnergyAmount < EnergyNeed)
        {
            if (EnergyNeedTime == 0)
            {
                Debug.Log("�������� �����մϴ�.");
                SoundManager.instance.SFXPlay("Sound", EnergyLow);
            }

            EnergyNeedTime += Time.deltaTime;

            if (EnergyNeedTime > 0.5f)
                EnergyNeedTime = 0;
        }
        else if (!robotMove.FireJoystick)
            EnergyNeedTime = 0;
    }

    //��¡ �ִϸ��̼�
    IEnumerator Charge()
    {
        if (SoundChargeTimeTwo == 0)
        {
            SoundChargeTimeTwo += Time.deltaTime;
            APCUI.GetComponent<Animator>().SetFloat("Charge time, APC", 1f / 2f);
            animator.SetBool("Charge armaina, MBCA-79", true);
            SoundManager.instance.SFXPlay9("Sound", APCCharging);
            yield return new WaitForSeconds(2);
            SoundManager.instance.SFXPlay9("Sound", APCChargingEnd);
            APCUI.GetComponent<Animator>().SetBool("Charge time complete, APC", true);
            animator.SetBool("Charge armaina, MBCA-79", false);
            ChargeEnd = true;
        }
    }
    
    //APC �߻�
    public IEnumerator Fire()
    {
        if (Charging > 0.01f && Charging >= 0)
        {
            Shake.Instance.ShakeCamera(FireShake + 7.5f, 0.25f);
            FireShake = 0;
            ChargeTime = 0;
            ChargingStart = false;
            EnergyAmount -= FireEnergy + EnergyNeed;
            if (EnergyAmount <= 0)
            {
                EnergyAmount = 0;
                ViewEnergy = EnergyAmount;
                AmmoAmountBar = EnergyAmount;
                FillBar.fillAmount = AmmoAmountBar / StartAmmo;
            }
            else
            {
                ViewEnergy = EnergyAmount;
                AmmoAmountBar = EnergyAmount;
                FillBar.fillAmount = AmmoAmountBar / StartAmmo;
            }
            SoundManager.instance.SFXPlay9("Sound", APCFire);

            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire, Vehicle HUD", true);
            APCUI.GetComponent<Animator>().SetFloat("Charge time, APC", 0);
            APCUI.GetComponent<Animator>().SetBool("Cool time start, APC", true);
            APCUI.GetComponent<Animator>().SetFloat("Cool time, APC", 1f / 2f);
            animator.SetBool("Charge armaina, MBCA-79", false);
            animator.SetBool("Fire Armania, MBCA-79", true);
            APCAmmo = VehicleObjectsManager.VehicleLoader("APCAmmo");
            APCAmmo.transform.position = APCAmmoPos.position;
            APCAmmo.transform.rotation = APCAmmoPos.rotation;
            APCAmmo.GetComponent<PlazmaBullet>().SetDamage(FireDamage + FirstDamage); //�Ѿ˿��� ������ ����
            GameObject PlasmaFire = Instantiate(PlazmaFire, APCAmmoPos.transform.position, APCAmmoPos.transform.rotation);
            Destroy(PlasmaFire, 3);
            yield return new WaitForSeconds(0.916f);
            animator.SetBool("Fire Armania, MBCA-79", false);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire, Vehicle HUD", false);

            yield return new WaitForSeconds(1.083f);
            APCUI.GetComponent<Animator>().SetBool("Cool time start, APC", false);
            APCUI.GetComponent<Animator>().SetFloat("Cool time, APC", 0);
            APCUI.GetComponent<Animator>().SetBool("Cool time action end, APC", true);
            Invoke("CoolTimeEnd", 0.5f);

            SoundChargeTime = 0;
            SoundChargeTimeTwo = 0;
            Charging = 0;
            FireEnergy = 0;
            FireDamage = 0;
            ChargingStart = true;
        }
    }

    void CoolTimeEnd()
    {
        APCUI.GetComponent<Animator>().SetBool("Cool time action end, APC", false);
    }
}