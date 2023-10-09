using System.Collections;
using UnityEngine;

public class BehaviourAtroCrossfa : MonoBehaviour
{
    ObjectManager objectManager;
    Rigidbody2D rb2d;
    Animator animator;

    private Shake shake;

    public float speed; //�⺻ �ӵ�, ���� �ӵ�
    public float EvasionSpeed; //ȸ���� �� �ӵ�
    public float runningSpeed; //�ٴ� �ӵ�, ȸ���ϰų� ������ �� ���δ�.
    private float currnetSpeed; //�⺻�� �ٴ� �ӵ��� ��ȯ���ִ� ����
    private float OneFireValue; //�ѹ��� �̻����� ������ �� �ֵ��� ����
    private float FireValue; //�ѹ��� �̻����� �߻��� �� �ֵ��� ����
    public float lineOfSite; //�ֿܰ���, �÷��̾� ������ ��ȸ�ϱ� ���� �뵵
    public float MissileAttackTime; //�ش� �ð��� ���� ������ �̻��� ����
    private float rollingTime; //����� ȸ��
    private float timeStamp; //����� ���縶�� �߻��ϱ� ���� ����
    public float GunFireRate; //����� ����ӵ�
    private float TaskTime; //������� �ѹ��̶� �߻��� �� �ٽ� �߻��ϱ� ���� ����
    private float AimStemp; //�ڷ�ƾ EvasionRandom�� �ѹ��� ������ ���� ����
    private float DownTime; //�ٿ�� ���¿��� ġ��Ÿ�� �� ���� �� �ൿ�� ������ �ʱ�ȭ �Ǵ� ���� ������ �� �ѹ��� �ٿ�Ǵ� ������ �����ϱ� ���� ����
    public float FollowVector; //���� ���� �÷��̾� �������� �̵��ϱ� ���� ����

    private float SoundTime;
    private float SoundChargeTime;

    public bool IntoLine = false; //���� ����, �÷��̾ �ִ� �������� �����ϱ� ���� ����ġ
    public bool IntoLineSpeed = false; //���� ����, �÷��̾� ���� ��ó�� ���� �� ������ �ӵ��� ����
    public bool TurnRolling = false; //�̴ϰ� ȸ���� ����ġ
    public bool MachinegunFire = false; //�̴ϰ� ������� �� ����ġ
    private bool MoveComplete = false; //�������� ������ ���� ����ġ
    private bool deathStop = false; //�׾��� ��, �������� ��������� �ϴ� ����ġ
    private bool LegsDown = false; //�ٸ��� ����ȭ �Ǿ��� ���� ����ġ
    private bool Leg1Down = false; //1�� �ٸ��� ����ȭ�� �˸��� ����ġ
    private bool Leg2Down = false; //2�� �ٸ��� ����ȭ�� �˸��� ����ġ
    public bool MLBDown = false; //�̻��� ���밡 �ٿ�Ǿ��� ���� ����ġ
    public bool MachinegunDown = false; //������� �ٿ�Ǿ��� ���� ����ġ
    private bool MissileLaunchOn = false; //�̻��� �߻�� Ȱ��ȭ�� ���� ����ġ
    private bool MissileFiring = false; //�̻����� �߻��ϰ� ���� ���� ����ġ
    private bool MissileLaunchState = false; //�̻��� �߻� �غ� ����, ���⼭ �Ͻ� �ٿ� �Ǿ��� ���� �߻������� �ߴܽ�Ų��.
    private bool EvasionActive = false; //ȸ�Ǳ⵿ ���� �� ����ġ
    private bool TakeItDown = false; //�ٸ��� ���ܵǾ� �ٿ�Ǿ��� �� �ٸ� ���ݱ���� ��� ����ȭ
    public bool ImDown = false; //ġ��Ÿ�� �¾� ��� ����� ��� ������ ���� ����ġ
    public bool AimState = false; //�÷��̾�� ���ش��ϰ� ������ �˸��� ����ġ
    private bool Attraction = false; //�߷º��� ���� ������ �� �ٸ� �������� ����ȭ�ϴ� �뵵
    public bool Trigger;

