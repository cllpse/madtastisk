using System;
using System.Collections.Generic;


namespace Iteration3.Models
{
    public class RecipeDTO : Entities.Recipe, IEquatable<RecipeDTO>
    {
        public List<IngredientDTO> Ingredients { get; set; }


        public Boolean Equals(RecipeDTO other)
        {
            return Id == other.Id;
        }

        
        public override Int32 GetHashCode()
        {
            return Id;
        }
    }
}