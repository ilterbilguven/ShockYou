using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomSpell : BaseSpell {

    public static SpellType UseSpell(PlayerController player)
    {
        Random rand = new Random();

        int whichspell = rand.Next(1, 3);
        SpellType spell = (SpellType) whichspell;
        Debug.Log("Random spell " + spell + " added to " + player.PlayerID);

        player.RemoveSpell();
        return spell;
    }
}
