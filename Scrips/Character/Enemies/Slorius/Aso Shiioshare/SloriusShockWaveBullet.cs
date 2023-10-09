using System.Collections;
using UnityEngine;

public class SloriusShockWaveBullet : MonoBehaviour
{
    int damage;
    public float DamagePerTime;

    float DamageTime;
    bool Direction;
    bool Direction2;

    public void SetDamageBeam(int num)
    {
        damage = num;
    }

    private void Start()
    {
        Invoke("AttackStop", 0.583f);
        Destroy(gameObject, 1);
    }

    void AttackStop()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        //Debug.Log(BeamDamageAction);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player"))
        {
            while (true)
            {
                DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                if (DamageTime >= DamagePerTime)
                {
                    Player Player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
                    Player.RicochetNum(1);
                    StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    Player.SetBeam(1); //빔 이펙트 값 전달
                    DamageTime = 0;
                }
                else
                    break;
            }
        }
        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            while (true)
            {
                DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                if (DamageTime >= DamagePerTime)
                {
                    RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player 스크립트 불러오기
                    RP.RicochetNum(1);
                    StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    RP.SetBeam(1); //빔 이펙트 값 전달
                    DamageTime = 0;
                }
                else
                    break;
            }
        }
    }
}
