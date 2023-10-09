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
                NameLogPrint = "��Ʈ�ν� ���� ������";
                DescriptionLogPrint = "�ش� ������ ��Ʈ�ν� ������ ��� ����϶�.";
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
                NameLogPrint = "���� �����";
                DescriptionLogPrint = "�� ������ �Ϸ�� ������ ��Ʈ�ν��� ������ ����϶�.";
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
                NameLogPrint = "���� ������";
                DescriptionLogPrint = "�����ڵ��� ������ ��� ��ȭ�϶�.";
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
                NameLogPrint = "���� ������";
                DescriptionLogPrint = "���� Ȯ���� �����ϱ� ����, �������� �����ڸ� ��� óġ�϶�.";
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
                NameLogPrint = "���θ�� ���� ħ������";
                DescriptionLogPrint = "���θ�� ������ ������ ���� �ý����� ����ȭ�϶�.";
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
                NameLogPrint = "ĭŸũ�� ���� ħ������";
                DescriptionLogPrint = "ĭŸũ�� ������ ����ũ���ý�Ʈ Ÿ���� ����ȭ�϶�.";
            }
        }

        StartCoroutine(StartPrint());
    }

    //�̼� UI ��� ����
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

    //�̼� �α� ���
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