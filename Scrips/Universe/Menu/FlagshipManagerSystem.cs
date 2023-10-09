using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FlagshipManagerSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public CameraZoom CameraZoom;
    public SystemMessages SystemMessages;
    public WordPrintMenu WordPrintMenu;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public TutorialSystem TutorialSystem;
    public MultiFlagshipSystem MultiFlagshipSystem;

    [Header("���� �޴� ��ư")]
    public GameObject CancelFleetButtonPrefab;
    public bool CancelFleetClick;

    [Header("���� �޴� â")]
    public Image FlagshipManagerExitButton; //������ ��ư
    public int FleetNumber = 0; //�Դ� �޴����� �Լ��� ������ �� �����ϱ� ���� ����
    public int FlagshipSkillType = 0; //1 = ���� �ܵ� ����, 2 = �Դ� ����, 3 = ���� ����
    public int SkillNumber = 0; //�� �� ��ų ���� ��ȣ

    [Header("���� ���")]
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

    [Header("���� ��ų ����")]
    public GameObject SkillSlot1;
    public GameObject SkillSlot2;
    public GameObject SkillSlot3;
    private int SlotNumber; //���� ��ȣ
    public bool SlotInput = false; //��� ���Կ� ������ �� �ִ��� ����

    [Header("���� ��� ���� ����")]
    private int UnlockNumber; //��� ��ȣ
    public bool PlanetUnlock = false; //�༺ �ع����� ���� ����
    public bool Unlock = false; //���ŷ� ���� ����
    private int UnlockCost; //���۵� ��� ���
    private int UnlockResource;
    private int UnlockTaritronic;
    public Text LockText;

    [Header("��ų ������")]
    public GameObject FlagshipAttackSkillItem1; //���� ������ ����(��ġ ������)
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

    [Header("���� ����ġ")]
    public GameObject FlagshipAddButtonPrefab;
    public bool FlagshipAddClick;

    [Header("���� ���")]
    public Text SkillSlot1Text;
    public Image SkillSlot1Image;
    public Text SkillSlot2Text;
    public Image SkillSlot2Image;
    public Text SkillSlot3Text;
    public Image SkillSlot3Image;

    [Header("����")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;
    public AudioClip SlotChangeAudio;

    //��� ��, �ٽ� ���θ޴� â���� ���ư���
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

    //������
    IEnumerator Exit()
    {
        SystemMessages.MessageType = 0;
        SystemMessages.MessageNumber = 0;
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        yield return new WaitForSecondsRealtime(0.1f);

        //��ų ���� �ִϸ��̼� �ʱ�ȭ
        if (SkillSlot1.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (SkillSlot2.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (SkillSlot3.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            SkillSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);

        MainMenuButtonSystem.MenuButtonAnim.SetActive(true); //���� �޴� ��ư Ȱ��ȭ
        MainMenuButtonSystem.FlagshipMenuWindow.SetActive(false);
        MainMenuButtonSystem.FlagshipMenuMode = false;
        StartCoroutine(CameraZoom.ExitingAnimation());
    }

    //���� ��� �������� �� ��ų ����Ʈ �ʱ�ȭ
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

    //���� ���� ��(��1) ����
    public void FlagshipAttackTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(true);
        FormationAttackTabClicked.SetActive(false);
        FlagshipSupportTabClicked.SetActive(false);
        WordPrintMenu.FlagshipMenuSlotExplain(1);

        //��ų ���� Ȱ��ȭ
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

    //�Դ� ���� ��(��2) ����
    public void FormationAttackTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(false);
        FormationAttackTabClicked.SetActive(true);
        FlagshipSupportTabClicked.SetActive(false);
        WordPrintMenu.FlagshipMenuSlotExplain(2);

        //��ų ���� Ȱ��ȭ
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

    //���� ���� ��(��3) ����
    public void FlagshipSupportTabSelect()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipAttackTabClicked.SetActive(false);
        FormationAttackTabClicked.SetActive(false);
        FlagshipSupportTabClicked.SetActive(true);

        //��ų ���� Ȱ��ȭ
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

    //��� ��ų ���� ����
    void SkillSlotsTurnOff()
    {
        FlagshipAttackSkillItem1.SetActive(false);
        FleetAttackSkillItem1.SetActive(false);
    }

    //���� ����
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

    //���õ� ������ ��ų ������ �ҷ�����
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

        //����1 �ʱ�ȭ
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false)
        {
            SkillSlot1.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot1.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //����2 �ʱ�ȭ
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false)
        {
            SkillSlot2.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot2.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //����3 �ʱ�ȭ
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false)
        {
            SkillSlot3.GetComponent<Animator>().SetBool("Locked, Menu slot", true);
        }
        else
        {
            SkillSlot3.GetComponent<Animator>().SetBool("Locked, Menu slot", false);
        }

        //����1 Ȱ��ȭ
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

        //����2 Ȱ��ȭ
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

        //����3 Ȱ��ȭ
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

    //���� ��� ���� ���� �ҷ�����
    public void SlotUnlockState()
    {
        //ù ��° ����
        if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
        {
            if (WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
            {
                SkillSlot1Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot1Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Papatus III");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot1Text.text = string.Format("<color=#FDFF00>�ʿ��� �ع�� �༺ : </color>�������� III");
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

        //�� ��° ����
        if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
        {
            if (WeaponUnlockManager.instance.JapetAgroneLabUnlock == false)
            {
                SkillSlot2Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot2Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Japet Agrone");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot2Text.text = string.Format("<color=#FDFF00>�ʿ��� �ع�� �༺ : </color>���� �Ʊ׷γ�");
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

        //�� ��° ����
        if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[FleetNumber] == false || WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
        {
            if (WeaponUnlockManager.instance.JeratoO95_2252LabUnlock == false)
            {
                SkillSlot3Image.raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    SkillSlot3Text.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Jerato O95-2252");
                else if (BattleSave.Save1.LanguageType == 2)
                    SkillSlot3Text.text = string.Format("<color=#FDFF00>�ʿ��� �ع�� �༺ : </color>������ O95-2252");
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

    //��� ����
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

    //������ ���� ���
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
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���, " + UnlockPay3 + " Ÿ��Ʈ�δ�");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay2 + " �Ǽ� ���");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay2 + " �Ǽ� ���" + UnlockPay3 + " Ÿ��Ʈ�δ�");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay3 + " Ÿ��Ʈ�δ�");
        }
    }

    //�ڱ��� �����Ͽ� ��� ���� ���� �޽����� ����
    void EnterUnlock()
    {
        SystemMessages.GlopaorosCostProcess = UnlockCost;
        SystemMessages.ConstructionResourceProcess = UnlockResource;
        SystemMessages.TaritronicProcess = UnlockTaritronic;
        SystemMessages.WeaponUnlockStep = 3;
        WordPrintMenu.UpgradeTableInform(UnlockCost, UnlockResource, UnlockTaritronic);
        StartCoroutine(SystemMessages.MessageOn(2, 0));
    }

    //��� ����
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

    //��ų ���
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

    //��ų ���� �巡�װ� ������ ����
    public void SlotIconTurnOff()
    {
        FlagshipSkillType = 0;
        SkillNumber = 0;
    }

    //��ų ����
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

    //���õ� ��ų�� ����
    public void SlotInputStart()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SlotChangeAudio);
        if (SlotNumber == 1 && WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[FleetNumber] == true)
        {
            SkillSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

            //�ʱ�ȭ
            if (SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot1.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //������ ��ų �̹����� ����
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

            //�ʱ�ȭ
            if (SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot2.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //������ ��ų �̹����� ����
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

            //�ʱ�ȭ
            if (SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.activeSelf == true)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(false);
            else if (SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.activeSelf == true)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(false);

            //������ ��ų �̹����� ����
            if (FlagshipSkillType == 1 && SkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FlagshipAttackSkillIcon1.SetActive(true);
            else if (FlagshipSkillType == 2 && SkillNumber == 1)
                SkillSlot3.GetComponent<SkillSlotIconActive>().FleetAttackSkillIcon1.SetActive(true);
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillType = FlagshipSkillType;
            ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FlagshipAttackSkill>().ThirdSkillNumber = SkillNumber;
        }
    }

    //���� ����ġ ��ư
    public void FlagshipAddButtonClick()
    {
        if (ShipManager.instance.FlagShipList.Count < 3) //���� �ѵ��� �°� �� ��ġ ����
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

    //���� ���� �޴� ����
    public void StartFlagshipManagerMenu()
    {
        StartCoroutine(CameraZoom.BootingAnimation());
        StartCoroutine(FlagshipManagerMenuWindowOpen());
        WordPrintMenu.FlagshipMenuSkillName.text = string.Format("");
        WordPrintMenu.FlagshipMenuSkillExplain.text = string.Format("");
        CancelSkillClick();
        FlagshipListLoad();
    }

    //�Դ� �迭 �޴� �۵�
    public IEnumerator FlagshipManagerMenuWindowOpen()
    {
        FlagshipManagerExitButton.raycastTarget = false;
        yield return new WaitForSecondsRealtime(0.55f);
        FlagshipManagerExitButton.raycastTarget = true; //���� �޴� ������ ��ư Ȱ��ȭ
        MainMenuButtonSystem.FlagshipMenuWindow.SetActive(true); //���� �޴�â Ȱ��ȭ
        WordPrintMenu.FlagshipAmount();
        Flagship1SelectClick();

        if (BattleSave.Save1.FlagshipManagementTutorial == true) //ù Ʃ�丮��
        {
            StartCoroutine(TutorialSystem.TutorialWindowOpen(102));
        }
    }
}