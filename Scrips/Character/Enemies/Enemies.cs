using System.Collections;
using UnityEngine;

public class Enemies : Character
{
    float hitPoints;
    float armor;
    float Ricochet;

    //���� Ÿ���� �޾��� ���� ������ ����
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            Ricochet = Random.Range(0, 5);

            if (Ricochet != 0)
            {
                hitPoints = hitPoints - (damage / armor);

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
            else if(Ricochet == 0)
            {
                Debug.Log("��!");
            }
        }
    }
}