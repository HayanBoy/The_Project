using UnityEngine.UI;
using UnityEngine;

public class UpgradeTableInformation2 : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public UpgradeMenu UpgradeMenu;

    [Header("���̺� ����")]
    public Text NameText1;
    public Text ExplainText1;
    public int MainTabNumber;
    public int SubTabNumber;
    public int AccessNumber;
    public int UpgradeNumber; //���׷��̵� ��ư ��ȣ
    public int SubUpgradeNumber; //���� ���׷��̵� ��ư ��ȣ

    //���׷��̵� �׸�
    public void TableTextChange(string Name, string ExplainText)
    {
        NameText1.text = Name;
        ExplainText1.text = ExplainText;
    }

    //���׷��̵� ���� ��ư
    public void UpgradeButtonClick()
    {
        UpgradeMenu.UpgradeMessage(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
    }
}