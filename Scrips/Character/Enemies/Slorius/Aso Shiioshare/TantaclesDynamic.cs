using UnityEngine;

public class TantaclesDynamic : MonoBehaviour
{
    private HingeJoint2D hJ2D;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        hJ2D = GetComponent<HingeJoint2D>();
    }

    void OnEnable()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        hJ2D.enabled = true;
    }
}
