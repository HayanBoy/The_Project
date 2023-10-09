using UnityEngine;

public class YFollow : MonoBehaviour
{
    public Transform FollowCamera;
    public float FollowType;
    public float Speed;

    void Update()
    {
        if (FollowType == 1)
            transform.position = Vector3.Lerp(transform.position, new Vector3(FollowCamera.position.x, transform.position.y, transform.position.z), Speed * Time.deltaTime);
        else if (FollowType == 2)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(FollowCamera.position.x, transform.position.y, transform.position.z), Speed * Time.deltaTime);
    }
}