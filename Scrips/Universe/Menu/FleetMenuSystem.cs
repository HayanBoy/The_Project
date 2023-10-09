using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class FleetMenuSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public CameraZoom CameraZoom;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public WordPrintMenu WordPrintMenu;
    public FleetFormationMenuSystem FleetFormationMenuSystem;
    public SystemMessages SystemMessages;
    public TutorialSystem TutorialSystem;

    [Header("�Դ� �޴� ��ư")]
    public GameObject NextFleetButtonPrefab; //���� �Դ� ���� ��ư������
    public bool NextFleetClick; //���� �Դ� ���� ��ư Ŭ�� ����ġ
    public GameObject PreviousFleetButtonPrefab; //���� �Դ� ���� ��ư������
    public bool PreviousFleetClick; //���� �Դ� ���� ��ư Ŭ�� ����ġ
    public GameObject CancelFleetButtonPrefab; //�Դ� �޴� ������ ��ư ������
    public bool CancelFleetClick; //�Դ� �޴� ������ ��ư Ŭ�� ����ġ
    public Image CancelFleetImage; //�Դ� �޴� ������ ��ư�� ����ĳ��Ʈ
    public Text CancelFleetText; //������ ��ư �ؽ�Ʈ
    public GameObject FleetSizeButtonPrefab; //�Դ� ���� ������â Ȯ�� �� ��� ��ư ������
    public GameObject FleetSizeBiggerIconPrefab; //�Դ� ���� ������â Ȯ�� ������ �̹��� ������
    public GameObject FleetSizeSmallerIconPrefab; //�Դ� ���� ������â ��� ������ �̹��� ������
    public bool FleetSizeClick;  //�Դ� ���� ������â Ȯ�� �� ��� ��ư Ŭ�� ����ġ
    public bool FleetSizeBig = false; //�Դ� ��ġ ȭ���� Ȯ��Ǿ������� ���� ����ġ

    [Header("�Լ� ������")]
    public GameObject Flagship1Prefab;
    public GameObject FormationShip1Prefab;
    public GameObject ShieldShip1Prefab;
    public GameObject Carrier1Prefab;

    [Header("�Լ� â")]
    public LayerMask FleetShipLayer; //������ �Դ��� ���̾�
    public GameObject FleetBackground; //�Դ� ���
    public Transform FlagshipCenterPos; //�Դ� â�� ���� �߽� ��ǥ. �̰����� ������ ������Ű��, ���� �߽����� �Դ� �迭�� ���������.
    public int FlagshipNumber = 0; //���Թ�ȣ
    public int FleetNumber = 0; //�Դ� �޴����� �Լ��� ������ �� �����ϱ� ���� ����
    private int CreateArea = 0; //�����Ǵ� ���Ե��� ���� ����߸��� ����

    [Header("�Լ� ��� â")]
    public int Flagship1Window = 0; //���õ� �Լ� Ƚ��
    public int FormationShip1Window = 0;
    public int ShieldShip1Window = 0;
    public int Carrier1Window = 0;
    public bool SelectOneType = false; //�� �з��� �Լ��� �����ϵ��� ���� ����ġ

    [Header("�Լ��� ���� ������")]
    public LayerMask TurretSlotLayer; //��� ���� ���̾�
    public LayerMask EquipmentSlotLayer; //��� ���� ���� ���̾�
    public GameObject FlagshipInput;
    public GameObject FormationInput;
    public GameObject ShieldInput;
    public GameObject CarrierInput;
    public int TurretNumber; //������ ���� ��ȣ
    private int SlotNumber; //���� ��ȣ
    public bool SlotInput = false; //��� ���Կ� ������ �� �ִ��� ����

    [Header("�Լ��� ����")]
    public GameObject FlagshipSlot1;
    public GameObject FlagshipSlot2;
    public GameObject FlagshipSlot3;
    public GameObject FlagshipSlot4;
    public GameObject FlagshipSlot5;
    public GameObject FlagshipSlot6;
    public GameObject FormationShipSlot1;
    public GameObject FormationShipSlot2;

    [Header("���� ������")]
    public GameObject TurretItem1; //���� ������ ����(��ġ ������)
    public GameObject TurretItem2;
    public GameObject TurretItem3;
    public GameObject TurretItem4;

    public GameObject Turret1Click;
    public GameObject Turret2Click;
    public GameObject Turret3Click;
    public GameObject Turret4Click;

    [Header("�ִϸ��̼� �� ������")]
    public GameObject FleetGearMenuPrefab; //�Դ� �޴� ��ü ���� ������
    public GameObject SelectFleetFramePrefab; //�Դ� ���� ȭ���� Ȯ��� ��Ÿ���� ���� UI
    public GameObject FleetSizeActiveEffectStartPrefab; //�Դ� ���� ȭ�� Ȯ�� �ִϸ��̼� ���۰� ���ÿ� �߻��Ǵ� ��ƼŬ ����Ʈ ���� ������
    public GameObject FleetSizeActiveEffectEndPrefab; //�Դ� ���� ȭ�� Ȯ�� �ִϸ��̼� ����� ���ÿ� �߻��Ǵ� ��ƼŬ ����Ʈ ���� ������

    [Header("���� ���")]
    private int UnlockNumber; //��� ��ȣ
    public GameObject LockPrefab;
    public Text LockText;
    public GameObject OverJumpLockPrefab;
    public Image OverJumpButton;
    public GameObject DeltaNeedle42HalistLockPrefab;
    public Image DeltaNeedle42HalistButton;

    [Header("���� ��� ���� ����")]
    public bool PlanetUnlock = false; //�༺ �ع����� ���� ����
    public bool Unlock = false; //���ŷ� ���� ����
    private int UnlockCost; //���۵� ��� ���
    private int UnlockResource;
    private int UnlockTaritronic;

    [Header("����")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;
    public AudioClip FleetSizeOnAudio;
    public AudioClip FleetSizeOffAudio;
    public AudioClip UnitSelectAudio;
    public AudioClip SlotChangeAudio;

    //���� �Դ� ����
    public void NextFleetButtonClick()
    {
        NextFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", true);
        SelectedShipInitialize();
        if (FleetNumber < ShipManager.instance.FleetShipList.Count - 1)
            FleetNumber++;
        else
            FleetNumber = 0;
        CameraZoom.CVCamera.m_Lens.OrthographicSize = 15;
        CameraZoom.CVCamera.Follow = ShipManager.instance.FleetShipList[FleetNumber].transform;
        ShipManager.instance.SelectedFleetFlagship[0] = ShipManager.instance.FleetShipList[FleetNumber];

        if (Flagship1Window > 0)
            FlagshipTurretIconGenerate();
        if (FormationShip1Window > 0)
            FormationShipTurretIconGenerate();
        WordPrintMenu.FleetMenuSelectedFleetNamePrint(FleetNumber, FleetSizeBig);
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

    //���� �Դ� ����
    public void PreviousFleetButtonClick()
    {
        PreviousFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(click), fleet next select button", true);
        SelectedShipInitialize();
        if (FleetNumber <= ShipManager.instance.FleetShipList.Count - 1 && FleetNumber > 0)
            FleetNumber--;
        else if (FleetNumber <= 0)
            FleetNumber = ShipManager.instance.FleetShipList.Count - 1;
        CameraZoom.CVCamera.m_Lens.OrthographicSize = 15;
        CameraZoom.CVCamera.Follow = ShipManager.instance.FleetShipList[FleetNumber].transform;
        ShipManager.instance.SelectedFleetFlagship[0] = ShipManager.instance.FleetShipList[FleetNumber];

        if (Flagship1Window > 0)
            FlagshipTurretIconGenerate();
        if (FormationShip1Window > 0)
            FormationShipTurretIconGenerate();
        WordPrintMenu.FleetMenuSelectedFleetNamePrint(FleetNumber, FleetSizeBig);
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

    //��� ��, �ٽ� ���θ޴� â���� ���ư���
    public void CancelFleetButtonClick()
    {
        SelectedShipInitialize();
        StartCoroutine(Exit());
    }

    //������
    IEnumerator Exit()
    {
        if (BattleSave.Save1.FleetWeaponTutorial == true)
        {
            TutorialSystem.FlagshipGearViewer.SetActive(false);
            TutorialSystem.ViewerPrefab.SetActive(true);
        }

        SystemMessages.MessageType = 0;
        SystemMessages.MessageNumber = 0;
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        yield return new WaitForSecondsRealtime(0.1f);
        if (FleetSizeBig == true)
        {
            CancelFleetImage.raycastTarget = true;
            FleetSizeBig = false;
            FleetSizeBiggerIconPrefab.SetActive(true);
            FleetSizeSmallerIconPrefab.SetActive(false);
            FleetSizeActiveEffectStartPrefab.SetActive(false);
            FleetSizeActiveEffectEndPrefab.SetActive(false);
        }
        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 0);
        CameraZoom.MainCameraPrefab.GetComponent<Animator>().SetFloat("Rect Change1, Main camera", 0);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active, Fleet gear menu", 0);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetFloat("Fleet size active, fleet gear menu size button", 0);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active(Text), Fleet gear menu", 0);
        if (SelectFleetFramePrefab.GetComponent<Animator>().GetBool("Ship select, Select fleet mode frame") == true)
            SelectFleetFramePrefab.GetComponent<Animator>().SetBool("Ship select, Select fleet mode frame", false);

        //���� ���� �ִϸ��̼� �ʱ�ȭ
        if (FlagshipSlot1.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FlagshipSlot2.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FlagshipSlot3.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FlagshipSlot4.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot4.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FlagshipSlot5.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot5.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FlagshipSlot6.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FlagshipSlot6.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FormationShipSlot1.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FormationShipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        if (FormationShipSlot2.GetComponent<Animator>().GetFloat("Slot active, Menu slot") > 0)
            FormationShipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);

        MainMenuButtonSystem.MenuButtonAnim.SetActive(true); //���� �޴� ��ư Ȱ��ȭ(Ÿ�ӽ������� 0�� ���¿��� �ش� ������Ʈ�� ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ� ���� ���, �ڷ�ƾ�� WaitForSecondsRealtime�� �۵��� ���� �� �����Ƿ� ���� ����)
        MainMenuButtonSystem.FleetMenuWindow.SetActive(false);
        MainMenuButtonSystem.FleetMenuMode = false;
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
        FleetBackground.SetActive(false);
        CancelTurretClick();
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

    //�Դ� ��ġ�� Ȯ��
    public void FleetSizeButtonClick()
    {
        if (FleetSizeBig == false)
            StartCoroutine(FleetSelectBiggerStart());
        else
            StartCoroutine(FleetSelectBiggerEnd());
    }
    public void FleetSizeButtonDown()
    {
        FleetSizeClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet gear menu size button", true);
    }
    public void FleetSizeButtonUp()
    {
        if (FleetSizeClick == true)
        {
            FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet gear menu size button", false);
        }
        FleetSizeClick = false;
    }
    public void FleetSizeButtonEnter()
    {
        if (FleetSizeClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet gear menu size button", true);
        }
    }
    public void FleetSizeButtonExit()
    {
        if (FleetSizeClick == true)
        {
            FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("touch(down), fleet gear menu size button", false);
        }
    }

    //�Դ� ����â ũ�� Ȯ�� �ִϸ��̼�
    IEnumerator FleetSelectBiggerStart()
    {
        if (BattleSave.Save1.FleetWeaponTutorial == true)
        {
            TutorialSystem.FlagshipGearViewer.SetActive(false);
        }
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", FleetSizeOnAudio);
        FleetSizeBig = true;
        CancelFleetImage.raycastTarget = false;
        FleetSizeActiveEffectStartPrefab.SetActive(true);
        CameraZoom.MainCameraPrefab.GetComponent<Animator>().SetFloat("Rect Change1, Main camera", 1);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active, Fleet gear menu", 1);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active(Text), Fleet gear menu", 1);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("Disappear, fleet next select button", true);
        yield return new WaitForSecondsRealtime(0.05f);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 100);
        yield return new WaitForSecondsRealtime(0.25f);
        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 1);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetFloat("Fleet size active, fleet gear menu size button", 1);
        WordPrintMenu.FleetMenuSelectedFleetNamePrint(FleetNumber, FleetSizeBig);
        yield return new WaitForSecondsRealtime(0.25f);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("Disappear, fleet next select button", false);
        yield return new WaitForSecondsRealtime(0.05f);
        FleetSizeActiveEffectStartPrefab.SetActive(false);
        FleetSizeBiggerIconPrefab.SetActive(false);
        FleetSizeSmallerIconPrefab.SetActive(true);
    }
    IEnumerator FleetSelectBiggerEnd()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", FleetSizeOffAudio);
        FleetSizeBig = false;
        FleetSizeActiveEffectEndPrefab.SetActive(true);
        SelectFleetFramePrefab.GetComponent<Animator>().SetFloat("Active, Select fleet mode frame", 2);
        CameraZoom.MainCameraPrefab.GetComponent<Animator>().SetFloat("Rect Change1, Main camera", 2);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active, Fleet gear menu", 2);
        FleetGearMenuPrefab.GetComponent<Animator>().SetFloat("Fleet size active(Text), Fleet gear menu", 2);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("Disappear, fleet next select button", true);
        yield return new WaitForSecondsRealtime(0.25f);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetFloat("Fleet size active, fleet gear menu size button", 2);
        WordPrintMenu.FleetMenuSelectedFleetNamePrint(FleetNumber, FleetSizeBig);
        yield return new WaitForSecondsRealtime(0.25f);
        MainMenuButtonSystem.CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 3);
        FleetSizeButtonPrefab.GetComponent<Animator>().SetBool("Disappear, fleet next select button", false);
        yield return new WaitForSecondsRealtime(0.05f);
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        FleetSizeActiveEffectEndPrefab.SetActive(false);
        FleetSizeBiggerIconPrefab.SetActive(true);
        FleetSizeSmallerIconPrefab.SetActive(false);
        yield return new WaitForSecondsRealtime(0.16f);
        CancelFleetImage.raycastTarget = true;

        if (BattleSave.Save1.FleetWeaponTutorial == true)
        {
            TutorialSystem.FlagshipGearViewer.SetActive(true);
        }
    }


    //���� ��� ����
    public void TurretNumber1Down() //���Ϸ��� �ý�Ʈ
    {
        TurretNumber = 1;
    }
    public void Turret1ExplainClick()
    {
        CancelTurretClick();
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");
        Turret1Click.SetActive(true);
        WordPrintMenu.PrintNumber = 1;
        WordPrintMenu.FleetMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.FlagshipSilenceSistDamage, 3, 4, UpgradeDataSystem.instance.FormationSilenceSistDamage, 3, 2);
    }
    public void TurretNumber2Down() //�ʰ�����
    {
        TurretNumber = 2;
    }
    public void Turret2ExplainClick()
    {
        CancelTurretClick();
        if (WeaponUnlockManager.instance.OverJumpUnlock == false || WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
        {
            LockPrefab.SetActive(true);
            UnlockNumber = 1;
            if (WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
            {
                LockPrefab.GetComponent<Image>().raycastTarget = false;
                if (BattleSave.Save1.LanguageType == 1)
                    LockText.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Papatus III");
                else if (BattleSave.Save1.LanguageType == 2)
                    LockText.text = string.Format("<color=#FDFF00>�ʿ��� �ع�� �༺ : </color>�������� III");
            }
            else if (WeaponUnlockManager.instance.OverJumpUnlock == false)
            {
                LockPrefab.GetComponent<Image>().raycastTarget = true;
                UnlockPay(LockText, 2000, 1150, 0);
            }
        }
        else
        {
            LockPrefab.SetActive(false);
            LockText.text = string.Format("");
        }
        Turret2Click.SetActive(true);
        WordPrintMenu.PrintNumber = 2;
        WordPrintMenu.FleetMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.FlagshipOverJumpDamage, 2.5f, 1, UpgradeDataSystem.instance.FormationOverJumpDamage, 5, 1);
    }
    public void TurretNumber3Down() //���� �̻���
    {
        TurretNumber = 3;
    }
    public void Turret3ExplainClick()
    {
        CancelTurretClick();
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");
        Turret3Click.SetActive(true);
        WordPrintMenu.PrintNumber = 3;
        WordPrintMenu.FleetMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.FlagshipSadLilly345Damage, 1.5f, 1, UpgradeDataSystem.instance.FormationSadLilly345Damage, 3, 1);
    }
    public void TurretNumber4Down() //��Ƽ �̻���
    {
        TurretNumber = 4;
    }
    public void Turret4ExplainClick()
    {
        CancelTurretClick();
        if (WeaponUnlockManager.instance.DeltaNeedle42HalistUnlock == false || WeaponUnlockManager.instance.DeriousHeriLabUnlock == false)
        {
            LockPrefab.SetActive(true);
            UnlockNumber = 2;
            if (WeaponUnlockManager.instance.DeriousHeriLabUnlock == false)
            {
                LockPrefab.GetComponent<Image>().raycastTarget = false;
                DeltaNeedle42HalistButton.raycastTarget = false;
                DeltaNeedle42HalistLockPrefab.SetActive(true);
                if (BattleSave.Save1.LanguageType == 1)
                    LockText.text = string.Format("<color=#FDFF00>A necessary liberated planet : </color>Derious Heri");
                else if (BattleSave.Save1.LanguageType == 2)
                    LockText.text = string.Format("<color=#FDFF00>�ʿ��� �ع�� �༺ : </color>�����콺 �츮");
            }
            else if (WeaponUnlockManager.instance.DeltaNeedle42HalistUnlock == false)
            {
                UnlockNumber = 2;
                DeltaNeedle42HalistButton.raycastTarget = false;
                DeltaNeedle42HalistLockPrefab.SetActive(true);
                LockPrefab.GetComponent<Image>().raycastTarget = true;
                UnlockPay(LockText, 2000, 1150, 0);
            }
        }
        else
        {
            LockPrefab.SetActive(false);
            LockText.text = string.Format("");
        }
        Turret4Click.SetActive(true);
        WordPrintMenu.PrintNumber = 4;
        WordPrintMenu.FleetMenuTurretExplainPrintNoClick(UpgradeDataSystem.instance.FlagshipDeltaNeedle42HalistDamage, 3, 3, UpgradeDataSystem.instance.FormationDeltaNeedle42HalistDamage, 3, 3);
    }

    //��ų ���� �巡�װ� ������ ����
    public void SlotIconTurnOff()
    {
        TurretNumber = 0;
    }

    //�ͷ� ����â ����
    void CancelTurretClick()
    {
        if (Turret1Click.activeSelf == true)
            Turret1Click.SetActive(false);
        if (Turret2Click.activeSelf == true)
            Turret2Click.SetActive(false);
        if (Turret3Click.activeSelf == true)
            Turret3Click.SetActive(false);
        if (Turret4Click.activeSelf == true)
            Turret4Click.SetActive(false);
    }

    //���õ� �Լ��� ���� Ÿ�� ����
    public void SlotInputStart()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", SlotChangeAudio);
        if (Flagship1Window > 0) //����
        {
            if (SlotNumber == 1)
            {
                FlagshipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

                //�ʱ�ȭ
                FlagshipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(false);

                //������ ��� �̹����� ����
                if (TurretNumber == 1)
                    FlagshipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                //������ ������ ������ ���缭 ������ �Լ��� ���� �̹��� ����
                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }

                //������ �Լ��� ���� Ÿ���� ����
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
            else if (SlotNumber == 2)
            {
                FlagshipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);
                FlagshipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                if (TurretNumber == 1)
                    FlagshipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
            else if (SlotNumber == 3)
            {
                FlagshipSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);
                FlagshipSlot3.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot3.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot3.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot3.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                if (TurretNumber == 1)
                    FlagshipSlot3.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot3.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot3.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot3.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
            else if (SlotNumber == 4)
            {
                FlagshipSlot4.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);
                FlagshipSlot4.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot4.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot4.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot4.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                if (TurretNumber == 1)
                    FlagshipSlot4.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot4.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot4.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot4.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
            else if (SlotNumber == 5)
            {
                FlagshipSlot5.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);
                FlagshipSlot5.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot5.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot5.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot5.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                if (TurretNumber == 1)
                    FlagshipSlot5.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot5.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot5.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot5.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
            else if (SlotNumber == 6)
            {
                FlagshipSlot6.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);
                FlagshipSlot6.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FlagshipSlot6.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FlagshipSlot6.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FlagshipSlot6.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                if (TurretNumber == 1)
                    FlagshipSlot6.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (TurretNumber == 2)
                    FlagshipSlot6.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (TurretNumber == 3)
                    FlagshipSlot6.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (TurretNumber == 4)
                    FlagshipSlot6.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                if (TurretNumber < 3)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                }
                else if (TurretNumber > 2)
                {
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                    ShipManager.instance.FleetShipList[FleetNumber].GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                }
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
            }
        }
        else if (FormationShip1Window > 0) //�����
        {
            if (SlotNumber == 1)
            {
                FormationShipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

                for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
                {
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                    FormationShipSlot1.GetComponent<SlotIconActive>().DifferentIcon.SetActive(false);

                    if (TurretNumber == 1)
                        FormationShipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                    else if (TurretNumber == 2)
                        FormationShipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                    else if (TurretNumber == 3)
                        FormationShipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                    else if (TurretNumber == 4)
                        FormationShipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                    if (TurretNumber < 3)
                    {
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                    }
                    else if (TurretNumber > 2)
                    {
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                    }
                    ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                    ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
            }
            else if (SlotNumber == 2)
            {
                FormationShipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 2);

                for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
                {
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                    FormationShipSlot2.GetComponent<SlotIconActive>().DifferentIcon.SetActive(false);

                    if (TurretNumber == 1)
                        FormationShipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                    else if (TurretNumber == 2)
                        FormationShipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                    else if (TurretNumber == 3)
                        FormationShipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                    else if (TurretNumber == 4)
                        FormationShipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(true);

                    if (TurretNumber < 3)
                    {
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(false);
                    }
                    else if (TurretNumber > 2)
                    {
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(false);
                        ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
                    }
                    ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType = TurretNumber;
                    ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonReinput();
                }
            }
        }
        TurretNumber = 0;

        if (BattleSave.Save1.FleetWeaponTutorial == true) //ù Ʃ�丮�� ����
        {
            TutorialSystem.FlagshipGearGuide1.SetActive(false);
            TutorialSystem.FlagshipGearGuide2.SetActive(false);
            TutorialSystem.FlagshipGearFrame1.SetActive(false);
            TutorialSystem.FlagshipGearFrame2.SetActive(false);
            BattleSave.Save1.FleetWeaponTutorial = false;
        }
    }

    //���� ���� ����
    public void FlagshipSlotNumber1Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 1;
            SlotInput = true;
            if (Flagship1Window > 0)
                FlagshipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
            else if (FormationShip1Window > 0)
                FormationShipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber1Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            if (Flagship1Window > 0)
                FlagshipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            else if (FormationShip1Window > 0)
                FormationShipSlot1.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }
    public void FlagshipSlotNumber2Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 2;
            SlotInput = true;
            if (Flagship1Window > 0)
                FlagshipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
            else if (FormationShip1Window > 0)
                FormationShipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber2Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            if (Flagship1Window > 0)
                FlagshipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
            else if (FormationShip1Window > 0)
                FormationShipSlot2.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }
    public void FlagshipSlotNumber3Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 3;
            SlotInput = true;
            FlagshipSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber3Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            FlagshipSlot3.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }
    public void FlagshipSlotNumber4Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 4;
            SlotInput = true;
            FlagshipSlot4.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber4Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            FlagshipSlot4.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }
    public void FlagshipSlotNumber5Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 5;
            SlotInput = true;
            FlagshipSlot5.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber5Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            FlagshipSlot5.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }
    public void FlagshipSlotNumber6Enter()
    {
        if (TurretNumber > 0 && SlotInput == false)
        {
            SlotNumber = 6;
            SlotInput = true;
            FlagshipSlot6.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 1);
        }
    }
    public void FlagshipSlotNumber6Exit()
    {
        if (TurretNumber > 0 && SlotInput == true)
        {
            SlotNumber = 0;
            SlotInput = false;
            FlagshipSlot6.GetComponent<Animator>().SetFloat("Slot active, Menu slot", 0);
        }
    }

    //�Լ� ��� �޴� ����
    public void StartFleetMenu()
    {
        WordPrintMenu.FleetMenuTurretSlotExplain(1, CancelFleetText);
        WordPrintMenu.FleetMenuTurretName.text = string.Format("");
        WordPrintMenu.FleetMenuTurretExplain.text = string.Format("");
        WordPrintMenu.FleetMenuTurretSlotExplain(); //���� ����â ���
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");
        ShipWeaponUnlockState();
        StartCoroutine(CameraZoom.TurnCameraFleetMenu());
        StartCoroutine(CameraZoom.BootingAnimation());
        StartCoroutine(FleetMenuWindowOpen());
        FleetBackground.SetActive(true);
        FleetGeneratorStart();
    }

    //�Լ� ��� �޴� �۵�
    public IEnumerator FleetMenuWindowOpen()
    {
        CancelFleetImage.raycastTarget = false;
        CameraZoom.MainCameraPrefab.GetComponent<Animator>().SetFloat("Rect Change1, Main camera", 2);
        yield return new WaitForSecondsRealtime(0.55f);
        MainMenuButtonSystem.FleetMenuWindow.SetActive(true); //�Դ� �޴�â Ȱ��ȭ
        CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        WordPrintMenu.FleetMenuSelectedFleetNamePrint(FleetNumber, FleetSizeBig); //���õ� �Դ� �̸� Ȱ��ȭ

        if (BattleSave.Save1.FleetWeaponTutorial == true) //ù Ʃ�丮��
        {
            TutorialSystem.ViewerPrefab.SetActive(false);
            TutorialSystem.FlagshipGearViewer.SetActive(true);
            TutorialSystem.FlagshipGearGuide1.SetActive(false);
            TutorialSystem.FlagshipGearGuide2.SetActive(false);
            TutorialSystem.FlagshipGearFrame1.SetActive(false);
            TutorialSystem.FlagshipGearFrame2.SetActive(false);
            StartCoroutine(TutorialSystem.TutorialWindowOpen(100));
        }
        yield return new WaitForSecondsRealtime(1f);
        if (FleetSizeBig == false)
        {
            CancelFleetButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
            CancelFleetImage.raycastTarget = true; //�Դ� �޴� ������ ��ư Ȱ��ȭ
        }
    }

    //�ڱ��� �����Ͽ� ��� ���� ���� �޽����� ����
    public void EnterUnlockClick()
    {
        SystemMessages.GlopaorosCostProcess = UnlockCost;
        SystemMessages.ConstructionResourceProcess = UnlockResource;
        SystemMessages.TaritronicProcess = UnlockTaritronic;
        SystemMessages.WeaponUnlockStep = 1;
        WordPrintMenu.UpgradeTableInform(UnlockCost, UnlockResource, UnlockTaritronic);
        StartCoroutine(SystemMessages.MessageOn(2, 0));
    }

    //�ش� ���� ������ �����Ͽ� ������ ���������� �̷�������� �ϱ� ����
    public void UnlockDown()
    {
        if (UnlockNumber == 1)
        {
            UnlockCost = 2000;
            UnlockResource = 1150;
            UnlockTaritronic = 0;
            SystemMessages.UnlockName = "Over Jump Unlock";
        }
        else if (UnlockNumber == 2)
        {
            UnlockCost = 2000;
            UnlockResource = 1150;
            UnlockTaritronic = 0;
            SystemMessages.UnlockName = "Delta Needle-42 Halist Unlock";
        }
    }

    //�Լ� ���� ��� ���� ���� �ҷ�����
    public void ShipWeaponUnlockState()
    {
        //�ʰ� ���� ���
        if (WeaponUnlockManager.instance.OverJumpUnlock == false || WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
        {
            if (WeaponUnlockManager.instance.PapatusIIILabUnlock == false)
            {
                OverJumpButton.raycastTarget = false;
                OverJumpLockPrefab.SetActive(true);
            }
            else if (WeaponUnlockManager.instance.OverJumpUnlock == false)
            {
                OverJumpButton.raycastTarget = false;
                OverJumpLockPrefab.SetActive(true);
            }
        }
        else
        {
            OverJumpButton.raycastTarget = true;
            OverJumpLockPrefab.SetActive(false);
        }

        //��Ÿ �ϵ�-42 �Ҹ���Ʈ ���
        if (WeaponUnlockManager.instance.DeltaNeedle42HalistUnlock == false || WeaponUnlockManager.instance.DeriousHeriLabUnlock == false)
        {
            if (WeaponUnlockManager.instance.DeriousHeriLabUnlock == false)
            {
                DeltaNeedle42HalistButton.raycastTarget = false;
                DeltaNeedle42HalistLockPrefab.SetActive(true);
            }
            else if (WeaponUnlockManager.instance.DeltaNeedle42HalistUnlock == false)
            {
                DeltaNeedle42HalistButton.raycastTarget = false;
                DeltaNeedle42HalistLockPrefab.SetActive(true);
            }
        }
        else
        {
            DeltaNeedle42HalistButton.raycastTarget = true;
            DeltaNeedle42HalistLockPrefab.SetActive(false);
        }
    }

    //������ ���� ���
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
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay2 != 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���, " + UnlockPay2 + " �Ǽ� ���, " + UnlockPay3 + " Ÿ��Ʈ�δ�" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay != 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay + " �۷���, " + UnlockPay3 + " Ÿ��Ʈ�δ�" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay == 0 && UnlockPay3 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay2 + " �Ǽ� ���" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay == 0 && UnlockPay3 != 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay2 + " �Ǽ� ���" + UnlockPay3 + " Ÿ��Ʈ�δ�" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
            else if (UnlockPay == 0 && UnlockPay2 == 0)
                text.text = string.Format("<color=#00FF8C>��� ���� ��� : </color>" + UnlockPay3 + " Ÿ��Ʈ�δ�" + "\n<color=#FCFF00>���⸦ Ŭ���Ͽ� ����</color>");
        }
    }

    //��� ����
    public void UnlockStart(string UnlockName)
    {
        LockPrefab.SetActive(false);
        LockText.text = string.Format("");

        if (UnlockName == "Over Jump Unlock")
            WeaponUnlockManager.instance.OverJumpUnlock = true;
        else if (UnlockName == "Delta Needle-42 Halist Unlock")
            WeaponUnlockManager.instance.DeltaNeedle42HalistUnlock = true;
    }

    //�޴��� �Դ� ���� �� �ҷ�����
    public void FleetGeneratorStart()
    {
        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            GameObject Flagship = Instantiate(Flagship1Prefab, new Vector2(FlagshipCenterPos.position.x + CreateArea, FlagshipCenterPos.position.y), FlagshipCenterPos.rotation);
            ShipManager.instance.FleetShipList.Add(Flagship);
            if (MainMenuButtonSystem.FleetFormationMenuMode == true)
            {
                Flagship.layer = 0;
            }

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret3.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret4.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret5.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Flagship.GetComponent<FleetMenuShipNumber>().Turret6.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (i == 0)
                ShipManager.instance.SelectedFleetFlagship.Add(Flagship);
            CreateArea += 1000;
            for (int j = 0; j < ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList.Count; j++)
            {
                if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 1)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location1").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location1").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location1").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 2)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location2").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location2").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location2").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 3)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location3").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location3").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location3").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 4)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location4").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location4").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location4").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 5)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location5").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location5").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location5").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 6)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location6").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location6").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location6").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 7)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location7").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location7").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location7").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 8)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location8").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location8").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location8").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 9)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location9").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location9").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location9").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 10)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location10").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location10").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location10").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 11)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location11").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location11").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location11").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 12)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location12").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location12").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location12").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 13)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location13").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location13").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location13").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 14)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location14").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location14").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location14").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 15)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location15").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location15").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location15").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 16)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location16").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location16").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location16").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 17)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location17").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location17").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location17").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 18)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location18").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location18").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location18").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 19)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location19").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location19").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location19").transform.position, i, j);
                }
                else if (ShipManager.instance.FlagShipList[i].GetComponent<FollowShipManager>().ShipList[j].GetComponent<MoveVelocity>().FormationIndex.GetComponent<ShipLocationNumber>().LocationNumber == 20)
                {
                    Flagship.transform.Find("Fleet formation control/Ship location20").transform.localPosition = ShipManager.instance.FlagShipList[i].transform.Find("Fleet formation control/Ship location20").transform.localPosition;
                    ShipMake(Flagship.transform.Find("Fleet formation control/Ship location20").transform.position, i, j);
                }
            }
        }
        FleetNumber = 0;
    }

    void ShipMake(Vector3 Flagship, int FlagshipNumber, int ListNumber)
    {
        if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<ShipRTS>().ShipNumber == 2)
        {
            GameObject Formation1 = Instantiate(FormationShip1Prefab, Flagship, FlagshipCenterPos.rotation);
            Formation1.GetComponent<FleetMenuShipNumber>().FollowShipNumber = ListNumber;
            ShipManager.instance.FleetShipList[FlagshipNumber].GetComponent<FleetMenuShipNumber>().FollowShipList.Add(Formation1);

            if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Formation1.GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Formation1.GetComponent<FleetMenuShipNumber>().Turret1.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);

            if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType <= 2)
                Formation1.GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().SilenceSist.SetActive(true);
            else if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType <= 4
                && ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType > 2)
                Formation1.GetComponent<FleetMenuShipNumber>().Turret2.GetComponent<FleetMenuTurrets>().Catroy.SetActive(true);
        }
        else if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<ShipRTS>().ShipNumber == 3)
        {
            GameObject Shield1 = Instantiate(ShieldShip1Prefab, Flagship, FlagshipCenterPos.rotation);
            Shield1.GetComponent<FleetMenuShipNumber>().FollowShipNumber = ListNumber;
            ShipManager.instance.FleetShipList[FlagshipNumber].GetComponent<FleetMenuShipNumber>().FollowShipList.Add(Shield1);
        }
        else if (ShipManager.instance.FlagShipList[FlagshipNumber].GetComponent<FollowShipManager>().ShipList[ListNumber].GetComponent<ShipRTS>().ShipNumber == 4)
        {
            GameObject Carrier1 = Instantiate(Carrier1Prefab, Flagship, FlagshipCenterPos.rotation);
            Carrier1.GetComponent<FleetMenuShipNumber>().FollowShipNumber = ListNumber;
            ShipManager.instance.FleetShipList[FlagshipNumber].GetComponent<FleetMenuShipNumber>().FollowShipList.Add(Carrier1);
        }
    }

    //���� ���� ���� ������ Ȱ��ȭ
    void FlagshipTurretIconGenerate()
    {
        FlagshipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        FlagshipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        FlagshipSlot3.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot3.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot3.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot3.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        FlagshipSlot4.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot4.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot4.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot4.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        FlagshipSlot5.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot5.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot5.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot5.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        FlagshipSlot6.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FlagshipSlot6.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FlagshipSlot6.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FlagshipSlot6.GetComponent<SlotIconActive>().Turret4.SetActive(false);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(true);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(true);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot3.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot3.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot3.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot3.GetComponent<SlotIconActive>().Turret4.SetActive(true);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot4.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot4.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot4.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot4.GetComponent<SlotIconActive>().Turret4.SetActive(true);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot5.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot5.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot5.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot5.GetComponent<SlotIconActive>().Turret4.SetActive(true);

        if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
            FlagshipSlot6.GetComponent<SlotIconActive>().Turret1.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
            FlagshipSlot6.GetComponent<SlotIconActive>().Turret2.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
            FlagshipSlot6.GetComponent<SlotIconActive>().Turret3.SetActive(true);
        else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
            FlagshipSlot6.GetComponent<SlotIconActive>().Turret4.SetActive(true);
    }

    //����� ���� ���� ������ Ȱ��ȭ
    void FormationShipTurretIconGenerate()
    {
        FormationShipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FormationShipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FormationShipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FormationShipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(false);
        FormationShipSlot1.GetComponent<SlotIconActive>().DifferentIcon.SetActive(false);

        FormationShipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(false);
        FormationShipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(false);
        FormationShipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(false);
        FormationShipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(false);
        FormationShipSlot2.GetComponent<SlotIconActive>().DifferentIcon.SetActive(false);
        int FirstShip = ShipManager.instance.SelectedFleetShips[0].GetComponent<FleetMenuShipNumber>().FollowShipNumber;

        for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
        {
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[FirstShip].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType)
            {
                if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
                    FormationShipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(true);
            }
            else
            {
                FormationShipSlot1.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FormationShipSlot1.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FormationShipSlot1.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FormationShipSlot1.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                FormationShipSlot1.GetComponent<SlotIconActive>().DifferentIcon.SetActive(true);
                break;
            }
            if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[FirstShip].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType)
            {
                if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 1)
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 2)
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 3)
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(true);
                else if (ShipManager.instance.FlagShipList[FleetNumber].GetComponent<FollowShipManager>().ShipList[ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber].GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().CannonType == 4)
                    FormationShipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(true);
            }
            else
            {
                FormationShipSlot2.GetComponent<SlotIconActive>().Turret1.SetActive(false);
                FormationShipSlot2.GetComponent<SlotIconActive>().Turret2.SetActive(false);
                FormationShipSlot2.GetComponent<SlotIconActive>().Turret3.SetActive(false);
                FormationShipSlot2.GetComponent<SlotIconActive>().Turret4.SetActive(false);
                FormationShipSlot2.GetComponent<SlotIconActive>().DifferentIcon.SetActive(true);
                break;
            }
        }
    }

    void Update()
    {
        if (MainMenuButtonSystem.FleetMenuMode == true)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    //Flagship1Window = false;
                    //FormationShip1Window = false;
                    //ShieldShip1Window = false;
                    //Carrier1Window = false;
                }
                if (touch.phase == TouchPhase.Ended) //���� ����
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
                            if (collider2D.gameObject.GetComponent<FleetMenuShipNumber>().isSelected == false) //���õ� �Լ� Ȱ��ȭ
                            {
                                if (collider2D.CompareTag("Player Flag Ship") && FormationShip1Window == 0 && ShieldShip1Window == 0 && Carrier1Window == 0)
                                {
                                    Flagship1Window++;
                                    if (SelectOneType == false)
                                        FlagshipInput.SetActive(true);
                                }
                                if (collider2D.CompareTag("Player Follow Ship") && Flagship1Window == 0 && ShieldShip1Window == 0 && Carrier1Window == 0)
                                {
                                    FormationShip1Window++;
                                    if (SelectOneType == false)
                                        FormationInput.SetActive(true);
                                }
                                if (collider2D.CompareTag("Player Shield Ship1") && Flagship1Window == 0 && FormationShip1Window == 0 && Carrier1Window == 0)
                                {
                                    ShieldShip1Window++;
                                    if (SelectOneType == false)
                                        ShieldInput.SetActive(true);
                                }
                                if (collider2D.CompareTag("Player Carrier1") && Flagship1Window == 0 && FormationShip1Window == 0 && ShieldShip1Window == 0)
                                {
                                    Carrier1Window++;
                                    if (SelectOneType == false)
                                        CarrierInput.SetActive(true);
                                }

                                collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(true);
                                ShipManager.instance.SelectedFleetShips.Add(collider2D.gameObject);
                                SelectFleetFramePrefab.GetComponent<Animator>().SetBool("Ship select, Select fleet mode frame", true);

                                if (Flagship1Window > 0)
                                    FlagshipTurretIconGenerate();
                                if (FormationShip1Window > 0)
                                    FormationShipTurretIconGenerate(); //����� 1���̻� ���ý�, ����� ����â ������Ʈ
                                if (BattleSave.Save1.FleetWeaponTutorial == true) //ù Ʃ�丮��
                                {
                                    TutorialSystem.FlagshipGearGuide1.SetActive(false);
                                    TutorialSystem.FlagshipGearGuide2.SetActive(true);
                                    TutorialSystem.FlagshipGearFrame1.SetActive(true);
                                    TutorialSystem.FlagshipGearFrame2.SetActive(true);
                                }
                            }
                            else //�̹� ���õǾ��� �Լ��� �ٽ� ���õǾ��� ���, ��Ȱ��ȭ
                            {
                                if (collider2D.CompareTag("Player Flag Ship"))
                                    Flagship1Window--;
                                if (collider2D.CompareTag("Player Follow Ship"))
                                    FormationShip1Window--;
                                if (collider2D.CompareTag("Player Shield Ship1"))
                                    ShieldShip1Window--;
                                if (collider2D.CompareTag("Player Carrier1"))
                                    Carrier1Window--;

                                collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(false);
                                ShipManager.instance.SelectedFleetShips.Remove(collider2D.gameObject);

                                if (FormationShip1Window > 0)
                                    FormationShipTurretIconGenerate();
                            }

                            collider2D.gameObject.GetComponent<FleetMenuShipNumber>().SwitchSelect();
                        }

                        if (SelectOneType == false) //������ Ÿ���� �Լ��� ������ ������ �ٸ� Ÿ���� �Լ����� ��� ���� ���ϵ��� ����
                        {
                            StartPosition = new Vector2(StartPosition.x - (100 / 2), StartPosition.y + (100 / 2));
                            currentMousePosition = new Vector2(StartPosition.x + 100, StartPosition.y - 100);
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition, FleetShipLayer);
                            foreach (Collider2D collider in collider2DArray)
                            {
                                if (collider != null)
                                {
                                    SelectOneType = true;
                                    if (collider.CompareTag("Player Flag Ship") && Flagship1Window == 0)
                                    {
                                        collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(true);
                                    }
                                    if (collider.CompareTag("Player Follow Ship") && FormationShip1Window == 0)
                                    {
                                        collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(true);
                                    }
                                    if (collider.CompareTag("Player Shield Ship1") && ShieldShip1Window == 0)
                                    {
                                        collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(true);
                                    }
                                    if (collider.CompareTag("Player Carrier1") && Carrier1Window == 0)
                                    {
                                        collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(true);
                                    }
                                }
                            }
                        }
                        if (Flagship1Window == 0 && FormationShip1Window == 0 && ShieldShip1Window == 0 && Carrier1Window == 0) //��� �Լ� ������ ���� ���� ���, ���ÿ��� ����ó���� �ٸ� �Լ����� �ٽ� �����ϵ��� ����
                        {
                            StartPosition = new Vector2(StartPosition.x - (100 / 2), StartPosition.y + (100 / 2));
                            currentMousePosition = new Vector2(StartPosition.x + 100, StartPosition.y - 100);
                            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition, FleetShipLayer);
                            foreach (Collider2D collider in collider2DArray)
                            {
                                if (collider != null)
                                {
                                    collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(false);
                                }
                            }
                            SelectFleetFramePrefab.GetComponent<Animator>().SetBool("Ship select, Select fleet mode frame", false);
                            SelectOneType = false;
                            FlagshipInput.SetActive(false);
                            FormationInput.SetActive(false);
                            ShieldInput.SetActive(false);
                            CarrierInput.SetActive(false);

                            if (BattleSave.Save1.FleetWeaponTutorial == true) //ù Ʃ�丮��
                            {
                                TutorialSystem.FlagshipGearGuide1.SetActive(true);
                                TutorialSystem.FlagshipGearGuide2.SetActive(false);
                                TutorialSystem.FlagshipGearFrame1.SetActive(false);
                                TutorialSystem.FlagshipGearFrame2.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    //���� �Դ�� �Ѿ ��, �̹� �����ߴ� �Դ븦 ��� ���
    public void SelectedShipInitialize()
    {
        Vector3 StartPosition = ShipManager.instance.SelectedFleetFlagship[0].transform.position;
        StartPosition = new Vector2(StartPosition.x - (100 / 2), StartPosition.y + (100 / 2));
        Vector3 currentMousePosition = new Vector2(StartPosition.x + 100, StartPosition.y - 100);
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(StartPosition, currentMousePosition, FleetShipLayer);
        foreach (Collider2D collider in collider2DArray)
        {
            if (collider != null)
            {
                collider.gameObject.GetComponent<FleetMenuShipNumber>().SelectedImage.SetActive(false);
                collider.gameObject.GetComponent<FleetMenuShipNumber>().DeselectedImage.SetActive(false);
                collider.gameObject.GetComponent<FleetMenuShipNumber>().isSelected = false;
            }
        }
        if (SelectFleetFramePrefab.GetComponent<Animator>().GetBool("Ship select, Select fleet mode frame") == true)
            SelectFleetFramePrefab.GetComponent<Animator>().SetBool("Ship select, Select fleet mode frame", false);
        SelectOneType = false;
        FlagshipInput.SetActive(false);
        FormationInput.SetActive(false);
        ShieldInput.SetActive(false);
        CarrierInput.SetActive(false);
        Flagship1Window = 0;
        FormationShip1Window = 0;
        ShieldShip1Window = 0;
        Carrier1Window = 0;
        ShipManager.instance.SelectedFleetShips.Clear();
    }

    //��ư�� ���� ������ ��ġ�� ��ȯ
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
}