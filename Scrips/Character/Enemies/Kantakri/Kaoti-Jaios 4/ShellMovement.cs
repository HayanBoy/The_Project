using UnityEngine;

public class ShellMovement : MonoBehaviour
{
    private float startYPosition;
    public float yVnot;
    public float xVnot;

    public float acceleration = -9.8f;
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
    public float Pos;

    public void SetDirection(bool boolean)
    {
        Direction = boolean;
    }

    public void SetThrow(int num)
    {
        LargeThrow = num;
    }

    void OnEnable()
    {
        xVnot = Random.Range(-5, 5); //탄피 x위치 시작점 랜덤함수.
        yVnot = Random.Range(-5, 5); //탄피 y위치 시작점 랜덤함수.
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

        while (true) //탄피 회전 속도 랜덤
        {
            rotationSpeed = Random.Range(50, 600);
            //Debug.Log(rotationSpeed);
            speedpoint += 1;

            if (speedpoint >= 1)
            {
                speedpoint = 0;
                break;
            }
        }

        rb2D.velocity = new Vector2(xVnot, yVnot); //탄피의 리지드 바디 속도에 x, y축 백터값 입력.
    }

    private void OnDisable()
    {
        GetComponent<Renderer>().sortingOrder = 8;
        GetComponent<Renderer>().sortingLayerName = "Charactors";
        velocityTime = 0;
    }

    private void Update()
    {
        startYPosition = Pos; //탄피가 처음 시작되는 장소 지정.

        if (Throwing == true)
        {
            Throwing = false;

            LargeThrow2 = Random.Range(2, 4);

            xVnot += LargeThrow * LargeThrow2;
            yVnot += LargeThrow * LargeThrow2;

            if (Direction == false)
                xVnot *= 1;
            else
                xVnot *= -1;

            rb2D.velocity = new Vector2(xVnot, yVnot);
            Acceleration();
        }
    }

    private void FixedUpdate()
    {
        Acceleration(); //탄피 방출 메서드
    }

    //탄피 방출 메서드
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 탄피의 움직임 시간 생성.

        if (rb2D.velocity.magnitude <= 0.1f) //탄피 리지드 바디의 속도 한계점이 0.5보다 작을 경우
        {
            rb2D.velocity = Vector2.zero; //탄피의 모든 움직임을 멈춘다.
            GetComponent<Renderer>().sortingLayerName = "Shell";
            GetComponent<Renderer>().sortingOrder = 0; //탄피가 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //탄피 방향 전환하는 최초 자세점, 방향전환 속도 계산

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

    //플레이어 이동에서의 탄피 튀기
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

    //탄피 튀는 모션
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