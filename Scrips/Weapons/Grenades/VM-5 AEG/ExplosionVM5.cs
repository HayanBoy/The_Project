using UnityEngine;

public class ExplosionVM5 : MonoBehaviour
{
    public Transform ExplosionEffectPos;
    public float BoomTime;
    public int damage;
    ObjectManager objectManager;

    public AudioClip VM5GrenadeExplosion;

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        Invoke("Active", BoomTime);
    }

    void Active()
    {
        SoundManager.instance.SFXPlay2("Sound", VM5GrenadeExplosion);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        GameObject VM5Explosion = objectManager.Loader("VM5Explosion");
        VM5Explosion.GetComponent<VM5Damage>().damage = this.damage;
        VM5Explosion.transform.position = ExplosionEffectPos.transform.position;
        VM5Explosion.transform.rotation = ExplosionEffectPos.transform.rotation;
        Destroy(gameObject);
    }
}