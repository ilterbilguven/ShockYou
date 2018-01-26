using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelName
{
    MainMenuPanel = 1,
    SelectLevelPanel = 2
}

public class PanelController : MonoBehaviour {

    public MainMenuPanel MainMenuPanel;
    public SelectLevelPanel SelectLevelPanel;

    public GameObject ActivePanel;


    public void OpenPanel(ContentType panelName)
    {
        //if(ActivePanel)
    }
}
