using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MissionCompleteUI : MonoBehaviour
{
    public ClearLine ClearLine;
    BackToUniverse BackToUniverse;

    public bool isMissionFailed;
    public GameObject VictoryOutFrame;
    public GameObject VictoryInFrame;
    public GameObject VictoryOKButten;
    public Image VictoryOKButtenImage;
    private bool VictoryOKButtenClick = false;

    public Text MissionCompleteName;
    public Text MissionCompleteExplain;
    public Text MissionCompleteRewardExplain;
    public Text RewardText;

    //수락 버튼
    public void AcceptButtonClick()
    {
        if (isMissionFailed == false) //미션 성공 시 수송기 호출
        {
            StartCoroutine(MissionCompleteUIEnd());
            StartCoroutine(ShuttleCall());
        }
        else//미션 실패시 함대로 즉시 복귀
        {
            StartCoroutine(MissionFaillUIEnd());
        }
    }
    public void AcceptButtonDown()
    {
        VictoryOKButtenClick = true;
        //SoundManager.instance.SFXPlay2("Sound", Beep5);
        VictoryOKButten.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void AcceptButtonUp()
    {
        if (VictoryOKButtenClick == true)
        {
            VictoryOKButten.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        VictoryOKButtenClick = false;
    }
    public void AcceptButtonEnter()
    {
        if (VictoryOKButtenClick == true)
        {
            //SoundManager.instance.SFXPlay2("Sound", Beep5);
            VictoryOKButten.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void AcceptButtonExit()
    {
        if (VictoryOKButtenClick == true)
        {
            VictoryOKButten.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }

    //미션 성공시 나타나는 성공 창
    public IEnumerator MissionCompleteUIStart()
    {
        ScoreManager.instance.TotalDeltaHurricaneMission++;
        if (BattleSave.Save1.MissionType > 0 && BattleSave.Save1.MissionType < 5)
            ScoreManager.instance.TotalDeltaHurricanePlanetMission++;
        else if (BattleSave.Save1.MissionArea == 100 || BattleSave.Save1.MissionArea == 101)
            ScoreManager.instance.TotalDeltaHurricaneFlagshipMission++;

        if (BattleSave.Save1.MissionType == 1)
            ScoreManager.instance.TotalDeltaHurricanePlanetControsConquestMission++;
        else if (BattleSave.Save1.MissionType == 2)
            ScoreManager.instance.TotalDeltaHurricanePlanetControsDefenceMission++;
        else if (BattleSave.Save1.MissionType == 3)
            ScoreManager.instance.TotalDeltaHurricanePlanetZombieConquestMission++;
        else if (BattleSave.Save1.MissionType == 4)
            ScoreManager.instance.TotalDeltaHurricanePlanetZombieDefenceMission++;

        if (BattleSave.Save1.FirstStart == true)
        {
            BattleSave.Save1.PlanetTutorial = 2;
        }

        Movement Movement = GameObject.Find("Play Control/Player").GetComponent<Movement>();
        Movement.MissionComplete = true;

        //플레이어 UI를 모두 비활성화
        if (Movement.VehicleActive == true)
        {
            if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            {
                StartCoroutine(GetComponent<GameControlSystem>().DisappearVehicleUI());
                StartCoroutine(GetComponent<GameControlSystem>().DisappearVehicleController());
            }
        }
        else
        {
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerUI());
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerController());
            StartCoroutine(GetComponent<GameControlSystem>().AmmoHUDDeactive());
        }
        StartCoroutine(GetComponent<GameControlSystem>().HealthBarDeactive());
        StartCoroutine(GetComponent<GameControlSystem>().DisappearShipUI());
        yield return new WaitForSeconds(1.5f);

        VictoryOutFrame.SetActive(true);
        VictoryInFrame.SetActive(true);
        VictoryOKButtenImage.raycastTarget = false;
        VictoryInFrame.GetComponent<Animator>().SetFloat("Victory number, Victory In Frame", 1);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        yield return new WaitForSeconds(1.5f);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        RewardList();
        yield return new WaitForSeconds(0.16f);
        VictoryOKButtenImage.raycastTarget = true;
    }

    public IEnumerator MissionCompleteUIEnd()
    {
        VictoryOKButtenImage.raycastTarget = false;
        VictoryOutFrame.GetComponent<Animator>().SetBool("Close, Victory Out Frame", true);
        VictoryInFrame.GetComponent<Animator>().SetBool("Close, Victory In Frame", true);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        yield return new WaitForSeconds(0.58f);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        yield return new WaitForSeconds(1);
        VictoryOutFrame.SetActive(false);
        VictoryInFrame.SetActive(false);
        VictoryOutFrame.GetComponent<Animator>().SetBool("Close, Victory Out Frame", false);
        VictoryInFrame.GetComponent<Animator>().SetBool("Close, Victory In Frame", false);
    }

    //미션 실패시 나타나는 실패 창
    public IEnumerator MissionFaillUIStart()
    {
        ScoreManager.instance.TotalDeltaHurricaneFailedMission++;

        Movement Movement = GameObject.Find("Play Control/Player").GetComponent<Movement>();

        //플레이어 UI를 모두 비활성화
        if (Movement.VehicleActive == true)
        {
            if (WeaponUnlockManager.instance.VehicleCountUnlock > 0)
            {
                StartCoroutine(GetComponent<GameControlSystem>().DisappearVehicleUI());
                StartCoroutine(GetComponent<GameControlSystem>().DisappearVehicleController());
            }
        }
        else
        {
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerUI());
            StartCoroutine(GetComponent<GameControlSystem>().DisappearPlayerController());
            StartCoroutine(GetComponent<GameControlSystem>().AmmoHUDDeactive());
        }
        StartCoroutine(GetComponent<GameControlSystem>().HealthBarDeactive());
        StartCoroutine(GetComponent<GameControlSystem>().DisappearShipUI());
        yield return new WaitForSeconds(1.5f);

        VictoryInFrame.SetActive(true);
        VictoryOKButtenImage.raycastTarget = false;
        VictoryInFrame.GetComponent<Animator>().SetFloat("Victory number, Victory In Frame", 2);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        yield return new WaitForSeconds(1.08f);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        MissionFailText();
        yield return new WaitForSeconds(0.16f);
        VictoryOKButtenImage.raycastTarget = true;
    }
    public IEnumerator MissionFaillUIEnd()
    {
        BackToUniverse = FindObjectOfType<BackToUniverse>();
        VictoryOKButtenImage.raycastTarget = false;
        VictoryInFrame.GetComponent<Animator>().SetBool("Close, Victory In Frame", true);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", true);
        yield return new WaitForSeconds(0.416f);
        VictoryOKButten.GetComponent<Animator>().SetBool("Disappear, cancel menu button", false);
        StartCoroutine(BackToUniverse.Exit(2));
        yield return new WaitForSeconds(1);
        VictoryInFrame.SetActive(false);
        VictoryInFrame.GetComponent<Animator>().SetBool("Close, Victory In Frame", false);
    }

    //수송기 호출
    IEnumerator ShuttleCall()
    {
        yield return new WaitForSeconds(1);
        ClearLine.ShuttleArrival();
    }

    //보상 목록
    public void RewardList()
    {
        float GlopaorosAdd = 0;
        float GlopaorosLimit = 0;
        float ConstructionResourceAdd = 0;
        float ConstructionResourceLimit = 0;
        float TaritronicAdd = 0;

        //행성전 보상
        if (BattleSave.Save1.MissionArea >= 1 && BattleSave.Save1.MissionArea <= 3)
        {
            if (BattleSave.Save1.MissionLevel == 1)
            {
                GlopaorosAdd = Random.Range(1200, 1600);
                ConstructionResourceAdd = Random.Range(1700, 2000);
                TaritronicAdd = Random.Range(1800, 1950);
            }
            else if (BattleSave.Save1.MissionLevel == 2)
            {
                GlopaorosAdd = Random.Range(1800, 2300);
                ConstructionResourceAdd = Random.Range(2200, 2700);
                TaritronicAdd = Random.Range(2500, 2650);
            }
            else if (BattleSave.Save1.MissionLevel == 3)
            {
                GlopaorosAdd = Random.Range(2400, 2800);
                ConstructionResourceAdd = Random.Range(3200, 3700);
                TaritronicAdd = Random.Range(3300, 3650);
            }
        }

        //기함 침투전 무력화 보상
        if (BattleSave.Save1.MissionArea == 100 || BattleSave.Save1.MissionArea == 101)
        {
            if (BattleSave.Save1.MissionLevel == 1)
            {
                GlopaorosAdd = Random.Range(800, 1200);
                ConstructionResourceAdd = Random.Range(1000, 1400);
                TaritronicAdd = Random.Range(1200, 1800);
            }
            else if (BattleSave.Save1.MissionLevel == 2)
            {
                GlopaorosAdd = Random.Range(1600, 2100);
                ConstructionResourceAdd = Random.Range(2000, 2400);
                TaritronicAdd = Random.Range(2200, 2800);
            }
            else if (BattleSave.Save1.MissionLevel == 3)
            {
                GlopaorosAdd = Random.Range(2200, 2800);
                ConstructionResourceAdd = Random.Range(3000, 3400);
                TaritronicAdd = Random.Range(3200, 3900);
            }
        }

        if (BattleSave.Save1.NarihaUnionGlopaoros + GlopaorosAdd < BattleSave.Save1.NarihaUnionGlopaoroslimit) //한도 초과 없이 모두 획득
            BattleSave.Save1.NarihaUnionGlopaoros += GlopaorosAdd;
        else if (BattleSave.Save1.NarihaUnionGlopaoros + GlopaorosAdd > BattleSave.Save1.NarihaUnionGlopaoroslimit) //한도 초과로 인해 제한된 자산만 획득
        {
            GlopaorosLimit = BattleSave.Save1.NarihaUnionGlopaoroslimit - BattleSave.Save1.NarihaUnionGlopaoros;
            GlopaorosAdd = GlopaorosLimit;
            BattleSave.Save1.NarihaUnionGlopaoros += GlopaorosAdd;
        }
        else if (BattleSave.Save1.NarihaUnionGlopaoros + GlopaorosAdd == BattleSave.Save1.NarihaUnionGlopaoroslimit) //한도와 보유 자산이 서로 같을 경우, 하나도 얻지 못하고 한도 초과 처리
        {
            GlopaorosLimit = 1;
            GlopaorosAdd = 0;
        }
        if (BattleSave.Save1.ConstructionResource + ConstructionResourceAdd < BattleSave.Save1.ConstructionResourcelimit)
            BattleSave.Save1.ConstructionResource += ConstructionResourceAdd;
        else if (BattleSave.Save1.ConstructionResource + ConstructionResourceAdd > BattleSave.Save1.ConstructionResourcelimit)
        {
            ConstructionResourceLimit = BattleSave.Save1.ConstructionResourcelimit - BattleSave.Save1.ConstructionResource;
            ConstructionResourceAdd = ConstructionResourceLimit;
            BattleSave.Save1.ConstructionResource += ConstructionResourceAdd;
        }
        if (BattleSave.Save1.ConstructionResource + ConstructionResourceAdd == BattleSave.Save1.ConstructionResourcelimit)
        {
            ConstructionResourceLimit = 1;
            ConstructionResourceAdd = 0;
        }
        BattleSave.Save1.Taritronic += TaritronicAdd;

        if (BattleSave.Save1.LanguageType == 1)
        {
            MissionCompleteName.text = string.Format("Mission Complete");
            MissionCompleteExplain.text = string.Format("Mission has been conpleted and Delta Hurricane is ready to return to our fleet.");
            MissionCompleteRewardExplain.text = string.Format("Hurricane commander, We report reward items.");
            if (GlopaorosLimit == 0 && ConstructionResourceLimit == 0)
                RewardText.text = string.Format(GlopaorosAdd + " Glopa\n\n" + ConstructionResourceAdd + " CR\n\n" + TaritronicAdd + " Taritronic");
            else if (GlopaorosLimit > 0 && ConstructionResourceLimit == 0)
                RewardText.text = string.Format(GlopaorosAdd + " Glopa(Limit exceeded)\n\n" + ConstructionResourceAdd + " CR\n\n" + TaritronicAdd + " Taritronic");
            else if (GlopaorosLimit == 0 && ConstructionResourceLimit > 0)
                RewardText.text = string.Format(GlopaorosAdd + " Glopa\n\n" + ConstructionResourceAdd + " CR(Limit exceeded)\n\n" + TaritronicAdd + " Taritronic");
            else if (GlopaorosLimit > 0 && ConstructionResourceLimit > 0)
                RewardText.text = string.Format(GlopaorosAdd + " Glopa(Limit exceeded)\n\n" + ConstructionResourceAdd + " CR(Limit exceeded)\n\n" + TaritronicAdd + " Taritronic");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            MissionCompleteName.text = string.Format("임무 완료");
            MissionCompleteExplain.text = string.Format("임무가 완료되었으며, 델타 허리케인은 우리의 함대로 복귀할 준비가 되었습니다.");
            MissionCompleteRewardExplain.text = string.Format("허리케인 사령관님, 보상을 보고합니다.");
            if (GlopaorosLimit == 0 && ConstructionResourceLimit == 0)
                RewardText.text = string.Format(GlopaorosAdd + " 글로파\n\n" + ConstructionResourceAdd + " 건설 재료\n\n" + TaritronicAdd + " 타리트로닉");
            else if (GlopaorosLimit > 0 && ConstructionResourceLimit == 0)
                RewardText.text = string.Format(GlopaorosAdd + " 글로파(한도 초과)\n\n" + ConstructionResourceAdd + " 건설 재료\n\n" + TaritronicAdd + " 타리트로닉");
            else if (GlopaorosLimit == 0 && ConstructionResourceLimit > 0)
                RewardText.text = string.Format(GlopaorosAdd + " 글로파\n\n" + ConstructionResourceAdd + " 건설 재료(한도 초과)\n\n" + TaritronicAdd + " 타리트로닉");
            else if (GlopaorosLimit > 0 && ConstructionResourceLimit > 0)
                RewardText.text = string.Format(GlopaorosAdd + " 글로파(한도 초과)\n\n" + ConstructionResourceAdd + " CR(한도 초과)\n\n" + TaritronicAdd + " 타리트로닉");
        }
    }

    //미션 실패시 나타나는 텍스트
    void MissionFailText()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            MissionCompleteName.text = string.Format("Mission Failure");
            MissionCompleteExplain.text = string.Format("Delta Hurricane has been fatally injured during operation. He is returning to fleet's military medical facility immediately.");
            MissionCompleteRewardExplain.text = string.Format("");
            RewardText.text = string.Format("");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            MissionCompleteName.text = string.Format("임무 실패");
            MissionCompleteExplain.text = string.Format("임무 도중, 델타 허리케인이 치명상을 입었습니다. 허리케인은 즉시 함대의 군사 의료 시설로 복귀하고 있습니다.");
            MissionCompleteRewardExplain.text = string.Format("");
            RewardText.text = string.Format("");
        }
    }
}