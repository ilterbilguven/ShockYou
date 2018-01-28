using Sirenix.OdinInspector;
using UnityEngine;

public class BaseSpell : SerializedMonoBehaviour
{
    public SpellType SpellType;
    public Sprite SpellSprite;

    public float Radius = 0.2f;
    public ushort Charge = 10;
    public float Delay = 0;

    public Collider2D Collider;
       
}
