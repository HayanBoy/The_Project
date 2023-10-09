using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

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

    //private Shake shake; // ȭ�� ��鸲 Ŭ���� 

    public void SetDamage(int num)
    {
        damage = num;
    }

    private void activeFalse() //ȭ������� ������ �� ��Ȱ��ȭ ó�� (�Ѿ�) 
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
                else if (collision is CapsuleCollider2D) //��
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
            //��
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
            //����
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
            //ī��Ƽ-���̿���4 ������
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //����
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
                else if (collision is CapsuleCollider2D) //��
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
            //��
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
            //����
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

            //Ÿ��ī-����-����Ʈ��1
            //����
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //����
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
                else if (collision is CapsuleCollider2D) //��
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
            //��
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

            //��Ʈ��-ũ�ν��� 390
            //����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //����
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                    {
                        Active();
                    }
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //�̻��� �߻��
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                    {
                        Active();
                    }
                }
            }
            //�ٸ� �� �����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
                else if (collision is CapsuleCollider2D) //�����
                {
                    if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                    {
                        Active();
                    }
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                //īũ�ν�-Ÿ�����ν� 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                Boss.RicochetNum(1);
                StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                Active();
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
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    Active();
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //��
            {
                Active();
            }

            if (collision.CompareTag("Shield"))
            {
                Active();
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
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //��
                {
                    Active();
                }
            }
            if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    Active();
                }
            }
        }

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����
            //orozeper.RicochetNum(1);
            StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�

            Active();
        }

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
                collision.gameObject.transform.GetComponent<ShellCase_SW06>().SetThrow(ThrowRandom);
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
                collision.gameObject.transform.GetComponent<SW06MagazineFall>().SetThrow(ThrowRandom);
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
                collision.gameObject.transform.GetComponent<ShellMovement>().SetThrow(ThrowRandom);
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
                collision.gameObject.transform.GetComponent<VM5Throw>().SetThrow(ThrowRandom);
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

                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetDirection(Direction);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().SetThrow(ThrowRandom);
                collision.gameObject.transform.GetComponent<TearCreateInfector>().Throwing = true;
            }
            if (collision.CompareTag("Death Body Taika-Lai-Throtro1"))
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
            if (collision.CompareTag("Death Body Sky Crane"))
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
        }
    }
}