using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyArmyAI : MonoBehaviour
{
    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float traceSite; //������, �ֿܰ����� ������ ���̿��� �÷��̾ ������Ű�� �뵵
    public float avoidSite; //���� ���ʿ� �ִ� ��, �÷��̾ ������ ���� ���, ���ϱ� ���� �뵵

    private Transform Enemy;
    private Transform Player;
    private Vector2 target;

    //public int Damage;
    public int SWdamage; //�߻�� ���ط�

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

    public Transform ammoPos; //�Ѿ� ���� ��ǥ
    public Transform ShellPos; //ź��1 ��ǥ
    public GameObject ammoPrefab;
    public GameObject ShellPrefab;

    GameObject[] SPammo; //ī��Ƽ-���̿��� 4 �Ѿ� �� ����
    GameObject[] SPShell; // ī��Ƽ-���̿��� 4 ź�� 1
    GameObject[] PoolMaker;

    Transform targetTransform = null;

    Rigidbody2D rb2d;
    Coroutine moveCoroutine;
    Vector3 endposition;

    float random;
    //Animator animator;
    float currentAngle = 0;

    Transform Enemytarget = null;
    ObjectManager objectManager;
    EnemiesSpawnManager enemiesSpawnManager;

    bool moveX;
    bool Trigger;
    bool Search;

    int AttackCnt; 
    public float AttackTime;
    public float AttackCoolTime;

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        enemiesSpawnManager = FindObjectOfType<EnemiesSpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���

        SPammo = new GameObject[30];
        SPShell = new GameObject[30];

        Generate();
    }

    void SearchEnemy()
    {
        if(Search)
        Enemytarget = enemiesSpawnManager.EnemyList[Random.Range(0, enemiesSpawnManager.EnemyList.Count - 1)].transform;
    }

    void Generate()
    {
        for (int index = 0; index < SPammo.Length; index++)
        {
            SPammo[index] = Instantiate(ammoPrefab);
            SPammo[index].SetActive(false);
        }

        for (int index = 0; index < SPShell.Length; index++)
        {
            SPShell[index] = Instantiate(ShellPrefab);
            SPShell[index].SetActive(false);
        }
    }

    public GameObject Loader(string type)
    {
        switch(type)
        {
            case "SPammo":
                PoolMaker = SPammo;
            break;

            case "SPShell":
                PoolMaker = SPShell;
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

    //yMove ���߱�
    IEnumerator StopyMove()
    {
        StopCoroutine(yMove());
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        AttackTime += Time.deltaTime;

        if(AttackTime > AttackCoolTime)
        {
            AttackCnt++;
            AttackTime = 0;
        }
        else if (AttackCnt >= 1)
        {
            AttackTime = 0;
        }


        random = Random.Range(-5, 7f);

        if (Trigger)
        {
            Attack();
        }

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

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x + random, Player.position.y + random, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
                MovementPlayer();
                moveX = false;
            }
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true) //��ȸ �� Y�� �̵�
            {
                currnetSpeed = speed;
                MovementPlayer();
                moveX = false;

            }

            //������ �������� ���, ȸ��
            else if (distanceFromPlayer < avoidSite)
            {
                moveX = true;
            }
 
        }

        //}




        //����
        if (ammo >= AmmoPerMagazine && reloading == false)
        {
            StartCoroutine(Reload());
            //StartCoroutine(ReloadSmoke());
        }

        SearchEnemy();

        if (Enemytarget != null && Enemytarget.gameObject.activeSelf == true)
        {
            Search = false;
            LookAtEnemy(); //�÷��̾� �ٶ󺸱�
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�
        }

        else
        {
            Search = true;
            return;
        }

        


     
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
        Debug.Log("��ȸ �� y�� �̵�");

        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);

        if(moveX)
        {
            if (moveDown == 1 || moveDown == 2) //�÷��̾� ��� ����
            {
                chasing = true;
                currnetSpeed = runningSpeed;
                StopCoroutine(ShootingMovement());
                //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * currnetSpeed);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.position.x + random, Player.position.y + random, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�, 2022.01.12
                                                                                                                                                                                                      //this.gameObject.AddComponent<EnemiesAI>().yChase(); //Y�� ������ by EnemiesAI
            }
            else if (moveDown != 1 && distanceFromPlayer > avoidSite && avoid == false && wandering == false && outOfSide == true)
            {
                wandering = true; //Debug.Log("wandering : " + wandering);
                chasing = true;
                StartCoroutine(ShootingMovement());
            }
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
        if (Enemytarget.position.x < transform.position.x)
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

        if (collision is CircleCollider2D || collision is CapsuleCollider2D && collision.CompareTag("Enemy"))
        {
            Trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!enabled)
            return;

        if (collision is CircleCollider2D || collision is CapsuleCollider2D && collision.CompareTag("Enemy")   )
        {
            Trigger = false;
        }
    }

    public void Attack() // ** ���� �Լ� �� �̳ѷ����� ���� ���� 
    {
        if (AttackCnt >= 1)
        {
            GameObject SPammo = Loader("SPammo");
            SPammo.transform.position = ammoPos.position;
            SPammo.transform.rotation = ammoPos.rotation;

            SPammo.GetComponent<SP_AmmoMovement>().SetDamage(SWdamage); //�Ѿ˿��� ������ ����

            EjectShell(); //ź�� ����

            //  Invoke("ani_false", fireDelay);
            AttackCnt--;
        }


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
       
        GameObject SPShell = Loader("SPShell");
        SPShell.transform.position = ShellPos.position;

        //enemyFrontAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        SPShell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
        SPShell.GetComponent<ShellCase_SW06>().yVnot = yVnot;

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