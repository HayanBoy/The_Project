using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAmmoObjectPoolInBackground : MonoBehaviour
{
    public static ShipAmmoObjectPoolInBackground instance = null;

    //나리하 인류연합 함포
    public GameObject NarihaArtillery1Prefab;
    public GameObject NarihaArtillery1ExplosionPrefab;
    public GameObject NarihaMissile1Prefab;
    public GameObject NarihaMissile1ExplosionPrefab;

    GameObject[] NarihaArtillery1;
    GameObject[] NarihaArtillery1Explosion;
    GameObject[] NarihaMissile1;
    GameObject[] NarihaMissile1Explosion;

    //슬로리어스 함포
    public GameObject SloriusEnergyRay1Prefab;
    public GameObject SloriusEnergyRay1ExplosionPrefab;

    GameObject[] SloriusEnergyRay1;
    GameObject[] SloriusEnergyRay1Explosion;

    //칸타크리 함포
    public GameObject KantakriMissile1Prefab;
    public GameObject KantakriMissile1ExplosionPrefab;

    GameObject[] KantakriMissile1;
    GameObject[] KantakriMissile1Explosion;

    GameObject[] PoolMaker;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        NarihaArtillery1 = new GameObject[50];
        NarihaArtillery1Explosion = new GameObject[100];
        NarihaMissile1 = new GameObject[50];
        NarihaMissile1Explosion = new GameObject[100];

        SloriusEnergyRay1 = new GameObject[50];
        SloriusEnergyRay1Explosion = new GameObject[100];

        KantakriMissile1 = new GameObject[50];
        KantakriMissile1Explosion = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < NarihaArtillery1.Length; index++)
        {
            NarihaArtillery1[index] = Instantiate(NarihaArtillery1Prefab);
            NarihaArtillery1Prefab.layer = 10;
            NarihaArtillery1[index].SetActive(false);
        }
        for (int index = 0; index < NarihaArtillery1Explosion.Length; index++)
        {
            NarihaArtillery1Explosion[index] = Instantiate(NarihaArtillery1ExplosionPrefab);
            NarihaArtillery1ExplosionPrefab.layer = 10;
            NarihaArtillery1Explosion[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMissile1.Length; index++)
        {
            NarihaMissile1[index] = Instantiate(NarihaMissile1Prefab);
            NarihaMissile1Prefab.layer = 10;
            NarihaMissile1[index].SetActive(false);
        }
        for (int index = 0; index < NarihaMissile1Explosion.Length; index++)
        {
            NarihaMissile1Explosion[index] = Instantiate(NarihaMissile1ExplosionPrefab);
            NarihaMissile1ExplosionPrefab.layer = 10;
            NarihaMissile1Explosion[index].SetActive(false);
        }

        for (int index = 0; index < SloriusEnergyRay1.Length; index++)
        {
            SloriusEnergyRay1[index] = Instantiate(SloriusEnergyRay1Prefab);
            SloriusEnergyRay1Prefab.layer = 10;
            SloriusEnergyRay1[index].SetActive(false);
        }
        for (int index = 0; index < SloriusEnergyRay1Explosion.Length; index++)
        {
            SloriusEnergyRay1Explosion[index] = Instantiate(SloriusEnergyRay1ExplosionPrefab);
            SloriusEnergyRay1ExplosionPrefab.layer = 10;
            SloriusEnergyRay1Explosion[index].SetActive(false);
        }

        for (int index = 0; index < KantakriMissile1.Length; index++)
        {
            KantakriMissile1[index] = Instantiate(KantakriMissile1Prefab);
            KantakriMissile1Prefab.layer = 10;
            KantakriMissile1[index].SetActive(false);
        }
        for (int index = 0; index < KantakriMissile1Explosion.Length; index++)
        {
            KantakriMissile1Explosion[index] = Instantiate(KantakriMissile1ExplosionPrefab);
            KantakriMissile1ExplosionPrefab.layer = 10;
            KantakriMissile1Explosion[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "NarihaArtillery1":
                PoolMaker = NarihaArtillery1;
                break;
            case "NarihaArtillery1Explosion":
                PoolMaker = NarihaArtillery1Explosion;
                break;
            case "NarihaMissile1":
                PoolMaker = NarihaMissile1;
                break;
            case "NarihaMissile1Explosion":
                PoolMaker = NarihaMissile1Explosion;
                break;

            case "SloriusEnergyRay1":
                PoolMaker = SloriusEnergyRay1;
                break;
            case "SloriusEnergyRay1Explosion":
                PoolMaker = SloriusEnergyRay1Explosion;
                break;

            case "KantakriMissile1":
                PoolMaker = KantakriMissile1;
                break;
            case "KantakriMissile1Explosion":
                PoolMaker = KantakriMissile1Explosion;
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