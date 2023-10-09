using UnityEngine;

public class DeathCallAsoShiioshare : MonoBehaviour
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

        TraceX call1 = transform.Find("Body1").GetComponent<TraceX>();
        call1.layers = layers;
        Impact call2 = transform.Find("Body1/Head").GetComponent<Impact>();
        call2.Roll = 1;
        call2.layers = layers;
        Impact call3 = transform.Find("Body1/Right sholder/Right arm1").GetComponent<Impact>();
        call3.Roll = 1;
        call3.layers = layers;
        Impact call4 = transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3").GetComponent<Impact>();
        call4.Roll = 1;
        call4.layers = layers;
        Impact call5 = transform.Find("Body1/Left sholder/Left down arm1").GetComponent<Impact>();
        call5.Roll = 1;
        call5.layers = layers;
        Impact call6 = transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3").GetComponent<Impact>();
        call6.Roll = 1;
        call6.layers = layers;
        Impact call7 = transform.Find("Body1/Left up arm1").GetComponent<Impact>();
        call7.Roll = 1;
        call7.layers = layers;
        Impact call8 = transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3").GetComponent<Impact>();
        call8.Roll = 1;
        call8.layers = layers;
        Impact call9 = transform.Find("Body1/Body2/Right leg top").GetComponent<Impact>();
        call9.Roll = 1;
        call9.layers = layers;
        Impact call10 = transform.Find("Body1/Body2/Right leg top/Right leg down").GetComponent<Impact>();
        call10.Roll = 1;
        call10.layers = layers;
        Impact call11 = transform.Find("Body1/Body2/Right leg top/Right leg down/Right leg down sub").GetComponent<Impact>();
        call11.Roll = 1;
        call11.layers = layers;
        Impact call12 = transform.Find("Body1/Body2/Left leg top").GetComponent<Impact>();
        call12.Roll = 1;
        call12.layers = layers;
        Impact call13 = transform.Find("Body1/Body2/Left leg top/Left leg down").GetComponent<Impact>();
        call13.Roll = 1;
        call13.layers = layers;
        Impact call14 = transform.Find("Body1/Body2/Left leg top/Left leg down/Left leg down sub").GetComponent<Impact>();
        call14.Roll = 1;
        call14.layers = layers;
    }
}
