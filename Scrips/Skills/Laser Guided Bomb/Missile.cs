using UnityEngine;

public class Missile : MonoBehaviour
{
    void Update()
    {
        Destroy();
    }
    void StopItem()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero; // 움직임을 멈추고
        rigid.gravityScale = 0.0f; // 중력 0으로 만들어서 멈춘 이후에 천천히 내려가는거 방지
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            //Debug.Log("펑");
            Destroy(this.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject, 1.8f);
    }
}