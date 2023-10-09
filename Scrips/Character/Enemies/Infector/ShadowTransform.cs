using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTransform : MonoBehaviour
{
    InfectorSize infectorSize;
    float ShadowBottom;

    void Start()
    {
        infectorSize = FindObjectOfType<InfectorSize>();

        ShadowBottom = infectorSize.BodySizeTransport * 2.5f;
        transform.Translate(new Vector3(0, ShadowBottom, 0));
    }
}