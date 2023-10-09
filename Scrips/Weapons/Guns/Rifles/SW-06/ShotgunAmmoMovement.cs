using System.Collections;
using UnityEngine;

public class ShotgunAmmoMovement : MonoBehaviour
{
    public int Type; //총알 타입
    public float AmmoVelocity;
    public int ShotgunRicochet;
    public int BodyThrow;
    public int PieceThrow;

    int damage;
    int fireRange; //총알발사의 높이 랜덤함수
    float FireHeight = 0.2f; //총알발사 높이범위
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
        RicochetSoundRandom = Random.Range(0, 5);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay("Sound", RicochetSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay("Sound", RicochetSound2);
        else if (RicochetSoundRandom == 2)
            SoundManager.instance.SFXPlay("Sound", RicochetSound3);
        else if (RicochetSoundRandom == 3)
            SoundManager.instance.SFXPlay("Sound", RicochetSound4);
    }

    public void RandomMeatSound()
    {
        RicochetSoundRandom = Random.Range(0, 2);

        if (RicochetSoundRandom == 0)
            SoundManager.instance.SFXPlay("Sound", MeatHitSound1);
        else if (RicochetSoundRandom == 1)
            SoundManager.instance.SFXPlay("Sound", MeatHitSound2);
    }

    public void Rico() // 도탄 발사 오브젝트 풀링 
    {
        RicochetEffect = objectManager.RicoCHET();  // 게임오브젝트는 위에서 선언하고 RemoveRico와 연결짓기 위해 게임오브젝트 제외하고 RicoEffect만 선언 
        RicochetEffect.transform.position = ricochetEffectPos.position;
        RicochetEffect.transform.rotation = ricochetEffectPos.rotation;
    }

