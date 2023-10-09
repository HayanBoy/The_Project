using System.Collections;
using UnityEngine;

public class RandomSitePlayer : MonoBehaviour
{
    public GameObject Ammo;
    public GameObject HeavyWeapon;
    public GameObject DropBoxPrefab; //지원박스 생성 프리팹
    public Transform DropStartPos;
    public Transform DropEndPos;
    public float fallTime; //강하 생성 시간

    public int DropType;
    public int DropsubGunFrontType;
    public int DropsubGunBackType;

    private void OnEnable()
    {
        StartCoroutine(CraneModel());
    }

    void CreateMark()
    {
        StartCoroutine(AnimationTime());
        Destroy(gameObject, fallTime + 2f);
    }

    IEnumerator AnimationTime()
    {
        GetComponent<Animator>().SetBool("Cycle Active, Drop", true);
        GetComponent<Animator>().SetFloat("Cycle, Drop", 1 / 1.8f);
        GetComponent<Animator>().SetFloat("Start, Drop", 1);
        yield return new WaitForSeconds(fallTime + 1.5f);
        GetComponent<Animator>().SetFloat("Start, Drop", 2);
    }

    IEnumerator CraneModel()
    {
        Invoke("CreateMark", fallTime / 5);
        yield return new WaitForSeconds(fallTime);
        GameObject Drop = Instantiate(DropBoxPrefab, DropStartPos.transform.position, DropStartPos.transform.rotation);
        if (DropType == 1) //DT-37 탄약 보급품
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 1;
            Ammo.SetActive(false);
        }
        else if (DropType == 1000) //DS-65 탄약 보급품
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 1000;
            Ammo.SetActive(false);
        }
        else if (DropType == 2000) //DP-9007 탄약 보급품
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 2000;
            Ammo.SetActive(false);
        }
        if (DropsubGunFrontType == 1) //CGD-27 탄약 보급품
        {
            Drop.GetComponent<ALSBDrop>().subGunFrontOption = 1;
            Ammo.SetActive(false);
        }
        if (DropsubGunBackType == 1) //CGD-27 탄약 보급품
        {
            Drop.GetComponent<ALSBDrop>().subGunBackOption = 1;
            Ammo.SetActive(false);
        }
        else if (DropType == 5000) //M3078
        {
            HeavyWeapon.SetActive(false);
            Drop.GetComponent<ALSBDrop>().MissionOption = 5000;
        }
        else if (DropType == 5001) //M3078
        {
            HeavyWeapon.SetActive(false);
            Drop.GetComponent<ALSBDrop>().MissionOption = 5001;
        }
    }
}