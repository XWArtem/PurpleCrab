using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// HoverHelpPanel is only active when the player's cursor goes into the "?" button
/// </summary>

public class HoverHelpPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _panelHelp;
    void Start()
    {
        _panelHelp.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelHelp.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _panelHelp.SetActive(false);
    }
}
