using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class LevelLobbyPanel : PanelBase
{
    public Image BackgroundImage;
    public int Countdown = 3;

    private LevelTypes _lvltype;

    public void StartLevel(LevelTypes levelType, Sprite levelSprite)
    {
        _lvltype = levelType;
        BackgroundImage.sprite = levelSprite;
        GameController.Instance.PanelController.OpenPanel(PanelName.LevelLobbyPanel);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        int countdown = Countdown;
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        //LoadLevel with given type
        Application.LoadLevel((int)_lvltype);
    }
}
