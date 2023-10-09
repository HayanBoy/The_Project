using System.Collections;
using UnityEngine;

public class SpawnSiteGenerator : MonoBehaviour
{
    EnemiesSpawnManager objectManager;

    public Transform Follow; //�����ϱ� ������ �÷��̾� ��ġ�� ���������� �̵���Ų��.
    public Transform RightSpawnArea;
    public Transform LeftSpawnArea;
    GameObject Enemy;

    public float UpMaxArea;
    public float DownMaxArea;
    public int SpawnCount; //��ɹ��� ������ŭ ����
    public int StartEnemyType; //RandomSpawnSide�� �ּ� ���ڷ� ���۵Ǵ� �� ����. ������ ��� ���� ���� ������ų�� ���ڷ� �̸� ����� �޴´�.
    public int EndEnemyType; //RandomSpawnSide�� �ִ� ���ڷ� ���۵Ǵ� �� ����. ������ ��� ���� ���� ������ų�� ���ڷ� �̸� ����� �޴´�.
    private int RandomSpawnSide; //�����ʰ� ���� �߿��� �������� ����
    private int RandomSpawnEnemy; //���� �� ����
    private float SpawnTime; //���� ���� �ð�

    private string EnemyName; //������ �� �̸�

    void Start()
    {
        objectManager = FindObjectOfType<EnemiesSpawnManager>();
    }

    //������ ���� ��ǥ
    void RightSpawnSite()
    {
        float posY = Random.Range(DownMaxArea, UpMaxArea);

        Transform RightSpawn = RightSpawnArea.transform;
        RightSpawn.position = new Vector3(RightSpawn.position.x, posY, 0);

        RandomSpawnEnemy = Random.Range(StartEnemyType, EndEnemyType);
        SelectionEnemy(); //�� ����
        Enemy = objectManager.EnemiesLoader(EnemyName);
        Enemy.transform.position = RightSpawn.position;
        Enemy.transform.rotation = RightSpawn.rotation;

        if (RandomSpawnEnemy == 101) //���̵� ���̴�(Ÿ��Ʈ��Ű ����)
        {
            Enemy.GetComponent<StartTypeTaitroki>().enabled = true;
            Enemy.GetComponent<StartTypeTaitroki>().Type = 1;
        }

        ScoreManager.instance.EnemyList.Add(Enemy);
    }

    //���� ���� ��ǥ
    void LeftSpawnSite()
    {
        float posY = Random.Range(DownMaxArea, UpMaxArea);

        Transform LeftSpawn = LeftSpawnArea.transform;
        LeftSpawn.position = new Vector3(LeftSpawn.position.x, posY, 0);

        RandomSpawnEnemy = Random.Range(StartEnemyType, EndEnemyType);
        SelectionEnemy(); //�� ����
        Enemy = objectManager.EnemiesLoader(EnemyName);
        Enemy.transform.position = LeftSpawn.position;
        Enemy.transform.rotation = LeftSpawn.rotation;

        if (RandomSpawnEnemy == 101) //���̵� ���̴�(Ÿ��Ʈ��Ű ����)
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

    //�� ����
    void SelectionEnemy()
    {
        if (RandomSpawnEnemy == 0) //ī��Ƽ-���̿���4(�Ϲ�)
            EnemyName = "Kaotijaios4";
        else if (RandomSpawnEnemy == 1) //ī��Ƽ-���̿���4(���� ���̵�)
            EnemyName = "Kaotijaios4Spear";
        else if (RandomSpawnEnemy == 2) //ī��Ƽ-���̿���4(��ȭ��)
            EnemyName = "Kaotijaios4Fleet1389";
        else if (RandomSpawnEnemy == 3) //Ÿ��ī-����-����Ʈ�� 1(���� ���ݼ�)
            EnemyName = "TaikaLaiThrotro1";
        else if (RandomSpawnEnemy == 4) //ī��Ƽ-���̿���4(����)
            EnemyName = "Kaotijaios4Dualgun";
        else if (RandomSpawnEnemy == 5) //ī��Ƽ-���̿���4 ����Ʈ1(������)
            EnemyName = "Kaotijaios4Armor";
        else if (RandomSpawnEnemy == 6) //ī��Ƽ-���̿���4 ����Ʈ2(���� + ��ȭ ����)
            EnemyName = "Kaotijaios4ArmorDualgun";
        else if (RandomSpawnEnemy == 7) //Ÿ��ī-����-����Ʈ�� 1(�ö�� ������)
            EnemyName = "TaikaLaiThrotro1Plasma";
        else if (RandomSpawnEnemy == 100) //����
            EnemyName = "ZomBie";
        else if (RandomSpawnEnemy == 101) //���̵� ���̴�(Ÿ��Ʈ��Ű ����)
            EnemyName = "ZomBie";
        else if (RandomSpawnEnemy == 200) //��Ʈ��-ũ�ν��� 390(���� �̻���)
            EnemyName = "AtroCrossfa390";
        else if (RandomSpawnEnemy == 201) //�ƽ��� ���̿��ξ�(����Ʈ ���ݺ�)
            EnemyName = "AsoShiioshare";
    }
}