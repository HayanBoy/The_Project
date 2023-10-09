using System.Collections;
using UnityEngine;

public class TearInfector : MonoBehaviour
{
    private int TearCount; //신체 부위가 잘려나갈 확률
    private int TearPartCount; //신체 부위가 잘려나갈 때 어떤 부위가 잘려나갈 확률
    private int TearType; //타격 부위 전달
    private int Count; //샷건 타격에서의 신체 훼손을 여러개 발생시키기 위한 for문 반복
    public bool Type = false; //타이트로키인지 확인하는 여부. 맞으면 일정 체력이 내려갈 때까지 사지가 날아가지 않는다. 아닐 경우, 기본 좀비처럼 사지가 일정 확률로 날아간다.
    public bool StartTearing = false; //타이트로키 전용. 일정 체력까지 감소되었을 경우부터 사지가 잘리기 시작.
    private bool HeadShot = false;
    private bool HeadBlood = false;
    private bool RABBlood = false;
    private bool RAUBlood3 = false;
    private bool RADBlood = false;
    private bool LABBlood = false;
    private bool RAUBlood5 = false;
    private bool LADBlood = false;
    private bool LLBBlood = false;
    private bool RLBBlood = false;


    public int TakeDown; //넘어질 확률
    Animator animator;

    IEnumerator bloodactionFlip;
    IEnumerator bloodaction;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;
    public GameObject FloorBloodFlip;
    public Transform FloorBloodFlipPos;

    //부위 별 파트 제거후, 해당 부위가 더 이상 발생되지 않기 위한 조취
    public bool Head = false; //얼굴이 날아갔을 때의 신호
    public bool HANDR = false; //오른손이 잘렸을 경우의 신호
    public bool B1LDRend = false; //오른팔 하단이 잘렸을 경우의 신호
    public bool B1LURend = false; //오른팔 상단이 잘렸을 경우의 신호
    public bool HANDL = false; //왼손이 잘렸을 경우의 신호
    public bool B1LDLend = false; //왼팔 하단이 잘렸을 경우의 신호
    public bool B1LULend = false; //왼팔 상단이 잘렸을 경우의 신호
    public bool LegL = false; //왼쪽 다리가 잘렸을 경우의 신호
    public bool LegR = false; //오른쪽 다리가 잘렸을 경우의 신호

    public bool TearPartByOneShot = false; //샷건 무기에 한번 타격을 받았을 때, 신체가 훼손되기 위한 스위치

    //public GameObject Blood;
    //public Transform LeftArmUpBloodpos;

    public void SetTear(int num)
    {
        TearType = num;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        bloodactionFlip = BloodActionFlip();
        bloodaction = BloodAction();
    }

    private void OnEnable()
    {
        if (HeadBlood == true)
        {
            HeadBlood = false;
            transform.Find("bone_1/Head body blood").gameObject.SetActive(false);
        }

        if (RABBlood == true)
        {
            RABBlood = false;
            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(false);
        }

        if (RAUBlood3 == true)
        {
            RAUBlood3 = false;
            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(false);
        }

        if (RADBlood == true)
        {
            RADBlood = false;
            transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(false);
        }

        if (LABBlood == true)
        {
            LABBlood = false;
            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(false);
        }

        if (RAUBlood5 == true)
        {
            RAUBlood5 = false;
            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(false);
        }

        if (LADBlood == true)
        {
            LADBlood = false;
            transform.Find("bone_1/bone_4/bone_5/bone_7/Left arm down blood").gameObject.SetActive(false);
        }

        if (LLBBlood == true)
        {
            LLBBlood = false;
            transform.Find("bone_1/Left leg body blood").gameObject.SetActive(false);
        }

        if (RLBBlood == true)
        {
            RLBBlood = false;
            transform.Find("bone_1/Right leg body blood").gameObject.SetActive(false);
        }

        TakeDown = 0;
        TearType = 0;
        TearPartCount = 0;
        HeadShot = false;
        Head = false;
        HANDR = false;
        B1LDRend = false;
        B1LURend = false;
        HANDL = false;
        B1LDLend = false;
        B1LULend = false;
        LegL = false;
        LegR = false;
        TearPartByOneShot = false;
        StartTearing = false;
    }

    IEnumerator BloodActionFlip()
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

    IEnumerator BloodAction()
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

