using UnityEngine;
using System.Collections;
using UnityEditor;

public class CSVParserWindow : EditorWindow 
{
    TextAsset               clothDataText;
    MockClothDataScriptable clothesData;

    void OnEnable()
    {
        clothesData = Resources.Load("TestClothData") as MockClothDataScriptable;
    }

    [MenuItem ("Window/DataParser")]
    static void Init()
    {
        CSVParserWindow window = (CSVParserWindow) EditorWindow.GetWindow(typeof(CSVParserWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Data Parser", EditorStyles.boldLabel);

        DrawClothParser();

        GUILayout.Space(10);

    }

    void DrawClothParser()
    {
        GUILayout.Space(10);

        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
                GUILayout.Label("Clothes Data CSV:", EditorStyles.boldLabel);
                clothDataText =  EditorGUILayout.ObjectField(clothDataText,typeof(TextAsset), false) as TextAsset;
                if(clothDataText == null)
                {
                    GUI.backgroundColor= Color.red;
                } else
                {
                    GUI.backgroundColor= Color.green;
                }
                if(GUILayout.Button("Parse clothes"))
                {
                    ParseClothes();
                }
                GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.Label("Cloth data scriptable: ", EditorStyles.boldLabel);
                EditorGUILayout.ObjectField(clothesData,typeof(MockClothDataScriptable),false);
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.Space(10);
    }

    void ParseClothes()
    {
        if(clothDataText != null)
        {
        BM3ClothDataParser clothDataParser = new BM3ClothDataParser();
        clothesData.data = clothDataParser.ParseAllItems(clothDataText);

        Debug.Log("Parse Clothes");
        }
        else
        {
            Debug.LogError("Reference to clothData csv not found");
        }

    }
}
