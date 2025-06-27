using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject ShipObject;
    Rigidbody ShipRigidBody;
    public Button LaunchButton;
    public void Start()
    {
        LaunchButton.onClick.AddListener(LaunchRocket);
       ShipRigidBody = ShipObject.GetComponent<Rigidbody>();
    }
    public void LaunchRocket()
    {
        ShipRigidBody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        //LaunchButton.gameObject.SetActive(false);

    }
}
