namespace Iteration1.Models
{
    public class IngredientDTO : Entities.Ingredient
    {
        public enum Accuracy
        {
            FullMatch,
            PartialMatch,
            NoMatch
        }


        private Accuracy _accuracy = Accuracy.NoMatch;
        
        public Accuracy MatchAccuracy
        {
            get
            {
                return _accuracy;
            }

            set
            {
                _accuracy = value;
            }
        }
    }
}