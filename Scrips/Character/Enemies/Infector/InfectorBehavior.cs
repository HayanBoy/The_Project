using System.Collections;
using UnityEngine;

public class InfectorBehavior : MonoBehaviour
{
    ObjectManager objectManager;
    public InfectorSpawn infectorSpawn;
    public HealthInfector healthInfector;
    public TearInfector tearInfector;

    private int idleChange; //�� �ִ� �ڼ� �������� �ٲٱ�
    private int idleSeizure; //�� �ִ� �ڼ� ���� ����
    private int moveType; //�����̱� ����
    private int MovingSpeed; //�����̱� �ִϸ��̼�
    private int MovingTop; //��ü ������ �ִϸ��̼�
    public float currnetSpeed; //�����ӵ�
    float VoiceTime;
    int AttackRandom;
    int VoiceRandom;
    int VoicePrint;
    public GameObject DamagePos;

    public float FollowVector; //���� ���� �÷��̾� �������� �̵��ϱ� ���� ����
    public float traceSite; //�ش� �ݰ� ���� �÷��̾ �����ϱ� ���� �뵵
    public Transform Enemytarget = null;

    Coroutine attack1;
    Coroutine attack2;
    Coroutine attack3;
    Coroutine attack4;
    Coroutine attack5;
    Coroutine downandRise;

    public bool IntoLine = false; //���� ����, �÷��̾ �ִ� �������� �����ϱ� ���� ����ġ
    public bool IntoLineSpeed = false; //���� ����, �÷��̾� ���� ��ó�� ���� �� ������ �ӵ��� ����
    private bool AttackBool1 = false;
    private bool AttackBool2 = false;
    private bool AttackBool3 = false;
    private bool AttackBool4 = false;
    private bool AttackBool5 = false;
    private bool downandRiseBool = false;
    public bool TaitrokiAcitvated = false;

    //public GameObject DamageObject;
    //public Transform DamageObjectPos; //������ �Ѿ� ���� ��ǥ
    public int Damage1;
    public int DamageBite;
    private float DownTime;
    private float CrawlTime;

    private bool idleTention = true;
    private bool ImStillDown = false; //�Ѿ����� �ִ� ������ ����ġ
    private bool Rising = false; //�Ͼ�� �ִ� ������ ����ġ
    public bool deathStop = false;
    public bool isAttacking = false;
    bool DownState = false;
    bool NoArm = false;

    Coroutine FlameMove;
    Coroutine moveCoroutine;
    Vector3 endposition; //���� ������ ��ġ��
    public bool FlameDeath = false; //�ҿ� ź ���¿��� ���� ��
    private float FlameTime;

    Animator animator;

    public Transform magnetForm = null;
    public float MagnetForce;

    public AudioClip Voice1;
    public AudioClip Voice2;
    public AudioClip Voice3;
    public AudioClip Voice4;
    public AudioClip Voice5;
    public AudioClip SwingFist1;
    public AudioClip SwingFist2;
    public AudioClip SwingFist3;
    public AudioClip MouthAttack1;
    public AudioClip MouthAttack2;

    public void InfectorDeath(bool droping)
    {
        if(droping == true)
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
        currnetSpeed = 9;
    }

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;

        StartCoroutine(SeizureTime());
        StartCoroutine(idleTensionChange());
        StartCoroutine(VoiceSound());

        moveType = Random.Range(0, 5); //�̵� ���� ����

