using UnityEngine.UI;
using UnityEngine;

public class HurricaneGearMenuInBattle : MonoBehaviour
{
    [Header("무기 선택 창")]
    public GameObject ActiveWindowPrefab;

    [Header("주 무기 선택")]
    public Image MainWeaponSprite;
    public Text MainWeaponNameText;
    public Text MainWeaponExplainText;
    public GameObject NextButtonMainPrefab;
    public GameObject PreviousMainButtonPrefab;
    private bool ButtonClick = false;

    [Header("주 무기 아이콘")]
    public Sprite DT37Sprite;
    public Sprite DS65Sprite;
    public Sprite DP9007Sprite;
    public Sprite CGD27Sprite;

    [Header("중화기 선택")]
    public Image HeavyWeaponSprite;
    public Text HeavyWeaponNameText;
    public Text HeavyWeaponExplainText;
    public GameObject NextHeavyWeaponButtonPrefab;
    public GameObject PreviousHeavyWeaponButtonPrefab;

    [Header("중화기 아이콘")]
    public Sprite M3078Sprite;
    public Sprite ASC365Sprite;

    [Header("사운드")]
    public AudioClip ButtonUIAudio;

    private void Awake()
    {
        //주 무기 번호를 리스트로 올리기
        DeltaHrricaneData.instance.MainWeaponList.Add(1); //DT-47
        if (WeaponUnlockManager.instance.DS65Unlock == true)
            DeltaHrricaneData.instance.MainWeaponList.Add(1000); //DS-65
        if (WeaponUnlockManager.instance.DP9007Unlock == true)
            DeltaHrricaneData.instance.MainWeaponList.Add(2000); //DP-9007
        if (WeaponUnlockManager.instance.CGD27PillishionUnlock == true)
            DeltaHrricaneData.instance.MainWeaponList.Add(0); //CGD-27

        if (WeaponUnlockManager.instance.M3078Unlock == true)
            DeltaHrricaneData.instance.HeavyWeaponList.Add(5000); //M3078
        if (WeaponUnlockManager.instance.ASC365Unlock == true)
            DeltaHrricaneData.instance.HeavyWeaponList.Add(5001); //ASC 365
    }

    private void Start()
    {
        MainWeaponSelect();
        if (WeaponUnlockManager.instance.PowerHeavyWeaponCountUnlock > 0)
        {
            DeltaHrricaneData.instance.SelectedHeavyWeaponNumber = DeltaHrricaneData.instance.HeavyWeaponList[0];
            HeavyWeaponSelect();
        }
    }

    //다음 주 무기
    public void NextMainWeaponClick()
    {
        if (DeltaHrricaneData.instance.MainWeaponType < DeltaHrricaneData.instance.MainWeaponList.Count - 1)
            DeltaHrricaneData.instance.MainWeaponType++;
        else
            DeltaHrricaneData.instance.MainWeaponType = 0;
        MainWeaponSelect();
    }

