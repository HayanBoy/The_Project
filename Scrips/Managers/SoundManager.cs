using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour // ** ½Ì±ÛÅæ »ç¿îµå ¸Å´ÏÀú 
{
    public static SoundManager instance;
    public AudioMixer mixer1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //public void SFXPlay(string sfxName, AudioClip clip)
    //{
    //    GameObject Make = new GameObject(sfxName);
    //    AudioSource audiosource = Make.AddComponent<AudioSource>();
    //    audiosource.outputAudioMixerGroup = mixer1.FindMatchingGroups("Master")[0];
    //    audiosource.clip = clip;
    //    audiosource.spatialBlend = 1;
    //    audiosource.maxDistance = 20;
    //    audiosource.Play();

    //    Destroy(Make, clip.length);
    //}

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject Make = new GameObject(sfxName);
        AudioSource audiosource = Make.AddComponent<AudioSource>();
        audiosource.volume = 0.5f;
        audiosource.outputAudioMixerGroup = mixer1.FindMatchingGroups("Master")[0];
        audiosource.clip = clip;


        audiosource.Play();

        Destroy(Make, clip.length);
    }

    public void SFXPlay2(string sfxName2, AudioClip clip2)
    {
        GameObject Make2 = new GameObject(sfxName2);
        AudioSource audiosource2 = Make2.AddComponent<AudioSource>();
        audiosource2.volume = 0.5f;
        audiosource2.outputAudioMixerGroup = mixer1.FindMatchingGroups("0")[0];
        audiosource2.clip = clip2;

        audiosource2.Play();

        Destroy(Make2, clip2.length);
    }

    public void SFXPlay3(string sfxName3, AudioClip clip3)
    {
        GameObject Make3 = new GameObject(sfxName3);
        AudioSource audiosource3 = Make3.AddComponent<AudioSource>();
        audiosource3.volume = 0.5f;
        audiosource3.outputAudioMixerGroup = mixer1.FindMatchingGroups("1")[0];
        audiosource3.clip = clip3;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource3.Play();

        Destroy(Make3, clip3.length);
    }

    public void SFXPlay4(string sfxName4, AudioClip clip4)
    {
        GameObject Make4 = new GameObject(sfxName4);
        AudioSource audiosource4 = Make4.AddComponent<AudioSource>();
        audiosource4.volume = 0.5f;
        audiosource4.outputAudioMixerGroup = mixer1.FindMatchingGroups("2")[0];
        audiosource4.clip = clip4;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource4.Play();

        Destroy(Make4, clip4.length);
    }

    public void SFXPlay5(string sfxName5, AudioClip clip5)
    {
        GameObject Make5 = new GameObject(sfxName5);
        AudioSource audiosource5 = Make5.AddComponent<AudioSource>();
        audiosource5.volume = 0.5f;
        audiosource5.outputAudioMixerGroup = mixer1.FindMatchingGroups("3")[0];
        audiosource5.clip = clip5;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource5.Play();

        Destroy(Make5, clip5.length);
    }

    public void SFXPlay6(string sfxName6, AudioClip clip6)
    {
        GameObject Make6 = new GameObject(sfxName6);
        AudioSource audiosource6 = Make6.AddComponent<AudioSource>();
        audiosource6.volume = 0.5f;
        audiosource6.outputAudioMixerGroup = mixer1.FindMatchingGroups("4")[0];
        audiosource6.clip = clip6;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource6.Play();

        Destroy(Make6, clip6.length);
    }

    public void SFXPlay7(string sfxName7, AudioClip clip7)
    {
        GameObject Make7 = new GameObject(sfxName7);
        AudioSource audiosource7 = Make7.AddComponent<AudioSource>();
        audiosource7.volume = 0.5f;
        audiosource7.outputAudioMixerGroup = mixer1.FindMatchingGroups("5")[0];
        audiosource7.clip = clip7;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource7.Play();

        Destroy(Make7, clip7.length);
    }

    public void SFXPlay8(string sfxName8, AudioClip clip8)
    {
        GameObject Make8 = new GameObject(sfxName8);
        AudioSource audiosource8 = Make8.AddComponent<AudioSource>();
        audiosource8.volume = 0.5f;
        audiosource8.outputAudioMixerGroup = mixer1.FindMatchingGroups("6")[0];
        audiosource8.clip = clip8;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource8.Play();

        Destroy(Make8, clip8.length);
    }

    public void SFXPlay9(string sfxName9, AudioClip clip9)
    {
        GameObject Make9 = new GameObject(sfxName9);
        AudioSource audiosource9 = Make9.AddComponent<AudioSource>();
        audiosource9.volume = 0.5f;
        audiosource9.outputAudioMixerGroup = mixer1.FindMatchingGroups("7")[0];
        audiosource9.clip = clip9;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource9.Play();

        Destroy(Make9, clip9.length);
    }

    public void SFXPlay10(string sfxName10, AudioClip clip10)
    {
        GameObject Make10 = new GameObject(sfxName10);
        AudioSource audiosource10 = Make10.AddComponent<AudioSource>();
        audiosource10.volume = 0.5f;
        audiosource10.outputAudioMixerGroup = mixer1.FindMatchingGroups("8")[0];
        audiosource10.clip = clip10;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource10.Play();

        Destroy(Make10, clip10.length);
    }

    public void SFXPlay11(string sfxName11, AudioClip clip11)
    {
        GameObject Make11 = new GameObject(sfxName11);
        AudioSource audiosource11 = Make11.AddComponent<AudioSource>();
        audiosource11.volume = 0.5f;
        audiosource11.outputAudioMixerGroup = mixer1.FindMatchingGroups("9")[0];
        audiosource11.clip = clip11;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource11.Play();

        Destroy(Make11, clip11.length);
    }

    public void SFXPlay12(string sfxName12, AudioClip clip12)
    {
        GameObject Make12 = new GameObject(sfxName12);
        AudioSource audiosource12 = Make12.AddComponent<AudioSource>();
        audiosource12.volume = 0.5f;
        audiosource12.outputAudioMixerGroup = mixer1.FindMatchingGroups("10")[0];
        audiosource12.clip = clip12;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource12.Play();

        Destroy(Make12, clip12.length);
    }

    public void SFXPlay13(string sfxName13, AudioClip clip13)
    {
        GameObject Make13 = new GameObject(sfxName13);
        AudioSource audiosource13 = Make13.AddComponent<AudioSource>();
        audiosource13.volume = 0.5f;
        audiosource13.outputAudioMixerGroup = mixer1.FindMatchingGroups("11")[0];
        audiosource13.clip = clip13;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource13.Play();

        Destroy(Make13, clip13.length);
    }

    public void SFXPlay14(string sfxName14, AudioClip clip14)
    {
        GameObject Make14 = new GameObject(sfxName14);
        AudioSource audiosource14 = Make14.AddComponent<AudioSource>();
        audiosource14.volume = 0.5f;
        audiosource14.outputAudioMixerGroup = mixer1.FindMatchingGroups("12")[0];
        audiosource14.clip = clip14;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource14.Play();

        Destroy(Make14, clip14.length);
    }

    public void SFXPlay15(string sfxName15, AudioClip clip15)
    {
        GameObject Make15 = new GameObject(sfxName15);
        AudioSource audiosource15 = Make15.AddComponent<AudioSource>();
        audiosource15.volume = 0.5f;
        audiosource15.outputAudioMixerGroup = mixer1.FindMatchingGroups("13")[0];
        audiosource15.clip = clip15;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource15.Play();

        Destroy(Make15, clip15.length);
    }

    public void SFXPlay18(string sfxName18, AudioClip clip18)
    {
        GameObject Make18 = new GameObject(sfxName18);
        AudioSource audiosource18 = Make18.AddComponent<AudioSource>();
        audiosource18.volume = 0.5f;
        audiosource18.outputAudioMixerGroup = mixer1.FindMatchingGroups("16")[0];
        audiosource18.clip = clip18;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource18.Play();

        Destroy(Make18, clip18.length);
    }

    public void SFXPlay22(string sfxName22, AudioClip clip22)
    {
        GameObject Make22 = new GameObject(sfxName22);
        AudioSource audiosource22 = Make22.AddComponent<AudioSource>();
        audiosource22.volume = 0.5f;
        audiosource22.outputAudioMixerGroup = mixer1.FindMatchingGroups("20")[0];
        audiosource22.clip = clip22;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource22.Play();

        Destroy(Make22, clip22.length);
    }

    public void SFXPlay23(string sfxName23, AudioClip clip23)
    {
        GameObject Make23 = new GameObject(sfxName23);
        AudioSource audiosource23 = Make23.AddComponent<AudioSource>();
        audiosource23.volume = 0.5f;
        audiosource23.outputAudioMixerGroup = mixer1.FindMatchingGroups("_1")[0];
        audiosource23.clip = clip23;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource23.Play();

        Destroy(Make23, clip23.length);
    }

    public void SFXPlay24(string sfxName24, AudioClip clip24)
    {
        GameObject Make24 = new GameObject(sfxName24);
        AudioSource audiosource24 = Make24.AddComponent<AudioSource>();
        audiosource24.volume = 0.5f;
        audiosource24.outputAudioMixerGroup = mixer1.FindMatchingGroups("_2")[0];
        audiosource24.clip = clip24;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource24.Play();

        Destroy(Make24, clip24.length);
    }

    public void SFXPlay25(string sfxName25, AudioClip clip25)
    {
        GameObject Make25 = new GameObject(sfxName25);
        AudioSource audiosource25 = Make25.AddComponent<AudioSource>();
        audiosource25.volume = 0.5f;
        audiosource25.outputAudioMixerGroup = mixer1.FindMatchingGroups("_3")[0];
        audiosource25.clip = clip25;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource25.Play();

        Destroy(Make25, clip25.length);
    }

    public void SFXPlay26(string sfxName26, AudioClip clip26)
    {
        GameObject Make26 = new GameObject(sfxName26);
        AudioSource audiosource26 = Make26.AddComponent<AudioSource>();
        audiosource26.volume = 0.5f;
        audiosource26.outputAudioMixerGroup = mixer1.FindMatchingGroups("_4")[0];
        audiosource26.clip = clip26;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource26.Play();

        Destroy(Make26, clip26.length);
    }

    public void SFXPlay27(string sfxName27, AudioClip clip27)
    {
        GameObject Make27 = new GameObject(sfxName27);
        AudioSource audiosource27 = Make27.AddComponent<AudioSource>();
        audiosource27.volume = 0.5f;
        audiosource27.outputAudioMixerGroup = mixer1.FindMatchingGroups("_5")[0];
        audiosource27.clip = clip27;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource27.Play();

        Destroy(Make27, clip27.length);
    }

    public void SFXPlay28(string sfxName28, AudioClip clip28)
    {
        GameObject Make28 = new GameObject(sfxName28);
        AudioSource audiosource28 = Make28.AddComponent<AudioSource>();
        audiosource28.volume = 0.5f;
        audiosource28.outputAudioMixerGroup = mixer1.FindMatchingGroups("_6")[0];
        audiosource28.clip = clip28;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource28.Play();

        Destroy(Make28, clip28.length);
    }

    public void SFXPlay29(string sfxName29, AudioClip clip29)
    {
        GameObject Make29 = new GameObject(sfxName29);
        AudioSource audiosource29 = Make29.AddComponent<AudioSource>();
        audiosource29.volume = 0.5f;
        audiosource29.outputAudioMixerGroup = mixer1.FindMatchingGroups("_7")[0];
        audiosource29.clip = clip29;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource29.Play();

        Destroy(Make29, clip29.length);
    }

    public void SFXPlay30(string sfxName30, AudioClip clip30)
    {
        GameObject Make30 = new GameObject(sfxName30);
        AudioSource audiosource30 = Make30.AddComponent<AudioSource>();
        audiosource30.volume = 0.5f;
        audiosource30.outputAudioMixerGroup = mixer1.FindMatchingGroups("_8")[0];
        audiosource30.clip = clip30;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource30.Play();

        Destroy(Make30, clip30.length);
    }

    public void SFXPlay31(string sfxName31, AudioClip clip31)
    {
        GameObject Make31 = new GameObject(sfxName31);
        AudioSource audiosource31 = Make31.AddComponent<AudioSource>();
        audiosource31.volume = 0.5f;
        audiosource31.outputAudioMixerGroup = mixer1.FindMatchingGroups("_9")[0];
        audiosource31.clip = clip31;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource31.Play();

        Destroy(Make31, clip31.length);
    }

    public void SFXPlay32(string sfxName32, AudioClip clip32)
    {
        GameObject Make32 = new GameObject(sfxName32);
        AudioSource audiosource32 = Make32.AddComponent<AudioSource>();
        audiosource32.volume = 0.5f;
        audiosource32.outputAudioMixerGroup = mixer1.FindMatchingGroups("_10")[0];
        audiosource32.clip = clip32;
        //audiosource.spatialBlend = 1;
        //audiosource.maxDistance = 20;
        audiosource32.Play();

        Destroy(Make32, clip32.length);
    }
}