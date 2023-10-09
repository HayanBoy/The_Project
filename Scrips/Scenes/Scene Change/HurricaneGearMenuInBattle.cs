using UnityEngine.UI;
using UnityEngine;

public class HurricaneGearMenuInBattle : MonoBehaviour
{
    [Header("���� ���� â")]
    public GameObject ActiveWindowPrefab;

    [Header("�� ���� ����")]
    public Image MainWeaponSprite;
    public Text MainWeaponNameText;
    public Text MainWeaponExplainText;
    public GameObject NextButtonMainPrefab;
    public GameObject PreviousMainButtonPrefab;
    private bool ButtonClick = false;

    [Header("�� ���� ������")]
    public Sprite DT37Sprite;
    public Sprite DS65Sprite;
    public Sprite DP9007Sprite;
    public Sprite CGD27Sprite;

    [Header("��ȭ�� ����")]
    public Image HeavyWeaponSprite;
    public Text HeavyWeaponNameText;
    public Text HeavyWeaponExplainText;
    public GameObject NextHeavyWeaponButtonPrefab;
    public GameObject PreviousHeavyWeaponButtonPrefab;

    [Header("��ȭ�� ������")]
    public Sprite M3078Sprite;
    public Sprite ASC365Sprite;

    [Header("����")]
    public AudioClip ButtonUIAudio;

    private void Awake()
    {
        //�� ���� ��ȣ�� ����Ʈ�� �ø���
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

    //���� �� ����
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

    //���� �� ����
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

    //�� ���� ����
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
                MainWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.DT37Damage + "\n�߻�ӵ� : 0.083��" + "\nźâ�� ź�� �� : "
                    + DeltaHrricaneData.instance.DT37AmmoPerMagazine + "\nź�� �� : " + DeltaHrricaneData.instance.DT37AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 1000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DS65Sprite;
                MainWeaponNameText.text = string.Format("DS-65");
                MainWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.DS65Damage + "\n�߻�ӵ� : 0.73��" + "\nźâ�� ź�� �� : "
                    + DeltaHrricaneData.instance.DS65AmmoPerMagazine + "\nź�� �� : " + DeltaHrricaneData.instance.DS65AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 2000)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = DP9007Sprite;
                MainWeaponNameText.text = string.Format("DP-9007");
                MainWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.DP9007Damage + "\n�߻�ӵ� : 1.16��" + "\nźâ�� ź�� �� : "
                    + DeltaHrricaneData.instance.DP9007AmmoPerMagazine + "\nź�� �� : " + DeltaHrricaneData.instance.DP9007AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedMainWeaponNumber == 0)
            {
                MainWeaponSprite.GetComponent<Image>().sprite = CGD27Sprite;
                MainWeaponNameText.text = string.Format("CGD-27 �ʸ��ÿ�(��Ŵ��)");
                MainWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.CGD27PillishionDamage + "\n�߻�ӵ� : 0.083��" + "\nźâ�� ź�� �� : "
                    + DeltaHrricaneData.instance.CGD27AmmoPerMagazine + "\nź�� �� : " + DeltaHrricaneData.instance.CGD27AmmoAmount);
            }
        }
    }

    //���� ��ȭ��
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

    //���� ��ȭ��
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

    //��ȭ�� ����
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
                HeavyWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.M3078Damage + "\n�߻�ӵ� : 0.05��" + "\nź�� �� : "
                    + DeltaHrricaneData.instance.M3078AmmoAmount);
            }
            else if (DeltaHrricaneData.instance.SelectedHeavyWeaponNumber == 5001)
            {
                HeavyWeaponSprite.GetComponent<Image>().sprite = ASC365Sprite;
                HeavyWeaponNameText.text = string.Format("ASC 365 ȭ������");
                HeavyWeaponExplainText.text = string.Format("\n������ : " + UpgradeDataSystem.instance.ASC365Damage + "\nź�� �� : " + DeltaHrricaneData.instance.ASC365AmmoAmount);
            }
        }
    }
}