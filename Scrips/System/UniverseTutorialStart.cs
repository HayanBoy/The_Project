using UnityEngine;

public class UniverseTutorialStart : MonoBehaviour
{
    TutorialData TutorialData;

    void Start()
    {
        TutorialData = FindObjectOfType<TutorialData>();
        TutorialData.enabled = true;
    }
}