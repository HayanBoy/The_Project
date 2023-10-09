using System.Collections;
using UnityEngine;

public class NarihaShieldSystem : MonoBehaviour
{
    [Header("방어 정보")]
    public int ShieldType; //방어 유형. 1 = 광역 방어, 2 = 미사일 요격, 3 = 레이져 방어, 4 = 성간 방어
    public float RangeOfShield; //방어 범위
    public float EyeRange; //근처에 적이 감지되면 자동 방어막 시전
    [SerializeField] LayerMask layerMask; //어떤 목표 레이어를 특정할 것인가
    private float SearchTime = 2; //함포가 대상을 검색하는 시간
    public float ShieldTime; //방어막 가동 시간, 임시
    private bool ShieldCoolTimeOn; //방어막 쿨타임, 임시
    public float ShieldCoolTime; //방어막 쿨타임, 임시

    [Header("방어 스위치")]
    public bool Flagship = false; //기함 여부
    public bool canDefence = false; //방어 가능 여부
    public bool DefenceOnline = false; //방어 명령

    [Header("보호막 내구도")]
    public int ShieldHitPoint; //방어막 체력
    private int ShieldHitPoint2; //방어막 체력(인게임)
    public int ShieldArmorSlorius; //슬로리어스 공격에 대한 방어막 저항력
    public int ShieldArmorKantakri; //칸타크리 공격에 대한 방어막 저항력

    [Header("요격 능력")]
    public int ShieldEnergy; //방어막 에너지. 에너지가 없으면 방어막을 사용할 수 없다
    public int ShieldEnergy2; //방어막 에너지(인게임)
    public int AmountOfInterception; //한번에 방어 가능한 갯수

    [Header("이펙트")]
    public GameObject EnergyBallPrefab; //에너지 볼 프리팹
    public GameObject ShieldPrefab; //방어막 프리팹
    public GameObject ShieldRipplePrefab; //방어막 타격 이펙트

    [Header("콜라이더")]
    public GameObject Collider;
    public GameObject PartCollider1;
    public GameObject PartCollider2;
    public GameObject PartCollider3;
    public GameObject PartCollider4;
    public GameObject PartCollider5;
    public GameObject PartCollider6;

    Coroutine turnOffShield;
    private float ShieldStemp;
    private float ShieldStemp2;

    void Start()
    {
        ShieldPrefab.transform.localScale = new Vector3(RangeOfShield * 100, RangeOfShield * 100, RangeOfShield * 100); //업그레이드시, 해당 메서드로 이전될 예정
    }

    public void ShieldDefenceEffect(Vector2 Transform)
    {
        GameObject Ripple = Instantiate(ShieldRipplePrefab, transform.position, transform.rotation);
        Ripple.GetComponent<ShieldRipples>().ShieldDefenceEffect(Transform);
        Ripple.transform.localScale = new Vector3(RangeOfShield * 100, RangeOfShield * 100, RangeOfShield * 100);
    }

    void Update()
    {
        if (canDefence == true && ShieldCoolTimeOn == false)
        {
            if (SearchTime <= 3)
                SearchTime += Time.deltaTime;

            if (SearchTime >= 3)
            {
                SearchTime = 0;

                Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, EyeRange, layerMask); //실시간으로 AttackEyeRange 범위내에서 가장 가까운 대상을 검색
                float shortestDistance = Mathf.Infinity;
                Collider2D nearestTarget = null;

                if (TargetShips != null)
                {
                    float DistanceToMonsters = Vector3.Distance(transform.position, TargetShips.transform.position);

                    if (DistanceToMonsters < shortestDistance) //가장 가까운 타겟으로 변경
                    {
                        shortestDistance = DistanceToMonsters;
                        nearestTarget = TargetShips;
                    }
                }
                if (nearestTarget != null && shortestDistance <= EyeRange) //지정된 타겟을 TargetShip리스트에 올리기
                {
                    DefenceOnline = true;
                }
                else
                {
                    DefenceOnline = false;
                    if (ShieldStemp == 0)
                    {
                        ShieldStemp += Time.deltaTime;
                        ShieldStemp2 = 0;
                        turnOffShield = StartCoroutine(TurnOffShield());
                    }
                }
            }

            if (DefenceOnline == true)
            {
                if (ShieldType == 1)
                {
                    if (ShieldStemp2 == 0)
                    {
                        ShieldStemp2 += Time.deltaTime;
                        ShieldStemp = 0;
                        ShieldPrefab.SetActive(true);
                        if (turnOffShield != null)
                            StopCoroutine(turnOffShield);
                        ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", false);
                        EnergyBallPrefab.GetComponent<Animator>().SetBool("Activated, Nariha Shield Ship ball", true);

                        Collider.layer = 0;
                        this.gameObject.layer = 0;
                        PartCollider1.layer = 0;
                        PartCollider2.layer = 0;
                        PartCollider3.layer = 0;
                        PartCollider4.layer = 0;
                        PartCollider5.layer = 0;
                        PartCollider6.layer = 0;

                        ShieldTime = 0;
                        ShieldCoolTime = 0;
                    }
                }
            }
        }
        else
        {
            gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
        }

        if (DefenceOnline == true)
        {
            ShieldTime += Time.deltaTime;

            if (ShieldTime >= 20)
            {
                DefenceOnline = false;
                ShieldCoolTimeOn = true;
                ShieldTime = 0;
                ShieldStemp = 0;
                ShieldStemp2 = 0;
                turnOffShield = StartCoroutine(TurnOffShield());
            }
        }
        if (ShieldCoolTimeOn == true)
        {
            ShieldCoolTime += Time.deltaTime;

            if (ShieldCoolTime >= 20)
            {
                ShieldCoolTimeOn = false;
                ShieldCoolTime = 0;
            }
        }
    }

    //전투가 끝나면 자동으로 방어막 종료
    IEnumerator TurnOffShield()
    {
        if (ShieldPrefab.activeSelf == true)
        {
            yield return new WaitForSeconds(2);
            ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", true);
            EnergyBallPrefab.GetComponent<Animator>().SetBool("Activated, Nariha Shield Ship ball", false);
            yield return new WaitForSeconds(3);
            ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", false);
            ShieldPrefab.SetActive(false);
        }

        Collider.layer = 6;
        this.gameObject.layer = 6;
        PartCollider1.layer = 6;
        PartCollider2.layer = 6;
        PartCollider3.layer = 6;
        PartCollider4.layer = 6;
        PartCollider5.layer = 6;
        PartCollider6.layer = 6;
    }
}