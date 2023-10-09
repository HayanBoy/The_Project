using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextShipState : MonoBehaviour
{
    public Text text;
    public Text text2;
    public float PrintTime;
    public int State;
    private int RandomShip;
    private string ShipName;

    void Start()
    {
        if (State == 0)
        {
            RandomShip = Random.Range(0, 6);
            if (BattleSave.Save1.LanguageType == 1)
            {
                if (RandomShip == 0)
                    ShipName = "Ship : Anaconda phillibuster 777-20A";
                else if (RandomShip == 1)
                    ShipName = "Ship : Eillysseunos laicos 93-249F";
                else if (RandomShip == 2)
                    ShipName = "Ship : Raknori ulysses 220-4S";
                else if (RandomShip == 3)
                    ShipName = "Ship : Gold Slipi 39-234X";
                else if (RandomShip == 4)
                    ShipName = "Ship : Xenon haicross 1-304J";
                else if (RandomShip == 5)
                    ShipName = "Ship : Sakurapius Arugi 34-879Y";
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                if (RandomShip == 0)
                    ShipName = "함선 : 아나콘다 필리버스터 777-20A";
                else if (RandomShip == 1)
                    ShipName = "함선 : 엘리시우노스 라이코스 93-249F";
                else if (RandomShip == 2)
                    ShipName = "함선 : 라크노리 율리시스 220-4S";
                else if (RandomShip == 3)
                    ShipName = "함선 : 골드 슬리피 39-234X";
                else if (RandomShip == 4)
                    ShipName = "함선 : 제논 하이크로스 1-304J";
                else if (RandomShip == 5)
                    ShipName = "함선 : 사쿠라피우스 아루지 34-879Y";
            }
            StartCoroutine(TextPrintStart());
        }
        else if (State == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
                ShipName = "State : Supporting";
            else if (BattleSave.Save1.LanguageType == 2)
                ShipName = "상태 : 지원 중";
            StartCoroutine(TextPrintStart());
        }
        else if (State == 2)
        {
            if (BattleSave.Save1.LanguageType == 1)
                ShipName = "Ship warp..";
            else if (BattleSave.Save1.LanguageType == 2)
                ShipName = "함선 워프중..";
            StartCoroutine(TextPrintStart());
        }
    }

    IEnumerator TextPrintStart()
    {
        for (int i = 0; i <= ShipName.Length; i++)
        {
            text2.text = ShipName.Substring(0, i);
            text.text = ShipName.Substring(0, i);
            yield return new WaitForSeconds(PrintTime);
        }
    }
}