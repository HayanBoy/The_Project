using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MissionStartPrint : MonoBehaviour
{
    public GameObject StartEffects;
    public Text Name;
    public Text Description;
    private string NameLogPrint;
    private string DescriptionLogPrint;

    void Start()
    {
        if (BattleSave.Save1.MissionType == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Contros Area Advance Battle";
                DescriptionLogPrint = "Eliminate the all Contros enemies of this area.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "컨트로스 지역 진격전";
                DescriptionLogPrint = "해당 지역의 컨트로스 병력을 모두 사살하라.";
            }
        }
        else if (BattleSave.Save1.MissionType == 2)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Position Defence Battle";
                DescriptionLogPrint = "Defend from the Contros's attack until the line of defense building complete.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "진지 방어전";
                DescriptionLogPrint = "방어선 구축이 완료될 때까지 컨트로스의 공격을 방어하라.";
            }
        }
        else if (BattleSave.Save1.MissionType == 3)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Infection Removal Battle";
                DescriptionLogPrint = "Purify the all area of infecters.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "감염 제거전";
                DescriptionLogPrint = "감염자들의 지역을 모두 정화하라.";
            }
        }
        else if (BattleSave.Save1.MissionType == 4)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Infection Restraint Battle";
                DescriptionLogPrint = "Eliminate all rushing infecters to curb the spread of infection.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "감염 저지전";
                DescriptionLogPrint = "감염 확산을 억제하기 위해, 몰려오는 감염자를 모두 처치하라.";
            }
        }
        else if (BattleSave.Save1.MissionType == 100)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Slorius Flagship Infiltration Operation";
                DescriptionLogPrint = "Destroy the energy store system of Slorius flagship.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "슬로리어스 기함 침투적전";
                DescriptionLogPrint = "슬로리어스 기함의 에너지 저장 시스템을 무력화하라.";
            }
        }
        else if (BattleSave.Save1.MissionType == 101)
        {
            if (BattleSave.Save1.LanguageType == 1)
            {
                NameLogPrint = "Kantakri Flagship Infiltration Operation";
                DescriptionLogPrint = "Destroy the Prokrasist tower of Kantakri flagship.";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                NameLogPrint = "칸타크리 기함 침투적전";
                DescriptionLogPrint = "칸타크리 기함의 프로크래시스트 타워를 무력화하라.";
            }
        }

        StartCoroutine(StartPrint());
    }

    //미션 UI 출력 시작
    IEnumerator StartPrint()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetFloat("Start, Mission Start", 1);
        yield return new WaitForSeconds(0.83f);
        StartEffects.SetActive(true);
        StartCoroutine(MissionLogPrintStart1());
        StartCoroutine(MissionLogPrintStart2());
        yield return new WaitForSeconds(2.17f);
        GetComponent<Animator>().SetFloat("Start, Mission Start", 2);
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetFloat("Start, Mission Start", 0);
        StartEffects.SetActive(false);
    }

    //미션 로그 출력
    IEnumerator MissionLogPrintStart1()
    {
        for (int i = 0; i <= NameLogPrint.Length; i++)
        {
            yield return new WaitForSeconds(0.005f);
            Name.text = NameLogPrint.Substring(0, i);
        }
    }
    IEnumerator MissionLogPrintStart2()
    {
        for (int i = 0; i <= DescriptionLogPrint.Length; i++)
        {
            yield return new WaitForSeconds(0.005f);
            Description.text = DescriptionLogPrint.Substring(0, i);
        }
    }
}