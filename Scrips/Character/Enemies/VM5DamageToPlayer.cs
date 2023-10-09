using System.Collections;
using UnityEngine;

public class VM5DamageToPlayer : MonoBehaviour
{
    private Shake shake;
    public float ShakeStrength;
    public float ShakeTime;

    public int damage;
    float BTime = 0;
    bool Direction;
    private int ThrowRandom;
    private int ThrowRandom2;
    private float Distance;
    public float ExplosionTime;

    public int PlayerThrow;

    public GameObject ShieldDamage;
    public Transform ShieldDamagePos;

    private void Start()
    {
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
    }

    public void SetDamage(int num)
    {
        damage = num;
    }

    private void OnEnable()
    {
        BTime = 0;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        ThrowRandom = Random.Range(5, 10);
        ThrowRandom2 = Random.Range(5, 10);
    }

    void Update()
    {
        if (BTime < ExplosionTime)
        {
            BTime += Time.deltaTime;

            if (BTime >= ExplosionTime)
            {
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Shake.Instance.ShakeCamera(ShakeStrength, ShakeTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player"))
        {
            //collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
            //collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow);
            //TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
            //tearAsoShiioshare.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
            //tearAsoShiioshare.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
            //tearAsoShiioshare.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
            Player Player = collision.gameObject.GetComponent<Player>(); //Player ��ũ��Ʈ �ҷ�����
            Player.RicochetNum(1);
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
        }

        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player ��ũ��Ʈ �ҷ�����
            RP.RicochetNum(1);
            StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
        }
    }
}
