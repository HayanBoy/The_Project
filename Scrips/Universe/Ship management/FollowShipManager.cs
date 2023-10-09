using System.Collections.Generic;
using UnityEngine;

public class FollowShipManager : MonoBehaviour
{
    public int ShipAccount; //소속 함선 갯수
    public List<GameObject> ShipList = new List<GameObject>(); //소속 함선 목록
    public List<int> EmptyLocationList = new List<int>(); //비어진 소속 자리 목록(함선 이전 및 함선 생산에서 배치할 때 쓰인다.)
    public int ManagerNumber; //기함 소속함선 매니저 번호
    public GameObject WarpFormation;
    public int FormationStorage; //편대 수용 수

    //함대 전형 위치
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

    public GameObject FormationRangePrefab;
    public GameObject LocationPrefab1;
    public GameObject LocationPrefab2;
    public GameObject LocationPrefab3;
    public GameObject LocationPrefab4;
    public GameObject LocationPrefab5;
    public GameObject LocationPrefab6;
    public GameObject LocationPrefab7;
    public GameObject LocationPrefab8;
    public GameObject LocationPrefab9;
    public GameObject LocationPrefab10;
    public GameObject LocationPrefab11;
    public GameObject LocationPrefab12;
    public GameObject LocationPrefab13;
    public GameObject LocationPrefab14;
    public GameObject LocationPrefab15;
    public GameObject LocationPrefab16;
    public GameObject LocationPrefab17;
    public GameObject LocationPrefab18;
    public GameObject LocationPrefab19;
    public GameObject LocationPrefab20;

    //배열 모드 활성화
    public void SelectModeOnline()
    {
        FormationRangePrefab.SetActive(true);

        for (int i = 0; i < ShipList.Count; i++)
        {
            if (LocationPrefab1 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 1)
                LocationPrefab1.SetActive(true);
            if (LocationPrefab2 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 2)
                LocationPrefab2.SetActive(true);
            if (LocationPrefab3 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 3)
                LocationPrefab3.SetActive(true);
            if (LocationPrefab4 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 4)
                LocationPrefab4.SetActive(true);
            if (LocationPrefab5 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 5)
                LocationPrefab5.SetActive(true);
            if (LocationPrefab6 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 6)
                LocationPrefab6.SetActive(true);
            if (LocationPrefab7 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 7)
                LocationPrefab7.SetActive(true);
            if (LocationPrefab8 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 8)
                LocationPrefab8.SetActive(true);
            if (LocationPrefab9 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 9)
                LocationPrefab9.SetActive(true);
            if (LocationPrefab10 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 10)
                LocationPrefab10.SetActive(true);
            if (LocationPrefab11 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 11)
                LocationPrefab11.SetActive(true);
            if (LocationPrefab12 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 12)
                LocationPrefab12.SetActive(true);
            if (LocationPrefab13 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 13)
                LocationPrefab13.SetActive(true);
            if (LocationPrefab14 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 14)
                LocationPrefab14.SetActive(true);
            if (LocationPrefab15 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 15)
                LocationPrefab15.SetActive(true);
            if (LocationPrefab16 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 16)
                LocationPrefab16.SetActive(true);
            if (LocationPrefab17 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 17)
                LocationPrefab17.SetActive(true);
            if (LocationPrefab18 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 18)
                LocationPrefab18.SetActive(true);
            if (LocationPrefab19 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 19)
                LocationPrefab19.SetActive(true);
            if (LocationPrefab20 != null && ShipList[i].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 20)
                LocationPrefab20.SetActive(true);
        }
    }
    public void SelectModeOffline()
    {
        FormationRangePrefab.SetActive(false);
        if (LocationPrefab1 != null)
            LocationPrefab1.SetActive(false);
        if (LocationPrefab2 != null)
            LocationPrefab2.SetActive(false);
        if (LocationPrefab3 != null)
            LocationPrefab3.SetActive(false);
        if (LocationPrefab4 != null)
            LocationPrefab4.SetActive(false);
        if (LocationPrefab5 != null)
            LocationPrefab5.SetActive(false);
        if (LocationPrefab6 != null)
            LocationPrefab6.SetActive(false);
        if (LocationPrefab7 != null)
            LocationPrefab7.SetActive(false);
        if (LocationPrefab8 != null)
            LocationPrefab8.SetActive(false);
        if (LocationPrefab9 != null)
            LocationPrefab9.SetActive(false);
        if (LocationPrefab10 != null)
            LocationPrefab10.SetActive(false);
        if (LocationPrefab11 != null)
            LocationPrefab11.SetActive(false);
        if (LocationPrefab12 != null)
            LocationPrefab12.SetActive(false);
        if (LocationPrefab13 != null)
            LocationPrefab13.SetActive(false);
        if (LocationPrefab14 != null)
            LocationPrefab14.SetActive(false);
        if (LocationPrefab15 != null)
            LocationPrefab15.SetActive(false);
        if (LocationPrefab16 != null)
            LocationPrefab16.SetActive(false);
        if (LocationPrefab17 != null)
            LocationPrefab17.SetActive(false);
        if (LocationPrefab18 != null)
            LocationPrefab18.SetActive(false);
        if (LocationPrefab19 != null)
            LocationPrefab19.SetActive(false);
        if (LocationPrefab20 != null)
            LocationPrefab20.SetActive(false);
    }

