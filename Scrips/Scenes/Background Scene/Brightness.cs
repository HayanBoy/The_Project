
using UnityEngine;

public class Brightness : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D Light;

    private bool Bright = false;
    public float fadeOutTime;

    void Update()
    {
        if (Bright == true)
            Light.intensity = Mathf.MoveTowards(Light.intensity, 1, Time.deltaTime * fadeOutTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Bright = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Bright = false;
        }
    }
}