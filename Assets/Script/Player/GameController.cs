using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject ShipObject;
    Rigidbody ShipRigidBody;

    public Button LaunchButton;

    public PlayerController playerController;
    bool isMoving = false;

    float CurrentFuel;
    float MaxFuel;

    int MaxResistance;
    int currentResistance;

    float TempMoney;

    public void Start()
    {
        instance = this;

        playerController = new PlayerController();
        playerController.Enable();
        playerController.Player.Move.started += ctx => isMoving = true;
        playerController.Player.Move.canceled += ctx => isMoving = false;

        ShipRigidBody = ShipObject.GetComponent<Rigidbody>();

    }

    public void Update()
    {
        if (!GameManager.instance.isPlaying) return;
        
        if (CurrentFuel > 0)
        {
            CurrentFuel -= Time.deltaTime;
            ShipRigidBody.AddForce(Vector3.up * 10 * Time.deltaTime, ForceMode.Force);
        }
        
        if (isMoving)
        {
            ShipRigidBody.AddForce(playerController.Player.Move.ReadValue<Vector2>() * 100 * Time.deltaTime, ForceMode.Force);
            
        }
        if(ShipRigidBody.linearVelocity.y < -3)
        {
            GameManager.instance.EndGame();
            GameManager.instance.AddMoney()
            isMoving = false;
        }
    }

    public void SetStats()
    {
       
    }

    public void OnPlay(float multi)
    {

        CurrentFuel = MaxFuel;
        GameManager.instance.didLaunch = true;
        LaunchRocket(multi);
    }

    public void LaunchRocket(float LaunchForceMulti)
    {
        ShipRigidBody.AddForce(Vector3.up * 10f * LaunchForceMulti, ForceMode.Impulse);
        LaunchButton.gameObject.SetActive(false);
    }
    
}
