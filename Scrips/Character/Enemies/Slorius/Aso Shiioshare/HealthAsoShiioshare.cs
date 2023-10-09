using System.Collections;
using UnityEngine;

public class HealthAsoShiioshare : Character
{
    TearAsoShiioshare tearAsoShiioshare;

    float hitPoints;
    float armor;

    private RegdollControllerAsoShiioshare rdcas; //RegdollControllerAsoShiioshare 불러오기

    Animator animator;
    Rigidbody2D rb2D;

    Coroutine damageAction;
    Coroutine damageAction2;
    private bool DamageActionBool = false;
    private bool DamageActionDownBool = false;

    public bool ImHit = false; //화염을 제외한 무기에 타격을 받았을 경우, 피를 생성하기 위한 조취
    public bool Death = false;
    public bool TearOn = false; //신체가 잘렸을 때의 신호
    private bool AttackUsing = false; //스킬 및 근접 공격 도중 피격 애니메이션 비활성화
    private bool ImDown = false; //넘어졌을 때, 데미지 애니메이션 비활성화용
    private bool DownMark = false;
    private bool HeadDown = false; //핼멧이 날아가면 작동되는 스위치
    private bool MainWeapons = false; //주무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool SubWeaponsR = false; //오른쪽 보조무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool SubWeaponsL = false; //왼쪽 보조무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool MainWeaponOff = false; //주무기를 떨어뜨렸을 때의 신호
    private bool SubWeaponsRReady = false; //오른쪽 보조무기를 들었을 때의 신호
    private bool SubWeaponsLReady = false; //왼쪽 보조무기를 들었을 때의 신호
    private bool ShieldDown = false; //쉴드가 무력화 되었을 때의 신호
    private bool KnockBackShot = false; //넉백 당할 때의 신호
    private bool FlameDeathing = false; //불에 타 죽을 때 불 이펙트 모두 발생하기 위한 스위치

    private int DamageType; //데미지 애니메이션
    private float HitAction;
    private float HitTime;
    public float DamageTime; //데미지 애니메이션 출력 시간
    public float KnockBackForce;
    public float KnockBackReducer; //넉백당했을 때, 감속처리
    public float KnockBackLevelUp;
    private float KnockBackSpeed;
    private float x;
    public float FlameHitTime; //화염방사기에 맞았을 때 불 지속시간

    public GameObject TargetPoint; //적이 살아있을 때만 적 상단에 표시되는 UI

    public GameObject Head;
    public Transform Headpos;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;

    IEnumerator bloodaction;
    public GameObject HeadBlood;
    public Transform FloorBlood1Pos;

    public GameObject MainWeapon;
    public Transform MainWeaponpos;
    public GameObject SubWeapon;
    public Transform SubWeaponRightpos;
    public Transform SubWeaponLeftpos;
    public GameObject Shadow;
    public GameObject AttackLine;

    //타격 빔 생성
    public GameObject BeamTaken1;
    public GameObject BeamTaken2;
    public GameObject BeamTaken3;
    public GameObject BeamTaken4;
    public Transform BeamTakenPos;
    int BeamDamageAction; //빔 효과 받기
    float TimeStemp;

    //타격 불 생성
    public GameObject FlameTakenBody;
    public GameObject FlameTakenArmTop1;
    public GameObject FlameTakenArmTop2;
    public GameObject FlameTakenArmTop3;
    public GameObject FlameTakenArmDown1;
    public GameObject FlameTakenArmDown2;
    public GameObject FlameTakenArmDown3;
    public GameObject FlameTakenLeg1Top;
    public GameObject FlameTakenLeg1Down;
    public GameObject FlameTakenLeg2Top;
    public GameObject FlameTakenLeg2Down;
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
    public ParticleSystem FlameEffect11;
    public ParticleSystem FlameEffect12;
    public ParticleSystem FlameEffect13;
    public ParticleSystem FlameEffect14;
    public ParticleSystem FlameEffect15;

    int VoiceRandom;
    int VoicePrint;
    float VoiceTime;

