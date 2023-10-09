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
        rigid.velocity = Vector3.zero; // �������� ���߰�
        rigid.gravityScale = 0.0f; // �߷� 0���� ���� ���� ���Ŀ� õõ�� �������°� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            //Debug.Log("��");
            Destroy(this.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject, 1.8f);
    }
}