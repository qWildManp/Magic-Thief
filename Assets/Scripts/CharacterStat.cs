using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public bool isPlayer;
    public int health;
    public int max_Health;
    public int mana;
    public int max_Mana;
    public int previousHealth;
    public bool immune;
    public float immuneTime;
    private float tick;
    [SerializeField] GameObject playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        immune = false;
        tick = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        int currentHealth = GetCurrentHealth();
        if(currentHealth == 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("GAME OVER");
            return;
        }
        if (isPlayer)
        {
            if(previousHealth > currentHealth)
            {
                if(immuneTime > 0)
                    immune = true;
            }
        }
        if (immune)
        {
            Debug.Log("immuning");
            tick += Time.deltaTime;
            float remainder = tick % 0.3f;
            playerSprite.GetComponent<SpriteRenderer>().enabled = remainder > 0.15f;
        }
        if (tick >= immuneTime)
        {
            tick = 0;
            playerSprite.GetComponent<SpriteRenderer>().enabled = true;
           immune = false;
        }
        previousHealth = GetCurrentHealth();
    }
    public void ChangeHealth(int num)
    {
        if(immune)
        {
            return;
        }
        if(health + num < 0)
        {
            this.health = 0;
        }
        else if(health + num > max_Health)
        {
            this.health = max_Health;
        }
        else
        {
            this.health += num;
        }
        
    }
    public int GetCurrentHealth()
    {
        return this.health;
    }

    public int GetCurrentMana()
    {
        return this.mana;
    }
    public void ChangeMana(int num)
    {
       
        if (mana + num < 0)
        {
            this.mana = 0;
        }
        else if (mana + num > max_Mana)
        {
            this.mana = max_Mana;
        }
        else
        {
            this.mana += num;
        }

    }
}
