using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class CSVEditor
{
    [SerializeField]
    static string DialogueCSVPath = "/Editor/CSVs/Files/Test.csv";

    [MenuItem("FreeStylers / Spain Indie Game Jam 23 / Generate Characters")]
    public static void GenerateCharacter()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + DialogueCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(';');

            CharacterScriptableObject character = ScriptableObject.CreateInstance<CharacterScriptableObject>();
            character.characterIdentifier = splitData[0];
            character.Dialogue1 = splitData[1];
            character.Dialogue2 = splitData[2];
            character.Dialogue3 = splitData[3];
            character.Dialogue4 = splitData[4];

            AssetDatabase.CreateAsset(character, $"Assets/ScriptableObjects/Characters/{character.characterIdentifier}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
