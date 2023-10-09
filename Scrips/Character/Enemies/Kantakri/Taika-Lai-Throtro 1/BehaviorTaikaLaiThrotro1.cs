using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTaikaLaiThrotro1 : MonoBehaviour
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

    public GameObject flareAmmoPrefab; //플레어 총알 프리팹
    public Transform flareAmmoPos; //플레어 생성 좌표
    public Transform flareAmmo2Pos; //플레어 생성2 좌표

    public GameObject flareFlamePrefab; //플레어 불꽃생성 좌표
    public Transform flareFlamePos; //플레어 불꽃 좌표

    public GameObject gunSmokePrefab; //사격 연기 프리팹
    public Transform gunSmokePos; //사격 연기 좌표
    
    public GameObject sitePrefab; //랜덤 좌표 프리팹
    /// 
    /// </summary>
    public GameObject skyCranePrefab; //스카이 크레인 프리팹
    public GameObject skyCrane2Prefab; //스카이 크레인 프리팹
   // public Transform skyCranePos; //스카이 크레인 생성 좌표

    public GameObject kaotiJaios4Prefab; // ** 카오티 4 프리팹
    public GameObject KaotiJaios4SpearPrefab;
    public GameObject KaotiJaios4Fleet1389Prefab;
    public GameObject KaotiJaios4DualgunPrefab;
    public GameObject KaotiJaios4ArmorDualgunPrefab;
    public GameObject KaotiJaios4ArmorPrefab;

    GameObject[] Kaotijaios4; // ** 카오티 4 
    GameObject[] Kaotijaios4Spear;
    GameObject[] Kaotijaios4Fleet1389;
    GameObject[] Kaotijaios4Dualgun;
    GameObject[] Kaotijaios4ArmorDualgun;
    GameObject[] Kaotijaios4Armor;
    GameObject[] skyCrane; // ** 카오티 4 
    GameObject[] skyCrane2; // ** 카오티 4 

    /// 
    public EnemyPoolManager EPmanager;
   

    private BoxCollider2D area;
    public int reset;

    GameObject[] flareAmmo;
    GameObject[] flareFlame;
    GameObject[] site;

    GameObject[] PoolMaker;

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
    Animator animator;

    float currentAngle = 0;

    private int RicochetSoundRandom;
    public AudioClip FireSound1;
    public AudioClip Reloading;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        area = GetComponent<BoxCollider2D>();

        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션
        StartCoroutine(SendingRequest()); //아군 증원 요청

        //flareAmmo = new GameObject[3];
        //flareFlame = new GameObject[3];
        //site = new GameObject[6];

        //Kaotijaios4 = new GameObject[6]; // ** 카오티 4  
        //skyCrane = new GameObject[3];
        //skyCrane2 = new GameObject[3];

       // Generate();
    }

    private void OnEnable()
    {

        avoid = false;
        fired = false;
        afterFire = false;
        wandering = false;
        Moving = false;
        stopGoing = false;
        chasing = false;
        outOfSide = false;
        charging = false;
        stopAndFire = false;
        firing = false;
        rotationTime = 0.5f;

        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        area = GetComponent<BoxCollider2D>();

        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션
        StartCoroutine(SendingRequest()); //아군 증원 요청
    }

    void Generate()
    {
        for (int index = 0; index < flareAmmo.Length; index++)
        {
            flareAmmo[index] = Instantiate(flareAmmoPrefab);
            flareAmmo[index].SetActive(false);
        }

        for (int index = 0; index < flareFlame.Length; index++)
        {
            flareFlame[index] = Instantiate(flareFlamePrefab);
            flareFlame[index].SetActive(false);
        }

        for (int index = 0; index < site.Length; index++)
        {
            site[index] = Instantiate(sitePrefab);
            site[index].SetActive(false);
        }

        for (int index = 0; index < Kaotijaios4.Length; index++)
        {
            Kaotijaios4[index] = Instantiate(kaotiJaios4Prefab);
            Kaotijaios4[index].SetActive(false);
        }

        for (int index = 0; index < Kaotijaios4Spear.Length; index++)
        {
            Kaotijaios4Spear[index] = Instantiate(KaotiJaios4SpearPrefab);
            Kaotijaios4Spear[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Fleet1389.Length; index++)
        {
            Kaotijaios4Fleet1389[index] = Instantiate(KaotiJaios4Fleet1389Prefab);
            Kaotijaios4Fleet1389[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Dualgun.Length; index++)
        {
            Kaotijaios4Dualgun[index] = Instantiate(KaotiJaios4DualgunPrefab);
            Kaotijaios4Dualgun[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4ArmorDualgun.Length; index++)
        {
            Kaotijaios4ArmorDualgun[index] = Instantiate(KaotiJaios4ArmorDualgunPrefab);
            Kaotijaios4ArmorDualgun[index].SetActive(false);
        }
        for (int index = 0; index < Kaotijaios4Armor.Length; index++)
        {
            Kaotijaios4Armor[index] = Instantiate(KaotiJaios4ArmorPrefab);
            Kaotijaios4Armor[index].SetActive(false);
        }

            for (int index = 0; index < skyCrane.Length; index++)
        {
            skyCrane[index] = Instantiate(skyCranePrefab);
            skyCrane[index].SetActive(false);
        }

        for (int index = 0; index < skyCrane2.Length; index++)
        {
            skyCrane2[index] = Instantiate(skyCrane2Prefab);
            skyCrane2[index].SetActive(false);
        }

    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "flareAmmo":
                PoolMaker = flareAmmo;
                break;

            case "flareFlame":
                PoolMaker = flareFlame;
                break;

            case "site":
                PoolMaker = site;
                break;

            case "Kaotijaios4":
                PoolMaker = Kaotijaios4;
                break;

            case "Kaotijaios4Spear":
                PoolMaker = Kaotijaios4Spear;
                break;

            case "Kaotijaios4Fleet1389":
                PoolMaker = Kaotijaios4Fleet1389;
                break;

            case "Kaotijaios4Dualgun":
                PoolMaker = Kaotijaios4Dualgun;
                break;

            case "Kaotijaios4ArmorDualgun":
                PoolMaker = Kaotijaios4ArmorDualgun;
                break;

            case "Kaotijaios4Armor":
                PoolMaker = Kaotijaios4Armor;
                break;

            case "skyCrane":
                PoolMaker = skyCrane;
                break;

            case "skyCrane2":
                PoolMaker = skyCrane2;
                break;
        }

        for(int index = 0; index < PoolMaker.Length; index++)
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

    //아군 증원 요청
    IEnumerator SendingRequest()
    {
        GameObject FlareFlame;
        //yield return new WaitForSeconds(10);

        if (charging == true || stopAndFire == true) //사격 중인 경우
        {
            while (true)
            {
                yield return new WaitForSeconds(1); //사격이 끝날 때까지 대기

                if(charging == true || stopAndFire == true)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        fired = true;
        animator.SetBool("Preparing fire flare, Taika-Lai-Throtro 1", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Preparing fire flare, Taika-Lai-Throtro 1", false);
        animator.SetBool("Fire flare, Taika-Lai-Throtro 1", true);
        // FlareFlame = Instantiate(flareFlamePrefab, flareFlamePos.position, transform.rotation);
        FlareFlame = EPmanager.Loader("flareFlame"); 
        FlareFlame.transform.position = flareFlamePos.position;
        FlareFlame.transform.rotation = transform.rotation;

       

        //  Destroy(FlareFlame, 2);
        SpawnSite();
        FireFlareEffect();
        yield return new WaitForSeconds(0.1f);
        FireFlareOut();
        yield return new WaitForSeconds(0.23f);

        animator.SetBool("Fire flare, Taika-Lai-Throtro 1", false);
        animator.SetBool("Fire flare2, Taika-Lai-Throtro 1", true);
        //FlareFlame = Instantiate(flareFlamePrefab, flareFlamePos.position, transform.rotation);
        FlareFlame = EPmanager.Loader("flareFlame");
        FlareFlame.transform.position = flareFlamePos.position;
        FlareFlame.transform.rotation = transform.rotation;

       // Destroy(FlareFlame, 2);
        FireFlareEffect();
        yield return new WaitForSeconds(0.1f);
        FireFlareOut();
        yield return new WaitForSeconds(0.23f);

        animator.SetBool("Fire flare2, Taika-Lai-Throtro 1", false);
        animator.SetBool("Fire flare3, Taika-Lai-Throtro 1", true);

        //FlareFlame = Instantiate(flareFlamePrefab, flareFlamePos.position, transform.rotation);
        FlareFlame = EPmanager.Loader("flareFlame");
        FlareFlame.transform.position = flareFlamePos.position;
        FlareFlame.transform.rotation = transform.rotation;
       // Destroy(FlareFlame, 2);

        FireFlareEffect();
        yield return new WaitForSeconds(0.1f);
        FireFlareOut();
        yield return new WaitForSeconds(0.23f);

        animator.SetBool("Fire flare3, Taika-Lai-Throtro 1", false);
        animator.SetBool("Fire flare end, Taika-Lai-Throtro 1", true);
        fired = false;
    }

    //랜덤 좌표
    void SpawnSite()
    {
        int spawn = Random.Range(1, 3);
        for (int i = 0; i < spawn; i++)
        {
            GameObject Site;
            Vector3 basePosition = transform.position;
            Vector3 size = area.size;

            float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
            float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
            float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

            Vector3 spawnPos = new Vector3(posX, posY, posZ);

         //   Site = Instantiate(sitePrefab, spawnPos, Quaternion.identity);
            Site = EPmanager.Loader("site");
            Site.transform.position = spawnPos;
            Site.transform.rotation = Quaternion.identity;

            RandomSite RAN = Site.GetComponent<RandomSite>();
        }
    }

    //랜덤 좌표 리셋
    public void ResetSite()
    {
        if(reset == 1)
        {
            reset = 0;
            GameObject Site;
            Vector3 basePosition = transform.position;
            Vector3 size = area.size;

            float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
            float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
            float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

            Vector3 spawnPos = new Vector3(posX, posY, posZ);

            //Site = Instantiate(sitePrefab, spawnPos, Quaternion.identity);
            Site = EPmanager.Loader("site");
            Site.transform.position = spawnPos;
            Site.transform.rotation = Quaternion.identity;
            //area.enabled = false; //해당 콜라이더의 체크박스를 해제
        }
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //Debug.Log(avoid);

        LookAtPlayer(); //플레이어 바라보기
        ResetSite(); //랜덤 좌표 리셋

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
        if(stopAndFire == true)
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
        if (firing == false)
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

        charging = true;
        animator.SetBool("Charging, Taika-Lai-Throtro 1", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Charging, Taika-Lai-Throtro 1", false);
        animator.SetBool("Charging2, Taika-Lai-Throtro 1", true);
        yield return new WaitForSeconds(2f);
        charging = false;
        stopAndFire = true;
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("Charging2, Taika-Lai-Throtro 1", false);
        animator.SetBool("Fire, Taika-Lai-Throtro 1", true);
        FireEffect(); //사격 효과
        SoundManager.instance.SFXPlay("Sound", FireSound1);
        RailGunAmmo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //발사 총알 생성
        RailGunAmmo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(Damage); //총알에다 데미지 전달
        afterFire = true;
        yield return new WaitForSeconds(2.333f);

        animator.SetBool("Fire, Taika-Lai-Throtro 1", false);
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
            rotationTime += Time.deltaTime / 2;
            animator.SetFloat("Weapon rotation speed", rotationTime);
        }
        else
        {
            rotationTime = 0.5f;
            animator.SetFloat("Weapon rotation speed", 0.5f);
        }
    }

    //사격 효과
    void FireEffect()
    {
        fireEffect = Random.Range(1, 3);

        if (fireEffect == 1)
        {
            animator.SetBool("Fire effect1, Taika-Lai-Throtro 1", true);
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("Fire effect2, Taika-Lai-Throtro 1", true);
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("Fire effect3, Taika-Lai-Throtro 1", true);
        }
    }

    //사격 정지
    void FireOut()
    {
        if (animator.GetBool("Fire effect1, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect1, Taika-Lai-Throtro 1", false);
        if (animator.GetBool("Fire effect2, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect2, Taika-Lai-Throtro 1", false);
        if (animator.GetBool("Fire effect3, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect3, Taika-Lai-Throtro 1", false);
    }

    //사격 플레어 효과
    void FireFlareEffect()
    {
        GameObject FlareAmmo;
        GameObject FlareAmmo2;
        fireEffect = Random.Range(1, 4);

        if (fireEffect == 1)
        {
            animator.SetBool("Fire flare effect1, Taika-Lai-Throtro 1", true);
            //FlareAmmo = Instantiate(flareAmmoPrefab, flareAmmoPos.position, transform.rotation); //발사 총알 생성
            FlareAmmo = EPmanager.Loader("flareAmmo");
            FlareAmmo.transform.position = flareAmmoPos.position;
            FlareAmmo.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("Fire flare effect1-1, Taika-Lai-Throtro 1", true);
            //  FlareAmmo2 = Instantiate(flareAmmoPrefab, flareAmmo2Pos.position, transform.rotation); //발사 총알2 생성
            FlareAmmo2 = EPmanager.Loader("flareAmmo");
            FlareAmmo2.transform.position = flareAmmo2Pos.position;
            FlareAmmo2.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("Fire flare effect2, Taika-Lai-Throtro 1", true);
            //FlareAmmo = Instantiate(flareAmmoPrefab, flareAmmoPos.position, transform.rotation); //발사 총알 생성
            FlareAmmo = EPmanager.Loader("flareAmmo");
            FlareAmmo.transform.position = flareAmmoPos.position;
            FlareAmmo.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 4)
        {
            animator.SetBool("Fire flare effect2-1, Taika-Lai-Throtro 1", true);
          //  FlareAmmo2 = Instantiate(flareAmmoPrefab, flareAmmo2Pos.position, transform.rotation); //발사 총알2 생성
            FlareAmmo2 = EPmanager.Loader("flareAmmo");
            FlareAmmo2.transform.position = flareAmmo2Pos.position;
            FlareAmmo2.transform.rotation = transform.rotation;
        }
    }

    //사격 플레어 정지
    void FireFlareOut()
    {
        if(animator.GetBool("Fire flare effect1, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire flare effect1, Taika-Lai-Throtro 1", false);
        else if (animator.GetBool("Fire flare effect1-1, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire flare effect1-1, Taika-Lai-Throtro 1", false);
        else if (animator.GetBool("Fire flare effect2, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire flare effect2, Taika-Lai-Throtro 1", false);
        else
            animator.SetBool("Fire flare effect2-1, Taika-Lai-Throtro 1", false);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, avoidSite);
        Gizmos.DrawWireSphere(transform.position, traceSite);
    }*/
}