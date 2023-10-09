using UnityEngine;

public class VM5Throw : MonoBehaviour
{
    private float startYPosition;
    public float yVnot;
    public float xVnot;

    public float acceleration;
    public Rigidbody2D rb2D;

    public float xvelocityReduction;
    public float yvelocityReduction;
    public float offsetMinusRandom;
    public float offsetMPlusRandom;
    public float rotationSpeed;
    private float offset;
    private float velocityTime;
    int speedpoint = 0;

    int LargeThrow;
    float LargeThrow2;
    public bool Direction;
    public bool Throwing = false;

    Vector3 explosionPosition;

    private float SoundTime;
    public AudioClip GrenadeFall;

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
            speedpoint += 1;

            if (speedpoint >= 1)
            {
                speedpoint = 0;
                break;
            }
        }

        startYPosition = transform.position.y; //������Ʈ�� ó�� ���۵Ǵ� ��� ����.
        rb2D.velocity = new Vector2(xVnot, yVnot); //������Ʈ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
    }

    private void Update()
    {
        if (Throwing == true)
        {
            Throwing = false;

            LargeThrow2 = Random.Range(2, 4);

            xVnot += LargeThrow * LargeThrow2 * 5;
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

            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                SoundManager.instance.SFXPlay26("Sound", GrenadeFall);
            }
        }
        else
        {
            float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
        }
    }

    //�÷��̾� �̵������� ����ź Ƣ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            explosionPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //������Ʈ ��ġ ȹ��.

            float explosionRadius = 6;
            float explosionForce = 250;
            float upliftModifier = 5;

            AddExplosionForce(explosionRadius, explosionForce, upliftModifier);
        }
    }

    //����ź Ƣ�� ���
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