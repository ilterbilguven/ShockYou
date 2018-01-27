using Controllers;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public enum SpellType
{
    Static = 0,
    Battery = 1,
    Taser = 2,
    Balloon = 3,
    Random = 42,
}

public class SpellController : SerializedMonoBehaviour
{
    [SerializeField]
    public Dictionary<SpellType, SpellItem> AllSpellIcons = new Dictionary<SpellType, SpellItem>();

    [SerializeField]
    public Dictionary<SpellType, BaseSpell> AllSpells = new Dictionary<SpellType, BaseSpell>();

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
        SpawnSpells(5);
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
            int randomNumber2 = rand.Next(0, _spellSpanPoints.Count);
            Transform spellSpawnPos = _spellSpanPoints[randomNumber2];

            // Spawn prefab at that location
            Instantiate(spawningspell, spellSpawnPos.transform.localPosition, Quaternion.identity);
        }
    }

    public void AddSpellToPlayer(SpellType spell, PlayerController player)
    {
        Debug.Log("Spell" + spell + " has added to player.");
        BaseSpell spellObject = GetSpellByType(spell);
        player.PlayerHand.AddSpellToHand(spellObject);
    }

    public BaseSpell GetSpellByType(SpellType type)
    {
        BaseSpell spell;
        if(!AllSpells.TryGetValue(type, out spell))
        {
            Debug.LogError("Couldn't get the spell by type.");
            return null;
        }

        return spell;
    }

    #region Spell action functions

    public void StaticSpell(PlayerController sender, PlayerController receiver)
    {
        int damageAmount = sender.ChargeAmount;

        // Player statics other player, collision is made by 
        Debug.Log(sender.PlayerID + " gave " + damageAmount + " static damage to " + receiver.PlayerID);
    }

    //public void BatterySpell(PlayerController sender, PlayerController receiver)
    //{
    //    int baseDamage = sender.ChargeAmount;

    //    Debug.Log(sender.PlayerID + " gave " + baseDamage + " BATTERY damage to " + receiver.PlayerID);
    //}

    //public void TaserSpell(PlayerController sender, PlayerController receiver)
    //{
    //    int baseDamage = sender.ChargeAmount;

    //    Debug.Log(sender.PlayerID + " gave " + baseDamage+75 + " TASER damage to " + receiver.PlayerID);
    //}

    //public void BaloonSpell(PlayerController user)
    //{
    //    Debug.Log(user.PlayerID + " has gained 25 charge from BALOON spell");
    //}


    #endregion


}
