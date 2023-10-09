using UnityEngine.UI;
using UnityEngine;

public class WordPrintMenu : MonoBehaviour
{
    [Header("스크립트")]
    public WordPrintSystem WordPrintSystem;
    public SystemMessages SystemMessages;
    public FleetFormationMenuSystem FleetFormationMenuSystem;
    public UpgradeMenu UpgradeMenu;

    public int PrintNumber;
    public int PrintType;

    [Header("나가기 및 취소 버튼")]
    public Text ExitButtonName;

    [Header("함대 장비 메뉴 함포 슬롯별 설명창")]
    public Text FleetMenuTurret1Name;
    public Text FleetMenuTurret2Name;
    public Text FleetMenuTurret3Name;
    public Text FleetMenuTurret4Name;

    public Text FleetMenuTurret1Explain;
    public Text FleetMenuTurret2Explain;
    public Text FleetMenuTurret3Explain;
    public Text FleetMenuTurret4Explain;

    [Header("함대 장비 메뉴 설명창")]
    public Text FleetMenuName; //선택된 함대 이름
    public Text FleetMenuTurretName;
    public Text FleetMenuTurretExplain;

    [Header("함대 배열 메뉴 설명창")]
    public Text FleetName;
    public Text InformationFormationShip;
    public Text InformationShieldShip;
    public Text InformationCarrier;
    public Text TotalInformation;
    public int ShipExperienceData = 1;

    [Header("함대 배열 메뉴 함선 설명창")]
    public RectTransform FleetMenuShipImage;
    public Text FleetMenuShipName;
    public Text FleetMenuShipExplainText;
    public RectTransform FleetMenuExplainTextSize;

    [Header("함대 배열 메뉴 함선 이미지")]
    public Sprite FormationShipSprite;
    public Sprite ShieldShipSprite;
    public Sprite CarrierSprite;

    [Header("함대 배열 메뉴 배열 관리창")]
    public Text ManagerTotalFleets;

    [Header("관리창 메시지")]
    public Text ProgressText1; //진행 창1에서의 메시지
    public Text ProgressText2; //진행 창2에서의 메시지
    public Text ManagerSelectedShip; //메인 메시지 창의 텍스트 내용
    public Text FleetManagerSelectExplain; //함대 선택 창에서의 함대 선택 메시지
    public Text FleetManagerSelectedShip; //함대 선택 창에서의 선택된 함대
    public Text WarnningFleetManager1;
    public Text FleetAddCostText; //함대 확장시 나타나는 비용 텍스트
    public Text ShipProduectionCostText; //함선 생산시 나타나는 비용 텍스트

    [Header("기함 관리 메뉴 설명창")]
    public Text FlagshipMenuSlot1Name;
    public Text FlagshipMenuSlot2Name;
    public Text FlagshipMenuSlot3Name;
    public Text FlagshipMenuSlot4Name;

    public Text FlagshipMenuSlot1Explain;
    public Text FlagshipMenuSlot2Explain;
    public Text FlagshipMenuSlot3Explain;
    public Text FlagshipMenuSlot4Explain;

    [Header("기함 관리 메뉴 설명창")]
    public Text FlagshipMenuSkillName;
    public Text FlagshipMenuSkillExplain;
    public Text FlagshipAmountText; //기함 보유 텍스트

    [Header("함대 명칭")]
    public Text Player1;
    public Text Player2;
    public Text Player3;
    public Text Player4;
    public Text Player5;

    [Header("업그레이드 메시지창")]
    public GameObject UpgradeTextsPrefab;
    public Text MainUpgrade1NameText; //업그레이드 테이블1의 텍스트
    public Text MainUpgrade1ExplainText; //업그레이드 테이블1의 텍스트
    public Text PresentStepUpgrade; //현재 스탯 상태 메시지
    public Text NextStepUpgrade; //업그레이드 이후의 스탯 상태 메시지
    public Text TotalCostText; //업그레이드 비용 메시지

    //나가기 버튼 텍스트 출력
    public void FleetMenuTurretSlotExplain(int number, Text text)
    {
        ExitButtonName = text; //각 메뉴의 나가기 버튼의 텍스트로 텍스트를 변경

        if (WordPrintSystem.LanguageType == 1)
        {
            if (number == 1)
                ExitButtonName.text = string.Format("Exit");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (number == 1)
                ExitButtonName.text = string.Format("나가기");
        }
    }

