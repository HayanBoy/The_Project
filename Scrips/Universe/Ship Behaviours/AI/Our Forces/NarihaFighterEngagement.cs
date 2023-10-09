using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarihaFighterEngagement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    public GameObject MotherCarrier;
    public GameObject MyFlagship;
    public string DeleteName;

    [Header("함재기 공격 정보")]
    public GameObject TargetShip; //목표 조준 대상
    public float FlightSpeed; //비행 속도
    public float WarpSpeed; //워프 속도
    public int FireDamage; //데미지량
    public int HitPoint; //체력
    private Vector3 endposition;

    [Header("함재기 작전 정보")]
    public float EngageRange; //공격 최소 거리
    public float RateOfFire; //시간당 공격
    private int FireEngage; //공격 명령
    private float RateOfFireTime;
    private float FlightTemp;
    private float LandingStemp;
    Coroutine flightTargetAround;
    Coroutine flightMove;

    [Header("함재기 작전 상태")]
    public bool isFight = false; //교전 상태
    public bool Ingagement = false; //작전 상태
    public bool WarpDriveActive = false; //워프 상태. 이는 모함이 워프 항법에 돌입했으므로, 모함을 따라간다.
    public bool Emagancy = false; //모함이 파괴되어 대피
    private float WarpdestinationStemp;
    Vector3 MotherCarrierDestination; //워프 도착 장소

    [Header("함재기 기타 정보")]
    public bool WarpActivate; //워프여부. 함대가 워프에 돌입하면 공격을 멈추고 함선으로 강제 귀환한다.
    public GameObject FirePrefab;
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    private float SearchTime = 2; //함포가 대상을 검색하는 시간

    GameObject NarihaFighterAmmo;
    GameObject NarihabomerMissile;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        isFight = false;
        FireEngage = 0;
        RateOfFireTime = 0;
        FlightTemp = 0;
        LandingStemp = 0;
    }

    public void GetInformation(int HP, int Damage)
    {
        HitPoint = HP;
        FireDamage = Damage;
        StartCoroutine(StartEngine());
    }

    //첫 사출
    IEnumerator StartEngine()
    {
        yield return new WaitForSeconds(0.25f);
        RateOfFireTime = RateOfFire;
        FireEngage = 1;
        isFight = true;
    }

    void Update()
    {
        if (MotherCarrier != null)
        {
            TargetShip = MotherCarrier.GetComponent<NarihaFighterSystem>().TargetShip;

            if (Ingagement == true && TargetShip != null)
            {
                float distanceFromTarget = Vector2.Distance(TargetShip.transform.position, transform.position);

                if (isFight == true)
                {
                    //공격을 이미 한번 한 상태이므로, 목표 대상 주변을 선회
                    if (RateOfFireTime < RateOfFire)
                    {
                        RateOfFireTime += Time.deltaTime;
                        if (FlightTemp == 0)
                        {
                            FlightTemp += Time.deltaTime;
                            flightTargetAround = StartCoroutine(FlightTargetAround());
                        }
                    }
                    //공격 가능할 때 한번 공격하기 위해 접근
                    else
                    {
                        FireEngage = 1;

                        if (distanceFromTarget >= EngageRange)
                        {
                            if (flightTargetAround != null)
                                StopCoroutine(flightTargetAround);
                            transform.position += transform.up * FlightSpeed * Time.deltaTime;
                            Vector3 dir = (TargetShip.transform.position - transform.position).normalized;
                            transform.up = Vector3.Lerp(transform.up, dir, 4 * Time.deltaTime);
                        }
                        else //공격
                        {
                            if (FireEngage == 1)
                            {
                                FireEngage = 0;
                                RateOfFireTime = 0;
                                FirePrefab.SetActive(true);
                                NarihabomerMissile = ShipAmmoObjectPool.instance.Loader("NarihaBomer1Artillery1");
                                NarihabomerMissile.transform.position = transform.position;
                                NarihabomerMissile.transform.rotation = transform.rotation;
                                NarihabomerMissile.GetComponent<CannonMovement>().SetDamage(FireDamage, TargetShip, "NarihaBomer1Artillery1Explosion", "NarihaBomer1Artillery1Delete", "NarihaBomer1Artillery1ExplosionDelete");
                                FlightTemp = 0;
                                Invoke("EffectTurnOff", 0.85f);
                            }
                        }
                    }
                }
                else
                {
                    transform.position += transform.up * FlightSpeed * Time.deltaTime;
                }
            }

            //목표물이 없거나 함선으로부터 귀환명령을 받은 경우
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == false || TargetShip == null)
            {
                if (WarpDriveActive == false) //비 워프 중에서만 실시한다.
                {
                    if (flightTargetAround != null)
                        StopCoroutine(flightTargetAround);
                    if (flightMove != null)
                        StopCoroutine(flightMove);

                    if (Vector2.Distance(MotherCarrier.transform.position, transform.position) > 1)
                    {
                        transform.position += transform.up * FlightSpeed * Time.deltaTime;
                        Vector3 dir = (MotherCarrier.transform.position - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, 10 * Time.deltaTime);
                    }
                    if (Vector2.Distance(MotherCarrier.transform.position, transform.position) <= 1)
                    {
                        if (LandingStemp == 0)
                        {
                            LandingStemp += Time.deltaTime;
                            MotherCarrier.GetComponent<NarihaFighterSystem>().EngagedFighterList.Remove(gameObject);
                            ShipAmmoObjectPool.instance.Deleter(DeleteName);
                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }

            //모함이 워프 중일 경우, 같이 워프를 통해 따라가기
            if (WarpDriveActive == true) //워프 도착 지점으로 워프 시작
            {
                if (WarpdestinationStemp == 0)
                {
                    WarpdestinationStemp += Time.deltaTime;
                    TargetShip = null;
                    isFight = false;
                    MotherCarrierDestination = new Vector3(MotherCarrier.gameObject.transform.parent.GetComponent<MoveVelocity>().WarpformationIndex.position.x + Random.Range(1, 5),
                        MotherCarrier.gameObject.transform.parent.GetComponent<MoveVelocity>().WarpformationIndex.position.y + Random.Range(1, 5), 0);
                }

                transform.position = Vector3.MoveTowards(transform.position, MotherCarrierDestination, WarpSpeed * Time.deltaTime);
                Vector3 DirectionAtFormation = (MotherCarrierDestination - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, targetRotation, 2 * Time.deltaTime);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) > 50f)
                    anim.SetFloat("Warp, Slorius Flagship", 1);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 15f)
                    anim.SetFloat("Warp, Slorius Flagship", 2);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 1)
                {
                    WarpDriveActive = false;
                    WarpdestinationStemp = 0;
                }
            }
        }
        else //모함이 파괴되었을 경우, 다른 곳으로 워프한다.
        {
            if (WarpdestinationStemp == 0)
            {
                WarpdestinationStemp += Time.deltaTime;
                isFight = false;
                MotherCarrierDestination = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
                StartCoroutine(EmagancyFighter());
            }

            if (Emagancy == false) //남은 폭장량으로 일정시간 동안 가장 가까이 있는 대상을 공격한 뒤, 워프
            {
                if (MyFlagship != null)
                    TargetShip = MyFlagship.GetComponent<FlagshipAttackSkill>().TargetShip;
                else
                    Emagancy = true;
            }
        }

        if (Emagancy == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, MotherCarrierDestination, WarpSpeed * Time.deltaTime);
            Vector3 DirectionAtFormation = (MotherCarrierDestination - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
            rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, targetRotation, 2 * Time.deltaTime);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) > 50f)
                anim.SetFloat("Warp, Slorius Flagship", 1);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 15f)
                anim.SetFloat("Warp, Slorius Flagship", 2);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 1)
            {
                WarpDriveActive = false;
                Emagancy = false;
                WarpdestinationStemp = 0;
                ShipAmmoObjectPool.instance.Deleter(DeleteName);
                this.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator EmagancyFighter()
    {
        float Time = Random.Range(5, 10);
        yield return new WaitForSeconds(Time);
        Emagancy = true;
    }

    IEnumerator FlightTargetAround()
    {
        if (MotherCarrier != null)
        {
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
            {
                while (true)
                {
                    float RandomMovement = Random.Range(-4, 4);
                    float RandomTime = Random.Range(0.25f, 1);

                    if (MotherCarrier != null)
                    {
                        if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
                            endposition = new Vector3(TargetShip.transform.position.x + RandomMovement, TargetShip.transform.position.y + RandomMovement, transform.position.z);
                    }

                    if (flightMove != null)
                        StopCoroutine(flightMove);

                    flightMove = StartCoroutine(FlightMove(rb2d, FlightSpeed));
                    yield return new WaitForSeconds(RandomTime);
                }
            }
        }
    }

    IEnumerator FlightMove(Rigidbody2D rigidbodyToMove, float speed)
    {
        if (MotherCarrier != null)
        {
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
            {
                float remainingDistance = (transform.position - endposition).sqrMagnitude;

                while (remainingDistance > float.Epsilon)
                {
                    //이동
                    if (rigidbodyToMove != null)
                    {
                        transform.position += transform.up * FlightSpeed * Time.deltaTime;
                        Vector3 dir = (endposition - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, 4 * Time.deltaTime);
                        remainingDistance = (transform.position - endposition).sqrMagnitude;
                    }
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }

    void EffectTurnOff()
    {
        FirePrefab.SetActive(false);
    }
}