using CsvHelper;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class Foo 
{
    public int year { get; set; }
    public string Name { get; set; }
    public string population { get; set; }
    public string gdp { get; set; } // Inflation-adjusted and cost-of-living–adjusted economic output across countries.
    public string co2 { get; set; } //total yearly carbon dioxide emissions, excluding land-use changes, reported in million tonnes.
    public string co2_per_capita { get; set; }
    public string co2_per_gdp {  get; set; }
}
public class MappedData
{
    public int year { get; set; }
    public string Name { get; set; }
    public long population { get; set; }
    public decimal gdp { get; set; } // Inflation-adjusted and cost-of-living–adjusted economic output across countries.
    public float co2 { get; set; } //total yearly carbon dioxide emissions, excluding land-use changes, reported in million tonnes.
    public float co2_per_capita { get; set; } //Yearly carbon dioxide (CO2) emissions, excluding land-use change, expressed in tonnes per person.
    public float co2_per_gdp {  get; set; }
}

public class importData
{
    public List<MappedData> ReadCsv(string srcFilePath, string destFilePath)
    {
        List<MappedData> myList = new List<MappedData>(); //creates a MappedData list to return to Data Vislulise.

        using (var reader = new StreamReader(srcFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();//all csv and reader stuff is part of the csv healper.

            while (csv.Read())
            {
                var record = csv.GetRecord<Foo>(); //reads the csv file based on the varibles that Foo has. and sets them to what the csv has

                if (record.co2 == "" || record.gdp == "" || record.year <= 1950 || record.co2_per_capita == ""|| record.co2_per_gdp == "")
                {
                    continue; //if the empty data it skips the year and goes to the next one.
                }

                try
                {   //converts Foo(record) into MappedData
                    myList.Add(new MappedData
                    {
                        Name = record.Name,
                        year = record.year,
                        population = long.Parse(record.population),
                        gdp = decimal.Parse(record.gdp, NumberStyles.Currency),
                        co2 = float.Parse(record.co2),
                        co2_per_capita = float.Parse(record.co2_per_capita),
                        co2_per_gdp = float.Parse(record.co2_per_gdp)

                    });

                }
                catch (System.OverflowException)
                {
                    Debug.LogWarning(record.population);
                }
                
            }
            reader.Close();
            
        }
        WriteCsv(myList, destFilePath);//saves myList into a new Csv file.
        return myList;
    }

    private void WriteCsv(List<MappedData> myList, string filepath)
    {
        using (var writer = new StreamWriter(filepath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(myList);
        }
    }
}