    //함대 장비 메뉴에서의 선택된 함대 이름 표시창
    public void FleetMenuSelectedFleetNamePrint(int number, bool boolean)
    {
        if (boolean == true) //함대 선택창이 확장된 상태
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    FleetMenuName.text = string.Format("Selected fleet : " + Player1.text);
                else if (number == 1)
                    FleetMenuName.text = string.Format("Selected fleet : " + Player2.text);
                else if (number == 2)
                    FleetMenuName.text = string.Format("Selected fleet : " + Player3.text);
                else if (number == 3)
                    FleetMenuName.text = string.Format("Selected fleet : " + Player4.text);
                else if (number == 4)
                    FleetMenuName.text = string.Format("Selected fleet : " + Player5.text);
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    FleetMenuName.text = string.Format("선택된 함대 : " + Player1.text);
                else if (number == 1)
                    FleetMenuName.text = string.Format("선택된 함대 : " + Player2.text);
                else if (number == 2)
                    FleetMenuName.text = string.Format("선택된 함대 : " + Player3.text);
                else if (number == 3)
                    FleetMenuName.text = string.Format("선택된 함대 : " + Player4.text);
                else if (number == 4)
                    FleetMenuName.text = string.Format("선택된 함대 : " + Player5.text);
            }
        }
        else
        {
            if (number == 0)
                FleetMenuName.text = Player1.text;
            else if (number == 1)
                FleetMenuName.text = Player2.text;
            else if (number == 2)
                FleetMenuName.text = Player3.text;
            else if (number == 3)
                FleetMenuName.text = Player4.text;
            else if (number == 4)
                FleetMenuName.text = Player5.text;
        }
    }

    //함대 장비 메뉴 슬롯 설명창
    public void FleetMenuTurretSlotExplain()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            FleetMenuTurret1Name.text = string.Format("Silence Sist");
            FleetMenuTurret2Name.text = string.Format("Over Jump");
            FleetMenuTurret3Name.text = string.Format("Sad Lilly-345 Missile Launcher");
            FleetMenuTurret4Name.text = string.Format("Delta Needle-42 Halist Missile Launcher");

            FleetMenuTurret1Explain.text = string.Format("Type : Kinetic Ammulation\nGood for : Slorius");
            FleetMenuTurret2Explain.text = string.Format("Type : Over Kinetic Ammulation\nGood for : Slorius, Kantakri");
            FleetMenuTurret3Explain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri");
            FleetMenuTurret4Explain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            FleetMenuTurret1Name.text = string.Format("사일런스 시스트");
            FleetMenuTurret2Name.text = string.Format("초과 점프");
            FleetMenuTurret3Name.text = string.Format("세드 릴리-345 미사일 런처");
            FleetMenuTurret4Name.text = string.Format("델타 니들-42 할리스트 미사일 런처");

            FleetMenuTurret1Explain.text = string.Format("타입 : 운동 탄환\n특화 : 슬로리어스");
            FleetMenuTurret2Explain.text = string.Format("타입 : 초과 운동 탄환\n특화 : 슬로리어스, 칸타크리");
            FleetMenuTurret3Explain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리");
            FleetMenuTurret4Explain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리");
        }
    }

    //함대 장비 메뉴 설명창
    public void FleetMenuTurretExplainPrintNoClick(int Damage, float RateOfFire, int AmountOfFire, int FormationShipDamage, float FormationShipRateOfFire, int FormationShipAmountOfFire)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (PrintNumber == 1)
            {
                FleetMenuTurretName.text = string.Format("Silence Sist");
                FleetMenuTurretExplain.text = string.Format("Type : Kinetic Ammulation\nGood for : Slorius\n\nFlagship\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire +
                    "\n\nFormation Ship\nDamage per a fire : " + FormationShipDamage +
                    "\nRate of fire(sec) : " + FormationShipRateOfFire +
                    "\nAmount of fire : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 2)
            {
                FleetMenuTurretName.text = string.Format("Over Jump");
                FleetMenuTurretExplain.text = string.Format("Type : Over Kinetic Ammulation\nGood for : Slorius, Kantakri\n\nFlagship\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire +
                    "\n\nFormation Ship\nDamage per a fire : " + FormationShipDamage +
                    "\nRate of fire(sec) : " + FormationShipRateOfFire +
                    "\nAmount of fire : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 3)
            {
                FleetMenuTurretName.text = string.Format("Sad Lilly-345 Missile Launcher");
                FleetMenuTurretExplain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri\n\nFlagship\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire +
                    "\n\nFormation Ship\nDamage per a fire : " + FormationShipDamage +
                    "\nRate of fire(sec) : " + FormationShipRateOfFire +
                    "\nAmount of fire : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 4)
            {
                FleetMenuTurretName.text = string.Format("Delta Needle-42 Halist Missile Launcher");
                FleetMenuTurretExplain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri\n\nFlagship\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire +
                    "\n\nFormation Ship\nDamage per a fire : " + FormationShipDamage +
                    "\nRate of fire(sec) : " + FormationShipRateOfFire +
                    "\nAmount of fire : " + FormationShipAmountOfFire);
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (PrintNumber == 1)
            {
                FleetMenuTurretName.text = string.Format("사일런스 시스트");
                FleetMenuTurretExplain.text = string.Format("타입 : 운동 탄환\n특화 : 슬로리어스\n\n기함\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : "+ RateOfFire +
                    "\n발사량 : " + AmountOfFire +
                    "\n\n편대함\n한 발당 데미지 : " + FormationShipDamage +
                    "\n발사속도(초) : " + FormationShipRateOfFire +
                    "\n발사량 : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 2)
            {
                FleetMenuTurretName.text = string.Format("초과 점프");
                FleetMenuTurretExplain.text = string.Format("타입 : 초과 운동 탄환\n특화 : 슬로리어스, 칸타크리\n\n기함\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : " + RateOfFire +
                    "\n발사량 : " + AmountOfFire +
                    "\n\n편대함\n한 발당 데미지 : " + FormationShipDamage +
                    "\n발사속도(초) : " + FormationShipRateOfFire +
                    "\n발사량 : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 3)
            {
                FleetMenuTurretName.text = string.Format("세드 릴리-345 미사일 런처");
                FleetMenuTurretExplain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리\n\n기함\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : " + RateOfFire +
                    "\n발사량 : " + AmountOfFire +
                    "\n\n편대함\n한 발당 데미지 : " + FormationShipDamage +
                    "\n발사속도(초) : " + FormationShipRateOfFire +
                    "\n발사량 : " + FormationShipAmountOfFire);
            }
            else if (PrintNumber == 4)
            {
                FleetMenuTurretName.text = string.Format("델타 니들-42 할리스트 미사일 런처");
                FleetMenuTurretExplain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리\n\n기함\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : " + RateOfFire +
                    "\n발사량 : " + AmountOfFire +
                    "\n\n편대함\n한 발당 데미지 : " + FormationShipDamage +
                    "\n발사속도(초) : " + FormationShipRateOfFire +
                    "\n발사량 : " + FormationShipAmountOfFire);
            }
        }
    }

    //함대 배열 메뉴 함대 정보창
    public void FleetFormationTotalInformationPrint(int number)
    {
        if (number == 0)
            FleetName.text = Player1.text;
        else if (number == 1)
            FleetName.text = Player2.text;
        else if (number == 2)
            FleetName.text = Player3.text;
        else if (number == 3)
            FleetName.text = Player4.text;
        else if (number == 4)
            FleetName.text = Player5.text;

        if (WordPrintSystem.LanguageType == 1)
        {
            InformationFormationShip.text = string.Format("Number of formation ship : " + ShipManager.instance.AmountOfFormationShip[number]);
            if (ShipManager.instance.AmountOfShieldShip[number] > 0)
                InformationShieldShip.text = string.Format("\nNumber of shield ship : " + ShipManager.instance.AmountOfShieldShip[number]);
            if (ShipManager.instance.AmountOfCarrier[number] > 0)
                InformationCarrier.text = string.Format("\n\nNumber of carrier: " + ShipManager.instance.AmountOfCarrier[number]);
            TotalInformation.text = string.Format("\n\n\nNumber of total ships : " + ShipManager.instance.FlagShipList[number].GetComponent<FollowShipManager>().ShipList.Count +
                "\nShip experience Data(YB) : " + ShipExperienceData +
                "\nNumber of ship : " + ((ShipManager.instance.AmountOfFormationShip[number] + ShipManager.instance.AmountOfShieldShip[number] + ShipManager.instance.AmountOfCarrier[number]) * ShipExperienceData));
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            InformationFormationShip.text = string.Format("편대함 수 : " + ShipManager.instance.AmountOfFormationShip[number]);
            if (ShipManager.instance.AmountOfShieldShip[number] > 0)
                InformationShieldShip.text = string.Format("\n방패함 수 : " + ShipManager.instance.AmountOfShieldShip[number]);
            if (ShipManager.instance.AmountOfCarrier[number] > 0)
                InformationCarrier.text = string.Format("\n\n우주모함 수 : " + ShipManager.instance.AmountOfCarrier[number]);
            TotalInformation.text = string.Format("\n\n\n총 함대 수 : " + ShipManager.instance.FlagShipList[number].GetComponent<FollowShipManager>().ShipList.Count +
                "\n함선 경험 데이터(YB) : " + ShipExperienceData +
                "\n함선수 : " + ((ShipManager.instance.AmountOfFormationShip[number] + ShipManager.instance.AmountOfShieldShip[number] + ShipManager.instance.AmountOfCarrier[number]) * ShipExperienceData));
        }
    }

    //함대 배열 관리창(함대 배열 메뉴 1탭)
    public void FleetFormationManagerPrint(int number)
    {
        ManagerTotalFleets.text = string.Format("{0}", ShipManager.instance.FlagShipList[number].GetComponent<FollowShipManager>().FormationStorage);
        if (ShipManager.instance.FlagShipList[number].GetComponent<FollowShipManager>().FormationStorage == 12)
        {
            FleetFormationMenuSystem.FleetAddButtonImage.raycastTarget = false;
            FleetFormationMenuSystem.FleetAddButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
        }
        else if (ShipManager.instance.FlagShipList[number].GetComponent<FollowShipManager>().FormationStorage < 12)
        {
            FleetFormationMenuSystem.FleetAddButtonImage.raycastTarget = true;
            FleetFormationMenuSystem.FleetAddButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
        }
    }

    //함대 선택 창에서 함대 선택 메시지(함대 배열 메뉴 1탭)
    public void FleetSelectShipsPrint()
    {
        if (FleetFormationMenuSystem.FleetSelectStep == 1)
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                FleetManagerSelectExplain.text = string.Format("Select your ships to relocate.");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                FleetManagerSelectExplain.text = string.Format("재배치하기 위한 함선을 선택하십시오.");
            }
        }
        if (FleetFormationMenuSystem.FleetSelectStep == 2)
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                FleetManagerSelectExplain.text = string.Format("Select your fleet to accommodate ships to relocate.");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                FleetManagerSelectExplain.text = string.Format("재배치할 함선을 수용하기 위한 함대를 선택하십시오.");
            }
        }
    }

    //함선 이전 메뉴에서 첫 함선 메뉴
    public void FleetFormationTransferNamePrint(int number)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (number == 0)
                FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player1.text);
            else if (number == 1)
                FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player2.text);
            else if (number == 2)
                FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player3.text);
            else if (number == 3)
                FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player4.text);
            else if (number == 4)
                FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player5.text);
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (number == 0)
                FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player1.text);
            else if (number == 1)
                FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player2.text);
            else if (number == 2)
                FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player3.text);
            else if (number == 3)
                FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player4.text);
            else if (number == 4)
                FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player5.text);
        }
    }

    //함대 선택 창에서의 선택된 함대 메시지(함대 배열 메뉴 1탭)
    public void FleetFormationSelectedFleetNamePrint(int number)
    {
        if (ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count == ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage)
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player1.text + "\nThis fleet cannot be deployed bacause number of this fleet's ships equals it's number of total formation.");
                else if (number == 1)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player2.text + "\nThis fleet cannot be deployed bacause number of this fleet's ships equals it's number of total formation.");
                else if (number == 2)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player3.text + "\nThis fleet cannot be deployed bacause number of this fleet's ships equals it's number of total formation.");
                else if (number == 3)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player4.text + "\nThis fleet cannot be deployed bacause number of this fleet's ships equals it's number of total formation.");
                else if (number == 4)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player5.text + "\nThis fleet cannot be deployed bacause number of this fleet's ships equals it's number of total formation.");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player1.text + "\n이 함대는 함선 수가 총 편대의 수와 동일하므로 배치될 수 없습니다.");
                else if (number == 1)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player2.text + "\n이 함대는 함선 수가 총 편대의 수와 동일하므로 배치될 수 없습니다.");
                else if (number == 2)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player3.text + "\n이 함대는 함선 수가 총 편대의 수와 동일하므로 배치될 수 없습니다.");
                else if (number == 3)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player4.text + "\n이 함대는 함선 수가 총 편대의 수와 동일하므로 배치될 수 없습니다.");
                else if (number == 4)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player5.text + "\n이 함대는 함선 수가 총 편대의 수와 동일하므로 배치될 수 없습니다.");
            }
            FleetFormationMenuSystem.FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            FleetFormationMenuSystem.FleetSelectOkImage.raycastTarget = false;
        }
        else
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player1.text);
                else if (number == 1)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player2.text);
                else if (number == 2)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player3.text);
                else if (number == 3)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player4.text);
                else if (number == 4)
                    FleetManagerSelectedShip.text = string.Format("Selected fleet : " + Player5.text);
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player1.text);
                else if (number == 1)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player2.text);
                else if (number == 2)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player3.text);
                else if (number == 3)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player4.text);
                else if (number == 4)
                    FleetManagerSelectedShip.text = string.Format("선택된 함대 : " + Player5.text);
            }
            FleetFormationMenuSystem.FleetSelectButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false);
            FleetFormationMenuSystem.FleetSelectOkImage.raycastTarget = true;
        }
    }

    //함대 배열 추가버튼을 누른 이후에 출력되는 진행 메시지(함대 배열 메뉴 1탭)
    public void FleetFormationManagerFleetAddProgressPrint(int number)
    {
        if (SystemMessages.MessageType == 2 && SystemMessages.MessageNumber == 1)
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    ProgressText2.text = string.Format("Selected fleet : " + Player1.text);
                else if (number == 1)
                    ProgressText2.text = string.Format("Selected fleet : " + Player2.text);
                else if (number == 2)
                    ProgressText2.text = string.Format("Selected fleet : " + Player3.text);
                else if (number == 3)
                    ProgressText2.text = string.Format("Selected fleet : " + Player4.text);
                else if (number == 4)
                    ProgressText2.text = string.Format("Selected fleet : " + Player5.text);

                if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource");
                else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> :" + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource" + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of formation extension</color> : " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    ProgressText2.text = string.Format("선택된 함대 : " + Player1.text);
                else if (number == 1)
                    ProgressText2.text = string.Format("선택된 함대 : " + Player2.text);
                else if (number == 2)
                    ProgressText2.text = string.Format("선택된 함대 : " + Player3.text);
                else if (number == 3)
                    ProgressText2.text = string.Format("선택된 함대 : " + Player4.text);
                else if (number == 4)
                    ProgressText2.text = string.Format("선택된 함대 : " + Player5.text);

                if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료");
                else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료" + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    FleetAddCostText.text = string.Format("<color=#00FF8C>편대 확장 비용</color> : " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
            }

            SystemMessages.GlopaorosCostProcess = UpgradeDataSystem.instance.GlopaorosCost;
            SystemMessages.ConstructionResourceProcess = UpgradeDataSystem.instance.ConstructionResourceCost;
            SystemMessages.TaritronicProcess = UpgradeDataSystem.instance.TaritronicCost;
        }
        else if (SystemMessages.MessageType == 2 && SystemMessages.MessageNumber == 3)
        {
            int AddShip = SystemMessages.AmountNumber + ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().ShipList.Count; //배치할 함선과 배치할 함대의 함선수의 합산
            int TotalShips = ShipManager.instance.FlagShipList[FleetFormationMenuSystem.FleetNumber].GetComponent<FollowShipManager>().FormationStorage; //배치할 함대의 총 함선수

            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    ProgressText1.text = string.Format("Please set the number of deployment." + "\n\nSelected fleet : " + Player1.text + "\nNumber of total formation of selected fleet after deployment : " + AddShip + "\nNumber of deployable total formation of selected fleet : " + TotalShips);
                else if (number == 1)
                    ProgressText1.text = string.Format("Please set the number of deployment." + "\n\nSelected fleet : " + Player2.text + "\nNumber of total formation of selected fleet after deployment : " + AddShip + "\nNumber of deployable total formation of selected fleet : " + TotalShips);
                else if (number == 2)
                    ProgressText1.text = string.Format("Please set the number of deployment." + "\n\nSelected fleet : " + Player3.text + "\nNumber of total formation of selected fleet after deployment : " + AddShip + "\nNumber of deployable total formation of selected fleet : " + TotalShips);
                else if (number == 3)
                    ProgressText1.text = string.Format("Please set the number of deployment." + "\n\nSelected fleet : " + Player4.text + "\nNumber of total formation of selected fleet after deployment : " + AddShip + "\nNumber of deployable total formation of selected fleet : " + TotalShips);
                else if (number == 4)
                    ProgressText1.text = string.Format("Please set the number of deployment." + "\n\nSelected fleet : " + Player5.text + "\nNumber of total formation of selected fleet after deployment : " + AddShip + "\nNumber of deployable total formation of selected fleet : " + TotalShips);

                if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " Glopa");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " Construction Resource");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " Construction Resource, " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " Construction Resource");
                else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> :" + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " Glopa, " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " Construction Resource" + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " Taritronic");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>Ship deployment cost</color> : " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " Taritronic");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    ProgressText1.text = string.Format("배치 수를 설정하십시오." + "\n\n선택된 함대 : " + Player1.text + "\n배치 후, 선택된 함대의 총 편대수 : " + AddShip + "\n선택된 함대의 배치가능한 총 편대수 : " + TotalShips);
                else if (number == 1)
                    ProgressText1.text = string.Format("배치 수를 설정하십시오." + "\n\n선택된 함대 : " + Player2.text + "\n배치 후, 선택된 함대의 총 편대수 : " + AddShip + "\n선택된 함대의 배치가능한 총 편대수 : " + TotalShips);
                else if (number == 2)
                    ProgressText1.text = string.Format("배치 수를 설정하십시오." + "\n\n선택된 함대 : " + Player3.text + "\n배치 후, 선택된 함대의 총 편대수 : " + AddShip + "\n선택된 함대의 배치가능한 총 편대수 : " + TotalShips);
                else if (number == 3)
                    ProgressText1.text = string.Format("배치 수를 설정하십시오." + "\n\n선택된 함대 : " + Player4.text + "\n배치 후, 선택된 함대의 총 편대수 : " + AddShip + "\n선택된 함대의 배치가능한 총 편대수 : " + TotalShips);
                else if (number == 4)
                    ProgressText1.text = string.Format("배치 수를 설정하십시오." + "\n\n선택된 함대 : " + Player5.text + "\n배치 후, 선택된 함대의 총 편대수 : " + AddShip + "\n선택된 함대의 배치가능한 총 편대수 : " + TotalShips);

                if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " 글로파");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " 건설 재료");
                else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " 건설 재료, " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " 건설 재료");
                else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber + " 글로파, " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber + " 건설 재료" + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " 타리트로닉");
                else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                    ShipProduectionCostText.text = string.Format("<color=#00FF8C>함선 배치 비용</color> : " + UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber + " 타리트로닉");
            }

            SystemMessages.GlopaorosCostProcess = UpgradeDataSystem.instance.GlopaorosCost * SystemMessages.AmountNumber;
            SystemMessages.ConstructionResourceProcess = UpgradeDataSystem.instance.ConstructionResourceCost * SystemMessages.AmountNumber;
            SystemMessages.TaritronicProcess = UpgradeDataSystem.instance.TaritronicCost * SystemMessages.AmountNumber;
        }
    }

    //함대 배열 추가버튼을 누른 이후에 출력되는 질문 메시지(함대 배열 메뉴 1탭)
    public void FleetFormationManagerFleetAddMessagePrint(int number)
    {
        ManagerSelectedShip.gameObject.SetActive(true);

        if (SystemMessages.MessageType == 2 && SystemMessages.MessageNumber == 1) //총 편대수 확장 질문 메시지
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to extend the number of total formation of this fleet?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to extend the number of total formation of this fleet?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to extend the number of total formation of this fleet?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to extend the number of total formation of this fleet?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to extend the number of total formation of this fleet?");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("이 함대의 총 편대수를 확장하시겠습니까?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("이 함대의 총 편대수를 확장하시겠습니까?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("이 함대의 총 편대수를 확장하시겠습니까?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("이 함대의 총 편대수를 확장하시겠습니까?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("이 함대의 총 편대수를 확장하시겠습니까?");
            }
        }
        else if (SystemMessages.MessageType == 2 && SystemMessages.MessageNumber == 2) //함선 재배치 질문 메시지
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("Selected fleet : " + Player1.text + "\nAre you sure you want to relocate this selected ships?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("Selected fleet : " + Player2.text + "\nAre you sure you want to relocate this selected ships?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("Selected fleet : " + Player3.text + "\nAre you sure you want to relocate this selected ships?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("Selected fleet : " + Player4.text + "\nAre you sure you want to relocate this selected ships?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("Selected fleet : " + Player5.text + "\nAre you sure you want to relocate this selected ships?");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("선택된 함대 : " + Player1.text + "\n선택된 함선을 재배치하시겠습니까?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("선택된 함대 : " + Player2.text + "\n선택된 함선을 재배치하시겠습니까?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("선택된 함대 : " + Player3.text + "\n선택된 함선을 재배치하시겠습니까?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("선택된 함대 : " + Player4.text + "\n선택된 함선을 재배치하시겠습니까?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("선택된 함대 : " + Player5.text + "\n선택된 함선을 재배치하시겠습니까?");
            }
        }
        else if (SystemMessages.MessageType == 2 && SystemMessages.MessageNumber == 3) //함선 생산 질문 메시지
        {
            if (WordPrintSystem.LanguageType == 1)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to deploy selected ship?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to deploy selected ship?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to deploy selected ship?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to deploy selected ship?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to deploy selected ship?");
            }
            else if (WordPrintSystem.LanguageType == 2)
            {
                if (number == 0)
                    ManagerSelectedShip.text = string.Format("선택한 함선을 배치하시겠습니까?");
                else if (number == 1)
                    ManagerSelectedShip.text = string.Format("선택한 함선을 배치하시겠습니까?");
                else if (number == 2)
                    ManagerSelectedShip.text = string.Format("선택한 함선을 배치하시겠습니까?");
                else if (number == 3)
                    ManagerSelectedShip.text = string.Format("선택한 함선을 배치하시겠습니까?");
                else if (number == 4)
                    ManagerSelectedShip.text = string.Format("선택한 함선을 배치하시겠습니까?");
            }
        }
    }

    //이전하려는 함선을 선택한 기함의 총 편대수가 부족하여 이전이 불가능할 경우에 출력되는 메시지(함대 배열 메뉴 1탭)
    public void WarnningFleetPrint()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("You've not enough the number of total formation of selected fleet as much as you accommodate ships to relocate.\n\nPlease extend the number of total formation of this selected fleet.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("재배치하려는 함선들을 수용할 수 없을 만큼 선택된 함대의 총 편대수가 부족합니다.\n\n해당 함대의 총 편대수를 확장하십시오.");
        }
    }

    //함대 배열 메뉴 함선 설명창 출력 메시지
    public void FleetFormationMenuShipExplainPrintNoClick(int ShipNumber, float ShipHitPoint)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (ShipNumber == 1)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = FormationShipSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(88, 263);
                FleetMenuShipImage.localScale = new Vector2(0.7f, 0.7f);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 520);
                FleetMenuShipName.text = string.Format("Formation ship");
                FleetMenuShipExplainText.text = string.Format("Type : Formation ship\nNumber of turrets : 2\nHull hit point : " + ShipHitPoint + "\n\nFormation ship symbolizes the mighty force that makes up all fleets of the Nariha. This can help stably operations of Flagships and Tactical ships, and this is being introduced as the efficient combat ship that can lead to victory with low cost at war.");
            }
            else if (ShipNumber == 2)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = ShieldShipSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(142, 390);
                FleetMenuShipImage.localScale = new Vector2(0.5f, 0.5f);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 550);
                FleetMenuShipName.text = string.Format("Shield ship");
                FleetMenuShipExplainText.text = string.Format("Type : Tactical ship\nSystem : Physical shield\nHull hit point : " + ShipHitPoint + "\n\nShield ship is the tactical ship that have a role to prevent attack of Contros fleet using a large shield. It use a shield during 20 seconds and can defend the attack of enemies fleet that contact shield. Shield ship must wait 20 seconds to use this shield again.");
            }
            else if (ShipNumber == 3)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = CarrierSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(202, 557);
                FleetMenuShipImage.localScale = new Vector2(0.4f, 0.4f);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 520);
                FleetMenuShipName.text = string.Format("Carrier");
                FleetMenuShipExplainText.text = string.Format("Type : Tactical ship\nNumber of carrier-based aircrafts : 15\nHull hit point : " + ShipHitPoint + "\n\nCarrier is a ship that attack enemies fleets using carrier-based aircrafts. 5 Aircrafts launch per 3 seconds, Carrier can use total 15 aircrafts. Carrier also is possible to attack enemies fleets using aircrafts from long-range distance.");
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (ShipNumber == 1)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = FormationShipSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(88, 263);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 500);
                FleetMenuShipName.text = string.Format("편대함");
                FleetMenuShipExplainText.text = string.Format("타입 : 편대함\n함포 수 : 2\n선체 체력 : " + ShipHitPoint + "\n\n편대함은 나리하의 모든 함대를 이루는 강력한 힘을 상징합니다. 기함과 전략함의 작전 임무를 안정적으로 도울 수 있으며, 적은 비용을 통해 전쟁에서 승리를 이끌 수 있는 효율적인 전투 함선으로 소개되고 있습니다.");
            }
            else if (ShipNumber == 2)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = ShieldShipSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(142, 390);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 500);
                FleetMenuShipName.text = string.Format("방패함");
                FleetMenuShipExplainText.text = string.Format("타입 : 전략함\n시스템 : 물리 방어막\n선체 체력 : " + ShipHitPoint + "\n\n방패함은 거대한 방어막을 펼쳐 컨트로스 함대의 공격을 저지하는 역할을 가진 전략함입니다. 20초 동안 방어막을 펼치며, 방어막에 접촉하는 적 함대의 공격을 방어할 수 있습니다. 방패함이 방어막을 다시 사용하려면 약 20초를 대기해야 합니다.");
            }
            else if (ShipNumber == 3)
            {
                FleetMenuShipImage.GetComponent<Image>().sprite = CarrierSprite;
                FleetMenuShipImage.sizeDelta = new Vector2(202, 557);
                FleetMenuExplainTextSize.sizeDelta = new Vector2(FleetMenuExplainTextSize.anchoredPosition.x, 500);
                FleetMenuShipName.text = string.Format("우주모함");
                FleetMenuShipExplainText.text = string.Format("타입 : 전략함\n함재기 수 : 15대\n선체 체력 : " + ShipHitPoint + "\n\n우주모함은 함재기를 발진시켜 적 함대를 공격합니다. 함재기는 3초당 5대씩 발진하며, 총 15대의 함재기들을 사용할 수 있습니다. 또한, 먼 거리에서도 함재기를 통해 적 함대를 공격하는 것이 가능합니다.");
            }
        }
        FleetMenuShipImage.gameObject.SetActive(true);
    }

    //기함 관리 메뉴 슬롯 설명창
    public void FlagshipMenuSlotExplain(int number)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (number == 1)
            {
                FlagshipMenuSlot1Name.text = string.Format("Sikro Class Cruise Missile");
                FlagshipMenuSlot1Explain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri");
            }
            else if (number == 2)
            {
                FlagshipMenuSlot2Name.text = string.Format("Cysiro-47 Patriot Missile");
                FlagshipMenuSlot2Explain.text = string.Format("Type : Explosion Missile\nGood for : Kantakri");
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (number == 1)
            {
                FlagshipMenuSlot1Name.text = string.Format("시크로급 순항 미사일");
                FlagshipMenuSlot1Explain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리");
            }
            else if (number == 2)
            {
                FlagshipMenuSlot2Name.text = string.Format("사이시로-47 페트리엇 미사일");
                FlagshipMenuSlot2Explain.text = string.Format("타입 : 폭발 미사일\n특화 : 칸타크리");
            }
        }
    }

    //기함 관리 메뉴 설명창
    public void FlagshipMenuTurretExplainPrintNoClick(int Damage, float RateOfFire, int AmountOfFire)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            //기함 단독 공격
            if (PrintType == 1 && PrintNumber == 1)
            {
                FlagshipMenuSkillName.text = string.Format("Sikro Class Cruise Missile");
                FlagshipMenuSkillExplain.text = string.Format("Using Type : Flagship Attack\nType : Explosion Missile\nGood for : Kantakri\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire);
            }
            //함대 공격
            if (PrintType == 2 && PrintNumber == 1)
            {
                FlagshipMenuSkillName.text = string.Format("Cysiro-47 Patriot Missile");
                FlagshipMenuSkillExplain.text = string.Format("Using Type : Fleet Attack\nType : Explosion Missile\nGood for : Kantakri\nDamage per a fire : " + Damage +
                    "\nRate of fire(sec) : " + RateOfFire +
                    "\nAmount of fire : " + AmountOfFire);
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            //기함 단독 공격
            if (PrintType == 1 && PrintNumber == 1)
            {
                FlagshipMenuSkillName.text = string.Format("시크로급 순항 미사일");
                FlagshipMenuSkillExplain.text = string.Format("사용 타입 : 기함 공격\n타입 : 폭발 미사일\n특화 : 칸타크리\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : " + RateOfFire +
                    "\n발사량 : " + AmountOfFire);
            }
            //함대 공격
            if (PrintType == 2 && PrintNumber == 1)
            {
                FlagshipMenuSkillName.text = string.Format("사이시로-47 페트리엇 미사일");
                FlagshipMenuSkillExplain.text = string.Format("사용 타입 : 함대 공격\n타입 : 폭발 미사일\n특화 : 칸타크리\n한 발당 데미지 : " + Damage +
                    "\n발사속도(초) : " + RateOfFire +
                    "\n발사량 : " + AmountOfFire);
            }
        }
    }

    //기함 추가 메시지
    public void FlagshipManagerFlagshipAddMessagePrint()
    {
        ManagerSelectedShip.gameObject.SetActive(true);

        if (WordPrintSystem.LanguageType == 1)
        {
            ProgressText2.text = string.Format("New flagship will be deployed after this process.");

            if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource");
            else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> :" + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource" + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>Cost of the new flagship deployment</color> : " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic");

            ManagerSelectedShip.text = string.Format("Are you sure you want to deploy the new flagship?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            ProgressText2.text = string.Format("이 절차 후에 새로운 기함이 배치될 것입니다.");

            if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료");
            else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> :" + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료" + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                FleetAddCostText.text = string.Format("<color=#00FF8C>새 기함 배치 비용</color> : " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉");

            ManagerSelectedShip.text = string.Format("새 기함을 배치하시겠습니까?");
        }

        SystemMessages.GlopaorosCostProcess = UpgradeDataSystem.instance.GlopaorosCost;
        SystemMessages.ConstructionResourceProcess = UpgradeDataSystem.instance.ConstructionResourceCost;
        SystemMessages.TaritronicProcess = UpgradeDataSystem.instance.TaritronicCost;
    }

    //연구 테이블 리스트
    public void UpgradeTableListPrint(int MainTabNumber, int SubTabNumber, int TableNumber)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (MainTabNumber == 1) //함선 장갑
            {
                if (SubTabNumber == 1)
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("Flagship armor system");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("Formation ship armor system");
                    else if (TableNumber == 3)
                        UpgradeMenu.TableName = ("Tactical ship armor system");
                }
                else if (SubTabNumber == 2)
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("Cannon type");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("Missile type");
                    else if (TableNumber == 3)
                        UpgradeMenu.TableName = ("Carrier-based aircraft type");
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("Flagship strike");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("Fleet strike");
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("Hit point");
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (TableNumber == 1) //돌격 소총
                        UpgradeMenu.TableName = ("Assault rifle type");
                    else if (TableNumber == 2)//샷건
                        UpgradeMenu.TableName = ("Shotgun type");
                    else if (TableNumber == 3) //저격총
                        UpgradeMenu.TableName = ("Sniper rifle type");
                    else if (TableNumber == 4) //기관단총
                        UpgradeMenu.TableName = ("Submachine gun type");
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (TableNumber == 1) //보조 장비
                        UpgradeMenu.TableName = ("Sub gear type");
                    else if (TableNumber == 2) //수류탄
                        UpgradeMenu.TableName = ("Grenade type");
                    else if (TableNumber == 3) //체인지 중화기
                        UpgradeMenu.TableName = ("Change heavy weapon");
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (TableNumber == 1) //보급 지원
                        UpgradeMenu.TableName = ("Logistics support");
                    else if (TableNumber == 2) //중화기 지원
                        UpgradeMenu.TableName = ("Heavy weapon support");
                    else if (TableNumber == 3) //탑승 차량 지원
                        UpgradeMenu.TableName = ("Vehicle support");
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (TableNumber == 1) //폭격 지원
                        UpgradeMenu.TableName = ("Bombardment support");
                }
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (MainTabNumber == 1) //함선 장갑
            {
                if (SubTabNumber == 1)
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("기함 장갑 시스템");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("편대함 장갑 시스템");
                    else if (TableNumber == 3)
                        UpgradeMenu.TableName = ("전략함 장갑 시스템");
                }
                else if (SubTabNumber == 2) //함선 무기
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("주포 타입");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("마사일 타입");
                    else if (TableNumber == 3)
                        UpgradeMenu.TableName = ("함재기 타입");
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("기함 공격");
                    else if (TableNumber == 2)
                        UpgradeMenu.TableName = ("함대 공격");
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (TableNumber == 1)
                        UpgradeMenu.TableName = ("체력");
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (TableNumber == 1) //돌격 소총
                        UpgradeMenu.TableName = ("돌격 소총 타입");
                    else if (TableNumber == 2) //샷건
                        UpgradeMenu.TableName = ("샷건 타입");
                    else if (TableNumber == 3) //저격총
                        UpgradeMenu.TableName = ("저격총 타입");
                    else if (TableNumber == 4) //기관단총
                        UpgradeMenu.TableName = ("기관단총 타입");
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (TableNumber == 1) //보조 장비
                        UpgradeMenu.TableName = ("보조 장비 타입");
                    else if (TableNumber == 2) //수류탄
                        UpgradeMenu.TableName = ("수류탄 타입");
                    else if (TableNumber == 3) //체인지 중화기
                        UpgradeMenu.TableName = ("체인지 중화기");
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (TableNumber == 1) //보급 지원
                        UpgradeMenu.TableName = ("보급 지원");
                    else if (TableNumber == 2) //중화기 지원
                        UpgradeMenu.TableName = ("중화기 지원");
                    else if (TableNumber == 3) //탑승 차량 지원
                        UpgradeMenu.TableName = ("탑승 차량 지원");
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (TableNumber == 1) //폭격 지원
                        UpgradeMenu.TableName = ("폭격 지원");
                }
            }
        }
    }

    //업그레이드 테이블 설명창
    public void UpgradeTableExplainPrint(int MainTabNumber, int SubTabNumber, int TableNumber)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (MainTabNumber == 1) //함선 장갑
            {
                if (SubTabNumber == 1)
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("Flagship hull enhancement");
                        MainUpgrade1ExplainText.text = ("Increase hit point of flagship's hull.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("Formation ship hull enhancement");
                        MainUpgrade1ExplainText.text = ("Increase hit point of formation ship's hull.");
                    }
                    else if (TableNumber == 3)
                    {
                        MainUpgrade1NameText.text = ("Tactical ship hull enhancement");
                        MainUpgrade1ExplainText.text = ("Increase hit point of tactical ship's hull.");
                    }
                }
                else if (SubTabNumber == 2) //함선 무기
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("Ship cannon attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all cannon weapons.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("Ship missile attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all missile weapons.");
                    }
                    else if (TableNumber == 3)
                    {
                        MainUpgrade1NameText.text = ("Carrier-based aircrafts attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all carrier-based aircrafts.");
                    }
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("Flagship strike attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all flagship strike.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("Fleet strike attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all fleet strike.");
                    }
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("Delta Hurricane hit point enhancement");
                        MainUpgrade1ExplainText.text = ("Increase hit point of the Delta Hurricane.");
                    }
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (TableNumber == 1) //돌격 소총
                    {
                        MainUpgrade1NameText.text = ("Assault rifle attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all assault rifles.");
                    }
                    else if (TableNumber == 2) //샷건
                    {
                        MainUpgrade1NameText.text = ("Shotgun attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all Shotguns.");
                    }
                    else if (TableNumber == 3) //저격총
                    {
                        MainUpgrade1NameText.text = ("Sniper rifle attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all sniper rifles.");
                    }
                    else if (TableNumber == 4) //기관단총
                    {
                        MainUpgrade1NameText.text = ("Submachine gun attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all submachine guns.");
                    }
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (TableNumber == 1) //보조 장비
                    {
                        MainUpgrade1NameText.text = ("Sub gear ability enhancement");
                        MainUpgrade1ExplainText.text = ("Increase ability of the all sub gears.");
                    }
                    else if (TableNumber == 2) //수류탄
                    {
                        MainUpgrade1NameText.text = ("Grenade ability enhancement");
                        MainUpgrade1ExplainText.text = ("Increase ability of the all grenades.");
                    }
                    else if (TableNumber == 3) //체인지 중화기
                    {
                        MainUpgrade1NameText.text = ("Change heavy weapon attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all change heavy weapons.");
                    }
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (TableNumber == 1) //보급 지원
                    {
                        MainUpgrade1NameText.text = ("Amount of the logistics support enhancement");
                        MainUpgrade1ExplainText.text = ("Increase amount of the logistics support that support to Delta Hurricane.");
                    }
                    else if (TableNumber == 2) //중화기 지원
                    {
                        MainUpgrade1NameText.text = ("Heavy weapon attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all heavy weapon that support to Delta Hurricane.");
                    }
                    else if (TableNumber == 3) //탑승 차량 지원
                    {
                        MainUpgrade1NameText.text = ("Vehicle ability enhancement");
                        MainUpgrade1ExplainText.text = ("Increase ability of the all vehicles that support to Delta Hurricane.");
                    }
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (TableNumber == 1) //폭격 지원
                    {
                        MainUpgrade1NameText.text = ("Bombardment attack damage enhancement");
                        MainUpgrade1ExplainText.text = ("Increase attack damage of the all bombardments that support to Delta Hurricane.");
                    }
                }
            }
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (MainTabNumber == 1) //함선 장갑
            {
                if (SubTabNumber == 1)
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("기함 선체 강화");
                        MainUpgrade1ExplainText.text = ("기함 선체의 체력을 강화합니다.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("편대함 선체 강화");
                        MainUpgrade1ExplainText.text = ("편대함 선체의 체력을 강화합니다.");
                    }
                    else if (TableNumber == 3)
                    {
                        MainUpgrade1NameText.text = ("전략함 선체 강화");
                        MainUpgrade1ExplainText.text = ("전략함 선체의 체력을 강화합니다.");
                    }
                }
                else if (SubTabNumber == 2) //함선 무기
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("함선 주포 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 주포의 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("함선 마사일 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 마사일의 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 3)
                    {
                        MainUpgrade1NameText.text = ("함선 함재기 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 함재기의 공격력을 강화합니다.");
                    }
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("기함 공격 무기 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 기함 공격 무기의 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 2)
                    {
                        MainUpgrade1NameText.text = ("함대 공격 무기 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 함대 공격 무기의 공격력을 강화합니다.");
                    }
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (TableNumber == 1)
                    {
                        MainUpgrade1NameText.text = ("델타 허리케인 체력 강화");
                        MainUpgrade1ExplainText.text = ("델타 허리케인 체력을 강화합니다.");
                    }
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (TableNumber == 1) //돌격 소총
                    {
                        MainUpgrade1NameText.text = ("돌격 소총 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 돌격 소총 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 2) //샷건
                    {
                        MainUpgrade1NameText.text = ("샷건 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 샷건 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 3) //저격총
                    {
                        MainUpgrade1NameText.text = ("저격총 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 저격총 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 4) //기관단총
                    {
                        MainUpgrade1NameText.text = ("기관단총 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 기관단총 공격력을 강화합니다.");
                    }
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (TableNumber == 1) //보조 장비
                    {
                        MainUpgrade1NameText.text = ("보조 장비 능력 강화");
                        MainUpgrade1ExplainText.text = ("모든 보조 장비의 능력을 강화합니다.");
                    }
                    else if (TableNumber == 2) //수류탄
                    {
                        MainUpgrade1NameText.text = ("수류탄 능력 강화");
                        MainUpgrade1ExplainText.text = ("모든 수류탄의 능력을 강화합니다.");
                    }
                    else if (TableNumber == 3) //체인지 중화기
                    {
                        MainUpgrade1NameText.text = ("체인지 중화기 공격력 강화");
                        MainUpgrade1ExplainText.text = ("모든 체인지 중화기 공격력을 강화합니다.");
                    }
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (TableNumber == 1) //보급 지원
                    {
                        MainUpgrade1NameText.text = ("보급 지원량 강화");
                        MainUpgrade1ExplainText.text = ("델타 허리케인에게 지원되는 보급 지원량을 강화합니다.");
                    }
                    else if (TableNumber == 2) //중화기 지원
                    {
                        MainUpgrade1NameText.text = ("중화기 무기 공격력 강화");
                        MainUpgrade1ExplainText.text = ("델타 허리케인에게 지원되는 모든 중화기의 공격력을 강화합니다.");
                    }
                    else if (TableNumber == 3) //탑승 차량 지원
                    {
                        MainUpgrade1NameText.text = ("탑승 차량 능력 강화");
                        MainUpgrade1ExplainText.text = ("델타 허리케인에게 지원되는 모든 탑승 차량의 능력을 강화합니다.");
                    }
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (TableNumber == 1) //폭격 지원
                    {
                        MainUpgrade1NameText.text = ("폭격 공격력 강화");
                        MainUpgrade1ExplainText.text = ("델타 허리케인에게 지원되는 모든 폭격의 공격력을 강화합니다.");
                    }
                }
            }
        }
    }

    //업그레이드 진행 창
    public void UpgradeProgress(int MainTabNumber, int SubTabNumber, int EnterNumber)
    {
        TotalCostText.gameObject.SetActive(true);

        if (WordPrintSystem.LanguageType == 1)
        {
            if (MainTabNumber == 1) //함대 탭
            {
                if (SubTabNumber == 1) //함선 장갑
                {
                    if (EnterNumber == 1) //기함 업그레이드
                    {
                        UpgradeDataSystem.instance.FlagshipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FlagshipUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Flagship ship hull class : " + UpgradeDataSystem.instance.FlagshipUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Flagship ship hull class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Flagship armor system?");
                    }
                    else if (EnterNumber == 2) //편대함 업그레이드
                    {
                        UpgradeDataSystem.instance.FormationShipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FormationUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Formation ship hull class : " + UpgradeDataSystem.instance.FormationUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Formation ship hull class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Formation ship armor system?");
                    }
                    else if (EnterNumber == 3) //전략함 업그레이드
                    {
                        UpgradeDataSystem.instance.TacticalShipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.TacticalShipUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Tactical ship hull class : " + UpgradeDataSystem.instance.TacticalShipUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Tactical ship hull class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Tactical ship armor system?");
                    }
                }
                else if (SubTabNumber == 2) //함선 무기
                {
                    if (EnterNumber == 1) //주포 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipCannonClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipCannonUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Cannon class : " + UpgradeDataSystem.instance.ShipCannonUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Cannon class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Cannon?");
                    }
                    else if (EnterNumber == 2) //미사일 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipMissileClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipMissileUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Missile class : " + UpgradeDataSystem.instance.ShipMissileUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Missile class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Missile?");
                    }
                    else if (EnterNumber == 3) //함재기 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipFighterClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipFighterUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Carrier-based aircraft class : " + UpgradeDataSystem.instance.ShipFighterUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Carrier-based aircraft class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Carrier-based aircraft?");
                    }
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (EnterNumber == 1) //기함 공격
                    {
                        UpgradeDataSystem.instance.FlagshipAttackSkillClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Flagship strike class : " + UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Flagship strike class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Flagship strike?");
                    }
                    else if (EnterNumber == 2) //함대 공격
                    {
                        UpgradeDataSystem.instance.FleetAttackSkillClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Fleet strike class : " + UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Fleet strike class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Fleet strike?");
                    }
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (EnterNumber == 1) //체력 업그레이드
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneHitPointLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Hit point class : " + UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Hit point class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Delta Hurricane hit point?");
                    }
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (EnterNumber == 1) //돌격 소총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneAssaultRifleLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Assault rifle class : " + UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Assault rifle class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Assault rifle type?");
                    }
                    else if (EnterNumber == 2) //샷건
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneShotgunLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Shotgun class : " + UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Shotgun class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Shotgun type?");
                    }
                    else if (EnterNumber == 3) //저격총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSniperRifleLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Sniper rifle class : " + UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Sniper rifle class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Sniper rifle type?");
                    }
                    else if (EnterNumber == 4) //기관단총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSubmachineGunLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Submachine gun class : " + UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Submachine gun class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Submachine gun type?");
                    }
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (EnterNumber == 1) //보조 장비
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSubGearLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Sub Gear class : " + UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Sub Gear class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Sub Gear type?");
                    }
                    else if (EnterNumber == 2) //수류탄
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneGrenadeLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Grenade class : " + UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Grenade class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Grenade type?");
                    }
                    else if (EnterNumber == 3) //체인지 중화기
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneChangeHeavyWeaponLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Change Heavy Weapon class : " + UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Change Heavy Weapon class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a research of the Change Heavy Weapon?");
                    }
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (EnterNumber == 1) //보급 지원
                    {
                        UpgradeDataSystem.instance.ShipAmmoSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Logistics support class : " + UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Logistics support class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a class promotion of the Logistics support?");
                    }
                    else if (EnterNumber == 2) //중화기 지원
                    {
                        UpgradeDataSystem.instance.ShipHeavyWeaponSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Heavy weapon support class : " + UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Heavy weapon support class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a class promotion of the Heavy weapon support?");
                    }
                    else if (EnterNumber == 3) //탑승 차량 지원
                    {
                        UpgradeDataSystem.instance.ShipVehicleSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Vehicle support class : " + UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Vehicle support class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a class promotion of the Vehicle support?");
                    }
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (EnterNumber == 1) //폭격 지원
                    {
                        UpgradeDataSystem.instance.ShipStrikeSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("Bombardment support class : " + UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("Bombardment support class : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\nAre you sure you want to approval a class promotion of the Bombardment support?");
                    }
                }
            }

            if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa\n\n");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource\n\n");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " Glopa, " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " Construction Resource" + UpgradeDataSystem.instance.TaritronicCost + " Taritronic\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>Research cost</color> : " + UpgradeDataSystem.instance.TaritronicCost + " Taritronic\n\n");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (MainTabNumber == 1) //함대 탭
            {
                if (SubTabNumber == 1) //함선 장갑
                {
                    if (EnterNumber == 1) //기함 업그레이드
                    {
                        UpgradeDataSystem.instance.FlagshipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FlagshipUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("기함 선체 등급 : " + UpgradeDataSystem.instance.FlagshipUpgradeLevel);
                        NextStepUpgrade.text = string.Format("기함 선체 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n기함 장갑 시스템의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //편대함 업그레이드
                    {
                        UpgradeDataSystem.instance.FormationShipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FormationUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("편대함 선체 등급 : " + UpgradeDataSystem.instance.FormationUpgradeLevel);
                        NextStepUpgrade.text = string.Format("편대함 선체 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n편대함 장갑 시스템의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 3) //전략함 업그레이드
                    {
                        UpgradeDataSystem.instance.TacticalShipClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.TacticalShipUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("전략함 선체 등급 : " + UpgradeDataSystem.instance.TacticalShipUpgradeLevel);
                        NextStepUpgrade.text = string.Format("전략함 선체 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n전략함 장갑 시스템의 연구를 승인하시겠습니까?");
                    }
                }
                else if (SubTabNumber == 2) //함선 무기
                {
                    if (EnterNumber == 1) //주포 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipCannonClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipCannonUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("주포 등급 : " + UpgradeDataSystem.instance.ShipCannonUpgradeLevel);
                        NextStepUpgrade.text = string.Format("주포 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n주포의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //미사일 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipMissileClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipMissileUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("미사일 등급 : " + UpgradeDataSystem.instance.ShipMissileUpgradeLevel);
                        NextStepUpgrade.text = string.Format("미사일 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n미사일의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 3) //함재기 업그레이드
                    {
                        UpgradeDataSystem.instance.ShipFighterClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipFighterUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("함재기 등급 : " + UpgradeDataSystem.instance.ShipFighterUpgradeLevel);
                        NextStepUpgrade.text = string.Format("함재기 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n함재기의 연구를 승인하시겠습니까?");
                    }
                }
                else if (SubTabNumber == 3) //함대 지원
                {
                    if (EnterNumber == 1) //기함 공격
                    {
                        UpgradeDataSystem.instance.FlagshipAttackSkillClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("기함 공격 등급 : " + UpgradeDataSystem.instance.FlagshipAttackSkillUpgradeLevel);
                        NextStepUpgrade.text = string.Format("기함 공격 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n기함 공격의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //함대 공격
                    {
                        UpgradeDataSystem.instance.FleetAttackSkillClassLevel();
                        int NextLevel = UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("함대 공격 등급 : " + UpgradeDataSystem.instance.FleetAttackSkillUpgradeLevel);
                        NextStepUpgrade.text = string.Format("함대 공격 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n함대 공격의 연구를 승인하시겠습니까?");
                    }
                }
            }

            else if (MainTabNumber == 2) //델타 허리케인 탭
            {
                if (SubTabNumber == 1) //내구도
                {
                    if (EnterNumber == 1) //체력 업그레이드
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneHitPointLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("체력 등급 : " + UpgradeDataSystem.instance.HurricaneHitPointUpgradeLevel);
                        NextStepUpgrade.text = string.Format("체력 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n델타 허리케인 체력의 연구를 승인하시겠습니까?");
                    }
                }
                else if (SubTabNumber == 2) //기본 무기
                {
                    if (EnterNumber == 1) //돌격 소총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneAssaultRifleLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("돌격 소총 등급 : " + UpgradeDataSystem.instance.HurricaneAssaultRifleUpgradeLevel);
                        NextStepUpgrade.text = string.Format("돌격 소총 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n돌격 소총 타입의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //샷건
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneShotgunLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("샷건 등급 : " + UpgradeDataSystem.instance.HurricaneShotgunUpgradeLevel);
                        NextStepUpgrade.text = string.Format("샷건 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n샷건 타입의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 3) //저격총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSniperRifleLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("저격총 등급 : " + UpgradeDataSystem.instance.HurricaneSniperRifleUpgradeLevel);
                        NextStepUpgrade.text = string.Format("저격총 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n저격총 타입의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 4) //기관단총
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSubmachineGunLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("기관단총 등급 : " + UpgradeDataSystem.instance.HurricaneSubmachineGunUpgradeLevel);
                        NextStepUpgrade.text = string.Format("기관단총 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n기관단총 타입의 연구를 승인하시겠습니까?");
                    }
                }
                else if (SubTabNumber == 3) //지원 무기
                {
                    if (EnterNumber == 1) //보조 장비
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneSubGearLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("보조 장비 등급 : " + UpgradeDataSystem.instance.HurricaneSubGearUpgradeLevel);
                        NextStepUpgrade.text = string.Format("보조 장비 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n보조 장비 타입의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //수류탄
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneGrenadeLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("수류탄 등급 : " + UpgradeDataSystem.instance.HurricaneGrenadeUpgradeLevel);
                        NextStepUpgrade.text = string.Format("수류탄 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n수류탄 타입의 연구를 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 3) //체인지 중화기
                    {
                        UpgradeDataSystem.instance.DeltaHurricaneChangeHeavyWeaponLevel();
                        int NextLevel = UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("체인지 중화기 등급 : " + UpgradeDataSystem.instance.HurricaneChangeHeavyWeaponUpgradeLevel);
                        NextStepUpgrade.text = string.Format("체인지 중화기 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n체인지 중화기의 연구를 승인하시겠습니까?");
                    }
                }
            }

            else if (MainTabNumber == 3) //함선 지원
            {
                if (SubTabNumber == 1) //무기 지원
                {
                    if (EnterNumber == 1) //보급 지원
                    {
                        UpgradeDataSystem.instance.ShipAmmoSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("보급 지원 등급 : " + UpgradeDataSystem.instance.ShipAmmoSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("보급 지원 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n보급 지원의 승급을 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 2) //중화기 지원
                    {
                        UpgradeDataSystem.instance.ShipHeavyWeaponSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("중화기 지원 등급 : " + UpgradeDataSystem.instance.ShipHeavyWeaponSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("중화기 지원 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n중화기 지원의 승급을 승인하시겠습니까?");
                    }
                    else if (EnterNumber == 3) //탑승 차량 지원
                    {
                        UpgradeDataSystem.instance.ShipVehicleSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("탑승 차량 지원 등급 : " + UpgradeDataSystem.instance.ShipVehicleSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("탑승 차량 지원 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n탑승 차량 지원의 승급을 승인하시겠습니까?");
                    }
                }
                else if (SubTabNumber == 2) //공격 지원
                {
                    if (EnterNumber == 1) //폭격 지원
                    {
                        UpgradeDataSystem.instance.ShipStrikeSupportLevel();
                        int NextLevel = UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel + 1;

                        PresentStepUpgrade.text = string.Format("폭격 지원 등급 : " + UpgradeDataSystem.instance.ShipStrikeSupportUpgradeLevel);
                        NextStepUpgrade.text = string.Format("폭격 지원 등급 : " + NextLevel);

                        ManagerSelectedShip.text = string.Format("\n폭격 지원의 승급을 승인하시겠습니까?");
                    }
                }
            }

            if (UpgradeDataSystem.instance.ConstructionResourceCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파\n\n");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료\n\n");
            else if (UpgradeDataSystem.instance.ConstructionResourceCost != 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost != 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> :" + UpgradeDataSystem.instance.GlopaorosCost + " 글로파, " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.TaritronicCost != 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.ConstructionResourceCost + " 건설 재료" + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉\n\n");
            else if (UpgradeDataSystem.instance.GlopaorosCost == 0 && UpgradeDataSystem.instance.ConstructionResourceCost == 0)
                TotalCostText.text = string.Format("<color=#00FF8C>연구 비용</color> : " + UpgradeDataSystem.instance.TaritronicCost + " 타리트로닉\n\n");
        }
        SystemMessages.GlopaorosCostProcess = UpgradeDataSystem.instance.GlopaorosCost;
        SystemMessages.ConstructionResourceProcess = UpgradeDataSystem.instance.ConstructionResourceCost;
        SystemMessages.TaritronicProcess = UpgradeDataSystem.instance.TaritronicCost;
    }

    //연구 메뉴의 잠금 해제 절차 메시지
    public void UpgradeTableInform(int UnlockCost, int UnlockResource, int UnlockTaritronic)
    {
        int NumberOfCost = 0;
        if (UnlockCost > 0)
            NumberOfCost++;
        if (UnlockResource > 0)
            NumberOfCost++;
        if (UnlockTaritronic > 0)
            NumberOfCost++;

        if (WordPrintSystem.LanguageType == 1)
        {
            if (NumberOfCost == 1)
            {
                if (UnlockCost > 0)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to pay the Glopaoros to unlock?");
                else if (UnlockResource > 0)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to use the Construction Resource to unlock?");
                else if (UnlockTaritronic > 0)
                    ManagerSelectedShip.text = string.Format("Are you sure you want to use the Taritronic to unlock?");
            }
            else if (NumberOfCost > 1)
                ManagerSelectedShip.text = string.Format("Are you sure you want to use assets to unlock?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (NumberOfCost == 1)
            {
                if (UnlockCost > 0)
                    ManagerSelectedShip.text = string.Format("글로파오로스를 지불하여 잠금 해제하시겠습니까?");
                else if (UnlockResource > 0)
                    ManagerSelectedShip.text = string.Format("건설 재료를 사용하여 잠금 해제하시겠습니까?");
                else if (UnlockTaritronic > 0)
                    ManagerSelectedShip.text = string.Format("타리트로닉을 사용하여 잠금 해제하시겠습니까?");
            }
            else if (NumberOfCost > 1)
                ManagerSelectedShip.text = string.Format("자산을 사용하여 잠금 해제하시겠습니까?");
        }
    }

    //기함 보유수
    public void FlagshipAmount()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            FlagshipAmountText.text = string.Format("Number of flagship : " + ShipManager.instance.FlagShipList.Count + " / " + 3);
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            FlagshipAmountText.text = string.Format("기함 수 : " + ShipManager.instance.FlagShipList.Count + " / " + 3);
        }
    }

    //자금 부족으로 언락을 불허하는 메시지
    public void UpgradeTableUnlockFail()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("It is unable to unlock because you've not enough payment to unlock.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("잠금 해제할 자본이 부족하여 잠금을 해제할 수 없습니다.");
        }
    }

    //자금 부족으로 함대 확대를 불허하는 메시지
    public void FleetAddFail()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("It is unable to extend the number of total formation of this fleet because you've not enough payment to extend.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("확장할 자본이 부족하여 이 함대의 총 편대수를 확장할 수 없습니다.");
        }
    }

    //자금 부족으로 함선 배치를 불허하는 메시지
    public void ShipProductionFail()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("It is unable to deploy the new ship because you've not enough payment to deploy.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("새 함선을 배치할 자본이 부족하여 함선을 배치할 수 없습니다.");
        }
    }

    //자금 부족으로 기함 새 배치를 불허하는 메시지
    public void NewFlagshipCashFail()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("It is unable to deploy the new flagship because you've not enough payment to deploy.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("새 기함을 배치할 자본이 부족하여 함선을 배치할 수 없습니다.");
        }
    }

    //기함 한도가 다찼으므로 새 기함 배치를 불허하는 메시지
    public void NewFlagshipNumberFail()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            WarnningFleetManager1.text = string.Format("It is unable to deploy the new flagship because you've maximum number of flagship.");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            WarnningFleetManager1.text = string.Format("최대 기함수에 도달했으므로, 배치할 수 없습니다.");
        }
    }

    //델타 허리케인 작전 메뉴 승인창
    public void HurricaneStart(int number)
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            if (number == 1)
                ManagerSelectedShip.text = string.Format("Will you send Delta Hurricane to this operation?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            if (number == 1)
                ManagerSelectedShip.text = string.Format("해당 작전에 델타 허리케인을 투입하시겠습니까?");
        }
    }

    //타이틀 돌아가기 메시지
    public void GoToTitle()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            ManagerSelectedShip.text = string.Format("Do you wish to disconnect this UCCIS console and go back to the Title screen?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            ManagerSelectedShip.text = string.Format("이 성간우주사령연합 콘솔 접속을 해제하고 타이틀 화면으로 돌아가시겠습니까?");
        }
    }

    //세이브 덮어쓰기 메시지
    public void SaveStart()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            ManagerSelectedShip.text = string.Format("This file already exists. Are you sure you want to overwrite it?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            ManagerSelectedShip.text = string.Format("파일이 이미 존재합니다. 덮어쓰시겠습니까?");
        }
    }

    //세이브 삭제 메시지
    public void DeleteStart()
    {
        if (WordPrintSystem.LanguageType == 1)
        {
            ManagerSelectedShip.text = string.Format("Are you sure you want to delete this file?");
        }
        else if (WordPrintSystem.LanguageType == 2)
        {
            ManagerSelectedShip.text = string.Format("해당 저장 파일을 삭제하시겠습니까?");
        }
    }
}