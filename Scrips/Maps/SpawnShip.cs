using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    public SpawnShip OtherSpawnShip;

    public bool InPlanet;
    public bool isMainMenu;
    public GameObject Ship1;
    public GameObject Ship2;
    public GameObject Ship3;
    public GameObject Ship4;
    public Transform ShipPos;

    float AmmoFireTime;
    int TypeRandom;

    public List<GameObject> ShipList = new List<GameObject>();

    void Start()
    {
        StartCoroutine(ShipSpawnTime());
        StartCoroutine(ShipCreate());
    }

    public IEnumerator ShipSpawnTime()
    {
        while (true)
        {
            if (isMainMenu == false)
            {
                AmmoFireTime = Random.Range(2f, 10f);
                TypeRandom = Random.Range(0, 14);
            }
            else
            {
                AmmoFireTime = Random.Range(2f, 5f);
                TypeRandom = Random.Range(0, 14);
            }
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator ShipCreate()
    {
        for (int i = 0; i < 2; i++)
        {
            float RandomLocation = Random.Range(0, 7);
            float RandomSize = 0;
            float RandomSpeed = 0;

            if (isMainMenu == false)
            {
                RandomSize = Random.Range(0.15f, 0.5f);
                RandomSpeed = Random.Range(0.25f, 2.5f);
            }
            else
            {
                RandomSize = Random.Range(0.15f, 0.35f);
                RandomSpeed = Random.Range(0.25f, 2f);
            }

            if (TypeRandom >= 0 && TypeRandom < 15)
            {
                GameObject AddShip = Instantiate(Ship1, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                ShipList.Add(AddShip);
                if (InPlanet == true)
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
            }
            else if (TypeRandom >= 15 && TypeRandom < 25)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                ShipList.Add(AddShip);
                if (InPlanet == true)
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
            }
            else if (TypeRandom >= 25 && TypeRandom < 29)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                ShipList.Add(AddShip);
                if (InPlanet == true)
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
            }
            else if (TypeRandom >= 29 && TypeRandom <= 30)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                ShipList.Add(AddShip);
                if (InPlanet == true)
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
            }
        }

        while (true)
        {
            if (ShipList.Count < 10 && ShipList.Count < OtherSpawnShip.ShipList.Count + 2)
            {
                float RandomLocation = Random.Range(0, 7);
                float RandomSize = 0;
                float RandomSpeed = 0;

                if (isMainMenu == false)
                {
                    RandomSize = Random.Range(0.15f, 0.5f);
                    RandomSpeed = Random.Range(0.25f, 2.5f);
                }
                else
                {
                    RandomSize = Random.Range(0.15f, 0.35f);
                    RandomSpeed = Random.Range(0.25f, 2f);
                }

                yield return new WaitForSeconds(AmmoFireTime);

                if (TypeRandom >= 0 && TypeRandom < 15)
                {
                    GameObject AddShip = Instantiate(Ship1, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                    AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                    AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                    ShipList.Add(AddShip);
                    if (InPlanet == true)
                        AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
                }
                else if (TypeRandom >= 15 && TypeRandom < 25)
                {
                    GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                    AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                    AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                    ShipList.Add(AddShip);
                    if (InPlanet == true)
                        AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
                }
                else if (TypeRandom >= 25 && TypeRandom < 29)
                {
                    GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                    AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                    AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                    ShipList.Add(AddShip);
                    if (InPlanet == true)
                        AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
                }
                else if (TypeRandom >= 29 && TypeRandom <= 30)
                {
                    GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                    AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                    AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
                    AddShip.GetComponent<HullSloriusFormationShipInBacground>().SpawnShip = this;
                    ShipList.Add(AddShip);
                    if (InPlanet == true)
                        AddShip.GetComponent<HullSloriusFormationShipInBacground>().inPlanet = true;
                }
            }
            yield return new WaitForSeconds(2);

            for (int i = 0; i < ShipList.Count; i++)
            {
                if (ShipList[i] == null || ShipList[i].activeSelf == false)
                {
                    ShipList.Remove(ShipList[i]);
                }
            }
        }
    }
}
