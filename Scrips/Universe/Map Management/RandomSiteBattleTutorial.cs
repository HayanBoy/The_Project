using UnityEngine;

public class RandomSiteBattleTutorial : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public TutorialSystem TutorialSystem;
    public RandomSiteBattle RandomSiteBattle;
    public LiveCommunicationSystem LiveCommunicationSystem;

    [Header("Ʃ�丮��")]
    public bool Tutorial;
    public bool FlagshipHere; //������ �ش� ���� ����Ʈ�� ������ ǥ��
    public GameObject BattleSite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BattleSave.Save1.FirstStart == true)
        {
            if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship")) //������ �ش� ����Ʈ�� ������ Ȱ��ȭ
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
        if (RandomSiteBattle.isInFight == true && RandomSiteBattle.EnemySpawn == true && RandomSiteBattle.BattleEnemyShipList.Count == 0) //���� ���� ��, ���� ���� ����
        {
            RandomSiteBattle.enabled = true;
            StartCoroutine(LiveCommunicationSystem.MainCommunication(1.02f));
            TutorialSystem.VictoryBattleSiteTutorial();
            BattleSite.SetActive(false);
        }
    }
}