using UnityEngine;

public class BloodCreate : MonoBehaviour
{
    private int BloodPrint;

    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        BloodPrint = Random.Range(0, 9);

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
    }
}