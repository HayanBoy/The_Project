using UnityEngine;

public class DeltaHurricaneMissionSystemOption : MonoBehaviour
{
    [Header("스크립트")]
    public BattleMessages BattleMessages;

    [Header("델타 허리케인 임무 취소")]
    public GameObject MissionCancelPrefab;
    private bool CancelClick;

    [Header("사운드")]
    public AudioClip CancelUIAudio;

    //임무 취소 버튼
    public void MissionCancelButtonClick()
    {
        StartCoroutine(BattleMessages.MessageStart(2, 0, 2));
    }
    public void MissionCancelButtonDown()
    {
        CancelClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
        MissionCancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
    }
    public void MissionCancelButtonUp()
    {
        if (CancelClick == true)
        {
            MissionCancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
        CancelClick = false;
    }
    public void MissionCancelButtonEnter()
    {
        if (CancelClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", CancelUIAudio);
            MissionCancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", true);
        }
    }
    public void MissionCancelButtonExit()
    {
        if (CancelClick == true)
        {
            MissionCancelPrefab.GetComponent<Animator>().SetBool("touch(down), cancel menu button", false);
        }
    }
}