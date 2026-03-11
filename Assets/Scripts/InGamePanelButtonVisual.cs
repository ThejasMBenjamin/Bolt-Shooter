using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGamePanelButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private float normalFontSize;
    private float newFontSize = 4f;

    private void Awake()
    {
        //Now i dont use image xD
        //img = GetComponent<Image>();
        //defualtColor = img.color;
        //Color c = img.color;

        //Text 
        text = GetComponent<TextMeshProUGUI>();
        normalFontSize = text.fontSize;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //img.color = new Color(238, 238, 238, 175);
        text.fontSize +=  newFontSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = normalFontSize;
    }
}
