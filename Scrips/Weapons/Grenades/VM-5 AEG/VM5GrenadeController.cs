using System.Collections;
using UnityEngine;

public class VM5GrenadeController : MonoBehaviour
{
    public GameControlSystem GameControlSystem;
    GameControlSystem gameControlSystem;
    ArthesL775Controller arthesL775Controller;
    Hydra56Controller hydra56Controller;
    MEAGController meagController;
    UGG98Controller ugg98Controller;
    Animator animator;
    Movement movement;

    public GameObject ReloadUI;
    public GameObject SwapUI;
    public GameObject AnimationUIGrenade;

    public int Damage;
    public int GunType; //기본 총 타입을 전달받기 위한 변수
    public int SubGunTypeFront = 0; //앞 기관단총 장착여부
    public int SubGunTypeBack = 0; //뒤 기관단총 장착여부
    public int BombCount; // 수류탄 개수 
    public float BombCoolTime; // 수류탄이 채워지는 쿨 
    public float BombTime;
    public float Power;
    private float BombCharge;
    private float ThrowTime;
    private float MarkTime;

    public GameObject GrenadePrefab;
    public GameObject GrenadeCoverPrefab;
    public GameObject GrenadePinPrefab;
    GameObject Mark;
    public GameObject GrenadeMarkPrefab;
    public Transform GrenadeMarkPos;
    public Transform GrenadePrefabPos;

    public bool Ready = true;
    public bool isBomb;
    public bool UsingChangeWeapon = false;
    public bool Reload;
    public bool UsingTask;
    public bool isShoot;
    private bool ThrowEnd = false; //던졌을 때 중복 던져지지 않도록 방지
    public bool WeaponOn;
    public bool VehicleActive;
    private bool Click;

    public int HeavyWeaponUsing;
    public int xVnotMinusRandom;
    public int xVnotPlusRandom;
    public int yVnotMinusRandom;
    public int yVnotPlusRandom;
    public int rotationMinusRandom;
    public int rotationPlusRandom;

    Coroutine grenadeReady;
    Coroutine grenadeOff;

    public int MovingState;

    public AudioClip PinOff;
    public AudioClip Beep1;
    public AudioClip Beep2;

    public void ReloadTime(bool Reloading)
    {
        if (Reloading == true)
        {
            Reload = true;
        }
        else
        {
            Reload = false;
        }
    }

