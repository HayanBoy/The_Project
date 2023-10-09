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

    [SerializeField] float speed = 0f; // �̻����� �ְ�ӵ� 
    float currentSpeed = 0f;
    [SerializeField] LayerMask layerMask=0; // � ���̾ Ư���� ���ΰ� 

    void SearchEnemy()
    {
        // Collider[] cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 100F, layerMask);

        if (cols.Length > 0)
        {
            target = cols[Random.Range(0, cols.Length)].transform;
        }
 
    }

    IEnumerator LaunchDelay() // �ӵ��� 0�� �Ǿ��� �� �߻�, ������ �� 5���� ���� 
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

    void Update() // ȣ�ֹ̻����� �����ϴ� �ڵ� 
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
                //ī��Ƽ - ���̿���4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Destroy(gameObject);
                Active();
            }
            if (collision is CircleCollider2D && collision.transform.CompareTag("Enemy") && collision.gameObject.layer == 14)
            { 
                //Ÿ��ī - ���� - ����Ʈ��1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Destroy(gameObject);
                 Active();
             }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision) // ������ ���� 
    //{
    //    if (collision is CircleCollider2D && collision.transform.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
    //    {
    //        Debug.Log("��");
    //        HealthTaikaLaiThrotro1 Throtro = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
    //        StartCoroutine(Throtro.DamageCharacter(damage, 0.0f));
    //        Destroy(gameObject);

    //    }

    //    if (collision is CircleCollider2D && collision.transform.CompareTag("Kantakri, Kaoti-Jaios 4"))
    //    {
    //        Debug.Log("��");
    //        KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
    //        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f));
    //        Destroy(gameObject);
    //    }
    //}
}
