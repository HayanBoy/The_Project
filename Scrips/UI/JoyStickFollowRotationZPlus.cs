using UnityEngine;

public class JoyStickFollowRotationZPlus : MonoBehaviour
{
    RectTransform selfAnchor;
    public float TargetPosX;
    public float TargetPosY;
    public float XChange;
    public float YChange;
    public float PosChange;

    void Start()
    {
        selfAnchor = GetComponent<RectTransform>();
    }

    void Update()
    {
        TargetPosX /= -5;
        TargetPosY /= 5;
        XChange *= -PosChange;
        YChange *= -PosChange;
        selfAnchor.rotation = Quaternion.Euler(TargetPosY, TargetPosX, selfAnchor.eulerAngles.z);
        selfAnchor.anchoredPosition = new Vector2(XChange, YChange);
    }
}