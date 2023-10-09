using System.Collections;
using UnityEngine;

public class ClearLine : MonoBehaviour
{
    public BattleMessages BattleMessages;
    public MissionCompleteUI MissionCompleteUI;
    public Stage1_9 Stage1_9;
    AreaStatement AreaStatement;

    public GameObject Shuttle;
    public GameObject ShuttleEnter;
    private Transform ShuttlePos;
    public Transform TestPos; //테스트 전용

    private void Start()
    {
        //Invoke("TestStart", 20);
    }

    void TestStart()
    {
        MissionComplete(TestPos);
    }

    //승리 창 띄우기
    public void MissionComplete(Transform Pos)
    {
        ShuttlePos = Pos;
        Stage1_9.StopSpawn();
        StartCoroutine(MissionCompleteUI.MissionCompleteUIStart());
    }

    //미션 실패 창 띄우기
    public void MissionFail()
    {
        MissionCompleteUI.isMissionFailed = true;
        StartCoroutine(MissionCompleteUI.MissionFaillUIStart());
    }

    //미션 완료 이후, OK버튼을 누르면 수송기가 도착
    public void ShuttleArrival()
    {
        if (BattleSave.Save1.MissionArea == 100 || BattleSave.Save1.MissionArea == 101)
            BattleSave.Save1.FlagshipMissionSuccessed++; //미션 성공처리

        MissionTableCut(BattleSave.Save1.MissionPlanetNumber, BattleSave.Save1.SelectedMissionTableNumber);
        Movement Movement = GameObject.Find("Play Control/Player").GetComponent<Movement>();
        Movement.MissionComplete = false;

        GameControlSystem GameControlSystem = GameObject.Find("Play Control/Game Control").GetComponent<GameControlSystem>();

        if (Movement.VehicleActive == true)
        {
            if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            {
                StartCoroutine(GameControlSystem.ActiveVehicleUI());
                StartCoroutine(GameControlSystem.ActiveVehicleController());
            }
        }
        else
        {
            StartCoroutine(GameControlSystem.StartButten());
            StartCoroutine(GameControlSystem.ActivePlayerController());
            StartCoroutine(GameControlSystem.StartShipUIActive());
        }
        StartCoroutine(GameControlSystem.HealthBarActive());
        Shuttle.SetActive(true);
        Shuttle.transform.parent.transform.position = new Vector2(ShuttlePos.transform.position.x, Shuttle.transform.position.y);
        Shuttle.GetComponent<Animator>().SetFloat("Mission Complete, HA-767", 1);
        StartCoroutine(StartShuttleIdle());
    }

    IEnumerator StartShuttleIdle()
    {
        yield return new WaitForSeconds(5);
        Shuttle.GetComponent<Animator>().SetFloat("Mission Complete, HA-767", 2);
    }

