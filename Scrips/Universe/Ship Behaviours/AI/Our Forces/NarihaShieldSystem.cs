using System.Collections;
using UnityEngine;

public class NarihaShieldSystem : MonoBehaviour
{
    [Header("��� ����")]
    public int ShieldType; //��� ����. 1 = ���� ���, 2 = �̻��� ���, 3 = ������ ���, 4 = ���� ���
    public float RangeOfShield; //��� ����
    public float EyeRange; //��ó�� ���� �����Ǹ� �ڵ� �� ����
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    private float SearchTime = 2; //������ ����� �˻��ϴ� �ð�
    public float ShieldTime; //�� ���� �ð�, �ӽ�
    private bool ShieldCoolTimeOn; //�� ��Ÿ��, �ӽ�
    public float ShieldCoolTime; //�� ��Ÿ��, �ӽ�

    [Header("��� ����ġ")]
    public bool Flagship = false; //���� ����
    public bool canDefence = false; //��� ���� ����
    public bool DefenceOnline = false; //��� ���

    [Header("��ȣ�� ������")]
    public int ShieldHitPoint; //�� ü��
    private int ShieldHitPoint2; //�� ü��(�ΰ���)
    public int ShieldArmorSlorius; //���θ�� ���ݿ� ���� �� ���׷�
    public int ShieldArmorKantakri; //ĭŸũ�� ���ݿ� ���� �� ���׷�

    [Header("��� �ɷ�")]
    public int ShieldEnergy; //�� ������. �������� ������ ���� ����� �� ����
    public int ShieldEnergy2; //�� ������(�ΰ���)
    public int AmountOfInterception; //�ѹ��� ��� ������ ����

    [Header("����Ʈ")]
    public GameObject EnergyBallPrefab; //������ �� ������
    public GameObject ShieldPrefab; //�� ������
    public GameObject ShieldRipplePrefab; //�� Ÿ�� ����Ʈ

    [Header("�ݶ��̴�")]
    public GameObject Collider;
    public GameObject PartCollider1;
    public GameObject PartCollider2;
    public GameObject PartCollider3;
    public GameObject PartCollider4;
    public GameObject PartCollider5;
    public GameObject PartCollider6;

    Coroutine turnOffShield;
    private float ShieldStemp;
    private float ShieldStemp2;

    void Start()
    {
        ShieldPrefab.transform.localScale = new Vector3(RangeOfShield * 100, RangeOfShield * 100, RangeOfShield * 100); //���׷��̵��, �ش� �޼���� ������ ����
    }

    public void ShieldDefenceEffect(Vector2 Transform)
    {
        GameObject Ripple = Instantiate(ShieldRipplePrefab, transform.position, transform.rotation);
        Ripple.GetComponent<ShieldRipples>().ShieldDefenceEffect(Transform);
        Ripple.transform.localScale = new Vector3(RangeOfShield * 100, RangeOfShield * 100, RangeOfShield * 100);
    }

    void Update()
    {
        if (canDefence == true && ShieldCoolTimeOn == false)
        {
            if (SearchTime <= 3)
                SearchTime += Time.deltaTime;

            if (SearchTime >= 3)
            {
                SearchTime = 0;

                Collider2D TargetShips = Physics2D.OverlapCircle(transform.position, EyeRange, layerMask); //�ǽð����� AttackEyeRange ���������� ���� ����� ����� �˻�
                float shortestDistance = Mathf.Infinity;
                Collider2D nearestTarget = null;

                if (TargetShips != null)
                {
                    float DistanceToMonsters = Vector3.Distance(transform.position, TargetShips.transform.position);

                    if (DistanceToMonsters < shortestDistance) //���� ����� Ÿ������ ����
                    {
                        shortestDistance = DistanceToMonsters;
                        nearestTarget = TargetShips;
                    }
                }
                if (nearestTarget != null && shortestDistance <= EyeRange) //������ Ÿ���� TargetShip����Ʈ�� �ø���
                {
                    DefenceOnline = true;
                }
                else
                {
                    DefenceOnline = false;
                    if (ShieldStemp == 0)
                    {
                        ShieldStemp += Time.deltaTime;
                        ShieldStemp2 = 0;
                        turnOffShield = StartCoroutine(TurnOffShield());
                    }
                }
            }

            if (DefenceOnline == true)
            {
                if (ShieldType == 1)
                {
                    if (ShieldStemp2 == 0)
                    {
                        ShieldStemp2 += Time.deltaTime;
                        ShieldStemp = 0;
                        ShieldPrefab.SetActive(true);
                        if (turnOffShield != null)
                            StopCoroutine(turnOffShield);
                        ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", false);
                        EnergyBallPrefab.GetComponent<Animator>().SetBool("Activated, Nariha Shield Ship ball", true);

                        Collider.layer = 0;
                        this.gameObject.layer = 0;
                        PartCollider1.layer = 0;
                        PartCollider2.layer = 0;
                        PartCollider3.layer = 0;
                        PartCollider4.layer = 0;
                        PartCollider5.layer = 0;
                        PartCollider6.layer = 0;

                        ShieldTime = 0;
                        ShieldCoolTime = 0;
                    }
                }
            }
        }
        else
        {
            gameObject.transform.parent.GetComponent<MoveVelocity>().TargetShip = null;
        }

        if (DefenceOnline == true)
        {
            ShieldTime += Time.deltaTime;

            if (ShieldTime >= 20)
            {
                DefenceOnline = false;
                ShieldCoolTimeOn = true;
                ShieldTime = 0;
                ShieldStemp = 0;
                ShieldStemp2 = 0;
                turnOffShield = StartCoroutine(TurnOffShield());
            }
        }
        if (ShieldCoolTimeOn == true)
        {
            ShieldCoolTime += Time.deltaTime;

            if (ShieldCoolTime >= 20)
            {
                ShieldCoolTimeOn = false;
                ShieldCoolTime = 0;
            }
        }
    }

    //������ ������ �ڵ����� �� ����
    IEnumerator TurnOffShield()
    {
        if (ShieldPrefab.activeSelf == true)
        {
            yield return new WaitForSeconds(2);
            ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", true);
            EnergyBallPrefab.GetComponent<Animator>().SetBool("Activated, Nariha Shield Ship ball", false);
            yield return new WaitForSeconds(3);
            ShieldPrefab.GetComponent<Animator>().SetBool("Shield Turn off, Nariha Shield Ship", false);
            ShieldPrefab.SetActive(false);
        }

        Collider.layer = 6;
        this.gameObject.layer = 6;
        PartCollider1.layer = 6;
        PartCollider2.layer = 6;
        PartCollider3.layer = 6;
        PartCollider4.layer = 6;
        PartCollider5.layer = 6;
        PartCollider6.layer = 6;
    }
}