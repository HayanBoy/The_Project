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
        xVnot = Random.Range(-5, 5); //ź�� x��ġ ������ �����Լ�.
        yVnot = Random.Range(-5, 5); //ź�� y��ġ ������ �����Լ�.
        offset = Random.Range(-2f, -3f); //ź�� ���� �����Լ�.

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

        while (true) //ź�� ȸ�� �ӵ� ����
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

        rb2D.velocity = new Vector2(xVnot, yVnot); //ź���� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
    }

    private void OnDisable()
    {
        GetComponent<Renderer>().sortingOrder = 8;
        GetComponent<Renderer>().sortingLayerName = "Charactors";
        velocityTime = 0;
    }

    private void Update()
    {
        startYPosition = Pos; //ź�ǰ� ó�� ���۵Ǵ� ��� ����.

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
        Acceleration(); //ź�� ���� �޼���
    }

    //ź�� ���� �޼���
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. ź���� ������ �ð� ����.

        if (rb2D.velocity.magnitude <= 0.1f) //ź�� ������ �ٵ��� �ӵ� �Ѱ����� 0.5���� ���� ���
        {
            rb2D.velocity = Vector2.zero; //ź���� ��� �������� �����.
            GetComponent<Renderer>().sortingLayerName = "Shell";
            GetComponent<Renderer>().sortingOrder = 0; //ź�ǰ� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.
            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //ź�� ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //ź���� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ź�� ������ �ٵ� y�ӵ��� 0���� ���� ���
        {
            float yVelocity = -rb2D.velocity.y * 0.25f; //ź���� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ź���� �ٴڿ� ���� ó�� ����.
            float xVelocity = rb2D.velocity.x * 0.25f;

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ź���� �ӵ��� ���ҽ�Ű�� ó��
            velocityTime = 0;
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }

    //�÷��̾� �̵������� ź�� Ƣ��
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

    //ź�� Ƣ�� ���
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