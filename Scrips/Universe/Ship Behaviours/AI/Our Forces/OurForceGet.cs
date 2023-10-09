using System.Collections;
using UnityEngine;

public class OurForceGet : MonoBehaviour
{
    public bool FlagshipWarp; //기함 워프 방식
    public bool isBattleSite; //전투 지역인가
    public bool isPlanet; //행성 지역인가

    public GameObject LocationBecon;
    public GameObject NarihaFlagShip;
    public GameObject NarihaFormationShip;
    public GameObject PlanetPrefab;

    private Vector3 NarihaWarpLocation; //적 워프 좌표 생성기
    private Vector3 NarihaShipLocation;
    private Vector3 LocationPoint;

    private void OnEnable()
    {
        GetEnemyWarpLocation();
    }

    //아군이 스폰 및 워프할 장소 가져오기
    public void GetEnemyWarpLocation()
    {
        if (FlagshipWarp == true)
        {
            float RandomMovement1 = Random.Range(-15, 15);
            if (RandomMovement1 > -10 && RandomMovement1 < 10)
            {
                while (RandomMovement1 < -10 && RandomMovement1 > 10)
                    RandomMovement1 = Random.Range(-15, 15);
            }
            float RandomMovement2 = Random.Range(-15, 15);
            if (RandomMovement2 > -10 && RandomMovement2 < 10)
            {
                while (RandomMovement2 < -10 && RandomMovement2 > 10)
                    RandomMovement2 = Random.Range(-15, 15);
            }
            NarihaWarpLocation = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, transform.position.z);
            GameObject GetLocationBecon = Instantiate(LocationBecon, NarihaWarpLocation, Quaternion.identity);

            float EnemyshipRange = Random.Range(500, 1000);
            NarihaShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), transform.position.z);

            LocationPoint = GetLocationBecon.transform.position;
        }

        EnemyAmount();
    }

    //아군 함대 수 정하기
    void EnemyAmount()
    {
        if (FlagshipWarp == true) //기함 중심 함대
        {
            GameObject EnemyFlagshipGet = Instantiate(NarihaFlagShip, NarihaShipLocation, Quaternion.identity);
            if (isBattleSite == true)
            {
                GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
            }

            int SpawnShips = Random.Range(4, 8);
            for (int i = 0; i < SpawnShips; i++)
            {
                GameObject EnemyFollowshipGet = Instantiate(NarihaFormationShip, NarihaShipLocation, Quaternion.identity);
                EnemyFollowshipGet.GetComponent<EnemyShipBehavior>().MyFlagship = EnemyFlagshipGet;
                EnemyFlagshipGet.GetComponent<EnemyFollowShipManager>().ShipList.Add(EnemyFollowshipGet);
                EnemyFlagshipGet.GetComponent<EnemyFollowShipManager>().ShipAccount++;
                EnemyFlagshipGet.GetComponent<EnemyWarpFormationControl>().GetFormation(EnemyFlagshipGet, i);
                if (isBattleSite == true)
                {
                    GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                }
            }
            StartCoroutine(FlagShipStartWarp(EnemyFlagshipGet));
            StartCoroutine(FollowShipStartWarp(EnemyFlagshipGet));
        }
        else //단독 편대함
            StartCoroutine(FollowShipSoloStartWarp());
    }

    //기함 워프
    IEnumerator FlagShipStartWarp(GameObject Flagship)
    {
        EnemyShipNavigator EnemyShipNavigator = Flagship.GetComponent<EnemyShipNavigator>();
        EnemyShipNavigator.MoveTo(LocationPoint);

        yield return new WaitForSeconds(0.1f);
        EnemyShipNavigator.WarpLocationGet(LocationPoint, Flagship.transform.rotation, Flagship);
    }

    //편대 함선 워프
    IEnumerator FollowShipStartWarp(GameObject Flagship)
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < Flagship.GetComponent<EnemyFollowShipManager>().ShipList.Count; i++)
        {
            EnemyShipNavigator EnemyShipNavigator = Flagship.GetComponent<EnemyFollowShipManager>().ShipList[i].GetComponent<EnemyShipNavigator>();
            EnemyShipNavigator.FollowShipWarpStart(true, 0);
            yield return new WaitForSeconds(0.1f);

            if (i >= Flagship.GetComponent<EnemyFollowShipManager>().ShipList.Count - 1) //가장 마지막에 기함워프 실시
            {
                yield return new WaitForSeconds(0.3f);
                EnemyShipNavigator EnemyShipNavigator2 = Flagship.GetComponent<EnemyShipNavigator>();
                EnemyShipNavigator2.FlagshipWarpStart(true);
            }
        }
    }

    //편대 함선만 단독으로 워프
    IEnumerator FollowShipSoloStartWarp()
    {
        if (isPlanet == true)
        {
            if (GetComponent<PlanetOurForceShipsManager>().FirstFree == false) //첫 해방으로 간주하여 함선이 워프하며 행성에 주둔한다.
            {
                float EnemyshipRange = Random.Range(1000, 1000);
                NarihaShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                    Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), transform.position.z);

                int SpawnShips = Random.Range(15, 20);

                for (int i = 0; i <= SpawnShips; i++)
                {
                    LocationPoint = new Vector3(Random.Range(transform.position.x + 40, transform.position.x - 40), Random.Range(transform.position.y + 40, transform.position.y - 40), transform.position.z);

                    GameObject OurFormationShipGet = Instantiate(NarihaFormationShip, NarihaShipLocation, Quaternion.identity);
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().FormationOn = false;
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().PlanetWalk = true;
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().MyPlanet = PlanetPrefab;
                    GetComponent<PlanetOurForceShipsManager>().NarihaOurForceShipList.Add(OurFormationShipGet);

                    OurForceShipNavigator OurForceShipNavigator = OurFormationShipGet.GetComponent<OurForceShipNavigator>();
                    OurForceShipNavigator.MoveTo(LocationPoint);

                    yield return new WaitForSeconds(0.1f);
                    OurForceShipNavigator.FollowShipWarpStart(true, 0);
                }
                GetComponent<PlanetOurForceShipsManager>().FirstFree = true;
            }
            else //이미 해방된 행성이므로 함선을 행성 주변에 스폰 시킨다.
            {
                int SpawnShips = Random.Range(15, 20);

                for (int i = 0; i <= SpawnShips; i++)
                {
                    LocationPoint = new Vector2(Random.Range(transform.position.x + 40, transform.position.x - 40), Random.Range(transform.position.y + 40, transform.position.y - 40));

                    GameObject OurFormationShipGet = Instantiate(NarihaFormationShip, LocationPoint, Quaternion.identity);
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().FormationOn = false;
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().PlanetWalk = true;
                    OurFormationShipGet.GetComponent<OurForceShipBehavior>().MyPlanet = PlanetPrefab;
                    GetComponent<PlanetOurForceShipsManager>().NarihaOurForceShipList.Add(OurFormationShipGet);
                }
            }
        }
        else if (isBattleSite == true) //전투 지역으로 아군 함선 스폰
        {
            float RandomMovement1 = 0;
            float RandomMovement2 = 0;
            int SpawnShips = Random.Range(3, 6);

            for (int i = 0; i <= SpawnShips; i++)
            {
                RandomMovement1 = Random.Range(-5, 5);
                if (RandomMovement1 > -2 && RandomMovement1 < 2)
                {
                    while (RandomMovement1 < -5 && RandomMovement1 > 5)
                        RandomMovement1 = Random.Range(-5, 5);
                }
                RandomMovement2 = Random.Range(-5, 5);
                if (RandomMovement2 > -2 && RandomMovement2 < 2)
                {
                    while (RandomMovement2 < -5 && RandomMovement2 > 5)
                        RandomMovement2 = Random.Range(-5, 5);
                }
                LocationPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, transform.position.z);

                GameObject OurFormationShipGet = Instantiate(NarihaFormationShip, LocationPoint, Quaternion.identity);
                OurFormationShipGet.GetComponent<OurForceShipBehavior>().FormationOn = false;
                if (isBattleSite == true)
                {
                    GetComponent<RandomSiteBattle>().NarihaOurForceShipList.Add(OurFormationShipGet);
                    OurFormationShipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                    OurFormationShipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                    OurFormationShipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    OurFormationShipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                }
            }
        }
    }
}