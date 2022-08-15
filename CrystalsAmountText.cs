using UnityEngine;
using TMPro;

public class CrystalsAmountText : MonoBehaviour
{
    private int _crystals;
    public int Crystals
    {
        private get
        {
            return _crystals;
        }
        set
        {
            _crystals = value;
            SetTheString();
        }
    }

    public TextMeshProUGUI _statsText;
    public Gradient PurpleToRed;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Color32 _purpleColor;
    [SerializeField] private Color32 _redColor;
    private void Awake()
    {
        GameManager.Instance.SetCrystalsAmountText(this);
    }
    public void Start()
    {
        SetTheString();
        _purpleColor = new Color32(133, 10, 147, 255);
        _redColor = new Color32(231, 8, 72, 255);
    }
    public void SetTheString()
    {
        _statsText.text = _crystals.ToString();
    }
    public void RedString()
    {
        _statsText.color = _redColor;
        Invoke (nameof(ColorChangeBack), 1);
    }
    private void ColorChangeBack ()
    {
        _statsText.color = _purpleColor;
    }
}