    private Vector2 target; //��� ���� ��ġ
    Vector3 endposition; //���� ������ ��ġ��
    Vector3 Evasionposition;
    Vector3 PlayerPosition; //�÷��̾� �� �Ʊ� ��ġ ����

    public int MissileDamage; //���� �̻��� �߻�� ���ط�
    public int GunDamage; //���� ����� �߻�� ���ط�
    private int GunSmokeOn = 0; //ó�� ���� �߻� ����, ���� ��ݸ��� ���� �߻� ����
    private int EvasionCount; //ȸ�Ǳ⵿�ϱ� ���� ���� ����
    public int EvasionLevel; //ȸ�� �⵿�� ���� �Լ�. ������ ���� ���� ȸ�Ǹ� �� ���� �Ѵ�.

    public GameObject MissilePrefab;
    public Transform MissilePos;

    public Transform ammoPos; //�Ѿ� ���� ��ǥ
    public Transform ejectPos; //ź�� ���� ��ǥ
    public GameObject GunSmokePrefab;
    public Transform GunSmokePos;

    Coroutine moveCoroutine;
    Coroutine missileReady;
    Coroutine missileCharging;
    Coroutine missileFire;
    Coroutine evasionRandom;
    Coroutine evasionAction;
    Coroutine evasionActionBoost;

    int fireEffect;
    public Transform Enemytarget = null;
    public Vector2 size;

    public Transform magnetForm = null;
    public float MagnetForce;

    public GameObject Sounds;
    public GameObject MissileReadySound;
    public GameObject ChargeSound;
    public AudioClip BoostOn;
    public AudioClip DownAndRising;
    public AudioClip MissileFireSound;
    public AudioClip MachinegunRolling;
    public AudioClip MachinegunRollingEnd;
    public AudioClip MachinegunFireSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        objectManager = FindObjectOfType<ObjectManager>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        MoveComplete = false;
        LegsDown = false;
        Leg1Down = false;
        Leg2Down = false;
        deathStop = false;
        MissileLaunchOn = false;
        MissileLaunchState = false;
        MissileFiring = false;
        MLBDown = false;
        TurnRolling = false;
        MachinegunFire = false;
        MachinegunDown = false;
        EvasionActive = false;
        ImDown = false;
        TakeItDown = false;
        AimState = false;
        Attraction = false;
        MissileReadySound.SetActive(false);
        ChargeSound.SetActive(false);
        Sounds.SetActive(true);

        if (BattleSave.Save1.MissionLevel == 1)
        {
            MissileDamage = 120;
            GunDamage = 8;
            MissileAttackTime = 7;
            GunFireRate = 0.05f;
        }
        else if (BattleSave.Save1.MissionLevel == 2)
        {
            MissileDamage = 168;
            GunDamage = 12;
            MissileAttackTime = 7;
            GunFireRate = 0.05f;
        }
        else if (BattleSave.Save1.MissionLevel == 3)
        {
            MissileDamage = 192;
            GunDamage = 15;
            MissileAttackTime = 6.5f;
            GunFireRate = 0.05f;
        }

        OneFireValue = 0;
        FireValue = 0;
        rollingTime = 0;
        timeStamp = 0;
        TaskTime = 0;
        GunSmokeOn = 0;
        AimStemp = 0;
        DownTime = 0;
        SoundTime = 0;
        SoundChargeTime = 0;

        currnetSpeed = speed;

