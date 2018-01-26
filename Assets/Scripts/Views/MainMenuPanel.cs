using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : PanelBase
{

    public void OnSelectLevelButtonClick()
    {
        GameController.Instance.PanelController.OpenPanel(PanelName.SelectLevelPanel);
    }

    public void OnOptionsButtonClick()
    {

    }
}
