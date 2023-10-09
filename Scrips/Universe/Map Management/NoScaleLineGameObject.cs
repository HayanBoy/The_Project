using UnityEngine;

public class NoScaleLineGameObject : MonoBehaviour
{
    [Header("라인 렌더러")]
    public LineRenderer lineRenderer;
    private float orthoOrg;
    private float orthoCurr;
    private float scaleOrg1;
    private float scaleOrg2;
    private float A;
    private float B;

    [Header("우주맵에서의 라인 머터리얼 타임 시간 조정")]
    public MapRouter MapRouter;
    public Material lineMaterial;
    private float LineTimeSpeed;

    void OnEnable()
    {
        LineTimeSpeed = 0;
        lineMaterial.SetFloat("Time", 0);
        orthoOrg = Camera.main.orthographicSize;
        orthoCurr = orthoOrg;
        scaleOrg1 = 0.25f;
        scaleOrg2 = 0.25f;
    }
    void Update()
    {
        var osize = Camera.main.orthographicSize;
        if (orthoCurr != osize)
        {
            A = scaleOrg1 * osize / orthoOrg;
            B = scaleOrg2 * osize / orthoOrg;
            lineRenderer.startWidth = A;
            lineRenderer.endWidth = B;
            orthoCurr = osize;
        }

        if (MapRouter.RouterCreateStart == true)
        {
            LineTimeSpeed += 0.5f * Time.unscaledDeltaTime;
            lineMaterial.SetFloat("Time", LineTimeSpeed);
        }
    }
}