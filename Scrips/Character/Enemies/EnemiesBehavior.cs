using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehavior : MonoBehaviour
{
    public float speed; //기본 속도, 평상시 속도
    public float runningSpeed; //뛰는 속도, 회피하거나 추적할 때 쓰인다.
    private float currnetSpeed; //기본과 뛰는 속도를 전환해주는 역할
    public float Damage; //적의 발사당 피해량
    public float lineOfSite; //최외각선, 플레이어 포착시 배회하기 위한 용도
    public float traceSite; //추적선, 최외각선과 추적선 사이에서 플레이어를 추적시키는 용도
    public float avoidSite; //가장 안쪽에 있는 선, 플레이어가 가까이 왔을 경우, 피하기 위한 용도
    public float directionChangeTime; //배회시 방향 전환까지 걸리는 시간

    private Transform player;
    private Vector2 target;

    public int shootingDelayOnEasyLevel; //난이도별 무기 발포 범위
    private int enemyShoot; //일정확률로 무기 발포
    private int moveDown; //일정확률로 추적하기 위한 랜덤함수

    bool avoid = false;

    public GameObject enemyAmmoPrefab; //발사 총알 프리팹
    public Transform enemyAmmoPos; //총알 생성 좌표

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
    Animator animator;

    float currentAngle = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(yMove());
    }

    //일정시간당 추적
    IEnumerator yMove()
    {
        while (true)
        {
            moveDown = Random.Range(0, 5);
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
        //float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        ////Debug.Log(avoid);
        
        ////일정 반경 내 접근시 배회 시작
        //if(distanceFromPlayer < lineOfSite)
        //{
        //    //추적 영역과 최외각 영역에 있을 경우, 플레이어를 추적 영역까지만 추적
        //    if (distanceFromPlayer > traceSite)
        //    {
        //        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //    }
        //    else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false) //배회 및 Y축 이동
        //    {
        //        currnetSpeed = speed;
        //        Movement();

        //        if(avoid == true)
        //        {
        //            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        //        }
        //    }

        //    //가까이 접근했을 경우, 회피
        //    if (distanceFromPlayer < avoidSite)
        //    {
        //        avoid = true;
        //        StartCoroutine(StopyMove());
        //        StartCoroutine(Avoid());
        //    }
        //    else if(distanceFromPlayer > avoidSite && avoid == true)
        //    {
        //        StartCoroutine(AvoidDuringWander()); //추적하던 적이 회피라인에서 회피를 통해 추적 영역에 다시들어섰을 경우
        //    }
        //}

        ////일정 반경을 이탈시, 배회 정지
        //if (distanceFromPlayer > lineOfSite)
        //{
        //    if (moveCoroutine != null)
        //    {
        //        StopCoroutine(ShootingMovement());
        //        StopCoroutine(Move(rb2d, currnetSpeed));
        //        currnetSpeed = 0;
        //    }

        //    targetTransform = null;
        //}
    }

    //회피
    IEnumerator Avoid()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
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
            StopCoroutine(ShootingMovement());
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            //this.gameObject.AddComponent<EnemiesAI>().yChase(); //Y축 추적중 by EnemiesAI
        }
        else
        {
            StartCoroutine(ShootingMovement());
        }
    }

    //일반 배회
    public IEnumerator ShootingMovement()
    {
        while(true)
        {
            ChooseNewEndPoint();

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
            yield return new WaitForSeconds(directionChangeTime);
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

        while(remainingDistance > float.Epsilon)
        {
            if(targetTransform != null)
            {
                endposition = targetTransform.position;
            }

            //이동
            if(rigidbodyToMove != null)
            {
                //animater.SetBool("", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endposition).sqrMagnitude;

                yield return new WaitForSeconds(directionChangeTime);
            }
        }
        //animater.SetBool("", false);
    }

    //총 발사
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            enemyShoot = Random.Range(1, shootingDelayOnEasyLevel);
            if(enemyShoot == 1)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                GameObject e_ammo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //발사 총알 생성
                //e_ammo.GetComponent<AmmoMovement>().SetDamage(Damage); //총알에다 데미지 전달
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, avoidSite);
        Gizmos.DrawWireSphere(transform.position, traceSite);
    }
}