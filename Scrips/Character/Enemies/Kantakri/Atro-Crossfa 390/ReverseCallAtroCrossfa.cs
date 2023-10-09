using UnityEngine;

public class ReverseCallAtroCrossfa : MonoBehaviour
{
    void Update()
    {
        Impact call1 = transform.Find("Body_1/Left leg").GetComponent<Impact>();
        call1.Roll = 2;
        Impact call2 = transform.Find("Body_1/Left leg/Left leg down1_1").GetComponent<Impact>();
        call2.Roll = 2;
        Impact call3 = transform.Find("Body_1/Left leg/Left leg down1_1/Left leg down2_1").GetComponent<Impact>();
        call3.Roll = 2;
        Impact call4 = transform.Find("Body_1/Left leg/Left leg down1_1/Left leg foot").GetComponent<Impact>();
        call4.Roll = 2;
        Impact call5 = transform.Find("Body_1/Right leg top_1").GetComponent<Impact>();
        call5.Roll = 2;
        Impact call6 = transform.Find("Body_1/Right leg top_1/Right leg down1_1").GetComponent<Impact>();
        call6.Roll = 2;
        Impact call7 = transform.Find("Body_1/Right leg top_1/Right leg down1_1/Right leg down2_1").GetComponent<Impact>();
        call7.Roll = 2;
        Impact call8 = transform.Find("Body_1/Right leg top_1/Right leg down1_1/Right leg foot").GetComponent<Impact>();
        call8.Roll = 2;
    }
}
