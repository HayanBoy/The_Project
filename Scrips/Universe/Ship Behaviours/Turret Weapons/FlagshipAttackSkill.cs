using UnityEngine;

public class FlagshipAttackSkill : MonoBehaviour
{
    [Header("���� ���� ����ġ")]
    public int SkillType; //��ų ���� ����. 1 = ���� �ܵ� ����, 2 = �Դ� ����, 3 = ���� ����
    public int SkillNumber; //SkillType�� �� 3������ ���� ��ų ��ȣ
    public int SecondSkillType; //��ų ���� ����. 1 = ���� �ܵ� ����, 2 = �Դ� ����, 3 = ���� ����
    public int SecondSkillNumber; //SkillType�� �� 3������ ���� ��ų ��ȣ
    public int ThirdSkillType; //��ų ���� ����. 1 = ���� �ܵ� ����, 2 = �Դ� ����, 3 = ���� ����
    public int ThirdSkillNumber; //SkillType�� �� 3������ ���� ��ų ��ȣ

    public bool canAttack = false; //���� ���� ����
    public bool RangeAttack = false; //��Ÿ����� ���� ������ ����
    public bool OrderTarget = false; //������ �Ҽ��Լ��鿡�� ������ ��� ����� �޾��� ��쿡�� �ߵ�

    [Header("��ǥ �� ���� ��ġ")]
    public GameObject TargetShip; //��ǥ ���� ���
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    public bool TargetOnline; //��ǥ�� �����Ǿ������� ���� ����ġ
    private float TargetMarkTime; //����� ���� ��ũ �ִϸ��̼� �ߵ��� �� �ѹ��� �߻��ϵ��� ����

    [Header("���� ��Ÿ�")]
    public float AttackEyeRange; //���ݰ����� ��Ÿ�

    [Header("���� ���� ����")]
    public int AmmoDamage; //���� ������
    public float FireRate; //�ð��� 1ȸ ����
    public float SecondFireRate;
    public float ThirdFireRate;
    private float RandomFire; //����ӵ��� ����ȭ
    private float AttackTime; //FireRate ��Ÿ�� ����
    private float SecondAttackTime;
    private float ThirdAttackTime;
    public int DamageCount; //�������� �ѹ��� �� �� �ִ����� ���� ����

    [Header("���� ���� ��ų ������ ����")]
    public int SikroClassCruiseMissileDamage;

    [Header("�Դ� ���� ��ų ������ ����")]
    public int Cysiro47PatriotMissileDamage;

    private void Start()
    {
        AttackTime = FireRate * 0.8f;
        SecondAttackTime = SecondFireRate * 0.8f;
        ThirdAttackTime = ThirdFireRate * 0.8f;

        SikroClassCruiseMissileDamage = UpgradeDataSystem.instance.SikroClassCruiseMissileDamage;
        Cysiro47PatriotMissileDamage = UpgradeDataSystem.instance.Cysiro47PatriotMissileDamage;
    }

    void Update()
    {
        //����� ������ ���, ��� ����
        if (canAttack == true)
        {
            //AttackTime ���ڸ� FireRate�� ���� �ʴ� ���ǿ��� �ǽð����� ����
            if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && AttackTime <= FireRate + RandomFire && TargetShip != null)
                AttackTime += Time.deltaTime;
            if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && SecondAttackTime <= SecondFireRate + RandomFire && TargetShip != null)
                SecondAttackTime += Time.deltaTime;
            if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && ThirdAttackTime <= ThirdFireRate + RandomFire && TargetShip != null)
                ThirdAttackTime += Time.deltaTime;

            if (OrderTarget == false) //������ ������ ���, �׸��� �������κ��� ������ ����� ���� �ʾ��� ���, �� �Լ����� ���� ����� ���� �ڵ� ����
            {
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

                    if (TargetMarkTime == 0) //������ ����� ���� ��ũ �߻�
                    {
                        TargetMarkTime += Time.deltaTime;
                        //SoundManager.instance.SFXPlay11("Sound", Beep1);
                    }
                }
                else
                {
                    TargetShip = null;
                    TargetOnline = false;
                }
            }

            if (RangeAttack == true) //��� ������ ��Ÿ��� ����� ��� ����
            {
                //�ʴ� ���� �߻�
                if (WeaponUnlockManager.instance.FlagshipFirstSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && AttackTime >= FireRate + RandomFire)
                {
                    AttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (SkillType == 1 && SkillNumber == 1)
                        NuclearMissileAttack();
                    if (SkillType == 2 && SkillNumber == 1)
                        ClusterMissileAttack();
                }
                if (WeaponUnlockManager.instance.FlagshipSecondSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && SecondAttackTime >= SecondFireRate + RandomFire)
                {
                    SecondAttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (SecondSkillType == 1 && SecondSkillNumber == 1)
                        NuclearMissileAttack();
                    if (SecondSkillType == 2 && SecondSkillNumber == 1)
                        ClusterMissileAttack();
                }
                if (WeaponUnlockManager.instance.FlagshipThirdSlotUnlock[GetComponent<MoveVelocity>().FlagshipNumber] == true && ThirdAttackTime >= ThirdFireRate + RandomFire)
                {
                    ThirdAttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (ThirdSkillType == 1 && ThirdSkillNumber == 1)
                        NuclearMissileAttack();
                    if (ThirdSkillType == 2 && ThirdSkillNumber == 1)
                        ClusterMissileAttack();
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;
        }
    }

    //���� ���� ��ų
    public void NuclearMissileAttack()
    {
        GetComponent<ShipAttackObject>().NuclearMissile1Fire(TargetShip, SikroClassCruiseMissileDamage, "NarihaNuclearMissile1Flagship", "NarihaNuclearMissile1ExplosionFlagship");
    }

    //�Դ� ���� ��ų
    public void ClusterMissileAttack()
    {
        float RandomFire = Random.Range(0, 0.5f);
        StartCoroutine(GetComponent<ShipAttackObject>().TargetMissile1Fire(TargetShip, RandomFire, AmmoDamage, DamageCount, "NarihaClusterMissile1Flagship", "NarihaMultiMissile1ExplosionFlagship"));

        for (int i = 0; i <= GetComponent<FollowShipManager>().ShipList.Count; i++)
        {
            RandomFire = Random.Range(0, 1f);
            ShipAttackObject ShipAttackObject = GetComponent<FollowShipManager>().ShipList[i].GetComponent<ShipAttackObject>();
            StartCoroutine(ShipAttackObject.TargetMissile1Fire(TargetShip, RandomFire, Cysiro47PatriotMissileDamage, DamageCount, "NarihaClusterMissile1Formation", "NarihaMultiMissile1ExplosionFormation"));
        }
    }
}