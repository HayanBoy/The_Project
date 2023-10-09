using System.Collections;
using UnityEngine;

public class Health2TaikaLaiThrotro1 : Character
{
    Animator animator;
    BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotroPlasma;

    Coroutine damageAction;

    public float hitPoints;
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
    int DeathRandom;

    public bool GunDown = false; //�� ü���� 0�� �Ǹ� �۵��Ǵ� ����ġ
    public bool Engine1Down = false; //1�� ������ ���ư��� �۵��Ǵ� ����ġ
    public bool Engine2Down = false; //2�� ������ ���ư��� �۵��Ǵ� ����ġ
    public bool EngineAllDown = false; //������ ��� ���ư��� �۵��Ǵ� ����ġ
    bool DeathCount = false;
    public bool TearOn = false; //��ü�� �߷��� ���� ��ȣ
    private bool KnockBackShot = false; //�˹� ���� ���� ��ȣ

    public Transform BlueExplosionPos;
    public Transform BlueExplosionPos2;
    GameObject BlueExplosion;
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
    public GameObject FlameTakenEngine1;
    public GameObject FlameTakenEngine2;
    public GameObject FlameTakenGun;

    public Transform backEnginePos;
    public Transform bodyPos;
    public Transform frontEnginePos;
    public Transform railGunPos;
    public Transform part1Pos;
    public Transform part2Pos;
    public Transform part3Pos;

    int part1Random;
    int part2Random;
    int part3Random;

    GameObject[] PoolMaker;

