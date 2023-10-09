using System.Collections;
using UnityEngine;

public class WallBootEffectManagement : MonoBehaviour
{
    public GameObject WallEffectUCCIS; //UCCIS메뉴 화면전환 애니메이션

    public GameObject MenuBoot;
    public GameObject DeltaBoot;
    public GameObject HurricaneBoot;

    void Start()
    {
        if (BattleSave.Save1.GroundBattleCount == 0)
        {
            MenuBoot.SetActive(true);
            DeltaBoot.SetActive(false);
            StartCoroutine(StartWallEffect1());
        }
        else if (BattleSave.Save1.GroundBattleCount > 0)
        {
            if (DeltaBoot != null)
            {
                MenuBoot.SetActive(false);
                DeltaBoot.SetActive(true);
                StartCoroutine(StartWallEffect2());
            }
            else if (HurricaneBoot != null)
            {
                StartCoroutine(StartWallEffect3());
            }
        }
    }

    IEnumerator StartWallEffect1()
    {
        MenuBoot.GetComponent<WallBackgroundMaterial>().Direction = 0;
        MenuBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        MenuBoot.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        MenuBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);

        yield return new WaitForSecondsRealtime(0.5f);
        WallEffectUCCIS.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    IEnumerator StartWallEffect2()
    {
        DeltaBoot.GetComponent<WallBackgroundMaterial>().Direction = 0;
        DeltaBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        DeltaBoot.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        DeltaBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);

        yield return new WaitForSecondsRealtime(0.5f);
        WallEffectUCCIS.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    IEnumerator StartWallEffect3()
    {
        HurricaneBoot.GetComponent<WallBackgroundMaterial>().Direction = 0;
        HurricaneBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = true;

        yield return new WaitForSecondsRealtime(0.05f);
        HurricaneBoot.GetComponent<WallBackgroundMaterial>().DissolveStart = true;
        HurricaneBoot.GetComponent<WallBackgroundMaterial>().DissolveSetting = false;
        WallEffectUCCIS.GetComponent<Animator>().SetFloat("Menu wall effect1, Menu wall", 1);
    }
}