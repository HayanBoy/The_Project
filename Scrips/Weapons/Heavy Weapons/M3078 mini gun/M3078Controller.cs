using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class M3078Controller : MonoBehaviour
{
    public GameControlSystem GameControlSystem;
    Animator animator;
    Rigidbody2D rb2D;
    ArthesL775Controller arthesL775Controller;
    Hydra56Controller hydra56Controller;
    MEAGController meagController;
    UGG98Controller ugg98Controller;
    VM5GrenadeController vm5GrenadeController;
    Movement movement;
    GunController gunController;
    public GameObject GameControlScript;
    public ObjectManager objectManager;

    private Shake shake;

    //GameObject MiniGunWeapon;
    public GameObject WeaponSwapBtn;
    public GameObject DashUI;
    public GameObject ReloadUI;
    public Image SwapActive;

    public int ViewSW06_Magazine; //�ΰ��� źâ �� ź�� ǥ�ñ�
    public int GunType; //�⺻ �� Ÿ���� ���޹ޱ� ���� ����
    public int SubGunTypeFront = 0; //�� ������� ��������
    public int SubGunTypeBack = 0; //�� ������� ��������
    public int AmmoAmount; //ź�෮
    public int RealAmmoAmount; //���� ź�� ��� ����
    private bool reloading = false; //��ȭ�� �������� ���� ����ġ
    private bool Active = false; //��ȭ�� ȹ���, ��ȭ�⸦ Ȱ��ȭ��Ű�� ����ġ

    //UI
    public GameObject PlayerMagazine;
    public GameObject PlayerMinigunAmmo;
    public Text ammoText;
    public Text ammoText2;

    //M3078
    private bool TurnRolling = false; //M3078 ȸ����Ű�� ��ȣ
    public GameObject M3078Prefab;
    public Transform M3078Pos;
    public GameObject M3078SmokePrefab;
    public Transform M3078SmokePos;
    public Transform ammoPos; //M3078 �Ѿ� ���� ��ǥ
    private GameObject ejectedShell; //M3078 źâ ������Ʈ Ǯ��, ������ �����ϱ� ���� ���� 
    public Transform ejectPos; //M3078 ź�� ���� ��ǥ
    public int MiniGunDamage; //M3078 �Ѿ˴� ������
    private int GunSmokeOn = 0; //M3078 ó�� ���� �߻� ����, ���� ��ݸ��� ���� �߻� ����
    public float FireRate; //M3078 �ʴ� �߻��
    private float rollingTime = 0; //M3078 ȸ���� �ð�

    //ASC 365
    public GameObject ASC365Prefab;
    public GameObject ASC365FlamePrefab; //ASC 365 ȭ���߻� ������
    public GameObject ASC365FlameStartPrefab; //ASC 365 ����ũ ȭ�� ������
    public GameObject ASC365FlameAmmoPrefab; //ASC 365 ȭ�� ����, �浹�� �ƴ� �����ð� ���Ŀ��� ������� ������ ���� �߻�ü
    public GameObject ASC365FlameSmokePrefab; //ASC 365�� �����ð� �̻� ���� �߻�� ���� �߻�
    public Transform ASC365FlameAmmoPos;
    public int ASC365FlameDamage;
    public float ASC365FireRate; //ASC 365 �ʴ� �߻��
    private float ASC365FireTime; //�����ð� �̻� ���� ��ݽ� ���Ⱑ �߻��ϱ� ���� ����
    private float ASCFireRepeatTime; //ȭ���߻�ݺ��� �׻� �߻� �� 3�� �ڿ� �ݺ��ǵ��� ó��

    public int HeavyWeaponType; //��ȭ�� Ÿ��
    public int GetHeavyWeapon; //ȹ���� ��ȭ�� ��ȣ
    public float UsingTime = 0; //��ȭ�⸦ ȹ������ ��, ��ȭ�� ȹ�� ����� �̾����� ���� ����
    private float timeStamp = 0.0f; //��ȭ�� �ʴ� �߻���� ���Ǵ� ����
    private float SwapTime = 0; //���� ��ü�� ����
    private float SwapTime2 = 0; //���� ��ü�� ����
    private float SwichTime = 0.5f; //���⸦ ��ü�ϱ� ���� �ð��� ��� ����
    private float SoundTime; //�Ҹ��� �� �� ���� ��½�Ű�� ���� ����
    private float SoundChargeTime; //�Ҹ��� �� �� ���� ��½�Ű�� ���� ����
    private float TaskTime; //��ȭ�� ����� ���߾��� ���� ǥ���ϱ� ���� ����
    private float AbandonTime; //��ȭ�⸦ �� ������ڸ��� �ڷ�ƾ�� �� �ѹ��� Ȱ��ȭ�ϱ� ���� ����

    public bool CantSwap = false; //ü���� ��ȭ�� ��� ���� ��, ������ ���ϰ� ó��
    public bool isMiniGun; // ��ư �Է¿� bool��
    public bool isSwapM3078 = false;
    public bool UsingTask;
    private bool UsingChangeWeapon = false; //��ȭ�� ��밡���� ����
    private bool Click;

    //M3078 ����
    public AudioClip M3078Start;
    public AudioClip M3078End;
    public AudioClip M3078PickUp;
    public AudioClip M3078Abandone;
    public AudioClip Beep1;
    //ASC 365 ����
    public GameObject ASC365SteamSound; //���� �ݺ� �Ҹ�
    public GameObject ASC365FireRepeat;
    public AudioClip ASC365FireEnd;

    //�ٸ� ���� ���� ��ȭ���� ȿ�� ����
    public void TurnOn()
    {
        if (HeavyWeaponType == 2)
            ASC365FlameStartPrefab.SetActive(true);
    }
    public void TurnOff()
    {
        if (HeavyWeaponType == 2)
            ASC365FlameStartPrefab.SetActive(false);
    }

    public void MiniGunDown()
    {
        isMiniGun = true;
        CantSwap = true;
    }

    public void MiniGunUp()
    {
        isMiniGun = false;
        CantSwap = false;
    }

    public void M3078SwapUp()
    {
        if (Click == true)
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", false);
        Click = false;
    }

    public void M3078SwapDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", true);
    }

    public void M3078SwapEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", true);
        }
    }

    public void M3078SwapExit()
    {
        if (Click == true)
            WeaponSwapBtn.GetComponent<Animator>().SetBool("Click, Swap", false);
    }

    public void M3078SwapClick()
    {
        if(arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false && 
            movement.UsingTask == false && vm5GrenadeController.UsingTask == false && CantSwap == false)
        {
            if (SwichTime < 0)
            {
                isSwapM3078 = !isSwapM3078;
            }
        }
    }

    private void UpdateBulletText()
    {
        if (ViewSW06_Magazine >= 100)
        {
            ammoText.text = string.Format("<color=#8DFFF3>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#50888C>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
        else if (ViewSW06_Magazine < 100 && ViewSW06_Magazine >= 10)
        {
            ammoText.text = string.Format("<color=#00665B>{0}</color><color=#8DFFF3>{1}</color><color=#8DFFF3>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#2E3D3F>{0}</color><color=#50888C>{1}</color><color=#50888C>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
        else if (ViewSW06_Magazine < 10)
        {
            ammoText.text = string.Format("<color=#8C1411>{0}</color><color=#8C1411>{1}</color><color=#FF1A15>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
            ammoText2.text = string.Format("<color=#380302>{0}</color><color=#380302>{1}</color><color=#7E0D0B>{2}</color>", ViewSW06_Magazine / 100, ViewSW06_Magazine % 100 / 10, ViewSW06_Magazine % 10);
        }
    }

    void Start()
    {
        arthesL775Controller = FindObjectOfType<ArthesL775Controller>();
        hydra56Controller = FindObjectOfType<Hydra56Controller>();
        meagController = FindObjectOfType<MEAGController>();
        ugg98Controller = FindObjectOfType<UGG98Controller>();
        vm5GrenadeController = FindObjectOfType<VM5GrenadeController>();
        movement = FindObjectOfType<Movement>();
        gunController = FindObjectOfType<GunController>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        ViewSW06_Magazine = AmmoAmount;
        RealAmmoAmount = -1;

        MiniGunDamage = UpgradeDataSystem.instance.M3078Damage;
        ASC365FlameDamage = UpgradeDataSystem.instance.ASC365Damage;
    }

    //��� �� ������
    public void Update()
    {
        timeStamp += Time.deltaTime;
        UpdateBulletText();

        if(UsingChangeWeapon == true && SwichTime > -0.5f)
            SwichTime -= Time.deltaTime;

        if (GetHeavyWeapon > 0)
            ChangeWeapon();

        //M3078
        if (HeavyWeaponType == 1)
        {
            if (GetHeavyWeapon == 1)
                M3078Fire();

            if (rollingTime <= 0 || GetHeavyWeapon == 0)
                rollingTime = 0;

            //M3078 ȸ���Ҹ� ���
            if (GetHeavyWeapon == 1 && UsingChangeWeapon == false)
            {
                if (isMiniGun && TurnRolling == true)
                {
                    if (SoundTime == 0)
                    {
                        SoundTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay18("Sound", M3078Start);
                    }
                }
            }
            //M3078 ����Ҹ� ���
            if (GetHeavyWeapon == 1 && UsingChangeWeapon == false && GunSmokeOn == 0)
            {
                if (!isMiniGun && TurnRolling == false)
                {
                    if (SoundChargeTime == 0)
                    {
                        SoundChargeTime += Time.deltaTime;
                        SoundManager.instance.SFXPlay18("Sound", M3078End);
                    }
                }
            }

            //�̴ϰ� ���� �ߴܽ� ȸ���ӵ� ���� �ִϸ��̼�
            if (TurnRolling == false && UsingChangeWeapon == false)
            {
                if (rollingTime >= 0)
                    rollingTime -= Time.deltaTime * 0.2f;

                if (rollingTime >= 0.3f && rollingTime < 0.5f)
                {
                    animator.SetBool("Attack rolling M3078, 2", false);
                    animator.SetBool("Attack rolling M3078, 1", true);
                    animator.SetFloat("Attack start M3078", 0.5f);
                }
                else if (rollingTime > 0 && rollingTime < 0.3f)
                {
                    animator.SetBool("Attack rolling M3078, 1", false);
                    animator.SetFloat("Attack start M3078", animator.GetFloat("Attack start M3078") - 0.01f);

                    if (animator.GetFloat("Attack start M3078") < 0)
                        animator.SetFloat("Attack start M3078", 0);
                }
                else if (rollingTime <= 0)
                {
                    animator.SetFloat("Attack start M3078", 0);

                    if (TaskTime == 0)
                    {
                        TaskTime += Time.deltaTime;
                        UsingTask = false;
                        Invoke("SwapAnimationOff", 0.5f);
                    }
                }
            }
        }

        //ASC 365
        else if (HeavyWeaponType == 2)
        {
            if (GetHeavyWeapon == 2 && UsingChangeWeapon == false)
            {
                if (SwichTime > -0.5f)
                    SwichTime -= Time.deltaTime;

                if (UsingTime > 0.5f)
                    StartCoroutine(ReadyHeavyWeapon());
                ASC365Fire();
            }

            if (!isMiniGun) //�̻�ݽ� ���� �ð� ����
            {
                if (TaskTime == 0 && UsingTask == true) //�̻�ݽ� ��������� �߻�
                {
                    TaskTime += Time.deltaTime;
                    UsingTask = false;
                    ASCFireRepeatTime = 0;
                    ASC365FireRepeat.SetActive(false);
                    SoundManager.instance.SFXPlay("Sound", ASC365FireEnd);
                    animator.SetFloat("ASC 365 Attack", 0);
                }

                if (ASC365FireTime > 0)
                    ASC365FireTime -= Time.deltaTime;
            }

            if (ASC365FireTime < 5) //���� �ð� �̻�ݽ� ���� �̹߻�
            {
                ASC365SteamSound.SetActive(false);
                animator.SetBool("ASC Smoke", false);
            }
        }

        //��ȭ�� ������
        if(RealAmmoAmount == 0)
        {
            GetHeavyWeapon = 0;

            if (AbandonTime == 0)
            {
                AbandonTime += Time.deltaTime;
                StartCoroutine(AbandonWeapon());
                //Debug.Log(Active);
            }
        }
    }

    //���� ��ü
    void ChangeWeapon()
    {
        //�ֹ���� ��ü
        if (isSwapM3078 || Input.GetKeyDown(KeyCode.Y))
        {
            if (ViewSW06_Magazine > 0 && Active == true && UsingChangeWeapon == false && SwichTime < 0)
            {
                if (SwapTime2 == 0)
                {
                    SwapTime2 += Time.deltaTime;
                    StartCoroutine(OutputBasicWeapon());
                }
            }
        }
        //��ȭ��� ��ü
        if (!isSwapM3078 || Input.GetKeyDown(KeyCode.Y))
        {
            if (UsingChangeWeapon == true && SwichTime < 0)
            {
                if (SwapTime == 0)
                {
                    SwapTime += Time.deltaTime;
                    StartCoroutine(OuputHeavyWeapon());
                }
            }
        }
    }

    //M3078 �̴ϰ� ���
    public void M3078Fire()
    {
        if(GetHeavyWeapon == 1 && UsingChangeWeapon == false)
        {
            if (SwichTime > -0.5f)
                SwichTime -= Time.deltaTime;
            GunSmokeOn = 2;

            if (UsingTime > 0.5f)
                StartCoroutine(ReadyHeavyWeapon());

            if (isMiniGun || Input.GetKey(KeyCode.C))
            {
                if(ViewSW06_Magazine > 0 && Active == true)
                {
                    TurnRolling = true;
                    UsingTask = true;
                    TaskTime = 0;
                    AbandonTime = 0;

                    if (TurnRolling == true)
                    {
                        rollingTime += Time.deltaTime;

                        if (rollingTime <= 0)
                        {
                            animator.SetFloat("Attack start M3078", 0);
                        }

                        else if (rollingTime > 0 && rollingTime < 0.3f)
                        {
                            animator.SetFloat("Attack start M3078", animator.GetFloat("Attack start M3078") + 0.01f);
                        }

                        else if (rollingTime >= 0.3f && rollingTime < 0.5f)
                        {
                            animator.SetBool("Attack rolling M3078, 2", false);
                            animator.SetBool("Attack rolling M3078, 1", true);
                        }
                        else if (rollingTime >= 0.5f)
                        {
                            animator.SetBool("Attack rolling M3078, 1", false);
                            animator.SetBool("Attack rolling M3078, 2", true);

                            if (rollingTime > 0.5f)
                                rollingTime = 0.5f;

                            if (timeStamp >= FireRate)
                            {
                                timeStamp = 0;
                                Shake.Instance.ShakeCamera(2, 0.1f);

                                GameObject MiniGunBullet = objectManager.LoaderMiniGun();

                                MiniGunBullet.transform.position = ammoPos.position;
                                MiniGunBullet.transform.rotation = ammoPos.rotation; //�߻� �Ѿ� ����
                                MiniGunBullet.GetComponent<MiniGunAmmoMovement>().SetDamage(MiniGunDamage); //�Ѿ˿��� ������ ����

                                MiniGunAmmoMovement MiniGunammoMovement = MiniGunBullet.GetComponent<MiniGunAmmoMovement>(); //MiniGunAmmoMovement ��ũ��Ʈ ������Ʈ �Ŵ��� �ʱ�ȭ, �̰� �����ָ� �ƹ��͵� ���� 

                                MiniGunammoMovement.isHit = false; //�ǰݹ���
                                MiniGunammoMovement.objectManager = objectManager;

                                EjectShell(); //ź�� ����
                                ViewSW06_Magazine -= 1;
                                RealAmmoAmount -= 1;
                            }
                        }
                    }
                }
            }

            //������� ���� �߻�
            else if (!isMiniGun && GunSmokeOn == 2 || Input.GetKeyUp(KeyCode.C))
            {
                TurnRolling = false;
                GunSmokeOn = 1;

                if (GunSmokeOn == 1 && rollingTime >= 0.5f)
                {
                    GunSmokeOn = 0;
                    GameObject M3078Smoke = Instantiate(M3078SmokePrefab, M3078SmokePos.position, M3078SmokePos.rotation);
                    M3078Smoke.SetActive(true);
                    Destroy(M3078Smoke, 3);
                    SoundTime = 0;
                    SoundChargeTime = 0;
                }
            }
        }
    }

    //ASC 365 ȭ������ ���
    void ASC365Fire()
    {
        if (isMiniGun || Input.GetKey(KeyCode.C))
        {
            UsingTask = true;
            TaskTime = 0;
            AbandonTime = 0;
            if (ASC365FireTime <= 7)
                ASC365FireTime += Time.deltaTime;
            if (ASCFireRepeatTime <= 3)
                ASCFireRepeatTime += Time.deltaTime;

            if (ASC365FireTime > 0 && ASC365FireTime < 1)
                animator.SetFloat("ASC 365 Attack", 1);
            else if (ASC365FireTime >= 1)
                animator.SetFloat("ASC 365 Attack", 1 + ASC365FireTime);

            if (timeStamp >= ASC365FireRate)
            {
                timeStamp = 0;

                GameObject FlameAmmo = objectManager.LoaderFlameGun();

                FlameAmmo.transform.position = ASC365FlameAmmoPos.position;
                FlameAmmo.transform.rotation = ASC365FlameAmmoPos.rotation; //�߻� �Ѿ� ����
                FlameAmmo.GetComponent<FlameAmmoMovement>().SetDamage(ASC365FlameDamage); //�Ѿ˿��� ������ ����

                ViewSW06_Magazine -= 1;
                RealAmmoAmount -= 1;
            }

            if (ASCFireRepeatTime > 1) //�߻� �� 1�� �ڿ� �߻� �ݺ��Ҹ� �߻�
                ASC365FireRepeat.SetActive(true);

            if (ASC365FireTime > 5) //5�� �̻� �߻�� ���� �߻�
            {
                ASC365SteamSound.SetActive(true);
                animator.SetBool("ASC Smoke", true);
            }
        }
    }

    //�ֹ���� ��ü
    IEnumerator OutputBasicWeapon()
    {
        UsingTask = true;
        CantSwap = true;
        animator.SetBool("Swap(M3078 input)", true);
        animator.SetBool("Reload Stop", false);

        GetComponent<GunController>().StopReload = false;
        GetComponent<Movement>().HeavyWeaponUsing(0); //��ȭ�� ��� ���� ����

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 1 / 0.66f);

        yield return new WaitForSeconds(0.33f);

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(false);
            ASC365FlameStartPrefab.SetActive(false);
            ASC365FlameSmokePrefab.SetActive(false);
        }

        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        if (HeavyWeaponType == 1)
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(true);
        else if (HeavyWeaponType == 2)
            transform.Find("bone_1/ASC 365").gameObject.SetActive(true);
        if (GunType == 1)
        {
            animator.SetFloat("Gun active", 2);
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
        }
        if (GunType == 1000)
        {
            animator.SetFloat("Gun active", 1000);
            transform.Find("bone_1/DS-65").gameObject.SetActive(false);
        }
        if (GunType == 2000)
        {
            animator.SetFloat("Gun active", 2000);
            transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
        }
        if (SubGunTypeFront == 1)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("subGun active", 1);
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
        }
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("Gun active", 0);
            animator.SetFloat("subGun active2", 1);
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
        }

        animator.SetBool("Swap(DT-37 output)", true);
        animator.SetBool("Swap(M3078 input)", false);

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = false;
        PlayerMagazine.gameObject.SetActive(true);
        PlayerMinigunAmmo.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.33f);

        animator.SetBool("Swap(DT-37 output)", false);

        GetComponent<GunController>().LaserGuiding(false); //��ų ��� ���� ����

        GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 0;
        GetComponent<UGG98Controller>().HeavyWeaponUsing = 0;
        GetComponent<MEAGController>().HeavyWeaponUsing = 0;
        GetComponent<Hydra56Controller>().HeavyWeaponUsing = 0;
        GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 0;

        SwichTime = 0.5f;
        SwapTime = 0;
        UsingChangeWeapon = true;
        CantSwap = false;
        UsingTask = false;
        GetComponent<Movement>().FireJoystickType = 1;

        animator.SetBool("Attack rolling M3078, 2", false);
        animator.SetBool("Attack rolling M3078, 1", false);
        animator.SetBool("M3078 end", true);
        animator.SetFloat("Attack start M3078", 0);
        rollingTime = 0;

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", true);
        Invoke("AfterEndCycle", 0.5f); //���� UI ����Ŭ �Ϸ���
        Invoke("ViewCountComplete", 0.5f); //���� UI ����Ŭ �Ϸ�ó��
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 0);

        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);

        yield return new WaitForSeconds(0.5f);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
    }

    //��ȭ��� ��ü
    IEnumerator OuputHeavyWeapon()
    {
        TaskTime = 1;
        UsingTask = true;
        CantSwap = true;
        if (GetComponent<GunController>().reloading == true)
            GetComponent<GunController>().StopReload = true;
        animator.SetFloat("Gun fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        animator.SetBool("Swap(DT-37 input)", true);
        animator.SetBool("M3078 end", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);

        GetComponent<GunController>().LaserGuiding(true); //��ų ��� ���� ����

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 1 / 0.913f);

        yield return new WaitForSeconds(0.33f);

        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        if (GunType == 1)
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
        if (GunType == 1000)
            transform.Find("bone_1/DS-65").gameObject.SetActive(true);
        if (GunType == 2000)
            transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
        if (SubGunTypeFront == 1)
        {
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            animator.SetFloat("subGun active", 0);
        }
        if (SubGunTypeBack == 1)
        {
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            animator.SetFloat("subGun active2", 0);
        }

        if (HeavyWeaponType == 1)
        {
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
            animator.SetFloat("Gun active", 5000);
        }
        else if (HeavyWeaponType == 2)
        {
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);
            animator.SetFloat("Gun active", 5001);
        }

        animator.SetBool("Swap(M3078 output)", true);
        GetComponent<Movement>().HeavyWeaponUsing(50); //��ȭ�� ��� ���� ����

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = true;
        PlayerMagazine.gameObject.SetActive(false);
        PlayerMinigunAmmo.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.583f);

        animator.SetBool("Swap(M3078 output)", false);
        animator.SetBool("Swap(DT-37 input)", false);

        if (HeavyWeaponType == 1 || HeavyWeaponType == 2)
        {
            GameControlSystem.TriggerIcon.SetActive(true);
            GameControlSystem.ChargeIcon.SetActive(false);
        }

        if (HeavyWeaponType == 1)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 1;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 1;
            GetComponent<MEAGController>().HeavyWeaponUsing = 1;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 1;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 1;
        }
        else if (HeavyWeaponType == 2)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 2;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 2;
            GetComponent<MEAGController>().HeavyWeaponUsing = 2;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 2;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 2;
        }

        CantSwap = false;
        UsingChangeWeapon = false;
        SwichTime = 0.5f;
        SwapTime2 = 0;
        GetComponent<Movement>().FireJoystickType = 100;

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", true);
        Invoke("AfterEndCycle", 0.5f); //���� UI ����Ŭ �Ϸ���
        Invoke("ViewCountComplete", 0.5f); //���� UI ����Ŭ �Ϸ�ó��
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Cool time, Swap", 0);

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(true);
            ASC365FlameStartPrefab.SetActive(true);
            ASC365FlameSmokePrefab.SetActive(true);
        }
    }

    //��ȭ�� ȹ��
    public IEnumerator ReadyHeavyWeapon()
    {
        TaskTime = 1;
        UsingTask = true;
        CantSwap = true;
        SwapActive.raycastTarget = false;
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Turn off, Swap", false);
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 2);
        GameControlScript.GetComponent<GameControlSystem>().inWeapon = true;
        GameControlScript.GetComponent<GameControlSystem>().GetHeavyWeapon = true;
        PlayerMagazine.gameObject.SetActive(false);
        PlayerMinigunAmmo.gameObject.SetActive(true);

        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", true);
        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);

        if (HeavyWeaponType == 1)
            animator.SetFloat("Gun active", 5000);
        else if (HeavyWeaponType == 2)
            animator.SetFloat("Gun active", 5001);

        if (HeavyWeaponType == 1 || HeavyWeaponType == 2)
        {
            GameControlSystem.TriggerIcon.SetActive(true);
            GameControlSystem.ChargeIcon.SetActive(false);
        }

        if (GetComponent<GunController>().reloading == true)
            GetComponent<GunController>().StopReload = true;
        animator.SetFloat("Gun fire", 0);
        animator.SetBool("SW-06_Effect1", false);
        animator.SetBool("SW-06_Effect2", false);
        animator.SetBool("SW-06_Effect3", false);
        animator.SetBool("SW-06_Effect4", false);

        if (GunType == 1)
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
        if (GunType == 1000)
            transform.Find("bone_1/DS-65").gameObject.SetActive(true);
        if (GunType == 2000)
            transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
        if (SubGunTypeFront == 1)
        {
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            animator.SetFloat("subGun active", 0);
        }
        if (SubGunTypeBack == 1)
        {
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            animator.SetFloat("subGun active2", 0);
        }


        SoundManager.instance.SFXPlay7("Sound", M3078PickUp);
        GetComponent<GunController>().LaserGuiding(true); //��ų ��� ���� ����
        GetComponent<Movement>().HeavyWeaponUsing(50); //��ȭ�� ��� ���� ����

        //��ų ���� ���
        if (HeavyWeaponType == 1)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 1;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 1;
            GetComponent<MEAGController>().HeavyWeaponUsing = 1;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 1;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 1;
        }
        else if (HeavyWeaponType == 2)
        {
            GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 2;
            GetComponent<UGG98Controller>().HeavyWeaponUsing = 2;
            GetComponent<MEAGController>().HeavyWeaponUsing = 2;
            GetComponent<Hydra56Controller>().HeavyWeaponUsing = 2;
            GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 2;
        }

        UsingTime = 0;
        animator.SetBool("Pick M3078", true);
        yield return new WaitForSeconds(0.75f);
        SwapActive.raycastTarget = true;
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 0);
        animator.SetBool("Pick M3078", false);
        Active = true;
        CantSwap = false;
        GetComponent<Movement>().FireJoystickType = 100;

        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(true);
            ASC365FlameStartPrefab.SetActive(true);
            ASC365FlameSmokePrefab.SetActive(true);
        }
    }

    //��ȭ�� ������
    public IEnumerator AbandonWeapon()
    {
        if (HeavyWeaponType == 2)
        {
            ASC365FlamePrefab.SetActive(false);
            ASC365FlameStartPrefab.SetActive(false);
            ASC365FlameSmokePrefab.SetActive(false);
        }

        UsingTask = true;
        GetComponent<GunController>().StopReload = false;
        animator.SetBool("Reload Stop", false);

        SwapActive.raycastTarget = false;
        WeaponSwapBtn.GetComponent<Animator>().SetFloat("Transform, Swap", 1);
        DashUI.GetComponent<Animator>().SetBool("Cool time start, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);

        SoundManager.instance.SFXPlay14("Sound", M3078End);
        GetComponent<Movement>().HeavyWeaponUsing(51); //��ȭ�� ��� ���� ����
        animator.SetBool("Abandon M3078", true);
        yield return new WaitForSeconds(0.35f);
        if (HeavyWeaponType == 1)
            Instantiate(M3078Prefab, M3078Pos.position, M3078Pos.rotation);
        else if (HeavyWeaponType == 2)
            Instantiate(ASC365Prefab, M3078Pos.position, M3078Pos.rotation);
        animator.SetFloat("Gun active", 0);
        animator.SetBool("Pick M3078", false);
        animator.SetBool("Attack rolling M3078, 1", false);
        animator.SetBool("Attack rolling M3078, 2", false);
        animator.SetFloat("Attack start M3078", 0);
        yield return new WaitForSeconds(0.483f);
        GetComponent<Movement>().HeavyWeaponUsing(0); //��ȭ�� ��� ���� ����
        if (GunType == 1)
        {
            animator.SetFloat("Gun active", 2);
            transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
        }
        if (GunType == 1000)
        {
            animator.SetFloat("Gun active", 1000);
            transform.Find("bone_1/DS-65").gameObject.SetActive(false);
        }
        if (GunType == 2000)
        {
            animator.SetFloat("Gun active", 2000);
            transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
        }
        if (SubGunTypeFront == 1)
        {
            animator.SetFloat("subGun active", 1);
            transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
        }
        if (SubGunTypeBack == 1)
        {
            animator.SetFloat("subGun active2", 1);
            transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Abandon M3078", false);
        RealAmmoAmount = -1;
        this.GetComponent<HeavyWeaponSupport>().TimeStart = 1;

        GameControlSystem.TriggerIcon.SetActive(true);
        GameControlSystem.ChargeIcon.SetActive(false);

        GetComponent<GunController>().LaserGuiding(false); //��ų ��� ���� ����

        GetComponent<ArthesL775Controller>().HeavyWeaponUsing = 0;
        GetComponent<UGG98Controller>().HeavyWeaponUsing = 0;
        GetComponent<MEAGController>().HeavyWeaponUsing = 0;
        GetComponent<Hydra56Controller>().HeavyWeaponUsing = 0;
        GetComponent<VM5GrenadeController>().HeavyWeaponUsing = 0;

        GameControlScript.GetComponent<GameControlSystem>().inWeapon = false;
        GameControlScript.GetComponent<GameControlSystem>().GetHeavyWeapon = false;
        PlayerMagazine.gameObject.SetActive(true);
        PlayerMinigunAmmo.gameObject.SetActive(false);

        GetComponent<Movement>().FireJoystickType = 1;
        UsingChangeWeapon = false;
        Active = false;
        if (TaskTime == 0)
        {
            TaskTime += Time.deltaTime;
            UsingTask = false;
        }

        if (HeavyWeaponType == 1)
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
        else if (HeavyWeaponType == 2)
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);

        WeaponSwapBtn.GetComponent<Animator>().SetBool("Turn off, Swap", true);

        DashUI.GetComponent<Animator>().SetBool("Cool time running, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", true);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);

        yield return new WaitForSeconds(0.5f);

        DashUI.GetComponent<Animator>().SetBool("Cool time end, Dash", false);
        ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
    }

    //ź�� ����
    public void EjectShell()
    {
        ejectedShell = objectManager.Loader("MiniGunShell");
        ejectedShell.transform.position = ejectPos.transform.position;
        ejectedShell.transform.rotation = ejectPos.transform.rotation;
        ShellCase_SW06 ShellCase_SW06 = ejectedShell.GetComponent<ShellCase_SW06>();
        ShellCase_SW06.Pos = ejectedShell.transform.position.y;

        float xVnot = Random.Range(5f, 10f);
        float yVnot = Random.Range(5f, 10f);

        ejectedShell.GetComponent<ShellCase_SW06>().xVnot = xVnot;
        ejectedShell.GetComponent<ShellCase_SW06>().yVnot = yVnot;
    }

    //���� UI ����Ŭ �Ϸ���
    void AfterEndCycle()
    {
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
    }

    //���� UI ����Ŭ �Ϸ�ó��
    void ViewCountComplete()
    {
        WeaponSwapBtn.GetComponent<Animator>().SetBool("Cool time cycle count, Swap", false);
    }
}