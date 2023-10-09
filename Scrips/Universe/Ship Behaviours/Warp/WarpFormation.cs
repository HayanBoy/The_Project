using UnityEngine;

public class WarpFormation : MonoBehaviour
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

    //각 편대 및 전략 함선에게 순서대로 WarpLocation를 부여하기
    public void GetFormationForNewShip(GameObject Flagship, int number)
    {
        if (number == 0 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 1)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation1;
            WarpLocation1.transform.position = Flagship.transform.Find("Fleet formation control/Ship location1").transform.position;
        }
        else if (number == 1 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 2)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation2;
            WarpLocation2.transform.position = Flagship.transform.Find("Fleet formation control/Ship location2").transform.position;
        }
        else if (number == 2 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 3)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation3;
            WarpLocation3.transform.position = Flagship.transform.Find("Fleet formation control/Ship location3").transform.position;
        }
        else if (number == 3 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 4)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation4;
            WarpLocation4.transform.position = Flagship.transform.Find("Fleet formation control/Ship location4").transform.position;
        }
        else if (number == 4 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 5)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation5;
            WarpLocation5.transform.position = Flagship.transform.Find("Fleet formation control/Ship location5").transform.position;
        }
        else if (number == 5 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 6)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation6;
            WarpLocation6.transform.position = Flagship.transform.Find("Fleet formation control/Ship location6").transform.position;
        }
        else if (number == 6 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 7)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation7;
            WarpLocation7.transform.position = Flagship.transform.Find("Fleet formation control/Ship location7").transform.position;
        }
        else if (number == 7 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 8)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation8;
            WarpLocation8.transform.position = Flagship.transform.Find("Fleet formation control/Ship location8").transform.position;
        }
        else if (number == 8 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 9)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation9;
            WarpLocation9.transform.position = Flagship.transform.Find("Fleet formation control/Ship location9").transform.position;
        }
        else if (number == 9 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 10)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation10;
            WarpLocation10.transform.position = Flagship.transform.Find("Fleet formation control/Ship location10").transform.position;
        }
        else if (number == 10 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 11)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation11;
        }
        else if (number == 11 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 12)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation12;
        }
        else if (number == 12 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 13)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation13;
        }
        else if (number == 13 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 14)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation14;
        }
        else if (number == 14 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 15)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation15;
        }
        else if (number == 15 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 16)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation16;
        }
        else if (number == 16 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 17)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation17;
        }
        else if (number == 17 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 18)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation18;
        }
        else if (number == 18 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 19)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation19;
        }
        else if (number == 19 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 20)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation20;
        }
    }

    //이전받은 함선이 순차적으로 먼저 빈 편대 번호에 배치하기
    public void EmptyLocationDistribution(GameObject Flagship, int number)
    {
        if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 1 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 1)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation1;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 2 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 2)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation2;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 3 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 3)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation3;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 4 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 4)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation4;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 5 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 5)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation5;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 6 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 6)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation6;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 7 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 7)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation7;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 8 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 8)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation8;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 9 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 9)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation9;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 10 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 10)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation10;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 11 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 11)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation11;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 12 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 12)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation12;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 13 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 13)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation13;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 14 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 14)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation14;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 15 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 15)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation15;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 16 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 16)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation16;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 17 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 17)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation17;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 18 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 18)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation18;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 19 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 19)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation19;
        }
        else if (Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0] == 20 && Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 20)
        {
            Flagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().WarpformationIndex = WarpLocation20;
        }
    }
}