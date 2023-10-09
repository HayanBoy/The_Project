using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class PlazmaBullet : MonoBehaviour
{
    VehicleObjectsManager vehicleObjectsManager;

    GameObject APCAmmo;
    public Transform APCAmmoPos;

    public float AmmoVelocity;
    public int damage;
    bool Direction;
    private int ThrowRandom;
    private float Distance;

    //private Shake shake; // 화면 흔들림 클래스 

    public void SetDamage(int num)
    {
        damage = num;
    }

    private void activeFalse() //화면밖으로 나갔을 때 비활성화 처리 (총알) 
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        vehicleObjectsManager = FindObjectOfType<VehicleObjectsManager>();
    }

    private void OnEnable()
    {
        Invoke("activeFalse", 0.5f);
    }

    void Active()
    {
        GameObject APCAmmo = vehicleObjectsManager.VehicleLoader("APCExplosion");
        APCAmmo.transform.position = APCAmmoPos.position;
        APCAmmo.transform.rotation = APCAmmoPos.rotation;
        ShieldExplosionDamage ShieldExplosionDamage = APCAmmo.gameObject.transform.GetComponent<ShieldExplosionDamage>();
        ShieldExplosionDamage.damage = damage;
        gameObject.SetActive(false);
    }

    void Update()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //칸타크리
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 27)
        {
            //카오티-자이오스4, 카오티-자이오스4 가시형, 카오티-자이오스4 강화형, 카오티-자이오스4 듀얼형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                {
                    Active();
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                {
                    Active();
                }
            }
            //카오티-자이오스4 방패형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                {
                    Active();
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                {
                    Active();
                }
            }

            //타이카-라이-쓰로트로1
            //몸통
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        Active();
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        Active();
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    Active();
                }
                else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                {
                    Active();
                }
            }

            //아트로-크로스파 390
            //몸통
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //미사일 발사대
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                    {
                        Active();
                    }
                }
            }
            //다리 및 기관포
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                Active();
            }
        }

        //슬로리어스
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 27)
        {
            //애이소 시이오셰어(앨리트)
            //몸통
            if (collision.CompareTag("Slorius, Aso Shiioshare body"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                Active();
            }

            if (collision.CompareTag("Shield"))
            {
                Active();
            }
        }

        //감염자
        if (collision.gameObject.layer == 16)
        {
            //일반 감염자
            if (collision.CompareTag("Infector, Body"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
                {
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    Active();
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨

            Active();
        }

        if (collision.gameObject.layer == 8)
        {
            if (collision.CompareTag("AmmoShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().Throwing = true;
            }
            if (collision.CompareTag("MagagineShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().Throwing = true;
            }
            if (collision.CompareTag("AmmoShellHuge"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellCase_Robot>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellCase_Robot>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellCase_Robot>().Throwing = true;
            }
            if (collision.CompareTag("AmmoShelEnemy"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<ShellMovement>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<ShellMovement>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<ShellMovement>().Throwing = true;
            }
            if (collision.CompareTag("Body Part"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("VM5 Throw"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<VM5Throw>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<VM5Throw>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<VM5Throw>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Kaoti-Jaios4"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Taika-Lai-Throtro1"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Sky Crane"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
        }
    }
}