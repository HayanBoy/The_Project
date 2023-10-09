using UnityEngine.UI;
using UnityEngine;

public class MissionWindowPlanetName : MonoBehaviour
{
    public WordPrintSystem WordPrintSystem;
    public Text Text;

    public void AreaNamePrint(int number)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (number == 1001)
                Text.text = string.Format("Satarius Glessia");
            else if (number == 1002)
                Text.text = string.Format("Aposis");
            else if (number == 1003)
                Text.text = string.Format("Torono");
            else if (number == 1004)
                Text.text = string.Format("Plopa II");
            else if (number == 1005)
                Text.text = string.Format("Vedes VI");
            else if (number == 1006)
                Text.text = string.Format("Aron Peri");
            else if (number == 1007)
                Text.text = string.Format("Papatus II");
            else if (number == 1008)
                Text.text = string.Format("Papatus III");
            else if (number == 1009)
                Text.text = string.Format("Kyepotoros");
            else if (number == 1010)
                Text.text = string.Format("Tratos");
            else if (number == 1011)
                Text.text = string.Format("Oclasis");
            else if (number == 1012)
                Text.text = string.Format("Derious Heri");
            else if (number == 1013)
                Text.text = string.Format("Veltrorexy");
            else if (number == 1014)
                Text.text = string.Format("Erix Jeoqeta");
            else if (number == 1015)
                Text.text = string.Format("Qeepo");
            else if (number == 1016)
                Text.text = string.Format("Crown Yosere");
            else if (number == 1017)
                Text.text = string.Format("Oros");
            else if (number == 1018)
                Text.text = string.Format("Japet Agrone");
            else if (number == 1019)
                Text.text = string.Format("Xacro 042351");
            else if (number == 1020)
                Text.text = string.Format("Delta D31-2208");
            else if (number == 1021)
                Text.text = string.Format("Delta D31-9523");
            else if (number == 1022)
                Text.text = string.Format("Delta D31-12721");
            else if (number == 1023)
                Text.text = string.Format("Jerato O95-1125");
            else if (number == 1024)
                Text.text = string.Format("Jerato O95-2252");
            else if (number == 1025)
                Text.text = string.Format("Jerato O95-8510");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (number == 1001)
                Text.text = string.Format("사타리우스 글래시아");
            else if (number == 1002)
                Text.text = string.Format("아포시스");
            else if (number == 1003)
                Text.text = string.Format("토로노");
            else if (number == 1004)
                Text.text = string.Format("플로파 II");
            else if (number == 1005)
                Text.text = string.Format("베데스 VI");
            else if (number == 1006)
                Text.text = string.Format("아론 페리");
            else if (number == 1007)
                Text.text = string.Format("파파투스 II");
            else if (number == 1008)
                Text.text = string.Format("파파투스 III");
            else if (number == 1009)
                Text.text = string.Format("키예포토로스");
            else if (number == 1010)
                Text.text = string.Format("트라토스");
            else if (number == 1011)
                Text.text = string.Format("오클라시스");
            else if (number == 1012)
                Text.text = string.Format("데리우스 헤리");
            else if (number == 1013)
                Text.text = string.Format("벨트로렉시");
            else if (number == 1014)
                Text.text = string.Format("에릭스 제퀘타");
            else if (number == 1015)
                Text.text = string.Format("퀴이포");
            else if (number == 1016)
                Text.text = string.Format("크라운 요세레");
            else if (number == 1017)
                Text.text = string.Format("오로스");
            else if (number == 1018)
                Text.text = string.Format("자펫 아그로네");
            else if (number == 1019)
                Text.text = string.Format("자크로 042351");
            else if (number == 1020)
                Text.text = string.Format("델타 D31-2208");
            else if (number == 1021)
                Text.text = string.Format("델타 D31-9523");
            else if (number == 1022)
                Text.text = string.Format("델타 D31-12721");
            else if (number == 1023)
                Text.text = string.Format("제라토 O95-1125");
            else if (number == 1024)
                Text.text = string.Format("제라토 O95-2252");
            else if (number == 1025)
                Text.text = string.Format("제라토 O95-8510");
        }
    }
}
