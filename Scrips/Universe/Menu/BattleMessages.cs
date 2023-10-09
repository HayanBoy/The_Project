using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class BattleMessages : MonoBehaviour
{
    [Header("스크립트")]
    public MessagePrintBattle MessagePrintBattle;
    public BackToUniverse BackToUniverse;
    public SceneLoad1 SceneLoad1;
    public LoadDataManager LoadDataManager;

    [Header("메시지")]
    public int MessageType; //어느 씬에서 진행되었는지에 대한 여부
    public Text MessageOK;
    public Text MessageCancel;

    [Header("진행 버튼")]
    public GameObject AcceptPrefab;
    public GameObject CancelPrefab;
    public GameObject MessagePrefab;
    public Image AcceptButtonImage;
    public Image CancelButtonImage;
    private bool AcceptClick;
    private bool CancelClick;
    private int ProcessNumber; //창을 닫을 때, 진행 창의 연출 번호

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip CancelUIAudio;
    public AudioClip MainInformOnAudio;
    public AudioClip MainInformOffAudio;

    //수락 버튼
    public void AcceptButtonClick()
    {
        //허리케인 작전 씬에서 취소하고 다시 함대전으로 돌아가기
        if (MessageType == 2 && BackToUniverse != null)
        {
            StartCoroutine(BackToUniverse.Exit(1));
        }
        else if (MessageType == 7)
            StartCoroutine(VictoryMessageOff());
        else if (SceneLoad1.ExitGame == true)
        {
            Application.Quit();
        }

        //세이브 삭제를 실행하기
        else if (LoadDataManager.DeleteStart == true)
        {
            LoadDataManager.DeleteStart = false;
            DataSaveManager.instance.DeleteData(LoadDataManager.DataPath);
            DataSaveManager.instance.DeleteData(LoadDataManager.TableDataPath);
            LoadDataManager.InitializeClickTable();
            LoadDataManager.TableNumber = 0;
            LoadDataManager.DataPath = null;
            StartCoroutine(MessageOff());
        }
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

    //메시지 활성화
    public IEnumerator MessageStart(int number, int ProgressNumber, int MessageNumber)
    {
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", MainInformOnAudio);
        ProcessNumber = ProgressNumber;
        MessageType = MessageNumber;

        if (MessagePrintBattle != null && SceneLoad1 == null)
            MessagePrintBattle.MessagePrint();

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
        MessageType = 0;
        AcceptButtonImage.raycastTarget = false;
        CancelButtonImage.raycastTarget = false;
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 2);
        MessagePrefab.GetComponent<Animator>().SetFloat("Window number(Off), Main message", ProcessNumber);
        yield return new WaitForSecondsRealtime(0.2f);

        //메시지 창 초기화
        MessagePrefab.GetComponent<Animator>().SetFloat("Ask, Main message", 0);
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 0);
        MessagePrefab.SetActive(false);
    }

    //승리 메시지 활성화
    public IEnumerator VictoryMessageStart(int MessageNumber)
    {
        MessageType = MessageNumber;

        if (MessagePrintBattle != null)
            MessagePrintBattle.MessagePrint();

        MessagePrefab.SetActive(true);
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 3);
        MessagePrefab.GetComponent<Animator>().SetFloat("Ask, Main message", 5);
        yield return new WaitForSecondsRealtime(0.5f);
        AcceptButtonImage.raycastTarget = true;
        CancelButtonImage.raycastTarget = true;
    }

    //승리 메시지 종료
    IEnumerator VictoryMessageOff()
    {
        MessageType = 0;
        AcceptButtonImage.raycastTarget = false;
        CancelButtonImage.raycastTarget = false;
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 4);
        yield return new WaitForSecondsRealtime(0.4f);

        //메시지 창 초기화
        MessagePrefab.GetComponent<Animator>().SetFloat("Ask, Main message", 0);
        MessagePrefab.GetComponent<Animator>().SetFloat("Main window, Main message", 0);
        MessagePrefab.SetActive(false);
    }
}