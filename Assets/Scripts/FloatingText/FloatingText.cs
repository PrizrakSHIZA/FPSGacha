using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class FloatingText : MonoBehaviour
{
    float lifeTime;
    TextMeshPro textMeshPro;
    Color textColor;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            textColor.a -= 3f * Time.deltaTime;
            textMeshPro.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }

    public void Setup(string text, Color color, float lifeTime = .5f)
    {
        textMeshPro.SetText(text);
        textMeshPro.color = color;
        textColor = color;
        this.lifeTime = lifeTime;
    }
}
