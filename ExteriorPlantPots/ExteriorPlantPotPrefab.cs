using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace ExteriorPlantPots
{
    public class ExteriorPlantPotPrefab : ModPrefab
    {
        private string basePlantPotPath;

        public ExteriorPlantPotPrefab(string plantPotId, string displayName, string tooltip, string basePotPath, TechType basePotTechType) : base(plantPotId, $"Submarine/Build/{plantPotId}")
        {
            this.basePlantPotPath = basePotPath;

            // Register the TechType
            TechType = TechTypeHandler.AddTechType(plantPotId, displayName, tooltip, false);

            // Register the Sprite(icon) of the new TechType with the icon of the base plant pot.
            SpriteHandler.RegisterSprite(TechType, SpriteManager.Get(basePotTechType));

            // Set the recipe of the TechType.
            CraftDataHandler.SetTechData(TechType, new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Titanium, 2)
                }
            });

            // Add it as a buildable.
            CraftDataHandler.AddBuildable(TechType);

            // Add it to the group in the PDA.
            CraftDataHandler.AddToGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, TechType);

            // Set it to unlock after the base TechType is unlocked.
            KnownTechHandler.SetAnalysisTechEntry(basePotTechType, new List<TechType>() { TechType });

            // Register this prefab.
            PrefabHandler.RegisterPrefab(this);
        }

        public override GameObject GetGameObject()
        {
            var prefab = Resources.Load<GameObject>(basePlantPotPath);
            var obj = GameObject.Instantiate(prefab);

            var constructable = obj.GetComponent<Constructable>();
            var techTag = obj.GetComponent<TechTag>();
            var planter = obj.GetComponent<Planter>();
            var prefabIdentifer = obj.GetComponent<PrefabIdentifier>();

            constructable.techType = TechType;
            constructable.allowedInBase = false;
            constructable.allowedInSub = false;
            constructable.allowedOutside = true;
            constructable.allowedOnConstructables = false;
            constructable.forceUpright = true;

            planter.isIndoor = false;
            planter.environment = Planter.PlantEnvironment.Dynamic;

            techTag.type = TechType;
            prefabIdentifer.ClassId = ClassID;

            var largeWorldEntity = obj.AddComponent<LargeWorldEntity>();
            largeWorldEntity.cellLevel = LargeWorldEntity.CellLevel.Global;

            return obj;
        }
    }
}
