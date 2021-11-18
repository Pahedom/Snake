using UnityEngine;
using UnityEngine.UI;

public class ResizeTextButton : MonoBehaviour
{
    [SerializeField] Text text;

    [SerializeField] float selectedSize;

    private int originalSize;

    void Start()
    {
        originalSize = text.fontSize;
    }

    public void PointerEnter()
    {
        text.fontSize = (int)(originalSize * selectedSize);
    }

    public void PointerExit()
    {
        text.fontSize = originalSize;
    }
}