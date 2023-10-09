using UnityEngine;

public class MissileEffect : MonoBehaviour
{
    public int damage;
    public Transform particlePos;
    ObjectManager objectManager;

    //public Shake shake; // ȭ�� ��鸲 Ŭ���� �ҷ�����

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        Invoke("Active", 1.8f);
    }
    void Active()
    {
        GameObject Explosion = objectManager.Loader("LaserMissileExplosion");
        Explosion.GetComponent<VM5Damage>().damage = this.damage;
        Explosion.transform.position = particlePos.transform.position;
        Explosion.transform.rotation = particlePos.transform.rotation;
        //StartCoroutine(shake.ShakeCamera(0.3f, 0.4f, 0.4f));
    }
}