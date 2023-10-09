using System.Collections;
using UnityEngine;

public class SmokeKaotiJaios4 : MonoBehaviour
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