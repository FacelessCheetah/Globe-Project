using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static ChangeCubeSize;

public class DataVislulise : MonoBehaviour
{
    importData _importData;
    public List<MappedData> mappedDatas;
    public Dictionary<string, List<MappedData>> countryDict;

    public UnityEvent DataReady; 
    [SerializeField]List<ChangeCubeSize> changeCubeSizes = new List<ChangeCubeSize>();

    private void Start()
    {
        _importData = new importData();

        string formattedDataNewFilePath = string.Concat(Application.persistentDataPath, "FormatedData.csv");
        string dataNewFilePath = string.Concat(Application.persistentDataPath, "Data.csv");

        changeCubeSizes = FindObjectsByType<ChangeCubeSize>(FindObjectsSortMode.None).ToList();
        GetBiggestAndSmallest();

#if UNITY_EDITOR
        if (!File.Exists(formattedDataNewFilePath))
        {
            File.Copy(string.Concat(Application.dataPath,"/StreamingAssets/FormatedData.csv"), string.Concat(Application.persistentDataPath, "FormatedData.csv"));
        }
        if (!File.Exists(dataNewFilePath))
        {
            File.Copy(string.Concat(Application.dataPath,"/StreamingAssets/Data.csv"), string.Concat(Application.persistentDataPath, "Data.csv"));
        }
#else
        if (!File.Exists(formattedDataNewFilePath))
        {
            File.Copy("CO2OnGlobe_Data/StreamingAssets/FormatedData.csv", string.Concat(Application.persistentDataPath, "FormatedData.csv"));
        }
        if (!File.Exists(dataNewFilePath))
        {
            File.Copy("CO2OnGlobe_Data/StreamingAssets/Data.csv", string.Concat(Application.persistentDataPath, "Data.csv"));
        }
#endif

        mappedDatas = _importData.ReadCsv(dataNewFilePath, formattedDataNewFilePath);//passes in the file paths
        countryDict = new Dictionary<string, List<MappedData>>();

        countryDict = FilterByCountry(mappedDatas);//creates a Dictionary with mappeddata with each entry being each counrty on it corponding data.

        DataReady.Invoke();
        Debug.Log("Invoked data ready");
    }
    Dictionary<string, List<MappedData>> FilterByCountry(List<MappedData> _input)
    {
        Dictionary<string, List<MappedData>> dict_ = new Dictionary<string, List<MappedData>>();

        foreach (MappedData mappedData in _input)
        {
            if (dict_.ContainsKey(mappedData.Name))
            {
                dict_[mappedData.Name].Add(mappedData);
            }
            else
            {
                dict_[mappedData.Name] = new List<MappedData>();
                dict_[mappedData.Name].Add(mappedData);
            }
            
        }

        return dict_;
    }
    public float biggest = 0;
    public float smallest  = 9999999999;
    public string sname;
    public string bname;
    public void GetBiggestAndSmallest()
    {
        biggest = 0;
        smallest = float.MaxValue;
        for (int i = 0; i < changeCubeSizes.Count; i++)
        {
            if (changeCubeSizes[i].size > biggest)
            {
                biggest = changeCubeSizes[i].size;

                bname = changeCubeSizes[i].name;
            }
            if (changeCubeSizes[i].size < smallest)
            {
                smallest = changeCubeSizes[i].size;

                sname = changeCubeSizes[i].name;
            }
        }
    }

}
