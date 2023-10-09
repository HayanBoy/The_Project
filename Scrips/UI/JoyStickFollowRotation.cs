using UnityEngine;

public class JoyStickFollowRotation : MonoBehaviour
{
    RectTransform selfAnchor;
    public float TargetPosX;
    public float TargetPosY;

    void Start()
    {
        selfAnchor = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        TargetPosX /= -5;
        TargetPosY /= 5;
        selfAnchor.rotation = Quaternion.Euler(TargetPosY, TargetPosX, selfAnchor.eulerAngles.z);
    }
}