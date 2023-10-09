using UnityEngine;

public class MapRouter : MonoBehaviour
{
    public LineRenderer Router;
    public GameObject firePos;
    public RectTransform EndPosition;
    public RectTransform PlayerTransform;
    public bool RouterCreateStart;

    public void CreateLine(RectTransform playerPos, RectTransform Destination)
    {
        PlayerTransform = playerPos;
        EndPosition = Destination;
        RouterCreateStart = true;
    }

    public void DeleteLine()
    {
        RouterCreateStart = false;
        Router.SetPosition(1, new Vector2(0, 0));
    }

    void Update()
    {
        if (RouterCreateStart == true)
        {
            firePos.transform.position = transform.position;
            float anchoredPos1 = EndPosition.anchoredPosition.x - PlayerTransform.anchoredPosition.x;
            float anchoredPos2 = EndPosition.anchoredPosition.y - PlayerTransform.anchoredPosition.y;
            Router.SetPosition(1, new Vector2(anchoredPos1, anchoredPos2));
        }
    }
}