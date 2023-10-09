using UnityEngine;
using System.Collections;  // IEunmerator ���� ���� ���� 

public class MiniGunAmmoMovement : MonoBehaviour
{
    public float AmmoVelocity;

    int damage;
    int fireRange; //�Ѿ˹߻��� ���� �����Լ�
    float FireHeight = 0.05f; //�Ѿ˹߻� ���̹���
    bool Direction;
    bool Direction2;

    string Effect = "ricochetEffect";
    public Transform ricochetEffectPos;

    public ObjectManager objectManager;
    GameObject RicochetEffect;

    public GameObject Blood;
    public GameObject SloriusBlood;
    public Transform BloodPos;

    public GameObject ShieldDamage;
    public Transform ShieldDamagePos;

    public bool isHit;

    int Ricochet;
    public int KaotiJaoiosRicochet;
    public int KaotiJaoiosEliteRicochet;
    public int TaikaLaiThrotroRicochet;
    public int AtroCrossfaRicochet;
    public int KakrosTaijaelosRicochet;
    public int AsoShiioshareLegRicochet;

    private int RicochetSoundRandom;
    public AudioClip RicochetSound1;
    public AudioClip RicochetSound2;
    public AudioClip RicochetSound3;
    public AudioClip RicochetSound4;

    private int MeatHitSoundRandom;
    public AudioClip MeatHitSound1;
    public AudioClip MeatHitSound2;

    public void SetDamage(int num)
    {
        damage = num;
    }

