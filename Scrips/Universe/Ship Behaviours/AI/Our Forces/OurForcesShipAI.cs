//NOT USED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurForcesShipAI : MonoBehaviour
{
    public MoveVelocity MoveVelocity;
    Rigidbody2D rb2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        MoveVelocity.enabled = true;
        Invoke("GetFormation", 0.15f);
    }

    //���� ��� ��ġ ��û
    void GetFormation()
    {
        //MoveVelocity.TransferFormation();
    }

    void Update()
    {
        
    }
}