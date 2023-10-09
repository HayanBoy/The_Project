using UnityEngine;

public class SW06MagazineFall : MonoBehaviour
{
    public int LED; //LED가 켜진 탄피인지에 대한 여부
    public GameObject LEDOn;

    private float startYPosition;
    public float yVnot;
    public float xVnot;

    public float acceleration = -7f;
    public Rigidbody2D rb2D;

    private float offset;
    private float velocityTime;
    private float rotationSpeed;
    int speedpoint = 0;

    int LargeThrow;
    float LargeThrow2;
    public bool Direction;
    public bool Throwing = false;

    Vector3 explosionPosition;

    public void SetDirection(bool boolean)
    {
        Direction = boolean;
    }

    public void SetThrow(int num)
    {
        LargeThrow = num;
    }

    void Start()
    {
        startYPosition = transform.position.y; //탄창이 처음 시작되는 장소 지정.
        rb2D.velocity = new Vector2(xVnot, yVnot); //탄창의 리지드 바디 속도에 x, y축 백터값 입력.
        offset = Random.Range(-3.5f, -2.1f); //탄피 높이 랜덤함수.

        while (true) //탄창 회전 속도 랜덤
        {
            rotationSpeed = Random.Range(50, 600);
            speedpoint += 1;

            if(speedpoint >= 1)
            {
                speedpoint = 0;
                break;
            }
        }
    }

    private void Update()
    {
        if (Throwing == true)
        {
            LargeThrow2 = Random.Range(5, 10);

            xVnot += 8 + LargeThrow * LargeThrow2;
            yVnot += LargeThrow * LargeThrow2;

            if (Direction == false)
                xVnot *= 1;
            else
                xVnot *= -1;

            rb2D.velocity = new Vector2(xVnot, yVnot);
            Falling();
            Throwing = false;
        }
    }

    public void FixedUpdate()
    {
        if(Throwing == false)
        {
            Falling();
        }
    }

    private void Falling()
    {
        velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 탄창의 움직임 시간 생성.

        if (rb2D.velocity.magnitude <= 0.1f && Throwing == false) //탄창 리지드 바디의 속도 한계점이 0.1보다 작을 경우
        {
            rb2D.velocity = Vector2.zero; //탄창의 모든 움직임을 멈춘다.
            GetComponent<Renderer>().sortingLayerName = "Shell"; //탄피가 바닥에 완전히 떨어진 후 이름 변경.
            GetComponent<Renderer>().sortingOrder = 0; //탄창이 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.
            if (LED == 1)
            {
                LEDOn.GetComponent<Renderer>().sortingOrder = 1;
                LEDOn.GetComponent<Renderer>().sortingLayerName = "Shell";
            }
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //탄창 방향 전환하는 최초 자세점, 방향전환 속도 계산

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //탄창의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 탄환 리지드 바디 y속도가 0보다 작을 경우
        {
            float yVelocity = -rb2D.velocity.y * 0.25f; //탄창의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 탄창이 바닥에 고정 처리 가능.
            float xVelocity = rb2D.velocity.x * 0.25f;

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //이 후, 탄창의 속도를 감소시키는 처리
            velocityTime = 0;
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }

    //플레이어 이동에서의 탄창 튀기
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            explosionPosition = collision.transform.position; //컴포넌트 위치 획득.

            float explosionRadius = 6;
            float explosionForce = 100;
            float upliftModifier = 5;

            AddExplosionForce(explosionRadius, explosionForce, upliftModifier);
        }
    }

    //탄창 튀는 모션
    public void AddExplosionForce(float eR, float eF, float uM)
    {
        var dir = (rb2D.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / eR);
        Vector3 baseForce = dir.normalized * eF * wearoff;
        rb2D.AddForce(baseForce);

        float upliftWearoff = 1 - uM / eR;
        Vector3 upliftForce = Vector2.up * eF * upliftWearoff;
        rb2D.AddForce(upliftForce);
    }
}