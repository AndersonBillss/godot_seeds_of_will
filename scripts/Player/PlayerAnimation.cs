using Godot;

public partial class Player: CharacterBody2D{
	public AnimatedSprite2D head;
	public AnimatedSprite2D body;
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
}