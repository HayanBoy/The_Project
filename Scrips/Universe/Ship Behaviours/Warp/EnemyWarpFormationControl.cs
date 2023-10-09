using UnityEngine;

public class EnemyWarpFormationControl : MonoBehaviour
{
    GameObject WarpLocation;
    public GameObject WarpFormation;

    private void Awake()
    {
        WarpLocation = Instantiate(WarpFormation, transform.position, transform.rotation);
    }

    //워프항법을 시작할 때 워프 편대에다 좌표 전송
    public void FastWarpFormationTransfer(Vector3 Location, Quaternion Roation)
    {
        WarpLocation.GetComponent<EnemyWarpFormation>().FastWarpFormationGet(Location, Roation);
    }

    //아군 함선이 편입될 때마다 편대 좌표에 해당 함선을 추가
    public void GetFormation(GameObject Flagship, int number)
    {
        GetComponent<EnemyFollowShipManager>().GetFormationForNewShip();
        WarpLocation.GetComponent<EnemyWarpFormation>().GetFormationForNewShip(Flagship, number);
    }
}