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
        //�Ѿ� �̵�
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

    //���� �浹�� �� ������ ����
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
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Spear.FlameBody();
                            KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Spear.FlameBody();
                            KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameGun();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Spear.FlameGun();
                        KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameGun();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameWheel();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Spear.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Spear.FlameWheel();
                        KaotiJaios4Spear.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameWheel();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //ī��Ƽ-���̿���4 ������
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                            kaotiJaios4.FlameBody();
                            kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                            KaotiJaios4Dual.FlameBody();
                            KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameGun();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameGun();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(1);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        kaotiJaios4.FlameHitTime = KantakriFlameDamageTime;
                        kaotiJaios4.FlameWheel();
                        kaotiJaios4.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(1);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        KaotiJaios4Dual.FlameHitTime = KantakriFlameDamageTime;
                        KaotiJaios4Dual.FlameWheel();
                        KaotiJaios4Dual.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                }
            }

            //Ÿ��ī-����-����Ʈ��1
            //����
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            HealthTaikaLaiThrotro1.FlameBody();
                            HealthTaikaLaiThrotro1.FlameEngine();
                            HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage / KantakriDividedDamage, 0)); //�������� ������ ����
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            Health2TaikaLaiThrotro1.FlameBody();
                            Health2TaikaLaiThrotro1.FlameEngine();
                            Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            HealthTaikaLaiThrotro1.FlameBody();
                            HealthTaikaLaiThrotro1.FlameEngine();
                            HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                            Health2TaikaLaiThrotro1.FlameBody();
                            Health2TaikaLaiThrotro1.FlameEngine();
                            Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        HealthTaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        HealthTaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                        HealthTaikaLaiThrotro1.FlameGun();
                        HealthTaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage / KantakriDividedDamage, 0)); //�ѿ��� ������ ����
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        Health2TaikaLaiThrotro1.RicochetNum(1);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        Health2TaikaLaiThrotro1.FlameHitTime = KantakriFlameDamageTime;
                        Health2TaikaLaiThrotro1.FlameGun();
                        Health2TaikaLaiThrotro1.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                    }
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
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameBody();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
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
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameBody();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
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
                            StartCoroutine(TearCrossfa.MLBDamage(damage / KantakriDividedDamage, 0)); //�̻��� ���뿡�� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                            StartCoroutine(TearCrossfa.LegDamage(damage / KantakriDividedDamage, 0)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameLegs();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
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
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage / KantakriDividedDamage, 0)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.FlameHitTime = KantakriFlameDamageTime;
                            HealthAtroCrossfa.FlameGun();
                            HealthAtroCrossfa.FlameDamgeStart((damage / KantakriDividedDamage) / 2, KantakriFlameDamagePerTime);
                        }
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //īũ�ν�-Ÿ�����ν� 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                if (gameObject.activeSelf == true)
                {
                    Boss.RicochetNum(1);
                    StartCoroutine(Boss.DamageCharacter(damage / KantakriDividedDamage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
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
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
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
                if (collision is CapsuleCollider2D) //��
                {
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.FlameHitTime = TaitrokiFlameDamageTime;
                        healthInfector.FlameHead();
                        healthInfector.FlameDamgeStart((damage / CreatureAddDamage) / 2, TaitrokiFlameDamagePerTime);
                        StartCoroutine(healthInfector.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }
            else if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    if (gameObject.activeSelf == true)
                    {
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.FlameHitTime = TaitrokiFlameDamageTime;
                        healthInfector.FlameLegs();
                        healthInfector.FlameDamgeStart((damage / CreatureAddDamage) / 2, TaitrokiFlameDamagePerTime);
                        StartCoroutine(healthInfector.DamageCharacter(damage * CreatureAddDamage, 0));
                    }
                }
            }
        }

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����

            if (gameObject.activeSelf == true)
                StartCoroutine(orozeper.DamageCharacter(damage, 0)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
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
                if (collision is BoxCollider2D) //�ٸ�
                {
                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                            healthAsoShiioshare.FlameHitTime = SloriusFlameDamageTime;
                            healthAsoShiioshare.FlameLegs();
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
                        ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� ShieldAsoShiioshare ��ũ��Ʈ �ҷ�����
                        shieldAsoShiioshare.ShieldDamageExplosion(false);
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage / KantakriDividedDamage, 0));
                    }
                }
            }
        }
    }
}