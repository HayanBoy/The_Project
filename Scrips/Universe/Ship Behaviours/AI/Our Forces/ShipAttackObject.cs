using System.Collections;
using UnityEngine;

public class ShipAttackObject : MonoBehaviour
{
    public GameObject Turret1; //만약에 터렛이 없는 함선일 경우, 지정된 발사 좌표 오브젝트로 대체한다.
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject Turret5;
    public GameObject Turret6;
    private int AmountOfTurret; //포탑 갯수

    GameObject NarihaClusterMissile1;
    GameObject NarihaNuclearMissile1;

    private void Awake()
    {
        if (Turret1 != null)
            AmountOfTurret++;
        if (Turret2 != null)
            AmountOfTurret++;
        if (Turret3 != null)
            AmountOfTurret++;
        if (Turret4 != null)
            AmountOfTurret++;
        if (Turret5 != null)
            AmountOfTurret++;
        if (Turret6 != null)
            AmountOfTurret++;
    }

    //클러스터 미사일 스킬
    public IEnumerator TargetMissile1Fire(GameObject Target, float Time, int AmmoDamage, int DamageCount, string name, string ExplosionName)
    {
        yield return new WaitForSeconds(Time);

        int RandomTurret = Random.Range(0, AmountOfTurret + 1);

        if (RandomTurret == 0)
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret1, Target, AmmoDamage, name, ExplosionName);
            }
        }
        else if (RandomTurret == 1)
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret2, Target, AmmoDamage, name, ExplosionName);
            }
        }
        else if (RandomTurret == 2)
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret3, Target, AmmoDamage, name, ExplosionName);
            }
        }
        else if (RandomTurret == 3)
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret4, Target, AmmoDamage, name, ExplosionName);
            }
        }
        else if (RandomTurret == 4)
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret5, Target, AmmoDamage, name, ExplosionName);
            }
        }
        else
        {
            for (int i = 0; i < DamageCount; i++)
            {
                FireClusterMissile(Turret6, Target, AmmoDamage, name, ExplosionName);
            }
        }
    }

    void FireClusterMissile(GameObject Turret, GameObject Target, int AmmoDamage, string name, string ExplosionName)
    {
        NarihaClusterMissile1 = ShipAmmoObjectPool.instance.Loader(name);
        NarihaClusterMissile1.transform.position = Turret.transform.position;
        int RandomRadius = Random.Range(-120, 120);
        NarihaClusterMissile1.transform.eulerAngles = new Vector3(Turret.transform.eulerAngles.x, Turret.transform.eulerAngles.y, Turret.transform.eulerAngles.z - 90 + RandomRadius);
        NarihaClusterMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, Target, ExplosionName);
    }

    //핵 미사일 스킬
    public void NuclearMissile1Fire(GameObject Target, int AmmoDamage, string name, string ExplosionName)
    {
        NarihaNuclearMissile1 = ShipAmmoObjectPool.instance.Loader(name);
        int RandomTurret = Random.Range(0, AmountOfTurret + 1);

        if (RandomTurret == 0)
        {
            NarihaNuclearMissile1.transform.position = Turret1.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret1.transform.eulerAngles.x, Turret1.transform.eulerAngles.y, Turret1.transform.eulerAngles.z + 90);
        }
        else if (RandomTurret == 1)
        {
            NarihaNuclearMissile1.transform.position = Turret2.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret2.transform.eulerAngles.x, Turret2.transform.eulerAngles.y, Turret2.transform.eulerAngles.z + 90);
        }
        else if (RandomTurret == 2)
        {
            NarihaNuclearMissile1.transform.position = Turret3.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret3.transform.eulerAngles.x, Turret3.transform.eulerAngles.y, Turret3.transform.eulerAngles.z + 90);
        }
        else if (RandomTurret == 3)
        {
            NarihaNuclearMissile1.transform.position = Turret4.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret4.transform.eulerAngles.x, Turret4.transform.eulerAngles.y, Turret4.transform.eulerAngles.z + 90);
        }
        else if (RandomTurret == 4)
        {
            NarihaNuclearMissile1.transform.position = Turret5.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret5.transform.eulerAngles.x, Turret5.transform.eulerAngles.y, Turret5.transform.eulerAngles.z + 90);
        }
        else
        {
            NarihaNuclearMissile1.transform.position = Turret6.transform.position;
            NarihaNuclearMissile1.transform.eulerAngles = new Vector3(Turret6.transform.eulerAngles.x, Turret6.transform.eulerAngles.y, Turret6.transform.eulerAngles.z + 90);
        }
        NarihaNuclearMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, Target, ExplosionName);
    }
}