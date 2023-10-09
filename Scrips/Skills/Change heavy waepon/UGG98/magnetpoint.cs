using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetpoint : MonoBehaviour
{
    public float forceFactor;

    List<Rigidbody2D> rgEnemys = new List<Rigidbody2D>();
    Transform magnetPoint;
    // Start is called before the first frame update
    void Start()
    {
        magnetPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Rigidbody2D enemy in rgEnemys)
        {
            enemy.AddForce( (magnetPoint.position - enemy.transform.position) * forceFactor * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kantakri, Kaoti-Jaios 4") || other.CompareTag("Kantakri, Taika-Lai-Throtro 1") || other.CompareTag("Kantakri, Kaoti-Jaios 4 Spear") || other.CompareTag("Kantakri, Kaoti-Jaios 4 Armor") || other.CompareTag("Kantakri, Kakros-Taijaelos 1389") || other.CompareTag("Infector, Standard") || other.CompareTag("Infector"))
            rgEnemys.Add(other.GetComponent<Rigidbody2D>());
    }
}
