using UnityEngine;

public class FlagshipAttackSkill : MonoBehaviour
{
    [Header("함포 공격 스위치")]
    public int SkillType; //스킬 공격 종류. 1 = 기함 단독 공격, 2 = 함대 공격, 3 = 기함 지원
    public int SkillNumber; //SkillType의 각 3가지에 대한 스킬 번호
    public int SecondSkillType; //스킬 공격 종류. 1 = 기함 단독 공격, 2 = 함대 공격, 3 = 기함 지원
    public int SecondSkillNumber; //SkillType의 각 3가지에 대한 스킬 번호
    public int ThirdSkillType; //스킬 공격 종류. 1 = 기함 단독 공격, 2 = 함대 공격, 3 = 기함 지원
    public int ThirdSkillNumber; //SkillType의 각 3가지에 대한 스킬 번호

    public bool canAttack = false; //공격 가능 여부
    public bool RangeAttack = false; //사거리내에 있을 때에만 공격
    public bool OrderTarget = false; //기함이 소속함선들에게 지정된 대상 명령을 받았을 경우에만 발동

    [Header("목표 및 함포 위치")]
    public GameObject TargetShip; //목표 조준 대상
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public bool TargetOnline; //목표가 지정되었는지에 대한 스위치
    private float TargetMarkTime; //대상을 향해 마크 애니메이션 발동을 딱 한번만 발생하도록 조취

    [Header("함포 사거리")]
    public float AttackEyeRange; //공격가능한 사거리

    [Header("함포 공격 정보")]
    public int AmmoDamage; //함포 데미지
    public float FireRate; //시간당 1회 공격
    public float SecondFireRate;
    public float ThirdFireRate;
    private float RandomFire; //연사속도를 랜덤화
    private float AttackTime; //FireRate 쿨타임 전용
    private float SecondAttackTime;
    private float ThirdAttackTime;
    public int DamageCount; //데미지를 한번에 몇 번 주는지에 대한 여부

    [Header("기함 공격 스킬 데미지 정보")]
    public int SikroClassCruiseMissileDamage;

    [Header("함대 공격 스킬 데미지 정보")]
    public int Cysiro47PatriotMissileDamage;

    private void Start()
    {
        AttackTime = FireRate * 0.8f;
        SecondAttackTime = SecondFireRate * 0.8f;
        ThirdAttackTime = ThirdFireRate * 0.8f;

        SikroClassCruiseMissileDamage = UpgradeDataSystem.instance.SikroClassCruiseMissileDamage;
        Cysiro47PatriotMissileDamage = UpgradeDataSystem.instance.Cysiro47PatriotMissileDamage;
    }

    void Update()
    {
        //사격이 가능할 경우, 사격 개시
        if (canAttack == true)
        {
            //AttackTime 숫자를 FireRate를 넘지 않는 조건에서 실시간으로 증가
            if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && AttackTime <= FireRate + RandomFire && TargetShip != null)
                AttackTime += Time.deltaTime;
            if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && SecondAttackTime <= SecondFireRate + RandomFire && TargetShip != null)
                SecondAttackTime += Time.deltaTime;
            if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && ThirdAttackTime <= ThirdFireRate + RandomFire && TargetShip != null)
                ThirdAttackTime += Time.deltaTime;

            if (OrderTarget == false) //기함이 존재할 경우, 그리고 기함으로부터 일점사 명령을 받지 않았을 경우, 각 함선마다 가장 가까운 적을 자동 공격
            {
                Collider2D[] TargetShips = Physics2D.OverlapCircleAll(transform.position, AttackEyeRange, layerMask); //실시간으로 AttackEyeRange 범위내에서 가장 가까운 대상을 검색
                float shortestDistance = Mathf.Infinity;
                Collider2D nearestTarget = null;

                foreach (Collider2D TargetShip in TargetShips)
                {
                    float DistanceToMonsters = Vector3.Distance(transform.position, TargetShip.transform.position);

                    if (DistanceToMonsters < shortestDistance) //가장 가까운 타겟으로 변경
                    {
                        shortestDistance = DistanceToMonsters;
                        nearestTarget = TargetShip;
                        TargetMarkTime = 0;
                    }
                }
                if (nearestTarget != null && shortestDistance <= AttackEyeRange) //지정된 타겟을 TargetShip리스트에 올리기
                {
                    TargetShip = nearestTarget.gameObject;
                    TargetOnline = true;

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
                }
            }

            if (RangeAttack == true) //사격 가능한 사거리를 벗어나면 사격 중지
            {
                //초당 무기 발사
                if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && AttackTime >= FireRate + RandomFire)
                {
                    AttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (SkillType == 1 && SkillNumber == 1)
                        NuclearMissileAttack();
                    if (SkillType == 2 && SkillNumber == 1)
                        ClusterMissileAttack();
                }
                if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && SecondAttackTime >= SecondFireRate + RandomFire)
                {
                    SecondAttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (SecondSkillType == 1 && SecondSkillNumber == 1)
                        NuclearMissileAttack();
                    if (SecondSkillType == 2 && SecondSkillNumber == 1)
                        ClusterMissileAttack();
                }
                if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && ThirdAttackTime >= ThirdFireRate + RandomFire)
                {
                    ThirdAttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (ThirdSkillType == 1 && ThirdSkillNumber == 1)
                        NuclearMissileAttack();
                    if (ThirdSkillType == 2 && ThirdSkillNumber == 1)
                        ClusterMissileAttack();
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;
        }
    }

    //기함 공격 스킬
    public void NuclearMissileAttack()
    {
        GetComponent<ShipAttackObject>().NuclearMissile1Fire(TargetShip, SikroClassCruiseMissileDamage, "NarihaNuclearMissile1Flagship", "NarihaNuclearMissile1ExplosionFlagship");
    }

    //함대 공격 스킬
    public void ClusterMissileAttack()
    {
        float RandomFire = Random.Range(0, 0.5f);
        StartCoroutine(GetComponent<ShipAttackObject>().TargetMissile1Fire(TargetShip, RandomFire, AmmoDamage, DamageCount, "NarihaClusterMissile1Flagship", "NarihaMultiMissile1ExplosionFlagship"));

        for (int i = 0; i <= GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
            RandomFire = Random.Range(0, 1f);
            ShipAttackObject ShipAttackObject = GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipAttackObject>();
            StartCoroutine(ShipAttackObject.TargetMissile1Fire(TargetShip, RandomFire, Cysiro47PatriotMissileDamage, DamageCount, "NarihaClusterMissile1Formation", "NarihaMultiMissile1ExplosionFormation"));
        }
    }
}