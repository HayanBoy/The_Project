using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectorSizeFace : MonoBehaviour
{
    InfectorSize infectorSize;
    float FaceSize;

    void Start()
    {
        infectorSize = FindObjectOfType<InfectorSize>();

        //몸 사이즈가 기본 값(1.0)보다 더 클 경우, 얼굴 크기를 키운다.
        if (infectorSize.BodySizeTransport >= 0)
        {
            FaceSize = 0.65f + infectorSize.BodySizeTransport;
            transform.localScale = new Vector3(FaceSize, FaceSize, 1);
        }
    }
}
