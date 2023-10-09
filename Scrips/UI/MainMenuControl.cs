using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuControl : MonoBehaviour
{

    public Button level02Button, level03Button, level04Button;
    int levelPassed = 5;

    void Start()
    {
        switch (levelPassed)
        {
            case 3:
                level02Button.interactable = true;
                break;
            case 4:
                level02Button.interactable = true;
                level03Button.interactable = true;
                break;

            case 5:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                break;
        }
    }

    public void LevelToLoad(int level)
    {
        SceneManager.LoadScene("Loading_Stage" + level);
    }

    public void resetPlayerPrefs()
    {
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;

        PlayerPrefs.DeleteAll();
    }

}
