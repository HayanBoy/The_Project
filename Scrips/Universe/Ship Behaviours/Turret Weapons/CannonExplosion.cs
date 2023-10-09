using System.Collections;
using UnityEngine;

public class CannonExplosion : MonoBehaviour
{
    public int Damage;
    public int ExplosionType; //1 :  탄환, 2 : 미사일, 3 : 함재기 미사일, 4 : 5초 이상 머물경우
    public int TargetLayer;
    public bool Explosion;
    private string DeleteName;

    private void OnEnable()
    {
        this.gameObject.layer = 8;

        if (ExplosionType == 2)
            this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(AmmoExplosive1());
    }

    private void OnDisable()
    {
        Explosion = false;
    }

    public void SetDamage(string Delete)
    {
        DeleteName = Delete;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetLayer == 7 && collision.gameObject.layer == 7) //타격 대상이 컨트로스
        {
            //슬로리어스
            if (collision.CompareTag("Slorius Ship Shield"))
            {
                ShieldSloriusShip ShieldSloriusShip = collision.gameObject.transform.parent.GetComponent<ShieldSloriusShip>();
                if (ExplosionType == 2)
                    ShieldSloriusShip.ShieldExplosion = true;
                else
                    ShieldSloriusShip.ShieldExplosion = false;
                StartCoroutine(ShieldSloriusShip.DamageShieldCharacter(Damage, 0.0f, transform.position));
            }
            //슬로리어스 기함1
            if (collision.CompareTag("Slorius Flag Ship"))
            {
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if(collision.CompareTag("Slorius Flagship1 Debris2 main part1 left1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part1 left2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part1 right1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part1 right2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if(collision.CompareTag("Slorius Flagship1 Debris2 main part2 left1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part2 left2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part2 right1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part2 right2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part3 left1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Flagship1 Debris2 main part3 right1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }

            //슬로리어스 편대 함선1
            if (collision.CompareTag("Slorius Follow Ship"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if(collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 left1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 right1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 left1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 right1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }

            //칸타크리
            //칸타크리 기함1
            if (collision.CompareTag("Kantakri Flag Ship1"))
            {
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flag Ship2"))
            {
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if(collision.CompareTag("Kantakri Flagship Debris main part1 module1(Left)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part1 module2(Left)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module1(Left)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module2(Left)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module3(Left)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part1 module1(Right)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part1 module2(Right)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module1(Right)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module2(Right)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Flagship Debris main part2 module3(Right)"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }

            //칸타크리 편대 함선1
            if (collision.CompareTag("Kantakri Follow Ship1"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Follow Ship2"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Formation ship1 main part1 module1(Left)"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Formation ship1 main part1 module2(Left)"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Formation ship1 main part1 module1(Right)"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Kantakri Formation ship1 main part1 module2(Right)"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
        }

        if (TargetLayer == 6 && collision.gameObject.layer == 6)  //타격 대상이 나리하 인류연합
        {
            //나리하
            //나리하 기함1
            if (collision.CompareTag("Player Flag Ship"))
            {
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if(collision.CompareTag("Player Flagship1 Debris main part1 left1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part1 left2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part1 right1"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part1 right2"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main1Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part2 left"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part2 right"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Left2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part3 left"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part3 right"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main2Right2Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part4 left"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Left1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Flagship1 Debris main part4 right"))
            {
                TearSloriusFlagship1 TearSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFlagship1>();
                StartCoroutine(TearSloriusFlagship1.Main3Right1Damage(Damage, 0.0f));
                TearSloriusFlagship1.isExplosion = Explosion;
                HullSloriusFlagship1 HullSloriusFlagship1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFlagship1>();
                StartCoroutine(HullSloriusFlagship1.DamageCharacter(Damage, 0.0f));
            }

            //나리하 편대 함선1
            if (collision.CompareTag("Player Follow Ship"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 left1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 right1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 left1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 right1"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }

            //나리하 방패 함선1
            if (collision.CompareTag("Player Shield Ship1"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part1 left"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part1 right"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part2 left"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part2 right"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part2 left module"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main3Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Shield Ship1 main part2 right module"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main3Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }

            //나리하 우주모함1
            if (collision.CompareTag("Player Carrier1"))
            {
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main part1 left"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main part1 right"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main part2 left"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main part3 right"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main down road part left"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main3Left1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Player Carrier1 main down road part right"))
            {
                TearSloriusFormationShip1 TearSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<TearSloriusFormationShip1>();
                StartCoroutine(TearSloriusFormationShip1.Main3Right1Damage(Damage, 0.0f));
                TearSloriusFormationShip1.isExplosion = Explosion;
                HullSloriusFormationShip1 HullSloriusFormationShip1 = collision.gameObject.transform.parent.parent.GetComponent<HullSloriusFormationShip1>();
                StartCoroutine(HullSloriusFormationShip1.DamageCharacter(Damage, 0.0f));
            }
            this.gameObject.layer = 0;
        }
    }

    IEnumerator AmmoExplosive1()
    {
        if (ExplosionType == 1)
            yield return new WaitForSeconds(0.2f);
        else if (ExplosionType == 2)
        {
            yield return new WaitForSeconds(0.05f);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(3f);
        }
        else if (ExplosionType == 3)
        {
            yield return new WaitForSeconds(2);
        }
        else if (ExplosionType == 4)
        {
            yield return new WaitForSeconds(0.05f);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(6f);
        }

        ShipAmmoObjectPool.instance.Deleter(DeleteName);
        this.gameObject.SetActive(false);
    }
}