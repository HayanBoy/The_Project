using System.Collections;
using UnityEngine;

public class CannonTower : MonoBehaviour
{
    Animator animator;

    int Fire;
    int FireEffectCount;
    int FireAction;
    int WaitTime;

    bool UsingAction = false;
    bool UsingFire = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Action());
        StartCoroutine(Attack());
        StartCoroutine(AttackCount());
        StartCoroutine(Wait());
    }

    IEnumerator Action()
    {
        while(true)
        {
            FireAction = Random.Range(0, 5);
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            Fire = Random.Range(0, 3);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Wait()
    {
        while (true)
        {
            WaitTime = Random.Range(0, 3);
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator AttackCount()
    {
        while (true)
        {
            FireEffectCount = Random.Range(0, 2);
            yield return new WaitForSeconds(2f);
        }
    }

    void Update()
    {
        if(WaitTime == 0)
        {
            StartCoroutine(ActionMove());
        }

        StartCoroutine(AttackCannon());
    }

    IEnumerator AttackCannon()
    {
        if (Fire == 0 && UsingFire == false)
        {
            if (FireEffectCount == 0)
            {
                UsingFire = true;
                animator.SetBool("Fire, Cannon Tower", true);
                animator.SetBool("Fire effect1, Cannon Tower", true);
                yield return new WaitForSeconds(0.83f);
                animator.SetBool("Fire, Cannon Tower", false);
                animator.SetBool("Fire effect1, Cannon Tower", false);
                UsingFire = false;
            }
            else if (FireEffectCount == 1)
            {
                UsingFire = true;
                animator.SetBool("Fire, Cannon Tower", true);
                animator.SetBool("Fire effect2, Cannon Tower", true);
                yield return new WaitForSeconds(0.83f);
                animator.SetBool("Fire, Cannon Tower", false);
                animator.SetBool("Fire effect2, Cannon Tower", false);
                UsingFire = false;
            }
        }
    }

    IEnumerator ActionMove()
    {
        if (FireAction == 0 && UsingAction == false)
        {
            UsingAction = true;
            animator.SetBool("Fire action2, Cannon Tower", false);
            animator.SetBool("Fire action3, Cannon Tower", false);
            animator.SetBool("Fire action4, Cannon Tower", false);
            animator.SetBool("Fire action1, Cannon Tower", true);
            yield return new WaitForSeconds(8f);
            UsingAction = false;
        }
        else if (FireAction == 1 && UsingAction == false)
        {
            UsingAction = true;
            animator.SetBool("Fire action1, Cannon Tower", false);
            animator.SetBool("Fire action3, Cannon Tower", false);
            animator.SetBool("Fire action4, Cannon Tower", false);
            animator.SetBool("Fire action2, Cannon Tower", true);
            yield return new WaitForSeconds(6.5f);
            UsingAction = false;
        }
        else if (FireAction == 2 && UsingAction == false)
        {
            UsingAction = true;
            animator.SetBool("Fire action1, Cannon Tower", false);
            animator.SetBool("Fire action2, Cannon Tower", false);
            animator.SetBool("Fire action4, Cannon Tower", false);
            animator.SetBool("Fire action3, Cannon Tower", true);
            yield return new WaitForSeconds(6f);
            UsingAction = false;
        }
        else if (FireAction == 3 && UsingAction == false)
        {
            UsingAction = true;
            animator.SetBool("Fire action1, Cannon Tower", false);
            animator.SetBool("Fire action2, Cannon Tower", false);
            animator.SetBool("Fire action3, Cannon Tower", false);
            animator.SetBool("Fire action4, Cannon Tower", true);
            yield return new WaitForSeconds(6.5f);
            UsingAction = false;
        }
    }
}
