using System.Collections;
using UnityEngine;

public class KaotiJaios4Dual : Character
{
    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios4;

    Animator animator;

    Coroutine damageAction;

    public int Number; //ī��Ƽ ���̿��� ����
    public string DeathBodyName;
    public string DeathGunFrontName;
    public string DeathGunBackName;

    public float hitPoints;
    float armor;
    public int Ricochet;
    int DeathRandom;

    int VoiceRandom;
    int VoicePrint;
    float VoiceTime;
    public float FlameHitTime; //ȭ�����⿡ �¾��� �� �� ���ӽð�
    private float HitAction;
    public float KnockBackForce;
    public float KnockBackReducer; //�˹������ ��, ����ó��
    public float KnockBackLevelUp;
    private float KnockBackSpeed;
    private float x;
    // float deathTime = 0.5f;

    public bool WheelDown = false; //���� ü���� 0�� �Ǹ� �۵��Ǵ� ����ġ
    public bool Gun1Down = false; //1�� ���� ���ư��� �۵��Ǵ� ����ġ
    public bool Gun2Down = false; //2�� ���� ���ư��� �۵��Ǵ� ����ġ
    bool DeathCount = false;
    public bool TearOn = false; //��ü�� �߷��� ���� ��ȣ
    private bool KnockBackShot = false; //�˹� ���� ���� ��ȣ

    private float DamageTime;

    public Transform GunExplosionPos;
    GameObject GunExplosion;
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
    public GameObject FlameTakenWheel;
    public GameObject FlameTakenGun;

    public Transform gunBackPos;
    public Transform gunFrontPos;
    public Transform gunFrontBoxPos;
    public Transform gunFrontCircleBoxPos;
    public Transform gunFrontJointPos;
    public Transform gunFrontJointArmPos;
    public Transform tireBackPos;
    public Transform tireFrontPos;
    public Transform bodyPos;
    public Transform part1Pos;
    public Transform part2Pos;
    public Transform part3Pos;

    int part1Random;
    int part2Random;
    int part3Random;

    GameObject Body;
    GameObject GunBack;
    GameObject GunFront;
    GameObject GunFrontBox;
    GameObject GunFrontCircleBox;
    GameObject GunFrontJoint;
    GameObject GunFrontJointArm;
    GameObject TireBack;
    GameObject TireFront;

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
            SoundManager.instance.SFXPlay("Sound", RicochetSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay("Sound", RicochetSound2);
        else if (RicochetSoundRandom == 2)
            SoundManager.instance.SFXPlay("Sound", RicochetSound3);
        else if (RicochetSoundRandom == 3)
            SoundManager.instance.SFXPlay("Sound", RicochetSound4);
        else if (RicochetSoundRandom == 4)
            SoundManager.instance.SFXPlay("Sound", RicochetSound5);
    }

    public void Start()
    {
        StartCoroutine(VoiceSound());
        animator = GetComponent<Animator>();
        DualBehaviourKaotiJaios4 = FindObjectOfType<DualBehaviourKaotiJaios5_>();
        animator.keepAnimatorStateOnDisable = true;
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            if (Number == 1) //ī��Ƽ-���̿��� 4(���)
            {
                hitPoints = 300;
                startingHitPoints = 300;
                maxHitPoints = 375;
                armor = 1;
                startingArmor = 1;
                KantakriRicochet = 20;
                Ricochet = 20;
            }
            else if (Number == 2) //ī��Ƽ-���̿��� 4(Armor ���)
            {
                hitPoints = 500;
                startingHitPoints = 500;
                maxHitPoints = 600;
                armor = 1.1f;
                startingArmor = 1.1f;
                KantakriRicochet = 10;
                Ricochet = 10;
            }
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            if (Number == 1)
            {
                hitPoints = 420;
                startingHitPoints = 420;
                maxHitPoints = 525;
                armor = 1.3f;
                startingArmor = 1.3f;
                KantakriRicochet = 16;
                Ricochet = 16;
            }
            else if (Number == 2)
            {
                hitPoints = 700;
                startingHitPoints = 700;
                maxHitPoints = 840;
                armor = 1.44f;
                startingArmor = 1.44f;
                KantakriRicochet = 8;
                Ricochet = 8;
            }
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            if (Number == 1)
            {
                hitPoints = 540;
                startingHitPoints = 540;
                maxHitPoints = 675;
                armor = 1.5f;
                startingArmor = 1.5f;
                KantakriRicochet = 14;
                Ricochet = 14;
            }
            else if (Number == 2)
            {
                hitPoints = 900;
                startingHitPoints = 900;
                maxHitPoints = 1080;
                armor = 1.76f;
                startingArmor = 1.76f;
                KantakriRicochet = 7;
                Ricochet = 7;
            }
        }

