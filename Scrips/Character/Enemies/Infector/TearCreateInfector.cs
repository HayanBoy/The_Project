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

        xVnot += LargeThrow;
        yVnot += LargeThrow;

        if (Direction == false)
            xVnot *= 1;
        else
            xVnot *= -1;

        startYPosition = transform.position.y; //������Ʈ�� ó�� ���۵Ǵ� ��� ����.
        rb2D.velocity = new Vector2(xVnot, yVnot); //������Ʈ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
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
            //GetComponent<Renderer>().sortingOrder = 0; //������Ʈ�� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //������Ʈ ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //������Ʈ�� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ������Ʈ ������ �ٵ� y�ӵ��� 0���� ���� ���
        {
            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
                Destroy(BloodFloor, 10);
            }

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
