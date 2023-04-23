using System.Text.Json.Serialization;
using System.Text.Json;
using Azure.AI.OpenAI;
using System;

namespace AITavern.Tavern.Converters;

public class ChatRoleJsonConverter : JsonConverter<ChatRole>
{
    public override ChatRole Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        ChatRole role;
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
        }
        
        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "label")
        {
            reader.Read();
            role = new(reader.GetString()!);
        }

        do
        {
            reader.Read();
        } while(reader.TokenType != JsonTokenType.EndObject);
        
        return role;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ChatRole chatRole,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("label", chatRole.ToString());
        writer.WriteEndObject();
    }
}