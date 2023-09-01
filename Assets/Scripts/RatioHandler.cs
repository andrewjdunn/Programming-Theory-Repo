using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RatioHandler : MonoBehaviour
{
    // Add this script to all sliders and set the fields - and test.. remove extra comments and then commit
    [SerializeField]
    private string optionField;

    [SerializeField]
    private GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.GetComponentInChildren<Slider>().value = (float) OptionsManager.Instance.Options.GetType().GetField(optionField).GetValue(OptionsManager.Instance.Options);
    }

    public void RatioChanged(Object changed)
    {
        if (changed is GameObject)
        {
            var ratio = changed.GetComponentInChildren<Slider>().value;
            ((GameObject)changed).gameObject.transform.Find("Ratio").GetComponent<TextMeshProUGUI>().text = $"{ratio:f2}";
            OptionsManager.Instance.Options.GetType().GetField(optionField).SetValue(OptionsManager.Instance.Options, ratio);
        }
    }
}
