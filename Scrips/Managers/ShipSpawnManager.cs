using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnManager : MonoBehaviour
{
    public GameObject Ship1Prefab;
    public GameObject Ship2Prefab;
    public GameObject Ship3Prefab;
    public GameObject Ship4Prefab;

    public GameObject E_Ship1Prefab;
    public GameObject E_Ship2Prefab;
    public GameObject E_Ship3Prefab;
    public GameObject E_Ship4Prefab;

    GameObject[] Ship1;
    GameObject[] Ship2;
    GameObject[] Ship3;
    GameObject[] Ship4;

    GameObject[] E_Ship1;
    GameObject[] E_Ship2;
    GameObject[] E_Ship3;
    GameObject[] E_Ship4;

    GameObject[] PoolMaker;

    private void Start()
    {
        Ship1 = new GameObject[10];
        Ship2 = new GameObject[10];
        Ship3 = new GameObject[10];
        Ship4 = new GameObject[10];

        E_Ship1 = new GameObject[10];
        E_Ship2 = new GameObject[10];
        E_Ship3 = new GameObject[10];
        E_Ship4 = new GameObject[10];

        Generate();

    }
    void Generate()
    {
        for (int index = 0; index < Ship1.Length; index++)
        {
            Ship1[index] = Instantiate(Ship1Prefab);
            Ship1[index].SetActive(false);
        }

        for (int index = 0; index < Ship2.Length; index++)
        {
            Ship2[index] = Instantiate(Ship2Prefab);
            Ship2[index].SetActive(false);
        }

        for (int index = 0; index < Ship3.Length; index++)
        {
            Ship3[index] = Instantiate(Ship3Prefab);
            Ship3[index].SetActive(false);
        }

        for (int index = 0; index < Ship4.Length; index++)
        {
            Ship4[index] = Instantiate(Ship4Prefab);
            Ship4[index].SetActive(false);
        }

        for (int index = 0; index < E_Ship1.Length; index++)
        {
            E_Ship1[index] = Instantiate(E_Ship1Prefab);
            E_Ship1[index].SetActive(false);
        }

        for (int index = 0; index < E_Ship2.Length; index++)
        {
            E_Ship2[index] = Instantiate(E_Ship2Prefab);
            E_Ship2[index].SetActive(false);
        }

        for (int index = 0; index < E_Ship3.Length; index++)
        {
            E_Ship3[index] = Instantiate(E_Ship3Prefab);
            E_Ship3[index].SetActive(false);
        }

        for (int index = 0; index < E_Ship4.Length; index++)
        {
            E_Ship4[index] = Instantiate(E_Ship4Prefab);
            E_Ship4[index].SetActive(false);
        }

    }

    public GameObject Loader(string type)
    {
        switch (type)
        {
            case "Ship1":
                PoolMaker = Ship1;
                break;
            case "Ship2":
                PoolMaker = Ship2;
                break;

            case "Ship3":
                PoolMaker = Ship3;
                break;

            case "Ship4":
                PoolMaker = Ship4;
                break;

            case "E_Ship1":
                PoolMaker = E_Ship1;
                break;

            case "E_Ship2":
                PoolMaker = E_Ship2;
                break;

            case "E_Ship3":
                PoolMaker = E_Ship3;
                break;

            case "E_Ship4":
                PoolMaker = E_Ship4;
                break;
        }

        for (int index = 0; index < PoolMaker.Length; index++)
        {
            if (!PoolMaker[index].activeSelf)
            {
                PoolMaker[index].SetActive(true);
                return PoolMaker[index];
            }
        }

        return null;
    }

}
