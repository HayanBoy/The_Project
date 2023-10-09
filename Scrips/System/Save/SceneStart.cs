using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    public string SpaceBattleScene;

    void Start()
    {
        BattleSave.Save1.GroundBattleCount = 1;
        SceneManager.LoadScene(SpaceBattleScene);
    }
}