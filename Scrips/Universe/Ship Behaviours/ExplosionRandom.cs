using UnityEngine;

public class ExplosionRandom : MonoBehaviour
{
    public int ExplosionCount;
    public GameObject Explosion1;
    public GameObject Explosion2;
    public GameObject Explosion3;
    public GameObject Explosion4;
    public GameObject Explosion5;
    public GameObject Explosion6;
    public GameObject Explosion7;
    public GameObject Explosion8;
    public GameObject Explosion9;
    public GameObject Explosion10;

    void OnEnable()
    {
        int PrintExplosion = Random.Range(0, ExplosionCount);

        if (PrintExplosion == 0)
            Explosion1.SetActive(true);
        else if (PrintExplosion == 1)
            Explosion2.SetActive(true);
        else if (PrintExplosion == 2)
            Explosion3.SetActive(true);
        else if (PrintExplosion == 3)
            Explosion4.SetActive(true);
        else if (PrintExplosion == 4)
            Explosion5.SetActive(true);
        else if (PrintExplosion == 5)
            Explosion6.SetActive(true);
        else if (PrintExplosion == 6)
            Explosion7.SetActive(true);
        else if (PrintExplosion == 7)
            Explosion8.SetActive(true);
        else if (PrintExplosion == 8)
            Explosion9.SetActive(true);
        else if (PrintExplosion == 9)
            Explosion10.SetActive(true);
    }
}