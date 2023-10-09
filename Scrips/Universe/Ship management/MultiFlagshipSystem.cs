using System.Collections;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine;

public class MultiFlagshipSystem : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    public UIControlSystem UIControlSystem;
    public CameraZoom CameraZoom;
    public WordPrintSystem WordPrintSystem;
    public CameraFollow CameraFollow;
    public HurricaneOperationMenu HurricaneOperationMenu;

    Coroutine shipTableList;

    public bool FlagshipListMode; //���� ����Ʈ ���
    private bool FlagshipListClick; //���� ����Ʈ ��ư Ŭ�� ����ġ

    [Header("�ִϸ��̼�")]
    public GameObject FlagshipListButton;
    public Image FlagshipListButtonImage;

    [Header("���̺� ����Ʈ")]
    public GameObject TableWindow; //���̺� â
    public GameObject FlagshipTable1;
    public GameObject FlagshipTable2;
    public GameObject FlagshipTable3;
    public GameObject FlagshipTable4;
    public GameObject FlagshipTable5;
    public GameObject FlagshipTable1Click;
    public GameObject FlagshipTable2Click;
    public GameObject FlagshipTable3Click;
    public GameObject FlagshipTable4Click;
    public GameObject FlagshipTable5Click;

    [Header("���� ���� �ؽ�Ʈ ����Ʈ")]
    public Text Player1Name;
    public Text Player2Name;
    public Text Player3Name;
    public Text Player4Name;
    public Text Player5Name;

    public Text Player1Area;
    public Text Player2Area;
    public Text Player3Area;
    public Text Player4Area;
    public Text Player5Area;

    public Text Player1State;
    public Text Player2State;
    public Text Player3State;
    public Text Player4State;
    public Text Player5State;

    public int FlagshipNumber; //���õ� ���� ��ȣ

    [Header("����")]
    public AudioClip ButtonUIAudio;
    public AudioClip ListOpenAudio;

    //���� ����Ʈ â���
    public void FlagshipListButtonClick()
    {
        if (!FlagshipListMode)
        {
            if (HurricaneOperationMenu.MenuStep > 0)
                HurricaneOperationMenu.HurricaneOperationButtonClick();
            FlagshipListButton.GetComponent<Animator>().SetBool("Active, Flagship list", true);
            TableWindow.SetActive(true);
            shipTableList = StartCoroutine(ShipTableList());
            FlagshipTableClickUpdate();
        }
        else
        {
            FlagshipListButton.GetComponent<Animator>().SetBool("Active, Flagship list", false);
            TableWindow.SetActive(false);
            FlagshipTable1.SetActive(false);
            FlagshipTable2.SetActive(false);
            FlagshipTable3.SetActive(false);
            FlagshipTable4.SetActive(false);
            FlagshipTable5.SetActive(false);
        }

        FlagshipListMode = !FlagshipListMode;
    }
    public void FlagshipListButtonDown()
    {
        FlagshipListClick = true;
        UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
        FlagshipListButton.GetComponent<Animator>().SetBool("Click, Flagship list", true);
    }
    public void FlagshipListButtonUp()
    {
        if (FlagshipListClick == true)
        {
            FlagshipListButton.GetComponent<Animator>().SetBool("Click, Flagship list", false);
        }
        if (shipTableList != null)
            StopCoroutine(shipTableList);
        FlagshipListClick = false;
    }
    public void FlagshipListButtonEnter()
    {
        if (FlagshipListClick == true)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ButtonUIAudio);
            FlagshipListButton.GetComponent<Animator>().SetBool("Click, Flagship list", true);
        }
    }
    public void FlagshipListButtonExit()
    {
        if (FlagshipListClick == true)
        {
            FlagshipListButton.GetComponent<Animator>().SetBool("Click, Flagship list", false);
        }
    }

    //���� ����Ʈ ���� ��, ���õ� ���� ���ַ� Ŭ�� ������ ���
    public void FlagshipTableClickUpdate()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber == 0)
                FlagshipTable1Click.SetActive(true);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber == 1)
                FlagshipTable2Click.SetActive(true);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber == 2)
                FlagshipTable3Click.SetActive(true);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber == 3)
                FlagshipTable4Click.SetActive(true);
            else if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().FlagshipNumber == 4)
                FlagshipTable5Click.SetActive(true);
        }
    }

    //���� ��� ������Ʈ
    public void FlagshipListUpdate()
    {
        FlagshipTable1.SetActive(false);
        FlagshipTable2.SetActive(false);
        FlagshipTable3.SetActive(false);
        FlagshipTable4.SetActive(false);
        FlagshipTable5.SetActive(false);

        for (int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            if (i == 0)
                FlagshipTable1.SetActive(true);
            else if (i == 1)
                FlagshipTable2.SetActive(true);
            else if (i == 2)
                FlagshipTable3.SetActive(true);
            else if (i == 3)
                FlagshipTable4.SetActive(true);
            else if (i == 4)
                FlagshipTable5.SetActive(true);
        }
    }

    //���� ����Ʈ â Ȱ��ȭ �ִϸ��̼�
    IEnumerator ShipTableList()
    {
        for(int i = 0; i < ShipManager.instance.FlagShipList.Count; i++)
        {
            UniverseSoundManager.instance.UniverseUISoundPlayMaster("Clip", ListOpenAudio);
            if (i == 0)
                FlagshipTable1.SetActive(true);
            else if (i == 1)
                FlagshipTable2.SetActive(true);
            else if (i == 2)
                FlagshipTable3.SetActive(true);
            else if (i == 3)
                FlagshipTable4.SetActive(true);
            else if (i == 4)
                FlagshipTable5.SetActive(true);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    //��ư�� ���� ���� ��ȯ
    public void Player1FlagshipSelect()
    {
        ClickOff();
        FlagshipTable1Click.SetActive(true);
        CameraDampingZero();
        ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.FlagShipList[0];
        UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            StartCoroutine(CameraDampingOne());
        FlagshipNumber = 1;
        FlagshipControlMode();
        FormationSettingMode();
    }
    public void Player2FlagshipSelect()
    {
        ClickOff();
        FlagshipTable2Click.SetActive(true);
        CameraDampingZero();
        ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.FlagShipList[1];
        UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            StartCoroutine(CameraDampingOne());
        FlagshipNumber = 2;
        FlagshipControlMode();
        FormationSettingMode();
    }
    public void Player3FlagshipSelect()
    {
        ClickOff();
        FlagshipTable3Click.SetActive(true);
        CameraDampingZero();
        ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.FlagShipList[2];
        UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            StartCoroutine(CameraDampingOne());
        FlagshipNumber = 3;
        FlagshipControlMode();
        FormationSettingMode();
    }
    public void Player4FlagshipSelect()
    {
        ClickOff();
        FlagshipTable4Click.SetActive(true);
        CameraDampingZero();
        ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.FlagShipList[3];
        UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            StartCoroutine(CameraDampingOne());
        FlagshipNumber = 4;
        FlagshipControlMode();
        FormationSettingMode();
    }
    public void Player5FlagshipSelect()
    {
        ClickOff();
        FlagshipTable5Click.SetActive(true);
        CameraDampingZero();
        ShipManager.instance.SelectedFlagShip[0] = ShipManager.instance.FlagShipList[4];
        UIControlSystem.CVCamera.Follow = ShipManager.instance.SelectedFlagShip[0].transform;
        if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            StartCoroutine(CameraDampingOne());
        FlagshipNumber = 5;
        FlagshipControlMode();
        FormationSettingMode();
    }

    //������ ������ �Դ� ������� ���� �ҷ�����
    public void FlagshipControlMode()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipControlMode == false)
                {
                    UIControlSystem.ShipControlModeOff();
                }
                else
                {
                    UIControlSystem.ShipControlModeOn();
                }
            }
        }
    }

    //������ ������ �迭 ��带 �ҷ�����
    public void FormationSettingMode()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveActive == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveReady == false && ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().WarpDriveStopReady == false)
            {
                if (ShipManager.instance.SelectedFlagShip[0].GetComponent<MoveVelocity>().ShipSelectionMode == false)
                {
                    UIControlSystem.ShipFormationSettingModeOff();
                }
                else
                {
                    UIControlSystem.ShipFormationSettingModeOn();
                }
            }
        }
    }

    void CameraDampingZero()
    {
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
    }
    IEnumerator CameraDampingOne()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().Seleted = true; //�ش� ������ ���õǾ����� ����Ʈ�� ǥ��
            yield return new WaitForSecondsRealtime(0.05f);
            CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1;
            CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1;
            CameraZoom.CVCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 1;
        }
    }

    //�ٸ� �������� ī�޶� �����ϱ� ��, ������ ���õ� ���� ����
    void ClickOff()
    {
        if (ShipManager.instance.SelectedFlagShip.Count > 0 && ShipManager.instance.SelectedFlagShip[0].gameObject != null)
        {
            ShipManager.instance.SelectedFlagShip[0].GetComponent<HurricaneOperationForFlagship>().Seleted = false; //���� ���õ� ������ ���� ������ ����
            FlagshipTable1Click.SetActive(false);
            FlagshipTable2Click.SetActive(false);
            FlagshipTable3Click.SetActive(false);
            FlagshipTable4Click.SetActive(false);
            FlagshipTable5Click.SetActive(false);
        }
    }

    private void Update()
    {
        if (TableWindow.activeSelf == true)
        {
            if (ShipManager.instance.FlagShipList.Count > 0)
            {
                Player1Area.text = string.Format(ShipManager.instance.FlagShipList[0].GetComponent<FlagshipSystemNumber>().playerAreaName);
                if (ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip != null)
                {
                    if (WordPrintSystem.LanguageType == 1)
                        Player1State.text = string.Format("<color=#FF0000>Fighting</color>");
                    if (WordPrintSystem.LanguageType == 2)
                        Player1State.text = string.Format("<color=#FF0000>������</color>");
                }
                else
                {
                    if (ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().FlagshipMove == true)
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player1State.text = string.Format("<color=#FFF600>On the voyage</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player1State.text = string.Format("<color=#FFF600>������</color>");
                    }
                    else
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player1State.text = string.Format("<color=#00FF30>Standing By</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player1State.text = string.Format("<color=#00FF30>�����</color>");
                    }
                }

                if (ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[0].transform.position, ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player1State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                            Player1State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player1State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                            Player1State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                        }
                    }
                    if (WordPrintSystem.LanguageType == 2)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[0].transform.position, ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[0].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player1State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                            Player1State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player1State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                            Player1State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                        }
                    }
                }
            }
            if (ShipManager.instance.FlagShipList.Count > 1)
            {
                Player2Area.text = string.Format(ShipManager.instance.FlagShipList[1].GetComponent<FlagshipSystemNumber>().playerAreaName);
                if (ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip != null)
                {
                    if (WordPrintSystem.LanguageType == 1)
                        Player2State.text = string.Format("<color=#FF0000>Fighting</color>");
                    if (WordPrintSystem.LanguageType == 2)
                        Player2State.text = string.Format("<color=#FF0000>������</color>");
                }
                else
                {
                    if (ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().FlagshipMove == true)
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player2State.text = string.Format("<color=#FFF600>On the voyage</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player2State.text = string.Format("<color=#FFF600>������</color>");
                    }
                    else
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player2State.text = string.Format("<color=#00FF30>Standing By</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player2State.text = string.Format("<color=#00FF30>�����</color>");
                    }
                }

                if (ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[1].transform.position, ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player2State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                            Player2State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player2State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                            Player2State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                        }
                    }
                    if (WordPrintSystem.LanguageType == 2)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[1].transform.position, ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[1].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player2State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                            Player2State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player2State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                            Player2State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                        }
                    }
                }
            }
            if (ShipManager.instance.FlagShipList.Count > 2)
            {
                Player3Area.text = string.Format(ShipManager.instance.FlagShipList[2].GetComponent<FlagshipSystemNumber>().playerAreaName);
                if (ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip != null)
                {
                    if (WordPrintSystem.LanguageType == 1)
                        Player3State.text = string.Format("<color=#FF0000>Fighting</color>");
                    if (WordPrintSystem.LanguageType == 2)
                        Player3State.text = string.Format("<color=#FF0000>������</color>");
                }
                else
                {
                    if (ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().FlagshipMove == true)
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player3State.text = string.Format("<color=#FFF600>On the voyage</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player3State.text = string.Format("<color=#FFF600>������</color>");
                    }
                    else
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player3State.text = string.Format("<color=#00FF30>Standing By</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player3State.text = string.Format("<color=#00FF30>�����</color>");
                    }
                }

                if (ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[2].transform.position, ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player3State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                            Player3State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player3State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                            Player3State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                        }
                    }
                    if (WordPrintSystem.LanguageType == 2)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[2].transform.position, ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[2].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player3State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                            Player3State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player3State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                            Player3State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                        }
                    }
                }
            }
            if (ShipManager.instance.FlagShipList.Count > 3)
            {
                Player4Area.text = string.Format(ShipManager.instance.FlagShipList[3].GetComponent<FlagshipSystemNumber>().playerAreaName);
                if (ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip != null)
                {
                    if (WordPrintSystem.LanguageType == 1)
                        Player4State.text = string.Format("<color=#FF0000>Fighting</color>");
                    if (WordPrintSystem.LanguageType == 2)
                        Player4State.text = string.Format("<color=#FF0000>������</color>");
                }
                else
                {
                    if (ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().FlagshipMove == true)
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player4State.text = string.Format("<color=#FFF600>On the voyage</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player4State.text = string.Format("<color=#FFF600>������</color>");
                    }
                    else
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player4State.text = string.Format("<color=#00FF30>Standing By</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player4State.text = string.Format("<color=#00FF30>�����</color>");
                    }
                }

                if (ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[3].transform.position, ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player4State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                            Player4State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player4State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                            Player4State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                        }
                    }
                    if (WordPrintSystem.LanguageType == 2)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[3].transform.position, ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[3].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player4State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                            Player4State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player4State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                            Player4State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                        }
                    }
                }
            }
            if (ShipManager.instance.FlagShipList.Count > 4)
            {
                Player5Area.text = string.Format(ShipManager.instance.FlagShipList[4].GetComponent<FlagshipSystemNumber>().playerAreaName);
                if (ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().Turret1.GetComponent<NarihaTurretAttackSystem>().TargetShip != null)
                {
                    if (WordPrintSystem.LanguageType == 1)
                        Player5State.text = string.Format("<color=#FF0000>Fighting</color>");
                    if (WordPrintSystem.LanguageType == 2)
                        Player5State.text = string.Format("<color=#FF0000>������</color>");
                }
                else
                {
                    if (ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().FlagshipMove == true)
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player5State.text = string.Format("<color=#FFF600>On the voyage</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player5State.text = string.Format("<color=#FFF600>������</color>");
                    }
                    else
                    {
                        if (WordPrintSystem.LanguageType == 1)
                            Player5State.text = string.Format("<color=#00FF30>Standing By</color>");
                        if (WordPrintSystem.LanguageType == 2)
                            Player5State.text = string.Format("<color=#00FF30>�����</color>");
                    }
                }

                if (ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().WarpDriveActive == true)
                {
                    if (WordPrintSystem.LanguageType == 1)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[4].transform.position, ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player5State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                            Player5State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}min {0:F0}sec</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player5State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                            Player5State.text = string.Format("<color=#00FFDA>In Warp</color>\n<color=#FF7F00>{0:F0}sec</color>", timeTaken);
                        }
                    }
                    if (WordPrintSystem.LanguageType == 2)
                    {
                        float Distance = Vector3.Distance(ShipManager.instance.FlagShipList[4].transform.position, ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().DestinationArea);
                        float timeTaken = Distance / ShipManager.instance.FlagShipList[4].GetComponent<MoveVelocity>().WarpSpeed;

                        if (timeTaken >= 60)
                        {
                            Player5State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                            Player5State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}�� {0:F0}��</color>", timeTaken / 60, timeTaken - 60);
                        }
                        else if (timeTaken < 60)
                        {
                            Player5State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                            Player5State.text = string.Format("<color=#00FFDA>������</color>\n<color=#FF7F00>{0:F0}��</color>", timeTaken);
                        }
                    }
                }
            }
        }
    }
}