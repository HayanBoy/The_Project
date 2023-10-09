using UnityEngine;

public class StageStart : MonoBehaviour
{
    public GameObject Stage;
    public int StageCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (StageCount == 1)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy1();
            else if (StageCount == 2)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy2();
            else if (StageCount == 3)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy3();
            else if (StageCount == 4)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy4();
            else if (StageCount == 5)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy5();
            else if (StageCount == 6)
                Stage.GetComponent<Stage1_9>().StartSpawnEnemy6();
        }
    }
}