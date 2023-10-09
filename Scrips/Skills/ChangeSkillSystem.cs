using System.Collections;
using UnityEngine;

public class ChangeSkillSystem : MonoBehaviour
{
    public GameControlSystem GameControlSystem;
    GunController gunController;

    public Joystick ChangeSkillJoystick;
    public GameObject Player;

    Coroutine upStart;
    Coroutine downStart;
    Coroutine rightStart;
    Coroutine leftStart;

    public float Once;

    public bool UsingChangeWeapon = false;
    public bool ArthesL775WeaponOn = false;
    public bool Hydra56WeaponOn = false;
    public bool MEAGWeaponOn = false;
    public bool UGG98WeaponOn = false;
    public bool isChange;
    public bool isSelect;

    public bool isCGWTop;
    public bool isCGWDown;
    public bool isCGWRight;
    public bool isCGWLeft;

    public GameObject CHW;
    public GameObject CHWWeapon;

    public GameObject PlayerMagazine;
    public GameObject PlayerMinigunAmmo;
    public GameObject ChangeSkillEnergyBoard; //체인지 중화기 에너지보드

    public AudioClip Beep1;

    public void selectUp()
    {
        isSelect = false;
    }

    public void selectDown()
    {
        isSelect = true;
    }

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        isSelect = true;
    }

    void Update()
    {
        if (ChangeSkillJoystick.Vertical >= .60f && UsingChangeWeapon == false) //위쪽
        {
            if (WeaponUnlockManager.instance.ChangeWeaponTopSlotUnlock == true)
            {
                CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 1);
                CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 0);
                CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
            }
            isCGWTop = false;
            isCGWRight = true;
            isCGWDown = true;
            isCGWLeft = true;
        }
        if (ChangeSkillJoystick.Vertical <= -.60f && UsingChangeWeapon == false) //아래쪽
        {
            if (WeaponUnlockManager.instance.ChangeWeaponDownSlotUnlock == true)
            {
                CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 2);
                CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 0);
                CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
            }
            isCGWDown = false;
            isCGWTop = true;
            isCGWRight = true;
            isCGWLeft = true;
        }
        if (ChangeSkillJoystick.Horizontal >= .60f && UsingChangeWeapon == false) //오른쪽
        {
            if (WeaponUnlockManager.instance.ChangeWeaponRightSlotUnlock == true)
            {
                CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 3);
                CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 0);
                CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
            }
            isCGWRight = false;
            isCGWTop = true;
            isCGWDown = true;
            isCGWLeft = true;
        }
        if (ChangeSkillJoystick.Horizontal <= -.60f && UsingChangeWeapon == false) //왼쪽
        {
            if (WeaponUnlockManager.instance.ChangeWeaponLeftSlotUnlock == true)
            {
                CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 4);
                CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 0);
                CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
            }
            isCGWLeft = false;
            isCGWTop = true;
            isCGWRight = true;
            isCGWDown = true;
        }


        if (!isCGWTop && !isSelect && UsingChangeWeapon == false && WeaponUnlockManager.instance.ArthesL775Unlock == true) //Arthes L-775 가동
        {
            if (Once == 0)
            {
                Once += Time.deltaTime;
                if (gunController.reloading == true)
                    Player.GetComponent<GunController>().StopReload = true;
                ChangeSkillEnergyBoard.gameObject.SetActive(true);
                PlayerMagazine.gameObject.SetActive(false);
                PlayerMinigunAmmo.gameObject.SetActive(false);
                Player.GetComponent<Hydra56Controller>().Seleted = false;
                isSelect = true;
                CHW.GetComponent<Animator>().SetBool("Active(run), CHW", true);
                Invoke("IconRun", 0.25f);
                upStart = StartCoroutine(UpStart());
                GameControlSystem.TriggerIcon.SetActive(false);
                GameControlSystem.ChargeIcon.SetActive(true);

                if (Hydra56WeaponOn == false && MEAGWeaponOn == false && UGG98WeaponOn == false)
                {
                    ArthesL775WeaponOn = true;
                    Player.GetComponent<ArthesL775Controller>().WeaponOn = true;
                    GetComponent<GameControlSystem>().ArthesL775WeaponOn = true;
                }
                else if (Hydra56WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    Hydra56WeaponOn = false;
                    ArthesL775WeaponOn = true;
                    Player.GetComponent<Hydra56Controller>().ChangeOff();
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeStart = 2;
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeEnd = 1;
                    Player.GetComponent<ArthesL775Controller>().ChangeOn();
                    Player.GetComponent<Hydra56Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().Hydra56WeaponOn = false;
                }
                else if (MEAGWeaponOn == true)
                {
                    MEAGWeaponOn = false;
                    ArthesL775WeaponOn = true;
                    Player.GetComponent<MEAGController>().ChangeOff();
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeStart = 3;
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeEnd = 1;
                    Player.GetComponent<ArthesL775Controller>().ChangeOn();
                    Player.GetComponent<MEAGController>().WeaponOn = false;
                    GetComponent<GameControlSystem>().MEAGWeaponOn = false;
                }
                else if (UGG98WeaponOn == true)
                {
                    UGG98WeaponOn = false;
                    ArthesL775WeaponOn = true;
                    Player.GetComponent<UGG98Controller>().ChangeOff();
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeStart = 4;
                    Player.GetComponent<ArthesL775Controller>().SwitchTypeEnd = 1;
                    Player.GetComponent<ArthesL775Controller>().ChangeOn();
                    Player.GetComponent<UGG98Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().UGG98WeaponOn = false;
                }

                Invoke("OnceTime", 1);
            }
        }

        if (!isCGWLeft && !isSelect && UsingChangeWeapon == false && WeaponUnlockManager.instance.Hydra56Unlock == true) //Hydra-56 가동
        {
            if (Once == 0)
            {
                Once += Time.deltaTime;
                if (gunController.reloading == true)
                    Player.GetComponent<GunController>().StopReload = true;
                ChangeSkillEnergyBoard.gameObject.SetActive(true);
                PlayerMagazine.gameObject.SetActive(false);
                PlayerMinigunAmmo.gameObject.SetActive(false);
                Player.GetComponent<Hydra56Controller>().Seleted = true;
                isSelect = true;
                CHW.GetComponent<Animator>().SetBool("Active(run), CHW", true);
                Invoke("IconRun", 0.25f);
                leftStart = StartCoroutine(LeftStart());
                GameControlSystem.TriggerIcon.SetActive(true);
                GameControlSystem.ChargeIcon.SetActive(false);

                if (ArthesL775WeaponOn == false && MEAGWeaponOn == false && UGG98WeaponOn == false)
                {
                    Hydra56WeaponOn = true;
                    Player.GetComponent<Hydra56Controller>().WeaponOn = true;
                    GetComponent<GameControlSystem>().Hydra56WeaponOn = true;
                }
                else if (ArthesL775WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    ArthesL775WeaponOn = false;
                    Hydra56WeaponOn = true;
                    Player.GetComponent<ArthesL775Controller>().ChangeOff();
                    Player.GetComponent<Hydra56Controller>().SwitchTypeStart = 1;
                    Player.GetComponent<Hydra56Controller>().SwitchTypeEnd = 2;
                    Player.GetComponent<Hydra56Controller>().ChangeOn();
                    Player.GetComponent<ArthesL775Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().ArthesL775WeaponOn = false;
                }
                else if (MEAGWeaponOn == true)
                {
                    MEAGWeaponOn = false;
                    Hydra56WeaponOn = true;
                    Player.GetComponent<MEAGController>().ChangeOff();
                    Player.GetComponent<Hydra56Controller>().SwitchTypeStart = 3;
                    Player.GetComponent<Hydra56Controller>().SwitchTypeEnd = 2;
                    Player.GetComponent<Hydra56Controller>().ChangeOn();
                    Player.GetComponent<MEAGController>().WeaponOn = false;
                    GetComponent<GameControlSystem>().MEAGWeaponOn = false;
                }
                else if (UGG98WeaponOn == true)
                {
                    UGG98WeaponOn = false;
                    Hydra56WeaponOn = true;
                    Player.GetComponent<UGG98Controller>().ChangeOff();
                    Player.GetComponent<Hydra56Controller>().SwitchTypeStart = 4;
                    Player.GetComponent<Hydra56Controller>().SwitchTypeEnd = 2;
                    Player.GetComponent<Hydra56Controller>().ChangeOn();
                    Player.GetComponent<UGG98Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().UGG98WeaponOn = false;
                }

                Invoke("OnceTime", 1);
            }
        }

        if (!isCGWRight && !isSelect && UsingChangeWeapon == false && WeaponUnlockManager.instance.MEAGUnlock == true) //MEAG 가동
        {
            if (Once == 0)
            {
                Once += Time.deltaTime;
                if (gunController.reloading == true)
                    Player.GetComponent<GunController>().StopReload = true;
                ChangeSkillEnergyBoard.gameObject.SetActive(true);
                PlayerMagazine.gameObject.SetActive(false);
                PlayerMinigunAmmo.gameObject.SetActive(false);
                Player.GetComponent<Hydra56Controller>().Seleted = false;
                isSelect = true;
                CHW.GetComponent<Animator>().SetBool("Active(run), CHW", true);
                Invoke("IconRun", 0.25f);
                rightStart = StartCoroutine(RightStart());
                GameControlSystem.TriggerIcon.SetActive(false);
                GameControlSystem.ChargeIcon.SetActive(true);

                if (ArthesL775WeaponOn == false && Hydra56WeaponOn == false && UGG98WeaponOn == false)
                {
                    MEAGWeaponOn = true;
                    Player.GetComponent<MEAGController>().WeaponOn = true;
                    GetComponent<GameControlSystem>().MEAGWeaponOn = true;
                }
                else if (ArthesL775WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    ArthesL775WeaponOn = false;
                    MEAGWeaponOn = true;
                    Player.GetComponent<ArthesL775Controller>().ChangeOff();
                    Player.GetComponent<MEAGController>().SwitchTypeStart = 1;
                    Player.GetComponent<MEAGController>().SwitchTypeEnd = 3;
                    Player.GetComponent<MEAGController>().ChangeOn();
                    Player.GetComponent<ArthesL775Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().ArthesL775WeaponOn = false;
                }
                else if (Hydra56WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    Hydra56WeaponOn = false;
                    MEAGWeaponOn = true;
                    Player.GetComponent<Hydra56Controller>().ChangeOff();
                    Player.GetComponent<MEAGController>().SwitchTypeStart = 2;
                    Player.GetComponent<MEAGController>().SwitchTypeEnd = 3;
                    Player.GetComponent<MEAGController>().ChangeOn();
                    Player.GetComponent<Hydra56Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().Hydra56WeaponOn = false;
                }
                else if (UGG98WeaponOn == true)
                {
                    UGG98WeaponOn = false;
                    MEAGWeaponOn = true;
                    Player.GetComponent<UGG98Controller>().ChangeOff();
                    Player.GetComponent<MEAGController>().SwitchTypeStart = 4;
                    Player.GetComponent<MEAGController>().SwitchTypeEnd = 3;
                    Player.GetComponent<MEAGController>().ChangeOn();
                    Player.GetComponent<UGG98Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().UGG98WeaponOn = false;
                }

                Invoke("OnceTime", 1);
            }
        }

        if (!isCGWDown && !isSelect && UsingChangeWeapon == false && WeaponUnlockManager.instance.UGG98Unlock == true) //UGG 98 가동
        {
            if (Once == 0)
            {
                Once += Time.deltaTime;
                if (gunController.reloading == true)
                    Player.GetComponent<GunController>().StopReload = true;
                ChangeSkillEnergyBoard.gameObject.SetActive(true);
                PlayerMagazine.gameObject.SetActive(false);
                PlayerMinigunAmmo.gameObject.SetActive(false);
                Player.GetComponent<Hydra56Controller>().Seleted = false;
                isSelect = true;
                CHW.GetComponent<Animator>().SetBool("Active(run), CHW", true);
                Invoke("IconRun", 0.25f);
                downStart = StartCoroutine(DownStart());
                GameControlSystem.TriggerIcon.SetActive(false);
                GameControlSystem.ChargeIcon.SetActive(true);

                if (ArthesL775WeaponOn == false && Hydra56WeaponOn == false && MEAGWeaponOn == false)
                {
                    UGG98WeaponOn = true;
                    Player.GetComponent<UGG98Controller>().WeaponOn = true;
                    GetComponent<GameControlSystem>().UGG98WeaponOn = true;
                }
                else if (ArthesL775WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    ArthesL775WeaponOn = false;
                    UGG98WeaponOn = true;
                    Player.GetComponent<ArthesL775Controller>().ChangeOff();
                    Player.GetComponent<UGG98Controller>().SwitchTypeStart = 1;
                    Player.GetComponent<UGG98Controller>().SwitchTypeEnd = 4;
                    Player.GetComponent<UGG98Controller>().ChangeOn();
                    Player.GetComponent<ArthesL775Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().ArthesL775WeaponOn = false;
                }
                else if (Hydra56WeaponOn == true)
                {
                    //ChangeSkillBtn.gameObject.SetActive(false);
                    Hydra56WeaponOn = false;
                    UGG98WeaponOn = true;
                    Player.GetComponent<Hydra56Controller>().ChangeOff();
                    Player.GetComponent<UGG98Controller>().SwitchTypeStart = 2;
                    Player.GetComponent<UGG98Controller>().SwitchTypeEnd = 4;
                    Player.GetComponent<UGG98Controller>().ChangeOn();
                    Player.GetComponent<Hydra56Controller>().WeaponOn = false;
                    GetComponent<GameControlSystem>().Hydra56WeaponOn = false;
                }
                else if (MEAGWeaponOn == true)
                {
                    MEAGWeaponOn = false;
                    UGG98WeaponOn = true;
                    Player.GetComponent<MEAGController>().ChangeOff();
                    Player.GetComponent<UGG98Controller>().SwitchTypeStart = 3;
                    Player.GetComponent<UGG98Controller>().SwitchTypeEnd = 4;
                    Player.GetComponent<UGG98Controller>().ChangeOn();
                    Player.GetComponent<MEAGController>().WeaponOn = false;
                    GetComponent<GameControlSystem>().MEAGWeaponOn = false;
                }

                Invoke("OnceTime", 1);
            }
        }
    }

    void OnceTime()
    {
        Once = 0;
    }

    void IconRun()
    {
        CHW.GetComponent<Animator>().SetFloat("Active(icon direction), CHW", 1);
        CHW.GetComponent<Animator>().SetBool("Active(icon run), CHW", true);
    }

    public void StopWeapons()
    {
        CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetBool("Turn off, CHW", true);
        Invoke("ButtenOff", 0.1f);
    }

    void ButtenOff()
    {
        CHWWeapon.gameObject.SetActive(false);
    }

    IEnumerator UpStart()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 1);
        yield return new WaitForSeconds(0.5f);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 1);
    }

    IEnumerator DownStart()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 2);
        yield return new WaitForSeconds(0.5f);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 2);
    }

    IEnumerator RightStart()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 3);
        yield return new WaitForSeconds(0.5f);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 3);
    }

    IEnumerator LeftStart()
    {
        SoundManager.instance.SFXPlay2("Sound", Beep1);
        CHWWeapon.GetComponent<Animator>().SetFloat("Weapon, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 4);
        yield return new WaitForSeconds(0.5f);
        CHWWeapon.GetComponent<Animator>().SetFloat("Active, CHW", 0);
        CHWWeapon.GetComponent<Animator>().SetFloat("Running, CHW", 4);
    }
}