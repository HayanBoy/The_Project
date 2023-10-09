using System.Collections;
using UnityEngine;

public class DropingKaotiJaios4Dual : MonoBehaviour
{
    float dropSpeed = 20f;
    float dropTime;
    bool falling = false;
    bool SlowDown = false;

    public Transform KantakriLandingFrontPos;
    public Transform KantakriLandingBackPos;
    GameObject KantakriLandingFront;
    GameObject KantakriLandingBack;

    Animator animator;

    public void SetDroping(bool droping)
    {
        if (droping == true)
        {
            gameObject.GetComponent<DualBehaviourKaotiJaios5_>().enabled = false; //�ൿ ��ũ��Ʈ ����
            this.gameObject.layer = 27; //���� �̿������� �������� ������ �� �ݶ��̴����� �浹���� ���ϱ� ���� ���̾� ����
            falling = true;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
    }

    private void OnEnable()
    {
        dropSpeed = 20f;
        dropTime = 0;
        falling = false;
        SlowDown = false;
    }

    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("Kaoti-Jaios 4 Drop", false);
    }

    void Update()
    {
        if (falling == true)
        {
            transform.Translate(transform.up * -1 * dropSpeed * Time.deltaTime); //�ϰ�
            StartCoroutine(TheFall()); //�ϰ�
        }
        if (SlowDown == true)
            dropSpeed -= Time.deltaTime * 8.4f; //����
        if (dropSpeed < 0)
        {
            dropSpeed = 0;
        }
    }

    //�ϰ�
    IEnumerator TheFall()
    {
        yield return new WaitForSeconds(1f);
        SlowDown = true;
        GetComponent<Animator>().SetBool("Kaoti-Jaios 4 Drop", true);
        yield return new WaitForSeconds(1.91f);
        dropSpeed = 15;
        yield return new WaitForSeconds(0.15f);

        if (dropTime == 0)
        {
            dropTime += Time.deltaTime;
            KantakriLandingFront = SingletonObject.instance.Loader("KantakriLandingFront");
            KantakriLandingFront.transform.position = KantakriLandingFrontPos.position;
            KantakriLandingFront.transform.rotation = KantakriLandingFrontPos.rotation;
            KantakriLandingBack = SingletonObject.instance.Loader("KantakriLandingBack");
            KantakriLandingBack.transform.position = KantakriLandingBackPos.position;
            KantakriLandingBack.transform.rotation = KantakriLandingBackPos.rotation;
        }
        dropSpeed = 0;
        falling = false;
        yield return new WaitForSeconds(0.85f);
        GetComponent<Animator>().SetBool("Kaoti-Jaios 4 Drop", false);
        SlowDown = false;
        //���� Ȱ��ȭ �� �ൿ ����
        gameObject.GetComponent<DualBehaviourKaotiJaios5_>().enabled = true;
        this.gameObject.layer = 13; //���̾� ����
    }
}