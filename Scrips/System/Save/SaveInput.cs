using System.Collections;
using Cinemachine;
using UnityEngine;

public class SaveInput : MonoBehaviour
{
    [Header("스크립트")]
    public HurricaneMissionManager HurricaneMissionManager;
    public MultiFlagshipSystem MultiFlagshipSystem;
    public TutorialSystem TutorialSystem;
    TutorialData TutorialData;

    public CinemachineVirtualCamera Camera;

    //나리하
    public GameObject NarihaFlagship1;
    public GameObject NarihaFormationShip1;

    //슬로리어스
    public GameObject SloriusFlagship1;
    public GameObject SloriusFormationShip1;

    void Start()
    {
        BattleSave.Save1.BattleLoadScene = 0;
        if (BattleSave.Save1.FirstStart == false)
        {
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                BattleSave.Save1.GroundBattleCount = 0;
                UpgradeDataSystem.instance.LoadUpgradePatch(); //각 업그레이드된 상태를 적용
                StartCoroutine(DeletedataStart());
            }
        }
        else
        {
            BattleSave.Save1.GroundBattleCount = 0;
            if (BattleSave.Save1.PlanetTutorial == 1) //튜토리얼 행성전 도중에 델타 허리케인 임무를 취소하였을 때
            {
                TutorialSystem.BackToGlessia();
            }
            else if (BattleSave.Save1.PlanetTutorial == 0) //튜로리얼을 완전히 처음 시작한 것으로 간주
            {
                if (TutorialData == null)
                    TutorialData = FindObjectOfType<TutorialData>();
                TutorialData.StartTutorial();
            }
            UpgradeDataSystem.instance.LoadUpgradePatch(); //각 업그레이드된 상태를 적용
            StartCoroutine(DeletedataStart());
        }
        Invoke("SelectedFlagship", 0.5f);
    }

    IEnumerator DeletedataStart()
    {
        yield return new WaitForSeconds(1);
        DeleteData();
    }

    void SelectedFlagship()
    {
        MultiFlagshipSystem.FlagshipNumber = 1;
    }

    public void DeleteData()
    {
        BattleSave.Save1.CameraLocation = new Vector3(0, 0, 0);
        BattleSave.Save1.SelectedshipNumber = 0;
        BattleSave.Save1.CountOfFlagship = 0;
        BattleSave.Save1.FlagshipMissionSuccessed = 0;

        BattleSave.Save1.FirstSkillType.Clear();
        BattleSave.Save1.FirstSkillNumber.Clear();
        BattleSave.Save1.SecondSkillType.Clear();
        BattleSave.Save1.SecondSkillNumber.Clear();
        BattleSave.Save1.ThirdSkillType.Clear();
        BattleSave.Save1.ThirdSkillNumber.Clear();

        BattleSave.Save1.FlagshipLocation.Clear();
        BattleSave.Save1.FlagshipRotanion.Clear();
        BattleSave.Save1.CountOfFormationShip.Clear();

        BattleSave.Save1.FlagshipShieldDown.Clear();
        BattleSave.Save1.FlagshipHull.Clear();
        BattleSave.Save1.FormationStorage.Clear();
        BattleSave.Save1.FlagshipFormationsCodex.Clear();

        BattleSave.Save1.Main1Left1DownFlagship.Clear();
        BattleSave.Save1.Main1Left2DownFlagship.Clear();
        BattleSave.Save1.Main1Right1DownFlagship.Clear();
        BattleSave.Save1.Main1Right2DownFlagship.Clear();
        BattleSave.Save1.Main2Left1DownFlagship.Clear();
        BattleSave.Save1.Main2Left2DownFlagship.Clear();
        BattleSave.Save1.Main2Right1DownFlagship.Clear();
        BattleSave.Save1.Main2Right2DownFlagship.Clear();
        BattleSave.Save1.Main3Left1DownFlagship.Clear();
        BattleSave.Save1.Main3Right1DownFlagship.Clear();

        BattleSave.Save1.Main1Left1HPFlagship.Clear();
        BattleSave.Save1.Main1Left2HPFlagship.Clear();
        BattleSave.Save1.Main1Right1HPFlagship.Clear();
        BattleSave.Save1.Main1Right2HPFlagship.Clear();
        BattleSave.Save1.Main2Left1HPFlagship.Clear();
        BattleSave.Save1.Main2Left2HPFlagship.Clear();
        BattleSave.Save1.Main2Right1HPFlagship.Clear();
        BattleSave.Save1.Main2Right2HPFlagship.Clear();
        BattleSave.Save1.Main3Left1HPFlagship.Clear();
        BattleSave.Save1.Main3Right1HPFlagship.Clear();

        if (BattleSave.Save1.DestinationArea.Count > 0)
            BattleSave.Save1.DestinationArea.Clear();
        if (BattleSave.Save1.WarpStartTime.Count > 0)
            BattleSave.Save1.WarpStartTime.Clear();
        if (BattleSave.Save1.WarpDriveReady.Count > 0)
            BattleSave.Save1.WarpDriveReady.Clear();
        if (BattleSave.Save1.WarpDriveActive.Count > 0)
            BattleSave.Save1.WarpDriveActive.Clear();

        BattleSave.Save1.FormationType.Clear();
        BattleSave.Save1.FormationShipLocation.Clear();
        BattleSave.Save1.FormationShipRotanion.Clear();

        BattleSave.Save1.FormationShieldDown.Clear();
        BattleSave.Save1.FormationShipHull.Clear();

        BattleSave.Save1.Main1Left1DownFormationShip.Clear();
        BattleSave.Save1.Main1Right1DownFormationShip.Clear();
        BattleSave.Save1.Main2Left1DownFormationShip.Clear();
        BattleSave.Save1.Main2Right1DownFormationShip.Clear();
        BattleSave.Save1.Main3Left1DownFormationShip.Clear();
        BattleSave.Save1.Main3Right1DownFormationShip.Clear();

        BattleSave.Save1.Main1Left1HPFormationShip.Clear();
        BattleSave.Save1.Main1Right1HPFormationShip.Clear();
        BattleSave.Save1.Main2Left1HPFormationShip.Clear();
        BattleSave.Save1.Main2Right1HPFormationShip.Clear();
        BattleSave.Save1.Main3Left1HPFormationShip.Clear();
        BattleSave.Save1.Main3Right1HPFormationShip.Clear();

        if (BattleSave.Save1.WarpDriveReady.Count > 0)
            BattleSave.Save1.WarpDriveReady.Clear();
        if (BattleSave.Save1.WarpDriveActive.Count > 0)
            BattleSave.Save1.WarpDriveActive.Clear();

        BattleSave.Save1.CannonType.Clear();
        BattleSave.Save1.AttackTime.Clear();
        BattleSave.Save1.FlagshipTurretDownList.Clear();
        BattleSave.Save1.FormationTurretDownList.Clear();

        BattleSave.Save1.EnemyIsBattleSite.Clear();
        BattleSave.Save1.EnemyBattleSiteNumber.Clear();

        BattleSave.Save1.SelectedEnemyFlagship.Clear();
        BattleSave.Save1.CountOfEnemyFlagship = 0;
        BattleSave.Save1.EnemyFlagshipLocation.Clear();
        BattleSave.Save1.EnemyFlagshipRotanion.Clear();

        BattleSave.Save1.EnemyNationType.Clear();
        BattleSave.Save1.EnemyFlagshipShieldDown.Clear();
        BattleSave.Save1.EnemyFlagshipHull.Clear();
        BattleSave.Save1.EnemyFlagshipShield.Clear();

        BattleSave.Save1.Main1Left1DownEnemyFlagship.Clear();
        BattleSave.Save1.Main1Left2DownEnemyFlagship.Clear();
        BattleSave.Save1.Main1Right1DownEnemyFlagship.Clear();
        BattleSave.Save1.Main1Right2DownEnemyFlagship.Clear();
        BattleSave.Save1.Main2Left1DownEnemyFlagship.Clear();
        BattleSave.Save1.Main2Left2DownEnemyFlagship.Clear();
        BattleSave.Save1.Main2Right1DownEnemyFlagship.Clear();
        BattleSave.Save1.Main2Right2DownEnemyFlagship.Clear();
        BattleSave.Save1.Main3Left1DownEnemyFlagship.Clear();
        BattleSave.Save1.Main3Right1DownEnemyFlagship.Clear();

        BattleSave.Save1.Main1Left1HPEnemyFlagship.Clear();
        BattleSave.Save1.Main1Left2HPEnemyFlagship.Clear();
        BattleSave.Save1.Main1Right1HPEnemyFlagship.Clear();
        BattleSave.Save1.Main1Right2HPEnemyFlagship.Clear();
        BattleSave.Save1.Main2Left1HPEnemyFlagship.Clear();
        BattleSave.Save1.Main2Left2HPEnemyFlagship.Clear();
        BattleSave.Save1.Main2Right1HPEnemyFlagship.Clear();
        BattleSave.Save1.Main2Right2HPEnemyFlagship.Clear();
        BattleSave.Save1.Main3Left1HPEnemyFlagship.Clear();
        BattleSave.Save1.Main3Right1HPEnemyFlagship.Clear();

        if (BattleSave.Save1.EnemyDestinationArea.Count > 0)
            BattleSave.Save1.EnemyDestinationArea.Clear();
        if (BattleSave.Save1.EnemyWarpStartTime.Count > 0)
            BattleSave.Save1.EnemyWarpStartTime.Clear();
        if (BattleSave.Save1.EnemyWarpDriveActive.Count > 0)
            BattleSave.Save1.EnemyWarpDriveActive.Clear();

        BattleSave.Save1.EnemyIsBattleSite.Clear();
        BattleSave.Save1.EnemyBattleSiteNumber.Clear();

        BattleSave.Save1.CountOfEnemyFormationShip.Clear();
        BattleSave.Save1.EnemyNationType.Clear();
        BattleSave.Save1.EnemyFormationType.Clear();
        BattleSave.Save1.EnemyFormationShipLocation.Clear();
        BattleSave.Save1.EnemyFormationShipRotanion.Clear();

        BattleSave.Save1.EnemyFormationShieldDown.Clear();
        BattleSave.Save1.EnemyFormationShipHull.Clear();
        BattleSave.Save1.EnemyFormationShield.Clear();

        BattleSave.Save1.Main1Left1DownEnemyFormationShip.Clear();
        BattleSave.Save1.Main1Right1DownEnemyFormationShip.Clear();
        BattleSave.Save1.Main2Left1DownEnemyFormationShip.Clear();
        BattleSave.Save1.Main2Right1DownEnemyFormationShip.Clear();

        BattleSave.Save1.Main1Left1HPEnemyFormationShip.Clear();
        BattleSave.Save1.Main1Right1HPEnemyFormationShip.Clear();
        BattleSave.Save1.Main2Left1HPEnemyFormationShip.Clear();
        BattleSave.Save1.Main2Right1HPEnemyFormationShip.Clear();

        if (BattleSave.Save1.EnemyWarpDriveActive.Count > 0)
            BattleSave.Save1.EnemyWarpDriveActive.Clear();

        BattleSave.Save1.EnemyCannonType.Clear();
        BattleSave.Save1.EnemyAttackTime.Clear();
        BattleSave.Save1.EnemyFlagshipTurretDownList.Clear();
        BattleSave.Save1.EnemyFormationTurretDownList.Clear();

        BattleSave.Save1.RandomSiteNumber.Clear();
        BattleSave.Save1.RandomSiteTransform.Clear();

        BattleSave.Save1.PlanetNumber.Clear();
        BattleSave.Save1.FirstFreePlanet.Clear();
        BattleSave.Save1.BattleVictoryPlanet.Clear();

        BattleSave.Save1.GoBackTitle = false;
    }
}