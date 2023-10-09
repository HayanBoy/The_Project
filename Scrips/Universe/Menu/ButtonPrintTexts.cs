using UnityEngine.UI;
using UnityEngine;

public class ButtonPrintTexts : MonoBehaviour
{
    public WordPrintSystem WordPrintSystem;
    public string EnglishName;
    public string KoreanName;
    public Text Text;

    private void OnEnable()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            Text.text = EnglishName;
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            Text.text = KoreanName;
        }
    }
}