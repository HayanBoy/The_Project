using UnityEngine;

public class EnemyWarpFormationControl : MonoBehaviour
{
    GameObject WarpLocation;
    public GameObject WarpFormation;

    private void Awake()
    {
        WarpLocation = Instantiate(WarpFormation, transform.position, transform.rotation);
    }

    //�����׹��� ������ �� ���� ��뿡�� ��ǥ ����
    public void FastWarpFormationTransfer(Vector3 Location, Quaternion Roation)
    {
        WarpLocation.GetComponent<EnemyWarpFormation>().FastWarpFormationGet(Location, Roation);
    }

    //�Ʊ� �Լ��� ���Ե� ������ ��� ��ǥ�� �ش� �Լ��� �߰�
    public void GetFormation(GameObject Flagship, int number)
    {
        GetComponent<EnemyFollowShipManager>().GetFormationForNewShip();
        WarpLocation.GetComponent<EnemyWarpFormation>().GetFormationForNewShip(Flagship, number);
    }
}