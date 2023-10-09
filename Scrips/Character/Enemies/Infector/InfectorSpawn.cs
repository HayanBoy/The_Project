using UnityEngine;

public class InfectorSpawn : MonoBehaviour
{
    public TearInfector tearInfector;

    public bool TearingStart;
    private int i;
    private int clothTear;
    private int DownRclothTear;
    private int DownLclothTear;
    private int TopDownLclothTear;
    private int TopDownRclothTear;

    private int body; //일반 몸, 마른 몸
    private int finger; //손가락 유형
    private int Glovefinger; //장갑 유형
    private int fingerType = 10; //손가락 장갑 착용
    private int fingerColor; //손가락 장갑 색상
    private int job; //일반인, 회사원, 기술자, 의료원
    private int topClothes; //일반인 의상
    private int pants; //일반인 하의
    private int shoes; //일반인 신발

    private int tearing; //옷이 찢어졌는지 여부
    private int tearingSR; //오른쪽 신발이 찢어졌는지 여부
    private int tearingSL; //왼쪽 신발이 찢어졌는지 여부
    private int LED; //LED가 켜졌는지 여부
    private int LEDR;
    private int LEDL;

    private int clothesColor; //일반옷 색상
    private int pantsColor; //일반바지 색상
    private int suitsColor; //작업복 색상
    private int shoesColor; //신발 색상
    private int glovesColor; //장갑 색상
    private int LEDcolorleg; //회사원 바지 LED 색상
    private int LEDcolor; //LED 색상
    private int LEDcolorR; //반바지 LED 오른쪽
    private int LEDcolorL; //반바지 LED 왼쪽
    private int LEDBetweenL; //LED 같은 색상에서 출력되는 여부
    private int LEDBetweenR; //LED 같은 색상에서 출력되는 여부
    private int tearAmounts; //찢김수
    private int tearType; //찢긴종류

    private int faceType; //얼굴 유형
    private int hairtype; //머리카락 존재
    private int hair; //머리카락 번호
    private int hairColor; //머리카락 색상

    public bool inCap = false; //모자 착용 여부
    bool dontGetHair = false; //특정 얼굴에서의 머리카락 거부 여부
    bool suit1in = false; //정장 착용 여부

    private int logoFrame; //로고 프레임
    private int logoFrameColor; //로고 프레임 색상
    private int logoInput; //로고 착용
    private int logo; //로고
    private int display; //화면 색상

    //재스폰시, 스타일 초가화
    private bool Body1Off = false;
    private bool Body2Off = false;
    private bool HandOff = false;
    private bool LogoDisplayOff = false;
    private bool Logo1_1FOff = false;
    private bool Logo1_2FOff = false;
    private bool Logo1_3FOff = false;
    private bool Logo1_4FOff = false;
    private bool Logo1_1Off = false;
    private bool Logo1_2Off = false;
    private bool Logo1_3Off = false;
    private bool Logo1_4Off = false;
    private bool Logo1_5Off = false;
    private bool Logo1_6Off = false;
    private bool Logo1_7Off = false;
    private bool Logo1_8Off = false;
    private bool Logo1_9Off = false;
    private bool Logo1_10Off = false;
    private bool Logo1_11Off = false;
    private bool Logo1_12Off = false;
    private bool Logo1_13Off = false;
    private bool Logo1_14Off = false;
    private bool Logo1_15Off = false;
    private bool Logo1_16Off = false;
    private bool Logo1_17Off = false;
    private bool Logo1_18Off = false;
    private bool Logo1_19Off = false;
    private bool Logo1_20Off = false;
    private bool Logo1_21Off = false;

    //얼굴 스타일 초기화
    private bool Face1Off = false;
    private bool Face2Off = false;
    private bool Face3Off = false;
    private bool Face4Off = false;
    private bool Face5Off = false;
    private bool Hair1Off = false;
    private bool Hair2Off = false;
    private bool Hair3Off = false;
    private bool Hair4Off = false;
    private bool Hair5Off = false;
    private bool Hair6Off = false;
    private bool CapOff = false;

    //일반인 스타일 초기화
    private bool Job1Off = false;
    private bool Job1_1Off = false;
    private bool Job1_2Off = false;
    private bool Job1_3Off = false;
    private bool Job1_4Off = false;
    private bool Job1LEDOff = false;
    private bool Job1LEDtOff = false;
    private bool Job1SOff = false;
    private bool Job1_1SOff = false;
    private bool Job1_2SOff = false;
    private bool Job1_3SOff = false;
    private bool Job1_4SOff = false;
    private bool Job1SLEDOff = false;
    private bool Job1_1POff = false;
    private bool Job1_2POff = false;
    private bool Job1_3POff = false;
    private bool Job1_4POff = false;
    private bool Job1PLEDOff = false;
    private bool Job1_1SPOff = false;
    private bool Job1_2SPOff = false;
    private bool Job1_3SPOff = false;
    private bool Job1_4SPOff = false;
    private bool Job1SPLEDROff = false;
    private bool Job1SPLEDLOff = false;
    private bool Job1_1ShoesOff = false;
    private bool Job1_2ShoesOff = false;
    private bool Job1_3ShoesOff = false;
    private bool Job1_4ShoesOff = false;
    private bool Job1_5ShoesOff = false;
    private bool Job1_1SShoesOff = false;
    private bool Job1_2SShoesOff = false;
    private bool Job1_3SShoesOff = false;
    private bool Job1_4SShoesOff = false;
    private bool Job1_5SShoesOff = false;

    //회사원 스타일 초기화
    private bool Job2_1Off = false;
    private bool Job2_2Off = false;
    private bool Job2_3Off = false;
    private bool Job2_4Off = false;
    private bool Job2_5Off = false;
    private bool Job2LEDOff = false;
    private bool Job2LEDLegtOff = false;
    private bool Job2LEDLegOff = false;
    private bool Job2_1ShoesOff = false;
    private bool Job2_2ShoesOff = false;
    private bool Job2_3ShoesOff = false;

    //기술자 스타일 초기화
    private bool Job3_1Off = false;
    private bool Job3_2Off = false;
    private bool Job3_3Off = false;
    private bool Job3_4Off = false;
    private bool Job3_5Off = false;
    private bool Job3_1LEDOff= false;
    private bool Job3_2LEDOff = false;
    private bool Job3_3LEDOff = false;
    private bool Job3_4LEDOff = false;
    private bool Job3_5LEDOff = false;
    private bool Job3_1SOff = false;
    private bool Job3_2SOff = false;
    private bool Job3_3SOff = false;
    private bool Job3_4SOff = false;

    //의료원 스타일 초기화
    private bool Job4_1Off = false;
    private bool Job4_2Off = false;
    private bool Job4_3Off = false;
    private bool Job4_4Off = false;
    private bool Job4_5Off = false;
    private bool Job4_1LEDOff= false;
    private bool Job4_2LEDOff = false;
    private bool Job4_3LEDOff = false;
    private bool Job4_4LEDOff = false;
    private bool Job4_5LEDOff = false;
    private bool Job4LEDRtOff = false;
    private bool Job4LEDROff = false;
    private bool Job4_1SOff = false;
    private bool Job4_2SOff = false;
    private bool Job4_3SOff = false;
    private bool Job4_4SOff = false;
    private bool Job4_5SOff = false;
    private bool Job4_1GOff = false;
    private bool Job4_2GOff = false;
    private bool Job4_3GOff = false;
    private bool Job4_4GOff = false;
    private bool Job4_5GOff = false;

    //얼굴
    public GameObject Head;
    public Transform Headpos;
    public bool HEADOUTonline = false;
    private bool FACE1 = false; //얼굴
    private bool FACE2 = false;
    private bool FACE3 = false;
    private bool FACE4 = false;
    private bool FACE5 = false;
    private bool HAIR1_1 = false; //머리카락
    private bool HAIR1_2 = false;
    private bool HAIR1_3 = false;
    private bool HAIR1_4 = false;
    private bool HAIR1_5 = false;
    private bool HAIR2_1 = false;
    private bool HAIR2_2 = false;
    private bool HAIR2_3 = false;
    private bool HAIR2_4 = false;
    private bool HAIR2_5 = false;
    private bool HAIR3_1 = false;
    private bool HAIR3_2 = false;
    private bool HAIR3_3 = false;
    private bool HAIR3_4 = false;
    private bool HAIR3_5 = false;
    private bool HAIR4_1 = false;
    private bool HAIR4_2 = false;
    private bool HAIR4_3 = false;
    private bool HAIR4_4 = false;
    private bool HAIR4_5 = false;
    private bool HAIR5_1 = false;
    private bool HAIR5_2 = false;
    private bool HAIR5_3 = false;
    private bool HAIR5_4 = false;
    private bool HAIR5_5 = false;
    private bool HAIR6_1 = false;
    private bool HAIR6_2 = false;
    private bool HAIR6_3 = false;
    private bool HAIR6_4 = false;
    private bool HAIR6_5 = false;
    private bool S3HC1 = false; //모자
    private bool S3HC2 = false;
    private bool S3HC3 = false;
    private bool S3HC4 = false;
    private bool S3HC5 = false;

    public bool HEADout = false; //얼굴 삭제

    //오른손
    public GameObject HandR; //오른쪽 하단 손
    public Transform HandRpos;
    public bool HANDROUTonline = false;
    private bool HAND1R = false;
    private bool HAND2R = false;
    private bool HAND3R = false;
    private bool G1HAND1RC1 = false;
    private bool G1HAND2RC1 = false;
    private bool G1HAND3RC1 = false;
    private bool G1HAND1RC2 = false;
    private bool G1HAND2RC2 = false;
    private bool G1HAND3RC2 = false;
    private bool G1HAND1RC3 = false;
    private bool G1HAND2RC3 = false;
    private bool G1HAND3RC3 = false;
    private bool G1HAND1RC4 = false;
    private bool G1HAND2RC4 = false;
    private bool G1HAND3RC4 = false;
    private bool G1HAND1RC5 = false;
    private bool G1HAND2RC5 = false;
    private bool G1HAND3RC5 = false;

    //오른팔 상단
    public GameObject RightArmUp; //오른쪽 상단 팔
    public Transform RightArmUppos;
    public GameObject RightArmUpDown; //오른쪽 상단 팔 + 오른쪽 하단 팔
    public Transform RightArmUpDownpos;
    public GameObject RightArmUpDownHand; //오른쪽 상단 팔 + 오른쪽 하단 팔 + 오른손
    public Transform RightArmUpDownHandpos;
    public bool RightArmUpOUTonline = false;
    public bool RightArmUpDownOUTonline = false;
    public bool RightArmUpDownHOUTonline = false;
    private bool BODY1RUA = false;
    private bool BODY2RUA = false;
    private bool CT1RUA = false;
    private bool CT2RA = false;
    private bool S1TRUAC1 = false; //회사원
    private bool S1TRUAC2 = false;
    private bool S1TRUAC3 = false;
    private bool S1TRUAC4 = false;
    private bool S1TRUAC5 = false;
    private bool S2TRUAC1 = false; //기술자
    private bool S2TRUAC2 = false;
    private bool S2TRUAC3 = false;
    private bool S2TRUAC4 = false;
    private bool S2TRUAC5 = false;
    private bool S3TRUAC1 = false; //의료원
    private bool S3TRUAC2 = false;
    private bool S3TRUAC3 = false;
    private bool S3TRUAC4 = false;
    private bool S3TRUAC5 = false;

    //오른팔 하단
    public GameObject RightArmDown; //오른쪽 하단 팔
    public Transform RightArmDownpos;
    public GameObject RightArmDownHand; //오른쪽 하단 팔 + 오른쪽 손
    public Transform RightArmDownHandpos;
    public bool RightArmDownOUTonline = false;
    public bool RightArmDownHOUTonline = false;
    private bool BODY1RDAR = false;
    private bool BODY2RDAR = false;
    private bool CT1RDA = false; //일반옷
    private bool S1TRDAC1 = false; //회사원
    private bool S1TRDAC2 = false;
    private bool S1TRDAC3 = false;
    private bool S1TRDAC4 = false;
    private bool S1TRDAC5 = false;
    private bool S2TRDAC1 = false; //기술자
    private bool S2TRDAC2 = false;
    private bool S2TRDAC3 = false;
    private bool S2TRDAC4 = false;
    private bool S2TRDAC5 = false;
    private bool S2TRDACC1 = false; //기술자 옷조각
    private bool S2TRDACC2 = false;
    private bool S2TRDACC3 = false;
    private bool S2TRDACC4 = false;
    private bool S2TRDACC5 = false;
    private bool S2TRDAL1 = false; //기술자 LED
    private bool S2TRDAL2 = false;
    private bool S2TRDAL3 = false;
    private bool S2TRDAL4 = false;
    private bool S2TRDAL5 = false;
    private bool S3TRDAC1 = false; //의료원
    private bool S3TRDAC2 = false;
    private bool S3TRDAC3 = false;
    private bool S3TRDAC4 = false;
    private bool S3TRDAC5 = false;
    private bool S3TRDACC1 = false; //의료원 옷조각
    private bool S3TRDACC2 = false;
    private bool S3TRDACC3 = false;
    private bool S3TRDACC4 = false;
    private bool S3TRDACC5 = false;
    private bool S3TRDAL1 = false; //의료원 LED
    private bool S3TRDAL2 = false;
    private bool S3TRDAL3 = false;
    private bool S3TRDAL4 = false;
    private bool S3TRDAL5 = false;

    //오른팔 영역 삭제 활성화
    public bool HandRout = false;
    public bool B1LDRout = false;
    public bool B1LURout = false;

    //왼손
    public GameObject HandL; //왼쪽 하단 손
    public Transform HandLpos;
    public bool HANDLOUTonline = false;
    private bool HAND1L = false;
    private bool HAND2L = false;
    private bool HAND3L = false;
    private bool G1HAND1LC1 = false;
    private bool G1HAND2LC1 = false;
    private bool G1HAND3LC1 = false;
    private bool G1HAND1LC2 = false;
    private bool G1HAND2LC2 = false;
    private bool G1HAND3LC2 = false;
    private bool G1HAND1LC3 = false;
    private bool G1HAND2LC3 = false;
    private bool G1HAND3LC3 = false;
    private bool G1HAND1LC4 = false;
    private bool G1HAND2LC4 = false;
    private bool G1HAND3LC4 = false;
    private bool G1HAND1LC5 = false;
    private bool G1HAND2LC5 = false;
    private bool G1HAND3LC5 = false;

    //왼팔 상단
    public GameObject LeftUp; //왼쪽 상단 팔
    public Transform LeftUppos;
    public GameObject LeftUpDown; //왼쪽 상단 팔 + 왼쪽 하단 팔
    public Transform LeftUpDownpos;
    public GameObject LeftUpDownHand; //왼쪽 상단 팔 + 왼쪽 하단 팔 + 왼손
    public Transform LeftUpDownHandpos;
    public bool LeftArmUpOUTonline = false;
    public bool LeftArmUpDownOUTonline = false;
    public bool LeftArmUpDownHOUTonline = false;
    private bool BODY1LUA = false;
    private bool BODY2LUA = false;
    private bool CT1LUA = false;
    private bool CT2LA = false;
    private bool S1TLUAC1 = false; //회사원
    private bool S1TLUAC2 = false;
    private bool S1TLUAC3 = false;
    private bool S1TLUAC4 = false;
    private bool S1TLUAC5 = false;
    private bool S2TLUAC1 = false; //기술자
    private bool S2TLUAC2 = false;
    private bool S2TLUAC3 = false;
    private bool S2TLUAC4 = false;
    private bool S2TLUAC5 = false;
    private bool S3TLUAC1 = false; //의료원
    private bool S3TLUAC2 = false;
    private bool S3TLUAC3 = false;
    private bool S3TLUAC4 = false;
    private bool S3TLUAC5 = false;

    //왼팔 하단
    public GameObject Body1LDL; //왼쪽 하단 팔
    public Transform Body1LDLpos;
    public GameObject Body1LDLh; //왼쪽 하단 팔 + 왼쪽 손
    public Transform Body1LDLhpos;
    public bool LeftArmDownOUTonline = false;
    public bool LeftArmDownHOUTonline = false;
    private bool BODY1LDA = false;
    private bool BODY2LDA = false;
    private bool CT1LDA = false; //일반옷
    private bool S1TLDAC1 = false; //회사원
    private bool S1TLDAC2 = false;
    private bool S1TLDAC3 = false;
    private bool S1TLDAC4 = false;
    private bool S1TLDAC5 = false;
    private bool S2TLDAC1 = false; //기술자
    private bool S2TLDAC2 = false;
    private bool S2TLDAC3 = false;
    private bool S2TLDAC4 = false;
    private bool S2TLDAC5 = false;
    private bool S2TLDACC1 = false; //기술자 옷조각
    private bool S2TLDACC2 = false;
    private bool S2TLDACC3 = false;
    private bool S2TLDACC4 = false;
    private bool S2TLDACC5 = false;
    private bool S2TLDAL1 = false; //기술자 LED
    private bool S2TLDAL2 = false;
    private bool S2TLDAL3 = false;
    private bool S2TLDAL4 = false;
    private bool S2TLDAL5 = false;
    private bool S3TLDAC1 = false; //의료원
    private bool S3TLDAC2 = false;
    private bool S3TLDAC3 = false;
    private bool S3TLDAC4 = false;
    private bool S3TLDAC5 = false;
    private bool S3TLDACC1 = false; //의료원 옷조각
    private bool S3TLDACC2 = false;
    private bool S3TLDACC3 = false;
    private bool S3TLDACC4 = false;
    private bool S3TLDACC5 = false;
    private bool S3TLDAL1 = false; //의료원 LED
    private bool S3TLDAL2 = false;
    private bool S3TLDAL3 = false;
    private bool S3TLDAL4 = false;
    private bool S3TLDAL5 = false;

    //왼팔 영역 삭제 활성화
    public bool HandLout = false;
    public bool B1LDLout = false;
    public bool B1LULout = false;

    //다리
    public bool BODY1LL = false;
    public bool BODY2LL = false;
    public bool FOOTL = false;
    public bool BODY1RL = false;
    public bool BODY2RL = false;
    public bool FOOTR = false;

    //왼쪽 바지
    public GameObject LeftLeg;
    public Transform LeftLegpos;
    public bool LeftLegOUTonline = false;
    private bool P1LC1 = false; //일반 바지
    private bool P1LC2 = false;
    private bool P1LC3 = false;
    private bool P1LC4 = false;
    private bool P1LCC1 = false; //찢어진 일반 바지
    private bool P1LCC2 = false;
    private bool P1LCC3 = false;
    private bool P1LCC4 = false;
    private bool P2LC1 = false; //일반 반바지
    private bool P2LC2 = false;
    private bool P2LC3 = false;
    private bool P2LC4 = false;
    private bool P2LCC1 = false; //찢어진 일반 반바지
    private bool P2LCC2 = false;
    private bool P2LCC3 = false;
    private bool P2LCC4 = false;
    private bool P2LL1 = false; //반바지 LED
    private bool P2LL2 = false;
    private bool P2LL3 = false;
    private bool P2LL4 = false;
    private bool P2LL5 = false;
    private bool S1LC1 = false; //일반 신발
    private bool S1LC2 = false;
    private bool S1LC3 = false;
    private bool S1LC4 = false;
    private bool S1LC5 = false;
    private bool S1LC1T = false; //찢어진 일반 신발
    private bool S1LC2T = false;
    private bool S1LC3T = false;
    private bool S1LC4T = false;
    private bool S1LC5T = false;
    private bool S2LC1 = false; //일반 샌들
    private bool S2LC2 = false;
    private bool S2LC3 = false;
    private bool S2LC4 = false;
    private bool S2LC5 = false;
    private bool S1DLC1 = false; //회사원 바지
    private bool S1DLC2 = false;
    private bool S1DLC3 = false;
    private bool S1DLC4 = false;
    private bool S1DLC5 = false;
    private bool S1DLCC1 = false; //회사원 찢어진 바지
    private bool S1DLCC2 = false;
    private bool S1DLCC3 = false;
    private bool S1DLCC4 = false;
    private bool S1DLCC5 = false;
    private bool S1DLL1 = false; //회사원 바지 LED
    private bool S1DLL2 = false;
    private bool S1DLL3 = false;
    private bool S1DLL4 = false;
    private bool S1DLL5 = false;
    private bool S1DLL1T = false; //회사원 파손된 LED
    private bool S1DLL2T = false;
    private bool S1DLL3T = false;
    private bool S1DLL4T = false;
    private bool S1DLL5T = false;
    private bool S3LC1 = false; //회사원 신발
    private bool S3LC2 = false;
    private bool S3LC3 = false;
    private bool S3LC1T = false; //회사원 찢어진 신발
    private bool S3LC2T = false;
    private bool S3LC3T = false;
    private bool S2DLC1 = false; //기술자 바지
    private bool S2DLC2 = false;
    private bool S2DLC3 = false;
    private bool S2DLC4 = false;
    private bool S2DLC5 = false;
    private bool S2DLCC1 = false; //기술자 찢어진 바지
    private bool S2DLCC2 = false;
    private bool S2DLCC3 = false;
    private bool S2DLCC4 = false;
    private bool S2DLCC5 = false;
    private bool S2DLL1 = false; //기술자 바지 LED
    private bool S2DLL2 = false;
    private bool S2DLL3 = false;
    private bool S2DLL4 = false;
    private bool S2DLL5 = false;
    private bool S4LC1 = false; //기술자 신발
    private bool S4LC2 = false;
    private bool S4LC1T = false; //기술자 찢어진 신발
    private bool S4LC2T = false;
    private bool S3DLC1 = false; //의료원 바지
    private bool S3DLC2 = false;
    private bool S3DLC3 = false;
    private bool S3DLC4 = false;
    private bool S3DLC5 = false;
    private bool S3DLCC1 = false; //의료원 찢어진 바지
    private bool S3DLCC2 = false;
    private bool S3DLCC3 = false;
    private bool S3DLCC4 = false;
    private bool S3DLCC5 = false;
    private bool S3DLL1 = false; //의료원 바지 LED
    private bool S3DLL2 = false;
    private bool S3DLL3 = false;
    private bool S3DLL4 = false;
    private bool S3DLL5 = false;
    private bool S3DLL1T = false; //의료원 파손된 LED
    private bool S3DLL2T = false;
    private bool S3DLL3T = false;
    private bool S3DLL4T = false;
    private bool S3DLL5T = false;
    private bool S5LC1 = false; //의료원 신발
    private bool S5LC2 = false;
    private bool S5LC3 = false;
    private bool S5LC4 = false;
    private bool S5LC5 = false;
    private bool S5LC1T = false; //의료원 찢어진 신발
    private bool S5LC2T = false;
    private bool S5LC3T = false;
    private bool S5LC4T = false;
    private bool S5LC5T = false;

    //왼쪽 다리 삭제 활성화
    public bool LegLout = false;

    //오른쪽 바지
    public GameObject RightLeg;
    public Transform RightLegpos;
    public bool RightLegOUTonline = false;
    private bool P1RC1 = false; //일반 바지
    private bool P1RC2 = false;
    private bool P1RC3 = false;
    private bool P1RC4 = false;
    private bool P1RCC1 = false; //찢어진 일반 바지
    private bool P1RCC2 = false;
    private bool P1RCC3 = false;
    private bool P1RCC4 = false;
    private bool P1RL1 = false; //일반 바지 LED
    private bool P1RL2 = false;
    private bool P1RL3 = false;
    private bool P1RL4 = false;
    private bool P1RL5 = false;
    private bool P2RC1 = false; //일반 반바지
    private bool P2RC2 = false;
    private bool P2RC3 = false;
    private bool P2RC4 = false;
    private bool P2RCC1 = false; //찢어진 일반 반바지
    private bool P2RCC2 = false;
    private bool P2RCC3 = false;
    private bool P2RCC4 = false;
    private bool P2RL1 = false; //반바지 LED
    private bool P2RL2 = false;
    private bool P2RL3 = false;
    private bool P2RL4 = false;
    private bool P2RL5 = false;
    private bool S1RC1 = false; //일반 신발
    private bool S1RC2 = false;
    private bool S1RC3 = false;
    private bool S1RC4 = false;
    private bool S1RC5 = false;
    private bool S1RC1T = false; //찢어진 일반 신발
    private bool S1RC2T = false;
    private bool S1RC3T = false;
    private bool S1RC4T = false;
    private bool S1RC5T = false;
    private bool S2RC1 = false; //일반 샌들
    private bool S2RC2 = false;
    private bool S2RC3 = false;
    private bool S2RC4 = false;
    private bool S2RC5 = false;
    private bool S1DRC1 = false; //회사원 바지
    private bool S1DRC2 = false;
    private bool S1DRC3 = false;
    private bool S1DRC4 = false;
    private bool S1DRC5 = false;
    private bool S1DRCC1 = false; //회사원 찢어진 바지
    private bool S1DRCC2 = false;
    private bool S1DRCC3 = false;
    private bool S1DRCC4 = false;
    private bool S1DRCC5 = false;
    private bool S1DRL1 = false; //회사원 바지 LED
    private bool S1DRL2 = false;
    private bool S1DRL3 = false;
    private bool S1DRL4 = false;
    private bool S1DRL5 = false;
    private bool S1DRL1T = false; //회사원 파손된 LED
    private bool S1DRL2T = false;
    private bool S1DRL3T = false;
    private bool S1DRL4T = false;
    private bool S1DRL5T = false;
    private bool S3RC1 = false; //회사원 신발
    private bool S3RC2 = false;
    private bool S3RC3 = false;
    private bool S3RC1T = false; //회사원 찢어진 신발
    private bool S3RC2T = false;
    private bool S3RC3T = false;
    private bool S2DRC1 = false; //기술자 바지
    private bool S2DRC2 = false;
    private bool S2DRC3 = false;
    private bool S2DRC4 = false;
    private bool S2DRC5 = false;
    private bool S2DRCC1 = false; //기술자 찢어진 바지
    private bool S2DRCC2 = false;
    private bool S2DRCC3 = false;
    private bool S2DRCC4 = false;
    private bool S2DRCC5 = false;
    private bool S2DRL1 = false; //기술자 바지 LED
    private bool S2DRL2 = false;
    private bool S2DRL3 = false;
    private bool S2DRL4 = false;
    private bool S2DRL5 = false;
    private bool S4RC1 = false; //기술자 신발
    private bool S4RC2 = false;
    private bool S4RC3 = false;
    private bool S4RC4 = false;
    private bool S4RC1T = false; //기술자 찢어진 신발
    private bool S4RC2T = false;
    private bool S4RC3T = false;
    private bool S4RC4T = false;
    private bool S3DRC1 = false; //의료원 바지
    private bool S3DRC2 = false;
    private bool S3DRC3 = false;
    private bool S3DRC4 = false;
    private bool S3DRC5 = false;
    private bool S3DRCC1 = false; //의료원 찢어진 바지
    private bool S3DRCC2 = false;
    private bool S3DRCC3 = false;
    private bool S3DRCC4 = false;
    private bool S3DRCC5 = false;
    private bool S3DRL1 = false; //의료원 바지 LED
    private bool S3DRL2 = false;
    private bool S3DRL3 = false;
    private bool S3DRL4 = false;
    private bool S3DRL5 = false;
    private bool S5RC1 = false; //의료원 신발
    private bool S5RC2 = false;
    private bool S5RC3 = false;
    private bool S5RC4 = false;
    private bool S5RC5 = false;
    private bool S5RC1T = false; //의료원 찢어진 신발
    private bool S5RC2T = false;
    private bool S5RC3T = false;
    private bool S5RC4T = false;
    private bool S5RC5T = false;

    //오른쪽 다리 삭제 활성화
    public bool LegRout = false;

    bool Direction;
    public int LargeThrow;

    public void SetDirection(bool Boolean)
    {
        Direction = Boolean;
    }

    private void Start()
    {
        tearInfector = FindObjectOfType<TearInfector>();
    }

    public void OnEnable()
    {
        clothTear = 0;
        DownRclothTear = 0;
        DownLclothTear = 0;
        TopDownLclothTear = 0;
        TopDownRclothTear = 0;

        InitializeBody(); //스폰시 스타일 초기화
    }

    private void OnDisable()
    {
        TearingStart = false;
        HEADout = false;
        HandRout = false;
        B1LDRout = false;
        B1LURout = false;
        HandLout = false;
        B1LDLout = false;
        B1LULout = false;
        BODY1LL = false;
        BODY2LL = false;
        FOOTL = false;
        BODY1RL = false;
        BODY2RL = false;
        FOOTR = false;
        LegLout = false;
        LegRout = false;

        FACE1 = false;
        FACE2 = false;
        FACE3 = false;
        FACE4 = false;
        FACE5 = false;
        HAIR1_1 = false;
        HAIR1_2 = false;
        HAIR1_3 = false;
        HAIR1_4 = false;
        HAIR1_5 = false;
        HAIR2_1 = false;
        HAIR2_2 = false;
        HAIR2_3 = false;
        HAIR2_4 = false;
        HAIR2_5 = false;
        HAIR3_1 = false;
        HAIR3_2 = false;
        HAIR3_3 = false;
        HAIR3_4 = false;
        HAIR3_5 = false;
        HAIR4_1 = false;
        HAIR4_2 = false;
        HAIR4_3 = false;
        HAIR4_4 = false;
        HAIR4_5 = false;
        HAIR5_1 = false;
        HAIR5_2 = false;
        HAIR5_3 = false;
        HAIR5_4 = false;
        HAIR5_5 = false;
        HAIR6_1 = false;
        HAIR6_2 = false;
        HAIR6_3 = false;
        HAIR6_4 = false;
        HAIR6_5 = false;
        S3HC1 = false;
        S3HC2 = false;
        S3HC3 = false;
        S3HC4 = false;
        S3HC5 = false;

        HAND1R = false;
        HAND2R = false;
        HAND3R = false;
        G1HAND1RC1 = false;
        G1HAND2RC1 = false;
        G1HAND3RC1 = false;
        G1HAND1RC2 = false;
        G1HAND2RC2 = false;
        G1HAND3RC2 = false;
        G1HAND1RC3 = false;
        G1HAND2RC3 = false;
        G1HAND3RC3 = false;
        G1HAND1RC4 = false;
        G1HAND2RC4 = false;
        G1HAND3RC4 = false;
        G1HAND1RC5 = false;
        G1HAND2RC5 = false;
        G1HAND3RC5 = false;

        BODY1RUA = false;
        BODY2RUA = false;
        CT1RUA = false;
        CT2RA = false;
        S1TRUAC1 = false; //회사원
        S1TRUAC2 = false;
        S1TRUAC3 = false;
        S1TRUAC4 = false;
        S1TRUAC5 = false;
        S2TRUAC1 = false; //기술자
        S2TRUAC2 = false;
        S2TRUAC3 = false;
        S2TRUAC4 = false;
        S2TRUAC5 = false;
        S3TRUAC1 = false; //의료원
        S3TRUAC2 = false;
        S3TRUAC3 = false;
        S3TRUAC4 = false;
        S3TRUAC5 = false;

        BODY1RDAR = false;
        BODY2RDAR = false;
        CT1RDA = false; //일반옷
        S1TRDAC1 = false; //회사원
        S1TRDAC2 = false;
        S1TRDAC3 = false;
        S1TRDAC4 = false;
        S1TRDAC5 = false;
        S2TRDAC1 = false; //기술자
        S2TRDAC2 = false;
        S2TRDAC3 = false;
        S2TRDAC4 = false;
        S2TRDAC5 = false;
        S2TRDACC1 = false; //기술자 옷조각
        S2TRDACC2 = false;
        S2TRDACC3 = false;
        S2TRDACC4 = false;
        S2TRDACC5 = false;
        S2TRDAL1 = false; //기술자 LED
        S2TRDAL2 = false;
        S2TRDAL3 = false;
        S2TRDAL4 = false;
        S2TRDAL5 = false;
        S3TRDAC1 = false; //의료원
        S3TRDAC2 = false;
        S3TRDAC3 = false;
        S3TRDAC4 = false;
        S3TRDAC5 = false;
        S3TRDACC1 = false; //의료원 옷조각
        S3TRDACC2 = false;
        S3TRDACC3 = false;
        S3TRDACC4 = false;
        S3TRDACC5 = false;
        S3TRDAL1 = false; //의료원 LED
        S3TRDAL2 = false;
        S3TRDAL3 = false;
        S3TRDAL4 = false;
        S3TRDAL5 = false;

        HAND1L = false;
        HAND2L = false;
        HAND3L = false;
        G1HAND1LC1 = false;
        G1HAND2LC1 = false;
        G1HAND3LC1 = false;
        G1HAND1LC2 = false;
        G1HAND2LC2 = false;
        G1HAND3LC2 = false;
        G1HAND1LC3 = false;
        G1HAND2LC3 = false;
        G1HAND3LC3 = false;
        G1HAND1LC4 = false;
        G1HAND2LC4 = false;
        G1HAND3LC4 = false;
        G1HAND1LC5 = false;
        G1HAND2LC5 = false;
        G1HAND3LC5 = false;

        BODY1LDA = false;
        BODY2LDA = false;
        CT1LDA = false; //일반옷
        S1TLDAC1 = false; //회사원
        S1TLDAC2 = false;
        S1TLDAC3 = false;
        S1TLDAC4 = false;
        S1TLDAC5 = false;
        S2TLDAC1 = false; //기술자
        S2TLDAC2 = false;
        S2TLDAC3 = false;
        S2TLDAC4 = false;
        S2TLDAC5 = false;
        S2TLDACC1 = false; //기술자 옷조각
        S2TLDACC2 = false;
        S2TLDACC3 = false;
        S2TLDACC4 = false;
        S2TLDACC5 = false;
        S2TLDAL1 = false; //기술자 옷 색상
        S2TLDAL2 = false;
        S2TLDAL3 = false;
        S2TLDAL4 = false;
        S2TLDAL5 = false; //기술자 LED
        S3TLDAC1 = false;
        S3TLDAC2 = false;
        S3TLDAC3 = false;
        S3TLDAC4 = false;
        S3TLDAC5 = false;
        S3TLDACC1 = false; //의료원 옷조각
        S3TLDACC2 = false;
        S3TLDACC3 = false;
        S3TLDACC4 = false;
        S3TLDACC5 = false;
        S3TLDAL1 = false; //의료원 LED
        S3TLDAL2 = false;
        S3TLDAL3 = false;
        S3TLDAL4 = false;
        S3TLDAL5 = false;

        P1LC1 = false; //일반 바지
        P1LC2 = false;
        P1LC3 = false;
        P1LC4 = false;
        P1LCC1 = false; //찢어진 일반 바지
        P1LCC2 = false;
        P1LCC3 = false;
        P1LCC4 = false;
        P2LC1 = false; //일반 반바지
        P2LC2 = false;
        P2LC3 = false;
        P2LC4 = false;
        P2LCC1 = false; //찢어진 일반 반바지
        P2LCC2 = false;
        P2LCC3 = false;
        P2LCC4 = false;
        P2LL1 = false; //반바지 LED
        P2LL2 = false;
        P2LL3 = false;
        P2LL4 = false;
        P2LL5 = false;
        S1LC1 = false; //일반 신발
        S1LC2 = false;
        S1LC3 = false;
        S1LC4 = false;
        S1LC5 = false;
        S1LC1T = false; //찢어진 일반 신발
        S1LC2T = false;
        S1LC3T = false;
        S1LC4T = false;
        S1LC5T = false;
        S2LC1 = false; //일반 샌들
        S2LC2 = false;
        S2LC3 = false;
        S2LC4 = false;
        S2LC5 = false;
        S1DLC1 = false; //회사원 바지
        S1DLC2 = false;
        S1DLC3 = false;
        S1DLC4 = false;
        S1DLC5 = false;
        S1DLCC1 = false; //회사원 찢어진 바지
        S1DLCC2 = false;
        S1DLCC3 = false;
        S1DLCC4 = false;
        S1DLCC5 = false;
        S1DLL1 = false; //회사원 바지 LED
        S1DLL2 = false;
        S1DLL3 = false;
        S1DLL4 = false;
        S1DLL5 = false;
        S1DLL1T = false; //회사원 파손된 LED
        S1DLL2T = false;
        S1DLL3T = false;
        S1DLL4T = false;
        S1DLL5T = false;
        S3LC1 = false; //회사원 신발
        S3LC2 = false;
        S3LC3 = false;
        S3LC1T = false; //회사원 찢어진 신발
        S3LC2T = false;
        S3LC3T = false;
        S2DLC1 = false; //기술자 바지
        S2DLC2 = false;
        S2DLC3 = false;
        S2DLC4 = false;
        S2DLC5 = false;
        S2DLCC1 = false; //기술자 찢어진 바지
        S2DLCC2 = false;
        S2DLCC3 = false;
        S2DLCC4 = false;
        S2DLCC5 = false;
        S2DLL1 = false; //기술자 바지 LED
        S2DLL2 = false;
        S2DLL3 = false;
        S2DLL4 = false;
        S2DLL5 = false;
        S4LC1 = false; //기술자 신발
        S4LC2 = false;
        S4LC1T = false; //기술자 찢어진 신발
        S4LC2T = false;
        S3DLC1 = false; //의료원 바지
        S3DLC2 = false;
        S3DLC3 = false;
        S3DLC4 = false;
        S3DLC5 = false;
        S3DLCC1 = false; //의료원 찢어진 바지
        S3DLCC2 = false;
        S3DLCC3 = false;
        S3DLCC4 = false;
        S3DLCC5 = false;
        S3DLL1 = false; //의료원 바지 LED
        S3DLL2 = false;
        S3DLL3 = false;
        S3DLL4 = false;
        S3DLL5 = false;
        S3DLL1T = false; //의료원 파손된 LED
        S3DLL2T = false;
        S3DLL3T = false;
        S3DLL4T = false;
        S3DLL5T = false;
        S5LC1 = false; //의료원 신발
        S5LC2 = false;
        S5LC3 = false;
        S5LC4 = false;
        S5LC5 = false;
        S5LC1T = false; //의료원 찢어진 신발
        S5LC2T = false;
        S5LC3T = false;
        S5LC4T = false;
        S5LC5T = false;

        P1RC1 = false; //일반 바지
        P1RC2 = false;
        P1RC3 = false;
        P1RC4 = false;
        P1RCC1 = false; //찢어진 일반 바지
        P1RCC2 = false;
        P1RCC3 = false;
        P1RCC4 = false;
        P1RL1 = false; //일반 바지 LED
        P1RL2 = false;
        P1RL3 = false;
        P1RL4 = false;
        P1RL5 = false;
        P2RC1 = false; //일반 반바지
        P2RC2 = false;
        P2RC3 = false;
        P2RC4 = false;
        P2RCC1 = false; //찢어진 일반 반바지
        P2RCC2 = false;
        P2RCC3 = false;
        P2RCC4 = false;
        P2RL1 = false; //반바지 LED
        P2RL2 = false;
        P2RL3 = false;
        P2RL4 = false;
        P2RL5 = false;
        S1RC1 = false; //일반 신발
        S1RC2 = false;
        S1RC3 = false;
        S1RC4 = false;
        S1RC5 = false;
        S1RC1T = false; //찢어진 일반 신발
        S1RC2T = false;
        S1RC3T = false;
        S1RC4T = false;
        S1RC5T = false;
        S2RC1 = false; //일반 샌들
        S2RC2 = false;
        S2RC3 = false;
        S2RC4 = false;
        S2RC5 = false;
        S1DRC1 = false; //회사원 바지
        S1DRC2 = false;
        S1DRC3 = false;
        S1DRC4 = false;
        S1DRC5 = false;
        S1DRCC1 = false; //회사원 찢어진 바지
        S1DRCC2 = false;
        S1DRCC3 = false;
        S1DRCC4 = false;
        S1DRCC5 = false;
        S1DRL1 = false; //회사원 바지 LED
        S1DRL2 = false;
        S1DRL3 = false;
        S1DRL4 = false;
        S1DRL5 = false;
        S1DRL1T = false; //회사원 파손된 LED
        S1DRL2T = false;
        S1DRL3T = false;
        S1DRL4T = false;
        S1DRL5T = false;
        S3RC1 = false; //회사원 신발
        S3RC2 = false;
        S3RC3 = false;
        S3RC1T = false; //회사원 찢어진 신발
        S3RC2T = false;
        S3RC3T = false;
        S2DRC1 = false; //기술자 바지
        S2DRC2 = false;
        S2DRC3 = false;
        S2DRC4 = false;
        S2DRC5 = false;
        S2DRCC1 = false; //기술자 찢어진 바지
        S2DRCC2 = false;
        S2DRCC3 = false;
        S2DRCC4 = false;
        S2DRCC5 = false;
        S2DRL1 = false; //기술자 바지 LED
        S2DRL2 = false;
        S2DRL3 = false;
        S2DRL4 = false;
        S2DRL5 = false;
        S4RC1 = false; //기술자 신발
        S4RC2 = false;
        S4RC3 = false;
        S4RC4 = false;
        S4RC1T = false; //기술자 찢어진 신발
        S4RC2T = false;
        S4RC3T = false;
        S4RC4T = false;
        S3DRC1 = false; //의료원 바지
        S3DRC2 = false;
        S3DRC3 = false;
        S3DRC4 = false;
        S3DRC5 = false;
        S3DRCC1 = false; //의료원 찢어진 바지
        S3DRCC2 = false;
        S3DRCC3 = false;
        S3DRCC4 = false;
        S3DRCC5 = false;
        S3DRL1 = false; //의료원 바지 LED
        S3DRL2 = false;
        S3DRL3 = false;
        S3DRL4 = false;
        S3DRL5 = false;
        S5RC1 = false; //의료원 신발
        S5RC2 = false;
        S5RC3 = false;
        S5RC4 = false;
        S5RC5 = false;
        S5RC1T = false; //의료원 찢어진 신발
        S5RC2T = false;
        S5RC3T = false;
        S5RC4T = false;
        S5RC5T = false;

        HEADOUTonline = false;
        HANDROUTonline = false;
        RightArmUpOUTonline = false;
        RightArmUpDownOUTonline = false;
        RightArmUpDownHOUTonline = false;
        RightArmDownOUTonline = false;
        RightArmDownHOUTonline = false;
        HANDLOUTonline = false;
        LeftArmUpOUTonline = false;
        LeftArmUpDownOUTonline = false;
        LeftArmUpDownHOUTonline = false;
        LeftArmDownOUTonline = false;
        LeftArmDownHOUTonline = false;
        LeftLegOUTonline = false;
        RightLegOUTonline = false;

        dontGetHair = false;
        inCap = false;
    }

    public void Update()
    {
        if (TearingStart == true)
        {
            ///////////////////////////////////////////////////////// 얼굴 /////////////////////////////////////////////////////////
            if (HEADout == true)
            {
                //얼굴 삭제
                if (faceType == 0)
                {
                    GameObject f1head = transform.Find("Face1 head").gameObject;
                    GameObject f1eb = transform.Find("Face1 eyeborrow").gameObject;
                    GameObject f1e = transform.Find("Face1 eyes").gameObject;
                    GameObject f1j = transform.Find("Face1 jaw").gameObject;
                    GameObject f1rp = transform.Find("Face1 right pupli").gameObject;
                    GameObject f1lp = transform.Find("Face1 left pupli").gameObject;
                    f1head.gameObject.SetActive(false);
                    f1eb.gameObject.SetActive(false);
                    f1e.gameObject.SetActive(false);
                    f1j.gameObject.SetActive(false);
                    f1rp.gameObject.SetActive(false);
                    f1lp.gameObject.SetActive(false);

                    FACE1 = true;
                }
                else if (faceType == 1)
                {
                    GameObject f2head = transform.Find("Face2 head").gameObject;
                    GameObject f2e = transform.Find("Face2 Eye").gameObject;
                    GameObject f2p = transform.Find("Face2 pupli").gameObject;
                    GameObject f2m = transform.Find("Face2 mask").gameObject;
                    f2head.gameObject.SetActive(false);
                    f2e.gameObject.SetActive(false);
                    f2p.gameObject.SetActive(false);
                    f2m.gameObject.SetActive(false);

                    FACE2 = true;
                }
                else if (faceType == 2)
                {
                    GameObject f3head = transform.Find("Face3 head").gameObject;
                    GameObject f3eb = transform.Find("Face3 eyeborrow").gameObject;
                    GameObject f3e = transform.Find("Face3 eyes").gameObject;
                    GameObject f3tm = transform.Find("Face3 Top mouth").gameObject;
                    GameObject f3dm = transform.Find("Face3 Down mouth").gameObject;
                    GameObject f3p = transform.Find("Face3 pupil").gameObject;
                    f3head.gameObject.SetActive(false);
                    f3eb.gameObject.SetActive(false);
                    f3e.gameObject.SetActive(false);
                    f3tm.gameObject.SetActive(false);
                    f3dm.gameObject.SetActive(false);
                    f3p.gameObject.SetActive(false);

                    FACE3 = true;
                }
                else if (faceType == 3)
                {
                    GameObject f5head = transform.Find("Face5 head").gameObject;
                    GameObject f5eb = transform.Find("Face5 eyeborrow").gameObject;
                    GameObject f5e = transform.Find("Face5 Eyes").gameObject;
                    GameObject f5j = transform.Find("Face5 jaw").gameObject;
                    GameObject f5rp = transform.Find("Face5 right pupil").gameObject;
                    GameObject f5lp = transform.Find("Face5 left pupil").gameObject;
                    f5head.gameObject.SetActive(false);
                    f5eb.gameObject.SetActive(false);
                    f5e.gameObject.SetActive(false);
                    f5j.gameObject.SetActive(false);
                    f5rp.gameObject.SetActive(false);
                    f5lp.gameObject.SetActive(false);

                    FACE4 = true;
                }
                else if (faceType == 4)
                {
                    GameObject f6head = transform.Find("Face6 head").gameObject;
                    f6head.gameObject.SetActive(false);

                    FACE5 = true;
                }

                //머리카락 제거
                if (inCap == false && dontGetHair == false)
                {
                    if (hair == 0)
                    {
                        if (hairColor == 0)
                        {
                            GameObject h1_1 = transform.Find("Hair1-1").gameObject;
                            h1_1.gameObject.SetActive(false);

                            HAIR1_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h1_2 = transform.Find("Hair1-2").gameObject;
                            h1_2.gameObject.SetActive(false);

                            HAIR1_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h1_3 = transform.Find("Hair1-3").gameObject;
                            h1_3.gameObject.SetActive(false);

                            HAIR1_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h1_4 = transform.Find("Hair1-4").gameObject;
                            h1_4.gameObject.SetActive(false);

                            HAIR1_4 = true;
                        }
                        else
                        {
                            GameObject h1_5 = transform.Find("Hair1-5").gameObject;
                            h1_5.gameObject.SetActive(false);

                            HAIR1_5 = true;
                        }
                    }
                    else if (hair == 1)
                    {
                        if (hairColor == 0)
                        {
                            GameObject h2_1 = transform.Find("Hair2-1").gameObject;
                            h2_1.gameObject.SetActive(false);

                            HAIR2_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h2_2 = transform.Find("Hair2-2").gameObject;
                            h2_2.gameObject.SetActive(false);

                            HAIR2_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h2_3 = transform.Find("Hair2-3").gameObject;
                            h2_3.gameObject.SetActive(false);

                            HAIR2_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h2_4 = transform.Find("Hair2-4").gameObject;
                            h2_4.gameObject.SetActive(false);

                            HAIR2_4 = true;
                        }
                        else
                        {
                            GameObject h2_5 = transform.Find("Hair2-5").gameObject;
                            h2_5.gameObject.SetActive(false);

                            HAIR2_5 = true;
                        }
                    }
                    else if (hair == 2)
                    {
                        if (hairColor == 0)
                        {
                            GameObject h3_1 = transform.Find("Hair3-1").gameObject;
                            h3_1.gameObject.SetActive(false);

                            HAIR3_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h3_2 = transform.Find("Hair3-2").gameObject;
                            h3_2.gameObject.SetActive(false);

                            HAIR3_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h3_3 = transform.Find("Hair3-3").gameObject;
                            h3_3.gameObject.SetActive(false);

                            HAIR3_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h3_4 = transform.Find("Hair3-4").gameObject;
                            h3_4.gameObject.SetActive(false);

                            HAIR3_4 = true;
                        }
                        else
                        {
                            GameObject h3_5 = transform.Find("Hair3-5").gameObject;
                            h3_5.gameObject.SetActive(false);

                            HAIR3_5 = true;
                        }
                    }
                    else if (hair == 3)
                    {
                        if (hairColor == 0)
                        {
                            GameObject h4_1 = transform.Find("Hair4-1").gameObject;
                            h4_1.gameObject.SetActive(false);

                            HAIR4_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h4_2 = transform.Find("Hair4-2").gameObject;
                            h4_2.gameObject.SetActive(false);

                            HAIR4_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h4_3 = transform.Find("Hair4-3").gameObject;
                            h4_3.gameObject.SetActive(false);

                            HAIR4_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h4_4 = transform.Find("Hair4-4").gameObject;
                            h4_4.gameObject.SetActive(false);

                            HAIR4_4 = true;
                        }
                        else
                        {
                            GameObject h4_5 = transform.Find("Hair4-5").gameObject;
                            h4_5.gameObject.SetActive(false);

                            HAIR4_5 = true;
                        }
                    }
                    else if (hair == 4)
                    {
                        if (hairColor == 0)
                        {
                            GameObject h5_1 = transform.Find("Hair5-1").gameObject;
                            h5_1.gameObject.SetActive(false);

                            HAIR5_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h5_2 = transform.Find("Hair5-2").gameObject;
                            h5_2.gameObject.SetActive(false);

                            HAIR5_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h5_3 = transform.Find("Hair5-3").gameObject;
                            h5_3.gameObject.SetActive(false);

                            HAIR5_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h5_4 = transform.Find("Hair5-4").gameObject;
                            h5_4.gameObject.SetActive(false);

                            HAIR5_4 = true;
                        }
                        else
                        {
                            GameObject h5_5 = transform.Find("Hair5-5").gameObject;
                            h5_5.gameObject.SetActive(false);

                            HAIR5_5 = true;
                        }
                    }
                    else
                    {
                        if (hairColor == 0)
                        {
                            GameObject h6_1 = transform.Find("Hair6-1").gameObject;
                            h6_1.gameObject.SetActive(false);

                            HAIR6_1 = true;
                        }
                        else if (hairColor == 1)
                        {
                            GameObject h6_2 = transform.Find("Hair6-2").gameObject;
                            h6_2.gameObject.SetActive(false);

                            HAIR6_2 = true;
                        }
                        else if (hairColor == 2)
                        {
                            GameObject h6_3 = transform.Find("Hair6-3").gameObject;
                            h6_3.gameObject.SetActive(false);

                            HAIR6_3 = true;
                        }
                        else if (hairColor == 3)
                        {
                            GameObject h6_4 = transform.Find("Hair6-4").gameObject;
                            h6_4.gameObject.SetActive(false);

                            HAIR6_4 = true;
                        }
                        else
                        {
                            GameObject h6_5 = transform.Find("Hair6-5").gameObject;
                            h6_5.gameObject.SetActive(false);

                            HAIR6_5 = true;
                        }
                    }
                }

                //모자 제거
                if (inCap == true)
                {
                    GameObject s3hb = transform.Find("Suit3 head blood").gameObject;
                    GameObject s3hi = transform.Find("Suit3 head inner").gameObject;
                    s3hb.gameObject.SetActive(false);
                    s3hi.gameObject.SetActive(false);

                    if (suitsColor == 0)
                    {
                        GameObject s3hc1 = transform.Find("Suit3 head color1").gameObject;
                        s3hc1.gameObject.SetActive(false);

                        S3HC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s3hc2 = transform.Find("Suit3 head color2").gameObject;
                        s3hc2.gameObject.SetActive(false);

                        S3HC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s3hc3 = transform.Find("Suit3 head color3").gameObject;
                        s3hc3.gameObject.SetActive(false);

                        S3HC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s3hc4 = transform.Find("Suit3 head color4").gameObject;
                        s3hc4.gameObject.SetActive(false);

                        S3HC4 = true;
                    }
                    else
                    {
                        GameObject s3hc5 = transform.Find("Suit3 head color5").gameObject;
                        s3hc5.gameObject.SetActive(false);

                        S3HC5 = true;
                    }
                }
            }

            ///////////////////////////////////////////////////////// 오른쪽 팔 /////////////////////////////////////////////////////////
            //오른손 삭제
            if (HandRout == true)
            {
                if (fingerType == 0)
                {
                    //Debug.Log("손 삭제");
                    if (finger == 0)
                    {
                        GameObject hand1r = transform.Find("Hand1 right").gameObject;
                        hand1r.gameObject.SetActive(false);
                        HAND1R = true;
                    }
                    else if (finger == 1)
                    {
                        GameObject hand2r = transform.Find("Hand2 right").gameObject;
                        hand2r.gameObject.SetActive(false);
                        HAND2R = true;
                    }
                    else
                    {
                        GameObject hand3r = transform.Find("Hand3 right").gameObject;
                        hand3r.gameObject.SetActive(false);
                        HAND3R = true;
                    }
                }
                else if (fingerType == 1)
                {
                    //Debug.Log("손 삭제");
                    if (fingerColor == 1)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1rc1 = transform.Find("Grove1 right hand1 color1").gameObject;
                            g1hand1rc1.gameObject.SetActive(false);
                            G1HAND1RC1 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2rc1 = transform.Find("Grove1 right hand2 color1").gameObject;
                            g1hand2rc1.gameObject.SetActive(false);
                            G1HAND2RC1 = true;
                        }
                        else
                        {
                            GameObject g1hand3rc1 = transform.Find("Grove1 right hand3 color1").gameObject;
                            g1hand3rc1.gameObject.SetActive(false);
                            G1HAND3RC1 = true;
                        }
                    }
                    else if (fingerColor == 2)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1rc2 = transform.Find("Grove1 right hand1 color2").gameObject;
                            g1hand1rc2.gameObject.SetActive(false);
                            G1HAND1RC2 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2rc2 = transform.Find("Grove1 right hand2 color2").gameObject;
                            g1hand2rc2.gameObject.SetActive(false);
                            G1HAND2RC2 = true;
                        }
                        else
                        {
                            GameObject g1hand3rc2 = transform.Find("Grove1 right hand3 color2").gameObject;
                            g1hand3rc2.gameObject.SetActive(false);
                            G1HAND3RC2 = true;
                        }
                    }
                    else if (fingerColor == 3)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1rc3 = transform.Find("Grove1 right hand1 color3").gameObject;
                            g1hand1rc3.gameObject.SetActive(false);
                            G1HAND1RC3 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2rc3 = transform.Find("Grove1 right hand2 color3").gameObject;
                            g1hand2rc3.gameObject.SetActive(false);
                            G1HAND2RC3 = true;
                        }
                        else
                        {
                            GameObject g1hand3rc3 = transform.Find("Grove1 right hand3 color3").gameObject;
                            g1hand3rc3.gameObject.SetActive(false);
                            G1HAND3RC3 = true;
                        }
                    }
                    else if (fingerColor == 4)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1rc4 = transform.Find("Grove1 right hand1 color4").gameObject;
                            g1hand1rc4.gameObject.SetActive(false);
                            G1HAND1RC4 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2rc4 = transform.Find("Grove1 right hand2 color4").gameObject;
                            g1hand2rc4.gameObject.SetActive(false);
                            G1HAND2RC4 = true;
                        }
                        else
                        {
                            GameObject g1hand3rc4 = transform.Find("Grove1 right hand3 color4").gameObject;
                            g1hand3rc4.gameObject.SetActive(false);
                            G1HAND3RC4 = true;
                        }
                    }
                    else if (fingerColor == 5)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1rc5 = transform.Find("Grove1 right hand1 color5").gameObject;
                            g1hand1rc5.gameObject.SetActive(false);
                            G1HAND1RC5 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2rc5 = transform.Find("Grove1 right hand2 color5").gameObject;
                            g1hand2rc5.gameObject.SetActive(false);
                            G1HAND2RC5 = true;
                        }
                        else
                        {
                            GameObject g1hand3rc5 = transform.Find("Grove1 right hand3 color5").gameObject;
                            g1hand3rc5.gameObject.SetActive(false);
                            G1HAND3RC5 = true;
                        }
                    }
                }

                //오른팔 하단 삭제
                if (B1LDRout == true)
                {
                    if (body == 0)
                    {
                        GameObject body1rdr = transform.Find("Body1 right down arm").gameObject;
                        body1rdr.gameObject.SetActive(false);
                        BODY1RDAR = true;
                    }
                    else
                    {
                        GameObject body2rdr = transform.Find("Body2 right down arm").gameObject;
                        body2rdr.gameObject.SetActive(false);
                        BODY2RDAR = true;
                    }

                    if (job <= 40 && topClothes == 0)
                    {
                        GameObject ct1rda = transform.Find("Clothes top1 right down arm").gameObject;
                        ct1rda.gameObject.SetActive(false);
                        CT1RDA = true;
                    }

                    //회사원
                    else if (job > 40 && job <= 60)
                    {
                        if (suitsColor == 0)
                        {
                            GameObject s1trdac1 = transform.Find("Suit1 top right down arm color1").gameObject;
                            s1trdac1.gameObject.SetActive(false);
                            S1TRDAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s1trdac2 = transform.Find("Suit1 top right down arm color2").gameObject;
                            s1trdac2.gameObject.SetActive(false);
                            S1TRDAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s1trdac3 = transform.Find("Suit1 top right down arm color3").gameObject;
                            s1trdac3.gameObject.SetActive(false);
                            S1TRDAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s1trdac4 = transform.Find("Suit1 top right down arm color4").gameObject;
                            s1trdac4.gameObject.SetActive(false);
                            S1TRDAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s1trdac5 = transform.Find("Suit1 top right down arm color5").gameObject;
                            s1trdac5.gameObject.SetActive(false);
                            S1TRDAC5 = true;
                        }
                    }

                    //기술자
                    else if (job > 60 && job <= 80)
                    {
                        if (suitsColor == 0)
                        {
                            GameObject s2trdac1 = transform.Find("Suit2 top right down arm color1").gameObject;
                            s2trdac1.gameObject.SetActive(false);
                            S2TRDAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s2trdac2 = transform.Find("Suit2 top right down arm color2").gameObject;
                            s2trdac2.gameObject.SetActive(false);
                            S2TRDAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s2trdac3 = transform.Find("Suit2 top right down arm color3").gameObject;
                            s2trdac3.gameObject.SetActive(false);
                            S2TRDAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s2trdac4 = transform.Find("Suit2 top right down arm color4").gameObject;
                            s2trdac4.gameObject.SetActive(false);
                            S2TRDAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s2trdac5 = transform.Find("Suit2 top right down arm color5").gameObject;
                            s2trdac5.gameObject.SetActive(false);
                            S2TRDAC5 = true;
                        }

                        if (LED == 0)
                        {
                            if (LEDcolor == 0)
                            {
                                GameObject s2trdaL1 = transform.Find("Suit2 top right down arm LED1").gameObject;
                                s2trdaL1.gameObject.SetActive(false);
                                S2TRDAL1 = true;
                            }
                            else if (LEDcolor == 1)
                            {
                                GameObject s2trdaL2 = transform.Find("Suit2 top right down arm LED2").gameObject;
                                s2trdaL2.gameObject.SetActive(false);
                                S2TRDAL2 = true;
                            }
                            else if (LEDcolor == 2)
                            {
                                GameObject s2trdaL3 = transform.Find("Suit2 top right down arm LED3").gameObject;
                                s2trdaL3.gameObject.SetActive(false);
                                S2TRDAL3 = true;
                            }
                            else if (LEDcolor == 3)
                            {
                                GameObject s2trdaL4 = transform.Find("Suit2 top right down arm LED4").gameObject;
                                s2trdaL4.gameObject.SetActive(false);
                                S2TRDAL4 = true;
                            }
                            else if (LEDcolor == 4)
                            {
                                GameObject s2trdaL5 = transform.Find("Suit2 top right down arm LED5").gameObject;
                                s2trdaL5.gameObject.SetActive(false);
                                S2TRDAL5 = true;
                            }
                        }
                    }
                    //의료원
                    else if (job > 80 && job <= 100)
                    {
                        if (clothTear == 1 || TopDownRclothTear == 1)
                        {
                            GameObject s3trdacc1 = transform.Find("Suit3 top right down arm cloth color1").gameObject;
                            s3trdacc1.gameObject.SetActive(false);
                            S3TRDACC1 = true;
                        }
                        else if (clothTear == 2 || TopDownRclothTear == 2)
                        {
                            GameObject s3trdacc2 = transform.Find("Suit3 top right down arm cloth color2").gameObject;
                            s3trdacc2.gameObject.SetActive(false);
                            S3TRDACC2 = true;
                        }
                        else if (clothTear == 3 || TopDownRclothTear == 3)
                        {
                            GameObject s3trdacc3 = transform.Find("Suit3 top right down arm cloth color3").gameObject;
                            s3trdacc3.gameObject.SetActive(false);
                            S3TRDACC3 = true;
                        }
                        else if (clothTear == 4 || TopDownRclothTear == 4)
                        {
                            GameObject s3trdacc4 = transform.Find("Suit3 top right down arm cloth color4").gameObject;
                            s3trdacc4.gameObject.SetActive(false);
                            S3TRDACC4 = true;
                        }
                        else if (clothTear == 5 || TopDownRclothTear == 5)
                        {
                            GameObject s3trdacc5 = transform.Find("Suit3 top right down arm cloth color5").gameObject;
                            s3trdacc5.gameObject.SetActive(false);
                            S3TRDACC5 = true;
                        }

                        if (suitsColor == 0)
                        {
                            GameObject s3trdac1 = transform.Find("Suit3 top right down arm color1").gameObject;
                            s3trdac1.gameObject.SetActive(false);
                            S3TRDAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s3trdac2 = transform.Find("Suit3 top right down arm color2").gameObject;
                            s3trdac2.gameObject.SetActive(false);
                            S3TRDAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s3trdac3 = transform.Find("Suit3 top right down arm color3").gameObject;
                            s3trdac3.gameObject.SetActive(false);
                            S3TRDAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s3trdac4 = transform.Find("Suit3 top right down arm color4").gameObject;
                            s3trdac4.gameObject.SetActive(false);
                            S3TRDAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s3trdac5 = transform.Find("Suit3 top right down arm color5").gameObject;
                            s3trdac5.gameObject.SetActive(false);
                            S3TRDAC5 = true;
                        }

                        if (LED == 0)
                        {
                            if (LEDcolor == 0)
                            {
                                GameObject s3trdaL1 = transform.Find("Suit3 top right down arm LED1").gameObject;
                                s3trdaL1.gameObject.SetActive(false);
                                S3TRDAL1 = true;
                            }
                            else if (LEDcolor == 1)
                            {
                                GameObject s3trdaL2 = transform.Find("Suit3 top right down arm LED2").gameObject;
                                s3trdaL2.gameObject.SetActive(false);
                                S3TRDAL2 = true;
                            }
                            else if (LEDcolor == 2)
                            {
                                GameObject s3trdaL3 = transform.Find("Suit3 top right down arm LED3").gameObject;
                                s3trdaL3.gameObject.SetActive(false);
                                S3TRDAL3 = true;
                            }
                            else if (LEDcolor == 3)
                            {
                                GameObject s3trdaL4 = transform.Find("Suit3 top right down arm LED4").gameObject;
                                s3trdaL4.gameObject.SetActive(false);
                                S3TRDAL4 = true;
                            }
                            else if (LEDcolor == 4)
                            {
                                GameObject s3trdaL5 = transform.Find("Suit3 top right down arm LED5").gameObject;
                                s3trdaL5.gameObject.SetActive(false);
                                S3TRDAL5 = true;
                            }
                        }
                    }
                }

                //오른팔 상단 삭제
                if (B1LURout == true)
                {
                    if (body == 0)
                    {
                        GameObject body1rua = transform.Find("Body1 right up arm").gameObject;
                        body1rua.gameObject.SetActive(false);
                        BODY1RUA = true;
                    }
                    else
                    {
                        GameObject body2rua = transform.Find("Body2 right up arm").gameObject;
                        body2rua.gameObject.SetActive(false);
                        BODY2RUA = true;
                    }

                    if (job <= 40)
                    {
                        if (topClothes == 0)
                        {
                            GameObject ct1rua = transform.Find("Clothes top1 right up arm").gameObject;
                            ct1rua.gameObject.SetActive(false);
                            CT1RUA = true;
                        }
                        else
                        {
                            GameObject ct2ra = transform.Find("Clothes top2 right arm").gameObject;
                            ct2ra.gameObject.SetActive(false);
                            CT2RA = true;
                        }
                    }

                    //회사원
                    else if (job > 40 && job <= 60)
                    {
                        if (suitsColor == 0)
                        {
                            GameObject s1truac1 = transform.Find("Suit1 top right up arm color1").gameObject;
                            s1truac1.gameObject.SetActive(false);
                            S1TRUAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s1truac2 = transform.Find("Suit1 top right up arm color2").gameObject;
                            s1truac2.gameObject.SetActive(false);
                            S1TRUAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s1truac3 = transform.Find("Suit1 top right up arm color3").gameObject;
                            s1truac3.gameObject.SetActive(false);
                            S1TRUAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s1truac4 = transform.Find("Suit1 top right up arm color4").gameObject;
                            s1truac4.gameObject.SetActive(false);
                            S1TRUAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s1truac5 = transform.Find("Suit1 top right up arm color5").gameObject;
                            s1truac5.gameObject.SetActive(false);
                            S1TRUAC5 = true;
                        }
                    }

                    //기술자
                    else if (job > 60 && job <= 80)
                    {
                        if (suitsColor == 0)
                        {
                            GameObject s2truac1 = transform.Find("Suit2 top right up arm color1").gameObject;
                            s2truac1.gameObject.SetActive(false);
                            S2TRUAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s2trdac2 = transform.Find("Suit2 top right up arm color2").gameObject;
                            s2trdac2.gameObject.SetActive(false);
                            S2TRUAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s2truac3 = transform.Find("Suit2 top right up arm color3").gameObject;
                            s2truac3.gameObject.SetActive(false);
                            S2TRUAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s2truac4 = transform.Find("Suit2 top right up arm color4").gameObject;
                            s2truac4.gameObject.SetActive(false);
                            S2TRUAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s2truac5 = transform.Find("Suit2 top right up arm color5").gameObject;
                            s2truac5.gameObject.SetActive(false);
                            S2TRUAC5 = true;
                        }
                    }
                    //의료원
                    else if (job > 80 && job <= 100)
                    {
                        if (suitsColor == 0)
                        {
                            GameObject s3truac1 = transform.Find("Suit3 top right up arm color1").gameObject;
                            s3truac1.gameObject.SetActive(false);
                            S3TRUAC1 = true;
                        }
                        else if (suitsColor == 1)
                        {
                            GameObject s3truac2 = transform.Find("Suit3 top right up arm color2").gameObject;
                            s3truac2.gameObject.SetActive(false);
                            S3TRUAC2 = true;
                        }
                        else if (suitsColor == 2)
                        {
                            GameObject s3truac3 = transform.Find("Suit3 top right up arm color3").gameObject;
                            s3truac3.gameObject.SetActive(false);
                            S3TRUAC3 = true;
                        }
                        else if (suitsColor == 3)
                        {
                            GameObject s3truac4 = transform.Find("Suit3 top right up arm color4").gameObject;
                            s3truac4.gameObject.SetActive(false);
                            S3TRUAC4 = true;
                        }
                        else if (suitsColor == 4)
                        {
                            GameObject s3truac5 = transform.Find("Suit3 top right up arm color5").gameObject;
                            s3truac5.gameObject.SetActive(false);
                            S3TRUAC5 = true;
                        }
                    }
                }
            }
            ///////////////////////////////////////////////////////// 오른쪽 팔 /////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////// 왼쪽 팔 /////////////////////////////////////////////////////////
            //왼손 삭제
            if (HandLout == true)
            {
                if (fingerType == 0)
                {
                    if (finger == 0)
                    {
                        GameObject hand1l = transform.Find("Hand1 left").gameObject;
                        hand1l.gameObject.SetActive(false);
                        HAND1L = true;
                    }
                    else if (finger == 1)
                    {
                        GameObject hand2l = transform.Find("Hand2 left").gameObject;
                        hand2l.gameObject.SetActive(false);
                        HAND2L = true;
                    }
                    else
                    {
                        GameObject hand3l = transform.Find("Hand3 left").gameObject;
                        hand3l.gameObject.SetActive(false);
                        HAND3L = true;
                    }
                }
                else if (fingerType == 1)
                {
                    if (fingerColor == 1)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1lc1 = transform.Find("Grove1 left hand1 color1").gameObject;
                            g1hand1lc1.gameObject.SetActive(false);
                            G1HAND1LC1 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2lc1 = transform.Find("Grove1 left hand2 color1").gameObject;
                            g1hand2lc1.gameObject.SetActive(false);
                            G1HAND2LC1 = true;
                        }
                        else
                        {
                            GameObject g1hand3lc1 = transform.Find("Grove1 left hand3 color1").gameObject;
                            g1hand3lc1.gameObject.SetActive(false);
                            G1HAND3LC1 = true;
                        }
                    }
                    else if (fingerColor == 2)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1lc2 = transform.Find("Grove1 left hand1 color2").gameObject;
                            g1hand1lc2.gameObject.SetActive(false);
                            G1HAND1LC2 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2lc2 = transform.Find("Grove1 left hand2 color2").gameObject;
                            g1hand2lc2.gameObject.SetActive(false);
                            G1HAND2LC2 = true;
                        }
                        else
                        {
                            GameObject g1hand3lc2 = transform.Find("Grove1 left hand3 color2").gameObject;
                            g1hand3lc2.gameObject.SetActive(false);
                            G1HAND3LC2 = true;
                        }
                    }
                    else if (fingerColor == 3)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1lc3 = transform.Find("Grove1 left hand1 color3").gameObject;
                            g1hand1lc3.gameObject.SetActive(false);
                            G1HAND1LC3 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2lc3 = transform.Find("Grove1 left hand2 color3").gameObject;
                            g1hand2lc3.gameObject.SetActive(false);
                            G1HAND2LC3 = true;
                        }
                        else
                        {
                            GameObject g1hand3lc3 = transform.Find("Grove1 left hand3 color3").gameObject;
                            g1hand3lc3.gameObject.SetActive(false);
                            G1HAND3LC3 = true;
                        }
                    }
                    else if (fingerColor == 4)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1lc4 = transform.Find("Grove1 left hand1 color4").gameObject;
                            g1hand1lc4.gameObject.SetActive(false);
                            G1HAND1LC4 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2lc4 = transform.Find("Grove1 left hand2 color4").gameObject;
                            g1hand2lc4.gameObject.SetActive(false);
                            G1HAND2LC4 = true;
                        }
                        else
                        {
                            GameObject g1hand3lc4 = transform.Find("Grove1 left hand3 color4").gameObject;
                            g1hand3lc4.gameObject.SetActive(false);
                            G1HAND3LC4 = true;
                        }
                    }
                    else if (fingerColor == 5)
                    {
                        if (Glovefinger == 0)
                        {
                            GameObject g1hand1lc5 = transform.Find("Grove1 left hand1 color5").gameObject;
                            g1hand1lc5.gameObject.SetActive(false);
                            G1HAND1LC5 = true;
                        }
                        else if (Glovefinger == 1)
                        {
                            GameObject g1hand2lc5 = transform.Find("Grove1 left hand2 color5").gameObject;
                            g1hand2lc5.gameObject.SetActive(false);
                            G1HAND2LC5 = true;
                        }
                        else
                        {
                            GameObject g1hand3lc5 = transform.Find("Grove1 left hand3 color5").gameObject;
                            g1hand3lc5.gameObject.SetActive(false);
                            G1HAND3LC5 = true;
                        }
                    }
                }
            }

            //왼팔 하단 삭제
            if (B1LDLout == true)
            {
                if (body == 0)
                {
                    GameObject body1lda = transform.Find("Body1 left down arm").gameObject;
                    body1lda.gameObject.SetActive(false);
                    BODY1LDA = true;
                }
                else
                {
                    GameObject body2lda = transform.Find("Body2 left down arm").gameObject;
                    body2lda.gameObject.SetActive(false);
                    BODY2LDA = true;
                }

                if (job <= 40 && topClothes == 0)
                {
                    GameObject ct1lda = transform.Find("Clothes top1 left down arm").gameObject;
                    ct1lda.gameObject.SetActive(false);
                    CT1LDA = true;
                }

                //회사원
                else if (job > 40 && job <= 60)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s1tldac1 = transform.Find("Suit1 top left down arm color1").gameObject;
                        s1tldac1.gameObject.SetActive(false);
                        S1TLDAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s1tldac2 = transform.Find("Suit1 top left down arm color2").gameObject;
                        s1tldac2.gameObject.SetActive(false);
                        S1TLDAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s1tldac3 = transform.Find("Suit1 top left down arm color3").gameObject;
                        s1tldac3.gameObject.SetActive(false);
                        S1TLDAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s1tldac4 = transform.Find("Suit1 top left down arm color4").gameObject;
                        s1tldac4.gameObject.SetActive(false);
                        S1TLDAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s1tldac5 = transform.Find("Suit1 top left down arm color5").gameObject;
                        s1tldac5.gameObject.SetActive(false);
                        S1TLDAC5 = true;
                    }
                }

                //기술자
                else if (job > 60 && job <= 80)
                {
                    if (clothTear == 1 || TopDownLclothTear == 1)
                    {
                        GameObject s2tldacc1 = transform.Find("Suit2 top left down arm cloth color1").gameObject;
                        s2tldacc1.gameObject.SetActive(false);
                        S2TLDACC1 = true;
                    }
                    else if (clothTear == 2 || TopDownLclothTear == 2)
                    {
                        GameObject s2tldacc2 = transform.Find("Suit2 top left down arm cloth color2").gameObject;
                        s2tldacc2.gameObject.SetActive(false);
                        S2TLDACC2 = true;
                    }
                    else if (clothTear == 3 || TopDownLclothTear == 3)
                    {
                        GameObject s2tldacc3 = transform.Find("Suit2 top left down arm cloth color3").gameObject;
                        s2tldacc3.gameObject.SetActive(false);
                        S2TLDACC3 = true;
                    }
                    else if (clothTear == 4 || TopDownLclothTear == 4)
                    {
                        GameObject s2tldacc4 = transform.Find("Suit2 top left down arm cloth color4").gameObject;
                        s2tldacc4.gameObject.SetActive(false);
                        S2TLDACC4 = true;
                    }
                    else if (clothTear == 5 || TopDownLclothTear == 5)
                    {
                        GameObject s2tldacc5 = transform.Find("Suit2 top left down arm cloth color5").gameObject;
                        s2tldacc5.gameObject.SetActive(false);
                        S2TLDACC5 = true;
                    }

                    if (suitsColor == 0)
                    {
                        GameObject s2tldac1 = transform.Find("Suit2 top left down arm color1").gameObject;
                        s2tldac1.gameObject.SetActive(false);
                        S2TLDAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s2tldac2 = transform.Find("Suit2 top left down arm color2").gameObject;
                        s2tldac2.gameObject.SetActive(false);
                        S2TLDAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s2tldac3 = transform.Find("Suit2 top left down arm color3").gameObject;
                        s2tldac3.gameObject.SetActive(false);
                        S2TLDAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s2tldac4 = transform.Find("Suit2 top left down arm color4").gameObject;
                        s2tldac4.gameObject.SetActive(false);
                        S2TLDAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s2tldac5 = transform.Find("Suit2 top left down arm color5").gameObject;
                        s2tldac5.gameObject.SetActive(false);
                        S2TLDAC5 = true;
                    }

                    if (LED == 0)
                    {
                        if (LEDcolor == 0)
                        {
                            GameObject s2tldaL1 = transform.Find("Suit2 top left down arm LED1").gameObject;
                            s2tldaL1.gameObject.SetActive(false);
                            S2TLDAL1 = true;
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject s2tldaL2 = transform.Find("Suit2 top left down arm LED2").gameObject;
                            s2tldaL2.gameObject.SetActive(false);
                            S2TLDAL2 = true;
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject s2tldaL3 = transform.Find("Suit2 top left down arm LED3").gameObject;
                            s2tldaL3.gameObject.SetActive(false);
                            S2TLDAL3 = true;
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject s2tldaL4 = transform.Find("Suit2 top left down arm LED4").gameObject;
                            s2tldaL4.gameObject.SetActive(false);
                            S2TLDAL4 = true;
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject s2tldaL5 = transform.Find("Suit2 top left down arm LED5").gameObject;
                            s2tldaL5.gameObject.SetActive(false);
                            S2TLDAL5 = true;
                        }
                    }
                }
                //의료원
                else if (job > 80 && job <= 100)
                {
                    if (clothTear == 1 || TopDownLclothTear == 1)
                    {
                        GameObject s3tldacc1 = transform.Find("Suit3 top left down arm cloth color1").gameObject;
                        s3tldacc1.gameObject.SetActive(false);
                        S3TLDACC1 = true;
                    }
                    else if (clothTear == 2 || TopDownLclothTear == 2)
                    {
                        GameObject s3tldacc2 = transform.Find("Suit3 top left down arm cloth color2").gameObject;
                        s3tldacc2.gameObject.SetActive(false);
                        S3TLDACC2 = true;
                    }
                    else if (clothTear == 3 || TopDownLclothTear == 3)
                    {
                        GameObject s3tldacc3 = transform.Find("Suit3 top left down arm cloth color3").gameObject;
                        s3tldacc3.gameObject.SetActive(false);
                        S3TLDACC3 = true;
                    }
                    else if (clothTear == 4 || TopDownLclothTear == 4)
                    {
                        GameObject s3tldacc4 = transform.Find("Suit3 top left down arm cloth color4").gameObject;
                        s3tldacc4.gameObject.SetActive(false);
                        S3TLDACC4 = true;
                    }
                    else if (clothTear == 5 || TopDownLclothTear == 5)
                    {
                        GameObject s3tldacc5 = transform.Find("Suit3 top left down arm cloth color5").gameObject;
                        s3tldacc5.gameObject.SetActive(false);
                        S3TLDACC5 = true;
                    }

                    if (suitsColor == 0)
                    {
                        GameObject s3tldac1 = transform.Find("Suit3 top left down arm color1").gameObject;
                        s3tldac1.gameObject.SetActive(false);
                        S3TLDAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s3tldac2 = transform.Find("Suit3 top left down arm color2").gameObject;
                        s3tldac2.gameObject.SetActive(false);
                        S3TLDAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s3tldac3 = transform.Find("Suit3 top left down arm color3").gameObject;
                        s3tldac3.gameObject.SetActive(false);
                        S3TLDAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s3tldac4 = transform.Find("Suit3 top left down arm color4").gameObject;
                        s3tldac4.gameObject.SetActive(false);
                        S3TLDAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s3tldac5 = transform.Find("Suit3 top left down arm color5").gameObject;
                        s3tldac5.gameObject.SetActive(false);
                        S3TLDAC5 = true;
                    }

                    if (LED == 0)
                    {
                        if (LEDcolor == 0)
                        {
                            GameObject s3tldaL1 = transform.Find("Suit3 top left down arm LED1").gameObject;
                            s3tldaL1.gameObject.SetActive(false);
                            S3TLDAL1 = true;
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject s3tldaL2 = transform.Find("Suit3 top left down arm LED2").gameObject;
                            s3tldaL2.gameObject.SetActive(false);
                            S3TLDAL2 = true;
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject s3tldaL3 = transform.Find("Suit3 top left down arm LED3").gameObject;
                            s3tldaL3.gameObject.SetActive(false);
                            S3TLDAL3 = true;
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject s3tldaL4 = transform.Find("Suit3 top left down arm LED4").gameObject;
                            s3tldaL4.gameObject.SetActive(false);
                            S3TLDAL4 = true;
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject s3tldaL5 = transform.Find("Suit3 top left down arm LED5").gameObject;
                            s3tldaL5.gameObject.SetActive(false);
                            S3TLDAL5 = true;
                        }
                    }
                }
            }

            //왼팔 상단 삭제
            if (B1LULout == true)
            {
                if (body == 0)
                {
                    GameObject body1lua = transform.Find("Body1 left up arm").gameObject;
                    body1lua.gameObject.SetActive(false);
                    BODY1LUA = true;
                }
                else
                {
                    GameObject body2lua = transform.Find("Body2 left up arm").gameObject;
                    body2lua.gameObject.SetActive(false);
                    BODY2LUA = true;
                }

                if (job <= 40)
                {
                    if (topClothes == 0)
                    {
                        GameObject ct1lua = transform.Find("Clothes top1 left up arm").gameObject;
                        ct1lua.gameObject.SetActive(false);
                        CT1LUA = true;
                    }
                    else
                    {
                        GameObject ct2la = transform.Find("Clothes top2 left arm").gameObject;
                        ct2la.gameObject.SetActive(false);
                        CT2LA = true;
                    }
                }

                //회사원
                else if (job > 40 && job <= 60)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s1tluac1 = transform.Find("Suit1 top left up arm color1").gameObject;
                        s1tluac1.gameObject.SetActive(false);
                        S1TLUAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s1tluac2 = transform.Find("Suit1 top left up arm color2").gameObject;
                        s1tluac2.gameObject.SetActive(false);
                        S1TLUAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s1tluac3 = transform.Find("Suit1 top left up arm color3").gameObject;
                        s1tluac3.gameObject.SetActive(false);
                        S1TLUAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s1tluac4 = transform.Find("Suit1 top left up arm color4").gameObject;
                        s1tluac4.gameObject.SetActive(false);
                        S1TLUAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s1tluac5 = transform.Find("Suit1 top left up arm color5").gameObject;
                        s1tluac5.gameObject.SetActive(false);
                        S1TLUAC5 = true;
                    }
                }

                //기술자
                else if (job > 60 && job <= 80)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s2tluac1 = transform.Find("Suit2 top left up arm color1").gameObject;
                        s2tluac1.gameObject.SetActive(false);
                        S2TLUAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s2tldac2 = transform.Find("Suit2 top left up arm color2").gameObject;
                        s2tldac2.gameObject.SetActive(false);
                        S2TLUAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s2tluac3 = transform.Find("Suit2 top left up arm color3").gameObject;
                        s2tluac3.gameObject.SetActive(false);
                        S2TLUAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s2tluac4 = transform.Find("Suit2 top left up arm color4").gameObject;
                        s2tluac4.gameObject.SetActive(false);
                        S2TLUAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s2tluac5 = transform.Find("Suit2 top left up arm color5").gameObject;
                        s2tluac5.gameObject.SetActive(false);
                        S2TLUAC5 = true;
                    }
                }
                //의료원
                else if (job > 80 && job <= 100)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s3tluac1 = transform.Find("Suit3 top left up arm color1").gameObject;
                        s3tluac1.gameObject.SetActive(false);
                        S3TLUAC1 = true;
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s3tluac2 = transform.Find("Suit3 top left up arm color2").gameObject;
                        s3tluac2.gameObject.SetActive(false);
                        S3TLUAC2 = true;
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s3tluac3 = transform.Find("Suit3 top left up arm color3").gameObject;
                        s3tluac3.gameObject.SetActive(false);
                        S3TLUAC3 = true;
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s3tluac4 = transform.Find("Suit3 top left up arm color4").gameObject;
                        s3tluac4.gameObject.SetActive(false);
                        S3TLUAC4 = true;
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s3tluac5 = transform.Find("Suit3 top left up arm color5").gameObject;
                        s3tluac5.gameObject.SetActive(false);
                        S3TLUAC5 = true;
                    }
                }
            }
            ///////////////////////////////////////////////////////// 왼쪽 팔 /////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////// 왼쪽 다리 /////////////////////////////////////////////////////////
            if (LegLout == true)
            {
                if (body == 0)
                {
                    GameObject body1ll = transform.Find("Body1 left leg").gameObject;
                    GameObject footL = transform.Find("Left foot1").gameObject;
                    body1ll.gameObject.SetActive(false);
                    footL.gameObject.SetActive(false);
                    BODY1LL = true;
                    FOOTL = true;
                }
                else
                {
                    GameObject body2ll = transform.Find("Body2 left leg").gameObject;
                    GameObject footL = transform.Find("Left foot1").gameObject;
                    body2ll.gameObject.SetActive(false);
                    footL.gameObject.SetActive(false);
                    BODY2LL = true;
                    FOOTL = true;
                }

                //일반인
                if (job <= 40)
                {
                    if (pants == 0)
                    {
                        if (pantsColor == 0)
                        {
                            GameObject p1lc1 = transform.Find("Pants1 left color1").gameObject;
                            p1lc1.gameObject.SetActive(false);
                            P1LC1 = true;
                            GameObject p1lcc1 = transform.Find("Pants1 left cloth color1").gameObject;
                            p1lcc1.gameObject.SetActive(false);
                            P1LCC1 = true;
                        }
                        else if (pantsColor == 1)
                        {
                            GameObject p1lc2 = transform.Find("Pants1 left color2").gameObject;
                            p1lc2.gameObject.SetActive(false);
                            P1LC2 = true;
                            GameObject p1lcc2 = transform.Find("Pants1 left cloth color2").gameObject;
                            p1lcc2.gameObject.SetActive(false);
                            P1LCC2 = true;
                        }
                        else if (pantsColor == 2)
                        {
                            GameObject p1lc3 = transform.Find("Pants1 left color3").gameObject;
                            p1lc3.gameObject.SetActive(false);
                            P1LC3 = true;
                            GameObject p1lcc3 = transform.Find("Pants1 left cloth color3").gameObject;
                            p1lcc3.gameObject.SetActive(false);
                            P1LCC3 = true;
                        }
                        else
                        {
                            GameObject p1lc4 = transform.Find("Pants1 left color4").gameObject;
                            p1lc4.gameObject.SetActive(false);
                            P1LC4 = true;
                            GameObject p1lcc4 = transform.Find("Pants1 left cloth color4").gameObject;
                            p1lcc4.gameObject.SetActive(false);
                            P1LCC4 = true;
                        }
                    }
                    else
                    {
                        if (pantsColor == 0)
                        {
                            GameObject p2lc1 = transform.Find("Pants2 left color1").gameObject;
                            p2lc1.gameObject.SetActive(false);
                            P2LC1 = true;
                            GameObject p2lcc1 = transform.Find("Pants2 left cloth color1").gameObject;
                            p2lcc1.gameObject.SetActive(false);
                            P2LCC1 = true;
                        }
                        else if (pantsColor == 1)
                        {
                            GameObject p2lc2 = transform.Find("Pants2 left color2").gameObject;
                            p2lc2.gameObject.SetActive(false);
                            P2LC2 = true;
                            GameObject p2lcc2 = transform.Find("Pants2 left cloth color2").gameObject;
                            p2lcc2.gameObject.SetActive(false);
                            P2LCC2 = true;
                        }
                        else if (pantsColor == 2)
                        {
                            GameObject p2lc3 = transform.Find("Pants2 left color3").gameObject;
                            p2lc3.gameObject.SetActive(false);
                            P2LC3 = true;
                            GameObject p2lcc3 = transform.Find("Pants2 left cloth color3").gameObject;
                            p2lcc3.gameObject.SetActive(false);
                            P2LCC3 = true;
                        }
                        else
                        {
                            GameObject p2lc4 = transform.Find("Pants2 left color4").gameObject;
                            p2lc4.gameObject.SetActive(false);
                            P2LC4 = true;
                            GameObject p2lcc4 = transform.Find("Pants2 left cloth color4").gameObject;
                            p2lcc4.gameObject.SetActive(false);
                            P2LCC4 = true;
                        }

                        if (LEDL == 0)
                        {
                            if (LEDcolorL == 0)
                            {
                                GameObject p2lL1 = transform.Find("Pants2 left LED1").gameObject;
                                p2lL1.gameObject.SetActive(false);
                                P2LL1 = true;
                            }
                            else if (LEDcolorL == 1)
                            {
                                GameObject p2lL2 = transform.Find("Pants2 left LED2").gameObject;
                                p2lL2.gameObject.SetActive(false);
                                P2LL2 = true;
                            }
                            else if (LEDcolorL == 2)
                            {
                                GameObject p2lL3 = transform.Find("Pants2 left LED3").gameObject;
                                p2lL3.gameObject.SetActive(false);
                                P2LL3 = true;
                            }
                            else if (LEDcolorL == 3)
                            {
                                GameObject p2lL4 = transform.Find("Pants2 left LED4").gameObject;
                                p2lL4.gameObject.SetActive(false);
                                P2LL4 = true;
                            }
                            else if (LEDcolorL == 4)
                            {
                                GameObject p2lL5 = transform.Find("Pants2 left LED5").gameObject;
                                p2lL5.gameObject.SetActive(false);
                                P2LL5 = true;
                            }
                        }
                    }

                    if (shoes == 0) //신발
                    {
                        if (shoesColor == 0)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s1lc1 = transform.Find("Shoes1 left color1").gameObject;
                                s1lc1.gameObject.SetActive(false);
                                S1LC1 = true;
                            }
                            else if (tearingSL == 1)
                            {
                                GameObject s1lc1t = transform.Find("Shoes1 left color1 tear").gameObject;
                                s1lc1t.gameObject.SetActive(false);
                                S1LC1T = true;
                            }
                        }
                        else if (shoesColor == 1)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s1lc2 = transform.Find("Shoes1 left color2").gameObject;
                                s1lc2.gameObject.SetActive(false);
                                S1LC2 = true;
                            }
                            else if (tearingSL == 1)
                            {
                                GameObject s1lc2t = transform.Find("Shoes1 left color2 tear").gameObject;
                                s1lc2t.gameObject.SetActive(false);
                                S1LC2T = true;
                            }
                        }
                        else if (shoesColor == 2)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s1lc3 = transform.Find("Shoes1 left color3").gameObject;
                                s1lc3.gameObject.SetActive(false);
                                S1LC3 = true;
                            }
                            else if (tearingSL == 1)
                            {
                                GameObject s1lc3t = transform.Find("Shoes1 left color3 tear").gameObject;
                                s1lc3t.gameObject.SetActive(false);
                                S1LC3T = true;
                            }
                        }
                        else if (shoesColor == 3)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s1lc4 = transform.Find("Shoes1 left color4").gameObject;
                                s1lc4.gameObject.SetActive(false);
                                S1LC4 = true;
                            }
                            else if (tearingSL == 1)
                            {
                                GameObject s1lc4t = transform.Find("Shoes1 left color4 tear").gameObject;
                                s1lc4t.gameObject.SetActive(false);
                                S1LC4T = true;
                            }
                        }
                        else
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s1lc5 = transform.Find("Shoes1 left color5").gameObject;
                                s1lc5.gameObject.SetActive(false);
                                S1LC5 = true;
                            }
                            else if (tearingSL == 1)
                            {
                                GameObject s1lc5t = transform.Find("Shoes1 left color5 tear").gameObject;
                                s1lc5t.gameObject.SetActive(false);
                                S1LC5T = true;
                            }
                        }
                    }
                    else //샌들
                    {
                        if (shoesColor == 0)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s2lc1 = transform.Find("Shoes2 left color1").gameObject;
                                s2lc1.gameObject.SetActive(false);
                                S2LC1 = true;
                            }
                        }
                        else if (shoesColor == 1)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s2lc2 = transform.Find("Shoes2 left color2").gameObject;
                                s2lc2.gameObject.SetActive(false);
                                S2LC2 = true;
                            }
                        }
                        else if (shoesColor == 2)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s2lc3 = transform.Find("Shoes2 left color3").gameObject;
                                s2lc3.gameObject.SetActive(false);
                                S2LC3 = true;
                            }
                        }
                        else if (shoesColor == 3)
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s2lc4 = transform.Find("Shoes2 left color4").gameObject;
                                s2lc4.gameObject.SetActive(false);
                                S2LC4 = true;
                            }
                        }
                        else
                        {
                            if (tearingSL == 0)
                            {
                                GameObject s2lc5 = transform.Find("Shoes2 left color5").gameObject;
                                s2lc5.gameObject.SetActive(false);
                                S2LC5 = true;
                            }
                        }
                    }
                }

                //회사원
                else if (job > 40 && job <= 60)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s1dlc1 = transform.Find("Suit1 down left color1").gameObject;
                        s1dlc1.gameObject.SetActive(false);
                        S1DLC1 = true;

                        if (clothTear == 1 || DownLclothTear == 1)
                        {
                            GameObject s1dlcc1 = transform.Find("Suit1 down left cloth color1").gameObject;
                            s1dlcc1.gameObject.SetActive(false);
                            S1DLCC1 = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s1dlc2 = transform.Find("Suit1 down left color2").gameObject;
                        s1dlc2.gameObject.SetActive(false);
                        S1DLC2 = true;

                        if (clothTear == 2 || DownLclothTear == 2)
                        {
                            GameObject s1dlcc2 = transform.Find("Suit1 down left cloth color2").gameObject;
                            s1dlcc2.gameObject.SetActive(false);
                            S1DLCC2 = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s1dlc3 = transform.Find("Suit1 down left color3").gameObject;
                        s1dlc3.gameObject.SetActive(false);
                        S1DLC3 = true;

                        if (clothTear == 3 || DownLclothTear == 3)
                        {
                            GameObject s1dlcc3 = transform.Find("Suit1 down left cloth color3").gameObject;
                            s1dlcc3.gameObject.SetActive(false);
                            S1DLCC3 = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s1dlc4 = transform.Find("Suit1 down left color4").gameObject;
                        s1dlc4.gameObject.SetActive(false);
                        S1DLC4 = true;

                        if (clothTear == 4 || DownLclothTear == 4)
                        {
                            GameObject s1dlcc4 = transform.Find("Suit1 down left cloth color4").gameObject;
                            s1dlcc4.gameObject.SetActive(false);
                            S1DLCC4 = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s1dlc5 = transform.Find("Suit1 down left color5").gameObject;
                        s1dlc5.gameObject.SetActive(false);
                        S1DLC5 = true;

                        if (clothTear == 5 || DownLclothTear == 5)
                        {
                            GameObject s1dlcc5 = transform.Find("Suit1 down left cloth color5").gameObject;
                            s1dlcc5.gameObject.SetActive(false);
                            S1DLCC5 = true;
                        }
                    }
                    if (LED == 0)
                    {
                        if (tearing == 0)
                        {
                            if (LEDcolorleg == 0)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s1dlL1t = transform.Find("Suit1 down left LED1 tear").gameObject;
                                    s1dlL1t.gameObject.SetActive(false);
                                    S1DLL1T = true;
                                }
                            }
                            else if (LEDcolorleg == 1)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s1dlL2t = transform.Find("Suit1 down left LED2 tear").gameObject;
                                    s1dlL2t.gameObject.SetActive(false);
                                    S1DLL2T = true;
                                }
                            }
                            else if (LEDcolorleg == 2)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s1dlL3t = transform.Find("Suit1 down left LED3 tear").gameObject;
                                    s1dlL3t.gameObject.SetActive(false);
                                    S1DLL3T = true;
                                }
                            }
                            else if (LEDcolorleg == 3)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s1dlL4t = transform.Find("Suit1 down left LED4 tear").gameObject;
                                    s1dlL4t.gameObject.SetActive(false);
                                    S1DLL4T = true;
                                }
                            }
                            else if (LEDcolorleg == 4)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s1dlL5t = transform.Find("Suit1 down left LED5 tear").gameObject;
                                    s1dlL5t.gameObject.SetActive(false);
                                    S1DLL5T = true;
                                }
                            }
                        }
                        else
                        {
                            if (LEDcolorleg == 0)
                            {
                                GameObject s1dlL1 = transform.Find("Suit1 down left LED1").gameObject;
                                s1dlL1.gameObject.SetActive(false);
                                S1DLL1 = true;
                            }
                            else if (LEDcolorleg == 1)
                            {
                                GameObject s1dlL2 = transform.Find("Suit1 down left LED2").gameObject;
                                s1dlL2.gameObject.SetActive(false);
                                S1DLL2 = true;
                            }
                            else if (LEDcolorleg == 2)
                            {
                                GameObject s1dlL3 = transform.Find("Suit1 down left LED3").gameObject;
                                s1dlL3.gameObject.SetActive(false);
                                S1DLL3 = true;
                            }
                            else if (LEDcolorleg == 3)
                            {
                                GameObject s1dlL4 = transform.Find("Suit1 down left LED4").gameObject;
                                s1dlL4.gameObject.SetActive(false);
                                S1DLL4 = true;
                            }
                            else if (LEDcolorleg == 4)
                            {
                                GameObject s1dlL5 = transform.Find("Suit1 down left LED5").gameObject;
                                s1dlL5.gameObject.SetActive(false);
                                S1DLL5 = true;
                            }
                        }
                    }

                    if (shoesColor == 0)
                    {
                        if (tearingSL == 0)
                        {
                            GameObject s3lc1 = transform.Find("Shoes3 left color1").gameObject;
                            s3lc1.gameObject.SetActive(false);
                            S3LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s3lc1t = transform.Find("Shoes3 left color1 tear").gameObject;
                            s3lc1t.gameObject.SetActive(false);
                            S3LC1T = true;
                        }
                    }
                    else if (shoesColor == 1)
                    {
                        if (tearingSL == 0)
                        {
                            GameObject s3lc2 = transform.Find("Shoes3 left color2").gameObject;
                            s3lc2.gameObject.SetActive(false);
                            S3LC2 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s3lc2t = transform.Find("Shoes3 left color2 tear").gameObject;
                            s3lc2t.gameObject.SetActive(false);
                            S3LC2T = true;
                        }
                    }
                    else
                    {
                        if (tearingSL == 0)
                        {
                            GameObject s3lc3 = transform.Find("Shoes3 left color3").gameObject;
                            s3lc3.gameObject.SetActive(false);
                            S3LC3 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s3lc3t = transform.Find("Shoes3 left color3 tear").gameObject;
                            s3lc3t.gameObject.SetActive(false);
                            S3LC3T = true;
                        }
                    }
                }

                //기술자
                else if (job > 60 && job <= 80)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s2dlc1 = transform.Find("Suit2 down left color1").gameObject;
                        s2dlc1.gameObject.SetActive(false);
                        S2DLC1 = true;

                        if (clothTear == 1 || DownLclothTear == 1)
                        {
                            GameObject s2dlcc1 = transform.Find("Suit2 down left cloth color1").gameObject;
                            s2dlcc1.gameObject.SetActive(false);
                            S2DLCC1 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                            s4lc1.gameObject.SetActive(false);
                            S4LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                            s4lc1t.gameObject.SetActive(false);
                            S4LC1T = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s2dlc2 = transform.Find("Suit2 down left color2").gameObject;
                        s2dlc2.gameObject.SetActive(false);
                        S2DLC2 = true;

                        if (clothTear == 2 || DownLclothTear == 2)
                        {
                            GameObject s2dlcc2 = transform.Find("Suit2 down left cloth color2").gameObject;
                            s2dlcc2.gameObject.SetActive(false);
                            S2DLCC2 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s4lc2 = transform.Find("Shoes4 left color2").gameObject;
                            s4lc2.gameObject.SetActive(false);
                            S4LC2 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s4lc2t = transform.Find("Shoes4 left color2 tear").gameObject;
                            s4lc2t.gameObject.SetActive(false);
                            S4LC2T = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s2dlc3 = transform.Find("Suit2 down left color3").gameObject;
                        s2dlc3.gameObject.SetActive(false);
                        S2DLC3 = true;

                        if (clothTear == 3 || DownLclothTear == 3)
                        {
                            GameObject s2dlcc3 = transform.Find("Suit2 down left cloth color3").gameObject;
                            s2dlcc3.gameObject.SetActive(false);
                            S2DLCC3 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                            s4lc1.gameObject.SetActive(false);
                            S4LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                            s4lc1t.gameObject.SetActive(false);
                            S4LC1T = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s2dlc4 = transform.Find("Suit2 down left color4").gameObject;
                        s2dlc4.gameObject.SetActive(false);
                        S2DLC4 = true;

                        if (clothTear == 4 || DownLclothTear == 4)
                        {
                            GameObject s2dlcc4 = transform.Find("Suit2 down left cloth color4").gameObject;
                            s2dlcc4.gameObject.SetActive(false);
                            S2DLCC4 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                            s4lc1.gameObject.SetActive(false);
                            S4LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                            s4lc1t.gameObject.SetActive(false);
                            S4LC1T = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s2dlc5 = transform.Find("Suit2 down left color5").gameObject;
                        s2dlc5.gameObject.SetActive(false);
                        S2DLC5 = true;

                        if (clothTear == 5 || DownLclothTear == 5)
                        {
                            GameObject s2dlcc5 = transform.Find("Suit2 down left cloth color5").gameObject;
                            s2dlcc5.gameObject.SetActive(false);
                            S2DLCC5 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                            s4lc1.gameObject.SetActive(false);
                            S4LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                            s4lc1t.gameObject.SetActive(false);
                            S4LC1T = true;
                        }
                    }

                    if (LED == 0)
                    {
                        if (LEDcolor == 0)
                        {
                            GameObject s2dlL1 = transform.Find("Suit2 down left LED1").gameObject;
                            s2dlL1.gameObject.SetActive(false);
                            S2DLL1 = true;
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject s2dlL2 = transform.Find("Suit2 down left LED2").gameObject;
                            s2dlL2.gameObject.SetActive(false);
                            S2DLL2 = true;
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject s2dlL3 = transform.Find("Suit2 down left LED3").gameObject;
                            s2dlL3.gameObject.SetActive(false);
                            S2DLL3 = true;
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject s2dlL4 = transform.Find("Suit2 down left LED4").gameObject;
                            s2dlL4.gameObject.SetActive(false);
                            S2DLL4 = true;
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject s2dlL5 = transform.Find("Suit2 down left LED5").gameObject;
                            s2dlL5.gameObject.SetActive(false);
                            S2DLL5 = true;
                        }
                    }
                }

                //의료원
                else if (job > 80 && job <= 100)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s3dlc1 = transform.Find("Suit3 down left color1").gameObject;
                        s3dlc1.gameObject.SetActive(false);
                        S3DLC1 = true;

                        if (clothTear == 1 || DownLclothTear == 1)
                        {
                            GameObject s3dlcc1 = transform.Find("Suit3 down left cloth color1").gameObject;
                            s3dlcc1.gameObject.SetActive(false);
                            S3DLCC1 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s5lc1 = transform.Find("Shoes5 left color1").gameObject;
                            s5lc1.gameObject.SetActive(false);
                            S5LC1 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s5lc1t = transform.Find("Shoes5 left color1 tear").gameObject;
                            s5lc1t.gameObject.SetActive(false);
                            S5LC1T = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s3dlc2 = transform.Find("Suit3 down left color2").gameObject;
                        s3dlc2.gameObject.SetActive(false);
                        S3DLC2 = true;

                        if (clothTear == 2 || DownLclothTear == 2)
                        {
                            GameObject s3dlcc2 = transform.Find("Suit3 down left cloth color2").gameObject;
                            s3dlcc2.gameObject.SetActive(false);
                            S3DLCC2 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s5lc2 = transform.Find("Shoes5 left color2").gameObject;
                            s5lc2.gameObject.SetActive(false);
                            S5LC2 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s5lc2t = transform.Find("Shoes5 left color2 tear").gameObject;
                            s5lc2t.gameObject.SetActive(false);
                            S5LC2T = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s3dlc3 = transform.Find("Suit3 down left color3").gameObject;
                        s3dlc3.gameObject.SetActive(false);
                        S3DLC3 = true;

                        if (clothTear == 3 || DownLclothTear == 3)
                        {
                            GameObject s3dlcc3 = transform.Find("Suit3 down left cloth color3").gameObject;
                            s3dlcc3.gameObject.SetActive(false);
                            S3DLCC3 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s5lc3 = transform.Find("Shoes5 left color3").gameObject;
                            s5lc3.gameObject.SetActive(false);
                            S5LC3 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s5lc3t = transform.Find("Shoes5 left color3 tear").gameObject;
                            s5lc3t.gameObject.SetActive(false);
                            S5LC3T = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s3dlc4 = transform.Find("Suit3 down left color4").gameObject;
                        s3dlc4.gameObject.SetActive(false);
                        S3DLC4 = true;

                        if (clothTear == 4 || DownLclothTear == 4)
                        {
                            GameObject s3dlcc4 = transform.Find("Suit3 down left cloth color4").gameObject;
                            s3dlcc4.gameObject.SetActive(false);
                            S3DLCC4 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s5lc4 = transform.Find("Shoes5 left color4").gameObject;
                            s5lc4.gameObject.SetActive(false);
                            S5LC4 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s5lc4t = transform.Find("Shoes5 left color4 tear").gameObject;
                            s5lc4t.gameObject.SetActive(false);
                            S5LC4T = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s3dlc5 = transform.Find("Suit3 down left color5").gameObject;
                        s3dlc5.gameObject.SetActive(false);
                        S3DLC5 = true;

                        if (clothTear == 5 || DownLclothTear == 5)
                        {
                            GameObject s3dlcc5 = transform.Find("Suit3 down left cloth color5").gameObject;
                            s3dlcc5.gameObject.SetActive(false);
                            S3DLCC5 = true;
                        }

                        if (tearingSL == 0)
                        {
                            GameObject s5lc5 = transform.Find("Shoes5 left color5").gameObject;
                            s5lc5.gameObject.SetActive(false);
                            S5LC5 = true;
                        }
                        else if (tearingSL == 1)
                        {
                            GameObject s5lc5t = transform.Find("Shoes5 left color5 tear").gameObject;
                            s5lc5t.gameObject.SetActive(false);
                            S5LC5T = true;
                        }
                    }

                    if (LED == 0)
                    {
                        if (tearing == 0)
                        {
                            if (LEDcolor == 0)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s3dlL1t = transform.Find("Suit3 down left LED1 tear").gameObject;
                                    s3dlL1t.gameObject.SetActive(false);
                                    S3DLL1T = true;
                                }
                            }
                            else if (LEDcolor == 1)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s3dlL2t = transform.Find("Suit3 down left LED2 tear").gameObject;
                                    s3dlL2t.gameObject.SetActive(false);
                                    S3DLL2T = true;
                                }
                            }
                            else if (LEDcolor == 2)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s3dlL3t = transform.Find("Suit3 down left LED3 tear").gameObject;
                                    s3dlL3t.gameObject.SetActive(false);
                                    S3DLL3T = true;
                                }
                            }
                            else if (LEDcolor == 3)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s3dlL4t = transform.Find("Suit3 down left LED4 tear").gameObject;
                                    s3dlL4t.gameObject.SetActive(false);
                                    S3DLL4T = true;
                                }
                            }
                            else if (LEDcolor == 4)
                            {
                                if (LEDBetweenL == 0)
                                {
                                    GameObject s3dlL5t = transform.Find("Suit3 down left LED5 tear").gameObject;
                                    s3dlL5t.gameObject.SetActive(false);
                                    S3DLL5T = true;
                                }
                            }
                        }
                        else if (tearing == 1)
                        {
                            if (LEDcolor == 0)
                            {
                                GameObject s3dlL1 = transform.Find("Suit3 down left LED1").gameObject;
                                s3dlL1.gameObject.SetActive(false);
                                S3DLL1 = true;
                            }
                            else if (LEDcolor == 1)
                            {
                                GameObject s3dlL2 = transform.Find("Suit3 down left LED2").gameObject;
                                s3dlL2.gameObject.SetActive(false);
                                S3DLL2 = true;
                            }
                            else if (LEDcolor == 2)
                            {
                                GameObject s3dlL3 = transform.Find("Suit3 down left LED3").gameObject;
                                s3dlL3.gameObject.SetActive(false);
                                S3DLL3 = true;
                            }
                            else if (LEDcolor == 3)
                            {
                                GameObject s3dlL4 = transform.Find("Suit3 down left LED4").gameObject;
                                s3dlL4.gameObject.SetActive(false);
                                S3DLL4 = true;
                            }
                            else if (LEDcolor == 4)
                            {
                                GameObject s3dlL5 = transform.Find("Suit3 down left LED5").gameObject;
                                s3dlL5.gameObject.SetActive(false);
                                S3DLL5 = true;
                            }
                        }
                    }
                }
            }
            ///////////////////////////////////////////////////////// 왼쪽 다리 /////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////// 오른쪽 다리 /////////////////////////////////////////////////////////
            if (LegRout == true)
            {
                if (body == 0)
                {
                    GameObject body1rl = transform.Find("Body1 right leg").gameObject;
                    GameObject footR = transform.Find("Right foot1").gameObject;
                    body1rl.gameObject.SetActive(false);
                    footR.gameObject.SetActive(false);
                    BODY1RL = true;
                    FOOTR = true;
                }
                else
                {
                    GameObject body2rl = transform.Find("Body2 right leg").gameObject;
                    GameObject footR = transform.Find("Right foot1").gameObject;
                    body2rl.gameObject.SetActive(false);
                    footR.gameObject.SetActive(false);
                    BODY2RL = true;
                    FOOTR = true;
                }

                //일반인
                if (job <= 40)
                {
                    if (pants == 0)
                    {
                        if (pantsColor == 0)
                        {
                            GameObject p1rc1 = transform.Find("Pants1 right color1").gameObject;
                            p1rc1.gameObject.SetActive(false);
                            P1RC1 = true;
                            GameObject p1rcc1 = transform.Find("Pants1 right cloth color1").gameObject;
                            p1rcc1.gameObject.SetActive(false);
                            P1RCC1 = true;
                        }
                        else if (pantsColor == 1)
                        {
                            GameObject p1rc2 = transform.Find("Pants1 right color2").gameObject;
                            p1rc2.gameObject.SetActive(false);
                            P1RC2 = true;
                            GameObject p1rcc2 = transform.Find("Pants1 right cloth color2").gameObject;
                            p1rcc2.gameObject.SetActive(false);
                            P1RCC2 = true;
                        }
                        else if (pantsColor == 2)
                        {
                            GameObject p1rc3 = transform.Find("Pants1 right color3").gameObject;
                            p1rc3.gameObject.SetActive(false);
                            P1RC3 = true;
                            GameObject p1rcc3 = transform.Find("Pants1 right cloth color3").gameObject;
                            p1rcc3.gameObject.SetActive(false);
                            P1RCC3 = true;
                        }
                        else
                        {
                            GameObject p1rc4 = transform.Find("Pants1 right color4").gameObject;
                            p1rc4.gameObject.SetActive(false);
                            P1RC4 = true;
                            GameObject p1rcc4 = transform.Find("Pants1 right cloth color4").gameObject;
                            p1rcc4.gameObject.SetActive(false);
                            P1RCC4 = true;
                        }

                        if (LED == 0)
                        {
                            if (LEDcolor == 0)
                            {
                                GameObject p1rL1 = transform.Find("Pants1 right LED1").gameObject;
                                p1rL1.gameObject.SetActive(false);
                                P1RL1 = true;
                            }
                            else if (LEDcolor == 1)
                            {
                                GameObject p1rL2 = transform.Find("Pants1 right LED2").gameObject;
                                p1rL2.gameObject.SetActive(false);
                                P1RL2 = true;
                            }
                            else if (LEDcolor == 2)
                            {
                                GameObject p1rL3 = transform.Find("Pants1 right LED3").gameObject;
                                p1rL3.gameObject.SetActive(false);
                                P1RL3 = true;
                            }
                            else if (LEDcolor == 3)
                            {
                                GameObject p1rL4 = transform.Find("Pants1 right LED4").gameObject;
                                p1rL4.gameObject.SetActive(false);
                                P1RL4 = true;
                            }
                            else if (LEDcolor == 4)
                            {
                                GameObject p1rL5 = transform.Find("Pants1 right LED5").gameObject;
                                p1rL5.gameObject.SetActive(false);
                                P1RL5 = true;
                            }
                        }
                    }
                    else
                    {
                        if (pantsColor == 0)
                        {
                            GameObject p2rc1 = transform.Find("Pants2 right color1").gameObject;
                            p2rc1.gameObject.SetActive(false);
                            P2RC1 = true;
                            GameObject p2rcc1 = transform.Find("Pants2 right cloth color1").gameObject;
                            p2rcc1.gameObject.SetActive(false);
                            P2RCC1 = true;
                        }
                        else if (pantsColor == 1)
                        {
                            GameObject p2rc2 = transform.Find("Pants2 right color2").gameObject;
                            p2rc2.gameObject.SetActive(false);
                            P2LC2 = true;
                            GameObject p2rcc2 = transform.Find("Pants2 right cloth color2").gameObject;
                            p2rcc2.gameObject.SetActive(false);
                            P2RCC2 = true;
                        }
                        else if (pantsColor == 2)
                        {
                            GameObject p2rc3 = transform.Find("Pants2 right color3").gameObject;
                            p2rc3.gameObject.SetActive(false);
                            P2RC3 = true;
                            GameObject p2rcc3 = transform.Find("Pants2 right cloth color3").gameObject;
                            p2rcc3.gameObject.SetActive(false);
                            P2RCC3 = true;
                        }
                        else
                        {
                            GameObject p2rc4 = transform.Find("Pants2 right color4").gameObject;
                            p2rc4.gameObject.SetActive(false);
                            P2RC4 = true;
                            GameObject p2rcc4 = transform.Find("Pants2 right cloth color4").gameObject;
                            p2rcc4.gameObject.SetActive(false);
                            P2RCC4 = true;
                        }

                        if (LEDR == 0)
                        {
                            if (LEDcolorR == 0)
                            {
                                GameObject p2rL1 = transform.Find("Pants2 right LED1").gameObject;
                                p2rL1.gameObject.SetActive(false);
                                P2RL1 = true;
                            }
                            else if (LEDcolorR == 1)
                            {
                                GameObject p2rL2 = transform.Find("Pants2 right LED2").gameObject;
                                p2rL2.gameObject.SetActive(false);
                                P2RL2 = true;
                            }
                            else if (LEDcolorR == 2)
                            {
                                GameObject p2rL3 = transform.Find("Pants2 right LED3").gameObject;
                                p2rL3.gameObject.SetActive(false);
                                P2RL3 = true;
                            }
                            else if (LEDcolorR == 3)
                            {
                                GameObject p2rL4 = transform.Find("Pants2 right LED4").gameObject;
                                p2rL4.gameObject.SetActive(false);
                                P2RL4 = true;
                            }
                            else if (LEDcolorR == 4)
                            {
                                GameObject p2rL5 = transform.Find("Pants2 right LED5").gameObject;
                                p2rL5.gameObject.SetActive(false);
                                P2RL5 = true;
                            }
                        }
                    }

                    if (shoes == 0) //신발
                    {
                        if (shoesColor == 0)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s1rc1 = transform.Find("Shoes1 right color1").gameObject;
                                s1rc1.gameObject.SetActive(false);
                                S1RC1 = true;
                            }
                            else if (tearingSR == 1)
                            {
                                GameObject s1rc1t = transform.Find("Shoes1 right color1 tear").gameObject;
                                s1rc1t.gameObject.SetActive(false);
                                S1RC1T = true;
                            }
                        }
                        else if (shoesColor == 1)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s1rc2 = transform.Find("Shoes1 right color2").gameObject;
                                s1rc2.gameObject.SetActive(false);
                                S1RC2 = true;
                            }
                            else if (tearingSR == 1)
                            {
                                GameObject s1rc2t = transform.Find("Shoes1 right color2 tear").gameObject;
                                s1rc2t.gameObject.SetActive(false);
                                S1RC2T = true;
                            }
                        }
                        else if (shoesColor == 2)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s1rc3 = transform.Find("Shoes1 right color3").gameObject;
                                s1rc3.gameObject.SetActive(false);
                                S1RC3 = true;
                            }
                            else if (tearingSR == 1)
                            {
                                GameObject s1rc3t = transform.Find("Shoes1 right color3 tear").gameObject;
                                s1rc3t.gameObject.SetActive(false);
                                S1RC3T = true;
                            }
                        }
                        else if (shoesColor == 3)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s1rc4 = transform.Find("Shoes1 right color4").gameObject;
                                s1rc4.gameObject.SetActive(false);
                                S1RC4 = true;
                            }
                            else if (tearingSR == 1)
                            {
                                GameObject s1rc4t = transform.Find("Shoes1 right color4 tear").gameObject;
                                s1rc4t.gameObject.SetActive(false);
                                S1RC4T = true;
                            }
                        }
                        else
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s1rc5 = transform.Find("Shoes1 right color5").gameObject;
                                s1rc5.gameObject.SetActive(false);
                                S1RC5 = true;
                            }
                            else if (tearingSR == 1)
                            {
                                GameObject s1rc5t = transform.Find("Shoes1 right color5 tear").gameObject;
                                s1rc5t.gameObject.SetActive(false);
                                S1RC5T = true;
                            }
                        }
                    }
                    else //샌들
                    {
                        shoesColor = Random.Range(0, 5);

                        if (shoesColor == 0)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s2rc1 = transform.Find("Shoes2 right color1").gameObject;
                                s2rc1.gameObject.SetActive(false);
                                S2RC1 = true;
                            }
                        }
                        else if (shoesColor == 1)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s2rc2 = transform.Find("Shoes2 right color2").gameObject;
                                s2rc2.gameObject.SetActive(false);
                                S2RC2 = true;
                            }
                        }
                        else if (shoesColor == 2)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s2rc3 = transform.Find("Shoes2 right color3").gameObject;
                                s2rc3.gameObject.SetActive(false);
                                S2RC3 = true;
                            }
                        }
                        else if (shoesColor == 3)
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s2rc4 = transform.Find("Shoes2 right color4").gameObject;
                                s2rc4.gameObject.SetActive(false);
                                S2RC4 = true;
                            }
                        }
                        else
                        {
                            if (tearingSR == 0)
                            {
                                GameObject s2rc5 = transform.Find("Shoes2 right color5").gameObject;
                                s2rc5.gameObject.SetActive(false);
                                S2RC5 = true;
                            }
                        }
                    }
                }

                //회사원
                else if (job > 40 && job <= 60)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s1drc1 = transform.Find("Suit1 down right color1").gameObject;
                        s1drc1.gameObject.SetActive(false);
                        S1DRC1 = true;

                        if (clothTear == 1 || DownRclothTear == 1)
                        {
                            GameObject s1drcc1 = transform.Find("Suit1 down right cloth color1").gameObject;
                            s1drcc1.gameObject.SetActive(false);
                            S1DRCC1 = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s1drc2 = transform.Find("Suit1 down right color2").gameObject;
                        s1drc2.gameObject.SetActive(false);
                        S1DRC2 = true;

                        if (clothTear == 2 || DownRclothTear == 2)
                        {
                            GameObject s1drcc2 = transform.Find("Suit1 down right cloth color2").gameObject;
                            s1drcc2.gameObject.SetActive(false);
                            S1DRCC2 = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s1drc3 = transform.Find("Suit1 down right color3").gameObject;
                        s1drc3.gameObject.SetActive(false);
                        S1DRC3 = true;

                        if (clothTear == 3 || DownRclothTear == 3)
                        {
                            GameObject s1drcc3 = transform.Find("Suit1 down right cloth color3").gameObject;
                            s1drcc3.gameObject.SetActive(false);
                            S1DRCC3 = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s1drc4 = transform.Find("Suit1 down right color4").gameObject;
                        s1drc4.gameObject.SetActive(false);
                        S1DRC4 = true;

                        if (clothTear == 4 || DownRclothTear == 4)
                        {
                            GameObject s1drcc4 = transform.Find("Suit1 down right cloth color4").gameObject;
                            s1drcc4.gameObject.SetActive(false);
                            S1DRCC4 = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s1drc5 = transform.Find("Suit1 down right color5").gameObject;
                        s1drc5.gameObject.SetActive(false);
                        S1DRC5 = true;

                        if (clothTear == 5 || DownRclothTear == 5)
                        {
                            GameObject s1drcc5 = transform.Find("Suit1 down right cloth color5").gameObject;
                            s1drcc5.gameObject.SetActive(false);
                            S1DRCC5 = true;
                        }
                    }

                    if (LED == 0)
                    {
                        if (tearing == 0)
                        {
                            if (LEDcolorleg == 0)
                            {
                                if (LEDBetweenR == 0)
                                {
                                    GameObject s1drL1t = transform.Find("Suit1 down right LED1 tear").gameObject;
                                    s1drL1t.gameObject.SetActive(false);
                                    S1DRL1T = true;
                                }
                            }
                            else if (LEDcolorleg == 1)
                            {
                                if (LEDBetweenR == 0)
                                {
                                    GameObject s1drL2t = transform.Find("Suit1 down right LED2 tear").gameObject;
                                    s1drL2t.gameObject.SetActive(false);
                                    S1DRL2T = true;
                                }
                            }
                            else if (LEDcolorleg == 2)
                            {
                                if (LEDBetweenR == 0)
                                {
                                    GameObject s1drL3t = transform.Find("Suit1 down right LED3 tear").gameObject;
                                    s1drL3t.gameObject.SetActive(false);
                                    S1DRL3T = true;
                                }
                            }
                            else if (LEDcolorleg == 3)
                            {
                                if (LEDBetweenR == 0)
                                {
                                    GameObject s1drL4t = transform.Find("Suit1 down right LED4 tear").gameObject;
                                    s1drL4t.gameObject.SetActive(false);
                                    S1DRL4T = true;
                                }
                            }
                            else if (LEDcolorleg == 4)
                            {
                                if (LEDBetweenR == 0)
                                {
                                    GameObject s1drL5t = transform.Find("Suit1 down right LED5 tear").gameObject;
                                    s1drL5t.gameObject.SetActive(false);
                                    S1DRL5T = true;
                                }
                            }
                        }
                        else
                        {
                            if (LEDcolorleg == 0)
                            {
                                GameObject s1drL1 = transform.Find("Suit1 down right LED1").gameObject;
                                s1drL1.gameObject.SetActive(false);
                                S1DRL1 = true;
                            }
                            else if (LEDcolorleg == 1)
                            {
                                GameObject s1drL2 = transform.Find("Suit1 down right LED2").gameObject;
                                s1drL2.gameObject.SetActive(false);
                                S1DRL2 = true;
                            }
                            else if (LEDcolorleg == 2)
                            {
                                GameObject s1drL3 = transform.Find("Suit1 down right LED3").gameObject;
                                s1drL3.gameObject.SetActive(false);
                                S1DRL3 = true;
                            }
                            else if (LEDcolorleg == 3)
                            {
                                GameObject s1drL4 = transform.Find("Suit1 down right LED4").gameObject;
                                s1drL4.gameObject.SetActive(false);
                                S1DRL4 = true;
                            }
                            else if (LEDcolorleg == 4)
                            {
                                GameObject s1drL5 = transform.Find("Suit1 down right LED5").gameObject;
                                s1drL5.gameObject.SetActive(false);
                                S1DRL5 = true;
                            }
                        }
                    }

                    if (shoesColor == 0)
                    {
                        if (tearingSR == 0)
                        {
                            GameObject s3rc1 = transform.Find("Shoes3 right color1").gameObject;
                            s3rc1.gameObject.SetActive(false);
                            S3RC1 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s3rc1t = transform.Find("Shoes3 right color1 tear").gameObject;
                            s3rc1t.gameObject.SetActive(false);
                            S3RC1T = true;
                        }
                    }
                    else if (shoesColor == 1)
                    {
                        if (tearingSR == 0)
                        {
                            GameObject s3rc2 = transform.Find("Shoes3 right color2").gameObject;
                            s3rc2.gameObject.SetActive(false);
                            S3RC2 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s3rc2t = transform.Find("Shoes3 right color2 tear").gameObject;
                            s3rc2t.gameObject.SetActive(false);
                            S3RC2T = true;
                        }
                    }
                    else
                    {
                        if (tearingSR == 0)
                        {
                            GameObject s3rc3 = transform.Find("Shoes3 right color3").gameObject;
                            s3rc3.gameObject.SetActive(false);
                            S3RC3 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s3rc3t = transform.Find("Shoes3 right color3 tear").gameObject;
                            s3rc3t.gameObject.SetActive(false);
                            S3RC3T = true;
                        }
                    }
                }

                //기술자
                else if (job > 60 && job <= 80)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s2drc1 = transform.Find("Suit2 down right color1").gameObject;
                        s2drc1.gameObject.SetActive(false);
                        S2DRC1 = true;

                        if (clothTear == 1 || DownRclothTear == 1)
                        {
                            GameObject s2drcc1 = transform.Find("Suit2 down right cloth color1").gameObject;
                            s2drcc1.gameObject.SetActive(false);
                            S2DRCC1 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s4rc1 = transform.Find("Shoes4 right color1").gameObject;
                            s4rc1.gameObject.SetActive(false);
                            S4RC1 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s4rc1t = transform.Find("Shoes4 right color1 tear").gameObject;
                            s4rc1t.gameObject.SetActive(false);
                            S4RC1T = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s2drc2 = transform.Find("Suit2 down right color2").gameObject;
                        s2drc2.gameObject.SetActive(false);
                        S2DRC2 = true;

                        if (clothTear == 2 || DownRclothTear == 2)
                        {
                            GameObject s2drcc2 = transform.Find("Suit2 down right cloth color2").gameObject;
                            s2drcc2.gameObject.SetActive(false);
                            S2DRCC2 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s4rc2 = transform.Find("Shoes4 right color2").gameObject;
                            s4rc2.gameObject.SetActive(false);
                            S4RC2 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s4rc2t = transform.Find("Shoes4 right color2 tear").gameObject;
                            s4rc2t.gameObject.SetActive(false);
                            S4RC2T = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s2drc3 = transform.Find("Suit2 down right color3").gameObject;
                        s2drc3.gameObject.SetActive(false);
                        S2DRC3 = true;

                        if (clothTear == 3 || DownRclothTear == 3)
                        {
                            GameObject s2drcc3 = transform.Find("Suit2 down right cloth color3").gameObject;
                            s2drcc3.gameObject.SetActive(false);
                            S2DRCC3 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s4rc3 = transform.Find("Shoes4 right color3").gameObject;
                            s4rc3.gameObject.SetActive(false);
                            S4RC3 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s4rc3t = transform.Find("Shoes4 right color3 tear").gameObject;
                            s4rc3t.gameObject.SetActive(false);
                            S4RC3T = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s2drc4 = transform.Find("Suit2 down right color4").gameObject;
                        s2drc4.gameObject.SetActive(false);
                        S2DRC4 = true;

                        if (clothTear == 4 || DownRclothTear == 4)
                        {
                            GameObject s2drcc4 = transform.Find("Suit2 down right cloth color4").gameObject;
                            s2drcc4.gameObject.SetActive(false);
                            S2DRCC4 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s4rc4 = transform.Find("Shoes4 right color4").gameObject;
                            s4rc4.gameObject.SetActive(false);
                            S4RC4 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s4rc4t = transform.Find("Shoes4 right color4 tear").gameObject;
                            s4rc4t.gameObject.SetActive(false);
                            S4RC4T = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s2drc5 = transform.Find("Suit2 down right color5").gameObject;
                        s2drc5.gameObject.SetActive(false);
                        S2DRC5 = true;

                        if (clothTear == 5 || DownRclothTear == 5)
                        {
                            GameObject s2drcc5 = transform.Find("Suit2 down right cloth color5").gameObject;
                            s2drcc5.gameObject.SetActive(false);
                            S2DRCC5 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s4rc4 = transform.Find("Shoes4 right color4").gameObject;
                            s4rc4.gameObject.SetActive(false);
                            S4RC4 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s4rc4t = transform.Find("Shoes4 right color4 tear").gameObject;
                            s4rc4t.gameObject.SetActive(false);
                            S4RC4T = true;
                        }
                    }

                    if (LED == 0)
                    {
                        if (LEDcolor == 0)
                        {
                            GameObject s2drL1 = transform.Find("Suit2 down right LED1").gameObject;
                            s2drL1.gameObject.SetActive(false);
                            S2DRL1 = true;
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject s2drL2 = transform.Find("Suit2 down right LED2").gameObject;
                            s2drL2.gameObject.SetActive(false);
                            S2DRL2 = true;
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject s2drL3 = transform.Find("Suit2 down right LED3").gameObject;
                            s2drL3.gameObject.SetActive(false);
                            S2DRL3 = true;
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject s2drL4 = transform.Find("Suit2 down right LED4").gameObject;
                            s2drL4.gameObject.SetActive(false);
                            S2DRL4 = true;
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject s2drL5 = transform.Find("Suit2 down right LED5").gameObject;
                            s2drL5.gameObject.SetActive(false);
                            S2DRL5 = true;
                        }
                    }
                }

                //의료원
                else if (job > 80 && job <= 100)
                {
                    if (suitsColor == 0)
                    {
                        GameObject s3drc1 = transform.Find("Suit3 down right color1").gameObject;
                        s3drc1.gameObject.SetActive(false);
                        S3DRC1 = true;

                        if (clothTear == 1 || DownRclothTear == 1)
                        {
                            GameObject s3drcc1 = transform.Find("Suit3 down right cloth color1").gameObject;
                            s3drcc1.gameObject.SetActive(false);
                            S3DRCC1 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s5rc1 = transform.Find("Shoes5 right color1").gameObject;
                            s5rc1.gameObject.SetActive(false);
                            S5RC1 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s5rc1t = transform.Find("Shoes5 right color1 tear").gameObject;
                            s5rc1t.gameObject.SetActive(false);
                            S5RC1T = true;
                        }
                    }
                    else if (suitsColor == 1)
                    {
                        GameObject s3drc2 = transform.Find("Suit3 down right color2").gameObject;
                        s3drc2.gameObject.SetActive(false);
                        S3DRC2 = true;

                        if (clothTear == 2 || DownRclothTear == 2)
                        {
                            GameObject s3drcc2 = transform.Find("Suit3 down right cloth color2").gameObject;
                            s3drcc2.gameObject.SetActive(false);
                            S3DRCC2 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s5rc2 = transform.Find("Shoes5 right color2").gameObject;
                            s5rc2.gameObject.SetActive(false);
                            S5RC2 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s5rc2t = transform.Find("Shoes5 right color2 tear").gameObject;
                            s5rc2t.gameObject.SetActive(false);
                            S5RC2T = true;
                        }
                    }
                    else if (suitsColor == 2)
                    {
                        GameObject s3drc3 = transform.Find("Suit3 down right color3").gameObject;
                        s3drc3.gameObject.SetActive(false);
                        S3DRC3 = true;

                        if (clothTear == 3 || DownRclothTear == 3)
                        {
                            GameObject s3drcc3 = transform.Find("Suit3 down right cloth color3").gameObject;
                            s3drcc3.gameObject.SetActive(false);
                            S3DRCC3 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s5rc3 = transform.Find("Shoes5 right color3").gameObject;
                            s5rc3.gameObject.SetActive(false);
                            S5RC3 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s5rc3t = transform.Find("Shoes5 right color3 tear").gameObject;
                            s5rc3t.gameObject.SetActive(false);
                            S5RC3T = true;
                        }
                    }
                    else if (suitsColor == 3)
                    {
                        GameObject s3drc4 = transform.Find("Suit3 down right color4").gameObject;
                        s3drc4.gameObject.SetActive(false);
                        S3DRC4 = true;

                        if (clothTear == 4 || DownRclothTear == 4)
                        {
                            GameObject s3drcc4 = transform.Find("Suit3 down right cloth color4").gameObject;
                            s3drcc4.gameObject.SetActive(false);
                            S3DRCC4 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s5rc4 = transform.Find("Shoes5 right color4").gameObject;
                            s5rc4.gameObject.SetActive(false);
                            S5RC4 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s5rc4t = transform.Find("Shoes5 right color4 tear").gameObject;
                            s5rc4t.gameObject.SetActive(false);
                            S5RC4T = true;
                        }
                    }
                    else if (suitsColor == 4)
                    {
                        GameObject s3drc5 = transform.Find("Suit3 down right color5").gameObject;
                        s3drc5.gameObject.SetActive(false);
                        S3DRC5 = true;

                        if (clothTear == 5 || DownRclothTear == 5)
                        {
                            GameObject s3drcc5 = transform.Find("Suit3 down right cloth color5").gameObject;
                            s3drcc5.gameObject.SetActive(false);
                            S3DRCC5 = true;
                        }

                        if (tearingSR == 0)
                        {
                            GameObject s5rc5 = transform.Find("Shoes5 right color5").gameObject;
                            s5rc5.gameObject.SetActive(false);
                            S5RC5 = true;
                        }
                        else if (tearingSR == 1)
                        {
                            GameObject s5rc5t = transform.Find("Shoes5 right color5 tear").gameObject;
                            s5rc5t.gameObject.SetActive(false);
                            S5RC5T = true;
                        }
                    }

                    if (LED == 0)
                    {
                        if (LEDcolor == 0)
                        {
                            GameObject s3drL1 = transform.Find("Suit3 down right LED1").gameObject;
                            s3drL1.gameObject.SetActive(false);
                            S3DRL1 = true;
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject s3drL2 = transform.Find("Suit3 down right LED2").gameObject;
                            s3drL2.gameObject.SetActive(false);
                            S3DRL2 = true;
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject s3drL3 = transform.Find("Suit3 down right LED3").gameObject;
                            s3drL3.gameObject.SetActive(false);
                            S3DRL3 = true;
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject s3drL4 = transform.Find("Suit3 down right LED4").gameObject;
                            s3drL4.gameObject.SetActive(false);
                            S3DRL4 = true;
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject s3drL5 = transform.Find("Suit3 down right LED5").gameObject;
                            s3drL5.gameObject.SetActive(false);
                            S3DRL5 = true;
                        }
                    }
                }
            }
            ///////////////////////////////////////////////////////// 오른쪽 다리 /////////////////////////////////////////////////////////
        }

        //잘려진 신체 발생
        //얼굴
        if (HEADOUTonline == true)
        {
            HeadCutdown();
            HEADOUTonline = false;
        }

        //오른쪽 팔
        if (HANDROUTonline == true)
        {
            HandRcut();
            HANDROUTonline = false;
        }
        if (RightArmDownHOUTonline == true)
        {
            RightArmDownHandcut();
            RightArmDownHOUTonline = false;
        }
        if (RightArmDownOUTonline == true)
        {
            RightArmDowncut();
            RightArmDownOUTonline = false;
        }
        if (RightArmUpDownHOUTonline == true)
        {
            RightArmUpDownHandcut();
            RightArmUpDownHOUTonline = false;
        }
        if (RightArmUpDownOUTonline == true)
        {
            RightArmUpDowncut();
            RightArmUpDownOUTonline = false;
        }
        if (RightArmUpOUTonline == true)
        {
            RightArmUpcut();
            RightArmUpOUTonline = false;
        }

        //왼쪽 팔
        if (HANDLOUTonline == true)
        {
            HandLcut();
            HANDLOUTonline = false;
        }
        if(LeftArmDownHOUTonline == true)
        {
            LeftArmDownHandcut();
            LeftArmDownHOUTonline = false;
        }
        if(LeftArmDownOUTonline == true)
        {
            LeftArmDowncut();
            LeftArmDownOUTonline = false;
        }
        if (LeftArmUpDownHOUTonline == true)
        {
            LeftArmUpDownHandcut();
            LeftArmUpDownHOUTonline = false;
        }
        if (LeftArmUpDownOUTonline == true)
        {
            LeftArmUpDowncut();
            LeftArmUpDownOUTonline = false;
        }
        if (LeftArmUpOUTonline == true)
        {
            LeftArmUpcut();
            LeftArmUpOUTonline = false;
        }

        //다리
        if(LeftLegOUTonline == true)
        {
            LeftLegcut();
            LeftLegOUTonline = false;
        }
        if (RightLegOUTonline == true)
        {
            RightLegcut();
            RightLegOUTonline = false;
        }
    }

    void HeadCutdown()
    {
        if (HEADOUTonline == true)
        {
            GameObject HeadCUT = Instantiate(Head, Headpos.transform.position, Headpos.transform.rotation);
            Destroy(HeadCUT, 30);

            HeadCUT.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                HeadCUT.GetComponent<TearCreateInfector>().Direction = false;
            else
                HeadCUT.GetComponent<TearCreateInfector>().Direction = true;

            if (FACE1 == true)
            {
                GameObject f1head = HeadCUT.transform.Find("Face1").gameObject;
                f1head.gameObject.SetActive(true);
            }
            else if (FACE2 == true)
            {
                GameObject f2head = HeadCUT.transform.Find("Face2").gameObject;
                f2head.gameObject.SetActive(true);
            }
            else if (FACE3 == true)
            {
                GameObject f3head = HeadCUT.transform.Find("Face3").gameObject;
                f3head.gameObject.SetActive(true);
            }
            else if (FACE4 == true)
            {
                GameObject f5head = HeadCUT.transform.Find("Face5").gameObject;
                f5head.gameObject.SetActive(true);
            }
            else if (FACE5 == true)
            {
                GameObject f6head = HeadCUT.transform.Find("Face6").gameObject;
                f6head.gameObject.SetActive(true);
            }

            if (HAIR1_1 == true)
            {
                GameObject h1_1 = HeadCUT.transform.Find("Hair1-1").gameObject;
                h1_1.gameObject.SetActive(true);
            }
            else if (HAIR1_2 == true)
            {
                GameObject h1_2 = HeadCUT.transform.Find("Hair1-2").gameObject;
                h1_2.gameObject.SetActive(true);
            }
            else if (HAIR1_3 == true)
            {
                GameObject h1_3 = HeadCUT.transform.Find("Hair1-3").gameObject;
                h1_3.gameObject.SetActive(true);
            }
            else if (HAIR1_4 == true)
            {
                GameObject h1_4 = HeadCUT.transform.Find("Hair1-4").gameObject;
                h1_4.gameObject.SetActive(true);
            }
            else if (HAIR1_5 == true)
            {
                GameObject h1_5 = HeadCUT.transform.Find("Hair1-5").gameObject;
                h1_5.gameObject.SetActive(true);
            }
            if (HAIR2_1 == true)
            {
                GameObject h2_1 = HeadCUT.transform.Find("Hair2-1").gameObject;
                h2_1.gameObject.SetActive(true);
            }
            else if (HAIR2_2 == true)
            {
                GameObject h2_2 = HeadCUT.transform.Find("Hair2-2").gameObject;
                h2_2.gameObject.SetActive(true);
            }
            else if (HAIR2_3 == true)
            {
                GameObject h2_3 = HeadCUT.transform.Find("Hair2-3").gameObject;
                h2_3.gameObject.SetActive(true);
            }
            else if (HAIR2_4 == true)
            {
                GameObject h2_4 = HeadCUT.transform.Find("Hair2-4").gameObject;
                h2_4.gameObject.SetActive(true);
            }
            else if (HAIR2_5 == true)
            {
                GameObject h2_5 = HeadCUT.transform.Find("Hair2-5").gameObject;
                h2_5.gameObject.SetActive(true);
            }
            if (HAIR3_1 == true)
            {
                GameObject h3_1 = HeadCUT.transform.Find("Hair3-1").gameObject;
                h3_1.gameObject.SetActive(true);
            }
            else if (HAIR3_2 == true)
            {
                GameObject h3_2 = HeadCUT.transform.Find("Hair3-2").gameObject;
                h3_2.gameObject.SetActive(true);
            }
            else if (HAIR3_3 == true)
            {
                GameObject h3_3 = HeadCUT.transform.Find("Hair3-3").gameObject;
                h3_3.gameObject.SetActive(true);
            }
            else if (HAIR3_4 == true)
            {
                GameObject h3_4 = HeadCUT.transform.Find("Hair3-4").gameObject;
                h3_4.gameObject.SetActive(true);
            }
            else if (HAIR3_5 == true)
            {
                GameObject h3_5 = HeadCUT.transform.Find("Hair3-5").gameObject;
                h3_5.gameObject.SetActive(true);
            }
            if (HAIR4_1 == true)
            {
                GameObject h4_1 = HeadCUT.transform.Find("Hair4-1").gameObject;
                h4_1.gameObject.SetActive(true);
            }
            else if (HAIR4_2 == true)
            {
                GameObject h4_2 = HeadCUT.transform.Find("Hair4-2").gameObject;
                h4_2.gameObject.SetActive(true);
            }
            else if (HAIR4_3 == true)
            {
                GameObject h4_3 = HeadCUT.transform.Find("Hair4-3").gameObject;
                h4_3.gameObject.SetActive(true);
            }
            else if (HAIR4_4 == true)
            {
                GameObject h4_4 = HeadCUT.transform.Find("Hair4-4").gameObject;
                h4_4.gameObject.SetActive(true);
            }
            else if (HAIR4_5 == true)
            {
                GameObject h4_5 = HeadCUT.transform.Find("Hair4-5").gameObject;
                h4_5.gameObject.SetActive(true);
            }
            if (HAIR5_1 == true)
            {
                GameObject h5_1 = HeadCUT.transform.Find("Hair5-1").gameObject;
                h5_1.gameObject.SetActive(true);
            }
            else if (HAIR5_2 == true)
            {
                GameObject h5_2 = HeadCUT.transform.Find("Hair5-2").gameObject;
                h5_2.gameObject.SetActive(true);
            }
            else if (HAIR5_3 == true)
            {
                GameObject h5_3 = HeadCUT.transform.Find("Hair5-3").gameObject;
                h5_3.gameObject.SetActive(true);
            }
            else if (HAIR5_4 == true)
            {
                GameObject h5_4 = HeadCUT.transform.Find("Hair5-4").gameObject;
                h5_4.gameObject.SetActive(true);
            }
            else if (HAIR5_5 == true)
            {
                GameObject h5_5 = HeadCUT.transform.Find("Hair5-5").gameObject;
                h5_5.gameObject.SetActive(true);
            }
            if (HAIR6_1 == true)
            {
                GameObject h6_1 = HeadCUT.transform.Find("Hair6-1").gameObject;
                h6_1.gameObject.SetActive(true);
            }
            else if (HAIR6_2 == true)
            {
                GameObject h6_2 = HeadCUT.transform.Find("Hair6-2").gameObject;
                h6_2.gameObject.SetActive(true);
            }
            else if (HAIR6_3 == true)
            {
                GameObject h6_3 = HeadCUT.transform.Find("Hair6-3").gameObject;
                h6_3.gameObject.SetActive(true);
            }
            else if (HAIR6_4 == true)
            {
                GameObject h6_4 = HeadCUT.transform.Find("Hair6-4").gameObject;
                h6_4.gameObject.SetActive(true);
            }
            else if (HAIR6_5 == true)
            {
                GameObject h6_5 = HeadCUT.transform.Find("Hair6-5").gameObject;
                h6_5.gameObject.SetActive(true);
            }

            if (S3HC1 == true)
            {
                GameObject s3hb = HeadCUT.transform.Find("Suit3 head blood").gameObject;
                GameObject s3hi = HeadCUT.transform.Find("Suit3 head inner").gameObject;
                GameObject s3hc1 = HeadCUT.transform.Find("Suit3 head color1").gameObject;
                s3hb.gameObject.SetActive(true);
                s3hi.gameObject.SetActive(true);
                s3hc1.gameObject.SetActive(true);
            }
            else if (S3HC2 == true)
            {
                GameObject s3hb = HeadCUT.transform.Find("Suit3 head blood").gameObject;
                GameObject s3hi = HeadCUT.transform.Find("Suit3 head inner").gameObject;
                GameObject s3hc2 = HeadCUT.transform.Find("Suit3 head color2").gameObject;
                s3hb.gameObject.SetActive(true);
                s3hi.gameObject.SetActive(true);
                s3hc2.gameObject.SetActive(true);
            }
            else if (S3HC3 == true)
            {
                GameObject s3hb = HeadCUT.transform.Find("Suit3 head blood").gameObject;
                GameObject s3hi = HeadCUT.transform.Find("Suit3 head inner").gameObject;
                GameObject s3hc3 = HeadCUT.transform.Find("Suit3 head color3").gameObject;
                s3hb.gameObject.SetActive(true);
                s3hi.gameObject.SetActive(true);
                s3hc3.gameObject.SetActive(true);
            }
            else if (S3HC4 == true)
            {
                GameObject s3hb = HeadCUT.transform.Find("Suit3 head blood").gameObject;
                GameObject s3hi = HeadCUT.transform.Find("Suit3 head inner").gameObject;
                GameObject s3hc4 = HeadCUT.transform.Find("Suit3 head color4").gameObject;
                s3hb.gameObject.SetActive(true);
                s3hi.gameObject.SetActive(true);
                s3hc4.gameObject.SetActive(true);
            }
            else if (S3HC5 == true)
            {
                GameObject s3hb = HeadCUT.transform.Find("Suit3 head blood").gameObject;
                GameObject s3hi = HeadCUT.transform.Find("Suit3 head inner").gameObject;
                GameObject s3hc5 = HeadCUT.transform.Find("Suit3 head color5").gameObject;
                s3hb.gameObject.SetActive(true);
                s3hi.gameObject.SetActive(true);
                s3hc5.gameObject.SetActive(true);
            }
        }
    }

    void HandRcut()
    {
        if (HANDROUTonline == true)
        {
            GameObject HR = Instantiate(HandR, HandRpos.transform.position, HandRpos.transform.rotation);
            Destroy(HR, 30);

            HR.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                HR.GetComponent<TearCreateInfector>().Direction = false;
            else
                HR.GetComponent<TearCreateInfector>().Direction = true;

            if (HAND1R == true)
            {
                GameObject hand1r = HR.transform.Find("Hand3 right").gameObject;
                hand1r.gameObject.SetActive(true);
            }
            else if (HAND2R == true)
            {
                GameObject hand2r = HR.transform.Find("Hand3 right").gameObject;
                hand2r.gameObject.SetActive(true);
            }
            else if (HAND3R == true)
            {
                GameObject hand3r = HR.transform.Find("Hand3 right").gameObject;
                hand3r.gameObject.SetActive(true);
            }
            else if (G1HAND1RC1 == true)
            {
                GameObject g1hand1rc1 = HR.transform.Find("Grove1 right hand1 color1").gameObject;
                g1hand1rc1.gameObject.SetActive(true);
            }
            else if (G1HAND2RC1 == true)
            {
                GameObject g1hand2rc1 = HR.transform.Find("Grove1 right hand2 color1").gameObject;
                g1hand2rc1.gameObject.SetActive(true);
            }
            else if (G1HAND3RC1 == true)
            {
                GameObject g1hand3rc1 = HR.transform.Find("Grove1 right hand3 color1").gameObject;
                g1hand3rc1.gameObject.SetActive(true);
            }
            else if (G1HAND1RC2 == true)
            {
                GameObject g1hand1rc2 = HR.transform.Find("Grove1 right hand1 color2").gameObject;
                g1hand1rc2.gameObject.SetActive(true);
            }
            else if (G1HAND2RC2 == true)
            {
                GameObject g1hand2rc2 = HR.transform.Find("Grove1 right hand2 color2").gameObject;
                g1hand2rc2.gameObject.SetActive(true);
            }
            else if (G1HAND3RC2 == true)
            {
                GameObject g1hand3rc2 = HR.transform.Find("Grove1 right hand3 color2").gameObject;
                g1hand3rc2.gameObject.SetActive(true);
            }
            else if (G1HAND1RC3 == true)
            {
                GameObject g1hand1rc3 = HR.transform.Find("Grove1 right hand1 color3").gameObject;
                g1hand1rc3.gameObject.SetActive(true);
            }
            else if (G1HAND2RC3 == true)
            {
                GameObject g1hand2rc3 = HR.transform.Find("Grove1 right hand2 color3").gameObject;
                g1hand2rc3.gameObject.SetActive(true);
            }
            else if (G1HAND3RC3 == true)
            {
                GameObject g1hand3rc3 = HR.transform.Find("Grove1 right hand3 color3").gameObject;
                g1hand3rc3.gameObject.SetActive(true);
            }
            else if (G1HAND1RC4 == true)
            {
                GameObject g1hand1rc4 = HR.transform.Find("Grove1 right hand1 color4").gameObject;
                g1hand1rc4.gameObject.SetActive(true);
            }
            else if (G1HAND2RC4 == true)
            {
                GameObject g1hand2rc4 = HR.transform.Find("Grove1 right hand2 color4").gameObject;
                g1hand2rc4.gameObject.SetActive(true);
            }
            else if (G1HAND3RC4 == true)
            {
                GameObject g1hand3rc4 = HR.transform.Find("Grove1 right hand3 color4").gameObject;
                g1hand3rc4.gameObject.SetActive(true);
            }
            else if (G1HAND1RC5 == true)
            {
                GameObject g1hand1rc5 = HR.transform.Find("Grove1 right hand1 color5").gameObject;
                g1hand1rc5.gameObject.SetActive(true);
            }
            else if (G1HAND2RC5 == true)
            {
                GameObject g1hand2rc5 = HR.transform.Find("Grove1 right hand2 color5").gameObject;
                g1hand2rc5.gameObject.SetActive(true);
            }
            else if (G1HAND3RC5 == true)
            {
                GameObject g1hand3rc5 = HR.transform.Find("Grove1 right hand3 color5").gameObject;
                g1hand3rc5.gameObject.SetActive(true);
            }
        }
    }

    void RightArmDownHandcut()
    {
        if (RightArmDownHOUTonline == true)
        {
            GameObject RADH = Instantiate(RightArmDownHand, RightArmDownHandpos.transform.position, RightArmDownHandpos.transform.rotation);
            Destroy(RADH, 30);

            RADH.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RADH.GetComponent<TearCreateInfector>().Direction = false;
            else
                RADH.GetComponent<TearCreateInfector>().Direction = true;

            //오른팔 하단
            if (BODY1RDAR == true)
            {
                GameObject body1rdr = RADH.transform.Find("Body1 right down arm").gameObject;
                body1rdr.gameObject.SetActive(true);
            }
            else if (BODY2RDAR == true)
            {
                GameObject body2rdr = RADH.transform.Find("Body2 right down arm").gameObject;
                body2rdr.gameObject.SetActive(true);
            }
            if (CT1RDA == true)
            {
                GameObject ct1rda = RADH.transform.Find("Clothes top1 right down arm").gameObject;
                ct1rda.gameObject.SetActive(true);
            }
            if (S1TRDAC1 == true)
            {
                GameObject s1trdac1 = RADH.transform.Find("Suit1 top right down arm color1").gameObject;
                s1trdac1.gameObject.SetActive(true);
            }
            else if (S1TRDAC2 == true)
            {
                GameObject s1trdac2 = RADH.transform.Find("Suit1 top right down arm color2").gameObject;
                s1trdac2.gameObject.SetActive(true);
            }
            else if (S1TRDAC3 == true)
            {
                GameObject s1trdac3 = RADH.transform.Find("Suit1 top right down arm color3").gameObject;
                s1trdac3.gameObject.SetActive(true);
            }
            else if (S1TRDAC4 == true)
            {
                GameObject s1trdac4 = RADH.transform.Find("Suit1 top right down arm color4").gameObject;
                s1trdac4.gameObject.SetActive(true);
            }
            else if (S1TRDAC5 == true)
            {
                GameObject s1trdac5 = RADH.transform.Find("Suit1 top right down arm color5").gameObject;
                s1trdac5.gameObject.SetActive(true);
            }
            if (S2TRDACC1 == true)
            {
                GameObject s2trdacc1 = RADH.transform.Find("Suit2 top right down arm cloth color1").gameObject;
                s2trdacc1.gameObject.SetActive(true);
            }
            else if (S2TRDACC2 == true)
            {
                GameObject s2trdacc2 = RADH.transform.Find("Suit2 top right down arm cloth color2").gameObject;
                s2trdacc2.gameObject.SetActive(true);
            }
            else if (S2TRDACC3 == true)
            {
                GameObject s2trdacc3 = RADH.transform.Find("Suit2 top right down arm cloth color3").gameObject;
                s2trdacc3.gameObject.SetActive(true);
            }
            else if (S2TRDACC4 == true)
            {
                GameObject s2trdacc4 = RADH.transform.Find("Suit2 top right down arm cloth color4").gameObject;
                s2trdacc4.gameObject.SetActive(true);
            }
            else if (S2TRDACC5 == true)
            {
                GameObject s2trdacc5 = RADH.transform.Find("Suit2 top right down arm cloth color5").gameObject;
                s2trdacc5.gameObject.SetActive(true);
            }
            if (S2TRDAC1 == true)
            {
                GameObject s2trdac1 = RADH.transform.Find("Suit2 top right down arm color1").gameObject;
                s2trdac1.gameObject.SetActive(true);
            }
            else if (S2TRDAC2 == true)
            {
                GameObject s2trdac2 = RADH.transform.Find("Suit2 top right down arm color2").gameObject;
                s2trdac2.gameObject.SetActive(true);
            }
            else if (S2TRDAC3 == true)
            {
                GameObject s2trdac3 = RADH.transform.Find("Suit2 top right down arm color3").gameObject;
                s2trdac3.gameObject.SetActive(true);
            }
            else if (S2TRDAC4 == true)
            {
                GameObject s2trdac4 = RADH.transform.Find("Suit2 top right down arm color4").gameObject;
                s2trdac4.gameObject.SetActive(true);
            }
            else if (S2TRDAC5 == true)
            {
                GameObject s2trdac5 = RADH.transform.Find("Suit2 top right down arm color5").gameObject;
                s2trdac5.gameObject.SetActive(true);
            }
            if (S2TRDAL1 == true)
            {
                GameObject s2trdaL1 = RADH.transform.Find("Suit2 top right down arm LED1").gameObject;
                s2trdaL1.gameObject.SetActive(true);
            }
            else if (S2TRDAL2 == true)
            {
                GameObject s2trdaL2 = RADH.transform.Find("Suit2 top right down arm LED2").gameObject;
                s2trdaL2.gameObject.SetActive(true);
            }
            else if (S2TRDAL3 == true)
            {
                GameObject s2trdaL3 = RADH.transform.Find("Suit2 top right down arm LED3").gameObject;
                s2trdaL3.gameObject.SetActive(true);
            }
            else if (S2TRDAL4 == true)
            {
                GameObject s2trdaL4 = RADH.transform.Find("Suit2 top right down arm LED4").gameObject;
                s2trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s2trdaL5 = RADH.transform.Find("Suit2 top right down arm LED5").gameObject;
                s2trdaL5.gameObject.SetActive(true);
            }
            if (S3TRDACC1 == true)
            {
                GameObject s3trdacc1 = RADH.transform.Find("Suit3 top right down arm cloth color1").gameObject;
                s3trdacc1.gameObject.SetActive(true);
            }
            else if (S3TRDACC2 == true)
            {
                GameObject s3trdacc2 = RADH.transform.Find("Suit3 top right down arm cloth color2").gameObject;
                s3trdacc2.gameObject.SetActive(true);
            }
            else if (S3TRDACC3 == true)
            {
                GameObject s3trdacc3 = RADH.transform.Find("Suit3 top right down arm cloth color3").gameObject;
                s3trdacc3.gameObject.SetActive(true);
            }
            else if (S3TRDACC4 == true)
            {
                GameObject s3trdacc4 = RADH.transform.Find("Suit3 top right down arm cloth color4").gameObject;
                s3trdacc4.gameObject.SetActive(true);
            }
            else if (S3TRDACC5 == true)
            {
                GameObject s3trdacc5 = RADH.transform.Find("Suit3 top right down arm cloth color5").gameObject;
                s3trdacc5.gameObject.SetActive(true);
            }
            if (S3TRDAC1 == true)
            {
                GameObject s3trdac1 = RADH.transform.Find("Suit3 top right down arm color1").gameObject;
                s3trdac1.gameObject.SetActive(true);
            }
            else if (S3TRDAC2 == true)
            {
                GameObject s3trdac2 = RADH.transform.Find("Suit3 top right down arm color2").gameObject;
                s3trdac2.gameObject.SetActive(true);
            }
            else if (S3TRDAC3 == true)
            {
                GameObject s3trdac3 = RADH.transform.Find("Suit3 top right down arm color3").gameObject;
                s3trdac3.gameObject.SetActive(true);
            }
            else if (S3TRDAC4 == true)
            {
                GameObject s3trdac4 = RADH.transform.Find("Suit3 top right down arm color4").gameObject;
                s3trdac4.gameObject.SetActive(true);
            }
            else if (S3TRDAC5 == true)
            {
                GameObject s3trdac5 = RADH.transform.Find("Suit3 top right down arm color5").gameObject;
                s3trdac5.gameObject.SetActive(true);
            }
            if (S3TRDAL1 == true)
            {
                GameObject s3trdaL1 = RADH.transform.Find("Suit3 top right down arm LED1").gameObject;
                s3trdaL1.gameObject.SetActive(true);
            }
            else if (S3TRDAL2 == true)
            {
                GameObject s3trdaL2 = RADH.transform.Find("Suit3 top right down arm LED2").gameObject;
                s3trdaL2.gameObject.SetActive(true);
            }
            else if (S3TRDAL3 == true)
            {
                GameObject s3trdaL3 = RADH.transform.Find("Suit3 top right down arm LED3").gameObject;
                s3trdaL3.gameObject.SetActive(true);
            }
            else if (S3TRDAL4 == true)
            {
                GameObject s3trdaL4 = RADH.transform.Find("Suit3 top right down arm LED4").gameObject;
                s3trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s3trdaL5 = RADH.transform.Find("Suit3 top right down arm LED5").gameObject;
                s3trdaL5.gameObject.SetActive(true);
            }

            //손
            if (HAND1R == true)
            {
                GameObject hand1r = RADH.transform.Find("Hand3 right").gameObject;
                hand1r.gameObject.SetActive(true);
            }
            else if (HAND2R == true)
            {
                GameObject hand2r = RADH.transform.Find("Hand3 right").gameObject;
                hand2r.gameObject.SetActive(true);
            }
            else if (HAND3R == true)
            {
                GameObject hand3r = RADH.transform.Find("Hand3 right").gameObject;
                hand3r.gameObject.SetActive(true);
            }
            else if (G1HAND1RC1 == true)
            {
                GameObject g1hand1rc1 = RADH.transform.Find("Grove1 right hand1 color1").gameObject;
                g1hand1rc1.gameObject.SetActive(true);
            }
            else if (G1HAND2RC1 == true)
            {
                GameObject g1hand2rc1 = RADH.transform.Find("Grove1 right hand2 color1").gameObject;
                g1hand2rc1.gameObject.SetActive(true);
            }
            else if (G1HAND3RC1 == true)
            {
                GameObject g1hand3rc1 = RADH.transform.Find("Grove1 right hand3 color1").gameObject;
                g1hand3rc1.gameObject.SetActive(true);
            }
            else if (G1HAND1RC2 == true)
            {
                GameObject g1hand1rc2 = RADH.transform.Find("Grove1 right hand1 color2").gameObject;
                g1hand1rc2.gameObject.SetActive(true);
            }
            else if (G1HAND2RC2 == true)
            {
                GameObject g1hand2rc2 = RADH.transform.Find("Grove1 right hand2 color2").gameObject;
                g1hand2rc2.gameObject.SetActive(true);
            }
            else if (G1HAND3RC2 == true)
            {
                GameObject g1hand3rc2 = RADH.transform.Find("Grove1 right hand3 color2").gameObject;
                g1hand3rc2.gameObject.SetActive(true);
            }
            else if (G1HAND1RC3 == true)
            {
                GameObject g1hand1rc3 = RADH.transform.Find("Grove1 right hand1 color3").gameObject;
                g1hand1rc3.gameObject.SetActive(true);
            }
            else if (G1HAND2RC3 == true)
            {
                GameObject g1hand2rc3 = RADH.transform.Find("Grove1 right hand2 color3").gameObject;
                g1hand2rc3.gameObject.SetActive(true);
            }
            else if (G1HAND3RC3 == true)
            {
                GameObject g1hand3rc3 = RADH.transform.Find("Grove1 right hand3 color3").gameObject;
                g1hand3rc3.gameObject.SetActive(true);
            }
            else if (G1HAND1RC4 == true)
            {
                GameObject g1hand1rc4 = RADH.transform.Find("Grove1 right hand1 color4").gameObject;
                g1hand1rc4.gameObject.SetActive(true);
            }
            else if (G1HAND2RC4 == true)
            {
                GameObject g1hand2rc4 = RADH.transform.Find("Grove1 right hand2 color4").gameObject;
                g1hand2rc4.gameObject.SetActive(true);
            }
            else if (G1HAND3RC4 == true)
            {
                GameObject g1hand3rc4 = RADH.transform.Find("Grove1 right hand3 color4").gameObject;
                g1hand3rc4.gameObject.SetActive(true);
            }
            else if (G1HAND1RC5 == true)
            {
                GameObject g1hand1rc5 = RADH.transform.Find("Grove1 right hand1 color5").gameObject;
                g1hand1rc5.gameObject.SetActive(true);
            }
            else if (G1HAND2RC5 == true)
            {
                GameObject g1hand2rc5 = RADH.transform.Find("Grove1 right hand2 color5").gameObject;
                g1hand2rc5.gameObject.SetActive(true);
            }
            else if (G1HAND3RC5 == true)
            {
                GameObject g1hand3rc5 = RADH.transform.Find("Grove1 right hand3 color5").gameObject;
                g1hand3rc5.gameObject.SetActive(true);
            }
        }
    }

    void RightArmDowncut()
    {
        if (RightArmDownOUTonline == true)
        {
            GameObject RAD = Instantiate(RightArmDown, RightArmDownpos.transform.position, RightArmDownpos.transform.rotation);
            Destroy(RAD, 30);

            RAD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RAD.GetComponent<TearCreateInfector>().Direction = false;
            else
                RAD.GetComponent<TearCreateInfector>().Direction = true;

            //오른팔 하단
            if (BODY1RDAR == true)
            {
                GameObject body1rda = RAD.transform.Find("Body1 right down arm").gameObject;
                body1rda.gameObject.SetActive(true);
            }
            else if (BODY2RDAR == true)
            {
                GameObject body2rda = RAD.transform.Find("Body2 right down arm").gameObject;
                body2rda.gameObject.SetActive(true);
            }
            if (CT1RDA == true)
            {
                GameObject ct1rda = RAD.transform.Find("Clothes top1 right down arm").gameObject;
                ct1rda.gameObject.SetActive(true);
            }
            if (S1TRDAC1 == true)
            {
                GameObject s1trdac1 = RAD.transform.Find("Suit1 top right down arm color1").gameObject;
                s1trdac1.gameObject.SetActive(true);
            }
            else if (S1TRDAC2 == true)
            {
                GameObject s1trdac2 = RAD.transform.Find("Suit1 top right down arm color2").gameObject;
                s1trdac2.gameObject.SetActive(true);
            }
            else if (S1TRDAC3 == true)
            {
                GameObject s1trdac3 = RAD.transform.Find("Suit1 top right down arm color3").gameObject;
                s1trdac3.gameObject.SetActive(true);
            }
            else if (S1TRDAC4 == true)
            {
                GameObject s1trdac4 = RAD.transform.Find("Suit1 top right down arm color4").gameObject;
                s1trdac4.gameObject.SetActive(true);
            }
            else if (S1TRDAC5 == true)
            {
                GameObject s1trdac5 = RAD.transform.Find("Suit1 top right down arm color5").gameObject;
                s1trdac5.gameObject.SetActive(true);
            }
            if (S2TRDACC1 == true)
            {
                GameObject s2trdacc1 = RAD.transform.Find("Suit2 top right down arm cloth color1").gameObject;
                s2trdacc1.gameObject.SetActive(true);
            }
            else if (S2TRDACC2 == true)
            {
                GameObject s2trdacc2 = RAD.transform.Find("Suit2 top right down arm cloth color2").gameObject;
                s2trdacc2.gameObject.SetActive(true);
            }
            else if (S2TRDACC3 == true)
            {
                GameObject s2trdacc3 = RAD.transform.Find("Suit2 top right down arm cloth color3").gameObject;
                s2trdacc3.gameObject.SetActive(true);
            }
            else if (S2TRDACC4 == true)
            {
                GameObject s2trdacc4 = RAD.transform.Find("Suit2 top right down arm cloth color4").gameObject;
                s2trdacc4.gameObject.SetActive(true);
            }
            else if (S2TRDACC5 == true)
            {
                GameObject s2trdacc5 = RAD.transform.Find("Suit2 top right down arm cloth color5").gameObject;
                s2trdacc5.gameObject.SetActive(true);
            }
            if (S2TRDAC1 == true)
            {
                GameObject s2trdac1 = RAD.transform.Find("Suit2 top right down arm color1").gameObject;
                s2trdac1.gameObject.SetActive(true);
            }
            else if (S2TRDAC2 == true)
            {
                GameObject s2trdac2 = RAD.transform.Find("Suit2 top right down arm color2").gameObject;
                s2trdac2.gameObject.SetActive(true);
            }
            else if (S2TRDAC3 == true)
            {
                GameObject s2trdac3 = RAD.transform.Find("Suit2 top right down arm color3").gameObject;
                s2trdac3.gameObject.SetActive(true);
            }
            else if (S2TRDAC4 == true)
            {
                GameObject s2trdac4 = RAD.transform.Find("Suit2 top right down arm color4").gameObject;
                s2trdac4.gameObject.SetActive(true);
            }
            else if (S2TRDAC5 == true)
            {
                GameObject s2trdac5 = RAD.transform.Find("Suit2 top right down arm color5").gameObject;
                s2trdac5.gameObject.SetActive(true);
            }
            if (S2TRDAL1 == true)
            {
                GameObject s2trdaL1 = RAD.transform.Find("Suit2 top right down arm LED1").gameObject;
                s2trdaL1.gameObject.SetActive(true);
            }
            else if (S2TRDAL2 == true)
            {
                GameObject s2trdaL2 = RAD.transform.Find("Suit2 top right down arm LED2").gameObject;
                s2trdaL2.gameObject.SetActive(true);
            }
            else if (S2TRDAL3 == true)
            {
                GameObject s2trdaL3 = RAD.transform.Find("Suit2 top right down arm LED3").gameObject;
                s2trdaL3.gameObject.SetActive(true);
            }
            else if (S2TRDAL4 == true)
            {
                GameObject s2trdaL4 = RAD.transform.Find("Suit2 top right down arm LED4").gameObject;
                s2trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s2trdaL5 = RAD.transform.Find("Suit2 top right down arm LED5").gameObject;
                s2trdaL5.gameObject.SetActive(true);
            }
            if (S3TRDACC1 == true)
            {
                GameObject s3trdacc1 = RAD.transform.Find("Suit3 top right down arm cloth color1").gameObject;
                s3trdacc1.gameObject.SetActive(true);
            }
            else if (S3TRDACC2 == true)
            {
                GameObject s3trdacc2 = RAD.transform.Find("Suit3 top right down arm cloth color2").gameObject;
                s3trdacc2.gameObject.SetActive(true);
            }
            else if (S3TRDACC3 == true)
            {
                GameObject s3trdacc3 = RAD.transform.Find("Suit3 top right down arm cloth color3").gameObject;
                s3trdacc3.gameObject.SetActive(true);
            }
            else if (S3TRDACC4 == true)
            {
                GameObject s3trdacc4 = RAD.transform.Find("Suit3 top right down arm cloth color4").gameObject;
                s3trdacc4.gameObject.SetActive(true);
            }
            else if (S3TRDACC5 == true)
            {
                GameObject s3trdacc5 = RAD.transform.Find("Suit3 top right down arm cloth color5").gameObject;
                s3trdacc5.gameObject.SetActive(true);
            }
            if (S3TRDAC1 == true)
            {
                GameObject s3trdac1 = RAD.transform.Find("Suit3 top right down arm color1").gameObject;
                s3trdac1.gameObject.SetActive(true);
            }
            else if (S3TRDAC2 == true)
            {
                GameObject s3trdac2 = RAD.transform.Find("Suit3 top right down arm color2").gameObject;
                s3trdac2.gameObject.SetActive(true);
            }
            else if (S3TRDAC3 == true)
            {
                GameObject s3trdac3 = RAD.transform.Find("Suit3 top right down arm color3").gameObject;
                s3trdac3.gameObject.SetActive(true);
            }
            else if (S3TRDAC4 == true)
            {
                GameObject s3trdac4 = RAD.transform.Find("Suit3 top right down arm color4").gameObject;
                s3trdac4.gameObject.SetActive(true);
            }
            else if (S3TRDAC5 == true)
            {
                GameObject s3trdac5 = RAD.transform.Find("Suit3 top right down arm color5").gameObject;
                s3trdac5.gameObject.SetActive(true);
            }
            if (S3TRDAL1 == true)
            {
                GameObject s3trdaL1 = RAD.transform.Find("Suit3 top right down arm LED1").gameObject;
                s3trdaL1.gameObject.SetActive(true);
            }
            else if (S3TRDAL2 == true)
            {
                GameObject s3trdaL2 = RAD.transform.Find("Suit3 top right down arm LED2").gameObject;
                s3trdaL2.gameObject.SetActive(true);
            }
            else if (S3TRDAL3 == true)
            {
                GameObject s3trdaL3 = RAD.transform.Find("Suit3 top right down arm LED3").gameObject;
                s3trdaL3.gameObject.SetActive(true);
            }
            else if (S3TRDAL4 == true)
            {
                GameObject s3trdaL4 = RAD.transform.Find("Suit3 top right down arm LED4").gameObject;
                s3trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s3trdaL5 = RAD.transform.Find("Suit3 top right down arm LED5").gameObject;
                s3trdaL5.gameObject.SetActive(true);
            }
        }
    }

    void RightArmUpDownHandcut()
    {
        if (RightArmUpDownHOUTonline == true)
        {
            GameObject RAUDH = Instantiate(RightArmUpDownHand, RightArmUpDownHandpos.transform.position, RightArmUpDownHandpos.transform.rotation);
            Destroy(RAUDH, 30);

            RAUDH.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RAUDH.GetComponent<TearCreateInfector>().Direction = false;
            else
                RAUDH.GetComponent<TearCreateInfector>().Direction = true;

            //오른팔 상단
            if (BODY1RUA == true)
            {
                GameObject body1rua = RAUDH.transform.Find("Body1 right up arm").gameObject;
                body1rua.gameObject.SetActive(true);
            }
            else if (BODY2RUA == true)
            {
                GameObject body2rua = RAUDH.transform.Find("Body2 right up arm").gameObject;
                body2rua.gameObject.SetActive(true);
            }
            if (CT1RUA == true)
            {
                GameObject ct1rua = RAUDH.transform.Find("Clothes top1 right up arm").gameObject;
                ct1rua.gameObject.SetActive(true);
            }
            else if (CT2RA == true)
            {
                GameObject ct2ra = RAUDH.transform.Find("Clothes top2 right arm").gameObject;
                ct2ra.gameObject.SetActive(true);
            }
            if (S1TRUAC1 == true)
            {
                GameObject s1truac1 = RAUDH.transform.Find("Suit1 top right up arm color1").gameObject;
                s1truac1.gameObject.SetActive(true);
            }
            else if (S1TRUAC2 == true)
            {
                GameObject s1truac2 = RAUDH.transform.Find("Suit1 top right up arm color2").gameObject;
                s1truac2.gameObject.SetActive(true);
            }
            else if (S1TRUAC3 == true)
            {
                GameObject s1truac3 = RAUDH.transform.Find("Suit1 top right up arm color3").gameObject;
                s1truac3.gameObject.SetActive(true);
            }
            else if (S1TRUAC4 == true)
            {
                GameObject s1truac4 = RAUDH.transform.Find("Suit1 top right up arm color4").gameObject;
                s1truac4.gameObject.SetActive(true);
            }
            else if (S1TRUAC5 == true)
            {
                GameObject s1truac5 = RAUDH.transform.Find("Suit1 top right up arm color5").gameObject;
                s1truac5.gameObject.SetActive(true);
            }
            if (S2TRUAC1 == true)
            {
                GameObject s2truac1 = RAUDH.transform.Find("Suit2 top right up arm color1").gameObject;
                s2truac1.gameObject.SetActive(true);
            }
            else if (S2TRUAC2 == true)
            {
                GameObject s2truac2 = RAUDH.transform.Find("Suit2 top right up arm color2").gameObject;
                s2truac2.gameObject.SetActive(true);
            }
            else if (S2TRUAC3 == true)
            {
                GameObject s2truac3 = RAUDH.transform.Find("Suit2 top right up arm color3").gameObject;
                s2truac3.gameObject.SetActive(true);
            }
            else if (S2TRUAC4 == true)
            {
                GameObject s2truac4 = RAUDH.transform.Find("Suit2 top right up arm color4").gameObject;
                s2truac4.gameObject.SetActive(true);
            }
            else if (S2TRUAC5 == true)
            {
                GameObject s2truac5 = RAUDH.transform.Find("Suit2 top right up arm color5").gameObject;
                s2truac5.gameObject.SetActive(true);
            }
            if (S3TRUAC1 == true)
            {
                GameObject s3truac1 = RAUDH.transform.Find("Suit3 top right up arm color1").gameObject;
                s3truac1.gameObject.SetActive(true);
            }
            else if (S3TRUAC2 == true)
            {
                GameObject s3truac2 = RAUDH.transform.Find("Suit3 top right up arm color2").gameObject;
                s3truac2.gameObject.SetActive(true);
            }
            else if (S3TRUAC3 == true)
            {
                GameObject s3truac3 = RAUDH.transform.Find("Suit3 top right up arm color3").gameObject;
                s3truac3.gameObject.SetActive(true);
            }
            else if (S3TRUAC4 == true)
            {
                GameObject s3truac4 = RAUDH.transform.Find("Suit3 top right up arm color4").gameObject;
                s3truac4.gameObject.SetActive(true);
            }
            else if (S3TRUAC5 == true)
            {
                GameObject s3truac5 = RAUDH.transform.Find("Suit3 top right up arm color5").gameObject;
                s3truac5.gameObject.SetActive(true);
            }

            //오른팔 하단
            if (BODY1RDAR == true)
            {
                GameObject body1rdr = RAUDH.transform.Find("Body1 right down arm").gameObject;
                body1rdr.gameObject.SetActive(true);
            }
            else if (BODY2RDAR == true)
            {
                GameObject body2rdr = RAUDH.transform.Find("Body2 right down arm").gameObject;
                body2rdr.gameObject.SetActive(true);
            }
            if (CT1RDA == true)
            {
                GameObject ct1rda = RAUDH.transform.Find("Clothes top1 right down arm").gameObject;
                ct1rda.gameObject.SetActive(true);
            }
            if (S1TRDAC1 == true)
            {
                GameObject s1trdac1 = RAUDH.transform.Find("Suit1 top right down arm color1").gameObject;
                s1trdac1.gameObject.SetActive(true);
            }
            else if (S1TRDAC2 == true)
            {
                GameObject s1trdac2 = RAUDH.transform.Find("Suit1 top right down arm color2").gameObject;
                s1trdac2.gameObject.SetActive(true);
            }
            else if (S1TRDAC3 == true)
            {
                GameObject s1trdac3 = RAUDH.transform.Find("Suit1 top right down arm color3").gameObject;
                s1trdac3.gameObject.SetActive(true);
            }
            else if (S1TRDAC4 == true)
            {
                GameObject s1trdac4 = RAUDH.transform.Find("Suit1 top right down arm color4").gameObject;
                s1trdac4.gameObject.SetActive(true);
            }
            else if (S1TRDAC5 == true)
            {
                GameObject s1trdac5 = RAUDH.transform.Find("Suit1 top right down arm color5").gameObject;
                s1trdac5.gameObject.SetActive(true);
            }
            if (S2TRDACC1 == true)
            {
                GameObject s2trdacc1 = RAUDH.transform.Find("Suit2 top right down arm cloth color1").gameObject;
                s2trdacc1.gameObject.SetActive(true);
            }
            else if (S2TRDACC2 == true)
            {
                GameObject s2trdacc2 = RAUDH.transform.Find("Suit2 top right down arm cloth color2").gameObject;
                s2trdacc2.gameObject.SetActive(true);
            }
            else if (S2TRDACC3 == true)
            {
                GameObject s2trdacc3 = RAUDH.transform.Find("Suit2 top right down arm cloth color3").gameObject;
                s2trdacc3.gameObject.SetActive(true);
            }
            else if (S2TRDACC4 == true)
            {
                GameObject s2trdacc4 = RAUDH.transform.Find("Suit2 top right down arm cloth color4").gameObject;
                s2trdacc4.gameObject.SetActive(true);
            }
            else if (S2TRDACC5 == true)
            {
                GameObject s2trdacc5 = RAUDH.transform.Find("Suit2 top right down arm cloth color5").gameObject;
                s2trdacc5.gameObject.SetActive(true);
            }
            if (S2TRDAC1 == true)
            {
                GameObject s2trdac1 = RAUDH.transform.Find("Suit2 top right down arm color1").gameObject;
                s2trdac1.gameObject.SetActive(true);
            }
            else if (S2TRDAC2 == true)
            {
                GameObject s2trdac2 = RAUDH.transform.Find("Suit2 top right down arm color2").gameObject;
                s2trdac2.gameObject.SetActive(true);
            }
            else if (S2TRDAC3 == true)
            {
                GameObject s2trdac3 = RAUDH.transform.Find("Suit2 top right down arm color3").gameObject;
                s2trdac3.gameObject.SetActive(true);
            }
            else if (S2TRDAC4 == true)
            {
                GameObject s2trdac4 = RAUDH.transform.Find("Suit2 top right down arm color4").gameObject;
                s2trdac4.gameObject.SetActive(true);
            }
            else if (S2TRDAC5 == true)
            {
                GameObject s2trdac5 = RAUDH.transform.Find("Suit2 top right down arm color5").gameObject;
                s2trdac5.gameObject.SetActive(true);
            }
            if (S2TRDAL1 == true)
            {
                GameObject s2trdaL1 = RAUDH.transform.Find("Suit2 top right down arm LED1").gameObject;
                s2trdaL1.gameObject.SetActive(true);
            }
            else if (S2TRDAL2 == true)
            {
                GameObject s2trdaL2 = RAUDH.transform.Find("Suit2 top right down arm LED2").gameObject;
                s2trdaL2.gameObject.SetActive(true);
            }
            else if (S2TRDAL3 == true)
            {
                GameObject s2trdaL3 = RAUDH.transform.Find("Suit2 top right down arm LED3").gameObject;
                s2trdaL3.gameObject.SetActive(true);
            }
            else if (S2TRDAL4 == true)
            {
                GameObject s2trdaL4 = RAUDH.transform.Find("Suit2 top right down arm LED4").gameObject;
                s2trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s2trdaL5 = RAUDH.transform.Find("Suit2 top right down arm LED5").gameObject;
                s2trdaL5.gameObject.SetActive(true);
            }
            if (S3TRDACC1 == true)
            {
                GameObject s3trdacc1 = RAUDH.transform.Find("Suit3 top right down arm cloth color1").gameObject;
                s3trdacc1.gameObject.SetActive(true);
            }
            else if (S3TRDACC2 == true)
            {
                GameObject s3trdacc2 = RAUDH.transform.Find("Suit3 top right down arm cloth color2").gameObject;
                s3trdacc2.gameObject.SetActive(true);
            }
            else if (S3TRDACC3 == true)
            {
                GameObject s3trdacc3 = RAUDH.transform.Find("Suit3 top right down arm cloth color3").gameObject;
                s3trdacc3.gameObject.SetActive(true);
            }
            else if (S3TRDACC4 == true)
            {
                GameObject s3trdacc4 = RAUDH.transform.Find("Suit3 top right down arm cloth color4").gameObject;
                s3trdacc4.gameObject.SetActive(true);
            }
            else if (S3TRDACC5 == true)
            {
                GameObject s3trdacc5 = RAUDH.transform.Find("Suit3 top right down arm cloth color5").gameObject;
                s3trdacc5.gameObject.SetActive(true);
            }
            if (S3TRDAC1 == true)
            {
                GameObject s3trdac1 = RAUDH.transform.Find("Suit3 top right down arm color1").gameObject;
                s3trdac1.gameObject.SetActive(true);
            }
            else if (S3TRDAC2 == true)
            {
                GameObject s3trdac2 = RAUDH.transform.Find("Suit3 top right down arm color2").gameObject;
                s3trdac2.gameObject.SetActive(true);
            }
            else if (S3TRDAC3 == true)
            {
                GameObject s3trdac3 = RAUDH.transform.Find("Suit3 top right down arm color3").gameObject;
                s3trdac3.gameObject.SetActive(true);
            }
            else if (S3TRDAC4 == true)
            {
                GameObject s3trdac4 = RAUDH.transform.Find("Suit3 top right down arm color4").gameObject;
                s3trdac4.gameObject.SetActive(true);
            }
            else if (S3TRDAC5 == true)
            {
                GameObject s3trdac5 = RAUDH.transform.Find("Suit3 top right down arm color5").gameObject;
                s3trdac5.gameObject.SetActive(true);
            }
            if (S3TRDAL1 == true)
            {
                GameObject s3trdaL1 = RAUDH.transform.Find("Suit3 top right down arm LED1").gameObject;
                s3trdaL1.gameObject.SetActive(true);
            }
            else if (S3TRDAL2 == true)
            {
                GameObject s3trdaL2 = RAUDH.transform.Find("Suit3 top right down arm LED2").gameObject;
                s3trdaL2.gameObject.SetActive(true);
            }
            else if (S3TRDAL3 == true)
            {
                GameObject s3trdaL3 = RAUDH.transform.Find("Suit3 top right down arm LED3").gameObject;
                s3trdaL3.gameObject.SetActive(true);
            }
            else if (S3TRDAL4 == true)
            {
                GameObject s3trdaL4 = RAUDH.transform.Find("Suit3 top right down arm LED4").gameObject;
                s3trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s3trdaL5 = RAUDH.transform.Find("Suit3 top right down arm LED5").gameObject;
                s3trdaL5.gameObject.SetActive(true);
            }

            //손
            if (HAND1R == true)
            {
                GameObject hand1r = RAUDH.transform.Find("Hand3 right").gameObject;
                hand1r.gameObject.SetActive(true);
            }
            else if (HAND2R == true)
            {
                GameObject hand2r = RAUDH.transform.Find("Hand3 right").gameObject;
                hand2r.gameObject.SetActive(true);
            }
            else if (HAND3R == true)
            {
                GameObject hand3r = RAUDH.transform.Find("Hand3 right").gameObject;
                hand3r.gameObject.SetActive(true);
            }
            else if (G1HAND1RC1 == true)
            {
                GameObject g1hand1rc1 = RAUDH.transform.Find("Grove1 right hand1 color1").gameObject;
                g1hand1rc1.gameObject.SetActive(true);
            }
            else if (G1HAND2RC1 == true)
            {
                GameObject g1hand2rc1 = RAUDH.transform.Find("Grove1 right hand2 color1").gameObject;
                g1hand2rc1.gameObject.SetActive(true);
            }
            else if (G1HAND3RC1 == true)
            {
                GameObject g1hand3rc1 = RAUDH.transform.Find("Grove1 right hand3 color1").gameObject;
                g1hand3rc1.gameObject.SetActive(true);
            }
            else if (G1HAND1RC2 == true)
            {
                GameObject g1hand1rc2 = RAUDH.transform.Find("Grove1 right hand1 color2").gameObject;
                g1hand1rc2.gameObject.SetActive(true);
            }
            else if (G1HAND2RC2 == true)
            {
                GameObject g1hand2rc2 = RAUDH.transform.Find("Grove1 right hand2 color2").gameObject;
                g1hand2rc2.gameObject.SetActive(true);
            }
            else if (G1HAND3RC2 == true)
            {
                GameObject g1hand3rc2 = RAUDH.transform.Find("Grove1 right hand3 color2").gameObject;
                g1hand3rc2.gameObject.SetActive(true);
            }
            else if (G1HAND1RC3 == true)
            {
                GameObject g1hand1rc3 = RAUDH.transform.Find("Grove1 right hand1 color3").gameObject;
                g1hand1rc3.gameObject.SetActive(true);
            }
            else if (G1HAND2RC3 == true)
            {
                GameObject g1hand2rc3 = RAUDH.transform.Find("Grove1 right hand2 color3").gameObject;
                g1hand2rc3.gameObject.SetActive(true);
            }
            else if (G1HAND3RC3 == true)
            {
                GameObject g1hand3rc3 = RAUDH.transform.Find("Grove1 right hand3 color3").gameObject;
                g1hand3rc3.gameObject.SetActive(true);
            }
            else if (G1HAND1RC4 == true)
            {
                GameObject g1hand1rc4 = RAUDH.transform.Find("Grove1 right hand1 color4").gameObject;
                g1hand1rc4.gameObject.SetActive(true);
            }
            else if (G1HAND2RC4 == true)
            {
                GameObject g1hand2rc4 = RAUDH.transform.Find("Grove1 right hand2 color4").gameObject;
                g1hand2rc4.gameObject.SetActive(true);
            }
            else if (G1HAND3RC4 == true)
            {
                GameObject g1hand3rc4 = RAUDH.transform.Find("Grove1 right hand3 color4").gameObject;
                g1hand3rc4.gameObject.SetActive(true);
            }
            else if (G1HAND1RC5 == true)
            {
                GameObject g1hand1rc5 = RAUDH.transform.Find("Grove1 right hand1 color5").gameObject;
                g1hand1rc5.gameObject.SetActive(true);
            }
            else if (G1HAND2RC5 == true)
            {
                GameObject g1hand2rc5 = RAUDH.transform.Find("Grove1 right hand2 color5").gameObject;
                g1hand2rc5.gameObject.SetActive(true);
            }
            else if (G1HAND3RC5 == true)
            {
                GameObject g1hand3rc5 = RAUDH.transform.Find("Grove1 right hand3 color5").gameObject;
                g1hand3rc5.gameObject.SetActive(true);
            }
        }
    }

    void RightArmUpDowncut()
    {
        if (RightArmUpDownOUTonline == true)
        {
            GameObject RAUD = Instantiate(RightArmUpDown, RightArmUpDownpos.transform.position, RightArmUpDownpos.transform.rotation);
            Destroy(RAUD, 30);

            RAUD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RAUD.GetComponent<TearCreateInfector>().Direction = false;
            else
                RAUD.GetComponent<TearCreateInfector>().Direction = true;

            //오른팔 상단
            if (BODY1RUA == true)
            {
                GameObject body1rua = RAUD.transform.Find("Body1 right up arm").gameObject;
                body1rua.gameObject.SetActive(true);
            }
            else if (BODY2RUA == true)
            {
                GameObject body2rua = RAUD.transform.Find("Body2 right up arm").gameObject;
                body2rua.gameObject.SetActive(true);
            }
            if (CT1RUA == true)
            {
                GameObject ct1rua = RAUD.transform.Find("Clothes top1 right up arm").gameObject;
                ct1rua.gameObject.SetActive(true);
            }
            else if (CT2RA == true)
            {
                GameObject ct2ra = RAUD.transform.Find("Clothes top2 right arm").gameObject;
                ct2ra.gameObject.SetActive(true);
            }
            if (S1TRUAC1 == true)
            {
                GameObject s1truac1 = RAUD.transform.Find("Suit1 top right up arm color1").gameObject;
                s1truac1.gameObject.SetActive(true);
            }
            else if (S1TRUAC2 == true)
            {
                GameObject s1truac2 = RAUD.transform.Find("Suit1 top right up arm color2").gameObject;
                s1truac2.gameObject.SetActive(true);
            }
            else if (S1TRUAC3 == true)
            {
                GameObject s1truac3 = RAUD.transform.Find("Suit1 top right up arm color3").gameObject;
                s1truac3.gameObject.SetActive(true);
            }
            else if (S1TRUAC4 == true)
            {
                GameObject s1truac4 = RAUD.transform.Find("Suit1 top right up arm color4").gameObject;
                s1truac4.gameObject.SetActive(true);
            }
            else if (S1TRUAC5 == true)
            {
                GameObject s1truac5 = RAUD.transform.Find("Suit1 top right up arm color5").gameObject;
                s1truac5.gameObject.SetActive(true);
            }
            if (S2TRUAC1 == true)
            {
                GameObject s2truac1 = RAUD.transform.Find("Suit2 top right up arm color1").gameObject;
                s2truac1.gameObject.SetActive(true);
            }
            else if (S2TRUAC2 == true)
            {
                GameObject s2truac2 = RAUD.transform.Find("Suit2 top right up arm color2").gameObject;
                s2truac2.gameObject.SetActive(true);
            }
            else if (S2TRUAC3 == true)
            {
                GameObject s2truac3 = RAUD.transform.Find("Suit2 top right up arm color3").gameObject;
                s2truac3.gameObject.SetActive(true);
            }
            else if (S2TRUAC4 == true)
            {
                GameObject s2truac4 = RAUD.transform.Find("Suit2 top right up arm color4").gameObject;
                s2truac4.gameObject.SetActive(true);
            }
            else if (S2TRUAC5 == true)
            {
                GameObject s2truac5 = RAUD.transform.Find("Suit2 top right up arm color5").gameObject;
                s2truac5.gameObject.SetActive(true);
            }
            if (S3TRUAC1 == true)
            {
                GameObject s3truac1 = RAUD.transform.Find("Suit3 top right up arm color1").gameObject;
                s3truac1.gameObject.SetActive(true);
            }
            else if (S3TRUAC2 == true)
            {
                GameObject s3truac2 = RAUD.transform.Find("Suit3 top right up arm color2").gameObject;
                s3truac2.gameObject.SetActive(true);
            }
            else if (S3TRUAC3 == true)
            {
                GameObject s3truac3 = RAUD.transform.Find("Suit3 top right up arm color3").gameObject;
                s3truac3.gameObject.SetActive(true);
            }
            else if (S3TRUAC4 == true)
            {
                GameObject s3truac4 = RAUD.transform.Find("Suit3 top right up arm color4").gameObject;
                s3truac4.gameObject.SetActive(true);
            }
            else if (S3TRUAC5 == true)
            {
                GameObject s3truac5 = RAUD.transform.Find("Suit3 top right up arm color5").gameObject;
                s3truac5.gameObject.SetActive(true);
            }

            //오른팔 하단
            if (BODY1RDAR == true)
            {
                GameObject body1rdr = RAUD.transform.Find("Body1 right down arm").gameObject;
                body1rdr.gameObject.SetActive(true);
            }
            else if (BODY2RDAR == true)
            {
                GameObject body2rdr = RAUD.transform.Find("Body2 right down arm").gameObject;
                body2rdr.gameObject.SetActive(true);
            }
            if (CT1RDA == true)
            {
                GameObject ct1rda = RAUD.transform.Find("Clothes top1 right down arm").gameObject;
                ct1rda.gameObject.SetActive(true);
            }
            if (S1TRDAC1 == true)
            {
                GameObject s1trdac1 = RAUD.transform.Find("Suit1 top right down arm color1").gameObject;
                s1trdac1.gameObject.SetActive(true);
            }
            else if (S1TRDAC2 == true)
            {
                GameObject s1trdac2 = RAUD.transform.Find("Suit1 top right down arm color2").gameObject;
                s1trdac2.gameObject.SetActive(true);
            }
            else if (S1TRDAC3 == true)
            {
                GameObject s1trdac3 = RAUD.transform.Find("Suit1 top right down arm color3").gameObject;
                s1trdac3.gameObject.SetActive(true);
            }
            else if (S1TRDAC4 == true)
            {
                GameObject s1trdac4 = RAUD.transform.Find("Suit1 top right down arm color4").gameObject;
                s1trdac4.gameObject.SetActive(true);
            }
            else if (S1TRDAC5 == true)
            {
                GameObject s1trdac5 = RAUD.transform.Find("Suit1 top right down arm color5").gameObject;
                s1trdac5.gameObject.SetActive(true);
            }
            if (S2TRDACC1 == true)
            {
                GameObject s2trdacc1 = RAUD.transform.Find("Suit2 top right down arm cloth color1").gameObject;
                s2trdacc1.gameObject.SetActive(true);
            }
            else if (S2TRDACC2 == true)
            {
                GameObject s2trdacc2 = RAUD.transform.Find("Suit2 top right down arm cloth color2").gameObject;
                s2trdacc2.gameObject.SetActive(true);
            }
            else if (S2TRDACC3 == true)
            {
                GameObject s2trdacc3 = RAUD.transform.Find("Suit2 top right down arm cloth color3").gameObject;
                s2trdacc3.gameObject.SetActive(true);
            }
            else if (S2TRDACC4 == true)
            {
                GameObject s2trdacc4 = RAUD.transform.Find("Suit2 top right down arm cloth color4").gameObject;
                s2trdacc4.gameObject.SetActive(true);
            }
            else if (S2TRDACC5 == true)
            {
                GameObject s2trdacc5 = RAUD.transform.Find("Suit2 top right down arm cloth color5").gameObject;
                s2trdacc5.gameObject.SetActive(true);
            }
            if (S2TRDAC1 == true)
            {
                GameObject s2trdac1 = RAUD.transform.Find("Suit2 top right down arm color1").gameObject;
                s2trdac1.gameObject.SetActive(true);
            }
            else if (S2TRDAC2 == true)
            {
                GameObject s2trdac2 = RAUD.transform.Find("Suit2 top right down arm color2").gameObject;
                s2trdac2.gameObject.SetActive(true);
            }
            else if (S2TRDAC3 == true)
            {
                GameObject s2trdac3 = RAUD.transform.Find("Suit2 top right down arm color3").gameObject;
                s2trdac3.gameObject.SetActive(true);
            }
            else if (S2TRDAC4 == true)
            {
                GameObject s2trdac4 = RAUD.transform.Find("Suit2 top right down arm color4").gameObject;
                s2trdac4.gameObject.SetActive(true);
            }
            else if (S2TRDAC5 == true)
            {
                GameObject s2trdac5 = RAUD.transform.Find("Suit2 top right down arm color5").gameObject;
                s2trdac5.gameObject.SetActive(true);
            }
            if (S2TRDAL1 == true)
            {
                GameObject s2trdaL1 = RAUD.transform.Find("Suit2 top right down arm LED1").gameObject;
                s2trdaL1.gameObject.SetActive(true);
            }
            else if (S2TRDAL2 == true)
            {
                GameObject s2trdaL2 = RAUD.transform.Find("Suit2 top right down arm LED2").gameObject;
                s2trdaL2.gameObject.SetActive(true);
            }
            else if (S2TRDAL3 == true)
            {
                GameObject s2trdaL3 = RAUD.transform.Find("Suit2 top right down arm LED3").gameObject;
                s2trdaL3.gameObject.SetActive(true);
            }
            else if (S2TRDAL4 == true)
            {
                GameObject s2trdaL4 = RAUD.transform.Find("Suit2 top right down arm LED4").gameObject;
                s2trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s2trdaL5 = RAUD.transform.Find("Suit2 top right down arm LED5").gameObject;
                s2trdaL5.gameObject.SetActive(true);
            }
            if (S3TRDACC1 == true)
            {
                GameObject s3trdacc1 = RAUD.transform.Find("Suit3 top right down arm cloth color1").gameObject;
                s3trdacc1.gameObject.SetActive(true);
            }
            else if (S3TRDACC2 == true)
            {
                GameObject s3trdacc2 = RAUD.transform.Find("Suit3 top right down arm cloth color2").gameObject;
                s3trdacc2.gameObject.SetActive(true);
            }
            else if (S3TRDACC3 == true)
            {
                GameObject s3trdacc3 = RAUD.transform.Find("Suit3 top right down arm cloth color3").gameObject;
                s3trdacc3.gameObject.SetActive(true);
            }
            else if (S3TRDACC4 == true)
            {
                GameObject s3trdacc4 = RAUD.transform.Find("Suit3 top right down arm cloth color4").gameObject;
                s3trdacc4.gameObject.SetActive(true);
            }
            else if (S3TRDACC5 == true)
            {
                GameObject s3trdacc5 = RAUD.transform.Find("Suit3 top right down arm cloth color5").gameObject;
                s3trdacc5.gameObject.SetActive(true);
            }
            if (S3TRDAC1 == true)
            {
                GameObject s3trdac1 = RAUD.transform.Find("Suit3 top right down arm color1").gameObject;
                s3trdac1.gameObject.SetActive(true);
            }
            else if (S3TRDAC2 == true)
            {
                GameObject s3trdac2 = RAUD.transform.Find("Suit3 top right down arm color2").gameObject;
                s3trdac2.gameObject.SetActive(true);
            }
            else if (S3TRDAC3 == true)
            {
                GameObject s3trdac3 = RAUD.transform.Find("Suit3 top right down arm color3").gameObject;
                s3trdac3.gameObject.SetActive(true);
            }
            else if (S3TRDAC4 == true)
            {
                GameObject s3trdac4 = RAUD.transform.Find("Suit3 top right down arm color4").gameObject;
                s3trdac4.gameObject.SetActive(true);
            }
            else if (S3TRDAC5 == true)
            {
                GameObject s3trdac5 = RAUD.transform.Find("Suit3 top right down arm color5").gameObject;
                s3trdac5.gameObject.SetActive(true);
            }
            if (S3TRDAL1 == true)
            {
                GameObject s3trdaL1 = RAUD.transform.Find("Suit3 top right down arm LED1").gameObject;
                s3trdaL1.gameObject.SetActive(true);
            }
            else if (S3TRDAL2 == true)
            {
                GameObject s3trdaL2 = RAUD.transform.Find("Suit3 top right down arm LED2").gameObject;
                s3trdaL2.gameObject.SetActive(true);
            }
            else if (S3TRDAL3 == true)
            {
                GameObject s3trdaL3 = RAUD.transform.Find("Suit3 top right down arm LED3").gameObject;
                s3trdaL3.gameObject.SetActive(true);
            }
            else if (S3TRDAL4 == true)
            {
                GameObject s3trdaL4 = RAUD.transform.Find("Suit3 top right down arm LED4").gameObject;
                s3trdaL4.gameObject.SetActive(true);
            }
            else if (S2TRDAL5 == true)
            {
                GameObject s3trdaL5 = RAUD.transform.Find("Suit3 top right down arm LED5").gameObject;
                s3trdaL5.gameObject.SetActive(true);
            }
        }
    }

    void RightArmUpcut()
    {
        if (RightArmUpOUTonline == true)
        {
            GameObject RAU = Instantiate(RightArmUp, RightArmUppos.transform.position, RightArmUppos.transform.rotation);
            Destroy(RAU, 30);

            RAU.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RAU.GetComponent<TearCreateInfector>().Direction = false;
            else
                RAU.GetComponent<TearCreateInfector>().Direction = true;

            //오른팔 상단
            if (BODY1RUA == true)
            {
                GameObject body1rua = RAU.transform.Find("Body1 right up arm").gameObject;
                body1rua.gameObject.SetActive(true);
            }
            else if (BODY2RUA == true)
            {
                GameObject body2rua = RAU.transform.Find("Body2 right up arm").gameObject;
                body2rua.gameObject.SetActive(true);
            }
            if (CT1RUA == true)
            {
                GameObject ct1rua = RAU.transform.Find("Clothes top1 right up arm").gameObject;
                ct1rua.gameObject.SetActive(true);
            }
            else if (CT2RA == true)
            {
                GameObject ct2ra = RAU.transform.Find("Clothes top2 right arm").gameObject;
                ct2ra.gameObject.SetActive(true);
            }
            if (S1TRUAC1 == true)
            {
                GameObject s1truac1 = RAU.transform.Find("Suit1 top right up arm color1").gameObject;
                s1truac1.gameObject.SetActive(true);
            }
            else if (S1TRUAC2 == true)
            {
                GameObject s1truac2 = RAU.transform.Find("Suit1 top right up arm color2").gameObject;
                s1truac2.gameObject.SetActive(true);
            }
            else if (S1TRUAC3 == true)
            {
                GameObject s1truac3 = RAU.transform.Find("Suit1 top right up arm color3").gameObject;
                s1truac3.gameObject.SetActive(true);
            }
            else if (S1TRUAC4 == true)
            {
                GameObject s1truac4 = RAU.transform.Find("Suit1 top right up arm color4").gameObject;
                s1truac4.gameObject.SetActive(true);
            }
            else if (S1TRUAC5 == true)
            {
                GameObject s1truac5 = RAU.transform.Find("Suit1 top right up arm color5").gameObject;
                s1truac5.gameObject.SetActive(true);
            }
            if (S2TRUAC1 == true)
            {
                GameObject s2truac1 = RAU.transform.Find("Suit2 top right up arm color1").gameObject;
                s2truac1.gameObject.SetActive(true);
            }
            else if (S2TRUAC2 == true)
            {
                GameObject s2truac2 = RAU.transform.Find("Suit2 top right up arm color2").gameObject;
                s2truac2.gameObject.SetActive(true);
            }
            else if (S2TRUAC3 == true)
            {
                GameObject s2truac3 = RAU.transform.Find("Suit2 top right up arm color3").gameObject;
                s2truac3.gameObject.SetActive(true);
            }
            else if (S2TRUAC4 == true)
            {
                GameObject s2truac4 = RAU.transform.Find("Suit2 top right up arm color4").gameObject;
                s2truac4.gameObject.SetActive(true);
            }
            else if (S2TRUAC5 == true)
            {
                GameObject s2truac5 = RAU.transform.Find("Suit2 top right up arm color5").gameObject;
                s2truac5.gameObject.SetActive(true);
            }
            if (S3TRUAC1 == true)
            {
                GameObject s3truac1 = RAU.transform.Find("Suit3 top right up arm color1").gameObject;
                s3truac1.gameObject.SetActive(true);
            }
            else if (S3TRUAC2 == true)
            {
                GameObject s3truac2 = RAU.transform.Find("Suit3 top right up arm color2").gameObject;
                s3truac2.gameObject.SetActive(true);
            }
            else if (S3TRUAC3 == true)
            {
                GameObject s3truac3 = RAU.transform.Find("Suit3 top right up arm color3").gameObject;
                s3truac3.gameObject.SetActive(true);
            }
            else if (S3TRUAC4 == true)
            {
                GameObject s3truac4 = RAU.transform.Find("Suit3 top right up arm color4").gameObject;
                s3truac4.gameObject.SetActive(true);
            }
            else if (S3TRUAC5 == true)
            {
                GameObject s3truac5 = RAU.transform.Find("Suit3 top right up arm color5").gameObject;
                s3truac5.gameObject.SetActive(true);
            }
        }
    }

    void HandLcut()
    {
        if (HANDLOUTonline == true)
        {
            GameObject HL = Instantiate(HandL, HandLpos.transform.position, HandLpos.transform.rotation);
            Destroy(HL, 30);

            HL.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                HL.GetComponent<TearCreateInfector>().Direction = false;
            else
                HL.GetComponent<TearCreateInfector>().Direction = true;

            if (HAND1L == true)
            {
                GameObject hand1l = HL.transform.Find("Hand3 left").gameObject;
                hand1l.gameObject.SetActive(true);
            }
            else if (HAND2L == true)
            {
                GameObject hand2l = HL.transform.Find("Hand3 left").gameObject;
                hand2l.gameObject.SetActive(true);
            }
            else if (HAND3L == true)
            {
                GameObject hand3l = HL.transform.Find("Hand3 left").gameObject;
                hand3l.gameObject.SetActive(true);
            }
            else if (G1HAND1LC1 == true)
            {
                GameObject g1hand1lc1 = HL.transform.Find("Grove1 left hand1 color1").gameObject;
                g1hand1lc1.gameObject.SetActive(true);
            }
            else if (G1HAND2LC1 == true)
            {
                GameObject g1hand2lc1 = HL.transform.Find("Grove1 left hand2 color1").gameObject;
                g1hand2lc1.gameObject.SetActive(true);
            }
            else if (G1HAND3LC1 == true)
            {
                GameObject g1hand3lc1 = HL.transform.Find("Grove1 left hand3 color1").gameObject;
                g1hand3lc1.gameObject.SetActive(true);
            }
            else if (G1HAND1LC2 == true)
            {
                GameObject g1hand1lc2 = HL.transform.Find("Grove1 left hand1 color2").gameObject;
                g1hand1lc2.gameObject.SetActive(true);
            }
            else if (G1HAND2LC2 == true)
            {
                GameObject g1hand2lc2 = HL.transform.Find("Grove1 left hand2 color2").gameObject;
                g1hand2lc2.gameObject.SetActive(true);
            }
            else if (G1HAND3LC2 == true)
            {
                GameObject g1hand3lc2 = HL.transform.Find("Grove1 left hand3 color2").gameObject;
                g1hand3lc2.gameObject.SetActive(true);
            }
            else if (G1HAND1LC3 == true)
            {
                GameObject g1hand1lc3 = HL.transform.Find("Grove1 left hand1 color3").gameObject;
                g1hand1lc3.gameObject.SetActive(true);
            }
            else if (G1HAND2LC3 == true)
            {
                GameObject g1hand2lc3 = HL.transform.Find("Grove1 left hand2 color3").gameObject;
                g1hand2lc3.gameObject.SetActive(true);
            }
            else if (G1HAND3LC3 == true)
            {
                GameObject g1hand3lc3 = HL.transform.Find("Grove1 left hand3 color3").gameObject;
                g1hand3lc3.gameObject.SetActive(true);
            }
            else if (G1HAND1LC4 == true)
            {
                GameObject g1hand1lc4 = HL.transform.Find("Grove1 left hand1 color4").gameObject;
                g1hand1lc4.gameObject.SetActive(true);
            }
            else if (G1HAND2LC4 == true)
            {
                GameObject g1hand2lc4 = HL.transform.Find("Grove1 left hand2 color4").gameObject;
                g1hand2lc4.gameObject.SetActive(true);
            }
            else if (G1HAND3LC4 == true)
            {
                GameObject g1hand3lc4 = HL.transform.Find("Grove1 left hand3 color4").gameObject;
                g1hand3lc4.gameObject.SetActive(true);
            }
            else if (G1HAND1LC5 == true)
            {
                GameObject g1hand1lc5 = HL.transform.Find("Grove1 left hand1 color5").gameObject;
                g1hand1lc5.gameObject.SetActive(true);
            }
            else if (G1HAND2LC5 == true)
            {
                GameObject g1hand2lc5 = HL.transform.Find("Grove1 left hand2 color5").gameObject;
                g1hand2lc5.gameObject.SetActive(true);
            }
            else if (G1HAND3LC5 == true)
            {
                GameObject g1hand3lc5 = HL.transform.Find("Grove1 left hand3 color5").gameObject;
                g1hand3lc5.gameObject.SetActive(true);
            }
        }
    }

    void LeftArmDowncut()
    {
        if (LeftArmDownOUTonline == true)
        {
            GameObject B1LDLC = Instantiate(Body1LDL, Body1LDLpos.transform.position, Body1LDLpos.transform.rotation);
            Destroy(B1LDLC, 30);

            B1LDLC.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                B1LDLC.GetComponent<TearCreateInfector>().Direction = false;
            else
                B1LDLC.GetComponent<TearCreateInfector>().Direction = true;

            if (BODY1LDA == true)
            {
                GameObject body1lda = B1LDLC.transform.Find("Body1 left down arm").gameObject;
                body1lda.gameObject.SetActive(true);
            }
            else if (BODY2LDA == true)
            {
                GameObject body2lda = B1LDLC.transform.Find("Body2 left down arm").gameObject;
                body2lda.gameObject.SetActive(true);
            }
            if (CT1LDA == true)
            {
                GameObject ct1lda = B1LDLC.transform.Find("Clothes top1 left down arm").gameObject;
                ct1lda.gameObject.SetActive(true);
            }
            if (S1TLDAC1 == true)
            {
                GameObject s1tldac1 = B1LDLC.transform.Find("Suit1 top left down arm color1").gameObject;
                s1tldac1.gameObject.SetActive(true);
            }
            else if (S1TLDAC2 == true)
            {
                GameObject s1tldac2 = B1LDLC.transform.Find("Suit1 top left down arm color2").gameObject;
                s1tldac2.gameObject.SetActive(true);
            }
            else if (S1TLDAC3 == true)
            {
                GameObject s1tldac3 = B1LDLC.transform.Find("Suit1 top left down arm color3").gameObject;
                s1tldac3.gameObject.SetActive(true);
            }
            else if (S1TLDAC4 == true)
            {
                GameObject s1tldac4 = B1LDLC.transform.Find("Suit1 top left down arm color4").gameObject;
                s1tldac4.gameObject.SetActive(true);
            }
            else if (S1TLDAC5 == true)
            {
                GameObject s1tldac5 = B1LDLC.transform.Find("Suit1 top left down arm color5").gameObject;
                s1tldac5.gameObject.SetActive(true);
            }
            if (S2TLDACC1 == true)
            {
                GameObject s2tldacc1 = B1LDLC.transform.Find("Suit2 top left down arm cloth color1").gameObject;
                s2tldacc1.gameObject.SetActive(true);
            }
            else if (S2TLDACC2 == true)
            {
                GameObject s2tldacc2 = B1LDLC.transform.Find("Suit2 top left down arm cloth color2").gameObject;
                s2tldacc2.gameObject.SetActive(true);
            }
            else if (S2TLDACC3 == true)
            {
                GameObject s2tldacc3 = B1LDLC.transform.Find("Suit2 top left down arm cloth color3").gameObject;
                s2tldacc3.gameObject.SetActive(true);
            }
            else if (S2TLDACC4 == true)
            {
                GameObject s2tldacc4 = B1LDLC.transform.Find("Suit2 top left down arm cloth color4").gameObject;
                s2tldacc4.gameObject.SetActive(true);
            }
            else if (S2TLDACC5 == true)
            {
                GameObject s2tldacc5 = B1LDLC.transform.Find("Suit2 top left down arm cloth color5").gameObject;
                s2tldacc5.gameObject.SetActive(true);
            }
            if (S2TLDAC1 == true)
            {
                GameObject s2tldac1 = B1LDLC.transform.Find("Suit2 top left down arm color1").gameObject;
                s2tldac1.gameObject.SetActive(true);
            }
            else if (S2TLDAC2 == true)
            {
                GameObject s2tldac2 = B1LDLC.transform.Find("Suit2 top left down arm color2").gameObject;
                s2tldac2.gameObject.SetActive(true);
            }
            else if (S2TLDAC3 == true)
            {
                GameObject s2tldac3 = B1LDLC.transform.Find("Suit2 top left down arm color3").gameObject;
                s2tldac3.gameObject.SetActive(true);
            }
            else if (S2TLDAC4 == true)
            {
                GameObject s2tldac4 = B1LDLC.transform.Find("Suit2 top left down arm color4").gameObject;
                s2tldac4.gameObject.SetActive(true);
            }
            else if (S2TLDAC5 == true)
            {
                GameObject s2tldac5 = B1LDLC.transform.Find("Suit2 top left down arm color5").gameObject;
                s2tldac5.gameObject.SetActive(true);
            }
            if (S2TLDAL1 == true)
            {
                GameObject s2tldaL1 = B1LDLC.transform.Find("Suit2 top left down arm LED1").gameObject;
                s2tldaL1.gameObject.SetActive(true);
            }
            else if (S2TLDAL2 == true)
            {
                GameObject s2tldaL2 = B1LDLC.transform.Find("Suit2 top left down arm LED2").gameObject;
                s2tldaL2.gameObject.SetActive(true);
            }
            else if (S2TLDAL3 == true)
            {
                GameObject s2tldaL3 = B1LDLC.transform.Find("Suit2 top left down arm LED3").gameObject;
                s2tldaL3.gameObject.SetActive(true);
            }
            else if (S2TLDAL4 == true)
            {
                GameObject s2tldaL4 = B1LDLC.transform.Find("Suit2 top left down arm LED4").gameObject;
                s2tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s2tldaL5 = B1LDLC.transform.Find("Suit2 top left down arm LED5").gameObject;
                s2tldaL5.gameObject.SetActive(true);
            }
            if (S3TLDACC1 == true)
            {
                GameObject s3tldacc1 = B1LDLC.transform.Find("Suit3 top left down arm cloth color1").gameObject;
                s3tldacc1.gameObject.SetActive(true);
            }
            else if (S3TLDACC2 == true)
            {
                GameObject s3tldacc2 = B1LDLC.transform.Find("Suit3 top left down arm cloth color2").gameObject;
                s3tldacc2.gameObject.SetActive(true);
            }
            else if (S3TLDACC3 == true)
            {
                GameObject s3tldacc3 = B1LDLC.transform.Find("Suit3 top left down arm cloth color3").gameObject;
                s3tldacc3.gameObject.SetActive(true);
            }
            else if (S3TLDACC4 == true)
            {
                GameObject s3tldacc4 = B1LDLC.transform.Find("Suit3 top left down arm cloth color4").gameObject;
                s3tldacc4.gameObject.SetActive(true);
            }
            else if (S3TLDACC5 == true)
            {
                GameObject s3tldacc5 = B1LDLC.transform.Find("Suit3 top left down arm cloth color5").gameObject;
                s3tldacc5.gameObject.SetActive(true);
            }
            if (S3TLDAC1 == true)
            {
                GameObject s3tldac1 = B1LDLC.transform.Find("Suit3 top left down arm color1").gameObject;
                s3tldac1.gameObject.SetActive(true);
            }
            else if (S3TLDAC2 == true)
            {
                GameObject s3tldac2 = B1LDLC.transform.Find("Suit3 top left down arm color2").gameObject;
                s3tldac2.gameObject.SetActive(true);
            }
            else if (S3TLDAC3 == true)
            {
                GameObject s3tldac3 = B1LDLC.transform.Find("Suit3 top left down arm color3").gameObject;
                s3tldac3.gameObject.SetActive(true);
            }
            else if (S3TLDAC4 == true)
            {
                GameObject s3tldac4 = B1LDLC.transform.Find("Suit3 top left down arm color4").gameObject;
                s3tldac4.gameObject.SetActive(true);
            }
            else if (S3TLDAC5 == true)
            {
                GameObject s3tldac5 = B1LDLC.transform.Find("Suit3 top left down arm color5").gameObject;
                s3tldac5.gameObject.SetActive(true);
            }
            if (S3TLDAL1 == true)
            {
                GameObject s3tldaL1 = B1LDLC.transform.Find("Suit3 top left down arm LED1").gameObject;
                s3tldaL1.gameObject.SetActive(true);
            }
            else if (S3TLDAL2 == true)
            {
                GameObject s3tldaL2 = B1LDLC.transform.Find("Suit3 top left down arm LED2").gameObject;
                s3tldaL2.gameObject.SetActive(true);
            }
            else if (S3TLDAL3 == true)
            {
                GameObject s3tldaL3 = B1LDLC.transform.Find("Suit3 top left down arm LED3").gameObject;
                s3tldaL3.gameObject.SetActive(true);
            }
            else if (S3TLDAL4 == true)
            {
                GameObject s3tldaL4 = B1LDLC.transform.Find("Suit3 top left down arm LED4").gameObject;
                s3tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s3tldaL5 = B1LDLC.transform.Find("Suit3 top left down arm LED5").gameObject;
                s3tldaL5.gameObject.SetActive(true);
            }
        }
    }

    void LeftArmDownHandcut()
    {
        if (LeftArmDownHOUTonline == true)
        {
            GameObject B1LDLH = Instantiate(Body1LDLh, Body1LDLhpos.transform.position, Body1LDLhpos.transform.rotation);
            Destroy(B1LDLH, 30);

            B1LDLH.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                B1LDLH.GetComponent<TearCreateInfector>().Direction = false;
            else
                B1LDLH.GetComponent<TearCreateInfector>().Direction = true;

            //왼팔 하단
            if (BODY1LDA == true)
            {
                GameObject body1lda = B1LDLH.transform.Find("Body1 left down arm").gameObject;
                body1lda.gameObject.SetActive(true);
            }
            else if (BODY2LDA == true)
            {
                GameObject body2lda = B1LDLH.transform.Find("Body2 left down arm").gameObject;
                body2lda.gameObject.SetActive(true);
            }
            if (CT1LDA == true)
            {
                GameObject ct1lda = B1LDLH.transform.Find("Clothes top1 left down arm").gameObject;
                ct1lda.gameObject.SetActive(true);
            }
            if (S1TLDAC1 == true)
            {
                GameObject s1tldac1 = B1LDLH.transform.Find("Suit1 top left down arm color1").gameObject;
                s1tldac1.gameObject.SetActive(true);
            }
            else if (S1TLDAC2 == true)
            {
                GameObject s1tldac2 = B1LDLH.transform.Find("Suit1 top left down arm color2").gameObject;
                s1tldac2.gameObject.SetActive(true);
            }
            else if (S1TLDAC3 == true)
            {
                GameObject s1tldac3 = B1LDLH.transform.Find("Suit1 top left down arm color3").gameObject;
                s1tldac3.gameObject.SetActive(true);
            }
            else if (S1TLDAC4 == true)
            {
                GameObject s1tldac4 = B1LDLH.transform.Find("Suit1 top left down arm color4").gameObject;
                s1tldac4.gameObject.SetActive(true);
            }
            else if (S1TLDAC5 == true)
            {
                GameObject s1tldac5 = B1LDLH.transform.Find("Suit1 top left down arm color5").gameObject;
                s1tldac5.gameObject.SetActive(true);
            }
            if (S2TLDACC1 == true)
            {
                GameObject s2tldacc1 = B1LDLH.transform.Find("Suit2 top left down arm cloth color1").gameObject;
                s2tldacc1.gameObject.SetActive(true);
            }
            else if (S2TLDACC2 == true)
            {
                GameObject s2tldacc2 = B1LDLH.transform.Find("Suit2 top left down arm cloth color2").gameObject;
                s2tldacc2.gameObject.SetActive(true);
            }
            else if (S2TLDACC3 == true)
            {
                GameObject s2tldacc3 = B1LDLH.transform.Find("Suit2 top left down arm cloth color3").gameObject;
                s2tldacc3.gameObject.SetActive(true);
            }
            else if (S2TLDACC4 == true)
            {
                GameObject s2tldacc4 = B1LDLH.transform.Find("Suit2 top left down arm cloth color4").gameObject;
                s2tldacc4.gameObject.SetActive(true);
            }
            else if (S2TLDACC5 == true)
            {
                GameObject s2tldacc5 = B1LDLH.transform.Find("Suit2 top left down arm cloth color5").gameObject;
                s2tldacc5.gameObject.SetActive(true);
            }
            if (S2TLDAC1 == true)
            {
                GameObject s2tldac1 = B1LDLH.transform.Find("Suit2 top left down arm color1").gameObject;
                s2tldac1.gameObject.SetActive(true);
            }
            else if (S2TLDAC2 == true)
            {
                GameObject s2tldac2 = B1LDLH.transform.Find("Suit2 top left down arm color2").gameObject;
                s2tldac2.gameObject.SetActive(true);
            }
            else if (S2TLDAC3 == true)
            {
                GameObject s2tldac3 = B1LDLH.transform.Find("Suit2 top left down arm color3").gameObject;
                s2tldac3.gameObject.SetActive(true);
            }
            else if (S2TLDAC4 == true)
            {
                GameObject s2tldac4 = B1LDLH.transform.Find("Suit2 top left down arm color4").gameObject;
                s2tldac4.gameObject.SetActive(true);
            }
            else if (S2TLDAC5 == true)
            {
                GameObject s2tldac5 = B1LDLH.transform.Find("Suit2 top left down arm color5").gameObject;
                s2tldac5.gameObject.SetActive(true);
            }
            if (S2TLDAL1 == true)
            {
                GameObject s2tldaL1 = B1LDLH.transform.Find("Suit2 top left down arm LED1").gameObject;
                s2tldaL1.gameObject.SetActive(true);
            }
            else if (S2TLDAL2 == true)
            {
                GameObject s2tldaL2 = B1LDLH.transform.Find("Suit2 top left down arm LED2").gameObject;
                s2tldaL2.gameObject.SetActive(true);
            }
            else if (S2TLDAL3 == true)
            {
                GameObject s2tldaL3 = B1LDLH.transform.Find("Suit2 top left down arm LED3").gameObject;
                s2tldaL3.gameObject.SetActive(true);
            }
            else if (S2TLDAL4 == true)
            {
                GameObject s2tldaL4 = B1LDLH.transform.Find("Suit2 top left down arm LED4").gameObject;
                s2tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s2tldaL5 = B1LDLH.transform.Find("Suit2 top left down arm LED5").gameObject;
                s2tldaL5.gameObject.SetActive(true);
            }
            if (S3TLDACC1 == true)
            {
                GameObject s3tldacc1 = B1LDLH.transform.Find("Suit3 top left down arm cloth color1").gameObject;
                s3tldacc1.gameObject.SetActive(true);
            }
            else if (S3TLDACC2 == true)
            {
                GameObject s3tldacc2 = B1LDLH.transform.Find("Suit3 top left down arm cloth color2").gameObject;
                s3tldacc2.gameObject.SetActive(true);
            }
            else if (S3TLDACC3 == true)
            {
                GameObject s3tldacc3 = B1LDLH.transform.Find("Suit3 top left down arm cloth color3").gameObject;
                s3tldacc3.gameObject.SetActive(true);
            }
            else if (S3TLDACC4 == true)
            {
                GameObject s3tldacc4 = B1LDLH.transform.Find("Suit3 top left down arm cloth color4").gameObject;
                s3tldacc4.gameObject.SetActive(true);
            }
            else if (S3TLDACC5 == true)
            {
                GameObject s3tldacc5 = B1LDLH.transform.Find("Suit3 top left down arm cloth color5").gameObject;
                s3tldacc5.gameObject.SetActive(true);
            }
            if (S3TLDAC1 == true)
            {
                GameObject s3tldac1 = B1LDLH.transform.Find("Suit3 top left down arm color1").gameObject;
                s3tldac1.gameObject.SetActive(true);
            }
            else if (S3TLDAC2 == true)
            {
                GameObject s3tldac2 = B1LDLH.transform.Find("Suit3 top left down arm color2").gameObject;
                s3tldac2.gameObject.SetActive(true);
            }
            else if (S3TLDAC3 == true)
            {
                GameObject s3tldac3 = B1LDLH.transform.Find("Suit3 top left down arm color3").gameObject;
                s3tldac3.gameObject.SetActive(true);
            }
            else if (S3TLDAC4 == true)
            {
                GameObject s3tldac4 = B1LDLH.transform.Find("Suit3 top left down arm color4").gameObject;
                s3tldac4.gameObject.SetActive(true);
            }
            else if (S3TLDAC5 == true)
            {
                GameObject s3tldac5 = B1LDLH.transform.Find("Suit3 top left down arm color5").gameObject;
                s3tldac5.gameObject.SetActive(true);
            }
            if (S3TLDAL1 == true)
            {
                GameObject s3tldaL1 = B1LDLH.transform.Find("Suit3 top left down arm LED1").gameObject;
                s3tldaL1.gameObject.SetActive(true);
            }
            else if (S3TLDAL2 == true)
            {
                GameObject s3tldaL2 = B1LDLH.transform.Find("Suit3 top left down arm LED2").gameObject;
                s3tldaL2.gameObject.SetActive(true);
            }
            else if (S3TLDAL3 == true)
            {
                GameObject s3tldaL3 = B1LDLH.transform.Find("Suit3 top left down arm LED3").gameObject;
                s3tldaL3.gameObject.SetActive(true);
            }
            else if (S3TLDAL4 == true)
            {
                GameObject s3tldaL4 = B1LDLH.transform.Find("Suit3 top left down arm LED4").gameObject;
                s3tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s3tldaL5 = B1LDLH.transform.Find("Suit3 top left down arm LED5").gameObject;
                s3tldaL5.gameObject.SetActive(true);
            }

            //손
            if (HAND1L == true)
            {
                GameObject hand1l = B1LDLH.transform.Find("Hand3 left").gameObject;
                hand1l.gameObject.SetActive(true);
            }
            else if (HAND2L == true)
            {
                GameObject hand2l = B1LDLH.transform.Find("Hand3 left").gameObject;
                hand2l.gameObject.SetActive(true);
            }
            else if (HAND3L == true)
            {
                GameObject hand3l = B1LDLH.transform.Find("Hand3 left").gameObject;
                hand3l.gameObject.SetActive(true);
            }
            else if (G1HAND1LC1 == true)
            {
                GameObject g1hand1lc1 = B1LDLH.transform.Find("Grove1 left hand1 color1").gameObject;
                g1hand1lc1.gameObject.SetActive(true);
            }
            else if (G1HAND2LC1 == true)
            {
                GameObject g1hand2lc1 = B1LDLH.transform.Find("Grove1 left hand2 color1").gameObject;
                g1hand2lc1.gameObject.SetActive(true);
            }
            else if (G1HAND3LC1 == true)
            {
                GameObject g1hand3lc1 = B1LDLH.transform.Find("Grove1 left hand3 color1").gameObject;
                g1hand3lc1.gameObject.SetActive(true);
            }
            else if (G1HAND1LC2 == true)
            {
                GameObject g1hand1lc2 = B1LDLH.transform.Find("Grove1 left hand1 color2").gameObject;
                g1hand1lc2.gameObject.SetActive(true);
            }
            else if (G1HAND2LC2 == true)
            {
                GameObject g1hand2lc2 = B1LDLH.transform.Find("Grove1 left hand2 color2").gameObject;
                g1hand2lc2.gameObject.SetActive(true);
            }
            else if (G1HAND3LC2 == true)
            {
                GameObject g1hand3lc2 = B1LDLH.transform.Find("Grove1 left hand3 color2").gameObject;
                g1hand3lc2.gameObject.SetActive(true);
            }
            else if (G1HAND1LC3 == true)
            {
                GameObject g1hand1lc3 = B1LDLH.transform.Find("Grove1 left hand1 color3").gameObject;
                g1hand1lc3.gameObject.SetActive(true);
            }
            else if (G1HAND2LC3 == true)
            {
                GameObject g1hand2lc3 = B1LDLH.transform.Find("Grove1 left hand2 color3").gameObject;
                g1hand2lc3.gameObject.SetActive(true);
            }
            else if (G1HAND3LC3 == true)
            {
                GameObject g1hand3lc3 = B1LDLH.transform.Find("Grove1 left hand3 color3").gameObject;
                g1hand3lc3.gameObject.SetActive(true);
            }
            else if (G1HAND1LC4 == true)
            {
                GameObject g1hand1lc4 = B1LDLH.transform.Find("Grove1 left hand1 color4").gameObject;
                g1hand1lc4.gameObject.SetActive(true);
            }
            else if (G1HAND2LC4 == true)
            {
                GameObject g1hand2lc4 = B1LDLH.transform.Find("Grove1 left hand2 color4").gameObject;
                g1hand2lc4.gameObject.SetActive(true);
            }
            else if (G1HAND3LC4 == true)
            {
                GameObject g1hand3lc4 = B1LDLH.transform.Find("Grove1 left hand3 color4").gameObject;
                g1hand3lc4.gameObject.SetActive(true);
            }
            else if (G1HAND1LC5 == true)
            {
                GameObject g1hand1lc5 = B1LDLH.transform.Find("Grove1 left hand1 color5").gameObject;
                g1hand1lc5.gameObject.SetActive(true);
            }
            else if (G1HAND2LC5 == true)
            {
                GameObject g1hand2lc5 = B1LDLH.transform.Find("Grove1 left hand2 color5").gameObject;
                g1hand2lc5.gameObject.SetActive(true);
            }
            else if (G1HAND3LC5 == true)
            {
                GameObject g1hand3lc5 = B1LDLH.transform.Find("Grove1 left hand3 color5").gameObject;
                g1hand3lc5.gameObject.SetActive(true);
            }
        }
    }

    void LeftArmUpDownHandcut()
    {
        if (LeftArmUpDownHOUTonline == true)
        {
            GameObject LUDH = Instantiate(LeftUpDownHand, LeftUpDownHandpos.transform.position, LeftUpDownHandpos.transform.rotation);
            Destroy(LUDH, 30);

            LUDH.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                LUDH.GetComponent<TearCreateInfector>().Direction = false;
            else
                LUDH.GetComponent<TearCreateInfector>().Direction = true;

            //왼팔 상단
            if (BODY1LUA == true)
            {
                GameObject body1lua = LUDH.transform.Find("Body1 left up arm").gameObject;
                body1lua.gameObject.SetActive(true);
            }
            else if (BODY2LUA == true)
            {
                GameObject body2lua = LUDH.transform.Find("Body2 left up arm").gameObject;
                body2lua.gameObject.SetActive(true);
            }
            if (CT1LUA == true)
            {
                GameObject ct1lua = LUDH.transform.Find("Clothes top1 left up arm").gameObject;
                ct1lua.gameObject.SetActive(true);
            }
            else if (CT2LA == true)
            {
                GameObject ct2la = LUDH.transform.Find("Clothes top2 left arm").gameObject;
                ct2la.gameObject.SetActive(true);
            }
            if (S1TLUAC1 == true)
            {
                GameObject s1tluac1 = LUDH.transform.Find("Suit1 top left up arm color1").gameObject;
                s1tluac1.gameObject.SetActive(true);
            }
            else if (S1TLUAC2 == true)
            {
                GameObject s1tluac2 = LUDH.transform.Find("Suit1 top left up arm color2").gameObject;
                s1tluac2.gameObject.SetActive(true);
            }
            else if (S1TLUAC3 == true)
            {
                GameObject s1tluac3 = LUDH.transform.Find("Suit1 top left up arm color3").gameObject;
                s1tluac3.gameObject.SetActive(true);
            }
            else if (S1TLUAC4 == true)
            {
                GameObject s1tluac4 = LUDH.transform.Find("Suit1 top left up arm color4").gameObject;
                s1tluac4.gameObject.SetActive(true);
            }
            else if (S1TLUAC5 == true)
            {
                GameObject s1tluac5 = LUDH.transform.Find("Suit1 top left up arm color5").gameObject;
                s1tluac5.gameObject.SetActive(true);
            }
            if (S2TLUAC1 == true)
            {
                GameObject s2tluac1 = LUDH.transform.Find("Suit2 top left up arm color1").gameObject;
                s2tluac1.gameObject.SetActive(true);
            }
            else if (S2TLUAC2 == true)
            {
                GameObject s2tluac2 = LUDH.transform.Find("Suit2 top left up arm color2").gameObject;
                s2tluac2.gameObject.SetActive(true);
            }
            else if (S2TLUAC3 == true)
            {
                GameObject s2tluac3 = LUDH.transform.Find("Suit2 top left up arm color3").gameObject;
                s2tluac3.gameObject.SetActive(true);
            }
            else if (S2TLUAC4 == true)
            {
                GameObject s2tluac4 = LUDH.transform.Find("Suit2 top left up arm color4").gameObject;
                s2tluac4.gameObject.SetActive(true);
            }
            else if (S2TLUAC5 == true)
            {
                GameObject s2tluac5 = LUDH.transform.Find("Suit2 top left up arm color5").gameObject;
                s2tluac5.gameObject.SetActive(true);
            }
            if (S3TLUAC1 == true)
            {
                GameObject s3tluac1 = LUDH.transform.Find("Suit3 top left up arm color1").gameObject;
                s3tluac1.gameObject.SetActive(true);
            }
            else if (S3TLUAC2 == true)
            {
                GameObject s3tluac2 = LUDH.transform.Find("Suit3 top left up arm color2").gameObject;
                s3tluac2.gameObject.SetActive(true);
            }
            else if (S3TLUAC3 == true)
            {
                GameObject s3tluac3 = LUDH.transform.Find("Suit3 top left up arm color3").gameObject;
                s3tluac3.gameObject.SetActive(true);
            }
            else if (S3TLUAC4 == true)
            {
                GameObject s3tluac4 = LUDH.transform.Find("Suit3 top left up arm color4").gameObject;
                s3tluac4.gameObject.SetActive(true);
            }
            else if (S3TLUAC5 == true)
            {
                GameObject s3tluac5 = LUDH.transform.Find("Suit3 top left up arm color5").gameObject;
                s3tluac5.gameObject.SetActive(true);
            }

            //왼팔 하단
            if (BODY1LDA == true)
            {
                GameObject body1lda = LUDH.transform.Find("Body1 left down arm").gameObject;
                body1lda.gameObject.SetActive(true);
            }
            else if (BODY2LDA == true)
            {
                GameObject body2lda = LUDH.transform.Find("Body2 left down arm").gameObject;
                body2lda.gameObject.SetActive(true);
            }
            if (CT1LDA == true)
            {
                GameObject ct1lda = LUDH.transform.Find("Clothes top1 left down arm").gameObject;
                ct1lda.gameObject.SetActive(true);
            }
            if (S1TLDAC1 == true)
            {
                GameObject s1tldac1 = LUDH.transform.Find("Suit1 top left down arm color1").gameObject;
                s1tldac1.gameObject.SetActive(true);
            }
            else if (S1TLDAC2 == true)
            {
                GameObject s1tldac2 = LUDH.transform.Find("Suit1 top left down arm color2").gameObject;
                s1tldac2.gameObject.SetActive(true);
            }
            else if (S1TLDAC3 == true)
            {
                GameObject s1tldac3 = LUDH.transform.Find("Suit1 top left down arm color3").gameObject;
                s1tldac3.gameObject.SetActive(true);
            }
            else if (S1TLDAC4 == true)
            {
                GameObject s1tldac4 = LUDH.transform.Find("Suit1 top left down arm color4").gameObject;
                s1tldac4.gameObject.SetActive(true);
            }
            else if (S1TLDAC5 == true)
            {
                GameObject s1tldac5 = LUDH.transform.Find("Suit1 top left down arm color5").gameObject;
                s1tldac5.gameObject.SetActive(true);
            }
            if (S2TLDACC1 == true)
            {
                GameObject s2tldacc1 = LUDH.transform.Find("Suit2 top left down arm cloth color1").gameObject;
                s2tldacc1.gameObject.SetActive(true);
            }
            else if (S2TLDACC2 == true)
            {
                GameObject s2tldacc2 = LUDH.transform.Find("Suit2 top left down arm cloth color2").gameObject;
                s2tldacc2.gameObject.SetActive(true);
            }
            else if (S2TLDACC3 == true)
            {
                GameObject s2tldacc3 = LUDH.transform.Find("Suit2 top left down arm cloth color3").gameObject;
                s2tldacc3.gameObject.SetActive(true);
            }
            else if (S2TLDACC4 == true)
            {
                GameObject s2tldacc4 = LUDH.transform.Find("Suit2 top left down arm cloth color4").gameObject;
                s2tldacc4.gameObject.SetActive(true);
            }
            else if (S2TLDACC5 == true)
            {
                GameObject s2tldacc5 = LUDH.transform.Find("Suit2 top left down arm cloth color5").gameObject;
                s2tldacc5.gameObject.SetActive(true);
            }
            if (S2TLDAC1 == true)
            {
                GameObject s2tldac1 = LUDH.transform.Find("Suit2 top left down arm color1").gameObject;
                s2tldac1.gameObject.SetActive(true);
            }
            else if (S2TLDAC2 == true)
            {
                GameObject s2tldac2 = LUDH.transform.Find("Suit2 top left down arm color2").gameObject;
                s2tldac2.gameObject.SetActive(true);
            }
            else if (S2TLDAC3 == true)
            {
                GameObject s2tldac3 = LUDH.transform.Find("Suit2 top left down arm color3").gameObject;
                s2tldac3.gameObject.SetActive(true);
            }
            else if (S2TLDAC4 == true)
            {
                GameObject s2tldac4 = LUDH.transform.Find("Suit2 top left down arm color4").gameObject;
                s2tldac4.gameObject.SetActive(true);
            }
            else if (S2TLDAC5 == true)
            {
                GameObject s2tldac5 = LUDH.transform.Find("Suit2 top left down arm color5").gameObject;
                s2tldac5.gameObject.SetActive(true);
            }
            if (S2TLDAL1 == true)
            {
                GameObject s2tldaL1 = LUDH.transform.Find("Suit2 top left down arm LED1").gameObject;
                s2tldaL1.gameObject.SetActive(true);
            }
            else if (S2TLDAL2 == true)
            {
                GameObject s2tldaL2 = LUDH.transform.Find("Suit2 top left down arm LED2").gameObject;
                s2tldaL2.gameObject.SetActive(true);
            }
            else if (S2TLDAL3 == true)
            {
                GameObject s2tldaL3 = LUDH.transform.Find("Suit2 top left down arm LED3").gameObject;
                s2tldaL3.gameObject.SetActive(true);
            }
            else if (S2TLDAL4 == true)
            {
                GameObject s2tldaL4 = LUDH.transform.Find("Suit2 top left down arm LED4").gameObject;
                s2tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s2tldaL5 = LUDH.transform.Find("Suit2 top left down arm LED5").gameObject;
                s2tldaL5.gameObject.SetActive(true);
            }
            if (S3TLDACC1 == true)
            {
                GameObject s3tldacc1 = LUDH.transform.Find("Suit3 top left down arm cloth color1").gameObject;
                s3tldacc1.gameObject.SetActive(true);
            }
            else if (S3TLDACC2 == true)
            {
                GameObject s3tldacc2 = LUDH.transform.Find("Suit3 top left down arm cloth color2").gameObject;
                s3tldacc2.gameObject.SetActive(true);
            }
            else if (S3TLDACC3 == true)
            {
                GameObject s3tldacc3 = LUDH.transform.Find("Suit3 top left down arm cloth color3").gameObject;
                s3tldacc3.gameObject.SetActive(true);
            }
            else if (S3TLDACC4 == true)
            {
                GameObject s3tldacc4 = LUDH.transform.Find("Suit3 top left down arm cloth color4").gameObject;
                s3tldacc4.gameObject.SetActive(true);
            }
            else if (S3TLDACC5 == true)
            {
                GameObject s3tldacc5 = LUDH.transform.Find("Suit3 top left down arm cloth color5").gameObject;
                s3tldacc5.gameObject.SetActive(true);
            }
            if (S3TLDAC1 == true)
            {
                GameObject s3tldac1 = LUDH.transform.Find("Suit3 top left down arm color1").gameObject;
                s3tldac1.gameObject.SetActive(true);
            }
            else if (S3TLDAC2 == true)
            {
                GameObject s3tldac2 = LUDH.transform.Find("Suit3 top left down arm color2").gameObject;
                s3tldac2.gameObject.SetActive(true);
            }
            else if (S3TLDAC3 == true)
            {
                GameObject s3tldac3 = LUDH.transform.Find("Suit3 top left down arm color3").gameObject;
                s3tldac3.gameObject.SetActive(true);
            }
            else if (S3TLDAC4 == true)
            {
                GameObject s3tldac4 = LUDH.transform.Find("Suit3 top left down arm color4").gameObject;
                s3tldac4.gameObject.SetActive(true);
            }
            else if (S3TLDAC5 == true)
            {
                GameObject s3tldac5 = LUDH.transform.Find("Suit3 top left down arm color5").gameObject;
                s3tldac5.gameObject.SetActive(true);
            }
            if (S3TLDAL1 == true)
            {
                GameObject s3tldaL1 = LUDH.transform.Find("Suit3 top left down arm LED1").gameObject;
                s3tldaL1.gameObject.SetActive(true);
            }
            else if (S3TLDAL2 == true)
            {
                GameObject s3tldaL2 = LUDH.transform.Find("Suit3 top left down arm LED2").gameObject;
                s3tldaL2.gameObject.SetActive(true);
            }
            else if (S3TLDAL3 == true)
            {
                GameObject s3tldaL3 = LUDH.transform.Find("Suit3 top left down arm LED3").gameObject;
                s3tldaL3.gameObject.SetActive(true);
            }
            else if (S3TLDAL4 == true)
            {
                GameObject s3tldaL4 = LUDH.transform.Find("Suit3 top left down arm LED4").gameObject;
                s3tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s3tldaL5 = LUDH.transform.Find("Suit3 top left down arm LED5").gameObject;
                s3tldaL5.gameObject.SetActive(true);
            }

            //손
            if (HAND1L == true)
            {
                GameObject hand1l = LUDH.transform.Find("Hand3 left").gameObject;
                hand1l.gameObject.SetActive(true);
            }
            else if (HAND2L == true)
            {
                GameObject hand2l = LUDH.transform.Find("Hand3 left").gameObject;
                hand2l.gameObject.SetActive(true);
            }
            else if (HAND3L == true)
            {
                GameObject hand3l = LUDH.transform.Find("Hand3 left").gameObject;
                hand3l.gameObject.SetActive(true);
            }
            else if (G1HAND1LC1 == true)
            {
                GameObject g1hand1lc1 = LUDH.transform.Find("Grove1 left hand1 color1").gameObject;
                g1hand1lc1.gameObject.SetActive(true);
            }
            else if (G1HAND2LC1 == true)
            {
                GameObject g1hand2lc1 = LUDH.transform.Find("Grove1 left hand2 color1").gameObject;
                g1hand2lc1.gameObject.SetActive(true);
            }
            else if (G1HAND3LC1 == true)
            {
                GameObject g1hand3lc1 = LUDH.transform.Find("Grove1 left hand3 color1").gameObject;
                g1hand3lc1.gameObject.SetActive(true);
            }
            else if (G1HAND1LC2 == true)
            {
                GameObject g1hand1lc2 = LUDH.transform.Find("Grove1 left hand1 color2").gameObject;
                g1hand1lc2.gameObject.SetActive(true);
            }
            else if (G1HAND2LC2 == true)
            {
                GameObject g1hand2lc2 = LUDH.transform.Find("Grove1 left hand2 color2").gameObject;
                g1hand2lc2.gameObject.SetActive(true);
            }
            else if (G1HAND3LC2 == true)
            {
                GameObject g1hand3lc2 = LUDH.transform.Find("Grove1 left hand3 color2").gameObject;
                g1hand3lc2.gameObject.SetActive(true);
            }
            else if (G1HAND1LC3 == true)
            {
                GameObject g1hand1lc3 = LUDH.transform.Find("Grove1 left hand1 color3").gameObject;
                g1hand1lc3.gameObject.SetActive(true);
            }
            else if (G1HAND2LC3 == true)
            {
                GameObject g1hand2lc3 = LUDH.transform.Find("Grove1 left hand2 color3").gameObject;
                g1hand2lc3.gameObject.SetActive(true);
            }
            else if (G1HAND3LC3 == true)
            {
                GameObject g1hand3lc3 = LUDH.transform.Find("Grove1 left hand3 color3").gameObject;
                g1hand3lc3.gameObject.SetActive(true);
            }
            else if (G1HAND1LC4 == true)
            {
                GameObject g1hand1lc4 = LUDH.transform.Find("Grove1 left hand1 color4").gameObject;
                g1hand1lc4.gameObject.SetActive(true);
            }
            else if (G1HAND2LC4 == true)
            {
                GameObject g1hand2lc4 = LUDH.transform.Find("Grove1 left hand2 color4").gameObject;
                g1hand2lc4.gameObject.SetActive(true);
            }
            else if (G1HAND3LC4 == true)
            {
                GameObject g1hand3lc4 = LUDH.transform.Find("Grove1 left hand3 color4").gameObject;
                g1hand3lc4.gameObject.SetActive(true);
            }
            else if (G1HAND1LC5 == true)
            {
                GameObject g1hand1lc5 = LUDH.transform.Find("Grove1 left hand1 color5").gameObject;
                g1hand1lc5.gameObject.SetActive(true);
            }
            else if (G1HAND2LC5 == true)
            {
                GameObject g1hand2lc5 = LUDH.transform.Find("Grove1 left hand2 color5").gameObject;
                g1hand2lc5.gameObject.SetActive(true);
            }
            else if (G1HAND3LC5 == true)
            {
                GameObject g1hand3lc5 = LUDH.transform.Find("Grove1 left hand3 color5").gameObject;
                g1hand3lc5.gameObject.SetActive(true);
            }
        }
    }

    void LeftArmUpDowncut()
    {
        if (LeftArmUpDownOUTonline == true)
        {
            GameObject LUD = Instantiate(LeftUpDown, LeftUpDownpos.transform.position, LeftUpDownpos.transform.rotation);
            Destroy(LUD, 30);

            LUD.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                LUD.GetComponent<TearCreateInfector>().Direction = false;
            else
                LUD.GetComponent<TearCreateInfector>().Direction = true;

            //왼팔 상단
            if (BODY1LUA == true)
            {
                GameObject body1lua = LUD.transform.Find("Body1 left up arm").gameObject;
                body1lua.gameObject.SetActive(true);
            }
            else if (BODY2LUA == true)
            {
                GameObject body2lua = LUD.transform.Find("Body2 left up arm").gameObject;
                body2lua.gameObject.SetActive(true);
            }
            if (CT1LUA == true)
            {
                GameObject ct1lua = LUD.transform.Find("Clothes top1 left up arm").gameObject;
                ct1lua.gameObject.SetActive(true);
            }
            else if (CT2LA == true)
            {
                GameObject ct2la = LUD.transform.Find("Clothes top2 left arm").gameObject;
                ct2la.gameObject.SetActive(true);
            }
            if (S1TLUAC1 == true)
            {
                GameObject s1tluac1 = LUD.transform.Find("Suit1 top left up arm color1").gameObject;
                s1tluac1.gameObject.SetActive(true);
            }
            else if (S1TLUAC2 == true)
            {
                GameObject s1tluac2 = LUD.transform.Find("Suit1 top left up arm color2").gameObject;
                s1tluac2.gameObject.SetActive(true);
            }
            else if (S1TLUAC3 == true)
            {
                GameObject s1tluac3 = LUD.transform.Find("Suit1 top left up arm color3").gameObject;
                s1tluac3.gameObject.SetActive(true);
            }
            else if (S1TLUAC4 == true)
            {
                GameObject s1tluac4 = LUD.transform.Find("Suit1 top left up arm color4").gameObject;
                s1tluac4.gameObject.SetActive(true);
            }
            else if (S1TLUAC5 == true)
            {
                GameObject s1tluac5 = LUD.transform.Find("Suit1 top left up arm color5").gameObject;
                s1tluac5.gameObject.SetActive(true);
            }
            if (S2TLUAC1 == true)
            {
                GameObject s2tluac1 = LUD.transform.Find("Suit2 top left up arm color1").gameObject;
                s2tluac1.gameObject.SetActive(true);
            }
            else if (S2TLUAC2 == true)
            {
                GameObject s2tluac2 = LUD.transform.Find("Suit2 top left up arm color2").gameObject;
                s2tluac2.gameObject.SetActive(true);
            }
            else if (S2TLUAC3 == true)
            {
                GameObject s2tluac3 = LUD.transform.Find("Suit2 top left up arm color3").gameObject;
                s2tluac3.gameObject.SetActive(true);
            }
            else if (S2TLUAC4 == true)
            {
                GameObject s2tluac4 = LUD.transform.Find("Suit2 top left up arm color4").gameObject;
                s2tluac4.gameObject.SetActive(true);
            }
            else if (S2TLUAC5 == true)
            {
                GameObject s2tluac5 = LUD.transform.Find("Suit2 top left up arm color5").gameObject;
                s2tluac5.gameObject.SetActive(true);
            }
            if (S3TLUAC1 == true)
            {
                GameObject s3tluac1 = LUD.transform.Find("Suit3 top left up arm color1").gameObject;
                s3tluac1.gameObject.SetActive(true);
            }
            else if (S3TLUAC2 == true)
            {
                GameObject s3tluac2 = LUD.transform.Find("Suit3 top left up arm color2").gameObject;
                s3tluac2.gameObject.SetActive(true);
            }
            else if (S3TLUAC3 == true)
            {
                GameObject s3tluac3 = LUD.transform.Find("Suit3 top left up arm color3").gameObject;
                s3tluac3.gameObject.SetActive(true);
            }
            else if (S3TLUAC4 == true)
            {
                GameObject s3tluac4 = LUD.transform.Find("Suit3 top left up arm color4").gameObject;
                s3tluac4.gameObject.SetActive(true);
            }
            else if (S3TLUAC5 == true)
            {
                GameObject s3tluac5 = LUD.transform.Find("Suit3 top left up arm color5").gameObject;
                s3tluac5.gameObject.SetActive(true);
            }

            //왼팔 하단
            if (BODY1LDA == true)
            {
                GameObject body1lda = LUD.transform.Find("Body1 left down arm").gameObject;
                body1lda.gameObject.SetActive(true);
            }
            else if (BODY2LDA == true)
            {
                GameObject body2lda = LUD.transform.Find("Body2 left down arm").gameObject;
                body2lda.gameObject.SetActive(true);
            }
            if (CT1LDA == true)
            {
                GameObject ct1lda = LUD.transform.Find("Clothes top1 left down arm").gameObject;
                ct1lda.gameObject.SetActive(true);
            }
            if (S1TLDAC1 == true)
            {
                GameObject s1tldac1 = LUD.transform.Find("Suit1 top left down arm color1").gameObject;
                s1tldac1.gameObject.SetActive(true);
            }
            else if (S1TLDAC2 == true)
            {
                GameObject s1tldac2 = LUD.transform.Find("Suit1 top left down arm color2").gameObject;
                s1tldac2.gameObject.SetActive(true);
            }
            else if (S1TLDAC3 == true)
            {
                GameObject s1tldac3 = LUD.transform.Find("Suit1 top left down arm color3").gameObject;
                s1tldac3.gameObject.SetActive(true);
            }
            else if (S1TLDAC4 == true)
            {
                GameObject s1tldac4 = LUD.transform.Find("Suit1 top left down arm color4").gameObject;
                s1tldac4.gameObject.SetActive(true);
            }
            else if (S1TLDAC5 == true)
            {
                GameObject s1tldac5 = LUD.transform.Find("Suit1 top left down arm color5").gameObject;
                s1tldac5.gameObject.SetActive(true);
            }
            if (S2TLDACC1 == true)
            {
                GameObject s2tldacc1 = LUD.transform.Find("Suit2 top left down arm cloth color1").gameObject;
                s2tldacc1.gameObject.SetActive(true);
            }
            else if (S2TLDACC2 == true)
            {
                GameObject s2tldacc2 = LUD.transform.Find("Suit2 top left down arm cloth color2").gameObject;
                s2tldacc2.gameObject.SetActive(true);
            }
            else if (S2TLDACC3 == true)
            {
                GameObject s2tldacc3 = LUD.transform.Find("Suit2 top left down arm cloth color3").gameObject;
                s2tldacc3.gameObject.SetActive(true);
            }
            else if (S2TLDACC4 == true)
            {
                GameObject s2tldacc4 = LUD.transform.Find("Suit2 top left down arm cloth color4").gameObject;
                s2tldacc4.gameObject.SetActive(true);
            }
            else if (S2TLDACC5 == true)
            {
                GameObject s2tldacc5 = LUD.transform.Find("Suit2 top left down arm cloth color5").gameObject;
                s2tldacc5.gameObject.SetActive(true);
            }
            if (S2TLDAC1 == true)
            {
                GameObject s2tldac1 = LUD.transform.Find("Suit2 top left down arm color1").gameObject;
                s2tldac1.gameObject.SetActive(true);
            }
            else if (S2TLDAC2 == true)
            {
                GameObject s2tldac2 = LUD.transform.Find("Suit2 top left down arm color2").gameObject;
                s2tldac2.gameObject.SetActive(true);
            }
            else if (S2TLDAC3 == true)
            {
                GameObject s2tldac3 = LUD.transform.Find("Suit2 top left down arm color3").gameObject;
                s2tldac3.gameObject.SetActive(true);
            }
            else if (S2TLDAC4 == true)
            {
                GameObject s2tldac4 = LUD.transform.Find("Suit2 top left down arm color4").gameObject;
                s2tldac4.gameObject.SetActive(true);
            }
            else if (S2TLDAC5 == true)
            {
                GameObject s2tldac5 = LUD.transform.Find("Suit2 top left down arm color5").gameObject;
                s2tldac5.gameObject.SetActive(true);
            }
            if (S2TLDAL1 == true)
            {
                GameObject s2tldaL1 = LUD.transform.Find("Suit2 top left down arm LED1").gameObject;
                s2tldaL1.gameObject.SetActive(true);
            }
            else if (S2TLDAL2 == true)
            {
                GameObject s2tldaL2 = LUD.transform.Find("Suit2 top left down arm LED2").gameObject;
                s2tldaL2.gameObject.SetActive(true);
            }
            else if (S2TLDAL3 == true)
            {
                GameObject s2tldaL3 = LUD.transform.Find("Suit2 top left down arm LED3").gameObject;
                s2tldaL3.gameObject.SetActive(true);
            }
            else if (S2TLDAL4 == true)
            {
                GameObject s2tldaL4 = LUD.transform.Find("Suit2 top left down arm LED4").gameObject;
                s2tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s2tldaL5 = LUD.transform.Find("Suit2 top left down arm LED5").gameObject;
                s2tldaL5.gameObject.SetActive(true);
            }
            if (S3TLDACC1 == true)
            {
                GameObject s3tldacc1 = LUD.transform.Find("Suit3 top left down arm cloth color1").gameObject;
                s3tldacc1.gameObject.SetActive(true);
            }
            else if (S3TLDACC2 == true)
            {
                GameObject s3tldacc2 = LUD.transform.Find("Suit3 top left down arm cloth color2").gameObject;
                s3tldacc2.gameObject.SetActive(true);
            }
            else if (S3TLDACC3 == true)
            {
                GameObject s3tldacc3 = LUD.transform.Find("Suit3 top left down arm cloth color3").gameObject;
                s3tldacc3.gameObject.SetActive(true);
            }
            else if (S3TLDACC4 == true)
            {
                GameObject s3tldacc4 = LUD.transform.Find("Suit3 top left down arm cloth color4").gameObject;
                s3tldacc4.gameObject.SetActive(true);
            }
            else if (S3TLDACC5 == true)
            {
                GameObject s3tldacc5 = LUD.transform.Find("Suit3 top left down arm cloth color5").gameObject;
                s3tldacc5.gameObject.SetActive(true);
            }
            if (S3TLDAC1 == true)
            {
                GameObject s3tldac1 = LUD.transform.Find("Suit3 top left down arm color1").gameObject;
                s3tldac1.gameObject.SetActive(true);
            }
            else if (S3TLDAC2 == true)
            {
                GameObject s3tldac2 = LUD.transform.Find("Suit3 top left down arm color2").gameObject;
                s3tldac2.gameObject.SetActive(true);
            }
            else if (S3TLDAC3 == true)
            {
                GameObject s3tldac3 = LUD.transform.Find("Suit3 top left down arm color3").gameObject;
                s3tldac3.gameObject.SetActive(true);
            }
            else if (S3TLDAC4 == true)
            {
                GameObject s3tldac4 = LUD.transform.Find("Suit3 top left down arm color4").gameObject;
                s3tldac4.gameObject.SetActive(true);
            }
            else if (S3TLDAC5 == true)
            {
                GameObject s3tldac5 = LUD.transform.Find("Suit3 top left down arm color5").gameObject;
                s3tldac5.gameObject.SetActive(true);
            }
            if (S3TLDAL1 == true)
            {
                GameObject s3tldaL1 = LUD.transform.Find("Suit3 top left down arm LED1").gameObject;
                s3tldaL1.gameObject.SetActive(true);
            }
            else if (S3TLDAL2 == true)
            {
                GameObject s3tldaL2 = LUD.transform.Find("Suit3 top left down arm LED2").gameObject;
                s3tldaL2.gameObject.SetActive(true);
            }
            else if (S3TLDAL3 == true)
            {
                GameObject s3tldaL3 = LUD.transform.Find("Suit3 top left down arm LED3").gameObject;
                s3tldaL3.gameObject.SetActive(true);
            }
            else if (S3TLDAL4 == true)
            {
                GameObject s3tldaL4 = LUD.transform.Find("Suit3 top left down arm LED4").gameObject;
                s3tldaL4.gameObject.SetActive(true);
            }
            else if (S2TLDAL5 == true)
            {
                GameObject s3tldaL5 = LUD.transform.Find("Suit3 top left down arm LED5").gameObject;
                s3tldaL5.gameObject.SetActive(true);
            }
        }
    }

    void LeftArmUpcut()
    {
        if (LeftArmUpOUTonline == true)
        {
            GameObject LU = Instantiate(LeftUp, LeftUppos.transform.position, LeftUppos.transform.rotation);
            Destroy(LU, 30);

            LU.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                LU.GetComponent<TearCreateInfector>().Direction = false;
            else
                LU.GetComponent<TearCreateInfector>().Direction = true;

            //왼팔 상단
            if (BODY1LUA == true)
            {
                GameObject body1lua = LU.transform.Find("Body1 left up arm").gameObject;
                body1lua.gameObject.SetActive(true);
            }
            else if (BODY2LUA == true)
            {
                GameObject body2lua = LU.transform.Find("Body2 left up arm").gameObject;
                body2lua.gameObject.SetActive(true);
            }
            if (CT1LUA == true)
            {
                GameObject ct1lua = LU.transform.Find("Clothes top1 left up arm").gameObject;
                ct1lua.gameObject.SetActive(true);
            }
            else if (CT2LA == true)
            {
                GameObject ct2la = LU.transform.Find("Clothes top2 left arm").gameObject;
                ct2la.gameObject.SetActive(true);
            }
            if (S1TLUAC1 == true)
            {
                GameObject s1tluac1 = LU.transform.Find("Suit1 top left up arm color1").gameObject;
                s1tluac1.gameObject.SetActive(true);
            }
            else if (S1TLUAC2 == true)
            {
                GameObject s1tluac2 = LU.transform.Find("Suit1 top left up arm color2").gameObject;
                s1tluac2.gameObject.SetActive(true);
            }
            else if (S1TLUAC3 == true)
            {
                GameObject s1tluac3 = LU.transform.Find("Suit1 top left up arm color3").gameObject;
                s1tluac3.gameObject.SetActive(true);
            }
            else if (S1TLUAC4 == true)
            {
                GameObject s1tluac4 = LU.transform.Find("Suit1 top left up arm color4").gameObject;
                s1tluac4.gameObject.SetActive(true);
            }
            else if (S1TLUAC5 == true)
            {
                GameObject s1tluac5 = LU.transform.Find("Suit1 top left up arm color5").gameObject;
                s1tluac5.gameObject.SetActive(true);
            }
            if (S2TLUAC1 == true)
            {
                GameObject s2tluac1 = LU.transform.Find("Suit2 top left up arm color1").gameObject;
                s2tluac1.gameObject.SetActive(true);
            }
            else if (S2TLUAC2 == true)
            {
                GameObject s2tluac2 = LU.transform.Find("Suit2 top left up arm color2").gameObject;
                s2tluac2.gameObject.SetActive(true);
            }
            else if (S2TLUAC3 == true)
            {
                GameObject s2tluac3 = LU.transform.Find("Suit2 top left up arm color3").gameObject;
                s2tluac3.gameObject.SetActive(true);
            }
            else if (S2TLUAC4 == true)
            {
                GameObject s2tluac4 = LU.transform.Find("Suit2 top left up arm color4").gameObject;
                s2tluac4.gameObject.SetActive(true);
            }
            else if (S2TLUAC5 == true)
            {
                GameObject s2tluac5 = LU.transform.Find("Suit2 top left up arm color5").gameObject;
                s2tluac5.gameObject.SetActive(true);
            }
            if (S3TLUAC1 == true)
            {
                GameObject s3tluac1 = LU.transform.Find("Suit3 top left up arm color1").gameObject;
                s3tluac1.gameObject.SetActive(true);
            }
            else if (S3TLUAC2 == true)
            {
                GameObject s3tluac2 = LU.transform.Find("Suit3 top left up arm color2").gameObject;
                s3tluac2.gameObject.SetActive(true);
            }
            else if (S3TLUAC3 == true)
            {
                GameObject s3tluac3 = LU.transform.Find("Suit3 top left up arm color3").gameObject;
                s3tluac3.gameObject.SetActive(true);
            }
            else if (S3TLUAC4 == true)
            {
                GameObject s3tluac4 = LU.transform.Find("Suit3 top left up arm color4").gameObject;
                s3tluac4.gameObject.SetActive(true);
            }
            else if (S3TLUAC5 == true)
            {
                GameObject s3tluac5 = LU.transform.Find("Suit3 top left up arm color5").gameObject;
                s3tluac5.gameObject.SetActive(true);
            }
        }
    }

    void LeftLegcut()
    {
        if (LeftLegOUTonline == true)
        {
            GameObject LL = Instantiate(LeftLeg, LeftLegpos.transform.position, LeftLegpos.transform.rotation);
            Destroy(LL, 30);

            LL.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                LL.GetComponent<TearCreateInfector>().Direction = false;
            else
                LL.GetComponent<TearCreateInfector>().Direction = true;

            if (BODY1LL == true)
            {
                GameObject body1ll = LL.transform.Find("Body1 left leg").gameObject;
                body1ll.gameObject.SetActive(true);
            }
            else if (BODY2LL == true)
            {
                GameObject body2ll = LL.transform.Find("Body2 left leg").gameObject;
                body2ll.gameObject.SetActive(true);
            }
            if (FOOTL == true)
            {
                GameObject footL = LL.transform.Find("Left foot1").gameObject;
                footL.gameObject.SetActive(true);
            }

            if (P1LC1 == true)
            {
                GameObject p1lc1 = LL.transform.Find("Pants1 left color1").gameObject;
                p1lc1.gameObject.SetActive(true);
            }
            else if (P1LC2 == true)
            {
                GameObject p1lc2 = LL.transform.Find("Pants1 left color2").gameObject;
                p1lc2.gameObject.SetActive(true);
            }
            else if (P1LC3 == true)
            {
                GameObject p1lc3 = LL.transform.Find("Pants1 left color3").gameObject;
                p1lc3.gameObject.SetActive(true);
            }
            else if (P1LC4 == true)
            {
                GameObject p1lc4 = LL.transform.Find("Pants1 left color4").gameObject;
                p1lc4.gameObject.SetActive(true);
            }
            if (P1LCC1 == true)
            {
                GameObject p1lcc1 = LL.transform.Find("Pants1 left cloth color1").gameObject;
                p1lcc1.gameObject.SetActive(true);
            }
            else if (P1LCC2 == true)
            {
                GameObject p1lcc2 = LL.transform.Find("Pants1 left cloth color2").gameObject;
                p1lcc2.gameObject.SetActive(true);
            }
            else if (P1LCC3 == true)
            {
                GameObject p1lcc3 = LL.transform.Find("Pants1 left cloth color3").gameObject;
                p1lcc3.gameObject.SetActive(true);
            }
            else if (P1LCC4 == true)
            {
                GameObject p1lcc4 = LL.transform.Find("Pants1 left cloth color4").gameObject;
                p1lcc4.gameObject.SetActive(true);
            }
            if (P2LC1 == true)
            {
                GameObject p2lc1 = LL.transform.Find("Pants2 left color1").gameObject;
                p2lc1.gameObject.SetActive(true);
            }
            else if (P2LC2 == true)
            {
                GameObject p2lc2 = LL.transform.Find("Pants2 left color2").gameObject;
                p2lc2.gameObject.SetActive(true);
            }
            else if (P2LC3 == true)
            {
                GameObject p2lc3 = LL.transform.Find("Pants2 left color3").gameObject;
                p2lc3.gameObject.SetActive(true);
            }
            else if (P2LC4 == true)
            {
                GameObject p2lc4 = LL.transform.Find("Pants2 left color4").gameObject;
                p2lc4.gameObject.SetActive(true);
            }
            if (P2LCC1 == true)
            {
                GameObject p2lcc1 = LL.transform.Find("Pants2 left cloth color1").gameObject;
                p2lcc1.gameObject.SetActive(true);
            }
            else if (P2LCC2 == true)
            {
                GameObject p2lcc2 = LL.transform.Find("Pants2 left cloth color2").gameObject;
                p2lcc2.gameObject.SetActive(true);
            }
            else if (P2LCC3 == true)
            {
                GameObject p2lcc3 = LL.transform.Find("Pants2 left cloth color3").gameObject;
                p2lcc3.gameObject.SetActive(true);
            }
            else if (P2LCC4 == true)
            {
                GameObject p2lcc4 = LL.transform.Find("Pants2 left cloth color4").gameObject;
                p2lcc4.gameObject.SetActive(true);
            }
            if (P2LL1 == true)
            {
                GameObject p2lL1 = LL.transform.Find("Pants2 left LED1").gameObject;
                p2lL1.gameObject.SetActive(true);
            }
            else if (P2LL2 == true)
            {
                GameObject p2lL2 = LL.transform.Find("Pants2 left LED2").gameObject;
                p2lL2.gameObject.SetActive(true);
            }
            else if (P2LL3 == true)
            {
                GameObject p2lL3 = LL.transform.Find("Pants2 left LED3").gameObject;
                p2lL3.gameObject.SetActive(true);
            }
            else if (P2LL4 == true)
            {
                GameObject p2lL4 = LL.transform.Find("Pants2 left LED4").gameObject;
                p2lL4.gameObject.SetActive(true);
            }
            else if (P2LL5 == true)
            {
                GameObject p2lL5 = LL.transform.Find("Pants2 left LED5").gameObject;
                p2lL5.gameObject.SetActive(true);
            }
            if (S1LC1 == true)
            {
                GameObject s1lc1 = LL.transform.Find("Shoes1 left color1").gameObject;
                s1lc1.gameObject.SetActive(true);
            }
            else if (S1LC2 == true)
            {
                GameObject s1lc2 = LL.transform.Find("Shoes1 left color2").gameObject;
                s1lc2.gameObject.SetActive(true);
            }
            else if (S1LC3 == true)
            {
                GameObject s1lc3 = LL.transform.Find("Shoes1 left color3").gameObject;
                s1lc3.gameObject.SetActive(true);
            }
            else if (S1LC4 == true)
            {
                GameObject s1lc4 = LL.transform.Find("Shoes1 left color4").gameObject;
                s1lc4.gameObject.SetActive(true);
            }
            else if (S1LC5 == true)
            {
                GameObject s1lc5 = LL.transform.Find("Shoes1 left color5").gameObject;
                s1lc5.gameObject.SetActive(true);
            }
            if (S1LC1T == true)
            {
                GameObject s1lc1t = LL.transform.Find("Shoes1 left color1 tear").gameObject;
                s1lc1t.gameObject.SetActive(true);
            }
            else if (S1LC2T == true)
            {
                GameObject s1lc2t = LL.transform.Find("Shoes1 left color2 tear").gameObject;
                s1lc2t.gameObject.SetActive(true);
            }
            else if (S1LC3T == true)
            {
                GameObject s1lc3t = LL.transform.Find("Shoes1 left color3 tear").gameObject;
                s1lc3t.gameObject.SetActive(true);
            }
            else if (S1LC4T == true)
            {
                GameObject s1lc4t = LL.transform.Find("Shoes1 left color4 tear").gameObject;
                s1lc4t.gameObject.SetActive(true);
            }
            else if (S1LC5T == true)
            {
                GameObject s1lc5t = LL.transform.Find("Shoes1 left color5 tear").gameObject;
                s1lc5t.gameObject.SetActive(true);
            }
            if (S2LC1 == true)
            {
                GameObject s2lc1 = LL.transform.Find("Shoes2 left color1").gameObject;
                s2lc1.gameObject.SetActive(true);
            }
            else if (S2LC2 == true)
            {
                GameObject s2lc2 = LL.transform.Find("Shoes2 left color2").gameObject;
                s2lc2.gameObject.SetActive(true);
            }
            else if (S2LC3 == true)
            {
                GameObject s2lc3 = LL.transform.Find("Shoes2 left color3").gameObject;
                s2lc3.gameObject.SetActive(true);
            }
            else if (S2LC4 == true)
            {
                GameObject s2lc4 = LL.transform.Find("Shoes2 left color4").gameObject;
                s2lc4.gameObject.SetActive(true);
            }
            else if (S2LC5 == true)
            {
                GameObject s2lc5 = LL.transform.Find("Shoes2 left color5").gameObject;
                s2lc5.gameObject.SetActive(true);
            }

            if (S1DLC1 == true)
            {
                GameObject s1dlc1 = LL.transform.Find("Suit1 down left color1").gameObject;
                s1dlc1.gameObject.SetActive(true);
            }
            else if (S1DLC2 == true)
            {
                GameObject s1dlc2 = LL.transform.Find("Suit1 down left color2").gameObject;
                s1dlc2.gameObject.SetActive(true);
            }
            else if (S1DLC3 == true)
            {
                GameObject s1dlc3 = LL.transform.Find("Suit1 down left color3").gameObject;
                s1dlc3.gameObject.SetActive(true);
            }
            else if (S1DLC4 == true)
            {
                GameObject s1dlc4 = LL.transform.Find("Suit1 down left color4").gameObject;
                s1dlc4.gameObject.SetActive(true);
            }
            else if (S1DLC5 == true)
            {
                GameObject s1dlc5 = LL.transform.Find("Suit1 down left color5").gameObject;
                s1dlc5.gameObject.SetActive(true);
            }
            if (S1DLCC1 == true)
            {
                GameObject s1dlcc1 = LL.transform.Find("Suit1 down left cloth color1").gameObject;
                s1dlcc1.gameObject.SetActive(true);
            }
            else if (S1DLCC2 == true)
            {
                GameObject s1dlcc2 = LL.transform.Find("Suit1 down left cloth color2").gameObject;
                s1dlcc2.gameObject.SetActive(true);
            }
            else if (S1DLCC3 == true)
            {
                GameObject s1dlcc3 = LL.transform.Find("Suit1 down left cloth color3").gameObject;
                s1dlcc3.gameObject.SetActive(true);
            }
            else if (S1DLCC4 == true)
            {
                GameObject s1dlcc4 = LL.transform.Find("Suit1 down left cloth color4").gameObject;
                s1dlcc4.gameObject.SetActive(true);
            }
            else if (S1DLCC5 == true)
            {
                GameObject s1dlcc5 = LL.transform.Find("Suit1 down left cloth color5").gameObject;
                s1dlcc5.gameObject.SetActive(true);
            }
            if (S1DLL1T == true)
            {
                GameObject s1dlL1t = LL.transform.Find("Suit1 down left LED1 tear").gameObject;
                s1dlL1t.gameObject.SetActive(true);
            }
            else if (S1DLL2T == true)
            {
                GameObject s1dlL2t = LL.transform.Find("Suit1 down left LED2 tear").gameObject;
                s1dlL2t.gameObject.SetActive(true);
            }
            else if (S1DLL3T == true)
            {
                GameObject s1dlL3t = LL.transform.Find("Suit1 down left LED3 tear").gameObject;
                s1dlL3t.gameObject.SetActive(true);
            }
            else if (S1DLL4T == true)
            {
                GameObject s1dlL4t = LL.transform.Find("Suit1 down left LED4 tear").gameObject;
                s1dlL4t.gameObject.SetActive(true);
            }
            else if (S1DLL5T == true)
            {
                GameObject s1dlL5t = LL.transform.Find("Suit1 down left LED5 tear").gameObject;
                s1dlL5t.gameObject.SetActive(true);
            }
            if (S1DLL1 == true)
            {
                GameObject s1dlL1 = LL.transform.Find("Suit1 down left LED1").gameObject;
                s1dlL1.gameObject.SetActive(true);
            }
            else if (S1DLL2 == true)
            {
                GameObject s1dlL2 = LL.transform.Find("Suit1 down left LED2").gameObject;
                s1dlL2.gameObject.SetActive(true);
            }
            else if (S1DLL3 == true)
            {
                GameObject s1dlL3 = LL.transform.Find("Suit1 down left LED3").gameObject;
                s1dlL3.gameObject.SetActive(true);
            }
            else if (S1DLL4 == true)
            {
                GameObject s1dlL4 = LL.transform.Find("Suit1 down left LED4").gameObject;
                s1dlL4.gameObject.SetActive(true);
            }
            else if (S1DLL5 == true)
            {
                GameObject s1dlL5 = LL.transform.Find("Suit1 down left LED5").gameObject;
                s1dlL5.gameObject.SetActive(true);
            }
            if (S3LC1 == true)
            {
                GameObject s3lc1 = LL.transform.Find("Shoes3 left color1").gameObject;
                s3lc1.gameObject.SetActive(true);
            }
            else if (S3LC2 == true)
            {
                GameObject s3lc2 = LL.transform.Find("Shoes3 left color2").gameObject;
                s3lc2.gameObject.SetActive(true);
            }
            else if (S3LC3 == true)
            {
                GameObject s3lc3 = LL.transform.Find("Shoes3 left color3").gameObject;
                s3lc3.gameObject.SetActive(true);
            }
            if (S3LC1T == true)
            {
                GameObject s3lc1t = LL.transform.Find("Shoes3 left color1 tear").gameObject;
                s3lc1t.gameObject.SetActive(true);
            }
            else if (S3LC2T == true)
            {
                GameObject s3lc2t = LL.transform.Find("Shoes3 left color2 tear").gameObject;
                s3lc2t.gameObject.SetActive(true);
            }
            else if (S3LC3T == true)
            {
                GameObject s3lc3t = LL.transform.Find("Shoes3 left color3 tear").gameObject;
                s3lc3t.gameObject.SetActive(true);
            }

            if (S2DLC1 == true)
            {
                GameObject s2dlc1 = LL.transform.Find("Suit2 down left color1").gameObject;
                s2dlc1.gameObject.SetActive(true);
            }
            else if (S2DLC2 == true)
            {
                GameObject s2dlc2 = LL.transform.Find("Suit2 down left color2").gameObject;
                s2dlc2.gameObject.SetActive(true);
            }
            else if (S2DLC3 == true)
            {
                GameObject s2dlc3 = LL.transform.Find("Suit2 down left color3").gameObject;
                s2dlc3.gameObject.SetActive(true);
            }
            else if (S2DLC4 == true)
            {
                GameObject s2dlc4 = LL.transform.Find("Suit2 down left color4").gameObject;
                s2dlc4.gameObject.SetActive(true);
            }
            else if (S2DLC5 == true)
            {
                GameObject s2dlc5 = LL.transform.Find("Suit2 down left color5").gameObject;
                s2dlc5.gameObject.SetActive(true);
            }
            if (S2DLCC1 == true)
            {
                GameObject s2dlcc1 = LL.transform.Find("Suit2 down left cloth color1").gameObject;
                s2dlcc1.gameObject.SetActive(true);
            }
            else if (S2DLCC2 == true)
            {
                GameObject s2dlcc2 = LL.transform.Find("Suit2 down left cloth color2").gameObject;
                s2dlcc2.gameObject.SetActive(true);
            }
            else if (S2DLCC3 == true)
            {
                GameObject s2dlcc3 = LL.transform.Find("Suit2 down left cloth color3").gameObject;
                s2dlcc3.gameObject.SetActive(true);
            }
            else if (S2DLCC4 == true)
            {
                GameObject s2dlcc4 = LL.transform.Find("Suit2 down left cloth color4").gameObject;
                s2dlcc4.gameObject.SetActive(true);
            }
            else if (S2DLCC5 == true)
            {
                GameObject s2dlcc5 = LL.transform.Find("Suit2 down left cloth color5").gameObject;
                s2dlcc5.gameObject.SetActive(true);
            }
            if (S4LC1 == true)
            {
                GameObject s4lc1 = LL.transform.Find("Shoes4 left color1").gameObject;
                s4lc1.gameObject.SetActive(true);
            }
            else if (S4LC2 == true)
            {
                GameObject s4lc2 = LL.transform.Find("Shoes4 left color2").gameObject;
                s4lc2.gameObject.SetActive(true);
            }
            if (S4LC1T == true)
            {
                GameObject s4lc1t = LL.transform.Find("Shoes4 left color1 tear").gameObject;
                s4lc1t.gameObject.SetActive(true);
            }
            else if (S4LC2T == true)
            {
                GameObject s4lc2t = LL.transform.Find("Shoes4 left color2 tear").gameObject;
                s4lc2t.gameObject.SetActive(true);
            }
            if (S2DLL1 == true)
            {
                GameObject s2dlL1 = LL.transform.Find("Suit2 down left LED1").gameObject;
                s2dlL1.gameObject.SetActive(true);
            }
            else if (S2DLL2 == true)
            {
                GameObject s2dlL2 = LL.transform.Find("Suit2 down left LED2").gameObject;
                s2dlL2.gameObject.SetActive(true);
            }
            else if (S2DLL3 == true)
            {
                GameObject s2dlL3 = LL.transform.Find("Suit2 down left LED3").gameObject;
                s2dlL3.gameObject.SetActive(true);
            }
            else if (S2DLL4 == true)
            {
                GameObject s2dlL4 = LL.transform.Find("Suit2 down left LED4").gameObject;
                s2dlL4.gameObject.SetActive(true);
            }
            else if (S2DLL5 == true)
            {
                GameObject s2dlL5 = LL.transform.Find("Suit2 down left LED5").gameObject;
                s2dlL5.gameObject.SetActive(true);
            }

            if (S3DLC1 == true)
            {
                GameObject s3dlc1 = LL.transform.Find("Suit3 down left color1").gameObject;
                s3dlc1.gameObject.SetActive(true);
            }
            else if (S3DLC2 == true)
            {
                GameObject s3dlc2 = LL.transform.Find("Suit3 down left color2").gameObject;
                s3dlc2.gameObject.SetActive(true);
            }
            else if (S3DLC3 == true)
            {
                GameObject s3dlc3 = LL.transform.Find("Suit3 down left color3").gameObject;
                s3dlc3.gameObject.SetActive(true);
            }
            else if (S3DLC4 == true)
            {
                GameObject s3dlc4 = LL.transform.Find("Suit3 down left color4").gameObject;
                s3dlc4.gameObject.SetActive(true);
            }
            else if (S3DLC5 == true)
            {
                GameObject s3dlc5 = LL.transform.Find("Suit3 down left color5").gameObject;
                s3dlc5.gameObject.SetActive(true);
            }
            if (S3DLCC1 == true)
            {
                GameObject s3dlcc1 = LL.transform.Find("Suit3 down left cloth color1").gameObject;
                s3dlcc1.gameObject.SetActive(true);
            }
            else if (S3DLCC2 == true)
            {
                GameObject s3dlcc2 = LL.transform.Find("Suit3 down left cloth color2").gameObject;
                s3dlcc2.gameObject.SetActive(true);
            }
            else if (S3DLCC3 == true)
            {
                GameObject s3dlcc3 = LL.transform.Find("Suit3 down left cloth color3").gameObject;
                s3dlcc3.gameObject.SetActive(true);
            }
            else if (S3DLCC4 == true)
            {
                GameObject s3dlcc4 = LL.transform.Find("Suit3 down left cloth color4").gameObject;
                s3dlcc4.gameObject.SetActive(true);
            }
            else if (S3DLCC5 == true)
            {
                GameObject s3dlcc5 = LL.transform.Find("Suit3 down left cloth color5").gameObject;
                s3dlcc5.gameObject.SetActive(true);
            }
            if (S5LC1 == true)
            {
                GameObject s5lc1 = transform.Find("Shoes5 left color1").gameObject;
                s5lc1.gameObject.SetActive(true);
            }
            else if (S5LC2 == true)
            {
                GameObject s5lc2 = LL.transform.Find("Shoes5 left color2").gameObject;
                s5lc2.gameObject.SetActive(true);
            }
            else if (S5LC3 == true)
            {
                GameObject s5lc3 = LL.transform.Find("Shoes5 left color3").gameObject;
                s5lc3.gameObject.SetActive(true);
            }
            else if (S5LC4 == true)
            {
                GameObject s5lc4 = LL.transform.Find("Shoes5 left color4").gameObject;
                s5lc4.gameObject.SetActive(true);
            }
            else if (S5LC5 == true)
            {
                GameObject s5lc5 = LL.transform.Find("Shoes5 left color5").gameObject;
                s5lc5.gameObject.SetActive(true);
            }
            if (S5LC1T == true)
            {
                GameObject s5lc1t = LL.transform.Find("Shoes5 left color1 tear").gameObject;
                s5lc1t.gameObject.SetActive(true);
            }
            else if (S5LC2T == true)
            {
                GameObject s5lc2t = LL.transform.Find("Shoes5 left color2 tear").gameObject;
                s5lc2t.gameObject.SetActive(true);
            }
            else if (S5LC3T == true)
            {
                GameObject s5lc3t = LL.transform.Find("Shoes5 left color3 tear").gameObject;
                s5lc3t.gameObject.SetActive(true);
            }
            else if (S5LC4T == true)
            {
                GameObject s5lc4t = LL.transform.Find("Shoes5 left color4 tear").gameObject;
                s5lc4t.gameObject.SetActive(true);
            }
            else if (S5LC5T == true)
            {
                GameObject s5lc5t = LL.transform.Find("Shoes5 left color5 tear").gameObject;
                s5lc5t.gameObject.SetActive(true);
            }
            if (S3DLL1 == true)
            {
                GameObject s3dlL1 = LL.transform.Find("Suit3 down left LED1").gameObject;
                s3dlL1.gameObject.SetActive(true);
            }
            else if (S3DLL2 == true)
            {
                GameObject s3dlL2 = LL.transform.Find("Suit3 down left LED2").gameObject;
                s3dlL2.gameObject.SetActive(true);
            }
            else if (S3DLL3 == true)
            {
                GameObject s3dlL3 = LL.transform.Find("Suit3 down left LED3").gameObject;
                s3dlL3.gameObject.SetActive(true);
            }
            else if (S3DLL4 == true)
            {
                GameObject s3dlL4 = LL.transform.Find("Suit3 down left LED4").gameObject;
                s3dlL4.gameObject.SetActive(true);
            }
            else if (S3DLL5 == true)
            {
                GameObject s3dlL5 = LL.transform.Find("Suit3 down left LED5").gameObject;
                s3dlL5.gameObject.SetActive(true);
            }
            else if (S3DLL1T == true)
            {
                GameObject s3dlL1t = LL.transform.Find("Suit3 down left LED1 tear").gameObject;
                s3dlL1t.gameObject.SetActive(true);
            }
            else if (S3DLL2T == true)
            {
                GameObject s3dlL2t = LL.transform.Find("Suit3 down left LED2 tear").gameObject;
                s3dlL2t.gameObject.SetActive(true);
            }
            else if (S3DLL3T == true)
            {
                GameObject s3dlL3t = LL.transform.Find("Suit3 down left LED3 tear").gameObject;
                s3dlL3t.gameObject.SetActive(true);
            }
            else if (S3DLL4T == true)
            {
                GameObject s3dlL4t = LL.transform.Find("Suit3 down left LED4 tear").gameObject;
                s3dlL4t.gameObject.SetActive(true);
            }
            else if (S3DLL5T == true)
            {
                GameObject s3dlL5t = LL.transform.Find("Suit3 down left LED5 tear").gameObject;
                s3dlL5t.gameObject.SetActive(true);
            }
        }
    }

    void RightLegcut()
    {
        if (RightLegOUTonline == true)
        {
            GameObject RL = Instantiate(RightLeg, RightLegpos.transform.position, RightLegpos.transform.rotation);
            Destroy(RL, 30);

            RL.GetComponent<TearCreateInfector>().SetThrow(LargeThrow);

            if (Direction == false)
                RL.GetComponent<TearCreateInfector>().Direction = false;
            else
                RL.GetComponent<TearCreateInfector>().Direction = true;

            if (BODY1RL == true)
            {
                GameObject body1rl = RL.transform.Find("Body1 right leg").gameObject;
                body1rl.gameObject.SetActive(true);
            }
            else if (BODY2RL == true)
            {
                GameObject body2rl = RL.transform.Find("Body2 right leg").gameObject;
                body2rl.gameObject.SetActive(true);
            }
            if (FOOTR == true)
            {
                GameObject footR = RL.transform.Find("Right foot1").gameObject;
                footR.gameObject.SetActive(true);
            }

            if (P1RC1 == true)
            {
                GameObject p1rc1 = RL.transform.Find("Pants1 right color1").gameObject;
                p1rc1.gameObject.SetActive(true);
            }
            else if (P1RC2 == true)
            {
                GameObject p1rc2 = RL.transform.Find("Pants1 right color2").gameObject;
                p1rc2.gameObject.SetActive(true);
            }
            else if (P1RC3 == true)
            {
                GameObject p1rc3 = RL.transform.Find("Pants1 right color3").gameObject;
                p1rc3.gameObject.SetActive(true);
            }
            else if (P1RC4 == true)
            {
                GameObject p1rc4 = RL.transform.Find("Pants1 right color4").gameObject;
                p1rc4.gameObject.SetActive(true);
            }
            if (P1RCC1 == true)
            {
                GameObject p1rcc1 = RL.transform.Find("Pants1 right cloth color1").gameObject;
                p1rcc1.gameObject.SetActive(true);
            }
            else if (P1RCC2 == true)
            {
                GameObject p1rcc2 = RL.transform.Find("Pants1 right cloth color2").gameObject;
                p1rcc2.gameObject.SetActive(true);
            }
            else if (P1RCC3 == true)
            {
                GameObject p1rcc3 = RL.transform.Find("Pants1 right cloth color3").gameObject;
                p1rcc3.gameObject.SetActive(true);
            }
            else if (P1RCC4 == true)
            {
                GameObject p1rcc4 = RL.transform.Find("Pants1 right cloth color4").gameObject;
                p1rcc4.gameObject.SetActive(true);
            }
            if (P1RL1 == true)
            {
                GameObject p1rL1 = RL.transform.Find("Pants1 right LED1").gameObject;
                p1rL1.gameObject.SetActive(true);
            }
            else if (P1RL2 == true)
            {
                GameObject p1rL2 = RL.transform.Find("Pants1 right LED2").gameObject;
                p1rL2.gameObject.SetActive(true);
            }
            else if (P1RL3 == true)
            {
                GameObject p1rL3 = RL.transform.Find("Pants1 right LED3").gameObject;
                p1rL3.gameObject.SetActive(true);
            }
            else if (P1RL4 == true)
            {
                GameObject p1rL4 = RL.transform.Find("Pants1 right LED4").gameObject;
                p1rL4.gameObject.SetActive(true);
            }
            else if (P1RL5 == true)
            {
                GameObject p1rL5 = RL.transform.Find("Pants1 right LED5").gameObject;
                p1rL5.gameObject.SetActive(true);
            }
            if (P2RC1 == true)
            {
                GameObject p2rc1 = RL.transform.Find("Pants2 right color1").gameObject;
                p2rc1.gameObject.SetActive(true);
            }
            else if (P2RC2 == true)
            {
                GameObject p2rc2 = RL.transform.Find("Pants2 right color2").gameObject;
                p2rc2.gameObject.SetActive(true);
            }
            else if (P2RC3 == true)
            {
                GameObject p2rc3 = RL.transform.Find("Pants2 right color3").gameObject;
                p2rc3.gameObject.SetActive(true);
            }
            else if (P2RC4 == true)
            {
                GameObject p2rc4 = RL.transform.Find("Pants2 right color4").gameObject;
                p2rc4.gameObject.SetActive(true);
            }
            if (P2RCC1 == true)
            {
                GameObject p2rcc1 = RL.transform.Find("Pants2 right cloth color1").gameObject;
                p2rcc1.gameObject.SetActive(true);
            }
            else if (P2RCC2 == true)
            {
                GameObject p2rcc2 = RL.transform.Find("Pants2 right cloth color2").gameObject;
                p2rcc2.gameObject.SetActive(true);
            }
            else if (P2RCC3 == true)
            {
                GameObject p2rcc3 = RL.transform.Find("Pants2 right cloth color3").gameObject;
                p2rcc3.gameObject.SetActive(true);
            }
            else if (P2RCC4 == true)
            {
                GameObject p2rcc4 = RL.transform.Find("Pants2 right cloth color4").gameObject;
                p2rcc4.gameObject.SetActive(true);
            }
            if (P2RL1 == true)
            {
                GameObject p2rL1 = RL.transform.Find("Pants2 right LED1").gameObject;
                p2rL1.gameObject.SetActive(true);
            }
            else if (P2RL2 == true)
            {
                GameObject p2rL2 = RL.transform.Find("Pants2 right LED2").gameObject;
                p2rL2.gameObject.SetActive(true);
            }
            else if (P2RL3 == true)
            {
                GameObject p2rL3 = RL.transform.Find("Pants2 right LED3").gameObject;
                p2rL3.gameObject.SetActive(true);
            }
            else if (P2RL4 == true)
            {
                GameObject p2rL4 = RL.transform.Find("Pants2 right LED4").gameObject;
                p2rL4.gameObject.SetActive(true);
            }
            else if (P2RL5 == true)
            {
                GameObject p2rL5 = RL.transform.Find("Pants2 right LED5").gameObject;
                p2rL5.gameObject.SetActive(true);
            }
            if (S1RC1 == true)
            {
                GameObject s1rc1 = RL.transform.Find("Shoes1 right color1").gameObject;
                s1rc1.gameObject.SetActive(true);
            }
            else if (S1RC2 == true)
            {
                GameObject s1rc2 = RL.transform.Find("Shoes1 right color2").gameObject;
                s1rc2.gameObject.SetActive(true);
            }
            else if (S1RC3 == true)
            {
                GameObject s1rc3 = RL.transform.Find("Shoes1 right color3").gameObject;
                s1rc3.gameObject.SetActive(true);
            }
            else if (S1RC4 == true)
            {
                GameObject s1rc4 = RL.transform.Find("Shoes1 right color4").gameObject;
                s1rc4.gameObject.SetActive(true);
            }
            else if (S1RC5 == true)
            {
                GameObject s1rc5 = RL.transform.Find("Shoes1 right color5").gameObject;
                s1rc5.gameObject.SetActive(true);
            }
            if (S1RC1T == true)
            {
                GameObject s1rc1t = RL.transform.Find("Shoes1 right color1 tear").gameObject;
                s1rc1t.gameObject.SetActive(true);
            }
            else if (S1RC2T == true)
            {
                GameObject s1rc2t = RL.transform.Find("Shoes1 right color2 tear").gameObject;
                s1rc2t.gameObject.SetActive(true);
            }
            else if (S1RC3T == true)
            {
                GameObject s1rc3t = RL.transform.Find("Shoes1 right color3 tear").gameObject;
                s1rc3t.gameObject.SetActive(true);
            }
            else if (S1RC4T == true)
            {
                GameObject s1rc4t = RL.transform.Find("Shoes1 right color4 tear").gameObject;
                s1rc4t.gameObject.SetActive(true);
            }
            else if (S1RC5T == true)
            {
                GameObject s1rc5t = RL.transform.Find("Shoes1 right color5 tear").gameObject;
                s1rc5t.gameObject.SetActive(true);
            }
            if (S2RC1 == true)
            {
                GameObject s2rc1 = RL.transform.Find("Shoes2 right color1").gameObject;
                s2rc1.gameObject.SetActive(true);
            }
            else if (S2RC2 == true)
            {
                GameObject s2rc2 = RL.transform.Find("Shoes2 right color2").gameObject;
                s2rc2.gameObject.SetActive(true);
            }
            else if (S2RC3 == true)
            {
                GameObject s2rc3 = RL.transform.Find("Shoes2 right color3").gameObject;
                s2rc3.gameObject.SetActive(true);
            }
            else if (S2RC4 == true)
            {
                GameObject s2rc4 = RL.transform.Find("Shoes2 right color4").gameObject;
                s2rc4.gameObject.SetActive(true);
            }
            else if (S2RC5 == true)
            {
                GameObject s2rc5 = RL.transform.Find("Shoes2 right color5").gameObject;
                s2rc5.gameObject.SetActive(true);
            }

            if (S1DRC1 == true)
            {
                GameObject s1drc1 = RL.transform.Find("Suit1 down right color1").gameObject;
                s1drc1.gameObject.SetActive(true);
            }
            else if (S1DRC2 == true)
            {
                GameObject s1drc2 = RL.transform.Find("Suit1 down right color2").gameObject;
                s1drc2.gameObject.SetActive(true);
            }
            else if (S1DRC3 == true)
            {
                GameObject s1drc3 = RL.transform.Find("Suit1 down right color3").gameObject;
                s1drc3.gameObject.SetActive(true);
            }
            else if (S1DRC4 == true)
            {
                GameObject s1drc4 = RL.transform.Find("Suit1 down right color4").gameObject;
                s1drc4.gameObject.SetActive(true);
            }
            else if (S1DRC5 == true)
            {
                GameObject s1drc5 = RL.transform.Find("Suit1 down right color5").gameObject;
                s1drc5.gameObject.SetActive(true);
            }
            if (S1DRCC1 == true)
            {
                GameObject s1drcc1 = RL.transform.Find("Suit1 down right cloth color1").gameObject;
                s1drcc1.gameObject.SetActive(true);
            }
            else if (S1DRCC2 == true)
            {
                GameObject s1drcc2 = RL.transform.Find("Suit1 down right cloth color2").gameObject;
                s1drcc2.gameObject.SetActive(true);
            }
            else if (S1DRCC3 == true)
            {
                GameObject s1drcc3 = RL.transform.Find("Suit1 down right cloth color3").gameObject;
                s1drcc3.gameObject.SetActive(true);
            }
            else if (S1DRCC4 == true)
            {
                GameObject s1drcc4 = RL.transform.Find("Suit1 down right cloth color4").gameObject;
                s1drcc4.gameObject.SetActive(true);
            }
            else if (S1DRCC5 == true)
            {
                GameObject s1drcc5 = RL.transform.Find("Suit1 down right cloth color5").gameObject;
                s1drcc5.gameObject.SetActive(true);
            }
            if (S1DRL1T == true)
            {
                GameObject s1drL1t = RL.transform.Find("Suit1 down right LED1 tear").gameObject;
                s1drL1t.gameObject.SetActive(true);
            }
            else if (S1DRL2T == true)
            {
                GameObject s1drL2t = RL.transform.Find("Suit1 down right LED2 tear").gameObject;
                s1drL2t.gameObject.SetActive(true);
            }
            else if (S1DRL3T == true)
            {
                GameObject s1drL3t = RL.transform.Find("Suit1 down right LED3 tear").gameObject;
                s1drL3t.gameObject.SetActive(true);
            }
            else if (S1DRL4T == true)
            {
                GameObject s1drL4t = RL.transform.Find("Suit1 down right LED4 tear").gameObject;
                s1drL4t.gameObject.SetActive(true);
            }
            else if (S1DRL5T == true)
            {
                GameObject s1drL5t = RL.transform.Find("Suit1 down right LED5 tear").gameObject;
                s1drL5t.gameObject.SetActive(true);
            }
            if (S1DRL1 == true)
            {
                GameObject s1drL1 = RL.transform.Find("Suit1 down right LED1").gameObject;
                s1drL1.gameObject.SetActive(true);
            }
            else if (S1DRL2 == true)
            {
                GameObject s1drL2 = RL.transform.Find("Suit1 down right LED2").gameObject;
                s1drL2.gameObject.SetActive(true);
            }
            else if (S1DRL3 == true)
            {
                GameObject s1drL3 = RL.transform.Find("Suit1 down right LED3").gameObject;
                s1drL3.gameObject.SetActive(true);
            }
            else if (S1DRL4 == true)
            {
                GameObject s1drL4 = RL.transform.Find("Suit1 down right LED4").gameObject;
                s1drL4.gameObject.SetActive(true);
            }
            else if (S1DRL5 == true)
            {
                GameObject s1drL5 = RL.transform.Find("Suit1 down right LED5").gameObject;
                s1drL5.gameObject.SetActive(true);
            }
            if (S3LC1 == true)
            {
                GameObject s3rc1 = RL.transform.Find("Shoes3 right color1").gameObject;
                s3rc1.gameObject.SetActive(true);
            }
            else if (S3RC2 == true)
            {
                GameObject s3rc2 = RL.transform.Find("Shoes3 right color2").gameObject;
                s3rc2.gameObject.SetActive(true);
            }
            else if (S3RC3 == true)
            {
                GameObject s3rc3 = RL.transform.Find("Shoes3 right color3").gameObject;
                s3rc3.gameObject.SetActive(true);
            }
            if (S3RC1T == true)
            {
                GameObject s3rc1t = RL.transform.Find("Shoes3 right color1 tear").gameObject;
                s3rc1t.gameObject.SetActive(true);
            }
            else if (S3RC2T == true)
            {
                GameObject s3rc2t = RL.transform.Find("Shoes3 right color2 tear").gameObject;
                s3rc2t.gameObject.SetActive(true);
            }
            else if (S3RC3T == true)
            {
                GameObject s3rc3t = RL.transform.Find("Shoes3 right color3 tear").gameObject;
                s3rc3t.gameObject.SetActive(true);
            }

            if (S2DRC1 == true)
            {
                GameObject s2drc1 = RL.transform.Find("Suit2 down right color1").gameObject;
                s2drc1.gameObject.SetActive(true);
            }
            else if (S2DRC2 == true)
            {
                GameObject s2drc2 = RL.transform.Find("Suit2 down right color2").gameObject;
                s2drc2.gameObject.SetActive(true);
            }
            else if (S2DRC3 == true)
            {
                GameObject s2drc3 = RL.transform.Find("Suit2 down right color3").gameObject;
                s2drc3.gameObject.SetActive(true);
            }
            else if (S2DRC4 == true)
            {
                GameObject s2drc4 = RL.transform.Find("Suit2 down right color4").gameObject;
                s2drc4.gameObject.SetActive(true);
            }
            else if (S2DRC5 == true)
            {
                GameObject s2drc5 = RL.transform.Find("Suit2 down right color5").gameObject;
                s2drc5.gameObject.SetActive(true);
            }
            if (S2DRCC1 == true)
            {
                GameObject s2drcc1 = RL.transform.Find("Suit2 down right cloth color1").gameObject;
                s2drcc1.gameObject.SetActive(true);
            }
            else if (S2DRCC2 == true)
            {
                GameObject s2drcc2 = RL.transform.Find("Suit2 down right cloth color2").gameObject;
                s2drcc2.gameObject.SetActive(true);
            }
            else if (S2DRCC3 == true)
            {
                GameObject s2drcc3 = RL.transform.Find("Suit2 down right cloth color3").gameObject;
                s2drcc3.gameObject.SetActive(true);
            }
            else if (S2DRCC4 == true)
            {
                GameObject s2drcc4 = RL.transform.Find("Suit2 down right cloth color4").gameObject;
                s2drcc4.gameObject.SetActive(true);
            }
            else if (S2DLCC5 == true)
            {
                GameObject s2drcc5 = RL.transform.Find("Suit2 down right cloth color5").gameObject;
                s2drcc5.gameObject.SetActive(true);
            }
            if (S4RC1 == true)
            {
                GameObject s4rc1 = RL.transform.Find("Shoes4 right color1").gameObject;
                s4rc1.gameObject.SetActive(true);
            }
            else if (S4RC2 == true)
            {
                GameObject s4rc2 = RL.transform.Find("Shoes4 right color2").gameObject;
                s4rc2.gameObject.SetActive(true);
            }
            else if (S4RC3 == true)
            {
                GameObject s4rc3 = RL.transform.Find("Shoes4 right color3").gameObject;
                s4rc3.gameObject.SetActive(true);
            }
            else if (S4RC4 == true)
            {
                GameObject s4rc4 = RL.transform.Find("Shoes4 right color4").gameObject;
                s4rc4.gameObject.SetActive(true);
            }
            if (S4RC1T == true)
            {
                GameObject s4rc1t = RL.transform.Find("Shoes4 right color1 tear").gameObject;
                s4rc1t.gameObject.SetActive(true);
            }
            else if (S4RC2T == true)
            {
                GameObject s4rc2t = RL.transform.Find("Shoes4 right color2 tear").gameObject;
                s4rc2t.gameObject.SetActive(true);
            }
            else if (S4RC3T == true)
            {
                GameObject s4rc3t = RL.transform.Find("Shoes4 right color3 tear").gameObject;
                s4rc3t.gameObject.SetActive(true);
            }
            else if (S4RC4T == true)
            {
                GameObject s4rc4t = RL.transform.Find("Shoes4 right color4 tear").gameObject;
                s4rc4t.gameObject.SetActive(true);
            }
            if (S2DRL1 == true)
            {
                GameObject s2drL1 = RL.transform.Find("Suit2 down right LED1").gameObject;
                s2drL1.gameObject.SetActive(true);
            }
            else if (S2DRL2 == true)
            {
                GameObject s2drL2 = RL.transform.Find("Suit2 down right LED2").gameObject;
                s2drL2.gameObject.SetActive(true);
            }
            else if (S2DRL3 == true)
            {
                GameObject s2drL3 = RL.transform.Find("Suit2 down right LED3").gameObject;
                s2drL3.gameObject.SetActive(true);
            }
            else if (S2DRL4 == true)
            {
                GameObject s2drL4 = RL.transform.Find("Suit2 down right LED4").gameObject;
                s2drL4.gameObject.SetActive(true);
            }
            else if (S2DRL5 == true)
            {
                GameObject s2drL5 = RL.transform.Find("Suit2 down right LED5").gameObject;
                s2drL5.gameObject.SetActive(true);
            }

            if (S3DRC1 == true)
            {
                GameObject s3drc1 = RL.transform.Find("Suit3 down right color1").gameObject;
                s3drc1.gameObject.SetActive(true);
            }
            else if (S3DRC2 == true)
            {
                GameObject s3drc2 = RL.transform.Find("Suit3 down right color2").gameObject;
                s3drc2.gameObject.SetActive(true);
            }
            else if (S3DRC3 == true)
            {
                GameObject s3drc3 = RL.transform.Find("Suit3 down right color3").gameObject;
                s3drc3.gameObject.SetActive(true);
            }
            else if (S3DRC4 == true)
            {
                GameObject s3drc4 = RL.transform.Find("Suit3 down right color4").gameObject;
                s3drc4.gameObject.SetActive(true);
            }
            else if (S3DRC5 == true)
            {
                GameObject s3drc5 = RL.transform.Find("Suit3 down right color5").gameObject;
                s3drc5.gameObject.SetActive(true);
            }
            if (S3DRCC1 == true)
            {
                GameObject s3drcc1 = RL.transform.Find("Suit3 down right cloth color1").gameObject;
                s3drcc1.gameObject.SetActive(true);
            }
            else if (S3DRCC2 == true)
            {
                GameObject s3drcc2 = RL.transform.Find("Suit3 down right cloth color2").gameObject;
                s3drcc2.gameObject.SetActive(true);
            }
            else if (S3DRCC3 == true)
            {
                GameObject s3drcc3 = RL.transform.Find("Suit3 down right cloth color3").gameObject;
                s3drcc3.gameObject.SetActive(true);
            }
            else if (S3DRCC4 == true)
            {
                GameObject s3drcc4 = RL.transform.Find("Suit3 down right cloth color4").gameObject;
                s3drcc4.gameObject.SetActive(true);
            }
            else if (S3DRCC5 == true)
            {
                GameObject s3drcc5 = RL.transform.Find("Suit3 down right cloth color5").gameObject;
                s3drcc5.gameObject.SetActive(true);
            }
            if (S5RC1 == true)
            {
                GameObject s5rc1 = RL.transform.Find("Shoes5 right color1").gameObject;
                s5rc1.gameObject.SetActive(true);
            }
            else if (S5RC2 == true)
            {
                GameObject s5rc2 = RL.transform.Find("Shoes5 right color2").gameObject;
                s5rc2.gameObject.SetActive(true);
            }
            else if (S5RC3 == true)
            {
                GameObject s5rc3 = RL.transform.Find("Shoes5 right color3").gameObject;
                s5rc3.gameObject.SetActive(true);
            }
            else if (S5RC4 == true)
            {
                GameObject s5rc4 = RL.transform.Find("Shoes5 right color4").gameObject;
                s5rc4.gameObject.SetActive(true);
            }
            else if (S5RC5 == true)
            {
                GameObject s5rc5 = RL.transform.Find("Shoes5 right color5").gameObject;
                s5rc5.gameObject.SetActive(true);
            }
            if (S5RC1T == true)
            {
                GameObject s5rc1t = RL.transform.Find("Shoes5 right color1 tear").gameObject;
                s5rc1t.gameObject.SetActive(true);
            }
            else if (S5RC2T == true)
            {
                GameObject s5rc2t = RL.transform.Find("Shoes5 right color2 tear").gameObject;
                s5rc2t.gameObject.SetActive(true);
            }
            else if (S5RC3T == true)
            {
                GameObject s5rc3t = RL.transform.Find("Shoes5 right color3 tear").gameObject;
                s5rc3t.gameObject.SetActive(true);
            }
            else if (S5RC4T == true)
            {
                GameObject s5rc4t = RL.transform.Find("Shoes5 right color4 tear").gameObject;
                s5rc4t.gameObject.SetActive(true);
            }
            else if (S5RC5T == true)
            {
                GameObject s5rc5t = RL.transform.Find("Shoes5 right color5 tear").gameObject;
                s5rc5t.gameObject.SetActive(true);
            }
            if (S3DRL1 == true)
            {
                GameObject s3drL1 = RL.transform.Find("Suit3 down right LED1").gameObject;
                s3drL1.gameObject.SetActive(true);
            }
            else if (S3DRL2 == true)
            {
                GameObject s3drL2 = RL.transform.Find("Suit3 down right LED2").gameObject;
                s3drL2.gameObject.SetActive(true);
            }
            else if (S3DRL3 == true)
            {
                GameObject s3drL3 = RL.transform.Find("Suit3 down right LED3").gameObject;
                s3drL3.gameObject.SetActive(true);
            }
            else if (S3DRL4 == true)
            {
                GameObject s3drL4 = RL.transform.Find("Suit3 down right LED4").gameObject;
                s3drL4.gameObject.SetActive(true);
            }
            else if (S3DRL5 == true)
            {
                GameObject s3drL5 = RL.transform.Find("Suit3 down right LED5").gameObject;
                s3drL5.gameObject.SetActive(true);
            }
        }
    }

    public void CreateBody()
    {
        //몸 선택
        body = Random.Range(0, 2);

        //직업 선택
        job = Random.Range(0, 100);

        if (body == 0)
        {
            GameObject body1 = transform.Find("Body1 body").gameObject;
            GameObject body1lda = transform.Find("Body1 left down arm").gameObject;
            GameObject body1ll = transform.Find("Body1 left leg").gameObject;
            GameObject body1lua = transform.Find("Body1 left up arm").gameObject;
            GameObject body1rdr = transform.Find("Body1 right down arm").gameObject;
            GameObject body1rl = transform.Find("Body1 right leg").gameObject;
            GameObject body1rua = transform.Find("Body1 right up arm").gameObject;
            GameObject footL = transform.Find("Left foot1").gameObject;
            GameObject footR = transform.Find("Right foot1").gameObject;
            body1.gameObject.SetActive(true);
            body1lda.gameObject.SetActive(true);
            body1ll.gameObject.SetActive(true);
            body1lua.gameObject.SetActive(true);
            body1rdr.gameObject.SetActive(true);
            body1rl.gameObject.SetActive(true);
            body1rua.gameObject.SetActive(true);
            footL.gameObject.SetActive(true);
            footR.gameObject.SetActive(true);
            Body1Off = true;
        }
        else
        {
            GameObject body2 = transform.Find("Body2 body").gameObject;
            GameObject body2lda = transform.Find("Body2 left down arm").gameObject;
            GameObject body2ll = transform.Find("Body2 left leg").gameObject;
            GameObject body2lua = transform.Find("Body2 left up arm").gameObject;
            GameObject body2rdr = transform.Find("Body2 right down arm").gameObject;
            GameObject body2rl = transform.Find("Body2 right leg").gameObject;
            GameObject body2rua = transform.Find("Body2 right up arm").gameObject;
            GameObject footL = transform.Find("Left foot1").gameObject;
            GameObject footR = transform.Find("Right foot1").gameObject;
            body2.gameObject.SetActive(true);
            body2lda.gameObject.SetActive(true);
            body2ll.gameObject.SetActive(true);
            body2lua.gameObject.SetActive(true);
            body2rdr.gameObject.SetActive(true);
            body2rl.gameObject.SetActive(true);
            body2rua.gameObject.SetActive(true);
            footL.gameObject.SetActive(true);
            footR.gameObject.SetActive(true);
            Body2Off = true;
        }

        if (job <= 80)
            fingerType = 0;
        else if (job > 80)
            fingerType = 1;

        if (fingerType == 0) //손가락 선택
        {
            //Debug.Log("손가락 생성");
            HandOff = true;
            finger = Random.Range(0, 3);

            if (finger == 0)
            {
                GameObject hand1r = transform.Find("Hand1 right").gameObject;
                GameObject hand1l = transform.Find("Hand1 left").gameObject;
                hand1r.gameObject.SetActive(true);
                hand1l.gameObject.SetActive(true);
            }
            else if (finger == 1)
            {
                GameObject hand2r = transform.Find("Hand2 right").gameObject;
                GameObject hand2l = transform.Find("Hand2 left").gameObject;
                hand2r.gameObject.SetActive(true);
                hand2l.gameObject.SetActive(true);
            }
            else if (finger == 2)
            {
                GameObject hand3r = transform.Find("Hand3 right").gameObject;
                GameObject hand3l = transform.Find("Hand3 left").gameObject;
                hand3r.gameObject.SetActive(true);
                hand3l.gameObject.SetActive(true);
            }
        }

        if (job <= 40) //일반인 ============================================================================================
        {
            //일반 상의
            topClothes = Random.Range(0, 2);

            //긴팔 옷
            if (topClothes == 0)
            {
                Job1Off = true;
                GameObject ct1rua = transform.Find("Clothes top1 right up arm").gameObject;
                GameObject ct1rda = transform.Find("Clothes top1 right down arm").gameObject;
                GameObject ct1lua = transform.Find("Clothes top1 left up arm").gameObject;
                GameObject ct1lda = transform.Find("Clothes top1 left down arm").gameObject;
                ct1rua.gameObject.SetActive(true);
                ct1rda.gameObject.SetActive(true);
                ct1lua.gameObject.SetActive(true);
                ct1lda.gameObject.SetActive(true);

                clothesColor = Random.Range(0, 4); //옷 색상

                if (clothesColor == 0) //1번 옷색상
                {
                    Job1_1Off = true;

                    GameObject ct1bc1 = transform.Find("Clothes top1 body color1").gameObject;
                    ct1bc1.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct1bcc1_1 = transform.Find("Clothes top1 body cloth color1-1").gameObject;
                                ct1bcc1_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct1bcc1_2 = transform.Find("Clothes top1 body cloth color1-2").gameObject;
                                ct1bcc1_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct1bcc1_3 = transform.Find("Clothes top1 body cloth color1-3").gameObject;
                                ct1bcc1_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct1bcc1_4 = transform.Find("Clothes top1 body cloth color1-4").gameObject;
                                ct1bcc1_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct1bcc1_1 = transform.Find("Clothes top1 body cloth color1-1").gameObject;
                        GameObject ct1bcc1_2 = transform.Find("Clothes top1 body cloth color1-2").gameObject;
                        GameObject ct1bcc1_3 = transform.Find("Clothes top1 body cloth color1-3").gameObject;
                        GameObject ct1bcc1_4 = transform.Find("Clothes top1 body cloth color1-4").gameObject;
                        ct1bcc1_1.gameObject.SetActive(true);
                        ct1bcc1_2.gameObject.SetActive(true);
                        ct1bcc1_3.gameObject.SetActive(true);
                        ct1bcc1_4.gameObject.SetActive(true);
                    }
                }
                else if (clothesColor == 1) //2번 옷색상
                {
                    Job1_2Off = true;
                    GameObject ct1bc2 = transform.Find("Clothes top1 body color2").gameObject;
                    ct1bc2.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct1bcc2_1 = transform.Find("Clothes top1 body cloth color2-1").gameObject;
                                ct1bcc2_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct1bcc2_2 = transform.Find("Clothes top1 body cloth color2-2").gameObject;
                                ct1bcc2_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct1bcc2_3 = transform.Find("Clothes top1 body cloth color2-3").gameObject;
                                ct1bcc2_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct1bcc2_4 = transform.Find("Clothes top1 body cloth color2-4").gameObject;
                                ct1bcc2_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct1bcc2_1 = transform.Find("Clothes top1 body cloth color2-1").gameObject;
                        GameObject ct1bcc2_2 = transform.Find("Clothes top1 body cloth color2-2").gameObject;
                        GameObject ct1bcc2_3 = transform.Find("Clothes top1 body cloth color2-3").gameObject;
                        GameObject ct1bcc2_4 = transform.Find("Clothes top1 body cloth color2-4").gameObject;
                        ct1bcc2_1.gameObject.SetActive(true);
                        ct1bcc2_2.gameObject.SetActive(true);
                        ct1bcc2_3.gameObject.SetActive(true);
                        ct1bcc2_4.gameObject.SetActive(true);
                    }
                }
                else if (clothesColor == 2) //3번 옷색상
                {
                    Job1_3Off = true;
                    GameObject ct1bc3 = transform.Find("Clothes top1 body color3").gameObject;
                    ct1bc3.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct1bcc3_1 = transform.Find("Clothes top1 body cloth color3-1").gameObject;
                                ct1bcc3_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct1bcc3_2 = transform.Find("Clothes top1 body cloth color3-2").gameObject;
                                ct1bcc3_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct1bcc3_3 = transform.Find("Clothes top1 body cloth color3-3").gameObject;
                                ct1bcc3_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct1bcc3_4 = transform.Find("Clothes top1 body cloth color3-4").gameObject;
                                ct1bcc3_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct1bcc3_1 = transform.Find("Clothes top1 body cloth color3-1").gameObject;
                        GameObject ct1bcc3_2 = transform.Find("Clothes top1 body cloth color3-2").gameObject;
                        GameObject ct1bcc3_3 = transform.Find("Clothes top1 body cloth color3-3").gameObject;
                        GameObject ct1bcc3_4 = transform.Find("Clothes top1 body cloth color3-4").gameObject;
                        ct1bcc3_1.gameObject.SetActive(true);
                        ct1bcc3_2.gameObject.SetActive(true);
                        ct1bcc3_3.gameObject.SetActive(true);
                        ct1bcc3_4.gameObject.SetActive(true);
                    }
                }
                else //4번 옷색상
                {
                    Job1_4Off = true;
                    GameObject ct1bc4 = transform.Find("Clothes top1 body color4").gameObject;
                    ct1bc4.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct1bcc4_1 = transform.Find("Clothes top1 body cloth color4-1").gameObject;
                                ct1bcc4_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct1bcc4_2 = transform.Find("Clothes top1 body cloth color4-2").gameObject;
                                ct1bcc4_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct1bcc4_3 = transform.Find("Clothes top1 body cloth color4-3").gameObject;
                                ct1bcc4_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct1bcc4_4 = transform.Find("Clothes top1 body cloth color4-4").gameObject;
                                ct1bcc4_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct1bcc4_1 = transform.Find("Clothes top1 body cloth color4-1").gameObject;
                        GameObject ct1bcc4_2 = transform.Find("Clothes top1 body cloth color4-2").gameObject;
                        GameObject ct1bcc4_3 = transform.Find("Clothes top1 body cloth color4-3").gameObject;
                        GameObject ct1bcc4_4 = transform.Find("Clothes top1 body cloth color4-4").gameObject;
                        ct1bcc4_1.gameObject.SetActive(true);
                        ct1bcc4_2.gameObject.SetActive(true);
                        ct1bcc4_3.gameObject.SetActive(true);
                        ct1bcc4_4.gameObject.SetActive(true);
                    }
                }

                LED = Random.Range(0, 2);

                if (LED == 0) //LED 활성화
                {
                    if (tearing == 0) //파손된 LED 발생
                    {
                        Job1LEDtOff = true;
                        LEDcolor = Random.Range(0, 5);

                        if (LEDcolor == 0)
                        {
                            GameObject ct1bL1t = transform.Find("Clothes top1 body LED1 tear").gameObject;
                            ct1bL1t.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject ct1bL2t = transform.Find("Clothes top1 body LED2 tear").gameObject;
                            ct1bL2t.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject ct1bL3t = transform.Find("Clothes top1 body LED3 tear").gameObject;
                            ct1bL3t.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject ct1bL4t = transform.Find("Clothes top1 body LED4 tear").gameObject;
                            ct1bL4t.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject ct1bL5t = transform.Find("Clothes top1 body LED5 tear").gameObject;
                            ct1bL5t.gameObject.SetActive(true);
                        }
                    }
                    else //멀쩡한 LED 발생
                    {
                        Job1LEDOff = true;
                        LEDcolor = Random.Range(0, 5);

                        if (LEDcolor == 0)
                        {
                            GameObject ct1bL1 = transform.Find("Clothes top1 body LED1").gameObject;
                            ct1bL1.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 1)
                        {
                            GameObject ct1bL2 = transform.Find("Clothes top1 body LED2").gameObject;
                            ct1bL2.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 2)
                        {
                            GameObject ct1bL3 = transform.Find("Clothes top1 body LED3").gameObject;
                            ct1bL3.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 3)
                        {
                            GameObject ct1bL4 = transform.Find("Clothes top1 body LED4").gameObject;
                            ct1bL4.gameObject.SetActive(true);
                        }
                        else if (LEDcolor == 4)
                        {
                            GameObject ct1bL5 = transform.Find("Clothes top1 body LED5").gameObject;
                            ct1bL5.gameObject.SetActive(true);
                        }
                    }
                }
            }

            //반팔 옷
            else
            {
                Job1SOff = true;
                GameObject ct2ra = transform.Find("Clothes top2 right arm").gameObject;
                GameObject ct2la = transform.Find("Clothes top2 left arm").gameObject;
                ct2ra.gameObject.SetActive(true);
                ct2la.gameObject.SetActive(true);

                clothesColor = Random.Range(0, 4); //옷 색상

                if (clothesColor == 0) //1번 옷색상
                {
                    Job1_1SOff = true;
                    GameObject ct2bc1 = transform.Find("Clothes top2 body color1").gameObject;
                    ct2bc1.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct2bcc1_1 = transform.Find("Clothes top2 body cloth color1-1").gameObject;
                                ct2bcc1_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct2bcc1_2 = transform.Find("Clothes top2 body cloth color1-2").gameObject;
                                ct2bcc1_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct2bcc1_3 = transform.Find("Clothes top2 body cloth color1-3").gameObject;
                                ct2bcc1_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct2bcc1_4 = transform.Find("Clothes top2 body cloth color1-4").gameObject;
                                ct2bcc1_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct2bcc1_1 = transform.Find("Clothes top2 body cloth color1-1").gameObject;
                        GameObject ct2bcc1_2 = transform.Find("Clothes top2 body cloth color1-2").gameObject;
                        GameObject ct2bcc1_3 = transform.Find("Clothes top2 body cloth color1-3").gameObject;
                        GameObject ct2bcc1_4 = transform.Find("Clothes top2 body cloth color1-4").gameObject;
                        ct2bcc1_1.gameObject.SetActive(true);
                        ct2bcc1_2.gameObject.SetActive(true);
                        ct2bcc1_3.gameObject.SetActive(true);
                        ct2bcc1_4.gameObject.SetActive(true);
                    }
                }
                else if (clothesColor == 1) //2번 옷색상
                {
                    Job1_2SOff = true;
                    GameObject ct2bc2 = transform.Find("Clothes top2 body color2").gameObject;
                    ct2bc2.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct2bcc2_1 = transform.Find("Clothes top2 body cloth color2-1").gameObject;
                                ct2bcc2_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct2bcc2_2 = transform.Find("Clothes top2 body cloth color2-2").gameObject;
                                ct2bcc2_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct2bcc2_3 = transform.Find("Clothes top2 body cloth color2-3").gameObject;
                                ct2bcc2_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct2bcc2_4 = transform.Find("Clothes top2 body cloth color2-4").gameObject;
                                ct2bcc2_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct2bcc2_1 = transform.Find("Clothes top2 body cloth color2-1").gameObject;
                        GameObject ct2bcc2_2 = transform.Find("Clothes top2 body cloth color2-2").gameObject;
                        GameObject ct2bcc2_3 = transform.Find("Clothes top2 body cloth color2-3").gameObject;
                        GameObject ct2bcc2_4 = transform.Find("Clothes top2 body cloth color2-4").gameObject;
                        ct2bcc2_1.gameObject.SetActive(true);
                        ct2bcc2_2.gameObject.SetActive(true);
                        ct2bcc2_3.gameObject.SetActive(true);
                        ct2bcc2_4.gameObject.SetActive(true);
                    }
                }
                else if (clothesColor == 2) //3번 옷색상
                {
                    Job1_3SOff = true;
                    GameObject ct2bc3 = transform.Find("Clothes top2 body color3").gameObject;
                    ct2bc3.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct2bcc3_1 = transform.Find("Clothes top2 body cloth color3-1").gameObject;
                                ct2bcc3_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct2bcc3_2 = transform.Find("Clothes top2 body cloth color3-2").gameObject;
                                ct2bcc3_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct2bcc3_3 = transform.Find("Clothes top2 body cloth color3-3").gameObject;
                                ct2bcc3_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct2bcc3_4 = transform.Find("Clothes top2 body cloth color3-4").gameObject;
                                ct2bcc3_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct2bcc3_1 = transform.Find("Clothes top2 body cloth color3-1").gameObject;
                        GameObject ct2bcc3_2 = transform.Find("Clothes top2 body cloth color3-2").gameObject;
                        GameObject ct2bcc3_3 = transform.Find("Clothes top2 body cloth color3-3").gameObject;
                        GameObject ct2bcc3_4 = transform.Find("Clothes top2 body cloth color3-4").gameObject;
                        ct2bcc3_1.gameObject.SetActive(true);
                        ct2bcc3_2.gameObject.SetActive(true);
                        ct2bcc3_3.gameObject.SetActive(true);
                        ct2bcc3_4.gameObject.SetActive(true);
                    }
                }
                else //4번 옷색상
                {
                    Job1_4SOff = true;
                    GameObject ct2bc4 = transform.Find("Clothes top2 body color4").gameObject;
                    ct2bc4.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //옷 찢어짐

                    if (tearing == 0)
                    {
                        tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                        for (i = 0; i <= tearAmounts; i++)
                        {
                            if (i == 1)
                            {
                                GameObject ct2bcc4_1 = transform.Find("Clothes top2 body cloth color4-1").gameObject;
                                ct2bcc4_1.gameObject.SetActive(true);
                            }
                            if (i == 2)
                            {
                                GameObject ct2bcc4_2 = transform.Find("Clothes top2 body cloth color4-2").gameObject;
                                ct2bcc4_2.gameObject.SetActive(true);
                            }
                            if (i == 3)
                            {
                                GameObject ct2bcc4_3 = transform.Find("Clothes top2 body cloth color4-3").gameObject;
                                ct2bcc4_3.gameObject.SetActive(true);
                            }
                            if (i == 4)
                            {
                                GameObject ct2bcc4_4 = transform.Find("Clothes top2 body cloth color4-4").gameObject;
                                ct2bcc4_4.gameObject.SetActive(true);
                            }
                        }
                    }
                    else //옷 안 찢어짐
                    {
                        GameObject ct2bcc4_1 = transform.Find("Clothes top2 body cloth color4-1").gameObject;
                        GameObject ct2bcc4_2 = transform.Find("Clothes top2 body cloth color4-2").gameObject;
                        GameObject ct2bcc4_3 = transform.Find("Clothes top2 body cloth color4-3").gameObject;
                        GameObject ct2bcc4_4 = transform.Find("Clothes top2 body cloth color4-4").gameObject;
                        ct2bcc4_1.gameObject.SetActive(true);
                        ct2bcc4_2.gameObject.SetActive(true);
                        ct2bcc4_3.gameObject.SetActive(true);
                        ct2bcc4_4.gameObject.SetActive(true);
                    }
                }

                LED = Random.Range(0, 2); //LED 활성화

                if (LED == 0)
                {
                    Job1SLEDOff = true;
                    LEDcolor = Random.Range(0, 5);

                    if (LEDcolor == 0)
                    {
                        GameObject ct2bL1 = transform.Find("Clothes top2 body LED1").gameObject;
                        ct2bL1.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 1)
                    {
                        GameObject ct2bL2 = transform.Find("Clothes top2 body LED2").gameObject;
                        ct2bL2.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 2)
                    {
                        GameObject ct2bL3 = transform.Find("Clothes top2 body LED3").gameObject;
                        ct2bL3.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 3)
                    {
                        GameObject ct2bL4 = transform.Find("Clothes top2 body LED4").gameObject;
                        ct2bL4.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 4)
                    {
                        GameObject ct2bL5 = transform.Find("Clothes top2 body LED5").gameObject;
                        ct2bL5.gameObject.SetActive(true);
                    }
                }
            }

            //일반 하의
            pants = Random.Range(0, 2);

            if (pants == 0) //긴 바지
            {
                pantsColor = Random.Range(0, 5);

                if (pantsColor == 0) //1번 바지 색상
                {
                    Job1_1POff = true;
                    GameObject p1rc1 = transform.Find("Pants1 right color1").gameObject;
                    GameObject p1lc1 = transform.Find("Pants1 left color1").gameObject;
                    p1rc1.gameObject.SetActive(true);
                    p1lc1.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1rcc1 = transform.Find("Pants1 right cloth color1").gameObject;
                        p1rcc1.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1lcc1 = transform.Find("Pants1 left cloth color1").gameObject;
                        p1lcc1.gameObject.SetActive(true);
                    }
                }
                else if (pantsColor == 1) //2번 바지 색상
                {
                    Job1_2POff = true;
                    GameObject p1rc2 = transform.Find("Pants1 right color2").gameObject;
                    GameObject p1lc2 = transform.Find("Pants1 left color2").gameObject;
                    p1rc2.gameObject.SetActive(true);
                    p1lc2.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1rcc2 = transform.Find("Pants1 right cloth color2").gameObject;
                        p1rcc2.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1lcc2 = transform.Find("Pants1 left cloth color2").gameObject;
                        p1lcc2.gameObject.SetActive(true);
                    }
                }
                else if (pantsColor == 2) //3번 바지 색상
                {
                    Job1_3POff = true;
                    GameObject p1rc3 = transform.Find("Pants1 right color3").gameObject;
                    GameObject p1lc3 = transform.Find("Pants1 left color3").gameObject;
                    p1rc3.gameObject.SetActive(true);
                    p1lc3.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1rcc3 = transform.Find("Pants1 right cloth color3").gameObject;
                        p1rcc3.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1lcc3 = transform.Find("Pants1 left cloth color3").gameObject;
                        p1lcc3.gameObject.SetActive(true);
                    }
                }
                else //4번 바지 색상
                {
                    Job1_4POff = true;
                    GameObject p1rc4 = transform.Find("Pants1 right color4").gameObject;
                    GameObject p1lc4 = transform.Find("Pants1 left color4").gameObject;
                    p1rc4.gameObject.SetActive(true);
                    p1lc4.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1rcc4 = transform.Find("Pants1 right cloth color4").gameObject;
                        p1rcc4.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p1lcc4 = transform.Find("Pants1 left cloth color4").gameObject;
                        p1lcc4.gameObject.SetActive(true);
                    }
                }

                LED = Random.Range(0, 2); //LED 활성화

                if (LED == 0)
                {
                    Job1PLEDOff = true;
                    LEDcolor = Random.Range(0, 5);

                    if (LEDcolor == 0)
                    {
                        GameObject p1rL1 = transform.Find("Pants1 right LED1").gameObject;
                        p1rL1.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 1)
                    {
                        GameObject p1rL2 = transform.Find("Pants1 right LED2").gameObject;
                        p1rL2.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 2)
                    {
                        GameObject p1rL3 = transform.Find("Pants1 right LED3").gameObject;
                        p1rL3.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 3)
                    {
                        GameObject p1rL4 = transform.Find("Pants1 right LED4").gameObject;
                        p1rL4.gameObject.SetActive(true);
                    }
                    else if (LEDcolor == 4)
                    {
                        GameObject p1rL5 = transform.Find("Pants1 right LED5").gameObject;
                        p1rL5.gameObject.SetActive(true);
                    }
                }
            }

            else //반 바지
            {
                pantsColor = Random.Range(0, 5);

                if (pantsColor == 0) //1번 바지 색상
                {
                    Job1_1SPOff = true;
                    GameObject p2rc1 = transform.Find("Pants2 right color1").gameObject;
                    GameObject p2lc1 = transform.Find("Pants2 left color1").gameObject;
                    p2rc1.gameObject.SetActive(true);
                    p2lc1.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2rcc1 = transform.Find("Pants2 right cloth color1").gameObject;
                        p2rcc1.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2lcc1 = transform.Find("Pants2 left cloth color1").gameObject;
                        p2lcc1.gameObject.SetActive(true);
                    }
                }
                else if (pantsColor == 1) //2번 바지 색상
                {
                    Job1_2SPOff = true;
                    GameObject p2rc2 = transform.Find("Pants2 right color2").gameObject;
                    GameObject p2lc2 = transform.Find("Pants2 left color2").gameObject;
                    p2rc2.gameObject.SetActive(true);
                    p2lc2.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2rcc2 = transform.Find("Pants2 right cloth color2").gameObject;
                        p2rcc2.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2lcc2 = transform.Find("Pants2 left cloth color2").gameObject;
                        p2lcc2.gameObject.SetActive(true);
                    }
                }
                else if (pantsColor == 2) //3번 바지 색상
                {
                    Job1_3SPOff = true;
                    GameObject p2rc3 = transform.Find("Pants2 right color3").gameObject;
                    GameObject p2lc3 = transform.Find("Pants2 left color3").gameObject;
                    p2rc3.gameObject.SetActive(true);
                    p2lc3.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2rcc3 = transform.Find("Pants2 right cloth color3").gameObject;
                        p2rcc3.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2lcc3 = transform.Find("Pants2 left cloth color3").gameObject;
                        p2lcc3.gameObject.SetActive(true);
                    }
                }
                else //4번 바지 색상
                {
                    Job1_4SPOff = true;
                    GameObject p2rc4 = transform.Find("Pants2 right color4").gameObject;
                    GameObject p2lc4 = transform.Find("Pants2 left color4").gameObject;
                    p2rc4.gameObject.SetActive(true);
                    p2lc4.gameObject.SetActive(true);

                    tearing = Random.Range(0, 2);  //오른쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2rcc4 = transform.Find("Pants2 right cloth color4").gameObject;
                        p2rcc4.gameObject.SetActive(true);
                    }

                    tearing = Random.Range(0, 2);  //왼쪽 바지 찢어짐

                    if (tearing == 0)
                    {
                        GameObject p2lcc4 = transform.Find("Pants2 left cloth color4").gameObject;
                        p2lcc4.gameObject.SetActive(true);
                    }
                }

                LEDR = Random.Range(0, 2); //오른쪽 LED 활성화

                if (LEDR == 0)
                {
                    Job1SPLEDROff = true;
                    LEDcolorR = Random.Range(0, 5);

                    if (LEDcolorR == 0)
                    {
                        GameObject p2rL1 = transform.Find("Pants2 right LED1").gameObject;
                        p2rL1.gameObject.SetActive(true);
                    }
                    else if (LEDcolorR == 1)
                    {
                        GameObject p2rL2 = transform.Find("Pants2 right LED2").gameObject;
                        p2rL2.gameObject.SetActive(true);
                    }
                    else if (LEDcolorR == 2)
                    {
                        GameObject p2rL3 = transform.Find("Pants2 right LED3").gameObject;
                        p2rL3.gameObject.SetActive(true);
                    }
                    else if (LEDcolorR == 3)
                    {
                        GameObject p2rL4 = transform.Find("Pants2 right LED4").gameObject;
                        p2rL4.gameObject.SetActive(true);
                    }
                    else if (LEDcolorR == 4)
                    {
                        GameObject p2rL5 = transform.Find("Pants2 right LED5").gameObject;
                        p2rL5.gameObject.SetActive(true);
                    }
                }

                LEDL = Random.Range(0, 2); //왼쪽 LED 활성화

                if (LEDL == 0)
                {
                    Job1SPLEDLOff = true;
                    LEDcolorL = Random.Range(0, 6);

                    if (LEDcolorL == 0)
                    {
                        GameObject p2lL1 = transform.Find("Pants2 left LED1").gameObject;
                        p2lL1.gameObject.SetActive(true);
                    }
                    else if (LEDcolorL == 1)
                    {
                        GameObject p2lL2 = transform.Find("Pants2 left LED2").gameObject;
                        p2lL2.gameObject.SetActive(true);
                    }
                    else if (LEDcolorL == 2)
                    {
                        GameObject p2lL3 = transform.Find("Pants2 left LED3").gameObject;
                        p2lL3.gameObject.SetActive(true);
                    }
                    else if (LEDcolorL == 3)
                    {
                        GameObject p2lL4 = transform.Find("Pants2 left LED4").gameObject;
                        p2lL4.gameObject.SetActive(true);
                    }
                    else if (LEDcolorL == 4)
                    {
                        GameObject p2lL5 = transform.Find("Pants2 left LED5").gameObject;
                        p2lL5.gameObject.SetActive(true);
                    }
                }
            }

            //일반 신발
            shoes = Random.Range(0, 2);

            if (shoes == 0) //신발
            {
                shoesColor = Random.Range(0, 5);

                if (shoesColor == 0) //1번 신발
                {
                    Job1_1ShoesOff = true;
                    tearingSR = Random.Range(0, 3); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s1rc1 = transform.Find("Shoes1 right color1").gameObject;
                        s1rc1.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s1rc1t = transform.Find("Shoes1 right color1 tear").gameObject;
                        s1rc1t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 3); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s1lc1 = transform.Find("Shoes1 left color1").gameObject;
                        s1lc1.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s1lc1t = transform.Find("Shoes1 left color1 tear").gameObject;
                        s1lc1t.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 1) //2번 신발
                {
                    Job1_2ShoesOff = true;
                    tearingSR = Random.Range(0, 3); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s1rc2 = transform.Find("Shoes1 right color2").gameObject;
                        s1rc2.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s1rc2t = transform.Find("Shoes1 right color2 tear").gameObject;
                        s1rc2t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 3); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s1lc2 = transform.Find("Shoes1 left color2").gameObject;
                        s1lc2.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s1lc2t = transform.Find("Shoes1 left color2 tear").gameObject;
                        s1lc2t.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 2) //3번 신발
                {
                    Job1_3ShoesOff = true;
                    tearingSR = Random.Range(0, 3); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s1rc3 = transform.Find("Shoes1 right color3").gameObject;
                        s1rc3.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s1rc3t = transform.Find("Shoes1 right color3 tear").gameObject;
                        s1rc3t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 3); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s1lc3 = transform.Find("Shoes1 left color3").gameObject;
                        s1lc3.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s1lc3t = transform.Find("Shoes1 left color3 tear").gameObject;
                        s1lc3t.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 3) //4번 신발
                {
                    Job1_4ShoesOff = true;
                    tearingSR = Random.Range(0, 3); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s1rc4 = transform.Find("Shoes1 right color4").gameObject;
                        s1rc4.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s1rc4t = transform.Find("Shoes1 right color4 tear").gameObject;
                        s1rc4t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 3); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s1lc4 = transform.Find("Shoes1 left color4").gameObject;
                        s1lc4.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s1lc4t = transform.Find("Shoes1 left color4 tear").gameObject;
                        s1lc4t.gameObject.SetActive(true);
                    }
                }
                else //5번 신발
                {
                    Job1_5ShoesOff = true;
                    tearingSR = Random.Range(0, 3); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s1rc5 = transform.Find("Shoes1 right color5").gameObject;
                        s1rc5.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s1rc5t = transform.Find("Shoes1 right color5 tear").gameObject;
                        s1rc5t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 3); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s1lc5 = transform.Find("Shoes1 left color5").gameObject;
                        s1lc5.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s1lc5t = transform.Find("Shoes1 left color5 tear").gameObject;
                        s1lc5t.gameObject.SetActive(true);
                    }
                }
            }
            else //샌들
            {
                shoesColor = Random.Range(0, 5);

                if (shoesColor == 0) //1번 신발
                {
                    Job1_1SShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s2rc1 = transform.Find("Shoes2 right color1").gameObject;
                        s2rc1.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s2lc1 = transform.Find("Shoes2 left color1").gameObject;
                        s2lc1.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 1) //2번 신발
                {
                    Job1_2SShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s2rc2 = transform.Find("Shoes2 right color2").gameObject;
                        s2rc2.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s2lc2 = transform.Find("Shoes2 left color2").gameObject;
                        s2lc2.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 2) //3번 신발
                {
                    Job1_3SShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s2rc3 = transform.Find("Shoes2 right color3").gameObject;
                        s2rc3.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s2lc3 = transform.Find("Shoes2 left color3").gameObject;
                        s2lc3.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 3) //4번 신발
                {
                    Job1_4SShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s2rc4 = transform.Find("Shoes2 right color4").gameObject;
                        s2rc4.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s2lc4 = transform.Find("Shoes2 left color4").gameObject;
                        s2lc4.gameObject.SetActive(true);
                    }
                }
                else //5번 신발
                {
                    Job1_5SShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s2rc5 = transform.Find("Shoes2 right color5").gameObject;
                        s2rc5.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s2lc5 = transform.Find("Shoes2 left color5").gameObject;
                        s2lc5.gameObject.SetActive(true);
                    }
                }
            }
        }

        //회사원 ============================================================================================
        else if (job > 40 && job <= 60)
        {
            suit1in = true;
            suitsColor = Random.Range(0, 5);

            if (suitsColor == 0) //1번 정장
            {
                Job2_1Off = true;
                GameObject s1tbc1 = transform.Find("Suit1 top body color1").gameObject;
                GameObject s1truac1 = transform.Find("Suit1 top right up arm color1").gameObject;
                GameObject s1trdac1 = transform.Find("Suit1 top right down arm color1").gameObject;
                GameObject s1tluac1 = transform.Find("Suit1 top left up arm color1").gameObject;
                GameObject s1tldac1 = transform.Find("Suit1 top left down arm color1").gameObject;
                GameObject s1drc1 = transform.Find("Suit1 down right color1").gameObject;
                GameObject s1dlc1 = transform.Find("Suit1 down left color1").gameObject;
                s1tbc1.gameObject.SetActive(true);
                s1truac1.gameObject.SetActive(true);
                s1trdac1.gameObject.SetActive(true);
                s1tluac1.gameObject.SetActive(true);
                s1tldac1.gameObject.SetActive(true);
                s1drc1.gameObject.SetActive(true);
                s1dlc1.gameObject.SetActive(true);

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s1tbcc1_1 = transform.Find("Suit1 top body cloth color1-1").gameObject;
                            s1tbcc1_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s1tbcc1_2 = transform.Find("Suit1 top body cloth color1-2").gameObject;
                            s1tbcc1_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s1tbcc1_3 = transform.Find("Suit1 top body cloth color1-3").gameObject;
                            s1tbcc1_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s1drcc1 = transform.Find("Suit1 down right cloth color1").gameObject;
                            s1drcc1.gameObject.SetActive(true);
                            DownRclothTear = 1;
                        }
                        if (i == 5)
                        {
                            GameObject s1dlcc1 = transform.Find("Suit1 down left cloth color1").gameObject;
                            s1dlcc1.gameObject.SetActive(true);
                            DownLclothTear = 1;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s1tbcc1_1 = transform.Find("Suit1 top body cloth color1-1").gameObject;
                    GameObject s1tbcc1_2 = transform.Find("Suit1 top body cloth color1-2").gameObject;
                    GameObject s1tbcc1_3 = transform.Find("Suit1 top body cloth color1-3").gameObject;
                    GameObject s1drcc1 = transform.Find("Suit1 down right cloth color1").gameObject;
                    GameObject s1dlcc1 = transform.Find("Suit1 down left cloth color1").gameObject;
                    s1tbcc1_1.gameObject.SetActive(true);
                    s1tbcc1_2.gameObject.SetActive(true);
                    s1tbcc1_3.gameObject.SetActive(true);
                    s1drcc1.gameObject.SetActive(true);
                    s1dlcc1.gameObject.SetActive(true);
                    clothTear = 1;
                }
            }
            else if (suitsColor == 1) //2번 정장
            {
                Job2_2Off = true;
                GameObject s1tbc2 = transform.Find("Suit1 top body color2").gameObject;
                GameObject s1truac2 = transform.Find("Suit1 top right up arm color2").gameObject;
                GameObject s1trdac2 = transform.Find("Suit1 top right down arm color2").gameObject;
                GameObject s1tluac2 = transform.Find("Suit1 top left up arm color2").gameObject;
                GameObject s1tldac2 = transform.Find("Suit1 top left down arm color2").gameObject;
                GameObject s1drc2 = transform.Find("Suit1 down right color2").gameObject;
                GameObject s1dlc2 = transform.Find("Suit1 down left color2").gameObject;
                s1tbc2.gameObject.SetActive(true);
                s1truac2.gameObject.SetActive(true);
                s1trdac2.gameObject.SetActive(true);
                s1tluac2.gameObject.SetActive(true);
                s1tldac2.gameObject.SetActive(true);
                s1drc2.gameObject.SetActive(true);
                s1dlc2.gameObject.SetActive(true);

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s1tbcc2_1 = transform.Find("Suit1 top body cloth color2-1").gameObject;
                            s1tbcc2_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s1tbcc2_2 = transform.Find("Suit1 top body cloth color2-2").gameObject;
                            s1tbcc2_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s1tbcc2_3 = transform.Find("Suit1 top body cloth color2-3").gameObject;
                            s1tbcc2_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s1drcc2 = transform.Find("Suit1 down right cloth color2").gameObject;
                            s1drcc2.gameObject.SetActive(true);
                            DownRclothTear = 2;
                        }
                        if (i == 5)
                        {
                            GameObject s1dlcc2 = transform.Find("Suit1 down left cloth color2").gameObject;
                            s1dlcc2.gameObject.SetActive(true);
                            DownLclothTear = 2;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s1tbcc2_1 = transform.Find("Suit1 top body cloth color2-1").gameObject;
                    GameObject s1tbcc2_2 = transform.Find("Suit1 top body cloth color2-2").gameObject;
                    GameObject s1tbcc2_3 = transform.Find("Suit1 top body cloth color2-3").gameObject;
                    GameObject s1drcc2 = transform.Find("Suit1 down right cloth color2").gameObject;
                    GameObject s1dlcc2 = transform.Find("Suit1 down left cloth color2").gameObject;
                    s1tbcc2_1.gameObject.SetActive(true);
                    s1tbcc2_2.gameObject.SetActive(true);
                    s1tbcc2_3.gameObject.SetActive(true);
                    s1drcc2.gameObject.SetActive(true);
                    s1dlcc2.gameObject.SetActive(true);
                    clothTear = 2;
                }
            }
            else if (suitsColor == 2) //3번 정장
            {
                Job2_3Off = true;
                GameObject s1tbc3 = transform.Find("Suit1 top body color3").gameObject;
                GameObject s1truac3 = transform.Find("Suit1 top right up arm color3").gameObject;
                GameObject s1trdac3 = transform.Find("Suit1 top right down arm color3").gameObject;
                GameObject s1tluac3 = transform.Find("Suit1 top left up arm color3").gameObject;
                GameObject s1tldac3 = transform.Find("Suit1 top left down arm color3").gameObject;
                GameObject s1drc3 = transform.Find("Suit1 down right color3").gameObject;
                GameObject s1dlc3 = transform.Find("Suit1 down left color3").gameObject;
                s1tbc3.gameObject.SetActive(true);
                s1truac3.gameObject.SetActive(true);
                s1trdac3.gameObject.SetActive(true);
                s1tluac3.gameObject.SetActive(true);
                s1tldac3.gameObject.SetActive(true);
                s1drc3.gameObject.SetActive(true);
                s1dlc3.gameObject.SetActive(true);

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s1tbcc3_1 = transform.Find("Suit1 top body cloth color3-1").gameObject;
                            s1tbcc3_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s1tbcc3_2 = transform.Find("Suit1 top body cloth color3-2").gameObject;
                            s1tbcc3_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s1tbcc3_3 = transform.Find("Suit1 top body cloth color3-3").gameObject;
                            s1tbcc3_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s1drcc3 = transform.Find("Suit1 down right cloth color3").gameObject;
                            s1drcc3.gameObject.SetActive(true);
                            DownRclothTear = 3;
                        }
                        if (i == 5)
                        {
                            GameObject s1dlcc3 = transform.Find("Suit1 down left cloth color3").gameObject;
                            s1dlcc3.gameObject.SetActive(true);
                            DownLclothTear = 3;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s1tbcc3_1 = transform.Find("Suit1 top body cloth color3-1").gameObject;
                    GameObject s1tbcc3_2 = transform.Find("Suit1 top body cloth color3-2").gameObject;
                    GameObject s1tbcc3_3 = transform.Find("Suit1 top body cloth color3-3").gameObject;
                    GameObject s1drcc3 = transform.Find("Suit1 down right cloth color3").gameObject;
                    GameObject s1dlcc3 = transform.Find("Suit1 down left cloth color3").gameObject;
                    s1tbcc3_1.gameObject.SetActive(true);
                    s1tbcc3_2.gameObject.SetActive(true);
                    s1tbcc3_3.gameObject.SetActive(true);
                    s1drcc3.gameObject.SetActive(true);
                    s1dlcc3.gameObject.SetActive(true);
                    clothTear = 3;
                }
            }
            else if (suitsColor == 3) //4번 정장
            {
                Job2_4Off = true;
                GameObject s1tbc4 = transform.Find("Suit1 top body color4").gameObject;
                GameObject s1truac4 = transform.Find("Suit1 top right up arm color4").gameObject;
                GameObject s1trdac4 = transform.Find("Suit1 top right down arm color4").gameObject;
                GameObject s1tluac4 = transform.Find("Suit1 top left up arm color4").gameObject;
                GameObject s1tldac4 = transform.Find("Suit1 top left down arm color4").gameObject;
                GameObject s1drc4 = transform.Find("Suit1 down right color4").gameObject;
                GameObject s1dlc4 = transform.Find("Suit1 down left color4").gameObject;
                s1tbc4.gameObject.SetActive(true);
                s1truac4.gameObject.SetActive(true);
                s1trdac4.gameObject.SetActive(true);
                s1tluac4.gameObject.SetActive(true);
                s1tldac4.gameObject.SetActive(true);
                s1drc4.gameObject.SetActive(true);
                s1dlc4.gameObject.SetActive(true);

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s1tbcc4_1 = transform.Find("Suit1 top body cloth color4-1").gameObject;
                            s1tbcc4_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s1tbcc4_2 = transform.Find("Suit1 top body cloth color4-2").gameObject;
                            s1tbcc4_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s1tbcc4_3 = transform.Find("Suit1 top body cloth color4-3").gameObject;
                            s1tbcc4_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s1drcc4 = transform.Find("Suit1 down right cloth color4").gameObject;
                            s1drcc4.gameObject.SetActive(true);
                            DownRclothTear = 4;
                        }
                        if (i == 5)
                        {
                            GameObject s1dlcc4 = transform.Find("Suit1 down left cloth color4").gameObject;
                            s1dlcc4.gameObject.SetActive(true);
                            DownLclothTear = 4;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s1tbcc4_1 = transform.Find("Suit1 top body cloth color4-1").gameObject;
                    GameObject s1tbcc4_2 = transform.Find("Suit1 top body cloth color4-2").gameObject;
                    GameObject s1tbcc4_3 = transform.Find("Suit1 top body cloth color4-3").gameObject;
                    GameObject s1drcc4 = transform.Find("Suit1 down right cloth color4").gameObject;
                    GameObject s1dlcc4 = transform.Find("Suit1 down left cloth color4").gameObject;
                    s1tbcc4_1.gameObject.SetActive(true);
                    s1tbcc4_2.gameObject.SetActive(true);
                    s1tbcc4_3.gameObject.SetActive(true);
                    s1drcc4.gameObject.SetActive(true);
                    s1dlcc4.gameObject.SetActive(true);
                    clothTear = 4;
                }
            }
            else //5번 정장
            {
                Job2_5Off = true;
                GameObject s1tbc5 = transform.Find("Suit1 top body color5").gameObject;
                GameObject s1truac5 = transform.Find("Suit1 top right up arm color5").gameObject;
                GameObject s1trdac5 = transform.Find("Suit1 top right down arm color5").gameObject;
                GameObject s1tluac5 = transform.Find("Suit1 top left up arm color5").gameObject;
                GameObject s1tldac5 = transform.Find("Suit1 top left down arm color5").gameObject;
                GameObject s1drc5 = transform.Find("Suit1 down right color5").gameObject;
                GameObject s1dlc5 = transform.Find("Suit1 down left color5").gameObject;
                s1tbc5.gameObject.SetActive(true);
                s1truac5.gameObject.SetActive(true);
                s1trdac5.gameObject.SetActive(true);
                s1tluac5.gameObject.SetActive(true);
                s1tldac5.gameObject.SetActive(true);
                s1drc5.gameObject.SetActive(true);
                s1dlc5.gameObject.SetActive(true);

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 5); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s1tbcc5_1 = transform.Find("Suit1 top body cloth color5-1").gameObject;
                            s1tbcc5_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s1tbcc5_2 = transform.Find("Suit1 top body cloth color5-2").gameObject;
                            s1tbcc5_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s1tbcc5_3 = transform.Find("Suit1 top body cloth color5-3").gameObject;
                            s1tbcc5_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s1drcc5 = transform.Find("Suit1 down right cloth color5").gameObject;
                            s1drcc5.gameObject.SetActive(true);
                            DownRclothTear = 5;
                        }
                        if (i == 5)
                        {
                            GameObject s1dlcc5 = transform.Find("Suit1 down left cloth color5").gameObject;
                            s1dlcc5.gameObject.SetActive(true);
                            DownLclothTear = 5;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s1tbcc5_1 = transform.Find("Suit1 top body cloth color5-1").gameObject;
                    GameObject s1tbcc5_2 = transform.Find("Suit1 top body cloth color5-2").gameObject;
                    GameObject s1tbcc5_3 = transform.Find("Suit1 top body cloth color5-3").gameObject;
                    GameObject s1drcc5 = transform.Find("Suit1 down right cloth color5").gameObject;
                    GameObject s1dlcc5 = transform.Find("Suit1 down left cloth color5").gameObject;
                    s1tbcc5_1.gameObject.SetActive(true);
                    s1tbcc5_2.gameObject.SetActive(true);
                    s1tbcc5_3.gameObject.SetActive(true);
                    s1drcc5.gameObject.SetActive(true);
                    s1dlcc5.gameObject.SetActive(true);
                    clothTear = 5;
                }
            }

            LED = Random.Range(0, 2); //상의 LED 활성화

            if (LED == 0 && suit1in == true)
            {
                Job2LEDOff = true;
                LEDcolor = Random.Range(0, 5);

                if (LEDcolor == 0)
                {
                    GameObject s1tbL1 = transform.Find("Suit1 top body LED1").gameObject;
                    s1tbL1.gameObject.SetActive(true);
                }
                else if (LEDcolor == 1)
                {
                    GameObject s1tbL2 = transform.Find("Suit1 top body LED2").gameObject;
                    s1tbL2.gameObject.SetActive(true);
                }
                else if (LEDcolor == 2)
                {
                    GameObject s1tbL3 = transform.Find("Suit1 top body LED3").gameObject;
                    s1tbL3.gameObject.SetActive(true);
                }
                else if (LEDcolor == 3)
                {
                    GameObject s1tbL4 = transform.Find("Suit1 top body LED4").gameObject;
                    s1tbL4.gameObject.SetActive(true);
                }
                else if (LEDcolor == 4)
                {
                    GameObject s1tbL5 = transform.Find("Suit1 top body LED5").gameObject;
                    s1tbL5.gameObject.SetActive(true);
                }
            }

            LED = Random.Range(0, 2);

            if (LED == 0 && suit1in == true) //하의 LED 활성화
            {
                if (tearing == 0) //파손된 LED 발생
                {
                    Job2LEDLegtOff = true;
                    LEDcolorleg = Random.Range(0, 5);
                    LEDBetweenR = Random.Range(0, 2);
                    LEDBetweenL = Random.Range(0, 2);

                    if (LEDcolorleg == 0)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL1t = transform.Find("Suit1 down right LED1 tear").gameObject;
                            s1drL1t.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL1t = transform.Find("Suit1 down left LED1 tear").gameObject;
                            s1dlL1t.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 1)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL2t = transform.Find("Suit1 down right LED2 tear").gameObject;
                            s1drL2t.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL2t = transform.Find("Suit1 down left LED2 tear").gameObject;
                            s1dlL2t.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 2)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL3t = transform.Find("Suit1 down right LED3 tear").gameObject;
                            s1drL3t.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL3t = transform.Find("Suit1 down left LED3 tear").gameObject;
                            s1dlL3t.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 3)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL4t = transform.Find("Suit1 down right LED4 tear").gameObject;
                            s1drL4t.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL4t = transform.Find("Suit1 down left LED4 tear").gameObject;
                            s1dlL4t.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 4)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL5t = transform.Find("Suit1 down right LED5 tear").gameObject;
                            s1drL5t.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL5t = transform.Find("Suit1 down left LED5 tear").gameObject;
                            s1dlL5t.gameObject.SetActive(true);
                        }
                    }
                }
                else //멀쩡한 LED 발생
                {
                    Job2LEDLegOff = true;
                    LEDcolorleg = Random.Range(0, 5);
                    LEDBetweenR = Random.Range(0, 2);
                    LEDBetweenL = Random.Range(0, 2);

                    if (LEDcolorleg == 0)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL1 = transform.Find("Suit1 down right LED1").gameObject;
                            s1drL1.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL1 = transform.Find("Suit1 down left LED1").gameObject;
                            s1dlL1.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 1)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL2 = transform.Find("Suit1 down right LED2").gameObject;
                            s1drL2.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL2 = transform.Find("Suit1 down left LED2").gameObject;
                            s1dlL2.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 2)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL3 = transform.Find("Suit1 down right LED3").gameObject;
                            s1drL3.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL3 = transform.Find("Suit1 down left LED3").gameObject;
                            s1dlL3.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 3)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL4 = transform.Find("Suit1 down right LED4").gameObject;
                            s1drL4.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL4 = transform.Find("Suit1 down left LED4").gameObject;
                            s1dlL4.gameObject.SetActive(true);
                        }
                    }
                    else if (LEDcolorleg == 4)
                    {
                        if (LEDBetweenR == 0)
                        {
                            GameObject s1drL5 = transform.Find("Suit1 down right LED5").gameObject;
                            s1drL5.gameObject.SetActive(true);
                        }
                        if (LEDBetweenL == 0)
                        {
                            GameObject s1dlL5 = transform.Find("Suit1 down left LED5").gameObject;
                            s1dlL5.gameObject.SetActive(true);
                        }
                    }
                }

                //정장 신발
                shoesColor = Random.Range(0, 3);

                if (shoesColor == 0) //1번 신발
                {
                    Job2_1ShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s3rc1 = transform.Find("Shoes3 right color1").gameObject;
                        s3rc1.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s3rc1t = transform.Find("Shoes3 right color1 tear").gameObject;
                        s3rc1t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s3lc1 = transform.Find("Shoes3 left color1").gameObject;
                        s3lc1.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s3lc1t = transform.Find("Shoes3 left color1 tear").gameObject;
                        s3lc1t.gameObject.SetActive(true);
                    }
                }
                else if (shoesColor == 1) //2번 신발
                {
                    Job2_2ShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s3rc2 = transform.Find("Shoes3 right color2").gameObject;
                        s3rc2.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s3rc2t = transform.Find("Shoes3 right color2 tear").gameObject;
                        s3rc2t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s3lc2 = transform.Find("Shoes3 left color2").gameObject;
                        s3lc2.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s3lc2t = transform.Find("Shoes3 left color2 tear").gameObject;
                        s3lc2t.gameObject.SetActive(true);
                    }
                }
                else //3번 신발
                {
                    Job2_3ShoesOff = true;
                    tearingSR = Random.Range(0, 2); //찢어짐

                    if (tearingSR == 0)
                    {
                        GameObject s3rc3 = transform.Find("Shoes3 right color3").gameObject;
                        s3rc3.gameObject.SetActive(true);
                    }
                    else if (tearingSR == 1)
                    {
                        GameObject s3rc3t = transform.Find("Shoes3 right color3 tear").gameObject;
                        s3rc3t.gameObject.SetActive(true);
                    }

                    tearingSL = Random.Range(0, 2); //찢어짐

                    if (tearingSL == 0)
                    {
                        GameObject s3lc3 = transform.Find("Shoes3 left color3").gameObject;
                        s3lc3.gameObject.SetActive(true);
                    }
                    else if (tearingSL == 1)
                    {
                        GameObject s3lc3t = transform.Find("Shoes3 left color3 tear").gameObject;
                        s3lc3t.gameObject.SetActive(true);
                    }
                }
            }
        }


        //기술자 ============================================================================================
        else if (job > 60 && job <= 80)
        {
            suitsColor = Random.Range(0, 5);

            if (suitsColor == 0) //1번 작업복
            {
                Job3_1Off = true;
                GameObject s2tbc1 = transform.Find("Suit2 top body color1").gameObject;
                GameObject s2truac1 = transform.Find("Suit2 top right up arm color1").gameObject;
                GameObject s2trdac1 = transform.Find("Suit2 top right down arm color1").gameObject;
                GameObject s2tluac1 = transform.Find("Suit2 top left up arm color1").gameObject;
                GameObject s2tldac1 = transform.Find("Suit2 top left down arm color1").gameObject;
                GameObject s2drc1 = transform.Find("Suit2 down right color1").gameObject;
                GameObject s2dlc1 = transform.Find("Suit2 down left color1").gameObject;
                s2tbc1.gameObject.SetActive(true);
                s2truac1.gameObject.SetActive(true);
                s2trdac1.gameObject.SetActive(true);
                s2tluac1.gameObject.SetActive(true);
                s2tldac1.gameObject.SetActive(true);
                s2drc1.gameObject.SetActive(true);
                s2dlc1.gameObject.SetActive(true);

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job3_1SOff = true;
                    GameObject s4rc1 = transform.Find("Shoes4 right color1").gameObject;
                    s4rc1.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job3_1SOff = true;
                    GameObject s4rc1t = transform.Find("Shoes4 right color1 tear").gameObject;
                    s4rc1t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                    s4lc1.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                    s4lc1t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2); //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s2tbcc1_1 = transform.Find("Suit2 top body cloth color1-1").gameObject;
                            s2tbcc1_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s2tbcc1_2 = transform.Find("Suit2 top body cloth color1-2").gameObject;
                            s2tbcc1_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s2tbcc1_3 = transform.Find("Suit2 top body cloth color1-3").gameObject;
                            s2tbcc1_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s2drcc1 = transform.Find("Suit2 down right cloth color1").gameObject;
                            s2drcc1.gameObject.SetActive(true);
                            DownRclothTear = 1;
                        }
                        if (i == 5)
                        {
                            GameObject s2dlcc1 = transform.Find("Suit2 down left cloth color1").gameObject;
                            s2dlcc1.gameObject.SetActive(true);
                            DownLclothTear = 1;
                        }
                        if (i == 6)
                        {
                            GameObject s2tldacc1 = transform.Find("Suit2 top left down arm cloth color1").gameObject;
                            s2tldacc1.gameObject.SetActive(true);
                            TopDownLclothTear = 1;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s2tbcc1_1 = transform.Find("Suit2 top body cloth color1-1").gameObject;
                    GameObject s2tbcc1_2 = transform.Find("Suit2 top body cloth color1-2").gameObject;
                    GameObject s2tbcc1_3 = transform.Find("Suit2 top body cloth color1-3").gameObject;
                    GameObject s2drcc1 = transform.Find("Suit2 down right cloth color1").gameObject;
                    GameObject s2dlcc1 = transform.Find("Suit2 down left cloth color1").gameObject;
                    GameObject s2tldacc1 = transform.Find("Suit2 top left down arm cloth color1").gameObject;
                    s2tbcc1_1.gameObject.SetActive(true);
                    s2tbcc1_2.gameObject.SetActive(true);
                    s2tbcc1_3.gameObject.SetActive(true);
                    s2drcc1.gameObject.SetActive(true);
                    s2dlcc1.gameObject.SetActive(true);
                    s2tldacc1.gameObject.SetActive(true);
                    clothTear = 1;
                }
            }
            else if (suitsColor == 1) //2번 작업복
            {
                Job3_2Off = true;
                GameObject s2tbc2 = transform.Find("Suit2 top body color2").gameObject;
                GameObject s2truac2 = transform.Find("Suit2 top right up arm color2").gameObject;
                GameObject s2trdac2 = transform.Find("Suit2 top right down arm color2").gameObject;
                GameObject s2tluac2 = transform.Find("Suit2 top left up arm color2").gameObject;
                GameObject s2tldac2 = transform.Find("Suit2 top left down arm color2").gameObject;
                GameObject s2drc2 = transform.Find("Suit2 down right color2").gameObject;
                GameObject s2dlc2 = transform.Find("Suit2 down left color2").gameObject;
                s2tbc2.gameObject.SetActive(true);
                s2truac2.gameObject.SetActive(true);
                s2trdac2.gameObject.SetActive(true);
                s2tluac2.gameObject.SetActive(true);
                s2tldac2.gameObject.SetActive(true);
                s2drc2.gameObject.SetActive(true);
                s2dlc2.gameObject.SetActive(true);

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job3_2SOff = true;
                    GameObject s4rc2 = transform.Find("Shoes4 right color2").gameObject;
                    s4rc2.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job3_2SOff = true;
                    GameObject s4rc2t = transform.Find("Shoes4 right color2 tear").gameObject;
                    s4rc2t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job3_2SOff = true;
                    GameObject s4lc2 = transform.Find("Shoes4 left color2").gameObject;
                    s4lc2.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job3_2SOff = true;
                    GameObject s4lc2t = transform.Find("Shoes4 left color2 tear").gameObject;
                    s4lc2t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s2tbcc2_1 = transform.Find("Suit2 top body cloth color2-1").gameObject;
                            s2tbcc2_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s2tbcc2_2 = transform.Find("Suit2 top body cloth color2-2").gameObject;
                            s2tbcc2_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s2tbcc2_3 = transform.Find("Suit2 top body cloth color2-3").gameObject;
                            s2tbcc2_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s2drcc2 = transform.Find("Suit2 down right cloth color2").gameObject;
                            s2drcc2.gameObject.SetActive(true);
                            DownRclothTear = 2;
                        }
                        if (i == 5)
                        {
                            GameObject s2dlcc2 = transform.Find("Suit2 down left cloth color2").gameObject;
                            s2dlcc2.gameObject.SetActive(true);
                            DownLclothTear = 2;
                        }
                        if (i == 6)
                        {
                            GameObject s2tldacc2 = transform.Find("Suit2 top left down arm cloth color2").gameObject;
                            s2tldacc2.gameObject.SetActive(true);
                            TopDownLclothTear = 2;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s2tbcc2_1 = transform.Find("Suit2 top body cloth color2-1").gameObject;
                    GameObject s2tbcc2_2 = transform.Find("Suit2 top body cloth color2-2").gameObject;
                    GameObject s2tbcc2_3 = transform.Find("Suit2 top body cloth color2-3").gameObject;
                    GameObject s2drcc2 = transform.Find("Suit2 down right cloth color2").gameObject;
                    GameObject s2dlcc2 = transform.Find("Suit2 down left cloth color2").gameObject;
                    GameObject s2tldacc2 = transform.Find("Suit2 top left down arm cloth color2").gameObject;
                    s2tbcc2_1.gameObject.SetActive(true);
                    s2tbcc2_2.gameObject.SetActive(true);
                    s2tbcc2_3.gameObject.SetActive(true);
                    s2drcc2.gameObject.SetActive(true);
                    s2dlcc2.gameObject.SetActive(true);
                    s2tldacc2.gameObject.SetActive(true);
                    clothTear = 2;
                }
            }
            else if (suitsColor == 2) //3번 작업복
            {
                Job3_3Off = true;
                GameObject s2tbc3 = transform.Find("Suit2 top body color3").gameObject;
                GameObject s2truac3 = transform.Find("Suit2 top right up arm color3").gameObject;
                GameObject s2trdac3 = transform.Find("Suit2 top right down arm color3").gameObject;
                GameObject s2tluac3 = transform.Find("Suit2 top left up arm color3").gameObject;
                GameObject s2tldac3 = transform.Find("Suit2 top left down arm color3").gameObject;
                GameObject s2drc3 = transform.Find("Suit2 down right color3").gameObject;
                GameObject s2dlc3 = transform.Find("Suit2 down left color3").gameObject;
                s2tbc3.gameObject.SetActive(true);
                s2truac3.gameObject.SetActive(true);
                s2trdac3.gameObject.SetActive(true);
                s2tluac3.gameObject.SetActive(true);
                s2tldac3.gameObject.SetActive(true);
                s2drc3.gameObject.SetActive(true);
                s2dlc3.gameObject.SetActive(true);

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job3_3SOff = true;
                    GameObject s4rc3 = transform.Find("Shoes4 right color3").gameObject;
                    s4rc3.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job3_3SOff = true;
                    GameObject s4rc3t = transform.Find("Shoes4 right color3 tear").gameObject;
                    s4rc3t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                    s4lc1.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                    s4lc1t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s2tbcc3_1 = transform.Find("Suit2 top body cloth color3-1").gameObject;
                            s2tbcc3_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s2tbcc3_2 = transform.Find("Suit2 top body cloth color3-2").gameObject;
                            s2tbcc3_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s2tbcc3_3 = transform.Find("Suit2 top body cloth color3-3").gameObject;
                            s2tbcc3_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s2drcc3 = transform.Find("Suit2 down right cloth color3").gameObject;
                            s2drcc3.gameObject.SetActive(true);
                            DownRclothTear = 3;
                        }
                        if (i == 5)
                        {
                            GameObject s2dlcc3 = transform.Find("Suit2 down left cloth color3").gameObject;
                            s2dlcc3.gameObject.SetActive(true);
                            DownLclothTear = 3;
                        }
                        if (i == 6)
                        {
                            GameObject s2tldacc3 = transform.Find("Suit2 top left down arm cloth color3").gameObject;
                            s2tldacc3.gameObject.SetActive(true);
                            TopDownLclothTear = 3;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s2tbcc3_1 = transform.Find("Suit2 top body cloth color3-1").gameObject;
                    GameObject s2tbcc3_2 = transform.Find("Suit2 top body cloth color3-2").gameObject;
                    GameObject s2tbcc3_3 = transform.Find("Suit2 top body cloth color3-3").gameObject;
                    GameObject s2drcc3 = transform.Find("Suit2 down right cloth color3").gameObject;
                    GameObject s2dlcc3 = transform.Find("Suit2 down left cloth color3").gameObject;
                    GameObject s2tldacc3 = transform.Find("Suit2 top left down arm cloth color3").gameObject;
                    s2tbcc3_1.gameObject.SetActive(true);
                    s2tbcc3_2.gameObject.SetActive(true);
                    s2tbcc3_3.gameObject.SetActive(true);
                    s2drcc3.gameObject.SetActive(true);
                    s2dlcc3.gameObject.SetActive(true);
                    s2tldacc3.gameObject.SetActive(true);
                    clothTear = 3;
                }
            }
            else if (suitsColor == 3) //4번 작업복
            {
                Job3_4Off = true;
                GameObject s2tbc4 = transform.Find("Suit2 top body color4").gameObject;
                GameObject s2truac4 = transform.Find("Suit2 top right up arm color4").gameObject;
                GameObject s2trdac4 = transform.Find("Suit2 top right down arm color4").gameObject;
                GameObject s2tluac4 = transform.Find("Suit2 top left up arm color4").gameObject;
                GameObject s2tldac4 = transform.Find("Suit2 top left down arm color4").gameObject;
                GameObject s2drc4 = transform.Find("Suit2 down right color4").gameObject;
                GameObject s2dlc4 = transform.Find("Suit2 down left color4").gameObject;
                s2tbc4.gameObject.SetActive(true);
                s2truac4.gameObject.SetActive(true);
                s2trdac4.gameObject.SetActive(true);
                s2tluac4.gameObject.SetActive(true);
                s2tldac4.gameObject.SetActive(true);
                s2drc4.gameObject.SetActive(true);
                s2dlc4.gameObject.SetActive(true);

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job3_4SOff = true;
                    GameObject s4rc4 = transform.Find("Shoes4 right color4").gameObject;
                    s4rc4.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job3_4SOff = true;
                    GameObject s4rc4t = transform.Find("Shoes4 right color4 tear").gameObject;
                    s4rc4t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                    s4lc1.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                    s4lc1t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s2tbcc4_1 = transform.Find("Suit2 top body cloth color4-1").gameObject;
                            s2tbcc4_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s2tbcc4_2 = transform.Find("Suit2 top body cloth color4-2").gameObject;
                            s2tbcc4_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s2tbcc4_3 = transform.Find("Suit2 top body cloth color4-3").gameObject;
                            s2tbcc4_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s2drcc4 = transform.Find("Suit2 down right cloth color4").gameObject;
                            s2drcc4.gameObject.SetActive(true);
                            DownRclothTear = 4;
                        }
                        if (i == 5)
                        {
                            GameObject s2dlcc4 = transform.Find("Suit2 down left cloth color4").gameObject;
                            s2dlcc4.gameObject.SetActive(true);
                            DownLclothTear = 4;
                        }
                        if (i == 6)
                        {
                            GameObject s2tldacc4 = transform.Find("Suit2 top left down arm cloth color4").gameObject;
                            s2tldacc4.gameObject.SetActive(true);
                            TopDownLclothTear = 4;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s2tbcc4_1 = transform.Find("Suit2 top body cloth color4-1").gameObject;
                    GameObject s2tbcc4_2 = transform.Find("Suit2 top body cloth color4-2").gameObject;
                    GameObject s2tbcc4_3 = transform.Find("Suit2 top body cloth color4-3").gameObject;
                    GameObject s2drcc4 = transform.Find("Suit2 down right cloth color4").gameObject;
                    GameObject s2dlcc4 = transform.Find("Suit2 down left cloth color4").gameObject;
                    GameObject s2tldacc4 = transform.Find("Suit2 top left down arm cloth color4").gameObject;
                    s2tbcc4_1.gameObject.SetActive(true);
                    s2tbcc4_2.gameObject.SetActive(true);
                    s2tbcc4_3.gameObject.SetActive(true);
                    s2drcc4.gameObject.SetActive(true);
                    s2dlcc4.gameObject.SetActive(true);
                    s2tldacc4.gameObject.SetActive(true);
                    clothTear = 4;
                }
            }
            else if (suitsColor == 4) //5번 작업복
            {
                Job3_5Off = true;
                GameObject s21tbc5 = transform.Find("Suit2 top body color5").gameObject;
                GameObject s2truac5 = transform.Find("Suit2 top right up arm color5").gameObject;
                GameObject s2trdac5 = transform.Find("Suit2 top right down arm color5").gameObject;
                GameObject s2tluac5 = transform.Find("Suit2 top left up arm color5").gameObject;
                GameObject s2tldac5 = transform.Find("Suit2 top left down arm color5").gameObject;
                GameObject s2drc5 = transform.Find("Suit2 down right color5").gameObject;
                GameObject s2dlc5 = transform.Find("Suit2 down left color5").gameObject;
                s21tbc5.gameObject.SetActive(true);
                s2truac5.gameObject.SetActive(true);
                s2trdac5.gameObject.SetActive(true);
                s2tluac5.gameObject.SetActive(true);
                s2tldac5.gameObject.SetActive(true);
                s2drc5.gameObject.SetActive(true);
                s2dlc5.gameObject.SetActive(true);

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job3_4SOff = true;
                    GameObject s4rc4 = transform.Find("Shoes4 right color4").gameObject;
                    s4rc4.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job3_4SOff = true;
                    GameObject s4rc4t = transform.Find("Shoes4 right color4 tear").gameObject;
                    s4rc4t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
                    s4lc1.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job3_1SOff = true;
                    GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
                    s4lc1t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s2tbcc5_1 = transform.Find("Suit2 top body cloth color5-1").gameObject;
                            s2tbcc5_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s2tbcc5_2 = transform.Find("Suit2 top body cloth color5-2").gameObject;
                            s2tbcc5_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s2tbcc5_3 = transform.Find("Suit2 top body cloth color5-3").gameObject;
                            s2tbcc5_3.gameObject.SetActive(true);
                        }
                        if (i == 4)
                        {
                            GameObject s2drcc5 = transform.Find("Suit2 down right cloth color5").gameObject;
                            s2drcc5.gameObject.SetActive(true);
                            DownRclothTear = 5;
                        }
                        if (i == 5)
                        {
                            GameObject s2dlcc5 = transform.Find("Suit2 down left cloth color5").gameObject;
                            s2dlcc5.gameObject.SetActive(true);
                            DownRclothTear = 5;
                        }
                        if (i == 6)
                        {
                            GameObject s2tldacc5 = transform.Find("Suit2 top left down arm cloth color5").gameObject;
                            s2tldacc5.gameObject.SetActive(true);
                            TopDownLclothTear = 5;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s2tbcc5_1 = transform.Find("Suit2 top body cloth color5-1").gameObject;
                    GameObject s2tbcc5_2 = transform.Find("Suit2 top body cloth color5-2").gameObject;
                    GameObject s2tbcc5_3 = transform.Find("Suit2 top body cloth color5-3").gameObject;
                    GameObject s2drcc5 = transform.Find("Suit2 down right cloth color5").gameObject;
                    GameObject s2dlcc5 = transform.Find("Suit2 down left cloth color5").gameObject;
                    GameObject s2tldacc5 = transform.Find("Suit2 top left down arm cloth color5").gameObject;
                    s2tbcc5_1.gameObject.SetActive(true);
                    s2tbcc5_2.gameObject.SetActive(true);
                    s2tbcc5_3.gameObject.SetActive(true);
                    s2drcc5.gameObject.SetActive(true);
                    s2dlcc5.gameObject.SetActive(true);
                    s2tldacc5.gameObject.SetActive(true);
                    clothTear = 5;
                }
            }

            LED = Random.Range(0, 2); //상의, 하의 LED 활성화

            if (LED == 0)
            {
                LEDcolor = Random.Range(0, 5);

                if (LEDcolor == 0)
                {
                    Job3_1LEDOff = true;
                    GameObject s2tbL1 = transform.Find("Suit2 top body LED1").gameObject;
                    GameObject s2trdaL1 = transform.Find("Suit2 top right down arm LED1").gameObject;
                    GameObject s2tldaL1 = transform.Find("Suit2 top left down arm LED1").gameObject;
                    GameObject s2drL1 = transform.Find("Suit2 down right LED1").gameObject;
                    GameObject s2dlL1 = transform.Find("Suit2 down left LED1").gameObject;
                    s2tbL1.gameObject.SetActive(true);
                    s2trdaL1.gameObject.SetActive(true);
                    s2tldaL1.gameObject.SetActive(true);
                    s2drL1.gameObject.SetActive(true);
                    s2dlL1.gameObject.SetActive(true);
                }
                else if (LEDcolor == 1)
                {
                    Job3_2LEDOff = true;
                    GameObject s2tbL2 = transform.Find("Suit2 top body LED2").gameObject;
                    GameObject s2trdaL2 = transform.Find("Suit2 top right down arm LED2").gameObject;
                    GameObject s2tldaL2 = transform.Find("Suit2 top left down arm LED2").gameObject;
                    GameObject s2drL2 = transform.Find("Suit2 down right LED2").gameObject;
                    GameObject s2dlL2 = transform.Find("Suit2 down left LED2").gameObject;
                    s2tbL2.gameObject.SetActive(true);
                    s2trdaL2.gameObject.SetActive(true);
                    s2tldaL2.gameObject.SetActive(true);
                    s2drL2.gameObject.SetActive(true);
                    s2dlL2.gameObject.SetActive(true);
                }
                else if (LEDcolor == 2)
                {
                    Job3_3LEDOff = true;
                    GameObject s2tbL3 = transform.Find("Suit2 top body LED3").gameObject;
                    GameObject s2trdaL3 = transform.Find("Suit2 top right down arm LED3").gameObject;
                    GameObject s2tldaL3 = transform.Find("Suit2 top left down arm LED3").gameObject;
                    GameObject s2drL3 = transform.Find("Suit2 down right LED3").gameObject;
                    GameObject s2dlL3 = transform.Find("Suit2 down left LED3").gameObject;
                    s2tbL3.gameObject.SetActive(true);
                    s2trdaL3.gameObject.SetActive(true);
                    s2tldaL3.gameObject.SetActive(true);
                    s2drL3.gameObject.SetActive(true);
                    s2dlL3.gameObject.SetActive(true);
                }
                else if (LEDcolor == 3)
                {
                    Job3_4LEDOff = true;
                    GameObject s2tbL4 = transform.Find("Suit2 top body LED4").gameObject;
                    GameObject s2trdaL4 = transform.Find("Suit2 top right down arm LED4").gameObject;
                    GameObject s2tldaL4 = transform.Find("Suit2 top left down arm LED4").gameObject;
                    GameObject s2drL4 = transform.Find("Suit2 down right LED4").gameObject;
                    GameObject s2dlL4 = transform.Find("Suit2 down left LED4").gameObject;
                    s2tbL4.gameObject.SetActive(true);
                    s2trdaL4.gameObject.SetActive(true);
                    s2tldaL4.gameObject.SetActive(true);
                    s2drL4.gameObject.SetActive(true);
                    s2dlL4.gameObject.SetActive(true);
                }
                else if (LEDcolor == 4)
                {
                    Job3_5LEDOff = true;
                    GameObject s2tbL5 = transform.Find("Suit2 top body LED5").gameObject;
                    GameObject s2trdaL5 = transform.Find("Suit2 top right down arm LED5").gameObject;
                    GameObject s2tldaL5 = transform.Find("Suit2 top left down arm LED5").gameObject;
                    GameObject s2drL5 = transform.Find("Suit2 down right LED5").gameObject;
                    GameObject s2dlL5 = transform.Find("Suit2 down left LED5").gameObject;
                    s2tbL5.gameObject.SetActive(true);
                    s2trdaL5.gameObject.SetActive(true);
                    s2tldaL5.gameObject.SetActive(true);
                    s2drL5.gameObject.SetActive(true);
                    s2dlL5.gameObject.SetActive(true);
                }
            }
        }
        //의료원 ============================================================================================
        else if (job > 80 && job <= 100)
        {
            inCap = true;
            GetComponent<InfectorFaceEmotion>().setCap(1);
            suitsColor = Random.Range(0, 5);

            if (suitsColor == 0) //1번 작업복
            {
                Job4_1Off = true;
                GameObject s3tbc1 = transform.Find("Suit3 top body color1").gameObject;
                GameObject s3truac1 = transform.Find("Suit3 top right up arm color1").gameObject;
                GameObject s3trdac1 = transform.Find("Suit3 top right down arm color1").gameObject;
                GameObject s3tluac1 = transform.Find("Suit3 top left up arm color1").gameObject;
                GameObject s3tldac1 = transform.Find("Suit3 top left down arm color1").gameObject;
                GameObject s3drc1 = transform.Find("Suit3 down right color1").gameObject;
                GameObject s3dlc1 = transform.Find("Suit3 down left color1").gameObject;
                s3tbc1.gameObject.SetActive(true);
                s3truac1.gameObject.SetActive(true);
                s3trdac1.gameObject.SetActive(true);
                s3tluac1.gameObject.SetActive(true);
                s3tldac1.gameObject.SetActive(true);
                s3drc1.gameObject.SetActive(true);
                s3dlc1.gameObject.SetActive(true);

                if (fingerType == 1) //손가락 선택
                {
                    Job4_1GOff = true;
                    Glovefinger = Random.Range(0, 3);

                    if (Glovefinger == 0)
                    {
                        GameObject g1hand1rc1 = transform.Find("Grove1 right hand1 color1").gameObject;
                        GameObject g1hand1lc1 = transform.Find("Grove1 left hand1 color1").gameObject;
                        g1hand1rc1.gameObject.SetActive(true);
                        g1hand1lc1.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 1)
                    {
                        GameObject g1hand2rc1 = transform.Find("Grove1 right hand2 color1").gameObject;
                        GameObject g1hand2lc1 = transform.Find("Grove1 left hand2 color1").gameObject;
                        g1hand2rc1.gameObject.SetActive(true);
                        g1hand2lc1.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 2)
                    {
                        GameObject g1hand3rc1 = transform.Find("Grove1 right hand3 color1").gameObject;
                        GameObject g1hand3lc1 = transform.Find("Grove1 left hand3 color1").gameObject;
                        g1hand3rc1.gameObject.SetActive(true);
                        g1hand3lc1.gameObject.SetActive(true);
                    }

                    fingerColor = 1;
                }

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job4_1SOff = true;
                    GameObject s5rc1 = transform.Find("Shoes5 right color1").gameObject;
                    s5rc1.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job4_1SOff = true;
                    GameObject s5rc1t = transform.Find("Shoes5 right color1 tear").gameObject;
                    s5rc1t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job4_1SOff = true;
                    GameObject s5lc1 = transform.Find("Shoes5 left color1").gameObject;
                    s5lc1.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job4_1SOff = true;
                    GameObject s5lc1t = transform.Find("Shoes5 left color1 tear").gameObject;
                    s5lc1t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2); //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s3tbcc1_1 = transform.Find("Suit3 top body cloth color1-1").gameObject;
                            s3tbcc1_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s3tbcc1_2 = transform.Find("Suit3 top body cloth color1-2").gameObject;
                            s3tbcc1_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s3trdacc1 = transform.Find("Suit3 top right down arm cloth color1").gameObject;
                            s3trdacc1.gameObject.SetActive(true);
                            TopDownRclothTear = 1;
                        }
                        if (i == 4)
                        {
                            GameObject s3drcc1 = transform.Find("Suit3 down right cloth color1").gameObject;
                            s3drcc1.gameObject.SetActive(true);
                            DownRclothTear = 1;
                        }
                        if (i == 5)
                        {
                            GameObject s3dlcc1 = transform.Find("Suit3 down left cloth color1").gameObject;
                            s3dlcc1.gameObject.SetActive(true);
                            DownLclothTear = 1;
                        }
                        if (i == 6)
                        {
                            GameObject s3tldacc1 = transform.Find("Suit3 top left down arm cloth color1").gameObject;
                            s3tldacc1.gameObject.SetActive(true);
                            TopDownLclothTear = 1;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s3tbcc1_1 = transform.Find("Suit3 top body cloth color1-1").gameObject;
                    GameObject s3tbcc1_2 = transform.Find("Suit3 top body cloth color1-2").gameObject;
                    GameObject s3trdacc1 = transform.Find("Suit3 top right down arm cloth color1").gameObject;
                    GameObject s3drcc1 = transform.Find("Suit3 down right cloth color1").gameObject;
                    GameObject s3dlcc1 = transform.Find("Suit3 down left cloth color1").gameObject;
                    GameObject s3tldacc1 = transform.Find("Suit3 top left down arm cloth color1").gameObject;
                    s3tbcc1_1.gameObject.SetActive(true);
                    s3tbcc1_2.gameObject.SetActive(true);
                    s3trdacc1.gameObject.SetActive(true);
                    s3drcc1.gameObject.SetActive(true);
                    s3dlcc1.gameObject.SetActive(true);
                    s3tldacc1.gameObject.SetActive(true);
                    clothTear = 1;
                }
            }
            else if (suitsColor == 1) //2번 작업복
            {
                Job4_2Off = true;
                GameObject s3tbc2 = transform.Find("Suit3 top body color2").gameObject;
                GameObject s3truac2 = transform.Find("Suit3 top right up arm color2").gameObject;
                GameObject s3trdac2 = transform.Find("Suit3 top right down arm color2").gameObject;
                GameObject s3tluac2 = transform.Find("Suit3 top left up arm color2").gameObject;
                GameObject s3tldac2 = transform.Find("Suit3 top left down arm color2").gameObject;
                GameObject s3drc2 = transform.Find("Suit3 down right color2").gameObject;
                GameObject s3dlc2 = transform.Find("Suit3 down left color2").gameObject;
                s3tbc2.gameObject.SetActive(true);
                s3truac2.gameObject.SetActive(true);
                s3trdac2.gameObject.SetActive(true);
                s3tluac2.gameObject.SetActive(true);
                s3tldac2.gameObject.SetActive(true);
                s3drc2.gameObject.SetActive(true);
                s3dlc2.gameObject.SetActive(true);

                if (fingerType == 1) //손가락 선택
                {
                    Job4_2GOff = true;
                    Glovefinger = Random.Range(0, 3);

                    if (Glovefinger == 0)
                    {
                        GameObject g1hand1rc2 = transform.Find("Grove1 right hand1 color2").gameObject;
                        GameObject g1hand1lc2 = transform.Find("Grove1 left hand1 color2").gameObject;
                        g1hand1rc2.gameObject.SetActive(true);
                        g1hand1lc2.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 1)
                    {
                        GameObject g1hand2rc2 = transform.Find("Grove1 right hand2 color2").gameObject;
                        GameObject g1hand2lc2 = transform.Find("Grove1 left hand2 color2").gameObject;
                        g1hand2rc2.gameObject.SetActive(true);
                        g1hand2lc2.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 2)
                    {
                        GameObject g1hand3rc2 = transform.Find("Grove1 right hand3 color2").gameObject;
                        GameObject g1hand3lc2 = transform.Find("Grove1 left hand3 color2").gameObject;
                        g1hand3rc2.gameObject.SetActive(true);
                        g1hand3lc2.gameObject.SetActive(true);
                    }

                    fingerColor = 2;
                }

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job4_2SOff = true;
                    GameObject s5rc2 = transform.Find("Shoes5 right color2").gameObject;
                    s5rc2.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job4_2SOff = true;
                    GameObject s5rc2t = transform.Find("Shoes5 right color2 tear").gameObject;
                    s5rc2t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job4_2SOff = true;
                    GameObject s5lc2 = transform.Find("Shoes5 left color2").gameObject;
                    s5lc2.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job4_2SOff = true;
                    GameObject s5lc2t = transform.Find("Shoes5 left color2 tear").gameObject;
                    s5lc2t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s3tbcc2_1 = transform.Find("Suit3 top body cloth color2-1").gameObject;
                            s3tbcc2_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s3tbcc2_2 = transform.Find("Suit3 top body cloth color2-2").gameObject;
                            s3tbcc2_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s3trdacc2 = transform.Find("Suit3 top right down arm cloth color2").gameObject;
                            s3trdacc2.gameObject.SetActive(true);
                            TopDownRclothTear = 2;
                        }
                        if (i == 4)
                        {
                            GameObject s3drcc2 = transform.Find("Suit3 down right cloth color2").gameObject;
                            s3drcc2.gameObject.SetActive(true);
                            DownRclothTear = 2;
                        }
                        if (i == 5)
                        {
                            GameObject s3dlcc2 = transform.Find("Suit3 down left cloth color2").gameObject;
                            s3dlcc2.gameObject.SetActive(true);
                            DownLclothTear = 2;
                        }
                        if (i == 6)
                        {
                            GameObject s3tldacc2 = transform.Find("Suit3 top left down arm cloth color2").gameObject;
                            s3tldacc2.gameObject.SetActive(true);
                            TopDownLclothTear = 2;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s3tbcc2_1 = transform.Find("Suit3 top body cloth color2-1").gameObject;
                    GameObject s3tbcc2_2 = transform.Find("Suit3 top body cloth color2-2").gameObject;
                    GameObject s3trdacc2 = transform.Find("Suit3 top right down arm cloth color2").gameObject;
                    GameObject s3drcc2 = transform.Find("Suit3 down right cloth color2").gameObject;
                    GameObject s3dlcc2 = transform.Find("Suit3 down left cloth color2").gameObject;
                    GameObject s3tldacc2 = transform.Find("Suit3 top left down arm cloth color2").gameObject;
                    s3tbcc2_1.gameObject.SetActive(true);
                    s3tbcc2_2.gameObject.SetActive(true);
                    s3trdacc2.gameObject.SetActive(true);
                    s3drcc2.gameObject.SetActive(true);
                    s3dlcc2.gameObject.SetActive(true);
                    s3tldacc2.gameObject.SetActive(true);
                    clothTear = 2;
                }
            }
            else if (suitsColor == 2) //3번 작업복
            {
                Job4_3Off = true;
                GameObject s3tbc3 = transform.Find("Suit3 top body color3").gameObject;
                GameObject s3truac3 = transform.Find("Suit3 top right up arm color3").gameObject;
                GameObject s3trdac3 = transform.Find("Suit3 top right down arm color3").gameObject;
                GameObject s3tluac3 = transform.Find("Suit3 top left up arm color3").gameObject;
                GameObject s3tldac3 = transform.Find("Suit3 top left down arm color3").gameObject;
                GameObject s3drc3 = transform.Find("Suit3 down right color3").gameObject;
                GameObject s3dlc3 = transform.Find("Suit3 down left color3").gameObject;
                s3tbc3.gameObject.SetActive(true);
                s3truac3.gameObject.SetActive(true);
                s3trdac3.gameObject.SetActive(true);
                s3tluac3.gameObject.SetActive(true);
                s3tldac3.gameObject.SetActive(true);
                s3drc3.gameObject.SetActive(true);
                s3dlc3.gameObject.SetActive(true);

                if (fingerType == 1) //손가락 선택
                {
                    Job4_3GOff = true;
                    Glovefinger = Random.Range(0, 3);

                    if (Glovefinger == 0)
                    {
                        GameObject g1hand1rc3 = transform.Find("Grove1 right hand1 color3").gameObject;
                        GameObject g1hand1lc3 = transform.Find("Grove1 left hand1 color3").gameObject;
                        g1hand1rc3.gameObject.SetActive(true);
                        g1hand1lc3.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 1)
                    {
                        GameObject g1hand2rc3 = transform.Find("Grove1 right hand2 color3").gameObject;
                        GameObject g1hand2lc3 = transform.Find("Grove1 left hand2 color3").gameObject;
                        g1hand2rc3.gameObject.SetActive(true);
                        g1hand2lc3.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 2)
                    {
                        GameObject g1hand3rc3 = transform.Find("Grove1 right hand3 color3").gameObject;
                        GameObject g1hand3lc3 = transform.Find("Grove1 left hand3 color3").gameObject;
                        g1hand3rc3.gameObject.SetActive(true);
                        g1hand3lc3.gameObject.SetActive(true);
                    }

                    fingerColor = 3;
                }

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job4_3SOff = true;
                    GameObject s5rc3 = transform.Find("Shoes5 right color3").gameObject;
                    s5rc3.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job4_3SOff = true;
                    GameObject s5rc3t = transform.Find("Shoes5 right color3 tear").gameObject;
                    s5rc3t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job4_3SOff = true;
                    GameObject s5lc3 = transform.Find("Shoes5 left color3").gameObject;
                    s5lc3.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job4_3SOff = true;
                    GameObject s5lc3t = transform.Find("Shoes5 left color3 tear").gameObject;
                    s5lc3t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s3tbcc3_1 = transform.Find("Suit3 top body cloth color3-1").gameObject;
                            s3tbcc3_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s3tbcc3_2 = transform.Find("Suit3 top body cloth color3-2").gameObject;
                            s3tbcc3_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s3trdacc3 = transform.Find("Suit3 top right down arm cloth color3").gameObject;
                            s3trdacc3.gameObject.SetActive(true);
                            TopDownRclothTear = 3;
                        }
                        if (i == 4)
                        {
                            GameObject s3drcc3 = transform.Find("Suit3 down right cloth color3").gameObject;
                            s3drcc3.gameObject.SetActive(true);
                            DownRclothTear = 3;
                        }
                        if (i == 5)
                        {
                            GameObject s3dlcc3 = transform.Find("Suit3 down left cloth color3").gameObject;
                            s3dlcc3.gameObject.SetActive(true);
                            DownLclothTear = 3;
                        }
                        if (i == 6)
                        {
                            GameObject s3tldacc3 = transform.Find("Suit3 top left down arm cloth color3").gameObject;
                            s3tldacc3.gameObject.SetActive(true);
                            TopDownLclothTear = 3;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s3tbcc3_1 = transform.Find("Suit3 top body cloth color3-1").gameObject;
                    GameObject s3tbcc3_2 = transform.Find("Suit3 top body cloth color3-2").gameObject;
                    GameObject s3trdacc3 = transform.Find("Suit3 top right down arm cloth color3").gameObject;
                    GameObject s3drcc3 = transform.Find("Suit3 down right cloth color3").gameObject;
                    GameObject s3dlcc3 = transform.Find("Suit3 down left cloth color3").gameObject;
                    GameObject s3tldacc3 = transform.Find("Suit3 top left down arm cloth color3").gameObject;
                    s3tbcc3_1.gameObject.SetActive(true);
                    s3tbcc3_2.gameObject.SetActive(true);
                    s3trdacc3.gameObject.SetActive(true);
                    s3drcc3.gameObject.SetActive(true);
                    s3dlcc3.gameObject.SetActive(true);
                    s3tldacc3.gameObject.SetActive(true);
                    clothTear = 3;
                }
            }
            else if (suitsColor == 3) //4번 작업복
            {
                Job4_4Off = true;
                GameObject s3tbc4 = transform.Find("Suit3 top body color4").gameObject;
                GameObject s3truac4 = transform.Find("Suit3 top right up arm color4").gameObject;
                GameObject s3trdac4 = transform.Find("Suit3 top right down arm color4").gameObject;
                GameObject s3tluac4 = transform.Find("Suit3 top left up arm color4").gameObject;
                GameObject s3tldac4 = transform.Find("Suit3 top left down arm color4").gameObject;
                GameObject s3drc4 = transform.Find("Suit3 down right color4").gameObject;
                GameObject s3dlc4 = transform.Find("Suit3 down left color4").gameObject;
                s3tbc4.gameObject.SetActive(true);
                s3truac4.gameObject.SetActive(true);
                s3trdac4.gameObject.SetActive(true);
                s3tluac4.gameObject.SetActive(true);
                s3tldac4.gameObject.SetActive(true);
                s3drc4.gameObject.SetActive(true);
                s3dlc4.gameObject.SetActive(true);

                if (fingerType == 1) //손가락 선택
                {
                    Job4_4GOff = true;
                    Glovefinger = Random.Range(0, 3);

                    if (Glovefinger == 0)
                    {
                        GameObject g1hand1rc4 = transform.Find("Grove1 right hand1 color4").gameObject;
                        GameObject g1hand1lc4 = transform.Find("Grove1 left hand1 color4").gameObject;
                        g1hand1rc4.gameObject.SetActive(true);
                        g1hand1lc4.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 1)
                    {
                        GameObject g1hand2rc4 = transform.Find("Grove1 right hand2 color4").gameObject;
                        GameObject g1hand2lc4 = transform.Find("Grove1 left hand2 color4").gameObject;
                        g1hand2rc4.gameObject.SetActive(true);
                        g1hand2lc4.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 2)
                    {
                        GameObject g1hand3rc4 = transform.Find("Grove1 right hand3 color4").gameObject;
                        GameObject g1hand3lc4 = transform.Find("Grove1 left hand3 color4").gameObject;
                        g1hand3rc4.gameObject.SetActive(true);
                        g1hand3lc4.gameObject.SetActive(true);
                    }

                    fingerColor = 4;
                }

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job4_4SOff = true;
                    GameObject s5rc4 = transform.Find("Shoes5 right color4").gameObject;
                    s5rc4.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job4_4SOff = true;
                    GameObject s5rc4t = transform.Find("Shoes5 right color4 tear").gameObject;
                    s5rc4t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job4_4SOff = true;
                    GameObject s5lc4 = transform.Find("Shoes5 left color4").gameObject;
                    s5lc4.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job4_4SOff = true;
                    GameObject s5lc4t = transform.Find("Shoes5 left color4 tear").gameObject;
                    s5lc4t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s3tbcc4_1 = transform.Find("Suit3 top body cloth color4-1").gameObject;
                            s3tbcc4_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s3tbcc4_2 = transform.Find("Suit3 top body cloth color4-2").gameObject;
                            s3tbcc4_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s3trdacc4 = transform.Find("Suit3 top right down arm cloth color4").gameObject;
                            s3trdacc4.gameObject.SetActive(true);
                            TopDownRclothTear = 4;
                        }
                        if (i == 4)
                        {
                            GameObject s3drcc4 = transform.Find("Suit3 down right cloth color4").gameObject;
                            s3drcc4.gameObject.SetActive(true);
                            DownRclothTear = 4;
                        }
                        if (i == 5)
                        {
                            GameObject s3dlcc4 = transform.Find("Suit3 down left cloth color4").gameObject;
                            s3dlcc4.gameObject.SetActive(true);
                            DownLclothTear = 4;
                        }
                        if (i == 6)
                        {
                            GameObject s3tldacc4 = transform.Find("Suit3 top left down arm cloth color4").gameObject;
                            s3tldacc4.gameObject.SetActive(true);
                            TopDownLclothTear = 4;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s3tbcc4_1 = transform.Find("Suit3 top body cloth color4-1").gameObject;
                    GameObject s3tbcc4_2 = transform.Find("Suit3 top body cloth color4-2").gameObject;
                    GameObject s3trdacc4 = transform.Find("Suit3 top right down arm cloth color4").gameObject;
                    GameObject s3drcc4 = transform.Find("Suit3 down right cloth color4").gameObject;
                    GameObject s3dlcc4 = transform.Find("Suit3 down left cloth color4").gameObject;
                    GameObject s3tldacc4 = transform.Find("Suit3 top left down arm cloth color4").gameObject;
                    s3tbcc4_1.gameObject.SetActive(true);
                    s3tbcc4_2.gameObject.SetActive(true);
                    s3trdacc4.gameObject.SetActive(true);
                    s3drcc4.gameObject.SetActive(true);
                    s3dlcc4.gameObject.SetActive(true);
                    s3tldacc4.gameObject.SetActive(true);
                    clothTear = 4;
                }
            }
            else //5번 작업복
            {
                Job4_5Off = true;
                GameObject s3tbc5 = transform.Find("Suit3 top body color5").gameObject;
                GameObject s3truac5 = transform.Find("Suit3 top right up arm color5").gameObject;
                GameObject s3trdac5 = transform.Find("Suit3 top right down arm color5").gameObject;
                GameObject s3tluac5 = transform.Find("Suit3 top left up arm color5").gameObject;
                GameObject s3tldac5 = transform.Find("Suit3 top left down arm color5").gameObject;
                GameObject s3drc5 = transform.Find("Suit3 down right color5").gameObject;
                GameObject s3dlc5 = transform.Find("Suit3 down left color5").gameObject;
                s3tbc5.gameObject.SetActive(true);
                s3truac5.gameObject.SetActive(true);
                s3trdac5.gameObject.SetActive(true);
                s3tluac5.gameObject.SetActive(true);
                s3tldac5.gameObject.SetActive(true);
                s3drc5.gameObject.SetActive(true);
                s3dlc5.gameObject.SetActive(true);

                if (fingerType == 1) //손가락 선택
                {
                    Job4_5GOff = true;
                    Glovefinger = Random.Range(0, 3);

                    if (Glovefinger == 0)
                    {
                        GameObject g1hand1rc5 = transform.Find("Grove1 right hand1 color5").gameObject;
                        GameObject g1hand1lc5 = transform.Find("Grove1 left hand1 color5").gameObject;
                        g1hand1rc5.gameObject.SetActive(true);
                        g1hand1lc5.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 1)
                    {
                        GameObject g1hand2rc5 = transform.Find("Grove1 right hand2 color5").gameObject;
                        GameObject g1hand2lc5 = transform.Find("Grove1 left hand2 color5").gameObject;
                        g1hand2rc5.gameObject.SetActive(true);
                        g1hand2lc5.gameObject.SetActive(true);
                    }
                    else if (Glovefinger == 2)
                    {
                        GameObject g1hand3rc5 = transform.Find("Grove1 right hand3 color5").gameObject;
                        GameObject g1hand3lc5 = transform.Find("Grove1 left hand3 color5").gameObject;
                        g1hand3rc5.gameObject.SetActive(true);
                        g1hand3lc5.gameObject.SetActive(true);
                    }

                    fingerColor = 5;
                }

                tearingSR = Random.Range(0, 2); //신발 찢어짐

                if (tearingSR == 0)
                {
                    Job4_5SOff = true;
                    GameObject s5rc4 = transform.Find("Shoes5 right color5").gameObject;
                    s5rc4.gameObject.SetActive(true);
                }
                else if (tearingSR == 1)
                {
                    Job4_5SOff = true;
                    GameObject s5rc4t = transform.Find("Shoes5 right color5 tear").gameObject;
                    s5rc4t.gameObject.SetActive(true);
                }

                tearingSL = Random.Range(0, 2); //신발 찢어짐

                if (tearingSL == 0)
                {
                    Job4_5SOff = true;
                    GameObject s5lc5 = transform.Find("Shoes5 left color5").gameObject;
                    s5lc5.gameObject.SetActive(true);
                }
                else if (tearingSL == 1)
                {
                    Job4_5SOff = true;
                    GameObject s5lc5t = transform.Find("Shoes5 left color5 tear").gameObject;
                    s5lc5t.gameObject.SetActive(true);
                }

                tearing = Random.Range(0, 2);  //옷 찢어짐

                if (tearing == 0)
                {
                    tearAmounts = Random.Range(0, 6); //옷 찢어진 갯수

                    for (i = 0; i <= tearAmounts; i++)
                    {
                        if (i == 1)
                        {
                            GameObject s3tbcc5_1 = transform.Find("Suit3 top body cloth color5-1").gameObject;
                            s3tbcc5_1.gameObject.SetActive(true);
                        }
                        if (i == 2)
                        {
                            GameObject s3tbcc5_2 = transform.Find("Suit3 top body cloth color5-2").gameObject;
                            s3tbcc5_2.gameObject.SetActive(true);
                        }
                        if (i == 3)
                        {
                            GameObject s3trdacc5 = transform.Find("Suit3 top right down arm cloth color5").gameObject;
                            s3trdacc5.gameObject.SetActive(true);
                            TopDownRclothTear = 5;
                        }
                        if (i == 4)
                        {
                            GameObject s3drcc5 = transform.Find("Suit3 down right cloth color5").gameObject;
                            s3drcc5.gameObject.SetActive(true);
                            DownRclothTear = 5;
                        }
                        if (i == 5)
                        {
                            GameObject s3dlcc5 = transform.Find("Suit3 down left cloth color5").gameObject;
                            s3dlcc5.gameObject.SetActive(true);
                            DownLclothTear = 5;
                        }
                        if (i == 6)
                        {
                            GameObject s3tldacc5 = transform.Find("Suit3 top left down arm cloth color5").gameObject;
                            s3tldacc5.gameObject.SetActive(true);
                            TopDownLclothTear = 5;
                        }
                    }
                }
                else //옷 안 찢어짐
                {
                    GameObject s3tbcc5_1 = transform.Find("Suit3 top body cloth color5-1").gameObject;
                    GameObject s3tbcc5_2 = transform.Find("Suit3 top body cloth color5-2").gameObject;
                    GameObject s3trdacc5 = transform.Find("Suit3 top right down arm cloth color5").gameObject;
                    GameObject s3drcc5 = transform.Find("Suit3 down right cloth color5").gameObject;
                    GameObject s3dlcc5 = transform.Find("Suit3 down left cloth color5").gameObject;
                    GameObject s3tldacc5 = transform.Find("Suit3 top left down arm cloth color5").gameObject;
                    s3tbcc5_1.gameObject.SetActive(true);
                    s3tbcc5_2.gameObject.SetActive(true);
                    s3trdacc5.gameObject.SetActive(true);
                    s3drcc5.gameObject.SetActive(true);
                    s3dlcc5.gameObject.SetActive(true);
                    s3tldacc5.gameObject.SetActive(true);
                    clothTear = 5;
                }
            }

            LED = Random.Range(0, 2); //상의, 하의 LED 활성화

            if (LED == 0)
            {
                LEDcolor = Random.Range(0, 5);

                if (LEDcolor == 0)
                {
                    Job4_1LEDOff = true;
                    GameObject s3trdaL1 = transform.Find("Suit3 top right down arm LED1").gameObject;
                    GameObject s3tldaL1 = transform.Find("Suit3 top left down arm LED1").gameObject;
                    GameObject s3drL1 = transform.Find("Suit3 down right LED1").gameObject;
                    s3trdaL1.gameObject.SetActive(true);
                    s3tldaL1.gameObject.SetActive(true);
                    s3drL1.gameObject.SetActive(true);
                }
                else if (LEDcolor == 1)
                {
                    Job4_2LEDOff = true;
                    GameObject s3trdaL2 = transform.Find("Suit3 top right down arm LED2").gameObject;
                    GameObject s3tldaL2 = transform.Find("Suit3 top left down arm LED2").gameObject;
                    GameObject s3drL2 = transform.Find("Suit3 down right LED2").gameObject;
                    s3trdaL2.gameObject.SetActive(true);
                    s3tldaL2.gameObject.SetActive(true);
                    s3drL2.gameObject.SetActive(true);
                }
                else if (LEDcolor == 2)
                {
                    Job4_3LEDOff = true;
                    GameObject s3trdaL3 = transform.Find("Suit3 top right down arm LED3").gameObject;
                    GameObject s3tldaL3 = transform.Find("Suit3 top left down arm LED3").gameObject;
                    GameObject s3drL3 = transform.Find("Suit3 down right LED3").gameObject;
                    s3trdaL3.gameObject.SetActive(true);
                    s3tldaL3.gameObject.SetActive(true);
                    s3drL3.gameObject.SetActive(true);
                }
                else if (LEDcolor == 3)
                {
                    Job4_4LEDOff = true;
                    GameObject s3trdaL4 = transform.Find("Suit3 top right down arm LED4").gameObject;
                    GameObject s3tldaL4 = transform.Find("Suit3 top left down arm LED4").gameObject;
                    GameObject s3drL4 = transform.Find("Suit3 down right LED4").gameObject;
                    s3trdaL4.gameObject.SetActive(true);
                    s3tldaL4.gameObject.SetActive(true);
                    s3drL4.gameObject.SetActive(true);
                }
                else if (LEDcolor == 4)
                {
                    Job4_5LEDOff = true;
                    GameObject s3trdaL5 = transform.Find("Suit3 top right down arm LED5").gameObject;
                    GameObject s3tldaL5 = transform.Find("Suit3 top left down arm LED5").gameObject;
                    GameObject s3drL5 = transform.Find("Suit3 down right LED5").gameObject;
                    s3trdaL5.gameObject.SetActive(true);
                    s3tldaL5.gameObject.SetActive(true);
                    s3drL5.gameObject.SetActive(true);
                }
            }
        }

        if (tearing == 0 && LED == 0 && inCap == true) //파손된 LED 발생
        {
            Job4LEDRtOff = true;
            LEDBetweenL = Random.Range(0, 2);

            if (LEDcolor == 0)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL1t = transform.Find("Suit3 down left LED1 tear").gameObject;
                    s3dlL1t.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 1)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL2t = transform.Find("Suit3 down left LED2 tear").gameObject;
                    s3dlL2t.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 2)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL3t = transform.Find("Suit3 down left LED3 tear").gameObject;
                    s3dlL3t.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 3)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL4t = transform.Find("Suit3 down left LED4 tear").gameObject;
                    s3dlL4t.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 4)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL5t = transform.Find("Suit3 down left LED5 tear").gameObject;
                    s3dlL5t.gameObject.SetActive(true);
                }
            }
        }
        else if (tearing == 1 && LED == 0 && inCap == true) //멀쩡한 LED 발생
        {
            Job4LEDROff = true;
            LEDBetweenL = Random.Range(0, 2);

            if (LEDcolor == 0)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL1 = transform.Find("Suit3 down left LED1").gameObject;
                    s3dlL1.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 1)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL2 = transform.Find("Suit3 down left LED2").gameObject;
                    s3dlL2.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 2)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL3 = transform.Find("Suit3 down left LED3").gameObject;
                    s3dlL3.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 3)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL4 = transform.Find("Suit3 down left LED4").gameObject;
                    s3dlL4.gameObject.SetActive(true);
                }
            }
            else if (LEDcolor == 4)
            {
                if (LEDBetweenL == 0)
                {
                    GameObject s3dlL5 = transform.Find("Suit3 down left LED5").gameObject;
                    s3dlL5.gameObject.SetActive(true);
                }
            }
        }

        //얼굴 선택
        faceType = Random.Range(0, 5);

        if (faceType == 0)
        {
            GameObject f1head = transform.Find("Face1 head").gameObject;
            GameObject f1eb = transform.Find("Face1 eyeborrow").gameObject;
            GameObject f1e = transform.Find("Face1 eyes").gameObject;
            GameObject f1j = transform.Find("Face1 jaw").gameObject;
            GameObject f1rp = transform.Find("Face1 right pupli").gameObject;
            GameObject f1lp = transform.Find("Face1 left pupli").gameObject;
            f1head.gameObject.SetActive(true);
            f1eb.gameObject.SetActive(true);
            f1e.gameObject.SetActive(true);
            f1j.gameObject.SetActive(true);
            f1rp.gameObject.SetActive(true);
            f1lp.gameObject.SetActive(true);
            Face1Off = true;
        }
        else if (faceType == 1)
        {
            GameObject f2head = transform.Find("Face2 head").gameObject;
            GameObject f2e = transform.Find("Face2 Eye").gameObject;
            GameObject f2p = transform.Find("Face2 pupli").gameObject;
            GameObject f2m = transform.Find("Face2 mask").gameObject;
            f2head.gameObject.SetActive(true);
            f2e.gameObject.SetActive(true);
            f2p.gameObject.SetActive(true);
            f2m.gameObject.SetActive(true);

            Face2Off = true;
            dontGetHair = true;

            GetComponent<InfectorFaceEmotion>().MaskSwing(1); //마스크 흔들림 구현 애니메이션 전달
        }
        else if (faceType == 2)
        {
            GameObject f3head = transform.Find("Face3 head").gameObject;
            GameObject f3eb = transform.Find("Face3 eyeborrow").gameObject;
            GameObject f3e = transform.Find("Face3 eyes").gameObject;
            GameObject f3tm = transform.Find("Face3 Top mouth").gameObject;
            GameObject f3dm = transform.Find("Face3 Down mouth").gameObject;
            GameObject f3p = transform.Find("Face3 pupil").gameObject;
            f3head.gameObject.SetActive(true);
            f3eb.gameObject.SetActive(true);
            f3e.gameObject.SetActive(true);
            f3tm.gameObject.SetActive(true);
            f3dm.gameObject.SetActive(true);
            f3p.gameObject.SetActive(true);
            Face3Off = true;

            GetComponent<InfectorFaceEmotion>().setEmotion(1); //눈썹 고정 애니메이션 전달
            GetComponent<InfectorFaceEmotion>().mouth(1); //입 움직임 애니메이션 전달
        }
        else if (faceType == 3)
        {
            GameObject f5head = transform.Find("Face5 head").gameObject;
            GameObject f5eb = transform.Find("Face5 eyeborrow").gameObject;
            GameObject f5e = transform.Find("Face5 Eyes").gameObject;
            GameObject f5j = transform.Find("Face5 jaw").gameObject;
            GameObject f5rp = transform.Find("Face5 right pupil").gameObject;
            GameObject f5lp = transform.Find("Face5 left pupil").gameObject;
            f5head.gameObject.SetActive(true);
            f5eb.gameObject.SetActive(true);
            f5e.gameObject.SetActive(true);
            f5j.gameObject.SetActive(true);
            f5rp.gameObject.SetActive(true);
            f5lp.gameObject.SetActive(true);
            Face4Off = true;

            GetComponent<InfectorFaceEmotion>().setEmotion(1);
        }
        else
        {
            GameObject f6head = transform.Find("Face6 head").gameObject;
            f6head.gameObject.SetActive(true);

            dontGetHair = true;
            Face5Off = true;

            GetComponent<InfectorFaceEmotion>().SkeletonSwing(1); //해골 얼굴 애니메이션 전달
        }

        //머리카락 선택
        hairtype = Random.Range(0, 5);

        if (hairtype <= 3 && inCap == false && dontGetHair == false)
        {
            hair = Random.Range(0, 7);

            if (hair == 0)
            {
                Hair1Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h1_1 = transform.Find("Hair1-1").gameObject;
                    h1_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h1_2 = transform.Find("Hair1-2").gameObject;
                    h1_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h1_3 = transform.Find("Hair1-3").gameObject;
                    h1_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h1_4 = transform.Find("Hair1-4").gameObject;
                    h1_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h1_5 = transform.Find("Hair1-5").gameObject;
                    h1_5.gameObject.SetActive(true);
                }
            }
            else if (hair == 1)
            {
                Hair2Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h2_1 = transform.Find("Hair2-1").gameObject;
                    h2_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h2_2 = transform.Find("Hair2-2").gameObject;
                    h2_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h2_3 = transform.Find("Hair2-3").gameObject;
                    h2_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h2_4 = transform.Find("Hair2-4").gameObject;
                    h2_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h2_5 = transform.Find("Hair2-5").gameObject;
                    h2_5.gameObject.SetActive(true);
                }
            }
            else if (hair == 2)
            {
                Hair3Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h3_1 = transform.Find("Hair3-1").gameObject;
                    h3_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h3_2 = transform.Find("Hair3-2").gameObject;
                    h3_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h3_3 = transform.Find("Hair3-3").gameObject;
                    h3_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h3_4 = transform.Find("Hair3-4").gameObject;
                    h3_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h3_5 = transform.Find("Hair3-5").gameObject;
                    h3_5.gameObject.SetActive(true);
                }
            }
            else if (hair == 3)
            {
                Hair4Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h4_1 = transform.Find("Hair4-1").gameObject;
                    h4_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h4_2 = transform.Find("Hair4-2").gameObject;
                    h4_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h4_3 = transform.Find("Hair4-3").gameObject;
                    h4_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h4_4 = transform.Find("Hair4-4").gameObject;
                    h4_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h4_5 = transform.Find("Hair4-5").gameObject;
                    h4_5.gameObject.SetActive(true);
                }
            }
            else if (hair == 4)
            {
                Hair5Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h5_1 = transform.Find("Hair5-1").gameObject;
                    h5_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h5_2 = transform.Find("Hair5-2").gameObject;
                    h5_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h5_3 = transform.Find("Hair5-3").gameObject;
                    h5_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h5_4 = transform.Find("Hair5-4").gameObject;
                    h5_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h5_5 = transform.Find("Hair5-5").gameObject;
                    h5_5.gameObject.SetActive(true);
                }
            }
            else
            {
                Hair6Off = true;
                hairColor = Random.Range(0, 5);

                if (hairColor == 0)
                {
                    GameObject h6_1 = transform.Find("Hair6-1").gameObject;
                    h6_1.gameObject.SetActive(true);
                }
                else if (hairColor == 1)
                {
                    GameObject h6_2 = transform.Find("Hair6-2").gameObject;
                    h6_2.gameObject.SetActive(true);
                }
                else if (hairColor == 2)
                {
                    GameObject h6_3 = transform.Find("Hair6-3").gameObject;
                    h6_3.gameObject.SetActive(true);
                }
                else if (hairColor == 3)
                {
                    GameObject h6_4 = transform.Find("Hair6-4").gameObject;
                    h6_4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject h6_5 = transform.Find("Hair6-5").gameObject;
                    h6_5.gameObject.SetActive(true);
                }
            }
        }

        //모자 착용
        if (inCap == true)
        {
            CapOff = true;
            GameObject s3hb = transform.Find("Suit3 head blood").gameObject;
            GameObject s3hi = transform.Find("Suit3 head inner").gameObject;
            s3hb.gameObject.SetActive(true);
            s3hi.gameObject.SetActive(true);

            if (suitsColor == 0)
            {
                GameObject s3hc1 = transform.Find("Suit3 head color1").gameObject;
                s3hc1.gameObject.SetActive(true);
            }
            else if (suitsColor == 1)
            {
                GameObject s3hc2 = transform.Find("Suit3 head color2").gameObject;
                s3hc2.gameObject.SetActive(true);
            }
            else if (suitsColor == 2)
            {
                GameObject s3hc3 = transform.Find("Suit3 head color3").gameObject;
                s3hc3.gameObject.SetActive(true);
            }
            else if (suitsColor == 3)
            {
                GameObject s3hc4 = transform.Find("Suit3 head color4").gameObject;
                s3hc4.gameObject.SetActive(true);
            }
            else
            {
                GameObject s3hc5 = transform.Find("Suit3 head color5").gameObject;
                s3hc5.gameObject.SetActive(true);
            }
        }

        //로고 착용
        logoInput = Random.Range(0, 2);

        if (logoInput == 0)
        {
            logoFrame = Random.Range(0, 5);

            if (logoFrame == 0)
            {
                Logo1_1FOff = true;
                logoFrameColor = Random.Range(0, 5);

                if (logoFrameColor == 0)
                {
                    GameObject lf1c1 = transform.Find("Logo Frame1 color1").gameObject;
                    lf1c1.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 1)
                {
                    GameObject lf1c2 = transform.Find("Logo Frame1 color2").gameObject;
                    lf1c2.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 2)
                {
                    GameObject lf1c3 = transform.Find("Logo Frame1 color3").gameObject;
                    lf1c3.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 3)
                {
                    GameObject lf1c4 = transform.Find("Logo Frame1 color4").gameObject;
                    lf1c4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject lf1c5 = transform.Find("Logo Frame1 color5").gameObject;
                    lf1c5.gameObject.SetActive(true);
                }
            }
            else if (logoFrame == 1)
            {
                Logo1_2FOff = true;
                logoFrameColor = Random.Range(0, 5);

                if (logoFrameColor == 0)
                {
                    GameObject lf2c1 = transform.Find("Logo Frame2 color1").gameObject;
                    lf2c1.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 1)
                {
                    GameObject lf2c2 = transform.Find("Logo Frame2 color2").gameObject;
                    lf2c2.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 2)
                {
                    GameObject lf2c3 = transform.Find("Logo Frame2 color3").gameObject;
                    lf2c3.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 3)
                {
                    GameObject lf2c4 = transform.Find("Logo Frame2 color4").gameObject;
                    lf2c4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject lf2c5 = transform.Find("Logo Frame2 color5").gameObject;
                    lf2c5.gameObject.SetActive(true);
                }
            }
            else if (logoFrame == 2)
            {
                Logo1_3FOff = true;
                logoFrameColor = Random.Range(0, 5);

                if (logoFrameColor == 0)
                {
                    GameObject lf3c1 = transform.Find("Logo Frame3 color1").gameObject;
                    lf3c1.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 1)
                {
                    GameObject lf3c2 = transform.Find("Logo Frame3 color2").gameObject;
                    lf3c2.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 2)
                {
                    GameObject lf3c3 = transform.Find("Logo Frame3 color3").gameObject;
                    lf3c3.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 3)
                {
                    GameObject lf3c4 = transform.Find("Logo Frame3 color4").gameObject;
                    lf3c4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject lf3c5 = transform.Find("Logo Frame3 color5").gameObject;
                    lf3c5.gameObject.SetActive(true);
                }
            }
            else
            {
                Logo1_4FOff = true;
                logoFrameColor = Random.Range(0, 5);

                if (logoFrameColor == 0)
                {
                    GameObject lf4c1 = transform.Find("Logo Frame4 color1").gameObject;
                    lf4c1.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 1)
                {
                    GameObject lf4c2 = transform.Find("Logo Frame4 color2").gameObject;
                    lf4c2.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 2)
                {
                    GameObject lf4c3 = transform.Find("Logo Frame4 color3").gameObject;
                    lf4c3.gameObject.SetActive(true);
                }
                else if (logoFrameColor == 3)
                {
                    GameObject lf4c4 = transform.Find("Logo Frame4 color4").gameObject;
                    lf4c4.gameObject.SetActive(true);
                }
                else
                {
                    GameObject lf4c5 = transform.Find("Logo Frame4 color5").gameObject;
                    lf4c5.gameObject.SetActive(true);
                }
            }

            //로고 디스플레이 착용
            display = Random.Range(0, 4);

            if (display == 0)
            {
                LogoDisplayOff = true;
                GameObject d1 = transform.Find("Display color1").gameObject;
                d1.gameObject.SetActive(true);
            }
            else if (display == 1)
            {
                LogoDisplayOff = true;
                GameObject d2 = transform.Find("Display color2").gameObject;
                d2.gameObject.SetActive(true);
            }
            else if (display == 2)
            {
                LogoDisplayOff = true;
                GameObject d3 = transform.Find("Display color3").gameObject;
                d3.gameObject.SetActive(true);
            }
            else
            {
                LogoDisplayOff = true;
                GameObject d4 = transform.Find("Display color4").gameObject;
                d4.gameObject.SetActive(true);
            }

            //로고 착용
            logo = Random.Range(0, 21);

            if (logo == 0)
            {
                Logo1_1Off = true;
                GameObject l1 = transform.Find("logo1").gameObject;
                GameObject l1d = transform.Find("logo1 down").gameObject;
                l1.gameObject.SetActive(true);
                l1d.gameObject.SetActive(true);
            }
            else if (logo == 1)
            {
                Logo1_2Off = true;
                GameObject l2 = transform.Find("logo2").gameObject;
                GameObject l2d = transform.Find("logo2 down").gameObject;
                l2.gameObject.SetActive(true);
                l2d.gameObject.SetActive(true);
            }
            else if (logo == 2)
            {
                Logo1_3Off = true;
                GameObject l3 = transform.Find("logo3").gameObject;
                GameObject l3d = transform.Find("logo3 down").gameObject;
                l3.gameObject.SetActive(true);
                l3d.gameObject.SetActive(true);
            }
            else if (logo == 3)
            {
                Logo1_4Off = true;
                GameObject l4 = transform.Find("logo4").gameObject;
                GameObject l4d = transform.Find("logo4 down").gameObject;
                l4.gameObject.SetActive(true);
                l4d.gameObject.SetActive(true);
            }
            else if (logo == 4)
            {
                Logo1_5Off = true;
                GameObject l5 = transform.Find("logo5").gameObject;
                GameObject l5d = transform.Find("logo5 down").gameObject;
                l5.gameObject.SetActive(true);
                l5d.gameObject.SetActive(true);
            }
            else if (logo == 5)
            {
                Logo1_6Off = true;
                GameObject l6 = transform.Find("logo6").gameObject;
                GameObject l6d = transform.Find("logo6 down").gameObject;
                l6.gameObject.SetActive(true);
                l6d.gameObject.SetActive(true);
            }
            else if (logo == 6)
            {
                Logo1_7Off = true;
                GameObject l7 = transform.Find("logo7").gameObject;
                GameObject l7d = transform.Find("logo7 down").gameObject;
                l7.gameObject.SetActive(true);
                l7d.gameObject.SetActive(true);
            }
            else if (logo == 7)
            {
                Logo1_8Off = true;
                GameObject l8 = transform.Find("logo8").gameObject;
                GameObject l8d = transform.Find("logo8 down").gameObject;
                l8.gameObject.SetActive(true);
                l8d.gameObject.SetActive(true);
            }
            else if (logo == 8)
            {
                Logo1_9Off = true;
                GameObject l9 = transform.Find("logo9").gameObject;
                GameObject l9d = transform.Find("logo9 down").gameObject;
                l9.gameObject.SetActive(true);
                l9d.gameObject.SetActive(true);
            }
            else if (logo == 9)
            {
                Logo1_10Off = true;
                GameObject l10 = transform.Find("logo10").gameObject;
                GameObject l10d = transform.Find("logo10 down").gameObject;
                l10.gameObject.SetActive(true);
                l10d.gameObject.SetActive(true);
            }
            else if (logo == 10)
            {
                Logo1_11Off = true;
                GameObject l11 = transform.Find("logo11").gameObject;
                GameObject l11d = transform.Find("logo11 down").gameObject;
                l11.gameObject.SetActive(true);
                l11d.gameObject.SetActive(true);
            }
            else if (logo == 11)
            {
                Logo1_12Off = true;
                GameObject l12 = transform.Find("logo12").gameObject;
                GameObject l12d = transform.Find("logo12 down").gameObject;
                l12.gameObject.SetActive(true);
                l12d.gameObject.SetActive(true);
            }
            else if (logo == 12)
            {
                Logo1_13Off = true;
                GameObject l13 = transform.Find("logo13").gameObject;
                GameObject l13d = transform.Find("logo13 down").gameObject;
                l13.gameObject.SetActive(true);
                l13d.gameObject.SetActive(true);
            }
            else if (logo == 13)
            {
                Logo1_14Off = true;
                GameObject l14 = transform.Find("logo14").gameObject;
                GameObject l14d = transform.Find("logo14 down").gameObject;
                l14.gameObject.SetActive(true);
                l14d.gameObject.SetActive(true);
            }
            else if (logo == 14)
            {
                Logo1_15Off = true;
                GameObject l15 = transform.Find("logo15").gameObject;
                GameObject l15d = transform.Find("logo15 down").gameObject;
                l15.gameObject.SetActive(true);
                l15d.gameObject.SetActive(true);
            }
            else if (logo == 15)
            {
                Logo1_16Off = true;
                GameObject l16 = transform.Find("logo16").gameObject;
                GameObject l16d = transform.Find("logo16 down").gameObject;
                l16.gameObject.SetActive(true);
                l16d.gameObject.SetActive(true);
            }
            else if (logo == 16)
            {
                Logo1_17Off = true;
                GameObject l17 = transform.Find("logo17").gameObject;
                GameObject l17d = transform.Find("logo17 down").gameObject;
                l17.gameObject.SetActive(true);
                l17d.gameObject.SetActive(true);
            }
            else if (logo == 17)
            {
                Logo1_18Off = true;
                GameObject l18 = transform.Find("logo18").gameObject;
                GameObject l18d = transform.Find("logo18 down").gameObject;
                l18.gameObject.SetActive(true);
                l18d.gameObject.SetActive(true);
            }
            else if (logo == 18)
            {
                Logo1_19Off = true;
                GameObject l19 = transform.Find("logo19").gameObject;
                GameObject l19d = transform.Find("logo19 down").gameObject;
                l19.gameObject.SetActive(true);
                l19d.gameObject.SetActive(true);
            }
            else if (logo == 19)
            {
                Logo1_20Off = true;
                GameObject l20 = transform.Find("logo20").gameObject;
                GameObject l20d = transform.Find("logo20 down").gameObject;
                l20.gameObject.SetActive(true);
                l20d.gameObject.SetActive(true);
            }
            else if (logo == 20)
            {
                Logo1_21Off = true;
                GameObject l21 = transform.Find("logo21").gameObject;
                GameObject l21d = transform.Find("logo21 down").gameObject;
                l21.gameObject.SetActive(true);
                l21d.gameObject.SetActive(true);
            }
        }
    }

    void InitializeBody()
    {
        //몸
        if (Body1Off == true)
        {
            Body1Off = false;
            GameObject body1 = transform.Find("Body1 body").gameObject;
            GameObject body1lda = transform.Find("Body1 left down arm").gameObject;
            GameObject body1ll = transform.Find("Body1 left leg").gameObject;
            GameObject body1lua = transform.Find("Body1 left up arm").gameObject;
            GameObject body1rdr = transform.Find("Body1 right down arm").gameObject;
            GameObject body1rl = transform.Find("Body1 right leg").gameObject;
            GameObject body1rua = transform.Find("Body1 right up arm").gameObject;
            GameObject footL = transform.Find("Left foot1").gameObject;
            GameObject footR = transform.Find("Right foot1").gameObject;
            body1.gameObject.SetActive(false);
            body1lda.gameObject.SetActive(false);
            body1ll.gameObject.SetActive(false);
            body1lua.gameObject.SetActive(false);
            body1rdr.gameObject.SetActive(false);
            body1rl.gameObject.SetActive(false);
            body1rua.gameObject.SetActive(false);
            footL.gameObject.SetActive(false);
            footR.gameObject.SetActive(false);
        }
        else if (Body2Off == true)
        {
            Body2Off = false;
            GameObject body2 = transform.Find("Body2 body").gameObject;
            GameObject body2lda = transform.Find("Body2 left down arm").gameObject;
            GameObject body2ll = transform.Find("Body2 left leg").gameObject;
            GameObject body2lua = transform.Find("Body2 left up arm").gameObject;
            GameObject body2rdr = transform.Find("Body2 right down arm").gameObject;
            GameObject body2rl = transform.Find("Body2 right leg").gameObject;
            GameObject body2rua = transform.Find("Body2 right up arm").gameObject;
            GameObject footL = transform.Find("Left foot1").gameObject;
            GameObject footR = transform.Find("Right foot1").gameObject;
            body2.gameObject.SetActive(false);
            body2lda.gameObject.SetActive(false);
            body2ll.gameObject.SetActive(false);
            body2lua.gameObject.SetActive(false);
            body2rdr.gameObject.SetActive(false);
            body2rl.gameObject.SetActive(false);
            body2rua.gameObject.SetActive(false);
            footL.gameObject.SetActive(false);
            footR.gameObject.SetActive(false);
        }

        //손
        if (HandOff == true)
        {
            HandOff = false;
            GameObject hand1r = transform.Find("Hand1 right").gameObject;
            GameObject hand1l = transform.Find("Hand1 left").gameObject;
            hand1r.gameObject.SetActive(false);
            hand1l.gameObject.SetActive(false);
            GameObject hand2r = transform.Find("Hand2 right").gameObject;
            GameObject hand2l = transform.Find("Hand2 left").gameObject;
            hand2r.gameObject.SetActive(false);
            hand2l.gameObject.SetActive(false);
            GameObject hand3r = transform.Find("Hand3 right").gameObject;
            GameObject hand3l = transform.Find("Hand3 left").gameObject;
            hand3r.gameObject.SetActive(false);
            hand3l.gameObject.SetActive(false);
        }

        //일반인
        if (Job1Off == true)
        {
            Job1Off = false;
            GameObject ct1rua = transform.Find("Clothes top1 right up arm").gameObject;
            GameObject ct1rda = transform.Find("Clothes top1 right down arm").gameObject;
            GameObject ct1lua = transform.Find("Clothes top1 left up arm").gameObject;
            GameObject ct1lda = transform.Find("Clothes top1 left down arm").gameObject;
            ct1rua.gameObject.SetActive(false);
            ct1rda.gameObject.SetActive(false);
            ct1lua.gameObject.SetActive(false);
            ct1lda.gameObject.SetActive(false);
        }

        if (Job1_1Off == true)
        {
            Job1_1Off = false;
            GameObject ct1bc1 = transform.Find("Clothes top1 body color1").gameObject;
            ct1bc1.gameObject.SetActive(false);
            GameObject ct1bcc1_1 = transform.Find("Clothes top1 body cloth color1-1").gameObject;
            ct1bcc1_1.gameObject.SetActive(false);
            GameObject ct1bcc1_2 = transform.Find("Clothes top1 body cloth color1-2").gameObject;
            ct1bcc1_2.gameObject.SetActive(false);
            GameObject ct1bcc1_3 = transform.Find("Clothes top1 body cloth color1-3").gameObject;
            ct1bcc1_3.gameObject.SetActive(false);
            GameObject ct1bcc1_4 = transform.Find("Clothes top1 body cloth color1-4").gameObject;
            ct1bcc1_4.gameObject.SetActive(true);
        }
        else if (Job1_2Off == true)
        {
            Job1_2Off = false;
            GameObject ct1bc2 = transform.Find("Clothes top1 body color2").gameObject;
            ct1bc2.gameObject.SetActive(false);
            GameObject ct1bcc2_1 = transform.Find("Clothes top1 body cloth color2-1").gameObject;
            ct1bcc2_1.gameObject.SetActive(false);
            GameObject ct1bcc2_2 = transform.Find("Clothes top1 body cloth color2-2").gameObject;
            ct1bcc2_2.gameObject.SetActive(false);
            GameObject ct1bcc2_3 = transform.Find("Clothes top1 body cloth color2-3").gameObject;
            ct1bcc2_3.gameObject.SetActive(false);
            GameObject ct1bcc2_4 = transform.Find("Clothes top1 body cloth color2-4").gameObject;
            ct1bcc2_4.gameObject.SetActive(false);
        }
        else if (Job1_3Off == true)
        {
            Job1_3Off = false;
            GameObject ct1bc3 = transform.Find("Clothes top1 body color3").gameObject;
            ct1bc3.gameObject.SetActive(false);
            GameObject ct1bcc3_1 = transform.Find("Clothes top1 body cloth color3-1").gameObject;
            ct1bcc3_1.gameObject.SetActive(false);
            GameObject ct1bcc3_2 = transform.Find("Clothes top1 body cloth color3-2").gameObject;
            ct1bcc3_2.gameObject.SetActive(false);
            GameObject ct1bcc3_3 = transform.Find("Clothes top1 body cloth color3-3").gameObject;
            ct1bcc3_3.gameObject.SetActive(false);
            GameObject ct1bcc3_4 = transform.Find("Clothes top1 body cloth color3-4").gameObject;
            ct1bcc3_4.gameObject.SetActive(false);
        }
        else if (Job1_4Off == true)
        {
            Job1_4Off = false;
            GameObject ct1bc4 = transform.Find("Clothes top1 body color4").gameObject;
            ct1bc4.gameObject.SetActive(false);
            GameObject ct1bcc4_1 = transform.Find("Clothes top1 body cloth color4-1").gameObject;
            ct1bcc4_1.gameObject.SetActive(false);
            GameObject ct1bcc4_2 = transform.Find("Clothes top1 body cloth color4-2").gameObject;
            ct1bcc4_2.gameObject.SetActive(false);
            GameObject ct1bcc4_3 = transform.Find("Clothes top1 body cloth color4-3").gameObject;
            ct1bcc4_3.gameObject.SetActive(false);
            GameObject ct1bcc4_4 = transform.Find("Clothes top1 body cloth color4-4").gameObject;
            ct1bcc4_4.gameObject.SetActive(false);
        }

        if (Job1LEDtOff == true)
        {
            Job1LEDtOff = false;
            GameObject ct1bL1t = transform.Find("Clothes top1 body LED1 tear").gameObject;
            ct1bL1t.gameObject.SetActive(false);
            GameObject ct1bL2t = transform.Find("Clothes top1 body LED2 tear").gameObject;
            ct1bL2t.gameObject.SetActive(false);
            GameObject ct1bL3t = transform.Find("Clothes top1 body LED3 tear").gameObject;
            ct1bL3t.gameObject.SetActive(false);
            GameObject ct1bL4t = transform.Find("Clothes top1 body LED4 tear").gameObject;
            ct1bL4t.gameObject.SetActive(false);
            GameObject ct1bL5t = transform.Find("Clothes top1 body LED5 tear").gameObject;
            ct1bL5t.gameObject.SetActive(false);
        }

        if (Job1LEDOff == true)
        {
            Job1LEDOff = false;
            GameObject ct1bL1 = transform.Find("Clothes top1 body LED1").gameObject;
            ct1bL1.gameObject.SetActive(false);
            GameObject ct1bL2 = transform.Find("Clothes top1 body LED2").gameObject;
            ct1bL2.gameObject.SetActive(false);
            GameObject ct1bL3 = transform.Find("Clothes top1 body LED3").gameObject;
            ct1bL3.gameObject.SetActive(false);
            GameObject ct1bL4 = transform.Find("Clothes top1 body LED4").gameObject;
            ct1bL4.gameObject.SetActive(false);
            GameObject ct1bL5 = transform.Find("Clothes top1 body LED5").gameObject;
            ct1bL5.gameObject.SetActive(false);
        }

        if (Job1SOff == true)
        {
            Job1SOff = false;
            GameObject ct2ra = transform.Find("Clothes top2 right arm").gameObject;
            GameObject ct2la = transform.Find("Clothes top2 left arm").gameObject;
            ct2ra.gameObject.SetActive(false);
            ct2la.gameObject.SetActive(false);
        }

        if (Job1_1SOff == true)
        {
            Job1_1SOff = false;
            GameObject ct2bc1 = transform.Find("Clothes top2 body color1").gameObject;
            ct2bc1.gameObject.SetActive(false);
            GameObject ct2bcc1_1 = transform.Find("Clothes top2 body cloth color1-1").gameObject;
            ct2bcc1_1.gameObject.SetActive(false);
            GameObject ct2bcc1_2 = transform.Find("Clothes top2 body cloth color1-2").gameObject;
            ct2bcc1_2.gameObject.SetActive(false);
            GameObject ct2bcc1_3 = transform.Find("Clothes top2 body cloth color1-3").gameObject;
            ct2bcc1_3.gameObject.SetActive(false);
            GameObject ct2bcc1_4 = transform.Find("Clothes top2 body cloth color1-4").gameObject;
            ct2bcc1_4.gameObject.SetActive(false);
        }
        else if (Job1_2SOff == true)
        {
            Job1_2SOff = false;
            GameObject ct2bc2 = transform.Find("Clothes top2 body color2").gameObject;
            ct2bc2.gameObject.SetActive(false);
            GameObject ct2bcc2_1 = transform.Find("Clothes top2 body cloth color2-1").gameObject;
            ct2bcc2_1.gameObject.SetActive(false);
            GameObject ct2bcc2_2 = transform.Find("Clothes top2 body cloth color2-2").gameObject;
            ct2bcc2_2.gameObject.SetActive(false);
            GameObject ct2bcc2_3 = transform.Find("Clothes top2 body cloth color2-3").gameObject;
            ct2bcc2_3.gameObject.SetActive(false);
            GameObject ct2bcc2_4 = transform.Find("Clothes top2 body cloth color2-4").gameObject;
            ct2bcc2_4.gameObject.SetActive(false);
        }
        else if (Job1_3SOff == true)
        {
            Job1_3SOff = false;
            GameObject ct2bc3 = transform.Find("Clothes top2 body color3").gameObject;
            ct2bc3.gameObject.SetActive(false);
            GameObject ct2bcc3_1 = transform.Find("Clothes top2 body cloth color3-1").gameObject;
            ct2bcc3_1.gameObject.SetActive(false);
            GameObject ct2bcc3_2 = transform.Find("Clothes top2 body cloth color3-2").gameObject;
            ct2bcc3_2.gameObject.SetActive(false);
            GameObject ct2bcc3_3 = transform.Find("Clothes top2 body cloth color3-3").gameObject;
            ct2bcc3_3.gameObject.SetActive(false);
            GameObject ct2bcc3_4 = transform.Find("Clothes top2 body cloth color3-4").gameObject;
            ct2bcc3_4.gameObject.SetActive(false);
        }
        else if (Job1_4SOff == true)
        {
            Job1_4SOff = false;
            GameObject ct2bc4 = transform.Find("Clothes top2 body color4").gameObject;
            ct2bc4.gameObject.SetActive(false);
            GameObject ct2bcc4_1 = transform.Find("Clothes top2 body cloth color4-1").gameObject;
            ct2bcc4_1.gameObject.SetActive(false);
            GameObject ct2bcc4_2 = transform.Find("Clothes top2 body cloth color4-2").gameObject;
            ct2bcc4_2.gameObject.SetActive(false);
            GameObject ct2bcc4_3 = transform.Find("Clothes top2 body cloth color4-3").gameObject;
            ct2bcc4_3.gameObject.SetActive(false);
            GameObject ct2bcc4_4 = transform.Find("Clothes top2 body cloth color4-4").gameObject;
            ct2bcc4_4.gameObject.SetActive(false);
        }

        if (Job1SLEDOff == true)
        {
            Job1SLEDOff = false;
            GameObject ct2bL1 = transform.Find("Clothes top2 body LED1").gameObject;
            ct2bL1.gameObject.SetActive(false);
            GameObject ct2bL2 = transform.Find("Clothes top2 body LED2").gameObject;
            ct2bL2.gameObject.SetActive(false);
            GameObject ct2bL3 = transform.Find("Clothes top2 body LED3").gameObject;
            ct2bL3.gameObject.SetActive(false);
            GameObject ct2bL4 = transform.Find("Clothes top2 body LED4").gameObject;
            ct2bL4.gameObject.SetActive(false);
            GameObject ct2bL5 = transform.Find("Clothes top2 body LED5").gameObject;
            ct2bL5.gameObject.SetActive(false);
        }

        if (Job1_1POff == true)
        {
            Job1_1POff = false;
            GameObject p1rc1 = transform.Find("Pants1 right color1").gameObject;
            GameObject p1lc1 = transform.Find("Pants1 left color1").gameObject;
            p1rc1.gameObject.SetActive(false);
            p1lc1.gameObject.SetActive(false);
            GameObject p1rcc1 = transform.Find("Pants1 right cloth color1").gameObject;
            p1rcc1.gameObject.SetActive(false);
            GameObject p1lcc1 = transform.Find("Pants1 left cloth color1").gameObject;
            p1lcc1.gameObject.SetActive(false);
        }
        else if (Job1_2POff == true)
        {
            Job1_2POff = false;
            GameObject p1rc2 = transform.Find("Pants1 right color2").gameObject;
            GameObject p1lc2 = transform.Find("Pants1 left color2").gameObject;
            p1rc2.gameObject.SetActive(false);
            p1lc2.gameObject.SetActive(false);
            GameObject p1rcc2 = transform.Find("Pants1 right cloth color2").gameObject;
            p1rcc2.gameObject.SetActive(false);
            GameObject p1lcc2 = transform.Find("Pants1 left cloth color2").gameObject;
            p1lcc2.gameObject.SetActive(false);
        }
        else if (Job1_3POff == true)
        {
            Job1_3POff = false;
            GameObject p1rc3 = transform.Find("Pants1 right color3").gameObject;
            GameObject p1lc3 = transform.Find("Pants1 left color3").gameObject;
            p1rc3.gameObject.SetActive(false);
            p1lc3.gameObject.SetActive(false);
            GameObject p1rcc3 = transform.Find("Pants1 right cloth color3").gameObject;
            p1rcc3.gameObject.SetActive(false);
            GameObject p1lcc3 = transform.Find("Pants1 left cloth color3").gameObject;
            p1lcc3.gameObject.SetActive(false);
        }
        else if (Job1_4POff == true)
        {
            Job1_4POff = false;
            GameObject p1rc4 = transform.Find("Pants1 right color4").gameObject;
            GameObject p1lc4 = transform.Find("Pants1 left color4").gameObject;
            p1rc4.gameObject.SetActive(false);
            p1lc4.gameObject.SetActive(false);
            GameObject p1rcc4 = transform.Find("Pants1 right cloth color4").gameObject;
            p1rcc4.gameObject.SetActive(false);
            GameObject p1lcc4 = transform.Find("Pants1 left cloth color4").gameObject;
            p1lcc4.gameObject.SetActive(false);
        }

        if (Job1PLEDOff == true)
        {
            Job1PLEDOff = false;
            GameObject p1rL1 = transform.Find("Pants1 right LED1").gameObject;
            p1rL1.gameObject.SetActive(false);
            GameObject p1rL2 = transform.Find("Pants1 right LED2").gameObject;
            p1rL2.gameObject.SetActive(false);
            GameObject p1rL3 = transform.Find("Pants1 right LED3").gameObject;
            p1rL3.gameObject.SetActive(false);
            GameObject p1rL4 = transform.Find("Pants1 right LED4").gameObject;
            p1rL4.gameObject.SetActive(false);
            GameObject p1rL5 = transform.Find("Pants1 right LED5").gameObject;
            p1rL5.gameObject.SetActive(false);
        }

        if (Job1_1SPOff == true)
        {
            Job1_1SPOff = false;
            GameObject p2rc1 = transform.Find("Pants2 right color1").gameObject;
            GameObject p2lc1 = transform.Find("Pants2 left color1").gameObject;
            p2rc1.gameObject.SetActive(false);
            p2lc1.gameObject.SetActive(false);
            GameObject p2rcc1 = transform.Find("Pants2 right cloth color1").gameObject;
            p2rcc1.gameObject.SetActive(false);
            GameObject p2lcc1 = transform.Find("Pants2 left cloth color1").gameObject;
            p2lcc1.gameObject.SetActive(false);
        }
        else if (Job1_2SPOff == true)
        {
            Job1_2SPOff = false;
            GameObject p2rc2 = transform.Find("Pants2 right color2").gameObject;
            GameObject p2lc2 = transform.Find("Pants2 left color2").gameObject;
            p2rc2.gameObject.SetActive(false);
            p2lc2.gameObject.SetActive(false);
            GameObject p2rcc2 = transform.Find("Pants2 right cloth color2").gameObject;
            p2rcc2.gameObject.SetActive(false);
            GameObject p2lcc2 = transform.Find("Pants2 left cloth color2").gameObject;
            p2lcc2.gameObject.SetActive(false);
        }
        else if (Job1_3SPOff == true)
        {
            Job1_3SPOff = false;
            GameObject p2rc3 = transform.Find("Pants2 right color3").gameObject;
            GameObject p2lc3 = transform.Find("Pants2 left color3").gameObject;
            p2rc3.gameObject.SetActive(false);
            p2lc3.gameObject.SetActive(false);
            GameObject p2rcc3 = transform.Find("Pants2 right cloth color3").gameObject;
            p2rcc3.gameObject.SetActive(false);
            GameObject p2lcc3 = transform.Find("Pants2 left cloth color3").gameObject;
            p2lcc3.gameObject.SetActive(false);
        }
        else if (Job1_4SPOff == true)
        {
            Job1_4SPOff = false;
            GameObject p2rc4 = transform.Find("Pants2 right color4").gameObject;
            GameObject p2lc4 = transform.Find("Pants2 left color4").gameObject;
            p2rc4.gameObject.SetActive(false);
            p2lc4.gameObject.SetActive(false);
            GameObject p2rcc4 = transform.Find("Pants2 right cloth color4").gameObject;
            p2rcc4.gameObject.SetActive(false);
            GameObject p2lcc4 = transform.Find("Pants2 left cloth color4").gameObject;
            p2lcc4.gameObject.SetActive(false);
        }

        if (Job1SPLEDROff == true)
        {
            Job1SPLEDROff = false;
            GameObject p2rL1 = transform.Find("Pants2 right LED1").gameObject;
            p2rL1.gameObject.SetActive(false);
            GameObject p2rL2 = transform.Find("Pants2 right LED2").gameObject;
            p2rL2.gameObject.SetActive(false);
            GameObject p2rL3 = transform.Find("Pants2 right LED3").gameObject;
            p2rL3.gameObject.SetActive(false);
            GameObject p2rL4 = transform.Find("Pants2 right LED4").gameObject;
            p2rL4.gameObject.SetActive(false);
            GameObject p2rL5 = transform.Find("Pants2 right LED5").gameObject;
            p2rL5.gameObject.SetActive(false);
        }

        if (Job1SPLEDLOff == true)
        {
            Job1SPLEDLOff = false;
            GameObject p2lL1 = transform.Find("Pants2 left LED1").gameObject;
            p2lL1.gameObject.SetActive(false);
            GameObject p2lL2 = transform.Find("Pants2 left LED2").gameObject;
            p2lL2.gameObject.SetActive(false);
            GameObject p2lL3 = transform.Find("Pants2 left LED3").gameObject;
            p2lL3.gameObject.SetActive(false);
            GameObject p2lL4 = transform.Find("Pants2 left LED4").gameObject;
            p2lL4.gameObject.SetActive(false);
            GameObject p2lL5 = transform.Find("Pants2 left LED5").gameObject;
            p2lL5.gameObject.SetActive(false);
        }

        if (Job1_1ShoesOff == true)
        {
            Job1_1ShoesOff = false;
            GameObject s1rc1 = transform.Find("Shoes1 right color1").gameObject;
            s1rc1.gameObject.SetActive(false);
            GameObject s1rc1t = transform.Find("Shoes1 right color1 tear").gameObject;
            s1rc1t.gameObject.SetActive(false);
            GameObject s1lc1 = transform.Find("Shoes1 left color1").gameObject;
            s1lc1.gameObject.SetActive(false);
            GameObject s1lc1t = transform.Find("Shoes1 left color1 tear").gameObject;
            s1lc1t.gameObject.SetActive(false);
        }
        else if (Job1_2ShoesOff == true)
        {
            Job1_2ShoesOff = false;
            GameObject s1rc2 = transform.Find("Shoes1 right color2").gameObject;
            s1rc2.gameObject.SetActive(false);
            GameObject s1rc2t = transform.Find("Shoes1 right color2 tear").gameObject;
            s1rc2t.gameObject.SetActive(false);
            GameObject s1lc2 = transform.Find("Shoes1 left color2").gameObject;
            s1lc2.gameObject.SetActive(false);
            GameObject s1lc2t = transform.Find("Shoes1 left color2 tear").gameObject;
            s1lc2t.gameObject.SetActive(false);
        }
        else if (Job1_3ShoesOff == true)
        {
            Job1_3ShoesOff = false;
            GameObject s1rc3 = transform.Find("Shoes1 right color3").gameObject;
            s1rc3.gameObject.SetActive(false);
            GameObject s1rc3t = transform.Find("Shoes1 right color3 tear").gameObject;
            s1rc3t.gameObject.SetActive(false);
            GameObject s1lc3 = transform.Find("Shoes1 left color3").gameObject;
            s1lc3.gameObject.SetActive(false);
            GameObject s1lc3t = transform.Find("Shoes1 left color3 tear").gameObject;
            s1lc3t.gameObject.SetActive(false);
        }
        else if (Job1_4ShoesOff == true)
        {
            Job1_4ShoesOff = false;
            GameObject s1rc4 = transform.Find("Shoes1 right color4").gameObject;
            s1rc4.gameObject.SetActive(false);
            GameObject s1rc4t = transform.Find("Shoes1 right color4 tear").gameObject;
            s1rc4t.gameObject.SetActive(false);
            GameObject s1lc4 = transform.Find("Shoes1 left color4").gameObject;
            s1lc4.gameObject.SetActive(false);
            GameObject s1lc4t = transform.Find("Shoes1 left color4 tear").gameObject;
            s1lc4t.gameObject.SetActive(false);
        }
        else if (Job1_5ShoesOff == true)
        {
            Job1_5ShoesOff = false;
            GameObject s1rc5 = transform.Find("Shoes1 right color5").gameObject;
            s1rc5.gameObject.SetActive(false);
            GameObject s1rc5t = transform.Find("Shoes1 right color5 tear").gameObject;
            s1rc5t.gameObject.SetActive(false);
            GameObject s1lc5 = transform.Find("Shoes1 left color5").gameObject;
            s1lc5.gameObject.SetActive(false);
            GameObject s1lc5t = transform.Find("Shoes1 left color5 tear").gameObject;
            s1lc5t.gameObject.SetActive(false);
        }

        if (Job1_1SShoesOff == true)
        {
            Job1_1SShoesOff = false;
            GameObject s2rc1 = transform.Find("Shoes2 right color1").gameObject;
            s2rc1.gameObject.SetActive(false);
            GameObject s2lc1 = transform.Find("Shoes2 left color1").gameObject;
            s2lc1.gameObject.SetActive(false);
        }
        else if (Job1_2SShoesOff == true)
        {
            Job1_2SShoesOff = false;
            GameObject s2rc2 = transform.Find("Shoes2 right color2").gameObject;
            s2rc2.gameObject.SetActive(false);
            GameObject s2lc2 = transform.Find("Shoes2 left color2").gameObject;
            s2lc2.gameObject.SetActive(false);
        }
        else if (Job1_3SShoesOff == true)
        {
            Job1_3SShoesOff = false;
            GameObject s2rc3 = transform.Find("Shoes2 right color3").gameObject;
            s2rc3.gameObject.SetActive(false);
            GameObject s2lc3 = transform.Find("Shoes2 left color3").gameObject;
            s2lc3.gameObject.SetActive(false);
        }
        else if (Job1_4SShoesOff == true)
        {
            Job1_4SShoesOff = false;
            GameObject s2rc4 = transform.Find("Shoes2 right color4").gameObject;
            s2rc4.gameObject.SetActive(false);
            GameObject s2lc4 = transform.Find("Shoes2 left color4").gameObject;
            s2lc4.gameObject.SetActive(false);
        }
        else if (Job1_5SShoesOff == true)
        {
            Job1_5SShoesOff = false;
            GameObject s2rc5 = transform.Find("Shoes2 right color5").gameObject;
            s2rc5.gameObject.SetActive(false);
            GameObject s2lc5 = transform.Find("Shoes2 left color5").gameObject;
            s2lc5.gameObject.SetActive(false);
        }

        //회사원
        if (Job2_1Off == true)
        {
            Job2_1Off = false;
            GameObject s1tbc1 = transform.Find("Suit1 top body color1").gameObject;
            GameObject s1truac1 = transform.Find("Suit1 top right up arm color1").gameObject;
            GameObject s1trdac1 = transform.Find("Suit1 top right down arm color1").gameObject;
            GameObject s1tluac1 = transform.Find("Suit1 top left up arm color1").gameObject;
            GameObject s1tldac1 = transform.Find("Suit1 top left down arm color1").gameObject;
            GameObject s1drc1 = transform.Find("Suit1 down right color1").gameObject;
            GameObject s1dlc1 = transform.Find("Suit1 down left color1").gameObject;
            s1tbc1.gameObject.SetActive(false);
            s1truac1.gameObject.SetActive(false);
            s1trdac1.gameObject.SetActive(false);
            s1tluac1.gameObject.SetActive(false);
            s1tldac1.gameObject.SetActive(false);
            s1drc1.gameObject.SetActive(false);
            s1dlc1.gameObject.SetActive(false);

            GameObject s1tbcc1_1 = transform.Find("Suit1 top body cloth color1-1").gameObject;
            s1tbcc1_1.gameObject.SetActive(false);
            GameObject s1tbcc1_2 = transform.Find("Suit1 top body cloth color1-2").gameObject;
            s1tbcc1_2.gameObject.SetActive(false);
            GameObject s1tbcc1_3 = transform.Find("Suit1 top body cloth color1-3").gameObject;
            s1tbcc1_3.gameObject.SetActive(false);
            GameObject s1drcc1 = transform.Find("Suit1 down right cloth color1").gameObject;
            s1drcc1.gameObject.SetActive(false);
            GameObject s1dlcc1 = transform.Find("Suit1 down left cloth color1").gameObject;
            s1dlcc1.gameObject.SetActive(false);
        }
        else if (Job2_2Off == true)
        {
            Job2_2Off = false;
            GameObject s1tbc2 = transform.Find("Suit1 top body color2").gameObject;
            GameObject s1truac2 = transform.Find("Suit1 top right up arm color2").gameObject;
            GameObject s1trdac2 = transform.Find("Suit1 top right down arm color2").gameObject;
            GameObject s1tluac2 = transform.Find("Suit1 top left up arm color2").gameObject;
            GameObject s1tldac2 = transform.Find("Suit1 top left down arm color2").gameObject;
            GameObject s1drc2 = transform.Find("Suit1 down right color2").gameObject;
            GameObject s1dlc2 = transform.Find("Suit1 down left color2").gameObject;
            s1tbc2.gameObject.SetActive(false);
            s1truac2.gameObject.SetActive(false);
            s1trdac2.gameObject.SetActive(false);
            s1tluac2.gameObject.SetActive(false);
            s1tldac2.gameObject.SetActive(false);
            s1drc2.gameObject.SetActive(false);
            s1dlc2.gameObject.SetActive(false);

            GameObject s1tbcc2_1 = transform.Find("Suit1 top body cloth color2-1").gameObject;
            s1tbcc2_1.gameObject.SetActive(false);
            GameObject s1tbcc2_2 = transform.Find("Suit1 top body cloth color2-2").gameObject;
            s1tbcc2_2.gameObject.SetActive(false);
            GameObject s1tbcc2_3 = transform.Find("Suit1 top body cloth color2-3").gameObject;
            s1tbcc2_3.gameObject.SetActive(false);
            GameObject s1drcc2 = transform.Find("Suit1 down right cloth color2").gameObject;
            s1drcc2.gameObject.SetActive(false);
            GameObject s1dlcc2 = transform.Find("Suit1 down left cloth color2").gameObject;
            s1dlcc2.gameObject.SetActive(false);
        }
        else if (Job2_3Off == true)
        {
            Job2_3Off = false;
            GameObject s1tbc3 = transform.Find("Suit1 top body color3").gameObject;
            GameObject s1truac3 = transform.Find("Suit1 top right up arm color3").gameObject;
            GameObject s1trdac3 = transform.Find("Suit1 top right down arm color3").gameObject;
            GameObject s1tluac3 = transform.Find("Suit1 top left up arm color3").gameObject;
            GameObject s1tldac3 = transform.Find("Suit1 top left down arm color3").gameObject;
            GameObject s1drc3 = transform.Find("Suit1 down right color3").gameObject;
            GameObject s1dlc3 = transform.Find("Suit1 down left color3").gameObject;
            s1tbc3.gameObject.SetActive(false);
            s1truac3.gameObject.SetActive(false);
            s1trdac3.gameObject.SetActive(false);
            s1tluac3.gameObject.SetActive(false);
            s1tldac3.gameObject.SetActive(false);
            s1drc3.gameObject.SetActive(false);
            s1dlc3.gameObject.SetActive(false);

            GameObject s1tbcc3_1 = transform.Find("Suit1 top body cloth color3-1").gameObject;
            s1tbcc3_1.gameObject.SetActive(false);
            GameObject s1tbcc3_2 = transform.Find("Suit1 top body cloth color3-2").gameObject;
            s1tbcc3_2.gameObject.SetActive(false);
            GameObject s1tbcc3_3 = transform.Find("Suit1 top body cloth color3-3").gameObject;
            s1tbcc3_3.gameObject.SetActive(false);
            GameObject s1drcc3 = transform.Find("Suit1 down right cloth color3").gameObject;
            s1drcc3.gameObject.SetActive(false);
            GameObject s1dlcc3 = transform.Find("Suit1 down left cloth color3").gameObject;
            s1dlcc3.gameObject.SetActive(false);
        }
        else if (Job2_4Off == true)
        {
            Job2_4Off = false;
            GameObject s1tbc4 = transform.Find("Suit1 top body color4").gameObject;
            GameObject s1truac4 = transform.Find("Suit1 top right up arm color4").gameObject;
            GameObject s1trdac4 = transform.Find("Suit1 top right down arm color4").gameObject;
            GameObject s1tluac4 = transform.Find("Suit1 top left up arm color4").gameObject;
            GameObject s1tldac4 = transform.Find("Suit1 top left down arm color4").gameObject;
            GameObject s1drc4 = transform.Find("Suit1 down right color4").gameObject;
            GameObject s1dlc4 = transform.Find("Suit1 down left color4").gameObject;
            s1tbc4.gameObject.SetActive(false);
            s1truac4.gameObject.SetActive(false);
            s1trdac4.gameObject.SetActive(false);
            s1tluac4.gameObject.SetActive(false);
            s1tldac4.gameObject.SetActive(false);
            s1drc4.gameObject.SetActive(false);
            s1dlc4.gameObject.SetActive(false);

            GameObject s1tbcc4_1 = transform.Find("Suit1 top body cloth color4-1").gameObject;
            s1tbcc4_1.gameObject.SetActive(false);
            GameObject s1tbcc4_2 = transform.Find("Suit1 top body cloth color4-2").gameObject;
            s1tbcc4_2.gameObject.SetActive(false);
            GameObject s1tbcc4_3 = transform.Find("Suit1 top body cloth color4-3").gameObject;
            s1tbcc4_3.gameObject.SetActive(false);
            GameObject s1drcc4 = transform.Find("Suit1 down right cloth color4").gameObject;
            s1drcc4.gameObject.SetActive(false);
            GameObject s1dlcc4 = transform.Find("Suit1 down left cloth color4").gameObject;
            s1dlcc4.gameObject.SetActive(false);
        }
        else if (Job2_5Off == true)
        {
            Job2_5Off = false;
            GameObject s1tbc5 = transform.Find("Suit1 top body color5").gameObject;
            GameObject s1truac5 = transform.Find("Suit1 top right up arm color5").gameObject;
            GameObject s1trdac5 = transform.Find("Suit1 top right down arm color5").gameObject;
            GameObject s1tluac5 = transform.Find("Suit1 top left up arm color5").gameObject;
            GameObject s1tldac5 = transform.Find("Suit1 top left down arm color5").gameObject;
            GameObject s1drc5 = transform.Find("Suit1 down right color5").gameObject;
            GameObject s1dlc5 = transform.Find("Suit1 down left color5").gameObject;
            s1tbc5.gameObject.SetActive(false);
            s1truac5.gameObject.SetActive(false);
            s1trdac5.gameObject.SetActive(false);
            s1tluac5.gameObject.SetActive(false);
            s1tldac5.gameObject.SetActive(false);
            s1drc5.gameObject.SetActive(false);
            s1dlc5.gameObject.SetActive(false);

            GameObject s1tbcc5_1 = transform.Find("Suit1 top body cloth color5-1").gameObject;
            s1tbcc5_1.gameObject.SetActive(false);
            GameObject s1tbcc5_2 = transform.Find("Suit1 top body cloth color5-2").gameObject;
            s1tbcc5_2.gameObject.SetActive(false);
            GameObject s1tbcc5_3 = transform.Find("Suit1 top body cloth color5-3").gameObject;
            s1tbcc5_3.gameObject.SetActive(false);
            GameObject s1drcc5 = transform.Find("Suit1 down right cloth color5").gameObject;
            s1drcc5.gameObject.SetActive(false);
            GameObject s1dlcc5 = transform.Find("Suit1 down left cloth color5").gameObject;
            s1dlcc5.gameObject.SetActive(false);
        }

        if (Job2LEDOff == true)
        {
            Job2LEDOff = false;
            GameObject s1tbL1 = transform.Find("Suit1 top body LED1").gameObject;
            s1tbL1.gameObject.SetActive(false);
            GameObject s1tbL2 = transform.Find("Suit1 top body LED2").gameObject;
            s1tbL2.gameObject.SetActive(false);
            GameObject s1tbL3 = transform.Find("Suit1 top body LED3").gameObject;
            s1tbL3.gameObject.SetActive(false);
            GameObject s1tbL4 = transform.Find("Suit1 top body LED4").gameObject;
            s1tbL4.gameObject.SetActive(false);
            GameObject s1tbL5 = transform.Find("Suit1 top body LED5").gameObject;
            s1tbL5.gameObject.SetActive(false);
        }

        if (Job2LEDLegtOff == true)
        {
            Job2LEDLegtOff = false;
            GameObject s1drL1t = transform.Find("Suit1 down right LED1 tear").gameObject;
            s1drL1t.gameObject.SetActive(false);
            GameObject s1dlL1t = transform.Find("Suit1 down left LED1 tear").gameObject;
            s1dlL1t.gameObject.SetActive(false);
            GameObject s1drL2t = transform.Find("Suit1 down right LED2 tear").gameObject;
            s1drL2t.gameObject.SetActive(false);
            GameObject s1dlL2t = transform.Find("Suit1 down left LED2 tear").gameObject;
            s1dlL2t.gameObject.SetActive(false);
            GameObject s1drL3t = transform.Find("Suit1 down right LED3 tear").gameObject;
            s1drL3t.gameObject.SetActive(false);
            GameObject s1dlL3t = transform.Find("Suit1 down left LED3 tear").gameObject;
            s1dlL3t.gameObject.SetActive(false);
            GameObject s1drL4t = transform.Find("Suit1 down right LED4 tear").gameObject;
            s1drL4t.gameObject.SetActive(false);
            GameObject s1dlL4t = transform.Find("Suit1 down left LED4 tear").gameObject;
            s1dlL4t.gameObject.SetActive(false);
            GameObject s1drL5t = transform.Find("Suit1 down right LED5 tear").gameObject;
            s1drL5t.gameObject.SetActive(false);
            GameObject s1dlL5t = transform.Find("Suit1 down left LED5 tear").gameObject;
            s1dlL5t.gameObject.SetActive(false);
        }

        if (Job2LEDLegOff == true)
        {
            Job2LEDLegOff = false;
            GameObject s1drL1 = transform.Find("Suit1 down right LED1").gameObject;
            s1drL1.gameObject.SetActive(false);
            GameObject s1dlL1 = transform.Find("Suit1 down left LED1").gameObject;
            s1dlL1.gameObject.SetActive(false);
            GameObject s1drL2 = transform.Find("Suit1 down right LED2").gameObject;
            s1drL2.gameObject.SetActive(false);
            GameObject s1dlL2 = transform.Find("Suit1 down left LED2").gameObject;
            s1dlL2.gameObject.SetActive(false);
            GameObject s1drL3 = transform.Find("Suit1 down right LED3").gameObject;
            s1drL3.gameObject.SetActive(false);
            GameObject s1dlL3 = transform.Find("Suit1 down left LED3").gameObject;
            s1dlL3.gameObject.SetActive(false);
            GameObject s1drL4 = transform.Find("Suit1 down right LED4").gameObject;
            s1drL4.gameObject.SetActive(false);
            GameObject s1dlL4 = transform.Find("Suit1 down left LED4").gameObject;
            s1dlL4.gameObject.SetActive(false);
            GameObject s1drL5 = transform.Find("Suit1 down right LED5").gameObject;
            s1drL5.gameObject.SetActive(false);
            GameObject s1dlL5 = transform.Find("Suit1 down left LED5").gameObject;
            s1dlL5.gameObject.SetActive(false);
        }

        if (Job2_1ShoesOff == true)
        {
            Job2_1ShoesOff = false;
            GameObject s3rc1 = transform.Find("Shoes3 right color1").gameObject;
            s3rc1.gameObject.SetActive(false);
            GameObject s3rc1t = transform.Find("Shoes3 right color1 tear").gameObject;
            s3rc1t.gameObject.SetActive(false);
            GameObject s3lc1 = transform.Find("Shoes3 left color1").gameObject;
            s3lc1.gameObject.SetActive(false);
            GameObject s3lc1t = transform.Find("Shoes3 left color1 tear").gameObject;
            s3lc1t.gameObject.SetActive(false);
        }
        else if (Job2_2ShoesOff == true)
        {
            Job2_2ShoesOff = false;
            GameObject s3rc2 = transform.Find("Shoes3 right color2").gameObject;
            s3rc2.gameObject.SetActive(false);
            GameObject s3rc2t = transform.Find("Shoes3 right color2 tear").gameObject;
            s3rc2t.gameObject.SetActive(false);
            GameObject s3lc2 = transform.Find("Shoes3 left color2").gameObject;
            s3lc2.gameObject.SetActive(false);
            GameObject s3lc2t = transform.Find("Shoes3 left color2 tear").gameObject;
            s3lc2t.gameObject.SetActive(false);
        }
        else if (Job2_3ShoesOff == true)
        {
            Job2_3ShoesOff = false;
            GameObject s3rc3 = transform.Find("Shoes3 right color3").gameObject;
            s3rc3.gameObject.SetActive(false);
            GameObject s3rc3t = transform.Find("Shoes3 right color3 tear").gameObject;
            s3rc3t.gameObject.SetActive(false);
            GameObject s3lc3 = transform.Find("Shoes3 left color3").gameObject;
            s3lc3.gameObject.SetActive(false);
            GameObject s3lc3t = transform.Find("Shoes3 left color3 tear").gameObject;
            s3lc3t.gameObject.SetActive(false);
        }

        //기술자
        if (Job3_1Off == true)
        {
            Job3_1Off = false;
            GameObject s2tbc1 = transform.Find("Suit2 top body color1").gameObject;
            GameObject s2truac1 = transform.Find("Suit2 top right up arm color1").gameObject;
            GameObject s2trdac1 = transform.Find("Suit2 top right down arm color1").gameObject;
            GameObject s2tluac1 = transform.Find("Suit2 top left up arm color1").gameObject;
            GameObject s2tldac1 = transform.Find("Suit2 top left down arm color1").gameObject;
            GameObject s2drc1 = transform.Find("Suit2 down right color1").gameObject;
            GameObject s2dlc1 = transform.Find("Suit2 down left color1").gameObject;
            s2tbc1.gameObject.SetActive(false);
            s2truac1.gameObject.SetActive(false);
            s2trdac1.gameObject.SetActive(false);
            s2tluac1.gameObject.SetActive(false);
            s2tldac1.gameObject.SetActive(false);
            s2drc1.gameObject.SetActive(false);
            s2dlc1.gameObject.SetActive(false);

            GameObject s2tbcc1_1 = transform.Find("Suit2 top body cloth color1-1").gameObject;
            s2tbcc1_1.gameObject.SetActive(false);
            GameObject s2tbcc1_2 = transform.Find("Suit2 top body cloth color1-2").gameObject;
            s2tbcc1_2.gameObject.SetActive(false);
            GameObject s2tbcc1_3 = transform.Find("Suit2 top body cloth color1-3").gameObject;
            s2tbcc1_3.gameObject.SetActive(false);
            GameObject s2drcc1 = transform.Find("Suit2 down right cloth color1").gameObject;
            s2drcc1.gameObject.SetActive(false);
            GameObject s2dlcc1 = transform.Find("Suit2 down left cloth color1").gameObject;
            s2dlcc1.gameObject.SetActive(false);
            GameObject s2tldacc1 = transform.Find("Suit2 top left down arm cloth color1").gameObject;
            s2tldacc1.gameObject.SetActive(false);
        }
        else if (Job3_2Off == true)
        {
            Job3_2Off = false;
            GameObject s2tbc2 = transform.Find("Suit2 top body color2").gameObject;
            GameObject s2truac2 = transform.Find("Suit2 top right up arm color2").gameObject;
            GameObject s2trdac2 = transform.Find("Suit2 top right down arm color2").gameObject;
            GameObject s2tluac2 = transform.Find("Suit2 top left up arm color2").gameObject;
            GameObject s2tldac2 = transform.Find("Suit2 top left down arm color2").gameObject;
            GameObject s2drc2 = transform.Find("Suit2 down right color2").gameObject;
            GameObject s2dlc2 = transform.Find("Suit2 down left color2").gameObject;
            s2tbc2.gameObject.SetActive(false);
            s2truac2.gameObject.SetActive(false);
            s2trdac2.gameObject.SetActive(false);
            s2tluac2.gameObject.SetActive(false);
            s2tldac2.gameObject.SetActive(false);
            s2drc2.gameObject.SetActive(false);
            s2dlc2.gameObject.SetActive(false);

            GameObject s2tbcc2_1 = transform.Find("Suit2 top body cloth color2-1").gameObject;
            s2tbcc2_1.gameObject.SetActive(false);
            GameObject s2tbcc2_2 = transform.Find("Suit2 top body cloth color2-2").gameObject;
            s2tbcc2_2.gameObject.SetActive(false);
            GameObject s2tbcc2_3 = transform.Find("Suit2 top body cloth color2-3").gameObject;
            s2tbcc2_3.gameObject.SetActive(false);
            GameObject s2drcc2 = transform.Find("Suit2 down right cloth color2").gameObject;
            s2drcc2.gameObject.SetActive(false);
            GameObject s2dlcc2 = transform.Find("Suit2 down left cloth color2").gameObject;
            s2dlcc2.gameObject.SetActive(false);
            GameObject s2tldacc2 = transform.Find("Suit2 top left down arm cloth color2").gameObject;
            s2tldacc2.gameObject.SetActive(false);
        }
        else if (Job3_3Off == true)
        {
            Job3_3Off = false;
            GameObject s2tbc3 = transform.Find("Suit2 top body color3").gameObject;
            GameObject s2truac3 = transform.Find("Suit2 top right up arm color3").gameObject;
            GameObject s2trdac3 = transform.Find("Suit2 top right down arm color3").gameObject;
            GameObject s2tluac3 = transform.Find("Suit2 top left up arm color3").gameObject;
            GameObject s2tldac3 = transform.Find("Suit2 top left down arm color3").gameObject;
            GameObject s2drc3 = transform.Find("Suit2 down right color3").gameObject;
            GameObject s2dlc3 = transform.Find("Suit2 down left color3").gameObject;
            s2tbc3.gameObject.SetActive(false);
            s2truac3.gameObject.SetActive(false);
            s2trdac3.gameObject.SetActive(false);
            s2tluac3.gameObject.SetActive(false);
            s2tldac3.gameObject.SetActive(false);
            s2drc3.gameObject.SetActive(false);
            s2dlc3.gameObject.SetActive(false);

            GameObject s2tbcc3_1 = transform.Find("Suit2 top body cloth color3-1").gameObject;
            s2tbcc3_1.gameObject.SetActive(false);
            GameObject s2tbcc3_2 = transform.Find("Suit2 top body cloth color3-2").gameObject;
            s2tbcc3_2.gameObject.SetActive(false);
            GameObject s2tbcc3_3 = transform.Find("Suit2 top body cloth color3-3").gameObject;
            s2tbcc3_3.gameObject.SetActive(false);
            GameObject s2drcc3 = transform.Find("Suit2 down right cloth color3").gameObject;
            s2drcc3.gameObject.SetActive(false);
            GameObject s2dlcc3 = transform.Find("Suit2 down left cloth color3").gameObject;
            s2dlcc3.gameObject.SetActive(false);
            GameObject s2tldacc3 = transform.Find("Suit2 top left down arm cloth color3").gameObject;
            s2tldacc3.gameObject.SetActive(false);
        }
        else if (Job3_4Off == true)
        {
            Job3_4Off = false;
            GameObject s2tbc4 = transform.Find("Suit2 top body color4").gameObject;
            GameObject s2truac4 = transform.Find("Suit2 top right up arm color4").gameObject;
            GameObject s2trdac4 = transform.Find("Suit2 top right down arm color4").gameObject;
            GameObject s2tluac4 = transform.Find("Suit2 top left up arm color4").gameObject;
            GameObject s2tldac4 = transform.Find("Suit2 top left down arm color4").gameObject;
            GameObject s2drc4 = transform.Find("Suit2 down right color4").gameObject;
            GameObject s2dlc4 = transform.Find("Suit2 down left color4").gameObject;
            s2tbc4.gameObject.SetActive(false);
            s2truac4.gameObject.SetActive(false);
            s2trdac4.gameObject.SetActive(false);
            s2tluac4.gameObject.SetActive(false);
            s2tldac4.gameObject.SetActive(false);
            s2drc4.gameObject.SetActive(false);
            s2dlc4.gameObject.SetActive(false);

            GameObject s2tbcc4_1 = transform.Find("Suit2 top body cloth color4-1").gameObject;
            s2tbcc4_1.gameObject.SetActive(false);
            GameObject s2tbcc4_2 = transform.Find("Suit2 top body cloth color4-2").gameObject;
            s2tbcc4_2.gameObject.SetActive(false);
            GameObject s2tbcc4_3 = transform.Find("Suit2 top body cloth color4-3").gameObject;
            s2tbcc4_3.gameObject.SetActive(false);
            GameObject s2drcc4 = transform.Find("Suit2 down right cloth color4").gameObject;
            s2drcc4.gameObject.SetActive(false);
            GameObject s2dlcc4 = transform.Find("Suit2 down left cloth color4").gameObject;
            s2dlcc4.gameObject.SetActive(false);
            GameObject s2tldacc4 = transform.Find("Suit2 top left down arm cloth color4").gameObject;
            s2tldacc4.gameObject.SetActive(false);
        }
        else if (Job3_5Off == true)
        {
            Job3_5Off = false;
            GameObject s21tbc5 = transform.Find("Suit2 top body color5").gameObject;
            GameObject s2truac5 = transform.Find("Suit2 top right up arm color5").gameObject;
            GameObject s2trdac5 = transform.Find("Suit2 top right down arm color5").gameObject;
            GameObject s2tluac5 = transform.Find("Suit2 top left up arm color5").gameObject;
            GameObject s2tldac5 = transform.Find("Suit2 top left down arm color5").gameObject;
            GameObject s2drc5 = transform.Find("Suit2 down right color5").gameObject;
            GameObject s2dlc5 = transform.Find("Suit2 down left color5").gameObject;
            s21tbc5.gameObject.SetActive(false);
            s2truac5.gameObject.SetActive(false);
            s2trdac5.gameObject.SetActive(false);
            s2tluac5.gameObject.SetActive(false);
            s2tldac5.gameObject.SetActive(false);
            s2drc5.gameObject.SetActive(false);
            s2dlc5.gameObject.SetActive(false);

            GameObject s2tbcc5_1 = transform.Find("Suit2 top body cloth color5-1").gameObject;
            s2tbcc5_1.gameObject.SetActive(false);
            GameObject s2tbcc5_2 = transform.Find("Suit2 top body cloth color5-2").gameObject;
            s2tbcc5_2.gameObject.SetActive(false);
            GameObject s2tbcc5_3 = transform.Find("Suit2 top body cloth color5-3").gameObject;
            s2tbcc5_3.gameObject.SetActive(false);
            GameObject s2drcc5 = transform.Find("Suit2 down right cloth color5").gameObject;
            s2drcc5.gameObject.SetActive(false);
            GameObject s2dlcc5 = transform.Find("Suit2 down left cloth color5").gameObject;
            s2dlcc5.gameObject.SetActive(false);
            GameObject s2tldacc5 = transform.Find("Suit2 top left down arm cloth color5").gameObject;
            s2tldacc5.gameObject.SetActive(false);
        }

        if (Job3_1SOff == true)
        {
            Job3_1SOff = false;
            GameObject s4rc1 = transform.Find("Shoes4 right color1").gameObject;
            s4rc1.gameObject.SetActive(false);
            GameObject s4rc1t = transform.Find("Shoes4 right color1 tear").gameObject;
            s4rc1t.gameObject.SetActive(false);
            GameObject s4lc1 = transform.Find("Shoes4 left color1").gameObject;
            s4lc1.gameObject.SetActive(false);
            GameObject s4lc1t = transform.Find("Shoes4 left color1 tear").gameObject;
            s4lc1t.gameObject.SetActive(false);
        }
        if (Job3_2SOff == true)
        {
            Job3_2SOff = false;
            GameObject s4rc2 = transform.Find("Shoes4 right color2").gameObject;
            s4rc2.gameObject.SetActive(false);
            GameObject s4rc2t = transform.Find("Shoes4 right color2 tear").gameObject;
            s4rc2t.gameObject.SetActive(false);
            GameObject s4lc2 = transform.Find("Shoes4 left color2").gameObject;
            s4lc2.gameObject.SetActive(false);
            GameObject s4lc2t = transform.Find("Shoes4 left color2 tear").gameObject;
            s4lc2t.gameObject.SetActive(false);
        }
        if (Job3_3SOff == true)
        {
            Job3_3SOff = false;
            GameObject s4rc3 = transform.Find("Shoes4 right color3").gameObject;
            s4rc3.gameObject.SetActive(false);
            GameObject s4rc3t = transform.Find("Shoes4 right color3 tear").gameObject;
            s4rc3t.gameObject.SetActive(false);
        }
        if (Job3_4SOff == true)
        {
            Job3_4SOff = false;
            GameObject s4rc4 = transform.Find("Shoes4 right color4").gameObject;
            s4rc4.gameObject.SetActive(false);
            GameObject s4rc4t = transform.Find("Shoes4 right color4 tear").gameObject;
            s4rc4t.gameObject.SetActive(false);
        }

        if (Job3_1LEDOff == true)
        {
            Job3_1LEDOff = false;
            GameObject s2tbL1 = transform.Find("Suit2 top body LED1").gameObject;
            GameObject s2trdaL1 = transform.Find("Suit2 top right down arm LED1").gameObject;
            GameObject s2tldaL1 = transform.Find("Suit2 top left down arm LED1").gameObject;
            GameObject s2drL1 = transform.Find("Suit2 down right LED1").gameObject;
            GameObject s2dlL1 = transform.Find("Suit2 down left LED1").gameObject;
            s2tbL1.gameObject.SetActive(false);
            s2trdaL1.gameObject.SetActive(false);
            s2tldaL1.gameObject.SetActive(false);
            s2drL1.gameObject.SetActive(false);
            s2dlL1.gameObject.SetActive(false);
        }
        else if (Job3_2LEDOff == true)
        {
            Job3_2LEDOff = false;
            GameObject s2tbL2 = transform.Find("Suit2 top body LED2").gameObject;
            GameObject s2trdaL2 = transform.Find("Suit2 top right down arm LED2").gameObject;
            GameObject s2tldaL2 = transform.Find("Suit2 top left down arm LED2").gameObject;
            GameObject s2drL2 = transform.Find("Suit2 down right LED2").gameObject;
            GameObject s2dlL2 = transform.Find("Suit2 down left LED2").gameObject;
            s2tbL2.gameObject.SetActive(false);
            s2trdaL2.gameObject.SetActive(false);
            s2tldaL2.gameObject.SetActive(false);
            s2drL2.gameObject.SetActive(false);
            s2dlL2.gameObject.SetActive(false);
        }
        else if (Job3_3LEDOff == true)
        {
            Job3_3LEDOff = false;
            GameObject s2tbL3 = transform.Find("Suit2 top body LED3").gameObject;
            GameObject s2trdaL3 = transform.Find("Suit2 top right down arm LED3").gameObject;
            GameObject s2tldaL3 = transform.Find("Suit2 top left down arm LED3").gameObject;
            GameObject s2drL3 = transform.Find("Suit2 down right LED3").gameObject;
            GameObject s2dlL3 = transform.Find("Suit2 down left LED3").gameObject;
            s2tbL3.gameObject.SetActive(false);
            s2trdaL3.gameObject.SetActive(false);
            s2tldaL3.gameObject.SetActive(false);
            s2drL3.gameObject.SetActive(false);
            s2dlL3.gameObject.SetActive(false);
        }
        else if (Job3_4LEDOff == true)
        {
            Job3_4LEDOff = false;
            GameObject s2tbL4 = transform.Find("Suit2 top body LED4").gameObject;
            GameObject s2trdaL4 = transform.Find("Suit2 top right down arm LED4").gameObject;
            GameObject s2tldaL4 = transform.Find("Suit2 top left down arm LED4").gameObject;
            GameObject s2drL4 = transform.Find("Suit2 down right LED4").gameObject;
            GameObject s2dlL4 = transform.Find("Suit2 down left LED4").gameObject;
            s2tbL4.gameObject.SetActive(false);
            s2trdaL4.gameObject.SetActive(false);
            s2tldaL4.gameObject.SetActive(false);
            s2drL4.gameObject.SetActive(false);
            s2dlL4.gameObject.SetActive(false);
        }
        else if (Job3_5LEDOff == true)
        {
            Job3_5LEDOff = false;
            GameObject s2tbL5 = transform.Find("Suit2 top body LED5").gameObject;
            GameObject s2trdaL5 = transform.Find("Suit2 top right down arm LED5").gameObject;
            GameObject s2tldaL5 = transform.Find("Suit2 top left down arm LED5").gameObject;
            GameObject s2drL5 = transform.Find("Suit2 down right LED5").gameObject;
            GameObject s2dlL5 = transform.Find("Suit2 down left LED5").gameObject;
            s2tbL5.gameObject.SetActive(false);
            s2trdaL5.gameObject.SetActive(false);
            s2tldaL5.gameObject.SetActive(false);
            s2drL5.gameObject.SetActive(false);
            s2dlL5.gameObject.SetActive(false);
        }

        //의료원
        if (Job4_1Off == true)
        {
            Job4_1Off = false;
            GameObject s3tbc1 = transform.Find("Suit3 top body color1").gameObject;
            GameObject s3truac1 = transform.Find("Suit3 top right up arm color1").gameObject;
            GameObject s3trdac1 = transform.Find("Suit3 top right down arm color1").gameObject;
            GameObject s3tluac1 = transform.Find("Suit3 top left up arm color1").gameObject;
            GameObject s3tldac1 = transform.Find("Suit3 top left down arm color1").gameObject;
            GameObject s3drc1 = transform.Find("Suit3 down right color1").gameObject;
            GameObject s3dlc1 = transform.Find("Suit3 down left color1").gameObject;
            s3tbc1.gameObject.SetActive(false);
            s3truac1.gameObject.SetActive(false);
            s3trdac1.gameObject.SetActive(false);
            s3tluac1.gameObject.SetActive(false);
            s3tldac1.gameObject.SetActive(false);
            s3drc1.gameObject.SetActive(false);
            s3dlc1.gameObject.SetActive(false);

            GameObject s3tbcc1_1 = transform.Find("Suit3 top body cloth color1-1").gameObject;
            s3tbcc1_1.gameObject.SetActive(false);
            GameObject s3tbcc1_2 = transform.Find("Suit3 top body cloth color1-2").gameObject;
            s3tbcc1_2.gameObject.SetActive(false);
            GameObject s3trdacc1 = transform.Find("Suit3 top right down arm cloth color1").gameObject;
            s3trdacc1.gameObject.SetActive(false);
            GameObject s3drcc1 = transform.Find("Suit3 down right cloth color1").gameObject;
            s3drcc1.gameObject.SetActive(false);
            GameObject s3dlcc1 = transform.Find("Suit3 down left cloth color1").gameObject;
            s3dlcc1.gameObject.SetActive(false);
            GameObject s3tldacc1 = transform.Find("Suit3 top left down arm cloth color1").gameObject;
            s3tldacc1.gameObject.SetActive(false);
        }
        else if (Job4_2Off == true)
        {
            Job4_2Off = false;
            GameObject s3tbc2 = transform.Find("Suit3 top body color2").gameObject;
            GameObject s3truac2 = transform.Find("Suit3 top right up arm color2").gameObject;
            GameObject s3trdac2 = transform.Find("Suit3 top right down arm color2").gameObject;
            GameObject s3tluac2 = transform.Find("Suit3 top left up arm color2").gameObject;
            GameObject s3tldac2 = transform.Find("Suit3 top left down arm color2").gameObject;
            GameObject s3drc2 = transform.Find("Suit3 down right color2").gameObject;
            GameObject s3dlc2 = transform.Find("Suit3 down left color2").gameObject;
            s3tbc2.gameObject.SetActive(false);
            s3truac2.gameObject.SetActive(false);
            s3trdac2.gameObject.SetActive(false);
            s3tluac2.gameObject.SetActive(false);
            s3tldac2.gameObject.SetActive(false);
            s3drc2.gameObject.SetActive(false);
            s3dlc2.gameObject.SetActive(false);

            GameObject s3tbcc2_1 = transform.Find("Suit3 top body cloth color2-1").gameObject;
            GameObject s3tbcc2_2 = transform.Find("Suit3 top body cloth color2-2").gameObject;
            GameObject s3trdacc2 = transform.Find("Suit3 top right down arm cloth color2").gameObject;
            GameObject s3drcc2 = transform.Find("Suit3 down right cloth color2").gameObject;
            GameObject s3dlcc2 = transform.Find("Suit3 down left cloth color2").gameObject;
            s3tbcc2_1.gameObject.SetActive(false);
            s3tbcc2_2.gameObject.SetActive(false);
            s3trdacc2.gameObject.SetActive(false);
            s3drcc2.gameObject.SetActive(false);
            s3dlcc2.gameObject.SetActive(false);
            GameObject s3tldacc2 = transform.Find("Suit3 top left down arm cloth color2").gameObject;
            s3tldacc2.gameObject.SetActive(false);
        }
        else if (Job4_3Off == true)
        {
            Job4_3Off = false;
            GameObject s3tbc3 = transform.Find("Suit3 top body color3").gameObject;
            GameObject s3truac3 = transform.Find("Suit3 top right up arm color3").gameObject;
            GameObject s3trdac3 = transform.Find("Suit3 top right down arm color3").gameObject;
            GameObject s3tluac3 = transform.Find("Suit3 top left up arm color3").gameObject;
            GameObject s3tldac3 = transform.Find("Suit3 top left down arm color3").gameObject;
            GameObject s3drc3 = transform.Find("Suit3 down right color3").gameObject;
            GameObject s3dlc3 = transform.Find("Suit3 down left color3").gameObject;
            s3tbc3.gameObject.SetActive(false);
            s3truac3.gameObject.SetActive(false);
            s3trdac3.gameObject.SetActive(false);
            s3tluac3.gameObject.SetActive(false);
            s3tldac3.gameObject.SetActive(false);
            s3drc3.gameObject.SetActive(false);
            s3dlc3.gameObject.SetActive(false);

            GameObject s3tbcc3_1 = transform.Find("Suit3 top body cloth color3-1").gameObject;
            GameObject s3tbcc3_2 = transform.Find("Suit3 top body cloth color3-2").gameObject;
            GameObject s3trdacc3 = transform.Find("Suit3 top right down arm cloth color3").gameObject;
            GameObject s3drcc3 = transform.Find("Suit3 down right cloth color3").gameObject;
            GameObject s3dlcc3 = transform.Find("Suit3 down left cloth color3").gameObject;
            s3tbcc3_1.gameObject.SetActive(false);
            s3tbcc3_2.gameObject.SetActive(false);
            s3trdacc3.gameObject.SetActive(false);
            s3drcc3.gameObject.SetActive(false);
            s3dlcc3.gameObject.SetActive(false);
            GameObject s3tldacc3 = transform.Find("Suit3 top left down arm cloth color3").gameObject;
            s3tldacc3.gameObject.SetActive(false);
        }
        else if (Job4_4Off == true)
        {
            Job4_4Off = false;
            GameObject s3tbc4 = transform.Find("Suit3 top body color4").gameObject;
            GameObject s3truac4 = transform.Find("Suit3 top right up arm color4").gameObject;
            GameObject s3trdac4 = transform.Find("Suit3 top right down arm color4").gameObject;
            GameObject s3tluac4 = transform.Find("Suit3 top left up arm color4").gameObject;
            GameObject s3tldac4 = transform.Find("Suit3 top left down arm color4").gameObject;
            GameObject s3drc4 = transform.Find("Suit3 down right color4").gameObject;
            GameObject s3dlc4 = transform.Find("Suit3 down left color4").gameObject;
            s3tbc4.gameObject.SetActive(false);
            s3truac4.gameObject.SetActive(false);
            s3trdac4.gameObject.SetActive(false);
            s3tluac4.gameObject.SetActive(false);
            s3tldac4.gameObject.SetActive(false);
            s3drc4.gameObject.SetActive(false);
            s3dlc4.gameObject.SetActive(false);

            GameObject s3tbcc4_1 = transform.Find("Suit3 top body cloth color4-1").gameObject;
            GameObject s3tbcc4_2 = transform.Find("Suit3 top body cloth color4-2").gameObject;
            GameObject s3trdacc4 = transform.Find("Suit3 top right down arm cloth color4").gameObject;
            GameObject s3drcc4 = transform.Find("Suit3 down right cloth color4").gameObject;
            GameObject s3dlcc4 = transform.Find("Suit3 down left cloth color4").gameObject;
            s3tbcc4_1.gameObject.SetActive(false);
            s3tbcc4_2.gameObject.SetActive(false);
            s3trdacc4.gameObject.SetActive(false);
            s3drcc4.gameObject.SetActive(false);
            s3dlcc4.gameObject.SetActive(false);
            GameObject s3tldacc4 = transform.Find("Suit3 top left down arm cloth color4").gameObject;
            s3tldacc4.gameObject.SetActive(false);
        }
        else if (Job4_5Off == true)
        {
            Job4_5Off = false;
            GameObject s3tbc5 = transform.Find("Suit3 top body color5").gameObject;
            GameObject s3truac5 = transform.Find("Suit3 top right up arm color5").gameObject;
            GameObject s3trdac5 = transform.Find("Suit3 top right down arm color5").gameObject;
            GameObject s3tluac5 = transform.Find("Suit3 top left up arm color5").gameObject;
            GameObject s3tldac5 = transform.Find("Suit3 top left down arm color5").gameObject;
            GameObject s3drc5 = transform.Find("Suit3 down right color5").gameObject;
            GameObject s3dlc5 = transform.Find("Suit3 down left color5").gameObject;
            s3tbc5.gameObject.SetActive(false);
            s3truac5.gameObject.SetActive(false);
            s3trdac5.gameObject.SetActive(false);
            s3tluac5.gameObject.SetActive(false);
            s3tldac5.gameObject.SetActive(false);
            s3drc5.gameObject.SetActive(false);
            s3dlc5.gameObject.SetActive(false);

            GameObject s3tbcc5_1 = transform.Find("Suit3 top body cloth color5-1").gameObject;
            GameObject s3tbcc5_2 = transform.Find("Suit3 top body cloth color5-2").gameObject;
            GameObject s3trdacc5 = transform.Find("Suit3 top right down arm cloth color5").gameObject;
            GameObject s3drcc5 = transform.Find("Suit3 down right cloth color5").gameObject;
            GameObject s3dlcc5 = transform.Find("Suit3 down left cloth color5").gameObject;
            s3tbcc5_1.gameObject.SetActive(false);
            s3tbcc5_2.gameObject.SetActive(false);
            s3trdacc5.gameObject.SetActive(false);
            s3drcc5.gameObject.SetActive(false);
            s3dlcc5.gameObject.SetActive(false);
            GameObject s3tldacc5 = transform.Find("Suit3 top left down arm cloth color5").gameObject;
            s3tldacc5.gameObject.SetActive(false);
        }

        if (Job4_1GOff == true)
        {
            Job4_1GOff = false;
            GameObject g1hand1rc1 = transform.Find("Grove1 right hand1 color1").gameObject;
            GameObject g1hand1lc1 = transform.Find("Grove1 left hand1 color1").gameObject;
            g1hand1rc1.gameObject.SetActive(false);
            g1hand1lc1.gameObject.SetActive(false);
            GameObject g1hand2rc1 = transform.Find("Grove1 right hand2 color1").gameObject;
            GameObject g1hand2lc1 = transform.Find("Grove1 left hand2 color1").gameObject;
            g1hand2rc1.gameObject.SetActive(false);
            g1hand2lc1.gameObject.SetActive(false);
            GameObject g1hand3rc1 = transform.Find("Grove1 right hand3 color1").gameObject;
            GameObject g1hand3lc1 = transform.Find("Grove1 left hand3 color1").gameObject;
            g1hand3rc1.gameObject.SetActive(false);
            g1hand3lc1.gameObject.SetActive(false);
        }
        else if (Job4_2GOff == true)
        {
            Job4_2GOff = false;
            GameObject g1hand1rc2 = transform.Find("Grove1 right hand1 color2").gameObject;
            GameObject g1hand1lc2 = transform.Find("Grove1 left hand1 color2").gameObject;
            g1hand1rc2.gameObject.SetActive(false);
            g1hand1lc2.gameObject.SetActive(false);
            GameObject g1hand2rc2 = transform.Find("Grove1 right hand2 color2").gameObject;
            GameObject g1hand2lc2 = transform.Find("Grove1 left hand2 color2").gameObject;
            g1hand2rc2.gameObject.SetActive(false);
            g1hand2lc2.gameObject.SetActive(false);
            GameObject g1hand3rc2 = transform.Find("Grove1 right hand3 color2").gameObject;
            GameObject g1hand3lc2 = transform.Find("Grove1 left hand3 color2").gameObject;
            g1hand3rc2.gameObject.SetActive(false);
            g1hand3lc2.gameObject.SetActive(false);
        }
        else if (Job4_3GOff == true)
        {
            Job4_3GOff = false;
            GameObject g1hand1rc3 = transform.Find("Grove1 right hand1 color3").gameObject;
            GameObject g1hand1lc3 = transform.Find("Grove1 left hand1 color3").gameObject;
            g1hand1rc3.gameObject.SetActive(false);
            g1hand1lc3.gameObject.SetActive(false);
            GameObject g1hand2rc3 = transform.Find("Grove1 right hand2 color3").gameObject;
            GameObject g1hand2lc3 = transform.Find("Grove1 left hand2 color3").gameObject;
            g1hand2rc3.gameObject.SetActive(false);
            g1hand2lc3.gameObject.SetActive(false);
            GameObject g1hand3rc3 = transform.Find("Grove1 right hand3 color3").gameObject;
            GameObject g1hand3lc3 = transform.Find("Grove1 left hand3 color3").gameObject;
            g1hand3rc3.gameObject.SetActive(false);
            g1hand3lc3.gameObject.SetActive(false);
        }
        else if (Job4_4GOff == true)
        {
            Job4_4GOff = false;
            GameObject g1hand1rc4 = transform.Find("Grove1 right hand1 color4").gameObject;
            GameObject g1hand1lc4 = transform.Find("Grove1 left hand1 color4").gameObject;
            g1hand1rc4.gameObject.SetActive(false);
            g1hand1lc4.gameObject.SetActive(false);
            GameObject g1hand2rc4 = transform.Find("Grove1 right hand2 color4").gameObject;
            GameObject g1hand2lc4 = transform.Find("Grove1 left hand2 color4").gameObject;
            g1hand2rc4.gameObject.SetActive(false);
            g1hand2lc4.gameObject.SetActive(false);
            GameObject g1hand3rc4 = transform.Find("Grove1 right hand3 color4").gameObject;
            GameObject g1hand3lc4 = transform.Find("Grove1 left hand3 color4").gameObject;
            g1hand3rc4.gameObject.SetActive(false);
            g1hand3lc4.gameObject.SetActive(false);
        }
        else if (Job4_5GOff == true)
        {
            Job4_5GOff = false;
            GameObject g1hand1rc5 = transform.Find("Grove1 right hand1 color5").gameObject;
            GameObject g1hand1lc5 = transform.Find("Grove1 left hand1 color5").gameObject;
            g1hand1rc5.gameObject.SetActive(false);
            g1hand1lc5.gameObject.SetActive(false);
            GameObject g1hand2rc5 = transform.Find("Grove1 right hand2 color5").gameObject;
            GameObject g1hand2lc5 = transform.Find("Grove1 left hand2 color5").gameObject;
            g1hand2rc5.gameObject.SetActive(false);
            g1hand2lc5.gameObject.SetActive(false);
            GameObject g1hand3rc5 = transform.Find("Grove1 right hand3 color5").gameObject;
            GameObject g1hand3lc5 = transform.Find("Grove1 left hand3 color5").gameObject;
            g1hand3rc5.gameObject.SetActive(false);
            g1hand3lc5.gameObject.SetActive(false);
        }

        if (Job4_1SOff == true)
        {
            Job4_1SOff = false;
            GameObject s5rc1 = transform.Find("Shoes5 right color1").gameObject;
            s5rc1.gameObject.SetActive(false);
            GameObject s5rc1t = transform.Find("Shoes5 right color1 tear").gameObject;
            s5rc1t.gameObject.SetActive(false);
            GameObject s5lc1 = transform.Find("Shoes5 left color1").gameObject;
            s5lc1.gameObject.SetActive(false);
            GameObject s5lc1t = transform.Find("Shoes5 left color1 tear").gameObject;
            s5lc1t.gameObject.SetActive(false);
        }
        else if (Job4_2SOff == true)
        {
            Job4_2SOff = false;
            GameObject s5rc2 = transform.Find("Shoes5 right color2").gameObject;
            s5rc2.gameObject.SetActive(false);
            GameObject s5rc2t = transform.Find("Shoes5 right color2 tear").gameObject;
            s5rc2t.gameObject.SetActive(false);
            GameObject s5lc2 = transform.Find("Shoes5 left color2").gameObject;
            s5lc2.gameObject.SetActive(false);
            GameObject s5lc2t = transform.Find("Shoes5 left color2 tear").gameObject;
            s5lc2t.gameObject.SetActive(false);
        }
        else if (Job4_3SOff == true)
        {
            Job4_3SOff = false;
            GameObject s5rc3 = transform.Find("Shoes5 right color3").gameObject;
            s5rc3.gameObject.SetActive(false);
            GameObject s5rc3t = transform.Find("Shoes5 right color3 tear").gameObject;
            s5rc3t.gameObject.SetActive(false);
            GameObject s5lc3 = transform.Find("Shoes5 left color3").gameObject;
            s5lc3.gameObject.SetActive(false);
            GameObject s5lc3t = transform.Find("Shoes5 left color3 tear").gameObject;
            s5lc3t.gameObject.SetActive(false);
        }
        else if (Job4_4SOff == true)
        {
            Job4_4SOff = false;
            GameObject s5rc4 = transform.Find("Shoes5 right color4").gameObject;
            s5rc4.gameObject.SetActive(false);
            GameObject s5rc4t = transform.Find("Shoes5 right color4 tear").gameObject;
            s5rc4t.gameObject.SetActive(false);
            GameObject s5lc4 = transform.Find("Shoes5 left color4").gameObject;
            s5lc4.gameObject.SetActive(false);
            GameObject s5lc4t = transform.Find("Shoes5 left color4 tear").gameObject;
            s5lc4t.gameObject.SetActive(false);
        }
        else if (Job4_5SOff == true)
        {
            Job4_5SOff = false;
            GameObject s5rc4 = transform.Find("Shoes5 right color5").gameObject;
            s5rc4.gameObject.SetActive(false);
            GameObject s5rc4t = transform.Find("Shoes5 right color5 tear").gameObject;
            s5rc4t.gameObject.SetActive(false);
            GameObject s5lc5 = transform.Find("Shoes5 left color5").gameObject;
            s5lc5.gameObject.SetActive(false);
            GameObject s5lc5t = transform.Find("Shoes5 left color5 tear").gameObject;
            s5lc5t.gameObject.SetActive(false);
        }

        if (Job4_1LEDOff == true)
        {
            Job4_1LEDOff = false;
            GameObject s3trdaL1 = transform.Find("Suit3 top right down arm LED1").gameObject;
            GameObject s3tldaL1 = transform.Find("Suit3 top left down arm LED1").gameObject;
            GameObject s3drL1 = transform.Find("Suit3 down right LED1").gameObject;
            s3trdaL1.gameObject.SetActive(false);
            s3tldaL1.gameObject.SetActive(false);
            s3drL1.gameObject.SetActive(false);
        }
        else if (Job4_2LEDOff == true)
        {
            Job4_2LEDOff = false;
            GameObject s3trdaL2 = transform.Find("Suit3 top right down arm LED2").gameObject;
            GameObject s3tldaL2 = transform.Find("Suit3 top left down arm LED2").gameObject;
            GameObject s3drL2 = transform.Find("Suit3 down right LED2").gameObject;
            s3trdaL2.gameObject.SetActive(false);
            s3tldaL2.gameObject.SetActive(false);
            s3drL2.gameObject.SetActive(false);
        }
        else if (Job4_3LEDOff == true)
        {
            Job4_3LEDOff = false;
            GameObject s3trdaL3 = transform.Find("Suit3 top right down arm LED3").gameObject;
            GameObject s3tldaL3 = transform.Find("Suit3 top left down arm LED3").gameObject;
            GameObject s3drL3 = transform.Find("Suit3 down right LED3").gameObject;
            s3trdaL3.gameObject.SetActive(false);
            s3tldaL3.gameObject.SetActive(false);
            s3drL3.gameObject.SetActive(false);
        }
        else if (Job4_4LEDOff == true)
        {
            Job4_4LEDOff = false;
            GameObject s3trdaL4 = transform.Find("Suit3 top right down arm LED4").gameObject;
            GameObject s3tldaL4 = transform.Find("Suit3 top left down arm LED4").gameObject;
            GameObject s3drL4 = transform.Find("Suit3 down right LED4").gameObject;
            s3trdaL4.gameObject.SetActive(false);
            s3tldaL4.gameObject.SetActive(false);
            s3drL4.gameObject.SetActive(false);
        }
        else if (Job4_5LEDOff == true)
        {
            Job4_5LEDOff = false;
            GameObject s3trdaL5 = transform.Find("Suit3 top right down arm LED5").gameObject;
            GameObject s3tldaL5 = transform.Find("Suit3 top left down arm LED5").gameObject;
            GameObject s3drL5 = transform.Find("Suit3 down right LED5").gameObject;
            s3trdaL5.gameObject.SetActive(false);
            s3tldaL5.gameObject.SetActive(false);
            s3drL5.gameObject.SetActive(false);
        }

        if (Job4LEDRtOff == true)
        {
            Job4LEDRtOff = false;
            GameObject s3dlL1t = transform.Find("Suit3 down left LED1 tear").gameObject;
            s3dlL1t.gameObject.SetActive(false);
            GameObject s3dlL2t = transform.Find("Suit3 down left LED2 tear").gameObject;
            s3dlL2t.gameObject.SetActive(false);
            GameObject s3dlL3t = transform.Find("Suit3 down left LED3 tear").gameObject;
            s3dlL3t.gameObject.SetActive(false);
            GameObject s3dlL4t = transform.Find("Suit3 down left LED4 tear").gameObject;
            s3dlL4t.gameObject.SetActive(false);
            GameObject s3dlL5t = transform.Find("Suit3 down left LED5 tear").gameObject;
            s3dlL5t.gameObject.SetActive(false);
        }

        if (Job4LEDROff == true)
        {
            Job4LEDROff = false;
            GameObject s3dlL1 = transform.Find("Suit3 down left LED1").gameObject;
            s3dlL1.gameObject.SetActive(false);
            GameObject s3dlL2 = transform.Find("Suit3 down left LED2").gameObject;
            s3dlL2.gameObject.SetActive(false);
            GameObject s3dlL3 = transform.Find("Suit3 down left LED3").gameObject;
            s3dlL3.gameObject.SetActive(false);
            GameObject s3dlL4 = transform.Find("Suit3 down left LED4").gameObject;
            s3dlL4.gameObject.SetActive(false);
            GameObject s3dlL5 = transform.Find("Suit3 down left LED5").gameObject;
            s3dlL5.gameObject.SetActive(false);
        }

        //얼굴
        if (Face1Off == true)
        {
            Face1Off = false;
            GameObject f1head = transform.Find("Face1 head").gameObject;
            GameObject f1eb = transform.Find("Face1 eyeborrow").gameObject;
            GameObject f1e = transform.Find("Face1 eyes").gameObject;
            GameObject f1j = transform.Find("Face1 jaw").gameObject;
            GameObject f1rp = transform.Find("Face1 right pupli").gameObject;
            GameObject f1lp = transform.Find("Face1 left pupli").gameObject;
            f1head.gameObject.SetActive(false);
            f1eb.gameObject.SetActive(false);
            f1e.gameObject.SetActive(false);
            f1j.gameObject.SetActive(false);
            f1rp.gameObject.SetActive(false);
            f1lp.gameObject.SetActive(false);
        }
        else if (Face2Off == true)
        {
            Face2Off = false;
            GameObject f2head = transform.Find("Face2 head").gameObject;
            GameObject f2e = transform.Find("Face2 Eye").gameObject;
            GameObject f2p = transform.Find("Face2 pupli").gameObject;
            GameObject f2m = transform.Find("Face2 mask").gameObject;
            f2head.gameObject.SetActive(false);
            f2e.gameObject.SetActive(false);
            f2p.gameObject.SetActive(false);
            f2m.gameObject.SetActive(false);
        }
        else if (Face3Off == true)
        {
            Face3Off = false;
            GameObject f3head = transform.Find("Face3 head").gameObject;
            GameObject f3eb = transform.Find("Face3 eyeborrow").gameObject;
            GameObject f3e = transform.Find("Face3 eyes").gameObject;
            GameObject f3tm = transform.Find("Face3 Top mouth").gameObject;
            GameObject f3dm = transform.Find("Face3 Down mouth").gameObject;
            GameObject f3p = transform.Find("Face3 pupil").gameObject;
            f3head.gameObject.SetActive(false);
            f3eb.gameObject.SetActive(false);
            f3e.gameObject.SetActive(false);
            f3tm.gameObject.SetActive(false);
            f3dm.gameObject.SetActive(false);
            f3p.gameObject.SetActive(false);
        }
        else if (Face4Off == true)
        {
            Face4Off = false;
            GameObject f5head = transform.Find("Face5 head").gameObject;
            GameObject f5eb = transform.Find("Face5 eyeborrow").gameObject;
            GameObject f5e = transform.Find("Face5 Eyes").gameObject;
            GameObject f5j = transform.Find("Face5 jaw").gameObject;
            GameObject f5rp = transform.Find("Face5 right pupil").gameObject;
            GameObject f5lp = transform.Find("Face5 left pupil").gameObject;
            f5head.gameObject.SetActive(false);
            f5eb.gameObject.SetActive(false);
            f5e.gameObject.SetActive(false);
            f5j.gameObject.SetActive(false);
            f5rp.gameObject.SetActive(false);
            f5lp.gameObject.SetActive(false);
        }
        else if (Face5Off == true)
        {
            Face5Off = false;
            GameObject f6head = transform.Find("Face6 head").gameObject;
            f6head.gameObject.SetActive(false);
        }

        //머리카락
        if (Hair1Off == true)
        {
            Hair1Off = false;
            GameObject h1_1 = transform.Find("Hair1-1").gameObject;
            h1_1.gameObject.SetActive(false);
            GameObject h1_2 = transform.Find("Hair1-2").gameObject;
            h1_2.gameObject.SetActive(false);
            GameObject h1_3 = transform.Find("Hair1-3").gameObject;
            h1_3.gameObject.SetActive(false);
            GameObject h1_4 = transform.Find("Hair1-4").gameObject;
            h1_4.gameObject.SetActive(false);
            GameObject h1_5 = transform.Find("Hair1-5").gameObject;
            h1_5.gameObject.SetActive(false);
        }
        else if (Hair2Off == true)
        {
            Hair2Off = false;
            GameObject h2_1 = transform.Find("Hair2-1").gameObject;
            h2_1.gameObject.SetActive(false);
            GameObject h2_2 = transform.Find("Hair2-2").gameObject;
            h2_2.gameObject.SetActive(false);
            GameObject h2_3 = transform.Find("Hair2-3").gameObject;
            h2_3.gameObject.SetActive(false);
            GameObject h2_4 = transform.Find("Hair2-4").gameObject;
            h2_4.gameObject.SetActive(false);
            GameObject h2_5 = transform.Find("Hair2-5").gameObject;
            h2_5.gameObject.SetActive(false);
        }
        else if (Hair3Off == true)
        {
            Hair3Off = false;
            GameObject h3_1 = transform.Find("Hair3-1").gameObject;
            h3_1.gameObject.SetActive(false);
            GameObject h3_2 = transform.Find("Hair3-2").gameObject;
            h3_2.gameObject.SetActive(false);
            GameObject h3_3 = transform.Find("Hair3-3").gameObject;
            h3_3.gameObject.SetActive(false);
            GameObject h3_4 = transform.Find("Hair3-4").gameObject;
            h3_4.gameObject.SetActive(false);
            GameObject h3_5 = transform.Find("Hair3-5").gameObject;
            h3_5.gameObject.SetActive(false);
        }
        else if (Hair4Off == true)
        {
            Hair4Off = false;
            GameObject h4_1 = transform.Find("Hair4-1").gameObject;
            h4_1.gameObject.SetActive(false);
            GameObject h4_2 = transform.Find("Hair4-2").gameObject;
            h4_2.gameObject.SetActive(false);
            GameObject h4_3 = transform.Find("Hair4-3").gameObject;
            h4_3.gameObject.SetActive(false);
            GameObject h4_4 = transform.Find("Hair4-4").gameObject;
            h4_4.gameObject.SetActive(false);
            GameObject h4_5 = transform.Find("Hair4-5").gameObject;
            h4_5.gameObject.SetActive(false);
        }
        else if (Hair5Off == true)
        {
            Hair5Off = false;
            GameObject h5_1 = transform.Find("Hair5-1").gameObject;
            h5_1.gameObject.SetActive(false);
            GameObject h5_2 = transform.Find("Hair5-2").gameObject;
            h5_2.gameObject.SetActive(false);
            GameObject h5_3 = transform.Find("Hair5-3").gameObject;
            h5_3.gameObject.SetActive(false);
            GameObject h5_4 = transform.Find("Hair5-4").gameObject;
            h5_4.gameObject.SetActive(false);
            GameObject h5_5 = transform.Find("Hair5-5").gameObject;
            h5_5.gameObject.SetActive(false);
        }
        else if (Hair6Off == true)
        {
            Hair6Off = false;
            GameObject h6_1 = transform.Find("Hair6-1").gameObject;
            h6_1.gameObject.SetActive(false);
            GameObject h6_2 = transform.Find("Hair6-2").gameObject;
            h6_2.gameObject.SetActive(false);
            GameObject h6_3 = transform.Find("Hair6-3").gameObject;
            h6_3.gameObject.SetActive(false);
            GameObject h6_4 = transform.Find("Hair6-4").gameObject;
            h6_4.gameObject.SetActive(false);
            GameObject h6_5 = transform.Find("Hair6-5").gameObject;
            h6_5.gameObject.SetActive(false);
        }

        if (CapOff == true)
        {
            CapOff = false;
            GameObject s3hb = transform.Find("Suit3 head blood").gameObject;
            GameObject s3hi = transform.Find("Suit3 head inner").gameObject;
            s3hb.gameObject.SetActive(false);
            s3hi.gameObject.SetActive(false);
            GameObject s3hc1 = transform.Find("Suit3 head color1").gameObject;
            s3hc1.gameObject.SetActive(false);
            GameObject s3hc2 = transform.Find("Suit3 head color2").gameObject;
            s3hc2.gameObject.SetActive(false);
            GameObject s3hc3 = transform.Find("Suit3 head color3").gameObject;
            s3hc3.gameObject.SetActive(false);
            GameObject s3hc4 = transform.Find("Suit3 head color4").gameObject;
            s3hc4.gameObject.SetActive(false);
            GameObject s3hc5 = transform.Find("Suit3 head color5").gameObject;
            s3hc5.gameObject.SetActive(false);
        }

        //로고
        if (Logo1_1FOff == true)
        {
            Logo1_1FOff = false;
            GameObject lf1c1 = transform.Find("Logo Frame1 color1").gameObject;
            lf1c1.gameObject.SetActive(false);
            GameObject lf1c2 = transform.Find("Logo Frame1 color2").gameObject;
            lf1c2.gameObject.SetActive(false);
            GameObject lf1c3 = transform.Find("Logo Frame1 color3").gameObject;
            lf1c3.gameObject.SetActive(false);
            GameObject lf1c4 = transform.Find("Logo Frame1 color4").gameObject;
            lf1c4.gameObject.SetActive(false);
            GameObject lf1c5 = transform.Find("Logo Frame1 color5").gameObject;
            lf1c5.gameObject.SetActive(false);
        }
        else if (Logo1_2FOff == true)
        {
            Logo1_2FOff = false;
            GameObject lf2c1 = transform.Find("Logo Frame2 color1").gameObject;
            lf2c1.gameObject.SetActive(false);
            GameObject lf2c2 = transform.Find("Logo Frame2 color2").gameObject;
            lf2c2.gameObject.SetActive(false);
            GameObject lf2c3 = transform.Find("Logo Frame2 color3").gameObject;
            lf2c3.gameObject.SetActive(false);
            GameObject lf2c4 = transform.Find("Logo Frame2 color4").gameObject;
            lf2c4.gameObject.SetActive(false);
            GameObject lf2c5 = transform.Find("Logo Frame2 color5").gameObject;
            lf2c5.gameObject.SetActive(false);
        }
        else if (Logo1_3FOff == true)
        {
            Logo1_3FOff = false;
            GameObject lf3c1 = transform.Find("Logo Frame3 color1").gameObject;
            lf3c1.gameObject.SetActive(false);
            GameObject lf3c2 = transform.Find("Logo Frame3 color2").gameObject;
            lf3c2.gameObject.SetActive(false);
            GameObject lf3c3 = transform.Find("Logo Frame3 color3").gameObject;
            lf3c3.gameObject.SetActive(false);
            GameObject lf3c4 = transform.Find("Logo Frame3 color4").gameObject;
            lf3c4.gameObject.SetActive(false);
            GameObject lf3c5 = transform.Find("Logo Frame3 color5").gameObject;
            lf3c5.gameObject.SetActive(false);
        }
        else if (Logo1_4FOff == true)
        {
            GameObject lf4c1 = transform.Find("Logo Frame4 color1").gameObject;
            lf4c1.gameObject.SetActive(false);
            GameObject lf4c2 = transform.Find("Logo Frame4 color2").gameObject;
            lf4c2.gameObject.SetActive(false);
            GameObject lf4c3 = transform.Find("Logo Frame4 color3").gameObject;
            lf4c3.gameObject.SetActive(false);
            GameObject lf4c4 = transform.Find("Logo Frame4 color4").gameObject;
            lf4c4.gameObject.SetActive(false);
            GameObject lf4c5 = transform.Find("Logo Frame4 color5").gameObject;
            lf4c5.gameObject.SetActive(false);
        }

        if (LogoDisplayOff == true)
        {
            LogoDisplayOff = false;
            GameObject d1 = transform.Find("Display color1").gameObject;
            d1.gameObject.SetActive(false);
            GameObject d2 = transform.Find("Display color2").gameObject;
            d2.gameObject.SetActive(false);
            GameObject d3 = transform.Find("Display color3").gameObject;
            d3.gameObject.SetActive(false);
            GameObject d4 = transform.Find("Display color4").gameObject;
            d4.gameObject.SetActive(false);
        }

        if (Logo1_1Off == true)
        {
            Logo1_1Off = false;
            GameObject l1 = transform.Find("logo1").gameObject;
            GameObject l1d = transform.Find("logo1 down").gameObject;
            l1.gameObject.SetActive(false);
            l1d.gameObject.SetActive(false);
        }
        else if (Logo1_2Off == true)
        {
            Logo1_2Off = false;
            GameObject l2 = transform.Find("logo2").gameObject;
            GameObject l2d = transform.Find("logo2 down").gameObject;
            l2.gameObject.SetActive(false);
            l2d.gameObject.SetActive(false);
        }
        else if (Logo1_3Off == true)
        {
            Logo1_3Off = false;
            GameObject l3 = transform.Find("logo3").gameObject;
            GameObject l3d = transform.Find("logo3 down").gameObject;
            l3.gameObject.SetActive(false);
            l3d.gameObject.SetActive(false);
        }
        else if (Logo1_4Off == true)
        {
            Logo1_4Off = false;
            GameObject l4 = transform.Find("logo4").gameObject;
            GameObject l4d = transform.Find("logo4 down").gameObject;
            l4.gameObject.SetActive(false);
            l4d.gameObject.SetActive(false);
        }
        else if (Logo1_5Off == true)
        {
            Logo1_5Off = false;
            GameObject l5 = transform.Find("logo5").gameObject;
            GameObject l5d = transform.Find("logo5 down").gameObject;
            l5.gameObject.SetActive(false);
            l5d.gameObject.SetActive(false);
        }
        else if (Logo1_6Off == true)
        {
            Logo1_6Off = false;
            GameObject l6 = transform.Find("logo6").gameObject;
            GameObject l6d = transform.Find("logo6 down").gameObject;
            l6.gameObject.SetActive(false);
            l6d.gameObject.SetActive(false);
        }
        else if (Logo1_7Off == true)
        {
            Logo1_7Off = false;
            GameObject l7 = transform.Find("logo7").gameObject;
            GameObject l7d = transform.Find("logo7 down").gameObject;
            l7.gameObject.SetActive(false);
            l7d.gameObject.SetActive(false);
        }
        else if (Logo1_8Off == true)
        {
            Logo1_8Off = false;
            GameObject l8 = transform.Find("logo8").gameObject;
            GameObject l8d = transform.Find("logo8 down").gameObject;
            l8.gameObject.SetActive(false);
            l8d.gameObject.SetActive(false);
        }
        else if (Logo1_9Off == true)
        {
            Logo1_9Off = false;
            GameObject l9 = transform.Find("logo9").gameObject;
            GameObject l9d = transform.Find("logo9 down").gameObject;
            l9.gameObject.SetActive(false);
            l9d.gameObject.SetActive(false);
        }
        else if (Logo1_10Off == true)
        {
            Logo1_10Off = false;
            GameObject l10 = transform.Find("logo10").gameObject;
            GameObject l10d = transform.Find("logo10 down").gameObject;
            l10.gameObject.SetActive(false);
            l10d.gameObject.SetActive(false);
        }
        else if (Logo1_11Off == true)
        {
            Logo1_11Off = false;
            GameObject l11 = transform.Find("logo11").gameObject;
            GameObject l11d = transform.Find("logo11 down").gameObject;
            l11.gameObject.SetActive(false);
            l11d.gameObject.SetActive(false);
        }
        else if (Logo1_12Off == true)
        {
            Logo1_12Off = false;
            GameObject l12 = transform.Find("logo12").gameObject;
            GameObject l12d = transform.Find("logo12 down").gameObject;
            l12.gameObject.SetActive(false);
            l12d.gameObject.SetActive(false);
        }
        else if (Logo1_13Off == true)
        {
            Logo1_13Off = false;
            GameObject l13 = transform.Find("logo13").gameObject;
            GameObject l13d = transform.Find("logo13 down").gameObject;
            l13.gameObject.SetActive(false);
            l13d.gameObject.SetActive(false);
        }
        else if (Logo1_14Off == true)
        {
            Logo1_14Off = false;
            GameObject l14 = transform.Find("logo14").gameObject;
            GameObject l14d = transform.Find("logo14 down").gameObject;
            l14.gameObject.SetActive(false);
            l14d.gameObject.SetActive(false);
        }
        else if (Logo1_15Off == true)
        {
            Logo1_15Off = false;
            GameObject l15 = transform.Find("logo15").gameObject;
            GameObject l15d = transform.Find("logo15 down").gameObject;
            l15.gameObject.SetActive(false);
            l15d.gameObject.SetActive(false);
        }
        else if (Logo1_16Off == true)
        {
            Logo1_16Off = false;
            GameObject l16 = transform.Find("logo16").gameObject;
            GameObject l16d = transform.Find("logo16 down").gameObject;
            l16.gameObject.SetActive(false);
            l16d.gameObject.SetActive(false);
        }
        else if (Logo1_17Off == true)
        {
            Logo1_17Off = false;
            GameObject l17 = transform.Find("logo17").gameObject;
            GameObject l17d = transform.Find("logo17 down").gameObject;
            l17.gameObject.SetActive(false);
            l17d.gameObject.SetActive(false);
        }
        else if (Logo1_18Off == true)
        {
            Logo1_18Off = false;
            GameObject l18 = transform.Find("logo18").gameObject;
            GameObject l18d = transform.Find("logo18 down").gameObject;
            l18.gameObject.SetActive(false);
            l18d.gameObject.SetActive(false);
        }
        else if (Logo1_19Off == true)
        {
            Logo1_19Off = false;
            GameObject l19 = transform.Find("logo19").gameObject;
            GameObject l19d = transform.Find("logo19 down").gameObject;
            l19.gameObject.SetActive(false);
            l19d.gameObject.SetActive(false);
        }
        else if (Logo1_20Off == true)
        {
            Logo1_20Off = false;
            GameObject l20 = transform.Find("logo20").gameObject;
            GameObject l20d = transform.Find("logo20 down").gameObject;
            l20.gameObject.SetActive(false);
            l20d.gameObject.SetActive(false);
        }
        else if (Logo1_21Off == true)
        {
            Logo1_21Off = false;
            GameObject l21 = transform.Find("logo21").gameObject;
            GameObject l21d = transform.Find("logo21 down").gameObject;
            l21.gameObject.SetActive(false);
            l21d.gameObject.SetActive(false);
        }

        CreateBody(); //스폰시 스타일 결정
    }
}