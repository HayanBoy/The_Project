using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //ĭŸũ��, ��Ʈ��-ũ�ν��� 390
        if (collision.CompareTag("Atro-Crossfa 390"))
        {
            collision.GetComponent<BehaviourAtroCrossfa>().AimState = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //ĭŸũ��, ��Ʈ��-ũ�ν��� 390
        if (collision.CompareTag("Atro-Crossfa 390"))
        {
            collision.GetComponent<BehaviourAtroCrossfa>().AimState = false;
        }
    }
}