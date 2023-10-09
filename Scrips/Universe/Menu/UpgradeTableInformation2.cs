using UnityEngine.UI;
using UnityEngine;

public class UpgradeTableInformation2 : MonoBehaviour
{
    [Header("스크립트")]
    public UpgradeMenu UpgradeMenu;

    [Header("테이블 정보")]
    public Text NameText1;
    public Text ExplainText1;
    public int MainTabNumber;
    public int SubTabNumber;
    public int AccessNumber;
    public int UpgradeNumber; //업그레이드 버튼 번호
    public int SubUpgradeNumber; //서브 업그레이드 버튼 번호

    //업그레이드 항목
    public void TableTextChange(string Name, string ExplainText)
    {
        NameText1.text = Name;
        ExplainText1.text = ExplainText;
    }

    //업그레이드 진행 버튼
    public void UpgradeButtonClick()
    {
        UpgradeMenu.UpgradeMessage(MainTabNumber, SubTabNumber, AccessNumber, UpgradeNumber, SubUpgradeNumber);
    }
}