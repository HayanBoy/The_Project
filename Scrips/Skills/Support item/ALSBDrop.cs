using System.Collections;
using UnityEngine;

public class ALSBDrop : MonoBehaviour
{
    Animator animator;

    public float DropSpeed;
    public int EatItem;
    public int MissionOption;
    public int subGunFrontOption;
    public int subGunBackOption;

    bool SlowDown = false;
    bool Stop = false;

    public Transform DropPos;

    public AudioClip LandingRokectBoom1;
    public AudioClip LandingRokect1;
    public AudioClip Explosion;

    float SoundBar1;
    float SoundBar2;

    public void ItemEat(int num)
    {
        EatItem = num;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        if (WeaponUnlockManager.instance.ChangeWeaponCountUnlock > 0)
        {
            if (MissionOption > 0 && MissionOption < 5000 || subGunFrontOption == 1 || subGunBackOption == 1)
                StartCoroutine(DroppingChangeEnergy());
        }
        if (MissionOption == 1)
            StartCoroutine(DroppingDT37Ammo());
        else if (MissionOption == 1000)
            StartCoroutine(DroppingDS65Ammo());
        else if (MissionOption == 2000)
            StartCoroutine(DroppingDP9007Ammo());
        else if (subGunFrontOption == 1 || subGunBackOption == 1)
            StartCoroutine(DroppingCGD27Ammo());
        else if (MissionOption == 5000)
            StartCoroutine(DroppingM3078());
        else if (MissionOption == 5001)
            StartCoroutine(DroppingASC365());
    }

    IEnumerator DroppingChangeEnergy()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Ammo/Change weapon energy").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo2 effect").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo2 effect/Particle").GetComponent<DisapearEffect1>().Type = 4999;
        transform.Find("Ammo/Ammo2 effect/Spark1").GetComponent<DisapearEffect1>().Type = 4999;
        transform.Find("Ammo/Ammo2 effect/Line light").GetComponent<DisapearEffect2>().Type = 4999;
        transform.Find("Ammo/Ammo2 effect/Glow").GetComponent<DisapearEffect2>().Type = 4999;
    }

    IEnumerator DroppingDT37Ammo()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Ammo/DT-37 ammo").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect/Particle").GetComponent<DisapearEffect1>().Type = 1;
        transform.Find("Ammo/Ammo1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 1;
        transform.Find("Ammo/Ammo1 effect/Line light").GetComponent<DisapearEffect2>().Type = 1;
        transform.Find("Ammo/Ammo1 effect/Glow").GetComponent<DisapearEffect2>().Type = 1;
    }

    IEnumerator DroppingDS65Ammo()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Ammo/DS-65 ammo").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect/Particle").GetComponent<DisapearEffect1>().Type = 1000;
        transform.Find("Ammo/Ammo1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 1000;
        transform.Find("Ammo/Ammo1 effect/Line light").GetComponent<DisapearEffect2>().Type = 1000;
        transform.Find("Ammo/Ammo1 effect/Glow").GetComponent<DisapearEffect2>().Type = 1000;
    }

    IEnumerator DroppingDP9007Ammo()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Ammo/DP-9007 ammo").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect/Particle").GetComponent<DisapearEffect1>().Type = 2000;
        transform.Find("Ammo/Ammo1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 2000;
        transform.Find("Ammo/Ammo1 effect/Line light").GetComponent<DisapearEffect2>().Type = 2000;
        transform.Find("Ammo/Ammo1 effect/Glow").GetComponent<DisapearEffect2>().Type = 2000;
    }

    IEnumerator DroppingCGD27Ammo()
    {
        yield return new WaitForSeconds(3.83f);
        if (subGunFrontOption == 1)
            transform.Find("Ammo/subGun ammo/CGD-27(front) ammo").gameObject.SetActive(true);
        if (subGunBackOption == 1)
            transform.Find("Ammo/subGun ammo/CGD-27(back) ammo").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect").gameObject.SetActive(true);
        transform.Find("Ammo/Ammo1 effect/Particle").GetComponent<DisapearEffect1>().Type = 3000; //다른 기관단총 모델에도 옆의 4개 전달 Type 변수값을 동일하게 적용
        transform.Find("Ammo/Ammo1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 3000;
        transform.Find("Ammo/Ammo1 effect/Line light").GetComponent<DisapearEffect2>().Type = 3000;
        transform.Find("Ammo/Ammo1 effect/Glow").GetComponent<DisapearEffect2>().Type = 3000;
    }

    IEnumerator DroppingM3078()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Heavy weapon/M3078 mini gun").gameObject.SetActive(true);
        transform.Find("Heavy weapon/Heavy weapon1 effect").gameObject.SetActive(true);
        transform.Find("Heavy weapon/Heavy weapon1 effect/Particle").GetComponent<DisapearEffect1>().Type = 5000;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 5000;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Line light").GetComponent<DisapearEffect2>().Type = 5000;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Glow").GetComponent<DisapearEffect2>().Type = 5000;
    }

    IEnumerator DroppingASC365()
    {
        yield return new WaitForSeconds(3.83f);
        transform.Find("Heavy weapon/ASC 365").gameObject.SetActive(true);
        transform.Find("Heavy weapon/Heavy weapon1 effect").gameObject.SetActive(true);
        transform.Find("Heavy weapon/Heavy weapon1 effect/Particle").GetComponent<DisapearEffect1>().Type = 5001;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Spark1").GetComponent<DisapearEffect1>().Type = 5001;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Line light").GetComponent<DisapearEffect2>().Type = 5001;
        transform.Find("Heavy weapon/Heavy weapon1 effect/Glow").GetComponent<DisapearEffect2>().Type = 5001;
    }

    void Update()
    {
        transform.Translate(transform.up * -1 * DropSpeed * Time.deltaTime); //하강
        StartCoroutine(TheFall()); //하강

        if (SlowDown == true)
            DropSpeed -= Time.deltaTime * 15f; //감속
        if (Stop == true)
            DropSpeed = 0; //정지
        if (DropSpeed < 0)
        {
            DropSpeed = 0;
        }
    }

    //하강
    IEnumerator TheFall()
    {
        yield return new WaitForSeconds(1.5f);
        if (SoundBar1 == 0)
        {
            SoundBar1 += Time.deltaTime;
            SoundManager.instance.SFXPlay8("Sound", LandingRokectBoom1);
        }
        yield return new WaitForSeconds(0.5f);
        SlowDown = true;
        if (SoundBar2 == 0)
        {
            SoundBar2 += Time.deltaTime;
            SoundManager.instance.SFXPlay8("Sound", LandingRokect1);
        }

        yield return new WaitForSeconds(0.5f);
        SlowDown = false;
        Stop = true;

        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}