using System.Collections;
using UnityEngine;

public class EnemyShipBehavior : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rb2D;

    [Header("함대 유형 및 명령")]
    public int NationType; //종족여부. 1 = 나리하 인류연합 아군, 2 = 슬로리어스, 3 = 칸타크리
    public bool FlagShip; //기함인지 판단하기 위한 여부
    public bool FormationOn; //편대모드 활성화 여부
    public bool FlagshipFirstDestroy; //기함이 먼저 격침되었는지 여부
    //public bool MoveOrder = false; //움직임이 명령되었는지 확인하는 스위치

    [Header("함선 위치 정보")]
    public Transform FormationIndex; //소속 함선의 각 지정된 자리영역
    public Transform WarpformationIndex; //FormationIndex의 워프 영역용
    Vector3 DirectionAtFormation; //FormationIndex를 통해 계산되는 편대 영역 도착 지점
    Vector3 DirectionAtMoving; //DestinationArea를 통해 계산되는 방향전환용 도착 지점
    public Vector3 DestinationArea; //지정된 도착 지점
    public float RotateSpeed; //방향전환 속도
    public float MoveOrderStemp; //함대모드 전환 후, 기함이 단독으로 움직일 때, 기함의 현재 도착지점과 기함의 회전값을 딱 한번만 불러오기

    [Header("가속도 및 감속도")]
    public float BoostSpeed; //설정 속도
    public float WarpSpeed; //워프 속도
    public float CurrentSpeed; //현재 속도
    private float AcceleratorSpeed; //가속도
    private float AccelAndDecelRange = 2f; //가속 및 감속이 허용되는 거리
    float RangeBetweenStartAndDestination; //시작과 도착지점의 거리. 함선의 가속 및 감속거리를 확보하기 위함

    [Header("활동 범위")]
    public GameObject MyFlagship;
    //public float FlagshipActiveSite; //기함을 중심으로 활동하는 제한된 영역
    private float NearFlagShipRange; //가장 가까운 기함과의 거리
    private float FlagShipRangeIndex; //기함과의 거리 저장용
    private int SetMyFlagShip; //가장 가까운 기함 번호 저장용
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가

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
    public float DisctanceFromTarget;
    public bool WarpDriveReady; //워프 준비
    public bool WarpDriveActive; //워프 가동 여부
    private float WarpCompleteTemp; //Update문에서 딱 한번만 가동하도록 조취
    private float WarpGeneratorTemp; //Update문에서 딱 한번만 가동하도록 조취
    public GameObject WarpGenerator; //워프 발생 이펙트
    public GameObject WarpArriveEffect; //워프 도착 이펙트

    private float MoveStemp;
    private float MoveStempArea;

    private Vector3 endposition;
    Coroutine moveTargetAround;
    Coroutine moveTargetAroundArea;
    Coroutine move;

    //워프 시작
    public void WarpSpeedUp(bool boolean)
    {
        WarpDriveActive = boolean;
        CurrentSpeed = WarpSpeed;
        WarpCompleteTemp = 0;
        WarpGeneratorTemp = 0;
        anim.SetFloat("Warp, Slorius Flagship", 1);
        WarpDriveReady = false;
    }

    private void Awake()
    {
        DestinationArea = transform.position;
    }

    private void OnEnable()
    {
        if (FlagshipFirstDestroy == true)
            FlagshipFirstDestroy = false;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        AcceleratorSpeed = BoostSpeed / (1250 / BoostSpeed); //1250은 가속도 연산값이다. 최대속도에 맞춰서 가속도가 연산된다.
    }

    //도착 지점 받기
    public void SetVelocity(Vector3 MovePositionDirection)
    {
        DestinationArea = MovePositionDirection;
        RangeBetweenStartAndDestination = Vector3.Distance(MovePositionDirection, transform.position);
        DirectionAtMoving = (MovePositionDirection - transform.position).normalized; //방향 전환용 도착지점을 계산
    }

    //워프도착 직후 워프 애니메이션 초기화
    IEnumerator WarpComplete()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetFloat("Warp, Slorius Flagship", 0);
    }

    private void FixedUpdate()
    {
        if (FlagShip == true)
        {
            if (WarpDriveActive == true)
            {
                //함선 기본 이동
                MoveDir = (DestinationArea - transform.position).normalized;
                DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                TargetShip = null;

                if (DisctanceFromTarget <= 200 && DisctanceFromTarget >= 100)
                {
                    if (WarpGeneratorTemp == 0)
                    {
                        WarpGeneratorTemp += Time.deltaTime;
                        Instantiate(WarpGenerator, DestinationArea, Quaternion.identity);
                    }
                }
                if (DisctanceFromTarget <= 15f)
                    anim.SetFloat("Warp, Slorius Flagship", 2);

                if (DisctanceFromTarget <= 2.5f)
                    CurrentSpeed = WarpSpeed * 0.1f;

                if (DisctanceFromTarget <= 1) //목적지에 도달할 경우, 함선 이동 정지
                {
                    //Debug.Log("적 워프 정지");
                    WarpDriveActive = false;
                    MoveDir = Vector3.zero;
                    CurrentSpeed = 0;

                    if (WarpCompleteTemp == 0)
                    {
                        WarpCompleteTemp += Time.deltaTime;

                        Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                        StartCoroutine(WarpComplete());

                        Turret1.GetComponent<EnemyAttackSystem>().canAttack = true;
                        Turret2.GetComponent<EnemyAttackSystem>().canAttack = true;
                        Turret3.GetComponent<EnemyAttackSystem>().canAttack = true;
                        Turret4.GetComponent<EnemyAttackSystem>().canAttack = true;
                        Turret5.GetComponent<EnemyAttackSystem>().canAttack = true;
                        Turret6.GetComponent<EnemyAttackSystem>().canAttack = true;
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

                distanceFromTarget = Vector2.Distance(TargetShip.transform.position, transform.position);

                if (distanceFromTarget > MaxDamageRange)
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret3.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret4.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret5.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret6.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                    }
                    else
                    {
                        Turret1.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<EnemyAttackSystem>().RangeAttack = false;
                    }
                }
                else
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret3.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret4.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret5.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret6.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                    }
                    else
                    {
                        Turret1.GetComponent<EnemyAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<EnemyAttackSystem>().RangeAttack = true;
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
                    if (TargetShip == null) //비전투상황에서의 움직임
                    {
                        Vector3 dir1 = (DestinationArea - transform.position).normalized;
                        float DistanceFromPlanet = Vector2.Distance(DestinationArea, transform.position);

                        if (DistanceFromPlanet > 110) //행성 최대 주둔 거리를 벗어나면 접근
                        {
                            MoveStemp = 0;
                            MoveStempArea = 0;

                            if (moveTargetAroundArea != null)
                                StopCoroutine(moveTargetAroundArea);
                            if (moveTargetAround != null)
                                StopCoroutine(moveTargetAround);
                            if (move != null)
                                StopCoroutine(move);

                            Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                            rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(DestinationArea.x, DestinationArea.y), CurrentSpeed * 2 * Time.deltaTime);

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                        else if (DistanceFromPlanet <= 110)
                        {
                            if (MoveStempArea == 0)
                            {
                                MoveStempArea += Time.deltaTime;
                                MoveStemp = 0;

                                if (moveTargetAround != null)
                                    StopCoroutine(moveTargetAround);
                                if (move != null)
                                    StopCoroutine(move);

                                moveTargetAroundArea = StartCoroutine(MoveTargetAroundArea());
                            }

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                    }
                    else //플레이어 함선을 향해 접근
                    {
                        Vector3 dir1 = (TargetShip.transform.position - transform.position).normalized;
                        Vector3 dir2 = (TargetShip.transform.position + transform.position).normalized;

                        if (distanceFromTarget > MaxDamageRange) //타겟이 최대 사거리를 벗어나면 접근
                        {
                            MoveStemp = 0;
                            MoveStempArea = 0;

                            if (moveTargetAroundArea != null)
                                StopCoroutine(moveTargetAroundArea);
                            if (moveTargetAround != null)
                                StopCoroutine(moveTargetAround);
                            if (move != null)
                                StopCoroutine(move);

                            Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                            rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                            transform.position = Vector2.MoveTowards(transform.position, TargetShip.transform.position, CurrentSpeed * Time.deltaTime);

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                        else if (distanceFromTarget < MaxDamageRange)
                        {
                            if (MoveStemp == 0)
                            {
                                MoveStemp += Time.deltaTime;
                                MoveStempArea = 0;

                                if (moveTargetAroundArea != null)
                                    StopCoroutine(moveTargetAroundArea);
                                if (move != null)
                                    StopCoroutine(move);
                                moveTargetAround = StartCoroutine(MoveTargetAround());
                            }

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
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
                            StartCoroutine(WarpComplete());
                            Turret1.GetComponent<EnemyAttackSystem>().canAttack = true;
                            Turret2.GetComponent<EnemyAttackSystem>().canAttack = true;
                        }
                    }
                }
            }
            else //함대진이 가동 중일 경우 기함 주변으로 정해진 배열에 맞춰 움직인다.
            {
                if (WarpDriveActive == false)
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
                else //워프 도착 지점으로 워프 시작
                {
                    transform.position = Vector3.MoveTowards(transform.position, WarpformationIndex.position, CurrentSpeed * Time.deltaTime);
                    TargetShip = null;
                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 200 && Vector3.Distance(WarpformationIndex.position, transform.position) >= 100)
                    {
                        if (WarpGeneratorTemp == 0)
                        {
                            WarpGeneratorTemp += Time.deltaTime;
                            Instantiate(WarpGenerator, WarpformationIndex.position, Quaternion.identity);
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
                        CurrentSpeed = 0;

                        if (WarpCompleteTemp == 0)
                        {
                            WarpCompleteTemp += Time.deltaTime;
                            Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                            StartCoroutine(WarpComplete());
                            Turret1.GetComponent<EnemyAttackSystem>().canAttack = true;
                            Turret2.GetComponent<EnemyAttackSystem>().canAttack = true;
                        }
                    }
                }
            }
        }
        else //기함
        {
            if (WarpDriveActive == false)
            {
                if (TargetShip == null) //비전투상황에서의 움직임
                {
                    Vector3 dir1 = (DestinationArea - transform.position).normalized;
                    float DistanceFromPlanet = Vector2.Distance(DestinationArea, transform.position);

                    if (DistanceFromPlanet > 110) //행성 최대 주둔 거리를 벗어나면 접근
                    {
                        MoveStemp = 0;
                        MoveStempArea = 0;

                        if (moveTargetAroundArea != null)
                            StopCoroutine(moveTargetAroundArea);
                        if (moveTargetAround != null)
                            StopCoroutine(moveTargetAround);
                        if (move != null)
                            StopCoroutine(move);

                        Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(DestinationArea.x, DestinationArea.y), CurrentSpeed * 2 * Time.deltaTime);

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                    else if (DistanceFromPlanet <= 110)
                    {
                        if (MoveStempArea == 0)
                        {
                            MoveStempArea += Time.deltaTime;
                            MoveStemp = 0;

                            if (moveTargetAround != null)
                                StopCoroutine(moveTargetAround);
                            if (move != null)
                                StopCoroutine(move);

                            moveTargetAroundArea = StartCoroutine(MoveTargetAroundArea());
                        }

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                }
                else
                {
                    Vector3 dir1 = (TargetShip.transform.position - transform.position).normalized;
                    Vector3 dir2 = (TargetShip.transform.position + transform.position).normalized;

                    if (distanceFromTarget > MaxDamageRange) //타겟이 최대 사거리를 벗어나면 접근
                    {
                        MoveStemp = 0;
                        MoveStempArea = 0;

                        if (moveTargetAroundArea != null)
                            StopCoroutine(moveTargetAroundArea);
                        if (moveTargetAround != null)
                            StopCoroutine(moveTargetAround);
                        if (move != null)
                            StopCoroutine(move);

                        Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                        transform.position = Vector2.MoveTowards(transform.position, TargetShip.transform.position, CurrentSpeed * Time.deltaTime);

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                    else if (distanceFromTarget < MaxDamageRange) //사거리 내에 있으면 자동으로 선회
                    {
                        if (MoveStemp == 0)
                        {
                            MoveStemp += Time.deltaTime;
                            MoveStempArea = 0;

                            if (moveTargetAroundArea != null)
                                StopCoroutine(moveTargetAroundArea);
                            if (move != null)
                                StopCoroutine(move);

                            moveTargetAround = StartCoroutine(MoveTargetAround());
                        }

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                }
            }
        }
    }

    //플레이어 함선 주변을 선회
    IEnumerator MoveTargetAround()
    {
        if (TargetShip != null)
        {
            while (true)
            {
                float RandomTime = Random.Range(5, 10);
                float RandomDistance = Random.Range(-30, 30);

                if (TargetShip != null)
                    endposition = new Vector3(Random.Range(TargetShip.transform.position.x + RandomDistance, TargetShip.transform.position.x - RandomDistance), Random.Range(TargetShip.transform.position.y + RandomDistance, TargetShip.transform.position.y - RandomDistance), transform.position.z);

                if (move != null)
                    StopCoroutine(move);

                move = StartCoroutine(Move(rb2D, CurrentSpeed));
                yield return new WaitForSeconds(RandomTime);
            }
        }
    }

    //자신이 있는 지역 주변을 선회
    IEnumerator MoveTargetAroundArea()
    {
        if (TargetShip == null)
        {
            while (true)
            {
                float RandomTime = Random.Range(5, 10);
                float RandomDistance = Random.Range(-100, 100);

                if (TargetShip == null)
                    endposition = new Vector3(Random.Range(DestinationArea.x + RandomDistance, DestinationArea.x - RandomDistance), Random.Range(DestinationArea.y + RandomDistance, DestinationArea.y - RandomDistance), transform.position.z);

                if (move != null)
                    StopCoroutine(move);

                move = StartCoroutine(Move(rb2D, CurrentSpeed));
                yield return new WaitForSeconds(RandomTime);
            }
        }
    }

    //선회 구현
    IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
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