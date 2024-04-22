using Gameplay;
using GameStates;
using Schemas;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [HideInInspector] public World World;
    [HideInInspector] public GameManager GameManager;

    // Non-MonoBehavior backed systems
    public Schemas.Schemas Schemas;
    public Gameplay.Loadout Loadout;
    public Gameplay.Bank Bank;
    
    [HideInInspector]
    public UIDisplayProcessor UIDisplayProcessor;

    // Moved the state machine to be handled by GameManager.
    public StateMachine StateMachine => GameManager.StateMachine;
    
    protected override void Awake()
    {
        base.Awake();
        
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
    }

    private void Update()
    {
        // todo: move somewhere else
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
    }
    
    public void Register(GameManager gameManager)
    {
        GameManager = gameManager;
    }
    
    public void Register(World world)
    {
        // todo: find a better way to initialize stuff now that SL is in Init scene
        Schemas = new Schemas.Schemas();
        Schemas.Initialize(MininmumStatus);

        Bank = new Gameplay.Bank();
        Bank.Initialize();
        
        World = world;
        World.Initialize(WorldSettings);
        
        Loadout = new Gameplay.Loadout();
        Loadout.Initialize(LoadoutSettings);
        
        // todo: update this, we need real flow handling
        GameManager.StateMachine.ChangeState(new StateSetup());
    }
    
    public void Register(UIDisplayProcessor processor)
    {
        UIDisplayProcessor = processor;
    }
}
