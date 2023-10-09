using System.Collections;
using UnityEngine;

public class BehaviorAsoShiioshare : MonoBehaviour
{
    ObjectManager objectManager;
    Rigidbody2D rb2d;
    Animator animator;

    private Shake shake;

    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float NearAttackSite; //���������� ���Ǵ� ����ũ��
    private float DownTime; //�ٿ�� ���¿��� ġ��Ÿ�� �� ���� �� �ൿ�� ������ �ʱ�ȭ �Ǵ� ���� ������ �� �ѹ��� �ٿ�Ǵ� ������ �����ϱ� ���� ����
    private float SkillCoolTime; //��ų �����ϱ� ���� ��Ÿ��
    private float SkillOnAccount; //��ų�� ������ �� SkillTime�� �ѹ��� ������ ���� ����
    private float NearTime; //�÷��̾ �������� �� ���� ���� �Լ� �ڷ�ƾ�� �ѹ��� Ȱ��ȭ�ϱ� ���� ����
    private float SubFireRate = 0.183f; //�������� ����� 
    private float SubTimeStamp; //�������� ���� �����ϱ� ���� ����
    private float SoundOne; //���带 �� ���� ���ǵ��� ����
    public float FollowVector; //���� ���� �÷��̾� �������� �̵��ϱ� ���� ����
    float distanceFromPlayerDot;

    public GameObject DamagePos;
    private Transform player = null; //�÷��̾� ���� ��ġ
    private Vector2 target; //��� ���� ��ġ
    Vector3 endposition; //���� ������ ��ġ��
    Vector3 PlayerPosition; //�÷��̾� �� �Ʊ� ��ġ ����

    public int Damage; //���� �ֹ��� �߻�� ���ط�
    public int SubDamage; //���� �������� �߻�� ���ط�
    public int SkillDamage; //���� ��ų ���ط�
    public int ShortAttackDamage; //���� ������
    public float fireDelay; //�ּ� �߻� ����
    public int shootingDelayOnEasyLevel; //���̵��� ���� ���� ����
    public int SkillRandomLevel; //��ų ��Ÿ���� �� �������� ��ų�� �����ϱ� ���� 1�ʸ��� �������� ������ ����. �� ���ڰ� 1�� �Ǹ� ��ų�� �����Ѵ�.
    public int NearAttackLevel; //NearAttackTime�� �ִ� ��������
    public int SkillCoolTimes; //��ų ��Ÿ���� �� ���µ� �ɸ��� ��ü�ð�. �� ���ڸ� �����ϸ� ��ų�� �� �� �Ŀ� ���������� ������ �� �ִ�.
    private int enemyShoot; //����Ȯ���� ���� ����
    private int moveDown; //����Ȯ���� �����ϱ� ���� �����Լ�
    private int SkillTime; //��ų ��Ÿ���� á�� ��, ��ų�� �����ϱ� ���� �����Լ�
    private int NearAttackRandom; //�������� ��� �����Լ�
    private int NearAttackTime; //�������� ���� �����Լ�

    int fireEffect;
    int ammo = 0;
    public int AmmoPerMagazine;

    public bool IntoLine = false; //���� ����, �÷��̾ �ִ� �������� �����ϱ� ���� ����ġ
    public bool IntoLineSpeed = false; //���� ����, �÷��̾� ���� ��ó�� ���� �� ������ �ӵ��� ����
    private bool SkillOn = false; //��ų����� �� �� ����ġ
    private bool SkillUsing = false; //��ų������� �� ����ġ
    private bool fired = false; //������� �� ����ġ
    private bool reloading = false; //���������� �� ����ġ
    private bool afterFire = false; //����� �� ���� ������ ����ġ
    private bool MoveComplete = false; //�������� ������ ���� ����ġ
    private bool SkillChasing = false; //��ų������ �� �÷��̾ �i�ư��� ����ġ
    private bool SkillFiring = false; //��ų ������ �ϰ� ���� �� ����ġ. �� ���¿��� �÷��̾� �Ĵٺ��⸦ �ߴ�.
    private bool SwordAcitve = false; //���� Į Ȱ��ȭ ����ġ
    private bool NearAttack = false; //���� ���� Ȱ��ȭ
    private bool NearAttackRunning = false; //���� ���ݽ� �޷��� �� ����ġ
    private bool NearAttacking = false; //���� �����ϰ� ���� �� ����ġ
    private bool deathStop = false; //�׾��� ��, �������� ��������� �ϴ� ����ġ
    private bool LegsDown = false; //�ٸ��� ����ȭ �Ǿ��� ���� ����ġ
    private bool MainGunDown = false; //�ֹ��Ⱑ ����ȭ �Ǿ��� ���� ����ġ
    private bool Arm1Down = false;
    private bool Arm2Down = false;
    private bool Arm3Down = false; //�ҵ� ���� ����ȭ �Ǿ��� ���� ����ġ
    private bool SubWeaponRight = false; //�������� ������ Ȱ��ȭ ����ġ
    private bool SubWeaponLeft = false; //�������� ���� Ȱ��ȭ ����ġ
    private bool SubWeaponRightAlready = false; //�������� �������� �̹� Ȱ��ȭ �� ������ ����ġ
    private bool SubWeaponLeftAlready = false; //�������� ������ �̹� Ȱ��ȭ �� ������ ����ġ
    private bool Leg1Down = false;
    private bool Leg2Down = false;
    private bool DownMark = false;
    private bool ImHit = false;
    private bool StillDown = false; //�ٸ��� �߷��� �Ѿ��� �� �ٸ� �ൿ�� ������ �ߴ�

    public GameObject AmmoPrefab; //�ֹ��� �Ѿ� ������
    public GameObject AmmoPrefab2; //�ֹ��� �Ѿ� ������(�ٿ� �Ǿ��� ��)
    public GameObject AmmoSubRPrefab; //��������(������) �Ѿ� ������
    public GameObject AmmoSubRPrefab2; //��������(������) �Ѿ� ������(�ٿ� �Ǿ��� ��)
    public GameObject AmmoSubLPrefab; //��������(����) �Ѿ� ������(�ٿ� �Ǿ��� ��)
    public GameObject SkillAmmoPrefab; //��ų �Ѿ� ������
    public Transform AmmoPos; //�Ѿ� ���� ��ǥ
    public GameObject AmmoFireEffectPrefab; //�ֹ��� �Ѿ� �߻� ����Ʈ ������
    public Transform AmmoFireEffectPos; //�Ѿ� ���� ��ǥ

