using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
   
    [SerializeField] float smoothInputSpeed = 0.2f;


    MainInputs controls;
    Vector2 moveInput;

    PlayerMotor motor;

    private Vector2 moveVector;
    private Vector2 smoothInputVelocity;

    void Awake(){
        controls = new MainInputs();
        motor = GetComponent<PlayerMotor>();
        Cursor.lockState = CursorLockMode.Locked;
        controls.Player.Jump.performed += jump;

        controls.Player.Sprint.started += sprint;
        controls.Player.Sprint.canceled += sprint;

        controls.Player.ToogleSprint.performed += toogleSprint;
    }


    void FixedUpdate(){
      Vector2 rawInput = controls.Player.Movement.ReadValue<Vector2>();
      moveVector = Vector2.SmoothDamp(moveVector, rawInput, ref smoothInputVelocity, smoothInputSpeed);
      motor.move(moveVector);
    }

    private void LateUpdate(){
      Vector2 lookInput = controls.Player.Look.ReadValue<Vector2>();
      //Debug.Log("look : " + lookInput);
      motor.look(lookInput);
      
    }

    private void jump(InputAction.CallbackContext ctx){
      motor.jump();
      // Debug.Log("[InputManager] Jump Trigerred!");
    }

    private void sprint(InputAction.CallbackContext ctx){
      motor.sprint(ctx.ReadValue<float>());
      // Debug.Log("[InputManager] Sprint Trigerred! : " + ctx.ReadValue<float>());
    }

    private void toogleSprint(InputAction.CallbackContext ctx){
      motor.toogleSprint();
      // Debug.Log("[InputManager] Sprint Trigerred! : " + ctx.ReadValue<float>());
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Disable();
    }
}