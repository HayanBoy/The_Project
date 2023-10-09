using UnityEngine;
using System.Collections;

public class RaserSkillBullet : MonoBehaviour
{
    int damage;
    public float DamagePerTime;
    int BeamDamageAction; //�� ����Ʈ ���� �����ϴ� ����

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
        Destroy(gameObject, 3f); // 3�ʵ� �����
    }

    //���� �浹�� �� ������ ����
    void OnTriggerStay2D(Collider2D collision)
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4Spear ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Spear.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Spear.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Spear.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Spear.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Spear.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //�������� ������ ����
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Spear.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                                KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                kaotiJaios4.RicochetNum(1);
                                StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                                KaotiJaios4Dual.RicochetNum(1);
                                StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(1);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            kaotiJaios4.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(1);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            KaotiJaios4Dual.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
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
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                                StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //�������� ������ ����
                                HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                                HealthTaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                                StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //�������� ������ ����
                                Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                                Health2TaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                                HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                                HealthTaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                                Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                                Health2TaikaLaiThrotro1.RicochetNum(1);
                                StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthTaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1.RicochetNum(1);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            Health2TaikaLaiThrotro1.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }

            //��Ʈ��-ũ�ν��� 390
            //����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                                HealthAtroCrossfa.RicochetNum(1);
                                StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                HealthAtroCrossfa.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                DamageTime = 0;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        while (true)
                        {
                            DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                            if (DamageTime >= DamagePerTime)
                            {
                                collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //�ǰ� �ִϸ��̼� �ߵ��� �����ϱ� ���� �̸� �����ϴ� ����
                                collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa ��ũ��Ʈ �ҷ�����
                                HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                                HealthAtroCrossfa.RicochetNum(1);
                                StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                                HealthAtroCrossfa.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                if (collision is BoxCollider2D) //�̻��� �߻��
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //�̻��� ���뿡�� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
            }
            //�ٸ� �� �����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                            DamageTime = 0;
                        }
                        else
                            break;
                    }
                }
                else if (collision is CapsuleCollider2D) //�����
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime; //�������� ����� ������ ���� �������� �ֱ����� �ð� ���

                        if (DamageTime >= DamagePerTime)
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(1);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                            HealthAtroCrossfa.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
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
                        //īũ�ν�-Ÿ�����ν� 1389
                        BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                        Boss.RicochetNum(1);
                        StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        Boss.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                        DamageTime = 0;
                    }
                    else
                        break;
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
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
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
                if (collision is CapsuleCollider2D) //��
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
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
                if (collision is BoxCollider2D) //�ٸ�
                {
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                                collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                                collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                                healthInfector.ImHit = true;
                                healthInfector.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                StartCoroutine(healthInfector.DamageCharacter(damage, 0));
                                collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                                DamageTime = 0;
                            }
                        }
                        else
                            break;
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
                    while (true)
                    {
                        DamageTime += Time.deltaTime;

                        if (DamageTime >= DamagePerTime)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                                {
                                    collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                                    collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                                    HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                                    healthAsoShiioshare.ImHit = true;
                                    healthAsoShiioshare.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                    StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                    TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
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
                if (collision is BoxCollider2D) //�ٸ�
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
                                    HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                                    healthAsoShiioshare.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                    TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
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

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //��
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
                                HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                                healthAsoShiioshare.ImHit = true;
                                healthAsoShiioshare.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                                StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                                TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
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
                                ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� ShieldAsoShiioshare ��ũ��Ʈ �ҷ�����
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

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1"))
            {
                while (true)
                {
                    DamageTime += Time.deltaTime;

                    if (DamageTime >= DamagePerTime)
                    {
                        Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����
                        //orozeper.RicochetNum(1);
                        StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        //orozeper.SetBeam(BeamDamageAction); //�� ����Ʈ �� ����
                        //DamageTime = 0;
                    }
                    else
                        break;
                }
            }
        }
    }
}