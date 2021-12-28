using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PositiveNumberValidation : MonoBehaviour
{
    InputField inputField;

    void Start()
    {
        inputField = GetComponent<InputField>();

        inputField.onValueChanged.AddListener(delegate { Validate(); });
    }

    public void Validate()
    {
        if (inputField.text == "-")
        {
            inputField.text = string.Empty;
        }
    }
}
