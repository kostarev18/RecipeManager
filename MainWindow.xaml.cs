using System.Windows;
using System.Windows.Controls; 

namespace RecipeManager
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            var recipes = DatabaseHelper.GetAllRecipes();
            foreach (var recipe in recipes)
            {
                RecipeListBox.Items.Add(recipe.Name);
            }
        }

        private void AddRecipeOnClick(object sender, RoutedEventArgs e)
        {
            string name = RecipeNameTextBox.Text.Trim();
            string description = RecipeDescriptionTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Name and Description cannot be empty!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newRecipe = new Recipe
            {
                Name = RecipeNameTextBox.Text,
                Description = RecipeDescriptionTextBox.Text
            };

            DatabaseHelper.AddRecipe(newRecipe);

            RecipeListBox.Items.Add(newRecipe.Name);

            RecipeNameTextBox.Clear();
            RecipeDescriptionTextBox.Clear();
        }

        private void DeleteRecipeOnClick(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is string selectedRecipeName)
            {
                var recipes = DatabaseHelper.GetAllRecipes();
                var recipeToDelete = recipes.FirstOrDefault(r => r.Name == selectedRecipeName);

                if (recipeToDelete != null)
                {
                    DatabaseHelper.DeleteRecipe(recipeToDelete);
                    RecipeListBox.Items.Remove(selectedRecipeName);
                }
            }
            else
            {
                MessageBox.Show("Select a recipe to delete");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var watermark = GetWatermarkForTextBox(textBox);
                if (watermark != null)
                {
                    watermark.Visibility = Visibility.Collapsed;  //This we need for hiding a placeholder when user is writing something   
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                var watermark = GetWatermarkForTextBox(textBox);
                if (watermark != null)
                {
                    watermark.Visibility = Visibility.Visible;  // But if the field is empty, we should show the placeholder 
                }
            }
        }

        private TextBlock GetWatermarkForTextBox(TextBox textBox)
        {
            if (textBox.Name == "RecipeNameTextBox")
                return RecipeNameWatermark;
            else if (textBox.Name == "RecipeDescriptionTextBox")
                return RecipeDescriptionWatermark;
            else throw new InvalidOperationException("Unknown TextBox name");
        }

    }
}
