namespace Iteration1.Models
{
    public class HeaderConfiguration
    {
        public enum MenuItem
        {
            None,
            SearchRecipe,
            AddRecipe
        }


        public MenuItem SelectedMenuItem { get; set; }
    }
}