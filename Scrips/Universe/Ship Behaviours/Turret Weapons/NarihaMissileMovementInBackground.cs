using System.Collections;
using UnityEngine;

public class NarihaMissileMovementInBackground : MonoBehaviour
{
    public float MissileSpeed; //미사일 속도
    private float MissileActiveSpeed; //미사일 속도(인게임용)
    private string ExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    private int AmmoDamage;
    public GameObject TargetLocation; //목표 대상

    private int StrikeMode; //돌진모드 활성화
    public bool RotationState; //회전방향
    public GameObject RotationEngineRight; //방향 전환 엔진 점화 이펙트
    public GameObject RotationEngineLeft;
    public GameObject SecondEngineEffect1; //2차 엔진 점화 이펙트
    public GameObject SecondEngineEffect2;

    public SpriteRenderer Image; //미사일 착탄시, 미사일 스프라이트를 끄기
    public ParticleSystem Fire1; //미사일 착탄시, 미사일의 날아가던 불꽃 궤적을 일정 시간 동안 유지
    public ParticleSystem Fire2;

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
        SecondEngineEffect2.SetActive(false);
        RotationEngineRight.SetActive(false);
        RotationEngineLeft.SetActive(false);
        Image.enabled = true;

        var Fire1Emission = Fire1.emission;
        var Fire2Emission = Fire2.emission;
        Fire1Emission.rateOverTime = 50;
        Fire2Emission.rateOverTime = 100;
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

        Vector3 MoveDir = (TargetLocation.transform.position - transform.position).normalized;
        if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
        {
            if (Temp == 0)
            {
                Temp += Time.deltaTime;
                Image.enabled = false;
                Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
                Explosion.transform.position = transform.position;
                Explosion.transform.rotation = transform.rotation;
                Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
                Explosion.GetComponent<CannonExplosionInBackground>().Explosion = true;
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
        SecondEngineEffect1.SetActive(true);
        SecondEngineEffect2.SetActive(true);
        StrikeMode = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //나리하 포탄
        {
            Image.enabled = false;
            Explosion = ShipAmmoObjectPoolInBackground.instance.Loader(ExplosionName);
            Explosion.transform.position = transform.position;
            Explosion.transform.rotation = transform.rotation;
            Explosion.GetComponent<CannonExplosionInBackground>().Damage = AmmoDamage;
            Explosion.GetComponent<CannonExplosionInBackground>().Explosion = true;
            StartCoroutine(ImageDown());
        }
    }

    IEnumerator ImageDown()
    {
        var Fire1Emission = Fire1.emission;
        var Fire2Emission = Fire2.emission;
        Fire1Emission.rateOverTime = 0;
        Fire2Emission.rateOverTime = 0;
        StrikeMode = 5;
        MissileActiveSpeed = 0;
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}