using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float traceSite; //������, �ֿܰ����� ������ ���̿��� �÷��̾ ������Ű�� �뵵
    public float avoidSite; //���� ���ʿ� �ִ� ��, �÷��̾ ������ ���� ���, ���ϱ� ���� �뵵

    Transform Enemy = null;
    private Transform Player;
    private Vector2 target;

    //public int Damage;
    public int NBdamage; //���� �߻�� ���ط�

    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    private float waitWanderTime; //��ȸ�� ���� ���ӽð�
    private int wantToStop; //��ȸ�ϴ� ������ Ȯ��
    private float TraceStartAcoount; //������ �ð� ���ϱ�, 2022.01.12

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

    public Transform RobotammoPos; //�Ѿ� ���� ��ǥ

    public Transform RobotShellPos; //ź��1 ��ǥ
    public GameObject RobotammoPrefab;
    public GameObject RobotShellPrefab;

    GameObject[] Robotammo; //ī��Ƽ-���̿��� 4 �Ѿ� �� ����
    GameObject[] RobotShell; // ī��Ƽ-���̿��� 4 ź�� 1
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
        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���

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


    //���� ���
    IEnumerator RandomFire()
    {
        while(true)
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
            moveDown = Random.Range(0, 2); //���� �߰�
            yield return new WaitForSeconds(1f);
        }
    }

    ////yMove ���߱�
    //IEnumerator StopyMove()
    //{
    //    StopCoroutine(yMove());
    //    yield return new WaitForSeconds(1f);
    //}

    void Update()
    {

        Debug.Log("�÷��̾ ��ȣ�Ѵ�");
        Player = GameObject.FindWithTag("Player").transform;
        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
        //Debug.Log(avoid);

        //Enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        //float distanceFromEnemy = Vector2.Distance(Enemy.position, transform.position);

        LookAtPlayer(); //�÷��̾� �ٶ󺸱�

        //���� �ݰ� �� ���ٽ� ��ȸ ����
        if (distanceFromPlayer < lineOfSite)
        {
            outOfSide = true;

            //���� ������ �ֿܰ� ������ ���� ���, �÷��̾ ���� ���������� ����
            if (distanceFromPlayer > traceSite)
            {
                chasing = true;
                currnetSpeed = runningSpeed;
                // transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x, Player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
                MovementPlayer();

            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true) //��ȸ �� Y�� �̵�
            {
                Debug.Log("����");
                currnetSpeed = speed;
                MovementPlayer();
            }
        }

        ////���� �ݰ��� ��Ż��, ��ȸ ����
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

        //    //���� ������ �ֿܰ� ������ ���� ���, �÷��̾ ���� ���������� ����
        //    if (distanceFromEnemy > traceSite)
        //    {
        //        chasing = true;
        //        currnetSpeed = runningSpeed;
        //        // transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);
        //        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemy.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
        //    }
        //    else if (distanceFromEnemy < traceSite && distanceFromEnemy > avoidSite && avoid == false && outOfSide == true) //��ȸ �� Y�� �̵�
        //    {
        //        currnetSpeed = speed;
        //        MovementEnemy();

        //        if (avoid == true)
        //        {
        //            transform.position = Vector2.MoveTowards(transform.position, Enemy.position, -currnetSpeed * Time.deltaTime);
        //        }
        //    }

        //    //������ �������� ���, ȸ��
        //    if (distanceFromEnemy < avoidSite)
        //    {
        //        avoid = true;
        //        chasing = true;
        //        moveDown = 0;
        //        currnetSpeed = runningSpeed;
        //        StartCoroutine(StopyMove());
        //        StartCoroutine(Avoid()); //ȸ��
        //    }
        //    else if (distanceFromEnemy > avoidSite && avoid == true)
        //    {
        //        StartCoroutine(AvoidDuringWander()); //�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
        //    }
        //}

        ////���� �ݰ��� ��Ż��, ��ȸ ����
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



        //����
        if (ammo >= AmmoPerMagazine && reloading == false)
        {
            StartCoroutine(Reload());
            //StartCoroutine(ReloadSmoke());
        }

        //���ݽ�, �÷��̾� ����

        ////��� �� ����
        //if (afterFire == true)
        //{
        //    afterFire = false;
        //    GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
        //    GameObject GunSmoke2 = Instantiate(gunSmoke2Prefab, gunSmoke2Pos.position, gunSmoke2Pos.rotation); //���� ����
        //    Destroy(GunSmoke, 2);
        //    Destroy(GunSmoke2, 2);
        //}

        SearchEnemy();

        if (Enemy != null)
        {
            Debug.Log("���߰� ������ �����Ѵ�");
            LookAtEnemy(); //�÷��̾� �ٶ󺸱�

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemy.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
        }

        else
            return;


        


    }

    ////ȸ��
    //IEnumerator Avoid()
    //{
    //    TraceStartAcoount = Random.Range(0.3f, 1.5f); //2022.01.12 ȸ��
    //    yield return new WaitForSeconds(TraceStartAcoount); //2022.01.12
    //    transform.position = Vector2.MoveTowards(transform.position, Enemy.position, -currnetSpeed * Time.deltaTime);
    //}

    ////�����ϴ� ���� ȸ�Ƕ��ο��� ȸ�Ǹ� ���� ���� ������ �ٽõ��� ���
    //IEnumerator AvoidDuringWander()
    //{
    //    yield return new WaitForSeconds(2f);
    //    avoid = false;
    //}

    //��ȸ �� Y�� �̵�
    void MovementPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);

        if (moveDown == 1 || moveDown == 2) //�÷��̾� ��� ����
        {
            chasing = true;
            currnetSpeed = runningSpeed;
            StopCoroutine(ShootingMovement());
            //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * currnetSpeed);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x * 0.9f, Player.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�, 2022.01.12
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
        Debug.Log("�Ϲݹ�ȸ");
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
        if(stopGoing == false)
        {
            rigidbodyToMove.velocity = Vector2.zero; //Debug.Log("Stop");
            stopGoing = true; //Debug.Log("stopGoing : " + stopGoing);
            chasing = false;
        }
    }

    //x�� �÷��̾� �Ĵٺ���
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

        //transform.Lookat(player.position) 360�� ȸ������ �÷��̾� �Ĵٺ���
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

        //transform.Lookat(player.position) 360�� ȸ������ �÷��̾� �Ĵٺ���
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
            return;

        if (collision is CircleCollider2D && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("�� ã�Ҵ� ������!!!!!!!!!!");

            if (enemyShoot == 1 && fired == false /*&& reloading == false*/)
            {
                Debug.Log("���߰�~~~~~~~~ ����!!!!!!!!!");
                fired = true;
                Attack();
                // StartCoroutine(Attack());
            }
        }
    }

    public void Attack() // ** ���� �Լ� �� �̳ѷ����� ���� ���� 
    { 
        GameObject Robotammo = Loader("Robotammo");
        Robotammo.transform.position = RobotammoPos.position;
        Robotammo.transform.rotation = transform.rotation;
        Robotammo.GetComponent<NomalBullet>().SetDamage(NBdamage); //�Ѿ˿��� ������ ����

        EjectShell(); //ź�� ����

        Invoke("ani_false", fireDelay);

    }


    void ani_false() // ** Fire ������ �Լ� 
    {
        ammo += 2;
        fired = false;
    }

    //ź�� ����
    void EjectShell() // ** ù��° ź�� �����Լ� 
    {
        //GameObject enemyFrontAmmoShell = Instantiate(enemyFrontAmmoShellPrefab, enemyFrontAmmoShellPos.position, transform.rotation);
       
        GameObject RobotShell = Loader("RobotShell");
        RobotShell.transform.position = RobotShellPos.position;

        //enemyFrontAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

       //  Destroy(enemyFrontAmmoShell, 15.0f); //�����ð���, ������ ź�� ����
    }



    //����
    IEnumerator Reload()
    {
        reloading = true;
        Attack();

        yield return new WaitForSeconds(2.8f);
        reloading = false;
        ammo = 0;
    }

    //���� ���� �߻�
    //IEnumerator ReloadSmoke()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //���� ����
    //    GameObject Smoke2 = Instantiate(smoke2Prefab, smoke2Pos.position, smoke2Pos.rotation); //���� ����
    //    Destroy(Smoke, 2);
    //    Destroy(Smoke2, 2);
    //}

}