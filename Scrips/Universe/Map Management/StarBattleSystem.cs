using System.Collections.Generic;
using UnityEngine;

public class StarBattleSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public LiveCommunicationSystem LiveCommunicationSystem;
    AreaStatement AreaStatement;

    [Header("�׼� ����")]
    public int StarNumber;
    public bool isInFight; //������ ���۵Ǹ� ����ġ�� ������.
    public bool EnemySpawn = false; //���� �����Ǿ����� ����
    public bool isFree = false; //�ع�Ǿ����� ����

    [Header("�Ҽӵ� �༺ ����Ʈ")]
    public GameObject Planet1;
    public GameObject Planet2;
    public GameObject Planet3;
    public GameObject Planet4;
    public GameObject Planet5;

    [Header("�׼� �ع� ����")]
    public int MinusFlagship; //��� ���� ��
    public int MinusFormationShip; //��� ����� ��
    public bool isSupported = false; //�׼� �߿��� �����Դ밡 �Ҽ��� �ִ����� ���� ����, Ȱ��ȭ�صθ� �ش� �׼��� �ع�� CantSupport�� �������� �����ȴ�.
    public bool CantSupport = false; //Ȱ��ȭ ��, �ش� ������ ���� �Դ븦 �ҷ��� �� ���� �ȴ�.

    [Header("�Դ� �ֵ� ��Ȳ")]
    public bool FlagshipHere;
    public bool EnemyShipHere;

    [Header("Ÿ�̸�")]
    public float BattleTimer; //�������� ��, ��� �ð� ����

    [Header("�Դ� ����Ʈ")]
    public List<GameObject> FlagshipShipList = new List<GameObject>(); //�湮�� ������ ����
    public List<GameObject> BattleEnemyShipList = new List<GameObject>();

    private void Start()
    {
        AreaStatement = FindObjectOfType<AreaStatement>();

        //������ �ҷ�����
        if (StarNumber == 1 && AreaStatement.ToropioStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 2 && AreaStatement.Roro1StarState == 1)
            StartGetStarFree();
        else if (StarNumber == 3 && AreaStatement.Roro2StarState == 1)
            StartGetStarFree();
        else if (StarNumber == 4 && AreaStatement.SarisiStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 5 && AreaStatement.GarixStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 6 && AreaStatement.SecrosStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 7 && AreaStatement.TeretosStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 8 && AreaStatement.MiniPopoStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 9 && AreaStatement.DeltaD31_4AStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 10 && AreaStatement.DeltaD31_4BStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 11 && AreaStatement.JeratoO95_7AStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 12 && AreaStatement.JeratoO95_7BStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 13 && AreaStatement.JeratoO95_14CStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 14 && AreaStatement.JeratoO95_14DStarState == 1)
            StartGetStarFree();
        else if (StarNumber == 15 && AreaStatement.JeratoO95_OmegaStarState == 1)
            StartGetStarFree();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && FlagshipHere == false) //������ �ش� �׼��� ������ �Ʊ� �Դ� Ȱ��ȭ
        {
            FlagshipHere = true;
            FlagshipShipList.Add(collision.gameObject);

            if (GetComponent<EnemyGet>().enabled == false && isFree == false) //�������� ���� �� ���� Ȱ��ȭ
            {
                GetComponent<EnemyGet>().WarpFleetDestination = collision.gameObject.transform.position;
                GetComponent<EnemyGet>().enabled = true;
            }
        }
        if (collision.gameObject.layer == 7) //�� �Դ밡 �ش� �׼��� ������ �� ���� Ȱ��ȭ
        {
            EnemyShipHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && collision.CompareTag("Player Flag Ship") && FlagshipHere == true) //������ �ش� �׼��� ������ �Ʊ� �Դ� ��Ȱ��ȭ
        {
            FlagshipHere = false;
            FlagshipShipList.Remove(collision.gameObject);
        }
        if (collision.gameObject.layer == 7) //�� �Դ밡 �ش� �༺���� ��� ��ħ�Ǹ� �� ���� ��Ȱ��ȭ
        {
            if (collision.CompareTag("Slorius Flag Ship") || collision.CompareTag("Slorius Follow Ship"))
            {
                if (collision.gameObject != null || collision.gameObject.activeSelf == false)
                    BattleEnemyShipList.Remove(collision.gameObject);
                else if (collision.gameObject == null)
                    BattleEnemyShipList.Remove(collision.gameObject);
            }
            if (collision.CompareTag("Kantakri Flag Ship1") || collision.CompareTag("Kantakri Follow Ship1"))
            {
                if (collision.transform.parent.gameObject != null || collision.transform.parent.gameObject.activeSelf == false)
                    BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
                else if (collision.transform.parent.gameObject == null)
                    BattleEnemyShipList.Remove(collision.transform.parent.gameObject);
            }
            if (FlagshipHere == true && isInFight  == true && BattleEnemyShipList.Count == 0)
            {
                EnemyShipHere = false;
                isInFight = false;
                EnemySpawn = false;
                PlanetsFree();
            }
        }
    }

    private void Update()
    {
        if (isInFight == true && FlagshipHere == false && BattleEnemyShipList.Count > 0) //���� ���� ��, ���� Ÿ�̸Ӱ� �ߵ��Ǹ� ���� �ð��� ������, �ڵ����� �ش� ����Ʈ�� ��� ���� �����Ǹ�, ������
        {
            if (BattleTimer <= 300)
            {
                BattleTimer += Time.deltaTime;
            }
            else
            {
                BattleTimer = 0;
                isInFight = false;
                EnemySpawn = false;
                EnemyShipHere = false;

                int Amount = BattleEnemyShipList.Count;
                for (int i = 0; i < Amount; i++)
                {
                    Destroy(BattleEnemyShipList[0], 0.1f);
                    BattleEnemyShipList.Remove(BattleEnemyShipList[0]);
                }

                GetComponent<EnemyGet>().enabled = false;
                GetComponent<EnemyGet>().FlagshipWarp = false;
                GetComponent<EnemyGet>().WarpControsType = 0;
            }
        }
    }

    //�׼����� �¸� ��, �� �༺�� �Դ���� ��� �� ���� ����
    void PlanetsFree()
    {
        if (Planet1 != null)
        {
            if (Planet1.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet1.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet1.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet1.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet2.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet2.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet2.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet3.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet3.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet3.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet4.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet4.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet4.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet5.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet5.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet5.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (isSupported == true)
            CantSupport = true;

        if (StarNumber == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Toropio";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "����ǿ�";
            AreaStatement.ToropioStarState = 1;
        }
        else if (StarNumber == 2)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro I";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�η� I";
            AreaStatement.Roro1StarState = 1;
        }
        else if (StarNumber == 3)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro II";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�η� II";
            AreaStatement.Roro2StarState = 1;
        }
        else if (StarNumber == 4)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Sarisi";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�縮��";
            AreaStatement.SarisiStarState = 1;
        }
        else if (StarNumber == 5)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Garix";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������";
            AreaStatement.GarixStarState = 1;
        }
        else if (StarNumber == 6)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Secros";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "��ũ�ν�";
            AreaStatement.SecrosStarState = 1;
        }
        else if (StarNumber == 7)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Teretos";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�׷��佺";
            AreaStatement.TeretosStarState = 1;
        }
        else if (StarNumber == 8)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Mini popo";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�̴� ����";
            AreaStatement.MiniPopoStarState = 1;
        }
        else if (StarNumber == 9)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-4A";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "��Ÿ D31-4A";
            AreaStatement.DeltaD31_4AStarState = 1;
        }
        else if (StarNumber == 10)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-4B";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "��Ÿ D31-4B";
            AreaStatement.DeltaD31_4BStarState = 1;
        }
        else if (StarNumber == 11)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-7A";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-7A";
            AreaStatement.JeratoO95_7AStarState = 1;
        }
        else if (StarNumber == 12)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-7B";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-7B";
            AreaStatement.JeratoO95_7BStarState = 1;
        }
        else if (StarNumber == 13)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-14C";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-14C";
            AreaStatement.JeratoO95_14CStarState = 1;
        }
        else if (StarNumber == 14)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-14D";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-14D";
            AreaStatement.JeratoO95_14DStarState = 1;
        }
        else if (StarNumber == 15)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-Omega";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-���ް�";
            AreaStatement.JeratoO95_OmegaStarState = 1;
        }

        StartCoroutine(LiveCommunicationSystem.MainCommunication(3.01f));
    }

    //�׼� �ع� ���� �ҷ�����
    void StartGetStarFree()
    {
        isFree = true;

        if (Planet1 != null)
        {
            if (Planet1.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet1.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet1.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet1.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet2.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet2.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet2.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet3.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet3.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet3.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet4.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet4.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet4.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<EnemyGet>().MaxFlagship > 0)
                Planet5.GetComponent<EnemyGet>().MaxFlagship -= MinusFlagship;
            if (Planet5.GetComponent<EnemyGet>().MaxFormationShip > 0)
                Planet5.GetComponent<EnemyGet>().MaxFormationShip -= MinusFormationShip;
        }
        if (isSupported == true)
            CantSupport = true;
    }

    //�ڽ��� �༺���� ���¸� �ҷ�����
    public void GetPlanetState()
    {
        int Planets = 0;
        int FreePlanets = 0;

        if (Planet1 != null)
            Planets++;
        if (Planet2 != null)
            Planets++;
        if (Planet3 != null)
            Planets++;
        if (Planet4 != null)
            Planets++;
        if (Planet5 != null)
            Planets++;

        //�༺�� �ع�Ǿ����� Ȯ��
        if (Planet1 != null)
        {
            if (Planet1.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet2 != null)
        {
            if (Planet2.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet3 != null)
        {
            if (Planet3.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet4 != null)
        {
            if (Planet4.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }
        if (Planet5 != null)
        {
            if (Planet5.GetComponent<PlanetOurForceShipsManager>().FirstFree == true)
                FreePlanets++;
        }

        //�Ҽӵ� �༺�� ��� �ع�Ǿ� ���� ���, ���� �׼� ���� �ع� ó��
        if (FreePlanets == Planets)
        {
            if (StarNumber == 1)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[0] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[0] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.ToropioStarState = 1;
            }
            else if (StarNumber == 2)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[1] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[1] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.Roro1StarState = 1;
                AreaStatement.Roro2StarState = 1;
            }
            else if (StarNumber == 4)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[2] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[2] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.SarisiStarState = 1;
            }
            else if (StarNumber == 5)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[3] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[3] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.GarixStarState = 1;
            }
            else if (StarNumber == 6)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[4] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[4] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.SecrosStarState = 1;
                AreaStatement.TeretosStarState = 1;
                AreaStatement.MiniPopoStarState = 1;
            }
            else if (StarNumber == 9)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[5] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[5] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.DeltaD31_4AStarState = 1;
                AreaStatement.DeltaD31_4BStarState = 1;
            }
            else if (StarNumber == 11)
            {
                if (MissionCompleteManager.MCMInstance.StarFreeList[6] == false)
                {
                    MissionCompleteManager.MCMInstance.StarFreeList[6] = true;
                    PrintLiveText();
                    StartCoroutine(LiveCommunicationSystem.MainCommunication(4.01f));
                }
                AreaStatement.JeratoO95_7AStarState = 1;
                AreaStatement.JeratoO95_7BStarState = 1;
                AreaStatement.JeratoO95_14CStarState = 1;
                AreaStatement.JeratoO95_14DStarState = 1;
                AreaStatement.JeratoO95_OmegaStarState = 1;
            }
        }
    }

    //�׼� �ع����� �� ��ȭ��
    void PrintLiveText()
    {
        if (StarNumber == 1)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Toropio";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "����ǿ�";
        }
        else if (StarNumber == 2 || StarNumber == 3)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Roro";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�η�";
        }
        else if (StarNumber == 4)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Sarisi";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "�縮��";
        }
        else if (StarNumber == 5)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Garix";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������";
        }
        else if (StarNumber == 6 || StarNumber == 7 || StarNumber == 8)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "OctoKrasis Patoro";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "����ũ��ý� �����";
        }
        else if (StarNumber == 9 || StarNumber == 10)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Delta D31-402054";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "��Ÿ D31-402054";
        }
        else if (StarNumber == 11 || StarNumber == 12 || StarNumber == 13 || StarNumber == 14 || StarNumber == 15)
        {
            if (BattleSave.Save1.LanguageType == 1)
                LiveCommunicationSystem.PlanetName = "Jerato O95-99024";
            else if (BattleSave.Save1.LanguageType == 2)
                LiveCommunicationSystem.PlanetName = "������ O95-99024";
        }
    }
}