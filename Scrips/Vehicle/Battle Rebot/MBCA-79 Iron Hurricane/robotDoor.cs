using UnityEngine;

public class robotDoor : MonoBehaviour
{
    private void OnEnable()
    {
        GameControlSystem.robotTheDoor = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameControlSystem.robotTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameControlSystem.robotTheDoor = false;
        }
    }
}