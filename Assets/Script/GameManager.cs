using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ShipStatSO CurrentShipStat;
    public bool isPlaying;
    public bool didLaunch = false;
    public Button StartButton;
    public GameObject MiniGameParentGameobject;

    public float scrollSpeed = 0.1f;
    float offset = 0;
    private Material skyMat;
    public void Start()
    {
        instance = this;
        isPlaying = false;
        skyMat = RenderSettings.skybox;
        StartButton.onClick.AddListener(OnGameStart);
    }

    public void StartLaunchMiniGame()
    {
        MiniGameParentGameobject.SetActive(true);
        StartButton.gameObject.SetActive(false);
    }

    public void Update()
    {
        
        if (didLaunch) {
            
            offset += GameController.instance.ShipObject.GetComponent<Rigidbody>().linearVelocity.y * 0.01f * Time.deltaTime;
            skyMat.SetTextureOffset("_MainTex", new Vector2(skyMat.GetTextureOffset("_MainTex").x, offset));
        }

    }

    public void OnGameStart()
    {
        isPlaying = true;
        StartLaunchMiniGame();
    }
}
