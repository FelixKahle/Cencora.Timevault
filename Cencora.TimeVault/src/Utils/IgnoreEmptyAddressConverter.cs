// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;
using Cencora.Common.Maps;

namespace Cencora.TimeVault.Utils;

/// <summary>
/// Converts an <see cref="Address"/> object to and from JSON, ignoring empty addresses during serialization.
/// </summary>
public class IgnoreEmptyAddressConverter : JsonConverter<Address>
{
    /// <inheritdoc/>
    public override Address Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<Address>(ref reader, options);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Address value, JsonSerializerOptions options)
    {
        if (value == Address.Empty)
        {
            return;
        }

        JsonSerializer.Serialize(writer, value, options);
    }
}