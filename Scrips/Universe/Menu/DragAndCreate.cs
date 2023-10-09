using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndCreate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int SlotType; //1 = 함대 장비 메뉴, 2 = 기함 관리 메뉴
    [SerializeField] private Canvas canvas;
    public GameObject FleetMenu;
    private RectTransform rect;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image2.GetComponent<Image>().enabled = true;
        image3.GetComponent<Image>().enabled = true;
        image4.GetComponent<Image>().enabled = true;
        image5.GetComponent<Image>().enabled = true;

        image1.maskable = false;
        image2.maskable = false;
        image3.maskable = false;
        image4.maskable = false;
        image5.maskable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image2.GetComponent<Image>().enabled = false;
        image3.GetComponent<Image>().enabled = false;
        image4.GetComponent<Image>().enabled = false;
        image5.GetComponent<Image>().enabled = false;

        image1.maskable = true;
        image2.maskable = true;
        image3.maskable = true;
        image4.maskable = true;
        image5.maskable = true;

        rect.anchoredPosition = new Vector2(0, 0);
        if (SlotType == 1)
        {
            if (FleetMenu.GetComponent<FleetMenuSystem>().SlotInput == true)
            {
                FleetMenu.GetComponent<FleetMenuSystem>().SlotInputStart();
                FleetMenu.GetComponent<FleetMenuSystem>().SlotInput = false;
            }
            else
            {
                FleetMenu.GetComponent<FlagshipManagerSystem>().SlotIconTurnOff();
            }
        }
        else if (SlotType == 2)
        {
            if (FleetMenu.GetComponent<FlagshipManagerSystem>().SlotInput == true)
            {
                FleetMenu.GetComponent<FlagshipManagerSystem>().SlotInputStart();
                FleetMenu.GetComponent<FlagshipManagerSystem>().SlotInput = false;
            }
            else
            {
                FleetMenu.GetComponent<FlagshipManagerSystem>().SlotIconTurnOff();
            }
        }
    }
}