    public GameObject smokePrefab; //���� ������
    public Transform smokePos; //���� ��ǥ
    public GameObject gunSmokePrefab; //��� ���� ������
    public Transform gunSmokePos; //��� ���� ��ǥ

    Coroutine FlameMove;
    Coroutine FlamemoveCoroutine;
    Vector3 FlameEndposition; //���� ������ ��ġ��
    public bool FlameDeath = false; //�ҿ� ź ���¿��� ���� ��
    private float FlameTime;

    Coroutine moveCoroutine;
    Coroutine randomskillfire;
    Coroutine randomNearAttack;
    Coroutine skillAttack;
    Coroutine attack1Go;
    Coroutine attack2Go;
    Coroutine pickSubWeaponArm1Up;
    Coroutine pickSubWeaponArm2Up;

    private bool RandomskillfireBool = false;
    private bool RandomNearAttackBool = false;
    private bool SkillAttackBool = false;
    private bool Attack1GoBool = false;
    private bool Attack2GoBool = false;
    private bool PickSubWeaponArm1UpBool = false;
    private bool PickSubWeaponArm2UpBool = false;
    public bool Trigger;

    public Transform Enemytarget = null;
    public Vector2 size;

    public Transform magnetForm = null;
    public float MagnetForce;

    public GameObject Sounds;
    public GameObject RifleSkillCharge;
    public AudioClip RifleFire;
    public AudioClip RifleReload;
    public AudioClip RifleSkillFire;
    public AudioClip SubgunOn;
    public AudioClip SubgunFire;
    public AudioClip SwordOn;
    public AudioClip SwordAttack1;
    public AudioClip SwordAttack2;

    public void AsoShiioshareDeath(bool droping)
    {
        if (droping == true)
        {
            deathStop = true;
        }
        else
        {
            deathStop = false;
        }
    }

    public void AsoShiioshareGunDown(bool Down)
    {
        if (Down == true)
        {
            MainGunDown = true;
        }
        else
        {
            MainGunDown = false;
        }
    }

    public void AsoShiioshareArm1Down(bool Down)
    {
        if (Down == true)
        {
            Arm1Down = true; //������ ����ȭ
            SubWeaponRightAlready = true;
            SubWeaponRight = false;
            fired = false;
            transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(false);

            if (PickSubWeaponArm1UpBool == true)
                StopCoroutine(pickSubWeaponArm1Up);

            if (SubWeaponLeftAlready == false)
            {
                //Debug.Log("���� �������� Ȱ��ȭ");
                PickSubWeaponArm2UpBool = true;
                pickSubWeaponArm2Up = StartCoroutine(PickSubWeaponArm2Up()); //���� Ȱ��ȭ
            }
        }
        else
        {
            Arm1Down = false;
        }
    }

    public void AsoShiioshareArm2Down(bool Down)
    {
        if (Down == true)
        {
            Arm2Down = true; //���� ����ȭ
            SubWeaponLeftAlready = true;
            SubWeaponLeft = false;
            fired = false;
            transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(false);

            if (PickSubWeaponArm2UpBool == true)
                StopCoroutine(pickSubWeaponArm2Up);

            if (SubWeaponRightAlready == false)
            {
                //Debug.Log("������ �������� Ȱ��ȭ");
                PickSubWeaponArm1UpBool = true;
                pickSubWeaponArm1Up = StartCoroutine(PickSubWeaponArm1Up()); //������ Ȱ��ȭ
            }
        }
        else
        {
            Arm2Down = false;
        }
    }

    public void AsoShiioshareSwordDown(bool Down)
    {
        if (Down == true)
        {
            Arm3Down = true;
            transform.Find("Aso Shiioshare Flasma sword").gameObject.SetActive(false);
        }
        else
        {
            Arm3Down = false;
        }
    }

