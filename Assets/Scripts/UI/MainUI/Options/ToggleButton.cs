using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private bool flag;
    public bool Value
    {
        get => flag;
        set
        {
            flag = value;
            if (flag)
            {
                label.text = "on";
                label.color = new Color(0.7f, 0.7f, 0.7f);
                image.color = new Color(0.4f, 0.4f, 0.4f);
                return;
            }

            label.text = "off";
            label.color = new Color(0.4f, 0.4f, 0.4f);
            image.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
    
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI label;
}