using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HTACController : MonoBehaviour
{
    VehicleObjectsManager VehicleObjectsManager;
    Animator animator;

    private Shake shake;

    public GameObject HTACUI;
    public Text ammoText;
    public Text ammoText2;
    public Image FillBar;

    public int ViewAmmo; //인게임 탄약 소지 표시기
    private int ammo = 0;
    private int GunSmokeOn = 0; //처음 연기 발생 방지, 이후 사격마다 연기 발생 유도
    public int Damage; //총알당 데미지
    public int AmmoAmount; //탄약량
    public int FillAmmoAmount; //탄약 회복량
    private int StartAmmo; //첫 시작할 때 주어지는 탄약량. 총 탄약량 계산에 사용됨

    public float FireRate; //초당 발사수
    private float timeStamp = 0.0f;
    private float StopHTAC;
    private float AmmoZeroTime;
    private float AmmoAmountBar;

    GameObject HTACAmmo;
    GameObject HTACAmmoFire;
    GameObject HTACAmmoSmoke;
    GameObject HTACShell;
    public GameObject AnimationUIVehicleHUD;

    public Transform ammoPos; //총알 생성 좌표
    public Transform ejectPos; //탄피 배출 좌표

    public bool isGun;
    public bool isGunActive;
    public bool HTACUse;

    public AudioClip HTACFireSound;

    public void FillAmmo()
    {
        AmmoAmount = FillAmmoAmount;
        ViewAmmo = FillAmmoAmount;
    }

    public void GunDown()
    {
        isGun = true;
    }

    public void GunUp()
    {
        isGun = false;
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
        if (StartAmmo >= 100)
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
                    StartCoroutine(ZeroAmmoOverUI());
                }
            }
        }
        else if (StartAmmo < 100)
        {
            if (ViewAmmo < 100 && ViewAmmo >= 10)
            {
                ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
            }
            else if (ViewAmmo < 10 && ViewAmmo >= 1)
            {
                ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#FF1A15>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
                ammoText2.text = string.Format("<color=#380302>{0}</color><color=#7E0D0B>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
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

    IEnumerator ZeroAmmoUI()
    {
        while (ViewAmmo == 0)
        {
            ammoText.text = string.Format("<color=#FF1A15>{0}</color><color=#FF1A15>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#7E0D0B>{0}</color><color=#7E0D0B>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
            ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color>", ViewAmmo % 100 / 10, ViewAmmo % 10);
            if (ViewAmmo != 0)
                break;
            yield return new WaitForSeconds(0.2f);
            if (ViewAmmo != 0)
                break;
        }
    }

    void Start()
    {
        if (HTACUse == true)
            GetComponent<VehicleLanding>().HTACOnline = true;
        animator = GetComponent<Animator>();
        VehicleObjectsManager = FindObjectOfType<VehicleObjectsManager>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();

        FillAmmoAmount = AmmoAmount;
        StartAmmo = AmmoAmount;
        ViewAmmo = AmmoAmount;
        AmmoAmountBar = AmmoAmount;
        timeStamp += 2;

        Damage = UpgradeDataSystem.instance.MBCA79IronHurricaneHTACDamage;
    }

    void Update()
    {
        if (timeStamp < FireRate)
            timeStamp += Time.deltaTime;
        if (timeStamp > FireRate)
        {
            timeStamp = FireRate;
            HTACUI.GetComponent<Animator>().SetBool("Cool time start, HTAC", false);
            HTACUI.GetComponent<Animator>().SetFloat("Cool time, HTAC", 0);
            HTACUI.GetComponent<Animator>().SetBool("Cool time action end, HTAC", true);
            Invoke("CoolTimeEnd", 0.5f);
        }

        UpdateBulletText();
        Fire();

        if (animator.GetBool("Fire HTAC, MBCA-79") == true)
            StopHTAC += Time.deltaTime;
    }

    void CoolTimeEnd()
    {
        HTACUI.GetComponent<Animator>().SetBool("Cool time action end, HTAC", false);
    }

    public void Fire()
    {
        if (isGun && isGunActive && AmmoAmount > 0)
        {
            if (timeStamp >= FireRate)
            {
                Shake.Instance.ShakeCamera(7.5f, 0.25f);
                timeStamp = 0;
                AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire, Vehicle HUD", true);
                HTACUI.GetComponent<Animator>().SetBool("Cool time start, HTAC", true);
                HTACUI.GetComponent<Animator>().SetFloat("Cool time, HTAC", 1 / FireRate);
                SoundManager.instance.SFXPlay12("Sound", HTACFireSound);
                GunSmokeOn = 1;
                if (timeStamp == 0)
                    animator.SetBool("Fire HTAC, MBCA-79", true);

                HTACAmmoFire = VehicleObjectsManager.VehicleLoader("HTACAmmoFire");
                HTACAmmoSmoke = VehicleObjectsManager.VehicleLoader("HTACAmmoSmoke");
                HTACAmmo = VehicleObjectsManager.VehicleLoader("HTACAmmo");

                HTACAmmoFire.transform.position = ammoPos.position;
                HTACAmmoFire.transform.rotation = ammoPos.rotation;
                HTACAmmoSmoke.transform.position = ammoPos.position;
                HTACAmmoSmoke.transform.rotation = ammoPos.rotation;
                HTACAmmo.transform.position = ammoPos.position;
                HTACAmmo.transform.rotation = ammoPos.rotation;
                HTACAmmo.GetComponent<NomalBullet>().SetDamage(Damage); //총알에다 데미지 전달

                EjectShell(); //탄피 방출
                AmmoAmount--;
                ViewAmmo = AmmoAmount;
                AmmoAmountBar = AmmoAmount;
                FillBar.fillAmount = AmmoAmountBar / StartAmmo;
            }
        }
        if (StopHTAC >= 0.5f)
        {
            animator.SetBool("Fire HTAC, MBCA-79", false);
            AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Fire, Vehicle HUD", false);
            StopHTAC = 0;
        }
    }

    //탄피 배출
    public void EjectShell()
    {
        HTACShell = VehicleObjectsManager.VehicleLoader("HTACShell");
        HTACShell.transform.position = ejectPos.transform.position;
        HTACShell.transform.rotation = ejectPos.transform.rotation;
        ShellCase_Robot ShellCase_Robot = HTACShell.GetComponent<ShellCase_Robot>();
        ShellCase_Robot.Pos = HTACShell.transform.position.y;
    }
}