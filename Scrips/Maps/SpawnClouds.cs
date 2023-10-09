using UnityEngine;

public class SpawnClouds : MonoBehaviour
{
    public Transform CloudPos1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.CompareTag("Cloud Delete"))
        {
            transform.position = CloudPos1.position;
        }
    }
}