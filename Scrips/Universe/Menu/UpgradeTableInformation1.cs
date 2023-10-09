using UnityEngine.UI;
using UnityEngine;

public class UpgradeTableInformation1 : MonoBehaviour
{
    [Header("스크립트")]
    public UpgradeMenu UpgradeMenu;
    public WordPrintSystem WordPrintSystem;
    public WordPrintMenu WordPrintMenu;
    public SystemMessages SystemMessages;

    [Header("테이블 정보")]
    public Image ClickImage;
    public Image Icon1;
    public Sprite UpgradeListIcon;
    public GameObject ClickedPrefab;
    public GameObject BlindPrefab;
    public Text NameText;
    public int MainTabNumber;
    public int SubTabNumber;
    public int AccessNumber;

    [Header("언락 정보")]
    public Text UnlockText; //잠금 해제하기 위한 요구 사항을 나타내는 문구
    public bool PlanetUnlock = false; //행성 해방으로 인한 해제
    public bool Unlock = false; //구매로 인한 해제
    private int UnlockCost; //전송된 언락 비용
    private int UnlockResource;
    private int UnlockTaritronic;
    private string PlanetName; //지정된 연구 항목이 소속된 행성
    private string UnlockName; //언락 이름

    private bool Click;

    //테이블의 연구 이름 및 정보를 불러오기
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
            if (PlanetUnolck == false) //행성 해방 조건 잠금 처리
            {
                ClickImage.raycastTarget = false;
                BlindPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    UnlockText.text = string.Format("<color=#FDFF00>The following planet must be liberated to unlock : </color>" + PlanetName);
                else if (WordPrintSystem.LanguageType == 2)
                    UnlockText.text = string.Format("<color=#FDFF00>잠금 해제를 위해 다음과 같은 행성이 해방되어야 합니다 : </color>" + PlanetName);
            }
            else if (Unlock == false) //비용 지불을 통한 잠금 해제
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
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay + " 글로파");
                    else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료");
                    else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료, " + UnlockPay3 + " 타리트로닉");
                    else if (UnlockPay == 0 && UnlockPay3 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay2 + " 건설 재료");
                    else if (UnlockPay != 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay + " 글로파, " + UnlockPay3 + " 타리트로닉");
                    else if (UnlockPay == 0 && UnlockPay3 != 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay2 + " 건설 재료" + UnlockPay3 + " 타리트로닉");
                    else if (UnlockPay == 0 && UnlockPay2 == 0)
                        UnlockText.text = string.Format("<color=#00FF8C>잠금 해제 비용</color> : " + UnlockPay3 + " 타리트로닉");
                }
            }
        }
        else
        {
            if (UpgradeLevel < 3) //업그레이드 한도가 아직 안 찼을 경우, 연구 가능
            {
                BlindPrefab.SetActive(false);
                ClickImage.raycastTarget = true;
            }
            else if (UpgradeLevel == 3) //업그레이드 한도가 다 찼을 경우, 연구 불가처리
            {
                ClickImage.raycastTarget = false;
                BlindPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    UnlockText.text = string.Format("<color=#00FF8C>All research complete</color>");
                else if (WordPrintSystem.LanguageType == 2)
                    UnlockText.text = string.Format("<color=#00FF8C>모든 연구 완료</color>");
            }
        }
    }

    //클릭한 테이블의 업그레이드 정보 접속
    public void EnterUpgradeClick()
    {
        if (PlanetUnlock == true && Unlock == true) //행성 해방 및 자금 사용을 통한 언락이 해제되어야 사용가능
        {
            UpgradeMenu.AccessUpgrade(MainTabNumber, SubTabNumber, AccessNumber, UpgradeListIcon);
        }
        else if (PlanetUnlock == false && Unlock == false)
        {
            Debug.Log("해당 행성이 점령되어 사용이 불가능합니다.");
        }
        else if (PlanetUnlock == true && Unlock == false) //자금을 지불하여 잠금 해제 절차 메시지를 띄우기
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
                PlanetName = "아포시스";
            else if (AreaNumber == 1005)
                PlanetName = "베데스 VI";
            else if (AreaNumber == 1008)
                PlanetName = "파파투스 III";
            else if (AreaNumber == 1012)
                PlanetName = "데리우스 헤리";
            else if (AreaNumber == 1016)
                PlanetName = "크라운 요세레";
            else if (AreaNumber == 1018)
                PlanetName = "자펫 아그로네";
            else if (AreaNumber == 1020)
                PlanetName = "델타 D31-2208";
            else if (AreaNumber == 1022)
                PlanetName = "델타 D31-12721";
            else if (AreaNumber == 1024)
                PlanetName = "제라토 O95-2252";
            else if (AreaNumber == 1025)
                PlanetName = "제라토 O95-8510";
        }
    }
}