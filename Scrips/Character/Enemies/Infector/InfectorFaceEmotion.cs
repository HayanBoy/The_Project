using System.Collections;
using UnityEngine;

public class InfectorFaceEmotion : MonoBehaviour
{
    public int emotion = 0;
    public int capSet = 0;
    public int mouthType = 0;
    public int masksweeping = 0;
    public int skeletonsweeping = 0;

    private int angry;
    private int jawMove;
    private int mouthMove;

    bool moving = false;
    bool attack = false;

    Animator animator;

    //Face3, 4, 5�� ���� ����
    public void setEmotion(int num)
    {
        emotion = num;
    }

    //���� ����� �� ������ ���� ������
    public void setCap(int num)
    {
        capSet = num;
    }

    //������ ������ �����ϱ� ����
    public void mouth(int num)
    {
        mouthType = num;
    }

    //����ũ ��鸲 ����
    public void MaskSwing(int num)
    {
        masksweeping = num;
    }

    //�ذ� ������ ����
    public void SkeletonSwing(int num)
    {
        skeletonsweeping = num;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        //���� ����
        if (emotion == 1)
        {
            animator.SetBool("Face3 eyeborrow fix", true);
        }

        StartCoroutine(angryMotion()); //ȭ�� ���� ǥ��

        if(mouthType == 0 && attack == false)
        {
            if (capSet == 1)
            {
                StartCoroutine(jawMotionInCap());
            }
            else
            {
                StartCoroutine(jawMotion()); //�� ������
            }
        }
        else if (mouthType == 1 && attack == false)
        {
            StartCoroutine(mouthMotion()); //�� ������
        }

        if(skeletonsweeping == 1 && moving == false && attack == false)
        {
            StartCoroutine(SkeletonMove()); //�ذ� �� ������
        }

        if (masksweeping == 1 && moving == false && attack == false || capSet == 1)
        {
            StartCoroutine(MaskMovement()); //����ũ ������
        }
    }

    //ȭ�� ���� ǥ��
    IEnumerator angryMotion()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            angry = Random.Range(0, 3);

            if (angry == 0)
            {
                animator.SetBool("Face eyeborrow angry, Infector", true);
            }
            else
            {
                animator.SetBool("Face eyeborrow angry, Infector", false);
            }
        }
    }

    //�� ������
    IEnumerator jawMotion()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            jawMove = Random.Range(0, 4);

            if (jawMove == 0)
            {
                animator.SetBool("Face jawmovement1", true);
            }
            else
            {
                animator.SetBool("Face jawmovement1", false);
            }
        }
    }

    //���� ���� ���¿����� �� ������
    IEnumerator jawMotionInCap()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            jawMove = Random.Range(0, 4);

            if (jawMove == 0)
            {
                animator.SetBool("Face jawmovement2", true);
            }
            else
            {
                animator.SetBool("Face jawmovement2", false);
            }
        }
    }

    //�� ������
    IEnumerator mouthMotion()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            mouthMove = Random.Range(0, 4);

            if (mouthMove == 0)
            {
                animator.SetBool("Face mouth movement", true);
            }
            else
            {
                animator.SetBool("Face mouth movement", false);
            }
        }
    }

    //�ذ� �� �����̱�
    IEnumerator SkeletonMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            angry = Random.Range(0, 2);

            if (angry == 0)
            {
                animator.SetBool("Face skeleton movement, Infector", true);
            }
            else
            {
                animator.SetBool("Face skeleton movement, Infector", false);
            }
        }
    }

    //����ũ �����̱�
    IEnumerator MaskMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            angry = Random.Range(0, 2);

            if (angry == 0)
            {
                animator.SetBool("Face mask movement, Infector", true);
            }
            else
            {
                animator.SetBool("Face mask movement, Infector", false);
            }
        }
    }

    void Update()
    {

    }
}
