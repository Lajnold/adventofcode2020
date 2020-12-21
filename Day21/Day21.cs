using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Part1And2();

void Part1And2()
{
    var foods = LoadFoods();

    var possibleIngredientsPerAllergen = new Dictionary<string, HashSet<string>>();
    foreach (var food in foods)
    {
        foreach (var allergen in food.Allergens)
        {
            var ingredientsForAllergen = possibleIngredientsPerAllergen.GetValueOrDefault(allergen);
            if (ingredientsForAllergen == null)
            {
                possibleIngredientsPerAllergen[allergen] = food.Ingredients.ToHashSet();
            }
            else
            {
                ingredientsForAllergen.IntersectWith(food.Ingredients);
            }
        }
    }

    var allIngredients = foods.SelectMany(f => f.Ingredients).ToList();
    var possibleAllergentIngredients = possibleIngredientsPerAllergen.Values.Aggregate(new HashSet<string>(), (acc, set) => { acc.UnionWith(set); return acc; });
    var unallergicIngredients = allIngredients.Except(possibleAllergentIngredients).ToList();
    var unallergicIngredientOccurrences = foods.SelectMany(f => unallergicIngredients.Intersect(f.Ingredients)).Count();
    Console.WriteLine($"Part 1: {unallergicIngredientOccurrences}");

    while (possibleIngredientsPerAllergen.Any(x => x.Value.Count > 1))
    {
        var singleIngredients = possibleIngredientsPerAllergen.Where(kvp => kvp.Value.Count == 1).ToList();
        foreach (var singleKvp in singleIngredients)
        {
            foreach (var otherKvp in possibleIngredientsPerAllergen.Where(x => x.Key != singleKvp.Key))
            {
                otherKvp.Value.Remove(singleKvp.Value.First());
            }
        }
    }

    var allergentIngredients = possibleIngredientsPerAllergen.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value.First()).ToList();
    Console.WriteLine($"Part 2: {string.Join(",", allergentIngredients)}");
}

FoodEntry[] LoadFoods()
{
    var lines = File.ReadLines("day21.txt");
    return lines.Select(ParseEntry).ToArray();
}

FoodEntry ParseEntry(string str)
{
    // abc cde (contains dairy, fish)
    var ingredientsSplit = str.Split(" (contains ");
    var ingredients = ingredientsSplit[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var contains = ingredientsSplit[1][..^1].Split(", ", StringSplitOptions.RemoveEmptyEntries);
    return new FoodEntry(ingredients, contains);
}

record FoodEntry(string[] Ingredients, string[] Allergens);
