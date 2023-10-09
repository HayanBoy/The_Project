using System.Collections;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    [Header("미사일 정보")]
    public int TargetType;
    public int MissileType; //미사일 유형. 1 = 일반 미사일, 2 = 클러스터 미사일
    public float MissileSpeed; //미사일 속도
    private float MissileSpeedInGame;
    public float RotationSpeed; //미사일 회전 속도
    private float RotationSpeedInGame;

    [Header("미사일 유형")]
    public bool ExplosionType; //폭발 타입인지에 대한 여부
    public bool isParticle; //파티클로 이루어졌는지 여부
    public int ParticleNumber; //파티클이 있을 경우에만 사용

    private string ExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    private int AmmoDamage;
    private float ApproachDistance;
    private float StrikeDistance;

    private bool SpeedAeccelate;
    private bool SpeedDeccelate;
    private bool LockOn = false; //대상을 추적하는 상태
    private bool ShieldImpact = false; //방패함선의 방어막에 충돌한 상태

    [Header("대상")]
    public GameObject TargetLocation; //목표 대상
    public Vector3 LastTarget;

    [Header("접근효과")]
    public GameObject RushEffect; //대상으로부터 정해진 거리만큼 근접했을 때 발현되는 이펙트
    private float EffectStemp;

    [Header("착탄효과")]
    public SpriteRenderer Image; //미사일 착탄시, 미사일 스프라이트를 끄기
    public ParticleSystem Engine1;
    public ParticleSystem Engine2;
    public ParticleSystem Engine3;
    public ParticleSystem Ammo1;
    public ParticleSystem Ammo2;
    public ParticleSystem Ammo3;
    public ParticleSystem Tail;

    private float Temp;

    private void OnEnable()
    {
        if (Image != null)
            Image.enabled = true;

        if (MissileType == 1) //일반 미사일
        {
            ApproachDistance = 5;
            StrikeDistance = 0.5f;
            LockOn = true;
        }
        if (MissileType == 2) //멀티 및 클러스터 미사일
        {
            ApproachDistance = 5;
            StrikeDistance = 0.5f;
            LockOn = true;
            StartCoroutine(ClusterMissileStart());
        }
        if (MissileType == 3) //핵 미사일
        {
            ApproachDistance = 5;
            StrikeDistance = 1;
            StartCoroutine(NucelarMissileStart());
        }

        if (Engine1 != null) //미사일 엔진 이펙트 사용중일 때 해당
        {
            var Engine1Emission = Engine1.emission;
            var Engine2Emission = Engine2.emission;
            var Engine3Emission = Engine3.emission;
            Engine1Emission.rateOverTime = 10;
            Engine2Emission.rateOverTime = 10;
            Engine3Emission.rateOverTime = 15;
        }

        if (Ammo1 != null)
        {
            if (ParticleNumber == 1) //칸타크리 게르피르-치타리오 23
            {
                ApproachDistance = 10;
                StrikeDistance = 0.5f;
                LockOn = true;
                var Fire3Emission = Engine1.emission;
                var Fire4Emission = Engine2.emission;
                var Fire5Emission = Engine3.emission;
                var Fire6Emission = Ammo1.emission;
                var Fire7Emission = Ammo2.emission;
                var Fire8Emission = Ammo3.emission;
                Fire3Emission.rateOverTime = 10;
                Fire4Emission.rateOverTime = 10;
                Fire5Emission.rateOverTime = 15;
                Fire6Emission.rateOverTime = 20;
                Fire7Emission.rateOverTime = 20;
                Fire8Emission.rateOverTime = 20;
                StartCoroutine(KantakriMultiHit1Start());
            }
        }
    }

    private void OnDisable()
    {
        Temp = 0;
        EffectStemp = 0;
        AmmoDamage = 0;
        ExplosionName = null;
        TargetLocation = null;
        if (Image != null)
            Image.enabled = true;
        LockOn = false;
        ShieldImpact = false;
        MissileSpeedInGame = MissileSpeed;
        RotationSpeedInGame = RotationSpeed;

        if (Tail != null)
        {
            var TailEmission = Tail.emission;
            TailEmission.rateOverTime = 10;
        }
    }

    public void SetDamage(int Damage, GameObject Target, string Name)
    {
        AmmoDamage = Damage;
        TargetLocation = Target;
        ExplosionName = Name;
    }

    void Update()
    {
        if (TargetLocation != null)
        {
            LastTarget = TargetLocation.transform.position;
            float DisctanceFromEnemy = Vector2.Distance(TargetLocation.transform.position, transform.position);
            if (SpeedDeccelate == true)
            {
                if (MissileSpeedInGame >= MissileSpeed)
                    MissileSpeedInGame -= 20 * Time.deltaTime;
                if (RotationSpeedInGame <= RotationSpeed)
                    RotationSpeedInGame += 20 * Time.deltaTime;
            }
            if (SpeedAeccelate == true)
            {
                if (ParticleNumber == 1)
                {
                    if (MissileSpeedInGame <= MissileSpeed * 2.5f)
                        MissileSpeedInGame += 10 * Time.deltaTime;
                }
            }

            if (LockOn == true)
            {
                if (DisctanceFromEnemy > ApproachDistance)
                {
                    transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                    Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * Time.deltaTime);
                }
                if (DisctanceFromEnemy <= ApproachDistance)
                {
                    if (ParticleNumber == 1)
                    {
                        if (EffectStemp == 0)
                        {
                            EffectStemp += Time.deltaTime;
                            RushEffect.SetActive(true);
                            SpeedAeccelate = true; //근접시, 이펙트 발생과 함께 가속
                        }
                        transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                        Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 100 * Time.deltaTime);
                    }
                    else
                    {
                        transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                        Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 50 * Time.deltaTime);
                    }
                }
                if (DisctanceFromEnemy <= StrikeDistance) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
                {
                    transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                    Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 100 * Time.deltaTime);

                    if (Temp == 0)
                    {
                        if (TargetLocation != null)
                        {
                            Temp += Time.deltaTime;
                            if (ParticleNumber == 1)
                                StartCoroutine(MultiHitStart(AmmoDamage));
                            else
                            {
                                Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                                Explosion.transform.position = transform.position;
                                Explosion.transform.rotation = transform.rotation;
                                Explosion.GetComponent<CannonExplosion>().Damage = AmmoDamage;
                            }
                            if (Image != null)
                                Image.enabled = false;
                            StartCoroutine(ImageDown());
                        }
                    }
                }
            }
            else
                transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
        }
        else //타겟이 도중에 파괴되어도 해당 사라진 좌표를 향해 돌진
        {
            float DisctanceFromEnemy = Vector2.Distance(LastTarget, transform.position);
            if (DisctanceFromEnemy > ApproachDistance)
            {
                transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                Vector3 dir = (LastTarget - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * Time.deltaTime);
            }
            if (DisctanceFromEnemy <= ApproachDistance)
            {
                if (ParticleNumber == 1)
                {
                    if (EffectStemp == 0)
                    {
                        EffectStemp += Time.deltaTime;
                        RushEffect.SetActive(true);
                        SpeedAeccelate = true; //근접시, 이펙트 발생과 함께 가속
                    }
                    transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                    Vector3 dir = (LastTarget - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 100 * Time.deltaTime);
                }
                else
                {
                    transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                    Vector3 dir = (LastTarget - transform.position).normalized;
                    transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 50 * Time.deltaTime);
                }
            }
            if (DisctanceFromEnemy <= StrikeDistance) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
            {
                transform.position += transform.up * MissileSpeedInGame * Time.deltaTime;
                Vector3 dir = (LastTarget - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, RotationSpeedInGame * 100 * Time.deltaTime);

                if (Temp == 0)
                {
                    Temp += Time.deltaTime;
                    if (ParticleNumber == 1)
                        StartCoroutine(MultiHitStart(AmmoDamage));
                    else
                    {
                        Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                        Explosion.transform.position = transform.position;
                        Explosion.transform.rotation = transform.rotation;
                        Explosion.GetComponent<CannonExplosion>().Damage = AmmoDamage;
                    }
                    if (Image != null)
                        Image.enabled = false;
                    StartCoroutine(ImageDown());
                }
            }
        }

        if (ShieldImpact == true)
        {
            RushEffect.SetActive(false);
            SpeedAeccelate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetType == 7 && collision.gameObject.layer == 23 && collision.CompareTag("Contros Ship Shield1")) //컨트로스 타격 대상. 날아가던 도중에 방패 함선의 쉴드 콜라이더에 부딛혔을 경우, 그 자리에서 데미지없이 폭발하도록 조정
        {
            if (collision != null)
            {
                Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                Explosion.transform.position = transform.position;
                Explosion.transform.rotation = transform.rotation;
                Explosion.GetComponent<CannonExplosion>().Damage = 0;
            }
            if (Image != null)
                Image.enabled = false;
            StartCoroutine(ImageDown());
        }
        if (TargetType == 6 && collision.gameObject.layer == 23 && collision.CompareTag("Player Ship Shield1")) //나리하 타격 대상. 날아가던 도중에 방패 함선의 쉴드 콜라이더에 부딛혔을 경우, 그 자리에서 데미지없이 폭발하도록 조정
        {
            if (collision != null)
            {
                if (ParticleNumber == 1)
                {
                    ShieldImpact = true;
                    TargetLocation = collision.gameObject;
                    StartCoroutine(MultiHitStart(0));
                }
            }
            if (Image != null)
                Image.enabled = false;
            StartCoroutine(ImageDown());
        }
    }

    IEnumerator ImageDown()
    {
        if (Engine1 != null)
        {
            var Engine1Emission = Engine1.emission;
            var Engine2Emission = Engine2.emission;
            var Engine3Emission = Engine3.emission;
            Engine1Emission.rateOverTime = 0;
            Engine2Emission.rateOverTime = 0;
            Engine3Emission.rateOverTime = 0;
        }

        if (Tail != null)
        {
            var TailEmission = Tail.emission;
            TailEmission.rateOverTime = 0;
        }

        if (Ammo1 != null)
        {
            if (ParticleNumber == 1) //칸타크리 게르피르-치타리오 23
            {
                RushEffect.SetActive(false);
                SpeedAeccelate = false;
                var Fire3Emission = Engine1.emission;
                var Fire4Emission = Engine2.emission;
                var Fire5Emission = Engine3.emission;
                var Fire6Emission = Ammo1.emission;
                var Fire7Emission = Ammo2.emission;
                var Fire8Emission = Ammo3.emission;
                Fire3Emission.rateOverTime = 0;
                Fire4Emission.rateOverTime = 0;
                Fire5Emission.burstCount = 0;
                Fire6Emission.burstCount = 0;
                Fire7Emission.burstCount = 0;
                Fire8Emission.burstCount = 0;
            }
        }

        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }

    IEnumerator ClusterMissileStart()
    {
        MissileSpeedInGame = MissileSpeed * 2;
        RotationSpeedInGame = RotationSpeed / 2;
        SpeedDeccelate = false;
        yield return new WaitForSeconds(0.1f);
        SpeedDeccelate = true;
    }

    IEnumerator NucelarMissileStart()
    {
        MissileSpeedInGame = MissileSpeed * 3;
        LockOn = false;
        yield return new WaitForSeconds(1);
        RotationSpeedInGame = RotationSpeed;
        LockOn = true;
    }

    //칸타크리 게르피르-치타리오 23 추진
    IEnumerator KantakriMultiHit1Start()
    {
        LockOn = false;
        yield return new WaitForSeconds(1);
        LockOn = true;
    }

    //칸타크리 게르피르-치타리오 23 다단히트
    IEnumerator MultiHitStart(int damage)
    {
        for (int i = 0; i < 5; i++)
        {
            Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
            float RandomMovement = Random.Range(-0.5f, 0.5f);
            Explosion.transform.position = new Vector3(transform.position.x + RandomMovement, transform.position.y + RandomMovement, transform.position.z);
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosion>().Damage = damage;
            yield return new WaitForSeconds(0.2f);
        }
    }
}