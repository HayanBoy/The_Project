using UnityEngine;

public class DeltaHurricaneMissionSystemOption : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public BattleMessages BattleMessages;

    [Header("��Ÿ �㸮���� �ӹ� ���")]
    public GameObject MissionCancelPrefab;
    private bool CancelClick;

    [Header("����")]
    public AudioClip CancelUIAudio;

    //�ӹ� ��� ��ư
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