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
                Text.text = string.Format("��Ÿ���콺 �۷��þ�");
            else if (number == 1002)
                Text.text = string.Format("�����ý�");
            else if (number == 1003)
                Text.text = string.Format("��γ�");
            else if (number == 1004)
                Text.text = string.Format("�÷��� II");
            else if (number == 1005)
                Text.text = string.Format("������ VI");
            else if (number == 1006)
                Text.text = string.Format("�Ʒ� �丮");
            else if (number == 1007)
                Text.text = string.Format("�������� II");
            else if (number == 1008)
                Text.text = string.Format("�������� III");
            else if (number == 1009)
                Text.text = string.Format("Ű������ν�");
            else if (number == 1010)
                Text.text = string.Format("Ʈ���佺");
            else if (number == 1011)
                Text.text = string.Format("��Ŭ��ý�");
            else if (number == 1012)
                Text.text = string.Format("�����콺 �츮");
            else if (number == 1013)
                Text.text = string.Format("��Ʈ�η���");
            else if (number == 1014)
                Text.text = string.Format("������ ����Ÿ");
            else if (number == 1015)
                Text.text = string.Format("������");
            else if (number == 1016)
                Text.text = string.Format("ũ��� �似��");
            else if (number == 1017)
                Text.text = string.Format("���ν�");
            else if (number == 1018)
                Text.text = string.Format("���� �Ʊ׷γ�");
            else if (number == 1019)
                Text.text = string.Format("��ũ�� 042351");
            else if (number == 1020)
                Text.text = string.Format("��Ÿ D31-2208");
            else if (number == 1021)
                Text.text = string.Format("��Ÿ D31-9523");
            else if (number == 1022)
                Text.text = string.Format("��Ÿ D31-12721");
            else if (number == 1023)
                Text.text = string.Format("������ O95-1125");
            else if (number == 1024)
                Text.text = string.Format("������ O95-2252");
            else if (number == 1025)
                Text.text = string.Format("������ O95-8510");
        }
    }
}