        TearOn = false;
        WheelDown = false;
        Gun1Down = false;
        Gun2Down = false;
        DeathCount = false;
        DeathRandom = 0;
        TargetPoint.SetActive(true);
        SingletonObject.instance.KaotiEnemyBody.Add(bodyPos);
        transform.Find("Auto target").gameObject.SetActive(true);
        ScoreManager.instance.AllEnemyCnt(1);
    }

    private void OnDisable()
    {
        SingletonObject.instance.KaotiEnemyBody.Remove(bodyPos);
        if (animator.GetBool("Kaoti-Jaios 4 Death1") == true)
            animator.SetBool("Kaoti-Jaios 4 Death1", false);
        if (animator.GetBool("Kaoti-Jaios 4 Death1 down") == true)
            animator.SetBool("Kaoti-Jaios 4 Death1 down", false);
        if (animator.GetBool("Kaoti-Jaios 4 Death2") == true)
            animator.SetBool("Kaoti-Jaios 4 Death2", false);
        if (animator.GetBool("Kaoti-Jaios 4 Death2 down") == true)
            animator.SetBool("Kaoti-Jaios 4 Death2 down", false);
        if (animator.GetBool("Kaoti-Jaios 4 Taking damage1") == true)
            animator.SetBool("Kaoti-Jaios 4 Taking damage1", false);
        if (animator.GetBool("Kaoti-Jaios 4 Taking damage2") == true)
            animator.SetBool("Kaoti-Jaios 4 Taking damage2", false);
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
        animator.SetBool("Kaoti-Jaios 4 FlameBody", true);
        Invoke("TurnOffFlameBody", FlameHitTime);
    }
    public void FlameWheel()
    {
        if (WheelDown == false)
            FlameTakenWheel.SetActive(true);
        animator.SetBool("Kaoti-Jaios 4 FlameWheel", true);
        Invoke("TurnOffFlameWheel", FlameHitTime);
    }
    public void FlameGun()
    {
        if (Gun1Down == false || Gun2Down == false)
            FlameTakenGun.SetActive(true);
        animator.SetBool("Kaoti-Jaios 4 FlameGun", true);
        Invoke("TurnOffFlameGun", FlameHitTime);
    }
    void TurnOffFlameBody()
    {
        animator.SetBool("Kaoti-Jaios 4 FlameBody", false);
    }
    void TurnOffFlameWheel()
    {
        animator.SetBool("Kaoti-Jaios 4 FlameWheel", false);
    }
    void TurnOffFlameGun()
    {
        animator.SetBool("Kaoti-Jaios 4 FlameGun", false);
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
            BeamDamageAction = 0; //������ ���⿡ Ÿ�ݹ��� ����, �ٸ� ���� Ÿ���� �޾��� �� ������ ���� ȿ���� ��Ÿ���� �ʵ��� �ϱ� ���� ����
        }

        if (HitAction >= 0)
            HitAction -= Time.deltaTime;

        if (FlameHitTime >= 0)
            FlameHitTime -= Time.deltaTime;

        if (WheelDown == true)
            FlameTakenWheel.SetActive(false);
        if (Gun1Down == true && Gun2Down == true)
            FlameTakenGun.SetActive(false);

        if (TearOn == true)
        {
            TearOn = false;
            damageAction = StartCoroutine(DamageAction());
        }

        if (VoiceRandom == 0)
        {
            VoicePrint = Random.Range(0, 5);

            if (VoicePrint == 0)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay("Sound", Voice1);
                }
            }
            else if (VoicePrint == 1)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay("Sound", Voice2);
                }
            }
            else if (VoicePrint == 2)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay("Sound", Voice3);
                }
            }
            else if (VoicePrint == 3)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay("Sound", Voice4);
                }
            }
            else if (VoicePrint == 4)
            {
                if (VoiceTime == 0)
                {
                    VoiceTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay("Sound", Voice5);
                }
            }
        }
        else
        {
            VoiceTime = 0;
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
                        HitAction = 0.75f;
                        DamageTime = 0.75f;
                        damageAction = StartCoroutine(DamageAction());
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        DeathCount = true;
                        TargetPoint.SetActive(false);
                        transform.Find("Auto target").gameObject.SetActive(false);
                        FlameTakenBody.SetActive(false);
                        FlameTakenWheel.SetActive(false);
                        FlameTakenGun.SetActive(false);
                        DeathAction();
                        ScoreManager.instance.setScore(1);
                        ScoreManager.instance.set_Kantakri_Score(1);
                        ScoreManager.instance.DieCnt(1);
                        ScoreManager.instance.EnemyList.Remove(gameObject);
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
        int damageAction;

        damageAction = Random.Range(1, 3); //1~2������ ���ڸ� ���

        animator.SetBool("Kaoti-Jaios 4 Taking damage1", false);
        animator.SetBool("Kaoti-Jaios 4 Taking damage2", false);

        if (damageAction == 1)
        {
            animator.SetBool("Kaoti-Jaios 4 Taking damage1", true);
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Kaoti-Jaios 4 Taking damage1", false);
        }
        else
        {
            animator.SetBool("Kaoti-Jaios 4 Taking damage2", true);
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Kaoti-Jaios 4 Taking damage2", false);
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
            animator.SetBool("Kaoti-Jaios 4 Firing", false);
            animator.SetBool("Kaoti-Jaios 4 Firing2", false);
            Death();
            KillCharacter();
        }
        else if (DeathRandom == 1)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing", false);
            animator.SetBool("Kaoti-Jaios 4 Firing2", false);
            StartCoroutine(Death1());
        }
        else if (DeathRandom == 2)
        {
            animator.SetBool("Kaoti-Jaios 4 Firing", false);
            animator.SetBool("Kaoti-Jaios 4 Firing2", false);
            StartCoroutine(Death2());
        }
    }

    IEnumerator Death1()
    {
        if (WheelDown == false)
        {
            GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
            GunExplosion.transform.position = GunExplosionPos.position;
            GunExplosion.transform.rotation = GunExplosionPos.rotation;
            animator.SetBool("Kaoti-Jaios 4 Death1", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
        else
        {
            GunExplosion = SingletonObject.instance.Loader("KantakriBlackSmallExplosion1");
            GunExplosion.transform.position = GunExplosionPos.position;
            GunExplosion.transform.rotation = GunExplosionPos.rotation;
            animator.SetBool("Kaoti-Jaios 4 Death1 down", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
    }

    IEnumerator Death2()
    {
        if (WheelDown == false)
        {
            animator.SetBool("Kaoti-Jaios 4 Death2", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
        else
        {
            animator.SetBool("Kaoti-Jaios 4 Death2 down", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
    }

    //�� �� ���� ����ȭ
    public void PartDeathWheel()
    {
        if (WheelDown == true)
        {
            TireBack = SingletonObject.instance.Loader("tireBack");
            TireBack.transform.position = tireBackPos.position;
            TireBack.transform.rotation = tireBackPos.rotation;
            DeathKaotiJaios4 TireBackpos = TireBack.GetComponent<DeathKaotiJaios4>();
            TireBackpos.Pos = TireBack.transform.position.y;

            TireFront = SingletonObject.instance.Loader("tireFront");
            TireFront.transform.position = tireFrontPos.position;
            TireFront.transform.rotation = tireFrontPos.rotation;
            DeathKaotiJaios4 TireFrontpos = TireFront.GetComponent<DeathKaotiJaios4>();
            TireFrontpos.Pos = TireFront.transform.position.y;
        }
    }
    public void PartDeathGun1()
    {
        if (Gun1Down == true)
        {
            GunFront = SingletonObject.instance.Loader(DeathGunFrontName);
            GunFront.transform.position = gunFrontPos.position;
            GunFront.transform.rotation = gunFrontPos.rotation;
            DeathKaotiJaios4 GunFrontpos = GunFront.GetComponent<DeathKaotiJaios4>();
            GunFrontpos.Pos = GunFront.transform.position.y;

            GunFrontBox = SingletonObject.instance.Loader("gunFrontBox");
            GunFrontBox.transform.position = gunFrontBoxPos.position;
            GunFrontBox.transform.rotation = gunFrontBoxPos.rotation;
            DeathKaotiJaios4 GunFrontBoxpos = GunFrontBox.GetComponent<DeathKaotiJaios4>();
            GunFrontBoxpos.Pos = GunFrontBox.transform.position.y;

            GunFrontCircleBox = SingletonObject.instance.Loader("gunFrontCircleBox");
            GunFrontCircleBox.transform.position = gunFrontCircleBoxPos.position;
            GunFrontCircleBox.transform.rotation = gunFrontCircleBoxPos.rotation;
            DeathKaotiJaios4 GunFrontCircleBoxpos = GunFrontCircleBox.GetComponent<DeathKaotiJaios4>();
            GunFrontCircleBoxpos.Pos = GunFrontCircleBox.transform.position.y;

            GunFrontJoint = SingletonObject.instance.Loader("gunFrontJoint");
            GunFrontJoint.transform.position = gunFrontJointPos.position;
            GunFrontJoint.transform.rotation = gunFrontJointPos.rotation;
            DeathKaotiJaios4 GunFrontJointpos = GunFrontJoint.GetComponent<DeathKaotiJaios4>();
            GunFrontJointpos.Pos = GunFrontJoint.transform.position.y;

            GunFrontJointArm = SingletonObject.instance.Loader("gunFrontJointArm");
            GunFrontJointArm.transform.position = gunFrontJointArmPos.position;
            GunFrontJointArm.transform.rotation = gunFrontJointArmPos.rotation;
            DeathKaotiJaios4 GunFrontJointArmpos = GunFrontJointArm.GetComponent<DeathKaotiJaios4>();
            GunFrontJointArmpos.Pos = GunFrontJointArm.transform.position.y;
        }
    }
    public void PartDeathGun2()
    {
        if (Gun2Down == true)
        {
            GunBack = SingletonObject.instance.Loader(DeathGunBackName);
            GunBack.transform.position = gunBackPos.position;
            GunBack.transform.rotation = gunBackPos.rotation;
            DeathKaotiJaios4 GunBackpos = GunBack.GetComponent<DeathKaotiJaios4>();
            GunBackpos.Pos = GunBack.transform.position.y;
        }
    }

    //���� �� ������ ����
    void Death()
    {
        Body = SingletonObject.instance.Loader(DeathBodyName);
        Body.transform.position = bodyPos.position;
        Body.transform.rotation = bodyPos.rotation;
        DeathKaotiJaios4 Bodypos = Body.GetComponent<DeathKaotiJaios4>();
        Bodypos.Pos = Body.transform.position.y;

        if (Gun2Down == false && DualBehaviourKaotiJaios4.Gun2Down == false)
        {
            GunBack = SingletonObject.instance.Loader(DeathGunBackName);
            GunBack.transform.position = gunBackPos.position;
            GunBack.transform.rotation = gunBackPos.rotation;
            DeathKaotiJaios4 GunBackpos = GunBack.GetComponent<DeathKaotiJaios4>();
            GunBackpos.Pos = GunBack.transform.position.y;
        }

        if (Gun1Down == false && DualBehaviourKaotiJaios4.Gun1Down == false)
        {
            GunFront = SingletonObject.instance.Loader(DeathGunFrontName);
            GunFront.transform.position = gunFrontPos.position;
            GunFront.transform.rotation = gunFrontPos.rotation;
            DeathKaotiJaios4 GunFrontpos = GunFront.GetComponent<DeathKaotiJaios4>();
            GunFrontpos.Pos = GunFront.transform.position.y;

            GunFrontBox = SingletonObject.instance.Loader("gunFrontBox");
            GunFrontBox.transform.position = gunFrontBoxPos.position;
            GunFrontBox.transform.rotation = gunFrontBoxPos.rotation;
            DeathKaotiJaios4 GunFrontBoxpos = GunFrontBox.GetComponent<DeathKaotiJaios4>();
            GunFrontBoxpos.Pos = GunFrontBox.transform.position.y;

            GunFrontCircleBox = SingletonObject.instance.Loader("gunFrontCircleBox");
            GunFrontCircleBox.transform.position = gunFrontCircleBoxPos.position;
            GunFrontCircleBox.transform.rotation = gunFrontCircleBoxPos.rotation;
            DeathKaotiJaios4 GunFrontCircleBoxpos = GunFrontCircleBox.GetComponent<DeathKaotiJaios4>();
            GunFrontCircleBoxpos.Pos = GunFrontCircleBox.transform.position.y;

            GunFrontJoint = SingletonObject.instance.Loader("gunFrontJoint");
            GunFrontJoint.transform.position = gunFrontJointPos.position;
            GunFrontJoint.transform.rotation = gunFrontJointPos.rotation;
            DeathKaotiJaios4 GunFrontJointpos = GunFrontJoint.GetComponent<DeathKaotiJaios4>();
            GunFrontJointpos.Pos = GunFrontJoint.transform.position.y;

            GunFrontJointArm = SingletonObject.instance.Loader("gunFrontJointArm");
            GunFrontJointArm.transform.position = gunFrontJointArmPos.position;
            GunFrontJointArm.transform.rotation = gunFrontJointArmPos.rotation;
            DeathKaotiJaios4 GunFrontJointArmpos = GunFrontJointArm.GetComponent<DeathKaotiJaios4>();
            GunFrontJointArmpos.Pos = GunFrontJointArm.transform.position.y;
        }

        if (WheelDown == false && DualBehaviourKaotiJaios4.WheelDown == false)
        {
            TireBack = SingletonObject.instance.Loader("tireBack");
            TireBack.transform.position = tireBackPos.position;
            TireBack.transform.rotation = tireBackPos.rotation;
            DeathKaotiJaios4 TireBackpos = TireBack.GetComponent<DeathKaotiJaios4>();
            TireBackpos.Pos = TireBack.transform.position.y;

            TireFront = SingletonObject.instance.Loader("tireFront");
            TireFront.transform.position = tireFrontPos.position;
            TireFront.transform.rotation = tireFrontPos.rotation;
            DeathKaotiJaios4 TireFrontpos = TireFront.GetComponent<DeathKaotiJaios4>();
            TireFrontpos.Pos = TireFront.transform.position.y;
        }


        part1Random = Random.Range(2, 4);
        for (int i = 1; i <= part1Random; i++)
        {
            GameObject Part1 = SingletonObject.instance.Loader("kao_part1");
            Part1.transform.position = part1Pos.position;
            Part1.transform.rotation = part1Pos.rotation;
            DeathKaotiJaios4 Part1pos = Part1.GetComponent<DeathKaotiJaios4>();
            Part1pos.Pos = Part1.transform.position.y;
        }
        part2Random = Random.Range(4, 6);
        for (int i = 1; i <= part2Random; i++)
        {
            GameObject Part2 = SingletonObject.instance.Loader("kao_part2");
            Part2.transform.position = part2Pos.position;
            Part2.transform.rotation = part2Pos.rotation;
            DeathKaotiJaios4 Part2pos = Part2.GetComponent<DeathKaotiJaios4>();
            Part2pos.Pos = Part2.transform.position.y;
        }
        part3Random = Random.Range(6, 8);
        for (int i = 1; i <= part3Random; i++)
        {

            GameObject Part3 = SingletonObject.instance.Loader("kao_part3");
            Part3.transform.position = part3Pos.position;
            Part3.transform.rotation = part3Pos.rotation;
            DeathKaotiJaios4 Part3pos = Part3.GetComponent<DeathKaotiJaios4>();
            Part3pos.Pos = Part3.transform.position.y;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Delete Area"))
        {
            DeathCount = true;
            TargetPoint.SetActive(false);
            transform.Find("Auto target").gameObject.SetActive(false);
            ScoreManager.instance.DieCnt(1);
            ScoreManager.instance.EnemyList.Remove(gameObject);
            this.gameObject.SetActive(false);
        }
    }
}