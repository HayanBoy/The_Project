
using UnityEngine;

public class LightOff : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D Light;
    public float fadeInTime;
    public float fadeOutTime;
    public float LightOuterRange;

    public bool FadeInComplete = false;

    void Awake()
    {
        Light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    private void OnEnable()
    {
        Light.pointLightOuterRadius = 0;
        FadeInComplete = false;
    }

    void Update()
    {
        if (FadeInComplete == false)
            Light.pointLightOuterRadius = Mathf.MoveTowards(Light.pointLightOuterRadius, LightOuterRange, Time.deltaTime * fadeInTime);
        if (Light.pointLightOuterRadius >= LightOuterRange)
            FadeInComplete = true;

        if (FadeInComplete == true)
            Light.pointLightOuterRadius = Mathf.MoveTowards(Light.pointLightOuterRadius, 0f, Time.deltaTime * fadeOutTime);
    }
}