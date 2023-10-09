using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FlagshipManagerSystem : MonoBehaviour
{
    [Header("스크립트")]
    public CameraZoom CameraZoom;
    public SystemMessages SystemMessages;
    public WordPrintMenu WordPrintMenu;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public TutorialSystem TutorialSystem;
    public MultiFlagshipSystem MultiFlagshipSystem;

    [Header("기함 메뉴 버튼")]
    public GameObject CancelFleetButtonPrefab;
    public bool CancelFleetClick;

    [Header("기함 메뉴 창")]
    public Image FlagshipManagerExitButton; //나가기 버튼
    public int FleetNumber = 0; //함대 메뉴에서 함선을 선택할 때 제어하기 위한 변수
    public int FlagshipSkillType = 0; //1 = 기함 단독 공격, 2 = 함대 공격, 3 = 기함 지원
    public int SkillNumber = 0; //위 각 스킬 슬롯 번호

    [Header("기함 목록")]
    public RectTransform FlagshipList1;
    public RectTransform FlagshipList2;
    public RectTransform FlagshipList3;
    public RectTransform FlagshipList4;
    public RectTransform FlagshipList5;
    public GameObject FlagshipList1Click;
    public GameObject FlagshipList2Click;
    public GameObject FlagshipList3Click;
    public GameObject FlagshipList4Click;
    public GameObject FlagshipList5Click;

    public Text Flagship1Name;
    public Text Flagship2Name;
    public Text Flagship3Name;
    public Text Flagship4Name;
    public Text Flagship5Name;
    public Text Flagship1Ships;
    public Text Flagship2Ships;
    public Text Flagship3Ships;
    public Text Flagship4Ships;
    public Text Flagship5Ships;

    [Header("기함 스킬 슬롯")]
    public GameObject SkillSlot1;
    public GameObject SkillSlot2;
    public GameObject SkillSlot3;
    private int SlotNumber; //슬롯 번호
    public bool SlotInput = false; //장비 슬롯에 장착할 수 있는지 여부

    [Header("무기 잠금 전달 변수")]
    private int UnlockNumber; //언락 번호
    public bool PlanetUnlock = false; //행성 해방으로 인한 해제
    public bool Unlock = false; //구매로 인한 해제
    private int UnlockCost; //전송된 언락 비용
    private int UnlockResource;
    private int UnlockTaritronic;
    public Text LockText;

    [Header("스킬 아이템")]
    public GameObject FlagshipAttackSkillItem1; //함포 아이템 슬롯(터치 추적용)
    public GameObject FleetAttackSkillItem1;
    public GameObject FlagshipSupportSkillItem1;

    public GameObject FlagshipAttackSkill1Click;
    public GameObject FleetAttackSkill1Click;
    public GameObject FlagshipSupportSkill1Click;

    public GameObject FlagshipAttackTabClick;
    public GameObject FormationAttackTabClick;
    public GameObject FlagshipSupportTabClick;
    public GameObject FlagshipAttackTabClicked;
    public GameObject FormationAttackTabClicked;
    public GameObject FlagshipSupportTabClicked;
    private bool TabClick;

    [Header("기함 새배치")]
    public GameObject FlagshipAddButtonPrefab;
    public bool FlagshipAddClick;

    [Header("슬롯 잠금")]
    public Text SkillSlot1Text;
    public Image SkillSlot1Image;
    public Text SkillSlot2Text;
    public Image SkillSlot2Image;
    public Text SkillSlot3Text;
    public Image SkillSlot3Image;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;
    public AudioClip SlotChangeAudio;

    //취소 후, 다시 메인메뉴 창으로 돌아가기
    public void CancelFleetButtonClick()
    {
        StartCoroutine(Exit());
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

    //나가기
    IEnumerator Exit()
    {
        SystemMessages.MessageType = 0;
        SystemMessages.MessageNumber = 0;
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        yield return new WaitForSecondsRealtime(0.1f);

        //스킬 슬롯 애니메이션 초기화
        if (SkillSlot1.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (SkillSlot2.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (SkillSlot3.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);

        MainMenuButtonSystem.MenuButtonAnim.SetActive(true); //메인 메뉴 버튼 활성화
        MainMenuButtonSystem.FlagshipMenuWindow.SetActive(false);
        MainMenuButtonSystem.FlagshipMenuMode = false;
        StartCoroutine(CameraZoom.ExitingAnimation());
    }

    //기함 목록 가져오기 및 스킬 리스트 초기화
    public void FlagshipListLoad()
    {
        for(int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            if (i == 0)
            {
                FlagshipList1.gameObject.SetActive(true);
                Flagship1Name.text = MultiFlagshipSystem.Player1Name.text;
                //Flagship1Name.text = ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().PlayerName;
                //Flagship1Ships.text = string.Format("{0}", ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().NumberOfShips);
            }
            else if (i == 1)
            {
                FlagshipList2.gameObject.SetActive(true);
                Flagship2Name.text = MultiFlagshipSystem.Player2Name.text;
                //Flagship2Name.text = ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().PlayerName;
                //Flagship2Ships.text = string.Format("{0}", ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().NumberOfShips);
            }
            else if (i == 2)
            {
                FlagshipList3.gameObject.SetActive(true);
                Flagship3Name.text = MultiFlagshipSystem.Player3Name.text;
                //Flagship3Name.text = ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().PlayerName;
                //Flagship3Ships.text = string.Format("{0}", ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().NumberOfShips);
            }
            else if (i == 3)
            {
                FlagshipList4.gameObject.SetActive(true);
                Flagship4Name.text = MultiFlagshipSystem.Player4Name.text;
                //Flagship4Name.text = ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().PlayerName;
                //Flagship4Ships.text = string.Format("{0}", ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().NumberOfShips);
            }
            else if (i == 4)
            {
                FlagshipList5.gameObject.SetActive(true);
                Flagship5Name.text = MultiFlagshipSystem.Player5Name.text;
                //Flagship5Name.text = ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().PlayerName;
                //Flagship5Ships.text = string.Format("{0}", ShipManager.instance.FlagShipList[i].GetComponent<FlagshipNameInformation>().NumberOfShips);
            }
        }
        FlagshipAttackTabSelect();
    }

    //기함 공격 탭(탭1) 선택
    public void FlagshipAttackTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(true);
        FormationAttackTabClicked.SetActive(false);
        FlagshipSupportTabClicked.SetActive(false);
        WordPrintMenu.FlagshipMenuSlotExplain(1);

        //스킬 슬롯 활성화
        SkillSlotsTurnOff();
        FlagshipAttackSkillItem1.SetActive(true);
    }
    public void FlagshipAttackTabDown()
    {
        TabClick = true;
        FlagshipAttackTabClick.SetActive(true);
    }
    public void FlagshipAttackTabUp()
    {
        if (TabClick == true)
        {
            FlagshipAttackTabClick.SetActive(false);
        }
        TabClick = false;
    }
    public void FlagshipAttackTabEnter()
    {
        if (TabClick == true)
        {
            FlagshipAttackTabClick.SetActive(true);
        }
    }
    public void FlagshipAttackTabExit()
    {
        if (TabClick == true)
        {
            FlagshipAttackTabClick.SetActive(false);
        }
    }

    //함대 공격 탭(탭2) 선택
    public void FormationAttackTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(false);
        FormationAttackTabClicked.SetActive(true);
        FlagshipSupportTabClicked.SetActive(false);
        WordPrintMenu.FlagshipMenuSlotExplain(2);

        //스킬 슬롯 활성화
        SkillSlotsTurnOff();
        FleetAttackSkillItem1.SetActive(true);
    }
    public void FormationAttackTabDown()
    {
        TabClick = true;
        FormationAttackTabClick.SetActive(true);
    }
    public void FormationAttackTabUp()
    {
        if (TabClick == true)
        {
            FormationAttackTabClick.SetActive(false);
        }
        TabClick = false;
    }
    public void FormationAttackTabEnter()
    {
        if (TabClick == true)
        {
            FormationAttackTabClick.SetActive(true);
        }
    }
    public void FormationAttackTabExit()
    {
        if (TabClick == true)
        {
            FormationAttackTabClick.SetActive(false);
        }
    }

    //기함 지원 탭(탭3) 선택
    public void FlagshipSupportTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(false);
        FormationAttackTabClicked.SetActive(false);
        FlagshipSupportTabClicked.SetActive(true);

        //스킬 슬롯 활성화
        SkillSlotsTurnOff();
    }
    public void FlagshipSupportTabDown()
    {
        TabClick = true;
        FlagshipSupportTabClick.SetActive(true);
    }
    public void FlagshipSupportTabUp()
    {
        if (TabClick == true)
        {
            FlagshipSupportTabClick.SetActive(false);
        }
        TabClick = false;
    }
    public void FlagshipSupportTabEnter()
    {
        if (TabClick == true)
        {
            FlagshipSupportTabClick.SetActive(true);
        }
    }
    public void FlagshipSupportTabExit()
    {
        if (TabClick == true)
        {
            FlagshipSupportTabClick.SetActive(false);
        }
    }

    //모든 스킬 슬롯 끄기
    void SkillSlotsTurnOff()
    {
        FlagshipAttackSkillItem1.SetActive(false);
        FleetAttackSkillItem1.SetActive(false);
    }

    //기함 선택
    public void Flagship1SelectClick()
    {
        FleetNumber = 0;
        CancelListClick();
        FlagshipList1Click.SetActive(true);
        SlotActivated();
    }
    public void Flagship2SelectClick()
    {
        FleetNumber = 1;
        CancelListClick();
        FlagshipList2Click.SetActive(true);
        SlotActivated();
    }
    public void Flagship3SelectClick()
    {
        FleetNumber = 2;
        CancelListClick();
        FlagshipList3Click.SetActive(true);
        SlotActivated();
    }
    public void Flagship4SelectClick()
    {
        FleetNumber = 3;
        CancelListClick();
        FlagshipList4Click.SetActive(true);
        SlotActivated();
    }
    public void Flagship5SelectClick()
    {
        FleetNumber = 4;
        CancelListClick();
        FlagshipList5Click.SetActive(true);
        SlotActivated();
    }

    void CancelListClick()
    {
        if (FlagshipList1Click != null && FlagshipList1Click.activeSelf == true)
            FlagshipList1Click.SetActive(false);
        if (FlagshipList2Click != null && FlagshipList2Click.activeSelf == true)
            FlagshipList2Click.SetActive(false);
        if (FlagshipList3Click != null && FlagshipList3Click.activeSelf == true)
            FlagshipList3Click.SetActive(false);
        if (FlagshipList4Click != null && FlagshipList4Click.activeSelf == true)
            FlagshipList4Click.SetActive(false);
        if (FlagshipList5Click != null && FlagshipList5Click.activeSelf == true)
            FlagshipList5Click.SetActive(false);
    }

    //선택된 기함의 스킬 슬롯을 불러오기
    void SlotActivated()
    {
        if (SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
            SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
        else if (SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
            SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

        if (SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
            SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
        else if (SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
            SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

        if (SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
            SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
        else if (SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
            SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

        //슬롯1 초기화
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false)
        {
            SkillSlot1.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot1.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //슬롯2 초기화
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false)
        {
            SkillSlot2.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot2.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //슬롯3 초기화
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false)
        {
            SkillSlot3.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot3.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //슬롯1 활성화
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == true)
        {
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 1)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 2)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 1)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 2)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 1)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber == 2)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon2.SetActive(true);
        }

        //슬롯2 활성화
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == true)
        {
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 1)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 2)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 1)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 2)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 1)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber == 2)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon2.SetActive(true);
        }

        //슬롯3 활성화
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == true)
        {
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 1 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 2)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 2 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 2)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon2.SetActive(true);

            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon1.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType == 3 && ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber == 2)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipSupportSkillIcon2.SetActive(true);
        }
        SlotUnlockState();
    }

    //슬롯 잠금 해제 상태 불러오기
    public void SlotUnlockState()
    {
        //첫 번째 슬롯
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
        {
            if (WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
            {
                SkillSlot1Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot1Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Papatus III");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot1Text.text = string.Format("<color=#FDFF00>필요한 해방된 행성 : </color>파파투스 III");
            }
            else if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false)
            {
                SkillSlot1Image.raycastTarget = true;
                UnlockPay(SkillSlot1Text, 1000, 1300, 0);
            }
        }
        else
        {
            SkillSlot1Text.text = string.Format("");
        }

        //두 번째 슬롯
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
        {
            if (WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
            {
                SkillSlot2Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot2Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Japet Agrone");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot2Text.text = string.Format("<color=#FDFF00>필요한 해방된 행성 : </color>자펫 아그로네");
            }
            else if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false)
            {
                SkillSlot2Image.raycastTarget = true;
                UnlockPay(SkillSlot2Text, 1000, 1300, 0);
            }
        }
        else
        {
            SkillSlot2Text.text = string.Format("");
        }

        //세 번째 슬롯
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
        {
            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
            {
                SkillSlot3Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot3Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Jerato O95-2252");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot3Text.text = string.Format("<color=#FDFF00>필요한 해방된 행성 : </color>제라토 O95-2252");
            }
            else if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false)
            {
                SkillSlot3Image.raycastTarget = true;
                UnlockPay(SkillSlot3Text, 1000, 1300, 0);
            }
        }
        else
        {
            SkillSlot3Text.text = string.Format("");
        }
    }

    //언락 해제
    public void UnlockSlot1Click()
    {
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false)
        {
            UnlockCost = 1000;
            UnlockResource = 1300;
            UnlockTaritronic = 0;
            SystemMessages.UnlockName = "Slot1 Unlock";
            EnterUnlock();
        }
    }
    public void UnlockSlot2Click()
    {
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false)
        {
            UnlockCost = 1000;
            UnlockResource = 1300;
            UnlockTaritronic = 0;
            SystemMessages.UnlockName = "Slot2 Unlock";
            EnterUnlock();
        }
    }
    public void UnlockSlot3Click()
    {
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false)
        {
            UnlockCost = 1000;
            UnlockResource = 1300;
            UnlockTaritronic = 0;
            SystemMessages.UnlockName = "Slot3 Unlock";
            EnterUnlock();
        }
    }

    //지불을 통한 언락
    void UnlockPay(Text text, int UnlockPay, int UnlockPay2, int UnlockPay3)
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            if (UnlockPay2 == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa, " + UnlockPay2 + " CR");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay + " Glopa, " + UnlockPay2 + " CR, " + UnlockPay3 + " Taritronic");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay2 + " CR");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay2 + " CR" + UnlockPay3 + " Taritronic");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>Unlock cost : </color>" + UnlockPay3 + " Taritronic");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (UnlockPay2 == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay + " 글로파, " + UnlockPay2 + " 건설 재료, " + UnlockPay3 + " 타리트로닉");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay2 + " 건설 재료");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay2 + " 건설 재료" + UnlockPay3 + " 타리트로닉");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>잠금 해제 비용 : </color>" + UnlockPay3 + " 타리트로닉");
        }
    }

    //자금을 지불하여 잠금 해제 절차 메시지를 띄우기
    void EnterUnlock()
    {
        SystemMessages.GlopaorosCostProcess = UnlockCost;
        SystemMessages.ConstructionResourceProcess = UnlockResource;
        SystemMessages.TaritronicProcess = UnlockTaritronic;
        SystemMessages.WeaponUnlockStep = 3;
        WordPrintMenu.UpgradeTableInform(UnlockCost, UnlockResource, UnlockTaritronic);
        StartCoroutine(SystemMessages.MessageOn(2, 0));
    }

    //언락 시작
    public void UnlockStart(string UnlockName)
    {
        if (UnlockName == "Slot1 Unlock")
        {
            WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] = true;
        }
        else if (UnlockName == "Slot2 Unlock")
        {
            WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] = true;
        }
        else if (UnlockName == "Slot3 Unlock")
        {
            WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] = true;
        }

        SlotActivated();
    }

    //스킬 목록
    public void FlagshipAttack1Down()
    {
        FlagshipSkillType = 1;
        SkillNumber = 1;
    }
    public void FlagshipAttack1Click()
    {
        CancelSkillClick();
        FlagshipAttackSkill1Click.SetActive(true);
        WordPrintMenu.PrintType = 1;
        WordPrintMenu.PrintNumber = 1;
        WordPrintMenu.FlagshipMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.SikroClassCruiseMissileDamage, 30, 1);
    }
    public void FleetAttack1Down()
    {
        FlagshipSkillType = 2;
        SkillNumber = 1;
    }
    public void FleetAttack1Click()
    {
        CancelSkillClick();
        FleetAttackSkill1Click.SetActive(true);
        WordPrintMenu.PrintType = 2;
        WordPrintMenu.PrintNumber = 1;
        WordPrintMenu.FlagshipMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.Cysiro47PatriotMissileDamage, 30, 5);
    }

    void CancelSkillClick()
    {
        if (FlagshipAttackSkill1Click != null && FlagshipAttackSkill1Click.activeSelf == true)
            FlagshipAttackSkill1Click.SetActive(false);
        if (FleetAttackSkill1Click != null && FleetAttackSkill1Click.activeSelf == true)
            FleetAttackSkill1Click.SetActive(false);
        if (FlagshipSupportSkill1Click != null && FlagshipSupportSkill1Click.activeSelf == true)
            FlagshipSupportSkill1Click.SetActive(false);
    }

    //스킬 슬롯 드래그가 끝나면 종료
    public void SlotIconTurnOff()
    {
        FlagshipSkillType = 0;
        SkillNumber = 0;
    }

    //스킬 슬롯
    public void SkillSlotNumber1Enter()
    {
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == false)
            {
                SlotNumber = 1;
                SlotInput = true;
                SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
            }
        }
    }
    public void SkillSlotNumber1Exit()
    {
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == true)
            {
                SlotNumber = 0;
                SlotInput = false;
                SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            }
        }
    }
    public void SkillSlotNumber2Enter()
    {
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == false)
            {
                SlotNumber = 2;
                SlotInput = true;
                SkillSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
            }
        }
    }
    public void SkillSlotNumber2Exit()
    {
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == true)
            {
                SlotNumber = 0;
                SlotInput = false;
                SkillSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            }
        }
    }
    public void SkillSlotNumber3Enter()
    {
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == false)
            {
                SlotNumber = 3;
                SlotInput = true;
                SkillSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
            }
        }
    }
    public void SkillSlotNumber3Exit()
    {
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == true)
        {
            if (SkillNumber > 0 && SlotInput == true)
            {
                SlotNumber = 0;
                SlotInput = false;
                SkillSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            }
        }
    }

    //선택된 스킬로 변경
    public void SlotInputStart()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SlotChangeAudio);
        if (SlotNumber == 1 && WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == true)
        {
            SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

            //초기화
            if (SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //선택한 스킬 이미지로 변경
            if (FlagshipSkillType == 1 && SkillNumber == 1)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (FlagshipSkillType == 2 && SkillNumber == 1)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillType = FlagshipSkillType;
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SkillNumber = SkillNumber;
        }
        else if (SlotNumber == 2 && WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == true)
        {
            SkillSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

            //초기화
            if (SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //선택한 스킬 이미지로 변경
            if (FlagshipSkillType == 1 && SkillNumber == 1)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (FlagshipSkillType == 2 && SkillNumber == 1)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillType = FlagshipSkillType;
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().SecondSkillNumber = SkillNumber;
        }
        else if (SlotNumber == 3 && WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == true)
        {
            SkillSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

            //초기화
            if (SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //선택한 스킬 이미지로 변경
            if (FlagshipSkillType == 1 && SkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (FlagshipSkillType == 2 && SkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType = FlagshipSkillType;
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber = SkillNumber;
        }
    }

    //기함 새배치 버튼
    public void FlagshipAddButtonClick()
    {
        if (ShipManager.instance.FlagShipList.Count < 3) //기함 한도에 맞게 새 배치 승인
        {
            SystemMessages.MessageType = 3;
            SystemMessages.MessageNumber = 1;
            UpgradeDataSystem.instance.GlopaorosCost = 3000;
            UpgradeDataSystem.instance.ConstructionResourceCost = 12000;
            UpgradeDataSystem.instance.TaritronicCost = 0;
            WordPrintMenu.FlagshipManagerFlagshipAddMessagePrint();
            StartCoroutine(SystemMessages.MessageOn(1, 1));
        }
        else
        {
            WordPrintMenu.NewFlagshipNumberFail();
            StartCoroutine(SystemMessages.MessageOn(0, 0));
            StartCoroutine(SystemMessages.SecondMessageOn(1));
        }
    }
    public void FlagshipAddButtonDown()
    {
        FlagshipAddClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void FlagshipAddButtonUp()
    {
        if (FlagshipAddClick == true)
        {
            FlagshipAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        FlagshipAddClick = false;
    }
    public void FlagshipAddButtonEnter()
    {
        if (FlagshipAddClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            FlagshipAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void FlagshipAddButtonExit()
    {
        if (FlagshipAddClick == true)
        {
            FlagshipAddButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //기함 관리 메뉴 부팅
    public void StartFlagshipManagerMenu()
    {
        StartCoroutine(CameraZoom.BootingAnimation());
        StartCoroutine(FlagshipManagerMenuWindowOpen());
        WordPrintMenu.FlagshipMenuSkillName.text = string.Format("");
        WordPrintMenu.FlagshipMenuSkillExplain.text = string.Format("");
        CancelSkillClick();
        FlagshipListLoad();
    }

    //함대 배열 메뉴 작동
    public IEnumerator FlagshipManagerMenuWindowOpen()
    {
        FlagshipManagerExitButton.raycastTarget = false;
        yield return new WaitForSecondsRealtime(0.55f);
        FlagshipManagerExitButton.raycastTarget = true; //기함 메뉴 나가기 버튼 활성화
        MainMenuButtonSystem.FlagshipMenuWindow.SetActive(true); //기함 메뉴창 활성화
        WordPrintMenu.FlagshipAmount();
        Flagship1SelectClick();

        if (BattleSave.Save1.FlagshipManagementTutorial == true) //첫 튜토리얼
        {
            StartCoroutine(TutorialSystem.TutorialWindowOpen(102));
        }
    }
}