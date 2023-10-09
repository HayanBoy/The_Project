using System.Collections;
using UnityEngine;

public class TearAsoShiioshare : MonoBehaviour
{
    BehaviorAsoShiioshare behaviorAsoShiioshare;
    HealthAsoShiioshare healthAsoShiioshare;

    Animator animator;

    public float LegsHitPoint; //다리 체력. 체력이 50%가 되면 다리 하나 날아가서 속도가 저하되며, 체력이 0이 되면 나머지 다리까지 날아가면서 이동이 무력화된다.
    public float ArmHitPoint; //팔 체력. 체력이 60%가 되면 랜덤함수에 의거하여 팔하나 날아간다.
    public float HeadHitPoint; //얼굴 체력. 체력이 모두 소진되면 핼멧이 날아간다.
    private float LegsHitPoint2; //리스폰용 다리 체력
    private float ArmHitPoint2; //리스폰용 팔 체력
    private float HeadHitPoint2; //리스폰용 얼굴체력
    private int TearCount; //찢김 여부
    private int TearPartCount; //랜덤으로 팔이 날아가는 변수
    private int TearArmPart; //팔 상단 및 하단의 날아가는 변수
    private int SubWeaponRight; //보조무기가 한번만 나오도록 조취하는 변수
    private int SubWeaponLeft; //보조무기가 한번만 나오도록 조취하는 변수

    private bool Arm1Down = false; //1번 팔이 날아가면 작동되는 스위치
    private bool Arm2Down = false; //2번 팔이 날아가면 작동되는 스위치
    private bool Arm3Down = false; //3번 팔이 날아가면 작동되는 스위치
    private bool NoTotalArm = false; //팔이 모두 날아갔을 때의 스위치
    private bool MainGunDown = false; //주무기 날아가면 작동되는 스위치
    private bool Leg1Down = false; //1번 다리가 날아가면 작동되는 스위치
    private bool Leg2Down = false; //2번 다리가 날아가면 작동되는 스위치
    private bool SwordOn = false; //에너지 소드가 활성화되었을 때 스위치

    public bool RightArmTop1 = false;
    public bool RightArmTop2 = false;
    public bool RightArmTop3 = false;
    public bool RightLegTop1 = false;
    public bool RightLegTop2 = false;
    public bool RightLegDown1 = false;
    public bool RightLegDown2 = false;
    public bool RightArmDown1 = false;
    public bool RightArmDown2 = false;
    public bool RightArmDown3 = false;
    private bool Leg1DownCut = false;
    private bool Leg2DownCut = false;
    private bool HalmetOut = false;

    bool Direction;
    public int LargeThrow;
    public bool TearPartByOneShot = false; //샷건 무기에 한번 타격을 받았을 때, 신체가 훼손되기 위한 스위치

    private bool MainWeapons = false; //주무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool SubWeaponsR = false; //오른쪽 보조무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool SubWeaponsL = false; //왼쪽 보조무기를 떨어뜨리지 않고 죽었을 경우의 스위치
    private bool MainWeaponOff = false; //주무기를 떨어뜨렸을 때의 신호
    private bool SubWeaponsRReady = false; //오른쪽 보조무기를 들었을 때의 신호
    private bool SubWeaponsLReady = false; //왼쪽 보조무기를 들었을 때의 신호

    IEnumerator bloodactionFlip;
    IEnumerator bloodaction;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;
    public GameObject FloorBloodFlip;
    public Transform FloorBloodFlipPos;

    public GameObject LeftArm;
    public Transform LeftArmpos;
    public GameObject LeftArmTop;
    public Transform LeftArmToppos;
    public GameObject LeftArmDown;
    public Transform LeftArmDownpos;
    public GameObject RightArm;
    public Transform RightArmpos;
    public GameObject RightArmTop;
    public Transform RightArmToppos;
    public GameObject RightArmDown;
    public Transform RightArmDownpos;
    public GameObject RightArm2;
    public Transform RightArm2pos;
    public GameObject RightArm2Sword;
    public Transform RightArm2Swordpos;
    public GameObject RightArm2Top;
    public Transform RightArm2Toppos;
    public GameObject RightArm2Down;
    public Transform RightArm2Downpos;
    public GameObject RightArm2SwordDown;
    public Transform RightArm2SwordDownpos;

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

    public GameObject Halmet;
    public Transform Halmetpos;

    public GameObject MainWeapon;
    public Transform MainWeaponpos;
    public GameObject SubWeapon;
    public Transform SubWeaponRightpos;
    public Transform SubWeaponLeftpos;

    public void AsoShiioshareSwordOn(bool Down)
    {
        if (Down == true)
        {
            SwordOn = true;
        }
        else
        {
            SwordOn = false;
        }
    }

