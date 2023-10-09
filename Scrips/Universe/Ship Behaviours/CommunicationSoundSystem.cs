using System.Collections;
using UnityEngine;

public class CommunicationSoundSystem : MonoBehaviour
{
    [Header("설정")]
    public bool CommunicationOn; //일반 통신 여부
    public bool InBattle; //전투 통신 여부
    public int BattleBigCommunicationNember; //전투 큰통신 갯수
    public int BattleCommunicationNember; //전투 통신 갯수
    public int CommunicationNember; //일반 통신 갯수

    [Header("일반 통신 소리")]
    public GameObject sound1;
    public GameObject sound2;
    public GameObject sound3;
    public GameObject sound4;
    public GameObject sound5;
    public GameObject sound6;
    public GameObject sound7;
    public GameObject sound8;
    public GameObject sound9;
    public GameObject sound10;

    [Header("적 발견 통신 소리")]
    public GameObject EnemyFindSound1;
    public GameObject EnemyFindSound2;

    [Header("교전 통신 소리")]
    public GameObject BattleSound1;
    public GameObject BattleSound2;
    public GameObject BattleSound3;
    public GameObject BattleSound4;
    public GameObject BattleSound5;

    [Header("교전 큰 짧은 통신 소리")]
    public GameObject BattleBigSound1;
    public GameObject BattleBigSound2;
    public GameObject BattleBigSound3;
    public GameObject BattleBigSound4;
    public GameObject BattleBigSound5;
    public GameObject BattleBigSound6;
    public GameObject BattleBigSound7;
    public GameObject BattleBigSound8;
    public GameObject BattleBigSound9;
    public GameObject BattleBigSound10;

    void Start()
    {
        if (CommunicationOn == true)
            StartCoroutine(SoundRandom());
    }

    //일반 통신 반복
    IEnumerator SoundRandom()
    {
        while(true)
        {
            int RandomSound = Random.Range(0, CommunicationNember);

            if (RandomSound == 0)
                sound1.SetActive(true);
            else if (RandomSound == 1)
                sound2.SetActive(true);
            else if (RandomSound == 2)
                sound3.SetActive(true);
            else if (RandomSound == 3)
                sound4.SetActive(true);
            else if (RandomSound == 4)
                sound5.SetActive(true);
            else if (RandomSound == 5)
                sound6.SetActive(true);
            else if (RandomSound == 6)
                sound7.SetActive(true);
            else if (RandomSound == 7)
                sound8.SetActive(true);
            else if (RandomSound == 8)
                sound9.SetActive(true);
            else if (RandomSound == 9)
                sound10.SetActive(true);
            yield return new WaitForSeconds(30);

            if (sound1 != null)
                sound1.SetActive(false);
            if (sound2 != null)
                sound2.SetActive(false);
            if (sound3 != null)
                sound3.SetActive(false);
            if (sound4 != null)
                sound4.SetActive(false);
            if (sound5 != null)
                sound5.SetActive(false);
            if (sound6 != null)
                sound6.SetActive(false);
            if (sound7 != null)
                sound7.SetActive(false);
            if (sound8 != null)
                sound8.SetActive(false);
            if (sound9 != null)
                sound9.SetActive(false);
            if (sound10 != null)
                sound10.SetActive(false);
        }
    }

    //적 함선 발견 시 통신
    public IEnumerator WarnningCommunication()
    {
        int RandomSound = Random.Range(0, 2);

        if (RandomSound == 0)
            EnemyFindSound1.SetActive(true);
        else if (RandomSound == 1)
            EnemyFindSound2.SetActive(true);

        yield return new WaitForSeconds(10);

        EnemyFindSound1.SetActive(false);
        EnemyFindSound2.SetActive(false);
    }

