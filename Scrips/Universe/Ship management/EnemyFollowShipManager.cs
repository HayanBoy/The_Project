using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowShipManager : MonoBehaviour
{
    public int ShipAccount; //�Ҽ� �Լ� ����
    public List<GameObject> ShipList = new List<GameObject>(); //�Ҽ� �Լ� ���
    public List<int> EmptyLocationList = new List<int>(); //����� �Ҽ� �ڸ� ���(�Լ� ���� �� �Լ� ���꿡�� ��ġ�� �� ���δ�.)
    public int ManagerNumber; //���� �Ҽ��Լ� �Ŵ��� ��ȣ
    public GameObject WarpFormation;
    public int FormationStorage; //��� ���� ��

    //�Դ� ���� ��ġ
    public Transform Location1;
    public Transform Location2;
    public Transform Location3;
    public Transform Location4;
    public Transform Location5;
    public Transform Location6;
    public Transform Location7;
    public Transform Location8;
    public Transform Location9;
    public Transform Location10;
    public Transform Location11;
    public Transform Location12;
    public Transform Location13;
    public Transform Location14;
    public Transform Location15;
    public Transform Location16;
    public Transform Location17;
    public Transform Location18;
    public Transform Location19;
    public Transform Location20;

    public void GetFormationForNewShip()
    {
        int i = ShipList.Count - 1;

        if (i == 0)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location1;
        }
        else if (i == 1)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location2;
        }
        else if (i == 2)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location3;
        }
        else if (i == 3)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location4;
        }
        else if (i == 4)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location5;
        }
        else if (i == 5)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location6;
        }
        else if (i == 6)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location7;
        }
        else if (i == 7)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location8;
        }
        else if (i == 8)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location9;
        }
        else if (i == 9)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location10;
        }
        else if (i == 10)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location11;
        }
        else if (i == 11)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location12;
        }
        else if (i == 12)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location13;
        }
        else if (i == 13)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location14;
        }
        else if (i == 14)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location15;
        }
        else if (i == 15)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location16;
        }
        else if (i == 16)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location17;
        }
        else if (i == 17)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location18;
        }
        else if (i == 18)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location19;
        }
        else if (i == 19)
        {
            ShipList[i].GetComponent<EnemyShipBehavior>().FormationIndex = Location20;
        }
    }
}