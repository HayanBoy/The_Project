using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("ü��")]
    public float maxHitPoints; //�ִ� ü��
    public float startingHitPoints; //���� ü��

    [Header("������ ���")]
    public float SloriusShieldArmor; //���θ�� ���� ���� ���� �����. 1~100������ ��ġ�� ������, �÷��̾��� ������ �ش� ������ ��ġ �ۼ������� ��ŭ ���ҽ�Ų��.
    public int KantakriRicochet; //ĭŸũ�� ���� ��ź Ȯ�� ��ġ. �� ��ġ�� ���� ���� �� ���� Ȯ���� ��ź�� ����Ų��.

    public float maxArmor;
    public float startingArmor;

    //ĳ���� ����
    public virtual void KillCharacter()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public abstract IEnumerator DamageCharacter(int damage, float interval);
}