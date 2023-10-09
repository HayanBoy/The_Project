using UnityEngine;

public class ShellCase_MetalShell : MonoBehaviour
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

    Vector3 explosionPosition;

    void Start()
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

        startYPosition = transform.position.y; //������Ʈ�� ó�� ���۵Ǵ� ��� ����.
        rb2D.velocity = new Vector2(xVnot, yVnot); //������Ʈ�� ������ �ٵ� �ӵ��� x, y�� ���Ͱ� �Է�.
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