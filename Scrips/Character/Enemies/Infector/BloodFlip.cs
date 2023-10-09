using UnityEngine;

public class BloodFlip : MonoBehaviour
{
    private ParticleSystemRenderer psr;
    private Vector3 flip;
    public bool FlipOnline = false;

    void OnEnable()
    {
        FlipOnline = false;
    }

    void Start()
    {
        psr = GetComponent<ParticleSystemRenderer>();
    }

    void Update()
    {
        if (FlipOnline == true)
        {
            psr.flip = flip;
            flip.x = 0;
        }
    }
}