using UnityEngine;

public class GravityBall2 : MonoBehaviour
{
    private float Disappearing;
    public int damage;

    public GameObject Explosion;
    public Transform ExplosionPos;

    public int StartType;

    //중력볼 사라지는 시간 원격
    public void ChargeBall(float num)
    {
        Disappearing = num;
    }

    public void BallCreateStart()
    {
        if (StartType == 1)
            GetComponent<Animator>().SetBool("Gravity ball start1", true);
        else
            GetComponent<Animator>().SetBool("Gravity ball start2", true);
    }

    void Start()
    {
        Invoke("DisappearingStart", Disappearing);
        Invoke("DestroyEffect", Disappearing + 0.6f);
        Destroy(gameObject, Disappearing + 2.6f);
    }

    void DisappearingStart()
    {
        if (StartType == 1)
            GetComponent<Animator>().SetBool("Gravity ball end1", true);
        else
            GetComponent<Animator>().SetBool("Gravity ball end2", true);
    }

    void DestroyEffect()
    {
        GameObject BallDisappearingEffect = Instantiate(Explosion, ExplosionPos.transform.position, ExplosionPos.transform.rotation);
        BallDisappearingEffect.GetComponent<VM5Damage>().damage = this.damage;
    }
}