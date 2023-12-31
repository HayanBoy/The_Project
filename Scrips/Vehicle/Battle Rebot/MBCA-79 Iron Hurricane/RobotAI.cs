using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    public float speed; //기본 속도, 평상시 속도
    public float runningSpeed; //뛰는 속도, 회피하거나 추적할 때 쓰인다.
    private float currnetSpeed; //기본과 뛰는 속도를 전환해주는 역할
    public float lineOfSite; //최외각선, 플레이어 포착시 배회하기 위한 용도
    public float traceSite; //추적선, 최외각선과 추적선 사이에서 플레이어를 추적시키는 용도
    public float avoidSite; //가장 안쪽에 있는 선, 플레이어가 가까이 왔을 경우, 피하기 위한 용도

    Transform Enemy = null;
    private Transform Player;
    private Vector2 target;

    //public int Damage;
    public int NBdamage; //적의 발사당 피해량

    public float fireDelay; //최소 발사 간격
    public int shootingDelayOnEasyLevel; //난이도별 무기 발포 범위
    private int enemyShoot; //일정확률로 무기 발포
    private int moveDown; //일정확률로 추적하기 위한 랜덤함수
    private float waitWanderTime; //배회후 정지 지속시간
    private int wantToStop; //배회하다 정지할 확률
    private float TraceStartAcoount; //추적할 시간 정하기, 2022.01.12

    int ammo = 0;
    public int AmmoPerMagazine;

    bool avoid = false;
    bool fired = false;
    bool reloading = false;
    /*bool afterFire = false;*/ bool wandering = false;
    bool Moving = false;
    bool stopGoing = false;
    bool chasing = false;
    bool outOfSide = false;

    public Transform RobotammoPos; //총알 생성 좌표

    public Transform RobotShellPos; //탄피1 좌표
    public GameObject RobotammoPrefab;
    public GameObject RobotShellPrefab;

    GameObject[] Robotammo; //카오티-자이오스 4 총알 및 파편
    GameObject[] RobotShell; // 카오티-자이오스 4 탄피 1
    GameObject[] PoolMaker;

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
  //  Animator animator;

    float currentAngle = 0;

    ObjectManager objectManager;
    Movement movement;

    void SearchEnemy()
    {
       Enemy = GameObject.FindWithTag("Enemy").transform;
    }

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        //objectManager.SupplyList.Add(gameObject);

        //animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //Player = GameObject.FindWithTag("Player").transform;

        //Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
       
        //target = new Vector2(Enemy.position.x, Enemy.position.y);
        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격

        Robotammo = new GameObject[5];
        RobotShell = new GameObject[20];

        Generate();
    }

    private void OnEnable()
    {
        objectManager.SupplyList.Add(gameObject);
    }

    private void OnDisable()
    {
        objectManager.SupplyList.Remove(gameObject);
    }

    void Generate()
    {
        for (int index = 0; index < Robotammo.Length; index++)
        {
            Robotammo[index] = Instantiate(RobotammoPrefab);
            Robotammo[index].SetActive(false);
        }

        for (int index = 0; index < RobotShell.Length; index++)
        {
            RobotShell[index] = Instantiate(RobotShellPrefab);
            RobotShell[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch(type)
        {
            case "Robotammo":
                PoolMaker = Robotammo;
            break;

            case "RobotShell":
                PoolMaker = RobotShell;
            break;
        }

        for (int index = 0; index < PoolMaker.Length; index++)
        {
            if (!PoolMaker[index].activeSelf)
            {
                PoolMaker[index].SetActive(true);
                return PoolMaker[index];
            }
        }
        return null; 
    }


    //랜덤 사격
    IEnumerator RandomFire()
    {
        while(true)
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
            moveDown = Random.Range(0, 2); //랜덤 추격
            yield return new WaitForSeconds(1f);
        }
    }

    ////yMove 멈추기
    //IEnumerator StopyMove()
    //{
    //    StopCoroutine(yMove());
    //    yield return new WaitForSeconds(1f);
    //}

    void Update()
    {

        Debug.Log("플레이어를 보호한다");
        Player = GameObject.FindWithTag("Player").transform;
        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
        //Debug.Log(avoid);

        //Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        //float distanceFromEnemy = Vector2.Distance(Enemy.position, transform.position);

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
                // transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x, Player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
                MovementPlayer();

            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true) //배회 및 Y축 이동
            {
                Debug.Log("씨발");
                currnetSpeed = speed;
                MovementPlayer();
            }
        }

        ////일정 반경을 이탈시, 배회 정지
        //if (distanceFromPlayer > lineOfSite)
        //{
        //    outOfSide = false;
        //    if (moveCoroutine != null)
        //    {
        //        currnetSpeed = 0;

        //        stopGoing = true;
        //        chasing = false;
        //    }

        //    targetTransform = null;
        //}


        //////////////////////////////////////////////////////////

        //if (distanceFromEnemy < lineOfSite)
        //{
        //    outOfSide = true;

        //    //추적 영역과 최외각 영역에 있을 경우, 플레이어를 추적 영역까지만 추적
        //    if (distanceFromEnemy > traceSite)
        //    {
        //        chasing = true;
        //        currnetSpeed = runningSpeed;
        //        // transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);
        //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemy.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
        //    }
        //    else if (distanceFromEnemy < traceSite && distanceFromEnemy > avoidSite && avoid == false && outOfSide == true) //배회 및 Y축 이동
        //    {
        //        currnetSpeed = speed;
        //        MovementEnemy();

        //        if (avoid == true)
        //        {
        //            transform.position = Vector2.MoveTowards(transform.position, Enemy.position, -currnetSpeed * Time.deltaTime);
        //        }
        //    }

        //    //가까이 접근했을 경우, 회피
        //    if (distanceFromEnemy < avoidSite)
        //    {
        //        avoid = true;
        //        chasing = true;
        //        moveDown = 0;
        //        currnetSpeed = runningSpeed;
        //        StartCoroutine(StopyMove());
        //        StartCoroutine(Avoid()); //회피
        //    }
        //    else if (distanceFromEnemy > avoidSite && avoid == true)
        //    {
        //        StartCoroutine(AvoidDuringWander()); //추적하던 적이 회피라인에서 회피를 통해 추적 영역에 다시들어섰을 경우
        //    }
        //}

        ////일정 반경을 이탈시, 배회 정지
        //if (distanceFromEnemy > lineOfSite)
        //{
        //    outOfSide = false;
        //    if (moveCoroutine != null)
        //    {
        //        currnetSpeed = 0;

        //        stopGoing = true;
        //        chasing = false;
        //    }

        //    targetTransform = null;
        //}



        //장전
        if (ammo >= AmmoPerMagazine && reloading == false)
        {
            StartCoroutine(Reload());
            //StartCoroutine(ReloadSmoke());
        }

        //공격시, 플레이어 추적

        ////사격 후 연기
        //if (afterFire == true)
        //{
        //    afterFire = false;
        //    GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //연기 생성
        //    GameObject GunSmoke2 = Instantiate(gunSmoke2Prefab, gunSmoke2Pos.position, gunSmoke2Pos.rotation); //연기 생성
        //    Destroy(GunSmoke, 2);
        //    Destroy(GunSmoke2, 2);
        //}

        SearchEnemy();

        if (Enemy != null)
        {
            Debug.Log("적발견 적에게 접근한다");
            LookAtEnemy(); //플레이어 바라보기

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemy.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동
        }

        else
            return;


        


    }

    ////회피
    //IEnumerator Avoid()
    //{
    //    TraceStartAcoount = Random.Range(0.3f, 1.5f); //2022.01.12 회피
    //    yield return new WaitForSeconds(TraceStartAcoount); //2022.01.12
    //    transform.position = Vector2.MoveTowards(transform.position, Enemy.position, -currnetSpeed * Time.deltaTime);
    //}

    ////추적하던 적이 회피라인에서 회피를 통해 추적 영역에 다시들어섰을 경우
    //IEnumerator AvoidDuringWander()
    //{
    //    yield return new WaitForSeconds(2f);
    //    avoid = false;
    //}

    //배회 및 Y축 이동
    void MovementPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);

        if (moveDown == 1 || moveDown == 2) //플레이어 잠시 추적
        {
            chasing = true;
            currnetSpeed = runningSpeed;
            StopCoroutine(ShootingMovement());
            //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * currnetSpeed);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x * 0.9f, Player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //특정 축으로 이동, 2022.01.12
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
        Debug.Log("일반배회");
        while(true)
        {
            ChooseNewEndPoint();

            if(wandering == true && Moving == false && stopGoing == false)
            {
                Moving = true;
                //Debug.Log("Moving : " + Moving);
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
        if(stopGoing == false)
        {
            rigidbodyToMove.velocity = Vector2.zero; //Debug.Log("Stop");
            stopGoing = true; //Debug.Log("stopGoing : " + stopGoing);
            chasing = false;
        }
    }

    //x축 플레이어 쳐다보기
    void LookAtPlayer()
    {
        if (Player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //transform.Lookat(player.position) 360도 회전으로 플레이어 쳐다보기
    }

    void LookAtEnemy()
    {
        if (Enemy.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //transform.Lookat(player.position) 360도 회전으로 플레이어 쳐다보기
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
            return;

        if (collision is CircleCollider2D && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("적 찾았다 공격해!!!!!!!!!!");

            if (enemyShoot == 1 && fired == false /*&& reloading == false*/)
            {
                Debug.Log("적발견~~~~~~~~ 공격!!!!!!!!!");
                fired = true;
                Attack();
                // StartCoroutine(Attack());
            }
        }
    }

    public void Attack() // ** 공격 함수 위 이넘레이터 수정 버전 
    { 
        GameObject Robotammo = Loader("Robotammo");
        Robotammo.transform.position = RobotammoPos.position;
        Robotammo.transform.rotation = transform.rotation;
        Robotammo.GetComponent<NomalBullet>().SetDamage(NBdamage); //총알에다 데미지 전달

        EjectShell(); //탄피 방출

        Invoke("ani_false", fireDelay);

    }


    void ani_false() // ** Fire 딜레이 함수 
    {
        ammo += 2;
        fired = false;
    }

    //탄피 방출
    void EjectShell() // ** 첫번째 탄피 배출함수 
    {
        //GameObject enemyFrontAmmoShell = Instantiate(enemyFrontAmmoShellPrefab, enemyFrontAmmoShellPos.position, transform.rotation);
       
        GameObject RobotShell = Loader("RobotShell");
        RobotShell.transform.position = RobotShellPos.position;

        //enemyFrontAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

       //  Destroy(enemyFrontAmmoShell, 15.0f); //일정시간후, 떨어진 탄피 삭제
    }



    //장전
    IEnumerator Reload()
    {
        reloading = true;
        Attack();

        yield return new WaitForSeconds(2.8f);
        reloading = false;
        ammo = 0;
    }

    //장전 연기 발생
    //IEnumerator ReloadSmoke()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //연기 생성
    //    GameObject Smoke2 = Instantiate(smoke2Prefab, smoke2Pos.position, smoke2Pos.rotation); //연기 생성
    //    Destroy(Smoke, 2);
    //    Destroy(Smoke2, 2);
    //}

}