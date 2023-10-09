using System.Collections;
using UnityEngine;

public class BehaviorTaikaLaiThrotro1_3 : MonoBehaviour
{
    Health2TaikaLaiThrotro1 health2TaikaLaiThrotro1;
    ObjectManager objectManager;
    Rigidbody2D rb2d;
    Animator animator;

    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float avoidSite; //���� ���ʿ� �ִ� ��, �÷��̾ ������ ���� ���, ���ϱ� ���� �뵵
    private float DownTime; //�ٿ�� ���¿��� ġ��Ÿ�� �� ���� �� �ൿ�� ������ �ʱ�ȭ �Ǵ� ���� ������ �� �ѹ��� �ٿ�Ǵ� ������ �����ϱ� ���� ����
    private float FireTime; //�ѹ��� ����ϵ��� ����
    public float FollowVector; //���� ���� �÷��̾� �������� �̵��ϱ� ���� ����

    public Transform Enemytarget = null;
    private Vector2 target; //��� ���� ��ġ
    Vector3 endposition; //���� ������ ��ġ��

    public int Damage; //���� �߻�� ���ط�
    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    public int SendingRequestTime; //�������ڸ��� �Ʊ��� �����ϴµ� �ɸ��� �ð�
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    private int EngineDown; //�������� ������ ���ư��� ����
    public float GunHitPoint; //�� ü��
    public float EngineHitPoint; //���� ü��. ü���� 50%�� �Ǹ� ������ �ϳ� ���ư���, ü���� 0�� �Ǹ� ������ �������� ���ư��鼭 �̵��� ����ȭ�ȴ�.
    private float GunHitPoint2; //�������� ü��
    private float EngineHitPoint2; //�������� ü��

    int fireEffect;

    public bool IntoLine = false; //���� ����, �÷��̾ �ִ� �������� �����ϱ� ���� ����ġ
    public bool IntoLineSpeed = false; //���� ����, �÷��̾� ���� ��ó�� ���� �� ������ �ӵ��� ����
    private bool charging = false; //����ϱ� ���� ��¡���� �� ����ġ
    private bool stopAndFire = false; //������� �� ����ġ
    private bool reloading = false; //���������� �� ����ġ
    private bool afterFire = false; //����� �� ���� ������ ����ġ
    private bool MoveComplete = false; //�������� ������ ���� ����ġ
    public bool Engine1Down = false; //����1�� ����ȭ �Ǿ��� ���� ����ġ
    public bool Engine2Down = false; //����2�� ����ȭ �Ǿ��� ���� ����ġ
    private bool EngineFirst1Down = false; //1�� ������ ���ư� ��, 2�� ������ ���ư����� �۵��Ǵ� ����ġ
    private bool EngineFirst2Down = false; //2�� ������ ���ư� ��, 1�� ������ ���ư����� �۵��Ǵ� ����ġ
    private bool EngineAllDown = false; //������ ��� ����ȭ �Ǿ��� ���� ����ġ
    public bool GunDown = false; //���� ���ư��� �۵��Ǵ� ����ġ
    public bool ImDown = false; //ġ��Ÿ�� �¾� ��� ����� ��� ������ ���� ����ġ

    float rotationTime = 0.5f;

    public Transform BlueExplosionPos;
    GameObject BlueExplosion;

    public GameObject enemyAmmoPrefab; //�߻� �Ѿ� ������
    public Transform enemyAmmoPos; //�Ѿ� ���� ��ǥ
    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ

    Coroutine attack;
    Coroutine moveCoroutine;

    public Transform magnetForm = null;
    public float MagnetForce;

    public AudioClip FireSound1;
    public AudioClip Charging;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        objectManager = FindObjectOfType<ObjectManager>();
        health2TaikaLaiThrotro1 = FindObjectOfType<Health2TaikaLaiThrotro1>();
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        reloading = false;
        afterFire = false;
        charging = false;
        stopAndFire = false;
        MoveComplete = false;
        GunDown = false;
        Engine1Down = false;
        Engine2Down = false;
        EngineFirst1Down = false;
        EngineFirst2Down = false;
        EngineAllDown = false;
        ImDown = false;
        EngineDown = 5;
        DownTime = 0;
        FireTime = 0;

        if (BattleSave.Save1.MissionLevel == 1)
        {
            Damage = 120;
            enemyShoot = 4;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            Damage = 168;
            enemyShoot = 3;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            Damage = 192;
            enemyShoot = 2;
        }

        EngineHitPoint = GetComponent<Health2TaikaLaiThrotro1>().startingHitPoints / 4;
        GunHitPoint = GetComponent<Health2TaikaLaiThrotro1>().startingHitPoints / 4;
        EngineHitPoint2 = EngineHitPoint;
        GunHitPoint2 = GunHitPoint;

