using System.Collections;
using UnityEngine;

public class TearSloriusFormationShip1 : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public HullSloriusFormationShip1 HullSloriusFormationShip1;
    LiveCommunicationSystem LiveCommunicationSystem;

    public bool isNariha;

    [Header("����ӵ� ������ ����ġ")]
    public float DebrisSpeed;
    public bool isExplosion; //������ ���� ��Ұ� ���������� ���� ����

    [Header("�Լ� ������ ü��")]
    public bool ShieldDown = true; //���� �ı��Ǿ������� ���� ����
    public float Main1Left1HP;
    public float Main1Right1HP;
    public float Main2Left1HP;
    public float Main2Right1HP;
    public float Main3Left1HP;
    public float Main3Right1HP;
    public float PartModuleHP; //������ ü��

    [Header("�Լ� ������ �ı��� ��������Ʈ �����")]
    public GameObject Main1Left1prefab;
    public GameObject Main1Right1prefab;
    public GameObject Main2Left1prefab;
    public GameObject Main2Right1prefab;
    public GameObject Main3Left1prefab;
    public GameObject Main3Right1prefab;

    [Header("�Լ� ���� ������")]
    public GameObject Main1Left1;
    public GameObject Main1Right1;
    public GameObject Main2Left1;
    public GameObject Main2Right1;
    public GameObject Main3Left1;
    public GameObject Main3Right1;

    [Header("�Լ� ���� ������ ���� ��ġ")]
    public Transform Main1Left1Pos;
    public Transform Main1Right1Pos;
    public Transform Main2Left1Pos;
    public Transform Main2Right1Pos;
    public Transform Main3Left1Pos;
    public Transform Main3Right1Pos;

    [Header("�Լ� ���� ���� ������")]
    public GameObject Main1Left1Repair;
    public GameObject Main1Right1Repair;
    public GameObject Main2Left1Repair;
    public GameObject Main2Right1Repair;
    public GameObject Main3Left1Repair;
    public GameObject Main3Right1Repair;

    public bool GetHullMode = false;
    public int RepairNumber; //���� ���� ��ü ��ȣ. �ѹ��� �ϳ����� �����ϵ��� ����

    GameObject Explosion;

    private void OnEnable()
    {
        if (Main1Left1Pos.gameObject.activeSelf == false)
            Main1Left1Pos.gameObject.SetActive(true);
        if (Main1Right1Pos.gameObject.activeSelf == false)
            Main1Right1Pos.gameObject.SetActive(true);
        if (Main2Left1Pos.gameObject.activeSelf == false)
            Main2Left1Pos.gameObject.SetActive(true);
        if (Main2Right1Pos.gameObject.activeSelf == false)
            Main2Right1Pos.gameObject.SetActive(true);
        if (Main3Left1Pos != null)
        {
            if (Main3Left1Pos.gameObject.activeSelf == false)
                Main3Left1Pos.gameObject.SetActive(true);
        }
        if (Main3Right1Pos != null)
        {
            if (Main3Right1Pos.gameObject.activeSelf == false)
                Main3Right1Pos.gameObject.SetActive(true);
        }

        if (isNariha == true)
            LiveCommunicationSystem = FindObjectOfType<LiveCommunicationSystem>();
    }

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

        if (Main1Left1Pos.gameObject.activeSelf == true)
            Main1Left1Pos.gameObject.SetActive(false);
        if (Main1Right1Pos.gameObject.activeSelf == true)
            Main1Right1Pos.gameObject.SetActive(false);
        if (Main2Left1Pos.gameObject.activeSelf == true)
            Main2Left1Pos.gameObject.SetActive(false);
        if (Main2Right1Pos.gameObject.activeSelf == true)
            Main2Right1Pos.gameObject.SetActive(false);
        if (Main3Left1Pos != null)
        {
            if (Main3Left1Pos.gameObject.activeSelf == true)
                Main3Left1Pos.gameObject.SetActive(false);
        }
        if (Main3Right1Pos != null)
        {
            if (Main3Right1Pos.gameObject.activeSelf == true)
                Main3Right1Pos.gameObject.SetActive(false);
        }

        PartModuleHP = HullSloriusFormationShip1.startingHitPoints / 10;
        Main1Left1HP = PartModuleHP;
        Main1Right1HP = PartModuleHP;
        Main2Left1HP = PartModuleHP;
        Main2Right1HP = PartModuleHP;
        if (Main3Left1Pos != null)
            Main3Left1HP = PartModuleHP;
        if (Main3Right1Pos != null)
            Main3Right1HP = PartModuleHP;
    }

    public void GetBattleData()
    {
        if (Main1Left1HP == 0)
        {
            Main1Left1Pos.gameObject.SetActive(false);
            Main1Left1prefab.SetActive(false);
        }
        if (Main1Right1HP == 0)
        {
            Main1Right1Pos.gameObject.SetActive(false);
            Main1Right1prefab.SetActive(false);
        }
        if (Main2Left1HP == 0)
        {
            Main2Left1Pos.gameObject.SetActive(false);
            Main2Left1prefab.SetActive(false);
        }
        if (Main2Right1HP == 0)
        {
            Main2Right1Pos.gameObject.SetActive(false);
            Main2Right1prefab.SetActive(false);
        }

        if (isNariha == true)
        {
            if (GetComponent<ShipRTS>().ShipNumber > 2)
            {
                if (Main3Left1HP == 0)
                {
                    Main3Left1Pos.gameObject.SetActive(false);
                    Main3Left1prefab.SetActive(false);
                }
                if (Main3Right1HP == 0)
                {
                    Main3Right1Pos.gameObject.SetActive(false);
                    Main3Right1prefab.SetActive(false);
                }
            }
        }
    }

    public void GetHull()
    {
        HullSloriusFormationShip1 = GetComponent<HullSloriusFormationShip1>();
        PartModuleHP = HullSloriusFormationShip1.startingHitPoints / 10;
        Main1Left1HP = PartModuleHP;
        Main1Right1HP = PartModuleHP;
        Main2Left1HP = PartModuleHP;
        Main2Right1HP = PartModuleHP;
        if (Main3Left1Pos != null)
            Main3Left1HP = PartModuleHP;
        if (Main3Right1Pos != null)
            Main3Right1HP = PartModuleHP;
    }

    //���θ�� �Լ� Main1Left1Damage ������ ���� �� �ı�
    public IEnumerator Main1Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main1Left1Down == false)
            {
                while (true)
                {
                    Main1Left1HP = Main1Left1HP - damage;

                    if (Main1Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main1Left1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main1Left1Down = true;
                            Main1Left1prefab.SetActive(false);
                            GameObject Main1Left1Destroy = Instantiate(Main1Left1, Main1Left1Pos.position, Main1Left1Pos.rotation);
                            Main1Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    //���θ�� �Լ� Main1Right1Damage ������ ���� �� �ı�
    public IEnumerator Main1Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main1Right1Down == false)
            {
                while (true)
                {
                    Main1Right1HP = Main1Right1HP - damage;

                    if (Main1Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main1Right1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main1Right1Down = true;
                            Main1Right1prefab.SetActive(false);
                            GameObject Main1Right1Destroy = Instantiate(Main1Right1, Main1Right1Pos.position, Main1Right1Pos.rotation);
                            Main1Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    //���θ�� �Լ� Main2Left1Damage ������ ���� �� �ı�
    public IEnumerator Main2Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main2Left1Down == false)
            {
                while (true)
                {
                    Main2Left1HP = Main2Left1HP - damage;

                    if (Main2Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main2Left1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main2Left1Down = true;
                            Main2Left1prefab.SetActive(false);
                            GameObject Main2Left1Destroy = Instantiate(Main2Left1, Main2Left1Pos.position, Main2Left1Pos.rotation);
                            Main2Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    //���θ�� �Լ� Main2Right1Damage ������ ���� �� �ı�
    public IEnumerator Main2Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main2Right1Down == false)
            {
                while (true)
                {
                    Main2Right1HP = Main2Right1HP - damage;

                    if (Main2Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main2Right1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main2Right1Down = true;
                            Main2Right1prefab.SetActive(false);
                            GameObject Main2Right1Destroy = Instantiate(Main2Right1, Main2Right1Pos.position, Main2Right1Pos.rotation);
                            Main2Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    //���θ�� �Լ� Main2Left1Damage ������ ���� �� �ı�
    public IEnumerator Main3Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main3Left1Down == false)
            {
                while (true)
                {
                    Main3Left1HP = Main3Left1HP - damage;

                    if (Main3Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main3Left1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main3Left1Down = true;
                            Main3Left1prefab.SetActive(false);
                            GameObject Main3Left1Destroy = Instantiate(Main3Left1, Main3Left1Pos.position, Main3Left1Pos.rotation);
                            Main3Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    //���θ�� �Լ� Main2Right1Damage ������ ���� �� �ı�
    public IEnumerator Main3Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFormationShip1.Main3Right1Down == false)
            {
                while (true)
                {
                    Main3Right1HP = Main3Right1HP - damage;

                    if (Main3Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFormationShip1.isDestroied == false)
                        {
                            Main3Right1Pos.gameObject.SetActive(false);
                            HullSloriusFormationShip1.Main3Right1Down = true;
                            Main3Right1prefab.SetActive(false);
                            GameObject Main3Right1Destroy = Instantiate(Main3Right1, Main3Right1Pos.position, Main3Right1Pos.rotation);
                            Main3Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                            {
                                if (GetComponent<HullSloriusFormationShip1>().isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(5.00f));
                            }
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
    }

    private void Update()
    {
        //������ ��ü�� ���������� ȸ��
        if (isNariha == true)
        {
            if (GetHullMode == true)
            {
                if (RepairNumber == 1)
                {
                    if (Main1Left1HP < PartModuleHP)
                    {
                        Main1Left1HP += Time.deltaTime * 100;
                        Main1Left1Repair.SetActive(true);
                    }
                    else if (Main1Left1HP >= PartModuleHP)
                    {
                        Main1Left1HP = PartModuleHP;
                        Main1Left1prefab.SetActive(true);
                        Main1Left1Repair.SetActive(false);
                        GetComponent<HullSloriusFormationShip1>().Main1Left1Down = false;
                        RepairNumber = 2;
                    }
                }
                else if (RepairNumber == 2)
                {
                    if (Main1Right1HP < PartModuleHP)
                    {
                        Main1Right1HP += Time.deltaTime * 100;
                        Main1Right1Repair.SetActive(true);
                    }
                    else if (Main1Right1HP >= PartModuleHP)
                    {
                        Main1Right1HP = PartModuleHP;
                        Main1Right1prefab.SetActive(true);
                        Main1Right1Repair.SetActive(false);
                        GetComponent<HullSloriusFormationShip1>().Main1Right1Down = false;
                        RepairNumber = 3;
                    }
                }
                else if (RepairNumber == 3)
                {
                    if (Main2Left1HP < PartModuleHP)
                    {
                        Main2Left1HP += Time.deltaTime * 100;
                        Main2Left1Repair.SetActive(true);
                    }
                    else if (Main2Left1HP >= PartModuleHP)
                    {
                        Main2Left1HP = PartModuleHP;
                        Main2Left1prefab.SetActive(true);
                        Main2Left1Repair.SetActive(false);
                        GetComponent<HullSloriusFormationShip1>().Main1Left1Down = false;
                        RepairNumber = 4;
                    }
                }
                else if (RepairNumber == 4)
                {
                    if (Main2Right1HP < PartModuleHP)
                    {
                        Main2Right1HP += Time.deltaTime * 100;
                        Main2Right1Repair.SetActive(true);
                    }
                    else if (Main2Right1HP >= PartModuleHP)
                    {
                        Main2Right1HP = PartModuleHP;
                        Main2Right1prefab.SetActive(true);
                        Main2Right1Repair.SetActive(false);
                        GetComponent<HullSloriusFormationShip1>().Main2Right1Down = false;
                        if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
                            RepairNumber = 5;
                        else if (GetComponent<ShipRTS>().ShipNumber == 2)
                        {
                            RepairNumber = 0;
                            GetHullMode = false;
                        }
                    }
                }
                if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
                {
                    if (RepairNumber == 5)
                    {
                        if (Main3Left1HP < PartModuleHP)
                        {
                            Main3Left1HP += Time.deltaTime * 100;
                            Main3Left1Repair.SetActive(true);
                        }
                        else if (Main3Left1HP >= PartModuleHP)
                        {
                            Main3Left1HP = PartModuleHP;
                            Main3Left1prefab.SetActive(true);
                            Main3Left1Repair.SetActive(false);
                            GetComponent<HullSloriusFormationShip1>().Main3Left1Down = false;
                            RepairNumber = 6;
                        }
                    }
                    else if (RepairNumber == 6)
                    {
                        if (Main3Right1HP < PartModuleHP)
                        {
                            Main3Right1HP += Time.deltaTime * 100;
                            Main3Right1Repair.SetActive(true);
                        }
                        else if (Main3Right1HP >= PartModuleHP)
                        {
                            Main3Right1HP = PartModuleHP;
                            Main3Right1prefab.SetActive(true);
                            Main3Right1Repair.SetActive(false);
                            GetComponent<HullSloriusFormationShip1>().Main3Right1Down = false;
                            RepairNumber = 0;
                            GetHullMode = false;
                        }
                    }
                }
            }
        }
    }
}