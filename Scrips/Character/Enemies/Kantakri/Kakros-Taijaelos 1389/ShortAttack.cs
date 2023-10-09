using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

public class ShortAttack : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;
    int fireRange; //�Ѿ˹߻��� ���� �����Լ�

    public bool isHit;


    public void SetDamage(int num)
    {
        damage = num;
    }

    private void Start()
    {
        Debug.Log("����");
        //Invoke("BulletFalse", 1.0f);
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

    private void BulletFalse() 
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player") && collision.gameObject.layer == 6)
        {
            Player Player = collision.gameObject.GetComponent<Player>(); //Player ��ũ��Ʈ �ҷ�����
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�         
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
        }

        if (collision is CircleCollider2D && collision.CompareTag("Player") && collision.gameObject.layer == 3)
        {
            RobotPlayer RbPlayer = collision.gameObject.GetComponent<RobotPlayer>(); //RbPlayer ��ũ��Ʈ �ҷ�����
            StartCoroutine(RbPlayer.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
        }

        /*if (collision is BoxCollider2D && collision.gameObject.tag == "Player" && collision.gameObject.layer == 4)
        {
            BarrierPlayer BPlayer = collision.gameObject.GetComponent<BarrierPlayer>(); //RbPlayer ��ũ��Ʈ �ҷ�����
            StartCoroutine(BPlayer.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
        }

        if (collision is BoxCollider2D && collision.gameObject.tag == "Player" && collision.gameObject.layer == 12)
        {
            SupplyArmy SupArmy = collision.gameObject.GetComponent<SupplyArmy>(); //RbPlayer ��ũ��Ʈ �ҷ�����
            StartCoroutine(SupArmy.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }*/
    }
}