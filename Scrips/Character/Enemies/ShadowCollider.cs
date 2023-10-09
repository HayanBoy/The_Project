using UnityEngine;

public class ShadowCollider : MonoBehaviour
{
    public GameObject Body1;
    public GameObject Body2;
    public GameObject Body3;
    public GameObject Body4;
    public GameObject Body5;
    public GameObject Body6;

    private bool OneTimeEnter = false; //스폰 직후, 플레이어가 있는 영역으로 진입하고 나서 더 이상 진입벽과 충돌되지 않도록 조취
    public int EnemyNumber; //적 번호. 해당 번호를 가진 적의 정보를 가져온다.

    private void OnEnable()
    {
        OneTimeEnter = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magnet"))
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = collision.transform;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().magnetForm = collision.transform;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = collision.transform;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().magnetForm = collision.transform;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = collision.transform;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().magnetForm = collision.transform;
            else if (EnemyNumber == 6)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().magnetForm = collision.transform;
            else if (EnemyNumber == 7)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().magnetForm = collision.transform;
            else if (EnemyNumber == 8)
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().magnetForm = collision.transform;
            else if (EnemyNumber == 100)
                gameObject.transform.parent.GetComponent<InfectorBehavior>().magnetForm = collision.transform;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().magnetForm = collision.transform;
        }

        if (OneTimeEnter == false)
        {
            if (collision.CompareTag("Into Line Right")) //스폰시작 시, 플레이어가 있는 쪽으로 무적상태로 이동
            {
                if (EnemyNumber == 0)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 1)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 2)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 3)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 4)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 5)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 6)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                }
                else if (EnemyNumber == 7)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                }
                else if (EnemyNumber == 8)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 100)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().FollowVector = -1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 200)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    Body2.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().FollowVector = -1;
                    Body1.layer = 0;
                }
            }

            if (collision.CompareTag("Into Line Left")) //스폰시작 시, 플레이어가 있는 쪽으로 무적상태로 이동
            {
                if (EnemyNumber == 0)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 1)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 2)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 3)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 4)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 5)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 6)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                }
                else if (EnemyNumber == 7)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                }
                else if (EnemyNumber == 8)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 100)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().FollowVector = 1;
                    Body1.layer = 0;
                    Body2.layer = 0;
                    Body3.layer = 0;
                }
                else if (EnemyNumber == 200)
                {
                    gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    Body2.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLine = true;
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().FollowVector = 1;
                    Body1.layer = 0;
                }
            }

            if (collision.CompareTag("Into Speed Right"))
            {
                if (EnemyNumber == 0)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 1)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLineSpeed = true;
                else if (EnemyNumber == 2)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 3)
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = true;
                else if (EnemyNumber == 4)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 5)
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = true;
                else if (EnemyNumber == 6)
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLineSpeed = true;
                else if (EnemyNumber == 7)
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLineSpeed = true;
                else if (EnemyNumber == 8)
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLineSpeed = true;
                else if (EnemyNumber == 100)
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLineSpeed = true;
                else if (EnemyNumber == 200)
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = true;
                else if (EnemyNumber == 200)
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = true;
            }

            if (collision.CompareTag("Into Speed Left"))
            {
                if (EnemyNumber == 0)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 1)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLineSpeed = true;
                else if (EnemyNumber == 2)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 3)
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = true;
                else if (EnemyNumber == 4)
                    gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = true;
                else if (EnemyNumber == 5)
                    gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = true;
                else if (EnemyNumber == 6)
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLineSpeed = true;
                else if (EnemyNumber == 7)
                    gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLineSpeed = true;
                else if (EnemyNumber == 8)
                    gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLineSpeed = true;
                else if (EnemyNumber == 100)
                    gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLineSpeed = true;
                else if (EnemyNumber == 200)
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = true;
                else if (EnemyNumber == 200)
                    gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Magnet"))
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = null;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().magnetForm = null;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = null;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().magnetForm = null;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().magnetForm = null;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().magnetForm = null;
            else if (EnemyNumber == 6)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().magnetForm = null;
            else if (EnemyNumber == 7)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().magnetForm = null;
            else if (EnemyNumber == 8)
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().magnetForm = null;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().magnetForm = null;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().magnetForm = null;
        }

        if (collision.CompareTag("Into Line Right") || collision.CompareTag("Into Line Left")) //스폰시작 후에 플레이어가 있는 영역으로 들어오면 무적상태가 해제되며 이동이 해제
        {
            OneTimeEnter = true;

            if (EnemyNumber == 0)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 1)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 2)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 3)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 4)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 5)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 6)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
            }
            else if (EnemyNumber == 7)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
            }
            else if (EnemyNumber == 8)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLine = false;
                Body1.layer = 13;
                Body2.layer = 13;
                Body3.layer = 13;
            }
            else if (EnemyNumber == 100)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLine = false;
                Body1.layer = 16;
                Body2.layer = 16;
                Body3.layer = 16;
            }
            else if (EnemyNumber == 200)
            {
                gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                Body2.GetComponent<BoxCollider2D>().enabled = true;
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLine = false;
                Body1.layer = 12;
            }
        }

        if (collision.CompareTag("Into Speed Right"))
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLineSpeed = false;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = false;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = false;
            else if (EnemyNumber == 6)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLineSpeed = false;
            else if (EnemyNumber == 7)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLineSpeed = false;
            else if (EnemyNumber == 8)
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLineSpeed = false;
            else if (EnemyNumber == 100)
                gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLineSpeed = false;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = false;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = false;
        }

        if (collision.CompareTag("Into Speed Left"))
        {
            if (EnemyNumber == 0)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 1)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4>().IntoLineSpeed = false;
            else if (EnemyNumber == 2)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 3)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = false;
            else if (EnemyNumber == 4)
                gameObject.transform.parent.GetComponent<BehaviourKaotiJaios4_>().IntoLineSpeed = false;
            else if (EnemyNumber == 5)
                gameObject.transform.parent.GetComponent<DualBehaviourKaotiJaios5_>().IntoLineSpeed = false;
            else if (EnemyNumber == 6)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_>().IntoLineSpeed = false;
            else if (EnemyNumber == 7)
                gameObject.transform.parent.GetComponent<BehaviorTaikaLaiThrotro1_3>().IntoLineSpeed = false;
            else if (EnemyNumber == 8)
                gameObject.transform.parent.GetComponent<BehaviourAtroCrossfa>().IntoLineSpeed = false;
            else if (EnemyNumber == 100)
                gameObject.transform.parent.GetComponent<InfectorBehavior>().IntoLineSpeed = false;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = false;
            else if (EnemyNumber == 200)
                gameObject.transform.parent.GetComponent<BehaviorAsoShiioshare>().IntoLineSpeed = false;
        }
    }
}