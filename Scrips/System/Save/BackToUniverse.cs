using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToUniverse : MonoBehaviour
{
    [Header("부팅 이펙트")]
    public GameObject BootingMenuPrefab;
    public Text UCCISText1;
    public Text UCCISText2;
    public Text UCCISText3;
    public Text UCCISLoadingText1;
    public Text UCCISLoadingText2;
    public Text UCCISLoadingText3;

    //나가기
    public IEnumerator Exit(int number)
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            UCCISText1.fontSize = 100;
            UCCISText2.fontSize = 100;
            UCCISText3.fontSize = 100;
            UCCISText1.text = string.Format("Delta Strike Group");
            UCCISText2.text = string.Format("Delta Strike Group");
            UCCISText3.text = string.Format("Delta Strike Group");

            if (number == 1)
            {
                UCCISLoadingText1.text = string.Format("Cancel the Delta Hurricane mission...");
                UCCISLoadingText2.text = string.Format("Cancel the Delta Hurricane mission...");
                UCCISLoadingText3.text = string.Format("Cancel the Delta Hurricane mission...");
            }
            else if (number == 2)
            {
                UCCISLoadingText1.text = string.Format("Delta Hurricane is returning to fleet...");
                UCCISLoadingText2.text = string.Format("Delta Hurricane is returning to fleet...");
                UCCISLoadingText3.text = string.Format("Delta Hurricane is returning to fleet...");
            }
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            UCCISText1.fontSize = 100;
            UCCISText2.fontSize = 100;
            UCCISText3.fontSize = 100;
            UCCISText1.text = string.Format("델타전단");
            UCCISText2.text = string.Format("델타전단");
            UCCISText3.text = string.Format("델타전단");

            if (number == 1)
            {
                UCCISLoadingText1.text = string.Format("델타 허리케인 임무 취소 중...");
                UCCISLoadingText2.text = string.Format("델타 허리케인 임무 취소 중...");
                UCCISLoadingText3.text = string.Format("델타 허리케인 임무 취소 중...");
            }
            else if (number == 2)
            {
                UCCISLoadingText1.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
                UCCISLoadingText2.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
                UCCISLoadingText3.text = string.Format("델타 허리케인이 함대로 복귀하는 중...");
            }
        }

        BattleSave.Save1.BattleLoadScene = number;

        BootingMenuPrefab.SetActive(true);
        BootingMenuPrefab.GetComponent<Animator>().SetFloat("Delta Strike Group Booting, UCCIS mark", 1);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Return Loading");
    }
}