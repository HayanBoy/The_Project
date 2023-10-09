using System.Collections;
using UnityEngine;

public class LEDRotation : MonoBehaviour
{
    public int RotaionLED = 0;
    private float Rotate = 10f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Rotate));

        if (RotaionLED == 0)
            Rotate = 10f;

        else if (RotaionLED == 1)
            Rotate += Time.deltaTime * 2;
    }
}
