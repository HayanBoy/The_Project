using UnityEngine.UI;
using UnityEngine;

public class CreditsWord : MonoBehaviour
{
    public Text PresentText;
    public Text CreditText;

    private void OnEnable()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            PresentText.text = string.Format("Presented by Fuenella\n\nDirected by Gyonam Kang");
            CreditText.text = string.Format("Gyonam Kang\nDirector, Sound effect, BGM\n\nGyumin Hwang\nProgramming, Game engine, Scenario script and setting, UI Design, Illustations, Graphic resources, Animations, Visual Effects\n\nHeasun Song\nProgramming, Game engine");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            PresentText.text = string.Format("���� : ǻ���ڶ�\n����: ������");
            CreditText.text = string.Format("������\n����, ���� ��ȹ, ���� ȿ��, BGM\n\nȲ�Թ�\n���α׷���, ���� ����, �ó����� ���� �� ����, �Ϸ���Ʈ, UI ������, �׷��� ���ҽ�, �ִϸ��̼�, �ð� ȿ��\n\n����\n���α׷���");
        }
    }
}