using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fill2048 : MonoBehaviour
{
    public int value;
    public Image image;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    public void FillValueUpdate(int ValuIn , Color color )  //pass value to display and color to hold
    {
        value= ValuIn;
        textMeshProUGUI.text =value.ToString();
        image.color = color;
    }
    public void UpdateMerge() //After merge Update Value 
    {
        value += value;
        textMeshProUGUI.text = value.ToString();
        image.color = GameController2048.instance.colorSetter.SetColor(value);
    }

}
