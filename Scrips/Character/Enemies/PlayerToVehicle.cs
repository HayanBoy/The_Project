using UnityEngine;

public class PlayerToVehicle : MonoBehaviour
{
    Player Player;

    public Transform PlayerPos;
    public Transform VehiclePos;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Player.VehicleActive == false)
            transform.position = PlayerPos.position;
        if (Player.VehicleActive == true)
            transform.position = VehiclePos.position;
    }
}