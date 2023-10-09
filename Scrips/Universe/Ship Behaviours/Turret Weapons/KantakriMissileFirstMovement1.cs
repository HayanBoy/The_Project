using System.Collections;
using UnityEngine;

public class KantakriMissileFirstMovement1 : MonoBehaviour
{
    [Header("기본 정보")]
    public float MissileActiveSpeed;
    public Transform MissileLocation; //미사일 스폰 위치
    public GameObject TargetLocation; //목표 대상

    private int AmmoDamage;
    private string ExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    GameObject KantakriMissile1;

    [Header("본체 분리 효과")]
    public GameObject Division1; //미사일 분리
    public GameObject Division2;
    public Transform Division1Pos;
    public Transform Division2Pos;

    [Header("착탄 효과")]
    public SpriteRenderer Image; //미사일 착탄시, 미사일 스프라이트를 끄기
    public ParticleSystem Fire1; //미사일 착탄시, 미사일의 날아가던 불꽃 궤적을 일정 시간 동안 유지
    public ParticleSystem Fire2;

    private bool FlyingMode = false;

    private void OnEnable()
    {
        StartCoroutine(DevisionStart());
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        AmmoDamage = 0;
        ExplosionName = null;
        TargetLocation = null;
        Image.enabled = true;

        var Fire1Emission = Fire1.emission;
        var Fire2Emission = Fire2.emission;
        Fire1Emission.rateOverTime = 50;
        Fire2Emission.rateOverTime = 100;
    }

    public void SetDamage(int Damage, GameObject Target, string Name)
    {
        AmmoDamage = Damage;
        TargetLocation = Target;
        ExplosionName = Name;
    }

    void Update()
    {
        if (FlyingMode == true)
            transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
    }

    IEnumerator DevisionStart()
    {
        FlyingMode = true;

        yield return new WaitForSeconds(0.75f);
        Firing();
        Image.enabled = false;
        GameObject Debris1 = Instantiate(Division1, Division1Pos.position, transform.rotation);
        GameObject Debris2 = Instantiate(Division2, Division2Pos.position, transform.rotation);
        yield return new WaitForSeconds(0.35f);
        Firing();
        yield return new WaitForSeconds(0.35f);
        Firing();
        yield return new WaitForSeconds(0.15f);
        Firing();
        FlyingMode = false;
        StartCoroutine(ImageDown());
    }

    IEnumerator ImageDown()
    {
        var Fire1Emission = Fire1.emission;
        var Fire2Emission = Fire2.emission;
        Fire1Emission.rateOverTime = 0;
        Fire2Emission.rateOverTime = 0;
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }

    void Firing()
    {
        KantakriMissile1 = ShipAmmoObjectPool.instance.Loader("KantakriMissile1");
        KantakriMissile1.transform.position = MissileLocation.transform.position;
        int RandomRotation = Random.Range(0, 2);
        if (RandomRotation == 0)
            KantakriMissile1.transform.eulerAngles = new Vector3(KantakriMissile1.transform.eulerAngles.x, KantakriMissile1.transform.eulerAngles.y, KantakriMissile1.transform.eulerAngles.z + 180);
        KantakriMissile1.GetComponent<KantakriMissileMovement>().SetDamage(AmmoDamage, TargetLocation, "KantakriMissile1Explosion");
        KantakriMissile1.GetComponent<KantakriMissileMovement>().SetRotation();
    }
}