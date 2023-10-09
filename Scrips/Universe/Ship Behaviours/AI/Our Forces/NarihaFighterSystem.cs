using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarihaFighterSystem : MonoBehaviour
{
    [Header("����� ����Ʈ")]
    public List<GameObject> EngagedFighterList = new List<GameObject>(); //����� ����� ����Ʈ
    public List<GameObject> FighterList = new List<GameObject>(); //����� ����Ʈ

    [Header("����� ����ġ")]
    public bool Flagship = false; //���� ����
    public bool canAttack = false; //���� ���� ����
    public bool FighterEngagement; //����Ⱑ ����� ���������� ���� ����
    public bool OrderTarget = false; //������ �Ҽ��Լ��鿡�� ������ ��� ����� �޾��� ��쿡�� �ߵ�
    public bool FighterUse = false; //��ݱ� ž�� ����
    public bool BomerUse = false; //��ݱ� ž�� ����

    [Header("����� ���� ����")]
    public float FighterRange; //����� ��� ����. ���� �� ��Ÿ����� ���� ������ ����⸦ ����Ѵ�. �� �̻��� ����� ����Ⱑ �ڵ����� ��ȯ�Ѵ�.
    public int FighterDamage; //��ݱ� ������
    public int BomberDamage; //���ݱ� ������
    public float TimeWaveAircraftSortie; //����� ��밡 �ѹ� ����ǰ� ���� ���� ��밡 �����ϱ���� �ɸ��� �ð�
    public float RangeOfAircraftAtOneTime; //��� �ϳ� ��ݸ��� ����� ��� ���� �ð�
    private float AttackStemp; //Update������ �ѹ��� �����ϵ��� ����
    private float WarpStemp;
    private float EmergencyStemp;
    Coroutine launchFighter;

    [Header("���׷��̵� ����")]
    public int FighterDamageUpgrade;
    public int BomerDamageUpgrade;

    [Header("������ ����")]
    public int AmountOfFormation; //����� ����
    public int AircraftsPerFormation; //���� ����� ��
    public int FighterAmount; //��ݱ� ��
    public int BomerAmount; //��ݱ� ��

    [Header("����� ������ ����")]
    public int FighterHitPoint; //��ݱ� ü��
    public int BomerHitPoint; //��ݱ� ü��

    [Header("��ǥ")]
    public GameObject TargetShip; //��ǥ ���� ���
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    public bool TargetOnline; //��ǥ�� �����Ǿ������� ���� ����ġ
    private float TargetMarkTime; //����� ���� ��ũ �ִϸ��̼� �ߵ��� �� �ѹ��� �߻��ϵ��� ����
    private float SearchTime = 2; //������ ����� �˻��ϴ� �ð�

    [Header("����� ������")]
    GameObject FighterPrefab;
    GameObject BomerPrefab;

    [Header("����� ��� ����")]
    public AudioClip AircraftSortieSound;

    void Start()
    {
        if (FighterUse == true)
        {
            for (int i = 0; i < FighterAmount; i++)
            {
                FighterList.Add(ShipAmmoObjectPool.instance.NarihaFighter1Prefab);
            }
        }
        if (BomerUse == true)
        {
            for (int i = 0; i < BomerAmount; i++)
            {
                FighterList.Add(ShipAmmoObjectPool.instance.NarihaBomer1Prefab);
            }
        }
    }

    //���׷��̵� ����
    public void UpgradePatch()
    {
        BomberDamage = BomerDamageUpgrade;
    }

    void Update()
    {
        if (canAttack == true)
        {
            if (OrderTarget == false) //������ ������ ���, �׸��� �������κ��� ������ ����� ���� �ʾ��� ���, �� �Լ����� ���� ����� ���� �ڵ� ����
            {
                if (SearchTime <= 3)
                    SearchTime += Time.deltaTime;

                if (SearchTime >= 3)
                {
                    SearchTime = 0;

                    Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, FighterRange, layerMask); //�ǽð����� AttackEyeRange ���������� ���� ����� ����� �˻�
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
                    if (nearestTarget != null && shortestDistance <= FighterRange) //������ Ÿ���� TargetShip����Ʈ�� �ø���
                    {
                        TargetShip = nearestTarget.gameObject;
                        TargetOnline = true;
                        gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = TargetShip;

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
                        FighterEngagement = false;
                        AttackStemp = 0;
                        gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    }
                }
            }
            else
            {
                if (TargetShip == null)
                {
                    gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
                    OrderTarget = false;
                }
            }
        }
        else
        {
            TargetShip = null;
            TargetOnline = false;
            OrderTarget = false;
            gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
        }

        //��ǥ �Լ��� �����Ǿ��� ���, ���ݱ� ����
        if (TargetOnline == true)
        {
            if (BomerUse == true)
            {
                if (AttackStemp == 0)
                {
                    AttackStemp += Time.deltaTime;
                    WarpStemp = 0;
                    FighterEngagement = true;
                    launchFighter = StartCoroutine(LaunchFighter());
                }
            }
        }

        //������ �ߴܵ� ���, ����⿡�� ��ȯ ��ȣ�� ������ ���� FighterEngagement�� true�� ��ȯ�Ѵ�.
        if (canAttack == false || TargetOnline == false)
        {
            if (launchFighter != null)
                StopCoroutine(launchFighter);
            FighterEngagement = false;
        }
         //������ �������� ���, ����⿡�� ���� �ð� �ڿ� ������ �ڵ��� ������ ����
        if (gameObject.transform.parent.GetComponent<MoveVelocity>().WarpDriveActive == true)
        {
            if (WarpStemp == 0)
            {
                WarpStemp += Time.deltaTime;
                StartCoroutine(AfterWarp());
            }
        }
        if (gameObject.transform.parent.GetComponent<MoveVelocity>().EmergencyWarp == true)
        {
            if (EmergencyStemp == 0)
            {
                EmergencyStemp += Time.deltaTime;
                StartCoroutine(TurnOff());
            }
        }
    }

    //������ �����ϸ� �����鿡�� ���� ���
    IEnumerator AfterWarp()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].GetComponent<NarihaFighterEngagement>().WarpDriveActive = true;
        }
    }

    //������ ���� ��ħ�� ���� �����ϸ� ����� ���� ���
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3);

        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].GetComponent<NarihaFighterEngagement>().Emagancy = true;
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < EngagedFighterList.Count; i++)
        {
            EngagedFighterList[i].gameObject.SetActive(false);
        }
    }

    //����� ����
    IEnumerator LaunchFighter()
    {
        //�� ���ݱ� ������ �̹� ���� ���� �������� ���� ����� �� �ִ� ����⸦ ����Ѵ�.
        int InsideFighters = 0;
        InsideFighters = BomerAmount - EngagedFighterList.Count;

        for (int j = 0; j < AmountOfFormation; j++)
        {
            for (int y = 0; y < AircraftsPerFormation; y++)
            {
                InsideFighters--;
                if (InsideFighters <= 0)
                    break;
                if (EngagedFighterList.Count == FighterList.Count)
                    break;
                if (canAttack == false)
                    break;

                BomerPrefab = ShipAmmoObjectPool.instance.Loader("NarihaBomer1");
                BomerPrefab.transform.position = transform.position;
                BomerPrefab.transform.rotation = transform.rotation;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().MotherCarrier = this.gameObject;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().MyFlagship = this.transform.parent.gameObject.GetComponent<MoveVelocity>().MyFlagship;
                BomerPrefab.GetComponent<NarihaFighterEngagement>().GetInformation(BomerHitPoint, BomberDamage);
                BomerPrefab.GetComponent<NarihaFighterEngagement>().Ingagement = true;
                EngagedFighterList.Add(BomerPrefab);

                yield return new WaitForSeconds(RangeOfAircraftAtOneTime);
            }

            if (EngagedFighterList.Count == FighterList.Count)
                break;
            if (canAttack == false)
                break;

            yield return new WaitForSeconds(TimeWaveAircraftSortie);
        }
    }
}