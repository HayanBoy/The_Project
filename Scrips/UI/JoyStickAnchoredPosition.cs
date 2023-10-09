using UnityEngine;

public class JoyStickAnchoredPosition : MonoBehaviour
{
    RectTransform selfAnchor;

    float TargetPosX;
    float TargetPosY;

    public GameObject HoldCircle1;
    public GameObject HoldCircle2;
    public GameObject HoldCircle3;
    public GameObject HoldCircle4;

    public GameObject LineCircle1;
    public GameObject LineCircle2;
    public GameObject LineCircle3;
    public GameObject LineCircle4;
    public GameObject LineCircle5;
    public GameObject LineCircle6;
    public GameObject LineCircle7;
    public GameObject LineCircle8;
    public GameObject LineCircle9;
    public GameObject LineCircle10;
    public GameObject LineCircle11;

    bool CircleOnline;
    public bool HolderOnline1;
    public bool HolderOnline2;
    public bool HolderOnline3;
    public bool HolderOnline4;
    public bool Online1;
    public bool Online2;
    public bool Online3;
    public bool Online4;
    public bool Online5;
    public bool Online6;
    public bool Online7;
    public bool Online8;
    public bool Online9;
    public bool Online10;
    public bool Online11;

    void Start()
    {
        selfAnchor = GetComponent<RectTransform>();
        CircleOnline = true;
    }

    void Update()
    {
        TargetPosX = selfAnchor.anchoredPosition.x;
        TargetPosY = selfAnchor.anchoredPosition.y;

        if (HolderOnline1)
        {
            HoldCircle1.GetComponent<JoyStickFollowRotation>().TargetPosX = TargetPosX;
            HoldCircle1.GetComponent<JoyStickFollowRotation>().TargetPosY = TargetPosY;
        }
        if (HolderOnline2)
        {
            HoldCircle2.GetComponent<JoyStickFollowRotation>().TargetPosX = TargetPosX;
            HoldCircle2.GetComponent<JoyStickFollowRotation>().TargetPosY = TargetPosY;
        }
        if (HolderOnline3)
        {
            HoldCircle3.GetComponent<JoyStickFollowRotation>().TargetPosX = TargetPosX;
            HoldCircle3.GetComponent<JoyStickFollowRotation>().TargetPosY = TargetPosY;
        }
        if (HolderOnline4)
        {
            HoldCircle4.GetComponent<JoyStickFollowRotation>().TargetPosX = TargetPosX;
            HoldCircle4.GetComponent<JoyStickFollowRotation>().TargetPosY = TargetPosY;
        }

        if (CircleOnline == true)
        {
            if (Online1)
            {
                LineCircle1.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle1.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle1.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle1.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online2)
            {
                LineCircle2.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle2.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle2.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle2.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online3)
            {
                LineCircle3.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle3.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle3.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle3.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online4)
            {
                LineCircle4.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle4.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle4.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle4.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online5)
            {
                LineCircle5.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle5.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle5.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle5.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online6)
            {
                LineCircle6.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle6.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle6.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle6.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online7)
            {
                LineCircle7.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle7.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle7.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle7.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online8)
            {
                LineCircle8.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle8.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle8.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle8.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online9)
            {
                LineCircle9.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle9.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle9.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle9.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online10)
            {
                LineCircle10.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle10.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle10.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle10.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
            if (Online11)
            {
                LineCircle11.GetComponent<JoyStickFollowRotationZPlus>().TargetPosX = TargetPosX;
                LineCircle11.GetComponent<JoyStickFollowRotationZPlus>().TargetPosY = TargetPosY;
                LineCircle11.GetComponent<JoyStickFollowRotationZPlus>().XChange = TargetPosX;
                LineCircle11.GetComponent<JoyStickFollowRotationZPlus>().YChange = TargetPosY;
            }
        }
    }
}