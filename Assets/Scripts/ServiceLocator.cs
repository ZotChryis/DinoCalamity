using Gameplay;
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
    public MapGenerator MapGenerator;

    // Non-MonoBehavior backed systems
    public StateMachine StateMachine = new StateMachine();
    public Player Player = new Player();

    public void Awake()
    {
        MapGenerator.Initialize();
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
