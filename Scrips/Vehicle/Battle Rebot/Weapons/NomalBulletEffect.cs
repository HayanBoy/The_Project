using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalBulletEffect : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Invoke("False", 2.5f);
    }

    void False()
    {
        gameObject.SetActive(false);
       
    }
}
