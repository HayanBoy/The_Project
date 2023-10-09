using System.Collections;
using UnityEngine;

public class HealthInfector : Character
{
    public TearInfector tearInfector;

    public float hitPoints;
    float armor;
    public float TaitrokiBladeHiderHP;
    public float MaxTaitrokiBladeHiderHP;
    public float TaitrokiBladeHiderArmor;

    private RegdollControllerInfector rdci; //RegdollControllerInfector스크립트 불러오기

    Animator animator;
    Rigidbody2D rb2D;

    Coroutine damageAction;
    public GameObject TargetPoint; //적이 살아있을 때만 적 상단에 표시되는 UI

    public bool Death = false;
    public bool ImHit = false;
    public bool TearOn = false; //신체가 잘렸을 때의 신호
    private bool TransformOne = false; //한번만 변신할 수 있도록 조취
    private bool KnockBackShot = false; //넉백 당할 때의 신호
    private bool FlameDeathing = false; //불에 타 죽을 떄 불 이펙트 모두 발생하기 위한 스위치

    public int Type = 0; //좀비 유형
    public int DamageType; //데미지 애니메이션
    private float RandomTransformHP; //타이트로키로 변신하기 위한 체력 조건
    private float HitAction;
    private float DamageTime;
    public float KnockBackForce;
    public float KnockBackReducer; //넉백당했을 때, 감속처리
    public float KnockBackLevelUp;
    private float KnockBackSpeed;
    private float x;
    public float FlameHitTime; //화염방사기에 맞았을 때 불 지속시간

    public GameObject Shadow;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;

    //타격 빔 생성
    public GameObject BeamTaken1;
    public GameObject BeamTaken2;
    public GameObject BeamTaken3;
    public GameObject BeamTaken4;
    public Transform BeamTakenPos;
    int BeamDamageAction; //빔 효과 받기
    float TimeStemp; //타격 빔 효과를 딱 한번만 발동되도록 조취

    //타격 불 생성
    public GameObject FlameTakenBody;
    public GameObject FlameTakenArmTop1;
    public GameObject FlameTakenArmTop2;
    public GameObject FlameTakenArmDown1;
    public GameObject FlameTakenArmDown2;
    public GameObject FlameTakenLeg1;
    public GameObject FlameTakenLeg2;
    public GameObject FlameTakenHead;
    public ParticleSystem FlameEffect1;
    public ParticleSystem FlameEffect2;
    public ParticleSystem FlameEffect3;
    public ParticleSystem FlameEffect4;
    public ParticleSystem FlameEffect5;
    public ParticleSystem FlameEffect6;
    public ParticleSystem FlameEffect7;
    public ParticleSystem FlameEffect8;
    public ParticleSystem FlameEffect9;
    public ParticleSystem FlameEffect10;

    public AudioClip TaitrokiDamage1;
    public AudioClip TaitrokiDamage2;
    public AudioClip TaitrokiDeathSound1;

    //대쉬 근접 공격에 의한 피격
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

