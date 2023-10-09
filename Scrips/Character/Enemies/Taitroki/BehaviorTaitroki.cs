using System.Collections;
using UnityEngine;

public class BehaviorTaitroki : MonoBehaviour
{
    ObjectManager objectManager;

    Rigidbody2D rb2d;
    Animator animator;

    public int Type; //Ÿ��Ʈ��Ű ����
    public int Damage; //�⺻ ���ݷ�
    public int JumpDamage; //���� ���ݷ�
    private int NearAttackTime; //�������� ���� �����Լ�
    private int NearAttackRandom; //�������� ��� �����Լ�
    public int NearAttackLevel; //NearAttackTime�� �ִ� ��������
    public int JumpAttackLevel; //�������� �󵵼�
    private int jumpAttackTime; //�������� �����Լ�

    public float NearAttackSite; //���������� ���Ǵ� ����ũ��
    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    public float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    private float NearTime; //�÷��̾ �������� �� ���� ���� �Լ� �ڷ�ƾ�� �ѹ��� Ȱ��ȭ�ϱ� ���� ����
    private float JumpNearTime; //�÷��̾ �������� �� ���� ���� �Լ� �ڷ�ƾ�� �ѹ��� Ȱ��ȭ�ϱ� ���� ����
    private float DownTime; //�Ѿ����� �Ͼ �� �ѹ��� ����Ǵ� ����
    private float CrawlTime; //�� �� �ѹ��� ����Ǵ� ����

    private bool TaitrokiAcitvated = false; //Ÿ��Ʈ��Ű Ȱ��ȭ ������ ����ġ
    private bool deathStop = false; //�׾��� ���� ����ġ
    private bool Moving = false; //�����̰� ���� ���� ����ġ
    private bool NearAttack = false; //���� ���� Ȱ��ȭ
    private bool NearAttacking = false; //���� �����ϰ� ���� �� ����ġ
    private bool UsingJumpAttack = false; //���� �����ϰ� ���� ���� ����ġ
    private bool MoveComplete = false; //�������� �Ϸ�Ǿ��� ���� ����ġ
    private bool NoArm = false; //���� ��� ���� ���� ����ġ
    private bool LegsDown = false; //�ٸ��� ��� ����ȭ �Ǿ��� ���� ����ġ
    private bool ImStillDown = false; //�Ѿ����� �ִ� ������ ����ġ
    private bool Rising = false; //�Ͼ�� �ִ� ������ ����ġ
    private bool DownState = false; //�ٸ��� �߷� �Ѿ��� ���¸� ��Ÿ���� ����ġ

    private Vector3 PlayerPosition; //�÷��̾� �� �Ʊ� ��ġ ����
    public Transform Enemytarget;
    public GameObject DamagePos;

    Coroutine FlameMove;
    Coroutine FlamemoveCoroutine;
    Vector3 FlameEndposition; //���� ������ ��ġ��
    public bool FlameDeath = false; //�ҿ� ź ���¿��� ���� ��
    private float FlameTime;

    Coroutine transformBladeHider;
    Coroutine moveCoroutine;
    Coroutine movementAround;
    Coroutine moveToWard;
    Coroutine randomNearAttack;
    Coroutine attack1Go;
    Coroutine attack2Go;
    Coroutine attackDown;
    Coroutine jumpAttackAccount;
    Coroutine jumpAttackStart;
    Coroutine jumpAttackStart2;
    Coroutine downandRise;

    public AudioClip TransformSound1;
    public AudioClip TransformSound2;
    public AudioClip TransformSound3;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip JumpAttack;

    public void InfectorDeath(bool droping)
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

