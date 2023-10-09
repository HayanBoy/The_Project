using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float magnetStrength = 1f;
    public float distanceStrength = 10f;
    public int magnetDirection = 1;
    public bool looseMagnet = true;

    private Transform trans;
    Rigidbody2D thisRb;
    private Transform magnetTrans;
    private bool magnetInzone;

    //void Awake()
    //{
    //    trans = transform;
    //    thisRb = trans.GetComponent<Rigidbody2D>();
    //}

    //void Update()
    //{
    //    if (magnetInzone)
    //    {
    //        Vector2 directionToMagnet = magnetTrans.position - trans.position;
    //        float distance = Vector2.Distance(magnetTrans.position, trans.position);
    //        float magentDistanceStr = (distanceStrength / distance) * magnetStrength;
    //        //thisRb.velocity = magentDistanceStr * (directionToMagnet * magnetDirection);
    //        thisRb.AddForce(magentDistanceStr * (directionToMagnet * magnetDirection), ForceMode2D.Force);
    //    }
    //}

    //void ActiveFalse()
    //{
    //    magnetInzone = false;
    //}


    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Magnet")
    //    {
    //        magnetTrans = other.transform;
    //        magnetInzone = true;
    //        thisRb.velocity = Vector3.zero;
    //        Invoke("ActiveFalse", 5.00000000001f);
    //    }
    //}
}