using GameStates;
using Utility;

/// <summary>
/// This class acts as a ServiceLocator root and can be statically accessed via Game.Instance.
/// </summary>

public class Game : SingletonMonoBehavior<Game>
{
    public static Game Instance => ((Game)InternalInstance);

    // Mono Game Systems
    public MapGenerator MapGenerator;
    
    // Non-Mono Game Systems
    // stuff inside of this huge Game class
    public StateMachine StateMachine = new StateMachine();
    public Hand Hand = new Hand();
    public Deck Deck = new Deck();
    
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