    //교전 통신 반복
    public IEnumerator BattleSoundRandom()
    {
        if (InBattle == true)
        {
            while (true)
            {
                int RandomSound = Random.Range(0, BattleCommunicationNember);

                if (RandomSound == 0)
                    BattleSound1.SetActive(true);
                else if (RandomSound == 1)
                    BattleSound2.SetActive(true);
                else if (RandomSound == 2)
                    BattleSound3.SetActive(true);
                else if (RandomSound == 3)
                    BattleSound4.SetActive(true);
                else if (RandomSound == 4)
                    BattleSound5.SetActive(true);
                yield return new WaitForSeconds(30);

                if (BattleSound1 != null)
                    BattleSound1.SetActive(false);
                if (BattleSound2 != null)
                    BattleSound2.SetActive(false);
                if (BattleSound3 != null)
                    BattleSound3.SetActive(false);
                if (BattleSound4 != null)
                    BattleSound4.SetActive(false);
                if (BattleSound5 != null)
                    BattleSound5.SetActive(false);
            }
        }
    }

    //교전 큰 짧은 통신 반복
    public IEnumerator BattleBigSoundRandom()
    {
        if (InBattle == true)
        {
            while (true)
            {
                int RandomSound = Random.Range(0, BattleCommunicationNember);

                if (RandomSound == 0)
                    BattleBigSound1.SetActive(true);
                else if (RandomSound == 1)
                    BattleBigSound2.SetActive(true);
                else if (RandomSound == 2)
                    BattleBigSound3.SetActive(true);
                else if (RandomSound == 3)
                    BattleBigSound4.SetActive(true);
                else if (RandomSound == 4)
                    BattleBigSound5.SetActive(true);
                else if (RandomSound == 5)
                    BattleBigSound2.SetActive(true);
                else if (RandomSound == 6)
                    BattleBigSound3.SetActive(true);
                else if (RandomSound == 7)
                    BattleBigSound4.SetActive(true);
                else if (RandomSound == 8)
                    BattleBigSound5.SetActive(true);
                yield return new WaitForSeconds(10);

                if (BattleBigSound1 != null)
                    BattleBigSound1.SetActive(false);
                if (BattleBigSound2 != null)
                    BattleBigSound2.SetActive(false);
                if (BattleBigSound3 != null)
                    BattleBigSound3.SetActive(false);
                if (BattleBigSound4 != null)
                    BattleBigSound4.SetActive(false);
                if (BattleBigSound5 != null)
                    BattleBigSound5.SetActive(false);
                if (BattleBigSound6 != null)
                    BattleBigSound6.SetActive(false);
                if (BattleBigSound7 != null)
                    BattleBigSound7.SetActive(false);
                if (BattleBigSound8 != null)
                    BattleBigSound8.SetActive(false);
                if (BattleBigSound9 != null)
                    BattleBigSound9.SetActive(false);
            }
        }
    }

    //교전 종료
    public void BattleEnd()
    {
        if (BattleSound1 != null)
            BattleSound1.SetActive(false);
        if (BattleSound2 != null)
            BattleSound2.SetActive(false);
        if (BattleSound3 != null)
            BattleSound3.SetActive(false);
        if (BattleSound4 != null)
            BattleSound4.SetActive(false);
        if (BattleSound5 != null)
            BattleSound5.SetActive(false);

        if (BattleBigSound1 != null)
            BattleBigSound1.SetActive(false);
        if (BattleBigSound2 != null)
            BattleBigSound2.SetActive(false);
        if (BattleBigSound3 != null)
            BattleBigSound3.SetActive(false);
        if (BattleBigSound4 != null)
            BattleBigSound4.SetActive(false);
        if (BattleBigSound5 != null)
            BattleBigSound5.SetActive(false);
        if (BattleBigSound6 != null)
            BattleBigSound6.SetActive(false);
        if (BattleBigSound7 != null)
            BattleBigSound7.SetActive(false);
        if (BattleBigSound8 != null)
            BattleBigSound8.SetActive(false);
        if (BattleBigSound9 != null)
            BattleBigSound9.SetActive(false);
    }
}