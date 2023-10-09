using System.Collections;
using UnityEngine;

public class EnemyGet : MonoBehaviour
{
    [Header("스폰 유형")]
    public bool FlagshipWarp; //기함 워프 방식
    public int WarpControsType; //워프할 컨트로스 종족타입. 1 = 슬로리어스, 2 = 칸타크리, 3 = 슬로리어스 기함을 중심으로 슬로리어스 편대함과 칸타크리 편대함
    public bool isBattleSite; //전투 지역인가
    public bool isPlanet; //행성 지역인가
    public bool isStar; //항성 지역인가
    public int isSupportEnemy; //적 증원인가(0은 지원 없음. 1 이상부터 레벨 적용)

    [Header("스폰 난이도")]
    public int MaxFlagship; //최대 기함 스폰 수
    public int MaxFormationShip; //최대 편대함 스폰 수

    [Header("스폰 함선과 장소")]
    public GameObject LocationBecon;
    public GameObject SloriusFlagShip;
    public GameObject SloriusFollowShip;
    public GameObject KantakriFlagShip;
    public GameObject KantakriFollowShip;

    [Header("스폰 좌표")]
    private Vector3 EnemyWarpLocation; //적 워프 좌표 생성기
    private Vector3 EnemyShipLocation;
    private Vector3 PlayerFlagshipPoint;
    public Vector3 WarpFleetDestination; //워프하는 아군 함대의 목적지

    private void OnEnable()
    {
        GetEnemyWarpLocation();
    }

