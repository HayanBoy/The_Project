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
        //PosRandom = Random.Range(-12, 12); // 아이템 첫 생성 x 위치 
        trans.localPosition = new Vector3(0, 30, 0); // 아이템 생성 위치 
    }
}