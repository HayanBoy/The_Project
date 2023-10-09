using System.Collections;
using UnityEngine;

public class RotationUI : MonoBehaviour
{
    RectTransform selfAnchor;
    public bool ClockRotation; //UI 회전 방향 여부
    public bool isMove; //플레이어의 이동속도에 따른 UI 회전
    public bool HpDown; //플레이어의 체력 상태에 따른 UI 여부

    public float Roll;
    public float MoveSpeed;

    private void Start()
    {
        selfAnchor = GetComponent<RectTransform>();
    }

    void OnEnable()
    {

    }

    void Update()
    {
        if (!isMove) //플레이어가 가만히 있을 때 느리게 회전
        {
            if (ClockRotation)
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, Roll) * Time.deltaTime);
            else
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, -Roll) * Time.deltaTime);
        }
        else //플레이어가 움직일 때 빠르게 회전
        {
            if (ClockRotation)
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, Roll) * MoveSpeed * Time.deltaTime);
            else
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, -Roll) * MoveSpeed * Time.deltaTime);
        }
    }
}