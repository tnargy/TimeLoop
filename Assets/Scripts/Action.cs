using UnityEngine;

public abstract class Action
{
    public Action()
    {

    }
    
    public abstract ActionType ActionType { get; }
    
    public abstract void ProcessOnCharacter(Character target);
}

public class Move : Action
{
    Vector3 startPosition;
    Vector3 endPosition;
    float duration;

    public Move(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.duration = duration;
    }

    public override ActionType ActionType => ActionType.Move;

    public override void ProcessOnCharacter(Character target)
    {
        throw new System.NotImplementedException();
    }
}

public class Interact : Action
{
    public Interact()
    {
    }

    public override ActionType ActionType => ActionType.Move;

    public override void ProcessOnCharacter(Character target)
    {
        throw new System.NotImplementedException();
    }
}