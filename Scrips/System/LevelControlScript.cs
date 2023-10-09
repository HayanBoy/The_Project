using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControlScript : MonoBehaviour
{
    //NOT USED
    // ΩÃ±€≈Ê 
    public static LevelControlScript instance = null;
    public Image MissionClear;
    public Text returnToTitle;
    public Image Congraturation;

    public Image FadeOut;

    int sceneIndex, levelPassed;
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        MissionClear.gameObject.SetActive(false);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

    public void youWin()
    {
        if (sceneIndex == 7)
        {
            Invoke("loadMainMenu", 10f);
            Invoke("Conraturation", 0.1f);
        }

          
        else
        {
            if (levelPassed < sceneIndex)
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);

            MissionClear.gameObject.SetActive(true);
            Invoke("AddText", 2f);
            Invoke("loadMainMenu", 5f);
        }
    }

    private void Update()
    {
        //Debug.Log(sceneIndex);
    }
    void Conraturation()
    {
        Congraturation.gameObject.SetActive(true);
        
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void AddText() // ¿·Ω√»ƒ ≈∏¿Ã∆≤∑Œ µπæ∆∞©¥œ¥Ÿ∏¶ ∂ÁøÏ¥¬ «‘ºˆ 
    {
        returnToTitle.gameObject.SetActive(true);
        FadeOut.gameObject.SetActive(true);
    }
}
