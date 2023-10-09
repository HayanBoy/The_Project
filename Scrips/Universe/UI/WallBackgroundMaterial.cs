using UnityEngine;

public class WallBackgroundMaterial : MonoBehaviour
{
    public Material material;
    public bool DissolveStart;
    public bool DissolveSetting;
    public float Direction;
    public float TimeSpeed;
    private float DissolveSpeed;

    private void Awake()
    {
        material.SetFloat("_Dissolve", 0);
    }

    void Update()
    {
        if (DissolveSetting == true)
        {
            DissolveSpeed = 1;
            material.SetFloat("_Dissolve", 1);
            material.SetFloat("_Direction", Direction);
        }

        if (DissolveStart == true)
        {
            if (Direction == 0)
            {
                if (DissolveSpeed > 0.95f)
                    DissolveSpeed -= 0.15f * Time.unscaledDeltaTime * TimeSpeed;
                else if (DissolveSpeed > 0.9f && DissolveSpeed <= 0.95f)
                    DissolveSpeed -= 0.7f * Time.unscaledDeltaTime * TimeSpeed;
                else if (DissolveSpeed > 0 && DissolveSpeed <= 0.9f)
                    DissolveSpeed -= Time.unscaledDeltaTime * TimeSpeed;
            }
            else
            {
                if (DissolveSpeed > 0.1f)
                    DissolveSpeed -= Time.unscaledDeltaTime * TimeSpeed;
                else if (DissolveSpeed > 0.05f && DissolveSpeed <= 0.1f)
                    DissolveSpeed -= 0.7f * Time.unscaledDeltaTime * TimeSpeed;
                else if (DissolveSpeed > 0 && DissolveSpeed <= 0.05f)
                    DissolveSpeed -= 0.15f * Time.unscaledDeltaTime * TimeSpeed;
            }
            material.SetFloat("_Dissolve", DissolveSpeed);
        }
    }
}