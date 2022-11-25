using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIUpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yesUpgrade;
    public Button noUpgrade;
    public TMP_Text currentUpgradeCoins;
    public static UpgradeButton currentUpgradeButton;
    public static GameObject currentUBText;
    public static GameObject player;
    //public GameObject insufficientUCsText;

    void Start()
    {
        if (InventoryManager.Instance.numUpgradeCoins <= 0)
        {
            yesUpgrade.GetComponent<Button>().interactable = false;
        }
        yesUpgrade.onClick.AddListener(() => Upgrade());
    }
    public static void setUpgradeButton(UpgradeButton ub)
    {
        currentUpgradeButton = ub;
    }
    public static void setUBText(GameObject ubt)
    {
        currentUBText = ubt;
    }

    public static void setPlayer(GameObject _player)
    {
        player = _player;
    }

    public void updateCurrentUpgradeCoins(int i)
    {
        currentUpgradeCoins.text = "Current Upgrade Coins: " + i;
    }
    public void Upgrade()
    {
        if (InventoryManager.Instance.numUpgradeCoins <= 0)
        {
            //insufficientUCsFlag = true;
            //insufficientUCsText.SetActive(true);
            yesUpgrade.GetComponent<Button>().interactable = false;
        }
        else
        {
            UpdateUB();
            InventoryManager.Instance.useUpgradeCoin();
        }
    }
    public static void UpdateUB()
    {
        if (currentUpgradeButton.bottomText == "Level")
        {
            currentUpgradeButton.addNumber();
        }else if (currentUpgradeButton.bottomText == "Cooldown")
        {
            currentUpgradeButton.subtractNumber();

        }
        currentUBText.GetComponent<TMP_Text>().text = currentUpgradeButton.number.ToString();
        player.GetComponent<PlayerAttack>().LevelUpAttack("fire");

    }
    /*void OnEnable()
    {
        //upgradePanelObj.currentUpgradeButton
        //Register Button Events
        yesUpgrade.onClick.AddListener(() => currentUpgradeButton.upgradeNumber());
        //noUpgrade.onClick.AddListener(() => Upgrade(fireSkillCDBtn));
        //yesUpgrade.onClick.AddListener();
        //fireUltimateLevel.onClick.AddListener(() => buttonCallBack3());
        //fireUltimateCD.onClick.AddListener(() => buttonCallBack4());
    }*/
    // Update is called once per frame

    //calls the upgrade numebr in upgradeButton... 
    void Update()
    {
        
    }
}
