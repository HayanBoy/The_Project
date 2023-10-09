using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectorSize : MonoBehaviour
{
    float BodySize;
    public float BodySizeTransport;

    void Start()
    {
        BodySize = Random.Range(0.9f, 1.1f);
        transform.localScale = new Vector3(BodySize, BodySize, 1);
        BodySizeTransport = 1f - BodySize;
    }
}
