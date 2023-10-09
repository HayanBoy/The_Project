using System.Collections;
using UnityEngine;

public class SpawnSiteGenerator : MonoBehaviour
{
    EnemiesSpawnManager objectManager;

    public Transform Follow; //스폰하기 직전에 플레이어 위치로 스폰영역을 이동시킨다.
    public Transform RightSpawnArea;
    public Transform LeftSpawnArea;
    GameObject Enemy;

    public float UpMaxArea;
    public float DownMaxArea;
    public int SpawnCount; //명령받은 갯수만큼 스폰
    public int StartEnemyType; //RandomSpawnSide의 최소 숫자로 시작되는 적 유형. 스폰시 어디 범위 적을 출현시킬지 숫자로 미리 명령을 받는다.
    public int EndEnemyType; //RandomSpawnSide의 최대 숫자로 시작되는 적 유형. 스폰시 어디 범위 적을 출현시킬지 숫자로 미리 명령을 받는다.
    private int RandomSpawnSide; //오른쪽과 왼쪽 중에서 랜덤으로 출현
    private int RandomSpawnEnemy; //랜덤 적 스폰
    private float SpawnTime; //스폰 간격 시간

    private string EnemyName; //스폰될 적 이름

    void Start()
    {
        objectManager = FindObjectOfType<EnemiesSpawnManager>();
    }

    //오른쪽 랜덤 좌표
    void RightSpawnSite()
    {
        float posY = Random.Range(DownMaxArea, UpMaxArea);

        Transform RightSpawn = RightSpawnArea.transform;
        RightSpawn.position = new Vector3(RightSpawn.position.x, posY, 0);

        RandomSpawnEnemy = Random.Range(StartEnemyType, EndEnemyType);
        SelectionEnemy(); //적 선택
        Enemy = objectManager.EnemiesLoader(EnemyName);
        Enemy.transform.position = RightSpawn.position;
        Enemy.transform.rotation = RightSpawn.rotation;

        if (RandomSpawnEnemy == 101) //블레이드 하이더(타이트로키 근접)
        {
            Enemy.GetComponent<StartTypeTaitroki>().enabled = true;
            Enemy.GetComponent<StartTypeTaitroki>().Type = 1;
        }

        ScoreManager.instance.EnemyList.Add(Enemy);
    }

    //왼쪽 랜덤 좌표
    void LeftSpawnSite()
    {
        float posY = Random.Range(DownMaxArea, UpMaxArea);

        Transform LeftSpawn = LeftSpawnArea.transform;
        LeftSpawn.position = new Vector3(LeftSpawn.position.x, posY, 0);

        RandomSpawnEnemy = Random.Range(StartEnemyType, EndEnemyType);
        SelectionEnemy(); //적 선택
        Enemy = objectManager.EnemiesLoader(EnemyName);
        Enemy.transform.position = LeftSpawn.position;
        Enemy.transform.rotation = LeftSpawn.rotation;

        if (RandomSpawnEnemy == 101) //블레이드 하이더(타이트로키 근접)
        {
            Enemy.GetComponent<StartTypeTaitroki>().enabled = true;
            Enemy.GetComponent<StartTypeTaitroki>().Type = 1;
        }

        ScoreManager.instance.EnemyList.Add(Enemy);
    }

    public void StartSpawn()
    {
        transform.position = new Vector2(Follow.position.x, Follow.position.y);
        StartCoroutine(SelectSide());
    }

    IEnumerator SelectSide()
    {
        for (int i = 1; i <= SpawnCount; i++)
        {
            RandomSpawnSide = Random.Range(0, 4);

            if (RandomSpawnSide == 0)
            {
                LeftSpawnSite();
                SpawnTime = Random.Range(1, 2);
                yield return new WaitForSeconds(SpawnTime);
            }
            else
            {
                RightSpawnSite();
                SpawnTime = Random.Range(1, 2);
                yield return new WaitForSeconds(SpawnTime);
            }
        }
    }

    //적 선택
    void SelectionEnemy()
    {
        if (RandomSpawnEnemy == 0) //카오티-자이오스4(일반)
            EnemyName = "Kaotijaios4";
        else if (RandomSpawnEnemy == 1) //카오티-자이오스4(근접 블레이드)
            EnemyName = "Kaotijaios4Spear";
        else if (RandomSpawnEnemy == 2) //카오티-자이오스4(강화건)
            EnemyName = "Kaotijaios4Fleet1389";
        else if (RandomSpawnEnemy == 3) //타이카-라이-쓰로트로 1(지원 저격수)
            EnemyName = "TaikaLaiThrotro1";
        else if (RandomSpawnEnemy == 4) //카오티-자이오스4(듀얼건)
            EnemyName = "Kaotijaios4Dualgun";
        else if (RandomSpawnEnemy == 5) //카오티-자이오스4 엘리트1(방패형)
            EnemyName = "Kaotijaios4Armor";
        else if (RandomSpawnEnemy == 6) //카오티-자이오스4 엘리트2(방패 + 강화 듀얼건)
            EnemyName = "Kaotijaios4ArmorDualgun";
        else if (RandomSpawnEnemy == 7) //타이카-라이-쓰로트로 1(플라즈마 대전차)
            EnemyName = "TaikaLaiThrotro1Plasma";
        else if (RandomSpawnEnemy == 100) //좀비
            EnemyName = "ZomBie";
        else if (RandomSpawnEnemy == 101) //블레이드 하이더(타이트로키 근접)
            EnemyName = "ZomBie";
        else if (RandomSpawnEnemy == 200) //아트로-크로스파 390(유도 미사일)
            EnemyName = "AtroCrossfa390";
        else if (RandomSpawnEnemy == 201) //아스오 시이오셰어(엘리트 돌격병)
            EnemyName = "AsoShiioshare";
    }
}