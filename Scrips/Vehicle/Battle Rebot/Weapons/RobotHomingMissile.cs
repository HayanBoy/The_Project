using System.Collections;
using UnityEngine;

public class RobotHomingMissile : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject EffectPrefab1;
    public Transform particlePos;
    GameObject[] Particle;

    public int damage;
    bool Direction;
    private int ThrowRandom;
    private int ThrowRandom2;
    private float Distance;

    Rigidbody2D rigid = null;
    Transform target = null;

    [SerializeField] float speed = 0f; // �̻����� �ְ�ӵ� 
    [SerializeField] LayerMask layerMask = 0;// � ���̾ Ư���� ���ΰ�
    float currentSpeed;

    bool FireStart = false;

    public float StopTime;

    public AudioClip MissileExplosion;

    public void SetDamage(int num)
    {
        damage = num;
    }

    void SearchEnemy()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 100f, layerMask);

        if (cols.Length > 0)
        {
            target = cols[Random.Range(0, cols.Length)].transform;
        }
    }

    IEnumerator LaunchDelay() //�ӵ��� 0�� �Ǿ��� �� �߻�, ������ �� 5���� ���� 
    {
        FireStart = true;

        GameObject MissileFireFlame = Instantiate(EffectPrefab1, particlePos.position, particlePos.rotation);
        Destroy(MissileFireFlame, 5);

        if (currentSpeed == 0)
            SearchEnemy();

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchDelay());

        Particle = new GameObject[1];
        Generate();
    }

    void Update() //ȣ�ֹ̻����� �����ϴ� �ڵ� 
    {
        if (target != null && FireStart == true)
        {
            //Debug.Log("������");
            this.rigid.gravityScale = 0;

            if (currentSpeed <= speed)
                currentSpeed += speed * Time.deltaTime;

            transform.position += transform.up * currentSpeed * Time.deltaTime;

            Vector3 dir = (target.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, dir, 0.1f);
        }
        else if (FireStart == false)
        {
            this.rigid.gravityScale += Time.deltaTime * StopTime;
        }
    }

    void Generate()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            Particle[index] = Instantiate(Prefab);
            Particle[index].SetActive(false);
        }
    }

    public GameObject Loader()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            if (!Particle[index].activeSelf)
            {
                Particle[index].SetActive(true);
                return Particle[index];
            }
        }

        return null;
    }
    void Active()
    {
        SoundManager.instance.SFXPlay("Sound", MissileExplosion);
        GameObject Effect = Loader();
        Effect.transform.position = particlePos.position;
        Effect.transform.rotation = particlePos.rotation;
        Destroy(Effect, 5);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //ĭŸũ��
        if (collision is CircleCollider2D && collision.gameObject.layer == 13)
        {
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear")) || collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor"))
            {
                //ī��Ƽ - ���̿���4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                kaotiJaios4.RicochetNum(1);
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Destroy(gameObject);
                Active();
            }
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
            {
                //Ÿ��ī - ���� - ����Ʈ��1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                TaikaLaiThrotro1.RicochetNum(1);
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Destroy(gameObject);
                Active();
            }
            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //īũ�ν�-Ÿ�����ν� 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Destroy(gameObject);
                Active();
            }
        }

        //������
        if (collision.gameObject.layer == 16)
        {
            //�Ϲ� ������
            if (collision.CompareTag("Infector, Standard"))
            {
                if (collision is CircleCollider2D) //����
                {
                    var contactPoint1 = collision.transform.position.x; //�浹ü�� ��ġ ��ǥ
                    Distance = contactPoint1 - transform.position.x; //�浹ü ��ǥ�� ���� ��ǥ�� �Ÿ� ���. �浹ü�� �����ʿ� ������ �÷��� ��, ���ʿ� ������ ���̳ʽ� ���� ����ȴ�.

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    gameObject.SetActive(false);
                    Active();
                }
                if (collision is CapsuleCollider2D) //��
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    gameObject.SetActive(false);
                    Active();
                }
                if (collision is BoxCollider2D) //�ٸ�
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    gameObject.SetActive(false);
                    Active();
                }
            }
        }

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�

            gameObject.SetActive(false);
            Active();
        }

        if (collision.gameObject.layer == 8)
        {
            if (collision.CompareTag("AmmoShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().Throwing = true;
            }
            if (collision.CompareTag("MagagineShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().Throwing = true;
            }
        }
    }
}