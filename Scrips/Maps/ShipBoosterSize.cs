using UnityEngine;

public class ShipBoosterSize : MonoBehaviour
{
    public GameObject Body;

    void Start()
    {
        if (Body.transform.localScale.x <= 0.7f && Body.transform.localScale.x >= 0.65f)
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        else if (Body.transform.localScale.x <= 0.65f && Body.transform.localScale.x >= 0.6f)
            transform.localScale = new Vector3(1.3f, 1.3f, 1);
        else if (Body.transform.localScale.x <= 0.6f && Body.transform.localScale.x >= 0.5f)
            transform.localScale = new Vector3(1.1f, 1.1f, 1);
        else if (Body.transform.localScale.x < 0.5f && Body.transform.localScale.x >= 0.4f)
            transform.localScale = new Vector3(0.9f, 0.9f, 1);
        else if (Body.transform.localScale.x < 0.4f && Body.transform.localScale.x >= 0.3f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (Body.transform.localScale.x < 0.3f && Body.transform.localScale.x >= 0.2f)
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        else if (Body.transform.localScale.x < 0.2f && Body.transform.localScale.x >= 0.15f)
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
    }
}
