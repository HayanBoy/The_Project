using System.Collections.Generic;
using UnityEngine;

public class RegdollControllerPlayer : MonoBehaviour
{
    [SerializeField] GameObject head, rightArmUp, rightArmDown, leftArmUp, leftArmDown, rightLegUp, rightLegDown, leftLegUp, leftLegDown, rightArmUpPower, rightArmDownPower, leftArmUpPower, leftArmDownPower, rightLegUpPower, rightLegDownPower, leftLegUpPower, leftLegDownPower;
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
        DisableRagdoll(); //���׵� ��Ȱ��ȭ
    }

    void RecordTransform()
    {
        foreach (var rb in rb2D)
        {
            initialPos[rb] = rb.transform.localPosition;
            initialRot[rb] = rb.transform.localRotation;
        }
    }

    //�ִϸ��̼� ��Ȱ��ȭ�� ���׵� Ȱ��ȭ
    public void ActiveRagdoll()
    {
        regdollActive = true;
        RecordTransform(); //���߿� ������ ���� ������ ���� ��ġ������ ����Ѵ�.
        animator.enabled = false;

        foreach (var rb in rb2D)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        foreach (var joint in hJ2D)
        {
            joint.enabled = true;
        }
    }

    //�ִϸ��̼� Ȱ��ȭ�� ���׵� ��Ȱ��ȭ
    public void DisableRagdoll()
    {
        regdollActive = false;
        animator.enabled = true;

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