using System.Collections;
using UnityEngine;

public class KantakriBlackExplotion : MonoBehaviour
{
    int PrintExplosion;

    void OnEnable()
    {
        PrintExplosion = Random.Range(0, 3);

        if(PrintExplosion == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("TurnOff1", 2);
        }
        else if (PrintExplosion == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Invoke("TurnOff2", 2);
        }
        else if (PrintExplosion == 2)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            Invoke("TurnOff3", 2);
        }
    }

    void TurnOff1()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void TurnOff2()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    void TurnOff3()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }
}