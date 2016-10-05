using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TestClothData
{
    public string name;
    public string rarity;
    public string clothtype;

    public TestClothData (string name, string rarity, string clothtype)
    {
        this.name = name;
        this.rarity = rarity;
        this.clothtype = clothtype;
    }
    public TestClothData()
    {
        
    }
}

public class TestParser : MonoBehaviour 
{
    public TextAsset textCSV;
    public List<TestClothData> datas;
    public BM3ClothDataParser clothParser;
	
    void Start()
    {
        clothParser = new BM3ClothDataParser();
    }

	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {           
             datas = ParseClothes(textCSV);
            string[,] p = CSVParser.Parse(textCSV);
        }
	}

    List<TestClothData> ParseClothes(TextAsset txt)
    {
        return this.clothParser.ParseAllItems(txt);
    }
}
