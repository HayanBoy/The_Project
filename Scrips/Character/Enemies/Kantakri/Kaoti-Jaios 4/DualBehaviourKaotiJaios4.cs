using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBehaviourKaotiJaios4 : MonoBehaviour
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
    public int damage;

    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    private float waitWanderTime; //��ȸ�� ���� ���ӽð�
    private int wantToStop; //��ȸ�ϴ� ������ Ȯ��

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

    // public GameObject enemyFrontAmmoPrefab; //�߻� �Ѿ� ������


    string enemyFrontAmmoPrefab = "KaotiJaios4Ammo";
    string enemyFrontAmmoPrefab2 = "KaotiJaios4Ammo3";

    public Transform enemyFrontAmmoPos; //�Ѿ� ���� ��ǥ
    public Transform enemyFrontAmmoPos2; //�Ѿ� ���� ��ǥ

    // public GameObject enemyBackAmmoPrefab; //�߻� �Ѿ�2 ������
    string enemyBackAmmoPrefab = "KaotiJaios4Ammo2";
    string enemyBackAmmoPrefab2 = "KaotiJaios4Ammo4";

    public Transform enemyBackAmmoPos; //�Ѿ�2 ���� ��ǥ
    public Transform enemyBackAmmoPos2; //�Ѿ�2 ���� ��ǥ


    // public GameObject enemyFrontAmmoShellPrefab; //ź��1 ������

    string enemyFrontAmmoShellPrefab = "KaotiJaios4Shell1"; //ź��1 ������


    public Transform enemyFrontAmmoShellPos; //ź��1 ��ǥ

    // public GameObject enemyBackAmmoShellPrefab; //ź��2 ������
    string enemyBackAmmoShellPrefab = "KaotiJaios4Shell2";


    public Transform enemyBackAmmoShellPos; //ź�� ��ǥ

    public GameObject KaotiJaios4AmmoPrefab;
    public GameObject KaotiJaios4Ammo2Prefab;
    public GameObject KaotiJaios4Shell1Prefab;
    public GameObject KaotiJaios4Shell2Prefab;

    public GameObject KaotiJaios4Ammo3Prefab;
    public GameObject KaotiJaios4Ammo4Prefab;

    GameObject[] KaotiJaios4Ammo; //ī��Ƽ-���̿��� 4 �Ѿ� �� ����
    GameObject[] KaotiJaios4Ammo2;
    GameObject[] KaotiJaios4Ammo3; //ī��Ƽ-���̿��� 4 �Ѿ� �� ����
    GameObject[] KaotiJaios4Ammo4;
    GameObject[] KaotiJaios4Shell1; // ī��Ƽ-���̿��� 4 ź�� 1
    GameObject[] KaotiJaios4Shell2; // ī��Ƽ-���̿��� 4 ź�� 2
    GameObject[] PoolMaker;


    public GameObject smokePrefab; //���� ������
    public Transform smokePos; //���� ��ǥ
    public GameObject smoke2Prefab; //���� ������
    public Transform smoke2Pos; //���� ��ǥ
    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ
    public GameObject gunSmoke2Prefab; //��� ���� ������
    public Transform gunSmoke2Pos; //��� ���� ��ǥ
    public GameObject gunSmoke3Prefab; //��� ���� ������
    public Transform gunSmoke3Pos; //��� ���� ��ǥ
    public GameObject gunSmoke4Prefab; //��� ���� ������
    public Transform gunSmoke4Pos; //��� ���� ��ǥ

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

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�

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

        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(RandomFire()); //���� ���
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�

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
            else if (distanceFromPlayer < traceSite && distanceFromPlayer > avoidSite && avoid == false && outOfSide == true) //��ȸ �� Y�� �̵�
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

        //����
        if (ammo >= AmmoPerMagazine && reloading == false)
        {
            StartCoroutine(Reload());
            StartCoroutine(ReloadSmoke());
        }

        //��� �� ����
        if (afterFire == true)
        {
            afterFire = false;
            GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
            GameObject GunSmoke2 = Instantiate(gunSmoke2Prefab, gunSmoke2Pos.position, gunSmoke2Pos.rotation); //���� ����
            GameObject GunSmoke3 = Instantiate(gunSmoke3Prefab, gunSmoke3Pos.position, gunSmoke3Pos.rotation); //���� ����
            GameObject GunSmoke4 = Instantiate(gunSmoke4Prefab, gunSmoke4Pos.position, gunSmoke4Pos.rotation); //���� ����
            Destroy(GunSmoke, 2);
            Destroy(GunSmoke2, 2);
            Destroy(GunSmoke3, 2);
            Destroy(GunSmoke4, 2);
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
        if (player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //transform.Lookat(player.position) 360�� ȸ������ �÷��̾� �Ĵٺ���
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

    //�� �߻�
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

    //���
    //IEnumerator Attack()
    //{
    //    GameObject BackAmmo;
    //    animator.SetBool("Kaoti-Jaios 4 Firing", true);
    //    animator.SetBool("Kaoti-Jaios 4 Firing Eye", true);
    //    FireEffect(); //��� ȿ��
    //                  //FrontAmmo = Instantiate(enemyFrontAmmoPrefab, enemyFrontAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����

    //    FrontAmmo = objectManager.Loader(enemyFrontAmmoPrefab);
    //    FrontAmmo.transform.position = enemyFrontAmmoPos.position;
    //    FrontAmmo.transform.rotation = enemyFrontAmmoPos.rotation;
    //    FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //�Ѿ˿��� ������ ����

    //    //AmmoMovementKaotiJaios4 ammoKaot = FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>();
    //    //ammoKaot.objectManager = objectManager;

    //    EjectShell(); //ź�� ����
    //    yield return new WaitForSeconds(0.04f);

    //    BackAmmo = Instantiate(enemyBackAmmoPrefab, enemyBackAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
    //    BackAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //�Ѿ˿��� ������ ����
    //    EjectShellBack(); //ź�� �� ����
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

    public void Kaot4_Attack() // ** ���� �Լ� �� �̳ѷ����� ���� ���� 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing", true);
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", true);
        FireEffect(); //��� ȿ��
        RandomSound();

        GameObject FrontAmmo = Loader(enemyFrontAmmoPrefab);
        FrontAmmo.transform.position = enemyFrontAmmoPos.position;
        FrontAmmo.transform.rotation = transform.rotation;
        FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        GameObject FrontAmmo2 = Loader(enemyFrontAmmoPrefab2);
        FrontAmmo2.transform.position = enemyFrontAmmoPos2.position;
        FrontAmmo2.transform.rotation = transform.rotation;
        FrontAmmo2.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        EjectShell(); //ź�� ����
        Invoke("BackAmmo_ShellBack", 0.04f);
    }

    void BackAmmo_ShellBack()
    {
        GameObject BackAmmo = Loader(enemyBackAmmoPrefab);
        BackAmmo.transform.position = enemyBackAmmoPos.position;
        BackAmmo.transform.rotation = transform.rotation;
        BackAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //�Ѿ˿��� ������ ����

        GameObject BackAmmo2 = Loader(enemyBackAmmoPrefab2);
        BackAmmo2.transform.position = enemyBackAmmoPos2.position;
        BackAmmo2.transform.rotation = transform.rotation;
        BackAmmo2.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage);

        //Debug.Log("2�� �Ѿ� �߻�");
        //BackAmmo.transform.rotation = enemyBackAmmoPos.rotation;
        // BackAmmo = Instantiate(enemyBackAmmoPrefab, enemyBackAmmoPos.position, transform.rotation); //�߻� �Ѿ� ����

        EjectShellBack(); //ź�� �� ����
        afterFire = true;

        Invoke("FireAni_false", 0.26f);

    }

    void FireAni_false() // ** Fire �ִϸ��̼� false �Լ� 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect1", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect2", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect3", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect4", false);

        Invoke("ani_false", fireDelay);
    }

    void ani_false() // ** Fire ������ �Լ� 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);
        ammo += 2;
        fired = false;
    }

    //ź�� ����
    void EjectShell() // ** ù��° ź�� �����Լ� 
    {
        //GameObject enemyFrontAmmoShell = Instantiate(enemyFrontAmmoShellPrefab, enemyFrontAmmoShellPos.position, transform.rotation);

        GameObject enemyFrontAmmoShell = Loader(enemyFrontAmmoShellPrefab);
        enemyFrontAmmoShell.transform.position = enemyFrontAmmoShellPos.position;

        //enemyFrontAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        enemyFrontAmmoShell.GetComponent<ShellMovement>().xVnot = xVnot;
        enemyFrontAmmoShell.GetComponent<ShellMovement>().yVnot = yVnot;

        //  Destroy(enemyFrontAmmoShell, 15.0f); //�����ð���, ������ ź�� ����
    }

    //ź�� �� ����
    void EjectShellBack() // ** �ι�° ź�� �����Լ� 
    {
        GameObject enemyBackAmmoShell = Loader(enemyBackAmmoShellPrefab);
        enemyBackAmmoShell.transform.position = enemyFrontAmmoShellPos.position;

        //enemyBackAmmoShell.transform.rotation = enemyFrontAmmoShellPos.rotation;

        //GameObject enemyBackAmmoShell = Instantiate(enemyBackAmmoShellPrefab, enemyBackAmmoShellPos.position, transform.rotation);
        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        enemyBackAmmoShell.GetComponent<ShellMovement>().xVnot = xVnot;
        enemyBackAmmoShell.GetComponent<ShellMovement>().yVnot = yVnot;

        // Destroy(enemyBackAmmoShell, 15.0f); //�����ð���, ������ ź�� ���� 
        // (ó������################################)
    }

    //��� ȿ��
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

    //����
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

    //���� ���� �߻�
    IEnumerator ReloadSmoke()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //���� ����
        GameObject Smoke2 = Instantiate(smoke2Prefab, smoke2Pos.position, smoke2Pos.rotation); //���� ����
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