    //미션완료 후, 지정된 미션 테이블 삭제
    public void MissionTableCut(int PlanetNumber, int number)
    {
        AreaStatement = FindObjectOfType<AreaStatement>();
        BattleSave.Save1.MissionSuccessed = true;

        if (PlanetNumber == 1001)
        {
            MissionCompleteManager.MCMInstance.SatariusGlessiaMission[number] = true;
            MissionCompleteManager.MCMInstance.SatariusGlessiaMissionCompleteCount++;
        }
        else if (PlanetNumber == 1002)
        {
            MissionCompleteManager.MCMInstance.AposisMission[number] = true;
            MissionCompleteManager.MCMInstance.AposisMissionCompleteCount++;

        }
        else if (PlanetNumber == 1003)
        {
            MissionCompleteManager.MCMInstance.ToronoMission[number] = true;
            MissionCompleteManager.MCMInstance.ToronoMissionCompleteCount++;
        }
        else if (PlanetNumber == 1004)
        {
            MissionCompleteManager.MCMInstance.PlopaIIMission[number] = true;
            MissionCompleteManager.MCMInstance.PlopaIIMissionCompleteCount++;
        }
        else if (PlanetNumber == 1005)
        {
            MissionCompleteManager.MCMInstance.VedesVIMission[number] = true;
            MissionCompleteManager.MCMInstance.VedesVIMissionCompleteCount++;
        }
        else if (PlanetNumber == 1006)
        {
            MissionCompleteManager.MCMInstance.AronPeriMission[number] = true;
            MissionCompleteManager.MCMInstance.AronPeriMissionCompleteCount++;
        }
        else if (PlanetNumber == 1007)
        {
            MissionCompleteManager.MCMInstance.PapatusIIMission[number] = true;
            MissionCompleteManager.MCMInstance.PapatusIIMissionCompleteCount++;
        }
        else if (PlanetNumber == 1008)
        {
            MissionCompleteManager.MCMInstance.PapatusIIIMission[number] = true;
            MissionCompleteManager.MCMInstance.PapatusIIIMissionCompleteCount++;
        }
        else if (PlanetNumber == 1009)
        {
            MissionCompleteManager.MCMInstance.KyepotorosMission[number] = true;
            MissionCompleteManager.MCMInstance.KyepotorosMissionCompleteCount++;
        }
        else if (PlanetNumber == 1010)
        {
            MissionCompleteManager.MCMInstance.TratosMission[number] = true;
            MissionCompleteManager.MCMInstance.TratosMissionCompleteCount++;
        }
        else if (PlanetNumber == 1011)
        {
            MissionCompleteManager.MCMInstance.OclasisMission[number] = true;
            MissionCompleteManager.MCMInstance.OclasisMissionCompleteCount++;
        }
        else if (PlanetNumber == 1012)
        {
            MissionCompleteManager.MCMInstance.DeriousHeriMission[number] = true;
            MissionCompleteManager.MCMInstance.DeriousHeriMissionCompleteCount++;
        }
        else if (PlanetNumber == 1013)
        {
            MissionCompleteManager.MCMInstance.VeltrorexyMission[number] = true;
            MissionCompleteManager.MCMInstance.VeltrorexyMissionCompleteCount++;
        }
        else if (PlanetNumber == 1014)
        {
            MissionCompleteManager.MCMInstance.ErixJeoqetaMission[number] = true;
            MissionCompleteManager.MCMInstance.ErixJeoqetaMissionCompleteCount++;
        }
        else if (PlanetNumber == 1015)
        {
            MissionCompleteManager.MCMInstance.QeepoMission[number] = true;
            MissionCompleteManager.MCMInstance.QeepoMissionCompleteCount++;
        }
        else if (PlanetNumber == 1016)
        {
            MissionCompleteManager.MCMInstance.CrownYosereMission[number] = true;
            MissionCompleteManager.MCMInstance.CrownYosereMissionCompleteCount++;
        }
        else if (PlanetNumber == 1017)
        {
            MissionCompleteManager.MCMInstance.OrosMission[number] = true;
            MissionCompleteManager.MCMInstance.OrosMissionCompleteCount++;
        }
        else if (PlanetNumber == 1018)
        {
            MissionCompleteManager.MCMInstance.JapetAgroneMission[number] = true;
            MissionCompleteManager.MCMInstance.JapetAgroneMissionCompleteCount++;
        }
        else if (PlanetNumber == 1019)
        {
            MissionCompleteManager.MCMInstance.Xacro042351Mission[number] = true;
            MissionCompleteManager.MCMInstance.Xacro042351MissionCompleteCount++;
        }
        else if (PlanetNumber == 1020)
        {
            MissionCompleteManager.MCMInstance.DeltaD31_2208Mission[number] = true;
            MissionCompleteManager.MCMInstance.DeltaD31_2208MissionCompleteCount++;
        }
        else if (PlanetNumber == 1021)
        {
            MissionCompleteManager.MCMInstance.DeltaD31_9523Mission[number] = true;
            MissionCompleteManager.MCMInstance.DeltaD31_9523MissionCompleteCount++;
        }
        else if (PlanetNumber == 1022)
        {
            MissionCompleteManager.MCMInstance.DeltaD31_12721Mission[number] = true;
            MissionCompleteManager.MCMInstance.DeltaD31_12721MissionCompleteCount++;
        }
        else if (PlanetNumber == 1023)
        {
            MissionCompleteManager.MCMInstance.JeratoO95_1125Mission[number] = true;
            MissionCompleteManager.MCMInstance.JeratoO95_1125MissionCompleteCount++;
        }
        else if (PlanetNumber == 1024)
        {
            MissionCompleteManager.MCMInstance.JeratoO95_2252Mission[number] = true;
            MissionCompleteManager.MCMInstance.JeratoO95_2252MissionCompleteCount++;
        }
        else if (PlanetNumber == 1025)
        {
            MissionCompleteManager.MCMInstance.JeratoO95_8510Mission[number] = true;
            MissionCompleteManager.MCMInstance.JeratoO95_8510MissionCompleteCount++;
        }
    }
}
