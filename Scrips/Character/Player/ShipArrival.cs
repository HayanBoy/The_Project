using System.Collections;
using UnityEngine;

public class ShipArrival : MonoBehaviour
{
    public GameObject StageShip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            StartCoroutine(StartShip());
        }
    }

    IEnumerator StartShip()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameControlSystem ShipStart = StageShip.gameObject.GetComponent<GameControlSystem>();
        StartCoroutine(ShipStart.StartShipArrival());
    }
}