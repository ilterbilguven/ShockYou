using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class ReadyAreaItem : MonoBehaviour
{
    [HideInInspector]
    public bool IsReady;
    public SpriteRenderer ShowcaseSprite;
    public Image PanelImage;
    public Color Ready, NotReady;
}