    public void SetDirection(bool Boolean)
    {
        Direction = Boolean;
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

    private void OnEnable()
    {
        Arm1Down = false;
        Arm2Down = false;
        Arm3Down = false;
        NoTotalArm = false;
        MainGunDown = false;
        Leg1Down = false;
        Leg2Down = false;
        Leg1DownCut = false;
        Leg2DownCut = false;
        RightLegDown1 = false;
        RightLegDown2 = false;

        RightArmTop1 = false;
        RightArmTop2 = false;
        RightArmTop3 = false;
        RightLegTop1 = false;
        RightLegTop2 = false;
        RightArmDown1 = false;
        RightArmDown2 = false;
        RightArmDown3 = false;
        HalmetOut = false;
        SwordOn = false;
        TearPartByOneShot = false;

        MainWeapons = false;
        MainWeaponOff = false;
        SubWeaponsR = false;
        SubWeaponsL = false;
        SubWeaponsRReady = false;
        SubWeaponsLReady = false;

        LegsHitPoint = 0;
        ArmHitPoint = 0;
        TearCount = 0;
        TearPartCount = 0;
        TearArmPart = 0;
        ArmHitPoint2 = 0;
        LegsHitPoint2 = 0;
        SubWeaponRight = 0;
        SubWeaponLeft = 0;

        HeadHitPoint = GetComponent<HealthAsoShiioshare>().startingHitPoints / 3;
        ArmHitPoint = GetComponent<HealthAsoShiioshare>().startingHitPoints / 3;
        LegsHitPoint = GetComponent<HealthAsoShiioshare>().startingHitPoints / 3;
        HeadHitPoint2 = HeadHitPoint;
        ArmHitPoint2 = ArmHitPoint;
        LegsHitPoint2 = LegsHitPoint;
    }

    private void OnDisable()
    {
        if (Arm1Down == true)
        {
            transform.Find("Aso Shiioshare Left arm up").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Left arm down").gameObject.SetActive(true);
            transform.Find("Body1/Left arm body blood").gameObject.SetActive(false);
            transform.Find("Body1/Right sholder/Right arm1/Left arm up blood").gameObject.SetActive(false);
            transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Aso Shiioshare LED1").gameObject.SetActive(true);
        }

        if (Arm2Down == true)
        {
            transform.Find("Aso Shiioshare Right arm up1").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Right arm down1").gameObject.SetActive(true);
            transform.Find("Body1/Right2 arm body blood").gameObject.SetActive(false);
            transform.Find("Body1/Left sholder/Left down arm1/Right arm up blood").gameObject.SetActive(false);
            transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Aso Shiioshare LED1").gameObject.SetActive(true);
        }

        if (Arm3Down == true)
        {
            transform.Find("Aso Shiioshare Right arm up2").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(true);
            transform.Find("Body1/Right1 arm up blood").gameObject.SetActive(false);
            transform.Find("Body1/Left up arm1/Right arm up blood").gameObject.SetActive(false);
            transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(true);
        }

        if (MainGunDown == true)
        {
            transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Leg1Down == true)
        {
            transform.Find("Aso Shiioshare Left leg up").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(true);
            transform.Find("Body1/Body2/Right leg top/Aso Shiioshare LED1").gameObject.SetActive(true);
        }

        if (Leg2Down == true)
        {
            transform.Find("Aso Shiioshare Right leg up").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(true);
            transform.Find("Body1/Body2/Left leg top/Aso Shiioshare LED1").gameObject.SetActive(true);
        }

        if (HeadHitPoint2 <= 0)
        {
            transform.Find("Aso Shiioshare Halmet").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Head").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Head tentacle1").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Head tentacle2").gameObject.SetActive(true);
            transform.Find("Aso Shiioshare Head tentacle3").gameObject.SetActive(true);
            transform.Find("Body1/Head/Aso Shiioshare LED1").gameObject.SetActive(true);
            transform.Find("Body1/Head/Aso Shiioshare LED2").gameObject.SetActive(true);
            transform.Find("Body1/Head/Aso Shiioshare LED3").gameObject.SetActive(true);
            transform.Find("Body1/Head/Aso Shiioshare LED4").gameObject.SetActive(true);
        }
    }

    void Start()
    {
        behaviorAsoShiioshare = FindObjectOfType<BehaviorAsoShiioshare>();
        healthAsoShiioshare = FindObjectOfType<HealthAsoShiioshare>();
        animator = GetComponent<Animator>();
        bloodactionFlip = BloodActionFlip();
        bloodaction = BloodAction();
    }

