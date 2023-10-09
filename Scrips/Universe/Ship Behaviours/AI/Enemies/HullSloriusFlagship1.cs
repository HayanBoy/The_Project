using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullSloriusFlagship1 : Character
{
    MultiFlagshipSystem MultiFlagshipSystem;
    ShipSpawner ShipSpawner;
    LiveCommunicationSystem LiveCommunicationSystem;
    CashResourceSystem CashResourceSystem;

    [Header("기본 선체 정보")]
    public bool isNariha; //나리하 함선인지 여부
    public int ControsType; //종족 정보. 1 = 슬로리어스, 2 = 칸타크리
    public float hitPoints; //실시간 HP를 보기 위해 public으로 처리
    public float DebrisSpeed;

    [Header("함선 상태")]
    public bool ShieldDown = false; //방어막이 파괴되었는지에 대한 여부
    public bool NoRicochet = false; //도탄을 무시할 수 있는지에 대한 여부
    public bool isDestroied = false; //함선이 파괴되었는지에 대한 여부
    private float GetHull;
    private float GetHull2;
    private float GetHull3;
    private float GetHull4;

    [Header("함선 부위 무력화 여부")]
    public bool Main1Left1Down = false;
    public bool Main1Left2Down = false;
    public bool Main1Right1Down = false;
    public bool Main1Right2Down = false;
    public bool Main2Left1Down = false;
    public bool Main2Left2Down = false;
    public bool Main2Right1Down = false;
    public bool Main2Right2Down = false;
    public bool Main3Left1Down = false;
    public bool Main3Right1Down = false;

    [Header("함선 부위 파괴시 스프라이트 끄기")]
    public GameObject Debrisprefab;

    [Header("함선 파괴시 발생하는 메인 파편 프리팹")]
    public GameObject Debris1;
    public GameObject Debris1_2;
    public GameObject Debris2;
    public GameObject Debris2_1;
    public GameObject Debris3;
    public GameObject Debris3_1;
    public GameObject Debris4;
    public GameObject Debris4_1;

    [Header("함선 파괴시 파편 프리팹의 생성 위치")]
    public Transform Debris1Pos;
    public GameObject Explosioneffect;
    public GameObject ShockWave;

    [Header("칸타크리 전용 함선 파괴시 스프라이트 끄기")]
    public GameObject KantakriDebris1;
    public GameObject KantakriDebris2;

    [Header("함포 화재")]
    public bool TurretDown1 = false;
    public bool TurretDown2 = false;
    public bool TurretDown3 = false;
    public GameObject TurretFire1;
    public GameObject TurretFire2;
    public GameObject TurretFire3;
    public GameObject TurretFire4;
    public GameObject TurretFire5;
    public GameObject TurretFire6;

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
        GetComponent<TearSloriusFlagship1>().GetHull();

        if (isNariha == false)
        {
            ShieldDown = false;
            NoRicochet = false;
        }
        isDestroied = false;

        Main1Left1Down = false;
        Main1Left2Down = false;
        Main1Right1Down = false;
        Main1Right2Down = false;
        Main2Left1Down = false;
        Main2Left2Down = false;
        Main2Right1Down = false;
        Main2Right2Down = false;
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
        startingHitPoints = UpgradeDataSystem.instance.FlagshipHitPoints;
        hitPoints = UpgradeDataSystem.instance.FlagshipHitPoints;
        maxHitPoints = UpgradeDataSystem.instance.FlagshipHitPoints;
        GetComponent<TearSloriusFlagship1>().GetHull();
    }

    //업그레이드만 불러오기
    public void LoadPatch()
    {
        startingHitPoints = UpgradeDataSystem.instance.FlagshipHitPoints;
        maxHitPoints = UpgradeDataSystem.instance.FlagshipHitPoints;
        GetComponent<TearSloriusFlagship1>().GetHull();
    }

    //적 레벨 적용
    public void EnemyLevelPatch()
    {
        CashResourceSystem = FindObjectOfType<CashResourceSystem>();
        if (ControsType == 1) //슬로리어스
        {
            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                startingHitPoints = 13000;
                hitPoints = 13000;
                maxHitPoints = 13000;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 25000;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 25000;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                startingHitPoints = 19500;
                hitPoints = 19500;
                maxHitPoints = 19500;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 37500;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 37500;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                startingHitPoints = 26000;
                hitPoints = 26000;
                maxHitPoints = 26000;
                GetComponent<ShieldSloriusShip>().StartShieldPoints = 50000;
                GetComponent<ShieldSloriusShip>().ShieldPoints = 50000;
            }
        }
        else if (ControsType == 2) //칸타크리
        {
            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                startingHitPoints = 36000;
                hitPoints = 36000;
                maxHitPoints = 36000;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                startingHitPoints = 54000;
                hitPoints = 54000;
                maxHitPoints = 54000;
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                startingHitPoints = 72000;
                hitPoints = 72000;
                maxHitPoints = 72000;
            }
        }
        GetComponent<TearSloriusFlagship1>().GetHull();
    }

    void Start()
    {
        if (isNariha == true)
        {
            MultiFlagshipSystem = FindObjectOfType<MultiFlagshipSystem>();
            LiveCommunicationSystem = FindObjectOfType<LiveCommunicationSystem>();
        }
        else
        {
            EnemyLevelPatch();
        }
    }

    private void Update()
    {
        //안전한 행성에 있을 경우, 선체 회복
        if (isNariha == true)
        {
            if (GetComponent<FlagshipSystemNumber>().StatePlanet == 1)
            {
                if (GetComponent<MoveVelocity>().WarpDriveReady == false && GetComponent<MoveVelocity>().WarpDriveActive == false)
                {
                    if (hitPoints < startingHitPoints)
                    {
                        hitPoints += Time.deltaTime * 250;

                        if (hitPoints >= startingHitPoints * 0.4f && TurretDown1 == true)
                        {
                            TurretDown1 = false;
                            RepairTurret();
                        }
                        else if (hitPoints >= startingHitPoints * 0.2f && TurretDown2 == true)
                        {
                            TurretDown2 = false;
                            RepairTurret();
                        }
                        else if (hitPoints >= startingHitPoints * 0.1f && TurretDown3 == true)
                        {
                            TurretDown3 = false;
                            RepairTurret();
                        }

                        if (GetHull == 0)
                        {
                            GetHull += Time.deltaTime;
                            GetHull2 = 0;
                            GetHull3 = 0;
                            RepairEffect.SetActive(true);
                            GetComponent<Animator>().SetFloat("Repair, Nariha ship", 1);
                            GetComponent<TearSloriusFlagship1>().GetHullMode = true;
                            GetComponent<TearSloriusFlagship1>().RepairNumber = 1;
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(7.01f));
                            for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
                            {
                                if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().hitPoints != GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().startingHitPoints)
                                    GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().GetHull = true;
                            }
                        }
                    }
                    else if (hitPoints >= startingHitPoints && GetHull > 0) //수리 완료
                    {
                        if (GetHull2 == 0)
                        {
                            GetHull2 += Time.deltaTime;
                            GetHull = 0;
                            GetHull3 = 0;
                            hitPoints = startingHitPoints;
                            RepairEffect.SetActive(false);
                            GetComponent<Animator>().SetFloat("Repair, Nariha ship", 0);
                            RepairComplete();
                            GetComponent<TearSloriusFlagship1>().GetHullMode = false;
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(7.02f));
                        }
                    }
                    else if (hitPoints >= startingHitPoints && GetHull4 == 0) //기함은 선체가 완전하지만, 편대함선들만 손상받은 경우, 편대함선들에게 수리를 명령
                    {
                        GetHull4 += Time.deltaTime;
                        GetHull = 0;
                        GetHull2 = 0;
                        GetHull3 = 0;
                        for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
                        {
                            if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().hitPoints != GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().startingHitPoints)
                            {
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(7.01f));
                                GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().GetHull = true;
                            }
                        }
                    }
                }
            }
            else //안전한 행성에 존재하지 않으면 회복 취소
            {
                if (GetHull3 == 0)
                {
                    GetHull3 += Time.deltaTime;
                    GetHull = 0;
                    GetHull2 = 0;
                    GetHull4 = 0;
                    GetComponent<TearSloriusFlagship1>().GetHullMode = false;
                    RepairEffect.SetActive(false);
                    GetComponent<Animator>().SetFloat("Repair, Nariha ship", 0);
                    for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
                    {
                        GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().RepairCancel();
                    }

                    if (GetComponent<TearSloriusFlagship1>().Main1Left1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main1Left1Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main1Left2Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main1Left2Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main1Right1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main1Right1Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main1Right2Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main1Right2Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main2Left1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main2Left1Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main2Left2Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main2Left2Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main2Right1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main2Right1Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main2Right2Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main2Right2Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main3Left1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main3Left1Repair.SetActive(false);
                    if (GetComponent<TearSloriusFlagship1>().Main3Right1Repair.activeSelf == true)
                        GetComponent<TearSloriusFlagship1>().Main3Right1Repair.SetActive(false);
                }
            }
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
                        if (hitPoints < startingHitPoints * 0.4f && TurretDown1 == false)
                        {
                            TurretDown1 = true;
                            StartCoroutine(CannonDestroy());
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(4.00f));
                        }
                        else if (hitPoints < startingHitPoints * 0.2f && TurretDown2 == false)
                        {
                            TurretDown2 = true;
                            StartCoroutine(CannonDestroy());
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(4.00f));
                        }
                        else if (hitPoints < startingHitPoints * 0.1f && TurretDown3 == false)
                        {
                            TurretDown3 = true;
                            StartCoroutine(CannonDestroy());
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(4.00f));
                        }
                    }
                    else
                    {
                        if (hitPoints < startingHitPoints * 0.5f && TurretDown1 == false)
                        {
                            TurretDown1 = true;
                            StartCoroutine(CannonDestroy());
                        }
                        else if (hitPoints < startingHitPoints * 0.35f && TurretDown2 == false)
                        {
                            TurretDown2 = true;
                            StartCoroutine(CannonDestroy());
                        }
                        else if (hitPoints < startingHitPoints * 0.2f && TurretDown3 == false)
                        {
                            TurretDown3 = true;
                            StartCoroutine(CannonDestroy());
                        }
                    }

                    if (hitPoints <= float.Epsilon)
                    {
                        if (isDestroied == false)
                        {
                            isDestroied = true;

                            if (isNariha == true)
                            {
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(8.00f));

                                //번호 삭제
                                if (GetComponent<MoveVelocity>().FlagshipNumber < ShipManager.instance.FlagShipList.Count - 1)
                                {
                                    for (int i = GetComponent<MoveVelocity>().FlagshipNumber + 1; i < ShipManager.instance.FlagShipList.Count; i++)
                                    {
                                        ShipManager.instance.FlagShipList[i].GetComponent<MoveVelocity>().FlagshipNumber--;
                                    }
                                }

                                ShipManager.instance.FlagShipList.Remove(this.gameObject); //기함 목록에서 해당 격침되는 기함을 제외

                                if (ShipManager.instance.FlagShipList.Count <= 0) //기함이 모두 없을경우, 새 기함 자동 배치
                                {
                                    ShipSpawner = FindObjectOfType<ShipSpawner>();
                                    ShipSpawner.DelpoyFlagship();
                                }

                                MultiFlagshipSystem.FlagshipListUpdate(); //기함 목록을 새로 업데이트
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
        if (isNariha == false)
        {
            ScoreManager.instance.EnemyAllFlagshipCnt++;
            float Glopaoros = 0;
            float CR = 0;
            float Taritronic = 0;

            if (GetComponent<EnemyShipLevelInformation>().Level == 1)
            {
                Glopaoros = Random.Range(250, 300);
                CR = Random.Range(300, 350);
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 2)
            {
                Glopaoros = Random.Range(300, 350);
                CR = Random.Range(350, 400);
            }
            else if (GetComponent<EnemyShipLevelInformation>().Level == 3)
            {
                Glopaoros = Random.Range(350, 400);
                CR = Random.Range(400, 450);
            }
            CashResourceSystem.ShipBattleGainStart(Glopaoros, CR, Taritronic);
            GameObject AssetGain = Instantiate(AssetGainTextPrefab, transform.position, Quaternion.identity);
            AssetGain.transform.Find("Active/Glopaoros (1)").GetComponent<TextMesh>().text = string.Format("+{0}", Glopaoros);
            AssetGain.transform.Find("Active/Glopaoros").GetComponent<TextMesh>().text = string.Format("+{0}", Glopaoros);
            AssetGain.transform.Find("Active/Construction Resource (1)").GetComponent<TextMesh>().text = string.Format("+{0}", CR);
            AssetGain.transform.Find("Active/Construction Resource").GetComponent<TextMesh>().text = string.Format("+{0}", CR);
            Destroy(AssetGain, 3);

            AIShipManager.instance.EnemiesFlagShipList.Remove(this.gameObject);
            if (GetComponent<EnemyShipLevelInformation>().isBattleSite == true) //전투 지역에 소속시, 해당 전투 지역에서 함선을 제외
            {
                GetComponent<EnemyShipLevelInformation>().Zone.GetComponent<RandomSiteBattle>().BattleEnemyShipList.Remove(gameObject);
            }

            //편대함선보다 먼저 격침되었을 경우, 편대 함선들에게 편대 상태를 강제 해제 조취
            if (GetComponent<EnemyFollowShipManager>().ShipList.Count != 0)
            {
                for (int i = 0; i < GetComponent<EnemyFollowShipManager>().ShipList.Count; i++)
                {
                    GetComponent<EnemyFollowShipManager>().ShipList[i].GetComponent<EnemyShipBehavior>().FormationOn = false;
                    GetComponent<EnemyFollowShipManager>().ShipList[i].GetComponent<EnemyShipBehavior>().FlagshipFirstDestroy = true; //기함이 먼저 격침됨을 알림
                    AIShipManager.instance.EnemiesFormationShipList.Add(GetComponent<EnemyFollowShipManager>().ShipList[i]);
                }
            }

            if (ControsType == 1) //슬로리어스 기함1
            {
                ScoreManager.instance.EnemySloriusFlagshipCnt++;
                AIShipManager.instance.SloriusFlagShipList.Remove(this.gameObject);
                int Divide = Random.Range(0, 2);

                if (Divide == 0)
                {
                    GameObject Destroy2 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy4 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
                else
                {
                    GameObject Destroy4 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy4.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                    Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();

                    GameObject Destroy3 = Instantiate(Debris1_2, Debris1Pos.position, Debris1Pos.rotation);
                    Destroy3.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                    Destroy3.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                    Destroy3.GetComponent<DebrisActionFlagship>().TurnOffPart();
                }
            }
            else if (ControsType == 2) //칸타크리 기함1
            {
                ScoreManager.instance.EnemyKantakriFlagshipCnt++;
                AIShipManager.instance.KantakriFlagShipList.Remove(this.gameObject);
                GameObject Destroy1 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy1.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy1.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                Destroy1.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy1.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy2 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();
            }
        }
        else //나리하 기함1
        {
            int Divide = Random.Range(0, 4);

            if (Divide == 0)
            {
                GameObject Destroy2 = Instantiate(Debris1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy4 = Instantiate(Debris1_2, Debris1Pos.position, Debris1Pos.rotation);
                Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy4.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
            }
            else if (Divide == 1)
            {
                GameObject Destroy2 = Instantiate(Debris2, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy4 = Instantiate(Debris2_1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
            }
            else if (Divide == 2)
            {
                GameObject Destroy2 = Instantiate(Debris3, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy4 = Instantiate(Debris3_1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
            }
            else if (Divide == 3)
            {
                GameObject Destroy2 = Instantiate(Debris4, Debris1Pos.position, Debris1Pos.rotation);
                Destroy2.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left1Down = Main1Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Left2Down = Main1Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right1Down = Main1Right1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main1Right2Down = Main1Right2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left1Down = Main2Left1Down;
                Destroy2.GetComponent<DebrisActionFlagship>().Main2Left2Down = Main2Left2Down;
                Destroy2.GetComponent<DebrisActionFlagship>().TurnOffPart();

                GameObject Destroy4 = Instantiate(Debris4_1, Debris1Pos.position, Debris1Pos.rotation);
                Destroy4.GetComponent<DebrisActionFlagship>().StartMoveDebris(DebrisSpeed, true, false);
                Destroy4.GetComponent<DebrisActionFlagship>().Main2Right1Down = Main2Right1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main2Right2Down = Main2Right2Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Left1Down = Main3Left1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().Main3Right1Down = Main3Right1Down;
                Destroy4.GetComponent<DebrisActionFlagship>().TurnOffPart();
            }
            StartCoroutine(FlyAway());
            Instantiate(Explosioneffect, transform.position, Quaternion.identity);
            ScoreManager.instance.LostNarihaFlagshipCnt++;

            //소속된 함선들이 흩어져서 사라지도록 조취
            for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
            {
                GameObject Ship = GetComponent<FollowShipManager>().ShipList[i].gameObject;

                float EnemyshipRange = Random.Range(10000, 10000);
                Vector3 EnemyShipLocation;
                EnemyShipLocation = new Vector3(Random.Range(Ship.transform.position.x - EnemyshipRange, Ship.transform.position.x + EnemyshipRange),
                    Random.Range(Ship.transform.position.y - EnemyshipRange, Ship.transform.position.y + EnemyshipRange), Ship.transform.position.z);

                if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                }
                else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 2)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Shield system").GetComponent<NarihaShieldSystem>().canDefence = false;
                }
                else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 3)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().canAttack = false;
                }

                Ship.GetComponent<MoveVelocity>().SetVelocity(EnemyShipLocation);
                Ship.GetComponent<MoveVelocity>().FormationOn = false;
                Ship.GetComponent<MoveVelocity>().EmergencyWarp = true;
                Destroy(Ship, 7);
            }
        }

        Debrisprefab.SetActive(false);
        Destroy(gameObject, 1);
    }

    //폭발 쇼크 웨이브 출력
    IEnumerator FlyAway()
    {
        yield return new WaitForSeconds(3);
        Instantiate(ShockWave, transform.position, Quaternion.identity);
    }

    //함포 무작위 무력화
    IEnumerator CannonDestroy()
    {
        while (true)
        {
            int RandomCannon = Random.Range(0, 6);

            if (RandomCannon == 0)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire1.SetActive(true);
                        TurretDownList.Add(1);
                        break;
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
                        break;
                    }
                }
            }
            else if (RandomCannon == 1)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire2.SetActive(true);
                        TurretDownList.Add(2);
                        break;
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
                        break;
                    }
                }
            }
            else if (RandomCannon == 2)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire3.SetActive(true);
                        TurretDownList.Add(3);
                        break;
                    }
                }
                else
                {
                    if (GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().enabled == true)
                    {
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire3.SetActive(true);
                        TurretDownList.Add(3);
                        break;
                    }
                }
            }
            else if (RandomCannon == 3)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire4.SetActive(true);
                        TurretDownList.Add(4);
                        break;
                    }
                }
                else
                {
                    if (GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().enabled == true)
                    {
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire4.SetActive(true);
                        TurretDownList.Add(4);
                        break;
                    }
                }
            }
            else if (RandomCannon == 4)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire5.SetActive(true);
                        TurretDownList.Add(5);
                        break;
                    }
                }
                else
                {
                    if (GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().enabled == true)
                    {
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire5.SetActive(true);
                        TurretDownList.Add(5);
                        break;
                    }
                }
            }
            else if (RandomCannon == 5)
            {
                if (isNariha == true)
                {
                    if (GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().enabled == true)
                    {
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire6.SetActive(true);
                        TurretDownList.Add(6);
                        break;
                    }
                }
                else
                {
                    if (GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().enabled == true)
                    {
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire6.SetActive(true);
                        TurretDownList.Add(6);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.05f);
            continue;
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
        else if (TurretDownList[0] == 3)
        {
            TurretFire3.SetActive(false);
            GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
        else if (TurretDownList[0] == 4)
        {
            TurretFire4.SetActive(false);
            GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
        else if (TurretDownList[0] == 5)
        {
            TurretFire5.SetActive(false);
            GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
        else if (TurretDownList[0] == 6)
        {
            TurretFire6.SetActive(false);
            GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().enabled = true;
            GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            TurretDownList.Remove(TurretDownList[0]);
        }
    }

    //무력화된 함포 정보 가져오기
    public void BringTurretDestroy()
    {
        if (TurretDownList.Count > 0)
        {
            for (int i = 0; i < TurretDownList.Count; i++)
            {
                if (TurretDownList.Count == 1)
                {
                    TurretDown1 = true;
                }
                else if (TurretDownList.Count == 2)
                {
                    TurretDown2 = true;
                }
                else if(TurretDownList.Count == 3)
                {
                    TurretDown3 = true;
                }

                if (isNariha == true)
                {
                    if (TurretDownList[i] == 1)
                    {
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire1.SetActive(true);
                    }
                    else if (TurretDownList[i] == 2)
                    {
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret2.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire2.SetActive(true);
                    }
                    else if (TurretDownList[i] == 3)
                    {
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret3.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire3.SetActive(true);
                    }
                    else if (TurretDownList[i] == 4)
                    {
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret4.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire4.SetActive(true);
                    }
                    else if (TurretDownList[i] == 5)
                    {
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret5.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire5.SetActive(true);
                    }
                    else if (TurretDownList[i] == 6)
                    {
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().TargetShip = null;
                        GetComponent<MoveVelocity>().Turret6.GetComponent<NarihaTurretAttackSystem>().enabled = false;
                        TurretFire6.SetActive(true);
                    }
                }
                else
                {
                    if (TurretDownList[i] == 1)
                    {
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret1.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire1.SetActive(true);
                    }
                    else if (TurretDownList[i] == 2)
                    {
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret2.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire2.SetActive(true);
                    }
                    else if (TurretDownList[i] == 3)
                    {
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret3.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire3.SetActive(true);
                    }
                    else if (TurretDownList[i] == 4)
                    {
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret4.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire4.SetActive(true);
                    }
                    else if (TurretDownList[i] == 5)
                    {
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret5.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire5.SetActive(true);
                    }
                    else if (TurretDownList[i] == 6)
                    {
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().canAttack = false;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().TargetShip = null;
                        GetComponent<EnemyShipBehavior>().Turret6.GetComponent<EnemyAttackSystem>().enabled = false;
                        TurretFire6.SetActive(true);
                    }
                }
            }
        }
    }

    //모든 선체 수리 완료
    public void RepairComplete()
    {
        GetComponent<TearSloriusFlagship1>().Main1Left1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main1Left2HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main1Right1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main1Right2HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main2Left1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main2Left2HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main2Right1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main2Right2HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main3Left1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;
        GetComponent<TearSloriusFlagship1>().Main3Right1HP = GetComponent<TearSloriusFlagship1>().PartModuleHP;

        Main1Left1Down = false;
        Main1Left2Down = false;
        Main1Right1Down = false;
        Main1Right2Down = false;
        Main2Left1Down = false;
        Main2Left2Down = false;
        Main2Right1Down = false;
        Main2Right2Down = false;
        Main3Left1Down = false;
        Main3Right1Down = false;

        if (GetComponent<TearSloriusFlagship1>().Main1Left1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main1Left1prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main1Left2prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main1Left2prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main1Right1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main1Right1prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main1Right2prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main1Right2prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main2Left1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main2Left1prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main2Left2prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main2Left2prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main2Right1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main2Right1prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main2Right2prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main2Right2prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main3Left1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main3Left1prefab.SetActive(true);
        if (GetComponent<TearSloriusFlagship1>().Main3Right1prefab.activeSelf == false)
            GetComponent<TearSloriusFlagship1>().Main3Right1prefab.SetActive(true);

        if (GetComponent<TearSloriusFlagship1>().Main1Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main1Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main1Left2Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main1Left2Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main1Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main1Right1Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main1Right2Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main1Right2Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main2Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main2Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main2Left2Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main2Left2Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main2Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main2Right1Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main2Right2Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main2Right2Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main3Left1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main3Left1Repair.SetActive(false);
        if (GetComponent<TearSloriusFlagship1>().Main3Right1Repair.activeSelf == true)
            GetComponent<TearSloriusFlagship1>().Main3Right1Repair.SetActive(false);
    }
}