using System;
using Controllers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : PanelBase
{
    public Text Header;
    [SerializeField]
    public List<PlayerEntry> Players;

    public GameObject Player1Container;
    public GameObject Player2Container;
    public GameObject Player3Container;
    public GameObject Player4Container;

    public Text Player1Score;
    public Text Player2Score;
    public Text Player3Score;
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
        Open(_playerList);
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
            Player4Container.gameObject.SetActive(true);
            Player4Score.text = playerList[3].Score.ToString();
        }
        if (playerCount >= 3)
        {
            Player3Container.gameObject.SetActive(true);
            Player3Score.text = playerList[2].Score.ToString();
        }
        if (playerCount >= 2)
        {
            Player2Container.gameObject.SetActive(true);
            Player1Container.gameObject.SetActive(true);

            Player2Score.text = playerList[1].Score.ToString();
            Player1Score.text = playerList[0].Score.ToString();
            
        }
        if (playerCount == 1 || playerCount == 0)
        {
            Debug.LogError("Victory panel player list is smaller than 2 player");
            return;
        }
    }

    [Serializable]
    public class PlayerEntry
    {
        public Image PlayerImage;
        public Text PlayerScore;
    }
    
}
