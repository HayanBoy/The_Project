using System.Collections;
using UnityEngine;

public class NarihaMissileMovement : MonoBehaviour
{
    [Header("나리하 미사일 정보")]
    public float MissileSpeed; //미사일 속도
    private float MissileActiveSpeed; //미사일 속도(인게임용)
    private string ExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    private int AmmoDamage;
    public bool ExplosionType; //폭발 타입인지에 대한 여부

    [Header("대상")]
    public GameObject TargetLocation; //목표 대상

    [Header("미사일 모드 및 연출")]
    private int StrikeMode; //돌진모드 활성화
    public bool RotationState; //회전방향
    public GameObject RotationEngineRight; //방향 전환 엔진 점화 이펙트
    public GameObject RotationEngineLeft;
    public GameObject SecondEngineEffect1; //2차 엔진 점화 이펙트
    public ParticleSystem Tail;

    [Header("착탄효과")]
    public SpriteRenderer Image; //미사일 착탄시, 미사일 스프라이트를 끄기

    [Header("사운드")]
    public AudioClip MissileEngineFire;


    private float Temp;

    public void SetRotation()
    {
        int RandomRotation = Random.Range(0, 2);
        if (RandomRotation == 0)
        {
            transform.eulerAngles = (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180));
            RotationState = true;
        }
    }

    private void OnEnable()
    {
        MissileActiveSpeed = MissileSpeed;
        StartCoroutine(FirstEngineEngage());
        Invoke("Destroy", 2);
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        Temp = 0;
        AmmoDamage = 0;
        MissileActiveSpeed = MissileSpeed;
        StrikeMode = 0;
        RotationState = false;
        ExplosionName = null;
        TargetLocation = null;
        SecondEngineEffect1.SetActive(false);
        RotationEngineRight.SetActive(false);
        RotationEngineLeft.SetActive(false);
        Image.enabled = true;

        if (Tail != null)
        {
            var TailEmission = Tail.emission;
            TailEmission.rateOverTime = 10;
        }
    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
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
            if (StrikeMode == 0) //수직 발사
            {
                transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
            }
            else if (StrikeMode == 1) //타겟을 향해 속도를 줄이며 회전
            {
                MissileActiveSpeed = 0;
                if (RotationState == false)
                    RotationEngineLeft.SetActive(true);
                else
                    RotationEngineRight.SetActive(true);

                Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, 8 * Time.deltaTime);
            }
            else if (StrikeMode == 2) //타겟을 향해 돌진
            {
                MissileActiveSpeed += Time.deltaTime * 100;

                transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
                Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, 8 * Time.deltaTime);
            }
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 1) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
            {
                if (Temp == 0)
                {
                    Temp += Time.deltaTime;
                    if (TargetLocation != null)
                    {
                        Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                        Explosion.transform.position = transform.position;
                        Explosion.transform.rotation = transform.rotation;
                        Explosion.GetComponent<CannonExplosion>().Damage = AmmoDamage;
                        Explosion.GetComponent<CannonExplosion>().Explosion = true;
                    }
                    Image.enabled = false;
                    StartCoroutine(ImageDown());
                }
            }
        }
        else
        {
            transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
            if (Temp == 0)
            {
                Temp += Time.deltaTime;
                Image.enabled = false;
                StartCoroutine(ImageDown());
            }
        }
    }

    //엔진 점화
    IEnumerator FirstEngineEngage()
    {
        yield return new WaitForSeconds(0.25f);
        StrikeMode = 1;
        yield return new WaitForSeconds(0.35f);
        UniverseSoundManager.instance.UniverseSoundPlayMaster("Nariha Missile Engine Fire Sound", MissileEngineFire, this.transform.position);
        SecondEngineEffect1.SetActive(true);
        StrikeMode = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 23 && collision.CompareTag("Contros Ship Shield1")) //날아가던 도중에 방패 함선의 쉴드 콜라이더에 부딛혔을 경우, 그 자리에서 데미지없이 폭발하도록 조정
        {
            if (collision != null)
            {
                Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                Explosion.transform.position = transform.position;
                Explosion.transform.rotation = transform.rotation;
                Explosion.GetComponent<CannonExplosion>().Damage = 0;
            }
            Image.enabled = false;
            StartCoroutine(ImageDown());
        }
    }

    IEnumerator ImageDown()
    {
        StrikeMode = 5;
        MissileActiveSpeed = 0;

        if (Tail != null)
        {
            var TailEmission = Tail.emission;
            TailEmission.rateOverTime = 0;
        }

        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}