    public void Update()
    {
        if (Type == false && StartTearing == false) //일반 좀비. 타격 받을 때마다 일정 확률로 사지가 날아간다.
        {
            if (TearType == 1) //얼굴 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true)
                    GetComponent<HealthInfector>().ImHit = false;
                if (GetComponent<HealthInfector>().Death == true && HeadShot == false)
                {
                    HeadShot = true;
                    GetComponent<InfectorSpawn>().HEADOUTonline = true;
                    GetComponent<InfectorSpawn>().HEADout = true;
                    HeadBlood = true;
                    transform.Find("bone_1/Head body blood").gameObject.SetActive(true);
                    StartCoroutine(bloodactionFlip);
                }
            }
            else if (TearType == 2) //몸 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 7);

                    if (TearCount == 0 && TearPartCount == 1 && B1LURend == false) //오른 팔 상단
                    {
                        if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmUpDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true;
                            GetComponent<InfectorSpawn>().B1LURout = true;
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else if (B1LDRend == true)//오른쪽 하단 팔이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmUpOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LURout = true;
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().RightArmUpDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true; //오른손을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LURout = true; //오른팔 상단을 삭제시킨다.
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 2 && B1LDRend == false && B1LURend == false) //오른 팔 하단
                    {
                        if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true;
                            B1LDRend = true;
                            RAUBlood3 = true;
                            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().RightArmDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단 삭제
                            GetComponent<InfectorSpawn>().HandRout = true; //오른손 삭제
                            B1LDRend = true;
                            RAUBlood3 = true;
                            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //오른쪽 손
                    {
                        GetComponent<InfectorSpawn>().HANDROUTonline = true;
                        GetComponent<InfectorSpawn>().HandRout = true;
                        HANDR = true;
                        RADBlood = true;
                        transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                        GetComponent<HealthInfector>().TearOn = true;
                    }

                    else if (TearCount == 0 && TearPartCount == 4 && B1LULend == false) //왼쪽 팔 상단
                    {
                        if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true;
                            GetComponent<InfectorSpawn>().B1LULout = true;
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else if (B1LDLend == true)//왼쪽 하단 팔이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LULout = true;
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().HandLout = true; //왼손을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LULout = true; //왼팔 상단을 삭제시킨다.
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 5 && B1LDLend == false && B1LULend == false) //왼쪽 팔 하단
                    {
                        if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true;
                            B1LDLend = true;
                            RAUBlood5 = true;
                            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().LeftArmDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단 삭제
                            GetComponent<InfectorSpawn>().HandLout = true; //왼손 삭제
                            B1LDLend = true;
                            RAUBlood5 = true;
                            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //왼쪽 손
                    {
                        GetComponent<InfectorSpawn>().HANDLOUTonline = true;
                        GetComponent<InfectorSpawn>().HandLout = true;
                        HANDL = true;
                        LADBlood = true;
                        transform.Find("bone_1/bone_4/bone_5/bone_7/Left arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodaction);
                        GetComponent<HealthInfector>().TearOn = true;
                    }
                }
                else if (TearPartByOneShot == true) //샷건 무기에 의한 타격
                {
                    TearPartByOneShot = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    Count = Random.Range(1, 4);

                    for (int i = 0; i < Count; i++)
                    {
                        TearPartCount = Random.Range(1, 7);

                        if (TearPartCount == 1 && B1LURend == false) //오른 팔 상단
                        {
                            if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmUpDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true;
                                GetComponent<InfectorSpawn>().B1LURout = true;
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else if (B1LDRend == true)//오른쪽 하단 팔이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmUpOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LURout = true;
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().RightArmUpDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().HandRout = true; //오른손을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LURout = true; //오른팔 상단을 삭제시킨다.
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 2 && B1LDRend == false && B1LURend == false) //오른 팔 하단
                        {
                            if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true;
                                B1LDRend = true;
                                RAUBlood3 = true;
                                transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().RightArmDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단 삭제
                                GetComponent<InfectorSpawn>().HandRout = true; //오른손 삭제
                                B1LDRend = true;
                                RAUBlood3 = true;
                                transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //오른쪽 손
                        {
                            GetComponent<InfectorSpawn>().HANDROUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true;
                            HANDR = true;
                            RADBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }

                        else if (TearPartCount == 4 && B1LULend == false) //왼쪽 팔 상단
                        {
                            if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true;
                                GetComponent<InfectorSpawn>().B1LULout = true;
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else if (B1LDLend == true)//왼쪽 하단 팔이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LULout = true;
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().HandLout = true; //왼손을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LULout = true; //왼팔 상단을 삭제시킨다.
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 5 && B1LDLend == false && B1LULend == false) //왼쪽 팔 하단
                        {
                            if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true;
                                B1LDLend = true;
                                RAUBlood5 = true;
                                transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().LeftArmDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단 삭제
                                GetComponent<InfectorSpawn>().HandLout = true; //왼손 삭제
                                B1LDLend = true;
                                RAUBlood5 = true;
                                transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //왼쪽 손
                        {
                            GetComponent<InfectorSpawn>().HANDLOUTonline = true;
                            GetComponent<InfectorSpawn>().HandLout = true;
                            HANDL = true;
                            LADBlood = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Left arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                }
            }

            else if (TearType == 3) //다리 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //왼쪽 다리
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        TakeDown = 0;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //오른쪽 다리
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        TakeDown = 0;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    //멀쩡한 상태로 넘어짐
                    else if (TearCount == 1)
                    {
                        if (LegL == false && LegR == false)
                        {
                            TakeDown = Random.Range(0, 2);
                        }
                    }
                }
                else if (TearPartByOneShot == true) //샷건 무기에 의한 타격
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //왼쪽 다리
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //오른쪽 다리
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                }
            }
        }

        else if (Type == true && StartTearing == true)//타이트로키 좀비. 일정 체력 이하가 내려가고 나서부터 사지가 날아가기 시작.
        {
            if (TearType == 1) //얼굴 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true)
                    GetComponent<HealthInfector>().ImHit = false;
                if (GetComponent<HealthInfector>().Death == true && HeadShot == false)
                {
                    HeadShot = true;
                    GetComponent<InfectorSpawn>().HEADOUTonline = true;
                    GetComponent<InfectorSpawn>().HEADout = true;
                    HeadBlood = true;
                    transform.Find("bone_1/bone_f1/Part3").gameObject.SetActive(false);
                    transform.Find("bone_1/bone_f1/Part9").gameObject.SetActive(false);
                    transform.Find("bone_1/Head body blood").gameObject.SetActive(true);
                    StartCoroutine(bloodactionFlip);
                }
            }
            else if (TearType == 2) //몸 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 7);

                    if (TearCount == 0 && TearPartCount == 1 && B1LURend == false) //오른 팔 상단
                    {
                        if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmUpDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true;
                            GetComponent<InfectorSpawn>().B1LURout = true;
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else if (B1LDRend == true)//오른쪽 하단 팔이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmUpOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LURout = true;
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().RightArmUpDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true; //오른손을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LURout = true; //오른팔 상단을 삭제시킨다.
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 2 && B1LDRend == false && B1LURend == false) //오른 팔 하단
                    {
                        if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().RightArmDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true;
                            B1LDRend = true;
                            RAUBlood3 = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().RightArmDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단 삭제
                            GetComponent<InfectorSpawn>().HandRout = true; //오른손 삭제
                            B1LDRend = true;
                            RAUBlood3 = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //오른쪽 손
                    {
                        GetComponent<InfectorSpawn>().HANDROUTonline = true;
                        GetComponent<InfectorSpawn>().HandRout = true;
                        HANDR = true;
                        RADBlood = true;
                        transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                        GetComponent<HealthInfector>().TearOn = true;
                    }

                    else if (TearCount == 0 && TearPartCount == 4 && B1LULend == false) //왼쪽 팔 상단
                    {
                        if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true;
                            GetComponent<InfectorSpawn>().B1LULout = true;
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else if (B1LDLend == true)//왼쪽 하단 팔이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LULout = true;
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().LeftArmUpDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().HandLout = true; //왼손을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단을 삭제시킨다.
                            GetComponent<InfectorSpawn>().B1LULout = true; //왼팔 상단을 삭제시킨다.
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 5 && B1LDLend == false && B1LULend == false) //왼쪽 팔 하단
                    {
                        if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                        {
                            GetComponent<InfectorSpawn>().LeftArmDownOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true;
                            B1LDLend = true;
                            RAUBlood5 = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                        else
                        {
                            GetComponent<InfectorSpawn>().LeftArmDownHOUTonline = true;
                            GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단 삭제
                            GetComponent<InfectorSpawn>().HandLout = true; //왼손 삭제
                            B1LDLend = true;
                            RAUBlood5 = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //왼쪽 손
                    {
                        GetComponent<InfectorSpawn>().HANDLOUTonline = true;
                        GetComponent<InfectorSpawn>().HandLout = true;
                        HANDL = true;
                        LADBlood = true;
                        transform.Find("bone_1/bone_4/bone_5/bone_7/Left arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodaction);
                        GetComponent<HealthInfector>().TearOn = true;
                    }
                }
                else if (TearPartByOneShot == true) //샷건 무기에 의한 타격
                {
                    TearPartByOneShot = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    Count = Random.Range(1, 4);

                    for (int i = 0; i < Count; i++)
                    {
                        TearPartCount = Random.Range(1, 7);

                        if (TearPartCount == 1 && B1LURend == false) //오른 팔 상단
                        {
                            if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmUpDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true;
                                GetComponent<InfectorSpawn>().B1LURout = true;
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else if (B1LDRend == true)//오른쪽 하단 팔이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmUpOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LURout = true;
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().RightArmUpDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().HandRout = true; //오른손을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LURout = true; //오른팔 상단을 삭제시킨다.
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part8").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Part10").gameObject.SetActive(false);
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 2 && B1LDRend == false && B1LURend == false) //오른 팔 하단
                        {
                            if (HANDR == true) //오른쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().RightArmDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true;
                                B1LDRend = true;
                                RAUBlood3 = true;
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().RightArmDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDRout = true; //오른팔 하단 삭제
                                GetComponent<InfectorSpawn>().HandRout = true; //오른손 삭제
                                B1LDRend = true;
                                RAUBlood3 = true;
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part12").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part13").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Part15").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/bone_6/Blade2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //오른쪽 손
                        {
                            GetComponent<InfectorSpawn>().HANDROUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true;
                            HANDR = true;
                            RADBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }

                        else if (TearPartCount == 4 && B1LULend == false) //왼쪽 팔 상단
                        {
                            if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true;
                                GetComponent<InfectorSpawn>().B1LULout = true;
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else if (B1LDLend == true)//왼쪽 하단 팔이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LULout = true;
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().LeftArmUpDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().HandLout = true; //왼손을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단을 삭제시킨다.
                                GetComponent<InfectorSpawn>().B1LULout = true; //왼팔 상단을 삭제시킨다.
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part2").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/Part3").gameObject.SetActive(false);
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 5 && B1LDLend == false && B1LULend == false) //왼쪽 팔 하단
                        {
                            if (HANDL == true) //왼쪽 손이 이미 잘려나갔을 경우
                            {
                                GetComponent<InfectorSpawn>().LeftArmDownOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true;
                                B1LDLend = true;
                                RAUBlood5 = true;
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                            else
                            {
                                GetComponent<InfectorSpawn>().LeftArmDownHOUTonline = true;
                                GetComponent<InfectorSpawn>().B1LDLout = true; //왼팔 하단 삭제
                                GetComponent<InfectorSpawn>().HandLout = true; //왼손 삭제
                                B1LDLend = true;
                                RAUBlood5 = true;
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part4").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part5").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Part6").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/bone_5/bone_7/Blade1").gameObject.SetActive(false);
                                transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //왼쪽 손
                        {
                            GetComponent<InfectorSpawn>().HANDLOUTonline = true;
                            GetComponent<InfectorSpawn>().HandLout = true;
                            HANDL = true;
                            LADBlood = true;
                            transform.Find("bone_1/bone_4/bone_5/bone_7/Left arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                }
            }

            else if (TearType == 3) //다리 타격
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //왼쪽 다리
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        TakeDown = 0;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //오른쪽 다리
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        TakeDown = 0;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    //멀쩡한 상태로 넘어짐
                    else if (TearCount == 1)
                    {
                        if (LegL == false && LegR == false)
                        {
                            TakeDown = Random.Range(0, 2);
                        }
                    }
                }
                else if (TearPartByOneShot == true) //샷건 무기에 의한 타격
                {
                    GetComponent<HealthInfector>().ImHit = false; //딱 한번만 타격 받았다는 것으로 처리되기 위한 조취
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //왼쪽 다리
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //오른쪽 다리
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                }
            }
        }

        else if (StartTearing == false)
        {
            if (GetComponent<HealthInfector>().ImHit == true)
            {
                GetComponent<HealthInfector>().ImHit = false;

                if (TearType == 3)
                {
                    int DownRandom = Random.Range(0, 2);

                    if (DownRandom == 1)
                    {
                        if (LegL == false && LegR == false)
                        {
                            TakeDown = Random.Range(0, 2);
                        }
                    }
                }
            }
        }
    }
}