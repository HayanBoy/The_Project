using System.Collections;
using UnityEngine;

public class ReturnToFleet : MonoBehaviour
{
    public BackToUniverse BackToUniverse;
    public ActionZero ActionZero;
    public ClearLine ClearLine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proximity Player")) //�÷��̾ ���۱� ���ܿ� �浹��, �÷��̾ �ش� ������ ���� ž���ϱ� ���ؼ� �̵��ϵ��� ����
        {
            if (collision.transform.parent.GetComponent<Movement>() != null) //ž�������� Ÿ�� ���� ���� ������ �ߵ�
            {
                collision.transform.parent.GetComponent<Movement>().EnteringShuttle = true;
                GameObject.Find("Play Control/Player Vehicles/Battle Robot").GetComponent<VehicleLanding>().EnteringShuttle = true;
                ActionZero.StopUI(collision);
            }
        }
    }

    //�÷��̾ ���۱⿡ ž���� �� ����ϸ鼭 �Դ������� ����
    public IEnumerator ChangeScene()
    {
        GameObject.Find("Game play process/Vehicle/HA-767 Shoebill").GetComponent<Animator>().SetBool("Player Entering, HA-767", false);
        ClearLine.Shuttle.GetComponent<Animator>().SetFloat("Mission Complete, HA-767", 3);
        yield return new WaitForSeconds(5);
        StartCoroutine(BackToUniverse.Exit(2));
    }
}