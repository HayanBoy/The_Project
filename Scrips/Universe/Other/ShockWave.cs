using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public float MaxSize;
    public float IncreaseSpeed;
    private Vector3 scaleChange;
    public float DestroyTime;

    private void Awake()
    {
        scaleChange = new Vector3(0.04f, 0.04f, 0.04f);
        Destroy(gameObject, DestroyTime);
    }

    void Update()
    {
        if (transform.localScale.x < MaxSize)
            transform.localScale += scaleChange;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(scaleChange, 1f);
    }
}