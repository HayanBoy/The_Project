using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareAmmoMovementTaikaLaiThrotro1 : MonoBehaviour
{
    public float AmmoVelocity;

    private void OnEnable()
    {
        StartCoroutine(BulletFalse());
    }

    void Update()
    {
        //ÃÑ¾Ë ÀÌµ¿
        transform.Translate(transform.up * 1 * AmmoVelocity * Time.deltaTime);
    }

    IEnumerator BulletFalse()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
