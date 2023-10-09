using System.Collections;
using UnityEngine;

public class NarihaTurretAttackSystem : MonoBehaviour
{
    Animator anim;

    [Header("함포 유형")]
    public int CannonType; //함포 유형. 1 = 양자점프 주포, 2 = 초과점프 주포, 3 = 단일 미사일, 4 = 멀티 미사일, 5 = 유체형 초과점프 프로젝터
    public int CannonRangeType; //함포 거리 유형. 1 = 장거리 함포, 2 = 단거리 함포

    [Header("함포 공격 스위치")]
    public bool Flagship = false; //기함 여부
    public bool canAttack = false; //공격 가능 여부
    public bool RangeAttack = false; //사거리내에 있을 때에만 공격
    public bool OrderTarget = false; //기함이 소속함선들에게 지정된 대상 명령을 받았을 경우에만 발동

    [Header("함포 종류별 프리팹")]
    public GameObject SilenceSist;
    public GameObject Catroy;

    [Header("함포 공격 정보")]
    public int AmmoDamage; //함포 데미지
    public float FireRate; //시간당 1회 공격
    private float RandomFire; //연사속도를 랜덤화
    public float AttackTime; //FireRate 쿨타임 전용
    public int DamageCount; //데미지를 한번에 몇 번 주는지에 대한 여부

    [Header("업그레이드 함포 공격 정보")]
    public int AmmoDamageUpgrade;
    public float FireRateUpgrade;

    [Header("함포 사거리")]
    public float AttackEyeRange; //공격가능한 사거리

    [Header("목표 및 함포 위치")]
    public GameObject TargetShip; //목표 조준 대상
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public Transform TurretLocation; //함포 위치
    public bool TargetOnline; //목표가 지정되었는지에 대한 스위치
    private float TargetMarkTime; //대상을 향해 마크 애니메이션 발동을 딱 한번만 발생하도록 조취
    private float SearchTime = 2; //함포가 대상을 검색하는 시간

    [Header("함포 발사 이펙트")]
    public GameObject NarihaFireEffect; //나리하 사일런스 시스트 양자 점프 가속포
    public GameObject NarihaFireEffect2; //나리하 초과점프 가속포
    public GameObject MissileFireEffect; //나리하 미사일 발사 연출
    GameObject NarihaArtillery1;
    GameObject NarihaOverJumpArtillery1;
    GameObject NarihaMissile1;

