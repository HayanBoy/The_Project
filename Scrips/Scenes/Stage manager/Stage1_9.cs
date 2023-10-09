using System.Collections;
using Cinemachine;
using UnityEngine;

public class Stage1_9 : MonoBehaviour
{
    [Header("스크립트")]
    public SpawnSiteGenerator SpawnSiteGenerator;
    public SpawnLevelType SpawnLevelType;
    public ClearLine ClearLine;
    public StageDefence StageDefence;
    public BombSettings BombSettings;

    Coroutine spawnStep;

    public enum MissionArea
    {
        Planet,
        EnemyFlagship
    }
    [Header("미션 지역")]
    public MissionArea MissionAreaType;

    [Header("스폰맵 위치")]
    public GameObject GoProcessBar;
    public Transform SpawnMap1;
    public Transform SpawnMap2;
    public Transform SpawnMap3;
    public Transform SpawnMap4;
    public Transform SpawnMap5;
    public Transform SpawnMap6;
    public Transform SpawnMap7;
    public Transform SpawnMap8;
    public Transform SpawnMap9;
    public Transform SpawnMap10;

    [Header("카메라 고정")]
    public PolygonCollider2D CamSpawnMap1;
    public PolygonCollider2D CamSpawnMap2;
    public PolygonCollider2D CamSpawnMap3;
    public PolygonCollider2D CamSpawnMap4;
    public PolygonCollider2D CamSpawnMap5;
    public PolygonCollider2D CamSpawnMap6;
    public PolygonCollider2D CamSpawnMap7;
    public PolygonCollider2D CamSpawnMap8;
    public PolygonCollider2D CamSpawnMap9;
    public PolygonCollider2D CamSpawnMap10;
    public PolygonCollider2D Map;
    public PolygonCollider2D EndMap;

    [Header("플레이어 이동 제한 영역")]
    public CinemachineConfiner Cam;
    public GameObject SpawnCollider1;
    public GameObject SpawnCollider2;
    public GameObject SpawnCollider3;
    public GameObject SpawnCollider4;
    public GameObject SpawnCollider5;
    public GameObject SpawnCollider6;
    public GameObject SpawnCollider7;
    public GameObject SpawnCollider8;
    public GameObject SpawnCollider9;
    public GameObject SpawnCollider10;
    public GameObject EndCollider;

    [Header("외부 영역의 적 삭제")]
    public GameObject DeleteBoxCollider1;
    public GameObject DeleteBoxCollider2;
    public GameObject DeleteBoxCollider3;
    public GameObject DeleteBoxCollider4;
    public GameObject DeleteBoxCollider5;
    public GameObject DeleteBoxCollider6;
    public GameObject DeleteBoxCollider7;
    public GameObject DeleteBoxCollider8;
    public GameObject DeleteBoxCollider9;
    public GameObject DeleteBoxCollider10;
    public GameObject DefenceDeleteBoxCollider1;
    public int ActiveDeleteNumber; //스폰 콜라이더 작동시, 해당 스폰 지역의 모든 적이 스폰된 이후 지정된 삭제 스폰 지역을 고르기 위한 용도. 스폰번호에 맞게 적는다.
    public float ActiveDeleteTime = 6;

    [Header("스폰 단계")]
    public int ClearSpawnStep; //정해진 스폰 번호를 클리어하면 클리어 라인이 발동
    public bool KillStepOne = false; //적이 한번 나오는 곳에서 적을 모두 처치하면 다음 적을 내보내기 위한 스위치
    public bool SpawnRunning = false; //적이 스폰되는 구간에서 적이 스폰을 시작한 직후부터 적을 해당 구간에서 모두 처치했을 때에 사용하기 위한 스위치
    public bool Step1 = false; //스폰 1의 적을 모두 처치했을 때의 스위치
    public bool Step2 = false;
    public bool Step3 = false;
    public bool Step4 = false;
    public bool Step5 = false;
    public bool Step6 = false;

    private int SpawnCountOnce; //스폰 한번에 적이 연속적으로 몇 번 나오는지에 대한 횟수. 2번 이상부터 첫 스폰 후, 랜덤시간 뒤에 두번째 스폰이 발생한다.
    private bool SpawnStop = false;

