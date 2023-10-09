using System.Collections;
using UnityEngine;

public class RandomSitePlayer : MonoBehaviour
{
    public GameObject Ammo;
    public GameObject HeavyWeapon;
    public GameObject DropBoxPrefab; //�����ڽ� ���� ������
    public Transform DropStartPos;
    public Transform DropEndPos;
    public float fallTime; //���� ���� �ð�

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
        if (DropType == 1) //DT-37 ź�� ����ǰ
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 1;
            Ammo.SetActive(false);
        }
        else if (DropType == 1000) //DS-65 ź�� ����ǰ
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 1000;
            Ammo.SetActive(false);
        }
        else if (DropType == 2000) //DP-9007 ź�� ����ǰ
        {
            Drop.GetComponent<ALSBDrop>().MissionOption = 2000;
            Ammo.SetActive(false);
        }
        if (DropsubGunFrontType == 1) //CGD-27 ź�� ����ǰ
        {
            Drop.GetComponent<ALSBDrop>().subGunFrontOption = 1;
            Ammo.SetActive(false);
        }
        if (DropsubGunBackType == 1) //CGD-27 ź�� ����ǰ
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