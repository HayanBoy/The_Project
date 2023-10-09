using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    [Header("스크립트")]
    public UniverseMapSystem UniverseMapSystem;
    public HurricaneOperationMenu HurricaneOperationMenu;
    public LiveCommunicationSystem LiveCommunicationSystem;
    public RandomSiteBattle RandomSiteBattle;
    public RandomSiteBattleTutorial RandomSiteBattleTutorial;
    public PlanetOurForceShipsManager PlanetOurForceShipsManager;
    public WordPrintSystem WordPrintSystem;
    DataSaveManager DataSaveManager;

    [Header("튜토리얼 창")]
    public GameObject TutorialWindowPrefab;
    public GameObject MessageWindowPrefab;
    public GameObject TextTutorialWindowPrefab;

    [Header("튜토리얼 소스")]
    public GameObject MainViewer;
    public GameObject ImagePrefab;
    public RenderTexture VideoRenderer;
    public VideoPlayer TutorialVideoPlayer;

    [Header("이미지 및 비디오")]
    public VideoClip TutorialStep1;
    public VideoClip TutorialStep2;
    public VideoClip TutorialStep3;
    public VideoClip TutorialStep4;
    public VideoClip TutorialStep5;
    public VideoClip TutorialStep6;

    [Header("튜토리얼 텍스트")]
    public Text MainExplainText;
    public Text MainStepText;
    public Text StepText;

    [Header("튜토리얼 과정")]
    public int TutorialNumber; //튜토리얼 번호
    private int TutorialStep; //튜토리얼에서 진행하는 과정
    public bool StillContinue; //다른 작업이 남아있을 경우, 그 작업이 종료되고 튜토리얼이 진행되도록 조취
    public bool isTutorialWindowOpen = false;

    [Header("튜토리얼 진행 버튼")]
    public GameObject OkMainButtonPrefab;
    public GameObject Ok2ButtonPrefab;
    public GameObject NextButtonPrefab;
    public GameObject PreviousButtonPrefab;
    public GameObject OkButtonPrefab;
    public GameObject Ok2SubButtonPrefab;
    public GameObject NextSubButtonPrefab;
    public GameObject PreviousSubButtonPrefab;
    public bool OkButtonClick = false;
    public RectTransform ScrollVerticalSize;

    [Header("기타 버튼")]
    public GameObject MenuButton;
    public GameObject AssetList;
    public GameObject FleetControlButton;
    public GameObject FleetBehaviorButton;
    public GameObject FleetFormationButton;
    public GameObject CameraButton;
    public GameObject UniverseMapButton;
    public GameObject FlagshipListButton;
    public GameObject HurricaneButton1;
    public GameObject HurricaneButton2;

    [Header("확인 버튼")]
    public GameObject FleetBehaviorGotIt;
    public GameObject CameraGotIt;
    public GameObject HurricaneControllerGotIt;
    public GameObject HurricaneFireIconGotIt;
    public GameObject CashListGotIt;
    public GameObject OtherGotIt;
    public GameObject MBCA79GotIt;

    [Header("튜토리얼 표시")]
    public GameObject ViewerPrefab;
    public GameObject UniverseMapViewGuidePrefab;
    public GameObject BehaviorViewGuidePrefab;
    public GameObject CameraViewGuidePrefab;
    public GameObject HurricaneOperationViewGuidePrefab;
    public GameObject HurricaneControllerViewGuidePrefab;
    public GameObject hurricaneFireIconViewGuidePrefab;
    public GameObject CashListViewGuidePrefab;
    public GameObject OtherViewGuidePrefab;
    public GameObject MBCA79ViewGuidePrefab;
    public GameObject FleetControlButtonImagePrefab;

    [Header("손가락 튜토리얼 표시")]
    public GameObject FingerPrefab;
    public GameObject FingerTutorialPrefab;

    [Header("튜토리얼 메뉴 표시")]
    public GameObject FlagshipGearViewer;
    public GameObject FlagshipGearGuide1;
    public GameObject FlagshipGearGuide2;
    public GameObject FlagshipGearFrame1;
    public GameObject FlagshipGearFrame2;

    [Header("사운드")]
    public AudioClip OkButtonAudio;

    private void Start()
    {
        //델타 허리케인 지상전 튜토리얼
        if (BattleSave.Save1.Tutorial == 1)
        {
            StartCoroutine(ShowController());
        }
    }

    IEnumerator ShowController()
    {
        yield return new WaitForSeconds(10);
        if (BattleSave.Save1.Tutorial == 1)
            HurricaneControllerViewGuidePrefab.SetActive(true);
    }

    public IEnumerator ShowMBCA79Instruction()
    {
        yield return new WaitForSeconds(1);
        MBCA79ViewGuidePrefab.SetActive(true);
    }

    //튜토리얼 창 진행과 종료 수순
    public void OKButtonClick()
    {
        if (TutorialNumber == 1)
        {
            if (TutorialStep == 1)
            {
                StartCoroutine(TutorialWindowClose());
                StartCoroutine(LiveCommunicationSystem.MainCommunication(1.00f));
            }
        }
        else if (TutorialNumber == 2)
        {
            if (TutorialStep == 1)
            {
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 3)
        {
            if (TutorialStep == 1)
            {
                PreviousButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 2)
            {
                TutorialStep++;
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 3)
            {
                TutorialStep++;
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 4)
            {
                TutorialStep++;
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 5)
            {
                Ok2ButtonPrefab.SetActive(true);
                NextButtonPrefab.SetActive(false);
                PreviousButtonPrefab.SetActive(true);
                TutorialStep++;
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 6)
            {
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
                FleetControlButton.GetComponent<Animator>().SetBool("Mark, Ship Mode Butten", false);
                FingerPrefab.SetActive(false);
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 4)
        {
            if (TutorialStep == 1)
            {
                Ok2SubButtonPrefab.SetActive(true);
                NextSubButtonPrefab.SetActive(false);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 2)
            {
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 5)
        {
            if (TutorialStep == 1)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(false);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 2)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 3)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 4)
            {
                Ok2SubButtonPrefab.SetActive(true);
                NextSubButtonPrefab.SetActive(false);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 5)
            {
                MenuButton.SetActive(true);
                AssetList.SetActive(true);
                CashListViewGuidePrefab.SetActive(true);

                //나머지 각각 튜토리얼은 진행하면서 자동으로 처리되도록 true로 전환
                BattleSave.Save1.PlanetTutorial = 0;
                BattleSave.Save1.FirstStart = false;
                BattleSave.Save1.FleetWeaponTutorial = true;
                BattleSave.Save1.FleetFormationTutorial = true;
                BattleSave.Save1.FlagshipManagementTutorial = true;
                BattleSave.Save1.LabTutorial = true;
                BattleSave.Save1.MBCA79Tutorial = true;
                DataSaveManager = FindObjectOfType<DataSaveManager>();
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 100)
        {
            if (TutorialStep == 1)
            {
                FlagshipGearGuide1.SetActive(true);
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 101)
        {
            if (TutorialStep == 1)
            {
                Ok2SubButtonPrefab.SetActive(true);
                NextSubButtonPrefab.SetActive(false);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 2)
            {
                BattleSave.Save1.FleetFormationTutorial = false;
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 102)
        {
            if (TutorialStep == 1)
            {
                Ok2SubButtonPrefab.SetActive(true);
                NextSubButtonPrefab.SetActive(false);
                PreviousSubButtonPrefab.SetActive(true);
                TutorialStep++;
                TutorialText(TutorialNumber);
            }
            else if (TutorialStep == 2)
            {
                BattleSave.Save1.FlagshipManagementTutorial = false;
                StartCoroutine(TutorialWindowClose());
            }
        }
        else if (TutorialNumber == 103)
        {
            if (TutorialStep == 1)
            {
                BattleSave.Save1.LabTutorial = false;
                StartCoroutine(TutorialWindowClose());
            }
        }
    }

    public void OKButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        OkButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        Ok2ButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        Ok2SubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        OkMainButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        NextButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        NextSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void OKButtonUp()
    {
        if (OkButtonClick == true)
        {
            OkButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            Ok2ButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            Ok2SubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            OkMainButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            NextButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            NextSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void OKButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            OkButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            Ok2ButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            Ok2SubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            OkMainButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            NextButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            NextSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void OKButtonExit()
    {
        if (OkButtonClick == true)
        {
            OkButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            Ok2ButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            Ok2SubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            OkMainButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            NextButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            NextSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //이전 버튼을 통해 이전으로 돌아가기
    public void PreviousButtonClick()
    {
        if (TutorialNumber == 3)
        {
            if (TutorialStep == 2)
            {
                FleetControlButtonImagePrefab.SetActive(true);
                TutorialVideoPlayer.clip = null;
                TutorialVideoPlayer.enabled = false;
                FingerPrefab.SetActive(false);

                OkButtonPrefab.SetActive(false);
                NextButtonPrefab.SetActive(true);
                PreviousButtonPrefab.SetActive(false);
            }
            else if (TutorialStep == 6)
            {
                OkButtonPrefab.SetActive(false);
                NextButtonPrefab.SetActive(true);
                PreviousButtonPrefab.SetActive(true);
            }
            FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 0);
            TutorialVideoPlayer.enabled = false;
            TutorialStep--;
            TutorialText(TutorialNumber);
        }
        else if (TutorialNumber == 4)
        {
            if (TutorialStep == 2)
            {
                StillContinue = true;
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(false);
            }
            TutorialStep--;
            TutorialText(TutorialNumber);
        }
        else if (TutorialNumber == 5)
        {
            if (TutorialStep == 3)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(false);
            }
            else if (TutorialStep == 4)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(true);
            }
            else if(TutorialStep == 5)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(true);
            }
            TutorialStep--;
            TutorialText(TutorialNumber);
        }
        else if (TutorialNumber == 101)
        {
            if (TutorialStep == 2)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(false);
            }
            TutorialStep--;
            TutorialText(TutorialNumber);
        }
        else if (TutorialNumber == 102)
        {
            if (TutorialStep == 2)
            {
                Ok2SubButtonPrefab.SetActive(false);
                NextSubButtonPrefab.SetActive(true);
                PreviousSubButtonPrefab.SetActive(false);
            }
            TutorialStep--;
            TutorialText(TutorialNumber);
        }
    }

    public void PreviousButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        PreviousButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        PreviousSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void PreviousButtonUp()
    {
        if (OkButtonClick == true)
        {
            PreviousButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            PreviousSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void PreviousButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            PreviousButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            PreviousSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void PreviousButtonExit()
    {
        if (OkButtonClick == true)
        {
            PreviousButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            PreviousSubButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(행동 모드) ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void GotItButtonClick()
    {
        BehaviorViewGuidePrefab.SetActive(false);
        FleetBehaviorButton.GetComponent<Animator>().SetBool("Mark, Behavior Butten", false);
    }

    public void GotItButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        FleetBehaviorGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotItButtonUp()
    {
        if (OkButtonClick == true)
        {
            FleetBehaviorGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotItButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            FleetBehaviorGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotItButtonExit()
    {
        if (OkButtonClick == true)
        {
            FleetBehaviorGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(카메라 버튼)
    public void GotIt2ButtonClick()
    {
        CameraViewGuidePrefab.SetActive(false);
        CameraButton.GetComponent<Animator>().SetBool("Mark, Camera", false);
    }

    public void GotIt2ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt2ButtonUp()
    {
        if (OkButtonClick == true)
        {
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt2ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt2ButtonExit()
    {
        if (OkButtonClick == true)
        {
            CameraGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(허리케인 조작법)
    public void GotIt3ButtonClick()
    {
        HurricaneControllerViewGuidePrefab.SetActive(false);
        hurricaneFireIconViewGuidePrefab.SetActive(true);
    }

    public void GotIt3ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        HurricaneControllerGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt3ButtonUp()
    {
        if (OkButtonClick == true)
        {
            HurricaneControllerGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt3ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            HurricaneControllerGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt3ButtonExit()
    {
        if (OkButtonClick == true)
        {
            HurricaneControllerGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(허리케인 사격 아이콘)
    public void GotIt4ButtonClick()
    {
        hurricaneFireIconViewGuidePrefab.SetActive(false);
    }

    public void GotIt4ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        HurricaneFireIconGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt4ButtonUp()
    {
        if (OkButtonClick == true)
        {
            HurricaneFireIconGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt4ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            HurricaneFireIconGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt4ButtonExit()
    {
        if (OkButtonClick == true)
        {
            HurricaneFireIconGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(현금 리스트)
    public void GotIt5ButtonClick()
    {
        CashListViewGuidePrefab.SetActive(false);
        OtherViewGuidePrefab.SetActive(true);
        FleetFormationButton.SetActive(true);
        FlagshipListButton.SetActive(true);
    }

    public void GotIt5ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        CashListGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt5ButtonUp()
    {
        if (OkButtonClick == true)
        {
            CashListGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt5ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            CashListGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt5ButtonExit()
    {
        if (OkButtonClick == true)
        {
            CashListGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(기타)
    public void GotIt6ButtonClick()
    {
        OtherViewGuidePrefab.SetActive(false);
    }

    public void GotIt6ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        OtherGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt6ButtonUp()
    {
        if (OkButtonClick == true)
        {
            OtherGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt6ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            OtherGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt6ButtonExit()
    {
        if (OkButtonClick == true)
        {
            OtherGotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //확인 버튼을 눌러서 튜토리얼 화면을 끈다.(MBCA-79)
    public void GotIt7ButtonClick()
    {
        BattleSave.Save1.MBCA79Tutorial = false;
        BattleSave.Save1.Tutorial = 0;
        MBCA79ViewGuidePrefab.SetActive(false);
    }

    public void GotIt7ButtonDown()
    {
        OkButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
        MBCA79GotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void GotIt7ButtonUp()
    {
        if (OkButtonClick == true)
        {
            MBCA79GotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        OkButtonClick = false;
    }
    public void GotIt7ButtonEnter()
    {
        if (OkButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OkButtonAudio);
            MBCA79GotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void GotIt7ButtonExit()
    {
        if (OkButtonClick == true)
        {
            MBCA79GotIt.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //함대 교전 조작법(1번)
    public void FleetMovingTutorial()
    {
        //첫 튜토리얼이므로 각 버튼들을 모두 숨긴다.
        MenuButton.SetActive(false);
        AssetList.SetActive(false);
        UniverseMapButton.SetActive(false);
        FleetBehaviorButton.SetActive(false);
        CameraButton.SetActive(false);
        FleetControlButton.SetActive(false);
        HurricaneButton1.SetActive(false);
        HurricaneButton2.SetActive(false);
        FleetFormationButton.SetActive(false);
        FlagshipListButton.SetActive(false);
        RandomSiteBattleTutorial.enabled = true;
        RandomSiteBattleTutorial.Tutorial = true;
        RandomSiteBattle.enabled = false;

        HurricaneOperationMenu.Tutorial = true;
        StartCoroutine(TutorialWindowOpen(1)); //가장 처음 시작할 때
    }

    //우주맵 처음 띄우기(2-1번)
    public void PopUpUniverseMapButton()
    {
        UniverseMapButton.SetActive(true);
        UniverseMapButton.GetComponent<Animator>().SetBool("Mark, Universe Map Butten", true);
        UniverseMapSystem.Tutorial = true;
        UniverseMapViewGuidePrefab.SetActive(true);
        UniverseMapSystem.TutorialMapStep = 1;
        UniverseMapSystem.TutorialOnce = true;
    }

    //행동 모드 띄우기(2-2번)
    public void PopUpBehaviorButton()
    {
        UniverseMapSystem.FirstBattle = true;
        StartCoroutine(BehaviorButtonStart());
    }

    //행동 모드 버튼 활성화(2-3번)
    IEnumerator BehaviorButtonStart()
    {
        yield return new WaitForSeconds(3);
        CameraViewGuidePrefab.SetActive(true);
        CameraButton.SetActive(true);
        CameraButton.GetComponent<Animator>().SetBool("Mark, Camera", true);
        CameraViewGuidePrefab.SetActive(true);

        yield return new WaitForSeconds(7);
        FleetBehaviorButton.SetActive(true);
        FleetBehaviorButton.GetComponent<Animator>().SetBool("Mark, Behavior Butten", true);
        BehaviorViewGuidePrefab.SetActive(true);
    }

    //튜토리얼 전용 전투 지역에서 승리할 때 신호(2-4번)
    public void VictoryBattleSiteTutorial()
    {
        RandomSiteBattleTutorial.enabled = false;
        UniverseMapSystem.FirstBattle = false;
    }

    //함대 조종 모드 조작법(3번)
    public IEnumerator FleetControlTutorial()
    {
        yield return new WaitForSeconds(3);
        FleetControlButton.SetActive(true);
        FleetControlButton.GetComponent<Animator>().SetBool("Mark, Ship Mode Butten", true);
        StartCoroutine(TutorialWindowOpen(3));
    }

    //첫 튜토리얼 완료
    public void FirstTutorialFinish()
    {
        StartCoroutine(TutorialWindowOpen(5));
    }

    //튜토리얼 창 열기
    public IEnumerator TutorialWindowOpen(int number)
    {
        Time.timeScale = 0;
        TutorialWindowPrefab.SetActive(true);
        TutorialStep = 1;

        yield return new WaitForSecondsRealtime(0.4f);

        if (number == 1) //메인 화면 전용의 텍스트 읽기용(큰 창)
        {
            OkMainButtonPrefab.SetActive(true);
            Ok2ButtonPrefab.SetActive(false);
            NextButtonPrefab.SetActive(false);
            PreviousButtonPrefab.SetActive(false);
        }
        else if (number == 3) //메인 화면 전용의 튜토리얼 여러 페이지(큰 창)
        {
            OkMainButtonPrefab.SetActive(false);
            Ok2ButtonPrefab.SetActive(false);
            NextButtonPrefab.SetActive(true);
            PreviousButtonPrefab.SetActive(false);
        }
        else if (number == 2 || number == 100 || number == 103) //보조 화면 전용의 튜토리얼 단일 페이지(작은 창)
        {
            OkButtonPrefab.SetActive(true);
            Ok2SubButtonPrefab.SetActive(false);
            NextSubButtonPrefab.SetActive(false);
            PreviousSubButtonPrefab.SetActive(false);
        }
        else if (number == 4 || number == 5 || number == 101 || number == 102 || number == 104) //보조 화면 전용의 튜토리얼 여러 페이지(작은 창)
        {
            OkButtonPrefab.SetActive(false);
            Ok2SubButtonPrefab.SetActive(false);
            NextSubButtonPrefab.SetActive(true);
            PreviousSubButtonPrefab.SetActive(false);
        }
        TutorialText(number);
    }

    //튜토리얼 창 닫기
    public IEnumerator TutorialWindowClose()
    {
        MessageWindowPrefab.SetActive(false);
        TextTutorialWindowPrefab.SetActive(false);
        MainViewer.SetActive(false);
        ImagePrefab.SetActive(false);
        MainExplainText.text = string.Format("");
        StepText.text = string.Format("");
        TutorialVideoPlayer.enabled = false;
        ScrollVerticalSize.sizeDelta = new Vector2(0, 0);

        yield return new WaitForSecondsRealtime(0.4f);
        TutorialWindowPrefab.SetActive(false);
        OkMainButtonPrefab.SetActive(false);
        OkButtonPrefab.SetActive(false);
        Ok2ButtonPrefab.SetActive(false);
        NextButtonPrefab.SetActive(false);
        PreviousButtonPrefab.SetActive(false);
        Ok2SubButtonPrefab.SetActive(false);
        NextSubButtonPrefab.SetActive(false);
        PreviousSubButtonPrefab.SetActive(false);
        isTutorialWindowOpen = false;
        Time.timeScale = 1;
    }

    //첫 보병전 임무를 취소하고 돌아왔을 때, 해당 튜토리얼 진행상태를 유지
    public void BackToGlessia()
    {
        MenuButton.SetActive(false);
        AssetList.SetActive(false);
        UniverseMapButton.SetActive(true);
        FleetBehaviorButton.SetActive(true);
        CameraButton.SetActive(true);
        FleetControlButton.SetActive(true);
        HurricaneButton1.SetActive(true);
        HurricaneButton2.SetActive(true);
        FleetFormationButton.SetActive(false);
        FlagshipListButton.SetActive(false);

        HurricaneOperationMenu.FirstStart = true;
        UniverseMapSystem.Tutorial = true;
        UniverseMapSystem.TutorialMapStep = 2;
        BattleSave.Save1.Tutorial = 0;
    }

    //내용물 개시
    void TutorialText(int number)
    {
        isTutorialWindowOpen = true;
        if (number == 1) //가장 처음 시작할 때
        {
            TutorialNumber = number;
            MessageWindowPrefab.SetActive(true);
            OkMainButtonPrefab.SetActive(true);
            if (WordPrintSystem.LanguageType == 1)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(0, 650);
                MainExplainText.text = string.Format("May the Constellation time pass by.\n\nGreeting, Gentlemen of the Nariha Human Union and Gentlemen of the United Command Center for Interstellar Space.\nI'm Benedict Archi who taked office of UCCIS chief fleet commander and It is my honor to send this message to you all as I take office fleets of the UCCIS and NHU.\nToday, We know the grand scale invasion again by the Contros in the 15years of war. They are starting to occupy again our many planets, and this is a problem that is a survival of our NHU. The mission is take back the lost home of you all. and these are the occupied planets. We will engage counterattack to the Contros, and will drive them out again in our territory without giving an inch.\n\nAlright, now I will connect the UCCIS command-center console device and will give order to you all that is at the fleets in live. As you know, It is my honor to be able to give traditional orders, and since you will feel your lives depend on my order, I will give them carefully.\n\nNow, based on current year 9072 in the Aloun calendar, We will begin an operation regain the planets.");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(0, 650);
                MainExplainText.text = string.Format("별자리 시간이 흐르길.\n\n반갑다, 나리하 인류연합 제군들, 그리고 성간우주사령연합 제군들. \n나는 이번 성간우주사령연합 함대 총사령관으로 위임된 베네딕트 아르키이며, 성간우주사령연합과 나리하 인류연합 함대를 통합적으로 통솔하게 되면서 제군들에게 이 메시지를 전송하게 되어 영광을 알린다.\n오늘, 우리는 컨트로스 종족과의 15년차 전쟁에서 또 다시 그들의 대규모 공습을 받고 있음을 잘 알고 있을 것이다. 그들은 우리의 수 많은 행성들을 또 다시 점령하기 시작했으며, 이번 사건은 우리 나리하 인류연합의 존망이 걸린 문제가 될 것이다. 임무는 잃어버린 제군들의 보금자리를 되찾는 것이다. 우리 나리하 인류연합의 점령당한 행성들이지. 그리고 컨트로스에게 반격을 개시하여, 놈들을 다시 우리의 땅에 단 한뼘도 내주지 않고 몰아낼 것이다.\n\n좋다, 제군들. 그럼 지금부터 내가 직접 이 성간우주사령연합 사령부 콘솔 기기에 접속하여 지금 함대에 있는 제군들에게 실시간으로 명령을 내릴 것이다. 자네들도 알겠지만, 이런 전통적인 명령을 내릴 수 있게되어 매우 영광이며, 제군들의 목숨이 나의 명령에 달려있음을 느낄 것이니 나 또한 매우 신중하게 명령을 내릴 것이다.\n\n그럼 현재 아로운력 9072년 시간 기준으로 행성 탈환 작전을 개시한다.");
            }
        }
        else if (number == 2) //우주맵 튜토리얼
        {
            UniverseMapButton.GetComponent<Animator>().SetBool("Mark, Universe Map Butten", false);
            TutorialNumber = number;
            MainViewer.SetActive(false);
            TextTutorialWindowPrefab.SetActive(true);
            UniverseMapViewGuidePrefab.SetActive(false);
            if (WordPrintSystem.LanguageType == 1)
                StepText.text = string.Format("This Tactical Map of Interstellar Fleet guide the NHU flagship to fly to a designated location using warp navigation. First, you must select a destination, and then select your flagship, the selected this will fly to the destination. Once you give flagship a order to warp, the flagship will engage warp voyage quickly with their ships.");
            else if (WordPrintSystem.LanguageType == 2)
                StepText.text = string.Format("이 성간함대 전술지도는 나리하 인류연합의 기함이 정해진 위치를 향해 워프항법으로 날아갈 수 있도록 유도합니다. 먼저 목적지를 선택해야 하며, 그 후에 원하는 기함을 선택하면 목적지를 향해 선택된 기함이 날아갈 수 있습니다. 기함에게 한번 워프항법 명령을 내리면, 기함은 즉시 자신의 함대와 함께 빠른 속도로 워프항해를 실시할 것입니다.");

        }
        else if (number == 3) //함대 조종
        {
            TutorialNumber = number;
            MainViewer.SetActive(true);
            MessageWindowPrefab.SetActive(true);
            StillContinue = true;
            ImagePrefab.GetComponent<RawImage>().texture = VideoRenderer;
            if (TutorialStep == 1)
            {
                ImagePrefab.SetActive(false);
                FleetControlButtonImagePrefab.SetActive(true);
                TutorialVideoPlayer.clip = null;
                TutorialVideoPlayer.enabled = false;
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("This fleet control button is used for precise control of your fleets. You can switch between flagship mode of fleet mode with click the button, and fleet control depends on the mode.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("이 함대 조종 버튼은 함대의 정밀한 제어를 위해 사용됩니다. 버튼 클릭을 통해 기함 모드나 함대 모드로 전환이 가능하며, 해당 모드에 따라 함대 조종이 달라집니다.");
            }
            else if (TutorialStep == 2)
            {
                ImagePrefab.SetActive(true);
                FleetControlButtonImagePrefab.SetActive(false);
                FingerPrefab.SetActive(true);
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 4);
                TutorialVideoPlayer.clip = TutorialStep1;
                TutorialVideoPlayer.enabled = true;
                TutorialVideoPlayer.Play();
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("When the flagship mode is activated, ships will follow to their flagship. This mode is very easy order system to move your fleets, and you can move fleet with just one touch.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("기함 모드가 실시되면, 기함을 중심으로 함대가 함께 움직입니다. 함대를 움직이는 가장 쉬운 명령체계이며, 터치 한 번으로 함대를 움직이는 것이 가능합니다.");
            }
            else if (TutorialStep == 3)
            {
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 1);
                TutorialVideoPlayer.clip = TutorialStep2;
                TutorialVideoPlayer.enabled = true;
                TutorialVideoPlayer.Play();
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("When the fleet mode is activated, you can give orders to each ship, including the flagship. This mode can attack or move precisely.\nPush the ship you want and swipe to the destination screen, the ship will move to that area.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("함대 모드가 실시되면, 해당 기함을 포함한 함선에게 각각 명령을 내릴 수 있습니다. 이 모드는 매우 정밀한 공격이나 기동이 가능합니다.\n조종하고자 하는 함선을 누르고 목적지 화면을 향해 스와이프 하시면 함선이 해당 지역으로 이동합니다.");
            }
            else if (TutorialStep == 4)
            {
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 2);
                TutorialVideoPlayer.clip = TutorialStep3;
                TutorialVideoPlayer.enabled = true;
                TutorialVideoPlayer.Play();
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("If you quickly tab the screen twice, multiple ships will be selected within the specified range, and Swipe to the destination, the selected ships will move. If you left your finger off the screen after double tab, the selection will cancel.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("두 번 연속으로 빠르게 화면을 누르면 지정된 범위에서 여러 함선들이 선택되며, 목적지를 향해 스와이프 하면, 선택된 함선들이 움직일 것입니다. 두 번 눌렀을 때 손가락을 화면에서 땐다면, 선택이 취소될 것입니다.");
            }
            else if (TutorialStep == 5)
            {
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 3);
                TutorialVideoPlayer.clip = TutorialStep4;
                TutorialVideoPlayer.enabled = true;
                TutorialVideoPlayer.Play();
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("If you want select the ships of same type, tab the ship you selected for a moment. All ships of same type within camera range will be selected automatically.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("만약 같은 유형의 함선들만 선택하고 싶다면, 해당 함선을 잠시 일정시간동안 누르십시오. 자동으로 카메라 반경내에 존재하는 같은 유형의 모든 함선들이 선택될 것입니다.");
            }
            else if (TutorialStep == 6)
            {
                FingerTutorialPrefab.GetComponent<Animator>().SetFloat("Finger 1, Tutorial finger", 1);
                TutorialVideoPlayer.clip = TutorialStep5;
                TutorialVideoPlayer.enabled = true;
                TutorialVideoPlayer.Play();
                if (WordPrintSystem.LanguageType == 1)
                    MainStepText.text = string.Format("Selected ship can be commanded not only to move, but also to attack the enemy target. It can protect the Nariha fleets at strategic situations, and is also possible to focus only the enemy flagship.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainStepText.text = string.Format("선택된 함선은 이동 뿐만 아니라, 특정 대상을 공격할 수 있도록 명령을 내릴 수 있습니다. 전략적인 상황에서 나리하를 보호할 수 있으며, 적 기함만 집중 타격하는 것도 가능합니다.");
                StartCoroutine(LiveCommunicationSystem.MainCommunication(1.03f));
            }
        }
        else if (number == 4 && StillContinue == true) //허리케인 모드 튜토리얼
        {
            TutorialNumber = number;
            HurricaneOperationViewGuidePrefab.SetActive(false);
            MainViewer.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (TutorialStep == 1)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("This Delta Hurricane Operation Table engage the liberation operation as sending the operations commander, Delta Hurricane. If the flagship you are controlling is at the planet, It will bring this planet's ground warfare informations. If you win the ground warfare through Delta Hurricane and complete all ground missions on the planet, the Contros forces will retreat and the planet will regain its freedom.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("이 델타 허리케인 작전 및 행성 관리 테이블은 델타 허리케인 작전 수행 사령관을 행성에 직접 파견하여 해방 작전을 실시합니다. 조종 중인 기함이 위치한 행성에 있다면, 해당 행성의 지상전 정보를 가져올 것입니다. 델타 허리케인 작전을 통해 지상전에서 승리를 하여 행성의 모든 지상전 임무를 완수한다면 컨트로스 병력은 물러날 것이며, 행성은 자유를 되찾을 것입니다.");
            }
            else if (TutorialStep == 2)
            {
                StillContinue = false;
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("If the flagship you are controlling approaches within a certain distance from an enemy flagship, You can also deploy the Delta Hurricane and can start a operation that can make cannon down state of the enemy flagship. This operation will have a devastating effect on the Contros fleet, and will provide a more favorable situation for the Nariha fleet.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("조종 중인 기함이 적 기함으로부터 일정거리 이하내로 접근했을 경우, 델타 허리케인을 투입시켜 적 기함의 함포를 무력화하는 작전을 수행할 수 있습니다. 이 작전은 컨트로스 함대에 치명적인 효과를 줄 것이며, 나리하 함대에게 좀 더 유리한 상황을 제공할 것입니다.");
            }
        }
        else if (number == 5) //행성 수복 이후, 본격적인 시작
        {
            TutorialNumber = number;
            if (TutorialStep == 1)
            {
                OkMainButtonPrefab.SetActive(true);
                ScrollVerticalSize.sizeDelta = new Vector2(0, 500);
                MessageWindowPrefab.SetActive(true);
                UniverseMapSystem.TutorialMapStep = 0;
                UniverseMapSystem.Tutorial = false;
                if (WordPrintSystem.LanguageType == 1)
                    MainExplainText.text = string.Format("Well done, soliders. The liberation of out first planet means that our NHU still stands strong.\n\nThis is a restoration of territory, but it will be a way of great voyage towards victory for our NHU. Even as we move, there are still many planets occupied by Contros, and our citizens are waiting for our NHU's constellation. Soliders, the here where we stand is a place to see that the Contros expand their fleets, but it symbolizes that we can not avoid to conflict them soon.\n\nSo, don't be afraid! Our march is the Nariha's march, and our fire is a voice of freedom, so, go forth, take up arms, and we will liberate our planets.");
                else if (WordPrintSystem.LanguageType == 2)
                    MainExplainText.text = string.Format("수고했다, 제군들. 우리의 첫 행성이 해방이 이루어진 것은 우리 나리하 인류연합은 여전히 굳건히 건재함을 의미한다.\n\n이 행성의 해방은 하나의 영토 회복이지만, 우리 나리하 인류연합의 승전을 향하는 큰 항해의 길이 될 것이다. 우리가 지금 이렇게 움직이고 있는 상황에서도 컨트로스에게 점령된 행성은 여전히 많으며, 시민들은 우리 나리하의 별자리를 기다리고 있다. 제군들, 우리가 서 있는 이곳은 컨트로스가 점점 자신의 함대를 확대하고 있는 모습을 바라보는 곳이지만, 이제 곧 그들을 마주하는 것을 피할 수 없는 것을 상징한다.\n\n그러니 두려워말라! 우리의 전진은 나리하의 전진이며, 우리의 발포는 자유의 외침이니, 나아가라, 무장하라, 그리고 우리의 행성을 해방할 것이다.");
            }
            else if (TutorialStep == 2)
            {
                OkMainButtonPrefab.SetActive(false);
                MainViewer.SetActive(true);
                ImagePrefab.SetActive(true);
                MessageWindowPrefab.SetActive(false);
                TextTutorialWindowPrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("Now you can use the menu of UCCIS. It can use for battle operation with this menu. As the new ship deploy makes fleet stronger, the new flagship deploy can expand number of fleet. As research can makes weapon stronger, and the Delta Hurricane will be able to operate more safely.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("이제 성간우주사령연합의 메뉴 사용이 허가되었습니다. 이 메뉴를 통해 전투 작전에 활용이 가능합니다. 함선 배치를 통해 기함을 따르는 함대를 더 강력하게 만들고, 기함을 새로 배치하여 함대를 늘리실 수 있습니다. 연구를 통해, 무장을 더 강력하게 만들 수 있으며, 델타 허리케인이 더 안전한 작전 수행이 가능할 것입니다.");
            }
            else if (TutorialStep == 3)
            {
                MainViewer.SetActive(true);
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("The more planets you liberate, the more support UCCIS wiil provide. If you liberate the research planets, you can receive more powerful ships and weapons, or conduct more powerful research.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("행성을 해방할 수록 성간우주사령연합은 더 많은 것을 지원할 것입니다. 연구 행성을 해방하면 더 강력한 함선이나 무기를 지원받을 수 있으며, 혹은 더 강력한 연구가 가능합니다.");
            }
            else if (TutorialStep == 4)
            {
                MainViewer.SetActive(true);
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("As liberate the mining planets, you can receive the assets of Glopaoros and Construction Resource in live. All the liberated mining planets can adjust the amount of resource acquisition through the Delta Hurricane Operation Table button.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("채광 행성을 해방하여, 글로파오로스 자산과 건설 자원을 실시간으로 획득할 수 있습니다. 모든 해방된 자원 행성은 델타 허리케인 작전을 통해 자원 획득량을 조절할 수 있습니다.");
            }
            else if (TutorialStep == 5)
            {
                MainViewer.SetActive(true);
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("you can receive the Taritronic that is used for research through the operation of Delta Hurricane. Proceed more powerful research with this Taritronic.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("델타 허리케인의 작전을 통해 연구에 사용되는 타리트로닉 자원을 획득할 수 있습니다. 이 자원을 통해 더 강력한 연구를 진행하십시오.");
            }
        }
        else if (number == 100) //함대 장비 메뉴
        {
            TutorialNumber = number;
            ImagePrefab.SetActive(true);
            MainViewer.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (WordPrintSystem.LanguageType == 1)
                StepText.text = string.Format("Through this Fleet weapon menu, you can change cannon weapons of each ship in live. you can consider the status of the enemy fleet, change the fleet's weapon, and you can attempt  counterattack to the Contros. If you select a ship, the using weapon slot will appear, and drag a weapon icon and drop it into a slot to equip the weapon.");
            else if (WordPrintSystem.LanguageType == 2)
                StepText.text = string.Format("이 함대 무기 메뉴를 통해 각 함선의 함포를 실시간으로 변경할 수 있습니다. 적 함대의 상태를 고려하여, 함대의 무장을 변경하시고, 그들에게 반격을 시도할 수 있습니다. 함선을 선택하면 사용 중인 무기 슬롯이 나타나며, 무기 아이콘을 드래그하여 슬롯에 드롭하면 무기가 장착됩니다.");
        }
        else if (number == 101) //함대 관리 메뉴
        {
            TutorialNumber = number;
            MainViewer.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (TutorialStep == 1)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("This Fleet Management menu manage states of your all fleets. In the tab of the Formation, you can deploy more ships as you add the number of total formation, and you can redeploy your ship to other fleet through the Ship redeployment.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("이 함대 관리 메뉴는 자신의 모든 함대의 상태를 관리합니다. 편대 탭에서는 총 편대수를 확장하면 더 많은 함선을 배치할 수 있으며, 함선 재배치를 통해 함선을 다른 함대로 재배치를 할 수 있습니다.");
            }
            else if (TutorialStep == 2)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("through the Production tab, you can deploy more ships. there are ships that can perform strategic missions, from the basic Formation ship to Tactical ship that has special mission capabilities.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("생산 탭을 통해 더 많은 함선을 배치할 수 있습니다. 가장 기본적인 함선인 편대함부터 특수한 임무 능력을 가진 전략함까지 전략적인 임무를 수행할 수 있는 함선이 준비되어 있습니다.");
            }
        }
        else if (number == 102) //기함 메뉴
        {
            TutorialNumber = number;
            MainViewer.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (TutorialStep == 1)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("You can deploy a new flagship in this Flagship Management menu. Also, the flagship can use their own skill, and there are skill slots at each flagship. It is possible to use powerful attack of fleet as the slot add. Drag a skill icon and drop it into a unlocked slot to equip the skill.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("기함 관리 메뉴에서 새로운 기함을 배치할 수 있습니다. 또한, 기함이 주도하는 특수 스킬을 사용할 수 있으며, 각 기함마다 스킬 슬롯이 존재합니다. 스킬 슬롯이 늘어날 수록 더 강력한 함대의 공격이 가능합니다. 스킬 아이콘을 드레그하여 잠금해제된 슬롯에 드롭하면 스킬이 장착됩니다.");
            }
            else if (TutorialStep == 2)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("The skill of the Flagship strike tab attack the eneny by firing weapon from only flagship, and attack effect is not affected by the number of ships. The skill of the Fleet strike tab attack the eneny by firing weapon from flagship and their ships, but attack effect will affect by the number of ships.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("기함 공격의 스킬은 기함에서만 무기를 발사하여 적 함대를 타격하며, 함대수에 공격 효과가 영향을 받지 않습니다. 함대 공격의 스킬은 아군 함선과 함께 적 함대를 향해 무기를 발사하여 타격을 시도하지만 함대의 수에 따라 공격 효과가 영향을 받을 것입니다.");
            }
        }
        else if (number == 103) //전쟁 연구 메뉴
        {
            TutorialNumber = number;
            ImagePrefab.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (WordPrintSystem.LanguageType == 1)
                StepText.text = string.Format("In the Warfare Research menu, you can enforce research about fleet and the Delta Hurricane. Since all research is conducted by materials from research planets, it can only be conducted if the research planet is liberated. Also, Taritronic is essential by all research.");
            else if (WordPrintSystem.LanguageType == 2)
                StepText.text = string.Format("전쟁 연구 메뉴에서 함대와 델타 허리케인에 관련된 무장 시스템을 연구하여 더 강화가 가능합니다. 모든 연구는 연구 행성으로부터 물자를 조달하여 이루어지기 때문에, 반드시 연구 행성이 해방되어야 연구 수행이 가능합니다. 또한, 연구 진행시 타리트로닉 광물이 필수적으로 사용됩니다.");
        }
        else if (number == 10) //전투 로봇 조작법
        {
            TutorialNumber = number;
            MainViewer.SetActive(true);
            TextTutorialWindowPrefab.SetActive(true);
            if (TutorialStep == 1)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("This battle robot, MBCA-79 Iron Hurricane is the Multipurpose Bipedalism Combat Armor designed to use heavy firepower. It can fast move using booster, and can use multiple weapons at once.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("이 MBCA-79 강철 허리케인 전투 로봇은 강력한 화력을 사용할 수 있도록 고안된 다목적 이족 보행 전투 장갑입니다. 부스터가 탑재되어 뛰어난 기동성이 가능하며, 한꺼번에 여러 무기를 동시에 사용할 수 있습니다.");
            }
            else if (TutorialStep == 2)
            {
                ImagePrefab.SetActive(true);
                if (WordPrintSystem.LanguageType == 1)
                    StepText.text = string.Format("If you activate the auto cannon, this cannon will detect enemies automatically and will fire. The two weapons fire by Attack controller, and you can turn this weapons on or off. If both this weapon is activated, both this will fire by Attack controller.");
                else if (WordPrintSystem.LanguageType == 2)
                    StepText.text = string.Format("오토 캐논을 활성화하면, 보이는 적을 자동으로 포착하여 사격할 것입니다. 무장된 2개의 무기는 모두 사격 컨트롤러를 통해 사격이 이루어지며, 활성화/비활성화를 할 수 있습니다. 둘 다 활성화 되어 있을 경우, 사격 컨트롤러를 통해 동시에 사격이 이루어집니다.");
            }
        }
    }
}