        if (moveType == 0)
        {
            MovingTop = Random.Range(1, 3);
        }
        else if (moveType == 1)
        {
            MovingTop = Random.Range(3, 5);
        }
        else if (moveType == 2)
        {
            MovingTop = Random.Range(5, 7);
        }
    }

    IEnumerator VoiceSound()
    {
        while(true)
        {
            VoiceRandom = Random.Range(0, 10);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            Damage1 = 30;
            DamageBite = 60;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            Damage1 = 42;
            DamageBite = 84;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            Damage1 = 48;
            DamageBite = 96;
        }

        if (idleTention == false)
            idleTention = true;
        if (deathStop == true)
            deathStop = false;
        if (isAttacking == true)
            isAttacking = false;
        if (DownState == true)
            DownState = false;
        if (NoArm == true)
            NoArm = false;
        if (ImStillDown == true)
            ImStillDown = false;
        if (Rising == true)
            Rising = false;
        if (TaitrokiAcitvated == true)
            TaitrokiAcitvated = false;
        if (FlameDeath == true)
            FlameDeath = false;

        if (AttackBool1 == true)
            AttackBool1 = false;
        if (AttackBool2 == true)
            AttackBool2 = false;
        if (AttackBool3 == true)
            AttackBool3 = false;
        if (AttackBool4 == true)
            AttackBool4 = false;
        if (AttackBool5 == true)
            AttackBool5 = false;
        if (downandRiseBool == true)
            downandRiseBool = false;

        if (MovingSpeed != 0)
            MovingSpeed = 0;
        if (currnetSpeed != 0)
            currnetSpeed = 0;
        if (DownTime != 0)
            DownTime = 0;
        if (MovingTop != 0)
            MovingTop = 0;
        if (FlameTime != 0)
            FlameTime = 0;
        StartCoroutine(SeizureTime());
        StartCoroutine(idleTensionChange());

        moveType = Random.Range(0, 5); //�̵� ���� ����

        if (moveType == 0)
            MovingTop = Random.Range(1, 3);
        else if (moveType == 1)
            MovingTop = Random.Range(3, 5);
        else if (moveType == 2)
            MovingTop = Random.Range(5, 7);

        if (FlameMove != null)
            StopCoroutine(FlameMove);
    }

    private void OnDisable()
    {
        if (idleTention == false)
            idleTention = true;
        if (deathStop == true)
            deathStop = false;
        if (isAttacking == true)
            isAttacking = false;
        if (DownState == true)
            DownState = false;
        if (NoArm == true)
            NoArm = false;
        if (ImStillDown == true)
            ImStillDown = false;
        if (Rising == true)
            Rising = false;
        if (TaitrokiAcitvated == true)
            TaitrokiAcitvated = false;

        if (AttackBool1 == true)
            AttackBool1 = false;
        if (AttackBool2 == true)
            AttackBool2 = false;
        if (AttackBool3 == true)
            AttackBool3 = false;
        if (AttackBool4 == true)
            AttackBool4 = false;
        if (AttackBool5 == true)
            AttackBool5 = false;
        if (downandRiseBool == true)
            downandRiseBool = false;

        if (MovingSpeed != 0)
            MovingSpeed = 0;
        if (currnetSpeed != 0)
            currnetSpeed = 0;
        if (DownTime != 0)
            DownTime = 0;
        if (MovingTop != 0)
            MovingTop = 0;

        if (FlameMove != null)
            StopCoroutine(FlameMove);

        animator.SetFloat("Moving Top", 0);
        animator.SetFloat("MovingSpeed", 0);
        animator.SetFloat("DownMovingSpeed", 0);
        if (animator.GetBool("Down, Infector") == true)
            animator.SetBool("Down, Infector", false);
    }

    void Update()
    {
        if (IntoLine == false)
        {
            //Debug.Log("currnetSpeed : " + currnetSpeed);

            if (magnetForm != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(magnetForm.position.x, magnetForm.position.y, transform.position.z), MagnetForce * Time.deltaTime); //Ư�� ������ �̵�
            }

            if (TaitrokiAcitvated == false)
            {
                Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
                LookAtPlayer();
            }

            if (tearInfector.TakeDown == 1 && TaitrokiAcitvated == false)
            {
                if (DownTime == 0)
                {
                    Rising = true;
                    DownTime += Time.deltaTime;
                    if (AttackBool1 == true)
                        StopCoroutine(attack1);
                    if (AttackBool2 == true)
                        StopCoroutine(attack2);
                    if (AttackBool3 == true)
                        StopCoroutine(attack3);
                    if (AttackBool4 == true)
                        StopCoroutine(attack4);
                    if (AttackBool5 == true)
                        StopCoroutine(attack5);
                    animator.SetBool("Attack1, Infector", false);
                    animator.SetBool("Attack2, Infector", false);
                    animator.SetBool("Attack3, Infector", false);
                    animator.SetBool("Attack4, Infector", false);
                    currnetSpeed = 0;
                    MovingSpeed = 0;
                    animator.SetFloat("MovingSpeed", 0);
                    MovingTop = 0;
                    animator.SetFloat("Moving Top", 0);
                    healthInfector.DamageType = 0;

                    downandRiseBool = true;
                    downandRise = StartCoroutine(DownandRise());
                }
            }

            //�Ͼ�� ���� �� �ٸ� �ൿ�� �߻����� �ʱ� ���� ����
            if (Rising == true || TaitrokiAcitvated == true)
            {
                currnetSpeed = 0;
                MovingSpeed = 0;
                animator.SetFloat("MovingSpeed", 0);
                MovingTop = 0;
                animator.SetFloat("Moving Top", 0);
                healthInfector.DamageType = 0;

                if (AttackBool1 == true)
                    StopCoroutine(attack1);
                if (AttackBool2 == true)
                    StopCoroutine(attack2);
                if (AttackBool3 == true)
                    StopCoroutine(attack3);
                if (AttackBool4 == true)
                    StopCoroutine(attack4);
                if (AttackBool5 == true)
                    StopCoroutine(attack5);
                animator.SetBool("Attack1, Infector", false);
                animator.SetBool("Attack2, Infector", false);
                animator.SetBool("Attack3, Infector", false);
                animator.SetBool("Attack4, Infector", false);
            }

            if (tearInfector.B1LURend == true && tearInfector.B1LULend == true) //���� ����� ���ư�
                NoArm = true;
            if (tearInfector.B1LDLend == true && tearInfector.B1LDRend == true) //���� �ϴ��� ���ư�
                NoArm = true;
            if (tearInfector.B1LURend == true && tearInfector.B1LDLend == true) //������ ���, ���� �ϴ��� ���ư�
                NoArm = true;
            if (tearInfector.B1LULend == true && tearInfector.B1LDRend == true) //���� ���, ������ �ϴ��� ���ư�
                NoArm = true;

            if (VoiceRandom == 0 && deathStop == false)
            {
                VoicePrint = Random.Range(0, 5);

                if (VoicePrint == 0)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay25("Sound", Voice1);
                    }
                }
                else if (VoicePrint == 1)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay26("Sound", Voice2);
                    }
                }
                else if (VoicePrint == 2)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay27("Sound", Voice3);
                    }
                }
                else if (VoicePrint == 3)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay24("Sound", Voice4);
                    }
                }
                else if (VoicePrint == 4)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay28("Sound", Voice5);
                    }
                }
            }
            else
            {
                VoiceTime = 0;
            }

            float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position); //�÷��̾��� ��ġ�� �ν��ϱ� ���� �뵵

            //�÷��̾� ����
            if (distanceFromPlayer < traceSite && deathStop == false && FlameDeath == false && TaitrokiAcitvated == false)
            {
                animator.SetBool("idle1, Infector", false);
                animator.SetBool("idle2, Infector", false);
                animator.SetBool("idle3, Infector", false);

                if (healthInfector.ImHit == false)
                {
                    if (tearInfector.LegL == false && tearInfector.LegR == false && tearInfector.TakeDown != 1)
                    {
                        if (distanceFromPlayer >= 0.5f)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                            if (DownState == false && Rising == false)
                                Movement(); //�̵� �ִϸ��̼����� ��ȯ.
                        }
                        else
                        {
                            currnetSpeed = 0;
                            animator.SetFloat("Moving Top", 0);
                            animator.SetFloat("MovingSpeed", 0);
                        }
                    }
                    if (tearInfector.LegL == true || tearInfector.LegR == true) //�Ѿ����� �� ����
                    {
                        if (distanceFromPlayer >= 0.5f)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
                            if (tearInfector.B1LDLend == false && tearInfector.B1LURend == false && tearInfector.B1LDRend == false && tearInfector.B1LULend == false)
                            {
                                if (DownState == true)
                                    CrawlMovement(); //���� �̵� �ִϸ��̼����� ��ȯ.
                            }
                            if (tearInfector.B1LDLend == true || tearInfector.B1LULend == true || tearInfector.B1LDRend == true || tearInfector.B1LURend == true)
                            {
                                if (DownState == true && NoArm == false)
                                    CrawlNoArmMovement(); //���� �ȷ� ���� �̵� �ִϸ��̼����� ��ȯ.
                            }
                        }
                        else
                        {
                            currnetSpeed = 0;
                            animator.SetFloat("DownMovingSpeed", 0);
                        }
                    }
                }
            }
            else if (distanceFromPlayer > traceSite && deathStop == false && FlameDeath == false && TaitrokiAcitvated == false)
            {
                if (tearInfector.LegL == false && tearInfector.LegR == false && tearInfector.TakeDown != 1)
                {
                    MovingSpeed = 0;
                    animator.SetFloat("MovingSpeed", MovingSpeed);
                    animator.SetFloat("Moving Top", 0);
                    Idle(); //������ �ֱ� �ִϸ��̼����� ��ȯ.
                }
                if (tearInfector.LegL == true || tearInfector.LegR == true || DownState == true)
                {
                    animator.SetFloat("MovingSpeed", 0); //�Ѿ��� ä�� ������ �ִ� �ִϸ��̼����� ��ȯ
                }
            }

            //������ �߷��� ��� �ƹ��͵� ����
            if (NoArm == true && TaitrokiAcitvated == false)
            {
                if (tearInfector.LegL == true || tearInfector.LegR == true || DownState == true)
                {
                    currnetSpeed = 0;
                    animator.SetFloat("MovingSpeed", 0); //�Ѿ��� ä�� ������ �ִ� �ִϸ��̼����� ��ȯ
                }
            }

            //�Ѿ��� ������ ���ִ� �ڼ� ��Ȱ��ȭ
            if (DownState == true && TaitrokiAcitvated == false)
            {
                animator.SetBool("idle1, Infector", false);
                animator.SetBool("idle2, Infector", false);
                animator.SetBool("idle3, Infector", false);
                animator.SetBool("idle1-1, Infector", false);
                animator.SetBool("idle2-1, Infector", false);
                animator.SetBool("idle3-1, Infector", false);
            }

            //�ٸ��� �ɷ� �Ѿ����� ���� �ٸ��� �߷��� �Ѿ����� ���� �� �� �� ��ġ�� �ʱ� ���� ����
            if (ImStillDown == true && TaitrokiAcitvated == false)
            {
                if (downandRiseBool == true)
                    StopCoroutine(downandRise);
                animator.SetBool("Get up front, Infector", false);
            }

            //�ٸ��� �߸� ���, �Ѿ���
            if (tearInfector.LegL == true || tearInfector.LegR == true)
            {
                if (DownTime == 0 && TaitrokiAcitvated == false)
                {
                    ImStillDown = true;
                    DownTime += Time.deltaTime;
                    if (AttackBool1 == true)
                        StopCoroutine(attack1);
                    if (AttackBool2 == true)
                        StopCoroutine(attack2);
                    if (AttackBool3 == true)
                        StopCoroutine(attack3);
                    if (AttackBool4 == true)
                        StopCoroutine(attack4);
                    if (AttackBool5 == true)
                        StopCoroutine(attack5);
                    animator.SetBool("Attack1, Infector", false);
                    animator.SetBool("Attack2, Infector", false);
                    animator.SetBool("Attack3, Infector", false);
                    animator.SetBool("Attack4, Infector", false);
                    StartCoroutine(ImDown());
                }
            }

            if (deathStop == true && FlameDeath == false && TaitrokiAcitvated == false)
            {
                currnetSpeed -= Time.deltaTime * 8f; //����
                transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);

                animator.SetFloat("Moving Top", 0);
                animator.SetBool("idle1, Infector", true);
                MovingSpeed = 0;
                isAttacking = false;
                AttackBool1 = false;
                AttackBool2 = false;
                AttackBool3 = false;
                AttackBool4 = false;

                if (currnetSpeed < 0)
                    currnetSpeed = 0;
            }
        }
        else
        {
            if (TaitrokiAcitvated == false && deathStop == false && FlameDeath == false)
            {
                Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
                LookAtPlayer(); //�÷��̾� �ٶ󺸱�

                if (IntoLineSpeed == false)
                    transform.Translate(transform.right * FollowVector * 20 * Time.deltaTime);
                else
                {
                    Movement(); //�̵� �ִϸ��̼����� ��ȯ.
                    transform.Translate(transform.right * FollowVector * currnetSpeed * Time.deltaTime);
                }
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

    //������ �ֱ� �ִϸ��̼�
    void Idle()
    {
        if (idleChange == 0 && idleTention == true)
        {
            animator.SetBool("idle2, Infector", false);
            animator.SetBool("idle3, Infector", false);
            animator.SetBool("idle1, Infector", true);
        }
        else if (idleChange == 1 && idleTention == true)
        {
            animator.SetBool("idle1, Infector", false);
            animator.SetBool("idle3, Infector", false);
            animator.SetBool("idle2, Infector", true);
        }
        else if (idleChange == 2 && idleTention == true)
        {
            animator.SetBool("idle1, Infector", false);
            animator.SetBool("idle2, Infector", false);
            animator.SetBool("idle3, Infector", true);
        }
    }

    //���ִ� �ڼ� ����
    IEnumerator idleTensionChange()
    {
        while (idleTention == true)
        {
            idleChange = Random.Range(0, 3);
            yield return new WaitForSeconds(3);
        }
    }

    //���ֱ� ����
    IEnumerator SeizureTime()
    {
        while (idleTention == true)
        {
            yield return new WaitForSeconds(0.5f);
            idleSeizure = Random.Range(0, 3);

            if (idleChange == 0 && idleSeizure == 0)
            {
                animator.SetBool("idle1-1, Infector", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("idle1-1, Infector", false);
            }
            else if (idleChange == 1 && idleSeizure == 0)
            {
                animator.SetBool("idle2-1, Infector", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("idle2-1, Infector", false);
            }
            else if (idleChange == 2 && idleSeizure == 0)
            {
                animator.SetBool("idle3-1, Infector", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("idle3-1, Infector", false);
            }
        }
    }

    //�̵� �ִϸ��̼�
    void Movement()
    {
        if (moveType == 0)
        {
            MovingSpeed = 1;
            currnetSpeed = 1;
            animator.SetFloat("MovingSpeed", MovingSpeed);

            if(MovingTop == 1)
                animator.SetFloat("Moving Top", MovingTop);
            else if (MovingTop == 2)
                animator.SetFloat("Moving Top", MovingTop);
        }
        else if (moveType == 1)
        {
            MovingSpeed = 2;
            currnetSpeed = 2;
            animator.SetFloat("MovingSpeed", MovingSpeed);

            if (MovingTop == 3)
                animator.SetFloat("Moving Top", MovingTop);
            else if (MovingTop == 4)
                animator.SetFloat("Moving Top", MovingTop);
        }
        else if (moveType == 2)
        {
            MovingSpeed = 3;
            currnetSpeed = 4;
            animator.SetFloat("MovingSpeed", MovingSpeed);

            if (MovingTop == 5)
                animator.SetFloat("Moving Top", MovingTop);
            else if (MovingTop == 6)
                animator.SetFloat("Moving Top", MovingTop);
        }
        else if (moveType == 3)
        {
            MovingSpeed = 4;
            currnetSpeed = 6;
            animator.SetFloat("MovingSpeed", MovingSpeed);
        }
        else
        {
            MovingSpeed = 5;
            currnetSpeed = 7;
            animator.SetFloat("MovingSpeed", MovingSpeed);
        }
    }

    //�Ѿ����� �ִϸ��̼�
    IEnumerator ImDown()
    {
        //Debug.Log("I'm down!");
        animator.SetBool("Down, Infector", true);
        currnetSpeed = 0;
        animator.SetFloat("Moving Top", 0);
        yield return new WaitForSeconds(0.66f);
        ImStillDown = false;
        DownState = true;
    }

    //���� �ִϸ��̼�
    void CrawlMovement()
    {
        //Debug.Log("currnetSpeed : " + currnetSpeed);
        currnetSpeed = 1;
        animator.SetFloat("DownMovingSpeed", 1);
    }

    void CrawlNoArmMovement()
    {
        //������ ���� �߸� ����
        if (tearInfector.B1LURend == true || tearInfector.B1LDRend == true)
        {
            if(tearInfector.B1LDLend == false && tearInfector.B1LULend == false) //���� ���� �� �߷��� ���
            {
                if(CrawlTime == 0)
                {
                    CrawlTime += Time.deltaTime;
                    MovingSpeed = 1;
                    animator.SetFloat("DownMovingSpeed", 1);
                    StartCoroutine(LeftCrawl());
                }
            }
            else if(tearInfector.B1LDLend == true || tearInfector.B1LULend == true)
            {
                NoArm = true;
                MovingSpeed = 0;
            }
        }
        //���� ���� �߸� ����
        if (tearInfector.B1LULend == true || tearInfector.B1LDLend == true)
        {
            if (tearInfector.B1LDRend == false && tearInfector.B1LURend == false) //������ ���� �� �߷��� ���
            {
                if (CrawlTime == 0)
                {
                    CrawlTime += Time.deltaTime;
                    MovingSpeed = 1;
                    animator.SetFloat("DownMovingSpeed", MovingSpeed);
                    StartCoroutine(RightCrawl());
                }
            }
            else if(tearInfector.B1LDRend == true || tearInfector.B1LURend == true)
            {
                NoArm = true;
                MovingSpeed = 0;
            }
        }
    }

    IEnumerator RightCrawl()
    {
        while(true)
        {
            if (tearInfector.B1LDRend == false && tearInfector.B1LURend == false)
            {
                //Debug.Log("Right currnetSpeed : " + currnetSpeed);
                currnetSpeed = 0;
                yield return new WaitForSeconds(0.5f);
                currnetSpeed = 1;
                yield return new WaitForSeconds(0.416f);
                CrawlTime = 0;
            }
            else if (tearInfector.B1LDRend == true || tearInfector.B1LURend == true)
                break;
        }
    }

    IEnumerator LeftCrawl()
    {
        while(true)
        {
            if (tearInfector.B1LDLend == false && tearInfector.B1LULend == false)
            {
                //Debug.Log("Left currnetSpeed : " + currnetSpeed);
                currnetSpeed = 1;
                yield return new WaitForSeconds(0.416f);
                currnetSpeed = 0;
                yield return new WaitForSeconds(0.5f);
                CrawlTime = 0;
            }
            else if (tearInfector.B1LDLend == true || tearInfector.B1LULend == true)
                break;
        }
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if (deathStop == false && TaitrokiAcitvated == false && FlameDeath == false)
        {
            if (NoArm == false || tearInfector.LegL == false && tearInfector.LegR == false)
            {
                if (Enemytarget.transform.position.x > transform.position.x)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    //�÷��̾� ����
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"|| collision is BoxCollider2D && collision.gameObject.layer == 18)
        {
            if (isAttacking == false && deathStop == false && FlameDeath == false && TaitrokiAcitvated == false)
            {
                //Debug.Log("Zombie attack");

                if (tearInfector.LegL == false && tearInfector.LegR == false && tearInfector.TakeDown != 1)
                {
                    if (tearInfector.B1LULend == false && tearInfector.B1LDLend == false && tearInfector.B1LURend == false && tearInfector.B1LDRend == false) //����
                    {
                        //Debug.Log("Zombie attack");
                        AttackRandom = Random.Range(0, 4);

                        if (AttackRandom == 0)
                        {
                            //Debug.Log("Zombie attack1");
                            AttackBool1 = true;
                            attack1 = StartCoroutine(Attack1());
                        }
                        else if (AttackRandom == 1)
                        {
                            //Debug.Log("Zombie attack2");
                            AttackBool2 = true;
                            attack2 = StartCoroutine(Attack2());
                        }
                        else if (AttackRandom == 2)
                        {
                            //Debug.Log("Zombie attack3");
                            AttackBool3 = true;
                            attack3 = StartCoroutine(Attack3());
                        }
                        else if (AttackRandom == 3)
                        {
                            //Debug.Log("Zombie attack4");
                            AttackBool4 = true;
                            attack4 = StartCoroutine(Attack4());
                        }
                    }
                    else if (tearInfector.B1LURend == true || tearInfector.B1LDRend == true) //���ȸ� �������� ��
                    {
                        if (NoArm == false)
                        {
                            //Debug.Log("Zombie attack");
                            AttackRandom = Random.Range(0, 4);

                            if (AttackRandom == 0)
                            {
                                AttackBool1 = true;
                                attack1 = StartCoroutine(Attack1());
                            }
                            else if (AttackRandom == 1)
                            {
                                AttackBool2 = true;
                                attack2 = StartCoroutine(Attack2());
                            }
                            else if (AttackRandom == 2)
                            {
                                AttackBool3 = true;
                                attack3 = StartCoroutine(Attack3());
                            }
                            else if (AttackRandom == 3)
                            {
                                AttackBool4 = true;
                                attack4 = StartCoroutine(Attack4());
                            }
                        }
                        else if (NoArm == true) //������ ���� ��
                        {
                            AttackBool4 = true;
                            attack4 = StartCoroutine(Attack4());
                        }
                    }
                    else if (tearInfector.B1LULend == true || tearInfector.B1LDLend == true) //�����ȸ� �������� ��
                    {
                        if (NoArm == false)
                        {
                            //Debug.Log("Zombie attack");
                            AttackRandom = Random.Range(0, 3);

                            if (AttackRandom == 0)
                            {
                                AttackBool1 = true;
                                attack1 = StartCoroutine(Attack1());
                            }
                            else if (AttackRandom == 1)
                            {
                                AttackBool2 = true;
                                attack2 = StartCoroutine(Attack2());
                            }
                            else if (AttackRandom == 2)
                            {
                                AttackBool4 = true;
                                attack4 = StartCoroutine(Attack4());
                            }
                        }
                        else if (NoArm == true) //������ ���� ��
                        {
                            AttackBool4 = true;
                            attack4 = StartCoroutine(Attack4());
                        }
                    }
                }
                else if (tearInfector.LegL == true || tearInfector.LegR == true) //�Ѿ��� ���¿��� ����
                {
                    AttackBool5 = true;
                    attack5 = StartCoroutine(Attack5());
                }
            }
        }
    }


    IEnumerator Attack1()
    {
        if (deathStop == false)
        {
            isAttacking = true;
            animator.SetBool("Attack1, Infector", true);
            yield return new WaitForSeconds(0.85f);
            if (deathStop == false)
            {
                RandomSwingSound();
                DamagePos.GetComponent<NearDamage>().SetDamage(Damage1, 0.2f);
                DamagePos.SetActive(true);
            }
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Attack1, Infector", false);
            isAttacking = false;
            //Debug.Log("Zombie attack1");
        }
    }
    IEnumerator Attack2()
    {
        if (deathStop == false)
        {
            isAttacking = true;
            animator.SetBool("Attack2, Infector", true);
            yield return new WaitForSeconds(0.41f);
            if (tearInfector.B1LURend == false || tearInfector.B1LDRend == false)
            {
                if (deathStop == false)
                {
                    RandomSwingSound();
                    DamagePos.GetComponent<NearDamage>().SetDamage(Damage1 / 2, 0.2f);
                    DamagePos.SetActive(true);
                }
            }
            yield return new WaitForSeconds(1.25f);
            if (tearInfector.B1LULend == false || tearInfector.B1LDLend == false)
            {
                if (deathStop == false)
                {
                    RandomSwingSound();
                    DamagePos.GetComponent<NearDamage>().SetDamage(Damage1 / 2, 0.2f);
                    DamagePos.SetActive(true);
                }
            }
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Attack2, Infector", false);
            isAttacking = false;
            //Debug.Log("Zombie attack2");
        }
    }
    IEnumerator Attack3()
    {
        if (deathStop == false)
        {
            isAttacking = true;
            animator.SetBool("Attack3, Infector", true);
            yield return new WaitForSeconds(0.58f);
            if (deathStop == false)
            {
                RandomSwingSound();
                DamagePos.GetComponent<NearDamage>().SetDamage(Damage1, 0.2f);
                DamagePos.SetActive(true);
            }
            yield return new WaitForSeconds(0.58f);
            animator.SetBool("Attack3, Infector", false);
            isAttacking = false;
            //Debug.Log("Zombie attack3");
        }
    }
    IEnumerator Attack4()
    {
        if (deathStop == false)
        {
            isAttacking = true;
            animator.SetBool("Attack4, Infector", true);
            RandomMouthSound();
            yield return new WaitForSeconds(0.75f);
            if (infectorSpawn.inCap == false && deathStop == false)
            {
                DamagePos.GetComponent<NearDamage>().SetDamage(DamageBite, 0.2f);
                DamagePos.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("Attack4, Infector", false);
            isAttacking = false;
            //Debug.Log("Zombie attack4");
        }
    }
    IEnumerator Attack5()
    {
        if (deathStop == false)
        {
            isAttacking = true;
            animator.SetBool("Attack mouth, Infector", true);
            RandomMouthSound();
            yield return new WaitForSeconds(0.583f);
            if (infectorSpawn.inCap == false && deathStop == false)
            {
                DamagePos.GetComponent<NearDamage>().SetDamage(DamageBite, 0.2f);
                DamagePos.SetActive(true);
            }
            yield return new WaitForSeconds(0.583f);
            animator.SetBool("Attack mouth, Infector", false);
            isAttacking = false;
            //Debug.Log("Zombie attack5");
        }
    }

    IEnumerator DownandRise()
    {
        animator.SetBool("Get up front, Infector", true);
        yield return new WaitForSeconds(2.083f);
        animator.SetBool("Get up front, Infector", false);
        tearInfector.TakeDown = 0;
        DownTime = 0;
        Rising = false;
    }

    void RandomSwingSound()
    {
        int SwingSound = Random.Range(0, 3);

        if (SwingSound == 0)
            SoundManager.instance.SFXPlay12("Sound", SwingFist1);
        else if (SwingSound == 1)
            SoundManager.instance.SFXPlay12("Sound", SwingFist2);
        else
            SoundManager.instance.SFXPlay12("Sound", SwingFist3);
    }

    void RandomMouthSound()
    {
        int BiteSound = Random.Range(0, 2);

        if (BiteSound == 0)
            SoundManager.instance.SFXPlay12("Sound", MouthAttack1);
        else
            SoundManager.instance.SFXPlay12("Sound", MouthAttack2);
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

                endposition = new Vector3(Random.Range(transform.position.x - RandomMovement, transform.position.x + RandomMovement),
                    Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);

                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }

                moveCoroutine = StartCoroutine(Move());
                yield return new WaitForSeconds(RandomWander);
            }
        }
    }

    //������ ��ǥ�� ���� ������ ����
    public IEnumerator Move()
    {
        if (FlameDeath == true)
        {
            float remainingDistance = (transform.position - endposition).sqrMagnitude;
            float EndPoint = endposition.x;
            //Debug.Log("Flame Death Start");

            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (DownState == false)
                    Movement();
                else if (DownState == true && NoArm == false)
                    animator.SetFloat("DownMovingSpeed", 1);
                transform.position = Vector2.MoveTowards(transform.position, endposition, currnetSpeed * Time.deltaTime);

                if (EndPoint > transform.position.x)
                    transform.eulerAngles = new Vector3(0, 0, 0);
                else
                    transform.eulerAngles = new Vector3(0, 180, 0);

                remainingDistance = (transform.position - endposition).sqrMagnitude;
                yield return new WaitForFixedUpdate();
            }
            animator.SetFloat("MovingSpeed", 0);
        }
    }
}