using System.Collections;
using UnityEngine;

public class HealthAtroCrossfa : Character
{
    Animator animator;

    private RegdollControllerAtroCrossfa rcac; //RegdollControllerAtroCrossfa �ҷ�����
    TearCrossfa tearCrossfa;

    private Shake shake;

    Coroutine damageAction;

    float hitPoints;
    float armor;
    private float DamageTime;
    private float HitAction;
    public float KnockBackForce;
    public float KnockBackReducer; //�˹������ ��, ����ó��
    public float KnockBackLevelUp;
    private float KnockBackSpeed;
    private float x;
    public float FlameHitTime; //ȭ�����⿡ �¾��� �� �� ���ӽð�

    public int Ricochet;
    private int DeathRandom;
    private int part1Random;
    private int part2Random;
    private int part3Random;
    private int TearCount; //���� ����
    private int TearPartCount; //�������� ���� ���ư��� ����
    private int TearArmPart; //�� ��� �� �ϴ��� ���ư��� ����
    private int DamageType; //������ �ִϸ��̼�

    public GameObject TargetPoint; //���� ������� ���� �� ��ܿ� ǥ�õǴ� UI

    //Ÿ�� �� ����
    public GameObject BeamTaken1;
    public GameObject BeamTaken2;
    public GameObject BeamTaken3;
    public GameObject BeamTaken4;
    public Transform BeamTakenPos;
    int BeamDamageAction; //�� ȿ�� �ޱ�
    float TimeStemp;

    //Ÿ�� �� ����
    public GameObject FlameTakenBody;
    public GameObject FlameTakenLegsTop1;
    public GameObject FlameTakenLegsTop2;
    public GameObject FlameTakenLegsDown1;
    public GameObject FlameTakenLegsDown2;
    public GameObject FlameTakenGun;

    public bool Death = false;
    private bool DeathCount = false;
    public bool Leg1Down = false;
    public bool Leg2Down = false;
    public bool MissileLaunchBaseDown = false;
    public bool MachinegunDown = false;
    public bool TearOn = false; //��ü�� �߷��� ���� ��ȣ
    private bool Leg1DownCut = false;
    private bool Leg2DownCut = false;
    private bool RightLegTop1 = false;
    private bool RightLegTop2 = false;
    private bool RightLegDown1 = false;
    private bool RightLegDown2 = false;
    private bool ImDown = false; //�Ѿ����� ��, ������ �ִϸ��̼� ��Ȱ��ȭ��
    private bool DownMark = false;
    public bool TakeItDown = false;
    private bool KnockBackShot = false; //�˹� ���� ���� ��ȣ

    public Transform BlueExplosionPos;
    public Transform BlueExplosionPos2;
    public Transform BlueExplosionPos3;
    public Transform BlueExplosionPos4;
    public Transform BlueExplosionPos5;
    GameObject BlueExplosion;
    public GameObject Shadow;
    public GameObject HorizonLight1;
    public GameObject HorizonLight2;
    public GameObject HorizonLight3;
    public GameObject AttackLine;
    public GameObject AttackLine2;

    public Transform part1Pos;
    public Transform part2Pos;
    public Transform part3Pos;
    public Transform MLBPos;
    public Transform LeftLegPos;
    public Transform RightLegPos;

    GameObject[] PoolMaker;

    int VoiceRandom;
    int VoicePrint;
    float VoiceTime;

    private int RicochetSoundRandom;
    public AudioClip RicochetSound1;
    public AudioClip RicochetSound2;
    public AudioClip RicochetSound3;
    public AudioClip RicochetSound4;
    public AudioClip RicochetSound5;
    public GameObject RicochetPrefab;
    public Transform RicochetPos;

    public AudioClip Voice1;
    public AudioClip Voice2;
    public AudioClip Voice3;
    public AudioClip Voice4;
    public AudioClip Voice5;

    public AudioClip DeathSound1;
    public AudioClip DeathExplosion1;

    IEnumerator VoiceSound()
    {
        while (true)
        {
            VoiceRandom = Random.Range(0, 20);
            yield return new WaitForSeconds(1);
        }
    }

