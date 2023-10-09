using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBehaviourKaotiJaios4 : MonoBehaviour
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
    public int damage;

    public float fireDelay; //최소 발사 간격
    public int shootingDelayOnEasyLevel; //난이도별 무기 발포 범위
    private int enemyShoot; //일정확률로 무기 발포
    private int moveDown; //일정확률로 추적하기 위한 랜덤함수
    private float waitWanderTime; //배회후 정지 지속시간
    private int wantToStop; //배회하다 정지할 확률

    int fireEffect;
    int ammo = 0;
    public int AmmoPerMagazine;

    bool avoid = false;
    bool fired = false;
    bool reloading = false;
    bool afterFire = false;
    bool wandering = false;
    bool Moving = false;
    bool stopGoing = false;
    bool chasing = false;
    bool outOfSide = false;

    // public GameObject enemyFrontAmmoPrefab; //발사 총알 프리팹


    string enemyFrontAmmoPrefab = "KaotiJaios4Ammo";
    string enemyFrontAmmoPrefab2 = "KaotiJaios4Ammo3";

    public Transform enemyFrontAmmoPos; //총알 생성 좌표
    public Transform enemyFrontAmmoPos2; //총알 생성 좌표

    // public GameObject enemyBackAmmoPrefab; //발사 총알2 프리팹
    string enemyBackAmmoPrefab = "KaotiJaios4Ammo2";
    string enemyBackAmmoPrefab2 = "KaotiJaios4Ammo4";

    public Transform enemyBackAmmoPos; //총알2 생성 좌표
    public Transform enemyBackAmmoPos2; //총알2 생성 좌표


    // public GameObject enemyFrontAmmoShellPrefab; //탄피1 프리팹

    string enemyFrontAmmoShellPrefab = "KaotiJaios4Shell1"; //탄피1 프리팹


    public Transform enemyFrontAmmoShellPos; //탄피1 좌표

    // public GameObject enemyBackAmmoShellPrefab; //탄피2 프리팹
    string enemyBackAmmoShellPrefab = "KaotiJaios4Shell2";


    public Transform enemyBackAmmoShellPos; //탄피 좌표

    public GameObject KaotiJaios4AmmoPrefab;
    public GameObject KaotiJaios4Ammo2Prefab;
    public GameObject KaotiJaios4Shell1Prefab;
    public GameObject KaotiJaios4Shell2Prefab;

    public GameObject KaotiJaios4Ammo3Prefab;
    public GameObject KaotiJaios4Ammo4Prefab;

    GameObject[] KaotiJaios4Ammo; //카오티-자이오스 4 총알 및 파편
    GameObject[] KaotiJaios4Ammo2;
    GameObject[] KaotiJaios4Ammo3; //카오티-자이오스 4 총알 및 파편
    GameObject[] KaotiJaios4Ammo4;
    GameObject[] KaotiJaios4Shell1; // 카오티-자이오스 4 탄피 1
    GameObject[] KaotiJaios4Shell2; // 카오티-자이오스 4 탄피 2
    GameObject[] PoolMaker;


    public GameObject smokePrefab; //연기 프리팹
    public Transform smokePos; //연기 좌표
    public GameObject smoke2Prefab; //연기 프리팹
    public Transform smoke2Pos; //연기 좌표
    public GameObject gunSmokePrefab; //사격 연기 프리팹
    public Transform gunSmokePos; //사격 연기 좌표
    public GameObject gunSmoke2Prefab; //사격 연기 프리팹
    public Transform gunSmoke2Pos; //사격 연기 좌표
    public GameObject gunSmoke3Prefab; //사격 연기 프리팹
    public Transform gunSmoke3Pos; //사격 연기 좌표
    public GameObject gunSmoke4Prefab; //사격 연기 프리팹
    public Transform gunSmoke4Pos; //사격 연기 좌표

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;
    Animator animator;

    float currentAngle = 0;

    private int RicochetSoundRandom;
    public AudioClip FireSound1;
    public AudioClip FireSound2;
    public AudioClip FireSound3;
    public AudioClip FireSound4;
    public AudioClip FireSound5;
    public AudioClip Reloading;

    // public ObjectManager objectManager;

    //GameObject FrontAmmo;
    // GameObject enemyFrontAmmoShell;
    // GameObject BackAmmo;
    // GameObject enemyBackAmmoShell;

    public void RandomSound()
    {
        RicochetSoundRandom = Random.Range(0, 6);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay("Sound", FireSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay("Sound", FireSound2);
        else if (RicochetSoundRandom == 2)
            SoundManager.instance.SFXPlay("Sound", FireSound3);
        else if (RicochetSoundRandom == 3)
            SoundManager.instance.SFXPlay("Sound", FireSound4);
        else if (RicochetSoundRandom == 4)
            SoundManager.instance.SFXPlay("Sound", FireSound5);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션

        KaotiJaios4Ammo = new GameObject[3];
        KaotiJaios4Ammo2 = new GameObject[3];
        KaotiJaios4Ammo3 = new GameObject[3];
        KaotiJaios4Ammo4 = new GameObject[3];
        KaotiJaios4Shell1 = new GameObject[15];
        KaotiJaios4Shell2 = new GameObject[15];

        Generate();
    }

    void OnEnable()
    {
        avoid = false;
        fired = false;
        reloading = false;
        afterFire = false;
        wandering = false;
        Moving = false;
        stopGoing = false;
        chasing = false;
        outOfSide = false;

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        StartCoroutine(yMove()); //일정시간당 추적
        StartCoroutine(RandomFire()); //랜덤 사격
        StartCoroutine(MoveToWard()); //전진 및 후진 애니메이션

        KaotiJaios4Ammo = new GameObject[3];
        KaotiJaios4Ammo2 = new GameObject[3];
        KaotiJaios4Ammo3 = new GameObject[3];
        KaotiJaios4Ammo4 = new GameObject[3];
        KaotiJaios4Shell1 = new GameObject[15];
        KaotiJaios4Shell2 = new GameObject[15];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < KaotiJaios4Ammo.Length; index++)
        {
            KaotiJaios4Ammo[index] = Instantiate(KaotiJaios4AmmoPrefab);
            KaotiJaios4Ammo[index].SetActive(false);
        }

        for (int index = 0; index < KaotiJaios4Ammo2.Length; index++)
        {
            KaotiJaios4Ammo2[index] = Instantiate(KaotiJaios4Ammo2Prefab);
            KaotiJaios4Ammo2[index].SetActive(false);
        }

        for (int index = 0; index < KaotiJaios4Ammo3.Length; index++)
        {
            KaotiJaios4Ammo3[index] = Instantiate(KaotiJaios4Ammo3Prefab);
            KaotiJaios4Ammo3[index].SetActive(false);
        }

        for (int index = 0; index < KaotiJaios4Ammo4.Length; index++)
        {
            KaotiJaios4Ammo4[index] = Instantiate(KaotiJaios4Ammo4Prefab);
            KaotiJaios4Ammo4[index].SetActive(false);
        }

        for (int index = 0; index < KaotiJaios4Shell1.Length; index++)
        {
            KaotiJaios4Shell1[index] = Instantiate(KaotiJaios4Shell1Prefab);
            KaotiJaios4Shell1[index].SetActive(false);
        }
        for (int index = 0; index < KaotiJaios4Shell2.Length; index++)
        {
            KaotiJaios4Shell2[index] = Instantiate(KaotiJaios4Shell2Prefab);
            KaotiJaios4Shell2[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "KaotiJaios4Ammo":
                PoolMaker = KaotiJaios4Ammo;
                break;

            case "KaotiJaios4Ammo2":
                PoolMaker = KaotiJaios4Ammo2;
                break;

            case "KaotiJaios4Ammo3":
                PoolMaker = KaotiJaios4Ammo3;
                break;

            case "KaotiJaios4Ammo4":
                PoolMaker = KaotiJaios4Ammo4;
                break;

            case "KaotiJaios4Shell1":
                PoolMaker = KaotiJaios4Shell1;
                break;

            case "KaotiJaios4Shell2":
                PoolMaker = KaotiJaios4Shell2;
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

    public void SetDamage(int num)
    {
        damage = num;
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
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true) //배회 및 Y축 이동
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

        //장전
        if (ammo >= AmmoPerMagazine && reloading == false)
        {
            StartCoroutine(Reload());
            StartCoroutine(ReloadSmoke());
        }

        //사격 후 연기
        if (afterFire == true)
        {
            afterFire = false;
            GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //연기 생성
            GameObject GunSmoke2 = Instantiate(gunSmoke2Prefab, gunSmoke2Pos.position, gunSmoke2Pos.rotation); //연기 생성
            GameObject GunSmoke3 = Instantiate(gunSmoke3Prefab, gunSmoke3Pos.position, gunSmoke3Pos.rotation); //연기 생성
            GameObject GunSmoke4 = Instantiate(gunSmoke4Prefab, gunSmoke4Pos.position, gunSmoke4Pos.rotation); //연기 생성
            Destroy(GunSmoke, 2);
            Destroy(GunSmoke2, 2);
            Destroy(GunSmoke3, 2);
            Destroy(GunSmoke4, 2);
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
                animator.SetBool("Kaoti-Jaios 4 moving", true);
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
        if (player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //transform.Lookat(player.position) 360도 회전으로 플레이어 쳐다보기
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
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", true);
                }
                else
                {
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", true);
                }
            }
            else if (v1.x < transform.position.x && stopGoing == false || v1.x < transform.position.x && chasing == true)
            {
                if (transform.rotation.y == 0)
                {
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", true);
                }
                else
                {
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", true);
                }
            }
            if (stopGoing == true && chasing == false)
            {
                animator.SetBool("Kaoti-Jaios 4 moving", false);
                animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
            }
        }
    }

    //총 발사
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Player")
        {
            if (enemyShoot == 1 && fired == false && reloading == false)
            {
                fired = true;
                Kaot4_Attack();
                // StartCoroutine(Attack());
            }
        }
    }

    //사격
    //IEnumerator Attack()
    //{
    //    GameObject BackAmmo;
    //    animator.SetBool("Kaoti-Jaios 4 Firing", true);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Eye", true);
    //    FireEffect(); //사격 효과
    //                  //FrontAmmo = Instantiate(enemyFrontAmmoPrefab, enemyFrontAmmoPos.position, transform.rotation); //발사 총알 생성

    //    FrontAmmo = objectManager.Loader(enemyFrontAmmoPrefab);
    //    FrontAmmo.transform.position = enemyFrontAmmoPos.position;
    //    FrontAmmo.transform.rotation = enemyFrontAmmoPos.rotation;
    //    FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //총알에다 데미지 전달

    //    //AmmoMovementKaotiJaios4 ammoKaot = FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>();
    //    //ammoKaot.objectManager = objectManager;

    //    EjectShell(); //탄피 방출
    //    yield return new WaitForSeconds(0.04f);

    //    BackAmmo = Instantiate(enemyBackAmmoPrefab, enemyBackAmmoPos.position, transform.rotation); //발사 총알 생성
    //    BackAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //총알에다 데미지 전달
    //    EjectShellBack(); //탄피 앞 방출
    //    afterFire = true;

    //    yield return new WaitForSeconds(0.26f);

    //    animator.SetBool("Kaoti-Jaios 4 Firing", false);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Effect1", false);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Effect2", false);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Effect3", false);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Effect4", false);

    //    yield return new WaitForSeconds(fireDelay);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);
    //    ammo += 2;
    //    fired = false;
    //}

    public void Kaot4_Attack() // ** 공격 함수 위 이넘레이터 수정 버전 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing", true);
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", true);
        FireEffect(); //사격 효과
        RandomSound();

        GameObject FrontAmmo = Loader(enemyFrontAmmoPrefab);
        FrontAmmo.transform.position = enemyFrontAmmoPos.position;
        FrontAmmo.transform.rotation = transform.rotation;
        FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        GameObject FrontAmmo2 = Loader(enemyFrontAmmoPrefab2);
        FrontAmmo2.transform.position = enemyFrontAmmoPos2.position;
        FrontAmmo2.transform.rotation = transform.rotation;
        FrontAmmo2.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        EjectShell(); //탄피 방출
        Invoke("BackAmmo_ShellBack", 0.04f);
    }

    void BackAmmo_ShellBack()
    {
        GameObject BackAmmo = Loader(enemyBackAmmoPrefab);
        BackAmmo.transform.position = enemyBackAmmoPos.position;
        BackAmmo.transform.rotation = transform.rotation;
        BackAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //총알에다 데미지 전달

        GameObject BackAmmo2 = Loader(enemyBackAmmoPrefab2);
        BackAmmo2.transform.position = enemyBackAmmoPos2.position;
        BackAmmo2.transform.rotation = transform.rotation;
        BackAmmo2.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        //Debug.Log("2번 총알 발사");
        //BackAmmo.transform.rotation = enemyBackAmmoPos.rotation;
        // BackAmmo = Instantiate(enemyBackAmmoPrefab, enemyBackAmmoPos.position, transform.rotation); //발사 총알 생성

        EjectShellBack(); //탄피 앞 방출
        afterFire = true;

        Invoke("FireAni_false", 0.26f);

    }

    void FireAni_false() // ** Fire 애니메이션 false 함수 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect1", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect2", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect3", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect4", false);

        Invoke("ani_false", fireDelay);
    }

    void ani_false() // ** Fire 딜레이 함수 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);
        ammo += 2;
        fired = false;
    }

    //탄피 방출
    void EjectShell() // ** 첫번째 탄피 배출함수 
    {
        //GameObject enemyFrontAmmoShell = Instantiate(enemyFrontAmmoShellPrefab, enemyFrontAmmoShellPos.position, transform.rotation);

        GameObject enemyFrontAmmoShell = Loader(enemyFrontAmmoShellPrefab);
        enemyFrontAmmoShell.transform.position = enemyFrontAmmoShellPos.position;

        //enemyFrontAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        enemyFrontAmmoShell.GetComponent<ShellMovement>().xVnot = xVnot;
        enemyFrontAmmoShell.GetComponent<ShellMovement>().yVnot = yVnot;

        //  Destroy(enemyFrontAmmoShell, 15.0f); //일정시간후, 떨어진 탄피 삭제
    }

    //탄피 앞 방출
    void EjectShellBack() // ** 두번째 탄피 배출함수 
    {
        GameObject enemyBackAmmoShell = Loader(enemyBackAmmoShellPrefab);
        enemyBackAmmoShell.transform.position = enemyFrontAmmoShellPos.position;

        //enemyBackAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

        //GameObject enemyBackAmmoShell = Instantiate(enemyBackAmmoShellPrefab, enemyBackAmmoShellPos.position, transform.rotation);
        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        enemyBackAmmoShell.GetComponent<ShellMovement>().xVnot = xVnot;
        enemyBackAmmoShell.GetComponent<ShellMovement>().yVnot = yVnot;

        // Destroy(enemyBackAmmoShell, 15.0f); //일정시간후, 떨어진 탄피 삭제 
        // (처리안함################################)
    }

    //사격 효과
    void FireEffect()
    {
        fireEffect = Random.Range(1, 4);

        if (fireEffect == 1)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect1", true);
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect2", true);
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect3", true);
        }
        else if (fireEffect == 4)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect4", true);
        }


    }

    //장전
    IEnumerator Reload()
    {
        reloading = true;
        Kaot4_Attack();
        //StopCoroutine(Attack());
        animator.SetBool("Kaoti-Jaios 4 Reloading", true);
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);
        SoundManager.instance.SFXPlay("Sound", Reloading);

        yield return new WaitForSeconds(2.8f);

        animator.SetBool("Kaoti-Jaios 4 Reloading", false);
        reloading = false;
        ammo = 0;
    }

    //장전 연기 발생
    IEnumerator ReloadSmoke()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //연기 생성
        GameObject Smoke2 = Instantiate(smoke2Prefab, smoke2Pos.position, smoke2Pos.rotation); //연기 생성
        Destroy(Smoke, 2);
        Destroy(Smoke2, 2);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, avoidSite);
        Gizmos.DrawWireSphere(transform.position, traceSite);
    }*/
}