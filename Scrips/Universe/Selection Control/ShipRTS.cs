using System.Collections;
using UnityEngine;

public class ShipRTS : MonoBehaviour
{
    private IMoveVelocity MoveVelocity;
    public GameObject SeletedUI;
    private Vector3 WarpPoint;
    public int ShipNumber; //선택된 함선번호. 1 = 기함, 2 = 편대함, 3 = 방패함, 4 = 우주모함 
    public int ShipListNumber; //리스트에 올라온 배치 번호
    private bool WarpTime; //소속함선 전용 워프 시작
    public float WarpStartTime; //워프 하기 위한 준비 시간

    private void Awake()
    {
        //UIControlSystem = FindObjectOfType<UIControlSystem>();
        MoveVelocity = GetComponent<IMoveVelocity>();
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible)
    {
        SeletedUI.SetActive(visible);
    }

    //함대에게 이동 좌표 전송
    public void MoveTo(Vector3 TargetPosition)
    {
        MoveVelocity.SetVelocity(TargetPosition);
    }

    //워프 부스터 가동
    public void WarpBoosterReady()
    {
        GetComponent<MoveVelocity>().WarpBoosterReady.SetActive(true);
        GetComponent<MoveVelocity>().WarpDriveReady = true;
    }

    //기함 전용 워프항법 위치 전송 및 워프 가동을 시작
    public void FlagshipWarpStart(bool boolean)
    {
        MoveVelocity.WarpSpeedUp(boolean);
    }

    //소속 함선 전용 워프항법 위치 전송 및 워프 가동을 시작
    public void FollowShipWarpStart(bool boolean)
    {
        WarpTime = boolean;
        MoveVelocity.WarpSpeedUp(WarpTime);
        WarpTime = false;
    }

    //소속 함선들의 워프항법을 위한 도착 지점 전송
    public void WarpLocationGet(Vector3 TargetPosition, Quaternion Roation, GameObject Flagship)
    {
        Flagship.GetComponent<FollowShipManager>().WarpFormation.GetComponent<WarpFormation>().FastWarpFormationGet(TargetPosition, Roation);
    }

    //워프항법 시작(기함에서 각 함선들에게 워프를 명령하는 방식)
    public void FastWarpLoactionGet(Vector3 Location)
    {
        if (GetComponent<MoveVelocity>().FlagShip == true)
        {
            WarpPoint = Location;
            StartCoroutine(FlagShipStartWarp(3));
            StartCoroutine(FollowShipStartWarp(3.5f));
        }
    }

    //기함 워프
    public IEnumerator FlagShipStartWarp(float Time)
    {
        WarpStartTime = Time;
        MoveTo(WarpPoint);
        WarpBoosterReady();

        yield return new WaitForSeconds(Time);
        WarpLocationGet(WarpPoint, transform.rotation, gameObject);
    }

    //소속 함선 워프
    public IEnumerator FollowShipStartWarp(float Time)
    {
        WarpStartTime = Time;
        GameObject Flagship = this.gameObject;
        if (GetComponent<FollowShipManager>().ShipList.Count > 0)
        {
            for (int i = 0; i < Flagship.GetComponent<FollowShipManager>().ShipList.Count; i++)
            {
                ShipRTS shipRTS = Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>();
                shipRTS.WarpBoosterReady();
            }
        }

        yield return new WaitForSeconds(WarpStartTime + 0.5f);

        if (GetComponent<FollowShipManager>().ShipList.Count > 0)
        {
            for (int i = 0; i < Flagship.GetComponent<FollowShipManager>().ShipList.Count; i++)
            {
                ShipRTS shipRTS = Flagship.GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>();
                yield return new WaitForSeconds(0.1f);
                shipRTS.FollowShipWarpStart(true);

                if (i >= Flagship.GetComponent<FollowShipManager>().ShipList.Count - 1) //가장 마지막에 기함워프 실시
                {
                    yield return new WaitForSeconds(0.3f);
                    //UIControlSystem.WarpStart();
                    ShipRTS shipRTS2 = Flagship.GetComponent<ShipRTS>();
                    shipRTS2.FlagshipWarpStart(true);
                }
            }
        }
        else
        {
            //UIControlSystem.WarpStart();
            ShipRTS shipRTS2 = Flagship.GetComponent<ShipRTS>();
            shipRTS2.FlagshipWarpStart(true);
        }
    }

    //소속 함선들에게 워프항법이 완료가 되었음을 알림
    public void WarpComplete()
    {
        for (int i = 0; i < ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
            ShipRTS shipRTS = ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipRTS>();
            shipRTS.MoveVelocity.WarpCompleteBoolean();
        }
    }
}