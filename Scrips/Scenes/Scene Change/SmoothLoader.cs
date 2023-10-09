using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SmoothLoader : MonoBehaviour
{
    [Header("스크립트")]
    public BattleMessages BattleMessages;
    public CinemachineVirtualCamera CVCamera;

    [Header("로드 값")]
    private AsyncOperation loadOperation;
    private float currentValue;
    private float targetValue;

    [Header("로드 프로세스")]
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 0.25f;
    public Slider progressbar;
    public Text loadtext;
    public string Map;
    private bool StartLoad;

    [Header("부팅 이펙트")]
    public GameObject BootingMenuPrefab;
    public GameObject MenuWallPrefab; //화면전환용 프리팹
    public GameObject WallEffectUCCIS; //UCCIS메뉴 화면전환 애니메이션
    public Text UCCISText1;
    public Text UCCISText2;
    public Text UCCISText3;
    public Text UCCISLoadingText1;
    public Text UCCISLoadingText2;
    public Text UCCISLoadingText3;

    [Header("버튼")]
    public GameObject StartButtonPrefab;
    public GameObject BackButtonPrefab;
    public GameObject BackToUniverse;
    public bool CancelFleetClick;

    [Header("연출")]
    private bool StartMission = false;
    public GameObject MissionAirceaftPrefab; //수송기 연출
    public GameObject WeaponSelectionPrefab; //무기선택 창 연출

    [Header("무기창")]
    public GameObject HeavyWeaponSelectWindow; //중화기 무기창

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;

    public void StartButtonClick()
    {
        StartMission = true;
        StartCoroutine(MissionStart());
    }
    public void StartButtonDown()
    {
        CancelFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        StartButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void StartButtonUp()
    {
        if (CancelFleetClick == true)
        {
            StartButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelFleetClick = false;
    }
    public void StartButtonEnter()
    {
        if (CancelFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            StartButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void StartButtonExit()
    {
        if (CancelFleetClick == true)
        {
            StartButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //취소 후, 다시 메인메뉴 창으로 돌아가기
    public void CancelButtonClick()
    {
        StartCoroutine(BattleMessages.MessageStart(2, 0, 2));
    }
    public void CancelButtonDown()
    {
        CancelFleetClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        BackButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelButtonUp()
    {
        if (CancelFleetClick == true)
        {
            BackButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelFleetClick = false;
    }
    public void CancelButtonEnter()
    {
        if (CancelFleetClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            BackButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelButtonExit()
    {
        if (CancelFleetClick == true)
        {
            BackButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    private void Awake()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
        }
    }

    private void Start()
    {
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
            HeavyWeaponSelectWindow.SetActive(true);
        MapLoad();
        UCCISBootingPrint();
        StartCoroutine(MissionAirceaftStart());
    }

    IEnumerator MissionAirceaftStart()
    {
        int RandomFlight = Random.Range(0, 4);

        if (RandomFlight == 0)
            MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 1);
        else if (RandomFlight == 1)
            MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 2);
        else if (RandomFlight == 2)
            MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 3);
        else
            MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 4);
        yield return new WaitForSeconds(10);
        MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 100);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        progressbar.value = currentValue = targetValue = 0;
        var currentScene = SceneManager.GetActiveScene();
        loadOperation = SceneManager.LoadSceneAsync(Map);
        loadOperation.allowSceneActivation = false;
        StartLoad = true;

        StartButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().Direction = 0;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        MenuWallPrefab.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);
        BootingMenuPrefab.SetActive(false);

        while (!loadOperation.isDone)
        {
            yield return null;

            if (loadOperation.progress < 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            else if (loadOperation.progress >= 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);

            if (progressbar.value < 0.3f)
            {
                if (BattleSave.Save1.LanguageType == 1)
                    loadtext.text = "Activating warp of the Delta Strike Group's fleet...";
                else if (BattleSave.Save1.LanguageType == 2)
                    loadtext.text = "델타전단 함대 워프 활성화 중...";
            }
            else if (progressbar.value < 1 && progressbar.value >= 0.6f)
            {
                if (BattleSave.Save1.LanguageType == 1)
                    loadtext.text = "Departing a transport aircraft from the Delta Strike Group's fleet...";
                else if (BattleSave.Save1.LanguageType == 2)
                    loadtext.text = "수송기가 델타전단 함대로부터 출격 중...";
            }
            else if (progressbar.value >= 1)
                loadtext.text = "";

            if (progressbar.value >= 0.9f && loadOperation.progress >= 0.9f)
            {
                progressbar.gameObject.SetActive(false);
                if (StartMission == false)
                    StartButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
            }
        }
    }

    //부팅창 시작 출력
    void UCCISBootingPrint()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            UCCISText1.fontSize = 80;
            UCCISText2.fontSize = 80;
            UCCISText3.fontSize = 80;
            UCCISText1.text = string.Format("Delta Strike Group");
            UCCISText2.text = string.Format("Delta Strike Group");
            UCCISText3.text = string.Format("Delta Strike Group");
            UCCISLoadingText1.text = string.Format("Preparing Delta Hurricane operation...");
            UCCISLoadingText2.text = string.Format("Preparing Delta Hurricane operation...");
            UCCISLoadingText3.text = string.Format("Preparing Delta Hurricane operation...");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            UCCISText1.fontSize = 100;
            UCCISText2.fontSize = 100;
            UCCISText3.fontSize = 100;
            UCCISText1.text = string.Format("델타전단");
            UCCISText2.text = string.Format("델타전단");
            UCCISText3.text = string.Format("델타전단");
            UCCISLoadingText1.text = string.Format("델타 허리케인 작전 준비 중...");
            UCCISLoadingText2.text = string.Format("델타 허리케인 작전 준비 중...");
            UCCISLoadingText3.text = string.Format("델타 허리케인 작전 준비 중...");
        }
        StartCoroutine(LoadScene());
    }

    //미션 시작
    IEnumerator MissionStart()
    {
        CinemachineBasicMultiChannelPerlin MultiChannelPerlin = CVCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        MultiChannelPerlin.m_AmplitudeGain = 1;
        WeaponSelectionPrefab.GetComponent<Animator>().SetBool("Close window, Weapon Selection", true);
        StartButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        BackButtonPrefab.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 101);
        yield return new WaitForSeconds(3);
        MissionAirceaftPrefab.GetComponent<Animator>().SetFloat("Mission Start, HA-767", 0);
        MissionAirceaftPrefab.SetActive(false);
        UCCISText1.text = string.Format("");
        UCCISText2.text = string.Format("");
        UCCISText3.text = string.Format("");

        if (BattleSave.Save1.LanguageType == 1)
        {
            UCCISLoadingText1.text = string.Format("Activating warp of a transport aircraft into the operation area...");
            UCCISLoadingText2.text = string.Format("Activating warp of a transport aircraft into the operation area...");
            UCCISLoadingText3.text = string.Format("Activating warp of a transport aircraft into the operation area...");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            UCCISLoadingText1.text = string.Format("작전 지역으로 수송기 워프 활성화 중...");
            UCCISLoadingText2.text = string.Format("작전 지역으로 수송기 워프 활성화 중...");
            UCCISLoadingText3.text = string.Format("작전 지역으로 수송기 워프 활성화 중...");
        }
        BootingMenuPrefab.SetActive(true);
        BootingMenuPrefab.GetComponent<Animator>().SetFloat("Delta Hurricane Booting, UCCIS mark", 1);
        yield return new WaitForSecondsRealtime(0.5f);
        loadOperation.allowSceneActivation = true;
    }

    //맵 선택
    void MapLoad()
    {
        if (BattleSave.Save1.MissionArea == 1) //도시 지역1
            Map = "City Area1";
        else if (BattleSave.Save1.MissionArea == 2) //대기권 스테이션1
            Map = "Atmosphere station1";
        else if (BattleSave.Save1.MissionArea == 3) //시설 내부 통로1
            Map = "Facility Inside Aisle1";
        else if (BattleSave.Save1.MissionArea == 100) //슬로리어스 기함
            Map = "Ship Battle";
        else if (BattleSave.Save1.MissionArea == 101) //칸타크리 기함
            Map = "Ship Battle";
    }

    void Update()
    {
        if (StartLoad == true)
        {
            targetValue = loadOperation.progress / 0.9f;

            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            progressbar.value = currentValue;
        }
    }
}