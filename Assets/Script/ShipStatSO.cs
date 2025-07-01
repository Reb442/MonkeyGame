using UnityEngine;

[CreateAssetMenu(fileName = "ShipStatSO", menuName = "Scriptable Objects/ShipStatSO")]
public class ShipStatSO : ScriptableObject
{
    public float MaxFuel;
    public int MaxResistance;
    public int ThrusterPower;
    public int mass;
    public int MovePower;
}
