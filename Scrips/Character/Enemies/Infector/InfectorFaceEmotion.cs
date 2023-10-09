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

    //Face3, 4, 5ÀÇ ´«½ç °íÁ¤
    public void setEmotion(int num)
    {
        emotion = num;
    }

    //¸ðÀÚ Âø¿ë½Ã ÀÔ ¹ú¸®±â °­µµ Á¶Àý¿ë
    public void setCap(int num)
    {
        capSet = num;
    }

    //ÀÔÀÎÁö ÅÎÀÎÁö ±¸ºÐÇÏ±â À§ÇÔ
    public void mouth(int num)
    {
        mouthType = num;
    }

    //¸¶½ºÅ© Èçµé¸² ±¸Çö
    public void MaskSwing(int num)
    {
        masksweeping = num;
    }

    //ÇØ°ñ ¿òÁ÷ÀÓ ±¸Çö
    public void SkeletonSwing(int num)
    {
        skeletonsweeping = num;
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        //´«½ç °íÁ¤
        if (emotion == 1)
        {
            animator.SetBool("Face3 eyeborrow fix", true);
        }

        StartCoroutine(angryMotion()); //È­³­ ´«½ç Ç¥Çö

        if(mouthType == 0 && attack == false)
        {
            if (capSet == 1)
            {
                StartCoroutine(jawMotionInCap());
            }
            else
            {
                StartCoroutine(jawMotion()); //ÅÎ ¹ú¸®±â
            }
        }
        else if (mouthType == 1 && attack == false)
        {
            StartCoroutine(mouthMotion()); //ÀÔ ¹ú¸®±â
        }

        if(skeletonsweeping == 1 && moving == false && attack == false)
        {
            StartCoroutine(SkeletonMove()); //ÇØ°ñ ¾ó±¼ ¿òÁ÷ÀÓ
        }

        if (masksweeping == 1 && moving == false && attack == false || capSet == 1)
        {
            StartCoroutine(MaskMovement()); //¸¶½ºÅ© ¿òÁ÷ÀÓ
        }
    }

    //È­³­ ´«½ç Ç¥Çö
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

    //ÅÎ ¹ú¸®±â
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

    //¸ðÀÚ Âø¿ë »óÅÂ¿¡¼­ÀÇ ÅÎ ¹ú¸®±â
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

    //ÀÔ ¹ú¸®±â
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

    //ÇØ°ñ ¾ó±¼ ¿òÁ÷ÀÌ±â
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

    //¸¶½ºÅ© ¿òÁ÷ÀÌ±â
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
