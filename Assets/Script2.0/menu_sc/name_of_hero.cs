using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class name_of_hero : MonoBehaviour
{
    Text picked_hero_name;
    // Start is called before the first frame update

    void Start()
    {
        picked_hero_name = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        picked_hero_name.text = hero.name_of_hero;

    }
}
