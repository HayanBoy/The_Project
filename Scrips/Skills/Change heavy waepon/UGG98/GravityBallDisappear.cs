using UnityEngine;

public class GravityBallDisappear : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4.5f);
    }
}