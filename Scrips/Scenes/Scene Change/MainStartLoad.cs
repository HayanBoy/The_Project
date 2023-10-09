using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainStartLoad : MonoBehaviour
{
    [Header("진행")]
    [SerializeField]
    [Range(0, 1)]
    private float progressAnimationMultiplier = 0.25f;
    public Slider progressbar;
    AsyncOperation loadOperation;
    private float currentValue;
    private float targetValue;
    private bool StartLoad = false;

    [Header("시작")]
    public string Map;

    [Header("부팅 이펙트")]
    public Text UCCISText1;
    public Text UCCISText2;
    public Text UCCISText3;
    public Text UCCISLoadingText1;
    public Text UCCISLoadingText2;
    public Text UCCISLoadingText3;

    IEnumerator LoadScene()
    {
        yield return null;

        if (BattleSave.Save1.LanguageType == 1)
        {
            if (BattleSave.Save1.BattleLoadScene == 0)
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
            else
            {
                UCCISText1.text = string.Format("Delta Strike Group");
                UCCISText2.text = string.Format("Delta Strike Group");
                UCCISText3.text = string.Format("Delta Strike Group");

                if (BattleSave.Save1.BattleLoadScene == 1)
                {
                    UCCISLoadingText1.text = string.Format("Cancel the Delta Hurricane mission...");
                    UCCISLoadingText2.text = string.Format("Cancel the Delta Hurricane mission...");
                    UCCISLoadingText3.text = string.Format("Cancel the Delta Hurricane mission...");
                }
                else if (BattleSave.Save1.BattleLoadScene == 2)
                {
                    UCCISLoadingText1.text = string.Format("Delta Hurricane is returning to fleet...");
                    UCCISLoadingText2.text = string.Format("Delta Hurricane is returning to fleet...");
                    UCCISLoadingText3.text = string.Format("Delta Hurricane is returning to fleet...");
                }
            }
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (BattleSave.Save1.BattleLoadScene == 0)
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
            else
            {
                UCCISText1.text = string.Format("델타전단");
                UCCISText2.text = string.Format("델타전단");
                UCCISText3.text = string.Format("델타전단");

                if (BattleSave.Save1.BattleLoadScene == 1)
                {
                    UCCISLoadingText1.text = string.Format("델타 허리케인 임무 취소 중...");
                    UCCISLoadingText2.text = string.Format("델타 허리케인 임무 취소 중...");
                    UCCISLoadingText3.text = string.Format("델타 허리케인 임무 취소 중...");
                }
                else if (BattleSave.Save1.BattleLoadScene == 2)
                {
                    UCCISLoadingText1.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
                    UCCISLoadingText2.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
                    UCCISLoadingText3.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
                }
            }
        }

        progressbar.value = currentValue = targetValue = 0;
        var currentScene = SceneManager.GetActiveScene();
        loadOperation = SceneManager.LoadSceneAsync(Map);
        loadOperation.allowSceneActivation = false;
        StartLoad = true;

        while (!loadOperation.isDone)
        {
            yield return null;

            if (loadOperation.progress < 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            else if (loadOperation.progress >= 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);

            if (progressbar.value >= 1f && loadOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2);
                loadOperation.allowSceneActivation = true;
            }
        }
    }

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (StartLoad == true)
        {
            targetValue = loadOperation.progress / 0.9f;

            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            progressbar.value = currentValue;

            if (Mathf.Approximately(currentValue, 1))
            {
                loadOperation.allowSceneActivation = true;
            }
        }
    }
}