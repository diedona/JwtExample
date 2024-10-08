﻿namespace JwtExample.CrossCutting.Options;

public class JwtOptions
{
    public const string Jwt = "Jwt";

    public string Key { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int MinutesToLive { get; set; } = 0;
}
