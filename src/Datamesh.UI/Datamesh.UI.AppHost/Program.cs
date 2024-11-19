using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Datamesh_UI_ApiService>("apiservice");
var dmsapi = builder.AddProject<Datamesh_API>("dmsapi");


builder.AddProject<Projects.Datamesh_UI_Web>("webfrontend")
    .WithExternalHttpEndpoints()
     .WithReference(dmsapi)
    .WithReference(apiService);

builder.Build().Run();
