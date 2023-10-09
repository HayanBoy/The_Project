using System.Collections;
using UnityEngine;

public class VehicleCall : MonoBehaviour
{
    public int DropType;
    public GameObject Vehicle;
    public float Calltime;
    public float Destroytime;

    public GameObject ShipVehiclePrefab;
    public Transform ShipVehiclePos;

    public void StratCall()
    {
        if (DropType == 0) //전투로봇, MBCA-79 Iron Hurricane
        {
            Instantiate(ShipVehiclePrefab, ShipVehiclePos.position, ShipVehiclePos.rotation); //함선 탑승차량 지원 연출
            StartCoroutine(AnimationTime());
            Invoke("Calling", Calltime);
            Invoke("Destroy", Destroytime);
        }
    }

    IEnumerator AnimationTime()
    {
        GetComponent<Animator>().SetBool("Cycle Active, Vehicle drop", true);
        GetComponent<Animator>().SetFloat("Cycle, Vehicle drop", 1 / Destroytime);
        GetComponent<Animator>().SetFloat("Start, Vehicle drop", 1);
        yield return new WaitForSeconds(Destroytime - 0.75f);
        GetComponent<Animator>().SetFloat("Start, Vehicle drop", 2);
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetBool("Cycle Active, Vehicle drop", false);
        GetComponent<Animator>().SetFloat("Cycle, Vehicle drop", 0);
        GetComponent<Animator>().SetFloat("Start, Vehicle drop", 0);
    }

    void Calling()
    {
        Vehicle.GetComponent<VehicleLanding>().GetStartBooster();
    }

    void Destroy()
    {
        transform.position = new Vector2(-500, -500);
    }
}