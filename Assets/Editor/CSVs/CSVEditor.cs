using UnityEngine;
using System.Collections.Generic;
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

        CharacterScriptableObject character = ScriptableObject.CreateInstance<CharacterScriptableObject>();

        character.characterIdentifier = "a";
        character.dialogues = new List<Dialogue>();

        for (int i = 0; i < allLines.Length-1; i++)
        {
            character.dialogues.Add(new Dialogue());
            character.dialogues[i].fragments = new List<string>();

            string[] splitData = allLines[i].Split(',');

            foreach (string s in splitData)
            {
                character.dialogues[i].fragments.Add(s);
            }
        }

        AssetDatabase.CreateAsset(character, $"Assets/ScriptableObjects/Characters/{"character"}.asset");

        AssetDatabase.SaveAssets();
    }
}
