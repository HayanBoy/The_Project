using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RobotMove : MonoBehaviour
{
    ObjectManager objectManager;
    HTACController hTACController;

    Animator animator;
    Rigidbody2D rb2D;

    Vector2 movement = new Vector2(); //����2 ���� ����

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

    public float Speed; //�÷��̾� �⺻ �̵� �ӵ�.
    private float XtargetSpeed; //�÷��̾��� �⺻ �ӵ��� ���� ��ǥ �ӵ�.
    private float YtargetSpeed;
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashCooldown = 0.5f; // �뽬 ��
    public float DashCoolTime; // �뽬�� ä������ �� 
    public float DashTime;
    private float SpeedView;
    public Text DashCountTxt;

    public int DashCount; // �뽬 ī��Ʈ
    private int DashCountInGame;
    public int FireJoystickType; //���⺰ ���޿� ��� ��Ʈ�ѷ�
    public int damage = 1000;
    public int dashDamage = 1000;

    private bool BeamFiring = false; //X�� ���� ��� ����
    public bool MoveStop = false;
    private bool Dashing = false; //�뽬�� �ѹ��� �� �� �ֵ��� ����
    private bool DashOn = false;
    public bool isDash; // ��ư �Է¿� bool��
    public bool VehicleActive; //������ ž������ �� �̵� �� �ൿ ������ ����ġ
    private bool Click;

    public GameObject mainBoostPrefab; //���� �ν�Ʈ ������
    public Transform mainBoostPos; //���� �ν�Ʈ ��ǥ
    public GameObject subBoostPrefab1; //���� �ν�Ʈ1 ������
    public Transform subBoostPos1; //���� �ν�Ʈ1 ��ǥ
    public GameObject subBoostPrefab2; //���� �ν�Ʈ2 ������
    public Transform subBoostPos2; //���� �ν�Ʈ2 ��ǥ
    public GameObject boostCloudPrefab; //�ν�Ʈ ���� ������

    public GameObject Barrier;
    public Transform BarrierPos; //�Ѿ� ���� ��ǥ
    public int BarrierCnt; // �ö���� ��ų ī��Ʈ  
    public float BarrierCoolTime; // �ö������ ä������ �� 
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
        //�̵� ���̽�ƽ
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

        //��� ���̽�ƽ
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
                hTACController.isGun = true; //�����κ� HTAC
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

    //�ǽð� ������Ʈ
    public void Update()
    {
        if (MoveStop == true && GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == false)
        {
            SpeedBar.fillAmount = SpeedView / 70;

            GetComponent<Animator>().SetBool("Landing, MBCA-79", false);
            LeftOnRight(); //���̽�ƽ �ִϸ��̼�
            StartCoroutine(UpdateState()); //�����¿� ���� �ִϸ��̼�
            MoveCharacter(); //�����¿� Ű�Է�
            if (VehicleActive == true)
                Dash(); // �뽬
        }
        else if (GameObject.Find("Play Control/Player").GetComponent<Movement>().MissionComplete == true)
        {
            if (MoveStop == true)
                GetComponent<Animator>().SetBool("Landing, MBCA-79", false);
            SpeedBar.fillAmount = 0;
            movement.x = 0;
            movement.y = 0;

            //��� ����
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
            StartCoroutine(dashAnimation()); //�뽬 �ִϸ��̼�
            DashThruster(); //�뽬 �ν���
        }

        if (DashOn == true && activeMoveSpeed > 0)
        {
            activeMoveSpeed -= dashSpeed * 0.5f / dashSpeed;
        }

        //�뽬 ��Ÿ�� ȸ���� ���� �ð� ����
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

        //�뽬 �ð��� á�� ���, �뽬�� �ϳ� ä���.
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

    //�뽬 �ν��� ���
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

    //�뽬 �ִϸ��̼� ���
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

    //�����¿� Ű�Է�
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

    //���ӵ� ó��
    private float IncrementTowards(float n, float target, float a)
    {
        if(n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n); //��ǥ�ӵ����� ����ӵ��� ����.
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    //�����¿� ���� �ִϸ��̼�
    IEnumerator UpdateState()
    {
        Vector3 v1 = transform.position;
        yield return new WaitForSeconds(0.1f);

        if (transform.rotation.y == 0 && movement.x != 0 && movement.y != 0) //�������� �Ĵٺ��� ���� ��
        {
            animator.SetBool("Move, MBCA-79", true);

            if (v1.x > transform.position.x) //����
                animator.SetFloat("Move Type", -1f);
            else //����
                animator.SetFloat("Move Type", 1f);
        }
        else if (transform.rotation.y != 0 && movement.x != 0 && movement.y != 0) //������ �Ĵٺ��� ���� ��
        {
            animator.SetBool("Move, MBCA-79", true);

            if (v1.x > transform.position.x) //����
                animator.SetFloat("Move Type", 1f);
            else //����
                animator.SetFloat("Move Type", -1f);
        }
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0)) //������ ���Ͱ� ������ 0�� �ִ��� Ȯ���Ѵ�. ��, �÷��̾� ���� �Է��� �������� �ʴ� ����̴�.
        {
            animator.SetBool("Move, MBCA-79", false);
            LandSound.GetComponent<RobotMoveLand1>().TurnOff = false;
            LandSound2.GetComponent<RobotMoveLand1>().TurnOff = true;
        }
    }

    void BarrierSkill() // ������ B
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