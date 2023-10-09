using UnityEngine;

public class BombColliderEnter : MonoBehaviour
{
    public BombSettings BombSettings;
    public ActionZero ActionZero;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proximity Player")) //폭탄 설치 장소에 도달하면 폭탄을 설치하기 시작
        {
            if (collision.transform.parent.GetComponent<Movement>() != null) //탑승차량에 타고 있지 않을 때에만 발동
            {
                collision.transform.parent.GetComponent<Movement>().EnteringMissionPos = transform.position;
                collision.transform.parent.GetComponent<Movement>().MissionAction = true;
                collision.transform.parent.GetComponent<Movement>().MissionNumber = 1;
                ActionZero.StopUI(collision);
                this.gameObject.SetActive(false);
            }
        }
    }
}