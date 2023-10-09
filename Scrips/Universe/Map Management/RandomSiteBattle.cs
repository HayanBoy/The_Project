using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSiteBattle : MonoBehaviour
{
    [Header("스크립트")]
    public UniverseMapSystem UniverseMapSystem;
    public LiveCommunicationSystem LiveCommunicationSystem;
    DataSaveManager DataSaveManager;

    [Header("전투 지역 정보")]
    public bool FirstSpawn; //게임 시작 후, 처음 항성계를 방문했을 때에만 랜덤으로 스폰 배치
    public bool isInFight; //전투가 시작되면 스위치가 켜진다.
    public int BattleSiteNumber;
    public GameObject BattleSite;
    public Transform MyStar;
    public int MySystem;
    [SerializeField] float SpawnRange; //스폰 범위
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public bool EnemySpawn = false; //적이 스폰되었는지 여부

    [Header("행성 지역 정보")]
    public int PlanetNumber;

    [Header("타이머")]
    private float Timer; //전투 승리 후, 경과 시간 층적
    public float BattleTimer; //전투시작 시, 경과 시간 측정
    public float ReSpawnTime; //전투 승리 후, 다시 사이트가 리스폰까지 걸리는 시간
    public bool FlagshipHere; //기함이 해당 전투 사이트에 있음을 표시
    public float SpawnTime;

    [Header("함대 리스트")]
    public List<GameObject> BattleEnemyShipList = new List<GameObject>();
    public List<GameObject> NarihaOurForceShipList = new List<GameObject>(); //나리하 아군 편대함 목록

    public void Awake()
    {
        if (BattleSave.Save1 != null)
        {
            if (BattleSave.Save1.GroundBattleCount > 0)
            {
                for (int i = 0; i < BattleSave.Save1.RandomSiteNumber.Count; i++)
                {
                    if (BattleSave.Save1.RandomSiteNumber[i] == this.BattleSiteNumber)
                    {
                        transform.position = BattleSave.Save1.RandomSiteTransform[i];
                    }
                }
            }
        }
        DataSaveManager = FindObjectOfType<DataSaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FirstSpawn == true && SpawnTime <= 5 && SpawnTime > 0)
        {
            if (BattleEnemyShipList.Count == 0 && NarihaOurForceShipList.Count == 0)
            {
                if (collision.gameObject.layer == 10 || collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //천체 및 아군 기함과 멀리 떨어져서 스폰되도록 조취
                {
                    FirstSpawn = false;
                    //Debug.Log("재탐색");
                    RandomAreaSpawnStart();
                }
                else if (SpawnTime == 0 && collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 워프없이 접근시, 전투가 시작
                {
                    //Debug.Log("교전 시작");
                    SpawnTime = 10;
                    UniverseMapSystem.BattleSiteEnemyGet(BattleSiteNumber);
                }
            }
        }
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 해당 사이트에 있으면 활성화
        {
            FlagshipHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 전투 도중 이탈하면 전투 시간이 돌아간다.
        {
            FlagshipHere = false;
        }
    }

    //랜덤 배치 시작
    public void RandomAreaSpawnStart()
    {
        if (FirstSpawn == false)
        {
            transform.position = new Vector3(Random.Range(MyStar.position.x - SpawnRange, MyStar.position.x + SpawnRange),
                Random.Range(MyStar.position.y - SpawnRange, MyStar.position.y + SpawnRange), transform.position.z);
            FirstSpawn = true;
            SpawnTime = 5;
        }
    }

    //전투가 종료되면 아군 함선들이 다른 쪽으로 워프하며 사라지기
    IEnumerator OurShipsGoOut()
    {
        int Amount = NarihaOurForceShipList.Count;
        for (int i = 0; i < Amount; i++)
        {
            float EnemyshipRange = Random.Range(1000, 1000);

            Vector3 LocationPoint = new Vector3(Random.Range(transform.position.x - EnemyshipRange, transform.position.x + EnemyshipRange),
                Random.Range(transform.position.y - EnemyshipRange, transform.position.y + EnemyshipRange), transform.position.z);
            NarihaOurForceShipList[i].GetComponent<OurForceShipBehavior>().TargetShip = null;
            NarihaOurForceShipList[i].GetComponent<OurForceShipNavigator>().MoveTo(LocationPoint);
            yield return new WaitForSeconds(0.1f);
            NarihaOurForceShipList[i].GetComponent<OurForceShipNavigator>().FollowShipWarpStart(true, 5);
        }

        yield return new WaitForSeconds(7);

        for (int i = 0; i < Amount; i++)
        {
            Destroy(NarihaOurForceShipList[0], 0.1f);
            NarihaOurForceShipList.Remove(NarihaOurForceShipList[0]);
        }
    }

    private void Update()
    {
        if (isInFight == true && EnemySpawn == true && BattleEnemyShipList.Count == 0) //전투 종료 후, 스폰 지역 삭제
        {
            isInFight = false;
            EnemySpawn = false;
            BattleSite.SetActive(false);
            SpawnTime = 0;
            StartCoroutine(OurShipsGoOut());
            StartCoroutine(LiveCommunicationSystem.SubCommunication(2.00f));
            //Debug.Log("전투 승리");
        }
        else if (isInFight == true && FlagshipHere == false && BattleEnemyShipList.Count > 0) //전투 시작 시, 전투 타이머가 발동되며 전투 시간이 지나면, 자동으로 해당 사이트의 모든 것이 삭제되며, 리스폰
        {
            if (BattleTimer <= ReSpawnTime)
            {
                BattleTimer += Time.deltaTime;
            }
            else
            {
                BattleTimer = 0;
                isInFight = false;
                FirstSpawn = false;
                EnemySpawn = false;

                int Amount = BattleEnemyShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(BattleEnemyShipList[0], 0.1f);
                    BattleEnemyShipList.Remove(BattleEnemyShipList[0]);
                }
                Amount = NarihaOurForceShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(NarihaOurForceShipList[0], 0.1f);
                    NarihaOurForceShipList.Remove(NarihaOurForceShipList[0]);
                }

                GetComponent<EnemyGet>().enabled = false;
                GetComponent<EnemyGet>().FlagshipWarp = false;
                GetComponent<EnemyGet>().WarpControsType = 0;
                GetComponent<OurForceGet>().enabled = false;
                RandomAreaSpawnStart();
                //Debug.Log("전투 이탈, 재 스폰됨");
            }
        }
        else if (FirstSpawn == true && isInFight == false && BattleEnemyShipList.Count == 0)
        {
            if (Timer <= ReSpawnTime)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                Timer = 0;
                GetComponent<EnemyGet>().enabled = false;
                GetComponent<EnemyGet>().FlagshipWarp = false;
                GetComponent<EnemyGet>().WarpControsType = 0;
                GetComponent<OurForceGet>().enabled = false;
                BattleSite.SetActive(true);
                FirstSpawn = false;
                RandomAreaSpawnStart();
                //Debug.Log("전투 승리, 재 스폰됨");
            }
        }

        if (SpawnTime <= 5 && SpawnTime > 0)
        {
            SpawnTime -= Time.deltaTime;
        }
        else if (SpawnTime < 0)
        {
            SpawnTime = 0;
        }
    }
}