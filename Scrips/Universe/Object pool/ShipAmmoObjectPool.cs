using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAmmoObjectPool : MonoBehaviour
{
    public static ShipAmmoObjectPool instance = null;

    [Header("나리하 인류연합 함포")]
    public GameObject NarihaSilenceSistArtillery1FlagshipPrefab;
    public GameObject NarihaSilenceSistArtillery1ExplosionFlagshipPrefab;
    public GameObject NarihaSilenceSistArtillery1FormationPrefab;
    public GameObject NarihaSilenceSistArtillery1ExplosionFormationPrefab;
    public GameObject NarihaOverJumpArtillery1FlagshipPrefab;
    public GameObject NarihaOverJumpArtillery1ExplosionFlagshipPrefab;
    public GameObject NarihaOverJumpArtillery1FormationPrefab;
    public GameObject NarihaOverJumpArtillery1ExplosionFormationPrefab;
    public GameObject NarihaSingleMissile1FlagshipPrefab;
    public GameObject NarihaSingleMissile1ExplosionFlagshipPrefab;
    public GameObject NarihaSingleMissile1FormationPrefab;
    public GameObject NarihaSingleMissile1ExplosionFormationPrefab;
    public GameObject NarihaMultiMissile1FlagshipPrefab;
    public GameObject NarihaMultiMissile1ExplosionFlagshipPrefab;
    public GameObject NarihaMultiMissile1FormationPrefab;
    public GameObject NarihaMultiMissile1ExplosionFormationPrefab;

    GameObject[] NarihaSilenceSistArtillery1Flagship;
    GameObject[] NarihaSilenceSistArtillery1ExplosionFlagship;
    GameObject[] NarihaSilenceSistArtillery1Formation;
    GameObject[] NarihaSilenceSistArtillery1ExplosionFormation;
    GameObject[] NarihaOverJumpArtillery1Flagship;
    GameObject[] NarihaOverJumpArtillery1ExplosionFlagship;
    GameObject[] NarihaOverJumpArtillery1Formation;
    GameObject[] NarihaOverJumpArtillery1ExplosionFormation;
    GameObject[] NarihaSingleMissile1Flagship;
    GameObject[] NarihaSingleMissile1ExplosionFlagship;
    GameObject[] NarihaSingleMissile1Formation;
    GameObject[] NarihaSingleMissile1ExplosionFormation;
    GameObject[] NarihaMultiMissile1Flagship;
    GameObject[] NarihaMultiMissile1ExplosionFlagship;
    GameObject[] NarihaMultiMissile1Formation;
    GameObject[] NarihaMultiMissile1ExplosionFormation;

    private int NarihaSilenceSistArtillery1FlagshipMaxCount;
    private int NarihaSilenceSistArtillery1ExplosionFlagshipMaxCount;
    private int NarihaSilenceSistArtillery1FormationMaxCount;
    private int NarihaSilenceSistArtillery1ExplosionFormationMaxCount;
    private int NarihaOverJumpArtillery1FlagshipMaxCount;
    private int NarihaOverJumpArtillery1ExplosionFlagshipMaxCount;
    private int NarihaOverJumpArtillery1FormationMaxCount;
    private int NarihaOverJumpArtillery1ExplosionFormationMaxCount;
    private int NarihaSingleMissile1FlagshipMaxCount;
    private int NarihaSingleMissile1ExplosionFlagshipMaxCount;
    private int NarihaSingleMissile1FormationMaxCount;
    private int NarihaSingleMissile1ExplosionFormationMaxCount;
    private int NarihaMultiMissile1FlagshipMaxCount;
    private int NarihaMultiMissile1ExplosionFlagshipMaxCount;
    private int NarihaMultiMissile1FormationMaxCount;
    private int NarihaMultiMissile1ExplosionFormationMaxCount;

    private int NarihaSilenceSistArtillery1FlagshipActiveCount;
    private int NarihaSilenceSistArtillery1ExplosionFlagshipActiveCount;
    private int NarihaSilenceSistArtillery1FormationActiveCount;
    private int NarihaSilenceSistArtillery1ExplosionFormationActiveCount;
    private int NarihaOverJumpArtillery1FlagshipActiveCount;
    private int NarihaOverJumpArtillery1ExplosionFlagshipActiveCount;
    private int NarihaOverJumpArtillery1FormationActiveCount;
    private int NarihaOverJumpArtillery1ExplosionFormationActiveCount;
    private int NarihaSingleMissile1FlagshipActiveCount;
    private int NarihaSingleMissile1ExplosionFlagshipActiveCount;
    private int NarihaSingleMissile1FormationActiveCount;
    private int NarihaSingleMissile1ExplosionFormationActiveCount;
    private int NarihaMultiMissile1FlagshipActiveCount;
    private int NarihaMultiMissile1ExplosionFlagshipActiveCount;
    private int NarihaMultiMissile1FormationActiveCount;
    private int NarihaMultiMissile1ExplosionFormationActiveCount;

    [Header("나리하 인류연합 함재기")]
    public GameObject NarihaFighter1Prefab;
    public GameObject NarihaBomer1Prefab;
    public GameObject NarihaBomer1Artillery1Prefab;
    public GameObject NarihaBomer1Artillery1ExplosionPrefab;

    GameObject[] NarihaFighter1;
    GameObject[] NarihaBomer1;
    GameObject[] NarihaBomer1Artillery1;
    GameObject[] NarihaBomer1Artillery1Explosion;

    private int NarihaFighter1MaxCount;
    private int NarihaBomer1MaxCount;
    private int NarihaBomer1Artillery1MaxCount;
    private int NarihaBomer1Artillery1ExplosionMaxCount;

    private int NarihaFighter1ActiveCount;
    private int NarihaBomer1ActiveCount;
    private int NarihaBomer1Artillery1ActiveCount;
    private int NarihaBomer1Artillery1ExplosionActiveCount;

    [Header("나리하 인류연합 스킬")]
    public GameObject NarihaClusterMissile1FlagshipPrefab;
    public GameObject NarihaClusterMissile1FormationPrefab;
    public GameObject NarihaNuclearMissile1FlagshipPrefab;
    public GameObject NarihaNuclearMissile1ExplosionFlagshipPrefab;
    GameObject[] NarihaClusterMissile1Flagship;
    GameObject[] NarihaClusterMissile1Formation;
    GameObject[] NarihaNuclearMissile1Flagship;
    GameObject[] NarihaNuclearMissile1ExplosionFlagship;

    [Header("슬로리어스 함포")]
    public GameObject SloriusEnergyRay1Prefab;
    public GameObject SloriusEnergyRay1ExplosionPrefab;
    public GameObject SloriusSolidBeam1FlagshipPrefab;
    public GameObject SloriusSolidBeam1ExplosionFlagshipPrefab;
    public GameObject SloriusSolidBeam1FormationPrefab;
    public GameObject SloriusSolidBeam1ExplosionFormationPrefab;

    GameObject[] SloriusEnergyRay1;
    GameObject[] SloriusEnergyRay1Explosion;
    GameObject[] SloriusSolidBeam1Flagship;
    GameObject[] SloriusSolidBeam1ExplosionFlagship;
    GameObject[] SloriusSolidBeam1Formation;
    GameObject[] SloriusSolidBeam1ExplosionFormation;

    private int SloriusEnergyRay1MaxCount;
    private int SloriusEnergyRay1ExplosionMaxCount;
    private int SloriusSolidBeam1FlagshipMaxCount;
    private int SloriusSolidBeam1ExplosionFlagshipMaxCount;
    private int SloriusSolidBeam1FormationMaxCount;
    private int SloriusSolidBeam1ExplosionFormationMaxCount;

    private int SloriusEnergyRay1ActiveCount;
    private int SloriusEnergyRay1ExplosionActiveCount;
    private int SloriusSolidBeam1FlagshipActiveCount;
    private int SloriusSolidBeam1ExplosionFlagshipActiveCount;
    private int SloriusSolidBeam1FormationActiveCount;
    private int SloriusSolidBeam1ExplosionFormationActiveCount;

    [Header("칸타크리 함포")]
    public GameObject KantakriArtillery1Prefab;
    public GameObject KantakriArtillery1ExplosionPrefab;
    public GameObject KantakriArtillery2Prefab;
    public GameObject KantakriArtillery2ExplosionPrefab;
    public GameObject KantakriMissile1BoxPrefab;
    public GameObject KantakriMissile1BoxExplosionPrefab;
    public GameObject KantakriMissile1Prefab;
    public GameObject KantakriMissile1ExplosionPrefab;
    public GameObject KantakriMultiHitAmmo1FlagshipPrefab;
    public GameObject KantakriMultiHitAmmoExplosion1FlagshipPrefab;
    public GameObject KantakriMultiHitAmmo1FormationPrefab;
    public GameObject KantakriMultiHitAmmo1ExplosionFormationPrefab;

    GameObject[] KantakriArtillery1;
    GameObject[] KantakriArtillery1Explosion;
    GameObject[] KantakriArtillery2;
    GameObject[] KantakriArtillery2Explosion;
    GameObject[] KantakriMissile1Box;
    GameObject[] KantakriMissile1BoxExplosion;
    GameObject[] KantakriMissile1;
    GameObject[] KantakriMissile1Explosion;
    GameObject[] KantakriMultiHitAmmo1Flagship;
    GameObject[] KantakriMultiHitAmmoExplosion1Flagship;
    GameObject[] KantakriMultiHitAmmo1Formation;
    GameObject[] KantakriMultiHitAmmo1ExplosionFormation;

    private int KantakriArtillery1MaxCount;
    private int KantakriArtillery1ExplosionMaxCount;
    private int KantakriArtillery2MaxCount;
    private int KantakriArtillery2ExplosionMaxCount;

    private int KantakriArtillery1ActiveCount;
    private int KantakriArtillery1ExplosionActiveCount;
    private int KantakriArtillery2ActiveCount;
    private int KantakriArtillery2ExplosionActiveCount;

    [Header("기타 효과")]
    public GameObject SloriusFlagshipExplosionPrefab;

    GameObject[] SloriusFlagshipExplosion;

    GameObject[] PoolMaker;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        //나리하 인류연합 함포
        NarihaSilenceSistArtillery1Flagship = new GameObject[50];
        NarihaSilenceSistArtillery1ExplosionFlagship = new GameObject[100];
        NarihaSilenceSistArtillery1Formation = new GameObject[150];
        NarihaSilenceSistArtillery1ExplosionFormation = new GameObject[150];
        NarihaOverJumpArtillery1Flagship = new GameObject[50];
        NarihaOverJumpArtillery1ExplosionFlagship = new GameObject[50];
        NarihaOverJumpArtillery1Formation = new GameObject[100];
        NarihaOverJumpArtillery1ExplosionFormation = new GameObject[100];
        NarihaSingleMissile1Flagship = new GameObject[100];
        NarihaSingleMissile1ExplosionFlagship = new GameObject[100];
        NarihaSingleMissile1Formation = new GameObject[100];
        NarihaSingleMissile1ExplosionFormation = new GameObject[100];
        NarihaMultiMissile1Flagship = new GameObject[100];
        NarihaMultiMissile1ExplosionFlagship = new GameObject[100];
        NarihaMultiMissile1Formation = new GameObject[200];
        NarihaMultiMissile1ExplosionFormation = new GameObject[200];

        //나리하 인류연합 함재기
        NarihaFighter1 = new GameObject[100];
        NarihaBomer1 = new GameObject[600];
        NarihaBomer1Artillery1 = new GameObject[600];
        NarihaBomer1Artillery1Explosion = new GameObject[600];

        //나리하 인류연합 스킬
        NarihaClusterMissile1Flagship = new GameObject[30];
        NarihaClusterMissile1Formation = new GameObject[200];
        NarihaNuclearMissile1Flagship = new GameObject[6];
        NarihaNuclearMissile1ExplosionFlagship = new GameObject[6];

        //슬로리어스 함포
        SloriusEnergyRay1 = new GameObject[200];
        SloriusEnergyRay1Explosion = new GameObject[200];
        SloriusSolidBeam1Flagship = new GameObject[50];
        SloriusSolidBeam1ExplosionFlagship = new GameObject[50];
        SloriusSolidBeam1Formation = new GameObject[150];
        SloriusSolidBeam1ExplosionFormation = new GameObject[150];

        //칸타크리 함포
        KantakriArtillery1 = new GameObject[400];
        KantakriArtillery1Explosion = new GameObject[400];
        KantakriArtillery2 = new GameObject[400];
        KantakriArtillery2Explosion = new GameObject[400];
        KantakriMissile1Box = new GameObject[100];
        KantakriMissile1BoxExplosion = new GameObject[100];
        KantakriMissile1 = new GameObject[200];
        KantakriMissile1Explosion = new GameObject[200];
        KantakriMultiHitAmmo1Flagship = new GameObject[100];
        KantakriMultiHitAmmoExplosion1Flagship = new GameObject[100];
        KantakriMultiHitAmmo1Formation = new GameObject[150];
        KantakriMultiHitAmmo1ExplosionFormation = new GameObject[150];

        SloriusFlagshipExplosion = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        //나리하 인류연합 발사체
        for (int index = 0; index < NarihaSilenceSistArtillery1Flagship.Length; index++)
        {
            NarihaSilenceSistArtillery1Flagship[index] = Instantiate(NarihaSilenceSistArtillery1FlagshipPrefab);
            NarihaSilenceSistArtillery1Flagship[index].SetActive(false);
            NarihaSilenceSistArtillery1FlagshipMaxCount++;
        }
        for (int index = 0; index < NarihaSilenceSistArtillery1ExplosionFlagship.Length; index++)
        {
            NarihaSilenceSistArtillery1ExplosionFlagship[index] = Instantiate(NarihaSilenceSistArtillery1ExplosionFlagshipPrefab);
            NarihaSilenceSistArtillery1ExplosionFlagship[index].SetActive(false);
            NarihaSilenceSistArtillery1ExplosionFlagshipMaxCount++;
        }
        for (int index = 0; index < NarihaSilenceSistArtillery1Formation.Length; index++)
        {
            NarihaSilenceSistArtillery1Formation[index] = Instantiate(NarihaSilenceSistArtillery1FormationPrefab);
            NarihaSilenceSistArtillery1Formation[index].SetActive(false);
            NarihaSilenceSistArtillery1FormationMaxCount++;
        }
        for (int index = 0; index < NarihaSilenceSistArtillery1ExplosionFormation.Length; index++)
        {
            NarihaSilenceSistArtillery1ExplosionFormation[index] = Instantiate(NarihaSilenceSistArtillery1ExplosionFormationPrefab);
            NarihaSilenceSistArtillery1ExplosionFormation[index].SetActive(false);
            NarihaSilenceSistArtillery1ExplosionFormationMaxCount++;
        }
        for (int index = 0; index < NarihaOverJumpArtillery1Flagship.Length; index++)
        {
            NarihaOverJumpArtillery1Flagship[index] = Instantiate(NarihaOverJumpArtillery1FlagshipPrefab);
            NarihaOverJumpArtillery1Flagship[index].SetActive(false);
            NarihaOverJumpArtillery1FlagshipMaxCount++;
        }
        for (int index = 0; index < NarihaOverJumpArtillery1ExplosionFlagship.Length; index++)
        {
            NarihaOverJumpArtillery1ExplosionFlagship[index] = Instantiate(NarihaOverJumpArtillery1ExplosionFlagshipPrefab);
            NarihaOverJumpArtillery1ExplosionFlagship[index].SetActive(false);
            NarihaOverJumpArtillery1ExplosionFlagshipMaxCount++;
        }
        for (int index = 0; index < NarihaOverJumpArtillery1Formation.Length; index++)
        {
            NarihaOverJumpArtillery1Formation[index] = Instantiate(NarihaOverJumpArtillery1FormationPrefab);
            NarihaOverJumpArtillery1Formation[index].SetActive(false);
            NarihaOverJumpArtillery1FormationMaxCount++;
        }
        for (int index = 0; index < NarihaOverJumpArtillery1ExplosionFormation.Length; index++)
        {
            NarihaOverJumpArtillery1ExplosionFormation[index] = Instantiate(NarihaOverJumpArtillery1ExplosionFormationPrefab);
            NarihaOverJumpArtillery1ExplosionFormation[index].SetActive(false);
            NarihaOverJumpArtillery1ExplosionFormationMaxCount++;
        }
        for (int index = 0; index < NarihaSingleMissile1Flagship.Length; index++)
        {
            NarihaSingleMissile1Flagship[index] = Instantiate(NarihaSingleMissile1FlagshipPrefab);
            NarihaSingleMissile1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaSingleMissile1ExplosionFlagship.Length; index++)
        {
            NarihaSingleMissile1ExplosionFlagship[index] = Instantiate(NarihaSingleMissile1ExplosionFlagshipPrefab);
            NarihaSingleMissile1ExplosionFlagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaSingleMissile1Formation.Length; index++)
        {
            NarihaSingleMissile1Formation[index] = Instantiate(NarihaSingleMissile1FormationPrefab);
            NarihaSingleMissile1Formation[index].SetActive(false);
        }
        for (int index = 0; index < NarihaSingleMissile1ExplosionFormation.Length; index++)
        {
            NarihaSingleMissile1ExplosionFormation[index] = Instantiate(NarihaSingleMissile1ExplosionFormationPrefab);
            NarihaSingleMissile1ExplosionFormation[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMultiMissile1Flagship.Length; index++)
        {
            NarihaMultiMissile1Flagship[index] = Instantiate(NarihaMultiMissile1FlagshipPrefab);
            NarihaMultiMissile1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMultiMissile1ExplosionFlagship.Length; index++)
        {
            NarihaMultiMissile1ExplosionFlagship[index] = Instantiate(NarihaMultiMissile1ExplosionFlagshipPrefab);
            NarihaMultiMissile1ExplosionFlagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMultiMissile1Formation.Length; index++)
        {
            NarihaMultiMissile1Formation[index] = Instantiate(NarihaMultiMissile1FormationPrefab);
            NarihaMultiMissile1Formation[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMultiMissile1ExplosionFormation.Length; index++)
        {
            NarihaMultiMissile1ExplosionFormation[index] = Instantiate(NarihaMultiMissile1ExplosionFormationPrefab);
            NarihaMultiMissile1ExplosionFormation[index].SetActive(false);
        }

        //나리하 인류연합 함재기
        for (int index = 0; index < NarihaFighter1.Length; index++)
        {
            NarihaFighter1[index] = Instantiate(NarihaFighter1Prefab);
            NarihaFighter1[index].SetActive(false);
            NarihaFighter1MaxCount++;
        }
        for (int index = 0; index < NarihaBomer1.Length; index++)
        {
            NarihaBomer1[index] = Instantiate(NarihaBomer1Prefab);
            NarihaBomer1[index].SetActive(false);
            NarihaBomer1MaxCount++;
        }
        for (int index = 0; index < NarihaBomer1Artillery1.Length; index++)
        {
            NarihaBomer1Artillery1[index] = Instantiate(NarihaBomer1Artillery1Prefab);
            NarihaBomer1Artillery1[index].SetActive(false);
            NarihaBomer1Artillery1MaxCount++;
        }
        for (int index = 0; index < NarihaBomer1Artillery1Explosion.Length; index++)
        {
            NarihaBomer1Artillery1Explosion[index] = Instantiate(NarihaBomer1Artillery1ExplosionPrefab);
            NarihaBomer1Artillery1Explosion[index].SetActive(false);
            NarihaBomer1Artillery1ExplosionMaxCount++;
        }

        //나리하 인류연합 스킬
        for (int index = 0; index < NarihaClusterMissile1Flagship.Length; index++)
        {
            NarihaClusterMissile1Flagship[index] = Instantiate(NarihaClusterMissile1FlagshipPrefab);
            NarihaClusterMissile1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaClusterMissile1Formation.Length; index++)
        {
            NarihaClusterMissile1Formation[index] = Instantiate(NarihaClusterMissile1FormationPrefab);
            NarihaClusterMissile1Formation[index].SetActive(false);
        }
        for (int index = 0; index < NarihaNuclearMissile1Flagship.Length; index++)
        {
            NarihaNuclearMissile1Flagship[index] = Instantiate(NarihaNuclearMissile1FlagshipPrefab);
            NarihaNuclearMissile1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < NarihaNuclearMissile1ExplosionFlagship.Length; index++)
        {
            NarihaNuclearMissile1ExplosionFlagship[index] = Instantiate(NarihaNuclearMissile1ExplosionFlagshipPrefab);
            NarihaNuclearMissile1ExplosionFlagship[index].SetActive(false);
        }

        //슬로리어스 발사체
        for (int index = 0; index < SloriusEnergyRay1.Length; index++)
        {
            SloriusEnergyRay1[index] = Instantiate(SloriusEnergyRay1Prefab);
            SloriusEnergyRay1[index].SetActive(false);
            SloriusEnergyRay1MaxCount++;
        }
        for (int index = 0; index < SloriusEnergyRay1Explosion.Length; index++)
        {
            SloriusEnergyRay1Explosion[index] = Instantiate(SloriusEnergyRay1ExplosionPrefab);
            SloriusEnergyRay1Explosion[index].SetActive(false);
            SloriusEnergyRay1ExplosionMaxCount++;
        }
        for (int index = 0; index < SloriusSolidBeam1Flagship.Length; index++)
        {
            SloriusSolidBeam1Flagship[index] = Instantiate(SloriusSolidBeam1FlagshipPrefab);
            SloriusSolidBeam1Flagship[index].SetActive(false);
            SloriusSolidBeam1FlagshipMaxCount++;
        }
        for (int index = 0; index < SloriusSolidBeam1ExplosionFlagship.Length; index++)
        {
            SloriusSolidBeam1ExplosionFlagship[index] = Instantiate(SloriusSolidBeam1ExplosionFlagshipPrefab);
            SloriusSolidBeam1ExplosionFlagship[index].SetActive(false);
            SloriusSolidBeam1ExplosionFlagshipMaxCount++;
        }
        for (int index = 0; index < SloriusSolidBeam1Formation.Length; index++)
        {
            SloriusSolidBeam1Formation[index] = Instantiate(SloriusSolidBeam1FormationPrefab);
            SloriusSolidBeam1Formation[index].SetActive(false);
            SloriusSolidBeam1FormationMaxCount++;
        }
        for (int index = 0; index < SloriusSolidBeam1ExplosionFormation.Length; index++)
        {
            SloriusSolidBeam1ExplosionFormation[index] = Instantiate(SloriusSolidBeam1ExplosionFormationPrefab);
            SloriusSolidBeam1ExplosionFormation[index].SetActive(false);
            SloriusSolidBeam1ExplosionFormationMaxCount++;
        }

        //칸타크리 발사체
        for (int index = 0; index < KantakriArtillery1.Length; index++)
        {
            KantakriArtillery1[index] = Instantiate(KantakriArtillery1Prefab);
            KantakriArtillery1[index].SetActive(false);
            KantakriArtillery1MaxCount++;
        }
        for (int index = 0; index < KantakriArtillery1Explosion.Length; index++)
        {
            KantakriArtillery1Explosion[index] = Instantiate(KantakriArtillery1ExplosionPrefab);
            KantakriArtillery1Explosion[index].SetActive(false);
            KantakriArtillery1ExplosionMaxCount++;
        }
        for (int index = 0; index < KantakriArtillery2.Length; index++)
        {
            KantakriArtillery2[index] = Instantiate(KantakriArtillery2Prefab);
            KantakriArtillery2[index].SetActive(false);
            KantakriArtillery2MaxCount++;
        }
        for (int index = 0; index < KantakriArtillery1Explosion.Length; index++)
        {
            KantakriArtillery2Explosion[index] = Instantiate(KantakriArtillery2ExplosionPrefab);
            KantakriArtillery2Explosion[index].SetActive(false);
            KantakriArtillery2ExplosionMaxCount++;
        }
        for (int index = 0; index < KantakriMissile1Box.Length; index++)
        {
            KantakriMissile1Box[index] = Instantiate(KantakriMissile1BoxPrefab);
            KantakriMissile1Box[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMissile1BoxExplosion.Length; index++)
        {
            KantakriMissile1BoxExplosion[index] = Instantiate(KantakriMissile1BoxExplosionPrefab);
            KantakriMissile1BoxExplosion[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMissile1.Length; index++)
        {
            KantakriMissile1[index] = Instantiate(KantakriMissile1Prefab);
            KantakriMissile1[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMissile1Explosion.Length; index++)
        {
            KantakriMissile1Explosion[index] = Instantiate(KantakriMissile1ExplosionPrefab);
            KantakriMissile1Explosion[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMultiHitAmmo1Flagship.Length; index++)
        {
            KantakriMultiHitAmmo1Flagship[index] = Instantiate(KantakriMultiHitAmmo1FlagshipPrefab);
            KantakriMultiHitAmmo1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMultiHitAmmoExplosion1Flagship.Length; index++)
        {
            KantakriMultiHitAmmoExplosion1Flagship[index] = Instantiate(KantakriMultiHitAmmoExplosion1FlagshipPrefab);
            KantakriMultiHitAmmoExplosion1Flagship[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMultiHitAmmo1Formation.Length; index++)
        {
            KantakriMultiHitAmmo1Formation[index] = Instantiate(KantakriMultiHitAmmo1FormationPrefab);
            KantakriMultiHitAmmo1Formation[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMultiHitAmmo1ExplosionFormation.Length; index++)
        {
            KantakriMultiHitAmmo1ExplosionFormation[index] = Instantiate(KantakriMultiHitAmmo1ExplosionFormationPrefab);
            KantakriMultiHitAmmo1ExplosionFormation[index].SetActive(false);
        }

        for (int index = 0; index < SloriusFlagshipExplosion.Length; index++)
        {
            SloriusFlagshipExplosion[index] = Instantiate(SloriusFlagshipExplosionPrefab);
            SloriusFlagshipExplosion[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "NarihaSilenceSistArtillery1Flagship":
                PoolMaker = NarihaSilenceSistArtillery1Flagship;
                NarihaSilenceSistArtillery1FlagshipActiveCount++;
                break;
            case "NarihaSilenceSistArtillery1ExplosionFlagship":
                PoolMaker = NarihaSilenceSistArtillery1ExplosionFlagship;
                NarihaSilenceSistArtillery1ExplosionFlagshipActiveCount++;
                break;
            case "NarihaSilenceSistArtillery1Formation":
                PoolMaker = NarihaSilenceSistArtillery1Formation;
                NarihaSilenceSistArtillery1FormationActiveCount++;
                break;
            case "NarihaSilenceSistArtillery1ExplosionFormation":
                PoolMaker = NarihaSilenceSistArtillery1ExplosionFormation;
                NarihaSilenceSistArtillery1ExplosionFormationActiveCount++;
                break;
            case "NarihaOverJumpArtillery1Flagship":
                PoolMaker = NarihaOverJumpArtillery1Flagship;
                NarihaOverJumpArtillery1FlagshipActiveCount++;
                break;
            case "NarihaOverJumpArtillery1ExplosionFlagship":
                PoolMaker = NarihaOverJumpArtillery1ExplosionFlagship;
                NarihaOverJumpArtillery1ExplosionFlagshipActiveCount++;
                break;
            case "NarihaOverJumpArtillery1Formation":
                PoolMaker = NarihaOverJumpArtillery1Formation;
                NarihaOverJumpArtillery1FormationActiveCount++;
                break;
            case "NarihaOverJumpArtillery1ExplosionFormation":
                PoolMaker = NarihaOverJumpArtillery1ExplosionFormation;
                NarihaOverJumpArtillery1ExplosionFormationActiveCount++;
                break;
            case "NarihaSingleMissile1Flagship":
                PoolMaker = NarihaSingleMissile1Flagship;
                NarihaSingleMissile1FlagshipActiveCount++;
                break;
            case "NarihaSingleMissile1ExplosionFlagship":
                PoolMaker = NarihaSingleMissile1ExplosionFlagship;
                NarihaSingleMissile1ExplosionFlagshipActiveCount++;
                break;
            case "NarihaSingleMissile1Formation":
                PoolMaker = NarihaSingleMissile1Formation;
                NarihaSingleMissile1FormationActiveCount++;
                break;
            case "NarihaSingleMissile1ExplosionFormation":
                PoolMaker = NarihaSingleMissile1ExplosionFormation;
                NarihaSingleMissile1ExplosionFormationActiveCount++;
                break;
            case "NarihaMultiMissile1Flagship":
                PoolMaker = NarihaMultiMissile1Flagship;
                NarihaMultiMissile1FlagshipActiveCount++;
                break;
            case "NarihaMultiMissile1ExplosionFlagship":
                PoolMaker = NarihaMultiMissile1ExplosionFlagship;
                NarihaMultiMissile1ExplosionFlagshipActiveCount++;
                break;
            case "NarihaMultiMissile1Formation":
                PoolMaker = NarihaMultiMissile1Formation;
                NarihaMultiMissile1FormationActiveCount++;
                break;
            case "NarihaMultiMissile1ExplosionFormation":
                PoolMaker = NarihaMultiMissile1ExplosionFormation;
                NarihaMultiMissile1ExplosionFormationActiveCount++;
                break;

            case "NarihaClusterMissile1Flagship":
                PoolMaker = NarihaClusterMissile1Flagship;
                break;
            case "NarihaClusterMissile1Formation":
                PoolMaker = NarihaClusterMissile1Formation;
                break;
            case "NarihaNuclearMissile1Flagship":
                PoolMaker = NarihaNuclearMissile1Flagship;
                break;
            case "NarihaNuclearMissile1ExplosionFlagship":
                PoolMaker = NarihaNuclearMissile1ExplosionFlagship;
                break;

            case "NarihaFighter1":
                PoolMaker = NarihaFighter1;
                break;
            case "NarihaBomer1":
                PoolMaker = NarihaBomer1;
                break;
            case "NarihaBomer1Artillery1":
                PoolMaker = NarihaBomer1Artillery1;
                break;
            case "NarihaBomer1Artillery1Explosion":
                PoolMaker = NarihaBomer1Artillery1Explosion;
                NarihaBomer1Artillery1ExplosionActiveCount++;
                break;

            case "SloriusEnergyRay1":
                PoolMaker = SloriusEnergyRay1;
                SloriusEnergyRay1ActiveCount++;
                break;
            case "SloriusEnergyRay1Explosion":
                PoolMaker = SloriusEnergyRay1Explosion;
                SloriusEnergyRay1ExplosionActiveCount++;
                break;
            case "SloriusSolidBeam1Flagship":
                PoolMaker = SloriusSolidBeam1Flagship;
                SloriusSolidBeam1FlagshipActiveCount++;
                break;
            case "SloriusSolidBeam1ExplosionFlagship":
                PoolMaker = SloriusSolidBeam1ExplosionFlagship;
                SloriusSolidBeam1ExplosionFlagshipActiveCount++;
                break;
            case "SloriusSolidBeam1Formation":
                PoolMaker = SloriusSolidBeam1Formation;
                SloriusSolidBeam1FormationActiveCount++;
                break;
            case "SloriusSolidBeam1ExplosionFormation":
                PoolMaker = SloriusSolidBeam1ExplosionFormation;
                SloriusSolidBeam1ExplosionFormationActiveCount++;
                break;

            case "KantakriArtillery1":
                PoolMaker = KantakriArtillery1;
                KantakriArtillery1ActiveCount++;
                break;
            case "KantakriArtillery1Explosion":
                PoolMaker = KantakriArtillery1Explosion;
                KantakriArtillery1ExplosionActiveCount++;
                break;
            case "KantakriArtillery2":
                PoolMaker = KantakriArtillery1;
                KantakriArtillery2ActiveCount++;
                break;
            case "KantakriArtillery2Explosion":
                PoolMaker = KantakriArtillery1Explosion;
                KantakriArtillery2ExplosionActiveCount++;
                break;
            case "KantakriMissile1Box":
                PoolMaker = KantakriMissile1Box;
                break;
            case "KantakriMissile1BoxExplosion":
                PoolMaker = KantakriMissile1BoxExplosion;
                break;
            case "KantakriMissile1":
                PoolMaker = KantakriMissile1;
                break;
            case "KantakriMissile1Explosion":
                PoolMaker = KantakriMissile1Explosion;
                break;
            case "KantakriMultiHitAmmo1Flagship":
                PoolMaker = KantakriMultiHitAmmo1Flagship;
                break;
            case "KantakriMultiHitAmmoExplosion1Flagship":
                PoolMaker = KantakriMultiHitAmmoExplosion1Flagship;
                break;
            case "KantakriMultiHitAmmo1Formation":
                PoolMaker = KantakriMultiHitAmmo1Formation;
                break;
            case "KantakriMultiHitAmmo1ExplosionFormation":
                PoolMaker = KantakriMultiHitAmmo1ExplosionFormation;
                break;

            case "SloriusFlagshipExplosion":
                PoolMaker = SloriusFlagshipExplosion;
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

        ConsiderObjectCount();

        return null;
    }

    void ConsiderObjectCount()
    {
        if (NarihaSilenceSistArtillery1FlagshipActiveCount >= NarihaSilenceSistArtillery1FlagshipMaxCount - 5)
        {
            for (int index = NarihaSilenceSistArtillery1FlagshipMaxCount + 1; index < NarihaSilenceSistArtillery1FlagshipMaxCount + 6; index++)
            {
                NarihaSilenceSistArtillery1Flagship[index] = Instantiate(NarihaSilenceSistArtillery1FlagshipPrefab);
                NarihaSilenceSistArtillery1Flagship[index].SetActive(false);
                NarihaSilenceSistArtillery1FlagshipMaxCount++;
                Debug.Log("sss");
            }
        }
    }

    public GameObject Deleter(string type)
    {
        switch (type)
        {
            case "NarihaSilenceSistArtillery1FlagshipDelete":
                NarihaSilenceSistArtillery1FlagshipActiveCount--;
                break;
            case "NarihaSilenceSistArtillery1ExplosionFlagshipDelete":
                NarihaSilenceSistArtillery1ExplosionFlagshipActiveCount--;
                break;
            case "NarihaSilenceSistArtillery1FormationDelete":
                NarihaSilenceSistArtillery1FormationActiveCount--;
                break;
            case "NarihaSilenceSistArtillery1ExplosionFormationDelete":
                NarihaSilenceSistArtillery1ExplosionFormationActiveCount--;
                break;
            case "NarihaOverJumpArtillery1FlagshipDelete":
                NarihaOverJumpArtillery1FlagshipActiveCount--;
                break;
            case "NarihaOverJumpArtillery1ExplosionFlagshipDelete":
                NarihaOverJumpArtillery1ExplosionFlagshipActiveCount--;
                break;
            case "NarihaOverJumpArtillery1FormationDelete":
                NarihaOverJumpArtillery1FormationActiveCount--;
                break;
            case "NarihaOverJumpArtillery1ExplosionFormationDelete":
                NarihaOverJumpArtillery1ExplosionFormationActiveCount--;
                break;
            case "NarihaSingleMissile1FlagshipDelete":
                NarihaSingleMissile1FlagshipActiveCount--;
                break;
            case "NarihaSingleMissile1ExplosionFlagshipDelete":
                NarihaSingleMissile1ExplosionFlagshipActiveCount--;
                break;
            case "NarihaSingleMissile1FormationDelete":
                NarihaSingleMissile1FormationActiveCount--;
                break;
            case "NarihaSingleMissile1ExplosionFormationDelete":
                NarihaSingleMissile1ExplosionFormationActiveCount--;
                break;
            case "NarihaMultiMissile1FlagshipDelete":
                NarihaMultiMissile1FlagshipActiveCount--;
                break;
            case "NarihaMultiMissile1ExplosionFlagshipDelete":
                NarihaMultiMissile1ExplosionFlagshipActiveCount--;
                break;
            case "NarihaMultiMissile1FormationDelete":
                NarihaMultiMissile1FormationActiveCount--;
                break;
            case "NarihaMultiMissile1ExplosionFormationDelete":
                NarihaMultiMissile1ExplosionFormationActiveCount--;
                break;

            case "NarihaClusterMissile1Flagship":
                PoolMaker = NarihaClusterMissile1Flagship;
                break;
            case "NarihaClusterMissile1Formation":
                PoolMaker = NarihaClusterMissile1Formation;
                break;
            case "NarihaNuclearMissile1Flagship":
                PoolMaker = NarihaNuclearMissile1Flagship;
                break;
            case "NarihaNuclearMissile1ExplosionFlagship":
                PoolMaker = NarihaNuclearMissile1ExplosionFlagship;
                break;

            case "NarihaFighter1Delete":
                NarihaFighter1ActiveCount--;
                break;
            case "NarihaBomer1Delete":
                NarihaBomer1ActiveCount--;
                break;
            case "NarihaBomer1Artillery1Delete":
                NarihaBomer1Artillery1ActiveCount--;
                break;
            case "NarihaBomer1Artillery1ExplosionDelete":
                NarihaBomer1Artillery1ExplosionActiveCount--;
                break;

            case "SloriusEnergyRay1Delete":
                SloriusEnergyRay1ActiveCount--;
                break;
            case "SloriusEnergyRay1ExplosionDelete":
                SloriusEnergyRay1ExplosionActiveCount--;
                break;
            case "SloriusSolidBeam1FlagshipDelete":
                SloriusSolidBeam1FlagshipActiveCount--;
                break;
            case "SloriusSolidBeam1ExplosionFlagshipDelete":
                SloriusSolidBeam1ExplosionFlagshipActiveCount--;
                break;
            case "SloriusSolidBeam1FormationDelete":
                SloriusSolidBeam1FormationActiveCount--;
                break;
            case "SloriusSolidBeam1ExplosionFormationDelete":
                SloriusSolidBeam1ExplosionFormationActiveCount--;
                break;

            case "KantakriArtillery1Delete":
                KantakriArtillery1ActiveCount--;
                break;
            case "KantakriArtillery1ExplosionDelete":
                KantakriArtillery1ExplosionActiveCount--;
                break;
            case "KantakriArtillery2Delete":
                KantakriArtillery2ActiveCount--;
                break;
            case "KantakriArtillery2ExplosionDelete":
                KantakriArtillery2ExplosionActiveCount--;
                break;
            case "KantakriMissile1Box":
                PoolMaker = KantakriMissile1Box;
                break;
            case "KantakriMissile1BoxExplosion":
                PoolMaker = KantakriMissile1BoxExplosion;
                break;
            case "KantakriMissile1":
                PoolMaker = KantakriMissile1;
                break;
            case "KantakriMissile1Explosion":
                PoolMaker = KantakriMissile1Explosion;
                break;
            case "KantakriMultiHitAmmo1Flagship":
                PoolMaker = KantakriMultiHitAmmo1Flagship;
                break;
            case "KantakriMultiHitAmmoExplosion1Flagship":
                PoolMaker = KantakriMultiHitAmmoExplosion1Flagship;
                break;
            case "KantakriMultiHitAmmo1Formation":
                PoolMaker = KantakriMultiHitAmmo1Formation;
                break;
            case "KantakriMultiHitAmmo1ExplosionFormation":
                PoolMaker = KantakriMultiHitAmmo1ExplosionFormation;
                break;

            case "SloriusFlagshipExplosion":
                PoolMaker = SloriusFlagshipExplosion;
                break;
        }

        return null;
    }
}