using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    ProjectActions inputs;
    public PlayerController controller;

    protected override void Awake()
    {
        base.Awake();
        inputs = new ProjectActions();
    }

    private void OnEnable()
    {
        inputs.Enable();
        inputs.Overworld.Move.performed += controller.OnMove;
        inputs.Overworld.Move.canceled += controller.MoveCancelled;
        //inputs.Overworld.Drop.performed += controller.DropWeapon;
        //inputs.Overworld.Attack.performed += controller.Attack;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Overworld.Move.performed -= controller.OnMove;
        inputs.Overworld.Move.canceled -= controller.MoveCancelled;
        //inputs.Overworld.Drop.performed -= controller.DropWeapon;
        //inputs.Overworld.Attack.performed -= controller.Attack;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
