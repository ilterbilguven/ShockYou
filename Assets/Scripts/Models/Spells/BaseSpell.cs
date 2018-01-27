using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : SerializedMonoBehaviour
{
    public Sprite SpellSprite;

    public float Radius = 0.2f;
    public ushort Charge = 10;
    public float Delay = 0;

    public Collider2D Collider;

    public virtual void UseSpell()
    {

    }
}
