using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

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
        //�Ѿ� �̵�
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
        bullet1.transform.rotation = BuckBulletPos.rotation; // ȸ�� �ʱ�ȭ, ȸ�� �� 0 
        bullet1.GetComponent<BuckBullet1>().SetDamage(DivideBulletDamage); //�Ѿ˿��� ������ ����

        GameObject bullet2 = Instantiate(BuckBullet2);
        bullet2.transform.position = BuckBulletPos.position;
        bullet2.transform.rotation = BuckBulletPos.rotation; // ȸ�� �ʱ�ȭ, ȸ�� �� 0 
        bullet2.GetComponent<BuckBullet2>().SetDamage(DivideBulletDamage); //�Ѿ˿��� ������ ����

        GameObject bullet3 = Instantiate(BuckBullet3);
        bullet3.transform.position = BuckBulletPos.position;
        bullet3.transform.rotation = BuckBulletPos.rotation; // ȸ�� �ʱ�ȭ, ȸ�� �� 0 
        bullet3.GetComponent<BuckBullet3>().SetDamage(DivideBulletDamage); //�Ѿ˿��� ������ ����

        GameObject bullet4 = Instantiate(BuckBullet4);
        bullet4.transform.position = BuckBulletPos.position;
        bullet4.transform.rotation = BuckBulletPos.rotation; // ȸ�� �ʱ�ȭ, ȸ�� �� 0 
        bullet4.GetComponent<BuckBullet4>().SetDamage(DivideBulletDamage); //�Ѿ˿��� ������ ����

        GameObject bullet5 = Instantiate(BuckBullet5);
        bullet5.transform.position = BuckBulletPos.position;
        bullet5.transform.rotation = BuckBulletPos.rotation; // ȸ�� �ʱ�ȭ, ȸ�� �� 0 
        bullet5.GetComponent<BuckBullet5>().SetDamage(DivideBulletDamage); //�Ѿ˿��� ������ ����

        DivideExplosion = objectManager.Loader("HydraSeparatingFire");
        DivideExplosion.transform.position = DivideExplosionPos.transform.position;
        DivideExplosion.transform.rotation = DivideExplosionPos.transform.rotation;
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                        KaotiJaios4 KaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4.RicochetNum(1);
                        StartCoroutine(KaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                    KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                    kaotiJaios4.RicochetNum(1);
                    StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //�ٸ����� ������ ����
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
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
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
            if (collision.CompareTag("Infector, Standard"))
            {
                if (collision is CircleCollider2D) //����
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }
                }
                else if (collision is BoxCollider2D) //�ٸ�
                {
                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
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
                        if (gameObject.activeSelf == true)
                        {
                            if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                            {
                                HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                                StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                                StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
                            }
                        }
                    }
                }

                if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                {
                    if (collision is BoxCollider2D) //�ٸ�
                    {
                        if (gameObject.activeSelf == true)
                        {
                            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                            {
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                                StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                            }
                        }
                    }
                }

                if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                        {
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
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
                            ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� ShieldAsoShiioshare ��ũ��Ʈ �ҷ�����
                            shieldAsoShiioshare.ShieldDamageExplosion(false);
                            StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                        }
                    }
                }
            }

            // �������� Orozeper
            if (collision is CircleCollider2D && collision.gameObject.layer == 14)
            {
                Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����
                StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
            }
        }
    }

    //ü���� Hydra-56 ö���� ź�� ����
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