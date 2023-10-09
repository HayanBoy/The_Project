using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    [Header("�Դ��� ����Ʈ")]
    public List<GameObject> FlagShipList = new List<GameObject>(); //���� ���
    public List<GameObject> SelectedFlagShip = new List<GameObject>(); //���õ� ���� ���
    public List<ShipRTS> SelectedShipList = new List<ShipRTS>(); //���õ� �Լ� ���

    [Header("�Դ� �޴� ����Ʈ")]
    public List<GameObject> FleetShipList = new List<GameObject>(); //�Դ� �޴� ���� ���� ���
    public List<GameObject> SelectedFleetFlagship = new List<GameObject>(); //���õ� �Դ� �޴� ���� ���� ���
    public List<GameObject> SelectedFleetShips = new List<GameObject>(); //���� ������ �Լ� ����Ʈ

    [Header("�� ���Ժ� �Լ� �� ����Ʈ")]
    public List<int> AmountOfFormationShip = new List<int>();
    public List<int> AmountOfShieldShip = new List<int>();
    public List<int> AmountOfCarrier = new List<int>();

    [Header("����Ʈ ����Ʈ")]
    public List<GameObject> FreePlanetList = new List<GameObject>(); //�ع�� �༺ ����Ʈ
    public List<GameObject> ActiveBattleSiteList = new List<GameObject>(); //Ȱ��ȭ ���� ���� ����Ʈ ����Ʈ

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}