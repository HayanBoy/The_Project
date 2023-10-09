using System.Collections;
using UnityEngine;

public class ShieldSloriusShip : MonoBehaviour
{
    [Header("�� ����")]
    public bool Flagship;
    public float StartShieldPoints;
    public float ShieldPoints;
    public int ShieldArmor;
    public bool ShieldExplosion = false; //���� �ǰ� �޾��� ��, �ش� �ǰ� ��Ұ� �������� �����ϱ� ���� ���

    [Header("�ݶ��̴�")]
    public GameObject Collider;


    [Header("�� ����")]
    public GameObject ShieldPowerRipplePrefab; //��ȭ �� Ÿ�� ����Ʈ
    public GameObject ShieldRipplePrefab; //�� Ÿ�� ����Ʈ

    //���� Ÿ���� �޾��� ���� ������ ����
    public IEnumerator DamageShieldCharacter(int damage, float interval, Vector2 Transform)
    {
        while (true)
        {
            if (ShieldExplosion == false)
            {
                GameObject Ripple = Instantiate(ShieldRipplePrefab, transform.position, transform.rotation);
                Ripple.GetComponent<ShieldRipples>().ShieldDefenceEffect(Transform);
                ShieldPoints = ShieldPoints - damage;
            }
            else
            {
                GameObject PowerRipple = Instantiate(ShieldPowerRipplePrefab, transform.position, transform.rotation);
                PowerRipple.GetComponent<ShieldRipples>().ShieldDefenceEffect(Transform);
                ShieldPoints = ShieldPoints - damage / ShieldArmor;
            }

            if (ShieldPoints <= float.Epsilon)
            {
                Collider.GetComponent<PolygonCollider2D>().enabled = false;
                Collider.layer = 0;
                if (Flagship == true)
                {
                    gameObject.GetComponent<HullSloriusFlagship1>().ShieldDown = true;
                    gameObject.GetComponent<TearSloriusFlagship1>().ShieldDown = true;
                }
                else
                {
                    gameObject.GetComponent<HullSloriusFormationShip1>().ShieldDown = true;
                    gameObject.GetComponent<TearSloriusFormationShip1>().ShieldDown = true;
                }
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
    }
}