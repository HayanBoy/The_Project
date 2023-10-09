using UnityEngine;

public class WarpFormationControl : MonoBehaviour
{
    GameObject WarpLocation;
    public GameObject WarpFormation;

    private void Awake()
    {
        WarpLocation = Instantiate(WarpFormation, transform.position, transform.rotation);
        GetComponent<FollowShipManager>().WarpFormation = WarpLocation;
    }

    //�Ʊ� �Լ��� ���Ե� ������ ��� ��ǥ�� �ش� �Լ��� �߰�
    public void GetFormation(GameObject Flagship, int number)
    {
        if (GetComponent<FollowShipManager>().EmptyLocationList.Count == 0)
        {
            GetComponent<FollowShipManager>().GetFormationForNewShip();
            WarpLocation.GetComponent<WarpFormation>().GetFormationForNewShip(Flagship, number);
        }
        else
        {
            GetComponent<FollowShipManager>().EmptyLocationDistribution(number);
            WarpLocation.GetComponent<WarpFormation>().EmptyLocationDistribution(Flagship, number);
            Flagship.GetComponent<FollowShipManager>().EmptyLocationList.Remove(Flagship.GetComponent<FollowShipManager>().EmptyLocationList[0]);
        }
    }
}