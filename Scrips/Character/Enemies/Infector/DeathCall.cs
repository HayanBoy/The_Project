using UnityEngine;

public class DeathCall : MonoBehaviour
{
    public int layers;
    public float layerTime;

    void Update()
    {
        if(layerTime == 0)
        {
            layerTime = Time.deltaTime;
            layers = Random.Range(0, 6);
        }

        TraceX call1 = transform.Find("bone_1").GetComponent<TraceX>();
        call1.layers = layers;
        Impact call2 = transform.Find("bone_1/bone_f1").GetComponent<Impact>();
        call2.Roll = 1;
        call2.layers = layers;
        Impact call3 = transform.Find("bone_1/bone_2/bone_3").GetComponent<Impact>();
        call3.Roll = 1;
        call3.layers = layers;
        Impact call4 = transform.Find("bone_1/bone_2/bone_3/bone_6").GetComponent<Impact>();
        call4.Roll = 1;
        call4.layers = layers;
        Impact call5 = transform.Find("bone_1/bone_4/bone_5").GetComponent<Impact>();
        call5.Roll = 1;
        call5.layers = layers;
        Impact call6 = transform.Find("bone_1/bone_4/bone_5/bone_7").GetComponent<Impact>();
        call6.Roll = 1;
        call6.layers = layers;
        Impact call7 = transform.Find("bone_1/bone_8/bone_9").GetComponent<Impact>();
        call7.Roll = 1;
        call7.layers = layers;
        Impact call8 = transform.Find("bone_1/bone_8/bone_9/bone_10").GetComponent<Impact>();
        call8.Roll = 1;
        call8.layers = layers;
        Impact call9 = transform.Find("bone_1/bone_11/bone_12").GetComponent<Impact>();
        call9.Roll = 1;
        call9.layers = layers;
        Impact call10 = transform.Find("bone_1/bone_11/bone_12/bone_13").GetComponent<Impact>();
        call10.Roll = 1;
        call10.layers = layers;
    }
}
