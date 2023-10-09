using UnityEngine;

public class HurricaneMissionManager : MonoBehaviour
{
    public WordPrintSystem WordPrintSystem;
    public HurricaneOperationMenu HurricaneOperationMenu;
    public MissionCompleteManager MissionCompleteManager;
    AreaStatement AreaStatement;

    public RectTransform MissionTableSize;

    public PlanetMissionType[] planetMissionType; //미션 지역

    [System.Serializable]
    public struct PlanetMissionType
    {
        public PlanetMissionList PlanetMissionList;
    }

    private void Start()
    {
        AreaStatement = FindObjectOfType<AreaStatement>();
    }

    //적 기함 미션 테이블 리스트 불러오기
    public void SearchEnemyFlagshipMissionList()
    {
        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count; i++)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count < 4)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count == 5)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Count == 6)
                MissionTableSize.sizeDelta = new Vector2(361, 800);

            HurricaneOperationMenu.EnemyFlagship.Add(ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().EnemyFlagship[i]);
            HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.SetActive(true);

            if(HurricaneOperationMenu.EnemyFlagship[i].GetComponent<EnemyShipBehavior>().NationType == 2)
            {
                HurricaneOperationMenu.missionTableList[i].shipMissionType = HurricaneOperationMenu.MissionTable.ShipMissionType.DestroySloriusFlagship;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea = 100;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 100;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = HurricaneOperationMenu.EnemyFlagship[i].GetComponent<EnemyShipLevelInformation>().Level;
                if (WordPrintSystem.LanguageType == 1)
                    HurricaneOperationMenu.missionTableList[i].AreaName.text = string.Format("Slorius Flagship");
                else if (WordPrintSystem.LanguageType == 2)
                    HurricaneOperationMenu.missionTableList[i].AreaName.text = string.Format("슬로리어스 기함");

                if (WordPrintSystem.LanguageType == 1)
                    HurricaneOperationMenu.missionTableList[i].MissionName.text = string.Format("Infiltration Tactics");
                else if (WordPrintSystem.LanguageType == 2)
                    HurricaneOperationMenu.missionTableList[i].MissionName.text = string.Format("침투전");
            }
            else if (HurricaneOperationMenu.EnemyFlagship[i].GetComponent<EnemyShipBehavior>().NationType == 3)
            {
                HurricaneOperationMenu.missionTableList[i].shipMissionType = HurricaneOperationMenu.MissionTable.ShipMissionType.DestroyKantakriFlagship;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea = 101;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 101;
                HurricaneOperationMenu.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = HurricaneOperationMenu.EnemyFlagship[i].GetComponent<EnemyShipLevelInformation>().Level;
                if (WordPrintSystem.LanguageType == 1)
                    HurricaneOperationMenu.missionTableList[i].AreaName.text = string.Format("Slorius Flagship");
                else if (WordPrintSystem.LanguageType == 2)
                    HurricaneOperationMenu.missionTableList[i].AreaName.text = string.Format("칸타크리 기함");

                if (WordPrintSystem.LanguageType == 1)
                    HurricaneOperationMenu.missionTableList[i].MissionName.text = string.Format("Infiltration Tactics");
                else if (WordPrintSystem.LanguageType == 2)
                    HurricaneOperationMenu.missionTableList[i].MissionName.text = string.Format("침투전");
            }
        }
    }

    //행성별 미션 테이블 리스트 불러오기
    public void SearchMissionListAtPlanet()
    {
        MissionTableSize.sizeDelta = new Vector2(361, 0);
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1001) //사타리우스 글래시아
        {
            for (int i = 0; i < planetMissionType[0].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.SatariusGlessiaMission[i] == false)
                {
                    planetMissionType[0].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[0].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[0].PlanetMissionList.MissionLevel;
                    planetMissionType[0].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[0].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(0, i);
                    PlanetBattleText(0, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1002) //아포시스
        {
            for (int i = 0; i < planetMissionType[1].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.AposisMission[i] == false)
                {
                    planetMissionType[1].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[1].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[1].PlanetMissionList.MissionLevel;
                    planetMissionType[1].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[1].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(1, i);
                    PlanetBattleText(1, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1003) //토로노
        {
            for (int i = 0; i < planetMissionType[2].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.ToronoMission[i] == false)
                {
                    planetMissionType[2].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[2].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[2].PlanetMissionList.MissionLevel;
                    planetMissionType[2].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[2].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(2, i);
                    PlanetBattleText(2, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1004) //플로파 II
        {
            for (int i = 0; i < planetMissionType[3].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.PlopaIIMission[i] == false)
                {
                    planetMissionType[3].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[3].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[3].PlanetMissionList.MissionLevel;
                    planetMissionType[3].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[3].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(3, i);
                    PlanetBattleText(3, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1005) //베데스 VI
        {
            for (int i = 0; i < planetMissionType[4].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.VedesVIMission[i] == false)
                {
                    planetMissionType[4].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[4].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[4].PlanetMissionList.MissionLevel;
                    planetMissionType[4].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[4].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(4, i);
                    PlanetBattleText(4, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1006) //아론 페리
        {
            for (int i = 0; i < planetMissionType[5].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.AronPeriMission[i] == false)
                {
                    planetMissionType[5].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[5].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[5].PlanetMissionList.MissionLevel;
                    planetMissionType[5].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[5].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(5, i);
                    PlanetBattleText(5, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1007) //파파투스 II
        {
            for (int i = 0; i < planetMissionType[6].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.PapatusIIMission[i] == false)
                {
                    planetMissionType[6].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[6].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[6].PlanetMissionList.MissionLevel;
                    planetMissionType[6].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[6].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(6, i);
                    PlanetBattleText(6, i);
                }
            }
            if (planetMissionType[6].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[6].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1008) //파파투스 III
        {
            for (int i = 0; i < planetMissionType[7].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.PapatusIIIMission[i] == false)
                {
                    planetMissionType[7].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[7].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[7].PlanetMissionList.MissionLevel;
                    planetMissionType[7].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[7].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(7, i);
                    PlanetBattleText(7, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1009) //키예포토로스
        {
            for (int i = 0; i < planetMissionType[8].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.KyepotorosMission[i] == false)
                {
                    planetMissionType[8].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[8].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[8].PlanetMissionList.MissionLevel;
                    planetMissionType[8].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[8].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(8, i);
                    PlanetBattleText(8, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1010) //트라토스
        {
            for (int i = 0; i < planetMissionType[9].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.TratosMission[i] == false)
                {
                    planetMissionType[9].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[9].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[9].PlanetMissionList.MissionLevel;
                    planetMissionType[9].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[9].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(9, i);
                    PlanetBattleText(9, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1011) //오클라시스
        {
            for (int i = 0; i < planetMissionType[10].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.OclasisMission[i] == false)
                {
                    planetMissionType[10].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[10].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[10].PlanetMissionList.MissionLevel;
                    planetMissionType[10].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[10].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(10, i);
                    PlanetBattleText(10, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1012) //데리우스 헤리
        {
            for (int i = 0; i < planetMissionType[11].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.DeriousHeriMission[i] == false)
                {
                    planetMissionType[11].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[11].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[11].PlanetMissionList.MissionLevel;
                    planetMissionType[11].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[11].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(11, i);
                    PlanetBattleText(11, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1013) //벨트로렉시
        {
            for (int i = 0; i < planetMissionType[12].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.VeltrorexyMission[i] == false)
                {
                    planetMissionType[12].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[12].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[12].PlanetMissionList.MissionLevel;
                    planetMissionType[12].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[12].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(12, i);
                    PlanetBattleText(12, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1014) //에릭스 제퀘타
        {
            for (int i = 0; i < planetMissionType[13].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.ErixJeoqetaMission[i] == false)
                {
                    planetMissionType[13].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[13].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[13].PlanetMissionList.MissionLevel;
                    planetMissionType[13].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[13].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(13, i);
                    PlanetBattleText(13, i);
                }
            }
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1015) //퀴이포
        {
            for (int i = 0; i < planetMissionType[14].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.QeepoMission[i] == false)
                {
                    planetMissionType[14].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[14].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[14].PlanetMissionList.MissionLevel;
                    planetMissionType[14].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[14].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(14, i);
                    PlanetBattleText(14, i);
                }
            }
            if (planetMissionType[14].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[14].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1016) //크라운 요세레
        {
            for (int i = 0; i < planetMissionType[15].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.CrownYosereMission[i] == false)
                {
                    planetMissionType[15].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[15].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[15].PlanetMissionList.MissionLevel;
                    planetMissionType[15].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[15].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(15, i);
                    PlanetBattleText(15, i);
                }
            }
            if (planetMissionType[15].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[15].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1017) //오로스
        {
            for (int i = 0; i < planetMissionType[16].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.OrosMission[i] == false)
                {
                    planetMissionType[16].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[16].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[16].PlanetMissionList.MissionLevel;
                    planetMissionType[16].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[16].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(16, i);
                    PlanetBattleText(16, i);
                }
            }
            if (planetMissionType[16].PlanetMissionList.missionTableList.Length <= 4)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[16].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1018) //자펫 아그로네
        {
            for (int i = 0; i < planetMissionType[17].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.JapetAgroneMission[i] == false)
                {
                    planetMissionType[17].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[17].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[17].PlanetMissionList.MissionLevel;
                    planetMissionType[17].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[17].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(17, i);
                    PlanetBattleText(17, i);
                }
            }
            if (planetMissionType[17].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[17].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1019) //자크로 042351
        {
            for (int i = 0; i < planetMissionType[18].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.Xacro042351Mission[i] == false)
                {
                    planetMissionType[18].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[18].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[18].PlanetMissionList.MissionLevel;
                    planetMissionType[18].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[18].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(18, i);
                    PlanetBattleText(18, i);
                }
            }
            if (planetMissionType[18].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[18].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1020) //델타 D31-2208
        {
            for (int i = 0; i < planetMissionType[19].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.DeltaD31_2208Mission[i] == false)
                {
                    planetMissionType[19].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[19].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[19].PlanetMissionList.MissionLevel;
                    planetMissionType[19].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[19].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(19, i);
                    PlanetBattleText(19, i);
                }
            }
            if (planetMissionType[19].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[19].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1021) //델타 D31-9523
        {
            for (int i = 0; i < planetMissionType[20].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.DeltaD31_9523Mission[i] == false)
                {
                    planetMissionType[20].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[20].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[20].PlanetMissionList.MissionLevel;
                    planetMissionType[20].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[20].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(20, i);
                    PlanetBattleText(20, i);
                }
            }
            if (planetMissionType[20].PlanetMissionList.missionTableList.Length == 5)
                MissionTableSize.sizeDelta = new Vector2(361, 800);
            else if (planetMissionType[20].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
            else if (planetMissionType[20].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1022) //델타 D31-12721
        {
            for (int i = 0; i < planetMissionType[21].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.DeltaD31_12721Mission[i] == false)
                {
                    planetMissionType[21].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[21].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[21].PlanetMissionList.MissionLevel;
                    planetMissionType[21].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[21].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(21, i);
                    PlanetBattleText(21, i);
                }
            }
            if (planetMissionType[21].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[21].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1023) //제라토 O95-1125
        {
            for (int i = 0; i < planetMissionType[22].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.JeratoO95_1125Mission[i] == false)
                {
                    planetMissionType[22].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[22].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[22].PlanetMissionList.MissionLevel;
                    planetMissionType[22].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[22].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(22, i);
                    PlanetBattleText(22, i);
                }
            }
            if (planetMissionType[22].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
            else if (planetMissionType[22].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1024) //제라토 O95-2252
        {
            for (int i = 0; i < planetMissionType[23].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.JeratoO95_2252Mission[i] == false)
                {
                    planetMissionType[23].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[23].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[23].PlanetMissionList.MissionLevel;
                    planetMissionType[23].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[23].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(23, i);
                    PlanetBattleText(23, i);
                }
            }
            if (planetMissionType[23].PlanetMissionList.missionTableList.Length == 5)
                MissionTableSize.sizeDelta = new Vector2(361, 800);
            else if (planetMissionType[23].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
            else if (planetMissionType[23].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
        }
        else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber == 1025) //제라토 O95-8510
        {
            for (int i = 0; i < planetMissionType[24].PlanetMissionList.missionTableList.Length; i++)
            {
                if (MissionCompleteManager.MCMInstance.JeratoO95_8510Mission[i] == false)
                {
                    planetMissionType[24].PlanetMissionList.missionTableList[i].MissionTablePrefab.SetActive(true);
                    planetMissionType[24].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionLevel = planetMissionType[24].PlanetMissionList.MissionLevel;
                    planetMissionType[24].PlanetMissionList.missionTableList[i].MissionTablePrefab.GetComponent<MissionTableInformation>().FinishSpawnNumber = planetMissionType[24].PlanetMissionList.missionTableList[i].FinishSpawnNumber;
                    PlanetAreaText(24, i);
                    PlanetBattleText(24, i);
                }
            }
            if (planetMissionType[24].PlanetMissionList.missionTableList.Length == 5)
                MissionTableSize.sizeDelta = new Vector2(361, 800);
            else if (planetMissionType[24].PlanetMissionList.missionTableList.Length == 4)
                MissionTableSize.sizeDelta = new Vector2(361, 650);
            else if (planetMissionType[24].PlanetMissionList.missionTableList.Length <= 3)
                MissionTableSize.sizeDelta = new Vector2(361, 0);
        }
    }

    //지역 이름 텍스트 출력
    void PlanetAreaText(int MissionAreaNumber, int number)
    {
        if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionAreaType == PlanetMissionList.MissionTable.MissionArea.CityOnFire)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("City area");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("도시 지역");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea = 1;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionAreaType == PlanetMissionList.MissionTable.MissionArea.AtmosphereStation)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("Atmosphere Station");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("대기권 스테이션");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea = 2;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionAreaType == PlanetMissionList.MissionTable.MissionArea.CityAtAisle)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("Facility Inside Aisle");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].AreaName.text = string.Format("시설 내부 통로");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionArea = 3;
        }
    }

    //전투 유형 텍스트 출력
    void PlanetBattleText(int MissionAreaNumber, int number)
    {
        if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.ConquestFromContros)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Contros Area Advance Battle");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("컨트로스 지역 진격전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 1;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.DefenceFromContros)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Position Defence Battle");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("진지 방어전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 2;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.ConquestFromZonbie)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Infection Removal Battle");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("감염 제거전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 3;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.DefenceFromZonbie)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Infection Restraint Battle");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("감염 저지전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 4;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.ConquestFromSloriusShip)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Slorius Flagship Infiltration Operation");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("슬로리어스 기함 침투적전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 100;
        }
        else if (planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].missionType == PlanetMissionList.MissionTable.MissionType.ConquestFromKantakriShip)
        {
            if (WordPrintSystem.LanguageType == 1)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("Kantakri Flagship Infiltration Operation");
            else if (WordPrintSystem.LanguageType == 2)
                planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionName.text = string.Format("칸타크리 기함 침투적전");
            planetMissionType[MissionAreaNumber].PlanetMissionList.missionTableList[number].MissionTablePrefab.GetComponent<MissionTableInformation>().MissionType = 101;
        }
    }
}