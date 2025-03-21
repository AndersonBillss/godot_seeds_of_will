using System;
using System.Collections.Generic;
using Godot;

namespace scripts.Utils.Animation;

public class AnimationHandler(string animationName) : IAnimationHandler
{
    private readonly List<AnimationLink> _animationLinks = [];
    private Dictionary<AnimationLink, string> _animationMap = [];

    public int animationLength = 0;
    public bool allowInterruption = true;
    public bool repeatAnimation = true;
    public bool restartTimer = true;
    public bool syncAnimation = true;

    public string animationName = animationName;

    public void SetAnimation(List<AnimationLink> animationLinks, string animationNameInput = null){
        animationNameInput ??= animationName;
        foreach(AnimationLink link in animationLinks){
            _animationLinks.Add(link);
            _animationMap[link] = animationNameInput;
        }
    }

    public void ChangeAnimation(AnimationLink animationLink, string newAnimation = null){
        newAnimation ??= animationName;
        _animationMap[animationLink] = newAnimation;
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
