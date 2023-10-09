using UnityEngine.UI;
using UnityEngine;

public class UpgradeTableInformation1 : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public UpgradeMenu UpgradeMenu;
    public WordPrintSystem WordPrintSystem;
    public WordPrintMenu WordPrintMenu;
    public SystemMessages SystemMessages;

    [Header("���̺� ����")]
    public Image ClickImage;
    public Image Icon1;
    public Sprite UpgradeListIcon;
    public GameObject ClickedPrefab;
    public GameObject BlindPrefab;
    public Text NameText;
    public int MainTabNumber;
    public int SubTabNumber;
    public int AccessNumber;

    [Header("��� ����")]
    public Text UnlockText; //��� �����ϱ� ���� �䱸 ������ ��Ÿ���� ����
    public bool PlanetUnlock = false; //�༺ �ع����� ���� ����
    public bool Unlock = false; //���ŷ� ���� ����
    private int UnlockCost; //���۵� ��� ���
    private int UnlockResource;
    private int UnlockTaritronic;
    private string PlanetName; //������ ���� �׸��� �Ҽӵ� �༺
    private string UnlockName; //��� �̸�

    private bool Click;

    //���̺��� ���� �̸� �� ������ �ҷ�����
    public void TableInformChange(Sprite Icon, string Name, int MainTabNumber, int SubTabNumber, bool PlanetUnolck, bool Unlock, int PlanetNumber, int UnlockPay, int UnlockPay2, int UnlockPay3, string UnlockName, int UpgradeLevel)
    {
        this.PlanetUnlock = PlanetUnolck;
        this.Unlock = Unlock;
        this.UnlockName = UnlockName;
        UnlockCost = UnlockPay;
        UnlockResource = UnlockPay2;
        UnlockTaritronic = UnlockPay3;

        Icon1.GetComponent<Image>().sprite = Icon;
        UpgradeListIcon = Icon;
        NameText.text = Name;
        this.MainTabNumber = MainTabNumber;
        this.SubTabNumber = SubTabNumber;
        UnlockText.text = string.Format("");

        if (PlanetNumber != 0)
            BringPlanetName(PlanetNumber);

        if (PlanetUnolck == false || Unlock == false)
        {
            if (PlanetUnolck == false) //�༺ �ع� ���� ��� ó��
            {
                ClickImage.raycastTarget = false;
                BlindPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    UnlockText.text = string.Format("<color=#FDFF00>The following planet must be liberated to unlock : </color>" + PlanetName);
                else if (WordPrintSystem.LanguageType == 2)
                    UnlockText.text = string.Format("<color=#FDFF00>��� ������ ���� ������ ���� �༺�� �ع�Ǿ�� �մϴ� : </color>" + PlanetName);
            }
            else if (Unlock == false) //��� ������ ���� ��� ����
            {
                ClickImage.raycastTarget = true;
                BlindPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                {
                    if (UnlockPay2 == 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay + " Glopa");
                    else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay + " Glopa, " + UnlockPay2 + " Construction Resource");
                    else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay + " Glopa, " + UnlockPay2 + " Construction Resource, " + UnlockPay3 + " Taritronic");
                    else if (UnlockPay == 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay2 + " Construction Resource");
                    else if (UnlockPay != 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay + " Glopa, " + UnlockPay3 + " Taritronic");
                    else if (UnlockPay == 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay2 + " Construction Resource" + UnlockPay3 + " Taritronic");
                    else if (UnlockPay == 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>Unlock cost</color> : " + UnlockPay3 + " Taritronic");
                }
                else if (WordPrintSystem.LanguageType == 2)
                {
                    if (UnlockPay2 == 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay + " �۷���");
                    else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���");
                    else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���, " + UnlockPay3 + " Ÿ��Ʈ�δ�");
                    else if (UnlockPay == 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay2 + " �Ǽ� ���");
                    else if (UnlockPay != 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay + " �۷���, " + UnlockPay3 + " Ÿ��Ʈ�δ�");
                    else if (UnlockPay == 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay2 + " �Ǽ� ���" + UnlockPay3 + " Ÿ��Ʈ�δ�");
                    else if (UnlockPay == 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>��� ���� ���</color> : " + UnlockPay3 + " Ÿ��Ʈ�δ�");
                }
            }
        }
        else
        {
            if (UpgradeLevel < 3) //���׷��̵� �ѵ��� ���� �� á�� ���, ���� ����
            {
                BlindPrefab.SetActive(false);
                ClickImage.raycastTarget = true;
            }
            else if (UpgradeLevel == 3) //���׷��̵� �ѵ��� �� á�� ���, ���� �Ұ�ó��
            {
                ClickImage.raycastTarget = false;
                BlindPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    UnlockText.text = string.Format("<color=#00FF8C>All research complete</color>");
                else if (WordPrintSystem.LanguageType == 2)
                    UnlockText.text = string.Format("<color=#00FF8C>��� ���� �Ϸ�</color>");
            }
        }
    }

    //Ŭ���� ���̺��� ���׷��̵� ���� ����
    public void EnterUpgradeClick()
    {
        if (PlanetUnlock == true && Unlock == true) //�༺ �ع� �� �ڱ� ����� ���� ����� �����Ǿ�� ��밡��
        {
            UpgradeMenu.AccessUpgrade(MainTabNumber, SubTabNumber, AccessNumber, UpgradeListIcon);
        }
        else if (PlanetUnlock == false && Unlock == false)
        {
            Debug.Log("�ش� �༺�� ���ɵǾ� ����� �Ұ����մϴ�.");
        }
        else if (PlanetUnlock == true && Unlock == false) //�ڱ��� �����Ͽ� ��� ���� ���� �޽����� ����
        {
            UpgradeMenu.UnlockStep = 1;
            WordPrintMenu.UpgradeTableInform(UnlockCost, UnlockResource, UnlockTaritronic);
            StartCoroutine(SystemMessages.MessageOn(2, 0));
            SystemMessages.GlopaorosCostProcess = UnlockCost;
            SystemMessages.ConstructionResourceProcess = UnlockResource;
            SystemMessages.TaritronicProcess = UnlockTaritronic;
            SystemMessages.UnlockName = UnlockName;
        }
    }
    public void EnterUpgradeDown()
    {
        Click = true;
        //SoundManager.instance.SFXPlay2("Sound", Beep5);
        ClickedPrefab.SetActive(true);
    }
    public void EnterUpgradeUp()
    {
        if (Click == true)
        {
            ClickedPrefab.SetActive(false);
        }
        Click = false;
    }
    public void EnterUpgradeEnter()
    {
        if (Click == true)
        {
            //SoundManager.instance.SFXPlay2("Sound", Beep5);
            ClickedPrefab.SetActive(true);
        }
    }
    public void EnterUpgradeExit()
    {
        if (Click == true)
        {
            ClickedPrefab.SetActive(false);
        }
    }

    void BringPlanetName(int AreaNumber)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (AreaNumber == 1002)
                PlanetName = "Aposis";
            else if (AreaNumber == 1005)
                PlanetName = "Vedes VI";
            else if (AreaNumber == 1008)
                PlanetName = "Papatus III";
            else if (AreaNumber == 1012)
                PlanetName = "Derious Heri";
            else if (AreaNumber == 1016)
                PlanetName = "Crown Yosere";
            else if (AreaNumber == 1018)
                PlanetName = "Japet Agrone";
            else if (AreaNumber == 1020)
                PlanetName = "Delta D31-2208";
            else if (AreaNumber == 1022)
                PlanetName = "Delta D31-12721";
            else if (AreaNumber == 1024)
                PlanetName = "Jerato O95-2252";
            else if (AreaNumber == 1025)
                PlanetName = "Jerato O95-8510";
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (AreaNumber == 1002)
                PlanetName = "�����ý�";
            else if (AreaNumber == 1005)
                PlanetName = "������ VI";
            else if (AreaNumber == 1008)
                PlanetName = "�������� III";
            else if (AreaNumber == 1012)
                PlanetName = "�����콺 �츮";
            else if (AreaNumber == 1016)
                PlanetName = "ũ��� �似��";
            else if (AreaNumber == 1018)
                PlanetName = "���� �Ʊ׷γ�";
            else if (AreaNumber == 1020)
                PlanetName = "��Ÿ D31-2208";
            else if (AreaNumber == 1022)
                PlanetName = "��Ÿ D31-12721";
            else if (AreaNumber == 1024)
                PlanetName = "������ O95-2252";
            else if (AreaNumber == 1025)
                PlanetName = "������ O95-8510";
        }
    }
}