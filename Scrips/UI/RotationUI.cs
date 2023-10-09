using System.Collections;
using UnityEngine;

public class RotationUI : MonoBehaviour
{
    RectTransform selfAnchor;
    public bool ClockRotation; //UI ȸ�� ���� ����
    public bool isMove; //�÷��̾��� �̵��ӵ��� ���� UI ȸ��
    public bool HpDown; //�÷��̾��� ü�� ���¿� ���� UI ����

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
        if (!isMove) //�÷��̾ ������ ���� �� ������ ȸ��
        {
            if (ClockRotation)
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, Roll) * Time.deltaTime);
            else
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, -Roll) * Time.deltaTime);
        }
        else //�÷��̾ ������ �� ������ ȸ��
        {
            if (ClockRotation)
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, Roll) * MoveSpeed * Time.deltaTime);
            else
                selfAnchor.Rotate(new Vector3(selfAnchor.rotation.x, selfAnchor.rotation.y, -Roll) * MoveSpeed * Time.deltaTime);
        }
    }
}