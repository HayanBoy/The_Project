using System.Collections.Generic;
using UnityEngine;

public class StarBattleSystem : MonoBehaviour
{
    [Header("스크립트")]
    public LiveCommunicationSystem LiveCommunicationSystem;
    AreaStatement AreaStatement;

    [Header("항성 정보")]
    public int StarNumber;
    public bool isInFight; //전투가 시작되면 스위치가 켜진다.
    public bool EnemySpawn = false; //적이 스폰되었는지 여부
    public bool isFree = false; //해방되었는지 여부

    [Header("소속된 행성 리스트")]
    public GameObject Planet1;
    public GameObject Planet2;
    public GameObject Planet3;
    public GameObject Planet4;
    public GameObject Planet5;

    [Header("항성 해방 권한")]
    public int MinusFlagship; //축소 기함 수
    public int MinusFormationShip; //축소 편대함 수
    public bool isSupported = false; //항성 중에서 지원함대가 소속해 있는지에 대한 여부, 활성화해두면 해당 항성을 해방시 CantSupport가 켜지도록 유도된다.
    public bool CantSupport = false; //활성화 시, 해당 지역에 지원 함대를 불러올 수 없게 된다.

    [Header("함대 주둔 상황")]
    public bool FlagshipHere;
    public bool EnemyShipHere;

    [Header("타이머")]
    public float BattleTimer; //전투시작 시, 경과 시간 측정

    [Header("함대 리스트")]
    public List<GameObject> FlagshipShipList = new List<GameObject>(); //방문한 나리하 기함
    public List<GameObject> BattleEnemyShipList = new List<GameObject>();

