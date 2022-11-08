using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour{

    CharacterController controller;
    [SerializeField] Camera cam;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistane = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float sprintModifier = 2f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] float sensivity = 30f;
    


    float xRotation = 0f;
    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    bool toogleSprinting;


     


    void Start(){
        controller = GetComponent<CharacterController>();
    }

    public void move(Vector2 value){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistane, groundMask);

        float speed = (isSprinting || toogleSprinting)? walkSpeed * sprintModifier : walkSpeed ;


        Vector3 moveInput = transform.right * value.x + transform.forward * value.y;

        moveInput = moveInput * Time.deltaTime * speed;
        controller.Move(moveInput);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistane, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -0.001f;            
        }else{
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public void look(Vector2 value){
        xRotation -= ( value.y * Time.deltaTime ) * sensivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        transform.Rotate(Vector3.up * (value.x * Time.deltaTime) * sensivity);
    }

    public void jump(){
        if(isGrounded){
            velocity.y = Mathf.Sqrt( jumpHeight * -2f * gravity);            
        }
    }

    public void sprint(float value){
        isSprinting = (value == 1 )? true : false;
    }

    public void toogleSprint(){
        toogleSprinting = !toogleSprinting;
    }


}
