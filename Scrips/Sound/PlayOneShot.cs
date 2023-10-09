using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    //NOT USED
    public AudioClip busterSound;
    private AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.PlayOneShot(busterSound);
    }
}