using UnityEngine;

public class ParticleFollowPosition : MonoBehaviour
{
    public Transform particlePos;

    void Update()
    {
        if (particlePos != null)
        {
            transform.position = particlePos.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}