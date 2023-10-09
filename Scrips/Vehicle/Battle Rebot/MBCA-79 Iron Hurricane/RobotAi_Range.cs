using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAi_Range : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            GameControlSystem.robotTheAIRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            GameControlSystem.robotTheAIRange = false;
        }
    }
}
