using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public Transform smokePos;

    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("Turn on", true);
    }

    void Update()
    {
        transform.position = smokePos.position;
    }

    public void StopEffect()
    {
        GetComponent<Animator>().SetBool("Turn on", false);
        GetComponent<Animator>().SetBool("Turn off", true);
        Invoke("Delete", 2);
    }

    void Delete()
    {
        GetComponent<Animator>().SetBool("Turn off", false);
        gameObject.SetActive(false);
    }
}