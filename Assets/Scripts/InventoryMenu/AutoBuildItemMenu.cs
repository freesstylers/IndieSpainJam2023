using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoBuildItemMenu : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    //Scriptable con el objeto con la info
    public delegate void FillItem(string desc, string name, Sprite sprite);
    public FillItem fi;

    private Item item;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(Item item_)
    {
        item = item_;
        GetComponent<UnityEngine.UI.Button>().image.sprite = item_.sprite;
    }

    public void SendInfo()
    {
        if (fi == null)
            return;

        fi.Invoke(item.description, item.name, item.sprite);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }
}
