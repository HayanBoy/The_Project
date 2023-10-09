using UnityEngine;

public class ParticlePos : MonoBehaviour
{
    public Transform particlePos;
    GameObject particle;
    public string ExplosionName;

    void OnEnable()
    {
        particle = SingletonObject.instance.Loader(ExplosionName);
        particle.transform.position = particlePos.position;
        particle.transform.rotation = particlePos.rotation;
        Invoke("ActiveFalse", 2.5f);
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
