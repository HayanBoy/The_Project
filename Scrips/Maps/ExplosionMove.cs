using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMove : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * 1 * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * Speed * Time.deltaTime);
        }
    }
}