    public AudioClip Voice1;
    public AudioClip Voice2;
    public AudioClip DamageVoice1;
    public AudioClip DamageVoice2;
    public AudioClip DeathVoice1;
    public AudioClip DeathVoice2;

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
        animator.SetBool("Flame body, Aso Shiioshare", true);
        Invoke("TurnOffFlameBody", FlameHitTime);
    }
    public void FlameLegs()
    {
        if (tearAsoShiioshare.RightLegTop1 == false)
            FlameTakenLeg1Top.SetActive(true);
        if (tearAsoShiioshare.RightLegTop1 == false && tearAsoShiioshare.RightLegDown1 == false)
            FlameTakenLeg1Down.SetActive(true);
        if (tearAsoShiioshare.RightLegTop2 == false)
            FlameTakenLeg2Top.SetActive(true);
        if (tearAsoShiioshare.RightLegTop2 == false && tearAsoShiioshare.RightLegDown2 == false)
            FlameTakenLeg2Down.SetActive(true);
        animator.SetBool("Flame legs, Aso Shiioshare", true);
        Invoke("TurnOffFlameLegs", FlameHitTime);
    }
    public void FlameArm()
    {
        if (tearAsoShiioshare.RightArmTop1 == false)
            FlameTakenArmTop1.SetActive(true);
        if (tearAsoShiioshare.RightArmTop1 == false && tearAsoShiioshare.RightArmDown1 == false)
            FlameTakenArmDown1.SetActive(true);
        if (tearAsoShiioshare.RightArmTop2 == false)
            FlameTakenArmTop2.SetActive(true);
        if (tearAsoShiioshare.RightArmTop2 == false && tearAsoShiioshare.RightArmDown2 == false)
            FlameTakenArmDown2.SetActive(true);
        if (tearAsoShiioshare.RightArmTop3 == false)
            FlameTakenArmTop3.SetActive(true);
        if (tearAsoShiioshare.RightArmTop3 == false && tearAsoShiioshare.RightArmDown3 == false)
            FlameTakenArmDown3.SetActive(true);
        animator.SetBool("Flame arm, Aso Shiioshare", true);
        Invoke("TurnOffFlameArm", FlameHitTime);
    }
    public void FlameHead()
    {
        FlameTakenHead.SetActive(true);
        animator.SetBool("Flame head, Aso Shiioshare", true);
        Invoke("TurnOffFlameHead", FlameHitTime);
    }
    void TurnOffFlameBody()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame body, Aso Shiioshare", false);
    }
    void TurnOffFlameLegs()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame legs, Aso Shiioshare", false);
    }
    void TurnOffFlameArm()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame arm, Aso Shiioshare", false);
    }
    void TurnOffFlameHead()
    {
        if (FlameDeathing == false)
            animator.SetBool("Flame head, Aso Shiioshare", false);
    }

    //화염 지속 데미지 시작
    public void FlameDamgeStart(int damage, float interval)
    {
        StartCoroutine(FireDamageCharacter(damage, interval));
    }

    //공격시, 타격 애니메이션 비활성화
    public void AsoShiioshareAttacking(bool Down)
    {
        if (Down == true)
        {
            AttackUsing = true;
        }
        else
        {
            AttackUsing = false;
        }
    }

    public void AsoShiioshareImDown(bool Down)
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

    public void AsoShiioshareDownMark(bool Down)
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

    public void AsoShiioshareHeadDown(bool Down)
    {
        if (Down == true)
        {
            HeadDown = true;
        }
        else
        {
            HeadDown = false;
        }
    }

    public void AsoShiioshareMainWeapon(bool Down)
    {
        if (Down == true)
        {
            MainWeapons = true;
            MainWeaponOff = true;
        }
        else
        {
            MainWeapons = false;
            MainWeaponOff = false;
        }
    }

    public void AsoShiioshareSubWeaponR(bool Down)
    {
        if (Down == true)
        {
            SubWeaponsR = true;
        }
        else
        {
            SubWeaponsR = false;
        }
    }

    public void AsoShiioshareSubWeaponL(bool Down)
    {
        if (Down == true)
        {
            SubWeaponsL = true;
        }
        else
        {
            SubWeaponsL = false;
        }
    }

    public void AsoShiioshareSubWeaponRReady(bool Down)
    {
        if (Down == true)
        {
            SubWeaponsRReady = true;
        }
        else
        {
            SubWeaponsRReady = false;
        }
    }

    public void AsoShiioshareSubWeaponLReady(bool Down)
    {
        if (Down == true)
        {
            SubWeaponsLReady = true;
        }
        else
        {
            SubWeaponsLReady = false;
        }
    }

    public void AsoShiioshareShieldDown(bool Down)
    {
        if (Down == true)
        {
            ShieldDown = true;
        }
        else
        {
            ShieldDown = false;
        }
    }

    IEnumerator VoiceSound()
    {
        while (true)
        {
            VoiceRandom = Random.Range(0, 20);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator BloodAction() //얼굴에서 뿜어져 나오는 피의 방향
    {
        GameObject BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(HeadBlood, FloorBlood1Pos.transform.position, FloorBlood1Pos.transform.rotation);
        Destroy(BloodFloor, 10);
    }

    void Start()
    {
        tearAsoShiioshare = FindObjectOfType<TearAsoShiioshare>();
        StartCoroutine(VoiceSound());
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        rdcas = GetComponent<RegdollControllerAsoShiioshare>();
        bloodaction = BloodAction();
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;
        TargetPoint.SetActive(true);
    }

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            hitPoints = 450;
            startingHitPoints = 450;
            maxHitPoints = 650;
            armor = 1.3f;
            startingArmor = 1.3f;
            GetComponent<ShieldAsoShiioshare>().StartShieldPoints = 450;
            GetComponent<ShieldAsoShiioshare>().ShieldPoints = 450;
            GetComponent<ShieldAsoShiioshare>().ShieldArmor = 30;
            SloriusShieldArmor = 30;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            hitPoints = 585;
            startingHitPoints = 585;
            maxHitPoints = 845;
            armor = 1.69f;
            startingArmor = 1.69f;
            GetComponent<ShieldAsoShiioshare>().StartShieldPoints = 585;
            GetComponent<ShieldAsoShiioshare>().ShieldPoints = 585;
            GetComponent<ShieldAsoShiioshare>().ShieldArmor = 39;
            SloriusShieldArmor = 39;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            hitPoints = 765;
            startingHitPoints = 765;
            maxHitPoints = 1105;
            armor = 2.2f;
            startingArmor = 2.2f;
            GetComponent<ShieldAsoShiioshare>().StartShieldPoints = 765;
            GetComponent<ShieldAsoShiioshare>().ShieldPoints = 765;
            GetComponent<ShieldAsoShiioshare>().ShieldArmor = 50;
            SloriusShieldArmor = 50;
        }

        if (Death == true)
        {
            if(HeadDown == true)
                transform.Find("Body1/Head body blood").gameObject.SetActive(false);
            TearOn = false;
            AttackUsing = false;
            ImDown = false;
            DownMark = false;
            HeadDown = false;
            MainWeapons = false;
            SubWeaponsR = false;
            SubWeaponsL = false;
            MainWeaponOff = false;
            SubWeaponsRReady = false;
            SubWeaponsLReady = false;
            ShieldDown = false;
            TargetPoint.SetActive(true);

            HitAction = 0;
            HitTime = 0;
            hitPoints = startingHitPoints;
            armor = startingArmor;
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            rdcas = GetComponent<RegdollControllerAsoShiioshare>();
            rdcas.DisableRagdoll();
            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareDeath(false); //죽은 상태 되돌리기 전달
            GetComponent<ReverseCallAsoShiioshare>().enabled = true;
            TraceX call1 = transform.Find("Body1").GetComponent<TraceX>();
            call1.ShadowReset = true;
            Invoke("ReverseOff", 0.1f);
            animator.SetBool("Damage1 down, Aso Shiioshare", false);
            animator.SetBool("Damage2 down, Aso Shiioshare", false);
            transform.Find("Auto target").gameObject.SetActive(true);
            Shadow.SetActive(true);

            if (animator.GetFloat("Flame Death, Infector") > 0)
                animator.SetFloat("Flame Death, Infector", 0);
        }
        ScoreManager.instance.AllEnemyCnt(1);
    }

    void ReverseOff()
    {
        GetComponent<ReverseCallAsoShiioshare>().enabled = false;
        Death = false;
    }

    void Update()
    {
        //Debug.Log("HitAction : " + HitAction);

        if (KnockBackShot == true)
        {
            transform.Translate(Vector2.right * KnockBackSpeed * Time.deltaTime);

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

        if (tearAsoShiioshare.RightArmTop1 == true)
            FlameTakenArmTop1.SetActive(false);
        if (tearAsoShiioshare.RightArmTop1 == true && tearAsoShiioshare.RightArmDown1 == true)
            FlameTakenArmDown1.SetActive(false);
        if (tearAsoShiioshare.RightArmTop2 == true)
            FlameTakenArmTop2.SetActive(false);
        if (tearAsoShiioshare.RightArmTop2 == true && tearAsoShiioshare.RightArmDown2 == true)
            FlameTakenArmDown2.SetActive(false);
        if (tearAsoShiioshare.RightArmTop3 == true)
            FlameTakenArmTop3.SetActive(false);
        if (tearAsoShiioshare.RightArmTop3 == true && tearAsoShiioshare.RightArmDown3 == true)
            FlameTakenArmDown3.SetActive(false);
        if (tearAsoShiioshare.RightLegTop1 == true)
            FlameTakenLeg1Top.SetActive(false);
        if (tearAsoShiioshare.RightLegTop1 == true && tearAsoShiioshare.RightLegDown1 == true)
            FlameTakenLeg1Down.SetActive(false);
        if (tearAsoShiioshare.RightLegTop2 == true)
            FlameTakenLeg2Top.SetActive(false);
        if (tearAsoShiioshare.RightLegTop2 == true && tearAsoShiioshare.RightLegDown2 == true)
            FlameTakenLeg2Down.SetActive(false);

        if (FlameDeathing == true)
        {
            FlameTakenBody.SetActive(true);
            animator.SetBool("Flame body, Aso Shiioshare", true);
            if (tearAsoShiioshare.RightArmTop1 == false)
                FlameTakenArmTop1.SetActive(true);
            if (tearAsoShiioshare.RightArmTop1 == false && tearAsoShiioshare.RightArmDown1 == false)
                FlameTakenArmDown1.SetActive(true);
            if (tearAsoShiioshare.RightArmTop2 == false)
                FlameTakenArmTop2.SetActive(true);
            if (tearAsoShiioshare.RightArmTop2 == false && tearAsoShiioshare.RightArmDown2 == false)
                FlameTakenArmDown2.SetActive(true);
            if (tearAsoShiioshare.RightArmTop3 == false)
                FlameTakenArmTop3.SetActive(true);
            if (tearAsoShiioshare.RightArmTop3 == false && tearAsoShiioshare.RightArmDown3 == false)
                FlameTakenArmDown3.SetActive(true);
            animator.SetBool("Flame legs, Aso Shiioshare", true);
            if (tearAsoShiioshare.RightLegTop1 == false)
                FlameTakenLeg1Top.SetActive(true);
            if (tearAsoShiioshare.RightLegTop1 == false && tearAsoShiioshare.RightLegDown1 == false)
                FlameTakenLeg1Down.SetActive(true);
            if (tearAsoShiioshare.RightLegTop2 == false)
                FlameTakenLeg2Top.SetActive(true);
            if (tearAsoShiioshare.RightLegTop2 == false && tearAsoShiioshare.RightLegDown2 == false)
                FlameTakenLeg2Down.SetActive(true);
            animator.SetBool("Flame arm, Aso Shiioshare", true);
            FlameTakenHead.SetActive(true);
            animator.SetBool("Flame head, Aso Shiioshare", true);
        }

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
                VoicePrint = Random.Range(0, 2);

                if (VoicePrint == 0)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay8("Sound", Voice1);
                    }
                }
                else if (VoicePrint == 1)
                {
                    if (VoiceTime == 0)
                    {
                        VoiceTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay8("Sound", Voice2);
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
            if(DamageActionBool == true)
                StopCoroutine(damageAction);
            animator.SetBool("Damage1, Aso Shiioshare", false);
            animator.SetBool("Damage2, Aso Shiioshare", false);
        }
    }

    //적이 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if(ShieldDown == true)
        {
            if (Death == false)
            {
                while (true)
                {
                    if (HeadDown == false)
                        hitPoints = hitPoints - (damage / armor);
                    else
                        hitPoints = hitPoints - (damage * 2f);

                    if (BeamDamageAction > 0)
                    {
                        StartCoroutine(BeamAction());
                        TimeStemp += 0.025f;
                    }

                    if (ImHit == true)
                    {
                        ImHit = false;
                        GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
                        Destroy(BloodFloor, 10);
                    }

                    if (HitAction <= 0 && AttackUsing == false)
                    {
                        if (ImDown == false)
                        {
                            HitAction = DamageTime;
                            DamageActionBool = true;
                            damageAction = StartCoroutine(DamageAction());
                            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(true);
                        }
                        if (DownMark == true)
                        {
                            HitAction = DamageTime;
                            DamageActionDownBool = true;
                            damageAction2 = StartCoroutine(DamageAction2());
                            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(true);
                        }
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        if (Death == false)
                        {
                            if (FlameHitTime <= 0) //불에 붙지 않았을 경우
                            {
                                Death = true;

                                if (HeadDown == true)
                                {
                                    transform.Find("Aso Shiioshare Head").gameObject.SetActive(false);
                                    transform.Find("Aso Shiioshare Head tentacle1").gameObject.SetActive(false);
                                    transform.Find("Aso Shiioshare Head tentacle2").gameObject.SetActive(false);
                                    transform.Find("Aso Shiioshare Head tentacle3").gameObject.SetActive(false);
                                    GameObject Head1 = Instantiate(Head, Headpos.transform.position, Headpos.transform.rotation);
                                    Destroy(Head1, 30);
                                    transform.Find("Body1/Head body blood").gameObject.SetActive(true);
                                    StartCoroutine(bloodaction);
                                }

                                if (MainWeapons == false)
                                {
                                    gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareMainWeapon(true);
                                    GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
                                    Destroy(MW, 30);
                                }

                                if (SubWeaponsR == false && MainWeaponOff == true && SubWeaponsRReady == true)
                                {
                                    gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponR(true);
                                    GameObject SW = Instantiate(SubWeapon, SubWeaponLeftpos.transform.position, SubWeaponLeftpos.transform.rotation);
                                    Destroy(SW, 30);
                                }

                                if (SubWeaponsL == false && MainWeaponOff == true && SubWeaponsLReady == true)
                                {
                                    gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponL(true);
                                    GameObject SW = Instantiate(SubWeapon, SubWeaponRightpos.transform.position, SubWeaponRightpos.transform.rotation);
                                    Destroy(SW, 30);
                                }

                                int DeathVoiceRandom = Random.Range(0, 2);

                                if (DeathVoiceRandom == 0)
                                    SoundManager.instance.SFXPlay6("Sound", DeathVoice1);
                                else
                                    SoundManager.instance.SFXPlay6("Sound", DeathVoice2);

                                //GameObject.Find("Play Control/Player Vehicles/Auto turret/FBWS iris").GetComponent<AutoTurretSystem>().TargetDeadState = true;
                                transform.Find("Charge center1").gameObject.SetActive(false);
                                rdcas.ActiveRagdoll(); //RegdollControllerInfector스크립트의 ActiveRagdoll 메소드 호출
                                GetComponent<BehaviorAsoShiioshare>().AsoShiioshareDeath(true); //죽은 상태 전달
                                GetComponent<DeathCallAsoShiioshare>().enabled = true; //레그돌 담당 신체 파트에다 죽음 신호 전달
                                DeathRolling call = transform.Find("Body1").GetComponent<DeathRolling>(); //죽을 때 신체 회전
                                call.enabled = true;
                                TraceX call1 = transform.Find("Body1").GetComponent<TraceX>();
                                call1.DeathTransformTime = true; //죽었을 때의 위치 좌표 계산 스위치
                                call1.ShadowTime = 5f;
                                TargetPoint.SetActive(false);
                                transform.Find("Auto target").gameObject.SetActive(false);
                                Shadow.SetActive(false);
                                AttackLine.SetActive(false);

                                ScoreManager.instance.setScore(1);
                                ScoreManager.instance.set_Slorius_Score(1);
                                ScoreManager.instance.SloriusEliteCnt++;
                                ScoreManager.instance.DieCnt(1);
                                ScoreManager.instance.EnemyList.Remove(gameObject);

                                Invoke("Kill", 5f);
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

    void Kill()
    {
        rdcas.DisableRagdoll();
        GetComponent<BehaviorAsoShiioshare>().AsoShiioshareDeath(false); //죽은 상태 되돌리기 전달
        GetComponent<DeathCallAsoShiioshare>().layerTime = 0;
        GetComponent<DeathCallAsoShiioshare>().enabled = false;
        FlameTakenBody.SetActive(false);
        FlameTakenArmTop1.SetActive(false);
        FlameTakenArmDown1.SetActive(false);
        FlameTakenArmTop2.SetActive(false);
        FlameTakenArmDown2.SetActive(false);
        FlameTakenArmTop3.SetActive(false);
        FlameTakenArmDown3.SetActive(false);
        FlameTakenLeg1Top.SetActive(false);
        FlameTakenLeg1Down.SetActive(false);
        FlameTakenLeg2Top.SetActive(false);
        FlameTakenLeg2Down.SetActive(false);
        FlameTakenHead.SetActive(false);
        KillCharacter();
    }

    public IEnumerator DamageAction()
    {
        DamageType = Random.Range(1, 3);

        animator.SetBool("Damage1, Aso Shiioshare", false);
        animator.SetBool("Damage2, Aso Shiioshare", false);

        if (DamageType == 1)
        {
            SoundManager.instance.SFXPlay4("Sound", DamageVoice1);
            animator.SetBool("Damage1, Aso Shiioshare", true);
            yield return new WaitForSeconds(DamageTime);
            animator.SetBool("Damage1, Aso Shiioshare", false);
            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(false);
        }
        else if (DamageType == 2)
        {
            SoundManager.instance.SFXPlay4("Sound", DamageVoice2);
            animator.SetBool("Damage2, Aso Shiioshare", true);
            yield return new WaitForSeconds(DamageTime);
            animator.SetBool("Damage2, Aso Shiioshare", false);
            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(false);
        }
    }

    public IEnumerator DamageAction2()
    {
        DamageType = Random.Range(1, 3);

        animator.SetBool("Damage1 down, Aso Shiioshare", false);
        animator.SetBool("Damage2 down, Aso Shiioshare", false);

        if (DamageType == 1)
        {
            SoundManager.instance.SFXPlay4("Sound", DamageVoice1);
            animator.SetBool("Damage1 down, Aso Shiioshare", true);
            yield return new WaitForSeconds(DamageTime);
            animator.SetBool("Damage1 down, Aso Shiioshare", false);
            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(false);
        }
        else if (DamageType == 2)
        {
            SoundManager.instance.SFXPlay4("Sound", DamageVoice2);
            animator.SetBool("Damage2 down, Aso Shiioshare", true);
            yield return new WaitForSeconds(DamageTime);
            animator.SetBool("Damage2 down, Aso Shiioshare", false);
            GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImHit(false);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Delete Area"))
        {
            Death = true;
            TargetPoint.SetActive(false);
            transform.Find("Charge center1").gameObject.SetActive(false);
            transform.Find("Auto target").gameObject.SetActive(false);
            Shadow.SetActive(false);
            AttackLine.SetActive(false);
            ScoreManager.instance.DieCnt(1);
            ScoreManager.instance.EnemyList.Remove(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    //화염에 의한 죽음
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
        var Flame11 = FlameEffect11.emission;
        var Flame12 = FlameEffect12.emission;
        var Flame13 = FlameEffect13.emission;
        var Flame14 = FlameEffect14.emission;
        var Flame15 = FlameEffect15.emission;

        GetComponent<BehaviorAsoShiioshare>().FlameDeath = true;
        FlameDeathing = true;

        yield return new WaitForSeconds(FlameDeathTime);

        GetComponent<BehaviorAsoShiioshare>().AsoShiioshareDeath(true); //죽은 상태 전달
        GetComponent<BehaviorAsoShiioshare>().FlameDeath = false;
        FlameDeathing = false;

        if (HeadDown == true)
        {
            transform.Find("Aso Shiioshare Head").gameObject.SetActive(false);
            transform.Find("Aso Shiioshare Head tentacle1").gameObject.SetActive(false);
            transform.Find("Aso Shiioshare Head tentacle2").gameObject.SetActive(false);
            transform.Find("Aso Shiioshare Head tentacle3").gameObject.SetActive(false);
            GameObject Head1 = Instantiate(Head, Headpos.transform.position, Headpos.transform.rotation);
            Destroy(Head1, 30);
            transform.Find("Body1/Head body blood").gameObject.SetActive(true);
            StartCoroutine(bloodaction);
        }

        if (MainWeapons == false)
        {
            gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareMainWeapon(true);
            GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
            Destroy(MW, 30);
        }

        if (SubWeaponsR == false && MainWeaponOff == true && SubWeaponsRReady == true)
        {
            gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponR(true);
            GameObject SW = Instantiate(SubWeapon, SubWeaponLeftpos.transform.position, SubWeaponLeftpos.transform.rotation);
            Destroy(SW, 30);
        }

        if (SubWeaponsL == false && MainWeaponOff == true && SubWeaponsLReady == true)
        {
            gameObject.GetComponent<TearAsoShiioshare>().AsoShiioshareSubWeaponL(true);
            GameObject SW = Instantiate(SubWeapon, SubWeaponRightpos.transform.position, SubWeaponRightpos.transform.rotation);
            Destroy(SW, 30);
        }

        int DeathVoiceRandom = Random.Range(0, 2);

        if (DeathVoiceRandom == 0)
            SoundManager.instance.SFXPlay6("Sound", DeathVoice1);
        else
            SoundManager.instance.SFXPlay6("Sound", DeathVoice2);

        transform.Find("Charge center1").gameObject.SetActive(false);
        rdcas.ActiveRagdoll(); //RegdollControllerInfector스크립트의 ActiveRagdoll 메소드 호출
        GetComponent<DeathCallAsoShiioshare>().enabled = true; //레그돌 담당 신체 파트에다 죽음 신호 전달
        DeathRolling call = transform.Find("Body1").GetComponent<DeathRolling>(); //죽을 때 신체 회전
        call.enabled = true;
        TraceX call1 = transform.Find("Body1").GetComponent<TraceX>();
        call1.DeathTransformTime = true; //죽었을 때의 위치 좌표 계산 스위치
        call1.ShadowTime = 7f;
        TargetPoint.SetActive(false);
        transform.Find("Auto target").gameObject.SetActive(false);
        Shadow.SetActive(false);
        AttackLine.SetActive(false);

        ScoreManager.instance.setScore(1);
        ScoreManager.instance.set_Slorius_Score(1);
        ScoreManager.instance.SloriusEliteCnt++;
        ScoreManager.instance.DieCnt(1);
        ScoreManager.instance.EnemyList.Remove(gameObject);

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
        Flame11.rateOverTime = 0;
        Flame12.rateOverTime = 0;
        Flame13.rateOverTime = 0;
        Flame14.rateOverTime = 0;
        Flame15.rateOverTime = 0;

        animator.SetBool("Flame body, Aso Shiioshare", false);
        animator.SetBool("Flame legs, Aso Shiioshare", false);
        animator.SetBool("Flame arm, Aso Shiioshare", false);
        animator.SetBool("Flame head, Aso Shiioshare", false);
    }
}