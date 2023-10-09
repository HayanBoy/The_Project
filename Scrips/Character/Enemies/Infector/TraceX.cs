using UnityEngine;

public class TraceX : MonoBehaviour
{
    public GameObject DeathShadow;
    public Transform DeathShadowPos;

    private float DeathTransform;
    private float DeathTransformTimeCount;
    private float DeathTransformTimeCount2;

    public bool DeathTransformTime = false;
    public bool ShadowReset = false;

    public int layers;
    public float ShadowTime;

    void Update()
    {
        if(ShadowReset == true)
        {
            DeathTransformTimeCount = 0;
            DeathTransformTimeCount2 = 0;
            DeathTransformTime = false;
            ShadowReset = false;
        }

        if (DeathTransformTime == true)
        {
            if(DeathTransformTimeCount2 == 0)
            {
                DeathTransformTimeCount2 += Time.deltaTime;
                DeathTransform = this.transform.position.y;
            }
            Invoke("Shadow", 0.3f);
        }
    }

    void Shadow()
    {
        if (DeathTransform > transform.position.y)
        {
            if (DeathTransformTimeCount == 0)
            {
                //Debug.Log("Shadow");
                DeathTransformTimeCount += Time.deltaTime;
                GameObject Shadow = Instantiate(DeathShadow, DeathShadowPos.transform.position, DeathShadowPos.transform.rotation);
                Shadow.gameObject.layer = layers;
                Shadow.transform.localPosition = new Vector3 (transform.position.x, transform.position.y - 2, transform.position.z);
                Shadow.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                Destroy(Shadow, ShadowTime);
            }
        }
    }
}