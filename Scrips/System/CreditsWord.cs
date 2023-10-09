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
            PresentText.text = string.Format("제작 : 퓨에넬라\n감독: 강교남");
            CreditText.text = string.Format("강교남\n감독, 게임 기획, 사운드 효과, BGM\n\n황규민\n프로그래밍, 게임 엔진, 시나리오 각본 및 설정, 일러스트, UI 디자인, 그래픽 리소스, 애니메이션, 시각 효과\n\n송희선\n프로그래밍");
        }
    }
}