    //새로 생성되는 함선에다 편대에 소속시키기
    public void GetFormationForNewShip()
    {
        if (ShipList.Count <= FormationStorage)
        {
            int i = ShipList.Count - 1;

            if (i == 0)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location1;
            }
            else if (i == 1)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location2;
            }
            else if (i == 2)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location3;
            }
            else if (i == 3)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location4;
            }
            else if (i == 4)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location5;
            }
            else if (i == 5)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location6;
            }
            else if (i == 6)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location7;
            }
            else if (i == 7)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location8;
            }
            else if (i == 8)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location9;
            }
            else if (i == 9)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location10;
            }
            else if (i == 10)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location11;
            }
            else if (i == 11)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location12;
            }
            else if (i == 12)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location13;
            }
            else if (i == 13)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location14;
            }
            else if (i == 14)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location15;
            }
            else if (i == 15)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location16;
            }
            else if (i == 16)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location17;
            }
            else if (i == 17)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location18;
            }
            else if (i == 18)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location19;
            }
            else if (i == 19)
            {
                ShipList[i].GetComponent<MoveVelocity>().FormationIndex = Location20;
            }
            //BattleSave.Save1.CountOfFormationShip[0] += ShipAccount;
        }
    }

    //함선이 이전했을 때, 이전하기 직전에 소속되었던 편대 번호를 저장하고, 리스트에 올리기
    public void EmptyLocationGenerator(int number)
    {
        EmptyLocationList.Add(number);
    }

    //이전받은 함선이 순차적으로 먼저 빈 편대 번호에 배치하기
    public void EmptyLocationDistribution(int number)
    {
        if (EmptyLocationList[0] == 1)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location1;
        }
        else if (EmptyLocationList[0] == 2)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location2;
        }
        else if (EmptyLocationList[0] == 3)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location3;
        }
        else if (EmptyLocationList[0] == 4)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location4;
        }
        else if (EmptyLocationList[0] == 5)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location5;
        }
        else if (EmptyLocationList[0] == 6)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location6;
        }
        else if (EmptyLocationList[0] == 7)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location7;
        }
        else if (EmptyLocationList[0] == 8)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location8;
        }
        else if (EmptyLocationList[0] == 9)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location9;
        }
        else if (EmptyLocationList[0] == 10)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location10;
        }
        else if (EmptyLocationList[0] == 11)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location11;
        }
        else if (EmptyLocationList[0] == 12)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location12;
        }
        else if (EmptyLocationList[0] == 13)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location13;
        }
        else if (EmptyLocationList[0] == 14)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location14;
        }
        else if (EmptyLocationList[0] == 15)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location15;
        }
        else if (EmptyLocationList[0] == 16)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location16;
        }
        else if (EmptyLocationList[0] == 17)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location17;
        }
        else if (EmptyLocationList[0] == 18)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location18;
        }
        else if (EmptyLocationList[0] == 19)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location19;
        }
        else if (EmptyLocationList[0] == 20)
        {
            ShipList[number].GetComponent<MoveVelocity>().FormationIndex = Location20;
        }
    }
}