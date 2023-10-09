using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDataManager : MonoBehaviour
{
    public WordPrintSystem WordPrintSystem;
    public SystemMessages SystemMessages;
    public WordPrintMenu WordPrintMenu;
    public BattleMessages BattleMessages;
    public MessagePrintBattle MessagePrintBattle;
    public UniverseMapSystem UniverseMapSystem;
    SceneLoad1 SceneLoad1;
    MainMenuButtonSystem MainMenuButtonSystem;

    public string DataPath; //저장 정보
    public string TableDataPath; //테이블 정보
    public int TableNumber;
    public bool SaveStart = false;
    public bool DeleteStart = false;
    public bool isSaveData = false; //세이브 메뉴인지에 대한 여부
    public AudioClip ButtonUIAudio;

    [Header("로드 창 및 버튼")]
    public GameObject CancelButtonPrefab;
    public GameObject LoadButtonPrefab;
    public Image LoadButtonImage;
    public Image LoadButtonImage2;
    public GameObject NewGameButtonPrefab;
    private bool ButtonClicked = false;
    private bool SceneLoadImmediately = true;
    public RectTransform ScrollVerticalSize;

    public SaveTablePrefab[] saveTablePrefab; //데이터 테이블

    [System.Serializable]
    public struct SaveTablePrefab
    {
        public SaveDataTable SaveDataTable;
    }

    //세이브 목록 불러오기
    public void GetSaveDataList()
    {
        LoadButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
        LoadButtonImage.raycastTarget = false;
        int Tables = 0;

        if (isSaveData == false)
        {
            for (int i = 0; i < 10; i++)
            {
                DataSaveManager.instance.GetSavedDate(i);
                if (i == 0)
                {
                    string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log.json");
                    if (File.Exists(dataPath))
                    {
                        Tables++;
                        saveTablePrefab[i].SaveDataTable.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.DataTableGet(i);
                    }
                    else
                    {
                        saveTablePrefab[i].SaveDataTable.gameObject.SetActive(false);
                    }
                }
                else
                {
                    string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log" + i + ".json");
                    if (File.Exists(dataPath))
                    {
                        Tables++;
                        saveTablePrefab[i].SaveDataTable.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.DataTableGet(i);
                    }
                    else
                    {
                        saveTablePrefab[i].SaveDataTable.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            NewGameButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            LoadButtonImage2.raycastTarget = false;
            Tables = 10;

            for (int i = 0; i < 10; i++)
            {
                DataSaveManager.instance.GetSavedDate(i);
                if (i == 0)
                {
                    string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log.json");
                    if (File.Exists(dataPath))
                    {
                        saveTablePrefab[i].SaveDataTable.NameText.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.InformText.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.DeleteIconPrefab.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.IsEmpty = false;
                        saveTablePrefab[i].SaveDataTable.DataTableGet(i);
                    }
                    else
                    {
                        saveTablePrefab[i].SaveDataTable.NameText.gameObject.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.InformText.gameObject.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.DeleteIconPrefab.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.IsEmpty = true;
                    }
                }
                else
                {
                    string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log" + i + ".json");
                    if (File.Exists(dataPath))
                    {
                        saveTablePrefab[i].SaveDataTable.NameText.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.InformText.gameObject.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.DeleteIconPrefab.SetActive(true);
                        saveTablePrefab[i].SaveDataTable.IsEmpty = false;
                        saveTablePrefab[i].SaveDataTable.DataTableGet(i);
                    }
                    else
                    {
                        saveTablePrefab[i].SaveDataTable.NameText.gameObject.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.InformText.gameObject.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.DeleteIconPrefab.SetActive(false);
                        saveTablePrefab[i].SaveDataTable.IsEmpty = true;
                    }
                }
            }
        }

        if (Tables < 5)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 500);
        else if (Tables == 5)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 750);
        else if (Tables == 6)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 900);
        else if (Tables == 7)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 1050);
        else if (Tables == 8)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 1200);
        else if (Tables == 9)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 1350);
        else if (Tables == 10)
            ScrollVerticalSize.sizeDelta = new Vector2(1350, 1500);
    }

    //데이터 경로 지정하기
    public void GetDataPath(int number)
    {
        if (number == 0)
            DataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log.json");
        else
            DataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log" + number + ".json");
        TableDataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log Date" + number + ".json");
    }

    public void LoadButtonClick()
    {
        if (isSaveData == false) //세이브 파일을 불러서 게임을 시작하기
        {
            if (SceneLoad1 == null)
                SceneLoad1 = FindObjectOfType<SceneLoad1>();
            SceneLoad1.ButtonBackground.raycastTarget = false;
            SceneLoad1.progressbar.gameObject.SetActive(true);
            StartCoroutine(SceneLoad1.MissionStart());
            DataSaveManager.instance.LoadSaveData(DataPath); //DataSaveManager에다 불러올 데이터 string값을 전송하기
        }
        else
        {
            if (File.Exists(DataPath))
            {
                SaveStart = true;
                WordPrintMenu.SaveStart();
                StartCoroutine(SystemMessages.MessageOn(2, 0));
            }
            else //해당 테이블에 데이터가 없으면 즉시 저장
            {
                StartCoroutine(DataSaveManager.instance.SaveStart(DataPath, TableNumber)); //저장을 실행하기
                InitializeClickTable();
                TableNumber = 0;
                DataPath = null;
                GetSaveDataList();
            }
        }
    }
    public void LoadButtonDown()
    {
        ButtonClicked = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        LoadButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void LoadButtonUp()
    {
        if (ButtonClicked == true)
        {
            LoadButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        ButtonClicked = false;
    }
    public void LoadButtonEnter()
    {
        if (ButtonClicked == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            LoadButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void LoadButtonExit()
    {
        if (ButtonClicked == true)
        {
            LoadButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //새 게임 시작(우주맵에서는 데이터 불러오기)
    public void NewGameButtonClick()
    {
        if (isSaveData == false) //새 게임 시작
        {
            string StartPath = Path.Combine(Application.persistentDataPath, "Start UCCIS Battle Log.json");
            DataSaveManager.instance.LoadSaveData(StartPath);

            if (SceneLoad1 == null)
                SceneLoad1 = FindObjectOfType<SceneLoad1>();
            SceneLoad1.ButtonBackground.raycastTarget = false;
            SceneLoad1.progressbar.gameObject.SetActive(true);
            StartCoroutine(SceneLoad1.MissionStart());
            DataSaveManager.instance.NewGame();
        }
        else //데이터 다시 불러오기
        {
            StartCoroutine(ReStartLoadData());
        }
    }
    public void NewGameButtonDown()
    {
        ButtonClicked = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        NewGameButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void NewGameButtonUp()
    {
        if (ButtonClicked == true)
        {
            NewGameButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        ButtonClicked = false;
    }
    public void NewGameButtonEnter()
    {
        if (ButtonClicked == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            NewGameButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void NewGameButtonExit()
    {
        if (ButtonClicked == true)
        {
            NewGameButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //취소 버튼
    public void CancelButtonClick()
    {
        InitializeClickTable();
        TableNumber = 0;
        DataPath = null;
        LoadButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
        LoadButtonImage.raycastTarget = false;
        if (isSaveData == true)
        {
            NewGameButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            LoadButtonImage2.raycastTarget = false;
        }
        DataSaveManager.instance.enabled = false;

        if (isSaveData == false)
        {
            if (SceneLoad1 == null)
                SceneLoad1 = FindObjectOfType<SceneLoad1>();
            SceneLoad1.StartText.SetActive(true);
            SceneLoad1.ButtonBackground.raycastTarget = true;
            SceneLoad1.LoadWindowPrefab.SetActive(false);
        }
        else
        {
            if (MainMenuButtonSystem == null)
                MainMenuButtonSystem = FindObjectOfType<MainMenuButtonSystem>();
            MainMenuButtonSystem.LoadWindowPrefab.SetActive(false);
        }
    }
    public void CancelButtonDown()
    {
        ButtonClicked = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void CancelButtonUp()
    {
        if (ButtonClicked == true)
        {
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        ButtonClicked = false;
    }
    public void CancelButtonEnter()
    {
        if (ButtonClicked == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void CancelButtonExit()
    {
        if (ButtonClicked == true)
        {
            CancelButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    public void InitializeClickTable()
    {
        for (int i = 0; i < 10; i++)
        {
            saveTablePrefab[i].SaveDataTable.ClickedPrefab.SetActive(false);
        }
    }

    //선택한 데이터 삭제
    public void DeleteDataStart()
    {
        if (isSaveData == false)
        {
            DeleteStart = true;
            MessagePrintBattle.DeleteStart();
            StartCoroutine(BattleMessages.MessageStart(2, 0, 0));
        }
        else
        {
            DeleteStart = true;
            WordPrintMenu.DeleteStart();
            StartCoroutine(SystemMessages.MessageOn(2, 0));
        }
    }

    //우주맵에서 데이터 다시 불러오기
    IEnumerator ReStartLoadData()
    {
        WordPrintSystem.UCCISBootingPrint(1000);
        UniverseMapSystem.MenuBooting.GetComponent<Animator>().SetFloat("Menu booting, UCCIS mark", 1);
        DataSaveManager.instance.LoadSaveData(DataPath); //DataSaveManager에다 불러올 데이터 string값을 전송하기
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}