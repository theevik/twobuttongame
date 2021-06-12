using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//the battle state are the turns of the game. This is what makes this game's combat turn-based
public enum BattleState1 { START, PLAYERTURN, ENEMYTURN, WON, LOST }

//the Character state is so we can switch between characters.
public enum CharacterState1 { Aero, Naiden, Beta, Frost }

public enum CharacterSwitch1 { Aero, Naiden, Beta, Frost }
public enum Switcher1 { Combat, Switch }

public enum Enemystate { Thug1, Thug2, Thug3}


public class battlesystemregular : MonoBehaviour
{
    public BattleState state;
    public CharacterState charState;
    public CharacterSwitch switchState;
    public Switcher switcher;
    public Enemystate enState;


    public GameObject playerPrefab, playerPrefab1, playerPrefab2, playerPrefab3;
    public GameObject enemyPrefab, enemyPrefab1, enemyPrefab2;

    public Text dialogueText;

    //refers to the unit scripts (we need them to give stats to each character/enemy)

    Unit playerUnit;
    Unit1 player1Unit;
    Unit2 player2Unit;
    Unit3 player3Unit;

    Unit enemyUnit;
    Unit1 enemy1Unit;
    Unit2 enemy2Unit;

    //refers to the battle hud scripts (with them we transfer the character/enemy stats to their huds)

    public BattleHUD playerHUD;
    public BattleHUD1 player1HUD;
    public BattleHUD2 player2HUD;
    public BattleHUD3 player3HUD;

    public BattleHUD enemyHUD;

    public GameObject SelectionIndicator;
    public GameObject ChangeFromAeroIndicator;
    public GameObject ChangeFromNaidenIndicator;
    public GameObject ChangeFromBetaIndicator;
    public GameObject ChangeFromFrostIndicator;

    public GameObject OptionButton1;
    public GameObject OptionButton2;
    public GameObject OptionButton3;

    public GameObject ActionsMenu;
    public GameObject AeroSwitchMenu;
    public GameObject NaidenSwitchMenu;
    public GameObject BetaSwitchMenu;
    public GameObject FrostSwitchMenu;

    public int selectionChangeDelay = 2;
    public int currentSelection = 0;
    public float timer;
    public GameObject Player;

    public int healAbility = 1;
    public int changeAbility = 1;
    public int critChance;

    public int enemyswitches = 3;
    public int enemylives = 3;

    public int frozen = 0;
    public int armor;

    float crit;

    //sprites--------------------

    public Sprite frostidle;
    public Sprite frostattack;

    // Start is called before the first frame update
    void Start()
    {
        timer = selectionChangeDelay;
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }


    //this is the initial state of the game, spawns the characters that will participate in the battle.


    IEnumerator SetupBattle()
    {



        AeroSwitchMenu.SetActive(false);
        NaidenSwitchMenu.SetActive(false);
        BetaSwitchMenu.SetActive(false);
        FrostSwitchMenu.SetActive(false);


        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject player1GO = playerPrefab1;
        player1Unit = player1GO.GetComponent<Unit1>();

        GameObject player2GO = playerPrefab2;
        player2Unit = player2GO.GetComponent<Unit2>();

        GameObject player3GO = playerPrefab3;
        player3Unit = player3GO.GetComponent<Unit3>();

        GameObject enemyGO = enemyPrefab;
        enemyUnit = enemyGO.GetComponent<Unit>();

        GameObject enemy1GO = enemyPrefab1;
        enemy1Unit = enemy1GO.GetComponent<Unit1>();

        GameObject enemy2GO = enemyPrefab2;
        enemy2Unit = enemy2GO.GetComponent<Unit2>();


        dialogueText.text = "PREPARE FOR BATTLE";

        playerHUD.SetHUD(playerUnit);
        player1HUD.SetHUD1(player1Unit);
        player2HUD.SetHUD2(player2Unit);
        player3HUD.SetHUD3(player3Unit);

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        charState = CharacterState.Aero;
        switchState = CharacterSwitch.Aero;
        switcher = Switcher.Combat;
        enState = Enemystate.Thug1;
        PlayerTurn();

        

    }



