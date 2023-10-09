using System.Collections;
using UnityEngine;

public class MissileRespawn : MonoBehaviour
{
    public float DeleteTime = 4.0f;
    public GameObject Effect;

    void OnEnable()
    {
        StartCoroutine(DeleteShell());
    }

    private void OnDisable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        Effect.gameObject.SetActive(true);
    }

    IEnumerator DeleteShell()
    {
        yield return new WaitForSeconds(DeleteTime);
        gameObject.SetActive(false);
    }
}