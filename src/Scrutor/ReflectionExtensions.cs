using System;
using Microsoft.Extensions.Internal;

namespace Scrutor;

internal static class ReflectionExtensions
{
    public static string ToFriendlyName(this Type type)
    {
        return TypeNameHelper.GetTypeDisplayName(type, includeGenericParameterNames: true);
    }

    public static bool IsOpenGeneric(this Type type)
    {
        return type.IsGenericTypeDefinition;
    }

    public static bool HasCompatibleGenericArguments(this Type type, Type genericTypeDefinition)
    {
        var genericArguments = type.GetGenericArguments();
        try
        {
            _ = genericTypeDefinition.MakeGenericType(genericArguments);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
