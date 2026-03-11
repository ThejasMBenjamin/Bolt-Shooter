using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuBtnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rt;
    [SerializeField] private float btnHoverSize = 1.05f;
    private Vector2 originalBtnSize;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        originalBtnSize = rt.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.localScale = originalBtnSize * btnHoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localScale = originalBtnSize;
    }
}
