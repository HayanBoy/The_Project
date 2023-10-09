using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    [Header("나리하 함대 스코어")]
    public int GetNarihaFlagshipCnt = 0; //함선 생산 횟수
    public int GetNarihaFormationShipCnt = 0;
    public int GetNarihaTacticalShipCnt = 0;
    public int LostNarihaFlagshipCnt = 0; //함선 손실 횟수
    public int LostNarihaFormationShipCnt = 0;
    public int LostNarihaTacticalShipCnt = 0;

    [Header("우주 스코어")]
    public int TotalDeltaHurricaneMission = 0;
    public int TotalDeltaHurricanePlanetMission = 0;
    public int TotalDeltaHurricanePlanetControsConquestMission = 0;
    public int TotalDeltaHurricanePlanetControsDefenceMission = 0;
    public int TotalDeltaHurricanePlanetZombieConquestMission = 0;
    public int TotalDeltaHurricanePlanetZombieDefenceMission = 0;
    public int TotalDeltaHurricaneFlagshipMission = 0;
    public int TotalDeltaHurricaneFailedMission = 0;
    public float TotalWarpDistance = 0; //함대가 총 날아간 거리

    [Header("함대전 적 격침 스코어")]
    public int EnemyAllFlagshipCnt = 0;
    public int EnemyAllFormationShipCnt = 0;
    public int EnemyKantakriFlagshipCnt = 0;
    public int EnemyKantakriFormationShipCnt = 0;
    public int EnemySloriusFlagshipCnt = 0;
    public int EnemySloriusFormationShipCnt = 0;

    [Header("보병전 적 처치 스코어")]
    public int enemyCnt = 0;
    public int KantakriCnt = 0;
    public int KantakriEliteCnt = 0;
    public int SloriusCnt = 0;
    public int SloriusEliteCnt = 0;
    public int InfectorCnt = 0;
    public int InfectorEliteCnt = 0;
    public int AllCnt = 0;

    public List<GameObject> EnemyList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void AllEnemyCnt(int value)
    {
        AllCnt += value;
    }

    public void setScore(int value)
    {
        enemyCnt += value;
    }

    public void DieCnt(int value)
    {
        AllCnt -= value;
    }

    public void set_Kantakri_Score(int value)
    {
        KantakriCnt += value;
    }

    public void set_Slorius_Score(int value)
    {
        SloriusCnt += value;
    }

    public void set_Infector_Score(int value)
    {
        InfectorCnt += value;
    }

    public int getScore()
    {
        return enemyCnt;
    }

    public void GameOverCnt()
    {
        AllCnt = 0;
        EnemyList = null;
    }

    public void GetData(SerializableScoreManager values)
    {
        GetNarihaFlagshipCnt = values.GetNarihaFlagshipCnt;
        GetNarihaFormationShipCnt = values.GetNarihaFormationShipCnt;
        GetNarihaTacticalShipCnt = values.GetNarihaTacticalShipCnt;
        LostNarihaFlagshipCnt = values.LostNarihaFlagshipCnt;
        LostNarihaFormationShipCnt = values.LostNarihaFormationShipCnt;
        LostNarihaTacticalShipCnt = values.LostNarihaTacticalShipCnt;

        TotalDeltaHurricaneMission = values.TotalDeltaHurricaneMission;
        TotalDeltaHurricanePlanetMission = values.TotalDeltaHurricanePlanetMission;
        TotalDeltaHurricanePlanetControsConquestMission = values.TotalDeltaHurricanePlanetControsConquestMission;
        TotalDeltaHurricanePlanetControsDefenceMission = values.TotalDeltaHurricanePlanetControsDefenceMission;
        TotalDeltaHurricanePlanetZombieConquestMission = values.TotalDeltaHurricanePlanetZombieConquestMission;
        TotalDeltaHurricanePlanetZombieDefenceMission = values.TotalDeltaHurricanePlanetZombieDefenceMission;
        TotalDeltaHurricaneFailedMission = values.TotalDeltaHurricaneFailedMission;
        TotalDeltaHurricaneFlagshipMission = values.TotalDeltaHurricaneFlagshipMission;
        TotalWarpDistance = values.TotalWarpDistance;

        EnemyAllFlagshipCnt = values.EnemyAllFlagshipCnt;
        EnemyAllFormationShipCnt = values.EnemyAllFormationShipCnt;
        EnemyKantakriFlagshipCnt = values.EnemyKantakriFlagshipCnt;
        EnemyKantakriFormationShipCnt = values.EnemyKantakriFormationShipCnt;
        EnemySloriusFlagshipCnt = values.EnemySloriusFlagshipCnt;
        EnemySloriusFormationShipCnt = values.EnemySloriusFormationShipCnt;

        enemyCnt = values.enemyCnt;
        KantakriCnt = values.KantakriCnt;
        KantakriEliteCnt = values.KantakriEliteCnt;
        SloriusCnt = values.SloriusCnt;
        SloriusEliteCnt = values.SloriusEliteCnt;
        InfectorCnt = values.InfectorCnt;
        InfectorEliteCnt = values.InfectorEliteCnt;
    }

    public SerializableScoreManager GetSerializable()
    {
        var output = new SerializableScoreManager();

        output.GetNarihaFlagshipCnt = this.GetNarihaFlagshipCnt;
        output.GetNarihaFormationShipCnt = this.GetNarihaFormationShipCnt;
        output.GetNarihaTacticalShipCnt = this.GetNarihaTacticalShipCnt;
        output.LostNarihaFlagshipCnt = this.LostNarihaFlagshipCnt;
        output.LostNarihaFormationShipCnt = this.LostNarihaFormationShipCnt;
        output.LostNarihaTacticalShipCnt = this.LostNarihaTacticalShipCnt;

        output.TotalDeltaHurricaneMission = this.TotalDeltaHurricaneMission;
        output.TotalDeltaHurricanePlanetMission = this.TotalDeltaHurricanePlanetMission;
        output.TotalDeltaHurricanePlanetControsConquestMission = this.TotalDeltaHurricanePlanetControsConquestMission;
        output.TotalDeltaHurricanePlanetControsDefenceMission = this.TotalDeltaHurricanePlanetControsDefenceMission;
        output.TotalDeltaHurricanePlanetZombieConquestMission = this.TotalDeltaHurricanePlanetZombieConquestMission;
        output.TotalDeltaHurricanePlanetZombieDefenceMission = this.TotalDeltaHurricanePlanetZombieDefenceMission;
        output.TotalDeltaHurricaneFailedMission = this.TotalDeltaHurricaneFailedMission;
        output.TotalDeltaHurricaneFlagshipMission = this.TotalDeltaHurricaneFlagshipMission;
        output.TotalWarpDistance = this.TotalWarpDistance;

        output.EnemyAllFlagshipCnt = this.EnemyAllFlagshipCnt;
        output.EnemyAllFormationShipCnt = this.EnemyAllFormationShipCnt;
        output.EnemyKantakriFlagshipCnt = this.EnemyKantakriFlagshipCnt;
        output.EnemyKantakriFormationShipCnt = this.EnemyKantakriFormationShipCnt;
        output.EnemySloriusFlagshipCnt = this.EnemySloriusFlagshipCnt;
        output.EnemySloriusFormationShipCnt = this.EnemySloriusFormationShipCnt;

        output.enemyCnt = this.enemyCnt;
        output.KantakriCnt = this.KantakriCnt;
        output.KantakriEliteCnt = this.KantakriEliteCnt;
        output.SloriusCnt = this.SloriusCnt;
        output.SloriusEliteCnt = this.SloriusEliteCnt;
        output.InfectorCnt = this.InfectorCnt;
        output.InfectorEliteCnt = this.InfectorEliteCnt;

        return output;
    }

    [Serializable]
    public class SerializableScoreManager
    {
        [Header("나리하 함대 스코어")]
        public int GetNarihaFlagshipCnt = 0; //함선 생산 횟수
        public int GetNarihaFormationShipCnt = 0;
        public int GetNarihaTacticalShipCnt = 0;
        public int LostNarihaFlagshipCnt = 0; //함선 손실 횟수
        public int LostNarihaFormationShipCnt = 0;
        public int LostNarihaTacticalShipCnt = 0;

        [Header("우주 스코어")]
        public int TotalDeltaHurricaneMission = 0;
        public int TotalDeltaHurricanePlanetMission = 0;
        public int TotalDeltaHurricanePlanetControsConquestMission = 0;
        public int TotalDeltaHurricanePlanetControsDefenceMission = 0;
        public int TotalDeltaHurricanePlanetZombieConquestMission = 0;
        public int TotalDeltaHurricanePlanetZombieDefenceMission = 0;
        public int TotalDeltaHurricaneFlagshipMission = 0;
        public int TotalDeltaHurricaneFailedMission = 0;
        public float TotalWarpDistance = 0; //함대가 총 날아간 거리

        [Header("함대전 적 격침 스코어")]
        public int EnemyAllFlagshipCnt = 0;
        public int EnemyAllFormationShipCnt = 0;
        public int EnemyKantakriFlagshipCnt = 0;
        public int EnemyKantakriFormationShipCnt = 0;
        public int EnemySloriusFlagshipCnt = 0;
        public int EnemySloriusFormationShipCnt = 0;

        [Header("보병전 적 처치 스코어")]
        public int enemyCnt = 0;
        public int KantakriCnt = 0;
        public int KantakriEliteCnt = 0;
        public int SloriusCnt = 0;
        public int SloriusEliteCnt = 0;
        public int InfectorCnt = 0;
        public int InfectorEliteCnt = 0;
    }
}