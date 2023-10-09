using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //칸타크리, 아트로-크로스파 390
        if (collision.CompareTag("Atro-Crossfa 390"))
        {
            collision.GetComponent<BehaviourAtroCrossfa>().AimState = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //칸타크리, 아트로-크로스파 390
        if (collision.CompareTag("Atro-Crossfa 390"))
        {
            collision.GetComponent<BehaviourAtroCrossfa>().AimState = false;
        }
    }
}