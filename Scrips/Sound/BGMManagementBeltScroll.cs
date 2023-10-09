using System.Collections;
using UnityEngine;

public class BGMManagementBeltScroll : MonoBehaviour
{
    [Header("BGM 오디오 소스")]
    public AudioSource BGM;

    [Header("전투 BGM")]
    public AudioClip AdvanceBattleBGM1;
    public AudioClip AdvanceBattleBGM2;
    public AudioClip AdvanceBattleBGM3;

    public AudioClip DefenceBattleBGM1;
    public AudioClip DefenceBattleBGM2;
    public AudioClip DefenceBattleBGM3;

    public AudioClip InfectionBattleBGM1;
    public AudioClip InfectionBattleBGM2;
    public AudioClip InfectionBattleBGM3;

    private int PlayNumber; //BGM 번호. 1 = 진격전, 2 = 방어전, 3 = 감염전
    private float SoundPlayTime;

    private void Start()
    {
        if (BattleSave.Save1.MissionType == 1 || BattleSave.Save1.MissionType == 100 || BattleSave.Save1.MissionType == 101) //컨트로스 지역 진격전 및 기함 침투적전
            PlayNumber = 1;
        else if (BattleSave.Save1.MissionType == 2) //진지 방어전
            PlayNumber = 2;
        else if (BattleSave.Save1.MissionType == 3 || BattleSave.Save1.MissionType == 4) //감염 제거전 및 저지전
            PlayNumber = 3;
    }

    private void Update()
    {
        //BGM이 종료되면 다음 BGM으로 변경
        if (BGM.isPlaying == false || BGM.clip == null)
        {
            if (SoundPlayTime == 0)
            {
                SoundPlayTime += Time.deltaTime;
                StartCoroutine(BGMStart());
            }
        }
    }

    IEnumerator BGMStart()
    {
        int RandomBGMTime = Random.Range(2, 5);
        yield return new WaitForSeconds(RandomBGMTime);

        int RandomBGM = Random.Range(0, 3);

        if (RandomBGM == 0)
        {
            if (PlayNumber == 1)
                BGM.clip = AdvanceBattleBGM1;
            else if (PlayNumber == 2)
                BGM.clip = DefenceBattleBGM1;
            else if (PlayNumber == 3)
                BGM.clip = InfectionBattleBGM1;
        }
        else if (RandomBGM == 1)
        {
            if (PlayNumber == 1)
                BGM.clip = AdvanceBattleBGM2;
            else if (PlayNumber == 2)
                BGM.clip = DefenceBattleBGM2;
            else if (PlayNumber == 3)
                BGM.clip = InfectionBattleBGM2;
        }
        else if (RandomBGM == 2)
        {
            if (PlayNumber == 1)
                BGM.clip = AdvanceBattleBGM3;
            else if (PlayNumber == 2)
                BGM.clip = DefenceBattleBGM3;
            else if (PlayNumber == 3)
                BGM.clip = InfectionBattleBGM3;
        }

        BGM.Play();
        SoundPlayTime = 0;
    }
}