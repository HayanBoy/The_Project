using UnityEngine;

public class SoundFadeProcess : MonoBehaviour
{
    public AudioSource AudioSource;
    private bool SoundOn;

    private void OnEnable()
    {
        SoundOn = true;
        AudioSource.volume = 0;
    }

    private void OnDisable()
    {
        SoundOn = false;
    }

    private void Update()
    {
        if (SoundOn == true)
        {
            if (AudioSource.volume < 0.5f)
                AudioSource.volume += Time.deltaTime * 0.5f;
            else if (AudioSource.volume >= 0.5f)
                AudioSource.volume = 0.5f;
        }
        else
        {
            if (AudioSource.volume > 0)
                AudioSource.volume -= Time.deltaTime * 0.5f;
            else if (AudioSource.volume <= 0)
                AudioSource.volume = 0;
        }
    }
}