using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    public bool SkyCraneSpawn;
    public bool KaotiJaios4Spawn;
    public bool Kaotijaios4SpearSpawn;
    public bool Kaotijaios4Fleet1389Spawn;
    public bool Kaotijaios4DualgunSpawn;
    public bool Kaotijaios4ArmorSpawn;
    public bool Kaotijaios4ArmorDualgunSpawn;

    public bool TaikaLaiThrotro1Spawn;
    public bool TaikaLaiThrotro1PlasmaSpawn;
    public bool AtroCrossfa390Spawn;

    public bool ZomBieSpawn;

    public bool AsoShiioshareSpawn;

    public static ObjectManager instance = null;

    //몬스터 소환
    public GameObject KaotiJaios4Prefab;
    public GameObject KaotiJaios4SpearPrefab;
    public GameObject KaotiJaios4Fleet1389Prefab;
    public GameObject KaotiJaios4DualgunPrefab;
    public GameObject KaotiJaios4ArmorDualgunPrefab;
    public GameObject KaotiJaios4ArmorPrefab;
    public GameObject TaikaLaiThrotro1Prefab;
    public GameObject TaikaLaiThrotro1PlasmaPrefab;
    public GameObject AtroCrossfa390Prefab;
    public GameObject skyCranePrefab; //스카이 크레인 프리팹
    public GameObject skyCrane2Prefab; //스카이 크레인 프리팹

    public GameObject ZomBiePrefab;

    public GameObject AsoShiiosharePrefab;

    GameObject[] Kaotijaios4;
    GameObject[] Kaotijaios4Spear;
    GameObject[] Kaotijaios4Fleet1389;
    GameObject[] Kaotijaios4Dualgun;
    GameObject[] Kaotijaios4ArmorDualgun;
    GameObject[] Kaotijaios4Armor;
    GameObject[] TaikaLaiThrotro1;
    GameObject[] TaikaLaiThrotro1Plasma;
    GameObject[] AtroCrossfa390;
    GameObject[] skyCrane;
    GameObject[] skyCrane2;

    GameObject[] ZomBie;

    GameObject[] AsoShiioshare;

    GameObject[] PoolMaker;

    public List<GameObject> EnemyList = new List<GameObject>();

    private void Start()
    {
        if (KaotiJaios4Spawn == true)
            Kaotijaios4 = new GameObject[20];
        if (Kaotijaios4SpearSpawn == true)
            Kaotijaios4Spear = new GameObject[20];
        if (Kaotijaios4Fleet1389Spawn == true)
            Kaotijaios4Fleet1389 = new GameObject[20];
        if (Kaotijaios4DualgunSpawn == true)
            Kaotijaios4Dualgun = new GameObject[20];
        if (Kaotijaios4ArmorDualgunSpawn == true)
            Kaotijaios4ArmorDualgun = new GameObject[20];
        if (Kaotijaios4ArmorSpawn == true)
            Kaotijaios4Armor = new GameObject[20];
        if (TaikaLaiThrotro1Spawn == true)
            TaikaLaiThrotro1 = new GameObject[20];
        if (TaikaLaiThrotro1PlasmaSpawn == true)
            TaikaLaiThrotro1Plasma = new GameObject[20];
        if (AtroCrossfa390Spawn == true)
            AtroCrossfa390 = new GameObject[20];
        if (ZomBieSpawn == true)
            ZomBie = new GameObject[30];
        if (AsoShiioshareSpawn == true)
            AsoShiioshare = new GameObject[10];

        if (SkyCraneSpawn == true)
        {
            skyCrane = new GameObject[10];
            skyCrane2 = new GameObject[10];
        }

        Generate();
    }

    void Generate()
    {
        if (TaikaLaiThrotro1Spawn == true)
        {
            for (int index = 0; index < TaikaLaiThrotro1.Length; index++)
            {
                TaikaLaiThrotro1[index] = Instantiate(TaikaLaiThrotro1Prefab);
                TaikaLaiThrotro1[index].SetActive(false);
            }
        }
        if (TaikaLaiThrotro1PlasmaSpawn == true)
        {
            for (int index = 0; index < TaikaLaiThrotro1Plasma.Length; index++)
            {
                TaikaLaiThrotro1Plasma[index] = Instantiate(TaikaLaiThrotro1PlasmaPrefab);
                TaikaLaiThrotro1Plasma[index].SetActive(false);
            }
        }
        if (KaotiJaios4Spawn == true)
        {
            for (int index = 0; index < Kaotijaios4.Length; index++)
            {
                Kaotijaios4[index] = Instantiate(KaotiJaios4Prefab);
                Kaotijaios4[index].SetActive(false);
            }
        }
        if (Kaotijaios4SpearSpawn == true)
        {
            for (int index = 0; index < Kaotijaios4Spear.Length; index++)
            {
                Kaotijaios4Spear[index] = Instantiate(KaotiJaios4SpearPrefab);
                Kaotijaios4Spear[index].SetActive(false);
            }
        }
        if (Kaotijaios4Fleet1389Spawn == true)
        {
            for (int index = 0; index < Kaotijaios4Fleet1389.Length; index++)
            {
                Kaotijaios4Fleet1389[index] = Instantiate(KaotiJaios4Fleet1389Prefab);
                Kaotijaios4Fleet1389[index].SetActive(false);
            }
        }
        if (Kaotijaios4DualgunSpawn == true)
        {
            for (int index = 0; index < Kaotijaios4Dualgun.Length; index++)
            {
                Kaotijaios4Dualgun[index] = Instantiate(KaotiJaios4DualgunPrefab);
                Kaotijaios4Dualgun[index].SetActive(false);
            }
        }
        if (Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < Kaotijaios4ArmorDualgun.Length; index++)
            {
                Kaotijaios4ArmorDualgun[index] = Instantiate(KaotiJaios4ArmorDualgunPrefab);
                Kaotijaios4ArmorDualgun[index].SetActive(false);
            }
        }
        if (Kaotijaios4ArmorSpawn == true)
        {
            for (int index = 0; index < Kaotijaios4Armor.Length; index++)
            {
                Kaotijaios4Armor[index] = Instantiate(KaotiJaios4ArmorPrefab);
                Kaotijaios4Armor[index].SetActive(false);
            }
        }

        if (AtroCrossfa390Spawn == true)
        {
            for (int index = 0; index < AtroCrossfa390.Length; index++)
            {
                AtroCrossfa390[index] = Instantiate(AtroCrossfa390Prefab);
                AtroCrossfa390[index].SetActive(false);
            }
        }

        if (SkyCraneSpawn == true)
        {
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

        if (ZomBieSpawn == true)
        {
            for (int index = 0; index < ZomBie.Length; index++)
            {
                ZomBie[index] = Instantiate(ZomBiePrefab);
                ZomBiePrefab.GetComponent<StartTypeTaitroki>().Type = 0;
                ZomBie[index].SetActive(false);
            }
        }

        if (AsoShiioshareSpawn == true)
        {
            for (int index = 0; index < AsoShiioshare.Length; index++)
            {
                AsoShiioshare[index] = Instantiate(AsoShiiosharePrefab);
                AsoShiioshare[index].SetActive(false);
            }
        }
    }

    public GameObject EnemiesLoader(string type)
    {
        switch (type)
        {
            //칸타크리
            case "Kaotijaios4": //기본
                PoolMaker = Kaotijaios4;
                break;

            case "Kaotijaios4Spear": //닿았을 경우 데미지를 가함
                PoolMaker = Kaotijaios4Spear;
                break;

            case "Kaotijaios4Fleet1389": //강화된 파란색 총알을 씀
                PoolMaker = Kaotijaios4Fleet1389;
                break;

            case "Kaotijaios4Dualgun": //4개의 총알을 씀
                PoolMaker = Kaotijaios4Dualgun;
                break;

            case "Kaotijaios4ArmorDualgun": //아머+4개의 강화된 총알을 쓰는 카오티-자이오스4 엘리트
                PoolMaker = Kaotijaios4ArmorDualgun;
                break;

            case "Kaotijaios4Armor": //방어형
                PoolMaker = Kaotijaios4Armor;
                break;

            case "TaikaLaiThrotro1": //기본, 몹을 소환한다.
                PoolMaker = TaikaLaiThrotro1;
                break;

            case "TaikaLaiThrotro1Plasma": //레일건 대신 플라즈마 포를 장착
                PoolMaker = TaikaLaiThrotro1Plasma;
                break;

            case "AtroCrossfa390":
                PoolMaker = AtroCrossfa390;
                break;

            case "skyCrane":
                PoolMaker = skyCrane;
                break;

            case "skyCrane2":
                PoolMaker = skyCrane2;
                break;


            //좀비 및 타이트로키
            case "ZomBie":
                PoolMaker = ZomBie;
                break;


            //슬로리어스
            case "AsoShiioshare": //아스오 시이오셰어(엘리트 돌격소총)
                PoolMaker = AsoShiioshare;
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
