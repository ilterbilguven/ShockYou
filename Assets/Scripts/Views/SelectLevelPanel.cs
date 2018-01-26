using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelPanel : PanelBase {

    public void OnBackButtonClicked()
    {
        GameController.Instance.PanelController.OpenPanel(PanelName.MainMenuPanel);
    }
}
