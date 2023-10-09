using UnityEngine;

public class BodyBloodCreate : MonoBehaviour
{
    public GameObject FloorBlood;
    public Transform FloorBloodPos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            //Debug.Log("Body Blood");
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            transform.Translate(new Vector3(0, -0.5f, 0));
            GameObject BloodFloor = Instantiate(FloorBlood, FloorBloodPos.transform.position, FloorBloodPos.transform.rotation);
            Destroy(BloodFloor, 10);
        }
    }
}
