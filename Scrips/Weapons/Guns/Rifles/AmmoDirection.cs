using UnityEngine;

public class AmmoDirection : MonoBehaviour
{
    private float DirectionSelection;
    public float MiniumDirection;
    public float MaxDirection;

    void Update()
    {
        DirectionSelection = Random.Range(MiniumDirection, MaxDirection);

        transform.Rotate(new Vector3(0, 0, DirectionSelection));
    }
}
