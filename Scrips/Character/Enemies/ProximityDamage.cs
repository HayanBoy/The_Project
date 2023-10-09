using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDamage : MonoBehaviour
{
    Coroutine KaotiJaios4Spear;

    public int Damage; //ī��Ƽ���̿���4������ ���� ������

    private void OnEnable()
    {
        if (BattleSave.Save1.MissionLevel == 1)
        {
            Damage = 20;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            Damage = 28;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            Damage = 32;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ĭŸũ��, ī��Ƽ-���̿��� 4 ���������� �浹
        if (collision is BoxCollider2D && collision.gameObject.tag == "Proximity Player")
        {
            Player Player = collision.gameObject.transform.parent.GetComponent<Player>(); //Player ��ũ��Ʈ �ҷ�����

            if (KaotiJaios4Spear == null)
                KaotiJaios4Spear = StartCoroutine(Player.NearDamageCharacter(Damage, 1.0f)); //�÷��̾�� ���� ������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //ĭŸũ��, ī��Ƽ-���̿��� 4 ���������� �浹����
        if (collision is BoxCollider2D && collision.gameObject.tag == "Proximity Player")
        {
            if (KaotiJaios4Spear != null)
            {
                StopCoroutine(KaotiJaios4Spear);
                KaotiJaios4Spear = null;
            }
        }
    }
}