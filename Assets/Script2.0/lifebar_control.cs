using UnityEngine;
using UnityEngine.UI;

public class lifebar_control : MonoBehaviour
{
    public Fighter watched_figther;
    Image bar;
    RectTransform tr;

    void Start()
    {
        bar = GetComponent<Image>();
        tr = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        tr.sizeDelta = new Vector2(watched_figther.health*3.5f, 25f);

        if (watched_figther.health <= 100 && watched_figther.health > 50)
        {
            bar.color = Color.green;
        }
        else if (watched_figther.health <= 50 && watched_figther.health > 15)
        {
            bar.color = Color.yellow;
        }
        else if (watched_figther.health < 10)
        {
            bar.color = Color.red;
        }
    }
}
