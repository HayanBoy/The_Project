using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class ExplosionBuckShotBullet : MonoBehaviour
{
    public float AmmoVelocity;

    int damage;
    public int DivideBulletDamage;
    public float DivideTime;
    bool Direction;

    public GameObject BuckBullet1;
    public GameObject BuckBullet2;
    public GameObject BuckBullet3;
    public GameObject BuckBullet4;
    public GameObject BuckBullet5;
    public Transform BuckBulletPos;

    GameObject DivideExplosion;
    public Transform DivideExplosionPos;
    ObjectManager objectManager;

    public GameObject MB_Shell1;
    public Transform MB_Shell1Pos;
    public GameObject MB_Shell2;
    public Transform MB_Shell2Pos;
    public GameObject MB_Shell3;
    public Transform MB_Shell3Pos;

    public AudioClip Hydra56MainAmmoBoom;


    public void SetDamage(int num)
    {
        damage = num;
    }

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        Destroy(gameObject, DivideTime+0.01f);
        Invoke("BuckShot", DivideTime);
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


    void BuckShot()
    {
        MetalSkillBullet_Shell();
        SoundManager.instance.SFXPlay("DT-37 Fire Sound", Hydra56MainAmmoBoom);

        GameObject bullet1 = Instantiate(BuckBullet1);
        bullet1.transform.position = BuckBulletPos.position;
        bullet1.transform.rotation = BuckBulletPos.rotation; // 회전 초기화, 회전 값 0 
        bullet1.GetComponent<BuckBullet1>().SetDamage(DivideBulletDamage); //총알에다 데미지 전달

        GameObject bullet2 = Instantiate(BuckBullet2);
        bullet2.transform.position = BuckBulletPos.position;
        bullet2.transform.rotation = BuckBulletPos.rotation; // 회전 초기화, 회전 값 0 
        bullet2.GetComponent<BuckBullet2>().SetDamage(DivideBulletDamage); //총알에다 데미지 전달

        GameObject bullet3 = Instantiate(BuckBullet3);
        bullet3.transform.position = BuckBulletPos.position;
        bullet3.transform.rotation = BuckBulletPos.rotation; // 회전 초기화, 회전 값 0 
        bullet3.GetComponent<BuckBullet3>().SetDamage(DivideBulletDamage); //총알에다 데미지 전달

        GameObject bullet4 = Instantiate(BuckBullet4);
        bullet4.transform.position = BuckBulletPos.position;
        bullet4.transform.rotation = BuckBulletPos.rotation; // 회전 초기화, 회전 값 0 
        bullet4.GetComponent<BuckBullet4>().SetDamage(DivideBulletDamage); //총알에다 데미지 전달

        GameObject bullet5 = Instantiate(BuckBullet5);
        bullet5.transform.position = BuckBulletPos.position;
        bullet5.transform.rotation = BuckBulletPos.rotation; // 회전 초기화, 회전 값 0 
        bullet5.GetComponent<BuckBullet5>().SetDamage(DivideBulletDamage); //총알에다 데미지 전달

        DivideExplosion = objectManager.Loader("HydraSeparatingFire");
        DivideExplosion.transform.position = DivideExplosionPos.transform.position;
        DivideExplosion.transform.rotation = DivideExplosionPos.transform.rotation;
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                        KaotiJaios4 KaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4.RicochetNum(1);
                        StartCoroutine(KaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //다리에다 데미지 전달
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
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
            if (collision.CompareTag("Infector, Standard"))
            {
                if (collision is CircleCollider2D) //몸통
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector 스크립트에다 타격 정보 전송
                    }
                }
                else if (collision is CapsuleCollider2D) //얼굴
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector 스크립트에다 타격 정보 전송
                    }
                }
                else if (collision is BoxCollider2D) //다리
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector 스크립트에다 타격 정보 전송
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
                        if (gameObject.activeSelf == true)
                        {
                            if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                            {
                                HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                                StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                                StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
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
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                                StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
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
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                            StartCoroutine(tearAsoShiioshare.HeadDamage(damage, 0.0f));
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
                            StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                        }
                    }
                }
            }

            // 오로제퍼 Orozeper
            if (collision is CircleCollider2D && collision.gameObject.layer == 14)
            {
                Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기
                StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
            }
        }
    }

    //체인지 Hydra-56 철갑포 탄피 배출
    public void MetalSkillBullet_Shell()
    {
        GameObject MetalSkillBullet_Shell1 = Instantiate(MB_Shell1);
        MetalSkillBullet_Shell1.transform.position = MB_Shell1Pos.position;
        MetalSkillBullet_Shell1.transform.rotation = MB_Shell1Pos.rotation;

        GameObject MetalSkillBullet_Shell2 = Instantiate(MB_Shell2);
        MetalSkillBullet_Shell2.transform.position = MB_Shell2Pos.position;
        MetalSkillBullet_Shell2.transform.rotation = MB_Shell2Pos.rotation;

        GameObject MetalSkillBullet_Shell3 = Instantiate(MB_Shell3);
        MetalSkillBullet_Shell3.transform.position = MB_Shell3Pos.position;
        MetalSkillBullet_Shell3.transform.rotation = MB_Shell3Pos.rotation;

        Destroy(MetalSkillBullet_Shell1, 5);
        Destroy(MetalSkillBullet_Shell2, 5);
        Destroy(MetalSkillBullet_Shell3, 5);
    }
}