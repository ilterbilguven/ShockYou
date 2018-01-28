using System;
using Controllers;
using UnityEngine;

public class HandController : MonoBehaviour {

    public BaseSpell SpellInHand;
    public GameObject SpellPrefab;

    public SpriteRenderer SpellSpriteRenderer;
    public PlayerController Player;
    

    public void AddSpellToHand(BaseSpell spell)
    {
        SpellInHand = spell;
        GameObject spellPrefab = GameController.Instance.SpellController.GetSpellPrefabByType(spell.SpellType);
        SpellPrefab = Instantiate(spellPrefab, transform);
    }

    public void RemoveSpell()
    {
        if(SpellPrefab != null)
            Destroy(SpellPrefab);

        SpellInHand = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Charging") || other.collider.CompareTag("Neutral"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
    }
}
