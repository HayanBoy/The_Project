using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public float DeleteTime;

    void OnEnable()
    {
        Destroy(gameObject, DeleteTime);
    }
}