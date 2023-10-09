using UnityEngine;

public class DeathCallAtroCrossfa : MonoBehaviour
{
    public int layers;
    public float layerTime;

    void Update()
    {
        if (layerTime == 0)
        {
            layerTime = Time.deltaTime;
            layers = Random.Range(0, 6);
        }

        TraceX call1 = transform.Find("Body_1").GetComponent<TraceX>();
        call1.layers = layers;
        Impact call2 = transform.Find("Body_1/Left leg").GetComponent<Impact>();
        call2.Roll = 1;
        call2.layers = layers;
        Impact call3 = transform.Find("Body_1/Left leg/Left leg down1_1").GetComponent<Impact>();
        call3.Roll = 1;
        call3.layers = layers;
        Impact call4 = transform.Find("Body_1/Left leg/Left leg down1_1/Left leg down2_1").GetComponent<Impact>();
        call4.Roll = 1;
        call4.layers = layers;
        Impact call5 = transform.Find("Body_1/Left leg/Left leg down1_1/Left leg foot").GetComponent<Impact>();
        call5.Roll = 1;
        call5.layers = layers;
        Impact call6 = transform.Find("Body_1/Right leg top_1").GetComponent<Impact>();
        call6.Roll = 1;
        call6.layers = layers;
        Impact call7 = transform.Find("Body_1/Right leg top_1/Right leg down1_1").GetComponent<Impact>();
        call7.Roll = 1;
        call7.layers = layers;
        Impact call8 = transform.Find("Body_1/Right leg top_1/Right leg down1_1/Right leg down2_1").GetComponent<Impact>();
        call8.Roll = 1;
        call8.layers = layers;
        Impact call9 = transform.Find("Body_1/Right leg top_1/Right leg down1_1/Right leg foot").GetComponent<Impact>();
        call9.Roll = 1;
        call9.layers = layers;
    }
}