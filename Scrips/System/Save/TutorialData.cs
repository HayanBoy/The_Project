using UnityEngine;

public class TutorialData : MonoBehaviour
{
    TutorialSystem TutorialSystem;

    //튜토리얼 시작하기
    public void StartTutorial()
    {
        if (BattleSave.Save1.FirstStart == true)
        {
            TutorialSystem = FindObjectOfType<TutorialSystem>();
            TutorialSystem.FleetMovingTutorial();
        }
    }
}