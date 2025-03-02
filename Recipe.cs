using SQLite;

public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Recipe() { }
}
