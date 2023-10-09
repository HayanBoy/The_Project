using System.Collections.Generic;
using UnityEngine;

public class VehicleObjectsManager : MonoBehaviour
{
    public static ObjectManager instance = null;

    public GameObject ricochetEffectPrefab;
    GameObject[] ricochetEffect;

    //HTAC
    public GameObject HTACAmmoPrefab;
    public GameObject HTACAmmoFirePrefab;
    public GameObject HTACAmmoSmokePrefab;
    public GameObject HTACShellPrefab;
    public GameObject HTACExplosionPrefab;
    GameObject[] HTACAmmo;
    GameObject[] HTACAmmoFire;
    GameObject[] HTACAmmoSmoke;
    GameObject[] HTACShell;
    GameObject[] HTACExplosion;

    //APC
    public GameObject APCAmmoPrefab;
    public GameObject APCExplosionPrefab;
    GameObject[] APCAmmo;
    GameObject[] APCExplosion;

    //FBWS
    public GameObject FBWSAmmoPrefab;
    public GameObject FBWSShellPrefab;
    GameObject[] FBWSAmmo;
    GameObject[] FBWSShell;

    //OSEHS
    public GameObject OSEHSMissilePrefab;
    GameObject[] OSEHSMissile;

    GameObject[] PoolMaker;

    int ShellCursor;

    public List<GameObject> SupplyList = new List<GameObject>();

    void Start()
    {
        ricochetEffect = new GameObject[10];

        HTACAmmo = new GameObject[3];
        HTACAmmoFire = new GameObject[3];
        HTACAmmoSmoke = new GameObject[3];
        HTACShell = new GameObject[15];
        HTACExplosion = new GameObject[10];

        APCAmmo = new GameObject[3];
        APCExplosion = new GameObject[10];

        FBWSAmmo = new GameObject[25];
        FBWSShell = new GameObject[400];

        OSEHSMissile = new GameObject[10];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < ricochetEffect.Length; index++)
        {
            ricochetEffect[index] = Instantiate(ricochetEffectPrefab);
            ricochetEffect[index].SetActive(false);
        }

        for (int index = 0; index < HTACAmmo.Length; index++)
        {
            HTACAmmo[index] = Instantiate(HTACAmmoPrefab);
            HTACAmmo[index].SetActive(false);
        }
        for (int index = 0; index < HTACAmmoFire.Length; index++)
        {
            HTACAmmoFire[index] = Instantiate(HTACAmmoFirePrefab);
            HTACAmmoFire[index].SetActive(false);
        }
        for (int index = 0; index < HTACAmmoSmoke.Length; index++)
        {
            HTACAmmoSmoke[index] = Instantiate(HTACAmmoSmokePrefab);
            HTACAmmoSmoke[index].SetActive(false);
        }
        for (int index = 0; index < HTACShell.Length; index++)
        {
            HTACShell[index] = Instantiate(HTACShellPrefab);
            HTACShell[index].SetActive(false);
        }
        for (int index = 0; index < HTACExplosion.Length; index++)
        {
            HTACExplosion[index] = Instantiate(HTACExplosionPrefab);
            HTACExplosion[index].SetActive(false);
        }

        for (int index = 0; index < APCAmmo.Length; index++)
        {
            APCAmmo[index] = Instantiate(APCAmmoPrefab);
            APCAmmo[index].SetActive(false);
        }
        for (int index = 0; index < APCExplosion.Length; index++)
        {
            APCExplosion[index] = Instantiate(APCExplosionPrefab);
            APCExplosion[index].SetActive(false);
        }

        for (int index = 0; index < FBWSAmmo.Length; index++)
        {
            FBWSAmmo[index] = Instantiate(FBWSAmmoPrefab);
            FBWSAmmo[index].SetActive(false);
        }
        for (int index = 0; index < FBWSShell.Length; index++)
        {
            FBWSShell[index] = Instantiate(FBWSShellPrefab);
            FBWSShell[index].SetActive(false);
        }

        for (int index = 0; index < OSEHSMissile.Length; index++)
        {
            OSEHSMissile[index] = Instantiate(OSEHSMissilePrefab);
            OSEHSMissile[index].SetActive(false);
        }
    }

    public GameObject VehicleLoader(string type)
    {
        switch (type)
        {
            case "ricochetEffect":
                PoolMaker = ricochetEffect;
                break;

            case "HTACAmmo":
                PoolMaker = HTACAmmo;
                break;
            case "HTACAmmoFire":
                PoolMaker = HTACAmmoFire;
                break;
            case "HTACAmmoSmoke":
                PoolMaker = HTACAmmoSmoke;
                break;
            case "HTACShell":
                PoolMaker = HTACShell;
                break;
            case "HTACExplosion":
                PoolMaker = HTACExplosion;
                break;

            case "APCAmmo":
                PoolMaker = APCAmmo;
                break;
            case "APCExplosion":
                PoolMaker = APCExplosion;
                break;

            case "FBWSAmmo":
                PoolMaker = FBWSAmmo;
                break;
            case "FBWSShell":
                PoolMaker = FBWSShell;
                break;

            case "OSEHSMissile":
                PoolMaker = OSEHSMissile;
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

    public GameObject RicoCHET()
    {
        for (int index = 0; index < ricochetEffect.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % ricochetEffect.Length;

            if (!ricochetEffect[index + ShellCursor].activeSelf)
            {
                ricochetEffect[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return ricochetEffect[index + ShellCursor];
            }
        }

        return null;
    }
}