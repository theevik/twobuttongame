using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD3 : MonoBehaviour
{
    public Text nameText3;
    public Text levelText3;
    public Slider hpSlider3;
    public Slider manaSlider3;


    public void SetHUD3(Unit3 unit3)
    {
        nameText3.text = unit3.unitName;
        levelText3.text = "lvl" + unit3.unitLevel;
        hpSlider3.maxValue = unit3.maxHP;
        hpSlider3.value = unit3.currentHP;
        manaSlider3.maxValue = unit3.maxMana;
        manaSlider3.value = unit3.mana;
    }

    public void SetHP3(int hp)
    {
        hpSlider3.value = hp;
    }

    public void SetMana3(int m)
    {
        manaSlider3.value = m;
    }

}