    public void AsoShiioshareLeg1Down(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("Leg1Down : " + Leg1Down);
            Leg1Down = true;
        }
        else
        {
            Leg1Down = false;
        }
    }

    public void AsoShiioshareLeg2Down(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("Leg2Down : " + Leg2Down);
            Leg2Down = true;
        }
        else
        {
            Leg2Down = false;
        }
    }

    public void AsoShiioshareImDown(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("I'm down!");
            StartCoroutine(ImDown());
        }
    }

    public void AsoShiioshareImHit(bool Down)
    {
        if (Down == true)
        {
            ImHit = true;
        }
        else
        {
            ImHit = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        objectManager = FindObjectOfType<ObjectManager>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            Damage = 35;
            SubDamage = 20;
            SkillDamage = 6;
            ShortAttackDamage = 60;
            shootingDelayOnEasyLevel = 4;
            SkillRandomLevel = 5;
            NearAttackLevel = 4;
            SkillCoolTimes = 7;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            Damage = 45;
            SubDamage = 26;
            SkillDamage = 8;
            ShortAttackDamage = 78;
            shootingDelayOnEasyLevel = 3;
            SkillRandomLevel = 4;
            NearAttackLevel = 3;
            SkillCoolTimes = 7;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            Damage = 56;
            SubDamage = 32;
            SkillDamage = 10;
            ShortAttackDamage = 105;
            shootingDelayOnEasyLevel = 2;
            SkillRandomLevel = 3;
            NearAttackLevel = 2;
            SkillCoolTimes = 7;
        }

        if (fired == true)
            fired = false;
        if (reloading == true)
            reloading = false;
        if (afterFire == true)
            afterFire = false;
        if (MoveComplete == true)
            MoveComplete = false;
        if (SkillOn == true)
            SkillOn = false;
        if (SkillUsing == true)
            SkillUsing = false;
        if (SkillChasing == true)
            SkillChasing = false;
        if (SkillFiring == true)
            SkillFiring = false;
        if (SwordAcitve == true)
            SwordAcitve = false;
        if (NearAttack == true)
            NearAttack = false;
        if (NearAttackRunning == true)
            NearAttackRunning = false;
        if (NearAttacking == true)
            NearAttacking = false;
        if (LegsDown == true)
            LegsDown = false;
        if (MainGunDown == true)
            MainGunDown = false;
        if (Arm1Down == true)
            Arm1Down = false;
        if (Arm2Down == true)
            Arm2Down = false;
        if (Arm3Down == true)
            Arm3Down = false;
        if (SubWeaponRight == true)
            SubWeaponRight = false;
        if (SubWeaponLeft == true)
            SubWeaponLeft = false;
        if (SubWeaponRightAlready == true)
            SubWeaponRightAlready = false;
        if (SubWeaponLeftAlready == true)
            SubWeaponLeftAlready = false;
        if (deathStop == true)
            deathStop = false;
        if (Leg1Down == true)
            Leg1Down = false;
        if (Leg2Down == true)
            Leg2Down = false;
        if (DownMark == true)
            DownMark = false;
        if (ImHit == true)
            ImHit = false;
        if (StillDown == true)
            StillDown = false;
        if (FlameDeath == true)
            FlameDeath = false;

        RifleSkillCharge.SetActive(false);
        Sounds.SetActive(true);

        NearAttackSite = 8;
        if (enemyShoot != 0)
            enemyShoot = 0;
        if (moveDown != 0)
            moveDown = 0;
        if (SkillTime != 0)
            SkillTime = 0;
        if (NearAttackRandom != 0)
            NearAttackRandom = 0;
        if (NearAttackTime != 0)
            NearAttackTime = 0;
        if (ammo != 0)
            ammo = 0;

        if (DownTime != 0)
            DownTime = 0;
        if (SkillCoolTime != 0)
            SkillCoolTime = 0;
        if (SkillOnAccount != 0)
            SkillOnAccount = 0;
        if (NearTime != 0)
            NearTime = 0;
        if (FlameTime != 0)
            FlameTime = 0;

        transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        if (FlameMove != null)
            StopCoroutine(FlameMove);

        currnetSpeed = speed;
        StartCoroutine(RandomFire());
        StartCoroutine(ShootingMovement());
        StartCoroutine(yMove()); //�����ð��� ����
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
    }

    private void OnDisable()
    {
        transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(false);
        transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(false);

        animator.SetBool("Warnning, Aso Shiioshare", false);
        animator.SetFloat("Move Speed", 1f);
        animator.SetBool("Move, Aso Shiioshare", false);
        animator.SetBool("Fire1, Aso Shiioshare", false);
        animator.SetBool("Fire2, Aso Shiioshare", false);
        animator.SetBool("Reload, Aso Shiioshare", false);
        animator.SetBool("Sword attack1, Aso Shiioshare", false);
        animator.SetBool("Sword attack2, Aso Shiioshare", false);

        animator.SetBool("Sub weapon move(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon move(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon pick up(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon pick up(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon idle(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon idle(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon fire(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon fire(Left), Aso Shiioshare", false);

        animator.SetBool("Fire1(Down), Aso Shiioshare", false);
        animator.SetBool("Fire2(Down), Aso Shiioshare", false);
        animator.SetBool("Move(Down), Aso Shiioshare", false);
        animator.SetBool("ImDown!, Aso Shiioshare", false);
        animator.SetBool("idle(Down), Aso Shiioshare", false);

        animator.SetBool("Sub weapon down pick up(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon down pick up(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon idle down(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon idle down(Left), Aso Shiioshare", false);
        animator.SetBool("Sub weapon fire down(Right), Aso Shiioshare", false);
        animator.SetBool("Sub weapon fire down(Left), Aso Shiioshare", false);

        if (animator.GetBool("Sub weapon fire off(Left), Aso Shiioshare") == true)
            animator.SetBool("Sub weapon fire off(Left), Aso Shiioshare", false);
        if (animator.GetBool("Sub weapon fire off(Right), Aso Shiioshare") == true)
            animator.SetBool("Sub weapon fire off(Right), Aso Shiioshare", false);
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

    //���� ��ų ����
    IEnumerator RandomSkillFire()
    {
        while (true)
        {
            RandomskillfireBool = true;
            SkillTime = Random.Range(0, SkillRandomLevel);
            yield return new WaitForSeconds(1f);
        }
    }

    //���� ���� ����
    IEnumerator RandomNearAttack()
    {
        while (true)
        {
            RandomNearAttackBool = true;
            NearAttackTime = Random.Range(0, NearAttackLevel);
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

    void Update()
    {
        if (IntoLine == false)
        {
            //Debug.DrawLine(rb2d.position, PlayerPosition, Color.red);
            //Debug.Log("SkillCoolTime : " + SkillCoolTime);
            //Debug.Log("SkillOn : " + SkillOn);
            //Debug.Log("SkillTime : " + SkillTime);
            //Debug.Log("NearAttackTime : " + NearAttackTime);
            //Debug.Log("NearAttack : " + NearAttack);
            //Debug.Log("SkillUsing : " + SkillUsing);
            //Debug.Log("reloading : " + reloading);
            //Debug.Log("fired : " + fired);
            //Debug.Log("enemyShoot : " + enemyShoot);
            //Debug.Log("SubWeaponRight : " + SubWeaponRight);
            //Debug.Log("SubWeaponLeft : " + SubWeaponLeft);

            if (magnetForm != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(magnetForm.position.x, magnetForm.position.y, transform.position.z), MagnetForce * Time.deltaTime); //Ư�� ������ �̵�
            }

            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�

            float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

            //�÷��̾ �ش� ������ ���� �ݰ��� ��Ż��, ����
            if (distanceFromPlayer > lineOfSite && LegsDown == false && deathStop == false && FlameDeath == false && SkillUsing == false)
            {
                if (DownMark == false)
                {
                    MoveComplete = false;
                    animator.SetBool("Move, Aso Shiioshare", true);
                    currnetSpeed = runningSpeed;
                    transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                }
                else
                {
                    MoveComplete = false;
                    animator.SetBool("Move, Aso Shiioshare", true);
                    currnetSpeed = speed / 2;
                    transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                }
            }
            else if (distanceFromPlayer < lineOfSite && LegsDown == false && FlameDeath == false)
                currnetSpeed = speed;

            //���
            if (Trigger)
            {
                if (enemyShoot == 1 && fired == false && reloading == false && SkillUsing == false && NearAttack == false && ImHit == false && deathStop == false && FlameDeath == false)
                {
                    if (MainGunDown == false)
                    {
                        fired = true;
                        if (Leg1Down == true || Leg2Down == true)
                            animator.SetBool("Fire1(Down), Aso Shiioshare", true);
                        else if (Leg1Down == false && Leg2Down == false)
                            animator.SetBool("Fire1, Aso Shiioshare", true);

                        MainGunAttack();
                    }

                    if (SubWeaponRight == true)
                    {
                        if (SubTimeStamp >= SubFireRate)
                        {
                            //Debug.Log("������ �������� ���");
                            fired = true;
                            SubTimeStamp = 0;
                            if (Leg1Down == true || Leg2Down == true)
                                animator.SetBool("Sub weapon fire down(Right), Aso Shiioshare", true);
                            else if (Leg1Down == false && Leg2Down == false)
                                animator.SetBool("Sub weapon fire(Right), Aso Shiioshare", true);
                            SubWeaponRightFire();
                        }
                    }

                    if (SubWeaponLeft == true)
                    {
                        if (SubTimeStamp >= SubFireRate)
                        {
                            //Debug.Log("���� �������� ���");
                            fired = true;
                            SubTimeStamp = 0;
                            if (Leg1Down == true || Leg2Down == true)
                                animator.SetBool("Sub weapon fire down(Left), Aso Shiioshare", true);
                            else if (Leg1Down == false && Leg2Down == false)
                                animator.SetBool("Sub weapon fire(Left), Aso Shiioshare", true);
                            SubWeaponLeftFire();
                        }
                    }
                }
            }

            if (SubTimeStamp <= SubFireRate + 0.1f && MainGunDown == true)
                SubTimeStamp += Time.deltaTime;

            //����
            if (ammo >= AmmoPerMagazine && reloading == false && SkillUsing == false && NearAttack == false && MainGunDown == false && deathStop == false && FlameDeath == false)
            {
                StartCoroutine(Reload());
                StartCoroutine(ReloadSmoke());
            }

            //��� �� ����
            if (afterFire == true)
            {
                afterFire = false;
                if (MainGunDown == false)
                {
                    GameObject GunSmoke = Instantiate(gunSmokePrefab, gunSmokePos.position, gunSmokePos.rotation); //���� ����
                    Destroy(GunSmoke, 4);
                }
            }

            //��ų ����
            if (SkillTime == 1 && SkillOn == true && MainGunDown == false && deathStop == false && FlameDeath == false)
            {
                SkillOn = false;
                SkillAttackBool = true;
                StartCoroutine(SkillFireWait());
            }

            //��ų ������ ���� �÷��̾� ����
            if (SkillChasing == true && MainGunDown == false && deathStop == false && LegsDown == false)
            {
                if (Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Move(Down), Aso Shiioshare", true);
                else if (Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Move, Aso Shiioshare", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Enemytarget.position.y, transform.position.z), currnetSpeed * Time.deltaTime);
            }

            //��ų ���� ��Ÿ��
            if (SkillCoolTime <= SkillCoolTimes)
            {
                SkillCoolTime += Time.deltaTime;

                if (SkillCoolTime >= SkillCoolTimes)
                {
                    SkillOn = true;

                    if (SkillOnAccount == 0 && SkillOn == true && deathStop == false)
                    {
                        SkillOnAccount += Time.deltaTime;
                        randomskillfire = StartCoroutine(RandomSkillFire());
                    }
                }
            }

            //������ �������� ���, ��������
            if (distanceFromPlayer < NearAttackSite && SkillUsing == false && SkillOn == true && fired == false && reloading == false && Arm3Down == false && deathStop == false && FlameDeath == false)
            {
                if (Leg1Down == false && Leg2Down == false)
                {
                    if (SwordAcitve == false && Arm3Down == false)
                    {
                        StartCoroutine(SwordActiveOn());
                        gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSwordOn(true);
                    }

                    if (NearAttack == false && SwordAcitve == true && NearAttackTime == 1)
                    {
                        NearAttack = true;
                        NearAttackTime = 0;
                        if (RandomNearAttackBool == true)
                            StopCoroutine(randomNearAttack);

                        if (SkillUsing == false)
                            StartCoroutine(RunBeforeAttack());
                    }
                }
                else if (Leg1Down == true || Leg2Down == true) //�Ѿ����� ���� ��������
                {
                    NearAttackSite = 4;

                    if (SwordAcitve == false && Arm3Down == false)
                    {
                        StartCoroutine(SwordActiveOn());
                        gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSwordOn(true);
                    }

                    if (NearAttack == false && SwordAcitve == true && NearAttackTime == 1)
                    {
                        NearAttack = true;
                        NearAttackTime = 0;
                        if (RandomNearAttackBool == true)
                            StopCoroutine(randomNearAttack);

                        if (SkillUsing == false)
                        {
                            NearAttackRandom = Random.Range(0, 2);

                            NearAttacking = true;
                            NearAttackRunning = false;

                            if (NearAttackRandom == 1)
                            {
                                Attack1GoBool = true;
                                attack1Go = StartCoroutine(Attack1Go());
                            }
                            else
                            {
                                Attack2GoBool = true;
                                attack2Go = StartCoroutine(Attack2Go());
                            }
                        }
                    }
                }
            }

            //���� ���� ������ �÷��̾ �������� ���, ���� �����ϱ� ���� ���� �Լ� ������
            if (distanceFromPlayer < NearAttackSite && NearTime == 0 && Arm3Down == false && deathStop == false && FlameDeath == false)
            {
                NearTime += Time.deltaTime;
                randomNearAttack = StartCoroutine(RandomNearAttack());
            }
            else if (distanceFromPlayer > NearAttackSite && NearTime > 0) //���� ���� ���� �������� �÷��̾ ����� ���� ���� ���� �Լ� �ߴ�
            {
                if (FlameDeath == false || deathStop == true)
                {
                    NearTime = 0;
                    NearAttackTime = 0;
                    if (RandomNearAttackBool == true)
                        StopCoroutine(randomNearAttack);
                }
            }

            //���� ���ݽ� �޷�����, �ڷ�ƾ RunBeforeAttack���� NearAttackRunning bool�� ����� �� Ȱ��ȭ
            if (NearAttackRunning == true && Arm3Down == false && deathStop == false && FlameDeath == false)
            {
                currnetSpeed = runningSpeed;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                animator.SetFloat("Move Speed", 4f);
                animator.SetBool("Move, Aso Shiioshare", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerPosition.x, PlayerPosition.y, transform.position.z), currnetSpeed * Time.deltaTime);
            }

            distanceFromPlayerDot = Vector2.Distance(PlayerPosition, transform.position); //����� �÷��̾� �� �Ʊ� ��ǥ���� ���� �Ÿ�

            //������ ���� ��ǥ�� �������� ��, ���� ���� �ڷ�ƾ 2���� �������� ������ Ȱ��ȭ
            if (NearAttack == true)
            {
                if (distanceFromPlayerDot <= 0 && NearAttacking == false && Arm3Down == false)
                {
                    NearAttackRandom = Random.Range(0, 2);

                    NearAttacking = true;
                    NearAttackRunning = false;
                    currnetSpeed = speed * 0.5f;

                    if (NearAttackRandom == 1)
                    {
                        Attack1GoBool = true;
                        attack1Go = StartCoroutine(Attack1Go());
                    }
                    else
                    {
                        Attack2GoBool = true;
                        attack2Go = StartCoroutine(Attack2Go());
                    }
                }
            }

            //�ൿ ���߿� �ǰ� �ִϸ��̼� ��Ȱ��ȭ
            if (SkillUsing == true || NearAttacking == true || reloading == true)
                GetComponent<HealthAsoShiioshare>().AsoShiioshareAttacking(true);
            else if (SkillUsing == false || NearAttacking == false || reloading == false)
                GetComponent<HealthAsoShiioshare>().AsoShiioshareAttacking(false);

            //�ֹ��Ⱑ ����ȭ�Ǿ��� ���, ��� ����
            if (MainGunDown == true)
            {
                animator.SetBool("Fire1, Aso Shiioshare", false);
                animator.SetBool("Fire2, Aso Shiioshare", false);
                animator.SetBool("Reload, Aso Shiioshare", false);
                Sounds.SetActive(false);

                SkillFiring = false;
                SkillUsing = false;
                SkillOn = false;
                reloading = false;
                SkillCoolTime = 0;
                SkillOnAccount = 0;
                SkillTime = 0;

                if (RandomskillfireBool == true)
                    StopCoroutine(randomskillfire);
                if (SkillAttackBool == true)
                    StopCoroutine(skillAttack);

                animator.SetBool("Warnning, Aso Shiioshare", false);
            }

            //���� ���߿� �׾��� ���, ������ ����
            if (deathStop == true || FlameDeath == true)
            {
                animator.SetBool("Fire2, Aso Shiioshare", false);
                animator.SetBool("Sword attack1, Aso Shiioshare", false);
                animator.SetBool("Sword attack2, Aso Shiioshare", false);
                Sounds.SetActive(false);

                if (deathStop == true)
                {
                    if (MainGunDown == false)
                    {
                        MainGunDown = true;
                        transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (SubWeaponRight == true)
                    {
                        SubWeaponRight = false;
                        transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(false);
                    }
                    if (SubWeaponLeft == true)
                    {
                        SubWeaponLeft = false;
                        transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(false);
                    }
                }

                SkillChasing = false;
                SkillFiring = false;
                SkillUsing = false;
                SkillOn = false;
                SkillCoolTime = 0;
                SkillOnAccount = 0;
                SkillTime = 0;

                if (RandomskillfireBool == true)
                    StopCoroutine(randomskillfire);
                if (SkillAttackBool == true)
                    StopCoroutine(skillAttack);
                if (PickSubWeaponArm1UpBool == true)
                    StopCoroutine(pickSubWeaponArm1Up);
                if (PickSubWeaponArm2UpBool == true)
                    StopCoroutine(pickSubWeaponArm2Up);

                animator.SetBool("Warnning, Aso Shiioshare", false);
            }

            //�ҵ� ���� �߷��� ���, ���� ���� ����ȭ
            if (Arm3Down == true)
            {
                transform.Find("Aso Shiioshare Flasma sword").gameObject.SetActive(false);
                animator.SetBool("Sword on, Aso Shiioshare", false);
                animator.SetBool("Sword attack1, Aso Shiioshare", false);
                animator.SetBool("Sword attack2, Aso Shiioshare", false);

                NearTime = 0;
                NearAttackTime = 0;
                NearAttack = false;
                NearAttacking = false;
                NearAttackRunning = false;

                if (Attack1GoBool == true)
                    StopCoroutine(attack1Go);
                if (Attack2GoBool == true)
                    StopCoroutine(attack2Go);
                if (RandomNearAttackBool == true)
                    StopCoroutine(randomNearAttack);
            }

            if (Arm1Down == true)
            {
                transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(false);
                animator.SetBool("Sub weapon move(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon pick up(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon fire(Right), Aso Shiioshare", false);
            }
            if (Arm2Down == true)
            {
                transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(false);
                animator.SetBool("Sub weapon move(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon pick up(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon fire(Left), Aso Shiioshare", false);
            }

            //�׾��� ���, �ӵ� ����
            if (deathStop == true)
            {
                if (currnetSpeed > 0)
                {
                    currnetSpeed -= Time.deltaTime * 8f; //����
                    if (this.gameObject.activeSelf == true)
                        transform.position = Vector2.MoveTowards(transform.position, player.position, currnetSpeed * Time.deltaTime);
                }

                if (currnetSpeed < 0)
                {
                    currnetSpeed = 0;
                }
            }

            //�� �ִ� ���¿����� �������� idle Ȱ��ȭ
            if (SubWeaponRight == true && fired == false && Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sub weapon idle(Right), Aso Shiioshare", true);
            if (SubWeaponLeft == true && fired == false && Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sub weapon idle(Left), Aso Shiioshare", true);

            //�Ѿ��� ���¿����� �������� idle Ȱ��ȭ
            if (DownMark == true && SubWeaponRight == true && fired == false)
                animator.SetBool("Sub weapon idle down(Right), Aso Shiioshare", true);
            if (DownMark == true && SubWeaponLeft == true && fired == false)
                animator.SetBool("Sub weapon idle down(Left), Aso Shiioshare", true);

            //�Ѿ��� ���¿����� idle Ȱ��ȭ
            if (DownMark == true)
                animator.SetBool("idle(Down), Aso Shiioshare", true);

            //�ٸ� 2���� ��� ����ȭ�Ǿ��� ���, ����ȭ ����ġ Ȱ��ȭ
            if (Leg1Down == true && Leg2Down == true)
                LegsDown = true;

            //�Ѿ��� ���Ŀ� �ֹ��Ⱑ ����ȭ�� ���
            if (DownMark == true && MainGunDown == true)
                animator.SetBool("Fire1(Down), Aso Shiioshare", false);

            if (Leg1Down == true || Leg2Down == true)
            {
                animator.SetBool("Sub weapon pick up(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon pick up(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon fire(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon fire(Left), Aso Shiioshare", false);
            }

            //�Ѿ����� ���� ��, �ٸ� �ൿ ���� �ߴ�
            if (StillDown == true)
            {
                currnetSpeed = 0;

                animator.SetBool("Fire1, Aso Shiioshare", false);
                animator.SetBool("Fire2, Aso Shiioshare", false);
                animator.SetBool("Reload, Aso Shiioshare", false);

                SkillFiring = false;
                SkillUsing = false;
                SkillChasing = false;
                SkillOn = false;
                reloading = false;
                SkillCoolTime = 0;
                SkillOnAccount = 0;
                SkillTime = 0;

                if (RandomskillfireBool == true)
                    StopCoroutine(randomskillfire);
                if (SkillAttackBool == true)
                    StopCoroutine(skillAttack);

                animator.SetBool("Warnning, Aso Shiioshare", false);
                animator.SetBool("Move, Aso Shiioshare", false);
            }
        }
        else
        {
            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�
            MoveComplete = false;
            animator.SetBool("Move, Aso Shiioshare", true);
            if (IntoLineSpeed == false)
                transform.Translate(transform.right * FollowVector * 20 * Time.deltaTime);
            else
            {
                currnetSpeed = runningSpeed;
                transform.Translate(transform.right * FollowVector * currnetSpeed * Time.deltaTime);
            }
        }

        //�ҿ� Ÿ�� ���� ���� ������
        if (FlameDeath == true)
        {
            if (FlameTime == 0)
            {
                FlameTime += Time.deltaTime;
                FlameMove = StartCoroutine(FlameMovement());
            }
        }
    }

    //�Ϲ� ��ȸ
    public IEnumerator ShootingMovement()
    {
        if (LegsDown == false && SkillUsing == false && deathStop == false && FlameDeath == false && IntoLine == false)
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

        if (LegsDown == false && SkillUsing == false && deathStop == false && FlameDeath == false && IntoLine == false)
        {
            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    MoveComplete = false;
                    //animator.SetBool("Move, Aso Shiioshare", true);
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
            //animator.SetBool("Move, Aso Shiioshare", false);
        }
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if (Enemytarget.gameObject.activeSelf == true && SkillFiring == false && deathStop == false && FlameDeath == false)
        {
            if (Enemytarget.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                gameObject.GetComponent<ShieldAsoShiioshare>().ShieldDamageDirection(true);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                gameObject.GetComponent<ShieldAsoShiioshare>().ShieldDamageDirection(false);
            }
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

            if (transform.rotation.y == 0) //������ �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("Move Speed", 1f);
                    animator.SetBool("Move, Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Left), Aso Shiioshare", true);
                    if (Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Move(Down), Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", true);
                }
                else //����
                {
                    animator.SetFloat("Move Speed", -1f);
                    animator.SetBool("Move, Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Left), Aso Shiioshare", true);
                    if (Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Move(Down), Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", true);
                }
            }
            else if (transform.rotation.y != 0) //�������� �Ĵٺ��� ���� ��
            {
                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("Move Speed", -1f);
                    animator.SetBool("Move, Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Left), Aso Shiioshare", true);
                    if (Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Move(Down), Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", true);
                }
                else //����
                {
                    animator.SetFloat("Move Speed", 1f);
                    animator.SetBool("Move, Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
                        animator.SetBool("Sub weapon move(Left), Aso Shiioshare", true);
                    if (Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Move(Down), Aso Shiioshare", true);
                    if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", true);
                    if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
                        animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", true);
                }
            }
            if (MoveComplete == true || LegsDown == true || SkillUsing == true && SkillChasing == false)
            {
                animator.SetBool("Move, Aso Shiioshare", false);
                if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Sub weapon move(Right), Aso Shiioshare", false);
                if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Sub weapon move(Left), Aso Shiioshare", false);
                if (Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Move(Down), Aso Shiioshare", false);
                if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Sub weapon move down(Right), Aso Shiioshare", false);
                if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Sub weapon move down(Left), Aso Shiioshare", false);
            }
        }
    }

    //�ֹ��� ����
    void MainGunAttack()
    {
        SoundManager.instance.SFXPlay28("Sound", RifleFire);

        if (Leg1Down == false && Leg2Down == false)
        {
            GameObject Ammo = Instantiate(AmmoPrefab, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
            Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(Damage); //�Ѿ˿��� ������ ����
        }
        else if (Leg1Down == true || Leg2Down == true)
        {
            GameObject Ammo = Instantiate(AmmoPrefab2, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
            Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(Damage); //�Ѿ˿��� ������ ����
        }

        GameObject AmmoEffect = Instantiate(AmmoFireEffectPrefab, AmmoFireEffectPos.position, transform.rotation); //�߻� ����Ʈ ����
        Destroy(AmmoEffect, 3f);
        afterFire = true;

        Invoke("MainGunOff", 0.4f);
    }

    //�ֹ��� ���� ����
    void MainGunOff()
    {
        if (MainGunDown == false && Leg1Down == false && Leg2Down == false)
            animator.SetBool("Fire1, Aso Shiioshare", false);
        else if(MainGunDown == false && Leg1Down == true || Leg2Down == true)
            animator.SetBool("Fire1(Down), Aso Shiioshare", false);

        ammo += 1;
        fired = false;
    }

    //����
    IEnumerator Reload()
    {
        reloading = true;
        animator.SetBool("Reload, Aso Shiioshare", true);

        SoundManager.instance.SFXPlay27("Sound", RifleReload);

        yield return new WaitForSeconds(2.5f);

        animator.SetBool("Reload, Aso Shiioshare", false);
        reloading = false;
        ammo = 0;
    }

    //���� ���� �߻�
    IEnumerator ReloadSmoke()
    {
        yield return new WaitForSeconds(0.66f);
        GameObject Smoke = Instantiate(smokePrefab, smokePos.position, smokePos.rotation); //���� ����
        Destroy(Smoke, 5);
    }

    //��ų ����
    IEnumerator SkillAttack()
    {
        if (deathStop == false && FlameDeath == false)
        {
            RifleSkillCharge.SetActive(true);
            animator.SetBool("Warnning, Aso Shiioshare", true);
            yield return new WaitForSeconds(2f);
            SkillChasing = false;
            currnetSpeed = 0;
            rb2d.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.5f);
            SkillFiring = true;
            animator.SetBool("Warnning, Aso Shiioshare", false);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Fire2(Down), Aso Shiioshare", true);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Fire2, Aso Shiioshare", true);
            yield return new WaitForSeconds(0.66f);
            if (deathStop == false && FlameDeath == false)
            {
                Shake.Instance.ShakeCamera(5, 0.25f);
                SoundManager.instance.SFXPlay2("Sound", RifleSkillFire);
                GameObject SkillAmmo = Instantiate(SkillAmmoPrefab, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                SkillAmmo.GetComponent<SloriusShockWaveBullet>().SetDamageBeam(SkillDamage); //�Ѿ˿��� ������ ����
            }
            yield return new WaitForSeconds(1.423f);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Fire2(Down), Aso Shiioshare", false);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Fire2, Aso Shiioshare", false);

            RifleSkillCharge.SetActive(false);
            SkillFiring = false;
            SkillUsing = false;
            SkillOn = false;
            SkillCoolTime = 0;
            SkillOnAccount = 0;
            SkillTime = 0;
        }
    }

    //��� �� ����, �������� ���� �� �� �� ���� �۾��� ������ ��� ��ų�� Ȱ��ȭ
    IEnumerator SkillFireWait()
    {
        if (deathStop == false && FlameDeath == false)
        {
            if (reloading == true || fired == true || NearAttack == true)
            {
                while (true)
                {
                    yield return new WaitForSeconds(1f);

                    if (reloading == true || fired == true || NearAttack == true)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                SkillUsing = true; //��ų ��� ������ ��ȯ�ؼ� �ٸ� ������ ���ϰ� �Ѵ�.
                SkillChasing = true; //��� �߰ݻ��·� ��ȯ
                StopCoroutine(randomskillfire);
                skillAttack = StartCoroutine(SkillAttack());
            }
            else if (reloading == false && fired == false && NearAttack == false)
            {
                SkillUsing = true;
                SkillChasing = true;
                StopCoroutine(randomskillfire);
                skillAttack = StartCoroutine(SkillAttack());
            }
        }
    }

    //����Į Ȱ��ȭ �ִϸ��̼�
    IEnumerator SwordActiveOn()
    {
        yield return new WaitForSeconds(0.25f);
        //SoundManager.instance.SFXPlay3("Sound", SwordActive);
        animator.SetBool("Sword on, Aso Shiioshare", true);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("Sword on, Aso Shiioshare", true);
        if (SoundOne == 0)
        {
            SoundOne += Time.deltaTime;
            SoundManager.instance.SFXPlay28("Sound", SwordOn);
        }
        transform.Find("Aso Shiioshare Flasma sword").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("Sword on, Aso Shiioshare", false);
        SwordAcitve = true;
    }

    //���� ���� ������ �÷��̾�� �޷����� ���� ��ǥ ����
    IEnumerator RunBeforeAttack()
    {
        PlayerPosition = Enemytarget.transform.position; //�÷��̾� �� �Ʊ� ��ǥ�� ����
        yield return new WaitForSeconds(0.5f);
        NearAttackRunning = true;
    }

    //���� ����1
    IEnumerator Attack1Go()
    {
        if (deathStop == false && FlameDeath == false)
        {
            SoundManager.instance.SFXPlay4("Sound", SwordAttack1);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sword attack1(down), Aso Shiioshare", true);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sword attack1, Aso Shiioshare", true);
            yield return new WaitForSeconds(0.25f);
            DamagePos.GetComponent<NearDamage>().SetDamage(ShortAttackDamage, 0.2f);
            DamagePos.SetActive(true);
            yield return new WaitForSeconds(0.58f);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sword attack1(down), Aso Shiioshare", false);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sword attack1, Aso Shiioshare", false);
            NearAttack = false;
            NearAttacking = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            if (SkillCoolTime >= SkillCoolTimes)
                SkillOn = true;
        }
    }

    //���� ����2
    IEnumerator Attack2Go()
    {
        if (deathStop == false && FlameDeath == false)
        {
            SoundManager.instance.SFXPlay4("Sound", SwordAttack2);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sword attack2(down), Aso Shiioshare", true);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sword attack2, Aso Shiioshare", true);
            yield return new WaitForSeconds(0.25f);
            DamagePos.GetComponent<NearDamage>().SetDamage(ShortAttackDamage, 0.2f);
            DamagePos.SetActive(true);
            yield return new WaitForSeconds(0.58f);
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sword attack2(down), Aso Shiioshare", false);
            else if (Leg1Down == false && Leg2Down == false)
                animator.SetBool("Sword attack2, Aso Shiioshare", false);
            NearAttack = false;
            NearAttacking = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            if (SkillCoolTime >= SkillCoolTimes)
                SkillOn = true;
        }
    }

    //��������1 ���
    IEnumerator PickSubWeaponArm1Up()
    {
        if(Arm1Down == false)
        {
            if (Leg1Down == false && Leg2Down == false)
            {
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Sub weapon pick up(Right), Aso Shiioshare", true);
                yield return new WaitForSeconds(0.416f);
                SoundManager.instance.SFXPlay2("Sound", SubgunOn);
                transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponL(true);
                yield return new WaitForSeconds(0.416f);
                animator.SetBool("Sub weapon pick up(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Right), Aso Shiioshare", true);
                SubWeaponRight = true;
                SubWeaponRightAlready = true;
                gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponRReady(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponRReady(true);
            }

            if (Leg1Down == true || Leg2Down == true)
            {
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Sub weapon down pick up(Right), Aso Shiioshare", true);
                yield return new WaitForSeconds(0.416f);
                SoundManager.instance.SFXPlay2("Sound", SubgunOn);
                transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Ouu Sheeo ooi").gameObject.SetActive(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponL(true);
                yield return new WaitForSeconds(0.416f);
                animator.SetBool("Sub weapon down pick up(Right), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle down(Right), Aso Shiioshare", true);
                SubWeaponRight = true;
                SubWeaponRightAlready = true;
                gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponRReady(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponRReady(true);
            }
        }
    }

    //��������1 ����
    void SubWeaponRightFire()
    {
        if (deathStop == false && FlameDeath == false)
        {
            SoundManager.instance.SFXPlay27("Sound", SubgunFire);

            if (Leg1Down == false && Leg2Down == false)
            {
                GameObject Ammo = Instantiate(AmmoSubRPrefab, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(SubDamage); //�Ѿ˿��� ������ ����
            }
            else if (Leg1Down == true || Leg2Down == true)
            {
                GameObject Ammo = Instantiate(AmmoSubRPrefab2, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(SubDamage); //�Ѿ˿��� ������ ����
            }

            GameObject AmmoEffect = Instantiate(AmmoFireEffectPrefab, AmmoFireEffectPos.position, transform.rotation); //�߻� ����Ʈ ����
            Destroy(AmmoEffect, 3f);

            Invoke("SubWeaponRightOff", SubFireRate);
        }
    }

    //��������1 ���� ����
    void SubWeaponRightOff()
    {
        if (SubWeaponRight == true && Leg1Down == false && Leg2Down == false)
            animator.SetBool("Sub weapon fire(Right), Aso Shiioshare", false);
        else if(SubWeaponRight == true)
            if(Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sub weapon fire down(Right), Aso Shiioshare", false);

        fired = false;
    }

    //��������2 ���
    IEnumerator PickSubWeaponArm2Up()
    {
        if (Arm2Down == false)
        {
            if (Leg1Down == false && Leg2Down == false)
            {
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Sub weapon pick up(Left), Aso Shiioshare", true);
                yield return new WaitForSeconds(0.416f);
                SoundManager.instance.SFXPlay2("Sound", SubgunOn);
                transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponR(true);
                yield return new WaitForSeconds(0.416f);
                animator.SetBool("Sub weapon pick up(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle(Left), Aso Shiioshare", true);
                SubWeaponLeft = true;
                SubWeaponLeftAlready = true;
                gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponLReady(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponLReady(true);
            }

            if (Leg1Down == true || Leg2Down == true)
            {
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("Sub weapon down pick up(Left), Aso Shiioshare", true);
                yield return new WaitForSeconds(0.416f);
                SoundManager.instance.SFXPlay2("Sound", SubgunOn);
                transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Ouu Sheeo ooi").gameObject.SetActive(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponR(true);
                yield return new WaitForSeconds(0.416f);
                animator.SetBool("Sub weapon down pick up(Left), Aso Shiioshare", false);
                animator.SetBool("Sub weapon idle down(Left), Aso Shiioshare", true);
                SubWeaponLeft = true;
                SubWeaponLeftAlready = true;
                gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponLReady(true);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareSubWeaponLReady(true);
            }
        }
    }

    //��������2 ����
    void SubWeaponLeftFire()
    {
        if (deathStop == false && FlameDeath == false)
        {
            SoundManager.instance.SFXPlay27("Sound", SubgunFire);

            if (Leg1Down == false && Leg2Down == false)
            {
                GameObject Ammo = Instantiate(AmmoPrefab, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(SubDamage); //�Ѿ˿��� ������ ����
            }
            else if (Leg1Down == true || Leg2Down == true)
            {
                GameObject Ammo = Instantiate(AmmoSubLPrefab, AmmoPos.position, transform.rotation); //�߻� �Ѿ� ����
                Ammo.GetComponent<AmmoMovementTaikaLaiThrotro1>().SetDamage(SubDamage); //�Ѿ˿��� ������ ����
            }

            GameObject AmmoEffect = Instantiate(AmmoFireEffectPrefab, AmmoFireEffectPos.position, transform.rotation); //�߻� ����Ʈ ����
            Destroy(AmmoEffect, 3f);

            Invoke("SubWeaponLeftOff", SubFireRate);
        }
    }

    //��������2 ���� ����
    void SubWeaponLeftOff()
    {
        if (SubWeaponLeft == true && Leg1Down == false && Leg2Down == false)
            animator.SetBool("Sub weapon fire(Left), Aso Shiioshare", false);
        else if (SubWeaponLeft == true)
            if (Leg1Down == true || Leg2Down == true)
                animator.SetBool("Sub weapon fire down(Left), Aso Shiioshare", false);

        fired = false;
    }

    //�Ѿ����� �ִϸ��̼�
    IEnumerator ImDown()
    {
        StillDown = true;
        animator.SetBool("ImDown!, Aso Shiioshare", true);

        yield return new WaitForSeconds(0.75f);

        StillDown = false;

        if (SubWeaponRight == true && Leg1Down == true || Leg2Down == true)
            animator.SetBool("Sub weapon fire off(Right), Aso Shiioshare", true);
        if (SubWeaponLeft == true && Leg1Down == true || Leg2Down == true)
            animator.SetBool("Sub weapon fire off(Left), Aso Shiioshare", true);

        animator.SetBool("idle(Down), Aso Shiioshare", true);
        animator.SetBool("ImDown!, Aso Shiioshare", false);

        animator.SetBool("Damage1 down, Aso Shiioshare", false);
        animator.SetBool("Damage2 down, Aso Shiioshare", false);

        currnetSpeed = speed / 2;
        DownMark = true;

        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareDownMark(true);
    }

    //�ҿ� �پ��� ��, ���� ��ȸ
    IEnumerator FlameMovement()
    {
        if (FlameDeath == true)
        {
            while (true)
            {
                float RandomMovement = 10;
                float RandomWander = Random.Range(0.5f, 1f);

                FlameEndposition = new Vector3(Random.Range(transform.position.x - RandomMovement, transform.position.x + RandomMovement),
                    Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);

                if (FlamemoveCoroutine != null)
                {
                    StopCoroutine(FlamemoveCoroutine);
                }

                FlamemoveCoroutine = StartCoroutine(Move());
                yield return new WaitForSeconds(RandomWander);
            }
        }
    }

    //������ ��ǥ�� ���� ������ ����
    public IEnumerator Move()
    {
        if (FlameDeath == true)
        {
            float remainingDistance = (transform.position - FlameEndposition).sqrMagnitude;
            float EndPoint = FlameEndposition.x;
            //Debug.Log("Flame Death Start");

            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (DownMark == false)
                {
                    animator.SetBool("Move, Aso Shiioshare", true);
                    animator.SetFloat("Move Speed", 1f);
                }
                else if (DownMark = true && LegsDown == false)
                    animator.SetBool("Move(Down), Aso Shiioshare", true);
                transform.position = Vector2.MoveTowards(transform.position, FlameEndposition, currnetSpeed * Time.deltaTime);

                if (EndPoint > transform.position.x)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 180, 0);

                remainingDistance = (transform.position - FlameEndposition).sqrMagnitude;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}