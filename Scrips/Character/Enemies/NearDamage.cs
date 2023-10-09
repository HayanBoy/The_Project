using System.Collections;
using UnityEngine;

public class NearDamage : MonoBehaviour
{
    private int damage;
    public float AttackTime;

    public AudioClip DamageSound1;
    public AudioClip DamageSound2;
    public AudioClip DamageSound3;
    public AudioClip DamageSound4;
    public AudioClip DamageSound5;
    public AudioClip DamageSound6;

    public void SetDamage(int num, float time)
    {
        damage = num;
        AttackTime = time;
    }

    private void OnEnable()
    {
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(AttackTime);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.activeSelf == true)
        {
            if (collision is BoxCollider2D && collision.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
                StartCoroutine(player.NearDamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                DamageSounds();
                gameObject.SetActive(false);
            }
            if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
            {
                RobotPlayer RobotPlayer = collision.gameObject.GetComponent<RobotPlayer>(); //Player 스크립트 불러오기
                StartCoroutine(RobotPlayer.NearDamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                DamageSounds2();
                gameObject.SetActive(false);
            }
        }
    }
}