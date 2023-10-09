using UnityEngine;

public class Indicator : MonoBehaviour
{
    ScoreManager scoreManager;
    public Transform Enemytarget = null;
    public int EnemyNumber;
    public float HideDistance;
    private float OneTime;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (scoreManager.AllCnt >= EnemyNumber + 1) //해당 담당 배열 번호만큼 스폰되었을 경우, 해당 배열까지 스폰되었다는 것을 의미하므로 적을 탐색한다.
        {
            Enemytarget = ScoreManager.instance.EnemyList[EnemyNumber].transform;

            var dir = Enemytarget.position - transform.position;
            OneTime = 0;

            if (dir.magnitude < HideDistance)
                SetChildrenActive(false);
            else
            {
                SetChildrenActive(true);
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        if (scoreManager.AllCnt < EnemyNumber + 1) //해당 담당 배열 번호만큼 스폰이 안되었을 경우, 해당 배열까지 스폰이 안되었다는 것을 의미하므로 탐색을 하지 않는다.
        {
            if (OneTime == 0)
            {
                OneTime += Time.deltaTime;
                Enemytarget = null;
                SetChildrenActive(false);
            }
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(value);
    }
}