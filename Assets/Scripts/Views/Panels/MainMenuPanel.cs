using Controllers;
using UnityEngine;

public class MainMenuPanel : PanelBase
{

    void Update()
    {
        if (Input.anyKey)
        {
            GameController.Instance.PanelController.OpenPanel(PanelName.SelectLevelPanel);
        }
    }
}
