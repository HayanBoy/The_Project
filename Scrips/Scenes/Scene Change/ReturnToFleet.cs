using System.Collections;
using UnityEngine;

public class ReturnToFleet : MonoBehaviour
{
    public BackToUniverse BackToUniverse;
    public ActionZero ActionZero;
    public ClearLine ClearLine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proximity Player")) //플레이어가 수송기 비콘에 충돌시, 플레이어가 해당 비콘을 향해 탑승하기 위해서 이동하도록 조취
        {
            if (collision.transform.parent.GetComponent<Movement>() != null) //탑승차량에 타고 있지 않을 때에만 발동
            {
                collision.transform.parent.GetComponent<Movement>().EnteringShuttle = true;
                GameObject.Find("Play Control/Player Vehicles/Battle Robot").GetComponent<VehicleLanding>().EnteringShuttle = true;
                ActionZero.StopUI(collision);
            }
        }
    }

    //플레이어가 수송기에 탑승한 뒤 출발하면서 함대전으로 복귀
    public IEnumerator ChangeScene()
    {
        GameObject.Find("Game play process/Vehicle/HA-767 Shoebill").GetComponent<Animator>().SetBool("Player Entering, HA-767", false);
        ClearLine.Shuttle.GetComponent<Animator>().SetFloat("Mission Complete, HA-767", 3);
        yield return new WaitForSeconds(5);
        StartCoroutine(BackToUniverse.Exit(2));
    }
}