using Cinemachine;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    public GameObject Object;
    public CinemachineConfiner Cam;
    public PolygonCollider2D CamCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Object.gameObject.SetActive(true);
            Cam.m_BoundingShape2D = CamCollider;
        }
    }
}