    //적이 스폰 및 워프할 장소 가져오기
    public void GetEnemyWarpLocation()
    {
        if (FlagshipWarp == true)
        {
            if (isPlanet == false)
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
                EnemyWarpLocation = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, 0);
                GameObject GetLocationBecon = Instantiate(LocationBecon, EnemyWarpLocation, Quaternion.identity);

                float EnemyshipRange = Random.Range(500, 1000);
                EnemyShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                    Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), 0);

                PlayerFlagshipPoint = GetLocationBecon.transform.position;
            }
            else if (isPlanet == true) //점령된 행성에 나리하 기함이 접근하면 워프를 통해 출현
            {
                float EnemyshipRange = Random.Range(500, 500);
                EnemyShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                    Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), 0);

                PlayerFlagshipPoint = new Vector3(Random.Range(transform.position.x + 40, transform.position.x - 40), Random.Range(transform.position.y + 40, transform.position.y - 40), 0);
            }
            else if (isStar == true) //점령된 항성에 나리하 기함이 접근하면 워프를 통해 출현
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

                float EnemyshipRange = Random.Range(500, 500);
                EnemyShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                    Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), 0);

                PlayerFlagshipPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, 0);
            }
        }

        EnemyAmount();
    }

    //적 유형 및 함대 수 정하기
    void EnemyAmount()
    {
        if (FlagshipWarp == true) //기함 중심 함대
        {
            int RandomFlagshipAdd = MaxFlagship;
            if (isSupportEnemy == 0)
                RandomFlagshipAdd = MaxFlagship;
            else if (isSupportEnemy == 1 || isSupportEnemy == 2)
                RandomFlagshipAdd = 1;
            for (int y = 0; y < RandomFlagshipAdd; y++)
            {
                if (WarpControsType == 1) //슬로리어스 함대
                {
                    GameObject EnemyFlagshipGet = Instantiate(SloriusFlagShip, EnemyShipLocation, Quaternion.identity);
                    AIShipManager.instance.EnemiesFlagShipList.Add(EnemyFlagshipGet);
                    AIShipManager.instance.SloriusFlagShipList.Add(EnemyFlagshipGet);
                    if (isBattleSite == true)
                    {
                        GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                    }
                    else if (isPlanet == true)
                    {
                        GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }
                    else if (isStar == true)
                    {
                        GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }

                    int SpawnShips = MaxFormationShip;
                    if (isSupportEnemy == 0)
                        SpawnShips = MaxFormationShip;
                    else if (isSupportEnemy == 1)
                        SpawnShips = Random.Range(2, 4);
                    else if (isSupportEnemy == 2)
                        SpawnShips = Random.Range(4, 6);

                    for (int i = 0; i < SpawnShips; i++)
                    {
                        GameObject EnemyFollowshipGet = Instantiate(SloriusFollowShip, EnemyShipLocation, Quaternion.identity);
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
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                        }
                        else if (isPlanet == true)
                        {
                            GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        }
                        else if (isStar == true)
                        {
                            GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        }
                    }
                    StartCoroutine(FlagShipStartWarp(EnemyFlagshipGet));
                    StartCoroutine(FollowShipStartWarp(EnemyFlagshipGet));
                }
                else if (WarpControsType == 2) //칸타크리 함대
                {
                    GameObject EnemyFlagshipGet = Instantiate(KantakriFlagShip, EnemyShipLocation, Quaternion.identity);
                    AIShipManager.instance.EnemiesFlagShipList.Add(EnemyFlagshipGet);
                    AIShipManager.instance.KantakriFlagShipList.Add(EnemyFlagshipGet);
                    if (isBattleSite == true)
                    {
                        GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                    }
                    else if (isPlanet == true)
                    {
                        GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }
                    else if (isStar == true)
                    {
                        GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }

                    int SpawnShips = MaxFormationShip;
                    if (isSupportEnemy == 0)
                        SpawnShips = MaxFormationShip;
                    else if (isSupportEnemy == 1)
                        SpawnShips = Random.Range(2, 4);
                    else if (isSupportEnemy == 2)
                        SpawnShips = Random.Range(4, 6);
                    for (int i = 0; i < SpawnShips; i++)
                    {
                        GameObject EnemyFollowshipGet = Instantiate(KantakriFollowShip, EnemyShipLocation, Quaternion.identity);
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
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                        }
                        else if (isPlanet == true)
                        {
                            GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;

                        }
                        else if (isStar == true)
                        {
                            GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                            EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        }
                    }
                    StartCoroutine(FlagShipStartWarp(EnemyFlagshipGet));
                    StartCoroutine(FollowShipStartWarp(EnemyFlagshipGet));
                }
                else if (WarpControsType == 3) //슬로리어스 기함 + 슬로리어스 편대함과 칸타크리 편대함 함대
                {
                    GameObject EnemyFlagshipGet = Instantiate(SloriusFlagShip, EnemyShipLocation, Quaternion.identity);
                    AIShipManager.instance.EnemiesFlagShipList.Add(EnemyFlagshipGet);
                    AIShipManager.instance.SloriusFlagShipList.Add(EnemyFlagshipGet);
                    if (isBattleSite == true)
                    {
                        GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                    }
                    else if (isPlanet == true)
                    {
                        GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }
                    else if (isStar == true)
                    {
                        GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFlagshipGet);
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                        EnemyFlagshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }

                    int SpawnShips = MaxFormationShip;
                    if (isSupportEnemy == 0)
                        SpawnShips = MaxFormationShip;
                    else if (isSupportEnemy == 1)
                        SpawnShips = Random.Range(2, 4);
                    else if (isSupportEnemy == 2)
                        SpawnShips = Random.Range(4, 6);
                    for (int i = 0; i < SpawnShips; i++)
                    {
                        int RandomFollowShip = Random.Range(0, 2);
                        if (RandomFollowShip == 0)
                        {
                            GameObject EnemyFollowshipGet = Instantiate(SloriusFollowShip, EnemyShipLocation, Quaternion.identity);
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
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                            }
                            else if (isPlanet == true)
                            {
                                GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                            }
                            else if (isStar == true)
                            {
                                GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                            }
                        }
                        else
                        {
                            GameObject EnemyFollowshipGet = Instantiate(KantakriFollowShip, EnemyShipLocation, Quaternion.identity);
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
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                            }
                            else if (isPlanet == true)
                            {
                                GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                            }
                            else if (isStar == true)
                            {
                                GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                                EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                            }
                        }
                    }
                    StartCoroutine(FlagShipStartWarp(EnemyFlagshipGet));
                    StartCoroutine(FollowShipStartWarp(EnemyFlagshipGet));
                }
            }
            isSupportEnemy = 0;
        }
        else //단독 편대함
            StartCoroutine(FollowShipSoloStartWarp());
    }

    //기함 워프
    IEnumerator FlagShipStartWarp(GameObject Flagship)
    {
        EnemyShipNavigator EnemyShipNavigator = Flagship.GetComponent<EnemyShipNavigator>();
        EnemyShipNavigator.MoveTo(PlayerFlagshipPoint);

        yield return new WaitForSeconds(0.1f);
        EnemyShipNavigator.WarpLocationGet(PlayerFlagshipPoint, Flagship.transform.rotation, Flagship);
    }

    //소속 함선 워프
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
        float EnemyshipRange = Random.Range(500, 1000);
        EnemyShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
            Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), 0);

        int RandomArea = Random.Range(0, 2);
        float RandomMovement1 = 0;
        float RandomMovement2 = 0;

        if (RandomArea == 1)
            RandomMovement2 = Random.Range(-10, 10);

        yield return new WaitForSeconds(1);

        int SpawnShips = MaxFormationShip;
        if (isSupportEnemy == 0)
            SpawnShips = MaxFormationShip;
        else if (isSupportEnemy == 1)
            SpawnShips = Random.Range(2, 4);
        else if (isSupportEnemy == 2)
            SpawnShips = Random.Range(4, 6);
        for (int i = 0; i < SpawnShips; i++)
        {
            if (RandomArea == 0)
            {
                if (isPlanet == false)
                {
                    RandomMovement1 = Random.Range(-10, 10);
                    if (RandomMovement1 > -7 && RandomMovement1 < 7)
                    {
                        while (RandomMovement1 < -7 && RandomMovement1 > 7)
                            RandomMovement1 = Random.Range(-10, 10);
                    }
                    RandomMovement2 = Random.Range(-10, 10);
                    if (RandomMovement2 > -7 && RandomMovement2 < 7)
                    {
                        while (RandomMovement2 < -7 && RandomMovement2 > 7)
                            RandomMovement2 = Random.Range(-10, 10);
                    }
                    PlayerFlagshipPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, 0);
                }
                else if (isPlanet == true) //점령된 행성에 나리하 기함이 접근하면 워프를 통해 출현
                {
                    PlayerFlagshipPoint = new Vector3(Random.Range(transform.position.x + 40, transform.position.x - 40), Random.Range(transform.position.y + 40, transform.position.y - 40), 0);
                }
                else if (isStar == true) //점령된 항성에 나리하 기함이 접근하면 워프를 통해 출현
                {
                    PlayerFlagshipPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, 0);
                }
            }
            else
            {
                if (isPlanet == false)
                {
                    RandomMovement1 = Random.Range(-10, 10);
                    float RandomMovement3 = Random.Range(-5, 5);
                    PlayerFlagshipPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2 + RandomMovement3, 0);
                }
                else if (isPlanet == true) //점령된 행성에 나리하 기함이 접근하면 워프를 통해 출현
                {
                    PlayerFlagshipPoint = new Vector3(Random.Range(transform.position.x + 40, transform.position.x - 40), Random.Range(transform.position.y + 40, transform.position.y - 40), 0);
                }
                else if (isStar == true) //점령된 항성에 나리하 기함이 접근하면 워프를 통해 출현
                {
                    PlayerFlagshipPoint = new Vector3(transform.position.x + RandomMovement1, transform.position.y + RandomMovement2, 0);
                }
            }

            if (WarpControsType == 1)
            {
                GameObject EnemyFollowshipGet = Instantiate(SloriusFollowShip, EnemyShipLocation, Quaternion.identity);
                EnemyFollowshipGet.GetComponent<EnemyShipBehavior>().FormationOn = false;
                AIShipManager.instance.EnemiesFormationShipList.Add(EnemyFollowshipGet);
                if (isBattleSite == true)
                {
                    GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                }
                else if (isPlanet == true)
                {
                    GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                }
                else if (isStar == true)
                {
                    GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                }

                EnemyShipNavigator EnemyShipNavigator = EnemyFollowshipGet.GetComponent<EnemyShipNavigator>();
                EnemyShipNavigator.MoveTo(PlayerFlagshipPoint);

                yield return new WaitForSeconds(0.1f);
                EnemyShipNavigator.FollowShipWarpStart(true, 0);
            }
            else if (WarpControsType == 2)
            {
                GameObject EnemyFollowshipGet = Instantiate(KantakriFollowShip, EnemyShipLocation, Quaternion.identity);
                EnemyFollowshipGet.GetComponent<EnemyShipBehavior>().FormationOn = false;
                AIShipManager.instance.EnemiesFormationShipList.Add(EnemyFollowshipGet);
                if (isBattleSite == true)
                {
                    GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                }
                else if (isPlanet == true)
                {
                    GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                }
                else if (isStar == true)
                {
                    GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                    EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                }

                EnemyShipNavigator EnemyShipNavigator = EnemyFollowshipGet.GetComponent<EnemyShipNavigator>();
                EnemyShipNavigator.MoveTo(PlayerFlagshipPoint);

                yield return new WaitForSeconds(0.1f);
                EnemyShipNavigator.FollowShipWarpStart(true, 0);
            }
            else if (WarpControsType == 3)
            {
                int RandomFollowShip = Random.Range(0, 2);
                if (RandomFollowShip == 0)
                {
                    GameObject EnemyFollowshipGet = Instantiate(SloriusFollowShip, EnemyShipLocation, Quaternion.identity);
                    EnemyFollowshipGet.GetComponent<EnemyShipBehavior>().FormationOn = false;
                    AIShipManager.instance.EnemiesFormationShipList.Add(EnemyFollowshipGet);
                    if (isBattleSite == true)
                    {
                        GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                    }
                    else if (isPlanet == true)
                    {
                        GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }
                    else if (isStar == true)
                    {
                        GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }

                    EnemyShipNavigator EnemyShipNavigator = EnemyFollowshipGet.GetComponent<EnemyShipNavigator>();
                    EnemyShipNavigator.MoveTo(PlayerFlagshipPoint);

                    yield return new WaitForSeconds(0.1f);
                    EnemyShipNavigator.FollowShipWarpStart(true, 0);
                }
                else
                {
                    GameObject EnemyFollowshipGet = Instantiate(KantakriFollowShip, EnemyShipLocation, Quaternion.identity);
                    EnemyFollowshipGet.GetComponent<EnemyShipBehavior>().FormationOn = false;
                    AIShipManager.instance.EnemiesFormationShipList.Add(EnemyFollowshipGet);
                    if (isBattleSite == true)
                    {
                        GetComponent<RandomSiteBattle>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isBattleSite = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<RandomSiteBattle>().BattleSiteNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().isInFight = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone.gameObject.GetComponent<RandomSiteBattle>().EnemySpawn = true;
                    }
                    else if (isPlanet == true)
                    {
                        GetComponent<PlanetOurForceShipsManager>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isPlanet = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<PlanetOurForceShipsManager>().PlanetNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }
                    else if (isStar == true)
                    {
                        GetComponent<StarBattleSystem>().BattleEnemyShipList.Add(EnemyFollowshipGet);
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().isStar = true;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = GetComponent<StarBattleSystem>().StarNumber;
                        EnemyFollowshipGet.GetComponent<EnemyShipLevelInformation>().Zone = this.gameObject;
                    }

                    EnemyShipNavigator EnemyShipNavigator = EnemyFollowshipGet.GetComponent<EnemyShipNavigator>();
                    EnemyShipNavigator.MoveTo(PlayerFlagshipPoint);

                    yield return new WaitForSeconds(0.1f);
                    EnemyShipNavigator.FollowShipWarpStart(true, 0);
                }
            }
        }
        isSupportEnemy = 0;
    }
}