using UnityEngine;

public class ReverseCall : MonoBehaviour
{
    void Update()
    {
        Impact call2 = transform.Find("bone_1/bone_f1").GetComponent<Impact>();
        call2.Roll = 2;
        Impact call3 = transform.Find("bone_1/bone_2/bone_3").GetComponent<Impact>();
        call3.Roll = 2;
        Impact call4 = transform.Find("bone_1/bone_2/bone_3/bone_6").GetComponent<Impact>();
        call4.Roll = 2;
        Impact call5 = transform.Find("bone_1/bone_4/bone_5").GetComponent<Impact>();
        call5.Roll = 2;
        Impact call6 = transform.Find("bone_1/bone_4/bone_5/bone_7").GetComponent<Impact>();
        call6.Roll = 2;
        Impact call7 = transform.Find("bone_1/bone_8/bone_9").GetComponent<Impact>();
        call7.Roll = 2;
        Impact call8 = transform.Find("bone_1/bone_8/bone_9/bone_10").GetComponent<Impact>();
        call8.Roll = 2;
        Impact call9 = transform.Find("bone_1/bone_11/bone_12").GetComponent<Impact>();
        call9.Roll = 2;
        Impact call10 = transform.Find("bone_1/bone_11/bone_12/bone_13").GetComponent<Impact>();
        call10.Roll = 2;
    }
}
