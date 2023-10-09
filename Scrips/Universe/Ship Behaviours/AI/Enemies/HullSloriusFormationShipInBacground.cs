using System.Collections;
using UnityEngine;

public class HullSloriusFormationShipInBacground : Character
{
    [Header("��ũ��Ʈ")]
    public SpawnShip SpawnShip;

    //�⺻ ��ü ����
    public bool inPlanet; //�༺�� �ִ��� ����
    public bool isNariha; //������ �Լ����� ����
    public int ControsType;
    public float hitPoints; //�ǽð� HP�� ���� ���� public���� ó��
    public float DebrisSpeed;

    //����
    public bool ShieldDown = false; //���� �ı��Ǿ������� ���� ����
    public bool NoRicochet = false; //��ź�� ������ �� �ִ����� ���� ����
    public bool isDestroied = false; //�Լ��� �ı��Ǿ������� ���� ����

    //�Լ� ���� ����ȭ ����
    public bool Main1Left1Down = false;
    public bool Main1Right1Down = false;
    public bool Main2Left1Down = false;
    public bool Main2Right1Down = false;

    public GameObject Body1;
    public GameObject Body2;
    public GameObject Body2_1;

    public GameObject Main1Left1prefab;
    public GameObject Main1Right1prefab;
    public GameObject Main2Left1prefab;
    public GameObject Main2Right1prefab;
    public GameObject Main1Left1On;
    public GameObject Main1Right1On;
    public GameObject Main2Left1On;
    public GameObject Main2Right1On;
    public GameObject Main1Left1Debris;
    public GameObject Main1Right1Debris;
    public GameObject Main2Left1Debris;
    public GameObject Main2Right1Debris;

    public GameObject Turret1;
    public GameObject Turret2;

    public GameObject Booster;
    public GameObject Explosion;
    public GameObject EnergyBall;
    public GameObject Explosion2;

