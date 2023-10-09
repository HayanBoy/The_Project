using System.Collections;
using UnityEngine;

public class BombSettings : MonoBehaviour
{
    public Stage1_9 Stage1_9;
    public ActionZero ActionZero;
    public StartHA767 StartHA767;
    public ClearLine ClearLine;

    [Header("기함 침투전 폭탄")]
    public GameObject Bomb; //폭탄
    public GameObject DeadLine; //폭탄이 터질 때 적들만 골라서 죽이는 프리팹
    public Transform BombPosition; //폭탄이 설치될 장소

    [Header("기함 침투전 폭탄의 작동")]
    public bool isBombSetted = false;
    public bool BombExplosion = false; //폭탄이 터질 때에만 작동
    public float ExplosionTime;

    //폭탄이 설치됨
    public void BombSetted(GameObject MissionBomb)
    {
        isBombSetted = true;
        Bomb = MissionBomb;

        StartCoroutine(StartSpawn());
        ActionZero.RestartUI();
        //Debug.Log("폭탄이 설치됨");
    }

    private void Update()
    {
        if (isBombSetted == true)
        {
            ExplosionTime -= Time.deltaTime;

            if (ExplosionTime <= 0.33f)
            {
                Bomb.GetComponent<Animator>().SetFloat("Step, Mission Bomb", 2);
            }
            if (ExplosionTime <= 0)
            {
                isBombSetted = false;
                ExplosionTime = 0;
                Destroy(Bomb);
                Instantiate(DeadLine, BombPosition.position, BombPosition.rotation);
                StartCoroutine(DestroyTower());

                Stage1_9.StopSpawn();
                if (BattleSave.Save1.FinishSpawnNumber == 3)
                    ClearLine.MissionComplete(Stage1_9.SpawnMap3);
                else if (BattleSave.Save1.FinishSpawnNumber == 4)
                    ClearLine.MissionComplete(Stage1_9.SpawnMap4);
                Debug.Log("기함 침투전 임무 완료");
                //Debug.Log("임무 완료");
            }
        }
    }

    //폭탄 설치 이후, 스폰을 시작한다.
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(2);

        if (BattleSave.Save1.FinishSpawnNumber == 3)
        {
            Stage1_9.Step3 = false;
            Stage1_9.StartSpawnEnemy3();
        }
        else if (BattleSave.Save1.FinishSpawnNumber == 4)
        {
            Stage1_9.Step4 = false;
            Stage1_9.StartSpawnEnemy4();
        }
    }

    //임무 완료로 스폰을 종료시킨다.
    IEnumerator DestroyTower()
    {
        StartHA767.TowerPrefab.GetComponent<Animator>().SetBool("Slorius tower destroy", true);
        yield return new WaitForSeconds(1.16f);
        Instantiate(DeadLine, BombPosition.position, BombPosition.rotation);
        BombExplosion = true;
    }
}