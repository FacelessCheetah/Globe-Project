using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
// used: https://www.youtube.com/watch?v=sXTAzcxNqv0
public class CountryDetails : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    [SerializeField] TMP_Text yearText;
    [SerializeField] TMP_Text poplation;
    [SerializeField] TMP_Text GDP;
    [SerializeField] TMP_Text co2;
    [SerializeField] TMP_Text co2PerCapita;
    [SerializeField] TMP_Text co2PerGDP;
    [Header("desciption")]
    [SerializeField] GameObject desciptionBox;
    [SerializeField] TMP_Text desciptionText;

    [HideInInspector] public GameObject selectedCrounty;


    public void NameCrountry(MappedData hoverCrountry, GameObject cameFromCrounty)
    {
        selectedCrounty = cameFromCrounty;
        yearText.SetText(hoverCrountry.Name);
        setPopulation(hoverCrountry.population);
        //GDP.SetText("GDP: $" + hoverCrountry.gdp);
        setGDP(hoverCrountry.gdp);
        co2.SetText("CO2: " + hoverCrountry.co2 + " Mt");
        //setCO2(hoverCrountry.co2);
        co2PerCapita.SetText("CO2 per capita: " + hoverCrountry.co2_per_capita);
        co2PerGDP.SetText("CO2 per GDP: " + hoverCrountry.co2_per_gdp);
    }
    void setPopulation(long populationNumber)//adds the commas into the population number
    {
        string populationText = populationNumber.ToString("N0");
        
        poplation.SetText("population: " + populationText);
    }//test

    void setGDP(decimal gdp)
    {
        double _gdp = (double)gdp;
        _gdp = _gdp / 1000000000;
        string gpdtext = _gdp.ToString("N0");
        GDP.SetText("GDP: $" + gpdtext + " Billion");
    }
    void setCO2(float CO2)
    {
        print("hui");
        string co2Text = CO2.ToString("N0");

       co2.SetText("CO2: " + co2Text + " Mt");
    }
    public void DragHandler(BaseEventData data)//allows the ui to be moved with mouse;
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
    }
    public void close()
    {
        gameObject.SetActive(false);
        if(desciptionBox.activeInHierarchy == false)
        {
            desciptionBox.SetActive(false);
        }
    }
    public void CloseDesciptionBox()
    {
        desciptionBox.SetActive(false);
    }
    public void OpenStatDescp(string desciption)
    {
        desciptionBox.SetActive(true);
        desciptionText.SetText(desciption);
    }
}