    //X축 반전 원격 제어
    public void XMoveStop(bool ChangeWeaponUsing)
    {
        if (ChangeWeaponUsing == true)
        {
            UsingChangeWeapon = true;
        }
        else
        {
            UsingChangeWeapon = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = FindObjectOfType<Movement>();
        gameControlSystem = FindObjectOfType<GameControlSystem>();
        arthesL775Controller = FindObjectOfType<ArthesL775Controller>();
        hydra56Controller = FindObjectOfType<Hydra56Controller>();
        meagController = FindObjectOfType<MEAGController>();
        ugg98Controller = FindObjectOfType<UGG98Controller>();

        Damage = UpgradeDataSystem.instance.VM5AEGDamage;
    }

    void Update()
    {
        StartCoroutine(Bomb()); //수류탄 투척
        if (VehicleActive == false)
            Bomb_Cool(); //수류탄 쿨 함수
    }

    public void BombUp()
    {
        if (Click == true)
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Click, Grenade", false);
        Click = false;
    }

    public void BombDown()
    {
        Click = true;
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        AnimationUIGrenade.GetComponent<Animator>().SetBool("Click, Grenade", true);
    }

    public void BombEnter()
    {
        if (Click == true)
        {
            SoundManager.instance.SFXPlay2("Sound", Beep1);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Click, Grenade", true);
        }
    }

    public void BombExit()
    {
        if (Click == true)
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Click, Grenade", false);
    }

    public void BombClick()
    {
        if (BombCount > 0 && UsingChangeWeapon == false)
        {
            if (!isBomb) //활성화
            {
                UsingTask = true;

                GameControlSystem.TriggerIcon.SetActive(false);
                GameControlSystem.ChargeIcon.SetActive(true);

                if (gameControlSystem.isChangeWeapon == true)
                    gameControlSystem.ChangeWeaponOff();
                GetComponent<GunController>().LaserGuiding(true); //스킬 사용 상태 전달

                //체인지 스킬 제한 목록
                GetComponent<Hydra56Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<UGG98Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<MEAGController>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<ArthesL775Controller>().XMoveStop(true); //체인지 스킬 사용 전달
                GetComponent<M3078Controller>().TurnOff();

                ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", true);
                ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", true);
                SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", true);
                SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", true);

                if (AnimationUIGrenade.GetComponent<Animator>().GetBool("Active(offline), Grenade") == true)
                    AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", false);
                AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(start), Grenade", true);
                WeaponOn = true;
                GetComponent<Movement>().FireJoystickType = 0;
                if (HeavyWeaponUsing == 0)
                {
                    if (GetComponent<GunController>().reloading == true)
                        GetComponent<GunController>().StopReload = true;
                    animator.SetFloat("Gun fire", 0);
                    animator.SetBool("SW-06_Effect1", false);
                    animator.SetBool("SW-06_Effect2", false);
                    animator.SetBool("SW-06_Effect3", false);
                    animator.SetBool("SW-06_Effect4", false);
                }
                else if (HeavyWeaponUsing >= 1)
                {
                    animator.SetFloat("Gun active", 0);
                }

                animator.SetBool("Grenade off, player", false);
                if (HeavyWeaponUsing == 0)
                {
                    if (GunType == 1)
                        transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
                    if (GunType == 1000)
                        transform.Find("bone_1/DS-65").gameObject.SetActive(false);
                    if (GunType == 2000)
                        transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
                    if (SubGunTypeFront == 1)
                        transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
                    if (SubGunTypeBack == 1)
                        transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
                    //transform.Find("Guns").gameObject.SetActive(true);
                    GetComponent<Movement>().StartAction = false;
                }

                if (grenadeOff != null)
                    StopCoroutine(grenadeOff);
                grenadeReady = StartCoroutine(GrenadeReady());
            }

            if (isBomb) //비활성화
            {
                SoundManager.instance.SFXPlay2("Sound", Beep2);
                if (AnimationUIGrenade.GetComponent<Animator>().GetBool("Active(start), Grenade") == true)
                    AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(start), Grenade", false);
                AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", true);
                animator.SetBool("Grenade ready, player", false);

                if (HeavyWeaponUsing == 0)
                {
                    if (GunType == 1)
                        transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
                    if (GunType == 1000)
                        transform.Find("bone_1/DS-65").gameObject.SetActive(true);
                    if (GunType == 2000)
                        transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
                    if (SubGunTypeFront == 1)
                        transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
                    if (SubGunTypeBack == 1)
                        transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
                    //transform.Find("Guns").gameObject.SetActive(false);

                    GetComponent<Movement>().FireJoystickType = 1;
                    GetComponent<Movement>().StartAction = true;
                }
                else if (HeavyWeaponUsing >= 1)
                    GetComponent<Movement>().FireJoystickType = 100;

                WeaponOn = false;

                if (grenadeReady != null)
                    StopCoroutine(grenadeReady);
                grenadeOff = StartCoroutine(GrenadeOff());
            }
            isBomb = !isBomb;
        }
    }

    public void BombOff()
    {
        if (AnimationUIGrenade.GetComponent<Animator>().GetBool("Active(start), Grenade") == true)
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(start), Grenade", false);
        AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", true);
        animator.SetBool("Grenade ready, player", false);

        if (HeavyWeaponUsing == 0)
        {
            if (GunType == 1)
                transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
            if (GunType == 1000)
                transform.Find("bone_1/DS-65").gameObject.SetActive(true);
            if (GunType == 2000)
                transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
            if (SubGunTypeFront == 1)
                transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            if (SubGunTypeBack == 1)
                transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            //transform.Find("Guns").gameObject.SetActive(false);

            GetComponent<Movement>().FireJoystickType = 1;
            GetComponent<Movement>().StartAction = true;
        }
        else if (HeavyWeaponUsing >= 1)
            GetComponent<Movement>().FireJoystickType = 100;

        WeaponOn = false;

        if (grenadeReady != null)
            StopCoroutine(grenadeReady);
        grenadeOff = StartCoroutine(GrenadeOff());
        isBomb = false;
    }

    IEnumerator GrenadeReady()
    {
        GetComponent<Movement>().HeavyWeaponOnline = 100; //수류탄 사용 상태 전달
        animator.SetBool("Grenade ready, player", true);
        if (HeavyWeaponUsing == 0)
            animator.SetFloat("Change active number", 1000);
        else if (HeavyWeaponUsing == 1)
            animator.SetFloat("Change active number", 2000);
        else if (HeavyWeaponUsing == 2)
            animator.SetFloat("Change active number", 2001);

        yield return new WaitForSeconds(0.40f);
        if (HeavyWeaponUsing == 0)
        {
            if (GunType == 1)
                transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(true);
            if (GunType == 1000)
                transform.Find("bone_1/DS-65").gameObject.SetActive(true);
            if (GunType == 2000)
                transform.Find("bone_1/DP-9007").gameObject.SetActive(true);
            if (SubGunTypeFront == 1)
                transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(true);
            if (SubGunTypeBack == 1)
                transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(true);
            //transform.Find("Guns").gameObject.SetActive(false);
        }
        else if (HeavyWeaponUsing == 1)
        {
            animator.SetFloat("Gun active", 0);
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(true);
        }
        else if (HeavyWeaponUsing == 2)
        {
            animator.SetFloat("Gun active", 0);
            transform.Find("bone_1/ASC 365").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.243f);
        animator.SetBool("Grenade ready, player", false);
        animator.SetFloat("Change active number", 0);

        if (HeavyWeaponUsing == 0)
            GetComponent<Movement>().StartAction = true;

        AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(start), Grenade", false);
    }

    IEnumerator GrenadeOff()
    {
        animator.SetFloat("Change active number", 0);
        if (HeavyWeaponUsing == 0)
        {
            ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);
        }
        else if (HeavyWeaponUsing >= 1)
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);