    private void Start()
    {
        AreaStatement = FindObjectOfType<AreaStatement>();

        //데이터 불러오기
        if (StarNumber == 1 && AreaStatement.ToropioStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 2 && AreaStatement.Roro1StarState == 1)
            StartGetStarFree();
        else if (StarNumber == 3 && AreaStatement.Roro2StarState == 1)
            StartGetStarFree();
        else if (StarNumber == 4 && AreaStatement.SarisiStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 5 && AreaStatement.GarixStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 6 && AreaStatement.SecrosStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 7 && AreaStatement.TeretosStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 8 && AreaStatement.MiniPopoStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 9 && AreaStatement.DeltaD31_4AStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 10 && AreaStatement.DeltaD31_4BStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 11 && AreaStatement.JeratoO95_7AStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 12 && AreaStatement.JeratoO95_7BStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 13 && AreaStatement.JeratoO95_14CStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 14 && AreaStatement.JeratoO95_14DStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 15 && AreaStatement.JeratoO95_OmegaStarState == 1)
            StartGetStarFree();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && FlagshipHere == false) //기함이 해당 항성에 있으면 아군 함대 활성화
        {
            FlagshipHere = true;
            FlagshipShipList.Add(collision.gameObject);

            if (GetComponent<EnemyGet>().enabled == false && isFree == false) //워프없이 접근 시 병력 활성화
            {
                GetComponent<EnemyGet>().WarpFleetDestination = collision.gameObject.transform.position;
                GetComponent<EnemyGet>().enabled = true;
            }
        }
        if (collision.gameObject.layer == 7) //적 함대가 해당 항성에 있으면 적 존재 활성화
        {
            EnemyShipHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && FlagshipHere == true) //기함이 해당 항성에 있으면 아군 함대 비활성화
        {
            FlagshipHere = false;
            FlagshipShipList.Remove(collision.gameObject);
        }
        if (collision.gameObject.layer == 7) //적 함대가 해당 행성에서 모두 격침되면 적 존재 비활성화
        {
            if (collision.CompareTag("Slorius Flag Ship") || collision.CompareTag("Slorius Follow Ship"))
            {
                if (collision.gameObject != null || collision.gameObject.activeSelf == false)
                    BattleEnemyShipList.Remove(collision.gameObject);
                else if (collision.gameObject == null)
                    BattleEnemyShipList.Remove(collision.gameObject);
            }
            if (collision.CompareTag("Kantakri Flag Ship1") || collision.CompareTag("Kantakri Follow Ship1"))
            {
                if (collision.transform.parent.gameObject != null || collision.transform.parent.gameObject.activeSelf == false)
                    BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
                else if (collision.transform.parent.gameObject == null)
                    BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
            }
            if (FlagshipHere == true && isInFight  == true && BattleEnemyShipList.Count == 0)
            {
                EnemyShipHere = false;
                isInFight = false;
                EnemySpawn = false;
                PlanetsFree();
            }
        }
    }

    private void Update()
    {
        if (isInFight == true && FlagshipHere == false && BattleEnemyShipList.Count > 0) //전투 시작 시, 전투 타이머가 발동되며 전투 시간이 지나면, 자동으로 해당 사이트의 모든 것이 삭제되며, 리스폰
        {
            if (BattleTimer <= 300)
            {
                BattleTimer += Time.deltaTime;
            }
            else
            {
                BattleTimer = 0;
                isInFight = false;
                EnemySpawn = false;
                EnemyShipHere = false;

                int Amount = BattleEnemyShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(BattleEnemyShipList[0], 0.1f);
                    BattleEnemyShipList.Remove(BattleEnemyShipList[0]);
                }

                GetComponent<EnemyGet>().enabled = false;
                GetComponent<EnemyGet>().FlagshipWarp = false;
                GetComponent<EnemyGet>().WarpControsType = 0;
            }
        }
    }

    //항성에서 승리 후, 각 행성의 함대수를 축소 및 지원 중지
    void PlanetsFree()
    {
        if (Planet1 != null)
        {
            if (Planet1.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet1.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet1.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet1.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet2.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet2.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet2.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet3.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet3.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet3.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet4.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet4.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet4.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet5.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet5.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet5.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (isSupported == true)
            CantSupport = true;

        if (StarNumber == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Toropio";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "토로피오";
            AreaStatement.ToropioStarState = 1;
        }
        else if (StarNumber == 2)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro I";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "로로 I";
            AreaStatement.Roro1StarState = 1;
        }
        else if (StarNumber == 3)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro II";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "로로 II";
            AreaStatement.Roro2StarState = 1;
        }
        else if (StarNumber == 4)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Sarisi";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "사리시";
            AreaStatement.SarisiStarState = 1;
        }
        else if (StarNumber == 5)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Garix";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "가릭스";
            AreaStatement.GarixStarState = 1;
        }
        else if (StarNumber == 6)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Secros";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "세크로스";
            AreaStatement.SecrosStarState = 1;
        }
        else if (StarNumber == 7)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Teretos";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "테레토스";
            AreaStatement.TeretosStarState = 1;
        }
        else if (StarNumber == 8)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Mini popo";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "미니 포포";
            AreaStatement.MiniPopoStarState = 1;
        }
        else if (StarNumber == 9)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-4A";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "델타 D31-4A";
            AreaStatement.DeltaD31_4AStarState = 1;
        }
        else if (StarNumber == 10)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-4B";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "델타 D31-4B";
            AreaStatement.DeltaD31_4BStarState = 1;
        }
        else if (StarNumber == 11)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-7A";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-7A";
            AreaStatement.JeratoO95_7AStarState = 1;
        }
        else if (StarNumber == 12)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-7B";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-7B";
            AreaStatement.JeratoO95_7BStarState = 1;
        }
        else if (StarNumber == 13)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-14C";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-14C";
            AreaStatement.JeratoO95_14CStarState = 1;
        }
        else if (StarNumber == 14)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-14D";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-14D";
            AreaStatement.JeratoO95_14DStarState = 1;
        }
        else if (StarNumber == 15)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-Omega";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-오메가";
            AreaStatement.JeratoO95_OmegaStarState = 1;
        }

        StartCoroutine(LiveCommunicationSystem.MainCommunication(3.01f));
    }

    //항성 해방 정보 불러오기
    void StartGetStarFree()
    {
        isFree = true;

        if (Planet1 != null)
        {
            if (Planet1.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet1.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet1.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet1.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet2.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet2.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet2.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet3.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet3.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet3.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet4.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet4.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet4.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet5.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet5.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet5.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (isSupported == true)
            CantSupport = true;
    }

    //자신의 행성들의 상태를 불러오기
    public void GetPlanetState()
    {
        int Planets = 0;
        int FreePlanets = 0;

        if (Planet1 != null)
            Planets++;
        if (Planet2 != null)
            Planets++;
        if (Planet3 != null)
            Planets++;
        if (Planet4 != null)
            Planets++;
        if (Planet5 != null)
            Planets++;

        //행성이 해방되었는지 확인
        if (Planet1 != null)
        {
            if (Planet1.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }

        //소속된 행성이 모두 해방되어 있을 경우, 현재 항성 역시 해방 처리
        if (FreePlanets == Planets)
        {
            if (StarNumber == 1)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[0] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[0] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.ToropioStarState = 1;
            }
            else if (StarNumber == 2)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[1] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[1] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.Roro1StarState = 1;
                AreaStatement.Roro2StarState = 1;
            }
            else if (StarNumber == 4)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[2] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[2] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.SarisiStarState = 1;
            }
            else if (StarNumber == 5)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[3] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[3] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.GarixStarState = 1;
            }
            else if (StarNumber == 6)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[4] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[4] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.SecrosStarState = 1;
                AreaStatement.TeretosStarState = 1;
                AreaStatement.MiniPopoStarState = 1;
            }
            else if (StarNumber == 9)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[5] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[5] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.DeltaD31_4AStarState = 1;
                AreaStatement.DeltaD31_4BStarState = 1;
            }
            else if (StarNumber == 11)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[6] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[6] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.JeratoO95_7AStarState = 1;
                AreaStatement.JeratoO95_7BStarState = 1;
                AreaStatement.JeratoO95_14CStarState = 1;
                AreaStatement.JeratoO95_14DStarState = 1;
                AreaStatement.JeratoO95_OmegaStarState = 1;
            }
        }
    }

    //항성 해방했을 때 대화문
    void PrintLiveText()
    {
        if (StarNumber == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Toropio";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "토로피오";
        }
        else if (StarNumber == 2 || StarNumber == 3)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "로로";
        }
        else if (StarNumber == 4)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Sarisi";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "사리시";
        }
        else if (StarNumber == 5)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Garix";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "가릭스";
        }
        else if (StarNumber == 6 || StarNumber == 7 || StarNumber == 8)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "OctoKrasis Patoro";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "옥토크라시스 파토로";
        }
        else if (StarNumber == 9 || StarNumber == 10)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-402054";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "델타 D31-402054";
        }
        else if (StarNumber == 11 || StarNumber == 12 || StarNumber == 13 || StarNumber == 14 || StarNumber == 15)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-99024";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "제라토 O95-99024";
        }
    }
}