        StartCoroutine(ShootingMovement());
        StartCoroutine(MoveToWard()); //���� �� ���� �ִϸ��̼�
        StartCoroutine(LaunchBase());
    }

    private void OnDisable()
    {
        if (animator.GetBool("Movement, Atro-Crossfa 390") == true)
            animator.SetBool("Movement, Atro-Crossfa 390", false);
        if (animator.GetBool("Movement(down), Atro-Crossfa 390") == true)
            animator.SetBool("Movement(down), Atro-Crossfa 390", false);
        if (animator.GetBool("Eye(Missile ready Color), Atro-Crossfa 390") == true)
            animator.SetBool("Eye(Missile ready Color), Atro-Crossfa 390", false);
        if (animator.GetBool("Missile ready, Atro-Crossfa 390") == true)
            animator.SetBool("Missile ready, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile ready state, Atro-Crossfa 390") == true)
            animator.SetBool("Missile ready state, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile Charge, Atro-Crossfa 390") == true)
            animator.SetBool("Missile Charge, Atro-Crossfa 390", false);
        if (animator.GetBool("Eye(Charge Color), Atro-Crossfa 390") == true)
            animator.SetBool("Eye(Charge Color), Atro-Crossfa 390", false);
        if (animator.GetBool("Missile fire, Atro-Crossfa 390") == true)
            animator.SetBool("Missile fire, Atro-Crossfa 390", false);
        if (animator.GetBool("Missile off, Atro-Crossfa 390") == true)
            animator.SetBool("Missile off, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun rolling1, Atro-Crossfa 390") == true)
            animator.SetBool("Gun rolling1, Atro-Crossfa 390", false);
        if (animator.GetBool("Gun rolling2, Atro-Crossfa 390") == true)
            animator.SetBool("Gun rolling2, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion front, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion front, Atro-Crossfa 390, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion back, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion back, Atro-Crossfa 390, Atro-Crossfa 390", false);
        if (animator.GetBool("Evasion Boost, Atro-Crossfa 390, Atro-Crossfa 390") == true)
            animator.SetBool("Evasion Boost, Atro-Crossfa 390, Atro-Crossfa 390", false);
        if (animator.GetBool("Im Down!, Atro-Crossfa 390") == true)
            animator.SetBool("Im Down!, Atro-Crossfa 390", false);
        if (animator.GetBool("Idle(down), Atro-Crossfa 390") == true)
            animator.SetBool("Idle(down), Atro-Crossfa 390", false);

        if (animator.GetFloat("Moving Speed, Atro-Crossfa 390") != 0)
            animator.SetFloat("Moving Speed, Atro-Crossfa 390", 0);
        if (animator.GetFloat("Gun rolling start, Atro-Crossfa 390") != 0)
            animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);
    }

    public void AtroCrossfaDeath(bool droping)
    {
        if (droping == true)
        {
            deathStop = true;
            Sounds.SetActive(false);
        }
        else
        {
            deathStop = false;
        }
    }

    //ġ��Ÿ�� �¾��� �� �ٿ�Ǿ��ٰ� �ٽ� �Ͼ��
    public void TakeDown(bool boolean)
    {
        ImDown = boolean;
        if (DownTime == 0 && ImDown == true)
        {
            DownTime += Time.deltaTime;
            StartCoroutine(Rising());
        }
    }

    //�ٸ� ���� ��, �Ѿ�����
    public void AtroCrossfaImDown(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("I'm down!");
            StartCoroutine(Imdown());
        }
    }

    IEnumerator Rising()
    {
        if (Leg1Down == false && Leg2Down == false)
        {
            SoundManager.instance.SFXPlay2("Sound", DownAndRising);
            animator.SetBool("Down but rising!, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Down but rising!, Atro-Crossfa 390", false);
            gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = false;
            ImDown = false;
            DownTime = 0;
        }
        else if (Leg1Down == true || Leg2Down == true)
        {
            SoundManager.instance.SFXPlay2("Sound", DownAndRising);
            animator.SetBool("Down but rising!(down), Atro-Crossfa 390", true);
            yield return new WaitForSeconds(2f);
            animator.SetBool("Down but rising!(down), Atro-Crossfa 390", false);
            gameObject.GetComponent<HealthAtroCrossfa>().TakeItDown = false;
            ImDown = false;
            DownTime = 0;
        }
    }

    public void AtroCrossfaLeg1Down(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("Leg1Down : " + Leg1Down);
            Leg1Down = true;
        }
        else
        {
            Leg1Down = false;
        }
    }

    public void AtroCrossfaLeg2Down(bool Down)
    {
        if (Down == true)
        {
            //Debug.Log("Leg2Down : " + Leg2Down);
            Leg2Down = true;
        }
        else
        {
            Leg2Down = false;
        }
    }

    //ȸ�Ǳ⵿�� ���� ���� �Լ�
    IEnumerator EvasionRandom()
    {
        if (LegsDown == false && deathStop == false && AimState == true && EvasionActive == false)
        {
            while (true)
            {
                EvasionCount = Random.Range(0, EvasionLevel);
                yield return new WaitForSeconds(1);
            }
        }
    }

    //�����ð����� �̻��� �߻�
    IEnumerator LaunchBase()
    {
        if(deathStop == false && MissileLaunchOn == false && MLBDown == false)
        {
            while (true)
            {
                yield return new WaitForSeconds(MissileAttackTime);
                MissileLaunchOn = true;
                missileReady = StartCoroutine(MissileReady());
            }
        }
    }

    //�̻��� ������
    IEnumerator MissileReady()
    {
        if(deathStop == false && MLBDown == false)
        {
            MissileReadySound.SetActive(true);
            animator.SetBool("Eye(Missile ready Color), Atro-Crossfa 390", true);
            animator.SetBool("Missile ready, Atro-Crossfa 390", true);
            yield return new WaitForSeconds(1.583f);
            animator.SetBool("Missile ready state, Atro-Crossfa 390", true);
            animator.SetBool("Eye(Missile ready Color), Atro-Crossfa 390", false);
            MissileLaunchState = true;
        }
    }

    //�̻��� �߻������� ����
    IEnumerator MissileCharge()
    {
        if (deathStop == false && MLBDown == false)
        {
            ChargeSound.SetActive(true);
            animator.SetBool("Missile Charge, Atro-Crossfa 390", true);
            animator.SetBool("Eye(Charge Color), Atro-Crossfa 390", true);
            animator.SetBool("Missile ready state, Atro-Crossfa 390", false);
            yield return new WaitForSeconds(2);
            MissileFiring = true;
        }
    }

    void Update()
    {
        if (IntoLine == false)
        {
            if (magnetForm != null)
            {
                Attraction = true;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(magnetForm.position.x, magnetForm.position.y, transform.position.z), MagnetForce * Time.deltaTime); //Ư�� ������ �̵�
            }
            else
                Attraction = false;

            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�

            //������ ���ϰ� ���� �� ȸ���ϱ� ���� �ڷ�ƾ ����
            if (AimState == true)
            {
                if (AimStemp == 0)
                {
                    AimStemp += Time.deltaTime;
                    evasionRandom = StartCoroutine(EvasionRandom());
                }
            }
            else
            {
                AimStemp = 0;
                if (evasionRandom != null)
                    StopCoroutine(evasionRandom);
            }

            //ȸ�Ǳ⵿
            if (LegsDown == false && deathStop == false && EvasionCount == 1 && ImDown == false && TakeItDown == false && Leg1Down == false && Leg2Down == false && Attraction == false)
            {
                EvasionCount = 0;
                evasionAction = StartCoroutine(EvasionAction());
                evasionActionBoost = StartCoroutine(EvasionActionBoost());
            }

            if (LegsDown == false && deathStop == false && EvasionActive == true && ImDown == false && TakeItDown == false && Leg1Down == false && Leg2Down == false && Attraction == false)
                transform.position = Vector3.Lerp(transform.position, Evasionposition, currnetSpeed * Time.deltaTime * 0.25f);

            //ġ��Ÿ�� ���� �ٿ�Ǿ��� �� �ٸ� �ൿ�� ��� ����
            if (ImDown == true && TakeItDown == true)
            {
                animator.SetBool("Damage1, Atro-Crossfa 390", false);
                animator.SetBool("Damage2, Atro-Crossfa 390", false);
                animator.SetBool("Damage1(down), Atro-Crossfa 390", false);
                animator.SetBool("Damage2(down), Atro-Crossfa 390", false);

                transform.Find("Body_1/Eye center/Eye fire light").gameObject.SetActive(false);
                transform.Find("Body_1/Gun firing").gameObject.SetActive(false);
                transform.Find("Body_1/Left leg/Boost").gameObject.SetActive(false);
                transform.Find("Body_1/Right leg top_1/Boost").gameObject.SetActive(false);

                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);
                if (evasionAction != null)
                    StopCoroutine(evasionAction);
                if (evasionActionBoost != null)
                    StopCoroutine(evasionActionBoost);
            }

            float distanceFromPlayer = Vector2.Distance(Enemytarget.position, transform.position);

            //�÷��̾ �ش� ������ ���� �ݰ��� ��Ż��, ����
            if (distanceFromPlayer > lineOfSite && LegsDown == false && deathStop == false)
            {
                MoveComplete = false;
                animator.SetBool("Movement, Atro-Crossfa 390", true);
                currnetSpeed = runningSpeed;
                transform.position = Vector2.MoveTowards(transform.position, Enemytarget.position, currnetSpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer < lineOfSite && LegsDown == false && EvasionActive == false)
                currnetSpeed = speed;

            //�̻��� ���밡 ����ȭ �Ǿ��� ���, ��� �̻��� ���� �ڷ�ƾ �ߴ�
            if (MLBDown == true)
            {
                if (missileReady != null)
                    StopCoroutine(missileReady);
                if (missileCharging != null)
                    StopCoroutine(missileCharging);
                if (missileFire != null)
                    StopCoroutine(missileFire);

                MissileReadySound.SetActive(false);
                ChargeSound.SetActive(false);
            }

            //�̻��� �߻����, ġ��Ÿ�� �¾Ұų� �̻��� ���밡 �ı��Ǿ��� ��� ������ �ߴܽ�Ų��.
            if (MissileLaunchState == true && MLBDown == false && deathStop == false)
            {
                if (ImDown == false && TakeItDown == false)
                {
                    if (OneFireValue == 0)
                    {
                        OneFireValue += Time.deltaTime;
                        missileCharging = StartCoroutine(MissileCharge());
                    }
                }
                else if (ImDown == true || TakeItDown == true)
                {
                    if (missileCharging != null)
                        StopCoroutine(missileCharging);

                    MissileReadySound.SetActive(false);
                    ChargeSound.SetActive(false);
                    animator.SetBool("Eye(Charge Color), Atro-Crossfa 390", false);
                    animator.SetBool("Eye(Missile ready Color), Atro-Crossfa 390", false);
                    animator.SetBool("Missile Charge, Atro-Crossfa 390", false);
                    animator.SetBool("Missile ready state, Atro-Crossfa 390", true);
                    OneFireValue = 0;
                }
            }

            //�̻��� �߻�
            if (MissileFiring == true && MLBDown == false && deathStop == false)
            {
                if (FireValue == 0)
                {
                    FireValue += Time.deltaTime;
                    missileFire = StartCoroutine(MissileFire());
                }
            }

            if (timeStamp <= GunFireRate)
                timeStamp += Time.deltaTime;

            if (TurnRolling == true)
            {
                if (SoundTime == 0)
                {
                    SoundTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay2("Sound", MachinegunRolling);
                }
            }

            if (TurnRolling == false && GunSmokeOn == 1)
            {
                if (SoundChargeTime == 0)
                {
                    SoundChargeTime += Time.deltaTime;
                    SoundManager.instance.SFXPlay2("Sound", MachinegunRollingEnd);
                }
            }

            //����� ���
            if (Trigger && MachinegunDown == false && ImDown == false && TakeItDown == false && deathStop == false)
            {
                GunSmokeOn = 2;
                TaskTime = 0;

                if (TurnRolling == true && MachinegunDown == false)
                {
                    rollingTime += Time.deltaTime;

                    if (rollingTime <= 0)
                    {
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);
                    }

                    else if (rollingTime > 0 && rollingTime < 0.3f)
                    {
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", animator.GetFloat("Gun rolling start, Atro-Crossfa 390") + 0.01f);
                    }

                    else if (rollingTime >= 0.3f && rollingTime < 0.5f)
                    {
                        animator.SetBool("Gun rolling2, Atro-Crossfa 390", false);
                        animator.SetBool("Gun rolling1, Atro-Crossfa 390", true);
                    }
                    else if (rollingTime >= 0.5f)
                    {
                        animator.SetBool("Gun rolling1, Atro-Crossfa 390", false);
                        animator.SetBool("Gun rolling2, Atro-Crossfa 390", true);

                        if (rollingTime > 0.5f)
                            rollingTime = 0.5f;
                        //StartCoroutine(shake.ShakeCamera());

                        if (timeStamp >= GunFireRate)
                        {
                            timeStamp = 0;

                            Shake.Instance.ShakeCamera(1, 0.1f);
                            if (Leg1Down == false && Leg2Down == false)
                            {
                                GameObject MiniGunBullet = SingletonObject.instance.Loader_AtroAmmo();
                                MiniGunBullet.transform.position = ammoPos.position;
                                MiniGunBullet.transform.rotation = ammoPos.rotation; //�߻� �Ѿ� ����
                                MiniGunBullet.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(GunDamage); //�Ѿ˿��� ������ ����
                            }
                            else if (Leg1Down == true || Leg2Down == true)
                            {
                                GameObject MiniGunBullet = SingletonObject.instance.Loader_AtroAmmo2();
                                MiniGunBullet.transform.position = ammoPos.position;
                                MiniGunBullet.transform.rotation = ammoPos.rotation; //�߻� �Ѿ� ����
                                MiniGunBullet.GetComponent<AmmoMovementKaotiJaios4>().SetDamage(GunDamage); //�Ѿ˿��� ������ ����
                            }
                            EjectShell(); //ź�� ����
                        }
                    }
                }
            }

            //�̴ϰ� ���� �ߴܽ� ȸ���ӵ� ���� �ִϸ��̼�
            if (TurnRolling == false && MachinegunDown == false && TakeItDown == false || Trigger && ImDown == true || Trigger && TakeItDown == true)
            {
                if (deathStop == false)
                {
                    if (rollingTime >= 0)
                        rollingTime -= Time.deltaTime * 0.1f;

                    if (rollingTime >= 0.4f && rollingTime < 0.5f)
                    {
                        //����� ������� ���� �߻�
                        if (GunSmokeOn == 2)
                        {
                            TurnRolling = false;
                            GunSmokeOn = 1;

                            if (GunSmokeOn == 1)
                            {
                                GunSmokeOn = 0;
                                GameObject Smoke = Instantiate(GunSmokePrefab, GunSmokePos.position, GunSmokePos.rotation);
                                Smoke.SetActive(true);
                                Destroy(Smoke, 3);
                            }
                        }

                        animator.SetBool("Gun rolling2, Atro-Crossfa 390", false);
                        animator.SetBool("Gun rolling1, Atro-Crossfa 390", true);
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 2);
                    }
                    else if (rollingTime > 0 && rollingTime < 0.4f)
                    {
                        animator.SetBool("Gun rolling1, Atro-Crossfa 390", false);
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", animator.GetFloat("Gun rolling start, Atro-Crossfa 390") - 0.01f);

                        if (animator.GetFloat("Gun rolling start, Atro-Crossfa 390") < 0)
                            animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);
                    }
                    else if (rollingTime <= 0)
                    {
                        animator.SetFloat("Gun rolling start, Atro-Crossfa 390", 0);

                        if (TaskTime == 0)
                        {
                            TaskTime += Time.deltaTime;
                            MachinegunFire = false;
                        }
                    }
                }
            }

            //�ٸ� 2���� ��� ����ȭ�Ǿ��� ���, ����ȭ ����ġ Ȱ��ȭ
            if (Leg1Down == true && Leg2Down == true)
                LegsDown = true;
        }
        else
        {
            Enemytarget = objectManager.SupplyList[Random.Range(0, objectManager.SupplyList.Count - 1)].transform;
            LookAtPlayer(); //�÷��̾� �ٶ󺸱�
            MoveComplete = false;
            animator.SetBool("Movement, Atro-Crossfa 390", true);
            if (IntoLineSpeed == false)
                transform.Translate(transform.right * FollowVector * 20 * Time.deltaTime);
            else
            {
                currnetSpeed = runningSpeed;
                transform.Translate(transform.right * FollowVector * currnetSpeed * Time.deltaTime);
            }
        }
    }

    //�̻��� �߻�
    IEnumerator MissileFire()
    {
        SoundManager.instance.SFXPlay2("Sound", MissileFireSound);
        GameObject Missile = Instantiate(MissilePrefab, MissilePos.position, MissilePos.rotation);
        Missile.GetComponentInChildren<BehaviourNarSyrHaicross13>().SetDamage(MissileDamage);
        animator.SetBool("Eye(Charge Color), Atro-Crossfa 390", false);
        animator.SetBool("Missile fire, Atro-Crossfa 390", true);
        yield return new WaitForSeconds(0.416f);
        animator.SetBool("Missile off, Atro-Crossfa 390", true);
        yield return new WaitForSeconds(1.166f);
        animator.SetBool("Missile off, Atro-Crossfa 390", false);
        animator.SetBool("Missile fire, Atro-Crossfa 390", false);
        animator.SetBool("Missile Charge, Atro-Crossfa 390", false);
        animator.SetBool("Missile ready state, Atro-Crossfa 390", false);
        animator.SetBool("Missile ready, Atro-Crossfa 390", false);

        MissileReadySound.SetActive(false);
        ChargeSound.SetActive(false);
        MissileFiring = false;
        MissileLaunchState = false;
        MissileLaunchOn = false;
        OneFireValue = 0;
        FireValue = 0;
    }

    //�Ϲ� ��ȸ
    public IEnumerator ShootingMovement()
    {
        if (LegsDown == false && deathStop == false && ImDown == false && TakeItDown == false && EvasionActive == false && Attraction == false && IntoLine == false)
        {
            while (true)
            {
                float RandomMovement = Random.Range(0.25f, 6);
                float RandomWander = Random.Range(0.25f, 1);

                endposition = new Vector3(Random.Range(transform.position.x - RandomMovement, transform.position.x + RandomMovement),
                    Random.Range(transform.position.y - RandomMovement, transform.position.y + RandomMovement), transform.position.z);
                //Debug.Log("Moving : " + Moving);

                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);

                moveCoroutine = StartCoroutine(Move(rb2d, currnetSpeed));
                yield return new WaitForSeconds(RandomWander);
            }
        }
    }

    //������ ��ǥ�� ���� ������ ����
    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endposition).sqrMagnitude;

        if (LegsDown == false && deathStop == false && ImDown == false && TakeItDown == false && EvasionActive == false && Attraction == false && IntoLine == false)
        {
            while (remainingDistance > float.Epsilon)
            {
                //�̵�
                if (rigidbodyToMove != null)
                {
                    MoveComplete = false;
                    //animator.SetBool("Move, Aso Shiioshare", true);
                    Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endposition, speed * Time.deltaTime);

                    if (newPosition == endposition || transform.position == endposition) //��ǥ������ ���� ������ ������ ���, ���ڸ����� ����ä�� �̵� �ִϸ��̼��� Ȱ��ȭ�Ǵ� ������ ����
                    {
                        //Debug.Log(string.Format("newPosition : {0}, endposition : {1}, rigidbodyToMove.position : {2}", newPosition, endposition, rigidbodyToMove.position));
                        MoveComplete = true;
                        break;
                    }

                    rb2d.MovePosition(newPosition);
                    remainingDistance = (transform.position - endposition).sqrMagnitude;
                }
                yield return new WaitForFixedUpdate();

                if (transform.position == endposition)
                {
                    MoveComplete = true;
                }
            }
            //animator.SetBool("Move, Aso Shiioshare", false);
        }
    }

    //ȸ�Ǳ⵿
    IEnumerator EvasionAction()
    {
        float RandomMovementX = Random.Range(-3, 3);
        float RandomMovementYPlus = Random.Range(5, 7);
        float RandomMovementYMinus = Random.Range(-7, -5);
        float RandomY = Random.Range(0, 2);
        float EvasionY;

        if (RandomY == 0)
            EvasionY = transform.position.y - RandomMovementYPlus;
        else
            EvasionY = transform.position.y - RandomMovementYMinus;

        if (RandomMovementX > 0)
        {
            animator.SetBool("Evasion front, Atro-Crossfa 390", true);
            Evasionposition = new Vector3(transform.position.x - RandomMovementX, EvasionY, transform.position.z);
            EvasionActive = true;
            currnetSpeed = EvasionSpeed;
            yield return new WaitForSeconds(0.916f);
            animator.SetBool("Evasion front, Atro-Crossfa 390", false);
        }
        else
        {
            animator.SetBool("Evasion back, Atro-Crossfa 390", true);
            Evasionposition = new Vector3(transform.position.x - RandomMovementX, EvasionY, transform.position.z);
            EvasionActive = true;
            currnetSpeed = EvasionSpeed;
            yield return new WaitForSeconds(0.916f);
            animator.SetBool("Evasion back, Atro-Crossfa 390", false);
        }

        EvasionActive = false;
        currnetSpeed = speed;
    }

    IEnumerator EvasionActionBoost()
    {
        SoundManager.instance.SFXPlay2("Sound", BoostOn);
        animator.SetBool("Evasion Boost, Atro-Crossfa 390", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("Evasion Boost, Atro-Crossfa 390", false);
    }

    //x�� �÷��̾� �Ĵٺ���
    void LookAtPlayer()
    {
        if (Enemytarget.gameObject.activeSelf == true && deathStop == false && TakeItDown == false && ImDown == false && LegsDown == false)
        {
            if (Enemytarget.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                //gameObject.GetComponent<ShieldAsoShiioshare>().ShieldDamageDirection(true);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                //gameObject.GetComponent<ShieldAsoShiioshare>().ShieldDamageDirection(false);
            }
        }
        //transform.Lookat(player.position) 360�� ȸ������ �÷��̾� �Ĵٺ���
    }

    //���� �� ����, ���� �ִϸ��̼�
    IEnumerator MoveToWard()
    {
        while (true)
        {
            Vector3 v1 = transform.position;
            yield return new WaitForSeconds(0.1f);

            if (transform.rotation.y == 0) //������ �Ĵٺ��� ���� ��
            {
                if (Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Movement(down), Atro-Crossfa 390", true);
                else if(Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Movement, Atro-Crossfa 390", true);

                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("Moving Speed, Atro-Crossfa 390", 2f);
                }
                else //����
                {
                    animator.SetFloat("Moving Speed, Atro-Crossfa 390", -2f);
                }
            }
            else if (transform.rotation.y != 0) //�������� �Ĵٺ��� ���� ��
            {
                if (Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Movement(down), Atro-Crossfa 390", true);
                else if (Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Movement, Atro-Crossfa 390", true);

                if (v1.x > transform.position.x) //����
                {
                    animator.SetFloat("Moving Speed, Atro-Crossfa 390", -2f);
                }
                else //����
                {
                    animator.SetFloat("Moving Speed, Atro-Crossfa 390", 2f);
                }
            }
            if (MoveComplete == true || LegsDown == true || ImDown == true || TakeItDown == true)
            {
                if (Leg1Down == true || Leg2Down == true)
                    animator.SetBool("Movement(down), Atro-Crossfa 390", false);
                else if (Leg1Down == false && Leg2Down == false)
                    animator.SetBool("Movement, Atro-Crossfa 390", false);
            }
        }
    }

    //ź�� ����
    public void EjectShell()
    {
        GameObject ejectedShell = SingletonObject.instance.Loader_AtroShell();
        ejectedShell.transform.position = ejectPos.position;
        ShellMovement ShellMovement = ejectedShell.GetComponent<ShellMovement>();
        ShellMovement.Pos = ejectedShell.transform.position.y;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        ejectedShell.GetComponent<ShellMovement>().xVnot = xVnot;
        ejectedShell.GetComponent<ShellMovement>().yVnot = yVnot;
    }

    //�Ѿ����� �ִϸ��̼�
    IEnumerator Imdown()
    {
        TakeItDown = true;
        currnetSpeed = 0;
        animator.SetBool("Im Down!, Atro-Crossfa 390", true);

        yield return new WaitForSeconds(1.33f);
        
        animator.SetBool("Idle(down), Atro-Crossfa 390", true);
        animator.SetBool("Im Down!, Atro-Crossfa 390", false);

        currnetSpeed = speed / 2;
        TakeItDown = false;
        gameObject.GetComponent<HealthAtroCrossfa>().AtroCrossfaDownMark(true);
    }
}