using UnityEngine;

public class ShellCase_SW06 : MonoBehaviour
{
    public int LED; //LED가 켜진 탄피인지에 대한 여부
    public GameObject LEDOn;

    private float startYPosition;
    public float yVnot;
    public float xVnot;
    public float offset = -2.1f;

    public int xVnotMinusRandom = -5;
    public int xVnotPlusRandom = 5;
    public int yVnotMinusRandom = -5;
    public int yVnotPlusRandom = 5;
    public float offsetMinusRandom = -1.5f;
    public float offsetMPlusRandom = -3.5f;

    public float acceleration = -9.8f;
    public Rigidbody2D rb2D;

    private float velocityTime;
    private float rotationSpeed;
    int speedpoint = 0;

    int LargeThrow;
    float LargeThrow2;
    public bool Direction;
    public bool Throwing = false;

    Vector3 explosionPosition;
    public float Pos;

    private float SoundTime;
    private int FallSoundRandom;
    public AudioClip ShellFalling1;
    public AudioClip ShellFalling2;
    public AudioClip ShellFalling3;
    public AudioClip ShellFalling4;
    public AudioClip ShellFalling5;

    public void Layer()
    {
        GetComponent<Renderer>().sortingOrder = 47;
        GetComponent<Renderer>().sortingLayerName = "Player";

        if (LED == 1)
        {
            LEDOn.GetComponent<Renderer>().sortingOrder = 48;
            LEDOn.GetComponent<Renderer>().sortingLayerName = "Player";
        }
    }

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
        xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //탄피 x위치 시작점 랜덤함수.
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //탄피 y위치 시작점 랜덤함수.
        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom); //탄피 높이 랜덤함수.

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

        if (LED == 1)
        {
            LEDOn.GetComponent<Renderer>().sortingOrder = 9;
            LEDOn.GetComponent<Renderer>().sortingLayerName = "Charactors";
        }

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

    void ShellFallSound()
    {
        FallSoundRandom = Random.Range(0, 5);
        if(FallSoundRandom == 0)
            SoundManager.instance.SFXPlay6(" Sound", ShellFalling1);
        else if (FallSoundRandom == 1)
            SoundManager.instance.SFXPlay6(" Sound", ShellFalling2);
        else if (FallSoundRandom == 2)
            SoundManager.instance.SFXPlay6(" Sound", ShellFalling3);
        else if (FallSoundRandom == 3)
            SoundManager.instance.SFXPlay6(" Sound", ShellFalling4);
        else if (FallSoundRandom == 4)
            SoundManager.instance.SFXPlay6(" Sound", ShellFalling5);
    }

    //탄피 방출 메서드
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //델타 타임 더해서 계산. 오브젝트의 움직임 시간 생성

        if (rb2D.velocity.magnitude <= 0.1f) //오브젝트 리지드 바디의 속도 한계점이 0.5보다 작을 경우
        {
            rb2D.velocity = Vector2.zero; //오브젝트의 모든 움직임을 멈춘다.
            GetComponent<Renderer>().sortingOrder = 0; //오브젝트가 바닥에 완전히 떨어진 후 레이어를 맨 뒤로 정렬.
            GetComponent<Renderer>().sortingLayerName = "Shell"; //탄피가 바닥에 완전히 떨어진 후 이름 변경.
            if (LED == 1)
            {
                LEDOn.GetComponent<Renderer>().sortingOrder = 1;
                LEDOn.GetComponent<Renderer>().sortingLayerName = "Shell";
            }

            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //오브젝트 방향 전환하는 최초 자세점, 방향전환 속도 계산

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //오브젝트의 y위치가 시작점+오프셋거리보다 같거나 작을 경우, 혹은 오브젝트 리지드 바디 y속도가 0보다 작을 경우
        {
            float yVelocity = -rb2D.velocity.y * 0.25f; //탄피의 x, y축 속도를 감소시키기 위한 계산 처리. 이 부분을 통해 중력없이 탄피이 바닥에 고정 처리 가능.
            float xVelocity = rb2D.velocity.x * 0.25f;

            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                ShellFallSound();
            }

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //이 후, 오브젝트의 속도를 감소시키는 처리
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

            float explosionRadius = 100;
            float explosionForce = 25;
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