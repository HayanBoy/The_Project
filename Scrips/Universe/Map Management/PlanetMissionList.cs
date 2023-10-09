using UnityEngine.UI;
using UnityEngine;

public class PlanetMissionList : MonoBehaviour
{
    public int MissionLevel; //������ ���� ���� �� ������ ���� �����ȴ�.
    public MissionTable[] missionTableList;

    //���̺� �� �̼�
    [System.Serializable]
    public struct MissionTable
    {
        public GameObject MissionTablePrefab;
        public Text AreaName;
        public Text MissionName;
        public int FinishSpawnNumber;

        //�̼� ���� ����
        public enum MissionArea
        {
            CityOnFire,
            AtmosphereStation,
            CityAtAisle
        }
        public MissionArea MissionAreaType;

        //�̼� ����
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