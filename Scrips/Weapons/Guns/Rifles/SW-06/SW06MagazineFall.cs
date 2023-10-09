using UnityEngine;

public class SW06MagazineFall : MonoBehaviour
{
    public int LED; //LED�� ���� ź�������� ���� ����
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
        startYPosition = transform.position.y; //źâ�� ó�� ���۵Ǵ� ��� ����.
        rb2D.velocity = new Vector2(xVnot, yVnot); //źâ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
        offset = Random.Range(-3.5f, -2.1f); //ź�� ���� �����Լ�.

        while (true) //źâ ȸ�� �ӵ� ����
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
        velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. źâ�� ������ �ð� ����.

        if (rb2D.velocity.magnitude <= 0.1f && Throwing == false) //źâ ������ �ٵ��� �ӵ� �Ѱ����� 0.1���� ���� ���
        {
            rb2D.velocity = Vector2.zero; //źâ�� ��� �������� �����.
            GetComponent<Renderer>().sortingLayerName = "Shell"; //ź�ǰ� �ٴڿ� ������ ������ �� �̸� ����.
            GetComponent<Renderer>().sortingOrder = 0; //źâ�� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.
            if (LED == 1)
            {
                LEDOn.GetComponent<Renderer>().sortingOrder = 1;
                LEDOn.GetComponent<Renderer>().sortingLayerName = "Shell";
            }
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //źâ ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //źâ�� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� źȯ ������ �ٵ� y�ӵ��� 0���� ���� ���
        {
            float yVelocity = -rb2D.velocity.y * 0.25f; //źâ�� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� źâ�� �ٴڿ� ���� ó�� ����.
            float xVelocity = rb2D.velocity.x * 0.25f;

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, źâ�� �ӵ��� ���ҽ�Ű�� ó��
            velocityTime = 0;
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }

    //�÷��̾� �̵������� źâ Ƣ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            explosionPosition = collision.transform.position; //������Ʈ ��ġ ȹ��.

            float explosionRadius = 6;
            float explosionForce = 100;
            float upliftModifier = 5;

            AddExplosionForce(explosionRadius, explosionForce, upliftModifier);
        }
    }

    //źâ Ƣ�� ���
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