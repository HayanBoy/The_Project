using System.Collections;
using UnityEngine;

public class TearSloriusFormationShipInBackground : MonoBehaviour
{
    public HullSloriusFormationShipInBacground HullSloriusFormationShip1;
    public bool isNariha;
    public float DebrisSpeed;
    public bool isExplosion; //������ ���� ��Ұ� ���������� ���� ����

    //�Լ� ������ ü��
    public float Main1Left1HP;
    public float Main1Right1HP;
    public float Main2Left1HP;
    public float Main2Right1HP;

    //�Լ��� �ı��� ��������Ʈ �����
    public GameObject Main1Left1prefab;
    public GameObject Main1Right1prefab;
    public GameObject Main2Left1prefab;
    public GameObject Main2Right1prefab;

    //�Լ� ���� ������
    public GameObject Main1Left1;
    public GameObject Main1Right1;
    public GameObject Main2Left1;
    public GameObject Main2Right1;

    GameObject Explosion;

    private void OnDisable()
    {
        if (Main1Left1prefab.activeSelf == false)
            Main1Left1prefab.SetActive(true);
        if (Main1Right1prefab.activeSelf == false)
            Main1Right1prefab.SetActive(true);
        if (Main2Left1prefab.activeSelf == false)
            Main2Left1prefab.SetActive(true);
        if (Main2Right1prefab.activeSelf == false)
            Main2Right1prefab.SetActive(true);

        Main1Left1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main1Right1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main2Left1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main2Right1HP = HullSloriusFormationShip1.hitPoints / 5;
    }

    public void GetHull()
    {
        HullSloriusFormationShip1 = GetComponent<HullSloriusFormationShipInBacground>();
        Main1Left1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main1Right1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main2Left1HP = HullSloriusFormationShip1.hitPoints / 5;
        Main2Right1HP = HullSloriusFormationShip1.hitPoints / 5;
    }

    void Start()
    {

    }

