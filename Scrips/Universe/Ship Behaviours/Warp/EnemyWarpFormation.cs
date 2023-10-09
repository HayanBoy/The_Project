using UnityEngine;

public class EnemyWarpFormation : MonoBehaviour
{
    //워프 위치
    public Transform WarpLocation1;
    public Transform WarpLocation2;
    public Transform WarpLocation3;
    public Transform WarpLocation4;
    public Transform WarpLocation5;
    public Transform WarpLocation6;
    public Transform WarpLocation7;
    public Transform WarpLocation8;
    public Transform WarpLocation9;
    public Transform WarpLocation10;
    public Transform WarpLocation11;
    public Transform WarpLocation12;
    public Transform WarpLocation13;
    public Transform WarpLocation14;
    public Transform WarpLocation15;
    public Transform WarpLocation16;
    public Transform WarpLocation17;
    public Transform WarpLocation18;
    public Transform WarpLocation19;
    public Transform WarpLocation20;

    public void FastWarpFormationGet(Vector3 Location, Quaternion Roation)
    {
        transform.position = Location;
        transform.rotation = Roation;
    }

    public void GetFormationForNewShip(GameObject Flagship, int number)
    {
        if (number == 0)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation1;
            WarpLocation1.transform.position = Flagship.transform.Find("Fleet formation control/Ship location1").transform.position;
        }
        else if (number == 1)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation2;
            WarpLocation2.transform.position = Flagship.transform.Find("Fleet formation control/Ship location2").transform.position;
        }
        else if (number == 2)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation3;
            WarpLocation3.transform.position = Flagship.transform.Find("Fleet formation control/Ship location3").transform.position;
        }
        else if (number == 3)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation4;
            WarpLocation4.transform.position = Flagship.transform.Find("Fleet formation control/Ship location4").transform.position;
        }
        else if (number == 4)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation5;
            WarpLocation5.transform.position = Flagship.transform.Find("Fleet formation control/Ship location5").transform.position;
        }
        else if (number == 5)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation6;
            WarpLocation6.transform.position = Flagship.transform.Find("Fleet formation control/Ship location6").transform.position;
        }
        else if (number == 6)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation7;
            WarpLocation7.transform.position = Flagship.transform.Find("Fleet formation control/Ship location7").transform.position;
        }
        else if (number == 7)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation8;
            WarpLocation8.transform.position = Flagship.transform.Find("Fleet formation control/Ship location8").transform.position;
        }
        else if (number == 8)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation9;
        }
        else if (number == 9)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation10;
        }
        else if (number == 10)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation11;
        }
        else if (number == 11)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation12;
        }
        else if (number == 12)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation13;
        }
        else if (number == 13)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation14;
        }
        else if (number == 14)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation15;
        }
        else if (number == 15)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation16;
        }
        else if (number == 16)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation17;
        }
        else if (number == 17)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation18;
        }
        else if (number == 18)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation19;
        }
        else if (number == 19)
        {
            Flagship.GetComponent<EnemyFollowShipManager>().ShipList[number].GetComponent<EnemyShipBehavior>().WarpformationIndex = WarpLocation20;
        }
    }
}