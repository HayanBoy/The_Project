using System.Collections;
using UnityEngine;

public class TearInfector : MonoBehaviour
{
    private int TearCount; //��ü ������ �߷����� Ȯ��
    private int TearPartCount; //��ü ������ �߷����� �� � ������ �߷����� Ȯ��
    private int TearType; //Ÿ�� ���� ����
    private int Count; //���� Ÿ�ݿ����� ��ü �Ѽ��� ������ �߻���Ű�� ���� for�� �ݺ�
    public bool Type = false; //Ÿ��Ʈ��Ű���� Ȯ���ϴ� ����. ������ ���� ü���� ������ ������ ������ ���ư��� �ʴ´�. �ƴ� ���, �⺻ ����ó�� ������ ���� Ȯ���� ���ư���.
    public bool StartTearing = false; //Ÿ��Ʈ��Ű ����. ���� ü�±��� ���ҵǾ��� ������ ������ �߸��� ����.
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


    public int TakeDown; //�Ѿ��� Ȯ��
    Animator animator;

    IEnumerator bloodactionFlip;
    IEnumerator bloodaction;
    public GameObject FloorBlood;
    public Transform FloorBloodPos;
    public GameObject FloorBloodFlip;
    public Transform FloorBloodFlipPos;

    //���� �� ��Ʈ ������, �ش� ������ �� �̻� �߻����� �ʱ� ���� ����
    public bool Head = false; //���� ���ư��� ���� ��ȣ
    public bool HANDR = false; //�������� �߷��� ����� ��ȣ
    public bool B1LDRend = false; //������ �ϴ��� �߷��� ����� ��ȣ
    public bool B1LURend = false; //������ ����� �߷��� ����� ��ȣ
    public bool HANDL = false; //�޼��� �߷��� ����� ��ȣ
    public bool B1LDLend = false; //���� �ϴ��� �߷��� ����� ��ȣ
    public bool B1LULend = false; //���� ����� �߷��� ����� ��ȣ
    public bool LegL = false; //���� �ٸ��� �߷��� ����� ��ȣ
    public bool LegR = false; //������ �ٸ��� �߷��� ����� ��ȣ

    public bool TearPartByOneShot = false; //���� ���⿡ �ѹ� Ÿ���� �޾��� ��, ��ü�� �ѼյǱ� ���� ����ġ

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
        if (Type == false && StartTearing == false) //�Ϲ� ����. Ÿ�� ���� ������ ���� Ȯ���� ������ ���ư���.
        {
            if (TearType == 1) //�� Ÿ��
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
            else if (TearType == 2) //�� Ÿ��
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 7);

                    if (TearCount == 0 && TearPartCount == 1 && B1LURend == false) //���� �� ���
                    {
                        if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                        else if (B1LDRend == true)//������ �ϴ� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().HandRout = true; //�������� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LURout = true; //������ ����� ������Ų��.
                            B1LURend = true;
                            RABBlood = true;
                            transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 2 && B1LDRend == false && B1LURend == false) //���� �� �ϴ�
                    {
                        if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ� ����
                            GetComponent<InfectorSpawn>().HandRout = true; //������ ����
                            B1LDRend = true;
                            RAUBlood3 = true;
                            transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }

