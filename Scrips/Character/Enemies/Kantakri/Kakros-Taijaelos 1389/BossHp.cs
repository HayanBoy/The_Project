using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : Character
{
    public float RealHP;
    float hitPoints; // 체력
    float armor; // 방어력
    public int Ricochet;

    //타격 빔 생성
    public GameObject BeamTaken1;
    public GameObject BeamTaken2;
    public GameObject BeamTaken3;
    public GameObject BeamTaken4;
    public Transform BeamTakenPos;
    int BeamDamageAction; //빔 효과 받기
    float TimeStemp;

    public GameObject RicochetPrefab;
    public Transform RicochetPos;

    public Slider Slider;

    private void Start()
    {

    }
    private void OnEnable()
    {
        hitPoints = startingHitPoints;
        armor = startingArmor;
     
    }

    //도탄 수치 받기
    public void RicochetNum(int num)
    {
        Ricochet = num;
    }

    //빔 데미지 받기
    public void SetBeam(int num)
    {
        BeamDamageAction = num;
    }

    void Update()
    {
        RealHP = hitPoints;
        TimeStemp -= Time.deltaTime;

        if (TimeStemp < 0)
        {
            TimeStemp = 0;
            BeamDamageAction = 0; //레이져 무기에 타격받은 이후, 다른 무기 타격을 받았을 때 레이져 맞은 효과가 나타나지 않도록 하기 위한 조취
        }
    }

    //플레이어가 타격을 받았을 때의 데미지 적용
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (Ricochet != 0)
            {
                hitPoints = hitPoints - (damage/armor);
                StartCoroutine(BeamAction());
                TimeStemp += 0.1f;
                //Healthbar.SetHealth(hitPoints, startingHitPoints); // 체력바에 기존 체력 대입 

                if (hitPoints <= 0)
                {
                    gameObject.GetComponent<boss>().enabled = false;
                    GetComponent<Animator>().SetBool("Death, Kakros-Taijaelos 1389", true);
                    this.gameObject.layer = 27;
                    Slider.gameObject.SetActive(false);

                    LevelControlScript.instance.youWin(); // 스테이지 클리어 
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
            else if (Ricochet == 0)
            {
                GameObject Ricochet1 = Instantiate(RicochetPrefab, RicochetPos.transform.position, RicochetPos.transform.rotation);
            }
        }
    }

    //빔 공격 받았을 때의 시각효과 발생
    public IEnumerator BeamAction()
    {
        //Debug.Log(TimeStemp);
        if (BeamDamageAction == 1)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam1 = Instantiate(BeamTaken1, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 2)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam2 = Instantiate(BeamTaken2, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 3)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam3 = Instantiate(BeamTaken3, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (BeamDamageAction == 4)
        {
            while (TimeStemp > 0)
            {
                GameObject DamageBeam4 = Instantiate(BeamTaken4, BeamTakenPos.transform.position, BeamTakenPos.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator HpDrain()
    {
        if (hitPoints <= 2490)
        {
            hitPoints += 1000;
            Debug.Log("HpDrain");
            Slider.value = hitPoints;

            if (hitPoints >= 2500)
                hitPoints = 2500;
        }
        yield return new WaitForSeconds(15f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Orozeper"))
        {
            armor += 10;
            Debug.Log("armor 상승");
        }

        if (collision.CompareTag("Orozeper_"))
        {
            armor += 10;
            Debug.Log("armor 상승");
        }

        if (collision.CompareTag("Orozeper_red"))
        {
            StartCoroutine(HpDrain());
        }

        if (collision.CompareTag("Orozeper_red_"))
        {
            StartCoroutine(HpDrain());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Orozeper"))
        {
            armor -= 10;
            Debug.Log("armor 감소");
        }

        if (collision.CompareTag("Orozeper_"))
        {
            armor -= 10;
            Debug.Log("armor 감소");
        }

        if (collision.CompareTag("Orozeper_red"))
        {
            StopCoroutine(HpDrain());
        }

        if (collision.CompareTag("Orozeper_red_"))
        {
            StopCoroutine(HpDrain());
        }
    }
}