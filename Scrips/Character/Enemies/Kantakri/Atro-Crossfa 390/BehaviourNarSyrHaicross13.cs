using System.Collections;
using UnityEngine;

public class BehaviourNarSyrHaicross13 : MonoBehaviour
{
    ObjectManager objectManager;

    private float startYPosition;
    public float yVnot;
    public float xVnot;

    public float acceleration = -9.8f;
    public Rigidbody2D rb2D;

    private float Velocity = 0; //�̻��� �ӵ�
    private float offset;
    private float velocityTime;
    private float Onefire;
    private float distanceFromPlayerDot;
    public int damage;
    int speedpoint = 0;

    private bool Flying = false;
    Vector3 direction;

    private Transform Enemytarget = null;
    private Vector3 PlayerPosition; //�÷��̾� �� �Ʊ� ��ġ ����

    public GameObject MissilePrefab;
    public GameObject WarnningMarkPrefab;
    GameObject Warnning;
    public Transform MissilePos;

    public void SetDamage(int num)
    {
        damage = num;
    }

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        rb2D = GetComponent<Rigidbody2D>();
        Invoke("EngineStart", 0.5f);
    }

    void OnEnable()
    {
        xVnot = Random.Range(-5, 10); //ź�� x��ġ ������ �����Լ�.
        yVnot = Random.Range(20, 40); //ź�� y��ġ ������ �����Լ�.
        offset = Random.Range(-2f, -3f); //ź�� ���� �����Լ�.

        if (xVnot == 0) //xVnot���� 0�� ���, ź�� ������ ���߿��� ���ߴ� ���װ� �߻��ϹǷ�, 0�� ���� �����ϱ� ���� ���ѷ���.
        {
            while (true)
            {
                xVnot = Random.Range(-8, 8);

                if (xVnot != 0)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        rb2D.velocity = new Vector2(xVnot, yVnot); //ź���� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
    }

    private void Update()
    {
        Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;

        if (Flying == false)
        {
            rb2D.velocity = new Vector2(xVnot, yVnot);
            velocityTime += Time.fixedDeltaTime;
            //rb2D.rotation = 0;

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //ź���� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ź�� ������ �ٵ� y�ӵ��� 0���� ���� ���
            {
                float yVelocity = -rb2D.velocity.y * 0.25f; //ź���� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ź���� �ٴڿ� ���� ó�� ����.
                float xVelocity = rb2D.velocity.x * 0.25f;

                rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ź���� �ӵ��� ���ҽ�Ű�� ó��
                velocityTime = 0;
            }
            else
            {
                float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
                rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
            }
        }
        else
        {
            rb2D.transform.up = Vector3.Lerp(transform.up, direction, 0.1f);
            Velocity += Time.deltaTime * 100;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerPosition.x, PlayerPosition.y, transform.position.z), Velocity * Time.deltaTime);
            float yVelocity = rb2D.velocity.y * Time.deltaTime * -0.01f;
            float xVelocity = rb2D.velocity.x * Time.deltaTime * -0.01f;
            rb2D.velocity = new Vector2(xVelocity, yVelocity);

            //Debug.Log("distanceFromPlayerDot : " + distanceFromPlayerDot);
            distanceFromPlayerDot = Vector2.Distance(PlayerPosition, transform.position); //����� �÷��̾� �� �Ʊ� ��ǥ���� ���� �Ÿ�
            if (distanceFromPlayerDot <= 0.1f && Onefire == 0)
            {
                Onefire += Time.deltaTime;
                Destroy(Warnning);
                GameObject Missile = Instantiate(MissilePrefab, MissilePos.position, Quaternion.identity);
                Missile.GetComponent<VM5DamageToPlayer>().SetDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    void EngineStart()
    {
        PlayerPosition = Enemytarget.transform.position;
        direction = (PlayerPosition - transform.position).normalized;
        Warnning = Instantiate(WarnningMarkPrefab, PlayerPosition, Quaternion.identity);
        Flying = true;
    }
}