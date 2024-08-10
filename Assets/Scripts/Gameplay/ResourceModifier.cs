using System.Collections.Generic;
using Schemas;

namespace Gameplay
{
    public interface IResourceModifierContainer
    {
        Dictionary<ResourceSchema.ResourceType, List<ResourceModifier>> GetResourceModifiers();

        public void AddMultiplier(ResourceSchema.ResourceType resourceType, float multiplier, int turns)
        {
            var mods = GetResourceModifiers();
            if (!mods.ContainsKey(resourceType))
            {
                mods.Add(resourceType, new List<ResourceModifier>());
            }
            
            mods[resourceType].Add(new ResourceModifier(
                ResourceModifier.ModifierType.Multiplier,
                multiplier,
                turns
            ));
        }
        
        public void AddFlatBonus(ResourceSchema.ResourceType resourceType, int bonus, int turns)
        {
            var mods = GetResourceModifiers();
            if (!mods.ContainsKey(resourceType))
            {
                mods.Add(resourceType, new List<ResourceModifier>());
            }
            
            mods[resourceType].Add(new ResourceModifier(
                ResourceModifier.ModifierType.Flat,
                bonus,
                turns
            ));
        }

        public void HandTurnCleanup()
        {
            var allModifiers = GetResourceModifiers();
            foreach (var (resourceType, modifiers) in allModifiers)
            {
                for (var i = modifiers.Count - 1; i >= 0 ; i--)
                {
                    if (modifiers[i].Turns == -1)
                    {
                        continue;
                    }

                    modifiers[i].Turns -= 1;
                    if (modifiers[i].Turns == 0)
                    {
                        modifiers.RemoveAt(i);
                    }
                }
            }
        }
    }
    
    public class ResourceModifier
    {
        /// <summary>
        /// We should talk about how we want this to function. For now, I've gone with:
        ///     (Base + SUM(Flat)) * SUM(Multiplier)
        /// </summary>
        public enum ModifierType
        {
            Flat,
            Multiplier      
        }

        /// <summary>
        /// The type of modifier this is.
        /// </summary>
        public readonly ModifierType Type;
        
        /// <summary>
        /// How much this modifier adds to the formula.
        /// </summary>
        public readonly float Amount;
        
        /// <summary>
        /// How many turns to apply this modifier for.
        /// -1 is indefinite.
        /// </summary>
        public int Turns;
        
        public ResourceModifier(ModifierType type, float amount, int turns)
        {
            Type = type;
            Amount = amount;
            Turns = turns;
        }
    }
}