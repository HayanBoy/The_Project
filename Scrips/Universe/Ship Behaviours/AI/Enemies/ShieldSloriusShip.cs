using System.Collections;
using UnityEngine;

public class ShieldSloriusShip : MonoBehaviour
{
    [Header("방어막 정보")]
    public bool Flagship;
    public float StartShieldPoints;
    public float ShieldPoints;
    public int ShieldArmor;
    public bool ShieldExplosion = false; //방어막이 피격 받았을 때, 해당 피격 요소가 폭발인지 구별하기 위한 요소

    [Header("콜라이더")]
    public GameObject Collider;


    [Header("방어막 연출")]
    public GameObject ShieldPowerRipplePrefab; //강화 방어막 타격 이펙트
    public GameObject ShieldRipplePrefab; //방어막 타격 이펙트

    //적이 타격을 받았을 때의 데미지 적용
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