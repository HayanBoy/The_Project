using UnityEngine;

public class DeathSoundKaotiJaios4 : MonoBehaviour
{
    private int RicochetSoundRandom;
    public AudioClip DeathSound1;
    public AudioClip DeathSound2;
    public AudioClip DeathSound3;

    void OnEnable()
    {
        RicochetSoundRandom = Random.Range(0, 3);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay3("Sound", DeathSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay3("Sound", DeathSound2);
        else
            SoundManager.instance.SFXPlay3("Sound", DeathSound3);
    }
}
