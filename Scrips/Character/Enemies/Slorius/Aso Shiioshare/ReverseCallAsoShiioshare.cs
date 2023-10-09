using UnityEngine;

public class ReverseCallAsoShiioshare : MonoBehaviour
{
    void Update()
    {
        Impact call1 = transform.Find("Body1/Head").GetComponent<Impact>();
        call1.Roll = 2;
        Impact call2 = transform.Find("Body1/Right sholder/Right arm1").GetComponent<Impact>();
        call2.Roll = 2;
        Impact call3 = transform.Find("Body1/Right sholder/Right arm1/Right arm2/Right arm3").GetComponent<Impact>();
        call3.Roll = 2;
        Impact call4 = transform.Find("Body1/Left sholder/Left down arm1").GetComponent<Impact>();
        call4.Roll = 2;
        Impact call5 = transform.Find("Body1/Left sholder/Left down arm1/Left down arm2/Left down arm3").GetComponent<Impact>();
        call5.Roll = 2;
        Impact call6 = transform.Find("Body1/Left up arm1").GetComponent<Impact>();
        call6.Roll = 2;
        Impact call7 = transform.Find("Body1/Left up arm1/Left up arm2/Left up arm3").GetComponent<Impact>();
        call7.Roll = 2;
        Impact call8 = transform.Find("Body1/Body2/Right leg top").GetComponent<Impact>();
        call8.Roll = 2;
        Impact call9 = transform.Find("Body1/Body2/Right leg top/Right leg down").GetComponent<Impact>();
        call9.Roll = 2;
        Impact call10 = transform.Find("Body1/Body2/Right leg top/Right leg down/Right leg down sub").GetComponent<Impact>();
        call10.Roll = 2;
        Impact call11 = transform.Find("Body1/Body2/Left leg top").GetComponent<Impact>();
        call11.Roll = 2;
        Impact call12 = transform.Find("Body1/Body2/Left leg top/Left leg down").GetComponent<Impact>();
        call12.Roll = 2;
        Impact call13 = transform.Find("Body1/Body2/Left leg top/Left leg down/Left leg down sub").GetComponent<Impact>();
        call13.Roll = 2;
    }
}
