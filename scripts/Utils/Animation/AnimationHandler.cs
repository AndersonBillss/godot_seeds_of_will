using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;

namespace Utils.Animation;

public class AnimationHandler : IAnimationHandler
{
    private readonly List<AnimationLink> _animationLinks = new();
    public int animationLength = 0;
    public bool allowInterruption = true;
    public bool repeatAnimation = true;
    public bool restartTimer = true;

    public string animationName;

    public AnimationHandler(string animationName){
        this.animationName = animationName;
    }
    public void AnimateDefault(List<AnimationLink> animationLinks){
        foreach(AnimationLink link in animationLinks){
            _animationLinks.Add(link);
            GD.Print("ADD thing");
        }
    }

    public virtual AnimationHandler Init(){
        return this;
    }

    public virtual void Start()
    {
        foreach(AnimationLink link in _animationLinks){
            link.SetFrame(0);
            link.SetAnimation(animationName);
        }
    }

    public virtual void NextFrame(int n)
    {
        foreach(AnimationLink link in _animationLinks){
            link.NextFrame();
        }
    }

    public virtual void End(){
        foreach(AnimationLink link in _animationLinks){
            link.SetFrame(animationLength-1);
        }
    }

    public static void MapLocation(Node2D node, float[] location){
        node.Position = new Vector2(location[0], location[1]);
        node.Rotation = (float)Math.PI/180 * location[2];
    }
}
