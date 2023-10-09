using UnityEngine;

public class IconHolder : MonoBehaviour
{
    public Transform iconPos;

    private void OnEnable()
    {
        transform.position = iconPos.position;
    }
}