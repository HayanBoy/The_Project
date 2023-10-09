using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour  // ** Sky Crane 최적화 떄문에 만든 스크립트 TalkaLaiThorotro1에 할당해주었음 
{
 
    public GameObject flareAmmoPrefab; //플레어 총알 프리팹
    public Transform flareAmmoPos; //플레어 생성 좌표
    public Transform flareAmmo2Pos; //플레어 생성2 좌표

    public GameObject flareFlamePrefab; //플레어 불꽃생성 좌표
    public Transform flareFlamePos; //플레어 불꽃 좌표

    public GameObject sitePrefab; //랜덤 좌표 프리팹

    GameObject[] flareAmmo;
    GameObject[] flareFlame;
    GameObject[] site;

    public GameObject KaotiJaios4Prefab;
    public GameObject KaotiJaios4SpearPrefab;
    public GameObject KaotiJaios4Fleet1389Prefab;
    public GameObject KaotiJaios4DualgunPrefab;
    public GameObject KaotiJaios4ArmorDualgunPrefab;
    public GameObject KaotiJaios4ArmorPrefab;
    public Transform kaotiJaios4Pos; //Kaoti-Jaios 4 생성 좌표


    public GameObject skyCranePrefab; //스카이 크레인 프리팹
    public GameObject skyCrane2Prefab; //스카이 크레인 프리팹

    GameObject[] Kaotijaios4;
    GameObject[] Kaotijaios4Spear;
    GameObject[] Kaotijaios4Fleet1389;
    GameObject[] Kaotijaios4Dualgun;
    GameObject[] Kaotijaios4ArmorDualgun;
    GameObject[] Kaotijaios4Armor;

    GameObject[] skyCrane; // ** 카오티 4 
    GameObject[] skyCrane2; // ** 카오티 4 


    GameObject[] PoolMaker;

   //GameObject[] Kaotijaios4; // ** 카오티 4 
    


    void Start()
    {
        //flareAmmo = new GameObject[3];
        //flareFlame = new GameObject[3];
        //site = new GameObject[6];

        //Kaotijaios4 = new GameObject[5];
        //Kaotijaios4Spear = new GameObject[5];
        //Kaotijaios4Fleet1389 = new GameObject[5];
        //Kaotijaios4Dualgun = new GameObject[5];
        //Kaotijaios4ArmorDualgun = new GameObject[5];
        //Kaotijaios4Armor = new GameObject[5];
        //skyCrane = new GameObject[6];
        //skyCrane2 = new GameObject[6];

        //Generate();
    }

    void Generate()
    {
        for (int index = 0; index < flareAmmo.Length; index++)
        {
            flareAmmo[index] = Instantiate(flareAmmoPrefab);
            flareAmmo[index].SetActive(false);
        }

        for (int index = 0; index < flareFlame.Length; index++)
        {
            flareFlame[index] = Instantiate(flareFlamePrefab);
            flareFlame[index].SetActive(false);
        }

        for (int index = 0; index < site.Length; index++)
        {
            site[index] = Instantiate(sitePrefab);
            site[index].SetActive(false);
        }

        for (int index = 0; index < Kaotijaios4.Length; index++)
        {
            Kaotijaios4[index] = Instantiate(KaotiJaios4Prefab);
            Kaotijaios4[index].SetActive(false);
        }

        for (int index = 0; index < Kaotijaios4Spear.Length; index++)
        {
            Kaotijaios4Spear[index] = Instantiate(KaotiJaios4SpearPrefab);
            Kaotijaios4Spear[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Fleet1389.Length; index++)
        {
            Kaotijaios4Fleet1389[index] = Instantiate(KaotiJaios4Fleet1389Prefab);
            Kaotijaios4Fleet1389[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Dualgun.Length; index++)
        {
            Kaotijaios4Dualgun[index] = Instantiate(KaotiJaios4DualgunPrefab);
            Kaotijaios4Dualgun[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4ArmorDualgun.Length; index++)
        {
            Kaotijaios4ArmorDualgun[index] = Instantiate(KaotiJaios4ArmorDualgunPrefab);
            Kaotijaios4ArmorDualgun[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Armor.Length; index++)
        {
            Kaotijaios4Armor[index] = Instantiate(KaotiJaios4ArmorPrefab);
            Kaotijaios4Armor[index].SetActive(false);
        }

        for (int index = 0; index < skyCrane.Length; index++)
        {
            skyCrane[index] = Instantiate(skyCranePrefab);
            skyCrane[index].SetActive(false);
        }

        for (int index = 0; index < skyCrane2.Length; index++)
        {
            skyCrane2[index] = Instantiate(skyCrane2Prefab);
            skyCrane2[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch(type)
        {
            case "flareAmmo":
                PoolMaker = flareAmmo;
                break;

            case "flareFlame":
                PoolMaker = flareFlame;
                break;

            case "site":
                PoolMaker = site;
                break;

            case "Kaotijaios4":
                PoolMaker = Kaotijaios4;
                break;

            case "Kaotijaios4Spear":
                PoolMaker = Kaotijaios4Spear;
                break;

            case "Kaotijaios4Fleet1389":
                PoolMaker = Kaotijaios4Fleet1389;
                break;

            case "Kaotijaios4Dualgun":
                PoolMaker = Kaotijaios4Dualgun;
                break;

            case "Kaotijaios4ArmorDualgun":
                PoolMaker = Kaotijaios4ArmorDualgun;
                break;

            case "Kaotijaios4Armor":
                PoolMaker = Kaotijaios4Armor;
                break;

            case "skyCrane":
                PoolMaker = skyCrane;
                break;

            case "skyCrane2":
                PoolMaker = skyCrane2;
                break;
        }

        for (int index = 0; index < PoolMaker.Length; index++)
        {
            if (!PoolMaker[index].activeSelf)
            {
                PoolMaker[index].SetActive(true);
                return PoolMaker[index];
            }
        }

        return null;
    }


}
