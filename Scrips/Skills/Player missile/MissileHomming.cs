using System.Collections;
using UnityEngine;

public class MissileHomming : MonoBehaviour
{
    ScoreManager scoreManager;
    public ObjectManager objectManager;
    public GameObject AnimationUISubWeapon;

    GameObject OSEHMissile;
    public Transform OSEHMissilePos;
    public Transform MissileSpawn2;
    public GameObject MissileFireEffect;
    public GameObject ZeroAutoTarget;

    public int Damage;

    public float HomingMissileCool; // 유도 미사일 쿨타임 
    public float HomingMissileTime;
    public float FireRate;

    private int MissileOn = 0;
    public int HomingMissileCnt; // 유도 미사일 카운트
    public int MissileAmount; //미사일 발사 수량

    public bool isHomingMissile;
    public bool VehicleActive;
    private bool Click;

    public AudioClip RockOn;
    public AudioClip MissileLaunch;
    public AudioClip Beep1;

    public void HomingClick()
    {
        if (HomingMissileCnt > 0)
            isHomingMissile = true;
    }

    public void HomingUp()
    {
        if (Click == true)
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, Sub weapon fire", false);
        Click = false;
    }

    public void HomingDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, Sub weapon fire", true);
    }

    public void HomingEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, Sub weapon fire", true);
        }
    }

    public void HomingExit()
    {
        if (Click == true)
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Click, Sub weapon fire", false);
    }

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        Damage = UpgradeDataSystem.instance.OSEH302WidowHireDamage;
    }

    void Update()
    {
        RobotHomingMissile(); //유도미사일

        if (VehicleActive == false)
            HomingMissile_Cool();

        if (scoreManager.AllCnt == 0 && VehicleActive == false)
            ZeroAutoTarget.SetActive(true);
        else if (scoreManager.AllCnt >= 1 && VehicleActive == false)
            ZeroAutoTarget.SetActive(false);
    }

    //미사일 생성
    void CreateHomingMissile()
    {
        SoundManager.instance.SFXPlay6("Sound", MissileLaunch);
        OSEHMissile = objectManager.Loader("OSEHMissile");
        OSEHMissile.transform.Find("OSEH-302 Widow hire").GetComponent<MissileRoot>().SetDamage(Damage);
        OSEHMissile.transform.position = OSEHMissilePos.transform.position;
        OSEHMissile.transform.rotation = OSEHMissilePos.transform.rotation;
        GameObject MissileFire = Instantiate(MissileFireEffect, MissileSpawn2.position, Quaternion.identity);
        Destroy(MissileFire, 5);
    }

    IEnumerator MissileFireAnimation()
    {
        GetComponent<Animator>().SetBool("OSEH-302 Widow hire fire", true);
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < MissileAmount; i++)
        {
            CreateHomingMissile();
            yield return new WaitForSeconds(FireRate);
        }
        yield return new WaitForSeconds(0.916f);
        GetComponent<Animator>().SetBool("OSEH-302 Widow hire fire", false);
    }

    //미사일 추적
    void RobotHomingMissile()
    {
        if (isHomingMissile && MissileOn == 0 && HomingMissileCnt > 0 || Input.GetKeyDown(KeyCode.Q))
        {
            HomingMissileCnt--;
            isHomingMissile = false;
            MissileOn = 1;
            SoundManager.instance.SFXPlay18("Sound", RockOn);
            StartCoroutine(MissileFireAnimation());
        }
    }

    //유도미사일 쿨
    void HomingMissile_Cool()
    {
        if (HomingMissileCnt == 0)
        {
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time start, Sub weapon fire", true);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time running, Sub weapon fire", true);
            AnimationUISubWeapon.GetComponent<Animator>().SetFloat("Cool time, Sub weapon fire", 1 / HomingMissileCool);
            HomingMissileTime += Time.deltaTime;
        }

        if (HomingMissileTime > HomingMissileCool)
        {
            HomingMissileTime = 0;
            MissileOn = 0;
            HomingMissileCnt++;
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time end, Sub weapon fire", true);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time cycle count, Sub weapon fire", true);
            Invoke("AfterEndCycle", 0.5f);
            Invoke("ViewCountComplete", 0.5f);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time start, Sub weapon fire", false);
            AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time running, Sub weapon fire", false);
            AnimationUISubWeapon.GetComponent<Animator>().SetFloat("Cool time, Sub weapon fire", 0);
        }
    }

    void AfterEndCycle()
    {
        AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time end, Sub weapon fire", false);
    }

    void ViewCountComplete()
    {
        AnimationUISubWeapon.GetComponent<Animator>().SetBool("Cool time cycle count, Sub weapon fire", false);
    }
}
