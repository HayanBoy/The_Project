using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ricochetFalse());
    }

    IEnumerator ricochetFalse()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

}
