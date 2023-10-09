using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class SP_AmmoMovement : MonoBehaviour
{
    public float AmmoVelocity;
  
    int damage;
    int fireRange; //총알발사의 높이 랜덤함수
    float FireHeight = 0.01f; //총알발사 높이범위

    //string Effect = "ricochetEffect";
    public Transform ricochetEffectPos;

    public GameObject RicochetEffect;
    GameObject rico;


    public bool isHit;

    public void SetDamage(int num)
    {
        damage = num;
    }

    public void Rico() // 도탄 발사 오브젝트 풀링 
    {
        rico = Instantiate(RicochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation);
    }

    void Update()
    {
        //총알 이동
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

                //카오티 - 자이오스4
                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                if (gameObject.activeSelf == true)
                {
                    Debug.Log("데미지줬다");
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                gameObject.SetActive(false);
                /* GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); *///도탄 파티클 이펙트 생성
                Rico();
                Destroy(rico, 0.3f);
            }

            if (collision.gameObject.layer == 14)
            {
                if (isHit)
                    return;

                isHit = true;

                //타이카 - 라이 - 쓰로트로1
                HealthTaikaLaiThrotro1 TaikaLaiThrotro1 = collision.gameObject.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                if (gameObject.activeSelf == true)
                {
                    Debug.Log("데미지줬다");
                    StartCoroutine(TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                gameObject.SetActive(false);
                Rico();
                Destroy(rico, 0.3f);

                //GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); //도탄 파티클 이펙트 생성
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

            //타이카 - 라이 - 쓰로트로1
            BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
            if (gameObject.activeSelf == true)
            {
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            }
            gameObject.SetActive(false);
            Rico();
            //GameObject RicochetEffect = Instantiate(ricochetEffect, ricochetEffectPos.position, ricochetEffectPos.rotation); //도탄 파티클 이펙트 생성
            //GameObject RicochetEffect = objectManager.Loader(Effect);
            //RicochetEffect.transform.position = ricochetEffectPos.position;
            //Destroy(RicochetEffect, 0.3f);
        }*/

        isHit = false;
    }

}