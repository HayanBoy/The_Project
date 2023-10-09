using System.Collections;
using UnityEngine;

public class TantaclesDead : MonoBehaviour
{
    private float Plus;
    private bool PlusAngle = false;
    private bool Stop = false;

    private void Start()
    {
        StartCoroutine(Angles());
        Invoke("StopTantacle", 10);
    }

    void StopTantacle()
    {
        Stop = true;
    }

    void Update()
    {
        if(Stop == false)
        {
            transform.Rotate(new Vector3(0, 0, Plus) * Time.deltaTime);

            if (PlusAngle == true)
                Plus -= Time.deltaTime * 1000f;
            else
                Plus += Time.deltaTime * 1000f;
        }
    }

    IEnumerator Angles()
    {
        if (Stop == false)
        {
            while (true)
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
}