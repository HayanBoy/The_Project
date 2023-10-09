using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

public class MetalSkillBullet : MonoBehaviour
{
    public float AmmoVelocity;

    int damage;

    public void SetDamage(int num)
    {
        damage = num;
    }

    void Update()
    {
        Destroy(gameObject, 5f);
        //�Ѿ� �̵�
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
        }
    }

    //���� �浹�� �� ������ ����
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CircleCollider2D && collision.gameObject.layer == 7 || collision is CircleCollider2D && collision.gameObject.layer == 27)
        {
            if (collision.gameObject.tag == "Kantakri, Kaoti-Jaios 4")
            {
                //ī��Ƽ - ���̿���4
                //Debug.Log("��Ż�Ҹ� ��Ҵ�");
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�    
            }

            if (collision.gameObject.tag == "Kantakri, Taika-Lai-Throtro 1")
            {
                //Ÿ��ī - ���� - ����Ʈ��1
                //Debug.Log("��Ż�Ҹ� ��Ҵ�22222");
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            }
        }
    }
}