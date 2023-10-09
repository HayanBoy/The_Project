using UnityEngine;

public class BackCollider : MonoBehaviour
{
    public GameObject CamCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            CamCollider.transform.position = transform.position;
        }
    }
}