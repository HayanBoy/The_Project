using System.Collections;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [Header("스크립트")]
    UIControlSystem UIControlSystem;
    UniverseMapSystem UniverseMapSystem;
    DataSaveManager DataSaveManager;
    CameraFollow CameraFollow;
    LiveCommunicationSystem LiveCommunicationSystem;
    public CommunicationSoundSystem CommunicationSoundSystem;

    Animator anim;
    private Rigidbody2D rb2D;

    Coroutine battleSoundRandom;
    Coroutine battleBigSoundRandom;
    private float OneTele;
    private float OneTele2;

    [Header("테스트 공격 활성화")]
    public bool TestAttack; //테스트 공격용

    [Header("함대 유형 및 명령")]
    public bool FlagShip; //기함인지 판단하기 위한 여부
    public int FlagshipNumber; //기함번호
    public bool FormationOn; //편대모드 활성화 여부
    public bool MoveOrder = false; //움직임이 명령되었는지 확인하는 스위치. 기함모드로 이동중에 편대모드로 변경되어 소속 함선들이 이동할 때의 명령 스위치
    public bool ImMoving = false; //이동 명령 스위치
    public bool FlagshipMove = false; //기함 움직임 감지
    public bool EmergencyWarp = false; //기함이 격침되었을 때, 편대함들이 다른 곳으로 도주하도록 설정
    public GameObject MoveSoundPrefab;

    [Header("함선 위치 정보")]
    public Transform FormationIndex; //소속 함선의 각 지정된 자리영역
    public Transform WarpformationIndex; //FormationIndex의 워프 영역용
    Vector3 DirectionAtFormation; //FormationIndex를 통해 계산되는 편대 영역 도착 지점
    Vector3 DirectionAtMoving; //DestinationArea를 통해 계산되는 방향전환용 도착 지점
    public Vector3 DestinationArea; //지정된 도착 지점
    public float RotateSpeed; //방향전환 속도
    public float MoveOrderStemp; //함대모드 전환 후, 기함이 단독으로 움직일 때, 기함의 현재 도착지점과 기함의 회전값을 딱 한번만 불러오기

    [Header("가속도 및 감속도")]
    [SerializeField] private float BoostSpeed; //설정 속도
    public float WarpSpeed; //워프 속도
    public float CurrentSpeed; //현재 속도
    private float AcceleratorSpeed; //가속도
    private float AccelAndDecelRange = 2f; //가속 및 감속이 허용되는 거리
    float RangeBetweenStartAndDestination; //시작과 도착지점의 거리. 함선의 가속 및 감속거리를 확보하기 위함

    [Header("활동 범위")]
    public GameObject MyFlagship; //소속 기함
    public float FlagshipActiveSite; //기함을 중심으로 활동하는 제한된 영역
    public int FormationNumber; //기함에 소속된 편대함선 번호
    public bool ShipControlMode = true; //함선 조종모드. 기함모드 = true, 함대모드 = false
    public bool ShipSelectionMode = false; //함대 배열모드

    [Header("편대함선 전용 공격 사거리")]
    public GameObject TargetShip; //지정 대상
    private float distanceFromTarget; //지정 대상과의 거리
    public float MaxDamageRange; //공격 최대 사거리
    private float SearchTime = 3;

    [Header("함포 프리팹")]
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject Turret5;
    public GameObject Turret6;

    [Header("워프")]
    Vector3 MoveDir;
    float DisctanceFromTarget;
    public bool WarpDriveReady; //워프 준비
    public bool WarpDriveActive; //워프 가동 여부
    public bool WarpDriveStopReady; //워프 멈추기 직전
    public bool WarpStopFormation; //기함이 워프를 완료한 이후까지 동작 정지
    private float WarpCompleteTemp; //Update문에서 딱 한번만 가동하도록 조취
    private float WarpGeneratorTemp; //Update문에서 딱 한번만 가동하도록 조취
    public GameObject WarpGenerator; //워프 발생 이펙트
    public GameObject WarpArriveEffect; //워프 도착 이펙트
    public GameObject WarpBoosterReady; //워프 준비 부스터
    public GameObject WarpBooster; //워프 부스터
    public GameObject Booster;
    public bool TransferWarpActive = false; //함대 이전을 받은 소속 함선이 워프를 마치면 함대에 소속되도록 조취
    private bool TransferWarpMode = false; //함대 이전을 받은 소속 함선이 워프할 때만 사용
    private float TransferWarpTime;

    private float MoveStemp;
    private Vector3 endposition;
    Coroutine moveTargetAround;
    Coroutine move;


    public void SpeedUpdate(float ChangeSpeed)
    {
        BoostSpeed = ChangeSpeed;
        AcceleratorSpeed = (BoostSpeed / (1250 / BoostSpeed)) * 500;
    }

    //워프 시작
    public void WarpSpeedUp(bool boolean)
    {
        if (this.gameObject != null)
        {
            if (FlagShip == true)
            {
                DoNotAttack();
                CameraFollow.WarpStartAnime(true);
            }
            WarpDriveActive = boolean;
            CurrentSpeed = WarpSpeed;
            WarpCompleteTemp = 0;
            WarpGeneratorTemp = 0;
            Booster.SetActive(false);
            WarpBooster.SetActive(true);
            WarpBoosterReady.SetActive(false);
            WarpDriveReady = false;
            NoDamageInWarp();
        }
    }

    //소속 함선 전용, 함대 이전을 받은 함선이 해당 기함과의 거리가 멀경우, 워프 실시
    public void TransferWarp()
    {
        TransferWarpMode = true;
    }

    //소속 함선 전용 워프 도착
    public void WarpCompleteBoolean()
    {
        //WarpDriveActive = false;
    }

    void OnEnable()
    {
        ImMoving = false;
        MoveOrder = false;
        WarpDriveActive = false;
    }

    void Awake()
    {
        UIControlSystem = FindObjectOfType<UIControlSystem>();
        UniverseMapSystem = FindObjectOfType<UniverseMapSystem>();
        CameraFollow = FindObjectOfType<CameraFollow>();
        DataSaveManager = FindObjectOfType<DataSaveManager>();
        LiveCommunicationSystem = FindObjectOfType<LiveCommunicationSystem>();
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        DestinationArea = transform.position;
        AcceleratorSpeed = (BoostSpeed / (1250 / BoostSpeed)) * 500; //1250은 가속도 연산값이다. 최대속도에 맞춰서 가속도가 연산된다.
    }

    //도착 지점 받기
    public void SetVelocity(Vector3 MovePositionDirection)
    {
        if (FlagShip == true)
            FlagshipMove = true;
        DestinationArea = MovePositionDirection;
        RangeBetweenStartAndDestination = Vector3.Distance(MovePositionDirection, transform.position);
        DirectionAtMoving = (MovePositionDirection - transform.position).normalized; //방향 전환용 도착지점을 계산
    }

    //공격 중지 명령
    public void DoNotAttack()
    {
        Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = false;
        GetComponent<FlagshipAttackSkill>().canAttack = false;

        for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
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
        }
    }

    private void FixedUpdate()
    {
        if (WarpDriveActive == true)
        {
            //함선 기본 이동
            MoveDir = (DestinationArea - transform.position).normalized;
            DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

            if (FlagShip == true)
            {
                transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                TargetShip = null;

                if (DisctanceFromTarget <= 200 && DisctanceFromTarget >= 100)
                {
                    if (WarpGeneratorTemp == 0)
                    {
                        WarpGeneratorTemp += Time.deltaTime;
                        WarpDriveStopReady = true;
                        CameraFollow.WarpStartAnime(false);
                        GameObject WarpEffect = Instantiate(WarpGenerator, DestinationArea, Quaternion.identity);
                        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                            UIControlSystem.CVCamera.Follow = WarpEffect.transform;
                        UIControlSystem.WarpArrive();
                        WarpBooster.SetActive(false);
                    }
                }
                if (DisctanceFromTarget <= 2.5f)
                    CurrentSpeed = WarpSpeed * 0.1f;

                if (DisctanceFromTarget <= 1) //목적지에 도달할 경우, 함선 이동 정지
                {
                    //Debug.Log("워프 정지");
                    WarpDriveActive = false;
                    MoveDir = Vector3.zero;
                    CurrentSpeed = 0;
                    ImMoving = false;
                    FlagshipMove = false;

                    if (WarpCompleteTemp == 0)
                    {
                        WarpCompleteTemp += Time.deltaTime;
                        WarpBooster.SetActive(false);
                        Booster.SetActive(true);
                        WarpDriveStopReady = false;
                        UIControlSystem.FlagShipWarpComplete();
                        Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                        CameraFollow.AmountOfCameraShake = 0;
                        GetComponent<FlagshipSystemNumber>().SystemDestinationNumber = 0;

                        Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        GetComponent<FlagshipAttackSkill>().canAttack = true;
                        StartDamageInWarp();

                        for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
                        {
                            if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                            {
                                GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                                GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                            }
                            else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 2)
                            {
                                GetComponent<FollowShipManager>().ShipList[i].transform.Find("Shield system").GetComponent<NarihaShieldSystem>().canDefence = true;
                            }
                            else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 3)
                            {
                                GetComponent<FollowShipManager>().ShipList[i].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().canAttack = true;
                            }
                            GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().WarpStopFormation = false;
                        }

                        for (int i = 0; i < GetComponent<FollowShipManager>().ShipList.Count; i++)
                        {
                            GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().WarpStopFormation = false;
                            if (Vector2.Distance(transform.position, GetComponent<FollowShipManager>().ShipList[i].transform.position) > 200)
                            {
                                GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().TransferWarp();
                            }
                        }
                    }
                }
            }
            else //소속 함선이 개별로 워프했을 경우(함선 이전 및 함선 생산으로 인해) 스스로 공격 명령 허용
            {
                if (DisctanceFromTarget <= 0.1f) //목적지에 도달할 경우, 함선 이동 정지
                {
                    if (GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                    {
                        transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                        transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                    }
                    else if (GetComponent<HullSloriusFormationShip1>().NarihaType == 2)
                    {
                        transform.Find("Shield system").GetComponent<NarihaShieldSystem>().canDefence = true;
                    }
                    else if (GetComponent<HullSloriusFormationShip1>().NarihaType == 3)
                    {
                        transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().canAttack = true;
                    }
                }
            }
        }
    }

    void Update()
    {
        //타겟이 잡혔을 때에만 사거리 별 사격 발동
        if (TargetShip != null)
        {
            if (SearchTime <= 3)
                SearchTime += Time.deltaTime;

            if (SearchTime >= 3)
            {
                SearchTime = 0;

                if (OneTele == 0)
                {
                    OneTele += Time.deltaTime;
                    OneTele2 = 0;
                    if (FlagShip == true)
                        StartCoroutine(CommunicationSoundSystem.WarnningCommunication());
                    CommunicationSoundSystem.InBattle = true;
                    battleSoundRandom = StartCoroutine(CommunicationSoundSystem.BattleSoundRandom());
                    battleBigSoundRandom = StartCoroutine(CommunicationSoundSystem.BattleBigSoundRandom());

                    if (FlagShip == true)
                    {
                        int RandomText = Random.Range(0, 2);
                        if (RandomText == 0)
                            StartCoroutine(LiveCommunicationSystem.SubCommunication(9.00f)); //나리하 교전 메시지
                        else if (RandomText == 1)
                        {
                            if (TargetShip.gameObject.transform.parent.parent.GetComponent<EnemyShipBehavior>().NationType == 2)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(10.01f)); //슬로리어스 교전 메시지
                            else if (TargetShip.gameObject.transform.parent.parent.GetComponent<EnemyShipBehavior>().NationType == 3)
                                StartCoroutine(LiveCommunicationSystem.SubCommunication(10.02f)); //칸타크리 교전 메시지
                        }
                    }
                }

                distanceFromTarget = Vector2.Distance(TargetShip.transform.position, transform.position);

                if (distanceFromTarget > MaxDamageRange)
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret3.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret4.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret5.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret6.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        GetComponent<FlagshipAttackSkill>().RangeAttack = false;
                    }
                    else
                    {
                        if (GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                        {
                            Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                            Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        }
                    }
                }
                else
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        Turret3.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        Turret4.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        Turret5.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        Turret6.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        GetComponent<FlagshipAttackSkill>().RangeAttack = true;
                    }
                    else
                    {
                        if (GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                        {
                            Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                            Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = true;
                        }
                    }
                }
            }
        }
        else
        {
            if (OneTele2 == 0)
            {
                OneTele2 += Time.deltaTime;
                OneTele = 0;
                CommunicationSoundSystem.InBattle = false;
                CommunicationSoundSystem.BattleEnd();
                if (battleSoundRandom != null)
                    StopCoroutine(battleSoundRandom);
                if (battleBigSoundRandom != null)
                    StopCoroutine(battleBigSoundRandom);

                if (FlagShip == true)
                {
                    Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    Turret3.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    Turret4.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    Turret5.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    Turret6.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    GetComponent<FlagshipAttackSkill>().RangeAttack = false;
                }
                else
                {
                    if (GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                    {
                        Turret1.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<NarihaTurretAttackSystem>().RangeAttack = false;
                    }
                }
            }
        }

        //기함 및 일반 함선 별 이동 방식
        if (FlagShip == false) //소속 함선
        {
            if (FormationOn == false) //함대진이 미가동 중일 경우 소속함선들이 지정된 지역으로 움직일 수 있다.
            {
                if (WarpDriveActive == false)
                {
                    if (EmergencyWarp == false)
                    {
                        //함선 기본 이동
                        MoveDir = (DestinationArea - transform.position).normalized;
                        DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);
                        float distanceFromPlayer = Vector2.Distance(MyFlagship.transform.position, transform.position);

                        //소속 함선이 기함의 일정 허용 반경을 이탈시, 기함 추적
                        if (distanceFromPlayer > FlagshipActiveSite)
                        {
                            DirectionAtMoving = MyFlagship.transform.position; //기함을 향해 기수를 돌린다.
                            DestinationArea = transform.position; //본래 목적지가 기함의 범위보다 더 멀리 있으므로 강제 취소
                            transform.position = Vector3.MoveTowards(transform.position, FormationIndex.position, BoostSpeed * 1.5f * Time.deltaTime);
                        }
                        else
                        {
                            //기함모드에서 함대모드로 변경되었을 경우, 해당 함선을 선택하여 이동하기 전까지는 기함을 따라간다.
                            if (MoveOrder == true && ImMoving == true) //이동 명령이 내려졌을 경우, 즉시 지정 장소로 이동
                            {
                                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                                rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                                transform.position += MoveDir * CurrentSpeed * Time.deltaTime;

                                if (RangeBetweenStartAndDestination > AccelAndDecelRange * 2) //이동거리가 가속+감속 거리보다 많을 경우, 정상적으로 움직인다.
                                {
                                    if (DisctanceFromTarget > AccelAndDecelRange) //가속
                                    {
                                        if (CurrentSpeed <= BoostSpeed)
                                            CurrentSpeed += AcceleratorSpeed * Time.deltaTime;
                                    }
                                    else if (DisctanceFromTarget < AccelAndDecelRange) //감속
                                    {
                                        if (CurrentSpeed >= 0)
                                            CurrentSpeed -= AcceleratorSpeed * Time.deltaTime;
                                    }
                                }
                                else //이동거리가 가속+감속 거리보다 적을 경우, 가속과 감속 거리를 각각 절반으로 나누어서 움직인다.
                                {
                                    if (DisctanceFromTarget > RangeBetweenStartAndDestination / 2)
                                    {
                                        if (CurrentSpeed <= BoostSpeed)
                                            CurrentSpeed += AcceleratorSpeed * Time.deltaTime;
                                    }
                                    else
                                    {
                                        if (CurrentSpeed >= 0)
                                            CurrentSpeed -= AcceleratorSpeed * Time.deltaTime;
                                    }
                                }
                            }
                            else if (TargetShip.gameObject == null && MoveOrder == false) //명령이 내려지지 않은 경우, 여전히 기함을 따라간다.
                            {
                                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().MoveOrder == true)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, WarpformationIndex.position, BoostSpeed * 1.5f * Time.deltaTime);
                                    if (Vector3.Distance(WarpformationIndex.position, transform.position) > 0.25f) //함선이 함대진으로 이동중인 상태에서 목적지를 향해 정렬
                                    {
                                        DirectionAtFormation = (WarpformationIndex.position - transform.position).normalized;
                                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                                    }
                                    else //함선이 함대진으로 이동이 완료한 뒤, 정렬하는 과정
                                    {
                                        CurrentSpeed = 0;
                                        rb2D.transform.rotation = Quaternion.Lerp(transform.rotation, WarpformationIndex.rotation, 0.06f);
                                    }
                                }
                                else
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, FormationIndex.position, BoostSpeed * 1.5f * Time.deltaTime);
                                    if (Vector3.Distance(FormationIndex.position, transform.position) > 0.25f) //함선이 함대진으로 이동중인 상태에서 목적지를 향해 정렬
                                    {
                                        DirectionAtFormation = (FormationIndex.position - transform.position).normalized;
                                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                                    }
                                    else //함선이 함대진으로 이동이 완료한 뒤, 정렬하는 과정
                                    {
                                        CurrentSpeed = 0;
                                        rb2D.transform.rotation = Quaternion.Lerp(transform.rotation, MyFlagship.transform.rotation, 0.06f);
                                    }
                                }
                            }
                            if (TargetShip.gameObject != null && ImMoving == false) //공격 대상이 있을 경우, 이동 명령을 받지 않았을 경우, 해당 대상을 향해 이동한다.
                            {
                                Vector3 dir1 = (TargetShip.transform.position - transform.position).normalized;
                                Vector3 dir2 = (TargetShip.transform.position + transform.position).normalized;

                                if (distanceFromTarget > MaxDamageRange) //타겟이 최대 사거리를 벗어나면 접근
                                {
                                    MoveStemp = 0;
                                    if (moveTargetAround != null)
                                        StopCoroutine(moveTargetAround);
                                    if (move != null)
                                        StopCoroutine(move);

                                    Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                                    transform.position = Vector2.MoveTowards(transform.position, TargetShip.transform.position, CurrentSpeed * Time.deltaTime);

                                    if (CurrentSpeed <= BoostSpeed)
                                        CurrentSpeed += AcceleratorSpeed * Time.deltaTime;
                                }
                            }
                        }
                    }
                    else
                    {
                        //함선 기본 이동
                        MoveDir = (DestinationArea - transform.position).normalized;
                        transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                        DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                        if (DisctanceFromTarget > MaxDamageRange)
                        {
                            MoveSoundPrefab.SetActive(true);
                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                        else if (DisctanceFromTarget <= 1)
                        {
                            MoveSoundPrefab.SetActive(false);
                        }
                    }
                }
                else //편대함 단독 워프
                {
                    MoveDir = (DestinationArea - transform.position).normalized;
                    transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);
                    TargetShip = null;

                    if (DisctanceFromTarget <= 200 && DisctanceFromTarget >= 100)
                    {
                        if (WarpGeneratorTemp == 0)
                        {
                            WarpGeneratorTemp += Time.deltaTime;
                            Instantiate(WarpGenerator, DestinationArea, Quaternion.identity);
                        }
                    }
                    if (DisctanceFromTarget > 50f)
                        anim.SetFloat("Warp, Slorius Flagship", 1);

                    if (DisctanceFromTarget <= 15f)
                        anim.SetFloat("Warp, Slorius Flagship", 2);

                    if (DisctanceFromTarget <= 2.5f)
                        CurrentSpeed = WarpSpeed * 0.1f;

                    if (DisctanceFromTarget <= 0.5f)
                    {
                        WarpDriveActive = false;
                        CurrentSpeed = 0;

                        if (WarpCompleteTemp == 0)
                        {
                            WarpCompleteTemp += Time.deltaTime;
                            Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                            Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                            Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                            StartDamageInWarp();
                        }
                    }
                }
            }
            else //함대진이 가동 중일 경우 기함 주변으로 정해진 배열에 맞춰 움직인다.
            {
                if (WarpDriveActive == false && WarpStopFormation == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, FormationIndex.position, BoostSpeed * 1.5f * Time.deltaTime);

                    if (Vector3.Distance(FormationIndex.position, transform.position) > 0.25f) //함선이 함대진으로 이동중인 상태에서 목적지를 향해 정렬
                    {
                        DirectionAtFormation = (FormationIndex.position - transform.position).normalized;
                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    }
                    else //함선이 함대진으로 이동이 완료한 뒤, 정렬하는 과정
                    {
                        CurrentSpeed = 0;
                        rb2D.transform.rotation = Quaternion.Lerp(transform.rotation, MyFlagship.transform.rotation, 0.06f);
                    }
                }
                else if (WarpDriveActive == true)//워프 도착 지점으로 워프 시작
                {
                    transform.position = Vector3.MoveTowards(transform.position, WarpformationIndex.position, CurrentSpeed * Time.deltaTime);
                    Vector3 DirectionAtFormation = (WarpformationIndex.position - transform.position).normalized;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    TargetShip = null;

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 200)
                    {
                        if (WarpGeneratorTemp == 0)
                        {
                            WarpGeneratorTemp += Time.deltaTime;
                            Instantiate(WarpGenerator, WarpformationIndex.position, Quaternion.identity);
                            WarpBooster.SetActive(false);
                        }
                    }
                    if (Vector3.Distance(WarpformationIndex.position, transform.position) > 50f)
                        anim.SetFloat("Warp, Slorius Flagship", 1);

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 15f)
                        anim.SetFloat("Warp, Slorius Flagship", 2);

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 2.5f)
                        CurrentSpeed = WarpSpeed * 0.1f;

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 0.1f)
                    {
                        WarpDriveActive = false;
                        MoveDir = Vector3.zero;
                        CurrentSpeed = 0;
                        ImMoving = false;

                        if (WarpCompleteTemp == 0)
                        {
                            WarpCompleteTemp += Time.deltaTime;
                            Booster.SetActive(true);
                            Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                            StartDamageInWarp();
                            if (TransferWarpActive == false)
                                WarpStopFormation = true; //기함이 워프를 완료할 때까지 먼저 워프 완료 후에도 자리를 유지
                            else
                            {
                                TransferWarpActive = false;
                            }
                        }
                    }
                }
            }
        }
        else //기함
        {
            if (WarpDriveActive == false)
            {
                //함선 기본 이동
                MoveDir = (DestinationArea - transform.position).normalized;
                DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                if (FlagshipMove == true)
                {
                    transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    MoveSoundPrefab.SetActive(true);
                }
                if (RangeBetweenStartAndDestination > AccelAndDecelRange * 2) //이동거리가 가속+감속 거리보다 많을 경우, 정상적으로 움직인다.
                {
                    if (DisctanceFromTarget > AccelAndDecelRange)
                    {
                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed * Time.deltaTime;
                    }
                    else if (DisctanceFromTarget < AccelAndDecelRange)
                    {
                        if (CurrentSpeed >= 0)
                            CurrentSpeed -= AcceleratorSpeed * Time.deltaTime;
                    }
                }
                else //이동거리가 가속+감속 거리보다 적을 경우, 가속과 감속 거리를 각각 절반으로 나누어서 움직인다.
                {
                    if (DisctanceFromTarget > RangeBetweenStartAndDestination / 2)
                    {
                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed * Time.deltaTime;
                    }
                    else
                    {
                        if (CurrentSpeed >= 0)
                            CurrentSpeed -= AcceleratorSpeed * Time.deltaTime;
                    }
                }
                if (DisctanceFromTarget <= 1)
                {
                    FlagshipMove = false;
                    MoveSoundPrefab.SetActive(false);
                }
            }
            else
            {
                if (DisctanceFromTarget >= RangeBetweenStartAndDestination * 0.7f)
                {
                    if (CameraFollow.AmountOfCameraShake < 6)
                        CameraFollow.AmountOfCameraShake += 2 * Time.deltaTime;
                }
                else if (DisctanceFromTarget < RangeBetweenStartAndDestination * 0.7f && DisctanceFromTarget > 600)
                {
                    if (CameraFollow.AmountOfCameraShake > 1)
                        CameraFollow.AmountOfCameraShake -= 5 * Time.deltaTime;
                }
                else if (DisctanceFromTarget <= 600 && DisctanceFromTarget > 200)
                {
                    if (CameraFollow.AmountOfCameraShake < 6)
                        CameraFollow.AmountOfCameraShake += 2 * Time.deltaTime;
                }
            }

            if (MoveOrder == true)
            {
                if (MoveOrderStemp == 0)
                {
                    MoveOrderStemp += Time.deltaTime;
                    UIControlSystem.TransformDestination();
                }
            }
        }

        if (TestAttack)
        {
            Turret1.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            Turret2.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            Turret3.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            Turret4.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            Turret5.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            Turret6.GetComponent<NarihaTurretAttackSystem>().canAttack = true;
            GetComponent<FlagshipAttackSkill>().canAttack = true;

            for (int i = 0; i < GetComponent<FollowShipManager>().ShipAccount; i++)
            {
                if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().canAttack = true;
                }
                else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 2)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Shield system").GetComponent<NarihaShieldSystem>().canDefence = true;
                }
                else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 3)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().canAttack = true;
                }
            }
        }

        //함대 이전을 받은 소속 함선이 워프하기 위한 카운트 다운
        if (TransferWarpMode == true || EmergencyWarp == true)
        {
            TransferWarpTime += Time.deltaTime;
            WarpDriveReady = true;

            if (TransferWarpTime > 1 && TransferWarpMode == true && EmergencyWarp == false) //함선 생산 및 이전
            {
                if (EmergencyWarp == false)
                    TransferWarpTime = 0;
                TransferWarpMode = false;
                TransferWarpActive = true; //새로 배치되는 편대함들은 먼저 이걸 켜두어야 워프 완료 후에 배치가 정상적으로 이루어진다.
                WarpSpeedUp(true);
            }
            else if (TransferWarpTime > 3) //기함이 격침되면 함선들이 흩어진다.
            {
                TransferWarpTime = 0;
                EmergencyWarp = false;
                WarpSpeedUp(true);
            }
        }
    }

    //선회 구현
    IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        if (TargetShip != null)
        {
            float remainingDistance = (transform.position - endposition).sqrMagnitude;

            while (remainingDistance > float.Epsilon)
            {
                //이동
                if (rigidbodyToMove != null)
                {
                    transform.position += transform.up * speed * Time.deltaTime;
                    Vector3 dir = (endposition - transform.position).normalized;
                    if (FlagShip == true)
                        transform.up = Vector3.Lerp(transform.up, dir, 0.01f);
                    else
                        transform.up = Vector3.Lerp(transform.up, dir, 0.05f);
                    remainingDistance = (transform.position - endposition).sqrMagnitude;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }

    //기함이 지정된 대상을 함대에게 공격명령
    public void TargetEngage(GameObject target)
    {
        if (FlagShip == true) //기함
        {
            TargetShip = target;
            Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret1.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret2.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret3.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret3.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret4.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret4.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret5.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret5.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret6.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret6.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;

            for (int i = 0; i < GetComponent<FollowShipManager>().ShipAccount; i++)
            {
                GetComponent<FollowShipManager>().ShipList[i].GetComponent<MoveVelocity>().TargetShip = target;
                if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 1)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret1").GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Turret2").GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
                }
                else if (GetComponent<FollowShipManager>().ShipList[i].GetComponent<HullSloriusFormationShip1>().NarihaType == 3)
                {
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().TargetShip = target;
                    GetComponent<FollowShipManager>().ShipList[i].transform.Find("Carrier Borne Aircraft System").GetComponent<NarihaFighterSystem>().OrderTarget = true;
                }
            }
        }

        else //편대 함대
        {
            TargetShip = target;
            if (GetComponent<ShipRTS>().ShipNumber == 2) //편대함
            {
                Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                Turret1.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
                Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                Turret2.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 4) //우주모함
            {
                Turret1.GetComponent<NarihaFighterSystem>().TargetShip = target;
                Turret1.GetComponent<NarihaFighterSystem>().OrderTarget = true;
            }
        }
    }

    //지정받은 대상을 개별로 공격
    public void TargetEngagePersonal(GameObject target)
    {
        if (FlagShip == true) //기함
        {
            Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret1.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret2.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret3.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret3.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret4.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret4.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret5.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret5.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            Turret6.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
            Turret6.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
        }

        else //편대 함대
        {
            if (GetComponent<ShipRTS>().ShipNumber == 2) //편대함
            {
                Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                Turret1.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
                Turret2.GetComponent<NarihaTurretAttackSystem>().TargetShip = target;
                Turret2.GetComponent<NarihaTurretAttackSystem>().OrderTarget = true;
            }
            else if (GetComponent<ShipRTS>().ShipNumber == 4) //우주모함
            {
                Turret1.GetComponent<NarihaFighterSystem>().TargetShip = target;
                Turret1.GetComponent<NarihaFighterSystem>().OrderTarget = true;
            }
        }
    }

    //워프 중에는 데미지를 받지 않는다.
    public void NoDamageInWarp()
    {
        if (FlagShip == true)
        {
            gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main1Left1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main1Left2Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main1Right1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main1Right2Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main2Left1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main2Left2Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main2Right1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main2Right2Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main3Left1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFlagship1>().Main3Right1Pos.gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 0;
            GetComponent<TearSloriusFormationShip1>().Main1Left1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFormationShip1>().Main1Right1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFormationShip1>().Main2Left1Pos.gameObject.layer = 0;
            GetComponent<TearSloriusFormationShip1>().Main2Right1Pos.gameObject.layer = 0;
            if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
            {
                GetComponent<TearSloriusFormationShip1>().Main3Left1Pos.gameObject.layer = 0;
                GetComponent<TearSloriusFormationShip1>().Main3Right1Pos.gameObject.layer = 0;
            }
        }
    }

    //워프 종료 후, 데미지 받기
    public void StartDamageInWarp()
    {
        if (FlagShip == true)
        {
            gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main1Left1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main1Left2Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main1Right1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main1Right2Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main2Left1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main2Left2Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main2Right1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main2Right2Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main3Left1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFlagship1>().Main3Right1Pos.gameObject.layer = 6;
        }
        else
        {
            gameObject.layer = 6;
            GetComponent<TearSloriusFormationShip1>().Main1Left1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFormationShip1>().Main1Right1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFormationShip1>().Main2Left1Pos.gameObject.layer = 6;
            GetComponent<TearSloriusFormationShip1>().Main2Right1Pos.gameObject.layer = 6;
            if (GetComponent<ShipRTS>().ShipNumber == 3 || GetComponent<ShipRTS>().ShipNumber == 4)
            {
                GetComponent<TearSloriusFormationShip1>().Main3Left1Pos.gameObject.layer = 6;
                GetComponent<TearSloriusFormationShip1>().Main3Right1Pos.gameObject.layer = 6;
            }
        }
    }
}