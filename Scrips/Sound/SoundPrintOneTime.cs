using UnityEngine;

public class SoundPrintOneTime : MonoBehaviour
{
    public bool isUniverse; //�Դ������� �ǽ��ϴ��� ����
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