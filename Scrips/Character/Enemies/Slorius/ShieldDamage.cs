using UnityEngine;

public class ShieldDamage : MonoBehaviour
{
    private int ShieldPrint;
    private bool ShieldCrack1 = false;
    private bool ShieldCrack2 = false;
    private bool ShieldCrack3 = false;
    private bool ShieldCrack4 = false;
    private bool ShieldCrack5 = false;
    private bool ShieldCrack6 = false;
    private bool ShieldCrack7 = false;
    private bool ShieldCrack8 = false;
    private bool ShieldCrack9 = false;
    private bool ShieldCrack10 = false;
    private bool ShieldCrack11 = false;
    private bool ShieldCrack12 = false;
    private bool ShieldCrack13 = false;

    void OnEnable()
    {
        ShieldCrack1 = false;
        ShieldCrack2 = false;
        ShieldCrack3 = false;
        ShieldCrack4 = false;
        ShieldCrack5 = false;
        ShieldCrack6 = false;
        ShieldCrack7 = false;
        ShieldCrack8 = false;
        ShieldCrack9 = false;
        ShieldCrack10 = false;
        ShieldCrack11 = false;
        ShieldCrack12 = false;
        ShieldCrack13 = false;
    }

    public void ShieldOn(int Boolean)
    {
        if(Boolean >= 1)
        {
            //Debug.Log("Shield hit!");
            Boolean = 0;
            ShieldPrint = Random.Range(1, 14);

            if (ShieldPrint == 1 && ShieldCrack1 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 1 && ShieldCrack1 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 2 && ShieldCrack2 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 2 && ShieldCrack2 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 3 && ShieldCrack3 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 3 && ShieldCrack3 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 4 && ShieldCrack4 == true)
            {
                while (true)
                {
                    ShieldPrint = Random.Range(1, 14);
                    if (ShieldPrint == 4 && ShieldCrack4 == true)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 5 && ShieldCrack5 == true)
            {
                while (true)
                {

                    if (ShieldPrint == 5 && ShieldCrack5 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 6 && ShieldCrack6 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 6 && ShieldCrack6 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 7 && ShieldCrack7 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 7 && ShieldCrack7 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 8 && ShieldCrack8 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 8 && ShieldCrack8 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 9 && ShieldCrack9 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 9 && ShieldCrack9 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 10 && ShieldCrack10 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 10 && ShieldCrack10 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 11 && ShieldCrack11 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 11 && ShieldCrack11 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 12 && ShieldCrack12 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 12 && ShieldCrack12 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (ShieldPrint == 13 && ShieldCrack13 == true)
            {
                while (true)
                {
                    if (ShieldPrint == 13 && ShieldCrack13 == true)
                    {
                        ShieldPrint = Random.Range(1, 14);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        if (ShieldPrint == 1 && ShieldCrack1 == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            ShieldCrack1 = true;
        }
        else if (ShieldPrint == 2 && ShieldCrack2 == false)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            ShieldCrack2 = true;
        }
        else if (ShieldPrint == 3 && ShieldCrack3 == false)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            ShieldCrack3 = true;
        }
        else if (ShieldPrint == 4 && ShieldCrack4 == false)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            ShieldCrack4 = true;
        }
        else if (ShieldPrint == 5 && ShieldCrack5 == false)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            ShieldCrack5 = true;
        }
        else if (ShieldPrint == 6 && ShieldCrack6 == false)
        {
            transform.GetChild(5).gameObject.SetActive(true);
            ShieldCrack6 = true;
        }
        else if (ShieldPrint == 7 && ShieldCrack7 == false)
        {
            transform.GetChild(6).gameObject.SetActive(true);
            ShieldCrack7 = true;
        }
        else if (ShieldPrint == 8 && ShieldCrack8 == false)
        {
            transform.GetChild(7).gameObject.SetActive(true);
            ShieldCrack8 = true;
        }
        else if (ShieldPrint == 9 && ShieldCrack9 == false)
        {
            transform.GetChild(8).gameObject.SetActive(true);
            ShieldCrack9 = true;
        }
        else if (ShieldPrint == 10 && ShieldCrack10 == false)
        {
            transform.GetChild(9).gameObject.SetActive(true);
            ShieldCrack10 = true;
        }
        else if (ShieldPrint == 11 && ShieldCrack11 == false)
        {
            transform.GetChild(10).gameObject.SetActive(true);
            ShieldCrack11 = true;
        }
        else if (ShieldPrint == 12 && ShieldCrack12 == false)
        {
            transform.GetChild(11).gameObject.SetActive(true);
            ShieldCrack12 = true;
        }
        else if (ShieldPrint == 13 && ShieldCrack13 == false)
        {
            transform.GetChild(12).gameObject.SetActive(true);
            ShieldCrack13 = true;
        }
    }

    public void ShieldOff(bool Boolean)
    {
        if (Boolean == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(7).gameObject.SetActive(false);
            transform.GetChild(8).gameObject.SetActive(false);
            transform.GetChild(9).gameObject.SetActive(false);
            transform.GetChild(10).gameObject.SetActive(false);
            transform.GetChild(11).gameObject.SetActive(false);
            transform.GetChild(12).gameObject.SetActive(false);
        }

        Boolean = false;
    }
}
