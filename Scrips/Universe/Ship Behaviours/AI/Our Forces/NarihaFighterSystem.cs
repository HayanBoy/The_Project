using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarihaFighterSystem : MonoBehaviour
{
    [Header("함재기 리스트")]
    public List<GameObject> EngagedFighterList = new List<GameObject>(); //사출된 함재기 리스트
    public List<GameObject> FighterList = new List<GameObject>(); //함재기 리스트

    [Header("함재기 스위치")]
    public bool Flagship = false; //기함 여부
    public bool canAttack = false; //공격 가능 여부
    public bool FighterEngagement; //함재기가 사출된 상태인지에 대한 여부
    public bool OrderTarget = false; //기함이 소속함선들에게 지정된 대상 명령을 받았을 경우에만 발동
    public bool FighterUse = false; //요격기 탑재 여부
    public bool BomerUse = false; //요격기 탑재 여부

    [Header("함재기 공격 정보")]
    public float FighterRange; //함재기 출격 범위. 적이 이 사거리내에 있을 때에만 함재기를 출격한다. 이 이상을 벗어나면 함재기가 자동으로 귀환한다.
    public int FighterDamage; //요격기 데미지
    public int BomberDamage; //폭격기 데미지
    public float TimeWaveAircraftSortie; //함재기 편대가 한번 사출되고 나서 다음 편대가 사출하기까지 걸리는 시간
    public float RangeOfAircraftAtOneTime; //편대 하나 출격마다 함재기 출격 간격 시간
    private float AttackStemp; //Update문에서 한번만 실행하도록 조취
    private float WarpStemp;
    private float EmergencyStemp;
    Coroutine launchFighter;

    [Header("업그레이드 정보")]
    public int FighterDamageUpgrade;
    public int BomerDamageUpgrade;

    [Header("함재기수 정보")]
    public int AmountOfFormation; //함재기 편대수
    public int AircraftsPerFormation; //편대당 함재기 수
    public int FighterAmount; //요격기 수
    public int BomerAmount; //요격기 수

    [Header("함재기 내구도 정보")]
    public int FighterHitPoint; //요격기 체력
    public int BomerHitPoint; //요격기 체력

    [Header("목표")]
    public GameObject TargetShip; //목표 조준 대상
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    public bool TargetOnline; //목표가 지정되었는지에 대한 스위치
    private float TargetMarkTime; //대상을 향해 마크 애니메이션 발동을 딱 한번만 발생하도록 조취
    private float SearchTime = 2; //함포가 대상을 검색하는 시간

    [Header("함재기 프리팹")]
    GameObject FighterPrefab;
    GameObject BomerPrefab;

    [Header("함재기 출격 사운드")]
    public AudioClip AircraftSortieSound;

    void Start()
    {
        if (FighterUse == true)
        {
            for (int i = 0; i < FighterAmount; i++)
            {
                FighterList.Add(ShipAmmoObjectPool.instance.NarihaFighter1Prefab);
            }
        }
        if (BomerUse == true)
        {
            for (int i = 0; i < BomerAmount; i++)
            {
                FighterList.Add(ShipAmmoObjectPool.instance.NarihaBomer1Prefab);
            }
        }
    }

    //업그레이드 적용
    public void UpgradePatch()
    {
        BomberDamage = BomerDamageUpgrade;
    }

    void Update()
    {
        if (canAttack == true)
        {
            if (OrderTarget == false) //기함이 존재할 경우, 그리고 기함으로부터 일점사 명령을 받지 않았을 경우, 각 함선마다 가장 가까운 적을 자동 공격
            {
                if (SearchTime <= 3)
                    SearchTime += Time.deltaTime;

                if (SearchTime >= 3)
                {
                    SearchTime = 0;

                    Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, FighterRange, layerMask); //실시간으로 AttackEyeRange 범위내에서 가장 가까운 대상을 검색
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
                    if (nearestTarget != null && shortestDistance <= FighterRange) //지정된 타겟을 TargetShip리스트에 올리기
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
                        FighterEngagement = false;
                        AttackStemp = 0;
                        gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    }
                }
            }
            else
            {
                if (TargetShip == null)
                {
                    gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    OrderTarget = false;
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

        //목표 함선이 포착되었을 경우, 폭격기 사출
        if (TargetOnline == true)
        {
            if (BomerUse == true)
            {
                if (AttackStemp == 0)
                {
                    AttackStemp += Time.deltaTime;
                    WarpStemp = 0;
                    FighterEngagement = true;
                    launchFighter = StartCoroutine(LaunchFighter());
                }
            }
        }

        //공격이 중단될 경우, 함재기에게 귀환 신호를 보내기 위한 FighterEngagement를 true로 전환한다.
        if (canAttack == false || TargetOnline == false)
        {
            if (launchFighter != null)
                StopCoroutine(launchFighter);
            FighterEngagement = false;
        }
         //워프에 돌입했을 경우, 함재기에게 일정 시간 뒤에 워프로 뒤따라 오도록 조취
        if (gameObject.transform.parent.GetComponent<MoveVelocity>().WarpDriveActive == true)
        {
            if (WarpStemp == 0)
            {
                WarpStemp += Time.deltaTime;
                StartCoroutine(AfterWarp());
            }
        }
        if (gameObject.transform.parent.GetComponent<MoveVelocity>().EmergencyWarp == true)
        {
            if (EmergencyStemp == 0)
            {
                EmergencyStemp += Time.deltaTime;
                StartCoroutine(TurnOff());
            }
        }
    }

    //모함이 워프하면 함재기들에게 워프 명령
    IEnumerator AfterWarp()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].GetComponent<NarihaFighterEngagement>().WarpDriveActive = true;
        }
    }

    //모함이 기함 격침에 의해 도주하면 함재기 삭제 명령
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3);

        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].GetComponent<NarihaFighterEngagement>().Emagancy = true;
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].gameObject.SetActive(false);
        }
    }

    //함재기 발진
    IEnumerator LaunchFighter()
    {
        //총 폭격기 수에서 이미 작전 중인 함재기수를 빼서 출격할 수 있는 함재기를 계산한다.
        int InsideFighters = 0;
        InsideFighters = BomerAmount - EngagedFighterList.Count;

        for (int j = 0; j < AmountOfFormation; j++)
        {
            for (int y = 0; y < AircraftsPerFormation; y++)
            {
                InsideFighters--;
                if (InsideFighters <= 0)
                    break;
                if (EngagedFighterList.Count == FighterList.Count)
                    break;
                if (canAttack == false)
                    break;

                BomerPrefab = ShipAmmoObjectPool.instance.Loader("NarihaBomer1");
                BomerPrefab.transform.position = transform.position;
                BomerPrefab.transform.rotation = transform.rotation;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().MotherCarrier = this.gameObject;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().MyFlagship = this.transform.parent.gameObject.GetComponent<MoveVelocity>().MyFlagship;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().GetInformation(BomerHitPoint, BomberDamage);
                BomerPrefab.GetComponent<NarihaFighterEngagement>().Ingagement = true;
                EngagedFighterList.Add(BomerPrefab);

                yield return new WaitForSeconds(RangeOfAircraftAtOneTime);
            }

            if (EngagedFighterList.Count == FighterList.Count)
                break;
            if (canAttack == false)
                break;

            yield return new WaitForSeconds(TimeWaveAircraftSortie);
        }
    }
}