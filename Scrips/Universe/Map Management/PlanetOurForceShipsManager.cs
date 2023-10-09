using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOurForceShipsManager : MonoBehaviour
{
    [Header("스크립트")]
    public StarBattleSystem StarBattleSystem; //컨트로스 증원기능을 가지고 있는 자신의 모항성에만 연결. 없으면 연결하지 않아도 된다.
    public LiveCommunicationSystem LiveCommunicationSystem;
    TutorialSystem TutorialSystem;
    AreaStatement AreaStatement;
    DataSaveManager DataSaveManager;

    [Header("행성 정보")]
    public int PlanetNumber;
    public bool isInFight; //전투가 시작되면 스위치가 켜진다.
    public bool EnemySpawn = false; //적이 스폰되었는지 여부
    public GameObject MyStar; //해방시 해당 항성계의 행성들이 모두 해방되었는지 체크하기 위한 목적
    private CircleCollider2D Collider;

    [Header("아군 주둔 상황")]
    public bool FlagshipHere;
    public bool EnemyShipHere;
    public bool FirstFree; //데이터를 불러올 때마다 해당 행성이 해방되어 있는지 확인하는 정보. 체크가 없으면, 첫 해방으로 함선이 주둔하는 연출을 보인다. 그 이후에 이것이 체크되어 있으면, 이미 주둔하는 것으로 처리한다.
    public bool BattleVictory; //데이터를 불러올 때마다 해당 행성에서 승리했는지 체크 여부.

    [Header("컨트로스 증원")]
    public bool CanSupport; //증원이 가능한 여부.
    public int SupportLevel; //증원 레벨
    public bool FlagshipSupport; //기함 증원 여부. 체크 되어 있는 행성은 기함과 편대함 둘 중 하나가 선택되며, 체크되어 있지 않을 경우, 편대함만 증원된다.
    public float ControsFleetTime; //컨트로스 증원 쿨타임
    public float ControsFleetTime2;
    public int NumberOfSupport; //가능한 증원 횟수
    public int NumberOfSupport2;
    private int RandomCount; //랜덤 증원

    [Header("튜토리얼")]
    public bool Tutorial = false;

    [Header("타이머")]
    public float BattleTimer; //전투시작 시, 경과 시간 측정

    [Header("함대 리스트")]
    public List<GameObject> NarihaOurForceShipList = new List<GameObject>(); //나리하 아군 편대함 목록
    public List<GameObject> BattleEnemyShipList = new List<GameObject>();
    private float FirstListTime;

    private void Awake()
    {
        if (BattleSave.Save1 != null)
        {
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                for (int i = 0; i < BattleSave.Save1.PlanetNumber.Count; i++)
                {
                    if (BattleSave.Save1.PlanetNumber[i] == this.PlanetNumber)
                    {
                        FirstFree = BattleSave.Save1.FirstFreePlanet[i];
                        BattleVictory = BattleSave.Save1.BattleVictoryPlanet[i];
                    }
                }
            }
        }
    }

    private void Start()
    {
        Collider = this.gameObject.GetComponent<CircleCollider2D>();
        DataSaveManager = FindObjectOfType<DataSaveManager>();
        AreaStatement = FindObjectOfType<AreaStatement>();
        ShipManager.instance.FreePlanetList.Add(gameObject);
        StartCoroutine(GetInform());
    }

    IEnumerator GetInform()
    {
        yield return new WaitForSeconds(0.5f);
        GetOurForceShips();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FirstFree == false)
        {
            if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 해당 행성에 있으면 아군 함대 활성화
            {
                FlagshipHere = true;

                if (GetComponent<EnemyGet>().enabled == false && BattleVictory == false) //워프없이 접근 시 병력 활성화
                    StartCoroutine(EnemyRemoteGet());

                for (int i = 0; i < NarihaOurForceShipList.Count; i++)
                {
                    NarihaOurForceShipList[i].SetActive(true);
                }

                if (FlagshipHere == true && EnemyShipHere == true && BattleSave.Save1.FirstStart == true) //튜토리얼 전투가 시작
                {
                    Tutorial = true;
                    TutorialSystem = FindObjectOfType<TutorialSystem>();
                    StartCoroutine(TutorialSystem.FleetControlTutorial());
                }
            }
            if (collision.gameObject.layer == 7) //적 함대가 해당 행성에 있으면 적 존재 활성화
            {
                EnemyShipHere = true;

                if (NarihaOurForceShipList.Count > 0)
                {
                    for (int i = 0; i < NarihaOurForceShipList.Count; i++)
                    {
                        NarihaOurForceShipList[i].SetActive(true);
                    }
                }

                if (FlagshipHere == true && EnemyShipHere == true && BattleSave.Save1.FirstStart == true) //튜토리얼 전투가 시작
                {
                    Tutorial = true;
                    TutorialSystem = FindObjectOfType<TutorialSystem>();
                    StartCoroutine(TutorialSystem.FleetControlTutorial());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (FirstFree == false)
        {
            if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 해당 행성에 있으면 아군 함대 비활성화
            {
                FlagshipHere = false;

                for (int i = 0; i < NarihaOurForceShipList.Count; i++)
                {
                    NarihaOurForceShipList[i].SetActive(false);
                }
            }
            if (collision.gameObject.layer == 7) //적 함대가 해당 행성에서 모두 격침되면 적 존재 비활성화
            {
                if (collision.CompareTag("Slorius Flag Ship") || collision.CompareTag("Slorius Follow Ship"))
                {
                    if (collision.gameObject != null || collision.gameObject.activeSelf == false)
                        BattleEnemyShipList.Remove(collision.gameObject);
                    else if (collision.gameObject == null)
                        BattleEnemyShipList.Remove(collision.gameObject);
                    if (BattleEnemyShipList.Count == 0)
                        EnemyShipHere = false;
                }
                if (collision.CompareTag("Kantakri Flag Ship1") || collision.CompareTag("Kantakri Follow Ship1"))
                {
                    if (collision.transform.parent.gameObject != null || collision.transform.parent.gameObject.activeSelf == false)
                        BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
                    else if (collision.transform.parent.gameObject == null)
                        BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
                    if (BattleEnemyShipList.Count == 0)
                        EnemyShipHere = false;
                }
                if (EnemyShipHere == false)
                    BattleEnemyShipList.Clear();
                if (FlagshipHere == true && EnemyShipHere == false && BattleEnemyShipList.Count == 0)
                {
                    EnemyShipHere = false;
                    isInFight = false;
                    EnemySpawn = false;
                    BattleVictory = true;
                    GetOurForceShips(); //행성 해방 절차
                }

                if (NarihaOurForceShipList.Count > 0)
                {
                    for (int i = 0; i < NarihaOurForceShipList.Count; i++)
                    {
                        NarihaOurForceShipList[i].SetActive(false);
                    }
                }
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
                ControsFleetTime2 = 0;
                NumberOfSupport2 = 0;
                isInFight = false;
                EnemySpawn = false;
                EnemyShipHere = false;

                int Amount = BattleEnemyShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(BattleEnemyShipList[0], 0.1f);
                    BattleEnemyShipList.Remove(BattleEnemyShipList[0]);
                }
                Amount = NarihaOurForceShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(NarihaOurForceShipList[0], 0.1f);
                    NarihaOurForceShipList.Remove(NarihaOurForceShipList[0]);
                }

                GetComponent<EnemyGet>().enabled = false;
                GetComponent<EnemyGet>().FlagshipWarp = false;
                GetComponent<EnemyGet>().WarpControsType = 0;
            }
        }

        //전투 시작 시, 컨트로스 증원 쿨타임이 시작된다.
        if (CanSupport == true)
        {
            if (StarBattleSystem.CantSupport == false && NumberOfSupport2 < NumberOfSupport)
            {
                if (isInFight == true && FlagshipHere == true && BattleEnemyShipList.Count > 0)
                {
                    ControsFleetTime2 += Time.deltaTime;

                    if (FirstListTime == 0)
                    {
                        FirstListTime += Time.deltaTime;
                        RandomCount = Random.Range(BattleEnemyShipList.Count / 8, BattleEnemyShipList.Count / 2); //증원이 이루어지는 시기
                    }
                }
                if (ControsFleetTime2 >= ControsFleetTime && BattleEnemyShipList.Count <= RandomCount)
                {
                    ControsFleetTime2 = 0;

                    if (BattleEnemyShipList.Count <= RandomCount)
                    {
                        NumberOfSupport2++;

                        GetComponent<EnemyGet>().enabled = false;
                        GetComponent<EnemyGet>().FlagshipWarp = false;
                        GetComponent<EnemyGet>().WarpControsType = 0;
                        StartCoroutine(SupportRequest());
                        StartCoroutine(LiveCommunicationSystem.MainCommunication(5.01f));
                    }
                }
            }
        }
    }

    //함대 주둔 시작
    public void GetOurForceShips()
    {
        if (BattleSave.Save1.MissionSuccessed == true)
        {
            BattleSave.Save1.MissionSuccessed = false;
        }
        if (EnemyShipHere == false && BattleEnemyShipList.Count == 0) //허리케인 작전을 모두 했음에도 적 함대가 남아있을 경우, 아직 해방되지 않은 것으로 간주한다.
        {
            if (PlanetNumber == 1001 && MissionCompleteManager.MCMInstance.SatariusGlessiaMissionCompleteCount == MissionCompleteManager.MCMInstance.SatariusGlessiaMission.Count)
            {
                if (FirstFree == false) //첫 해방했을 때만 적용
                {
                    AreaStatement.SatariusGlessiaState = 1;
                    AreaStatement.SatariusGlessiaFreeCount++;
                    WeaponUnlockManager.instance.SatariusGlessiaCommercialUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit = 10000;
                    BattleSave.Save1.ConstructionResourcelimit = 10000;
                    StartCoroutine(ChangeState());
                }

                if (AreaStatement.SatariusGlessiaState == 1) //매번 게임을 불러오거나 첫 해방했을 때에만 적용
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1002 && MissionCompleteManager.MCMInstance.AposisMissionCompleteCount == MissionCompleteManager.MCMInstance.AposisMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.AposisState = 1;
                    AreaStatement.AposisFreeCount++;
                    WeaponUnlockManager.instance.AposisLabUnlock = true;
                    WeaponUnlockManager.instance.Hydra56Unlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponLeftSlotUnlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponCountUnlock++;
                    WeaponUnlockManager.instance.OSEH302WidowHireUnlock = true;
                    WeaponUnlockManager.instance.SubWeaponCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Aposis";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "아포시스";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.AposisState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1003 && MissionCompleteManager.MCMInstance.ToronoMissionCompleteCount == MissionCompleteManager.MCMInstance.ToronoMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.ToronoState = 1;
                    AreaStatement.ToronoFreeCount++;
                    WeaponUnlockManager.instance.ToronoResourceUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Torono";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "토로노";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.ToronoState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1004 && MissionCompleteManager.MCMInstance.PlopaIIMissionCompleteCount == MissionCompleteManager.MCMInstance.PlopaIIMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.Plopa2State = 1;
                    AreaStatement.Plopa2FreeCount++;
                    WeaponUnlockManager.instance.PlopaIIResidenceUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Plopa II";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "플로파 II";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.Plopa2State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1005 && MissionCompleteManager.MCMInstance.VedesVIMissionCompleteCount == MissionCompleteManager.MCMInstance.VedesVIMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.Vedes4State = 1;
                    AreaStatement.Vedes4FreeCount++;
                    WeaponUnlockManager.instance.VedesVILabUnlock = true;
                    WeaponUnlockManager.instance.MEAGUnlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponRightSlotUnlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponCountUnlock++;
                    WeaponUnlockManager.instance.CGD27PillishionUnlock = true;
                    WeaponUnlockManager.instance.VM5AEGUnlock = true;
                    WeaponUnlockManager.instance.GrenadeCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Vedes VI";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "베데스 VI";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.Vedes4State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1006 && MissionCompleteManager.MCMInstance.AronPeriMissionCompleteCount == MissionCompleteManager.MCMInstance.AronPeriMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.AronPeriState = 1;
                    AreaStatement.AronPeriFreeCount++;
                    WeaponUnlockManager.instance.AronPeriCommercialUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Aron Peri";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "아론 페리";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.AronPeriState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1007 && MissionCompleteManager.MCMInstance.PapatusIIMissionCompleteCount == MissionCompleteManager.MCMInstance.PapatusIIMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.Papatus2State = 1;
                    AreaStatement.Papatus2FreeCount++;
                    WeaponUnlockManager.instance.PapatusIIResourceUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Papatus II";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "파파투스 II";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.Papatus2State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1008 && MissionCompleteManager.MCMInstance.PapatusIIIMissionCompleteCount == MissionCompleteManager.MCMInstance.PapatusIIIMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.Papatus3State = 1;
                    AreaStatement.Papatus3FreeCount++;
                    WeaponUnlockManager.instance.PapatusIIILabUnlock = true;
                    WeaponUnlockManager.instance.ArthesL775Unlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponTopSlotUnlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponCountUnlock++;
                    WeaponUnlockManager.instance.DP9007Unlock = true;
                    WeaponUnlockManager.instance.M3078Unlock = true;
                    WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Papatus III";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "파파투스 III";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.Papatus3State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1009 && MissionCompleteManager.MCMInstance.KyepotorosMissionCompleteCount == MissionCompleteManager.MCMInstance.KyepotorosMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.KyepotorosState = 1;
                    AreaStatement.KyepotorosFreeCount++;
                    WeaponUnlockManager.instance.KyepotorosResidenceUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Kyepotoros";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "키예포토로스";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.KyepotorosState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1010 && MissionCompleteManager.MCMInstance.TratosMissionCompleteCount == MissionCompleteManager.MCMInstance.TratosMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.TratosState = 1;
                    AreaStatement.TratosFreeCount++;
                    WeaponUnlockManager.instance.TratosResidenceUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Tratos";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "트라토스";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.TratosState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1011 && MissionCompleteManager.MCMInstance.OclasisMissionCompleteCount == MissionCompleteManager.MCMInstance.OclasisMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.OclasisState = 1;
                    AreaStatement.OclasisFreeCount++;
                    WeaponUnlockManager.instance.OclasisResourceUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Oclasis";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "오클라시스";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.OclasisState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1012 && MissionCompleteManager.MCMInstance.DeriousHeriMissionCompleteCount == MissionCompleteManager.MCMInstance.DeriousHeriMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.DeriousHeriState = 1;
                    AreaStatement.DeriousHeriFreeCount++;
                    WeaponUnlockManager.instance.DeriousHeriLabUnlock = true;
                    WeaponUnlockManager.instance.UGG98Unlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponDownSlotUnlock = true;
                    WeaponUnlockManager.instance.ChangeWeaponCountUnlock++;
                    WeaponUnlockManager.instance.DS65Unlock = true;
                    WeaponUnlockManager.instance.PGM1036ScaletHawkUnlock = true;
                    WeaponUnlockManager.instance.AirStrikeCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "DeriousHeri";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "데리우스 헤리";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.DeriousHeriState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1013 && MissionCompleteManager.MCMInstance.VeltrorexyMissionCompleteCount == MissionCompleteManager.MCMInstance.VeltrorexyMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.VeltrorexyState = 1;
                    AreaStatement.VeltrorexyFreeCount++;
                    WeaponUnlockManager.instance.VeltrorexyCommercialUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Veltrorexy";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "벨트로렉시";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.VeltrorexyState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1014 && MissionCompleteManager.MCMInstance.ErixJeoqetaMissionCompleteCount == MissionCompleteManager.MCMInstance.ErixJeoqetaMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.ErixJeoqetaState = 1;
                    AreaStatement.ErixJeoqetaFreeCount++;
                    WeaponUnlockManager.instance.ErixJeoqetaCommercialUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Erix Jeoqeta";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "에릭스 제퀘타";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.ErixJeoqetaState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1015 && MissionCompleteManager.MCMInstance.QeepoMissionCompleteCount == MissionCompleteManager.MCMInstance.QeepoMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.QeepoState = 1;
                    AreaStatement.QeepoFreeCount++;
                    WeaponUnlockManager.instance.QeepoResourceUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Qeepo";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "퀴이포";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.QeepoState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1016 && MissionCompleteManager.MCMInstance.CrownYosereMissionCompleteCount == MissionCompleteManager.MCMInstance.CrownYosereMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.CrownYosereState = 1;
                    AreaStatement.CrownYosereFreeCount++;
                    WeaponUnlockManager.instance.CrownYosereLabUnlock = true;
                    WeaponUnlockManager.instance.ASC365Unlock = true;
                    WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Crown Yosere";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "크라운 요세레";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.CrownYosereState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1017 && MissionCompleteManager.MCMInstance.OrosMissionCompleteCount == MissionCompleteManager.MCMInstance.OrosMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.OrosState = 1;
                    AreaStatement.OrosFreeCount++;
                    WeaponUnlockManager.instance.OrosCommercialUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Oros";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "오로스";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.OrosState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1018 && MissionCompleteManager.MCMInstance.JapetAgroneMissionCompleteCount == MissionCompleteManager.MCMInstance.JapetAgroneMission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.JapetAgroneState = 1;
                    AreaStatement.JapetAgroneFreeCount++;
                    WeaponUnlockManager.instance.JapetAgroneLabUnlock = true;
                    WeaponUnlockManager.instance.MBCA79IronHurricaneUnlock = true;
                    WeaponUnlockManager.instance.VehicleCountUnlock++;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Japet Agrone";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "자펫 아그로네";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.JapetAgroneState == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1019 && MissionCompleteManager.MCMInstance.Xacro042351MissionCompleteCount == MissionCompleteManager.MCMInstance.Xacro042351Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.Xacro042351State = 1;
                    AreaStatement.Xacro042351FreeCount++;
                    WeaponUnlockManager.instance.Xacro042351ResourceUnlock = true;
                    BattleSave.Save1.NarihaUnionGlopaoroslimit += 2000;
                    BattleSave.Save1.ConstructionResourcelimit += 2000;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Xacro 042351";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "자크로 042351";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.Xacro042351State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1020 && MissionCompleteManager.MCMInstance.DeltaD31_2208MissionCompleteCount == MissionCompleteManager.MCMInstance.DeltaD31_2208Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.DeltaD31_2208State = 1;
                    AreaStatement.DeltaD31_2208FreeCount++;
                    WeaponUnlockManager.instance.DeltaD31_2208LabUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Delta D31-2208";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "델타 D31-2208";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.DeltaD31_2208State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1021 && MissionCompleteManager.MCMInstance.DeltaD31_9523MissionCompleteCount == MissionCompleteManager.MCMInstance.DeltaD31_9523Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.DeltaD31_9523State = 1;
                    AreaStatement.DeltaD31_9523FreeCount++;
                    WeaponUnlockManager.instance.DeltaD31_9523ResidenceUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Delta D31-9523";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "델타 D31-9523";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.DeltaD31_9523State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1022 && MissionCompleteManager.MCMInstance.DeltaD31_12721MissionCompleteCount == MissionCompleteManager.MCMInstance.DeltaD31_12721Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.DeltaD31_12721State = 1;
                    AreaStatement.DeltaD31_12721FreeCount++;
                    WeaponUnlockManager.instance.DeltaD31_12721LabUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Delta D31-12721";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "델타 D31-12721";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.DeltaD31_12721State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1023 && MissionCompleteManager.MCMInstance.JeratoO95_1125MissionCompleteCount == MissionCompleteManager.MCMInstance.JeratoO95_1125Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.JeratoO95_1125State = 1;
                    AreaStatement.JeratoO95_1125FreeCount++;
                    WeaponUnlockManager.instance.JeratoO95_1125ResidenceUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Jerato O95-1125";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "제라토 O95-1125";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.JeratoO95_1125State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1024 && MissionCompleteManager.MCMInstance.JeratoO95_2252MissionCompleteCount == MissionCompleteManager.MCMInstance.JeratoO95_2252Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.JeratoO95_2252State = 1;
                    AreaStatement.JeratoO95_2252FreeCount++;
                    WeaponUnlockManager.instance.JeratoO95_2252LabUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Jerato O95-2252";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "제라토 O95-2252";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.JeratoO95_2252State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }
            else if (PlanetNumber == 1025 && MissionCompleteManager.MCMInstance.JeratoO95_8510MissionCompleteCount == MissionCompleteManager.MCMInstance.JeratoO95_8510Mission.Count)
            {
                if (FirstFree == false)
                {
                    AreaStatement.JeratoO95_8510State = 1;
                    AreaStatement.JeratoO95_8510FreeCount++;
                    WeaponUnlockManager.instance.JeratoO95_8510LabUnlock = true;
                    StartCoroutine(ChangeState());

                    if (BattleSave.Save1.LanguageType == 1)
                        LiveCommunicationSystem.PlanetName = "Jerato O95-8510";
                    else if (BattleSave.Save1.LanguageType == 2)
                        LiveCommunicationSystem.PlanetName = "제라토 O95-8510";

                    StartCoroutine(LiveCommunicationSystem.MainCommunication(2.01f));
                }
                if (AreaStatement.JeratoO95_8510State == 1)
                {
                    GetComponent<OurForceGet>().enabled = true;
                    MyStar.GetComponent<StarBattleSystem>().GetPlanetState();
                }
            }

            if (BattleSave.Save1.Tutorial > 0 && BattleSave.Save1.PlanetTutorial == 2)
            {
                BattleSave.Save1.Tutorial = 0;
                TutorialSystem = FindObjectOfType<TutorialSystem>();
                TutorialSystem.FirstTutorialFinish();
            }
        }
    }

    //컨트로스의 증원 요청
    IEnumerator SupportRequest()
    {
        yield return new WaitForSeconds(0.1f);

        if (FlagshipSupport == true)
        {
            int RandomSupport = Random.Range(0, 2);

            if (RandomSupport == 0)
                GetComponent<EnemyGet>().FlagshipWarp = true;
            else
                GetComponent<EnemyGet>().FlagshipWarp = false;
        }
        else
        {
            GetComponent<EnemyGet>().FlagshipWarp = false;
        }

        int RandomEnemy = Random.Range(1, 4);
        GetComponent<EnemyGet>().WarpControsType = RandomEnemy;
        GetComponent<EnemyGet>().isSupportEnemy = SupportLevel;
        GetComponent<EnemyGet>().enabled = true;
    }

    //새로 불러올 때마다, 해당 행성이 해방되었거나, 혹은 승전한 상태에서는 워프없이 접근하거나, 혹은 해당 행성에서 시작한 경우에 적 함대가 불러오는 일을 차단하기
    IEnumerator EnemyRemoteGet()
    {
        yield return new WaitForSeconds(1);

        if (FirstFree == false && BattleVictory == false)
            GetComponent<EnemyGet>().enabled = true;
    }

    //상태를 기함이 변경되도록 조취
    IEnumerator ChangeState()
    {
        Collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Collider.enabled = true;
    }
}