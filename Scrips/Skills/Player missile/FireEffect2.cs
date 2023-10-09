using UnityEngine;

public class FireEffect2 : MonoBehaviour
{
    public void StopEffect()
    {
        Invoke("Delete", 2);
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