    //빔 데미지 받기
    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    //불 이펙트 생성
    public void FlameBody()
    {
        FlameTakenBody.SetActive(true);
        animator.SetBool("Flame body, Infector", true);
        Invoke("TurnOffFlameBody", FlameHitTime);
    }
    public void FlameLegs()
    {
        if (tearInfector.LegR == false)
            FlameTakenLeg1.SetActive(true);
        if (tearInfector.LegL == false)
            FlameTakenLeg2.SetActive(true);
        animator.SetBool("Flame legs, Infector", true);
        Invoke("TurnOffFlameLegs", FlameHitTime);
    }
    public void FlameArm()
    {
        if (tearInfector.B1LURend == false)
            FlameTakenArmTop1.SetActive(true);
        if (tearInfector.B1LURend == false && tearInfector.B1LDRend == false)
            FlameTakenArmDown1.SetActive(true);
        if (tearInfector.B1LULend == false)
            FlameTakenArmTop2.SetActive(true);
        if (tearInfector.B1LULend == false && tearInfector.B1LDLend == false)
            FlameTakenArmDown2.SetActive(true);
        animator.SetBool("Flame arm, Infector", true);
        Invoke("TurnOffFlameArm", FlameHitTime);
    }
    public void FlameHead()
    {
        FlameTakenHead.SetActive(true);
        animator.SetBool("Flame head, Infector", true);
        Invoke("TurnOffFlameHead", FlameHitTime);
    }
    void TurnOffFlameBody()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame body, Infector", false);
    }
    void TurnOffFlameLegs()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame legs, Infector", false);
    }
    void TurnOffFlameArm()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame arm, Infector", false);
    }
    void TurnOffFlameHead()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame head, Infector", false);
    }

    //화염 지속 데미지 시작
    public void FlameDamgeStart(int damage, float interval)
    {
        StartCoroutine(FireDamageCharacter(damage, interval));
    }

    public void BladeTaitroki()
    {
        Type = 1;
        hitPoints = TaitrokiBladeHiderHP;
        armor = TaitrokiBladeHiderArmor;
        RandomTransformHP = Random.Range(200, 400);
        StartCoroutine(SpawnTaiTroki());
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        rdci = GetComponent<RegdollControllerInfector>();
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;
        //rdci.DisableRagdoll();
    }

    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<InfectorBehavior>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<InfectorSpawn>().enabled = true;
        GetComponent<InfectorSpawn>().TearingStart = true;
    }

    IEnumerator SpawnTaiTroki()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<InfectorBehavior>().enabled = true;
        GetComponent<InfectorBehavior>().BladeTaitroki();
        yield return new WaitForSeconds(0.5f);
        GetComponent<InfectorSpawn>().enabled = true;
        GetComponent<InfectorSpawn>().TearingStart = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BehaviorTaitroki>().enabled = true;
    }

    private void OnEnable()
    {
        if (Type == 0) //일반 좀비
        {
            if (BattleSave.Save1.MissionLevel == 1)
            {
                hitPoints = 200;
                startingHitPoints = 200;
                maxHitPoints = 400;
                armor = 1;
                startingArmor = 1;
            }
            else if (BattleSave.Save1.MissionLevel == 2)
            {
                hitPoints = 280;
                startingHitPoints = 280;
                maxHitPoints = 560;
                armor = 1.3f;
                startingArmor = 1.3f;
            }
            else if (BattleSave.Save1.MissionLevel == 3)
            {
                hitPoints = 360;
                startingHitPoints = 360;
                maxHitPoints = 720;
                armor = 1.5f;
                startingArmor = 1.5f;
            }
            StartCoroutine(SpawnZombie());
        }
        else if (Type == 1) //타이트로키
        {
            if (BattleSave.Save1.MissionLevel == 1)
            {
                TaitrokiBladeHiderHP = 400;
                MaxTaitrokiBladeHiderHP = 600;
                TaitrokiBladeHiderArmor = 1.3f;
            }
            else if (BattleSave.Save1.MissionLevel == 2)
            {
                TaitrokiBladeHiderHP = 560;
                MaxTaitrokiBladeHiderHP = 840;
                TaitrokiBladeHiderArmor = 1.72f;
            }
            else if (BattleSave.Save1.MissionLevel == 3)
            {
                TaitrokiBladeHiderHP = 720;
                MaxTaitrokiBladeHiderHP = 1080;
                TaitrokiBladeHiderArmor = 2.08f;
            }
            hitPoints = TaitrokiBladeHiderHP;
            armor = TaitrokiBladeHiderArmor;
            RandomTransformHP = Random.Range(100, 300);
        }

        if (Death == true)
        {
            if (ImHit == true)
                ImHit = false;
            if (TearOn == true)
                TearOn = false;
            if (TransformOne == true)
                TransformOne = false;
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            rdci = GetComponent<RegdollControllerInfector>();
            rdci.DisableRagdoll();
            if (Type == 0)
                GetComponent<InfectorBehavior>().InfectorDeath(false); //죽은 상태 되돌리기 전달
            if (Type == 1)
                GetComponent<BehaviorTaitroki>().InfectorDeath(false);
            GetComponent<ReverseCall>().enabled = true;
            TraceX call1 = transform.Find("bone_1").GetComponent<TraceX>();
            call1.ShadowReset = true;
            Invoke("ReverseOff", 0.1f);
            TargetPoint.SetActive(true);
            transform.Find("Auto target").gameObject.SetActive(true);
            Shadow.SetActive(true);
            
            if (animator.GetFloat("Flame Death, Infector") > 0)
                animator.SetFloat("Flame Death, Infector", 0);
        }

        ScoreManager.instance.AllEnemyCnt(1);
    }

    void ReverseOff()
    {
        GetComponent<ReverseCall>().enabled = false;
        Death = false;
    }

    void Update()
    {
        //Debug.Log("ImHit : " + ImHit);

        if (KnockBackShot == true)
        {
            transform.Translate(Vector2.left * KnockBackSpeed * Time.deltaTime);

            if (KnockBackSpeed > 0)
                KnockBackSpeed -= KnockBackReducer;
            else
                KnockBackSpeed = 0;
        }

        if (TimeStemp >= 0)
            TimeStemp -= Time.deltaTime;

        if (TimeStemp < 0)
        {
            TimeStemp = 0;
            BeamDamageAction = 0; //레이져 무기에 타격받은 이후, 다른 무기 타격을 받았을 때 레이져 맞은 효과가 나타나지 않도록 하기 위한 조취
        }

        if (HitAction >= 0)
            HitAction -= Time.deltaTime;

        if (FlameHitTime >= 0)
            FlameHitTime -= Time.deltaTime;

        if (tearInfector.LegR == true)
            FlameTakenLeg1.SetActive(false);
        if (tearInfector.LegL == true)
            FlameTakenLeg2.SetActive(false);
        if (tearInfector.B1LURend == true)
        {
            FlameTakenArmTop1.SetActive(false);
            FlameTakenArmDown1.SetActive(false);
        }
        if (tearInfector.B1LDRend == true)
            FlameTakenArmDown1.SetActive(false);
        if (tearInfector.B1LULend == true)
        {
            FlameTakenArmTop2.SetActive(false);
            FlameTakenArmDown2.SetActive(false);
        }
        if (tearInfector.B1LDLend == true)
            FlameTakenArmDown2.SetActive(false);

        if (FlameDeathing == true)
        {
            FlameTakenBody.SetActive(true);
            animator.SetBool("Flame body, Infector", true);
            if (tearInfector.LegR == false)
                FlameTakenLeg1.SetActive(true);
            if (tearInfector.LegL == false)
                FlameTakenLeg2.SetActive(true);
            animator.SetBool("Flame legs, Infector", true);
            if (tearInfector.B1LURend == false)
                FlameTakenArmTop1.SetActive(true);
            if (tearInfector.B1LURend == false && tearInfector.B1LDRend == false)
                FlameTakenArmDown1.SetActive(true);
            if (tearInfector.B1LULend == false)
                FlameTakenArmTop2.SetActive(true);
            if (tearInfector.B1LULend == false && tearInfector.B1LDLend == false)
                FlameTakenArmDown2.SetActive(true);
            animator.SetBool("Flame arm, Infector", true);
            FlameTakenHead.SetActive(true);
            animator.SetBool("Flame head, Infector", true);
        }

        if (TearOn == true)
        {
            TearOn = false;
            damageAction = StartCoroutine(DamageAction());
        }

        if (tearInfector.LegL == true || tearInfector.LegR == true || tearInfector.TakeDown == 1)
        {
            StopCoroutine(damageAction);
            animator.SetFloat("Damage Type", 0);
            animator.SetBool("Damage face up, Infector", false);
            animator.SetBool("Damage face down, Infector", false);
        }
    }

    //적이 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if (Death == false)
        {
            while (true)
            {
                hitPoints = hitPoints - (damage / armor);

                if (BeamDamageAction > 0)
                {
                    StartCoroutine(BeamAction());
                    TimeStemp += 0.025f;
                }

                if (ImHit == true)
                {
                    GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
                    Destroy(BloodFloor, 10);

                    if (Type == 1 && TransformOne == true) //블레이드 하이더 피격 목소리
                    {
                        int TaitrokiDamageRandom = Random.Range(0, 2);

                        if (TaitrokiDamageRandom == 0)
                            SoundManager.instance.SFXPlay2("Sound", TaitrokiDamage1);
                        else
                            SoundManager.instance.SFXPlay2("Sound", TaitrokiDamage2);
                    }
                }

                if (HitAction <= 0)
                {
                    HitAction = 0.83f;
                    if (tearInfector.LegL == false && tearInfector.LegR == false)
                    {
                        DamageTime = 0.83f;
                        damageAction = StartCoroutine(DamageAction());
                    }
                }

                if (Type == 1 && TransformOne == false) //블레이드 하이더 변신 조건
                {
                    if (hitPoints <= TaitrokiBladeHiderHP - RandomTransformHP)
                    {
                        TransformOne = true;
                        StopCoroutine(damageAction);
                        animator.SetFloat("Damage Type", 0);
                        animator.SetBool("Damage face up, Infector", false);
                        animator.SetBool("Damage face down, Infector", false);
                        GetComponent<BehaviorTaitroki>().BladeTaitroki();
                        transform.Find("Auto target").gameObject.tag = "Taitroki";
                    }
                }

                if (Type > 0 && hitPoints <= TaitrokiBladeHiderHP * 0.4f) //타이트로키 체력이 60% 이상 감소되었을 경우 사지가 잘리기 시작
                {
                    GetComponent<InfectorSpawn>().TearingStart = true;
                    GetComponent<TearInfector>().StartTearing = true;
                }

                if (hitPoints <= float.Epsilon) //죽음
                {
                    if (Death == false)
                    {
                        if (FlameHitTime <= 0) //불에 붙지 않았을 경우
                        {
                            Death = true;
                            if (Type == 0)
                                animator.SetBool("Down, Infector", false);
                            if (Type == 1)
                                animator.SetBool("Taitroki Turn off", true);
                            rdci.ActiveRagdoll(); //RegdollControllerInfector스크립트의 ActiveRagdoll 메소드 호출
                            if (Type == 0)
                                GetComponent<InfectorBehavior>().InfectorDeath(true); //죽은 상태 전달
                            if (Type == 1)
                            {
                                SoundManager.instance.SFXPlay2("Sound", TaitrokiDeathSound1);
                                GetComponent<BehaviorTaitroki>().InfectorDeath(true);
                            }
                            GetComponent<DeathCall>().enabled = true; //레그돌 담당 신체 파트에다 죽음 신호 전달
                            DeathRolling call = transform.Find("bone_1").GetComponent<DeathRolling>(); //죽을 때 신체 회전
                            call.enabled = true;
                            TraceX call1 = transform.Find("bone_1").GetComponent<TraceX>();
                            call1.DeathTransformTime = true;//죽었을 때의 위치 좌표 계산 스위치
                            call1.ShadowTime = 5f;
                            animator.SetBool("Down, Infector", false);
                            TargetPoint.SetActive(false);
                            transform.Find("Auto target").gameObject.SetActive(false);
                            Shadow.SetActive(false);

                            if (Type > 0)
                                ScoreManager.instance.InfectorEliteCnt++;
                            ScoreManager.instance.setScore(1);
                            ScoreManager.instance.set_Infector_Score(1);
                            ScoreManager.instance.DieCnt(1);
                            ScoreManager.instance.EnemyList.Remove(gameObject);

                            Type = 0;
                            Invoke("InfectorSpawnOff", 1f);
                            Invoke("Kill", 4f);
                            break;
                        }
                        else if (FlameHitTime > 0) //불이 붙은 상태의 경우
                        {
                            if (Death == false)
                            {
                                Death = true;
                                StartCoroutine(FlameDeath());
                                break;
                            }
                        }
                    }
                }

                if (interval > float.Epsilon)
                    yield return new WaitForSeconds(interval);

                else
                    break;
            }
        }
    }

    //적에 불이 붙었을 때의 데미지 적용
    IEnumerator FireDamageCharacter(int damage, float interval)
    {
        if (Death == false)
        {
            while (FlameHitTime > 0)
            {
                StartCoroutine(DamageCharacter(damage, 0));
                yield return new WaitForSeconds(interval);
            }
        }
    }

    void InfectorSpawnOff()
    {
        GetComponent<InfectorSpawn>().enabled = false;
    }

    void Kill()
    {
        GetComponent<StartTypeTaitroki>().enabled = false;
        GetComponent<InfectorBehavior>().enabled = false;
        GetComponent<BehaviorTaitroki>().enabled = false;
        rdci.DisableRagdoll();
        if (Type == 0)
            GetComponent<InfectorBehavior>().InfectorDeath(false); //죽은 상태 되돌리기 전달
        if (Type == 1)
            GetComponent<BehaviorTaitroki>().InfectorDeath(false);
        GetComponent<DeathCall>().layerTime = 0;
        GetComponent<DeathCall>().enabled = false;
        FlameTakenBody.SetActive(false);
        FlameTakenLeg1.SetActive(false);
        FlameTakenLeg2.SetActive(false);
        FlameTakenArmTop1.SetActive(false);
        FlameTakenArmDown1.SetActive(false);
        FlameTakenArmTop2.SetActive(false);
        FlameTakenArmDown2.SetActive(false);
        FlameTakenHead.SetActive(false);
        KillCharacter();
    }

    void Delete()
    {
        GetComponent<StartTypeTaitroki>().enabled = false;
        GetComponent<InfectorBehavior>().enabled = false;
        GetComponent<BehaviorTaitroki>().enabled = false;
        ScoreManager.instance.DieCnt(1);
        ScoreManager.instance.EnemyList.Remove(gameObject);
        this.gameObject.SetActive(false);
    }

    public IEnumerator DamageAction()
    {
        DamageType = Random.Range(1, 6);

        animator.SetFloat("Damage Type", 0);

        if (DamageType == 1)
        {
            animator.SetFloat("Moving Top", 0);
            animator.SetFloat("Damage Type", DamageType);
            yield return new WaitForSeconds(DamageTime);
            animator.SetFloat("Damage Type", 0);
        }
        else if (DamageType == 2)
        {
            animator.SetFloat("Moving Top", 0);
            animator.SetFloat("Damage Type", DamageType);
            yield return new WaitForSeconds(DamageTime);
            animator.SetFloat("Damage Type", 0);
        }
        else if (DamageType == 3)
        {
            animator.SetFloat("Moving Top", 0);
            animator.SetFloat("Damage Type", DamageType);
            yield return new WaitForSeconds(DamageTime);
            animator.SetFloat("Damage Type", 0);
        }
        else if (DamageType == 4)
        {
            animator.SetFloat("Moving Top", 0);
            animator.SetFloat("Damage Type", DamageType);
            yield return new WaitForSeconds(DamageTime);
            animator.SetFloat("Damage Type", 0);
        }

        int damageActionFace;
        damageActionFace = Random.Range(0, 2);

        animator.SetBool("Damage face up, Infector", false);
        animator.SetBool("Damage face down, Infector", false);

        if (damageActionFace == 0)
        {
            animator.SetBool("Damage face up, Infector", true);
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Damage face up, Infector", false);
        }
        else
        {
            animator.SetBool("Damage face down, Infector", true);
            yield return new WaitForSeconds(0.4f);
            animator.SetBool("Damage face down, Infector", false);
        }
    }

    //빔 공격 받았을 때의 시각효과 발생
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

    IEnumerator FlameDeath()
    {
        int FlameDeathAnimation = Random.Range(0, 3);
        float FlameDeathTime = Random.Range(3, 5);

        if (FlameDeathAnimation == 0)
            animator.SetFloat("Flame Death, Infector", 1);
        else if (FlameDeathAnimation == 1)
            animator.SetFloat("Flame Death, Infector", 2);
        else
            animator.SetFloat("Flame Death, Infector", 3);

        var Flame1 = FlameEffect1.emission;
        var Flame2 = FlameEffect2.emission;
        var Flame3 = FlameEffect3.emission;
        var Flame4 = FlameEffect4.emission;
        var Flame5 = FlameEffect5.emission;
        var Flame6 = FlameEffect6.emission;
        var Flame7 = FlameEffect7.emission;
        var Flame8 = FlameEffect8.emission;
        var Flame9 = FlameEffect9.emission;
        var Flame10 = FlameEffect10.emission;

        FlameDeathing = true;

        if (Type == 0)
            GetComponent<InfectorBehavior>().FlameDeath = true;
        if (Type == 1)
            GetComponent<BehaviorTaitroki>().FlameDeath = true;
        yield return new WaitForSeconds(FlameDeathTime);

        if (Type == 0)
            GetComponent<InfectorBehavior>().FlameDeath = false;
        if (Type == 1)
            GetComponent<BehaviorTaitroki>().FlameDeath = false;

        FlameDeathing = false;

        if (Type == 0)
            animator.SetBool("Down, Infector", false);
        if (Type == 1)
            animator.SetBool("Taitroki Turn off", true);
        rdci.ActiveRagdoll(); //RegdollControllerInfector스크립트의 ActiveRagdoll 메소드 호출
        if (Type == 0)
            GetComponent<InfectorBehavior>().InfectorDeath(true); //죽은 상태 전달
        if (Type == 1)
        {
            SoundManager.instance.SFXPlay2("Sound", TaitrokiDeathSound1);
            GetComponent<BehaviorTaitroki>().InfectorDeath(true);
        }
        GetComponent<DeathCall>().enabled = true; //레그돌 담당 신체 파트에다 죽음 신호 전달
        DeathRolling call = transform.Find("bone_1").GetComponent<DeathRolling>(); //죽을 때 신체 회전
        call.enabled = true;
        TraceX call1 = transform.Find("bone_1").GetComponent<TraceX>();
        call1.DeathTransformTime = true;//죽었을 때의 위치 좌표 계산 스위치
        call1.ShadowTime = 7f;
        animator.SetBool("Down, Infector", false);
        TargetPoint.SetActive(false);
        transform.Find("Auto target").gameObject.SetActive(false);
        Shadow.SetActive(false);

        if (Type > 0)
            ScoreManager.instance.InfectorEliteCnt++;
        ScoreManager.instance.setScore(1);
        ScoreManager.instance.set_Infector_Score(1);
        ScoreManager.instance.DieCnt(1);
        ScoreManager.instance.EnemyList.Remove(gameObject);

        Type = 0;
        Invoke("InfectorSpawnOff", 1f);
        Invoke("Kill", 7);

        yield return new WaitForSeconds(5);
        Flame1.rateOverTime = 0;
        Flame2.rateOverTime = 0;
        Flame3.rateOverTime = 0;
        Flame4.rateOverTime = 0;
        Flame5.rateOverTime = 0;
        Flame6.rateOverTime = 0;
        Flame7.rateOverTime = 0;
        Flame8.rateOverTime = 0;
        Flame9.rateOverTime = 0;
        Flame10.rateOverTime = 0;

        animator.SetBool("Flame body, Infector", false);
        animator.SetBool("Flame legs, Infector", false);
        animator.SetBool("Flame arm, Infector", false);
        animator.SetBool("Flame head, Infector", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Delete Area"))
        {
            Death = true;
            TargetPoint.SetActive(false);
            transform.Find("Auto target").gameObject.SetActive(false);
            Type = 0;
            Invoke("InfectorSpawnOff", 1f);
            Invoke("Delete", 4f);
        }
    }

    //    부록
    //    GameObject bodyBone = transform.Find("bone_1").gameObject; //첫 카테고리의 자식 찾기
    //    bodyBone.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //리지드바디2D에서 바디타입 바꾸기
    //    GameObject head = bodyBone.transform.Find("bone_f1").gameObject; //두번째 카테고리의 자식 찾기
    //    GameObject rightArmUp = GameObject.FindWithTag("Infector Bone_3"); //태그로 찾기
}