    //���θ�� �Լ� Main1Left1Damage ������ ���� �� �ı�
    public IEnumerator Main1Left1Damage(int damage, float interval)
    {
        if (HullSloriusFormationShip1.Main1Left1Down == false)
        {
            while (true)
            {
                Main1Left1HP = Main1Left1HP - damage;

                if (Main1Left1HP <= float.Epsilon)
                {
                    HullSloriusFormationShip1.Main1Left1Down = true;
                    Main1Left1prefab.SetActive(false);
                    Main1Left1prefab.layer = 0;
                    Main1Left1.gameObject.SetActive(true);

                    if (isNariha == false)
                    {
                        if (Main2Right1HP <= 0)
                        {
                            Main1Right1prefab.SetActive(false);
                            Main1Right1prefab.layer = 0;
                            Main1Right1HP = 0;
                            Main1Right1.gameObject.SetActive(true);
                        }
                    }
                    if (GetComponent<HullSloriusFormationShipInBacground>().inPlanet == true)
                    {
                        float RandomGravity = Random.Range(0.02f, 0.08f);
                        Main1Left1.GetComponent<Rigidbody2D>().gravityScale = RandomGravity;
                    }
                    Main1Left1.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
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

    //���θ�� �Լ� Main1Right1Damage ������ ���� �� �ı�
    public IEnumerator Main1Right1Damage(int damage, float interval)
    {
        if (HullSloriusFormationShip1.Main1Right1Down == false)
        {
            while (true)
            {
                Main1Right1HP = Main1Right1HP - damage;

                if (Main1Right1HP <= float.Epsilon)
                {
                    if (isNariha == true)
                    {
                        HullSloriusFormationShip1.Main1Right1Down = true;
                        Main1Left1prefab.SetActive(false);
                        Main1Left1prefab.layer = 0;
                        Main1Left1HP = 0;
                        Main1Left1.gameObject.SetActive(true);
                        Main1Right1prefab.SetActive(false);
                        Main1Right1prefab.layer = 0;
                        Main1Right1.gameObject.SetActive(true);
                    }
                    else
                    {
                        HullSloriusFormationShip1.Main1Right1Down = true;
                        Main1Right1prefab.SetActive(false);
                        Main1Right1prefab.layer = 0;
                        Main1Right1.gameObject.SetActive(true);
                    }
                    if (GetComponent<HullSloriusFormationShipInBacground>().inPlanet == true)
                    {
                        float RandomGravity = Random.Range(0.02f, 0.08f);
                        Main1Right1.GetComponent<Rigidbody2D>().gravityScale = RandomGravity;
                    }
                    Main1Right1.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
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

    //���θ�� �Լ� Main2Left1Damage ������ ���� �� �ı�
    public IEnumerator Main2Left1Damage(int damage, float interval)
    {
        if (HullSloriusFormationShip1.Main2Left1Down == false)
        {
            while (true)
            {
                Main2Left1HP = Main2Left1HP - damage;

                if (Main2Left1HP <= float.Epsilon)
                {
                    HullSloriusFormationShip1.Main2Left1Down = true;
                    Main2Left1prefab.SetActive(false);
                    Main2Left1prefab.layer = 0;
                    Main2Left1.gameObject.SetActive(true);

                    if (isNariha == false && Main2Right1HP <= 0)
                    {
                        Main1Left1prefab.SetActive(false);
                        Main1Left1prefab.layer = 0;
                        Main1Left1HP = 0;
                        Main1Left1.gameObject.SetActive(true);
                        Main1Right1prefab.SetActive(false);
                        Main1Right1prefab.layer = 0;
                        Main1Right1HP = 0;
                        Main1Right1.gameObject.SetActive(true);
                    }
                    if (GetComponent<HullSloriusFormationShipInBacground>().inPlanet == true)
                    {
                        float RandomGravity = Random.Range(0.02f, 0.08f);
                        Main2Left1.GetComponent<Rigidbody2D>().gravityScale = RandomGravity;
                    }
                    Main2Left1.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
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

    //���θ�� �Լ� Main2Right1Damage ������ ���� �� �ı�
    public IEnumerator Main2Right1Damage(int damage, float interval)
    {
        if (HullSloriusFormationShip1.Main2Right1Down == false)
        {
            while (true)
            {
                Main2Right1HP = Main2Right1HP - damage;

                if (Main2Right1HP <= float.Epsilon)
                {
                    if (isNariha == true)
                    {
                        HullSloriusFormationShip1.Main2Right1Down = true;
                        Main2Left1prefab.SetActive(false);
                        Main2Left1prefab.layer = 0;
                        Main2Left1HP = 0;
                        Main2Left1.gameObject.SetActive(true);
                        Main2Right1prefab.SetActive(false);
                        Main2Right1prefab.layer = 0;
                        Main2Right1HP = 0;
                        Main2Right1.gameObject.SetActive(true);
                    }
                    else
                    {
                        HullSloriusFormationShip1.Main2Right1Down = true;
                        Main2Right1prefab.SetActive(false);
                        Main2Right1prefab.layer = 0;
                        Main2Right1.gameObject.SetActive(true);

                        if (Main1Left1HP <= 0)
                        {
                            Main1Right1prefab.SetActive(false);
                            Main1Right1prefab.layer = 0;
                            Main1Right1HP = 0;
                            Main1Right1.gameObject.SetActive(true);
                        }
                        if (Main2Left1HP <= 0)
                        {
                            Main1Left1prefab.SetActive(false);
                            Main1Left1prefab.layer = 0;
                            Main1Left1HP = 0;
                            Main1Left1.gameObject.SetActive(true);
                            Main1Right1prefab.SetActive(false);
                            Main1Right1prefab.layer = 0;
                            Main1Right1HP = 0;
                            Main1Right1.gameObject.SetActive(true);
                        }
                    }
                    if (GetComponent<HullSloriusFormationShipInBacground>().inPlanet == true)
                    {
                        float RandomGravity = Random.Range(0.02f, 0.08f);
                        Main2Right1.GetComponent<Rigidbody2D>().gravityScale = RandomGravity;
                    }
                    Main2Right1.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
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
}