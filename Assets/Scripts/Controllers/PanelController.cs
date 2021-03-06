﻿using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum PanelName
{
    MainMenuPanel = 1,
    SelectLevelPanel = 2,
    LevelLobbyPanel = 3,
    VictoryPanel = 4
}

public class PanelController : SerializedMonoBehaviour {

    [SerializeField]
    public Dictionary<PanelName, PanelBase> Panels = new Dictionary<PanelName, PanelBase>();

    public PanelBase ActivePanel;

    public void OpenPanel(PanelName panelName)
    {
        if (ActivePanel != null)
            ActivePanel.gameObject.SetActive(false);

        PanelBase Panel = null;

        if (Panels.TryGetValue(panelName, out Panel) != false)
        {
            ActivePanel = Panel;
            Panel.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Panel not found.");
        }
    }
}
