using UnityEngine;

public class LineTimeLine : MonoBehaviour
{
    [Header("���� ���͸��� Ÿ�� �ð� ����")]
    public Material lineMaterial;
    private float LineTimeSpeed;

    void Update()
    {
        LineTimeSpeed += 1.5f * Time.deltaTime;
        lineMaterial.SetFloat("Time", LineTimeSpeed);
    }
}