using UnityEngine;
using SMLHelper;
using SMLHelper.Patchers;

namespace ExteriorPlantPots
{
    public class Main
    {
        public static string BASIC_EXTERIOR_PLANT_POT_CLASSID = "BasicExteriorPlantPot";
        public static string COMPOSITE_EXTERIOR_PLANT_POT_CLASSID = "CompositeExteriorPlantPot";
        public static string CHIC_EXTERIOR_PLANT_POT_CLASSID = "ChicExteriorPlantPot";

        public static TechType basicExteriorPlantPotTechType;
        public static TechType compositeExteriorPlantPotTechType;
        public static TechType chicExteriorPlantPotTechType;

        public static GameObject GetPotResource(string path, TechType techType, string classId)
        {
            var prefab = Resources.Load<GameObject>(path);
            var obj = GameObject.Instantiate(prefab);

            var constructable = obj.GetComponent<Constructable>();
            var techTag = obj.GetComponent<TechTag>();
            var planter = obj.GetComponent<Planter>();
            var prefabIdentifer = obj.GetComponent<PrefabIdentifier>();

            constructable.techType = techType;
            constructable.allowedInBase = false;
            constructable.allowedInSub = false;
            constructable.allowedOutside = true;
            constructable.allowedOnConstructables = false;
            constructable.forceUpright = true;

            planter.isIndoor = false;
            planter.environment = Planter.PlantEnvironment.Dynamic;

            techTag.type = techType;
            prefabIdentifer.ClassId = classId;

            var largeWorldEntity = obj.AddComponent<LargeWorldEntity>();
            largeWorldEntity.cellLevel = LargeWorldEntity.CellLevel.Global;

            return obj;
        }

        public static GameObject GetBasicPotResource()
        {
            return GetPotResource("Submarine/Build/PlanterPot", basicExteriorPlantPotTechType, BASIC_EXTERIOR_PLANT_POT_CLASSID);
        }

        public static GameObject GetCompositePotResource()
        {
            return GetPotResource("Submarine/Build/PlanterPot2", compositeExteriorPlantPotTechType, COMPOSITE_EXTERIOR_PLANT_POT_CLASSID);
        }

        public static GameObject GetChicPotResource()
        {
            return GetPotResource("Submarine/Build/PlanterPot3", chicExteriorPlantPotTechType, CHIC_EXTERIOR_PLANT_POT_CLASSID);
        }

        public static void Patch()
        {
            basicExteriorPlantPotTechType = TechTypePatcher.AddTechType(
                BASIC_EXTERIOR_PLANT_POT_CLASSID,
                "Basic Exterior Plant Pot",
                "Titanium pot containing synthetic soil.");

            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(
                BASIC_EXTERIOR_PLANT_POT_CLASSID,
                "Submarine/Build/BasicExteriorPlantPot",
                basicExteriorPlantPotTechType,
                GetBasicPotResource));

            CraftDataPatcher.customBuildables.Add(basicExteriorPlantPotTechType);
            CraftDataPatcher.AddToCustomGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, basicExteriorPlantPotTechType);

            compositeExteriorPlantPotTechType = TechTypePatcher.AddTechType(
                COMPOSITE_EXTERIOR_PLANT_POT_CLASSID,
                "Composite Exterior Plant Pot",
                "Designer pot containing synthetic soil.");

            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(
                COMPOSITE_EXTERIOR_PLANT_POT_CLASSID,
                "Submarine/Build/CompositeExteriorPlantPot",
                compositeExteriorPlantPotTechType,
                GetCompositePotResource));

            CraftDataPatcher.customBuildables.Add(compositeExteriorPlantPotTechType);
            CraftDataPatcher.AddToCustomGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, compositeExteriorPlantPotTechType);

            chicExteriorPlantPotTechType = TechTypePatcher.AddTechType(
                CHIC_EXTERIOR_PLANT_POT_CLASSID,
                "Chic Exterior Plant Pot",
                "Upmarket pot containing synthetic soil.");

            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(
                CHIC_EXTERIOR_PLANT_POT_CLASSID,
                "Submarine/Build/ChicExteriorPlantPot",
                chicExteriorPlantPotTechType,
                GetChicPotResource));

            CraftDataPatcher.customBuildables.Add(chicExteriorPlantPotTechType);
            CraftDataPatcher.AddToCustomGroup(TechGroup.ExteriorModules, TechCategory.ExteriorOther, chicExteriorPlantPotTechType);

            var basicPlantPotSprite = SpriteManager.Get(TechType.PlanterPot);
            var compositePlantPotSprite = SpriteManager.Get(TechType.PlanterPot2);
            var chicPlantPotSprite = SpriteManager.Get(TechType.PlanterPot3);

            CustomSpriteHandler.customSprites.Add(new CustomSprite(basicExteriorPlantPotTechType, basicPlantPotSprite));
            CustomSpriteHandler.customSprites.Add(new CustomSprite(compositeExteriorPlantPotTechType, compositePlantPotSprite));
            CustomSpriteHandler.customSprites.Add(new CustomSprite(chicExteriorPlantPotTechType, chicPlantPotSprite));

            var techData = new TechDataHelper
            {
                _craftAmount = 1,
                _ingredients = new System.Collections.Generic.List<IngredientHelper>()
                {
                    new IngredientHelper(TechType.Titanium, 2)
                }
            };

            CraftDataPatcher.customTechData.Add(basicExteriorPlantPotTechType, techData);
            CraftDataPatcher.customTechData.Add(compositeExteriorPlantPotTechType, techData);
            CraftDataPatcher.customTechData.Add(chicExteriorPlantPotTechType, techData);
        }
    }
}
