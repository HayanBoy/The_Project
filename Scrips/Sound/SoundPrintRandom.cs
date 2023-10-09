using UnityEngine;

public class SoundPrintRandom : MonoBehaviour
{
    public bool isUniverse; //함대전에서 실시하는지 여부
    public int NumberOfSound;
    public AudioClip Audio1;
    public AudioClip Audio2;
    public AudioClip Audio3;
    public AudioClip Audio4;
    public AudioClip Audio5;
    public AudioClip Audio6;
    public AudioClip Audio7;
    public AudioClip Audio8;

    void OnEnable()
    {
        int RandomWalk = Random.Range(0, NumberOfSound);

        if (isUniverse == true)
        {
            if (RandomWalk == 0)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio1);
            else if (RandomWalk == 1)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio2);
            else if (RandomWalk == 2)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio3);
            else if (RandomWalk == 3)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio4);
            else if (RandomWalk == 4)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio5);
            else if (RandomWalk == 5)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio6);
            else if (RandomWalk == 6)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio7);
            else if (RandomWalk == 7)
                UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Audio8);
        }
        else
        {
            if (RandomWalk == 0)
                SoundManager.instance.SFXPlay2("Sound", Audio1);
            else if (RandomWalk == 1)
                SoundManager.instance.SFXPlay2("Sound", Audio2);
            else if (RandomWalk == 2)
                SoundManager.instance.SFXPlay2("Sound", Audio3);
            else if (RandomWalk == 3)
                SoundManager.instance.SFXPlay2("Sound", Audio4);
            else if (RandomWalk == 4)
                SoundManager.instance.SFXPlay2("Sound", Audio5);
            else if (RandomWalk == 5)
                SoundManager.instance.SFXPlay2("Sound", Audio6);
            else if (RandomWalk == 6)
                SoundManager.instance.SFXPlay2("Sound", Audio7);
            else if (RandomWalk == 7)
                SoundManager.instance.SFXPlay2("Sound", Audio8);
        }
    }
}