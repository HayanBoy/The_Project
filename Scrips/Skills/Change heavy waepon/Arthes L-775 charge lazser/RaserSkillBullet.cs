using UnityEngine;
using System.Collections;

public class RaserSkillBullet : MonoBehaviour
{
    int damage;
    public float DamagePerTime;
    int BeamDamageAction; //빔 이펙트 값을 전달하는 변수

    float DamageTime;
    bool Direction;
    bool Direction2;

    public void SetDamageBeam(int num)
    {
        damage = num;
    }

    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    void Update()
    {
        //Debug.Log(BeamDamageAction);
        Destroy(gameObject, 3f); // 3초뒤 사라짐
    }

    //적과 충돌할 때 데미지 전달
    void OnTriggerStay2D(Collider2D collision)
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4Spear 스크립트 불러오기
                                KaotiJaios4Spear.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Spear.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 스크립트 불러오기
                                KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                                KaotiJaios4Spear.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Spear.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Spear.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Spear.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            kaotiJaios4.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                                StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                                HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                                HealthTaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                                StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                                Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                                Health2TaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                                HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                                HealthTaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                                Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                                Health2TaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                                HealthAtroCrossfa.RicochetNum(1);
                                StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                HealthAtroCrossfa.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //피격 애니메이션 발동을 저지하기 위해 이를 방지하는 목적
                                collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa 스크립트 불러오기
                                HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                                HealthAtroCrossfa.RicochetNum(1);
                                StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                                HealthAtroCrossfa.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //미사일 발사대
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //미사일 포대에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //다리 및 기관포
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //레이저에 닿았을 때에만 지속 데미지를 주기위한 시간 계산

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                while (true)
                {
                    DamageTime += Time.deltaTime;

                    if (DamageTime >= DamagePerTime)
                    {
                        //카크로스-타이제로스 1389
                        BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                        Boss.RicochetNum(1);
                        StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        Boss.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                        DamageTime = 0;
                    }
                    else
                        break;
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
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector 스크립트에다 타격 정보 전송
                                DamageTime = 0;
                            }
                        }
                        else
                            break;
                    }
                }
            }
            else if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector 스크립트에다 타격 정보 전송
                                DamageTime = 0;
                            }
                        }
                        else
                            break;
                    }
                }
            }
            else if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector 스크립트에다 타격 정보 전송
                                DamageTime = 0;
                            }
                        }
                        else
                            break;
                    }
                }
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
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                                {
                                    collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                                    collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                    HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                                    healthAsoShiioshare.ImHit = true;
                                    healthAsoShiioshare.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                    StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                    TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                                    StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
                                    DamageTime = 0;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                                {
                                    HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                                    healthAsoShiioshare.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                    TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                                    StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                                    DamageTime = 0;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                while (true)
                {
                    DamageTime += Time.deltaTime;

                    if (DamageTime >= DamagePerTime)
                    {
                        if (gameObject.activeSelf == true)
                        {
                            if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                            {
                                HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                                healthAsoShiioshare.ImHit = true;
                                healthAsoShiioshare.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                                StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                                StartCoroutine(tearAsoShiioshare.HeadDamage(damage, 0.0f));
                                DamageTime = 0;
                            }
                        }
                    }
                    else
                        break;
                }
            }

            if (collision.CompareTag("Shield"))
            {
                while (true)
                {
                    DamageTime += Time.deltaTime;

                    if (DamageTime >= DamagePerTime)
                    {
                        if (gameObject.activeSelf == true)
                        {
                            if (collision.CompareTag("Shield"))
                            {
                                ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //타격 부위의 부모 오브젝트의 ShieldAsoShiioshare 스크립트 불러오기
                                shieldAsoShiioshare.ShieldDamageExplosion(false);
                                StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                                DamageTime = 0;
                            }
                        }
                    }
                    else
                        break;
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
            {
                while (true)
                {
                    DamageTime += Time.deltaTime;

                    if (DamageTime >= DamagePerTime)
                    {
                        Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
                        //orozeper.RicochetNum(1);
                        StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        //orozeper.SetBeam(BeamDamageAction); //빔 이펙트 값 전달
                        //DamageTime = 0;
                    }
                    else
                        break;
                }
            }
        }
    }
}