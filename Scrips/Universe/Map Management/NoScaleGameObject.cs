using UnityEngine;

public class NoScaleGameObject : MonoBehaviour
{
    public bool Direction = true;
    private float orthoOrg;
    private float orthoCurr;
    private Vector3 scaleOrg;

    void Start()
    {
        orthoOrg = 10; //ó�� ���� �� Camera.main.orthographicSize���� ���� ������Ʈ ù ũ�Ⱑ �����ǹǷ�, ũ��� 10�������� �����Ѵ�.
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