using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public enum LevelTypes
{
    Office = 1,
    Store = 2,
    Medrese = 3,
    Stadium = 4
}

public class LevelItem : MonoBehaviour {

    public LevelTypes levelType;
    public Image LevelImage;
    public Text LevelNameText;


    public void OnLevelItemClicked()
    {
        PanelBase panel = GameController.Instance.PanelController.Panels[PanelName.LevelLobbyPanel];
        LevelLobbyPanel slp = (LevelLobbyPanel)panel;

        slp.StartLevel(levelType, LevelImage.sprite);
        //GameController.Instance.PanelController.OpenPanel(PanelName.LevelLobbyPanel);

    }
}
