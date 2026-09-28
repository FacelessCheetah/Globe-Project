using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChangeCubeSize : MonoBehaviour
{
    public enum DataShowing
    {
        CO2,
        CO2_per_capita,
        CO2_per_GDP
    }
    public List<MappedData> countryData;
    [SerializeField] DataVislulise data;
    [SerializeField] string crounty;
    [SerializeField]CountryDetails countryDetails;
    bool countrySelected;
    int currentIndex;
    public DataShowing dataShowing = DataShowing.CO2;

    float Scale;
    public float size = 0;
    public void SetUp()
    {
        print("egg");
        countryData = new List<MappedData>();
        countryData = data.countryDict[crounty];
        Debug.Log(countryData.Count + " " + crounty);
        ChangeCube(0);
    }
    public void ChangeViewingData(int value)//uses the dropdown box
    {
        dataShowing = (DataShowing)value;
        ChangeCube(currentIndex);
    }
    public void ChangeCube(int index)//gets called when the year is changed
    {
      
        currentIndex = index;
        switch (dataShowing)
        {
            case DataShowing.CO2:
                Scale = 0.01f;
                size = (countryData[index].co2 * Scale) / 2;               
                break;
            case DataShowing.CO2_per_capita:
                Scale = 0.4f;
                size = (countryData[index].co2_per_capita * Scale) / 2;
                break;
            case DataShowing.CO2_per_GDP:
                Scale = 1.3f;
                size = (countryData[index].co2_per_gdp * Scale) / 2;
                break;
        }
        transform.localScale = new Vector3(0.02f, size, 0.02f);

        if(gameObject == countryDetails.selectedCrounty)
        {
            countryDetails.NameCrountry(countryData[currentIndex], gameObject);
        }
        SetColour();
        //for changing data use a switch statment 
        // make (size * 0.01f)/2 a float that can be changed
        // so scaleChange = (size * 0.01f)/
        //use an enum for differnt ones
    }
    private void SetColour()
    {
        //set the range for the green to red colour change 
        float t = (size - 0.00031f) / (2.75269f - 0.00031f);
        Color targetColor = Color.Lerp(Color.green, Color.red, t);

        // Apply color to the material
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = targetColor;
    }
    public void CubeHasBeenClickedOn()
    {
        print("boxClicked");
        if (isActiveAndEnabled)
        {
            countryDetails.gameObject.SetActive(true);
        }
        countryDetails.NameCrountry(countryData[currentIndex], gameObject);
    }
    private void OnMouseEnter()
    {
        
    }
}
//try was for detecting if something was out of range 
//{
//    size = countryData[index].co2;
//    currentIndex = index;
//}
//catch (System.ArgumentOutOfRangeException)
//{
//    size = countryData[countryData.Count - 1].co2;
//    currentIndex = countryData.Count - 1;

//    Debug.LogWarning($"Ran out of data. Index: {index} does not exist in list for {crounty}");
//}