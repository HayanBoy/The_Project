using System.Collections;
using UnityEngine;

public class RobotMoveLand1 : MonoBehaviour
{
    public bool TurnOff;

    public AudioClip LandFoot;

    void OnEnable()
    {
        if (TurnOff == false)
            SoundManager.instance.SFXPlay4("Sound", LandFoot);
        TurnOff = true;
        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(0.2f);
        TurnOff = false;
    }
}