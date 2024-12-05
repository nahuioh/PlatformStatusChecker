public class ApiCallCounter
{
    public int Id { get; set; } // Clave primaria
    public string EndpointName { get; set; } // Nombre del endpoint
    public int CallCount { get; set; } // Contador de llamadas
}