    public AudioClip SilentSistFireSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackTime = FireRate * 0.8f;
        CannonReinput();
    }

    public void CannonReinput()
    {
        if (CannonType == 1) //사일런스 시스트(연발 주포)
        {
            SilenceSist.SetActive(true);
            Catroy.SetActive(false);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipSilenceSistDamage;
                FireRate = 3;
                DamageCount = 4;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationSilenceSistDamage;
                FireRate = 3;
                DamageCount = 2;
            }
        }
        else if (CannonType == 2) //초과점프(단발 주포)
        {
            SilenceSist.SetActive(true);
            Catroy.SetActive(false);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipOverJumpDamage;
                FireRate = 2.5f;
                DamageCount = 1;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationOverJumpDamage;
                FireRate = 5;
                DamageCount = 1;
            }
        }
        else if (CannonType == 3) //세드 릴리-345 단발 미사일
        {
            SilenceSist.SetActive(false);
            Catroy.SetActive(true);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipSadLilly345Damage;
                FireRate = 1.5f;
                DamageCount = 1;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationSadLilly345Damage;
                FireRate = 3;
                DamageCount = 1;
            }
        }
        else if (CannonType == 4) //델타 니들-42 할리스트 멀티 미사일
        {
            SilenceSist.SetActive(false);
            Catroy.SetActive(true);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipDeltaNeedle42HalistDamage;
                FireRate = 3;
                DamageCount = 5;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationDeltaNeedle42HalistDamage;
                FireRate = 3;
                DamageCount = 3;
            }
        }
    }

    void Update()
    {
        //사격이 가능할 경우, 사격 개시
        if (canAttack == true)
        {
            //AttackTime 숫자를 FireRate를 넘지 않는 조건에서 실시간으로 증가
            if (AttackTime <= FireRate + RandomFire)
                AttackTime += Time.deltaTime;

            if (TargetShip != null) //타겟을 잡은 경우에만 포대 작동
            {
                Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //포대 회전
                transform.right = Vector3.Lerp(transform.right, dir, 3 * Time.deltaTime);
            }

            if (OrderTarget == false) //기함이 존재할 경우, 그리고 기함으로부터 일점사 명령을 받지 않았을 경우, 각 함선마다 가장 가까운 적을 자동 공격
            {
                if (SearchTime <= 0.5f)
                    SearchTime += Time.deltaTime;

                if (SearchTime >= 0.5f)
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
                        gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = TargetShip;

                        if (TargetMarkTime == 0) //지정된 대상을 향해 마크 발생
                        {
                            TargetMarkTime += Time.deltaTime;
                            //SoundManager.instance.SFXPlay11("Sound", Beep1);
                        }
                    }
                    else
                    {
                        TargetShip = null;
                        TargetOnline = false;
                        gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    }
                }
            }
            else //대상 지정 공격 명령 이후에 대상이 격침되었을 경우, 자동으로 해제
            {
                if (TargetShip == null)
                {
                    gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    OrderTarget = false;
                }
            }

            if (RangeAttack == true) //사격 가능한 사거리를 벗어나면 사격 중지
            {
                //초당 무기 발사
                if (AttackTime >= FireRate + RandomFire)
                {
                    AttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (CannonType == 1)
                        StartCoroutine(FireNarihaQuantum1());
                    else if (CannonType == 2)
                        StartCoroutine(FireNarihaOverJump1());
                    else if (CannonType == 3)
                        StartCoroutine(FireNarihaSingleMissile1());
                    else if (CannonType == 4)
                        StartCoroutine(FireNarihaMultiMissile1());
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;
            OrderTarget = false;
            gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
        }
    }

    //나리하 사일런스 시스트 양자 도약 가속포(멀티발 주포)
    IEnumerator FireNarihaQuantum1()
    {
        NarihaFireEffect.SetActive(true);


        for (int i = 0; i <= DamageCount; i++)
        {
            if (Flagship == true)
            {
                UniverseSoundManager.instance.UniverseSoundPlayMaster("Nariha Silent Sist Fire Sound", SilentSistFireSound, this.transform.position);
                SilenceSistCannonSystem1Flagship();
                if (i < DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Flagship)", 1);
                    yield return new WaitForSeconds(0.2f);
                    anim.SetFloat("Fire Cannon(Flagship)", 0);
                    yield return new WaitForSeconds(0.05f);
                }
                else if (i == DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Flagship)", 2);
                    yield return new WaitForSeconds(1.16f);
                    anim.SetFloat("Fire Cannon(Flagship)", 0);
                }
            }
            else if (Flagship == false)
            {
                UniverseSoundManager.instance.UniverseSoundPlayMaster("Nariha Silent Sist Fire Sound", SilentSistFireSound, this.transform.position);
                SilenceSistCannonSystem1Formation();
                if (i < DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Formation)", 1);
                    yield return new WaitForSeconds(0.2f);
                    anim.SetFloat("Fire Cannon(Formation)", 0);
                    yield return new WaitForSeconds(0.05f);
                }
                else if (i == DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Formation)", 2);
                    yield return new WaitForSeconds(1.16f);
                    anim.SetFloat("Fire Cannon(Formation)", 0);
                }
            }
        }

        yield return new WaitForSeconds(0.25f);
        NarihaFireEffect.SetActive(false);
        yield return new WaitForSeconds(RandomFire);
    }

    //나리하 초과점프 가속포(단발 주포)
    IEnumerator FireNarihaOverJump1()
    {
        NarihaFireEffect2.SetActive(true);
        if (Flagship == true)
            OverJump1CannonSystem1Flagship();
        else
            OverJump1CannonSystem1Formation();

        anim.SetFloat("Fire Cannon(Flagship)", 2);
        yield return new WaitForSeconds(1.16f);
        anim.SetFloat("Fire Cannon(Flagship)", 0);
        NarihaFireEffect2.SetActive(false);
        yield return new WaitForSeconds(RandomFire);
    }

    //나리하 단일 미사일 발사 연출
    IEnumerator FireNarihaSingleMissile1()
    {
        if (Flagship == true)
            SingleMissileSystem1Flagship();
        else
            SingleMissileSystem1Formation();
        StartCoroutine(FireNarihaSingleMissile1Animation());
        MissileFireEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    //나리하 멀티 미사일 발사 연출
    IEnumerator FireNarihaMultiMissile1()
    {
        for (int i = 0; i < DamageCount; i++)
        {
            if (Flagship == true)
                MultiMissile1System1Flagship();
            else
                MultiMissile1System1Formation();
            StartCoroutine(FireNarihaSingleMissile1Animation());
            Instantiate(MissileFireEffect, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    IEnumerator FireNarihaSingleMissile1Animation()
    {
        if (Flagship == true)
        {
            anim.SetFloat("Fire Cannon(Flagship)", 3);
            yield return new WaitForSeconds(1);
            anim.SetFloat("Fire Cannon(Flagship)", 0);
        }
        else if (Flagship == false)
        {
            anim.SetFloat("Fire Cannon(Formation)", 3);
            yield return new WaitForSeconds(1);
            anim.SetFloat("Fire Cannon(Formation)", 0);
        }
    }

    //나리하 사일런스 시스트 양자 점프 가속포(기함) 데미지 전달체 생성
    void SilenceSistCannonSystem1Flagship()
    {
        NarihaArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaSilenceSistArtillery1Flagship");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSilenceSistArtillery1ExplosionFlagship", "NarihaSilenceSistArtillery1FlagshipDelete", "NarihaSilenceSistArtillery1ExplosionFlagshipDelete");
    }
    //나리하 사일런스 시스트 양자 점프 가속포(편대함) 데미지 전달체 생성
    void SilenceSistCannonSystem1Formation()
    {
        NarihaArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaSilenceSistArtillery1Formation");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSilenceSistArtillery1ExplosionFormation", "NarihaSilenceSistArtillery1FormationDelete", "NarihaSilenceSistArtillery1ExplosionFormationDelete");
    }

    //나리하 초과 점프 가속포(기함) 데미지 전달체 생성
    void OverJump1CannonSystem1Flagship()
    {
        NarihaOverJumpArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaOverJumpArtillery1Flagship");
        NarihaOverJumpArtillery1.transform.position = TurretLocation.transform.position;
        NarihaOverJumpArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaOverJumpArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaOverJumpArtillery1ExplosionFlagship", "NarihaOverJumpArtillery1FlagshipDelete", "NarihaOverJumpArtillery1ExplosionFlagshipDelete");
    }
    //나리하 초과 점프 가속포(편대함) 데미지 전달체 생성
    void OverJump1CannonSystem1Formation()
    {
        NarihaOverJumpArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaOverJumpArtillery1Formation");
        NarihaOverJumpArtillery1.transform.position = TurretLocation.transform.position;
        NarihaOverJumpArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaOverJumpArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaOverJumpArtillery1ExplosionFormation", "NarihaOverJumpArtillery1FormationDelete", "NarihaOverJumpArtillery1ExplosionFormationDelete");
    }

    //나리하 단일 미사일(기함) 데미지 전달체 생성
    void SingleMissileSystem1Flagship()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaSingleMissile1Flagship");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSingleMissile1ExplosionFlagship");
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetRotation();
    }
    //나리하 단일 미사일(편대함) 데미지 전달체 생성
    void SingleMissileSystem1Formation()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaSingleMissile1Formation");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSingleMissile1ExplosionFormation");
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetRotation();
    }

    //나리하 멀티미사일(기함) 데미지 전달체 생성
    void MultiMissile1System1Flagship()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaMultiMissile1Flagship");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        int RandomRadius = Random.Range(-40, 40);
        NarihaMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z - 90 + RandomRadius);
        NarihaMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaMultiMissile1ExplosionFlagship");
    }
    //나리하 멀티미사일(편대함) 데미지 전달체 생성
    void MultiMissile1System1Formation()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaMultiMissile1Formation");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        int RandomRadius = Random.Range(-40, 40);
        NarihaMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z - 90 + RandomRadius);
        NarihaMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaMultiMissile1ExplosionFormation");
    }
}