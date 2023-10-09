using System.Collections;
using UnityEngine;

public class OurForceAttackSystem : MonoBehaviour
{
    Animator anim;

    [Header("���� ����")]
    public int CannonType; //���� ����. 1 = �������� ����, 2 = �ʰ����� ����, 3 = ���� �̻���, 4 = ��Ƽ �̻���, 5 = ��ü�� �ʰ����� ��������
    public int CannonStyle; //���� �Ÿ� ����. 1 = ��Ÿ� ����, 2 = �ܰŸ� ����

    [Header("���� ���� ����ġ")]
    public bool Flagship = false; //���� ����
    public bool canAttack = false; //���� ���� ����
    public bool RangeAttack = false; //��Ÿ����� ���� ������ ����
    public bool OrderTarget = false; //������ �Ҽ��Լ��鿡�� ������ ��� ����� �޾��� ��쿡�� �ߵ�

    [Header("���� ������ ������")]
    public GameObject SilenceSist;
    public GameObject Catroy;

    [Header("���� ���� ����")]
    public int AmmoDamage; //���� ������
    public float FireRate; //�ð��� 1ȸ ����
    private float RandomFire; //����ӵ��� ����ȭ
    public float AttackTime; //FireRate ��Ÿ�� ����
    public int DamageCount; //�������� �ѹ��� �� �� �ִ����� ���� ����

    [Header("���׷��̵� ���� ���� ����")]
    public int AmmoDamageUpgrade;
    public float FireRateUpgrade;

    [Header("���� ��Ÿ�")]
    public float AttackEyeRange; //���ݰ����� ��Ÿ�

    [Header("��ǥ �� ���� ��ġ")]
    public GameObject TargetShip; //��ǥ ���� ���
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    public Transform TurretLocation; //���� ��ġ
    public bool TargetOnline; //��ǥ�� �����Ǿ������� ���� ����ġ
    private float TargetMarkTime; //����� ���� ��ũ �ִϸ��̼� �ߵ��� �� �ѹ��� �߻��ϵ��� ����
    private float SearchTime = 2; //������ ����� �˻��ϴ� �ð�

    [Header("���� �߻� ����Ʈ")]
    public GameObject NarihaFireEffect; //������ ���Ϸ��� �ý�Ʈ ���� ���� ������
    public GameObject NarihaFireEffect2; //������ �ʰ����� ������
    public GameObject MissileFireEffect; //������ �̻��� �߻� ����
    GameObject NarihaArtillery1;
    GameObject NarihaOverJumpArtillery1;
    GameObject NarihaMissile1;

