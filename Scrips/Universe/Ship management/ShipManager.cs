using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    [Header("함대전 리스트")]
    public List<GameObject> FlagShipList = new List<GameObject>(); //기함 목록
    public List<GameObject> SelectedFlagShip = new List<GameObject>(); //선택됨 기함 목록
    public List<ShipRTS> SelectedShipList = new List<ShipRTS>(); //선택됨 함선 목록

    [Header("함대 메뉴 리스트")]
    public List<GameObject> FleetShipList = new List<GameObject>(); //함대 메뉴 전용 기함 목록
    public List<GameObject> SelectedFleetFlagship = new List<GameObject>(); //선택된 함대 메뉴 전용 기함 목록
    public List<GameObject> SelectedFleetShips = new List<GameObject>(); //직접 선택한 함선 리스트

    [Header("각 기함별 함선 수 리스트")]
    public List<int> AmountOfFormationShip = new List<int>();
    public List<int> AmountOfShieldShip = new List<int>();
    public List<int> AmountOfCarrier = new List<int>();

    [Header("사이트 리스트")]
    public List<GameObject> FreePlanetList = new List<GameObject>(); //해방된 행성 리스트
    public List<GameObject> ActiveBattleSiteList = new List<GameObject>(); //활성화 중인 전투 사이트 리스트

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}