        currnetSpeed = speed;
        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //�������
        StartCoroutine(ShootingMovement()); //��ȸ
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
    }

    private void OnDisable()
    {
        animator.SetBool("Charge Karrgen-Arite 31", false);
        animator.SetBool("Fire Karrgen-Arite 31", false);
        animator.SetBool("Fire, Taika-Lai-Throtro 1", false);

        animator.SetBool("Flying, Taika-Lai-Throtro 1", false);
        animator.SetBool("Im down!, Taika-Lai-Throtro 1", false);
        animator.SetBool("Engine down!, Taika-Lai-Throtro 1", false);
        animator.SetBool("Engine down!2, Taika-Lai-Throtro 1", false);
        animator.SetBool("Im down! no engine, Taika-Lai-Throtro 1", false);
        animator.SetBool("No Karrgen-Arite 31, Taika-Lai-Throtro 1", false);
        animator.SetBool("no engine front, Taika-Lai-Throtro 1", false);
        animator.SetBool("no engine back, Taika-Lai-Throtro 1", false);
        animator.SetBool("Warnning(small), Taika-Lai-Throtro 1", false);

        if (charging == true && stopAndFire == true)
        {
            StopCoroutine(attack);
            animator.SetBool("Warnning(small), Taika-Lai-Throtro 1", false);
        }
    }

    //�������� ������ ����
    public IEnumerator EngineDamage(int damage, float interval)
    {
        if (EngineAllDown == false)
        {
            while (true)
            {
                EngineHitPoint2 = EngineHitPoint2 - damage;

                if (EngineFirst1Down == false && EngineFirst2Down == false)
                {
                    if (EngineHitPoint2 <= EngineHitPoint / 2)
                    {
                        EngineDown = Random.Range(0, 2);

                        if (EngineDown == 0 && health2TaikaLaiThrotro1.hitPoints > 0)
                        {
                            Engine1Down = true;
                            EngineFirst1Down = true;
                            health2TaikaLaiThrotro1.TearOn = true;
                            gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().Engine1Down = true;
                            gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().PartDeathEngine1();
                            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
                            BlueExplosion.transform.position = BlueExplosionPos.position;
                            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
                            animator.SetBool("no engine front, Taika-Lai-Throtro 1", true);
                        }
                        else if (EngineDown == 1 && health2TaikaLaiThrotro1.hitPoints > 0)
                        {
                            Engine2Down = true;
                            EngineFirst2Down = true;
                            health2TaikaLaiThrotro1.TearOn = true;
                            gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().Engine2Down = true;
                            gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().PartDeathEngine2();
                            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
                            BlueExplosion.transform.position = BlueExplosionPos.position;
                            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
                            animator.SetBool("no engine back, Taika-Lai-Throtro 1", true);
                        }

                        EngineDown = 5;
                    }
                }

                if (EngineHitPoint2 <= float.Epsilon)
                {
                    if (EngineFirst1Down == true && health2TaikaLaiThrotro1.hitPoints > 0)
                    {
                        EngineFirst1Down = false;
                        StartCoroutine(EngineTakeDownAll());
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().Engine2Down = true;
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().EngineAllDown = true;
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().PartDeathEngine2();
                        Engine2Down = true;
                        health2TaikaLaiThrotro1.TearOn = true;
                    }
                    else if (EngineFirst2Down == true && health2TaikaLaiThrotro1.hitPoints > 0)
                    {
                        EngineFirst2Down = false;
                        StartCoroutine(EngineTakeDownAll());
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().Engine1Down = true;
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().EngineAllDown = true;
                        gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().PartDeathEngine1();
                        Engine1Down = true;
                        health2TaikaLaiThrotro1.TearOn = true;
                    }

                    EngineAllDown = true;
                    currnetSpeed = 0;
                    rb2d.velocity = Vector3.zero;
                    break;
                }

                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }

                else
                {
                    break;
                }
            }
        }
    }

    IEnumerator EngineTakeDownAll()
    {
        animator.SetBool("no engine front, Taika-Lai-Throtro 1", false);
        animator.SetBool("no engine back, Taika-Lai-Throtro 1", false);
        animator.SetBool("Engine down!, Taika-Lai-Throtro 1", true);
        BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
        BlueExplosion.transform.position = BlueExplosionPos.position;
        BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
        yield return new WaitForSeconds(0.83f);
        animator.SetBool("Engine down!2, Taika-Lai-Throtro 1", true);
        animator.SetBool("Engine down!, Taika-Lai-Throtro 1", false);
    }

    //ġ��Ÿ�� �¾��� �� �ٿ�Ǿ��ٰ� �ٽ� �Ͼ��
    public void TakeDown(bool boolean)
    {
        if (charging == false && stopAndFire == false)
            ImDown = boolean;
        if (DownTime == 0 && ImDown == true)
        {
            DownTime += Time.deltaTime;
            StartCoroutine(Rising());
        }
    }

    IEnumerator Rising()
    {
        if (EngineAllDown == false)
        {
            animator.SetBool("no engine front, Taika-Lai-Throtro 1", false);
            animator.SetBool("no engine back, Taika-Lai-Throtro 1", false);
            animator.SetBool("Im down!, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Im down!, Taika-Lai-Throtro 1", false);

            ImDown = false;
            DownTime = 0;
        }
        else
        {
            animator.SetBool("no engine front, Taika-Lai-Throtro 1", false);
            animator.SetBool("no engine back, Taika-Lai-Throtro 1", false);
            animator.SetBool("Im down! no engine, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Im down! no engine, Taika-Lai-Throtro 1", false);
            ImDown = false;
            DownTime = 0;
        }
    }

    //�ѿ��� ������ ����
    public IEnumerator GunDamage(int damage, float interval)
    {
        if (GunDown == false)
        {
            while (true)
            {
                GunHitPoint2 = GunHitPoint2 - damage;

                if (GunHitPoint2 <= float.Epsilon && GunDown == false && health2TaikaLaiThrotro1.hitPoints > 0)
                {
                    GunDown = true;
                    gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().GunDown = true;
                    gameObject.transform.GetComponent<Health2TaikaLaiThrotro1>().PartDeathGun();
                    health2TaikaLaiThrotro1.TearOn = true;
                    StopCoroutine(attack);
                    animator.SetBool("Warnning(small), Taika-Lai-Throtro 1", false);
                    animator.SetBool("Charge Karrgen-Arite 31", false);
                    animator.SetBool("Fire Karrgen-Arite 31", false);
                    animator.SetBool("Fire, Taika-Lai-Throtro 1", false);
                    animator.SetBool("No Karrgen-Arite 31, Taika-Lai-Throtro 1", true);
                    BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
                    BlueExplosion.transform.position = BlueExplosionPos.position;
                    BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
                    break;
                }

                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }

                else
                {
                    break;
                }
            }
        }
    }

    //���� ���
    IEnumerator RandomFire()
    {
        while (true)
        {
            if (IntoLine == false)
            {
                enemyShoot = Random.Range(0, shootingDelayOnEasyLevel);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
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


    void Update()
    {
        if (IntoLine == false)
        {
            if (magnetForm != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(magnetForm.position.x, magnetForm.position.y, transform.position.z), MagnetForce * Time.deltaTime); //Ư�� ������ �̵�
            }

            //����Ÿ�� �°� ��� ����� ��� ����
            if (ImDown == true)
            {
                currnetSpeed = 0;
                rb2d.velocity = Vector3.zero;
                enemyShoot = 10;
                MoveComplete = true;
            }

            //����Ÿ�� ���� ���� ���¿����� Ȱ��
            else if (ImDown == false)
            {
                if (EngineAllDown == false)
                {
                    Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
                    LookAtPlayer(); //�÷��̾� �ٶ󺸱�

                    float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

                    //�÷��̾ �ش� ������ ���� �ݰ��� ��Ż��, ����
                    if (distanceFromPlayer > lineOfSite && EngineAllDown == false)
                    {
                        MoveComplete = false;
                        animator.SetBool("Flying, Taika-Lai-Throtro 1", true);
                        currnetSpeed = runningSpeed;
                        transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                    }
                    else if (distanceFromPlayer < lineOfSite && EngineAllDown == false)
                        currnetSpeed = speed;

                    //������ �������� ���, ȸ��
                    if (distanceFromPlayer < avoidSite && EngineAllDown == false)
                    {
                        //avoid = true;
                        currnetSpeed = runningSpeed;
                        StartCoroutine(Avoid()); //ȸ��
                    }

                    if (GunDown == false)
                    {
                        //���
                        if (enemyShoot == 1 && ImDown == false && GunDown == false && charging == false && stopAndFire == false)
                        {
                            if (FireTime == 0)
                            {
                                FireTime += Time.deltaTime;
                                attack = StartCoroutine(Attack());
                                //Debug.Log("fire");
                            }
                        }

                        //���ݽ�, �÷��̾� ����
                        if (charging == true && EngineAllDown == false)
                            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime); //Ư�� ������ �̵�

                        //��� ������ �ӵ� ����
                        if (stopAndFire == true && EngineAllDown == false)
                            currnetSpeed = 0;
                    }
                }
            }
        }
        else
        {
            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�
            MoveComplete = false;
            animator.SetBool("Flying, Taika-Lai-Throtro 1", true);
            if (IntoLineSpeed == false)
                transform.Translate(transform.right * FollowVector * 20 * Time.deltaTime);
            else
            {
                currnetSpeed = runningSpeed;
                transform.Translate(transform.right * FollowVector * currnetSpeed * Time.deltaTime);
            }
            enemyShoot = 0;
        }
    }

    //ȸ��
    IEnumerator Avoid()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, -currnetSpeed * Time.deltaTime);
    }

    //�Ϲ� ��ȸ
    public IEnumerator ShootingMovement()
    {
        if (ImDown == false && EngineAllDown == false && IntoLine == false)
        {
            while (true)
            {
                float RandomMovement = Random.Range(0.25f, 3);
                float RandomWander = Random.Range(0.25f, 1);

                endposition = new Vector3(Random.Range(transform.position.x - RandomMovement, transform.position.x + RandomMovement),
                    Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);
                //Debug.Log("Moving : " + Moving);

                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }

                moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
                yield return new WaitForSeconds(RandomWander);
            }
        }
    }

    //������ ��ǥ�� ���� ������ ����
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endposition).sqrMagnitude;

        if (ImDown == false && EngineAllDown == false && IntoLine == false)
        {
            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    MoveComplete = false;
                    animator.SetBool("Flying, Taika-Lai-Throtro 1", true);
                    Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);

                    if (newPosition == endposition || transform.position == endposition) //��ǥ������ ���� ������ ������ ���, ���ڸ����� ����ä�� �̵� �ִϸ��̼��� Ȱ��ȭ�Ǵ� ������ ����
                    {
                        //Debug.Log(string.Format("newPosition : {0}, endposition : {1}, rigidbodyToMove.position : {2}", newPosition, endposition, rigidbodyToMove.position));
                        MoveComplete = true;
                        break;
                    }

                    rb2d.MovePosition(newPosition);
                    remainingDistance = (transform.position - endposition).sqrMagnitude;
                }
                yield return new WaitForFixedUpdate();

                if (transform.position == endposition)
                {
                    MoveComplete = true;
                }
            }
            animator.SetBool("Flying, Taika-Lai-Throtro 1", false);
        }
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if (Enemytarget.gameObject.activeSelf == true && stopAndFire == false)
        {
            if (Enemytarget.position.x < transform.position.x)
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

            if (transform.rotation.y == 0 && ImDown == false && EngineAllDown == false) //������ �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.583f);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", true);
                }
                else //����
                {
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.566f);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", true);
                }
            }
            else if (transform.rotation.y != 0 && ImDown == false && EngineAllDown == false) //�������� �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.566f);
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", true);
                }
                else //����
                {
                    animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", true);
                    yield return new WaitForSeconds(0.583f);
                    animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                    animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", true);
                }
            }
            if (MoveComplete == true || ImDown == true || EngineAllDown == true)
            {
                animator.SetBool("Flying, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving forward, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving forward now, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving back, Taika-Lai-Throtro 1", false);
                animator.SetBool("Moving back now, Taika-Lai-Throtro 1", false);
            }
        }
    }

    //���
    IEnumerator Attack()
    {
        GameObject RailGunAmmo;

        charging = true;
        SoundManager.instance.SFXPlay("Sound", Charging);
        animator.SetBool("Warnning plasma(small), Taika-Lai-Throtro 1", true);
        animator.SetBool("Charge Karrgen-Arite 31", true);
        yield return new WaitForSeconds(1f);
        charging = false;
        stopAndFire = true;
        yield return new WaitForSeconds(1f);
        animator.SetBool("Warnning plasma(small), Taika-Lai-Throtro 1", false);
        yield return new WaitForSeconds(0.5f);
        afterFire = true;
        animator.SetBool("Charge Karrgen-Arite 31", false);
        animator.SetBool("Fire Karrgen-Arite 31", true);
        SoundManager.instance.SFXPlay("Sound", FireSound1);
        RailGunAmmo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
        RailGunAmmo.GetComponent<PlazmaMovementTaikaLaiThrotro1>().SetDamage(Damage); //�Ѿ˿��� ������ ����
        GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
        Destroy(GunSmoke, 3);
        yield return new WaitForSeconds(1.5f);

        animator.SetBool("Fire Karrgen-Arite 31", false);
        enemyShoot = 2;
        stopAndFire = false;
        afterFire = false;
        FireTime = 0;
    }
}