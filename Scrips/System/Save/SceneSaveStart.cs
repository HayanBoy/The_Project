using UnityEngine;

public class SceneSaveStart : MonoBehaviour
{
    public SaveInput SaveInput;
    public WordPrintSystem WordPrintSystem;
    public GameObject SpaceBattleScene;
    public bool isJson;

    private void OnEnable()
    {
        BattleSave.Save1.SelectedshipNumber = ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber;
        BattleSave.Save1.CameraLocation = GameObject.Find("Moving Camera").transform.position;

        BattleSave.Save1.NarihaFlagShipList = AIShipManager.instance.NarihaFlagShipList.Count;
        BattleSave.Save1.EnemiesFlagShipList = AIShipManager.instance.EnemiesFlagShipList.Count;
        BattleSave.Save1.EnemiesFormationShipList = AIShipManager.instance.EnemiesFormationShipList.Count;

        //활성화 중인 전투 사이트 정보 저장
        for (int i = 0; i < ShipManager.instance.ActiveBattleSiteList.Count; i++)
        {
            BattleSave.Save1.RandomSiteNumber.Add(ShipManager.instance.ActiveBattleSiteList[i].GetComponent<RandomSiteBattle>().BattleSiteNumber);
            BattleSave.Save1.RandomSiteTransform.Add(ShipManager.instance.ActiveBattleSiteList[i].transform.position);
        }

        for (int i = 0; i < ShipManager.instance.FreePlanetList.Count; i++)
        {
            BattleSave.Save1.PlanetNumber.Add(ShipManager.instance.FreePlanetList[i].GetComponent<PlanetOurForceShipsManager>().PlanetNumber);
            BattleSave.Save1.FirstFreePlanet.Add(ShipManager.instance.FreePlanetList[i].GetComponent<PlanetOurForceShipsManager>().FirstFree);
            BattleSave.Save1.BattleVictoryPlanet.Add(ShipManager.instance.FreePlanetList[i].GetComponent<PlanetOurForceShipsManager>().BattleVictory);
        }

        BattleSave.Save1.CountOfFlagship = ShipManager.instance.FlagShipList.Count;

        //기함 및 기함별 편대 함선의 위치와 회전 정보 저장
        if (ShipManager.instance.FlagShipList.Count > 0)
        {
            for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
            {
                //기함 스킬 상태 저장
                BattleSave.Save1.FirstSkillType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().SkillType);
                BattleSave.Save1.FirstSkillNumber.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().SkillNumber);
                BattleSave.Save1.SecondSkillType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().SecondSkillType);
                BattleSave.Save1.SecondSkillNumber.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().SecondSkillNumber);
                BattleSave.Save1.ThirdSkillType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().ThirdSkillType);
                BattleSave.Save1.ThirdSkillNumber.Add(ShipManager.instance.FlagShipList[i].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber);

                //위치 및 회전
                BattleSave.Save1.FlagshipLocation.Add(ShipManager.instance.FlagShipList[i].transform.position);
                BattleSave.Save1.FlagshipRotanion.Add(ShipManager.instance.FlagShipList[i].transform.rotation);
                BattleSave.Save1.CountOfFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count);
                BattleSave.Save1.FormationStorage.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().FormationStorage);

                for (int Codex = 0; Codex < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().FormationStorage; Codex++)
                {
                    if (Codex == 0)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location1.position);
                    else if (Codex == 1)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location2.position);
                    else if (Codex == 2)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location3.position);
                    else if (Codex == 3)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location4.position);
                    else if (Codex == 4)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location5.position);
                    else if (Codex == 5)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location6.position);
                    else if (Codex == 6)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location7.position);
                    else if (Codex == 7)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location8.position);
                    else if (Codex == 8)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location9.position);
                    else if (Codex == 9)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location10.position);
                    else if (Codex == 10)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location11.position);
                    else if (Codex == 11)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location12.position);
                    else if (Codex == 12)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location13.position);
                    else if (Codex == 13)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location14.position);
                    else if (Codex == 14)
                        BattleSave.Save1.FlagshipFormationsCodex.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().Location15.position);
                }

                //쉴드 및 선체
                BattleSave.Save1.FlagshipShieldDown.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().ShieldDown);
                BattleSave.Save1.FlagshipHull.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().hitPoints);

                //부위별 선체 격파 스위치
                BattleSave.Save1.Main1Left1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Left1Down);
                BattleSave.Save1.Main1Left2DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Left2Down);
                BattleSave.Save1.Main1Right1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right1Down);
                BattleSave.Save1.Main1Right2DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right2Down);
                BattleSave.Save1.Main1Right2DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right2Down);
                BattleSave.Save1.Main2Left1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Left1Down);
                BattleSave.Save1.Main2Left2DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Left2Down);
                BattleSave.Save1.Main2Right1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Right1Down);
                BattleSave.Save1.Main2Right2DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Right2Down);
                BattleSave.Save1.Main3Left1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main3Left1Down);
                BattleSave.Save1.Main3Right1DownFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().Main3Right1Down);

                //부위별 선체 내구도
                BattleSave.Save1.Main1Left1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Left1HP);
                BattleSave.Save1.Main1Left2HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Left2HP);
                BattleSave.Save1.Main1Right1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Right1HP);
                BattleSave.Save1.Main1Right2HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Right2HP);
                BattleSave.Save1.Main2Left1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Left1HP);
                BattleSave.Save1.Main2Left2HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Left2HP);
                BattleSave.Save1.Main2Right1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Right1HP);
                BattleSave.Save1.Main2Right2HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Right2HP);
                BattleSave.Save1.Main3Left1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main3Left1HP);
                BattleSave.Save1.Main3Right1HPFlagship.Add(ShipManager.instance.FlagShipList[i].GetComponent<TearSloriusFlagship1>().Main3Right1HP);

                //함포 유형 및 사격 상태
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType);
                BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().AttackTime);

                //함포 무력화 상태
                for (int e = 0; e < ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().TurretDownList.Count; e++)
                {
                    BattleSave.Save1.FlagshipTurretDownList.Add(ShipManager.instance.FlagShipList[i].GetComponent<HullSloriusFlagship1>().TurretDownList[e]);
                }

                //워프 항법 상태
                BattleSave.Save1.DestinationArea.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().DestinationArea);
                BattleSave.Save1.WarpStartTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<ShipRTS>().WarpStartTime);
                BattleSave.Save1.WarpDriveReady.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().WarpDriveReady);
                BattleSave.Save1.WarpDriveActive.Add(ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().WarpDriveActive);

                //편대함 데이터
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count > 0)
                {
                    for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
                    {
                        BattleSave.Save1.FormationType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber);
                        BattleSave.Save1.FormationShipLocation.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].transform.position);
                        BattleSave.Save1.FormationShipRotanion.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].transform.rotation);

                        BattleSave.Save1.FormationShieldDown.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().ShieldDown);
                        BattleSave.Save1.FormationShipHull.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().hitPoints);

                        BattleSave.Save1.Main1Left1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main1Left1Down);
                        BattleSave.Save1.Main1Right1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main1Right1Down);
                        BattleSave.Save1.Main2Left1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main2Left1Down);
                        BattleSave.Save1.Main2Right1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main2Right1Down);

                        BattleSave.Save1.Main1Left1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main1Left1HP);
                        BattleSave.Save1.Main1Right1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main1Right1HP);
                        BattleSave.Save1.Main2Left1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main2Left1HP);
                        BattleSave.Save1.Main2Right1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main2Right1HP);

                        if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber > 2)
                        {
                            BattleSave.Save1.Main3Left1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main3Left1Down);
                            BattleSave.Save1.Main3Right1DownFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main3Right1Down);
                            BattleSave.Save1.Main3Left1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main3Left1HP);
                            BattleSave.Save1.Main3Right1HPFormationShip.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main3Right1HP);
                        }

                        if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<ShipRTS>().ShipNumber == 2)
                        {
                            BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType);
                            BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                            BattleSave.Save1.CannonType.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType);
                            BattleSave.Save1.AttackTime.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().AttackTime);
                        }

                        //함포 무력화 상태
                        for (int e = 0; e < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().TurretDownList.Count; e++)
                        {
                            BattleSave.Save1.FormationTurretDownList.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().TurretDownList[e]);
                        }

                        BattleSave.Save1.WarpDriveReady.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().WarpDriveReady);
                        BattleSave.Save1.WarpDriveActive.Add(ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().WarpDriveActive);
                    }
                }
            }
        }

        //적 기함 및 기함별 편대 함선의 위치와 회전 정보 저장
        for (int i = 0; i < AIShipManager.instance.EnemiesFlagShipList.Count; i++)
        {
            //상태
            BattleSave.Save1.EnemyIsBattleSite.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipLevelInformation>().isBattleSite);
            BattleSave.Save1.EnemyBattleSiteNumber.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipLevelInformation>().BattleSiteNumber);
            if (AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TransferColliderFlagship>().isDowned == true)
                BattleSave.Save1.EnemyFlagshipDown.Add(true);
            else
                BattleSave.Save1.EnemyFlagshipDown.Add(false);

            //위치 및 회전
            BattleSave.Save1.EnemyNationType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().NationType);
            BattleSave.Save1.EnemyFlagshipLocation.Add(AIShipManager.instance.EnemiesFlagShipList[i].transform.position);
            BattleSave.Save1.EnemyFlagshipRotanion.Add(AIShipManager.instance.EnemiesFlagShipList[i].transform.rotation);
            BattleSave.Save1.CountOfEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList.Count);

            //쉴드 및 선체
            BattleSave.Save1.EnemyFlagshipShieldDown.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().ShieldDown);
            BattleSave.Save1.EnemyFlagshipHull.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().hitPoints);
            if (AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().NationType == 2)
                BattleSave.Save1.EnemyFlagshipShield.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<ShieldSloriusShip>().ShieldPoints);
            else
                BattleSave.Save1.EnemyFlagshipShield.Add(0);

            //부위별 선체 격파 스위치
            BattleSave.Save1.Main1Left1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Left1Down);
            BattleSave.Save1.Main1Left2DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Left2Down);
            BattleSave.Save1.Main1Right1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right1Down);
            BattleSave.Save1.Main1Right2DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right2Down);
            BattleSave.Save1.Main1Right2DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main1Right2Down);
            BattleSave.Save1.Main2Left1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Left1Down);
            BattleSave.Save1.Main2Left2DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Left2Down);
            BattleSave.Save1.Main2Right1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Right1Down);
            BattleSave.Save1.Main2Right2DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main2Right2Down);
            BattleSave.Save1.Main3Left1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main3Left1Down);
            BattleSave.Save1.Main3Right1DownEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().Main3Right1Down);

            //부위별 선체 내구도
            BattleSave.Save1.Main1Left1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Left1HP);
            BattleSave.Save1.Main1Left2HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Left2HP);
            BattleSave.Save1.Main1Right1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Right1HP);
            BattleSave.Save1.Main1Right2HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main1Right2HP);
            BattleSave.Save1.Main2Left1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Left1HP);
            BattleSave.Save1.Main2Left2HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Left2HP);
            BattleSave.Save1.Main2Right1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Right1HP);
            BattleSave.Save1.Main2Right2HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main2Right2HP);
            BattleSave.Save1.Main3Left1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main3Left1HP);
            BattleSave.Save1.Main3Right1HPEnemyFlagship.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<TearSloriusFlagship1>().Main3Right1HP);

            //함포 유형 및 사격 상태
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().AttackTime);

            //함포 무력화 상태
            for (int e = 0; e < AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().TurretDownList.Count; e++)
            {
                BattleSave.Save1.EnemyFlagshipTurretDownList.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<HullSloriusFlagship1>().TurretDownList[e]);
            }

            //워프 항법 상태
            BattleSave.Save1.EnemyDestinationArea.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().DestinationArea);
            BattleSave.Save1.EnemyWarpDriveActive.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().WarpDriveActive);

            //적 선택 상태
            if (AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipLevelInformation>().Selected == true)
            {
                BattleSave.Save1.SelectedEnemyFlagship.Add(true);
            }
            else
            {
                BattleSave.Save1.SelectedEnemyFlagship.Add(false);
            }

            for (int j = 0; j < AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList.Count; j++)
            {
                //상태
                BattleSave.Save1.EnemyIsBattleSite.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipLevelInformation>().isBattleSite);
                BattleSave.Save1.EnemyBattleSiteNumber.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipLevelInformation>().BattleSiteNumber);

                BattleSave.Save1.EnemyNationType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyShipBehavior>().NationType);
                BattleSave.Save1.EnemyFormationType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipLevelInformation>().ShipNumber);
                BattleSave.Save1.EnemyFormationShipLocation.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].transform.position);
                BattleSave.Save1.EnemyFormationShipRotanion.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].transform.rotation);

                BattleSave.Save1.EnemyFormationShieldDown.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().ShieldDown);
                BattleSave.Save1.EnemyFormationShipHull.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().hitPoints);
                if (AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().NationType == 2)
                    BattleSave.Save1.EnemyFormationShield.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<ShieldSloriusShip>().ShieldPoints);
                else
                    BattleSave.Save1.EnemyFormationShield.Add(0);

                BattleSave.Save1.Main1Left1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main1Left1Down);
                BattleSave.Save1.Main1Right1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main1Right1Down);
                BattleSave.Save1.Main2Left1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main2Left1Down);
                BattleSave.Save1.Main2Right1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().Main2Right1Down);

                BattleSave.Save1.Main1Left1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main1Left1HP);
                BattleSave.Save1.Main1Right1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main1Right1HP);
                BattleSave.Save1.Main2Left1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main2Left1HP);
                BattleSave.Save1.Main2Right1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<TearSloriusFormationShip1>().Main2Right1HP);

                BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonType);
                BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().AttackTime);
                BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonType);
                BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().AttackTime);

                //함포 무력화 상태
                for (int e = 0; e < AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().TurretDownList.Count; e++)
                {
                    BattleSave.Save1.EnemyFormationTurretDownList.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<HullSloriusFormationShip1>().TurretDownList[e]);
                }

                BattleSave.Save1.EnemyWarpDriveActive.Add(AIShipManager.instance.EnemiesFlagShipList[i].GetComponent<EnemyFollowShipManager>().ShipList[j].GetComponent<EnemyShipBehavior>().WarpDriveActive);
            }
        }

        //적 단독 편대함 위치와 회전 정보 저장
        for (int i = 0; i < AIShipManager.instance.EnemiesFormationShipList.Count; i++)
        {
            //상태
            BattleSave.Save1.EnemyIsBattleSite.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipLevelInformation>().isBattleSite);
            BattleSave.Save1.EnemyBattleSiteNumber.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipLevelInformation>().BattleSiteNumber);

            BattleSave.Save1.EnemyNationType.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().NationType);
            BattleSave.Save1.EnemyFormationType.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipLevelInformation>().ShipNumber);
            BattleSave.Save1.EnemyFormationShipLocation.Add(AIShipManager.instance.EnemiesFormationShipList[i].transform.position);
            BattleSave.Save1.EnemyFormationShipRotanion.Add(AIShipManager.instance.EnemiesFormationShipList[i].transform.rotation);

            BattleSave.Save1.EnemyFormationShieldDown.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<TearSloriusFormationShip1>().ShieldDown);
            BattleSave.Save1.EnemyFormationShipHull.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<HullSloriusFormationShip1>().hitPoints);
            if (AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().NationType == 2)
                BattleSave.Save1.EnemyFormationShield.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<ShieldSloriusShip>().ShieldPoints);
            else
                BattleSave.Save1.EnemyFormationShield.Add(0);

            BattleSave.Save1.Main1Left1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<HullSloriusFormationShip1>().Main1Left1Down);
            BattleSave.Save1.Main1Right1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<HullSloriusFormationShip1>().Main1Right1Down);
            BattleSave.Save1.Main2Left1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<HullSloriusFormationShip1>().Main2Left1Down);
            BattleSave.Save1.Main2Right1DownEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<HullSloriusFormationShip1>().Main2Right1Down);

            BattleSave.Save1.Main1Left1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<TearSloriusFormationShip1>().Main1Left1HP);
            BattleSave.Save1.Main1Right1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<TearSloriusFormationShip1>().Main1Right1HP);
            BattleSave.Save1.Main2Left1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<TearSloriusFormationShip1>().Main2Left1HP);
            BattleSave.Save1.Main2Right1HPEnemyFormationShip.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<TearSloriusFormationShip1>().Main2Right1HP);

            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().AttackTime);
            BattleSave.Save1.EnemyCannonType.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().CannonType);
            BattleSave.Save1.EnemyAttackTime.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().AttackTime);

            BattleSave.Save1.EnemyDestinationArea.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().DestinationArea);
            BattleSave.Save1.EnemyWarpDriveActive.Add(AIShipManager.instance.EnemiesFormationShipList[i].GetComponent<EnemyShipBehavior>().WarpDriveActive);
        }

        if (isJson == false)
        {
            SpaceBattleScene.SetActive(true);
        }
    }
}