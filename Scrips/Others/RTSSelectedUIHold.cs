using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSSelectedUIHold : MonoBehaviour
{
    public Transform Pos;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Pos.rotation.z * -1);
    }
}