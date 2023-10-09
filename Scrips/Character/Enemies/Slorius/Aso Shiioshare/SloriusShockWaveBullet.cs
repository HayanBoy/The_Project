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
                DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                if (DamageTime >= DamagePerTime)
                {
                    Player Player = collision.gameObject.GetComponent<Player>(); //Player ��ũ��Ʈ �ҷ�����
                    Player.RicochetNum(1);
                    StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    Player.SetBeam(1); //�� ����Ʈ �� ����
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
                DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                if (DamageTime >= DamagePerTime)
                {
                    RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player ��ũ��Ʈ �ҷ�����
                    RP.RicochetNum(1);
                    StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    RP.SetBeam(1); //�� ����Ʈ �� ����
                    DamageTime = 0;
                }
                else
                    break;
            }
        }
    }
}
