using UnityEngine;

public class RandomSiteBattleTutorial : MonoBehaviour
{
    [Header("스크립트")]
    public TutorialSystem TutorialSystem;
    public RandomSiteBattle RandomSiteBattle;
    public LiveCommunicationSystem LiveCommunicationSystem;

    [Header("튜토리얼")]
    public bool Tutorial;
    public bool FlagshipHere; //기함이 해당 전투 사이트에 있음을 표시
    public GameObject BattleSite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BattleSave.Save1.FirstStart == true)
        {
            if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //기함이 해당 사이트에 있으면 활성화
            {
                FlagshipHere = true;
                if (Tutorial == true && RandomSiteBattle.BattleEnemyShipList.Count > 0)
                {
                    TutorialSystem.PopUpBehaviorButton();
                }
            }
        }
    }

    private void Update()
    {
        if (RandomSiteBattle.isInFight == true && RandomSiteBattle.EnemySpawn == true && RandomSiteBattle.BattleEnemyShipList.Count == 0) //전투 종료 후, 스폰 지역 삭제
        {
            RandomSiteBattle.enabled = true;
            StartCoroutine(LiveCommunicationSystem.MainCommunication(1.02f));
            TutorialSystem.VictoryBattleSiteTutorial();
            BattleSite.SetActive(false);
        }
    }
}