using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class SaveDataTable : MonoBehaviour
{
    LoadDataManager LoadDataManager;

    private bool ButtonClicked = false;
    public bool IsEmpty = false; //데이터가 없을 경우
    public int TableNumber;
    public GameObject ClickedPrefab;
    public GameObject DeleteIconPrefab;
    public Text NameText;
    public Text InformText;

    public void DataTableGet(int number)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "UCCIS Battle Log Date" + number + ".json");
        if (File.Exists(dataPath))
        {
            int TotalFlagship = SaveDateAndShips.instance.TotalFlagships;
            int TotalShip = SaveDateAndShips.instance.TotalShips;
            string dateString = SaveDateAndShips.instance.SavedDate;

            if (BattleSave.Save1.LanguageType == 1)
            {
                InformText.text = string.Format("Flagships : " + TotalFlagship + ", Ships : " + TotalShip + " // Date : " + dateString);
            }
            else if (BattleSave.Save1.LanguageType == 2)
            {
                InformText.text = string.Format("기함 : " + TotalFlagship + " 척, 함대 : " + TotalShip + " 척 // 날짜 : " + dateString);
            }
        }
        else
        {
            InformText.text = string.Format("");
        }
    }

    public void ButtonClick()
    {
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();
        LoadDataManager.InitializeClickTable();
        LoadDataManager.TableNumber = TableNumber;
        LoadDataManager.LoadButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false); //데이터 불러오기 및 저장 버튼에 동시에 사용됨
        LoadDataManager.LoadButtonImage.raycastTarget = true;

        if (LoadDataManager.isSaveData == true && IsEmpty == false)
        {
            LoadDataManager.NewGameButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", false); //우주맵에서 불러오기 버튼이 데이터가 있는 테이블에만 활성화 되도록 조취
            LoadDataManager.LoadButtonImage2.raycastTarget = true;
        }
        else if (LoadDataManager.isSaveData == true && IsEmpty == true)
        {
            LoadDataManager.NewGameButtonPrefab.GetComponent<Animator>().SetBool("Dont touch, cancel menu button", true);
            LoadDataManager.LoadButtonImage2.raycastTarget = false;
        }

        LoadDataManager.GetDataPath(TableNumber);

        if (ButtonClicked == false)
            ClickedPrefab.SetActive(true);
        else
            ClickedPrefab.SetActive(false);
    }

    //데이터를 삭제하기
    public void DeleteData()
    {
        ButtonClick();
        if (LoadDataManager == null)
            LoadDataManager = FindObjectOfType<LoadDataManager>();
        LoadDataManager.DeleteDataStart();
    }
}