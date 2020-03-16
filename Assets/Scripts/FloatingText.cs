using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float _gravity = 9.8f;
    [SerializeField] float _velocity = 0;
    [SerializeField] Text _text;
    [SerializeField] float _speedTransparentColor;

    private void Update()
    {
        _velocity += _gravity * Time.unscaledDeltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + _velocity * Time.unscaledDeltaTime, transform.position.z);

        Color color = _text.color;
        color.a -= _speedTransparentColor * Time.unscaledDeltaTime;
        if (color.a < 0)
        {
            color.a = 0;
            Destroy(gameObject);
        }

        _text.color = color;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
