using System.Collections;
using UnityEngine;

public class RandomSite : MonoBehaviour
{
    GameObject Mark;
    public GameObject MarkPrefab; //강하 경고 마크 생성 프리팹
    public Transform skyCranePos; //스카이 크레인 생성 좌표
    public Transform kaotiJaios4Pos; //Kaoti-Jaios 4 생성 좌표
    public Transform MarkPos;
    int craneModel;
    int Reset = 0;
    float fallTime;

    int DropSoliders;

    public bool DropMarkOnline = false;

    private void OnEnable()
    {
        DropSoliders = Random.Range(0, 11);
        StartCoroutine(CraneModel());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Reset = 1;
            GameObject.Find("Taika-Lai-Throtro 1").GetComponent<BehaviorTaikaLaiThrotro1_>().reset = Reset;
            gameObject.SetActive(false);
        }
    }

    void CreateMark()
    {
        if(DropMarkOnline == true)
        {
            DropMarkOnline = false;
            Mark = Instantiate(MarkPrefab, MarkPos.transform.position, MarkPos.transform.rotation);
            StartCoroutine(AnimationMark());
            Destroy(Mark, fallTime + 2f);
        }
    }

    IEnumerator AnimationMark()
    {
        Mark.GetComponent<Animator>().SetFloat("Start, Enemy drop", 1);
        yield return new WaitForSeconds(fallTime + 1.5f);
        Mark.GetComponent<Animator>().SetFloat("Start, Enemy drop", 2);
    }

    IEnumerator CraneModel()
    {
        fallTime = Random.Range(2f, 5f);
        Invoke("CreateMark", fallTime / 5);
        yield return new WaitForSeconds(fallTime);

        craneModel = Random.Range(0, 2);

        if (craneModel == 0)
        {
            GameObject skyCrane1 = SingletonObject.instance.Loader("skyCrane");
            skyCrane1.transform.position = skyCranePos.position;
            skyCrane1.transform.rotation = transform.rotation;
        }
        else
        {
            GameObject skyCrane2 = SingletonObject.instance.Loader("skyCrane2"); 
            skyCrane2.transform.position = skyCranePos.position;
            skyCrane2.transform.rotation = transform.rotation;
        }

        if (DropSoliders >= 0 && DropSoliders < 6)
        {
            GameObject KaotiJaios4 = SingletonObject.instance.Loader("Kaotijaios4"); 
            KaotiJaios4.transform.position = kaotiJaios4Pos.position;
            KaotiJaios4.transform.rotation = transform.rotation;
            ScoreManager.instance.EnemyList.Add(KaotiJaios4);

            KaotiJaios4.GetComponent<DropingKaotiJaios4>().SetDroping(true); //Kaoti-Jaios 4에다 강하상태 전달
        }
        else if (DropSoliders >= 6 && DropSoliders < 10)
        {
            GameObject Kaotijaios4Spear = SingletonObject.instance.Loader("Kaotijaios4Spear");
            Kaotijaios4Spear.transform.position = kaotiJaios4Pos.position;
            Kaotijaios4Spear.transform.rotation = transform.rotation;
            ScoreManager.instance.EnemyList.Add(Kaotijaios4Spear);

            Kaotijaios4Spear.GetComponent<DropingKaotiJaios4Spear>().SetDroping(true); //Kaoti-Jaios 4에다 강하상태 전달
        }

        else if (DropSoliders >= 10 && DropSoliders < 12)
        {
            GameObject Kaotijaios4Dualgun = SingletonObject.instance.Loader("Kaotijaios4Dualgun");
            Kaotijaios4Dualgun.transform.position = kaotiJaios4Pos.position;
            Kaotijaios4Dualgun.transform.rotation = transform.rotation;
            ScoreManager.instance.EnemyList.Add(Kaotijaios4Dualgun);

            Kaotijaios4Dualgun.GetComponent<DropingKaotiJaios4Dual>().SetDroping(true); //Kaoti-Jaios 4에다 강하상태 전달
        }

        StartCoroutine(DeleteSite());
    }

    IEnumerator DeleteSite()
    {
        yield return new WaitForSeconds(1);
        ScoreManager.instance.EnemyList.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