                    else if (TearCount == 0 && TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //������ ��
                    {
                        GetComponent<InfectorSpawn>().HANDROUTonline = true;
                        GetComponent<InfectorSpawn>().HandRout = true;
                        HANDR = true;
                        RADBlood = true;
                        transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                        GetComponent<HealthInfector>().TearOn = true;
                    }

                    else if (TearCount == 0 && TearPartCount == 4 && B1LULend == false) //���� �� ���
                    {
                        if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                        else if (B1LDLend == true)//���� �ϴ� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().HandLout = true; //�޼��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LULout = true; //���� ����� ������Ų��.
                            B1LULend = true;
                            LABBlood = true;
                            transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 5 && B1LDLend == false && B1LULend == false) //���� �� �ϴ�
                    {
                        if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ� ����
                            GetComponent<InfectorSpawn>().HandLout = true; //�޼� ����
                            B1LDLend = true;
                            RAUBlood5 = true;
                            transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                            StartCoroutine(bloodaction);
                            GetComponent<HealthInfector>().TearOn = true;
                        }
                    }
                    else if (TearCount == 0 && TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //���� ��
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
                else if (TearPartByOneShot == true) //���� ���⿡ ���� Ÿ��
                {
                    TearPartByOneShot = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    Count = Random.Range(1, 4);

                    for (int i = 0; i < Count; i++)
                    {
                        TearPartCount = Random.Range(1, 7);

                        if (TearPartCount == 1 && B1LURend == false) //���� �� ���
                        {
                            if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                            else if (B1LDRend == true)//������ �ϴ� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().HandRout = true; //�������� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LURout = true; //������ ����� ������Ų��.
                                B1LURend = true;
                                RABBlood = true;
                                transform.Find("bone_1/Right arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 2 && B1LDRend == false && B1LURend == false) //���� �� �ϴ�
                        {
                            if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ� ����
                                GetComponent<InfectorSpawn>().HandRout = true; //������ ����
                                B1LDRend = true;
                                RAUBlood3 = true;
                                transform.Find("bone_1/bone_2/bone_3/Right arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodactionFlip);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }

                        else if (TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //������ ��
                        {
                            GetComponent<InfectorSpawn>().HANDROUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true;
                            HANDR = true;
                            RADBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }

                        else if (TearPartCount == 4 && B1LULend == false) //���� �� ���
                        {
                            if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                            else if (B1LDLend == true)//���� �ϴ� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().HandLout = true; //�޼��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LULout = true; //���� ����� ������Ų��.
                                B1LULend = true;
                                LABBlood = true;
                                transform.Find("bone_1/Left arm body blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 5 && B1LDLend == false && B1LULend == false) //���� �� �ϴ�
                        {
                            if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ� ����
                                GetComponent<InfectorSpawn>().HandLout = true; //�޼� ����
                                B1LDLend = true;
                                RAUBlood5 = true;
                                transform.Find("bone_1/bone_4/Left arm up blood").gameObject.SetActive(true);
                                StartCoroutine(bloodaction);
                                GetComponent<HealthInfector>().TearOn = true;
                            }
                        }
                        else if (TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //���� ��
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

            else if (TearType == 3) //�ٸ� Ÿ��
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //���� �ٸ�
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        TakeDown = 0;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //������ �ٸ�
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        TakeDown = 0;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    //������ ���·� �Ѿ���
                    else if (TearCount == 1)
                    {
                        if (LegL == false && LegR == false)
                        {
                            TakeDown = Random.Range(0, 2);
                        }
                    }
                }
                else if (TearPartByOneShot == true) //���� ���⿡ ���� Ÿ��
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //���� �ٸ�
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //������ �ٸ�
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

        else if (Type == true && StartTearing == true)//Ÿ��Ʈ��Ű ����. ���� ü�� ���ϰ� �������� �������� ������ ���ư��� ����.
        {
            if (TearType == 1) //�� Ÿ��
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
            else if (TearType == 2) //�� Ÿ��
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 7);

                    if (TearCount == 0 && TearPartCount == 1 && B1LURend == false) //���� �� ���
                    {
                        if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                        else if (B1LDRend == true)//������ �ϴ� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().HandRout = true; //�������� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LURout = true; //������ ����� ������Ų��.
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

                    else if (TearCount == 0 && TearPartCount == 2 && B1LDRend == false && B1LURend == false) //���� �� �ϴ�
                    {
                        if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ� ����
                            GetComponent<InfectorSpawn>().HandRout = true; //������ ����
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

                    else if (TearCount == 0 && TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //������ ��
                    {
                        GetComponent<InfectorSpawn>().HANDROUTonline = true;
                        GetComponent<InfectorSpawn>().HandRout = true;
                        HANDR = true;
                        RADBlood = true;
                        transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                        GetComponent<HealthInfector>().TearOn = true;
                    }

                    else if (TearCount == 0 && TearPartCount == 4 && B1LULend == false) //���� �� ���
                    {
                        if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                        else if (B1LDLend == true)//���� �ϴ� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().HandLout = true; //�޼��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ��� ������Ų��.
                            GetComponent<InfectorSpawn>().B1LULout = true; //���� ����� ������Ų��.
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
                    else if (TearCount == 0 && TearPartCount == 5 && B1LDLend == false && B1LULend == false) //���� �� �ϴ�
                    {
                        if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                            GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ� ����
                            GetComponent<InfectorSpawn>().HandLout = true; //�޼� ����
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
                    else if (TearCount == 0 && TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //���� ��
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
                else if (TearPartByOneShot == true) //���� ���⿡ ���� Ÿ��
                {
                    TearPartByOneShot = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    Count = Random.Range(1, 4);

                    for (int i = 0; i < Count; i++)
                    {
                        TearPartCount = Random.Range(1, 7);

                        if (TearPartCount == 1 && B1LURend == false) //���� �� ���
                        {
                            if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                            else if (B1LDRend == true)//������ �ϴ� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().HandRout = true; //�������� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LURout = true; //������ ����� ������Ų��.
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

                        else if (TearPartCount == 2 && B1LDRend == false && B1LURend == false) //���� �� �ϴ�
                        {
                            if (HANDR == true) //������ ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().B1LDRout = true; //������ �ϴ� ����
                                GetComponent<InfectorSpawn>().HandRout = true; //������ ����
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

                        else if (TearPartCount == 3 && HANDR == false && B1LDRend == false && B1LURend == false) //������ ��
                        {
                            GetComponent<InfectorSpawn>().HANDROUTonline = true;
                            GetComponent<InfectorSpawn>().HandRout = true;
                            HANDR = true;
                            RADBlood = true;
                            transform.Find("bone_1/bone_2/bone_3/bone_6/Right arm down blood").gameObject.SetActive(true);
                            StartCoroutine(bloodactionFlip);
                            GetComponent<HealthInfector>().TearOn = true;
                        }

                        else if (TearPartCount == 4 && B1LULend == false) //���� �� ���
                        {
                            if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                            else if (B1LDLend == true)//���� �ϴ� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().HandLout = true; //�޼��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ��� ������Ų��.
                                GetComponent<InfectorSpawn>().B1LULout = true; //���� ����� ������Ų��.
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
                        else if (TearPartCount == 5 && B1LDLend == false && B1LULend == false) //���� �� �ϴ�
                        {
                            if (HANDL == true) //���� ���� �̹� �߷������� ���
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
                                GetComponent<InfectorSpawn>().B1LDLout = true; //���� �ϴ� ����
                                GetComponent<InfectorSpawn>().HandLout = true; //�޼� ����
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
                        else if (TearPartCount == 6 && HANDL == false && B1LDLend == false && B1LULend == false) //���� ��
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

            else if (TearType == 3) //�ٸ� Ÿ��
            {
                if (GetComponent<HealthInfector>().ImHit == true && TearPartByOneShot == false)
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //���� �ٸ�
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        TakeDown = 0;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //������ �ٸ�
                    {
                        GetComponent<InfectorSpawn>().RightLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegRout = true;
                        LegR = true;
                        TakeDown = 0;
                        RLBBlood = true;
                        transform.Find("bone_1/Right leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    //������ ���·� �Ѿ���
                    else if (TearCount == 1)
                    {
                        if (LegL == false && LegR == false)
                        {
                            TakeDown = Random.Range(0, 2);
                        }
                    }
                }
                else if (TearPartByOneShot == true) //���� ���⿡ ���� Ÿ��
                {
                    GetComponent<HealthInfector>().ImHit = false; //�� �ѹ��� Ÿ�� �޾Ҵٴ� ������ ó���Ǳ� ���� ����
                    TearCount = Random.Range(0, 2);
                    TearPartCount = Random.Range(1, 3);

                    if (TearCount == 0 && TearPartCount == 1 && LegL == false) //���� �ٸ�
                    {
                        GetComponent<InfectorSpawn>().LeftLegOUTonline = true;
                        GetComponent<InfectorSpawn>().LegLout = true;
                        LegL = true;
                        LLBBlood = true;
                        transform.Find("bone_1/Left leg body blood").gameObject.SetActive(true);
                        StartCoroutine(bloodactionFlip);
                    }
                    else if (TearCount == 0 && TearPartCount == 2 && LegR == false) //������ �ٸ�
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