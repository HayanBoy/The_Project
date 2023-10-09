using System.Collections;
using UnityEngine;

public class ShellDelete : MonoBehaviour
{
    public float DeleteTime = 4.0f;

    void OnEnable()
    {
        StartCoroutine(DeleteShell());        
    } 

    IEnumerator DeleteShell()
    {
        yield return new WaitForSeconds(DeleteTime);
        gameObject.SetActive(false);      
    }
}