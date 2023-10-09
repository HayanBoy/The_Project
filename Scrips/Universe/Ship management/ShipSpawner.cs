using Cinemachine;
using System.Collections;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [Header("스크립트")]
    public MultiFlagshipSystem MultiFlagshipSystem;
    public UIControlSystem UIControlSystem;
    public UniverseMapSystem UniverseMapSystem;
    DataSaveManager DataSaveManager;

    [Header("스폰 지역")]
    public static ShipSpawner Spawner;
    public CinemachineVirtualCamera Camera;
    public Transform StartFlagshipPosition;
    public Transform StartPos;
    public Transform RandomArea;
    public Transform RandomArea2;

    [Header("나리하 함선 프리팹")]
    //나리하
    public GameObject NarihaFlagship1;
    public GameObject NarihaFormationShip1;
    public GameObject NarihaShieldShip1;
    public GameObject NarihaCarrier1;

    [Header("슬로리어스 함선 프리팹")]
    //슬로리어스
    public GameObject SloriusFlagship1;
    public GameObject SloriusFormationShip1;

    [Header("칸타크리 함선 프리팹")]
    //칸타크리
    public GameObject KantakriFlagship1;
    public GameObject KantakriFormationShip1;

    public bool NewDeployed = false; //새로 함선을 생산해서 배치하는 경우에만 해당

    void Awake()
    {
        if (Spawner == null)
            Spawner = this;
        else if (Spawner != this)
            Destroy(gameObject);

        DataSaveManager = FindObjectOfType<DataSaveManager>();
    }

    //기함 새배치
    public void DelpoyFlagship()
    {
        GameObject NarihaFlagshipAdd = Instantiate(NarihaFlagship1, transform.position, transform.rotation);
        FlagshipDeploy(NarihaFlagshipAdd, ShipManager.instance.FlagShipList.Count);

        float EnemyshipRange = Random.Range(500, 1000);
        Vector3 EnemyShipLocation;
        EnemyShipLocation = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
            Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), transform.position.z);
        NarihaFlagshipAdd.transform.position = EnemyShipLocation;

        WeaponUnlockManager.instance.FlagshipFirstSlotUnlock.Add(false);
        WeaponUnlockManager.instance.FlagshipSecondSlotUnlock.Add(false);
        WeaponUnlockManager.instance.FlagshipThirdSlotUnlock.Add(false);

        ShipManager.instance.AmountOfFormationShip.Add(0);
        ShipManager.instance.AmountOfShieldShip.Add(0);
        ShipManager.instance.AmountOfCarrier.Add(0);
        NarihaFlagshipAdd.GetComponent<FollowShipManager>().FormationStorage = 2;

        NarihaFlagshipAdd.GetComponent<ShipRTS>().FastWarpLoactionGet(StartFlagshipPosition.position);
        ScoreManager.instance.GetNarihaFlagshipCnt++;
    }

    //함선 생산후, 지정된 기함으로 배치
    public void DeployShip(GameObject Flagship, int ShipNumber, int number)
    {
        for (int i = 0; i < number; i++)
        {
            if (ShipNumber == 1)
            {
                FormationShipDeploy(Flagship, Flagship.GetComponent<FollowShipManager>().ShipList.Count, Flagship.GetComponent<MoveVelocity>().FlagshipNumber);
            }
            else if (ShipNumber == 2)
            {
                ShieldShipDeploy(Flagship, Flagship.GetComponent<FollowShipManager>().ShipList.Count, Flagship.GetComponent<MoveVelocity>().FlagshipNumber);
            }
            else if (ShipNumber == 3)
            {
                CarrierDeploy(Flagship, Flagship.GetComponent<FollowShipManager>().ShipList.Count, Flagship.GetComponent<MoveVelocity>().FlagshipNumber);
            }
        }
        NewDeployed = false;
    }

    //기함 배치
    void FlagshipDeploy(GameObject Flagship, int FlagshipNumber)
    {
        if (BattleSave.Save1.GroundBattleCount > 0) //보병전 종료 직후, 보병전 진입 직전의 세이브 데이터 받기
        {
            Flagship.GetComponent<FlagshipAttackSkill>().SkillType = BattleSave.Save1.FirstSkillType[0];
            Flagship.GetComponent<FlagshipAttackSkill>().SkillNumber = BattleSave.Save1.FirstSkillNumber[0];
            Flagship.GetComponent<FlagshipAttackSkill>().SecondSkillType = BattleSave.Save1.SecondSkillType[0];
            Flagship.GetComponent<FlagshipAttackSkill>().SecondSkillNumber = BattleSave.Save1.SecondSkillNumber[0];
            Flagship.GetComponent<FlagshipAttackSkill>().ThirdSkillType = BattleSave.Save1.ThirdSkillType[0];
            Flagship.GetComponent<FlagshipAttackSkill>().ThirdSkillNumber = BattleSave.Save1.ThirdSkillNumber[0];

            Flagship.GetComponent<FollowShipManager>().FormationStorage = BattleSave.Save1.FormationStorage[0];
            Flagship.GetComponent<TearSloriusFlagship1>().ShieldDown = BattleSave.Save1.FlagshipShieldDown[0];
            Flagship.GetComponent<HullSloriusFlagship1>().hitPoints = BattleSave.Save1.FlagshipHull[0];

            Flagship.GetComponent<HullSloriusFlagship1>().Main1Left1Down = BattleSave.Save1.Main1Left1DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Left2Down = BattleSave.Save1.Main1Left2DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Right1Down = BattleSave.Save1.Main1Right1DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Right2Down = BattleSave.Save1.Main1Right2DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Left1Down = BattleSave.Save1.Main2Left1DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Left2Down = BattleSave.Save1.Main2Left2DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Right1Down = BattleSave.Save1.Main2Right1DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Right2Down = BattleSave.Save1.Main2Right2DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main3Left1Down = BattleSave.Save1.Main3Left1DownFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main3Right1Down = BattleSave.Save1.Main3Right1DownFlagship[0];

            Flagship.GetComponent<TearSloriusFlagship1>().Main1Left1HP = BattleSave.Save1.Main1Left1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Left2HP = BattleSave.Save1.Main1Left2HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Right1HP = BattleSave.Save1.Main1Right1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Right2HP = BattleSave.Save1.Main1Right2HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Left1HP = BattleSave.Save1.Main2Left1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Left2HP = BattleSave.Save1.Main2Left2HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Right1HP = BattleSave.Save1.Main2Right1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Right2HP = BattleSave.Save1.Main2Right2HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main3Left1HP = BattleSave.Save1.Main3Left1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main3Right1HP = BattleSave.Save1.Main3Right1HPFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().GetBattleData();

            for (int Codex = 0; Codex < BattleSave.Save1.FormationStorage[0]; Codex++)
            {
                if (Codex == 0)
                    Flagship.GetComponent<FollowShipManager>().Location1.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 1)
                    Flagship.GetComponent<FollowShipManager>().Location2.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 2)
                    Flagship.GetComponent<FollowShipManager>().Location3.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 3)
                    Flagship.GetComponent<FollowShipManager>().Location4.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 4)
                    Flagship.GetComponent<FollowShipManager>().Location5.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 5)
                    Flagship.GetComponent<FollowShipManager>().Location6.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 6)
                    Flagship.GetComponent<FollowShipManager>().Location7.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 7)
                    Flagship.GetComponent<FollowShipManager>().Location8.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 8)
                    Flagship.GetComponent<FollowShipManager>().Location9.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 9)
                    Flagship.GetComponent<FollowShipManager>().Location10.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 10)
                    Flagship.GetComponent<FollowShipManager>().Location11.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 11)
                    Flagship.GetComponent<FollowShipManager>().Location12.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 12)
                    Flagship.GetComponent<FollowShipManager>().Location13.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 13)
                    Flagship.GetComponent<FollowShipManager>().Location14.position = BattleSave.Save1.FlagshipFormationsCodex[0];
                else if (Codex == 14)
                    Flagship.GetComponent<FollowShipManager>().Location15.position = BattleSave.Save1.FlagshipFormationsCodex[0];

                BattleSave.Save1.FlagshipFormationsCodex.Remove(BattleSave.Save1.FlagshipFormationsCodex[0]);
            }

            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 1)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 2)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 3)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 4)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 5)
                {
                    Flagship.GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Flagship.GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Flagship.GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                BattleSave.Save1.CannonType.Remove(BattleSave.Save1.CannonType[0]); //편대함 함포 데이터를 위한 데이터 미리 삭제
                BattleSave.Save1.AttackTime.Remove(BattleSave.Save1.AttackTime[0]);
            }

            //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프 시작 시간을 그대로 실행
            Flagship.GetComponent<MoveVelocity>().WarpDriveReady = BattleSave.Save1.WarpDriveReady[0];
            if (Flagship.GetComponent<MoveVelocity>().WarpDriveReady == true)
            {
                Flagship.GetComponent<MoveVelocity>().SetVelocity(BattleSave.Save1.DestinationArea[0]);
                StartCoroutine(Flagship.GetComponent<ShipRTS>().FlagShipStartWarp(BattleSave.Save1.WarpStartTime[0]));
                StartCoroutine(Flagship.GetComponent<ShipRTS>().FollowShipStartWarp(BattleSave.Save1.WarpStartTime[0]));
            }

            //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프를 그대로 유지(만약 워프 도착 직전일 경우에도 이 부분이 발동되며 자동으로 워프 정지 준비를 실시하도록 조취되었음)
            Flagship.GetComponent<MoveVelocity>().WarpDriveActive = BattleSave.Save1.WarpDriveActive[0];
            if (Flagship.GetComponent<MoveVelocity>().WarpDriveActive == true)
            {
                Flagship.GetComponent<MoveVelocity>().SetVelocity(BattleSave.Save1.DestinationArea[0]);
                Flagship.GetComponent<MoveVelocity>().WarpSpeedUp(true);
            }

            //함포 무력화 상태
            for (int e = 0; e < BattleSave.Save1.FlagshipTurretDownList.Count; e++)
            {
                Flagship.GetComponent<HullSloriusFlagship1>().TurretDownList.Add(BattleSave.Save1.FlagshipTurretDownList[0]);
                BattleSave.Save1.FlagshipTurretDownList.Remove(BattleSave.Save1.FlagshipTurretDownList[0]);
            }
            Flagship.GetComponent<HullSloriusFlagship1>().BringTurretDestroy();
        }
        else if (BattleSave.Save1.GroundBattleCount == 0) //새로 배치한 기함에만 해당
        {
            Flagship.GetComponent<HullSloriusFlagship1>().UpgradePatch();
        }

        Flagship.GetComponent<MoveVelocity>().FlagshipNumber = FlagshipNumber;
        ShipManager.instance.FlagShipList.Add(Flagship);
        /*
        if (FlagshipNumber == 0)
            MultiFlagshipSystem.Player1Name.text = ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FlagshipNameInformation>().PlayerName;
        else if (FlagshipNumber == 1)
            MultiFlagshipSystem.Player2Name.text = ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FlagshipNameInformation>().PlayerName;
        else if (FlagshipNumber == 2)
            MultiFlagshipSystem.Player3Name.text = ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FlagshipNameInformation>().PlayerName;
        else if (FlagshipNumber == 3)
            MultiFlagshipSystem.Player4Name.text = ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FlagshipNameInformation>().PlayerName;
        else if (FlagshipNumber == 4)
            MultiFlagshipSystem.Player5Name.text = ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FlagshipNameInformation>().PlayerName;*/
    }

    //편대함 배치
    void FormationShipDeploy(GameObject Flagship, int FormationNumber, int FlagshipNumber)
    {
        GameObject NarihaFormationShipAdd = Instantiate(NarihaFormationShip1, transform.position, transform.rotation);

        if (NewDeployed == true)
        {
            float EnemyshipRange = Random.Range(500, 1000);
            Vector3 EnemyShipLocation;
            EnemyShipLocation = new Vector3(Random.Range(Flagship.transform.position.x - EnemyshipRange, Flagship.transform.position.x + EnemyshipRange),
                Random.Range(Flagship.transform.position.y - EnemyshipRange, Flagship.transform.position.y + EnemyshipRange), transform.position.z);
            NarihaFormationShipAdd.transform.position = EnemyShipLocation;
            NarihaFormationShipAdd.GetComponent<HullSloriusFormationShip1>().UpgradePatch();

            FormationNumberSet(Flagship, NarihaFormationShipAdd, FlagshipNumber, FormationNumber);
            ScoreManager.instance.GetNarihaFormationShipCnt++;
        }
        else
        {
            SpawnShipDeploy(Flagship, NarihaFormationShipAdd, FlagshipNumber, FormationNumber);
        }

        if (NewDeployed == true)
        {
            NarihaFormationShipAdd.GetComponent<MoveVelocity>().TransferWarp();
        }

        if (BattleSave.Save1.GroundBattleCount > 0)
        {
            FormationShipBattleDataInput(NarihaFormationShipAdd, FormationNumber);
        }
    }

    //방패함 배치
    void ShieldShipDeploy(GameObject Flagship, int FormationNumber, int FlagshipNumber)
    {
        GameObject NarihaShieldShipAdd = Instantiate(NarihaShieldShip1, transform.position, transform.rotation);
        if (NewDeployed == true)
        {
            float EnemyshipRange = Random.Range(500, 1000);
            Vector3 EnemyShipLocation;
            EnemyShipLocation = new Vector3(Random.Range(Flagship.transform.position.x - EnemyshipRange, Flagship.transform.position.x + EnemyshipRange),
                Random.Range(Flagship.transform.position.y - EnemyshipRange, Flagship.transform.position.y + EnemyshipRange), transform.position.z);
            NarihaShieldShipAdd.transform.position = EnemyShipLocation;
            NarihaShieldShipAdd.GetComponent<HullSloriusFormationShip1>().UpgradePatch();

            FormationNumberSet(Flagship, NarihaShieldShipAdd, FlagshipNumber, FormationNumber);
            ScoreManager.instance.GetNarihaTacticalShipCnt++;
        }
        else
        {
            SpawnShipDeploy(Flagship, NarihaShieldShipAdd, FlagshipNumber, FormationNumber);
        }

        if (NewDeployed == true)
        {
            NarihaShieldShipAdd.GetComponent<MoveVelocity>().TransferWarp();
        }

        if (BattleSave.Save1.GroundBattleCount > 0)
        {
            FormationShipBattleDataInput(NarihaShieldShipAdd, FormationNumber);
        }
    }

    //우주모함 배치
    void CarrierDeploy(GameObject Flagship, int FormationNumber, int FlagshipNumber)
    {
        GameObject NarihaCarrierAdd = Instantiate(NarihaCarrier1, transform.position, transform.rotation);
        if (NewDeployed == true)
        {
            float EnemyshipRange = Random.Range(500, 1000);
            Vector3 EnemyShipLocation;
            EnemyShipLocation = new Vector3(Random.Range(Flagship.transform.position.x - EnemyshipRange, Flagship.transform.position.x + EnemyshipRange),
                Random.Range(Flagship.transform.position.y - EnemyshipRange, Flagship.transform.position.y + EnemyshipRange), transform.position.z);
            NarihaCarrierAdd.transform.position = EnemyShipLocation;
            NarihaCarrierAdd.GetComponent<HullSloriusFormationShip1>().UpgradePatch();

            FormationNumberSet(Flagship, NarihaCarrierAdd, FlagshipNumber, FormationNumber);
            ScoreManager.instance.GetNarihaTacticalShipCnt++;
        }
        else
        {
            SpawnShipDeploy(Flagship, NarihaCarrierAdd, FlagshipNumber, FormationNumber);
        }

        if (NewDeployed == true)
        {
            NarihaCarrierAdd.GetComponent<MoveVelocity>().TransferWarp();
        }

        if (BattleSave.Save1.GroundBattleCount > 0)
        {
            FormationShipBattleDataInput(NarihaCarrierAdd, FormationNumber);
        }
    }

    //적 기함 배치
    void EnemyFlagshipDeploy(GameObject Flagship)
    {
        if (BattleSave.Save1.GroundBattleCount > 0) //보병전 종료 직후, 보병전 진입 직전의 세이브 데이터 받기
        {
            Flagship.GetComponent<EnemyShipLevelInformation>().isBattleSite = BattleSave.Save1.EnemyIsBattleSite[0];
            Flagship.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = BattleSave.Save1.EnemyBattleSiteNumber[0];
            UniverseMapSystem.BattleSiteGet(Flagship, BattleSave.Save1.EnemyBattleSiteNumber[0]);

            Flagship.GetComponent<TearSloriusFlagship1>().ShieldDown = BattleSave.Save1.EnemyFlagshipShieldDown[0];
            Flagship.GetComponent<HullSloriusFlagship1>().hitPoints = BattleSave.Save1.EnemyFlagshipHull[0];
            if (Flagship.GetComponent<EnemyShipBehavior>().NationType == 2)
                Flagship.GetComponent<ShieldSloriusShip>().ShieldPoints = BattleSave.Save1.EnemyFlagshipShield[0];

            Flagship.GetComponent<HullSloriusFlagship1>().Main1Left1Down = BattleSave.Save1.Main1Left1DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Left2Down = BattleSave.Save1.Main1Left2DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Right1Down = BattleSave.Save1.Main1Right1DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main1Right2Down = BattleSave.Save1.Main1Right2DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Left1Down = BattleSave.Save1.Main2Left1DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Left2Down = BattleSave.Save1.Main2Left2DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Right1Down = BattleSave.Save1.Main2Right1DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main2Right2Down = BattleSave.Save1.Main2Right2DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main3Left1Down = BattleSave.Save1.Main3Left1DownEnemyFlagship[0];
            Flagship.GetComponent<HullSloriusFlagship1>().Main3Right1Down = BattleSave.Save1.Main3Right1DownEnemyFlagship[0];

            Flagship.GetComponent<TearSloriusFlagship1>().Main1Left1HP = BattleSave.Save1.Main1Left1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Left2HP = BattleSave.Save1.Main1Left2HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Right1HP = BattleSave.Save1.Main1Right1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main1Right2HP = BattleSave.Save1.Main1Right2HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Left1HP = BattleSave.Save1.Main2Left1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Left2HP = BattleSave.Save1.Main2Left2HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Right1HP = BattleSave.Save1.Main2Right1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main2Right2HP = BattleSave.Save1.Main2Right2HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main3Left1HP = BattleSave.Save1.Main3Left1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().Main3Right1HP = BattleSave.Save1.Main3Right1HPEnemyFlagship[0];
            Flagship.GetComponent<TearSloriusFlagship1>().GetBattleData();

            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else if (i == 1)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else if (i == 2)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else if (i == 3)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else if (i == 4)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                else if (i == 5)
                {
                    Flagship.GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                    Flagship.GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonReinput();
                }
                BattleSave.Save1.EnemyCannonType.Remove(BattleSave.Save1.EnemyCannonType[0]); //편대함 함포 데이터를 위한 데이터 미리 삭제
                BattleSave.Save1.EnemyAttackTime.Remove(BattleSave.Save1.EnemyAttackTime[0]);
            }

            if (BattleSave.Save1.SelectedEnemyFlagship[0] == true) //기함 침투전에 선택된 기함에서 임무가 완료되었을 경우, 해당 기함의 포탑 기능 끄기(무력화 처리)
            {
                if (BattleSave.Save1.FlagshipMissionSuccessed > 0)
                {
                    BattleSave.Save1.FlagshipMissionSuccessed = 0;
                    BattleSave.Save1.EnemyFlagshipDown[0] = true;
                }
            }
            if (BattleSave.Save1.EnemyFlagshipDown.Count > 0)
            {
                if (BattleSave.Save1.EnemyFlagshipDown[0] == true)
                {
                    Flagship.GetComponent<TransferColliderFlagship>().isDowned = true;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().canAttack = false;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().canAttack = false;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().canAttack = false;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().canAttack = false;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().canAttack = false;
                    Flagship.GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().canAttack = false;
                }
            }

            //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프를 그대로 유지(만약 워프 도착 직전일 경우에도 이 부분이 발동되며 자동으로 워프 정지 준비를 실시하도록 조취되었음)
            Flagship.GetComponent<EnemyShipBehavior>().WarpDriveActive = BattleSave.Save1.EnemyWarpDriveActive[0];
            if (Flagship.GetComponent<EnemyShipBehavior>().WarpDriveActive == true)
            {
                Flagship.GetComponent<EnemyShipBehavior>().SetVelocity(BattleSave.Save1.EnemyDestinationArea[0]);
                Flagship.GetComponent<EnemyShipBehavior>().WarpSpeedUp(true);
            }

            //함포 무력화 상태
            for (int e = 0; e < BattleSave.Save1.EnemyFlagshipTurretDownList.Count; e++)
            {
                Flagship.GetComponent<HullSloriusFlagship1>().TurretDownList.Add(BattleSave.Save1.EnemyFlagshipTurretDownList[0]);
                BattleSave.Save1.EnemyFlagshipTurretDownList.Remove(BattleSave.Save1.EnemyFlagshipTurretDownList[0]);
            }
            Flagship.GetComponent<HullSloriusFlagship1>().BringTurretDestroy();

            if (Flagship.GetComponent<EnemyShipBehavior>().NationType == 2)
                AIShipManager.instance.SloriusFlagShipList.Add(Flagship);
            else if (Flagship.GetComponent<EnemyShipBehavior>().NationType == 1)
                AIShipManager.instance.KantakriFlagShipList.Add(Flagship);
            AIShipManager.instance.EnemiesFlagShipList.Add(Flagship);
        }
    }

    //적 기함 소속 편대함 배치
    void EnemyFormationShipDeploy(GameObject Flagship, int FormationNumber)
    {
        if (BattleSave.Save1.EnemyNationType[FormationNumber] == 2) //슬로리어스 기함 소속
        {
            GameObject EnemiesFormationshipAdd = Instantiate(SloriusFormationShip1, transform.position, transform.rotation);
            EnemiesFormationshipAdd.transform.position = Flagship.transform.position;

            EnemiesFormationshipAdd.GetComponent<EnemyShipBehavior>().MyFlagship = Flagship;
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList.Add(EnemiesFormationshipAdd);
            Flagship.GetComponent<EnemyFollowShipManager>().ShipAccount++;
            Flagship.GetComponent<EnemyWarpFormationControl>().GetFormation(Flagship, FormationNumber);

            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                EnemyFormationShipBattleDataInput(EnemiesFormationshipAdd, FormationNumber);
            }
        }
        else if (BattleSave.Save1.EnemyNationType[FormationNumber] == 3) //칸타크리 기함 소속
        {
            GameObject EnemiesFormationshipAdd = Instantiate(KantakriFormationShip1, transform.position, transform.rotation);
            EnemiesFormationshipAdd.transform.position = Flagship.transform.position;

            EnemiesFormationshipAdd.GetComponent<EnemyShipBehavior>().MyFlagship = Flagship;
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList.Add(EnemiesFormationshipAdd);
            Flagship.GetComponent<EnemyFollowShipManager>().ShipAccount++;
            Flagship.GetComponent<EnemyWarpFormationControl>().GetFormation(Flagship, FormationNumber);

            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                EnemyFormationShipBattleDataInput(EnemiesFormationshipAdd, FormationNumber);
            }
        }
    }

    //적 단독 편대함 배치
    void SingleEnemyFormationShipDeploy(int number)
    {
        if (BattleSave.Save1.EnemyNationType[number] == 2)
        {
            GameObject EnemiesFormationshipAdd = Instantiate(SloriusFormationShip1, BattleSave.Save1.EnemyFormationShipLocation[number], BattleSave.Save1.EnemyFormationShipRotanion[number]);
            EnemiesFormationshipAdd.GetComponent<EnemyShipBehavior>().FormationOn = false;
            EnemyFormationShipBattleDataInput(EnemiesFormationshipAdd, number);
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                AIShipManager.instance.EnemiesFormationShipList.Add(EnemiesFormationshipAdd);
            }
        }
        else if (BattleSave.Save1.EnemyNationType[number] == 3)
        {
            GameObject EnemiesFormationshipAdd = Instantiate(KantakriFormationShip1, BattleSave.Save1.EnemyFormationShipLocation[number], BattleSave.Save1.EnemyFormationShipRotanion[number]);
            EnemiesFormationshipAdd.GetComponent<EnemyShipBehavior>().FormationOn = false;
            EnemyFormationShipBattleDataInput(EnemiesFormationshipAdd, number);
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                AIShipManager.instance.EnemiesFormationShipList.Add(EnemiesFormationshipAdd);
            }
        }
    }

    void Start()
    {
        if (Camera != null)
        {
            //보병전을 실시한 뒤, 다시 함대전으로 복귀했을 경우, 보병전 실행 직전에 세이브된 전투 데이터에 맞게 함선을 스폰 시킨뒤, 각 함선마다 이전 세이브 정보를 모두 받는다.
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                for (int number = 0; number < BattleSave.Save1.CountOfFlagship; number++)
                {
                    ShipManager.instance.AmountOfFormationShip.Add(0);
                    ShipManager.instance.AmountOfShieldShip.Add(0);
                    ShipManager.instance.AmountOfCarrier.Add(0);

                    GameObject NarihaFlagshipAdd = Instantiate(NarihaFlagship1, BattleSave.Save1.FlagshipLocation[0], BattleSave.Save1.FlagshipRotanion[0]); //데이터 장소에 맞게 스폰, 안 그러면 강제로 원본 장소로 이동된다.
                    FlagshipDeploy(NarihaFlagshipAdd, number);
                    FlagshipDataRemove(); //편대함 스폰을 실시하기 이전에 현재 기함의 받은 데이터를 먼저 삭제하여 다음 기함에 데이터를 정상으로 받을 수 있도록 조취

                    for (int i = 0; i < BattleSave.Save1.CountOfFormationShip[number]; i++) //기함의 배열 번호에 맞게 배치되었던 함선을 그대로 스폰
                    {
                        if (BattleSave.Save1.FormationType[i] == 2)
                            FormationShipDeploy(NarihaFlagshipAdd, i, number);
                        else if (BattleSave.Save1.FormationType[i] == 3)
                            ShieldShipDeploy(NarihaFlagshipAdd, i, number);
                        else if (BattleSave.Save1.FormationType[i] == 4)
                            CarrierDeploy(NarihaFlagshipAdd, i, number);

                        if (i == BattleSave.Save1.CountOfFormationShip[number] - 1) //편대함 스폰 및 데이터 이전이 모두 완료
                        {
                            if (NarihaFlagshipAdd.GetComponent<MoveVelocity>().WarpDriveActive == true) //워프 배열이 모두 배치가 완료되는 시점이므로 이 때 배열전체 위치를 옮긴다.
                            {
                                NarihaFlagshipAdd.GetComponent<FollowShipManager>().WarpFormation.transform.position = NarihaFlagshipAdd.GetComponent<MoveVelocity>().DestinationArea;
                                NarihaFlagshipAdd.GetComponent<FollowShipManager>().WarpFormation.transform.rotation = NarihaFlagshipAdd.transform.rotation;
                            }

                            //편대함 데이터를 모두 이전을 완료했으므로, 정해진 분량의 편대함 배열 번호까지만 데이터 리스트 삭제(다음 함대의 리스트를 정상으로 사용하기 위함)
                            for (int j = 0; j < BattleSave.Save1.CountOfFormationShip[number]; j++)
                            {
                                FormationShipDataRemove();
                            }
                        }
                    }

                    //마지막 선택된 기함을 기준으로 카메라 선택
                    if (number == BattleSave.Save1.SelectedshipNumber)
                    {
                        Camera.Follow = NarihaFlagshipAdd.transform;
                        ShipManager.instance.SelectedFlagShip.Add(ShipManager.instance.FlagShipList[number]);
                        ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().Seleted = true; //해당 기함이 선택되었음을 리스트에 표시
                    }
                    UniverseMapSystem.GetPlayerFlagship();
                }

                //적 기함이 존재할 경우, 스폰 시작
                if (BattleSave.Save1.EnemiesFlagShipList > 0)
                {
                    for (int number = 0; number < BattleSave.Save1.EnemiesFlagShipList; number++)
                    {
                        //데이터 장소에 맞게 스폰, 안 그러면 강제로 원본 장소로 이동된다.
                        if (BattleSave.Save1.EnemyNationType[number] == 2)
                        {
                            GameObject EnemiesFlagshipAdd = Instantiate(SloriusFlagship1, BattleSave.Save1.EnemyFlagshipLocation[number], BattleSave.Save1.EnemyFlagshipRotanion[number]);
                            EnemyFlagshipDeploy(EnemiesFlagshipAdd);
                            EnemyFlagshipDataRemove();
                            if (BattleSave.Save1.CountOfEnemyFormationShip[number] > 0)
                                GetEnemyFormaion(EnemiesFlagshipAdd, number);
                        }
                        else if (BattleSave.Save1.EnemyNationType[number] == 3)
                        {
                            GameObject EnemiesFlagshipAdd = Instantiate(KantakriFlagship1, BattleSave.Save1.EnemyFlagshipLocation[number], BattleSave.Save1.EnemyFlagshipRotanion[number]);
                            EnemyFlagshipDeploy(EnemiesFlagshipAdd);
                            EnemyFlagshipDataRemove();
                            if (BattleSave.Save1.CountOfEnemyFormationShip[number] > 0)
                                GetEnemyFormaion(EnemiesFlagshipAdd, number);
                        }
                    }
                }

                //단독 적 편대함이 존재할 경우, 스폰 시작
                if (BattleSave.Save1.EnemiesFormationShipList > 0)
                {
                    for (int number = 0; number < BattleSave.Save1.EnemiesFormationShipList; number++)
                    {
                        SingleEnemyFormationShipDeploy(number);

                        if (number == BattleSave.Save1.EnemiesFormationShipList - 1) //편대함 스폰 및 데이터 이전이 모두 완료
                        {
                            //편대함 데이터를 모두 이전을 완료했으므로, 정해진 분량의 편대함 배열 번호까지만 데이터 리스트 삭제(다음 함대의 리스트를 정상으로 사용하기 위함)
                            for (int j = 0; j < BattleSave.Save1.EnemiesFormationShipList; j++)
                            {
                                EnemyFormationShipDataRemove();
                            }
                        }
                    }
                }
            }
            else //처음 시작용 함대 스폰(우주맵에서는 테스트 함대로 사용 가능)
            {
                for (int number = 0; number < 1; number++)
                {
                    ShipManager.instance.AmountOfFormationShip.Add(0);
                    ShipManager.instance.AmountOfShieldShip.Add(0);
                    ShipManager.instance.AmountOfCarrier.Add(0);

                    WeaponUnlockManager.instance.FlagshipFirstSlotUnlock.Add(false);
                    WeaponUnlockManager.instance.FlagshipSecondSlotUnlock.Add(false);
                    WeaponUnlockManager.instance.FlagshipThirdSlotUnlock.Add(false);

                    if (number == 0)
                    {
                        GameObject NarihaFlagshipAdd = Instantiate(NarihaFlagship1, StartPos.position, StartPos.rotation);
                        FlagshipDeploy(NarihaFlagshipAdd, number);
                        Camera.Follow = NarihaFlagshipAdd.transform;
                        ShipManager.instance.SelectedFlagShip.Add(ShipManager.instance.FlagShipList[number]);
                        ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().Seleted = true; //해당 기함이 선택되었음을 리스트에 표시
                        for (int i = 0; i < 4; i++)
                        {
                            FormationShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        /*
                        for (int i = 0; i < 2; i++)
                        {
                            ShieldShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        for (int i = 0; i < 2; i++)
                        {
                            CarrierDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }*/
                    }
                    else if (number == 1)
                    {
                        GameObject NarihaFlagshipAdd = Instantiate(NarihaFlagship1, RandomArea.position, RandomArea.rotation);
                        FlagshipDeploy(NarihaFlagshipAdd, number);
                        for (int i = 0; i < 2; i++)
                        {
                            FormationShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            ShieldShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            CarrierDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                    }
                    else if (number == 2)
                    {
                        GameObject NarihaFlagshipAdd = Instantiate(NarihaFlagship1, RandomArea2.position, RandomArea2.rotation);
                        FlagshipDeploy(NarihaFlagshipAdd, number);
                        for (int i = 0; i < 2; i++)
                        {
                            FormationShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            ShieldShipDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            CarrierDeploy(NarihaFlagshipAdd, NarihaFlagshipAdd.GetComponent<FollowShipManager>().ShipList.Count, number);
                        }
                    }

                    UniverseMapSystem.GetPlayerFlagship();
                }
            }
        }
    }

    //기함 전투 데이터 삭제
    public void FlagshipDataRemove()
    {
        BattleSave.Save1.FirstSkillType.Remove(BattleSave.Save1.FirstSkillType[0]);
        BattleSave.Save1.FirstSkillNumber.Remove(BattleSave.Save1.FirstSkillNumber[0]);
        BattleSave.Save1.SecondSkillType.Remove(BattleSave.Save1.SecondSkillType[0]);
        BattleSave.Save1.SecondSkillNumber.Remove(BattleSave.Save1.SecondSkillNumber[0]);
        BattleSave.Save1.ThirdSkillType.Remove(BattleSave.Save1.ThirdSkillType[0]);
        BattleSave.Save1.ThirdSkillNumber.Remove(BattleSave.Save1.ThirdSkillNumber[0]);

        BattleSave.Save1.FlagshipLocation.Remove(BattleSave.Save1.FlagshipLocation[0]);
        BattleSave.Save1.FlagshipRotanion.Remove(BattleSave.Save1.FlagshipRotanion[0]);

        BattleSave.Save1.FormationStorage.Remove(BattleSave.Save1.FormationStorage[0]);
        BattleSave.Save1.FlagshipShieldDown.Remove(BattleSave.Save1.FlagshipShieldDown[0]);
        BattleSave.Save1.FlagshipHull.Remove(BattleSave.Save1.FlagshipHull[0]);

        BattleSave.Save1.Main1Left1DownFlagship.Remove(BattleSave.Save1.Main1Left1DownFlagship[0]);
        BattleSave.Save1.Main1Left2DownFlagship.Remove(BattleSave.Save1.Main1Left2DownFlagship[0]);
        BattleSave.Save1.Main1Right1DownFlagship.Remove(BattleSave.Save1.Main1Right1DownFlagship[0]);
        BattleSave.Save1.Main1Right2DownFlagship.Remove(BattleSave.Save1.Main1Right2DownFlagship[0]);
        BattleSave.Save1.Main2Left1DownFlagship.Remove(BattleSave.Save1.Main2Left1DownFlagship[0]);
        BattleSave.Save1.Main2Left2DownFlagship.Remove(BattleSave.Save1.Main2Left2DownFlagship[0]);
        BattleSave.Save1.Main2Right1DownFlagship.Remove(BattleSave.Save1.Main2Right1DownFlagship[0]);
        BattleSave.Save1.Main2Right2DownFlagship.Remove(BattleSave.Save1.Main2Right2DownFlagship[0]);
        BattleSave.Save1.Main3Left1DownFlagship.Remove(BattleSave.Save1.Main3Left1DownFlagship[0]);
        BattleSave.Save1.Main3Right1DownFlagship.Remove(BattleSave.Save1.Main3Right1DownFlagship[0]);

        BattleSave.Save1.Main1Left1HPFlagship.Remove(BattleSave.Save1.Main1Left1HPFlagship[0]);
        BattleSave.Save1.Main1Left2HPFlagship.Remove(BattleSave.Save1.Main1Left2HPFlagship[0]);
        BattleSave.Save1.Main1Right1HPFlagship.Remove(BattleSave.Save1.Main1Right1HPFlagship[0]);
        BattleSave.Save1.Main1Right2HPFlagship.Remove(BattleSave.Save1.Main1Right2HPFlagship[0]);
        BattleSave.Save1.Main2Left1HPFlagship.Remove(BattleSave.Save1.Main2Left1HPFlagship[0]);
        BattleSave.Save1.Main2Left2HPFlagship.Remove(BattleSave.Save1.Main2Left2HPFlagship[0]);
        BattleSave.Save1.Main2Right1HPFlagship.Remove(BattleSave.Save1.Main2Right1HPFlagship[0]);
        BattleSave.Save1.Main2Right2HPFlagship.Remove(BattleSave.Save1.Main2Right2HPFlagship[0]);
        BattleSave.Save1.Main3Left1HPFlagship.Remove(BattleSave.Save1.Main3Left1HPFlagship[0]);
        BattleSave.Save1.Main3Right1HPFlagship.Remove(BattleSave.Save1.Main3Right1HPFlagship[0]);

        if (BattleSave.Save1.DestinationArea.Count > 0)
            BattleSave.Save1.DestinationArea.Remove(BattleSave.Save1.DestinationArea[0]);
        if (BattleSave.Save1.WarpStartTime.Count > 0)
            BattleSave.Save1.WarpStartTime.Remove(BattleSave.Save1.WarpStartTime[0]);
        if (BattleSave.Save1.WarpDriveReady.Count > 0)
            BattleSave.Save1.WarpDriveReady.Remove(BattleSave.Save1.WarpDriveReady[0]);
        if (BattleSave.Save1.WarpDriveActive.Count > 0)
            BattleSave.Save1.WarpDriveActive.Remove(BattleSave.Save1.WarpDriveActive[0]);
    }

    //편대함 전투 데이터 주입(보병전 종료 직후, 보병전 진입 직전의 세이브 데이터 받기)
    void FormationShipBattleDataInput(GameObject Ship, int FormationNumber)
    {
        Ship.transform.position = new Vector2(BattleSave.Save1.FormationShipLocation[FormationNumber].x, BattleSave.Save1.FormationShipLocation[FormationNumber].y);
        Ship.transform.rotation = BattleSave.Save1.FormationShipRotanion[FormationNumber];

        Ship.GetComponent<TearSloriusFormationShip1>().ShieldDown = BattleSave.Save1.FormationShieldDown[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().hitPoints = BattleSave.Save1.FormationShipHull[FormationNumber];

        Ship.GetComponent<HullSloriusFormationShip1>().Main1Left1Down = BattleSave.Save1.Main1Left1DownFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main1Right1Down = BattleSave.Save1.Main1Right1DownFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main2Left1Down = BattleSave.Save1.Main2Left1DownFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main2Right1Down = BattleSave.Save1.Main2Right1DownFormationShip[FormationNumber];

        Ship.GetComponent<TearSloriusFormationShip1>().Main1Left1HP = BattleSave.Save1.Main1Left1HPFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main1Right1HP = BattleSave.Save1.Main1Right1HPFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main2Left1HP = BattleSave.Save1.Main2Left1HPFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main2Right1HP = BattleSave.Save1.Main2Right1HPFormationShip[FormationNumber];

        if (Ship.GetComponent<ShipRTS>().ShipNumber > 2)
        {
            Ship.GetComponent<HullSloriusFormationShip1>().Main3Left1Down = BattleSave.Save1.Main3Left1DownFormationShip[0];
            Ship.GetComponent<HullSloriusFormationShip1>().Main3Right1Down = BattleSave.Save1.Main3Right1DownFormationShip[0];
            Ship.GetComponent<TearSloriusFormationShip1>().Main3Left1HP = BattleSave.Save1.Main3Left1HPFormationShip[0];
            Ship.GetComponent<TearSloriusFormationShip1>().Main3Right1HP = BattleSave.Save1.Main3Right1HPFormationShip[0];

            BattleSave.Save1.Main3Left1DownFormationShip.Remove(BattleSave.Save1.Main3Left1DownFormationShip[0]);
            BattleSave.Save1.Main3Right1DownFormationShip.Remove(BattleSave.Save1.Main3Right1DownFormationShip[0]);
            BattleSave.Save1.Main3Left1HPFormationShip.Remove(BattleSave.Save1.Main3Left1HPFormationShip[0]);
            BattleSave.Save1.Main3Right1HPFormationShip.Remove(BattleSave.Save1.Main3Right1HPFormationShip[0]);
        }

        Ship.GetComponent<TearSloriusFormationShip1>().GetBattleData();

        if (Ship.GetComponent<ShipRTS>().ShipNumber == 2) //편대함(함포 2개)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Ship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Ship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Ship.GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
                else if (i == 1)
                {
                    Ship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType = BattleSave.Save1.CannonType[0];
                    Ship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().AttackTime = BattleSave.Save1.AttackTime[0];
                    Ship.GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }

                BattleSave.Save1.CannonType.Remove(BattleSave.Save1.CannonType[0]); //데이터를 옮길 때마다 삭제하여 데이터를 이어서 옮길 수 있도록 조취
                BattleSave.Save1.AttackTime.Remove(BattleSave.Save1.AttackTime[0]);
            }
        }

        //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프 시작 시간을 그대로 실행
        Ship.GetComponent<MoveVelocity>().WarpDriveReady = BattleSave.Save1.WarpDriveReady[FormationNumber];
        if (Ship.GetComponent<MoveVelocity>().WarpDriveReady == true)
        {
            StartCoroutine(StartWarp(Ship));
            Ship.GetComponent<MoveVelocity>().TransferWarpActive = true;
        }

        //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프를 그대로 유지
        Ship.GetComponent<MoveVelocity>().WarpDriveActive = BattleSave.Save1.WarpDriveActive[FormationNumber];
        if (Ship.GetComponent<MoveVelocity>().WarpDriveActive == true)
        {
            StartCoroutine(StartWarp(Ship));
            Ship.GetComponent<MoveVelocity>().TransferWarpActive = true;
        }

        //함포 무력화 상태
        if (BattleSave.Save1.FormationTurretDownList.Count > 0)
        {
            for (int e = 0; e < BattleSave.Save1.FormationTurretDownList.Count; e++)
            {
                Ship.GetComponent<HullSloriusFormationShip1>().TurretDownList.Add(BattleSave.Save1.FormationTurretDownList[0]);
                BattleSave.Save1.FormationTurretDownList.Remove(BattleSave.Save1.FormationTurretDownList[0]);
            }
            Ship.GetComponent<HullSloriusFormationShip1>().BringTurretDestroy();
        }
    }

    //편대함 전투 데이터 삭제
    public void FormationShipDataRemove()
    {
        BattleSave.Save1.FormationType.Remove(BattleSave.Save1.FormationType[0]);
        BattleSave.Save1.FormationShipLocation.Remove(BattleSave.Save1.FormationShipLocation[0]);
        BattleSave.Save1.FormationShipRotanion.Remove(BattleSave.Save1.FormationShipRotanion[0]);

        BattleSave.Save1.FormationShieldDown.Remove(BattleSave.Save1.FormationShieldDown[0]);
        BattleSave.Save1.FormationShipHull.Remove(BattleSave.Save1.FormationShipHull[0]);

        BattleSave.Save1.Main1Left1DownFormationShip.Remove(BattleSave.Save1.Main1Left1DownFormationShip[0]);
        BattleSave.Save1.Main1Right1DownFormationShip.Remove(BattleSave.Save1.Main1Right1DownFormationShip[0]);
        BattleSave.Save1.Main2Left1DownFormationShip.Remove(BattleSave.Save1.Main2Left1DownFormationShip[0]);
        BattleSave.Save1.Main2Right1DownFormationShip.Remove(BattleSave.Save1.Main2Right1DownFormationShip[0]);

        BattleSave.Save1.Main1Left1HPFormationShip.Remove(BattleSave.Save1.Main1Left1HPFormationShip[0]);
        BattleSave.Save1.Main1Right1HPFormationShip.Remove(BattleSave.Save1.Main1Right1HPFormationShip[0]);
        BattleSave.Save1.Main2Left1HPFormationShip.Remove(BattleSave.Save1.Main2Left1HPFormationShip[0]);
        BattleSave.Save1.Main2Right1HPFormationShip.Remove(BattleSave.Save1.Main2Right1HPFormationShip[0]);

        if (BattleSave.Save1.WarpDriveReady.Count > 0)
            BattleSave.Save1.WarpDriveReady.Remove(BattleSave.Save1.WarpDriveReady[0]);
        if (BattleSave.Save1.WarpDriveActive.Count > 0)
            BattleSave.Save1.WarpDriveActive.Remove(BattleSave.Save1.WarpDriveActive[0]);
    }

    //FormationShipBattleDataInput 메서드에서 실시.
    IEnumerator StartWarp(GameObject Ship)
    {
        yield return new WaitForSeconds(0.05f);
        Ship.GetComponent<MoveVelocity>().WarpSpeedUp(true);
    }

    //적 편대함 스폰
    void GetEnemyFormaion(GameObject EnemiesFlagshipAdd, int number)
    {
        for (int i = 0; i < BattleSave.Save1.CountOfEnemyFormationShip[number]; i++) //기함의 배열 번호에 맞게 배치되었던 함선을 그대로 스폰
        {
            EnemyFormationShipDeploy(EnemiesFlagshipAdd, i);

            if (i == BattleSave.Save1.CountOfEnemyFormationShip[number] - 1) //편대함 스폰 및 데이터 이전이 모두 완료
            {
                if (EnemiesFlagshipAdd.GetComponent<EnemyShipBehavior>().WarpDriveActive == true) //워프 배열이 모두 배치가 완료되는 시점이므로 이 때 배열전체 위치를 옮긴다.
                {
                    EnemiesFlagshipAdd.GetComponent<EnemyFollowShipManager>().WarpFormation.transform.position = EnemiesFlagshipAdd.GetComponent<EnemyShipBehavior>().DestinationArea;
                    EnemiesFlagshipAdd.GetComponent<EnemyFollowShipManager>().WarpFormation.transform.rotation = EnemiesFlagshipAdd.transform.rotation;
                }

                //편대함 데이터를 모두 이전을 완료했으므로, 정해진 분량의 편대함 배열 번호까지만 데이터 리스트 삭제(다음 함대의 리스트를 정상으로 사용하기 위함)
                for (int j = 0; j < BattleSave.Save1.CountOfEnemyFormationShip[number]; j++)
                {
                    EnemyFormationShipDataRemove();
                }
            }
        }
    }

    //적 기함 전투 데이터 삭제
    public void EnemyFlagshipDataRemove()
    {
        BattleSave.Save1.EnemyIsBattleSite.Remove(BattleSave.Save1.EnemyIsBattleSite[0]);
        BattleSave.Save1.EnemyBattleSiteNumber.Remove(BattleSave.Save1.EnemyBattleSiteNumber[0]);

        BattleSave.Save1.EnemyNationType.Remove(BattleSave.Save1.EnemyNationType[0]);
        BattleSave.Save1.EnemyFlagshipShieldDown.Remove(BattleSave.Save1.EnemyFlagshipShieldDown[0]);
        BattleSave.Save1.EnemyFlagshipHull.Remove(BattleSave.Save1.EnemyFlagshipHull[0]);
        BattleSave.Save1.EnemyFlagshipShield.Remove(BattleSave.Save1.EnemyFlagshipShield[0]);

        BattleSave.Save1.Main1Left1DownEnemyFlagship.Remove(BattleSave.Save1.Main1Left1DownEnemyFlagship[0]);
        BattleSave.Save1.Main1Left2DownEnemyFlagship.Remove(BattleSave.Save1.Main1Left2DownEnemyFlagship[0]);
        BattleSave.Save1.Main1Right1DownEnemyFlagship.Remove(BattleSave.Save1.Main1Right1DownEnemyFlagship[0]);
        BattleSave.Save1.Main1Right2DownEnemyFlagship.Remove(BattleSave.Save1.Main1Right2DownEnemyFlagship[0]);
        BattleSave.Save1.Main2Left1DownEnemyFlagship.Remove(BattleSave.Save1.Main2Left1DownEnemyFlagship[0]);
        BattleSave.Save1.Main2Left2DownEnemyFlagship.Remove(BattleSave.Save1.Main2Left2DownEnemyFlagship[0]);
        BattleSave.Save1.Main2Right1DownEnemyFlagship.Remove(BattleSave.Save1.Main2Right1DownEnemyFlagship[0]);
        BattleSave.Save1.Main2Right2DownEnemyFlagship.Remove(BattleSave.Save1.Main2Right2DownEnemyFlagship[0]);
        BattleSave.Save1.Main3Left1DownEnemyFlagship.Remove(BattleSave.Save1.Main3Left1DownEnemyFlagship[0]);
        BattleSave.Save1.Main3Right1DownEnemyFlagship.Remove(BattleSave.Save1.Main3Right1DownEnemyFlagship[0]);

        BattleSave.Save1.Main1Left1HPEnemyFlagship.Remove(BattleSave.Save1.Main1Left1HPEnemyFlagship[0]);
        BattleSave.Save1.Main1Left2HPEnemyFlagship.Remove(BattleSave.Save1.Main1Left2HPEnemyFlagship[0]);
        BattleSave.Save1.Main1Right1HPEnemyFlagship.Remove(BattleSave.Save1.Main1Right1HPEnemyFlagship[0]);
        BattleSave.Save1.Main1Right2HPEnemyFlagship.Remove(BattleSave.Save1.Main1Right2HPEnemyFlagship[0]);
        BattleSave.Save1.Main2Left1HPEnemyFlagship.Remove(BattleSave.Save1.Main2Left1HPEnemyFlagship[0]);
        BattleSave.Save1.Main2Left2HPEnemyFlagship.Remove(BattleSave.Save1.Main2Left2HPEnemyFlagship[0]);
        BattleSave.Save1.Main2Right1HPEnemyFlagship.Remove(BattleSave.Save1.Main2Right1HPEnemyFlagship[0]);
        BattleSave.Save1.Main2Right2HPEnemyFlagship.Remove(BattleSave.Save1.Main2Right2HPEnemyFlagship[0]);
        BattleSave.Save1.Main3Left1HPEnemyFlagship.Remove(BattleSave.Save1.Main3Left1HPEnemyFlagship[0]);
        BattleSave.Save1.Main3Right1HPEnemyFlagship.Remove(BattleSave.Save1.Main3Right1HPEnemyFlagship[0]);

        if (BattleSave.Save1.EnemyDestinationArea.Count > 0)
            BattleSave.Save1.EnemyDestinationArea.Remove(BattleSave.Save1.EnemyDestinationArea[0]);
        if (BattleSave.Save1.EnemyWarpStartTime.Count > 0)
            BattleSave.Save1.EnemyWarpStartTime.Remove(BattleSave.Save1.EnemyWarpStartTime[0]);
        if (BattleSave.Save1.EnemyWarpDriveActive.Count > 0)
            BattleSave.Save1.EnemyWarpDriveActive.Remove(BattleSave.Save1.EnemyWarpDriveActive[0]);

        BattleSave.Save1.SelectedEnemyFlagship.Remove(BattleSave.Save1.SelectedEnemyFlagship[0]);
        if (BattleSave.Save1.EnemyFlagshipDown.Count > 0)
            BattleSave.Save1.EnemyFlagshipDown.Remove(BattleSave.Save1.EnemyFlagshipDown[0]);
    }

    //적 편대함 전투 데이터 주입(보병전 종료 직후, 보병전 진입 직전의 세이브 데이터 받기)
    void EnemyFormationShipBattleDataInput(GameObject Ship, int FormationNumber)
    {
        Ship.GetComponent<EnemyShipLevelInformation>().isBattleSite = BattleSave.Save1.EnemyIsBattleSite[0];
        Ship.GetComponent<EnemyShipLevelInformation>().BattleSiteNumber = BattleSave.Save1.EnemyBattleSiteNumber[0];
        UniverseMapSystem.BattleSiteGet(Ship, BattleSave.Save1.EnemyBattleSiteNumber[0]);

        Ship.transform.position = new Vector2(BattleSave.Save1.EnemyFormationShipLocation[FormationNumber].x, BattleSave.Save1.EnemyFormationShipLocation[FormationNumber].y);
        Ship.transform.rotation = BattleSave.Save1.EnemyFormationShipRotanion[FormationNumber];

        Ship.GetComponent<TearSloriusFormationShip1>().ShieldDown = BattleSave.Save1.EnemyFormationShieldDown[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().hitPoints = BattleSave.Save1.EnemyFormationShipHull[FormationNumber];
        if (Ship.GetComponent<EnemyShipBehavior>().NationType == 2)
            Ship.GetComponent<ShieldSloriusShip>().ShieldPoints = BattleSave.Save1.EnemyFormationShield[0];

        Ship.GetComponent<HullSloriusFormationShip1>().Main1Left1Down = BattleSave.Save1.Main1Left1DownEnemyFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main1Right1Down = BattleSave.Save1.Main1Right1DownEnemyFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main2Left1Down = BattleSave.Save1.Main2Left1DownEnemyFormationShip[FormationNumber];
        Ship.GetComponent<HullSloriusFormationShip1>().Main2Right1Down = BattleSave.Save1.Main2Right1DownEnemyFormationShip[FormationNumber];

        Ship.GetComponent<TearSloriusFormationShip1>().Main1Left1HP = BattleSave.Save1.Main1Left1HPEnemyFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main1Right1HP = BattleSave.Save1.Main1Right1HPEnemyFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main2Left1HP = BattleSave.Save1.Main2Left1HPEnemyFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().Main2Right1HP = BattleSave.Save1.Main2Right1HPEnemyFormationShip[FormationNumber];
        Ship.GetComponent<TearSloriusFormationShip1>().GetBattleData();

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                Ship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                Ship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                Ship.GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonReinput();
            }
            else if (i == 1)
            {
                Ship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonType = BattleSave.Save1.EnemyCannonType[0];
                Ship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().AttackTime = BattleSave.Save1.EnemyAttackTime[0];
                Ship.GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonReinput();
            }

            BattleSave.Save1.EnemyCannonType.Remove(BattleSave.Save1.EnemyCannonType[0]); //데이터를 옮길 때마다 삭제하여 데이터를 이어서 옮길 수 있도록 조취
            BattleSave.Save1.EnemyAttackTime.Remove(BattleSave.Save1.EnemyAttackTime[0]);
        }

        //워프항법 도중에 보병전을 실시했을 경우에 이어서 워프를 그대로 유지
        Ship.GetComponent<EnemyShipBehavior>().WarpDriveActive = BattleSave.Save1.EnemyWarpDriveActive[FormationNumber];
        if (Ship.GetComponent<EnemyShipBehavior>().WarpDriveActive == true)
        {
            StartCoroutine(EnemyStartWarp(Ship)); //도착지점을 표시하는 워프 편대 위치가 자리를 다 잡을 때까지 기함보다 0.05초 늦게 워프 시작
        }

        //함포 무력화 상태
        if (BattleSave.Save1.EnemyFormationTurretDownList.Count > 0)
        {
            for (int e = 0; e < BattleSave.Save1.EnemyFormationTurretDownList.Count; e++)
            {
                Ship.GetComponent<HullSloriusFormationShip1>().TurretDownList.Add(BattleSave.Save1.EnemyFormationTurretDownList[0]);
                BattleSave.Save1.EnemyFormationTurretDownList.Remove(BattleSave.Save1.EnemyFormationTurretDownList[0]);
            }
            Ship.GetComponent<HullSloriusFormationShip1>().BringTurretDestroy();
        }
    }

    //적 편대함 전투 데이터 삭제
    public void EnemyFormationShipDataRemove()
    {
        BattleSave.Save1.EnemyIsBattleSite.Remove(BattleSave.Save1.EnemyIsBattleSite[0]);
        BattleSave.Save1.EnemyBattleSiteNumber.Remove(BattleSave.Save1.EnemyBattleSiteNumber[0]);

        BattleSave.Save1.EnemyNationType.Remove(BattleSave.Save1.EnemyNationType[0]);
        BattleSave.Save1.EnemyFormationType.Remove(BattleSave.Save1.EnemyFormationType[0]);
        BattleSave.Save1.EnemyFormationShipLocation.Remove(BattleSave.Save1.EnemyFormationShipLocation[0]);
        BattleSave.Save1.EnemyFormationShipRotanion.Remove(BattleSave.Save1.EnemyFormationShipRotanion[0]);

        BattleSave.Save1.EnemyFormationShieldDown.Remove(BattleSave.Save1.EnemyFormationShieldDown[0]);
        BattleSave.Save1.EnemyFormationShipHull.Remove(BattleSave.Save1.EnemyFormationShipHull[0]);
        BattleSave.Save1.EnemyFormationShield.Remove(BattleSave.Save1.EnemyFormationShield[0]);

        BattleSave.Save1.Main1Left1DownEnemyFormationShip.Remove(BattleSave.Save1.Main1Left1DownEnemyFormationShip[0]);
        BattleSave.Save1.Main1Right1DownEnemyFormationShip.Remove(BattleSave.Save1.Main1Right1DownEnemyFormationShip[0]);
        BattleSave.Save1.Main2Left1DownEnemyFormationShip.Remove(BattleSave.Save1.Main2Left1DownEnemyFormationShip[0]);
        BattleSave.Save1.Main2Right1DownEnemyFormationShip.Remove(BattleSave.Save1.Main2Right1DownEnemyFormationShip[0]);

        BattleSave.Save1.Main1Left1HPEnemyFormationShip.Remove(BattleSave.Save1.Main1Left1HPEnemyFormationShip[0]);
        BattleSave.Save1.Main1Right1HPEnemyFormationShip.Remove(BattleSave.Save1.Main1Right1HPEnemyFormationShip[0]);
        BattleSave.Save1.Main2Left1HPEnemyFormationShip.Remove(BattleSave.Save1.Main2Left1HPEnemyFormationShip[0]);
        BattleSave.Save1.Main2Right1HPEnemyFormationShip.Remove(BattleSave.Save1.Main2Right1HPEnemyFormationShip[0]);

        if (BattleSave.Save1.EnemyWarpDriveActive.Count > 0)
            BattleSave.Save1.EnemyWarpDriveActive.Remove(BattleSave.Save1.EnemyWarpDriveActive[0]);
    }

    //EnemyFormationShipBattleDataInput 메서드에서 실시.
    IEnumerator EnemyStartWarp(GameObject Ship)
    {
        yield return new WaitForSeconds(0.05f);
        Ship.GetComponent<EnemyShipBehavior>().WarpSpeedUp(true);
    }

    //스폰용 함선 배치
    public void SpawnShipDeploy(GameObject Flagship, GameObject FormationShip, int FlagshipNumber, int FormationNumber)
    {
        FormationShip.transform.position = Flagship.transform.position;
        FormationShip.GetComponent<ShipRTS>().ShipListNumber = Flagship.GetComponent<FollowShipManager>().ShipList.Count;
        Flagship.GetComponent<FollowShipManager>().ShipList.Add(FormationShip);
        FormationDeployStart(Flagship, FormationShip, FlagshipNumber, FormationNumber);
    }

    //배열에 알맞게 배치
    public void FormationNumberSet(GameObject Flagship, GameObject FormationShip, int FlagshipNumber, int FormationNumber)
    {
        //편대함이 하나도 없을 경우
        if (Flagship.GetComponent<FollowShipManager>().ShipList.Count == 0)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList.Add(FormationShip);
            FormationNumber = 0;
            FormationShip.GetComponent<ShipRTS>().ShipListNumber = FormationNumber;
            Debug.Log("0 : " + FormationShip.GetComponent<ShipRTS>().ShipListNumber);
        }
        else if (Flagship.GetComponent<FollowShipManager>().ShipList.Count > 0)
        {
            for (int i = 0; i < Flagship.GetComponent<FollowShipManager>().ShipList.Count; i++)
            {
                if (i > 0)
                {
                    //현재 편대함 번호와 이전 편대함 번호가 1씩 정상적으로 차이가 날 경우, 맨 마지막에 추가되는 것으로 처리
                    if (Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>().ShipListNumber - Flagship.GetComponent<FollowShipManager>().ShipList[i - 1].GetComponent<ShipRTS>().ShipListNumber == 1)
                    {
                        if (i == Flagship.GetComponent<FollowShipManager>().ShipList.Count - 1)
                        {
                            FormationShip.GetComponent<ShipRTS>().ShipListNumber = Flagship.GetComponent<FollowShipManager>().ShipList.Count;
                            FormationNumber = Flagship.GetComponent<FollowShipManager>().ShipList.Count;
                            Flagship.GetComponent<FollowShipManager>().ShipList.Add(FormationShip);
                            Flagship.GetComponent<FollowShipManager>().EmptyLocationList.Add(FormationShip.GetComponent<ShipRTS>().ShipListNumber + 1);
                            Debug.Log("1 : " + FormationShip.GetComponent<ShipRTS>().ShipListNumber);
                            break;
                        }
                    }
                    //현재 편대함 번호와 이전 편대함 번호가 2 이상 차이가 날 경우, 중간에 하나 비어있는 것으로 간주하여 이전 편대함 번호에서 +1 추가된 번호로 배치
                    else if (Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>().ShipListNumber - Flagship.GetComponent<FollowShipManager>().ShipList[i - 1].GetComponent<ShipRTS>().ShipListNumber != 1)
                    {
                        FormationShip.GetComponent<ShipRTS>().ShipListNumber = Flagship.GetComponent<FollowShipManager>().ShipList[i - 1].GetComponent<ShipRTS>().ShipListNumber + 1;
                        FormationNumber = FormationShip.GetComponent<ShipRTS>().ShipListNumber;
                        Flagship.GetComponent<FollowShipManager>().ShipList.Insert(FormationNumber, FormationShip);
                        Flagship.GetComponent<FollowShipManager>().EmptyLocationList.Add(FormationShip.GetComponent<ShipRTS>().ShipListNumber + 1);
                        Debug.Log("2 : " + FormationShip.GetComponent<ShipRTS>().ShipListNumber);
                        break;
                    }
                }
                else if (i == 0)
                {
                    //0번 편대함이 존재하지 않을 경우, 0번 편대함을 추가
                    if (Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>().ShipListNumber != 0)
                    {
                        FormationShip.GetComponent<ShipRTS>().ShipListNumber = 0;
                        FormationNumber = 0;
                        Flagship.GetComponent<FollowShipManager>().ShipList.Insert(0, FormationShip);
                        Flagship.GetComponent<FollowShipManager>().EmptyLocationList.Add(1);
                        Debug.Log("3 : " + FormationShip.GetComponent<ShipRTS>().ShipListNumber);
                        break;
                    }
                    else
                    {
                        //0번 편대함만 혼자 배치된 경우, 1번 편대함을 추가하며, 2대 이상의 편대함이 배치되어 있을 경우에는 이 절차를 무시한다.
                        if (Flagship.GetComponent<FollowShipManager>().ShipList.Count == 1)
                        {
                            FormationShip.GetComponent<ShipRTS>().ShipListNumber = 1;
                            FormationNumber = 1;
                            Flagship.GetComponent<FollowShipManager>().ShipList.Add(FormationShip);
                            Flagship.GetComponent<FollowShipManager>().EmptyLocationList.Add(2);
                            Debug.Log("4 : " + FormationShip.GetComponent<ShipRTS>().ShipListNumber);
                            break;
                        }
                    }
                }
            }
        }
        FormationDeployStart(Flagship, FormationShip, FlagshipNumber, FormationNumber);
    }

    //최종 배치 시작
    public void FormationDeployStart(GameObject Flagship, GameObject FormationShip, int FlagshipNumber, int FormationNumber)
    {
        FormationShip.GetComponent<MoveVelocity>().MyFlagship = Flagship;
        Flagship.GetComponent<FollowShipManager>().ShipAccount++;
        Flagship.GetComponent<WarpFormationControl>().GetFormation(Flagship, FormationNumber);
        ShipManager.instance.AmountOfFormationShip[FlagshipNumber]++;
    }
}