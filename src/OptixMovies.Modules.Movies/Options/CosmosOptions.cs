﻿using System.ComponentModel.DataAnnotations;

namespace OptixMovies.Modules.Movies.Options;

public class CosmosOptions
{
    [Required] public string Endpoint { get; set; }
    [Required] public string AuthKey { get; set; }
    [Required] public string DatabaseName { get; set; }
    public bool IgnoreCertificate { get; set; } = false;
    public string PartitionKey { get; set; } = "/id";

}
