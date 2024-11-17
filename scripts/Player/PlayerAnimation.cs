using System;
using Godot;

public partial class Player: CharacterBody2D{
	public AnimatedSprite2D head;
	public AnimatedSprite2D body;
    public Node2D BodyRotationCenter;
	public AnimatedSprite2D rightArm;
	public AnimatedSprite2D leftArm;
	public AnimatedSprite2D rightLeg;
	public AnimatedSprite2D leftLeg;

    public void PlayAnimation(string animationName){
        if(animationName == "walk"){
            rightLeg.Play("walk");
            leftLeg.Play("walk");
        }
        if(animationName == "default"){
            rightLeg.Play("default");
            leftLeg.Play("default");
        }
        if(animationName == "jump"){
            rightLeg.Play("jump");
            leftLeg.Play("jump");
        }
    }

    public void RotateBody(float degrees){
        SetRotationAround(body, BodyRotationCenter.Position, body.Rotation+degrees);
	}


    public static void SetRotationAround(Node2D rotatingItem, Vector2 relativePosition, float degrees){
        Vector2 direction = -relativePosition;
        
        // Rotate the vector
        direction = direction.Rotated(degrees);
        rotatingItem.Rotation = degrees;
        
        // Set the new position
        rotatingItem.Position = relativePosition + direction;
    }
}