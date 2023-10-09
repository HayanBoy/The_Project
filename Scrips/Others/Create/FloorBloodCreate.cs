using UnityEngine;

public class FloorBloodCreate : MonoBehaviour
{
    private int BloodPrint;

    void Start()
    {
        transform.Translate(new Vector3(0, -2, 0));

        BloodPrint = Random.Range(0, 16);

        if (BloodPrint == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (BloodPrint == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (BloodPrint == 2)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (BloodPrint == 3)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (BloodPrint == 4)
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (BloodPrint == 5)
        {
            transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (BloodPrint == 6)
        {
            transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (BloodPrint == 7)
        {
            transform.GetChild(7).gameObject.SetActive(true);
        }
        else if (BloodPrint == 8)
        {
            transform.GetChild(8).gameObject.SetActive(true);
        }
        else if (BloodPrint == 9)
        {
            transform.GetChild(9).gameObject.SetActive(true);
        }
        else if (BloodPrint == 10)
        {
            transform.GetChild(10).gameObject.SetActive(true);
        }
        else if (BloodPrint == 11)
        {
            transform.GetChild(11).gameObject.SetActive(true);
        }
        else if (BloodPrint == 12)
        {
            transform.GetChild(12).gameObject.SetActive(true);
        }
        else if (BloodPrint == 13)
        {
            transform.GetChild(13).gameObject.SetActive(true);
        }
        else if (BloodPrint == 14)
        {
            transform.GetChild(14).gameObject.SetActive(true);
        }
        else if (BloodPrint == 15)
        {
            transform.GetChild(15).gameObject.SetActive(true);
        }
    }
}
