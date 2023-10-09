using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class KingBulletAmmo : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;
    int fireRange; //총알발사의 높이 랜덤함수
    float FireHeight = 0.05f; //총알발사 높이범위

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
        //총알 이동
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
            Player Player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            GameObject Ex = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Ex, 5);
            Destroy(gameObject);
        }

        if (collision is CircleCollider2D && collision.CompareTag("Player") && collision.gameObject.layer == 3)
        {
            RobotPlayer RbPlayer = collision.gameObject.GetComponent<RobotPlayer>(); //RbPlayer 스크립트 불러오기
            StartCoroutine(RbPlayer.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            GameObject Ex = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Ex, 5);
            Destroy(gameObject);
        }
    }
}