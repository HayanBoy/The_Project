using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FBWSFire : MonoBehaviour
{
    Animator animator;
    public VehicleObjectsManager vehicleObjectsManager;
    AutoTurretSystem autoTurretSystem;

    private Shake shake;

    public Text ammoText;
    public Text ammoText2;
    public Image FillBar;
    public GameObject AnimationUIVehicleHUD;

    private int StartAmmo; //첫 시작할 때 주어지는 탄약량. 총 탄약량 계산에 사용됨
    public int AmmoAmount; //탄약량
    public int FillAmmoAmount; //탄약 회복량
    public int ViewAmmo;
    private int GunSmokeOn = 3;
    public int Damage;

    public float AmmoSpeed;
    public float FireRate;
    public float timeStamp;
    private float rollingTime = 0;
    private float SoundTime;
    private float SoundChargeTime;
    private float AmmoZeroTime;
    private float AmmoAmountBar;

    public bool FireStart = false;
    private bool TurnRolling = false;
    public bool FBWSUse = false;

    GameObject vehicleLanding;
    GameObject FBWSFiring;
    GameObject FBWSShell;
    public GameObject FBWSSmoke;
    public Transform FBWSAmmoPos;
    public Transform FBWSShellPos;
    public Transform SmokePos;

    public AudioClip FBWSFIreStartSound;
    public AudioClip FBWSFIreEndSound;

    public void FillAmmo()
    {
        AmmoAmount = FillAmmoAmount;
        ViewAmmo = FillAmmoAmount;
    }

    public void ReloadVehicle()
    {
        AmmoZeroTime = 0;
        AmmoAmount = StartAmmo;
        ViewAmmo = AmmoAmount;
        AmmoAmountBar = AmmoAmount;
        timeStamp += 2;
    }

    private void UpdateBulletText()
    {
        if (StartAmmo >= 1000)
        {
            if (ViewAmmo >= 1000)
            {
                ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color><color=#8DFFF3>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color><color=#50888C>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < 1000 && ViewAmmo >= StartAmmo * 0.1f)
            {
                ammoText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color><color=#8DFFF3>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color><color=#50888C>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < StartAmmo * 0.1f && ViewAmmo >= 100)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < StartAmmo * 0.1f && ViewAmmo < 100 && ViewAmmo >= 10)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < 10 && ViewAmmo >= 1)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color><color=#FF1A15>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color><color=#7E0D0B>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }

            else if (ViewAmmo <= 0)
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
            if (ViewAmmo >= 100)
            {
                ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < 100 && ViewAmmo >= StartAmmo * 0.1f)
            {
                ammoText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < StartAmmo * 0.1f && ViewAmmo >= 10)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < 10 && ViewAmmo >= 1)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color>", ViewAmmo / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            }

            else if (ViewAmmo <= 0)
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
        while (ViewAmmo == 0)
        {
            ammoText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color><color=#FF1A15>{2}</color><color=#FF1A15>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color><color=#7E0D0B>{2}</color><color=#7E0D0B>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#8C1411>{2}</color><color=#8C1411>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#380302>{2}</color><color=#380302>{3}</color>", ViewAmmo / 1000, ViewAmmo % 1000 / 100, ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ViewAmmo != 0)
                break;
        }
    }

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

    void Start()
    {
        if (FBWSUse == true)
            gameObject.transform.parent.parent.parent.GetComponent<VehicleLanding>().FBWSOnline = true;
        animator = GetComponent<Animator>();
        autoTurretSystem = FindObjectOfType<AutoTurretSystem>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();

        FillAmmoAmount = AmmoAmount;
        StartAmmo = AmmoAmount;
        ViewAmmo = AmmoAmount;
        AmmoAmountBar = AmmoAmount;

        Damage = UpgradeDataSystem.instance.MBCA79IronHurricaneFBWSIrisDamage;
    }

    void Update()
    {
        if (timeStamp <= FireRate + 0.1f)
            timeStamp += Time.deltaTime;

        UpdateBulletText();
        FiringStopAnimation();

        if (FireStart && AmmoAmount > 0)
            Fire();
        else if (!FireStart && GunSmokeOn == 2)
            Smoke();
        else
        {
            TurnRolling = false;
        }

        if (FireStart == true && TurnRolling == true)
        {
            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                SoundManager.instance.SFXPlay18("Sound", FBWSFIreStartSound);
            }
        }
        if (FireStart == false && TurnRolling == false && GunSmokeOn == 0)
        {
            if (SoundChargeTime == 0)
            {
                SoundChargeTime += Time.deltaTime;
                SoundManager.instance.SFXPlay18("Sound", FBWSFIreEndSound);
            }
        }

        if (AmmoAmount <= 0)
            autoTurretSystem.GetComponent<AutoTurretSystem>().NoAmmo = true;
    }

    void Fire()
    {
        GunSmokeOn = 2;
        TurnRolling = true;

        if (TurnRolling == true)
        {
            rollingTime += Time.deltaTime;

            if (rollingTime > 1f)
                rollingTime = 1f;

            if (rollingTime <= 0)
            {
                animator.SetFloat("Ready FBWS, MBCA-79", 0);
            }

            else if (rollingTime > 0 && rollingTime < 1f)
            {
                animator.SetFloat("Ready FBWS, MBCA-79", animator.GetFloat("Ready FBWS, MBCA-79") + 0.01f);
            }
            else if (rollingTime >= 1f)
            {
                animator.SetBool("Start FBWS, MBCA-79", false);
                animator.SetBool("Fire FBWS, MBCA-79", true);
                AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire AC, Vehicle HUD", true);

                if (rollingTime > 1f)
                    rollingTime = 1f;

                if (timeStamp >= FireRate)
                {
                    timeStamp = 0;
                    Shake.Instance.ShakeCamera(3, 0.1f);

                    FBWSFiring = vehicleObjectsManager.VehicleLoader("FBWSAmmo");
                    FBWSFiring.transform.position = FBWSAmmoPos.transform.position;
                    FBWSFiring.transform.rotation = FBWSAmmoPos.transform.rotation;
                    FBWSFiring.GetComponent<Rigidbody2D>().AddForce(autoTurretSystem.direction.normalized * AmmoSpeed * Time.deltaTime);
                    FBWSFiring.GetComponent<DrillBullet>().SetDamage(Damage); //총알에다 데미지 전달

                    DrillBullet DrillBullet = FBWSFiring.GetComponent<DrillBullet>();// AmmoMovement 스크립트 오브젝트 매니저 초기화, 이거 안해주면 아무것도 못함 

                    DrillBullet.isHit = false; // 피격방지 
                    DrillBullet.vehicleObjectsManager = vehicleObjectsManager;

                    EjectShell();
                    AmmoAmount--;
                    ViewAmmo = AmmoAmount;
                    AmmoAmountBar = AmmoAmount;
                    FillBar.fillAmount = AmmoAmountBar / StartAmmo;
                }
            }
        }
    }

    void Smoke()
    {
        TurnRolling = false;
        GunSmokeOn = 1;

        if (GunSmokeOn == 1 && rollingTime >= 0.5f)
        {
            GunSmokeOn = 0;

            GameObject Smoke = Instantiate(FBWSSmoke, SmokePos.position, SmokePos.rotation);
            Destroy(Smoke, 4);
            FBWSSmoke.SetActive(true);
            SoundTime = 0;
            SoundChargeTime = 0;
        }
    }

    void FiringStopAnimation()
    {
        if (rollingTime >= 0.5f && rollingTime <= 1f)
        {
            rollingTime -= Time.deltaTime * 0.25f;
            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire AC, Vehicle HUD", false);
            animator.SetBool("Fire FBWS, MBCA-79", false);
            animator.SetBool("Start FBWS, MBCA-79", true);
            animator.SetFloat("Ready FBWS, MBCA-79", 1f);
        }
        else if (rollingTime > 0.15f && rollingTime < 0.5f)
        {
            rollingTime -= Time.deltaTime * 0.15f;
            animator.SetBool("Start FBWS, MBCA-79", false);
            animator.SetFloat("Ready FBWS, MBCA-79", animator.GetFloat("Ready FBWS, MBCA-79") - 0.01f);

            if (animator.GetFloat("Ready FBWS, MBCA-79") < 0)
                animator.SetFloat("Ready FBWS, MBCA-79", 0);
        }
        else if (rollingTime > 0 && rollingTime < 0.15f)
        {
            rollingTime -= Time.deltaTime * 0.05f;
            animator.SetFloat("Ready FBWS, MBCA-79", animator.GetFloat("Ready FBWS, MBCA-79") - 0.001f);

            if (animator.GetFloat("Ready FBWS, MBCA-79") < 0)
                animator.SetFloat("Ready FBWS, MBCA-79", 0);
        }
        else if (rollingTime <= 0)
        {
            animator.SetFloat("Ready FBWS, MBCA-79", 0);
        }
    }

    //탄피 배출
    public void EjectShell()
    {
        FBWSShell = vehicleObjectsManager.VehicleLoader("FBWSShell");
        FBWSShell.transform.position = FBWSShellPos.transform.position;
        FBWSShell.transform.rotation = FBWSShellPos.transform.rotation;
        ShellCase_SW06 ShellCase_SW06 = FBWSShell.GetComponent<ShellCase_SW06>();
        ShellCase_SW06.Pos = FBWSShell.transform.position.y;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        FBWSShell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
        FBWSShell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
    }
}