    IEnumerator BloodActionFlip() //왼쪽에서 뿜어져 나오는 피의 방향
    {
        GameObject BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
        //Debug.Log("Blood Flip");
        yield return new WaitForSeconds(0.5f);
        BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloorF = Instantiate(FloorBloodFlip, FloorBloodFlipPos.transform.position, FloorBloodFlipPos.transform.rotation);
        Destroy(BloodFloorF, 10);
    }

    IEnumerator BloodAction() //얼굴, 오른쪽에서 뿜어져 나오는 피의 방향
    {
        GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
        yield return new WaitForSeconds(0.5f);
        BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
        Destroy(BloodFloor, 10);
    }

    //팔에게 데미지 전달
    public IEnumerator ArmDamage(int damage, float interval)
    {
        if (NoTotalArm == false)
        {
            if(TearPartByOneShot == false)
            {
                while (true)
                {
                    ArmHitPoint2 = ArmHitPoint2 - damage;

                    if (ArmHitPoint2 <= ArmHitPoint * 0.6f)
                    {
                        //Debug.Log("팔 체력 60% 이하");
                        TearCount = Random.Range(0, 2);

                        if (TearCount == 1)
                        {
                            TearPartCount = Random.Range(0, 6);

                            if (TearPartCount == 1)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightArmTop1 == false)
                                {
                                    transform.Find("Aso Shiioshare Left arm up").gameObject.SetActive(false); //팔 상단 잘림
                                    transform.Find("Aso Shiioshare Left arm down").gameObject.SetActive(false);
                                    transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    healthAsoShiioshare.TearOn = true;
                                    RightArmTop1 = true;
                                    Arm1Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm1Down(true);
                                    transform.Find("Body1/Left arm body blood").gameObject.SetActive(true);
                                    StartCoroutine(bloodactionFlip);

                                    if (RightArmDown1 == false)
                                    {
                                        GameObject LR = Instantiate(LeftArm, LeftArmpos.transform.position, LeftArmpos.transform.rotation);
                                        LR.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            LR.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            LR.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(LR, 30);
                                    }
                                    else
                                    {
                                        GameObject LRT = Instantiate(LeftArmTop, LeftArmToppos.transform.position, LeftArmToppos.transform.rotation);
                                        LRT.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            LRT.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            LRT.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(LRT, 30);
                                    }
                                }
                                else if (TearArmPart == 2 && RightArmTop1 == false && RightArmDown1 == false)
                                {
                                    transform.Find("Aso Shiioshare Left arm down").gameObject.SetActive(false); //팔 하단 잘림
                                    transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    healthAsoShiioshare.TearOn = true;
                                    RightArmDown1 = true;
                                    Arm1Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm1Down(true);
                                    transform.Find("Body1/Right sholder/Right arm1/Left arm up blood").gameObject.SetActive(true);
                                    StartCoroutine(bloodactionFlip);
                                    GameObject LRD = Instantiate(LeftArmDown, LeftArmDownpos.transform.position, LeftArmDownpos.transform.rotation);
                                    LRD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        LRD.GetComponent<TearCreateInfector>().Direction = false;
                                    else
                                        LRD.GetComponent<TearCreateInfector>().Direction = true;

                                    Destroy(LRD, 30);
                                }

                                if (Arm1Down == true)
                                {
                                    if (MainGunDown == false && MainWeapons ==false)
                                    {
                                        //Debug.Log("주무기 무력화");
                                        MainGunDown = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareGunDown(true);
                                        gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareMainWeapon(true);
                                        transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                        GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
                                        MW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            MW.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            MW.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(MW, 30);
                                    }
                                }
                                else
                                {
                                    if (MainGunDown == true && SubWeaponLeft == 0 && SubWeaponsL == false && SubWeaponsLReady == true)
                                    {
                                        GameObject SW = Instantiate(SubWeapon, SubWeaponLeftpos.transform.position, SubWeaponLeftpos.transform.rotation);
                                        SW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            SW.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            SW.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(SW, 30);
                                        SubWeaponLeft++;
                                    }
                                }
                            }
                            else if (TearPartCount == 2)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightArmTop2 == false)
                                {
                                    transform.Find("Aso Shiioshare Right arm up1").gameObject.SetActive(false); //팔 상단 잘림
                                    transform.Find("Aso Shiioshare Right arm down1").gameObject.SetActive(false);
                                    transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    healthAsoShiioshare.TearOn = true;
                                    RightArmTop2 = true;
                                    Arm2Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm2Down(true);
                                    transform.Find("Body1/Right2 arm body blood").gameObject.SetActive(true);
                                    StartCoroutine(bloodaction);

                                    if (RightArmDown2 == false)
                                    {
                                        GameObject RA = Instantiate(RightArm, RightArmpos.transform.position, RightArmpos.transform.rotation);
                                        RA.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RA.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            RA.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(RA, 30);
                                    }
                                    else
                                    {
                                        GameObject RAT = Instantiate(RightArmTop, RightArmToppos.transform.position, RightArmToppos.transform.rotation);
                                        RAT.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RAT.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            RAT.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(RAT, 30);
                                    }
                                }
                                else if (TearArmPart == 2 && RightArmTop2 == false && RightArmDown2 == false)
                                {
                                    transform.Find("Aso Shiioshare Right arm down1").gameObject.SetActive(false); //팔 하단 잘림
                                    transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    healthAsoShiioshare.TearOn = true;
                                    RightArmDown2 = true;
                                    Arm2Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm2Down(true);
                                    transform.Find("Body1/Left sholder/Left down arm1/Right arm up blood").gameObject.SetActive(true);
                                    StartCoroutine(bloodaction);
                                    GameObject RAD = Instantiate(RightArmDown, RightArmDownpos.transform.position, RightArmDownpos.transform.rotation);
                                    RAD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                    if (Direction == false)
                                        RAD.GetComponent<TearCreateInfector>().Direction = false;
                                    else
                                        RAD.GetComponent<TearCreateInfector>().Direction = true;

                                    Destroy(RAD, 30);
                                }

                                if (Arm2Down == true)
                                {
                                    if (MainGunDown == false && MainWeapons == false)
                                    {
                                        //Debug.Log("주무기 무력화");
                                        MainGunDown = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareGunDown(true);
                                        gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareMainWeapon(true);
                                        transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                        GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
                                        MW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            MW.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            MW.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(MW, 30);
                                    }
                                }
                                else
                                {
                                    if (MainGunDown == true && SubWeaponRight == 0 && SubWeaponsR == false && SubWeaponsRReady == true)
                                    {
                                        GameObject SW = Instantiate(SubWeapon, SubWeaponRightpos.transform.position, SubWeaponRightpos.transform.rotation);
                                        SW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            SW.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            SW.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(SW, 30);
                                        SubWeaponRight++;
                                    }
                                }
                            }
                            else if (TearPartCount == 3)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightArmTop3 == false)
                                {
                                    if (SwordOn == true)
                                    {
                                        //Debug.Log("소드 무력화");
                                        transform.Find("Aso Shiioshare Right arm up2").gameObject.SetActive(false); //소드 팔 상단 잘림
                                        transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false);
                                        transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmTop3 = true;

                                        Arm3Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                        transform.Find("Body1/Right1 arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);

                                        if (RightArmDown3 == false)
                                        {
                                            GameObject RA2S = Instantiate(RightArm2Sword, RightArm2Swordpos.transform.position, RightArm2Swordpos.transform.rotation);
                                            RA2S.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2S.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2S.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2S, 30);
                                        }
                                        else
                                        {
                                            GameObject RA2T = Instantiate(RightArm2Top, RightArm2Toppos.transform.position, RightArm2Toppos.transform.rotation);
                                            RA2T.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2T.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2T.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2T, 30);
                                        }
                                    }
                                    else
                                    {
                                        //Debug.Log("소드 무력화");
                                        transform.Find("Aso Shiioshare Right arm up2").gameObject.SetActive(false); //소드 팔 상단 잘림
                                        transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false);
                                        transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmTop3 = true;

