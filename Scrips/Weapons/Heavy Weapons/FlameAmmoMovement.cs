using System.Collections;
using UnityEngine;

public class FlameAmmoMovement : MonoBehaviour
{
    public float AmmoVelocity;
    public float DisappearTime;
    public int KantakriDividedDamage;
    public int CreatureAddDamage;
    public float KantakriFlameDamageTime;
    public float SloriusFlameDamageTime;
    public float TaitrokiFlameDamageTime;
    public float KantakriFlameDamagePerTime;
    public float SloriusFlameDamagePerTime;
    public float TaitrokiFlameDamagePerTime;
    int damage;

    public void SetDamage(int num)
    {
        damage = num;
    }

    void Update()
    {
        //총알 이동
        if (transform.rotation.y == 0)
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
        else
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        StartCoroutine(bulletFalse());
    }


    IEnumerator bulletFalse()
    {
        yield return new WaitForSeconds(DisappearTime);
        gameObject.SetActive(false);
    }

    //적과 충돌할 때 데미지 전달
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
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Spear.FlameBody();
                            KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Spear.FlameBody();
                            KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameGun();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Spear.FlameGun();
                        KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameGun();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage / KantakriDividedDamage, 0)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameWheel();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage / KantakriDividedDamage, 0)); //바퀴에다 데미지 전달
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Spear.FlameWheel();
                        KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage / KantakriDividedDamage, 0)); //바퀴에다 데미지 전달
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameWheel();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //카오티-자이오스4 방패형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameGun();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameGun();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage / KantakriDividedDamage, 0)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameWheel();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage / KantakriDividedDamage, 0)); //바퀴에다 데미지 전달
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameWheel();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }

            //타이카-라이-쓰로트로1
            //몸통
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage / KantakriDividedDamage, 0)); //엔진에다 데미지 전달
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            HealthTaikaLaiThrotro1.FlameBody();
                            HealthTaikaLaiThrotro1.FlameEngine();
                            HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage / KantakriDividedDamage, 0)); //엔진에다 데미지 전달
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            Health2TaikaLaiThrotro1.FlameBody();
                            Health2TaikaLaiThrotro1.FlameEngine();
                            Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            HealthTaikaLaiThrotro1.FlameBody();
                            HealthTaikaLaiThrotro1.FlameEngine();
                            HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            Health2TaikaLaiThrotro1.FlameBody();
                            Health2TaikaLaiThrotro1.FlameEngine();
                            Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 스크립트 불러오기
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                        HealthTaikaLaiThrotro1.FlameGun();
                        HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage / KantakriDividedDamage, 0)); //총에다 데미지 전달
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                        Health2TaikaLaiThrotro1.FlameGun();
                        Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }

            //아트로-크로스파 390
            //몸통
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameBody();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //피격 애니메이션 발동을 저지하기 위해 이를 방지하는 목적
                            collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameBody();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //미사일 발사대
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MLBDamage(damage / KantakriDividedDamage, 0)); //미사일 포대에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                }
            }
            //다리 및 기관포
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.LegDamage(damage / KantakriDividedDamage, 0)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameLegs();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage / KantakriDividedDamage, 0)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameGun();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                if (gameObject.activeSelf == true)
                {
                    Boss.RicochetNum(1);
                    StartCoroutine(Boss.DamageCharacter(damage / KantakriDividedDamage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
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
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.FlameHitTime = TaitrokiFlameDamageTime;
                        healthInfector.FlameBody();
                        healthInfector.FlameArm();
                        healthInfector.FlameDamgeStart((damage / CreatureAddDamage) / 2, TaitrokiFlameDamagePerTime);
                        StartCoroutine(healthInfector.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }
            else if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
                {
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.FlameHitTime = TaitrokiFlameDamageTime;
                        healthInfector.FlameHead();
                        healthInfector.FlameDamgeStart((damage / CreatureAddDamage) / 2, TaitrokiFlameDamagePerTime);
                        StartCoroutine(healthInfector.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }
            else if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.FlameHitTime = TaitrokiFlameDamageTime;
                        healthInfector.FlameLegs();
                        healthInfector.FlameDamgeStart((damage / CreatureAddDamage) / 2, TaitrokiFlameDamagePerTime);
                        StartCoroutine(healthInfector.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기

            if (gameObject.activeSelf == true)
                StartCoroutine(orozeper.DamageCharacter(damage, 0)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                        {
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                            healthAsoShiioshare.FlameHitTime = SloriusFlameDamageTime;
                            healthAsoShiioshare.FlameBody();
                            healthAsoShiioshare.FlameArm();
                            healthAsoShiioshare.FlameDamgeStart((damage / CreatureAddDamage) / 2, SloriusFlameDamagePerTime);
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage * CreatureAddDamage, 0));
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                            healthAsoShiioshare.FlameHitTime = SloriusFlameDamageTime;
                            healthAsoShiioshare.FlameLegs();
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                    {
                        HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                        healthAsoShiioshare.FlameHitTime = SloriusFlameDamageTime;
                        healthAsoShiioshare.FlameHead();
                        healthAsoShiioshare.FlameDamgeStart((damage / CreatureAddDamage) / 2, SloriusFlameDamagePerTime);
                        StartCoroutine(healthAsoShiioshare.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }

            if (collision.CompareTag("Shield"))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Shield"))
                    {
                        ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //타격 부위의 부모 오브젝트의 ShieldAsoShiioshare 스크립트 불러오기
                        shieldAsoShiioshare.ShieldDamageExplosion(false);
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage / KantakriDividedDamage, 0));
                    }
                }
            }
        }
    }
}