using UnityEngine;

public class XAxisFollow : MonoBehaviour
{
    public Transform PlayerPos;
    public Transform VehiclePos;
    int Follow = 1;

    public void FollowPlayer()
    {
        Follow = 1;
    }
    public void FollowVehicle()
    {
        Follow = 2;
    }

    void Update()
    {
        if (Follow == 1)
        {
            transform.position = new Vector3(PlayerPos.position.x, transform.position.y, transform.position.z);
        }
        else if (Follow == 2)
        {
            transform.position = new Vector3(VehiclePos.position.x, transform.position.y, transform.position.z);
        }
    }
}
