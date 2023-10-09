using UnityEngine;

public class BeamWidthSetting : MonoBehaviour
{
    LineRenderer lineRenderer;

    private float StartWidth;
    private float EndWidth;

    public float SettingWidthUpSpeed;
    public float EndWidthPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
    }

    void Update()
    {
        lineRenderer.startWidth = StartWidth;
        lineRenderer.endWidth = EndWidth;

        StartWidth += Time.deltaTime * SettingWidthUpSpeed;
        EndWidth += Time.deltaTime * SettingWidthUpSpeed;

        if (StartWidth > EndWidthPoint)
            StartWidth = EndWidthPoint;
        if (EndWidth > EndWidthPoint)
            EndWidth = EndWidthPoint;
    }
}
