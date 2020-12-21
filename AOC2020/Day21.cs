using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(21)]
    class Day21 : Solver
    {
        readonly List<(List<string> Ingredients, List<string> Allergens)> RecipeList;
        readonly HashSet<string> Allergens;
        readonly HashSet<string> BadIngredients;

        public Day21()
        {
            RecipeList = Rows.Select(x => (
                Ingredients: x.Split(" (contains ")[0].Split(" ").ToList(),
                Allergens: x.Split(" (contains ")[1].Replace(")", "").Split(", ").ToList())).ToList();
            Allergens = RecipeList.SelectMany(x => x.Allergens).ToHashSet();
            BadIngredients =
                Allergens
                    .SelectMany(allergen => RecipeList.Where(x => x.Allergens.Contains(allergen)).IntersectMany(x => x.Ingredients))
                    .ToHashSet();
        }

        public override object SolveOne()
        {
            return RecipeList.Sum(recipe => recipe.Ingredients.Count(ingredient => !BadIngredients.Contains(ingredient)));
        }

        public override object SolveTwo()
        {
            var allergenToIngredient = Allergens.ToDictionary(allergen => allergen,
                allergen =>
                    RecipeList
                        .Where(recipe => recipe.Allergens.Contains(allergen))
                        .IntersectMany(recipe => recipe.Ingredients)
                        .Intersect(BadIngredients).ToList());
            while (allergenToIngredient.Where(x => x.Value.Count > 1).Any())
            {
                var solvedIngredients = allergenToIngredient.Where(x => x.Value.Count == 1).Select(x => x.Value[0]).ToHashSet();
                allergenToIngredient
                    .Where(x => x.Value.Count > 1)
                    .Select(x => x.Key)
                    .ToList()
                    .ForEach(allergen => allergenToIngredient[allergen].RemoveAll(ingredient => solvedIngredients.Contains(ingredient)));
            }
            return string.Join(",", allergenToIngredient.ToList().OrderBy(x => x.Key).Select(x => x.Value[0]));
        }
    }
}
