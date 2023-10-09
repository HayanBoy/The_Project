using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    BossHp bossHp;

    public int Damage; // Bullet 발사당 피해량

    public int KingDamage; //KingBullet 발사당 피해량
    public int ShortAttackDamage; //KingBullet 발사당 피해량
    public GameObject ExplosionFire;

    public int damage;

    SpriteRenderer spriterenderer;

    public Transform pos;
    public Vector2 boxSize;

    public int attackdamage;

    bool AttackBool = false;
    bool SwordAcitve = false;

    Transform Enemytarget = null;
    ObjectManager objectManager;

    public float speed; //기본 속도, 평상시 속도
    public float runningSpeed; //뛰는 속도, 회피하거나 추적할 때 쓰인다.
    private float currnetSpeed; //기본과 뛰는 속도를 전환해주는 역할
    public float lineOfSite; //최외각선, 플레이어 포착시 배회하기 위한 용도
    public float traceSite; //추적선, 최외각선과 추적선 사이에서 플레이어를 추적시키는 용도
    public float avoidSite; //가장 안쪽에 있는 선, 플레이어가 가까이 왔을 경우, 피하기 위한 용도

    public float fireDelay; //최소 발사 간격
    private int enemyShoot; //일정확률로 무기 발포
    public int shootingDelayOnEasyLevel; //난이도별 무기 발포 범위

    private int moveDown; //일정확률로 추적하기 위한 랜덤함수
    private float waitWanderTime; //배회후 정지 지속시간
    private int wantToStop; //배회하다 정지할 확률
    private float TraceStartAcoount; //추적할 시간 정하기, 2022.01.12

    public float dashSpeed;
    float activeMoveSpeed;
    public float dashLength = .5f; // 대쉬 길이 
    public float dashCooldown = 0.5f; // 대쉬 쿨
    private float dashCounter;
    private float dashCoolCounter;

    public int DashCount; // 대쉬 카운트 
    public float DashCoolTime; // 대쉬가 채워지는 쿨 
    public float DashTime;

    bool avoid = false;
    bool fired = false;
    bool BasicFire = false;

    /*bool afterFire = false;*/ 
    bool wandering = false;
    bool Moving = false;
    bool stopGoing = false;
    bool chasing = false;
    bool outOfSide = false;

    int VoiceRandom;
    int VoicePrint;
    float VoiceTime;

    bool Skilltime = false;


    float currentAngle = 0;
    Coroutine moveCoroutine;
    float chasingTime;

    Rigidbody2D rb2d;
    Animator animator;

    Vector3 endposition;

    Transform targetTransform = null;

    int ammo = 0;

    public GameObject BossAmmoPrefab;
    public Transform BossAmmoPos;
    public GameObject KingBulletAmmoPrefab;

    public GameObject ArcAmmoPrefab;
    public Transform ArcShellPos;
    int ATTACKCNT;

    int randomSkill;

    public GameObject ShortAttackPrefab;
    public Transform ShortAttackPos;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    float vectorY;
    float vectorY2;

    float angle;
    int NearAttackRandom;

    [SerializeField] private float KBTime;
    public float KBCoolTime; // KingBullet 쿨타임 
    int KBcnt;

    [SerializeField] private float ArcTime;
    public float ArcCoolTime; // KingBullet 쿨타임 
    int Arccnt;
    // Start is called before the first frame update
    bool isDash;
    bool Trigger;

    public AudioClip Voice1;
    public AudioClip Voice2;
    public AudioClip Fire;
    public AudioClip FireSkillBefore;
    public AudioClip FireSkill;
    public AudioClip SwordActive;
    public AudioClip Sword1;

    IEnumerator VoiceSound()
    {
        while (true)
        {
            VoiceRandom = Random.Range(0, 4);
            yield return new WaitForSeconds(5);
        }
    }

    void Start()
    {
        bossHp = FindObjectOfType<BossHp>();

        StartCoroutine(VoiceSound());

        ATTACKCNT = 1;
        vectorY = -1;
        vectorY2 = 1;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire());

        objectManager = FindObjectOfType<ObjectManager>();
       // spriterenderer = GetComponent<SpriteRenderer>();

    }
    private void OnEnable()
    {
        ATTACKCNT = 1;
        vectorY = -1;
        vectorY2 = 1;

        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire());

        objectManager = FindObjectOfType<ObjectManager>();
        //spriterenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator Attack1()
    {
        AttackBool = true;
        SoundManager.instance.SFXPlay28("Sound", Sword1);
        animator.SetBool("Sword Attack1, Kakros-Taijaelos 1389", true);
        yield return new WaitForSeconds(0.46f);

        //Debug.Log("1");
        Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2ds)
        {
            if (collider.tag == "Player" && ATTACKCNT > 0)
            {
                GameObject ShortAttack = Instantiate(ShortAttackPrefab, ShortAttackPos.position, ShortAttackPos.rotation);
                Debug.Log("2");
                ShortAttack.GetComponent<ShortAttack>().SetDamage(ShortAttackDamage); //총알에다 데미지 전달

                //Player player = collider.GetComponent<Player>(); //Player 스크립트 불러오기
                //StartCoroutine(player.DamageCharacter(attackdamage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                
                //spriterenderer.color = Color.red;
                ATTACKCNT--;

                Invoke("attackB", 3F);
            }
        }

        ATTACKCNT++;
        if (ATTACKCNT == 2)
        {
            ATTACKCNT = 1;
            Invoke("attackB", 3F);
        }
        yield return new WaitForSeconds(0.53f);
        animator.SetBool("Sword Attack1, Kakros-Taijaelos 1389", false);
    }

    IEnumerator Attack2()
    {
        AttackBool = true;
        SoundManager.instance.SFXPlay28("Sound", Sword1);
        animator.SetBool("Sword Attack2, Kakros-Taijaelos 1389", true);
        yield return new WaitForSeconds(0.46f);

        //Debug.Log("1");
        Collider2D[] collider2ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2ds)
        {
            if (collider.tag == "Player" && ATTACKCNT > 0)
            {
                GameObject ShortAttack = Instantiate(ShortAttackPrefab, ShortAttackPos.position, ShortAttackPos.rotation);
                Debug.Log("2");
                ShortAttack.GetComponent<ShortAttack>().SetDamage(ShortAttackDamage); //총알에다 데미지 전달

                //Player player = collider.GetComponent<Player>(); //Player 스크립트 불러오기
                //StartCoroutine(player.DamageCharacter(attackdamage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨

                //spriterenderer.color = Color.red;
                ATTACKCNT--;

                Invoke("attackB", 3F);
            }
        }

        ATTACKCNT++;
        if (ATTACKCNT == 2)
        {
            ATTACKCNT = 1;
            Invoke("attackB", 3F);
        }
        yield return new WaitForSeconds(0.53f);
        animator.SetBool("Sword Attack2, Kakros-Taijaelos 1389", false);
    }

    private void attackB()
    {
        AttackBool = false;
    }
  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    void SearchEnemy()
    {
        Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
    }

    void Update()
    {
        Dash_Cool();
        Dash();

        if (VoiceRandom == 0)
        {
            VoicePrint = Random.Range(0, 2);

            if (VoicePrint == 0)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay30("Sound", Voice1);
                }
            }
            else if (VoicePrint == 1)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay29("Sound", Voice2);
                }
            }
        }
        else
        {
            VoiceTime = 0;
        }

        KingBulletCool();
        ArcCool();

        randomSkill = Random.Range(1, 5);
        SearchEnemy();
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션

        if (Skilltime == true)
        {
            rb2d.velocity = Vector2.zero;
            currnetSpeed = 0;
        }

        if (Enemytarget != null && Enemytarget.gameObject.activeSelf == true && moveDown >= 2 && Skilltime == false && bossHp.RealHP > 0)
        {
            LookAtPlayer(); //플레이어 바라보기
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Enemytarget.position.x, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
        }
        else
        {
            if(moveDown >= 2)
            {
                animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
            }
            return;
        }

        float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);
        LookAtPlayer(); //플레이어 바라보기

        chasingTime += Time.deltaTime;
        //일정 반경 내 접근시 배회 시작

        if (distanceFromPlayer < lineOfSite && bossHp.RealHP > 0)
        {
            outOfSide = true;

            //추적 영역과 최외각 영역에 있을 경우, 플레이어를 추적 영역까지만 추적
            if (distanceFromPlayer > traceSite && Skilltime == false && bossHp.RealHP > 0)
            {
                chasing = true;
                currnetSpeed = runningSpeed;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true && Skilltime == false && bossHp.RealHP > 0) //배회 및 Y축 이동
            {
                currnetSpeed = speed;
                Movement();
            }

            //가까이 접근했을 경우, 근접공격
            if (distanceFromPlayer < avoidSite && Skilltime == false)
            {
                //Debug.Log("접근");
                if (SwordAcitve == false)
                    StartCoroutine(SwordActiveOn());

                if (AttackBool == false && SwordAcitve == true)
                {
                    NearAttackRandom = Random.Range(0, 2);
                    if(NearAttackRandom == 0 && Skilltime == false)
                        StartCoroutine(Attack1());
                    else
                        StartCoroutine(Attack2());
                    //Invoke("attackB", 3F);
                }
          
                // avoid = true;

                chasing = true;
                moveDown = 0;
                currnetSpeed = runningSpeed;
                //StartCoroutine(StopyMove());
                //StartCoroutine(Avoid()); //회피
            }
            else if (distanceFromPlayer > avoidSite /*&& avoid == true*/)
            {
                //Debug.Log("접근해제");
            }
        }

       //Debug.Log(Trigger);
        //공격시, 플레이어 추적

        if (Trigger)
        {
            if (enemyShoot == 1 && fired == false)
            {
                fired = true;

                if (KBcnt == 0 && Arccnt == 0 && Skilltime == false && BasicFire == false)
                {
                    StartCoroutine(Bullet_Attack());
                    Invoke("ani_false", fireDelay);
                }

                if (KBcnt >= 1 && Skilltime == false && BasicFire == false)
                {
                    StartCoroutine(KingBullet());
                    KBcnt--;
                    Invoke("ani_false", fireDelay);
                }

                else if (Arccnt >= 1 && Skilltime == false && BasicFire == false)
                {
                    if (randomSkill < 3)
                    {
                        StartCoroutine(Bullet_DownUpAnime());
                        Invoke("Bullet_DownUp", 1.416f);
                        Arccnt--;
                        Invoke("ani_false", fireDelay);
                    }

                    else if (randomSkill >= 3 && Skilltime == false && BasicFire == false)
                    {
                        StartCoroutine(Bullet_UpDownAnime());
                        Invoke("Bullet_UpDown", 1.416f);
                        Arccnt--;
                        Invoke("ani_false", fireDelay);
                    }
                }
            }

            else
            {
                isDash = true;
                Invoke("Dashfalse", 1f);
            }
        }

        if (bossHp.RealHP <= 0)
            currnetSpeed = 0;
    }

    void Dashfalse()
    {
        isDash = false;
    }


    void Dash()
    {
        if (isDash)
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0 && DashCount > 0)
            {
                Debug.Log("대 쉬");
                DashCount--;
                speed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                speed = 3;
                //activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void Dash_Cool() //  대쉬 쿨타임 함수 
    {
        DashTime += Time.deltaTime;
        if (DashTime > DashCoolTime)
        {
            DashCount++;
            DashTime = 0;
            // Debug.Log("대쉬쿨이 찼습니다");

        }
        else if (DashCount >= 3)
        {
            DashTime = 0;
            //Debug.Log("대쉬쿨이 모두 찼습니다");
        }
    }
    IEnumerator RandomFire()
    {
        while (true)
        {
            enemyShoot = Random.Range(1, shootingDelayOnEasyLevel);
            yield return new WaitForSeconds(1f);
        }
    }

    //일정시간당 추적 및 멈추기
    IEnumerator yMove()
    {
        while (true)
        {
            moveDown = Random.Range(0, 6); //랜덤 추격
            yield return new WaitForSeconds(1f);
        }
    }

    //yMove 멈추기
    IEnumerator StopyMove()
    {
        StopCoroutine(yMove());
        yield return new WaitForSeconds(1f);
    }

    //회피
    IEnumerator Avoid()
    {
        TraceStartAcoount = Random.Range(0.3f, 1.5f); //2022.01.12 회피
        yield return new WaitForSeconds(TraceStartAcoount); //2022.01.12
        transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, -currnetSpeed * Time.deltaTime);
    }

    //추적하던 적이 회피라인에서 회피를 통해 추적 영역에 다시들어섰을 경우
    IEnumerator AvoidDuringWander()
    {
        yield return new WaitForSeconds(2f);
        avoid = false;
    }

    //배회 및 Y축 이동
    void Movement()
    {
        if (Skilltime == false)
        {
            float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

            if (moveDown == 1 || moveDown == 2 && distanceFromPlayer > avoidSite && avoid == false && Skilltime == false && bossHp.RealHP > 0) //플레이어 잠시 추적
            {
                chasing = true;
                currnetSpeed = runningSpeed;

                if(Skilltime == true)
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
                    currnetSpeed = 0;
                }
                StopCoroutine(ShootingMovement());
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Enemytarget.position.x * 0.5f, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동, 2022.01.12
            }
            else if (moveDown != 1 && distanceFromPlayer > avoidSite && avoid == false && wandering == false && outOfSide == true && Skilltime == false && bossHp.RealHP > 0)
            {
                wandering = true; //Debug.Log("wandering : " + wandering);
                chasing = true;

                if (Skilltime == true)
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
                    currnetSpeed = 0;
                }

                StartCoroutine(ShootingMovement());
            }
        }

    }

    //일반 배회
    public IEnumerator ShootingMovement()
    {
        while (true)
        {
            ChooseNewEndPoint();

            if (wandering == true && Moving == false && stopGoing == false && Skilltime == false)
            {
                if (Skilltime == true)
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
                    currnetSpeed = 0;
                }

                Moving = true;
                if(Skilltime == false && bossHp.RealHP > 0)
                {
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", true);
                }
                else
                {
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
                }
                moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
                yield return StartCoroutine(Move(rb2d, currnetSpeed));
            }

            if (Skilltime == true)
            {
                currnetSpeed = 0;
            }

            wantToStop = Random.Range(0, 1);
            waitWanderTime = Random.Range(0.1f, 0.5f);

            if (wantToStop == 1 && Skilltime == false)
            {
                if (Skilltime == true)
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
                    currnetSpeed = 0;
                }

                yield return new WaitForSeconds(waitWanderTime);
                wandering = false; //Debug.Log("wandering : " + wandering);
                Moving = false;
                stopGoing = false;
                break;
            }
            else
            {
                //Debug.Log("Moving to Continue");
                Moving = false;
                stopGoing = false;
                continue;
            }

            if(bossHp.RealHP <= 0)
            {
                break;
            }
        }
    }

    //목표 각도 선택
    public void ChooseNewEndPoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endposition += Vector3FromAngle(currentAngle);
    }

    //각도 결정
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    //각도를 통한 움직임 구현
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endposition).sqrMagnitude;

        while (remainingDistance > float.Epsilon && outOfSide == true && bossHp.RealHP > 0)
        {
            if (targetTransform != null)
            {
                endposition = targetTransform.position;
            }

            //이동
            if (rigidbodyToMove != null && Skilltime == false && bossHp.RealHP > 0)
            {
                if (Skilltime == true)
                {
                    currnetSpeed = 0;
                }

                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);

                if (newPosition == endposition || transform.position == endposition) //목표지점이 이전 지점과 동일한 경우, 제자리에서 멈춘채로 이동 애니메이션이 활성화되는 영역을 감지
                {
                    //Debug.Log(string.Format("newPosition : {0}, endposition : {1}, rigidbodyToMove.position : {2}", newPosition, endposition, rigidbodyToMove.position));
                    break;
                }

                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endposition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        if (stopGoing == false)
        {
            rigidbodyToMove.velocity = Vector2.zero; //Debug.Log("Stop");
            stopGoing = true; //Debug.Log("stopGoing : " + stopGoing);
            chasing = false;
        }
    }

    //전진 및 후진, 정지 애니메이션
    IEnumerator MoveToWard()
    {
        while (true)
        {
            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (v1.x > transform.position.x && Skilltime == false && bossHp.RealHP > 0)
            {
                if (transform.rotation.y == 0 && stopGoing == false || transform.rotation.y == 0 && chasing == true)
                {
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move, Kakros-Taijaelos 1389", true);
                }
                else
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", true);
                }
            }
            else if (v1.x < transform.position.x && stopGoing == false && Skilltime == false && bossHp.RealHP > 0 || v1.x < transform.position.x && chasing == true && Skilltime == false && bossHp.RealHP > 0)
            {
                if (transform.rotation.y == 0)
                {
                    animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", true);
                }
                else
                {
                    animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                    animator.SetBool("Move, Kakros-Taijaelos 1389", true);
                }
            }
            if (stopGoing == true && chasing == false)
            {
                animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
            }
            if(Skilltime == true && bossHp.RealHP > 0)
            {
                animator.SetBool("Move, Kakros-Taijaelos 1389", false);
                animator.SetBool("Moveback, Kakros-Taijaelos 1389", false);
                animator.SetBool("Move Top, Kakros-Taijaelos 1389", false);
            }
            if (bossHp.RealHP <= 0)
            {
                break;
            }
        }
    }

        //x축 플레이어 쳐다보기
        void LookAtPlayer()
    {
        if (Enemytarget.gameObject.activeSelf == true)
        {
            if (Enemytarget.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    void KingBulletCool() // KingBullet 쿨타임 함수 
    {
        KBTime += Time.deltaTime;
        if (KBTime > KBCoolTime)
        {
            KBcnt++;
            KBTime = 0;
        }
        else if (KBcnt >= 1)
        {
            KBTime = 0;
        }
    }

    void ArcCool() //  ArcCool 쿨타임 함수 
    {
        ArcTime += Time.deltaTime;
        if (ArcTime > ArcCoolTime)
        {
            Arccnt++;
            ArcTime = 0;
        }
        else if (Arccnt >= 1)
        {
            ArcTime = 0;
        }
    }

    void ArcCool111() //  폭탄 쿨타임 함수 
    {
        KBTime += Time.deltaTime;
        if (KBTime > KBCoolTime)
        {
            KBcnt++;
            KBTime = 0;
        }
        else if (KBcnt >= 1)
        {
            KBTime = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player") || collision is CircleCollider2D && collision.CompareTag("Player") || collision is BoxCollider2D && collision.gameObject.layer == 6)
        {
            //Debug.Log("???");
            Trigger = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player")/* || collision is CircleCollider2D && collision.CompareTag("Player") || collision is BoxCollider2D && collision.gameObject.layer == 6*/)
        {
            Trigger = false;

        }
    }

    public IEnumerator Bullet_Attack() // ** 공격 함수 위 이넘레이터 수정 버전 
    {
        BasicFire = true;
        SoundManager.instance.SFXPlay14("Sound", Fire);
        animator.SetBool("Attack, Kakros-Taijaelos 1389", true);
        GameObject Ex = Instantiate(ExplosionFire, pos.transform.position, pos.transform.rotation);
        Destroy(Ex, 5);
        GameObject FrontAmmo = Instantiate(BossAmmoPrefab, BossAmmoPos.position, transform.rotation);
        FrontAmmo.GetComponent<BossAmmo>().SetDamage(Damage); //총알에다 데미지 전달
        FireAni_false();
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack, Kakros-Taijaelos 1389", false);
        BasicFire = false;
    }

    void FireAni_false() // ** Fire 애니메이션 false 함수 
    {
        Invoke("ani_false", fireDelay);
    }

    void ani_false() // ** Fire 딜레이 함수 
    {
        ammo += 2;
        fired = false;
    }

    public IEnumerator KingBullet() // ** 공격 함수 위 이넘레이터 수정 버전 
    {
        //Debug.Log("킹");
        BasicFire = true;
        SoundManager.instance.SFXPlay14("Sound", Fire);
        animator.SetBool("Attack, Kakros-Taijaelos 1389", true);
        GameObject Ex = Instantiate(ExplosionFire, pos.transform.position, pos.transform.rotation);
        Destroy(Ex, 5);
        GameObject KingBullet = Instantiate(KingBulletAmmoPrefab, BossAmmoPos.position, transform.rotation);
        KingBullet.GetComponent<KingBulletAmmo>().SetDamage(Damage); //총알에다 데미지 전달
        FireAni_false();
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack, Kakros-Taijaelos 1389", false);
        BasicFire = false;
    }

    public void Bullet_DownUp() // ** 공격 함수 위 이넘레이터 수정 버전 
    {
        //Debug.Log(vectorY);
        SoundManager.instance.SFXPlay24("Sound", FireSkill);
        GameObject Ex = Instantiate(ExplosionFire, pos.transform.position, pos.transform.rotation);
        Destroy(Ex, 5);
        GameObject FrontAmmo = Instantiate(ArcAmmoPrefab, ArcShellPos.position, Quaternion.identity);
        FrontAmmo.GetComponent<ArcAmmo>().SetDamage(Damage); //총알에다 데미지 전달

        Rigidbody2D rigid = FrontAmmo.GetComponent<Rigidbody2D>();

        float radians;
        radians = angle * 360 * Mathf.PI / 2;

        float x = Mathf.Cos(radians);
        //float y = Mathf.Sin(radians);

        if (transform.rotation.y == 0)
        {
            Vector2 dirVec = new Vector2(-x, vectorY);
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 dirVec = new Vector2(x, vectorY);
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        curPatternCount++;
        vectorY += 0.4F;

        if (vectorY >= 1.0F)
            vectorY = -1;

        if (curPatternCount <= maxPatternCount[patternIndex])
        {
            Invoke("Bullet_DownUp", 0.3f);
        }
    }
    IEnumerator Bullet_DownUpAnime()
    {
        Skilltime = true;
        SoundManager.instance.SFXPlay24("Sound", FireSkillBefore);
        animator.SetBool("Attack roll, Kakros-Taijaelos 1389", true);
        animator.SetBool("Attack Down Skill, Kakros-Taijaelos 1389", true);
        yield return new WaitForSeconds(1.416f);
        yield return new WaitForSeconds(2.42f);
        animator.SetBool("Attack roll, Kakros-Taijaelos 1389", false);
        animator.SetBool("Attack Down Skill, Kakros-Taijaelos 1389", false);
        Skilltime = false;
        curPatternCount = 0;
    }

    public void Bullet_UpDown()
    {
        //Debug.Log(vectorY);
        SoundManager.instance.SFXPlay24("Sound", FireSkill);
        GameObject Ex = Instantiate(ExplosionFire, pos.transform.position, pos.transform.rotation);
        Destroy(Ex, 5);
        GameObject FrontAmmo = Instantiate(ArcAmmoPrefab, ArcShellPos.position, Quaternion.identity);
        FrontAmmo.GetComponent<ArcAmmo>().SetDamage(Damage); //총알에다 데미지 전달

        Rigidbody2D rigid = FrontAmmo.GetComponent<Rigidbody2D>();

        float radians;
        radians = angle * 360 * Mathf.PI / 2;

        float x = Mathf.Cos(radians);
        //float y = Mathf.Sin(radians);

        if (transform.rotation.y == 0)
        {
            Vector2 dirVec = new Vector2(-x, vectorY2);
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 dirVec = new Vector2(x, vectorY2);
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        curPatternCount++;
        vectorY2 -= 0.4F;

        if (vectorY2 <= -1.0F)
            vectorY2 = 1;

        if (curPatternCount <= maxPatternCount[patternIndex])
        {
            Invoke("Bullet_UpDown", 0.3f);
        }
    }
    IEnumerator Bullet_UpDownAnime()
    {
        Skilltime = true;
        SoundManager.instance.SFXPlay24("Sound", FireSkillBefore);
        animator.SetBool("Attack roll, Kakros-Taijaelos 1389", true);
        animator.SetBool("Attack Up Skill, Kakros-Taijaelos 1389", true);
        yield return new WaitForSeconds(1.416f);
        yield return new WaitForSeconds(2.42f);
        animator.SetBool("Attack roll, Kakros-Taijaelos 1389", false);
        animator.SetBool("Attack Up Skill, Kakros-Taijaelos 1389", false);
        Skilltime = false;
        curPatternCount = 0;
    }

    IEnumerator SwordActiveOn()
    {
        yield return new WaitForSeconds(0.16f);
        SoundManager.instance.SFXPlay32("Sound", SwordActive);
        animator.SetBool("Sword Active, Kakros-Taijaelos 1389", true);
        yield return new WaitForSeconds(0.423f);
        animator.SetBool("Sword Active, Kakros-Taijaelos 1389", false);
        animator.SetBool("Sword on, Kakros-Taijaelos 1389", true);
        SwordAcitve = true;
    }
}
