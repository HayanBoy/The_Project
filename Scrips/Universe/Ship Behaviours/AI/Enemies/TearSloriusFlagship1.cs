using System.Collections;
using UnityEngine;

public class TearSloriusFlagship1 : MonoBehaviour
{
    [Header("스크립트")]
    public HullSloriusFlagship1 HullSloriusFlagship1;
    LiveCommunicationSystem LiveCommunicationSystem;

    [Header("파편속도 조절용 스위치")]
    public bool isNariha; //나리하 함선인지 여부
    public int ControsType;
    public float DebrisSpeed;
    public bool isExplosion; //데미지 받은 요소가 폭발인지에 대한 여부

    [Header("함선 부위별 체력")]
    public bool ShieldDown = true;
    public float Main1Left1HP;
    public float Main1Left2HP;
    public float Main1Right1HP;
    public float Main1Right2HP;
    public float Main2Left1HP;
    public float Main2Left2HP;
    public float Main2Right1HP;
    public float Main2Right2HP;
    public float Main3Left1HP;
    public float Main3Right1HP;
    public float PartModuleHP; //부위별 체력

    [Header("함선 부위별 파괴시 스프라이트 끄기용")]
    public GameObject Main1Left1prefab;
    public GameObject Main1Left2prefab;
    public GameObject Main1Right1prefab;
    public GameObject Main1Right2prefab;
    public GameObject Main2Left1prefab;
    public GameObject Main2Left2prefab;
    public GameObject Main2Right1prefab;
    public GameObject Main2Right2prefab;
    public GameObject Main3Left1prefab;
    public GameObject Main3Right1prefab;

    [Header("함선 부위 프리팹")]
    public GameObject Main1Left1;
    public GameObject Main1Left2;
    public GameObject Main1Right1;
    public GameObject Main1Right2;
    public GameObject Main2Left1;
    public GameObject Main2Left2;
    public GameObject Main2Right1;
    public GameObject Main2Right2;
    public GameObject Main3Left1;
    public GameObject Main3Right1;

    [Header("함선 부위 프리팹 생성 위치")]
    public Transform Main1Left1Pos;
    public Transform Main1Left2Pos;
    public Transform Main1Right1Pos;
    public Transform Main1Right2Pos;
    public Transform Main2Left1Pos;
    public Transform Main2Left2Pos;
    public Transform Main2Right1Pos;
    public Transform Main2Right2Pos;
    public Transform Main3Left1Pos;
    public Transform Main3Right1Pos;

    [Header("함선 수리 부위 프리팹")]
    public GameObject Main1Left1Repair;
    public GameObject Main1Left2Repair;
    public GameObject Main1Right1Repair;
    public GameObject Main1Right2Repair;
    public GameObject Main2Left1Repair;
    public GameObject Main2Left2Repair;
    public GameObject Main2Right1Repair;
    public GameObject Main2Right2Repair;
    public GameObject Main3Left1Repair;
    public GameObject Main3Right1Repair;

    public bool GetHullMode = false;
    public int RepairNumber; //수리 중인 선체 번호. 한번에 하나씩만 수리하도록 조취

    GameObject Explosion;

    private void OnEnable()
    {
        if (Main1Left1Pos.gameObject.activeSelf == false)
            Main1Left1Pos.gameObject.SetActive(true);
        if (Main1Left2Pos.gameObject.activeSelf == false)
            Main1Left2Pos.gameObject.SetActive(true);
        if (Main1Right1Pos.gameObject.activeSelf == false)
            Main1Right1Pos.gameObject.SetActive(true);
        if (Main1Right2Pos.gameObject.activeSelf == false)
            Main1Right2Pos.gameObject.SetActive(true);
        if (Main2Left1Pos.gameObject.activeSelf == false)
            Main2Left1Pos.gameObject.SetActive(true);
        if (Main2Left2Pos.gameObject.activeSelf == false)
            Main2Left2Pos.gameObject.SetActive(true);
        if (Main2Right1Pos.gameObject.activeSelf == false)
            Main2Right1Pos.gameObject.SetActive(true);
        if (Main2Right2Pos.gameObject.activeSelf == false)
            Main2Right2Pos.gameObject.SetActive(true);
        if (Main3Left1Pos.gameObject.activeSelf == false)
            Main3Left1Pos.gameObject.SetActive(true);
        if (Main3Right1Pos.gameObject.activeSelf == false)
            Main3Right1Pos.gameObject.SetActive(true);

        if (isNariha == true)
            LiveCommunicationSystem = FindObjectOfType<LiveCommunicationSystem>();
    }

