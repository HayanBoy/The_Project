using System.Collections;
using UnityEngine;

public class LEDout2 : MonoBehaviour
{
    public SpriteRenderer led;
    private int Type;
    private bool Out;
    private float OutTime = 1;

    void OnEnable()
    {
        led = GetComponent<SpriteRenderer>();

        Type = Random.Range(0, 1);

        if (Type == 0)
            StartCoroutine(LED1());
    }

    private void Update()
    {
        if (Out == true)
        {
            OutTime -= 0.01f;
            led.color = new Color(1, 1, 1, OutTime);
        }
    }

    IEnumerator LED1()
    {
        for(int i = 0; i <= 30; i++)
        {
            yield return new WaitForSeconds(0.05f);
            led.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            led.color = new Color(1, 1, 1, 0.4f);

            if (i == 30)
                Out = true;
        }
    }
}