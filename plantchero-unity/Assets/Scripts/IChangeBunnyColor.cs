
using UnityEngine;
using UnityEngine.InputSystem;

public class IChangeBunnyColor : MonoBehaviour
{
    void Start()
    {
        var input = GetComponent<PlayerInput>();
        transform.GetChild(input.playerIndex).gameObject.SetActive(true);
    }
}
