using Controllers;
using UnityEngine;
using UnityEngine.UI;

public enum LevelTypes
{
    Office = 1,
    Store = 2,
    Medrese = 3,
    Stadium = 4
}

public class LevelItem : MonoBehaviour {

    public LevelTypes levelType;
    public Image LevelImage;
    public Sprite LevelBackground;
    public Text LevelNameText;


    public void OnLevelItemClicked()
    {
        PanelBase panel = GameController.Instance.PanelController.Panels[PanelName.LevelLobbyPanel];

        for (int i = 0; i < 4; i++)
        {
            GameObject player = GameController.Instance.Players[i];

            player.GetComponent<PlayerController>().Container.gameObject.SetActive(false);
            player.SetActive(true);
            player.GetComponent<Rigidbody2D>().simulated = false;
        }

        LevelLobbyPanel slp = (LevelLobbyPanel)panel;

        slp.StartLevel(levelType, LevelBackground);

    }
}
