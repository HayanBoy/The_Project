using UnityEngine;

public class DeathRolling : MonoBehaviour
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
    public Rigidbody2D rb2D;
    public float RollingTimeOne;

    private float velocityTime;
    private float rotationSpeed;
    public float RollingTime;
    int speedpoint = 0;
    int LargeThrow;
    public bool Direction;

    Vector3 explosionPosition;

    public void SetDirection(bool boolean)
    {
        Direction = boolean;
    }

    public void SetThrow(int num)
    {
        LargeThrow = num;
    }

    void Update()
    {
        if(RollingTimeOne == 0)
        {
            RollingTimeOne += Time.deltaTime;

            xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //오브젝트 x위치 시작점 랜덤함수.
            yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //오브젝트 y위치 시작점 랜덤함수.

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

        velocityTime += Time.deltaTime;

        if (velocityTime < RollingTime)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); //오브젝트 방향 전환하는 최초 자세점, 방향전환 속도 계산
            Invoke("TurnOff", 0.5f);
        }
    }

    void TurnOff()
    {
        velocityTime = 0;
        RollingTimeOne = 0;
        GetComponent<DeathRolling>().enabled = false;
    }
}