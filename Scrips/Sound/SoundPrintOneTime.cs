using UnityEngine;

public class SoundPrintOneTime : MonoBehaviour
{
    public bool isUniverse; //함대전에서 실시하는지 여부
    public AudioClip Sound;

    void OnEnable()
    {
        if (isUniverse == true)
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", Sound);
        else
        {
            SoundManager.instance.SFXPlay("Clip", Sound);
        }
    }
}