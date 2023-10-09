using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

public class KingBulletAmmo : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;
    int fireRange; //�Ѿ˹߻��� ���� �����Լ�
    float FireHeight = 0.05f; //�Ѿ˹߻� ���̹���

    public GameObject Explosion;
    public Transform ExplosionPos;

    public void SetDamage(int num)
    {
        damage = num;
    }


    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    private void Start()
    {
        Invoke("BulletFalse", 5.0f);
    }

    void Update()
    {
        //�Ѿ� �̵�
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 5);

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
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 5);

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
            GameObject Ex = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Ex, 5);
            Destroy(gameObject);
        }

        if (collision is CircleCollider2D && collision.CompareTag("Player") && collision.gameObject.layer == 3)
        {
            RobotPlayer RbPlayer = collision.gameObject.GetComponent<RobotPlayer>(); //RbPlayer ��ũ��Ʈ �ҷ�����
            StartCoroutine(RbPlayer.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            GameObject Ex = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Ex, 5);
            Destroy(gameObject);
        }
    }
}