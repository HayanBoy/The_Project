using System.Collections;
using UnityEngine;

public class OurForceShipNavigator : MonoBehaviour
{
    private bool WarpTime; //�Ҽ��Լ� ���� ���� ����
    private GameObject FlagShip;

    //�Դ뿡�� �̵� ��ǥ ����
    public void MoveTo(Vector3 TargetPosition)
    {
        GetComponent<OurForceShipBehavior>().SetVelocity(TargetPosition);
    }

    //���� ���� �����׹� ��ġ ���� �� ���� ������ ����
    public void FlagshipWarpStart(bool boolean)
    {
        GetComponent<OurForceShipBehavior>().WarpSpeedUp(boolean);
    }

    //�Ҽ� �Լ� ���� �����׹� ��ġ ���� �� ���� ������ ����
    public void FollowShipWarpStart(bool boolean, float time)
    {
        GetComponent<OurForceShipBehavior>().WarpBoosterReady.SetActive(true);
        WarpTime = boolean;
        Invoke("DelayWarp", time);
    }

    //FollowShipWarpStart���� ȣ��
    void DelayWarp()
    {
        GetComponent<OurForceShipBehavior>().WarpSpeedUp(WarpTime);
        WarpTime = false;
    }

    //�Ҽ� �Լ����� �����׹��� ���� ���� ���� ��������
    public void WarpLocationGet(Vector3 TargetPosition, Quaternion Roation, GameObject Ship)
    {
        FlagShip = Ship;
        EnemyWarpFormationControl EnemyWarpFormationControl = FlagShip.GetComponent<EnemyWarpFormationControl>();
        EnemyWarpFormationControl.FastWarpFormationTransfer(TargetPosition, Roation);
    }
}