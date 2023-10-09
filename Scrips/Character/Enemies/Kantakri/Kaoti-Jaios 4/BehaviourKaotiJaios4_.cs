using System.Collections;
using UnityEngine;

public class BehaviourKaotiJaios4_ : MonoBehaviour
{
    ObjectManager objectManager;
    KaotiJaios4 kaotiJaios4;
    Rigidbody2D rb2d;
    Animator animator;

    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    private float DownTime; //�ٿ�� ���¿��� ġ��Ÿ�� �� ���� �� �ൿ�� ������ �ʱ�ȭ �Ǵ� ���� ������ �� �ѹ��� �ٿ�Ǵ� ������ �����ϱ� ���� ����
    public float FollowVector; //���� ���� �÷��̾� �������� �̵��ϱ� ���� ����

    private Vector2 target; //��� ���� ��ġ
    Vector3 endposition; //���� ������ ��ġ��

    public int Damage; //���� �߻�� ���ط�
    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    public int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    public float WheelHitPoint; //���� ü��. 0�� �Ǹ� ������ ���ư��鼭 �̵��� ����ȭ�ȴ�.
    public float GunHitPoint; //�� ü��. ü���� 50%�� �Ǹ� ���� �ϳ� ���ư���, ü���� 0�� �Ǹ� ������ �ѱ��� ���ư��鼭 ����� ����ȭ�ȴ�.
    private float GunHitPointTotal; //�� ��ü ü��
    private int GunDown; //�������� ���� ���ư��� ����
    private float GunHitPoint2; //�������� ü��
    private float WheelHitPoint2; //�������� ü��

    int fireEffect;
    int ammo = 0;
    public int AmmoPerMagazine;

    public bool IntoLine = false; //���� ����, �÷��̾ �ִ� �������� �����ϱ� ���� ����ġ
    public bool IntoLineSpeed = false; //���� ����, �÷��̾� ���� ��ó�� ���� �� ������ �ӵ��� ����
    public bool fired = false; //������� �� ����ġ
    public bool reloading = false; //���������� �� ����ġ
    private bool afterFire = false; //����� �� ���� ������ ����ġ
    private bool MoveComplete = false; //�������� ������ ���� ����ġ
    public bool WheelDown = false; //������ ����ȭ �Ǿ��� ���� ����ġ
    public bool Gun1Down = false; //1�� ���� ���ư��� �۵��Ǵ� ����ġ
    private bool GunFirst1Down = false; //1�� ���� ���ư� ��, 2�� ���� ���ư����� �۵��Ǵ� ����ġ
    public bool Gun2Down = false; //2�� ���� ���ư��� �۵��Ǵ� ����ġ
    private bool GunFirst2Down = false; //2�� ���� ���ư� ��, 1�� ���� ���ư����� �۵��Ǵ� ����ġ
    private bool GunFirstDown = false; //ù ���� ���ư��� ���� ����ġ
    private bool GunFinalDown = false; //��� ���� ���ư��� ���� ����ġ
    public bool ImDown = false; //ġ��Ÿ�� �¾� ��� ����� ��� ������ ���� ����ġ
    public bool Trigger;

    public Transform GunExplosionPos;
    public Transform WheelExplosionPos;
    GameObject GunExplosion;
    GameObject WheelExplosion;

    public Transform enemyFrontAmmoPos; //�Ѿ� ���� ��ǥ
    public Transform enemyBackAmmoPos; //�Ѿ�2 ���� ��ǥ
    public Transform enemyFrontAmmoShellPos; //ź��1 ��ǥ
    public Transform enemyBackAmmoShellPos; //ź�� ��ǥ

    public GameObject smokePrefab; //���� ������
    public Transform smokePos; //���� ��ǥ
    public GameObject smoke2Prefab; //���� ������
    public Transform smoke2Pos; //���� ��ǥ
    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ
    public GameObject gunSmoke2Prefab; //��� ���� ������
    public Transform gunSmoke2Pos; //��� ���� ��ǥ

    Coroutine moveCoroutine;

    public Transform Enemytarget = null;
    public Vector2 size;

    public Transform magnetForm = null;
    public float MagnetForce;

    private int RicochetSoundRandom;
    public AudioClip FireSound1;
    public AudioClip FireSound2;
    public AudioClip FireSound3;
    public AudioClip FireSound4;
    public AudioClip FireSound5;

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

