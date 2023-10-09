using UnityEngine;

public class TargetLoactionFollow : MonoBehaviour
{
    public Vector3 Target;

    void Update()
    {
        transform.position = Target;
    }
}