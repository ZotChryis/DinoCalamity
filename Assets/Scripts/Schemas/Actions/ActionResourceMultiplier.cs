using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionResourceModifier")]  
    public class ActionResourceModifier : Action
    {
        public enum ModifierLocality
        {
            // Applies globally, aka to the Bank
            Global,     
            
            // Applies to all things of these types
            AllStructures,
            AllCards,
            AllTiles,

            // Applies locally to the location provided, IF it supports IResourceModifierContainer
            // Currently, that is any Invoker (Card, Building, Tile)
            Owner,
            Target
        }

        public ModifierLocality Locality;
        public ResourceSchema Resource;
        public float Multiplier = 0;
        public int FlatBonus = 0;
        public int Turns = -1;
        
        public override void Invoke(Invoker.Context context)
        {
            List<IResourceModifierContainer> modContainers = new List<IResourceModifierContainer>();
            switch (Locality)
            {
                case ModifierLocality.Global:
                    modContainers.Add(ServiceLocator.Instance.Bank);
                    break;
                
                case ModifierLocality.AllStructures:
                    var allStructures = ServiceLocator.Instance.World.GetAllStructures();
                    foreach (var structure in allStructures)
                    {
                        modContainers.Add(structure.Invoker);
                    }
                    break;
                
                case ModifierLocality.AllTiles:
                    var allTiles = ServiceLocator.Instance.World.GetAllTiles();
                    foreach (var tile in allTiles)
                    {
                        modContainers.Add(tile.Invoker);
                    }
                    break;
                
                case ModifierLocality.AllCards:
                    var allCards = ServiceLocator.Instance.Loadout.GetAllCards();
                    foreach (var card in allCards)
                    {
                        modContainers.Add(card.Invoker);
                    }
                    break;
                
                case ModifierLocality.Owner:
                    var owner = context.Owner;
                    if (owner is not IResourceModifierContainer)
                    {
                        return;
                    }

                    modContainers.Add((IResourceModifierContainer) context.Owner);
                    break;
                case ModifierLocality.Target:
                    var target = context.Target;
                    if (target is not IResourceModifierContainer)
                    {
                        return;
                    }

                    modContainers.Add((IResourceModifierContainer) context.Target);
                    break;
            }

            if (Multiplier > 0)
            {
                modContainers.ForEach(modContainer => modContainer.AddMultiplier(Resource.Type, Multiplier, Turns));
            }
            
            if (FlatBonus > 0)
            {
                modContainers.ForEach(modContainer => modContainer.AddFlatBonus(Resource.Type, FlatBonus, Turns));
            }
        }
    }
}
