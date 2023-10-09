using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDamage : MonoBehaviour
{
    Coroutine KaotiJaios4Spear;

    public int Damage; //카오티자이오스4가시형 근접 데미지

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
        //칸타크리, 카오티-자이오스 4 가시형과의 충돌
        if (collision is BoxCollider2D && collision.gameObject.tag == "Proximity Player")
        {
            Player Player = collision.gameObject.transform.parent.GetComponent<Player>(); //Player 스크립트 불러오기

            if (KaotiJaios4Spear == null)
                KaotiJaios4Spear = StartCoroutine(Player.NearDamageCharacter(Damage, 1.0f)); //플레이어에게 근접 데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //칸타크리, 카오티-자이오스 4 가시형과의 충돌해제
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