using System.Collections;
using UnityEngine;

public class TearCrossfa : MonoBehaviour
{
    HealthAtroCrossfa healthAtroCrossfa;

    Animator animator;

    public bool TearPartByOneShot = false; //샷건 무기에 한번 타격을 받았을 때, 신체가 훼손되기 위한 스위치
    private bool MLBDown = false; //미사일포대가 무력화되었을 때의 스위치
    private bool MachinegunDown = false; //기관포가 무력화되었을 때의 스위치
    private bool Leg1Down = false; //1번 다리의 무력화를 알리는 스위치
    private bool Leg2Down = false; //2번 다리의 무력화를 알리는 스위치
    private bool Leg1DownCut = false;
    private bool Leg2DownCut = false;
    public bool RightLegTop1 = false;
    public bool RightLegTop2 = false;
    public bool RightLegDown1 = false;
    public bool RightLegDown2 = false;

    bool Direction;
    public int LargeThrow;

    public float MLBHitPoint; //미사일 포대 체력
    private float MLBHitPoint2; //리스폰용 미사일 포대체력
    public float MachinegunHitPoint; //기관포 체력
    private float MachinegunHitPoint2; //리스폰용 기관포 체력
    public float LegHitPoint; //다리 체력
    private float LegHitPoint2; //리스폰용 체력
    private int TearCount; //찢김 여부
    private int TearPartCount; //랜덤으로 팔이 날아가는 변수
    private int TearArmPart; //팔 상단 및 하단의 날아가는 변수

    public Transform BlueExplosionPos;
    GameObject BlueExplosion;

    public GameObject LeftLeg;
    public Transform LeftLegpos;
    public GameObject LeftLegDown;
    public Transform LeftLegDownpos;
    public GameObject LeftLegTop;
    public Transform LeftLegToppos;
    public GameObject RightLeg;
    public Transform RightLegpos;
    public GameObject RightLegDown;
    public Transform RightLegDownpos;
    public GameObject RightLegTop;
    public Transform RightLegToppos;

    void Start()
    {
        healthAtroCrossfa = FindObjectOfType<HealthAtroCrossfa>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
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

        TearPartByOneShot = false;
        MLBDown = false;
        MachinegunDown = false;
        Leg1Down = false;
        Leg2Down = false;
        Leg1DownCut = false;
        Leg2DownCut = false;
        RightLegTop1 = false;
        RightLegTop2 = false;
        RightLegDown1 = false;
        RightLegDown2 = false;
        Direction = false;

        TearCount = 0;
        TearPartCount = 0;
        TearArmPart = 0;
        LargeThrow = 0;

        LegHitPoint = GetComponent<HealthAtroCrossfa>().startingHitPoints / 3;
        MLBHitPoint = GetComponent<HealthAtroCrossfa>().startingHitPoints / 3;
        MachinegunHitPoint = GetComponent<HealthAtroCrossfa>().startingHitPoints / 3;
        LegHitPoint2 = LegHitPoint;
        MLBHitPoint2 = MLBHitPoint;
        MachinegunHitPoint2 = MachinegunHitPoint;
    }

    public void SetDirection(bool Boolean)
    {
        Direction = Boolean;
    }