    //�ѿ��� ������ ����
    public IEnumerator GunDamage(int damage, float interval)
    {
        if (GunFinalDown == false)
        {
            while (true)
            {
                GunHitPoint2 = GunHitPoint2 - damage;

                if (GunFirstDown == false)
                {
                    if (GunHitPoint2 <= GunHitPointTotal / 2)
                    {
                        GunFirstDown = true;
                        GunDown = Random.Range(0, 2);

                        if (GunDown == 0 && kaotiJaios4.hitPoints > 0)
                        {
                            Gun1Down = true;
                            GunFirst1Down = true;
                            kaotiJaios4.TearOn = true;
                            GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
                            GunExplosion.transform.position = GunExplosionPos.position;
                            GunExplosion.transform.rotation = GunExplosionPos.rotation;
                            animator.SetBool("Kaoti-Jaios 4 No Gun1", true);
                            gameObject.transform.GetComponent<KaotiJaios4>().Gun1Down = true;
                            gameObject.transform.GetComponent<KaotiJaios4>().PartDeathGun1();
                        }
                        else if (GunDown == 1 && kaotiJaios4.hitPoints > 0)
                        {
                            Gun2Down = true;
                            GunFirst2Down = true;
                            kaotiJaios4.TearOn = true;
                            GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
                            GunExplosion.transform.position = GunExplosionPos.position;
                            GunExplosion.transform.rotation = GunExplosionPos.rotation;
                            animator.SetBool("Kaoti-Jaios 4 No Gun2", true);
                            gameObject.transform.GetComponent<KaotiJaios4>().Gun2Down = true;
                            gameObject.transform.GetComponent<KaotiJaios4>().PartDeathGun2();
                        }
                        GunDown = 5;
                    }
                }

                if (GunHitPoint2 <= float.Epsilon)
                {
                    if (GunFirst1Down == true && kaotiJaios4.hitPoints > 0)
                    {
                        GunFirst1Down = false;
                        Gun2Down = true;
                        kaotiJaios4.TearOn = true;
                        GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
                        GunExplosion.transform.position = GunExplosionPos.position;
                        GunExplosion.transform.rotation = GunExplosionPos.rotation;
                        animator.SetBool("Kaoti-Jaios 4 No Gun All", true);
                        gameObject.transform.GetComponent<KaotiJaios4>().Gun2Down = true;
                        gameObject.transform.GetComponent<KaotiJaios4>().PartDeathGun2();
                    }
                    else if (GunFirst2Down == true && kaotiJaios4.hitPoints > 0)
                    {
                        GunFirst2Down = false;
                        Gun1Down = true;
                        kaotiJaios4.TearOn = true;
                        GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
                        GunExplosion.transform.position = GunExplosionPos.position;
                        GunExplosion.transform.rotation = GunExplosionPos.rotation;
                        animator.SetBool("Kaoti-Jaios 4 No Gun All", true);
                        gameObject.transform.GetComponent<KaotiJaios4>().Gun1Down = true;
                        gameObject.transform.GetComponent<KaotiJaios4>().PartDeathGun1();
                    }

                    GunFinalDown = true;
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

    //�������� ������ ����
    public IEnumerator WheelDamage(int damage, float interval)
    {
        if(WheelDown == false)
        {
            while (true)
            {
                WheelHitPoint2 = WheelHitPoint2 - damage;

                if (WheelHitPoint2 <= float.Epsilon && kaotiJaios4.hitPoints > 0)
                {
                    animator.SetBool("Kaoti-Jaios 4 Firing", false);
                    animator.SetBool("Kaoti-Jaios 4 Firing2", false);
                    WheelExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
                    WheelExplosion.transform.position = WheelExplosionPos.position;
                    WheelExplosion.transform.rotation = WheelExplosionPos.rotation;
                    animator.SetBool("Kaoti-Jaios 4 Wheel Down", true);
                    StartCoroutine(WheelTakeDown());
                    gameObject.transform.GetComponent<KaotiJaios4>().WheelDown = true;
                    gameObject.transform.GetComponent<KaotiJaios4>().PartDeathWheel();
                    WheelDown = true;
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
    IEnumerator WheelTakeDown()
    {
        yield return new WaitForSeconds(0.66f);
        animator.SetBool("Kaoti-Jaios 4 Wheel Down2", true);
        animator.SetBool("Kaoti-Jaios 4 Wheel Down", false);
    }

    //ġ��Ÿ�� �¾��� �� �ٿ�Ǿ��ٰ� �ٽ� �Ͼ��
    public void TakeDown(bool boolean)
    {
        if (boolean == true)
        {
            if (DownTime == 0)
            {
                ImDown = boolean;
                DownTime += Time.deltaTime;
                StartCoroutine(Rising());
            }
        }
    }
    IEnumerator Rising()
    {
        if (WheelDown == false)
        {
            animator.SetBool("Kaoti-Jaios 4 Reloading", false);
            animator.SetBool("Kaoti-Jaios 4 ImDown!", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Kaoti-Jaios 4 ImDown!", false);
            ImDown = false;
            DownTime = 0;
        }
        else
        {
            animator.SetBool("Kaoti-Jaios 4 Reloading", false);
            animator.SetBool("Kaoti-Jaios 4 Wheel Down ImDown!", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Kaoti-Jaios 4 Wheel Down ImDown!", false);
            ImDown = false;
            DownTime = 0;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        objectManager = FindObjectOfType<ObjectManager>();
        kaotiJaios4 = FindObjectOfType<KaotiJaios4>();
        GunHitPointTotal = GunHitPoint;
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        SingletonObject.instance.KaotiFirePos.Add(enemyFrontAmmoPos);

        if (BattleSave.Save1.MissionLevel == 1)
        {
            if (GetComponent<KaotiJaios4>().Number == 1) //ī��Ƽ-���̿��� 4(�⺻)
            {
                Damage = 20;
                fireDelay = 0.5f;
                enemyShoot = 4;
                AmmoPerMagazine = 10;
            }
            else if (GetComponent<KaotiJaios4>().Number == 2) //ī��Ƽ-���̿��� 4(Kantarr-Jaeratroy Boss fleet)
            {
                Damage = 40;
                fireDelay = 0.5f;
                enemyShoot = 4;
                AmmoPerMagazine = 14;
            }
            else if (GetComponent<KaotiJaios4>().Number == 3) //ī��Ƽ-���̿��� 4(Kantarr-Jaeratroy Boss fleet Armor)
            {
                Damage = 25;
                fireDelay = 0.7f;
                enemyShoot = 4;
                AmmoPerMagazine = 8;
            }
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            if (GetComponent<KaotiJaios4>().Number == 1)
            {
                Damage = 28;
                fireDelay = 0.4f;
                enemyShoot = 3;
                AmmoPerMagazine = 14;
            }
            else if (GetComponent<KaotiJaios4>().Number == 2)
            {
                Damage = 56;
                fireDelay = 0.3f;
                enemyShoot = 3;
                AmmoPerMagazine = 18;
            }
            else if (GetComponent<KaotiJaios4>().Number == 3)
            {
                Damage = 35;
                fireDelay = 0.6f;
                enemyShoot = 3;
                AmmoPerMagazine = 12;
            }
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            if (GetComponent<KaotiJaios4>().Number == 1)
            {
                Damage = 32;
                fireDelay = 0.35f;
                enemyShoot = 2;
                AmmoPerMagazine = 18;
            }
            else if (GetComponent<KaotiJaios4>().Number == 2)
            {
                Damage = 64;
                fireDelay = 0.25f;
                enemyShoot = 2;
                AmmoPerMagazine = 24;
            }
            else if (GetComponent<KaotiJaios4>().Number == 3)
            {
                Damage = 40;
                fireDelay = 0.55f;
                enemyShoot = 2;
                AmmoPerMagazine = 16;
            }
        }

        WheelHitPoint = GetComponent<KaotiJaios4>().startingHitPoints / 4;
        GunHitPoint = GetComponent<KaotiJaios4>().startingHitPoints / 4;
        WheelHitPoint2 = WheelHitPoint;
        GunHitPoint2 = GunHitPoint;

        fired = false;
        reloading = false;
        afterFire = false;
        MoveComplete = false;
        WheelDown = false;
        Gun1Down = false;
        GunFirst1Down = false;
        Gun2Down = false;
        GunFirst2Down = false;
        GunFirstDown = false;
        GunFinalDown = false;
        ImDown = false;
        GunDown = 5;
        ammo = 0;

        currnetSpeed = speed;
        StartCoroutine(RandomFire());
        StartCoroutine(ShootingMovement());
        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
    }

    private void OnDisable()
    {
        SingletonObject.instance.KaotiFirePos.Remove(enemyFrontAmmoPos);
        if (animator.GetBool("Kaoti-Jaios 4 moving") == true)
            animator.SetBool("Kaoti-Jaios 4 moving", false);
        if (animator.GetBool("Kaoti-Jaios 4 ImDown!") == true)
            animator.SetBool("Kaoti-Jaios 4 ImDown!", false);
        if (animator.GetBool("Kaoti-Jaios 4 Wheel Down ImDown!") == true)
            animator.SetBool("Kaoti-Jaios 4 Wheel Down ImDown!", false);
        if (animator.GetBool("Kaoti-Jaios 4 No Gun1") == true)
            animator.SetBool("Kaoti-Jaios 4 No Gun1", false);
        if (animator.GetBool("Kaoti-Jaios 4 No Gun2") == true)
            animator.SetBool("Kaoti-Jaios 4 No Gun2", false);
        if (animator.GetBool("Kaoti-Jaios 4 No Gun All") == true)
            animator.SetBool("Kaoti-Jaios 4 No Gun All", false);
        if (animator.GetBool("Kaoti-Jaios 4 Wheel Down") == true)
            animator.SetBool("Kaoti-Jaios 4 Wheel Down", false);
        if (animator.GetBool("Kaoti-Jaios 4 Wheel Down2") == true)
            animator.SetBool("Kaoti-Jaios 4 Wheel Down2", false);
        if (animator.GetBool("Kaoti-Jaios 4 Reloading") == true)
            animator.SetBool("Kaoti-Jaios 4 Reloading", false);
    }

    //���� ���
    IEnumerator RandomFire()
    {
        if (ImDown == false)
        {
            while (true)
            {
                enemyShoot = Random.Range(1, shootingDelayOnEasyLevel);
                yield return new WaitForSeconds(1f);
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

            //Debug.DrawLine(rb2d.position, endposition, Color.red);

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
                if (WheelDown == false)
                {
                    Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
                    LookAtPlayer(); //�÷��̾� �ٶ󺸱�

                    float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

                    //�÷��̾ �ش� ������ ���� �ݰ��� ��Ż��, ����
                    if (distanceFromPlayer > lineOfSite && WheelDown == false)
                    {
                        MoveComplete = false;
                        animator.SetBool("Kaoti-Jaios 4 moving", true);
                        currnetSpeed = runningSpeed;
                        transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                    }
                    else if (distanceFromPlayer < lineOfSite && WheelDown == false)
                        currnetSpeed = speed;
                }

                //���
                if (Trigger)
                {
                    if (enemyShoot == 1 && fired == false && reloading == false)
                    {
                        fired = true;

                        if (WheelDown == false)
                            animator.SetBool("Kaoti-Jaios 4 Firing", true);
                        else
                            animator.SetBool("Kaoti-Jaios 4 Firing2", true);
                        animator.SetBool("Kaoti-Jaios 4 Firing Eye", true);
                        RandomSound();

                        if (Gun1Down == false)
                            Kaot4_Attack();
                        if (Gun2Down == false)
                            Invoke("BackAmmo_ShellBack", 0.04f);
                        if (Gun1Down == false || Gun2Down == false)
                        {
                            afterFire = true;
                        }
                    }
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
                    if (Gun1Down == false)
                    {
                        GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
                        Destroy(GunSmoke, 2);
                    }
                    else if (Gun2Down == false)
                    {
                        GameObject GunSmoke2 = Instantiate(gunSmoke2Prefab, gunSmoke2Pos.position, gunSmoke2Pos.rotation); //���� ����
                        Destroy(GunSmoke2, 2);
                    }
                }
            }
        }
        else
        {
            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�
            MoveComplete = false;
            animator.SetBool("Kaoti-Jaios 4 moving", true);
            if (IntoLineSpeed == false)
                transform.Translate(transform.right * FollowVector * 20 * Time.deltaTime);
            else
            {
                currnetSpeed = runningSpeed;
                transform.Translate(transform.right * FollowVector * currnetSpeed * Time.deltaTime);
            }
        }
    }

    //�Ϲ� ��ȸ
    public IEnumerator ShootingMovement()
    {
        if (ImDown == false && WheelDown == false && IntoLine == false)
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

        if (ImDown == false && WheelDown == false && IntoLine == false)
        {
            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    MoveComplete = false;
                    animator.SetBool("Kaoti-Jaios 4 moving", true);
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
            animator.SetBool("Kaoti-Jaios 4 moving", false);
        }
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if(Enemytarget.gameObject.activeSelf == true)
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

            if (transform.rotation.y == 0 && ImDown == false) //������ �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", true);
                }
                else //����
                {
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", true);
                }
            }
            else if (transform.rotation.y != 0 && ImDown == false) //�������� �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", true);
                }
                else //����
                {
                    animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
                    animator.SetBool("Kaoti-Jaios 4 Wheel moving", true);
                }
            }
            if (MoveComplete == true || WheelDown == true)
            {
                animator.SetBool("Kaoti-Jaios 4 moving", false);
                animator.SetBool("Kaoti-Jaios 4 Wheel moving", false);
                animator.SetBool("Kaoti-Jaios 4 Moving Back", false);
            }
        }
    }

    public void Kaot4_Attack() // ** ���� �Լ� �� �̳ѷ����� ���� ���� 
    {
        FireEffect2(); //��� ȿ��
        GameObject FrontAmmo = SingletonObject.instance.Loader_ammo();
        FrontAmmo.transform.position = enemyFrontAmmoPos.position;
        FrontAmmo.transform.rotation = transform.rotation;
        FrontAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //�Ѿ˿��� ������ ����

        EjectShell(); //ź�� ����
        Invoke("FireAni_false2", 0.26f);
    }

    void BackAmmo_ShellBack()
    {
        //Debug.Log("2�� �Ѿ� �߻�");
        FireEffect(); //��� ȿ��
        GameObject BackAmmo = SingletonObject.instance.Loader_backammo();
        BackAmmo.transform.position = enemyBackAmmoPos.position;
        BackAmmo.transform.rotation = transform.rotation;
        BackAmmo.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(Damage); //�Ѿ˿��� ������ ����

        EjectShellBack(); //ź�� �� ����
        Invoke("FireAni_false", 0.26f);
    }

    void FireAni_false() // ** Fire �ִϸ��̼� false �Լ� 
    {
        if (WheelDown == false)
            animator.SetBool("Kaoti-Jaios 4 Firing", false);
        else
            animator.SetBool("Kaoti-Jaios 4 Firing2", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect1", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect2", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect3", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect4", false);

        Invoke("ani_false", fireDelay);
    }

    void FireAni_false2() // ** Fire �ִϸ��̼� false �Լ� 
    {
        if (WheelDown == false)
            animator.SetBool("Kaoti-Jaios 4 Firing", false);
        else
            animator.SetBool("Kaoti-Jaios 4 Firing2", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect1 other", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect2 other", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect3 other", false);
        animator.SetBool("Kaoti-Jaios 4 Firing Effect4 other", false);

        Invoke("ani_false", fireDelay);
    }

    void ani_false() // ** Fire ������ �Լ� 
    {
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);

        if(Gun1Down == false && Gun2Down == false)
        {
            ammo += 2;
        }
        else if(Gun1Down == true || Gun2Down == true)
        {
            ammo += 1;
        }
        fired = false;
    }

    //ź�� ����
    void EjectShell() // ** ù��° ź�� �����Լ� 
    {
        GameObject enemyFrontAmmoShell = SingletonObject.instance.Loader_FrontShell();
        enemyFrontAmmoShell.transform.position = enemyFrontAmmoShellPos.position;
        ShellMovement ShellMovement = enemyFrontAmmoShell.GetComponent<ShellMovement>();
        ShellMovement.Pos = enemyFrontAmmoShell.transform.position.y;
    }

    //ź�� �� ����
    void EjectShellBack() // ** �ι�° ź�� �����Լ� 
    {
        GameObject enemyBackAmmoShell = SingletonObject.instance.Loader_BackShell();
        enemyBackAmmoShell.transform.position = enemyFrontAmmoShellPos.position;
        ShellMovement ShellMovement2 = enemyBackAmmoShell.GetComponent<ShellMovement>();
        ShellMovement2.Pos = enemyBackAmmoShell.transform.position.y;
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

    void FireEffect2()
    {
        fireEffect = Random.Range(1, 4);

        if (fireEffect == 1)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect1 other", true);
        }
        else if (fireEffect == 2)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect2 other", true);
        }
        else if (fireEffect == 3)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect3 other", true);
        }
        else if (fireEffect == 4)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing Effect4 other", true);
        }
    }

    //����
    IEnumerator Reload()
    {
        reloading = true;
        animator.SetBool("Kaoti-Jaios 4 Reloading", true);
        animator.SetBool("Kaoti-Jaios 4 Firing Eye", false);

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
}