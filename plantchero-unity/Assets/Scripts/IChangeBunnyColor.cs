
using UnityEngine;
using UnityEngine.InputSystem;

public class IChangeBunnyColor : MonoBehaviour
{
    void Start()
    {
        var input = GetComponent<PlayerInput>();
        Debug.Log(input.playerIndex);
        transform.GetChild(input.playerIndex).gameObject.SetActive(true);
    }
}
