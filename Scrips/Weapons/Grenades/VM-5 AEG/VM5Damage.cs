using System.Collections;
using UnityEngine;

public class VM5Damage : MonoBehaviour
{
    private Shake shake;
    public float ShakeStrength;
    public float ShakeTime;

    public int damage;
    float BTime = 0;
    bool Direction;
    private int ThrowRandom;
    private int ThrowRandom2;
    private float Distance;
    public float ExplosionTime;

    public int AsoShiioshareThrow;

    public GameObject ShieldDamage;
    public Transform ShieldDamagePos;

    private void Start()
    {
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
    }

    private void OnEnable()
    {
        BTime = 0;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        ThrowRandom = Random.Range(5, 10);
        ThrowRandom2 = Random.Range(5, 10);
    }

    private void OnDisable()
    {
        Shake.Instance.ShakeCamera(0, 0);
    }

    void Update()
    {
        if(BTime < ExplosionTime)
        {
            BTime += Time.deltaTime;

            if (BTime >= ExplosionTime)
            {
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Shake.Instance.ShakeCamera(ShakeStrength, ShakeTime);
            }
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                {
                    BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Spear.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                    StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                {
                    BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                    KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Spear.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                    StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                    StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                    StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                    StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 스크립트 불러오기
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                    StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                    HealthTaikaLaiThrotro1.RicochetNum(1);
                    StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                {
                    BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                    StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                    Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                    Health2TaikaLaiThrotro1.RicochetNum(1);
                    StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //타격 부위의 부모 오브젝트의 TearCrossfa 스크립트 불러오기
                            TearCrossfa.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            TearCrossfa.SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            TearCrossfa.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //타격 부위의 부모 오브젝트의 TearCrossfa 스크립트 불러오기
                            TearCrossfa.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            TearCrossfa.SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            TearCrossfa.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //피격 애니메이션 발동을 저지하기 위해 이를 방지하는 목적
                            collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //타격 부위의 부모 오브젝트의 TearCrossfa 스크립트 불러오기
                            TearCrossfa.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            TearCrossfa.SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            TearCrossfa.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //미사일 포대에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //타격 부위의 부모 오브젝트의 TearCrossfa 스크립트 불러오기
                            TearCrossfa.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            TearCrossfa.SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            TearCrossfa.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            StartCoroutine(TearCrossfa.LegRaillgunDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //타격 부위의 부모 오브젝트의 TearCrossfa 스크립트 불러오기
                            TearCrossfa.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            TearCrossfa.SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            TearCrossfa.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    var contactPoint1 = collision.transform.position.x; //충돌체의 위치 좌표
                    Distance = contactPoint1 - transform.position.x; //충돌체 좌표와 폭발 좌표간 거리 계산. 충돌체가 오른쪽에 있으면 플러스 값, 왼쪽에 있으면 마이너스 값이 산출된다.

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //폭발에 의해 멀리 날아가도록 변수 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector 스크립트에다 타격 정보 전송
                    }
                }
            }
            if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector 스크립트에다 타격 정보 전송
                    }
                }
            }
            if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector 스크립트에다 타격 정보 전송
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
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    //Debug.Log(Distance);

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //폭발에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow); //폭발에 의해 멀리 날아가도록 변수 전달
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                            tearAsoShiioshare.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            tearAsoShiioshare.SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                            tearAsoShiioshare.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                            healthAsoShiioshare.ImHit = true;
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                        Direction = false;
                    else
                        Direction = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow);
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                            tearAsoShiioshare.TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                            tearAsoShiioshare.SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                            tearAsoShiioshare.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                            StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow);
                        TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                        tearAsoShiioshare.SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        tearAsoShiioshare.LargeThrow = ThrowRandom; //더 멀리 날아가는 가변수 전달
                        StartCoroutine(tearAsoShiioshare.HeadDamage(damage, 0.0f));
                        HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                        healthAsoShiioshare.ImHit = true;
                        StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
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
                        shieldAsoShiioshare.ShieldDamageExplosion(true); //폭발 데미지를 받은 것으로 간주. 이럴 경우 방어능력으로 인해 더 낮은 데미지로 전달됨.
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                        GameObject ShieldDamageEffect = Instantiate(ShieldDamage, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                        Destroy(ShieldDamageEffect, 3);
                    }
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
        }

        //폭발에 의한 오브젝트 날아가기
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
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetThrow(ThrowRandom * ThrowRandom2);
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
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetThrow(ThrowRandom * ThrowRandom2);
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
                collision.gameObject.transform.GetComponent<ShellMovement>().SetThrow(ThrowRandom * ThrowRandom2);
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
                collision.gameObject.transform.GetComponent<VM5Throw>().SetThrow(ThrowRandom * ThrowRandom2);
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

                collision.gameObject.transform.GetComponent<DeathKaotiJaios4>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<DeathKaotiJaios4>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<DeathKaotiJaios4>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Taika-Lai-Throtro1"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<DeathTaikaLaiThrotro1>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<DeathTaikaLaiThrotro1>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<DeathTaikaLaiThrotro1>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Sky Crane"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //충돌지점 x축과 폭발지점 x축 위치 비교
                    Direction = false;
                else
                    Direction = true;

                collision.gameObject.transform.GetComponent<DebrisSkyCrane>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<DebrisSkyCrane>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<DebrisSkyCrane>().Throwing = true;
            }
        }
    }
}