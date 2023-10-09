using System.Collections;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public float XRotateSpeed;
    public float YRotateSpeed;
    public float ZRotateSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(XRotateSpeed * Time.deltaTime, YRotateSpeed * Time.deltaTime, ZRotateSpeed * Time.deltaTime));
    }
}