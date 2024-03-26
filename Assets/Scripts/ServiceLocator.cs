using Gameplay;
using GameStates;
using Schemas;
using Utility;

/// <summary>
/// This class acts as a ServiceLocator root and can be statically accessed via ServiceLocator.Instance.
/// It is what bootstraps the game scene.
/// </summary>

public class ServiceLocator : SingletonMonoBehavior<ServiceLocator>
{
    public static ServiceLocator Instance => ((ServiceLocator)InternalInstance);

    // MonoBehavior backed systems
    public MapGenerator MapGenerator;

    // Non-MonoBehavior backed systems
    public StaticData Schemas = new StaticData();
    public Player Player = new Player();
    
    //  todo: go in some world/gamemanager location
    public StateMachine StateMachine = new StateMachine();
    
    public void Awake()
    {
        Schemas.Initialize();
    }

    public void Start()
    {
        StateMachine.ChangeState(new StateSetup());
    }

    public void Update()
    {
        StateMachine.Update();
    }
}
