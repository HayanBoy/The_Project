using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovementTaikaLaiThrotro1 : MonoBehaviour
{
    ObjectManager objectManager;

    public float AmmoVelocity;

    int Ricochet;
    public int damage;

    public Transform ricochetEffectPos;
    GameObject RicochetEffect;

    public AudioClip DamageSound1;
    public AudioClip DamageSound2;
    public AudioClip DamageSound3;
    public AudioClip DamageSound4;
    public AudioClip DamageSound5;
    public AudioClip DamageSound6;

    //스크립트 KaotiJaios4로부터 전달받은 데미지
    public void SetDamage(int num)
    {
        damage = num;
    }

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    void OnEnable()
    {
        StartCoroutine(BulletFalse());
    }

    IEnumerator BulletFalse()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void DamageSounds()
    {
        int RandomSound = Random.Range(0, 3);

        if (RandomSound == 0)
            SoundManager.instance.SFXPlay("Sound", DamageSound1);
        else if (RandomSound == 1)
            SoundManager.instance.SFXPlay("Sound", DamageSound2);
        else
            SoundManager.instance.SFXPlay("Sound", DamageSound3);
    }

    void DamageSounds2()
    {
        int RandomSound = Random.Range(0, 3);

        if (RandomSound == 0)
            SoundManager.instance.SFXPlay("Sound", DamageSound4);
        else if (RandomSound == 1)
            SoundManager.instance.SFXPlay("Sound", DamageSound5);
        else
            SoundManager.instance.SFXPlay("Sound", DamageSound6);
    }

    public void Rico()
    {
        RicochetEffect = objectManager.RicoCHET();
        RicochetEffect.transform.position = ricochetEffectPos.position;
        RicochetEffect.transform.rotation = ricochetEffectPos.rotation;
    }

    void Update()
    {
        //총알 이동
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
        }
    }

    //적과 충돌할 때 데미지 전달
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player"))
        {
            Ricochet = Random.Range(0, 70);

            Player Player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
            Player.RicochetNum(Ricochet);
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            DamageSounds();
            gameObject.SetActive(false);            
        }

        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            Ricochet = Random.Range(0, 70);

            RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player 스크립트 불러오기
            RP.RicochetNum(Ricochet);
            StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            DamageSounds2();
            Rico();
            gameObject.SetActive(false);
        }
    }
}