    void Update()
    {
        if (SpawnRunning == true && ScoreManager.instance.AllCnt <= 0 && ScoreManager.instance.EnemyList.Count <= 0) //스폰의 1 사이클 상태에서 적들을 모두 잡았을 경우, 다음 단계로 보낸다.
        {
            ScoreManager.instance.AllCnt = 0;
            SpawnRunning = false;
            KillStepOne = true;

            if (BattleSave.Save1.MissionType == 2 || BattleSave.Save1.MissionType == 4)
            {
                DefenceDeleteBoxCollider1.SetActive(false);
            }
        }

        if (Cam.m_Damping > 0)
            Cam.m_Damping -= Time.deltaTime;
        if (Cam.m_Damping < 0)
            Cam.m_Damping = 0;
    }

    //적 삭제 영역 켜기
    IEnumerator ActiveDeleteBox()
    {
        yield return new WaitForSeconds(ActiveDeleteTime);

        if (KillStepOne == false)
        {
            if (BattleSave.Save1.MissionType == 1 || BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 100 || BattleSave.Save1.MissionType == 101)
            {
                if (ActiveDeleteNumber == 1)
                    DeleteBoxCollider1.SetActive(true);
                else if (ActiveDeleteNumber == 2)
                    DeleteBoxCollider2.SetActive(true);
                else if (ActiveDeleteNumber == 3)
                    DeleteBoxCollider3.SetActive(true);
                else if (ActiveDeleteNumber == 4)
                    DeleteBoxCollider4.SetActive(true);
                else if (ActiveDeleteNumber == 5)
                    DeleteBoxCollider5.SetActive(true);
                else if (ActiveDeleteNumber == 6)
                    DeleteBoxCollider6.SetActive(true);
                else if (ActiveDeleteNumber == 7)
                    DeleteBoxCollider7.SetActive(true);
                else if (ActiveDeleteNumber == 8)
                    DeleteBoxCollider8.SetActive(true);
                else if (ActiveDeleteNumber == 9)
                    DeleteBoxCollider9.SetActive(true);
                else if (ActiveDeleteNumber == 10)
                    DeleteBoxCollider10.SetActive(true);
            }
            else if (BattleSave.Save1.MissionType == 2 || BattleSave.Save1.MissionType == 4)
            {
                DefenceDeleteBoxCollider1.SetActive(true);
            }
        }
    }

    //적을 모두 처치한 뒤 다음 지역으로 이동
    void CompleteKillAll(bool boolean)
    {
        if (ActiveDeleteNumber == 1)
            Step1 = boolean;
        else if (ActiveDeleteNumber == 2)
            Step2 = boolean;
        else if (ActiveDeleteNumber == 3)
            Step3 = boolean;
        else if (ActiveDeleteNumber == 4)
            Step4 = boolean;
        else if (ActiveDeleteNumber == 5)
            Step5 = boolean;
        else if (ActiveDeleteNumber == 6)
            Step6 = boolean;

        if (Step1 == true)
        {
            Step1 = false;
            SpawnCollider1.SetActive(false);
            Cam.m_BoundingShape2D = Map;
            Debug.Log("스폰1 클리어");
        }
        if (Step2 == true)
        {
            Step2 = false;
            SpawnCollider2.SetActive(false);
            Cam.m_BoundingShape2D = Map;
            Debug.Log("스폰2 클리어");
        }
        if (Step3 == true)
        {
            Step3 = false;
            SpawnCollider3.SetActive(false);
            Cam.m_BoundingShape2D = Map;
            Debug.Log("스폰3 클리어");
        }
        if (Step4 == true)
        {
            Step4 = false;
            SpawnCollider4.SetActive(false);
            Cam.m_BoundingShape2D = Map;
            Debug.Log("스폰4 클리어");
        }
        if (Step5 == true)
        {
            Step5 = false;
            SpawnCollider5.SetActive(false);
            Cam.m_BoundingShape2D = Map;
            Debug.Log("스폰5 클리어");
        }
        if (Step6 == true)
        {
            Step6 = false;
            SpawnCollider6.SetActive(false);
            EndCollider.SetActive(true);
            Cam.m_BoundingShape2D = EndMap;
            Debug.Log("스폰6 클리어");
        }
    }

