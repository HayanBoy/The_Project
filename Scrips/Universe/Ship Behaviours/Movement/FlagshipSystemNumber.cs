using UnityEngine;

public class FlagshipSystemNumber : MonoBehaviour
{
    [Header("스크립트")]
    WordPrintSystem WordPrintSystem;
    AreaStatement AreaStatement;

    public int PlayerNumber; //해당 함대가 방문한 지역 번호
    public string playerAreaName; //해당 함대가 있는 지역 이름
    public int SystemNowNumber; //현재 기함이 어느 항성계에 있는지 보여주는 번호
    public int SystemDestinationNumber; //목적지 항성계 번호
    public int StatePlanet; //방문한 행성의 상태
    public int PlanetType; //행성 유형. 1 = 자원 행성, 2 = 연구 행성, 3 = 거주 행성
    private bool isVisit = false; //방문했는지에 대한 스위치

    private void Awake()
    {
        WordPrintSystem = FindObjectOfType<WordPrintSystem>();
        AreaStatement = FindObjectOfType<AreaStatement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //항성
            if (collision.CompareTag("Toropio"))
            {
                PlayerNumber = 1;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.ToropioStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Toropio";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "토로피오";
            }
            else if (collision.CompareTag("Roro I"))
            {
                PlayerNumber = 2;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.Roro1StarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Roro I";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "로로 I";
            }
            else if (collision.CompareTag("Roro II"))
            {
                PlayerNumber = 3;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.Roro2StarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Roro II";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "로로 II";
            }
            else if (collision.CompareTag("Sarisi"))
            {
                PlayerNumber = 4;
                SystemNowNumber = 3;
                StatePlanet = AreaStatement.SarisiStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Sarisi";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "사리시";
            }
            else if (collision.CompareTag("Garix"))
            {
                PlayerNumber = 5;
                SystemNowNumber = 4;
                StatePlanet = AreaStatement.GarixStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Garix";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "가릭스";
            }
            else if (collision.CompareTag("Secros"))
            {
                PlayerNumber = 6;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.SecrosStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Secros";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "세크로스";
            }
            else if (collision.CompareTag("Teretos"))
            {
                PlayerNumber = 7;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.TeretosStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Teretos";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "테레토스";
            }
            else if (collision.CompareTag("Mini popo"))
            {
                PlayerNumber = 8;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.MiniPopoStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Mini popo";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "미니 포포";
            }
            else if (collision.CompareTag("Delta D31-4A"))
            {
                PlayerNumber = 9;
                SystemNowNumber = 6;
                StatePlanet = AreaStatement.DeltaD31_4AStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Delta D31-4A";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "델타 D31-4A";
            }
            else if (collision.CompareTag("Delta D31-4B"))
            {
                PlayerNumber = 10;
                SystemNowNumber = 6;
                StatePlanet = AreaStatement.DeltaD31_4BStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Delta D31-4B";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "델타 D31-4B";
            }
            else if (collision.CompareTag("Jerato O95-7A"))
            {
                PlayerNumber = 11;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_7AStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-7A";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-7A";
            }
            else if (collision.CompareTag("Jerato O95-7B"))
            {
                PlayerNumber = 12;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_7BStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-7B";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-7B";
            }
            else if (collision.CompareTag("Jerato O95-14C"))
            {
                PlayerNumber = 13;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_14CStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-14C";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-14C";
            }
            else if (collision.CompareTag("Jerato O95-14D"))
            {
                PlayerNumber = 14;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_14DStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-14D";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-14D";
            }
            else if (collision.CompareTag("Jerato O95-Omega"))
            {
                PlayerNumber = 15;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_OmegaStarState;
                PlanetType = 0;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-Omega";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-오메가";
            }

            //행성
            else if (collision.CompareTag("Satarius Glessia"))
            {
                PlayerNumber = 1001;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.SatariusGlessiaState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Satarius Glessia";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "사타리우스 글래시아";
            }
            else if (collision.CompareTag("Aposis"))
            {
                PlayerNumber = 1002;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.AposisState;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Aposis";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "아포시스";
            }
            else if (collision.CompareTag("Torono"))
            {
                PlayerNumber = 1003;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.ToronoState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Torono";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "토로노";
            }
            else if (collision.CompareTag("Plopa II"))
            {
                PlayerNumber = 1004;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.Plopa2State;
                PlanetType = 3;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Plopa II";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "플로파 II";
            }
            else if (collision.CompareTag("Vedes VI"))
            {
                PlayerNumber = 1005;
                SystemNowNumber = 1;
                StatePlanet = AreaStatement.Vedes4State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Vedes VI";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "베데스 VI";
            }
            else if (collision.CompareTag("Aron Peri"))
            {
                PlayerNumber = 1006;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.AronPeriState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Aron Peri";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "아론 페리";
            }
            else if (collision.CompareTag("Papatus II"))
            {
                PlayerNumber = 1007;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.Papatus2State;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Papatus II";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "파파투스 II";
            }
            else if (collision.CompareTag("Papatus III"))
            {
                PlayerNumber = 1008;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.Papatus3State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Papatus III";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "파파투스 III";
            }
            else if (collision.CompareTag("Kyepotoros"))
            {
                PlayerNumber = 1009;
                SystemNowNumber = 2;
                StatePlanet = AreaStatement.KyepotorosState;
                PlanetType = 3;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Kyepotoros";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "키예포토로스";
            }
            else if (collision.CompareTag("Tratos"))
            {
                PlayerNumber = 1010;
                SystemNowNumber = 3;
                StatePlanet = AreaStatement.TratosState;
                PlanetType = 3;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Tratos";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "트라토스";
            }
            else if (collision.CompareTag("Oclasis"))
            {
                PlayerNumber = 1011;
                SystemNowNumber = 3;
                StatePlanet = AreaStatement.OclasisState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Oclasis";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "오클라시스";
            }
            else if (collision.CompareTag("Derious Heri"))
            {
                PlayerNumber = 1012;
                SystemNowNumber = 3;
                StatePlanet = AreaStatement.DeriousHeriState;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Derious Heri";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "데리우스 헤리";
            }
            else if (collision.CompareTag("Veltrorexy"))
            {
                PlayerNumber = 1013;
                SystemNowNumber = 4;
                StatePlanet = AreaStatement.VeltrorexyState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Veltrorexy";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "벨트로렉시";
            }
            else if (collision.CompareTag("Erix Jeoqeta"))
            {
                PlayerNumber = 1014;
                SystemNowNumber = 4;
                StatePlanet = AreaStatement.ErixJeoqetaState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Erix Jeoqeta";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "에릭스 제퀘타";
            }
            else if (collision.CompareTag("Qeepo"))
            {
                PlayerNumber = 1015;
                SystemNowNumber = 4;
                StatePlanet = AreaStatement.QeepoState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Qeepo";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "퀴이포";
            }
            else if (collision.CompareTag("Crown Yosere"))
            {
                PlayerNumber = 1016;
                SystemNowNumber = 4;
                StatePlanet = AreaStatement.CrownYosereState;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Crown Yosere";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "크라운 요세레";
            }
            else if (collision.CompareTag("Oros"))
            {
                PlayerNumber = 1017;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.OrosState;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Oros";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "오로스";
            }
            else if (collision.CompareTag("Japet Agrone"))
            {
                PlayerNumber = 1018;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.JapetAgroneState;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Japet Agrone";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "자펫 아그로네";
            }
            else if (collision.CompareTag("Xacro 042351"))
            {
                PlayerNumber = 1019;
                SystemNowNumber = 5;
                StatePlanet = AreaStatement.Xacro042351State;
                PlanetType = 1;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Xacro 042351";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "자크로 042351";
            }
            else if (collision.CompareTag("Delta D31-2208"))
            {
                PlayerNumber = 1020;
                SystemNowNumber = 6;
                StatePlanet = AreaStatement.DeltaD31_2208State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Delta D31-2208";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "델타 D31-2208";
            }
            else if (collision.CompareTag("Delta D31-9523"))
            {
                PlayerNumber = 1021;
                SystemNowNumber = 6;
                StatePlanet = AreaStatement.DeltaD31_9523State;
                PlanetType = 3;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Delta D31-9523";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "델타 D31-9523";
            }
            else if (collision.CompareTag("Delta D31-12721"))
            {
                PlayerNumber = 1022;
                SystemNowNumber = 6;
                StatePlanet = AreaStatement.DeltaD31_12721State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Delta D31-12721";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "델타 D31-12721";
            }
            else if (collision.CompareTag("Jerato O95-1125"))
            {
                PlayerNumber = 1023;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_1125State;
                PlanetType = 3;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-1125";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-1125";
            }
            else if (collision.CompareTag("Jerato O95-2252"))
            {
                PlayerNumber = 1024;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_2252State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-2252";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-2252";
            }
            else if (collision.CompareTag("Jerato O95-8510"))
            {
                PlayerNumber = 1025;
                SystemNowNumber = 7;
                StatePlanet = AreaStatement.JeratoO95_8510State;
                PlanetType = 2;

                if (WordPrintSystem.LanguageType == 1)
                    playerAreaName = "Jerato O95-8510";
                if (WordPrintSystem.LanguageType == 2)
                    playerAreaName = "제라토 O95-8510";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //항성
            if (collision.CompareTag("Toropio"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Roro I"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Roro II"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Sarisi"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Garix"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Secros"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Teretos"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Mini popo"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Delta D31-4A"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Delta D31-4B"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-7A"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-7B"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-14C"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-14D"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-Omega"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }

            //행성
            else if (collision.CompareTag("Satarius Glessia"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Aposis"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Torono"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Plopa II"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Vedes VI"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Aron Peri"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Papatus II"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Papatus III"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Kyepotoros"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Tratos"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Oclasis"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Derious Heri"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Veltrorexy"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Erix Jeoqeta"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Qeepo"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Crown Yosere"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Oros"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Japet Agrone"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Xacro 042351"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Delta D31-2208"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Delta D31-9523"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Delta D31-12721"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-1125"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-2252"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
            else if (collision.CompareTag("Jerato O95-8510"))
            {
                PlayerNumber = 0;
                StatePlanet = 0;
                PlanetType = 0;
            }
        }
    }
}