using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] GameObject HealthBarUI;
    [SerializeField] GameObject ManaBarUI;
    CharacterStat playerStat;
    void Start()
    {
        playerStat = player.GetComponent<CharacterStat>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealthUI();
        ChangeManaUI();
    }

    private void ChangeHealthUI()
    {
        float currentHealth = playerStat.health;
        float maxHealth = playerStat.max_Health;
        float currentRatio = currentHealth / maxHealth;

        HealthBarUI.GetComponent<Image>().fillAmount = currentRatio;
    }
    private void ChangeManaUI()
    {
        float currentMana = playerStat.mana;
        float maxMana = playerStat.max_Mana;
        float currentRatio = currentMana / maxMana;
        ManaBarUI.GetComponent<Image>().fillAmount = currentRatio;
    }
}
