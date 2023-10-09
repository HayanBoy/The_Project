using System.Collections;
using UnityEngine;

public class OurForceShipNavigator : MonoBehaviour
{
    private bool WarpTime; //소속함선 전용 워프 시작
    private GameObject FlagShip;

    //함대에게 이동 좌표 전송
    public void MoveTo(Vector3 TargetPosition)
    {
        GetComponent<OurForceShipBehavior>().SetVelocity(TargetPosition);
    }

    //기함 전용 워프항법 위치 전송 및 워프 가동을 시작
    public void FlagshipWarpStart(bool boolean)
    {
        GetComponent<OurForceShipBehavior>().WarpSpeedUp(boolean);
    }

    //소속 함선 전용 워프항법 위치 전송 및 워프 가동을 시작
    public void FollowShipWarpStart(bool boolean, float time)
    {
        GetComponent<OurForceShipBehavior>().WarpBoosterReady.SetActive(true);
        WarpTime = boolean;
        Invoke("DelayWarp", time);
    }

    //FollowShipWarpStart에서 호출
    void DelayWarp()
    {
        GetComponent<OurForceShipBehavior>().WarpSpeedUp(WarpTime);
        WarpTime = false;
    }

    //소속 함선들의 워프항법을 위한 도착 지점 가져오기
    public void WarpLocationGet(Vector3 TargetPosition, Quaternion Roation, GameObject Ship)
    {
        FlagShip = Ship;
        EnemyWarpFormationControl EnemyWarpFormationControl = FlagShip.GetComponent<EnemyWarpFormationControl>();
        EnemyWarpFormationControl.FastWarpFormationTransfer(TargetPosition, Roation);
    }
}