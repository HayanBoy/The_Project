using System.Collections;
using UnityEngine;

public class OurForceShipBehavior : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rb2D;

    [Header("�Դ� ���� �� ���")]
    public bool FlagShip; //�������� �Ǵ��ϱ� ���� ����
    public bool FormationOn; //����� Ȱ��ȭ ����
    public bool FlagshipFirstDestroy; //������ ���� ��ħ�Ǿ����� ����

    [Header("�Լ� ��ġ ����")]
    public Transform FormationIndex; //�Ҽ� �Լ��� �� ������ �ڸ�����
    public Transform WarpformationIndex; //FormationIndex�� ���� ������
    Vector3 DirectionAtFormation; //FormationIndex�� ���� ���Ǵ� ��� ���� ���� ����
    Vector3 DirectionAtMoving; //DestinationArea�� ���� ���Ǵ� ������ȯ�� ���� ����
    public Vector3 DestinationArea; //������ ���� ����
    public float RotateSpeed; //������ȯ �ӵ�
    public float MoveOrderStemp; //�Դ��� ��ȯ ��, ������ �ܵ����� ������ ��, ������ ���� ���������� ������ ȸ������ �� �ѹ��� �ҷ�����

    [Header("���ӵ� �� ���ӵ�")]
    public float BoostSpeed; //���� �ӵ�
    public float WarpSpeed; //���� �ӵ�
    public float CurrentSpeed; //���� �ӵ�
    private float AcceleratorSpeed; //���ӵ�
    private float AccelAndDecelRange = 2f; //���� �� ������ ���Ǵ� �Ÿ�
    float RangeBetweenStartAndDestination; //���۰� ���������� �Ÿ�. �Լ��� ���� �� ���ӰŸ��� Ȯ���ϱ� ����

    [Header("Ȱ�� ����")]
    public GameObject MyFlagship;
    public GameObject MyPlanet;
    public bool PlanetWalk; //�༺�� �ֵ��� ����

    [Header("����Լ� ���� ���� ��Ÿ�")]
    public GameObject TargetShip; //���� ���
    private float distanceFromTarget; //���� ������ �Ÿ�
    public float MinDamageRange; //���� �ּ� ��Ÿ�. �ش� ��Ÿ����� ���� ��� �������� ���ҵȴ�.
    public float MaxDamageRange; //���� �ִ� ��Ÿ�. �ش� ��Ÿ����� ���� ��� �������� ���ҵȴ�.
    private float SearchTime = 3;

    [Header("���� ������")]
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject Turret5;
    public GameObject Turret6;

    [Header("����")]
    Vector3 MoveDir;
    public float DisctanceFromTarget;
    public bool WarpDriveReady; //���� �غ�
    public bool WarpDriveActive; //���� ���� ����
    private float WarpCompleteTemp; //Update������ �� �ѹ��� �����ϵ��� ����
    private float WarpGeneratorTemp; //Update������ �� �ѹ��� �����ϵ��� ����
    public GameObject WarpGenerator; //���� �߻� ����Ʈ
    public GameObject WarpArriveEffect; //���� ���� ����Ʈ
    public GameObject WarpBoosterReady; //���� �غ� �ν���
    public GameObject WarpBooster; //���� �ν���
    public GameObject Booster;

    private float MoveStemp;
    private Vector3 endposition;
    Coroutine moveTargetAround;
    Coroutine move;

    //���� ����
    public void WarpSpeedUp(bool boolean)
    {
        WarpDriveActive = boolean;
        CurrentSpeed = WarpSpeed;
        WarpCompleteTemp = 0;
        WarpGeneratorTemp = 0;
        Booster.SetActive(false);
        WarpBooster.SetActive(true);
        WarpBoosterReady.SetActive(false);
        anim.SetFloat("Warp, Slorius Flagship", 1);
    }

    private void Awake()
    {
        DestinationArea = transform.position;
    }

    private void OnEnable()
    {
        if (FlagshipFirstDestroy == true)
            FlagshipFirstDestroy = false;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        AcceleratorSpeed = BoostSpeed / (1250 / BoostSpeed); //1250�� ���ӵ� ���갪�̴�. �ִ�ӵ��� ���缭 ���ӵ��� ����ȴ�.
    }

    //���� ���� �ޱ�
    public void SetVelocity(Vector3 MovePositionDirection)
    {
        DestinationArea = MovePositionDirection;
        RangeBetweenStartAndDestination = Vector3.Distance(MovePositionDirection, transform.position);
        DirectionAtMoving = (MovePositionDirection - transform.position).normalized; //���� ��ȯ�� ���������� ���
    }

    //�������� ���� ���� �ִϸ��̼� �ʱ�ȭ
    IEnumerator WarpComplete()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetFloat("Warp, Slorius Flagship", 0);
    }

    private void FixedUpdate()
    {
        if (FlagShip == true)
        {
            if (WarpDriveActive == true)
            {
                //�Լ� �⺻ �̵�
                MoveDir = (DestinationArea - transform.position).normalized;
                DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

                if (DisctanceFromTarget <= 200 && DisctanceFromTarget >= 100)
                {
                    if (WarpGeneratorTemp == 0)
                    {
                        WarpGeneratorTemp += Time.deltaTime;
                        Instantiate(WarpGenerator, DestinationArea, Quaternion.identity);
                        WarpBooster.SetActive(false);
                    }
                }
                if (DisctanceFromTarget <= 15f)
                    anim.SetFloat("Warp, Slorius Flagship", 2);

                if (DisctanceFromTarget <= 2.5f)
                    CurrentSpeed = WarpSpeed * 0.1f;

                if (DisctanceFromTarget <= 1) //�������� ������ ���, �Լ� �̵� ����
                {
                    //Debug.Log("�� ���� ����");
                    WarpDriveActive = false;
                    MoveDir = Vector3.zero;
                    CurrentSpeed = 0;
                    WarpBooster.SetActive(false);
                    Booster.SetActive(true);

                    if (WarpCompleteTemp == 0)
                    {
                        WarpCompleteTemp += Time.deltaTime;

                        Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                        StartCoroutine(WarpComplete());

                        Turret1.GetComponent<OurForceAttackSystem>().canAttack = true;
                        Turret2.GetComponent<OurForceAttackSystem>().canAttack = true;
                        Turret3.GetComponent<OurForceAttackSystem>().canAttack = true;
                        Turret4.GetComponent<OurForceAttackSystem>().canAttack = true;
                        Turret5.GetComponent<OurForceAttackSystem>().canAttack = true;
                        Turret6.GetComponent<OurForceAttackSystem>().canAttack = true;
                    }
                }
            }
        }
    }

    void Update()
    {
        //Ÿ���� ������ ������ ��Ÿ� �� ��� �ߵ�
        if (TargetShip != null)
        {
            if (SearchTime <= 3)
                SearchTime += Time.deltaTime;

            if (SearchTime >= 3)
            {
                SearchTime = 0;

                distanceFromTarget = Vector2.Distance(TargetShip.transform.position, transform.position);

                if (distanceFromTarget > MaxDamageRange)
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret3.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret4.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret5.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret6.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                    }
                    else
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = false;
                    }
                }
                else if (distanceFromTarget < MinDamageRange)
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret3.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret4.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret5.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret6.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                    }
                    else
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                    }
                }
                else if (distanceFromTarget < MaxDamageRange && distanceFromTarget > MinDamageRange)
                {
                    if (FlagShip == true)
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret3.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret4.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret5.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret6.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                    }
                    else
                    {
                        Turret1.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                        Turret2.GetComponent<OurForceAttackSystem>().RangeAttack = true;
                    }
                }
            }
        }

        //���� �� �Ϲ� �Լ� �� �̵� ���
        if (FlagShip == false) //�Ҽ� �Լ�
        {
            if (FormationOn == false) //�Դ����� �̰��� ���� ��� �Ҽ��Լ����� ������ �������� ������ �� �ִ�.
            {
                if (WarpDriveActive == false)
                {
                    if (TargetShip == null) //��������Ȳ������ ������
                    {
                        if (MyPlanet != null) //�༺ �ֵ�
                        {
                            Vector3 dir1 = (MyPlanet.transform.position - transform.position).normalized;
                            float DistanceFromPlanet = Vector2.Distance(MyPlanet.transform.position, transform.position);

                            if (DistanceFromPlanet > 110) //�༺ �ִ� �ֵ� �Ÿ��� ����� ����
                            {
                                MoveStemp = 0;
                                if (moveTargetAround != null)
                                    StopCoroutine(moveTargetAround);
                                if (move != null)
                                    StopCoroutine(move);

                                Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                                rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(MyPlanet.transform.position.x, MyPlanet.transform.position.y), CurrentSpeed * 2 * Time.deltaTime);

                                if (CurrentSpeed <= BoostSpeed)
                                    CurrentSpeed += AcceleratorSpeed;
                            }
                            else if (DistanceFromPlanet < 110)
                            {
                                if (MoveStemp == 0)
                                {
                                    MoveStemp += Time.deltaTime;
                                    moveTargetAround = StartCoroutine(MoveTargetAround(false));
                                }

                                if (CurrentSpeed <= BoostSpeed)
                                    CurrentSpeed += AcceleratorSpeed;
                            }
                        }
                        else //�������� ���ؼ� ������
                        {
                            transform.position = Vector2.MoveTowards(transform.position, DestinationArea, CurrentSpeed * Time.deltaTime);
                            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                            rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

                            if (DisctanceFromTarget > MaxDamageRange)
                            {
                                if (CurrentSpeed <= BoostSpeed)
                                    CurrentSpeed += AcceleratorSpeed;
                            }
                        }
                    }
                    else
                    {
                        Vector3 dir1 = (TargetShip.transform.position - transform.position).normalized;
                        Vector3 dir2 = (TargetShip.transform.position + transform.position).normalized;

                        if (distanceFromTarget > MaxDamageRange) //Ÿ���� �ִ� ��Ÿ��� ����� ����
                        {
                            MoveStemp = 0;
                            if (moveTargetAround != null)
                                StopCoroutine(moveTargetAround);
                            if (move != null)
                                StopCoroutine(move);

                            Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                            rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                            transform.position = Vector2.MoveTowards(transform.position, TargetShip.transform.position, CurrentSpeed * Time.deltaTime);

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                        else if (distanceFromTarget < MaxDamageRange)
                        {
                            if (MoveStemp == 0)
                            {
                                MoveStemp += Time.deltaTime;
                                moveTargetAround = StartCoroutine(MoveTargetAround(true));
                            }

                            if (CurrentSpeed <= BoostSpeed)
                                CurrentSpeed += AcceleratorSpeed;
                        }
                    }
                }
                else //����� �ܵ� ����
                {
                    transform.position = Vector2.MoveTowards(transform.position, DestinationArea, CurrentSpeed * Time.deltaTime);
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                    if (DisctanceFromTarget <= 200 && DisctanceFromTarget >= 100)
                    {
                        if (WarpGeneratorTemp == 0)
                        {
                            WarpGeneratorTemp += Time.deltaTime;
                            Instantiate(WarpGenerator, DestinationArea, Quaternion.identity);
                            WarpBooster.SetActive(false);
                        }
                    }
                    if (DisctanceFromTarget > 50f)
                        anim.SetFloat("Warp, Slorius Flagship", 1);

                    if (DisctanceFromTarget <= 15f)
                        anim.SetFloat("Warp, Slorius Flagship", 2);

                    if (DisctanceFromTarget <= 2.5f)
                        CurrentSpeed = WarpSpeed * 0.1f;

                    if (DisctanceFromTarget <= 0.5f)
                    {
                        WarpDriveActive = false;
                        CurrentSpeed = 0;

                        if (WarpCompleteTemp == 0)
                        {
                            WarpCompleteTemp += Time.deltaTime;
                            Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                            Booster.SetActive(true);
                            StartCoroutine(WarpComplete());
                            Turret1.GetComponent<OurForceAttackSystem>().canAttack = true;
                            Turret2.GetComponent<OurForceAttackSystem>().canAttack = true;
                        }
                    }
                }
            }
            else //�Դ����� ���� ���� ��� ���� �ֺ����� ������ �迭�� ���� �����δ�.
            {
                if (WarpDriveActive == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, FormationIndex.position, BoostSpeed * 1.5f * Time.deltaTime);

                    if (Vector3.Distance(FormationIndex.position, transform.position) > 0.25f) //�Լ��� �Դ������� �̵����� ���¿��� �������� ���� ����
                    {
                        DirectionAtFormation = (FormationIndex.position - transform.position).normalized;
                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtFormation);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                    }
                    else //�Լ��� �Դ������� �̵��� �Ϸ��� ��, �����ϴ� ����
                    {
                        CurrentSpeed = 0;
                        rb2D.transform.rotation = Quaternion.Lerp(transform.rotation, MyFlagship.transform.rotation, 0.06f);
                    }
                }
                else //���� ���� �������� ���� ����
                {
                    transform.position = Vector3.MoveTowards(transform.position, WarpformationIndex.position, CurrentSpeed * Time.deltaTime);
                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 200 && Vector3.Distance(WarpformationIndex.position, transform.position) >= 100)
                    {
                        if (WarpGeneratorTemp == 0)
                        {
                            WarpGeneratorTemp += Time.deltaTime;
                            Instantiate(WarpGenerator, WarpformationIndex.position, Quaternion.identity);
                            WarpBooster.SetActive(false);
                        }
                    }
                    if (Vector3.Distance(WarpformationIndex.position, transform.position) > 50f)
                        anim.SetFloat("Warp, Slorius Flagship", 1);

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 15f)
                        anim.SetFloat("Warp, Slorius Flagship", 2);

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 2.5f)
                        CurrentSpeed = WarpSpeed * 0.1f;

                    if (Vector3.Distance(WarpformationIndex.position, transform.position) <= 0.1f)
                    {
                        WarpDriveActive = false;
                        CurrentSpeed = 0;

                        if (WarpCompleteTemp == 0)
                        {
                            WarpCompleteTemp += Time.deltaTime;
                            Instantiate(WarpArriveEffect, transform.position, Quaternion.identity);
                            Booster.SetActive(true);
                            StartCoroutine(WarpComplete());
                            Turret1.GetComponent<OurForceAttackSystem>().canAttack = true;
                            Turret2.GetComponent<OurForceAttackSystem>().canAttack = true;
                        }
                    }
                }
            }
        }
        else //����
        {
            if (WarpDriveActive == false)
            {
                //�Լ� �⺻ �̵�
                MoveDir = (DestinationArea - transform.position).normalized;
                DisctanceFromTarget = Vector2.Distance(DestinationArea, transform.position);

                if (TargetShip == null) //��������Ȳ������ ������
                {
                    transform.position += MoveDir * CurrentSpeed * Time.deltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, DirectionAtMoving);
                    rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 dir1 = (TargetShip.transform.position - transform.position).normalized;
                    Vector3 dir2 = (TargetShip.transform.position + transform.position).normalized;

                    if (distanceFromTarget > MaxDamageRange) //Ÿ���� �ִ� ��Ÿ��� ����� ����
                    {
                        MoveStemp = 0;
                        if (moveTargetAround != null)
                            StopCoroutine(moveTargetAround);
                        if (move != null)
                            StopCoroutine(move);

                        Quaternion targetRotation1 = Quaternion.LookRotation(Vector3.forward, dir1);
                        rb2D.transform.rotation = Quaternion.Lerp(rb2D.transform.rotation, targetRotation1, RotateSpeed * Time.deltaTime);
                        transform.position = Vector2.MoveTowards(transform.position, TargetShip.transform.position, CurrentSpeed * Time.deltaTime);

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                    else if (distanceFromTarget < MaxDamageRange) //��Ÿ� ���� ������ �ڵ����� ��ȸ
                    {
                        if (MoveStemp == 0)
                        {
                            MoveStemp += Time.deltaTime;
                            moveTargetAround = StartCoroutine(MoveTargetAround(true));
                        }

                        if (CurrentSpeed <= BoostSpeed)
                            CurrentSpeed += AcceleratorSpeed;
                    }
                }
            }
        }
    }

    //����� ��ȸ
    IEnumerator MoveTargetAround(bool boolean)
    {
        if (TargetShip != null || MyPlanet != null)
        {
            bool Online = false;

            while (true)
            {
                int RangeTarget = 0;
                float RandomTime = 0;

                if (boolean == true) //Ÿ�� ����� �ֺ����� ��ȸ
                {
                    RangeTarget = 4;
                    RandomTime = Random.Range(5, 10);
                }
                else //�༺�� �ֺ����� ��ȸ
                {
                    if (Online == false)
                    {
                        RangeTarget = 110;
                        RandomTime = Random.Range(1, 2);
                        Online = true;
                    }
                    else
                    {
                        RangeTarget = 110;
                        RandomTime = Random.Range(10, 30);
                    }
                }

                if (TargetShip != null)
                    endposition = new Vector3(Random.Range(TargetShip.transform.position.x + RangeTarget, TargetShip.transform.position.x - RangeTarget), Random.Range(TargetShip.transform.position.y + RangeTarget, TargetShip.transform.position.y - RangeTarget), transform.position.z);
                else if (MyPlanet != null)
                    endposition = new Vector3(Random.Range(MyPlanet.transform.position.x + RangeTarget, MyPlanet.transform.position.x - RangeTarget), Random.Range(MyPlanet.transform.position.y + RangeTarget, MyPlanet.transform.position.y - RangeTarget), transform.position.z);

                if (move != null)
                    StopCoroutine(move);

                move = StartCoroutine(Move(rb2D, CurrentSpeed));
                yield return new WaitForSeconds(RandomTime);
            }
        }
    }

    //��ȸ ����
    IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        if (TargetShip != null || MyPlanet != null)
        {
            float remainingDistance = (transform.position - endposition).sqrMagnitude;

            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    transform.position += transform.up * speed * Time.deltaTime;
                    Vector3 dir = (endposition - transform.position).normalized;
                    if (FlagShip == true)
                        transform.up = Vector3.Lerp(transform.up, dir, 0.01f);
                    else
                        transform.up = Vector3.Lerp(transform.up, dir, 0.05f);
                    remainingDistance = (transform.position - endposition).sqrMagnitude;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}