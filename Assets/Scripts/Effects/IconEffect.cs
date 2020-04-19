using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffect : MonoBehaviour
{
    private float DURATION = 2.3f;
    private float SPEED = 0.3f;
    private float startingTime;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + SPEED * Time.deltaTime);
        sprite.color = new Color(1, 1, 1, 1 - ((Time.time - startingTime) / DURATION));
        if (Time.time > startingTime + DURATION)
            Destroy(gameObject);
    }
}
