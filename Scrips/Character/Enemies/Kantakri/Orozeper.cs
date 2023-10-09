using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Orozeper: Character
{
    public float hitPoints;
    float armor;
    float Ricochet;

   // public Slider HealthBar; //체력 게이지바


    //private int TakeDamageSoundRandom;
    //public AudioClip Voice1;
    //public AudioClip Voice2;
    //public AudioClip Voice3;
    //public AudioClip Voice4;

    //private int DeathSoundRandom;
    //public AudioClip DeathVoice1;
    //public AudioClip DeathVoice2;
    //public AudioClip DeathVoice3;
    //public AudioClip DeathVoice4;

    //public void RandomTakeDamageSound()
    //{
    //    TakeDamageSoundRandom = Random.Range(0, 4);

    //    if (TakeDamageSoundRandom == 0)
    //        SoundManager.instance.SFXPlay("Sound", Voice1);
    //    else if (TakeDamageSoundRandom == 1)
    //        SoundManager.instance.SFXPlay("Sound", Voice2);
    //    else if (TakeDamageSoundRandom == 2)
    //        SoundManager.instance.SFXPlay("Sound", Voice3);
    //    else if (TakeDamageSoundRandom == 3)
    //        SoundManager.instance.SFXPlay("Sound", Voice4);
    //}

    //public void RandomDaethSound()
    //{
    //    DeathSoundRandom = Random.Range(0, 4);

    //    if (DeathSoundRandom == 0)
    //        SoundManager.instance.SFXPlay("Sound", DeathVoice1);
    //    else if (DeathSoundRandom == 1)
    //        SoundManager.instance.SFXPlay("Sound", DeathVoice2);
    //    else if (DeathSoundRandom == 2)
    //        SoundManager.instance.SFXPlay("Sound", DeathVoice3);
    //    else if (DeathSoundRandom == 3)
    //        SoundManager.instance.SFXPlay("Sound", DeathVoice4);
    //}


    public void Start()
    {
        hitPoints = startingHitPoints;
        armor = startingArmor;
     //   HealthBar.value = hitPoints;
    }

    //플레이어가 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            Ricochet = Random.Range(0, 5);

            if (Ricochet != 0)
            {
                hitPoints = hitPoints - (damage / armor);
                //HealthBar.value = hitPoints;
                //StartCoroutine(DamageAction());

                if (hitPoints <= float.Epsilon)
                {
                    KillCharacter();      
                    break;
                }

                if (interval > float.Epsilon)
                {
                    yield return new WaitForSeconds(interval);
                }

                else
                {
                    break;
                }
            }
            else if (Ricochet == 0)
            {
                //Debug.Log("팅!");
            }
        }
    }

    public IEnumerator DamageAction()
    {
        int damageAction;

        damageAction = Random.Range(1, 3); //1~2까지의 숫자를 출력
        //Debug.Log(damageAction);

        if (damageAction == 1)
        {
            GetComponent<Animator>().SetBool("Taking damage1, player", true);
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().SetBool("Taking damage1, player", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Taking damage2, player", true);
            yield return new WaitForSeconds(0.4f);
            GetComponent<Animator>().SetBool("Taking damage2, player", false);
        }
    }
}