using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTaikaLaiThrotro1 : MonoBehaviour
{ 
    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float traceSite; //������, �ֿܰ����� ������ ���̿��� �÷��̾ ������Ű�� �뵵
    public float avoidSite; //���� ���ʿ� �ִ� ��, �÷��̾ ������ ���� ���, ���ϱ� ���� �뵵

    private Transform player;
    private Vector2 target;

    public int Damage; //���� �߻�� ���ط�
    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    private float waitWanderTime; //��ȸ�� ���� ���ӽð�
    private int wantToStop; //��ȸ�ϴ� ������ Ȯ��

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

    public GameObject enemyAmmoPrefab; //�߻� �Ѿ� ������
    public Transform enemyAmmoPos; //�Ѿ� ���� ��ǥ

    public GameObject flareAmmoPrefab; //�÷��� �Ѿ� ������
    public Transform flareAmmoPos; //�÷��� ���� ��ǥ
    public Transform flareAmmo2Pos; //�÷��� ����2 ��ǥ

    public GameObject flareFlamePrefab; //�÷��� �Ҳɻ��� ��ǥ
    public Transform flareFlamePos; //�÷��� �Ҳ� ��ǥ

    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ
    
    public GameObject sitePrefab; //���� ��ǥ ������
    /// 
    /// </summary>
    public GameObject skyCranePrefab; //��ī�� ũ���� ������
    public GameObject skyCrane2Prefab; //��ī�� ũ���� ������
   // public Transform skyCranePos; //��ī�� ũ���� ���� ��ǥ

    public GameObject kaotiJaios4Prefab; // ** ī��Ƽ 4 ������
    public GameObject KaotiJaios4SpearPrefab;
    public GameObject KaotiJaios4Fleet1389Prefab;
    public GameObject KaotiJaios4DualgunPrefab;
    public GameObject KaotiJaios4ArmorDualgunPrefab;
    public GameObject KaotiJaios4ArmorPrefab;

    GameObject[] Kaotijaios4; // ** ī��Ƽ 4 
    GameObject[] Kaotijaios4Spear;
    GameObject[] Kaotijaios4Fleet1389;
    GameObject[] Kaotijaios4Dualgun;
    GameObject[] Kaotijaios4ArmorDualgun;
    GameObject[] Kaotijaios4Armor;
    GameObject[] skyCrane; // ** ī��Ƽ 4 
    GameObject[] skyCrane2; // ** ī��Ƽ 4 

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

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
        StartCoroutine(SendingRequest()); //�Ʊ� ���� ��û

        //flareAmmo = new GameObject[3];
        //flareFlame = new GameObject[3];
        //site = new GameObject[6];

        //Kaotijaios4 = new GameObject[6]; // ** ī��Ƽ 4  
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

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
        StartCoroutine(SendingRequest()); //�Ʊ� ���� ��û
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

    //���� ���
    IEnumerator RandomFire()
    {
        while (true)
        {
            enemyShoot = Random.Range(1, shootingDelayOnEasyLevel);
            yield return new WaitForSeconds(1f);
        }
    }

    //�����ð��� ���� �� ���߱�
    IEnumerator yMove()
    {
        while (true)
        {
            moveDown = Random.Range(0, 5); //���� �߰�
            yield return new WaitForSeconds(1f);
        }
    }

    //yMove ���߱�
    IEnumerator StopyMove()
    {
        StopCoroutine(yMove());
        yield return new WaitForSeconds(1f);
    }

    //�Ʊ� ���� ��û
    IEnumerator SendingRequest()
    {
        GameObject FlareFlame;
        //yield return new WaitForSeconds(10);

        if (charging == true || stopAndFire == true) //��� ���� ���
        {
            while (true)
            {
                yield return new WaitForSeconds(1); //����� ���� ������ ���

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

    //���� ��ǥ
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

    //���� ��ǥ ����
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
            //area.enabled = false; //�ش� �ݶ��̴��� üũ�ڽ��� ����
        }
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //Debug.Log(avoid);

        LookAtPlayer(); //�÷��̾� �ٶ󺸱�
        ResetSite(); //���� ��ǥ ����

        //���� �ݰ� �� ���ٽ� ��ȸ ����
        if (distanceFromPlayer < lineOfSite)
        {
            outOfSide = true;

            //���� ������ �ֿܰ� ������ ���� ���, �÷��̾ ���� ���������� ����
            if (distanceFromPlayer > traceSite)
            {
                chasing = true;
                currnetSpeed = runningSpeed;
                transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true && charging == false && stopAndFire == false) //��ȸ �� Y�� �̵�
            {
                currnetSpeed = speed;
                Movement();

                if (avoid == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -currnetSpeed * Time.deltaTime);
                }
            }

            //������ �������� ���, ȸ��
            if (distanceFromPlayer < avoidSite)
            {
                avoid = true;
                chasing = true;
                moveDown = 0;
                currnetSpeed = runningSpeed;
                StartCoroutine(StopyMove());
                StartCoroutine(Avoid()); //ȸ��
            }
            else if (distanceFromPlayer > avoidSite && avoid == true)
            {
                StartCoroutine(AvoidDuringWander()); //�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
            }
        }

        //���� �ݰ��� ��Ż��, ��ȸ ����
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

        WeaponRotation(); //���� ������ ȸ�� �ӵ� ����

        //���ݽ�, �÷��̾� ����
        if (charging == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
        }

        //��� ������ �ӵ� ����
        if(stopAndFire == true)
        {
            currnetSpeed = 0;
        }

        //��� �� ����
        if (afterFire == true)
        {
            afterFire = false;
            GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
            Destroy(GunSmoke, 3);
        }
    }

    //ȸ��
    IEnumerator Avoid()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -currnetSpeed * Time.deltaTime);
    }

    //�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
    IEnumerator AvoidDuringWander()
    {
        yield return new WaitForSeconds(2f);
        avoid = false;
    }

    //��ȸ �� Y�� �̵�
    void Movement()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (moveDown == 1 && distanceFromPlayer > avoidSite && avoid == false) //�÷��̾� ��� ����
        {
            chasing = true;
            currnetSpeed = runningSpeed;
            StopCoroutine(ShootingMovement());
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * currnetSpeed);
            //this.gameObject.AddComponent<EnemiesAI>().yChase(); //Y�� ������ by EnemiesAI
        }
        else if (moveDown != 1 && distanceFromPlayer > avoidSite && avoid == false && wandering == false && outOfSide == true)
        {
            wandering = true; //Debug.Log("wandering : " + wandering);
            chasing = true;
            StartCoroutine(ShootingMovement());
        }
    }

    //�Ϲ� ��ȸ
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

    //��ǥ ���� ����
    public void ChooseNewEndPoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endposition += Vector3FromAngle(currentAngle);
    }

    //���� ����
    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    //������ ���� ������ ����
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endposition).sqrMagnitude;

        while (remainingDistance > float.Epsilon && outOfSide == true)
        {
            if (targetTransform != null)
            {
                endposition = targetTransform.position;
            }

            //�̵�
            if (rigidbodyToMove != null)
            {
                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);

                if (newPosition == endposition || transform.position == endposition) //��ǥ������ ���� ������ ������ ���, ���ڸ����� ����ä�� �̵� �ִϸ��̼��� Ȱ��ȭ�Ǵ� ������ ����
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

    //x�� �÷��̾� �Ĵٺ���
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

    //���� �� ����, ���� �ִϸ��̼�
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

    //�� �߻�
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Player")
        {
            if (enemyShoot == 1 && fired == false)
            {
                fired = true;
                StartCoroutine(Attack()); //���
            }
        }
    }

    //���
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
        FireEffect(); //��� ȿ��
        SoundManager.instance.SFXPlay("Sound", FireSound1);
        RailGunAmmo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
        RailGunAmmo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(Damage); //�Ѿ˿��� ������ ����
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

    //���� ������ ȸ�� �ӵ� ����
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

    //��� ȿ��
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

    //��� ����
    void FireOut()
    {
        if (animator.GetBool("Fire effect1, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect1, Taika-Lai-Throtro 1", false);
        if (animator.GetBool("Fire effect2, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect2, Taika-Lai-Throtro 1", false);
        if (animator.GetBool("Fire effect3, Taika-Lai-Throtro 1"))
            animator.SetBool("Fire effect3, Taika-Lai-Throtro 1", false);
    }

    //��� �÷��� ȿ��
    void FireFlareEffect()
    {
        GameObject FlareAmmo;
        GameObject FlareAmmo2;
        fireEffect = Random.Range(1, 4);

        if (fireEffect == 1)
        {
            animator.SetBool("Fire flare effect1, Taika-Lai-Throtro 1", true);
            //FlareAmmo = Instantiate(flareAmmoPrefab, flareAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
            FlareAmmo = EPmanager.Loader("flareAmmo");
            FlareAmmo.transform.position = flareAmmoPos.position;
            FlareAmmo.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("Fire flare effect1-1, Taika-Lai-Throtro 1", true);
            //  FlareAmmo2 = Instantiate(flareAmmoPrefab, flareAmmo2Pos.position, transform.rotation); //�߻� �Ѿ�2 ����
            FlareAmmo2 = EPmanager.Loader("flareAmmo");
            FlareAmmo2.transform.position = flareAmmo2Pos.position;
            FlareAmmo2.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("Fire flare effect2, Taika-Lai-Throtro 1", true);
            //FlareAmmo = Instantiate(flareAmmoPrefab, flareAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
            FlareAmmo = EPmanager.Loader("flareAmmo");
            FlareAmmo.transform.position = flareAmmoPos.position;
            FlareAmmo.transform.rotation = transform.rotation;
        }
        else if (fireEffect == 4)
        {
            animator.SetBool("Fire flare effect2-1, Taika-Lai-Throtro 1", true);
          //  FlareAmmo2 = Instantiate(flareAmmoPrefab, flareAmmo2Pos.position, transform.rotation); //�߻� �Ѿ�2 ����
            FlareAmmo2 = EPmanager.Loader("flareAmmo");
            FlareAmmo2.transform.position = flareAmmo2Pos.position;
            FlareAmmo2.transform.rotation = transform.rotation;
        }
    }

    //��� �÷��� ����
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