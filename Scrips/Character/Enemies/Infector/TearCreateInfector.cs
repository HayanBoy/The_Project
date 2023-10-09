using UnityEngine;

public class TearCreateInfector : MonoBehaviour
{
    private float startYPosition;
    private float yVnot;
    private float xVnot;

    public int xVnotMinusRandom;
    public int xVnotPlusRandom;
    public int yVnotMinusRandom;
    public int yVnotPlusRandom;
    public int rotationMinusRandom;
    public int rotationPlusRandom;

    public float acceleration;
    public Rigidbody2D rb2D;

    public float xvelocityReduction;
    public float yvelocityReduction;
    public float offsetMinusRandom;
    public float offsetMPlusRandom;
    private float offset;
    private float velocityTime;
    private float rotationSpeed;
    int speedpoint = 0;

    public bool Direction;
    int LargeThrow;
    float LargeThrow2;
    public bool Throwing = false;

    private float SoundTime;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;

    Vector3 explosionPosition;

    public void SetDirection(bool boolean)
    {
        Direction = boolean;
    }

    public void SetThrow(int num)
    {
        LargeThrow = num;
    }

    public void Start()
    {
        xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //오브젝트 x위치 시작점 랜덤함수.
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //오브젝트 y위치 시작점 랜덤함수.
        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom);


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

        while (true) //오브젝트 회전 속도 랜덤
        {
            rotationSpeed = Random.Range(rotationMinusRandom, rotationPlusRandom);
            speedpoint += 1;

            if (speedpoint >= 1)
            {
                speedpoint = 0;
                break;
            }
        }

        xVnot += LargeThrow;
        yVnot += LargeThrow;

        if (Direction == false)
            xVnot *= 1;
        else
            xVnot *= -1;

        startYPosition = transform.position.y; //오브젝트가 처음 시작되는 장소 지정.
        rb2D.velocity = new Vector2(xVnot, yVnot); //오브젝트의 리지드 바디 속도에 x, y축 백터값 입력.
    }

    private void Update()
    {
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
        Acceleration(); //오브젝트 방출 메서드
    }

    //오브젝트 방출 메서드
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 오브젝트의 움직임 시간 생성

        if (rb2D.velocity.magnitude <= 0.1f) //오브젝트 리지드 바디의 속도 한계점이 0.5보다 작을 경우
        {
            rb2D.velocity = Vector2.zero; //오브젝트의 모든 움직임을 멈춘다.
            //GetComponent<Renderer>().sortingOrder = 0; //오브젝트가 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //오브젝트 방향 전환하는 최초 자세점, 방향전환 속도 계산

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //오브젝트의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 오브젝트 리지드 바디 y속도가 0보다 작을 경우
        {
            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
                Destroy(BloodFloor, 10);
            }

            float yVelocity = -rb2D.velocity.y * yvelocityReduction; //오브젝트의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 오브젝트가 바닥에 고정 처리 가능.
            float xVelocity = rb2D.velocity.x * xvelocityReduction;

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //이 후, 오브젝트의 속도를 감소시키는 처리
            velocityTime = 0;
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }
}
