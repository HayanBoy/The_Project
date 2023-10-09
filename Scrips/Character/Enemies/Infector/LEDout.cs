using System.Collections;
using UnityEngine;

public class LEDout : MonoBehaviour
{
    public SpriteRenderer led;
    private int Type;

    void OnEnable()
    {
        led = GetComponent<SpriteRenderer>();

        Type = Random.Range(0, 7);

        if(Type == 0)
            StartCoroutine(LED1());
        else if (Type == 1)
            StartCoroutine(LED2());
        else if (Type == 2)
            StartCoroutine(LED3());
        else if (Type == 3)
            StartCoroutine(LED4());
        else if (Type == 4)
            StartCoroutine(LED5());
    }

    IEnumerator LED1()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0.4f);
        }
    }

    IEnumerator LED2()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(1);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.1f);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(1);
            led.color = new Color(1, 1, 1, 0.4f);
        }
    }

    IEnumerator LED3()
    {
        while (true)
        {
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(1);
            led.color = new Color(1, 1, 1, 0);
        }
    }
    IEnumerator LED4()
    {
        while (true)
        {
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.3f);
            led.color = new Color(1, 1, 1, 0);
        }
    }

    IEnumerator LED5()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(1);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.7f);
            led.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.3f);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.4f);
            led.color = new Color(1, 1, 1, 0.4f);
        }
    }
}