using UnityEngine;

public class DeathCallPlayer : MonoBehaviour
{
    public int layers;
    public float layerTime;

    public GameObject Head;
    public GameObject Body;
    public GameObject rightArmUp;
    public GameObject rightArmDown;
    public GameObject leftArmUp;
    public GameObject leftArmDown;
    public GameObject rightLegUp;
    public GameObject rightLegDown;
    public GameObject leftLegUp;
    public GameObject leftLegDown;
    public GameObject rightArmUpPower, rightArmDownPower, leftArmUpPower, leftArmDownPower, rightLegUpPower, rightLegDownPower, leftLegUpPower, leftLegDownPower;

    void Update()
    {
        if (layerTime == 0)
        {
            layerTime = Time.deltaTime;
            layers = Random.Range(0, 6);
        }

        TraceX call1 = Body.GetComponent<TraceX>();
        call1.layers = layers;
        Impact call2 = Head.GetComponent<Impact>();
        call2.Roll = 1;
        call2.layers = layers;
        Impact call3 = rightArmUp.GetComponent<Impact>();
        call3.Roll = 1;
        call3.layers = layers;
        Impact call4 = rightArmDown.GetComponent<Impact>();
        call4.Roll = 1;
        call4.layers = layers;
        Impact call5 = leftArmUp.GetComponent<Impact>();
        call5.Roll = 1;
        call5.layers = layers;
        Impact call6 = leftArmDown.GetComponent<Impact>();
        call6.Roll = 1;
        call6.layers = layers;
        Impact call7 = rightLegUp.GetComponent<Impact>();
        call7.Roll = 1;
        call7.layers = layers;
        Impact call8 = rightLegDown.GetComponent<Impact>();
        call8.Roll = 1;
        call8.layers = layers;
        Impact call9 = leftLegUp.GetComponent<Impact>();
        call9.Roll = 1;
        call9.layers = layers;
        Impact call10 = leftLegDown.GetComponent<Impact>();
        call10.Roll = 1;
        call10.layers = layers;
        Impact call11 = rightArmUpPower.GetComponent<Impact>();
        call11.Roll = 1;
        call11.layers = layers;
        Impact call12 = rightArmDownPower.GetComponent<Impact>();
        call12.Roll = 1;
        call12.layers = layers;
        Impact call13 = leftArmUpPower.GetComponent<Impact>();
        call13.Roll = 1;
        call13.layers = layers;
        Impact call14 = leftArmDownPower.GetComponent<Impact>();
        call14.Roll = 1;
        call14.layers = layers;
        Impact call15 = rightLegUpPower.GetComponent<Impact>();
        call15.Roll = 1;
        call15.layers = layers;
        Impact call16 = rightLegDownPower.GetComponent<Impact>();
        call16.Roll = 1;
        call16.layers = layers;
        Impact call17 = leftLegUpPower.GetComponent<Impact>();
        call17.Roll = 1;
        call17.layers = layers;
        Impact call18 = leftLegDownPower.GetComponent<Impact>();
        call18.Roll = 1;
        call18.layers = layers;
    }
}