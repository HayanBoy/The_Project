using System.Collections.Generic;
using UnityEngine;

public class RegdollControllerAsoShiioshare : MonoBehaviour
{
    [SerializeField] GameObject head, rightArmUp, rightArmDown, rightLegUp, rightLegDown, leftLegUp, leftLegDown;
    [SerializeField] Animator animator;
    public bool regdollActive { get; private set; }

    private HingeJoint2D[] hJ2D;
    private Rigidbody2D[] rb2D;
    Rigidbody2D rb;

    private Dictionary<Rigidbody2D, Vector3> initialPos = new Dictionary<Rigidbody2D, Vector3>();
    private Dictionary<Rigidbody2D, Quaternion> initialRot = new Dictionary<Rigidbody2D, Quaternion>();

    private bool explosionTime = false;

    void Awake()
    {
        hJ2D = GetComponentsInChildren<HingeJoint2D>();
        rb2D = GetComponentsInChildren<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        foreach (var rb in rb2D)
        {
            initialPos.Add(rb, rb.transform.localPosition);
            initialRot.Add(rb, rb.transform.localRotation);
        }

        explosionTime = true;
        DisableRagdoll(); //래그돌 비활성화
    }

    void RecordTransform()
    {
        foreach (var rb in rb2D)
        {
            initialPos[rb] = rb.transform.localPosition;
            initialRot[rb] = rb.transform.localRotation;
        }
    }

    //애니메이션 비활성화시 래그돌 활성화
    public void ActiveRagdoll()
    {
        regdollActive = true;
        RecordTransform(); //나중에 꺼지기 전에 마지막 뼈의 위치정보를 기록한다.
        animator.enabled = false;
        Transform damageCarrierInfector1 = transform.Find("Body1"); //자식 오브젝트 위치 찾기
        Transform damageCarrierInfector2 = transform.Find("Body1/Body2");
        Transform damageCarrierInfector3 = transform.Find("Body1/Head");
        damageCarrierInfector1.GetComponent<CircleCollider2D>().enabled = false; //사격에서 제외하도록 콜라이더 박스 해제
        damageCarrierInfector2.GetComponent<BoxCollider2D>().enabled = false;
        damageCarrierInfector3.GetComponent<CircleCollider2D>().enabled = false;
        damageCarrierInfector1.gameObject.layer = 0;
        damageCarrierInfector2.gameObject.layer = 0;
        damageCarrierInfector3.gameObject.layer = 0;

        foreach (var rb in rb2D)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        foreach (var joint in hJ2D)
        {
            joint.enabled = true;
        }
    }

    //애니메이션 활성화시 래그돌 비활성화
    public void DisableRagdoll()
    {
        regdollActive = false;
        animator.enabled = true;
        Transform damageCarrierInfector1 = transform.Find("Body1"); //자식 오브젝트 위치 찾기
        Transform damageCarrierInfector2 = transform.Find("Body1/Body2");
        Transform damageCarrierInfector3 = transform.Find("Body1/Head");
        damageCarrierInfector1.GetComponent<CircleCollider2D>().enabled = false; //사격에서 제외하도록 콜라이더 박스 해제
        damageCarrierInfector2.GetComponent<BoxCollider2D>().enabled = false;
        damageCarrierInfector3.GetComponent<CircleCollider2D>().enabled = false;
        damageCarrierInfector1.gameObject.layer = 0;
        damageCarrierInfector2.gameObject.layer = 0;
        damageCarrierInfector3.gameObject.layer = 0;

        foreach (var rb in rb2D)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.transform.localPosition = initialPos[rb];
            rb.transform.localRotation = initialRot[rb];
        }

        foreach (var joint in hJ2D)
        {
            joint.enabled = false;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        if (explosionTime == true)
            rb.AddForce(transform.forward * 50f);
    }
}
