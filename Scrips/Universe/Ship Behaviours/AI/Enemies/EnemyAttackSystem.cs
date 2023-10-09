using System.Collections;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    Animator animator;

    [Header("함포 유형")]
    public int NationType; //종족. 1 = 슬로리어스, 2 = 칸타크리
    public int CannonType; //함포 유형. NationType 1번 기준(슬로리어스), 1 = 유체형 아광속 강습 레이져, 2 = 고체형 초음속 강습 레이져, 3 = 지속형 광속 순항 레이져, 4 = 지속 증폭형 광속 순항 레이져
                           //NationType 2번 기준(칸타크리), 1 = 양자 초가속 주포, 2 = 소립자 초가속 주포, 3 = 단일 미사일, 4 = 멀티 미사일
    public int CannonStyle; //함포 유형

    [Header("함포 공격 스위치")]
    public bool Flagship = false; //기함 여부
    public bool canAttack = false; //공격 가능 여부
    public bool RangeAttack = false; //사거리내에 있을 때에만 공격
    public bool OrderTarget = false; //기함이 소속함선들에게 지정된 대상 명령을 받았을 경우에만 발동

    [Header("함포 공격 정보")]
    public int AmmoDamage; //함포 데미지
    public float FireRate; //시간당 1회 공격
    private float RandomFire; //연사속도를 랜덤화
    public float AttackTime; //FireRate 쿨타임 전용
    public int DamageCount; //데미지를 한번에 몇 번 주는지에 대한 여부

    [Header("대상 감지 사거리")]
    public float AttackEyeRange; //함포가 감지할 수 있는 최대 사거리

    [Header("목표 및 함포 위치")]
    public GameObject TargetShip; //목표 조준 대상
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public Transform TurretLocation; //함포 위치
    public bool TargetOnline; //목표가 지정되었는지에 대한 스위치
    private float TargetMarkTime; //대상을 향해 마크 애니메이션 발동을 딱 한번만 발생하도록 조취
    private float SearchTime = 0; //함포가 대상을 검색하는 시간

    [Header("함포 발사 이펙트")]
    public GameObject SloriusEnergyRay1Prefab; //슬로리어스 익쉬이우 슈루스 아광속 강습 광선 애니메이션용 프리팹
    public GameObject SloriusSolidBeam1Prefab; //아익쉬 쇼우스 고체 광선 충전 이펙트
    public GameObject SloriusSolidBeam1FirePrefab; //아익쉬 쇼우스 고체 광선 발사 이펙트
    public GameObject KantakriMissileAmmoFireEffect; //킬리스-해로스트 8899 가속포 발사 이펙트
    public GameObject KantakriMissileFireReadyEffect; //칸타크리 킬리스-해로스트 8899 조준 이펙트
    public GameObject KantakriMissileFireEffect; //칸타크리 킬리스-해로스트 8899 발사 이펙트
    public GameObject KantakriMultiHitChargeEffect; //게르피르-치타리오 23 소립자 충전 이펙트
    public GameObject KantakriMultiHitFireEffect; //게르피르-치타리오 23 소립자 발사 이펙트

    GameObject SloriusEnergyRay1; //슬로리어스 익쉬이우 슈루스 아광속 강습 광선
    GameObject SloriusSolidBeam1Flagship;
    GameObject SloriusSolidBeam1Formation;
    GameObject KantakriMissile1;
    GameObject KantakriArtillery1;

    public void CannonReinput()
    {
        AttackTime = FireRate * 0.8f;

        //랜덤 포탑 장착
        if (CannonStyle == 1)
        {
            if (NationType == 1)
                CannonType = 1;
            else if (NationType == 2)
                CannonType = 10;
        }
        else if (CannonStyle == 2)
        {
            if (NationType == 1)
                CannonType = 2;
            else if (NationType == 2)
                CannonType = 100;
        }
        else if (CannonStyle == 3)
        {
            if (NationType == 1)
            {
                int RandomCannon = Random.Range(0, 2);

                if (RandomCannon == 0)
                    CannonType = 1;
                else
                    CannonType = 2;
            }
            else if (NationType == 2)
            {
                int RandomCannon = Random.Range(0, 3);

                if (RandomCannon == 0)
                    CannonType = 1;
                else if (RandomCannon == 1)
                    CannonType = 10;
                else if (RandomCannon == 2)
                    CannonType = 100;
            }
        }

        if (NationType == 1) //슬로리어스
        {
            if (CannonType == 1) //슬로리어스 익쉬이우 슈루스 아광속 강습 광선
            {
                if (Flagship == true)
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 30;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 50;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 70;
                    }
                    FireRate = 5;
                    DamageCount = 10;
                }
                else
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 15;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 32;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 48;
                    }
                    FireRate = 3;
                    DamageCount = 7;
                }
            }
            else if (CannonType == 2) //슬로리어스 아익쉬 쇼우스 고체 광선
            {
                if (Flagship == true)
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 175;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 380;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 550;
                    }
                    FireRate = 2.5f;
                    DamageCount = 1;
                }
                else
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 150;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 325;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 475;
                    }
                    FireRate = 3.5f;
                    DamageCount = 1;
                }
            }
        }
        else if (NationType == 2) //칸타크리
        {
            if (CannonType == 1) //칸타크리 킬리스-해로스트 8899 가속포
            {
                if (Flagship == true)
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 6;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 10;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 17;
                    }
                    FireRate = 0.1f;
                    DamageCount = 1;
                }
                else
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 4;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 7;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 11;
                    }
                    FireRate = 0.1f;
                    DamageCount = 1;
                }
            }
            else if (CannonType == 10) //칸타크리 킬리스-해로스트 8899 전멸 분리탄
            {
                if (Flagship == true)
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 163 / 4;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 338 / 4;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 513 / 4;
                    }
                    FireRate = 10;
                    DamageCount = 1;
                }
                else
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 120 / 4;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 235 / 4;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 338 / 4;
                    }
                    FireRate = 10;
                    DamageCount = 1;
                }
            }
            else if (CannonType == 100) //칸타크리 게르피르-치타리오 23 소립자 제트 에너지탄
            {
                if (Flagship == true)
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 320 / 5;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 650 / 5;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 950 / 5;
                    }
                    FireRate = 5;
                    DamageCount = 1;
                }
                else
                {
                    if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 1)
                    {
                        AmmoDamage = 300 / 5;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 2)
                    {
                        AmmoDamage = 600 / 5;
                    }
                    else if (transform.parent.GetComponent<EnemyShipLevelInformation>().Level == 3)
                    {
                        AmmoDamage = 900 / 5;
                    }
                    FireRate = 7;
                    DamageCount = 1;
                }
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //사격이 가능할 경우, 사격 개시
        if (canAttack == true && transform.parent.GetComponent<EnemyShipBehavior>().WarpDriveActive == false)
        {
            //AttackTime 숫자를 FireRate를 넘지 않는 조건에서 실시간으로 증가
            if (AttackTime <= FireRate + RandomFire && TargetShip != null)
                AttackTime += Time.deltaTime;

            if (TargetShip != null) //타겟을 잡은 경우에만 포대 작동
            {
                Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //포대 회전
                transform.right = Vector3.Lerp(transform.right, dir, 8 * Time.deltaTime);
            }

            if (OrderTarget == false) //기함이 존재할 경우, 그리고 기함으로부터 일점사 명령을 받지 않았을 경우, 각 함선마다 가장 가까운 적을 자동 공격
            {
                if (SearchTime <= 3)
                    SearchTime += Time.deltaTime;

                if (SearchTime >= 3)
                {
                    SearchTime = 0;
                    Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, AttackEyeRange, layerMask); //실시간으로 AttackEyeRange 범위내에서 가장 가까운 대상을 검색
                    float shortestDistance = Mathf.Infinity;
                    Collider2D nearestTarget = null;

                   if (TargetShips != null)
                    {
                        float DistanceToMonsters = Vector3.Distance(transform.position, TargetShips.transform.position);

                        if (DistanceToMonsters < shortestDistance) //가장 가까운 타겟으로 변경
                        {
                            shortestDistance = DistanceToMonsters;
                            nearestTarget = TargetShips;
                            TargetMarkTime = 0;
                        }
                    }
                    if (nearestTarget != null && shortestDistance <= AttackEyeRange) //지정된 타겟을 TargetShip리스트에 올리기
                    {
                        TargetShip = nearestTarget.gameObject;
                        TargetOnline = true;
                        gameObject.transform.parent.GetComponent<EnemyShipBehavior>().TargetShip = TargetShip;
                    }
                    else
                    {
                        if (NationType == 2)
                        {
                            if (KantakriMissileAmmoFireEffect != null)
                                KantakriMissileAmmoFireEffect.SetActive(false);
                            if (Flagship == true)
                                animator.SetBool("Kilris-Haerost 8899 ammo(Flagship), Kantakri", false);
                            else
                                animator.SetBool("Kilris-Haerost 8899 ammo(Formation), Kantakri", false);
                        }
                        TargetShip = null;
                        TargetOnline = false;
                        gameObject.transform.parent.GetComponent<EnemyShipBehavior>().TargetShip = null;
                    }
                }
            }
            else
            {
                if (Flagship == true) //기함일 경우, 소속함선들에게 일점사 명령을 내리기 위한 전용 명령
                {
                    if (NationType != 2 && CannonType != 3 && CannonType != 4) //칸타크리 미사일이 아닌 일반적으로 일직선으로 날아가는 무기들
                    {

                    }
                    else if (NationType == 2 && CannonType == 3 && CannonType == 4) //칸타크리 미사일 유형
                    {

                    }
                }
                else //소속함선일 경우, 기함으로부터 받은 일점사 명령을 하달 받는다.
                {
                    if (NationType != 2 && CannonType != 3 && CannonType != 4) //칸타크리 미사일이 아닌 일반적으로 일직선으로 날아가는 무기들
                    {

                    }
                    else if (NationType == 2 && CannonType == 3 && CannonType == 4) //칸타크리 미사일 유형
                    {

                    }
                }
            }

            if (RangeAttack == true) //사격 가능한 사거리를 벗어나면 사격 중지
            {
                if (AttackTime >= FireRate - 3) //무기 발사 직전 이펙트
                {
                    if (NationType == 1 && CannonType == 2)
                        SloriusSolidBeam1Prefab.SetActive(true);
                    if (NationType == 2 && CannonType == 10)
                        KantakriMissileFireReadyEffect.SetActive(true);
                    if (NationType == 2 && CannonType == 100)
                        KantakriMultiHitChargeEffect.SetActive(true);
                }

                //초당 무기 발사
                if (AttackTime >= FireRate + RandomFire)
                {
                    AttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (NationType == 1 && CannonType == 1) //슬로리어스 익쉬이우 슈루스 아광속 강습 광선
                    {
                        this.gameObject.GetComponent<RayAttachment>().Fire = true;
                        this.gameObject.GetComponent<RayAttachment>().FireEndPos = TargetShip.transform.position;
                        StartCoroutine(FireSloriusEnergyRay1());
                        StartCoroutine(FireSloriusEnergyRayAnimation());
                    }
                    else if (NationType == 1 && CannonType == 2) //슬로리어스 아익쉬 쇼우스 고체 광선
                    {
                        StartCoroutine(FireSloriusSolidBeam1());
                    }
                    else if (NationType == 2 && CannonType == 1) //칸타크리 킬리스-해로스트 8899 가속포
                    {
                        if (Flagship == true)
                        {
                            KantakriMissileAmmoFireEffect.SetActive(true);
                            animator.SetBool("Kilris-Haerost 8899 ammo(Flagship), Kantakri", true);
                            KantakriArtillerySystem1();
                        }
                        else
                        {
                            KantakriMissileAmmoFireEffect.SetActive(true);
                            animator.SetBool("Kilris-Haerost 8899 ammo(Formation), Kantakri", true);
                            KantakriArtillerySystem2();
                        }
                    }
                    else if (NationType == 2 && CannonType == 10) //칸타크리 킬리스-해로스트 8899 전멸 분리탄
                        StartCoroutine(KantakriWeaponSystem1());
                    else if (NationType == 2 && CannonType == 100) //칸타크리 게르피르-치타리오 23 소립자 제트 에너지탄
                        StartCoroutine(KantakriMuitiHitSystem1());
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;

            if (NationType == 1)
            {
                //this.gameObject.GetComponent<RayAttachment>().Fire = false;
            }
        }
    }

    //슬로리어스 익쉬이우 슈루스 아광속 강습 광선 연출
    IEnumerator FireSloriusEnergyRay1()
    {
        for (int i = 0; i <= DamageCount; i++)
        {
            SloriusWeaponSystem1();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<RayAttachment>().Fire = false;
        yield return new WaitForSeconds(RandomFire);
    }

    //슬로리어스 아익쉬 쇼우스 고체 광선 연출
    IEnumerator FireSloriusSolidBeam1()
    {
        for (int i = 0; i < DamageCount; i++)
        {
            SloriusSolidBeam1FirePrefab.SetActive(true);
            if (Flagship == true)
            {
                SloriusSolidBeamSystem1Flagship();
                animator.SetBool("Aickshi Shouus fire(Flagship), Slorius", true);
            }
            else
            {
                SloriusSolidBeamSystem1Formation();
                animator.SetBool("Aickshi Shouus fire(Formation), Slorius", true);
            }

            yield return new WaitForSeconds(0.8f);
            if (Flagship == true)
                animator.SetBool("Aickshi Shouus fire(Flagship), Slorius", false);
            else
                animator.SetBool("Aickshi Shouus fire(Formation), Slorius", false);
            yield return new WaitForSeconds(0.2f);
            SloriusSolidBeam1FirePrefab.SetActive(false);
        }
        SloriusSolidBeam1Prefab.SetActive(false);
        yield return new WaitForSeconds(RandomFire);
    }

    IEnumerator FireSloriusEnergyRayAnimation()
    {
        if (Flagship == true)
        {
            animator.SetBool("Ickshiiu Shuluus(Flagship), Slorius", true);
            yield return new WaitForSeconds(4);
            animator.SetBool("Ickshiiu Shuluus(Flagship), Slorius", false);
        }
        else
        {
            animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", true);
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", false);
        }
    }

    //슬로리어스 익쉬이우 슈루스 아광속 강습 광선 데미지 전달체 생성
    void SloriusWeaponSystem1()
    {
        SloriusEnergyRay1 = ShipAmmoObjectPool.instance.Loader("SloriusEnergyRay1");
        SloriusEnergyRay1.transform.position = TurretLocation.transform.position;
        SloriusEnergyRay1.transform.rotation = TurretLocation.transform.rotation;
        SloriusEnergyRay1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "SloriusEnergyRay1Explosion", "SloriusEnergyRay1Delete", "SloriusEnergyRay1ExplosionDelete");
    }

    //슬로리어스 아익쉬 쇼우스 고체 광선(기함) 전달체 생성
    void SloriusSolidBeamSystem1Flagship()
    {
        SloriusSolidBeam1Flagship = ShipAmmoObjectPool.instance.Loader("SloriusSolidBeam1Flagship");
        SloriusSolidBeam1Flagship.transform.position = TurretLocation.transform.position;
        SloriusSolidBeam1Flagship.transform.rotation = TurretLocation.transform.rotation;
        SloriusSolidBeam1Flagship.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "SloriusSolidBeam1ExplosionFlagship", "SloriusSolidBeam1FlagshipDelete", "SloriusSolidBeam1ExplosionFlagshipDelete");
    }
    //슬로리어스 아익쉬 쇼우스 고체 광선(편대함) 전달체 생성
    void SloriusSolidBeamSystem1Formation()
    {
        SloriusSolidBeam1Formation = ShipAmmoObjectPool.instance.Loader("SloriusSolidBeam1Formation");
        SloriusSolidBeam1Formation.transform.position = TurretLocation.transform.position;
        SloriusSolidBeam1Formation.transform.rotation = TurretLocation.transform.rotation;
        SloriusSolidBeam1Formation.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "SloriusSolidBeam1ExplosionFormation", "SloriusSolidBeam1FormationDelete", "SloriusSolidBeam1ExplosionFormationDelete");
    }

    //칸타크리 킬리스-해로스트 8899 가속포 데미지 전달체 생성
    void KantakriArtillerySystem1()
    {
        KantakriArtillery1 = ShipAmmoObjectPool.instance.Loader("KantakriArtillery1");
        KantakriArtillery1.transform.position = TurretLocation.transform.position;
        KantakriArtillery1.transform.rotation = TurretLocation.transform.rotation;
        KantakriArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "KantakriArtillery1Explosion", "KantakriArtillery1Delete", "KantakriArtillery1ExplosionDelete");
    }
    //칸타크리 킬리스-해로스트 8899 가속포 데미지 전달체 생성(편대함)
    void KantakriArtillerySystem2()
    {
        KantakriArtillery1 = ShipAmmoObjectPool.instance.Loader("KantakriArtillery2");
        KantakriArtillery1.transform.position = TurretLocation.transform.position;
        KantakriArtillery1.transform.rotation = TurretLocation.transform.rotation;
        KantakriArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "KantakriArtillery2Explosion", "KantakriArtillery2Delete", "KantakriArtillery2ExplosionDelete");
    }

    //칸타크리 킬리스-해로스트 8899 데미지 전달체 생성
    IEnumerator KantakriWeaponSystem1()
    {
        KantakriMissileFireReadyEffect.SetActive(false);
        KantakriMissileFireEffect.SetActive(true);
        if (Flagship == true)
            animator.SetBool("Kilris-Haerost 8899(Flagship), Kantakri", true);
        else
            animator.SetBool("Kilris-Haerost 8899(Formation), Kantakri", true);
        KantakriMissile1 = ShipAmmoObjectPool.instance.Loader("KantakriMissile1Box");
        KantakriMissile1.transform.position = TurretLocation.transform.position;
        KantakriMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z + 90);
        KantakriMissile1.GetComponent<KantakriMissileFirstMovement1>().SetDamage(AmmoDamage, TargetShip, "KantakriMissile1Explosion");
        yield return new WaitForSeconds(1);
        KantakriMissileFireEffect.SetActive(false);
        if (Flagship == true)
            animator.SetBool("Kilris-Haerost 8899(Flagship), Kantakri", false);
        else
            animator.SetBool("Kilris-Haerost 8899(Formation), Kantakri", false);
    }

    //칸타크리 게르피르-치타리오 23 소립자 제트 에너지탄 데미지 전달체 생성
    IEnumerator KantakriMuitiHitSystem1()
    {
        KantakriMultiHitChargeEffect.SetActive(false);
        KantakriMultiHitFireEffect.SetActive(true);
        KantakriMissileFireEffect.SetActive(true);
        if (Flagship == true)
        {
            KantakriMissile1 = ShipAmmoObjectPool.instance.Loader("KantakriMultiHitAmmo1Flagship");
            KantakriMissile1.transform.position = TurretLocation.transform.position;
            KantakriMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z + 90);
            KantakriMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "KantakriMultiHitAmmoExplosion1Flagship");
        }
        else
        {
            KantakriMissile1 = ShipAmmoObjectPool.instance.Loader("KantakriMultiHitAmmo1Formation");
            KantakriMissile1.transform.position = TurretLocation.transform.position;
            KantakriMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z + 90);
            KantakriMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "KantakriMultiHitAmmo1ExplosionFormation");
        }

        yield return new WaitForSeconds(1);
        KantakriMissileFireEffect.SetActive(false);
    }
}