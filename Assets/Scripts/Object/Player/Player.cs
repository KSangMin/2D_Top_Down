using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerCondition condition;
    [HideInInspector] public PlayerController controller;

    [HideInInspector] public bool isDead;

    private void Awake()
    {
        GameManager.Instance.player = this;

        condition = GetComponent<PlayerCondition>();
        controller = GetComponent<PlayerController>();
    }
}
