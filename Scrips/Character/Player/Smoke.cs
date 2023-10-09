using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Transform smokePos;

    void Update()
    {
        if (smokePos != null)
        {
            transform.position = smokePos.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
