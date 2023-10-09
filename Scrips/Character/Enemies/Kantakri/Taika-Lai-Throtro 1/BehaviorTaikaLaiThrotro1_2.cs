using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTaikaLaiThrotro1_2 : MonoBehaviour
{
    public float speed; //기본 속도, 평상시 속도
    public float runningSpeed; //뛰는 속도, 회피하거나 추적할 때 쓰인다.
    private float currnetSpeed; //기본과 뛰는 속도를 전환해주는 역할
    public float lineOfSite; //최외각선, 플레이어 포착시 배회하기 위한 용도
    public float traceSite; //추적선, 최외각선과 추적선 사이에서 플레이어를 추적시키는 용도
    public float avoidSite; //가장 안쪽에 있는 선, 플레이어가 가까이 왔을 경우, 피하기 위한 용도

    private Transform player;
    private Vector2 target;

    public int Damage; //적의 발사당 피해량
    public float fireDelay; //최소 발사 간격
    public int shootingDelayOnEasyLevel; //난이도별 무기 발포 범위
    private int enemyShoot; //일정확률로 무기 발포
    private int moveDown; //일정확률로 추적하기 위한 랜덤함수
    private float waitWanderTime; //배회후 정지 지속시간
    private int wantToStop; //배회하다 정지할 확률

    int fireEffect;

    bool avoid = false;
    bool fired = false;
    bool afterFire = false;
    bool wandering = false;
    bool Moving = false;
    bool stopGoing = false;
    bool chasing = false;
    bool outOfSide = false;
    bool charging = false;
    bool stopAndFire = false;
    bool firing = false;
    float rotationTime = 0.5f;

    public GameObject enemyAmmoPrefab; //발사 총알 프리팹
    public Transform enemyAmmoPos; //총알 생성 좌표

    public GameObject gunSmokePrefab; //사격 연기 프리팹
    public Transform gunSmokePos; //사격 연기 좌표

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
    Animator animator;

    float currentAngle = 0;

    public AudioClip FireSound1;
    public AudioClip Reloading;

    void Start()
    {

    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션
    }
    //랜덤 사격
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
            moveDown = Random.Range(0, 5); //랜덤 추격
            yield return new WaitForSeconds(1f);
        }
    }

    //yMove 멈추기
    IEnumerator StopyMove()
    {
        StopCoroutine(yMove());
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //Debug.Log(avoid);

        LookAtPlayer(); //플레이어 바라보기

        //일정 반경 내 접근시 배회 시작
        if (distanceFromPlayer < lineOfSite)
        {
            outOfSide = true;

            //추적 영역과 최외각 영역에 있을 경우, 플레이어를 추적 영역까지만 추적
            if (distanceFromPlayer > traceSite)
            {
                chasing = true;
                currnetSpeed = runningSpeed;
                transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true && charging == false && stopAndFire == false) //배회 및 Y축 이동
            {
                currnetSpeed = speed;
                Movement();

                if (avoid == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -currnetSpeed * Time.deltaTime);
                }
            }

            //가까이 접근했을 경우, 회피
            if (distanceFromPlayer < avoidSite)
            {
                avoid = true;
                chasing = true;
                moveDown = 0;
                currnetSpeed = runningSpeed;
                StartCoroutine(StopyMove());
                StartCoroutine(Avoid()); //회피
            }
            else if (distanceFromPlayer > avoidSite && avoid == true)
            {
                StartCoroutine(AvoidDuringWander()); //추적하던 적이 회피라인에서 회피를 통해 추적 영역에 다시들어섰을 경우
            }
        }

        //일정 반경을 이탈시, 배회 정지
        if (distanceFromPlayer > lineOfSite)
        {
            outOfSide = false;
            if (moveCoroutine != null)
            {
                currnetSpeed = 0;

                stopGoing = true;
                chasing = false;
            }

            targetTransform = null;
        }

        WeaponRotation(); //레일 저격총 회전 속도 관리

        //저격시, 플레이어 추적
        if (charging == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
        }

        //사격 직전에 속도 감소
        if (stopAndFire == true)
        {
            currnetSpeed = 0;
        }

        //사격 후 연기
        if (afterFire == true)
        {
            afterFire = false;
            GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //연기 생성
            Destroy(GunSmoke, 3);
        }
    }

    //회피
    IEnumerator Avoid()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -currnetSpeed * Time.deltaTime);
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
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (moveDown == 1 && distanceFromPlayer > avoidSite && avoid == false) //플레이어 잠시 추적
        {
            chasing = true;
            currnetSpeed = runningSpeed;
            StopCoroutine(ShootingMovement());
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * currnetSpeed);
            //this.gameObject.AddComponent<EnemiesAI>().yChase(); //Y축 추적중 by EnemiesAI
        }
        else if (moveDown != 1 && distanceFromPlayer > avoidSite && avoid == false && wandering == false && outOfSide == true)
        {
            wandering = true; //Debug.Log("wandering : " + wandering);
            chasing = true;
            StartCoroutine(ShootingMovement());
        }
    }

    //일반 배회
    public IEnumerator ShootingMovement()
    {
        while (true)
        {
            ChooseNewEndPoint();

            if (wandering == true && Moving == false && stopGoing == false)
            {
                Moving = true;
                //Debug.Log("Moving : " + Moving);
                animator.SetBool("Flying, Taika-Lai-Throtro 1", true);
                moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
                yield return StartCoroutine(Move(rb2d, currnetSpeed));
            }

            wantToStop = Random.Range(0, 5);
            waitWanderTime = Random.Range(0.5f, 3f);

            if (wantToStop == 1)
            {
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

        while (remainingDistance > float.Epsilon && outOfSide == true)
        {
            if (targetTransform != null)
            {
                endposition = targetTransform.position;
            }

            //이동
            if (rigidbodyToMove != null)
            {
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

    //x축 플레이어 쳐다보기
    void LookAtPlayer()
    {
        if(firing == false)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    //전진 및 후진, 정지 애니메이션
    IEnumerator MoveToWard()
    {
        while (true)
        {
            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (v1.x > transform.position.x)
            {
                if (transform.rotation.y == 0 && stopGoing == false || transform.rotation.y == 0 && chasing == true)
                {
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.583f);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", true);
                }
                else
                {
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.566f);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", true);
                }
            }
            else if (v1.x < transform.position.x && stopGoing == false || v1.x < transform.position.x && chasing == true)
            {
                if (transform.rotation.y == 0)
                {
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.566f);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", true);
                }
                else
                {
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.583f);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", true);
                }
            }
            if (stopGoing == true && chasing == false)
            {
                animator.SetBool("Flying, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
            }
        }
    }

    //총 발사
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Player")
        {
            if (enemyShoot == 1 && fired == false)
            {
                fired = true;
                StartCoroutine(Attack()); //사격
            }
        }
    }

    //사격
    IEnumerator Attack()
    {
        GameObject RailGunAmmo;
        LEDRotation call1 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED3").GetComponent<LEDRotation>();
        call1.RotaionLED = 1;
        LED2Rotation call2 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED2").GetComponent<LED2Rotation>();
        call2.RotaionLED = 1;
        LED3Rotation call3 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED1").GetComponent<LED3Rotation>();
        call3.RotaionLED = 1;

        charging = true;

        animator.SetBool("Charge Karrgen-Arite 31", true);
        yield return new WaitForSeconds(4f);
        charging = false;
        stopAndFire = true;
        yield return new WaitForSeconds(1.5f);

        firing = true;
        animator.SetBool("Charge Karrgen-Arite 31", false);
        animator.SetBool("Fire Karrgen-Arite 31", true);
        FireEffect(); //사격 효과
        SoundManager.instance.SFXPlay("Sound", FireSound1);
        RailGunAmmo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //발사 총알 생성
        RailGunAmmo.GetComponent<PlazmaMovementTaikaLaiThrotro1>().SetDamage(Damage); //총알에다 데미지 전달
        afterFire = true;
        yield return new WaitForSeconds(1.5f);

        call1 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED3").GetComponent<LEDRotation>();
        call1.RotaionLED = 0;
        call2 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED2").GetComponent<LED2Rotation>();
        call2.RotaionLED = 0;
        call3 = GameObject.Find("bone_1, Taika-Lai-Throtro 1/Karrgen-Arite 31 body/Karrgen-Arite 31 LED1").GetComponent<LED3Rotation>();
        call3.RotaionLED = 0;

        animator.SetBool("Fire Karrgen-Arite 31", false);
        FireOut();
        enemyShoot = 2;
        stopAndFire = false;
        afterFire = false;
        fired = false;
        firing = false;
    }

    //레일 저격총 회전 속도 관리
    void WeaponRotation()
    {
        if (charging == true || stopAndFire == true)
        {
        }
        else
        {
            rotationTime = 0.5f;
        }
    }

    //사격 효과
    void FireEffect()
    {
        animator.SetBool("Karrgen-Arite 31 effect", true);
    }

    //사격 정지
    void FireOut()
    {
        if (animator.GetBool("Karrgen-Arite 31 effect"))
            animator.SetBool("Karrgen-Arite 31 effect", false);
    }
}