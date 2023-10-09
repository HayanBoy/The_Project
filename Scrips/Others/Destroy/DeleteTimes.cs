using System.Collections;
using UnityEngine;

public class DeleteTimes : MonoBehaviour
{
    public float DeleteTime;

    void OnEnable()
    {
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(DeleteTime);
        gameObject.SetActive(false);
    }
}