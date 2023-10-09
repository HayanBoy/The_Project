using Cinemachine;
using UnityEngine.UI;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("스크립트")]
    public UniverseMapSystem UniverseMapSystem;
    public WordPrintSystem WordPrintSystem;
    public CameraZoom CameraZoom;
    public Shake shake;

    [Header("카메라")]
    public CinemachineVirtualCamera CVCamera;

    [Header("워프 카메라")]
    public RectTransform WarpMonitor;
    public GameObject WarpMonitorPrefab;
    public GameObject WarpSpace;
    float timeTaken; //워프 소요시간 계산
    public float AmountOfCameraShake; //카메라 흔들기 강도
    float WarpTemp;
    float WarpTemp2;

    [Header("워프 중의 상태에 따른 오브젝트 변화")]
    public GameObject WarpLiveLogs; //워프 실시간 로그, 워프 엔진 가동 로그, 남은 거리 표시기

    [Header("경고 메시지")]
    public Text WarningText1;
    public Text WarningText2;

    public bool MoveEnabled = true; //워프 도중에 기함이 터치로 이동하지 못하도록 처리

    public void WarpStartAnime(bool boolean)
    {
        WarpMonitorPrefab.GetComponent<Animator>().SetBool("Warp start, Warp Effect", boolean);
    }

    void JourneyTime(float Speed, Vector3 Player)
    {
        float Distance = Vector3.Distance(Player, ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().DestinationArea);
        timeTaken = Distance / Speed;
        WordPrintSystem.WarpDistanceShow(Distance, ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().SystemDestinationNumber, ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().SystemNowNumber);
    }

    private void Update()
    {
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<HullSloriusFlagship1>().hitPoints <= ShipManager.instance.SelectedFlagShip[0].GetComponent<HullSloriusFlagship1>().startingHitPoints * 0.35f)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                WarningText1.text = string.Format("Warning, serious damage of this flagship has detected.");
                WarningText2.text = string.Format("Warning, serious damage of this flagship has detected.");
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                WarningText1.text = string.Format("경고, 이 기함의 피해가 심각합니다.");
                WarningText2.text = string.Format("경고, 이 기함의 피해가 심각합니다.");
            }
        }
        else
        {
            WarningText1.text = string.Format("");
            WarningText2.text = string.Format("");
        }
        if (ShipManager.instance.SelectedFlagShip.Count != 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            //선택된 기함이 워프 준비 상태에 속할 경우
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == true)
            {
                MoveEnabled = false;
                WarpLiveLogs.gameObject.SetActive(true);
                WarpMonitorPrefab.SetActive(true);
                if (UniverseMapSystem.UniverseMapMode == false && UniverseMapSystem.UniverseMapCompleteOff == true) //카메라 조작 버튼 사용중지
                    CameraZoom.CameraInitialize();
            }

            //선택된 기함이 워프 중이며 워프 정지 전에 속하지 않을 경우
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            {
                //워프 공간 이펙트 및 화면 흔들기
                Quaternion rotation = ShipManager.instance.SelectedFlagShip[0].transform.rotation;
                Vector3 euler = rotation.eulerAngles;
                WarpMonitor.eulerAngles = new Vector3(0, 0, euler.z);
                Shake.Instance.ShakeCamera(AmountOfCameraShake, 0.1f);
                MoveEnabled = false;
                WarpSpace.SetActive(true);
                WarpLiveLogs.gameObject.SetActive(true);
                WarpMonitorPrefab.SetActive(true);
                if (UniverseMapSystem.UniverseMapMode == false && UniverseMapSystem.UniverseMapCompleteOff == true) //카메라 조작 버튼 사용중지
                    CameraZoom.CameraInitialize();
            }
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == true)
            {
                MoveEnabled = false;
                WarpLiveLogs.gameObject.SetActive(true);
                WarpSpace.SetActive(false);
                WarpMonitorPrefab.SetActive(true);
            }

            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false
                && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false)
            {
                MoveEnabled = true;
                WarpLiveLogs.gameObject.SetActive(false);
                WarpMonitorPrefab.SetActive(false);
                if (WarpTemp2 == 0)
                {
                    WarpTemp2 += Time.deltaTime;
                    CameraZoom.CameraImage.raycastTarget = true;
                }
            }

            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true || ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == true
                || ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == true)
            {
                WarpTemp2 = 0;
                CameraZoom.CameraImage.raycastTarget = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (ShipManager.instance.SelectedFlagShip.Count != 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            //선택된 기함이 워프 중, 워프 정지 준비 상태에 있을 경우
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true || ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == true)
            {
                JourneyTime(ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpSpeed, ShipManager.instance.SelectedFlagShip[0].transform.position); //워프 항해 소요 시간 계산
                WordPrintSystem.PlayerWarpArriveTime(timeTaken);
                WarpTemp = 0;
                MoveEnabled = false;
            }
            else
            {
                if (WarpTemp == 0)
                {
                    WarpTemp += Time.deltaTime;
                    WordPrintSystem.PlayerWarpArriveTime(0);
                    MoveEnabled = true;
                    CameraZoom.CameraImage.raycastTarget = true;
                }
            }
        }
    }
}