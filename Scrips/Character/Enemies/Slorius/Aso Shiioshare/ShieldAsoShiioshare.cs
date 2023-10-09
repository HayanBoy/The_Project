using System.Collections;
using UnityEngine;

public class ShieldAsoShiioshare : MonoBehaviour
{
    Animator animator;

    public int SloriusType; //슬로리어스 병사 타입.
    public GameObject Shield;
    public GameObject ShieldCollider;
    public GameObject ShieldCracks;
    public GameObject Body1;
    public GameObject Body2;
    public GameObject Body3;
    public GameObject Body4;
    public GameObject Body5;

    public float StartShieldPoints;
    public float ShieldPoints;
    public int ShieldArmor;
    public float DamageTime; //데미지 애니메이션 출력 시간
    public float DamageDissolveTime;
    public float KnockBackForce;
    public float KnockBackReducer; //넉백당했을 때, 감속처리
    public float KnockBackLevelUp;
    private float KnockBackSpeed;
    private float x;

    private int ShieldDamageRandom;

    private bool Crack1 = false;
    private bool Crack2 = false;
    private bool Crack3 = false;
    private bool Crack4 = false;
    private bool Crack5 = false;
    private bool Crack6 = false;
    private bool Crack7 = false;
    private bool Crack8 = false;
    private bool Crack9 = false;
    private bool Crack10 = false;
    private bool Crack11 = false;
    private bool Crack12 = false;
    private bool Crack13 = false;
    private bool ShieldDamageD = false;
    private bool ShieldExplosion = false; //방어막이 피격 받았을 때, 해당 피격 요소가 폭발인지 구별하기 위한 요소
    private bool KnockBackShot = false; //넉백 당할 때의 신호

    GameObject SD;
    public GameObject ShieldPiece;
    public Transform ShieldPiecePos;
    public GameObject ShieldDamage;
    public GameObject ShieldDamage2;
    public GameObject ShieldDamage3;
    public Transform ShieldDamagePos;

    public AudioClip ShieldDamageSound1;
    public AudioClip ShieldDamageSound2;
    public AudioClip ShieldDamageSound3;
    public AudioClip ShieldDamageDeffence;
    public AudioClip ShieldCrack;
    public AudioClip ShieldDestroy;

    //대쉬 근접 공격에 의한 피격
    public void Hurt(Vector2 pos)
    {
        x = transform.position.x - pos.x;
        if (x < 0)
            x = 1;
        else
            x = -1;

        KnockBackSpeed = KnockBackForce;
        KnockBackShot = true;
        Invoke("StopKnockBack", 1);
    }

    void StopKnockBack()
    {
        KnockBackShot = false;
    }

    public void ShieldDamageDirection(bool SDD)
    {
        if (SDD == true)
            ShieldDamageD = true; //오른쪽
        else
            ShieldDamageD = false; //왼쪽
    }

    public void ShieldDamageExplosion(bool boolean)
    {
        if (boolean == true)
            ShieldExplosion = true; //오른쪽
        else
            ShieldExplosion = false; //왼쪽
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        KnockBackForce += KnockBackLevelUp;
        KnockBackReducer += KnockBackLevelUp * 0.01f;
    }

    private void OnEnable()
    {
        Shield.gameObject.SetActive(true);
        ShieldCollider.GetComponent<CircleCollider2D>().enabled = true;
        ShieldCollider.gameObject.layer = 12;

        Crack1 = false;
        Crack2 = false;
        Crack3 = false;
        Crack4 = false;
        Crack5 = false;
        Crack6 = false;
        Crack7 = false;
        Crack8 = false;
        Crack9 = false;
        Crack10 = false;
        Crack11 = false;
        Crack12 = false;
        Crack13 = false;
        ShieldExplosion = false;

        ShieldPoints = StartShieldPoints;
    }

    private void Update()
    {
        //SD.transform.position = ShieldDamagePos.transform.position;

        if (KnockBackShot == true)
        {
            transform.Translate(Vector2.right * KnockBackSpeed * Time.deltaTime);

            if (KnockBackSpeed > 0)
                KnockBackSpeed -= KnockBackReducer;
            else
                KnockBackSpeed = 0;
        }
    }

