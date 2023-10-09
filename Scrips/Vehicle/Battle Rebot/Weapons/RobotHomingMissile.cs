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

    [SerializeField] float speed = 0f; // 미사일의 최고속도 
    [SerializeField] LayerMask layerMask = 0;// 어떤 레이어를 특정할 것인가
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

    IEnumerator LaunchDelay() //속도가 0이 되었을 때 발사, 생성된 뒤 5초후 삭제 
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

    void Update() //호밍미사일이 추적하는 코드 
    {
        if (target != null && FireStart == true)
        {
            //Debug.Log("추적중");
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
        //칸타크리
        if (collision is CircleCollider2D && collision.gameObject.layer == 13)
        {
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear")) || collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor"))
            {
                //카오티 - 자이오스4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                kaotiJaios4.RicochetNum(1);
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Destroy(gameObject);
                Active();
            }
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
            {
                //타이카 - 라이 - 쓰로트로1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                TaikaLaiThrotro1.RicochetNum(1);
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Destroy(gameObject);
                Active();
            }
            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Destroy(gameObject);
                Active();
            }
        }

        //감염자
        if (collision.gameObject.layer == 16)
        {
            //일반 감염자
            if (collision.CompareTag("Infector, Standard"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    var contactPoint1 = collision.transform.position.x; //충돌체의 위치 좌표
                    Distance = contactPoint1 - transform.position.x; //충돌체 좌표와 폭발 좌표간 거리 계산. 충돌체가 오른쪽에 있으면 플러스 값, 왼쪽에 있으면 마이너스 값이 산출된다.

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector 스크립트에다 타격 정보 전송
                    }

                    gameObject.SetActive(false);
                    Active();
                }
                if (collision is CapsuleCollider2D) //얼굴
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector 스크립트에다 타격 정보 전송
                    }

                    gameObject.SetActive(false);
                    Active();
                }
                if (collision is BoxCollider2D) //다리
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector 스크립트에다 타격 정보 전송
                    }

                    gameObject.SetActive(false);
                    Active();
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨

            gameObject.SetActive(false);
            Active();
        }

        if (collision.gameObject.layer == 8)
        {
            if (collision.CompareTag("AmmoShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
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

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
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