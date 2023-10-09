using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GunController GunController;
    public static ObjectManager instance = null;

    //ÇÃ·¹ÀÌ¾î ÅºÇÇ
    public GameObject DT37AmmoPrefab;
    public GameObject DT37ShellPrefab;
    public GameObject DS65ShellPrefab;
    public GameObject DP9007ShellPrefab;
    public GameObject CGD27ShellPrefab;

    public int HeavyWeaponType;
    public int subGunAmmo;

    //ÇÃ·¹ÀÌ¾î Åº¾Ë
    public GameObject PlayerSniperAmmoPrefab; // Àú°ÝÃÑ ÃÑ¾Ë
    public GameObject PlayerShotGunAmmoPrefab; // ¼¦°Ç ÃÑ¾Ë
    public GameObject PlayerSMGAmmoPrefab; // ±â°ü´ÜÃÑ(Double Gun) ÃÑ¾Ë
    public GameObject RobotAmmoPrefab;
    public GameObject MiniGunAmmoPrefab;
    public GameObject MiniGunShellPrefab;
    public GameObject ASC365AmmoPrefab;
    public GameObject CannonDropPrefab; //±â°üÆ÷ Æø°Ý Åº¾Ë

    //ÇÃ·¹ÀÌ¾î ±âÅ¸ ¹ß»çÃ¼
    public GameObject OSEHMissilePrefab;

    //ÀÌÆåÆ®
    public GameObject ricochetEffectPrefab;
    public GameObject VM5ExplosionPrefab;
    public GameObject HydraSeparatingFirePrefab;
    public GameObject HydraExplosionPrefab;
    public GameObject LaserMissileExplosionPrefab;
    public GameObject OSEH302ExplosionPrefab;
    public GameObject OSEH302ExplosionPrefab2;

    GameObject[] DT37Shell;
    GameObject[] DS65Shell;
    GameObject[] DP9007Shell;
    GameObject[] CGD27Shell;

    GameObject[] DT37Ammo;
    GameObject[] PlayerSniperAmmo;
    GameObject[] PlayerShotGunAmmo;
    GameObject[] PlayerSMGAmmo;

    GameObject[] RobotAmmo;
    GameObject[] MiniGunAmmo;
    GameObject[] MiniGunShell;
    GameObject[] ASC365Ammo;

    GameObject[] CannonDrop;

    GameObject[] OSEHMissile;

    GameObject[] ricochetEffect;
    GameObject[] VM5Explosion;
    GameObject[] HydraSeparatingFire;
    GameObject[] HydraExplosion;
    GameObject[] LaserMissileExplosion;
    GameObject[] OSEH302Explosion;
    GameObject[] OSEH302Explosion2;

    GameObject[] PoolMaker;

    int ShellCursor;

    public List<GameObject> SupplyList = new List<GameObject>();

    void Start()
    {
        if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5000)
            HeavyWeaponType = 1;
        else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
            HeavyWeaponType = 2;

        if (GunController.GunType == 1)
            DT37Shell = new GameObject[150];
        else if (GunController.GunType == 1000)
            DS65Shell = new GameObject[50];
        else if (GunController.GunType == 2000)
            DP9007Shell = new GameObject[50];
        else if (GunController.GunType == 0)
        {
            if (GunController.SubGunTypeFront == 1)
                CGD27Shell = new GameObject[subGunAmmo];
            if (GunController.SubGunTypeBack == 1)
                CGD27Shell = new GameObject[subGunAmmo];
        }

        if (GunController.GunType == 1)
            DT37Ammo = new GameObject[50];
        else if (GunController.GunType == 1000)
            PlayerShotGunAmmo = new GameObject[30];
        else if (GunController.GunType == 2000)
            PlayerSniperAmmo = new GameObject[15];
        else if (GunController.GunType == 0)
        {
            if (GunController.SubGunTypeFront == 1)
                PlayerSMGAmmo = new GameObject[50];
            if (GunController.SubGunTypeBack == 1)
                PlayerSMGAmmo = new GameObject[50];
        }

        if (HeavyWeaponType == 1)
        {
            MiniGunAmmo = new GameObject[50];
            MiniGunShell = new GameObject[400];
        }
        else if (HeavyWeaponType == 2)
            ASC365Ammo = new GameObject[50];
        RobotAmmo = new GameObject[20];
        CannonDrop = new GameObject[38];

        OSEHMissile = new GameObject[10];

        ricochetEffect = new GameObject[30];
        VM5Explosion = new GameObject[3];
        HydraSeparatingFire = new GameObject[5];
        HydraExplosion = new GameObject[10];
        LaserMissileExplosion = new GameObject[15];
        OSEH302Explosion = new GameObject[20];
        OSEH302Explosion2 = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        if (GunController.GunType == 1)
        {
            for (int index = 0; index < DT37Shell.Length; index++)
            {
                DT37Shell[index] = Instantiate(DT37ShellPrefab);
                DT37Shell[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 1000)
        {
            for (int index = 0; index < DS65Shell.Length; index++)
            {
                DS65Shell[index] = Instantiate(DS65ShellPrefab);
                DS65Shell[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 2000)
        {
            for (int index = 0; index < DP9007Shell.Length; index++)
            {
                DP9007Shell[index] = Instantiate(DP9007ShellPrefab);
                DP9007Shell[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 0)
        {
            for (int index = 0; index < CGD27Shell.Length; index++)
            {
                CGD27Shell[index] = Instantiate(CGD27ShellPrefab);
                CGD27Shell[index].SetActive(false);
            }
        }

        if (GunController.GunType == 1)
        {
            for (int index = 0; index < DT37Ammo.Length; index++)
            {
                DT37Ammo[index] = Instantiate(DT37AmmoPrefab);
                DT37Ammo[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 1000)
        {
            for (int index = 0; index < PlayerShotGunAmmo.Length; index++)
            {
                PlayerShotGunAmmo[index] = Instantiate(PlayerShotGunAmmoPrefab);
                PlayerShotGunAmmo[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 2000)
        {
            for (int index = 0; index < PlayerSniperAmmo.Length; index++)
            {
                PlayerSniperAmmo[index] = Instantiate(PlayerSniperAmmoPrefab);
                PlayerSniperAmmo[index].SetActive(false);
            }
        }

        else if (GunController.GunType == 0)
        {
            for (int index = 0; index < PlayerSMGAmmo.Length; index++)
            {
                PlayerSMGAmmo[index] = Instantiate(PlayerSMGAmmoPrefab);
                PlayerSMGAmmo[index].SetActive(false);
            }
        }

        if (HeavyWeaponType == 1)
        {
            for (int index = 0; index < MiniGunAmmo.Length; index++)
            {
                MiniGunAmmo[index] = Instantiate(MiniGunAmmoPrefab);
                MiniGunAmmo[index].SetActive(false);
            }
            for (int index = 0; index < MiniGunShell.Length; index++)
            {
                MiniGunShell[index] = Instantiate(MiniGunShellPrefab);
                MiniGunShell[index].SetActive(false);
            }
        }

        else if (HeavyWeaponType == 2)
        {
            for (int index = 0; index < ASC365Ammo.Length; index++)
            {
                ASC365Ammo[index] = Instantiate(ASC365AmmoPrefab);
                ASC365Ammo[index].SetActive(false);
            }
        }

        for (int index = 0; index < CannonDrop.Length; index++)
        {
            CannonDrop[index] = Instantiate(CannonDropPrefab);
            CannonDrop[index].SetActive(false);
        }

        for (int index = 0; index < OSEHMissile.Length; index++)
        {
            OSEHMissile[index] = Instantiate(OSEHMissilePrefab);
            OSEHMissile[index].SetActive(false);
        }

        for (int index = 0; index < ricochetEffect.Length; index++)
        {
            ricochetEffect[index] = Instantiate(ricochetEffectPrefab);
            ricochetEffect[index].SetActive(false);
        }

        for (int index = 0; index < VM5Explosion.Length; index++)
        {
            VM5Explosion[index] = Instantiate(VM5ExplosionPrefab);
            VM5Explosion[index].SetActive(false);
        }

        for (int index = 0; index < HydraSeparatingFire.Length; index++)
        {
            HydraSeparatingFire[index] = Instantiate(HydraSeparatingFirePrefab);
            HydraSeparatingFire[index].SetActive(false);
        }

        for (int index = 0; index < HydraExplosion.Length; index++)
        {
            HydraExplosion[index] = Instantiate(HydraExplosionPrefab);
            HydraExplosion[index].SetActive(false);
        }

        for (int index = 0; index < LaserMissileExplosion.Length; index++)
        {
            LaserMissileExplosion[index] = Instantiate(LaserMissileExplosionPrefab);
            LaserMissileExplosion[index].SetActive(false);
        }

        for (int index = 0; index < OSEH302Explosion.Length; index++)
        {
            OSEH302Explosion[index] = Instantiate(OSEH302ExplosionPrefab);
            OSEH302Explosion[index].SetActive(false);
        }

        for (int index = 0; index < OSEH302Explosion2.Length; index++)
        {
            OSEH302Explosion2[index] = Instantiate(OSEH302ExplosionPrefab2);
            OSEH302Explosion2[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "DT37Shell":
                PoolMaker = DT37Shell;
                break;

            case "DS65Shell":
                PoolMaker = DS65Shell;
                break;

            case "DP9007Shell":
                PoolMaker = DP9007Shell;
                break;

            case "CGD27Shell":
                PoolMaker = CGD27Shell;
                break;

            case "DT37Ammo":
                PoolMaker = DT37Ammo;
                break;

            case "PlayerSniperAmmo":
                PoolMaker = PlayerSniperAmmo;
                break;

            case "PlayerShotGunAmmo":
                PoolMaker = PlayerShotGunAmmo;
                break;

            case "PlayerSMGAmmo":
                PoolMaker = PlayerSMGAmmo;
                break;

            case "MiniGunAmmo":
                PoolMaker = MiniGunAmmo;
                break;

            case "MiniGunShell":
                PoolMaker = MiniGunShell;
                break;

            case "RobotAmmo":
                PoolMaker = RobotAmmo;
                break;

            case "OSEHMissile":
                PoolMaker = OSEHMissile;
                break;

            case "ricochetEffect":
                PoolMaker = ricochetEffect;
                break;

            case "VM5Explosion":
                PoolMaker = VM5Explosion;
                break;

            case "HydraSeparatingFire":
                PoolMaker = HydraSeparatingFire;
                break;

            case "HydraExplosion":
                PoolMaker = HydraExplosion;
                break;

            case "LaserMissileExplosion":
                PoolMaker = LaserMissileExplosion;
                break;

            case "OSEH302Explosion":
                PoolMaker = OSEH302Explosion;
                break;

            case "OSEH302Explosion2":
                PoolMaker = OSEH302Explosion2;
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

    public GameObject LoaderMiniGun()
    {
        for (int index = 0; index < MiniGunAmmo.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % MiniGunAmmo.Length;

            if (!MiniGunAmmo[index + ShellCursor].activeSelf)
            {
                MiniGunAmmo[index + ShellCursor].SetActive(true);
                return MiniGunAmmo[index + ShellCursor];
            }
        }

        return null;
    }

    public GameObject LoaderFlameGun()
    {
        for (int index = 0; index < ASC365Ammo.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % ASC365Ammo.Length;

            if (!ASC365Ammo[index + ShellCursor].activeSelf)
            {
                ASC365Ammo[index + ShellCursor].SetActive(true);
                return ASC365Ammo[index + ShellCursor];
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

    public GameObject CannonDropPool()
    {
        for (int index = 0; index < CannonDrop.Length; index++)
        {
            ShellCursor = (ShellCursor + 1) % CannonDrop.Length;

            if (!CannonDrop[index + ShellCursor].activeSelf)
            {
                CannonDrop[index + ShellCursor].SetActive(true);
                //ShellDropAni.Acceleration();
                return CannonDrop[index + ShellCursor];
            }
        }

        return null;
    }
}