using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLobbyPanel : PanelBase
{
    public Color ReadyColor, NotReadyColor;
    public Image BackgroundImage;
    public Text CountdownText;
    public int Countdown;
    [SerializeField]
    public List<ReadyAreaItem> PlayerReadySubPanels;

    private LevelTypes _lvltype;

    void OnEnable()
    {
        for (int i = 0; i < GameController.Instance.Players.Count; i++)
        {
            GameController.Instance.Players[i].GetComponent<PlayerController>().PlayerToggledReady += MarkdownPlayerReady;
        }

        for (int i = 0; i < PlayerReadySubPanels.Count; i++)
        {
            PlayerReadySubPanels[i].PanelImage.color = NotReadyColor;
            PlayerReadySubPanels[i].ShowcaseSprite.sprite = GameController.Instance.Players[i].GetComponent<SpriteRenderer>().sprite;
        }

    }

    private void OnDisable()
    {
        for (int i = 0; i < GameController.Instance.Players.Count; i++)
        {
            GameController.Instance.Players[i].GetComponent<PlayerController>().PlayerToggledReady -= MarkdownPlayerReady;
        }
    }

    public void StartLevel(LevelTypes levelType, Sprite levelSprite)
    {
        _lvltype = levelType;
        BackgroundImage.sprite = levelSprite;
        GameController.Instance.PanelController.OpenPanel(PanelName.LevelLobbyPanel);

        StartCoroutine(StartCountdown());
    }

    private void MarkdownPlayerReady(string id, bool ready)
    {
        Debug.Log("Ready Button Pressed");
        Color color = PlayerReadySubPanels[Int32.Parse(id)].transform.GetChild(1).GetComponent<Image>().color;
        if (isActiveAndEnabled)
        {
            if (color == ReadyColor)
            {
                color = NotReadyColor;
            }
            else if (color == NotReadyColor)
            {
                color = ReadyColor;
            }
        }
        PlayerReadySubPanels[Int32.Parse(id)].transform.GetChild(1).GetComponent<Image>().color = color;
    }

    IEnumerator StartCountdown()
    {
        int countdown = Countdown;
        while (countdown > 0)
        {
            CountdownText.text = countdown + "";
            yield return new WaitForSeconds(1f);
            countdown--;
            //Play audio
        }
        //LoadLevel with given type
        SceneManager.LoadScene((int)_lvltype);
        
    }
}
