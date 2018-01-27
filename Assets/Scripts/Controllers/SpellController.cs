using Controllers;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public enum SpellType
{
    Random = 0,
    Battery = 1,
    Taser = 2,
    Baloon = 3
}

public class SpellController : SerializedMonoBehaviour
{
    [SerializeField]
    public Dictionary<SpellType, SpellItem> AllSpellIcons = new Dictionary<SpellType, SpellItem>();

    private LevelController _levelController;
    private List<Transform> _spellSpanPoints;
    

    private void Start()
    {
        GameController.Instance.LevelStarted += GameControllerOnLevelStarted;
    }

    private void GameControllerOnLevelStarted()
    {
        _levelController = GameController.Instance.LevelController;
        _spellSpanPoints = _levelController.SpellPoints;
        SpawnSpells(2);
    }

    private void SpawnSpells(int howManySpellToSpawn)
    {
        System.Random rand = new System.Random();

        for(int i = 0; i < howManySpellToSpawn; i++)
        {
            // Choose random spell from spawn list
            int randomNumber = rand.Next(0, AllSpellIcons.Count);
            SpellItem spawningspell = AllSpellIcons[(SpellType)randomNumber];

            // Choose random position from pos list
            randomNumber = rand.Next(0, _spellSpanPoints.Count);
            Transform spellSpawnPos = _spellSpanPoints[randomNumber];

            // Spawn prefab at that location
            Instantiate(spawningspell, spellSpawnPos.transform.localPosition, Quaternion.identity);
        }
    }

    public void SpellCollected(string playerID, SpellType spellType)
    {
        Debug.Log("Player ID:" + playerID);
        Debug.Log("Spell Type:" + spellType);
    }
}
