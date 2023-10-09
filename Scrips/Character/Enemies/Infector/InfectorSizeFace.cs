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

        //�� ����� �⺻ ��(1.0)���� �� Ŭ ���, �� ũ�⸦ Ű���.
        if (infectorSize.BodySizeTransport >= 0)
        {
            FaceSize = 0.65f + infectorSize.BodySizeTransport;
            transform.localScale = new Vector3(FaceSize, FaceSize, 1);
        }
    }
}
