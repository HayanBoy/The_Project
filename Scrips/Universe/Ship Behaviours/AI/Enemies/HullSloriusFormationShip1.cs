using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HullSloriusFormationShip1 : Character
{
    LiveCommunicationSystem LiveCommunicationSystem;
    CashResourceSystem CashResourceSystem;

    [Header("기본 선체 정보")]
    public bool isNariha; //나리하 함선인지 여부
    public bool isOurforce; //AI 아군인지 여부
    public int NarihaType; //나리하 함선 타입. 1 = 편대함, 2 = 방패함, 3 = 우주모함
    public int ControsType; //종족 정보. 1 = 슬로리어스, 2 = 칸타크리
    public float hitPoints; //실시간 HP를 보기 위해 public으로 처리
    public float DebrisSpeed;

    [Header("함선 상태")]
    public bool ShieldDown = false; //방어막이 파괴되었는지에 대한 여부
    public bool NoRicochet = false; //도탄을 무시할 수 있는지에 대한 여부
    public bool isDestroied = false; //함선이 파괴되었는지에 대한 여부
    public bool GetHull = false; //기함으로부터 수리 명령을 받은 여부
    private float GetHullComplete = 1;
    private float GetHullComplete2;

    [Header("함선 부위 무력화 여부")]
    public bool Main1Left1Down = false;
    public bool Main1Right1Down = false;
    public bool Main2Left1Down = false;
    public bool Main2Right1Down = false;
    public bool Main3Left1Down = false;
    public bool Main3Right1Down = false;

    [Header("함선 부위 파괴시 스프라이트 끄기")]
    public GameObject Debrisprefab;

    [Header("함선 파괴시 발생하는 메인 파편 프리팹")]
    public GameObject Debris1;
    public GameObject Debris1_1;
    public GameObject Debris2;
    public GameObject Debris2_1;

    [Header("함선 파괴시 파편 프리팹의 생성 위치")]
    public Transform Debris1Pos;

    [Header("칸타크리 전용 함선 파괴시 스프라이트 끄기")]
    public GameObject KantakriDebris1;
    public GameObject KantakriDebris2;

    [Header("함포 화재")]
    public bool TurretDown1 = false;
    public GameObject TurretFire1;
    public GameObject TurretFire2;

    [Header("선체 수리")]
    public GameObject RepairEffect;

    [Header("자산 획득")]
    public GameObject AssetGainTextPrefab;

    public List<int> TurretDownList = new List<int>(); //무력화된 함포 리스트

    GameObject Explosion;

    void OnDisable()
    {
        if (Debrisprefab.activeSelf == false)
            Debrisprefab.SetActive(true);

        hitPoints = startingHitPoints;
        GetComponent<TearSloriusFormationShip1>().GetHull();

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
        Main3Left1Down = false;
        Main3Right1Down = false;

        if (KantakriDebris1 != null)
            KantakriDebris1.SetActive(false);
        if (KantakriDebris2 != null)
            KantakriDebris2.SetActive(false);
    }

    //업그레이드 적용
    public void UpgradePatch()
    {
        if (isOurforce == false)
        {
            if (GetComponent<ShipRTS>().ShipNumber == 2) //편대함
            {
                startingHitPoints = UpgradeDataSystem.instance.FormationHitPoints;
                hitPoints = UpgradeDataSystem.instance.FormationHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.FormationHitPoints;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 3) //방패함
            {
                startingHitPoints = UpgradeDataSystem.instance.ShieldShipHitPoints;
                hitPoints = UpgradeDataSystem.instance.ShieldShipHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.ShieldShipHitPoints;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 4) //우주모함
            {
                startingHitPoints = UpgradeDataSystem.instance.CarrierHitPoints;
                hitPoints = UpgradeDataSystem.instance.CarrierHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.CarrierHitPoints;
            }
        }
        else
        {
            startingHitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
            hitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
            maxHitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
        }
        GetComponent<TearSloriusFormationShip1>().GetHull();
    }

    //업그레이드만 불러오기
    public void LoadPatch()
    {
        if (isOurforce == false)
        {
            if (GetComponent<ShipRTS>().ShipNumber == 2) //편대함
            {
                startingHitPoints = UpgradeDataSystem.instance.FormationHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.FormationHitPoints;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 3) //방패함
            {
                startingHitPoints = UpgradeDataSystem.instance.ShieldShipHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.ShieldShipHitPoints;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 4) //우주모함
            {
                startingHitPoints = UpgradeDataSystem.instance.CarrierHitPoints;
                maxHitPoints = UpgradeDataSystem.instance.CarrierHitPoints;
            }
        }
        else
        {
            startingHitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
            hitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
            maxHitPoints = UpgradeDataSystem.instance.FormationHitPoints * 0.7f;
        }
        GetComponent<TearSloriusFormationShip1>().GetHull();
    }

    //적 레벨 적용
    public void EnemyLevelPatch()
    {
        CashResourceSystem = FindObjectOfType<CashResourceSystem>();
        if (ControsType == 1) //슬로리어스
        {
            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                startingHitPoints = 4000;
                hitPoints = 4000;
                maxHitPoints = 4000;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 7500;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 7500;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                startingHitPoints = 6000;
                hitPoints = 6000;
                maxHitPoints = 6000;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 11250;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 11250;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                startingHitPoints = 8000;
                hitPoints = 8000;
                maxHitPoints = 8000;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 15000;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 15000;
            }
        }
        else if (ControsType == 2) //칸타크리
        {
            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                startingHitPoints = 9000;
                hitPoints = 9000;
                maxHitPoints = 9000;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                startingHitPoints = 13500;
                hitPoints = 13500;
                maxHitPoints = 13500;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                startingHitPoints = 18000;
                hitPoints = 18000;
                maxHitPoints = 18000;
            }
        }
        GetComponent<TearSloriusFormationShip1>().GetHull();
    }

    void Start()
    {
        if (isNariha == true)
        {
            LiveCommunicationSystem = FindObjectOfType<LiveCommunicationSystem>();
        }
    }

    private void Update()
    {
        //선체 회복
        if (isNariha == true)
        {
            if (GetHull == true)
            {
                if (GetComponent<MoveVelocity>().WarpDriveReady == false && GetComponent<MoveVelocity>().WarpDriveActive == false)
                {
                    if (hitPoints < startingHitPoints)
                    {
                        hitPoints += Time.deltaTime * 100;

                        if (GetComponent<ShipRTS>().ShipNumber == 2)
                        {
                            if (hitPoints >= startingHitPoints * 0.35f && TurretDown1 == true)
                            {
                                TurretDown1 = false;
                                RepairTurret();
                            }
                        }
                        if (GetHullComplete2 == 0)
                        {
                            GetHullComplete2 += Time.deltaTime;
                            GetHullComplete = 0;
                            GetComponent<TearSloriusFormationShip1>().GetHullMode = true;
                            GetComponent<TearSloriusFormationShip1>().RepairNumber = 1;
                            RepairEffect.SetActive(true);
                            if (GetComponent<ShipRTS>().ShipNumber == 2)
                                GetComponent<Animator>().SetFloat("Repair, Nariha ship", 2);
                            else if (GetComponent<ShipRTS>().ShipNumber == 3)
                                GetComponent<Animator>().SetFloat("Repair, Nariha ship", 3);
                            else if (GetComponent<ShipRTS>().ShipNumber == 4)
                                GetComponent<Animator>().SetFloat("Repair, Nariha ship", 4);
                        }
                    }
                    else if (hitPoints >= startingHitPoints && GetHullComplete2 > 0) //수리 완료
                    {
                        if (GetHullComplete == 0)
                        {
                            GetHullComplete += Time.deltaTime;
                            GetHullComplete2 = 0;
                            hitPoints = startingHitPoints;
                            RepairComplete();
                            GetHull = false;
                            GetComponent<TearSloriusFormationShip1>().GetHullMode = false;
                            RepairEffect.SetActive(false);
                            GetComponent<Animator>().SetFloat("Repair, Nariha ship", 0);
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(7.02f));
                        }
                    }
                }
            }
        }
    }

    //회복 취소 명령
    public void RepairCancel()
    {
        GetHullComplete = 0;
        GetHullComplete2 = 0;
        GetHull = false;
        GetComponent<TearSloriusFormationShip1>().GetHullMode = false;
        RepairEffect.SetActive(false);
        GetComponent<Animator>().SetFloat("Repair, Nariha ship", 0);

        if (GetComponent<TearSloriusFormationShip1>().Main1Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main1Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main1Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main1Right1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main2Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main2Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main2Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main2Right1Repair.SetActive(false);
        if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
        {
            if (GetComponent<TearSloriusFormationShip1>().Main3Left1Repair.activeSelf == true)
                GetComponent<TearSloriusFormationShip1>().Main3Left1Repair.SetActive(false);
            if (GetComponent<TearSloriusFormationShip1>().Main3Right1Repair.activeSelf == true)
                GetComponent<TearSloriusFormationShip1>().Main3Right1Repair.SetActive(false);
        }
    }

    //슬로리어스 함선 전체 데미지 적용 및 파괴
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        if (ShieldDown == true)
        {
            if (isDestroied == false)
            {
                while (true)
                {
                    hitPoints = hitPoints - damage;

                    //일정 수치 이하부터 함포가 랜덤으로 무력화
                    if (isNariha == true)
                    {
                        if (isOurforce == false)
                        {
                            if (GetComponent<ShipRTS>().ShipNumber == 2)
                            {
                                if (hitPoints < startingHitPoints * 0.35f && TurretDown1 == false)
                                {
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(6.00f));
                                    TurretDown1 = true;
                                    CannonDestroy();
                                }
                            }
                        }
                        else
                        {
                            if (hitPoints < startingHitPoints * 0.35f && TurretDown1 == false)
                            {
                                TurretDown1 = true;
                                CannonDestroy();
                            }
                        }
                    }
                    else
                    {
                        if (hitPoints < startingHitPoints * 0.5f && TurretDown1 == false)
                        {
                            TurretDown1 = true;
                            CannonDestroy();
                        }
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        if (isDestroied == false)
                        {
                            isDestroied = true;
                            if (isNariha == true)
                            {
                                if (isOurforce == false)
                                    StartCoroutine(LiveCommunicationSystem.SubCommunication(8.00f));
                            }
                            StartDestroy();
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

    //함선 파괴 연출
    void StartDestroy()
    {
        if (isNariha == true)
        {
            if (isOurforce == false)
            {
                if (GetComponent<MoveVelocity>().MyFlagship != null)
                {
                    GetComponent<MoveVelocity>().MyFlagship.GetComponent<FollowShipManager>().ShipList.Remove(gameObject);
                    GetComponent<MoveVelocity>().MyFlagship.GetComponent<FollowShipManager>().ShipAccount--;
                }
            }
            if (NarihaType == 1) //나리하 편대함1
            {
                ScoreManager.instance.LostNarihaFormationShipCnt++;
                int Divide = Random.Range(0, 2);

                if (Divide == 0)
                {
                    GameObject Destroy2 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy4 = Instantiate(Debris1_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
                else
                {
                    GameObject Destroy4 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy3 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy3.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }

            else if (NarihaType == 2) //나리하 방패함1
            {
                ScoreManager.instance.LostNarihaTacticalShipCnt++;
                int Divide = Random.Range(0, 2);

                if (Divide == 0)
                {
                    GameObject Destroy1 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy1.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy2 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
                else
                {
                    GameObject Destroy3 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy3.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }

            else if (NarihaType == 3) //나리하 우주모함1
            {
                ScoreManager.instance.LostNarihaTacticalShipCnt++;
                int Divide = Random.Range(0, 2);

                if (Divide == 0)
                {
                    GameObject Destroy1 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy1.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy2 = Instantiate(Debris1_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
                else
                {
                    GameObject Destroy1 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy1.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy2 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy1.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }
        }
        else
        {
            ScoreManager.instance.EnemyAllFormationShipCnt++;
            float Glopaoros = 0;
            float CR = 0;
            float Taritronic = 0;

            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                Glopaoros = Random.Range(75, 100);
                CR = Random.Range(100, 150);
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                Glopaoros = Random.Range(100, 125);
                CR = Random.Range(150, 200);
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                Glopaoros = Random.Range(125, 150);
                CR = Random.Range(200, 250);
            }
            CashResourceSystem.ShipBattleGainStart(Glopaoros, CR, Taritronic);
            GameObject AssetGain = Instantiate(AssetGainTextPrefab, transform.position, Quaternion.identity);
            AssetGain.transform.Find("Active/Glopaoros (1)").GetComponent<TextMesh>().text = string.Format("+{0}", Glopaoros);
            AssetGain.transform.Find("Active/Glopaoros").GetComponent<TextMesh>().text = string.Format("+{0}", Glopaoros);
            AssetGain.transform.Find("Active/Construction Resource (1)").GetComponent<TextMesh>().text = string.Format("+{0}", CR);
            AssetGain.transform.Find("Active/Construction Resource").GetComponent<TextMesh>().text = string.Format("+{0}", CR);
            Destroy(AssetGain, 3);

            AIShipManager.instance.EnemiesFormationShipList.Remove(this.gameObject);

            if (GetComponent<EnemyShipLevelInformation>().isBattleSite == true) //전투 지역에 소속시, 해당 전투 지역에서 함선을 제외
            {
                GetComponent<EnemyShipLevelInformation>().Zone.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Remove(gameObject);
            }
            if (GetComponent<EnemyShipBehavior>().MyFlagship != null)
            {
                GetComponent<EnemyShipBehavior>().MyFlagship.GetComponent<EnemyFollowShipManager>().ShipList.Remove(gameObject);
                GetComponent<EnemyShipBehavior>().MyFlagship.GetComponent<EnemyFollowShipManager>().ShipAccount--;
            }
            if (ControsType == 1) //슬로리어스 편대함1
            {
                ScoreManager.instance.EnemySloriusFormationShipCnt++;
                int Divide = Random.Range(0, 2);

                if (Divide == 0)
                {
                    GameObject Destroy2 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy4 = Instantiate(Debris1_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
                else
                {
                    GameObject Destroy4 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy3 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy3.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }
            else if (ControsType == 2) //칸타크리 편대함1
            {
                ScoreManager.instance.EnemyKantakriFormationShipCnt++;
                GameObject Destroy1 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy1.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy1.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy2 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

            }
        }

        Debrisprefab.SetActive(false);
        Destroy(gameObject);
    }

    //함포 무작위 무력화
    void CannonDestroy()
    {
        int RandomCannon = Random.Range(0, 2);

        if (RandomCannon == 0)
        {
            if (isNariha == true)
            {
                if (isOurforce == false)
                {
                    if (GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire1.SetActive(true);
                        TurretDownList.Add(1);
                    }
                }
                else
                {
                    if (GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().enabled == true)
                    {
                        GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().canAttack = false;
                        GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().TargetShip = null;
                        GetComponent<OurForceShipBehavior>().Turret1.GetComponent<OurForceAttackSystem>().enabled = false;
                        TurretFire1.SetActive(true);
                        TurretDownList.Add(1);
                    }
                }
            }
            else
            {
                if (GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().enabled == true)
                {
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().canAttack = false;
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().TargetShip = null;
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().enabled = false;
                    TurretFire1.SetActive(true);
                    TurretDownList.Add(1);
                }
            }
        }
        else if (RandomCannon == 1)
        {
            if (isNariha == true)
            {
                if (isOurforce == false)
                {
                    if (GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire2.SetActive(true);
                        TurretDownList.Add(2);
                    }
                }
                else
                {
                    if (GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().enabled == true)
                    {
                        GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().canAttack = false;
                        GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().TargetShip = null;
                        GetComponent<OurForceShipBehavior>().Turret2.GetComponent<OurForceAttackSystem>().enabled = false;
                        TurretFire2.SetActive(true);
                        TurretDownList.Add(2);
                    }
                }
            }
            else
            {
                if (GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().enabled == true)
                {
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().canAttack = false;
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().TargetShip = null;
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().enabled = false;
                    TurretFire2.SetActive(true);
                    TurretDownList.Add(2);
                }
            }
        }
    }

    //함포 복구
    void RepairTurret()
    {
        if (TurretDownList[0] == 1)
        {
            TurretFire1.SetActive(false);
            GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
        else if (TurretDownList[0] == 2)
        {
            TurretFire2.SetActive(false);
            GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
    }

    //무력화된 함포 정보 가져오기
    public void BringTurretDestroy()
    {
        if (TurretDownList.Count > 0)
        {
            TurretDown1 = true;

            if (isNariha == true)
            {
                if (TurretDownList[0] == 1)
                {
                    GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                    GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                    GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                    TurretFire1.SetActive(true);
                }
                else if (TurretDownList[0] == 2)
                {
                    GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                    GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                    GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                    TurretFire2.SetActive(true);
                }
            }
            else
            {
                if (TurretDownList[0] == 1)
                {
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().canAttack = false;
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().TargetShip = null;
                    GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().enabled = false;
                    TurretFire1.SetActive(true);
                }
                else if (TurretDownList[0] == 2)
                {
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().canAttack = false;
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().TargetShip = null;
                    GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().enabled = false;
                    TurretFire2.SetActive(true);
                }
            }
        }
    }

    //모든 선체 수리 완료
    public void RepairComplete()
    {
        GetComponent<TearSloriusFormationShip1>().Main1Left1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
        GetComponent<TearSloriusFormationShip1>().Main1Right1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
        GetComponent<TearSloriusFormationShip1>().Main2Left1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
        GetComponent<TearSloriusFormationShip1>().Main2Right1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
        if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
        {
            GetComponent<TearSloriusFormationShip1>().Main3Left1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
            GetComponent<TearSloriusFormationShip1>().Main3Right1HP = GetComponent<TearSloriusFormationShip1>().PartModuleHP;
        }

        Main1Left1Down = false;
        Main1Right1Down = false;
        Main2Left1Down = false;
        Main2Right1Down = false;
        Main3Left1Down = false;
        Main3Right1Down = false;

        if (GetComponent<TearSloriusFormationShip1>().Main1Left1prefab.activeSelf == false)
            GetComponent<TearSloriusFormationShip1>().Main1Left1prefab.SetActive(true);
        if (GetComponent<TearSloriusFormationShip1>().Main1Right1prefab.activeSelf == false)
            GetComponent<TearSloriusFormationShip1>().Main1Right1prefab.SetActive(true);
        if (GetComponent<TearSloriusFormationShip1>().Main2Left1prefab.activeSelf == false)
            GetComponent<TearSloriusFormationShip1>().Main2Left1prefab.SetActive(true);
        if (GetComponent<TearSloriusFormationShip1>().Main2Right1prefab.activeSelf == false)
            GetComponent<TearSloriusFormationShip1>().Main2Right1prefab.SetActive(true);
        if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
        {
            if (GetComponent<TearSloriusFormationShip1>().Main3Left1prefab.activeSelf == false)
                GetComponent<TearSloriusFormationShip1>().Main3Left1prefab.SetActive(true);
            if (GetComponent<TearSloriusFormationShip1>().Main3Right1prefab.activeSelf == false)
                GetComponent<TearSloriusFormationShip1>().Main3Right1prefab.SetActive(true);
        }

        if (GetComponent<TearSloriusFormationShip1>().Main1Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main1Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main1Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main1Right1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main2Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main2Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFormationShip1>().Main2Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFormationShip1>().Main2Right1Repair.SetActive(false);
        if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
        {
            if (GetComponent<TearSloriusFormationShip1>().Main3Left1Repair.activeSelf == true)
                GetComponent<TearSloriusFormationShip1>().Main3Left1Repair.SetActive(false);
            if (GetComponent<TearSloriusFormationShip1>().Main3Right1Repair.activeSelf == true)
                GetComponent<TearSloriusFormationShip1>().Main3Right1Repair.SetActive(false);
        }
    }
}