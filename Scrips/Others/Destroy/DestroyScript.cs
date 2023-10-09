using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public float Time;

    void Start()
    {
        Destroy(this.gameObject, Time);
    }
}