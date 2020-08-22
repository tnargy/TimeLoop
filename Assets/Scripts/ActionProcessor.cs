using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ActionProcessor
{
    private static Dictionary<ActionType, Action> _actions = new Dictionary<ActionType, Action>();
    private static bool _initialized;
    
    public static void Initialize()
    {
            _actions.Clear();

            var allActionTypes = Assembly.GetAssembly(typeof(Action)).GetTypes()
                .Where(t => typeof(Action).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var actionType in allActionTypes)
            {
                Action action = new Activator.CreateInstance(actionType) as Action;
                _actions.Add(action.ActionType, action);
            }

            _initialized = true;
    }

    public static void UseAction(Character target, ActionType actionType)
    {
        if (_initialized == false)
            Initialize();

        var action = _actions[actionType];
        action.ProcessOnCharacter(target);
    }
}
