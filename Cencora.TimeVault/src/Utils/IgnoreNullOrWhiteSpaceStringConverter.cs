// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cencora.TimeVault.Utils;

/// <summary>
/// Converts a <see cref="string"/> object to and from JSON, ignoring null or white space strings during serialization.
/// </summary>
public class IgnoreNullOrWhiteSpaceStringConverter : JsonConverter<string>
{
    /// <inheritdoc/>
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<string>(ref reader, options);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        JsonSerializer.Serialize(writer, value, options);
    }
}