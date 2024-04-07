using Gameplay;
using Gameplay.World;
using Schemas;
using Utility;
using Loadout = Schemas.Loadout;

/// <summary>
/// This class acts as a ServiceLocator root and can be statically accessed via ServiceLocator.Instance.
/// It is what bootstraps the game scene.
/// </summary>

public class ServiceLocator : SingletonMonoBehaviour
{
    public static ServiceLocator Instance => ((ServiceLocator)InternalInstance);

    // WorldSettings and Config of the run (Move this stuff somewhere else later)
    // 
    public WorldSettings WorldSettings;
    public Loadout LoadoutSettings;
    public Schema.ProductionStatus MininmumStatus;
    
    // MonoBehavior backed systems
    public World World;
    public GameManager GameManager;

    // Non-MonoBehavior backed systems
    public Schemas.Schemas Schemas = new Schemas.Schemas();
    public Gameplay.Loadout Loadout = new Gameplay.Loadout();
    public Bank Bank = new Bank();
    public UIDisplayProcessor UIDisplayProcessor;

    // Moved the state machine to be handled by GameManager.
    public StateMachine StateMachine => GameManager.StateMachine;
    
    protected override void Awake()
    {
        base.Awake();
        
        Schemas.Initialize(MininmumStatus);
        World.Initialize(WorldSettings);
        Loadout.Initialize(LoadoutSettings);
        Bank.Initialize();
    }

    // TODO: Move to this paradigm for the rest of things?
    public void RegisterUIDisplayProcessor(UIDisplayProcessor processor)
    {
        UIDisplayProcessor = processor;
    }
}
