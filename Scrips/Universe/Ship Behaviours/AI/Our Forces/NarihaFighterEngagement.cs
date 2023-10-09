using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarihaFighterEngagement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    public GameObject MotherCarrier;
    public GameObject MyFlagship;
    public string DeleteName;

    [Header("����� ���� ����")]
    public GameObject TargetShip; //��ǥ ���� ���
    public float FlightSpeed; //���� �ӵ�
    public float WarpSpeed; //���� �ӵ�
    public int FireDamage; //��������
    public int HitPoint; //ü��
    private Vector3 endposition;

    [Header("����� ���� ����")]
    public float EngageRange; //���� �ּ� �Ÿ�
    public float RateOfFire; //�ð��� ����
    private int FireEngage; //���� ���
    private float RateOfFireTime;
    private float FlightTemp;
    private float LandingStemp;
    Coroutine flightTargetAround;
    Coroutine flightMove;

    [Header("����� ���� ����")]
    public bool isFight = false; //���� ����
    public bool Ingagement = false; //���� ����
    public bool WarpDriveActive = false; //���� ����. �̴� ������ ���� �׹��� ���������Ƿ�, ������ ���󰣴�.
    public bool Emagancy = false; //������ �ı��Ǿ� ����
    private float WarpdestinationStemp;
    Vector3 MotherCarrierDestination; //���� ���� ���

    [Header("����� ��Ÿ ����")]
    public bool WarpActivate; //��������. �Դ밡 ������ �����ϸ� ������ ���߰� �Լ����� ���� ��ȯ�Ѵ�.
    public GameObject FirePrefab;
    [SerializeField] LayerMask layerMask; //� ��ǥ ���̾ Ư���� ���ΰ�
    private float SearchTime = 2; //������ ����� �˻��ϴ� �ð�

    GameObject NarihaFighterAmmo;
    GameObject NarihabomerMissile;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        isFight = false;
        FireEngage = 0;
        RateOfFireTime = 0;
        FlightTemp = 0;
        LandingStemp = 0;
    }

    public void GetInformation(int HP, int Damage)
    {
        HitPoint = HP;
        FireDamage = Damage;
        StartCoroutine(StartEngine());
    }

    //ù ����
    IEnumerator StartEngine()
    {
        yield return new WaitForSeconds(0.25f);
        RateOfFireTime = RateOfFire;
        FireEngage = 1;
        isFight = true;
    }

    void Update()
    {
        if (MotherCarrier != null)
        {
            TargetShip = MotherCarrier.GetComponent<NarihaFighterSystem>().TargetShip;

            if (Ingagement == true && TargetShip != null)
            {
                float distanceFromTarget = Vector2.Distance(TargetShip.transform.position, transform.position);

                if (isFight == true)
                {
                    //������ �̹� �ѹ� �� �����̹Ƿ�, ��ǥ ��� �ֺ��� ��ȸ
                    if (RateOfFireTime < RateOfFire)
                    {
                        RateOfFireTime += Time.deltaTime;
                        if (FlightTemp == 0)
                        {
                            FlightTemp += Time.deltaTime;
                            flightTargetAround = StartCoroutine(FlightTargetAround());
                        }
                    }
                    //���� ������ �� �ѹ� �����ϱ� ���� ����
                    else
                    {
                        FireEngage = 1;

                        if (distanceFromTarget >= EngageRange)
                        {
                            if (flightTargetAround != null)
                                StopCoroutine(flightTargetAround);
                            transform.position += transform.up * FlightSpeed * Time.deltaTime;
                            Vector3 dir = (TargetShip.transform.position - transform.position).normalized;
                            transform.up = Vector3.Lerp(transform.up, dir, 4 * Time.deltaTime);
                        }
                        else //����
                        {
                            if (FireEngage == 1)
                            {
                                FireEngage = 0;
                                RateOfFireTime = 0;
                                FirePrefab.SetActive(true);
                                NarihabomerMissile = ShipAmmoObjectPool.instance.Loader("NarihaBomer1Artillery1");
                                NarihabomerMissile.transform.position = transform.position;
                                NarihabomerMissile.transform.rotation = transform.rotation;
                                NarihabomerMissile.GetComponent<CannonMovement>().SetDamage(FireDamage, TargetShip, "NarihaBomer1Artillery1Explosion", "NarihaBomer1Artillery1Delete", "NarihaBomer1Artillery1ExplosionDelete");
                                FlightTemp = 0;
                                Invoke("EffectTurnOff", 0.85f);
                            }
                        }
                    }
                }
                else
                {
                    transform.position += transform.up * FlightSpeed * Time.deltaTime;
                }
            }

            //��ǥ���� ���ų� �Լ����κ��� ��ȯ����� ���� ���
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == false || TargetShip == null)
            {
                if (WarpDriveActive == false) //�� ���� �߿����� �ǽ��Ѵ�.
                {
                    if (flightTargetAround != null)
                        StopCoroutine(flightTargetAround);
                    if (flightMove != null)
                        StopCoroutine(flightMove);

                    if (Vector2.Distance(MotherCarrier.transform.position, transform.position) > 1)
                    {
                        transform.position += transform.up * FlightSpeed * Time.deltaTime;
                        Vector3 dir = (MotherCarrier.transform.position - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, 10 * Time.deltaTime);
                    }
                    if (Vector2.Distance(MotherCarrier.transform.position, transform.position) <= 1)
                    {
                        if (LandingStemp == 0)
                        {
                            LandingStemp += Time.deltaTime;
                            MotherCarrier.GetComponent<NarihaFighterSystem>().EngagedFighterList.Remove(gameObject);
                            ShipAmmoObjectPool.instance.Deleter(DeleteName);
                            this.gameObject.SetActive(false);
                        }
                    }
                }
            }

            //������ ���� ���� ���, ���� ������ ���� ���󰡱�
            if (WarpDriveActive == true) //���� ���� �������� ���� ����
            {
                if (WarpdestinationStemp == 0)
                {
                    WarpdestinationStemp += Time.deltaTime;
                    TargetShip = null;
                    isFight = false;
                    MotherCarrierDestination = new Vector3(MotherCarrier.gameObject.transform.parent.GetComponent<MoveVelocity>().WarpformationIndex.position.x + Random.Range(1, 5),
                        MotherCarrier.gameObject.transform.parent.GetComponent<MoveVelocity>().WarpformationIndex.position.y + Random.Range(1, 5), 0);
                }

                transform.position = Vector3.MoveTowards(transform.position, MotherCarrierDestination, WarpSpeed * Time.deltaTime);
                Vector3 DirectionAtFormation = (MotherCarrierDestination - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, targetRotation, 2 * Time.deltaTime);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) > 50f)
                    anim.SetFloat("Warp, Slorius Flagship", 1);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 15f)
                    anim.SetFloat("Warp, Slorius Flagship", 2);

                if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 1)
                {
                    WarpDriveActive = false;
                    WarpdestinationStemp = 0;
                }
            }
        }
        else //������ �ı��Ǿ��� ���, �ٸ� ������ �����Ѵ�.
        {
            if (WarpdestinationStemp == 0)
            {
                WarpdestinationStemp += Time.deltaTime;
                isFight = false;
                MotherCarrierDestination = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
                StartCoroutine(EmagancyFighter());
            }

            if (Emagancy == false) //���� ���差���� �����ð� ���� ���� ������ �ִ� ����� ������ ��, ����
            {
                if (MyFlagship != null)
                    TargetShip = MyFlagship.GetComponent<FlagshipAttackSkill>().TargetShip;
                else
                    Emagancy = true;
            }
        }

        if (Emagancy == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, MotherCarrierDestination, WarpSpeed * Time.deltaTime);
            Vector3 DirectionAtFormation = (MotherCarrierDestination - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
            rb2d.transform.rotation = Quaternion.Lerp(rb2d.transform.rotation, targetRotation, 2 * Time.deltaTime);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) > 50f)
                anim.SetFloat("Warp, Slorius Flagship", 1);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 15f)
                anim.SetFloat("Warp, Slorius Flagship", 2);

            if (Vector3.Distance(MotherCarrierDestination, transform.position) <= 1)
            {
                WarpDriveActive = false;
                Emagancy = false;
                WarpdestinationStemp = 0;
                ShipAmmoObjectPool.instance.Deleter(DeleteName);
                this.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator EmagancyFighter()
    {
        float Time = Random.Range(5, 10);
        yield return new WaitForSeconds(Time);
        Emagancy = true;
    }

    IEnumerator FlightTargetAround()
    {
        if (MotherCarrier != null)
        {
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
            {
                while (true)
                {
                    float RandomMovement = Random.Range(-4, 4);
                    float RandomTime = Random.Range(0.25f, 1);

                    if (MotherCarrier != null)
                    {
                        if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
                            endposition = new Vector3(TargetShip.transform.position.x + RandomMovement, TargetShip.transform.position.y + RandomMovement, transform.position.z);
                    }

                    if (flightMove != null)
                        StopCoroutine(flightMove);

                    flightMove = StartCoroutine(FlightMove(rb2d, FlightSpeed));
                    yield return new WaitForSeconds(RandomTime);
                }
            }
        }
    }

    IEnumerator FlightMove(Rigidbody2D rigidbodyToMove, float speed)
    {
        if (MotherCarrier != null)
        {
            if (MotherCarrier.GetComponent<NarihaFighterSystem>().canAttack == true && TargetShip != null)
            {
                float remainingDistance = (transform.position - endposition).sqrMagnitude;

                while (remainingDistance > float.Epsilon)
                {
                    //�̵�
                    if (rigidbodyToMove != null)
                    {
                        transform.position += transform.up * FlightSpeed * Time.deltaTime;
                        Vector3 dir = (endposition - transform.position).normalized;
                        transform.up = Vector3.Lerp(transform.up, dir, 4 * Time.deltaTime);
                        remainingDistance = (transform.position - endposition).sqrMagnitude;
                    }
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }

    void EffectTurnOff()
    {
        FirePrefab.SetActive(false);
    }
}