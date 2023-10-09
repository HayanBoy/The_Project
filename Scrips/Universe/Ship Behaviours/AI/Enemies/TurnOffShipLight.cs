using System.Collections;
using UnityEngine;

public class TurnOffShipLight : MonoBehaviour
{
    public GameObject LED;
    public GameObject LED2;
    private float RandomLED;

    void Start()
    {
        RandomLED = Random.Range(0.02f, 0.06f);
        StartCoroutine(LEDDown());
    }

    //ºÒºûÀÌ ±ôºýÀÌ¸ç Á¤ÀüµÇ´Â È½¼ö
    IEnumerator LEDDown()
    {
        if (LED != null)
        {
            int RandomLEDCount = Random.Range(2, 8);
            float RandomLEDDownStart = Random.Range(0, 3);

            yield return new WaitForSeconds(RandomLEDDownStart);

            for (int i = 0; i < RandomLEDCount; i++)
            {
                yield return new WaitForSeconds(RandomLED);
                LED.SetActive(false);
                yield return new WaitForSeconds(RandomLED);
                LED.SetActive(true);
                yield return new WaitForSeconds(RandomLED);
                LED.SetActive(false);
            }

            yield return new WaitForSeconds(0.1f);
            LED.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            LED.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            LED.SetActive(false);
        }
        if (LED2 != null)
        {
            int RandomLEDCount = Random.Range(2, 8);
            float RandomLEDDownStart = Random.Range(0, 3);

            yield return new WaitForSeconds(RandomLEDDownStart);

            for (int i = 0; i < RandomLEDCount; i++)
            {
                yield return new WaitForSeconds(RandomLED);
                LED2.SetActive(false);
                yield return new WaitForSeconds(RandomLED);
                LED2.SetActive(true);
                yield return new WaitForSeconds(RandomLED);
                LED2.SetActive(false);
            }

            yield return new WaitForSeconds(0.1f);
            LED2.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            LED2.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            LED2.SetActive(false);
        }
    }
}