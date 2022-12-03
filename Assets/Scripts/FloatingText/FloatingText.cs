using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class FloatingText : MonoBehaviour
{
    float lifeTime;
    TextMeshPro textMeshPro;
    Color32 textColor;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        textColor.a = 0;

        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            textMeshPro.color = Color.Lerp(textMeshPro.color, textColor, 15f * Time.deltaTime);
            if (textMeshPro.color.a <= 0.01)
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
