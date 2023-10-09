using UnityEngine;

public class EnemyShipLevelInformation : MonoBehaviour
{
    public bool isNariha;
    public bool Selected; //이 함선이 선택되었을 경우에만 체크
    public int Level;
    public int ShipNumber;
    public bool isBattleSite; //전투 지역인가
    public bool isPlanet; //행성 지역인가
    public bool isStar; //항성 지역인가
    public int BattleSiteNumber;
    public int PlanetNumber;
    public GameObject Zone; //지금 있는 지역. 전투가 끝난 뒤의 상호작용을 위함

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (collision.CompareTag("Toropio System") || collision.CompareTag("Roro System"))
            {
                Level = 1;
            }
            else if (collision.CompareTag("Sarisi System") || collision.CompareTag("Garix System") || collision.CompareTag("OctoKrasis Patoro System"))
            {
                Level = 2;
            }
            else if (collision.CompareTag("Delta D31-402054 System") || collision.CompareTag("Jerato O95-99024 System"))
            {
                Level = 3;
            }

            if (isNariha == false)
            {
                if (GetComponent<EnemyShipBehavior>().FlagShip == true)
                {
                    GetComponent<HullSloriusFlagship1>().EnemyLevelPatch();

                    int RandomCannonType = Random.Range(0, 4);
                    if (RandomCannonType == 0)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                    }
                    else if (RandomCannonType == 1)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                    }
                    else if (RandomCannonType == 3)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                    }
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else
                {
                    GetComponent<HullSloriusFormationShip1>().EnemyLevelPatch();

                    int RandomCannonType = Random.Range(0, 4);
                    if (RandomCannonType == 0)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 1;
                    }
                    else if (RandomCannonType == 1)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 2;
                    }
                    else if (RandomCannonType == 3)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonStyle = 3;
                    }
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonReinput();
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
            }
            else
            {
                GetComponent<HullSloriusFormationShip1>().UpgradePatch();

                int RandomCannonType = Random.Range(0, 4);
                if (RandomCannonType == 0)
                {
                    GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().CannonStyle = 1;
                    GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().CannonStyle = 1;
                }
                else if (RandomCannonType == 1)
                {
                    GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().CannonStyle = 2;
                    GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().CannonStyle = 2;
                }
                else if (RandomCannonType == 3)
                {
                    GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().CannonStyle = 3;
                    GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().CannonStyle = 3;
                }
                GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().CannonReinput();
                GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().CannonReinput();
            }
        }
    }
}