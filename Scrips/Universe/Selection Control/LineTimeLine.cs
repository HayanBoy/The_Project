using UnityEngine;

public class LineTimeLine : MonoBehaviour
{
    [Header("라인 머터리얼 타임 시간 조정")]
    public Material lineMaterial;
    private float LineTimeSpeed;

    void Update()
    {
        LineTimeSpeed += 1.5f * Time.deltaTime;
        lineMaterial.SetFloat("Time", LineTimeSpeed);
    }
}