    private void OnEnable()
    {
        if (isNariha == false)
        {
            if (ControsType == 1) //���θ��
            {
                EnergyBall.SetActive(true);
                Explosion2.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        hitPoints = startingHitPoints;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<TearSloriusFormationShipInBackground>().GetHull();

        if (isNariha == false)
        {
            ShieldDown = false;
            NoRicochet = false;
        }
        isDestroied = false;

        Main1Left1Down = false;
        Main1Right1Down = false;
        Main2Left1Down = false;
        Main2Right1Down = false;
    }

    void Start()
    {
        hitPoints = startingHitPoints;
        GetComponent<TearSloriusFormationShipInBackground>().GetHull();
    }

    //���θ�� �Լ� ��ü ������ ���� �� �ı�
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if (isDestroied == false)
        {
            while (true)
            {
                hitPoints = hitPoints - damage;

                if (hitPoints <= float.Epsilon)
                {
                    if (isDestroied == false)
                    {
                        isDestroied = true;
                        if (inPlanet == true)
                        {
                            float RandomGravity = Random.Range(0.02f, 0.08f);
                            GetComponent<Rigidbody2D>().gravityScale = RandomGravity;
                        }
                        SpawnShip.ShipList.Remove(gameObject);
                        StartDestroy();
                        Destroy(gameObject, 60);
                    }
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
    }

    //�Լ� �ı� ����
    void StartDestroy()
    {
        this.gameObject.layer = 0;
        Main1Left1prefab.gameObject.layer = 0;
        Main1Right1prefab.gameObject.layer = 0;
        Main2Left1prefab.gameObject.layer = 0;
        Main2Right1prefab.gameObject.layer = 0;
        if (isNariha == false)
        {
            if (ControsType == 1) //���θ��
            {
                EnergyBall.SetActive(false);
                Explosion2.SetActive(true);
            }
        }
        GameObject NarihaExplosion1 = Instantiate(Explosion, transform.position, transform.rotation);
        NarihaExplosion1.transform.localScale = new Vector2 (transform.localScale.x * 2, transform.localScale.y * 2);
        Booster.SetActive(false);

        if (GetComponent<TearSloriusFormationShipInBackground>().Main1Left1HP > 0)
        {
            Main1Left1On.SetActive(true);
            Main1Left1prefab.SetActive(false);

            int Divide1 = Random.Range(0, 3);
            if (Divide1 == 0)
            {
                Main1Left1On.SetActive(false);
                Main1Left1Debris.SetActive(true);
            }
        }
        if (GetComponent<TearSloriusFormationShipInBackground>().Main1Right1HP > 0)
        {
            Main1Right1On.SetActive(true);
            Main1Right1prefab.SetActive(false);

            int Divide1 = Random.Range(0, 3);
            if (Divide1 == 0)
            {
                Main1Right1On.SetActive(false);
                Main1Right1Debris.SetActive(true);
            }

            if (isNariha == true)
            {
                Main1Left1prefab.SetActive(false);

                int Divide2 = Random.Range(0, 3);
                if (Divide2 == 0)
                {
                    Main1Left1On.SetActive(false);
                    Main1Left1Debris.SetActive(true);
                }
            }
        }
        if (GetComponent<TearSloriusFormationShipInBackground>().Main2Left1HP > 0)
        {
            Main2Left1On.SetActive(true);
            Main2Left1prefab.SetActive(false);

            int Divide1 = Random.Range(0, 3);
            if (Divide1 == 0)
            {
                Main2Left1On.SetActive(false);
                Main2Left1Debris.SetActive(true);

                if (GetComponent<TearSloriusFormationShipInBackground>().Main2Right1HP <= 0)
                {
                    Main1Left1On.SetActive(false);
                    Main1Left1Debris.SetActive(true);
                    Main1Right1On.SetActive(false);
                    Main1Right1Debris.SetActive(true);
                }
            }

            if (isNariha == false)
            {
                if (GetComponent<TearSloriusFormationShipInBackground>().Main2Right1HP <= 0)
                {
                    Main1Left1prefab.SetActive(false);
                    Main1Right1prefab.SetActive(false);
                }
            }
        }
        if (GetComponent<TearSloriusFormationShipInBackground>().Main2Right1HP > 0)
        {
            Main2Right1On.SetActive(true);
            Main2Right1prefab.SetActive(false);

            int Divide1 = Random.Range(0, 3);
            if (Divide1 == 0)
            {
                Main2Right1On.SetActive(false);
                Main2Right1Debris.SetActive(true);

                if (GetComponent<TearSloriusFormationShipInBackground>().Main2Left1HP <= 0)
                {
                    Main1Left1On.SetActive(false);
                    Main1Left1Debris.SetActive(true);
                    Main1Right1On.SetActive(false);
                    Main1Right1Debris.SetActive(true);
                }
            }

            if (isNariha == true)
            {
                Main2Left1prefab.SetActive(false);
            }
            else
            {
                if (GetComponent<TearSloriusFormationShipInBackground>().Main2Left1HP <= 0)
                {
                    Main1Left1prefab.SetActive(false);
                    Main1Right1prefab.SetActive(false);
                }
            }
        }

        int Divide = Random.Range(0, 3);

        if (Divide == 0)
        {
            Body2.gameObject.SetActive(false);
            Body2_1.gameObject.SetActive(true);
        }
        else
            Body2.GetComponent<TurnOffShipLight>().enabled = true;

        Body1.GetComponent<DebrisAction>().enabled = true;
        if (isNariha == true)
        {
            Turret1.GetComponent<NarihaTurretAttackSystemInBackground>().enabled = false;
            Turret2.GetComponent<NarihaTurretAttackSystemInBackground>().enabled = false;
        }
        else
        {
            Turret1.GetComponent<EnemyAttackSystemInBackground>().enabled = false;
            Turret2.GetComponent<EnemyAttackSystemInBackground>().enabled = false;
        }
    }
}