    public void RandomSound()
    {
        RicochetSoundRandom = Random.Range(0, 5);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay2("Sound", RicochetSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay2("Sound", RicochetSound2);
        else if (RicochetSoundRandom == 2)
            SoundManager.instance.SFXPlay2("Sound", RicochetSound3);
        else if (RicochetSoundRandom == 3)
            SoundManager.instance.SFXPlay2("Sound", RicochetSound4);
        else if (RicochetSoundRandom == 4)
            SoundManager.instance.SFXPlay2("Sound", RicochetSound5);
    }

    void Start()
    {
        StartCoroutine(VoiceSound());
        animator = GetComponent<Animator>();
        rcac = GetComponent<RegdollControllerAtroCrossfa>();
        tearCrossfa = FindObjectOfType<TearCrossfa>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        animator.keepAnimatorStateOnDisable = true;
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;

        TargetPoint.SetActive(true);
        transform.Find("Auto target").gameObject.SetActive(true);
        Shadow.SetActive(true);
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            hitPoints = 1000;
            startingHitPoints = 1000;
            maxHitPoints = 1250;
            armor = 1.3f;
            startingArmor = 1.3f;
            KantakriRicochet = 15;
            Ricochet = 15;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            hitPoints = 1400;
            startingHitPoints = 1400;
            maxHitPoints = 1750;
            armor = 1.72f;
            startingArmor = 1.72f;
            KantakriRicochet = 12;
            Ricochet = 12;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            hitPoints = 1800;
            startingHitPoints = 1800;
            maxHitPoints = 2250;
            armor = 2.08f;
            startingArmor = 2.08f;
            KantakriRicochet = 10;
            Ricochet = 10;
        }

        if (Death == true)
        {
            hitPoints = startingHitPoints;
            armor = startingArmor;

            DeathCount = false;
            Leg1Down = false;
            Leg2Down = false;
            MissileLaunchBaseDown = false;
            TearOn = false;
            Leg1DownCut = false;
            Leg2DownCut = false;
            RightLegTop1 = false;
            RightLegTop2 = false;
            RightLegDown1 = false;
            RightLegDown2 = false;
            ImDown = false;
            DownMark = false;
            TakeItDown = false;

            DeathRandom = 0;
            TearCount = 0;
            TearPartCount = 0;
            TearArmPart = 0;

            rcac.DisableRagdoll();
            GetComponent<BehaviourAtroCrossfa>().AtroCrossfaDeath(false); //���� ���� �ǵ����� ����
            GetComponent<ReverseCallAtroCrossfa>().enabled = true;
            TraceX call1 = transform.Find("Body_1").GetComponent<TraceX>();
            call1.ShadowReset = true;
            Invoke("ReverseOff", 0.1f);

            if (animator.GetBool("Damage1, Atro-Crossfa 390") == true)
                animator.SetBool("Damage1, Atro-Crossfa 390", false);
            if (animator.GetBool("Damage2, Atro-Crossfa 390") == true)
                animator.SetBool("Damage2, Atro-Crossfa 390", false);
            if (animator.GetBool("Damage1(down), Atro-Crossfa 390") == true)
                animator.SetBool("Damage1(down), Atro-Crossfa 390", false);
            if (animator.GetBool("Damage2(down), Atro-Crossfa 390") == true)
                animator.SetBool("Damage2(down), Atro-Crossfa 390", false);
            TargetPoint.SetActive(true);
            transform.Find("Auto target").gameObject.SetActive(true);
            Shadow.SetActive(true);
        }
        ScoreManager.instance.AllEnemyCnt(1);
    }