    public void RandomSound()
    {
        RicochetSoundRandom = Random.Range(0, 6);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay6("Sound", RicochetSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay6("Sound", RicochetSound2);
        else if (RicochetSoundRandom == 2)
            SoundManager.instance.SFXPlay6("Sound", RicochetSound3);
        else if (RicochetSoundRandom == 3)
            SoundManager.instance.SFXPlay6("Sound", RicochetSound4);
    }

    public void RandomMeatSound()
    {
        RicochetSoundRandom = Random.Range(0, 6);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay("Sound", MeatHitSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay("Sound", MeatHitSound2);
    }

    void RemoveRico()  // ��ź �߻� ��Ȱ��ȭ 
    {
        if (RicochetEffect.activeSelf == true)
        {
            RicochetEffect.SetActive(false);
        }
    }

    public void Rico() // ��ź �߻� ������Ʈ Ǯ�� 
    {
        RicochetEffect = objectManager.Loader(Effect);  // ���ӿ�����Ʈ�� ������ �����ϰ� RemoveRico�� �������� ���� ���ӿ�����Ʈ �����ϰ� RicoEffect�� ���� 
        RicochetEffect.transform.position = ricochetEffectPos.position;
        RicochetEffect.transform.rotation = ricochetEffectPos.rotation;

        Invoke("RemoveRico", 0.14f);
    }

    private void OnEnable()
    {
        StartCoroutine(BulletFalse());
    }
    void Update()
    {
        //�Ѿ� �̵�
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 5);

            if (Time.timeScale == 1)
            {
                if (fireRange == 1)
                {
                    transform.Translate(transform.up * -FireHeight);
                }
                else if (fireRange == 2)
                {
                    transform.Translate(transform.up * FireHeight);
                }
            }
        }
        else
        {
            transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);

            fireRange = Random.Range(0, 5);

            if (Time.timeScale == 1)
            {
                if (fireRange == 1)
                {
                    transform.Translate(transform.up * -FireHeight);
                }
                else if (fireRange == 2)
                {
                    transform.Translate(transform.up * FireHeight);
                }
            }
        }
    }

    IEnumerator BulletFalse() 
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.tag == "Block")
        {
            gameObject.SetActive(false);
        }

        //ĭŸũ��
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 27)
        {
            //ī��Ƽ-���̿���4, ī��Ƽ-���̿���4 ������, ī��Ƽ-���̿���4 ��ȭ��, ī��Ƽ-���̿���4 �����
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))))
            {
                if (collision is BoxCollider2D) //����
                {
                    Ricochet = Random.Range(0, KaotiJaoiosRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    Ricochet = Random.Range(0, KaotiJaoiosRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Spear.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                Ricochet = Random.Range(0, KaotiJaoiosRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                Ricochet = Random.Range(0, KaotiJaoiosRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //�������� ������ ����
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Spear.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //ī��Ƽ-���̿���4 ������
            //����
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //����
                {
                    Ricochet = Random.Range(0, KaotiJaoiosEliteRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    Ricochet = Random.Range(0, KaotiJaoiosEliteRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                            KaotiJaios4Dual.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                Ricochet = Random.Range(0, KaotiJaoiosEliteRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //����
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                Ricochet = Random.Range(0, KaotiJaoiosEliteRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //�������� ������ ����
                        KaotiJaios4Dual KaotiJaios4Dual = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Dual>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        KaotiJaios4Dual.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Dual.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }

            //Ÿ��ī-����-����Ʈ��1
            //����
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //����
                {
                    Ricochet = Random.Range(0, TaikaLaiThrotroRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //�������� ������ ����
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //�������� ������ ����
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    Ricochet = Random.Range(0, TaikaLaiThrotroRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                            Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //��
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                Ricochet = Random.Range(0, TaikaLaiThrotroRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 ��ũ��Ʈ �ҷ�����
                        HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 ��ũ��Ʈ �ҷ�����
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //�ѿ��� ������ ����
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 ��ũ��Ʈ �ҷ�����
                        Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }

            //��Ʈ��-ũ�ν��� 390
            //����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //����
                {
                    Ricochet = Random.Range(0, AtroCrossfaRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //��
                {
                    Ricochet = Random.Range(0, AtroCrossfaRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //�ǰ� �ִϸ��̼� �ߵ��� �����ϱ� ���� �̸� �����ϴ� ����
                            collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
            {
                if (collision is BoxCollider2D) //�̻��� �߻��
                {
                    Ricochet = Random.Range(0, AtroCrossfaRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //�̻��� ���뿡�� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //�ٸ� �� �����
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    Ricochet = Random.Range(0, AtroCrossfaRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //�����
                {
                    Ricochet = Random.Range(0, AtroCrossfaRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa ��ũ��Ʈ �ҷ�����
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //�ٸ����� ������ ����
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa ��ũ��Ʈ �ҷ�����
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }

            if (collision.CompareTag("Kantakri, Kakros-Taijaelos 1389"))
            {
                Ricochet = Random.Range(0, KakrosTaijaelosRicochet);

                if (isHit)
                    return;

                isHit = true;

                //īũ�ν�-Ÿ�����ν� 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp ��ũ��Ʈ �ҷ�����
                if (gameObject.activeSelf == true)
                {
                    Boss.RicochetNum(1);
                    StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
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
                    int DirectionRandom = Random.Range(0, 2);
                    if (DirectionRandom == 0)
                        Direction2 = true;
                    else
                        Direction2 = false;

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //�浹ü�� �θ� ��ü�� �ִ� InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //��
                {
                    int DirectionRandom = Random.Range(0, 2);
                    if (DirectionRandom == 0)
                        Direction2 = true;
                    else
                        Direction2 = false;

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction2);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(5);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    int DirectionRandom = Random.Range(0, 2);
                    if (DirectionRandom == 0)
                        Direction2 = true;
                    else
                        Direction2 = false;

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn���� �ǰݽ� ��ü �Ѽ� ���� ����
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //�׳� ���ư��� �Ϲ� ���� ����
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction2);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(5);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthInfector ��ũ��Ʈ �ҷ�����
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector ��ũ��Ʈ���� Ÿ�� ���� ����
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
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
                    int DirectionRandom = Random.Range(0, 2);
                    if (DirectionRandom == 0)
                        Direction2 = true;
                    else
                        Direction2 = false;

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //���� ���� ���Ŀ� �Ѿ˿� �¾��� ���, ���������� �׵��� ó��
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                            healthAsoShiioshare.ImHit = true;
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                            tearAsoShiioshare.SetDirection(Direction);
                            tearAsoShiioshare.LargeThrow = 0;
                            StartCoroutine(tearAsoShiioshare.ArmDamage(damage, 0.0f));
                        }
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(SloriusBlood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
            {
                if (collision is BoxCollider2D) //�ٸ�
                {
                    Ricochet = Random.Range(0, AsoShiioshareLegRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                            StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                        }
                    }

                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //��
            {
                int DirectionRandom = Random.Range(0, 2);
                if (DirectionRandom == 0)
                    Direction2 = true;
                else
                    Direction2 = false;

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                    {
                        HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� HealthAsoShiioshare ��ũ��Ʈ �ҷ�����
                        healthAsoShiioshare.ImHit = true;
                        StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                        TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� TearAsoShiioshare ��ũ��Ʈ �ҷ�����
                        tearAsoShiioshare.SetDirection(Direction);
                        tearAsoShiioshare.LargeThrow = 0;
                        StartCoroutine(tearAsoShiioshare.HeadDamage(damage, 0.0f));
                    }
                }

                RandomMeatSound();
                GameObject BloodEffect = Instantiate(SloriusBlood, BloodPos.transform.position, BloodPos.transform.rotation);
                Destroy(BloodEffect, 3);
                gameObject.SetActive(false);
            }

            if (collision.CompareTag("Shield"))
            {
                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Shield"))
                    {
                        ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //Ÿ�� ������ �θ� ������Ʈ�� ShieldAsoShiioshare ��ũ��Ʈ �ҷ�����
                        shieldAsoShiioshare.ShieldDamageExplosion(false);
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                    }
                }

                GameObject ShieldDamageEffect = Instantiate(ShieldDamage, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                Destroy(ShieldDamageEffect, 3);
                gameObject.SetActive(false);
            }
        }

        // �������� Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            if (isHit)
                return;

            isHit = true;

            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector ��ũ��Ʈ �ҷ�����

            if (gameObject.activeSelf == true)
                StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //������ ���� ����, �ش� ������ �ǰ� �ִϸ��̼��� ���Ե�

            gameObject.SetActive(false);
        }
    }
}