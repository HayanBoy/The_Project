using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class ArcAmmo : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;

    public GameObject Explosion;
    public Transform ExplosionPos;
    public bool isHit;

    public void SetDamage(int num)
    {
        damage = num;
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