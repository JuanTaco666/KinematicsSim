using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour, IDragHandler
{

    public Button close;

    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    private GameObject panel;

    private void Awake()
    {

        if(dragRectTransform == null)
        {
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        if(canvas == null)
        {
            Transform testCanvasTransform = transform;

            while(canvas == null){
                testCanvasTransform = testCanvasTransform.parent;
                canvas = testCanvasTransform.GetComponent<Canvas>();
            }
        }

    }

    void Start()
    {
        panel = transform.parent.gameObject;
    
        close.onClick.AddListener(Close);

    }

    void Close()
    {
        Destroy(panel);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
