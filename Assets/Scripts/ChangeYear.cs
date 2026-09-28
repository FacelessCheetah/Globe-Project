using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class ChangeYear : MonoBehaviour
{
    [SerializeField] Slider yearSlider;
    [SerializeField] DataVislulise data;
    [SerializeField] TMP_Text yearText;
    [SerializeField] Dropdown nameDropdown;
    List<MappedData> countryData;

    public UnityEvent<int> currentYear;


    public void SetUP()
    {
        countryData = new List<MappedData>();
        countryData = data.countryDict["United Kingdom"];
        yearSlider.maxValue = countryData.Count - 1;

        yearSlider.onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(float input)
    {
        var currentIndex = (int)input;
        var item = countryData[currentIndex];
        yearText.text = ("Year: " + item.year);
        currentYear.Invoke(currentIndex);
        
    }

}
