using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : PanelBase
{
    public Text Header;
    public Image Player1Image;
    public Text Player1Score;
    public Image Player2Image;
    public Text Player2Score;
    public Image Player3Image;
    public Text Player3Score;
    public Image Player4Image;
    public Text Player4Score;

    private LTDescr _waveZoomAnimation;

    private List<PlayerController> _playerList = new List<PlayerController>();

    private void OnEnable()
    {
        PlayWaveZoomAnimation();
        
        foreach (var item in GameController.Instance.Players)
        {
            _playerList.Add(item.GetComponent<PlayerController>());
        }
    }

    private void OnDisable()
    {
        LeanTween.cancel(_waveZoomAnimation.uniqueId);
        _playerList.Clear();
    }

    private void PlayWaveZoomAnimation()
    {
        _waveZoomAnimation = LeanTween.scale(Header.gameObject, Header.gameObject.transform.localScale + new Vector3(0.3f, 0.3f, 0.3f), 1.2f).setRepeat(-1).setLoopPingPong(3);
    }

    public void Open(List<PlayerController> playerList)
    {
        int playerCount = playerList.Count;

        if (playerCount >= 4)
        {
            Player4Image.sprite = playerList[3].GetPlayerSpriteByType();
            //Player4Score.text = playerList[3].;
            Player4Score.text = "200";
        }
        if (playerCount >= 3)
        {
            Player3Image.sprite = playerList[2].GetPlayerSpriteByType();
            //Player3Score.text = playerList[2].player;
            Player3Score.text = "150";
        }
        if (playerCount >= 2)
        {
            Player2Image.sprite = playerList[1].GetPlayerSpriteByType();
            //Player2Score.text = playerList[1].player;
            Player2Score.text = "100";

            Player1Image.sprite = playerList[0].GetPlayerSpriteByType();
            //Player1Score.text = playerList[0].player;
            Player1Score.text = "50";
        }
        if (playerCount == 1 || playerCount == 0)
        {
            Debug.LogError("Victory panel player list is smaller than 2 player");
            return;
        }
    }
    
}
