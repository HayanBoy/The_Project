using System.Collections;
using UnityEngine;

public class SpawnLevelType : MonoBehaviour
{
    public SpawnSiteGenerator SpawnSiteGenerator;

    //스폰1 영역
    public void SpawnOfPlanet1()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            SpawnSiteGenerator.SpawnCount = 3;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 1;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 3;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 5;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //스폰2 영역
    public void SpawnOfPlanet2()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            SpawnSiteGenerator.SpawnCount = 3;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 2;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 5;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 7;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //스폰3 영역
    public void SpawnOfPlanet3()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            SpawnSiteGenerator.SpawnCount = 3;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 3;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 5;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            SpawnSiteGenerator.SpawnCount = 5;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 7;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //기함 침투전 스폰
    public void SpawnOfFlagship1()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            SpawnSiteGenerator.SpawnCount = 3;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 1;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 5;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            SpawnSiteGenerator.SpawnCount = 3;
            SpawnSiteGenerator.StartEnemyType = 0;
            SpawnSiteGenerator.EndEnemyType = 5;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //좀비 스폰
    public void SpawnOfZombie1()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            SpawnSiteGenerator.SpawnCount = 4;
            SpawnSiteGenerator.StartEnemyType = 100;
            SpawnSiteGenerator.EndEnemyType = 100;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            SpawnSiteGenerator.SpawnCount = 6;
            SpawnSiteGenerator.StartEnemyType = 100;
            SpawnSiteGenerator.EndEnemyType = 100;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            SpawnSiteGenerator.SpawnCount = 8;
            SpawnSiteGenerator.StartEnemyType = 100;
            SpawnSiteGenerator.EndEnemyType = 100;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //엘리트 스폰1 영역
    public void EliteSpawn1()
    {
        int OneType = Random.Range(0, 2);
        SpawnSiteGenerator.SpawnCount = 1;

        if (OneType == 0) //단일 엘리트
        {
            int Number = Random.Range(200, 202);
            SpawnSiteGenerator.StartEnemyType = Number;
            SpawnSiteGenerator.EndEnemyType = Number;
        }
        else //여러 엘리트
        {
            SpawnSiteGenerator.StartEnemyType = 200;
            SpawnSiteGenerator.EndEnemyType = 201;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //엘리트 스폰2 영역
    public void EliteSpawn2()
    {
        int OneType = Random.Range(0, 2);
        SpawnSiteGenerator.SpawnCount = Random.Range(1, 3);

        if (OneType == 0) //단일 엘리트
        {
            int Number = Random.Range(200, 202);
            SpawnSiteGenerator.StartEnemyType = Number;
            SpawnSiteGenerator.EndEnemyType = Number;
        }
        else //여러 엘리트
        {
            SpawnSiteGenerator.StartEnemyType = 200;
            SpawnSiteGenerator.EndEnemyType = 201;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //엘리트 스폰3 영역
    public void EliteSpawn3()
    {
        int OneType = Random.Range(0, 2);
        SpawnSiteGenerator.SpawnCount = Random.Range(2, 4);

        if (OneType == 0) //단일 엘리트
        {
            int Number = Random.Range(200, 202);
            SpawnSiteGenerator.StartEnemyType = Number;
            SpawnSiteGenerator.EndEnemyType = Number;
        }
        else //여러 엘리트
        {
            SpawnSiteGenerator.StartEnemyType = 200;
            SpawnSiteGenerator.EndEnemyType = 201;
        }
        SpawnSiteGenerator.StartSpawn();
    }

    //좀비 엘리트 스폰1 영역
    public void ZombieEliteSpawn1()
    {
        int OneType = Random.Range(0, 2);
        SpawnSiteGenerator.SpawnCount = Random.Range(2, 4);

        if (OneType == 0) //단일 엘리트
        {
            int Number = Random.Range(101, 101);
            SpawnSiteGenerator.StartEnemyType = Number;
            SpawnSiteGenerator.EndEnemyType = Number;
        }
        else //여러 엘리트
        {
            SpawnSiteGenerator.StartEnemyType = 101;
            SpawnSiteGenerator.EndEnemyType = 101;
        }
        SpawnSiteGenerator.StartSpawn();
    }
}