    public void StartSpawnEnemy1()
    {
        KillStepOne = false;
        SpawnCollider1.SetActive(true);
        Spawn1();
    }
    public void StartSpawnEnemy2()
    {
        KillStepOne = false;
        SpawnCollider2.SetActive(true);
        Spawn2();
    }
    public void StartSpawnEnemy3()
    {
        KillStepOne = false;
        SpawnCollider3.SetActive(true);
        Spawn3();
    }
    public void StartSpawnEnemy4()
    {
        KillStepOne = false;
        SpawnCollider4.SetActive(true);
        Spawn4();
    }
    public void StartSpawnEnemy5()
    {
        KillStepOne = false;
        SpawnCollider5.SetActive(true);
        Spawn5();
    }
    public void StartSpawnEnemy6()
    {
        KillStepOne = false;
        SpawnCollider6.SetActive(true);
        Spawn6();
    }

    //스폰1
    void  Spawn1()
    {
        if (Step1 == false)
        {
            Step1 = true;
            SpawnSiteGenerator.Follow = SpawnMap1;
            Cam.m_BoundingShape2D = CamSpawnMap1;
            KillStepOne = true;

            int SpawnCycle = 1;
            if (BattleSave.Save1.MissionLevel == 1)
                SpawnCycle = Random.Range(2, 3);
            else if (BattleSave.Save1.MissionLevel > 2)
                SpawnCycle = Random.Range(2, 4);

            spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 1, 1, false));
        }
    }

    //스폰2
    void Spawn2()
    {
        if (Step2 == false)
        {
            Step2 = true;
            GoProcessBar.SetActive(false);
            SpawnSiteGenerator.Follow = SpawnMap2;
            Cam.m_BoundingShape2D = CamSpawnMap2;
            SpawnRunning = true;

            int SpawnCycle = 1;
            if (BattleSave.Save1.MissionLevel == 1)
                SpawnCycle = Random.Range(2, 3);
            else if (BattleSave.Save1.MissionLevel > 2)
                SpawnCycle = Random.Range(2, 4);

            spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 2, 2, false));
        }
    }

    //스폰3
    void Spawn3()
    {
        if (Step3 == false)
        {
            Step3 = true;
            GoProcessBar.SetActive(false);
            SpawnSiteGenerator.Follow = SpawnMap3;
            Cam.m_BoundingShape2D = CamSpawnMap3;
            SpawnRunning = true;

            if (BattleSave.Save1.MissionType == 100 || BattleSave.Save1.MissionType == 101)
            {
                if (BattleSave.Save1.FinishSpawnNumber != ActiveDeleteNumber + 1) //기함 침투용일 경우, 폭탄 설치전까지 스폰이 중단된다.
                {
                    int SpawnCycle = 1;
                    if (BattleSave.Save1.MissionLevel == 1)
                        SpawnCycle = Random.Range(2, 3);
                    else if (BattleSave.Save1.MissionLevel > 2)
                        SpawnCycle = Random.Range(2, 4);

                    spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 3, 3, true));
                }
                else if (BombSettings.isBombSetted == true) //기함 침투용 스폰(폭탄 설치 뒤, 스폰 시작)
                {
                    KillStepOne = true;
                    spawnStep = StartCoroutine(SpawnStep(100, 3, 3, true));
                }
            }
            else if (BattleSave.Save1.MissionType > 0 && BattleSave.Save1.MissionType <= 4)
            {
                int SpawnCycle = 1;
                if (BattleSave.Save1.MissionLevel == 1)
                    SpawnCycle = Random.Range(2, 3);
                else if (BattleSave.Save1.MissionLevel > 2)
                    SpawnCycle = Random.Range(2, 4);

                spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 3, 3, true));
            }
        }
    }

    //스폰4
    void Spawn4()
    {
        if (Step4 == false)
        {
            Step4 = true;
            GoProcessBar.SetActive(false);
            SpawnSiteGenerator.Follow = SpawnMap4;
            Cam.m_BoundingShape2D = CamSpawnMap4;
            SpawnRunning = true;

            if (BattleSave.Save1.MissionType == 100 || BattleSave.Save1.MissionType == 101)
            {
                if (BattleSave.Save1.FinishSpawnNumber != ActiveDeleteNumber + 1) //기함 침투용일 경우, 폭탄 설치전까지 스폰이 중단된다.
                {
                    int SpawnCycle = 1;
                    if (BattleSave.Save1.MissionLevel == 1)
                        SpawnCycle = Random.Range(2, 3);
                    else if (BattleSave.Save1.MissionLevel > 2)
                        SpawnCycle = Random.Range(2, 4);

                    spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 4, 3, true));
                }
                else if (BombSettings.isBombSetted == true) //기함 침투용 스폰(폭탄 설치 뒤, 스폰 시작)
                {
                    KillStepOne = true;
                    spawnStep = StartCoroutine(SpawnStep(100, 4, 2, true));
                }
            }
            else if (BattleSave.Save1.MissionType > 0 && BattleSave.Save1.MissionType <= 4)
            {
                int SpawnCycle = 1;
                if (BattleSave.Save1.MissionLevel == 1)
                    SpawnCycle = Random.Range(2, 3);
                else if (BattleSave.Save1.MissionLevel > 2)
                    SpawnCycle = Random.Range(2, 4);

                spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 4, 2, true));
            }
        }
    }

    //스폰5
    void Spawn5()
    {
        if (Step5 == false)
        {
            Step5 = true;
            GoProcessBar.SetActive(false);
            SpawnSiteGenerator.Follow = SpawnMap5;
            Cam.m_BoundingShape2D = CamSpawnMap5;
            SpawnRunning = true;

            int SpawnCycle = 1;
            if (BattleSave.Save1.MissionLevel == 1)
                SpawnCycle = Random.Range(2, 3);
            else if (BattleSave.Save1.MissionLevel > 2)
                SpawnCycle = Random.Range(2, 4);

            spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 5, 2, false));
        }
    }

    //스폰6
    void Spawn6()
    {
        if (Step6 == false)
        {
            Step6 = true;
            GoProcessBar.SetActive(false);
            SpawnSiteGenerator.Follow = SpawnMap6;
            Cam.m_BoundingShape2D = CamSpawnMap6;
            SpawnRunning = true;

            int SpawnCycle = 1;
            if (BattleSave.Save1.MissionLevel == 1)
                SpawnCycle = Random.Range(2, 3);
            else if (BattleSave.Save1.MissionLevel > 2)
                SpawnCycle = Random.Range(2, 4);

            spawnStep = StartCoroutine(SpawnStep(SpawnCycle, 6, 3, true));
        }
    }

    //스폰 절차
    public IEnumerator SpawnStep(int SpawnCycle, int DeleteNumber, int AreaLevel, bool isElite)
    {
        if (SpawnCycle != 0 && SpawnStop == false)
        {
            for (int i = 0; i <= SpawnCycle; i++)
            {
                while (true)
                {
                    if (KillStepOne == true) //첫번째 스폰
                    {
                        if (AreaLevel == 1 && SpawnStop == false)
                        {
                            if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                            {
                                if (MissionAreaType == MissionArea.Planet)
                                    SpawnLevelType.SpawnOfPlanet1();
                                else if (MissionAreaType == MissionArea.EnemyFlagship)
                                    SpawnLevelType.SpawnOfPlanet1();
                            }
                            else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                            {
                                SpawnLevelType.SpawnOfZombie1();
                            }
                        }
                        else if (AreaLevel == 2 && SpawnStop == false)
                        {
                            if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                            {
                                if (MissionAreaType == MissionArea.Planet)
                                    SpawnLevelType.SpawnOfPlanet2();
                                else if (MissionAreaType == MissionArea.EnemyFlagship)
                                    SpawnLevelType.SpawnOfPlanet2();
                            }
                            else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                            {
                                SpawnLevelType.SpawnOfZombie1();
                            }
                        }
                        else if (AreaLevel >= 3 && SpawnStop == false)
                        {
                            if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                            {
                                if (MissionAreaType == MissionArea.Planet)
                                    SpawnLevelType.SpawnOfPlanet3();
                                else if (MissionAreaType == MissionArea.EnemyFlagship)
                                    SpawnLevelType.SpawnOfPlanet3();
                            }
                            else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                            {
                                SpawnLevelType.SpawnOfZombie1();
                            }
                        }

                        if (isElite == true && SpawnStop == false) //엘리트 스폰
                        {
                            int EliteOn = Random.Range(0, 2);
                            if (EliteOn == 0)
                            {
                                if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                                {
                                    if (BattleSave.Save1.MissionLevel > 1)
                                        SpawnLevelType.EliteSpawn1();
                                }
                                else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                                {
                                    if (BattleSave.Save1.MissionLevel > 1)
                                        SpawnLevelType.ZombieEliteSpawn1();
                                }
                            }
                        }
                        if (SpawnStop == true)
                            SpawnCycle = 0;

                        //Debug.Log("1차 스폰 종료");
                        if (BattleSave.Save1.MissionLevel == 1)
                            SpawnCountOnce = 0;
                        else if (BattleSave.Save1.MissionLevel == 2)
                            SpawnCountOnce = Random.Range(0, 2);
                        else if (BattleSave.Save1.MissionLevel >= 3)
                            SpawnCountOnce = Random.Range(0, 3);

                        if (SpawnCountOnce != 0 && SpawnStop == false)
                        {
                            for (int j = 0; j < SpawnCountOnce; j++) //첫 번째 이후의 연속 스폰
                            {
                                if (SpawnCountOnce != 0)
                                {
                                    int RandomSpawnTime = Random.Range(8, 12);
                                    yield return new WaitForSeconds(RandomSpawnTime);

                                    if (AreaLevel == 1 && SpawnStop == false)
                                    {
                                        if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                                        {
                                            if (MissionAreaType == MissionArea.Planet)
                                                SpawnLevelType.SpawnOfPlanet1();
                                            else if (MissionAreaType == MissionArea.EnemyFlagship)
                                                SpawnLevelType.SpawnOfPlanet1();
                                        }
                                        else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                                        {
                                            SpawnLevelType.SpawnOfZombie1();
                                        }
                                    }
                                    else if (AreaLevel == 2 && SpawnStop == false)
                                    {
                                        if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                                        {
                                            if (MissionAreaType == MissionArea.Planet)
                                                SpawnLevelType.SpawnOfPlanet2();
                                            else if (MissionAreaType == MissionArea.EnemyFlagship)
                                                SpawnLevelType.SpawnOfPlanet2();
                                        }
                                        else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                                        {
                                            SpawnLevelType.SpawnOfZombie1();
                                        }
                                    }
                                    else if (AreaLevel >= 3 && SpawnStop == false)
                                    {
                                        if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                                        {
                                            if (MissionAreaType == MissionArea.Planet)
                                                SpawnLevelType.SpawnOfPlanet3();
                                            else if (MissionAreaType == MissionArea.EnemyFlagship)
                                                SpawnLevelType.SpawnOfPlanet3();
                                        }
                                        else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                                        {
                                            SpawnLevelType.SpawnOfZombie1();
                                        }
                                    }
                                }

                                if (isElite == true && SpawnStop == false)
                                {
                                    if (BattleSave.Save1.MissionType != 3 && BattleSave.Save1.MissionType != 4)
                                    {
                                        if (BattleSave.Save1.MissionLevel == 2)
                                            SpawnLevelType.EliteSpawn1();
                                        else if (BattleSave.Save1.MissionLevel > 2)
                                            SpawnLevelType.EliteSpawn2();
                                    }
                                    else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4)
                                    {
                                        if (BattleSave.Save1.MissionLevel > 1)
                                            SpawnLevelType.ZombieEliteSpawn1();
                                    }
                                }

                                if (j == SpawnCountOnce) //연속 스폰이 끝났을 경우, 스폰 종료 절차를 시작한다
                                {
                                    ActiveDeleteNumber = DeleteNumber;
                                    StartCoroutine(ActiveDeleteBox());
                                    KillStepOne = false;
                                    SpawnRunning = true;
                                    //Debug.Log("연속 스폰 종료, 마지막 사이클 도달");
                                }
                            }
                        }
                        else if (SpawnCountOnce == 0 && SpawnStop == false)
                        {
                            ActiveDeleteNumber = DeleteNumber;
                            StartCoroutine(ActiveDeleteBox());
                            KillStepOne = false;
                            SpawnRunning = true;
                            //Debug.Log("연속 스폰 없는 마지막 사이클 스폰 도달");
                        }
                        if (SpawnStop == true)
                            SpawnCycle = 0;
                    }
                    else //만약 마지막 사이클이 아닐 경우, 자동으로 이곳으로 와서 다음 사이클이 진행되기 전까지 게속 대기
                    {
                        yield return new WaitForSeconds(1);
                        if (SpawnStop == true)
                            SpawnCycle = 0;
                        continue;
                    }
                    break;
                }

                yield return new WaitForSeconds(6);

                if (i == SpawnCycle) //마지막 사이클에 도달했을 경우에만 여기서 대기, 마지막 사이클이 아닐 경우, 이어서 반복 스폰 시작
                {
                    while (true)
                    {
                        if (KillStepOne == true && ScoreManager.instance.AllCnt <= 0 && ScoreManager.instance.EnemyList.Count <= 0) //적들이 모두 처치되면 마지막 사이클을 종료하며 임무 완료
                        {
                            ScoreManager.instance.AllCnt = 0;
                            ScoreManager.instance.EnemyList.Clear();
                            SpawnRunning = true;
                            KillStepOne = false;

                            if (DeleteNumber == BattleSave.Save1.FinishSpawnNumber)
                            {
                                if (StageDefence.WaveStepStart == true)
                                {
                                    ClearLine.MissionComplete(StageDefence.SpawnMap);
                                    //Debug.Log("방어전 임무 완료");
                                }
                                else
                                {
                                    if (DeleteNumber == 1)
                                        ClearLine.MissionComplete(SpawnMap1);
                                    else if (DeleteNumber == 2)
                                        ClearLine.MissionComplete(SpawnMap2);
                                    else if (DeleteNumber == 3)
                                        ClearLine.MissionComplete(SpawnMap3);
                                    else if (DeleteNumber == 4)
                                        ClearLine.MissionComplete(SpawnMap4);
                                    else if (DeleteNumber == 5)
                                        ClearLine.MissionComplete(SpawnMap5);
                                    else if (DeleteNumber == 6)
                                        ClearLine.MissionComplete(SpawnMap6);
                                    //Debug.Log("진격전 임무 완료");
                                }
                            }
                            else if (DeleteNumber != BattleSave.Save1.FinishSpawnNumber && SpawnStop == false)
                            {
                                if (StageDefence.WaveStepStart == true)
                                {
                                    StartCoroutine(StageDefence.CompleteKillAll());
                                    //Debug.Log("방어전 웨이브 대기");
                                }
                                else
                                {
                                    //Debug.Log("다음");
                                    Cam.m_Damping = 2;
                                    CompleteKillAll(true);
                                    GoProcessBar.SetActive(true);
                                    yield return new WaitForSeconds(3);
                                    GoProcessBar.SetActive(false);
                                }
                            }
                            break;
                        }
                        else if (KillStepOne == false || ScoreManager.instance.AllCnt > 0 || ScoreManager.instance.EnemyList.Count > 0) //마지막 사이클의 적들이 처치 될 때까지 대기
                        {
                            yield return new WaitForSeconds(1);
                            //Debug.Log("마지막 사이클 대기");
                            if (SpawnStop == true)
                                SpawnCycle = 0;
                            continue;
                        }
                    }
                }
            }
        }
    }

    //스폰 중단
    public void StopSpawn()
    {
        if (spawnStep != null)
            StopCoroutine(spawnStep);
        SpawnStop = true;
    }
}