    IEnumerator PlayerAttack()
    {
        crit = Random.Range(1.0f, 100.0f);


        if (enState == Enemystate.Thug1)
        {
            if (charState == CharacterState.Aero)
            {

                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(playerUnit.damage * 2);

                    if (isDead)
                    {
                        //state = BattleState.WON;
                        //EndBattle();

                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());

                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);


            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player1Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player1Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player2Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player2Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }

        else if (enState == Enemystate.Thug2)
        {
            if (charState == CharacterState.Aero)
            {

                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(playerUnit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(playerUnit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                //yield return new WaitForSeconds(2f);


            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player1Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player1Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player2Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player2Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }
        else if (enState == Enemystate.Thug3)
        {
            if (charState == CharacterState.Aero)
            {

                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(playerUnit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(playerUnit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                //yield return new WaitForSeconds(2f);


            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player1Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player1Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player2Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player2Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.damage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.damage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }


    }

    IEnumerator Ultimate()
    {
        yield return new WaitForSeconds(1f);
        crit = Random.Range(1.0f, 100.0f);


        if (enState == Enemystate.Thug1)
        {
            if (charState == CharacterState.Aero)
            {

                bool isDead = enemyUnit.TakeDamage(playerUnit.heavydamage * 3);

                if (isDead)
                {
                    enemylives = enemylives - 1;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }

            }

            else if (charState == CharacterState.Beta)
            {

                playerUnit.BetaHeal(player2Unit.healing);
                player1Unit.BetaHeal(player2Unit.healing);
                player2Unit.BetaHeal(player2Unit.healing);
                player3Unit.BetaHeal(player2Unit.healing);

                playerHUD.SetHP(playerUnit.currentHP);
                player1HUD.SetHP1(playerUnit.currentHP);
                player2HUD.SetHP2(playerUnit.currentHP);
                player3HUD.SetHP3(playerUnit.currentHP);

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }

            else if (charState == CharacterState.Frost)
            {
               // this.playerPrefab3.GetComponent<SpriteRenderer>().sprite = frostattack;

                frozen = 1;
                changeAbility = 1;

                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                yield return new WaitForSeconds(0f);
            }
            else if (charState == CharacterState.Naiden)
            {
                armor = 4;

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        else if (enState == Enemystate.Thug2)
        {
            if (charState == CharacterState.Aero)
            {

                bool isDead = enemy1Unit.TakeDamage(playerUnit.heavydamage * 3);

                if (isDead)
                {
                    enemylives = enemylives - 1;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }

            }

            else if (charState == CharacterState.Beta)
            {

                playerUnit.BetaHeal(player2Unit.healing);
                player1Unit.BetaHeal(player2Unit.healing);
                player2Unit.BetaHeal(player2Unit.healing);
                player3Unit.BetaHeal(player2Unit.healing);

                playerHUD.SetHP(playerUnit.currentHP);
                player1HUD.SetHP1(playerUnit.currentHP);
                player2HUD.SetHP2(playerUnit.currentHP);
                player3HUD.SetHP3(playerUnit.currentHP);

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }

            else if (charState == CharacterState.Frost)
            {

                frozen = 1;
                changeAbility = 1;

                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                yield return new WaitForSeconds(0f);
            }
            else if (charState == CharacterState.Naiden)
            {
                armor = 4;

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        if (enState == Enemystate.Thug3)
        {
            if (charState == CharacterState.Aero)
            {

                bool isDead = enemy2Unit.TakeDamage(playerUnit.heavydamage * 3);

                if (isDead)
                {
                    enemylives = enemylives - 1;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }

            }

            else if (charState == CharacterState.Beta)
            {

                playerUnit.BetaHeal(player2Unit.healing);
                player1Unit.BetaHeal(player2Unit.healing);
                player2Unit.BetaHeal(player2Unit.healing);
                player3Unit.BetaHeal(player2Unit.healing);

                playerHUD.SetHP(playerUnit.currentHP);
                player1HUD.SetHP1(playerUnit.currentHP);
                player2HUD.SetHP2(playerUnit.currentHP);
                player3HUD.SetHP3(playerUnit.currentHP);

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }

            else if (charState == CharacterState.Frost)
            {

                frozen = 1;
                changeAbility = 1;

                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                yield return new WaitForSeconds(0f);
            }
            else if (charState == CharacterState.Naiden)
            {
                armor = 4;

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator HeavyAttack()
    {
        crit = Random.Range(1.0f, 100.0f);

        if (enState == Enemystate.Thug1)
        {
            if (charState == CharacterState.Aero)
            {


                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(playerUnit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(playerUnit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player1Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player1Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player2Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player2Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemyUnit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemyUnit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }
        else if (enState == Enemystate.Thug2)
        {
            if (charState == CharacterState.Aero)
            {


                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(playerUnit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(playerUnit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player1Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player1Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player2Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player2Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy1Unit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy1Unit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }
        else if (enState == Enemystate.Thug3)
        {
            if (charState == CharacterState.Aero)
            {


                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(playerUnit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(playerUnit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }
            else if (charState == CharacterState.Naiden)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player1Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player1Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Beta)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player2Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player2Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                //yield return new WaitForSeconds(2f);

            }

            else if (charState == CharacterState.Frost)
            {
                if (crit <= critChance)
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.heavydamage * 2);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    bool isDead = enemy2Unit.TakeDamage(player3Unit.heavydamage);

                    if (isDead)
                    {
                        enemylives = enemylives - 1;
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }


                enemyHUD.SetHP(enemy2Unit.currentHP);


                yield return new WaitForSeconds(0f);

            }
        }

    }


    //the enemy turn, currently the enemy can simply just hit back, NEEDS WORK!!!!
    IEnumerator EnemyTurn()
    {
        crit = Random.Range(1.0f, 100.0f);

        
        this.playerPrefab3.GetComponent<SpriteRenderer>().sprite = frostidle;

        //enemy heals once when under 60 health

        if (enemyUnit.currentHP <= 60 && healAbility > 0)
        {
            enemyUnit.Heal(enemyUnit.healing);
            healAbility = 0;

            enemyHUD.SetHP(enemyUnit.currentHP);
        }

        if (enemyUnit.currentHP <= 30 && enState == Enemystate.Thug1 && enemyswitches > 0)
        {
            yield return new WaitForSeconds(2f);
            enState = Enemystate.Thug2;
            enemyswitches = enemyswitches - 1;
        }

        if (enemy1Unit.currentHP <= 30 && enState == Enemystate.Thug2 && enemyswitches > 0)
        {
            yield return new WaitForSeconds(2f);
            enState = Enemystate.Thug3;
            enemyswitches = enemyswitches - 1;
        }

        if (enemy2Unit.currentHP <= 30 && enState == Enemystate.Thug3 && enemyswitches > 0)
        {
            yield return new WaitForSeconds(2f);
            enState = Enemystate.Thug1;
            enemyswitches = enemyswitches - 1;
        }

        






        if (enemyUnit.currentHP <= 0 && enState == Enemystate.Thug1)
        {
            enState = Enemystate.Thug2;
        }

        else if (enemyUnit.currentHP <= 0 && enemy1Unit.currentHP <= 0 && enState == Enemystate.Thug1)
        {
            enState = Enemystate.Thug3;
        }

        else if (enemy1Unit.currentHP <= 0 && enState == Enemystate.Thug2)
        {
            enState = Enemystate.Thug3;
        }

        else if (enemy1Unit.currentHP <= 0 && enemy2Unit.currentHP <= 0 && enState == Enemystate.Thug2)
        {
            enState = Enemystate.Thug1;
        }

        else if (enemy2Unit.currentHP <= 0 && enState == Enemystate.Thug3)
        {
            enState = Enemystate.Thug1;
        }

        else if (enemy2Unit.currentHP <= 0 && enemyUnit.currentHP <= 0 && enState == Enemystate.Thug3)
        {
            enState = Enemystate.Thug2;
        }


        if (enemylives <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {

            armor = armor - 1;

            if (armor < 0)
            {
                armor = 0;
            }

            if (frozen == 0)

            {
                ActionsMenu.SetActive(false);


                yield return new WaitForSeconds(2f);


                changeAbility = 1;



                if (playerUnit.mana <= 90)
                {
                    playerUnit.GainMana(playerUnit.gainmana);
                    playerHUD.SetMana(playerUnit.mana);
                }

                if (player1Unit.mana <= 90)
                {
                    player1Unit.GainMana1(player1Unit.gainmana);
                    player1HUD.SetMana1(player1Unit.mana);
                }

                if (player2Unit.mana <= 90)
                {
                    player2Unit.GainMana2(player2Unit.gainmana);
                    player2HUD.SetMana2(player2Unit.mana);
                }

                if (player3Unit.mana <= 90)
                {
                    player3Unit.GainMana3(player3Unit.gainmana);
                    player3HUD.SetMana3(player3Unit.mana);
                }

                if (charState == CharacterState.Aero)
                {
                    dialogueText.text = "Enemy Is Attacking";

                    yield return new WaitForSeconds(2f);



                    if (armor <= 0)
                    {
                        if (crit <= critChance)
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit.damage * 2);



                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);



                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }


                    }
                    else if (armor > 0)
                    {

                        if (crit <= critChance)
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);



                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit.damage / 2);



                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle(); ;
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }

                }
                else if (charState == CharacterState.Naiden)
                {
                    dialogueText.text = "Enemy Is Attacking";

                    yield return new WaitForSeconds(2f);



                    if (armor <= 0)
                    {
                        if (crit <= critChance)
                        {
                            bool isDead = player1Unit.TakeDamage(enemyUnit.damage * 2);

                            player1HUD.SetHP1(player1Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }
                        else
                        {
                            bool isDead = player1Unit.TakeDamage(enemyUnit.damage);

                            player1HUD.SetHP1(player1Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }
                    else if (armor > 0)
                    {

                        if (crit <= critChance)
                        {
                            bool isDead = player1Unit.TakeDamage(enemyUnit.damage);

                            player1HUD.SetHP1(player1Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                        else
                        {
                            bool isDead = player1Unit.TakeDamage(enemyUnit.damage / 2);

                            player1HUD.SetHP1(player1Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }
                }
                else if (charState == CharacterState.Beta)
                {
                    dialogueText.text = "Enemy Is Attacking";

                    yield return new WaitForSeconds(2f);



                    if (armor <= 0)
                    {

                        if (crit <= critChance)
                        {
                            bool isDead = player2Unit.TakeDamage(enemyUnit.damage * 2);

                            player2HUD.SetHP2(player2Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }
                        else
                        {
                            bool isDead = player2Unit.TakeDamage(enemyUnit.damage);

                            player2HUD.SetHP2(player2Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }
                    else if (armor > 0)
                    {

                        if (crit <= critChance)
                        {
                            bool isDead = player2Unit.TakeDamage(enemyUnit.damage);

                            player2HUD.SetHP2(player2Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }
                        else
                        {
                            bool isDead = player2Unit.TakeDamage(enemyUnit.damage / 2);

                            player2HUD.SetHP2(player2Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }
                }
                else if (charState == CharacterState.Frost)
                {
                    dialogueText.text = "Enemy Is Attacking";

                    yield return new WaitForSeconds(2f);



                    if (armor <= 0)
                    {

                        if (crit <= critChance)
                        {
                            bool isDead = player3Unit.TakeDamage(enemyUnit.damage * 2);

                            player3HUD.SetHP3(player3Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }
                        else
                        {
                            bool isDead = player3Unit.TakeDamage(enemyUnit.damage);

                            player3HUD.SetHP3(player3Unit.currentHP);

                            yield return new WaitForSeconds(1f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                EndBattle();
                            }
                            else
                            {
                                state = BattleState.PLAYERTURN;
                                PlayerTurn();
                            }
                        }

                    }
                }
                else if (armor > 0)
                {

                    if (crit <= critChance)
                    {
                        bool isDead = player3Unit.TakeDamage(enemyUnit.damage);

                        player3HUD.SetHP3(player3Unit.currentHP);

                        yield return new WaitForSeconds(1f);

                        if (isDead)
                        {
                            state = BattleState.LOST;
                            EndBattle();
                        }
                        else
                        {
                            state = BattleState.PLAYERTURN;
                            PlayerTurn();
                        }
                    }
                    else
                    {
                        bool isDead = player3Unit.TakeDamage(enemyUnit.damage / 2);

                        player3HUD.SetHP3(player3Unit.currentHP);

                        yield return new WaitForSeconds(1f);

                        if (isDead)
                        {
                            state = BattleState.LOST;
                            EndBattle();
                        }
                        else
                        {
                            state = BattleState.PLAYERTURN;
                            PlayerTurn();
                        }
                    }

                }
            }
            else if (frozen == 1)
            {
                frozen = 0;
                state = BattleState.PLAYERTURN;
                PlayerTurn();

            }


        }




        


    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "YOU WON";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "YOU LOST";
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "YOUR TURN";
        ActionsMenu.SetActive(true);
    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }



    void Update()
    {


        timer = timer - Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = selectionChangeDelay;

            currentSelection = currentSelection + 1;
            if (currentSelection > 3)
            {
                currentSelection = 0;
            }
        }

        if (currentSelection == 0)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 60, 0);
            ChangeFromAeroIndicator.transform.localPosition = new Vector3(-100, 60, 0);
            ChangeFromNaidenIndicator.transform.localPosition = new Vector3(-100, 60, 0);
            ChangeFromBetaIndicator.transform.localPosition = new Vector3(-100, 60, 0);
            ChangeFromFrostIndicator.transform.localPosition = new Vector3(-100, 60, 0);
        }
        if (currentSelection == 1)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 20, 0);
            ChangeFromAeroIndicator.transform.localPosition = new Vector3(-100, 20, 0);
            ChangeFromNaidenIndicator.transform.localPosition = new Vector3(-100, 20, 0);
            ChangeFromBetaIndicator.transform.localPosition = new Vector3(-100, 20, 0);
            ChangeFromFrostIndicator.transform.localPosition = new Vector3(-100, 20, 0);
        }
        if (currentSelection == 2)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -20, 0);
            ChangeFromAeroIndicator.transform.localPosition = new Vector3(-100, -20, 0);
            ChangeFromNaidenIndicator.transform.localPosition = new Vector3(-100, -20, 0);
            ChangeFromBetaIndicator.transform.localPosition = new Vector3(-100, -20, 0);
            ChangeFromFrostIndicator.transform.localPosition = new Vector3(-100, -20, 0);
        }
        if (currentSelection == 3)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -60, 0);
            ChangeFromAeroIndicator.transform.localPosition = new Vector3(-100, -60, 0);
            ChangeFromNaidenIndicator.transform.localPosition = new Vector3(-100, -60, 0);
            ChangeFromBetaIndicator.transform.localPosition = new Vector3(-100, -60, 0);
            ChangeFromFrostIndicator.transform.localPosition = new Vector3(-100, -60, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) & state == BattleState.PLAYERTURN)
        {
            if (currentSelection == 0 && switcher == Switcher.Combat)
            {

                if (state != BattleState.PLAYERTURN)
                    return;

                StartCoroutine(PlayerAttack());

            }
            if (currentSelection == 1 && switcher == Switcher.Combat)
            {
                if (charState == CharacterState.Aero && playerUnit.mana >= 33)
                {

                    playerUnit.ConsumeMana(playerUnit.losemana);
                    playerHUD.SetMana(playerUnit.mana);

                    StartCoroutine(HeavyAttack());
                }

                else if (charState == CharacterState.Naiden && player1Unit.mana >= 33)
                {
                    player1Unit.ConsumeMana1(player1Unit.losemana);
                    player1HUD.SetMana1(player1Unit.mana);

                    StartCoroutine(HeavyAttack());
                }

                else if (charState == CharacterState.Beta && player2Unit.mana >= 33)
                {
                    player2Unit.ConsumeMana2(player2Unit.losemana);
                    player2HUD.SetMana2(player2Unit.mana);

                    StartCoroutine(HeavyAttack());
                }

                else if (charState == CharacterState.Frost && player3Unit.mana >= 33)
                {
                    player3Unit.ConsumeMana3(player3Unit.losemana);
                    player3HUD.SetMana3(player3Unit.mana);

                    StartCoroutine(HeavyAttack());
                }

            }

            //ultimate abilities

            if (currentSelection == 2 && switcher == Switcher.Combat)
            {
                if (charState == CharacterState.Aero && playerUnit.mana >= 90)
                {

                    playerUnit.ConsumeMana(90);
                    playerHUD.SetMana(playerUnit.mana);

                    StartCoroutine(Ultimate());
                }

                else if (charState == CharacterState.Beta && player2Unit.mana >= 90)
                {

                    player2Unit.ConsumeMana2(90);
                    player2HUD.SetMana2(player2Unit.mana);

                    StartCoroutine(Ultimate());
                }
                else if (charState == CharacterState.Frost && player3Unit.mana >= 90)
                {

                    player3Unit.ConsumeMana3(90);
                    player3HUD.SetMana3(player3Unit.mana);

                    this.playerPrefab3.GetComponent<SpriteRenderer>().sprite = frostattack;


                    StartCoroutine(Ultimate());
                }
                else if (charState == CharacterState.Naiden && player1Unit.mana >= 90)
                {

                    player1Unit.ConsumeMana1(90);
                    player1HUD.SetMana1(player1Unit.mana);

                    StartCoroutine(Ultimate());
                }
            }

            //the character switch button



            if (currentSelection == 3 && switcher == Switcher.Combat && switchState == CharacterSwitch.Aero && changeAbility == 1)
            {
                AeroSwitchMenu.SetActive(true);
                ActionsMenu.SetActive(false);
                switcher = Switcher.Switch;
                changeAbility = 0;
            }
            else if (currentSelection == 3 && switcher == Switcher.Combat && switchState == CharacterSwitch.Naiden && changeAbility == 1)
            {
                NaidenSwitchMenu.SetActive(true);
                ActionsMenu.SetActive(false);
                switcher = Switcher.Switch;
                changeAbility = 0;
            }
            else if (currentSelection == 3 && switcher == Switcher.Combat && switchState == CharacterSwitch.Beta && changeAbility == 1)
            {
                BetaSwitchMenu.SetActive(true);
                ActionsMenu.SetActive(false);
                switcher = Switcher.Switch;
                changeAbility = 0;
            }
            else if (currentSelection == 3 && switcher == Switcher.Combat && switchState == CharacterSwitch.Frost && changeAbility == 1)
            {
                FrostSwitchMenu.SetActive(true);
                ActionsMenu.SetActive(false);
                switcher = Switcher.Switch;
                changeAbility = 0;
            }

            //IF YOU ARE AERO----------------------------------------------------------------------------------------------------------------------------
            if (currentSelection == 0 && switchState == CharacterSwitch.Aero && switcher == Switcher.Switch)
            {
                //SWITCH TO NAIDEN CODE

                AeroSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player1HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab1.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Naiden;
                charState = CharacterState.Naiden;
            }

            if (currentSelection == 1 && switchState == CharacterSwitch.Aero && switcher == Switcher.Switch)
            {
                //SWITCH TO BETA CODE

                AeroSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player2HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab2.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Beta;
                charState = CharacterState.Beta;
            }

            if (currentSelection == 2 && switchState == CharacterSwitch.Aero && switcher == Switcher.Switch)
            {
                //SWITCH TO FROST CODE

                AeroSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player3HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Frost;
                charState = CharacterState.Frost;
            }


            //IF YOU ARE NAIDEN----------------------------------------------------------------------------------------------------------------------------
            if (currentSelection == 0 && switchState == CharacterSwitch.Naiden && switcher == Switcher.Switch)
            {
                //SWITCH TO AERO CODE

                NaidenSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                playerHUD.transform.localPosition = new Vector3(-409, 87, 0);

                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Aero;
                charState = CharacterState.Aero;
            }
            if (currentSelection == 1 && switchState == CharacterSwitch.Naiden && switcher == Switcher.Switch)
            {
                //SWITCH TO BETA CODE

                NaidenSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player2HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab2.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Beta;
                charState = CharacterState.Beta;
            }
            if (currentSelection == 2 && switchState == CharacterSwitch.Naiden && switcher == Switcher.Switch)
            {
                //SWITCH TO FROST CODE

                NaidenSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player3HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Frost;
                charState = CharacterState.Frost;
            }


            //IF YOU ARE BETA----------------------------------------------------------------------------------------------------------------
            if (currentSelection == 0 && switchState == CharacterSwitch.Beta && switcher == Switcher.Switch)
            {
                //SWITCH TO AERO CODE

                BetaSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                playerHUD.transform.localPosition = new Vector3(-409, 87, 0);

                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Aero;
                charState = CharacterState.Aero;
            }

            if (currentSelection == 1 && switchState == CharacterSwitch.Beta && switcher == Switcher.Switch)
            {
                //SWITCH TO NAIDEN CODE

                BetaSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player1HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab1.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Naiden;
                charState = CharacterState.Naiden;
            }
            if (currentSelection == 2 && switchState == CharacterSwitch.Beta && switcher == Switcher.Switch)
            {
                //SWITCH TO FROST CODE

                BetaSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player3HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Frost;
                charState = CharacterState.Frost;
            }


            //IF YOU ARE FROST----------------------------------------------------------------------------------------------------------------
            if (currentSelection == 0 && switchState == CharacterSwitch.Frost && switcher == Switcher.Switch)
            {
                //SWITCH TO AERO CODE

                FrostSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                playerHUD.transform.localPosition = new Vector3(-409, 87, 0);

                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Aero;
                charState = CharacterState.Aero;
            }
            if (currentSelection == 1 && switchState == CharacterSwitch.Frost && switcher == Switcher.Switch)
            {
                //SWITCH TO NAIDEN CODE

                FrostSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player1HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab1.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Naiden;
                charState = CharacterState.Naiden;
            }
            if (currentSelection == 2 && switchState == CharacterSwitch.Frost && switcher == Switcher.Switch)
            {
                //SWITCH TO BETA CODE

                FrostSwitchMenu.SetActive(false);
                ActionsMenu.SetActive(true);
                switcher = Switcher.Combat;

                //switch the pannels
                player2HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters               
                playerPrefab2.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);

                switchState = CharacterSwitch.Beta;
                charState = CharacterState.Beta;
            }



            // Hide the panel
            //gameObject.SetActive(false);


        }

    }
}
