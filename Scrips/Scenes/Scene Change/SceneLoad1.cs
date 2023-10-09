using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoad1 : MonoBehaviour
{
    [Header("스크립트")]
    public BattleMessages BattleMessages;
    public MessagePrintBattle MessagePrintBattle;
    LoadDataManager LoadDataManager;

    [Header("진행")]
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 0.25f;
    public Slider progressbar;
    private bool ReadyToStart = false;

    [Header("로고")]
    public GameObject TitlePrefab;
    public GameObject CameraAnime;
    public GameObject LogoPrefab;

    [Header("시작")]
    public Image ButtonBackground;
    public GameObject StartText;
    public Text loadtext;
    public string Map;
    public bool ExitGame; //게임 나가기

    [Header("부팅 이펙트")]
    public GameObject BootingMenuPrefab;
    public Text UCCISText1;
    public Text UCCISText2;
    public Text UCCISText3;
    public Text UCCISLoadingText1;
    public Text UCCISLoadingText2;
    public Text UCCISLoadingText3;

    [Header("크래딧")]
    public bool Clicked = false;
    public bool isCreditWindowOpned = false;
    public RectTransform CreditButtonRect;
    public GameObject CreditWindowPrefab;
    public GameObject CreditButtonPrefab;
    public GameObject FuenellaPrefab;
    public AudioClip ButtonUIAudio;

    [Header("로드 창 및 버튼")]
    public GameObject LoadWindowPrefab;

    [Header("사운드 믹서")]
    public AudioMixer mixer;
    public AudioMixer BeltScrolMixer;
    public AudioMixer BGMMixer;

    [Header("사운드")]
    public AudioSource MainBGMSource;
    public GameObject MainBGM;
    public GameObject MainBGMRepeat;

    //크래딧 버튼
    public void CreditButtonClick()
    {
        if (isCreditWindowOpned == false)
        {
            isCreditWindowOpned = true;
            CreditWindowPrefab.SetActive(true);
            FuenellaPrefab.GetComponent<Animator>().SetFloat("Action, Fuenella", 1);
            ButtonBackground.raycastTarget = false;
            StartText.SetActive(false);
        }
        else
        {
            FuenellaPrefab.GetComponent<Animator>().SetFloat("Action, Fuenella", 0);
            isCreditWindowOpned = false;
            CreditWindowPrefab.SetActive(false);
            ButtonBackground.raycastTarget = true;
            StartText.SetActive(true);
        }
    }
    public void CreditButtonDown()
    {
        Clicked = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        CreditButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CreditButtonUp()
    {
        if (Clicked == true)
        {
            CreditButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        Clicked = false;
    }
    public void CreditButtonEnter()
    {
        if (Clicked == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            CreditButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CreditButtonExit()
    {
        if (Clicked == true)
        {
            CreditButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    private void Awake()
    {
        //언어 자동 선택(시스템 설정에서 언어를 선택)
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                BattleSave.Save1.LanguageType = 1;
                break;
            case SystemLanguage.Korean:
                BattleSave.Save1.LanguageType = 2;
                break;
            default:
                BattleSave.Save1.LanguageType = 1;
                break;
        }

        Application.targetFrameRate = 60; //프레임 60으로 고정

        if (BattleSave.Save1.GoBackTitle == false)
        {
            CreditButtonRect.anchoredPosition = new Vector2(-1000, CreditButtonRect.anchoredPosition.y);
            CreditButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
            StartCoroutine(LogoStartAnimation());
        }
        else
        {
            CreditButtonRect.anchoredPosition = new Vector2(-1000, CreditButtonRect.anchoredPosition.y);
            CreditButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
            StartCoroutine(BackToTitleAnimation());
        }
    }

    private void Start()
    {
        StartCoroutine(GetOption());
    }

    IEnumerator GetOption()
    {
        yield return new WaitForSeconds(0.1f);
        BGMMixer.SetFloat("BGM sound", Mathf.Log(BattleSave.Save1.BGMSliderValue) * 20);
        mixer.SetFloat("Universe Sound Effect", Mathf.Log(BattleSave.Save1.UniverseSoundEffectSliderValue) * 20);
        BeltScrolMixer.SetFloat("Belt scroll Sound Effect", Mathf.Log(BattleSave.Save1.BSSoundEffectSliderValue) * 20);
    }

    //게임을 실행할 때 로고 시작
    IEnumerator LogoStartAnimation()
    {
        DataSaveManager.instance.NewGameDataSave();
        CameraAnime.GetComponent<Animator>().SetBool("Logo start", true);
        LogoPrefab.GetComponent<Animator>().SetBool("Logo start", true);
        yield return new WaitForSeconds(6);
        MainBGM.SetActive(true);
        LogoPrefab.GetComponent<Animator>().SetBool("Logo start", false);
        yield return new WaitForSeconds(2.5f);
        TitlePrefab.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CameraAnime.GetComponent<Animator>().SetBool("Logo start", false);
        StartText.SetActive(true);
        ButtonBackground.raycastTarget = true;
        ReadyToStart = true;
        yield return new WaitForSeconds(0.5f);
        CreditButtonRect.anchoredPosition = new Vector2(-797.4f, CreditButtonRect.anchoredPosition.y);
        CreditButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
    }

    //타이틀로 되돌아갔을 때에만 시작
    IEnumerator BackToTitleAnimation()
    {
        CameraAnime.GetComponent<Animator>().SetBool("Go back title start", true);
        MainBGM.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        TitlePrefab.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CameraAnime.GetComponent<Animator>().SetBool("Go back title start", false);
        StartText.SetActive(true);
        ButtonBackground.raycastTarget = true;
        ReadyToStart = true;
        yield return new WaitForSeconds(0.5f);
        CreditButtonRect.anchoredPosition = new Vector2(-797.4f, CreditButtonRect.anchoredPosition.y);
        CreditButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
    }

    //눌러서 게임 시작하기
    public void StartGame()
    {
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();
        StartText.SetActive(false);
        LoadWindowPrefab.SetActive(true);
        DataSaveManager.instance.enabled = true;
    }

    //부팅창 출력 이후, 게임 시작
    public IEnumerator MissionStart()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            UCCISText1.fontSize = 80;
            UCCISText2.fontSize = 80;
            UCCISText3.fontSize = 80;
            UCCISText1.text = string.Format("United Command Center for Interstellar Space");
            UCCISText2.text = string.Format("United Command Center for Interstellar Space");
            UCCISText3.text = string.Format("United Command Center for Interstellar Space");
            UCCISLoadingText1.text = string.Format("Connecting console device of command center...");
            UCCISLoadingText2.text = string.Format("Connecting console device of command center...");
            UCCISLoadingText3.text = string.Format("Connecting console device of command center...");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            UCCISText1.fontSize = 100;
            UCCISText2.fontSize = 100;
            UCCISText3.fontSize = 100;
            UCCISText1.text = string.Format("성간우주사령연합");
            UCCISText2.text = string.Format("성간우주사령연합");
            UCCISText3.text = string.Format("성간우주사령연합");
            UCCISLoadingText1.text = string.Format("사령부 콘솔 기기에 접속 중...");
            UCCISLoadingText2.text = string.Format("사령부 콘솔 기기에 접속 중...");
            UCCISLoadingText3.text = string.Format("사령부 콘솔 기기에 접속 중...");
        }
        BootingMenuPrefab.SetActive(true);
        BootingMenuPrefab.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 1);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadSceneAsync(Map);
    }

    void Update()
    {
        if (ReadyToStart == true && MainBGMSource.isPlaying == false)
        {
            MainBGM.SetActive(false);
            MainBGMRepeat.SetActive(true);
        }

        //타이틀 화면에서 우측 하단의 뒤로 가기 버튼을 눌러서 게임 나가기(안드로이드 전용)
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame = true;
                MessagePrintBattle.GameExit();
                StartCoroutine(BattleMessages.MessageStart(2, 0, 2));
            }
        }
    }
}
