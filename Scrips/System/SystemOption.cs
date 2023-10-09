using UnityEngine.UI;
using UnityEngine;

public class SystemOption : MonoBehaviour
{
    [Header("창")]
    public GameObject SystemOptionPrefab;

    [Header("탭")]
    public GameObject SoundTabPrefab;
    public GameObject BrightnessTabPrefab;
    public GameObject SoundTabClicked;
    public GameObject BrightnessTabClicked;
    private bool TabClick;
    private int TabNumber;

    [Header("버튼")]
    public GameObject ApplyButtonPrefab;
    private bool ButtonClick;

    [Header("밝기")]
    public Slider BrightnessSlider;
    public Image BrightnessColor;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;
    public AudioClip OKButtonAudio;

    private void Start()
    {
        BrightnessColor = GetComponent<Image>();
    }

    //밝기 조절
    public void BrightnessVolume()
    {
        float volume = BrightnessSlider.value;
        var tempColor = BrightnessColor.color;
        tempColor.a = BrightnessSlider.value;
    }

    //탭 선택
    public void TabSelect()
    {
        SoundTabClicked.SetActive(false);
        BrightnessTabClicked.SetActive(false);

        if (TabNumber == 1)
        {
            SoundTabPrefab.SetActive(true);
            BrightnessTabPrefab.SetActive(false);
            SoundTabClicked.SetActive(true);
        }
        else if (TabNumber == 2)
        {
            SoundTabPrefab.SetActive(false);
            BrightnessTabPrefab.SetActive(true);
            BrightnessTabClicked.SetActive(true);
        }
    }
    private void TabDown()
    {
        TabClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        if (TabNumber == 1)
            SoundTabClicked.SetActive(true);
        else if (TabNumber == 2)
            BrightnessTabClicked.SetActive(true);
    }
    public void SoundTabDown()
    {
        TabNumber = 1;
        TabDown();
    }
    public void BrightnessTabDown()
    {
        TabNumber = 2;
        TabDown();
    }
    public void TabUp()
    {
        if (TabClick == true)
        {
            if (TabNumber == 1)
                SoundTabClicked.SetActive(false);
            else if (TabNumber == 2)
                BrightnessTabClicked.SetActive(false);
        }
        TabClick = false;
    }
    public void TabEnter()
    {
        if (TabClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            if (TabNumber == 1)
                SoundTabClicked.SetActive(true);
            else if (TabNumber == 2)
                BrightnessTabClicked.SetActive(true);
        }
    }
    public void TabExit()
    {
        if (TabClick == true)
        {
            if (TabNumber == 1)
                SoundTabClicked.SetActive(false);
            else if (TabNumber == 2)
                BrightnessTabClicked.SetActive(false);
        }
    }

    //적용 버튼
    public void ApplyButtonClick()
    {
        SystemOptionPrefab.SetActive(false);
    }
    public void ApplyButtonDown()
    {
        ButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
        ApplyButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void ApplyButtonUp()
    {
        if (ButtonClick == true)
        {
            ApplyButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        ButtonClick = false;
    }
    public void ApplyButtonEnter()
    {
        if (ButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", OKButtonAudio);
            ApplyButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void ApplyButtonExit()
    {
        if (ButtonClick == true)
        {
            ApplyButtonPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //시스템 설정창 열기
    public void OpenSystemOption()
    {
        SystemOptionPrefab.SetActive(true);
        TabNumber = 1;
        TabSelect();
    }
}