using System.Collections;
using UnityEngine;

public class EnemyBodyDelete : MonoBehaviour
{
    public float DeleteTime = 4.0f;

    private void OnEnable()
    {
        StartCoroutine(BodyFalse());
    }

    IEnumerator BodyFalse()
    {
        yield return new WaitForSeconds(DeleteTime);
        gameObject.SetActive(false);
    }
}