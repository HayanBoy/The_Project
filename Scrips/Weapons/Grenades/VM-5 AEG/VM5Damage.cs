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
        //ĭŸũ��
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 27)
        {
            //ī��Ƽ-���̿���4, ī��Ƽ-���̿���4 ������, ī��Ƽ-���̿���4 ��ȭ��, ī��Ƽ-���̿���4 �����
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                {
                    BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Spear.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                {
                    BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //�������� ������ ����
                    KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Spear.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
            }
            //ī��Ƽ-���̿���4 ������
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                {
                    DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                    KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    KaotiJaios4Dual.RicochetNum(1);
                    StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
            }

            //Ÿ��ī-����-����Ʈ��1
            //����
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //�������� ������ ����
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //�������� ������ ����
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                    {
                        collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                    HealthTaikaLaiThrotro1.RicochetNum(1);
                    StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                {
                    BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                    StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                    Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                    Health2TaikaLaiThrotro1.RicochetNum(1);
                    StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
            }

            //��Ʈ��-ũ�ν��� 390
            //����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //Ÿ�� ������ �θ� ������Ʈ�� TearCrossfa ��ũ��Ʈ �ҷ�����
                            TearCrossfa.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            TearCrossfa.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� TearCrossfa���� �ǰݽ� ��ü �Ѽ� ���� ����
                            TearCrossfa.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //Ÿ�� ������ �θ� ������Ʈ�� TearCrossfa ��ũ��Ʈ �ҷ�����
                            TearCrossfa.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            TearCrossfa.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� TearCrossfa���� �ǰݽ� ��ü �Ѽ� ���� ����
                            TearCrossfa.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //�ǰ� �ִϸ��̼� �ߵ��� �����ϱ� ���� �̸� �����ϴ� ����
                            collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //�̻��� �߻��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //Ÿ�� ������ �θ� ������Ʈ�� TearCrossfa ��ũ��Ʈ �ҷ�����
                            TearCrossfa.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            TearCrossfa.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� TearCrossfa���� �ǰݽ� ��ü �Ѽ� ���� ����
                            TearCrossfa.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //�̻��� ���뿡�� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                }
            }
            //�ٸ� �� �����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //Ÿ�� ������ �θ� ������Ʈ�� TearCrossfa ��ũ��Ʈ �ҷ�����
                            TearCrossfa.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            TearCrossfa.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� TearCrossfa���� �ǰݽ� ��ü �Ѽ� ���� ����
                            TearCrossfa.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            StartCoroutine(TearCrossfa.LegRaillgunDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //�����
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //Ÿ�� ������ �θ� ������Ʈ�� TearCrossfa ��ũ��Ʈ �ҷ�����
                            TearCrossfa.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            TearCrossfa.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� TearCrossfa���� �ǰݽ� ��ü �Ѽ� ���� ����
                            TearCrossfa.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //īũ�ν�-Ÿ�����ν� 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            }
        }

        //������
        if (collision.gameObject.layer == 16)
        {
            //�Ϲ� ������
            if (collision.CompareTag("Infector, Body"))
            {
                if (collision is CircleCollider2D) //����
                {
                    var contactPoint1 = collision.transform.position.x; //�浹ü�� ��ġ ��ǥ
                    Distance = contactPoint1 - transform.position.x; //�浹ü ��ǥ�� ���� ��ǥ�� �Ÿ� ���. �浹ü�� �����ʿ� ������ �÷��� ��, ���ʿ� ������ ���̳ʽ� ���� ����ȴ�.

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2); //���߿� ���� �ָ� ���ư����� ���� ����
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }
                }
            }
            if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //��
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }
                }
            }
            if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    //Debug.Log(Distance);

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }
                }
            }
        }

        //���θ��
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 27)
        {
            //���̼� ���̿��ξ�(�ٸ�Ʈ)
            //����
            if (collision.CompareTag("Slorius, Aso Shiioshare body"))
            {
                if (collision is CircleCollider2D) //����
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    //Debug.Log(Distance);

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //���߿� ���� �׾��� �� �ݴ�� ���ư����� ���� ����
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow); //���߿� ���� �ָ� ���ư����� ���� ����
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                            tearAsoShiioshare.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            tearAsoShiioshare.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                            tearAsoShiioshare.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                            healthAsoShiioshare.ImHit = true;
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    var contactPoint1 = collision.transform.position.x;
                    Distance = contactPoint1 - transform.position.x;

                    if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                        Direction = false;
                    else
                        Direction = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow);
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                            tearAsoShiioshare.TearPartByOneShot = true; //���ǿ� ���� Ÿ���� �޾��� �� ��ü�� ���� �� ���ư��� ����
                            tearAsoShiioshare.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                            tearAsoShiioshare.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                            StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                        }
                    }
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //��
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
                    Direction = false;
                else
                    Direction = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                    {
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(ThrowRandom * ThrowRandom2 * AsoShiioshareThrow);
                        TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                        tearAsoShiioshare.SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        tearAsoShiioshare.LargeThrow = ThrowRandom; //�� �ָ� ���ư��� ������ ����
                        StartCoroutine(tearAsoShiioshare.HeadDamage(damage, 0.0f));
                        HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
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
                        ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� ShieldAsoShiioshare ��ũ��Ʈ �ҷ�����
                        shieldAsoShiioshare.ShieldDamageExplosion(true); //���� �������� ���� ������ ����. �̷� ��� ���ɷ����� ���� �� ���� �������� ���޵�.
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                        GameObject ShieldDamageEffect = Instantiate(ShieldDamage, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                        Destroy(ShieldDamageEffect, 3);
                    }
                }
            }
        }

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
        }

        //���߿� ���� ������Ʈ ���ư���
        if (collision.gameObject.layer == 8)
        {
            if (collision.CompareTag("AmmoShell"))
            {
                var contactPoint1 = collision.transform.position.x;
                Distance = contactPoint1 - transform.position.x;

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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

                if (Distance > 0) //�浹���� x��� �������� x�� ��ġ ��
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