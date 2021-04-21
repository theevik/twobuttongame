﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Slider manaSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "lvl" + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        manaSlider.maxValue = unit.maxMana;
        manaSlider.value = unit.mana;
        
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMana(int m)
    {
        manaSlider.value = m;
    }

}