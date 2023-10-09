using UnityEngine;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour
{
    void Awake()
    {
        Invoke("LoadToScene", 8);
    }

    public void LoadToScene()
    {
        SceneManager.LoadScene("Main menu");
    }
}