using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;

namespace Utils.Animation;

public class AnimationHandler : IAnimationHandler
{
    private readonly List<AnimationLink> _animationLinks = new();
    private Dictionary<AnimationLink, string> _animationMap= new();

    public int animationLength = 0;
    public bool allowInterruption = true;
    public bool repeatAnimation = true;
    public bool restartTimer = true;
    public bool syncAnimation = true;

    public string animationName;

    public AnimationHandler(string animationName){
        this.animationName = animationName;
    }
    public void SetAnimation(List<AnimationLink> animationLinks, string animationNameInput = null){
        animationNameInput ??= animationName;
    
        foreach(AnimationLink link in animationLinks){
            _animationLinks.Add(link);
            _animationMap[link] = animationNameInput;
        }
    }

    public virtual AnimationHandler Init(){
        return this;
    }

    public virtual void Start()
    {
        foreach(AnimationLink link in _animationLinks){
            link?.SetAnimation(_animationMap[link]);
            link?.SetFrame(0);
        }
    }

    public virtual void NextFrame(int n)
    {
        foreach(AnimationLink link in _animationLinks){
            if(syncAnimation){
                link.SetFrame(n);
            } else{
                link?.NextFrame();
            }
        }
    }

    public virtual void End(){ }

    public static void MapLocation(Node2D node, float[] location){
        node.Position = new Vector2(location[0], location[1]);
        node.Rotation = (float)Math.PI/180 * location[2];
    }
}
