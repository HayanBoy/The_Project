using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("체력")]
    public float maxHitPoints; //최대 체력
    public float startingHitPoints; //시작 체력

    [Header("종족별 방어")]
    public float SloriusShieldArmor; //슬로리어스 전용 방어막의 피해 흡수율. 1~100까지의 수치가 있으며, 플레이어의 공격을 해당 방어력의 수치 퍼센테이지 만큼 감소시킨다.
    public int KantakriRicochet; //칸타크리 전용 도탄 확률 수치. 이 수치가 높을 수록 더 높은 확률로 도탄을 일으킨다.

    public float maxArmor;
    public float startingArmor;

    //캐릭터 삭제
    public virtual void KillCharacter()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public abstract IEnumerator DamageCharacter(int damage, float interval);
}