    //미사일 포대에게 데미지 전달
    public IEnumerator MLBDamage(int damage, float interval)
    {
        if (MLBDown == false)
        {
            while (true)
            {
                MLBHitPoint2 = MLBHitPoint2 - damage;

                if (MLBHitPoint2 <= float.Epsilon)
                {
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
                    transform.Find("Body_1/NarSyr-Haicross 13 missile").gameObject.SetActive(false);
                    gameObject.transform.GetComponent<BehaviourAtroCrossfa>().MLBDown = true;
                    gameObject.transform.GetComponent<HealthAtroCrossfa>().MissileLaunchBaseDown = true;
                    gameObject.transform.GetComponent<HealthAtroCrossfa>().PartDeathMLB();
                    MLBDown = true;
                    healthAtroCrossfa.TearOn = true;
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
        }
    }

    //기관포에게 데미지 전달
    public IEnumerator MachinegunDamage(int damage, float interval)
    {
        if (MachinegunDown == false)
        {
            while (true)
            {
                MachinegunHitPoint2 = MachinegunHitPoint2 - damage;

                if (MachinegunHitPoint2 <= float.Epsilon)
                {
                    if (animator.GetBool("Gun rolling1, Atro-Crossfa 390") == true)
                        animator.SetBool("Gun rolling1, Atro-Crossfa 390", false);
                    if (animator.GetBool("Gun rolling2, Atro-Crossfa 390") == true)
                        animator.SetBool("Gun rolling2, Atro-Crossfa 390", false);
                    if (animator.GetFloat("Gun rolling start, Atro-Crossfa 390") != 0)
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);
                    gameObject.transform.GetComponent<BehaviourAtroCrossfa>().MachinegunDown = true;
                    gameObject.transform.GetComponent<HealthAtroCrossfa>().MachinegunDown = true;
                    gameObject.transform.GetComponent<HealthAtroCrossfa>().PartDeathMachinegun();
                    MachinegunDown = true;
                    healthAtroCrossfa.TearOn = true;
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
        }
    }

    //다리에게 데미지 전달
    public IEnumerator LegDamage(int damage, float interval)
    {
        if (TearPartByOneShot == false)
        {
            while (true)
            {
                LegHitPoint2 = LegHitPoint2 - damage;

                if (LegHitPoint2 <= LegHitPoint * 0.6f)
                {
                    TearCount = Random.Range(0, 2);

                    if (TearCount == 1)
                    {
                        TearPartCount = Random.Range(0, 6);

                        if (TearPartCount == 1)
                        {
                            TearArmPart = Random.Range(1, 3);

                            if (TearArmPart == 1 && RightLegTop1 == false)
                            {
                                transform.Find("Right leg top").gameObject.SetActive(false); //다리 상단 잘림
                                transform.Find("Right leg top knee").gameObject.SetActive(false);
                                transform.Find("Right leg down1").gameObject.SetActive(false);
                                transform.Find("Right leg down2").gameObject.SetActive(false);
                                transform.Find("Right leg down foot").gameObject.SetActive(false);
                                healthAtroCrossfa.TearOn = true;
                                RightLegTop1 = true;
                                Leg1Down = true;
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);

                                if (Leg1DownCut == false)
                                {
                                    GameObject RL = Instantiate(RightLeg, RightLegpos.transform.position, RightLegpos.transform.rotation);
                                    RL.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        RL.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        RL.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(RL, 30);
                                }
                                else
                                {
                                    GameObject RLT = Instantiate(RightLegTop, RightLegToppos.transform.position, RightLegToppos.transform.rotation);
                                    RLT.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        RLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        RLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(RLT, 30);
                                }
                            }
                            else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                            {
                                transform.Find("Right leg down1").gameObject.SetActive(false); //다리 하단 잘림
                                transform.Find("Right leg down2").gameObject.SetActive(false);
                                transform.Find("Right leg down foot").gameObject.SetActive(false);
                                healthAtroCrossfa.TearOn = true;
                                RightLegDown1 = true;
                                Leg1DownCut = true;
                                Leg1Down = true;
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                                RLD.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                if (Direction == false)
                                    RLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                else
                                    RLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                Destroy(RLD, 30);
                            }
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
                        }
                        else if (TearPartCount == 2)
                        {
                            TearArmPart = Random.Range(1, 3);

                            if (TearArmPart == 1 && RightLegTop2 == false)
                            {
                                transform.Find("Left leg top").gameObject.SetActive(false); //다리 상단 잘림
                                transform.Find("Left leg top knee").gameObject.SetActive(false);
                                transform.Find("Left leg down1").gameObject.SetActive(false);
                                transform.Find("Left leg down2").gameObject.SetActive(false);
                                transform.Find("Left leg down foot").gameObject.SetActive(false);
                                healthAtroCrossfa.TearOn = true;
                                RightLegTop2 = true;
                                Leg2Down = true;
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);

                                if (Leg2DownCut == false)
                                {
                                    GameObject LL = Instantiate(LeftLeg, LeftLegpos.transform.position, LeftLegpos.transform.rotation);
                                    LL.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        LL.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        LL.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(LL, 30);
                                }
                                else
                                {
                                    GameObject LLT = Instantiate(LeftLegTop, LeftLegToppos.transform.position, LeftLegToppos.transform.rotation);
                                    LLT.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        LLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        LLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(LLT, 30);
                                }
                            }
                            else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                            {
                                transform.Find("Left leg down1").gameObject.SetActive(false); //다리 하단 잘림
                                transform.Find("Left leg down2").gameObject.SetActive(false);
                                transform.Find("Left leg down foot").gameObject.SetActive(false);
                                healthAtroCrossfa.TearOn = true;
                                RightLegDown2 = true;
                                Leg2DownCut = true;
                                Leg2Down = true;
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                                LLD.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                if (Direction == false)
                                    LLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                else
                                    LLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                Destroy(LLD, 30);
                            }
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
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

        else
        {
            while (true)
            {
                LegHitPoint2 = LegHitPoint2 - damage;

                if (LegHitPoint2 <= LegHitPoint * 0.6f)
                {
                    int Count = Random.Range(2, 3);

                    for (int i = 0; i < Count; i++)
                    {
                        TearCount = 1;

                        if (TearCount == 1)
                        {
                            TearPartCount = Random.Range(1, 3);

                            if (TearPartCount == 1)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightLegTop1 == false)
                                {
                                    transform.Find("Right leg top").gameObject.SetActive(false); //다리 상단 잘림
                                    transform.Find("Right leg top knee").gameObject.SetActive(false);
                                    transform.Find("Right leg down1").gameObject.SetActive(false);
                                    transform.Find("Right leg down2").gameObject.SetActive(false);
                                    transform.Find("Right leg down foot").gameObject.SetActive(false);
                                    healthAtroCrossfa.TearOn = true;
                                    RightLegTop1 = true;
                                    Leg1Down = true;
                                    gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                    gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);

                                    if (Leg1DownCut == false)
                                    {
                                        GameObject RL = Instantiate(RightLeg, RightLegpos.transform.position, RightLegpos.transform.rotation);
                                        RL.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RL.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                        else
                                            RL.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                        Destroy(RL, 30);
                                    }
                                    else
                                    {
                                        GameObject RLT = Instantiate(RightLegTop, RightLegToppos.transform.position, RightLegToppos.transform.rotation);
                                        RLT.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                        else
                                            RLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                        Destroy(RLT, 30);
                                    }
                                }
                                else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                                {
                                    transform.Find("Right leg down1").gameObject.SetActive(false); //다리 하단 잘림
                                    transform.Find("Right leg down2").gameObject.SetActive(false);
                                    transform.Find("Right leg down foot").gameObject.SetActive(false);
                                    healthAtroCrossfa.TearOn = true;
                                    RightLegDown1 = true;
                                    Leg1DownCut = true;
                                    Leg1Down = true;
                                    gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                    gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);
                                    GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                                    RLD.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        RLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        RLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(RLD, 30);
                                }
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
                            }
                            else if (TearPartCount == 2)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightLegTop2 == false)
                                {
                                    transform.Find("Left leg top").gameObject.SetActive(false); //다리 상단 잘림
                                    transform.Find("Left leg top knee").gameObject.SetActive(false);
                                    transform.Find("Left leg down1").gameObject.SetActive(false);
                                    transform.Find("Left leg down2").gameObject.SetActive(false);
                                    transform.Find("Left leg down foot").gameObject.SetActive(false);
                                    healthAtroCrossfa.TearOn = true;
                                    RightLegTop2 = true;
                                    Leg2Down = true;
                                    gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                    gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);

                                    if (Leg2DownCut == false)
                                    {
                                        GameObject LL = Instantiate(LeftLeg, LeftLegpos.transform.position, LeftLegpos.transform.rotation);
                                        LL.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            LL.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                        else
                                            LL.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                        Destroy(LL, 30);
                                    }
                                    else
                                    {
                                        GameObject LLT = Instantiate(LeftLegTop, LeftLegToppos.transform.position, LeftLegToppos.transform.rotation);
                                        LLT.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            LLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                        else
                                            LLT.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                        Destroy(LLT, 30);
                                    }
                                }
                                else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                                {
                                    transform.Find("Left leg down1").gameObject.SetActive(false); //다리 하단 잘림
                                    transform.Find("Left leg down2").gameObject.SetActive(false);
                                    transform.Find("Left leg down foot").gameObject.SetActive(false);
                                    healthAtroCrossfa.TearOn = true;
                                    RightLegDown2 = true;
                                    Leg2DownCut = true;
                                    Leg2Down = true;
                                    gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                    gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);
                                    GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                                    LLD.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        LLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = false;
                                    else
                                        LLD.GetComponent<DeathTaikaLaiThrotro1>().Direction = true;

                                    Destroy(LLD, 30);
                                }
                                gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                                gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
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

    //다리에게 데미지 전달
    public IEnumerator LegRaillgunDamage(int damage, float interval)
    {
        while (true)
        {
            LegHitPoint2 = LegHitPoint2 - damage;

            if (LegHitPoint2 <= LegHitPoint * 0.6f)
            {
                TearCount = 1;

                if (TearCount == 1)
                {
                    TearPartCount = Random.Range(1, 3);

                    if (TearPartCount == 1)
                    {
                        TearArmPart = Random.Range(1, 3);

                        if (TearArmPart == 1 && RightLegTop1 == false)
                        {
                            transform.Find("Right leg top").gameObject.SetActive(false); //다리 상단 잘림
                            transform.Find("Right leg top knee").gameObject.SetActive(false);
                            transform.Find("Right leg down1").gameObject.SetActive(false);
                            transform.Find("Right leg down2").gameObject.SetActive(false);
                            transform.Find("Right leg down foot").gameObject.SetActive(false);
                            healthAtroCrossfa.TearOn = true;
                            RightLegTop1 = true;
                            Leg1Down = true;
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);

                            if (Leg1DownCut == false)
                            {
                                GameObject RL = Instantiate(RightLeg, RightLegpos.transform.position, RightLegpos.transform.rotation);
                                Destroy(RL, 30);
                            }
                            else
                            {
                                GameObject RLT = Instantiate(RightLegTop, RightLegToppos.transform.position, RightLegToppos.transform.rotation);
                                Destroy(RLT, 30);
                            }
                        }
                        else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                        {
                            transform.Find("Right leg down1").gameObject.SetActive(false); //다리 하단 잘림
                            transform.Find("Right leg down2").gameObject.SetActive(false);
                            transform.Find("Right leg down foot").gameObject.SetActive(false);
                            healthAtroCrossfa.TearOn = true;
                            RightLegDown1 = true;
                            Leg1DownCut = true;
                            Leg1Down = true;
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg1Down(true);
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg1Down(true);
                            GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                            Destroy(RLD, 30);
                        }
                        gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                        gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
                    }
                    else if (TearPartCount == 2)
                    {
                        TearArmPart = Random.Range(1, 3);

                        if (TearArmPart == 1 && RightLegTop2 == false)
                        {
                            transform.Find("Left leg top").gameObject.SetActive(false); //다리 상단 잘림
                            transform.Find("Left leg top knee").gameObject.SetActive(false);
                            transform.Find("Left leg down1").gameObject.SetActive(false);
                            transform.Find("Left leg down2").gameObject.SetActive(false);
                            transform.Find("Left leg down foot").gameObject.SetActive(false);
                            healthAtroCrossfa.TearOn = true;
                            RightLegTop2 = true;
                            Leg2Down = true;
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);

                            if (Leg2DownCut == false)
                            {
                                GameObject LL = Instantiate(LeftLeg, LeftLegpos.transform.position, LeftLegpos.transform.rotation);
                                Destroy(LL, 30);
                            }
                            else
                            {
                                GameObject LLT = Instantiate(LeftLegTop, LeftLegToppos.transform.position, LeftLegToppos.transform.rotation);
                                Destroy(LLT, 30);
                            }
                        }
                        else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                        {
                            transform.Find("Left leg down1").gameObject.SetActive(false); //다리 하단 잘림
                            transform.Find("Left leg down2").gameObject.SetActive(false);
                            transform.Find("Left leg down foot").gameObject.SetActive(false);
                            healthAtroCrossfa.TearOn = true;
                            RightLegDown2 = true;
                            Leg2DownCut = true;
                            Leg2Down = true;
                            gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaLeg2Down(true);
                            gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaLeg2Down(true);
                            GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                            Destroy(LLD, 30);
                        }
                        gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaImDown(true);
                        gameObject.GetComponent<BehaviourAtroCrossfa>().AtroCrossfaImDown(true);
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