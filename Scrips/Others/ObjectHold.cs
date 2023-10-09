using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public Transform Pos;

    void Update()
    {
        if (Pos != null)
        {
            transform.position = Pos.position;
            transform.rotation = Pos.rotation;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}