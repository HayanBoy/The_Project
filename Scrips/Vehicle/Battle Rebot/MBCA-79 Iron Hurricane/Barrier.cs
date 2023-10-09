using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    //public int damage;
    private void Start()
    {
        Invoke("ActiveFalse", 60f);
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
