using UnityEngine;
using System.Collections;

public class GravityBall1 : MonoBehaviour
{
    public float AmmoVelocity;
    public float BallLifeTime;
    public float FireSpeed;
    public float StopTime;
    public int damage;

    public GameObject GravityBall;
    public GameObject GravityBallTwo;
    public Transform GravityBallPos;

    public int Disappearing = 0;
    public float DestroyBallTime1;
    public float DestroyBallTime2;

    //중력볼 사라지는 시간 원격
    public void ChargeBall(int num)
    {
        Disappearing = num;
    }

    private void Start()
    {
        if (Disappearing == 1)
            Invoke("STOP", StopTime);
        else
            Invoke("STOP2", StopTime);
        Destroy(gameObject, StopTime + 0.001f);
    }

    void STOP()
    {
        GameObject BallGenerator = Instantiate(GravityBall, GravityBallPos.transform.position, GravityBallPos.transform.rotation);
        BallGenerator.GetComponent<GravityBall2>().damage = this.damage;
        BallGenerator.GetComponent<GravityBall2>().ChargeBall(DestroyBallTime1); //중력볼 사라지는 시간 변수 전달
        BallGenerator.GetComponent<GravityBall2>().StartType = 1;
        BallGenerator.GetComponent<GravityBall2>().BallCreateStart();
    }

    void STOP2()
    {
        GameObject BallGenerator2 = Instantiate(GravityBallTwo, GravityBallPos.transform.position, GravityBallPos.transform.rotation);
        BallGenerator2.GetComponent<GravityBall2>().damage = this.damage;
        BallGenerator2.GetComponent<GravityBall2>().ChargeBall(DestroyBallTime2); //중력볼 사라지는 시간 변수 전달
        BallGenerator2.GetComponent<GravityBall2>().StartType = 2;
        BallGenerator2.GetComponent<GravityBall2>().BallCreateStart();
    }

    void Update()
    {
        Destroy(gameObject, BallLifeTime);

        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * FireSpeed * AmmoVelocity * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -FireSpeed * AmmoVelocity * Time.deltaTime);
        }
    }
}