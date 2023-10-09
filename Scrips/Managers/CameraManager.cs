using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera PlayerCam;
    public CinemachineVirtualCamera RobotCam;

    public void PlayerCamOn()
    {
        PlayerCam.gameObject.SetActive(true);
        RobotCam.gameObject.SetActive(false);
    }

    public void RobotCamOn()
    {
        RobotCam.gameObject.SetActive(true);
        PlayerCam.gameObject.SetActive(false);
    }
}