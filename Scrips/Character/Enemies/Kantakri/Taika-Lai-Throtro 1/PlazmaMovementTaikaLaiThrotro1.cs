using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmaMovementTaikaLaiThrotro1 : MonoBehaviour
{
    public float AmmoVelocity;
    public float DestroyTime;
    public GameObject Explosion;
    public Transform ExplosionPos; //�Ѿ� ���� ��ǥ

    public int damage;

    //��ũ��Ʈ KaotiJaios4�κ��� ���޹��� ������
    public void SetDamage(int num)
    {
        damage = num;
    }

    void Start()
    {
        Invoke("OnDestroy", DestroyTime); //Invoke�� ������ �Լ����� �ǹ�, ���ڴ� �����ð�.
    }

    void Update()
    {
        //�Ѿ� �̵�
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    //���� �浹�� �� ������ ����
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Player")
        {
            Player Player = collision.gameObject.GetComponent<Player>(); //Player ��ũ��Ʈ �ҷ�����
            Player.RicochetNum(1);
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            GameObject Explosive = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Explosive, 5);
            Destroy(gameObject);
        }

        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player ��ũ��Ʈ �ҷ�����
            RP.RicochetNum(1);
            StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            GameObject Explosive = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Explosive, 5);
            Destroy(gameObject);
        }
    }
}