    private void OnDisable()
    {
        if (animator.GetBool("Death1, Atro-Crossfa 390") == true)
            animator.SetBool("Death1, Atro-Crossfa 390", false);
        if (animator.GetBool("Death1(down), Atro-Crossfa 390") == true)
            animator.SetBool("Death1(down), Atro-Crossfa 390", false);
        if (animator.GetBool("Death2, Atro-Crossfa 390") == true)
            animator.SetBool("Death2, Atro-Crossfa 390", false);
        if (animator.GetBool("Death2(down), Atro-Crossfa 390") == true)
            animator.SetBool("Death2(down), Atro-Crossfa 390", false);
        if (animator.GetBool("MLB out, Atro-Crossfa 390") == true)
            animator.SetBool("MLB out, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun out, Atro-Crossfa 390") == true)
            animator.SetBool("Gun out, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun out, Atro-Crossfa 390") == true)
            animator.SetBool("Death, Atro-Crossfa 390", false);

        transform.Find("Body_1/Electrical spark").gameObject.SetActive(false);
        transform.Find("Body death").gameObject.SetActive(false);
        transform.Find("Body").gameObject.SetActive(true);
        transform.Find("Camera").gameObject.SetActive(true);
        transform.Find("Body plate").gameObject.SetActive(true);
        transform.Find("Body rifle cover").gameObject.SetActive(true);
        transform.Find("Body rifle1").gameObject.SetActive(true);
        transform.Find("Body rifle2").gameObject.SetActive(true);
        transform.Find("Body rifle3").gameObject.SetActive(true);
        transform.Find("Eye center").gameObject.SetActive(true);
        transform.Find("Eye1-1").gameObject.SetActive(true);
        transform.Find("Eye2-1").gameObject.SetActive(true);
        transform.Find("Eye3").gameObject.SetActive(true);
        transform.Find("Eye4").gameObject.SetActive(true);
        transform.Find("Body_1/NarSyr-Haicross 13 missile").gameObject.SetActive(true);

        if (Leg1Down == true)
        {
            transform.Find("Right leg top").gameObject.SetActive(true);
            transform.Find("Right leg top knee").gameObject.SetActive(true);
            transform.Find("Right leg down1").gameObject.SetActive(true);
            transform.Find("Right leg down2").gameObject.SetActive(true);
            transform.Find("Right leg down foot").gameObject.SetActive(true);
        }

        if (Leg2Down == true)
        {
            transform.Find("Left leg top").gameObject.SetActive(true);
            transform.Find("Left leg top knee").gameObject.SetActive(true);
            transform.Find("Left leg down1").gameObject.SetActive(true);
            transform.Find("Left leg down2").gameObject.SetActive(true);
            transform.Find("Left leg down foot").gameObject.SetActive(true);
        }
    }

    //�뽬 ���� ���ݿ� ���� �ǰ�
    public void Hurt(Vector2 pos)
    {
        x = transform.position.x - pos.x;
        if (x < 0)
            x = 1;
        else
            x = -1;

        KnockBackSpeed = KnockBackForce;
        KnockBackShot = true;
        Invoke("StopKnockBack", 1);
    }

    void StopKnockBack()
    {
        KnockBackShot = false;
    }

    void ReverseOff()
    {
        GetComponent<ReverseCallAtroCrossfa>().enabled = false;
        Death = false;
    }

    public void AtroCrossfaLeg1Down(bool Down)
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

    public void AtroCrossfaLeg2Down(bool Down)
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

    public void AtroCrossfaImDown(bool Down)
    {
        if (Down == true)
        {
            ImDown = true;
        }
        else
        {
            ImDown = false;
        }
    }

    public void AtroCrossfaDownMark(bool Down)
    {
        if (Down == true)
        {
            DownMark = true;
        }
        else
        {
            DownMark = false;
        }
    }

    //��ź ��ġ �ޱ�
    public void RicochetNum(int num)
    {
        Ricochet = num;
    }

    //�� ������ �ޱ�
    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    //�� ����Ʈ ����
    public void FlameBody()
    {
        FlameTakenBody.SetActive(true);
        animator.SetBool("Flame body, Atro-Crossfa 390", true);
        Invoke("TurnOffFlameBody", FlameHitTime);
    }
    public void FlameLegs()
    {
        if (tearCrossfa.RightLegTop1 == false)
            FlameTakenLegsTop1.SetActive(true);
        if (tearCrossfa.RightLegTop2 == false)
            FlameTakenLegsTop2.SetActive(true);
        if (tearCrossfa.RightLegTop1 == false && tearCrossfa.RightLegDown1 == false)
            FlameTakenLegsDown1.SetActive(true);
        if (tearCrossfa.RightLegTop2 == false && tearCrossfa.RightLegDown2 == false)
            FlameTakenLegsDown2.SetActive(true);
        animator.SetBool("Flame legs, Atro-Crossfa 390", true);
        Invoke("TurnOffFlameLegs", FlameHitTime);
    }
    public void FlameGun()
    {
        if (MachinegunDown == false)
            FlameTakenGun.SetActive(true);
        animator.SetBool("Flame gun, Atro-Crossfa 390", true);
        Invoke("TurnOffFlameGun", FlameHitTime);
    }
    void TurnOffFlameBody()
    {
        animator.SetBool("Flame body, Atro-Crossfa 390", false);
    }
    void TurnOffFlameLegs()
    {
        animator.SetBool("Flame legs, Atro-Crossfa 390", false);
    }
    void TurnOffFlameGun()
    {
        animator.SetBool("Flame gun, Atro-Crossfa 390", false);
    }

    //ȭ�� ���� ������ ����
    public void FlameDamgeStart(int damage, float interval)
    {
        StartCoroutine(FireDamageCharacter(damage, interval));
    }

    void Update()
    {
        if (KnockBackShot == true)
        {
            transform.Translate(Vector2.right * KnockBackSpeed * Time.deltaTime);

            if (KnockBackSpeed > 0)
                KnockBackSpeed -= KnockBackReducer;
            else
                KnockBackSpeed = 0;
        }

        TimeStemp -= Time.deltaTime;

        if (TimeStemp < 0)
        {
            TimeStemp = 0;
            BeamDamageAction = 0;
        }

        if (HitAction >= 0)
            HitAction -= Time.deltaTime;

        if (FlameHitTime >= 0)
            FlameHitTime -= Time.deltaTime;

        if (tearCrossfa.RightLegTop1 == true)
        {
            FlameTakenLegsTop1.SetActive(false);
            FlameTakenLegsDown1.SetActive(false);
        }
        if (tearCrossfa.RightLegTop2 == true)
        {
            FlameTakenLegsTop2.SetActive(false);
            FlameTakenLegsDown2.SetActive(false);
        }
        if (tearCrossfa.RightLegDown1 == true)
            FlameTakenLegsDown1.SetActive(false);
        if (tearCrossfa.RightLegDown2 == true)
            FlameTakenLegsDown2.SetActive(false);

        if (MachinegunDown == true)
            FlameTakenGun.SetActive(false);

        if (TearOn == true)
        {
            if (ImDown == false)
            {
                TearOn = false;
                damageAction = StartCoroutine(DamageAction());
            }
            if (DownMark == true)
            {
                TearOn = false;
                damageAction = StartCoroutine(DamageAction2());
            }
        }

        if (Death == false)
        {
            if (VoiceRandom == 0)
            {
                VoicePrint = Random.Range(0, 5);

                if (VoicePrint == 0)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay2("Sound", Voice1);
                    }
                }
                else if (VoicePrint == 1)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay2("Sound", Voice2);
                    }
                }
                else if (VoicePrint == 2)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay2("Sound", Voice3);
                    }
                }
                else if (VoicePrint == 3)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay2("Sound", Voice4);
                    }
                }
                else if (VoicePrint == 4)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay2("Sound", Voice5);
                    }
                }
            }
            else
            {
                VoiceTime = 0;
            }
        }

        if (ImDown == true)
        {
            if (damageAction != null)
                StopCoroutine(damageAction);
            animator.SetBool("Damage1, Atro-Crossfa 390", false);
            animator.SetBool("Damage2, Atro-Crossfa 390", false);
            animator.SetBool("Damage1(down), Atro-Crossfa 390", false);
            animator.SetBool("Damage2(down), Atro-Crossfa 390", false);
        }
    }

    //���� Ÿ���� �޾��� ���� ������ ����
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if (DeathCount == false)
        {
            while (true)
            {
                if (Ricochet != 0)
                {
                    hitPoints = hitPoints - (damage / armor);
                    StartCoroutine(BeamAction());
                    TimeStemp += 0.025f;

                    if (HitAction <= 0)
                    {
                        if(TakeItDown == false)
                        {
                            if (ImDown == false)
                            {
                                HitAction = 1.116f;
                                DamageTime = 1.116f;
                                damageAction = StartCoroutine(DamageAction());
                            }
                            if (DownMark == true)
                            {
                                HitAction = 1.116f;
                                DamageTime = 1.116f;
                                damageAction = StartCoroutine(DamageAction2());
                            }
                        }
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        DeathCount = true;
                        TargetPoint.SetActive(false);
                        transform.Find("Auto target").gameObject.SetActive(false);
                        Shadow.SetActive(false);
                        if (HorizonLight1.activeSelf == true)
                            HorizonLight1.SetActive(false);
                        if (HorizonLight2.activeSelf == true)
                            HorizonLight2.SetActive(false);
                        if (HorizonLight3.activeSelf == true)
                            HorizonLight3.SetActive(false);
                        AttackLine.SetActive(false);
                        AttackLine2.SetActive(false);
                        FlameTakenBody.SetActive(false);
                        FlameTakenLegsTop1.SetActive(false);
                        FlameTakenLegsTop2.SetActive(false);
                        FlameTakenLegsDown1.SetActive(false);
                        FlameTakenLegsDown2.SetActive(false);
                        FlameTakenGun.SetActive(false);
                        DeathAction();
                        ScoreManager.instance.setScore(1);
                        ScoreManager.instance.set_Kantakri_Score(1);
                        ScoreManager.instance.KantakriEliteCnt++;
                        ScoreManager.instance.DieCnt(1);
                        ScoreManager.instance.EnemyList.Remove(gameObject);
                        Shake.Instance.ShakeCamera(4, 0.1f);
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
                else if (Ricochet == 0)
                {
                    //Debug.Log("��!");
                    GameObject Ricochet1 = Instantiate(RicochetPrefab, RicochetPos.transform.position, RicochetPos.transform.rotation);
                    RandomSound();
                    break;
                }
            }
        }
    }

    //���� ���� �پ��� ���� ������ ����
    IEnumerator FireDamageCharacter(int damage, float interval)
    {
        if (DeathCount == false)
        {
            while (FlameHitTime > 0)
            {
                StartCoroutine(DamageCharacter(damage, 0));
                yield return new WaitForSeconds(interval);
            }
        }
    }

    public IEnumerator DamageAction()
    {
        DamageType = Random.Range(1, 3); //1~2������ ���ڸ� ���

        animator.SetBool("Damage1, Atro-Crossfa 390", false);
        animator.SetBool("Damage2, Atro-Crossfa 390", false);

        if (DamageType == 1)
        {
            animator.SetBool("Damage1, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.75f);
            animator.SetBool("Damage1, Atro-Crossfa 390", false);
        }
        else
        {
            animator.SetBool("Damage2, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.75f);
            animator.SetBool("Damage2, Atro-Crossfa 390", false);
        }
    }

    public IEnumerator DamageAction2()
    {
        DamageType = Random.Range(1, 3);

        animator.SetBool("Damage1(down), Atro-Crossfa 390", false);
        animator.SetBool("Damage2(down), Atro-Crossfa 390", false);

        if (DamageType == 1)
        {
            animator.SetBool("Damage1(down), Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.75f);
            animator.SetBool("Damage1(down), Atro-Crossfa 390", false);
        }
        else if (DamageType == 2)
        {
            animator.SetBool("Damage2(down), Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.75f);
            animator.SetBool("Damage2(down), Atro-Crossfa 390", false);
        }
    }

    //�� ���� �޾��� ���� �ð�ȿ�� �߻�
    public IEnumerator BeamAction()
    {
        //Debug.Log(TimeStemp);
        if (BeamDamageAction == 1)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam1 = Instantiate(BeamTaken1, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 2)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam2 = Instantiate(BeamTaken2, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 3)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam3 = Instantiate(BeamTaken3, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 4)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam4 = Instantiate(BeamTaken4, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    //���� �ִϸ��̼�
    void DeathAction()
    {
        DeathRandom = Random.Range(0, 3);

        if (DeathRandom == 0)
        {
            //StopAnimationBeforeDeath();
            DeathPeace();
            DeathProcess();
            Invoke("Kill", 5f);
            //Debug.Log("Death0");
        }
        else if (DeathRandom == 1)
        {
            //StopAnimationBeforeDeath();
            DontShoot();
            StartCoroutine(Death1());
        }
        else if (DeathRandom == 2)
        {
            //StopAnimationBeforeDeath();
            DontShoot();
            StartCoroutine(Death2());
        }
    }

    IEnumerator Death1()
    {
        if (Leg1Down == false && Leg2Down == false)
        {
            SoundManager.instance.SFXPlay2("Sound", DeathExplosion1);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos.position;
            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
            animator.SetBool("Death2, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.416f);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos2.position;
            BlueExplosion.transform.rotation = BlueExplosionPos2.rotation;
            yield return new WaitForSeconds(0.75f);
            DeathPeace();
            DeathProcess();
            Invoke("Kill", 5f);
            //Debug.Log("Death1");
        }
        else if (Leg1Down == true || Leg2Down == true)
        {
            SoundManager.instance.SFXPlay2("Sound", DeathExplosion1);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos.position;
            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
            animator.SetBool("Death2(down), Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.416f);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos2.position;
            BlueExplosion.transform.rotation = BlueExplosionPos2.rotation;
            yield return new WaitForSeconds(0.75f);
            DeathPeace();
            DeathProcess();
            Invoke("Kill", 5f);
            //Debug.Log("Death1 no leg");
        }
    }

    IEnumerator Death2()
    {
        if (Leg1Down == false && Leg2Down == false)
        {
            animator.SetBool("Death1, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.916f);
            DeathPeace();
            DeathProcess();
            Invoke("Kill", 5f);
            //Debug.Log("Death2");
        }
        else if (Leg1Down == true || Leg2Down == true)
        {
            animator.SetBool("Death1(down), Atro-Crossfa 390", true);
            yield return new WaitForSeconds(0.916f);
            DeathPeace();
            DeathProcess();
            Invoke("Kill", 5f);
            //Debug.Log("Death2 no leg");
        }
    }

    //���� �� �ʿ��� ó������
    void DeathProcess()
    {
        Death = true;
        rcac.ActiveRagdoll(); //RegdollControllerAtroCrossfa ��ũ��Ʈ�� ActiveRagdoll �޼ҵ� ȣ��
        GetComponent<BehaviourAtroCrossfa>().AtroCrossfaDeath(true); //���� ���� ����
        GetComponent<DeathCallAtroCrossfa>().enabled = true; //���׵� ��� ��ü ��Ʈ���� ���� ��ȣ ����
        DeathRolling call = transform.Find("Body_1").GetComponent<DeathRolling>(); //���� �� ��ü ȸ��
        call.enabled = true;
        TraceX call1 = transform.Find("Body_1").GetComponent<TraceX>();
        call1.DeathTransformTime = true; //�׾��� ���� ��ġ ��ǥ ��� ����ġ
    }

    //���� �� ��ݿ��� ����
    void DontShoot()
    {
        Transform damageCarrierInfector1 = transform.Find("Body_1"); //�ڽ� ������Ʈ ��ġ ã��
        Transform damageCarrierInfector2 = transform.Find("Body_1/Missile launch base1");
        Transform damageCarrierInfector3 = transform.Find("Leg damage");
        damageCarrierInfector1.GetComponent<BoxCollider2D>().isTrigger = true; //��ݿ��� �����ϵ��� Ʈ���� ��Ȱ��ȭ
        damageCarrierInfector1.GetComponent<CapsuleCollider2D>().enabled = false; //��ݿ��� �����ϵ��� �ݶ��̴� ����
        damageCarrierInfector2.GetComponent<BoxCollider2D>().enabled = false;
        damageCarrierInfector3.GetComponent<BoxCollider2D>().enabled = false;
        damageCarrierInfector3.GetComponent<CapsuleCollider2D>().enabled = false;
        damageCarrierInfector1.gameObject.layer = 0;
        damageCarrierInfector2.gameObject.layer = 0;
        damageCarrierInfector3.gameObject.layer = 0;
    }

    void Kill()
    {
        rcac.DisableRagdoll();
        GetComponent<BehaviourAtroCrossfa>().AtroCrossfaDeath(false); //���� ���� �ǵ����� ����
        GetComponent<DeathCallAtroCrossfa>().layerTime = 0;
        GetComponent<DeathCallAtroCrossfa>().enabled = false;
        KillCharacter();
    }

    //�̻��� ����ȭ
    public void PartDeathMLB()
    {
        if (MissileLaunchBaseDown == true)
        {
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos3.position;
            BlueExplosion.transform.rotation = BlueExplosionPos3.rotation;
            animator.SetBool("MLB out, Atro-Crossfa 390", true);
        }
    }

    //����� ����ȭ
    public void PartDeathMachinegun()
    {
        if (MachinegunDown == true)
        {
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos4.position;
            BlueExplosion.transform.rotation = BlueExplosionPos4.rotation;
            animator.SetBool("Gun out, Atro-Crossfa 390", true);
        }
    }

    //�ٵ� ���� ó��
    void deathBodyCreation()
    {
        GameObject Explosion = SingletonObject.instance.Loader("AtroCrossfa390Explosion");
        Explosion.transform.position = BlueExplosionPos5.position;
        Explosion.transform.rotation = BlueExplosionPos5.rotation;
        transform.Find("Body_1/Electrical spark").gameObject.SetActive(true);

        transform.Find("Body death").gameObject.SetActive(true);
        transform.Find("Body").gameObject.SetActive(false);
        transform.Find("Camera").gameObject.SetActive(false);
        transform.Find("Body plate").gameObject.SetActive(false);
        transform.Find("Body rifle cover").gameObject.SetActive(false);
        transform.Find("Body rifle1").gameObject.SetActive(false);
        transform.Find("Body rifle1 fade").gameObject.SetActive(false);
        transform.Find("Body rifle2").gameObject.SetActive(false);
        transform.Find("Body rifle2 fade").gameObject.SetActive(false);
        transform.Find("Body rifle3").gameObject.SetActive(false);
        transform.Find("Body rifle3 fade").gameObject.SetActive(false);
        transform.Find("Eye center").gameObject.SetActive(false);
        transform.Find("Eye1-1").gameObject.SetActive(false);
        transform.Find("Eye2-1").gameObject.SetActive(false);
        transform.Find("Eye3").gameObject.SetActive(false);
        transform.Find("Eye4").gameObject.SetActive(false);
        transform.Find("Launch pod").gameObject.SetActive(false);
        transform.Find("Body_1/NarSyr-Haicross 13 missile").gameObject.SetActive(false);
        transform.Find("Body_1/Eye center/Eye fire light").gameObject.SetActive(false);
        transform.Find("Body_1/Gun firing").gameObject.SetActive(false);
        transform.Find("Body_1/Left leg/Boost").gameObject.SetActive(false);
        transform.Find("Body_1/Right leg top_1/Boost").gameObject.SetActive(false);
    }

    //������Ʈ ����
    void DeathPeace()
    {
        SoundManager.instance.SFXPlay4("Sound", DeathSound1);
        deathBodyCreation();

        if (Leg1Down == false)
        {
            TearArmPart = Random.Range(1, 3);

            if (TearArmPart == 1 && RightLegTop1 == false)
            {
                transform.Find("Right leg top").gameObject.SetActive(false); //�ٸ� ��� �߸�
                transform.Find("Right leg top knee").gameObject.SetActive(false);
                transform.Find("Right leg down1").gameObject.SetActive(false);
                transform.Find("Right leg down2").gameObject.SetActive(false);
                transform.Find("Right leg down foot").gameObject.SetActive(false);

                if (Leg1DownCut == false)
                {
                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390RightLeg");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
                else
                {
                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390RightLegTop");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
            }
            else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
            {
                transform.Find("Right leg down1").gameObject.SetActive(false); //�ٸ� �ϴ� �߸�
                transform.Find("Right leg down2").gameObject.SetActive(false);
                transform.Find("Right leg down foot").gameObject.SetActive(false);

                GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390RightLegdown");
                RightLeg.transform.position = RightLegPos.position;
                RightLeg.transform.rotation = RightLegPos.rotation;
                DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                RightLegpos.Pos = RightLeg.transform.position.y;
            }
            Leg1Down = true;
        }
        else
        {
            if (tearCrossfa.RightLegTop1 == false && tearCrossfa.RightLegDown1 == true)
            {
                TearArmPart = Random.Range(1, 3);

                if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                {
                    transform.Find("Right leg top").gameObject.SetActive(false); //�ٸ� ��� �߸�
                    transform.Find("Right leg top knee").gameObject.SetActive(false);

                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390RightLegTop");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
            }
        }

        if (Leg2Down == false)
        {
            TearArmPart = Random.Range(1, 3);

            if (TearArmPart == 1 && RightLegTop2 == false)
            {
                transform.Find("Left leg top").gameObject.SetActive(false); //�ٸ� ��� �߸�
                transform.Find("Left leg top knee").gameObject.SetActive(false);
                transform.Find("Left leg down1").gameObject.SetActive(false);
                transform.Find("Left leg down2").gameObject.SetActive(false);
                transform.Find("Left leg down foot").gameObject.SetActive(false);

                if (Leg2DownCut == false)
                {
                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390LeftLeg");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
                else
                {
                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390LeftLegTop");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
            }
            else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
            {
                transform.Find("Left leg down1").gameObject.SetActive(false); //�ٸ� �ϴ� �߸�
                transform.Find("Left leg down2").gameObject.SetActive(false);
                transform.Find("Left leg down foot").gameObject.SetActive(false);

                GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390LeftLegdown");
                RightLeg.transform.position = RightLegPos.position;
                RightLeg.transform.rotation = RightLegPos.rotation;
                DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                RightLegpos.Pos = RightLeg.transform.position.y;
            }
            Leg2Down = true;
        }
        else
        {
            if (tearCrossfa.RightLegTop2 == false && tearCrossfa.RightLegDown2 == true)
            {
                TearArmPart = Random.Range(1, 3);

                if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                {
                    transform.Find("Left leg top").gameObject.SetActive(false); //�ٸ� ��� �߸�
                    transform.Find("Left leg top knee").gameObject.SetActive(false);

                    GameObject RightLeg = SingletonObject.instance.Loader("AtroCrossfa390LeftLegTop");
                    RightLeg.transform.position = RightLegPos.position;
                    RightLeg.transform.rotation = RightLegPos.rotation;
                    DeathTaikaLaiThrotro1 RightLegpos = RightLeg.GetComponent<DeathTaikaLaiThrotro1>();
                    RightLegpos.Pos = RightLeg.transform.position.y;
                }
            }
        }

        if (MissileLaunchBaseDown == false)
        {
            animator.SetBool("MLB out, Atro-Crossfa 390", true);
        }

        part1Random = Random.Range(2, 4);
        for (int i = 1; i <= part1Random; i++)
        {
            GameObject Part1 = SingletonObject.instance.Loader("Taika_part1");
            Part1.transform.position = part1Pos.position;
            Part1.transform.rotation = part1Pos.rotation;
            DeathTaikaLaiThrotro1 Part1pos = Part1.GetComponent<DeathTaikaLaiThrotro1>();
            Part1pos.Pos = Part1.transform.position.y;
        }
        part2Random = Random.Range(4, 6);
        for (int i = 1; i <= part2Random; i++)
        {
            GameObject Part2 = SingletonObject.instance.Loader("Taika_part2");
            Part2.transform.position = part2Pos.position;
            Part2.transform.rotation = part2Pos.rotation;
            DeathTaikaLaiThrotro1 Part2pos = Part2.GetComponent<DeathTaikaLaiThrotro1>();
            Part2pos.Pos = Part2.transform.position.y;
        }
        part3Random = Random.Range(6, 8);
        for (int i = 1; i <= part3Random; i++)
        {
            GameObject Part3 = SingletonObject.instance.Loader("Taika_part3");
            Part3.transform.position = part3Pos.position;
            Part3.transform.rotation = part3Pos.rotation;
            DeathTaikaLaiThrotro1 Part3pos = Part3.GetComponent<DeathTaikaLaiThrotro1>();
            Part3pos.Pos = Part3.transform.position.y;
        }
    }

    void StopAnimationBeforeDeath()
    {
        if (animator.GetBool("Movement, Atro-Crossfa 390") == true)
            animator.SetBool("Movement, Atro-Crossfa 390", false);
        if (animator.GetBool("Eye(Missile ready Color), Atro-Crossfa 390") == true)
            animator.SetBool("Eye(Missile ready Color), Atro-Crossfa 390", false);
        if (animator.GetBool("Missile ready, Atro-Crossfa 390") == true)
            animator.SetBool("Missile ready, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile ready state, Atro-Crossfa 390") == true)
            animator.SetBool("Missile ready state, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile Charge, Atro-Crossfa 390") == true)
            animator.SetBool("Missile Charge, Atro-Crossfa 390", false);
        if (animator.GetBool("Eye(Charge Color), Atro-Crossfa 390") == true)
            animator.SetBool("Eye(Charge Color), Atro-Crossfa 390", false);
        if (animator.GetBool("Missile fire, Atro-Crossfa 390") == true)
            animator.SetBool("Missile fire, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile off, Atro-Crossfa 390") == true)
            animator.SetBool("Missile off, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun rolling1, Atro-Crossfa 390") == true)
            animator.SetBool("Gun rolling1, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun rolling2, Atro-Crossfa 390") == true)
            animator.SetBool("Gun rolling2, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion front, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion front, Atro-Crossfa 390, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion back, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion back, Atro-Crossfa 390, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion Boost, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion Boost, Atro-Crossfa 390, Atro-Crossfa 390", false);

        if (animator.GetFloat("Moving Speed, Atro-Crossfa 390") != 0)
            animator.SetFloat("Moving Speed, Atro-Crossfa 390", 0);
        if (animator.GetFloat("Gun rolling start, Atro-Crossfa 390") != 0)
            animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Delete Area"))
        {
            DeathCount = true;
            Death = true;
            TargetPoint.SetActive(false);
            transform.Find("Auto target").gameObject.SetActive(false);
            Shadow.SetActive(false);
            if (HorizonLight1.activeSelf == true)
                HorizonLight1.SetActive(false);
            if (HorizonLight2.activeSelf == true)
                HorizonLight2.SetActive(false);
            if (HorizonLight3.activeSelf == true)
                HorizonLight3.SetActive(false);
            AttackLine.SetActive(false);
            AttackLine2.SetActive(false);
            ScoreManager.instance.DieCnt(1);
            ScoreManager.instance.EnemyList.Remove(gameObject);
            this.gameObject.SetActive(false);
        }
    }
}