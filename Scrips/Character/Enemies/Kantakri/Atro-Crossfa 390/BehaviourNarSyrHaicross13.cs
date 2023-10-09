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

    private float Velocity = 0; //미사일 속도
    private float offset;
    private float velocityTime;
    private float Onefire;
    private float distanceFromPlayerDot;
    public int damage;
    int speedpoint = 0;

    private bool Flying = false;
    Vector3 direction;

    private Transform Enemytarget = null;
    private Vector3 PlayerPosition; //플레이어 및 아군 위치 저장

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
        xVnot = Random.Range(-5, 10); //탄피 x위치 시작점 랜덤함수.
        yVnot = Random.Range(20, 40); //탄피 y위치 시작점 랜덤함수.
        offset = Random.Range(-2f, -3f); //탄피 높이 랜덤함수.

        if (xVnot == 0) //xVnot값이 0일 경우, 탄피 배출이 공중에서 멈추는 버그가 발생하므로, 0의 값을 제거하기 위한 무한루프.
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
        rb2D.velocity = new Vector2(xVnot, yVnot); //탄피의 리지드 바디 속도에 x, y축 백터값 입력.
    }

    private void Update()
    {
        Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;

        if (Flying == false)
        {
            rb2D.velocity = new Vector2(xVnot, yVnot);
            velocityTime += Time.fixedDeltaTime;
            //rb2D.rotation = 0;

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //탄피의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 탄피 리지드 바디 y속도가 0보다 작을 경우
            {
                float yVelocity = -rb2D.velocity.y * 0.25f; //탄피의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 탄피이 바닥에 고정 처리 가능.
                float xVelocity = rb2D.velocity.x * 0.25f;

                rb2D.velocity = new Vector2(xVelocity, yVelocity); //이 후, 탄피의 속도를 감소시키는 처리
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
            distanceFromPlayerDot = Vector2.Distance(PlayerPosition, transform.position); //저장된 플레이어 및 아군 좌표값과 적의 거리
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