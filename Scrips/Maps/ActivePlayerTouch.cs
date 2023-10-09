using UnityEngine;

public class ActivePlayerTouch : MonoBehaviour
{
    public GameObject Object;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Object.gameObject.SetActive(true);
        }
    }
}