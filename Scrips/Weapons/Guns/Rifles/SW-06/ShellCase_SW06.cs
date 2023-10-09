using UnityEngine;

public class ShellCase_SW06 : MonoBehaviour
{
    public int LED; //LED�� ���� ź�������� ���� ����
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
        xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom); //ź�� x��ġ ������ �����Լ�.
        yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom); //ź�� y��ġ ������ �����Լ�.
        offset = Random.Range(offsetMinusRandom, offsetMPlusRandom); //ź�� ���� �����Լ�.

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

    //ź�� ���� �޼���
    public void Acceleration()
    {
        velocityTime += Time.fixedDeltaTime; //��Ÿ Ÿ�� ���ؼ� ���. ������Ʈ�� ������ �ð� ����

        if (rb2D.velocity.magnitude <= 0.1f) //������Ʈ ������ �ٵ��� �ӵ� �Ѱ����� 0.5���� ���� ���
        {
            rb2D.velocity = Vector2.zero; //������Ʈ�� ��� �������� �����.
            GetComponent<Renderer>().sortingOrder = 0; //������Ʈ�� �ٴڿ� ������ ������ �� ���̾ �� �ڷ� ����.
            GetComponent<Renderer>().sortingLayerName = "Shell"; //ź�ǰ� �ٴڿ� ������ ������ �� �̸� ����.
            if (LED == 1)
            {
                LEDOn.GetComponent<Renderer>().sortingOrder = 1;
                LEDOn.GetComponent<Renderer>().sortingLayerName = "Shell";
            }

            return;
        }

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime); //������Ʈ ���� ��ȯ�ϴ� ���� �ڼ���, ������ȯ �ӵ� ���

        if (transform.position.y <= startYPosition + offset && rb2D.velocity.y < 0) //������Ʈ�� y��ġ�� ������+�����°Ÿ����� ���ų� ���� ���, Ȥ�� ������Ʈ ������ �ٵ� y�ӵ��� 0���� ���� ���
        {
            float yVelocity = -rb2D.velocity.y * 0.25f; //ź���� x, y�� �ӵ��� ���ҽ�Ű�� ���� ��� ó��. �� �κ��� ���� �߷¾��� ź���� �ٴڿ� ���� ó�� ����.
            float xVelocity = rb2D.velocity.x * 0.25f;

            if (SoundTime == 0)
            {
                SoundTime += Time.deltaTime;
                ShellFallSound();
            }

            rb2D.velocity = new Vector2(xVelocity, yVelocity); //�� ��, ������Ʈ�� �ӵ��� ���ҽ�Ű�� ó��
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

            float explosionRadius = 100;
            float explosionForce = 25;
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