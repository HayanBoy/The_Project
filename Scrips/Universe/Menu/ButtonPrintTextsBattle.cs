using UnityEngine.UI;
using UnityEngine;

public class ButtonPrintTextsBattle : MonoBehaviour
{
    public string EnglishName;
    public string KoreanName;
    public Text Text;

    private void OnEnable()
    {
        Invoke("TurnOn", 0.25f);
    }

    void TurnOn()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            Text.text = EnglishName;
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            Text.text = KoreanName;
        }
    }
}