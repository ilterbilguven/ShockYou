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
        _waveAnimation = LeanTween.moveLocalY(this.gameObject, this.transform.localPosition.y + 0.3f, 0.7f)
            .setFrom(this.transform.localPosition.y - 0.3f)
            .setRepeat(-1)
            .setLoopPingPong(-1);
    }
}
