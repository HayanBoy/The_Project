using UnityEngine;

public class BombColliderEnter : MonoBehaviour
{
    public BombSettings BombSettings;
    public ActionZero ActionZero;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proximity Player")) //��ź ��ġ ��ҿ� �����ϸ� ��ź�� ��ġ�ϱ� ����
        {
            if (collision.transform.parent.GetComponent<Movement>() != null) //ž�������� Ÿ�� ���� ���� ������ �ߵ�
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