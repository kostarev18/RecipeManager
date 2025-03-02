using SQLite;
using System;
using System.IO;

public class DatabaseHelper
{
    private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");
    private static SQLiteConnection? db;

    public static SQLiteConnection GetConnection()
    {
        if (db == null)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Recipe>();
        }
        return db;
    }

    public static void AddRecipe(Recipe recipe)
    {
        var db = GetConnection();
        db.Insert(recipe);
    }

    public static List<Recipe> GetAllRecipes()
    {
        var db = GetConnection();
        return db.Table<Recipe>().ToList();
    }
}