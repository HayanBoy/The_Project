using System.Collections;
using UnityEngine;

public class SkyCraneBehavior : MonoBehaviour
{
    float dropSpeed = 20f;
    bool falling = true;
    bool Destruction = false;
    bool SlowDown = false;
    public string DeathBodyName;
    public string ExplosionName;

    public Transform modelPos;
    public Transform engienPos;
    public Transform nozzleRightPos;
    public Transform nozzleLeftPos;
    public Transform part1Pos;
    public Transform part2Pos;
    public Transform part3Pos;
    public Transform particlePos;

    //  float deathTime = 2.5f;

    int part1Random;
    int part2Random;
    int part3Random;

    GameObject[] PoolMaker;

    public AudioClip LandingRokectBoom1;
    public AudioClip LandingRokect1;
    public AudioClip Explosion;

    float SoundBar1;
    float SoundBar2;

    private void OnEnable()
    {
        dropSpeed = 20f;
        falling = true;
        Destruction = false;
        SlowDown = false;
    }

    void Update()
    {
        if (falling == true)
        {
            transform.Translate(transform.up * -1 * dropSpeed * Time.deltaTime); //馬悪
            StartCoroutine(TheFall()); //馬悪
        }
        if (SlowDown == true)
            dropSpeed -= Time.deltaTime * 8.4f; //姶紗
        if (dropSpeed < 0)
        {
            dropSpeed = 0;
        }
        SelfDestruction();
    }

    void SelfDestruction()
    {
        if(Destruction == true)
        {
            Destruction = false;
            gameObject.SetActive(false);
            Destroy();
        }
    }

    //馬悪
    IEnumerator TheFall()
    {
        yield return new WaitForSeconds(1f);
        SlowDown = true;
        yield return new WaitForSeconds(0.5f);
        if (SoundBar1 == 0)
        {
            SoundBar1 += Time.deltaTime;
            SoundManager.instance.SFXPlay2("Sound", LandingRokectBoom1);
        }
        yield return new WaitForSeconds(0.5f);
        if (SoundBar2 == 0)
        {
            SoundBar2 += Time.deltaTime;
            SoundManager.instance.SFXPlay6("Sound", LandingRokect1);
        }

        yield return new WaitForSeconds(1f);
        SlowDown = false;

        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.SFXPlay31("Sound", Explosion);
        Destruction = true;
    }

    void Destroy()
    {
        GameObject Model = SingletonObject.instance.Loader(DeathBodyName);
        Model.transform.position = modelPos.position;
        Model.transform.rotation = modelPos.rotation;
        DebrisSkyCrane ModelPos = Model.GetComponent<DebrisSkyCrane>();
        ModelPos.Pos = Model.transform.position.y;

        GameObject particle = SingletonObject.instance.Loader(ExplosionName);
        particle.transform.position = particlePos.position;
        particle.transform.rotation = particlePos.rotation;

        GameObject Engine = SingletonObject.instance.Loader("engine");
        Engine.transform.position = engienPos.position;
        Engine.transform.rotation = engienPos.rotation;
        DebrisSkyCrane Enginepos = Engine.GetComponent<DebrisSkyCrane>();
        Enginepos.Pos = Engine.transform.position.y;

        GameObject NozzleRight = SingletonObject.instance.Loader("nozzleRight");
        NozzleRight.transform.position = nozzleRightPos.position;
        NozzleRight.transform.rotation = nozzleRightPos.rotation;
        DebrisSkyCrane NozzleRightpos = NozzleRight.GetComponent<DebrisSkyCrane>();
        NozzleRightpos.Pos = NozzleRight.transform.position.y;

        GameObject NozzleLeft = SingletonObject.instance.Loader("nozzleLeft");
        NozzleLeft.transform.position = nozzleLeftPos.position;
        NozzleLeft.transform.rotation = nozzleLeftPos.rotation;
        DebrisSkyCrane NozzleLeftpos = NozzleLeft.GetComponent<DebrisSkyCrane>();
        NozzleLeftpos.Pos = NozzleLeft.transform.position.y;

        part1Random = Random.Range(2, 4);
        for (int i = 1; i <= part1Random; i++)
        {
            GameObject Part1 = SingletonObject.instance.Loader("part1");
            Part1.transform.position = part1Pos.position;
            Part1.transform.rotation = part1Pos.rotation;
            DeathKaotiJaios4 Part1pos = Part1.GetComponent<DeathKaotiJaios4>();
            Part1pos.Pos = Part1.transform.position.y;
        }
        part2Random = Random.Range(4, 6);
        for (int i = 1; i <= part2Random; i++)
        {
            GameObject Part2 = SingletonObject.instance.Loader("part2");
            Part2.transform.position = part2Pos.position;
            Part2.transform.rotation = part2Pos.rotation;
            DeathKaotiJaios4 Part2pos = Part2.GetComponent<DeathKaotiJaios4>();
            Part2pos.Pos = Part2.transform.position.y;
        }
        part3Random = Random.Range(6, 8);
        for (int i = 1; i <= part3Random; i++)
        {
            GameObject Part3 = SingletonObject.instance.Loader("part3");
            Part3.transform.position = part3Pos.position;
            Part3.transform.rotation = part3Pos.rotation;
            DeathKaotiJaios4 Part3pos = Part3.GetComponent<DeathKaotiJaios4>();
            Part3pos.Pos = Part3.transform.position.y;
        }
    }
}