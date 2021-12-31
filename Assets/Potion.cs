using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PotionType
{
    HEAL = 1,
    MANA = 2,
}
public class Potion : MonoBehaviour
{
    public PotionType potionType;
    public float respawnTime;
    public float currentTime;
    public bool isUsed;

    // Start is called before the first frame update
    private void Update()
    {
        if (isUsed)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            currentTime += Time.deltaTime;
            if (currentTime >= respawnTime)
            {
                currentTime = 0;
                GetComponent<SpriteRenderer>().enabled = true;
                isUsed = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            
            isUsed = true;
            CharacterStat playerStat = collider.gameObject.GetComponent<CharacterStat>();
            switch (potionType)
            {
                case PotionType.HEAL:
                    playerStat.ChangeHealth(1);
                    break;
                case PotionType.MANA:
                    playerStat.ChangeMana(2);
                    break;
                default:
                    break;
            }
        }
    }
}
