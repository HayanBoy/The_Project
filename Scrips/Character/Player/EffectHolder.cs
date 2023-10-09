using UnityEngine;

public class EffectHolder : MonoBehaviour
{
    public Transform smokePos;

    void Update()
    {
        transform.position = smokePos.position;
    }
}