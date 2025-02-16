using System.Net.Http;
using Godot;
using Utils.Animation;

namespace Player;

public class AnimationHandlerBase: AnimationHandler{
    public AnimationLink head;
    public AnimationLink body;
    public AnimationLink rightArm;
    public AnimationLink leftArm;
    public AnimationLink rightLeg;
    public AnimationLink leftLeg;

    public AnimationHandlerBase(string animationName) : base(animationName){ 

    }
}

public class AnimationCreator{
    public AnimationLink head;
    public AnimationLink body;
    public AnimationLink rightArm;
    public AnimationLink leftArm;
    public AnimationLink rightLeg;
    public AnimationLink leftLeg;

    public AnimationCreator(AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg){
        this.head = head;
        this.body = body;
        this.rightArm = rightArm;
        this.leftArm = leftArm;
        this.rightLeg = rightLeg;
        this.leftLeg = leftLeg;
    }

    public T AddAnimationLinks<T>(T handlerBase) where T : AnimationHandlerBase{
        handlerBase.head = head;
        handlerBase.body = body;
        handlerBase.rightArm = rightArm;
        handlerBase.leftArm = leftArm;
        handlerBase.rightLeg = rightLeg;
        handlerBase.leftLeg = leftLeg;
        return handlerBase;
    }
}