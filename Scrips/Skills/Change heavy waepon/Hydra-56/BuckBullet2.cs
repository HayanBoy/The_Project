using UnityEngine;
using System.Collections;  // IEunmerator 쓰기 위해 선언 

public class BuckBullet2 : MonoBehaviour
{
    float AmmoVelocity;
    public float MaxAmmoVelocity;
    public float MinAmmoVelocity;

    int damage;
    public float ExplosionTime;
    public Transform ExplosionPos;
    ObjectManager objectManager;

    public AudioClip Hydra56AmmoBoom;

    public void SetDamage(int num)
    {
        damage = num;
    }

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        AmmoVelocity = Random.Range(MaxAmmoVelocity, MinAmmoVelocity);
        Invoke("FinalExplosion", ExplosionTime);
    }
    void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate((transform.right * 1 + transform.up * 0.2f) * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate((transform.right * -1 + transform.up * 0.2f) * AmmoVelocity * Time.deltaTime);
        }
    }

    void FinalExplosion()
    {
        GameObject Explosion = objectManager.Loader("HydraExplosion");
        Explosion.transform.position = ExplosionPos.transform.position;
        Explosion.transform.rotation = ExplosionPos.transform.rotation;
        VM5Damage VM5Damage = Explosion.gameObject.transform.GetComponent<VM5Damage>();
        VM5Damage.damage = damage;
        SoundManager.instance.SFXPlay23("DT-37 Fire Sound", Hydra56AmmoBoom);
    }
}