    void Update()
    {
        if (Type == 0)
        {
            //총알 이동
            if (transform.rotation.y == 0)
            {
                transform.Translate(transform.right * 1 * AmmoVelocity * Time.deltaTime);
                Direction = false; //오른쪽으로 피격
            }
            else
            {
                transform.Translate(transform.right * -1 * AmmoVelocity * Time.deltaTime);
                Direction = true; //왼쪽으로 피격
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(bulletFalse());
    }


    IEnumerator bulletFalse()
    {
        yield return new WaitForSeconds(0.5f);
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
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().TakeDown(true); //BehaviourKaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4Spear.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            KaotiJaios4 KaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            KaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(KaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))))
            {
                Ricochet = Random.Range(0, ShotgunRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear gun"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))))
            {
                Ricochet = Random.Range(0, ShotgunRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Spear wheel"))
                    {
                        BehaviourKaotiJaios4 BehaviourKaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                        KaotiJaios4Spear KaotiJaios4Spear = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4Spear>(); //KaotiJaios4 스크립트 불러오기
                        KaotiJaios4Spear.RicochetNum(Ricochet);
                        StartCoroutine(KaotiJaios4Spear.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //카오티-자이오스4 방패형
            //몸통
            if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().TakeDown(true); //BehaviourKaotiJaios4_ 스크립트 불러오기
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor body dual"))
                        {
                            collision.gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().TakeDown(true); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                            KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                            kaotiJaios4.RicochetNum(Ricochet);
                            StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual")))
            {
                Ricochet = Random.Range(0, ShotgunRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor gun dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }
            //바퀴
            else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel") || (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual")))
            {
                Ricochet = Random.Range(0, ShotgunRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel"))
                    {
                        BehaviourKaotiJaios4_ BehaviourKaotiJaios4_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviourKaotiJaios4_>(); //BehaviourKaotiJaios4_ 스크립트 불러오기
                        StartCoroutine(BehaviourKaotiJaios4_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Kaoti-Jaios 4 Armor wheel dual"))
                    {
                        DualBehaviourKaotiJaios5_ DualBehaviourKaotiJaios5_ = collision.gameObject.transform.parent.parent.GetComponent<DualBehaviourKaotiJaios5_>(); //DualBehaviourKaotiJaios5_ 스크립트 불러오기
                        StartCoroutine(DualBehaviourKaotiJaios5_.WheelDamage(damage, 0.0f)); //바퀴에다 데미지 전달
                        KaotiJaios4 kaotiJaios4 = collision.gameObject.transform.parent.parent.GetComponent<KaotiJaios4>(); //KaotiJaios4 스크립트 불러오기
                        kaotiJaios4.RicochetNum(Ricochet);
                        StartCoroutine(kaotiJaios4.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }

            //타이카-라이-쓰로트로1
            //몸통
            if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body")))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                            StartCoroutine(BehaviorTaikaLaiThrotro1_3.EngineDamage(damage, 0.0f)); //엔진에다 데미지 전달
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                            Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().TakeDown(true); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                            HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<HealthTaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                            HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                        else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().TakeDown(true); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                            Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.GetComponent<Health2TaikaLaiThrotro1>(); //Health2TaikaLaiThrotro1 스크립트 불러오기
                            Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                            StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //총
            else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun") || (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun")))
            {
                Ricochet = Random.Range(0, ShotgunRicochet);

                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_ BehaviorTaikaLaiThrotro1_ = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_>(); //BehaviorTaikaLaiThrotro1_ 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        HealthTaikaLaiThrotro1 HealthTaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<HealthTaikaLaiThrotro1>(); //KaotiJaios4 스크립트 불러오기
                        HealthTaikaLaiThrotro1.RicochetNum(Ricochet);
                        StartCoroutine(HealthTaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                    else if (collision.CompareTag("Kantakri, Taika-Lai-Throtro 1 Karrgen-Arite 3 gun"))
                    {
                        BehaviorTaikaLaiThrotro1_3 BehaviorTaikaLaiThrotro1_3 = collision.gameObject.transform.parent.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>(); //BehaviorTaikaLaiThrotro1_3 스크립트 불러오기
                        StartCoroutine(BehaviorTaikaLaiThrotro1_3.GunDamage(damage, 0.0f)); //총에다 데미지 전달
                        Health2TaikaLaiThrotro1 Health2TaikaLaiThrotro1 = collision.gameObject.transform.parent.parent.GetComponent<Health2TaikaLaiThrotro1>(); //HealthTaikaLaiThrotro1 스크립트 불러오기
                        Health2TaikaLaiThrotro1.RicochetNum(Ricochet);
                        StartCoroutine(Health2TaikaLaiThrotro1.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                    }
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
            }

            //아트로-크로스파 390
            //몸통
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
            {
                if (collision is BoxCollider2D) //몸통
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //샷건에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(BodyThrow);
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //눈
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //샷건에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(BodyThrow);
                            collision.gameObject.transform.parent.gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = true; //피격 애니메이션 발동을 저지하기 위해 이를 방지하는 목적
                            collision.gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TakeDown(true); //BehaviourAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                if (collision is BoxCollider2D) //미사일 발사대
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 MLB"))
                        {
                            collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction); //샷건에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(BodyThrow);
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MLBDamage(damage, 0.0f)); //미사일 포대에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }
            //다리 및 기관포
            if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
            {
                if (collision is BoxCollider2D) //다리
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //샷건에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.LegDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                        }
                    }
                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
                else if (collision is CapsuleCollider2D) //기관포
                {
                    Ricochet = Random.Range(0, ShotgunRicochet);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Kantakri, Atro-Crossfa 390 legs"))
                        {
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearCrossfa에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearCrossfa>().LargeThrow = 5; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                            TearCrossfa TearCrossfa = collision.gameObject.transform.parent.GetComponent<TearCrossfa>(); //TearCrossfa 스크립트 불러오기
                            StartCoroutine(TearCrossfa.MachinegunDamage(damage, 0.0f)); //다리에다 데미지 전달
                            HealthAtroCrossfa HealthAtroCrossfa = collision.gameObject.transform.parent.GetComponent<HealthAtroCrossfa>(); //HealthAtroCrossfa 스크립트 불러오기
                            HealthAtroCrossfa.RicochetNum(Ricochet);
                            StartCoroutine(HealthAtroCrossfa.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
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
                Ricochet = Random.Range(0, 70);

                if (isHit)
                    return;

                isHit = true;

                //카크로스-타이제로스 1389
                BossHp Boss = collision.gameObject.GetComponent<BossHp>(); //BossHp 스크립트 불러오기
                if (gameObject.activeSelf == true)
                {
                    Boss.RicochetNum(1);
                    StartCoroutine(Boss.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨
                }
                RandomSound();
                if (Ricochet != 0)
                    Rico();
                gameObject.SetActive(false);
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
                        //collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //충돌체의 부모 객체에 있는 InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction2); //작은 폭발 직후에 총알에 맞았을 경우, 정상적으로 죽도록 처리
                        collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(5);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.GetComponent<TearInfector>().SetTear(2); //TearInfector 스크립트에다 타격 정보 전송
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Infector, Face"))
            {
                if (collision is CapsuleCollider2D) //얼굴
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
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction2);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(5);
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(1); //TearInfector 스크립트에다 타격 정보 전송
                        //HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //저격총에 의한 얼굴 타격시 한 방에 즉사하는 데미지 전달
                        //StartCoroutine(healthInfector.DamageCharacter(10000, 0.0f));
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }
            else if (collision.CompareTag("Infector, Legs"))
            {
                if (collision is BoxCollider2D) //다리
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
                        //collision.gameObject.transform.parent.GetComponent<TearInfector>().TearPartByOneShot = true; //샷건에 의해 타격을 받았을 때 신체가 여러 개 날아가는 조취
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().SetDirection(Direction); //InfectorSpawn에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetDirection(Direction2);
                        collision.gameObject.transform.parent.GetComponent<DeathRolling>().SetThrow(5);
                        collision.gameObject.transform.parent.parent.GetComponent<InfectorSpawn>().LargeThrow = 0; //그냥 날아가는 일반 변수 전달
                        HealthInfector healthInfector = collision.gameObject.transform.parent.parent.GetComponent<HealthInfector>(); //타격 부위의 부모 오브젝트의 HealthInfector 스크립트 불러오기
                        healthInfector.ImHit = true;
                        StartCoroutine(healthInfector.DamageCharacter(damage, 0.0f));
                        collision.gameObject.transform.parent.parent.GetComponent<TearInfector>().SetTear(3); //TearInfector 스크립트에다 타격 정보 전송
                    }

                    RandomMeatSound();
                    GameObject BloodEffect = Instantiate(Blood, BloodPos.transform.position, BloodPos.transform.rotation);
                    Destroy(BloodEffect, 3);
                    gameObject.SetActive(false);
                }
            }
        }

        // 오로제퍼 Orozeper
        if (collision is CircleCollider2D && collision.gameObject.layer == 14)
        {
            if (isHit)
                return;

            isHit = true;

            Orozeper orozeper = collision.gameObject.GetComponent<Orozeper>(); //HealthInfector 스크립트 불러오기

            if (gameObject.activeSelf == true)
                StartCoroutine(orozeper.DamageCharacter(damage, 0.0f)); //데미지 정보 전달, 해당 영역에 피격 애니메이션이 포함됨

            gameObject.SetActive(false);
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
                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare body"))
                        {
                            collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearAsoShiioshare에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetDirection(Direction); //샷건에 의해 죽었을 때 반대로 날아가도록 방향 전달
                            collision.gameObject.transform.GetComponent<DeathRolling>().SetThrow(BodyThrow * 3);
                            HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                            StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
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
                if (collision is BoxCollider2D) //다리
                {
                    Ricochet = Random.Range(0, 70);

                    if (isHit)
                        return;

                    isHit = true;

                    if (gameObject.activeSelf == true)
                    {
                        if (collision.CompareTag("Slorius, Aso Shiioshare Legs"))
                        {
                            collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearAsoShiioshare에다 피격시 신체 훼손 방향 전달
                            collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                            TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
                            StartCoroutine(tearAsoShiioshare.LegDamage(damage, 0.0f));
                        }
                    }

                    RandomSound();
                    if (Ricochet != 0)
                        Rico();
                    gameObject.SetActive(false);
                }
            }

            if (collision.CompareTag("Slorius, Aso Shiioshare Head")) //얼굴
            {
                if (isHit)
                    return;

                isHit = true;

                if (gameObject.activeSelf == true)
                {
                    if (collision.CompareTag("Slorius, Aso Shiioshare Head"))
                    {
                        collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>().SetDirection(Direction); //충돌체의 부모 객체에 있는 TearAsoShiioshare에다 피격시 신체 훼손 방향 전달
                        collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>().LargeThrow = PieceThrow; //더 멀리 날아가는 가변수 전달
                        HealthAsoShiioshare healthAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<HealthAsoShiioshare>(); //타격 부위의 부모 오브젝트의 HealthAsoShiioshare 스크립트 불러오기
                        StartCoroutine(healthAsoShiioshare.DamageCharacter(damage, 0.0f));
                        TearAsoShiioshare tearAsoShiioshare = collision.gameObject.transform.parent.parent.GetComponent<TearAsoShiioshare>(); //타격 부위의 부모 오브젝트의 TearAsoShiioshare 스크립트 불러오기
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
                        ShieldAsoShiioshare shieldAsoShiioshare = collision.gameObject.transform.parent.parent.parent.GetComponent<ShieldAsoShiioshare>(); //타격 부위의 부모 오브젝트의 ShieldAsoShiioshare 스크립트 불러오기
                        shieldAsoShiioshare.ShieldDamageExplosion(false);
                        StartCoroutine(shieldAsoShiioshare.DamageShieldCharacter(damage, 0.0f));
                    }
                }

                GameObject ShieldDamageEffect = Instantiate(ShieldDamage, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                Destroy(ShieldDamageEffect, 3);
                gameObject.SetActive(false);
            }
        }
    }
}