using UnityEngine;

public class NoScaleGameObject : MonoBehaviour
{
    public bool Direction = true;
    private float orthoOrg;
    private float orthoCurr;
    private Vector3 scaleOrg;

    void Start()
    {
        orthoOrg = 10; //처음 켜질 때 Camera.main.orthographicSize값에 따라 오브젝트 첫 크기가 결정되므로, 크기는 10기준으로 시작한다.
        orthoCurr = orthoOrg;
        scaleOrg = transform.localScale;
    }
    void Update()
    {
        var osize = Camera.main.orthographicSize;
        if (orthoCurr != osize)
        {
            if (Direction == true)
            {
                transform.localScale = scaleOrg * osize / orthoOrg;
                orthoCurr = osize;
            }
            else
            {
                transform.localScale = scaleOrg / (osize / orthoOrg);
                orthoCurr = osize;
            }
        }
    }
}