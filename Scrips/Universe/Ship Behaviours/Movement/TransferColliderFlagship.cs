using UnityEngine;

public class TransferColliderFlagship : MonoBehaviour
{
    private bool Seleted = false;
    public bool isDowned; //기함 침투전으로 무력화되었는가

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Seleted == false && isDowned == false)
        {
            if (collision.gameObject.CompareTag("Boarding Operation"))
            {
                Seleted = true;
                collision.transform.parent.gameObject.GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Add(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Seleted == true && isDowned == false)
        {
            if (collision.gameObject.CompareTag("Boarding Operation"))
            {
                Seleted = false;
                collision.transform.parent.gameObject.GetComponent<HurricaneOperationForFlagship>().EnemyFlagship.Remove(gameObject);
            }
        }
    }
}