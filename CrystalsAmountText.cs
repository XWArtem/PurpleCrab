using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CrystalsAmountText : MonoBehaviour
{
    public TextMeshProUGUI _statsText;
    public Gradient PurpleToRed;
    private Mesh mesh;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Color32 purpleColor;
    [SerializeField] private Color32 redColor;
    [SerializeField] Color32 MyColor;


    public void Start()
    {
        SetTheString();
        purpleColor = new Color32(133, 10, 147, 255);
        redColor = new Color32(231, 8, 72, 255);
    }

    // Update is called once per frame
    public void SetTheString()
    {
        _statsText.text = GameManager.Instance.Crystals.ToString();
    }
    //public void Update()
    //{
    //
     //   _statsText.color = PurpleToRed.Evaluate(Mathf.Repeat(Time.time, 1f));
     //   
        //_statsText.color = Color32.Lerp(purpleColor, redColor, 0.2f);
   // }
    public void RedString()
    {
        _statsText.color = redColor;
        Invoke ("ColorChangeBack", 1);
    }
    //public void ColorChange()
    //{
    // 
    //}
    private void ColorChangeBack ()
    {
        _statsText.color = purpleColor;
    }

}
