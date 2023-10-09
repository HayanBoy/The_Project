using UnityEngine;

public class ShipLaserSize : MonoBehaviour
{
    public GameObject Body;

    void Start()
    {
        if (Body.transform.localScale.x <= 0.7f && Body.transform.localScale.x >= 0.65f)
            transform.localScale = new Vector3(0.7f, transform.localScale.y, 1);
        else if(Body.transform.localScale.x <= 0.65f && Body.transform.localScale.x >= 0.6f)
            transform.localScale = new Vector3(0.8f, transform.localScale.y, 1);
        else if(Body.transform.localScale.x <= 0.6f && Body.transform.localScale.x >= 0.5f)
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        else if (Body.transform.localScale.x < 0.5f && Body.transform.localScale.x >= 0.25f)
            transform.localScale = new Vector3((transform.localScale.x + ((0.5f - Body.transform.localScale.x)) * 3.5f), transform.localScale.y, 1);
        else if (Body.transform.localScale.x < 0.25f && Body.transform.localScale.x >= 0.2f)
            transform.localScale = new Vector3(3, transform.localScale.y, 1);
        else if (Body.transform.localScale.x < 0.2f && Body.transform.localScale.x >= 0.15f)
            transform.localScale = new Vector3(3.7f, transform.localScale.y, 1);
    }
}