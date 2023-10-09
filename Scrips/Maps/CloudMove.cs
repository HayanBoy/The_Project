using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Translate(transform.right * 1 * Speed * Time.deltaTime);
    }
}