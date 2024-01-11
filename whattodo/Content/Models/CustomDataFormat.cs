namespace whattodo;

public class CustomDataFormat
{
    public VariableType? Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public object? Value { get; set; }

    public CustomDataFormat(string varName, string description, object value, VariableType type)
    {
        Description = description;
        Name = varName;
        Value = value;
        Type = type;
    }
}
public enum VariableType
{
    Text,
    Date,
    Number,
    Id,
    List,
    Bool,
    Link,
    Timespan
}
