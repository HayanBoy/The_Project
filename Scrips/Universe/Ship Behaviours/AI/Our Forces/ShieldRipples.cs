using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRipples : MonoBehaviour
{
    public Material ShieldMaterial;

    public void ShieldDefenceEffect(Vector2 Transform)
    {
        ShieldMaterial = GetComponent<Renderer>().materials[0]; //���͸����� ���������� ���� �ٸ� ��ġ�� ������ �� �ֵ��� ����
        Vector3 Area = new Vector3(Transform.x, Transform.y, -4.23f);
        ShieldMaterial.SetVector("_SphereCenter", Area);
        Destroy(gameObject, 2);
    }
}