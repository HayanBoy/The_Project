using System.Collections;
using UnityEngine;

public class ZRotation : MonoBehaviour
{
    public Transform Pos;

    void Update()
    {
        transform.rotation = Pos.rotation;
    }
}
