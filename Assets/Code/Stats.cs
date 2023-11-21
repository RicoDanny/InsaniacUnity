using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Stats : MonoBehaviour
{
    public string jsonFilePath = "Assets/CSV/csvjson.json"; // Adjust the path based on your project structure

    private string jsonContent;

    private StatBehaviour player;

    void Start()
    {
        StatHandler();
    }

    public void StatHandler()
    {
        jsonContent = File.ReadAllText(jsonFilePath);
        player = JsonUtility.FromJson<StatBehaviour>(jsonContent);

        // Now you can use 'player' in your game
        Debug.Log($"{player.Name}'s health: {player.hp}");
    }
}
