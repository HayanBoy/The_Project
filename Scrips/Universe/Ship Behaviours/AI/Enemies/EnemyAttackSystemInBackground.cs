using System.Collections;
using UnityEngine;

public class EnemyAttackSystemInBackground : MonoBehaviour
{
    Animator animator;

    public int NationType; //����. 1 = ���θ��, 2 = ĭŸũ��
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

    public GameObject SloriusEnergyRay1Prefab; //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ���� �ִϸ��̼ǿ� ������
    GameObject SloriusEnergyRay1; //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ����
    GameObject KantakriMissile1;

    void OnEnable()
    {
        AttackTime = FireRate / 2;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //AttackTime ���ڸ� FireRate�� ���� �ʴ� ���ǿ��� �ǽð����� ����
        if (AttackTime <= FireRate + RandomFire && TargetShip != null)
            AttackTime += Time.deltaTime;

        if (TargetShip != null) //Ÿ���� ���� ��쿡�� ���� �۵�
        {
            Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //���� ȸ��
            transform.right = Vector3.Lerp(transform.right, dir, 6 * Time.deltaTime);
        }

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
        }
        if (TargetShip != null && TargetShip.layer != 19)
            TargetShip = null;

        //�ʴ� ���� �߻�
        if (AttackTime >= FireRate + RandomFire)
        {
            AttackTime = 0;
            RandomFire = Random.Range(0, 0.15f);

            if (NationType == 1 && CannonType == 1) //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ����
            {
                this.gameObject.GetComponent<RayAttachment>().Fire = true;
                this.gameObject.GetComponent<RayAttachment>().FireEndPos = TargetShip.transform.position;
                StartCoroutine(FireSloriusEnergyRay1());
                StartCoroutine(FireSloriusEnergyRayAnimation());
            }
            else if (NationType == 2 && CannonType == 3)
                KantakriWeaponSystem3();
        }
    }

    //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ���� ����
    IEnumerator FireSloriusEnergyRay1()
    {
        for (int i = 0; i <= DamageCount; i++)
        {
            SloriusWeaponSystem1();
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
        this.gameObject.GetComponent<RayAttachment>().Fire = false;

        yield return new WaitForSeconds(RandomFire);
    }

    IEnumerator FireSloriusEnergyRayAnimation()
    {
        animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Ickshiiu Shuluus(Formation), Slorius", false);
    }

    //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ���� ������ ����ü ����
    void SloriusWeaponSystem1()
    {
        SloriusEnergyRay1 = ShipAmmoObjectPoolInBackground.instance.Loader("SloriusEnergyRay1");
        SloriusEnergyRay1.transform.position = TurretLocation.transform.position;
        SloriusEnergyRay1.transform.rotation = TurretLocation.transform.rotation;
        SloriusEnergyRay1.GetComponent<CannonMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "SloriusEnergyRay1Explosion");
    }

    void KantakriWeaponSystem3()
    {
        KantakriMissile1 = ShipAmmoObjectPoolInBackground.instance.Loader("KantakriMissile1");
        KantakriMissile1.transform.position = TurretLocation.transform.position;
        KantakriMissile1.transform.rotation = TurretLocation.transform.rotation;
        KantakriMissile1.GetComponent<NarihaMissileMovementInBackground>().SetDamage(AmmoDamage, TargetShip, "KantakriMissile1Explosion");
    }
}