using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLobbyPanel : PanelBase
{
    public Image BackgroundImage;
    public Text CountdownText;
    public int Countdown;
    public static int PlayerCount;
    [SerializeField]
    public List<ReadyAreaItem> PlayerReadySubPanels;

    private LevelTypes _lvltype;

    void OnEnable()
    {
        PlayerCount = 0;

        for (int i = 0; i < GameController.Instance.Players.Count; i++)
        {
            GameController.Instance.Players[i].SetActive(true);
        }

        StartCoroutine(RegisterLobbyEvent());
    }

    private IEnumerator RegisterLobbyEvent()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameController.Instance.Players.ForEach(o => o.GetComponent<PlayerController>().PlayerToggledReady += MarkdownPlayerReady);
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
        GameController.Instance.PanelController.OpenPanel(PanelName.LevelLobbyPanel);

        StartCoroutine(StartCountdown(Countdown));
    }

    private void MarkdownPlayerReady(string id, bool ready)
    {
        Debug.Log("T1");
        PlayerReadySubPanels[Int32.Parse(id)].IsReady = ready;
        PlayerReadySubPanels[Int32.Parse(id)].Skeleton.enabled = PlayerReadySubPanels[Int32.Parse(id)].IsReady;
        PlayerCount = ready ? PlayerCount + 1 : PlayerCount - 1;
    }

    IEnumerator StartCountdown(int countdown)
    {
        int localCountdown = countdown;

        Debug.Log(countdown + ", " + PlayerCount);

        yield return new WaitUntil(() => PlayerCount > 1);

        while (localCountdown > 0)
        {
            yield return new WaitForSeconds(1f);
            CountdownText.text = localCountdown-- + "";

            if (PlayerCount < 2)
            {
                CountdownText.text = "";
                localCountdown = countdown;
                yield return new WaitUntil(() => PlayerCount > 1);
            }
        }

        StartLevel();
    }

    void StartLevel()
    {
        SceneManager.LoadScene((int)_lvltype);

        GameController.Instance.LevelStarted += LevelStarted;
    }

    private void LevelStarted()
    {
        GameController.Instance.PanelController.ActivePanel.gameObject.SetActive(false);
        GameController.Instance.PanelController.ActivePanel = null;

        for (int i = 0; i < 4; i++)
        {
            if (PlayerReadySubPanels[i].IsReady)
            {
                GameObject player = GameController.Instance.Players[i];
                player.GetComponent<Rigidbody2D>().simulated = true;
                player.transform.localPosition = GameController.Instance.LevelController.SpawnPoints[i].position;
                player.GetComponent<PlayerController>().CanvasContainer.gameObject.SetActive(true);
                player.GetComponent<PlayerController>().PhysicsContainer.gameObject.SetActive(true);
                player.GetComponent<PlayerController>().Score = 0;
            }
        }
        //Activate players;
    }
}
