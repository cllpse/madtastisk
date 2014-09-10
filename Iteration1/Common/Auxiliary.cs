using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library;


namespace Iteration1.Common
{
    public static class Auxiliary
    {
        public const String REGEX_SPACE = "\\s+";
        public const String REGEX_DIGIT = "\\d+";
        public const String REGEX_INGREDIENT_GROUP = "([a-zæøå]+)((\\d+)([a-zæøå]+))?,?";
        public const String REGEX_DASH = "-";
        public const String SUGGEST_QUERY_ALL_INGREDIENTS = "SUGGEST_QUERY_ALL_INGREDIENTS";


        public static IEnumerable<Entities.Ingredient> MatchIngredients(String query)
        {
            var regexSpace = new Regex(REGEX_SPACE);
            var regexDash = new Regex(REGEX_DASH);

            var regexIngredientGroup = new Regex(REGEX_INGREDIENT_GROUP, RegexOptions.IgnoreCase);

            var ingredientMatches = regexIngredientGroup.Matches(regexDash.Replace(regexSpace.Replace(query, ""), ",")).Cast<Match>().Select(m => new Entities.Ingredient
            {
                Name = m.Groups[1].Value,
                Unit = m.Groups[4].Success ? m.Groups[4].Value.ToString() : String.Empty,
                Amount = m.Groups[3].Success ? m.Groups[3].Value.ToDouble() : Double.MinValue
            });
            
            return ingredientMatches;
        }


        public static Models.IngredientDTO.Accuracy DetermineIngredientMatch(Entities.Ingredient needle, IEnumerable<Entities.Ingredient> haystack)
        {
            var accuracy = Models.IngredientDTO.Accuracy.NoMatch;

            var filtered = haystack.Where(straw => straw.Name == needle.Name);

            foreach (var straw in filtered)
            {
                accuracy = Models.IngredientDTO.Accuracy.PartialMatch;

                if (needle.Amount <= straw.Amount && needle.Unit == straw.Unit) accuracy = Models.IngredientDTO.Accuracy.FullMatch;
            }

            return accuracy;
        }
    }
}