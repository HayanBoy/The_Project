using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    public bool isNariha;
    public float Speed;

    void Update()
    {
        //�Ѿ� �̵�
        if (isNariha == true)
        {
            transform.Translate(transform.right * 1 * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * Speed * Time.deltaTime);
        }
    }

    //�浹��, ��� �ۿ� �����ϹǷ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.CompareTag("Disapear"))
        {
            Destroy(gameObject);
        }
    }
}