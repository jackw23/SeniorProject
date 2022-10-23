using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton
{
    // Start is called before the first frame update

    public int number;
    public string bottomText;
    public string element;
    public string attackType;

    public UpgradeButton(string element, string attackType, string bottomText, int number)
    {
        this.element = element;
        this.attackType = attackType;
        this.bottomText = bottomText;
        this.number = number;

    }
    public void Upgrade()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