    public void BladeTaitroki()
    {
        GetComponent<InfectorBehavior>().TaitrokiAcitvated = true;
        transformBladeHider = StartCoroutine(TransformBladeHider());
    }

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            Damage = 50;
            JumpDamage = 80;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            Damage = 70;
            JumpDamage = 112;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            Damage = 80;
            JumpDamage = 128;
        }

        animator.SetBool("Taitroki Turn off", true);
        animator.SetBool("Taitroki Turn off", false);

        if (animator.GetFloat("TaitrokiMoving") != 0)
            animator.SetFloat("TaitrokiMoving", 0);
        if (animator.GetFloat("Attack(Blade), Taitroki") != 0)
            animator.SetFloat("Attack(Blade), Taitroki", 0);
        if (animator.GetFloat("Attack jump(Blade), Taitroki") != 0)
            animator.SetFloat("Attack jump(Blade), Taitroki", 0);
        if (animator.GetBool("Movement(Blade), Taitroki") == true)
            animator.SetBool("Movement(Blade), Taitroki", false);
        if (animator.GetBool("Get up front, Infector") == true)
            animator.SetBool("Get up front, Infector", false);
        if (animator.GetBool("Down(Blade), Taitroki") == true)
            animator.SetBool("Down(Blade), Taitroki", false);
        if (animator.GetBool("Down moving(Blade), Taitroki") == true)
            animator.SetBool("Down moving(Blade), Taitroki", false);
        if (animator.GetBool("idle(Blade), Taitroki") == true)
            animator.SetBool("idle(Blade), Taitroki", false);
        if (animator.GetBool("idle down(Blade), Taitroki") == true)
            animator.SetBool("idle down(Blade), Taitroki", false);

        if (TaitrokiAcitvated == true)
            TaitrokiAcitvated = false;
        if (deathStop == true)
            deathStop = false;
        if (Moving == true)
            Moving = false;
        if (MoveComplete == true)
            MoveComplete = false;
        if (NearAttack == true)
            NearAttack = false;
        if (NearAttacking == true)
            NearAttacking = false;
        if (UsingJumpAttack == true)
            UsingJumpAttack = false;
        if (NoArm == true)
            NoArm = false;
        if (LegsDown == true)
            LegsDown = false;
        if (ImStillDown == true)
            ImStillDown = false;
        if (Rising == true)
            Rising = false;
        if (DownState == true)
            DownState = false;
        if (FlameDeath == true)
            FlameDeath = false;

        if (DownTime != 0)
            DownTime = 0;
        if (CrawlTime != 0)
            CrawlTime = 0;
        if (jumpAttackTime != 0)
            jumpAttackTime = 0;
        if (NearTime != 0)
            NearTime = 0;
        if (JumpNearTime != 0)
            JumpNearTime = 0;
        if (NearAttackTime != 0)
            NearAttackTime = 0;
        if (FlameTime != 0)
            FlameTime = 0;

        if (FlameMove != null)
            StopCoroutine(FlameMove);
    }

    private void OnDisable()
    {
        transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);

        transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
        transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);

        transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
        transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
        transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
        transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);

        transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
        transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
        transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);

        transform.Find("bone_1/bone_f1/Part3").gameObject.SetActive(false);
        transform.Find("bone_1/bone_f1/Part9").gameObject.SetActive(false);

        if (animator.GetFloat("TaitrokiMoving") != 0)
            animator.SetFloat("TaitrokiMoving", 0);
        if (animator.GetFloat("Attack(Blade), Taitroki") != 0)
            animator.SetFloat("Attack(Blade), Taitroki", 0);
        if (animator.GetFloat("Attack jump(Blade), Taitroki") != 0)
            animator.SetFloat("Attack jump(Blade), Taitroki", 0);
        if (animator.GetBool("Movement(Blade), Taitroki") == true)
            animator.SetBool("Movement(Blade), Taitroki", false);
        if (animator.GetBool("Get up front, Infector") == true)
            animator.SetBool("Get up front, Infector", false);
        if (animator.GetBool("Down(Blade), Taitroki") == true)
            animator.SetBool("Down(Blade), Taitroki", false);
        if (animator.GetBool("Down moving(Blade), Taitroki") == true)
            animator.SetBool("Down moving(Blade), Taitroki", false);
        if (animator.GetBool("idle(Blade), Taitroki") == true)
            animator.SetBool("idle(Blade), Taitroki", false);
        if (animator.GetBool("idle down(Blade), Taitroki") == true)
            animator.SetBool("idle down(Blade), Taitroki", false);
    }

    void Update()
    {
        if (TaitrokiAcitvated == true)
        {
            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�

            if (GetComponent<TearInfector>().B1LURend == true && GetComponent<TearInfector>().B1LULend == true) //���� ����� ���ư�
                NoArm = true;
            if (GetComponent<TearInfector>().B1LDLend == true && GetComponent<TearInfector>().B1LDRend == true) //���� �ϴ��� ���ư�
                NoArm = true;
            if (GetComponent<TearInfector>().B1LURend == true && GetComponent<TearInfector>().B1LDLend == true) //������ ���, ���� �ϴ��� ���ư�
                NoArm = true;
            if (GetComponent<TearInfector>().B1LULend == true && GetComponent<TearInfector>().B1LDRend == true) //���� ���, ������ �ϴ��� ���ư�
                NoArm = true;

            //�Ѿ����� �� ������ �߷��� ��� �ƹ��͵� ����
            if (NoArm == true && DownState == true)
            {
                currnetSpeed = 0;
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                animator.SetBool("Down moving(Blade), Taitroki", false);
            }

            //�Ѿ����� �Ͼ�� ���� ���� ����
            if (GetComponent<TearInfector>().TakeDown == 1 && UsingJumpAttack == false)
            {
                if (DownTime == 0)
                {
                    Rising = true;
                    DownTime += Time.deltaTime;

                    if (moveCoroutine != null)
                        StopCoroutine(moveCoroutine);
                    if (attack1Go != null)
                        StopCoroutine(attack1Go);
                    if (attack2Go != null)
                        StopCoroutine(attack2Go);
                    if (attackDown != null)
                        StopCoroutine(attackDown);
                    if (jumpAttackStart != null)
                        StopCoroutine(jumpAttackStart);
                    if (jumpAttackStart2 != null)
                        StopCoroutine(jumpAttackStart2);

                    currnetSpeed = 0;
                    animator.SetFloat("Attack(Blade), Taitroki", 0);
                    animator.SetBool("Movement(Blade), Taitroki", false);

                    downandRise = StartCoroutine(DownandRise());
                }
            }

            //�Ͼ�� ���� �� �ٸ� �ൿ�� �߻����� �ʱ� ���� ����
            if (Rising == true)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                if (attack1Go != null)
                    StopCoroutine(attack1Go);
                if (attack2Go != null)
                    StopCoroutine(attack2Go);
                if (attackDown != null)
                    StopCoroutine(attackDown);
                if (jumpAttackStart != null)
                    StopCoroutine(jumpAttackStart);
                if (jumpAttackStart2 != null)
                    StopCoroutine(jumpAttackStart2);

                currnetSpeed = 0;
                animator.SetFloat("Attack(Blade), Taitroki", 0);
                animator.SetBool("Movement(Blade), Taitroki", false);
            }

            //�ٸ��� �ɷ� �Ѿ����� ���� �ٸ��� �߷��� �Ѿ����� ���� �� �� �� ��ġ�� �ʱ� ���� ����
            if (ImStillDown == true)
            {
                if (downandRise != null)
                    StopCoroutine(downandRise);
                animator.SetBool("Get up front, Infector", false);
            }

            //�ٸ��� �߸� ���, �Ѿ���
            if (GetComponent<TearInfector>().LegL == true || GetComponent<TearInfector>().LegR == true)
            {
                if (DownTime == 0)
                {
                    DownTime += Time.deltaTime;
                    ImStillDown = true;
                    currnetSpeed = 0;

                    if (attack1Go != null)
                        StopCoroutine(attack1Go);
                    if (attack2Go != null)
                        StopCoroutine(attack2Go);
                    if (attackDown != null)
                        StopCoroutine(attackDown);
                    if (jumpAttackStart != null)
                        StopCoroutine(jumpAttackStart);
                    if (jumpAttackStart2 != null)
                        StopCoroutine(jumpAttackStart2);

                    StartCoroutine(ImDown());
                }
            }

            //�Ѿ��� ������ ���ִ� �ڼ� ��Ȱ��ȭ
            if (DownState == true && deathStop == false)
            {
                animator.SetFloat("Attack(Blade), Taitroki", 0);
                animator.SetBool("Movement(Blade), Taitroki", false);
            }

            float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

            //Debug.DrawLine(rb2d.position, PlayerPosition, Color.red);
            //Debug.Log("NearAttack : " + NearAttack);
            //Debug.Log("NearTime : " + NearTime);
            //Debug.Log("NearAttackTime : " + NearAttackTime);
            //Debug.Log("currnetSpeed : " + currnetSpeed);
            //Debug.Log("MoveComplete : " + MoveComplete);

            //�Ѿ����� ���� ����
            if (DownState == true && NoArm == false && deathStop == false && FlameDeath == false)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                if (moveToWard != null)
                    StopCoroutine(moveToWard);

                if (distanceFromPlayer >= 0.5f)
                {
                    animator.SetBool("idle down(Blade), Taitroki", false);
                    animator.SetBool("Down moving(Blade), Taitroki", true);
                    transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);

                    //���� ��� ������ �� ���������� ����
                    if (GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LURend == false && GetComponent<TearInfector>().B1LDRend == false && GetComponent<TearInfector>().B1LULend == false)
                    {
                        currnetSpeed = 4;
                    }
           
                    if (GetComponent<TearInfector>().B1LDLend == true || GetComponent<TearInfector>().B1LULend == true || GetComponent<TearInfector>().B1LDRend == true || GetComponent<TearInfector>().B1LURend == true)
                    {
                        //������ ���� �߸� ���·� ����
                        if (GetComponent<TearInfector>().B1LURend == true || GetComponent<TearInfector>().B1LDRend == true)
                        {
                            if (GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LULend == false) //���� ���� �� �߷��� ���
                            {
                                if (CrawlTime == 0)
                                {
                                    //Debug.Log("���� ����");
                                    CrawlTime += Time.deltaTime;
                                    StartCoroutine(LeftCrawl());
                                }
                            }
                            else if (GetComponent<TearInfector>().B1LDLend == true || GetComponent<TearInfector>().B1LULend == true)
                            {
                                NoArm = true;
                            }
                        }
                        //���� ���� �߸� ���·� ����
                        if (GetComponent<TearInfector>().B1LULend == true || GetComponent<TearInfector>().B1LDLend == true)
                        {
                            if (GetComponent<TearInfector>().B1LDRend == false && GetComponent<TearInfector>().B1LURend == false) //������ ���� �� �߷��� ���
                            {
                                if (CrawlTime == 0)
                                {
                                    //Debug.Log("������ ����");
                                    CrawlTime += Time.deltaTime;
                                    StartCoroutine(RightCrawl());
                                }
                            }
                            else if (GetComponent<TearInfector>().B1LDRend == true || GetComponent<TearInfector>().B1LURend == true)
                            {
                                NoArm = true;
                            }
                        }
                    }
                }
                else //�Ѿ��� ���¿��� ���� ��� ���� ��
                {
                    currnetSpeed = 0;
                    animator.SetBool("Down moving(Blade), Taitroki", false);
                }
            }

            //������ �������� ���, ��������
            if (distanceFromPlayer < NearAttackSite && deathStop == false && UsingJumpAttack == false && NoArm == false && Rising == false && ImStillDown == false 
                && FlameDeath == false)
            {
                if (DownState == false)
                {
                    if (GetComponent<TearInfector>().B1LULend == false && GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LURend == false && GetComponent<TearInfector>().B1LDRend == false) //����
                    {
                        JumpNearTime = 0;

                        if (NearTime == 0)
                        {
                            NearTime += Time.deltaTime;
                            if (jumpAttackAccount != null)
                                StopCoroutine(jumpAttackAccount);
                            randomNearAttack = StartCoroutine(RandomNearAttack());
                        }

                        if (NearAttack == false && NearAttackTime == 1)
                        {
                            NearAttack = true;
                            NearAttackTime = 0;
                            if (randomNearAttack != null)
                                StopCoroutine(randomNearAttack);

                            NearAttackRandom = Random.Range(1, 3);

                            NearAttacking = true;

                            if (NearAttackRandom == 1)
                            {
                                attack1Go = StartCoroutine(Attack1Go());
                            }
                            else
                            {
                                attack2Go = StartCoroutine(Attack2Go());
                            }
                        }
                    }
                    else if (GetComponent<TearInfector>().B1LURend == true || GetComponent<TearInfector>().B1LDRend == true) //���ȸ� �������� ��
                    {
                        if (NoArm == false)
                        {
                            JumpNearTime = 0;

                            if (NearTime == 0)
                            {
                                NearTime += Time.deltaTime;
                                if (jumpAttackAccount != null)
                                    StopCoroutine(jumpAttackAccount);
                                randomNearAttack = StartCoroutine(RandomNearAttack());
                            }

                            if (NearAttack == false && NearAttackTime == 1)
                            {
                                NearAttack = true;
                                NearAttackTime = 0;
                                if (randomNearAttack != null)
                                    StopCoroutine(randomNearAttack);

                                NearAttacking = true;
                                attack1Go = StartCoroutine(Attack1Go());
                            }
                        }
                    }
                    else if (GetComponent<TearInfector>().B1LULend == true || GetComponent<TearInfector>().B1LDLend == true) //�����ȸ� �������� ��
                    {
                        if (NoArm == false)
                        {
                            JumpNearTime = 0;

                            if (NearTime == 0)
                            {
                                NearTime += Time.deltaTime;
                                if (jumpAttackAccount != null)
                                    StopCoroutine(jumpAttackAccount);
                                randomNearAttack = StartCoroutine(RandomNearAttack());
                            }

                            if (NearAttack == false && NearAttackTime == 1)
                            {
                                NearAttack = true;
                                NearAttackTime = 0;
                                if (randomNearAttack != null)
                                    StopCoroutine(randomNearAttack);

                                NearAttacking = true;
                                attack1Go = StartCoroutine(Attack2Go());
                            }
                        }
                    }
                }
                else //�Ѿ��� ���¿��� ����
                {
                    if (NearAttack == false)
                    {
                        //Debug.Log("����");
                        NearAttack = true;
                        NearAttacking = true;
                        attackDown = StartCoroutine(AttackDown());
                    }
                }
            }

            //���� ���� ������ ����� ���, ���� ����
            else if(distanceFromPlayer > NearAttackSite + 4 && deathStop == false && NearAttack == false && NearAttacking == false && NoArm == false && Rising == false 
                && ImStillDown == false && FlameDeath == false)
            {
                if (DownState == false)
                {
                    if (GetComponent<TearInfector>().B1LULend == false && GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LURend == false && GetComponent<TearInfector>().B1LDRend == false) //����
                    {
                        NearTime = 0;

                        if (JumpNearTime == 0)
                        {
                            JumpNearTime += Time.deltaTime;
                            if (randomNearAttack != null)
                                StopCoroutine(randomNearAttack);
                            jumpAttackAccount = StartCoroutine(JumpAttackAccount());
                        }

                        if (UsingJumpAttack == false && jumpAttackTime == 1)
                        {
                            UsingJumpAttack = true;
                            jumpAttackTime = 0;

                            if (jumpAttackAccount != null)
                                StopCoroutine(jumpAttackAccount);

                            if (UsingJumpAttack == true)
                            {
                                NearAttackRandom = Random.Range(1, 3);
                                if (NearAttackRandom == 1)
                                    jumpAttackStart = StartCoroutine(JumpAttackStart());
                                else
                                    jumpAttackStart2 = StartCoroutine(JumpAttackStart2());
                            }
                        }
                    }
                    else if (GetComponent<TearInfector>().B1LURend == true || GetComponent<TearInfector>().B1LDRend == true) //���ȸ� �������� ��
                    {
                        NearTime = 0;

                        if (JumpNearTime == 0)
                        {
                            JumpNearTime += Time.deltaTime;
                            if (randomNearAttack != null)
                                StopCoroutine(randomNearAttack);
                            jumpAttackAccount = StartCoroutine(JumpAttackAccount());
                        }

                        if (UsingJumpAttack == false && jumpAttackTime == 1)
                        {
                            UsingJumpAttack = true;
                            jumpAttackTime = 0;

                            if (jumpAttackAccount != null)
                                StopCoroutine(jumpAttackAccount);

                            if (UsingJumpAttack == true)
                            {
                                jumpAttackStart2 = StartCoroutine(JumpAttackStart2());
                            }
                        }
                    }
                    else if (GetComponent<TearInfector>().B1LULend == true || GetComponent<TearInfector>().B1LDLend == true) //�����ȸ� �������� ��
                    {
                        NearTime = 0;

                        if (JumpNearTime == 0)
                        {
                            JumpNearTime += Time.deltaTime;
                            if (randomNearAttack != null)
                                StopCoroutine(randomNearAttack);
                            jumpAttackAccount = StartCoroutine(JumpAttackAccount());
                        }

                        if (UsingJumpAttack == false && jumpAttackTime == 1)
                        {
                            UsingJumpAttack = true;
                            jumpAttackTime = 0;

                            if (jumpAttackAccount != null)
                                StopCoroutine(jumpAttackAccount);

                            if (UsingJumpAttack == true)
                            {
                                jumpAttackStart = StartCoroutine(JumpAttackStart());
                            }
                        }
                    }
                }
            }

            //�׾��� ��
            if (deathStop == true)
            {
                currnetSpeed -= Time.deltaTime * 8f; //����
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                if (movementAround != null)
                    StopCoroutine(movementAround);
                transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);

                if (currnetSpeed < 0)
                {
                    currnetSpeed = 0;
                }
            }

            if (FlameDeath == true)
            {
                if (FlameTime == 0)
                {
                    FlameTime += Time.deltaTime;
                    FlameMove = StartCoroutine(FlameMovement());
                }
            }
        }
    }

    //�����̵� ���̴��� ����
    IEnumerator TransformBladeHider()
    {
        int BladeHiderActivation = Random.Range(1, 4);
        
        if (BladeHiderActivation == 1)
        {
            SoundManager.instance.SFXPlay15("Sound", TransformSound1);
            animator.SetFloat("Transforming(Blade), Taitroki", 1);
        }
        else if (BladeHiderActivation == 2)
        {
            SoundManager.instance.SFXPlay11("Sound", TransformSound2);
            animator.SetFloat("Transforming(Blade), Taitroki", 2);
        }
        else if (BladeHiderActivation == 3)
        {
            SoundManager.instance.SFXPlay10("Sound", TransformSound3);
            animator.SetFloat("Transforming(Blade), Taitroki", 3);
        }

        yield return new WaitForSeconds(0.1f);

        if (GetComponent<TearInfector>().B1LURend == false && GetComponent<TearInfector>().B1LDRend == false)
        {
            transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(true);
        }

        if (GetComponent<TearInfector>().B1LURend == false)
        {
            transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(true);
            transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(true);
        }

        if (GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LULend == false)
        {
            transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(true);
            transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(true);
            transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(true);
            transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(true);
        }

        if (GetComponent<TearInfector>().B1LULend == false)
        {
            transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(true);
            transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(true);
            transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(true);
        }

        transform.Find("bone_1/bone_f1/Part3").gameObject.SetActive(true);
        transform.Find("bone_1/bone_f1/Part9").gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
        animator.SetBool("idle(Blade), Taitroki", true);
        animator.SetFloat("Transforming(Blade), Taitroki", 0);
        TaitrokiAcitvated = true;
        currnetSpeed = speed;
        moveToWard = StartCoroutine(MoveToWard());
        movementAround = StartCoroutine(MovementAround());
    }

    //�÷��̾� �ֺ� ��ȸ
    IEnumerator MovementAround()
    {
        if (LegsDown == false && deathStop == false && UsingJumpAttack == false && Rising == false && FlameDeath == false)
        {
            while (true)
            {
                float RandomMovement = Random.Range(3, 6);
                float RandomWander = Random.Range(0.25f, 1);

                PlayerPosition = new Vector3(Random.Range(Enemytarget.transform.position.x - RandomMovement, Enemytarget.transform.position.x + RandomMovement),
                    Random.Range(Enemytarget.transform.position.y - RandomMovement, Enemytarget.transform.position.y + RandomMovement), transform.position.z);

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
    IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - PlayerPosition).sqrMagnitude;

        if (LegsDown == false && deathStop == false && UsingJumpAttack == false && Rising == false && FlameDeath == false)
        {
            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    MoveComplete = false;
                    Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, PlayerPosition, speed * Time.deltaTime);

                    if (newPosition == PlayerPosition || transform.position == PlayerPosition) //��ǥ������ ���� ������ ������ ���, ���ڸ����� ����ä�� �̵� �ִϸ��̼��� Ȱ��ȭ�Ǵ� ������ ����
                    {
                        //Debug.Log(string.Format("newPosition : {0}, endposition : {1}, rigidbodyToMove.position : {2}", newPosition, endposition, rigidbodyToMove.position));
                        MoveComplete = true;
                        break;
                    }

                    rb2d.MovePosition(newPosition);
                    remainingDistance = (transform.position - PlayerPosition).sqrMagnitude;
                }
                yield return new WaitForFixedUpdate();

                if (transform.position == PlayerPosition)
                {
                    MoveComplete = true;
                }
            }
        }
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if (deathStop == false)
        {
            if (NoArm == false || DownState == false)
            {
                if (Enemytarget.transform.position.x > transform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
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
                animator.SetBool("Movement(Blade), Taitroki", true);
                if (animator.GetBool("idle down(Blade), Taitroki") == true)
                animator.SetBool("idle down(Blade), Taitroki", false);

                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("TaitrokiMoving", -1f);
                }
                else //����
                {
                    animator.SetFloat("TaitrokiMoving", 1f);
                }
            }
            else if (transform.rotation.y != 0) //�������� �Ĵٺ��� ���� ��
            {
                animator.SetBool("Movement(Blade), Taitroki", true);
                if (animator.GetBool("idle down(Blade), Taitroki") == true)
                    animator.SetBool("idle down(Blade), Taitroki", false);

                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("TaitrokiMoving", 1f);
                }
                else //����
                {
                    animator.SetFloat("TaitrokiMoving", -1f);
                }
            }
            if (MoveComplete == true || LegsDown == true)
            {
                if (DownState == false)
                {
                    if (animator.GetBool("idle down(Blade), Taitroki") == true)
                        animator.SetBool("idle down(Blade), Taitroki", false);
                    animator.SetBool("Movement(Blade), Taitroki", false);
                    animator.SetBool("idle(Blade), Taitroki", true);
                }
            }
        }
    }

    //���� ���� ����
    IEnumerator RandomNearAttack()
    {
        while (true)
        {
            NearAttackTime = Random.Range(0, NearAttackLevel);
            yield return new WaitForSeconds(1f);
        }
    }

    //���� ���� ����
    IEnumerator JumpAttackAccount()
    {
        while (true)
        {
            jumpAttackTime = Random.Range(0, JumpAttackLevel);
            yield return new WaitForSeconds(1f);
        }
    }

    //���� ����(������)
    IEnumerator Attack1Go()
    {
        SoundManager.instance.SFXPlay13("Sound", Attack1);
        animator.SetFloat("Attack(Blade), Taitroki", 1);
        yield return new WaitForSeconds(0.43f);
        DamagePos.GetComponent<NearDamage>().SetDamage(Damage, 0.2f);
        DamagePos.SetActive(true);
        yield return new WaitForSeconds(0.57f);
        animator.SetFloat("Attack(Blade), Taitroki", 0);
        NearTime = 0;
        NearAttack = false;
        NearAttacking = false;
    }

    //���� ����(����)
    IEnumerator Attack2Go()
    {
        SoundManager.instance.SFXPlay13("Sound", Attack2);
        animator.SetFloat("Attack(Blade), Taitroki", 2);
        yield return new WaitForSeconds(0.46f);
        DamagePos.GetComponent<NearDamage>().SetDamage(Damage, 0.2f);
        DamagePos.SetActive(true);
        yield return new WaitForSeconds(0.54f);
        animator.SetFloat("Attack(Blade), Taitroki", 0);
        NearTime = 0;
        NearAttack = false;
        NearAttacking = false;
    }

    //���� ����(������)
    IEnumerator JumpAttackStart()
    {
        SoundManager.instance.SFXPlay2("Sound", JumpAttack);
        if (movementAround != null)
            StopCoroutine(movementAround);
        currnetSpeed = 0;
        Vector3 EndPosition = Enemytarget.transform.position; //��������
        animator.SetFloat("Attack jump(Blade), Taitroki", 1);
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.SFXPlay13("Sound", Attack1);
        StartCoroutine(Jumping(EndPosition, 0.8f));
        yield return new WaitForSeconds(0.5f);
        DamagePos.GetComponent<NearDamage>().SetDamage(JumpDamage, 0.33f);
        DamagePos.SetActive(true);
        yield return new WaitForSeconds(0.583f);
        animator.SetFloat("Attack jump(Blade), Taitroki", 0);
        UsingJumpAttack = false;
        JumpNearTime = 0;
        currnetSpeed = speed;
        movementAround = StartCoroutine(MovementAround());
    }

    //���� ����(����)
    IEnumerator JumpAttackStart2()
    {
        SoundManager.instance.SFXPlay2("Sound", JumpAttack);
        if (movementAround != null)
            StopCoroutine(movementAround);
        currnetSpeed = 0;
        Vector3 EndPosition = Enemytarget.transform.position; //��������
        animator.SetFloat("Attack jump(Blade), Taitroki", 2);
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.SFXPlay13("Sound", Attack2);
        StartCoroutine(Jumping(EndPosition, 0.8f));
        yield return new WaitForSeconds(0.583f);
        DamagePos.GetComponent<NearDamage>().SetDamage(JumpDamage, 0.33f);
        DamagePos.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Attack jump(Blade), Taitroki", 0);
        UsingJumpAttack = false;
        JumpNearTime = 0;
        currnetSpeed = speed;
        movementAround = StartCoroutine(MovementAround());
    }

    //���� ������ ����
    public IEnumerator Jumping(Vector3 destination, float duration) //�������� ���� ���� ������Ʈ�� �����̴� �޼���. destination�� ���� ������, duration�� ���������� ���µ� �ɸ��� �ð�.
    {
        var startPosition = transform.position; //transform.position�� ���������� ���� �����Ѵ�.
        var percentComplete = 0.0f; //���������� ����� �� ����� ����. ���� 0%�� �صд�.
        while (percentComplete < 1.0f) //percentComplete�� 1.0(100%)���� ������ Ȯ��.
        {
            percentComplete += Time.deltaTime / duration; //Time.deltaTime�� ���� �������� �׸� ���ķ� �帥 �ð�, percentComplete�� ���������� ��������� ���� �����ӿ����� ������� ���� �� ������̴�.
            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete); //������ �߻� �ڵ�. ���� ��� 0���� ���ֱ������ �������� ��� � ���� �ǹ��Ѵ�. /*������ �ڵ�*/
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) /*���⼭ ���� ������ �ڵ�*/ + Vector3.up * 6/*�ִ����*/ * currentHeight; //���������� ����ؼ� �ִ��� �Ų����� �̵��ϴ� �� �����ϵ��� �� 3���� ��Ҹ� ����. �޼���� Lerp()�̴�.
            yield return null; //���� �����ӱ��� �ڷ�ƾ�� ������ �Ͻ� �����Ѵ�.
        }
    }

    //�Ѿ��� ���¿��� �����ϱ�
    IEnumerator AttackDown()
    {
        SoundManager.instance.SFXPlay13("Sound", Attack1);
        animator.SetBool("Attack crawl(Blade), Taitroki", true);
        yield return new WaitForSeconds(0.33f);
        DamagePos.GetComponent<NearDamage>().SetDamage(Damage, 0.2f);
        DamagePos.SetActive(true);
        yield return new WaitForSeconds(0.416f);
        animator.SetBool("Attack crawl(Blade), Taitroki", false);
        NearAttack = false;
        NearAttacking = false;
    }

    //�Ѿ����� �Ͼ��
    IEnumerator DownandRise()
    {
        animator.SetBool("Get up front(Blade), Infector", true);
        yield return new WaitForSeconds(2.083f);
        animator.SetBool("Get up front(Blade), Infector", false);
        GetComponent<TearInfector>().TakeDown = 0;
        DownTime = 0;
        currnetSpeed = speed;
        Rising = false;
    }

    //�ٸ��� �߷��� �Ѿ����� �ִϸ��̼�
    IEnumerator ImDown()
    {
        //Debug.Log("I'm down!");
        animator.SetBool("Get up front(Blade), Infector", false);
        animator.SetBool("Down(Blade), Taitroki", true);
        currnetSpeed = 0;
        yield return new WaitForSeconds(0.66f);
        Rising = false;
        NearAttack = false;
        UsingJumpAttack = false;
        ImStillDown = false;
        DownState = true;
        currnetSpeed = 4;
        animator.SetBool("idle down(Blade), Taitroki", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Down(Blade), Taitroki", false);
        //Debug.Log("I'm down!");
    }

    //�����ȷ� ����
    IEnumerator RightCrawl()
    {
        while (true)
        {
            if (GetComponent<TearInfector>().B1LDRend == false && GetComponent<TearInfector>().B1LURend == false)
            {
                //Debug.Log("Right currnetSpeed : " + currnetSpeed);
                currnetSpeed = 0;
                yield return new WaitForSeconds(0.25f);
                currnetSpeed = 4;
                yield return new WaitForSeconds(0.33f);
                CrawlTime = 0;
            }
            else if (GetComponent<TearInfector>().B1LDRend == true || GetComponent<TearInfector>().B1LURend == true)
                break;
        }
    }

    //���ȷ� ����
    IEnumerator LeftCrawl()
    {
        while (true)
        {
            if (GetComponent<TearInfector>().B1LDLend == false && GetComponent<TearInfector>().B1LULend == false)
            {
                //Debug.Log("Left currnetSpeed : " + currnetSpeed);
                currnetSpeed = 4;
                yield return new WaitForSeconds(0.33f);
                currnetSpeed = 0;
                yield return new WaitForSeconds(0.25f);
                CrawlTime = 0;
            }
            else if (GetComponent<TearInfector>().B1LDLend == true || GetComponent<TearInfector>().B1LULend == true)
                break;
        }
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
                if (Type == 1)
                {
                    if (DownState == false)
                    {
                        animator.SetBool("Movement(Blade), Taitroki", true);
                        animator.SetFloat("TaitrokiMoving", 1f);
                    }
                    else if (DownState = true && NoArm == false)
                        animator.SetBool("Down moving(Blade), Taitroki", true);
                }
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