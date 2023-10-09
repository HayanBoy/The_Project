using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StageDefence : MonoBehaviour
{
    [Header("스크립트")]
    public Stage1_9 Stage1_9;
    public SpawnSiteGenerator SpawnSiteGenerator;

    [Header("카메라 콜라이더")]
    public Transform SpawnMap;
    public PolygonCollider2D CamSpawnMap;
    public GameObject NoBackColliderPrefab; //진격전 전용 1번 콜라이더 프리팹은 방어전에서 사용되면 안되므로 방어전에서는 끈다.
    public GameObject SpawnPrefab; //진격전 스폰 프리팹을 끈다.

    [Header("웨이브 정보")]
    public bool WaveStepStart = false; //웨이브가 시작
    private bool Wave = false;
    private int WaveNumber; //웨이브 번호

    [Header("웨이브 메시지")]
    public GameObject WaveMessage;
    public Text WaveTalk1; //웨이브 메시지
    public Text WaveTalk2;

    private void Start()
    {
        NoBackColliderPrefab.SetActive(false);
        SpawnPrefab.SetActive(false);
        SpawnSiteGenerator.Follow = SpawnMap;
        Stage1_9.Cam.m_BoundingShape2D = CamSpawnMap;
        StartCoroutine(StartFirstWave());
    }

    //첫 웨이브 시작
    IEnumerator StartFirstWave()
    {
        yield return new WaitForSeconds(11);
        WaveMessage.SetActive(true);
        if (BattleSave.Save1.LanguageType == 1)
        {
            WaveTalk1.text = string.Format("Wave" + (WaveNumber + 1) + " start");
            WaveTalk2.text = string.Format("Wave" + (WaveNumber + 1) + " start");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            WaveTalk1.text = string.Format("Wave" + (WaveNumber + 1) + " 시작");
            WaveTalk2.text = string.Format("Wave" + (WaveNumber + 1) + " 시작");
        }
        yield return new WaitForSeconds(4);
        WaveTalk1.text = string.Format("");
        WaveTalk2.text = string.Format("");
        WaveMessage.SetActive(false);
        WaveStart();
    }

    //웨이브 끝날 때마다 클리어 대사를 출력시키고, 일정 시간 뒤에 다음 웨이브 시작
    public IEnumerator CompleteKillAll()
    {
        Wave = false;
        WaveStepStart = false;
        Debug.Log("웨이브 클리어");

        yield return new WaitForSeconds(3);
        WaveMessage.SetActive(true);
        if (BattleSave.Save1.LanguageType == 1)
        {
            WaveTalk1.text = string.Format("Wave" + (WaveNumber + 1) + " start");
            WaveTalk2.text = string.Format("Wave" + (WaveNumber + 1) + " start");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            WaveTalk1.text = string.Format("Wave" + (WaveNumber + 1) + " 시작");
            WaveTalk2.text = string.Format("Wave" + (WaveNumber + 1) + " 시작");
        }
        yield return new WaitForSeconds(4);
        WaveTalk1.text = string.Format("");
        WaveTalk2.text = string.Format("");
        WaveMessage.SetActive(false);
        WaveStart();
    }

    //방어전 웨이브 시작
    void WaveStart()
    {
        WaveNumber++;
        Stage1_9.KillStepOne = true;
        WaveStepStart = true;

        int SpawnCycle = 1;
        if (BattleSave.Save1.MissionLevel < 2)
            SpawnCycle = Random.Range(1, 2);
        else if (BattleSave.Save1.MissionLevel >= 2)
            SpawnCycle = Random.Range(2, 4);

        if (WaveNumber == 1)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 1, false));
        else if (WaveNumber == 2)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 2, false));
        else if (WaveNumber == 3)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 3, true));
        else if (WaveNumber == 4)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 2, true));
        else if (WaveNumber == 5)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 2, false));
        else if (WaveNumber == 6)
            StartCoroutine(Stage1_9.SpawnStep(SpawnCycle, WaveNumber, 3, true));
    }
}