        if (HeavyWeaponUsing == 0)
        {
            GetComponent<Movement>().HeavyWeaponOnline = 0; //수류탄 사용 상태 해제 전달
            animator.SetBool("Grenade off, player", true);
            yield return new WaitForSeconds(0.25f);
            if (GunType == 1)
                transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
            if (GunType == 1000)
                transform.Find("bone_1/DS-65").gameObject.SetActive(false);
            if (GunType == 2000)
                transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
            if (SubGunTypeFront == 1)
                transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
            if (SubGunTypeBack == 1)
                transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
            //transform.Find("Guns").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.416f);
            animator.SetBool("Grenade off, player", false);
            if (animator.GetBool("Reload Stop") == true)
                animator.SetBool("Reload Stop", false);
            GetComponent<Movement>().StartAction = false;
            GetComponent<GunController>().StopReload = false;
            animator.SetBool("Reload Stop", false);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", false);
        }
        else if (HeavyWeaponUsing == 1)
        {
            GetComponent<Movement>().HeavyWeaponOnline = 50;
            animator.SetBool("Grenade off, player", true);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("Gun active", 5000);
            animator.SetBool("Swap(M3078 output)", true);
            transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
        }
        else if (HeavyWeaponUsing == 2)
        {
            GetComponent<Movement>().HeavyWeaponOnline = 51;
            animator.SetBool("Grenade off, player", true);
            yield return new WaitForSeconds(0.25f);
            animator.SetFloat("Gun active", 5001);
            animator.SetBool("Swap(M3078 output)", true);
            transform.Find("bone_1/ASC 365").gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.416f);
        animator.SetBool("Swap(M3078 output)", false);
        animator.SetBool("Grenade off, player", false);
        AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", false);

        if (HeavyWeaponUsing == 0)
        {
            ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", false);

            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        }
        else if (HeavyWeaponUsing >= 1)
        {
            SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", true);
        }

        GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 해제 전달
        GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
        GetComponent<M3078Controller>().TurnOn();

        GameControlSystem.TriggerIcon.SetActive(true);
        GameControlSystem.ChargeIcon.SetActive(false);

        UsingTask = false;

        yield return new WaitForSeconds(0.5f);

