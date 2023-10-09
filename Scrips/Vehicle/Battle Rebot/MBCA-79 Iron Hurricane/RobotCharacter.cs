using System.Collections;
using UnityEngine;

public abstract class RobotCharacter : MonoBehaviour
{
    public float maxHitPoints;
    public float startingHitPoints;

    public float maxArmor;
    public float startingArmor;

    //ĳ���� ����
    public virtual void DestroyRobot()
    {
        gameObject.SetActive(false);
    }

    public abstract IEnumerator DamageCharacter(int damage, float interval);
}