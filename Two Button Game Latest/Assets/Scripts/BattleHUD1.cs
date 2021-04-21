using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD1 : MonoBehaviour
{
    public Text nameText1;
    public Text levelText1;
    public Slider hpSlider1;
    public Slider manaSlider1;

    public void SetHUD1(Unit1 unit1)
    {
        nameText1.text = unit1.unitName;
        levelText1.text = "lvl" + unit1.unitLevel;
        hpSlider1.maxValue = unit1.maxHP;
        hpSlider1.value = unit1.currentHP;
        manaSlider1.maxValue = unit1.maxMana;
        manaSlider1.value = unit1.mana;
    }

    public void SetHP1(int hp)
    {
        hpSlider1.value = hp;
    }

    public void SetMana1(int m)
    {
        manaSlider1.value = m;
    }
}