        if (HeavyWeaponUsing == 0)
        {
            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
        }
        else if (HeavyWeaponUsing >= 1)
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
    }

    void Bomb_Cool() //폭탄 쿨타임 함수 
    {
        if (BombCount <= 0)
        {
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time start, Grenade", true);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time running, Grenade", true);
            AnimationUIGrenade.GetComponent<Animator>().SetFloat("Cool time, Grenade", 1 / BombCoolTime);
            BombTime += Time.deltaTime;
        }
        if (BombTime > BombCoolTime)
        {
            BombTime = 0;
            BombCount++;
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time end, Grenade", true);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time cycle count, Grenade", true);
            Invoke("AfterEndCycle", 0.5f);
            Invoke("ViewCountComplete", 0.5f);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time start, Grenade", false);
            AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time running, Grenade", false);
            AnimationUIGrenade.GetComponent<Animator>().SetFloat("Cool time, Grenade", 0);
        }
    }

    void AfterEndCycle()
    {
        AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time end, Grenade", false);
    }

    void ViewCountComplete()
    {
        AnimationUIGrenade.GetComponent<Animator>().SetBool("Cool time cycle count, Grenade", false);
    }

    //수류탄 투척
    public IEnumerator Bomb()
    {
        if (WeaponOn && movement.FireJoystick && BombCount == 1 || WeaponOn && movement.FireJoystick & Input.GetKeyDown(KeyCode.X))
        {
            if(Reload == false && ThrowEnd == false)
            {
                if (BombCharge < 1)
                {
                    BombCharge += Time.deltaTime;
                    Power += 20 * Time.deltaTime;
                }

                if (BombCount > 0)
                {
                    animator.SetBool("Grenade charge, player", true);

                    if (MarkTime == 0)
                    {
                        MarkTime += Time.deltaTime;
                        Mark = Instantiate(GrenadeMarkPrefab, GrenadeMarkPos.position, GrenadeMarkPos.rotation);
                        Mark.GetComponent<GrenadeMarkMove>().Throw(true);
                    }
                }
                else
                {
                    //Debug.Log("수류탄이 없습니다");
                }
            }
        }
        else if (BombCharge > 0)
        {
            ThrowEnd = true;
            ReloadUI.GetComponent<Animator>().SetBool("Cool time start, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time start, Swap", false);

            if (HeavyWeaponUsing == 0)
            {
                GetComponent<Movement>().XMoveStop(true); //X반전 상태 전달
                GetComponent<Movement>().HeavyWeaponUsing(0); //수류탄 사용 상태 해제 전달
                GetComponent<Movement>().StartAction = false;
            }
            else if (HeavyWeaponUsing == 1 || HeavyWeaponUsing == 2)
                GetComponent<Movement>().HeavyWeaponOnline = 50;

            if (BombCharge > 0.5f)
            {
                animator.SetBool("Grenade charge, player", false);
                animator.SetBool("Grenade throw(long), player", true);
                yield return new WaitForSeconds(0.16f);
                if (ThrowTime == 0)
                {
                    ThrowTime += Time.deltaTime;
                    BombCount--;
                    BombCharge = 0;
                    SoundManager.instance.SFXPlay11("Sound", PinOff);
                    ThrowGrenade();
                }
            }
            else
            {
                animator.SetBool("Grenade charge, player", false);
                animator.SetBool("Grenade throw(short), player", true);
                yield return new WaitForSeconds(0.16f);
                if (ThrowTime == 0)
                {
                    ThrowTime += Time.deltaTime;
                    BombCount--;
                    BombCharge = 0;
                    SoundManager.instance.SFXPlay11("Sound", PinOff);
                    ThrowGrenade();
                }
            }

            BombCharge = 0;

            if (HeavyWeaponUsing == 0)
                yield return new WaitForSeconds(0.75f);
            else if (HeavyWeaponUsing == 1 || HeavyWeaponUsing == 2)
                yield return new WaitForSeconds(0.6f);

            GetComponent<Movement>().XMoveStop(false); //X반전 상태 해제 전달

            if (HeavyWeaponUsing == 0)
            {
                transform.Find("Guns").gameObject.SetActive(true);
                if (GunType == 1)
                    transform.Find("bone_1/DT(Delta Hurricane)-37").gameObject.SetActive(false);
                if (GunType == 1000)
                    transform.Find("bone_1/DS-65").gameObject.SetActive(false);
                if (GunType == 2000)
                    transform.Find("bone_1/DP-9007").gameObject.SetActive(false);
                if (SubGunTypeFront == 1)
                    transform.Find("bone_1/subMacine gun(front)").gameObject.SetActive(false);
                if (SubGunTypeBack == 1)
                    transform.Find("bone_1/subMacine gun(back)").gameObject.SetActive(false);
            }
            else if (HeavyWeaponUsing == 1)
            {
                if (arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false)
                    animator.SetFloat("Gun active", 5000);
                animator.SetBool("Swap(M3078 output)", true);
                transform.Find("bone_1/M3078 mini gun").gameObject.SetActive(false);
            }
            else if (HeavyWeaponUsing == 2)
            {
                if (arthesL775Controller.UsingTask == false && hydra56Controller.UsingTask == false && meagController.UsingTask == false && ugg98Controller.UsingTask == false)
                    animator.SetFloat("Gun active", 5001);
                animator.SetBool("Swap(M3078 output)", true);
                transform.Find("bone_1/ASC 365").gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.13f);
            if (HeavyWeaponUsing == 0)
                GetComponent<GunController>().StopReload = false;
            animator.SetBool("Reload Stop", false);

            if (animator.GetBool("Grenade charge, player") == true)
                animator.SetBool("Grenade charge, player", false);
            if (animator.GetBool("Grenade throw(long), player") == true)
                animator.SetBool("Grenade throw(long), player", false);
            if (animator.GetBool("Grenade throw(short), player") == true)
                animator.SetBool("Grenade throw(short), player", false);

            if (HeavyWeaponUsing == 0)
                GetComponent<Movement>().FireJoystickType = 1;
            else if (HeavyWeaponUsing == 1 || HeavyWeaponUsing == 2)
                GetComponent<Movement>().FireJoystickType = 100;

            if (HeavyWeaponUsing == 1 || HeavyWeaponUsing == 2)
            {
                yield return new WaitForSeconds(0.286f);
                animator.SetBool("Swap(M3078 output)", false);
            }

            isBomb = false;
            WeaponOn = false;
            ThrowEnd = false;
            ThrowTime = 0;
            Power = 0;
            MarkTime = 0;

            if (AnimationUIGrenade.GetComponent<Animator>().GetBool("Active(start), Grenade") == true)
                AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(start), Grenade", false);
            if (AnimationUIGrenade.GetComponent<Animator>().GetBool("Active(offline), Grenade") == true)
                AnimationUIGrenade.GetComponent<Animator>().SetBool("Active(offline), Grenade", false);

            ReloadUI.GetComponent<Animator>().SetBool("Cool time running, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time running, Swap", false);

            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", true);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", true);

            GetComponent<GunController>().LaserGuiding(false); //스킬 사용 상태 해제 전달
            GetComponent<Hydra56Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
            GetComponent<UGG98Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
            GetComponent<MEAGController>().XMoveStop(false); //체인지 스킬 사용 해제 전달
            GetComponent<ArthesL775Controller>().XMoveStop(false); //체인지 스킬 사용 해제 전달
            GetComponent<M3078Controller>().TurnOn();

            GameControlSystem.TriggerIcon.SetActive(true);
            GameControlSystem.ChargeIcon.SetActive(false);

            UsingTask = false;

            yield return new WaitForSeconds(0.5f);

            ReloadUI.GetComponent<Animator>().SetBool("Cool time end, Reload", false);
            SwapUI.GetComponent<Animator>().SetBool("Cool time end, Swap", false);
        }
    }

    //수류탄 배출
    public void ThrowGrenade()
    {
        GameObject VM5 = Instantiate(GrenadePrefab, GrenadePrefabPos.position, GrenadePrefabPos.rotation);
        GameObject VM5Cover = Instantiate(GrenadeCoverPrefab, GrenadePrefabPos.position, GrenadePrefabPos.rotation);
        GameObject VM5Pin = Instantiate(GrenadePinPrefab, GrenadePrefabPos.position, GrenadePrefabPos.rotation);
        VM5.GetComponent<ExplosionVM5>().damage = Damage;
        Destroy(VM5Cover, 30);
        Destroy(VM5Pin, 30);
        Mark.GetComponent<GrenadeMarkMove>().Throw(false);
        Destroy(Mark);

        float xVnot = Random.Range(xVnotMinusRandom, xVnotPlusRandom);
        float yVnot = Random.Range(yVnotMinusRandom, yVnotPlusRandom);
        float rotationSpeed = Random.Range(rotationMinusRandom, rotationPlusRandom);

        if (transform.rotation.y == 0)
        {
            Power *= 1;
            rotationSpeed *= 1;
        }
        else
        {
            Power *= -1;
            if (Power < -10)
                Power -= 3;
            rotationSpeed *= -1;
        }


        VM5.GetComponent<VM5Throw>().rotationSpeed = rotationSpeed;
        VM5.GetComponent<VM5Throw>().xVnot = Power;
        VM5.GetComponent<VM5Throw>().yVnot = yVnot + (Power / 5);

        VM5Cover.GetComponent<VM5Throw>().rotationSpeed = rotationSpeed;
        VM5Cover.GetComponent<VM5Throw>().xVnot = xVnot/1.8f;
        VM5Cover.GetComponent<VM5Throw>().yVnot = yVnot/1.8f;

        VM5Pin.GetComponent<VM5Throw>().rotationSpeed = rotationSpeed;
        VM5Pin.GetComponent<VM5Throw>().xVnot = xVnot / 3f;
        VM5Pin.GetComponent<VM5Throw>().yVnot = yVnot / 3f;
    }
}
