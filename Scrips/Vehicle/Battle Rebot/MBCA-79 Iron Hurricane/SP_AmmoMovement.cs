using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

public class SP_AmmoMovement : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;
    int fireRange; //�Ѿ˹߻��� ���� �����Լ�
    float FireHeight = 0.01f; //�Ѿ˹߻� ���̹���

    //string Effect = "ricochetEffect";
    public Transform ricochetEffectPos;

    public GameObject RicochetEffect;
    GameObject rico;


    public bool isHit;

    public void SetDamage(int num)
    {
        damage = num;
    }

    public void Rico() // ��ź �߻� ������Ʈ Ǯ�� 
    {
        rico = Instantiate(RicochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation);
    }

    void Update()
    {
        //�Ѿ� �̵�
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 2);

            if(Time.timeScale == 1)
            {
                if (fireRange == 1)
                {
                    transform.Translate(transform.up * -FireHeight);
                }
                else if (fireRange == 2)
                {
                    transform.Translate(transform.up * FireHeight);
                }
            }
        }
        else
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 2);

            if(Time.timeScale == 1)
            {
                if (fireRange == 1)
                {
                    transform.Translate(transform.up * -FireHeight);
                }
                else if (fireRange == 2)
                {
                    transform.Translate(transform.up * FireHeight);
                }
            }         
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CircleCollider2D && collision.gameObject.tag == "Enemy" || collision is CircleCollider2D && collision.gameObject.layer == 27)
        {
            if (collision.gameObject.layer == 13)
            {
                if (isHit)
                    return;

                isHit = true;

                //ī��Ƽ - ���̿���4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                if (gameObject.activeSelf == true)
                {
                    Debug.Log("���������");
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                gameObject.SetActive(false);
                /* GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); *///��ź ��ƼŬ ����Ʈ ����
                Rico();
                Destroy(rico, 0.3f);
            }

            if (collision.gameObject.layer == 14)
            {
                if (isHit)
                    return;

                isHit = true;

                //Ÿ��ī - ���� - ����Ʈ��1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                if (gameObject.activeSelf == true)
                {
                    Debug.Log("���������");
                    StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                gameObject.SetActive(false);
                Rico();
                Destroy(rico, 0.3f);

                //GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); //��ź ��ƼŬ ����Ʈ ����
                //GameObject RicochetEffect = objectManager.Loader(Effect);
                //RicochetEffect.transform.position = ricochetEffectPos.position;
                //Destroy(RicochetEffect, 0.3f);
            }
        }

        /*if (collision is CapsuleCollider2D && collision.gameObject.layer == 7)
        {
            if (isHit)
                return;

            isHit = true;

            //Ÿ��ī - ���� - ����Ʈ��1
            BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
            if (gameObject.activeSelf == true)
            {
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            }
            gameObject.SetActive(false);
            Rico();
            //GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); //��ź ��ƼŬ ����Ʈ ����
            //GameObject RicochetEffect = objectManager.Loader(Effect);
            //RicochetEffect.transform.position = ricochetEffectPos.position;
            //Destroy(RicochetEffect, 0.3f);
        }*/

        isHit = false;
    }

}