    public void NextMainWeaponDown()
    {
        ButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        NextButtonMainPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
    }
    public void NextMainWeaponUp()
    {
        if (ButtonClick == true)
        {
            NextButtonMainPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
        ButtonClick = false;
    }
    public void NextMainWeaponEnter()
    {
        if (ButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            NextButtonMainPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
        }
    }
    public void NextMainWeaponExit()
    {
        if (ButtonClick == true)
        {
            NextButtonMainPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
    }

    //이전 주 무기
    public void PreviousMainWeaponClick()
    {
        if (DeltaHrricaneData.instance.MainWeaponType <= DeltaHrricaneData.instance.MainWeaponList.Count - 1 && DeltaHrricaneData.instance.MainWeaponType > 0)
            DeltaHrricaneData.instance.MainWeaponType--;
        else if (DeltaHrricaneData.instance.MainWeaponType <= 0)
            DeltaHrricaneData.instance.MainWeaponType = DeltaHrricaneData.instance.MainWeaponList.Count - 1;
        MainWeaponSelect();
    }

    public void PreviousMainWeaponDown()
    {
        ButtonClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        PreviousMainButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
    }
    public void PreviousMainWeaponUp()
    {
        if (ButtonClick == true)
        {
            PreviousMainButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
        ButtonClick = false;
    }
    public void PreviousMainWeaponEnter()
    {
        if (ButtonClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            PreviousMainButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
        }
    }
    public void PreviousMainWeaponExit()
    {
        if (ButtonClick == true)
        {
            PreviousMainButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
    }

    //주 무기 선택
    void MainWeaponSelect()
    {
        DeltaHrricaneData.instance.SelectedMainWeaponNumber = DeltaHrricaneData.instance.MainWeaponList[DeltaHrricaneData.instance.MainWeaponType];

        if (BattleSave.Save1.LanguageType == 1)
        {
            if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DT37Sprite;
                MainWeaponNameText.text = string.Format("DT-37");
                MainWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.DT37Damage + "\nRate of fire : 0.083sec" + "\nNumber of ammo per magazine : "
                    + DeltaHrricaneData.instance.DT37AmmoPerMagazine  + "\nNumber of ammo : " + DeltaHrricaneData.instance.DT37AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DS65Sprite;
                MainWeaponNameText.text = string.Format("DS-65");
                MainWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.DS65Damage + "\nRate of fire : 0.73sec" + "\nNumber of ammo per magazine : "
                    + DeltaHrricaneData.instance.DS65AmmoPerMagazine + "\nNumber of ammo : " + DeltaHrricaneData.instance.DS65AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 2000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DP9007Sprite;
                MainWeaponNameText.text = string.Format("DP-9007");
                MainWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.DP9007Damage + "\nRate of fire : 1.16sec" + "\nNumber of ammo per magazine : "
                    + DeltaHrricaneData.instance.DP9007AmmoPerMagazine + "\nNumber of ammo : " + DeltaHrricaneData.instance.DP9007AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 0)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = CGD27Sprite;
                MainWeaponNameText.text = string.Format("CGD-27 Pillishion(Akimbo)");
                MainWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.CGD27PillishionDamage + "\nRate of fire : 0.083sec" + "\nNumber of ammo per magazine : "
                    + DeltaHrricaneData.instance.CGD27AmmoPerMagazine + "\nNumber of ammo : " + DeltaHrricaneData.instance.CGD27AmmoAmount);
            }
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DT37Sprite;
                MainWeaponNameText.text = string.Format("DT-37");
                MainWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.DT37Damage + "\n발사속도 : 0.083초" + "\n탄창당 탄약 수 : "
                    + DeltaHrricaneData.instance.DT37AmmoPerMagazine + "\n탄약 수 : " + DeltaHrricaneData.instance.DT37AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DS65Sprite;
                MainWeaponNameText.text = string.Format("DS-65");
                MainWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.DS65Damage + "\n발사속도 : 0.73초" + "\n탄창당 탄약 수 : "
                    + DeltaHrricaneData.instance.DS65AmmoPerMagazine + "\n탄약 수 : " + DeltaHrricaneData.instance.DS65AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 2000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DP9007Sprite;
                MainWeaponNameText.text = string.Format("DP-9007");
                MainWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.DP9007Damage + "\n발사속도 : 1.16초" + "\n탄창당 탄약 수 : "
                    + DeltaHrricaneData.instance.DP9007AmmoPerMagazine + "\n탄약 수 : " + DeltaHrricaneData.instance.DP9007AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 0)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = CGD27Sprite;
                MainWeaponNameText.text = string.Format("CGD-27 필리시온(아킴보)");
                MainWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.CGD27PillishionDamage + "\n발사속도 : 0.083초" + "\n탄창당 탄약 수 : "
                    + DeltaHrricaneData.instance.CGD27AmmoPerMagazine + "\n탄약 수 : " + DeltaHrricaneData.instance.CGD27AmmoAmount);
            }
        }
    }

    //다음 중화기
    public void NextHeavyWeaponClick()
    {
        if (DeltaHrricaneData.instance.HeavyWeaponType < DeltaHrricaneData.instance.HeavyWeaponList.Count - 1)
            DeltaHrricaneData.instance.HeavyWeaponType++;
        else
            DeltaHrricaneData.instance.HeavyWeaponType = 0;
        HeavyWeaponSelect();
    }

    public void NextHeavyWeaponDown()
    {
        ButtonClick = true;
        //SoundManager.instance.SFXPlay2("Sound", Beep5);
        NextHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
    }
    public void NextHeavyWeaponUp()
    {
        if (ButtonClick == true)
        {
            NextHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
        ButtonClick = false;
    }
    public void NextHeavyWeaponEnter()
    {
        if (ButtonClick == true)
        {
            //SoundManager.instance.SFXPlay2("Sound", Beep5);
            NextHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
        }
    }
    public void NextHeavyWeaponExit()
    {
        if (ButtonClick == true)
        {
            NextHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
    }

    //이전 중화기
    public void PreviousHeavyWeaponClick()
    {
        if (DeltaHrricaneData.instance.HeavyWeaponType <= DeltaHrricaneData.instance.HeavyWeaponList.Count - 1 && DeltaHrricaneData.instance.HeavyWeaponType > 0)
            DeltaHrricaneData.instance.HeavyWeaponType--;
        else if (DeltaHrricaneData.instance.HeavyWeaponType <= 0)
            DeltaHrricaneData.instance.HeavyWeaponType = DeltaHrricaneData.instance.HeavyWeaponList.Count - 1;
        HeavyWeaponSelect();
    }

    public void PreviousHeavyWeaponDown()
    {
        ButtonClick = true;
        //SoundManager.instance.SFXPlay2("Sound", Beep5);
        PreviousHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
    }
    public void PreviousHeavyWeaponUp()
    {
        if (ButtonClick == true)
        {
            PreviousHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
        ButtonClick = false;
    }
    public void PreviousHeavyWeaponEnter()
    {
        if (ButtonClick == true)
        {
            //SoundManager.instance.SFXPlay2("Sound", Beep5);
            PreviousHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", true);
        }
    }
    public void PreviousHeavyWeaponExit()
    {
        if (ButtonClick == true)
        {
            PreviousHeavyWeaponButtonPrefab.GetComponent<Animator>().SetBool("Previous fleet Click, Fleet menu", false);
        }
    }

    //중화기 선택
    void HeavyWeaponSelect()
    {
        DeltaHrricaneData.instance.SelectedHeavyWeaponNumber = DeltaHrricaneData.instance.HeavyWeaponList[DeltaHrricaneData.instance.HeavyWeaponType];

        if (BattleSave.Save1.LanguageType == 1)
        {
            if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5000)
            {
                HeavyWeaponSprite.GetComponent<Image>().sprite = M3078Sprite;
                HeavyWeaponNameText.text = string.Format("M3078");
                HeavyWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.M3078Damage + "\nRate of fire : 0.05sec" + "\nNumber of ammo : "
                    + DeltaHrricaneData.instance.M3078AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
            {
                HeavyWeaponSprite.GetComponent<Image>().sprite = ASC365Sprite;
                HeavyWeaponNameText.text = string.Format("ASC 365 Flamethrower");
                HeavyWeaponExplainText.text = string.Format("\nDamage : " + UpgradeDataSystem.instance.ASC365Damage + "\nNumber of ammo : " + DeltaHrricaneData.instance.ASC365AmmoAmount);
            }
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5000)
            {
                HeavyWeaponSprite.GetComponent<Image>().sprite = M3078Sprite;
                HeavyWeaponNameText.text = string.Format("M3078");
                HeavyWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.M3078Damage + "\n발사속도 : 0.05초" + "\n탄약 수 : "
                    + DeltaHrricaneData.instance.M3078AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
            {
                HeavyWeaponSprite.GetComponent<Image>().sprite = ASC365Sprite;
                HeavyWeaponNameText.text = string.Format("ASC 365 화염방사기");
                HeavyWeaponExplainText.text = string.Format("\n데미지 : " + UpgradeDataSystem.instance.ASC365Damage + "\n탄약 수 : " + DeltaHrricaneData.instance.ASC365AmmoAmount);
            }
        }
    }
}