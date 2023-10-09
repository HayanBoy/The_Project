using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    GunController guncontrol;
    Hydra56Controller hydra56Controller;
    ArthesL775Controller arthesL775Controller;
    MEAGController meagController;
    UGG98Controller ugg98Controller;
    VM5GrenadeController vm5GrenadeController;

    public Image DashCoolIMG;
    public Image HPCoolIMG;
    public Image GrenadeCoolIMG;
    public Image AirBombCoolIMG;
    public Image HoimingCoolIMG;
    public Image ItemCoolIMG;
    public Image SupArmyCoolIMG;
    public Image WeaponDropCoolIMG;
    public Image RobotCallCoolIMG;

    public float Dashcooldown;
    public float HPcooldown;
    public float AirBombcooldown;
    public float Homingcooldown;
    public float Itemcooldown;
    public float SupArmycooldown;
    public float WeaponDropcooldown;
    public float RobotCallCoolDown;
    // public float Raisercooldown;

    bool isCooldown1 = false;
    bool isCooldown2 = false;
    bool isCooldown3 = false;

    bool isCooldown4 = false;
    bool isCooldown5 = false;
    bool isCooldown6 = false;
    bool isCooldown7 = false;
    bool isCooldown8 = false;

    public bool isCooldown9 = false;
    bool isCooldownSupArmy = false;
    bool isCooldownWeaponDrop = false;
    bool isCooldownRobotCall = false;

    public bool isDash;
    public bool isRaiser;
    public bool isRail;
    public bool isExplosion;

    public bool isHP;
    public bool isGrenade;
    public bool isAirBomb;
    public bool isHoming;
    public bool isitem;

    public bool isMagnet;

    public bool isSupArmy;
    public bool isWeaponDrop;
    public bool isRobotCall;

    public bool HPHPHP;  // HP 버튼 사용 불가능할때에도 돌아가는현상 해결하기 위해 선언한 BOOL 값 
    public bool magmag;
    public bool magmagmag;

    public bool mag1 = false;
    public bool mag2 = false;

    Movement movement;
    GunController gun;
    Player player;

    public void SupArmyUp()
    {
        isSupArmy = false;
    }

    public void SupArmyDown()
    {
        isSupArmy = true;
    }

    public void DashUp()
    {
        isDash = false;
    }

    public void RaiserhUp()
    {
        isRaiser = false;
    }

    public void RaiserDown()
    {
        if (arthesL775Controller.UsingChangeWeapon == false && arthesL775Controller.Reload == false)
            isRaiser = true;
    }

    public void RailgunhUp()
    {
        isRail = false;
    }

    public void RailgunhDown()
    {
        if (meagController.UsingChangeWeapon == false && meagController.Reload == false)
            isRail = true;
    }

    public void ExplosionUp()
    {
        isExplosion = false;
    }

    public void ExplosionDown()
    {
        if (hydra56Controller.UsingChangeWeapon == false && hydra56Controller.Reload == false)
            isExplosion = true;
    }

    public void HPUp()
    {
        isHP = false;
    }

    public void HPDown()
    {
        isHP = true;
    }

    public void GrenadeUp()
    {
        isGrenade = false;
    }

    public void GrenadeDown()
    {
        if (vm5GrenadeController.UsingChangeWeapon == false && vm5GrenadeController.Reload == false)
            isGrenade = true;
    }


    public void HomingUp()
    {
        isHoming = false;
    }

    public void HomingDown()
    {
        isHoming = true;
    }

    public void AirBombUp()
    {
        isAirBomb = false;
    }

    public void AirBombUpDown()
    {
        if(movement.HeavyWeaponOnline == 0)
            isAirBomb = true;
    }
    public void ItemUp()
    {
        isitem = false;
    }

    public void ItemDown()
    {
        isitem = true;
    }

    public void MagnetUp()
    {
        isMagnet = false;
    }

    public void MagnetDown()
    {
        if (ugg98Controller.UsingChangeWeapon == false && ugg98Controller.Reload == false)
            isMagnet = true;
    }

    public void WeaponDropUp()
    {
        isWeaponDrop = false;
    }

    public void WeaponDropDown()
    {
        isWeaponDrop = true;
    }

    public void RobotCallUp()
    {
        isRobotCall = false;
    }

    public void RobotCallDown()
    {
        isRobotCall = true;
    }

    void Start()
    {
        guncontrol = FindObjectOfType<GunController>();

        DashCoolIMG.fillAmount = 1;

        HPCoolIMG.fillAmount = 0;
        GrenadeCoolIMG.fillAmount = 0;
        AirBombCoolIMG.fillAmount = 0;
        HoimingCoolIMG.fillAmount = 0;
        ItemCoolIMG.fillAmount = 0;
        SupArmyCoolIMG.fillAmount = 0;
        WeaponDropCoolIMG.fillAmount = 0;
        RobotCallCoolIMG.fillAmount = 0;

        movement = FindObjectOfType<Movement>();
        gun = FindObjectOfType<GunController>();
        player = FindObjectOfType<Player>();
        ugg98Controller = FindObjectOfType<UGG98Controller>();
        arthesL775Controller = FindObjectOfType<ArthesL775Controller>();
        meagController = FindObjectOfType<MEAGController>();
        hydra56Controller = FindObjectOfType<Hydra56Controller>();
        vm5GrenadeController = FindObjectOfType<VM5GrenadeController>();
    }

    void Update()
    {
        DashCool();
       
        HomingCool();
        AirBombCool();
        itemCool();
        HPCool();

        Magnet1();
        Magnet2();
        MagnetCross();

        SupArmyCool();
        WeaponDropCool();
        RobotCallCool();
    }

    public void DashCool()
    {
        if (movement.DashCount >= 0)
        {
            DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

            if (DashCoolIMG.fillAmount <= 0)
            {
                //movement.DashCount++;
                DashCoolIMG.fillAmount = 1;     
            }
        }

        if (movement.DashCount == 3)
        {
            //movement.DashTime = 0;
            DashCoolIMG.fillAmount = 0;

            if (movement.DashCount == 2)
            {
                DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

                if (DashCoolIMG.fillAmount <= 0)
                {
                    //movement.DashCount++;
                    DashCoolIMG.fillAmount = 1;
                }
            }

            if (movement.DashCount == 1)
            {
                DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

                if (DashCoolIMG.fillAmount <= 0)
                {
                    //movement.DashCount++;
                    DashCoolIMG.fillAmount = 1;
                }
            }

            if (movement.DashCount == 0)
            {
                DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

                if (DashCoolIMG.fillAmount <= 0)
                {
                    //movement.DashCount++;
                    DashCoolIMG.fillAmount = 1;
                }
            }
        }
    }

    void HPHPHPHP()
    {
        if( player.Medicine > 0 && player.Medicine < 6 && player.hitPoints < 500 && player.MedicineCool >= player.MedicineTimeAmount)
        {
            HPHPHP = true;
            isHP = false;
        }
    }

    public void HPCool()
    {
        HPHPHPHP();

        if (isHP && isCooldown4 == false && player.Medicine >= 0 && player.Medicine < 6 && HPHPHP)
        {

            isCooldown4 = true;
            HPCoolIMG.fillAmount = 1;    
        }

        if (isCooldown4)
        {
            HPCoolIMG.fillAmount -= 1 / HPcooldown * Time.deltaTime;

            if (HPCoolIMG.fillAmount <= 0)
            {
                HPCoolIMG.fillAmount = 0;
                isCooldown4 = false;
                HPHPHP = false;

                HPUp();
            }
        }
    }

    public void HomingCool()
    {
        if (isHoming && isCooldown6 == false)
        {
            isCooldown6 = true;
            HoimingCoolIMG.fillAmount = 1;
        }

        if (isCooldown6)
        {
            HoimingCoolIMG.fillAmount -= 1 / Homingcooldown * Time.deltaTime;

            if (HoimingCoolIMG.fillAmount <= 0)
            {
                HoimingCoolIMG.fillAmount = 0;
                isCooldown6 = false;

                HomingUp();
            }
        }
    }

    public void AirBombCool()
    {
        if (movement.HeavyWeaponOnline == 0)
        {
            if (isAirBomb && isCooldown7 == false)
            {
                isCooldown7 = true;
                AirBombCoolIMG.fillAmount = 1;
            }

            if (isCooldown7)
            {
                AirBombCoolIMG.fillAmount -= 1 / AirBombcooldown * Time.deltaTime;

                if (AirBombCoolIMG.fillAmount <= 0)
                {
                    AirBombCoolIMG.fillAmount = 0;
                    isCooldown7 = false;

                    AirBombUp();
                }
            }
        }
    }

    public void itemCool()
    {
        if (isitem && isCooldown8 == false)
        {
            isCooldown8 = true;
            ItemCoolIMG.fillAmount = 1;
        }

        if (isCooldown8)
        {
            ItemCoolIMG.fillAmount -= 1 / Itemcooldown * Time.deltaTime;

            if (ItemCoolIMG.fillAmount <= 0)
            {
                ItemCoolIMG.fillAmount = 0;
                isCooldown8 = false;

                ItemUp();
            }
        }
    }

    void Magnet1()
    {
        if (ugg98Controller.MagnetCharging >= 0.01f && ugg98Controller.MagnetCharging <= 1.0f)
        {
            magmag = true;
        }

        else
        {
            magmag = false;
        }
    }

    void Magnet2()
    {
        if(ugg98Controller.MagnetCharging >= 1.01f)
        {
            magmagmag = true;
        }
        else
        {
            magmagmag = false;
        }
    }

    void MagnetCross()
    {
        if (magmag)
            mag1 = true;
       
        if (magmagmag)
        {
            mag2 = true;
            mag1 = false;
        }           
    }

    public void SupArmyCool()
    {
        if (isSupArmy && isCooldownSupArmy == false)
        {
            isCooldownSupArmy = true;
            SupArmyCoolIMG.fillAmount = 1;
        }

        if (isCooldownSupArmy)
        {
            SupArmyCoolIMG.fillAmount -= 1 / SupArmycooldown * Time.deltaTime;

            if (SupArmyCoolIMG.fillAmount <= 0)
            {
                SupArmyCoolIMG.fillAmount = 0;
                isCooldownSupArmy = false;

                SupArmyUp();
            }
        }
    }

    public void WeaponDropCool()
    {
        if (isWeaponDrop && isCooldownWeaponDrop == false)
        {
            isCooldownWeaponDrop = true;
            WeaponDropCoolIMG.fillAmount = 1;
        }

        if (isCooldownWeaponDrop)
        {
            WeaponDropCoolIMG.fillAmount -= 1 / WeaponDropcooldown * Time.deltaTime;

            if (WeaponDropCoolIMG.fillAmount <= 0)
            {
                WeaponDropCoolIMG.fillAmount = 0;
                isCooldownWeaponDrop = false;

                WeaponDropUp();
            }
        }
    }

    public void RobotCallCool()
    {
        if (isRobotCall && isCooldownRobotCall == false)
        {
            isCooldownRobotCall = true;
            RobotCallCoolIMG.fillAmount = 1;
        }

        if (isCooldownRobotCall)
        {
            RobotCallCoolIMG.fillAmount -= 1 / RobotCallCoolDown * Time.deltaTime;

            if (RobotCallCoolIMG.fillAmount <= 0)
            {
                RobotCallCoolIMG.fillAmount = 0;
                isCooldownRobotCall = false;

                RobotCallUp();
            }
        }
    }
}