using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [Header("포탄 정보")]
    public int TargetType;
    public float AmmoSpeed; //함포 탄알 속도
    public int ParticleNumber; //파티클이 있을 경우에만 사용. 1 = 슬로리어스 아익쉬 쇼우스
    private string ExplosionName;
    private string DeleteAmmoName;
    private string DeleteExplosionName;
    GameObject Explosion; //탄알이 폭발할 때의 이펙트. 이 이펙트가 생성한 지점의 함선과 충돌하면 데미지를 전달한다
    private int AmmoDamage;

    [Header("대상")]
    public GameObject TargetLocation; //목표 대상

    [Header("스위치")]
    public bool isTale; //비행 꼬리가 있는지 여부
    public bool isParticle; //파티클로 이루어졌는지 여부
    public ParticleSystem Ammo;
    public ParticleSystem Ammo2;
    public ParticleSystem Ammo3;
    public bool ExplosionType; //폭발 타입인지에 대한 여부

    private float Temp;

    private void OnEnable()
    {
        if (isTale == false)
            Invoke("TurnOff", 2);
        else
            Invoke("TurnOff", 3);
        if (Ammo != null)
        {
            if (ParticleNumber == 1)
            {
                var Fire1Emission = Ammo.emission;
                var Fire2Emission = Ammo2.emission;
                var Fire3Emission = Ammo3.emission;
                Fire1Emission.rateOverTime = 10;
                Fire2Emission.rateOverTime = 50;
                Fire3Emission.rateOverTime = 50;
            }
        }
    }

    private void OnDisable()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>() != null)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Temp = 0;
        AmmoDamage = 0;
        ExplosionName = null;
        TargetLocation = null;
    }

    void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void SetDamage(int Damage, GameObject Target, string Name, string DeleteAmmo, string ExplosionAmmo)
    {
        AmmoDamage = Damage;
        TargetLocation = Target;
        ExplosionName = Name;
        DeleteAmmoName = DeleteAmmo;
        DeleteExplosionName = ExplosionAmmo;
    }

    void Update()
    {
        if (TargetLocation != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetLocation.transform.position, AmmoSpeed * Time.deltaTime);
            Vector3 MoveDir = (TargetLocation.transform.position - transform.position).normalized;
            transform.right = Vector3.Lerp(transform.right, MoveDir, 100 * Time.deltaTime);
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //목적지에 도달할 경우, 데미지 전달 오브젝트 생성
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
                        Explosion.GetComponent<CannonExplosion>().SetDamage(DeleteExplosionName);
                    }
                    if (isTale == false && isParticle == false)
                    {
                        ShipAmmoObjectPool.instance.Deleter(DeleteAmmoName);
                        this.gameObject.SetActive(false);
                    }
                    else
                        StartCoroutine(TaleTurnOff());
                    if (isParticle == true)
                        StartCoroutine(ParticleTurnOff());
                    if (Ammo != null)
                    {
                        var Fire1Emission = Ammo.emission;
                        Fire1Emission.rateOverTime = 0;
                    }
                }
            }
        }
        else
        {
            if (isTale == false)
                this.gameObject.SetActive(false);
            else
                StartCoroutine(TaleTurnOff());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetType == 6) //나리하 타격 대상
        {
            if (TargetType == 6 && collision.gameObject.layer == 23 && collision.CompareTag("Player Ship Shield1")) //날아가던 도중에 방패 함선의 쉴드 콜라이더에 부딛혔을 경우, 그 자리에서 데미지없이 폭발하도록 조정
            {
                if (collision != null)
                {
                    collision.gameObject.transform.parent.GetComponent<NarihaShieldSystem>().ShieldDefenceEffect(transform.position);
                    Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                    Explosion.transform.position = transform.position;
                    Explosion.transform.rotation = transform.rotation;
                    Explosion.GetComponent<CannonExplosion>().Damage = 0;
                    Explosion.GetComponent<CannonExplosion>().SetDamage(DeleteExplosionName);
                }
                if (isTale == false)
                {
                    ShipAmmoObjectPool.instance.Deleter(DeleteAmmoName);
                    this.gameObject.SetActive(false);
                }
                else
                    StartCoroutine(TaleTurnOff());
                if (Ammo != null)
                {
                    if (ParticleNumber == 1)
                    {
                        var Fire1Emission = Ammo.emission;
                        var Fire2Emission = Ammo2.emission;
                        var Fire3Emission = Ammo3.emission;
                        Fire1Emission.rateOverTime = 0;
                        Fire2Emission.rateOverTime = 0;
                        Fire3Emission.rateOverTime = 0;
                    }
                }
            }
        }

        if (TargetType == 7) //컨트로스 타격 대상
        {
            if (TargetType == 7 && collision.gameObject.layer == 23 && collision.CompareTag("Contros Ship Shield1")) //날아가던 도중에 방패 함선의 쉴드 콜라이더에 부딛혔을 경우, 그 자리에서 데미지없이 폭발하도록 조정
            {
                if (collision != null)
                {
                    Explosion = ShipAmmoObjectPool.instance.Loader(ExplosionName);
                    Explosion.transform.position = transform.position;
                    Explosion.transform.rotation = transform.rotation;
                    Explosion.GetComponent<CannonExplosion>().Damage = 0;
                    Explosion.GetComponent<CannonExplosion>().SetDamage(DeleteExplosionName);
                }
                if (isTale == false)
                {
                    ShipAmmoObjectPool.instance.Loader(DeleteAmmoName);
                    this.gameObject.SetActive(false);
                }
                else
                    StartCoroutine(TaleTurnOff());
            }
        }
    }

    IEnumerator TaleTurnOff()
    {
        if (Ammo == null)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        ShipAmmoObjectPool.instance.Deleter(DeleteAmmoName);
        this.gameObject.SetActive(false);
    }

    IEnumerator ParticleTurnOff()
    {
        yield return new WaitForSeconds(3);
        ShipAmmoObjectPool.instance.Deleter(DeleteAmmoName);
        this.gameObject.SetActive(false);
    }
}