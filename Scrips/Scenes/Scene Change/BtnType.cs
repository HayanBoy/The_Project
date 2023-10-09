using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnType : MonoBehaviour
{
    public GameObject FadeOut;

    public BTNType currentType;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    bool isSound;

    public Image BackToTheMain;
    public Image MainUI;
    public Image StageUI;
    public Image ResetMenu;

    void Start()
    {
        ResetMenu.gameObject.SetActive(false);
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                StartCoroutine(PlayBtn());
                Debug.Log("������");
                break;

            case BTNType.Option:
                Debug.Log("�ɼ�");
                break;

            case BTNType.Sound:
                // audioListener, audioSource�� ���� ����� ��ũ��Ʈ���� �Ҹ� �������ָ� �ȴ�. 
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("���� OFF");
                }
                else
                {
                    isSound = true;
                    Debug.Log("���� ON");
                }
                break;


            case BTNType.Reset:
                ResetMenu.gameObject.SetActive(true);

                break;

            case BTNType.Back:
                Debug.Log("�ڷΰ���");
                break;
        }
    }

    public IEnumerator PlayBtn()
    {
        FadeOut.GetComponent<FadeOut>().stopOut = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Loading_Stage1");
    }

    public void ResetButton()
    {
        ResetMenu.gameObject.SetActive(false);
    }

}
