using UnityEngine;

public class ShakeOneTime : MonoBehaviour
{
    private Shake shake;
    public float ShakeStrength;
    public float ShakeTime;

    void Start()
    {
        shake = GameObject.Find("Main Camera").GetComponent<Shake>();
    }

    private void OnEnable()
    {
        Shake.Instance.ShakeCamera(ShakeStrength, ShakeTime);
    }
}