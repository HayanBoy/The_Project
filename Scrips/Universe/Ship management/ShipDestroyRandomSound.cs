using UnityEngine;

public class ShipDestroyRandomSound : MonoBehaviour
{
    public int AmountOfSounds;
    public GameObject Sound1;
    public GameObject Sound2;
    public GameObject Sound3;
    public GameObject Sound4;
    public GameObject Sound5;
    public GameObject Sound6;
    public GameObject Sound7;
    public GameObject Sound8;
    public GameObject Sound9;
    public GameObject Sound10;

    void Start()
    {
        int RandomSound = Random.Range(0, AmountOfSounds + 1);

        if (RandomSound == 0 && Sound1 != null)
            Sound1.SetActive(true);
        else if (RandomSound == 1 && Sound2 != null)
            Sound2.SetActive(true);
        else if (RandomSound == 2 && Sound3 != null)
            Sound3.SetActive(true);
        else if (RandomSound == 3 && Sound4 != null)
            Sound4.SetActive(true);
        else if (RandomSound == 4 && Sound5 != null)
            Sound5.SetActive(true);
        else if (RandomSound == 5 && Sound6 != null)
            Sound6.SetActive(true);
        else if (RandomSound == 6 && Sound7 != null)
            Sound7.SetActive(true);
        else if (RandomSound == 7 && Sound8 != null)
            Sound8.SetActive(true);
        else if (RandomSound == 8 && Sound9 != null)
            Sound9.SetActive(true);
        else if (RandomSound == 9 && Sound10 != null)
            Sound10.SetActive(true);
    }
}