using Gameplay;
using Gameplay.World;
using GameStates;
using Utility;

/// <summary>
/// This class acts as a ServiceLocator root and can be statically accessed via ServiceLocator.Instance.
/// It is what bootstraps the game scene.
/// </summary>

public class ServiceLocator : SingletonMonoBehavior<ServiceLocator>
{
    public static ServiceLocator Instance => ((ServiceLocator)InternalInstance);

    // MonoBehavior backed systems
    public World World;

    // Non-MonoBehavior backed systems
    public Schemas.Schemas Schemas = new Schemas.Schemas();
    public Player Player = new Player();
    public Bank Bank = new Bank();
    public UIDisplayProcessor UIDisplayProcessor;
    
    //  todo: go in some gamemanager
    public StateMachine StateMachine = new StateMachine();
    
    public void Awake()
    {
        Schemas.Initialize();
        Bank.Initialize();
    }

    public void Start()
    {
        StateMachine.ChangeState(new StateSetup());
    }

    public void Update()
    {
        StateMachine.Update();
    }

    public void RegisterUIDisplayProcessor(UIDisplayProcessor processor)
    {
        UIDisplayProcessor = processor;
    }
}
