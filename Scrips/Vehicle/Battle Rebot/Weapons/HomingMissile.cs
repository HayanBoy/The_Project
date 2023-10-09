using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{

    public GameObject Prefab;
    public Transform particlePos;

    GameObject[] Particle;

    public int damage = 50;

    Rigidbody2D rigid = null;
    Transform target = null;

    [SerializeField] float speed = 0f; // 미사일의 최고속도 
    float currentSpeed = 0f;
    [SerializeField] LayerMask layerMask=0; // 어떤 레이어를 특정할 것인가 

    void SearchEnemy()
    {
        // Collider[] cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 100F, layerMask);

        if (cols.Length > 0)
        {
            target = cols[Random.Range(0, cols.Length)].transform;
        }
 
    }

    IEnumerator LaunchDelay() // 속도가 0이 되었을 때 발사, 생성된 뒤 5초후 삭제 
    {
        yield return new WaitUntil(() => rigid.velocity.y < 0f);
        yield return new WaitForSeconds(0.3f);

        SearchEnemy();

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchDelay());


        Particle = new GameObject[5];

        Generate();
    }

    void Update() // 호밍미사일이 추적하는 코드 
    {


        if (target != null)
        {
            if (currentSpeed <= speed)
                currentSpeed += speed * Time.deltaTime;

            transform.position += transform.up * currentSpeed * Time.deltaTime;

            Vector3 dir = (target.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, dir, 0.1f);
        }
    }

    void Generate()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            Particle[index] = Instantiate(Prefab);
            Particle[index].SetActive(false);
        }
    }

    public GameObject Loader()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            if (!Particle[index].activeSelf)
            {
                Particle[index].SetActive(true);
                return Particle[index];
            }
        }

        return null;
    }
    void Active()
    {
        GameObject Effect = Loader();
        Effect.transform.position = particlePos.position;
        Effect.transform.rotation = particlePos.rotation;
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision is CircleCollider2D && collision.transform.CompareTag("Enemy") && collision.gameObject.layer == 13)
            {
                //카오티 - 자이오스4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Destroy(gameObject);
                Active();
            }
            if (collision is CircleCollider2D && collision.transform.CompareTag("Enemy") && collision.gameObject.layer == 14)
            { 
                //타이카 - 라이 - 쓰로트로1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Destroy(gameObject);
                 Active();
             }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision) // 데미지 전달 
    //{
    //    if (collision is CircleCollider2D && collision.transform.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
    //    {
    //        Debug.Log("펑");
    //        HealthTaikaLaiThrotro1 Throtro = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 스크립트 불러오기
    //        StartCoroutine(Throtro.DamageCharacter(damage, 0.0f));
    //        Destroy(gameObject);

    //    }

    //    if (collision is CircleCollider2D && collision.transform.CompareTag("Kantakri, Kaoti-Jaios 4"))
    //    {
    //        Debug.Log("펑");
    //        KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
    //        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f));
    //        Destroy(gameObject);
    //    }
    //}
}
