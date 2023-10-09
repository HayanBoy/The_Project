using UnityEngine;

public class PlazmaEffectScript1 : MonoBehaviour
{
    public GameObject Prefab;
    public Transform particlePos;

    GameObject[] Particle;

  
    void Start()
    {
        Particle = new GameObject[20];

        Generate();

        Invoke("ActiveFalse", 1.5f);
        Invoke("Active", 0.5f);
        //  GameObject Particle = Instantiate(Prefab, particlePos.position, transform.rotation);
  
    }

    void Generate()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            Particle[index] = Instantiate(Prefab);
            Particle[index].SetActive(false);
        }
    }

    public GameObject Loader()
    {
        for (int index = 0; index < Particle.Length; index++)
        {
            if (!Particle[index].activeSelf)
            {
                Particle[index].SetActive(true);
                return Particle[index];
            }
        }

        return null;
    }
    void Active()
    {
        GameObject Effect = Loader();
        Effect.transform.position = particlePos.position;
        Effect.transform.rotation= particlePos.rotation;
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
