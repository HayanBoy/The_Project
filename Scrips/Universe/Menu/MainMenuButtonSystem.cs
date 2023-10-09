using System.Collections;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuButtonSystem : MonoBehaviour
{
    [Header("스크립트")]
    public UniverseMapSystem UniverseMapSystem;
    public RTSControlSystem RTSControlSystem;
    public CameraZoom CameraZoom;
    public FleetMenuSystem FleetMenuSystem;
    public FleetFormationMenuSystem FleetFormationMenuSystem;
    public FlagshipManagerSystem FlagshipManagerSystem;
    public UpgradeMenu UpgradeMenu;
    public SystemOption SystemOption;
    public WordPrintMenu WordPrintMenu;
    public SystemMessages SystemMessages;
    public SaveInput SaveInput;
    public HurricaneOperationMenu HurricaneOperationMenu;
    LoadDataManager LoadDataManager;

    [Header("메인메뉴")]
    public bool MenuMode; //메뉴 버튼 모드
    public bool MenuClick; //메뉴 버튼 클릭 스위치
    public GameObject MenuListPrefab;
    public GameObject MenuButtonAnim; //메뉴 UI 애니메이션
    public Image MenuButtonImage;
    public GameObject UniverseMapAnim;
    public GameObject FramePrafab; //함대전 프레임 틀
    public GameObject CashListPrefab; //재화 표기 시스템
    public GameObject LiveCommunication; //실시간 자막 및 워프 항법 자막

    Coroutine startMainMenu;
    Coroutine endMainMenu;
    Coroutine beforeMenuOnline;
    Coroutine beforeMenuOffline;

    [Header("메인메뉴 나가기")]
    public GameObject CancelButtonPrefab; //함대 메뉴 나가기 버튼 프리팹
    public bool CancelClick; //함대 메뉴 나가기 버튼 클릭 스위치

    [Header("함대 장비 메뉴")]
    public bool FleetMenuMode; //함대 장비 메뉴 버튼 모드
    public bool FleetMenuClick; //함대 장비 메뉴 버튼 클릭 스위치
    public GameObject FleetMenuButtonAnim; //함대 장비 메뉴 UI 애니메이션
    public GameObject FleetMenuWindow; //함대 장비 메뉴창 프리팹

    [Header("함대 배열 메뉴")]
    public bool FleetFormationMenuMode; //함대 배열 메뉴 버튼 모드
    public bool FleetFormationClick; //함대 배열 메뉴 버튼 클릭 스위치
    public GameObject FleetFormationMenuButtonAnim; //함대 배열 메뉴 UI 애니메이션
    public GameObject FleetFormationMenuWindow; //함대 배열 메뉴창 프리팹

    [Header("기함 관리 메뉴")]
    public bool FlagshipMenuMode; //기함 메뉴 버튼 모드
    public bool FlagshipMenuClick; //기함 메뉴 버튼 클릭 스위치
    public GameObject FlagshipMenuButtonAnim; //기함 메뉴 UI 애니메이션
    public GameObject FlagshipMenuWindow; //기함 메뉴창 프리팹

    [Header("연구 메뉴")]
    public bool LabMenuMode; //연구 메뉴 버튼 모드
    public bool LabMenuClick; //연구 메뉴 버튼 클릭 스위치
    public GameObject LabMenuButtonAnim; //연구 메뉴 UI 애니메이션
    public GameObject LabMenuWindow; //연구 메뉴창 프리팹

    [Header("시스템 설정")]
    public bool SystemOptionClick; //연구 메뉴 버튼 클릭 스위치
    public GameObject SystemOptionButtonAnim; //연구 메뉴 UI 애니메이션

    [Header("게임 나가기")]
    public bool GameExitClick; //연구 메뉴 버튼 클릭 스위치
    public bool GameExitMessage = false;
    public GameObject GameExitButtonAnim; //연구 메뉴 UI 애니메이션

    [Header("게임 세이브")]
    public bool GameSaveClick; //연구 메뉴 버튼 클릭 스위치
    public GameObject GameSaveButtonPrefab; //연구 메뉴 UI 애니메이션
    public GameObject LoadWindowPrefab;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip IconSelectAudio;
    public AudioClip CancelUIAudio;

    //메인 메뉴
    public void MainMenuButtonClick()
    {
        MenuMode = true;
        FleetMenuSystem.enabled = true;
        FleetFormationMenuSystem.enabled = true;
        FlagshipManagerSystem.enabled = true;
        UpgradeMenu.enabled = true;
        FramePrafab.SetActive(false);
        if (beforeMenuOffline != null)
            StopCoroutine(beforeMenuOffline);
        beforeMenuOnline = StartCoroutine(BeforeMenuOnline());
        if (endMainMenu != null)
            StopCoroutine(endMainMenu);
        startMainMenu = StartCoroutine(StartMainMenu());
    }
    public void MainMenuButtonDown()
    {
        MenuClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        MenuButtonAnim.GetComponent<Animator>().SetBool("Click, Main menu", true);
    }
    public void MainMenuButtonUp()
    {
        if (MenuClick == true)
        {
            MenuButtonAnim.GetComponent<Animator>().SetBool("Click, Main menu", false);
        }
        MenuClick = false;
    }
    public void MainMenuButtonEnter()
    {
        if (MenuClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            MenuButtonAnim.GetComponent<Animator>().SetBool("Click, Main menu", true);
        }
    }
    public void MainMenuButtonExit()
    {
        if (MenuClick == true)
        {
            MenuButtonAnim.GetComponent<Animator>().SetBool("Click, Main menu", false);
        }
    }

    public void CancelButtonClick()
    {
        endMainMenu = StartCoroutine(EndMainMenu());
    }

    public void CancelButtonDown()
    {
        CancelClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelButtonUp()
    {
        if (CancelClick == true)
        {
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelClick = false;
    }
    public void CancelButtonEnter()
    {
        if (CancelClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelButtonExit()
    {
        if (CancelClick == true)
        {
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //세이브 메뉴를 불러오기
    public void SaveButtonClick()
    {
        LoadWindowPrefab.SetActive(true);
        LoadDataManager = FindObjectOfType<LoadDataManager>();
        LoadDataManager.GetSaveDataList();
    }

    public void SaveButtonDown()
    {
        GameSaveClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        GameSaveButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void SaveButtonUp()
    {
        if (GameSaveClick == true)
        {
            GameSaveButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        GameSaveClick = false;
    }
    public void SaveButtonEnter()
    {
        if (GameSaveClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            GameSaveButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void SaveButtonExit()
    {
        if (GameSaveClick == true)
        {
            GameSaveButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //메뉴 리스트 되돌리기
    public void MenuListBack()
    {
        MenuListPrefab.SetActive(true);
        MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 1);
    }

    //메인 메뉴 활성화 애니메이션
    IEnumerator StartMainMenu()
    {
        MenuButtonAnim.GetComponent<Animator>().SetBool("Menu booting, Main menu", true);
        MenuButtonImage.raycastTarget = false;
        RTSControlSystem.enabled = false;
        MenuListPrefab.SetActive(true);
        MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 1);
        CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 2);
        yield return new WaitForSecondsRealtime(0.05f);
    }
    //메인 메뉴 비활성화 애니메이션
    IEnumerator EndMainMenu()
    {
        if (startMainMenu != null)
            StopCoroutine(startMainMenu);
        if (beforeMenuOnline != null)
            StopCoroutine(beforeMenuOnline);
        MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 2);
        FleetMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        FleetFormationMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        FlagshipMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        LabMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        yield return new WaitForSecondsRealtime(0.05f);
        RTSControlSystem.enabled = true;
        yield return new WaitForSecondsRealtime(0.5f);
        MenuButtonAnim.GetComponent<Animator>().SetBool("Menu booting, Main menu", false);
        MenuListPrefab.GetComponent<Animator>().SetFloat("Menu online, Menu", 0);
        yield return new WaitForSecondsRealtime(0.05f);
        FramePrafab.SetActive(true);

        MenuMode = false;
        FleetMenuSystem.enabled = false;
        FleetFormationMenuSystem.enabled = false;
        FlagshipManagerSystem.enabled = false;
        UpgradeMenu.enabled = false;

        beforeMenuOffline = StartCoroutine(BeforeMenuOffline());
        MenuButtonImage.raycastTarget = true;
        MenuListPrefab.SetActive(false);
        CashListPrefab.GetComponent<Animator>().SetFloat("Position, Cash list", 0);
    }

    //함대 장비 메뉴
    public void FleetMenuButtonClick()
    {
        FleetMenuMode = true;
        FleetMenuSystem.StartFleetMenu();
    }
    public void FleetMenuButtonDown()
    {
        FleetMenuClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        FleetMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void FleetMenuButtonUp()
    {
        if (FleetMenuClick == true)
        {
            FleetMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        FleetMenuClick = false;
    }
    public void FleetMenuButtonEnter()
    {
        if (FleetMenuClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            FleetMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void FleetMenuButtonExit()
    {
        if (FleetMenuClick == true)
        {
            FleetMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //함대 배열 메뉴
    public void FleetFormationMenuButtonClick()
    {
        FleetFormationMenuMode = true;
        FleetFormationMenuSystem.StartFleetFormationMenu();
    }
    public void FleetFormationMenuButtonDown()
    {
        FleetFormationClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        FleetFormationMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void FleetFormationMenuButtonUp()
    {
        if (FleetFormationClick == true)
        {
            FleetFormationMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        FleetFormationClick = false;
    }
    public void FleetFormationMenuButtonEnter()
    {
        if (FleetFormationClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            FleetFormationMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void FleetFormationMenuButtonExit()
    {
        if (FleetFormationClick == true)
        {
            FleetFormationMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //기함 관리 메뉴
    public void FlagshipMenuButtonClick()
    {
        FlagshipMenuMode = true;
        FlagshipManagerSystem.StartFlagshipManagerMenu();
    }
    public void FlagshipMenuButtonDown()
    {
        FlagshipMenuClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        FlagshipMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void FlagshipMenuButtonUp()
    {
        if (FlagshipMenuClick == true)
        {
            FlagshipMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        FlagshipMenuClick = false;
    }
    public void FlagshipMenuButtonEnter()
    {
        if (FlagshipMenuClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            FlagshipMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void FlagshipMenuButtonExit()
    {
        if (FlagshipMenuClick == true)
        {
            FlagshipMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //연구 메뉴
    public void LabMenuButtonClick()
    {
        LabMenuMode = true;
        UpgradeMenu.StartLabMenu();
    }
    public void LabMenuButtonDown()
    {
        LabMenuClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        LabMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void LabMenuButtonUp()
    {
        if (LabMenuClick == true)
        {
            LabMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        LabMenuClick = false;
    }
    public void LabMenuButtonEnter()
    {
        if (LabMenuClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            LabMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void LabMenuButtonExit()
    {
        if (LabMenuClick == true)
        {
            LabMenuButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //시스템 설정 메뉴
    public void SystemOptionsButtonClick()
    {
        SystemOption.OpenSystemOption();
    }
    public void SystemOptionsButtonDown()
    {
        SystemOptionClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        SystemOptionButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void SystemOptionsButtonUp()
    {
        if (SystemOptionClick == true)
        {
            SystemOptionButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        SystemOptionClick = false;
    }
    public void SystemOptionsButtonEnter()
    {
        if (SystemOptionClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            SystemOptionButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void SystemOptionsButtonExit()
    {
        if (SystemOptionClick == true)
        {
            SystemOptionButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //게임 나가기 메뉴
    public void GameExitButtonClick()
    {
        GameExitMessage = true;
        WordPrintMenu.GoToTitle();
        StartCoroutine(SystemMessages.MessageOn(2, 0));

    }
    public void GameExitButtonDown()
    {
        GameExitClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
        GameExitButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
    }
    public void GameExitButtonUp()
    {
        if (GameExitClick == true)
        {
            GameExitButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
        GameExitClick = false;
    }
    public void GameExitButtonEnter()
    {
        if (GameExitClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", IconSelectAudio);
            GameExitButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", true);
        }
    }
    public void GameExitButtonExit()
    {
        if (GameExitClick == true)
        {
            GameExitButtonAnim.GetComponent<Animator>().SetBool("Click icon, Menu", false);
        }
    }

    //어떤 메뉴창이 활성화 되기 전에 미리 함대전 UI버튼들을 비활성화하는 애니메이션
    IEnumerator BeforeMenuOnline()
    {
        //함대전 UI버튼 끄기
        Time.timeScale = 0;
        UniverseMapSystem.CameraFollow.WarpLiveLogs.SetActive(false);
        UniverseMapSystem.UniverseMapButtonImage.raycastTarget = false;
        UniverseMapSystem.UIControlSystem.ShipModeUIImage.raycastTarget = false;
        UniverseMapSystem.UIControlSystem.BehaviorModeUIImage.raycastTarget = false;
        UniverseMapSystem.UIControlSystem.SelectButtenImage.raycastTarget = false;
        UniverseMapSystem.CameraZoom.CameraImage.raycastTarget = false;
        UniverseMapSystem.MultiFlagshipSystem.FlagshipListButtonImage.raycastTarget = false;
        UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonImage.raycastTarget = false;
        if (UniverseMapSystem.MultiFlagshipSystem.FlagshipListMode == true)
            UniverseMapSystem.MultiFlagshipSystem.FlagshipListButtonClick();
        if (HurricaneOperationMenu.MenuStep > 0)
            HurricaneOperationMenu.HurricaneOperationButtonClick();

        yield return new WaitForSecondsRealtime(0.05f);
        UniverseMapSystem.UIControlSystem.ShipModeUI.GetComponent<Animator>().SetBool("Menu booting, Ship Mode Butten", true);
        UniverseMapSystem.UIControlSystem.BehaviorModeUI.GetComponent<Animator>().SetBool("Menu booting, Behavior Butten", true);
        UniverseMapSystem.UIControlSystem.SelectButtenUI.GetComponent<Animator>().SetBool("Menu booting, Select Butten", true);
        UniverseMapSystem.UIControlSystem.UniverseFrame.GetComponent<Animator>().SetBool("Menu booting, Universe Frame", true);
        UniverseMapSystem.CameraZoom.CameraUI.GetComponent<Animator>().SetBool("Menu booting, Camera", true);
        UniverseMapSystem.MultiFlagshipSystem.FlagshipListButton.GetComponent<Animator>().SetBool("Menu booting, Flagship list", true);
        UniverseMapAnim.GetComponent<Animator>().SetBool("Menu booting, Universe Map Butten", true);
        UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Menu booting, Hurricane operation", true);
        if (UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().GetFloat("Online, Hurricane operation") == 2)
            UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetFloat("Online, Hurricane operation", 0);
        if (UniverseMapSystem.HurricaneOperationMenu.PlanetAnime.GetComponent<Animator>().GetFloat("Active, Hurricane operation planet") < 2)
            UniverseMapSystem.HurricaneOperationMenu.PlanetAnime.SetActive(false);
        UniverseMapSystem.HurricaneOperationMenu.HurricaneAnimePrefab.GetComponent<SpriteMask>().enabled = false;

        LiveCommunication.SetActive(false);
    }

    //어떤 메뉴창이 꺼지고 메인 메뉴 창으로 돌아가기 전에 미리 함대전 UI버튼들을 활성화하는 애니메이션
    IEnumerator BeforeMenuOffline()
    {
        //함대전 UI로 복귀
        UniverseMapSystem.UIControlSystem.ShipModeUI.GetComponent<Animator>().SetBool("Menu booting, Ship Mode Butten", false);
        UniverseMapSystem.UIControlSystem.BehaviorModeUI.GetComponent<Animator>().SetBool("Menu booting, Behavior Butten", false);
        UniverseMapSystem.UIControlSystem.SelectButtenUI.GetComponent<Animator>().SetBool("Menu booting, Select Butten", false);
        UniverseMapSystem.UIControlSystem.UniverseFrame.GetComponent<Animator>().SetBool("Menu booting, Universe Frame", false);
        UniverseMapSystem.CameraZoom.CameraUI.GetComponent<Animator>().SetBool("Menu booting, Camera", false);
        UniverseMapSystem.MultiFlagshipSystem.FlagshipListButton.GetComponent<Animator>().SetBool("Menu booting, Flagship list", false);
        UniverseMapAnim.GetComponent<Animator>().SetBool("Menu booting, Universe Map Butten", false);
        UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonPrefab.GetComponent<Animator>().SetBool("Menu booting, Hurricane operation", false);
        UniverseMapSystem.HurricaneOperationMenu.PlanetAnime.SetActive(true);
        UniverseMapSystem.HurricaneOperationMenu.HurricaneAnimePrefab.GetComponent<SpriteMask>().enabled = true;
        SystemMessages.FirstAmountButtonPrefab.SetActive(false);

        yield return new WaitForSecondsRealtime(0.05f);
        UniverseMapSystem.UniverseMapButtonImage.raycastTarget = true;
        UniverseMapSystem.UIControlSystem.MenuUIImage.raycastTarget = true;
        UniverseMapSystem.UIControlSystem.ShipModeUIImage.raycastTarget = true;
        UniverseMapSystem.UIControlSystem.BehaviorModeUIImage.raycastTarget = true;
        UniverseMapSystem.UIControlSystem.SelectButtenImage.raycastTarget = true;
        UniverseMapSystem.CameraZoom.CameraImage.raycastTarget = true;
        UniverseMapSystem.MultiFlagshipSystem.FlagshipListButtonImage.raycastTarget = true;
        UniverseMapSystem.HurricaneOperationMenu.HurricaneOperationButtonImage.raycastTarget = true;
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
        LiveCommunication.SetActive(true);

        Time.timeScale = 1;
    }
}