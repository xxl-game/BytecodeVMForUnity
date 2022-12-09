using System;

public class ConfigNameAttribute : Attribute
{
    public string Name { get; }

    public ConfigNameAttribute(string name)
    {
        Name = name;
    }
}