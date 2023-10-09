using UnityEngine;

public class BombCountDownText : MonoBehaviour
{
    public TextMesh CountDownNumber;
    BombSettings BombSettings;

    void Start()
    {
        BombSettings = FindObjectOfType<BombSettings>();
    }

    void Update()
    {
        if (BombSettings.ExplosionTime > 0)
            CountDownNumber.text = string.Format("{0:F2}", BombSettings.ExplosionTime);
        else if (BombSettings.ExplosionTime <= 0)
        {
            CountDownNumber.text = string.Format("");
            gameObject.SetActive(false);
        }
    }
}