    private void OnDisable()
    {
        if (Main1Left1prefab.activeSelf == false)
            Main1Left1prefab.SetActive(true);
        if (Main1Left2prefab.activeSelf == false)
            Main1Left2prefab.SetActive(true);
        if (Main1Right1prefab.activeSelf == false)
            Main1Right1prefab.SetActive(true);
        if (Main1Right2prefab.activeSelf == false)
            Main1Right2prefab.SetActive(true);
        if (Main2Left1prefab.activeSelf == false)
            Main2Left1prefab.SetActive(true);
        if (Main2Left2prefab.activeSelf == false)
            Main2Left2prefab.SetActive(true);
        if (Main2Right1prefab.activeSelf == false)
            Main2Right1prefab.SetActive(true);
        if (Main2Right2prefab.activeSelf == false)
            Main2Right2prefab.SetActive(true);
        if (Main3Left1prefab.activeSelf == false)
            Main3Left1prefab.SetActive(true);
        if (Main3Right1prefab.activeSelf == false)
            Main3Right1prefab.SetActive(true);

        if (Main1Left1Pos.gameObject.activeSelf == true)
            Main1Left1Pos.gameObject.SetActive(false);
        if (Main1Left2Pos.gameObject.activeSelf == true)
            Main1Left2Pos.gameObject.SetActive(false);
        if (Main1Right1Pos.gameObject.activeSelf == true)
            Main1Right1Pos.gameObject.SetActive(false);
        if (Main1Right2Pos.gameObject.activeSelf == true)
            Main1Right2Pos.gameObject.SetActive(false);
        if (Main2Left1Pos.gameObject.activeSelf == true)
            Main2Left1Pos.gameObject.SetActive(false);
        if (Main2Left2Pos.gameObject.activeSelf == true)
            Main2Left2Pos.gameObject.SetActive(false);
        if (Main2Right1Pos.gameObject.activeSelf == true)
            Main2Right1Pos.gameObject.SetActive(false);
        if (Main2Right2Pos.gameObject.activeSelf == true)
            Main2Right2Pos.gameObject.SetActive(false);
        if (Main3Left1Pos.gameObject.activeSelf == true)
            Main3Left1Pos.gameObject.SetActive(false);
        if (Main3Right1Pos.gameObject.activeSelf == true)
            Main3Right1Pos.gameObject.SetActive(false);

        PartModuleHP = HullSloriusFlagship1.startingHitPoints / 10;
        Main1Left1HP = PartModuleHP;
        Main1Left2HP = PartModuleHP;
        Main1Right1HP = PartModuleHP;
        Main1Right2HP = PartModuleHP;
        Main2Left1HP = PartModuleHP;
        Main2Left2HP = PartModuleHP;
        Main2Right1HP = PartModuleHP;
        Main2Right2HP = PartModuleHP;
        Main3Left1HP = PartModuleHP;
        Main3Right1HP = PartModuleHP;
    }

