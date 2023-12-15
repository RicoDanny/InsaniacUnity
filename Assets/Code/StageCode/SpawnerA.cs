using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerA : MonoBehaviour
{
    public string selectedCharacterTag1 = "SelectedCharacterTag1"; // Tag to identify the objects
    public string selectedCharacterTag2 = "SelectedCharacterTag2";
    public string selectedCharacterTag3 = "SelectedCharacterTag3";
    public Transform spawnDest1;
    public Transform spawnDest2;
    public Transform spawnDest3;
    public bool spawningBool = false;

    // Start is called before the first frame update
    void Start()
    {
        if (spawningBool == true)
        {
            // Find GameObject with the specified tag
            GameObject[] CharacterWithTag1 = GameObject.FindGameObjectsWithTag(selectedCharacterTag1);
            GameObject CharacterToSpawn1 = CharacterWithTag1[1];
            Instantiate(CharacterToSpawn1, spawnDest1.position, spawnDest1.rotation);

            GameObject[] CharacterWithTag2 = GameObject.FindGameObjectsWithTag(selectedCharacterTag2);
            GameObject CharacterToSpawn2 = CharacterWithTag2[2];
            Instantiate(CharacterToSpawn2, spawnDest2.position, spawnDest2.rotation);

            GameObject[] CharacterWithTag3 = GameObject.FindGameObjectsWithTag(selectedCharacterTag3);
            GameObject CharacterToSpawn3 = CharacterWithTag3[3];
            Instantiate(CharacterToSpawn3, spawnDest3.position, spawnDest3.rotation);
        }
    }
}