using UnityEngine;

public class DebrisSkyCrane : MonoBehaviour
{
    private float startYPosition;
    private float yVnot;
    private float xVnot;

    public int xVnotMinusRandom = -10;
    public int xVnotPlusRandom= 10;
    public int yVnotMinusRandom = -10;
    public int yVnotPlusRandom = 10;
    public int rotationMinusRandom = 50;
    public int rotationPlusRandom = 600;

    public float acceleration = -5;
    public Rigidbody2D rb2D;

    int LargeThrow;
    float LargeThrow2;
    public bool Direction;
    public bool Throwing = false;

    public float xvelocityReduction = 0.25f;
    public float yvelocityReduction = 0.25f;
    public float offsetMinusRandom = -1.5f;
    public float offsetMPlusRandom = -3.5f;
    private float offset;
    private float velocityTime;
    private float rotationSpeed;
    int speedpoint = 0;

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

    private void OnEnable()
    {
        xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //������Ʈ x��ġ ������ �����Լ�.
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //������Ʈ y��ġ ������ �����Լ�.

        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom);


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

        startYPosition = Pos; //������Ʈ�� ó�� ���۵Ǵ� ��� ����.
        rb2D.velocity = new Vector2(xVnot, yVnot); //������Ʈ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
    }

    private void OnDisable()
    {
        GetComponent<Renderer>().sortingOrder = 8;
        GetComponent<Renderer>().sortingLayerName = "Charactors";
        velocityTime = 0;
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
        Acceleration(); //������Ʈ ���� �޼���
    }

    //������Ʈ ���� �޼���
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. ������Ʈ�� ������ �ð� ����

        if (rb2D.velocity.magnitude <= 0.1f) //������Ʈ ������ �ٵ��� �ӵ� �Ѱ����� 0.5���� ���� ���
        {
            rb2D.velocity = Vector2.zero; //������Ʈ�� ��� �������� �����.
            GetComponent<Renderer>().sortingOrder = 0; //������Ʈ�� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //������Ʈ ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //������Ʈ�� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ������Ʈ ������ �ٵ� y�ӵ��� 0���� ���� ���
        {
            float yVelocity = -rb2D.velocity.y * yvelocityReduction; //������Ʈ�� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ������Ʈ�� �ٴڿ� ���� ó�� ����.
            float xVelocity = rb2D.velocity.x * xvelocityReduction;

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ������Ʈ�� �ӵ��� ���ҽ�Ű�� ó��
            velocityTime = 0;
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }
}
