using UnityEngine;

public class DropItemPos : MonoBehaviour
{
    Transform trans;
    int PosRandom;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        //PosRandom = Random.Range(-12, 12); // ������ ù ���� x ��ġ 
        trans.localPosition = new Vector3(0, 30, 0); // ������ ���� ��ġ 
    }
}