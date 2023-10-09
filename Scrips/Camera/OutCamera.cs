using UnityEngine;

public class OutCamera : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            transform.GetChild(0).gameObject.SetActive(false);
    }
}