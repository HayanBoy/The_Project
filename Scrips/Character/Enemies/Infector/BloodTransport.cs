using UnityEngine;

public class BloodTransport : MonoBehaviour
{
    private int BloodPrint;
    private int BloodPrint2;
    bool Flip = false;

    void OnEnable()
    {
        Flip = false;
        
        for(int i = 0; i < 3; i++)
        {
            BloodPrint = Random.Range(1, 12);
            BloodOn();
        }

        BloodPrint2 = Random.Range(1, 4);
        BloodStartOn();
    }

    void BloodStartOn()
    {
        if (BloodPrint2 == 1)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(10).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(10).gameObject.SetActive(true);
                BloodFlip callstart1 = transform.GetChild(10).GetComponent<BloodFlip>();
                callstart1.FlipOnline = true;
            }
        }
        else if (BloodPrint2 == 2)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(11).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(11).gameObject.SetActive(true);
                BloodFlip callstart2 = transform.GetChild(11).GetComponent<BloodFlip>();
                callstart2.FlipOnline = true;
            }
        }
        else if (BloodPrint2 == 3)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(12).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(12).gameObject.SetActive(true);
                BloodFlip callstart3 = transform.GetChild(12).GetComponent<BloodFlip>();
                callstart3.FlipOnline = true;
            }
        }
    }

    void BloodOn()
    {
        if (BloodPrint == 1)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                BloodFlip call1 = transform.GetChild(0).GetComponent<BloodFlip>();
                call1.FlipOnline = true;
            }
        }
        else if (BloodPrint == 2)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(true);
                BloodFlip call2 = transform.GetChild(1).GetComponent<BloodFlip>();
                call2.FlipOnline = true;
            }
        }
        else if (BloodPrint == 3)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(2).gameObject.SetActive(true);
                BloodFlip call3 = transform.GetChild(2).GetComponent<BloodFlip>();
                call3.FlipOnline = true;
            }
        }
        else if (BloodPrint == 4)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(true);
                BloodFlip call4 = transform.GetChild(3).GetComponent<BloodFlip>();
                call4.FlipOnline = true;
            }
        }
        else if (BloodPrint == 5)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(4).gameObject.SetActive(true);
                BloodFlip call5 = transform.GetChild(4).GetComponent<BloodFlip>();
                call5.FlipOnline = true;
            }
        }
        else if (BloodPrint == 6)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(5).gameObject.SetActive(true);
                BloodFlip call6 = transform.GetChild(5).GetComponent<BloodFlip>();
                call6.FlipOnline = true;
            }
        }
        else if (BloodPrint == 7)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(6).gameObject.SetActive(true);
                BloodFlip call7 = transform.GetChild(6).GetComponent<BloodFlip>();
                call7.FlipOnline = true;
            }
        }
        else if (BloodPrint == 8)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(7).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(7).gameObject.SetActive(true);
                BloodFlip call8 = transform.GetChild(7).GetComponent<BloodFlip>();
                call8.FlipOnline = true;
            }
        }
        else if (BloodPrint == 9)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(8).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(8).gameObject.SetActive(true);
                BloodFlip call9 = transform.GetChild(8).GetComponent<BloodFlip>();
                call9.FlipOnline = true;
            }
        }
        else if (BloodPrint == 10)
        {
            if (transform.rotation.y == 0)
            {
                transform.GetChild(9).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(9).gameObject.SetActive(true);
                BloodFlip call10 = transform.GetChild(9).GetComponent<BloodFlip>();
                call10.FlipOnline = true;
            }
        }
    }
}