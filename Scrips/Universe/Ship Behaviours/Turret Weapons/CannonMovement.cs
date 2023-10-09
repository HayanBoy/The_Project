using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [Header("��ź ����")]
    public int TargetType;
    public float AmmoSpeed; //���� ź�� �ӵ�
    public int ParticleNumber; //��ƼŬ�� ���� ��쿡�� ���. 1 = ���θ�� ���ͽ� ��콺
    private string ExplosionName;
    private string DeleteAmmoName;
    private string DeleteExplosionName;
    GameObject Explosion; //ź���� ������ ���� ����Ʈ. �� ����Ʈ�� ������ ������ �Լ��� �浹�ϸ� �������� �����Ѵ�
    private int AmmoDamage;

    [Header("���")]
    public GameObject TargetLocation; //��ǥ ���

    [Header("����ġ")]
    public bool isTale; //���� ������ �ִ��� ����
    public bool isParticle; //��ƼŬ�� �̷�������� ����
    public ParticleSystem Ammo;
    public ParticleSystem Ammo2;
    public ParticleSystem Ammo3;
    public bool ExplosionType; //���� Ÿ�������� ���� ����

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
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //�������� ������ ���, ������ ���� ������Ʈ ����
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
        if (TargetType == 6) //������ Ÿ�� ���
        {
            if (TargetType == 6 && collision.gameObject.layer == 23 && collision.CompareTag("Player Ship Shield1")) //���ư��� ���߿� ���� �Լ��� ���� �ݶ��̴��� �ε����� ���, �� �ڸ����� ���������� �����ϵ��� ����
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

        if (TargetType == 7) //��Ʈ�ν� Ÿ�� ���
        {
            if (TargetType == 7 && collision.gameObject.layer == 23 && collision.CompareTag("Contros Ship Shield1")) //���ư��� ���߿� ���� �Լ��� ���� �ݶ��̴��� �ε����� ���, �� �ڸ����� ���������� �����ϵ��� ����
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