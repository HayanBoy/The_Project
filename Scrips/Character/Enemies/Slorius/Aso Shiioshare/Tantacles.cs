using System.Collections;
using UnityEngine;

public class Tantacles : MonoBehaviour
{
    private float Plus;
    private bool PlusAngle = false;

    void OnEnable()
    {
        StartCoroutine(Angles());
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Plus) * Time.deltaTime);

        if(PlusAngle == true)
            Plus -= Time.deltaTime * 1000f;
        else
            Plus += Time.deltaTime * 1000f;
    }

    IEnumerator Angles()
    {
        while(true)
        {
            Plus = 200;
            PlusAngle = true;
            yield return new WaitForSeconds(0.3f);
            Plus = -200;
            PlusAngle = false;
            yield return new WaitForSeconds(0.6f);
            Plus = 200;
            PlusAngle = true;
            yield return new WaitForSeconds(0.3f);
        }
    }
}