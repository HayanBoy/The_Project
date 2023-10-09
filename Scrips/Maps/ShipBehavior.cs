using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    public bool isNariha;
    public float Speed;

    void Update()
    {
        //총알 이동
        if (isNariha == true)
        {
            transform.Translate(transform.right * 1 * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * Speed * Time.deltaTime);
        }
    }

    //충돌시, 배경 밖에 존재하므로 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.CompareTag("Disapear"))
        {
            Destroy(gameObject);
        }
    }
}