using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RobotMove : MonoBehaviour
{
    ObjectManager objectManager;
    HTACController hTACController;

    Animator animator;
    Rigidbody2D rb2D;

    Vector2 movement = new Vector2(); //벡터2 생성 선언

    public Joystick joyStick;
    public GameObject MoveController1;
    public GameObject MoveController2;
    public GameObject MoveController3;
    public GameObject MoveController4;
    public GameObject MoveController5;
    public GameObject MoveController6;
    public GameObject ActiveMoveController1;
    public GameObject ActiveMoveController2;
    public Image SpeedBar;
    public Joystick GunjoyStick;
    public GameObject AttackController1;
    public GameObject AttackController2;
    public GameObject AttackController3;
    public GameObject AttackController4;
    public GameObject ActiveAttackController1;
    public GameObject ActiveAttackController2;
    public GameObject AnimationUIDash;
    public GameObject AnimationUIVehicleHUD;
    public bool FireJoystick;

    public float Speed; //플레이어 기본 이동 속도.
    private float XtargetSpeed; //플레이어의 기본 속도에 맞출 목표 속도.
    private float YtargetSpeed;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashCooldown = 0.5f; // 대쉬 쿨
    public float DashCoolTime; // 대쉬가 채워지는 쿨 
    public float DashTime;
    private float SpeedView;
    public Text DashCountTxt;

    public int DashCount; // 대쉬 카운트
    private int DashCountInGame;
    public int FireJoystickType; //무기별 전달용 사격 컨트롤러
    public int damage = 1000;
    public int dashDamage = 1000;

    private bool BeamFiring = false; //X축 반전 사용 여부
    public bool MoveStop = false;
    private bool Dashing = false; //대쉬를 한번만 할 수 있도록 조취
    private bool DashOn = false;
    public bool isDash; // 버튼 입력용 bool값
    public bool VehicleActive; //차량에 탑승했을 때 이동 및 행동 금지용 스위치
    private bool Click;

    public GameObject mainBoostPrefab; //메인 부스트 프리팹
    public Transform mainBoostPos; //메인 부스트 좌표
    public GameObject subBoostPrefab1; //서브 부스트1 프리팹
    public Transform subBoostPos1; //서브 부스트1 좌표
    public GameObject subBoostPrefab2; //서브 부스트2 프리팹
    public Transform subBoostPos2; //서브 부스트2 좌표
    public GameObject boostCloudPrefab; //부스트 연기 프리팹

    public GameObject Barrier;
    public Transform BarrierPos; //총알 생성 좌표
    public int BarrierCnt; // 플라즈마포 스킬 카운트  
    public float BarrierCoolTime; // 플라즈마포가 채워지는 쿨 
    public float BarrierTime;
    public bool isBarrier;
    public bool BarrierBool;

    public GameObject LandSound;
    public GameObject LandSound2;

    public AudioClip DashSound;
    public AudioClip Beep1;
    public AudioClip Beep2;

    void LeftOnRight()
    {
        //이동 조이스틱
        if (joyStick.Horizontal < 0 && BeamFiring == false)
        {
            joyStick.GetComponent<Animator>().SetBool("Move vehicle, Move joystick", true);
            MoveController1.GetComponent<RotationUI>().isMove = true;
            MoveController2.GetComponent<RotationUI>().isMove = true;
            MoveController3.GetComponent<RotationUI>().isMove = true;
            MoveController4.GetComponent<RotationUI>().isMove = true;
            MoveController5.GetComponent<RotationUI>().isMove = true;
            MoveController6.GetComponent<RotationUI>().isMove = true;
            ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = true;
            ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (joyStick.Horizontal > 0 && BeamFiring == false)
        {
            joyStick.GetComponent<Animator>().SetBool("Move vehicle, Move joystick", true);
            MoveController1.GetComponent<RotationUI>().isMove = true;
            MoveController2.GetComponent<RotationUI>().isMove = true;
            MoveController3.GetComponent<RotationUI>().isMove = true;
            MoveController4.GetComponent<RotationUI>().isMove = true;
            MoveController5.GetComponent<RotationUI>().isMove = true;
            MoveController6.GetComponent<RotationUI>().isMove = true;
            ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = true;
            ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            joyStick.GetComponent<Animator>().SetBool("Move vehicle, Move joystick", false);
            MoveController1.GetComponent<RotationUI>().isMove = false;
            MoveController2.GetComponent<RotationUI>().isMove = false;
            MoveController3.GetComponent<RotationUI>().isMove = false;
            MoveController4.GetComponent<RotationUI>().isMove = false;
            MoveController5.GetComponent<RotationUI>().isMove = false;
            MoveController6.GetComponent<RotationUI>().isMove = false;
            ActiveMoveController1.GetComponent<RotationActiveUI>().isMove = false;
            ActiveMoveController2.GetComponent<RotationActiveUI>().isMove = false;
        }

        //사격 조이스틱
        if (GunjoyStick.Horizontal <= -.2f && BeamFiring == false)
        {
            GunjoyStick.GetComponent<Animator>().SetBool("Move vehicle, Attack joystick", true);
            AttackController1.GetComponent<RotationUI>().isMove = true;
            AttackController2.GetComponent<RotationUI>().isMove = true;
            AttackController3.GetComponent<RotationUI>().isMove = true;
            AttackController4.GetComponent<RotationUI>().isMove = true;
            ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = true;
            ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (GunjoyStick.Horizontal >= .2f && BeamFiring == false)
        {
            GunjoyStick.GetComponent<Animator>().SetBool("Move vehicle, Attack joystick", true);
            AttackController1.GetComponent<RotationUI>().isMove = true;
            AttackController2.GetComponent<RotationUI>().isMove = true;
            AttackController3.GetComponent<RotationUI>().isMove = true;
            AttackController4.GetComponent<RotationUI>().isMove = true;
            ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = true;
            ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            GunjoyStick.GetComponent<Animator>().SetBool("Move vehicle, Attack joystick", false);
            AttackController1.GetComponent<RotationUI>().isMove = false;
            AttackController2.GetComponent<RotationUI>().isMove = false;
            AttackController3.GetComponent<RotationUI>().isMove = false;
            AttackController4.GetComponent<RotationUI>().isMove = false;
            ActiveAttackController1.GetComponent<RotationActiveUI>().isMove = false;
            ActiveAttackController2.GetComponent<RotationActiveUI>().isMove = false;
        }

        if (GunjoyStick.Horizontal <= -.9f && BeamFiring == false)
        {
            GunjoyStick.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 10);

            if (FireJoystickType == 1)
                hTACController.isGun = true; //전투로봇 HTAC
        }
        else if (GunjoyStick.Horizontal >= .9f && BeamFiring == false)
        {
            GunjoyStick.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 10);

            if (FireJoystickType == 1)
                hTACController.isGun = true;
        }
        else
        {
            GunjoyStick.GetComponent<Animator>().SetFloat("Fire, Attack joystick", 0);

            if (FireJoystickType == 1)
                hTACController.isGun = false;
        }
    }

    public void Entering(bool online)
    {
        if (online == true)
        {
            if (animator.GetBool("Take wait, MBCA-79") == true)
                animator.SetBool("Take wait, MBCA-79", false);
            animator.SetBool("Take, MBCA-79", true);
        }
        else
            animator.SetBool("Take, MBCA-79", false);
    }

    public void Exiting(bool offline)
    {
        if (offline == true)
        {
            animator.SetBool("Take off, MBCA-79", true);
        }
        else
        {
            animator.SetBool("Take wait, MBCA-79", true);
            animator.SetBool("Take off, MBCA-79", false);
        }
    }

    public void FireUp()
    {
        FireJoystick = false;
    }

    public void FireDown()
    {
        FireJoystick = true;
    }

    public void Start()
    {
        hTACController = FindObjectOfType<HTACController>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        DashCountInGame = DashCount;
        activeMoveSpeed = Speed;
    }

    public void DashUp()
    {
        if (Click == true)
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, MBCA-79 dash", false);
        Click = false;
    }

    public void DashDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        SoundManager.instance.SFXPlay2("Sound", Beep2);
        AnimationUIDash.GetComponent<Animator>().SetBool("Click, MBCA-79 dash", true);
    }

    public void DashEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            SoundManager.instance.SFXPlay2("Sound", Beep2);
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, MBCA-79 dash", true);
        }
    }

    public void DashExit()
    {
        if (Click == true)
            AnimationUIDash.GetComponent<Animator>().SetBool("Click, MBCA-79 dash", false);
    }

    public void DashClick()
    {
        if (Dashing == false && DashCountInGame > 0)
            if (XtargetSpeed != 0 || YtargetSpeed != 0)
                isDash = true;
    }

    public void BarrierDown()
    {
        isBarrier = true;
    }

    public void BarrierUp()
    {
        isBarrier = false;
    }

    public void Stop(bool Stopbool)
    {
        if (Stopbool == true)
        {
            MoveStop = true;
        }
        else
        {
            MoveStop = false;
        }
    }

    //실시간 업데이트
    public void Update()
    {
        if (MoveStop == true && GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SpeedBar.fillAmount = SpeedView / 70;

            GetComponent<Animator>().SetBool("Landing, MBCA-79", false);
            LeftOnRight(); //조이스틱 애니메이션
            StartCoroutine(UpdateState()); //상하좌우 동작 애니메이션
            MoveCharacter(); //상하좌우 키입력
            if (VehicleActive == true)
                Dash(); // 대쉬
        }
        else if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == true)
        {
            if (MoveStop == true)
                GetComponent<Animator>().SetBool("Landing, MBCA-79", false);
            SpeedBar.fillAmount = 0;
            movement.x = 0;
            movement.y = 0;

            //사격 중지
            GetComponent<HTACController>().isGun = false;
            FireJoystick = false;
        }

        UpdateDashText();

        //BarrierTime += Time.deltaTime;
        //BarrierSkill();
        //BarrierSkillCool();
    }

    private void UpdateDashText()
    {
        DashCountTxt.text = string.Format("{0}", DashCountInGame);
    }

    void Dash()
    {
        if(isDash)
        {
            DashOn = true;
            isDash = false;
            //gameObject.layer = 11;
            Dashing = true;
            DashCountInGame--;
            activeMoveSpeed = dashSpeed;
            SoundManager.instance.SFXPlay9("Sound", DashSound);
            StartCoroutine(dashAnimation()); //대쉬 애니메이션
            DashThruster(); //대쉬 부스터
        }

        if (DashOn == true && activeMoveSpeed > 0)
        {
            activeMoveSpeed -= dashSpeed * 0.5f / dashSpeed;
        }

        //대쉬 쿨타임 회복을 위해 시간 가동
        if (DashCountInGame < DashCount)
        {
            AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle start, MBCA-79 dash", true);
            AnimationUIDash.GetComponent<Animator>().SetFloat("Cool time, MBCA-79 dash", 1 / DashCoolTime);
            DashTime += Time.deltaTime;

            if (DashCountInGame == 0)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time start, MBCA-79 dash", true);
                //AnimationUIDash.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
            }
        }

        //대쉬 시간이 찼을 경우, 대쉬를 하나 채운다.
        if (DashTime >= DashCoolTime)
        {
            DashTime = 0;
            DashCountInGame++;
            AnimationUIDash.GetComponent<Animator>().SetBool("View count complete, MBCA-79 dash", true);
            AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle count, MBCA-79 dash", true);
            Invoke("ViewCountComplete", 0.5f);

            if (DashCountInGame > 0)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time action end, MBCA-79 dash", true);
                Invoke("AfterEndCycle", 0.5f);
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time start, MBCA-79 dash", false);
                //AnimationUIDash.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
            }
            if (DashCountInGame == DashCount)
            {
                AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle start, MBCA-79 dash", false);
                AnimationUIDash.GetComponent<Animator>().SetFloat("Cool time, MBCA-79 dash", 0);
            }
        }
    }

    void AfterEndCycle()
    {
        AnimationUIDash.GetComponent<Animator>().SetBool("Cool time action end, MBCA-79 dash", false);
    }

    void ViewCountComplete()
    {
        AnimationUIDash.GetComponent<Animator>().SetBool("View count complete, MBCA-79 dash", false);
        AnimationUIDash.GetComponent<Animator>().SetBool("Cool time cycle count, MBCA-79 dash", false);
    }

    //대쉬 부스터 출력
    void DashThruster()
    {
        GameObject MainBoost = Instantiate(mainBoostPrefab, mainBoostPos.position, mainBoostPos.rotation);
        GameObject SubBoost1 = Instantiate(subBoostPrefab1, subBoostPos1.position, subBoostPos1.rotation);
        GameObject SubBoost2 = Instantiate(subBoostPrefab2, subBoostPos2.position, subBoostPos2.rotation);
        GameObject BoostCloud = Instantiate(boostCloudPrefab, mainBoostPos.position, mainBoostPos.rotation);

        Destroy(MainBoost, 3);
        Destroy(SubBoost1, 3);
        Destroy(SubBoost2, 3);
        Destroy(BoostCloud, 3);
    }

    //대쉬 애니메이션 출력
    IEnumerator dashAnimation()
    {
        animator.SetBool("Dash, MBCA-79", true);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Dash, Vehicle HUD", true);
        joyStick.GetComponent<Animator>().SetBool("Dash vehicle, Move joystick", true);
        AnimationUIDash.GetComponent<Animator>().SetBool("Activated, MBCA-79 dash", true);
        activeMoveSpeed = dashSpeed;
        yield return new WaitForSeconds(0.85f);
        animator.SetBool("Dash, MBCA-79", false);
        AnimationUIVehicleHUD.GetComponent<Animator>().SetBool("Dash, Vehicle HUD", false);
        joyStick.GetComponent<Animator>().SetBool("Dash vehicle, Move joystick", false);
        AnimationUIDash.GetComponent<Animator>().SetBool("Activated, MBCA-79 dash", false);
        activeMoveSpeed = Speed;
        DashOn = false;
        yield return new WaitForSeconds(dashCooldown);
        Dashing = false;
    }

    //상하좌우 키입력
    public void MoveCharacter()
    {
        XtargetSpeed = joyStick.Horizontal * activeMoveSpeed;
        YtargetSpeed = joyStick.Vertical * activeMoveSpeed;

        movement.x = XtargetSpeed * 0.7f;
        movement.y = YtargetSpeed * 0.4f;

        if (joyStick.Horizontal > 0 && joyStick.Vertical > 0)
        {
            if (joyStick.Horizontal > joyStick.Vertical)
                SpeedView = XtargetSpeed;
            else
                SpeedView = YtargetSpeed;
        }
        else if (joyStick.Horizontal > 0 && joyStick.Vertical < 0)
        {
            if (joyStick.Horizontal > -joyStick.Vertical)
                SpeedView = XtargetSpeed;
            else
                SpeedView = YtargetSpeed * -1;
        }
        else if (joyStick.Horizontal < 0 && joyStick.Vertical < 0)
        {
            if (-joyStick.Horizontal > -joyStick.Vertical)
                SpeedView = XtargetSpeed * -1;
            else
                SpeedView = YtargetSpeed * -1;
        }
        else if (joyStick.Horizontal < 0 && joyStick.Vertical > 0)
        {
            if (-joyStick.Horizontal > joyStick.Vertical)
                SpeedView = XtargetSpeed * -1;
            else
                SpeedView = YtargetSpeed;
        }
        else
            SpeedView = 0;

        transform.position += Vector3.right * XtargetSpeed * 0.7f * Time.deltaTime;
        transform.position += Vector3.up * YtargetSpeed * 0.4f * Time.deltaTime;
    }

    //가속도 처리
    private float IncrementTowards(float n, float target, float a)
    {
        if(n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n); //목표속도에서 현재속도를 뺀다.
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    //상하좌우 동작 애니메이션
    IEnumerator UpdateState()
    {
        Vector3 v1 = transform.position;
        yield return new WaitForSeconds(0.1f);

        if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //오른쪽을 쳐다보고 있을 때
        {
            animator.SetBool("Move, MBCA-79", true);

            if (v1.x > transform.position.x) //후진
                animator.SetFloat("Move Type", -1f);
            else //전진
                animator.SetFloat("Move Type", 1f);
        }
        else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //왼쪽을 쳐다보고 있을 때
        {
            animator.SetBool("Move, MBCA-79", true);

            if (v1.x > transform.position.x) //후진
                animator.SetFloat("Move Type", 1f);
            else //전진
                animator.SetFloat("Move Type", -1f);
        }
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0)) //움직임 벡터가 여전히 0에 있는지 확인한다. 즉, 플레이어 동작 입력이 존재하지 않는 경우이다.
        {
            animator.SetBool("Move, MBCA-79", false);
            LandSound.GetComponent<RobotMoveLand1>().TurnOff = false;
            LandSound2.GetComponent<RobotMoveLand1>().TurnOff = true;
        }
    }

    void BarrierSkill() // 베리어 B
    {
       //if (Input.GetKeyDown(KeyCode.B))
       if(isBarrier)
        {
            if (BarrierCnt > 0 )
            {
                BarrierBool = true;
                Invoke("BarrierDelay", 2f);
                BarrierCnt--;               
            }
        }
    }

    void BarrierDelay()
    {
        GameObject BK = Instantiate(Barrier, BarrierPos.transform.position, BarrierPos.transform.rotation);
        BarrierBool = false;
    }

    void BarrierSkillCool()
    {
        if (BarrierTime >= BarrierCoolTime)
        {
            BarrierCnt++;
            BarrierTime = 0;
        }
        else if (BarrierCnt > 0)
        {
            BarrierTime = 0;
        }
    }
}