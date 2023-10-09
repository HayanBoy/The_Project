using System.Collections;
using UnityEngine;

public class SpawnShipLoading : MonoBehaviour
{
    public bool InPlanet;
    public GameObject Ship1;
    public GameObject Ship2;
    public GameObject Ship3;
    public GameObject Ship4;
    public Transform ShipPos;

    float AmmoFireTime;
    int TypeRandom;

    void Start()
    {
        StartCoroutine(ShipSpawnTime());
        StartCoroutine(ShipCreate());
    }

    public IEnumerator ShipSpawnTime()
    {
        while (true)
        {
            AmmoFireTime = Random.Range(2, 4);
            TypeRandom = Random.Range(0, 14);
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator ShipCreate()
    {
        while (true)
        {
            float RandomLocation = Random.Range(-5, 5);
            float RandomSize = Random.Range(0.15f, 0.5f);
            float RandomSpeed = Random.Range(-5, -0.5f);

            if (TypeRandom >= 0 && TypeRandom < 15)
            {
                GameObject AddShip = Instantiate(Ship1, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
            }
            else if (TypeRandom >= 15 && TypeRandom < 25)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
            }
            else if (TypeRandom >= 25 && TypeRandom < 29)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
            }
            else if (TypeRandom >= 29 && TypeRandom <= 30)
            {
                GameObject AddShip = Instantiate(Ship2, new Vector3(ShipPos.transform.position.x, ShipPos.transform.position.y + RandomLocation, ShipPos.transform.position.z), ShipPos.transform.rotation);
                AddShip.transform.localScale = new Vector3(RandomSize, RandomSize, 1);
                AddShip.GetComponent<ShipBehavior>().Speed = RandomSpeed;
            }
            yield return new WaitForSeconds(AmmoFireTime);
        }
    }
}