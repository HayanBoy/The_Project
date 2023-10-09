using System.Collections;
using UnityEngine;

public class EnemyAttackSystemInBackground : MonoBehaviour
{
    Animator animator;

    public int NationType; //종족. 1 = 슬로리어스, 2 = 칸타크리
    public int CannonType;

    public int AmmoDamage; //함포 데미지
    public float FireRate; //시간당 1회 공격
    private float RandomFire; //연사속도를 랜덤화
    private float AttackTime; //FireRate 쿨타임 전용
    public int DamageCount; //데미지를 한번에 몇 번 주는지에 대한 여부

    public float AttackEyeRange; //공격가능한 사거리

    public GameObject TargetShip; //목표 조준 대상
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public Transform TurretLocation; //함포 위치

    public bool TargetOnline; //목표가 지정되었는지에 대한 스위치
    private float TargetMarkTime; //대상을 향해 마크 애니메이션 발동을 딱 한번만 발생하도록 조취

    public GameObject SloriusEnergyRay1Prefab; //슬로리어스 익쉬이우 슈루스 아광속 강습 광선 애니메이션용 프리팹
    GameObject SloriusEnergyRay1; //슬로리어스 익쉬이우 슈루스 아광속 강습 광선
    GameObject KantakriMissile1;

    void OnEnable()
    {
        AttackTime = FireRate / 2;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //AttackTime 숫자를 FireRate를 넘지 않는 조건에서 실시간으로 증가
        if (AttackTime <= FireRate + RandomFire && TargetShip != null)
            AttackTime += Time.deltaTime;

        if (TargetShip != null) //타겟을 잡은 경우에만 포대 작동
        {
            Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //포대 회전
            transform.right = Vector3.Lerp(transform.right, dir, 6 * Time.deltaTime);
        }

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
        }
        if (TargetShip != null && TargetShip.layer != 19)
            TargetShip = null;

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
            else if (NationType == 2 && CannonType == 3)
                KantakriWeaponSystem3();
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

    IEnumerator FireSloriusEnergyRayAnimation()
    {
        animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", false);
    }

    //슬로리어스 익쉬이우 슈루스 아광속 강습 광선 데미지 전달체 생성
    void SloriusWeaponSystem1()
    {
        SloriusEnergyRay1 = ShipAmmoObjectPoolInBackground.instance.Loader("SloriusEnergyRay1");
        SloriusEnergyRay1.transform.position = TurretLocation.transform.position;
        SloriusEnergyRay1.transform.rotation = TurretLocation.transform.rotation;
        SloriusEnergyRay1.GetComponent<CannonMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "SloriusEnergyRay1Explosion");
    }

    void KantakriWeaponSystem3()
    {
        KantakriMissile1 = ShipAmmoObjectPoolInBackground.instance.Loader("KantakriMissile1");
        KantakriMissile1.transform.position = TurretLocation.transform.position;
        KantakriMissile1.transform.rotation = TurretLocation.transform.rotation;
        KantakriMissile1.GetComponent<NarihaMissileMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "KantakriMissile1Explosion");
    }
}