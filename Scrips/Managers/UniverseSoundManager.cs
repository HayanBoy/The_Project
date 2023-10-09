using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UniverseSoundManager : MonoBehaviour
{
    public static UniverseSoundManager instance;
    public bool isNoUniverse;
    public AudioMixer mixer;
    public AudioMixer BeltScrolMixer;
    public AudioMixer BGMMixer;
    public AudioSource source;

    public Slider SoundEffectSlider;
    public Slider BSSoundEffectSlider;
    public Slider BGMSlider;

    //옵션용 믹서 볼륨
    public void BGMSoundVolume()
    {
        float volume = BGMSlider.value;
        BattleSave.Save1.BGMSliderValue = BGMSlider.value;
        BGMMixer.SetFloat("BGM sound", Mathf.Log(volume) * 20);
    }
    public void MixerSoundVolume()
    {
        float volume = SoundEffectSlider.value;
        BattleSave.Save1.UniverseSoundEffectSliderValue = SoundEffectSlider.value;
        mixer.SetFloat("Universe Sound Effect", Mathf.Log(volume) * 20);
    }
    public void BeltScrolMixerSoundVolume()
    {
        float volume = BSSoundEffectSlider.value;
        BattleSave.Save1.BSSoundEffectSliderValue = BSSoundEffectSlider.value;
        BeltScrolMixer.SetFloat("Belt scroll Sound Effect", Mathf.Log(volume) * 20);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (isNoUniverse == false)
            StartCoroutine(LoadBGMSettings());
    }

    IEnumerator LoadBGMSettings()
    {
        yield return new WaitForSeconds(0.5f);

        BGMSlider.value = BattleSave.Save1.BGMSliderValue;
        BGMMixer.SetFloat("BGM sound", Mathf.Log(BGMSlider.value) * 20);
        SoundEffectSlider.value = BattleSave.Save1.UniverseSoundEffectSliderValue;
        mixer.SetFloat("Universe Sound Effect", Mathf.Log(SoundEffectSlider.value) * 20);
        BSSoundEffectSlider.value = BattleSave.Save1.BSSoundEffectSliderValue;
        BeltScrolMixer.SetFloat("Belt scroll Sound Effect", Mathf.Log(BSSoundEffectSlider.value) * 20);
    }

    public void UniverseSoundPlayMaster(string sfxName, AudioClip clip, Vector2 Pos)
    {
        GameObject Make = new GameObject(sfxName);
        Make.transform.position = Pos;
        AudioSource audiosource = Make.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];
        audiosource.volume = 0.5f;
        audiosource.clip = clip;
        audiosource.spatialBlend = 1;
        audiosource.spread = 100;
        audiosource.minDistance = 50;
        audiosource.maxDistance = 125;
        audiosource.rolloffMode = AudioRolloffMode.Linear;
        audiosource.Play();

        Destroy(Make, clip.length);
    }

    public void UniverseUISoundPlayMaster(string sfxName, AudioClip clip)
    {
        GameObject Make = new GameObject(sfxName);
        AudioSource audiosource = Make.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("UI")[0];
        audiosource.volume = 0.5f;
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(Make, clip.length);
    }
}