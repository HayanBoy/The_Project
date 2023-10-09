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
            //TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
            //tearAsoShiioshare.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
            //tearAsoShiioshare.SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
            //tearAsoShiioshare.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
            Player Player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
            Player.RicochetNum(1);
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
        }

        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player 스크립트 불러오기
            RP.RicochetNum(1);
            StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
        }
    }
}
