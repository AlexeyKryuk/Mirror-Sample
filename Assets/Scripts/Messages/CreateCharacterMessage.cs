using Mirror;

public struct CreateCharacterMessage : NetworkMessage
{
    public GameItemType Type;
    public string Name;

    public CreateCharacterMessage(GameItemType type, string name)
    {
        Type = type;
        Name = name;
    }

    public override string ToString()
    {
        return $"Type: {Type} Name: {Name}";
    }
}