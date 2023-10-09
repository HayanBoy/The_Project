using System.Collections;
using UnityEngine;

public class MissileRoot : MonoBehaviour
{
    [SerializeField]
    public Transform target = null;
    Rigidbody2D rigid = null;

    public float FireBase; //플레이어 혹은 탑승차량에서 발사하는지의 여부. 플레이어의 경우 0.4으로 처리. 탑승차량의 경우 0 처리
    public int MissileExplosionType; //미사일에 따라서 폭발 이펙트가 다르도록 처리
    public GameObject EffectPrefab1;
    public Transform particlePos;

    ObjectManager objectManager;
    public GameObject fireEffect;
    public GameObject fireEffect2;

    public int damage;

    [SerializeField] float speed = 0f; // 미사일의 최고속도 
    [SerializeField] LayerMask layerMask = 0;// 어떤 레이어를 특정할 것인가
    float currentSpeed;

    bool FireStart = false;
    bool Direction;
    private int ThrowRandom;
    private float Distance;
    public float range;

    public float StopTime;

    public AudioClip MissileExplosion;

    public void SetDamage(int num)
    {
        damage = num;
    }

    void UpdateTarget()
    {
        if (target == null || target.gameObject.activeSelf == false)
        {
            Collider2D[] Monsters = Physics2D.OverlapCircleAll(transform.position, 100F, layerMask);
            float shortestDistance = Mathf.Infinity;
            Collider2D nearestMonster = null;
            foreach (Collider2D Monster in Monsters)
            {
                float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position);

                if (DistanceToMonsters < shortestDistance)
                {
                    shortestDistance = DistanceToMonsters;
                    nearestMonster = Monster;
                }
            }

            if (nearestMonster != null && shortestDistance <= range)
            {
                target = nearestMonster.transform;
            }

            else
            {
                target = null;
            }
        }
    }

    IEnumerator LaunchDelay() //속도가 0이 되었을 때 발사, 생성된 뒤 5초후 삭제 
    {
        yield return new WaitForSeconds(FireBase);
        FireStart = true;

        GameObject MissileFireFlame = Instantiate(EffectPrefab1, particlePos.position, particlePos.rotation);
        Destroy(MissileFireFlame, 5);
    }
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        rigid = GetComponent<Rigidbody2D>();
        this.rigid.gravityScale = -15;
        StartCoroutine(LaunchDelay());
    }

    private void OnEnable()
    {
        FireStart = false;
        currentSpeed = 0;
        this.rigid.gravityScale = -15;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(LaunchDelay());
    }

    private void OnDisable()
    {
        target = null;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;
    }

    void Update() //호밍미사일이 추적하는 코드 
    {
        UpdateTarget();

        if (target != null && FireStart == true)
        {
            this.rigid.gravityScale = 0;

            if (currentSpeed <= speed)
                currentSpeed += speed * Time.deltaTime;

            transform.position += transform.up * currentSpeed * Time.deltaTime;

            Vector3 dir = (target.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, dir, 6 * Time.deltaTime);
        }
        else if(FireStart == false)
        {
            this.rigid.gravityScale += Time.deltaTime * StopTime;
        }
    }

    void Active()
    {
        fireEffect.GetComponent<FireEffect>().StopEffect();
        fireEffect2.GetComponent<FireEffect2>().StopEffect();
        SoundManager.instance.SFXPlay("Sound", MissileExplosion);

        if(MissileExplosionType == 1)
        {
            GameObject MissileExplosionEffect = objectManager.Loader("OSEH302Explosion");
            MissileExplosionEffect.transform.position = particlePos.transform.position;
            MissileExplosionEffect.transform.rotation = particlePos.transform.rotation;
            VM5Damage VM5Damage = MissileExplosionEffect.gameObject.transform.GetComponent<VM5Damage>();
            VM5Damage.damage = damage;
        }
        else if (MissileExplosionType == 2)
        {
            GameObject MissileExplosionEffect2 = objectManager.Loader("OSEH302Explosion2");
            MissileExplosionEffect2.transform.position = particlePos.transform.position;
            MissileExplosionEffect2.transform.rotation = particlePos.transform.rotation;
            VM5Damage VM5Damage2 = MissileExplosionEffect2.gameObject.transform.GetComponent<VM5Damage>();
            VM5Damage2.damage = damage;
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //칸타크리
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 27)
        {
            //카오티-자이오스4, 카오티-자이오스4 가시형, 카오티-자이오스4 강화형, 카오티-자이오스4 듀얼형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                {
                    Active();
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                {
                    Active();
                }
            }
            //카오티-자이오스4 방패형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                {
                    Active();
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                {
                    Active();
                }
            }

            //타이카-라이-쓰로트로1
            //몸통
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                {
                    Active();
                }
            }

            //아트로-크로스파 390
            //몸통
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //미사일 발사대
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                    {
                        Active();
                    }
                }
            }
            //다리 및 기관포
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            }
        }

        //감염자
        if (collision.gameObject.layer == 16)
        {
            //일반 감염자
            if (collision.CompareTag("Infector, Body"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
                {
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    Active();
                }
            }
        }

        //슬로리어스
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 27)
        {
            //애이소 시이오셰어(앨리트)
            //몸통
            if (collision.CompareTag("Slorius, Aso Shiioshare body"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                Active();
            }

            if (collision.CompareTag("Shield"))
            {
                Active();
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
            if (collision.CompareTag("AmmoShellHuge"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellCase_Robot>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellCase_Robot>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellCase_Robot>().Throwing = true;
            }
            if (collision.CompareTag("AmmoShelEnemy"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellMovement>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellMovement>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellMovement>().Throwing = true;
            }
            if (collision.CompareTag("Body Part"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("VM5 Throw"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<VM5Throw>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<VM5Throw>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<VM5Throw>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Kaoti-Jaios4"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Taika-Lai-Throtro1"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Sky Crane"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
        }
    }
}