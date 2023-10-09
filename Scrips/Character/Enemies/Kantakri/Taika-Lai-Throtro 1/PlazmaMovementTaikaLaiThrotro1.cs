using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmaMovementTaikaLaiThrotro1 : MonoBehaviour
{
    public float AmmoVelocity;
    public float DestroyTime;
    public GameObject Explosion;
    public Transform ExplosionPos; //총알 생성 좌표

    public int damage;

    //스크립트 KaotiJaios4로부터 전달받은 데미지
    public void SetDamage(int num)
    {
        damage = num;
    }

    void Start()
    {
        Invoke("OnDestroy", DestroyTime); //Invoke는 실행할 함수명을 의미, 숫자는 지연시간.
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

    void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    //적과 충돌할 때 데미지 전달
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Player")
        {
            Player Player = collision.gameObject.GetComponent<Player>(); //Player 스크립트 불러오기
            Player.RicochetNum(1);
            StartCoroutine(Player.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            GameObject Explosive = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Explosive, 5);
            Destroy(gameObject);
        }

        if (collision is CircleCollider2D && collision.CompareTag("Vehicle"))
        {
            RobotPlayer RP = collision.gameObject.GetComponent<RobotPlayer>(); //Player 스크립트 불러오기
            RP.RicochetNum(1);
            StartCoroutine(RP.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            GameObject Explosive = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
            Destroy(Explosive, 5);
            Destroy(gameObject);
        }
    }
}
