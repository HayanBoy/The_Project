using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KantakriMissileMovement : MonoBehaviour
{
    [Header("ĭŸũ�� �̻��� ����")]
    public float MissileSpeed; //�̻��� �ӵ�
    private float MissileActiveSpeed; //�̻��� �ӵ�(�ΰ��ӿ�)
    private string ExplosionName;
    GameObject Explosion; //ź���� ������ ���� ����Ʈ. �� ����Ʈ�� ������ ������ �Լ��� �浹�ϸ� �������� �����Ѵ�
    private int AmmoDamage;
    public bool ExplosionType; //���� Ÿ�������� ���� ����

    [Header("���")]
    public GameObject TargetLocation; //��ǥ ���

    [Header("�̻��� ��� �� ����")]
    private int StrikeMode; //������� Ȱ��ȭ
    public bool RotationState; //ȸ������
    public GameObject RotationEngineRight; //���� ��ȯ ���� ��ȭ ����Ʈ
    public GameObject RotationEngineLeft;
    public GameObject SecondEngineEffect1; //2�� ���� ��ȭ ����Ʈ
    public GameObject SecondEngineEffect2;

    [Header("��źȿ��")]
    public SpriteRenderer Image; //�̻��� ��ź��, �̻��� ��������Ʈ�� ����
    public ParticleSystem Fire1; //�̻��� ��ź��, �̻����� ���ư��� �Ҳ� ������ ���� �ð� ���� ����
    public ParticleSystem Fire2;
    public ParticleSystem Engine1;
    public ParticleSystem Engine2;
    public ParticleSystem Engine3;

    [Header("����")]
    public AudioClip MissileEngineFire;


    private float Temp;

    public void SetRotation()
    {
        RotationState = true;
    }

    private void OnEnable()
    {
        MissileActiveSpeed = MissileSpeed;
        StartCoroutine(FirstEngineEngage());
        Invoke("Destroy", 3);
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

        if (Engine1 != null)
        {
            var Engine1Emission = Engine1.emission;
            var Engine2Emission = Engine2.emission;
            var Engine3Emission = Engine3.emission;
            Engine1Emission.rateOverTime = 10;
            Engine2Emission.rateOverTime = 10;
            Engine3Emission.rateOverTime = 15;
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
        if (TargetLocation != null) //����� �������� ���� ���, �״�� �����Ͽ� ������ ����
        {
            if (StrikeMode == 0) //���� �߻�
            {
                transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
            }
            else if (StrikeMode == 1) //Ÿ���� ���� �ӵ��� ���̸� ȸ��
            {
                MissileActiveSpeed = 0;
                if (RotationState == false)
                    RotationEngineLeft.SetActive(true);
                else
                    RotationEngineRight.SetActive(true);

                Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, 3 * Time.deltaTime);
            }
            else if (StrikeMode == 2) //Ÿ���� ���� ����
            {
                if (MissileActiveSpeed < MissileSpeed * 10)
                    MissileActiveSpeed += Time.deltaTime * 100;
                else if (MissileActiveSpeed >= MissileSpeed * 10)
                {
                    MissileActiveSpeed = MissileSpeed * 6;
                }

                transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
                Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
                transform.up = Vector3.Lerp(transform.up, dir, 100 * Time.deltaTime);
            }

            Vector3 MoveDir = (TargetLocation.transform.position - transform.position).normalized;
            if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 1) //�������� ������ ���, ������ ���� ������Ʈ ����
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
        else //�Լ��� �������� ���, �������� ���� �ʰ�, ������ �ڸ��� �����Ͽ� �Ҹ�
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

    //���� ��ȭ
    IEnumerator FirstEngineEngage()
    {
        float RandomTime = Random.Range(0.25f, 0.5f);
        yield return new WaitForSeconds(RandomTime);
        StrikeMode = 1;
        yield return new WaitForSeconds(0.35f);
        UniverseSoundManager.instance.UniverseSoundPlayMaster("Kantakri Missile Engine Fire Sound", MissileEngineFire, this.transform.position);
        SecondEngineEffect1.SetActive(true);
        SecondEngineEffect2.SetActive(true);
        StrikeMode = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 23 && collision.CompareTag("Player Ship Shield1")) //���ư��� ���߿� ���� �Լ��� ���� �ݶ��̴��� �ε����� ���, �� �ڸ����� ���������� �����ϵ��� ����
        {
            if (collision != null)
            {
                collision.gameObject.transform.parent.GetComponent<NarihaShieldSystem>().ShieldDefenceEffect(transform.position);
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
        var Fire1Emission = Fire1.emission;
        var Fire2Emission = Fire2.emission;
        Fire1Emission.rateOverTime = 0;
        Fire2Emission.rateOverTime = 0;

        if (Engine1 != null)
        {
            var Engine1Emission = Engine1.emission;
            var Engine2Emission = Engine2.emission;
            var Engine3Emission = Engine3.emission;
            Engine1Emission.rateOverTime = 0;
            Engine2Emission.rateOverTime = 0;
            Engine3Emission.rateOverTime = 0;
        }

        StrikeMode = 5;
        MissileActiveSpeed = 0;
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}