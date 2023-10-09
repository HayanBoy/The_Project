using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public int EnemyNumber; //적 번호. 해당 번호를 가진 적의 조준을 가져온다.

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player") || collision is CircleCollider2D && collision.CompareTag("Player") || collision is BoxCollider2D && collision.gameObject.layer == 18)
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = true;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().Trigger = true;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = true;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().Trigger = true;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = true;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().Trigger = true;
            else if (EnemyNumber == 8)
            {
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().Trigger = true;
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TurnRolling = true;
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().MachinegunFire = true;
            }
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().Trigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.CompareTag("Player") || collision is CircleCollider2D && collision.CompareTag("Player") || collision is BoxCollider2D && collision.gameObject.layer == 18)
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = false;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().Trigger = false;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = false;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().Trigger = false;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().Trigger = false;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().Trigger = false;
            else if (EnemyNumber == 8)
            {
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().Trigger = false;
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().TurnRolling = false;
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().MachinegunFire = false;
            }
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().Trigger = false;
        }
    }
}