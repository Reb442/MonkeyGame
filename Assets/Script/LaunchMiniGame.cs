using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.CameraUI;

public class LaunchMiniGame : MonoBehaviour
{
    public Slider HitSlider;
    public Slider TargetSlider;
    public Button LaunchButton;
    public int HitSize;
    public bool isPlaying;
    public int dir;
    public void Start()
    {
        HitSlider.maxValue = 100;
        TargetSlider.maxValue = 90;
        TargetSlider.minValue = 10;
        LaunchButton.onClick.AddListener(OnLaunch);
    }

    public void OnEnable()
    {
        SetTargetSlider();
        isPlaying = true;
    }

    public void OnLaunch()
    {
        if (HitSlider.value >= TargetSlider.value - HitSize && HitSlider.value < TargetSlider.value + HitSize)
        {
            GameController.instance.OnPlay(MultiCalculator(2));
            
        }
        else
        {
            GameController.instance.OnPlay(MultiCalculator());
        }
        isPlaying = false;
        gameObject.SetActive(false);
        
    }
    public void Update()
    {
        //if (!isPlaying) return;
        
        MoveSlider();
        if (HitSlider.value == HitSlider.maxValue || HitSlider.value == HitSlider.minValue)
        {
            ChangeDir();
        }
    }

    public void MoveSlider()
    {
        HitSlider.value += 50 * dir * Time.deltaTime;
    }
    public void ChangeDir()
    {
        dir = dir * -1;
    }
    public void SetTargetSlider()
    {
        TargetSlider.value = UnityEngine.Random.Range(TargetSlider.minValue, TargetSlider.maxValue);
    }
    public float MultiCalculator()
    {
        float output = (float)Math.Round(1 - (Mathf.Abs(HitSlider.value - TargetSlider.value) / 100), 1) * 2;
        Debug.Log(output);
        return output;
    }
    public float MultiCalculator(int Jackpot)
    {
        Debug.Log(Jackpot);
        return Jackpot;
    }
}
