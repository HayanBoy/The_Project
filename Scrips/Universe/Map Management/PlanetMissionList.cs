using UnityEngine.UI;
using UnityEngine;

public class PlanetMissionList : MonoBehaviour
{
    public int MissionLevel; //레벨이 높을 수록 더 강력한 적이 스폰된다.
    public MissionTable[] missionTableList;

    //테이블 별 미션
    [System.Serializable]
    public struct MissionTable
    {
        public GameObject MissionTablePrefab;
        public Text AreaName;
        public Text MissionName;
        public int FinishSpawnNumber;

        //미션 지역 종류
        public enum MissionArea
        {
            CityOnFire,
            AtmosphereStation,
            CityAtAisle
        }
        public MissionArea MissionAreaType;

        //미션 종류
        public enum MissionType
        {
            ConquestFromContros,
            DefenceFromContros,
            ConquestFromZonbie,
            DefenceFromZonbie,
            ConquestFromSloriusShip,
            ConquestFromKantakriShip
        }
        public MissionType missionType;
    }
}