    private int RicochetSoundRandom;
    public AudioClip RicochetSound1;
    public AudioClip RicochetSound2;
    public AudioClip RicochetSound3;
    public AudioClip RicochetSound4;
    public AudioClip RicochetSound5;
    public GameObject RicochetPrefab;
    public Transform RicochetPos;

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
        animator = GetComponent<Animator>();
        BehaviorTaikaLaiThrotroPlasma = FindObjectOfType<BehaviorTaikaLaiThrotro1_3>();
        animator.keepAnimatorStateOnDisable = true;
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            hitPoints = 225;
            startingHitPoints = 225;
            maxHitPoints = 300;
            armor = 1.2f;
            startingArmor = 1.2f;
            KantakriRicochet = 10;
            Ricochet = 10;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            hitPoints = 315;
            startingHitPoints = 315;
            maxHitPoints = 420;
            armor = 1.58f;
            startingArmor = 1.58f;
            KantakriRicochet = 8;
            Ricochet = 8;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            hitPoints = 405;
            startingHitPoints = 405;
            maxHitPoints = 540;
            armor = 1.92f;
            startingArmor = 1.92f;
            KantakriRicochet = 7;
            Ricochet = 7;
        }

        TearOn = false;
        GunDown = false;
        Engine1Down = false;
        Engine2Down = false;
        EngineAllDown = false;
        DeathCount = false;
        TargetPoint.SetActive(true);

        SingletonObject.instance.TaikaPos.Add(bodyPos);
        transform.Find("Auto target").gameObject.SetActive(true);
        ScoreManager.instance.AllEnemyCnt(1);
    }

    private void OnDisable()
    {
        SingletonObject.instance.TaikaPos.Remove(bodyPos);
        animator.SetBool("Death1, Taika-Lai-Throtro 1", false);
        animator.SetBool("Death1 down, Taika-Lai-Throtro 1", false);
        animator.SetBool("Death2, Taika-Lai-Throtro 1", false);
        animator.SetBool("Death2 down, Taika-Lai-Throtro 1", false);
        animator.SetBool("Taking Damage Fast, Taika-Lai-Throtro 1", false);
        animator.SetBool("Taking Damage Fast2, Taika-Lai-Throtro 1", false);
        animator.SetBool("Taking Damage, Taika-Lai-Throtro 1", false);
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
        animator.SetBool("Flame body, Taika-Lai-Throtro 1", true);
        Invoke("TurnOffFlameBody", FlameHitTime);
    }
    public void FlameEngine()
    {
        if (Engine1Down == false)
            FlameTakenEngine1.SetActive(true);
        if (Engine2Down == false)
            FlameTakenEngine2.SetActive(true);
        animator.SetBool("Flame engine, Taika-Lai-Throtro 1", true);
        Invoke("TurnOffFlameEngine", FlameHitTime);
    }
    public void FlameGun()
    {
        if (GunDown == false)
            FlameTakenGun.SetActive(true);
        animator.SetBool("Flame gun, Taika-Lai-Throtro 1", true);
        Invoke("TurnOffFlameGun", FlameHitTime);
    }
    void TurnOffFlameBody()
    {
        animator.SetBool("Flame body, Taika-Lai-Throtro 1", false);
    }
    void TurnOffFlameEngine()
    {
        animator.SetBool("Flame engine, Taika-Lai-Throtro 1", false);
    }
    void TurnOffFlameGun()
    {
        animator.SetBool("Flame gun, Taika-Lai-Throtro 1", false);
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

        if (Engine1Down == true)
            FlameTakenEngine1.SetActive(false);
        if (Engine2Down == true)
            FlameTakenEngine2.SetActive(false);
        if (GunDown == true)
            FlameTakenGun.SetActive(false);

        if (TearOn == true)
        {
            TearOn = false;
            damageAction = StartCoroutine(DamageAction());
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
                        HitAction = 1.116f;
                        DamageTime = 1.116f;
                        damageAction = StartCoroutine(DamageAction());
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        DeathCount = true;
                        TargetPoint.SetActive(false);
                        transform.Find("Auto target").gameObject.SetActive(false);
                        FlameTakenBody.SetActive(false);
                        FlameTakenEngine1.SetActive(true);
                        FlameTakenEngine2.SetActive(true);
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

        GetComponent<Animator>().SetBool("Taking Damage Fast, Taika-Lai-Throtro 1", false);
        GetComponent<Animator>().SetBool("Taking Damage Fast2, Taika-Lai-Throtro 1", false);

        if (damageAction == 1)
        {
            GetComponent<Animator>().SetBool("Taking Damage Fast, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().SetBool("Taking Damage Fast, Taika-Lai-Throtro 1", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Taking Damage Fast2, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.3f);
            GetComponent<Animator>().SetBool("Taking Damage Fast2, Taika-Lai-Throtro 1", false);
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
            animator.SetBool("Charge Karrgen-Arite 31", false);
            animator.SetBool("Fire Karrgen-Arite 31", false);
            animator.SetBool("Fire, Taika-Lai-Throtro 1", false);
            Death();
            KillCharacter();
        }
        else if (DeathRandom == 1)
        {
            animator.SetBool("Charge Karrgen-Arite 31", false);
            animator.SetBool("Fire Karrgen-Arite 31", false);
            animator.SetBool("Fire, Taika-Lai-Throtro 1", false);
            StartCoroutine(Death1());
        }
        else if (DeathRandom == 2)
        {
            animator.SetBool("Charge Karrgen-Arite 31", false);
            animator.SetBool("Fire Karrgen-Arite 31", false);
            animator.SetBool("Fire, Taika-Lai-Throtro 1", false);
            StartCoroutine(Death2());
        }
    }

    IEnumerator Death1()
    {
        if (EngineAllDown == false)
        {
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos.position;
            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
            animator.SetBool("Death1, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.33f);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos2.position;
            BlueExplosion.transform.rotation = BlueExplosionPos2.rotation;
            yield return new WaitForSeconds(0.33f);
            Death();
            KillCharacter();
        }
        else
        {
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos.position;
            BlueExplosion.transform.rotation = BlueExplosionPos.rotation;
            animator.SetBool("Death1 down, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.33f);
            BlueExplosion = SingletonObject.instance.Loader("KantakriBlueSmallExplosion1");
            BlueExplosion.transform.position = BlueExplosionPos2.position;
            BlueExplosion.transform.rotation = BlueExplosionPos2.rotation;
            yield return new WaitForSeconds(0.33f);
            Death();
            KillCharacter();
        }
    }

    IEnumerator Death2()
    {
        if (EngineAllDown == false)
        {
            animator.SetBool("Death2, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
        else
        {
            animator.SetBool("Death2 down, Taika-Lai-Throtro 1", true);
            yield return new WaitForSeconds(0.66f);
            Death();
            KillCharacter();
        }
    }

    //�� �� ���� ����ȭ
    public void PartDeathGun()
    {
        if (GunDown == true)
        {
            GameObject RailGun = SingletonObject.instance.Loader("KarrgenArite31");
            RailGun.transform.position = railGunPos.position;
            RailGun.transform.rotation = railGunPos.rotation;
        }
    }
    public void PartDeathEngine1()
    {
        if (Engine1Down == true)
        {
            GameObject FrontEngine = SingletonObject.instance.Loader("Taika_frontEngine");
            FrontEngine.transform.position = frontEnginePos.position;
            FrontEngine.transform.rotation = frontEnginePos.rotation;
        }
    }
    public void PartDeathEngine2()
    {
        if (Engine2Down == true)
        {
            GameObject BackEngine = SingletonObject.instance.Loader("Taika_backEngine");
            BackEngine.transform.position = backEnginePos.position;
            BackEngine.transform.rotation = backEnginePos.rotation;
        }
    }

    //������Ʈ ����
    void Death()
    {
        GameObject Body = SingletonObject.instance.Loader("Taika_body2");
        Body.transform.position = bodyPos.position;
        Body.transform.rotation = bodyPos.rotation;

        if (Engine1Down == false && BehaviorTaikaLaiThrotroPlasma.Engine1Down == false)
        {
            GameObject FrontEngine = SingletonObject.instance.Loader("Taika_frontEngine");
            FrontEngine.transform.position = frontEnginePos.position;
            FrontEngine.transform.rotation = frontEnginePos.rotation;
        }

        if (Engine2Down == false && BehaviorTaikaLaiThrotroPlasma.Engine2Down == false)
        {
            GameObject BackEngine = SingletonObject.instance.Loader("Taika_backEngine");
            BackEngine.transform.position = backEnginePos.position;
            BackEngine.transform.rotation = backEnginePos.rotation;
        }

        if (GunDown == false && BehaviorTaikaLaiThrotroPlasma.GunDown == false)
        {
            GameObject RailGun = SingletonObject.instance.Loader("KarrgenArite31");
            RailGun.transform.position = railGunPos.position;
            RailGun.transform.rotation = railGunPos.rotation;
        }

        part1Random = Random.Range(2, 4);
        for (int i = 1; i <= part1Random; i++)
        {
            GameObject Part1 = SingletonObject.instance.Loader("Taika_part1");
            Part1.transform.position = part1Pos.position;
            Part1.transform.rotation = part1Pos.rotation;
        }
        part2Random = Random.Range(4, 6);
        for (int i = 1; i <= part2Random; i++)
        {
            GameObject Part2 = SingletonObject.instance.Loader("Taika_part2");
            Part2.transform.position = part2Pos.position;
            Part2.transform.rotation = part2Pos.rotation;
        }
        part3Random = Random.Range(6, 8);
        for (int i = 1; i <= part3Random; i++)
        {
            GameObject Part3 = SingletonObject.instance.Loader("Taika_part3");
            Part3.transform.position = part3Pos.position;
            Part3.transform.rotation = part3Pos.rotation;
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