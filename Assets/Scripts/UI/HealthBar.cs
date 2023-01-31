using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImg;

    public void SetHealthBarFillAmount(float amount)
    {
        _healthBarImg.fillAmount = amount;
        _healthBarImg.color = Color.Lerp(Color.red, Color.green, amount);
    }
}