    public AudioClip SilentSistFireSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackTime = FireRate * 0.8f;
        CannonReinput();
    }

    public void CannonReinput()
    {
        //���� ��ž ����
        if (CannonStyle == 1)
            CannonType = 1;
        else if (CannonStyle == 2)
            CannonType = 2;
        else if (CannonStyle == 3)
        {
            int RandomCannon = Random.Range(0, 4);

            if (RandomCannon == 0)
                CannonType = 1;
            else if (RandomCannon == 1)
                CannonType = 2;
            else if (RandomCannon == 2)
                CannonType = 3;
            else if (RandomCannon == 3)
                CannonType = 4;
        }

        if (CannonType == 1) //���Ϸ��� �ý�Ʈ(���� ����)
        {
            SilenceSist.SetActive(true);
            Catroy.SetActive(false);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipSilenceSistDamage / 2;
                FireRate = 6;
                DamageCount = 4;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationSilenceSistDamage / 2;
                FireRate = 6;
                DamageCount = 2;
            }
        }
        else if (CannonType == 2) //�ʰ�����(�ܹ� ����)
        {
            SilenceSist.SetActive(true);
            Catroy.SetActive(false);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipOverJumpDamage / 2;
                FireRate = 6;
                DamageCount = 1;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationOverJumpDamage / 2;
                FireRate = 8;
                DamageCount = 1;
            }
        }
        else if (CannonType == 3) //���� ����-345 �ܹ� �̻���
        {
            SilenceSist.SetActive(false);
            Catroy.SetActive(true);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipSadLilly345Damage / 2;
                FireRate = 6;
                DamageCount = 1;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationSadLilly345Damage / 2;
                FireRate = 6;
                DamageCount = 1;
            }
        }
        else if (CannonType == 4) //��Ÿ �ϵ�-42 �Ҹ���Ʈ ��Ƽ �̻���
        {
            SilenceSist.SetActive(false);
            Catroy.SetActive(true);
            if (Flagship == true)
            {
                AmmoDamage = UpgradeDataSystem.instance.FlagshipDeltaNeedle42HalistDamage / 2;
                FireRate = 6;
                DamageCount = 5;
            }
            else
            {
                AmmoDamage = UpgradeDataSystem.instance.FormationDeltaNeedle42HalistDamage / 2;
                FireRate = 6;
                DamageCount = 3;
            }
        }
    }

    void Update()
    {
        //����� ������ ���, ��� ����
        if (canAttack == true)
        {
            //AttackTime ���ڸ� FireRate�� ���� �ʴ� ���ǿ��� �ǽð����� ����
            if (AttackTime <= FireRate + RandomFire && TargetShip != null)
                AttackTime += Time.deltaTime;

            if (TargetShip != null) //Ÿ���� ���� ��쿡�� ���� �۵�
            {
                Vector3 dir = (TargetShip.transform.position - transform.position).normalized; //���� ȸ��
                transform.right = Vector3.Lerp(transform.right, dir, 3 * Time.deltaTime);
            }

            if (OrderTarget == false) //������ ������ ���, �׸��� �������κ��� ������ ����� ���� �ʾ��� ���, �� �Լ����� ���� ����� ���� �ڵ� ����
            {
                if (SearchTime <= 3)
                    SearchTime += Time.deltaTime;

                if (SearchTime >= 3)
                {
                    SearchTime = 0;
                    Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, AttackEyeRange, layerMask); //�ǽð����� AttackEyeRange ���������� ���� ����� ����� �˻�
                    float shortestDistance = Mathf.Infinity;
                    Collider2D nearestTarget = null;

                    if (TargetShips != null)
                    {
                        float DistanceToMonsters = Vector3.Distance(transform.position, TargetShips.transform.position);

                        if (DistanceToMonsters < shortestDistance) //���� ����� Ÿ������ ����
                        {
                            shortestDistance = DistanceToMonsters;
                            nearestTarget = TargetShips;
                            TargetMarkTime = 0;
                        }
                    }
                    if (nearestTarget != null && shortestDistance <= AttackEyeRange) //������ Ÿ���� TargetShip����Ʈ�� �ø���
                    {
                        TargetShip = nearestTarget.gameObject;
                        TargetOnline = true;
                        gameObject.transform.parent.GetComponent<OurForceShipBehavior>().TargetShip = TargetShip;
                    }
                    else
                    {
                        TargetShip = null;
                        TargetOnline = false;
                    }
                }
            }
            else
            {
                if (TargetShip.activeSelf == false || TargetShip == null)
                {
                    gameObject.transform.parent.GetComponent<OurForceShipBehavior>().TargetShip = null;
                    OrderTarget = false;
                }
            }

            if (RangeAttack == true) //��� ������ ��Ÿ��� ����� ��� ����
            {
                //�ʴ� ���� �߻�
                if (AttackTime >= FireRate + RandomFire)
                {
                    AttackTime = 0;
                    RandomFire = Random.Range(0, 0.15f);

                    if (CannonType == 1)
                        StartCoroutine(FireNarihaQuantum1());
                    else if (CannonType == 2)
                        StartCoroutine(FireNarihaOverJump1());
                    else if (CannonType == 3)
                        StartCoroutine(FireNarihaSingleMissile1());
                    else if (CannonType == 4)
                        StartCoroutine(FireNarihaMultiMissile1());
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;
            OrderTarget = false;
            gameObject.transform.parent.GetComponent<OurForceShipBehavior>().TargetShip = null;
        }
    }

    //������ ���Ϸ��� �ý�Ʈ ���� ���� ������(��Ƽ�� ����)
    IEnumerator FireNarihaQuantum1()
    {
        NarihaFireEffect.SetActive(true);


        for (int i = 0; i <= DamageCount; i++)
        {
            if (Flagship == true)
            {
                UniverseSoundManager.instance.UniverseSoundPlayMaster("Nariha Silent Sist Fire Sound", SilentSistFireSound, this.transform.position);
                SilenceSistCannonSystem1Flagship();
                if (i < DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Flagship)", 1);
                    yield return new WaitForSeconds(0.2f);
                    anim.SetFloat("Fire Cannon(Flagship)", 0);
                    yield return new WaitForSeconds(0.05f);
                }
                else if (i == DamageCount)
                {
                    anim.SetFloat("Fire Cannon(Flagship)", 2);
                    yield return new WaitForSeconds(1.16f);
                    anim.SetFloat("Fire Cannon(Flagship)", 0);
                }
            }
            else if (Flagship == false)
            {
                UniverseSoundManager.instance.UniverseSoundPlayMaster("Nariha Silent Sist Fire Sound", SilentSistFireSound, this.transform.position);
                SilenceSistCannonSystem1Formation();
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
        }

        yield return new WaitForSeconds(0.25f);
        NarihaFireEffect.SetActive(false);
        yield return new WaitForSeconds(RandomFire);
    }

    //������ �ʰ����� ������(�ܹ� ����)
    IEnumerator FireNarihaOverJump1()
    {
        NarihaFireEffect2.SetActive(true);
        if (Flagship == true)
            OverJump1CannonSystem1Flagship();
        else
            OverJump1CannonSystem1Formation();

        anim.SetFloat("Fire Cannon(Flagship)", 2);
        yield return new WaitForSeconds(1.16f);
        anim.SetFloat("Fire Cannon(Flagship)", 0);
        NarihaFireEffect2.SetActive(false);
        yield return new WaitForSeconds(RandomFire);
    }

    //������ ���� �̻��� �߻� ����
    IEnumerator FireNarihaSingleMissile1()
    {
        if (Flagship == true)
            SingleMissileSystem1Flagship();
        else
            SingleMissileSystem1Formation();
        StartCoroutine(FireNarihaSingleMissile1Animation());
        MissileFireEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    //������ ��Ƽ �̻��� �߻� ����
    IEnumerator FireNarihaMultiMissile1()
    {
        for (int i = 0; i < DamageCount; i++)
        {
            if (Flagship == true)
                MultiMissile1System1Flagship();
            else
                MultiMissile1System1Formation();
            StartCoroutine(FireNarihaSingleMissile1Animation());
            Instantiate(MissileFireEffect, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);
        MissileFireEffect.SetActive(false);
    }

    IEnumerator FireNarihaSingleMissile1Animation()
    {
        if (Flagship == true)
        {
            anim.SetFloat("Fire Cannon(Flagship)", 3);
            yield return new WaitForSeconds(1);
            anim.SetFloat("Fire Cannon(Flagship)", 0);
        }
        else if (Flagship == false)
        {
            anim.SetFloat("Fire Cannon(Formation)", 3);
            yield return new WaitForSeconds(1);
            anim.SetFloat("Fire Cannon(Formation)", 0);
        }
    }

    //������ ���Ϸ��� �ý�Ʈ ���� ���� ������(����) ������ ����ü ����
    void SilenceSistCannonSystem1Flagship()
    {
        NarihaArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaSilenceSistArtillery1Flagship");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSilenceSistArtillery1ExplosionFlagship", "NarihaSilenceSistArtillery1FlagshipDelete", "NarihaSilenceSistArtillery1ExplosionFlagshipDelete");
    }
    //������ ���Ϸ��� �ý�Ʈ ���� ���� ������(�����) ������ ����ü ����
    void SilenceSistCannonSystem1Formation()
    {
        NarihaArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaSilenceSistArtillery1Formation");
        NarihaArtillery1.transform.position = TurretLocation.transform.position;
        NarihaArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSilenceSistArtillery1ExplosionFormation", "NarihaSilenceSistArtillery1FormationDelete", "NarihaSilenceSistArtillery1ExplosionFormationDelete");
    }

    //������ �ʰ� ���� ������(����) ������ ����ü ����
    void OverJump1CannonSystem1Flagship()
    {
        NarihaOverJumpArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaOverJumpArtillery1Flagship");
        NarihaOverJumpArtillery1.transform.position = TurretLocation.transform.position;
        NarihaOverJumpArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaOverJumpArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaOverJumpArtillery1ExplosionFlagship", "NarihaOverJumpArtillery1FlagshipDelete", "NarihaOverJumpArtillery1ExplosionFlagshipDelete");
    }
    //������ �ʰ� ���� ������(�����) ������ ����ü ����
    void OverJump1CannonSystem1Formation()
    {
        NarihaOverJumpArtillery1 = ShipAmmoObjectPool.instance.Loader("NarihaOverJumpArtillery1Formation");
        NarihaOverJumpArtillery1.transform.position = TurretLocation.transform.position;
        NarihaOverJumpArtillery1.transform.rotation = TurretLocation.transform.rotation;
        NarihaOverJumpArtillery1.GetComponent<CannonMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaOverJumpArtillery1ExplosionFormation", "NarihaOverJumpArtillery1FormationDelete", "NarihaOverJumpArtillery1ExplosionFormationDelete");
    }

    //������ ���� �̻���(����) ������ ����ü ����
    void SingleMissileSystem1Flagship()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaSingleMissile1Flagship");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSingleMissile1ExplosionFlagship");
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetRotation();
    }
    //������ ���� �̻���(�����) ������ ����ü ����
    void SingleMissileSystem1Formation()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaSingleMissile1Formation");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        NarihaMissile1.transform.rotation = TurretLocation.transform.rotation;
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaSingleMissile1ExplosionFormation");
        NarihaMissile1.GetComponent<NarihaMissileMovement>().SetRotation();
    }

    //������ ��Ƽ�̻���(����) ������ ����ü ����
    void MultiMissile1System1Flagship()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaMultiMissile1Flagship");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        int RandomRadius = Random.Range(-40, 40);
        NarihaMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z - 90 + RandomRadius);
        NarihaMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaMultiMissile1ExplosionFlagship");
    }
    //������ ��Ƽ�̻���(�����) ������ ����ü ����
    void MultiMissile1System1Formation()
    {
        NarihaMissile1 = ShipAmmoObjectPool.instance.Loader("NarihaMultiMissile1Formation");
        NarihaMissile1.transform.position = TurretLocation.transform.position;
        int RandomRadius = Random.Range(-40, 40);
        NarihaMissile1.transform.eulerAngles = new Vector3(TurretLocation.transform.eulerAngles.x, TurretLocation.transform.eulerAngles.y, TurretLocation.transform.eulerAngles.z - 90 + RandomRadius);
        NarihaMissile1.GetComponent<MissileMovement>().SetDamage(AmmoDamage, TargetShip, "NarihaMultiMissile1ExplosionFormation");
    }
}