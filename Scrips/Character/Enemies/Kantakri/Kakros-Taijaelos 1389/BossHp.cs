using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : Character
{
    public float RealHP;
    float hitPoints; // ü��
    float armor; // ����
    public int Ricochet;

    //Ÿ�� �� ����
    public GameObject BeamTaken1;
    public GameObject BeamTaken2;
    public GameObject BeamTaken3;
    public GameObject BeamTaken4;
    public Transform BeamTakenPos;
    int BeamDamageAction; //�� ȿ�� �ޱ�
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

    //��ź ��ġ �ޱ�
    public void RicochetNum(int num)
    {
        Ricochet = num;
    }

    //�� ������ �ޱ�
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
            BeamDamageAction = 0; //������ ���⿡ Ÿ�ݹ��� ����, �ٸ� ���� Ÿ���� �޾��� �� ������ ���� ȿ���� ��Ÿ���� �ʵ��� �ϱ� ���� ����
        }
    }

    //�÷��̾ Ÿ���� �޾��� ���� ������ ����
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            if (Ricochet != 0)
            {
                hitPoints = hitPoints - (damage/armor);
                StartCoroutine(BeamAction());
                TimeStemp += 0.1f;
                //Healthbar.SetHealth(hitPoints, startingHitPoints); // ü�¹ٿ� ���� ü�� ���� 

                if (hitPoints <= 0)
                {
                    gameObject.GetComponent<boss>().enabled = false;
                    GetComponent<Animator>().SetBool("Death, Kakros-Taijaelos 1389", true);
                    this.gameObject.layer = 27;
                    Slider.gameObject.SetActive(false);

                    LevelControlScript.instance.youWin(); // �������� Ŭ���� 
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

    //�� ���� �޾��� ���� �ð�ȿ�� �߻�
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
            Debug.Log("armor ���");
        }

        if (collision.CompareTag("Orozeper_"))
        {
            armor += 10;
            Debug.Log("armor ���");
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
            Debug.Log("armor ����");
        }

        if (collision.CompareTag("Orozeper_"))
        {
            armor -= 10;
            Debug.Log("armor ����");
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