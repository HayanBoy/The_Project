using System.Collections;
using UnityEngine;

public class CannonExplosionInBackground : MonoBehaviour
{
    public int Damage;
    public int ExplosionType;
    public int TargetLayer;
    public bool Explosion;

    private void OnEnable()
    {
        StartCoroutine(AmmoExplosive1());
    }

    private void OnDisable()
    {
        Explosion = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetLayer == 20 && collision.gameObject.layer == 20)
        {
            //슬로리어스
            //슬로리어스 편대 함선1
            if (collision.CompareTag("Enemy Follow Ship"))
            {
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 left1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 right1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 left1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 right1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
        }

        if (TargetLayer == 19 && collision.gameObject.layer == 19)
        {
            //나리하
            //나리하 편대 함선1
            if (collision.CompareTag("Player Follow Ship"))
            {
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 left1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main1Left1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part1 right1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main1Right1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 left1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main2Left1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
            else if (collision.CompareTag("Slorius Formation Ship1 Debris2 main part2 right1"))
            {
                TearSloriusFormationShipInBackground TearSloriusFormationShipInBackground = collision.gameObject.transform.parent.GetComponent<TearSloriusFormationShipInBackground>();
                StartCoroutine(TearSloriusFormationShipInBackground.Main2Right1Damage(Damage, 0.0f));
                TearSloriusFormationShipInBackground.isExplosion = Explosion;
                HullSloriusFormationShipInBacground HullSloriusFormationShipInBacground = collision.gameObject.transform.parent.GetComponent<HullSloriusFormationShipInBacground>();
                StartCoroutine(HullSloriusFormationShipInBacground.DamageCharacter(Damage, 0.0f));
            }
        }
    }

    IEnumerator AmmoExplosive1()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
}