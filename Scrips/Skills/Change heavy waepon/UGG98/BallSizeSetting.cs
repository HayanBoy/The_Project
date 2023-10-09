using UnityEngine;

public class BallSizeSetting : MonoBehaviour
{
    public float SizeOverAdd;
    public float FinalSize;
    float TimeStemp = 0;

    void Update()
    {
        TimeStemp += Time.deltaTime;

        if(TimeStemp < FinalSize)
            transform.localScale = new Vector3(transform.localScale.x + 1f * SizeOverAdd * Time.deltaTime, transform.localScale.y + 1f * SizeOverAdd * Time.deltaTime, 0);
    }
}
