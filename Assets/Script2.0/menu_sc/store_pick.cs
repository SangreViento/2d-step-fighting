using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store_pick : MonoBehaviour
{
    public GameObject go;
    public Button pick_icon;
    public hero hero;

    void Start()
    {

        pick_icon.onClick.AddListener(pick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    void pick()
    {
        if (go.name == "Spider")
            hero.left_hero = 0;
        else if (go.name == "Henox")
            hero.left_hero = 1;
        else if (go.name == "Troll")
            hero.left_hero = 2;
        else if (go.name == "Mage")
            hero.left_hero = 3;
        else if (go.name == "Eye")
            hero.left_hero = 4;
        else if (go.name == "Skeleton")
            hero.left_hero = 5;
        else if (go.name == "Knight")
            hero.left_hero = 6;

        hero.right_hero = (int)Random.Range(0, 6);
        hero.name_of_hero = go.name;
    }
}
