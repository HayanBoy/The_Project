using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Speed));
    }
}
