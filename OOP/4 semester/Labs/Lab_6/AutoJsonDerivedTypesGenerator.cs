using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace Lab_6;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class AutoJsonDerivedTypesAttribute : Attribute
{
    public string TypeDiscriminatorProperty { get; set; } = "$type";
}

public static class JsonExtensions
{
    public static JsonSerializerOptions AddAutoDerivedTypes(this JsonSerializerOptions options)
    {
        var baseTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttribute<AutoJsonDerivedTypesAttribute>() != null);

        foreach (var baseType in baseTypes)
        {
            var attribute = baseType.GetCustomAttribute<AutoJsonDerivedTypesAttribute>();
            var derivedTypes = baseType.Assembly.GetTypes()
                .Where(t => t != baseType && baseType.IsAssignableFrom(t));

            options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { typeInfo =>
                {
                    if (typeInfo.Type == baseType)
                    {
                        typeInfo.PolymorphismOptions ??= new JsonPolymorphismOptions();
                        typeInfo.PolymorphismOptions.TypeDiscriminatorPropertyName = attribute?.TypeDiscriminatorProperty;

                        foreach (var derivedType in derivedTypes)
                        {
                            typeInfo.PolymorphismOptions.DerivedTypes.Add(
                                new JsonDerivedType(derivedType, derivedType.Name)
                            );
                        }
                    }
                }}
            };
        }

        return options;
    }
}