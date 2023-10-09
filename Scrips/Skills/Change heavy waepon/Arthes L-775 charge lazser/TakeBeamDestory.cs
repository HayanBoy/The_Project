using UnityEngine;

public class TakeBeamDestory : MonoBehaviour
{
    public float DestroyTime;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
