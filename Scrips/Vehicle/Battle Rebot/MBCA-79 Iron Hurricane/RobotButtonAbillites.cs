using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotButtonAbillites : MonoBehaviour
{
    public Image DashCoolIMG;
    public Image BarrierCoolIMG;
    public Image VulcanCoolIMG;
    public Image PlazmaCoolIMG;
    public Image HoimingCoolIMG;

    public float Dashcooldown;
    public float Barriercooldown;
    public float Vulcancooldown;
    public float Plazmacooldown;
    public float Homingcooldown;

    bool isCooldown1 = false;
    bool isCooldown2 = false;
    bool isCooldown3 = false;
    bool isCooldown4 = false;
   // bool isCooldown5 = false;

    public bool isDash;
    public bool isBarrier;
    public bool isVulcan;
    public bool isPlazma;
    public bool isHoming;

    RobotMove robotmove;

    public void DashUp()
    {
        isDash = false;
    }

    public void BarrierUp()
    {
        isBarrier = false;
    }

    public void BarrierDown()
    {
        isBarrier = true;
    }

    public void VulcanUp()
    {
        isVulcan = false;
    }

    public void VulcanDown()
    {
        isVulcan = true;
    }

    public void PlazmaUP()
    {
        isPlazma = false;
    }

    public void PlazmaDown()
    {
        isPlazma = true;
    }

    public void HomingUp()
    {
        isHoming = false;
    }

    public void HomingDown()
    {
        isHoming = true;
    }

    void Start()
    {
        DashCoolIMG.fillAmount = 1;
        BarrierCoolIMG.fillAmount = 0;
        VulcanCoolIMG.fillAmount = 0;
        PlazmaCoolIMG.fillAmount = 0;
        HoimingCoolIMG.fillAmount = 0;


        robotmove = FindObjectOfType<RobotMove>();
    }

    void Update()
    {
        DashCool();
        BarrierCool();
        VulcanCool();
        PlazmaCool();
        HomingCool();    
    }


    public void DashCool()
    {
        if (robotmove.DashCount >= 0)
        {
            DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

            if (DashCoolIMG.fillAmount <= 0)
            {
                //movement.DashCount++;
                DashCoolIMG.fillAmount = 1;     
            }
        }

        if (robotmove.DashCount == 2)
        {
            //movement.DashTime = 0;
            DashCoolIMG.fillAmount = 0;

            if (robotmove.DashCount == 1)
            {
                DashCoolIMG.fillAmount -= 1 / Dashcooldown * Time.deltaTime;

                if (DashCoolIMG.fillAmount <= 0)
                {
                    //movement.DashCount++;
                    DashCoolIMG.fillAmount = 1;
                }
            }

            if (robotmove.DashCount == 0)
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

    public void BarrierCool()
    {
        if (isBarrier && isCooldown1 == false)
        {

            isCooldown1 = true;
            BarrierCoolIMG.fillAmount = 1;
        }

        if (isCooldown1)
        {
            BarrierCoolIMG.fillAmount -= 1 / Barriercooldown * Time.deltaTime;

            if (BarrierCoolIMG.fillAmount <= 0)
            {
                BarrierCoolIMG.fillAmount = 0;
                isCooldown1 = false;

                BarrierUp();
            }
        }
    }

    public void VulcanCool()
    {
        if (isVulcan && isCooldown2 == false)
        {

            isCooldown2 = true;
            VulcanCoolIMG.fillAmount = 1;
        }

        if (isCooldown2)
        {
            VulcanCoolIMG.fillAmount -= 1 / Vulcancooldown * Time.deltaTime;

            if (VulcanCoolIMG.fillAmount <= 0)
            {
                VulcanCoolIMG.fillAmount = 0;
                isCooldown2 = false;

                VulcanUp();
            }
        }
    }

    public void PlazmaCool()
    {
        if (isPlazma && isCooldown3 == false)
        {
            isCooldown3 = true;
            PlazmaCoolIMG.fillAmount = 1;
        }

        if (isCooldown3)
        {
            PlazmaCoolIMG.fillAmount -= 1 / Plazmacooldown * Time.deltaTime;

            if (PlazmaCoolIMG.fillAmount <= 0)
            {
                PlazmaCoolIMG.fillAmount = 0;
                isCooldown3 = false;

                PlazmaUP();
            }
        }
    }


    public void HomingCool()
    {
        if (isHoming && isCooldown4 == false)
        {
            isCooldown4 = true;
            HoimingCoolIMG.fillAmount = 1;
        }

        if (isCooldown4)
        {
            HoimingCoolIMG.fillAmount -= 1 / Homingcooldown * Time.deltaTime;

            if (HoimingCoolIMG.fillAmount <= 0)
            {
                HoimingCoolIMG.fillAmount = 0;
                isCooldown4 = false;

                HomingUp();
            }
        }
    }

}
