using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BGMManagement : MonoBehaviour
{
    [Header("스크립트")]
    public MainMenuButtonSystem MainMenuButtonSystem;
    public UniverseMapSystem UniverseMapSystem;
    public TutorialSystem TutorialSystem;

    [Header("BGM 오디오 소스")]
    public AudioSource MenuBGM;
    public AudioSource StandByBGM;
    public AudioSource FightBGM;
    public AudioSource PlanetFightBGM;
    public AudioSource StarFightBGM;

    [Header("BGM 관리")]
    public float SoundVolumeSpeed; //사운드 증감속도
    public bool Menu;
    public bool StandBy;
    public bool Fight;
    public bool PlanetFight;
    public bool StarFight;
    public bool StandByPause;
    public bool FightPause;
    public bool PlanetFightPause;
    public bool StarFightPause;
    private float SoundPlayTime;
    private float StartPlay;
    private float SoundEffectTime;
    private float SoundEffectTime2;

    [Header("메뉴 BGM")]
    public AudioClip MenuBGM1;

    [Header("대기 BGM")]
    public AudioClip StandByBGM1;
    public AudioClip StandByBGM2;
    public AudioClip StandByBGM3;

    [Header("전투 BGM")]
    public AudioClip FightBGM1;
    public AudioClip FightBGM2;
    public AudioClip FightBGM3;

    [Header("행성전투 BGM")]
    public AudioClip PlanetFightBGM1;
    public AudioClip PlanetFightBGM2;
    public AudioClip PlanetFightBGM3;

    [Header("항성전투 BGM")]
    public AudioClip StarFightBGM1;
    public AudioClip StarFightBGM2;
    public AudioClip StarFightBGM3;

    private void Update()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (MainMenuButtonSystem.MenuMode == true || UniverseMapSystem.UniverseMapMode == true || TutorialSystem.isTutorialWindowOpen == true) //메뉴
            {
                Menu = true;
                StandBy = false;
                Fight = false;
                PlanetFight = false;
                StarFight = false;
            }
            else if (MainMenuButtonSystem.MenuMode == false && UniverseMapSystem.UniverseMapMode == false && TutorialSystem.isTutorialWindowOpen == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().TargetShip == null) //비 전투 상황
                {
                    Menu = false;
                    StandBy = true;
                    Fight = false;
                    PlanetFight = false;
                    StarFight = false;
                }
                else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().TargetShip != null) //전투 상황
                {
                    Menu = false;
                    StandBy = false;
                    if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber >= 1 && ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber <= 15)
                    {
                        Fight = false;
                        PlanetFight = false;
                        StarFight = true;
                    }
                    else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber >= 1001 && ShipManager.instance.SelectedFlagShip[0].GetComponent<FlagshipSystemNumber>().PlayerNumber <= 1025)
                    {
                        Fight = false;
                        PlanetFight = true;
                        StarFight = false;
                    }
                    else
                    {
                        Fight = true;
                        PlanetFight = false;
                        StarFight = false;
                    }
                }
            }
        }

        //상황에 따른 BGM 변경
        if (Menu == true)
        {
            if (MenuBGM.volume < 0.5f)
            {
                MenuBGM.UnPause();
                MenuBGM.volume += Time.unscaledDeltaTime * SoundVolumeSpeed;
                StandByBGM.volume -= Time.unscaledDeltaTime * SoundVolumeSpeed;
                FightBGM.volume -= Time.unscaledDeltaTime * SoundVolumeSpeed;
                PlanetFightBGM.volume -= Time.unscaledDeltaTime * SoundVolumeSpeed;
                StarFightBGM.volume -= Time.unscaledDeltaTime * SoundVolumeSpeed;
            }
            if (MenuBGM.volume >= 0.5f)
            {
                MenuBGM.volume = 0.5f;
                StandByBGM.volume = 0;
                FightBGM.volume = 0;
                PlanetFightBGM.volume = 0;
                StarFightBGM.volume = 0;
                StandByBGM.Pause();
                FightBGM.Pause();
                PlanetFightBGM.Pause();
                StarFightBGM.Pause();
                StandByPause = true;
                FightPause = true;
                PlanetFightPause = true;
                StarFightPause = true;
            }

            if (SoundEffectTime == 0)
            {
                SoundEffectTime += Time.deltaTime;
                SoundEffectTime2 = 0;
                UniverseSoundManager.instance.mixer.SetFloat("0", -80);
                UniverseSoundManager.instance.mixer.SetFloat("3", -80);
                UniverseSoundManager.instance.mixer.SetFloat("6", -80);
                UniverseSoundManager.instance.mixer.SetFloat("9", -80);
                UniverseSoundManager.instance.mixer.SetFloat("12", -80);
                UniverseSoundManager.instance.mixer.SetFloat("15", -80);
                UniverseSoundManager.instance.mixer.SetFloat("-3", -80);
                UniverseSoundManager.instance.mixer.SetFloat("-6", -80);
                UniverseSoundManager.instance.mixer.SetFloat("-9", -80);
                UniverseSoundManager.instance.mixer.SetFloat("-12", -80);
                UniverseSoundManager.instance.mixer.SetFloat("-15", -80);
            }
        }
        else if(StandBy == true)
        {
            if (StandByBGM.volume < 0.5f)
            {
                StandByPause = false;
                StandByBGM.UnPause();
                MenuBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StandByBGM.volume += Time.deltaTime * SoundVolumeSpeed;
                FightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                PlanetFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StarFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
            }
            if (StandByBGM.volume >= 0.5f)
            {
                MenuBGM.volume = 0;
                StandByBGM.volume = 0.5f;
                FightBGM.volume = 0;
                PlanetFightBGM.volume = 0;
                StarFightBGM.volume = 0;
                MenuBGM.Pause();
                FightBGM.Pause();
                PlanetFightBGM.Pause();
                StarFightBGM.Pause();
                FightPause = true;
                PlanetFightPause = true;
                StarFightPause = true;
            }
        }
        else if (Fight == true)
        {
            if (FightBGM.volume < 0.5f)
            {
                FightPause = false;
                FightBGM.UnPause();
                MenuBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StandByBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                FightBGM.volume += Time.deltaTime * SoundVolumeSpeed;
                PlanetFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StarFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
            }
            if (FightBGM.volume >= 0.5f)
            {
                MenuBGM.volume = 0;
                StandByBGM.volume = 0;
                FightBGM.volume = 0.5f;
                PlanetFightBGM.volume = 0;
                StarFightBGM.volume = 0;
                StandByBGM.Pause();
                MenuBGM.Pause();
                PlanetFightBGM.Pause();
                StarFightBGM.Pause();
                StandByPause = true;
                PlanetFightPause = true;
                StarFightPause = true;
            }
        }
        else if (PlanetFight == true)
        {
            if (PlanetFightBGM.volume < 0.5f)
            {
                PlanetFightPause = false;
                PlanetFightBGM.UnPause();
                MenuBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StandByBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                FightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                PlanetFightBGM.volume += Time.deltaTime * SoundVolumeSpeed;
                StarFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
            }
            if (PlanetFightBGM.volume >= 0.5f)
            {
                MenuBGM.volume = 0;
                StandByBGM.volume = 0;
                FightBGM.volume = 0;
                PlanetFightBGM.volume = 0.5f;
                StarFightBGM.volume = 0;
                StandByBGM.Pause();
                FightBGM.Pause();
                MenuBGM.Pause();
                StarFightBGM.Pause();
                StandByPause = true;
                FightPause = true;
                StarFightPause = true;
            }
        }
        else if (StarFight == true)
        {
            if (StarFightBGM.volume < 0.5f)
            {
                StarFightPause = false;
                StarFightBGM.UnPause();
                MenuBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StandByBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                FightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                PlanetFightBGM.volume -= Time.deltaTime * SoundVolumeSpeed;
                StarFightBGM.volume += Time.deltaTime * SoundVolumeSpeed;
            }
            if (StarFightBGM.volume >= 0.5f)
            {
                MenuBGM.volume = 0;
                StandByBGM.volume = 0;
                FightBGM.volume = 0;
                PlanetFightBGM.volume = 0;
                StarFightBGM.volume = 0.5f;
                StandByBGM.Pause();
                FightBGM.Pause();
                PlanetFightBGM.Pause();
                MenuBGM.Pause();
                StandByPause = true;
                FightPause = true;
                PlanetFightPause = true;
            }
        }

        if (Menu == false)
        {
            if (SoundEffectTime2 == 0)
            {
                SoundEffectTime2 += Time.deltaTime;
                SoundEffectTime = 0;
                UniverseSoundManager.instance.mixer.SetFloat("0", 0);
                UniverseSoundManager.instance.mixer.SetFloat("3", 3);
                UniverseSoundManager.instance.mixer.SetFloat("6", 6);
                UniverseSoundManager.instance.mixer.SetFloat("9", 9);
                UniverseSoundManager.instance.mixer.SetFloat("12", 12);
                UniverseSoundManager.instance.mixer.SetFloat("15", 15);
                UniverseSoundManager.instance.mixer.SetFloat("-3", -3);
                UniverseSoundManager.instance.mixer.SetFloat("-6", -6);
                UniverseSoundManager.instance.mixer.SetFloat("-9", -9);
                UniverseSoundManager.instance.mixer.SetFloat("-12", -12);
                UniverseSoundManager.instance.mixer.SetFloat("-15", -15);
            }
        }


        //BGM이 종료되면 다음 BGM으로 변경
        if (StartPlay >= 5)
        {
            if (StandByBGM.isPlaying == false && StandByPause == false)
            {
                if (SoundPlayTime == 0)
                {
                    SoundPlayTime += Time.deltaTime;
                    StartCoroutine(StandByBGMStart());
                }
            }
            if (FightBGM.isPlaying == false && FightPause == false)
            {
                if (SoundPlayTime == 0)
                {
                    SoundPlayTime += Time.deltaTime;
                    StartCoroutine(FightBGMStart());
                }
            }
            if (PlanetFightBGM.isPlaying == false && PlanetFightPause == false)
            {
                if (SoundPlayTime == 0)
                {
                    SoundPlayTime += Time.deltaTime;
                    StartCoroutine(PlanetFightBGMStart());
                }
            }
            if (StarFightBGM.isPlaying == false && StarFightPause == false)
            {
                if (SoundPlayTime == 0)
                {
                    SoundPlayTime += Time.deltaTime;
                    StartCoroutine(StarFightBGMStart());
                }
            }
        }
        if (StartPlay < 5)
            StartPlay += Time.deltaTime;
        else if (StartPlay >= 5)
            StartPlay = 5;
    }

    IEnumerator StandByBGMStart()
    {
        int RandomBGMTime = Random.Range(2, 5);
        yield return new WaitForSeconds(RandomBGMTime);

        int RandomBGM = Random.Range(0, 3);

        if (RandomBGM == 0)
        {
            StandByBGM.clip = StandByBGM1;
        }
        else if (RandomBGM == 1)
        {
            StandByBGM.clip = StandByBGM2;
        }
        else if (RandomBGM == 2)
        {
            StandByBGM.clip = StandByBGM3;
        }

        StandByBGM.Play();
        SoundPlayTime = 0;
    }

    IEnumerator FightBGMStart()
    {
        int RandomBGMTime = Random.Range(2, 5);
        yield return new WaitForSeconds(RandomBGMTime);

        int RandomBGM = Random.Range(0, 3);

        if (RandomBGM == 0)
        {
            FightBGM.clip = FightBGM1;
        }
        else if (RandomBGM == 1)
        {
            FightBGM.clip = FightBGM2;
        }
        else if (RandomBGM == 2)
        {
            FightBGM.clip = FightBGM3;
        }

        FightBGM.Play();
        SoundPlayTime = 0;
    }

    IEnumerator PlanetFightBGMStart()
    {
        int RandomBGMTime = Random.Range(2, 5);
        yield return new WaitForSeconds(RandomBGMTime);

        int RandomBGM = Random.Range(0, 3);

        if (RandomBGM == 0)
        {
            PlanetFightBGM.clip = PlanetFightBGM1;
        }
        else if (RandomBGM == 1)
        {
            PlanetFightBGM.clip = PlanetFightBGM2;
        }
        else if (RandomBGM == 2)
        {
            PlanetFightBGM.clip = PlanetFightBGM3;
        }

        PlanetFightBGM.Play();
        SoundPlayTime = 0;
    }

    IEnumerator StarFightBGMStart()
    {
        int RandomBGMTime = Random.Range(2, 5);
        yield return new WaitForSeconds(RandomBGMTime);

        int RandomBGM = Random.Range(0, 3);

        if (RandomBGM == 0)
        {
            StarFightBGM.clip = StarFightBGM1;
        }
        else if (RandomBGM == 1)
        {
            StarFightBGM.clip = StarFightBGM2;
        }
        else if (RandomBGM == 2)
        {
            StarFightBGM.clip = StarFightBGM3;
        }

        StarFightBGM.Play();
        SoundPlayTime = 0;
    }
}