    public void GetBattleData()
    {
        if (Main1Left1HP == 0)
        {
            Main1Left1Pos.gameObject.SetActive(false);
            Main1Left1prefab.SetActive(false);
        }
        if (Main1Left2HP == 0)
        {
            Main1Left2Pos.gameObject.SetActive(false);
            Main1Left2prefab.SetActive(false);
        }
        if (Main1Right1HP == 0)
        {
            Main1Right1Pos.gameObject.SetActive(false);
            Main1Right1prefab.SetActive(false);
        }
        if (Main1Right2HP == 0)
        {
            Main1Right2Pos.gameObject.SetActive(false);
            Main1Right2prefab.SetActive(false);
        }
        if (Main2Left1HP == 0)
        {
            Main2Left1Pos.gameObject.SetActive(false);
            Main2Left1prefab.SetActive(false);
        }
        if (Main2Left2HP == 0)
        {
            Main2Left2Pos.gameObject.SetActive(false);
            Main2Left2prefab.SetActive(false);
        }
        if (Main2Right1HP == 0)
        {
            Main2Right1Pos.gameObject.SetActive(false);
            Main2Right1prefab.SetActive(false);
        }
        if (Main2Right2HP == 0)
        {
            Main2Right2Pos.gameObject.SetActive(false);
            Main2Right2prefab.SetActive(false);
        }
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

    public void GetHull()
    {
        HullSloriusFlagship1 = GetComponent<HullSloriusFlagship1>();
        PartModuleHP = HullSloriusFlagship1.startingHitPoints / 10;
        Main1Left1HP = PartModuleHP;
        Main1Left2HP = PartModuleHP;
        Main1Right1HP = PartModuleHP;
        Main1Right2HP = PartModuleHP;
        Main2Left1HP = PartModuleHP;
        Main2Left2HP = PartModuleHP;
        Main2Right1HP = PartModuleHP;
        Main2Right2HP = PartModuleHP;
        Main3Left1HP = PartModuleHP;
        Main3Right1HP = PartModuleHP;
    }

    //슬로리어스 함선 Main1Left1Damage 데미지 적용 및 파괴
    public IEnumerator Main1Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main1Left1Down == false)
            {
                while (true)
                {
                    Main1Left1HP = Main1Left1HP - damage;

                    if (Main1Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main1Left1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main1Left1Down = true;
                            Main1Left1prefab.SetActive(false);
                            GameObject Main1Left1Destroy = Instantiate(Main1Left1, Main1Left1Pos.position, Main1Left1Pos.rotation);
                            Main1Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);

                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main1Left2Damage 데미지 적용 및 파괴
    public IEnumerator Main1Left2Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main1Left2Down == false)
            {
                while (true)
                {
                    Main1Left2HP = Main1Left2HP - damage;

                    if (Main1Left2HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main1Left2Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main1Left2Down = true;
                            Main1Left2prefab.SetActive(false);
                            GameObject Main1Left2Destroy = Instantiate(Main1Left2, Main1Left2Pos.position, Main1Left2Pos.rotation);
                            Main1Left2Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main1Right1Damage 데미지 적용 및 파괴
    public IEnumerator Main1Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main1Right1Down == false)
            {
                while (true)
                {
                    Main1Right1HP = Main1Right1HP - damage;

                    if (Main1Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main1Right1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main1Right1Down = true;
                            Main1Right1prefab.SetActive(false);
                            GameObject Main1Right1Destroy = Instantiate(Main1Right1, Main1Right1Pos.position, Main1Right1Pos.rotation);
                            Main1Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main1Right2Damage 데미지 적용 및 파괴
    public IEnumerator Main1Right2Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main1Right2Down == false)
            {
                while (true)
                {
                    Main1Right2HP = Main1Right2HP - damage;

                    if (Main1Right2HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main1Right2Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main1Right2Down = true;
                            Main1Right2prefab.SetActive(false);
                            GameObject Main1Right2Destroy = Instantiate(Main1Right2, Main1Right2Pos.position, Main1Right2Pos.rotation);
                            Main1Right2Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main2Left1Damage 데미지 적용 및 파괴
    public IEnumerator Main2Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main2Left1Down == false)
            {
                while (true)
                {
                    Main2Left1HP = Main2Left1HP - damage;

                    if (Main2Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main2Left1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main2Left1Down = true;
                            Main2Left1prefab.SetActive(false);
                            GameObject Main2Left1Destroy = Instantiate(Main2Left1, Main2Left1Pos.position, Main2Left1Pos.rotation);
                            Main2Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main2Left2Damage 데미지 적용 및 파괴
    public IEnumerator Main2Left2Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main2Left2Down == false)
            {
                while (true)
                {
                    Main2Left2HP = Main2Left2HP - damage;

                    if (Main2Left2HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main2Left1Pos.gameObject.SetActive(false);
                            Main2Left2Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main2Left1Down = true;
                            HullSloriusFlagship1.Main2Left2Down = true;
                            Main2Left1prefab.SetActive(false);
                            Main2Left2prefab.SetActive(false);
                            GameObject Main2Left2Destroy = Instantiate(Main2Left2, Main2Left2Pos.position, Main2Left2Pos.rotation);
                            Main2Left2Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main2Right1Damage 데미지 적용 및 파괴
    public IEnumerator Main2Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main2Right1Down == false)
            {
                while (true)
                {
                    Main2Right1HP = Main2Right1HP - damage;

                    if (Main2Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main2Right1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main2Right1Down = true;
                            Main2Right1prefab.SetActive(false);
                            GameObject Main2Right1Destroy = Instantiate(Main2Right1, Main2Right1Pos.position, Main2Right1Pos.rotation);
                            Main2Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main2Right2Damage 데미지 적용 및 파괴
    public IEnumerator Main2Right2Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main2Right2Down == false)
            {
                while (true)
                {
                    Main2Right2HP = Main2Right2HP - damage;

                    if (Main2Right2HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main2Right1Pos.gameObject.SetActive(false);
                            Main2Right2Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main2Right1Down = true;
                            HullSloriusFlagship1.Main2Right2Down = true;
                            Main2Right1prefab.SetActive(false);
                            Main2Right2prefab.SetActive(false);
                            GameObject Main2Right2Destroy = Instantiate(Main2Right2, Main2Right2Pos.position, Main2Right2Pos.rotation);
                            Main2Right2Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main3Left1Damage 데미지 적용 및 파괴
    public IEnumerator Main3Left1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main3Left1Down == false)
            {
                while (true)
                {
                    Main3Left1HP = Main3Left1HP - damage;

                    if (Main3Left1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main3Left1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main3Left1Down = true;
                            Main3Left1prefab.SetActive(false);
                            GameObject Main3Left1Destroy = Instantiate(Main3Left1, Main3Left1Pos.position, Main3Left1Pos.rotation);
                            Main3Left1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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

    //슬로리어스 함선 Main3Right1Damage 데미지 적용 및 파괴
    public IEnumerator Main3Right1Damage(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (HullSloriusFlagship1.Main3Right1Down == false)
            {
                while (true)
                {
                    Main3Right1HP = Main3Right1HP - damage;

                    if (Main3Right1HP <= float.Epsilon)
                    {
                        if (HullSloriusFlagship1.isDestroied == false)
                        {
                            Main3Right1Pos.gameObject.SetActive(false);
                            HullSloriusFlagship1.Main3Right1Down = true;
                            Main3Right1prefab.SetActive(false);
                            GameObject Main3Right1Destroy = Instantiate(Main3Right1, Main3Right1Pos.position, Main3Right1Pos.rotation);
                            Main3Right1Destroy.GetComponent<DebrisAction>().StartMoveDebris(DebrisSpeed, true, isExplosion);
                            if (isNariha == true)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(3.00f));
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
        //부위별 선체를 순차적으로 회복
        if (isNariha == true)
        {
            if (GetHullMode == true)
            {
                if (RepairNumber == 1)
                {
                    if (Main1Left1HP < PartModuleHP)
                    {
                        Main1Left1HP += Time.deltaTime * 250;
                        Main1Left1Repair.SetActive(true);
                    }
                    else if (Main1Left1HP >= PartModuleHP)
                    {
                        Main1Left1HP = PartModuleHP;
                        Main1Left1prefab.SetActive(true);
                        Main1Left1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Left1Down = false;
                        RepairNumber = 2;
                    }
                }
                else if (RepairNumber == 2)
                {
                    if (Main1Left2HP < PartModuleHP)
                    {
                        Main1Left2HP += Time.deltaTime * 250;
                        Main1Left2Repair.SetActive(true);
                    }
                    else if (Main1Left2HP >= PartModuleHP)
                    {
                        Main1Left2HP = PartModuleHP;
                        Main1Left2prefab.SetActive(true);
                        Main1Left2Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Left2Down = false;
                        RepairNumber = 3;
                    }
                }
                else if (RepairNumber == 3)
                {
                    if (Main1Right1HP < PartModuleHP)
                    {
                        Main1Right1HP += Time.deltaTime * 250;
                        Main1Right1Repair.SetActive(true);
                    }
                    else if (Main1Right1HP >= PartModuleHP)
                    {
                        Main1Right1HP = PartModuleHP;
                        Main1Right1prefab.SetActive(true);
                        Main1Right1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Right1Down = false;
                        RepairNumber = 4;
                    }
                }
                else if (RepairNumber == 4)
                {
                    if (Main1Right2HP < PartModuleHP)
                    {
                        Main1Right2HP += Time.deltaTime * 250;
                        Main1Right2Repair.SetActive(true);
                    }
                    else if (Main1Right2HP >= PartModuleHP)
                    {
                        Main1Right2HP = PartModuleHP;
                        Main1Right2prefab.SetActive(true);
                        Main1Right2Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Right2Down = false;
                        RepairNumber = 5;
                    }
                }
                else if (RepairNumber == 5)
                {
                    if (Main2Left1HP < PartModuleHP)
                    {
                        Main2Left1HP += Time.deltaTime * 250;
                        Main2Left1Repair.SetActive(true);
                    }
                    else if (Main2Left1HP >= PartModuleHP)
                    {
                        Main2Left1HP = PartModuleHP;
                        Main2Left1prefab.SetActive(true);
                        Main2Left1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Left1Down = false;
                        RepairNumber = 6;
                    }
                }
                else if (RepairNumber == 6)
                {
                    if (Main2Left2HP < PartModuleHP)
                    {
                        Main2Left2HP += Time.deltaTime * 250;
                        Main2Left2Repair.SetActive(true);
                    }
                    else if (Main2Left2HP >= PartModuleHP)
                    {
                        Main2Left2HP = PartModuleHP;
                        Main2Left2prefab.SetActive(true);
                        Main2Left2Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main1Left2Down = false;
                        RepairNumber = 7;
                    }
                }
                else if (RepairNumber == 7)
                {
                    if (Main2Right1HP < PartModuleHP)
                    {
                        Main2Right1HP += Time.deltaTime * 250;
                        Main2Right1Repair.SetActive(true);
                    }
                    else if (Main2Right1HP >= PartModuleHP)
                    {
                        Main2Right1HP = PartModuleHP;
                        Main2Right1prefab.SetActive(true);
                        Main2Right1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main2Right1Down = false;
                        RepairNumber = 8;
                    }
                }
                else if (RepairNumber == 8)
                {
                    if (Main2Right2HP < PartModuleHP)
                    {
                        Main2Right2HP += Time.deltaTime * 250;
                        Main2Right2Repair.SetActive(true);
                    }
                    else if (Main2Right2HP >= PartModuleHP)
                    {
                        Main2Right2HP = PartModuleHP;
                        Main2Right2prefab.SetActive(true);
                        Main2Right2Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main2Right2Down = false;
                        RepairNumber = 9;
                    }
                }
                else if (RepairNumber == 9)
                {
                    if (Main3Left1HP < PartModuleHP)
                    {
                        Main3Left1HP += Time.deltaTime * 250;
                        Main3Left1Repair.SetActive(true);
                    }
                    else if (Main3Left1HP >= PartModuleHP)
                    {
                        Main3Left1HP = PartModuleHP;
                        Main3Left1prefab.SetActive(true);
                        Main3Left1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main3Left1Down = false;
                        RepairNumber = 10;
                    }
                }
                else if (RepairNumber == 10)
                {
                    if (Main3Right1HP < PartModuleHP)
                    {
                        Main3Right1HP += Time.deltaTime * 250;
                        Main3Right1Repair.SetActive(true);
                    }
                    else if (Main3Right1HP >= PartModuleHP)
                    {
                        Main3Right1HP = PartModuleHP;
                        Main3Right1prefab.SetActive(true);
                        Main3Right1Repair.SetActive(false);
                        GetComponent<HullSloriusFlagship1>().Main3Right1Down = false;
                        RepairNumber = 0;
                        GetHullMode = false;
                    }
                }
            }
        }
    }
}