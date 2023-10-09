using UnityEngine;

public class GrenadeMarkMove : MonoBehaviour
{
    public Transform Player;
    public Transform Player1;
    private float Speed;
    private float BombCharge;

    public void Throw(bool boolean)
    {
        if (boolean == true)
            GetComponent<Animator>().SetBool("Throw, Grenade mark", true);
        else
            GetComponent<Animator>().SetBool("Throw, Grenade mark", false);
    }

    void Update()
    {
        if (BombCharge < 1)
        {
            BombCharge += Time.deltaTime;
            //Speed += 30 * Time.deltaTime;
            //transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        transform.position = Player.position;

        if (Player1.rotation.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}