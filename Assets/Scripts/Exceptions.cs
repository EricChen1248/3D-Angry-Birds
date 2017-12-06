using System;

public class LevelException : Exception
{
    public LevelException() { }
    public LevelException(string message) : base(message) { }
    public LevelException(int currentLevel, int level) : base(string.Format("Current level is {0}, target level is {1}",currentLevel, level)) {  }
    public LevelException(string message, Exception inner) : base(message, inner) { }
    protected LevelException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}