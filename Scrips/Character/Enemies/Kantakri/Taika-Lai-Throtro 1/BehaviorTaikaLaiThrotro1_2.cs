using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTaikaLaiThrotro1_2 : MonoBehaviour
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

    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ

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

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
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

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //Debug.Log(avoid);

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
        if (stopAndFire == true)
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
        FireEffect(); //��� ȿ��
        SoundManager.instance.SFXPlay("Sound", FireSound1);
        RailGunAmmo = Instantiate(enemyAmmoPrefab, enemyAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
        RailGunAmmo.GetComponent<PlazmaMovementTaikaLaiThrotro1>().SetDamage(Damage); //�Ѿ˿��� ������ ����
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

    //���� ������ ȸ�� �ӵ� ����
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

    //��� ȿ��
    void FireEffect()
    {
        animator.SetBool("Karrgen-Arite 31 effect", true);
    }

    //��� ����
    void FireOut()
    {
        if (animator.GetBool("Karrgen-Arite 31 effect"))
            animator.SetBool("Karrgen-Arite 31 effect", false);
    }
}