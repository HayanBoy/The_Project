using System.Collections;
using UnityEngine;

public class NarihaTurretAttackSystemInBackground : MonoBehaviour
{
    Animator anim;

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

    public GameObject NarihaFireEffect; //나리하 사일런스 시스트 양자 점프 가속포
    public GameObject MissileFireEffect; //나리하 미사일 발사 연출
    GameObject NarihaArtillery1;
    GameObject NarihaMissile1;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackTime = FireRate / 2;
    }

    void Update()
    {
        //AttackTime 숫자를 FireRate를 넘지 않는 조건에서 실시간으로 증가
        if (AttackTime <= FireRate + RandomFire && TargetShip != null)
            AttackTime += Time.deltaTime;

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

            Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //포대 회전
            transform.right = Vector3.Lerp(transform.right, dir, 10 * Time.deltaTime);
        }
        if (TargetShip != null && TargetShip.layer != 20)
            TargetShip = null;

        //초당 무기 발사
        if (AttackTime >= FireRate + RandomFire)
        {
            AttackTime = 0;
            RandomFire = Random.Range(0, 0.15f);

            if (CannonType == 1) //나리하 사일런스 시스트 양자 점프 가속포
                StartCoroutine(FireNarihaQuantum1());
            else if (CannonType == 3)
                StartCoroutine(FireNarihaMissile1());
        }
    }

    //나리하 사일런스 시스트 양자 점프 가속포 연출
    IEnumerator FireNarihaQuantum1()
    {
        NarihaFireEffect.SetActive(true);

        for (int i = 0; i <= DamageCount; i++)
        {
            WeaponSystem1();
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

        yield return new WaitForSeconds(0.25f);
        NarihaFireEffect.SetActive(false);

        yield return new WaitForSeconds(RandomFire);
    }

    //나리하 미사일 발사 연출
    IEnumerator FireNarihaMissile1()
    {
        WeaponSystem2();
        MissileFireEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    //나리하 사일런스 시스트 양자 점프 가속포(편대 함선용) 데미지 전달체 생성
    void WeaponSystem1()
    {
        NarihaArtillery1 = ShipAmmoObjectPoolInBackground.instance.Loader("NarihaArtillery1");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "NarihaArtillery1Explosion");
    }

    //나리하 미사일(편대 함선용) 데미지 전달체 생성
    void WeaponSystem2()
    {
        NarihaMissile1 = ShipAmmoObjectPoolInBackground.instance.Loader("NarihaMissile1");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "NarihaMissile1Explosion");
        NarihaMissile1.GetComponent<NarihaMissileMovementInBackground>().SetRotation();
    }
}