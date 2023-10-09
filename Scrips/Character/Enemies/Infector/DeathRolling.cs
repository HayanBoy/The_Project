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

            xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //������Ʈ x��ġ ������ �����Լ�.
            yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //������Ʈ y��ġ ������ �����Լ�.

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

            while (true) //������Ʈ ȸ�� �ӵ� ����
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

            startYPosition = transform.position.y; //������Ʈ�� ó�� ���۵Ǵ� ��� ����.
            rb2D.velocity = new Vector2(xVnot, yVnot); //������Ʈ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
        }

        velocityTime += Time.deltaTime;

        if (velocityTime < RollingTime)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); //������Ʈ ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���
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