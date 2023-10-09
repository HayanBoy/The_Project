using System.Collections;
using UnityEngine;

public class NarihaTurretAttackSystemInBackground : MonoBehaviour
{
    Animator anim;

    public int CannonType;

    public int AmmoDamage; //���� ������
    public float FireRate; //�ð��� 1ȸ ����
    private float RandomFire; //����ӵ��� ����ȭ
    private float AttackTime; //FireRate ��Ÿ�� ����
    public int DamageCount; //�������� �ѹ��� �� �� �ִ����� ���� ����

    public float AttackEyeRange; //���ݰ����� ��Ÿ�

    public GameObject TargetShip; //��ǥ ���� ���
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    public Transform TurretLocation; //���� ��ġ

    public bool TargetOnline; //��ǥ�� �����Ǿ������� ���� ����ġ
    private float TargetMarkTime; //����� ���� ��ũ �ִϸ��̼� �ߵ��� �� �ѹ��� �߻��ϵ��� ����

    public GameObject NarihaFireEffect; //������ ���Ϸ��� �ý�Ʈ ���� ���� ������
    public GameObject MissileFireEffect; //������ �̻��� �߻� ����
    GameObject NarihaArtillery1;
    GameObject NarihaMissile1;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackTime = FireRate / 2;
    }

    void Update()
    {
        //AttackTime ���ڸ� FireRate�� ���� �ʴ� ���ǿ��� �ǽð����� ����
        if (AttackTime <= FireRate + RandomFire && TargetShip != null)
            AttackTime += Time.deltaTime;

        Collider2D[] TargetShips = Physics2D.OverlapCircleAll(transform.position, AttackEyeRange, layerMask); //�ǽð����� AttackEyeRange ���������� ���� ����� ����� �˻�
        float shortestDistance = Mathf.Infinity;
        Collider2D nearestTarget = null;

        foreach (Collider2D TargetShip in TargetShips)
        {
            float DistanceToMonsters = Vector3.Distance(transform.position, TargetShip.transform.position);

            if (DistanceToMonsters < shortestDistance) //���� ����� Ÿ������ ����
            {
                shortestDistance = DistanceToMonsters;
                nearestTarget = TargetShip;
                TargetMarkTime = 0;
            }
        }
        if (nearestTarget != null && shortestDistance <= AttackEyeRange) //������ Ÿ���� TargetShip����Ʈ�� �ø���
        {
            TargetShip = nearestTarget.gameObject;
            TargetOnline = true;

            Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //���� ȸ��
            transform.right = Vector3.Lerp(transform.right, dir, 10 * Time.deltaTime);
        }
        if (TargetShip != null && TargetShip.layer != 20)
            TargetShip = null;

        //�ʴ� ���� �߻�
        if (AttackTime >= FireRate + RandomFire)
        {
            AttackTime = 0;
            RandomFire = Random.Range(0, 0.15f);

            if (CannonType == 1) //������ ���Ϸ��� �ý�Ʈ ���� ���� ������
                StartCoroutine(FireNarihaQuantum1());
            else if (CannonType == 3)
                StartCoroutine(FireNarihaMissile1());
        }
    }

    //������ ���Ϸ��� �ý�Ʈ ���� ���� ������ ����
    IEnumerator FireNarihaQuantum1()
    {
        NarihaFireEffect.SetActive(true);

        for (int i = 0; i <= DamageCount; i++)
        {
            WeaponSystem1();
            if (i < DamageCount)
            {
                anim.SetFloat("Fire Cannon(Formation)", 1);
                yield return new WaitForSeconds(0.2f);
                anim.SetFloat("Fire Cannon(Formation)", 0);
                yield return new WaitForSeconds(0.05f);
            }
            else if (i == DamageCount)
            {
                anim.SetFloat("Fire Cannon(Formation)", 2);
                yield return new WaitForSeconds(1.16f);
                anim.SetFloat("Fire Cannon(Formation)", 0);
            }
        }

        yield return new WaitForSeconds(0.25f);
        NarihaFireEffect.SetActive(false);

        yield return new WaitForSeconds(RandomFire);
    }

    //������ �̻��� �߻� ����
    IEnumerator FireNarihaMissile1()
    {
        WeaponSystem2();
        MissileFireEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    //������ ���Ϸ��� �ý�Ʈ ���� ���� ������(��� �Լ���) ������ ����ü ����
    void WeaponSystem1()
    {
        NarihaArtillery1 = ShipAmmoObjectPoolInBackground.instance.Loader("NarihaArtillery1");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "NarihaArtillery1Explosion");
    }

    //������ �̻���(��� �Լ���) ������ ����ü ����
    void WeaponSystem2()
    {
        NarihaMissile1 = ShipAmmoObjectPoolInBackground.instance.Loader("NarihaMissile1");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "NarihaMissile1Explosion");
        NarihaMissile1.GetComponent<NarihaMissileMovementInBackground>().SetRotation();
    }
}