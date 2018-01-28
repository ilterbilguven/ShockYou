using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : PanelBase
{

    public Sprite LevelBackground;

    void Update()
    {
        if (Input.anyKey)
        {
            PanelBase panel = GameController.Instance.PanelController.Panels[PanelName.LevelLobbyPanel];

            GameController.Instance.PanelController.OpenPanel(PanelName.LevelLobbyPanel);

            for (int i = 0; i < 4; i++)
            {
                GameObject player = GameController.Instance.Players[i];

                player.SetActive(true);
                player.GetComponent<PlayerController>().CanvasContainer.gameObject.SetActive(false);
                player.GetComponent<PlayerController>().PhysicsContainer.gameObject.SetActive(false);
                player.GetComponent<Rigidbody2D>().simulated = false;
            }

            LevelLobbyPanel slp = (LevelLobbyPanel)panel;

            slp.StartLevel(LevelTypes.Office, LevelBackground);
        }
    }
}
