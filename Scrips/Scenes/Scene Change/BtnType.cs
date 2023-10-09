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
                Debug.Log("새게임");
                break;

            case BTNType.Option:
                Debug.Log("옵션");
                break;

            case BTNType.Sound:
                // audioListener, audioSource와 같은 오디오 스크립트에서 소리 제어해주며 된다. 
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("사운드 OFF");
                }
                else
                {
                    isSound = true;
                    Debug.Log("사운드 ON");
                }
                break;


            case BTNType.Reset:
                ResetMenu.gameObject.SetActive(true);

                break;

            case BTNType.Back:
                Debug.Log("뒤로가기");
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
