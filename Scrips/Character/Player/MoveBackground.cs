using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public GameObject Stage;
    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;
    public GameObject Background4;

    public Transform Background1Area;
    public Transform Background2Area;
    public Transform Background3Area;
    public Transform Background4Area;

    private void Start()
    {
        Background1Area.position = Background1.transform.position;
        Background2Area.position = Background2.transform.position;
        Background3Area.position = Background3.transform.position;
        Background4Area.position = Background4.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proximity Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Background1.transform.position = Background1Area.position;
            Background2.transform.position = Background2Area.position;
            Background3.transform.position = Background3Area.position;
            Background4.transform.position = Background4Area.position;

            Invoke("move", 1f);
        }
    }

    void move()
    {
        Stage.transform.position = transform.position;
    }
}