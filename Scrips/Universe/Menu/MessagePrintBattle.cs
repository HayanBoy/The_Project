using UnityEngine.UI;
using UnityEngine;

public class MessagePrintBattle : MonoBehaviour
{
    public BattleMessages BattleMessages;

    public Text MessageText;

    //메시지 출력
    public void MessagePrint()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            if (BattleMessages.MessageType == 2)
                MessageText.text = string.Format("Are you sure you want to return to fleet after cencel the mission?");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            if (BattleMessages.MessageType == 2)
                MessageText.text = string.Format("임무를 취소하고 함대로 복귀하시겠습니까?");
        }
    }

    public void GameExit()
    {
        if (BattleSave.Save1.LanguageType == 1)
            MessageText.text = string.Format("Do you really wish to quit Utocalypse?");
        else if (BattleSave.Save1.LanguageType == 2)
            MessageText.text = string.Format("정말로 유토칼립스를 종료하시겠습니까?");
    }

    //세이브 삭제 메시지
    public void DeleteStart()
    {
        if (BattleSave.Save1.LanguageType == 1)
        {
            MessageText.text = string.Format("Are you sure you want to delete this file?");
        }
        else if (BattleSave.Save1.LanguageType == 2)
        {
            MessageText.text = string.Format("해당 저장 파일을 삭제하시겠습니까?");
        }
    }
}