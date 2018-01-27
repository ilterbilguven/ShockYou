using Controllers;
using UnityEngine;

public class HandController : MonoBehaviour {

    public BaseSpell SpellInHand;
    public SpriteRenderer SpellSpriteRenderer;
    public PlayerController Player;

    public void AddSpellToHand(BaseSpell spell)
    {
        SpellInHand = spell;
        SpellSpriteRenderer.sprite = spell.SpellSprite;
    }
}
