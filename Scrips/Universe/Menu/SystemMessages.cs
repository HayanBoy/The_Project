using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMessages : MonoBehaviour
{
    [Header("스크립트")]
    public FleetFormationMenuSystem FleetFormationMenuSystem;
    public FleetMenuSystem FleetMenuSystem;
    public FlagshipManagerSystem FlagshipManagerSystem;
    public WordPrintMenu WordPrintMenu;
    public ShipSpawner ShipSpawner;
    public UpgradeMenu UpgradeMenu;
    public HurricaneOperationMenu HurricaneOperationMenu;
    public CashResourceSystem CashResourceSystem;
    public MainMenuButtonSystem MainMenuButtonSystem;
    public LoadDataManager LoadDataManager;

    [Header("창 디자인")]
    public GameObject MainWindow1;
    public GameObject ProgressWindow1;
    public GameObject ProgressWindow2;
    public GameObject LabProgressWindow1;
    public GameObject SecondWindow1;
    public GameObject SecondWindow2;

    [Header("진행 버튼")]
    public GameObject FirstSelectButtonPrefab;
    public GameObject FirstAmountButtonPrefab;
    public GameObject AcceptPrefab;
    public GameObject CancelPrefab;
    public GameObject MessagePrefab;
    public Image AcceptButtonImage;
    public Image CancelButtonImage;
    private bool AcceptClick;
    private bool CancelClick;
    private bool OKClick;
    private int ProcessNumber; //창을 닫을 때, 진행 창의 연출 번호

    [Header("두번째 진행 메시지 버튼")]
    public GameObject SecondMessagePrefab;
    public GameObject SecondOKButtonPrefab;
    public Image SecondOKButtonImage;
    private bool SecondOKClick;

    [Header("메시지 번호")]
    public int MessageType; //메시지 타입. 1 = 함선 장비 메뉴, 2 = 함대의 편대 메뉴
    public int MessageNumber; //메시지 출력 번호
    public int MainTabNumber; //업그레이드 탭 번호
    public int SubTabNumber;
    public int AccessNumber; //업그레이드에서 선택한 메뉴 번호
    public int UpgradeNumber; //선택한 메뉴에서 어떤 업그레이드를 진행하는지에 대한 번호
    public int SubUpgradeNumber; //UpgradeNumber의 서브 번호
    public Text AmountNumberText;
    public int AmountNumber;

    [Header("함대의 편대 메뉴")]
    public RectTransform AcceptButtons;

    [Header("언락 및 지불 승인")]
    public int GlopaorosCostProcess; //비용을 지불하기 위해 전송된 자금
    public int ConstructionResourceProcess;
    public int TaritronicProcess;
    public string UnlockName; //언락 이름
    public int WeaponUnlockStep; //언락 절차

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;
    public AudioClip MainInformOnAudio;
    public AudioClip MainInformOffAudio;
    public AudioClip InformOnAudio;
    public AudioClip InformOffAudio;

    private int[] ShipList;

    //수락 버튼
    public void AcceptButtonClick()
    {
        if (MessageType == 2 && MessageNumber == 1) //함대 총 편대수 증가 승인
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess)
            {
                ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FlagshipNumber].GetComponent<FollowShipManager>().FormationStorage++;
                WordPrintMenu.ManagerTotalFleets.text = string.Format("{0}", ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FlagshipNumber].GetComponent<FollowShipManager>().FormationStorage);
                WordPrintMenu.FleetFormationManagerPrint(FleetFormationMenuSystem.FleetNumber);
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
            }
            else if (BattleSave.Save1.NarihaUnionGlopaoros < GlopaorosCostProcess || BattleSave.Save1.ConstructionResource < ConstructionResourceProcess || BattleSave.Save1.Taritronic < TaritronicProcess)
            {
                WordPrintMenu.FleetAddFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }
        //선택된 함선을 다른 함대로 이전 승인
        else if (MessageType == 2 && MessageNumber == 2 && ShipManager.instance.SelectedFleetShips.Count + ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count <= ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage)
        {
            //선택된 함선들을 다른 함대로 배치
            GameObject PreviousFlagship = ShipManager.instance.FlagShipList[FleetFormationMenuSystem.SelectedFleetNumber].gameObject;
            GameObject SelectedFlagship = ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].gameObject;

            for (int SelectedShipNumber = 0; SelectedShipNumber < ShipManager.instance.SelectedFleetShips.Count; SelectedShipNumber++) //선택한 함선을 선택한 기함으로 소속 이전 진행
            {
                int number = ShipManager.instance.SelectedFleetShips[SelectedShipNumber].GetComponent<FleetMenuShipNumber>().FollowShipNumber;

                PreviousFlagship.GetComponent<FollowShipManager>().ShipAccount--; //기존의 기함의 함선수 감소
                PreviousFlagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().MyFlagship = SelectedFlagship; //선택된 기함으로 소속 기함을 변경

                SelectedFlagship.GetComponent<FollowShipManager>().ShipAccount++; //선택한 기함의 함선수 증가

                //함대의 배열 상태에 맞게 배열 번호와 위치를 지정, 배열 번호는 이미 메서드에서 자동으로 재설정 해주므로 SelectedShipNumber를 해도 문제없다.
                ShipSpawner.FormationNumberSet(SelectedFlagship, PreviousFlagship.GetComponent<FollowShipManager>().ShipList[number].gameObject, SelectedFlagship.GetComponent<MoveVelocity>().FlagshipNumber, number);

                PreviousFlagship.GetComponent<FollowShipManager>().ShipList[number].GetComponent<MoveVelocity>().TransferWarp(); //이전 받은 함선에게 거리가 멀리 있는 기함에게 워프 실시
            }

            //이전 함대의 선택된 함선들을 순차적으로 삭제 실시
            ShipList = new int[ShipManager.instance.SelectedFleetShips.Count]; //가장 큰 함선 번호부터 차례대로 실행해야 함선 번호가 어긋나지 않으므로, 숫자 정렬을 먼저 시작
            for (int i = 0; i < ShipManager.instance.SelectedFleetShips.Count; i++)
            {
                ShipList[i] = ShipManager.instance.SelectedFleetShips[i].GetComponent<FleetMenuShipNumber>().FollowShipNumber;
            }
            Array.Sort(ShipList); //숫자 정렬(가장 작은 숫자순)
            Array.Reverse(ShipList); //내림차순으로 역순처리(가장 큰 숫자순으로 변환)

            for (int k = 0; k < ShipManager.instance.SelectedFleetShips.Count; k++)
            {
                int SelectedShipNumber = ShipList[k];
                PreviousFlagship.GetComponent<FollowShipManager>().ShipList.Remove(PreviousFlagship.GetComponent<FollowShipManager>().ShipList[SelectedShipNumber].gameObject); //기존 함대의 함선 리스트에서 선택한 함선을 제외
            }

            FleetFormationMenuSystem.FleetSelectStep = 1;
            FleetFormationMenuSystem.FleetSelectCancelButtonClick();
            StartCoroutine(MessageOff());
        }
        //선택된 함선을 다른 함대로 이전 불허(이전하려는 함선과 선택된 기함의 최대 편대수의 합산이 선택된 기함의 편대수를 초과)
        else if (MessageType == 2 && MessageNumber == 2 && ShipManager.instance.SelectedFleetShips.Count + ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage > ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage)
        {
            WordPrintMenu.WarnningFleetPrint();
            StartCoroutine(SecondMessageOn(1));
        }
        //선택한 함선을 구매하여, 선택한 함대에 배치
        else if (MessageType == 2 && MessageNumber == 3)
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess)
            {
                ShipSpawner.NewDeployed = true;
                ShipSpawner.DeployShip(ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].gameObject, FleetFormationMenuSystem.ShipProductionNumber, AmountNumber);

                FleetFormationMenuSystem.ShipProductionStep = 1;
                FleetFormationMenuSystem.FleetSelectCancelButtonClick();
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
            }
            else if (BattleSave.Save1.NarihaUnionGlopaoros < GlopaorosCostProcess || BattleSave.Save1.ConstructionResource < ConstructionResourceProcess || BattleSave.Save1.Taritronic < TaritronicProcess)
            {
                FleetFormationMenuSystem.ShipProductionStep = 1;
                WordPrintMenu.ShipProductionFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }
        //기함 새배치를 승인
        else if (MessageType == 3 && MessageNumber == 1)
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess)
            {
                ShipSpawner.DelpoyFlagship();
                FlagshipManagerSystem.FlagshipListLoad();
                WordPrintMenu.FlagshipAmount();
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
            }
            else if (BattleSave.Save1.NarihaUnionGlopaoros < GlopaorosCostProcess || BattleSave.Save1.ConstructionResource < ConstructionResourceProcess || BattleSave.Save1.Taritronic < TaritronicProcess)
            {
                WordPrintMenu.NewFlagshipCashFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }
        //업그레이드 진행 승인
        else if (MessageType == 4)
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess)
            {
                UpgradeDataSystem.instance.UpgradeStart(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
            }
            else if (BattleSave.Save1.NarihaUnionGlopaoros < GlopaorosCostProcess || BattleSave.Save1.ConstructionResource < ConstructionResourceProcess || BattleSave.Save1.Taritronic < TaritronicProcess)
            {
                WordPrintMenu.UpgradeTableUnlockFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }

        //무기 언락 절차
        else if (MessageType == 0 && MessageNumber == 0 && WeaponUnlockStep > 0)
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess) //승인
            {
                if (WeaponUnlockStep == 1)
                {
                    WeaponUnlockStep = 0;
                    FleetMenuSystem.UnlockStart(UnlockName);
                    FleetMenuSystem.ShipWeaponUnlockState();
                }
                else if (WeaponUnlockStep == 2)
                {
                    WeaponUnlockStep = 0;
                    FleetFormationMenuSystem.UnlockStart(UnlockName);
                    FleetFormationMenuSystem.ShipUnlockState();
                }
                else if (WeaponUnlockStep == 3)
                {
                    WeaponUnlockStep = 0;
                    FlagshipManagerSystem.UnlockStart(UnlockName);
                }
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
            }
            else //자금 부족으로 미승인
            {
                WordPrintMenu.UpgradeTableUnlockFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }

        //연구 언락 절차
        else if (MessageType == 0 && MessageNumber == 0 && UpgradeMenu.UnlockStep == 1)
        {
            if (BattleSave.Save1.NarihaUnionGlopaoros >= GlopaorosCostProcess && BattleSave.Save1.ConstructionResource >= ConstructionResourceProcess && BattleSave.Save1.Taritronic >= TaritronicProcess) //승인
            {
                UpgradeMenu.UnlockStart(UnlockName);
                StartCoroutine(MessageOff());
                CashResourceSystem.ResetCash(GlopaorosCostProcess, ConstructionResourceProcess, TaritronicProcess);
                UpgradeMenu.CancelNumber = 1;
                UpgradeMenu.UnlockStep = 0;
            }
            else //자금 부족으로 미승인
            {
                WordPrintMenu.UpgradeTableUnlockFail();
                StartCoroutine(SecondMessageOn(1));
            }
        }

        //허리케인 무기 선택 씬으로 넘어가도록 조취
        else if (MessageType == 0 && MessageNumber == 0 && HurricaneOperationMenu.MenuStep == 2)
        {
            StartCoroutine(HurricaneOperationMenu.StartHurricaneOperation());
        }

        //게임 나가기
        else if (MessageType == 0 && MessageNumber == 0 && MainMenuButtonSystem.GameExitMessage == true)
        {
            BattleSave.Save1.GroundBattleCount = 1;
            BattleSave.Save1.GoBackTitle = true;
            DataSaveManager.instance.enabled = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("Main menu");
        }

        //세이브 저장을 실행하기
        else if (MessageType == 0 && MessageNumber == 0 && LoadDataManager.SaveStart == true)
        {
            LoadDataManager.SaveStart = false;
            StartCoroutine(DataSaveManager.instance.SaveStart(LoadDataManager.DataPath, LoadDataManager.TableNumber));
            LoadDataManager.InitializeClickTable();
            LoadDataManager.TableNumber = 0;
            LoadDataManager.DataPath = null;
            StartCoroutine(MessageOff());
        }

        //세이브 삭제를 실행하기
        else if (MessageType == 0 && MessageNumber == 0 && LoadDataManager.DeleteStart == true)
        {
            LoadDataManager.DeleteStart = false;
            DataSaveManager.instance.DeleteData(LoadDataManager.DataPath);
            DataSaveManager.instance.DeleteData(LoadDataManager.TableDataPath);
            LoadDataManager.InitializeClickTable();
            LoadDataManager.TableNumber = 0;
            LoadDataManager.DataPath = null;
            StartCoroutine(MessageOff());
        }

        GlopaorosCostProcess = 0;
        ConstructionResourceProcess = 0;
        TaritronicProcess = 0;

        WordPrintMenu.TotalCostText.gameObject.SetActive(false);
    }
    public void AcceptButtonDown()
    {
        AcceptClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        AcceptPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void AcceptButtonUp()
    {
        if (AcceptClick == true)
        {
            AcceptPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        AcceptClick = false;
    }
    public void AcceptButtonEnter()
    {
        if (AcceptClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            AcceptPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void AcceptButtonExit()
    {
        if (AcceptClick == true)
        {
            AcceptPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //취소 버튼
    public void CancelButtonClick()
    {
        if (FleetFormationMenuSystem.ShipProductionStep == 2)
            FleetFormationMenuSystem.ShipProductionStep = 1;

        else if (HurricaneOperationMenu.MenuStep == 2)
        {
            HurricaneOperationMenu.MenuStep = 1;
        }

        else if (MainMenuButtonSystem.GameExitMessage == true)
            MainMenuButtonSystem.GameExitMessage = false;

        if (LoadDataManager.SaveStart == true)
            LoadDataManager.SaveStart = false;

        if (LoadDataManager.DeleteStart == true)
            LoadDataManager.DeleteStart = false;

        StartCoroutine(MessageOff());
    }
    public void CancelButtonDown()
    {
        CancelClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        CancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelButtonUp()
    {
        if (CancelClick == true)
        {
            CancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelClick = false;
    }
    public void CancelButtonEnter()
    {
        if (CancelClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            CancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelButtonExit()
    {
        if (CancelClick == true)
        {
            CancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //두번째 메시지 OK버튼
    public void SecondOKButtonClick()
    {
        StartCoroutine(SecondMessageOff());
        StartCoroutine(MessageOff());
    }
    public void SecondOKButtonDown()
    {
        SecondOKClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        SecondOKButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void SecondOKButtonUp()
    {
        if (SecondOKClick == true)
        {
            SecondOKButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        SecondOKClick = false;
    }
    public void SecondOKButtonEnter()
    {
        if (SecondOKClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            SecondOKButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void SecondOKButtonExit()
    {
        if (SecondOKClick == true)
        {
            SecondOKButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //수량 조절 버튼
    public void NumberUpClick()
    {
        if (MessageType == 2 && MessageNumber == 3 && AmountNumber + ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count < ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage) //배치할 함대를 선택후, 수량 증가 버튼 이후, 동기화할 메시지 내역
        {
            AmountNumber++;

            if (AmountNumber + ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count >= ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage)
                AmountNumber = ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage - ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count;
            
            AmountNumberText.text = string.Format("{0}", AmountNumber);
            WordPrintMenu.FleetFormationManagerFleetAddProgressPrint(FleetFormationMenuSystem.FleetNumber);
        }
    }
    public void NumberDownClick()
    {
        if (MessageType == 2 && MessageNumber == 3 && AmountNumber > 1) //배치할 함대를 선택후, 수량 감소 버튼 이후, 동기화할 메시지 내역
        {
            AmountNumber--;

            if (AmountNumber <= 1)
                AmountNumber = 1;

            AmountNumberText.text = string.Format("{0}", AmountNumber);
            WordPrintMenu.FleetFormationManagerFleetAddProgressPrint(FleetFormationMenuSystem.FleetNumber);
        }
    }

    //메시지 활성화
    public IEnumerator MessageOn(int number, int ProgressNumber) //첫번째 : 메인 메시지 창 유형, 두번째 : 생산 진행 창 유형
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", MainInformOnAudio);
        ProcessNumber = ProgressNumber;
        MessagePrefab.SetActive(true);
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 1);
        MessagePrefab.GetComponent<Animator>().SetFloat("Ask, Main message", number);
        MessagePrefab.GetComponent<Animator>().SetFloat("Window number, Main message", ProgressNumber);
        yield return new WaitForSecondsRealtime(0.2f);
        AcceptButtonImage.raycastTarget = true;
        CancelButtonImage.raycastTarget = true;
    }
    //메시지 종료
    public IEnumerator MessageOff()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", MainInformOffAudio);
        WordPrintMenu.ManagerSelectedShip.gameObject.SetActive(false);
        WordPrintMenu.UpgradeTextsPrefab.SetActive(false);
        WordPrintMenu.TotalCostText.text = string.Format("");

        AcceptButtonImage.raycastTarget = false;
        CancelButtonImage.raycastTarget = false;
        FleetFormationMenuSystem.FleetSelectOkImage.raycastTarget = true;
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 2);
        MessagePrefab.GetComponent<Animator>().SetFloat("Window number(Off), Main message", ProcessNumber);
        yield return new WaitForSecondsRealtime(0.2f);

        //메시지 창 초기화
        MessagePrefab.GetComponent<Animator>().SetFloat("Ask, Main message", 0);
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 0);
        MessagePrefab.SetActive(false);

        if (MainWindow1.activeSelf == true)
            MainWindow1.SetActive(false);
        if (ProgressWindow1.activeSelf == true)
            ProgressWindow1.SetActive(false);
        if (ProgressWindow2.activeSelf == true)
            ProgressWindow2.SetActive(false);
        if (LabProgressWindow1.activeSelf == true)
            LabProgressWindow1.SetActive(false);
        if (SecondWindow1.activeSelf == true)
            SecondWindow1.SetActive(false);
        if (SecondWindow2.activeSelf == true)
            SecondWindow2.SetActive(false);

        MessageType = 0;
        MessageNumber = 0;
        FirstAmountButtonPrefab.SetActive(false);
    }

    //두번째 메시지 활성화
    public IEnumerator SecondMessageOn(int number)
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", InformOnAudio);
        SecondMessagePrefab.SetActive(true);

        if (number == 1)
        {
            SecondMessagePrefab.GetComponent<Animator>().SetFloat("Message type, Second message", number);
        }
        //FleetAddMessage.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
        yield return new WaitForSecondsRealtime(0.2f);
        SecondOKButtonImage.raycastTarget = true;
    }
    //두번째 메시지 종료
    public IEnumerator SecondMessageOff()
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", InformOffAudio);
        SecondOKButtonImage.raycastTarget = false;
        //FleetAddMessage.GetComponent<Animator>().SetBool("Cancel fleet Click, Fleet menu", true);
        yield return new WaitForSecondsRealtime(0.2f);

        SecondMessagePrefab.GetComponent<Animator>().SetFloat("Message type, Second message", 0);
        SecondMessagePrefab.SetActive(false);

        MessageType = 0;
        MessageNumber = 0;
    }
}