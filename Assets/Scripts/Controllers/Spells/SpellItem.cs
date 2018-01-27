using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellItem : MonoBehaviour {

    public SpellType SpellType;
    public SpriteRenderer SpellImage;

    private LTDescr _waveAnimation;

    private void OnEnable()
    {
        PlayWaveAnimation();
    }

    public void OnSpellCollected()
    {
        LeanTween.cancel(_waveAnimation.uniqueId);
    }

    private void PlayWaveAnimation()
    {
        _waveAnimation = LeanTween.moveLocalY(this.gameObject, this.transform.localPosition.y + 5, 1f)
            .setFrom(this.transform.localPosition.y - 5)
            .setRepeat(-1)
            .setLoopPingPong(-1);
    }
}
