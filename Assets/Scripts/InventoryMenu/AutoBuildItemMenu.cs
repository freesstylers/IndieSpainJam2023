using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoBuildItemMenu : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler, IBeginDragHandler
{
    private Item item;

    private Vector2 initRect;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private ItemMenuController itemMenu;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>(true);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(Item item_, ItemMenuController itemMenu_)
    {
        item = item_;
        itemMenu = itemMenu_;
        GetComponent<UnityEngine.UI.Button>().image.sprite = item_.sprite;
    }

    public void SendInfo()
    {
        if (itemMenu == null)
            return;
        itemMenu.FillFullItem(item.description, item.name, item.sprite);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initRect = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = initRect;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag != gameObject)
        {
            string id = eventData.pointerDrag.GetComponent<AutoBuildItemMenu>().item.id;
            itemMenu.CombineItems(id, item.id);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
}
