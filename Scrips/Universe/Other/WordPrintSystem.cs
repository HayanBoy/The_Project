using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class WordPrintSystem : MonoBehaviour
{
    [Header("스크립트")]
    public UniverseMapSystem UniverseMapSystem;
    AreaStatement AreaStatement;

    [Header("출력번호")]
    public int PrintNumber; //출력번호. 번호에 따라 해당 구간에 맞는 문자를 출력한다.
    public int PrintTypeNumber; //출력번호. 유형 및 종류가 많은 영역에만 사용한다. (예 : 행성 및 항성)

    [Header("언어")]
    public int LanguageType; //언어. 1 = 영어, 2 = 한국어

    [Header("부팅창")]
    public Text UCCISText1;
    public Text UCCISText2;
    public Text UCCISText3;
    public Text UCCISLoadingText1;
    public Text UCCISLoadingText2;
    public Text UCCISLoadingText3;
    private string UCCISLoadingprint;

    [Header("우주맵 설명창")]
    public RectTransform ScrollVerticalSize; //우주맵 지역 선택시 설명창 스크롤 최대 길이 지정용
    public Text UniverseMapAreaName; //우주맵 지역 선택시, 설명창의 지역 이름
    public Text UniverseMapExplainUI; //우주맵 지역 선택시, 설명창 내용
    public Text UniverseMapAreaSelectText; //우주맵 중앙 하단의 선택 요청문
    public Text MapProgerssComplete; //함선 선택시 나타나는 좌표 텍스트 출력
    public Text Confirm; //승인 버튼
    public Text CancelText; //취소 버튼
    public Text WarnningText; //워프 불가 알림 메시지
    private string AreaName;

    [Header("워프 소요시간 텍스트")]
    public Text Player1WarpText; //플레이어 함대선택 직후에 나타나는 워프 소요 시간
    public Text Player2WarpText;
    public Text Player3WarpText;
    public Text Player4WarpText;
    public Text Player5WarpText;

    public Text WarpArriveText; //플레이어 함대의 워프 남은 시간
    public Text WarpArriveText2;
    public Text WarpDistance; //워프거리

    public Text Player1WarpDistanceCalculator; //워프 항해거리 계산용
    public Text Player2WarpDistanceCalculator;
    public Text Player3WarpDistanceCalculator;
    public Text Player4WarpDistanceCalculator;
    public Text Player5WarpDistanceCalculator;

    [Header("워프 실시간 로그")]
    Coroutine StartWarpLog;
    Coroutine warpLogPrintStart;
    public RectTransform WarpLogText; //워프 실시간 로그를 위로 올리기 위한 목적
    public Text WarpLog; //왼쪽 하단의 실시간 워프 로그
    private string WarpLogPrint; //왼쪽 하단의 실시간 워프 로그 출력
    float WarpLogStemp; //PlayerWarpArriveTime에 있는 StartWarpLog코루틴을 한 번만 실행하기 위한 목적
    public Text WarpEngineStateLog; //오른쪽 하단의 워프엔진 상태에 대한 실시간 로그

    [Header("플레이어 기함 이름")]
    public string Player1FlagshipName = "Player1";
    public string Player2FlagshipName = "Player2";
    public string Player3FlagshipName = "Player3";
    public string Player4FlagshipName = "Player4";
    public string Player5FlagshipName = "Player5";

    public Text Player1Flagship;
    public Text Player2Flagship;
    public Text Player3Flagship;
    public Text Player4Flagship;
    public Text Player5Flagship;

    [Header("항성 이름")]
    public Text Toropio;
    public Text Roro1;
    public Text Roro2;
    public Text Sarisi;
    public Text Garix;
    public Text Secros;
    public Text Teretos;
    public Text MiniPopo;
    public Text DeltaD31_4A;
    public Text DeltaD31_4B;
    public Text JeratoO95_7A;
    public Text JeratoO95_7B;
    public Text JeratoO95_14C;
    public Text JeratoO95_14D;
    public Text JeratoO95_Omega;

    [Header("행성 이름")]
    public Text SatariusGlessia;
    public Text Aposis;
    public Text Torono;
    public Text Plopa2;
    public Text Vedes4;
    public Text AronPeri;
    public Text Papatus2;
    public Text Papatus3;
    public Text Kyepotoros;
    public Text Tratos;
    public Text Oclasis;
    public Text DeriousHeri;
    public Text Veltrorexy;
    public Text ErixJeoqeta;
    public Text Qeepo;
    public Text CrownYosere;
    public Text Oros;
    public Text JapetAgrone;
    public Text Xacro042351;
    public Text DeltaD31_2208;
    public Text DeltaD31_9523;
    public Text DeltaD31_12721;
    public Text JeratoO95_1125;
    public Text JeratoO95_2252;
    public Text JeratoO95_8510;

    private void Start()
    {
        LanguageType = BattleSave.Save1.LanguageType;
        AreaStatement = FindObjectOfType<AreaStatement>();
        UniverseMapAreaNamePrint();
    }

    //플레이어 함선 이름 숨기기
    public void HidePlayerShipName()
    {
        Player1Flagship.enabled = false;
        if (UniverseMapSystem.WorldPlayer2 != null)
            Player2Flagship.enabled = false;
        if (UniverseMapSystem.WorldPlayer3 != null)
            Player3Flagship.enabled = false;
        if (UniverseMapSystem.WorldPlayer4 != null)
            Player4Flagship.enabled = false;
        if (UniverseMapSystem.WorldPlayer5 != null)
            Player5Flagship.enabled = false;
    }

    //지역 선택 완료시, 플레이어 함선 이름 나타내기
    public void PrintPlayerShipName()
    {
        Player1Flagship.enabled = true;
        if (UniverseMapSystem.WorldPlayer2 != null)
            Player2Flagship.enabled = true;
        if (UniverseMapSystem.WorldPlayer3 != null)
            Player3Flagship.enabled = true;
        if (UniverseMapSystem.WorldPlayer4 != null)
            Player4Flagship.enabled = true;
        if (UniverseMapSystem.WorldPlayer5 != null)
            Player5Flagship.enabled = true;
    }

    //지역 선택 완료시, 선택된 지역을 제외한 나머지 지역 이름 숨기기
    public void HideAreaName(int AreaNumber)
    {
        if (AreaNumber != 1)
            Toropio.enabled = false;
        if (AreaNumber != 2)
            Roro1.enabled = false;
        if (AreaNumber != 3)
            Roro2.enabled = false;
        if (AreaNumber != 4)
            Sarisi.enabled = false;
        if (AreaNumber != 5)
            Garix.enabled = false;
        if (AreaNumber != 6)
            Secros.enabled = false;
        if (AreaNumber != 7)
            Teretos.enabled = false;
        if (AreaNumber != 8)
            MiniPopo.enabled = false;
        if (AreaNumber != 9)
            DeltaD31_4A.enabled = false;
        if (AreaNumber != 10)
            DeltaD31_4B.enabled = false;
        if (AreaNumber != 11)
            JeratoO95_7A.enabled = false;
        if (AreaNumber != 12)
            JeratoO95_7B.enabled = false;
        if (AreaNumber != 13)
            JeratoO95_14C.enabled = false;
        if (AreaNumber != 14)
            JeratoO95_14D.enabled = false;
        if (AreaNumber != 15)
            JeratoO95_Omega.enabled = false;

        if (AreaNumber != 1001)
            SatariusGlessia.enabled = false;
        if (AreaNumber != 1002)
            Aposis.enabled = false;
        if (AreaNumber != 1003)
            Torono.enabled = false;
        if (AreaNumber != 1004)
            Plopa2.enabled = false;
        if (AreaNumber != 1005)
            Vedes4.enabled = false;
        if (AreaNumber != 1006)
            AronPeri.enabled = false;
        if (AreaNumber != 1007)
            Papatus2.enabled = false;
        if (AreaNumber != 1008)
            Papatus3.enabled = false;
        if (AreaNumber != 1009)
            Kyepotoros.enabled = false;
        if (AreaNumber != 1010)
            Tratos.enabled = false;
        if (AreaNumber != 1011)
            Oclasis.enabled = false;
        if (AreaNumber != 1012)
            DeriousHeri.enabled = false;
        if (AreaNumber != 1013)
            Veltrorexy.enabled = false;
        if (AreaNumber != 1014)
            ErixJeoqeta.enabled = false;
        if (AreaNumber != 1015)
            Qeepo.enabled = false;
        if (AreaNumber != 1016)
            CrownYosere.enabled = false;
        if (AreaNumber != 1017)
            Oros.enabled = false;
        if (AreaNumber != 1018)
            JapetAgrone.enabled = false;
        if (AreaNumber != 1019)
            Xacro042351.enabled = false;
        if (AreaNumber != 1020)
            DeltaD31_2208.enabled = false;
        if (AreaNumber != 1021)
            DeltaD31_9523.enabled = false;
        if (AreaNumber != 1022)
            DeltaD31_12721.enabled = false;
        if (AreaNumber != 1023)
            JeratoO95_1125.enabled = false;
        if (AreaNumber != 1024)
            JeratoO95_2252.enabled = false;
        if (AreaNumber != 1025)
            JeratoO95_8510.enabled = false;
    }

    //지역 이름 나타내기
    public void PrintAreaName()
    {
        Toropio.enabled = true;
        Roro1.enabled = true;
        Roro2.enabled = true;
        Sarisi.enabled = true;
        Garix.enabled = true;
        Secros.enabled = true;
        Teretos.enabled = true;
        MiniPopo.enabled = true;
        DeltaD31_4A.enabled = true;
        DeltaD31_4B.enabled = true;
        JeratoO95_7A.enabled = true;
        JeratoO95_7B.enabled = true;
        JeratoO95_14C.enabled = true;
        JeratoO95_14D.enabled = true;
        JeratoO95_Omega.enabled = true;

        SatariusGlessia.enabled = true;
        Aposis.enabled = true;
        Torono.enabled = true;
        Plopa2.enabled = true;
        Vedes4.enabled = true;
        AronPeri.enabled = true;
        Papatus2.enabled = true;
        Papatus3.enabled = true;
        Kyepotoros.enabled = true;
        Tratos.enabled = true;
        Oclasis.enabled = true;
        DeriousHeri.enabled = true;
        Veltrorexy.enabled = true;
        ErixJeoqeta.enabled = true;
        Qeepo.enabled = true;
        CrownYosere.enabled = true;
        Oros.enabled = true;
        JapetAgrone.enabled = true;
        Xacro042351.enabled = true;
        DeltaD31_2208.enabled = true;
        DeltaD31_9523.enabled = true;
        DeltaD31_12721.enabled = true;
        JeratoO95_1125.enabled = true;
        JeratoO95_2252.enabled = true;
        JeratoO95_8510.enabled = true;
    }

    void AreaNameTextPrintStart()
    {
        for (int i = 0; i <= AreaName.Length; i++)
            UniverseMapAreaSelectText.text = AreaName.Substring(0, i);
    }

    IEnumerator UCCISBootingTextPrintStart()
    {
        for (int i = 0; i <= UCCISLoadingprint.Length; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            //UCCISLoadingText1.text = UCCISLoadingprint.Substring(0, i);
        }
    }

    //부팅창 시작 출력
    public void UCCISBootingPrint(int number)
    {
        if (LanguageType == 1)
        {
            if (number == 1)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Tactical Map of Interstellar Fleet Loading...");
                UCCISLoadingText2.text = string.Format("Tactical Map of Interstellar Fleet Loading...");
                UCCISLoadingText3.text = string.Format("Tactical Map of Interstellar Fleet Loading...");
            }
            else if (number == 2)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Fleet Gear Tactical Menu Loading...");
                UCCISLoadingText2.text = string.Format("Fleet Gear Tactical Menu Loading...");
                UCCISLoadingText3.text = string.Format("Fleet Gear Tactical Menu Loading...");
            }
            else if (number == 3)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Fleet Formation Management Menu Loading...");
                UCCISLoadingText2.text = string.Format("Fleet Formation Management Menu Loading...");
                UCCISLoadingText3.text = string.Format("Fleet Formation Management Menu Loading...");
            }
            else if (number == 4)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Flagship Management Menu Loading...");
                UCCISLoadingText2.text = string.Format("Flagship Management Menu Loading...");
                UCCISLoadingText3.text = string.Format("Flagship Management Menu Loading...");
            }
            else if (number == 5)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Military Laboratory Menu Loading...");
                UCCISLoadingText2.text = string.Format("Military Laboratory Menu Loading...");
                UCCISLoadingText3.text = string.Format("Military Laboratory Menu Loading...");
            }
            else if (number == 100)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("Delta Strike Group");
                UCCISText2.text = string.Format("Delta Strike Group");
                UCCISText3.text = string.Format("Delta Strike Group");
                UCCISLoadingText1.text = string.Format("Preparing Delta Hurricane operation...");
                UCCISLoadingText2.text = string.Format("Preparing Delta Hurricane operation...");
                UCCISLoadingText3.text = string.Format("Preparing Delta Hurricane operation...");
            }
            else if (number == 1000)
            {
                UCCISText1.fontSize = 80;
                UCCISText2.fontSize = 80;
                UCCISText3.fontSize = 80;
                UCCISText1.text = string.Format("United Command Center for Interstellar Space");
                UCCISText2.text = string.Format("United Command Center for Interstellar Space");
                UCCISText3.text = string.Format("United Command Center for Interstellar Space");
                UCCISLoadingText1.text = string.Format("Connecting console device of command center...");
                UCCISLoadingText2.text = string.Format("Connecting console device of command center...");
                UCCISLoadingText3.text = string.Format("Connecting console device of command center...");
            }
        }
        else if (LanguageType == 2)
        {
            if (number == 1)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("성간함대 전술지도 불러오는 중...");
                UCCISLoadingText2.text = string.Format("성간함대 전술지도 불러오는 중...");
                UCCISLoadingText3.text = string.Format("성간함대 전술지도 불러오는 중...");
            }
            else if (number == 2)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("함대 장비 전술 메뉴 불러오는 중...");
                UCCISLoadingText2.text = string.Format("함대 장비 전술 메뉴 불러오는 중...");
                UCCISLoadingText3.text = string.Format("함대 장비 전술 메뉴 불러오는 중...");
            }
            else if (number == 3)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("함대 배열 관리 메뉴 불러오는 중...");
                UCCISLoadingText2.text = string.Format("함대 배열 관리 메뉴 불러오는 중...");
                UCCISLoadingText3.text = string.Format("함대 배열 관리 메뉴 불러오는 중...");
            }
            else if (number == 4)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("기함 관리 메뉴 불러오는 중...");
                UCCISLoadingText2.text = string.Format("기함 관리 메뉴 불러오는 중...");
                UCCISLoadingText3.text = string.Format("기함 관리 메뉴 불러오는 중...");
            }
            else if (number == 5)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("군사 연구 메뉴 불러오는 중...");
                UCCISLoadingText2.text = string.Format("군사 연구 메뉴 불러오는 중...");
                UCCISLoadingText3.text = string.Format("군사 연구 메뉴 불러오는 중...");
            }
            else if (number == 100)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("델타전단");
                UCCISText2.text = string.Format("델타전단");
                UCCISText3.text = string.Format("델타전단");
                UCCISLoadingText1.text = string.Format("델타 허리케인 작전 준비 중...");
                UCCISLoadingText2.text = string.Format("델타 허리케인 작전 준비 중...");
                UCCISLoadingText3.text = string.Format("델타 허리케인 작전 준비 중...");
            }
            else if (number == 1000)
            {
                UCCISText1.fontSize = 100;
                UCCISText2.fontSize = 100;
                UCCISText3.fontSize = 100;
                UCCISText1.text = string.Format("성간우주사령연합");
                UCCISText2.text = string.Format("성간우주사령연합");
                UCCISText3.text = string.Format("성간우주사령연합");
                UCCISLoadingText1.text = string.Format("사령부 콘솔 기기에 접속 중...");
                UCCISLoadingText2.text = string.Format("사령부 콘솔 기기에 접속 중...");
                UCCISLoadingText3.text = string.Format("사령부 콘솔 기기에 접속 중...");
            }
        }
    }

    //부팅창 종료 출력
    public void UCCISExitingPrint()
    {
        if (LanguageType == 1)
        {
            UCCISLoadingText1.text = string.Format("Exiting...");
            UCCISLoadingText2.text = string.Format("Exiting...");
            UCCISLoadingText3.text = string.Format("Exiting...");
        }
        else if (LanguageType == 2)
        {
            UCCISLoadingText1.text = string.Format("종료 중...");
            UCCISLoadingText2.text = string.Format("종료 중...");
            UCCISLoadingText3.text = string.Format("종료 중...");
        }
    }

    public void UniverseMapAreaNamePrint()
    {
        if (LanguageType == 1)
        {
            Toropio.text = string.Format("Toropio");
            Roro1.text = string.Format("Roro I");
            Roro2.text = string.Format("Roro II");
            Sarisi.text = string.Format("Sarisi");
            Garix.text = string.Format("Garix");
            Secros.text = string.Format("Secros");
            Teretos.text = string.Format("Teretos");
            MiniPopo.text = string.Format("Mini popo");
            DeltaD31_4A.text = string.Format("Delta D31-4A");
            DeltaD31_4B.text = string.Format("Delta D31-4B");
            JeratoO95_7A.text = string.Format("Jerato O95-7A");
            JeratoO95_7B.text = string.Format("Jerato O95-7B");
            JeratoO95_14C.text = string.Format("Jerato O95-14C");
            JeratoO95_14D.text = string.Format("Jerato O95-14D");
            JeratoO95_Omega.text = string.Format("Jerato O95-Omega");

            SatariusGlessia.text = string.Format("Satarius Glessia");
            Aposis.text = string.Format("Aposis");
            Torono.text = string.Format("Torono");
            Plopa2.text = string.Format("Plopa II");
            Vedes4.text = string.Format("Vedes VI");
            AronPeri.text = string.Format("Aron Peri");
            Papatus2.text = string.Format("Papatus II");
            Papatus3.text = string.Format("Papatus III");
            Kyepotoros.text = string.Format("Kyepotoros");
            Tratos.text = string.Format("Tratos");
            Oclasis.text = string.Format("Oclasis");
            DeriousHeri.text = string.Format("Derious Heri");
            Veltrorexy.text = string.Format("Veltrorexy");
            ErixJeoqeta.text = string.Format("Erix Jeoqeta");
            Qeepo.text = string.Format("Qeepo");
            CrownYosere.text = string.Format("Crown Yosere");
            Oros.text = string.Format("Oros");
            JapetAgrone.text = string.Format("Japet Agrone");
            Xacro042351.text = string.Format("Xacro 042351");
            DeltaD31_2208.text = string.Format("Delta D31-2208");
            DeltaD31_9523.text = string.Format("Delta D31-9523");
            DeltaD31_12721.text = string.Format("Delta D31-12721");
            JeratoO95_1125.text = string.Format("Jerato O95-1125");
            JeratoO95_2252.text = string.Format("Jerato O95-2252");
            JeratoO95_8510.text = string.Format("Jerato O95-8510");
        }
        else if (LanguageType == 2)
        {
            Toropio.text = string.Format("토로피오");
            Roro1.text = string.Format("로로 I");
            Roro2.text = string.Format("로로 II");
            Sarisi.text = string.Format("사리시");
            Garix.text = string.Format("가릭스");
            Secros.text = string.Format("세크로스");
            Teretos.text = string.Format("테레토스");
            MiniPopo.text = string.Format("미니 포포");
            DeltaD31_4A.text = string.Format("델타 D31-4A");
            DeltaD31_4B.text = string.Format("델타 D31-4B");
            JeratoO95_7A.text = string.Format("제라토 O95-7A");
            JeratoO95_7B.text = string.Format("제라토 O95-7B");
            JeratoO95_14C.text = string.Format("제라토 O95-14C");
            JeratoO95_14D.text = string.Format("제라토 O95-14D");
            JeratoO95_Omega.text = string.Format("제라토 O95-오메가");

            SatariusGlessia.text = string.Format("사타리우스 글래시아");
            Aposis.text = string.Format("아포시스");
            Torono.text = string.Format("토로노");
            Plopa2.text = string.Format("플로파 II");
            Vedes4.text = string.Format("베데스 VI");
            AronPeri.text = string.Format("아론 페리");
            Papatus2.text = string.Format("파파투스 II");
            Papatus3.text = string.Format("파파투스 III");
            Kyepotoros.text = string.Format("키예포토로스");
            Tratos.text = string.Format("트라토스");
            Oclasis.text = string.Format("오클라시스");
            DeriousHeri.text = string.Format("데리우스 헤리");
            Veltrorexy.text = string.Format("벨트로렉시");
            ErixJeoqeta.text = string.Format("에릭스 제퀘타");
            Qeepo.text = string.Format("퀴이포");
            CrownYosere.text = string.Format("크라운 요세레");
            Oros.text = string.Format("오로스");
            JapetAgrone.text = string.Format("자펫 아그로네");
            Xacro042351.text = string.Format("자크로 042351");
            DeltaD31_2208.text = string.Format("델타 D31-2208");
            DeltaD31_9523.text = string.Format("델타 D31-9523");
            DeltaD31_12721.text = string.Format("델타 D31-12721");
            JeratoO95_1125.text = string.Format("제라토 O95-1125");
            JeratoO95_2252.text = string.Format("제라토 O95-2252");
            JeratoO95_8510.text = string.Format("제라토 O95-8510");
        }
    }

    //우주맵 선택 요청문 출력
    public void PrintAreaSelectText()
    {
        if (LanguageType == 1)
        {
            if (PrintNumber == 1)
                AreaName = "Select Area";
            else if (PrintNumber == 2)
                AreaName = "Exiting...";
            else if (PrintNumber == 3)
                AreaName = "Select Flagship";
            else if (PrintNumber == 4)
                AreaName = "Transfering warp domain coordinate to selected fleet...";
            else if (PrintNumber == 5)
                AreaName = string.Format("Number of Selected Flagship : {0}", UniverseMapSystem.AccountOfShip);

            else if (PrintTypeNumber == 1)
                AreaName = "Selected Star : Toropio";
            else if (PrintTypeNumber == 2)
                AreaName = "Selected Star : Roro I";
            else if (PrintTypeNumber == 3)
                AreaName = "Selected Star : Roro II";
            else if (PrintTypeNumber == 4)
                AreaName = "Selected Star : Sarisi";
            else if (PrintTypeNumber == 5)
                AreaName = "Selected Star : Garix";
            else if (PrintTypeNumber == 6)
                AreaName = "Selected Star : Secros";
            else if (PrintTypeNumber == 7)
                AreaName = "Selected Star : Teretos";
            else if (PrintTypeNumber == 8)
                AreaName = "Selected Star : Mini popo";
            else if (PrintTypeNumber == 9)
                AreaName = "Selected Star : Delta D31-4A";
            else if (PrintTypeNumber == 10)
                AreaName = "Selected Star : Delta D31-4B";
            else if (PrintTypeNumber == 11)
                AreaName = "Selected Star : Jerato O95-7A";
            else if (PrintTypeNumber == 12)
                AreaName = "Selected Star : Jerato O95-7B";
            else if (PrintTypeNumber == 13)
                AreaName = "Selected Star : Jerato O95-14C";
            else if (PrintTypeNumber == 14)
                AreaName = "Selected Star : Jerato O95-14D";
            else if (PrintTypeNumber == 15)
                AreaName = "Selected Star : Jerato O95-Omega";

            else if (PrintTypeNumber == 1001)
                AreaName = "Selected Planet : Satarius Glessia";
            else if (PrintTypeNumber == 1002)
                AreaName = "Selected Planet : Aposis";
            else if (PrintTypeNumber == 1003)
                AreaName = "Selected Planet : Torono";
            else if (PrintTypeNumber == 1004)
                AreaName = "Selected Planet : Plopa II";
            else if (PrintTypeNumber == 1005)
                AreaName = "Selected Planet : Vedes VI";
            else if (PrintTypeNumber == 1006)
                AreaName = "Selected Planet : Aron Peri";
            else if (PrintTypeNumber == 1007)
                AreaName = "Selected Planet : Papatus II";
            else if (PrintTypeNumber == 1008)
                AreaName = "Selected Planet : Papatus III";
            else if (PrintTypeNumber == 1009)
                AreaName = "Selected Planet : Kyepotoros";
            else if (PrintTypeNumber == 1010)
                AreaName = "Selected Planet : Tratos";
            else if (PrintTypeNumber == 1011)
                AreaName = "Selected Planet : Oclasis";
            else if (PrintTypeNumber == 1012)
                AreaName = "Selected Planet : Derious Heri";
            else if (PrintTypeNumber == 1013)
                AreaName = "Selected Planet : Veltrorexy";
            else if (PrintTypeNumber == 1014)
                AreaName = "Selected Planet : Erix Jeoqeta";
            else if (PrintTypeNumber == 1015)
                AreaName = "Selected Planet : Qeepo";
            else if (PrintTypeNumber == 1016)
                AreaName = "Selected Planet : Crown Yosere";
            else if (PrintTypeNumber == 1017)
                AreaName = "Selected Planet : Oros";
            else if (PrintTypeNumber == 1018)
                AreaName = "Selected Planet : Japet Agrone";
            else if (PrintTypeNumber == 1019)
                AreaName = "Selected Planet : Xacro 042351";
            else if (PrintTypeNumber == 1020)
                AreaName = "Selected Planet : Delta D31-2208";
            else if (PrintTypeNumber == 1021)
                AreaName = "Selected Planet : Delta D31-9523";
            else if (PrintTypeNumber == 1022)
                AreaName = "Selected Planet : Delta D31-12721";
            else if (PrintTypeNumber == 1023)
                AreaName = "Selected Planet : Jerato O95-1125";
            else if (PrintTypeNumber == 1024)
                AreaName = "Selected Planet : Jerato O95-2252";
            else if (PrintTypeNumber == 1025)
                AreaName = "Selected Planet : Jerato O95-8510";
            else if (PrintTypeNumber >= 10000 && PrintTypeNumber <= 10024)
                AreaName = "Battle Area Selected";
        }
        else if (LanguageType == 2)
        {
            if (PrintNumber == 1)
                AreaName = "지역을 선택하십시오";
            else if (PrintNumber == 2)
                AreaName = "종료 중...";
            else if (PrintNumber == 3)
                AreaName = "기함을 선택하십시오";
            else if (PrintNumber == 4)
                AreaName = "선택된 함대에게 워프 도메인 좌표를 전송하는 중...";
            else if (PrintNumber == 5)
                AreaName = string.Format("선택된 기함 수 : {0}", UniverseMapSystem.AccountOfShip);

            else if (PrintTypeNumber == 1)
                AreaName = "선택된 항성 : 토로피오";
            else if (PrintTypeNumber == 2)
                AreaName = "선택된 항성 : 로로 I";
            else if (PrintTypeNumber == 3)
                AreaName = "선택된 항성 : 로로 II";
            else if (PrintTypeNumber == 4)
                AreaName = "선택된 항성 : 사리시";
            else if (PrintTypeNumber == 5)
                AreaName = "선택된 항성 : 가릭스";
            else if (PrintTypeNumber == 6)
                AreaName = "선택된 항성 : 세크로스";
            else if (PrintTypeNumber == 7)
                AreaName = "선택된 항성 : 테레토스";
            else if (PrintTypeNumber == 8)
                AreaName = "선택된 항성 : 미니 포포";
            else if (PrintTypeNumber == 9)
                AreaName = "선택된 항성 : 델타 D31-4A";
            else if (PrintTypeNumber == 10)
                AreaName = "선택된 항성 : 델타 D31-4B";
            else if (PrintTypeNumber == 11)
                AreaName = "선택된 항성 : 제라토 O95-7A";
            else if (PrintTypeNumber == 12)
                AreaName = "선택된 항성 : 제라토 O95-7B";
            else if (PrintTypeNumber == 13)
                AreaName = "선택된 항성 : 제라토 O95-14C";
            else if (PrintTypeNumber == 14)
                AreaName = "선택된 항성 : 제라토 O95-14D";
            else if (PrintTypeNumber == 15)
                AreaName = "선택된 항성 : 제라토 O95-오메가";

            else if (PrintTypeNumber == 1001)
                AreaName = "선택된 행성 : 사타리우스 글래시아";
            else if (PrintTypeNumber == 1002)
                AreaName = "선택된 행성 : 아포시스";
            else if (PrintTypeNumber == 1003)
                AreaName = "선택된 행성 : 토로노";
            else if (PrintTypeNumber == 1004)
                AreaName = "선택된 행성 : 플로파 II";
            else if (PrintTypeNumber == 1005)
                AreaName = "선택된 행성 : 베데스 VI";
            else if (PrintTypeNumber == 1006)
                AreaName = "선택된 행성 : 아론 페리";
            else if (PrintTypeNumber == 1007)
                AreaName = "선택된 행성 : 파파투스 II";
            else if (PrintTypeNumber == 1008)
                AreaName = "선택된 행성 : 파파투스 III";
            else if (PrintTypeNumber == 1009)
                AreaName = "선택된 행성 : 키예포토로스";
            else if (PrintTypeNumber == 1010)
                AreaName = "선택된 행성 : 트라토스";
            else if (PrintTypeNumber == 1011)
                AreaName = "선택된 행성 : 오클라시스";
            else if (PrintTypeNumber == 1012)
                AreaName = "선택된 행성 : 데리우스 헤리";
            else if (PrintTypeNumber == 1013)
                AreaName = "선택된 행성 : 벨트로렉시";
            else if (PrintTypeNumber == 1014)
                AreaName = "선택된 행성 : 에릭스 제퀘타";
            else if (PrintTypeNumber == 1015)
                AreaName = "선택된 행성 : 퀴이포";
            else if (PrintTypeNumber == 1016)
                AreaName = "선택된 행성 : 크라운 요세레";
            else if (PrintTypeNumber == 1017)
                AreaName = "선택된 행성 : 오로스";
            else if (PrintTypeNumber == 1018)
                AreaName = "선택된 행성 : 자펫 아그로네";
            else if (PrintTypeNumber == 1019)
                AreaName = "선택된 행성 : 자크로 042351";
            else if (PrintTypeNumber == 1020)
                AreaName = "선택된 행성 : 델타 D31-2208";
            else if (PrintTypeNumber == 1021)
                AreaName = "선택된 행성 : 델타 D31-9523";
            else if (PrintTypeNumber == 1022)
                AreaName = "선택된 행성 : 델타 D31-12721";
            else if (PrintTypeNumber == 1023)
                AreaName = "선택된 행성 : 제라토 O95-1125";
            else if (PrintTypeNumber == 1024)
                AreaName = "선택된 행성 : 제라토 O95-2252";
            else if (PrintTypeNumber == 1025)
                AreaName = "선택된 행성 : 제라토 O95-8510";
            else if (PrintTypeNumber >= 10000 && PrintTypeNumber <= 10024)
                AreaName = "전투 지역 선택됨";
        }

        AreaNameTextPrintStart();
    }

    //우주맵 승인출력창
    public void UniverseConfirmPrintText()
    {
        if (LanguageType == 1)
        {
            Confirm.fontSize = 20;
            if (PrintNumber == 1)
                Confirm.text = string.Format("Select");
            else if (PrintNumber == 2)
                Confirm.text = string.Format("Start Warp");
            else if (PrintNumber == 3)
                Confirm.text = string.Format("Selecting...");
        }
        else if (LanguageType == 2)
        {
            Confirm.fontSize = 25;
            if (PrintNumber == 1)
                Confirm.text = string.Format("선택");
            else if (PrintNumber == 2)
                Confirm.text = string.Format("워프 시작");
            else if (PrintNumber == 3)
                Confirm.text = string.Format("선택 중...");
        }
    }

    //우주맵 취소출력창
    public void UniverseMapCancelPrintText()
    {
        if (LanguageType == 1)
        {
            CancelText.fontSize = 20;
            if (PrintNumber == 1)
                CancelText.text = string.Format("Area Cancel");
            else if (PrintNumber == 2)
                CancelText.text = string.Format("Ship Cancel");
            else if (PrintNumber == 3)
                CancelText.text = string.Format("Ship initialize");
        }

        else if (LanguageType == 2)
        {
            CancelText.fontSize = 25;
            if (PrintNumber == 1)
                CancelText.text = string.Format("지역 취소");
            else if (PrintNumber == 2)
                CancelText.text = string.Format("함선 취소");
            else if (PrintNumber == 3)
                CancelText.text = string.Format("함선 초기화");
        }
    }

    //워프항해시간 출력
    public void PlayerWarpPrintText(float ArrivalTime, int PlayerNumber)
    {
        float Time = Mathf.FloorToInt(ArrivalTime);
        if (LanguageType == 1)
        {
            if (PrintNumber == 1)
            {
                if (Time >= 60)
                {
                    if (PlayerNumber == 1)
                        Player1WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F0}</color>min <color=#FBFF00>{1:F1}</color>sec", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                        Player2WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F0}</color>min <color=#FBFF00>{1:F1}</color>sec", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                        Player3WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F0}</color>min <color=#FBFF00>{1:F1}</color>sec", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                        Player4WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F0}</color>min <color=#FBFF00>{1:F1}</color>sec", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                        Player5WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F0}</color>min <color=#FBFF00>{1:F1}</color>sec", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                }
                else
                {
                    if (PlayerNumber == 1)
                        Player1WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F1}</color>sec", Time);
                    else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                        Player2WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F1}</color>sec", Time);
                    else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                        Player3WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F1}</color>sec", Time);
                    else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                        Player4WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F1}</color>sec", Time);
                    else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                        Player5WarpText.text = string.Format("Warp Voyage Time : <color=#FBFF00>{0:F1}</color>sec", Time);
                }
            }
        }
        else if (LanguageType == 2)
        {
            if (PrintNumber == 1)
            {
                if (Time >= 60)
                {
                    if (PlayerNumber == 1)
                        Player1WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F0}</color>분 <color=#FBFF00>{1:F1}</color>초", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                        Player2WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F0}</color>분 <color=#FBFF00>{1:F1}</color>초", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                        Player3WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F0}</color>분 <color=#FBFF00>{1:F1}</color>초", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                        Player4WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F0}</color>분 <color=#FBFF00>{1:F1}</color>초", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                    else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                        Player5WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F0}</color>분 <color=#FBFF00>{1:F1}</color>초", Mathf.FloorToInt(ArrivalTime) / 60, Time % 60);
                }
                else
                {
                    if (PlayerNumber == 1)
                        Player1WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F1}</color>초", Time);
                    else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                        Player2WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F1}</color>초", Time);
                    else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                        Player3WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F1}</color>초", Time);
                    else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                        Player4WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F1}</color>초", Time);
                    else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                        Player5WarpText.text = string.Format("워프항해시간 : <color=#FBFF00>{0:F1}</color>초", Time);
                }
            }
        }

        if (ArrivalTime == 0) //특정 함대를 선택해서 취소
        {
            if (PlayerNumber == 1)
                Player1WarpText.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                Player2WarpText.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                Player3WarpText.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                Player4WarpText.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                Player5WarpText.text = string.Format("");

            if (PlayerNumber == 1)
                Player1WarpDistanceCalculator.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                Player2WarpDistanceCalculator.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                Player3WarpDistanceCalculator.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                Player4WarpDistanceCalculator.text = string.Format("");
            else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                Player5WarpDistanceCalculator.text = string.Format("");
        }
        else if (ArrivalTime == -1) //취소에서 전 함대 선택
        {
            Player1WarpText.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer2 != null)
                Player2WarpText.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer3 != null)
                Player3WarpText.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer4 != null)
                Player4WarpText.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer5 != null)
                Player5WarpText.text = string.Format("");

            Player1WarpDistanceCalculator.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer2 != null)
                Player2WarpDistanceCalculator.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer3 != null)
                Player3WarpDistanceCalculator.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer4 != null)
                Player4WarpDistanceCalculator.text = string.Format("");
            if (UniverseMapSystem.WorldPlayer5 != null)
                Player5WarpDistanceCalculator.text = string.Format("");
        }
    }

    //우주맵에서의 항해거리 계산
    public void WarpDistanceCalculate(float distance, int DestinationNumber, int NowNumber, int PlayerNumber)
    {
        if (NowNumber == DestinationNumber)
        {
            float AstronomicalUnit = distance / 200;
            if (LanguageType == 1)
            {
                if (PlayerNumber == 1)
                    Player1WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                    Player2WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                    Player3WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                    Player4WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                    Player5WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
            }
            else if (LanguageType == 2)
            {
                if (PlayerNumber == 1)
                    Player1WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                    Player2WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                    Player3WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                    Player4WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
                else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                    Player5WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>AU", AstronomicalUnit);
            }
        }
        else
        {
            float LightYear = distance / 200;
            if (LanguageType == 1)
            {
                if (PlayerNumber == 1)
                    Player1WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>LY", LightYear);
                else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                    Player2WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>LY", LightYear);
                else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                    Player3WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>LY", LightYear);
                else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                    Player4WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>LY", LightYear);
                else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                    Player5WarpDistanceCalculator.text = string.Format("Distance : <color=#FBFF00>{0:F1}</color>LY", LightYear);
            }
            else if (LanguageType == 2)
            {
                if (PlayerNumber == 1)
                    Player1WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>광년", LightYear);
                else if(UniverseMapSystem.WorldPlayer2 != null && PlayerNumber == 2)
                    Player2WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>광년", LightYear);
                else if (UniverseMapSystem.WorldPlayer3 != null && PlayerNumber == 3)
                    Player3WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>광년", LightYear);
                else if (UniverseMapSystem.WorldPlayer4 != null && PlayerNumber == 4)
                    Player4WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>광년", LightYear);
                else if (UniverseMapSystem.WorldPlayer5 != null && PlayerNumber == 5)
                    Player5WarpDistanceCalculator.text = string.Format("거리 : <color=#FBFF00>{0:F1}</color>광년", LightYear);
            }
        }
    }

    //워프도착예정시간 출력
    public void PlayerWarpArriveTime(float ArrivalTime)
    {
        float Times = Mathf.FloorToInt(ArrivalTime);
        if (LanguageType == 1)
        {
            if (PrintNumber == 1)
            {
                if (Times >= 60)
                {
                    WarpArriveText.text = string.Format("Estimated Time of Arrival : {0:F0}min {1:F0}sec", Mathf.FloorToInt(ArrivalTime) / 60, Times % 60);
                    WarpArriveText2.text = string.Format("Estimated Time of Arrival : {0:F0}min {1:F0}sec", Mathf.FloorToInt(ArrivalTime) / 60, Times % 60);
                }
                else if (Times < 60 && Times >= 6)
                {
                    WarpArriveText.text = string.Format("Estimated Time of Arrival : {0:F0}sec", Times);
                    WarpArriveText2.text = string.Format("Estimated Time of Arrival : {0:F0}sec", Times);
                }
                else if(Times < 6 && Times >= 0.5f)
                {
                    if (WarpLogStemp == 0)
                    {
                        WarpLogStemp += Time.deltaTime;
                        if (StartWarpLog != null)
                        {
                            StopCoroutine(StartWarpLog);
                            WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, -875);
                        }
                        WarpEndLiveLog();
                    }
                    WarpArriveText.text = string.Format("Estimated Time of Arrival : <color=#FBFF00>{0:F0}</color>sec", Times);
                    WarpArriveText2.text = string.Format("Estimated Time of Arrival : <color=#FF0008>{0:F0}</color>sec", Times);
                }
                else if(Times <= 0.5f)
                {
                    WarpArriveText.text = string.Format("Stopping warp engine...");
                    WarpArriveText2.text = string.Format("Stopping warp engine...");
                }
            }
        }
        else if (LanguageType == 2)
        {
            if (PrintNumber == 1)
            {
                if (Times >= 60)
                {
                    WarpArriveText.text = string.Format("도착예정시간 : {0:F0}분 {1:F0}초", Mathf.FloorToInt(ArrivalTime) / 60, Times % 60);
                    WarpArriveText2.text = string.Format("도착예정시간 : {0:F0}분 {1:F0}초", Mathf.FloorToInt(ArrivalTime) / 60, Times % 60);
                }
                else if (Times < 60 && Times >= 6)
                {
                    WarpArriveText.text = string.Format("도착예정시간 : {0:F0}초", Times);
                    WarpArriveText2.text = string.Format("도착예정시간 : {0:F0}초", Times);
                }
                else if(Times < 6 && Times >= 0.5f)
                {
                    if (WarpLogStemp == 0)
                    {
                        WarpLogStemp += Time.deltaTime;
                        if (StartWarpLog != null)
                        {
                            StopCoroutine(StartWarpLog);
                            WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, -875);
                        }
                        WarpEndLiveLog();
                    }
                    WarpArriveText.text = string.Format("도착예정시간 : <color=#FBFF00>{0:F0}</color>초", Times);
                    WarpArriveText2.text = string.Format("도착예정시간 : <color=#FF0008>{0:F0}</color>초", Times);
                }
                else if(Times <= 0.5f)
                {
                    WarpArriveText.text = string.Format("워프엔진 정지 중...");
                    WarpArriveText2.text = string.Format("워프엔진 정지 중...");
                }
            }
        }

        if (ArrivalTime == 0)
        {
            WarpArriveText.text = string.Format("");
            WarpArriveText2.text = string.Format("");
            WarpDistance.enabled = false;
        }
    }

    //워프하는 동안의 남은거리 계산
    public void WarpDistanceShow(float distance, int DestinationNumber, int NowNumber)
    {
        WarpDistance.enabled = true;

        if (NowNumber == DestinationNumber)
        {
            if (LanguageType == 1)
            {
                float AstronomicalUnit = distance / 200;
                float Kilometer = AstronomicalUnit * 149597870.7f;

                if (AstronomicalUnit > 1)
                    WarpDistance.text = string.Format("Remaining distance : {0:F2}AU", AstronomicalUnit);
                else if (AstronomicalUnit <= 1)
                    WarpDistance.text = string.Format("Remaining distance : {0:F0}Km", Kilometer);
            }
            else if (LanguageType == 2)
            {
                float AstronomicalUnit = distance / 200;
                float Kilometer = AstronomicalUnit * 149597870.7f;

                if (AstronomicalUnit > 1)
                    WarpDistance.text = string.Format("남은 거리 : {0:F2}AU", AstronomicalUnit);
                else if (AstronomicalUnit <= 1)
                    WarpDistance.text = string.Format("남은 거리 : {0:F0}Km", Kilometer);
            }
        }
        else
        {
            if (LanguageType == 1)
            {
                float LightYear = distance / 200;
                float AstronomicalUnit = LightYear * 63241.07f;
                float Kilometer = LightYear * 9460730472580f;

                if (LightYear > 1)
                    WarpDistance.text = string.Format("Remaining distance : {0:F2}LY", LightYear);
                else if (LightYear <= 1)
                {
                    if (AstronomicalUnit > 1)
                        WarpDistance.text = string.Format("Remaining distance : {0:F2}AU", AstronomicalUnit);
                }
            }
            else if (LanguageType == 2)
            {
                float LightYear = distance / 200;
                float AstronomicalUnit = LightYear * 63241.07f;
                float Kilometer = LightYear * 9460730472580f;

                if (LightYear > 1)
                    WarpDistance.text = string.Format("남은 거리 : {0:F2}광년", LightYear);
                else if (LightYear <= 1)
                {
                    if (AstronomicalUnit > 1)
                        WarpDistance.text = string.Format("남은 거리 : {0:F2}AU", AstronomicalUnit);
                }
            }
        }
    }

    //우주맵 중앙 상단의 함선 선택 시 워프 준비 완료 출력창
    public void WarpReadyPrint(int AreaNumber, int WarpToPlayer)
    {
        if (LanguageType == 1)
        {
            if (AreaNumber == 1)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Toropio star has been completed");
            else if (AreaNumber == 2)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Roro I star has been completed");
            else if (AreaNumber == 3)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Roro II star has been completed");
            else if (AreaNumber == 4)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Sarisi star has been completed");
            else if (AreaNumber == 5)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Garix star has been completed");
            else if (AreaNumber == 6)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Secros star has been completed");
            else if (AreaNumber == 7)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Teretos star has been completed");
            else if (AreaNumber == 8)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Mini popo star has been completed");
            else if (AreaNumber == 9)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Delta D31-4A star has been completed");
            else if (AreaNumber == 10)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Delta D31-4B star has been completed");
            else if (AreaNumber == 11)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-7A star has been completed");
            else if (AreaNumber == 12)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-7B star has been completed");
            else if (AreaNumber == 13)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-14C star has been completed");
            else if (AreaNumber == 14)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-14D star has been completed");
            else if (AreaNumber == 15)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-Omega star has been completed");

            else if (AreaNumber == 1001)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Satarius Glessia planet has been completed");
            else if (AreaNumber == 1002)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Aposis planet has been completed");
            else if (AreaNumber == 1003)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Torono planet has been completed");
            else if (AreaNumber == 1004)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Plopa II planet has been completed");
            else if (AreaNumber == 1005)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Vedes VI planet has been completed");
            else if (AreaNumber == 1006)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Aron Peri planet has been completed");
            else if (AreaNumber == 1007)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Papatus II planet has been completed");
            else if (AreaNumber == 1008)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Papatus III planet has been completed");
            else if (AreaNumber == 1009)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Kyepotoros planet has been completed");
            else if (AreaNumber == 1010)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Tratos planet has been completed");
            else if (AreaNumber == 1011)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Oclasis planet has been completed");
            else if (AreaNumber == 1012)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Derious Heri planet has been completed");
            else if (AreaNumber == 1013)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Veltrorexy planet has been completed");
            else if (AreaNumber == 1014)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Erix Jeoqeta planet has been completed");
            else if (AreaNumber == 1015)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Qeepo planet has been completed");
            else if (AreaNumber == 1016)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Crown Yosere planet has been completed");
            else if (AreaNumber == 1017)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Oros planet has been completed");
            else if (AreaNumber == 1018)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Japet Agrone planet has been completed");
            else if (AreaNumber == 1019)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Xacro 042351 planet has been completed");
            else if (AreaNumber == 1020)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Delta D31-2208 planet has been completed");
            else if (AreaNumber == 1021)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Delta D31-9523 planet has been completed");
            else if (AreaNumber == 1022)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Delta D31-12721 planet has been completed");
            else if (AreaNumber == 1023)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-1125 planet has been completed");
            else if (AreaNumber == 1024)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-2252 planet has been completed");
            else if (AreaNumber == 1025)
                MapProgerssComplete.text = string.Format("Warp domain coordinate access of Jerato O95-8510 planet has been completed");
            else if (AreaNumber >= 10000 && AreaNumber <= 10024)
                MapProgerssComplete.text = string.Format("coordinate access of our force ship in combat has been completed");

            if (AreaNumber == 0)
            {
                if (WarpToPlayer == 0)
                    MapProgerssComplete.text = string.Format("");
                else if (WarpToPlayer > 0)
                    MapProgerssComplete.text = string.Format("coordinate access of flagship has been completed");
            }
        }
        else if (LanguageType == 2)
        {
            if (AreaNumber == 1)
                MapProgerssComplete.text = string.Format("토로피오 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 2)
                MapProgerssComplete.text = string.Format("로로 I 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 3)
                MapProgerssComplete.text = string.Format("로로 II 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 4)
                MapProgerssComplete.text = string.Format("사리시 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 5)
                MapProgerssComplete.text = string.Format("가릭스 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 6)
                MapProgerssComplete.text = string.Format("세크로스 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 7)
                MapProgerssComplete.text = string.Format("테레토스 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 8)
                MapProgerssComplete.text = string.Format("미니 포포 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 9)
                MapProgerssComplete.text = string.Format("델타 D31-4A 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 10)
                MapProgerssComplete.text = string.Format("델타 D31-4B 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 11)
                MapProgerssComplete.text = string.Format("제라토 O95-7A 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 12)
                MapProgerssComplete.text = string.Format("제라토 O95-7B 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 13)
                MapProgerssComplete.text = string.Format("제라토 O95-14C 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 14)
                MapProgerssComplete.text = string.Format("제라토 O95-14D 항성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 15)
                MapProgerssComplete.text = string.Format("제라토 O95-오메가 항성 워프 도메인 좌표 접속 완료");

            else if (AreaNumber == 1001)
                MapProgerssComplete.text = string.Format("사타리우스 글래시아 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1002)
                MapProgerssComplete.text = string.Format("아포시스 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1003)
                MapProgerssComplete.text = string.Format("토로노 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1004)
                MapProgerssComplete.text = string.Format("플로파 II 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1005)
                MapProgerssComplete.text = string.Format("베데스 VI 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1006)
                MapProgerssComplete.text = string.Format("아론 페리 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1007)
                MapProgerssComplete.text = string.Format("파파투스 II 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1008)
                MapProgerssComplete.text = string.Format("파파투스 III 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1009)
                MapProgerssComplete.text = string.Format("키예포토로스 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1010)
                MapProgerssComplete.text = string.Format("트라토스 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1011)
                MapProgerssComplete.text = string.Format("오클라시스 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1012)
                MapProgerssComplete.text = string.Format("데리우스 헤리 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1013)
                MapProgerssComplete.text = string.Format("벨트로렉시 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1014)
                MapProgerssComplete.text = string.Format("에릭스 제퀘타 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1015)
                MapProgerssComplete.text = string.Format("퀴이포 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1016)
                MapProgerssComplete.text = string.Format("크라운 요세레 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1017)
                MapProgerssComplete.text = string.Format("오로스 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1018)
                MapProgerssComplete.text = string.Format("자펫 아그로네 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1019)
                MapProgerssComplete.text = string.Format("자크로 042351 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1020)
                MapProgerssComplete.text = string.Format("델타 D31-2208 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1021)
                MapProgerssComplete.text = string.Format("델타 D31-9523 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1022)
                MapProgerssComplete.text = string.Format("델타 D31-12721 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1023)
                MapProgerssComplete.text = string.Format("제라토 O95-1125 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1024)
                MapProgerssComplete.text = string.Format("제라토 O95-2252 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber == 1024)
                MapProgerssComplete.text = string.Format("제라토 O95-8510 행성 워프 도메인 좌표 접속 완료");
            else if (AreaNumber >= 10000 && AreaNumber <= 10024)
                MapProgerssComplete.text = string.Format("교전 중인 아군 함선 좌표 접속 완료");

            if (AreaNumber == 0)
            {
                if (WarpToPlayer == 0)
                    MapProgerssComplete.text = string.Format("");
                else if (WarpToPlayer > 0)
                    MapProgerssComplete.text = string.Format("기함 좌표 접속 완료");
            }
        }
    }

    //워프 실시간 함대 로그 텍스트 출력
    IEnumerator WarpLogPrintStart()
    {
        if (LanguageType == 1)
        {
            for (int i = 0; i <= WarpLogPrint.Length; i++)
            {
                yield return new WaitForSeconds(0.001f);
                WarpLog.text = WarpLogPrint.Substring(0, i);
            }

            yield return new WaitForSeconds(3);

            for (int i = 0; i < 6; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 45);
                yield return new WaitForSeconds(0.15f);
            }
        }
        else if (LanguageType == 2)
        {
            for (int i = 0; i <= WarpLogPrint.Length; i++)
            {
                yield return new WaitForSeconds(0.005f);
                WarpLog.text = WarpLogPrint.Substring(0, i);
            }

            yield return new WaitForSeconds(2);

            for (int i = 0; i < 6; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 45);
                yield return new WaitForSeconds(0.15f);
            }
        }

        WarpLog.text = string.Format("");
        WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, -875);
    }
    
    //워프 실시간 함대 로그
    IEnumerator WarpStartLogPrintNext()
    {
        if (LanguageType == 1)
        {
            yield return new WaitForSeconds(2.5f);

            for (int i = 0; i < 14; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 15);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (LanguageType == 2)
        {
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < 14; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 15);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    IEnumerator WarpEndLogPrintNext()
    {
        if (LanguageType == 1)
        {
            yield return new WaitForSeconds(2.5f);

            for (int i = 0; i < 10; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 15);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (LanguageType == 2)
        {
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < 10; i++)
            {
                WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, WarpLogText.anchoredPosition.y + 15);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    //워프 불가 알림 메시지 출력
    public IEnumerator WarpWarnningPrint(int number)
    {
        if (LanguageType == 1)
        {
            if (number == 1)
            {
                WarnningText.text = string.Format("This fleet is in the warp space!");
            }
            else if (number == 2)
            {
                WarnningText.text = string.Format("This fleet is already at the destination!");
            }
            else if (number == 3)
            {
                WarnningText.text = string.Format("This fleet is already joining with the selected fleet!");
            }
        }
        else if (LanguageType == 2)
        {
            if (number == 1)
            {
                WarnningText.text = string.Format("이 함대는 현재 워프 중입니다!");
            }
            else if (number == 2)
            {
                WarnningText.text = string.Format("이 함대는 이미 목적지에 있습니다!");
            }
            else if (number == 3)
            {
                WarnningText.text = string.Format("이 함대는 이미 선택된 함대와 합류 중입니다!");
            }
        }
        yield return new WaitForSecondsRealtime(2);
        WarnningText.text = string.Format("");
    }

    //워프 실시하는 과정에서 왼쪽 하단의 워프 로그 출력
    public void WarpStartLiveLog(int AreaNumber)
    {
        WarpLog.text = string.Format("");
        WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, -875);
        WarpLogStemp = 0;
        string AreaName = AreaNumberForLog(AreaNumber);
        StartCoroutine(WarpEngineLog()); //워프 엔진 로그 출력

        if (LanguageType == 1)
        {
            string PrintCode1 = GenerateRandomCode(); //코드 제조기
            string PrintCode2 = GenerateRandomCode();
            WarpLogPrint = "//Accessing center of warp domain..\nAccess fleet : 플레이어1\nAccess code : " + PrintCode1 + "\n\n//Center of warp domain access is completed\nArea name " + AreaName
                + " Initiating the quantum encryption of warp code\nQuantum encryption is completed\nTransmitting the code to this fleet's ships...\ntransmission dispatch number : " + PrintCode2 + "\ntransmission dispatch to : " + Player1FlagshipName
                + "\n\n//Transmission warp domain code to this fleet's ships is completed\nNumber of waiting ships to warp : " + ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList.Count + "\nStarting transmission protocol encryption\nTransmitting warp command to this fleet's ships..."
                + "\n\n//Initiating fleet warp procedure\nFixation the warp coordinate of flagship is completed\nReady for warp procedure\nInitiating ejection of warp energy\nGenerating warp bubble...";
        }
        else if (LanguageType == 2)
        {
            string PrintCode1 = GenerateRandomCode(); //코드 제조기
            string PrintCode2 = GenerateRandomCode();
            WarpLogPrint = "//중앙 워프 도메인 접속중..\n접속 함대 : 플레이어1\n접속 코드 : " + PrintCode1 + "\n\n//중앙 워프 도메인 접속 완료\n지역명 " + AreaName
                + " 워프 코드 양자 암호화 실시\n양자 암호화 완료\n코드를 함대에게 전송 중...\n전송 발령 번호 : " + PrintCode2 + "\n전송 발령 대상 : " + Player1FlagshipName
                + "\n\n//워프 도메인 코드 함대 전송 완료\n워프 대기 함대 수 : " + ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList.Count + "\n전달 프로토콜 암호화 실시\n함대 워프 명령 전달 중..."
                + "\n\n//함대 워프 절차 시작\n기함 워프 좌표 고정 완료\n워프 절차 준비\n워프 에너지 방출 시작\n워프 버블 발생 중...";
        }

        StartWarpLog = StartCoroutine(WarpStartLogPrintNext());
        warpLogPrintStart = StartCoroutine(WarpLogPrintStart());
    }

    //워프 종료하는 과정에서 왼쪽 하단의 워프 로그 출력
    public void WarpEndLiveLog()
    {
        if (warpLogPrintStart != null)
            StopCoroutine(warpLogPrintStart);
        WarpLog.text = string.Format("");
        WarpLogText.anchoredPosition = new Vector2(WarpLogText.anchoredPosition.x, -875);
        string AreaName = AreaNumberForLog(0);

        if (LanguageType == 1)
        {
            string PrintCode1 = GenerateRandomCode(); //코드 제조기
            string PrintCode2 = GenerateRandomCode();
            WarpLogPrint = "//Transmitting the command of fleet warp stop\nStarting fleet warp deceleration\nStarting flagship warp deceleration\nReady to cancel the warp voyage mode... Initiating the cruise mode..."
                + "\n\n//Initiating the fleet cruise system\nStarting flagship battle protocol restart\nDomain access command was transmitted to this fleet's ships\nCommand transmission code : " + PrintCode1 + "\nThis fleet's ships is accessing the flagship battle domain..."
                + "\n\n//Initiating warp termination procedure\nFleet arriving...\nStart deletion the warp encryption code\nDispatch code : " + PrintCode2 + "\nExiting warp...";
        }
        else if (LanguageType == 2)
        {
            string PrintCode1 = GenerateRandomCode(); //코드 제조기
            string PrintCode2 = GenerateRandomCode();
            WarpLogPrint = "//함대 워프 정지 명령 전달\n함대 워프 속도 감속 시작\n기함 워프 속도 감속 시작\n워프 항행 모드 해제 준비... 항행 모드 시작 중..."
                + "\n\n//함대 항행 시스템 시작\n기함 전투 프로토콜 재가동 시작\n소속 함대들에게 도메인 접속 명령 전달 됨\n명령 전달 코드 : " + PrintCode1 + "\n소속 함대들이 기함 전투 도메인 접속 중..."
                + "\n\n//워프 종료 절차 시작\n함대 도착 중...\n워프 암호화 코드 삭제 실시\n발령 코드 : " + PrintCode2 + "\n워프 종료 중...";
        }
        StartCoroutine(WarpEndLogPrintNext());
        warpLogPrintStart = StartCoroutine(WarpLogPrintStart());
    }

    //워프 실시하는 과정에서 오른쪽 하단의 워프 로그 출력(멀티 기함 작업때 이걸 기함 프리팹에 따로 워프 로그 스크립트를 만들어서 거기다 변수 형태로 저장한다음, 해당 스크립트의 변수를 여기로 가져와서 출력하도록 해야 함)
    IEnumerator WarpEngineLog()
    {
        if (LanguageType == 1)
        {
            for (int i = 0; i < 50; i++)
            {
                WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...{0}%//", i * 2);
                yield return new WaitForSeconds(0.005f);
            }
            for (int i = 0; i < 50; i++)
            {
                WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...{0}%//", i * 2);
                yield return new WaitForSeconds(0.005f);
            }
            for (int i = 0; i < 100; i++)
            {
                WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...{0}%//", i);
                yield return new WaitForSeconds(0.005f);
            }

            while (true)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
                {
                    WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal.//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal..//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal...//");
                    yield return new WaitForSeconds(0.1f);
                }

                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    while (true)
                    {
                        WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal...\nInitiating fleet warp//");
                        yield return new WaitForSeconds(0.05f);
                        WarpEngineStateLog.text = string.Format("Injecting the warp engine fuel...100%//\nTransforming warp engine fuel to warp energy...100%//\nArranging the matrix of warp energy...100%//\nTransmitting the fleet's warp signal...\n");
                        yield return new WaitForSeconds(0.05f);

                        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                        {
                            StartCoroutine(WarpEngineStart());
                            break;
                        }
                    }
                }

                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    break;
                }
            }
        }
        else if (LanguageType == 2)
        {
            for (int i = 0; i < 50; i++)
            {
                WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...{0}%//", i * 2);
                yield return new WaitForSeconds(0.005f);
            }
            for (int i = 0; i < 50; i++)
            {
                WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...{0}%//", i * 2);
                yield return new WaitForSeconds(0.005f);
            }
            for (int i = 0; i < 100; i++)
            {
                WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...{0}%//", i);
                yield return new WaitForSeconds(0.005f);
            }

            while(true)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false)
                {
                    WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중.//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중..//");
                    yield return new WaitForSeconds(0.1f);
                    WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중...//");
                    yield return new WaitForSeconds(0.1f);
                }

                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<FollowShipManager>().ShipList[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    while (true)
                    {
                        WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중...\n함대 도약 중//");
                        yield return new WaitForSeconds(0.05f);
                        WarpEngineStateLog.text = string.Format("워프 엔진 연료 주입 중...100%//\n워프 엔진 연료를 에너지로 전환 중...100%//\n워프 에너지 메트릭스 정렬 중...100%//\n함대 워프 신호 전달 중...\n");
                        yield return new WaitForSeconds(0.05f);

                        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                        {
                            StartCoroutine(WarpEngineStart());
                            break;
                        }
                    }
                }

                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    break;
                }
            }
        }
    }

    IEnumerator WarpEngineStart()
    {
        for (int i = 0; i < 10; i++)
        {
            if (LanguageType == 1)
                WarpEngineStateLog.text = string.Format("\n\n\n\nFlagship warp engine start////");
            else if (LanguageType == 2)
                WarpEngineStateLog.text = string.Format("\n\n\n\n기함 워프 엔진 가동////");
            yield return new WaitForSeconds(0.05f);
            WarpEngineStateLog.text = string.Format("");
            yield return new WaitForSeconds(0.05f);
        }
        WarpEngineStateLog.text = string.Format("");
    }

    //우주맵 지역 설명창
    public void AreaExplainWindow(int AreaNumber)
    {
        UniverseMapSystem.ShowUI.SetActive(true);
        ScrollVerticalSize.anchoredPosition = new Vector2(-351, 0);

        if (LanguageType == 1) //영어
        {
            //항성
            if (AreaNumber == 1)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 580);
                UniverseMapAreaName.text = string.Format("Toropio");
                if (AreaStatement.ToropioStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Single Star system\nPlanets : Aposis, Satarius Glessia, Torono, Vedes VI, Plopa II\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n"); //Toropio Star is so active, and has the Satarius Glessia in this system that has most many populations. The rest planets is almost normal terrestrial planet, but the Vedes VI is very hot terrestrial planet that is nearest at the Toropio, and population which is still living has lass than 5millions. Toropio is due to have so much activity, this area is used as the test firing area of UCCIS Flagship's cannon.\n\n<color=#00E4FF>Reconnaissance Report of Centirocks Fleet : There is no possibility of invasion, due to having no Contros's any reconnaissance at the rest planet, excepted Satarius Glessia these days.</color>
                else if (AreaStatement.ToropioStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Single Star system\nPlanets : Aposis, Satarius Glessia, Torono, Vedes VI, Plopa II\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.ToropioStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Single Star system\nPlanets : Aposis, Satarius Glessia, Torono, Vedes VI, Plopa II\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 2)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 520);
                UniverseMapAreaName.text = string.Format("Roro I");
                if (AreaStatement.Roro1StarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Roro1StarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Roro1StarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship\n-Blocking the Contros reinforcement fleet");
            }
            else if (AreaNumber == 3)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 520);
                UniverseMapAreaName.text = string.Format("Roro II");
                if (AreaStatement.Roro2StarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Roro2StarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Roro2StarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar type : Binary star system\nPlanets : Aron Peri, Papatus II, Papatus III, Kyepotoros\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 4)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 650);
                UniverseMapAreaName.text = string.Format("Sarisi");
                if (AreaStatement.SarisiStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Tratos, Oclasis, Derious Heri\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#77FF2F>Stable</color>\n\n"); //The Sarisi turns out to be an old star than expected. Because it formed about 3.4billion years earlier than the neighbor star Toropio, Now Sarisi has been changed a difficult star for current planets. Planet Tratos's water has decreased about 78% over hundreds of thousands of years, Planet Oclasis's atmosphere has also disappeared about 89%. Agrorit and Datas were turned into Lava planets and their residences were withdrawn due to severe planet's crust activity.\n\n<color=#00E4FF>Reconnaissance Report of Centirocks Fleet : In this planet which created by rare mineral Taritronic that was discovered as vein at The Oclasis, Additional more lots of Taritronic veins have recently been discovered on this planet's crust. As Taritronic is uesd in the Chaitri enhancement materials of the Kantakri, We are keeping an eye on their reconnaissance.</color>
                else if (AreaStatement.SarisiStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Tratos, Oclasis, Derious Heri\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.SarisiStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Tratos, Oclasis, Derious Heri\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 5)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Garix");
                if (AreaStatement.GarixStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Veltrorexy, Erix Jeoqeta, Qeepo, Crown Yosere\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.GarixStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Veltrorexy, Erix Jeoqeta, Qeepo, Crown Yosere\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.GarixStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Single Star system\nPlanets : Veltrorexy, Erix Jeoqeta, Qeepo, Crown Yosere\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship\n-Blocking the Contros reinforcement fleet");
            }
            else if (AreaNumber == 6)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Secros");
                if (AreaStatement.SecrosStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.SecrosStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.SecrosStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Flagship");
            }
            else if (AreaNumber == 7)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Teretos");
                if (AreaStatement.TeretosStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.TeretosStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.TeretosStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 8)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Mini popo");
                if (AreaStatement.MiniPopoStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.MiniPopoStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.MiniPopoStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar type : Triple star system\nPlanets : Oros, Japet Agrone, Xacro 042351\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Blocking the Contros reinforcement fleet");
            }
            else if (AreaNumber == 9)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Delta D31-4A");
                if (AreaStatement.DeltaD31_4AStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeltaD31_4AStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeltaD31_4AStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Flagship\n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 10)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Delta D31-4B");
                if (AreaStatement.DeltaD31_4BStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeltaD31_4BStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeltaD31_4BStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Binary star system\nPlanets : Delta D31-2208, Delta D31-9523, Delta D31-9523\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Blocking the Contros reinforcement fleet");
            }
            else if (AreaNumber == 11)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Jerato O95-7A");
                if (AreaStatement.JeratoO95_7AStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_7AStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_7AStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Flagship");
            }
            else if (AreaNumber == 12)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Jerato O95-7B");
                if (AreaStatement.JeratoO95_7BStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_7BStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_7BStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 13)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Jerato O95-14C");
                if (AreaStatement.JeratoO95_14CStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_14CStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_14CStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Formation ship");
            }
            else if (AreaNumber == 14)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Jerato O95-14D");
                if (AreaStatement.JeratoO95_14DStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_14DStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_14DStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Decrease in number of the Contros Flagship");
            }
            else if (AreaNumber == 15)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 600);
                UniverseMapAreaName.text = string.Format("Jerato O95-Omega");
                if (AreaStatement.JeratoO95_OmegaStarState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_OmegaStarState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_OmegaStarState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar type : Five star system\nPlanets : Jerato O95-1125, Jerato O95-2252, Jerato O95-8510\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nThe large scale Contros invasion fleet is stationed on this star. If we regain this star, we can damage the Contros fleet stationed on the planets as follows : \n-Blocking the Contros reinforcement fleet");
            }

            //행성
            else if (AreaNumber == 1001)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Satarius Glessia");
                if (AreaStatement.SatariusGlessiaState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Jadra Class3 Commerce\nPopulation : 15.3Billion\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.SatariusGlessiaState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Jadra Class3 Commerce\nPopulation : 15.3Billion\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.SatariusGlessiaState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Jadra Class3 Commerce\nPopulation : 15.3Billion\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 12 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit accpet : 10000\nConstruction Resource limit accpet : 10000");
                else if (AreaStatement.SatariusGlessiaState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Jadra Class3 Commerce\nPopulation : 15.3Billion\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1002)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 470);
                UniverseMapAreaName.text = string.Format("Aposis");
                if (AreaStatement.AposisState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Keist Class11 Research\nPopulation : 4.3Billion 78Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.AposisState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Keist Class11 Research\nPopulation : 4.3Billion 78Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.AposisState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Keist Class11 Research\nPopulation : 4.3Billion 78Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nShip : Shield ship\nDelta Hurricane Weapon : Change heavy weapon(Hydra-56 Armor Piercing Discarding Sabot)\nDelta Hurricane Weapon : Sub gea(OSEH-302 Widow Hire guided missile)\n\nResearch : Logistics support class 1");
                else if (AreaStatement.AposisState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Keist Class11 Research\nPopulation : 4.3Billion 78Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1003)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Torono");
                if (AreaStatement.ToronoState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Tiroxy Class3 Lode\nPopulation : 2.8Billion 1.1Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.ToronoState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Tiroxy Class3 Lode\nPopulation : 2.8Billion 1.1Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.ToronoState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Tiroxy Class3 Lode\nPopulation : 2.8Billion 1.1Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 12 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.ToronoState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Tiroxy Class3 Lode\nPopulation : 2.8Billion 1.1Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1004)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Plopa II");
                if (AreaStatement.Plopa2State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Intorricty Class7 Residence\nPopulation : 2.37Billion 85.2Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Plopa2State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Intorricty Class7 Residence\nPopulation : 2.37Billion 85.2Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Plopa2State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Intorricty Class7 Residence\nPopulation : 2.37Billion 85.2Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nYou can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
                else if (AreaStatement.Plopa2State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Intorricty Class7 Residence\nPopulation : 2.37Billion 85.2Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1005)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 470);
                UniverseMapAreaName.text = string.Format("Vedes VI");
                if (AreaStatement.Vedes4State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Nakamara Class5 Research\nPopulation : 4.6Billion 53Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Vedes4State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Nakamara Class5 Research\nPopulation : 4.6Billion 53Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Vedes4State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Nakamara Class5 Research\nPopulation : 4.6Billion 53Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nDelta Hurricane Weapon : Change heavy weapon(MEAG railgun)\nDelta Hurricane Weapon : Main weapon(CGD-27 Pillishion Submachine gun)\nDelta Hurricane Weapon : Grenade(VM-5 AEG)\n\nResearch : Delta Hurricane Hit point class 1");
                else if (AreaStatement.Vedes4State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Toropio\nPlanet Type : Nakamara Class5 Research\nPopulation : 4.6Billion 53Million\nFactor of Safety : <color=#77FF2F>1.0</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1006)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Aron Peri");
                if (AreaStatement.AronPeriState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Commerce\nPopulation : 8.7Billion 77.8Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.AronPeriState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Commerce\nPopulation : 8.7Billion 77.8Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.AronPeriState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Commerce\nPopulation : 8.7Billion 77.8Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 14 Glopa per 4 seconds\nMax 1.2 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.AronPeriState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Commerce\nPopulation : 8.7Billion 77.8Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1007)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Papatus II");
                if (AreaStatement.Papatus2State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Hanapyron Class4 Lode\nPopulation :  4.1Billion\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Papatus2State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Hanapyron Class4 Lode\nPopulation : 4.1Billion\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Papatus2State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Hanapyron Class4 Lode\nPopulation : 4.1Billion\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 16 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.Papatus2State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Hanapyron Class4 Lode\nPopulation : 4.1Billion\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1008)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 520);
                UniverseMapAreaName.text = string.Format("Papatus III");
                if (AreaStatement.Papatus3State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Epido Class6 Research\nPopulation : 2.9Billion 89Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Papatus3State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Epido Class6 Research\nPopulation : 2.9Billion 89Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Papatus3State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Epido Class6 Research\nPopulation : 2.9Billion 89Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nShip weapon : Over jump\nFleet support slot : First slot\nDelta Hurricane Weapon : Change heavy weapon(Arthes L-775 Charge laser)\nDelta Hurricane Weapon : Main weapon(DP-9007 Sniper rifle)\nShip support : Heavy weapon(M3078 Mini gun)\n\nResearch : Sub gear type class 1\nResearch : Grenade type class 1");
                else if (AreaStatement.Papatus3State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Epido Class6 Research\nPopulation : 2.9Billion 89Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1009)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Kyepotoros");
                if (AreaStatement.KyepotorosState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Residence\nPopulation : 7.2Billion 7.3Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.KyepotorosState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Residence\nPopulation : 7.2Billion 7.3Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.KyepotorosState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Residence\nPopulation : 7.2Billion 7.3Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#FF322F>Occupied</color>\n\nYou can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
                else if (AreaStatement.KyepotorosState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Glro\nStar : Roro I, Roro II\nPlanet Type : Intorricty Class7 Residence\nPopulation : 7.2Billion 7.3Million\nFactor of Safety : <color=#77FF2F>0.9</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1010)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Tratos");
                if (AreaStatement.TratosState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Epido Class6 Residence\nPopulation : 10.8Billion\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.TratosState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Epido Class6 Residence\nPopulation : 10.8Billion\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.TratosState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Epido Class6 Residence\nPopulation : 10.8Billion\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FF322F>Occupied</color>\n\nYou can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
                else if (AreaStatement.TratosState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Epido Class6 Residence\nPopulation : 10.8Billion\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1011)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Oclasis");
                if (AreaStatement.OclasisState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Tiroxy Class8 Lode\nPopulation : 7.9Billion 65Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.OclasisState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Tiroxy Class8 Lode\nPopulation : 7.9Billion 65Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.OclasisState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Tiroxy Class8 Lode\nPopulation : 7.9Billion 65Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 18 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.OclasisState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Tiroxy Class8 Lode\nPopulation : 7.9Billion 65Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1012)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 850);
                UniverseMapAreaName.text = string.Format("Derious Heri");
                if (AreaStatement.DeriousHeriState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Intorricty Class7 Research\nPopulation : 2.7Billion 77.8Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeriousHeriState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Intorricty Class7 Research\nPopulation : 2.7Billion 77.8Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeriousHeriState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Intorricty Class7 Research\nPopulation : 2.7Billion 77.8Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nShip weapon : Delta Needle-42 Halist multi missile\nDelta Hurricane Weapon : Change heavy weapon(UGG 98 Gravity cannon)\nDelta Hurricane Weapon : Main weapon(DS-65 Shotgun)\nShip support : Bombardment(PGM 1036 Scalet Hawk cruise missile)\n\nResearch : Flagship armor system class 1\nResearch : Formation ship armor system class 1\nResearch : Tactical ship armor system class 1\nResearch : Cannon type class 1\nResearch : Missile type class 1\nResearch : Carrier-based aircraft type class 1\nResearch : Change heavy weapon class 1\nResearch : Logistics support class 2\nResearch : Bombardment support class 1");
                else if (AreaStatement.DeriousHeriState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Sarisi\nPlanet Type : Intorricty Class7 Research\nPopulation : 2.7Billion 77.8Million\nFactor of Safety : <color=#FFA42F>0.6</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1013)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Veltrorexy");
                if (AreaStatement.VeltrorexyState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Commerce\nPopulation : 6.1Billion\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.VeltrorexyState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Commerce\nPopulation : 6.1Billion\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.VeltrorexyState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Commerce\nPopulation : 6.1Billion\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 20 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.VeltrorexyState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Commerce\nPopulation : 6.1Billion\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1014)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Erix Jeoqeta");
                if (AreaStatement.ErixJeoqetaState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Kalro Class2 Commerce\nPopulation : 27.3Billion 28Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.ErixJeoqetaState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Kalro Class2 Commerce\nPopulation : 27.3Billion 28Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.ErixJeoqetaState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Kalro Class2 Commerce\nPopulation : 27.3Billion 28Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 22 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.ErixJeoqetaState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Kalro Class2 Commerce\nPopulation : 27.3Billion 28Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1015)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Qeepo");
                if (AreaStatement.QeepoState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Lode\nPopulation : 3.8Billion 95Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.QeepoState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Lode\nPopulation : 3.8Billion 95Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.QeepoState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Lode\nPopulation : 3.8Billion 95Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 24 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.QeepoState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Nakamara Class5 Lode\nPopulation : 3.8Billion 95Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1016)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 580);
                UniverseMapAreaName.text = string.Format("Crown Yosere");
                if (AreaStatement.CrownYosereState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Jadra Class3 Research\nPopulation : 1.7Billion 0.2Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.CrownYosereState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Jadra Class3 Research\nPopulation : 1.7Billion 0.2Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.CrownYosereState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Jadra Class3 Research\nPopulation : 1.7Billion 0.2Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nShip : Carrier\nShip support : Heavy weapon(ASC 365 flamethrower)\n\nResearch : Hurricane Hit point class 2\nResearch : Assault rifle type class 1\nResearch : Shotgun type class 1\nResearch : Sniper rifle type class 1\nResearch : Submachine gun type class 1\nResearch : Grenade type class 2\nResearch : Heavy weapon support class 1");
                else if (AreaStatement.CrownYosereState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : Garix\nPlanet Type : Jadra Class3 Research\nPopulation : 1.7Billion 0.2Million\nFactor of Safety : <color=#FFA42F>0.4</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1017)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Oros");
                if (AreaStatement.OrosState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Hanapyron Class4 Commerce\nPopulation : 13.5Billion\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.OrosState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Hanapyron Class4 Commerce\nPopulation : 13.5Billion\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.OrosState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Hanapyron Class4 Commerce\nPopulation : 13.5Billion\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 26 Glopa per 5 seconds\nMax 4 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.OrosState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Hanapyron Class4 Commerce\nPopulation : 13.5Billion\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1018)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 500);
                UniverseMapAreaName.text = string.Format("Japet Agrone");
                if (AreaStatement.JapetAgroneState == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Epido Class6 Research\nPopulation : 10.6Billion 28.6Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JapetAgroneState == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Epido Class6 Research\nPopulation : 10.6Billion 28.6Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JapetAgroneState == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Epido Class6 Research\nPopulation : 10.6Billion 28.6Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nFleet support slot : Second slot\nShip support : Vehicle(MBCA-79 Iron Hurricane)\n\nResearch : Flagship strike class 1\nResearch : Fleet strike class 1\nResearch : Sub gear type class 2\nResearch : Vehicle support class 1\nResearch : Bombardment support class 2");
                else if (AreaStatement.JapetAgroneState == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Epido Class6 Research\nPopulation : 10.6Billion 28.6Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1019)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Xacro 042351");
                if (AreaStatement.Xacro042351State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Araoros Class1 Lode\nPopulation : 5.4Billion 32Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.Xacro042351State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Araoros Class1 Lode\nPopulation : 5.4Billion 32Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.Xacro042351State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Araoros Class1 Lode\nPopulation : 5.4Billion 32Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing resources will be gained if you liberate this planet. \n\nMax 4 Glopa per 5 seconds\nMax 28 Construction Resource per 5 seconds\n\nGlopaoros limit addition : 2000\nConstruction Resource limit addition : 2000");
                else if (AreaStatement.Xacro042351State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Taros\nStar : OctoKrasis Patoro System\nPlanet Type : Araoros Class1 Lode\nPopulation : 5.4Billion 32Million\nFactor of Safety : <color=#FFA42F>0.1</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1020)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 580);
                UniverseMapAreaName.text = string.Format("Delta D31-2208");
                if (AreaStatement.DeltaD31_2208State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 7.9Billion 12.3Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeltaD31_2208State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 7.9Billion 12.3Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeltaD31_2208State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 7.9Billion 12.3Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nResearch : Flagship armor system class 2\nResearch : Formation ship armor system class 2\nResearch : Tactical ship armor system class 2\nResearch : Cannon type class 2\nResearch : Missile type class 2\nResearch : Carrier-based aircraft type class 2\nResearch : Grenade type class 3\nResearch : Change heavy weapon class 2\nResearch : Heavy weapon support class 2");
                else if (AreaStatement.DeltaD31_2208State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 7.9Billion 12.3Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1021)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Delta D31-9523");
                if (AreaStatement.DeltaD31_9523State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Jadra Class3 Military Base\nPopulation : 12.8Billion\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeltaD31_9523State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Jadra Class3 Military Base\nPopulation : 12.8Billion\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeltaD31_9523State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Jadra Class3 Military Base\nPopulation : 12.8Billion\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FF322F>Occupied</color>\n\nYou can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
                else if (AreaStatement.DeltaD31_9523State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Jadra Class3 Military Base\nPopulation : 12.8Billion\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1022)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 540);
                UniverseMapAreaName.text = string.Format("Delta D31-12721");
                if (AreaStatement.DeltaD31_12721State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Araoros Class1 Military Research\nPopulation : 9.3Billion 8.9Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.DeltaD31_12721State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Araoros Class1 Military Research\nPopulation : 9.3Billion 8.9Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.DeltaD31_12721State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Araoros Class1 Military Research\nPopulation : 9.3Billion 8.9Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nResearch : Flagship strike class 2\nResearch : Fleet strike class 2\nResearch : Hurricane Hit point class 3\nResearch : Assault rifle type class 2\nResearch : Shotgun type class 2\nResearch : Sniper rifle type class 2\nResearch : Submachine gun type class 2\nResearch : Logistics support class 3\nResearch : Vehicle support class 2");
                else if (AreaStatement.DeltaD31_12721State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Delta D31-402054 System\nPlanet Type : Araoros Class1 Military Research\nPopulation : 9.3Billion 8.9Million\nFactor of Safety : <color=#FF322F>-0.3</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1023)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("Jerato O95-1125");
                if (AreaStatement.JeratoO95_1125State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Araoros Class1 Military Base\nPopulation : 25.4Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_1125State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Araoros Class1 Military Base\nPopulation : 25.4Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_1125State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Araoros Class1 Military Base\nPopulation : 25.4Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nYou can check missions that can help your operation if you liberate this planet.(Currently unavailable, available in upcoming update)");
                else if (AreaStatement.JeratoO95_1125State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Araoros Class1 Military Base\nPopulation : 25.4Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1024)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 630);
                UniverseMapAreaName.text = string.Format("Jerato O95-2252");
                if (AreaStatement.JeratoO95_2252State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 8.7Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_2252State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 8.7Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_2252State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 8.7Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nFleet support slot : Third slot\n\nResearch : Flagship armor system class 3\nResearch : Formation ship armor system class 3\nResearch : Tactical ship armor system class 3\nResearch : Cannon type class 3\nResearch : Missile type class 3\nResearch : Carrier-based aircraft type class 3\nResearch : Sub gear type class 3\nResearch : Change heavy weapon class 3\nResearch : Bombardment support class 3");
                else if (AreaStatement.JeratoO95_2252State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Kalro Class2 Military Research\nPopulation : 8.7Billion\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else if (AreaNumber == 1025)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 490);
                UniverseMapAreaName.text = string.Format("Jerato O95-8510");
                if (AreaStatement.JeratoO95_8510State == 1)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Premirona Class0 Military Research\nPopulation : 31.5Billion 39Million\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#77FF2F>Stable</color>\n\n");
                else if (AreaStatement.JeratoO95_8510State == 2)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Premirona Class0 Military Research\nPopulation : 31.5Billion 39Million\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FFA42F>Invaded</color>\n\n");
                else if (AreaStatement.JeratoO95_8510State == 3)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Premirona Class0 Military Research\nPopulation : 31.5Billion 39Million\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#FF322F>Occupied</color>\n\nFollowing weapons will unlock if you liberate this planet. \n\nResearch : Flagship strike class 3\nResearch : Fleet strike class 3\nResearch : Assault rifle type class 3\nResearch : Shotgun type class 3\nResearch : Sniper rifle type class 3\nResearch : Submachine gun type class 3\nResearch : Heavy weapon support class 3\nResearch : Vehicle support class 3");
                else if (AreaStatement.JeratoO95_8510State == 4)
                    UniverseMapExplainUI.text = string.Format("Region : Garispaagorr\nStar : Jerato O95-99024 System\nPlanet Type : Premirona Class0 Military Research\nPopulation : 31.5Billion 39Million\nFactor of Safety : <color=#FF322F>-0.7</color>\nState : <color=#BD2FFF>Infected</color>\n\n");
            }
            else
            {
                UniverseMapSystem.ShowUI.SetActive(false);
                UniverseMapAreaName.text = string.Format("");
                UniverseMapExplainUI.text = string.Format("");
            }
        }
        else if (LanguageType == 2) //한국어
        {
            //항성
            if (AreaNumber == 1)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("토로피오");
                if (AreaStatement.ToropioStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 단일 항성\n행성 : 아포시스, 사타리우스 글래시아, 토로노, 아리주, 베데스 VI, 플로파 II\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n"); //토로피오 항성은 매우 활발한 활동을 하고 있으며, 이 시스템에서 가장 인구가 많은 사타리우스 글래시아 행성을 보유하고 있습니다. 나머지 행성들은 대부분 평범한 암석 행성이지만, 베데스 VI는 토로피오에 가장 근접한 매우 뜨거운 암석 행성이며, 아직 거주하는 인구가 5백만 명도 되지 않습니다. 매우 활발한 특성을 가진 덕분에 성간우주사령연합 기함 주포의 사격 실험장으로도 활용되고 있습니다.\n\n<color=#00E4FF>센티록스 전단 정찰 보고 : 현재 사타리우스 글래시아를 제외한 나머지 행성들은 컨트로스의 어떠한 정찰 흔적이 발견되지 않아, 침공 가능성이 존재하지 않습니다.</color>
                else if (AreaStatement.ToropioStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 단일 항성\n행성 : 아포시스, 사타리우스 글래시아, 토로노, 아리주, 베데스 VI, 플로파 II\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.ToropioStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 단일 항성\n행성 : 아포시스, 사타리우스 글래시아, 토로노, 아리주, 베데스 VI, 플로파 II\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 2)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("로로 I");
                if (AreaStatement.Roro1StarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Roro1StarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Roro1StarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소\n-컨트로스 지원 함대 차단");
            }
            else if (AreaNumber == 3)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("로로 II");
                if (AreaStatement.Roro2StarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Roro2StarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Roro2StarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 유형 : 쌍성계\n행성 : 아론 페리, 파파투스 II, 파파투스 III, 키예포토로스, 다르곤\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 4)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("사리시");
                if (AreaStatement.SarisiStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 에그로리트, 데이타스, 트라토스, 오클라시스, 데리우스 해리\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#77FF2F>안전</color>\n\n"); //사리시는 예측보다 더 늙은 항성으로 밝혀졌습니다. 이웃항성 토로피오보다 약 34억 년 더 빨리 생성되었기 때문에 지금은 현재 행성들에게는 힘든 항성으로 변하고 있습니다. 트라토스의 물이 수십만 년에 걸쳐서 약 78% 감소되었으며, 오클라시스의 대기는 약 89%가 사라지기도 하였습니다. 에그로리트와 데이타스는 라바행성으로 변하였으며, 지각활동이 심각한 관계로 거주지가 철수되었습니다.\n\n<color=#00E4FF>센티록스 전단 정찰 보고 : 오클라시스에서 희귀한 광물인 타리트로닉이 광맥 단위로 발견되면서 조성된 이 행성에서 최근에 추가로 지각 내부에 더 많은 광맥이 발견되었습니다. 이는 칸타크리들의 차이트리 강화 물질에 사용되는 만큼, 그들의 정찰을 주시하고 있습니다.</color>
                else if (AreaStatement.SarisiStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 에그로리트, 데이타스, 트라토스, 오클라시스, 데리우스 해리\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.SarisiStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 에그로리트, 데이타스, 트라토스, 오클라시스, 데리우스 해리\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 5)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("가릭스");
                if (AreaStatement.GarixStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 벨트로렉시, 에릭스 제퀘타, 퀴이포, 크라운 요세레\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.GarixStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 벨트로렉시, 에릭스 제퀘타, 퀴이포, 크라운 요세레\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.GarixStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 단일 항성\n행성 : 벨트로렉시, 에릭스 제퀘타, 퀴이포, 크라운 요세레\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소\n-컨트로스 지원 함대 차단");
            }
            else if (AreaNumber == 6)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("세크로스");
                if (AreaStatement.SecrosStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.SecrosStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.SecrosStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 기함 수 감소");
            }
            else if (AreaNumber == 7)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("테레토스");
                if (AreaStatement.TeretosStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.TeretosStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.TeretosStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 8)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("미니 포포");
                if (AreaStatement.MiniPopoStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.MiniPopoStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.MiniPopoStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 유형 : 3성계\n행성 : 오로스, 자펫 아그로네, 데일리 칸타크로스, 키시크루 04291, 자크로 042351\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 지원 함대 차단");
            }
            else if (AreaNumber == 9)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("델타 D31-4A");
                if (AreaStatement.DeltaD31_4AStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeltaD31_4AStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeltaD31_4AStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 기함 수 감소\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 10)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("델타 D31-4B");
                if (AreaStatement.DeltaD31_4BStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeltaD31_4BStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeltaD31_4BStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 쌍성계\n행성 : 델타 D31-2208, 델타 D31-9523, 델타 D31-9523\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 지원 함대 차단");
            }
            else if (AreaNumber == 11)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("제라토 O95-7A");
                if (AreaStatement.JeratoO95_7AStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_7AStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_7AStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 기함 수 감소");
            }
            else if (AreaNumber == 12)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("제라토 O95-7B");
                if (AreaStatement.JeratoO95_7BStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_7BStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_7BStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 13)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("제라토 O95-14C");
                if (AreaStatement.JeratoO95_14CStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_14CStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_14CStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 편대함 수 감소");
            }
            else if (AreaNumber == 14)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("제라토 O95-14D");
                if (AreaStatement.JeratoO95_14DStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_14DStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_14DStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 기함 수 감소");
            }
            else if (AreaNumber == 15)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 400);
                UniverseMapAreaName.text = string.Format("제라토 O95-오메가");
                if (AreaStatement.JeratoO95_OmegaStarState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_OmegaStarState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_OmegaStarState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 유형 : 5성계\n행성 : 제라토 O95-1125, 제라토 O95-2252, 제라토 O95-8510\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 항성에는 대규모 컨트로스 침공 함대가 주둔하고 있습니다. 이 항성을 되찾으면, 다음과 같이 행성에 주둔한 컨트로스 함대에게 타격을 줄 수 있습니다.\n-컨트로스 지원 함대 차단");
            }

            //행성
            else if (AreaNumber == 1001)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("사타리우스 글래시아");
                if (AreaStatement.SatariusGlessiaState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 3급 자드라 상업지\n인구 : 153억\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n"); //<b>\"사타리우스 글래시아 행성은 물이 가득하며, 공기가 다른 행성들에 비해 맑지만, 가장 좋은 것은 경치가 아주 좋다는 겁니다. 성간우주사령연합의 특이한 기함들이 지나가는 장관이 펼쳐진 경치 말이지요!\"\n<color=#B7B7B7>-사타리우스 글래시아의 부동산 기업, 산트라 릴렉스의 광고</color></b>\n\n사타리우스 글래시아는 풍부한 물이 존재하며, 많은 기업이 들어선 3급 자드라 상업 행성입니다. 이 행성에는 사일런스 시스트의 기함급 함포 개발본부가 존재하기 때문에 나리하의 기함들이 자주 왔다 가는 행성인 만큼, 기함들을 구경하기 좋은 행성 중 하나입니다.\n\n그리고 중요한 사실은, 성간우주사령연합의 지원을 많이 받는 행성입니다. 사일런스 시스트의 기함급 주포를 연구하는 것은 천문학적인 비용이 드는 것을 부정할 수 없는 사실이지요.\n\n<color=#00E4FF>센티록스 전단 정찰 보고 : 현재 대규모 컨트로스 함대가 가리스파고르 지역으로 집결하고 있는 만큼, 이곳은 최근 다시 수복한 이후로 컨트로스가 더 이상 집결하지 않는 것으로 확인되었습니다. 우리의 기함들을 안전하게 주둔시킬 수 있을 겁니다.</color>
                else if (AreaStatement.SatariusGlessiaState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 3급 자드라 상업지\n인구 : 153억\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.SatariusGlessiaState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 3급 자드라 상업지\n인구 : 153억\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 12 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 승인 : 10000\n건설 재료 한도 승인 : 10000");
                else if (AreaStatement.SatariusGlessiaState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 3급 자드라 상업지\n인구 : 153억\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1002)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("아포시스");
                if (AreaStatement.AposisState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 11급 케이스토 연구지\n인구 : 43억 7800만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.AposisState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 11급 케이스토 연구지\n인구 : 43억 7800만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.AposisState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 11급 케이스토 연구지\n인구 : 43억 7800만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 : 방패함\n델타 허리케인 무기 : 체인지 중화기(히드라-56 분리 철갑포)\n델타 허리케인 무기 : 보조 장비(OSEH-302 위도우 하이어 추적 미사일)\n\n연구 : 보급 지원 1 등급");
                else if (AreaStatement.AposisState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 11급 케이스토 연구지\n인구 : 43억 7800만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1003)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("토로노");
                if (AreaStatement.ToronoState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 8급 티록시 광맥\n인구 : 28억 110만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.ToronoState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 8급 티록시 광맥\n인구 : 28억 110만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.ToronoState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 8급 티록시 광맥\n인구 : 28억 110만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 12 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.ToronoState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 8급 티록시 광맥\n인구 : 28억 110만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1004)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("플로파 II");
                if (AreaStatement.Plopa2State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 7급 인토릭티 거주지\n인구 : 237억 8520만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Plopa2State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 7급 인토릭티 거주지\n인구 : 237억 8520만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Plopa2State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 7급 인토릭티 거주지\n인구 : 237억 8520만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
                else if (AreaStatement.Plopa2State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 7급 인토릭티 거주지\n인구 : 237억 8520만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1005)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("베데스 VI");
                if (AreaStatement.Vedes4State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 5급 나카마라 연구지\n인구 : 46억 5300만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Vedes4State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 5급 나카마라 연구지\n인구 : 46억 5300만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Vedes4State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 5급 나카마라 연구지\n인구 : 46억 5300만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n델타 허리케인 무기 : 체인지 중화기(MEAG 레일건)\n델타 허리케인 무기 : 주무기(CGD-27 필리시온 기관단총)\n델타 허리케인 무기 : 수류탄(VM-5 AEG)\n\n연구 : 델타 허리케인 체력 1 등급");
                else if (AreaStatement.Vedes4State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 토로피오\n행성 유형 : 5급 나카마라 연구지\n인구 : 46억 5300만\n안전척도 : <color=#77FF2F>1.0</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }

            else if (AreaNumber == 1006)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("아론 페리");
                if (AreaStatement.AronPeriState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 상업지\n인구 : 87억 7780만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.AronPeriState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 상업지\n인구 : 87억 7780만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.AronPeriState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 상업지\n인구 : 87억 7780만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 14 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.AronPeriState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 상업지\n인구 : 87억 7780만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1007)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("파파투스 II");
                if (AreaStatement.Papatus2State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 4급 하나파이론 광맥\n인구 : 41억\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Papatus2State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 4급 하나파이론 광맥\n인구 : 41억\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Papatus2State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 4급 하나파이론 광맥\n인구 : 41억\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 16 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.Papatus2State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 4급 하나파이론 광맥\n인구 : 41억\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1008)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 450);
                UniverseMapAreaName.text = string.Format("파파투스 III");
                if (AreaStatement.Papatus3State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 6급 에피도 연구지\n인구 : 29억 8900만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Papatus3State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 6급 에피도 연구지\n인구 : 29억 8900만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Papatus3State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 6급 에피도 연구지\n인구 : 29억 8900만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 무기 : 초과 점프\n함대 지원 슬롯 : 첫 번째 슬롯\n델타 허리케인 무기 : 체인지 중화기(아레스 L-775 충전 레이져)\n델타 허리케인 무기 : 주무기(DP-9007 저격총)\n함선 지원 : 중화기(M3078 미니건)\n\n연구 : 보조 장비 타입 1 등급\n연구 : 수류탄 타입 1 등급");
                else if (AreaStatement.Papatus3State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 6급 에피도 연구지\n인구 : 29억 8900만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1009)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("키예포토로스");
                if (AreaStatement.KyepotorosState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 거주지\n인구 : 72억 730만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.KyepotorosState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 거주지\n인구 : 72억 730만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.KyepotorosState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 거주지\n인구 : 72억 730만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
                else if (AreaStatement.KyepotorosState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 글로\n항성 : 로로 I, 로로 II\n행성 유형 : 7급 인토릭티 거주지\n인구 : 72억 730만\n안전척도 : <color=#77FF2F>0.9</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }

            else if (AreaNumber == 1010)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("트라토스");
                if (AreaStatement.TratosState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 6급 에피도 거주지\n인구 : 108억\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#77FF2F>안전</color>\n\n"); //<b>\"사는 집이 사타리우스 글래시아에 있는지, 아니면 이곳에 있는지에 따라 보는 눈이 달라진다고 하죠. 마치 가리스파고르 시골 출신처럼 쳐다보더군요.\"\n<color=#B7B7B7>-성간방위사령부 요원 소총 연구원 한스 진의 \'지역 출신별 효율론\'에 대해 연설 중</color></b>\n\n한 때 풍부한 바다가 가득했던 이 행성은 이미 97만년 전부터 사리시의 활동으로 급격하게 수면이 낮아지기 시작했으며, 지금은 물이 많이 존재하지 않습니다. 대신 사타리우스 글래시아의 좋은 위성 도시 역할을 하고 있으며, 많은 경제인들이 이곳에서 거주하고 있습니다.\n\n<color=#00E4FF>센티록스 전단 정찰 보고 : 칸타크리 함대 일부가 이곳 사리시 항성계 근처에서 포착되었지만, 곧바로 다른 지역으로 워프했음을 감지했습니다. 그들은 아마도 오클라시스 위치를 파악하기 위한 사전 작업일 가능성에 무게를 두고 있습니다.</color>
                else if (AreaStatement.TratosState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 6급 에피도 거주지\n인구 : 108억\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.TratosState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 6급 에피도 거주지\n인구 : 108억\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
                else if (AreaStatement.TratosState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 6급 에피도 거주지\n인구 : 108억\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1011)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("오클라시스");
                if (AreaStatement.OclasisState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 8급 티록시 광맥\n인구 : 79억 6500만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#77FF2F>안전</color>\n\n"); //<b>\"타리트로닉은 은밀한 천국을 부르는 물질입니다. 이것이 발견되는 순간, 그곳에 기업들이 몰리고, 돈이 몰리죠. 그리고 연방정부보다 성간기술정부가 더 빨리, 은밀하게 달려옵니다.\"\n<color=#B7B7B7>-게룩스 방송사의 탐사 다큐멘터리 \'정부를 부르는 광맥\'에서</color></b>\n\n타리트로닉이 굉장히 풍부하게 묻혀있는 오클라시스 행성은 사리시의 영향으로 58만 년 전부터 대기가 거의 사라졌습니다. 그런데 그것이 타리트로닉 광물이 급격하게 늘어나는 요소가 되었음이 연구를 통해 밝혀졌습니다. 비록 거주민은 적지만, 여러 채광 대기업과 성간우주사령연합이 자리를 잡고 있지요.\n\n<color=#00E4FF>델타 허리케인 보고 : 타리트로닉은 칸타크리의 우주 사령 장교들이 즐겨마시는 2급 사령직 티르트로이 제조 원료로 사용되고 있어. 최고급 원료중 하나이지. 물론, 장교 칸타크리의 프로크래시스트 강화에도 사용되고 있어. 아르키, 이 행성은 칸타크리들이 머지않아 침공할 계획을 갖출거야.</color>
                else if (AreaStatement.OclasisState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 8급 티록시 광맥\n인구 : 79억 6500만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.OclasisState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 8급 티록시 광맥\n인구 : 79억 6500만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 18 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.OclasisState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 8급 티록시 광맥\n인구 : 79억 6500만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1012)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 620);
                UniverseMapAreaName.text = string.Format("데리우스 헤리");
                if (AreaStatement.DeriousHeriState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 7급 인토릭티 연구지\n인구 : 27억 7780만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeriousHeriState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 7급 인토릭티 연구지\n인구 : 27억 7780만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeriousHeriState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 7급 인토릭티 연구지\n인구 : 27억 7780만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 무기 : 델타 니들-42 할리스트 멀티 미사일\n델타 허리케인 무기 : 체인지 중화기(UGG 98 중력포)\n델타 허리케인 무기 : 주무기(DS-65 샷건)\n함선 지원 : 폭격(PGM 1036 스칼렛 호크 순항 미사일)\n\n연구 : 기함 장갑 시스템 1 등급\n연구 : 편대함 장갑 시스템 1 등급\n연구 : 전략함 장갑 시스템 1 등급\n연구 : 주포 타입 1 등급\n연구 : 마사일 타입 1 등급\n연구 : 함재기 타입 1 등급\n연구 : 체인지 중화기 1 등급\n연구 : 보급 지원 2 등급\n연구 : 폭격 지원 1 등급");
                else if (AreaStatement.DeriousHeriState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 사리시\n행성 유형 : 7급 인토릭티 연구지\n인구 : 27억 7780만\n안전척도 : <color=#FFA42F>0.6</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1013)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("벨트로렉시");
                if (AreaStatement.VeltrorexyState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 상업지\n인구 : 61억\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.VeltrorexyState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 상업지\n인구 : 61억\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.VeltrorexyState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 상업지\n인구 : 61억\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 20 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.VeltrorexyState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 상업지\n인구 : 61억\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1014)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("에릭스 제퀘타");
                if (AreaStatement.ErixJeoqetaState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 2급 칼로 상업지\n인구 : 273억 2800만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.ErixJeoqetaState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 2급 칼로 상업지\n인구 : 273억 2800만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.ErixJeoqetaState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 2급 칼로 상업지\n인구 : 273억 2800만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 22 글로파\n\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.ErixJeoqetaState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 2급 칼로 상업지\n인구 : 273억 2800만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1015)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("퀴이포");
                if (AreaStatement.QeepoState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 광맥\n인구 : 38억 9500만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.QeepoState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 광맥\n인구 : 38억 9500만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.QeepoState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 광맥\n인구 : 38억 9500만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 24 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.QeepoState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 5급 나카마라 광맥\n인구 : 38억 9500만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1016)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 470);
                UniverseMapAreaName.text = string.Format("크라운 요세레");
                if (AreaStatement.CrownYosereState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 3급 자드라 연구지\n인구 : 17억 20만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.CrownYosereState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 3급 자드라 연구지\n인구 : 17억 20만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.CrownYosereState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 3급 자드라 연구지\n인구 : 17억 20만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함선 : 우주모함\n함선 지원 : 중화기(ASC 365 화염방사기)\n\n연구 : 델타 허리케인 체력 2 등급\n연구 : 돌격 소총 타입 1 등급\n연구 : 샷건 타입 1 등급\n연구 : 저격총 타입 1 등급\n연구 : 기관단총 타입 1 등급\n연구 : 수류탄 타입 2 등급\n연구 : 중화기 지원 1 등급");
                else if (AreaStatement.CrownYosereState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 가릭스\n행성 유형 : 3급 자드라 연구지\n인구 : 17억 20만\n안전척도 : <color=#FFA42F>0.4</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1017)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("오로스");
                if (AreaStatement.OrosState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 4급 하나파이론 상업지\n인구 : 135억\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.OrosState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 4급 하나파이론 상업지\n인구 : 135억\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.OrosState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 4급 하나파이론 상업지\n인구 : 135억\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 26 글로파\n5초당 최대 4 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.OrosState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 4급 하나파이론 상업지\n인구 : 135억\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1018)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("자펫 아그로네");
                if (AreaStatement.JapetAgroneState == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 6급 에피도 연구지\n인구 : 106억 2860만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JapetAgroneState == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 6급 에피도 연구지\n인구 : 106억 2860만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JapetAgroneState == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 6급 에피도 연구지\n인구 : 106억 2860만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함대 지원 슬롯 : 두 번째 슬롯\n함선 지원 : 탑승 차량(MBCA-79 아이언 허리케인)\n\n연구 : 기함 공격 1 등급\n연구 : 함대 공격 1 등급\n연구 : 보조 장비 타입 2 등급\n연구 : 탑승 차량 지원 1 등급\n연구 : 폭격 지원 2 등급");
                else if (AreaStatement.JapetAgroneState == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 6급 에피도 연구지\n인구 : 106억 2860만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1019)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("자크로 042351");
                if (AreaStatement.Xacro042351State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 1급 아라오로스 광맥\n인구 : 54억 3200만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.Xacro042351State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 1급 아라오로스 광맥\n인구 : 54억 3200만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.Xacro042351State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 1급 아라오로스 광맥\n인구 : 54억 3200만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 자원을 획득할 수 있습니다. \n\n5초당 최대 4 글로파\n5초당 최대 28 건설 재료\n\n글로파오로스 한도 추가 : 2000\n건설 재료 한도 추가 : 2000");
                else if (AreaStatement.Xacro042351State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 타로스\n항성 : 옥토크라시스 파토로 항성계\n행성 유형 : 1급 아라오로스 광맥\n인구 : 54억 3200만\n안전척도 : <color=#FFA42F>0.1</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1020)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("델타 D31-2208");
                if (AreaStatement.DeltaD31_2208State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 79억 1230만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeltaD31_2208State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 79억 1230만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeltaD31_2208State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 79억 1230만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 장갑 시스템 2 등급\n연구 : 편대함 장갑 시스템 2 등급\n연구 : 전략함 장갑 시스템 2 등급\n연구 : 주포 타입 2 등급\n연구 : 마사일 타입 2 등급\n연구 : 함재기 타입 2 등급\n연구 : 수류탄 타입 3 등급\n연구 : 체인지 중화기 2 등급\n연구 : 중화기 지원 2 등급");
                else if (AreaStatement.DeltaD31_2208State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 79억 1230만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1021)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("델타 D31-9523");
                if (AreaStatement.DeltaD31_9523State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 3급 자드라 군사기지\n인구 : 128억\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeltaD31_9523State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 3급 자드라 군사기지\n인구 : 128억\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeltaD31_9523State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 3급 자드라 군사기지\n인구 : 128억\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
                else if (AreaStatement.DeltaD31_9523State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 3급 자드라 군사기지\n인구 : 128억\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1022)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("델타 D31-12721");
                if (AreaStatement.DeltaD31_12721State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 1급 아라오로스 군사 연구지\n인구 : 93억 890만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.DeltaD31_12721State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 1급 아라오로스 군사 연구지\n인구 : 93억 890만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.DeltaD31_12721State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 1급 아라오로스 군사 연구지\n인구 : 93억 890만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 공격 2 등급\n연구 : 함대 공격 2 등급\n연구 : 델타 허리케인 체력 3 등급\n연구 : 돌격 소총 타입 2 등급\n연구 : 샷건 타입 2 등급\n연구 : 저격총 타입 2 등급\n연구 : 기관단총 타입 2 등급\n연구 : 보급 지원 3 등급\n연구 : 탑승 차량 지원 2 등급");
                else if (AreaStatement.DeltaD31_12721State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 델타 D31-402054 항성계\n행성 유형 : 1급 아라오로스 군사 연구지\n인구 : 93억 890만\n안전척도 : <color=#FF322F>-0.3</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1023)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("제라토 O95-1125");
                if (AreaStatement.JeratoO95_1125State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 1급 아라오로스 군사기지\n인구 : 254억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_1125State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 1급 아라오로스 군사기지\n인구 : 254억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_1125State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 1급 아라오로스 군사기지\n인구 : 254억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 작전에 도움이 될 수 있는 임무를 확인할 수 있습니다.(현재 사용 불가능하며, 다음 업데이트에서 가능)");
                else if (AreaStatement.JeratoO95_1125State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 1급 아라오로스 군사기지\n인구 : 254억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1024)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 475);
                UniverseMapAreaName.text = string.Format("제라토 O95-2252");
                if (AreaStatement.JeratoO95_2252State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 87억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_2252State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 87억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_2252State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 87억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n함대 지원 슬롯 : 세 번째 슬롯\n\n연구 : 기함 장갑 시스템 3 등급\n연구 : 편대함 장갑 시스템 3 등급\n연구 : 전략함 장갑 시스템 3 등급\n연구 : 주포 타입 3 등급\n연구 : 마사일 타입 3 등급\n연구 : 함재기 타입 3 등급\n연구 : 보조 장비 타입 3 등급\n연구 : 체인지 중화기 3 등급\n연구 : 폭격 지원 3 등급");
                else if (AreaStatement.JeratoO95_2252State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 2급 칼로 군사 연구지\n인구 : 87억\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else if (AreaNumber == 1025)
            {
                ScrollVerticalSize.sizeDelta = new Vector2(361, 0);
                UniverseMapAreaName.text = string.Format("제라토 O95-8510");
                if (AreaStatement.JeratoO95_8510State == 1)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 0급 프리미로나 군사 연구지\n인구 : 315억 3900만\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#77FF2F>안전</color>\n\n");
                else if (AreaStatement.JeratoO95_8510State == 2)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 0급 프리미로나 군사 연구지\n인구 : 315억 3900만\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FFA42F>침공</color>\n\n");
                else if (AreaStatement.JeratoO95_8510State == 3)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 0급 프리미로나 군사 연구지\n인구 : 315억 3900만\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#FF322F>점령</color>\n\n이 행성을 해방시, 다음과 같은 무기가 잠금 해제됩니다. \n\n연구 : 기함 공격 3 등급\n연구 : 함대 공격 3 등급\n연구 : 돌격 소총 타입 3 등급\n연구 : 샷건 타입 3 등급\n연구 : 저격총 타입 3 등급\n연구 : 기관단총 타입 3 등급\n연구 : 중화기 지원 3 등급\n연구 : 탑승 차량 지원 3 등급");
                else if (AreaStatement.JeratoO95_8510State == 4)
                    UniverseMapExplainUI.text = string.Format("지역 : 가리스파고르\n항성 : 제라토 O95-99024 항성계\n행성 유형 : 0급 프리미로나 군사 연구지\n인구 : 315억 3900만\n안전척도 : <color=#FF322F>-0.7</color>\n상태 : <color=#BD2FFF>감염</color>\n\n");
            }
            else
            {
                UniverseMapSystem.ShowUI.SetActive(false);
                UniverseMapAreaName.text = string.Format("");
                UniverseMapExplainUI.text = string.Format("");
            }
        }
    }

    string AreaNumberForLog(int AreaNumber)
    {
        string name = "";

        if (LanguageType == 1)
        {
            if (AreaNumber == 1)
                name =  "Toropio";
            else if (AreaNumber == 2)
                name = "Roro I";
            else if (AreaNumber == 3)
                name = "Roro II";
            else if (AreaNumber == 4)
                name = "Sarisi";
            else if (AreaNumber == 5)
                name = "Garix";
            else if (AreaNumber == 6)
                name = "Secros";
            else if (AreaNumber == 7)
                name = "Teretos";
            else if (AreaNumber == 8)
                name = "Mini popo";
            else if (AreaNumber == 9)
                name = "Delta D31-4A";
            else if (AreaNumber == 10)
                name = "Delta D31-4B";
            else if (AreaNumber == 11)
                name = "Jerato O95-7A";
            else if (AreaNumber == 12)
                name = "Jerato O95-7B";
            else if (AreaNumber == 13)
                name = "Jerato O95-14C";
            else if (AreaNumber == 14)
                name = "Jerato O95-14D";
            else if (AreaNumber == 15)
                name = "Jerato O95-Omega";

            else if (AreaNumber == 1001)
                name = "Satarius Glessia";
            else if (AreaNumber == 1002)
                name = "Aposis";
            else if (AreaNumber == 1003)
                name = "Torono";
            else if (AreaNumber == 1004)
                name = "Plopa II";
            else if (AreaNumber == 1005)
                name = "Vedes VI";
            else if (AreaNumber == 1006)
                name = "Aron Peri";
            else if (AreaNumber == 1007)
                name = "Papatus II";
            else if (AreaNumber == 1008)
                name = "Papatus III";
            else if (AreaNumber == 1009)
                name = "Kyepotoros";
            else if (AreaNumber == 1010)
                name = "Tratos";
            else if (AreaNumber == 1011)
                name = "Oclasis";
            else if (AreaNumber == 1012)
                name = "Derious Heri";
            else if (AreaNumber == 1013)
                name = "Veltrorexy";
            else if (AreaNumber == 1014)
                name = "Erix Jeoqeta";
            else if (AreaNumber == 1015)
                name = "Qeepo";
            else if (AreaNumber == 1016)
                name = "Crown Yosere";
            else if (AreaNumber == 1017)
                name = "Oros";
            else if (AreaNumber == 1018)
                name = "Japet Agrone";
            else if (AreaNumber == 1019)
                name = "Xacro 042351";
            else if (AreaNumber == 1020)
                name = "Delta D31-2208";
            else if (AreaNumber == 1021)
                name = "Delta D31-9523";
            else if (AreaNumber == 1022)
                name = "Delta D31-12721";
            else if (AreaNumber == 1023)
                name = "Jerato O95-1125";
            else if (AreaNumber == 1024)
                name = "Jerato O95-2252";
            else if (AreaNumber == 1025)
                name = "Jerato O95-8510";
        }
        else if (LanguageType == 2)
        {
            if (AreaNumber == 1)
                name = "토로피오";
            else if (AreaNumber == 2)
                name = "로로 I";
            else if (AreaNumber == 3)
                name = "로로 II";
            else if (AreaNumber == 4)
                name = "사리시";
            else if (AreaNumber == 5)
                name = "가릭스";
            else if (AreaNumber == 6)
                name = "세크로스";
            else if (AreaNumber == 7)
                name = "테레토스";
            else if (AreaNumber == 8)
                name = "미니 포포";
            else if (AreaNumber == 9)
                name = "델타 D31-4A";
            else if (AreaNumber == 10)
                name = "델타 D31-4B";
            else if (AreaNumber == 11)
                name = "제라토 O95-7A";
            else if (AreaNumber == 12)
                name = "제라토 O95-7B";
            else if (AreaNumber == 13)
                name = "제라토 O95-14C";
            else if (AreaNumber == 14)
                name = "제라토 O95-14D";
            else if (AreaNumber == 15)
                name = "제라토 O95-오메가";

            else if (AreaNumber == 1001)
                name = "사타리우스 글래시아";
            else if (AreaNumber == 1002)
                name = "아포시스";
            else if (AreaNumber == 1003)
                name = "토로노";
            else if (AreaNumber == 1004)
                name = "플로파 II";
            else if (AreaNumber == 1005)
                name = "베데스 VI";
            else if (AreaNumber == 1006)
                name = "아론 페리";
            else if (AreaNumber == 1007)
                name = "파파투스 II";
            else if (AreaNumber == 1008)
                name = "파파투스 III";
            else if (AreaNumber == 1009)
                name = "키예포토로스";
            else if (AreaNumber == 1010)
                name = "트라토스";
            else if (AreaNumber == 1011)
                name = "오클라시스";
            else if (AreaNumber == 1012)
                name = "데리우스 헤리";
            else if (AreaNumber == 1013)
                name = "벨트로렉시";
            else if (AreaNumber == 1014)
                name = "에릭스 제퀘타";
            else if (AreaNumber == 1015)
                name = "퀴이포";
            else if (AreaNumber == 1016)
                name = "크라운 요세레";
            else if (AreaNumber == 1017)
                name = "오로스";
            else if (AreaNumber == 1018)
                name = "자펫 아그로네";
            else if (AreaNumber == 1019)
                name = "자크로 042351";
            else if (AreaNumber == 1020)
                name = "델타 D31-2208";
            else if (AreaNumber == 1021)
                name = "델타 D31-9523";
            else if (AreaNumber == 1022)
                name = "델타 D31-12721";
            else if (AreaNumber == 1023)
                name = "제라토 O95-1125";
            else if (AreaNumber == 1024)
                name = "제라토 O95-2252";
            else if (AreaNumber == 1025)
                name = "제라토 O95-8510";
        }
        return name;
    }

    //코드 제조기
    string GenerateRandomCode()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string nextChar = "-";
        var random = new System.Random();
        int CodeAmount = Random.Range(2, 4);
        int codeLength = 0;
        int Type1 = 0;
        int Type2 = 0;

        for (int i = 0; i < CodeAmount; i++)
        {
            codeLength += Random.Range(5, 9);
            if (i == 0)
                Type1 = codeLength;
            if (i == 1)
                Type2 = codeLength;
        }

        var result = new char[codeLength];

        for (int i = 0; i < codeLength; i++)
        {
            result[i] = chars[random.Next(chars.Length)];

            if (CodeAmount == 2)
            {
                if (i == Type1 - 1)
                    result[i] = nextChar[0];
            }
            else if (CodeAmount == 3)
            {
                if (i == Type1 - 1)
                    result[i] = nextChar[0];
                if (i == Type2 - 1)
                    result[i] = nextChar[0];
            }
        }
        return new string(result);
    }
}