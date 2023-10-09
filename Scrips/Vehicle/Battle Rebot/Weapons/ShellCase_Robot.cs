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
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //ź�� y��ġ ������ �����Լ�.
        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom);

        while (true) //ź�� ȸ�� �ӵ� ����
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
        if (transform.rotation.y == 0)
        {
            velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. ź���� ������ �ð� ����.

            if (rb2D.velocity.magnitude <= 0.1f) //ź�� ������ �ٵ��� �ӵ� �Ѱ����� 0.5���� ���� ���
            {
                rb2D.velocity = Vector2.zero; //ź���� ��� �������� �����.
                GetComponent<Renderer>().sortingLayerName = "Shell"; //ź�ǰ� �ٴڿ� ������ ������ �� �̸� ����.
                GetComponent<Renderer>().sortingOrder = 0; //ź�ǰ� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.

                return;
            }

            transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //ź�� ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //ź���� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ź�� ������ �ٵ� y�ӵ��� 0���� ���� ���
            {
                float yVelocity = -rb2D.velocity.y * 0.3f; //ź���� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ź���� �ٴڿ� ���� ó�� ����.
                float xVelocity = rb2D.velocity.x * 0.3f;

                if (SoundTime == 0)
                {
                    SoundTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay(" Sound", HTACFalling);
                }

                rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ź���� �ӵ��� ���ҽ�Ű�� ó��
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
            velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. ź���� ������ �ð� ����.

            if (rb2D.velocity.magnitude <= 0.1f) //ź�� ������ �ٵ��� �ӵ� �Ѱ����� 0.5���� ���� ���
            {
                rb2D.velocity = Vector2.zero; //ź���� ��� �������� �����.
                GetComponent<Renderer>().sortingLayerName = "Shell"; //ź�ǰ� �ٴڿ� ������ ������ �� �̸� ����.
                GetComponent<Renderer>().sortingOrder = 0; //ź�ǰ� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.

                return;
            }

            transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //ź�� ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

            if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //ź���� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ź�� ������ �ٵ� y�ӵ��� 0���� ���� ���
            {
                float yVelocity = -rb2D.velocity.y * 0.3f; //ź���� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ź���� �ٴڿ� ���� ó�� ����.
                float xVelocity = rb2D.velocity.x * 0.3f;

                if (SoundTime == 0)
                {
                    SoundTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay(" Sound", HTACFalling);
                }

                rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ź���� �ӵ��� ���ҽ�Ű�� ó��
                velocityTime = 0;
            }
            else
            {
                float yVelocity = rb2D.velocity.y + acceleration * velocityTime;
                rb2D.velocity = new Vector2(rb2D.velocity.x, yVelocity);
            }
        }
    }

    //�÷��̾� �̵������� ź�� Ƣ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CapsuleCollider2D)
        {
            explosionPosition = collision.transform.position; //������Ʈ ��ġ ȹ��.

            float explosionRadius = 6;
            float explosionForce = 250;
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