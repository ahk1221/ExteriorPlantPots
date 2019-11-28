using UnityEngine;
using SMLHelper;

namespace ExteriorPlantPots
{
    public class Main
    {
        public static string BASIC_EXTERIOR_PLANT_POT_CLASSID = "BasicExteriorPlantPot";
        public static string COMPOSITE_EXTERIOR_PLANT_POT_CLASSID = "CompositeExteriorPlantPot";
        public static string CHIC_EXTERIOR_PLANT_POT_CLASSID = "ChicExteriorPlantPot";

        public static void Patch()
        {
            var basicExteriorPlantPot = new ExteriorPlantPotPrefab(
                BASIC_EXTERIOR_PLANT_POT_CLASSID,
                "Basic Exterior Plant Pot",
                "Titanium pot containing synthetic soil.",
                "Submarine/Build/PlanterPot",
                TechType.PlanterPot);

            var compositeExteriorPlantPot = new ExteriorPlantPotPrefab(
                COMPOSITE_EXTERIOR_PLANT_POT_CLASSID,
                "Composite Exterior Plant Pot",
                "Designer pot containing synthetic soil.",
                "Submarine/Build/PlanterPot2",
                TechType.PlanterPot2);

            var chicExteriorPlantPot = new ExteriorPlantPotPrefab(
                CHIC_EXTERIOR_PLANT_POT_CLASSID,
                "Chic Exterior Plant Pot",
                "Upmarket pot containing synthetic soil.",
                "Submarine/Build/PlanterPot3",
                TechType.PlanterPot3);

        }
    }
}
