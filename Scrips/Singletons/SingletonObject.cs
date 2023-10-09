using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SingletonObject : MonoBehaviour
{
    public bool KantakriSpawn;
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

    int ShellCursor;
    int ShellCursor1;

    // 싱글톤 
    public static SingletonObject instance = null;

    //칸타크리 강하기
    public GameObject skyCranePrefab; //스카이 크레인 프리팹
    public GameObject skyCrane2Prefab; //스카이 크레인 프리팹

    GameObject[] skyCrane; // ** 카오티 4 
    GameObject[] skyCrane2; // ** 카오티 4 

    public GameObject model1Prefab;
    public GameObject model2Prefab;
    public GameObject enginePrefab;
    public GameObject nozzleRightPrefab;
    public GameObject nozzleLeftPrefab;
    public GameObject part1Prefab;
    public GameObject part2Prefab;
    public GameObject part3Prefab;

    GameObject[] model1;
    GameObject[] model2;
    GameObject[] engine;
    GameObject[] nozzleRight;
    GameObject[] nozzleLeft;
    GameObject[] part1;
    GameObject[] part2;
    GameObject[] part3;

    public GameObject SkyCraneExplosion1Prefab;
    public GameObject SkyCraneExplosion2Prefab;
    GameObject[] Particle;
    GameObject[] Particle2;

    public GameObject KantakriBlackSmallExplosion1Prefab;
    GameObject[] KantakriBlackSmallExplosion1;
    public GameObject KantakriBlueSmallExplosion1Prefab;
    GameObject[] KantakriBlueSmallExplosion1;
    public GameObject KantakriLandingFrontPrefab;
    GameObject[] KantakriLandingFront;
    public GameObject KantakriLandingBackPrefab;
    GameObject[] KantakriLandingBack;

    GameObject[] PoolMaker;
    GameObject[] PoolMaker1;

    //카오티-자이오스4 
    public GameObject KaotiJaios4Prefab;
    public GameObject Kaotijaios4SpearPrefab;
    public GameObject Kaotijaios4Fleet1389Prefab;
    public GameObject Kaotijaios4DualgunPrefab;
    public GameObject Kaotijaios4ArmorPrefab;
    public GameObject Kaotijaios4ArmorDualgunPrefab;

    //강하정 스폰 몬스터
    GameObject[] Kaotijaios4;
    GameObject[] Kaotijaios4Spear;
    GameObject[] Kaotijaios4Fleet1389;
    GameObject[] Kaotijaios4Dualgun;
    GameObject[] Kaotijaios4Armor;
    GameObject[] Kaotijaios4ArmorDualgun;

    public GameObject gunBackPrefab;
    public GameObject gunFrontPrefab;
    public GameObject dualgunBackPrefab;
    public GameObject dualgunFrontPrefab;
    public GameObject gunFrontBoxPrefab;
    public GameObject gunFrontCircleBoxPrefab;
    public GameObject gunFrontJointPrefab;
    public GameObject gunFrontJointArmPrefab;
    public GameObject tireBackPrefab;
    public GameObject tireFrontPrefab;
    public GameObject bodyPrefab;
    public GameObject bodySpearPrefab;
    public GameObject bodyBluePrefab;
    public GameObject bodyArmorPrefab;
    public GameObject kao_part1Prefab;
    public GameObject kao_part2Prefab;
    public GameObject kao_part3Prefab;

    GameObject[] gunBack;
    GameObject[] gunFront;
    GameObject[] dualgunBack;
    GameObject[] dualgunFront;
    GameObject[] gunFrontBox;
    GameObject[] gunFrontCircleBox;
    GameObject[] gunFrontJoint;
    GameObject[] gunFrontJointArm;
    GameObject[] tireBack;
    GameObject[] tireFront;
    GameObject[] body;
    GameObject[] bodySpear;
    GameObject[] bodyBlue;
    GameObject[] bodyArmor;
    GameObject[] kao_part1;
    GameObject[] kao_part2;
    GameObject[] kao_part3;

    //카오티-자이오스4 탄환 
    public GameObject KaotiJaios4AmmoPrefab;
    public GameObject KaotiJaios4Ammo2Prefab;

    public GameObject DualKaotiJaios4AmmoPrefab;
    public GameObject KaotiJaios4Shell1Prefab;
    public GameObject KaotiJaios4Shell2Prefab;

    //아트로-크로스파 390 기관포 탄환
    public GameObject AtroCrossfa390AmmoPrefab;
    public GameObject AtroCrossfa390Ammo2Prefab;
    public GameObject AtroCrossfa390ShellPrefab;

    GameObject[] KaotiJaios4Ammo; //카오티-자이오스 4 총알 및 파편
    GameObject[] KaotiJaios4Ammo2;
    GameObject[] DualKaotiJaios4Ammo;
    GameObject[] KaotiJaios4Shell1; //카오티-자이오스 4 탄피 1
    GameObject[] KaotiJaios4Shell2; //카오티-자이오스 4 탄피 2

    GameObject[] AtroCrossfa390Ammo; //아트로-크로스파 390 탄환
    GameObject[] AtroCrossfa390Ammo2;
    GameObject[] AtroCrossfa390Shell;

    //타이카-라이-쓰로트로1
    public GameObject Taika_backEnginePrefab;
    public GameObject Taika_bodyPrefab;
    public GameObject Taika_body2Prefab;
    public GameObject Taika_frontEnginePrefab;
    public GameObject Taika_railGunPrefab;
    public GameObject KarrgenArite31Prefab;
    public GameObject Taika_part1Prefab;
    public GameObject Taika_part2Prefab;
    public GameObject Taika_part3Prefab;

    GameObject[] Taika_backEngine;
    GameObject[] Taika_body;
    GameObject[] Taika_body2;
    GameObject[] Taika_frontEngine;
    GameObject[] Taika_railGun;
    GameObject[] KarrgenArite31;
    GameObject[] Taika_part1;
    GameObject[] Taika_part2;
    GameObject[] Taika_part3;

    public GameObject flareAmmoPrefab; //플레어 총알 프리팹
    public GameObject flareFlamePrefab; //플레어 불꽃생성 좌표
    public GameObject sitePrefab; //랜덤 좌표 프리팹

    GameObject[] flareAmmo;
    GameObject[] flareFlame;
    GameObject[] site;

    //아트로-크로스파 390
    public GameObject AtroCrossfa390RightLegPrefab;
    public GameObject AtroCrossfa390RightLegTopPrefab;
    public GameObject AtroCrossfa390RightLegdownPrefab;
    public GameObject AtroCrossfa390LeftLegPrefab;
    public GameObject AtroCrossfa390LeftLegTopPrefab;
    public GameObject AtroCrossfa390LeftLegdownPrefab;
    public GameObject AtroCrossfa390ExplosionPrefab;

    GameObject[] AtroCrossfa390RightLeg;
    GameObject[] AtroCrossfa390RightLegTop;
    GameObject[] AtroCrossfa390RightLegdown;
    GameObject[] AtroCrossfa390LeftLeg;
    GameObject[] AtroCrossfa390LeftLegTop;
    GameObject[] AtroCrossfa390LeftLegdown;
    GameObject[] AtroCrossfa390Explosion;

    public List<Transform> KaotiEnemyBody = new List<Transform>(); //카오티자이오스 죽을 때 부품들이 튀는 위치 고정값
    public List<Transform> TaikaPos = new List<Transform>(); // 타이카라이가 죽을 때 부품들이 튀는 위치 고정값
    public List<Transform> KaotiFirePos = new List<Transform>(); //카오티자이오스 죽을 때 부품들이 튀는 위치 고정값
    public List<Transform> AtroCrossfa390 = new List<Transform>(); //아트로-크로스파390 죽을 때 부품들이 튀는 위치 고정값

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //칸타크리 강하정 스폰
        if (TaikaLaiThrotro1Spawn == true)
        {
            flareAmmo = new GameObject[20];
            flareFlame = new GameObject[20];
            site = new GameObject[20];

            Kaotijaios4 = new GameObject[20];
            Kaotijaios4Spear = new GameObject[20];
            Kaotijaios4Fleet1389 = new GameObject[20];
            Kaotijaios4Dualgun = new GameObject[20];
            Kaotijaios4Armor = new GameObject[20];
            Kaotijaios4ArmorDualgun = new GameObject[20];
        }
  
        //칸타크리 전용
        if (KantakriSpawn == true)
        {
            KantakriBlackSmallExplosion1 = new GameObject[30]; //칸타크리 흑 폭발 이펙트
            KantakriBlueSmallExplosion1 = new GameObject[30]; //칸타크리 백 폭발 이펙트
            Taika_part1 = new GameObject[30];
            Taika_part2 = new GameObject[30];
            Taika_part3 = new GameObject[50];
        }

        //칸타크리 강하정
        if (SkyCraneSpawn == true)
        {
            skyCrane = new GameObject[15];
            skyCrane2 = new GameObject[15];
            Particle = new GameObject[15];  // 스카이크레인 폭발 이펙트
            Particle2 = new GameObject[15]; // 스카이크레인 폭발 이펙트
            model1 = new GameObject[15];
            model2 = new GameObject[15];
            engine = new GameObject[15];
            nozzleRight = new GameObject[15];
            nozzleLeft = new GameObject[15];
            part1 = new GameObject[100];
            part2 = new GameObject[100];
            part3 = new GameObject[150];
            KantakriLandingFront = new GameObject[15]; //칸타크리 착륙 먼지 발생 이펙트
            KantakriLandingBack = new GameObject[15];
        }

        //KaotiJaios 4 내부 Object
        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorSpawn == true
            || Kaotijaios4ArmorDualgunSpawn == true)
        {
            gunFrontBox = new GameObject[20];
            gunFrontCircleBox = new GameObject[20];
            gunFrontJoint = new GameObject[20];
            gunFrontJointArm = new GameObject[20];
            tireBack = new GameObject[20];
            tireFront = new GameObject[20];

            kao_part1 = new GameObject[60];
            kao_part2 = new GameObject[80];
            kao_part3 = new GameObject[120];
        }
        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4ArmorSpawn == true)
        {
            gunBack = new GameObject[20];
            gunFront = new GameObject[20];
        }
        if (Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
        {
            dualgunBack = new GameObject[20];
            dualgunFront = new GameObject[20];
        }
        if (KaotiJaios4Spawn == true)
            body = new GameObject[15];
        if (Kaotijaios4SpearSpawn == true)
            bodySpear = new GameObject[15];
        if (Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true)
            bodyBlue = new GameObject[15];
        if (Kaotijaios4ArmorSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
            bodyArmor = new GameObject[15];

        //KaotiJaios4 탄환
        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4ArmorSpawn == true)
        {
            KaotiJaios4Ammo = new GameObject[60];
            KaotiJaios4Ammo2 = new GameObject[60];
        }
        if (Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
            DualKaotiJaios4Ammo = new GameObject[60];
        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorSpawn == true
            || Kaotijaios4ArmorDualgunSpawn == true)
        {
            KaotiJaios4Shell1 = new GameObject[400];
            KaotiJaios4Shell2 = new GameObject[400];
        }

        //AtroCrossfa390 탄환
        if (AtroCrossfa390Spawn == true)
        {
            AtroCrossfa390Ammo = new GameObject[100];
            AtroCrossfa390Ammo2 = new GameObject[100];
            AtroCrossfa390Shell = new GameObject[400];
        }

        //TaikaLai 내부 Object
        if (TaikaLaiThrotro1Spawn == true || TaikaLaiThrotro1PlasmaSpawn == true)
        {
            Taika_backEngine = new GameObject[20];
            Taika_frontEngine = new GameObject[20];
        }
        if (TaikaLaiThrotro1Spawn == true)
        {
            Taika_body = new GameObject[20];
            Taika_railGun = new GameObject[20];
        }
        if (TaikaLaiThrotro1PlasmaSpawn == true)
        {
            Taika_body2 = new GameObject[20];
            KarrgenArite31 = new GameObject[20];
        }

        //AtroCrossfa390 4 내부 Object
        if (AtroCrossfa390Spawn == true)
        {
            AtroCrossfa390RightLeg = new GameObject[10];
            AtroCrossfa390RightLegTop = new GameObject[10];
            AtroCrossfa390RightLegdown = new GameObject[10];
            AtroCrossfa390LeftLeg = new GameObject[10];
            AtroCrossfa390LeftLegTop = new GameObject[10];
            AtroCrossfa390LeftLegdown = new GameObject[10];
            AtroCrossfa390Explosion = new GameObject[10];
        }

        Generate();
    }

    void Generate()
    {
        if (TaikaLaiThrotro1Spawn == true)
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
                Kaotijaios4Spear[index] = Instantiate(Kaotijaios4SpearPrefab);
                Kaotijaios4Spear[index].SetActive(false);
            }
            for (int index = 0; index < Kaotijaios4Fleet1389.Length; index++)
            {
                Kaotijaios4Fleet1389[index] = Instantiate(Kaotijaios4Fleet1389Prefab);
                Kaotijaios4Fleet1389[index].SetActive(false);
            }
            for (int index = 0; index < Kaotijaios4Dualgun.Length; index++)
            {
                Kaotijaios4Dualgun[index] = Instantiate(Kaotijaios4DualgunPrefab);
                Kaotijaios4Dualgun[index].SetActive(false);
            }
            for (int index = 0; index < Kaotijaios4Armor.Length; index++)
            {
                Kaotijaios4Armor[index] = Instantiate(Kaotijaios4ArmorPrefab);
                Kaotijaios4Armor[index].SetActive(false);
            }
            for (int index = 0; index < Kaotijaios4ArmorDualgun.Length; index++)
            {
                Kaotijaios4ArmorDualgun[index] = Instantiate(Kaotijaios4ArmorDualgunPrefab);
                Kaotijaios4ArmorDualgun[index].SetActive(false);
            }
        }

        if (KantakriSpawn == true)
        {
            for (int index = 0; index < KantakriBlackSmallExplosion1.Length; index++)
            {
                KantakriBlackSmallExplosion1[index] = Instantiate(KantakriBlackSmallExplosion1Prefab);
                KantakriBlackSmallExplosion1[index].SetActive(false);
            }
            for (int index = 0; index < KantakriBlueSmallExplosion1.Length; index++)
            {
                KantakriBlueSmallExplosion1[index] = Instantiate(KantakriBlueSmallExplosion1Prefab);
                KantakriBlueSmallExplosion1[index].SetActive(false);
            }
            for (int index = 0; index < Taika_part1.Length; index++)
            {
                Taika_part1[index] = Instantiate(Taika_part1Prefab);
                Taika_part1[index].SetActive(false);
            }
            for (int index = 0; index < Taika_part2.Length; index++)
            {
                Taika_part2[index] = Instantiate(Taika_part2Prefab);
                Taika_part2[index].SetActive(false);
            }
            for (int index = 0; index < Taika_part3.Length; index++)
            {
                Taika_part3[index] = Instantiate(Taika_part3Prefab);
                Taika_part3[index].SetActive(false);
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
            for (int index = 0; index < Particle.Length; index++)
            {
                Particle[index] = Instantiate(SkyCraneExplosion1Prefab);
                Particle[index].SetActive(false);
            }
            for (int index = 0; index < Particle2.Length; index++)
            {
                Particle2[index] = Instantiate(SkyCraneExplosion2Prefab);
                Particle2[index].SetActive(false);
            }
            for (int index = 0; index < model1.Length; index++)
            {
                model1[index] = Instantiate(model1Prefab);
                model1[index].SetActive(false);
            }
            for (int index = 0; index < model2.Length; index++)
            {
                model2[index] = Instantiate(model2Prefab);
                model2[index].SetActive(false);
            }
            for (int index = 0; index < engine.Length; index++)
            {
                engine[index] = Instantiate(enginePrefab);
                engine[index].SetActive(false);
            }
            for (int index = 0; index < nozzleRight.Length; index++)
            {
                nozzleRight[index] = Instantiate(nozzleRightPrefab);
                nozzleRight[index].SetActive(false);
            }
            for (int index = 0; index < nozzleLeft.Length; index++)
            {
                nozzleLeft[index] = Instantiate(nozzleLeftPrefab);
                nozzleLeft[index].SetActive(false);
            }
            for (int index = 0; index < part1.Length; index++)
            {
                part1[index] = Instantiate(part1Prefab);
                part1[index].SetActive(false);
            }
            for (int index = 0; index < part2.Length; index++)
            {
                part2[index] = Instantiate(part2Prefab);
                part2[index].SetActive(false);
            }
            for (int index = 0; index < part3.Length; index++)
            {
                part3[index] = Instantiate(part3Prefab);
                part3[index].SetActive(false);
            }
            for (int index = 0; index < KantakriLandingFront.Length; index++)
            {
                KantakriLandingFront[index] = Instantiate(KantakriLandingFrontPrefab);
                KantakriLandingFront[index].SetActive(false);
            }
            for (int index = 0; index < KantakriLandingBack.Length; index++)
            {
                KantakriLandingBack[index] = Instantiate(KantakriLandingBackPrefab);
                KantakriLandingBack[index].SetActive(false);
            }
        }

        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorSpawn == true
            || Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < gunFrontBox.Length; index++)
            {
                gunFrontBox[index] = Instantiate(gunFrontBoxPrefab);
                gunFrontBox[index].SetActive(false);
            }
            for (int index = 0; index < gunFrontCircleBox.Length; index++)
            {
                gunFrontCircleBox[index] = Instantiate(gunFrontCircleBoxPrefab);
                gunFrontCircleBox[index].SetActive(false);
            }
            for (int index = 0; index < gunFrontJoint.Length; index++)
            {
                gunFrontJoint[index] = Instantiate(gunFrontJointPrefab);
                gunFrontJoint[index].SetActive(false);
            }
            for (int index = 0; index < gunFrontJointArm.Length; index++)
            {
                gunFrontJointArm[index] = Instantiate(gunFrontJointArmPrefab);
                gunFrontJointArm[index].SetActive(false);
            }
            for (int index = 0; index < tireBack.Length; index++)
            {
                tireBack[index] = Instantiate(tireBackPrefab);
                tireBack[index].SetActive(false);
            }
            for (int index = 0; index < tireFront.Length; index++)
            {
                tireFront[index] = Instantiate(tireFrontPrefab);
                tireFront[index].SetActive(false);
            }
            for (int index = 0; index < kao_part1.Length; index++)
            {
                kao_part1[index] = Instantiate(kao_part1Prefab);
                kao_part1[index].SetActive(false);
            }

            for (int index = 0; index < kao_part2.Length; index++)
            {
                kao_part2[index] = Instantiate(kao_part2Prefab);
                kao_part2[index].SetActive(false);
            }

            for (int index = 0; index < kao_part3.Length; index++)
            {
                kao_part3[index] = Instantiate(kao_part3Prefab);
                kao_part3[index].SetActive(false);
            }
        }

        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4ArmorSpawn == true)
        {
            for (int index = 0; index < gunBack.Length; index++)
            {
                gunBack[index] = Instantiate(gunBackPrefab);
                gunBack[index].SetActive(false);
            }
            for (int index = 0; index < gunFront.Length; index++)
            {
                gunFront[index] = Instantiate(gunFrontPrefab);
                gunFront[index].SetActive(false);
            }
        }

        if (Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < dualgunBack.Length; index++)
            {
                dualgunBack[index] = Instantiate(dualgunBackPrefab);
                dualgunBack[index].SetActive(false);
            }
            for (int index = 0; index < dualgunFront.Length; index++)
            {
                dualgunFront[index] = Instantiate(dualgunFrontPrefab);
                dualgunFront[index].SetActive(false);
            }
        }

        if (KaotiJaios4Spawn == true)
        {
            for (int index = 0; index < body.Length; index++)
            {
                body[index] = Instantiate(bodyPrefab);
                body[index].SetActive(false);
            }
        }

        if (Kaotijaios4SpearSpawn == true)
        {
            for (int index = 0; index < bodySpear.Length; index++)
            {
                bodySpear[index] = Instantiate(bodySpearPrefab);
                bodySpear[index].SetActive(false);
            }
        }

        if (Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true)
        {
            for (int index = 0; index < bodyBlue.Length; index++)
            {
                bodyBlue[index] = Instantiate(bodyBluePrefab);
                bodyBlue[index].SetActive(false);
            }
        }

        if (Kaotijaios4ArmorSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < bodyArmor.Length; index++)
            {
                bodyArmor[index] = Instantiate(bodyArmorPrefab);
                bodyArmor[index].SetActive(false);
            }
        }

        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4ArmorSpawn == true)
        {
            for (int index = 0; index < KaotiJaios4Ammo.Length; index++)
            {
                KaotiJaios4Ammo[index] = Instantiate(KaotiJaios4AmmoPrefab);
                KaotiJaios4Ammo[index].SetActive(false);
            }
            for (int index = 0; index < KaotiJaios4Ammo2.Length; index++)
            {
                KaotiJaios4Ammo2[index] = Instantiate(KaotiJaios4Ammo2Prefab);
                KaotiJaios4Ammo2[index].SetActive(false);
            }
        }

        if (Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < DualKaotiJaios4Ammo.Length; index++)
            {
                DualKaotiJaios4Ammo[index] = Instantiate(DualKaotiJaios4AmmoPrefab);
                DualKaotiJaios4Ammo[index].SetActive(false);
            }
        }

        if (KaotiJaios4Spawn == true || Kaotijaios4SpearSpawn == true || Kaotijaios4Fleet1389Spawn == true || Kaotijaios4DualgunSpawn == true || Kaotijaios4ArmorSpawn == true
            || Kaotijaios4ArmorDualgunSpawn == true)
        {
            for (int index = 0; index < KaotiJaios4Shell1.Length; index++)
            {
                KaotiJaios4Shell1[index] = Instantiate(KaotiJaios4Shell1Prefab);
                KaotiJaios4Shell1[index].SetActive(false);
            }
            for (int index = 0; index < KaotiJaios4Shell2.Length; index++)
            {
                KaotiJaios4Shell2[index] = Instantiate(KaotiJaios4Shell2Prefab);
                KaotiJaios4Shell2[index].SetActive(false);
            }
        }

        if (AtroCrossfa390Spawn == true)
        {
            for (int index = 0; index < AtroCrossfa390Ammo.Length; index++)
            {
                AtroCrossfa390Ammo[index] = Instantiate(AtroCrossfa390AmmoPrefab);
                AtroCrossfa390Ammo[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390Ammo2.Length; index++)
            {
                AtroCrossfa390Ammo2[index] = Instantiate(AtroCrossfa390Ammo2Prefab);
                AtroCrossfa390Ammo2[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390Shell.Length; index++)
            {
                AtroCrossfa390Shell[index] = Instantiate(AtroCrossfa390ShellPrefab);
                AtroCrossfa390Shell[index].SetActive(false);
            }
        }

        //타이카-라이-쓰로트로1
        if (TaikaLaiThrotro1Spawn == true || TaikaLaiThrotro1PlasmaSpawn == true)
        {
            for (int index = 0; index < Taika_backEngine.Length; index++)
            {
                Taika_backEngine[index] = Instantiate(Taika_backEnginePrefab);
                Taika_backEngine[index].SetActive(false);
            }
            for (int index = 0; index < Taika_frontEngine.Length; index++)
            {
                Taika_frontEngine[index] = Instantiate(Taika_frontEnginePrefab);
                Taika_frontEngine[index].SetActive(false);
            }
        }

        if (TaikaLaiThrotro1Spawn == true)
        {
            for (int index = 0; index < Taika_body.Length; index++)
            {
                Taika_body[index] = Instantiate(Taika_bodyPrefab);
                Taika_body[index].SetActive(false);
            }
            for (int index = 0; index < Taika_railGun.Length; index++)
            {
                Taika_railGun[index] = Instantiate(Taika_railGunPrefab);
                Taika_railGun[index].SetActive(false);
            }
        }

        if (TaikaLaiThrotro1PlasmaSpawn == true)
        {
            for (int index = 0; index < Taika_body2.Length; index++)
            {
                Taika_body2[index] = Instantiate(Taika_body2Prefab);
                Taika_body2[index].SetActive(false);
            }
            for (int index = 0; index < KarrgenArite31.Length; index++)
            {
                KarrgenArite31[index] = Instantiate(KarrgenArite31Prefab);
                KarrgenArite31[index].SetActive(false);
            }
        }

        //아트로-크로스파 390
        if (AtroCrossfa390Spawn == true)
        {
            for (int index = 0; index < AtroCrossfa390RightLeg.Length; index++)
            {
                AtroCrossfa390RightLeg[index] = Instantiate(AtroCrossfa390RightLegPrefab);
                AtroCrossfa390RightLeg[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390RightLegTop.Length; index++)
            {
                AtroCrossfa390RightLegTop[index] = Instantiate(AtroCrossfa390RightLegTopPrefab);
                AtroCrossfa390RightLegTop[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390RightLegdown.Length; index++)
            {
                AtroCrossfa390RightLegdown[index] = Instantiate(AtroCrossfa390RightLegdownPrefab);
                AtroCrossfa390RightLegdown[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390LeftLeg.Length; index++)
            {
                AtroCrossfa390LeftLeg[index] = Instantiate(AtroCrossfa390LeftLegPrefab);
                AtroCrossfa390LeftLeg[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390LeftLegTop.Length; index++)
            {
                AtroCrossfa390LeftLegTop[index] = Instantiate(AtroCrossfa390LeftLegTopPrefab);
                AtroCrossfa390LeftLegTop[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390LeftLegdown.Length; index++)
            {
                AtroCrossfa390LeftLegdown[index] = Instantiate(AtroCrossfa390LeftLegdownPrefab);
                AtroCrossfa390LeftLegdown[index].SetActive(false);
            }
            for (int index = 0; index < AtroCrossfa390Explosion.Length; index++)
            {
                AtroCrossfa390Explosion[index] = Instantiate(AtroCrossfa390ExplosionPrefab);
                AtroCrossfa390Explosion[index].SetActive(false);
            }
        }
    }
    public GameObject Loader_ammo() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < KaotiJaios4Ammo.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % KaotiJaios4Ammo.Length;

            if (!KaotiJaios4Ammo[index + ShellCursor].activeSelf)
            {
                KaotiJaios4Ammo[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return KaotiJaios4Ammo[index + ShellCursor];
            }
        }

        return null;
    }

    public GameObject Loader_backammo() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < KaotiJaios4Ammo2.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % KaotiJaios4Ammo2.Length;

            if (!KaotiJaios4Ammo2[index + ShellCursor].activeSelf)
            {
                KaotiJaios4Ammo2[index + ShellCursor].SetActive(true);
                return KaotiJaios4Ammo2[index + ShellCursor];
            }
        }
        return null;
    }

    public GameObject Loader_Dualammo() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < DualKaotiJaios4Ammo.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % DualKaotiJaios4Ammo.Length;

            if (!DualKaotiJaios4Ammo[index + ShellCursor].activeSelf)
            {
                DualKaotiJaios4Ammo[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return DualKaotiJaios4Ammo[index + ShellCursor];
            }
        }

        return null;
    }

    public GameObject Loader_AtroAmmo() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < AtroCrossfa390Ammo.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % AtroCrossfa390Ammo.Length;

            if (!AtroCrossfa390Ammo[index + ShellCursor].activeSelf)
            {
                AtroCrossfa390Ammo[index + ShellCursor].SetActive(true);
                return AtroCrossfa390Ammo[index + ShellCursor];
            }
        }

        return null;
    }
    public GameObject Loader_AtroAmmo2() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < AtroCrossfa390Ammo2.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % AtroCrossfa390Ammo2.Length;

            if (!AtroCrossfa390Ammo2[index + ShellCursor].activeSelf)
            {
                AtroCrossfa390Ammo2[index + ShellCursor].SetActive(true);
                return AtroCrossfa390Ammo2[index + ShellCursor];
            }
        }

        return null;
    }


    public GameObject Loader_FrontShell() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < KaotiJaios4Shell1.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % KaotiJaios4Shell1.Length;

            if (!KaotiJaios4Shell1[index + ShellCursor].activeSelf)
            {
                KaotiJaios4Shell1[index + ShellCursor].SetActive(true);
                return KaotiJaios4Shell1[index + ShellCursor];
            }
        }
        return null;
    }

    public GameObject Loader_BackShell() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < KaotiJaios4Shell2.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % KaotiJaios4Shell2.Length;

            if (!KaotiJaios4Shell2[index + ShellCursor].activeSelf)
            {
                KaotiJaios4Shell2[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return KaotiJaios4Shell2[index + ShellCursor];
            }
        }
        return null;
    }

    public GameObject Loader_AtroShell() // ** 탄피를 활성화 시켰던 프리팹을 중복 사용하지 않게 하는 코드 
    {
        for (int index = 0; index < AtroCrossfa390Shell.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % AtroCrossfa390Shell.Length;

            if (!AtroCrossfa390Shell[index + ShellCursor].activeSelf)
            {
                AtroCrossfa390Shell[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return AtroCrossfa390Shell[index + ShellCursor];
            }
        }

        return null;
    }

    public GameObject Loader(string type)
    {
        switch (type)
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

            case "Kaotijaios4Armor":
                PoolMaker = Kaotijaios4Armor;
                break;

            case "Kaotijaios4ArmorDualgun":
                PoolMaker = Kaotijaios4ArmorDualgun;
                break;

            case "skyCrane":
                PoolMaker = skyCrane;
                break;

            case "skyCrane2":
                PoolMaker = skyCrane2;
                break;


            case "Particle":
                PoolMaker = Particle;
                break;

            case "Particle2":
                PoolMaker = Particle2;
                break;

            case "KantakriBlackSmallExplosion1":
                PoolMaker = KantakriBlackSmallExplosion1;
                break;

            case "KantakriBlueSmallExplosion1":
                PoolMaker = KantakriBlueSmallExplosion1;
                break;

            case "KantakriLandingFront":
                PoolMaker = KantakriLandingFront;
                break;

            case "KantakriLandingBack":
                PoolMaker = KantakriLandingBack;
                break;

            // 스카이크레인  

            case "model1":
                PoolMaker = model1;
                break;

            case "model2":
                PoolMaker = model2;
                break;

            case "engine":
                PoolMaker = engine;
                break;

            case "nozzleRight":
                PoolMaker = nozzleRight;
                break;

            case "nozzleLeft":
                PoolMaker = nozzleLeft;
                break;

            case "part1":
                PoolMaker = part1;
                break;

            case "part2":
                PoolMaker = part2;
                break;

            case "part3":
                PoolMaker = part3;
                break;

            //카오티-자이오스4
            case "gunBack":
                PoolMaker = gunBack;
                break;
            case "gunFront":
                PoolMaker = gunFront;
                break;

            case "dualgunBack":
                PoolMaker = dualgunBack;
                break;
            case "dualgunFront":
                PoolMaker = dualgunFront;
                break;

            case "gunFrontBox":
                PoolMaker = gunFrontBox;
                break;

            case "gunFrontCircleBox":
                PoolMaker = gunFrontCircleBox;
                break;

            case "gunFrontJoint":
                PoolMaker = gunFrontJoint;
                break;

            case "gunFrontJointArm":
                PoolMaker = gunFrontJointArm;
                break;

            case "tireBack":
                PoolMaker = tireBack;
                break;

            case "tireFront":
                PoolMaker = tireFront;
                break;

            case "body":
                PoolMaker = body;
                break;

            case "bodySpear":
                PoolMaker = bodySpear;
                break;

            case "bodyBlue":
                PoolMaker = bodyBlue;
                break;

            case "bodyArmor":
                PoolMaker = bodyArmor;
                break;

            case "kao_part1":
                PoolMaker = kao_part1;
                break;

            case "kao_part2":
                PoolMaker = kao_part2;
                break;

            case "kao_part3":
                PoolMaker = kao_part3;
                break;

            //타이카-라이-쓰로트로1
            case "Taika_backEngine":
                PoolMaker = Taika_backEngine;
                break;
            case "Taika_body":
                PoolMaker = Taika_body;
                break;

            case "Taika_body2":
                PoolMaker = Taika_body2;
                break;

            case "Taika_frontEngine":
                PoolMaker = Taika_frontEngine;
                break;

            case "Taika_railGun":
                PoolMaker = Taika_railGun;
                break;

            case "KarrgenArite31":
                PoolMaker = KarrgenArite31;
                break;

            case "Taika_part1":
                PoolMaker = Taika_part1;
                break;

            case "Taika_part2":
                PoolMaker = Taika_part2;
                break;

            case "Taika_part3":
                PoolMaker = Taika_part3;
                break;

            //아트로-크로스파 390
            case "AtroCrossfa390RightLeg":
                PoolMaker = AtroCrossfa390RightLeg;

                break;
            case "AtroCrossfa390RightLegTop":
                PoolMaker = AtroCrossfa390RightLegTop;
                break;

            case "AtroCrossfa390RightLegdown":
                PoolMaker = AtroCrossfa390RightLegdown;
                break;

            case "AtroCrossfa390LeftLeg":
                PoolMaker = AtroCrossfa390LeftLeg;
                break;

            case "AtroCrossfa390LeftLegTop":
                PoolMaker = AtroCrossfa390LeftLegTop;
                break;

            case "AtroCrossfa390LeftLegdown":
                PoolMaker = AtroCrossfa390LeftLegdown;
                break;

            case "AtroCrossfa390Explosion":
                PoolMaker = AtroCrossfa390Explosion;
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