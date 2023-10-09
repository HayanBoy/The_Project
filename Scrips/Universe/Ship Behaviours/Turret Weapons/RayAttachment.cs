using UnityEngine;

public class RayAttachment : MonoBehaviour
{
    [Header("���� �߻� ����ġ")]
    public bool Fire;

    [Header("���θ�� ���� ���� ����")]
    public int WeaponType; //���� Ÿ��. 1 = Ray1, Ray2�� ���Ե� ����, 2 = Ray3 ����

    [Header("Ÿ�� ���")]
    public LayerMask LayerMask; //Ÿ�� ���

    [Header("����")]
    public LineRenderer Ray1; //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ����¥�� 1�� ����
    public LineRenderer Ray2; //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ����¥�� 2�� ����
    public LineRenderer Ray3; //���θ�� �ͽ��̿� ���罺 �Ʊ��� ���� ����¥�� ����

    [Header("������ �������� ������")]
    public Transform FirePosition;
    public Vector3 FireEndPos;

    [Header("���� �ǰ� ����Ʈ")]
    public GameObject EndRayVFX; //�ǰ� ����Ʈ

    void EnableRay()
    {
        if (WeaponType == 1)
        {
            Ray1.enabled = true;
            Ray2.enabled = true;
            EndRayVFX.SetActive(true);
        }
        else if (WeaponType == 2)
        {
            Ray3.enabled = true;
            EndRayVFX.SetActive(true);
        }
    }

    void DisableRay()
    {
        if (WeaponType == 1)
        {
            Ray1.enabled = false;
            Ray2.enabled = false;
            EndRayVFX.SetActive(false);
        }
        else if (WeaponType == 2)
        {
            Ray3.enabled = false;
            EndRayVFX.SetActive(false);
        }
    }

    void UpdateRay()
    {
        float RotationZ = transform.rotation.eulerAngles.z;
        RotationZ *= Mathf.Deg2Rad;
        float Length;

        Vector2 Direction = new Vector2(Mathf.Cos(RotationZ), Mathf.Sin(RotationZ));
        Vector2 StartPos = FirePosition.position;

        RaycastHit2D HitRay2D = Physics2D.Raycast(transform.position, Direction.normalized, 1000, LayerMask);
        float EndRotation = 180;

        if (HitRay2D) //������ ���̾� �ݶ��̴��� ���� �浹�� ���, �ش� �浹�� �������� ���� ���
        {
            Length = (HitRay2D.point - StartPos).magnitude;
            Vector2 EndPos = StartPos + Length * Direction;
            EndRotation = Vector2.Angle(Direction, HitRay2D.normal);
            if (WeaponType == 1)
            {
                Ray1.SetPosition(1, new Vector2(Length, 0));
                Ray2.SetPosition(1, new Vector2(Length, 0));
            }
            else if (WeaponType == 2)
                Ray3.SetPosition(1, new Vector2(Length, 0));
            EndRayVFX.transform.position = EndPos;
            EndRayVFX.transform.rotation= Quaternion.Euler(0, 0, EndRotation);
        }
        else
        {
            Length = 1000;
            Vector2 EndPos = StartPos + Length * Direction;
            if (WeaponType == 1)
            {
                Ray1.SetPosition(1, new Vector2(Length, 0));
                Ray2.SetPosition(1, new Vector2(Length, 0));
            }
            else if (WeaponType == 2)
                Ray3.SetPosition(1, new Vector2(Length, 0));
            EndRayVFX.transform.position = EndPos;
            EndRayVFX.transform.rotation = Quaternion.Euler(0, 0, EndRotation);
        }
    }

    void Update()
    {
        if (Fire == true)
        {
            EnableRay();
            UpdateRay();
        }
        else
            DisableRay();
    }
}