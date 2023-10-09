using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

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
        //총알 이동
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
        }
    }

    //적과 충돌할 때 데미지 전달
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CircleCollider2D && collision.gameObject.layer == 7 || collision is CircleCollider2D && collision.gameObject.layer == 27)
        {
            if (collision.gameObject.tag == "Kantakri, Kaoti-Jaios 4")
            {
                //카오티 - 자이오스4
                //Debug.Log("메탈불릿 닿았다");
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨    
            }

            if (collision.gameObject.tag == "Kantakri, Taika-Lai-Throtro 1")
            {
                //타이카 - 라이 - 쓰로트로1
                //Debug.Log("메탈불릿 닿았다22222");
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            }
        }
    }
}