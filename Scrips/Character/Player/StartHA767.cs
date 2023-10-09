using System.Collections;
using UnityEngine;

public class StartHA767 : MonoBehaviour
{
    [Header("스크립트")]
    GameControlSystem gameControlSystem;
    TutorialSystem TutorialSystem;
    public BombSettings BombSettings;

    [Header("탑승 수단")]
    public GameObject HA767;
    public GameObject Pod;

    [Header("플레이어")]
    public GameObject Player;
    public Transform PayerPos;

    [Header("기함 침투전 전용")]
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject BombLocation;
    public GameObject FlagshipBattlePrefab;
    public GameObject TowerPrefab; //기함 무력화 대상 구조물

    [Header("임무 유형")]
    public int StartType;
    public GameObject DefencePrefab;

    void Start()
    {
        ScoreManager.instance.AllCnt = 0;
        ScoreManager.instance.EnemyList.Clear();

        gameControlSystem = FindObjectOfType<GameControlSystem>();
        gameControlSystem.StartUI();
        if (BattleSave.Save1.MissionType == 2 || BattleSave.Save1.MissionType == 4) //방어전일 경우, 방어전 프리팹을 활성화
            DefencePrefab.SetActive(true);
        else if (BattleSave.Save1.MissionType == 100 || BattleSave.Save1.MissionType == 101) //기함 침투전의 경우, 해당 기능을 활성화
        {
            FlagshipBattlePrefab.SetActive(true);
            TowerPrefab.SetActive(true);
            int FinishLine = Random.Range(3, 5);
            BattleSave.Save1.FinishSpawnNumber = FinishLine;

            if (FinishLine == 3)
            {
                BombLocation.transform.position = new Vector2(Spawn1.transform.position.x, BombLocation.transform.position.y);
                TowerPrefab.transform.position = new Vector2(Spawn1.transform.position.x, TowerPrefab.transform.position.y);
            }
            else if (FinishLine == 4)
            {
                BombLocation.transform.position = new Vector2(Spawn2.transform.position.x, BombLocation.transform.position.y);
                TowerPrefab.transform.position = new Vector2(Spawn2.transform.position.x, TowerPrefab.transform.position.y);
            }
            BombSettings.BombPosition.transform.position = new Vector2(BombLocation.transform.position.x, BombSettings.BombPosition.transform.position.y);
            BombLocation.SetActive(true);
        }

        if (StartType == 0)
        {
            StartCoroutine(StartSceneShuttle());
        }
    }

    //게임 시작(수송기)
    IEnumerator StartSceneShuttle()
    {
        HA767.gameObject.SetActive(true);
        Player.GetComponent<Animator>().SetBool("Player Turn off", true);
        Player.transform.position = PayerPos.transform.position;
        HA767.GetComponent<Animator>().SetBool("Landing, HA-767", true);
        yield return new WaitForSeconds(3.5f);
        Player.GetComponent<Animator>().SetBool("Player landing", true);
        Player.GetComponent<Animator>().SetBool("Player Turn off", false);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(gameControlSystem.StartButten());
        StartCoroutine(gameControlSystem.StartController());
        Player.GetComponent<Animator>().SetBool("Player Turn off", false);
        Player.GetComponent<Animator>().SetBool("Player landing", false);
        Player.GetComponent<Animator>().SetBool("Swap(DT-37 output)", true);
        yield return new WaitForSeconds(0.33f);
        Player.GetComponent<Animator>().SetBool("Swap(DT-37 output)", false);
        yield return new WaitForSeconds(5);
        HA767.GetComponent<Animator>().SetBool("Landing, HA-767", false);
        HA767.gameObject.SetActive(false);
    }
}