                                        Arm3Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                        transform.Find("Body1/Right1 arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);

                                        if (RightArmDown3 == false)
                                        {
                                            GameObject RA2 = Instantiate(RightArm2, RightArm2pos.transform.position, RightArm2pos.transform.rotation);
                                            RA2.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2, 30);
                                        }
                                        else
                                        {
                                            GameObject RA2T = Instantiate(RightArm2Top, RightArm2Toppos.transform.position, RightArm2Toppos.transform.rotation);
                                            RA2T.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2T.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2T.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2T, 30);
                                        }
                                    }
                                }
                                else if (TearArmPart == 2 && RightArmTop3 == false && RightArmDown3 == false)
                                {
                                    if (SwordOn == true)
                                    {
                                        //Debug.Log("소드 무력화");
                                        transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false); //소드 팔 하단 잘림
                                        transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmDown3 = true;

                                        Arm3Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                        transform.Find("Body1/Left up arm1/Right arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);
                                        GameObject RA2SD = Instantiate(RightArm2SwordDown, RightArm2SwordDownpos.transform.position, RightArm2SwordDownpos.transform.rotation);
                                        RA2SD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RA2SD.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            RA2SD.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(RA2SD, 30);
                                    }
                                    else
                                    {
                                        //Debug.Log("소드 무력화");
                                        transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false); //소드 팔 하단 잘림
                                        transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmDown3 = true;

                                        Arm3Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                        transform.Find("Body1/Left up arm1/Right arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);
                                        GameObject RA2D = Instantiate(RightArm2Down, RightArm2Downpos.transform.position, RightArm2Downpos.transform.rotation);
                                        RA2D.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RA2D.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            RA2D.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(RA2D, 30);
                                    }
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
            else //폭발에 의한 사지절단 여러개 처리
            {
                while (true)
                {
                    ArmHitPoint2 = ArmHitPoint2 - damage;

                    if (ArmHitPoint2 <= ArmHitPoint * 0.6f)
                    {
                        //Debug.Log("팔 체력 60% 이하");
                        int Count = Random.Range(3, 4);

                        for (int i = 0; i < Count; i++)
                        {
                            TearCount = 1;

                            if (TearCount == 1)
                            {
                                TearPartCount = Random.Range(1, 4);

                                if (TearPartCount == 1)
                                {
                                    TearArmPart = Random.Range(1, 3);

                                    if (TearArmPart == 1 && RightArmTop1 == false)
                                    {
                                        transform.Find("Aso Shiioshare Left arm up").gameObject.SetActive(false); //팔 상단 잘림
                                        transform.Find("Aso Shiioshare Left arm down").gameObject.SetActive(false);
                                        transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmTop1 = true;
                                        Arm1Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm1Down(true);
                                        transform.Find("Body1/Left arm body blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodactionFlip);

                                        if (RightArmDown1 == false)
                                        {
                                            GameObject LR = Instantiate(LeftArm, LeftArmpos.transform.position, LeftArmpos.transform.rotation);
                                            LR.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                LR.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                LR.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(LR, 30);
                                        }
                                        else
                                        {
                                            GameObject LRT = Instantiate(LeftArmTop, LeftArmToppos.transform.position, LeftArmToppos.transform.rotation);
                                            LRT.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                LRT.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                LRT.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(LRT, 30);
                                        }
                                    }
                                    else if (TearArmPart == 2 && RightArmTop1 == false && RightArmDown1 == false)
                                    {
                                        transform.Find("Aso Shiioshare Left arm down").gameObject.SetActive(false); //팔 하단 잘림
                                        transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmDown1 = true;
                                        Arm1Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm1Down(true);
                                        transform.Find("Body1/Right sholder/Right arm1/Left arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodactionFlip);
                                        GameObject LRD = Instantiate(LeftArmDown, LeftArmDownpos.transform.position, LeftArmDownpos.transform.rotation);
                                        LRD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            LRD.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            LRD.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(LRD, 30);
                                    }

                                    if (Arm1Down == true)
                                    {
                                        if (MainGunDown == false && MainWeapons == false)
                                        {
                                            //Debug.Log("주무기 무력화");
                                            MainGunDown = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareGunDown(true);
                                            gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareMainWeapon(true);
                                            transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                            GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
                                            MW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                MW.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                MW.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(MW, 30);
                                        }
                                    }
                                    else
                                    {
                                        if (MainGunDown == true && SubWeaponLeft == 0 && SubWeaponsL == false)
                                        {
                                            GameObject SW = Instantiate(SubWeapon, SubWeaponLeftpos.transform.position, SubWeaponLeftpos.transform.rotation);
                                            SW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                SW.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                SW.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(SW, 30);
                                            SubWeaponLeft++;
                                        }
                                    }
                                }
                                else if (TearPartCount == 2)
                                {
                                    TearArmPart = Random.Range(1, 3);

                                    if (TearArmPart == 1 && RightArmTop2 == false)
                                    {
                                        transform.Find("Aso Shiioshare Right arm up1").gameObject.SetActive(false); //팔 상단 잘림
                                        transform.Find("Aso Shiioshare Right arm down1").gameObject.SetActive(false);
                                        transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmTop2 = true;
                                        Arm2Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm2Down(true);
                                        transform.Find("Body1/Right2 arm body blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);

                                        if (RightArmDown2 == false)
                                        {
                                            GameObject RA = Instantiate(RightArm, RightArmpos.transform.position, RightArmpos.transform.rotation);
                                            RA.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA, 30);
                                        }
                                        else
                                        {
                                            GameObject RAT = Instantiate(RightArmTop, RightArmToppos.transform.position, RightArmToppos.transform.rotation);
                                            RAT.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RAT.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RAT.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RAT, 30);
                                        }
                                    }
                                    else if (TearArmPart == 2 && RightArmTop2 == false && RightArmDown2 == false)
                                    {
                                        transform.Find("Aso Shiioshare Right arm down1").gameObject.SetActive(false); //팔 하단 잘림
                                        transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                        healthAsoShiioshare.TearOn = true;
                                        RightArmDown2 = true;
                                        Arm2Down = true;
                                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareArm2Down(true);
                                        transform.Find("Body1/Left sholder/Left down arm1/Right arm up blood").gameObject.SetActive(true);
                                        StartCoroutine(bloodaction);
                                        GameObject RAD = Instantiate(RightArmDown, RightArmDownpos.transform.position, RightArmDownpos.transform.rotation);
                                        RAD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                        if (Direction == false)
                                            RAD.GetComponent<TearCreateInfector>().Direction = false;
                                        else
                                            RAD.GetComponent<TearCreateInfector>().Direction = true;

                                        Destroy(RAD, 30);
                                    }

                                    if (Arm2Down == true)
                                    {
                                        if (MainGunDown == false && MainWeapons == false)
                                        {
                                            //Debug.Log("주무기 무력화");
                                            MainGunDown = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareGunDown(true);
                                            gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareMainWeapon(true);
                                            transform.Find("Aso Shiioshare Weapon").gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                            GameObject MW = Instantiate(MainWeapon, MainWeaponpos.transform.position, MainWeaponpos.transform.rotation);
                                            MW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                MW.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                MW.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(MW, 30);
                                        }
                                    }
                                    else
                                    {
                                        if (MainGunDown == true && SubWeaponRight == 0 && SubWeaponsR == false)
                                        {
                                            GameObject SW = Instantiate(SubWeapon, SubWeaponRightpos.transform.position, SubWeaponRightpos.transform.rotation);
                                            SW.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                SW.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                SW.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(SW, 30);
                                            SubWeaponRight++;
                                        }
                                    }
                                }
                                else if (TearPartCount == 3)
                                {
                                    TearArmPart = Random.Range(1, 3);

                                    if (TearArmPart == 1 && RightArmTop3 == false)
                                    {
                                        if (SwordOn == true)
                                        {
                                            //Debug.Log("소드 무력화");
                                            transform.Find("Aso Shiioshare Right arm up2").gameObject.SetActive(false); //소드 팔 상단 잘림
                                            transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false);
                                            transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                            healthAsoShiioshare.TearOn = true;
                                            RightArmTop3 = true;

                                            Arm3Down = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                            transform.Find("Body1/Right1 arm up blood").gameObject.SetActive(true);
                                            StartCoroutine(bloodaction);

                                            if (RightArmDown3 == false)
                                            {
                                                GameObject RA2S = Instantiate(RightArm2Sword, RightArm2Swordpos.transform.position, RightArm2Swordpos.transform.rotation);
                                                RA2S.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                                if (Direction == false)
                                                    RA2S.GetComponent<TearCreateInfector>().Direction = false;
                                                else
                                                    RA2S.GetComponent<TearCreateInfector>().Direction = true;

                                                Destroy(RA2S, 30);
                                            }
                                            else
                                            {
                                                GameObject RA2T = Instantiate(RightArm2Top, RightArm2Toppos.transform.position, RightArm2Toppos.transform.rotation);
                                                RA2T.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                                if (Direction == false)
                                                    RA2T.GetComponent<TearCreateInfector>().Direction = false;
                                                else
                                                    RA2T.GetComponent<TearCreateInfector>().Direction = true;

                                                Destroy(RA2T, 30);
                                            }
                                        }
                                        else
                                        {
                                            //Debug.Log("소드 무력화");
                                            transform.Find("Aso Shiioshare Right arm up2").gameObject.SetActive(false); //소드 팔 상단 잘림
                                            transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false);
                                            transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                            healthAsoShiioshare.TearOn = true;
                                            RightArmTop3 = true;

                                            Arm3Down = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                            transform.Find("Body1/Right1 arm up blood").gameObject.SetActive(true);
                                            StartCoroutine(bloodaction);

                                            if (RightArmDown3 == false)
                                            {
                                                GameObject RA2 = Instantiate(RightArm2, RightArm2pos.transform.position, RightArm2pos.transform.rotation);
                                                RA2.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                                if (Direction == false)
                                                    RA2.GetComponent<TearCreateInfector>().Direction = false;
                                                else
                                                    RA2.GetComponent<TearCreateInfector>().Direction = true;

                                                Destroy(RA2, 30);
                                            }
                                            else
                                            {
                                                GameObject RA2T = Instantiate(RightArm2Top, RightArm2Toppos.transform.position, RightArm2Toppos.transform.rotation);
                                                RA2T.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                                if (Direction == false)
                                                    RA2T.GetComponent<TearCreateInfector>().Direction = false;
                                                else
                                                    RA2T.GetComponent<TearCreateInfector>().Direction = true;

                                                Destroy(RA2T, 30);
                                            }
                                        }
                                    }
                                    else if (TearArmPart == 2 && RightArmTop3 == false && RightArmDown3 == false)
                                    {
                                        if (SwordOn == true)
                                        {
                                            //Debug.Log("소드 무력화");
                                            transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false); //소드 팔 하단 잘림
                                            transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                            healthAsoShiioshare.TearOn = true;
                                            RightArmDown3 = true;

                                            Arm3Down = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                            transform.Find("Body1/Left up arm1/Right arm up blood").gameObject.SetActive(true);
                                            StartCoroutine(bloodaction);
                                            GameObject RA2SD = Instantiate(RightArm2SwordDown, RightArm2SwordDownpos.transform.position, RightArm2SwordDownpos.transform.rotation);
                                            RA2SD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2SD.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2SD.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2SD, 30);
                                        }
                                        else
                                        {
                                            //Debug.Log("소드 무력화");
                                            transform.Find("Aso Shiioshare Right arm down2").gameObject.SetActive(false); //소드 팔 하단 잘림
                                            transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3/Aso Shiioshare LED1").gameObject.SetActive(false);
                                            healthAsoShiioshare.TearOn = true;
                                            RightArmDown3 = true;

                                            Arm3Down = true;
                                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareSwordDown(true);
                                            transform.Find("Body1/Left up arm1/Right arm up blood").gameObject.SetActive(true);
                                            StartCoroutine(bloodaction);
                                            GameObject RA2D = Instantiate(RightArm2Down, RightArm2Downpos.transform.position, RightArm2Downpos.transform.rotation);
                                            RA2D.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);
                                            if (Direction == false)
                                                RA2D.GetComponent<TearCreateInfector>().Direction = false;
                                            else
                                                RA2D.GetComponent<TearCreateInfector>().Direction = true;

                                            Destroy(RA2D, 30);
                                        }
                                    }
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

    //다리에게 데미지 전달
    public IEnumerator LegDamage(int damage, float interval)
    {
        if (TearPartByOneShot == false)
        {
            while (true)
            {
                LegsHitPoint2 = LegsHitPoint2 - damage;

                if (LegsHitPoint2 <= LegsHitPoint * 0.6f)
                {
                    //Debug.Log("다리 체력 60% 이하");
                    TearCount = Random.Range(0, 2);

                    if (TearCount == 1)
                    {
                        TearPartCount = Random.Range(0, 6);

                        if (TearPartCount == 1)
                        {
                            TearArmPart = Random.Range(1, 3);

                            if (TearArmPart == 1 && RightLegTop1 == false)
                            {
                                transform.Find("Aso Shiioshare Left leg up").gameObject.SetActive(false); //다리 상단 잘림
                                transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false);
                                transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                                transform.Find("Body1/Body2/Right leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                                RightLegTop1 = true;
                                Leg1Down = true;
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);

                                if (Leg1DownCut == false)
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
                            else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                            {
                                transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false); //다리 하단 잘림
                                transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                                RightLegDown1 = true;
                                Leg1DownCut = true;
                                Leg1Down = true;
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);
                                GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                                Destroy(LLD, 30);
                            }

                            gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
                        }
                        else if (TearPartCount == 2)
                        {
                            TearArmPart = Random.Range(1, 3);

                            if (TearArmPart == 1 && RightLegTop2 == false)
                            {
                                transform.Find("Aso Shiioshare Right leg up").gameObject.SetActive(false); //다리 상단 잘림
                                transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false);
                                transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                                transform.Find("Body1/Body2/Left leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                                RightLegTop2 = true;
                                Leg2Down = true;
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);

                                if (Leg2DownCut == false)
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
                            else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                            {
                                transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false); //다리 하단 잘림
                                transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                                RightLegDown2 = true;
                                Leg2DownCut = true;
                                Leg2Down = true;
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);
                                GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                                Destroy(RLD, 30);
                            }

                            gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
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
                LegsHitPoint2 = LegsHitPoint2 - damage;

                if (LegsHitPoint2 <= LegsHitPoint * 0.6f)
                {
                    //Debug.Log("다리 체력 60% 이하");
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
                                    transform.Find("Aso Shiioshare Left leg up").gameObject.SetActive(false); //다리 상단 잘림
                                    transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false);
                                    transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                                    transform.Find("Body1/Body2/Right leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    RightLegTop1 = true;
                                    Leg1Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);

                                    if (Leg1DownCut == false)
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
                                else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                                {
                                    transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false); //다리 하단 잘림
                                    transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                                    RightLegDown1 = true;
                                    Leg1DownCut = true;
                                    Leg1Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);
                                    GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                                    Destroy(LLD, 30);
                                }

                                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
                            }
                            else if (TearPartCount == 2)
                            {
                                TearArmPart = Random.Range(1, 3);

                                if (TearArmPart == 1 && RightLegTop2 == false)
                                {
                                    transform.Find("Aso Shiioshare Right leg up").gameObject.SetActive(false); //다리 상단 잘림
                                    transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false);
                                    transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                                    transform.Find("Body1/Body2/Left leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                                    RightLegTop2 = true;
                                    Leg2Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);

                                    if (Leg2DownCut == false)
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
                                else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                                {
                                    transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false); //다리 하단 잘림
                                    transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                                    RightLegDown2 = true;
                                    Leg2DownCut = true;
                                    Leg2Down = true;
                                    gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);
                                    GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                                    Destroy(RLD, 30);
                                }

                                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                                gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
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
    public IEnumerator LegDamageRailGun(int damage, float interval)
    {
        while (true)
        {
            LegsHitPoint2 = LegsHitPoint2 - damage;

            if (LegsHitPoint2 <= LegsHitPoint * 0.6f)
            {
                //Debug.Log("다리 체력 60% 이하");
                TearCount = 1;

                if (TearCount == 1)
                {
                    TearPartCount = Random.Range(1, 3);

                    if (TearPartCount == 1)
                    {
                        TearArmPart = Random.Range(1, 3);

                        if (TearArmPart == 1 && RightLegTop1 == false)
                        {
                            transform.Find("Aso Shiioshare Left leg up").gameObject.SetActive(false); //다리 상단 잘림
                            transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false);
                            transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                            transform.Find("Body1/Body2/Right leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                            RightLegTop1 = true;
                            Leg1Down = true;
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);

                            if (Leg1DownCut == false)
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
                        else if (TearArmPart == 2 && RightLegTop1 == false && RightLegDown1 == false)
                        {
                            transform.Find("Aso Shiioshare Left leg down").gameObject.SetActive(false); //다리 하단 잘림
                            transform.Find("Aso Shiioshare Left leg down back").gameObject.SetActive(false);
                            RightLegDown1 = true;
                            Leg1DownCut = true;
                            Leg1Down = true;
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg1Down(true);
                            GameObject LLD = Instantiate(LeftLegDown, LeftLegDownpos.transform.position, LeftLegDownpos.transform.rotation);
                            Destroy(LLD, 30);
                        }

                        gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
                    }
                    else if (TearPartCount == 2)
                    {
                        TearArmPart = Random.Range(1, 3);

                        if (TearArmPart == 1 && RightLegTop2 == false)
                        {
                            transform.Find("Aso Shiioshare Right leg up").gameObject.SetActive(false); //다리 상단 잘림
                            transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false);
                            transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                            transform.Find("Body1/Body2/Left leg top/Aso Shiioshare LED1").gameObject.SetActive(false);
                            RightLegTop2 = true;
                            Leg2Down = true;
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);

                            if (Leg2DownCut == false)
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
                        else if (TearArmPart == 2 && RightLegTop2 == false && RightLegDown2 == false)
                        {
                            transform.Find("Aso Shiioshare Right leg down").gameObject.SetActive(false); //다리 하단 잘림
                            transform.Find("Aso Shiioshare Right leg down back").gameObject.SetActive(false);
                            RightLegDown2 = true;
                            Leg2DownCut = true;
                            Leg2Down = true;
                            gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareLeg2Down(true);
                            GameObject RLD = Instantiate(RightLegDown, RightLegDownpos.transform.position, RightLegDownpos.transform.rotation);
                            Destroy(RLD, 30);
                        }

                        gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareImDown(true);
                        gameObject.GetComponent<BehaviorAsoShiioshare>().AsoShiioshareImDown(true);
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

    //얼굴에게 데미지 전달
    public IEnumerator HeadDamage(int damage, float interval)
    {
        while (true)
        {
            HeadHitPoint2 = HeadHitPoint2 - damage;

            if(HeadHitPoint2 <= 0)
            {
                transform.Find("Aso Shiioshare Halmet").gameObject.SetActive(false);
                transform.Find("Body1/Head/Aso Shiioshare LED1").gameObject.SetActive(false);
                transform.Find("Body1/Head/Aso Shiioshare LED2").gameObject.SetActive(false);
                transform.Find("Body1/Head/Aso Shiioshare LED3").gameObject.SetActive(false);
                transform.Find("Body1/Head/Aso Shiioshare LED4").gameObject.SetActive(false);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareHeadDown(true);

                if (HalmetOut == false)
                {
                    HalmetOut = true;
                    GameObject halmet = Instantiate(Halmet, Halmetpos.transform.position, Halmetpos.transform.rotation);
                    Destroy(halmet, 30);
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

    void Update()
    {
        if(Arm1Down == true && Arm2Down == true && Arm3Down == true)
        {
            NoTotalArm = true;
        }
    }
}