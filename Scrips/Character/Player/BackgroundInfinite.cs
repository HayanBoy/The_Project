using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInfinite : MonoBehaviour
{
    [SerializeField][Range(-1f, 1f)] float fadeTime = 3f;
    [SerializeField] float PosValue;

    Vector2 StartPos;
    float NewPos;

    void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        NewPos = Mathf.Repeat(Time.time * fadeTime, PosValue);
        transform.position = StartPos + Vector2.left * NewPos;
    }
}