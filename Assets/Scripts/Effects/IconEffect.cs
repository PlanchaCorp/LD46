using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IconEffect : MonoBehaviour
{
    [SerializeField]
    private float DURATION = 2.3f;
    [SerializeField]
    private float SPEED = 5f;
    private Color color = new Color(1, 1, 1, 1);
    private float startingTime;
    private SpriteRenderer sprite;
    private TextMeshProUGUI tmpro;

    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
        tmpro = GetComponent<TextMeshProUGUI>();
        if (tmpro != null)
            color = tmpro.color;
        if (sprite != null)
            color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + SPEED * Time.deltaTime);
        Color nextColor = color;
        nextColor.a = 1 - ((Time.time - startingTime) / DURATION);
        if (sprite != null)
            sprite.color = nextColor;
        if (tmpro != null)
            tmpro.color = nextColor;
        if (Time.time > startingTime + DURATION)
            Destroy(gameObject);
    }
}