    //적이 타격을 받았을 때의 데미지 적용
    public IEnumerator DamageShieldCharacter(int damage, float interval)
    {
        while (true)
        {
            if(ShieldExplosion == false)
            {
                ShieldPoints = ShieldPoints - damage;
                if (ShieldDamageD == true)
                {
                    SD = Instantiate(ShieldDamage, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                    Destroy(SD, 2);
                }
                else
                {
                    SD = Instantiate(ShieldDamage2, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                    SD.transform.eulerAngles = new Vector3(0, 180, 0);
                    Destroy(SD, 2);
                }
                if (SloriusType == 3)
                    StartCoroutine(DamageAction());
            }
            else
            {
                ShieldPoints = ShieldPoints - damage / ShieldArmor;
                SD = Instantiate(ShieldDamage3, ShieldDamagePos.transform.position, ShieldDamagePos.transform.rotation);
                Destroy(SD, 2);
                if (SloriusType == 3)
                    StartCoroutine(DamageAction2());
            }
            //Debug.Log("ShieldPoints : " + ShieldPoints);

            if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.0769f) && Crack1 == false) //1차 방어막 크랙
            {
                SoundManager.instance.SFXPlay28("Sound", ShieldCrack);
                Crack1 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.1538f) && Crack2 == false) //2차 방어막 크랙
            {
                Crack2 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.2307f) && Crack3 == false) //3차 방어막 크랙
            {
                Crack3 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.3076f) && Crack4 == false) //4차 방어막 크랙
            {
                Crack4 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.3845f) && Crack5 == false) //5차 방어막 크랙
            {
                SoundManager.instance.SFXPlay28("Sound", ShieldCrack);
                Crack5 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.4614f) && Crack6 == false) //6차 방어막 크랙
            {
                Crack6 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.5383f) && Crack7 == false) //7차 방어막 크랙
            {
                Crack7 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.6152f) && Crack8 == false) //8차 방어막 크랙
            {
                Crack8 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.6921f) && Crack9 == false) //9차 방어막 크랙
            {
                Crack9 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.749f) && Crack10 == false) //10차 방어막 크랙
            {
                SoundManager.instance.SFXPlay28("Sound", ShieldCrack);
                Crack10 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.8059f) && Crack11 == false) //11차 방어막 크랙
            {
                Crack11 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.8728f) && Crack12 == false) //12차 방어막 크랙
            {
                Crack12 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }
            else if (ShieldPoints < StartShieldPoints - (StartShieldPoints * 0.9f) && Crack13 == false) //13차 방어막 크랙
            {
                SoundManager.instance.SFXPlay28("Sound", ShieldCrack);
                Crack13 = true;
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOn(1);
            }

            if (ShieldPoints <= float.Epsilon)
            {
                SoundManager.instance.SFXPlay6("Sound", ShieldDestroy);
                ShieldCracks.GetComponent<ShieldDamage>().ShieldOff(true);

                ShieldCollider.GetComponent<CircleCollider2D>().enabled = false;
                ShieldCollider.gameObject.layer = 0;

                Shield.gameObject.SetActive(false);
                GameObject ShieldCracksPrefab = Instantiate(ShieldPiece, ShieldPiecePos.transform.position, ShieldPiecePos.transform.rotation);
                Destroy(ShieldCracksPrefab, DamageDissolveTime);
                gameObject.GetComponent<HealthAsoShiioshare>().AsoShiioshareShieldDown(true);

                Body1.GetComponent<CircleCollider2D>().enabled = true;
                Body2.GetComponent<BoxCollider2D>().enabled = true;
                Body3.GetComponent<CircleCollider2D>().enabled = true;
                Body1.gameObject.layer = 12;
                Body2.gameObject.layer = 12;
                Body3.gameObject.layer = 12;
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }

            else
            {
                break;
            }
        }
    }

    IEnumerator DamageAction()
    {
        ShieldDamageRandom = Random.Range(0, 3);
        if (ShieldDamageRandom == 0)
            SoundManager.instance.SFXPlay26("Sound", ShieldDamageSound1);
        else if (ShieldDamageRandom == 1)
            SoundManager.instance.SFXPlay24("Sound", ShieldDamageSound2);
        if (ShieldDamageRandom == 2)
            SoundManager.instance.SFXPlay2("Sound", ShieldDamageSound3);

        animator.SetBool("Shield damage, Aso Shiioshare", true);
        yield return new WaitForSeconds(DamageTime);
        animator.SetBool("Shield damage, Aso Shiioshare", false);
    }

    IEnumerator DamageAction2()
    {
        SoundManager.instance.SFXPlay25("Sound", ShieldDamageDeffence);
        animator.SetBool("Shield damage2, Aso Shiioshare", true);
        yield return new WaitForSeconds(DamageTime * 2);
        animator.SetBool("Shield damage2, Aso Shiioshare", false);
    }
}
