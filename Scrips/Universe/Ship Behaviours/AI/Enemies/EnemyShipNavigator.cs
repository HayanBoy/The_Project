using System.Collections;
using UnityEngine;

public class EnemyShipNavigator : MonoBehaviour
{
    private bool WarpTime; //�Ҽ��Լ� ���� ���� ����
    private GameObject FlagShip;
    public GameObject SelectMark;
    Coroutine turnOffUI;

    //�Դ뿡�� �̵� ��ǥ ����
    public void MoveTo(Vector3 TargetPosition)
    {
        GetComponent<EnemyShipBehavior>().SetVelocity(TargetPosition);
        GetComponent<EnemyShipBehavior>().WarpDriveReady = true;
        GetComponent<EnemyShipBehavior>().TargetShip = null;
    }

    //���� ���� �����׹� ��ġ ���� �� ���� ������ ����
    public void FlagshipWarpStart(bool boolean)
    {
        GetComponent<EnemyShipBehavior>().WarpSpeedUp(boolean);
    }

    //�Ҽ� �Լ� ���� �����׹� ��ġ ���� �� ���� ������ ����
    public void FollowShipWarpStart(bool boolean, float time)
    {
        WarpTime = boolean;
        Invoke("DelayWarp", time);
    }

    //FollowShipWarpStart���� ȣ��
    void DelayWarp()
    {
        GetComponent<EnemyShipBehavior>().WarpSpeedUp(WarpTime);
        WarpTime = false;
    }

    //�Ҽ� �Լ����� �����׹��� ���� ���� ���� ��������
    public void WarpLocationGet(Vector3 TargetPosition, Quaternion Roation, GameObject Ship)
    {
        FlagShip = Ship;
        EnemyWarpFormationControl EnemyWarpFormationControl = FlagShip.GetComponent<EnemyWarpFormationControl>();
        EnemyWarpFormationControl.FastWarpFormationTransfer(TargetPosition, Roation);
    }

    //������ ���, ���� UI Ȱ��ȭ
    public void TargetMark()
    {
        SelectMark.SetActive(false);

        if (turnOffUI != null)
            StopCoroutine(turnOffUI);
        turnOffUI = StartCoroutine(TurnOffUI());
    }
    IEnumerator TurnOffUI()
    {
        SelectMark.SetActive(true);
        yield return new WaitForSeconds(1);
        SelectMark.SetActive(false);
    }
}