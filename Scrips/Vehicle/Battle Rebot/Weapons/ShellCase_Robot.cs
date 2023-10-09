using UnityEngine;

public class ShellCase_Robot : MonoBehaviour
{
    private float startYPosition;
    private float yVnot;
    public float xVnot;

    public int xVnotMinusRandom;
    public int xVnotPlusRandom;
    public int yVnotMinusRandom;
    public int yVnotPlusRandom;
    public int rotationMinusRandom;
    public int rotationPlusRandom;
    public float offsetMinusRandom;
    public float offsetMPlusRandom;

    public float acceleration = -9.8f;
    public Rigidbody2D rb2D;

    int LargeThrow;
    float LargeThrow2;
    public bool Direction;
    public bool Throwing = false;

    private float offset;
    private float velocityTime;
    private float rotationSpeed;
    int speedpoint = 0;

    Vector3 explosionPosition;
    public float Pos;

    private float SoundTime;
    public AudioClip HTACFalling;

    public void SetDirection(bool boolean)
    {
        Direction = boolean;
    }

    public void SetThrow(int num)
    {
        LargeThrow = num;
    }

    private void OnEnable()
    {
        xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom);
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //탄피 y위치 시작점 랜덤함수.
        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom);

        while (true) //탄피 회전 속도 랜덤
        {
            rotationSpeed = Random.Range(rotationMinusRandom, rotationPlusRandom);
            //Debug.Log(rotationSpeed);
            speedpoint += 1;

            if (speedpoint >= 1)
            {
                speedpoint = 0;
                break;
            }
        }

        rb2D.velocity = new Vector2(xVnot, yVnot);
    }

    private void OnDisable()
    {
        GetComponent<Renderer>().sortingOrder = 8;
        GetComponent<Renderer>().sortingLayerName = "Charactors";
        velocityTime = 0;
        SoundTime = 0;
    }

    private void Update()
    {
        //Debug.Log("startYPosition : " + startYPosition);
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
        if (transform.rotation.y == 0)
        {
            velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 탄피의 움직임 시간 생성.

            if (rb2D.velocity.magnitude <= 0.1f) //탄피 리지드 바디의 속도 한계점이 0.5보다 작을 경우
            {
                rb2D.velocity = Vector2.zero; //탄피의 모든 움직임을 멈춘다.
                GetComponent<Renderer>().sortingLayerName = "Shell"; //탄피가 바닥에 완전히 떨어진 후 이름 변경.
                GetComponent<Renderer>().sortingOrder = 0; //탄피가 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.

                return;
            }

            transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //탄피 방향 전환하는 최초 자세점, 방향전환 속도 계산

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //탄피의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 탄피 리지드 바디 y속도가 0보다 작을 경우
            {
                float yVelocity = -rb2D.velocity.y * 0.3f; //탄피의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 탄피이 바닥에 고정 처리 가능.
                float xVelocity = rb2D.velocity.x * 0.3f;

                if (SoundTime == 0)
                {
                    SoundTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay(" Sound", HTACFalling);
                }

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
            velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 탄피의 움직임 시간 생성.

            if (rb2D.velocity.magnitude <= 0.1f) //탄피 리지드 바디의 속도 한계점이 0.5보다 작을 경우
            {
                rb2D.velocity = Vector2.zero; //탄피의 모든 움직임을 멈춘다.
                GetComponent<Renderer>().sortingLayerName = "Shell"; //탄피가 바닥에 완전히 떨어진 후 이름 변경.
                GetComponent<Renderer>().sortingOrder = 0; //탄피가 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.

                return;
            }

            transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //탄피 방향 전환하는 최초 자세점, 방향전환 속도 계산

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //탄피의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 탄피 리지드 바디 y속도가 0보다 작을 경우
            {
                float yVelocity = -rb2D.velocity.y * 0.3f; //탄피의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 탄피이 바닥에 고정 처리 가능.
                float xVelocity = rb2D.velocity.x * 0.3f;

                if (SoundTime == 0)
                {
                    SoundTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay(" Sound", HTACFalling);
                }

                rb2D.velocity = new Vector2(xVelocity, yVelocity); //이 후, 탄피의 속도를 감소시키는 처리
                velocityTime = 0;
            }
            else
            {
                float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
                rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
            }
        }
    }

    //플레이어 이동에서의 탄피 튀기
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            explosionPosition = collision.transform.position; //컴포넌트 위치 획득.

            float explosionRadius = 6;
            float explosionForce = 250;
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