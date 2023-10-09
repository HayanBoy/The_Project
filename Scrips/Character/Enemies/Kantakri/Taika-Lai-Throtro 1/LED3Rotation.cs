using System.Collections;
using UnityEngine;

public class LED3Rotation : MonoBehaviour
{
    public int RotaionLED = 0;
    private float Rotate = 2.5f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Rotate));

        if (RotaionLED == 0)
            Rotate = 2.5f;

        else if (RotaionLED == 1)
            Rotate += Time.deltaTime * 3;
    }
}
