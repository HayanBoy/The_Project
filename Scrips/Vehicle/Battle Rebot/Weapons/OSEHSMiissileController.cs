using System.Collections;
using UnityEngine;

public class OSEHSMiissileController : MonoBehaviour
{
    public VehicleObjectsManager vehicleObjectsManager;
    ScoreManager scoreManager;

    public GameObject AnimationUISubWeapon;
    GameObject OSEHSMissile;
    public Transform OSEHSMissilePos;
    public Transform OSEHSMissilePos2;
    public GameObject ZeroAutoTarget;

    public float HomingMissileCool; //���� �̻��� ��Ÿ�� 
    public float HomingMissileTime; //�̻��� ��Ÿ�� ���� �뵵
    public float FireRate;

    public int Damage;

    private int MissileOn = 0;
    private int FirePos;
    public int HomingMissileCnt; //���� �̻��� ī��Ʈ
    public int MissileAmount; //�̻��� �߻� ����
    public int HomingMissileDamage;

    public bool isHoming;
    private bool FireComplete = false;
    public bool VehicleActive; //������ ž������ �� �̵� �� �ൿ ������ ����ġ
    private bool Click;

    public AudioClip OSEHSFireSound;
    public AudioClip Beep1;
    public AudioClip Beep2;

    public void HomingUp()
    {
        if (Click == true)
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, OSEHS", false);
        Click = false;
    }

    public void HomingDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, OSEHS", true);
    }

    public void HomingEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, OSEHS", false);
        }
    }

    public void HomingExit()
    {
        if (Click == true)
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, OSEHS", true);
    }

    public void HomingClick()
    {
        if (HomingMissileCnt > 0)
            isHoming = true;
    }

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        Damage = UpgradeDataSystem.instance.MBCA79IronHurricaneOSEHSDamage;
    }

    void Update()
    {
        OSEHSFire();
        if (VehicleActive == true)
            HomingMissile_Cool();

        if (scoreManager.AllCnt == 0 && VehicleActive == true)
            ZeroAutoTarget.SetActive(true);
        else if (scoreManager.AllCnt >= 1 && VehicleActive == true)
            ZeroAutoTarget.SetActive(false);
    }

    //�̻��� ����
    void CreateMissile()
    {
        SoundManager.instance.SFXPlay10("Sound", OSEHSFireSound);
        OSEHSMissile = vehicleObjectsManager.VehicleLoader("OSEHSMissile");
        OSEHSMissile.transform.Find("OSEH-708 Hell Fire").GetComponent<MissileRoot>().SetDamage(Damage);
        FirePos = Random.Range(0, 2);
        if (FirePos == 0)
        {
            OSEHSMissile.transform.position = OSEHSMissilePos.transform.position;
            OSEHSMissile.transform.rotation = OSEHSMissilePos.transform.rotation;
        }
        else
        {
            OSEHSMissile.transform.position = OSEHSMissilePos2.transform.position;
            OSEHSMissile.transform.rotation = OSEHSMissilePos2.transform.rotation;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            OSEHSMissile.GetComponent<Rigidbody2D>().velocity = Vector3.right * 3;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            OSEHSMissile.GetComponent<Rigidbody2D>().velocity = Vector3.left * 3;
        }
    }

    void OSEHSFire()
    {
        if (isHoming)
        {
            if (HomingMissileCnt > 0 && MissileOn == 0)
            {
                SoundManager.instance.SFXPlay2("Sound", Beep1);
                isHoming = false;
                HomingMissileCnt--;
                MissileOn = 1;
                StartCoroutine(Firemissile());
            }
        }
    }

    IEnumerator Firemissile()
    {
        for(int i = 0; i < MissileAmount; i++)
        {
            CreateMissile();
            yield return new WaitForSeconds(FireRate);
        }
    }

    //�����̻��� ��
    void HomingMissile_Cool()
    {
        if (HomingMissileCnt == 0)
        {
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time start, OSEHS", true);
            AnimationUISubWeapon.GetComponent<Animator>().SetFloat("Cool time, OSEHS", 1 / HomingMissileCool);
            HomingMissileTime += Time.deltaTime;
        }

        if (HomingMissileTime > HomingMissileCool)
        {
            HomingMissileTime = 0;
            MissileOn = 0;
            HomingMissileCnt++;
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time action end, OSEHS", true);
            Invoke("AfterEndCycle", 0.5f);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time start, OSEHS", false);
            AnimationUISubWeapon.GetComponent<Animator>().SetFloat("Cool time, OSEHS", 0);
        }
    }

    void AfterEndCycle()
    {
        AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time action end, OSEHS", false);
    }
}