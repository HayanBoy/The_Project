
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D Light;

    private bool Dark = false;
    public float fadeOutTime;

    void Update()
    {
        if (Dark == true)
            Light.intensity = Mathf.MoveTowards(Light.intensity, 0.3f, Time.deltaTime * fadeOutTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Dark = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            Dark = false;
        }
    }
}