using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FleetFormationMenuSystem : MonoBehaviour
{
    [Header("스크립트")]
    public CameraZoom CameraZoom;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public WordPrintMenu WordPrintMenu;
    public SystemMessages SystemMessages;
    public FleetMenuSystem FleetMenuSystem;
    public TutorialSystem TutorialSystem;

    [Header("탭과 창")]
    public GameObject FormationTransferTab;
    public GameObject FormationTransferWindow;
    public GameObject FormationAddTab;
    public GameObject FormationAddWindow;
    public GameObject Tab1ActivePrefab;
    public GameObject Tab1ClickPrefab;
    public GameObject Tab2ActivePrefab;
    public GameObject Tab2ClickPrefab;
    private bool TabClick = false;

    [Header("함대 메뉴 버튼")]
    public GameObject NextFleetButtonPrefab;
    public bool NextFleetClick;
    public GameObject PreviousFleetButtonPrefab;
    public bool PreviousFleetClick;
    public GameObject CancelFleetButtonPrefab;
    public bool CancelFleetClick;
    public Image CancelFleetImage; //함대 메뉴 나가기 버튼의 레이캐스트
    public bool FleetSizeBig = false; //함대 터치 화면이 확장되었는지에 대한 스위치

    [Header("무기 잠금 전달 변수")]
    public bool PlanetUnlock = false; //행성 해방으로 인한 해제
    public bool Unlock = false; //구매로 인한 해제
    private int UnlockCost; //전송된 언락 비용
    private int UnlockResource;
    private int UnlockTaritronic;

    [Header("함대 배열 수 증가 탭")]
    public GameObject FleetAddButtonPrefab;
    public Image FleetAddButtonImage;
    public bool FleetAddClick;

    [Header("함선 조정(탭1)")]
    public GameObject FleetTransferButtonPrefab;
    public bool FleetTransferClick;
    public GameObject FleetSelectButtonPrefab;
    public bool FleetSelectClick;
    public GameObject FleetSelectCancelButtonPrefab;
    public bool FleetSelectCancelClick;
    public bool FleetSelectMode = false; //함대 선택시, 해당 모드로 전환

    [Header("함선 선택 창(탭1)")]
    public LayerMask FleetShipLayer;
    public int FleetSelectStep = 0; //함선 선택 단계. 1 = 함선 선택, 2 = 함대 선택
    public int SelectedFleetNumber = 0; //선택된 함대 번호
    public Image FleetSelectOkImage;

    [Header("함선 생산 버튼(탭2)")]
    public GameObject FormationProductionButtonPrefab;
    public GameObject ShieldProductionButtonPrefab;
    public GameObject CarrierProductionButtonPrefab;
    public bool ShipPruductionClick;

    [Header("함선 생산 창(탭2)")]
    public int ShipProductionStep = 0; //함선 생산 단계. 1 = 함대 선택, 2 = 수량 및 승인 버튼
    public int ShipProductionNumber = 0; //함선 생산 번호.

    [Header("함선 생산 잠금(탭2)")]
    public GameObject ShieldShipLockPrefab;
    public Image ShieldShipProductionButton;
    public GameObject CarrierLockPrefab;
    public Image CarrierProductionButton;

    [Header("함선 생산 설명창 언락")]
    private int UnlockNumber; //언락 번호
    public GameObject LockPrefab;
    public Image LockImage;
    public Text LockText;

    [Header("함선 창")]
    public Image FleetExitButton; //나가기 버튼
    public int FlagshipNumber = 0; //기함번호
    public int FleetNumber = 0; //함대 메뉴에서 함선을 선택할 때 제어하기 위한 변수

    [Header("애니메이션 및 프리팹")]
    public GameObject FleetFormationMenuPrefab; //함대 관리 메뉴 전체 프리팹
    public GameObject SelectFleetFramePrefab; //함대 선택 화면을 확장시 나타나는 전용 UI
    public GameObject FleetSizeActiveEffectStartPrefab; //함대 선택 화면 확장 애니메이션 시작과 동시에 발생되는 파티클 이펙트 폴더 프리팹
    public GameObject FleetSizeActiveEffectEndPrefab; //함대 선택 화면 확장 애니메이션 종료와 동시에 발생되는 파티클 이펙트 폴더 프리팹

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip OKButtonAudio;
    public AudioClip CancelUIAudio;
    public AudioClip FleetSizeOnAudio;
    public AudioClip FleetSizeOffAudio;
    public AudioClip UnitSelectAudio;

    //탭1(함선 이전 메뉴) 누르기
    public void FormationTransferTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        Tab1ActivePrefab.SetActive(true);
        Tab2ActivePrefab.SetActive(false);
        FormationTransferWindow.SetActive(true);
        FormationAddWindow.SetActive(false);
        WordPrintMenu.FleetMenuShipImage.gameObject.SetActive(false);
    }
    public void FormationTransferTabDown()
    {
        TabClick = true;
        Tab1ClickPrefab.SetActive(true);
    }
    public void FormationTransferTabUp()
    {
        if (TabClick == true)
        {
            Tab1ClickPrefab.SetActive(false);
        }
        TabClick = false;
    }
    public void FormationTransferTabEnter()
    {
        if (TabClick == true)
        {
            Tab1ClickPrefab.SetActive(true);
        }
    }
    public void FormationTransferTabExit()
    {
        if (TabClick == true)
        {
            Tab1ClickPrefab.SetActive(false);
        }
    }

    //탭2(함선 생산 메뉴) 누르기
    public void FormationAddTabClick()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        Tab1ActivePrefab.SetActive(false);
        Tab2ActivePrefab.SetActive(true);
        FormationAddWindow.SetActive(true);
        FormationTransferWindow.SetActive(false);
        ShipUnlockState();
    }
    public void FormationAddTabDown()
    {
        TabClick = true;
        Tab2ClickPrefab.SetActive(true);
    }
    public void FormationAddTabUp()
    {
        if (TabClick == true)
        {
            Tab2ClickPrefab.SetActive(false);
        }
        TabClick = false;
    }
    public void FormationAddTabEnter()
    {
        if (TabClick == true)
        {
            Tab2ClickPrefab.SetActive(true);
        }
    }
    public void FormationAddTabExit()
    {
        if (TabClick == true)
        {
            Tab2ClickPrefab.SetActive(false);
        }
    }

    //함선별 설명창 클릭
    public void FormationShipExplainClick()
    {
        WordPrintMenu.FleetFormationMenuShipExplainPrintNoClick(1, UpgradeDataSystem.instance.FormationHitPoints);
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");
    }
    public void ShieldShipExplainClick()
    {
        WordPrintMenu.FleetFormationMenuShipExplainPrintNoClick(2, UpgradeDataSystem.instance.ShieldShipHitPoints);

        if (WeaponUnlockManager.instance.ShieldShipUnlock == false || WeaponUnlockManager.instance.AposisLabUnlock == false)
        {
            LockPrefab.SetActive(true);
            UnlockNumber = 1;
            if (WeaponUnlockManager.instance.AposisLabUnlock == false)
            {
                LockImage.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    LockText.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Aposis");
                else if (BattleSave.Save1.LanguageType == 2)
                    LockText.text = string.Format("<color=#FDFF00>필요한 해방된 행성 : </color>아포시스");
            }
            else if (WeaponUnlockManager.instance.ShieldShipUnlock == false)
            {
                LockImage.raycastTarget = true;
                UnlockPay(LockText, 1000, 0, 3300);
            }
        }
        else
        {
            LockPrefab.SetActive(false);
            LockText.text = string.Format("");
        }
    }
    public void CarrierExplainClick()
    {
        WordPrintMenu.FleetFormationMenuShipExplainPrintNoClick(3, UpgradeDataSystem.instance.CarrierHitPoints);

        if (WeaponUnlockManager.instance.CarrierUnlock == false || WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
        {
            LockPrefab.SetActive(true);
            UnlockNumber = 2;
            if (WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
            {
                LockImage.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    LockText.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Crown Yosere");
                else if (BattleSave.Save1.LanguageType == 2)
                    LockText.text = string.Format("<color=#FDFF00>필요한 해방된 행성 : </color>크라운 요세레");
            }
            else if (WeaponUnlockManager.instance.CarrierUnlock == false)
            {
                LockImage.raycastTarget = true;
                UnlockPay(LockText, 2200, 0, 5800);
            }
        }
        else
        {
            LockPrefab.SetActive(false);
            LockText.text = string.Format("");
        }
    }

    //자금을 지불하여 잠금 해제 절차 메시지를 띄우기
    public void EnterUnlockClick()
    {
        SystemMessages.GlopaorosCostProcess = UnlockCost;
        SystemMessages.ConstructionResourceProcess = UnlockResource;
        SystemMessages.TaritronicProcess = UnlockTaritronic;
        SystemMessages.WeaponUnlockStep = 2;
        WordPrintMenu.UpgradeTableInform(UnlockCost, UnlockResource, UnlockTaritronic);
        StartCoroutine(SystemMessages.MessageOn(2, 0));
    }

    //해당 지불 가격을 지정하여 지불이 정상적으로 이루어지도록 하기 위함
    public void UnlockDown()
    {
        if (UnlockNumber == 1)
        {
            UnlockCost = 1000;
            UnlockResource = 0;
            UnlockTaritronic = 3300;
            SystemMessages.UnlockName = "Shield Ship Unlock";
        }
        else if (UnlockNumber == 2)
        {
            UnlockCost = 2200;
            UnlockResource = 0;
            UnlockTaritronic = 5800;
            SystemMessages.UnlockName = "Carrier Unlock";
        }
    }

    //함선 생산 잠금 해제 상태 불러오기
    public void ShipUnlockState()
    {
        //방패함 언락
        if (WeaponUnlockManager.instance.ShieldShipUnlock == false || WeaponUnlockManager.instance.AposisLabUnlock == false)
        {
            if (WeaponUnlockManager.instance.AposisLabUnlock == false)
            {
                ShieldShipProductionButton.raycastTarget = false;
                ShieldShipLockPrefab.SetActive(true);
            }
            else if (WeaponUnlockManager.instance.ShieldShipUnlock == false)
            {
                ShieldShipProductionButton.raycastTarget = false;
                ShieldShipLockPrefab.SetActive(true);
            }
        }
        else
        {
            ShieldShipProductionButton.raycastTarget = true;
            ShieldShipLockPrefab.SetActive(false);
        }

        //우주모함 언락
        if (WeaponUnlockManager.instance.CarrierUnlock == false || WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
        {
            if (WeaponUnlockManager.instance.CrownYosereLabUnlock == false)
            {
                CarrierProductionButton.raycastTarget = false;
                CarrierLockPrefab.SetActive(true);
            }
            else if (WeaponUnlockManager.instance.CarrierUnlock == false)
            {
                CarrierProductionButton.raycastTarget = false;
                CarrierLockPrefab.SetActive(true);
            }
        }
        else
        {
            CarrierProductionButton.raycastTarget = true;
            CarrierLockPrefab.SetActive(false);
        }
    }

    //지불을 통한 언락
    void UnlockPay(Text text, int UnlockPay, int UnlockPay2, int UnlockPay3)
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            if (UnlockPay2 == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa, " + UnlockPay2 + " Construction Resource" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa, " + UnlockPay2 + " Construction Resource, " + UnlockPay3 + " Taritronic" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay2 + " Construction Resource" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay != 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa, " + UnlockPay3 + " Taritronic" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay2 + " Construction Resource" + UnlockPay3 + " Taritronic" + "\n<color=#FCFF00>Click here to pay</color>");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay3 + " Taritronic" + "\n<color=#FCFF00>Click here to pay</color>");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (UnlockPay2 == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료, " + UnlockPay3 + " 타리트로닉" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay2 + " 건설 재료" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay != 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파, " + UnlockPay3 + " 타리트로닉" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay2 + " 건설 재료" + UnlockPay3 + " 타리트로닉" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay3 + " 타리트로닉" + "\n<color=#FCFF00>여기를 클릭하여 지불</color>");
        }
    }

    //언락 시작
    public void UnlockStart(string UnlockName)
    {
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");

        if (UnlockName == "Shield Ship Unlock")
            WeaponUnlockManager.instance.ShieldShipUnlock = true;
        else if (UnlockName == "Carrier Unlock")
            WeaponUnlockManager.instance.CarrierUnlock = true;
    }

    //다음 함대 선택
    public void NextFleetButtonClick()
    {
        NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", true);
        SelectedShipInitialize();
        if (FleetNumber < ShipManager.instance.FlagShipList.Count - 1)
            FleetNumber++;
        else
            FleetNumber = 0;
        if (FleetSelectStep == 2 && FleetNumber == SelectedFleetNumber)
            FleetNumber++;

        FlagshipNumber = FleetNumber;
        CameraZoom.CVCamera.m_Lens.OrthographicSize = 15;
        CameraZoom.CVCamera.Follow = ShipManager.instance.FleetShipList[FleetNumber].transform;
        ShipManager.instance.SelectedFleetFlagship[0] = ShipManager.instance.FleetShipList[FleetNumber];
        WordPrintMenu.FleetFormationTotalInformationPrint(FleetNumber);
        WordPrintMenu.FleetFormationManagerPrint(FleetNumber);
        if (FleetSelectStep > 0)
        {
            WordPrintMenu.FleetFormationTransferNamePrint(FleetNumber);
            if (ShipManager.instance.SelectedFleetShips.Count == 0) //함선 미선택시 OK버튼 무력화
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            else
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
        }
        if (ShipProductionStep > 0)
            WordPrintMenu.FleetFormationSelectedFleetNamePrint(FleetNumber);
    }
    public void NextFleetButtonDown()
    {
        NextFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", false);
        NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", true);
    }
    public void NextFleetButtonUp()
    {
        if (NextFleetClick == true)
        {
            NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", false);
        }
        NextFleetClick = false;
    }
    public void NextFleetButtonEnter()
    {
        if (NextFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", true);
        }
    }
    public void NextFleetButtonExit()
    {
        if (NextFleetClick == true)
        {
            NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", false);
        }
    }

    //이전 함대 선택
    public void PreviousFleetButtonClick()
    {
        PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", true);
        SelectedShipInitialize();
        if (FleetNumber <= ShipManager.instance.FlagShipList.Count - 1 && FleetNumber > 0)
            FleetNumber--;
        else if (FleetNumber <= 0)
            FleetNumber = ShipManager.instance.FlagShipList.Count - 1;
        if (FleetSelectStep == 2 && FleetNumber == SelectedFleetNumber)
            FleetNumber--;

        FlagshipNumber = FleetNumber;
        CameraZoom.CVCamera.m_Lens.OrthographicSize = 15;
        CameraZoom.CVCamera.Follow = ShipManager.instance.FleetShipList[FleetNumber].transform;
        ShipManager.instance.SelectedFleetFlagship[0] = ShipManager.instance.FleetShipList[FleetNumber];
        WordPrintMenu.FleetFormationTotalInformationPrint(FleetNumber);
        WordPrintMenu.FleetFormationManagerPrint(FleetNumber);
        if (FleetSelectStep > 0)
        {
            WordPrintMenu.FleetFormationTransferNamePrint(FleetNumber);
            if (ShipManager.instance.SelectedFleetShips.Count == 0) //함선 미선택시 OK버튼 무력화
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            else
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
        }
        if (ShipProductionStep > 0)
            WordPrintMenu.FleetFormationSelectedFleetNamePrint(FleetNumber);
    }
    public void PreviousFleetButtonDown()
    {
        PreviousFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", false);
        PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", true);
    }
    public void PreviousFleetButtonUp()
    {
        if (PreviousFleetClick == true)
        {
            PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", false);
        }
        PreviousFleetClick = false;
    }
    public void PreviousFleetButtonEnter()
    {
        if (PreviousFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", true);
        }
    }
    public void PreviousFleetButtonExit()
    {
        if (PreviousFleetClick == true)
        {
            PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet next select button", false);
        }
    }

    //취소 후, 다시 메인메뉴 창으로 돌아가기
    public void CancelFleetButtonClick()
    {
        StartCoroutine(Exit());
    }

    //나가기
    IEnumerator Exit()
    {
        SystemMessages.MessageType = 0;
        SystemMessages.MessageNumber = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        if (FleetSizeBig == false)
        {
            FleetExitButton.raycastTarget = true;
            FleetSizeBig = false;
        }

        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 0);
        FleetFormationMenuPrefab.GetComponent<Animator>().SetFloat("Fleet select active, Fleet formation menu", 0);

        MainMenuButtonSystem.MenuButtonAnim.SetActive(true); //메인 메뉴 버튼 활성화(타임스케일이 0인 상태에서 해당 오브젝트의 스크립트가 비활성화되어 있을 경우, 코루틴의 WaitForSecondsRealtime이 작동을 멈출 수 있으므로 먼저 가동)
        MainMenuButtonSystem.FleetFormationMenuWindow.SetActive(false);
        MainMenuButtonSystem.FleetFormationMenuMode = false;
        CameraZoom.TurnOffCameraFleetMenu();
        StartCoroutine(CameraZoom.ExitingAnimation());
        for (int i = 0; i < ShipManager.instance.FleetShipList.Count; i++)
        {
            for (int j = 0; j < ShipManager.instance.FleetShipList[i].GetComponent<FleetMenuShipNumber>().FollowShipList.Count; j++)
            {
                Destroy(ShipManager.instance.FleetShipList[i].GetComponent<FleetMenuShipNumber>().FollowShipList[j].gameObject);
            }
            Destroy(ShipManager.instance.FleetShipList[i]);
        }
        ShipManager.instance.FleetShipList.Clear();
        ShipManager.instance.SelectedFleetFlagship.Clear();
        FleetMenuSystem.FleetBackground.SetActive(false);
    }

    public void CancelFleetButtonDown()
    {
        CancelFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelFleetButtonUp()
    {
        if (CancelFleetClick == true)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelFleetClick = false;
    }
    public void CancelFleetButtonEnter()
    {
        if (CancelFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelFleetButtonExit()
    {
        if (CancelFleetClick == true)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //함대 최대 배열 증가 버튼(탭1)
    public void FleetAddButtonClick()
    {
        SystemMessages.MessageType = 2;
        SystemMessages.MessageNumber = 1;
        UpgradeDataSystem.instance.GlopaorosCost = 1000;
        UpgradeDataSystem.instance.ConstructionResourceCost = 0;
        UpgradeDataSystem.instance.TaritronicCost = 0;
        WordPrintMenu.FleetFormationManagerFleetAddProgressPrint(FleetNumber);
        WordPrintMenu.FleetFormationManagerFleetAddMessagePrint(FleetNumber);
        StartCoroutine(SystemMessages.MessageOn(1, 2));
    }
    public void FleetAddButtonDown()
    {
        FleetAddClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        FleetAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void FleetAddButtonUp()
    {
        if (FleetAddClick == true)
        {
            FleetAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        FleetAddClick = false;
    }
    public void FleetAddButtonEnter()
    {
        if (FleetAddClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            FleetAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void FleetAddButtonExit()
    {
        if (FleetAddClick == true)
        {
            FleetAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //함선 이전 버튼(탭1)
    public void FleetTransferButtonClick()
    {
        FleetSelectStep = 1;
        FleetSelectMode = true;
        WordPrintMenu.FleetSelectShipsPrint();
        WordPrintMenu.FleetFormationTransferNamePrint(FleetNumber);
        StartCoroutine(SelectFleetStart());
    }
    public void FleetTransferButtonDown()
    {
        FleetTransferClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        FleetTransferButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void FleetTransferButtonUp()
    {
        if (FleetTransferClick == true)
        {
            FleetTransferButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        FleetTransferClick = false;
    }
    public void FleetTransferButtonEnter()
    {
        if (FleetTransferClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            FleetTransferButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void FleetTransferButtonExit()
    {
        if (FleetTransferClick == true)
        {
            FleetTransferButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //함대 선택 창 활성화 애니메이션
    public IEnumerator SelectFleetStart()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", FleetSizeOnAudio);
        FleetSizeBig = true;
        CancelFleetImage.raycastTarget = false;
        FleetSizeActiveEffectStartPrefab.SetActive(true);
        FleetFormationMenuPrefab.GetComponent<Animator>().SetFloat("Fleet select active, Fleet formation menu", 1);
        CameraZoom.CameraPrefab.GetComponent<Animator>().SetBool("Fleet online, Camera", true);
        yield return new WaitForSecondsRealtime(0.05f);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        yield return new WaitForSecondsRealtime(0.25f);
        if (FleetSelectStep > 0)
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 100);
        else if (ShipProductionStep > 0)
            MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 1);
        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 1);

        if (FleetSelectStep > 0)
        {
            if (ShipManager.instance.SelectedFleetShips.Count == 0) //함선 미선택시 OK버튼 무력화
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            else
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
        }
        else if (ShipProductionStep > 0)
        {
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList.Count == ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().FormationStorage)
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            else
                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
        }

        yield return new WaitForSecondsRealtime(0.25f);

        FleetSizeActiveEffectStartPrefab.SetActive(false);
        CameraZoom.CameraPrefab.GetComponent<Animator>().SetBool("Fleet online, Camera", false);
        CameraZoom.CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
    }
    public IEnumerator SelectFleetEnd()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", FleetSizeOffAudio);
        FleetSizeBig = false;
        CameraZoom.CameraPrefab.GetComponent<Animator>().SetBool("Fleet online, Camera", false);
        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 2);
        yield return new WaitForSecondsRealtime(0.1f);
        FleetSizeActiveEffectEndPrefab.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);
        FleetFormationMenuPrefab.GetComponent<Animator>().SetFloat("Fleet select active, Fleet formation menu", 2);
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 2);
        yield return new WaitForSecondsRealtime(0.3f);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        CameraZoom.CameraPrefab.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        FleetSizeActiveEffectEndPrefab.SetActive(false);
        yield return new WaitForSecondsRealtime(0.16f);
        CancelFleetImage.raycastTarget = true;
    }

    //함선 선택 버튼(탭1, 탭2)
    public void FleetSelectOKButtonClick()
    {
        if (FleetSelectStep == 1) //함대 선택으로 넘어가기
        {
            FleetSelectStep++;
            SelectedFleetNumber = FleetNumber;
            NextFleetButtonClick();
            WordPrintMenu.FleetSelectShipsPrint();
        }
        else if (FleetSelectStep == 2) //함선 이전 최종 승인 메시지 출력
        {
            SystemMessages.MessageType = 2;
            SystemMessages.MessageNumber = 2;
            WordPrintMenu.FleetFormationManagerFleetAddMessagePrint(FleetNumber);
            StartCoroutine(SystemMessages.MessageOn(2, 0));
        }
        else if (ShipProductionStep == 1) //생산 승인 메시지 출력
        {
            ShipProductionStep++;
            SystemMessages.MessageType = 2;
            SystemMessages.MessageNumber = 3;
            SystemMessages.AmountNumber = 1;
            SystemMessages.AmountNumberText.text = string.Format("{0}", SystemMessages.AmountNumber);
            WordPrintMenu.FleetFormationManagerFleetAddProgressPrint(FleetNumber);
            WordPrintMenu.FleetFormationManagerFleetAddMessagePrint(FleetNumber);
            StartCoroutine(SystemMessages.MessageOn(3, 1));
        }
    }
    public void FleetSelectOKButtonDown()
    {
        FleetSelectClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void FleetSelectOKButtonUp()
    {
        if (FleetSelectClick == true)
        {
            FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        FleetSelectClick = false;
    }
    public void FleetSelectOKButtonEnter()
    {
        if (FleetSelectClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void FleetSelectOKButtonExit()
    {
        if (FleetSelectClick == true)
        {
            FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //함선 선택 취소(탭1, 탭2)
    public void FleetSelectCancelButtonClick()
    {
        if (FleetSelectStep == 1) //함선 선택창을 취소하고 다시 메뉴창으로 되돌아가기
        {
            FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
            FleetSelectStep = 0;
            SelectedFleetNumber = 0;
            SelectedShipInitialize();
            FleetSelectMode = false;
            FleetSelectOkImage.raycastTarget = false;
            StartCoroutine(SelectFleetEnd());
        }
        else if (FleetSelectStep == 2) //함선 선택으로 되돌아가기
        {
            CameraZoom.CVCamera.m_Lens.OrthographicSize = 15;
            CameraZoom.CVCamera.Follow = ShipManager.instance.FleetShipList[SelectedFleetNumber].transform;
            ShipManager.instance.SelectedFleetFlagship[0] = ShipManager.instance.FleetShipList[SelectedFleetNumber];

            for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
            {
                ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(true);
            }
            FleetSelectStep--;
            WordPrintMenu.FleetSelectShipsPrint();
        }
        else if (ShipProductionStep == 1) //함선 선택창을 취소하고 다시 메뉴창으로 되돌아가기
        {
            FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
            ShipProductionStep = 0;
            SelectedShipInitialize();
            SystemMessages.FirstAmountButtonPrefab.SetActive(false);
            FleetSelectOkImage.raycastTarget = false;
            StartCoroutine(SelectFleetEnd());
        }
    }
    public void FleetSelectCancelButtonDown()
    {
        FleetSelectCancelClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        FleetSelectCancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void FleetSelectCancelButtonUp()
    {
        if (FleetSelectCancelClick == true)
        {
            FleetSelectCancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        FleetSelectCancelClick = false;
    }
    public void FleetSelectCancelButtonEnter()
    {
        if (FleetSelectCancelClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            FleetSelectCancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void FleetSelectCancelButtonExit()
    {
        if (FleetSelectCancelClick == true)
        {
            FleetSelectCancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //함선 생산 버튼
    public void ShipProductionButtonClick()
    {
        SystemMessages.MessageType = 2;
        SystemMessages.MessageNumber = 3;
        FleetSelectOkImage.raycastTarget = true;
        ShipProductionStep = 1;
        WordPrintMenu.FleetSelectShipsPrint();
        WordPrintMenu.FleetFormationSelectedFleetNamePrint(FleetNumber);
        StartCoroutine(SelectFleetStart());
    }
    public void ShipProductionButtonDownFormation() //편대함 생산 버튼
    {
        ShipPruductionClick = true;
        ShipProductionNumber = 1;
        UpgradeDataSystem.instance.GlopaorosCost = 600;
        UpgradeDataSystem.instance.ConstructionResourceCost = 2500;
        UpgradeDataSystem.instance.TaritronicCost = 0;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        //SelectedProductionShipNumber();
    }
    public void ShipProductionButtonDownShield() //방패함 생산 버튼
    {
        ShipPruductionClick = true;
        ShipProductionNumber = 2;
        UpgradeDataSystem.instance.GlopaorosCost = 900;
        UpgradeDataSystem.instance.ConstructionResourceCost = 3000;
        UpgradeDataSystem.instance.TaritronicCost = 0;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        //SelectedProductionShipNumber();
    }
    public void ShipProductionButtonDownCarrier() //우주모함 생산 버튼
    {
        ShipPruductionClick = true;
        ShipProductionNumber = 3;
        UpgradeDataSystem.instance.GlopaorosCost = 1400;
        UpgradeDataSystem.instance.ConstructionResourceCost = 6200;
        UpgradeDataSystem.instance.TaritronicCost = 0;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        //SelectedProductionShipNumber();
    }
    void SelectedProductionShipNumber()
    {
        if (ShipProductionNumber == 1)
            FormationProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
        else if (ShipProductionNumber == 2)
            ShieldProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
        else if (ShipProductionNumber == 3)
            CarrierProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
    }
    public void ShipProductionButtonUp()
    {
        if (ShipPruductionClick == true)
        {
            if (ShipProductionNumber == 1)
                FormationProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
            else if (ShipProductionNumber == 2)
                ShieldProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
            else if (ShipProductionNumber == 3)
                CarrierProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
        }
        ShipPruductionClick = false;
    }
    public void ShipProductionButtonEnter()
    {
        if (ShipPruductionClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            if (ShipProductionNumber == 1)
                FormationProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
            else if (ShipProductionNumber == 2)
                ShieldProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
            else if (ShipProductionNumber == 3)
                CarrierProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
        }
    }
    public void ShipProductionButtonExit()
    {
        if (ShipPruductionClick == true)
        {
            if (ShipProductionNumber == 1)
                FormationProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
            else if (ShipProductionNumber == 2)
                ShieldProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
            else if (ShipProductionNumber == 3)
                CarrierProductionButtonPrefab.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", false);
        }
    }

    //함대 배열 부팅
    public void StartFleetFormationMenu()
    {
        StartCoroutine(CameraZoom.BootingAnimation());
        StartCoroutine(CameraZoom.TurnCameraFleetMenu());
        StartCoroutine(FleetFormationMenuWindowOpen());
        FleetMenuSystem.FleetBackground.SetActive(true);
        FleetMenuSystem.FleetGeneratorStart();
        Tab1ActivePrefab.SetActive(true);
        Tab2ActivePrefab.SetActive(false);

        LockPrefab.SetActive(false);
        LockText.text = string.Format("");
        WordPrintMenu.FleetMenuShipImage.gameObject.SetActive(false);
    }

    //함대 배열 메뉴 작동
    public IEnumerator FleetFormationMenuWindowOpen()
    {
        WordPrintMenu.FleetFormationTotalInformationPrint(FleetNumber);
        FormationTransferTabClick();
        yield return new WaitForSecondsRealtime(0.55f);
        MainMenuButtonSystem.FleetFormationMenuWindow.SetActive(true); //함대 메뉴창 활성화
        WordPrintMenu.FleetFormationManagerPrint(FleetNumber);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);

        if (BattleSave.Save1.FleetFormationTutorial == true) //첫 튜토리얼
        {
            StartCoroutine(TutorialSystem.TutorialWindowOpen(101));
        }
        yield return new WaitForSecondsRealtime(1.05f);
        if (FleetSizeBig == false)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
            FleetExitButton.raycastTarget = true; //함대 메뉴 나가기 버튼 활성화
        }
    }

    private void Update()
    {
        if (FleetSelectMode == true && FleetSelectStep == 1)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended) //유닛 선택
                {
                    UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", UnitSelectAudio);
                    Vector3 StartPosition = GetMouseWorldPosition();
                    StartPosition = new Vector2(StartPosition.x - (0.25f / 2), StartPosition.y + (0.25f / 2));
                    Vector3 currentMousePosition = new Vector2(StartPosition.x + 0.25f, StartPosition.y - 0.25f);

                    Collider2D collider2D = Physics2D.OverlapArea(StartPosition, currentMousePosition, FleetShipLayer);
                    if (collider2D != null)
                    {
                        if (collider2D.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.activeSelf == false)
                        {
                            if (collider2D.gameObject.GetComponent<FleetMenuShipNumber>().isSelected == false) //선택된 함선 활성화
                            {
                                collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(true);
                                ShipManager.instance.SelectedFleetShips.Add(collider2D.gameObject);
                                FleetSelectOkImage.raycastTarget = true;
                            }
                            else //이미 선택되었던 함선이 다시 선택되었을 경우, 비활성화
                            {
                                collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(false);
                                ShipManager.instance.SelectedFleetShips.Remove(collider2D.gameObject);

                                if (ShipManager.instance.SelectedFleetShips.Count == 0)
                                    FleetSelectOkImage.raycastTarget = false;
                            }

                            if (ShipManager.instance.SelectedFleetShips.Count == 0) //함선 미선택시 OK버튼 무력화
                                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
                            else
                                FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);

                            collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SwitchSelect();
                        }
                    }
                }
            }
        }
    }

    //버튼을 누른 지역의 위치를 변환
    Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    //선택한 함선을 초기화
    public void SelectedShipInitialize()
    {
        if (FleetSelectStep != 2)
        {
            for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
            {
                ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(false);
            }
            ShipManager.instance.SelectedFleetShips.Clear();
        }
    }
}