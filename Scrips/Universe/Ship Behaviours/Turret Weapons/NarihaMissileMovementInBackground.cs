using System.Collections;
using UnityEngine;

public class NarihaMissileMovementInBackground : MonoBehaviour
{
    public float MissileSpeed; //�̻��� �ӵ�
    private float MissileActiveSpeed; //�̻��� �ӵ�(�ΰ��ӿ�)
    private string ExplosionName;
    GameObject Explosion; //ź���� ������ ���� ����Ʈ. �� ����Ʈ�� ������ ������ �Լ��� �浹�ϸ� �������� �����Ѵ�
    private int AmmoDamage;
    public GameObject TargetLocation; //��ǥ ���

    private int StrikeMode; //������� Ȱ��ȭ
    public bool RotationState; //ȸ������
    public GameObject RotationEngineRight; //���� ��ȯ ���� ��ȭ ����Ʈ
    public GameObject RotationEngineLeft;
    public GameObject SecondEngineEffect1; //2�� ���� ��ȭ ����Ʈ
    public GameObject SecondEngineEffect2;

    public SpriteRenderer Image; //�̻��� ��ź��, �̻��� ��������Ʈ�� ����
    public ParticleSystem Fire1; //�̻��� ��ź��, �̻����� ���ư��� �Ҳ� ������ ���� �ð� ���� ����
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
            transform.up = Vector3.Lerp(transform.up, dir, 8 * Time.deltaTime);
        }
        else if (StrikeMode == 2) //Ÿ���� ���� ����
        {
            MissileActiveSpeed += Time.deltaTime * 100;

            transform.position += transform.up * MissileActiveSpeed * Time.deltaTime;
            Vector3 dir = (TargetLocation.transform.position - transform.position).normalized;
            transform.up = Vector3.Lerp(transform.up, dir, 8 * Time.deltaTime);
        }

        Vector3 MoveDir = (TargetLocation.transform.position - transform.position).normalized;
        if (Vector3.Distance(TargetLocation.transform.position, transform.position) <= 0.01f) //�������� ������ ���, ������ ���� ������Ʈ ����
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

    //���� ��ȭ
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
        if (collision.gameObject.layer == 7) //������ ��ź
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