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
        if (scoreManager.AllCnt >= EnemyNumber + 1) //�ش� ��� �迭 ��ȣ��ŭ �����Ǿ��� ���, �ش� �迭���� �����Ǿ��ٴ� ���� �ǹ��ϹǷ� ���� Ž���Ѵ�.
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

        if (scoreManager.AllCnt < EnemyNumber + 1) //�ش� ��� �迭 ��ȣ��ŭ ������ �ȵǾ��� ���, �ش� �迭���� ������ �ȵǾ��ٴ� ���� �ǹ��ϹǷ� Ž���� ���� �ʴ´�.
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